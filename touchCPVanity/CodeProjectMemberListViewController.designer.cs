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
	[Register ("CodeProjectMemberListViewController")]
	partial class CodeProjectMemberListViewController
	{
		[Outlet]
		MonoTouch.UIKit.UITableView MemberListTable { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISearchBar MemberSearchBar { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISearchDisplayController MemberSearchBarController { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (MemberListTable != null) {
				MemberListTable.Dispose ();
				MemberListTable = null;
			}

			if (MemberSearchBar != null) {
				MemberSearchBar.Dispose ();
				MemberSearchBar = null;
			}

			if (MemberSearchBarController != null) {
				MemberSearchBarController.Dispose ();
				MemberSearchBarController = null;
			}
		}
	}
}
