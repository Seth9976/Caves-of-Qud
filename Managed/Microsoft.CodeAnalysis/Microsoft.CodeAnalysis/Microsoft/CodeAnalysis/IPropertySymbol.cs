using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200024C RID: 588
	[NullableContext(2)]
	public interface IPropertySymbol : ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x060016AE RID: 5806
		bool IsIndexer { get; }

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x060016AF RID: 5807
		bool IsReadOnly { get; }

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x060016B0 RID: 5808
		bool IsWriteOnly { get; }

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x060016B1 RID: 5809
		bool IsWithEvents { get; }

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x060016B2 RID: 5810
		bool ReturnsByRef { get; }

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x060016B3 RID: 5811
		bool ReturnsByRefReadonly { get; }

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x060016B4 RID: 5812
		RefKind RefKind { get; }

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x060016B5 RID: 5813
		[Nullable(1)]
		ITypeSymbol Type
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x060016B6 RID: 5814
		NullableAnnotation NullableAnnotation { get; }

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x060016B7 RID: 5815
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IParameterSymbol> Parameters
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x060016B8 RID: 5816
		IMethodSymbol GetMethod { get; }

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x060016B9 RID: 5817
		IMethodSymbol SetMethod { get; }

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x060016BA RID: 5818
		[Nullable(1)]
		IPropertySymbol OriginalDefinition
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x060016BB RID: 5819
		IPropertySymbol OverriddenProperty { get; }

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x060016BC RID: 5820
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IPropertySymbol> ExplicitInterfaceImplementations
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x060016BD RID: 5821
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<CustomModifier> RefCustomModifiers
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x060016BE RID: 5822
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<CustomModifier> TypeCustomModifiers
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
