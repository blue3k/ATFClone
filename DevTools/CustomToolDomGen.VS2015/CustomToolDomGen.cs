

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
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio;
using Sce.Atf;
using VSLangProj80;
//using VSLangProj140;
using DomGen;

namespace Atf.DomGen
{
    /// <summary>
    /// This is the generator class. 
    /// When setting the 'Custom Tool' property of a C#, VB, or J# project item to "XmlClassGenerator", 
    /// the GenerateCode function will get called and will return the contents of the generated file 
    /// to the project system
    /// </summary>
    [ComVisible(true)]
    [Guid("3A278187-00BD-4C56-9EF3-068BE7C64D5D")]
    [CodeGeneratorRegistration(typeof(CustomToolDomGen),"C# XML Class Generator",vsContextGuids.vsContextGuidVCSProject,GeneratesDesignTimeSource = true)]
    //[CodeGeneratorRegistration(typeof(CustomToolDomGen), "VB XML Class Generator", vsContextGuids.vsContextGuidVBProject, GeneratesDesignTimeSource = true)]
    //[CodeGeneratorRegistration(typeof(CustomToolDomGen), "J# XML Class Generator", vsContextGuids.vsContextGuidVJSProject, GeneratesDesignTimeSource = true)]
    [ProvideObject(typeof(CustomToolDomGen))]
    public class CustomToolDomGen : IVsSingleFileGenerator //BaseCodeGeneratorWithSite
    {
#pragma warning disable 0414
        //The name of this generator (use for 'Custom Tool' property of project item)
        internal static string name = "CustomToolDomGen";
#pragma warning restore 0414
        
        /// <summary>
        /// Implements the IVsSingleFileGenerator.Generate method.
        /// Return default extension
        /// </summary>
        public int DefaultExtension(out string pbstrDefaultExtension)
        {
            pbstrDefaultExtension = ".cs";
            return VSConstants.S_OK;
        }

        //private IVsGeneratorProgress codeGeneratorProgress;
        private string codeFileNameSpace = string.Empty;
        private string codeFilePath = string.Empty;

        /// <summary>
        /// Namespace for the file
        /// </summary>
        protected string FileNameSpace
        {
            get
            {
                return codeFileNameSpace;
            }
        }

        /// <summary>
        /// File-path for the input file
        /// </summary>
        protected string InputFilePath
        {
            get
            {
                return codeFilePath;
            }
        }

        ///// <summary>
        ///// Interface to the VS shell object we use to tell our progress while we are generating
        ///// </summary>
        //internal IVsGeneratorProgress CodeGeneratorProgress
        //{
        //    get
        //    {
        //        return codeGeneratorProgress;
        //    }
        //}

        /// <summary>
        /// Implements the IVsSingleFileGenerator.Generate method.
        /// Executes the transformation and returns the newly generated output file, whenever a custom tool is loaded, or the input file is saved
        /// </summary>
        /// <param name="wszInputFilePath">The full path of the input file. May be a null reference (Nothing in Visual Basic) in future releases of Visual Studio, so generators should not rely on this value</param>
        /// <param name="bstrInputFileContents">The contents of the input file. This is either a UNICODE BSTR (if the input file is text) or a binary BSTR (if the input file is binary). If the input file is a text file, the project system automatically converts the BSTR to UNICODE</param>
        /// <param name="wszDefaultNamespace">This parameter is meaningful only for custom tools that generate code. It represents the namespace into which the generated code will be placed. If the parameter is not a null reference (Nothing in Visual Basic) and not empty, the custom tool can use the following syntax to enclose the generated code</param>
        /// <param name="rgbOutputFileContents">[out] Returns an array of bytes to be written to the generated file. You must include UNICODE or UTF-8 signature bytes in the returned byte array, as this is a raw stream. The memory for rgbOutputFileContents must be allocated using the .NET Framework call, System.Runtime.InteropServices.AllocCoTaskMem, or the equivalent Win32 system call, CoTaskMemAlloc. The project system is responsible for freeing this memory</param>
        /// <param name="pcbOutput">[out] Returns the count of bytes in the rgbOutputFileContent array</param>
        /// <param name="pGenerateProgress">A reference to the IVsGeneratorProgress interface through which the generator can report its progress to the project system</param>
        /// <returns>If the method succeeds, it returns S_OK. If it fails, it returns E_FAIL</returns>
        int IVsSingleFileGenerator.Generate(string wszInputFilePath, string bstrInputFileContents, string wszDefaultNamespace, IntPtr[] rgbOutputFileContents, out uint pcbOutput, IVsGeneratorProgress pGenerateProgress)
        {
            var debugOut = new StreamWriter("D:\\Work\\ATF\\DevTools\\ATFDomGen\\bin\\Debug\\debug.txt", false, Encoding.UTF8);

            if (bstrInputFileContents == null)
            {
                debugOut.WriteLine("Invalid filecontent");
                debugOut.Close();
                throw new ArgumentNullException(bstrInputFileContents);
            }

            if (wszInputFilePath == null)
            {
                debugOut.WriteLine("Invalid wszInputFilePath");
                debugOut.Close();
                throw new ArgumentNullException(bstrInputFileContents);
            }

            //if (CodeGeneratorProgress == null) debugOut.WriteLine("Invalid CodeGeneratorProgress");
            //debugOut.Flush();
            //CodeGeneratorProgress.Progress(0, 100);

            codeFilePath = wszInputFilePath;
            codeFileNameSpace = wszDefaultNamespace;
            //codeGeneratorProgress = pGenerateProgress;


            try {

                debugOut.WriteLine(InputFilePath);
                // class name should always the same as output file name
                string className = Path.GetFileNameWithoutExtension(InputFilePath);

                SchemaLoader typeLoader = new SchemaLoader();
                typeLoader.Load(new MemoryStream(Encoding.UTF8.GetBytes(bstrInputFileContents)));
                debugOut.WriteLine("Loaded");
                string[] fakeArgs = { "CustomToolDomGen", InputFilePath, className + ".cs", className, FileNameSpace };

                string bodyString = SchemaGen.Generate(typeLoader, "", FileNameSpace, className, fakeArgs);
                debugOut.WriteLine("Parsed");
                //CodeGeneratorProgress.Progress(50, 100);
                debugOut.WriteLine(bodyString);

                if (string.IsNullOrEmpty(bodyString))
                {
                    // This signals that GenerateCode() has failed. Tasklist items have been put up in GenerateCode()
                    rgbOutputFileContents = null;
                    pcbOutput = 0;
                    debugOut.Close();
                    // Return E_FAIL to inform Visual Studio that the generator has failed (so that no file gets generated)
                    return VSConstants.E_FAIL;
                }
                else
                {
                    // The contract between IVsSingleFileGenerator implementors and consumers is that 
                    // any output returned from IVsSingleFileGenerator.Generate() is returned through  
                    // memory allocated via CoTaskMemAlloc(). Therefore, we have to convert the 
                    // byte[] array returned from GenerateCode() into an unmanaged blob.  

                    var bytes = Encoding.UTF8.GetBytes(bodyString);

                    int outputLength = bytes.Length;
                    rgbOutputFileContents[0] = Marshal.AllocCoTaskMem(outputLength);
                    Marshal.Copy(bytes, 0, rgbOutputFileContents[0], outputLength);
                    pcbOutput = (uint)outputLength;
                    debugOut.Close();
                    return VSConstants.S_OK;
                }
            }
            catch(Exception exp)
            {
                rgbOutputFileContents = null;
                pcbOutput = 0;

                debugOut.WriteLine(exp.ToString());
                debugOut.WriteLine(exp.StackTrace.ToString());
                debugOut.Close();
                return VSConstants.E_FAIL;
            }
        }

        ///// <summary>
        ///// Function that builds the contents of the generated file based on the contents of the input file
        ///// </summary>
        ///// <param name="inputFileContent">Content of the input file</param> 
        ///// <returns>Generated file as a byte array</returns>
        //protected override byte[] GenerateCode(string inputFileContent)
        //{
        //    CodeDomProvider provider = GetCodeProvider();

        //    var debugOut = new StreamWriter("D:\\Work\\ATF\\DevTools\\ATFDomGen\\bin\\Debug\\debug.txt", false, Encoding.UTF8);

        //    debugOut.WriteLine(inputFileContent);

        //    try
        //    {

        //        // class name should always the same as output file name
        //        string className = Path.GetFileNameWithoutExtension(InputFilePath);
        //        debugOut.WriteLine(InputFilePath);
        //        SchemaLoader typeLoader = new SchemaLoader();
        //        typeLoader.Load(new MemoryStream(Encoding.UTF8.GetBytes(inputFileContent)));
        //        debugOut.WriteLine("Loaded");
        //        string[] fakeArgs = { "CustomToolDomGen", InputFilePath, className + ".cs", className, FileNameSpace };

        //        string bodyString = SchemaGen.Generate(typeLoader, "", FileNameSpace, className, fakeArgs);
        //        debugOut.WriteLine("Generated");

        //        //Create the CodeCompileUnit from the passed-in XML file
        //        CodeCompileUnit code = new CodeCompileUnit();

        //        // Just for VB.NET:
        //        // Option Strict On (controls whether implicit type conversions are allowed)
        //        code.UserData.Add("AllowLateBound", false);
        //        // Option Explicit On (controls whether variable declarations are required)
        //        code.UserData.Add("RequireVariableDeclaration", true);

        //        CodeNamespace codeNamespace = new CodeNamespace("test1");

        //        CodeTypeDeclaration typeDeclaration = new CodeTypeDeclaration("enumType1");
        //        typeDeclaration.IsEnum = true;
        //        typeDeclaration.TypeAttributes = System.Reflection.TypeAttributes.Public;
        //        typeDeclaration.Members.Add(new CodeMemberField("System.Int32", "enumValue1"));
        //        typeDeclaration.Members.Add(new CodeMemberField("System.Int32", "enumValue2"));
        //        codeNamespace.Types.Add(typeDeclaration);

        //        code.Namespaces.Add(codeNamespace);

        //        CodeCompileUnit compileUnit = code;

        //        if (this.CodeGeneratorProgress != null)
        //        {
        //            //Report that we are 1/2 done
        //            this.CodeGeneratorProgress.Progress(50, 100);
        //        }

        //        // Use utf8 encoding all the time
        //        using (StringWriter writer = new Utf8StringWriter(new StringBuilder()))
        //        {
        //            CodeGeneratorOptions options = new CodeGeneratorOptions();
        //            options.BlankLinesBetweenMembers = false;
        //            options.BracingStyle = "C";

        //            //Generate the code
        //            provider.GenerateCodeFromCompileUnit(compileUnit, writer, options);

        //            if (this.CodeGeneratorProgress != null)
        //            {
        //                //Report that we are done
        //                this.CodeGeneratorProgress.Progress(100, 100);
        //            }
        //            writer.Flush();

        //            //Get the Encoding used by the writer. We're getting the WindowsCodePage encoding, 
        //            //which may not work with all languages
        //            //Encoding enc = Encoding.GetEncoding(writer.Encoding.WindowsCodePage);
        //            Encoding enc = writer.Encoding;

        //            //Get the preamble (byte-order mark) for our encoding
        //            byte[] preamble = enc.GetPreamble();
        //            int preambleLength = preamble.Length;

        //            //Convert the writer contents to a byte array
        //            byte[] body = enc.GetBytes(bodyString);// writer.ToString());

        //            //Prepend the preamble to body (store result in resized preamble array)
        //            Array.Resize<byte>(ref preamble, preambleLength + body.Length);
        //            Array.Copy(body, 0, preamble, preambleLength, body.Length);
        //            debugOut.Close();
        //            //Return the combined byte array
        //            return preamble;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        debugOut.WriteLine(e.ToString());
        //        debugOut.WriteLine(e.StackTrace.ToString());
        //        debugOut.Close();
        //        //System.Diagnostics.Debug.Print(e.ToString());
        //        this.GeneratorError(4, e.ToString(), 1, 1);
        //        //Returning null signifies that generation has failed
        //        return null;
        //    }
        //}

    }
}