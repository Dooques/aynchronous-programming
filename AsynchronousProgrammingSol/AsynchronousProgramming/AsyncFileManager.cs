using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousProgramming
{
    internal class AsyncFileManager
    {
        public static string ReadFile(string filePath) 
        {
            return File.ReadAllTextAsync(filePath).Result;
            
        }
    }
}
