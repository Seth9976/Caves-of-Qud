using System;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000371 RID: 881
	internal abstract class StructuredTriviaSyntax : CSharpSyntaxNode
	{
		// Token: 0x06004F33 RID: 20275 RVA: 0x00141423 File Offset: 0x0013F623
		internal StructuredTriviaSyntax(SyntaxKind kind, DiagnosticInfo[] diagnostics = null, SyntaxAnnotation[] annotations = null)
			: base(kind, diagnostics, annotations)
		{
			this.Initialize();
		}

		// Token: 0x06004F34 RID: 20276 RVA: 0x00141434 File Offset: 0x0013F634
		internal StructuredTriviaSyntax(ObjectReader reader)
			: base(reader)
		{
			this.Initialize();
		}

		// Token: 0x06004F35 RID: 20277 RVA: 0x00141443 File Offset: 0x0013F643
		private void Initialize()
		{
			this.flags |= GreenNode.NodeFlags.ContainsStructuredTrivia;
			if (base.Kind == SyntaxKind.SkippedTokensTrivia)
			{
				this.flags |= GreenNode.NodeFlags.ContainsSkippedText;
			}
		}
	}
}
