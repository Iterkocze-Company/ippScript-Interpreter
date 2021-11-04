using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ippScriptInterpreter
{
    class SyntaxChecker
    {
        public static byte StringsChars = 0;
        public static void Analyse(string line)
        {
            foreach (char c in line)
            {
                if (c == '\"')
                {
                    StringsChars++;
                }
            }

            if (line.StartsWith(" "))
            {
                Log.Error("Spaces detected. Use only tabs!\n");
                Interpreter.errors++;
            }

            if (StringsChars % 2 != 0)
            {
                Log.Error($"String without end in: {line}\n");
                Interpreter.errors++;
                StringsChars = 0;
            }
        }
    }
}
