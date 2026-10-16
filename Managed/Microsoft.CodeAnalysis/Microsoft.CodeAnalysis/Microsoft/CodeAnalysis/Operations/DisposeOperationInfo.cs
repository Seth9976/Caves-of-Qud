using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000461 RID: 1121
	[NullableContext(2)]
	[Nullable(0)]
	internal struct DisposeOperationInfo
	{
		// Token: 0x06002C23 RID: 11299 RVA: 0x0007CD3C File Offset: 0x0007AF3C
		public DisposeOperationInfo(IMethodSymbol disposeMethod, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IArgumentOperation> disposeArguments)
		{
			this.DisposeMethod = disposeMethod;
			this.DisposeArguments = disposeArguments;
		}

		// Token: 0x0400116F RID: 4463
		public readonly IMethodSymbol DisposeMethod;

		// Token: 0x04001170 RID: 4464
		[Nullable(new byte[] { 0, 1 })]
		public readonly ImmutableArray<IArgumentOperation> DisposeArguments;
	}
}
