using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using be.trojkasoftware.portableCPVanity;
using be.trojkasoftware.monoCPVanity.Util;
using System.Threading.Tasks;
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

//			WebImageRetriever imageDownloader = new WebImageRetriever ();
//			Task<UIImage> loadGraphTask = imageDownloader.GetImageAsync (new Uri (Member.ReputationGraph));
//
//			loadGraphTask.ContinueWith (t => ReputationGraphLoaded(t.Result), context);
		}

		public void ReputationGraphLoaded(byte[] graph) {

			progressView.StopAnimating ();

			NSData data = NSData.FromArray (graph);
			this.ReputationGraph.Image = UIImage.LoadFromData (data, 1);
		}

//		public void ReputationGraphLoaded(UIImage graph) {
//
//			progressView.StopAnimating ();
//
//			this.ReputationGraph.Image = graph;
//		}

		public void SetMember(CodeProjectMember member) {
			viewModel.MemberReputationGraph = member.ReputationGraph;
		}

//		public CodeProjectMember Member {
//			get;
//			set;
//		}

		UIActivityIndicatorView progressView;
		CodeProjectMemberReputationViewModel viewModel;
	}
}

