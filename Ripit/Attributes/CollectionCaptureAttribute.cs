using System;

namespace be.trojkasoftware.Ripit.Attributes
{
	public class CollectionCaptureAttribute : Attribute
	{
		public CollectionCaptureAttribute (string regExCapture)
		{
			CaptureExpression = regExCapture;
		}

		public string CaptureExpression {
			get;
			private set;
		}	}
}

