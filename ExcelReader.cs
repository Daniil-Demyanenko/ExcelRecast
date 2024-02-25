using System;
using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;

namespace ExcelRecast;

public class ExcelReader: IDisposable
{
    private readonly XLWorkbook _wb;
    private IXLWorksheet _ws;

    public ExcelReader(string path)
    {
        _wb = new XLWorkbook(path);
        _ws = _wb.Worksheet(1);
    }

    public List<string[]> Read()
    {
        var table = new List<string[]>();
        var lastRow = _ws.LastRowUsed().RowNumber();
        var lastCol = _ws.LastColumnUsed().ColumnNumber();
        
        for (int i = 2; i <= lastRow; i++) // Потому что на первой строке шапка 
        {
            var row = Enumerable.Range(1, lastCol).Select(j => _ws.Cell(i, j).Value.ToString().Trim()).ToArray();
            table.Add(row);
        }

        return table;
    }

    public void Dispose()
    {
        _wb.Dispose();
    }
}
