using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001E7 RID: 487
	[NullableContext(1)]
	public interface ISyntaxReceiver
	{
		// Token: 0x06001413 RID: 5139
		void OnVisitSyntaxNode(SyntaxNode syntaxNode);
	}
}
