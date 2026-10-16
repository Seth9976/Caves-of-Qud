using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020006E9 RID: 1769
	[DebuggerDisplay("id = {id}, keyword = {keyword}, number = {number}, boolean = {boolean}, color = {color}, object = {resource}")]
	[StructLayout(LayoutKind.Explicit)]
	internal struct StyleValue
	{
		// Token: 0x04001A0C RID: 6668
		[FieldOffset(0)]
		public StylePropertyId id;

		// Token: 0x04001A0D RID: 6669
		[FieldOffset(4)]
		public StyleKeyword keyword;

		// Token: 0x04001A0E RID: 6670
		[FieldOffset(8)]
		public float number;

		// Token: 0x04001A0F RID: 6671
		[FieldOffset(8)]
		public Length length;

		// Token: 0x04001A10 RID: 6672
		[FieldOffset(8)]
		public Color color;

		// Token: 0x04001A11 RID: 6673
		[FieldOffset(8)]
		public GCHandle resource;

		// Token: 0x04001A12 RID: 6674
		[FieldOffset(8)]
		public BackgroundPosition position;

		// Token: 0x04001A13 RID: 6675
		[FieldOffset(8)]
		public BackgroundRepeat repeat;
	}
}
