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
using be.trojkasoftware.portableCPVanity;
using be.trojkasoftware.monoCPVanity.Data;

namespace be.trojkasoftware.droidCPVanity
{
	public class CodeProjectMemberListFragment : ListFragment
	{
		public override void OnActivityCreated(Bundle savedInstanceState) {
			base.OnActivityCreated(savedInstanceState);
			RegisterForContextMenu(ListView);

			LoadMembers ();
		}

		public override void OnListItemClick (ListView l, View v, int position, long id)
		{
			var intent = new Intent (this.Activity, typeof(CodeProjectMemberProfileActivity));

			Bundle bundle = new Bundle ();
			bundle.PutInt (CodeProjectMemberProfileActivity.MemberIdKey, members[position].Id);

			intent.PutExtras(bundle);

			StartActivity (intent);
		}

		public override void OnCreateContextMenu (IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
		{
			menu.Add ("Delete");
		}

		public override bool OnContextItemSelected (IMenuItem item)
		{
			AdapterView.AdapterContextMenuInfo info = (AdapterView.AdapterContextMenuInfo)item.MenuInfo;
			CodeProjectMember memberToDelete = members [info.Position];
			CodeProjectDatabase db = new CodeProjectDatabase ();
			db.DeleteMember (memberToDelete.Id);

			members = db.GetMembers ();
			ListAdapter = new CodeProjectMemberAdapter (this.Activity, members);

			return true;
		}

		public void LoadMembers() {
			CodeProjectDatabase db = new CodeProjectDatabase ();
			members = db.GetMembers ();

			ListAdapter = new CodeProjectMemberAdapter (this.Activity, members);
		}

		List<CodeProjectMember> members;
	}
}

