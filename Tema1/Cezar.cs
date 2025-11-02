using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Tema1
{
    internal class Cezar : NRot {
        internal Cezar(string inputPath, string outputPath) : base(inputPath, outputPath, 4) {}
        internal new void setKey(int k) {
            Console.WriteLine("Key in Caesar algoritm can only be 3");
        }
        internal new void analiza() {
            throw new Exception("Cheie e 3 la cifrul cezar.Doar decripteaza");
        }

    }
}
