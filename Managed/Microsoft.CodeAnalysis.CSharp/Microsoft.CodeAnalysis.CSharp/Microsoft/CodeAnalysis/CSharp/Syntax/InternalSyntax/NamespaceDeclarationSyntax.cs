using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200041A RID: 1050
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class NamespaceDeclarationSyntax : MemberDeclarationSyntax
	{
		// Token: 0x06005A25 RID: 23077 RVA: 0x00160D40 File Offset: 0x0015EF40
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal NamespaceDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken namespaceKeyword, [global::System.Runtime.CompilerServices.Nullable(1)] NameSyntax name, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken openBraceToken, GreenNode externs, GreenNode usings, GreenNode members, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken closeBraceToken, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 10;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			if (modifiers != null)
			{
				base.AdjustFlagsAndWidth(modifiers);
				this.modifiers = modifiers;
			}
			base.AdjustFlagsAndWidth(namespaceKeyword);
			this.namespaceKeyword = namespaceKeyword;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			base.AdjustFlagsAndWidth(openBraceToken);
			this.openBraceToken = openBraceToken;
			if (externs != null)
			{
				base.AdjustFlagsAndWidth(externs);
				this.externs = externs;
			}
			if (usings != null)
			{
				base.AdjustFlagsAndWidth(usings);
				this.usings = usings;
			}
			if (members != null)
			{
				base.AdjustFlagsAndWidth(members);
				this.members = members;
			}
			base.AdjustFlagsAndWidth(closeBraceToken);
			this.closeBraceToken = closeBraceToken;
			if (semicolonToken != null)
			{
				base.AdjustFlagsAndWidth(semicolonToken);
				this.semicolonToken = semicolonToken;
			}
		}

		// Token: 0x06005A26 RID: 23078 RVA: 0x00160E14 File Offset: 0x0015F014
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal NamespaceDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken namespaceKeyword, [global::System.Runtime.CompilerServices.Nullable(1)] NameSyntax name, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken openBraceToken, GreenNode externs, GreenNode usings, GreenNode members, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken closeBraceToken, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 10;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			if (modifiers != null)
			{
				base.AdjustFlagsAndWidth(modifiers);
				this.modifiers = modifiers;
			}
			base.AdjustFlagsAndWidth(namespaceKeyword);
			this.namespaceKeyword = namespaceKeyword;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			base.AdjustFlagsAndWidth(openBraceToken);
			this.openBraceToken = openBraceToken;
			if (externs != null)
			{
				base.AdjustFlagsAndWidth(externs);
				this.externs = externs;
			}
			if (usings != null)
			{
				base.AdjustFlagsAndWidth(usings);
				this.usings = usings;
			}
			if (members != null)
			{
				base.AdjustFlagsAndWidth(members);
				this.members = members;
			}
			base.AdjustFlagsAndWidth(closeBraceToken);
			this.closeBraceToken = closeBraceToken;
			if (semicolonToken != null)
			{
				base.AdjustFlagsAndWidth(semicolonToken);
				this.semicolonToken = semicolonToken;
			}
		}

		// Token: 0x06005A27 RID: 23079 RVA: 0x00160EEC File Offset: 0x0015F0EC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal NamespaceDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken namespaceKeyword, [global::System.Runtime.CompilerServices.Nullable(1)] NameSyntax name, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken openBraceToken, GreenNode externs, GreenNode usings, GreenNode members, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
			: base(kind)
		{
			base.SlotCount = 10;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			if (modifiers != null)
			{
				base.AdjustFlagsAndWidth(modifiers);
				this.modifiers = modifiers;
			}
			base.AdjustFlagsAndWidth(namespaceKeyword);
			this.namespaceKeyword = namespaceKeyword;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			base.AdjustFlagsAndWidth(openBraceToken);
			this.openBraceToken = openBraceToken;
			if (externs != null)
			{
				base.AdjustFlagsAndWidth(externs);
				this.externs = externs;
			}
			if (usings != null)
			{
				base.AdjustFlagsAndWidth(usings);
				this.usings = usings;
			}
			if (members != null)
			{
				base.AdjustFlagsAndWidth(members);
				this.members = members;
			}
			base.AdjustFlagsAndWidth(closeBraceToken);
			this.closeBraceToken = closeBraceToken;
			if (semicolonToken != null)
			{
				base.AdjustFlagsAndWidth(semicolonToken);
				this.semicolonToken = semicolonToken;
			}
		}

		// Token: 0x170014BD RID: 5309
		// (get) Token: 0x06005A28 RID: 23080 RVA: 0x00160FBA File Offset: 0x0015F1BA
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x170014BE RID: 5310
		// (get) Token: 0x06005A29 RID: 23081 RVA: 0x00160FC7 File Offset: 0x0015F1C7
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<SyntaxToken> Modifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SyntaxToken>(this.modifiers);
			}
		}

		// Token: 0x170014BF RID: 5311
		// (get) Token: 0x06005A2A RID: 23082 RVA: 0x00160FD4 File Offset: 0x0015F1D4
		public SyntaxToken NamespaceKeyword
		{
			get
			{
				return this.namespaceKeyword;
			}
		}

		// Token: 0x170014C0 RID: 5312
		// (get) Token: 0x06005A2B RID: 23083 RVA: 0x00160FDC File Offset: 0x0015F1DC
		public NameSyntax Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170014C1 RID: 5313
		// (get) Token: 0x06005A2C RID: 23084 RVA: 0x00160FE4 File Offset: 0x0015F1E4
		public SyntaxToken OpenBraceToken
		{
			get
			{
				return this.openBraceToken;
			}
		}

		// Token: 0x170014C2 RID: 5314
		// (get) Token: 0x06005A2D RID: 23085 RVA: 0x00160FEC File Offset: 0x0015F1EC
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<ExternAliasDirectiveSyntax> Externs
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<ExternAliasDirectiveSyntax>(this.externs);
			}
		}

		// Token: 0x170014C3 RID: 5315
		// (get) Token: 0x06005A2E RID: 23086 RVA: 0x00160FF9 File Offset: 0x0015F1F9
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<UsingDirectiveSyntax> Usings
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<UsingDirectiveSyntax>(this.usings);
			}
		}

		// Token: 0x170014C4 RID: 5316
		// (get) Token: 0x06005A2F RID: 23087 RVA: 0x00161006 File Offset: 0x0015F206
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<MemberDeclarationSyntax> Members
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<MemberDeclarationSyntax>(this.members);
			}
		}

		// Token: 0x170014C5 RID: 5317
		// (get) Token: 0x06005A30 RID: 23088 RVA: 0x00161013 File Offset: 0x0015F213
		public SyntaxToken CloseBraceToken
		{
			get
			{
				return this.closeBraceToken;
			}
		}

		// Token: 0x170014C6 RID: 5318
		// (get) Token: 0x06005A31 RID: 23089 RVA: 0x0016101B File Offset: 0x0015F21B
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public SyntaxToken SemicolonToken
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.semicolonToken;
			}
		}

		// Token: 0x06005A32 RID: 23090 RVA: 0x00161024 File Offset: 0x0015F224
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.attributeLists;
				break;
			case 1:
				greenNode = this.modifiers;
				break;
			case 2:
				greenNode = this.namespaceKeyword;
				break;
			case 3:
				greenNode = this.name;
				break;
			case 4:
				greenNode = this.openBraceToken;
				break;
			case 5:
				greenNode = this.externs;
				break;
			case 6:
				greenNode = this.usings;
				break;
			case 7:
				greenNode = this.members;
				break;
			case 8:
				greenNode = this.closeBraceToken;
				break;
			case 9:
				greenNode = this.semicolonToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005A33 RID: 23091 RVA: 0x001610BE File Offset: 0x0015F2BE
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new NamespaceDeclarationSyntax(this, parent, position);
		}

		// Token: 0x06005A34 RID: 23092 RVA: 0x001610C8 File Offset: 0x0015F2C8
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitNamespaceDeclaration(this);
		}

		// Token: 0x06005A35 RID: 23093 RVA: 0x001610D1 File Offset: 0x0015F2D1
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitNamespaceDeclaration(this);
		}

		// Token: 0x06005A36 RID: 23094 RVA: 0x001610DC File Offset: 0x0015F2DC
		public NamespaceDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken namespaceKeyword, NameSyntax name, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<ExternAliasDirectiveSyntax> externs, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<UsingDirectiveSyntax> usings, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
		{
			if (attributeLists != this.AttributeLists || modifiers != this.Modifiers || namespaceKeyword != this.NamespaceKeyword || name != this.Name || openBraceToken != this.OpenBraceToken || externs != this.Externs || usings != this.Usings || members != this.Members || closeBraceToken != this.CloseBraceToken || semicolonToken != this.SemicolonToken)
			{
				NamespaceDeclarationSyntax namespaceDeclarationSyntax = SyntaxFactory.NamespaceDeclaration(attributeLists, modifiers, namespaceKeyword, name, openBraceToken, externs, usings, members, closeBraceToken, semicolonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					namespaceDeclarationSyntax = namespaceDeclarationSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					namespaceDeclarationSyntax = namespaceDeclarationSyntax.WithAnnotationsGreen(annotations);
				}
				return namespaceDeclarationSyntax;
			}
			return this;
		}

		// Token: 0x06005A37 RID: 23095 RVA: 0x001611AC File Offset: 0x0015F3AC
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new NamespaceDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.namespaceKeyword, this.name, this.openBraceToken, this.externs, this.usings, this.members, this.closeBraceToken, this.semicolonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005A38 RID: 23096 RVA: 0x00161208 File Offset: 0x0015F408
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new NamespaceDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.namespaceKeyword, this.name, this.openBraceToken, this.externs, this.usings, this.members, this.closeBraceToken, this.semicolonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005A39 RID: 23097 RVA: 0x00161264 File Offset: 0x0015F464
		internal NamespaceDeclarationSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 10;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.attributeLists = greenNode;
			}
			GreenNode greenNode2 = (GreenNode)reader.ReadValue();
			if (greenNode2 != null)
			{
				base.AdjustFlagsAndWidth(greenNode2);
				this.modifiers = greenNode2;
			}
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.namespaceKeyword = syntaxToken;
			NameSyntax nameSyntax = (NameSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(nameSyntax);
			this.name = nameSyntax;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.openBraceToken = syntaxToken2;
			GreenNode greenNode3 = (GreenNode)reader.ReadValue();
			if (greenNode3 != null)
			{
				base.AdjustFlagsAndWidth(greenNode3);
				this.externs = greenNode3;
			}
			GreenNode greenNode4 = (GreenNode)reader.ReadValue();
			if (greenNode4 != null)
			{
				base.AdjustFlagsAndWidth(greenNode4);
				this.usings = greenNode4;
			}
			GreenNode greenNode5 = (GreenNode)reader.ReadValue();
			if (greenNode5 != null)
			{
				base.AdjustFlagsAndWidth(greenNode5);
				this.members = greenNode5;
			}
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.closeBraceToken = syntaxToken3;
			SyntaxToken syntaxToken4 = (SyntaxToken)reader.ReadValue();
			if (syntaxToken4 != null)
			{
				base.AdjustFlagsAndWidth(syntaxToken4);
				this.semicolonToken = syntaxToken4;
			}
		}

		// Token: 0x06005A3A RID: 23098 RVA: 0x001613AC File Offset: 0x0015F5AC
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.modifiers);
			writer.WriteValue(this.namespaceKeyword);
			writer.WriteValue(this.name);
			writer.WriteValue(this.openBraceToken);
			writer.WriteValue(this.externs);
			writer.WriteValue(this.usings);
			writer.WriteValue(this.members);
			writer.WriteValue(this.closeBraceToken);
			writer.WriteValue(this.semicolonToken);
		}

		// Token: 0x06005A3B RID: 23099 RVA: 0x00161438 File Offset: 0x0015F638
		static NamespaceDeclarationSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(NamespaceDeclarationSyntax), (ObjectReader r) => new NamespaceDeclarationSyntax(r));
		}

		// Token: 0x040017C1 RID: 6081
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x040017C2 RID: 6082
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode modifiers;

		// Token: 0x040017C3 RID: 6083
		internal readonly SyntaxToken namespaceKeyword;

		// Token: 0x040017C4 RID: 6084
		internal readonly NameSyntax name;

		// Token: 0x040017C5 RID: 6085
		internal readonly SyntaxToken openBraceToken;

		// Token: 0x040017C6 RID: 6086
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode externs;

		// Token: 0x040017C7 RID: 6087
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode usings;

		// Token: 0x040017C8 RID: 6088
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode members;

		// Token: 0x040017C9 RID: 6089
		internal readonly SyntaxToken closeBraceToken;

		// Token: 0x040017CA RID: 6090
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly SyntaxToken semicolonToken;
	}
}
