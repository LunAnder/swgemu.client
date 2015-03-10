using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wxv.Swg.Common.Files;

namespace SWG.Client.Object.Templates.Shared
{
    public class SharedCellObjectTemplate : SharedObjectTemplate
    {
        private static readonly LogAbstraction.ILogger _Logger = LogAbstraction.LogManagerFacad.GetCurrentClassLogger();

        public override void ReadNode(IFFFile.Node node, ITemplateRepository repo, bool checkType = true)
        {
            if (node.Type != "CCLT")
            {
                _Logger.Warn("Got {0} when expecting SHOT", node.Type);
                base.ReadNode(node, repo, checkType);
                return;
            }

            //nothing special about cell objects
            ParseData(node, repo, (n, r) => { }, _Logger);
        }

    }
}
