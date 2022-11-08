using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Docati.DocumentGeneration.CLI
{
    static class Helpers
    {
        public static void PrintError(string error)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(error);
            Console.ForegroundColor = originalColor;
        }

        public static void PrintError(Exception ex)
        {
            PrintError(ex, false);
        }

        public static void PrintError(Exception ex, bool showStackTrace)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            if (showStackTrace)
                Console.Error.WriteLine(ex);
            else
                Console.Error.WriteLine(ex.Message);
            Console.ForegroundColor = originalColor;
        }
    }
}
