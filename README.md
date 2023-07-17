# Using Dotnet to access data.gouv.fr API

Caution: I'm not a C# developer, so do not hesitate to make feedback here. Example borrowed mainly from https://www.youtube.com/watch?v=1jV08t6aK34


Recipe below


```bash
mkdir csharp-data-gouv
cd csharp-data-gouv
dotnet new console --force --use-program-main
dotnet add package Newtonsoft.Json --version 13.0.3

echo 'using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace csharp_data_gouv
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}' >| Program.cs

dotnet build
dotnet bin/Debug/net7.0/csharp-data-gouv.dll

echo 'using System;
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
}' >| Program.cs

dotnet build
dotnet bin/Debug/net7.0/csharp-data-gouv.dll
```
