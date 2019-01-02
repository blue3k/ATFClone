﻿//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;

namespace Sce.Atf.Dom
{
    /// <summary>
    /// Class that describes the type of an attribute on a DOM node. Attributes can be
    /// primitive values, like int or float, or arrays of primitive values. Attributes
    /// are not always value types; they can be reference types in the case of URIs and
    /// references to DomNodes.</summary>
    public class AttributeType : NamedMetadata
    {
        /// <summary>
        /// Type conversion helper.
        /// TODO: might be better if we have converter approach
        /// </summary>
        /// <param name="stringValue">input string value</param>
        /// <returns>Converted value object</returns>
        public delegate object delParseString(string stringValue);
        public delParseString ValueStringParser
        {
            get; set;
        }

        static AttributeType()
        {
            // These should be match
            //System.Diagnostics.Debug.Assert(Enum.GetValues(typeof(AttributeTypes)).Length == stm_TypeMap.Length);
            stm_TypeNameMap = new Dictionary<string, AttributeType>();

            stm_TypeNameMap.Add("boolean", BooleanType);
            stm_TypeNameMap.Add("Boolean", BooleanType);
            stm_TypeNameMap.Add("booleanarray", BooleanArrayType);
            stm_TypeNameMap.Add("BooleanArray", BooleanArrayType);

            stm_TypeNameMap.Add("int8", Int8Type);
            stm_TypeNameMap.Add("Int8", Int8Type);
            stm_TypeNameMap.Add("int8array", Int8ArrayType);
            stm_TypeNameMap.Add("Int8Array", Int8ArrayType);
            stm_TypeNameMap.Add("uint8", UInt8Type);
            stm_TypeNameMap.Add("UInt8", UInt8Type);
            stm_TypeNameMap.Add("uint8array", UInt8ArrayType);
            stm_TypeNameMap.Add("UInt8Array", UInt8ArrayType);

            stm_TypeNameMap.Add("int16", Int16Type);
            stm_TypeNameMap.Add("Int16", Int16Type);
            stm_TypeNameMap.Add("int16array", Int16ArrayType);
            stm_TypeNameMap.Add("Int16Array", Int16ArrayType);
            stm_TypeNameMap.Add("uint16", UInt16Type);
            stm_TypeNameMap.Add("UInt16", UInt16Type);
            stm_TypeNameMap.Add("uint16array", UInt16ArrayType);
            stm_TypeNameMap.Add("UInt16Array", UInt16ArrayType);

            stm_TypeNameMap.Add("int32", IntType);
            stm_TypeNameMap.Add("Int32", IntType);
            stm_TypeNameMap.Add("int32array", IntArrayType);
            stm_TypeNameMap.Add("Int32Array", IntArrayType);
            stm_TypeNameMap.Add("uint32", UIntType);
            stm_TypeNameMap.Add("UInt32", UIntType);
            stm_TypeNameMap.Add("uint32array", UIntArrayType);
            stm_TypeNameMap.Add("UInt32Array", UIntArrayType);

            stm_TypeNameMap.Add("int", IntType);
            stm_TypeNameMap.Add("Int", IntType);
            stm_TypeNameMap.Add("intarray", IntArrayType);
            stm_TypeNameMap.Add("IntArray", IntArrayType);
            stm_TypeNameMap.Add("uint", UIntType);
            stm_TypeNameMap.Add("UInt", UIntType);
            stm_TypeNameMap.Add("uintarray", UIntArrayType);
            stm_TypeNameMap.Add("UIntArray", UIntArrayType);
            stm_TypeNameMap.Add("int64", Int64Type);
            stm_TypeNameMap.Add("Int64", Int64Type);
            stm_TypeNameMap.Add("int64array", Int64ArrayType);
            stm_TypeNameMap.Add("Int64Array", Int64ArrayType);
            stm_TypeNameMap.Add("uint64", UInt64Type);
            stm_TypeNameMap.Add("UInt64", UInt64Type);
            stm_TypeNameMap.Add("uint64array", UInt64ArrayType);
            stm_TypeNameMap.Add("UInt64Array", UInt64ArrayType);
            stm_TypeNameMap.Add("float", FloatType);
            stm_TypeNameMap.Add("Float", FloatType);
            stm_TypeNameMap.Add("floatarray", FloatArrayType);
            stm_TypeNameMap.Add("FloatArray", FloatArrayType);
            stm_TypeNameMap.Add("single", FloatType);
            stm_TypeNameMap.Add("Single", FloatType);
            stm_TypeNameMap.Add("singlearray", FloatArrayType);
            stm_TypeNameMap.Add("SingleArray", FloatArrayType);
            stm_TypeNameMap.Add("double", DoubleType);
            stm_TypeNameMap.Add("Double", DoubleType);
            stm_TypeNameMap.Add("doublearray", DoubleArrayType);
            stm_TypeNameMap.Add("DoubleArray", DoubleArrayType);

            stm_TypeNameMap.Add("decimal", DecimalType);
            stm_TypeNameMap.Add("Decimal", DecimalType);
            stm_TypeNameMap.Add("decimalarray", DecimalArrayType);
            stm_TypeNameMap.Add("DecimalArray", DecimalArrayType);

            stm_TypeNameMap.Add("string", StringType);
            stm_TypeNameMap.Add("String", StringType);
            stm_TypeNameMap.Add("stringarray", StringArrayType);
            stm_TypeNameMap.Add("StringArray", StringArrayType);
            stm_TypeNameMap.Add("namestring", NameStringType);
            stm_TypeNameMap.Add("NameString", NameStringType);
            stm_TypeNameMap.Add("namestringarray", NameStringArrayType);
            stm_TypeNameMap.Add("NameStringArray", NameStringArrayType);

            stm_TypeNameMap.Add("datetime", DateTimeType);
            stm_TypeNameMap.Add("DateTime", DateTimeType);
            //stm_TypeNameMap.Add("datetimearray", DateTimeArrayType);
            //stm_TypeNameMap.Add("DateTimeArray", DateTimeArrayType);

            stm_TypeNameMap.Add("uri", UriType);
            stm_TypeNameMap.Add("Uri", UriType);
            //stm_TypeNameMap.Add("uriarray", UriArrayType);
            //stm_TypeNameMap.Add("UriArray", UriArrayType);

            stm_TypeNameMap.Add("reference", DomNodeRefType);
            stm_TypeNameMap.Add("Reference", DomNodeRefType);
            stm_TypeNameMap.Add(DomNodeRefType.Name, DomNodeRefType);
            //stm_TypeNameMap.Add("referencearray", ReferenceArrayType);
            //stm_TypeNameMap.Add("ReferenceArray", ReferenceArrayType);
        }

        /// <summary>
        /// Constructs a simple type or an array of length 1 of a simple type</summary>
        /// <param name="name">Type name</param>
        /// <param name="type">CLR type. If this is an array type, then the length will be 1</param>
        public AttributeType(string name, Type type)
            : this(name, type, 1)
        {
        }

        /// <summary>
        /// Initializes an instance of a scalar or array simple type</summary>
        /// <param name="name">Type name</param>
        /// <param name="type">CLR type</param>
        /// <param name="length">Must be 1 for a scalar type. For an array type, this is
        /// the length of array. For an unspecified length, use Int32.MaxValue</param>
        public AttributeType(string name, Type type, int length)
            : base(name)
        {
            m_length = length;
            m_clrType = type;

            // translate type into our internal types
            Type valueType = type;
            bool isArray = type.IsArray;
            if (isArray)
                valueType = type.GetElementType();

            if (length != 1 && isArray == false)
                throw new InvalidOperationException("The length of an AttributeType must be 1 for CLR types that are scalars. AttributeType: " + name);

            if (valueType == typeof(SByte))
            {
                m_type = isArray ? AttributeTypes.Int8Array : AttributeTypes.Int8;
            }
            else if (valueType == typeof(Byte))
            {
                m_type = isArray ? AttributeTypes.UInt8Array : AttributeTypes.UInt8;
            }
            else if (valueType == typeof(Int16))
            {
                m_type = isArray ? AttributeTypes.Int16Array : AttributeTypes.Int16;
            }
            else if (valueType == typeof(UInt16))
            {
                m_type = isArray ? AttributeTypes.UInt16Array : AttributeTypes.UInt16;
            }
            else if (valueType == typeof(Int32))
            {
                m_type = isArray ? AttributeTypes.Int32Array : AttributeTypes.Int32;
            }
            else if (valueType == typeof(UInt32))
            {
                m_type = isArray ? AttributeTypes.UInt32Array : AttributeTypes.UInt32;
            }
            else if (valueType == typeof(Int64))
            {
                m_type = isArray ? AttributeTypes.Int64Array : AttributeTypes.Int64;
            }
            else if (valueType == typeof(UInt64))
            {
                m_type = isArray ? AttributeTypes.UInt64Array : AttributeTypes.UInt64;
            }
            else if (valueType == typeof(Single))
            {
                m_type = isArray ? AttributeTypes.SingleArray : AttributeTypes.Single;
            }
            else if (valueType == typeof(Double))
            {
                m_type = isArray ? AttributeTypes.DoubleArray : AttributeTypes.Double;
            }
            else if (valueType == typeof(Decimal))
            {
                m_type = isArray ? AttributeTypes.DecimalArray : AttributeTypes.Decimal;
            }
            else if (valueType == typeof(String))
            {
                m_type = isArray ? AttributeTypes.StringArray : AttributeTypes.String;
            }
            else if (type == typeof(DomNode))
            {
                m_type = AttributeTypes.Reference;
            }
            else if (valueType == typeof(Boolean))
            {
                m_type = isArray ? AttributeTypes.BooleanArray : AttributeTypes.Boolean;
            }
            else if (valueType == typeof(Uri))
            {
                m_type = AttributeTypes.Uri;
            }
            else if (valueType == typeof(DateTime))
            {
                m_type = AttributeTypes.DateTime;
            }
            else if (valueType == typeof(NameString))
            {
                m_type = AttributeTypes.NameString;
            }
            else
            {
                m_clrType = typeof(string); // unrecognized type, so treat it as a string, as in normal DOM
                m_type = AttributeTypes.String;
            }
        }

        /// <summary>
        /// Gets the CLR type of the simple type</summary>
        public Type ClrType
        {
            get { return m_clrType; }
        }

        /// <summary>
        /// Get the SimpleType enum value</summary>
        public AttributeTypes Type
        {
            get { return m_type; }
        }

        /// <summary>
        /// Gets the length of the simple type. Scalar types (i.e., non-arrays) always
        /// have a length of 1.</summary>
        public int Length
        {
            get { return m_length; }
        }

        /// <summary>
        /// Gets a value indicating if this attribute type is an array. This is the same
        /// as testing if the ClrType is an array.</summary>
        public bool IsArray
        {
            get { return m_clrType.IsArray; }
        }


        /// <summary>
        /// Gets or sets the attribute rules for this type, which constrain the allowable
        /// values</summary>
        public IEnumerable<AttributeRule> Rules
        {
            get
            {
                if (m_rules != null)
                    return m_rules;

                return EmptyEnumerable<AttributeRule>.Instance;
            }
        }

        /// <summary>
        /// Adds a rule to the attribute type</summary>
        /// <param name="rule">Rule, constraining the attribute value</param>
        public void AddRule(AttributeRule rule)
        {
            if (rule == null)
                throw new ArgumentNullException("rule");

            if (m_rules == null)
                m_rules = new List<AttributeRule>();
            m_rules.Add(rule);
        }

        public AttributeRuleT GetRule<AttributeRuleT>() where AttributeRuleT : AttributeRule
        {
            foreach (var rule in Rules)
            {
                var myRule = rule as AttributeRuleT;
                if (myRule != null) return myRule;
            }
            return null;
        }

        /// <summary>
        /// Validates a value for assignment to attribute of this type</summary>
        /// <param name="value">Value, to be assigned</param>
        /// <param name="info">Info for particular attribute instance, or null</param>
        /// <returns>True, iff value can be assigned to attribute</returns>
        public virtual bool Validate(object value, AttributeInfo info)
        {
            if (value != null &&
                !m_clrType.IsAssignableFrom(value.GetType()))
            {
                return false;
            }

            if (m_rules != null)
            {
                foreach (AttributeRule rule in m_rules)
                    if (!rule.Validate(value, info))
                        return false;
            }

            return true;
        }

        /// <summary>
        /// Gets a default value for instances of the simple type</summary>
        /// <returns>Default value for instances of the simple type</returns>
        public object GetDefault()
        {
            object result = null;
            switch (m_type)
            {
                case AttributeTypes.Boolean:
                    result = s_defaultBoolean;
                    break;

                case AttributeTypes.BooleanArray:
                    result = GetDefaultArrayValue<Boolean>();
                    break;

                case AttributeTypes.Int8:
                    result = s_defaultInt8;
                    break;

                case AttributeTypes.Int8Array:
                    result = GetDefaultArrayValue<SByte>();
                    break;

                case AttributeTypes.UInt8:
                    result = s_defaultUInt8;
                    break;

                case AttributeTypes.UInt8Array:
                    result = GetDefaultArrayValue<Byte>();
                    break;

                case AttributeTypes.Int16:
                    result = s_defaultInt16;
                    break;

                case AttributeTypes.Int16Array:
                    result = GetDefaultArrayValue<Int16>();
                    break;

                case AttributeTypes.UInt16:
                    result = s_defaultUInt16;
                    break;

                case AttributeTypes.UInt16Array:
                    result = GetDefaultArrayValue<UInt16>();
                    break;

                case AttributeTypes.Int32:
                    result = s_defaultInt32;
                    break;

                case AttributeTypes.Int32Array:
                    result = GetDefaultArrayValue<Int32>();
                    break;

                case AttributeTypes.UInt32:
                    result = s_defaultUInt32;
                    break;

                case AttributeTypes.UInt32Array:
                    result = GetDefaultArrayValue<UInt32>();
                    break;

                case AttributeTypes.Int64:
                    result = s_defaultInt64;
                    break;

                case AttributeTypes.Int64Array:
                    result = GetDefaultArrayValue<Int64>();
                    break;

                case AttributeTypes.UInt64:
                    result = s_defaultUInt64;
                    break;

                case AttributeTypes.UInt64Array:
                    result = GetDefaultArrayValue<UInt64>();
                    break;

                case AttributeTypes.Single:
                    result = s_defaultSingle;
                    break;

                case AttributeTypes.SingleArray:
                    result = GetDefaultArrayValue<Single>();
                    break;

                case AttributeTypes.Double:
                    result = s_defaultDouble;
                    break;

                case AttributeTypes.DoubleArray:
                    result = GetDefaultArrayValue<Double>();
                    break;

                case AttributeTypes.Decimal:
                    result = s_defaultDecimal;
                    break;

                case AttributeTypes.DecimalArray:
                    result = GetDefaultArrayValue<Decimal>();
                    break;

                case AttributeTypes.String:
                    result = string.Empty;
                    break;

                case AttributeTypes.StringArray:
                    result = GetDefaultArrayValue<String>();
                    break;

                case AttributeTypes.NameString:
                    result = NameString.Empty;
                    break;

                case AttributeTypes.Reference:
                    break;

                //case AttributeTypes.Uri: // null, no good default value for URI
                //    break;

                case AttributeTypes.DateTime:
                    result = s_defaultDateTime;
                    break;
            }

            return result;
        }

        /// <summary>
        /// Tests two simple type values for equality</summary>
        /// <param name="val1">First value, must be of this type</param>
        /// <param name="val2">Second value, must be of this type</param>
        /// <returns>True, iff first value equals second value</returns>
        public bool AreEqual(object val1, object val2)
        {
            if (val1 == null)
                return val2 == null;
            // val1 is non-null

            bool result = false;
            switch (m_type)
            {
                case AttributeTypes.Boolean:
                case AttributeTypes.Int8:
                case AttributeTypes.UInt8:
                case AttributeTypes.Int16:
                case AttributeTypes.UInt16:
                case AttributeTypes.Int32:
                case AttributeTypes.UInt32:
                case AttributeTypes.Int64:
                case AttributeTypes.UInt64:
                case AttributeTypes.Single:
                case AttributeTypes.Double:
                case AttributeTypes.Decimal:
                case AttributeTypes.String:
                case AttributeTypes.Reference:
                case AttributeTypes.Uri:
                case AttributeTypes.DateTime:
                    result = val1.Equals(val2);
                    break;

                case AttributeTypes.BooleanArray:
                    result = AreEqualArraysOf<Boolean>(val1, val2);
                    break;

                case AttributeTypes.Int8Array:
                    result = AreEqualArraysOf<SByte>(val1, val2);
                    break;

                case AttributeTypes.UInt8Array:
                    result = AreEqualArraysOf<Byte>(val1, val2);
                    break;

                case AttributeTypes.Int16Array:
                    result = AreEqualArraysOf<Int16>(val1, val2);
                    break;

                case AttributeTypes.UInt16Array:
                    result = AreEqualArraysOf<UInt16>(val1, val2);
                    break;

                case AttributeTypes.Int32Array:
                    result = AreEqualArraysOf<Int32>(val1, val2);
                    break;

                case AttributeTypes.UInt32Array:
                    result = AreEqualArraysOf<UInt32>(val1, val2);
                    break;

                case AttributeTypes.Int64Array:
                    result = AreEqualArraysOf<Int64>(val1, val2);
                    break;

                case AttributeTypes.UInt64Array:
                    result = AreEqualArraysOf<UInt64>(val1, val2);
                    break;

                case AttributeTypes.SingleArray:
                    result = AreEqualArraysOf<Single>(val1, val2);
                    break;

                case AttributeTypes.DoubleArray:
                    result = AreEqualArraysOf<Double>(val1, val2);
                    break;

                case AttributeTypes.DecimalArray:
                    result = AreEqualArraysOf<Decimal>(val1, val2);
                    break;

                case AttributeTypes.StringArray:
                    result = AreEqualArraysOf<String>(val1, val2);
                    break;

                case AttributeTypes.NameString:
                    result = val1.Equals(val2);
                    break;
            }

            return result;
        }


        /// <summary>
        /// Clones a value of the simple type</summary>
        /// <param name="value">Value to clone</param>
        /// <returns>Cloned value of simple type</returns>
        public object Clone(object value)
        {
            object result = null;
            if (value != null)
            {
                switch (m_type)
                {
                    case AttributeTypes.Boolean:
                    case AttributeTypes.Int8:
                    case AttributeTypes.UInt8:
                    case AttributeTypes.Int16:
                    case AttributeTypes.UInt16:
                    case AttributeTypes.Int32:
                    case AttributeTypes.UInt32:
                    case AttributeTypes.Int64:
                    case AttributeTypes.UInt64:
                    case AttributeTypes.Single:
                    case AttributeTypes.Double:
                    case AttributeTypes.Decimal:
                    case AttributeTypes.String:
                    case AttributeTypes.Reference:
                    case AttributeTypes.Uri:
                    case AttributeTypes.DateTime:
                        result = value; // immutable types don't need to be cloned
                        break;

                    case AttributeTypes.BooleanArray:
                        result = ((Boolean[])value).Clone();
                        break;

                    case AttributeTypes.Int8Array:
                        result = ((SByte[])value).Clone();
                        break;

                    case AttributeTypes.UInt8Array:
                        result = ((Byte[])value).Clone();
                        break;

                    case AttributeTypes.Int16Array:
                        result = ((Int16[])value).Clone();
                        break;

                    case AttributeTypes.UInt16Array:
                        result = ((UInt16[])value).Clone();
                        break;

                    case AttributeTypes.Int32Array:
                        result = ((Int32[])value).Clone();
                        break;

                    case AttributeTypes.UInt32Array:
                        result = ((UInt32[])value).Clone();
                        break;

                    case AttributeTypes.Int64Array:
                        result = ((Int64[])value).Clone();
                        break;

                    case AttributeTypes.UInt64Array:
                        result = ((UInt64[])value).Clone();
                        break;

                    case AttributeTypes.SingleArray:
                        result = ((Single[])value).Clone();
                        break;

                    case AttributeTypes.DoubleArray:
                        result = ((Double[])value).Clone();
                        break;

                    case AttributeTypes.DecimalArray:
                        result = ((Decimal[])value).Clone();
                        break;

                    case AttributeTypes.StringArray:
                        result = ((String[])value).Clone();
                        break;

                    case AttributeTypes.NameString:
                        result = new NameString((NameString)value);
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// Converts an instance of the simple type to a string</summary>
        /// <param name="value">Instance of simple type</param>
        /// <returns>String representation of instance</returns>
        public virtual string Convert(object value)
        {
            StringBuilder sb;
            string result = string.Empty;
            switch (m_type)
            {
                case AttributeTypes.Int8:
                case AttributeTypes.UInt8:
                case AttributeTypes.Int16:
                case AttributeTypes.UInt16:
                case AttributeTypes.Int32:
                case AttributeTypes.UInt32:
                case AttributeTypes.Int64:
                case AttributeTypes.UInt64:
                case AttributeTypes.Decimal:
                    result = ((IFormattable)value).ToString(null, CultureInfo.InvariantCulture);
                    break;
                case AttributeTypes.DateTime:
                    // Persist in UTC.
                    DateTime utc = ((DateTime)value).ToUniversalTime();
                    result = utc.ToString("o", DateTimeFormatInfo.InvariantInfo); //round-trip
                    break;
                case AttributeTypes.Single:
                case AttributeTypes.Double:
                    result = ((IFormattable)value).ToString("R", CultureInfo.InvariantCulture);
                    break;

                case AttributeTypes.String:
                    result = value.ToString();
                    break;

                case AttributeTypes.NameString:
                    result = value.ToString();
                    break;

                case AttributeTypes.Uri:
                    result = Uri.EscapeUriString(Uri.UnescapeDataString(value.ToString()));
                    break;

                // references require special handling by persisters
                // However, it is good if we have generalized way. We are going to use unique name mapping as default implementation
                case AttributeTypes.Reference: 
                    result = ((DomNode)value).GetId();
                    break;

                // we need to convert "True" and "False" to lower case to be valid xs:boolean values
                case AttributeTypes.Boolean:
                    result = (bool)value ? "true" : "false";
                    break;

                case AttributeTypes.Int8Array:
                    result = Convert<SByte>(value);
                    break;

                case AttributeTypes.UInt8Array:
                    result = Convert<Byte>(value);
                    break;

                case AttributeTypes.Int16Array:
                    result = Convert<Int16>(value);
                    break;

                case AttributeTypes.UInt16Array:
                    result = Convert<UInt16>(value);
                    break;

                case AttributeTypes.Int32Array:
                    result = Convert<Int32>(value);
                    break;

                case AttributeTypes.UInt32Array:
                    result = Convert<UInt32>(value);
                    break;

                case AttributeTypes.Int64Array:
                    result = Convert<Int64>(value);
                    break;

                case AttributeTypes.UInt64Array:
                    result = Convert<UInt64>(value);
                    break;

                case AttributeTypes.SingleArray:
                    result = Convert<Single>(value);
                    break;

                case AttributeTypes.DoubleArray:
                    result = Convert<Double>(value);
                    break;

                case AttributeTypes.DecimalArray:
                    result = Convert<Decimal>(value);
                    break;

                case AttributeTypes.StringArray:
                    result = ConvertStringArrayToString((string[])value);
                    break;

                case AttributeTypes.BooleanArray:
                    sb = new StringBuilder();
                    foreach (bool b in value as bool[])
                    {
                        sb.Append(b ? "true" : "false");
                        sb.Append(" ");
                    }
                    if (sb.Length > 0)
                        sb.Length -= 1; // remove trailing space
                    result = sb.ToString();
                    break;
                default:
                    throw new InvalidCastException("We don't know how to convert it");
            }

            return result;
        }

        /// <summary>
        /// Converts string to instance of simple type</summary>
        /// <param name="s">Input string</param>
        /// <returns>Instance of simple type or null, if the conversion failed</returns>
        public virtual object Convert(string s)
        {
            object result = null;
            string[] strings;
            int stringsToParse;

            switch (m_type)
            {
                case AttributeTypes.Boolean:
                    Boolean boolResult;
                    if (Boolean.TryParse(s, out boolResult))
                        result = boolResult;
                    break;

                case AttributeTypes.Int8:
                    SByte int8Result;
                    if (SByte.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out int8Result))
                        result = int8Result;
                    break;

                case AttributeTypes.UInt8:
                    Byte uint8Result;
                    if (Byte.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out uint8Result))
                        result = uint8Result;
                    break;

                case AttributeTypes.Int16:
                    Int16 int16Result;
                    if (Int16.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out int16Result))
                        result = int16Result;
                    break;

                case AttributeTypes.UInt16:
                    UInt16 uint16Result;
                    if (UInt16.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out uint16Result))
                        result = uint16Result;
                    break;

                case AttributeTypes.Int32:
                    Int32 int32Result;
                    if (Int32.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out int32Result))
                        result = int32Result;
                    break;

                case AttributeTypes.UInt32:
                    UInt32 uint32Result;
                    if (UInt32.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out uint32Result))
                        result = uint32Result;
                    break;

                case AttributeTypes.Int64:
                    Int64 int64Result;
                    if (Int64.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out int64Result))
                        result = int64Result;
                    break;

                case AttributeTypes.UInt64:
                    UInt64 uint64Result;
                    if (UInt64.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out uint64Result))
                        result = uint64Result;
                    break;

                case AttributeTypes.Single:
                    Single singleResult;
                    if (Single.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out singleResult))
                        result = singleResult;
                    break;

                case AttributeTypes.Double:
                    Double doubleResult;
                    if (Double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out doubleResult))
                        result = doubleResult;
                    break;

                case AttributeTypes.Decimal:
                    Decimal decimalResult;
                    if (Decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out decimalResult))
                        result = decimalResult;
                    break;

                case AttributeTypes.String:
                    result = s;
                    break;

                case AttributeTypes.NameString:
                    result = new NameString(s);
                    break;

                case AttributeTypes.Uri:
                    Uri uri;
                    Uri.TryCreate(s, UriKind.RelativeOrAbsolute, out uri);
                    result = uri;
                    break;

                // references require special handling by persisters
                // However, it is good if we have generalized way. We are going to use unique name mapping as default implementation
                case AttributeTypes.Reference:
                    if (ValueStringParser != null)
                        result = ValueStringParser(s);
                    break;

                case AttributeTypes.DateTime:
                    // Use local time in the client app.
                    DateTime dateTimeResult;
                    if (DateTime.TryParse(s, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.RoundtripKind, out dateTimeResult))
                        dateTimeResult = dateTimeResult.ToLocalTime();
                    result = dateTimeResult;
                    break;

                case AttributeTypes.BooleanArray:
                    Boolean[] booleans = CreateArrayValue<Boolean>(s, out strings, out stringsToParse);
                    for (int i = 0; i < stringsToParse; i++)
                        Boolean.TryParse(strings[i], out booleans[i]);
                    result = booleans;
                    break;

                case AttributeTypes.Int8Array:
                    SByte[] int8s = CreateArrayValue<SByte>(s, out strings, out stringsToParse);
                    for (int i = 0; i < stringsToParse; i++)
                        SByte.TryParse(strings[i], NumberStyles.Integer, CultureInfo.InvariantCulture, out int8s[i]);
                    result = int8s;
                    break;

                case AttributeTypes.UInt8Array:
                    Byte[] uint8s = CreateArrayValue<Byte>(s, out strings, out stringsToParse);
                    for (int i = 0; i < stringsToParse; i++)
                        Byte.TryParse(strings[i], NumberStyles.Integer, CultureInfo.InvariantCulture, out uint8s[i]);
                    result = uint8s;
                    break;

                case AttributeTypes.Int16Array:
                    Int16[] int16s = CreateArrayValue<Int16>(s, out strings, out stringsToParse);
                    for (int i = 0; i < stringsToParse; i++)
                        Int16.TryParse(strings[i], NumberStyles.Integer, CultureInfo.InvariantCulture, out int16s[i]);
                    result = int16s;
                    break;

                case AttributeTypes.UInt16Array:
                    UInt16[] uint16s = CreateArrayValue<UInt16>(s, out strings, out stringsToParse);
                    for (int i = 0; i < stringsToParse; i++)
                        UInt16.TryParse(strings[i], NumberStyles.Integer, CultureInfo.InvariantCulture, out uint16s[i]);
                    result = uint16s;
                    break;

                case AttributeTypes.Int32Array:
                    Int32[] int32s = CreateArrayValue<Int32>(s, out strings, out stringsToParse);
                    for (int i = 0; i < stringsToParse; i++)
                        Int32.TryParse(strings[i], NumberStyles.Integer, CultureInfo.InvariantCulture, out int32s[i]);
                    result = int32s;
                    break;

                case AttributeTypes.UInt32Array:
                    UInt32[] uint32s = CreateArrayValue<UInt32>(s, out strings, out stringsToParse);
                    for (int i = 0; i < stringsToParse; i++)
                        UInt32.TryParse(strings[i], NumberStyles.Integer, CultureInfo.InvariantCulture, out uint32s[i]);
                    result = uint32s;
                    break;

                case AttributeTypes.Int64Array:
                    Int64[] int64s = CreateArrayValue<Int64>(s, out strings, out stringsToParse);
                    for (int i = 0; i < stringsToParse; i++)
                        Int64.TryParse(strings[i], NumberStyles.Integer, CultureInfo.InvariantCulture, out int64s[i]);
                    result = int64s;
                    break;

                case AttributeTypes.UInt64Array:
                    UInt64[] uint64s = CreateArrayValue<UInt64>(s, out strings, out stringsToParse);
                    for (int i = 0; i < stringsToParse; i++)
                        UInt64.TryParse(strings[i], NumberStyles.Integer, CultureInfo.InvariantCulture, out uint64s[i]);
                    result = uint64s;
                    break;

                case AttributeTypes.SingleArray:
                    Single[] singles = CreateArrayValue<Single>(s, out strings, out stringsToParse);
                    for (int i = 0; i < stringsToParse; i++)
                        Single.TryParse(strings[i], NumberStyles.Float, CultureInfo.InvariantCulture, out singles[i]);
                    result = singles;
                    break;

                case AttributeTypes.DoubleArray:
                    Double[] doubles = CreateArrayValue<Double>(s, out strings, out stringsToParse);
                    for (int i = 0; i < stringsToParse; i++)
                        Double.TryParse(strings[i], NumberStyles.Float, CultureInfo.InvariantCulture, out doubles[i]);
                    result = doubles;
                    break;

                case AttributeTypes.DecimalArray:
                    Decimal[] decimals = CreateArrayValue<Decimal>(s, out strings, out stringsToParse);
                    for (int i = 0; i < stringsToParse; i++)
                        Decimal.TryParse(strings[i], NumberStyles.Number, CultureInfo.InvariantCulture, out decimals[i]);
                    result = decimals;
                    break;

                case AttributeTypes.StringArray:
                    result = ConvertStringToStringArray(s);
                    break;

                default:
                    throw new InvalidCastException("We don't know how to convert it");
            }

            return result;
        }

        /// <summary>
        /// Gets a generic AttributeType that specifies a CLR string</summary>
        public static AttributeType StringType
        {
            get { return s_stringType; }
        }
        private static readonly AttributeType s_stringType = new AttributeType("string", typeof(string), 1);

        /// <summary>
        /// Gets a generic AttributeType that specifies a CLR string</summary>
        public static AttributeType StringArrayType
        {
            get { return s_stringArrayType; }
        }
        private static readonly AttributeType s_stringArrayType = new AttributeType("string[]", typeof(string[]), Int32.MaxValue);

        /// <summary>
        /// Gets a generic AttributeType that specifies a CLR int</summary>
        public static AttributeType Int8Type
        {
            get { return s_int8Type; }
        }
        private static readonly AttributeType s_int8Type = new AttributeType("Int8", typeof(sbyte), 1);

        /// <summary>
        /// Gets a generic AttributeType that specifies a CLR int</summary>
        public static AttributeType Int8ArrayType
        {
            get { return s_int8ArrayType; }
        }
        private static readonly AttributeType s_int8ArrayType = new AttributeType("int8[]", typeof(sbyte[]), Int32.MaxValue);

        /// <summary>
        /// Gets a generic AttributeType that specifies a CLR int</summary>
        public static AttributeType UInt8Type
        {
            get { return s_uint8Type; }
        }
        private static readonly AttributeType s_uint8Type = new AttributeType("UInt8", typeof(byte), 1);

        /// <summary>
        /// Gets a generic AttributeType that specifies a CLR int</summary>
        public static AttributeType UInt8ArrayType
        {
            get { return s_uint8ArrayType; }
        }
        private static readonly AttributeType s_uint8ArrayType = new AttributeType("uint8[]", typeof(byte[]), Int32.MaxValue);

        /// <summary>
        /// Gets a generic AttributeType that specifies a CLR int</summary>
        public static AttributeType Int16Type
        {
            get { return s_int16Type; }
        }
        private static readonly AttributeType s_int16Type = new AttributeType("Int16", typeof(Int16), 1);

        /// <summary>
        /// Gets a generic AttributeType that specifies a CLR int</summary>
        public static AttributeType Int16ArrayType
        {
            get { return s_int16ArrayType; }
        }
        private static readonly AttributeType s_int16ArrayType = new AttributeType("int16[]", typeof(Int16[]), Int32.MaxValue);

        /// <summary>
        /// Gets a generic AttributeType that specifies a CLR int</summary>
        public static AttributeType UInt16Type
        {
            get { return s_uint16Type; }
        }
        private static readonly AttributeType s_uint16Type = new AttributeType("UInt16", typeof(UInt16), 1);

        /// <summary>
        /// Gets a generic AttributeType that specifies a CLR int</summary>
        public static AttributeType UInt16ArrayType
        {
            get { return s_uint16ArrayType; }
        }
        private static readonly AttributeType s_uint16ArrayType = new AttributeType("uint16[]", typeof(UInt16[]), Int32.MaxValue);

        /// <summary>
        /// Gets a generic AttributeType that specifies a CLR int</summary>
        public static AttributeType IntType
        {
            get { return s_intType; }
        }
        private static readonly AttributeType s_intType = new AttributeType("int", typeof(int), 1);

        /// <summary>
        /// Gets a generic AttributeType that specifies a CLR int</summary>
        public static AttributeType IntArrayType
        {
            get { return s_intArrayType; }
        }
        private static readonly AttributeType s_intArrayType = new AttributeType("int[]", typeof(int[]), Int32.MaxValue);

        /// <summary>
        /// Gets a generic AttributeType that specifies a CLR int</summary>
        public static AttributeType UIntType
        {
            get { return s_uintType; }
        }
        private static readonly AttributeType s_uintType = new AttributeType("UInt", typeof(UInt32), 1);

        /// <summary>
        /// Gets a generic AttributeType that specifies a CLR int</summary>
        public static AttributeType UIntArrayType
        {
            get { return s_uintArrayType; }
        }
        private static readonly AttributeType s_uintArrayType = new AttributeType("uint[]", typeof(UInt32[]), Int32.MaxValue);

        /// <summary>
        /// Gets a generic AttributeType that specifies a CLR int</summary>
        public static AttributeType Int64Type
        {
            get { return s_int64Type; }
        }
        private static readonly AttributeType s_int64Type = new AttributeType("Int64", typeof(Int64), 1);

        /// <summary>
        /// Gets a generic AttributeType that specifies a CLR int</summary>
        public static AttributeType Int64ArrayType
        {
            get { return s_int64ArrayType; }
        }
        private static readonly AttributeType s_int64ArrayType = new AttributeType("int64[]", typeof(Int64[]), Int32.MaxValue);

        /// <summary>
        /// Gets a generic AttributeType that specifies a CLR int</summary>
        public static AttributeType UInt64Type
        {
            get { return s_uint64Type; }
        }
        private static readonly AttributeType s_uint64Type = new AttributeType("UInt64", typeof(UInt64), 1);

        /// <summary>
        /// Gets a generic AttributeType that specifies a CLR int</summary>
        public static AttributeType UInt64ArrayType
        {
            get { return s_uint64ArrayType; }
        }
        private static readonly AttributeType s_uint64ArrayType = new AttributeType("uint64[]", typeof(UInt64[]), Int32.MaxValue);

        /// <summary>
        /// Gets a generic AttributeType that specifies a CLR float</summary>
        public static AttributeType FloatType
        {
            get { return s_floatType; }
        }
        private static readonly AttributeType s_floatType = new AttributeType("float", typeof(float), 1);

        /// <summary>
        /// Gets a generic AttributeType that specifies a CLR float</summary>
        public static AttributeType FloatArrayType
        {
            get { return s_floatArrayType; }
        }
        private static readonly AttributeType s_floatArrayType = new AttributeType("float[]", typeof(float[]), Int32.MaxValue);

        /// <summary>
        /// Gets a generic AttributeType that specifies a CLR float</summary>
        public static AttributeType DoubleType
        {
            get { return s_doubleType; }
        }
        private static readonly AttributeType s_doubleType = new AttributeType("double", typeof(double), 1);

        /// <summary>
        /// Gets a generic AttributeType that specifies a CLR float</summary>
        public static AttributeType DoubleArrayType
        {
            get { return s_doubleArrayType; }
        }
        private static readonly AttributeType s_doubleArrayType = new AttributeType("double[]", typeof(double[]), Int32.MaxValue);

        /// <summary>
        /// Gets a generic AttributeType that specifies a CLR decimal</summary>
        public static AttributeType DecimalType
        {
            get { return s_decimalType; }
        }
        private static readonly AttributeType s_decimalType = new AttributeType("decimal", typeof(decimal), 1);

        /// <summary>
        /// Gets a generic AttributeType that specifies a CLR decimal</summary>
        public static AttributeType DecimalArrayType
        {
            get { return s_decimalArrayType; }
        }
        private static readonly AttributeType s_decimalArrayType = new AttributeType("decimal[]", typeof(decimal[]), Int32.MaxValue);

        /// <summary>
        /// Gets a generic AttributeType that specifies a CLR Boolean</summary>
        public static AttributeType BooleanType
        {
            get { return s_booleanType; }
        }
        private static readonly AttributeType s_booleanType = new AttributeType("boolean", typeof(bool), 1);

        /// <summary>
        /// Gets a generic AttributeType that specifies a CLR Boolean</summary>
        public static AttributeType BooleanArrayType
        {
            get { return s_booleanArrayType; }
        }
        private static readonly AttributeType s_booleanArrayType = new AttributeType("boolean[]", typeof(bool[]), Int32.MaxValue);


        /// <summary>
        /// Name string type
        /// </summary>
        public static AttributeType NameStringType
        {
            get { return s_nameStringType; }
        }
        private static readonly AttributeType s_nameStringType = new AttributeType("NameString", typeof(NameString), 1);

        /// <summary>
        /// name string array
        /// </summary>
        public static AttributeType NameStringArrayType
        {
            get { return s_nameStringArrayType; }
        }
        private static readonly AttributeType s_nameStringArrayType = new AttributeType("NameString[]", typeof(NameString[]), Int32.MaxValue);

        /// <summary>
        /// Type for DateTime
        /// </summary>
        public static AttributeType DateTimeType
        {
            get { return s_dateTimeType; }
        }
        private static readonly AttributeType s_dateTimeType = new AttributeType("DateTime", typeof(DateTime), 1);

        /// <summary>
        /// Gets a generic AttributeType that specifies a CLR string</summary>
        public static AttributeType UriType
        {
            get { return s_uriType; }
        }
        private static readonly AttributeType s_uriType = new AttributeType("uri", typeof(Uri), 1);

        /// <summary>
        /// Type for DomNodeRef
        /// </summary>
        public static AttributeType DomNodeRefType
        {
            get { return s_domNodeRefType; }
        }
        private static readonly AttributeType s_domNodeRefType = new AttributeType("DomNodeRef", typeof(DomNode), 1);


        private object GetDefaultArrayValue<T>()
        {
            int length = 0;
            if (m_length < Int32.MaxValue)
                length = m_length;

            return new T[length];
        }

        private T[] CreateArrayValue<T>(string s, out string[] strings, out int stringsToParse)
        {
            string[] array = s.Split(s_arraySeparator, StringSplitOptions.RemoveEmptyEntries);
            // restrict length if type defines cardinality
            int length = (m_length < Int32.MaxValue) ? m_length : array.Length;
            strings = array;
            // don't parse excess strings
            stringsToParse = Math.Min(length, array.Length);
            return new T[length];
        }

        private string Convert<T>(object array)
            where T : IFormattable
        {
            IEnumerable<T> values = array as IEnumerable<T>;
            StringBuilder sb = new StringBuilder();
            foreach (T value in values)
            {
                if (typeof(T) == typeof(Single) || typeof(T) == typeof(Double))
                    sb.Append(value.ToString("R", CultureInfo.InvariantCulture));
                else
                    sb.Append(value.ToString(null, CultureInfo.InvariantCulture));
                sb.Append(" ");
            }
            if (sb.Length > 0)
                sb.Length -= 1; // remove trailing space

            return sb.ToString();
        }

        private static bool AreEqualArraysOf<T>(object val1, object val2)
        {
            T[] array1 = val1 as T[];
            T[] array2 = val2 as T[];
            if (array2 == null || array1.Length != array2.Length)
                return false;

            for (int i = 0; i < array1.Length; i++)
                if (!array1[i].Equals(array2[i]))
                    return false;

            return true;
        }

        // Converts an array of strings to a single string in a reversible manner.
        // Complements ConvertStringToStringArray().
        // Process:
        //  Each element, if it contains quotes, has the quotes escaped by inserting a '\' before the '"'.
        //  Each element, if it contains spaces, has quotes (") put at the beginning and end.
        //  The elements are then concatenated together, separated by spaces.
        private static string ConvertStringArrayToString(string[] array)
        {
            var sb = new StringBuilder();
            for(int i = 0; i < array.Length; i++)
            {
                string s = array[i];
                
                if (string.IsNullOrEmpty(s))
                    s = "\"\"";
                else
                    s = s.Replace("\"", "\\\"");

                if (s.Contains(" "))
                {
                    sb.Append('\"');
                    sb.Append(s);
                    sb.Append("\" ");
                }
                else
                {
                    sb.Append(s);
                    sb.Append(' ');
                }
            }
            if (sb.Length > 0)
                sb.Length -= 1; // remove trailing space
            return sb.ToString();
        }

        // Reverses the transformation of ConvertStringArrayToString.
        private static string[] ConvertStringToStringArray(string concatenation)
        {
            List<string> elements = new List<string>();

            // Find the elements enclosed with quotes and separated by spaces
            var element = new StringBuilder();
            for (int index = 0; index < concatenation.Length; index++)
            {
                // Look for a quoted element, terminated by another double-quote
                if (concatenation[index] == '"')
                {
                    index++; // advance past this first '"'
                    char unescaped;
                    while (GetUnescapedChar(concatenation, ref index, '"', out unescaped))
                        element.Append(unescaped);
                    
                    index++; // advance past the '"'
                }
                // Look for non-quoted element, terminated by a space
                else
                {
                    char unescaped;
                    while (GetUnescapedChar(concatenation, ref index, ' ', out unescaped))
                        element.Append(unescaped);
                }

                string finalElement = element.ToString();
                elements.Add(finalElement);
                element.Remove(0, finalElement.Length); //Note: in .Net Framework 4.0, StringBuilder has Clear() method
            }

            return elements.ToArray();
        }

        // Processes one or more characters out of 's' to produce a single unescaped character.  'index'
        //  is incremented by however many characters from 's' are parsed in order to produce 'result'.
        //  If false is returned, 'index' is not incremented.
        private static bool GetUnescapedChar(string s, ref int index, char terminator, out char result)
        {
            // Run out of characters to read or hit terminator? Then don't increment 'index'.
            if (index >= s.Length ||
                s[index] == terminator)
            {
                result = terminator;
                return false;
            }

            // Escaped sequence?
            if (s[index] == '\\' &&
                index + 1 < s.Length &&
                s[index+1] == '"')
            {
                result = '"';
                index += 2;
                return true;
            }

            // Normal one-to-one copy
            result = s[index++];
            return true;
        }

        public static AttributeType GetAttributeType(string typeName)
        {
            AttributeType typeValue;
            if (stm_TypeNameMap.TryGetValue(typeName, out typeValue))
            {
                return typeValue;
            }
            else
            {
                return null;
            }
        }


        private readonly Type m_clrType;
        private readonly AttributeTypes m_type;
        private readonly int m_length;
        private List<AttributeRule> m_rules;

        // declare as object so they will be boxed just once; they are effectively immutable.
        private static readonly object s_defaultBoolean = false;
        private static readonly object s_defaultInt8 = (SByte)0;
        private static readonly object s_defaultUInt8 = (Byte)0;
        private static readonly object s_defaultInt16 = (Int16)0;
        private static readonly object s_defaultUInt16 = (UInt16)0;
        private static readonly object s_defaultInt32 = 0;
        private static readonly object s_defaultUInt32 = 0U;
        private static readonly object s_defaultInt64 = 0L;
        private static readonly object s_defaultUInt64 = 0UL;
        private static readonly object s_defaultSingle = 0F;
        private static readonly object s_defaultDouble = 0D;
        private static readonly object s_defaultDecimal = 0M;

        private static readonly DateTime s_defaultDateTime = new DateTime(0);

        private static readonly char[] s_arraySeparator = new[] { ' ', '\n', '\t' };

        // 
        private static Dictionary<string, AttributeType> stm_TypeNameMap;

    }
}
