using System;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FeedZillaConsume
{
    class Consume
    {
        static async void GetArticlesByQuery(HttpClient client)
        {
            Console.WriteLine("Enter seqrch query: ");
            string query = "Michael";

            var responce = await client.GetAsync("search.json?q=" + query + "&count=3");

            responce.EnsureSuccessStatusCode();
            string json = await responce.Content.ReadAsStringAsync();

            printArticles(json);
        }

        private static void printArticles(string json)
        {
            var tmp = JsonConvert.DeserializeObject<Response>(json);
            var articles = JArray.Parse(JsonConvert.SerializeObject(tmp.articles));

            foreach (var article in articles)
            {
                try
                {
                    Console.WriteLine(article["title"]);
                    Console.WriteLine(article["url"]);
                    Console.WriteLine(new String('=', 100));
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        static void Main(string[] args)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://api.feedzilla.com/v1/categories/26/articles/");

            GetArticlesByQuery(httpClient);
            var a = Console.ReadLine();

            //string json = File.ReadAllText("../../a.json");
            //printArticles(json);
        }

        private class Response
        {
            public object[] articles;
            public string description;
            public string syndication_url;
            public string title;
        }
    }
}
