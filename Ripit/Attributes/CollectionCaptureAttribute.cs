using System;

namespace be.trojkasoftware.Ripit.Attributes
{
	public class CollectionCaptureAttribute : Attribute, TextActionInterface
	{
		public CollectionCaptureAttribute (string regExCapture)
			:this(0, regExCapture)
		{
		}

		public CollectionCaptureAttribute (int index, string regExCapture)
		{
			Index = index;
			CaptureExpression = regExCapture;
		}

		#region TextActionInterface implementation

		public int Index {
			get;
			private set;
		}

		#endregion

		public string CaptureExpression {
			get;
			private set;
		}	}
}

