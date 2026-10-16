using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002CF RID: 719
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class LetClauseSyntax : QueryClauseSyntax
	{
		// Token: 0x06004099 RID: 16537 RVA: 0x0010DF22 File Offset: 0x0010C122
		internal LetClauseSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170010BE RID: 4286
		// (get) Token: 0x0600409A RID: 16538 RVA: 0x0010DF2D File Offset: 0x0010C12D
		public SyntaxToken LetKeyword
		{
			get
			{
				return new SyntaxToken(this, ((LetClauseSyntax)base.Green).letKeyword, base.Position, 0);
			}
		}

		// Token: 0x170010BF RID: 4287
		// (get) Token: 0x0600409B RID: 16539 RVA: 0x0010DF4C File Offset: 0x0010C14C
		public SyntaxToken Identifier
		{
			get
			{
				return new SyntaxToken(this, ((LetClauseSyntax)base.Green).identifier, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x170010C0 RID: 4288
		// (get) Token: 0x0600409C RID: 16540 RVA: 0x0010DF72 File Offset: 0x0010C172
		public SyntaxToken EqualsToken
		{
			get
			{
				return new SyntaxToken(this, ((LetClauseSyntax)base.Green).equalsToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x170010C1 RID: 4289
		// (get) Token: 0x0600409D RID: 16541 RVA: 0x0010DF98 File Offset: 0x0010C198
		public ExpressionSyntax Expression
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.expression, 3);
			}
		}

		// Token: 0x0600409E RID: 16542 RVA: 0x0010DFA7 File Offset: 0x0010C1A7
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 3)
			{
				return null;
			}
			return base.GetRed<ExpressionSyntax>(ref this.expression, 3);
		}

		// Token: 0x0600409F RID: 16543 RVA: 0x0010DFBC File Offset: 0x0010C1BC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 3)
			{
				return null;
			}
			return this.expression;
		}

		// Token: 0x060040A0 RID: 16544 RVA: 0x0010DFCA File Offset: 0x0010C1CA
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitLetClause(this);
		}

		// Token: 0x060040A1 RID: 16545 RVA: 0x0010DFD3 File Offset: 0x0010C1D3
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitLetClause(this);
		}

		// Token: 0x060040A2 RID: 16546 RVA: 0x0010DFDC File Offset: 0x0010C1DC
		public LetClauseSyntax Update(SyntaxToken letKeyword, SyntaxToken identifier, SyntaxToken equalsToken, ExpressionSyntax expression)
		{
			if (!(letKeyword != this.LetKeyword) && !(identifier != this.Identifier) && !(equalsToken != this.EqualsToken) && expression == this.Expression)
			{
				return this;
			}
			LetClauseSyntax letClauseSyntax = SyntaxFactory.LetClause(letKeyword, identifier, equalsToken, expression);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return letClauseSyntax;
			}
			return letClauseSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060040A3 RID: 16547 RVA: 0x0010E041 File Offset: 0x0010C241
		public LetClauseSyntax WithLetKeyword(SyntaxToken letKeyword)
		{
			return this.Update(letKeyword, this.Identifier, this.EqualsToken, this.Expression);
		}

		// Token: 0x060040A4 RID: 16548 RVA: 0x0010E05C File Offset: 0x0010C25C
		public LetClauseSyntax WithIdentifier(SyntaxToken identifier)
		{
			return this.Update(this.LetKeyword, identifier, this.EqualsToken, this.Expression);
		}

		// Token: 0x060040A5 RID: 16549 RVA: 0x0010E077 File Offset: 0x0010C277
		public LetClauseSyntax WithEqualsToken(SyntaxToken equalsToken)
		{
			return this.Update(this.LetKeyword, this.Identifier, equalsToken, this.Expression);
		}

		// Token: 0x060040A6 RID: 16550 RVA: 0x0010E092 File Offset: 0x0010C292
		public LetClauseSyntax WithExpression(ExpressionSyntax expression)
		{
			return this.Update(this.LetKeyword, this.Identifier, this.EqualsToken, expression);
		}

		// Token: 0x04001497 RID: 5271
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;
	}
}
