using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200000A RID: 10
	internal interface IBoundNodeWithIOperationChildren
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x060000C0 RID: 192
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		ImmutableArray<BoundNode> Children
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			get;
		}
	}
}
