using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.Cci
{
	// Token: 0x020000CC RID: 204
	internal interface IGenericParameter : IGenericParameterReference, ITypeReference, IReference, INamedEntity, IParameterListEntry
	{
		// Token: 0x0600070E RID: 1806
		[NullableContext(1)]
		IEnumerable<TypeReferenceWithAttributes> GetConstraints(EmitContext context);

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600070F RID: 1807
		bool MustBeReferenceType { get; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000710 RID: 1808
		bool MustBeValueType { get; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000711 RID: 1809
		bool MustHaveDefaultConstructor { get; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000712 RID: 1810
		TypeParameterVariance Variance { get; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000713 RID: 1811
		[Nullable(2)]
		IGenericMethodParameter AsGenericMethodParameter
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000714 RID: 1812
		[Nullable(2)]
		IGenericTypeParameter AsGenericTypeParameter
		{
			[NullableContext(2)]
			get;
		}
	}
}
