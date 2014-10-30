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
using be.trojkasoftware.droidCPVanity.Util;
using System.Threading.Tasks;
using Android.Graphics;
using be.trojkasoftware.monoCPVanity.Data;

namespace be.trojkasoftware.droidCPVanity
{
	[Activity (Label = "CPVanity", ParentActivity = typeof(CodeProjectMemberArticlesActivity))]			
	public class CodeProjectMemberReputationActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			ActionBar.SetDisplayHomeAsUpEnabled (true);

			SetContentView (Resource.Layout.CodeProjectMemberReputationLayout);

			memberReputationGraph = FindViewById<ImageView> (Resource.Id.imageViewReputationGraph);

			MemberReputationGraph = Intent.Extras.GetString (CodeProjectMemberProfileActivity.MemberReputationGraphKey);

			WebImageRetriever imageDownloader = new WebImageRetriever ();
			Task<Bitmap> loadGraphTask = imageDownloader.GetImageAsync (new Uri (MemberReputationGraph));

			var context = TaskScheduler.FromCurrentSynchronizationContext();

			loadGraphTask.ContinueWith (t => ReputationGraphLoaded(t.Result), context);
		}

		public void ReputationGraphLoaded(Bitmap graph) {

			this.memberReputationGraph.SetImageBitmap(graph);
		}

		public string MemberReputationGraph {
			get;
			set;
		}
			
		ImageView memberReputationGraph;
	}
}

