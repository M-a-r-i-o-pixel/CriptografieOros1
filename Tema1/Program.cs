using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Tema1
{
    internal class Program
    {

        static string plainPath = "plain.txt";
        static string cypherPath = "cypher.txt";
        static string decypheredPath = "decyphered.txt";
        static string analysisPath = "analysis.txt";
        public static void afis(string filePath)
        {
            string text = File.ReadAllText(filePath);
            Console.Write(text + "\n\n\n");
        }
        public static void afis(string prefix, string filePath)
        {
            string text = File.ReadAllText(filePath);
            Console.Write(prefix + ":" + text + "\n\n\n");
        }

        public static void cezarTest()
        {
            Console.WriteLine("[[ Caesar ]]");


            Cezar cez = new Cezar(plainPath, cypherPath);
            cez.criptare();
            cez.InputPath = cypherPath;
            cez.OutputPath = decypheredPath;
            cez.decriptare();
            cez.OutputPath = analysisPath;
            cez.analiza();
        }
        public static void nRotTest()
        {
            Console.WriteLine("[[ NRot ]]");


            NRot nrot = new NRot(plainPath, cypherPath, 10);
            nrot.criptare();
            nrot.InputPath = cypherPath;
            nrot.OutputPath = decypheredPath;
            nrot.decriptare();
            nrot.OutputPath = analysisPath;
            nrot.analiza();
        }
        public static void substitutionTest()
        {

            Console.WriteLine("[[ SUBSTITUTION ]]");

            string key = "qwertyuiopasdfghjklzxcvbnm";
            Substitution sub = new Substitution(key, plainPath, cypherPath);
            sub.criptare();
            sub.InputPath = cypherPath;
            sub.OutputPath = decypheredPath;
            sub.decriptare();
            sub.OutputPath = analysisPath;
            sub.manuscriptPath = "manuscript.txt";
            sub.analiza();
        }
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            afis($"Plain text", plainPath);
            try
            {
                // substitutionTest();
                nRotTest();

                //cezarTest();



                Console.ForegroundColor = ConsoleColor.Blue;

                afis("Cyphered text", cypherPath);
                Console.ForegroundColor = ConsoleColor.Yellow;
                afis("Decyphered text", decypheredPath);
                Console.ForegroundColor = ConsoleColor.Red;
                afis("Analysised text", analysisPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}