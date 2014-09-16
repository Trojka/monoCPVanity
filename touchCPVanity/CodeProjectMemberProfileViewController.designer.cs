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
	[Register ("CodeProjectMemberProfileViewController")]
	partial class CodeProjectMemberProfileViewController
	{
		[Outlet]
		MonoTouch.UIKit.UILabel ArticleCountLbl { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel AvgArticleRatingLbl { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel AvgBlogRatingLbl { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel BlogCountLbl { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView MemberImage { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel MemberNameLbl { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel MemberReputationLbl { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton SaveBtn { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ArticleCountLbl != null) {
				ArticleCountLbl.Dispose ();
				ArticleCountLbl = null;
			}

			if (AvgArticleRatingLbl != null) {
				AvgArticleRatingLbl.Dispose ();
				AvgArticleRatingLbl = null;
			}

			if (AvgBlogRatingLbl != null) {
				AvgBlogRatingLbl.Dispose ();
				AvgBlogRatingLbl = null;
			}

			if (BlogCountLbl != null) {
				BlogCountLbl.Dispose ();
				BlogCountLbl = null;
			}

			if (MemberImage != null) {
				MemberImage.Dispose ();
				MemberImage = null;
			}

			if (MemberNameLbl != null) {
				MemberNameLbl.Dispose ();
				MemberNameLbl = null;
			}

			if (MemberReputationLbl != null) {
				MemberReputationLbl.Dispose ();
				MemberReputationLbl = null;
			}

			if (SaveBtn != null) {
				SaveBtn.Dispose ();
				SaveBtn = null;
			}
		}
	}
}
