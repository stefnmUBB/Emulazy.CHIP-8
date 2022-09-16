using Emulazy.C8.BuiltInROMs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emulazy.C8
{
    public partial class C8EmuForm : Form
    {
        bool DebugEnabled = true;
        public List<C8OpCodeData> OpCodes = new List<C8OpCodeData>();
        public C8Interpreter Chip8 = new C8Interpreter();
        public C8EmuForm()
        {
            InitializeComponent();
            Emulator.Chip8 = Chip8;
            ApplyButtonState(PlayButton, ButtonStates.Play);
            Body.Panel2Collapsed = true;
            DebugEnabled = false;
            ApplyButtonState(DebugButton, ButtonStates.DebuggerOff);

            MachineContainer.Panel2Collapsed = true;
            ApplyButtonState(ConfigButton, ButtonStates.ConfigOff);
        }

        protected override void OnLoad(EventArgs e)
        {
            Chip8.Draw = Emulator.DefaultDraw;
            base.OnLoad(e);
        }

        private void C8EmuForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.P) PlayButton_Click(null, null);
            Emulator.TriggerKeyDown(e);
        }

        private void C8EmuForm_KeyUp(object sender, KeyEventArgs e)
        {
            Emulator.TriggerKeyUp(e);
        }

        private void EmuScreen_ProgramCounterChanged(object sender, EventArgs e)
        {
            if (!DebugEnabled) return;
            InstructionsListView.CurrentItem = (Chip8.PC - 0x200) / 2;
        }

        private void FPSInput_ValueChanged(object sender, EventArgs e)
        {
            Emulator.FPS = (int)FPSInput.Value;
        }

        void ApplyButtonState(Button button, ButtonStates state)
        {
            button.Text = state.Text;
            button.ForeColor = state.Color;
        }
        bool ROMloaded = false;
        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            if (OFD.ShowDialog() == DialogResult.OK)
            {
                var filename = OFD.FileName;
                try
                {
                    Emulator.ResetEmulator();
                    OpCodes = Chip8.LoadROM(filename);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    ROMloaded = false;
                    return;
                }
                InstructionsListView.Items = OpCodes;
                InstructionsListView.Invalidate();
                Text = $"Emulazy | CHIP-8 ({Path.GetFileName(OFD.FileName)})";
                ApplyButtonState(PlayButton, ButtonStates.Play);
                ROMloaded = true;
            }
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            if (!Emulator.Running)
            {
                if (!ROMloaded) return;
                Emulator.Running = true;
                Emulator.StartEmulation();
                ApplyButtonState(PlayButton, ButtonStates.Pause);
            }
            else
            {
                if (!Emulator.Paused)
                {
                    Emulator.Paused = true;
                    ApplyButtonState(PlayButton, ButtonStates.Resume);
                }
                else
                {
                    Emulator.Paused = false;
                    ApplyButtonState(PlayButton, ButtonStates.Pause);
                }
            }
        }

        private void ConfigButton_Click(object sender, EventArgs e)
        {
            if (MachineContainer.Panel2Collapsed)
            {
                MachineContainer.Panel2Collapsed = false;
                ApplyButtonState(ConfigButton, ButtonStates.ConfigOn);
            }
            else
            {
                MachineContainer.Panel2Collapsed = true;
                ApplyButtonState(ConfigButton, ButtonStates.ConfigOff);
            }
        }

        private void DebugButton_Click(object sender, EventArgs e)
        {
            if (Body.Panel2Collapsed)
            {
                Body.Panel2Collapsed = false;
                DebugEnabled = true;
                ApplyButtonState(DebugButton, ButtonStates.DebuggerOn);
            }
            else
            {
                Body.Panel2Collapsed = true;
                DebugEnabled = false;
                ApplyButtonState(DebugButton, ButtonStates.DebuggerOff);
            }
        }

        private void loadFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileButton_Click(null, null);
        }

        void LoadBuiltInROM(IBuiltInROM ROM)
        {
            Emulator.ResetEmulator();
            Emulator.FPS = ROM.FPS;
            OpCodes = Emulator.Chip8.LoadROM(ROM.Bytes);
            InstructionsListView.Items = OpCodes;
            InstructionsListView.Invalidate();
            Text = $"Emulazy | CHIP-8 ({ROM.GetType().Name})";
            ApplyButtonState(PlayButton, ButtonStates.Play);
            ROMloaded = true;
        }

        private void tetrisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadBuiltInROM(new Tetris());
        }

        private void tictacToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadBuiltInROM(new Tictac());
        }       
    }

    internal class ButtonStates
    {
        public string Text;
        public Color Color;
        internal ButtonStates(string text, Color color)
        {
            Text = text;
            Color = color;
            
        }

        internal static ButtonStates Play = new ButtonStates("▶", Color.FromArgb(0, 255, 0));
        internal static ButtonStates Resume = new ButtonStates("▶", Color.FromArgb(255, 255, 0));
        internal static ButtonStates Pause = new ButtonStates("❚❚", Color.FromArgb(0, 255, 255));
        internal static ButtonStates DebuggerOff = new ButtonStates("🐞", Color.White);
        internal static ButtonStates DebuggerOn = new ButtonStates("🐞", Color.LimeGreen);
        internal static ButtonStates ConfigOff = new ButtonStates("⚙", Color.White);
        internal static ButtonStates ConfigOn = new ButtonStates("⚙", Color.CornflowerBlue);


    }
}
