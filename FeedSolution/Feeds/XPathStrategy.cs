using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Feeds
{
    public class XPathStrategy : IProcessStreamStrategy
    {
        public IEnumerable<Item> Process(Stream stream)
        {
            XDocument doc = XDocument.Load(stream);
            var result = doc.XPathEvaluate("//item/title | //item/description | //item/category[1]");
            if(result is IEnumerable<object> results)
            {
                foreach(var item in results)
                {
                    Console.WriteLine(item);
                }
            }
            yield return new Item();
        }
    }
}
