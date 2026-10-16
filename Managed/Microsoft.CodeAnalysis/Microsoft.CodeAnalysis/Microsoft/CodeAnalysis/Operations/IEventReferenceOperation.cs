using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000385 RID: 901
	public interface IEventReferenceOperation : IMemberReferenceOperation, IOperation
	{
		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06002544 RID: 9540
		[Nullable(1)]
		IEventSymbol Event
		{
			[NullableContext(1)]
			get;
		}
	}
}
