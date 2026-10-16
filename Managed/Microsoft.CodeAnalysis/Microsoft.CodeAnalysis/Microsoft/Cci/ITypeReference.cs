using System;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.Cci
{
	// Token: 0x020000E1 RID: 225
	[NullableContext(2)]
	internal interface ITypeReference : IReference
	{
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000747 RID: 1863
		bool IsEnum { get; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000748 RID: 1864
		bool IsValueType { get; }

		// Token: 0x06000749 RID: 1865
		ITypeDefinition GetResolvedType(EmitContext context);

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600074A RID: 1866
		PrimitiveTypeCode TypeCode { get; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600074B RID: 1867
		TypeDefinitionHandle TypeDef { get; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600074C RID: 1868
		IGenericMethodParameterReference AsGenericMethodParameterReference { get; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600074D RID: 1869
		IGenericTypeInstanceReference AsGenericTypeInstanceReference { get; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x0600074E RID: 1870
		IGenericTypeParameterReference AsGenericTypeParameterReference { get; }

		// Token: 0x0600074F RID: 1871
		INamespaceTypeDefinition AsNamespaceTypeDefinition(EmitContext context);

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000750 RID: 1872
		INamespaceTypeReference AsNamespaceTypeReference { get; }

		// Token: 0x06000751 RID: 1873
		INestedTypeDefinition AsNestedTypeDefinition(EmitContext context);

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000752 RID: 1874
		INestedTypeReference AsNestedTypeReference { get; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000753 RID: 1875
		ISpecializedNestedTypeReference AsSpecializedNestedTypeReference { get; }

		// Token: 0x06000754 RID: 1876
		ITypeDefinition AsTypeDefinition(EmitContext context);
	}
}
