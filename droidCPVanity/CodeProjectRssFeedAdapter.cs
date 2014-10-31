using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Util;
using Android.Widget;
using be.trojkasoftware.portableCPVanity;
using be.trojkasoftware.portableCPVanity.RssFeeds;
using be.trojkasoftware.Ripit.Core;


namespace be.trojkasoftware.droidCPVanity
{
	public class CodeProjectRssFeedAdapter : BaseAdapter
	{
		public CodeProjectRssFeedAdapter (Activity activity, CodeProjectRssFeed rssItemList)
		{
			this.activity = activity;
			this.rssItemList = rssItemList;
		}

		#region implemented abstract members of BaseAdapter

		public override Java.Lang.Object GetItem (int position)
		{
			return null;
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override Android.Views.View GetView (int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
		{

			var view = convertView ?? activity.LayoutInflater.Inflate (
				Resource.Layout.CodeProjectRssFeedItem, parent, false);
			var rssItemTitle = view.FindViewById<TextView> (Resource.Id.textViewRssItemTitle);
			var rssItemDescription = view.FindViewById<TextView> (Resource.Id.textViewRssItemDescription);

			rssItemTitle.Text = rssItemList[position].Title;
			rssItemDescription.Text = rssItemList[position].Description;

			return view;
		}

		public override int Count {
			get {
				return rssItemList.Count;
			}
		}

		#endregion

		Activity activity;
		CodeProjectRssFeed rssItemList;
	}
}

