//MIT License

//Copyright (c) 2022 Ezequiel Medina

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using System;
using System.Threading.Tasks;
using LiveSplit.UI.Components;
using System.Windows.Forms;
using System.Threading;
using System.Xml;
using HitCounterManager;

namespace AutoSplitterCore
{
    public class AslSplitter
    {
        private static LiveSplit.Model.LiveSplitState state = new LiveSplit.Model.LiveSplitState();
        public static ASLComponent asl = new ASLComponent(state);
        public Control control = asl.GetSettingsControl(LiveSplit.UI.LayoutMode.Vertical);
        public ProfilesControl _profile;
        public bool enableSplitting = false;
        public bool _SplitGo = false;
        public bool _PracticeMode = false;
        private static readonly object _object = new object();       
        private System.Windows.Forms.Timer _update_timer = new System.Windows.Forms.Timer() { Interval = 1500 };
        private System.Windows.Forms.Timer _update_timer2 = new System.Windows.Forms.Timer() { Interval = 1000 };
        public bool DebugMode = false;


        public void setData(XmlNode node, ProfilesControl profile)
        {
            this._profile = profile;
            if (node != null) { asl.SetSettings(node); asl.UpdateScript(); }
            _update_timer.Tick += (sender, args) => asl.UpdateScript();
            _update_timer2.Tick += (sender, args) => SplitGo();
            _update_timer.Enabled = true;                    
        }

        public XmlNode getData(XmlDocument doc)
        {
            return asl.GetSettings(doc);
        }
           
        public void LoadAutoSplitterProcedure()
        {
            var task1 = new Task(() =>
            {
                Split();
            });

            task1.Start();
        }

        public void setStatusSplitting(bool status)
        {
            enableSplitting = status;
            if (status) { LoadAutoSplitterProcedure(); _update_timer2.Enabled = true; } else { _update_timer2.Enabled = false; }
        }

        public void UpdateScript()
        {
            asl.UpdateScript();
        }

        #region init()

        public void SplitGo()
        {
            if (_SplitGo)
            {
                if (!DebugMode) { try { _profile.ProfileSplitGo(+1); } catch (Exception) { } } else { Thread.Sleep(15000); }
                _SplitGo = false;
            }
        }

        private void SplitCheck()
        {
            lock (_object)
            {
                if (_SplitGo) { Thread.Sleep(2000); }
                _SplitGo = true;
            }
        }
        private void Split()
         {
             while (enableSplitting)
             {
                 Thread.Sleep(1000);
                 if (asl.Script != null)
                 {
                     if (asl.Script.shouldSplit)
                     {
                        if (!_PracticeMode) { SplitCheck(); }     
                        asl.Script.shouldSplit = false;
                     }
                 }
             }
         }
         #endregion
    }
}
