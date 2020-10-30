using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cw1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var httpclient = new HttpClient())
            {
                try
                {
                    UrlAttribute url = new UrlAttribute();
                    if (!url.IsValid(args[0]))
                    {
                        throw new ArgumentException("Nie prawidlowy URL");
                    }
                }catch (Exception ex)
                {
                    throw new ArgumentNullException("Nie podano url");
                }
                var response = await httpclient.GetAsync(args[0]);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Blad w czasie pobierania strony");
                }
                string responseToString = await response.Content.ReadAsStringAsync();
                char[] spliters = { '<', '>', ':', '\r', '\n', ' ', '"' };
                string[] array = responseToString.Split(spliters);

                string pattern = @"([a-zA-Z0-9._-]+@[a-zA-Z0-9._-]+\.[a-zA-Z0-9_-]+)";
                Regex regex = new Regex(pattern);
                IList<string> mailist = new List<string>();
                foreach (string test in array) 
                {
                    if (regex.IsMatch(test)) 
                    {
                        if (!mailist.Contains(test))
                        {
                            mailist.Add(test);
                        }
                    }
                }
                if (mailist.Count == 0)
                {
                    Console.WriteLine("Nie znaleziono zadnych adresow email");
                }
                else
                {
                    foreach(string test in mailist)
                    {
                        Console.WriteLine(test);
                    }
                }
            }
        }
    }
}
