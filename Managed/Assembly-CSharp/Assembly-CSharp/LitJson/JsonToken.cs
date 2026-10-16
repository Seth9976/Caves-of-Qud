using System;

namespace LitJson
{
	// Token: 0x02000328 RID: 808
	public enum JsonToken
	{
		// Token: 0x04000F9D RID: 3997
		None,
		// Token: 0x04000F9E RID: 3998
		ObjectStart,
		// Token: 0x04000F9F RID: 3999
		PropertyName,
		// Token: 0x04000FA0 RID: 4000
		ObjectEnd,
		// Token: 0x04000FA1 RID: 4001
		ArrayStart,
		// Token: 0x04000FA2 RID: 4002
		ArrayEnd,
		// Token: 0x04000FA3 RID: 4003
		Int,
		// Token: 0x04000FA4 RID: 4004
		Long,
		// Token: 0x04000FA5 RID: 4005
		Double,
		// Token: 0x04000FA6 RID: 4006
		String,
		// Token: 0x04000FA7 RID: 4007
		Boolean,
		// Token: 0x04000FA8 RID: 4008
		Null
	}
}
