using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SWG.Client.Utils;
using Wxv.Swg.Common.Files;

namespace SWG.Client.Object.Templates.Data
{
    public class StringIdData : TemplateBase<string>
    {

        private string _fullString;

        private StringData _file;
        private StringData _stringId;

        public StringData File
        {
            get
            {
                return _file;
            }

            set
            {
                _file = value;
                UpdateFullString();
            }
        }

        public StringData StringId
        {
            get { return _stringId; }
            set
            {
                _stringId = value;
                UpdateFullString();
            }
        }

        public override string Value
        {
            get
            {
                return base.Value;
            }

            set
            {
                //base.Value = value;
                if (string.IsNullOrEmpty(value))
                {
                    _file = null;
                    _stringId = null;
                }
                else
                {
                    var split = value.Split(new[] { '@', ':' }, StringSplitOptions.RemoveEmptyEntries);
                    if (split.Length == 2)
                    {
                        _file = split[0];
                        _stringId = split[1];
                    }
                }

                UpdateFullString();
            }
        }

        public StringIdData() : base(DataTypes.StringId)
        {

        }

        public StringIdData(IFFFile.Node Source, ref int Offset) : base(Source, ref Offset, DataTypes.StringId)
        {

        }

        public StringIdData(byte[] Data, ref int Offset) : base(Data, ref Offset, DataTypes.StringId)
        {

        }

        public override bool Parse(IFFFile.Node Source, ref int offset)
        {
            return Parse(Source.Data, ref offset);
        }

        private void UpdateFullString()
        {
            base.Value = string.Format("@{0}:{1}", _file ?? "", _stringId ?? "");
        }

        protected override bool InternalParseValue(byte[] Data, ref int offset, byte type)
        {
            var file = new StringData();
            var id = new StringData();
            if (!file.Parse(Data, ref offset) || !id.Parse(Data, ref offset))
            {
                return false;
            }

            File = file;
            StringId = id;

            return true;
        }

        public static implicit operator StringIdData(string Value)
        {
            return new StringIdData
            {
                Value = Value
            };
        }

        public static implicit operator string(StringIdData Data)
        {

            if(Data == null || !Data.HasValue)
            {
                return null;
            }

            return Data.Value;
        }

        public override string ToString()
        {
            return HasValue ? Value : base.ToString();
        }
    }
}
