using System.Net;
using Newtonsoft.Json;

namespace WebClient;

class Program
{
    static void Main()
    {
        SimpleCall();

        Console.WriteLine("End!!!");
        Console.ReadLine();
    }

    private static async Task SimpleCall()
    {
        //WebRequest re = WebRequest.Create("file://localhost:7107");
        //re.GetResponse()

        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7107/");

        var response = await client.GetAsync("WeatherForecast");
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine(response.Content.Headers.ContentType);
            var str = await response.Content.ReadAsStreamAsync();

            using (StreamReader rdr = new StreamReader(str))
            {
                string json = rdr.ReadToEnd();
                var wfs = JsonConvert.DeserializeObject<WeatherForecast[]>(json);
                foreach(var item in wfs)
                {
                    Console.WriteLine(item.Summary);
                }
            }
            //using (JsonReader reader = new JsonTextReader(rdr))
            //{
            //    while (reader.Read())
            //    {
            //        Console.WriteLine(reader.Value);
            //        Console.WriteLine("=========================");
            //    }
            //}
            
            client.Dispose();
        }
    }
}
