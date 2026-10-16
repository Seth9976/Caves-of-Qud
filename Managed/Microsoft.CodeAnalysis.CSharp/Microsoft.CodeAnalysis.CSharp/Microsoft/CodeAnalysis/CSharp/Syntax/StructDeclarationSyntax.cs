using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200030F RID: 783
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class StructDeclarationSyntax : TypeDeclarationSyntax
	{
		// Token: 0x060043A8 RID: 17320 RVA: 0x001130B6 File Offset: 0x001112B6
		internal StructDeclarationSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001171 RID: 4465
		// (get) Token: 0x060043A9 RID: 17321 RVA: 0x001130C1 File Offset: 0x001112C1
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17001172 RID: 4466
		// (get) Token: 0x060043AA RID: 17322 RVA: 0x001130D8 File Offset: 0x001112D8
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

		// Token: 0x17001173 RID: 4467
		// (get) Token: 0x060043AB RID: 17323 RVA: 0x00113114 File Offset: 0x00111314
		public override SyntaxToken Keyword
		{
			get
			{
				return new SyntaxToken(this, ((StructDeclarationSyntax)base.Green).keyword, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17001174 RID: 4468
		// (get) Token: 0x060043AC RID: 17324 RVA: 0x0011313A File Offset: 0x0011133A
		public override SyntaxToken Identifier
		{
			get
			{
				return new SyntaxToken(this, ((StructDeclarationSyntax)base.Green).identifier, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x17001175 RID: 4469
		// (get) Token: 0x060043AD RID: 17325 RVA: 0x00113160 File Offset: 0x00111360
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override TypeParameterListSyntax TypeParameterList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<TypeParameterListSyntax>(ref this.typeParameterList, 4);
			}
		}

		// Token: 0x17001176 RID: 4470
		// (get) Token: 0x060043AE RID: 17326 RVA: 0x0011316F File Offset: 0x0011136F
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override BaseListSyntax BaseList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<BaseListSyntax>(ref this.baseList, 5);
			}
		}

		// Token: 0x17001177 RID: 4471
		// (get) Token: 0x060043AF RID: 17327 RVA: 0x0011317E File Offset: 0x0011137E
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<TypeParameterConstraintClauseSyntax> ConstraintClauses
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<TypeParameterConstraintClauseSyntax>(base.GetRed(ref this.constraintClauses, 6));
			}
		}

		// Token: 0x17001178 RID: 4472
		// (get) Token: 0x060043B0 RID: 17328 RVA: 0x00113192 File Offset: 0x00111392
		public override SyntaxToken OpenBraceToken
		{
			get
			{
				return new SyntaxToken(this, ((StructDeclarationSyntax)base.Green).openBraceToken, this.GetChildPosition(7), base.GetChildIndex(7));
			}
		}

		// Token: 0x17001179 RID: 4473
		// (get) Token: 0x060043B1 RID: 17329 RVA: 0x001131B8 File Offset: 0x001113B8
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<MemberDeclarationSyntax> Members
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<MemberDeclarationSyntax>(base.GetRed(ref this.members, 8));
			}
		}

		// Token: 0x1700117A RID: 4474
		// (get) Token: 0x060043B2 RID: 17330 RVA: 0x001131CC File Offset: 0x001113CC
		public override SyntaxToken CloseBraceToken
		{
			get
			{
				return new SyntaxToken(this, ((StructDeclarationSyntax)base.Green).closeBraceToken, this.GetChildPosition(9), base.GetChildIndex(9));
			}
		}

		// Token: 0x1700117B RID: 4475
		// (get) Token: 0x060043B3 RID: 17331 RVA: 0x001131F4 File Offset: 0x001113F4
		public override SyntaxToken SemicolonToken
		{
			get
			{
				SyntaxToken semicolonToken = ((StructDeclarationSyntax)base.Green).semicolonToken;
				if (semicolonToken == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, semicolonToken, this.GetChildPosition(10), base.GetChildIndex(10));
			}
		}

		// Token: 0x060043B4 RID: 17332 RVA: 0x00113238 File Offset: 0x00111438
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

		// Token: 0x060043B5 RID: 17333 RVA: 0x001132C4 File Offset: 0x001114C4
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

		// Token: 0x060043B6 RID: 17334 RVA: 0x0011332D File Offset: 0x0011152D
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitStructDeclaration(this);
		}

		// Token: 0x060043B7 RID: 17335 RVA: 0x00113336 File Offset: 0x00111536
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitStructDeclaration(this);
		}

		// Token: 0x060043B8 RID: 17336 RVA: 0x00113340 File Offset: 0x00111540
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public StructDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, BaseListSyntax baseList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
		{
			if (!(attributeLists != this.AttributeLists) && !(modifiers != this.Modifiers) && !(keyword != this.Keyword) && !(identifier != this.Identifier) && typeParameterList == this.TypeParameterList && baseList == this.BaseList && !(constraintClauses != this.ConstraintClauses) && !(openBraceToken != this.OpenBraceToken) && !(members != this.Members) && !(closeBraceToken != this.CloseBraceToken) && !(semicolonToken != this.SemicolonToken))
			{
				return this;
			}
			StructDeclarationSyntax structDeclarationSyntax = SyntaxFactory.StructDeclaration(attributeLists, modifiers, keyword, identifier, typeParameterList, baseList, constraintClauses, openBraceToken, members, closeBraceToken, semicolonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return structDeclarationSyntax;
			}
			return structDeclarationSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060043B9 RID: 17337 RVA: 0x0011341A File Offset: 0x0011161A
		internal override MemberDeclarationSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x060043BA RID: 17338 RVA: 0x00113424 File Offset: 0x00111624
		public new StructDeclarationSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.Modifiers, this.Keyword, this.Identifier, this.TypeParameterList, this.BaseList, this.ConstraintClauses, this.OpenBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x060043BB RID: 17339 RVA: 0x00113474 File Offset: 0x00111674
		internal override MemberDeclarationSyntax WithModifiersCore(SyntaxTokenList modifiers)
		{
			return this.WithModifiers(modifiers);
		}

		// Token: 0x060043BC RID: 17340 RVA: 0x00113480 File Offset: 0x00111680
		public new StructDeclarationSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return this.Update(this.AttributeLists, modifiers, this.Keyword, this.Identifier, this.TypeParameterList, this.BaseList, this.ConstraintClauses, this.OpenBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x060043BD RID: 17341 RVA: 0x001134D0 File Offset: 0x001116D0
		internal override TypeDeclarationSyntax WithKeywordCore(SyntaxToken keyword)
		{
			return this.WithKeyword(keyword);
		}

		// Token: 0x060043BE RID: 17342 RVA: 0x001134DC File Offset: 0x001116DC
		public new StructDeclarationSyntax WithKeyword(SyntaxToken keyword)
		{
			return this.Update(this.AttributeLists, this.Modifiers, keyword, this.Identifier, this.TypeParameterList, this.BaseList, this.ConstraintClauses, this.OpenBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x060043BF RID: 17343 RVA: 0x0011352C File Offset: 0x0011172C
		internal override BaseTypeDeclarationSyntax WithIdentifierCore(SyntaxToken identifier)
		{
			return this.WithIdentifier(identifier);
		}

		// Token: 0x060043C0 RID: 17344 RVA: 0x00113538 File Offset: 0x00111738
		public new StructDeclarationSyntax WithIdentifier(SyntaxToken identifier)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, identifier, this.TypeParameterList, this.BaseList, this.ConstraintClauses, this.OpenBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x060043C1 RID: 17345 RVA: 0x00113588 File Offset: 0x00111788
		internal override TypeDeclarationSyntax WithTypeParameterListCore([global::System.Runtime.CompilerServices.Nullable(2)] TypeParameterListSyntax typeParameterList)
		{
			return this.WithTypeParameterList(typeParameterList);
		}

		// Token: 0x060043C2 RID: 17346 RVA: 0x00113594 File Offset: 0x00111794
		public new StructDeclarationSyntax WithTypeParameterList([global::System.Runtime.CompilerServices.Nullable(2)] TypeParameterListSyntax typeParameterList)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, this.Identifier, typeParameterList, this.BaseList, this.ConstraintClauses, this.OpenBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x060043C3 RID: 17347 RVA: 0x001135E4 File Offset: 0x001117E4
		internal override BaseTypeDeclarationSyntax WithBaseListCore([global::System.Runtime.CompilerServices.Nullable(2)] BaseListSyntax baseList)
		{
			return this.WithBaseList(baseList);
		}

		// Token: 0x060043C4 RID: 17348 RVA: 0x001135F0 File Offset: 0x001117F0
		public new StructDeclarationSyntax WithBaseList([global::System.Runtime.CompilerServices.Nullable(2)] BaseListSyntax baseList)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, this.Identifier, this.TypeParameterList, baseList, this.ConstraintClauses, this.OpenBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x060043C5 RID: 17349 RVA: 0x00113640 File Offset: 0x00111840
		internal override TypeDeclarationSyntax WithConstraintClausesCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses)
		{
			return this.WithConstraintClauses(constraintClauses);
		}

		// Token: 0x060043C6 RID: 17350 RVA: 0x0011364C File Offset: 0x0011184C
		public new StructDeclarationSyntax WithConstraintClauses([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, this.Identifier, this.TypeParameterList, this.BaseList, constraintClauses, this.OpenBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x060043C7 RID: 17351 RVA: 0x0011369C File Offset: 0x0011189C
		internal override BaseTypeDeclarationSyntax WithOpenBraceTokenCore(SyntaxToken openBraceToken)
		{
			return this.WithOpenBraceToken(openBraceToken);
		}

		// Token: 0x060043C8 RID: 17352 RVA: 0x001136A8 File Offset: 0x001118A8
		public new StructDeclarationSyntax WithOpenBraceToken(SyntaxToken openBraceToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, this.Identifier, this.TypeParameterList, this.BaseList, this.ConstraintClauses, openBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x060043C9 RID: 17353 RVA: 0x001136F8 File Offset: 0x001118F8
		internal override TypeDeclarationSyntax WithMembersCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members)
		{
			return this.WithMembers(members);
		}

		// Token: 0x060043CA RID: 17354 RVA: 0x00113704 File Offset: 0x00111904
		public new StructDeclarationSyntax WithMembers([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, this.Identifier, this.TypeParameterList, this.BaseList, this.ConstraintClauses, this.OpenBraceToken, members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x060043CB RID: 17355 RVA: 0x00113754 File Offset: 0x00111954
		internal override BaseTypeDeclarationSyntax WithCloseBraceTokenCore(SyntaxToken closeBraceToken)
		{
			return this.WithCloseBraceToken(closeBraceToken);
		}

		// Token: 0x060043CC RID: 17356 RVA: 0x00113760 File Offset: 0x00111960
		public new StructDeclarationSyntax WithCloseBraceToken(SyntaxToken closeBraceToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, this.Identifier, this.TypeParameterList, this.BaseList, this.ConstraintClauses, this.OpenBraceToken, this.Members, closeBraceToken, this.SemicolonToken);
		}

		// Token: 0x060043CD RID: 17357 RVA: 0x001137B0 File Offset: 0x001119B0
		internal override BaseTypeDeclarationSyntax WithSemicolonTokenCore(SyntaxToken semicolonToken)
		{
			return this.WithSemicolonToken(semicolonToken);
		}

		// Token: 0x060043CE RID: 17358 RVA: 0x001137BC File Offset: 0x001119BC
		public new StructDeclarationSyntax WithSemicolonToken(SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, this.Identifier, this.TypeParameterList, this.BaseList, this.ConstraintClauses, this.OpenBraceToken, this.Members, this.CloseBraceToken, semicolonToken);
		}

		// Token: 0x060043CF RID: 17359 RVA: 0x0011380C File Offset: 0x00111A0C
		internal override MemberDeclarationSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x060043D0 RID: 17360 RVA: 0x00113818 File Offset: 0x00111A18
		public new StructDeclarationSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x060043D1 RID: 17361 RVA: 0x0011383A File Offset: 0x00111A3A
		internal override MemberDeclarationSyntax AddModifiersCore(params SyntaxToken[] items)
		{
			return this.AddModifiers(items);
		}

		// Token: 0x060043D2 RID: 17362 RVA: 0x00113844 File Offset: 0x00111A44
		public new StructDeclarationSyntax AddModifiers(params SyntaxToken[] items)
		{
			return this.WithModifiers(this.Modifiers.AddRange(items));
		}

		// Token: 0x060043D3 RID: 17363 RVA: 0x00113866 File Offset: 0x00111A66
		internal override TypeDeclarationSyntax AddTypeParameterListParametersCore(params TypeParameterSyntax[] items)
		{
			return this.AddTypeParameterListParameters(items);
		}

		// Token: 0x060043D4 RID: 17364 RVA: 0x00113870 File Offset: 0x00111A70
		public new StructDeclarationSyntax AddTypeParameterListParameters(params TypeParameterSyntax[] items)
		{
			TypeParameterListSyntax typeParameterListSyntax;
			if ((typeParameterListSyntax = this.TypeParameterList) == null)
			{
				typeParameterListSyntax = SyntaxFactory.TypeParameterList(default(SeparatedSyntaxList<TypeParameterSyntax>));
			}
			TypeParameterListSyntax typeParameterListSyntax2 = typeParameterListSyntax;
			return this.WithTypeParameterList(typeParameterListSyntax2.WithParameters(typeParameterListSyntax2.Parameters.AddRange(items)));
		}

		// Token: 0x060043D5 RID: 17365 RVA: 0x001138B1 File Offset: 0x00111AB1
		internal override BaseTypeDeclarationSyntax AddBaseListTypesCore(params BaseTypeSyntax[] items)
		{
			return this.AddBaseListTypes(items);
		}

		// Token: 0x060043D6 RID: 17366 RVA: 0x001138BC File Offset: 0x00111ABC
		public new StructDeclarationSyntax AddBaseListTypes(params BaseTypeSyntax[] items)
		{
			BaseListSyntax baseListSyntax;
			if ((baseListSyntax = this.BaseList) == null)
			{
				baseListSyntax = SyntaxFactory.BaseList(default(SeparatedSyntaxList<BaseTypeSyntax>));
			}
			BaseListSyntax baseListSyntax2 = baseListSyntax;
			return this.WithBaseList(baseListSyntax2.WithTypes(baseListSyntax2.Types.AddRange(items)));
		}

		// Token: 0x060043D7 RID: 17367 RVA: 0x001138FD File Offset: 0x00111AFD
		internal override TypeDeclarationSyntax AddConstraintClausesCore(params TypeParameterConstraintClauseSyntax[] items)
		{
			return this.AddConstraintClauses(items);
		}

		// Token: 0x060043D8 RID: 17368 RVA: 0x00113908 File Offset: 0x00111B08
		public new StructDeclarationSyntax AddConstraintClauses(params TypeParameterConstraintClauseSyntax[] items)
		{
			return this.WithConstraintClauses(this.ConstraintClauses.AddRange(items));
		}

		// Token: 0x060043D9 RID: 17369 RVA: 0x0011392A File Offset: 0x00111B2A
		internal override TypeDeclarationSyntax AddMembersCore(params MemberDeclarationSyntax[] items)
		{
			return this.AddMembers(items);
		}

		// Token: 0x060043DA RID: 17370 RVA: 0x00113934 File Offset: 0x00111B34
		public new StructDeclarationSyntax AddMembers(params MemberDeclarationSyntax[] items)
		{
			return this.WithMembers(this.Members.AddRange(items));
		}

		// Token: 0x040014E4 RID: 5348
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x040014E5 RID: 5349
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeParameterListSyntax typeParameterList;

		// Token: 0x040014E6 RID: 5350
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private BaseListSyntax baseList;

		// Token: 0x040014E7 RID: 5351
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode constraintClauses;

		// Token: 0x040014E8 RID: 5352
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode members;
	}
}
