using System;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200015F RID: 351
	[Flags]
	internal enum CustomAttributeValueTreatment : byte
	{
		// Token: 0x040008C8 RID: 2248
		None = 0,
		// Token: 0x040008C9 RID: 2249
		AttributeUsageAllowSingle = 1,
		// Token: 0x040008CA RID: 2250
		AttributeUsageAllowMultiple = 2,
		// Token: 0x040008CB RID: 2251
		AttributeUsageVersionAttribute = 3,
		// Token: 0x040008CC RID: 2252
		AttributeUsageDeprecatedAttribute = 4
	}
}
