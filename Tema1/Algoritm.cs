using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema1
{
    internal abstract class Algoritm<T>
    {

        internal T Key;
        internal string InputPath;
        internal string OutputPath;

       protected Algoritm(T key,string inputPath,string outputPath) {
            this.Key = key;
            this.InputPath = inputPath;
            this.OutputPath = outputPath;
        }

        public abstract void criptare();
        public abstract void decriptare();


    }
}
