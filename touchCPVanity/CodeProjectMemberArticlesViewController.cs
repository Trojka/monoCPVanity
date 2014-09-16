﻿using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using be.trojkasoftware.portableCPVanity;
using be.trojkasoftware.Ripit.Core;
using System.Collections.Generic;

namespace touchCPVanity
{
	public partial class CodeProjectMemberArticlesViewController : UIViewController
	{
		public CodeProjectMemberArticlesViewController (IntPtr handle) : base (handle)
		{
		}

		[Export("tableView:cellForRowAtIndexPath:")]
		public UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell ("ArticleCell");

			(cell.ViewWithTag (100) as UILabel).Text = MemberArticles[indexPath.Row].Title;

			return cell;
		}

		[Export("tableView:numberOfRowsInSection:")]
		public int RowsInSection (UITableView tableView, int section)
		{
			return MemberArticles.Count;
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);

			if (segue.Identifier == "MemberReputationSegue") {

				var memberReputationController = segue.DestinationViewController as CodeProjectMemberReputationViewController;

				if (memberReputationController != null) {
					memberReputationController.Member = Member;
				}
			}
			if (segue.Identifier == "MemberArticleSegue") {

				var memberArticleViewController = segue.DestinationViewController as WebPageViewController;

				if (memberArticleViewController != null) {
					NSIndexPath indexPath =  MemberArticlesTable.IndexPathForSelectedRow;
					memberArticleViewController.PageURL = MemberArticles [indexPath.Row].Link;
				}
			}
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			MemberArticles = new CodeProjectMemberArticles ();
			MemberArticles.Id = Member.Id;

			Dictionary<String, String> param = new Dictionary<string, string> ();
			param.Add ("Id", Member.Id.ToString());

			ObjectBuilder objectBuilder = new ObjectBuilder ();
			objectBuilder.FillList (MemberArticles, param, () => new CodeProjectMemberArticle());

			MemberArticlesTable.WeakDataSource = this;
		}

		public CodeProjectMember Member {
			get;
			set;
		}

		public CodeProjectMemberArticles MemberArticles {
			get;
			set;
		}
	}
}

