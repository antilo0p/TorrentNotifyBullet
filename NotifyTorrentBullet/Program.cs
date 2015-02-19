using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Net;

namespace NotifyTorrentBullet
{
    public class NoteNotification
    {
        public string type { get; set; }
        public string title { get; set; }
        public string body { get; set; }

        public NoteNotification(string Type, string Title, string Body)
        {
            type = Type;
            title = Title;
            body = Body;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Number of command line parameters = {0}", args.Length);
            if (args.Length < 2)
            {
                Console.WriteLine("Usage:");
                Console.WriteLine("{0} \"<token>\" \"<torrent_name>\" ", System.AppDomain.CurrentDomain.FriendlyName);
                Thread.Sleep(2000);
                System.Environment.Exit(-1);
            }
            String paramtoken = args[0];
            String paramtorrent = args[1];
            Console.WriteLine("Using token {0} to notify about {1}", paramtoken, paramtorrent);

            NoteNotification nota = new NoteNotification("note", "Torrent completed", paramtorrent);

            var client = new RestClient();
            client.BaseUrl = new Uri("https://api.pushbullet.com/v2/");

            client.Authenticator = new HttpBasicAuthenticator(paramtoken, "");
            var request = new RestRequest("pushes", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { type = nota.type, title = nota.title, body = nota.body });

            IRestResponse response = client.Execute(request);
            var content = response.Content;
            if ( response.StatusCode != HttpStatusCode.Accepted)
            {
                Console.WriteLine("Unauthorized access while retrieving response., check access token or connectivty");
                Console.WriteLine(content);
                Thread.Sleep(2000);
                Console.WriteLine("bye!");
                System.Environment.Exit(-1);
            }
            else
            {
                Console.WriteLine("Result from REST call:");
                Console.WriteLine(content);
                Console.WriteLine("---> Done! good bye.");
                Thread.Sleep(5000);
                Console.WriteLine("bye!");
            }
        }
    }
}
