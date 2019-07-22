using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace GenericLoader
{
    class Program
    {

        static StringBuilder sb = new StringBuilder();
        
         public static string Init(byte[] vars)
         {
            String args = Encoding.UTF8.GetString(vars, 0, vars.Length);
            String[] argList = args.Split(' ');


            Main(argList);

            return sb.ToString();
            
         }

        public static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Application requires a Base64 encoded binary as an argument. \nExample : ./GenericLoader.exe <PATH TO B64 File>");
                System.Environment.Exit(0);
            }

            if(args[0] != null)
            {

                //Redirect all STDOUT to a stream
                MemoryStream stream = new MemoryStream();
                StreamWriter sw = new StreamWriter(stream);
                TextWriter old_stdout = Console.Out;
                Console.SetOut(sw);


                //Decode B64 string passed via arguments
                Console.WriteLine("[*] Reading Base64 from File.");
                string fbyte = System.IO.File.ReadAllText(args[0]);
                byte[] ShellCode = Convert.FromBase64String(fbyte);
                

                //Executing Shellcode Extracted from B64 String
                if (ShellCode.Length > 0)
                {
                    try
                    {
                        var assembly = Assembly.Load(ShellCode);
                    
                        foreach (var type in assembly.GetTypes())
                        {
                            Console.WriteLine("[*] Loaded Type {0}", type);
                            object instance = Activator.CreateInstance(type);
                            object[] ar = new object[] { new string[] { "" } };
                            try
                            {
                                object ret = type.GetMethod("Main").Invoke(instance, ar);
                                if (ret != null)
                                {
                                    sb.AppendLine(ret.ToString());
                                }                        
                            }
                            catch (Exception e){ Console.WriteLine(e); }

                        }
                    }
                    catch
                    {
                        Console.WriteLine("Loading assembly fails. Possible that shellcode was built with a later .NET framework");
                    }
                }

                Console.SetOut(old_stdout);
                sw.Flush();
                string output = Encoding.ASCII.GetString(stream.ToArray());
                sw.Close();
                sb.AppendLine(output);
                
            }

            
            
        }
    }
}
