using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wxv.Swg.Common.Files;

namespace SWG.Client.Object.Templates.Shared.Tangible
{
    public class SharedBaseFillerBuilding : SharedBuildingObjectTemplate
    {
        private static readonly LogAbstraction.ILogger _Logger = LogAbstraction.LogManagerFacad.GetCurrentClassLogger();

        public override void ReadNode(IFFFile.Node node, ITemplateRepository repo, bool checkType = true)
        {
            if (checkType && node.Type != "STAT")
            {
                base.ReadNode(node, repo);
                return;
            }
            //nothing special about filler buildings
            ParseData(node, repo, (n,r) => { }, _Logger);
        }
    }
}
