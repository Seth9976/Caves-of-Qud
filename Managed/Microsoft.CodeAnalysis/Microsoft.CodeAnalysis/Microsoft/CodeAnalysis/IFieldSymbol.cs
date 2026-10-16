using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000240 RID: 576
	[NullableContext(2)]
	public interface IFieldSymbol : ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x0600162D RID: 5677
		ISymbol AssociatedSymbol { get; }

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x0600162E RID: 5678
		bool IsConst { get; }

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x0600162F RID: 5679
		bool IsReadOnly { get; }

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001630 RID: 5680
		bool IsVolatile { get; }

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001631 RID: 5681
		bool IsFixedSizeBuffer { get; }

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001632 RID: 5682
		[Nullable(1)]
		ITypeSymbol Type
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001633 RID: 5683
		NullableAnnotation NullableAnnotation { get; }

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001634 RID: 5684
		[MemberNotNullWhen(true, "ConstantValue")]
		bool HasConstantValue
		{
			[MemberNotNullWhen(true, "ConstantValue")]
			get;
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001635 RID: 5685
		object ConstantValue { get; }

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001636 RID: 5686
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<CustomModifier> CustomModifiers
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001637 RID: 5687
		[Nullable(1)]
		IFieldSymbol OriginalDefinition
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001638 RID: 5688
		IFieldSymbol CorrespondingTupleField { get; }

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001639 RID: 5689
		bool IsExplicitlyNamedTupleElement { get; }
	}
}
