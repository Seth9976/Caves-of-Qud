using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000429 RID: 1065
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class EnumDeclarationSyntax : BaseTypeDeclarationSyntax
	{
		// Token: 0x06005B2B RID: 23339 RVA: 0x001649CC File Offset: 0x00162BCC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal EnumDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken enumKeyword, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, BaseListSyntax baseList, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken openBraceToken, GreenNode members, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken closeBraceToken, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 9;
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
			base.AdjustFlagsAndWidth(enumKeyword);
			this.enumKeyword = enumKeyword;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			if (baseList != null)
			{
				base.AdjustFlagsAndWidth(baseList);
				this.baseList = baseList;
			}
			base.AdjustFlagsAndWidth(openBraceToken);
			this.openBraceToken = openBraceToken;
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

		// Token: 0x06005B2C RID: 23340 RVA: 0x00164A8C File Offset: 0x00162C8C
		internal EnumDeclarationSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode modifiers, SyntaxToken enumKeyword, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] BaseListSyntax baseList, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode members, SyntaxToken closeBraceToken, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken semicolonToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 9;
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
			base.AdjustFlagsAndWidth(enumKeyword);
			this.enumKeyword = enumKeyword;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			if (baseList != null)
			{
				base.AdjustFlagsAndWidth(baseList);
				this.baseList = baseList;
			}
			base.AdjustFlagsAndWidth(openBraceToken);
			this.openBraceToken = openBraceToken;
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

		// Token: 0x06005B2D RID: 23341 RVA: 0x00164B50 File Offset: 0x00162D50
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal EnumDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken enumKeyword, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, BaseListSyntax baseList, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken openBraceToken, GreenNode members, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
			: base(kind)
		{
			base.SlotCount = 9;
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
			base.AdjustFlagsAndWidth(enumKeyword);
			this.enumKeyword = enumKeyword;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			if (baseList != null)
			{
				base.AdjustFlagsAndWidth(baseList);
				this.baseList = baseList;
			}
			base.AdjustFlagsAndWidth(openBraceToken);
			this.openBraceToken = openBraceToken;
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

		// Token: 0x17001514 RID: 5396
		// (get) Token: 0x06005B2E RID: 23342 RVA: 0x00164C0A File Offset: 0x00162E0A
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x17001515 RID: 5397
		// (get) Token: 0x06005B2F RID: 23343 RVA: 0x00164C17 File Offset: 0x00162E17
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<SyntaxToken> Modifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SyntaxToken>(this.modifiers);
			}
		}

		// Token: 0x17001516 RID: 5398
		// (get) Token: 0x06005B30 RID: 23344 RVA: 0x00164C24 File Offset: 0x00162E24
		public SyntaxToken EnumKeyword
		{
			get
			{
				return this.enumKeyword;
			}
		}

		// Token: 0x17001517 RID: 5399
		// (get) Token: 0x06005B31 RID: 23345 RVA: 0x00164C2C File Offset: 0x00162E2C
		public override SyntaxToken Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x17001518 RID: 5400
		// (get) Token: 0x06005B32 RID: 23346 RVA: 0x00164C34 File Offset: 0x00162E34
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override BaseListSyntax BaseList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.baseList;
			}
		}

		// Token: 0x17001519 RID: 5401
		// (get) Token: 0x06005B33 RID: 23347 RVA: 0x00164C3C File Offset: 0x00162E3C
		public override SyntaxToken OpenBraceToken
		{
			get
			{
				return this.openBraceToken;
			}
		}

		// Token: 0x1700151A RID: 5402
		// (get) Token: 0x06005B34 RID: 23348 RVA: 0x00164C44 File Offset: 0x00162E44
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<EnumMemberDeclarationSyntax> Members
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SeparatedSyntaxList<EnumMemberDeclarationSyntax>(new SyntaxList<CSharpSyntaxNode>(this.members));
			}
		}

		// Token: 0x1700151B RID: 5403
		// (get) Token: 0x06005B35 RID: 23349 RVA: 0x00164C5B File Offset: 0x00162E5B
		public override SyntaxToken CloseBraceToken
		{
			get
			{
				return this.closeBraceToken;
			}
		}

		// Token: 0x1700151C RID: 5404
		// (get) Token: 0x06005B36 RID: 23350 RVA: 0x00164C63 File Offset: 0x00162E63
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxToken SemicolonToken
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.semicolonToken;
			}
		}

		// Token: 0x06005B37 RID: 23351 RVA: 0x00164C6C File Offset: 0x00162E6C
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
				greenNode = this.enumKeyword;
				break;
			case 3:
				greenNode = this.identifier;
				break;
			case 4:
				greenNode = this.baseList;
				break;
			case 5:
				greenNode = this.openBraceToken;
				break;
			case 6:
				greenNode = this.members;
				break;
			case 7:
				greenNode = this.closeBraceToken;
				break;
			case 8:
				greenNode = this.semicolonToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005B38 RID: 23352 RVA: 0x00164CF9 File Offset: 0x00162EF9
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new EnumDeclarationSyntax(this, parent, position);
		}

		// Token: 0x06005B39 RID: 23353 RVA: 0x00164D03 File Offset: 0x00162F03
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitEnumDeclaration(this);
		}

		// Token: 0x06005B3A RID: 23354 RVA: 0x00164D0C File Offset: 0x00162F0C
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitEnumDeclaration(this);
		}

		// Token: 0x06005B3B RID: 23355 RVA: 0x00164D18 File Offset: 0x00162F18
		public EnumDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken enumKeyword, SyntaxToken identifier, BaseListSyntax baseList, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<EnumMemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
		{
			if (!(attributeLists != this.AttributeLists) && !(modifiers != this.Modifiers) && enumKeyword == this.EnumKeyword && identifier == this.Identifier && baseList == this.BaseList && openBraceToken == this.OpenBraceToken)
			{
				SeparatedSyntaxList<EnumMemberDeclarationSyntax> separatedSyntaxList = this.Members;
				if (!((in members) != (in separatedSyntaxList)) && closeBraceToken == this.CloseBraceToken && semicolonToken == this.SemicolonToken)
				{
					return this;
				}
			}
			EnumDeclarationSyntax enumDeclarationSyntax = SyntaxFactory.EnumDeclaration(attributeLists, modifiers, enumKeyword, identifier, baseList, openBraceToken, members, closeBraceToken, semicolonToken);
			DiagnosticInfo[] diagnostics = base.GetDiagnostics();
			if (diagnostics != null && diagnostics.Length != 0)
			{
				enumDeclarationSyntax = enumDeclarationSyntax.WithDiagnosticsGreen(diagnostics);
			}
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations != null && annotations.Length != 0)
			{
				enumDeclarationSyntax = enumDeclarationSyntax.WithAnnotationsGreen(annotations);
			}
			return enumDeclarationSyntax;
		}

		// Token: 0x06005B3C RID: 23356 RVA: 0x00164DD4 File Offset: 0x00162FD4
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new EnumDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.enumKeyword, this.identifier, this.baseList, this.openBraceToken, this.members, this.closeBraceToken, this.semicolonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005B3D RID: 23357 RVA: 0x00164E2C File Offset: 0x0016302C
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new EnumDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.enumKeyword, this.identifier, this.baseList, this.openBraceToken, this.members, this.closeBraceToken, this.semicolonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005B3E RID: 23358 RVA: 0x00164E84 File Offset: 0x00163084
		internal EnumDeclarationSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 9;
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
			this.enumKeyword = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.identifier = syntaxToken2;
			BaseListSyntax baseListSyntax = (BaseListSyntax)reader.ReadValue();
			if (baseListSyntax != null)
			{
				base.AdjustFlagsAndWidth(baseListSyntax);
				this.baseList = baseListSyntax;
			}
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.openBraceToken = syntaxToken3;
			GreenNode greenNode3 = (GreenNode)reader.ReadValue();
			if (greenNode3 != null)
			{
				base.AdjustFlagsAndWidth(greenNode3);
				this.members = greenNode3;
			}
			SyntaxToken syntaxToken4 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken4);
			this.closeBraceToken = syntaxToken4;
			SyntaxToken syntaxToken5 = (SyntaxToken)reader.ReadValue();
			if (syntaxToken5 != null)
			{
				base.AdjustFlagsAndWidth(syntaxToken5);
				this.semicolonToken = syntaxToken5;
			}
		}

		// Token: 0x06005B3F RID: 23359 RVA: 0x00164FAC File Offset: 0x001631AC
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.modifiers);
			writer.WriteValue(this.enumKeyword);
			writer.WriteValue(this.identifier);
			writer.WriteValue(this.baseList);
			writer.WriteValue(this.openBraceToken);
			writer.WriteValue(this.members);
			writer.WriteValue(this.closeBraceToken);
			writer.WriteValue(this.semicolonToken);
		}

		// Token: 0x06005B40 RID: 23360 RVA: 0x0016502C File Offset: 0x0016322C
		static EnumDeclarationSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(EnumDeclarationSyntax), (ObjectReader r) => new EnumDeclarationSyntax(r));
		}

		// Token: 0x0400180F RID: 6159
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x04001810 RID: 6160
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode modifiers;

		// Token: 0x04001811 RID: 6161
		internal readonly SyntaxToken enumKeyword;

		// Token: 0x04001812 RID: 6162
		internal readonly SyntaxToken identifier;

		// Token: 0x04001813 RID: 6163
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly BaseListSyntax baseList;

		// Token: 0x04001814 RID: 6164
		internal readonly SyntaxToken openBraceToken;

		// Token: 0x04001815 RID: 6165
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode members;

		// Token: 0x04001816 RID: 6166
		internal readonly SyntaxToken closeBraceToken;

		// Token: 0x04001817 RID: 6167
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly SyntaxToken semicolonToken;
	}
}
