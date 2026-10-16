using System;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000374 RID: 884
	internal class SyntaxLastTokenReplacer : CSharpSyntaxRewriter
	{
		// Token: 0x06005050 RID: 20560 RVA: 0x001457EC File Offset: 0x001439EC
		private SyntaxLastTokenReplacer(SyntaxToken oldToken, SyntaxToken newToken)
			: base(false)
		{
			this._oldToken = oldToken;
			this._newToken = newToken;
		}

		// Token: 0x06005051 RID: 20561 RVA: 0x0014580A File Offset: 0x00143A0A
		internal static TRoot Replace<TRoot>(TRoot root, SyntaxToken newToken) where TRoot : CSharpSyntaxNode
		{
			return (TRoot)((object)new SyntaxLastTokenReplacer(root.GetLastToken(), newToken).Visit(root));
		}

		// Token: 0x06005052 RID: 20562 RVA: 0x00145830 File Offset: 0x00143A30
		private static int CountNonNullSlots(CSharpSyntaxNode node)
		{
			return node.ChildNodesAndTokens().Count;
		}

		// Token: 0x06005053 RID: 20563 RVA: 0x0014584C File Offset: 0x00143A4C
		public override CSharpSyntaxNode Visit(CSharpSyntaxNode node)
		{
			if (node != null && !this._found)
			{
				this._count--;
				if (this._count == 0)
				{
					if (node is SyntaxToken)
					{
						this._found = true;
						return this._newToken;
					}
					this._count += SyntaxLastTokenReplacer.CountNonNullSlots(node);
					return base.Visit(node);
				}
			}
			return node;
		}

		// Token: 0x040015D0 RID: 5584
		private readonly SyntaxToken _oldToken;

		// Token: 0x040015D1 RID: 5585
		private readonly SyntaxToken _newToken;

		// Token: 0x040015D2 RID: 5586
		private int _count = 1;

		// Token: 0x040015D3 RID: 5587
		private bool _found;
	}
}
