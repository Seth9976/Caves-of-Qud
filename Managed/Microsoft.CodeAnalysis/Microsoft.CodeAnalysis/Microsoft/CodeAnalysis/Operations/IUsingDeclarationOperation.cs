using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003DA RID: 986
	[NullableContext(1)]
	public interface IUsingDeclarationOperation : IOperation
	{
		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x060025EB RID: 9707
		IVariableDeclarationGroupOperation DeclarationGroup { get; }

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x060025EC RID: 9708
		bool IsAsynchronous { get; }
	}
}
