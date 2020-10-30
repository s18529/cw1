using System;
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
                var response = await httpclient.GetAsync(args[0]);
                response.EnsureSuccessStatusCode();
                string responseToString = await response.Content.ReadAsStringAsync();
                string[] array = responseToString.Split();

                string pattern = @"([a-zA-Z0-9._-]+@[a-zA-Z0-9._-]+\.[a-zA-Z0-9_-]+)";
                Regex regex = new Regex(pattern);
                foreach (string test in array) 
                {
                    if (regex.IsMatch(test)) 
                    {
                        Console.WriteLine(test);
                    }
                }
            }
        }
    }
}
