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
			base.DidReceiveMemoryWarning ();
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

