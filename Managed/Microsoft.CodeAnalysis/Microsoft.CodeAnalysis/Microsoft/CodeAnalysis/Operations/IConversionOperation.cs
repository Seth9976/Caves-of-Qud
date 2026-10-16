using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200037C RID: 892
	[NullableContext(1)]
	public interface IConversionOperation : IOperation
	{
		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x0600252E RID: 9518
		IOperation Operand { get; }

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x0600252F RID: 9519
		[Nullable(2)]
		IMethodSymbol OperatorMethod
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06002530 RID: 9520
		CommonConversion Conversion { get; }

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06002531 RID: 9521
		bool IsTryCast { get; }

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06002532 RID: 9522
		bool IsChecked { get; }
	}
}
