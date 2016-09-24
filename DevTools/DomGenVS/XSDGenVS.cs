

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
using System.Diagnostics;
using Microsoft.Win32;

namespace Sce.Atf.Dom.Gen
{

    /// <summary>
    /// Generator class for ATF DOM
    /// </summary>
    [ComVisible(true)]
    [Guid("9DC61C2E-49E7-4A32-BBF9-97E2F382EC64")]
    [CodeGeneratorRegistration(typeof(XSDGenVS),"C# XSD Class Generator",vsContextGuids.vsContextGuidVCSProject,GeneratesDesignTimeSource = true)]
    [ProvideObject(typeof(XSDGenVS))]
    public class XSDGenVS : IVsSingleFileGenerator
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

        string GetMSSDKPath()
        {
            uint[] selectedVersion = new uint[4] { 0, 0, 0, 0 };
            string selectedSDKPath = null;
            using (var subKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Microsoft SDKs\\Windows"))
            {
                foreach (var subKeyName in subKey.GetSubKeyNames())
                {
                    string installationPath = null;
                    string versionString = null;
                    using (var child = subKey.OpenSubKey(subKeyName))
                    {
                        using (var net40PackageKey = child.OpenSubKey("WinSDK-NetFx40Tools"))
                        using (var net35PackageKey = child.OpenSubKey("WinSDK-NetFx40Tools"))
                        {
                            if (net40PackageKey != null)
                                installationPath = net40PackageKey.GetValue("InstallationFolder") as string;
                            else if (net35PackageKey != null)
                                installationPath = net35PackageKey.GetValue("InstallationFolder") as string;
                            else
                                continue;
                        }

                        versionString = child.GetValue("ProductVersion") as string;
                    }


                    if (!string.IsNullOrEmpty(installationPath) && !string.IsNullOrEmpty(versionString))
                    {
                        uint[] version = new uint[4] { 0, 0, 0, 0 };
                        var versionStrings = versionString.Split('.');

                        for (int iVersion = 0; iVersion < versionStrings.Length; iVersion++)
                        {
                            if (!uint.TryParse(versionStrings[iVersion], out version[iVersion]))
                            {
                                version[0] = 0;
                                break;
                            }
                        }

                        bool isBig = true;
                        for (int iVersion = 0; iVersion < versionStrings.Length; iVersion++)
                        {
                            if (selectedVersion[iVersion] > version[iVersion])
                            {
                                isBig = false;
                                break;
                            }
                        }

                        if (isBig)
                        {
                            selectedVersion = version;
                            selectedSDKPath = installationPath;
                        }
                    }
                }

            }

            return selectedSDKPath;
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
            string tempPath = Path.GetTempPath();
            string outputFilePath = Path.Combine(tempPath,Path.ChangeExtension(Path.GetFileName(wszInputFilePath), ".cs"));
            string FileNameSpace = wszDefaultNamespace;

            try {

                var splitedNameSpec = wszDefaultNamespace.Split(',');
                string languageName = splitedNameSpec.Length > 0 ? splitedNameSpec[0] : "CS";
                string namespaceName = splitedNameSpec.Length > 1 ? splitedNameSpec[1] : "";
                string namespaceParam = string.IsNullOrEmpty(namespaceName) ? "" : string.Format("/namespace:{0}", namespaceName);

                var StartInfo = new ProcessStartInfo();

                StartInfo.CreateNoWindow = false;
                StartInfo.UseShellExecute = false;
                StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                StartInfo.StandardErrorEncoding = Encoding.UTF8;
                StartInfo.StandardOutputEncoding = Encoding.UTF8;
                StartInfo.RedirectStandardError = true;
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.FileName = Path.Combine(GetMSSDKPath(), "xsd.exe");
                StartInfo.Arguments = string.Format(" {0} /o:{1} /classes /language:{2} {3}", InputFilePath, tempPath, languageName, namespaceParam);

                var m_RunningProcess = new Process();
                m_RunningProcess.StartInfo = StartInfo;

                m_RunningProcess.Start();
                var output = m_RunningProcess.StandardOutput.ReadToEnd();

                using (var outFileStream = new FileStream(outputFilePath, FileMode.Open))
                {
                    if(outFileStream == null || outFileStream.Length == 0)
                    {
                        rgbOutputFileContents = null;
                        pcbOutput = 0;
                        return VSConstants.E_FAIL;
                    }
                    byte[] buffer = new byte[outFileStream.Length];
                    int readSize = outFileStream.Read(buffer, 0, buffer.Length);
                    if (readSize != buffer.Length)
                    {
                        rgbOutputFileContents = null;
                        pcbOutput = 0;
                        return VSConstants.E_FAIL;
                    }

                    int outputLength = buffer.Length;
                    rgbOutputFileContents[0] = Marshal.AllocCoTaskMem(outputLength);
                    Marshal.Copy(buffer, 0, rgbOutputFileContents[0], outputLength);
                    pcbOutput = (uint)outputLength;
                    return VSConstants.S_OK;
                }

            }
            catch(Exception exp)
            {
                pGenerateProgress.GeneratorError(0, 0, exp.Message, 0, 0);
                rgbOutputFileContents = null;
                pcbOutput = 0;
                return VSConstants.E_FAIL;
            }
        }
        
    }
}