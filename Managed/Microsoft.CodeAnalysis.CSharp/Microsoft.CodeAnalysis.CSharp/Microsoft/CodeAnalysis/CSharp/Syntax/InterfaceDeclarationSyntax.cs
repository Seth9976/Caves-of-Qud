using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000310 RID: 784
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class InterfaceDeclarationSyntax : TypeDeclarationSyntax
	{
		// Token: 0x060043DB RID: 17371 RVA: 0x00113956 File Offset: 0x00111B56
		internal InterfaceDeclarationSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700117C RID: 4476
		// (get) Token: 0x060043DC RID: 17372 RVA: 0x00113961 File Offset: 0x00111B61
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x1700117D RID: 4477
		// (get) Token: 0x060043DD RID: 17373 RVA: 0x00113978 File Offset: 0x00111B78
		public override SyntaxTokenList Modifiers
		{
			get
			{
				GreenNode slot = base.Green.GetSlot(1);
				if (slot == null)
				{
					return default(SyntaxTokenList);
				}
				return new SyntaxTokenList(this, slot, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x1700117E RID: 4478
		// (get) Token: 0x060043DE RID: 17374 RVA: 0x001139B4 File Offset: 0x00111BB4
		public override SyntaxToken Keyword
		{
			get
			{
				return new SyntaxToken(this, ((InterfaceDeclarationSyntax)base.Green).keyword, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x1700117F RID: 4479
		// (get) Token: 0x060043DF RID: 17375 RVA: 0x001139DA File Offset: 0x00111BDA
		public override SyntaxToken Identifier
		{
			get
			{
				return new SyntaxToken(this, ((InterfaceDeclarationSyntax)base.Green).identifier, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x17001180 RID: 4480
		// (get) Token: 0x060043E0 RID: 17376 RVA: 0x00113A00 File Offset: 0x00111C00
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override TypeParameterListSyntax TypeParameterList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<TypeParameterListSyntax>(ref this.typeParameterList, 4);
			}
		}

		// Token: 0x17001181 RID: 4481
		// (get) Token: 0x060043E1 RID: 17377 RVA: 0x00113A0F File Offset: 0x00111C0F
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override BaseListSyntax BaseList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<BaseListSyntax>(ref this.baseList, 5);
			}
		}

		// Token: 0x17001182 RID: 4482
		// (get) Token: 0x060043E2 RID: 17378 RVA: 0x00113A1E File Offset: 0x00111C1E
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<TypeParameterConstraintClauseSyntax> ConstraintClauses
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<TypeParameterConstraintClauseSyntax>(base.GetRed(ref this.constraintClauses, 6));
			}
		}

		// Token: 0x17001183 RID: 4483
		// (get) Token: 0x060043E3 RID: 17379 RVA: 0x00113A32 File Offset: 0x00111C32
		public override SyntaxToken OpenBraceToken
		{
			get
			{
				return new SyntaxToken(this, ((InterfaceDeclarationSyntax)base.Green).openBraceToken, this.GetChildPosition(7), base.GetChildIndex(7));
			}
		}

		// Token: 0x17001184 RID: 4484
		// (get) Token: 0x060043E4 RID: 17380 RVA: 0x00113A58 File Offset: 0x00111C58
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<MemberDeclarationSyntax> Members
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<MemberDeclarationSyntax>(base.GetRed(ref this.members, 8));
			}
		}

		// Token: 0x17001185 RID: 4485
		// (get) Token: 0x060043E5 RID: 17381 RVA: 0x00113A6C File Offset: 0x00111C6C
		public override SyntaxToken CloseBraceToken
		{
			get
			{
				return new SyntaxToken(this, ((InterfaceDeclarationSyntax)base.Green).closeBraceToken, this.GetChildPosition(9), base.GetChildIndex(9));
			}
		}

		// Token: 0x17001186 RID: 4486
		// (get) Token: 0x060043E6 RID: 17382 RVA: 0x00113A94 File Offset: 0x00111C94
		public override SyntaxToken SemicolonToken
		{
			get
			{
				SyntaxToken semicolonToken = ((InterfaceDeclarationSyntax)base.Green).semicolonToken;
				if (semicolonToken == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, semicolonToken, this.GetChildPosition(10), base.GetChildIndex(10));
			}
		}

		// Token: 0x060043E7 RID: 17383 RVA: 0x00113AD8 File Offset: 0x00111CD8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			switch (index)
			{
			case 0:
				return base.GetRedAtZero(ref this.attributeLists);
			case 4:
				return base.GetRed<TypeParameterListSyntax>(ref this.typeParameterList, 4);
			case 5:
				return base.GetRed<BaseListSyntax>(ref this.baseList, 5);
			case 6:
				return base.GetRed(ref this.constraintClauses, 6);
			case 8:
				return base.GetRed(ref this.members, 8);
			}
			return null;
		}

		// Token: 0x060043E8 RID: 17384 RVA: 0x00113B64 File Offset: 0x00111D64
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			switch (index)
			{
			case 0:
				return this.attributeLists;
			case 4:
				return this.typeParameterList;
			case 5:
				return this.baseList;
			case 6:
				return this.constraintClauses;
			case 8:
				return this.members;
			}
			return null;
		}

		// Token: 0x060043E9 RID: 17385 RVA: 0x00113BCD File Offset: 0x00111DCD
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitInterfaceDeclaration(this);
		}

		// Token: 0x060043EA RID: 17386 RVA: 0x00113BD6 File Offset: 0x00111DD6
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitInterfaceDeclaration(this);
		}

		// Token: 0x060043EB RID: 17387 RVA: 0x00113BE0 File Offset: 0x00111DE0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public InterfaceDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, BaseListSyntax baseList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
		{
			if (!(attributeLists != this.AttributeLists) && !(modifiers != this.Modifiers) && !(keyword != this.Keyword) && !(identifier != this.Identifier) && typeParameterList == this.TypeParameterList && baseList == this.BaseList && !(constraintClauses != this.ConstraintClauses) && !(openBraceToken != this.OpenBraceToken) && !(members != this.Members) && !(closeBraceToken != this.CloseBraceToken) && !(semicolonToken != this.SemicolonToken))
			{
				return this;
			}
			InterfaceDeclarationSyntax interfaceDeclarationSyntax = SyntaxFactory.InterfaceDeclaration(attributeLists, modifiers, keyword, identifier, typeParameterList, baseList, constraintClauses, openBraceToken, members, closeBraceToken, semicolonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return interfaceDeclarationSyntax;
			}
			return interfaceDeclarationSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060043EC RID: 17388 RVA: 0x00113CBA File Offset: 0x00111EBA
		internal override MemberDeclarationSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x060043ED RID: 17389 RVA: 0x00113CC4 File Offset: 0x00111EC4
		public new InterfaceDeclarationSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.Modifiers, this.Keyword, this.Identifier, this.TypeParameterList, this.BaseList, this.ConstraintClauses, this.OpenBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x060043EE RID: 17390 RVA: 0x00113D14 File Offset: 0x00111F14
		internal override MemberDeclarationSyntax WithModifiersCore(SyntaxTokenList modifiers)
		{
			return this.WithModifiers(modifiers);
		}

		// Token: 0x060043EF RID: 17391 RVA: 0x00113D20 File Offset: 0x00111F20
		public new InterfaceDeclarationSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return this.Update(this.AttributeLists, modifiers, this.Keyword, this.Identifier, this.TypeParameterList, this.BaseList, this.ConstraintClauses, this.OpenBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x060043F0 RID: 17392 RVA: 0x00113D70 File Offset: 0x00111F70
		internal override TypeDeclarationSyntax WithKeywordCore(SyntaxToken keyword)
		{
			return this.WithKeyword(keyword);
		}

		// Token: 0x060043F1 RID: 17393 RVA: 0x00113D7C File Offset: 0x00111F7C
		public new InterfaceDeclarationSyntax WithKeyword(SyntaxToken keyword)
		{
			return this.Update(this.AttributeLists, this.Modifiers, keyword, this.Identifier, this.TypeParameterList, this.BaseList, this.ConstraintClauses, this.OpenBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x060043F2 RID: 17394 RVA: 0x00113DCC File Offset: 0x00111FCC
		internal override BaseTypeDeclarationSyntax WithIdentifierCore(SyntaxToken identifier)
		{
			return this.WithIdentifier(identifier);
		}

		// Token: 0x060043F3 RID: 17395 RVA: 0x00113DD8 File Offset: 0x00111FD8
		public new InterfaceDeclarationSyntax WithIdentifier(SyntaxToken identifier)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, identifier, this.TypeParameterList, this.BaseList, this.ConstraintClauses, this.OpenBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x060043F4 RID: 17396 RVA: 0x00113E28 File Offset: 0x00112028
		internal override TypeDeclarationSyntax WithTypeParameterListCore([global::System.Runtime.CompilerServices.Nullable(2)] TypeParameterListSyntax typeParameterList)
		{
			return this.WithTypeParameterList(typeParameterList);
		}

		// Token: 0x060043F5 RID: 17397 RVA: 0x00113E34 File Offset: 0x00112034
		public new InterfaceDeclarationSyntax WithTypeParameterList([global::System.Runtime.CompilerServices.Nullable(2)] TypeParameterListSyntax typeParameterList)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, this.Identifier, typeParameterList, this.BaseList, this.ConstraintClauses, this.OpenBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x060043F6 RID: 17398 RVA: 0x00113E84 File Offset: 0x00112084
		internal override BaseTypeDeclarationSyntax WithBaseListCore([global::System.Runtime.CompilerServices.Nullable(2)] BaseListSyntax baseList)
		{
			return this.WithBaseList(baseList);
		}

		// Token: 0x060043F7 RID: 17399 RVA: 0x00113E90 File Offset: 0x00112090
		public new InterfaceDeclarationSyntax WithBaseList([global::System.Runtime.CompilerServices.Nullable(2)] BaseListSyntax baseList)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, this.Identifier, this.TypeParameterList, baseList, this.ConstraintClauses, this.OpenBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x060043F8 RID: 17400 RVA: 0x00113EE0 File Offset: 0x001120E0
		internal override TypeDeclarationSyntax WithConstraintClausesCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses)
		{
			return this.WithConstraintClauses(constraintClauses);
		}

		// Token: 0x060043F9 RID: 17401 RVA: 0x00113EEC File Offset: 0x001120EC
		public new InterfaceDeclarationSyntax WithConstraintClauses([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, this.Identifier, this.TypeParameterList, this.BaseList, constraintClauses, this.OpenBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x060043FA RID: 17402 RVA: 0x00113F3C File Offset: 0x0011213C
		internal override BaseTypeDeclarationSyntax WithOpenBraceTokenCore(SyntaxToken openBraceToken)
		{
			return this.WithOpenBraceToken(openBraceToken);
		}

		// Token: 0x060043FB RID: 17403 RVA: 0x00113F48 File Offset: 0x00112148
		public new InterfaceDeclarationSyntax WithOpenBraceToken(SyntaxToken openBraceToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, this.Identifier, this.TypeParameterList, this.BaseList, this.ConstraintClauses, openBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x060043FC RID: 17404 RVA: 0x00113F98 File Offset: 0x00112198
		internal override TypeDeclarationSyntax WithMembersCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members)
		{
			return this.WithMembers(members);
		}

		// Token: 0x060043FD RID: 17405 RVA: 0x00113FA4 File Offset: 0x001121A4
		public new InterfaceDeclarationSyntax WithMembers([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, this.Identifier, this.TypeParameterList, this.BaseList, this.ConstraintClauses, this.OpenBraceToken, members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x060043FE RID: 17406 RVA: 0x00113FF4 File Offset: 0x001121F4
		internal override BaseTypeDeclarationSyntax WithCloseBraceTokenCore(SyntaxToken closeBraceToken)
		{
			return this.WithCloseBraceToken(closeBraceToken);
		}

		// Token: 0x060043FF RID: 17407 RVA: 0x00114000 File Offset: 0x00112200
		public new InterfaceDeclarationSyntax WithCloseBraceToken(SyntaxToken closeBraceToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, this.Identifier, this.TypeParameterList, this.BaseList, this.ConstraintClauses, this.OpenBraceToken, this.Members, closeBraceToken, this.SemicolonToken);
		}

		// Token: 0x06004400 RID: 17408 RVA: 0x00114050 File Offset: 0x00112250
		internal override BaseTypeDeclarationSyntax WithSemicolonTokenCore(SyntaxToken semicolonToken)
		{
			return this.WithSemicolonToken(semicolonToken);
		}

		// Token: 0x06004401 RID: 17409 RVA: 0x0011405C File Offset: 0x0011225C
		public new InterfaceDeclarationSyntax WithSemicolonToken(SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, this.Identifier, this.TypeParameterList, this.BaseList, this.ConstraintClauses, this.OpenBraceToken, this.Members, this.CloseBraceToken, semicolonToken);
		}

		// Token: 0x06004402 RID: 17410 RVA: 0x001140AC File Offset: 0x001122AC
		internal override MemberDeclarationSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x06004403 RID: 17411 RVA: 0x001140B8 File Offset: 0x001122B8
		public new InterfaceDeclarationSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x06004404 RID: 17412 RVA: 0x001140DA File Offset: 0x001122DA
		internal override MemberDeclarationSyntax AddModifiersCore(params SyntaxToken[] items)
		{
			return this.AddModifiers(items);
		}

		// Token: 0x06004405 RID: 17413 RVA: 0x001140E4 File Offset: 0x001122E4
		public new InterfaceDeclarationSyntax AddModifiers(params SyntaxToken[] items)
		{
			return this.WithModifiers(this.Modifiers.AddRange(items));
		}

		// Token: 0x06004406 RID: 17414 RVA: 0x00114106 File Offset: 0x00112306
		internal override TypeDeclarationSyntax AddTypeParameterListParametersCore(params TypeParameterSyntax[] items)
		{
			return this.AddTypeParameterListParameters(items);
		}

		// Token: 0x06004407 RID: 17415 RVA: 0x00114110 File Offset: 0x00112310
		public new InterfaceDeclarationSyntax AddTypeParameterListParameters(params TypeParameterSyntax[] items)
		{
			TypeParameterListSyntax typeParameterListSyntax;
			if ((typeParameterListSyntax = this.TypeParameterList) == null)
			{
				typeParameterListSyntax = SyntaxFactory.TypeParameterList(default(SeparatedSyntaxList<TypeParameterSyntax>));
			}
			TypeParameterListSyntax typeParameterListSyntax2 = typeParameterListSyntax;
			return this.WithTypeParameterList(typeParameterListSyntax2.WithParameters(typeParameterListSyntax2.Parameters.AddRange(items)));
		}

		// Token: 0x06004408 RID: 17416 RVA: 0x00114151 File Offset: 0x00112351
		internal override BaseTypeDeclarationSyntax AddBaseListTypesCore(params BaseTypeSyntax[] items)
		{
			return this.AddBaseListTypes(items);
		}

		// Token: 0x06004409 RID: 17417 RVA: 0x0011415C File Offset: 0x0011235C
		public new InterfaceDeclarationSyntax AddBaseListTypes(params BaseTypeSyntax[] items)
		{
			BaseListSyntax baseListSyntax;
			if ((baseListSyntax = this.BaseList) == null)
			{
				baseListSyntax = SyntaxFactory.BaseList(default(SeparatedSyntaxList<BaseTypeSyntax>));
			}
			BaseListSyntax baseListSyntax2 = baseListSyntax;
			return this.WithBaseList(baseListSyntax2.WithTypes(baseListSyntax2.Types.AddRange(items)));
		}

		// Token: 0x0600440A RID: 17418 RVA: 0x0011419D File Offset: 0x0011239D
		internal override TypeDeclarationSyntax AddConstraintClausesCore(params TypeParameterConstraintClauseSyntax[] items)
		{
			return this.AddConstraintClauses(items);
		}

		// Token: 0x0600440B RID: 17419 RVA: 0x001141A8 File Offset: 0x001123A8
		public new InterfaceDeclarationSyntax AddConstraintClauses(params TypeParameterConstraintClauseSyntax[] items)
		{
			return this.WithConstraintClauses(this.ConstraintClauses.AddRange(items));
		}

		// Token: 0x0600440C RID: 17420 RVA: 0x001141CA File Offset: 0x001123CA
		internal override TypeDeclarationSyntax AddMembersCore(params MemberDeclarationSyntax[] items)
		{
			return this.AddMembers(items);
		}

		// Token: 0x0600440D RID: 17421 RVA: 0x001141D4 File Offset: 0x001123D4
		public new InterfaceDeclarationSyntax AddMembers(params MemberDeclarationSyntax[] items)
		{
			return this.WithMembers(this.Members.AddRange(items));
		}

		// Token: 0x040014E9 RID: 5353
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x040014EA RID: 5354
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeParameterListSyntax typeParameterList;

		// Token: 0x040014EB RID: 5355
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private BaseListSyntax baseList;

		// Token: 0x040014EC RID: 5356
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode constraintClauses;

		// Token: 0x040014ED RID: 5357
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode members;
	}
}
