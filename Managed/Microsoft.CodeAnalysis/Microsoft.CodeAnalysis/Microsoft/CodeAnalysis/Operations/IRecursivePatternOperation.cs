using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003CF RID: 975
	[NullableContext(2)]
	public interface IRecursivePatternOperation : IPatternOperation, IOperation
	{
		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x060025D4 RID: 9684
		[Nullable(1)]
		ITypeSymbol MatchedType
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x060025D5 RID: 9685
		ISymbol DeconstructSymbol { get; }

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x060025D6 RID: 9686
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IPatternOperation> DeconstructionSubpatterns
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x060025D7 RID: 9687
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IPropertySubpatternOperation> PropertySubpatterns
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x060025D8 RID: 9688
		ISymbol DeclaredSymbol { get; }
	}
}
