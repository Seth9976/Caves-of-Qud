using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000124 RID: 292
	[Flags]
	internal enum DeclarationModifiers : uint
	{
		// Token: 0x0400069F RID: 1695
		None = 0U,
		// Token: 0x040006A0 RID: 1696
		Abstract = 1U,
		// Token: 0x040006A1 RID: 1697
		Sealed = 2U,
		// Token: 0x040006A2 RID: 1698
		Static = 4U,
		// Token: 0x040006A3 RID: 1699
		New = 8U,
		// Token: 0x040006A4 RID: 1700
		Public = 16U,
		// Token: 0x040006A5 RID: 1701
		Protected = 32U,
		// Token: 0x040006A6 RID: 1702
		Internal = 64U,
		// Token: 0x040006A7 RID: 1703
		ProtectedInternal = 128U,
		// Token: 0x040006A8 RID: 1704
		Private = 256U,
		// Token: 0x040006A9 RID: 1705
		PrivateProtected = 512U,
		// Token: 0x040006AA RID: 1706
		ReadOnly = 1024U,
		// Token: 0x040006AB RID: 1707
		Const = 2048U,
		// Token: 0x040006AC RID: 1708
		Volatile = 4096U,
		// Token: 0x040006AD RID: 1709
		Extern = 8192U,
		// Token: 0x040006AE RID: 1710
		Partial = 16384U,
		// Token: 0x040006AF RID: 1711
		Unsafe = 32768U,
		// Token: 0x040006B0 RID: 1712
		Fixed = 65536U,
		// Token: 0x040006B1 RID: 1713
		Virtual = 131072U,
		// Token: 0x040006B2 RID: 1714
		Override = 262144U,
		// Token: 0x040006B3 RID: 1715
		Indexer = 524288U,
		// Token: 0x040006B4 RID: 1716
		Async = 1048576U,
		// Token: 0x040006B5 RID: 1717
		Ref = 2097152U,
		// Token: 0x040006B6 RID: 1718
		Data = 4194304U,
		// Token: 0x040006B7 RID: 1719
		All = 8388607U,
		// Token: 0x040006B8 RID: 1720
		Unset = 8388608U,
		// Token: 0x040006B9 RID: 1721
		AccessibilityMask = 1008U
	}
}
