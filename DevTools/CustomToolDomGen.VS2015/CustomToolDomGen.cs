

using System;
using System.Runtime.InteropServices;
using System.CodeDom.Compiler;
using System.CodeDom;
//using EnvDTE;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
//using Microsoft.Win32;
using Microsoft.VisualStudio.Shell;
using VSLangProj80;
//using VSLangProj140;

namespace DomGen
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


    /// <summary>
    /// This is the generator class. 
    /// When setting the 'Custom Tool' property of a C#, VB, or J# project item to "XmlClassGenerator", 
    /// the GenerateCode function will get called and will return the contents of the generated file 
    /// to the project system
    /// </summary>
    [ComVisible(true)]
    [Guid("3A278187-00BD-4C56-9EF3-068BE7C64D5D")]
    //[Guid("52B316AA-1997-4c81-9969-83604C09EEB4")]
    [CodeGeneratorRegistration(typeof(CustomToolDomGen),"C# XML Class Generator",vsContextGuids.vsContextGuidVCSProject,GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(CustomToolDomGen), "VB XML Class Generator", vsContextGuids.vsContextGuidVBProject, GeneratesDesignTimeSource = true)]
    [CodeGeneratorRegistration(typeof(CustomToolDomGen), "J# XML Class Generator", vsContextGuids.vsContextGuidVJSProject, GeneratesDesignTimeSource = true)]
    [ProvideObject(typeof(CustomToolDomGen))]
    public class CustomToolDomGen : BaseCodeGeneratorWithSite
    {
#pragma warning disable 0414
        //The name of this generator (use for 'Custom Tool' property of project item)
        internal static string name = "CustomToolDomGen";
#pragma warning restore 0414

        /// <summary>
        /// Function that builds the contents of the generated file based on the contents of the input file
        /// </summary>
        /// <param name="inputFileContent">Content of the input file</param>
        /// <returns>Generated file as a byte array</returns>
        protected override byte[] GenerateCode(string inputFileContent)
        {
            try
            {
                // class name should always the same as output file name
                string className = Path.GetFileNameWithoutExtension(inputFileContent);

                SchemaLoader typeLoader = new SchemaLoader();
                typeLoader.Load(inputFileContent);

                string[] fakeArgs = { "CustomToolDomGen", inputFileContent, Path.GetFileNameWithoutExtension(inputFileContent) + ".cs", className, FileNameSpace };

                string bodyString = SchemaGen.Generate(typeLoader, "", FileNameSpace, className, fakeArgs);

                Encoding enc = System.Text.Encoding.UTF8;

                //Get the preamble (byte-order mark) for our encoding
                byte[] preamble = enc.GetPreamble();
                int preambleLength = preamble.Length;

                //Convert the writer contents to a byte array
                byte[] body = enc.GetBytes(bodyString);

                //Prepend the preamble to body (store result in resized preamble array)
                Array.Resize<byte>(ref preamble, preambleLength + body.Length);
                Array.Copy(body, 0, preamble, preambleLength, body.Length);

                //Return the combined byte array
                return preamble;
            }
            catch (Exception e)
            {
                this.GeneratorError(4, e.ToString(), 1, 1);
                //Returning null signifies that generation has failed
                return null;
            }
        }

    }
}