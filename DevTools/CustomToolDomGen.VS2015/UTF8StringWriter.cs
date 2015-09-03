

using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using DomGen;

namespace Atf.DomGen
{
    /// <summary>
    /// This is string writer for the generator class. 
    /// </summary>
    public sealed class Utf8StringWriter : StringWriter
    {
        /// <summary>
        /// Override encoding
        /// </summary>
        public Utf8StringWriter(StringBuilder strBuilder)
            : base(strBuilder)
        {

        }

        /// <summary>
        /// Override encoding
        /// </summary>
        public override Encoding Encoding { get { return Encoding.UTF8; } }
    }

}