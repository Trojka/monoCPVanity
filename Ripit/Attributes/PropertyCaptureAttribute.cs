using System;

namespace be.trojkasoftware.Ripit.Attributes
{
	public class PropertyCaptureAttribute : Attribute //, TextActionInterface
	{
		public PropertyCaptureAttribute (string regExCapture, int group, bool isOptional)
		{
			CaptureExpression = regExCapture;
			Group = group;
			IsOptional = isOptional;
		}

		public string CaptureExpression {
			get;
			private set;
		}

		public int Group {
			get;
			private set;
		}

		public bool IsOptional {
			get;
			private set;
		}
	}
}

