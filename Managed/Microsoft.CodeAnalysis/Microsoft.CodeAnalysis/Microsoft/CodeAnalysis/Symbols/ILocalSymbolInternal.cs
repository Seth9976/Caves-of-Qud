using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Symbols
{
	// Token: 0x020002BB RID: 699
	internal interface ILocalSymbolInternal : ISymbolInternal
	{
		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06001D8F RID: 7567
		bool IsImportedFromMetadata { get; }

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06001D90 RID: 7568
		SynthesizedLocalKind SynthesizedKind { get; }

		// Token: 0x06001D91 RID: 7569
		[NullableContext(1)]
		SyntaxNode GetDeclaratorSyntax();
	}
}
