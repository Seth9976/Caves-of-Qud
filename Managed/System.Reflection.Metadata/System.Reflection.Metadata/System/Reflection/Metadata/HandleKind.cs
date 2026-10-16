using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200009D RID: 157
	public enum HandleKind : byte
	{
		// Token: 0x04000390 RID: 912
		ModuleDefinition,
		// Token: 0x04000391 RID: 913
		TypeReference,
		// Token: 0x04000392 RID: 914
		TypeDefinition,
		// Token: 0x04000393 RID: 915
		FieldDefinition = 4,
		// Token: 0x04000394 RID: 916
		MethodDefinition = 6,
		// Token: 0x04000395 RID: 917
		Parameter = 8,
		// Token: 0x04000396 RID: 918
		InterfaceImplementation,
		// Token: 0x04000397 RID: 919
		MemberReference,
		// Token: 0x04000398 RID: 920
		Constant,
		// Token: 0x04000399 RID: 921
		CustomAttribute,
		// Token: 0x0400039A RID: 922
		DeclarativeSecurityAttribute = 14,
		// Token: 0x0400039B RID: 923
		StandaloneSignature = 17,
		// Token: 0x0400039C RID: 924
		EventDefinition = 20,
		// Token: 0x0400039D RID: 925
		PropertyDefinition = 23,
		// Token: 0x0400039E RID: 926
		MethodImplementation = 25,
		// Token: 0x0400039F RID: 927
		ModuleReference,
		// Token: 0x040003A0 RID: 928
		TypeSpecification,
		// Token: 0x040003A1 RID: 929
		AssemblyDefinition = 32,
		// Token: 0x040003A2 RID: 930
		AssemblyFile = 38,
		// Token: 0x040003A3 RID: 931
		AssemblyReference = 35,
		// Token: 0x040003A4 RID: 932
		ExportedType = 39,
		// Token: 0x040003A5 RID: 933
		GenericParameter = 42,
		// Token: 0x040003A6 RID: 934
		MethodSpecification,
		// Token: 0x040003A7 RID: 935
		GenericParameterConstraint,
		// Token: 0x040003A8 RID: 936
		ManifestResource = 40,
		// Token: 0x040003A9 RID: 937
		Document = 48,
		// Token: 0x040003AA RID: 938
		MethodDebugInformation,
		// Token: 0x040003AB RID: 939
		LocalScope,
		// Token: 0x040003AC RID: 940
		LocalVariable,
		// Token: 0x040003AD RID: 941
		LocalConstant,
		// Token: 0x040003AE RID: 942
		ImportScope,
		// Token: 0x040003AF RID: 943
		CustomDebugInformation = 55,
		// Token: 0x040003B0 RID: 944
		NamespaceDefinition = 124,
		// Token: 0x040003B1 RID: 945
		UserString = 112,
		// Token: 0x040003B2 RID: 946
		String = 120,
		// Token: 0x040003B3 RID: 947
		Blob = 113,
		// Token: 0x040003B4 RID: 948
		Guid
	}
}
