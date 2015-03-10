using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Object.Scene.Variables
{
    public class StringId : IComparable<StringId>, IEquatable<StringId>
    {
        private string _file = null;
        private string _id = null;
        private string _fullPath = null;


        public StringId()
        {
            
        }

        public StringId(string fullPath) 
        {
            FullPathPath = fullPath;
        }

        public StringId(string file, string id)
        {
            _file = file;
            _id = id;
            UpdateFullPath();
        }

        public string File
        {
            get { return _file; }
            set
            {
                _file = value; 
                UpdateFullPath();
            }
        }

        public string Id
        {
            get { return _id; }
            set
            {
                _id = value; 
                UpdateFullPath();
            }
        }

        public string FullPathPath
        {
            get { return _fullPath; }
            set
            {
                _fullPath = value; 
                ParseFullPath();
            }
        }


        private void UpdateFullPath()
        {
            _fullPath = string.Format("@{0}:{1}", _file, _id);
        }

        private void ParseFullPath()
        {
            if (FullPathPath == null)
            {
                _file = null;
                _id = null;
            }

            var split = _fullPath.Split(new[] {'@', ':'}, StringSplitOptions.RemoveEmptyEntries);
            if (split.Length == 2)
            {
                _file = split[0];
                _id = split[1];
            }
        }

        public int CompareTo(StringId other)
        {
            if (other == null)
            {
                return 1;
            }

            return FullPathPath.CompareTo(other.FullPathPath);
        }

        public bool Equals(StringId other)
        {
            if (other == null)
            {
                return false;
            }

            return FullPathPath.Equals(other.FullPathPath);
        }

        public override string ToString()
        {
            return FullPathPath;
        }

        public static implicit operator StringId(string data)
        {
            return new StringId(data);
        }

        public static implicit operator string(StringId stringId)
        {
            return stringId.FullPathPath;
        }

    }
}
