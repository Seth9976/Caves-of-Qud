using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000303 RID: 771
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class CatchFilterClauseSyntax : CSharpSyntaxNode
	{
		// Token: 0x060042E0 RID: 17120 RVA: 0x00111A75 File Offset: 0x0010FC75
		internal CatchFilterClauseSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001143 RID: 4419
		// (get) Token: 0x060042E1 RID: 17121 RVA: 0x00111A80 File Offset: 0x0010FC80
		public SyntaxToken WhenKeyword
		{
			get
			{
				return new SyntaxToken(this, ((CatchFilterClauseSyntax)base.Green).whenKeyword, base.Position, 0);
			}
		}

		// Token: 0x17001144 RID: 4420
		// (get) Token: 0x060042E2 RID: 17122 RVA: 0x00111A9F File Offset: 0x0010FC9F
		public SyntaxToken OpenParenToken
		{
			get
			{
				return new SyntaxToken(this, ((CatchFilterClauseSyntax)base.Green).openParenToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001145 RID: 4421
		// (get) Token: 0x060042E3 RID: 17123 RVA: 0x00111AC5 File Offset: 0x0010FCC5
		public ExpressionSyntax FilterExpression
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.filterExpression, 2);
			}
		}

		// Token: 0x17001146 RID: 4422
		// (get) Token: 0x060042E4 RID: 17124 RVA: 0x00111AD4 File Offset: 0x0010FCD4
		public SyntaxToken CloseParenToken
		{
			get
			{
				return new SyntaxToken(this, ((CatchFilterClauseSyntax)base.Green).closeParenToken, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x060042E5 RID: 17125 RVA: 0x00111AFA File Offset: 0x0010FCFA
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 2)
			{
				return null;
			}
			return base.GetRed<ExpressionSyntax>(ref this.filterExpression, 2);
		}

		// Token: 0x060042E6 RID: 17126 RVA: 0x00111B0F File Offset: 0x0010FD0F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 2)
			{
				return null;
			}
			return this.filterExpression;
		}

		// Token: 0x060042E7 RID: 17127 RVA: 0x00111B1D File Offset: 0x0010FD1D
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitCatchFilterClause(this);
		}

		// Token: 0x060042E8 RID: 17128 RVA: 0x00111B26 File Offset: 0x0010FD26
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitCatchFilterClause(this);
		}

		// Token: 0x060042E9 RID: 17129 RVA: 0x00111B30 File Offset: 0x0010FD30
		public CatchFilterClauseSyntax Update(SyntaxToken whenKeyword, SyntaxToken openParenToken, ExpressionSyntax filterExpression, SyntaxToken closeParenToken)
		{
			if (!(whenKeyword != this.WhenKeyword) && !(openParenToken != this.OpenParenToken) && filterExpression == this.FilterExpression && !(closeParenToken != this.CloseParenToken))
			{
				return this;
			}
			CatchFilterClauseSyntax catchFilterClauseSyntax = SyntaxFactory.CatchFilterClause(whenKeyword, openParenToken, filterExpression, closeParenToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return catchFilterClauseSyntax;
			}
			return catchFilterClauseSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060042EA RID: 17130 RVA: 0x00111B95 File Offset: 0x0010FD95
		public CatchFilterClauseSyntax WithWhenKeyword(SyntaxToken whenKeyword)
		{
			return this.Update(whenKeyword, this.OpenParenToken, this.FilterExpression, this.CloseParenToken);
		}

		// Token: 0x060042EB RID: 17131 RVA: 0x00111BB0 File Offset: 0x0010FDB0
		public CatchFilterClauseSyntax WithOpenParenToken(SyntaxToken openParenToken)
		{
			return this.Update(this.WhenKeyword, openParenToken, this.FilterExpression, this.CloseParenToken);
		}

		// Token: 0x060042EC RID: 17132 RVA: 0x00111BCB File Offset: 0x0010FDCB
		public CatchFilterClauseSyntax WithFilterExpression(ExpressionSyntax filterExpression)
		{
			return this.Update(this.WhenKeyword, this.OpenParenToken, filterExpression, this.CloseParenToken);
		}

		// Token: 0x060042ED RID: 17133 RVA: 0x00111BE6 File Offset: 0x0010FDE6
		public CatchFilterClauseSyntax WithCloseParenToken(SyntaxToken closeParenToken)
		{
			return this.Update(this.WhenKeyword, this.OpenParenToken, this.FilterExpression, closeParenToken);
		}

		// Token: 0x040014D4 RID: 5332
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax filterExpression;
	}
}
