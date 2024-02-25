using ExcelRecast.Processors;

namespace ExcelRecast
{
    class Program
    {
        static void Main(string[] args)
        {
            using var reader = new ExcelReader("/home/demyan/Desktop/a.xlsx");
            var table = reader.Read();
            
            table.ProcessPhones();
            table.ProcessMails();
            table.ProcessNames();
            
            using var writer = new ExcelOverwritter("/home/demyan/Desktop/a.xlsx");
            writer.Write(table);
        }
    }
}
