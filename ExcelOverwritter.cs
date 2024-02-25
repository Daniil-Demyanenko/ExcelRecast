using System;
using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;

namespace ExcelRecast;

public class ExcelOverwritter: IDisposable
{
    private readonly XLWorkbook _wb;
    private IXLWorksheet _ws;

    public ExcelOverwritter(string path)
    {
        _wb = new XLWorkbook(path);
        _ws = _wb.Worksheet(1);
    }

    public void Write(IEnumerable<IEnumerable<string>> table)
    {
        int i = 2; // На первой строке шапка
        foreach (var row in table)
        {
            int j = 1;
            foreach (var cell in row)
            {
                _ws.Cell(i, j).Value = cell ?? "";
                j++;
            }
            i++;
        }
    }


    public void Dispose()//TODO: нормальный save()
    {
        // _wb.Save();
        _wb.SaveAs(@"/home/demyan/Desktop/b.xlsx");
        _wb.Dispose();
    }
}
