using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.Cci
{
	// Token: 0x02000092 RID: 146
	[NullableContext(1)]
	[Nullable(0)]
	internal static class ITypeReferenceExtensions
	{
		// Token: 0x060003E5 RID: 997 RVA: 0x0000DC70 File Offset: 0x0000BE70
		internal static void GetConsolidatedTypeArguments(this ITypeReference typeReference, ArrayBuilder<ITypeReference> consolidatedTypeArguments, EmitContext context)
		{
			INestedTypeReference asNestedTypeReference = typeReference.AsNestedTypeReference;
			if (asNestedTypeReference != null)
			{
				asNestedTypeReference.GetContainingType(context).GetConsolidatedTypeArguments(consolidatedTypeArguments, context);
			}
			IGenericTypeInstanceReference asGenericTypeInstanceReference = typeReference.AsGenericTypeInstanceReference;
			if (asGenericTypeInstanceReference != null)
			{
				consolidatedTypeArguments.AddRange(asGenericTypeInstanceReference.GetGenericArguments(context));
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000DCB0 File Offset: 0x0000BEB0
		internal static ITypeReference GetUninstantiatedGenericType(this ITypeReference typeReference, EmitContext context)
		{
			IGenericTypeInstanceReference asGenericTypeInstanceReference = typeReference.AsGenericTypeInstanceReference;
			if (asGenericTypeInstanceReference != null)
			{
				return asGenericTypeInstanceReference.GetGenericType(context);
			}
			ISpecializedNestedTypeReference asSpecializedNestedTypeReference = typeReference.AsSpecializedNestedTypeReference;
			if (asSpecializedNestedTypeReference != null)
			{
				return asSpecializedNestedTypeReference.GetUnspecializedVersion(context);
			}
			return typeReference;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000DCE4 File Offset: 0x0000BEE4
		internal static bool IsTypeSpecification(this ITypeReference typeReference)
		{
			INestedTypeReference asNestedTypeReference = typeReference.AsNestedTypeReference;
			if (asNestedTypeReference != null)
			{
				return asNestedTypeReference.AsSpecializedNestedTypeReference != null || asNestedTypeReference.AsGenericTypeInstanceReference != null;
			}
			return typeReference.AsNamespaceTypeReference == null;
		}
	}
}
