using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003B7 RID: 951
	[NullableContext(1)]
	public interface IArgumentOperation : IOperation
	{
		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x060025A3 RID: 9635
		ArgumentKind ArgumentKind { get; }

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x060025A4 RID: 9636
		[Nullable(2)]
		IParameterSymbol Parameter
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x060025A5 RID: 9637
		IOperation Value { get; }

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x060025A6 RID: 9638
		CommonConversion InConversion { get; }

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x060025A7 RID: 9639
		CommonConversion OutConversion { get; }
	}
}
