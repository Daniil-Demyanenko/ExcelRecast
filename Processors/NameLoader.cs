using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExcelRecast.Processors;

public static class NameLoader
{
    private static string _pathFemale = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "female_names_rus.txt");
    private static string _pathMale = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "male_names_rus.txt");
    private static string[] _names = null;

    public static string[] Names
    {
        get
        {
            if (_names is null) _names = GetNames();
            return _names;
        }
    }


    private static string[] GetNames()
    {
        var names = new List<string>();
        var femaleNames = File.ReadLines(_pathFemale).Select(Normalize);
        var maleNames = File.ReadLines(_pathMale).Select(Normalize);

        names.AddRange(femaleNames);
        names.AddRange(maleNames);

        return names.ToArray();
    }

    public static string Normalize(string name)
        => name.Trim().Replace('ё', 'е').ToLower();
}