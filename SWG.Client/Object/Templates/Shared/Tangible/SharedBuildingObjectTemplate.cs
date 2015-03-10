using SWG.Client.Object.Templates.Data;
using SWG.Client.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wxv.Swg.Common.Files;

namespace SWG.Client.Object.Templates.Shared.Tangible
{
    public class SharedBuildingObjectTemplate : SharedTangibleObjectTemplate
    {
        private static readonly LogAbstraction.ILogger _Logger = LogAbstraction.LogManagerFacad.GetCurrentClassLogger();

        public StringData TerrainModificationFileName;
        public StringData InteriorLayoutFileName;


        public override void ReadNode(IFFFile.Node node, ITemplateRepository repo, bool checkType = true)
        {

            if (checkType && node.Type != "SBOT")
            {
                base.ReadNode(node, repo);
                return;
            }


            ParseData(node, repo, ParseData, _Logger);
        }

        private void ParseData(IFFFile.Node node, ITemplateRepository repo)
        {
            var offset = 0;
            var varName = node.Data.ReadAsciiString(ref offset);
            switch (varName)
            {
                case "terrainModificationFileName":
                    TerrainModificationFileName = TryParseAssign(node, ref offset, TerrainModificationFileName);
                    break;
                case "interiorLayoutFileName":
                    InteriorLayoutFileName = TryParseAssign(node, ref offset, InteriorLayoutFileName);
                    break;
                default:
                    break;
            }
        }
    }
}
