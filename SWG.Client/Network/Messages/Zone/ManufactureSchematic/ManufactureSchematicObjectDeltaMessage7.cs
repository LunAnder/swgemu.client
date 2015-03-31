using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Network.Messages.Base;
using SWG.Client.Network.Objects;
using SWG.Client.Utils.Attribute;

namespace SWG.Client.Network.Messages.Zone.ManufactureSchematic
{
    [RegisterDeltaMessage(MessageOp.MSCO, 0x07)]
    public class ManufactureSchematicObjectDeltaMessage7 : DeltaMessage
    {
        public ListChange<StringFile>[] SlotNames { get; set; }
        public ListChange<int>[] SlotContentTypes { get; set; }
        public ListChange<long>[] IngerdientIds { get; set; }
        public ListChange<int> IngredientQuantities { get; set; }
        public ListChange<float>[] QualitiesChanges { get; set; }
        public ListChange<int>[] Values { get; set; }
        public ListChange<int>[] SlotIndexes { get; set; }
        public int IngredientsCounter { get; set; }
        public ListChange<StringFile>[] ExperimentationNames { get; set; }
        public ListChange<float>[] CurrentExperimentationValues { get; set; }
        public ListChange<float>[] ExperimentationOffsets { get; set; }
        public ListChange<float>[] BlueBarSizes { get; set; }
        public ListChange<float>[] MaxExperimentations { get; set; }
        public ListChange<string>[] CustomizationNames { get; set; }
        public ListChange<int>[] PalleteSelections { get; set; }
        public ListChange<int>[] PalleteStartIndexs { get; set; }
        public ListChange<int>[] PalleteEndIndexs { get; set; }
        public byte CustomizationCounter { get; set; }
        public float RiskFactor { get; set; }
        public ListChange<string>[] ObjectTemplateCustomizations { get; set; }
        public byte Ready { get; set; }
        


        public ManufactureSchematicObjectDeltaMessage7()
        {
        }

        public ManufactureSchematicObjectDeltaMessage7(byte[] Data, int Size, bool parseFromData = false) : base(Data, Size, parseFromData)
        {
        }

        public ManufactureSchematicObjectDeltaMessage7(Message message, bool parseFromData = false) : base(message, parseFromData)
        {
        }


        public override bool ParseFromData()
        {
            if (!base.ParseFromData())
            {
                return false;
            }

            for (int i = 0; i < UpdateCount; i++)
            {
                var updateId = ReadInt16();
                switch (updateId)
                {
                    case 0x00:
                        SlotNames = ReadIndexedListChanges<StringFile>();
                        break;
                    case 0x01:
                        SlotContentTypes = ReadIntIndexedListChanges();
                        break;
                    //case 0x02:
                    //    break;
                    //case 0x03:
                    //    break;
                    //case 0x04:
                    //    break;
                    //case 0x05:
                    //    break;
                    //case 0x06:
                    //    break;
                    //case 0x07:
                    //    break;
                    case 0x08:
                        ExperimentationNames = ReadIndexedListChanges<StringFile>();
                        break;
                    case 0x09:
                        CurrentExperimentationValues = ReadFloatIndexListChanges();
                        break;
                    case 0x0A:
                        ExperimentationOffsets = ReadFloatIndexListChanges();
                        break;
                    case 0x0B:
                        BlueBarSizes = ReadFloatIndexListChanges();
                        break;
                    case 0x0C:
                        MaxExperimentations = ReadFloatIndexListChanges();
                        break;
                    case 0x0D:
                        CustomizationNames = ReadIndexedListChanges(() => ReadString(Encoding.ASCII));
                        break;
                    case 0x0E:
                        PalleteSelections = ReadIntIndexedListChanges();
                        break;
                    case 0x0F:
                        PalleteStartIndexs = ReadIntIndexedListChanges();
                        break;
                    case 0x10:
                        PalleteEndIndexs = ReadIntIndexedListChanges();
                        break;
                    case 0x11:
                        CustomizationCounter = ReadByte();
                        break;
                    case 0x12:
                        RiskFactor = ReadFloat();
                        break;
                    case 0x13:
                        ObjectTemplateCustomizations = ReadIndexedListChanges(() => ReadString(Encoding.ASCII));
                        break;
                    case 0x14:
                        Ready = ReadByte();
                        break;
                }

            }

            return true;
        }
    }
}
