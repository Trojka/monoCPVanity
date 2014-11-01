using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using be.trojkasoftware.portableCPVanity;
using be.trojkasoftware.monoCPVanity.Util;
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

			progressView = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
			progressView.Center = new PointF (this.View.Frame.Width / 2, this.View.Frame.Height / 2);
			this.View.AddSubview (progressView);

			WebImageRetriever imageDownloader = new WebImageRetriever ();
			Task<UIImage> loadGraphTask = imageDownloader.GetImageAsync (new Uri (Member.ReputationGraph));

			progressView.StartAnimating ();

			var context = TaskScheduler.FromCurrentSynchronizationContext();

			loadGraphTask.ContinueWith (t => ReputationGraphLoaded(t.Result), context);
		}

		public void ReputationGraphLoaded(UIImage graph) {

			progressView.StopAnimating ();

			this.ReputationGraph.Image = graph;
		}


		public CodeProjectMember Member {
			get;
			set;
		}

		UIActivityIndicatorView progressView;
	}
}

