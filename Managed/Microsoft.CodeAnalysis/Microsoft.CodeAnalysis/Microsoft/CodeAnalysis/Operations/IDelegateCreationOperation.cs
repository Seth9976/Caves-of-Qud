using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003A4 RID: 932
	[NullableContext(1)]
	public interface IDelegateCreationOperation : IOperation
	{
		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x0600258A RID: 9610
		IOperation Target { get; }
	}
}
