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

			return FillCellWithMember(cell, MemberList[indexPath.Row]);
		}

		public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, MonoTouch.Foundation.NSIndexPath indexPath)
		{
		
		}

		public override bool CanEditRow (UITableView tableView, NSIndexPath indexPath)
		{
			return true; // return false if you wish to disable editing for a specific indexPath or for all rows
		}

		public override UITableViewCellEditingStyle EditingStyleForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return UITableViewCellEditingStyle.Delete;
		}

		public static UITableViewCell FillCellWithMember(UITableViewCell cell, CodeProjectMember member) {
			(cell.ViewWithTag (100) as UILabel).Text = member.Name;
			(cell.ViewWithTag (101) as UILabel).Text = "Posts: " + (member.ArticleCount + member.BlogCount);
			(cell.ViewWithTag (102) as UILabel).Text = member.Reputation;

			return cell;
		}
	}
}

