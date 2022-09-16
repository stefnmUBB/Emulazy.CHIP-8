using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulazy.C8
{
    public class C8Configuration
    {
        /// <summary>
        /// https://faizilham.github.io/revisiting-chip8    :
        /// In the original Chip-8, instruction Fx55 and Fx65 should store/load register the values of V0 to Vx into/from memory starting from address I, 
        /// and then increment register I by x (I = I + x). Some newer games do not take into account the register I increment, and behaves as if I is 
        /// not changed by these instructions. Tic-Tac-Toe and Space Invaders also have this quirk.
        /// </summary>
        public bool IndexIncrement { get; set; } = false;
        
    }
}
