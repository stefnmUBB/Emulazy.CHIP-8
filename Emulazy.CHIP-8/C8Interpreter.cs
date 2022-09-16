using System;
using System.Collections.Generic;
using System.IO;

namespace Emulazy.C8
{
    public class C8Interpreter
    {
        public C8Configuration Configuration = new C8Configuration();
        public ushort OpCode;
        /// <summary>
        /// System's Memory map
        /// 0x000 - 0x1FF  : C8 interpreter (font set in emu)
        /// 0x050 - 0x0A0  : built-in 4x5 pixel font set (0-F)
        /// 0x200 - 0xFFF  : program ROM and work RAM
        /// </summary>
        public byte[] Memory = new byte[4096];
        public byte[] V = new byte[16]; //CPU registers
        public ushort I;  //Index register
        public ushort PC; //Program counter

        bool[] GFX = new bool[64 * 32];

        public byte DelayTimer;
        public byte SoundTimer;

        ushort[] Stack = new ushort[16];
        public ushort SP; //Stack Pointer
        byte[] Key = new byte[16];

        public void ClearDisplay()
        {
            for (int i = 0; i < 2048; i++) GFX[i] = false;
        }
        
        public void Initialize()
        {
            // Initialize registers and memory once
            PC = 0x200;  // Program counter starts at 0x200
            OpCode = 0;      // Reset current opcode	
            I = 0;      // Reset index register
            SP = 0;      // Reset stack pointer

            // Clear display	
            ClearDisplay();
            // Clear stack
            for (int i = 0; i < 16; i++) Stack[i] = 0;
            // Clear registers V0-VF
            for (int i = 0; i < 16; i++) Key[i] = V[i] = 0;
            // Clear memory
            for (int i = 0; i < 4096; i++) Memory[i] = 0;

            // Load fontset
            for (int i = 0; i < 80; ++i)
                Memory[i] = C8FontSet[i];

            // Reset timers
            DelayTimer = 0;
            SoundTimer = 0;

            // Cleaer Screen Once
            drawflag = true;

            //Random init
            rand = new Random();
        }

        Random rand = new Random();

        /*public void LoadROM(string filename)
        {
            byte[] buffer = File.ReadAllBytes(filename);
            int bufferSize = buffer.Length;
            if (4096 - 512 > bufferSize)
            {
                for (int i = 0; i < bufferSize; i++)
                    Memory[i + 512] = buffer[i];
            }
            else Console.WriteLine("ROM too big");
        }*/

        public List<C8OpCodeData> LoadROM(byte[] buffer)
        {
            List<C8OpCodeData> result = new List<C8OpCodeData>();
            if (buffer == null) return result;
            int bufferSize = buffer.Length;
            if (4096 - 512 > bufferSize)
            {
                ushort opcode = 0;
                for (int i = 0; i < bufferSize; i++)
                {
                    if (i > 0 && i % 2 == 0)
                    {
                        result.Add(new C8OpCodeData(opcode));
                        opcode = 0;
                    }
                    opcode = (ushort)(opcode * 256 + buffer[i]);
                    Memory[i + 512] = buffer[i];
                }
            }
            else
            {
                throw new ArgumentException("ROM is too big.");
            }
            return result;
        }

        public List<C8OpCodeData> LoadROM(string filename)
        {
            List<C8OpCodeData> result = new List<C8OpCodeData>();
            byte[] buffer;
            try
            {
                 buffer = File.ReadAllBytes(filename);
            }
            catch(Exception e)
            {
                throw new ArgumentException("Failed to open the specified ROM.\n\n" + e.Message);
            }

            int bufferSize = buffer.Length;
            if (4096 - 512 > bufferSize)
            {
                ushort opcode = 0;
                for (int i = 0; i < bufferSize; i++)
                {                    
                    if (i > 0 && i % 2 == 0)
                    {
                        result.Add(new C8OpCodeData(opcode));                        
                        opcode = 0;
                    }
                    opcode = (ushort)(opcode * 256 + buffer[i]);
                    Memory[i + 512] = buffer[i];           
                }
            }
            else
            {
                throw new ArgumentException("ROM is too big.");                
            }
            return result;
        }        

        public ushort FetchOpCode(int pc)
        {
            return (ushort)(((Memory[pc]) << 8) | Memory[pc + 1]);
        }

        public void EmulateCycle()
        {            
            // Fetch OpCode                        
            OpCode = (ushort)(((Memory[PC]) << 8) | Memory[PC + 1]);                       
            // Decode OpCode                        
                switch (OpCode & 0xF000)
                {
                    case 0x0000:
                        {
                            switch (OpCode)
                            {
                                case 0x00E0:
                                    {
                                        ClearDisplay();
                                        //drawflag = true;
                                        PC += 2;
                                        break;
                                    }
                                case 0x00EE:
                                    {                                        
                                        PC = Stack[--SP];
                                        PC += 2;
                                        break;
                                    }
                                default:
                                    {
                                        /*SP++;
                                        Memory[SP] = (byte)PC;
                                        int NNN = OpCode & 0x0FFF;
                                        PC = (ushort)NNN;*/
                                        Console.WriteLine("Unknown opcode 0x{0:X4}", OpCode);
                                        break;
                                    }
                            }
                            break;
                        }
                    case 0x1000: // 0x1NNN : goto NNN
                        {
                            PC = (ushort)(OpCode & 0x0FFF);
                            break;
                        }
                    case 0x2000: // 0x2NNN : calls subroutine NNN
                        {
                            Stack[SP++] = PC;                            
                            PC = (ushort)(OpCode & 0x0FFF);
                            break;
                        }

                    case 0x3000: // 0x3XNN : skips next instruction if VX==NN
                        {
                            int X = (OpCode & 0x0F00) >> 8;
                            int NN = (OpCode & 0x00FF);
                            if (V[X] == NN) PC += 2;
                            PC += 2;
                            break;
                        }
                    case 0x4000: // 0x4XNN : skips next instruction if VX!=NN
                        {
                            int X = (OpCode & 0x0F00) >> 8;
                            int NN = (OpCode & 0x00FF);
                            if (V[X] != NN) PC += 2;
                            PC += 2;
                            break;
                        }
                    case 0x5000: // 0x5XY0 : skips next instruction if VX==VY
                        {
                            int X = (OpCode & 0x0F00) >> 8;
                            int Y = (OpCode & 0x00F0) >> 4;
                            if (V[X] == V[Y]) PC += 2;
                            PC += 2;
                            break;
                        }
                    case 0x6000: // 0x6XNN : VX=NN
                        {
                            int X = (OpCode & 0x0F00) >> 8;
                            byte NN = (byte)(OpCode & 0x00FF);
                            V[X] = NN;
                            PC += 2;
                            break;
                        }
                    case 0x7000: // 0x7XNN : VX+=NN (carry flag is not changed)
                        {
                            int X = (OpCode & 0x0F00) >> 8;
                            byte NN = (byte)(OpCode & 0x00FF);
                            V[X] += NN;
                            PC += 2;
                            break;
                        }

                    case 0x8000:
                        {
                            int Y = (OpCode & 0x00F0) >> 4;
                            int X = (OpCode & 0x0F00) >> 8;
                            switch (OpCode & 0x000F)
                            {
                                case 0x0000: // 0x8XY0 Vx=VY
                                    {
                                        V[X] = V[Y];
                                        PC += 2;
                                        break;
                                    }
                                case 0x0001: // 0x8XY1 VX = VX | VY
                                    {
                                        V[X] |= V[Y];
                                        PC += 2;
                                        break;
                                    }
                                case 0x0002: // 0x8XY2 VX = VX & VY
                                    {
                                        V[X] &= V[Y];
                                        PC += 2;
                                        break;
                                    }
                                case 0x0003: // 0x8XY3 VX = VX ^ VY
                                    {
                                        V[X] ^= V[Y];
                                        PC += 2;
                                        break;
                                    }
                                case 0x0004: // 0x8XY4 Adds VY to VX. VF is set to 1 when there's a carry, and to 0 when there isn't.
                                    {
                                        V[X] += V[Y];
                                        if (V[Y] > (0xFF - V[X]))
                                            V[0xF] = 1; //carry
                                        else
                                            V[0xF] = 0;                                        
                                        PC += 2;
                                        break;
                                    }
                                case 0x0005: // 0x8XY5 VY is subtracted from VX. VF is set to 0 when there's a borrow, and to 1 when there isn't.
                                    {
                                        if (V[Y] > V[X])
                                            V[0xF] = 0; //borrow
                                        else
                                            V[0xF] = 1;
                                        V[X] -= V[Y];
                                        PC += 2;
                                        break;
                                    }
                                case 0x0006: // 0x8XY6 VX>>=1 and VF stores the least signifiant bit of VX
                                    {
                                        V[0xF] = (byte)(V[X] & 0x1);
                                        V[X] >>= 1;
                                        PC += 2;
                                        break;
                                    }
                                case 0x0007: // 0x8XY7 VX=VY-VX & borrow VF
                                    {
                                        if (V[X] > V[Y])
                                            V[0xF] = 0; //borrow;
                                        else
                                            V[0xF] = 1;
                                        V[X] = (byte)(V[Y] - V[X]);
                                        PC += 2;
                                        break;
                                    }
                                case 0x000E: //0x8XYE VX<<=1 & VF stores most signifiant bit
                                    {
                                        V[0xF] = (byte)(V[X] >> 7);
                                        V[X] <<= 1;
                                        PC += 2;
                                        break;
                                    }
                            }
                            break;
                        }

                    case 0x9000: // 0x9XY0 : skips next instruction if VX!=VY
                        {
                            int X = (OpCode & 0x0F00) >> 8;
                            int Y = (OpCode & 0x00F0) >> 4;
                            if (V[X] != V[Y]) PC += 2;
                            PC += 2;
                            break;
                        }

                    case 0xA000: // 0xANNN : I=NNN
                        {
                            I = (ushort)(OpCode & 0x0FFF);
                            PC += 2;
                            break;
                        }
                    case 0xB000: // PC=V0+NNN
                        {
                            int NNN = OpCode & 0x0FFF;
                            PC = (ushort)(V[0] + NNN);
                            break;
                        }
                    case 0xC000: // 0xCXNN : VX= rand() & NN
                        {
                            int X = (OpCode & 0x0F00) >> 8;
                            int NN = OpCode & 0x00FF;
                            V[X] = (byte)(rand.Next(256) & NN);
                            PC += 2;
                            break;
                        }

                    case 0xD000: // 0xDXYN : draw(Vx,Vy,N)
                        {
                            int X = V[(OpCode & 0x0F00) >> 8];
                            int Y = V[(OpCode & 0x00F0) >> 4];
                            int height = (OpCode & 0x000F);
                            int pixel;

                            V[0xF] = 0;
                            for (int yline = 0; yline < height; yline++)
                            {
                                pixel = Memory[I + yline];
                                for (int xline = 0; xline < 8; xline++)
                                {
                                    if ((pixel & (0x80 >> xline)) != 0)
                                    {                                                                          
                                        int index = (X + xline + (Y + yline) * 64) % 2048; //wrapping                                                                          
                                        bool old = GFX[index];
                                        GFX[index] ^= true;
                                        if (!GFX[index] && old) V[0xF] = 1;                                      
                                    }
                                }
                            }
                            drawflag = true;
                            PC += 2;
                            break;
                        }

                    case 0xE000:
                        {
                            int X = (OpCode & 0x0F00) >> 8;
                            switch (OpCode & 0x00FF)
                            {
                                case 0x009E: // EX9E: Skips the next instruction if the key stored in VX is pressed
                                    {
                                        if (Key[V[X]] != 0)
                                            PC += 4;
                                        else PC += 2;
                                        break;
                                    }
                                case 0x00A1: // EXA1: Skips the next instruction if the key stored in VX isn't pressed
                                    {
                                        if (Key[V[X]] == 0)
                                            PC += 4;
                                        else PC += 2;
                                        break;
                                    }
                                default:
                                    {
                                        Console.WriteLine("Unknown opcode {0:X}", OpCode);
                                        break;
                                    }
                            }
                            break;

                        }

                    case 0xF000:
                        {
                            int X = (OpCode & 0x0F00) >> 8;
                            switch (OpCode & 0x00FF)
                            {
                                case 0x0007: // FX07: Sets VX to the value of the delay timer
                                    {
                                        V[X] = DelayTimer;
                                        PC += 2;
                                        break;
                                    }
                                case 0x000A: // FX0A: A key press is awaited, and then stored in VX
                                    {                                    
                                        bool keyPress = false;
                                        for (int i = 0; i < 16; i++)
                                        {
                                            if (Key[i] != 0)
                                            {
                                                V[X] = (byte)i;
                                                keyPress = true;
                                                break;
                                            }
                                        }
                                        // If we didn't received a keypress, skip this cycle and try again.
                                        if (!keyPress)
                                            return;

                                        PC += 2;
                                        break;                            
                                    }
                                case 0x0015: // FX15: Sets the delay timer to VX
                                    {
                                        DelayTimer = V[X];
                                        PC += 2;
                                        break;
                                    }
                                case 0x0018: // FX18: Sets the sound timer to VX
                                    {
                                        SoundTimer = V[X];
                                        PC += 2;
                                        break;
                                    }
                                case 0x001E: // FX1E: Adds VX to I
                                    {
                                        if (I + V[X] > 0xFFF)  // VF is set to 1 when range overflow (I+VX>0xFFF), and 0 when there isn't.
                                            V[0xF] = 1;
                                        else
                                            V[0xF] = 0;
                                        I += V[X];
                                        PC += 2;
                                        break;
                                    }
                                case 0x0029: // FX29: Sets I to the location of the sprite for the character in VX. Characters 0-F (in hexadecimal) are represented by a 4x5 font
                                    {
                                        I = (ushort)(V[X] * 0x5);
                                        PC += 2;
                                        break;
                                    }

                                case 0x0033:  // 0xFX33 
                                    {                                      
                                        Memory[I] = (byte)(V[X] / 100);
                                        Memory[I + 1] = (byte)((V[X] / 10) % 10);
                                        Memory[I + 2] = (byte)(V[X] % 10);
                                        PC += 2;
                                        break;
                                    }
                                case 0x0055: //Stores V[0] to V[X] at memory starting at I
                                    {
                                        for (int i = 0; i <= X; i++)                                        
                                            Memory[I + i] = V[i];
                                        if (Configuration.IndexIncrement)
                                            I += (ushort)(X + 1);
                                        PC += 2;
                                        break;
                                    }

                                case 0x0065: //Fills V[0] to V[X] with values starting from I
                                    {
                                        for (int i = 0; i <= X; i++) 
                                        {
                                            V[i] = Memory[I + i];
                                        }
                                        if (Configuration.IndexIncrement)
                                            I += (ushort)(X + 1);
                                        PC += 2;
                                        break;
                                    }
                                default:
                                    {
                                        Console.WriteLine("Unknown opcode {0:X4}", OpCode);                                        
                                        break;
                                    }

                            }
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Unknown opcode : {0:X}", OpCode);                            
                            break;
                        }
                }         
        }
        public Action Beep;
        public void Update()
        {
            if (DelayTimer > 0) DelayTimer--;
            if (SoundTimer > 0)
            {
                if (SoundTimer == 1)
                    Beep?.Invoke();
                SoundTimer--;
            }
            if(drawflag)
            {
                drawflag = false;
                Draw?.Invoke(GFX);
            }
        }

        bool drawflag = false;
        public bool DrawFlag { get => drawflag; set => drawflag = value; }
        public Action<bool[]> Draw;        

        public void KeyDown(byte k)
        {
            Key[k] = 1;
        }

        public void KeyUp(byte k)
        {
            Key[k] = 0;
        }

        byte[] C8FontSet = new byte[]
        {
            0xF0, 0x90, 0x90, 0x90, 0xF0, // 0
            0x20, 0x60, 0x20, 0x20, 0x70, // 1
            0xF0, 0x10, 0xF0, 0x80, 0xF0, // 2
            0xF0, 0x10, 0xF0, 0x10, 0xF0, // 3
            0x90, 0x90, 0xF0, 0x10, 0x10, // 4
            0xF0, 0x80, 0xF0, 0x10, 0xF0, // 5
            0xF0, 0x80, 0xF0, 0x90, 0xF0, // 6
            0xF0, 0x10, 0x20, 0x40, 0x40, // 7
            0xF0, 0x90, 0xF0, 0x90, 0xF0, // 8
            0xF0, 0x90, 0xF0, 0x10, 0xF0, // 9
            0xF0, 0x90, 0xF0, 0x90, 0x90, // A
            0xE0, 0x90, 0xE0, 0x90, 0xE0, // B
            0xF0, 0x80, 0x80, 0x80, 0xF0, // C
            0xE0, 0x90, 0x90, 0x90, 0xE0, // D
            0xF0, 0x80, 0xF0, 0x80, 0xF0, // E
            0xF0, 0x80, 0xF0, 0x80, 0x80  // F
        };

    }
}
