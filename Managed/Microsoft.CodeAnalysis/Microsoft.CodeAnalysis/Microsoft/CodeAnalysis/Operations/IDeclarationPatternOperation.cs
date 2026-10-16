using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003C5 RID: 965
	[NullableContext(2)]
	public interface IDeclarationPatternOperation : IPatternOperation, IOperation
	{
		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x060025C1 RID: 9665
		ITypeSymbol MatchedType { get; }

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x060025C2 RID: 9666
		bool MatchesNull { get; }

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x060025C3 RID: 9667
		ISymbol DeclaredSymbol { get; }
	}
}
