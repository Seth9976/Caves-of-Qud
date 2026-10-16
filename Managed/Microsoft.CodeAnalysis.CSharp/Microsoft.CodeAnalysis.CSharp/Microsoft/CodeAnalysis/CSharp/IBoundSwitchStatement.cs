using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000EE RID: 238
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	internal interface IBoundSwitchStatement
	{
		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06001210 RID: 4624
		BoundNode Value { get; }

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06001211 RID: 4625
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		ImmutableArray<BoundStatementList> Cases
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
