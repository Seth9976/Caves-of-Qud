using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000456 RID: 1110
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class IndexerMemberCrefSyntax : MemberCrefSyntax
	{
		// Token: 0x06005DC8 RID: 24008 RVA: 0x0016CF3C File Offset: 0x0016B13C
		internal IndexerMemberCrefSyntax(SyntaxKind kind, SyntaxToken thisKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] CrefBracketedParameterListSyntax parameters, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(thisKeyword);
			this.thisKeyword = thisKeyword;
			if (parameters != null)
			{
				base.AdjustFlagsAndWidth(parameters);
				this.parameters = parameters;
			}
		}

		// Token: 0x06005DC9 RID: 24009 RVA: 0x0016CF6F File Offset: 0x0016B16F
		internal IndexerMemberCrefSyntax(SyntaxKind kind, SyntaxToken thisKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] CrefBracketedParameterListSyntax parameters, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(thisKeyword);
			this.thisKeyword = thisKeyword;
			if (parameters != null)
			{
				base.AdjustFlagsAndWidth(parameters);
				this.parameters = parameters;
			}
		}

		// Token: 0x06005DCA RID: 24010 RVA: 0x0016CFA6 File Offset: 0x0016B1A6
		internal IndexerMemberCrefSyntax(SyntaxKind kind, SyntaxToken thisKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] CrefBracketedParameterListSyntax parameters)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(thisKeyword);
			this.thisKeyword = thisKeyword;
			if (parameters != null)
			{
				base.AdjustFlagsAndWidth(parameters);
				this.parameters = parameters;
			}
		}

		// Token: 0x170015C2 RID: 5570
		// (get) Token: 0x06005DCB RID: 24011 RVA: 0x0016CFD5 File Offset: 0x0016B1D5
		public SyntaxToken ThisKeyword
		{
			get
			{
				return this.thisKeyword;
			}
		}

		// Token: 0x170015C3 RID: 5571
		// (get) Token: 0x06005DCC RID: 24012 RVA: 0x0016CFDD File Offset: 0x0016B1DD
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public CrefBracketedParameterListSyntax Parameters
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x06005DCD RID: 24013 RVA: 0x0016CFE8 File Offset: 0x0016B1E8
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
					greenNode = this.parameters;
				}
			}
			else
			{
				greenNode = this.thisKeyword;
			}
			return greenNode;
		}

		// Token: 0x06005DCE RID: 24014 RVA: 0x0016D013 File Offset: 0x0016B213
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new IndexerMemberCrefSyntax(this, parent, position);
		}

		// Token: 0x06005DCF RID: 24015 RVA: 0x0016D01D File Offset: 0x0016B21D
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitIndexerMemberCref(this);
		}

		// Token: 0x06005DD0 RID: 24016 RVA: 0x0016D026 File Offset: 0x0016B226
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitIndexerMemberCref(this);
		}

		// Token: 0x06005DD1 RID: 24017 RVA: 0x0016D030 File Offset: 0x0016B230
		public IndexerMemberCrefSyntax Update(SyntaxToken thisKeyword, CrefBracketedParameterListSyntax parameters)
		{
			if (thisKeyword != this.ThisKeyword || parameters != this.Parameters)
			{
				IndexerMemberCrefSyntax indexerMemberCrefSyntax = SyntaxFactory.IndexerMemberCref(thisKeyword, parameters);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					indexerMemberCrefSyntax = indexerMemberCrefSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					indexerMemberCrefSyntax = indexerMemberCrefSyntax.WithAnnotationsGreen(annotations);
				}
				return indexerMemberCrefSyntax;
			}
			return this;
		}

		// Token: 0x06005DD2 RID: 24018 RVA: 0x0016D086 File Offset: 0x0016B286
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new IndexerMemberCrefSyntax(base.Kind, this.thisKeyword, this.parameters, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005DD3 RID: 24019 RVA: 0x0016D0A6 File Offset: 0x0016B2A6
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new IndexerMemberCrefSyntax(base.Kind, this.thisKeyword, this.parameters, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005DD4 RID: 24020 RVA: 0x0016D0C8 File Offset: 0x0016B2C8
		internal IndexerMemberCrefSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.thisKeyword = syntaxToken;
			CrefBracketedParameterListSyntax crefBracketedParameterListSyntax = (CrefBracketedParameterListSyntax)reader.ReadValue();
			if (crefBracketedParameterListSyntax != null)
			{
				base.AdjustFlagsAndWidth(crefBracketedParameterListSyntax);
				this.parameters = crefBracketedParameterListSyntax;
			}
		}

		// Token: 0x06005DD5 RID: 24021 RVA: 0x0016D11A File Offset: 0x0016B31A
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.thisKeyword);
			writer.WriteValue(this.parameters);
		}

		// Token: 0x06005DD6 RID: 24022 RVA: 0x0016D13B File Offset: 0x0016B33B
		static IndexerMemberCrefSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(IndexerMemberCrefSyntax), (ObjectReader r) => new IndexerMemberCrefSyntax(r));
		}

		// Token: 0x040018AF RID: 6319
		internal readonly SyntaxToken thisKeyword;

		// Token: 0x040018B0 RID: 6320
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly CrefBracketedParameterListSyntax parameters;
	}
}
