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
using Android.Preferences;
using be.trojkasoftware.portableCPVanity;
using be.trojkasoftware.Ripit.Core;
using be.trojkasoftware.monoCPVanity.Data;

namespace be.trojkasoftware.droidCPVanity
{
	public class CodeProjectSettingsFragment : PreferenceFragment, ISharedPreferencesOnSharedPreferenceChangeListener
	{
		public override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			AddPreferencesFromResource (Resource.Xml.preferences);
		}

		public override void OnResume()
		{
			base.OnResume();

			Update ();

			PreferenceScreen.SharedPreferences.RegisterOnSharedPreferenceChangeListener(this);
		}

		public override void OnPause()
		{
			base.OnPause();
			PreferenceScreen.SharedPreferences.
			UnregisterOnSharedPreferenceChangeListener(this);
		}

		public void OnSharedPreferenceChanged(ISharedPreferences sharedPreferences, string key)
		{
			Update (key);
		}

		private void Update(string key = null)
		{
			if(string.IsNullOrEmpty(key) || key == "me_id") {
				EditTextPreference id = (EditTextPreference)FindPreference ("me_id");
				string memberIsAsString = id.Text;

				if (!string.IsNullOrEmpty (memberIsAsString)) {
					int memberId = int.Parse (memberIsAsString);
					CodeProjectMember member = new CodeProjectMember ();
					member.Id = memberId;

					Dictionary<String, String> param = new Dictionary<string, string> ();
					param.Add ("Id", memberIsAsString);

					ObjectBuilder objectBuilder = new ObjectBuilder ();
					objectBuilder.Fill (member, param);

					CodeProjectDatabase db = new CodeProjectDatabase ();
					db.AddMember (member, true);

					id.Summary = "Your Id is " + id.Text;
				}
			}
		}

	}
}

