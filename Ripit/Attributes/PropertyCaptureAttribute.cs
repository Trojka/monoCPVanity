using System;

namespace be.trojkasoftware.Ripit.Attributes
{
	public class PropertyCaptureAttribute : Attribute, TextActionInterface
	{
		public PropertyCaptureAttribute (string regExCapture)
			:this(0, regExCapture, 0, false)
		{
		}

		public PropertyCaptureAttribute (int index, string regExCapture)
			:this(index, regExCapture, 0, false)
		{
		}

		public PropertyCaptureAttribute (string regExCapture, int group, bool isOptional)
			:this(0, regExCapture, group, isOptional)
		{
		}

		public PropertyCaptureAttribute (int index, string regExCapture, int group, bool isOptional)
		{
			Index = index;
			CaptureExpression = regExCapture;
			Group = group;
			IsOptional = isOptional;
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

