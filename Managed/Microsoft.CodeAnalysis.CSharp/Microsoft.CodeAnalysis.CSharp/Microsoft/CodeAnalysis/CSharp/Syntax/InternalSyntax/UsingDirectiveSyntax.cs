using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000418 RID: 1048
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class UsingDirectiveSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005A0D RID: 23053 RVA: 0x00160884 File Offset: 0x0015EA84
		internal UsingDirectiveSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken globalKeyword, SyntaxToken usingKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken staticKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] NameEqualsSyntax alias, NameSyntax name, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 6;
			if (globalKeyword != null)
			{
				base.AdjustFlagsAndWidth(globalKeyword);
				this.globalKeyword = globalKeyword;
			}
			base.AdjustFlagsAndWidth(usingKeyword);
			this.usingKeyword = usingKeyword;
			if (staticKeyword != null)
			{
				base.AdjustFlagsAndWidth(staticKeyword);
				this.staticKeyword = staticKeyword;
			}
			if (alias != null)
			{
				base.AdjustFlagsAndWidth(alias);
				this.alias = alias;
			}
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x06005A0E RID: 23054 RVA: 0x0016090C File Offset: 0x0015EB0C
		internal UsingDirectiveSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken globalKeyword, SyntaxToken usingKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken staticKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] NameEqualsSyntax alias, NameSyntax name, SyntaxToken semicolonToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 6;
			if (globalKeyword != null)
			{
				base.AdjustFlagsAndWidth(globalKeyword);
				this.globalKeyword = globalKeyword;
			}
			base.AdjustFlagsAndWidth(usingKeyword);
			this.usingKeyword = usingKeyword;
			if (staticKeyword != null)
			{
				base.AdjustFlagsAndWidth(staticKeyword);
				this.staticKeyword = staticKeyword;
			}
			if (alias != null)
			{
				base.AdjustFlagsAndWidth(alias);
				this.alias = alias;
			}
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x06005A0F RID: 23055 RVA: 0x00160998 File Offset: 0x0015EB98
		internal UsingDirectiveSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken globalKeyword, SyntaxToken usingKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken staticKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] NameEqualsSyntax alias, NameSyntax name, SyntaxToken semicolonToken)
			: base(kind)
		{
			base.SlotCount = 6;
			if (globalKeyword != null)
			{
				base.AdjustFlagsAndWidth(globalKeyword);
				this.globalKeyword = globalKeyword;
			}
			base.AdjustFlagsAndWidth(usingKeyword);
			this.usingKeyword = usingKeyword;
			if (staticKeyword != null)
			{
				base.AdjustFlagsAndWidth(staticKeyword);
				this.staticKeyword = staticKeyword;
			}
			if (alias != null)
			{
				base.AdjustFlagsAndWidth(alias);
				this.alias = alias;
			}
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x170014B5 RID: 5301
		// (get) Token: 0x06005A10 RID: 23056 RVA: 0x00160A1A File Offset: 0x0015EC1A
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public SyntaxToken GlobalKeyword
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.globalKeyword;
			}
		}

		// Token: 0x170014B6 RID: 5302
		// (get) Token: 0x06005A11 RID: 23057 RVA: 0x00160A22 File Offset: 0x0015EC22
		public SyntaxToken UsingKeyword
		{
			get
			{
				return this.usingKeyword;
			}
		}

		// Token: 0x170014B7 RID: 5303
		// (get) Token: 0x06005A12 RID: 23058 RVA: 0x00160A2A File Offset: 0x0015EC2A
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public SyntaxToken StaticKeyword
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.staticKeyword;
			}
		}

		// Token: 0x170014B8 RID: 5304
		// (get) Token: 0x06005A13 RID: 23059 RVA: 0x00160A32 File Offset: 0x0015EC32
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public NameEqualsSyntax Alias
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.alias;
			}
		}

		// Token: 0x170014B9 RID: 5305
		// (get) Token: 0x06005A14 RID: 23060 RVA: 0x00160A3A File Offset: 0x0015EC3A
		public NameSyntax Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170014BA RID: 5306
		// (get) Token: 0x06005A15 RID: 23061 RVA: 0x00160A42 File Offset: 0x0015EC42
		public SyntaxToken SemicolonToken
		{
			get
			{
				return this.semicolonToken;
			}
		}

		// Token: 0x06005A16 RID: 23062 RVA: 0x00160A4C File Offset: 0x0015EC4C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.globalKeyword;
				break;
			case 1:
				greenNode = this.usingKeyword;
				break;
			case 2:
				greenNode = this.staticKeyword;
				break;
			case 3:
				greenNode = this.alias;
				break;
			case 4:
				greenNode = this.name;
				break;
			case 5:
				greenNode = this.semicolonToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005A17 RID: 23063 RVA: 0x00160AB2 File Offset: 0x0015ECB2
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new UsingDirectiveSyntax(this, parent, position);
		}

		// Token: 0x06005A18 RID: 23064 RVA: 0x00160ABC File Offset: 0x0015ECBC
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitUsingDirective(this);
		}

		// Token: 0x06005A19 RID: 23065 RVA: 0x00160AC5 File Offset: 0x0015ECC5
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitUsingDirective(this);
		}

		// Token: 0x06005A1A RID: 23066 RVA: 0x00160AD0 File Offset: 0x0015ECD0
		public UsingDirectiveSyntax Update(SyntaxToken globalKeyword, SyntaxToken usingKeyword, SyntaxToken staticKeyword, NameEqualsSyntax alias, NameSyntax name, SyntaxToken semicolonToken)
		{
			if (globalKeyword != this.GlobalKeyword || usingKeyword != this.UsingKeyword || staticKeyword != this.StaticKeyword || alias != this.Alias || name != this.Name || semicolonToken != this.SemicolonToken)
			{
				UsingDirectiveSyntax usingDirectiveSyntax = SyntaxFactory.UsingDirective(globalKeyword, usingKeyword, staticKeyword, alias, name, semicolonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					usingDirectiveSyntax = usingDirectiveSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					usingDirectiveSyntax = usingDirectiveSyntax.WithAnnotationsGreen(annotations);
				}
				return usingDirectiveSyntax;
			}
			return this;
		}

		// Token: 0x06005A1B RID: 23067 RVA: 0x00160B54 File Offset: 0x0015ED54
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new UsingDirectiveSyntax(base.Kind, this.globalKeyword, this.usingKeyword, this.staticKeyword, this.alias, this.name, this.semicolonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005A1C RID: 23068 RVA: 0x00160B98 File Offset: 0x0015ED98
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new UsingDirectiveSyntax(base.Kind, this.globalKeyword, this.usingKeyword, this.staticKeyword, this.alias, this.name, this.semicolonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005A1D RID: 23069 RVA: 0x00160BDC File Offset: 0x0015EDDC
		internal UsingDirectiveSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 6;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			if (syntaxToken != null)
			{
				base.AdjustFlagsAndWidth(syntaxToken);
				this.globalKeyword = syntaxToken;
			}
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.usingKeyword = syntaxToken2;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			if (syntaxToken3 != null)
			{
				base.AdjustFlagsAndWidth(syntaxToken3);
				this.staticKeyword = syntaxToken3;
			}
			NameEqualsSyntax nameEqualsSyntax = (NameEqualsSyntax)reader.ReadValue();
			if (nameEqualsSyntax != null)
			{
				base.AdjustFlagsAndWidth(nameEqualsSyntax);
				this.alias = nameEqualsSyntax;
			}
			NameSyntax nameSyntax = (NameSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(nameSyntax);
			this.name = nameSyntax;
			SyntaxToken syntaxToken4 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken4);
			this.semicolonToken = syntaxToken4;
		}

		// Token: 0x06005A1E RID: 23070 RVA: 0x00160CA4 File Offset: 0x0015EEA4
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.globalKeyword);
			writer.WriteValue(this.usingKeyword);
			writer.WriteValue(this.staticKeyword);
			writer.WriteValue(this.alias);
			writer.WriteValue(this.name);
			writer.WriteValue(this.semicolonToken);
		}

		// Token: 0x06005A1F RID: 23071 RVA: 0x00160D00 File Offset: 0x0015EF00
		static UsingDirectiveSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(UsingDirectiveSyntax), (ObjectReader r) => new UsingDirectiveSyntax(r));
		}

		// Token: 0x040017BB RID: 6075
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly SyntaxToken globalKeyword;

		// Token: 0x040017BC RID: 6076
		internal readonly SyntaxToken usingKeyword;

		// Token: 0x040017BD RID: 6077
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly SyntaxToken staticKeyword;

		// Token: 0x040017BE RID: 6078
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly NameEqualsSyntax alias;

		// Token: 0x040017BF RID: 6079
		internal readonly NameSyntax name;

		// Token: 0x040017C0 RID: 6080
		internal readonly SyntaxToken semicolonToken;
	}
}
