using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var signo = "escorpiao";
            string Url = $"https://estilo.uol.com.br/horoscopo/escorpiao/horoscopo-do-dia/";
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(Url);

            var script = doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[3]/script[1]");


            var txtScript = script.InnerHtml;

            var start = txtScript.IndexOf("\"description\"") + "description".Length + 2;
             var end = txtScript.IndexOf("\"author\"");

            var previsao = txtScript.Substring(start + 2, (end - start) - 4);


            var txt = HttpUtility.HtmlDecode(previsao).ToString();

            Console.Write(txt);
            Console.ReadKey();
        }
    }

    public class Logo
    {
        // public string __invalid_name__@type { get; set; }
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Publisher
    {
        //public string __invalid_name__@type { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public Logo logo { get; set; }
    }

    public class Author
    {
        // public string __invalid_name__@type { get; set; }
        public bool name { get; set; }
    }

    public class Image
    {
        //public string __invalid_name__@type { get; set; }
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Mention
    {
        //public string __invalid_name__@type { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class RootObject
    {
        //public string __invalid_name__@type { get; set; }
        //public string __invalid_name__@context { get; set; }
        public Publisher publisher { get; set; }
        public string url { get; set; }
        public string mainEntityOfPage { get; set; }
        public string headline { get; set; }
        public string description { get; set; }
        public Author author { get; set; }
        public Image image { get; set; }
        public List<Mention> mentions { get; set; }
    }
}
