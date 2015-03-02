using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Object.Templates.Data
{
    public abstract class DataBase(DataTypes Type)
    {
        public DataTypes Type { get;set; } = Type;

        public DataBase(Wxv.Swg.Common.Files.IFFFile.Node Source, ref int offset, DataTypes DataType) : this(DataType)
        {
            Parse(Source, ref offset);
        }


        public abstract bool Parse(Wxv.Swg.Common.Files.IFFFile.Node Source, ref int offset);

    }
}
