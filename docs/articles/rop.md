
# ROP usage

namespace `Maya.Ext.Rop` includes the `Result` class and its extension methods.

## Example

`````c#
using System;
using System.IO;
using System.Threading.Tasks;

using Maya.Ext.Rop;

namespace MayaExtResearch
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello! Enter file path:");
            string path = Console.ReadLine();
            ReadFile(path)
                .Handle(
                    success => Console.WriteLine(success),
                    fail =>
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine(fail);
                        Console.ResetColor();
                    }
                );
            Console.ReadKey();
        }

        static Result<string, Exception> ReadFile(string path)
        {
            try
            {
                // This text is added only once to the file.
                if (!File.Exists(path))
                {
                    return Result<string, Exception>.Failed(new Exception("File does not exists"));
                }

                // Open the file to read from.
                string readText = File.ReadAllText(path);

                return Result<string, Exception>.Succeeded(readText);
            }
            catch (Exception e)
            {
                return Result<string, Exception>.Failed(e);
            }
        }
    }
}

`````