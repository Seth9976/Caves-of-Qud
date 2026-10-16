using System;

namespace Internal.Cryptography.Pal
{
	// Token: 0x0200011D RID: 285
	internal enum GeneralNameType
	{
		// Token: 0x040004CC RID: 1228
		OtherName,
		// Token: 0x040004CD RID: 1229
		Rfc822Name,
		// Token: 0x040004CE RID: 1230
		Email = 1,
		// Token: 0x040004CF RID: 1231
		DnsName,
		// Token: 0x040004D0 RID: 1232
		X400Address,
		// Token: 0x040004D1 RID: 1233
		DirectoryName,
		// Token: 0x040004D2 RID: 1234
		EdiPartyName,
		// Token: 0x040004D3 RID: 1235
		UniformResourceIdentifier,
		// Token: 0x040004D4 RID: 1236
		IPAddress,
		// Token: 0x040004D5 RID: 1237
		RegisteredId
	}
}
