using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Preferences;


namespace be.trojkasoftware.droidCPVanity
{
	[Activity (Label = "droidCPVanity", MainLauncher = true)]
	[MetaData ("android.app.default_searchable", Value = "be.trojkasoftware.droidcpvanity.CodeProjectMemberProfileActivity")]
	public class MainActivity : Activity, ActionBar.IOnNavigationListener
	{
		enum Screen {
			MemberList,
			ArticleFeed,
			LoungFeed
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.MainLayout);

			String[] data = new String[]{ "Members", "Articles", "Community" };
			ArrayAdapter list = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, data);

			ActionBar.SetDisplayShowTitleEnabled (false);
			ActionBar.NavigationMode = ActionBarNavigationMode.List;
			ActionBar.SetListNavigationCallbacks (list, this);

			CodeProjectMemberListFragment codeProjectMemberListFragment = new CodeProjectMemberListFragment();
			this.FragmentManager.BeginTransaction()
				.Add(Resource.Id.frameLayoutFragmentContainer, codeProjectMemberListFragment).Commit();

			currentScreen = Screen.MemberList;
		}

		public bool OnNavigationItemSelected (int itemPosition, long itemId)
		{
			Fragment fragment = null;
			switch (itemPosition)
			{
			case 0:
				fragment = new CodeProjectMemberListFragment ();
				currentScreen = Screen.MemberList;
				break;
			case 1:
				fragment = new CodeProjectArticleFeedFragment ();
				currentScreen = Screen.ArticleFeed;
				break;
			case 2:
				fragment = new CodeProjectLoungeFeedFragment ();
				currentScreen = Screen.LoungFeed;
				break;
			}

			this.FragmentManager.BeginTransaction()
				.Replace(Resource.Id.frameLayoutFragmentContainer, fragment).Commit();

			this.InvalidateOptionsMenu ();

			return true;
		}

		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater inflater = this.MenuInflater;
			inflater.Inflate(Resource.Menu.main_actions, menu);

			SearchManager searchManager = (SearchManager) GetSystemService(Context.SearchService);
			SearchView searchView = (SearchView) menu.FindItem(Resource.Id.action_search).ActionView;
			searchView.SetSearchableInfo(searchManager.GetSearchableInfo(ComponentName));

			return base.OnCreateOptionsMenu (menu);
		}

		public override bool OnPrepareOptionsMenu (IMenu menu) {

			if(currentScreen == Screen.MemberList) {
				menu.FindItem(Resource.Id.action_search).SetVisible(true);
				menu.FindItem(Resource.Id.action_category).SetVisible(false);
			}
			else {
				menu.FindItem(Resource.Id.action_search).SetVisible(false);
				menu.FindItem(Resource.Id.action_category).SetVisible(true);
			}

			return true;
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			switch (item.ItemId) {
			case Resource.Id.action_search:
				return true;
			case Resource.Id.action_category:
				Fragment fragment = this.FragmentManager.FindFragmentById (Resource.Id.frameLayoutFragmentContainer);
				if (fragment is CodeProjectRSSFeedFragment) {
					(fragment as CodeProjectRSSFeedFragment).SelectCategory ();
				}
				return true;
			default:
				return base.OnOptionsItemSelected(item);
			}
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			Fragment fragment = this.FragmentManager.FindFragmentById (Resource.Id.frameLayoutFragmentContainer);
			if (fragment is CodeProjectMemberListFragment) {
				(fragment as CodeProjectMemberListFragment).LoadMembers ();
			}

			base.OnActivityResult (requestCode, resultCode, data);
		}

		private Screen currentScreen = Screen.MemberList;
	}
}


