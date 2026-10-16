using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002D9 RID: 729
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class InterpolatedStringExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06004117 RID: 16663 RVA: 0x0010EEA4 File Offset: 0x0010D0A4
		internal InterpolatedStringExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170010DE RID: 4318
		// (get) Token: 0x06004118 RID: 16664 RVA: 0x0010EEAF File Offset: 0x0010D0AF
		public SyntaxToken StringStartToken
		{
			get
			{
				return new SyntaxToken(this, ((InterpolatedStringExpressionSyntax)base.Green).stringStartToken, base.Position, 0);
			}
		}

		// Token: 0x170010DF RID: 4319
		// (get) Token: 0x06004119 RID: 16665 RVA: 0x0010EECE File Offset: 0x0010D0CE
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<InterpolatedStringContentSyntax> Contents
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<InterpolatedStringContentSyntax>(base.GetRed(ref this.contents, 1));
			}
		}

		// Token: 0x170010E0 RID: 4320
		// (get) Token: 0x0600411A RID: 16666 RVA: 0x0010EEE2 File Offset: 0x0010D0E2
		public SyntaxToken StringEndToken
		{
			get
			{
				return new SyntaxToken(this, ((InterpolatedStringExpressionSyntax)base.Green).stringEndToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x0600411B RID: 16667 RVA: 0x0010EF08 File Offset: 0x0010D108
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed(ref this.contents, 1);
		}

		// Token: 0x0600411C RID: 16668 RVA: 0x0010EF1D File Offset: 0x0010D11D
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.contents;
		}

		// Token: 0x0600411D RID: 16669 RVA: 0x0010EF2B File Offset: 0x0010D12B
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitInterpolatedStringExpression(this);
		}

		// Token: 0x0600411E RID: 16670 RVA: 0x0010EF34 File Offset: 0x0010D134
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitInterpolatedStringExpression(this);
		}

		// Token: 0x0600411F RID: 16671 RVA: 0x0010EF40 File Offset: 0x0010D140
		public InterpolatedStringExpressionSyntax Update(SyntaxToken stringStartToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<InterpolatedStringContentSyntax> contents, SyntaxToken stringEndToken)
		{
			if (!(stringStartToken != this.StringStartToken) && !(contents != this.Contents) && !(stringEndToken != this.StringEndToken))
			{
				return this;
			}
			InterpolatedStringExpressionSyntax interpolatedStringExpressionSyntax = SyntaxFactory.InterpolatedStringExpression(stringStartToken, contents, stringEndToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return interpolatedStringExpressionSyntax;
			}
			return interpolatedStringExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004120 RID: 16672 RVA: 0x0010EF99 File Offset: 0x0010D199
		public InterpolatedStringExpressionSyntax WithStringStartToken(SyntaxToken stringStartToken)
		{
			return this.Update(stringStartToken, this.Contents, this.StringEndToken);
		}

		// Token: 0x06004121 RID: 16673 RVA: 0x0010EFAE File Offset: 0x0010D1AE
		public InterpolatedStringExpressionSyntax WithContents([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<InterpolatedStringContentSyntax> contents)
		{
			return this.Update(this.StringStartToken, contents, this.StringEndToken);
		}

		// Token: 0x06004122 RID: 16674 RVA: 0x0010EFC3 File Offset: 0x0010D1C3
		public InterpolatedStringExpressionSyntax WithStringEndToken(SyntaxToken stringEndToken)
		{
			return this.Update(this.StringStartToken, this.Contents, stringEndToken);
		}

		// Token: 0x06004123 RID: 16675 RVA: 0x0010EFD8 File Offset: 0x0010D1D8
		public InterpolatedStringExpressionSyntax AddContents(params InterpolatedStringContentSyntax[] items)
		{
			return this.WithContents(this.Contents.AddRange(items));
		}

		// Token: 0x040014A4 RID: 5284
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode contents;
	}
}
