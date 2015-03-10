using SWG.Client.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Object.Templates.Data
{
    public abstract class TemplateBase<T> : DataBase
    {

        public TemplateBase(DataTypes Type) : base(Type) { }


        public TemplateBase(byte[] Data, ref int Offset, DataTypes DataType) : base(Data, ref Offset, DataType)
        {

        }

        public TemplateBase(Wxv.Swg.Common.Files.IFFFile.Node Source, ref int Offset, DataTypes DataType) :base(Source, ref Offset, DataType)
        {

        }

        public bool HasValue { get; set; }

        private T _value;

        public virtual T Value
        {
            get { return _value; }
            set
            {
                _value = value;
                HasValue = true;
            }
        }

        public override string ToString()
        {
            if (HasValue)
            {
                return Value.ToString();
            }

            return base.ToString();
        }

        public static bool operator ==(TemplateBase<T> x, TemplateBase<T> y)
        {
            return Equals(ref x, ref y);
        }

        public static bool operator !=(TemplateBase<T> x, TemplateBase<T> y)
        {
            return !Equals(ref x, ref y);
        }

        private static bool Equals(ref TemplateBase<T> x, ref TemplateBase<T> y)
        {
            if(!x.HasValue || !y.HasValue)
            {
                return false;
            }

            return EqualityComparer<T>.Default.Equals(x.Value, y.Value);
        }


        public override bool Parse(byte[] Data, ref int offset)
        {

            if(offset >= Data.Length)
            {
                return false;
            }

            var type = Data.ReadByte(ref offset);

            return InternalParseValue(Data, ref offset, type);
        }

        protected abstract bool InternalParseValue(byte[] Data, ref int offset, byte type);
    }
}
