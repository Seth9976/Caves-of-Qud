using System;

namespace UnityEngine
{
	// Token: 0x02000021 RID: 33
	public sealed class GUILayoutOption
	{
		// Token: 0x06000288 RID: 648 RVA: 0x0000A233 File Offset: 0x00008433
		internal GUILayoutOption(GUILayoutOption.Type type, object value)
		{
			this.type = type;
			this.value = value;
		}

		// Token: 0x04000087 RID: 135
		internal GUILayoutOption.Type type;

		// Token: 0x04000088 RID: 136
		internal object value;

		// Token: 0x02000022 RID: 34
		internal enum Type
		{
			// Token: 0x0400008A RID: 138
			fixedWidth,
			// Token: 0x0400008B RID: 139
			fixedHeight,
			// Token: 0x0400008C RID: 140
			minWidth,
			// Token: 0x0400008D RID: 141
			maxWidth,
			// Token: 0x0400008E RID: 142
			minHeight,
			// Token: 0x0400008F RID: 143
			maxHeight,
			// Token: 0x04000090 RID: 144
			stretchWidth,
			// Token: 0x04000091 RID: 145
			stretchHeight,
			// Token: 0x04000092 RID: 146
			alignStart,
			// Token: 0x04000093 RID: 147
			alignMiddle,
			// Token: 0x04000094 RID: 148
			alignEnd,
			// Token: 0x04000095 RID: 149
			alignJustify,
			// Token: 0x04000096 RID: 150
			equalSize,
			// Token: 0x04000097 RID: 151
			spacing
		}
	}
}
