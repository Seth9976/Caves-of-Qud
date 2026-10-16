using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002A1 RID: 673
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class AwaitExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06003E7D RID: 15997 RVA: 0x0010A5CD File Offset: 0x001087CD
		internal AwaitExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001040 RID: 4160
		// (get) Token: 0x06003E7E RID: 15998 RVA: 0x0010A5D8 File Offset: 0x001087D8
		public SyntaxToken AwaitKeyword
		{
			get
			{
				return new SyntaxToken(this, ((AwaitExpressionSyntax)base.Green).awaitKeyword, base.Position, 0);
			}
		}

		// Token: 0x17001041 RID: 4161
		// (get) Token: 0x06003E7F RID: 15999 RVA: 0x0010A5F7 File Offset: 0x001087F7
		public ExpressionSyntax Expression
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.expression, 1);
			}
		}

		// Token: 0x06003E80 RID: 16000 RVA: 0x0010A606 File Offset: 0x00108806
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed<ExpressionSyntax>(ref this.expression, 1);
		}

		// Token: 0x06003E81 RID: 16001 RVA: 0x0010A61B File Offset: 0x0010881B
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.expression;
		}

		// Token: 0x06003E82 RID: 16002 RVA: 0x0010A629 File Offset: 0x00108829
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitAwaitExpression(this);
		}

		// Token: 0x06003E83 RID: 16003 RVA: 0x0010A632 File Offset: 0x00108832
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitAwaitExpression(this);
		}

		// Token: 0x06003E84 RID: 16004 RVA: 0x0010A63C File Offset: 0x0010883C
		public AwaitExpressionSyntax Update(SyntaxToken awaitKeyword, ExpressionSyntax expression)
		{
			if (!(awaitKeyword != this.AwaitKeyword) && expression == this.Expression)
			{
				return this;
			}
			AwaitExpressionSyntax awaitExpressionSyntax = SyntaxFactory.AwaitExpression(awaitKeyword, expression);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return awaitExpressionSyntax;
			}
			return awaitExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003E85 RID: 16005 RVA: 0x0010A681 File Offset: 0x00108881
		public AwaitExpressionSyntax WithAwaitKeyword(SyntaxToken awaitKeyword)
		{
			return this.Update(awaitKeyword, this.Expression);
		}

		// Token: 0x06003E86 RID: 16006 RVA: 0x0010A690 File Offset: 0x00108890
		public AwaitExpressionSyntax WithExpression(ExpressionSyntax expression)
		{
			return this.Update(this.AwaitKeyword, expression);
		}

		// Token: 0x0400145B RID: 5211
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;
	}
}
