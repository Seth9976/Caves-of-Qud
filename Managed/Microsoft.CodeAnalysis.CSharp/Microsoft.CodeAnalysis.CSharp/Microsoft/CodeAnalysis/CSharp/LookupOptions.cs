using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000035 RID: 53
	[Flags]
	internal enum LookupOptions
	{
		// Token: 0x04000099 RID: 153
		Default = 0,
		// Token: 0x0400009A RID: 154
		NamespaceAliasesOnly = 2,
		// Token: 0x0400009B RID: 155
		NamespacesOrTypesOnly = 4,
		// Token: 0x0400009C RID: 156
		MustBeInvocableIfMember = 8,
		// Token: 0x0400009D RID: 157
		MustBeInstance = 16,
		// Token: 0x0400009E RID: 158
		MustNotBeInstance = 32,
		// Token: 0x0400009F RID: 159
		MustNotBeNamespace = 64,
		// Token: 0x040000A0 RID: 160
		AllMethodsOnArityZero = 128,
		// Token: 0x040000A1 RID: 161
		LabelsOnly = 256,
		// Token: 0x040000A2 RID: 162
		UseBaseReferenceAccessibility = 512,
		// Token: 0x040000A3 RID: 163
		IncludeExtensionMethods = 1024,
		// Token: 0x040000A4 RID: 164
		AttributeTypeOnly = 2052,
		// Token: 0x040000A5 RID: 165
		VerbatimNameAttributeTypeOnly = 6148,
		// Token: 0x040000A6 RID: 166
		AllNamedTypesOnArityZero = 8192,
		// Token: 0x040000A7 RID: 167
		MustNotBeMethodTypeParameter = 16384
	}
}
