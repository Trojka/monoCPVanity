using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using be.trojkasoftware.portableCPVanity;
using be.trojkasoftware.portableCPVanity.ViewModels;

namespace be.trojkasoftware.droidCPVanity
{
	public class CodeProjectMemberArticleAdapter : BaseAdapter
	{
		public CodeProjectMemberArticleAdapter (Activity activity, List<CodeProjectMemberArticleViewModel> list)
		{
			this.activity = activity;
			this.list = list;
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
				Resource.Layout.CodeProjectMemberArticlesItem, parent, false);
			var articleTitle = view.FindViewById<TextView> (Resource.Id.textViewTitle);
			var articleDate = view.FindViewById<TextView> (Resource.Id.textViewDate);
			//var articleScore = view.FindViewById<TextView> (Resource.Id.textViewScore);
			//var articleVotes = view.FindViewById<TextView> (Resource.Id.textViewVotes);

			articleTitle.Text = list[position].Title;
			articleDate.Text = list[position].DateUpdated;
			//articleScore.Text = list[position].Rating;
			//articleVotes.Text = list[position].Votes;

			return view;
		}

		public override int Count {
			get {
				return list.Count;
			}
		}

		#endregion

		Activity activity;
		List<CodeProjectMemberArticleViewModel> list;	}
}

