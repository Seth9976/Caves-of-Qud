using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003B8 RID: 952
	[NullableContext(1)]
	public interface ICatchClauseOperation : IOperation
	{
		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x060025A8 RID: 9640
		[Nullable(2)]
		IOperation ExceptionDeclarationOrExpression
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x060025A9 RID: 9641
		ITypeSymbol ExceptionType { get; }

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x060025AA RID: 9642
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ILocalSymbol> Locals
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x060025AB RID: 9643
		[Nullable(2)]
		IOperation Filter
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x060025AC RID: 9644
		IBlockOperation Handler { get; }
	}
}
