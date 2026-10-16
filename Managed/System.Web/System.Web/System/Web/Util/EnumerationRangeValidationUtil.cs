using System;
using System.Web.UI.WebControls;

namespace System.Web.Util
{
	// Token: 0x02000115 RID: 277
	internal static class EnumerationRangeValidationUtil
	{
		// Token: 0x06000DE8 RID: 3560 RVA: 0x00025627 File Offset: 0x00023827
		public static void ValidateRepeatLayout(RepeatLayout value)
		{
			if (value < RepeatLayout.Table || value > RepeatLayout.OrderedList)
			{
				throw new ArgumentOutOfRangeException("value");
			}
		}
	}
}
