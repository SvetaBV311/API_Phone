using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace ApiPhone
{
    public class Response   // указываем тут переменные, которые хотим получить
    {
        public string Provider { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Timezone { get; set; }
    }


    internal class Program
    {
        static HttpClient httpClient = new HttpClient();
        static async Task Main(string[] args)
        {
            Console.WriteLine("Введите номер телефона:");
            string query = Console.ReadLine();

            httpClient.DefaultRequestHeaders.Add("Authorization", "Token 80390271b06d4d9635938b51d98bb1d1ee4ab19e");
            httpClient.DefaultRequestHeaders.Add("X-Secret", "eeafe170b0a22f10e3a128602180c1df4fbc4620");
            var numberResponse = httpClient.PostAsJsonAsync("https://cleaner.dadata.ru/api/v1/clean/phone", new[] { query }).Result;
            var message = await numberResponse.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<Response[]>();


            Console.WriteLine();
            Console.WriteLine("Провайдер: " + message[0].Provider);
            Console.WriteLine("Страна: " + message[0].Country);
            Console.WriteLine("Регион: " + message[0].Region);
            if (message[0].City == null)
            {
                Console.WriteLine("Город: Нет данных");
            }
            else
            {
                Console.WriteLine("Город: " + message[0].City);
            }  
            Console.WriteLine("Временная зона: " + message[0].Timezone);
            Console.WriteLine();
        }
    }
}

