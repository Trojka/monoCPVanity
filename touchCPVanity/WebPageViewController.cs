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
			base.DidReceiveMemoryWarning ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			string targetUrl = PageURL;
			if(!targetUrl.StartsWith(CodeProjectUrlScheme.BaseUrl)) {
				targetUrl = CodeProjectUrlScheme.BaseUrl + targetUrl;
			}

			NSUrlRequest request = NSUrlRequest.FromUrl (NSUrl.FromString (targetUrl));
			WebView.LoadRequest(request);
		}

		public String PageURL {
			get;
			set;
		}
	}
}

