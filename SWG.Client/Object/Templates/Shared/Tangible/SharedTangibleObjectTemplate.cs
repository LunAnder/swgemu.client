using SWG.Client.Object.Templates.Data;
using SWG.Client.Object.Templates.Data.Complex;
using SWG.Client.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wxv.Swg.Common.Files;

namespace SWG.Client.Object.Templates.Shared
{
    public class SharedTangibleObjectTemplate : SharedObjectTemplate
    {
        private static readonly LogAbstraction.ILogger _Logger = LogAbstraction.LogManagerFacad.GetCurrentClassLogger();

        //public StructureFootprint StructureFootprint;

        public BoolData UseStructureFootprintOutline;

        public BoolData Targetable;

        public UInt16 PlayerUseMask;

        public int Level;

        public List<string> CertificationsRequired;

        public int MaxCondition;

        public uint OptionsBitmask;

        public uint PvPStatusBitmask;

        public int UseCount;

        public int FactoryCrateSize;

        public bool Sliceable;

        public int Faction;

        public Dictionary<string, int> SkillMods;

        public List<Int16> NumberExperimentalProperties;

        public List<string> ExperimentalProperties;

        public List<Int16> ExperimentalWeights;

        public List<string> ExperimentalGroupTitles;

        public List<string> ExperimentalSubGroupTitles;

        public List<float> ExperimentalMin;

        public List<float> ExperimentalMax;

        public List<UInt16> ExperimentalPrecision;

        public List<UInt16> ExperimentalCombineType;

        public List<uint> PlayerRaces;

        //public List<ResourceWeight> ResourceWeights;

        public PaletteColorCustomizationVariables PaletteColorCustomizationVariables;

        public RangedIntCustomizationVariables RangedIntCustomizationVariables;


        public override void ReadFile(IFFFile File, ITemplateRepository repo)
        {
            ReadNode(File.Root, repo);
        }

        public override void ReadNode(IFFFile.Node node, ITemplateRepository repo, bool checkType = true)
        {

            if (checkType && node.Type != "STOT")
            {
                base.ReadNode(node, repo);
                return;
            }


            ParseData(node, repo, ParseData, _Logger);
        }

        
        private void ParseData(IFFFile.Node node, ITemplateRepository repo)
        {
            int offset = 0;
            var name = node.Data.ReadAsciiString(ref offset);
            switch (name)
            {
                case "paletteColorCustomizationVariables":
                    PaletteColorCustomizationVariables = TryParseAssign(node, ref offset, PaletteColorCustomizationVariables);
                    break;
                case "rangedIntCustomizationVariables":
                    RangedIntCustomizationVariables = TryParseAssign(node, ref offset, RangedIntCustomizationVariables);
                    break;
                /*case "constStringCustomizationVariables":
                    break;
                case "socketDestinations":
                    break;*/
                /* case "structureFootprintFileName":
                     var structureFootprintFileName = new StringData(node, ref offset);
                     if(structureFootprintFileName.HasValue)
                     {
                         StructureFootprint = repo.LoadStructureFootprint(structureFootprintFileName.Value);
                     }
                     break;*/
                case "useStructureFootprintOutline":
                    UseStructureFootprintOutline = TryParseAssign(node, ref offset, UseStructureFootprintOutline);
                    break;
                case "targetable":
                    Targetable = TryParseAssign(node, ref offset, Targetable);
                    break;
                /*case "certificationsRequired":
                    break;
                case "customizationVariableMapping":
                    break;*/
                default:
                    break;
            }
        }

    }
}
