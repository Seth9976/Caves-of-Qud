using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000427 RID: 1063
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class InterfaceDeclarationSyntax : TypeDeclarationSyntax
	{
		// Token: 0x06005AF9 RID: 23289 RVA: 0x0016391C File Offset: 0x00161B1C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal InterfaceDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken keyword, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, TypeParameterListSyntax typeParameterList, BaseListSyntax baseList, GreenNode constraintClauses, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken openBraceToken, GreenNode members, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken closeBraceToken, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 11;
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
			base.AdjustFlagsAndWidth(keyword);
			this.keyword = keyword;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			if (typeParameterList != null)
			{
				base.AdjustFlagsAndWidth(typeParameterList);
				this.typeParameterList = typeParameterList;
			}
			if (baseList != null)
			{
				base.AdjustFlagsAndWidth(baseList);
				this.baseList = baseList;
			}
			if (constraintClauses != null)
			{
				base.AdjustFlagsAndWidth(constraintClauses);
				this.constraintClauses = constraintClauses;
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

		// Token: 0x06005AFA RID: 23290 RVA: 0x00163A04 File Offset: 0x00161C04
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal InterfaceDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken keyword, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, TypeParameterListSyntax typeParameterList, BaseListSyntax baseList, GreenNode constraintClauses, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken openBraceToken, GreenNode members, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken closeBraceToken, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 11;
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
			base.AdjustFlagsAndWidth(keyword);
			this.keyword = keyword;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			if (typeParameterList != null)
			{
				base.AdjustFlagsAndWidth(typeParameterList);
				this.typeParameterList = typeParameterList;
			}
			if (baseList != null)
			{
				base.AdjustFlagsAndWidth(baseList);
				this.baseList = baseList;
			}
			if (constraintClauses != null)
			{
				base.AdjustFlagsAndWidth(constraintClauses);
				this.constraintClauses = constraintClauses;
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

		// Token: 0x06005AFB RID: 23291 RVA: 0x00163AF0 File Offset: 0x00161CF0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal InterfaceDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken keyword, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, TypeParameterListSyntax typeParameterList, BaseListSyntax baseList, GreenNode constraintClauses, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken openBraceToken, GreenNode members, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
			: base(kind)
		{
			base.SlotCount = 11;
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
			base.AdjustFlagsAndWidth(keyword);
			this.keyword = keyword;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			if (typeParameterList != null)
			{
				base.AdjustFlagsAndWidth(typeParameterList);
				this.typeParameterList = typeParameterList;
			}
			if (baseList != null)
			{
				base.AdjustFlagsAndWidth(baseList);
				this.baseList = baseList;
			}
			if (constraintClauses != null)
			{
				base.AdjustFlagsAndWidth(constraintClauses);
				this.constraintClauses = constraintClauses;
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

		// Token: 0x170014FC RID: 5372
		// (get) Token: 0x06005AFC RID: 23292 RVA: 0x00163BD2 File Offset: 0x00161DD2
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x170014FD RID: 5373
		// (get) Token: 0x06005AFD RID: 23293 RVA: 0x00163BDF File Offset: 0x00161DDF
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<SyntaxToken> Modifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SyntaxToken>(this.modifiers);
			}
		}

		// Token: 0x170014FE RID: 5374
		// (get) Token: 0x06005AFE RID: 23294 RVA: 0x00163BEC File Offset: 0x00161DEC
		public override SyntaxToken Keyword
		{
			get
			{
				return this.keyword;
			}
		}

		// Token: 0x170014FF RID: 5375
		// (get) Token: 0x06005AFF RID: 23295 RVA: 0x00163BF4 File Offset: 0x00161DF4
		public override SyntaxToken Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x17001500 RID: 5376
		// (get) Token: 0x06005B00 RID: 23296 RVA: 0x00163BFC File Offset: 0x00161DFC
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override TypeParameterListSyntax TypeParameterList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.typeParameterList;
			}
		}

		// Token: 0x17001501 RID: 5377
		// (get) Token: 0x06005B01 RID: 23297 RVA: 0x00163C04 File Offset: 0x00161E04
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override BaseListSyntax BaseList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.baseList;
			}
		}

		// Token: 0x17001502 RID: 5378
		// (get) Token: 0x06005B02 RID: 23298 RVA: 0x00163C0C File Offset: 0x00161E0C
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<TypeParameterConstraintClauseSyntax> ConstraintClauses
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<TypeParameterConstraintClauseSyntax>(this.constraintClauses);
			}
		}

		// Token: 0x17001503 RID: 5379
		// (get) Token: 0x06005B03 RID: 23299 RVA: 0x00163C19 File Offset: 0x00161E19
		public override SyntaxToken OpenBraceToken
		{
			get
			{
				return this.openBraceToken;
			}
		}

		// Token: 0x17001504 RID: 5380
		// (get) Token: 0x06005B04 RID: 23300 RVA: 0x00163C21 File Offset: 0x00161E21
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<MemberDeclarationSyntax> Members
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<MemberDeclarationSyntax>(this.members);
			}
		}

		// Token: 0x17001505 RID: 5381
		// (get) Token: 0x06005B05 RID: 23301 RVA: 0x00163C2E File Offset: 0x00161E2E
		public override SyntaxToken CloseBraceToken
		{
			get
			{
				return this.closeBraceToken;
			}
		}

		// Token: 0x17001506 RID: 5382
		// (get) Token: 0x06005B06 RID: 23302 RVA: 0x00163C36 File Offset: 0x00161E36
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxToken SemicolonToken
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.semicolonToken;
			}
		}

		// Token: 0x06005B07 RID: 23303 RVA: 0x00163C40 File Offset: 0x00161E40
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
				greenNode = this.keyword;
				break;
			case 3:
				greenNode = this.identifier;
				break;
			case 4:
				greenNode = this.typeParameterList;
				break;
			case 5:
				greenNode = this.baseList;
				break;
			case 6:
				greenNode = this.constraintClauses;
				break;
			case 7:
				greenNode = this.openBraceToken;
				break;
			case 8:
				greenNode = this.members;
				break;
			case 9:
				greenNode = this.closeBraceToken;
				break;
			case 10:
				greenNode = this.semicolonToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005B08 RID: 23304 RVA: 0x00163CE7 File Offset: 0x00161EE7
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new InterfaceDeclarationSyntax(this, parent, position);
		}

		// Token: 0x06005B09 RID: 23305 RVA: 0x00163CF1 File Offset: 0x00161EF1
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitInterfaceDeclaration(this);
		}

		// Token: 0x06005B0A RID: 23306 RVA: 0x00163CFA File Offset: 0x00161EFA
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitInterfaceDeclaration(this);
		}

		// Token: 0x06005B0B RID: 23307 RVA: 0x00163D04 File Offset: 0x00161F04
		public InterfaceDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken keyword, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, BaseListSyntax baseList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
		{
			if (attributeLists != this.AttributeLists || modifiers != this.Modifiers || keyword != this.Keyword || identifier != this.Identifier || typeParameterList != this.TypeParameterList || baseList != this.BaseList || constraintClauses != this.ConstraintClauses || openBraceToken != this.OpenBraceToken || members != this.Members || closeBraceToken != this.CloseBraceToken || semicolonToken != this.SemicolonToken)
			{
				InterfaceDeclarationSyntax interfaceDeclarationSyntax = SyntaxFactory.InterfaceDeclaration(attributeLists, modifiers, keyword, identifier, typeParameterList, baseList, constraintClauses, openBraceToken, members, closeBraceToken, semicolonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					interfaceDeclarationSyntax = interfaceDeclarationSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					interfaceDeclarationSyntax = interfaceDeclarationSyntax.WithAnnotationsGreen(annotations);
				}
				return interfaceDeclarationSyntax;
			}
			return this;
		}

		// Token: 0x06005B0C RID: 23308 RVA: 0x00163DD8 File Offset: 0x00161FD8
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new InterfaceDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.keyword, this.identifier, this.typeParameterList, this.baseList, this.constraintClauses, this.openBraceToken, this.members, this.closeBraceToken, this.semicolonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005B0D RID: 23309 RVA: 0x00163E3C File Offset: 0x0016203C
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new InterfaceDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.keyword, this.identifier, this.typeParameterList, this.baseList, this.constraintClauses, this.openBraceToken, this.members, this.closeBraceToken, this.semicolonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005B0E RID: 23310 RVA: 0x00163EA0 File Offset: 0x001620A0
		internal InterfaceDeclarationSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 11;
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
			this.keyword = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.identifier = syntaxToken2;
			TypeParameterListSyntax typeParameterListSyntax = (TypeParameterListSyntax)reader.ReadValue();
			if (typeParameterListSyntax != null)
			{
				base.AdjustFlagsAndWidth(typeParameterListSyntax);
				this.typeParameterList = typeParameterListSyntax;
			}
			BaseListSyntax baseListSyntax = (BaseListSyntax)reader.ReadValue();
			if (baseListSyntax != null)
			{
				base.AdjustFlagsAndWidth(baseListSyntax);
				this.baseList = baseListSyntax;
			}
			GreenNode greenNode3 = (GreenNode)reader.ReadValue();
			if (greenNode3 != null)
			{
				base.AdjustFlagsAndWidth(greenNode3);
				this.constraintClauses = greenNode3;
			}
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.openBraceToken = syntaxToken3;
			GreenNode greenNode4 = (GreenNode)reader.ReadValue();
			if (greenNode4 != null)
			{
				base.AdjustFlagsAndWidth(greenNode4);
				this.members = greenNode4;
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

		// Token: 0x06005B0F RID: 23311 RVA: 0x0016400C File Offset: 0x0016220C
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.modifiers);
			writer.WriteValue(this.keyword);
			writer.WriteValue(this.identifier);
			writer.WriteValue(this.typeParameterList);
			writer.WriteValue(this.baseList);
			writer.WriteValue(this.constraintClauses);
			writer.WriteValue(this.openBraceToken);
			writer.WriteValue(this.members);
			writer.WriteValue(this.closeBraceToken);
			writer.WriteValue(this.semicolonToken);
		}

		// Token: 0x06005B10 RID: 23312 RVA: 0x001640A4 File Offset: 0x001622A4
		static InterfaceDeclarationSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(InterfaceDeclarationSyntax), (ObjectReader r) => new InterfaceDeclarationSyntax(r));
		}

		// Token: 0x040017F7 RID: 6135
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x040017F8 RID: 6136
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode modifiers;

		// Token: 0x040017F9 RID: 6137
		internal readonly SyntaxToken keyword;

		// Token: 0x040017FA RID: 6138
		internal readonly SyntaxToken identifier;

		// Token: 0x040017FB RID: 6139
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly TypeParameterListSyntax typeParameterList;

		// Token: 0x040017FC RID: 6140
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly BaseListSyntax baseList;

		// Token: 0x040017FD RID: 6141
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode constraintClauses;

		// Token: 0x040017FE RID: 6142
		internal readonly SyntaxToken openBraceToken;

		// Token: 0x040017FF RID: 6143
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode members;

		// Token: 0x04001800 RID: 6144
		internal readonly SyntaxToken closeBraceToken;

		// Token: 0x04001801 RID: 6145
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly SyntaxToken semicolonToken;
	}
}
