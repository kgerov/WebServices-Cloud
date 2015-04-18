using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using Newtonsoft.Json;

namespace ConsoleClient
{
    class Interact
    {
        static void Main(string[] args)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:62858/api/");

            bool flag = true;

            while (flag)
            {
                Console.Write("Enter command: ");
                string cmd = Console.ReadLine();
                string[] tokens = cmd.Split(' ');

                switch (tokens[0])
                {
                    case "register":
                        if (tokens.Length != 4)
                        {
                            Console.WriteLine("Not enough info. Invalid Command.");
                            break;
                        }

                        Register(tokens[1], tokens[2], tokens[3], httpClient);
                        break;
                    case "exit":
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Command");
                        break;
                }
            }
        }

        static int Register(string email, string pass, string passConfirm, HttpClient httpClient)
        {
            if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(pass) || String.IsNullOrEmpty(passConfirm))
            {
                Console.WriteLine("Invalid");
                return 0;
            }

            if (pass != passConfirm)
            {
                Console.WriteLine("Passwords don't match");
                return 0;
            }

            User user = new User() {email = email, password = "asdfA-123123", confirmPassword = "asdfA-123123"};

            CreateUser(httpClient, user);

            return 1;
        }

        static async void CreateUser(HttpClient client, User user)
        {
            Console.WriteLine(JsonConvert.SerializeObject(user));
            HttpContent content = new StringContent(JsonConvert.SerializeObject(user));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync("Account/Register", content);

            try
            {
                response.EnsureSuccessStatusCode();
                Console.WriteLine("Account Created!");
            }
            catch (Exception)
            {
                Console.WriteLine(response.RequestMessage);
                Console.WriteLine(response.StatusCode);
                Console.WriteLine("Error creating student");
            }
        }

        private class User
        {
            public string email { get; set; }

            public string password { get; set; }

            public string confirmPassword { get; set; }
        }
    }
}
