using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002C0 RID: 704
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class InitializerExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06003FDE RID: 16350 RVA: 0x0010CA63 File Offset: 0x0010AC63
		internal InitializerExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001092 RID: 4242
		// (get) Token: 0x06003FDF RID: 16351 RVA: 0x0010CA6E File Offset: 0x0010AC6E
		public SyntaxToken OpenBraceToken
		{
			get
			{
				return new SyntaxToken(this, ((InitializerExpressionSyntax)base.Green).openBraceToken, base.Position, 0);
			}
		}

		// Token: 0x17001093 RID: 4243
		// (get) Token: 0x06003FE0 RID: 16352 RVA: 0x0010CA90 File Offset: 0x0010AC90
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<ExpressionSyntax> Expressions
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				SyntaxNode red = base.GetRed(ref this.expressions, 1);
				if (red == null)
				{
					return default(SeparatedSyntaxList<ExpressionSyntax>);
				}
				return new SeparatedSyntaxList<ExpressionSyntax>(red, base.GetChildIndex(1));
			}
		}

		// Token: 0x17001094 RID: 4244
		// (get) Token: 0x06003FE1 RID: 16353 RVA: 0x0010CAC5 File Offset: 0x0010ACC5
		public SyntaxToken CloseBraceToken
		{
			get
			{
				return new SyntaxToken(this, ((InitializerExpressionSyntax)base.Green).closeBraceToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x06003FE2 RID: 16354 RVA: 0x0010CAEB File Offset: 0x0010ACEB
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed(ref this.expressions, 1);
		}

		// Token: 0x06003FE3 RID: 16355 RVA: 0x0010CB00 File Offset: 0x0010AD00
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.expressions;
		}

		// Token: 0x06003FE4 RID: 16356 RVA: 0x0010CB0E File Offset: 0x0010AD0E
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitInitializerExpression(this);
		}

		// Token: 0x06003FE5 RID: 16357 RVA: 0x0010CB17 File Offset: 0x0010AD17
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitInitializerExpression(this);
		}

		// Token: 0x06003FE6 RID: 16358 RVA: 0x0010CB20 File Offset: 0x0010AD20
		public InitializerExpressionSyntax Update(SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ExpressionSyntax> expressions, SyntaxToken closeBraceToken)
		{
			if (!(openBraceToken != this.OpenBraceToken) && !(expressions != this.Expressions) && !(closeBraceToken != this.CloseBraceToken))
			{
				return this;
			}
			InitializerExpressionSyntax initializerExpressionSyntax = SyntaxFactory.InitializerExpression(base.Kind(), openBraceToken, expressions, closeBraceToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return initializerExpressionSyntax;
			}
			return initializerExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003FE7 RID: 16359 RVA: 0x0010CB7F File Offset: 0x0010AD7F
		public InitializerExpressionSyntax WithOpenBraceToken(SyntaxToken openBraceToken)
		{
			return this.Update(openBraceToken, this.Expressions, this.CloseBraceToken);
		}

		// Token: 0x06003FE8 RID: 16360 RVA: 0x0010CB94 File Offset: 0x0010AD94
		public InitializerExpressionSyntax WithExpressions([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ExpressionSyntax> expressions)
		{
			return this.Update(this.OpenBraceToken, expressions, this.CloseBraceToken);
		}

		// Token: 0x06003FE9 RID: 16361 RVA: 0x0010CBA9 File Offset: 0x0010ADA9
		public InitializerExpressionSyntax WithCloseBraceToken(SyntaxToken closeBraceToken)
		{
			return this.Update(this.OpenBraceToken, this.Expressions, closeBraceToken);
		}

		// Token: 0x06003FEA RID: 16362 RVA: 0x0010CBC0 File Offset: 0x0010ADC0
		public InitializerExpressionSyntax AddExpressions(params ExpressionSyntax[] items)
		{
			return this.WithExpressions(this.Expressions.AddRange(items));
		}

		// Token: 0x04001481 RID: 5249
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode expressions;
	}
}
