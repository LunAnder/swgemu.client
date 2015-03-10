using SWG.Client.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wxv.Swg.Common.Files;

namespace SWG.Client.Object.Templates.Data.Complex
{
    public class ComplexVectorData<T> : VectorData<T>
        where T : DataBase, new()
    {

        protected virtual string NodeName { get { return ""; } }

        public override bool Parse(IFFFile.Node Source, ref int offset)
        {
            var type1 = Source.Data.ReadByte(ref offset);
            var numElements = Source.Data.ReadInt32(ref offset);

            if (numElements == 0)
            {
                return false;
            }

            Value = new List<T>(numElements);

            foreach (var child in Source.Children.Where(cur => cur.Type == NodeName))
            {
                var toAdd = new T();
                var ofst = 0;
                if (toAdd.Parse(child, ref ofst))
                {
                    Value.Add(toAdd);
                }
            }

            return true;
        }

        public override bool Parse(byte[] Data, ref int offset)
        {
            throw new NotImplementedException();
        }
    }
}
