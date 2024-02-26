using System;
using System.Collections.Generic;
using System.Linq;


namespace ExcelRecast.Processors;

public static class NameProcessor
{
    private const int NameCol = 4;
    private const int ResultCol = 5;
    private const string BadNameValue = "ПРОВЕРИТЬ";

    public static void ProcessNames(this List<string[]> table)
    {
        for (int i = 0; i < table.Count; i++)
        {
            var fullname = table[i][NameCol];

            table[i][ResultCol] = VerdictFor(fullname);
        }
    }

    private static string VerdictFor(string fullname)
    {
        var names = fullname.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        foreach (var name in names)
            if (NameLoader.Names.Contains(NameLoader.Normalize(name)))
                return name;

        return BadNameValue;
    }
}