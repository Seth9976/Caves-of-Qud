using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200038F RID: 911
	[NullableContext(1)]
	public interface IIsTypeOperation : IOperation
	{
		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06002561 RID: 9569
		IOperation ValueOperand { get; }

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06002562 RID: 9570
		ITypeSymbol TypeOperand { get; }

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06002563 RID: 9571
		bool IsNegated { get; }
	}
}
