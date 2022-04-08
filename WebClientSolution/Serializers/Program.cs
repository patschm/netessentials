using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Serializers;

class Program
{
    static void Main()
    {
        //Serialzie();
        //Deserialize();
        JsonSers();
    }

    private static void JsonSers()
    {
        Person p = new Person { Id = 1, FirstName = "Jan", LastName = "Hendriks", Age = 50 };
        JsonSerializer serializer = new JsonSerializer();
        serializer.ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() };
        

        FileStream fileStream = File.Create(@"D:\Test\person.json");
        using (StreamWriter writer = new StreamWriter(fileStream))
            serializer.Serialize(writer, p);
    }

    private static void Deserialize()
    {
        XmlSerializer serialize = new XmlSerializer(typeof(Person));
        FileStream fileStream = File.OpenRead(@"D:\Test\person.xml");
        using (XmlReader reader = XmlReader.Create(fileStream))
        {
            Person p = serialize.Deserialize(reader) as Person;
            Console.WriteLine(p.Id);
        }
    }

    private static void Serialzie()
    {
        Person p = new Person { Id = 1, FirstName="Jan", LastName="Hendriks", Age=50 };

        XmlSerializer serialize = new XmlSerializer(typeof(Person));
        FileStream fileStream = File.Create(@"D:\Test\person.xml");
        using (XmlWriter writer = XmlWriter.Create(fileStream))
            serialize.Serialize(writer, p);
    }
}
