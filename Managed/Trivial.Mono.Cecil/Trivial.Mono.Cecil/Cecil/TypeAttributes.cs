using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000BB RID: 187
	[Flags]
	public enum TypeAttributes : uint
	{
		// Token: 0x040002A6 RID: 678
		VisibilityMask = 7U,
		// Token: 0x040002A7 RID: 679
		NotPublic = 0U,
		// Token: 0x040002A8 RID: 680
		Public = 1U,
		// Token: 0x040002A9 RID: 681
		NestedPublic = 2U,
		// Token: 0x040002AA RID: 682
		NestedPrivate = 3U,
		// Token: 0x040002AB RID: 683
		NestedFamily = 4U,
		// Token: 0x040002AC RID: 684
		NestedAssembly = 5U,
		// Token: 0x040002AD RID: 685
		NestedFamANDAssem = 6U,
		// Token: 0x040002AE RID: 686
		NestedFamORAssem = 7U,
		// Token: 0x040002AF RID: 687
		LayoutMask = 24U,
		// Token: 0x040002B0 RID: 688
		AutoLayout = 0U,
		// Token: 0x040002B1 RID: 689
		SequentialLayout = 8U,
		// Token: 0x040002B2 RID: 690
		ExplicitLayout = 16U,
		// Token: 0x040002B3 RID: 691
		ClassSemanticMask = 32U,
		// Token: 0x040002B4 RID: 692
		Class = 0U,
		// Token: 0x040002B5 RID: 693
		Interface = 32U,
		// Token: 0x040002B6 RID: 694
		Abstract = 128U,
		// Token: 0x040002B7 RID: 695
		Sealed = 256U,
		// Token: 0x040002B8 RID: 696
		SpecialName = 1024U,
		// Token: 0x040002B9 RID: 697
		Import = 4096U,
		// Token: 0x040002BA RID: 698
		Serializable = 8192U,
		// Token: 0x040002BB RID: 699
		WindowsRuntime = 16384U,
		// Token: 0x040002BC RID: 700
		StringFormatMask = 196608U,
		// Token: 0x040002BD RID: 701
		AnsiClass = 0U,
		// Token: 0x040002BE RID: 702
		UnicodeClass = 65536U,
		// Token: 0x040002BF RID: 703
		AutoClass = 131072U,
		// Token: 0x040002C0 RID: 704
		BeforeFieldInit = 1048576U,
		// Token: 0x040002C1 RID: 705
		RTSpecialName = 2048U,
		// Token: 0x040002C2 RID: 706
		HasSecurity = 262144U,
		// Token: 0x040002C3 RID: 707
		Forwarder = 2097152U
	}
}
