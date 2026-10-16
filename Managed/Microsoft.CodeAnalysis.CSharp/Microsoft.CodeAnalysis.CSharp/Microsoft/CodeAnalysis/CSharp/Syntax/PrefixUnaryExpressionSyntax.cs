using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002A0 RID: 672
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class PrefixUnaryExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06003E73 RID: 15987 RVA: 0x0010A4F6 File Offset: 0x001086F6
		internal PrefixUnaryExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700103E RID: 4158
		// (get) Token: 0x06003E74 RID: 15988 RVA: 0x0010A501 File Offset: 0x00108701
		public SyntaxToken OperatorToken
		{
			get
			{
				return new SyntaxToken(this, ((PrefixUnaryExpressionSyntax)base.Green).operatorToken, base.Position, 0);
			}
		}

		// Token: 0x1700103F RID: 4159
		// (get) Token: 0x06003E75 RID: 15989 RVA: 0x0010A520 File Offset: 0x00108720
		public ExpressionSyntax Operand
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.operand, 1);
			}
		}

		// Token: 0x06003E76 RID: 15990 RVA: 0x0010A52F File Offset: 0x0010872F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed<ExpressionSyntax>(ref this.operand, 1);
		}

		// Token: 0x06003E77 RID: 15991 RVA: 0x0010A544 File Offset: 0x00108744
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.operand;
		}

		// Token: 0x06003E78 RID: 15992 RVA: 0x0010A552 File Offset: 0x00108752
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitPrefixUnaryExpression(this);
		}

		// Token: 0x06003E79 RID: 15993 RVA: 0x0010A55B File Offset: 0x0010875B
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitPrefixUnaryExpression(this);
		}

		// Token: 0x06003E7A RID: 15994 RVA: 0x0010A564 File Offset: 0x00108764
		public PrefixUnaryExpressionSyntax Update(SyntaxToken operatorToken, ExpressionSyntax operand)
		{
			if (!(operatorToken != this.OperatorToken) && operand == this.Operand)
			{
				return this;
			}
			PrefixUnaryExpressionSyntax prefixUnaryExpressionSyntax = SyntaxFactory.PrefixUnaryExpression(base.Kind(), operatorToken, operand);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return prefixUnaryExpressionSyntax;
			}
			return prefixUnaryExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003E7B RID: 15995 RVA: 0x0010A5AF File Offset: 0x001087AF
		public PrefixUnaryExpressionSyntax WithOperatorToken(SyntaxToken operatorToken)
		{
			return this.Update(operatorToken, this.Operand);
		}

		// Token: 0x06003E7C RID: 15996 RVA: 0x0010A5BE File Offset: 0x001087BE
		public PrefixUnaryExpressionSyntax WithOperand(ExpressionSyntax operand)
		{
			return this.Update(this.OperatorToken, operand);
		}

		// Token: 0x0400145A RID: 5210
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax operand;
	}
}
