using System;

namespace TMPro
{
	// Token: 0x0200001C RID: 28
	public static class TMP_Compatibility
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00002D88 File Offset: 0x00000F88
		public static TextAlignmentOptions ConvertTextAlignmentEnumValues(TextAlignmentOptions oldValue)
		{
			switch (oldValue)
			{
			case (TextAlignmentOptions)0:
				return TextAlignmentOptions.TopLeft;
			case (TextAlignmentOptions)1:
				return TextAlignmentOptions.Top;
			case (TextAlignmentOptions)2:
				return TextAlignmentOptions.TopRight;
			case (TextAlignmentOptions)3:
				return TextAlignmentOptions.TopJustified;
			case (TextAlignmentOptions)4:
				return TextAlignmentOptions.Left;
			case (TextAlignmentOptions)5:
				return TextAlignmentOptions.Center;
			case (TextAlignmentOptions)6:
				return TextAlignmentOptions.Right;
			case (TextAlignmentOptions)7:
				return TextAlignmentOptions.Justified;
			case (TextAlignmentOptions)8:
				return TextAlignmentOptions.BottomLeft;
			case (TextAlignmentOptions)9:
				return TextAlignmentOptions.Bottom;
			case (TextAlignmentOptions)10:
				return TextAlignmentOptions.BottomRight;
			case (TextAlignmentOptions)11:
				return TextAlignmentOptions.BottomJustified;
			case (TextAlignmentOptions)12:
				return TextAlignmentOptions.BaselineLeft;
			case (TextAlignmentOptions)13:
				return TextAlignmentOptions.Baseline;
			case (TextAlignmentOptions)14:
				return TextAlignmentOptions.BaselineRight;
			case (TextAlignmentOptions)15:
				return TextAlignmentOptions.BaselineJustified;
			case (TextAlignmentOptions)16:
				return TextAlignmentOptions.MidlineLeft;
			case (TextAlignmentOptions)17:
				return TextAlignmentOptions.Midline;
			case (TextAlignmentOptions)18:
				return TextAlignmentOptions.MidlineRight;
			case (TextAlignmentOptions)19:
				return TextAlignmentOptions.MidlineJustified;
			case (TextAlignmentOptions)20:
				return TextAlignmentOptions.CaplineLeft;
			case (TextAlignmentOptions)21:
				return TextAlignmentOptions.Capline;
			case (TextAlignmentOptions)22:
				return TextAlignmentOptions.CaplineRight;
			case (TextAlignmentOptions)23:
				return TextAlignmentOptions.CaplineJustified;
			default:
				return TextAlignmentOptions.TopLeft;
			}
		}

		// Token: 0x02000088 RID: 136
		public enum AnchorPositions
		{
			// Token: 0x040005F5 RID: 1525
			TopLeft,
			// Token: 0x040005F6 RID: 1526
			Top,
			// Token: 0x040005F7 RID: 1527
			TopRight,
			// Token: 0x040005F8 RID: 1528
			Left,
			// Token: 0x040005F9 RID: 1529
			Center,
			// Token: 0x040005FA RID: 1530
			Right,
			// Token: 0x040005FB RID: 1531
			BottomLeft,
			// Token: 0x040005FC RID: 1532
			Bottom,
			// Token: 0x040005FD RID: 1533
			BottomRight,
			// Token: 0x040005FE RID: 1534
			BaseLine,
			// Token: 0x040005FF RID: 1535
			None
		}
	}
}
