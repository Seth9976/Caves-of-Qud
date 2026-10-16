using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200030E RID: 782
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ClassDeclarationSyntax : TypeDeclarationSyntax
	{
		// Token: 0x06004375 RID: 17269 RVA: 0x00112817 File Offset: 0x00110A17
		internal ClassDeclarationSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001166 RID: 4454
		// (get) Token: 0x06004376 RID: 17270 RVA: 0x00112822 File Offset: 0x00110A22
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17001167 RID: 4455
		// (get) Token: 0x06004377 RID: 17271 RVA: 0x00112838 File Offset: 0x00110A38
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

		// Token: 0x17001168 RID: 4456
		// (get) Token: 0x06004378 RID: 17272 RVA: 0x00112874 File Offset: 0x00110A74
		public override SyntaxToken Keyword
		{
			get
			{
				return new SyntaxToken(this, ((ClassDeclarationSyntax)base.Green).keyword, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17001169 RID: 4457
		// (get) Token: 0x06004379 RID: 17273 RVA: 0x0011289A File Offset: 0x00110A9A
		public override SyntaxToken Identifier
		{
			get
			{
				return new SyntaxToken(this, ((ClassDeclarationSyntax)base.Green).identifier, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x1700116A RID: 4458
		// (get) Token: 0x0600437A RID: 17274 RVA: 0x001128C0 File Offset: 0x00110AC0
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override TypeParameterListSyntax TypeParameterList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<TypeParameterListSyntax>(ref this.typeParameterList, 4);
			}
		}

		// Token: 0x1700116B RID: 4459
		// (get) Token: 0x0600437B RID: 17275 RVA: 0x001128CF File Offset: 0x00110ACF
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override BaseListSyntax BaseList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<BaseListSyntax>(ref this.baseList, 5);
			}
		}

		// Token: 0x1700116C RID: 4460
		// (get) Token: 0x0600437C RID: 17276 RVA: 0x001128DE File Offset: 0x00110ADE
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<TypeParameterConstraintClauseSyntax> ConstraintClauses
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<TypeParameterConstraintClauseSyntax>(base.GetRed(ref this.constraintClauses, 6));
			}
		}

		// Token: 0x1700116D RID: 4461
		// (get) Token: 0x0600437D RID: 17277 RVA: 0x001128F2 File Offset: 0x00110AF2
		public override SyntaxToken OpenBraceToken
		{
			get
			{
				return new SyntaxToken(this, ((ClassDeclarationSyntax)base.Green).openBraceToken, this.GetChildPosition(7), base.GetChildIndex(7));
			}
		}

		// Token: 0x1700116E RID: 4462
		// (get) Token: 0x0600437E RID: 17278 RVA: 0x00112918 File Offset: 0x00110B18
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<MemberDeclarationSyntax> Members
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<MemberDeclarationSyntax>(base.GetRed(ref this.members, 8));
			}
		}

		// Token: 0x1700116F RID: 4463
		// (get) Token: 0x0600437F RID: 17279 RVA: 0x0011292C File Offset: 0x00110B2C
		public override SyntaxToken CloseBraceToken
		{
			get
			{
				return new SyntaxToken(this, ((ClassDeclarationSyntax)base.Green).closeBraceToken, this.GetChildPosition(9), base.GetChildIndex(9));
			}
		}

		// Token: 0x17001170 RID: 4464
		// (get) Token: 0x06004380 RID: 17280 RVA: 0x00112954 File Offset: 0x00110B54
		public override SyntaxToken SemicolonToken
		{
			get
			{
				SyntaxToken semicolonToken = ((ClassDeclarationSyntax)base.Green).semicolonToken;
				if (semicolonToken == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, semicolonToken, this.GetChildPosition(10), base.GetChildIndex(10));
			}
		}

		// Token: 0x06004381 RID: 17281 RVA: 0x00112998 File Offset: 0x00110B98
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

		// Token: 0x06004382 RID: 17282 RVA: 0x00112A24 File Offset: 0x00110C24
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

		// Token: 0x06004383 RID: 17283 RVA: 0x00112A8D File Offset: 0x00110C8D
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitClassDeclaration(this);
		}

		// Token: 0x06004384 RID: 17284 RVA: 0x00112A96 File Offset: 0x00110C96
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitClassDeclaration(this);
		}

		// Token: 0x06004385 RID: 17285 RVA: 0x00112AA0 File Offset: 0x00110CA0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public ClassDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, BaseListSyntax baseList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
		{
			if (!(attributeLists != this.AttributeLists) && !(modifiers != this.Modifiers) && !(keyword != this.Keyword) && !(identifier != this.Identifier) && typeParameterList == this.TypeParameterList && baseList == this.BaseList && !(constraintClauses != this.ConstraintClauses) && !(openBraceToken != this.OpenBraceToken) && !(members != this.Members) && !(closeBraceToken != this.CloseBraceToken) && !(semicolonToken != this.SemicolonToken))
			{
				return this;
			}
			ClassDeclarationSyntax classDeclarationSyntax = SyntaxFactory.ClassDeclaration(attributeLists, modifiers, keyword, identifier, typeParameterList, baseList, constraintClauses, openBraceToken, members, closeBraceToken, semicolonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return classDeclarationSyntax;
			}
			return classDeclarationSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004386 RID: 17286 RVA: 0x00112B7A File Offset: 0x00110D7A
		internal override MemberDeclarationSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x06004387 RID: 17287 RVA: 0x00112B84 File Offset: 0x00110D84
		public new ClassDeclarationSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.Modifiers, this.Keyword, this.Identifier, this.TypeParameterList, this.BaseList, this.ConstraintClauses, this.OpenBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x06004388 RID: 17288 RVA: 0x00112BD4 File Offset: 0x00110DD4
		internal override MemberDeclarationSyntax WithModifiersCore(SyntaxTokenList modifiers)
		{
			return this.WithModifiers(modifiers);
		}

		// Token: 0x06004389 RID: 17289 RVA: 0x00112BE0 File Offset: 0x00110DE0
		public new ClassDeclarationSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return this.Update(this.AttributeLists, modifiers, this.Keyword, this.Identifier, this.TypeParameterList, this.BaseList, this.ConstraintClauses, this.OpenBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x0600438A RID: 17290 RVA: 0x00112C30 File Offset: 0x00110E30
		internal override TypeDeclarationSyntax WithKeywordCore(SyntaxToken keyword)
		{
			return this.WithKeyword(keyword);
		}

		// Token: 0x0600438B RID: 17291 RVA: 0x00112C3C File Offset: 0x00110E3C
		public new ClassDeclarationSyntax WithKeyword(SyntaxToken keyword)
		{
			return this.Update(this.AttributeLists, this.Modifiers, keyword, this.Identifier, this.TypeParameterList, this.BaseList, this.ConstraintClauses, this.OpenBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x0600438C RID: 17292 RVA: 0x00112C8C File Offset: 0x00110E8C
		internal override BaseTypeDeclarationSyntax WithIdentifierCore(SyntaxToken identifier)
		{
			return this.WithIdentifier(identifier);
		}

		// Token: 0x0600438D RID: 17293 RVA: 0x00112C98 File Offset: 0x00110E98
		public new ClassDeclarationSyntax WithIdentifier(SyntaxToken identifier)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, identifier, this.TypeParameterList, this.BaseList, this.ConstraintClauses, this.OpenBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x0600438E RID: 17294 RVA: 0x00112CE8 File Offset: 0x00110EE8
		internal override TypeDeclarationSyntax WithTypeParameterListCore([global::System.Runtime.CompilerServices.Nullable(2)] TypeParameterListSyntax typeParameterList)
		{
			return this.WithTypeParameterList(typeParameterList);
		}

		// Token: 0x0600438F RID: 17295 RVA: 0x00112CF4 File Offset: 0x00110EF4
		public new ClassDeclarationSyntax WithTypeParameterList([global::System.Runtime.CompilerServices.Nullable(2)] TypeParameterListSyntax typeParameterList)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, this.Identifier, typeParameterList, this.BaseList, this.ConstraintClauses, this.OpenBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x06004390 RID: 17296 RVA: 0x00112D44 File Offset: 0x00110F44
		internal override BaseTypeDeclarationSyntax WithBaseListCore([global::System.Runtime.CompilerServices.Nullable(2)] BaseListSyntax baseList)
		{
			return this.WithBaseList(baseList);
		}

		// Token: 0x06004391 RID: 17297 RVA: 0x00112D50 File Offset: 0x00110F50
		public new ClassDeclarationSyntax WithBaseList([global::System.Runtime.CompilerServices.Nullable(2)] BaseListSyntax baseList)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, this.Identifier, this.TypeParameterList, baseList, this.ConstraintClauses, this.OpenBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x06004392 RID: 17298 RVA: 0x00112DA0 File Offset: 0x00110FA0
		internal override TypeDeclarationSyntax WithConstraintClausesCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses)
		{
			return this.WithConstraintClauses(constraintClauses);
		}

		// Token: 0x06004393 RID: 17299 RVA: 0x00112DAC File Offset: 0x00110FAC
		public new ClassDeclarationSyntax WithConstraintClauses([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, this.Identifier, this.TypeParameterList, this.BaseList, constraintClauses, this.OpenBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x06004394 RID: 17300 RVA: 0x00112DFC File Offset: 0x00110FFC
		internal override BaseTypeDeclarationSyntax WithOpenBraceTokenCore(SyntaxToken openBraceToken)
		{
			return this.WithOpenBraceToken(openBraceToken);
		}

		// Token: 0x06004395 RID: 17301 RVA: 0x00112E08 File Offset: 0x00111008
		public new ClassDeclarationSyntax WithOpenBraceToken(SyntaxToken openBraceToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, this.Identifier, this.TypeParameterList, this.BaseList, this.ConstraintClauses, openBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x06004396 RID: 17302 RVA: 0x00112E58 File Offset: 0x00111058
		internal override TypeDeclarationSyntax WithMembersCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members)
		{
			return this.WithMembers(members);
		}

		// Token: 0x06004397 RID: 17303 RVA: 0x00112E64 File Offset: 0x00111064
		public new ClassDeclarationSyntax WithMembers([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, this.Identifier, this.TypeParameterList, this.BaseList, this.ConstraintClauses, this.OpenBraceToken, members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x06004398 RID: 17304 RVA: 0x00112EB4 File Offset: 0x001110B4
		internal override BaseTypeDeclarationSyntax WithCloseBraceTokenCore(SyntaxToken closeBraceToken)
		{
			return this.WithCloseBraceToken(closeBraceToken);
		}

		// Token: 0x06004399 RID: 17305 RVA: 0x00112EC0 File Offset: 0x001110C0
		public new ClassDeclarationSyntax WithCloseBraceToken(SyntaxToken closeBraceToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, this.Identifier, this.TypeParameterList, this.BaseList, this.ConstraintClauses, this.OpenBraceToken, this.Members, closeBraceToken, this.SemicolonToken);
		}

		// Token: 0x0600439A RID: 17306 RVA: 0x00112F10 File Offset: 0x00111110
		internal override BaseTypeDeclarationSyntax WithSemicolonTokenCore(SyntaxToken semicolonToken)
		{
			return this.WithSemicolonToken(semicolonToken);
		}

		// Token: 0x0600439B RID: 17307 RVA: 0x00112F1C File Offset: 0x0011111C
		public new ClassDeclarationSyntax WithSemicolonToken(SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, this.Identifier, this.TypeParameterList, this.BaseList, this.ConstraintClauses, this.OpenBraceToken, this.Members, this.CloseBraceToken, semicolonToken);
		}

		// Token: 0x0600439C RID: 17308 RVA: 0x00112F6C File Offset: 0x0011116C
		internal override MemberDeclarationSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x0600439D RID: 17309 RVA: 0x00112F78 File Offset: 0x00111178
		public new ClassDeclarationSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x0600439E RID: 17310 RVA: 0x00112F9A File Offset: 0x0011119A
		internal override MemberDeclarationSyntax AddModifiersCore(params SyntaxToken[] items)
		{
			return this.AddModifiers(items);
		}

		// Token: 0x0600439F RID: 17311 RVA: 0x00112FA4 File Offset: 0x001111A4
		public new ClassDeclarationSyntax AddModifiers(params SyntaxToken[] items)
		{
			return this.WithModifiers(this.Modifiers.AddRange(items));
		}

		// Token: 0x060043A0 RID: 17312 RVA: 0x00112FC6 File Offset: 0x001111C6
		internal override TypeDeclarationSyntax AddTypeParameterListParametersCore(params TypeParameterSyntax[] items)
		{
			return this.AddTypeParameterListParameters(items);
		}

		// Token: 0x060043A1 RID: 17313 RVA: 0x00112FD0 File Offset: 0x001111D0
		public new ClassDeclarationSyntax AddTypeParameterListParameters(params TypeParameterSyntax[] items)
		{
			TypeParameterListSyntax typeParameterListSyntax;
			if ((typeParameterListSyntax = this.TypeParameterList) == null)
			{
				typeParameterListSyntax = SyntaxFactory.TypeParameterList(default(SeparatedSyntaxList<TypeParameterSyntax>));
			}
			TypeParameterListSyntax typeParameterListSyntax2 = typeParameterListSyntax;
			return this.WithTypeParameterList(typeParameterListSyntax2.WithParameters(typeParameterListSyntax2.Parameters.AddRange(items)));
		}

		// Token: 0x060043A2 RID: 17314 RVA: 0x00113011 File Offset: 0x00111211
		internal override BaseTypeDeclarationSyntax AddBaseListTypesCore(params BaseTypeSyntax[] items)
		{
			return this.AddBaseListTypes(items);
		}

		// Token: 0x060043A3 RID: 17315 RVA: 0x0011301C File Offset: 0x0011121C
		public new ClassDeclarationSyntax AddBaseListTypes(params BaseTypeSyntax[] items)
		{
			BaseListSyntax baseListSyntax;
			if ((baseListSyntax = this.BaseList) == null)
			{
				baseListSyntax = SyntaxFactory.BaseList(default(SeparatedSyntaxList<BaseTypeSyntax>));
			}
			BaseListSyntax baseListSyntax2 = baseListSyntax;
			return this.WithBaseList(baseListSyntax2.WithTypes(baseListSyntax2.Types.AddRange(items)));
		}

		// Token: 0x060043A4 RID: 17316 RVA: 0x0011305D File Offset: 0x0011125D
		internal override TypeDeclarationSyntax AddConstraintClausesCore(params TypeParameterConstraintClauseSyntax[] items)
		{
			return this.AddConstraintClauses(items);
		}

		// Token: 0x060043A5 RID: 17317 RVA: 0x00113068 File Offset: 0x00111268
		public new ClassDeclarationSyntax AddConstraintClauses(params TypeParameterConstraintClauseSyntax[] items)
		{
			return this.WithConstraintClauses(this.ConstraintClauses.AddRange(items));
		}

		// Token: 0x060043A6 RID: 17318 RVA: 0x0011308A File Offset: 0x0011128A
		internal override TypeDeclarationSyntax AddMembersCore(params MemberDeclarationSyntax[] items)
		{
			return this.AddMembers(items);
		}

		// Token: 0x060043A7 RID: 17319 RVA: 0x00113094 File Offset: 0x00111294
		public new ClassDeclarationSyntax AddMembers(params MemberDeclarationSyntax[] items)
		{
			return this.WithMembers(this.Members.AddRange(items));
		}

		// Token: 0x040014DF RID: 5343
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x040014E0 RID: 5344
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeParameterListSyntax typeParameterList;

		// Token: 0x040014E1 RID: 5345
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private BaseListSyntax baseList;

		// Token: 0x040014E2 RID: 5346
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode constraintClauses;

		// Token: 0x040014E3 RID: 5347
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode members;
	}
}
