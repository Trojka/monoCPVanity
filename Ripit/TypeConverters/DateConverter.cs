using System;

namespace be.trojkasoftware.Ripit.TypeConverters
{
	public class DateConverter : Attribute
	{
		public DateConverter (string format)
		{
		}

		public string Format {
			private set;
			get;
		}
	}
}

