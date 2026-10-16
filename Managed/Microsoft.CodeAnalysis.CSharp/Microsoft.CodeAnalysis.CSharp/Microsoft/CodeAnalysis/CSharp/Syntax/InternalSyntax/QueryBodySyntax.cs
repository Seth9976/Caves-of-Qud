using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003C4 RID: 964
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class QueryBodySyntax : CSharpSyntaxNode
	{
		// Token: 0x060054D7 RID: 21719 RVA: 0x00150F40 File Offset: 0x0014F140
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal QueryBodySyntax(SyntaxKind kind, GreenNode clauses, [global::System.Runtime.CompilerServices.Nullable(1)] SelectOrGroupClauseSyntax selectOrGroup, QueryContinuationSyntax continuation, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			if (clauses != null)
			{
				base.AdjustFlagsAndWidth(clauses);
				this.clauses = clauses;
			}
			base.AdjustFlagsAndWidth(selectOrGroup);
			this.selectOrGroup = selectOrGroup;
			if (continuation != null)
			{
				base.AdjustFlagsAndWidth(continuation);
				this.continuation = continuation;
			}
		}

		// Token: 0x060054D8 RID: 21720 RVA: 0x00150F94 File Offset: 0x0014F194
		internal QueryBodySyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode clauses, SelectOrGroupClauseSyntax selectOrGroup, [global::System.Runtime.CompilerServices.Nullable(2)] QueryContinuationSyntax continuation, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			if (clauses != null)
			{
				base.AdjustFlagsAndWidth(clauses);
				this.clauses = clauses;
			}
			base.AdjustFlagsAndWidth(selectOrGroup);
			this.selectOrGroup = selectOrGroup;
			if (continuation != null)
			{
				base.AdjustFlagsAndWidth(continuation);
				this.continuation = continuation;
			}
		}

		// Token: 0x060054D9 RID: 21721 RVA: 0x00150FEC File Offset: 0x0014F1EC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal QueryBodySyntax(SyntaxKind kind, GreenNode clauses, [global::System.Runtime.CompilerServices.Nullable(1)] SelectOrGroupClauseSyntax selectOrGroup, QueryContinuationSyntax continuation)
			: base(kind)
		{
			base.SlotCount = 3;
			if (clauses != null)
			{
				base.AdjustFlagsAndWidth(clauses);
				this.clauses = clauses;
			}
			base.AdjustFlagsAndWidth(selectOrGroup);
			this.selectOrGroup = selectOrGroup;
			if (continuation != null)
			{
				base.AdjustFlagsAndWidth(continuation);
				this.continuation = continuation;
			}
		}

		// Token: 0x17001387 RID: 4999
		// (get) Token: 0x060054DA RID: 21722 RVA: 0x0015103A File Offset: 0x0014F23A
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<QueryClauseSyntax> Clauses
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<QueryClauseSyntax>(this.clauses);
			}
		}

		// Token: 0x17001388 RID: 5000
		// (get) Token: 0x060054DB RID: 21723 RVA: 0x00151047 File Offset: 0x0014F247
		public SelectOrGroupClauseSyntax SelectOrGroup
		{
			get
			{
				return this.selectOrGroup;
			}
		}

		// Token: 0x17001389 RID: 5001
		// (get) Token: 0x060054DC RID: 21724 RVA: 0x0015104F File Offset: 0x0014F24F
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public QueryContinuationSyntax Continuation
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.continuation;
			}
		}

		// Token: 0x060054DD RID: 21725 RVA: 0x00151058 File Offset: 0x0014F258
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.clauses;
				break;
			case 1:
				greenNode = this.selectOrGroup;
				break;
			case 2:
				greenNode = this.continuation;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x060054DE RID: 21726 RVA: 0x00151097 File Offset: 0x0014F297
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new QueryBodySyntax(this, parent, position);
		}

		// Token: 0x060054DF RID: 21727 RVA: 0x001510A1 File Offset: 0x0014F2A1
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitQueryBody(this);
		}

		// Token: 0x060054E0 RID: 21728 RVA: 0x001510AA File Offset: 0x0014F2AA
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitQueryBody(this);
		}

		// Token: 0x060054E1 RID: 21729 RVA: 0x001510B4 File Offset: 0x0014F2B4
		public QueryBodySyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<QueryClauseSyntax> clauses, SelectOrGroupClauseSyntax selectOrGroup, QueryContinuationSyntax continuation)
		{
			if (clauses != this.Clauses || selectOrGroup != this.SelectOrGroup || continuation != this.Continuation)
			{
				QueryBodySyntax queryBodySyntax = SyntaxFactory.QueryBody(clauses, selectOrGroup, continuation);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					queryBodySyntax = queryBodySyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					queryBodySyntax = queryBodySyntax.WithAnnotationsGreen(annotations);
				}
				return queryBodySyntax;
			}
			return this;
		}

		// Token: 0x060054E2 RID: 21730 RVA: 0x00151119 File Offset: 0x0014F319
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new QueryBodySyntax(base.Kind, this.clauses, this.selectOrGroup, this.continuation, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060054E3 RID: 21731 RVA: 0x0015113F File Offset: 0x0014F33F
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new QueryBodySyntax(base.Kind, this.clauses, this.selectOrGroup, this.continuation, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060054E4 RID: 21732 RVA: 0x00151168 File Offset: 0x0014F368
		internal QueryBodySyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.clauses = greenNode;
			}
			SelectOrGroupClauseSyntax selectOrGroupClauseSyntax = (SelectOrGroupClauseSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(selectOrGroupClauseSyntax);
			this.selectOrGroup = selectOrGroupClauseSyntax;
			QueryContinuationSyntax queryContinuationSyntax = (QueryContinuationSyntax)reader.ReadValue();
			if (queryContinuationSyntax != null)
			{
				base.AdjustFlagsAndWidth(queryContinuationSyntax);
				this.continuation = queryContinuationSyntax;
			}
		}

		// Token: 0x060054E5 RID: 21733 RVA: 0x001511D7 File Offset: 0x0014F3D7
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.clauses);
			writer.WriteValue(this.selectOrGroup);
			writer.WriteValue(this.continuation);
		}

		// Token: 0x060054E6 RID: 21734 RVA: 0x00151204 File Offset: 0x0014F404
		static QueryBodySyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(QueryBodySyntax), (ObjectReader r) => new QueryBodySyntax(r));
		}

		// Token: 0x04001697 RID: 5783
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode clauses;

		// Token: 0x04001698 RID: 5784
		internal readonly SelectOrGroupClauseSyntax selectOrGroup;

		// Token: 0x04001699 RID: 5785
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly QueryContinuationSyntax continuation;
	}
}
