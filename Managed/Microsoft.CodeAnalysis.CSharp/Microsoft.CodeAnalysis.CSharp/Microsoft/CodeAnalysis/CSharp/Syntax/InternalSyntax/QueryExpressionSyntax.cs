using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003C3 RID: 963
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class QueryExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x060054C8 RID: 21704 RVA: 0x00150D2B File Offset: 0x0014EF2B
		internal QueryExpressionSyntax(SyntaxKind kind, FromClauseSyntax fromClause, QueryBodySyntax body, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(fromClause);
			this.fromClause = fromClause;
			base.AdjustFlagsAndWidth(body);
			this.body = body;
		}

		// Token: 0x060054C9 RID: 21705 RVA: 0x00150D5B File Offset: 0x0014EF5B
		internal QueryExpressionSyntax(SyntaxKind kind, FromClauseSyntax fromClause, QueryBodySyntax body, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(fromClause);
			this.fromClause = fromClause;
			base.AdjustFlagsAndWidth(body);
			this.body = body;
		}

		// Token: 0x060054CA RID: 21706 RVA: 0x00150D8F File Offset: 0x0014EF8F
		internal QueryExpressionSyntax(SyntaxKind kind, FromClauseSyntax fromClause, QueryBodySyntax body)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(fromClause);
			this.fromClause = fromClause;
			base.AdjustFlagsAndWidth(body);
			this.body = body;
		}

		// Token: 0x17001385 RID: 4997
		// (get) Token: 0x060054CB RID: 21707 RVA: 0x00150DBB File Offset: 0x0014EFBB
		public FromClauseSyntax FromClause
		{
			get
			{
				return this.fromClause;
			}
		}

		// Token: 0x17001386 RID: 4998
		// (get) Token: 0x060054CC RID: 21708 RVA: 0x00150DC3 File Offset: 0x0014EFC3
		public QueryBodySyntax Body
		{
			get
			{
				return this.body;
			}
		}

		// Token: 0x060054CD RID: 21709 RVA: 0x00150DCC File Offset: 0x0014EFCC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			if (index != 0)
			{
				if (index != 1)
				{
					greenNode = null;
				}
				else
				{
					greenNode = this.body;
				}
			}
			else
			{
				greenNode = this.fromClause;
			}
			return greenNode;
		}

		// Token: 0x060054CE RID: 21710 RVA: 0x00150DF7 File Offset: 0x0014EFF7
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new QueryExpressionSyntax(this, parent, position);
		}

		// Token: 0x060054CF RID: 21711 RVA: 0x00150E01 File Offset: 0x0014F001
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitQueryExpression(this);
		}

		// Token: 0x060054D0 RID: 21712 RVA: 0x00150E0A File Offset: 0x0014F00A
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitQueryExpression(this);
		}

		// Token: 0x060054D1 RID: 21713 RVA: 0x00150E14 File Offset: 0x0014F014
		public QueryExpressionSyntax Update(FromClauseSyntax fromClause, QueryBodySyntax body)
		{
			if (fromClause != this.FromClause || body != this.Body)
			{
				QueryExpressionSyntax queryExpressionSyntax = SyntaxFactory.QueryExpression(fromClause, body);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					queryExpressionSyntax = queryExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					queryExpressionSyntax = queryExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return queryExpressionSyntax;
			}
			return this;
		}

		// Token: 0x060054D2 RID: 21714 RVA: 0x00150E6A File Offset: 0x0014F06A
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new QueryExpressionSyntax(base.Kind, this.fromClause, this.body, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060054D3 RID: 21715 RVA: 0x00150E8A File Offset: 0x0014F08A
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new QueryExpressionSyntax(base.Kind, this.fromClause, this.body, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060054D4 RID: 21716 RVA: 0x00150EAC File Offset: 0x0014F0AC
		internal QueryExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			FromClauseSyntax fromClauseSyntax = (FromClauseSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(fromClauseSyntax);
			this.fromClause = fromClauseSyntax;
			QueryBodySyntax queryBodySyntax = (QueryBodySyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(queryBodySyntax);
			this.body = queryBodySyntax;
		}

		// Token: 0x060054D5 RID: 21717 RVA: 0x00150EFB File Offset: 0x0014F0FB
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.fromClause);
			writer.WriteValue(this.body);
		}

		// Token: 0x060054D6 RID: 21718 RVA: 0x00150F1C File Offset: 0x0014F11C
		static QueryExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(QueryExpressionSyntax), (ObjectReader r) => new QueryExpressionSyntax(r));
		}

		// Token: 0x04001695 RID: 5781
		internal readonly FromClauseSyntax fromClause;

		// Token: 0x04001696 RID: 5782
		internal readonly QueryBodySyntax body;
	}
}
