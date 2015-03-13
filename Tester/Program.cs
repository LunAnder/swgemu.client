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
using SWG.Client.Object.Templates;
using SWG.Client.Object.Templates.Shared;
using SWG.Client.Object.Templates.Shared.Tangible;

namespace Tester
{
    class Program
    {
        private static readonly LogAbstraction.ILogger _logger =
            LogAbstraction.LogManagerFacad.GetCurrentClassLogger();

        static void Main(string[] args)
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

            var connector = new SessionLogin();
            connector.Server = "login.swgemu.com";
            connector.Password = "SWGEmu_889656";
            connector.Username = "crazyguymrkii";

            connector.ConnectToLoginServer();
            connector.LoginToServer();

            var serverToConnect = connector.AvailableServers.First(cur => cur.Name == "Basilisk");

            var graph = new ObjectGraph();
            graph.ConnectTimeout = TimeSpan.FromMilliseconds(30000);
            graph.EstablishConnection(connector.UserId, connector.SessionKey, IPAddress.Parse(serverToConnect.ServerIP),
                serverToConnect.ServerPort);

            graph.LoginCharacter(serverToConnect.Characters.First(cur => cur.Name == "CrazedZealot").CharacterID);

            Console.ReadLine();

            
            }

        }
        
    }
}
