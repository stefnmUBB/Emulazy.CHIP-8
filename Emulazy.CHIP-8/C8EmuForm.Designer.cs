namespace Emulazy.C8
{
    partial class C8EmuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(C8EmuForm));
            this.OFD = new System.Windows.Forms.OpenFileDialog();
            this.DebugContainer = new System.Windows.Forms.SplitContainer();
            this.Body = new System.Windows.Forms.SplitContainer();
            this.MachineContainer = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SettingsGroup = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.FPSInput = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.ControlPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.OpenFileButton = new System.Windows.Forms.Button();
            this.ContextLoadROM = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.LoadFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BbuiltInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadTetrisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PlayButton = new System.Windows.Forms.Button();
            this.DebugButton = new System.Windows.Forms.Button();
            this.ConfigButton = new System.Windows.Forms.Button();
            this.Emulator = new Emulazy.C8.C8EmuControl();
            this.InstructionsListView = new Emulazy.C8.C8InstructionsListView();
            this.LoadTictacToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.DebugContainer)).BeginInit();
            this.DebugContainer.Panel1.SuspendLayout();
            this.DebugContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Body)).BeginInit();
            this.Body.Panel1.SuspendLayout();
            this.Body.Panel2.SuspendLayout();
            this.Body.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MachineContainer)).BeginInit();
            this.MachineContainer.Panel1.SuspendLayout();
            this.MachineContainer.Panel2.SuspendLayout();
            this.MachineContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SettingsGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPSInput)).BeginInit();
            this.MainPanel.SuspendLayout();
            this.ControlPanel.SuspendLayout();
            this.ContextLoadROM.SuspendLayout();
            this.SuspendLayout();
            // 
            // DebugContainer
            // 
            resources.ApplyResources(this.DebugContainer, "DebugContainer");
            this.DebugContainer.Name = "DebugContainer";
            // 
            // DebugContainer.Panel1
            // 
            this.DebugContainer.Panel1.Controls.Add(this.InstructionsListView);
            // 
            // DebugContainer.Panel2
            // 
            this.DebugContainer.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            // 
            // Body
            // 
            resources.ApplyResources(this.Body, "Body");
            this.Body.Name = "Body";
            // 
            // Body.Panel1
            // 
            this.Body.Panel1.Controls.Add(this.MachineContainer);
            // 
            // Body.Panel2
            // 
            this.Body.Panel2.Controls.Add(this.DebugContainer);
            // 
            // MachineContainer
            // 
            resources.ApplyResources(this.MachineContainer, "MachineContainer");
            this.MachineContainer.Name = "MachineContainer";
            // 
            // MachineContainer.Panel1
            // 
            this.MachineContainer.Panel1.Controls.Add(this.Emulator);
            // 
            // MachineContainer.Panel2
            // 
            this.MachineContainer.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.MachineContainer.Panel2.Controls.Add(this.panel1);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.SettingsGroup);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // SettingsGroup
            // 
            this.SettingsGroup.Controls.Add(this.panel2);
            this.SettingsGroup.Controls.Add(this.FPSInput);
            this.SettingsGroup.Controls.Add(this.label1);
            resources.ApplyResources(this.SettingsGroup, "SettingsGroup");
            this.SettingsGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.SettingsGroup.Name = "SettingsGroup";
            this.SettingsGroup.TabStop = false;
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // FPSInput
            // 
            this.FPSInput.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.FPSInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FPSInput.ForeColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.FPSInput, "FPSInput");
            this.FPSInput.Name = "FPSInput";
            this.FPSInput.TabStop = false;
            this.FPSInput.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.FPSInput.ValueChanged += new System.EventHandler(this.FPSInput_ValueChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.label1.Name = "label1";
            // 
            // MainPanel
            // 
            this.MainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MainPanel.Controls.Add(this.Body);
            resources.ApplyResources(this.MainPanel, "MainPanel");
            this.MainPanel.Name = "MainPanel";
            // 
            // ControlPanel
            // 
            this.ControlPanel.Controls.Add(this.OpenFileButton);
            this.ControlPanel.Controls.Add(this.PlayButton);
            this.ControlPanel.Controls.Add(this.DebugButton);
            this.ControlPanel.Controls.Add(this.ConfigButton);
            resources.ApplyResources(this.ControlPanel, "ControlPanel");
            this.ControlPanel.Name = "ControlPanel";
            // 
            // OpenFileButton
            // 
            this.OpenFileButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.OpenFileButton.ContextMenuStrip = this.ContextLoadROM;
            this.OpenFileButton.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.OpenFileButton, "OpenFileButton");
            this.OpenFileButton.ForeColor = System.Drawing.Color.Yellow;
            this.OpenFileButton.Name = "OpenFileButton";
            this.OpenFileButton.UseVisualStyleBackColor = false;
            this.OpenFileButton.Click += new System.EventHandler(this.OpenFileButton_Click);
            // 
            // ContextLoadROM
            // 
            this.ContextLoadROM.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoadFromFileToolStripMenuItem,
            this.BbuiltInToolStripMenuItem});
            this.ContextLoadROM.Name = "ContextLoadROM";
            resources.ApplyResources(this.ContextLoadROM, "ContextLoadROM");
            // 
            // loadFromFileToolStripMenuItem
            // 
            this.LoadFromFileToolStripMenuItem.Name = "loadFromFileToolStripMenuItem";
            resources.ApplyResources(this.LoadFromFileToolStripMenuItem, "loadFromFileToolStripMenuItem");
            this.LoadFromFileToolStripMenuItem.Click += new System.EventHandler(this.loadFromFileToolStripMenuItem_Click);
            // 
            // builtInToolStripMenuItem
            // 
            this.BbuiltInToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoadTetrisToolStripMenuItem,
            this.LoadTictacToolStripMenuItem});
            this.BbuiltInToolStripMenuItem.Name = "builtInToolStripMenuItem";
            resources.ApplyResources(this.BbuiltInToolStripMenuItem, "builtInToolStripMenuItem");
            // 
            // tetrisToolStripMenuItem
            // 
            this.LoadTetrisToolStripMenuItem.Name = "tetrisToolStripMenuItem";
            resources.ApplyResources(this.LoadTetrisToolStripMenuItem, "tetrisToolStripMenuItem");
            this.LoadTetrisToolStripMenuItem.Click += new System.EventHandler(this.tetrisToolStripMenuItem_Click);
            // 
            // PlayButton
            // 
            this.PlayButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.PlayButton.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.PlayButton, "PlayButton");
            this.PlayButton.ForeColor = System.Drawing.Color.Lime;
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.UseVisualStyleBackColor = false;
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // DebugButton
            // 
            this.DebugButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.DebugButton.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.DebugButton, "DebugButton");
            this.DebugButton.ForeColor = System.Drawing.Color.Lime;
            this.DebugButton.Name = "DebugButton";
            this.DebugButton.UseVisualStyleBackColor = false;
            this.DebugButton.Click += new System.EventHandler(this.DebugButton_Click);
            // 
            // ConfigButton
            // 
            this.ConfigButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ConfigButton.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.ConfigButton, "ConfigButton");
            this.ConfigButton.ForeColor = System.Drawing.Color.White;
            this.ConfigButton.Name = "ConfigButton";
            this.ConfigButton.UseVisualStyleBackColor = false;
            this.ConfigButton.Click += new System.EventHandler(this.ConfigButton_Click);
            // 
            // EmuScreen
            // 
            this.Emulator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.Emulator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Emulator.Chip8 = null;
            resources.ApplyResources(this.Emulator, "EmuScreen");
            this.Emulator.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Emulator.FPS = 60;
            this.Emulator.Name = "EmuScreen";
            this.Emulator.Paused = false;
            this.Emulator.Running = false;
            this.Emulator.ProgramCounterChanged += new Emulazy.C8.C8EmuControl.OnProgramCounterChanged(this.EmuScreen_ProgramCounterChanged);
            // 
            // InstructionsListView
            // 
            this.InstructionsListView.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.InstructionsListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InstructionsListView.CurrentItem = -1;
            resources.ApplyResources(this.InstructionsListView, "InstructionsListView");
            this.InstructionsListView.Name = "InstructionsListView";
            // 
            // tictacToolStripMenuItem
            // 
            this.LoadTictacToolStripMenuItem.Name = "tictacToolStripMenuItem";
            resources.ApplyResources(this.LoadTictacToolStripMenuItem, "tictacToolStripMenuItem");
            this.LoadTictacToolStripMenuItem.Click += new System.EventHandler(this.tictacToolStripMenuItem_Click);
            // 
            // C8EmuForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.ControlPanel);
            this.KeyPreview = true;
            this.Name = "C8EmuForm";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.C8EmuForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.C8EmuForm_KeyUp);
            this.DebugContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DebugContainer)).EndInit();
            this.DebugContainer.ResumeLayout(false);
            this.Body.Panel1.ResumeLayout(false);
            this.Body.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Body)).EndInit();
            this.Body.ResumeLayout(false);
            this.MachineContainer.Panel1.ResumeLayout(false);
            this.MachineContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MachineContainer)).EndInit();
            this.MachineContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.SettingsGroup.ResumeLayout(false);
            this.SettingsGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPSInput)).EndInit();
            this.MainPanel.ResumeLayout(false);
            this.ControlPanel.ResumeLayout(false);
            this.ContextLoadROM.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public C8EmuControl Emulator;
        private System.Windows.Forms.OpenFileDialog OFD;
        private System.Windows.Forms.SplitContainer MachineContainer;
        private C8InstructionsListView InstructionsListView;
        private System.Windows.Forms.SplitContainer DebugContainer;
        private System.Windows.Forms.SplitContainer Body;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox SettingsGroup;
        private System.Windows.Forms.NumericUpDown FPSInput;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.FlowLayoutPanel ControlPanel;
        private System.Windows.Forms.Button PlayButton;
        private System.Windows.Forms.Button OpenFileButton;
        private System.Windows.Forms.Button DebugButton;
        private System.Windows.Forms.Button ConfigButton;
        private System.Windows.Forms.ContextMenuStrip ContextLoadROM;
        private System.Windows.Forms.ToolStripMenuItem LoadFromFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BbuiltInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LoadTetrisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LoadTictacToolStripMenuItem;
    }
}