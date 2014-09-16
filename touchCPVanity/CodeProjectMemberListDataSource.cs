using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using be.trojkasoftware.portableCPVanity;
using MonoTouch.Foundation;

namespace touchCPVanity
{
	public class CodeProjectMemberListDataSource : UITableViewSource
	{
		public CodeProjectMemberListDataSource (List<CodeProjectMember> members)
		{
			MemberList = members;
		}

		public List<CodeProjectMember> MemberList {
			private set;
			get;
		}
			
		public override int RowsInSection (UITableView tableview, int section)
		{
			return MemberList.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell ("MemberCell");
			(cell.ViewWithTag (100) as UILabel).Text = MemberList[indexPath.Row].Name;

//			var cell = tableView.DequeueReusableCell ("TableCell");
//			var cellStyle = UITableViewCellStyle.Default;
//
//			if (cell == null) {
//				cell = new UITableViewCell (cellStyle, "TableCell");
//			}
//
//			cell.TextLabel.Text = MemberList[indexPath.Row].Name;

			return cell;
		}

		public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, MonoTouch.Foundation.NSIndexPath indexPath)
		{
		
		}

		/// <summary>
		/// Called by the table view to determine whether or not the row is editable
		/// </summary>
		public override bool CanEditRow (UITableView tableView, NSIndexPath indexPath)
		{
			return true; // return false if you wish to disable editing for a specific indexPath or for all rows
		}

//		/// <summary>
//		/// Called by the table view to determine whether or not the row is moveable
//		/// </summary>
//		public override bool CanMoveRow (UITableView tableView, NSIndexPath indexPath)
//		{
//			return false;
//		}

		public override UITableViewCellEditingStyle EditingStyleForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return UITableViewCellEditingStyle.Delete;
		}
	}
}

