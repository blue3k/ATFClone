using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sce.Atf.Dom.Gen
{
    /// <summary>
    /// Debug output 
    /// </summary>
    public class DebugOutput
    {
        /// <summary>
        /// ShowMessage with string format
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void ShowMessage(string format, params object[] args)
        {
            ShowMessage(string.Format(format, args));
        }

        /// <summary>
        /// ShowMessage
        /// </summary>
        /// <param name="message"></param>
        public static void ShowMessage(string message)
        {
            IVsOutputWindow outWindow = Package.GetGlobalService(typeof(SVsOutputWindow)) as IVsOutputWindow;

            Guid generalPaneGuid = VSConstants.GUID_OutWindowGeneralPane; // P.S. There's also the GUID_OutWindowDebugPane available.
            IVsOutputWindowPane generalPane;
            outWindow.GetPane(ref generalPaneGuid, out generalPane);

            generalPane.OutputString(message);
            generalPane.Activate(); // Brings this pane into view


        }
    }
}
