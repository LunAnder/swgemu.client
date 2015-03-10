using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Object.Templates.Data
{
    public abstract class DataBase
    {
        

        public DataTypes Type { get;set; }


        public DataBase(DataTypes DataType)
        {
            Type = DataType;
        }

        public DataBase(Wxv.Swg.Common.Files.IFFFile.Node Source, ref int offset, DataTypes DataType) : this(DataType)
        {
            Parse(Source, ref offset);
        }

        public DataBase(byte[] Data, ref int offset, DataTypes DataType) : this(DataType)
        {
            Parse(Data, ref offset);
        }

        public abstract bool Parse(Wxv.Swg.Common.Files.IFFFile.Node Source, ref int offset);

        public abstract bool Parse(byte[] Data, ref int offset);  

    }
}
