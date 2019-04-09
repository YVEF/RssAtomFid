using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace ApiTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new WebClient();
            var URI = new Uri("http://localhost:5000/api/discovers/tech/"); // see more requests in README.txt

            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwiZW1haWwiOiJ0ZXN0QHRlc3QuY29tIiwibmJmI" +
                "joxNTU0ODE4MTE4LCJleHAiOjE1NTUyNTAxMTgsImlhdCI6MTU1NDgxODExOH0.YtAGwcwgcACU9GW2hVYrSGSkLvRDbjAaJkmyC-rs0Oc"; // exists token


            client.Headers.Add("Authorization", "Bearer " + token);
            client.Headers.Add("Content-Type", "application/json; charset=utf-8");

            var result = client.DownloadString(URI);


            Console.WriteLine(result);

            Console.Read();

            client.Dispose();
        }

        private string Login(string email, string password)
        {
            var user = JsonConvert.SerializeObject(new { Email = email, Password = password });
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/json; charset=utf-8");
                return client.UploadString(new Uri("http://localhost:5000/api/account/login"), "POST", user);
            }                
        }

        private string Register(string userName, string email, string password, string passwordConfirm)
        {
            var user = JsonConvert.SerializeObject(new {
                UserName = userName,
                Email = email,
                Password = password,
                PasswordConfirm = passwordConfirm
            });
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/json; charset=utf-8");
                return client.UploadString(new Uri("http://localhost:5000/api/account/register"), "POST", user);
            }
        }
    }
}
