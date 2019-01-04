//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using System;
using System.Collections.Generic;

namespace Sce.Atf.Dom
{
    /// <summary>
    /// Attribute rule that constrains a string value to one of a set of given string values</summary>
    public class StringEnumRule : AttributeRule
    {
        public StringEnumRule()
        {
            m_values = new List<string>();
        }

        /// <summary>
        /// Constructor</summary>
        /// <param name="values">Enumeration values</param>
        public StringEnumRule(string enumTypeName, string[] values)
            : this(values)
        {
            m_EnumTypeName = enumTypeName;
        }

        /// <summary>
        /// Constructor</summary>
        /// <param name="values">Enumeration values</param>
        public StringEnumRule(IList<string> values)
        {
            if (values == null || values.Count == 0)
                throw new ArgumentException("values must be an array with at least one element");

            m_values = values;
        }

        public void DefineEnum(string value)
        {
            m_values.Add(value);
        }

        /// <summary>
        /// Validates the given value for assignment to the given attribute</summary>
        /// <param name="value">Value to validate</param>
        /// <param name="info">Attribute info</param>
        /// <returns>True, iff value is valid for the given attribute</returns>
        public override bool Validate(object value, AttributeInfo info)
        {
            foreach (string s in m_values)
                if (s.Equals(value))
                    return true;

            return false;
        }

        public string EnumTypeName
        {
            get { return m_EnumTypeName; }
        }

        /// <summary>
        /// Gets the enumeration values</summary>
        public IEnumerable<string> Values
        {
            get { return m_values; }
        }

        private readonly string m_EnumTypeName = null;
        private readonly IList<string> m_values;
    }
}
