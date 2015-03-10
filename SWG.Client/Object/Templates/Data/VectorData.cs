using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wxv.Swg.Common.Files;
using SWG.Client.Utils;

namespace SWG.Client.Object.Templates.Data
{
    public class VectorData<T> : TemplateBase<List<T>>
        where T : DataBase, new()
    {

        public VectorData() : base(DataTypes.Vector) { }

        public VectorData(IFFFile.Node Source, ref int Offset) : base(Source, ref Offset, DataTypes.Vector)
        {

        }

        public VectorData(byte[] Data, ref int Offset) : base(Data, ref Offset, DataTypes.Vector)
        {

        }

        public override bool Parse(byte[] Data, ref int offset)
        {
            int size = Data.ReadInt32(ref offset);

            Value = new List<T>(size);
            for (int i = 0; i < size; i++)
            {
                var newData = new T();
                if (newData.Parse(Data, ref offset))
                {
                    Value.Add(newData);
                }
            }

            return Value.Count > 0;
        }

        public override bool Parse(IFFFile.Node Source, ref int offset)
        {
            return Parse(Source.Data, ref offset);
        }

        protected override bool InternalParseValue(byte[] Data, ref int offset, byte type)
        {
            return true;
        }

        public static implicit operator VectorData<T>(List<T> Values)
        {
            return new VectorData<T>
            {
                Value = Values
            };
        }

        public static implicit operator List<T>(VectorData<T> Data)
        {
            if(Data == null || !Data.HasValue)
            {
                return null;
            }

            return Data.Value;
        }
    }
}
