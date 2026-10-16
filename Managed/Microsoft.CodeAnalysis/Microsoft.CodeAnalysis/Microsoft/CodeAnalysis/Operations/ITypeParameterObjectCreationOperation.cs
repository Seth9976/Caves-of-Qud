using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200038C RID: 908
	[NullableContext(2)]
	public interface ITypeParameterObjectCreationOperation : IOperation
	{
		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x0600255D RID: 9565
		IObjectOrCollectionInitializerOperation Initializer { get; }
	}
}
