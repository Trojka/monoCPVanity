using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using be.trojkasoftware.portableCPVanity;
using be.trojkasoftware.portableCPVanity.RssFeeds;
using be.trojkasoftware.Ripit.Core;
using be.trojkasoftware.portableCPVanity.ViewModels;

namespace be.trojkasoftware.droidCPVanity
{
	public class CodeProjectLoungeFeedFragment : CodeProjectRSSFeedFragment
	{

		public override CodeProjectRssFeed GetFeed()
		{
			return new CodeProjectLoungeFeed ();
		}

		public override void SelectCategory() {
			AlertDialog.Builder builderSingle = new AlertDialog.Builder(this.Activity);
			builderSingle.SetTitle ("Select Category");

			ArrayAdapter<String> arrayAdapter = new ArrayAdapter<String>(
				this.Activity,
				Android.Resource.Layout.SelectDialogSingleChoice);
			arrayAdapter.AddAll (categories.Categories);

			builderSingle.SetAdapter (arrayAdapter, this.ItemClicked);
			builderSingle.Show ();
		}

		void ItemClicked(object source, DialogClickEventArgs eventArgs)
		{
			viewModel.ItemFeed = categories.GetFeedForCategory(categories.Categories[eventArgs.Which]);

			LoadFeed ();
		}

		private CommunityRssCategories categories = new CommunityRssCategories();

//		View view;
//
////		public override void OnActivityCreated (Bundle savedInstanceState) 
////		{
////			base.OnActivityCreated (savedInstanceState);
////		}
//
//		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
//		{
//			view = inflater.Inflate (Resource.Layout.CodeProjectLoungeFeedLayout, null);
//
//			return view;
//		}
	}
}

