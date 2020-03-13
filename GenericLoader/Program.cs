using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace GenericLoader
{
    class Program {
        public static void Main(string[] args) {
            if (args.Length < 1) {
                Console.WriteLine("Application requires a Base64 encoded binary as an argument. \nExample : ./GenericLoader.exe <PATH TO EXE>");
                System.Environment.Exit(0);
            }

            if(args[0] != null)
            {
                //Decode B64 string passed via arguments
                Console.WriteLine("[*] Reading Bytes from File.");
                var ShellCode = File.ReadAllBytes(args[0]);
                
                //Executing Shellcode Extracted from B64 String
                if (ShellCode.Length > 0) {
                    try {
                        var assembly = Assembly.Load(ShellCode);
                        var method = assembly.EntryPoint;
                        
                        object instance = assembly.CreateInstance(method.Name);
                        object[] ar = new object[] { new string[] { args[1] } };
                        try {
                            object ret = assembly.EntryPoint.Invoke(instance, ar);
                            if (ret != null) {
                                Console.WriteLine(ret.ToString());
                            }                        
                        }
                        catch (Exception e){ Console.WriteLine(e); }
                    }
                    catch {
                        Console.WriteLine("Loading assembly fails. Possible that shellcode was built with a later .NET framework");
                    }
                }
            }
        }
    }
}
