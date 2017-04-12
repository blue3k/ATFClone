using System.ComponentModel;
using System.Windows.Forms;

namespace Sce.Atf.Gui.TitleBarTab
{
	/// <summary>Event arguments class for a cancelable event that occurs on a collection of collection of <see cref="TitleBarTabItem" />s.</summary>
	public class TitleBarTabCancelEventArgs : CancelEventArgs
	{
		/// <summary>Action that is being performed.</summary>
		public TabControlAction Action
		{
			get;
			set;
		}

		/// <summary>The tab that the <see cref="Action" /> is being performed on.</summary>
		public TitleBarTabItem Tab
		{
			get;
			set;
		}

		/// <summary>Index of the tab within the collection.</summary>
		public int TabIndex
		{
			get;
			set;
		}
	}
}