using System;
using ExcelRecast.Processors;

namespace ExcelRecast
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var file in args)
            {
                using var reader = new ExcelReader(file);
                var table = reader.Read();

                table.ProcessPhones();
                table.ProcessMails();
                table.ProcessNames();

                using var writer = new ExcelOverwritter(file);
                writer.Write(table);
            }
        }
    }
}