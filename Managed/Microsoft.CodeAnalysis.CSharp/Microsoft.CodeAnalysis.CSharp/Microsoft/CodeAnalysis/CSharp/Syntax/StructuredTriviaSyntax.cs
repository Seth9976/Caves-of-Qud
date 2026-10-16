using System;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200027F RID: 639
	public abstract class StructuredTriviaSyntax : CSharpSyntaxNode, IStructuredTriviaSyntax
	{
		// Token: 0x06003CB7 RID: 15543 RVA: 0x00105D6E File Offset: 0x00103F6E
		internal StructuredTriviaSyntax(CSharpSyntaxNode green, SyntaxNode parent, int position)
			: base(green, position, (parent == null) ? null : parent.SyntaxTree)
		{
		}

		// Token: 0x06003CB8 RID: 15544 RVA: 0x00105D84 File Offset: 0x00103F84
		internal static StructuredTriviaSyntax Create(SyntaxTrivia trivia)
		{
			GreenNode underlyingNode = trivia.UnderlyingNode;
			SyntaxNode parent = trivia.Token.Parent;
			int position = trivia.Position;
			StructuredTriviaSyntax structuredTriviaSyntax = (StructuredTriviaSyntax)underlyingNode.CreateRed(parent, position);
			structuredTriviaSyntax._parent = trivia;
			return structuredTriviaSyntax;
		}

		// Token: 0x17000FE3 RID: 4067
		// (get) Token: 0x06003CB9 RID: 15545 RVA: 0x00105DC3 File Offset: 0x00103FC3
		public override SyntaxTrivia ParentTrivia
		{
			get
			{
				return this._parent;
			}
		}

		// Token: 0x04001424 RID: 5156
		private SyntaxTrivia _parent;
	}
}
