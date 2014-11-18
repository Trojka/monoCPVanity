using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using be.trojkasoftware.portableCPVanity;
using be.trojkasoftware.monoCPVanity.Data;
using be.trojkasoftware.monoCPVanity.Util;
using be.trojkasoftware.portableCPVanity.ViewModels;

namespace be.trojkasoftware.droidCPVanity
{
	[Activity (Label = "CPVanity")]			
	public class CodeProjectMemberReputationActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.CodeProjectMemberReputationLayout);

			memberReputationGraph = FindViewById<ImageView> (Resource.Id.imageViewReputationGraph);
			memberReputationGraph.SetImageBitmap (null);

			spinner = this.FindViewById<ProgressBar>(Resource.Id.progressBar1);
			spinner.Visibility = ViewStates.Gone;

			viewModel = new CodeProjectMemberReputationViewModel ();
			viewModel.ReputationGraphLoaded += this.ReputationGraphLoaded;

			viewModel.MemberReputationGraphUrl = Intent.Extras.GetString (CodeProjectMemberProfileActivity.MemberReputationGraphKey);

			spinner.Visibility = ViewStates.Visible;

			var context = TaskScheduler.FromCurrentSynchronizationContext();

			viewModel.LoadMemberReputation (context);
		}

		public void ReputationGraphLoaded(byte[] graph) {

			spinner.Visibility = ViewStates.Gone;

			Bitmap bitmap = BitmapFactory.DecodeByteArray (graph, 0, graph.Length);
			this.memberReputationGraph.SetImageBitmap(bitmap);
		}

		public string MemberReputationGraph {
			get;
			set;
		}
			
		ImageView memberReputationGraph;
		ProgressBar spinner;

		CodeProjectMemberReputationViewModel viewModel;
	}
}

