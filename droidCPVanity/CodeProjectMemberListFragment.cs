using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

//using be.trojkasoftware.Ripit.Core;
using be.trojkasoftware.portableCPVanity;
using be.trojkasoftware.monoCPVanity.Data;

namespace be.trojkasoftware.droidCPVanity
{
	public class CodeProjectMemberListFragment : ListFragment
	{
		public override void OnActivityCreated(Bundle savedInstanceState) {
			base.OnActivityCreated(savedInstanceState);

			CodeProjectDatabase db = new CodeProjectDatabase ();
			members = db.GetMembers();

			ListAdapter = new CodeProjectMemberAdapter(this.Activity, members);

		}

		public override void OnListItemClick (ListView l, View v, int position, long id)
		{
			var intent = new Intent (this.Activity, typeof(CodeProjectMemberDetailActivity));

			Bundle bundle = new Bundle ();
			bundle.PutInt (CodeProjectMemberDetailActivity.MemberId, members[position].Id);

			intent.PutExtras(bundle);

			StartActivity (intent);
		}

		List<CodeProjectMember> members;
	}
}

