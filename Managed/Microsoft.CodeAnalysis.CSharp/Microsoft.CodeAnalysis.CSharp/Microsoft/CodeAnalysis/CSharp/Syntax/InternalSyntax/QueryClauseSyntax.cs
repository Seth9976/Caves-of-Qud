using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003C1 RID: 961
	internal abstract class QueryClauseSyntax : CSharpSyntaxNode
	{
		// Token: 0x060054C2 RID: 21698 RVA: 0x00150CF1 File Offset: 0x0014EEF1
		internal QueryClauseSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
		}

		// Token: 0x060054C3 RID: 21699 RVA: 0x00150CFC File Offset: 0x0014EEFC
		internal QueryClauseSyntax(SyntaxKind kind)
			: base(kind)
		{
		}

		// Token: 0x060054C4 RID: 21700 RVA: 0x00150D05 File Offset: 0x0014EF05
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected QueryClauseSyntax(ObjectReader reader)
			: base(reader)
		{
		}
	}
}
