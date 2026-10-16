using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000300 RID: 768
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class SwitchExpressionArmSyntax : CSharpSyntaxNode
	{
		// Token: 0x060042B4 RID: 17076 RVA: 0x001114C6 File Offset: 0x0010F6C6
		internal SwitchExpressionArmSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001137 RID: 4407
		// (get) Token: 0x060042B5 RID: 17077 RVA: 0x001114D1 File Offset: 0x0010F6D1
		public PatternSyntax Pattern
		{
			get
			{
				return base.GetRedAtZero<PatternSyntax>(ref this.pattern);
			}
		}

		// Token: 0x17001138 RID: 4408
		// (get) Token: 0x060042B6 RID: 17078 RVA: 0x001114DF File Offset: 0x0010F6DF
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public WhenClauseSyntax WhenClause
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<WhenClauseSyntax>(ref this.whenClause, 1);
			}
		}

		// Token: 0x17001139 RID: 4409
		// (get) Token: 0x060042B7 RID: 17079 RVA: 0x001114EE File Offset: 0x0010F6EE
		public SyntaxToken EqualsGreaterThanToken
		{
			get
			{
				return new SyntaxToken(this, ((SwitchExpressionArmSyntax)base.Green).equalsGreaterThanToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x1700113A RID: 4410
		// (get) Token: 0x060042B8 RID: 17080 RVA: 0x00111514 File Offset: 0x0010F714
		public ExpressionSyntax Expression
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.expression, 3);
			}
		}

		// Token: 0x060042B9 RID: 17081 RVA: 0x00111524 File Offset: 0x0010F724
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			switch (index)
			{
			case 0:
				return base.GetRedAtZero<PatternSyntax>(ref this.pattern);
			case 1:
				return base.GetRed<WhenClauseSyntax>(ref this.whenClause, 1);
			case 3:
				return base.GetRed<ExpressionSyntax>(ref this.expression, 3);
			}
			return null;
		}

		// Token: 0x060042BA RID: 17082 RVA: 0x0011157C File Offset: 0x0010F77C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			switch (index)
			{
			case 0:
				return this.pattern;
			case 1:
				return this.whenClause;
			case 3:
				return this.expression;
			}
			return null;
		}

		// Token: 0x060042BB RID: 17083 RVA: 0x001115BF File Offset: 0x0010F7BF
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitSwitchExpressionArm(this);
		}

		// Token: 0x060042BC RID: 17084 RVA: 0x001115C8 File Offset: 0x0010F7C8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitSwitchExpressionArm(this);
		}

		// Token: 0x060042BD RID: 17085 RVA: 0x001115D4 File Offset: 0x0010F7D4
		public SwitchExpressionArmSyntax Update(PatternSyntax pattern, [global::System.Runtime.CompilerServices.Nullable(2)] WhenClauseSyntax whenClause, SyntaxToken equalsGreaterThanToken, ExpressionSyntax expression)
		{
			if (pattern == this.Pattern && whenClause == this.WhenClause && !(equalsGreaterThanToken != this.EqualsGreaterThanToken) && expression == this.Expression)
			{
				return this;
			}
			SwitchExpressionArmSyntax switchExpressionArmSyntax = SyntaxFactory.SwitchExpressionArm(pattern, whenClause, equalsGreaterThanToken, expression);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return switchExpressionArmSyntax;
			}
			return switchExpressionArmSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060042BE RID: 17086 RVA: 0x0011162F File Offset: 0x0010F82F
		public SwitchExpressionArmSyntax WithPattern(PatternSyntax pattern)
		{
			return this.Update(pattern, this.WhenClause, this.EqualsGreaterThanToken, this.Expression);
		}

		// Token: 0x060042BF RID: 17087 RVA: 0x0011164A File Offset: 0x0010F84A
		public SwitchExpressionArmSyntax WithWhenClause([global::System.Runtime.CompilerServices.Nullable(2)] WhenClauseSyntax whenClause)
		{
			return this.Update(this.Pattern, whenClause, this.EqualsGreaterThanToken, this.Expression);
		}

		// Token: 0x060042C0 RID: 17088 RVA: 0x00111665 File Offset: 0x0010F865
		public SwitchExpressionArmSyntax WithEqualsGreaterThanToken(SyntaxToken equalsGreaterThanToken)
		{
			return this.Update(this.Pattern, this.WhenClause, equalsGreaterThanToken, this.Expression);
		}

		// Token: 0x060042C1 RID: 17089 RVA: 0x00111680 File Offset: 0x0010F880
		public SwitchExpressionArmSyntax WithExpression(ExpressionSyntax expression)
		{
			return this.Update(this.Pattern, this.WhenClause, this.EqualsGreaterThanToken, expression);
		}

		// Token: 0x040014CD RID: 5325
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private PatternSyntax pattern;

		// Token: 0x040014CE RID: 5326
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private WhenClauseSyntax whenClause;

		// Token: 0x040014CF RID: 5327
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;
	}
}
