/*  This file is part of Iterkocze ippScriptInterpreter and it's under BSD-3-Clause License.
    Copyright (c) 2021, Iterkocze-Company
    All rights reserved.
    
*/

using System;
using System.IO;
using System.Linq;

namespace ippScriptInterpreter
{
    class Program
    {
        public static string CODE_FILE_PATH = "";
        public static int FILE_LEN = 0;

        private static void HandleCompilerFlags(string[] flags)
        {
            foreach (string flag in flags)
            {
                if (flag.Trim().Contains(".ipps")) CODE_FILE_PATH = Path.GetFullPath(flag);
                if (flag.Trim().Contains(".ipp")) CODE_FILE_PATH = Path.GetFullPath(flag);
            }
        }

        private static bool SetFilePath(string path)
        {
            if (!File.Exists(path))
            {
                Log.Error("An error occured while opening code file!");
                Console.ReadLine();
                return false;
            }
            else
            {
                CODE_FILE_PATH = Path.GetFullPath(path);
                FILE_LEN = File.ReadLines(path).Count();
                return true;
            }

        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to ippScript Interpreter.");
            if (args.Length == 0)
            {
                Console.Write("Enter ipps code file path: ");
                bool opt = SetFilePath(Console.ReadLine());
                if (opt) HandleCompilerFlags(args);
                if (opt) Interpreter.Interpret();
            }
            else
            {
                HandleCompilerFlags(args);
                Interpreter.Interpret();
            }
        }
    }
}
