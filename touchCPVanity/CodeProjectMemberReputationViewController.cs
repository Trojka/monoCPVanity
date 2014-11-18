using System;
using System.Drawing;
using System.Threading.Tasks;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using be.trojkasoftware.portableCPVanity;
using be.trojkasoftware.monoCPVanity.Util;
using be.trojkasoftware.portableCPVanity.ViewModels;

namespace touchCPVanity
{
	public partial class CodeProjectMemberReputationViewController : UIViewController
	{
		public CodeProjectMemberReputationViewController (IntPtr handle) : base (handle)
		{
			viewModel = new CodeProjectMemberReputationViewModel ();
			viewModel.ReputationGraphLoaded += this.ReputationGraphLoaded;
		}
			
		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			progressView = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
			progressView.Center = new PointF (this.View.Frame.Width / 2, this.View.Frame.Height / 2);
			this.View.AddSubview (progressView);

			progressView.StartAnimating ();

			var context = TaskScheduler.FromCurrentSynchronizationContext();

			viewModel.LoadMemberReputation (context);
		}

		public void ReputationGraphLoaded(byte[] graph) {

			progressView.StopAnimating ();

			NSData data = NSData.FromArray (graph);
			this.ReputationGraph.Image = UIImage.LoadFromData (data, 1);
		}

		public void SetMemberReputationGraphUrl(string memberReputationGraphUrl) {
			viewModel.MemberReputationGraphUrl = memberReputationGraphUrl;
		}

		UIActivityIndicatorView progressView;
		CodeProjectMemberReputationViewModel viewModel;
	}
}

