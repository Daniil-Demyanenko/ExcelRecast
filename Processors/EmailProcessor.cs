using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ExcelRecast.Processors;

public static class EmailProcessor
{
    private const int EmailCol = 2;
    private const int ResultCol = 3;
    private const string BadMailValue = "ПРОВЕРИТЬ";
    private const string CorporateMailValue = "Корпоративный";
    private const string ConsumerMailValue = "Пользовательский";

    private static readonly string[] ConsumerDomains =
    [
        "outlook.com", "hotmail.com", "icloud.com", "protonmail.com", "gmail.com", "mail.ru", "yandex.ru", "ya.ru", "ya.ua", "yandex.ua", "rambler.ru", "vk.com", "yahoo.com"
    ];

    public static void ProcessMails(this List<string[]> table)
    {
        for (int i = 0; i < table.Count; i++)
        {
            var mail = table[i][EmailCol];

            table[i][ResultCol] = VerdictFor(mail);
        }
    }

    private static string VerdictFor(string mail)
    {
        if(mail is null || !IsValid(mail)) return BadMailValue;
        if (IsCorporateMail(mail)) return CorporateMailValue;
        return ConsumerMailValue;
    }

    private static bool IsValid(string mail)
    {
        var pattern = @"^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";

        return Regex.IsMatch(mail, pattern);
    }

    private static bool IsCorporateMail(string mail)
    {
        var domain = mail.Split('@').Last();
        return !ConsumerDomains.Contains(domain);
    }
}