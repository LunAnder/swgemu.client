using SWG.Client.Object.Templates.Data;
using SWG.Client.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wxv.Swg.Common;
using Wxv.Swg.Common.Files;

namespace SWG.Client.Object.Templates.Shared
{
    public class SharedObjectTemplate
    {

        protected List<string> _loadedDervs = new List<string>();

        private static readonly LogAbstraction.ILogger _Logger = LogAbstraction.LogManagerFacad.GetCurrentClassLogger();

        public int ClientGameObjectType;

        public StringIdData ObjectName;
        public StringIdData DetailedDescription;
        public StringIdData LookAtText;

        public BoolData SnapToTerrain;
        public IntegerData ContainerType;
        public IntegerData ContainerVolumeLimit;

        public StringData TintPallete;

        //public ArrangementDescriptor _arrangementDescriptors;
        //public SlotDescriptor _slotDescriptors;

        public StringIdData SlotDescriptorFileName;
        public StringIdData ArrangementDescriptorFileName;

        public StringData AppearanceFileName;
        public StringData PortalLayoutFileName;
        public StringData ClientDataFile;
        public IntegerData CollisionMaterialFlags;
        public IntegerData CollisionMaterialPassFlags;
        public FloatData Scale;
        public IntegerData CollisionMaterialBlockFlags;
        public IntegerData CollisionActionFlags;
        public IntegerData CollisionActionPassFlags;
        public IntegerData CollisionActionBlockFlags;
        public IntegerData GameObjectType;
        public BoolData SendToClient;
        public FloatData ScaleThresholdBeforeExtentTest;
        public FloatData ClearFloraRadius;
        public IntegerData SurfaceType;
        public FloatData NoBuildRadius;
        public BoolData OnlyVisibleInTools;
        public FloatData LocationReservationRadius;
         


        public SharedObjectTemplate() { }

        public SharedObjectTemplate(IFFFile File, ITemplateRepository repo)
        {
            ReadFile(File, repo);
        }

        public virtual void ReadFile(IFFFile File, ITemplateRepository repo)
        {
            ReadNode(File.Root, repo);
            //InternalParse(File.Root, repo);
        }

        public virtual void ReadNode(IFFFile.Node node, ITemplateRepository repo, bool checkType = true)
        {
            if (checkType && node.Type != "SHOT")
            {
                _Logger.Warn("Got {0} when expecting SHOT", node.Type);
                return;
            }

            ParseData(node, repo, ParseData, _Logger);
            //InternalParse(node, repo);
        }

        protected virtual void ParseDerv(IFFFile.Node node, ITemplateRepository repo)
        {
            var dervOfNode = node.FindSubNode("XXXX");
            if(dervOfNode == null)
            {
                return;
                
            }

            var offset = 0;
            var dervString = dervOfNode.Data.ReadAsciiString(ref offset);
            if (string.IsNullOrEmpty(dervString))
            {
                return;
            }


            if (_loadedDervs.Contains(dervString))
            {
                return;
            }
            
            var dervIff = repo.LoadIff(dervString);
            if (dervIff == null)
            {
                return;
            }

            _loadedDervs.Add(dervString);
            ReadFile(dervIff, repo);
            
        }

        /*private void InternalParse(IFFFile.Node node, ITemplateRepository repo)
        {
            ParseData(node, repo, ParseData, _Logger);
        }*/


        private void ParseData(IFFFile.Node node, ITemplateRepository repo)
        {
            int offset = 0;
            var name = node.Data.ReadAsciiString(ref offset);
            switch (name)
            {
                case "objectName":
                    ObjectName = TryParseAssign(node, ref offset, ObjectName);
                    break;
                case "detailedDescription":
                    DetailedDescription = TryParseAssign(node, ref offset, DetailedDescription);
                    break;
                case "lookAtText":
                    LookAtText = TryParseAssign(node, ref offset, LookAtText);
                    break;
                case "snapToTerrain":
                    SnapToTerrain = TryParseAssign(node, ref offset, SnapToTerrain);
                    break;
                case "containerType":
                    ContainerType = TryParseAssign(node, ref offset, ContainerType);
                    break;
                case "containerVolumeLimit":
                    ContainerVolumeLimit = TryParseAssign(node, ref offset, ContainerVolumeLimit);
                    break;
                case "tintPallete":
                    TintPallete = TryParseAssign(node, ref offset, TintPallete);
                    break;
                case "slotDescriptorFilename":
                    SlotDescriptorFileName = TryParseAssign(node, ref offset, SlotDescriptorFileName);
                    break;
                case "arrangementDescriptorFilename":
                    ArrangementDescriptorFileName = TryParseAssign(node, ref offset, ArrangementDescriptorFileName);
                    break;
                case "appearanceFilename":
                    AppearanceFileName = TryParseAssign(node, ref offset, AppearanceFileName);
                    break;
                case "portalLayoutFilename":
                    PortalLayoutFileName = TryParseAssign(node, ref offset, PortalLayoutFileName);
                    break;
                case "clientDataFile":
                    ClientDataFile = TryParseAssign(node, ref offset, ClientDataFile);
                    break;
                case "collisionMaterialFlags":
                    CollisionMaterialFlags = TryParseAssign(node, ref offset, CollisionMaterialFlags);
                    break;
                case "collisionMaterialPassFlags":
                    CollisionMaterialPassFlags = TryParseAssign(node, ref offset, CollisionMaterialPassFlags);
                    break;
                case "collisionMaterialBlockFlags":
                    CollisionMaterialBlockFlags = TryParseAssign(node, ref offset, CollisionMaterialBlockFlags);
                    break;
                case "collisionActionFlags":
                    CollisionActionFlags = TryParseAssign(node, ref offset, CollisionActionFlags);
                    break;
                case "collisionActionPassFlags":
                    CollisionActionPassFlags = TryParseAssign(node, ref offset, CollisionActionPassFlags);
                    break;
                case "collisionActionBlockFlags":
                    CollisionActionBlockFlags = TryParseAssign(node, ref offset, CollisionActionBlockFlags);
                    break;
                case "scale":
                    Scale = TryParseAssign(node, ref offset, Scale);
                    break;
                case "gameObjectType":
                    GameObjectType = TryParseAssign(node, ref offset, GameObjectType);
                    ClientGameObjectType = GameObjectType.Value;
                    break;
                case "sendToClient":
                    SendToClient = TryParseAssign(node, ref offset, SendToClient);
                    break;
                case "scaleThresholdBeforeExtentTest":
                    ScaleThresholdBeforeExtentTest = TryParseAssign(node, ref offset, ScaleThresholdBeforeExtentTest);
                    break;
                case "clearFloraRadius":
                    ClearFloraRadius = TryParseAssign(node, ref offset, ClearFloraRadius);
                    break;
                case "surfaceType":
                    SurfaceType = TryParseAssign(node, ref offset, SurfaceType);
                    break;
                case "noBuildRadius":
                    NoBuildRadius = TryParseAssign(node, ref offset, NoBuildRadius);
                    break;
                case "onlyVisibleInTools":
                    OnlyVisibleInTools = TryParseAssign(node, ref offset, OnlyVisibleInTools);
                    break;
                case "locationReservationRadius":
                    LocationReservationRadius = TryParseAssign(node, ref offset, LocationReservationRadius);
                    break;
                default:
                    break;
            }

        }


        protected void ParseData(IFFFile.Node node, ITemplateRepository repo, Action<IFFFile.Node, ITemplateRepository> parseDataAction, LogAbstraction.ILogger logger, Action<IFFFile.Node, ITemplateRepository> parseDerveAction = null, bool parseNext = true)
        {

            if(parseDerveAction == null)
            {
                parseDerveAction = ParseDerv;
            }

            var derv = node.FindSubNode("DERV");
            if (derv != null)
            {
                parseDerveAction(node, repo);
            }

            var nextNode = derv != null ? derv.FindNextSibling() : node.Children.FirstOrDefault();

            if(nextNode == null)
            {
                _Logger.Trace("unable to locate next node for node {0}", node.Type);
                return;
            }
            
            var sizeNode = nextNode.FindSubNode("PCNT", true);

            if (sizeNode == null || nextNode.Children == null)
            {
                logger.Trace("Unable to locate PCNT node or its parent for node {0}", node.Type);
                return;
            }

            var nodeList = nextNode.Children.ToList();
            var size = sizeNode.Data.ReadInt32();

            if (size == 0 || size != nodeList.Count - 1)
            {
                return;
            }

            for (int i = 0; i < size; i++)
            {
                switch (nodeList[i].Type)
                {
                    case "PCNT":
                        continue;
                    case "XXXX":
                        parseDataAction(nodeList[i], repo);
                        break;
                    case "DERV":
                        parseDerveAction(nodeList[i], repo);
                        break;
                    default:
                        logger.Warn("Invalid node type {0}", nodeList[i].Type);
                        break;
                }
            }

            nextNode = nextNode.FindNextSibling();

            if(parseNext && nextNode != null)
            {

                ReadNode(nextNode, repo);
            }
        }

        protected T TryParseAssign<T>(IFFFile.Node node, ref int offset, T defaultValue)
            where T : DataBase, new()
        {
            var newVal = new T();
            if(newVal.Parse(node , ref offset))
            {
                return newVal;
            }

            return defaultValue;
        }
    }
}
