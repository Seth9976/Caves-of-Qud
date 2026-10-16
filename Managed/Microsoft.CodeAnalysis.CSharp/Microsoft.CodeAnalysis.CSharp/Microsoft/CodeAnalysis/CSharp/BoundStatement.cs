using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000186 RID: 390
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class BoundStatement : BoundNode
	{
		// Token: 0x06001E69 RID: 7785 RVA: 0x000A0BC7 File Offset: 0x0009EDC7
		protected BoundStatement(BoundKind kind, SyntaxNode syntax, bool hasErrors)
			: base(kind, syntax, hasErrors)
		{
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x000A0BD2 File Offset: 0x0009EDD2
		protected BoundStatement(BoundKind kind, SyntaxNode syntax)
			: base(kind, syntax)
		{
		}
	}
}
