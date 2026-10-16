using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Symbols
{
	// Token: 0x020002BC RID: 700
	[NullableContext(1)]
	internal interface IMethodSymbolInternal : ISymbolInternal
	{
		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06001D92 RID: 7570
		bool IsIterator { get; }

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06001D93 RID: 7571
		bool IsAsync { get; }

		// Token: 0x06001D94 RID: 7572
		int CalculateLocalSyntaxOffset(int declaratorPosition, SyntaxTree declaratorTree);

		// Token: 0x06001D95 RID: 7573
		IMethodSymbolInternal Construct(params ITypeSymbolInternal[] typeArguments);
	}
}
