using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace be.trojkasoftware.droidCPVanity
{
	public class CodeProjectArticleFeedFragment : Fragment
	{
		View view;

//		public override void OnActivityCreated (Bundle savedInstanceState) 
//		{
//			base.OnActivityCreated (savedInstanceState);
//		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			view = inflater.Inflate (Resource.Layout.CodeProjectArticleFeedLayout, null);

			return view;
		}
	}
}

