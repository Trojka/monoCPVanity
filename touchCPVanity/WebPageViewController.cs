using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using be.trojkasoftware.portableCPVanity;

namespace touchCPVanity
{
	public partial class WebPageViewController : UIViewController
	{
		public WebPageViewController (IntPtr handle) : base (handle)
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

			NSUrlRequest request = NSUrlRequest.FromUrl (NSUrl.FromString (CodeProjectUrlScheme.BaseUrl + PageURL));
			WebView.LoadRequest(request);
		}

		public String PageURL {
			get;
			set;
		}
	}
}

