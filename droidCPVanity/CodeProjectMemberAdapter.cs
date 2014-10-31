using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Util;
using Android.Widget;
using Java.IO;
using be.trojkasoftware.portableCPVanity;
using be.trojkasoftware.monoCPVanity.Data;


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
			var memberReputation = view.FindViewById<TextView> (Resource.Id.textViewReputation);
			var memberPostCnt = view.FindViewById<TextView> (Resource.Id.textViewPostCnt);
			var memberIcon = view.FindViewById<ImageView> (Resource.Id.imageViewMemberImage);

			memberName.Text = list[position].Name;
			memberReputation.Text = list[position].Reputation;
			memberPostCnt.Text = "Posts: " + (list[position].ArticleCount + list[position].BlogCount);

			int textAppearanceLarge = TextAppearanceHeight (Android.Resource.Attribute.TextAppearanceLarge);
			int textAppearanceSmall = TextAppearanceHeight (Android.Resource.Attribute.TextAppearanceSmall);

			int bitmapSize = textAppearanceLarge + textAppearanceSmall;
			//memberIcon.SetMaxHeight(textAppearanceLarge + textAppearanceSmall);
			//memberIcon.SetMaxWidth(textAppearanceLarge + textAppearanceSmall);
			//memberIcon.RequestLayout ();

			CodeProjectDatabase database = new CodeProjectDatabase ();
			byte[] gravatar = database.GetGravatar (list[position].Id);
			if (gravatar != null) {

				Bitmap image = BitmapFactory.DecodeByteArray (gravatar, 0, gravatar.Length);
				memberIcon.SetImageBitmap (Bitmap.CreateScaledBitmap(image, bitmapSize, bitmapSize, false));
				image.Recycle();
			}

//			string memberIconFilenam = list[position].Id.ToString () + ".png";
//			var dir = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), memberIconFilenam);
//			if (dir.Exists ()) {
//				try {
//					var bitmap = BitmapFactory.DecodeFile (dir.AbsolutePath);
//					var scaledBitmap = Bitmap.CreateScaledBitmap(bitmap, bitmap.Width/2, bitmap.Height/2, false);
//					memberIcon.SetImageBitmap (scaledBitmap);
//				} catch (Exception e) {
//					Log.Error ("Error", e.Message);
//				}
//			}

			return view;
		}

		public override int Count {
			get {
				return list.Count;
			}
		}

		#endregion

		int TextAppearanceHeight(int textAppearance) {

			TypedValue typedValue = new TypedValue(); 
			activity.Theme.ResolveAttribute(textAppearance, typedValue, true);
			int[] textSizeAttr = new int[] { Android.Resource.Attribute.TextSize };
			int indexOfAttrTextSize = 0;
			TypedArray a = activity.ObtainStyledAttributes(typedValue.Data, textSizeAttr);
			int textSize = a.GetDimensionPixelSize(indexOfAttrTextSize, -1);
			a.Recycle();

			return textSize;
		}

		Activity activity;
		List<CodeProjectMember> list;
	}
}

