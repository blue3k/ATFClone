

using System;
using System.Runtime.InteropServices;
using System.CodeDom.Compiler;
using System.CodeDom;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio;
using VSLangProj80;
using Sce.Atf;
using Sce.Atf.Dom;

namespace Sce.Atf.Dom.Gen
{
    /// <summary>
    /// Local xml url resolver
    /// </summary>
    public class LocalXmlUrlResolver : XmlUrlResolver
    {
        /// <summary>
        /// Base search path
        /// </summary>
        string m_BasePath;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="basePath">Uri base</param>
        public LocalXmlUrlResolver(string basePath)
        {
            m_BasePath = basePath;
        }

        /// <summary>
        /// Override ResolveUri method
        /// </summary>
        /// <param name="baseUri"></param>
        /// <param name="relativeUri"></param>
        /// <returns></returns>
        public override Uri ResolveUri(Uri baseUri, string relativeUri)
        {
            return new Uri(Path.Combine(m_BasePath, relativeUri));
        }
    }

    /// <summary>
    /// Generator class for ATF DOM
    /// </summary>
    [ComVisible(true)]
    [Guid("3A278187-00BD-4C56-9EF3-068BE7C64D5D")]
    [CodeGeneratorRegistration(typeof(DomGenVS),"ATF C# DOM Class Generator",vsContextGuids.vsContextGuidVCSProject,GeneratesDesignTimeSource = true)]
    [ProvideObject(typeof(DomGenVS))]
    public class DomGenVS : IVsSingleFileGenerator
    {
        /// <summary>
        /// Debug output
        /// </summary>
        public static StreamWriter debugOut = null;
        /// <summary>
        /// Implements the IVsSingleFileGenerator.Generate method.
        /// Return default extension
        /// </summary>
        public int DefaultExtension(out string pbstrDefaultExtension)
        {
            pbstrDefaultExtension = ".cs";
            return VSConstants.S_OK;
        }
        
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
            if (bstrInputFileContents == null)
            {
                debugOut.Flush();
                throw new ArgumentNullException(bstrInputFileContents);
            }

            if (wszInputFilePath == null)
            {
                throw new ArgumentNullException(bstrInputFileContents);
            }
            
            string InputFilePath = wszInputFilePath;
            string FileNameSpace = wszDefaultNamespace;

            try {

                // class name should always the same as output file name
                string className = Path.GetFileNameWithoutExtension(InputFilePath);

                SchemaLoader typeLoader = new SchemaLoader();

                typeLoader.SchemaResolver = new LocalXmlUrlResolver(Path.GetDirectoryName(InputFilePath));
                typeLoader.Load(new MemoryStream(Encoding.UTF8.GetBytes(bstrInputFileContents)));

                string[] fakeArgs = { "DomGenVS", Path.GetFileName(InputFilePath), className + ".cs", className, FileNameSpace, "-enums" };
                string bodyString = SchemaGen.Generate(typeLoader, "", FileNameSpace, className, fakeArgs);

                if (string.IsNullOrEmpty(bodyString))
                {
                    rgbOutputFileContents = null;
                    pcbOutput = 0;

                    return VSConstants.E_FAIL;
                }
                else
                {
                    // Return memory must be allocated by AllocCoTaskMem
                    var bytes = Encoding.UTF8.GetBytes(bodyString);

                    int outputLength = bytes.Length;
                    rgbOutputFileContents[0] = Marshal.AllocCoTaskMem(outputLength);
                    Marshal.Copy(bytes, 0, rgbOutputFileContents[0], outputLength);
                    pcbOutput = (uint)outputLength;
                    return VSConstants.S_OK;
                }
            }
            catch(Exception)
            {
                rgbOutputFileContents = null;
                pcbOutput = 0;
                return VSConstants.E_FAIL;
            }
        }
        
    }
}