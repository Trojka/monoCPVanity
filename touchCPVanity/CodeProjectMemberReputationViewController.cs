using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using be.trojkasoftware.portableCPVanity;
using touchCPVanity.Util;
using System.Threading.Tasks;

namespace touchCPVanity
{
	public partial class CodeProjectMemberReputationViewController : UIViewController
	{
		public CodeProjectMemberReputationViewController (IntPtr handle) : base (handle)
		{
		}
			
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			WebImageRetriever imageDownloader = new WebImageRetriever ();
			imageDownloader.GetImageAsync (new Uri(Member.ReputationGraph)).ContinueWith (t => {
				this.ReputationGraph.Image = t.Result;
			}, TaskScheduler.FromCurrentSynchronizationContext ());
		}

		public CodeProjectMember Member {
			get;
			set;
		}
	}
}

