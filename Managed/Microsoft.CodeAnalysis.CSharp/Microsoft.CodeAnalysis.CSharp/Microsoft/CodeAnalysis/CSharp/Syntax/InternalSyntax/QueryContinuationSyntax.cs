using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003CE RID: 974
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class QueryContinuationSyntax : CSharpSyntaxNode
	{
		// Token: 0x0600557D RID: 21885 RVA: 0x00152DC9 File Offset: 0x00150FC9
		internal QueryContinuationSyntax(SyntaxKind kind, SyntaxToken intoKeyword, SyntaxToken identifier, QueryBodySyntax body, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(intoKeyword);
			this.intoKeyword = intoKeyword;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(body);
			this.body = body;
		}

		// Token: 0x0600557E RID: 21886 RVA: 0x00152E0C File Offset: 0x0015100C
		internal QueryContinuationSyntax(SyntaxKind kind, SyntaxToken intoKeyword, SyntaxToken identifier, QueryBodySyntax body, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(intoKeyword);
			this.intoKeyword = intoKeyword;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(body);
			this.body = body;
		}

		// Token: 0x0600557F RID: 21887 RVA: 0x00152E5B File Offset: 0x0015105B
		internal QueryContinuationSyntax(SyntaxKind kind, SyntaxToken intoKeyword, SyntaxToken identifier, QueryBodySyntax body)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(intoKeyword);
			this.intoKeyword = intoKeyword;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(body);
			this.body = body;
		}

		// Token: 0x170013AB RID: 5035
		// (get) Token: 0x06005580 RID: 21888 RVA: 0x00152E97 File Offset: 0x00151097
		public SyntaxToken IntoKeyword
		{
			get
			{
				return this.intoKeyword;
			}
		}

		// Token: 0x170013AC RID: 5036
		// (get) Token: 0x06005581 RID: 21889 RVA: 0x00152E9F File Offset: 0x0015109F
		public SyntaxToken Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x170013AD RID: 5037
		// (get) Token: 0x06005582 RID: 21890 RVA: 0x00152EA7 File Offset: 0x001510A7
		public QueryBodySyntax Body
		{
			get
			{
				return this.body;
			}
		}

		// Token: 0x06005583 RID: 21891 RVA: 0x00152EB0 File Offset: 0x001510B0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.intoKeyword;
				break;
			case 1:
				greenNode = this.identifier;
				break;
			case 2:
				greenNode = this.body;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005584 RID: 21892 RVA: 0x00152EEF File Offset: 0x001510EF
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new QueryContinuationSyntax(this, parent, position);
		}

		// Token: 0x06005585 RID: 21893 RVA: 0x00152EF9 File Offset: 0x001510F9
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitQueryContinuation(this);
		}

		// Token: 0x06005586 RID: 21894 RVA: 0x00152F02 File Offset: 0x00151102
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitQueryContinuation(this);
		}

		// Token: 0x06005587 RID: 21895 RVA: 0x00152F0C File Offset: 0x0015110C
		public QueryContinuationSyntax Update(SyntaxToken intoKeyword, SyntaxToken identifier, QueryBodySyntax body)
		{
			if (intoKeyword != this.IntoKeyword || identifier != this.Identifier || body != this.Body)
			{
				QueryContinuationSyntax queryContinuationSyntax = SyntaxFactory.QueryContinuation(intoKeyword, identifier, body);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					queryContinuationSyntax = queryContinuationSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					queryContinuationSyntax = queryContinuationSyntax.WithAnnotationsGreen(annotations);
				}
				return queryContinuationSyntax;
			}
			return this;
		}

		// Token: 0x06005588 RID: 21896 RVA: 0x00152F6C File Offset: 0x0015116C
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new QueryContinuationSyntax(base.Kind, this.intoKeyword, this.identifier, this.body, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005589 RID: 21897 RVA: 0x00152F92 File Offset: 0x00151192
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new QueryContinuationSyntax(base.Kind, this.intoKeyword, this.identifier, this.body, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600558A RID: 21898 RVA: 0x00152FB8 File Offset: 0x001511B8
		internal QueryContinuationSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.intoKeyword = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.identifier = syntaxToken2;
			QueryBodySyntax queryBodySyntax = (QueryBodySyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(queryBodySyntax);
			this.body = queryBodySyntax;
		}

		// Token: 0x0600558B RID: 21899 RVA: 0x00153021 File Offset: 0x00151221
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.intoKeyword);
			writer.WriteValue(this.identifier);
			writer.WriteValue(this.body);
		}

		// Token: 0x0600558C RID: 21900 RVA: 0x0015304E File Offset: 0x0015124E
		static QueryContinuationSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(QueryContinuationSyntax), (ObjectReader r) => new QueryContinuationSyntax(r));
		}

		// Token: 0x040016BB RID: 5819
		internal readonly SyntaxToken intoKeyword;

		// Token: 0x040016BC RID: 5820
		internal readonly SyntaxToken identifier;

		// Token: 0x040016BD RID: 5821
		internal readonly QueryBodySyntax body;
	}
}
