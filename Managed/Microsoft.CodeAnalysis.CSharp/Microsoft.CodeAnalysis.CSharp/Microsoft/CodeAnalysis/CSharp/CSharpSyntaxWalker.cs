using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200022F RID: 559
	public abstract class CSharpSyntaxWalker : CSharpSyntaxVisitor
	{
		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06002EB6 RID: 11958 RVA: 0x000F02E3 File Offset: 0x000EE4E3
		protected SyntaxWalkerDepth Depth { get; }

		// Token: 0x06002EB7 RID: 11959 RVA: 0x000F02EB File Offset: 0x000EE4EB
		protected CSharpSyntaxWalker(SyntaxWalkerDepth depth = SyntaxWalkerDepth.Node)
		{
			this.Depth = depth;
		}

		// Token: 0x06002EB8 RID: 11960 RVA: 0x000F02FA File Offset: 0x000EE4FA
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override void Visit(SyntaxNode node)
		{
			if (node != null)
			{
				this._recursionDepth++;
				StackGuard.EnsureSufficientExecutionStack(this._recursionDepth);
				((CSharpSyntaxNode)node).Accept(this);
				this._recursionDepth--;
			}
		}

		// Token: 0x06002EB9 RID: 11961 RVA: 0x000F0334 File Offset: 0x000EE534
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public override void DefaultVisit(SyntaxNode node)
		{
			int count = node.ChildNodesAndTokens().Count;
			int num = 0;
			do
			{
				SyntaxNodeOrToken syntaxNodeOrToken = ChildSyntaxList.ItemInternal((CSharpSyntaxNode)node, num);
				num++;
				SyntaxNode syntaxNode = syntaxNodeOrToken.AsNode();
				if (syntaxNode != null)
				{
					if (this.Depth >= SyntaxWalkerDepth.Node)
					{
						this.Visit(syntaxNode);
					}
				}
				else if (this.Depth >= SyntaxWalkerDepth.Token)
				{
					this.VisitToken(syntaxNodeOrToken.AsToken());
				}
			}
			while (num < count);
		}

		// Token: 0x06002EBA RID: 11962 RVA: 0x000F039D File Offset: 0x000EE59D
		public virtual void VisitToken(SyntaxToken token)
		{
			if (this.Depth >= SyntaxWalkerDepth.Trivia)
			{
				this.VisitLeadingTrivia(token);
				this.VisitTrailingTrivia(token);
			}
		}

		// Token: 0x06002EBB RID: 11963 RVA: 0x000F03B8 File Offset: 0x000EE5B8
		public virtual void VisitLeadingTrivia(SyntaxToken token)
		{
			if (token.HasLeadingTrivia)
			{
				foreach (SyntaxTrivia syntaxTrivia in token.LeadingTrivia)
				{
					this.VisitTrivia(syntaxTrivia);
				}
			}
		}

		// Token: 0x06002EBC RID: 11964 RVA: 0x000F03F8 File Offset: 0x000EE5F8
		public virtual void VisitTrailingTrivia(SyntaxToken token)
		{
			if (token.HasTrailingTrivia)
			{
				foreach (SyntaxTrivia syntaxTrivia in token.TrailingTrivia)
				{
					this.VisitTrivia(syntaxTrivia);
				}
			}
		}

		// Token: 0x06002EBD RID: 11965 RVA: 0x000F0438 File Offset: 0x000EE638
		public virtual void VisitTrivia(SyntaxTrivia trivia)
		{
			if (this.Depth >= SyntaxWalkerDepth.StructuredTrivia && trivia.HasStructure)
			{
				this.Visit((CSharpSyntaxNode)trivia.GetStructure());
			}
		}

		// Token: 0x0400115A RID: 4442
		private int _recursionDepth;
	}
}
