using System;

namespace be.trojkasoftware.Ripit.Attributes
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class HttpSourceAttribute : Attribute
	{
		public HttpSourceAttribute (int id, string url)
		{
			Id = id;
			Url = url;
		}

		public HttpSourceAttribute (string url)
			:this(0, url)
		{
		}

		public int Id {
			get;
			private set;
		}

		public string Url {
			get;
			private set;
		}
	}
}

