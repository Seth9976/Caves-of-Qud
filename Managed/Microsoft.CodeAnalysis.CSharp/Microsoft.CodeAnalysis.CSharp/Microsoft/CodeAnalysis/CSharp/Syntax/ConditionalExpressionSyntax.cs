using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002AB RID: 683
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ConditionalExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06003EE9 RID: 16105 RVA: 0x0010B084 File Offset: 0x00109284
		internal ConditionalExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001057 RID: 4183
		// (get) Token: 0x06003EEA RID: 16106 RVA: 0x0010B08F File Offset: 0x0010928F
		public ExpressionSyntax Condition
		{
			get
			{
				return base.GetRedAtZero<ExpressionSyntax>(ref this.condition);
			}
		}

		// Token: 0x17001058 RID: 4184
		// (get) Token: 0x06003EEB RID: 16107 RVA: 0x0010B09D File Offset: 0x0010929D
		public SyntaxToken QuestionToken
		{
			get
			{
				return new SyntaxToken(this, ((ConditionalExpressionSyntax)base.Green).questionToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001059 RID: 4185
		// (get) Token: 0x06003EEC RID: 16108 RVA: 0x0010B0C3 File Offset: 0x001092C3
		public ExpressionSyntax WhenTrue
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.whenTrue, 2);
			}
		}

		// Token: 0x1700105A RID: 4186
		// (get) Token: 0x06003EED RID: 16109 RVA: 0x0010B0D2 File Offset: 0x001092D2
		public SyntaxToken ColonToken
		{
			get
			{
				return new SyntaxToken(this, ((ConditionalExpressionSyntax)base.Green).colonToken, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x1700105B RID: 4187
		// (get) Token: 0x06003EEE RID: 16110 RVA: 0x0010B0F8 File Offset: 0x001092F8
		public ExpressionSyntax WhenFalse
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.whenFalse, 4);
			}
		}

		// Token: 0x06003EEF RID: 16111 RVA: 0x0010B108 File Offset: 0x00109308
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			switch (index)
			{
			case 0:
				return base.GetRedAtZero<ExpressionSyntax>(ref this.condition);
			case 2:
				return base.GetRed<ExpressionSyntax>(ref this.whenTrue, 2);
			case 4:
				return base.GetRed<ExpressionSyntax>(ref this.whenFalse, 4);
			}
			return null;
		}

		// Token: 0x06003EF0 RID: 16112 RVA: 0x0010B164 File Offset: 0x00109364
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			switch (index)
			{
			case 0:
				return this.condition;
			case 2:
				return this.whenTrue;
			case 4:
				return this.whenFalse;
			}
			return null;
		}

		// Token: 0x06003EF1 RID: 16113 RVA: 0x0010B1AB File Offset: 0x001093AB
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitConditionalExpression(this);
		}

		// Token: 0x06003EF2 RID: 16114 RVA: 0x0010B1B4 File Offset: 0x001093B4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitConditionalExpression(this);
		}

		// Token: 0x06003EF3 RID: 16115 RVA: 0x0010B1C0 File Offset: 0x001093C0
		public ConditionalExpressionSyntax Update(ExpressionSyntax condition, SyntaxToken questionToken, ExpressionSyntax whenTrue, SyntaxToken colonToken, ExpressionSyntax whenFalse)
		{
			if (condition == this.Condition && !(questionToken != this.QuestionToken) && whenTrue == this.WhenTrue && !(colonToken != this.ColonToken) && whenFalse == this.WhenFalse)
			{
				return this;
			}
			ConditionalExpressionSyntax conditionalExpressionSyntax = SyntaxFactory.ConditionalExpression(condition, questionToken, whenTrue, colonToken, whenFalse);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return conditionalExpressionSyntax;
			}
			return conditionalExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003EF4 RID: 16116 RVA: 0x0010B22C File Offset: 0x0010942C
		public ConditionalExpressionSyntax WithCondition(ExpressionSyntax condition)
		{
			return this.Update(condition, this.QuestionToken, this.WhenTrue, this.ColonToken, this.WhenFalse);
		}

		// Token: 0x06003EF5 RID: 16117 RVA: 0x0010B24D File Offset: 0x0010944D
		public ConditionalExpressionSyntax WithQuestionToken(SyntaxToken questionToken)
		{
			return this.Update(this.Condition, questionToken, this.WhenTrue, this.ColonToken, this.WhenFalse);
		}

		// Token: 0x06003EF6 RID: 16118 RVA: 0x0010B26E File Offset: 0x0010946E
		public ConditionalExpressionSyntax WithWhenTrue(ExpressionSyntax whenTrue)
		{
			return this.Update(this.Condition, this.QuestionToken, whenTrue, this.ColonToken, this.WhenFalse);
		}

		// Token: 0x06003EF7 RID: 16119 RVA: 0x0010B28F File Offset: 0x0010948F
		public ConditionalExpressionSyntax WithColonToken(SyntaxToken colonToken)
		{
			return this.Update(this.Condition, this.QuestionToken, this.WhenTrue, colonToken, this.WhenFalse);
		}

		// Token: 0x06003EF8 RID: 16120 RVA: 0x0010B2B0 File Offset: 0x001094B0
		public ConditionalExpressionSyntax WithWhenFalse(ExpressionSyntax whenFalse)
		{
			return this.Update(this.Condition, this.QuestionToken, this.WhenTrue, this.ColonToken, whenFalse);
		}

		// Token: 0x0400146A RID: 5226
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax condition;

		// Token: 0x0400146B RID: 5227
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax whenTrue;

		// Token: 0x0400146C RID: 5228
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax whenFalse;
	}
}
