using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002D7 RID: 727
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class QueryContinuationSyntax : CSharpSyntaxNode
	{
		// Token: 0x06004102 RID: 16642 RVA: 0x0010ECC4 File Offset: 0x0010CEC4
		internal QueryContinuationSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170010DA RID: 4314
		// (get) Token: 0x06004103 RID: 16643 RVA: 0x0010ECCF File Offset: 0x0010CECF
		public SyntaxToken IntoKeyword
		{
			get
			{
				return new SyntaxToken(this, ((QueryContinuationSyntax)base.Green).intoKeyword, base.Position, 0);
			}
		}

		// Token: 0x170010DB RID: 4315
		// (get) Token: 0x06004104 RID: 16644 RVA: 0x0010ECEE File Offset: 0x0010CEEE
		public SyntaxToken Identifier
		{
			get
			{
				return new SyntaxToken(this, ((QueryContinuationSyntax)base.Green).identifier, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x170010DC RID: 4316
		// (get) Token: 0x06004105 RID: 16645 RVA: 0x0010ED14 File Offset: 0x0010CF14
		public QueryBodySyntax Body
		{
			get
			{
				return base.GetRed<QueryBodySyntax>(ref this.body, 2);
			}
		}

		// Token: 0x06004106 RID: 16646 RVA: 0x0010ED23 File Offset: 0x0010CF23
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 2)
			{
				return null;
			}
			return base.GetRed<QueryBodySyntax>(ref this.body, 2);
		}

		// Token: 0x06004107 RID: 16647 RVA: 0x0010ED38 File Offset: 0x0010CF38
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 2)
			{
				return null;
			}
			return this.body;
		}

		// Token: 0x06004108 RID: 16648 RVA: 0x0010ED46 File Offset: 0x0010CF46
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitQueryContinuation(this);
		}

		// Token: 0x06004109 RID: 16649 RVA: 0x0010ED4F File Offset: 0x0010CF4F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitQueryContinuation(this);
		}

		// Token: 0x0600410A RID: 16650 RVA: 0x0010ED58 File Offset: 0x0010CF58
		public QueryContinuationSyntax Update(SyntaxToken intoKeyword, SyntaxToken identifier, QueryBodySyntax body)
		{
			if (!(intoKeyword != this.IntoKeyword) && !(identifier != this.Identifier) && body == this.Body)
			{
				return this;
			}
			QueryContinuationSyntax queryContinuationSyntax = SyntaxFactory.QueryContinuation(intoKeyword, identifier, body);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return queryContinuationSyntax;
			}
			return queryContinuationSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600410B RID: 16651 RVA: 0x0010EDAC File Offset: 0x0010CFAC
		public QueryContinuationSyntax WithIntoKeyword(SyntaxToken intoKeyword)
		{
			return this.Update(intoKeyword, this.Identifier, this.Body);
		}

		// Token: 0x0600410C RID: 16652 RVA: 0x0010EDC1 File Offset: 0x0010CFC1
		public QueryContinuationSyntax WithIdentifier(SyntaxToken identifier)
		{
			return this.Update(this.IntoKeyword, identifier, this.Body);
		}

		// Token: 0x0600410D RID: 16653 RVA: 0x0010EDD6 File Offset: 0x0010CFD6
		public QueryContinuationSyntax WithBody(QueryBodySyntax body)
		{
			return this.Update(this.IntoKeyword, this.Identifier, body);
		}

		// Token: 0x0600410E RID: 16654 RVA: 0x0010EDEC File Offset: 0x0010CFEC
		public QueryContinuationSyntax AddBodyClauses(params QueryClauseSyntax[] items)
		{
			return this.WithBody(this.Body.WithClauses(this.Body.Clauses.AddRange(items)));
		}

		// Token: 0x040014A3 RID: 5283
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private QueryBodySyntax body;
	}
}
