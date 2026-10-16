using System;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004D1 RID: 1233
	[Flags]
	internal enum CompletionPart
	{
		// Token: 0x040019FC RID: 6652
		None = 0,
		// Token: 0x040019FD RID: 6653
		Attributes = 1,
		// Token: 0x040019FE RID: 6654
		ReturnTypeAttributes = 2,
		// Token: 0x040019FF RID: 6655
		Parameters = 4,
		// Token: 0x04001A00 RID: 6656
		Type = 8,
		// Token: 0x04001A01 RID: 6657
		StartBaseType = 16,
		// Token: 0x04001A02 RID: 6658
		FinishBaseType = 32,
		// Token: 0x04001A03 RID: 6659
		StartInterfaces = 64,
		// Token: 0x04001A04 RID: 6660
		FinishInterfaces = 128,
		// Token: 0x04001A05 RID: 6661
		EnumUnderlyingType = 256,
		// Token: 0x04001A06 RID: 6662
		TypeArguments = 512,
		// Token: 0x04001A07 RID: 6663
		TypeParameters = 1024,
		// Token: 0x04001A08 RID: 6664
		Members = 2048,
		// Token: 0x04001A09 RID: 6665
		TypeMembers = 4096,
		// Token: 0x04001A0A RID: 6666
		SynthesizedExplicitImplementations = 8192,
		// Token: 0x04001A0B RID: 6667
		StartMemberChecks = 16384,
		// Token: 0x04001A0C RID: 6668
		FinishMemberChecks = 32768,
		// Token: 0x04001A0D RID: 6669
		MembersCompleted = 65536,
		// Token: 0x04001A0E RID: 6670
		All = 131071,
		// Token: 0x04001A0F RID: 6671
		NamedTypeSymbolWithLocationAll = 65521,
		// Token: 0x04001A10 RID: 6672
		NamedTypeSymbolAll = 131057,
		// Token: 0x04001A11 RID: 6673
		StartValidatingImports = 16,
		// Token: 0x04001A12 RID: 6674
		FinishValidatingImports = 32,
		// Token: 0x04001A13 RID: 6675
		ImportsAll = 48,
		// Token: 0x04001A14 RID: 6676
		NameToMembersMap = 2048,
		// Token: 0x04001A15 RID: 6677
		NamespaceSymbolAll = 67584,
		// Token: 0x04001A16 RID: 6678
		FixedSize = 2048,
		// Token: 0x04001A17 RID: 6679
		ConstantValue = 4096,
		// Token: 0x04001A18 RID: 6680
		FieldSymbolAll = 6153,
		// Token: 0x04001A19 RID: 6681
		StartAsyncMethodChecks = 2048,
		// Token: 0x04001A1A RID: 6682
		FinishAsyncMethodChecks = 4096,
		// Token: 0x04001A1B RID: 6683
		StartMethodChecks = 8192,
		// Token: 0x04001A1C RID: 6684
		FinishMethodChecks = 16384,
		// Token: 0x04001A1D RID: 6685
		MethodSymbolAll = 31759,
		// Token: 0x04001A1E RID: 6686
		StartDefaultSyntaxValue = 2048,
		// Token: 0x04001A1F RID: 6687
		EndDefaultSyntaxValue = 4096,
		// Token: 0x04001A20 RID: 6688
		EndDefaultSyntaxValueDiagnostics = 8192,
		// Token: 0x04001A21 RID: 6689
		ComplexParameterSymbolAll = 14337,
		// Token: 0x04001A22 RID: 6690
		TypeParameterConstraints = 2048,
		// Token: 0x04001A23 RID: 6691
		TypeParameterSymbolAll = 2049,
		// Token: 0x04001A24 RID: 6692
		StartPropertyEnsureSignature = 16,
		// Token: 0x04001A25 RID: 6693
		FinishPropertyEnsureSignature = 32,
		// Token: 0x04001A26 RID: 6694
		StartPropertyParameters = 64,
		// Token: 0x04001A27 RID: 6695
		FinishPropertyParameters = 128,
		// Token: 0x04001A28 RID: 6696
		StartPropertyType = 256,
		// Token: 0x04001A29 RID: 6697
		FinishPropertyType = 512,
		// Token: 0x04001A2A RID: 6698
		PropertySymbolAll = 1009,
		// Token: 0x04001A2B RID: 6699
		AliasTarget = 16,
		// Token: 0x04001A2C RID: 6700
		StartAttributeChecks = 16,
		// Token: 0x04001A2D RID: 6701
		FinishAttributeChecks = 32,
		// Token: 0x04001A2E RID: 6702
		Module = 64,
		// Token: 0x04001A2F RID: 6703
		StartValidatingAddedModules = 256,
		// Token: 0x04001A30 RID: 6704
		FinishValidatingAddedModules = 512,
		// Token: 0x04001A31 RID: 6705
		AssemblySymbolAll = 881,
		// Token: 0x04001A32 RID: 6706
		StartValidatingReferencedAssemblies = 16,
		// Token: 0x04001A33 RID: 6707
		FinishValidatingReferencedAssemblies = 32
	}
}
