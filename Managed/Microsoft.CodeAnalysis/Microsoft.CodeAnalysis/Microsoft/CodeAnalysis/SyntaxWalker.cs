using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000289 RID: 649
	public abstract class SyntaxWalker
	{
		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06001ADE RID: 6878 RVA: 0x0004F88C File Offset: 0x0004DA8C
		protected SyntaxWalkerDepth Depth { get; }

		// Token: 0x06001ADF RID: 6879 RVA: 0x0004F894 File Offset: 0x0004DA94
		protected SyntaxWalker(SyntaxWalkerDepth depth = SyntaxWalkerDepth.Node)
		{
			this.Depth = depth;
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x0004F8A4 File Offset: 0x0004DAA4
		[NullableContext(1)]
		public virtual void Visit(SyntaxNode node)
		{
			foreach (SyntaxNodeOrToken syntaxNodeOrToken in node.ChildNodesAndTokens())
			{
				if (syntaxNodeOrToken.IsNode)
				{
					if (this.Depth >= SyntaxWalkerDepth.Node)
					{
						this.Visit(syntaxNodeOrToken.AsNode());
					}
				}
				else if (syntaxNodeOrToken.IsToken && this.Depth >= SyntaxWalkerDepth.Token)
				{
					this.VisitToken(syntaxNodeOrToken.AsToken());
				}
			}
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x0004F913 File Offset: 0x0004DB13
		protected virtual void VisitToken(SyntaxToken token)
		{
			if (this.Depth >= SyntaxWalkerDepth.Trivia)
			{
				this.VisitLeadingTrivia(in token);
				this.VisitTrailingTrivia(in token);
			}
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x0004F930 File Offset: 0x0004DB30
		private void VisitLeadingTrivia(in SyntaxToken token)
		{
			if (token.HasLeadingTrivia)
			{
				foreach (SyntaxTrivia syntaxTrivia in token.LeadingTrivia)
				{
					this.VisitTrivia(syntaxTrivia);
				}
			}
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x0004F970 File Offset: 0x0004DB70
		private void VisitTrailingTrivia(in SyntaxToken token)
		{
			if (token.HasTrailingTrivia)
			{
				foreach (SyntaxTrivia syntaxTrivia in token.TrailingTrivia)
				{
					this.VisitTrivia(syntaxTrivia);
				}
			}
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x0004F9AE File Offset: 0x0004DBAE
		protected virtual void VisitTrivia(SyntaxTrivia trivia)
		{
			if (this.Depth >= SyntaxWalkerDepth.StructuredTrivia && trivia.HasStructure)
			{
				this.Visit(trivia.GetStructure());
			}
		}
	}
}
