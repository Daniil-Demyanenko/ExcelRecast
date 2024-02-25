using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ExcelRecast.Processors;

public static class PhoneNumberProcessor
{
    private const int PhoneCol = 0;
    private const int ResultCol = 1;
    private const string BadNumberValue = "ПРОВЕРИТЬ";
    

    public static void ProcessPhones(this List<string[]> table)
    {
        for (int i = 0; i < table.Count; i++)
        {
            var phone = table[i][PhoneCol];
            if (phone is null) continue;

            table[i][ResultCol] = VerdictFor(phone);
        }
    }

    private static string VerdictFor(string phone)
    {
        var digitsOnly = ConverToDigitsOnly(phone);
        if (phone[0] == '+' && digitsOnly[0] != '7') return BadNumberValue;

        return IsRussianPhone(digitsOnly) ? digitsOnly : BadNumberValue;
    }

    private static string ConverToDigitsOnly(string phone)
    {
        string digitsOnly = Regex.Replace(phone, @"[^\d]", "");
        if (digitsOnly.Length == 10) digitsOnly = "7" + digitsOnly;
        return digitsOnly;
    }

    private static bool IsRussianPhone(string phone)
    {
        return phone.Length == 11 && phone[0] is '7' or '8';
    }
}