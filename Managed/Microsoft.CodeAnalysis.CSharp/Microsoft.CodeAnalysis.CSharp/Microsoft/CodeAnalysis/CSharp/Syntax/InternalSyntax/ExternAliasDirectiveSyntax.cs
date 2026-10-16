using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000417 RID: 1047
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ExternAliasDirectiveSyntax : CSharpSyntaxNode
	{
		// Token: 0x060059FC RID: 23036 RVA: 0x00160544 File Offset: 0x0015E744
		internal ExternAliasDirectiveSyntax(SyntaxKind kind, SyntaxToken externKeyword, SyntaxToken aliasKeyword, SyntaxToken identifier, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(externKeyword);
			this.externKeyword = externKeyword;
			base.AdjustFlagsAndWidth(aliasKeyword);
			this.aliasKeyword = aliasKeyword;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x060059FD RID: 23037 RVA: 0x001605A0 File Offset: 0x0015E7A0
		internal ExternAliasDirectiveSyntax(SyntaxKind kind, SyntaxToken externKeyword, SyntaxToken aliasKeyword, SyntaxToken identifier, SyntaxToken semicolonToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(externKeyword);
			this.externKeyword = externKeyword;
			base.AdjustFlagsAndWidth(aliasKeyword);
			this.aliasKeyword = aliasKeyword;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x060059FE RID: 23038 RVA: 0x00160600 File Offset: 0x0015E800
		internal ExternAliasDirectiveSyntax(SyntaxKind kind, SyntaxToken externKeyword, SyntaxToken aliasKeyword, SyntaxToken identifier, SyntaxToken semicolonToken)
			: base(kind)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(externKeyword);
			this.externKeyword = externKeyword;
			base.AdjustFlagsAndWidth(aliasKeyword);
			this.aliasKeyword = aliasKeyword;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x170014B1 RID: 5297
		// (get) Token: 0x060059FF RID: 23039 RVA: 0x00160657 File Offset: 0x0015E857
		public SyntaxToken ExternKeyword
		{
			get
			{
				return this.externKeyword;
			}
		}

		// Token: 0x170014B2 RID: 5298
		// (get) Token: 0x06005A00 RID: 23040 RVA: 0x0016065F File Offset: 0x0015E85F
		public SyntaxToken AliasKeyword
		{
			get
			{
				return this.aliasKeyword;
			}
		}

		// Token: 0x170014B3 RID: 5299
		// (get) Token: 0x06005A01 RID: 23041 RVA: 0x00160667 File Offset: 0x0015E867
		public SyntaxToken Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x170014B4 RID: 5300
		// (get) Token: 0x06005A02 RID: 23042 RVA: 0x0016066F File Offset: 0x0015E86F
		public SyntaxToken SemicolonToken
		{
			get
			{
				return this.semicolonToken;
			}
		}

		// Token: 0x06005A03 RID: 23043 RVA: 0x00160678 File Offset: 0x0015E878
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			SyntaxToken syntaxToken;
			switch (index)
			{
			case 0:
				syntaxToken = this.externKeyword;
				break;
			case 1:
				syntaxToken = this.aliasKeyword;
				break;
			case 2:
				syntaxToken = this.identifier;
				break;
			case 3:
				syntaxToken = this.semicolonToken;
				break;
			default:
				syntaxToken = null;
				break;
			}
			return syntaxToken;
		}

		// Token: 0x06005A04 RID: 23044 RVA: 0x001606C4 File Offset: 0x0015E8C4
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ExternAliasDirectiveSyntax(this, parent, position);
		}

		// Token: 0x06005A05 RID: 23045 RVA: 0x001606CE File Offset: 0x0015E8CE
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitExternAliasDirective(this);
		}

		// Token: 0x06005A06 RID: 23046 RVA: 0x001606D7 File Offset: 0x0015E8D7
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitExternAliasDirective(this);
		}

		// Token: 0x06005A07 RID: 23047 RVA: 0x001606E0 File Offset: 0x0015E8E0
		public ExternAliasDirectiveSyntax Update(SyntaxToken externKeyword, SyntaxToken aliasKeyword, SyntaxToken identifier, SyntaxToken semicolonToken)
		{
			if (externKeyword != this.ExternKeyword || aliasKeyword != this.AliasKeyword || identifier != this.Identifier || semicolonToken != this.SemicolonToken)
			{
				ExternAliasDirectiveSyntax externAliasDirectiveSyntax = SyntaxFactory.ExternAliasDirective(externKeyword, aliasKeyword, identifier, semicolonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					externAliasDirectiveSyntax = externAliasDirectiveSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					externAliasDirectiveSyntax = externAliasDirectiveSyntax.WithAnnotationsGreen(annotations);
				}
				return externAliasDirectiveSyntax;
			}
			return this;
		}

		// Token: 0x06005A08 RID: 23048 RVA: 0x0016074C File Offset: 0x0015E94C
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ExternAliasDirectiveSyntax(base.Kind, this.externKeyword, this.aliasKeyword, this.identifier, this.semicolonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005A09 RID: 23049 RVA: 0x00160778 File Offset: 0x0015E978
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ExternAliasDirectiveSyntax(base.Kind, this.externKeyword, this.aliasKeyword, this.identifier, this.semicolonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005A0A RID: 23050 RVA: 0x001607A4 File Offset: 0x0015E9A4
		internal ExternAliasDirectiveSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 4;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.externKeyword = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.aliasKeyword = syntaxToken2;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.identifier = syntaxToken3;
			SyntaxToken syntaxToken4 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken4);
			this.semicolonToken = syntaxToken4;
		}

		// Token: 0x06005A0B RID: 23051 RVA: 0x00160827 File Offset: 0x0015EA27
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.externKeyword);
			writer.WriteValue(this.aliasKeyword);
			writer.WriteValue(this.identifier);
			writer.WriteValue(this.semicolonToken);
		}

		// Token: 0x06005A0C RID: 23052 RVA: 0x00160860 File Offset: 0x0015EA60
		static ExternAliasDirectiveSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ExternAliasDirectiveSyntax), (ObjectReader r) => new ExternAliasDirectiveSyntax(r));
		}

		// Token: 0x040017B7 RID: 6071
		internal readonly SyntaxToken externKeyword;

		// Token: 0x040017B8 RID: 6072
		internal readonly SyntaxToken aliasKeyword;

		// Token: 0x040017B9 RID: 6073
		internal readonly SyntaxToken identifier;

		// Token: 0x040017BA RID: 6074
		internal readonly SyntaxToken semicolonToken;
	}
}
