using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002C4 RID: 708
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class WithExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x0600401B RID: 16411 RVA: 0x0010D001 File Offset: 0x0010B201
		internal WithExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700109F RID: 4255
		// (get) Token: 0x0600401C RID: 16412 RVA: 0x0010D00C File Offset: 0x0010B20C
		public ExpressionSyntax Expression
		{
			get
			{
				return base.GetRedAtZero<ExpressionSyntax>(ref this.expression);
			}
		}

		// Token: 0x170010A0 RID: 4256
		// (get) Token: 0x0600401D RID: 16413 RVA: 0x0010D01A File Offset: 0x0010B21A
		public SyntaxToken WithKeyword
		{
			get
			{
				return new SyntaxToken(this, ((WithExpressionSyntax)base.Green).withKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x170010A1 RID: 4257
		// (get) Token: 0x0600401E RID: 16414 RVA: 0x0010D040 File Offset: 0x0010B240
		public InitializerExpressionSyntax Initializer
		{
			get
			{
				return base.GetRed<InitializerExpressionSyntax>(ref this.initializer, 2);
			}
		}

		// Token: 0x0600401F RID: 16415 RVA: 0x0010D050 File Offset: 0x0010B250
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
					expressionSyntax = base.GetRed<InitializerExpressionSyntax>(ref this.initializer, 2);
				}
			}
			else
			{
				expressionSyntax = base.GetRedAtZero<ExpressionSyntax>(ref this.expression);
			}
			return expressionSyntax;
		}

		// Token: 0x06004020 RID: 16416 RVA: 0x0010D088 File Offset: 0x0010B288
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
					expressionSyntax = this.initializer;
				}
			}
			else
			{
				expressionSyntax = this.expression;
			}
			return expressionSyntax;
		}

		// Token: 0x06004021 RID: 16417 RVA: 0x0010D0B3 File Offset: 0x0010B2B3
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitWithExpression(this);
		}

		// Token: 0x06004022 RID: 16418 RVA: 0x0010D0BC File Offset: 0x0010B2BC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitWithExpression(this);
		}

		// Token: 0x06004023 RID: 16419 RVA: 0x0010D0C8 File Offset: 0x0010B2C8
		public WithExpressionSyntax Update(ExpressionSyntax expression, SyntaxToken withKeyword, InitializerExpressionSyntax initializer)
		{
			if (expression == this.Expression && !(withKeyword != this.WithKeyword) && initializer == this.Initializer)
			{
				return this;
			}
			WithExpressionSyntax withExpressionSyntax = SyntaxFactory.WithExpression(expression, withKeyword, initializer);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return withExpressionSyntax;
			}
			return withExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004024 RID: 16420 RVA: 0x0010D117 File Offset: 0x0010B317
		public WithExpressionSyntax WithExpression(ExpressionSyntax expression)
		{
			return this.Update(expression, this.WithKeyword, this.Initializer);
		}

		// Token: 0x06004025 RID: 16421 RVA: 0x0010D12C File Offset: 0x0010B32C
		public WithExpressionSyntax WithWithKeyword(SyntaxToken withKeyword)
		{
			return this.Update(this.Expression, withKeyword, this.Initializer);
		}

		// Token: 0x06004026 RID: 16422 RVA: 0x0010D141 File Offset: 0x0010B341
		public WithExpressionSyntax WithInitializer(InitializerExpressionSyntax initializer)
		{
			return this.Update(this.Expression, this.WithKeyword, initializer);
		}

		// Token: 0x06004027 RID: 16423 RVA: 0x0010D158 File Offset: 0x0010B358
		public WithExpressionSyntax AddInitializerExpressions(params ExpressionSyntax[] items)
		{
			return this.WithInitializer(this.Initializer.WithExpressions(this.Initializer.Expressions.AddRange(items)));
		}

		// Token: 0x04001487 RID: 5255
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;

		// Token: 0x04001488 RID: 5256
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private InitializerExpressionSyntax initializer;
	}
}
