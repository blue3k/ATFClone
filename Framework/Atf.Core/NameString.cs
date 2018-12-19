//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace Sce.Atf
{
    /// <summary>Name string. hold string hash for compaire</summary>
    public struct NameString
    {
        static public NameString Empty = new NameString();


        /// <summary>Constructors</summary>
        /// <param name="nameString">String to initialize</param>
        public NameString(string nameString)
            : this()
        {
            m_NameString = null;
            m_Hash32 = 0;
            m_Hash = 0;
            SetString(nameString);
        }

        public NameString(NameString nameString)
            : this()
        {
            m_NameString = nameString.m_NameString;
            m_Hash32 = nameString.m_Hash32;
            m_Hash = nameString.m_Hash;
        }

        /// <summary>SetString</summary>
        /// <param name="nameString">String to initialize</param>
        public void SetString(string nameString)
        {
            m_NameString = nameString;
            if(m_NameString != null)
            {
                m_Hash32 =  m_NameString.GetHashCode();
                m_Hash = (UInt64)m_NameString.GetHashCode(); //  TODO: use murmur hash or Crc64
            }
            else
            {
                m_Hash32 = 0;
                m_Hash = 0;
            }
        }

        public override bool Equals(object other)
        {
            if (other == null)
                return false;

            if (other.GetType() != GetType())
                return false;

            return m_Hash == ((NameString)other).m_Hash;
        }

        public override int GetHashCode()
        {
            return m_Hash32;
        }

        public static bool operator ==(NameString lhs, NameString rhs)
        {
            if (lhs ==  null)
                return rhs == null;
            return lhs.Equals(rhs);
        }

        public static bool operator !=(NameString lhs, NameString rhs)
        {
            if (lhs == null)
                return rhs == null;
            return !lhs.Equals(rhs);
        }


        public static bool operator ==(NameString lhs, string rhs)
        {
            if (lhs == null)
                return rhs == null;
            return lhs.m_NameString.Equals(rhs);
        }

        public static bool operator !=(NameString lhs, string rhs)
        {
            if (lhs == null)
                return rhs == null;
            return !lhs.m_NameString.Equals(rhs);
        }

        public static bool operator ==(string lhs, NameString rhs)
        {
            if (lhs == null)
                return rhs == null;
            return lhs.Equals(rhs.m_NameString);
        }

        public static bool operator !=(string lhs, NameString rhs)
        {
            if (lhs == null)
                return rhs == null;
            return !lhs.Equals(rhs.m_NameString);
        }




        public static explicit operator string(NameString name)
        {
            return name.m_NameString;
        }

        public override string ToString()
        {
            return m_NameString;
        }

        private string m_NameString;
        private UInt64 m_Hash;
        private int m_Hash32;
    }
}
