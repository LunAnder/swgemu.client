using SWG.Client.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wxv.Swg.Common.Files;

namespace SWG.Client.Object.Templates.Shared.Tangible
{
    public class StaticObjectTemplate : SharedObjectTemplate
    {

        private static readonly LogAbstraction.ILogger _Logger = LogAbstraction.LogManagerFacad.GetCurrentClassLogger();


        public override void ReadNode(IFFFile.Node node, ITemplateRepository repo, bool checkType = true)
        {
            if (node.Type != "STAT")
            {
                _Logger.Warn("Got {0} when expecting STAT", node.Type);
                base.ReadNode(node, repo);
                return;
            }

            ParseData(node, repo, (n, r) => { }, _Logger);
        }

    }
}
