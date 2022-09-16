using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulazy.C8
{
    public class C8OpCodeData
    {
        public ushort OpCode;
        public C8OpCodeData(ushort opcode=0)
        {
            OpCode = opcode;
        }

        public string ToHex
        {
            get
            {
                string hex = OpCode.ToString("X4");                
                return "0x" + hex;
            }
        }

        public string Description
        {
            get
            {
                string NNN = (OpCode & 0x0FFF).ToString("X3");
                string NN = (OpCode & 0x00FF).ToString("X2");
                string N = (OpCode & 0x000F).ToString("X1");
                string X = ((OpCode & 0x0F00) >> 8).ToString("X1");
                string Y = ((OpCode & 0x00F0) >> 4).ToString("X1");
                switch (OpCode & 0xF000)
                {
                    case 0x0000:
                        {
                            switch (OpCode)
                            {
                                case 0x00E0:                                    
                                        return "CLS";                                    
                                case 0x00EE:                                    
                                        return "RET";                                    
                                default:
                                    return $"SYS  #{NNN}";                                   
                            }                            
                        }
                    case 0x1000: // 0x1NNN : goto NNN                        
                        return $"JP   #{NNN}";                                                
                    case 0x2000: // 0x2NNN : calls subroutine NNN                        
                        return $"CALL #{NNN}";
                    case 0x3000: // 0x3XNN : skips next instruction if VX==NN                                                   
                        return $"SE   V{X}, #{NN}";                       
                    case 0x4000: // 0x4XNN : skips next instruction if VX!=NN                                                                               
                        return $"SNE  V{X}, #{NN}";                        
                    case 0x5000: // 0x5XY0 : skips next instruction if VX==VY                                                                               
                        return $"SNE  V{X}, V{Y}";                        
                    case 0x6000: // 0x6XNN : VX=NN                                                                           
                        return $"LD   V{X}, #{NN}";                        
                    case 0x7000: // 0x7XNN : VX+=NN (carry flag is not changed)                                                   
                        return $"ADD  V{X}, #{NN}";                                            
                    case 0x8000:
                        {                                                    
                            switch (OpCode & 0x000F)
                            {
                                case 0x0000: // 0x8XY0 Vx=VY                                    
                                    return $"LD   V{X}, V{Y}";                                   
                                case 0x0001: // 0x8XY1 VX = VX | VY                                    
                                    return $"OR   V{X}, V{Y}";                                    
                                case 0x0002: // 0x8XY2 VX = VX & VY
                                    return $"AND  V{X}, V{Y}";
                                case 0x0003: // 0x8XY3 VX = VX ^ VY
                                    return $"XOR  V{X}, V{Y}";
                                case 0x0004: // 0x8XY4 Adds VY to VX. VF is set to 1 when there's a carry, and to 0 when there isn't.
                                    return $"ADD  V{X}, V{Y}";
                                case 0x0005: // 0x8XY5 VY is subtracted from VX. VF is set to 1 when there's a borrow, and to 0 when there isn't.
                                    return $"SUB  V{X}, V{Y}";
                                case 0x0006: // 0x8XY6 VX>>=1 and VF stores the least signifiant bit of VX
                                    return $"SHR  V{X}, V{Y}";
                                case 0x0007: // 0x8XY7 VX=VY-VX & borrow VF
                                    return $"SUBN V{X}, V{Y}";
                                case 0x000E: //0x8XYE VX<<=1 & VF stores most signifiant bit
                                    return $"SHL  V{X}, V{Y}";
                                default:
                                    return "???";
                            }                            
                        }

                    case 0x9000: // 0x9XY0 : skips next instruction if VX!=VY                                                                              
                        return $"SNE  V{X}, V{Y}";                        
                    case 0xA000: // 0xANNN : I=NNN                                                   
                        return $"LD    I, #{NNN}";                                                    
                    case 0xB000: // PC=V0+NNN                                                    
                        return $"JP   V0, #{NNN}";                                                    
                    case 0xC000: // 0xCXNN : VX= rand() & NN                       
                        return $"RND  V{X}, #{NN}";                        
                    case 0xD000: // 0xDXYN : draw(Vx,Vy,N)                                                                                                           
                        return $"DRW  V{X}, V{Y}, #{N}";                       

                    case 0xE000:
                            switch (OpCode & 0x00FF)
                            {
                                case 0x009E: // EX9E: Skips the next instruction if the key stored in VX is pressed                                    
                                    return $"SKP  V{X}";                                   
                                case 0x00A1: // EXA1: Skips the next instruction if the key stored in VX isn't pressed                                    
                                    return $"SKNP V{X}";                                 
                                default:                                    
                                    return "???";                                    
                            }                                                    

                    case 0xF000:                        
                            switch (OpCode & 0x00FF)
                            {
                                case 0x0007: // FX07: Sets VX to the value of the delay timer                                    
                                    return $"LD   V{X}, DT";
                                case 0x000A: // FX0A: A key press is awaited, and then stored in VX
                                    return $"LD   V{X}, K";
                                case 0x0015: // FX15: Sets the delay timer to VX
                                    return $"LD   DT, V{X}";
                                case 0x0018: // FX18: Sets the sound timer to VX
                                    return $"LD   ST, V{X}";
                                case 0x001E: // FX1E: Adds VX to I
                                    return $"ADD   I, V{X}";
                                case 0x0029: // FX29: Sets I to the location of the sprite for the character in VX. Characters 0-F (in hexadecimal) are represented by a 4x5 font
                                    return $"LD    F, V{X}";
                                case 0x0033:  // 0xFX33 
                                    return $"LD    B, V{X}";
                                case 0x0055: // FX55: Stores V0 to VX in memory starting at address I
                                    return $"LD   V{X}, [I]";
                                case 0x0065: // FX65: Fills V0 to VX with values from memory starting at address I	
                                    return $"LD   [I], V{X}";
                                default:                                    
                                        return "???";                                    
                            }                        
                  default:                        
                        return "???";                        
                }
            }
        }

    }
}
