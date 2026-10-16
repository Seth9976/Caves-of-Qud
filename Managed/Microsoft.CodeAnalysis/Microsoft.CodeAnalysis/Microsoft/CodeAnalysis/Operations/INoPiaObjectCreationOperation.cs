using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003D6 RID: 982
	[NullableContext(2)]
	internal interface INoPiaObjectCreationOperation : IOperation
	{
		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x060025E6 RID: 9702
		IObjectOrCollectionInitializerOperation Initializer { get; }
	}
}
