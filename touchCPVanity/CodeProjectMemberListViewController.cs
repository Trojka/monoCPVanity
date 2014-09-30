using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using be.trojkasoftware.portableCPVanity;
using be.trojkasoftware.monoCPVanity.Data;

namespace touchCPVanity
{
	public partial class CodeProjectMemberListViewController : UIViewController
	{
		public CodeProjectMemberListViewController (IntPtr handle) : base (handle)
		{
		}

		[Export ("tableView:heightForRowAtIndexPath:")]
		public float GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			if (tableView != MemberListTable && indexPath.Item == 0) {
				return 35;
			}

			return 70;
		}

		[Export("tableView:numberOfRowsInSection:")]
		public int RowsInSection (UITableView tableView, int section)
		{
//			if (tableView == MemberListTable) {
//				return MemberList.Count;
//			} else {
				if ((MemberList != null) && !String.IsNullOrEmpty(searchString)) {
					int searchId;

					if (int.TryParse (searchString, out searchId)) {
						return MemberList.Where (x => x.Id == searchId).Count () + 1;
					} 
					else {
						return MemberList.Where (x => x.Name.ToUpper().Contains(searchString.ToUpper())).Count () + 1;
					}
				} else {
					return 1;
				}
//			}

		}

		[Export("tableView:cellForRowAtIndexPath:")]
		public UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
//			if (tableView == MemberListTable) {
//				var cell = tableView.DequeueReusableCell ("TableCell");
//				var cellStyle = UITableViewCellStyle.Default;
//
//				if (cell == null) {
//					cell = new UITableViewCell (cellStyle, "TableCell");
//				}
//
//				cell.TextLabel.Text = MemberList[indexPath.Row].Name;
//
//				return cell;
//			} else {
				if (indexPath.Item == 0) {
					var cell = MemberListTable.DequeueReusableCell ("AddMemberCell");

					(cell.ViewWithTag (100) as UILabel).Text = "Load member " + searchString;

					return cell;
				} else {
					var cell = MemberListTable.DequeueReusableCell ("MemberCell");

					int searchId;
					CodeProjectMember member = null;

					if (int.TryParse (searchString, out searchId)) {
						member = MemberList.Where (x => x.Id == searchId).ToList () [indexPath.Item - 1];
					} else {
						member = MemberList.Where (x => x.Name.ToUpper ().Contains (searchString.ToUpper ())).ToList () [indexPath.Item - 1];
					}

					CodeProjectMemberListDataSource.FillCellWithMember(cell, member);

					return cell;
				}
//			}
		}

		[Export("tableView:canEditRowAtIndexPath:")]
		public bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
		{
			return true;
		}

		[Export("tableView:editingStyleForRowAtIndexPath:")]
		public UITableViewCellEditingStyle EditingStyleForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return UITableViewCellEditingStyle.Delete;
		}

		[Export("searchDisplayController:shouldReloadTableForSearchString:")]
		public bool ShouldReloadForSearchString (UISearchDisplayController controller, string forSearchString)
		{
			searchString = forSearchString;
			return true;
		}

		[Export("searchDisplayController:shouldReloadTableForSearchScope:")]
		public bool ShouldReloadForSearchScope (UISearchDisplayController controller, int forSearchOption)
		{
			return true;
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);

			int memberId = -1;
			if (segue.Identifier == "LoadMember") {
				if (!int.TryParse (searchString, out memberId)) {
					CodeProjectMember member = MemberList.Where (x => x.Name == searchString).FirstOrDefault ();
					if (member != null) {
						memberId = member.Id;
					}
				} 
				MemberSearchBarController.SearchBar.Text = String.Empty;
				MemberSearchBarController.SearchBar.ResignFirstResponder();
				MemberSearchBarController.Active = false;

			} else if (segue.Identifier == "ShowMember") {
				NSIndexPath selectedIndexPath = MemberListTable.IndexPathForSelectedRow;
				if (selectedIndexPath != null) {
					CodeProjectMember member = MemberList [selectedIndexPath.Row];
					if (member != null) {
						memberId = member.Id;
					}
				} else {
					selectedIndexPath = MemberSearchBarController.SearchResultsTableView.IndexPathForSelectedRow;
					if (selectedIndexPath != null) {
						CodeProjectMember member = MemberList.Where (x => x.Name.ToUpper ().Contains (searchString.ToUpper ())).ToList () [selectedIndexPath.Row - 1];
						if (member != null) {
							memberId = member.Id;
						}
						MemberSearchBarController.SearchBar.Text = String.Empty;
						MemberSearchBarController.SearchBar.ResignFirstResponder();
						MemberSearchBarController.Active = false;

					}
				}
			}

			var memberProfileController = segue.DestinationViewController as CodeProjectMemberProfileViewController;

			if (memberProfileController != null) {
				memberProfileController.MemberId = memberId;
			}
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.RefreshBtn.Clicked += OnRefreshClicked;

			CodeProjectDatabase db = new CodeProjectDatabase ();
			MemberList = db.GetMembers();

			MemberListTable.Source = new CodeProjectMemberListDataSource(MemberList);

			MemberSearchBarController.SearchResultsWeakDataSource = this;
			MemberSearchBarController.WeakDelegate = this;

		}

		public override void ViewDidAppear (bool animated)
		{
			Refresh ();
		}

		void OnRefreshClicked(object sender, EventArgs E)
		{
			Refresh ();
		}

		void Refresh()
		{
			CodeProjectDatabase db = new CodeProjectDatabase ();
			MemberList = db.GetMembers();

			MemberListTable.Source = new CodeProjectMemberListDataSource(MemberList);
			MemberListTable.ReloadData ();
		}

		public List<CodeProjectMember> MemberList {
			private set;
			get;
		}

		String searchString;
	}
}

