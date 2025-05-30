using System;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Web;
namespace ClassicConnect
{
    public class ClassicubeAPI
    {
        public class ClassicubeServer
        {
            public string country_abbr { get; set; }
            public bool featured { get; set;}
            public string hash { get; set; }
            public string ip { get; set; }
            public int maxplayers { get; set; }
            public string mp_pass { get; set; }
            public string name { get; set; }
            public int players { get; set; }
            public int port { get; set; }
            public string software { get; set; }
            public int uptime { get; set; }
            public bool web { get; set; }

        }

        public class ClassicubeServerList
        {
            public List<ClassicubeServer> servers { get; set; }
        }

        public class CLassicubeLoginDetails
        {
            public bool authenticated { get; set; }

            public List<string> errors { get; set; }
            public string token { get; set; }

            public string username { get; set; }
        }


        static CookieContainer cookieContainer = new CookieContainer();
        static HttpClientHandler classicubeHTTPhandler = new HttpClientHandler() {
            CookieContainer = cookieContainer, UseCookies=true, AllowAutoRedirect=true };
        private static HttpClient classicubeHTTPClient = new(classicubeHTTPhandler)
        {
            BaseAddress = new Uri("https://classicube.net"),
        };

        public static async Task<List<ClassicubeServer>> GetServerListAsync()
        {
            var result = (await classicubeHTTPClient.GetFromJsonAsync<ClassicubeServerList>("api/servers/"));
            return result != null ? result.servers : new List<ClassicubeServer>();
        }

        public static async Task<List<ClassicubeServer>> GetServerInfoAsync(string hash)
        {
            var result = (await classicubeHTTPClient.GetFromJsonAsync<ClassicubeServerList>($"api/server/{hash}/"));
            return result != null ? result.servers : new List<ClassicubeServer>();
        }

        public static List<ClassicubeServer> GetServerList()
        {
            return Task.Run(() => GetServerListAsync()).Result;
        }

        public static List<ClassicubeServer> GetServerInfo(string hash)
        {
            return Task.Run(() => GetServerInfoAsync(hash)).Result;
        }

        public static bool Login(string username, string password)
        {
            return Task.Run(() => LoginAsync(username, password)).Result;
        }

        public static CLassicubeLoginDetails LoginDetails =  new CLassicubeLoginDetails() { username = "unknown", authenticated = false, token = "", errors = ["connection"] };
       
        public static async Task<bool> LoginPost(string username, string password, string token, string logincode="")
        {
            var values = new Dictionary<string, string>
            {
                { "username", username },
                { "password", password },
                { "token"   , token},
                { "login_code", logincode }
            };

            var content = new FormUrlEncodedContent(values);
            Console.WriteLine(await content.ReadAsStringAsync());

            var response = await classicubeHTTPClient.PostAsync($"/api/login/", content);

            Console.WriteLine(response.StatusCode);
            var loginDetails = await response.Content.ReadFromJsonAsync<CLassicubeLoginDetails>();
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            if (loginDetails.errors.Count > 0)
            {
                Console.WriteLine("Login errors: ");
                foreach (var e in loginDetails.errors)
                    Console.WriteLine(e);

                return false;
            }

            if (!loginDetails.authenticated)
            {
                Console.WriteLine("Warning not authenticated!");
                Console.WriteLine("Please enter the verification code: ");
                var code = Console.ReadLine();
                return await LoginAsync(username, password, code.Trim());
            }

            LoginDetails = loginDetails;
            return true;
        }
        public static async Task<bool> LoginAsync(string username, string password, string login_code="")
        {
            Console.WriteLine($"Logging in as {username}...");

            var result = (await classicubeHTTPClient.GetFromJsonAsync<CLassicubeLoginDetails>($"/api/login/"));
            
            if (result == null)
            {
                Console.WriteLine("Failed to retreive login token");
                return false;
            }

            if (result.authenticated)
            {
                LoginDetails = result;
                return true;
            }

            Console.WriteLine(string.Join(", ", result.errors));

            Console.WriteLine($"Token {result.token}");
            return await LoginPost(username, password, result.token, login_code);
        }

    }
}
