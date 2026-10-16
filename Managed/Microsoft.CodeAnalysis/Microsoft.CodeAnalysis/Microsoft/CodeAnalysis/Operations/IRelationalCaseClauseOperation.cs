using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003BE RID: 958
	public interface IRelationalCaseClauseOperation : ICaseClauseOperation, IOperation
	{
		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x060025B7 RID: 9655
		[Nullable(1)]
		IOperation Value
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x060025B8 RID: 9656
		BinaryOperatorKind Relation { get; }
	}
}
