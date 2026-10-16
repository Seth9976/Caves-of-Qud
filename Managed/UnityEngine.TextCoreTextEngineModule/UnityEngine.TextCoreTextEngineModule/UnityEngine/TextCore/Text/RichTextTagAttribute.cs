using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000053 RID: 83
	internal struct RichTextTagAttribute
	{
		// Token: 0x040002FC RID: 764
		public int nameHashCode;

		// Token: 0x040002FD RID: 765
		public int valueHashCode;

		// Token: 0x040002FE RID: 766
		public TagValueType valueType;

		// Token: 0x040002FF RID: 767
		public int valueStartIndex;

		// Token: 0x04000300 RID: 768
		public int valueLength;

		// Token: 0x04000301 RID: 769
		public TagUnitType unitType;
	}
}
