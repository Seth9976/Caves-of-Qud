using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002CC RID: 716
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class QueryExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06004071 RID: 16497 RVA: 0x0010DA30 File Offset: 0x0010BC30
		internal QueryExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170010B4 RID: 4276
		// (get) Token: 0x06004072 RID: 16498 RVA: 0x0010DA3B File Offset: 0x0010BC3B
		public FromClauseSyntax FromClause
		{
			get
			{
				return base.GetRedAtZero<FromClauseSyntax>(ref this.fromClause);
			}
		}

		// Token: 0x170010B5 RID: 4277
		// (get) Token: 0x06004073 RID: 16499 RVA: 0x0010DA49 File Offset: 0x0010BC49
		public QueryBodySyntax Body
		{
			get
			{
				return base.GetRed<QueryBodySyntax>(ref this.body, 1);
			}
		}

		// Token: 0x06004074 RID: 16500 RVA: 0x0010DA58 File Offset: 0x0010BC58
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 1)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = base.GetRed<QueryBodySyntax>(ref this.body, 1);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero<FromClauseSyntax>(ref this.fromClause);
			}
			return syntaxNode;
		}

		// Token: 0x06004075 RID: 16501 RVA: 0x0010DA90 File Offset: 0x0010BC90
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 1)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = this.body;
				}
			}
			else
			{
				syntaxNode = this.fromClause;
			}
			return syntaxNode;
		}

		// Token: 0x06004076 RID: 16502 RVA: 0x0010DABB File Offset: 0x0010BCBB
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitQueryExpression(this);
		}

		// Token: 0x06004077 RID: 16503 RVA: 0x0010DAC4 File Offset: 0x0010BCC4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitQueryExpression(this);
		}

		// Token: 0x06004078 RID: 16504 RVA: 0x0010DAD0 File Offset: 0x0010BCD0
		public QueryExpressionSyntax Update(FromClauseSyntax fromClause, QueryBodySyntax body)
		{
			if (fromClause == this.FromClause && body == this.Body)
			{
				return this;
			}
			QueryExpressionSyntax queryExpressionSyntax = SyntaxFactory.QueryExpression(fromClause, body);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return queryExpressionSyntax;
			}
			return queryExpressionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004079 RID: 16505 RVA: 0x0010DB10 File Offset: 0x0010BD10
		public QueryExpressionSyntax WithFromClause(FromClauseSyntax fromClause)
		{
			return this.Update(fromClause, this.Body);
		}

		// Token: 0x0600407A RID: 16506 RVA: 0x0010DB1F File Offset: 0x0010BD1F
		public QueryExpressionSyntax WithBody(QueryBodySyntax body)
		{
			return this.Update(this.FromClause, body);
		}

		// Token: 0x0600407B RID: 16507 RVA: 0x0010DB30 File Offset: 0x0010BD30
		public QueryExpressionSyntax AddBodyClauses(params QueryClauseSyntax[] items)
		{
			return this.WithBody(this.Body.WithClauses(this.Body.Clauses.AddRange(items)));
		}

		// Token: 0x04001490 RID: 5264
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private FromClauseSyntax fromClause;

		// Token: 0x04001491 RID: 5265
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private QueryBodySyntax body;
	}
}
