using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000382 RID: 898
	public interface IFieldReferenceOperation : IMemberReferenceOperation, IOperation
	{
		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x0600253E RID: 9534
		[Nullable(1)]
		IFieldSymbol Field
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x0600253F RID: 9535
		bool IsDeclaration { get; }
	}
}
