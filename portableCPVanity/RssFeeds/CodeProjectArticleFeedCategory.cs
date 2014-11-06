namespace be.trojkasoftware.portableCPVanity.RssFeeds
{
	public class CodeProjectArticleFeedCategory
	{
		public CodeProjectArticleFeedCategory ()
		{
		}

		public int Id {
			get;
			set;
		}

		public string Name {
			get;
			set;
		}

		public override string ToString ()
		{
			return Name;
		}
	}
}

