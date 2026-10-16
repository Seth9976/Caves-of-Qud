using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000244 RID: 580
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ArrayRankSpecifierSyntax : CSharpSyntaxNode
	{
		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x060037B8 RID: 14264 RVA: 0x000F9C6C File Offset: 0x000F7E6C
		public int Rank
		{
			get
			{
				return this.Sizes.Count;
			}
		}

		// Token: 0x060037B9 RID: 14265 RVA: 0x000F9C87 File Offset: 0x000F7E87
		internal ArrayRankSpecifierSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x060037BA RID: 14266 RVA: 0x000F9C92 File Offset: 0x000F7E92
		public SyntaxToken OpenBracketToken
		{
			get
			{
				return new SyntaxToken(this, ((ArrayRankSpecifierSyntax)base.Green).openBracketToken, base.Position, 0);
			}
		}

		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x060037BB RID: 14267 RVA: 0x000F9CB4 File Offset: 0x000F7EB4
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<ExpressionSyntax> Sizes
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				SyntaxNode red = base.GetRed(ref this.sizes, 1);
				if (red == null)
				{
					return default(SeparatedSyntaxList<ExpressionSyntax>);
				}
				return new SeparatedSyntaxList<ExpressionSyntax>(red, base.GetChildIndex(1));
			}
		}

		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x060037BC RID: 14268 RVA: 0x000F9CE9 File Offset: 0x000F7EE9
		public SyntaxToken CloseBracketToken
		{
			get
			{
				return new SyntaxToken(this, ((ArrayRankSpecifierSyntax)base.Green).closeBracketToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x060037BD RID: 14269 RVA: 0x000F9D0F File Offset: 0x000F7F0F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed(ref this.sizes, 1);
		}

		// Token: 0x060037BE RID: 14270 RVA: 0x000F9D24 File Offset: 0x000F7F24
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.sizes;
		}

		// Token: 0x060037BF RID: 14271 RVA: 0x000F9D32 File Offset: 0x000F7F32
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitArrayRankSpecifier(this);
		}

		// Token: 0x060037C0 RID: 14272 RVA: 0x000F9D3B File Offset: 0x000F7F3B
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitArrayRankSpecifier(this);
		}

		// Token: 0x060037C1 RID: 14273 RVA: 0x000F9D44 File Offset: 0x000F7F44
		public ArrayRankSpecifierSyntax Update(SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ExpressionSyntax> sizes, SyntaxToken closeBracketToken)
		{
			if (!(openBracketToken != this.OpenBracketToken) && !(sizes != this.Sizes) && !(closeBracketToken != this.CloseBracketToken))
			{
				return this;
			}
			ArrayRankSpecifierSyntax arrayRankSpecifierSyntax = SyntaxFactory.ArrayRankSpecifier(openBracketToken, sizes, closeBracketToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return arrayRankSpecifierSyntax;
			}
			return arrayRankSpecifierSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060037C2 RID: 14274 RVA: 0x000F9D9D File Offset: 0x000F7F9D
		public ArrayRankSpecifierSyntax WithOpenBracketToken(SyntaxToken openBracketToken)
		{
			return this.Update(openBracketToken, this.Sizes, this.CloseBracketToken);
		}

		// Token: 0x060037C3 RID: 14275 RVA: 0x000F9DB2 File Offset: 0x000F7FB2
		public ArrayRankSpecifierSyntax WithSizes([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ExpressionSyntax> sizes)
		{
			return this.Update(this.OpenBracketToken, sizes, this.CloseBracketToken);
		}

		// Token: 0x060037C4 RID: 14276 RVA: 0x000F9DC7 File Offset: 0x000F7FC7
		public ArrayRankSpecifierSyntax WithCloseBracketToken(SyntaxToken closeBracketToken)
		{
			return this.Update(this.OpenBracketToken, this.Sizes, closeBracketToken);
		}

		// Token: 0x060037C5 RID: 14277 RVA: 0x000F9DDC File Offset: 0x000F7FDC
		public ArrayRankSpecifierSyntax AddSizes(params ExpressionSyntax[] items)
		{
			return this.WithSizes(this.Sizes.AddRange(items));
		}

		// Token: 0x04001396 RID: 5014
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode sizes;
	}
}
