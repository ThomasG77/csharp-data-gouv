using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace csharp_data_gouv
{
    class Program
    {
        public static async Task Main(string[] args)
        {
        dynamic response = await Program.MakeRequest("https://www.data.gouv.fr/api/1/datasets/bureaux-de-vote-et-adresses-de-leurs-electeurs/");
                //649998d50f6f27459dc6cf5b/");
        Console.WriteLine("Dataset infos");
        Console.WriteLine(response.last_modified);
        Console.WriteLine(response.last_update);
        foreach (dynamic resource in response.resources)
            {
        Console.WriteLine("Resource infos");
            Console.WriteLine(resource["id"] + " " + resource["title"] + " " + resource["last_modified"]);
            }

        }
        public static async Task<dynamic> MakeRequest(string url)
        {   
        using var client = new HttpClient();
        var result = await client.GetStringAsync(url);
            dynamic json = JsonConvert.DeserializeObject(result);
        return json;
        }
    }
}
