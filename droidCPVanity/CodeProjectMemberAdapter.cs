using System;
using Android.Widget;
using Android.Content;
using System.Collections.Generic;
using be.trojkasoftware.portableCPVanity;
using Android.App;
using Java.IO;
using Android.Graphics;
using Android.Util;


namespace be.trojkasoftware.droidCPVanity
{
	public class CodeProjectMemberAdapter : BaseAdapter
	{
		public CodeProjectMemberAdapter (Activity activity, List<CodeProjectMember> list)
		{
			this.activity = activity;
			this.list = list;
		}

		public CodeProjectMember GetMember(int position)
		{
			return list[position];
		}

		#region implemented abstract members of BaseAdapter

		public override Java.Lang.Object GetItem (int position)
		{
			return null;
		}

		public override long GetItemId (int position)
		{
			return list[position].Id;
		}

		public override Android.Views.View GetView (int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
		{

			var view = convertView ?? activity.LayoutInflater.Inflate (
				Resource.Layout.CodeProjectMemberListItem, parent, false);
			var memberName = view.FindViewById<TextView> (Resource.Id.textViewMemberName);
			var memberArticleCnt = view.FindViewById<TextView> (Resource.Id.textViewArticleCnt);
			var memberBlogCnt = view.FindViewById<TextView> (Resource.Id.textViewBlogCnt);
			var memberIcon = view.FindViewById<ImageView> (Resource.Id.imageViewMemberImage);

			memberName.Text = list[position].Name;
			memberArticleCnt.Text = "Articles: " + list[position].ArticleCount;
			memberBlogCnt.Text = "Blogs: " + list[position].BlogCount;

			string memberIconFilenam = list[position].Id.ToString () + ".png";
			var dir = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), memberIconFilenam);
			if (dir.Exists ()) {
				try {
					var bitmap = BitmapFactory.DecodeFile (dir.AbsolutePath);
					var scaledBitmap = Bitmap.CreateScaledBitmap(bitmap, bitmap.Width/2, bitmap.Height/2, false);
					memberIcon.SetImageBitmap (scaledBitmap);
				} catch (Exception e) {
					Log.Error ("Error", e.Message);
				}
			}

			return view;
		}

		public override int Count {
			get {
				return list.Count;
			}
		}

		#endregion

		Activity activity;
		List<CodeProjectMember> list;
	}
}

