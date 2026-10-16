using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000249 RID: 585
	[NullableContext(1)]
	public interface IParameterSymbol : ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x0600169F RID: 5791
		RefKind RefKind { get; }

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060016A0 RID: 5792
		bool IsParams { get; }

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060016A1 RID: 5793
		bool IsOptional { get; }

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x060016A2 RID: 5794
		bool IsThis { get; }

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060016A3 RID: 5795
		bool IsDiscard { get; }

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060016A4 RID: 5796
		ITypeSymbol Type { get; }

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060016A5 RID: 5797
		NullableAnnotation NullableAnnotation { get; }

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060016A6 RID: 5798
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<CustomModifier> CustomModifiers
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x060016A7 RID: 5799
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<CustomModifier> RefCustomModifiers
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x060016A8 RID: 5800
		int Ordinal { get; }

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x060016A9 RID: 5801
		bool HasExplicitDefaultValue { get; }

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x060016AA RID: 5802
		[Nullable(2)]
		object ExplicitDefaultValue
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x060016AB RID: 5803
		IParameterSymbol OriginalDefinition { get; }
	}
}
