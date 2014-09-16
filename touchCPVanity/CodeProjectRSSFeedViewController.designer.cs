// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace touchCPVanity
{
	[Register ("CodeProjectRSSFeedViewController")]
	partial class CodeProjectRSSFeedViewController
	{
		[Outlet]
		MonoTouch.UIKit.UILabel CategoryLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView RSSItemTable { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CategoryLabel != null) {
				CategoryLabel.Dispose ();
				CategoryLabel = null;
			}

			if (RSSItemTable != null) {
				RSSItemTable.Dispose ();
				RSSItemTable = null;
			}
		}
	}
}
