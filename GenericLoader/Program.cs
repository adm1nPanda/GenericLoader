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
                Console.WriteLine("Application requires a b64 of binary as an argument. \nExample : ./GenericLoader.exe <PATH TO b64 of EXE> <args for exe>(user blank quotes if no args)");
                System.Environment.Exit(0);
            }

            if(args[0] != null)
            {
                Console.WriteLine("[*] Reading Bytes from File.");
                var b64ShellCode = File.ReadAllText(args[0]);
                byte[] ShellCode = Convert.FromBase64String(b64ShellCode);

                string exeargs = args[1];
                string[] exeargsplit = exeargs.Split(' ');

                Console.WriteLine("Executing file : " + args[0]);
                Console.WriteLine("Arguments passed : " + args[1]);

                //Executing Shellcode
                if (ShellCode.Length > 0) {
                    try {
                        var assembly = Assembly.Load(ShellCode);
                        var method = assembly.EntryPoint;
                        
                        object instance = assembly.CreateInstance(method.Name);
                        object[] ar = new object[] { exeargsplit };
                        
                        try {
                            object ret = assembly.EntryPoint.Invoke(instance, ar);
                            if (ret != null) {
                                Console.WriteLine(ret.ToString());
                            }                        
                        }
                        catch (Exception e){
                            Console.WriteLine("Forwarding an error from the executable");
                            Console.WriteLine(e); 
                        }
                    }
                    catch (BadImageFormatException e) {
                        Console.WriteLine("BadImageFormatExecption: Loaded x86 Shellcode in x64 exe (or Vice-Versa)");
                    }
                    catch {
                        Console.WriteLine("Loading assembly fails. Possible that shellcode was built with a later .NET framework");
                    }
                }
            }
        }
    }
}
