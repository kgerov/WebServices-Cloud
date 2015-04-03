using System;
using System.Net;
using System.Net.Http;

namespace ConsoleApplicationWebClient
{
    class Client
    {
        static void Main(string[] args)
        {
            using (var client = new WebClient())
            {
                var response = client.UploadString("http://localhost:59201/distance?x1=3&y1=3&x2=2&y2=3", "POST", "");

                Console.WriteLine(response);
            }
        }
    }
}
