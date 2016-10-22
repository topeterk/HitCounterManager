'MIT License

'Copyright(c) 2016 Peter Kirmeier

'Permission Is hereby granted, free Of charge, to any person obtaining a copy
'of this software And associated documentation files (the "Software"), to deal
'in the Software without restriction, including without limitation the rights
'to use, copy, modify, merge, publish, distribute, sublicense, And/Or sell
'copies of the Software, And to permit persons to whom the Software Is
'furnished to do so, subject to the following conditions:

'The above copyright notice And this permission notice shall be included In all
'copies Or substantial portions of the Software.

'THE SOFTWARE Is PROVIDED "AS IS", WITHOUT WARRANTY Of ANY KIND, EXPRESS Or
'IMPLIED, INCLUDING BUT Not LIMITED To THE WARRANTIES Of MERCHANTABILITY,
'FITNESS FOR A PARTICULAR PURPOSE And NONINFRINGEMENT. IN NO EVENT SHALL THE
'AUTHORS Or COPYRIGHT HOLDERS BE LIABLE For ANY CLAIM, DAMAGES Or OTHER
'LIABILITY, WHETHER In AN ACTION Of CONTRACT, TORT Or OTHERWISE, ARISING FROM,
'OUT OF Or IN CONNECTION WITH THE SOFTWARE Or THE USE Or OTHER DEALINGS IN THE
'SOFTWARE.

' Holds data about a hotkey
Public Class ShortcutsKey
    Public Const KEY_PRESSED_NOW As Long = &H8000

    <Runtime.InteropServices.DllImport("User32.dll")>
    Public Shared Function GetAsyncKeyState(ByVal vKey As Long) As System.UInt16
    End Function

    Private _used As Boolean ' tells if shortcut is registered at windows
    Private _down As Boolean ' tells if shortcut is currently pressed
    Public valid As Boolean ' tell if a valid key/modifier pair was ever set
    Public key As KeyEventArgs

    Public Sub New()
        _used = False
        _down = False
        valid = False
        key = New KeyEventArgs(0)
    End Sub

    Public Function ShallowCopy() As ShortcutsKey
        Return DirectCast(Me.MemberwiseClone(), ShortcutsKey)
    End Function

    ' Checks current key stat and returns if all registered keys are pressed at the moment
    Private Function CheckPressedState(ShiftState As Boolean, ControlState As Boolean, AltState As Boolean) As Boolean
        CheckPressedState = False ' assume not pressed

        If Not GetAsyncKeyState(key.KeyCode) And KEY_PRESSED_NOW Then Exit Function
        If key.Shift And Not ShiftState Then Exit Function
        If key.Control And Not ControlState Then Exit Function
        If key.Alt And Not AltState Then Exit Function

        CheckPressedState = True
    End Function

    ' Checks if all registered keys are just pressed the first time
    Public Function WasPressed(ShiftState As Boolean, ControlState As Boolean, AltState As Boolean) As Boolean
        WasPressed = False ' assume not pressed

        If CheckPressedState(ShiftState, ControlState, AltState) Then ' Is key down right now?
            If Not _down Then ' Did key go down the first time?
                WasPressed = True ' Okay, key was just pressed!
                _down = True ' Make sure key gets released before going down again
            End If
        Else
            _down = False ' Key is up again, so re-arm 'first time' trigger 
        End If
    End Function

    ' tells if shortcut is registered as hotkey
    Public Property used() As Boolean
        Get
            Return _used
        End Get
        Set(IsUsed As Boolean)
            CheckPressedState(False, False, False) ' flush current state
            _used = IsUsed
        End Set
    End Property
End Class

' Manages all hot keys aka shortcuts
Public Class Shortcuts
    Public Const MOD_ALT As Integer = &H0001
    Public Const MOD_CONTROL As Integer = &H0002
    Public Const MOD_SHIFT As Integer = &H0004
    Public Const WM_HOTKEY As Integer = &H312
    Public Const VK_SHIFT As Long = &H10
    Public Const VK_CONTROL As Long = &H11
    Public Const VK_MENU As Long = &H12
    Public Const KEY_PRESSED_NOW As Long = &H8000

    <Runtime.InteropServices.DllImport("User32.dll")>
    Public Shared Function RegisterHotKey(ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Integer
    End Function

    <Runtime.InteropServices.DllImport("User32.dll")>
    Public Shared Function UnregisterHotKey(ByVal hwnd As IntPtr, ByVal id As Integer) As Integer
    End Function

    Public Delegate Sub TimerProc(ByVal hwnd As IntPtr, ByVal uMsg As UInteger, ByVal nIDEvent As IntPtr, ByVal dwTime As UInteger)
    Private TimerProcKeepAliveReference As TimerProc ' prevent garbage garbage collector freeing up the callback without any reason

    <Runtime.InteropServices.DllImport("User32.dll")>
    Public Shared Function SetTimer(ByVal hwnd As IntPtr, ByVal nIDEvent As IntPtr, ByVal uElapse As UInteger, ByVal lpTimerFunc As TimerProc) As IntPtr
    End Function

    <Runtime.InteropServices.DllImport("User32.dll")>
    Public Shared Function KillTimer(ByVal hwnd As IntPtr, ByVal nIDEvent As IntPtr) As Boolean
    End Function

    <Runtime.InteropServices.DllImport("User32.dll")>
    Public Shared Function GetAsyncKeyState(ByVal vKey As Long) As System.UInt16
    End Function

    <Runtime.InteropServices.DllImport("User32.dll")>
    Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
    End Function

    Public Enum SC_Type
        SC_Type_Reset = 0
        SC_Type_Hit = 1
        SC_Type_Split = 2
        SC_Type_MAX = 3
    End Enum
    Public Enum SC_HotKeyMethod
        SC_HotKeyMethod_Sync = 0 ' = RegisterHotKey + UnregisterHotKey
        SC_HotKeyMethod_Async = 1 ' = SetTimer + KillTimer + GetAsyncKeyState + SendMessage
    End Enum

    Private hwnd As IntPtr
    Private sc_list(SC_Type.SC_Type_MAX - 1) As ShortcutsKey
    Private method As SC_HotKeyMethod

    Public NextStart_Method As SC_HotKeyMethod

    ' Build empty hot key list, save window handle (and setup timer)
    Public Sub New(WindowHandle As IntPtr, HotKeyMethod As SC_HotKeyMethod)
        hwnd = WindowHandle
        method = HotKeyMethod
        NextStart_Method = method
        For i = 0 To SC_Type.SC_Type_MAX - 1 Step 1
            sc_list(i) = New ShortcutsKey()
        Next

        If method = SC_HotKeyMethod.SC_HotKeyMethod_Async Then
            TimerProcKeepAliveReference = New TimerProc(AddressOf timer_event) ' stupid garbage collection fixup
            SetTimer(hwnd, 0, 20, TimerProcKeepAliveReference)
        End If
    End Sub

    ' Destroy object (and free timer)
    Protected Overrides Sub Finalize()
        If method = SC_HotKeyMethod.SC_HotKeyMethod_Async Then KillTimer(hwnd, 0)
    End Sub

    ' Configures and disables a shortcut for a key
    Public Sub Key_PreSet(Id As SC_Type, key As ShortcutsKey)
        Key_Set(Id, key.ShallowCopy()) ' configures and validate key (enable once)
        Key_SetState(Id, False) ' disable afterwards
    End Sub

    ' Registers and unregisters a hotkey
    Private Sub HotKeyRegister(Id As SC_Type, key As ShortcutsKey, Enable As Boolean)
        Dim modifier As Integer = 0

        If key.key.Shift Then modifier += MOD_SHIFT
        If key.key.Control Then modifier += MOD_CONTROL
        If key.key.Alt Then modifier += MOD_ALT

        If method = SC_HotKeyMethod.SC_HotKeyMethod_Sync Then

            If Enable Then
                If 0 = RegisterHotKey(hwnd, Id, modifier, key.key.KeyCode) Then
                    'anything went wrong while registering, clear key..
                    key.used = False
                    key.valid = False
                Else
                    key.used = True
                    key.valid = True
                End If
            Else
                UnregisterHotKey(hwnd, Id)
                key.used = False
            End If

        ElseIf method = SC_HotKeyMethod.SC_HotKeyMethod_Async Then

            If Enable Then
                If 0 = RegisterHotKey(hwnd, Id, modifier, key.key.KeyCode) Then
                    'anything went wrong while registering, clear key..
                    key.used = False
                    key.valid = False
                Else
                    UnregisterHotKey(hwnd, Id) ' don't use this method, we just wanted to check if keycode works
                    key.used = True
                    key.valid = True
                End If
            Else
                key.used = False
            End If

        End If
    End Sub

    ' Configures and enables a shortcut for a key
    Public Sub Key_Set(Id As SC_Type, key As ShortcutsKey)
        If Key_Get(Id).used Then
            HotKeyRegister(Id, key, False)
        End If

        HotKeyRegister(Id, key, True)
        sc_list.SetValue(key.ShallowCopy(), Id)
    End Sub

    ' Enables or Disables a shortcut
    Public Sub Key_SetState(Id As SC_Type, IsEnabled As Boolean)
        Dim key = Key_Get(Id)

        If Not key.valid Then Exit Sub

        If Not key.used And IsEnabled Then
            HotKeyRegister(Id, key, True)
        ElseIf key.used And Not IsEnabled Then
            HotKeyRegister(Id, key, False)
        End If
        sc_list.SetValue(key, Id)
    End Sub

    ' Get the bound key of a shortcut
    Public Function Key_Get(Id As SC_Type) As ShortcutsKey
        Dim key As ShortcutsKey = sc_list.GetValue(Id)
        Key_Get = key.ShallowCopy()
    End Function

    ' Timer interrupt to check for keys pressed (needed for some full screen applications or applications that do not use window messages
    Sub timer_event(ByVal hwnd As IntPtr, ByVal uMsg As UInteger, ByVal nIDEvent As IntPtr, ByVal dwTime As UInteger)
        Dim k_shift As Boolean
        Dim k_control As Boolean
        Dim k_alt As Boolean

        k_shift = GetAsyncKeyState(VK_SHIFT) And KEY_PRESSED_NOW
        k_control = GetAsyncKeyState(VK_CONTROL) And KEY_PRESSED_NOW
        k_alt = GetAsyncKeyState(VK_MENU) And KEY_PRESSED_NOW

        For i = 0 To SC_Type.SC_Type_MAX - 1 Step 1
            If sc_list(i).used Then
                If sc_list(i).WasPressed(k_shift, k_control, k_alt) Then SendMessage(hwnd, WM_HOTKEY, i, 0)
            End If
        Next
    End Sub

End Class
