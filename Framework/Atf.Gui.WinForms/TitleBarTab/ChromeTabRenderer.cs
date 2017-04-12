using System.Drawing;

namespace Sce.Atf.Gui.TitleBarTab
{
	/// <summary>Renderer that produces tabs that mimic the appearance of the Chrome browser.</summary>
	public class ChromeTabRenderer : BaseTabRenderer
	{
		/// <summary>Constructor that initializes the various resources that we use in rendering.</summary>
		/// <param name="parentWindow">Parent window that this renderer belongs to.</param>
		public ChromeTabRenderer(TitleBarTabMainForm parentWindow)
			: base(parentWindow)
		{
			// Initialize the various images to use during rendering
			_activeLeftSideImage = ResourceUtil.GetImage(Resources.ChromeLeftImage);
			_activeRightSideImage = ResourceUtil.GetImage(Resources.ChromeRightImage);
			_activeCenterImage = ResourceUtil.GetImage(Resources.ChromeCenterImage);
			_inactiveLeftSideImage = ResourceUtil.GetImage(Resources.ChromeInactiveLeftImage);
			_inactiveRightSideImage = ResourceUtil.GetImage(Resources.ChromeInactiveRightImage);
			_inactiveCenterImage = ResourceUtil.GetImage(Resources.ChromeInactiveCenterImage);
			_closeButtonImage = ResourceUtil.GetImage(Resources.ChromeCloseImage);
			_closeButtonHoverImage = ResourceUtil.GetImage(Resources.ChromeCloseHoverImage);
			_background = ResourceUtil.GetImage(Resources.ChromeBackgroundImage);
			_addButtonImage = new Bitmap(ResourceUtil.GetImage(Resources.ChromeAddImage));
			_addButtonHoverImage = new Bitmap(ResourceUtil.GetImage(Resources.ChromeAddHoverImage));

			// Set the various positioning properties
			CloseButtonMarginTop = 6;
			CloseButtonMarginLeft = 2;
			AddButtonMarginTop = 5;
			AddButtonMarginLeft = -3;
			CaptionMarginTop = 5;
			IconMarginTop = 6;
			IconMarginRight = 5;
			AddButtonMarginRight = 5;

            ShowAddButton = false;
        }

		/// <summary>Since Chrome tabs overlap, we set this property to the amount that they overlap by.</summary>
		public override int OverlapWidth
		{
			get
			{
				return 16;
			}
		}
	}
}