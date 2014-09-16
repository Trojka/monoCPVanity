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

namespace be.trojkasoftware.droidCPVanity
{
	[Activity (Label = "Settings", ParentActivity = typeof(MainActivity))]			
	public class CodeProjectSettingsActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			ActionBar.SetDisplayHomeAsUpEnabled (true);

			SetContentView (Resource.Layout.MainLayout);

			CodeProjectSettingsFragment codeProjectSettingsFragment = new CodeProjectSettingsFragment();
			this.FragmentManager.BeginTransaction()
				.Add(Resource.Id.frameLayoutFragmentContainer, codeProjectSettingsFragment).Commit();

		}
	}
}

