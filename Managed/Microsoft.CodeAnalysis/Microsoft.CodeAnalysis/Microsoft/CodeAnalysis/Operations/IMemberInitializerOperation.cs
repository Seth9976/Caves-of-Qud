using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200039B RID: 923
	[NullableContext(1)]
	public interface IMemberInitializerOperation : IOperation
	{
		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06002577 RID: 9591
		IOperation InitializedMember { get; }

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06002578 RID: 9592
		IObjectOrCollectionInitializerOperation Initializer { get; }
	}
}
