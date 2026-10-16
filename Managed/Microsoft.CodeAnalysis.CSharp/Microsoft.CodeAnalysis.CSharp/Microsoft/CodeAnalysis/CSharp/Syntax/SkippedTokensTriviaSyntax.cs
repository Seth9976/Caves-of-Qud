using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200027D RID: 637
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class SkippedTokensTriviaSyntax : StructuredTriviaSyntax, ISkippedTokensTriviaSyntax
	{
		// Token: 0x06003CA1 RID: 15521 RVA: 0x00105B44 File Offset: 0x00103D44
		internal SkippedTokensTriviaSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000FDF RID: 4063
		// (get) Token: 0x06003CA2 RID: 15522 RVA: 0x00105B50 File Offset: 0x00103D50
		public SyntaxTokenList Tokens
		{
			get
			{
				GreenNode slot = base.Green.GetSlot(0);
				if (slot == null)
				{
					return default(SyntaxTokenList);
				}
				return new SyntaxTokenList(this, slot, base.Position, 0);
			}
		}

		// Token: 0x06003CA3 RID: 15523 RVA: 0x00105B85 File Offset: 0x00103D85
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x06003CA4 RID: 15524 RVA: 0x00105B88 File Offset: 0x00103D88
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x06003CA5 RID: 15525 RVA: 0x00105B8B File Offset: 0x00103D8B
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitSkippedTokensTrivia(this);
		}

		// Token: 0x06003CA6 RID: 15526 RVA: 0x00105B94 File Offset: 0x00103D94
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitSkippedTokensTrivia(this);
		}

		// Token: 0x06003CA7 RID: 15527 RVA: 0x00105BA0 File Offset: 0x00103DA0
		public SkippedTokensTriviaSyntax Update(SyntaxTokenList tokens)
		{
			if (!(tokens != this.Tokens))
			{
				return this;
			}
			SkippedTokensTriviaSyntax skippedTokensTriviaSyntax = SyntaxFactory.SkippedTokensTrivia(tokens);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return skippedTokensTriviaSyntax;
			}
			return skippedTokensTriviaSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003CA8 RID: 15528 RVA: 0x00105BDB File Offset: 0x00103DDB
		public SkippedTokensTriviaSyntax WithTokens(SyntaxTokenList tokens)
		{
			return this.Update(tokens);
		}

		// Token: 0x06003CA9 RID: 15529 RVA: 0x00105BE4 File Offset: 0x00103DE4
		public SkippedTokensTriviaSyntax AddTokens(params SyntaxToken[] items)
		{
			return this.WithTokens(this.Tokens.AddRange(items));
		}
	}
}
