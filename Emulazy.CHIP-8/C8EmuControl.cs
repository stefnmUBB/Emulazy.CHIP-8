using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace Emulazy.C8
{
    public partial class C8EmuControl : UserControl
    {
        public C8Interpreter Chip8 { get; set; }
        public C8EmuControl()
        {
            InitializeComponent();

            var palette = Canvas.Palette;
            palette.Entries[0] = Color.FromArgb(50, 0, 0, 0);
            palette.Entries[1] = Color.WhiteSmoke;
            Canvas.Palette = palette;

            Frame.Image = Canvas;
        }        

        public void StartEmulation()
        {
            Task.Run(Loop);
        }

        public Bitmap Canvas { get; private set; } = new Bitmap(64, 32, PixelFormat.Format8bppIndexed);
        public void DefaultDraw(bool[] GFX)
        {
            var bmpData = Canvas.LockBits(
                new Rectangle(0, 0, Canvas.Width, Canvas.Height), 
                ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            int stride = bmpData.Stride < 0 ? -bmpData.Stride : bmpData.Stride;

            unsafe
            {
                byte* data = (byte*)bmpData.Scan0;
                for(int i=0;i<32*64;i++)
                {
                    data[i] = (byte) (GFX[i] ? 1 : 0);
                }
            }

            Canvas.UnlockBits(bmpData);
            
            Frame.Refresh();
            
            FrameUpdate?.Invoke(this, new EventArgs());
        }

        // For timing...
        private int _FPS = 60;
        public int FPS
        {
            get => _FPS;
            set
            {
                _FPS = value;
                targetElapsedTimeSet = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / value);
            }
        }
        readonly Stopwatch stopWatch = Stopwatch.StartNew();        
        TimeSpan targetElapsedTimeSet = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / 60);
        readonly TimeSpan targetElapsedTime = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / 1000);
        TimeSpan lastTime;

        public bool Paused
        {
            get => _Paused;
            set
            {
                bool wasPaused = (_Paused == true);
                _Paused = value;
                _ResumeFlag = (wasPaused && !_Paused);
                int x = 10;
            }
        } 
        bool _Paused = false;       
        bool _ResumeFlag = false; //activates after gaining focus
        
        bool _Running = false;
        public bool Running
        {
            get => _Running;
            set => _Running = value;                        
        }
        Task Loop()
        {            
            int pc = 0;
            while (_Running) 
            {
                if (_Paused) continue;               
                var currentTime = stopWatch.Elapsed;
                var elapsedTime = currentTime - lastTime;

                if (_ResumeFlag)
                {
                    elapsedTime = targetElapsedTimeSet;
                    _ResumeFlag = false;
                }
                

                while (elapsedTime >= targetElapsedTimeSet)
                {
                    Invoke((Action)delegate()
                    {
                        Chip8.Update();                                                 
                    });
                    elapsedTime -= targetElapsedTimeSet;
                    lastTime += targetElapsedTimeSet;                    
                }
                
                Invoke((Action)delegate()
                {
                    for (int i = 0; i < 16; i++)
                    {
                        Chip8.EmulateCycle();
                        if (pc != Chip8.PC)
                        {
                            pc = Chip8.PC;
                            ProgramCounterChanged?.Invoke(this, new EventArgs());
                        }
                    }
                });
                Thread.Sleep(targetElapsedTime);                                
            }

            return new Task(new Action(delegate () { }));
        }     

        public void TriggerKeyDown(KeyEventArgs e)
        {
            if (KeyMap.ContainsKey(e.KeyCode))
                Chip8.KeyDown(KeyMap[e.KeyCode]);
        }

        public void TriggerKeyUp(KeyEventArgs e)
        {
            if (KeyMap.ContainsKey(e.KeyCode))
                Chip8.KeyUp(KeyMap[e.KeyCode]);
        }               

        Dictionary<Keys, byte> KeyMap = new Dictionary<Keys, byte>()
        {
            { Keys.D1, 0x1 }, { Keys.D2, 0x2 }, { Keys.D3, 0x3 }, { Keys.D4, 0xC },
            { Keys.Q,  0x4 }, { Keys.W,  0x5 }, { Keys.E,  0x6 }, { Keys.R,  0xD },
            { Keys.A,  0x7 }, { Keys.S,  0x8 }, { Keys.D,  0x9 }, { Keys.F,  0xE },
            { Keys.Z,  0xA }, { Keys.X,  0x0 }, { Keys.C,  0xB }, { Keys.V,  0xF }
        };

        public delegate void OnProgramCounterChanged(object sender, EventArgs e);
        public event OnProgramCounterChanged ProgramCounterChanged;

        public delegate void OnFrameUpdate(object sender, EventArgs e);
        public event OnFrameUpdate FrameUpdate;     

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;    // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        private void Frame_Click(object sender, EventArgs e)
        {
            Focus();            
            OnClick(e);
        }            

        public void ResetEmulator()
        {
            _Running = false;
            Chip8.Initialize();
            Chip8.Draw(BootScreen.GFX);
        }        
    }     
}
