using System;

namespace Mono.Btls
{
	// Token: 0x02000111 RID: 273
	[Flags]
	internal enum MonoBtlsX509TrustKind
	{
		// Token: 0x04000483 RID: 1155
		DEFAULT = 0,
		// Token: 0x04000484 RID: 1156
		TRUST_CLIENT = 1,
		// Token: 0x04000485 RID: 1157
		TRUST_SERVER = 2,
		// Token: 0x04000486 RID: 1158
		TRUST_ALL = 4,
		// Token: 0x04000487 RID: 1159
		REJECT_CLIENT = 32,
		// Token: 0x04000488 RID: 1160
		REJECT_SERVER = 64,
		// Token: 0x04000489 RID: 1161
		REJECT_ALL = 128
	}
}
