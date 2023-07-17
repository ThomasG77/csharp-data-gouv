# Using Dotnet to access data.gouv.fr API

Caution: I'm not a C# developer, so do not hesitate to make feedback here. Example borrowed mainly from <https://www.youtube.com/watch?v=1jV08t6aK34>

## How to execute the sample

```bash
git clone https://github.com/ThomasG77/csharp-data-gouv.git
cd csharp-data-gouv
dotnet build
dotnet bin/Debug/net7.0/csharp-data-gouv.dll
```


Output

```bash
Dataset infos
28/06/2023 14:57:35
27/06/2023 17:08:37
Resource infos
8b5c75df-24ea-43ae-9f4c-6f5c633e942b table-adresses-reu.parquet 27/06/2023 16:54:51
Resource infos
5142e8a9-15f3-4216-b865-deeeb02dde70 table-adresses-reu.csv 27/06/2023 16:55:14
Resource infos
45ff30bb-d1f1-416a-8fcd-1dcc04edd520 dictionnaire-donnees-adresses.pdf 27/06/2023 17:07:26
Resource infos
6faacf36-1897-43f5-bf39-af8b41a15d26 table-bv-reu.parquet 27/06/2023 16:55:41
Resource infos
3a321a06-9e05-4de9-8dd3-421da6abd4c5 table-bv-reu.csv 27/06/2023 16:55:52
Resource infos
8c5fa7a6-d5c6-439f-9d45-9e11865bc6d4 dictionnaire-donnees-bv.pdf 27/06/2023 17:06:16
Resource infos
5ba909b1-6900-42d2-b970-ceae2f381abd methodologie.pdf 27/06/2023 17:08:37
```

If you want to use another url instead of current URL in the code <https://www.data.gouv.fr/api/1/datasets/bureaux-de-vote-et-adresses-de-leurs-electeurs/>, go to <https://data.gouv.fr>, look at a dataset like for example <https://www.data.gouv.fr/fr/datasets/carte-des-loyers-indicateurs-de-loyers-dannonce-par-commune-en-2022/>. Then, after url `https://www.data.gouv.fr/api/1/datasets/` add `carte-des-loyers-indicateurs-de-loyers-dannonce-par-commune-en-2022/` to get API URL <https://www.data.gouv.fr/api/1/datasets/carte-des-loyers-indicateurs-de-loyers-dannonce-par-commune-en-2022/>. Finally, replace the hardcoded URL with your own.


## Full recipe

Recipe below if starting from scratch


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
