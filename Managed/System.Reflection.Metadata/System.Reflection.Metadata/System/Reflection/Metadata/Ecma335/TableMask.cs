using System;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200011E RID: 286
	[Flags]
	internal enum TableMask : ulong
	{
		// Token: 0x04000681 RID: 1665
		Module = 1UL,
		// Token: 0x04000682 RID: 1666
		TypeRef = 2UL,
		// Token: 0x04000683 RID: 1667
		TypeDef = 4UL,
		// Token: 0x04000684 RID: 1668
		FieldPtr = 8UL,
		// Token: 0x04000685 RID: 1669
		Field = 16UL,
		// Token: 0x04000686 RID: 1670
		MethodPtr = 32UL,
		// Token: 0x04000687 RID: 1671
		MethodDef = 64UL,
		// Token: 0x04000688 RID: 1672
		ParamPtr = 128UL,
		// Token: 0x04000689 RID: 1673
		Param = 256UL,
		// Token: 0x0400068A RID: 1674
		InterfaceImpl = 512UL,
		// Token: 0x0400068B RID: 1675
		MemberRef = 1024UL,
		// Token: 0x0400068C RID: 1676
		Constant = 2048UL,
		// Token: 0x0400068D RID: 1677
		CustomAttribute = 4096UL,
		// Token: 0x0400068E RID: 1678
		FieldMarshal = 8192UL,
		// Token: 0x0400068F RID: 1679
		DeclSecurity = 16384UL,
		// Token: 0x04000690 RID: 1680
		ClassLayout = 32768UL,
		// Token: 0x04000691 RID: 1681
		FieldLayout = 65536UL,
		// Token: 0x04000692 RID: 1682
		StandAloneSig = 131072UL,
		// Token: 0x04000693 RID: 1683
		EventMap = 262144UL,
		// Token: 0x04000694 RID: 1684
		EventPtr = 524288UL,
		// Token: 0x04000695 RID: 1685
		Event = 1048576UL,
		// Token: 0x04000696 RID: 1686
		PropertyMap = 2097152UL,
		// Token: 0x04000697 RID: 1687
		PropertyPtr = 4194304UL,
		// Token: 0x04000698 RID: 1688
		Property = 8388608UL,
		// Token: 0x04000699 RID: 1689
		MethodSemantics = 16777216UL,
		// Token: 0x0400069A RID: 1690
		MethodImpl = 33554432UL,
		// Token: 0x0400069B RID: 1691
		ModuleRef = 67108864UL,
		// Token: 0x0400069C RID: 1692
		TypeSpec = 134217728UL,
		// Token: 0x0400069D RID: 1693
		ImplMap = 268435456UL,
		// Token: 0x0400069E RID: 1694
		FieldRva = 536870912UL,
		// Token: 0x0400069F RID: 1695
		EnCLog = 1073741824UL,
		// Token: 0x040006A0 RID: 1696
		EnCMap = 2147483648UL,
		// Token: 0x040006A1 RID: 1697
		Assembly = 4294967296UL,
		// Token: 0x040006A2 RID: 1698
		AssemblyRef = 34359738368UL,
		// Token: 0x040006A3 RID: 1699
		File = 274877906944UL,
		// Token: 0x040006A4 RID: 1700
		ExportedType = 549755813888UL,
		// Token: 0x040006A5 RID: 1701
		ManifestResource = 1099511627776UL,
		// Token: 0x040006A6 RID: 1702
		NestedClass = 2199023255552UL,
		// Token: 0x040006A7 RID: 1703
		GenericParam = 4398046511104UL,
		// Token: 0x040006A8 RID: 1704
		MethodSpec = 8796093022208UL,
		// Token: 0x040006A9 RID: 1705
		GenericParamConstraint = 17592186044416UL,
		// Token: 0x040006AA RID: 1706
		Document = 281474976710656UL,
		// Token: 0x040006AB RID: 1707
		MethodDebugInformation = 562949953421312UL,
		// Token: 0x040006AC RID: 1708
		LocalScope = 1125899906842624UL,
		// Token: 0x040006AD RID: 1709
		LocalVariable = 2251799813685248UL,
		// Token: 0x040006AE RID: 1710
		LocalConstant = 4503599627370496UL,
		// Token: 0x040006AF RID: 1711
		ImportScope = 9007199254740992UL,
		// Token: 0x040006B0 RID: 1712
		StateMachineMethod = 18014398509481984UL,
		// Token: 0x040006B1 RID: 1713
		CustomDebugInformation = 36028797018963968UL,
		// Token: 0x040006B2 RID: 1714
		PtrTables = 4718760UL,
		// Token: 0x040006B3 RID: 1715
		EncTables = 3221225472UL,
		// Token: 0x040006B4 RID: 1716
		TypeSystemTables = 34952443854847UL,
		// Token: 0x040006B5 RID: 1717
		DebugTables = 71776119061217280UL,
		// Token: 0x040006B6 RID: 1718
		AllTables = 71811071505072127UL,
		// Token: 0x040006B7 RID: 1719
		ValidPortablePdbExternalTables = 34949217910615UL
	}
}
