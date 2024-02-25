using System;
using ExcelRecast.Processors;

namespace ExcelRecast
{
    class Program
    {
        static void Main(string[] args)
        {
            using var er = new ExcelReader("/home/demyan/Desktop/a.xlsx");
            var table = er.Read();
            
            table.ProcessPhones();
            
            using var ew = new ExcelOverwritter("/home/demyan/Desktop/a.xlsx");
            ew.Write(table);
        }
    }
}
