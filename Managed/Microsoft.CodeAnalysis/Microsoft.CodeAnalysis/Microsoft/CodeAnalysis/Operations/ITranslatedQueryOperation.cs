using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003A3 RID: 931
	[NullableContext(1)]
	public interface ITranslatedQueryOperation : IOperation
	{
		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06002589 RID: 9609
		IOperation Operation { get; }
	}
}
