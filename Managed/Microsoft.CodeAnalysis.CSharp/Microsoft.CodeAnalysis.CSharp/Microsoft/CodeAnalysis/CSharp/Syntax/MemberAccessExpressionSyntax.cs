using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002A3 RID: 675
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class MemberAccessExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06003E91 RID: 16017 RVA: 0x0010A779 File Offset: 0x00108979
		internal MemberAccessExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001044 RID: 4164
		// (get) Token: 0x06003E92 RID: 16018 RVA: 0x0010A784 File Offset: 0x00108984
		public ExpressionSyntax Expression
		{
			get
			{
				return base.GetRedAtZero<ExpressionSyntax>(ref this.expression);
			}
		}

		// Token: 0x17001045 RID: 4165
		// (get) Token: 0x06003E93 RID: 16019 RVA: 0x0010A792 File Offset: 0x00108992
		public SyntaxToken OperatorToken
		{
			get
			{
				return new SyntaxToken(this, ((MemberAccessExpressionSyntax)base.Green).operatorToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001046 RID: 4166
		// (get) Token: 0x06003E94 RID: 16020 RVA: 0x0010A7B8 File Offset: 0x001089B8
		public SimpleNameSyntax Name
		{
			get
			{
				return base.GetRed<SimpleNameSyntax>(ref this.name, 2);
			}
		}

		// Token: 0x06003E95 RID: 16021 RVA: 0x0010A7C8 File Offset: 0x001089C8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			ExpressionSyntax expressionSyntax;
			if (index != 0)
			{
				if (index != 2)
				{
					expressionSyntax = null;
				}
				else
				{
					expressionSyntax = base.GetRed<SimpleNameSyntax>(ref this.name, 2);
				}
			}
			else
			{
				expressionSyntax = base.GetRedAtZero<ExpressionSyntax>(ref this.expression);
			}
			return expressionSyntax;
		}

		// Token: 0x06003E96 RID: 16022 RVA: 0x0010A800 File Offset: 0x00108A00
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			ExpressionSyntax expressionSyntax;
			if (index != 0)
			{
				if (index != 2)
				{
					expressionSyntax = null;
				}
				else
				{
					expressionSyntax = this.name;
				}
			}
			else
			{
				expressionSyntax = this.expression;
			}
			return expressionSyntax;
		}

		// Token: 0x06003E97 RID: 16023 RVA: 0x0010A82B File Offset: 0x00108A2B
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitMemberAccessExpression(this);
		}

		// Token: 0x06003E98 RID: 16024 RVA: 0x0010A834 File Offset: 0x00108A34
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitMemberAccessExpression(this);
		}

		// Token: 0x06003E99 RID: 16025 RVA: 0x0010A840 File Offset: 0x00108A40
		public MemberAccessExpressionSyntax Update(ExpressionSyntax expression, SyntaxToken operatorToken, SimpleNameSyntax name)
		{
			if (expression == this.Expression && !(operatorToken != this.OperatorToken) && name == this.Name)
			{
				return this;
			}
			MemberAccessExpressionSyntax memberAccessExpressionSyntax = SyntaxFactory.MemberAccessExpression(base.Kind(), expression, operatorToken, name);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return memberAccessExpressionSyntax;
			}
			return memberAccessExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003E9A RID: 16026 RVA: 0x0010A895 File Offset: 0x00108A95
		public MemberAccessExpressionSyntax WithExpression(ExpressionSyntax expression)
		{
			return this.Update(expression, this.OperatorToken, this.Name);
		}

		// Token: 0x06003E9B RID: 16027 RVA: 0x0010A8AA File Offset: 0x00108AAA
		public MemberAccessExpressionSyntax WithOperatorToken(SyntaxToken operatorToken)
		{
			return this.Update(this.Expression, operatorToken, this.Name);
		}

		// Token: 0x06003E9C RID: 16028 RVA: 0x0010A8BF File Offset: 0x00108ABF
		public MemberAccessExpressionSyntax WithName(SimpleNameSyntax name)
		{
			return this.Update(this.Expression, this.OperatorToken, name);
		}

		// Token: 0x0400145D RID: 5213
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;

		// Token: 0x0400145E RID: 5214
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SimpleNameSyntax name;
	}
}
