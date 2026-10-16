using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002CD RID: 717
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class QueryBodySyntax : CSharpSyntaxNode
	{
		// Token: 0x0600407C RID: 16508 RVA: 0x0010DB62 File Offset: 0x0010BD62
		internal QueryBodySyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170010B6 RID: 4278
		// (get) Token: 0x0600407D RID: 16509 RVA: 0x0010DB6D File Offset: 0x0010BD6D
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<QueryClauseSyntax> Clauses
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<QueryClauseSyntax>(base.GetRed(ref this.clauses, 0));
			}
		}

		// Token: 0x170010B7 RID: 4279
		// (get) Token: 0x0600407E RID: 16510 RVA: 0x0010DB81 File Offset: 0x0010BD81
		public SelectOrGroupClauseSyntax SelectOrGroup
		{
			get
			{
				return base.GetRed<SelectOrGroupClauseSyntax>(ref this.selectOrGroup, 1);
			}
		}

		// Token: 0x170010B8 RID: 4280
		// (get) Token: 0x0600407F RID: 16511 RVA: 0x0010DB90 File Offset: 0x0010BD90
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public QueryContinuationSyntax Continuation
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<QueryContinuationSyntax>(ref this.continuation, 2);
			}
		}

		// Token: 0x06004080 RID: 16512 RVA: 0x0010DBA0 File Offset: 0x0010BDA0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			switch (index)
			{
			case 0:
				syntaxNode = base.GetRedAtZero(ref this.clauses);
				break;
			case 1:
				syntaxNode = base.GetRed<SelectOrGroupClauseSyntax>(ref this.selectOrGroup, 1);
				break;
			case 2:
				syntaxNode = base.GetRed<QueryContinuationSyntax>(ref this.continuation, 2);
				break;
			default:
				syntaxNode = null;
				break;
			}
			return syntaxNode;
		}

		// Token: 0x06004081 RID: 16513 RVA: 0x0010DBF4 File Offset: 0x0010BDF4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			switch (index)
			{
			case 0:
				syntaxNode = this.clauses;
				break;
			case 1:
				syntaxNode = this.selectOrGroup;
				break;
			case 2:
				syntaxNode = this.continuation;
				break;
			default:
				syntaxNode = null;
				break;
			}
			return syntaxNode;
		}

		// Token: 0x06004082 RID: 16514 RVA: 0x0010DC33 File Offset: 0x0010BE33
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitQueryBody(this);
		}

		// Token: 0x06004083 RID: 16515 RVA: 0x0010DC3C File Offset: 0x0010BE3C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitQueryBody(this);
		}

		// Token: 0x06004084 RID: 16516 RVA: 0x0010DC48 File Offset: 0x0010BE48
		public QueryBodySyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<QueryClauseSyntax> clauses, SelectOrGroupClauseSyntax selectOrGroup, [global::System.Runtime.CompilerServices.Nullable(2)] QueryContinuationSyntax continuation)
		{
			if (!(clauses != this.Clauses) && selectOrGroup == this.SelectOrGroup && continuation == this.Continuation)
			{
				return this;
			}
			QueryBodySyntax queryBodySyntax = SyntaxFactory.QueryBody(clauses, selectOrGroup, continuation);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return queryBodySyntax;
			}
			return queryBodySyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004085 RID: 16517 RVA: 0x0010DC97 File Offset: 0x0010BE97
		public QueryBodySyntax WithClauses([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<QueryClauseSyntax> clauses)
		{
			return this.Update(clauses, this.SelectOrGroup, this.Continuation);
		}

		// Token: 0x06004086 RID: 16518 RVA: 0x0010DCAC File Offset: 0x0010BEAC
		public QueryBodySyntax WithSelectOrGroup(SelectOrGroupClauseSyntax selectOrGroup)
		{
			return this.Update(this.Clauses, selectOrGroup, this.Continuation);
		}

		// Token: 0x06004087 RID: 16519 RVA: 0x0010DCC1 File Offset: 0x0010BEC1
		public QueryBodySyntax WithContinuation([global::System.Runtime.CompilerServices.Nullable(2)] QueryContinuationSyntax continuation)
		{
			return this.Update(this.Clauses, this.SelectOrGroup, continuation);
		}

		// Token: 0x06004088 RID: 16520 RVA: 0x0010DCD8 File Offset: 0x0010BED8
		public QueryBodySyntax AddClauses(params QueryClauseSyntax[] items)
		{
			return this.WithClauses(this.Clauses.AddRange(items));
		}

		// Token: 0x04001492 RID: 5266
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode clauses;

		// Token: 0x04001493 RID: 5267
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SelectOrGroupClauseSyntax selectOrGroup;

		// Token: 0x04001494 RID: 5268
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private QueryContinuationSyntax continuation;
	}
}
