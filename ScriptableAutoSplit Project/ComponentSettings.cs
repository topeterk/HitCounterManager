using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.ASL;
using System.IO;

namespace LiveSplit.UI.Components
{
    public partial class ComponentSettings : UserControl
    {
        public string ScriptPath { get; set; }

        public bool ObjectChecked { get; set; }

        // if true, next path loaded from settings will be ignored
        private bool _ignore_next_path_setting;

        private Dictionary<string, CheckBox> _basic_settings;

        // Save the state of settings independant of actual ASLSetting objects
        // or the actual GUI components (checkboxes). This is used to restore
        // the state when the script is first loaded (because settings are
        // loaded before the script) or reloaded.
        //
        // State is synchronized with the ASLSettings when a script is
        // successfully loaded, as well as when the checkboxes/tree check
        // state is changed by the user or program. It is also updated
        // when loaded from XML.
        //
        // State is stored from the current script, or the last loaded script
        // if no script is currently loaded.

        // Start/Reset/Split checkboxes
        private Dictionary<string, bool> _basic_settings_state;

        // Custom settings
        private Dictionary<string, bool> _custom_settings_state;


        public ComponentSettings()
        {
            InitializeComponent();

            ScriptPath = string.Empty;

            this.txtScriptPath.DataBindings.Add("Text", this, "ScriptPath", false,
                DataSourceUpdateMode.OnPropertyChanged);

            SetGameVersion(null);
            UpdateCustomSettingsVisibility();

            _basic_settings = new Dictionary<string, CheckBox> {
                // Capitalized names for saving it in XML.
                ["Split"] = checkboxSplit
            };

            _basic_settings_state = new Dictionary<string, bool>();
            _custom_settings_state = new Dictionary<string, bool>();
        }

        public ComponentSettings(string scriptPath)
            : this()
        {
            ScriptPath = scriptPath;
            _ignore_next_path_setting = true;
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            XmlElement settings_node = document.CreateElement("Settings");

            settings_node.AppendChild(SettingsHelper.ToElement(document, "Version", "1.5"));
            settings_node.AppendChild(SettingsHelper.ToElement(document, "ScriptPath", ScriptPath));
            AppendBasicSettingsToXml(document, settings_node);
            AppendCustomSettingsToXml(document, settings_node);

            return settings_node;
        }

        // Loads the settings of this component from Xml. This might happen more than once
        // (e.g. when the settings dialog is cancelled, to restore previous settings).
        public void SetSettings(XmlNode settings)
        {
            var element = (XmlElement)settings;
            if (!element.IsEmpty)
            {
                if (!_ignore_next_path_setting)
                    ScriptPath = SettingsHelper.ParseString(element["ScriptPath"], string.Empty);
                _ignore_next_path_setting = false;
                ParseBasicSettingsFromXml(element);
                ParseCustomSettingsFromXml(element);
            }
        }

        public void SetGameVersion(string version)
        {
            this.lblGameVersion.Text = string.IsNullOrEmpty(version) ? "" : "Game Version: " + version;
        }

        /// <summary>
        /// Populates the component with the settings defined in the ASL script.
        /// </summary>
        public void SetASLSettings(ASLSettings settings)
        {
            InitASLSettings(settings, true);
        }

        /// <summary>
        /// Empties the GUI of all settings (but still keeps settings state
        /// for the next script load).
        /// </summary>
        public void ResetASLSettings()
        {
            InitASLSettings(new ASLSettings(), false);
        }

        private void InitASLSettings(ASLSettings settings, bool script_loaded)
        {
            if (string.IsNullOrWhiteSpace(ScriptPath))
            {
                _basic_settings_state.Clear();
                _custom_settings_state.Clear();
            }

            this.treeCustomSettings.BeginUpdate();
            this.treeCustomSettings.Nodes.Clear();

            var values = new Dictionary<string, bool>();

            // Store temporary for easier lookup of parent nodes
            var flat = new Dictionary<string, TreeNode>();

            foreach (ASLSetting setting in settings.OrderedSettings)
            {
                var value = setting.Value;
                if (_custom_settings_state.ContainsKey(setting.Id))
                    value = _custom_settings_state[setting.Id];

                var node = new TreeNode(setting.Label) {
                    Tag = setting,
                    Checked = value,
                    ContextMenuStrip = this.treeContextMenu2,
                    ToolTipText = setting.ToolTip
                };
                setting.Value = value;

                if (setting.Parent == null)
                {
                    this.treeCustomSettings.Nodes.Add(node);
                }
                else if (flat.ContainsKey(setting.Parent))
                {
                    flat[setting.Parent].Nodes.Add(node);
                    flat[setting.Parent].ContextMenuStrip = this.treeContextMenu;
                }

                flat.Add(setting.Id, node);
                values.Add(setting.Id, value);
            }

            // Gray out deactivated nodes after all have been added
            foreach (var item in flat) {
                if (!item.Value.Checked)
                {
                    UpdateGrayedOut(item.Value);
                }
            }

            // Only if a script was actually loaded, update current state with current ASL settings
            // (which may be empty if the successfully loaded script has no settings, but shouldn't
            // be empty because the script failed to load, which can happen frequently when working
            // on ASL scripts)
            if (script_loaded)
                _custom_settings_state = values;

            treeCustomSettings.ExpandAll();
            treeCustomSettings.EndUpdate();

            // Scroll up to the top
            if (this.treeCustomSettings.Nodes.Count > 0)
                this.treeCustomSettings.Nodes[0].EnsureVisible();

            UpdateCustomSettingsVisibility();
            InitBasicSettings(settings);
        }


        private void AppendBasicSettingsToXml(XmlDocument document, XmlNode settings_node)
        {
            foreach (var item in _basic_settings)
            {
                if (_basic_settings_state.ContainsKey(item.Key.ToLower()))
                {
                    var value = _basic_settings_state[item.Key.ToLower()];
                    settings_node.AppendChild(SettingsHelper.ToElement(document, item.Key, value));
                }
            }
        }

        private void AppendCustomSettingsToXml(XmlDocument document, XmlNode parent)
        {
            XmlElement asl_parent = document.CreateElement("CustomSettings");

            foreach (var setting in _custom_settings_state)
            {
                XmlElement element = SettingsHelper.ToElement(document, "Setting", setting.Value);
                XmlAttribute id = SettingsHelper.ToAttribute(document, "id", setting.Key);
                // In case there are other setting types in the future
                XmlAttribute type = SettingsHelper.ToAttribute(document, "type", "bool");

                element.Attributes.Append(id);
                element.Attributes.Append(type);
                asl_parent.AppendChild(element);
            }

            parent.AppendChild(asl_parent);
        }

        private void ParseBasicSettingsFromXml(XmlElement element)
        {
            foreach (var item in _basic_settings)
            {
                if (element[item.Key] != null)
                {
                    var value = bool.Parse(element[item.Key].InnerText);

                    // If component is not enabled, don't check setting
                    if (item.Value.Enabled)
                        item.Value.Checked = value;

                    _basic_settings_state[item.Key.ToLower()] = value;
                }
            }
        }

        /// <summary>
        /// Parses custom settings, stores them and updates the checked state of already added tree nodes.
        /// </summary>
        /// 
        private void ParseCustomSettingsFromXml(XmlElement data)
        {
            XmlElement custom_settings_node = data["CustomSettings"];

            if (custom_settings_node != null && custom_settings_node.HasChildNodes)
            {
                foreach (XmlElement element in custom_settings_node.ChildNodes)
                {
                    if (element.Name != "Setting")
                        continue;

                    string id = element.Attributes["id"].Value;
                    string type = element.Attributes["type"].Value;

                    if (id != null && type == "bool")
                    {
                        bool value = SettingsHelper.ParseBool(element);
                        _custom_settings_state[id] = value;
                    }
                }
            }

            // Update tree with loaded state (in case the tree is already populated)
            UpdateNodesCheckedState(_custom_settings_state);
        }

        private void InitBasicSettings(ASLSettings settings)
        {
            foreach (var item in _basic_settings)
            {
                string name = item.Key.ToLower();
                CheckBox checkbox = item.Value;

                if (settings.IsBasicSettingPresent(name))
                {
                    ASLSetting setting = settings.BasicSettings[name];
                    checkbox.Enabled = true;
                    checkbox.Tag = setting;
                    var value = setting.Value;

                    if (_basic_settings_state.ContainsKey(name))
                        value = _basic_settings_state[name];

                    checkbox.Checked = value;
                    setting.Value = value;
                }
                else
                {
                    checkbox.Tag = null;
                    checkbox.Enabled = false;
                    checkbox.Checked = false;
                }
            }
        }

        private void UpdateCustomSettingsVisibility()
        {
            bool show = this.treeCustomSettings.GetNodeCount(false) > 0;
            this.treeCustomSettings.Visible = show;
            this.btnResetToDefault.Visible = show;
            this.btnCheckAll.Visible = show;
            this.btnUncheckAll.Visible = show;
            this.labelCustomSettings.Visible = show;
        }

        /// <summary>
        /// Generic update on all given nodes and their childnodes, ignoring childnodes for
        /// nodes where the Func returns false.
        /// </summary>
        /// 
        private void UpdateNodesInTree(Func<TreeNode, bool> func, TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                bool include_child_nodes = func(node);
                if (include_child_nodes)
                    UpdateNodesInTree(func, node.Nodes);
            }
        }

        /// <summary>
        /// Update the checked state of all given nodes and their childnodes based on the return
        /// value of the given Func.
        /// </summary>
        /// <param name="nodes">If nodes is null, all nodes of the custom settings tree are affected.</param>
        /// 
        private void UpdateNodesCheckedState(Func<ASLSetting, bool> func, TreeNodeCollection nodes = null)
        {
            if (nodes == null)
                nodes = this.treeCustomSettings.Nodes;

            UpdateNodesInTree(node => {
                var setting = (ASLSetting)node.Tag;
                bool check = func(setting);

                if (node.Checked != check)
                    node.Checked = check;

                return true;
            }, nodes);
        }

        /// <summary>
        /// Update the checked state of all given nodes and their childnodes
        /// based on a dictionary of setting values.
        /// </summary>
        /// 
        private void UpdateNodesCheckedState(Dictionary<string, bool> setting_values, TreeNodeCollection nodes = null)
        {
            if (setting_values == null)
                return;

            UpdateNodesCheckedState(setting => {
                string id = setting.Id;

                if (setting_values.ContainsKey(id))
                    return setting_values[id];

                return setting.Value;
            }, nodes);
        }

        private void UpdateNodeCheckedState(Func<ASLSetting, bool> func, TreeNode node)
        {
            var setting = (ASLSetting)node.Tag;
            bool check = func(setting);

            if (node.Checked != check)
                node.Checked = check;
        }

        /// <summary>
        /// If the given node is unchecked, grays out all childnodes.
        /// </summary>
        private void UpdateGrayedOut(TreeNode node)
        {
            // Only change color of childnodes if this node isn't already grayed out
            if (node.ForeColor != SystemColors.GrayText)
            {
                UpdateNodesInTree(n => {
                    n.ForeColor = node.Checked ? SystemColors.WindowText : SystemColors.GrayText;
                    return n.Checked || !node.Checked;
                }, node.Nodes);
            }
        }


        // Events

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog()
            {
                Filter = "Auto Split Script (*.asl)|*.asl|All Files (*.*)|*.*"
            };
            if (File.Exists(ScriptPath))
            {
                dialog.InitialDirectory = Path.GetDirectoryName(ScriptPath);
                dialog.FileName = Path.GetFileName(ScriptPath);
            }

            if (dialog.ShowDialog() == DialogResult.OK)
                ScriptPath = this.txtScriptPath.Text = dialog.FileName;
        }

        // Basic Setting checked/unchecked
        //
        // This detects both changes made by the user and by the program, so this should
        // change the state in _basic_settings_state fine as well.
        private void methodCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            var checkbox = (CheckBox)sender;
            var setting = (ASLSetting)checkbox.Tag;

            if (setting != null)
            {
                setting.Value = checkbox.Checked;
                _basic_settings_state[setting.Id] = setting.Value;
            }
        }

        // Custom Setting checked/unchecked (only after initially building the tree)
        private void settingsTree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            // Update value in the ASLSetting object, which also changes it in the ASL script
            ASLSetting setting = (ASLSetting)e.Node.Tag;
            setting.Value = e.Node.Checked;
            _custom_settings_state[setting.Id] = setting.Value;

            UpdateGrayedOut(e.Node);
            ObjectChecked = true;

        }

        private void settingsTree_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = e.Node.ForeColor == SystemColors.GrayText;
        }

        // Custom Settings Button Events

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            UpdateNodesCheckedState(s => true);
        }

        private void btnUncheckAll_Click(object sender, EventArgs e)
        {
            UpdateNodesCheckedState(s => false);
        }

        private void btnResetToDefault_Click(object sender, EventArgs e)
        {
            UpdateNodesCheckedState(s => s.DefaultValue);
        }


        // Custom Settings Context Menu Events

        private void settingsTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // Select clicked node (not only with left-click) for use with context menu
            this.treeCustomSettings.SelectedNode = e.Node;
        }

        private void cmiCheckBranch_Click(object sender, EventArgs e)
        {
            UpdateNodesCheckedState(s => true, this.treeCustomSettings.SelectedNode.Nodes);
            UpdateNodeCheckedState(s => true, this.treeCustomSettings.SelectedNode);
        }

        private void cmiUncheckBranch_Click(object sender, EventArgs e)
        {
            UpdateNodesCheckedState(s => false, this.treeCustomSettings.SelectedNode.Nodes);
            UpdateNodeCheckedState(s => false, this.treeCustomSettings.SelectedNode);
        }

        private void cmiResetBranchToDefault_Click(object sender, EventArgs e)
        {
            UpdateNodesCheckedState(s => s.DefaultValue, this.treeCustomSettings.SelectedNode.Nodes);
            UpdateNodeCheckedState(s => s.DefaultValue, this.treeCustomSettings.SelectedNode);
        }

        private void cmiExpandBranch_Click(object sender, EventArgs e)
        {
            this.treeCustomSettings.SelectedNode.ExpandAll();
            this.treeCustomSettings.SelectedNode.EnsureVisible();
        }

        private void cmiCollapseBranch_Click(object sender, EventArgs e)
        {
            this.treeCustomSettings.SelectedNode.Collapse();
            this.treeCustomSettings.SelectedNode.EnsureVisible();
        }

        private void cmiCollapseTreeToSelection_Click(object sender, EventArgs e)
        {
            TreeNode selected = this.treeCustomSettings.SelectedNode;
            this.treeCustomSettings.CollapseAll();
            this.treeCustomSettings.SelectedNode = selected;
            selected.EnsureVisible();
        }

        private void cmiExpandTree_Click(object sender, EventArgs e)
        {
            this.treeCustomSettings.ExpandAll();
            this.treeCustomSettings.SelectedNode.EnsureVisible();
        }

        private void cmiCollapseTree_Click(object sender, EventArgs e)
        {
            this.treeCustomSettings.CollapseAll();
        }

        private void cmiResetSettingToDefault_Click(object sender, EventArgs e)
        {
            UpdateNodeCheckedState(s => s.DefaultValue, this.treeCustomSettings.SelectedNode);
        }
    }

    /// <summary>
    /// TreeView with fixed double-clicking on checkboxes.
    /// </summary>
    /// 
    /// See also:
    /// http://stackoverflow.com/questions/17356976/treeview-with-checkboxes-not-processing-clicks-correctly
    /// http://stackoverflow.com/questions/14647216/c-sharp-treeview-ignore-double-click-only-at-checkbox
    class NewTreeView : TreeView
    {
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x203) // identified double click
            {
                var local_pos = PointToClient(Cursor.Position);
                var hit_test_info = HitTest(local_pos);

                if (hit_test_info.Location == TreeViewHitTestLocations.StateImage)
                {
                    m.Msg = 0x201; // if checkbox was clicked, turn into single click
                }

                base.WndProc(ref m);
            }
            else
            {
                base.WndProc(ref m);
            }
        }
    }
}
