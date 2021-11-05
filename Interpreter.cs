﻿/*  This file is part of Iterkocze ippScriptInterpreter and it's under BSD-3-Clause License.
    Copyright (c) 2021, Iterkocze-Company
    All rights reserved.
    
*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ippScriptInterpreter
{
    class Interpreter
    {
        public static int errors = 0;

        private static List<string> VAR_NAMES_INT = new();
        private static List<int> VAR_VALS_INT = new();

        private static List<string> VAR_NAMES_FLOAT = new();
        private static List<float> VAR_VALS_FLOAT = new();

        private static List<string> VAR_NAMES_DOUBLE = new();
        private static List<double> VAR_VALS_DOUBLE = new();

        private static List<string> VAR_NAMES_CHAR = new();
        private static List<char> VAR_VALS_CHAR = new();

        private static List<string> VAR_NAMES_STRING = new();
        private static List<string> VAR_VALS_STRING = new();

        private static List<string> VAR_ALL = new();

        private static string[] ReadFileContents(string pathToFile)
        {
            return File.ReadAllText(pathToFile).Replace("\r\n", "").Split(";");
        }

        private static string[] lines = ReadFileContents(Program.CODE_FILE_PATH);

        public static void Interpret()
        {
            Console.Clear();
            Log.Debug("Interpreter");

            foreach (string line in lines)
            {
                if (line != lines[lines.Length - 1])
                {
                    string[] parts = line.Split(" ");
                    int a = 0;

                    while (parts[a] == "") a++;

                    string afterFirst = line;
                    string toFirst = "";
                    for (int j = 0; j <= a; j++)
                    {
                        if (parts[j] == "") toFirst += " ";
                        else toFirst += parts[j];
                    }
                    afterFirst = afterFirst.Replace(toFirst, "");
                    SyntaxChecker.Analyse(line);

                    switch (parts[a].Replace("\n", "").Replace("\t", ""))
                    {
                        case "EchoLine":
                            string textToEchoLine = line.Replace("\"", "").Replace("EchoLine", "").Replace("\t", "");
                            string textToEchoLine2 = "";
                            foreach (char c in textToEchoLine)
                            {
                                if (c != ' ') textToEchoLine2 += c;
                                if (c == '~') textToEchoLine2 += " ";
                                foreach (string name in VAR_NAMES_INT)
                                {
                                    if (name == c.ToString())
                                    {
                                        textToEchoLine2 += VAR_VALS_INT[VAR_NAMES_INT.IndexOf(c.ToString())];
                                        textToEchoLine2 = textToEchoLine2.Replace(c.ToString(), "");
                                    }
                                }
                                foreach (string name in VAR_NAMES_FLOAT)
                                {
                                    if (name == c.ToString())
                                    {
                                        textToEchoLine2 += VAR_VALS_FLOAT[VAR_NAMES_FLOAT.IndexOf(c.ToString())];
                                        textToEchoLine2 = textToEchoLine2.Replace(c.ToString(), "");
                                    }
                                }
                                foreach (string name in VAR_NAMES_DOUBLE)
                                {
                                    if (name == c.ToString())
                                    {
                                        textToEchoLine2 += VAR_VALS_DOUBLE[VAR_NAMES_DOUBLE.IndexOf(c.ToString())];
                                        textToEchoLine2 = textToEchoLine2.Replace(c.ToString(), "");
                                    }
                                }
                                foreach (string name in VAR_NAMES_CHAR)
                                {
                                    if (name == c.ToString())
                                    {
                                        textToEchoLine2 += VAR_VALS_CHAR[VAR_NAMES_CHAR.IndexOf(c.ToString())];
                                        textToEchoLine2 = textToEchoLine2.Replace(c.ToString(), "");
                                    }
                                }
                                foreach (string name in VAR_NAMES_STRING)
                                {
                                    if (name == c.ToString())
                                    {
                                        textToEchoLine2 += VAR_VALS_STRING[VAR_NAMES_STRING.IndexOf(c.ToString())];
                                        textToEchoLine2 = textToEchoLine2.Replace(c.ToString(), "");
                                    }
                                }
                            }
                            Console.WriteLine(textToEchoLine2.Replace("~", ""));
                            break;

                        case "Echo":
                            string textToEcho = line.Replace("\"", "").Replace("Echo", "").Replace("\t", "");
                            string textToEcho2 = "";
                            foreach (char c in textToEcho)
                            {
                                if (c != ' ') textToEcho2 += c;
                                if (c == '~') textToEcho2 += " ";
                                foreach (string name in VAR_NAMES_INT)
                                {
                                    if (name == c.ToString())
                                    {
                                        textToEcho2 += VAR_VALS_INT[VAR_NAMES_INT.IndexOf(c.ToString())];
                                        textToEcho2 = textToEcho2.Replace(c.ToString(), "");
                                    }
                                }
                                foreach (string name in VAR_NAMES_FLOAT)
                                {
                                    if (name == c.ToString())
                                    {
                                        textToEcho2 += VAR_VALS_FLOAT[VAR_NAMES_FLOAT.IndexOf(c.ToString())];
                                        textToEcho2 = textToEcho2.Replace(c.ToString(), "");
                                    }
                                }
                                foreach (string name in VAR_NAMES_DOUBLE)
                                {
                                    if (name == c.ToString())
                                    {
                                        textToEcho2 += VAR_VALS_DOUBLE[VAR_NAMES_DOUBLE.IndexOf(c.ToString())];
                                        textToEcho2 = textToEcho2.Replace(c.ToString(), "");
                                    }
                                }
                                foreach (string name in VAR_NAMES_CHAR)
                                {
                                    if (name == c.ToString())
                                    {
                                        textToEcho2 += VAR_VALS_CHAR[VAR_NAMES_CHAR.IndexOf(c.ToString())];
                                        textToEcho2 = textToEcho2.Replace(c.ToString(), "");
                                    }
                                }
                                foreach (string name in VAR_NAMES_STRING)
                                {
                                    if (name == c.ToString())
                                    {
                                        textToEcho2 += VAR_VALS_STRING[VAR_NAMES_STRING.IndexOf(c.ToString())];
                                        textToEcho2 = textToEcho2.Replace(c.ToString(), "");
                                    }
                                }
                            }
                            Console.Write(textToEcho2.Replace("~", ""));
                            break;

                        case "CreateFile":
                            string filenameToCreate = line.Replace("CreateFile", "").Trim();
                            File.Create(Directory.GetCurrentDirectory() + "/" + filenameToCreate + ".txt");
                            break;

                        case "File.Write":
                            string filenameToWrite = line.Replace("File.Write", "").Trim();
                            filenameToWrite = filenameToWrite.Remove(line.IndexOf(',')).Trim();
                            string textToWrite = line.Substring(line.IndexOf(",") + 1);
                            File.WriteAllText(Directory.GetCurrentDirectory() + "/" + filenameToWrite + ".txt", "");
                            break;


                        //Vars
                        case "int":
                            if (line.Contains("="))
                            {
                                string var_name = line.Replace("int", "").Replace("=", "").Trim();
                                string var_val = "";
                                foreach (char c in var_name)
                                {
                                    if (Char.IsDigit(c)) var_val += c;
                                }
                                var_name = var_name.Remove(var_name.IndexOf(" "));
                                VAR_NAMES_INT.Add(var_name);
                                VAR_ALL.Add(var_name);
                                VAR_VALS_INT.Add(int.Parse(var_val));
                            }
                            else
                            {
                                string var_name = line.Replace("int", "").Trim();
                                VAR_NAMES_INT.Add(var_name);
                                VAR_ALL.Add(var_name);
                                VAR_VALS_INT.Add(0);
                            }
                            break;
                        case "float":
                            if (line.Contains("="))
                            {
                                string var_name = line.Replace("float", "").Replace("=", "").Trim();
                                string var_val = "";
                                foreach (char c in var_name)
                                {
                                    if (Char.IsDigit(c) || c == '.') var_val += c;
                                }
                                var_name = var_name.Remove(var_name.IndexOf(" "));
                                VAR_NAMES_FLOAT.Add(var_name);
                                VAR_ALL.Add(var_name);
                                VAR_VALS_FLOAT.Add(float.Parse(var_val, CultureInfo.InvariantCulture.NumberFormat));
                            }
                            else
                            {
                                string var_name = line.Replace("float", "").Trim();
                                VAR_NAMES_FLOAT.Add(var_name);
                                VAR_ALL.Add(var_name);
                                VAR_VALS_FLOAT.Add(0);
                            }
                            break;
                        case "double":
                            if (line.Contains("="))
                            {
                                string var_name = line.Replace("double", "").Replace("=", "").Trim();
                                string var_val = "";
                                foreach (char c in var_name)
                                {
                                    if (Char.IsDigit(c) || c == '.') var_val += c;
                                }
                                var_name = var_name.Remove(var_name.IndexOf(" "));
                                VAR_NAMES_DOUBLE.Add(var_name);
                                VAR_ALL.Add(var_name);
                                VAR_VALS_DOUBLE.Add(double.Parse(var_val, CultureInfo.InvariantCulture.NumberFormat));
                            }
                            else
                            {
                                string var_name = line.Replace("double", "").Trim();
                                VAR_NAMES_DOUBLE.Add(var_name);
                                VAR_ALL.Add(var_name);
                                VAR_VALS_DOUBLE.Add(0);
                            }
                            break;
                        case "char":
                            if (line.Contains("="))
                            {
                                string var_name = line.Replace("char", "").Replace("=", "").Trim();
                                string var_val = var_name.Substring(var_name.IndexOf(" ")).Replace("\'", "").Trim();
                                var_name = var_name.Remove(var_name.IndexOf(" "));
                                VAR_NAMES_CHAR.Add(var_name);
                                VAR_ALL.Add(var_name);
                                VAR_VALS_CHAR.Add(var_val.ToCharArray()[0]);
                            }
                            else
                            {
                                string var_name = line.Replace("char", "").Trim();
                                VAR_NAMES_CHAR.Add(var_name);
                                VAR_ALL.Add(var_name);
                                VAR_VALS_CHAR.Add(' ');
                            }
                            break;
                        case "string":
                            if (line.Contains("="))
                            {
                                string var_name = line.Replace("string", "").Replace("=", "").Trim();
                                string var_val = var_name.Substring(var_name.IndexOf(" ")).Replace("\"", "").Trim();
                                var_name = var_name.Remove(var_name.IndexOf(" "));
                                VAR_NAMES_STRING.Add(var_name);
                                VAR_ALL.Add(var_name);
                                VAR_VALS_STRING.Add(var_val);
                            }
                            else
                            {
                                string var_name = line.Replace("string", "").Trim();
                                VAR_NAMES_STRING.Add(var_name);
                                VAR_ALL.Add(var_name);
                                VAR_VALS_STRING.Add("UNDEFINED");
                            }
                            break;
                    }
                }

                bool skip = true;

                foreach (string var in VAR_ALL)
                {
                    if (line.Contains(var))
                    {
                        if ((!line.EndsWith("++") && !line.EndsWith("--")) && (!line.Contains("=") && !line.Contains("Echo") && !line.Contains("end")))
                            skip = false;
                    }
                }

                int indexOfVars = 0;
                foreach (string var in VAR_ALL)
                {
                    if (skip == false)
                    {
                        string otherIntVal = "";
                        foreach (string name in VAR_ALL)
                        {
                            string valTwo = "";
                            foreach (string name2 in VAR_ALL)
                            {
                                if (line.Contains("+")) valTwo = line.Substring(line.IndexOf("+")).Replace("+", "").Trim();
                                if (line.Contains("-")) valTwo = line.Substring(line.IndexOf("-")).Replace("-", "").Trim();
                                if (line.Contains("/")) valTwo = line.Substring(line.IndexOf("/")).Replace("/", "").Trim();
                                if (line.Contains("*")) valTwo = line.Substring(line.IndexOf("*")).Replace("*", "").Trim();
                                if (line.Contains("%")) valTwo = line.Substring(line.IndexOf("%")).Replace("%", "").Trim();

                                if (line.Contains(name) && line.Contains(valTwo) && (!int.TryParse(valTwo, out _) || float.TryParse(valTwo, out _)))
                                {
                                    if (VAR_NAMES_INT.Contains(name))
                                    {
                                        int index = VAR_NAMES_INT.IndexOf(valTwo);
                                        otherIntVal = VAR_VALS_INT[index].ToString();
                                        VAR_VALS_INT[indexOfVars] += int.Parse(otherIntVal);
                                        goto end;
                                    }
                                    if (VAR_NAMES_FLOAT.Contains(name) && !float.TryParse(valTwo, out _))
                                    {
                                        int index = VAR_NAMES_FLOAT.IndexOf(valTwo);
                                        otherIntVal = VAR_VALS_FLOAT[index].ToString();
                                        VAR_VALS_FLOAT[indexOfVars] += float.Parse(otherIntVal, CultureInfo.InvariantCulture);
                                        goto end;
                                    }
                                    if (VAR_NAMES_DOUBLE.Contains(name) && !float.TryParse(valTwo, out _))
                                    {
                                        int index = VAR_NAMES_DOUBLE.IndexOf(valTwo);
                                        otherIntVal = VAR_VALS_DOUBLE[index].ToString();
                                        VAR_VALS_DOUBLE[indexOfVars] += float.Parse(otherIntVal, CultureInfo.InvariantCulture);
                                        goto end;
                                    }
                                }
                            }
                            if (line.Contains(name))
                            {
                                if (line.Contains("+")) otherIntVal = line.Replace("+", "").Replace(name, "").Trim();
                                if (line.Contains("-")) otherIntVal = line.Replace("-", "").Replace(name, "").Trim();
                                if (line.Contains("/")) otherIntVal = line.Replace("/", "").Replace(name, "").Trim();
                                if (line.Contains("*")) otherIntVal = line.Replace("*", "").Replace(name, "").Trim();
                                if (line.Contains("%")) otherIntVal = line.Replace("%", "").Replace(name, "").Trim();

                                foreach (string gujdfoihdf in VAR_NAMES_INT)
                                    if (line.Contains(gujdfoihdf)) VAR_VALS_INT[VAR_NAMES_INT.IndexOf(gujdfoihdf)] += int.Parse(otherIntVal);
                                foreach (string gujdfoihdf in VAR_NAMES_FLOAT)
                                    if (line.Contains(gujdfoihdf)) VAR_VALS_FLOAT[VAR_NAMES_FLOAT.IndexOf(gujdfoihdf)] += float.Parse(otherIntVal, CultureInfo.InvariantCulture);
                                foreach (string gujdfoihdf in VAR_NAMES_DOUBLE)
                                    if (line.Contains(gujdfoihdf)) VAR_VALS_DOUBLE[VAR_NAMES_DOUBLE.IndexOf(gujdfoihdf)] += double.Parse(otherIntVal, CultureInfo.InvariantCulture);
                            }
                        }
                    end:
                        break;
                    }

                    if (line.EndsWith("++"))
                    {
                        VAR_VALS_INT[indexOfVars]++;
                        break;
                    }
                    if (line.EndsWith("--"))
                    {
                        VAR_VALS_INT[indexOfVars]--;
                        break;
                    }
                    indexOfVars++;
                }

                indexOfVars = 0;
            }

            Log.Debug("\nInterpreter exited.\n");
            Console.ReadKey();
        }
    }
}

