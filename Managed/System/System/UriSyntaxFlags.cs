using System;

namespace System
{
	// Token: 0x0200016E RID: 366
	[Flags]
	internal enum UriSyntaxFlags
	{
		// Token: 0x040006A5 RID: 1701
		None = 0,
		// Token: 0x040006A6 RID: 1702
		MustHaveAuthority = 1,
		// Token: 0x040006A7 RID: 1703
		OptionalAuthority = 2,
		// Token: 0x040006A8 RID: 1704
		MayHaveUserInfo = 4,
		// Token: 0x040006A9 RID: 1705
		MayHavePort = 8,
		// Token: 0x040006AA RID: 1706
		MayHavePath = 16,
		// Token: 0x040006AB RID: 1707
		MayHaveQuery = 32,
		// Token: 0x040006AC RID: 1708
		MayHaveFragment = 64,
		// Token: 0x040006AD RID: 1709
		AllowEmptyHost = 128,
		// Token: 0x040006AE RID: 1710
		AllowUncHost = 256,
		// Token: 0x040006AF RID: 1711
		AllowDnsHost = 512,
		// Token: 0x040006B0 RID: 1712
		AllowIPv4Host = 1024,
		// Token: 0x040006B1 RID: 1713
		AllowIPv6Host = 2048,
		// Token: 0x040006B2 RID: 1714
		AllowAnInternetHost = 3584,
		// Token: 0x040006B3 RID: 1715
		AllowAnyOtherHost = 4096,
		// Token: 0x040006B4 RID: 1716
		FileLikeUri = 8192,
		// Token: 0x040006B5 RID: 1717
		MailToLikeUri = 16384,
		// Token: 0x040006B6 RID: 1718
		V1_UnknownUri = 65536,
		// Token: 0x040006B7 RID: 1719
		SimpleUserSyntax = 131072,
		// Token: 0x040006B8 RID: 1720
		BuiltInSyntax = 262144,
		// Token: 0x040006B9 RID: 1721
		ParserSchemeOnly = 524288,
		// Token: 0x040006BA RID: 1722
		AllowDOSPath = 1048576,
		// Token: 0x040006BB RID: 1723
		PathIsRooted = 2097152,
		// Token: 0x040006BC RID: 1724
		ConvertPathSlashes = 4194304,
		// Token: 0x040006BD RID: 1725
		CompressPath = 8388608,
		// Token: 0x040006BE RID: 1726
		CanonicalizeAsFilePath = 16777216,
		// Token: 0x040006BF RID: 1727
		UnEscapeDotsAndSlashes = 33554432,
		// Token: 0x040006C0 RID: 1728
		AllowIdn = 67108864,
		// Token: 0x040006C1 RID: 1729
		AllowIriParsing = 268435456
	}
}
