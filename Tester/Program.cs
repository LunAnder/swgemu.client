using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

using SWG.Client.Network;
using SWG.Client.Network.Messages;
using SWG.Client.Network.Messages.Login;
using SWG.Client.Network.Messages.Zone;
using SWG.Client.Network.Objects;
using SWG.Client.Utils;
using Wxv.Swg.Common.Files;
using System.IO;
using LogAbstraction.Nlog;
using SWG.Client.Object.Templates;
using SWG.Client.Object.Templates.Shared;
using SWG.Client.Object.Templates.Shared.Tangible;

namespace Tester
{
    internal class Program
    {
        private static readonly LogAbstraction.ILogger _logger =
            LogAbstraction.LogManagerFacad.GetCurrentClassLogger();

        private static void Main(string[] args)
        {
            /*using(var fileStream = File.OpenRead(@"E:\workspace\tre\patch_05.tre"))
             {
                 var treReader = new TREFileReader();
                 var treeFile = treReader.Load(fileStream);

                 var found = treeFile.InfoFiles.FirstOrDefault(cur => cur.Name.ToLower().Contains("shared_base_object.iff"));

                 if (found != null)
                 {
                     var treeStream = found.Open(fileStream);
                     var iffReader = new IFFFileReader();
                     var iffFile = iffReader.Load(treeStream);
                     //var root = iffFile.Root.Children.ToList();

                     var sharedObject = new SharedObjectTemplate(iffFile);

                 }
             }*/

            //var repo = new ArchiveRepository(@"D:\SWGTrees");
            //repo.LoadArchives();

            //var templateRepo = new TemplateRepository
            //{
            //    ArchiveRepo = repo,
            //};



            //var tmp = repo.LoadCrCMap("misc/object_template_crc_string_table.iff");

            //var last = tmp.CRCMap.Last();

            //var stringFile = repo.LoadStf("string/en/city/city.stf");

            //Console.WriteLine("Enter to end;");
            //Console.ReadLine();
            //return;

            //var addrToConnect = "swgemutest";
            //var connectToServerName = "swgemutest";

            LogAbstraction.LogManagerFacad.ManagerImplementaiton = new NlogLogManagerImplementaion();

            var graph = new ObjectGraph { ConnectTimeout = TimeSpan.FromMilliseconds(30000) };
            graph.ResolveMessageFactories();
            graph.ResolveFallbackMessageFactory();
            graph.RegisterMessagesInFacories();
            graph.RegisterMessagesInFallbackFactory();

            var connector = new SessionLogin
            {
                Server = "login.swgemu.com",
                Password = "SWGEmu_889656",
                Username = "crazyguymrkii"
            };

            Console.WriteLine("Connecting to login server");
            connector.ConnectToLoginServer();
            Console.WriteLine("Logging in to login server");
            connector.LoginToServer();

            var serverToConnect = connector.AvailableServers.First(cur => cur.Name == "Basilisk");
            Console.WriteLine("Establishing connecting to game server: {0}:{1}", serverToConnect.ServerIP, serverToConnect.ServerPort);


            graph.EstablishConnection(connector.UserId, connector.SessionKey, IPAddress.Parse(serverToConnect.ServerIP),
                serverToConnect.ServerPort);
            Console.WriteLine("Logging in character");
            graph.LoginCharacter(serverToConnect.Characters.First(cur => cur.Name == "CrazedZealot").CharacterID);
            Console.WriteLine("Press enter to exit");
            Console.ReadLine();

            


        }

    }

}
