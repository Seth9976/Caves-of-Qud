using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002D2 RID: 722
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class WhereClauseSyntax : QueryClauseSyntax
	{
		// Token: 0x060040CB RID: 16587 RVA: 0x0010E74C File Offset: 0x0010C94C
		internal WhereClauseSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170010CE RID: 4302
		// (get) Token: 0x060040CC RID: 16588 RVA: 0x0010E757 File Offset: 0x0010C957
		public SyntaxToken WhereKeyword
		{
			get
			{
				return new SyntaxToken(this, ((WhereClauseSyntax)base.Green).whereKeyword, base.Position, 0);
			}
		}

		// Token: 0x170010CF RID: 4303
		// (get) Token: 0x060040CD RID: 16589 RVA: 0x0010E776 File Offset: 0x0010C976
		public ExpressionSyntax Condition
		{
			get
			{
				return base.GetRed<ExpressionSyntax>(ref this.condition, 1);
			}
		}

		// Token: 0x060040CE RID: 16590 RVA: 0x0010E785 File Offset: 0x0010C985
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed<ExpressionSyntax>(ref this.condition, 1);
		}

		// Token: 0x060040CF RID: 16591 RVA: 0x0010E79A File Offset: 0x0010C99A
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.condition;
		}

		// Token: 0x060040D0 RID: 16592 RVA: 0x0010E7A8 File Offset: 0x0010C9A8
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitWhereClause(this);
		}

		// Token: 0x060040D1 RID: 16593 RVA: 0x0010E7B1 File Offset: 0x0010C9B1
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitWhereClause(this);
		}

		// Token: 0x060040D2 RID: 16594 RVA: 0x0010E7BC File Offset: 0x0010C9BC
		public WhereClauseSyntax Update(SyntaxToken whereKeyword, ExpressionSyntax condition)
		{
			if (!(whereKeyword != this.WhereKeyword) && condition == this.Condition)
			{
				return this;
			}
			WhereClauseSyntax whereClauseSyntax = SyntaxFactory.WhereClause(whereKeyword, condition);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return whereClauseSyntax;
			}
			return whereClauseSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060040D3 RID: 16595 RVA: 0x0010E801 File Offset: 0x0010CA01
		public WhereClauseSyntax WithWhereKeyword(SyntaxToken whereKeyword)
		{
			return this.Update(whereKeyword, this.Condition);
		}

		// Token: 0x060040D4 RID: 16596 RVA: 0x0010E810 File Offset: 0x0010CA10
		public WhereClauseSyntax WithCondition(ExpressionSyntax condition)
		{
			return this.Update(this.WhereKeyword, condition);
		}

		// Token: 0x0400149D RID: 5277
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExpressionSyntax condition;
	}
}
