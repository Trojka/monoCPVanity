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
	[Register ("WebPageViewController")]
	partial class WebPageViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIWebView WebView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (WebView != null) {
				WebView.Dispose ();
				WebView = null;
			}
		}
	}
}
