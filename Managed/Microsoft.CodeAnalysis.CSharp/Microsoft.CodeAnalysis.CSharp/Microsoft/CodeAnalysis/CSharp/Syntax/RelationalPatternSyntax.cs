using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002E7 RID: 743
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class RelationalPatternSyntax : PatternSyntax
	{
		// Token: 0x060041A9 RID: 16809 RVA: 0x0010FD4B File Offset: 0x0010DF4B
		internal RelationalPatternSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170010FD RID: 4349
		// (get) Token: 0x060041AA RID: 16810 RVA: 0x0010FD56 File Offset: 0x0010DF56
		public SyntaxToken OperatorToken
		{
			get
			{
				return new SyntaxToken(this, ((RelationalPatternSyntax)base.Green).operatorToken, base.Position, 0);
			}
		}

		// Token: 0x170010FE RID: 4350
		// (get) Token: 0x060041AB RID: 16811 RVA: 0x0010FD75 File Offset: 0x0010DF75
		public ExpressionSyntax Expression
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.expression, 1);
			}
		}

		// Token: 0x060041AC RID: 16812 RVA: 0x0010FD84 File Offset: 0x0010DF84
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed<ExpressionSyntax>(ref this.expression, 1);
		}

		// Token: 0x060041AD RID: 16813 RVA: 0x0010FD99 File Offset: 0x0010DF99
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.expression;
		}

		// Token: 0x060041AE RID: 16814 RVA: 0x0010FDA7 File Offset: 0x0010DFA7
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitRelationalPattern(this);
		}

		// Token: 0x060041AF RID: 16815 RVA: 0x0010FDB0 File Offset: 0x0010DFB0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitRelationalPattern(this);
		}

		// Token: 0x060041B0 RID: 16816 RVA: 0x0010FDBC File Offset: 0x0010DFBC
		public RelationalPatternSyntax Update(SyntaxToken operatorToken, ExpressionSyntax expression)
		{
			if (!(operatorToken != this.OperatorToken) && expression == this.Expression)
			{
				return this;
			}
			RelationalPatternSyntax relationalPatternSyntax = SyntaxFactory.RelationalPattern(operatorToken, expression);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return relationalPatternSyntax;
			}
			return relationalPatternSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060041B1 RID: 16817 RVA: 0x0010FE01 File Offset: 0x0010E001
		public RelationalPatternSyntax WithOperatorToken(SyntaxToken operatorToken)
		{
			return this.Update(operatorToken, this.Expression);
		}

		// Token: 0x060041B2 RID: 16818 RVA: 0x0010FE10 File Offset: 0x0010E010
		public RelationalPatternSyntax WithExpression(ExpressionSyntax expression)
		{
			return this.Update(this.OperatorToken, expression);
		}

		// Token: 0x040014B6 RID: 5302
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax expression;
	}
}
