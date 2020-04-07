using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleClient1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest()
            {
                Address=disco.TokenEndpoint,
                ClientId = "console client",
                ClientSecret = "console client",
                Scope="api1"
            }) ;

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }
            var client2 = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client2.GetAsync("http://localhost:5002/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
            Console.WriteLine(tokenResponse.Json);

            Console.WriteLine("Hello World!");
        }
    }
}
