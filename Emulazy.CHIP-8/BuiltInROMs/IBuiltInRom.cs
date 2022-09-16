using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulazy.C8.BuiltInROMs
{
    public interface IBuiltInROM
    {
        byte[] Bytes { get; }
        int FPS { get; }
    }
}
