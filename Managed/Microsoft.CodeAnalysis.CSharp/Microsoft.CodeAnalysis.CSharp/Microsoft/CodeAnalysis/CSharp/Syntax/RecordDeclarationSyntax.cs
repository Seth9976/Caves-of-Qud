using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000278 RID: 632
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class RecordDeclarationSyntax : TypeDeclarationSyntax
	{
		// Token: 0x17000FC2 RID: 4034
		// (get) Token: 0x06003C1B RID: 15387 RVA: 0x00104720 File Offset: 0x00102920
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal PrimaryConstructorBaseTypeSyntax PrimaryConstructorBaseTypeIfClass
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				if (base.Kind() == SyntaxKind.RecordStructDeclaration)
				{
					return null;
				}
				BaseListSyntax baseListSyntax = this.BaseList;
				return ((baseListSyntax != null) ? baseListSyntax.Types.FirstOrDefault() : null) as PrimaryConstructorBaseTypeSyntax;
			}
		}

		// Token: 0x06003C1C RID: 15388 RVA: 0x0010475C File Offset: 0x0010295C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public RecordDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, BaseListSyntax baseList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
		{
			return this.Update(attributeLists, modifiers, keyword, this.ClassOrStructKeyword, identifier, typeParameterList, parameterList, baseList, constraintClauses, openBraceToken, members, closeBraceToken, semicolonToken);
		}

		// Token: 0x06003C1D RID: 15389 RVA: 0x0010478A File Offset: 0x0010298A
		internal RecordDeclarationSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000FC3 RID: 4035
		// (get) Token: 0x06003C1E RID: 15390 RVA: 0x00104795 File Offset: 0x00102995
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000FC4 RID: 4036
		// (get) Token: 0x06003C1F RID: 15391 RVA: 0x001047AC File Offset: 0x001029AC
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

		// Token: 0x17000FC5 RID: 4037
		// (get) Token: 0x06003C20 RID: 15392 RVA: 0x001047E8 File Offset: 0x001029E8
		public override SyntaxToken Keyword
		{
			get
			{
				return new SyntaxToken(this, ((RecordDeclarationSyntax)base.Green).keyword, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17000FC6 RID: 4038
		// (get) Token: 0x06003C21 RID: 15393 RVA: 0x00104810 File Offset: 0x00102A10
		public SyntaxToken ClassOrStructKeyword
		{
			get
			{
				SyntaxToken classOrStructKeyword = ((RecordDeclarationSyntax)base.Green).classOrStructKeyword;
				if (classOrStructKeyword == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, classOrStructKeyword, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x17000FC7 RID: 4039
		// (get) Token: 0x06003C22 RID: 15394 RVA: 0x00104850 File Offset: 0x00102A50
		public override SyntaxToken Identifier
		{
			get
			{
				return new SyntaxToken(this, ((RecordDeclarationSyntax)base.Green).identifier, this.GetChildPosition(4), base.GetChildIndex(4));
			}
		}

		// Token: 0x17000FC8 RID: 4040
		// (get) Token: 0x06003C23 RID: 15395 RVA: 0x00104876 File Offset: 0x00102A76
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override TypeParameterListSyntax TypeParameterList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<TypeParameterListSyntax>(ref this.typeParameterList, 5);
			}
		}

		// Token: 0x17000FC9 RID: 4041
		// (get) Token: 0x06003C24 RID: 15396 RVA: 0x00104885 File Offset: 0x00102A85
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ParameterListSyntax ParameterList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<ParameterListSyntax>(ref this.parameterList, 6);
			}
		}

		// Token: 0x17000FCA RID: 4042
		// (get) Token: 0x06003C25 RID: 15397 RVA: 0x00104894 File Offset: 0x00102A94
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override BaseListSyntax BaseList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<BaseListSyntax>(ref this.baseList, 7);
			}
		}

		// Token: 0x17000FCB RID: 4043
		// (get) Token: 0x06003C26 RID: 15398 RVA: 0x001048A3 File Offset: 0x00102AA3
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<TypeParameterConstraintClauseSyntax> ConstraintClauses
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<TypeParameterConstraintClauseSyntax>(base.GetRed(ref this.constraintClauses, 8));
			}
		}

		// Token: 0x17000FCC RID: 4044
		// (get) Token: 0x06003C27 RID: 15399 RVA: 0x001048B8 File Offset: 0x00102AB8
		public override SyntaxToken OpenBraceToken
		{
			get
			{
				SyntaxToken openBraceToken = ((RecordDeclarationSyntax)base.Green).openBraceToken;
				if (openBraceToken == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, openBraceToken, this.GetChildPosition(9), base.GetChildIndex(9));
			}
		}

		// Token: 0x17000FCD RID: 4045
		// (get) Token: 0x06003C28 RID: 15400 RVA: 0x001048FA File Offset: 0x00102AFA
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<MemberDeclarationSyntax> Members
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<MemberDeclarationSyntax>(base.GetRed(ref this.members, 10));
			}
		}

		// Token: 0x17000FCE RID: 4046
		// (get) Token: 0x06003C29 RID: 15401 RVA: 0x00104910 File Offset: 0x00102B10
		public override SyntaxToken CloseBraceToken
		{
			get
			{
				SyntaxToken closeBraceToken = ((RecordDeclarationSyntax)base.Green).closeBraceToken;
				if (closeBraceToken == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, closeBraceToken, this.GetChildPosition(11), base.GetChildIndex(11));
			}
		}

		// Token: 0x17000FCF RID: 4047
		// (get) Token: 0x06003C2A RID: 15402 RVA: 0x00104954 File Offset: 0x00102B54
		public override SyntaxToken SemicolonToken
		{
			get
			{
				SyntaxToken semicolonToken = ((RecordDeclarationSyntax)base.Green).semicolonToken;
				if (semicolonToken == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, semicolonToken, this.GetChildPosition(12), base.GetChildIndex(12));
			}
		}

		// Token: 0x06003C2B RID: 15403 RVA: 0x00104998 File Offset: 0x00102B98
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			switch (index)
			{
			case 0:
				return base.GetRedAtZero(ref this.attributeLists);
			case 5:
				return base.GetRed<TypeParameterListSyntax>(ref this.typeParameterList, 5);
			case 6:
				return base.GetRed<ParameterListSyntax>(ref this.parameterList, 6);
			case 7:
				return base.GetRed<BaseListSyntax>(ref this.baseList, 7);
			case 8:
				return base.GetRed(ref this.constraintClauses, 8);
			case 10:
				return base.GetRed(ref this.members, 10);
			}
			return null;
		}

		// Token: 0x06003C2C RID: 15404 RVA: 0x00104A3C File Offset: 0x00102C3C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			switch (index)
			{
			case 0:
				return this.attributeLists;
			case 5:
				return this.typeParameterList;
			case 6:
				return this.parameterList;
			case 7:
				return this.baseList;
			case 8:
				return this.constraintClauses;
			case 10:
				return this.members;
			}
			return null;
		}

		// Token: 0x06003C2D RID: 15405 RVA: 0x00104AB6 File Offset: 0x00102CB6
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitRecordDeclaration(this);
		}

		// Token: 0x06003C2E RID: 15406 RVA: 0x00104ABF File Offset: 0x00102CBF
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitRecordDeclaration(this);
		}

		// Token: 0x06003C2F RID: 15407 RVA: 0x00104AC8 File Offset: 0x00102CC8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public RecordDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken classOrStructKeyword, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, BaseListSyntax baseList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
		{
			if (!(attributeLists != this.AttributeLists) && !(modifiers != this.Modifiers) && !(keyword != this.Keyword) && !(classOrStructKeyword != this.ClassOrStructKeyword) && !(identifier != this.Identifier) && typeParameterList == this.TypeParameterList && parameterList == this.ParameterList && baseList == this.BaseList && !(constraintClauses != this.ConstraintClauses) && !(openBraceToken != this.OpenBraceToken) && !(members != this.Members) && !(closeBraceToken != this.CloseBraceToken) && !(semicolonToken != this.SemicolonToken))
			{
				return this;
			}
			RecordDeclarationSyntax recordDeclarationSyntax = SyntaxFactory.RecordDeclaration(base.Kind(), attributeLists, modifiers, keyword, classOrStructKeyword, identifier, typeParameterList, parameterList, baseList, constraintClauses, openBraceToken, members, closeBraceToken, semicolonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return recordDeclarationSyntax;
			}
			return recordDeclarationSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003C30 RID: 15408 RVA: 0x00104BCB File Offset: 0x00102DCB
		internal override MemberDeclarationSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x06003C31 RID: 15409 RVA: 0x00104BD4 File Offset: 0x00102DD4
		public new RecordDeclarationSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.Modifiers, this.Keyword, this.ClassOrStructKeyword, this.Identifier, this.TypeParameterList, this.ParameterList, this.BaseList, this.ConstraintClauses, this.OpenBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x06003C32 RID: 15410 RVA: 0x00104C30 File Offset: 0x00102E30
		internal override MemberDeclarationSyntax WithModifiersCore(SyntaxTokenList modifiers)
		{
			return this.WithModifiers(modifiers);
		}

		// Token: 0x06003C33 RID: 15411 RVA: 0x00104C3C File Offset: 0x00102E3C
		public new RecordDeclarationSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return this.Update(this.AttributeLists, modifiers, this.Keyword, this.ClassOrStructKeyword, this.Identifier, this.TypeParameterList, this.ParameterList, this.BaseList, this.ConstraintClauses, this.OpenBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x06003C34 RID: 15412 RVA: 0x00104C98 File Offset: 0x00102E98
		internal override TypeDeclarationSyntax WithKeywordCore(SyntaxToken keyword)
		{
			return this.WithKeyword(keyword);
		}

		// Token: 0x06003C35 RID: 15413 RVA: 0x00104CA4 File Offset: 0x00102EA4
		public new RecordDeclarationSyntax WithKeyword(SyntaxToken keyword)
		{
			return this.Update(this.AttributeLists, this.Modifiers, keyword, this.ClassOrStructKeyword, this.Identifier, this.TypeParameterList, this.ParameterList, this.BaseList, this.ConstraintClauses, this.OpenBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x06003C36 RID: 15414 RVA: 0x00104D00 File Offset: 0x00102F00
		public RecordDeclarationSyntax WithClassOrStructKeyword(SyntaxToken classOrStructKeyword)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, classOrStructKeyword, this.Identifier, this.TypeParameterList, this.ParameterList, this.BaseList, this.ConstraintClauses, this.OpenBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x06003C37 RID: 15415 RVA: 0x00104D5C File Offset: 0x00102F5C
		internal override BaseTypeDeclarationSyntax WithIdentifierCore(SyntaxToken identifier)
		{
			return this.WithIdentifier(identifier);
		}

		// Token: 0x06003C38 RID: 15416 RVA: 0x00104D68 File Offset: 0x00102F68
		public new RecordDeclarationSyntax WithIdentifier(SyntaxToken identifier)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, this.ClassOrStructKeyword, identifier, this.TypeParameterList, this.ParameterList, this.BaseList, this.ConstraintClauses, this.OpenBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x06003C39 RID: 15417 RVA: 0x00104DC4 File Offset: 0x00102FC4
		internal override TypeDeclarationSyntax WithTypeParameterListCore([global::System.Runtime.CompilerServices.Nullable(2)] TypeParameterListSyntax typeParameterList)
		{
			return this.WithTypeParameterList(typeParameterList);
		}

		// Token: 0x06003C3A RID: 15418 RVA: 0x00104DD0 File Offset: 0x00102FD0
		public new RecordDeclarationSyntax WithTypeParameterList([global::System.Runtime.CompilerServices.Nullable(2)] TypeParameterListSyntax typeParameterList)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, this.ClassOrStructKeyword, this.Identifier, typeParameterList, this.ParameterList, this.BaseList, this.ConstraintClauses, this.OpenBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x06003C3B RID: 15419 RVA: 0x00104E2C File Offset: 0x0010302C
		public RecordDeclarationSyntax WithParameterList([global::System.Runtime.CompilerServices.Nullable(2)] ParameterListSyntax parameterList)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, this.ClassOrStructKeyword, this.Identifier, this.TypeParameterList, parameterList, this.BaseList, this.ConstraintClauses, this.OpenBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x06003C3C RID: 15420 RVA: 0x00104E88 File Offset: 0x00103088
		internal override BaseTypeDeclarationSyntax WithBaseListCore([global::System.Runtime.CompilerServices.Nullable(2)] BaseListSyntax baseList)
		{
			return this.WithBaseList(baseList);
		}

		// Token: 0x06003C3D RID: 15421 RVA: 0x00104E94 File Offset: 0x00103094
		public new RecordDeclarationSyntax WithBaseList([global::System.Runtime.CompilerServices.Nullable(2)] BaseListSyntax baseList)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, this.ClassOrStructKeyword, this.Identifier, this.TypeParameterList, this.ParameterList, baseList, this.ConstraintClauses, this.OpenBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x06003C3E RID: 15422 RVA: 0x00104EF0 File Offset: 0x001030F0
		internal override TypeDeclarationSyntax WithConstraintClausesCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses)
		{
			return this.WithConstraintClauses(constraintClauses);
		}

		// Token: 0x06003C3F RID: 15423 RVA: 0x00104EFC File Offset: 0x001030FC
		public new RecordDeclarationSyntax WithConstraintClauses([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, this.ClassOrStructKeyword, this.Identifier, this.TypeParameterList, this.ParameterList, this.BaseList, constraintClauses, this.OpenBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x06003C40 RID: 15424 RVA: 0x00104F58 File Offset: 0x00103158
		internal override BaseTypeDeclarationSyntax WithOpenBraceTokenCore(SyntaxToken openBraceToken)
		{
			return this.WithOpenBraceToken(openBraceToken);
		}

		// Token: 0x06003C41 RID: 15425 RVA: 0x00104F64 File Offset: 0x00103164
		public new RecordDeclarationSyntax WithOpenBraceToken(SyntaxToken openBraceToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, this.ClassOrStructKeyword, this.Identifier, this.TypeParameterList, this.ParameterList, this.BaseList, this.ConstraintClauses, openBraceToken, this.Members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x06003C42 RID: 15426 RVA: 0x00104FC0 File Offset: 0x001031C0
		internal override TypeDeclarationSyntax WithMembersCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members)
		{
			return this.WithMembers(members);
		}

		// Token: 0x06003C43 RID: 15427 RVA: 0x00104FCC File Offset: 0x001031CC
		public new RecordDeclarationSyntax WithMembers([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, this.ClassOrStructKeyword, this.Identifier, this.TypeParameterList, this.ParameterList, this.BaseList, this.ConstraintClauses, this.OpenBraceToken, members, this.CloseBraceToken, this.SemicolonToken);
		}

		// Token: 0x06003C44 RID: 15428 RVA: 0x00105028 File Offset: 0x00103228
		internal override BaseTypeDeclarationSyntax WithCloseBraceTokenCore(SyntaxToken closeBraceToken)
		{
			return this.WithCloseBraceToken(closeBraceToken);
		}

		// Token: 0x06003C45 RID: 15429 RVA: 0x00105034 File Offset: 0x00103234
		public new RecordDeclarationSyntax WithCloseBraceToken(SyntaxToken closeBraceToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, this.ClassOrStructKeyword, this.Identifier, this.TypeParameterList, this.ParameterList, this.BaseList, this.ConstraintClauses, this.OpenBraceToken, this.Members, closeBraceToken, this.SemicolonToken);
		}

		// Token: 0x06003C46 RID: 15430 RVA: 0x00105090 File Offset: 0x00103290
		internal override BaseTypeDeclarationSyntax WithSemicolonTokenCore(SyntaxToken semicolonToken)
		{
			return this.WithSemicolonToken(semicolonToken);
		}

		// Token: 0x06003C47 RID: 15431 RVA: 0x0010509C File Offset: 0x0010329C
		public new RecordDeclarationSyntax WithSemicolonToken(SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Keyword, this.ClassOrStructKeyword, this.Identifier, this.TypeParameterList, this.ParameterList, this.BaseList, this.ConstraintClauses, this.OpenBraceToken, this.Members, this.CloseBraceToken, semicolonToken);
		}

		// Token: 0x06003C48 RID: 15432 RVA: 0x001050F8 File Offset: 0x001032F8
		internal override MemberDeclarationSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x06003C49 RID: 15433 RVA: 0x00105104 File Offset: 0x00103304
		public new RecordDeclarationSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x06003C4A RID: 15434 RVA: 0x00105126 File Offset: 0x00103326
		internal override MemberDeclarationSyntax AddModifiersCore(params SyntaxToken[] items)
		{
			return this.AddModifiers(items);
		}

		// Token: 0x06003C4B RID: 15435 RVA: 0x00105130 File Offset: 0x00103330
		public new RecordDeclarationSyntax AddModifiers(params SyntaxToken[] items)
		{
			return this.WithModifiers(this.Modifiers.AddRange(items));
		}

		// Token: 0x06003C4C RID: 15436 RVA: 0x00105152 File Offset: 0x00103352
		internal override TypeDeclarationSyntax AddTypeParameterListParametersCore(params TypeParameterSyntax[] items)
		{
			return this.AddTypeParameterListParameters(items);
		}

		// Token: 0x06003C4D RID: 15437 RVA: 0x0010515C File Offset: 0x0010335C
		public new RecordDeclarationSyntax AddTypeParameterListParameters(params TypeParameterSyntax[] items)
		{
			TypeParameterListSyntax typeParameterListSyntax;
			if ((typeParameterListSyntax = this.TypeParameterList) == null)
			{
				typeParameterListSyntax = SyntaxFactory.TypeParameterList(default(SeparatedSyntaxList<TypeParameterSyntax>));
			}
			TypeParameterListSyntax typeParameterListSyntax2 = typeParameterListSyntax;
			return this.WithTypeParameterList(typeParameterListSyntax2.WithParameters(typeParameterListSyntax2.Parameters.AddRange(items)));
		}

		// Token: 0x06003C4E RID: 15438 RVA: 0x001051A0 File Offset: 0x001033A0
		public RecordDeclarationSyntax AddParameterListParameters(params ParameterSyntax[] items)
		{
			ParameterListSyntax parameterListSyntax;
			if ((parameterListSyntax = this.ParameterList) == null)
			{
				parameterListSyntax = SyntaxFactory.ParameterList(default(SeparatedSyntaxList<ParameterSyntax>));
			}
			ParameterListSyntax parameterListSyntax2 = parameterListSyntax;
			return this.WithParameterList(parameterListSyntax2.WithParameters(parameterListSyntax2.Parameters.AddRange(items)));
		}

		// Token: 0x06003C4F RID: 15439 RVA: 0x001051E1 File Offset: 0x001033E1
		internal override BaseTypeDeclarationSyntax AddBaseListTypesCore(params BaseTypeSyntax[] items)
		{
			return this.AddBaseListTypes(items);
		}

		// Token: 0x06003C50 RID: 15440 RVA: 0x001051EC File Offset: 0x001033EC
		public new RecordDeclarationSyntax AddBaseListTypes(params BaseTypeSyntax[] items)
		{
			BaseListSyntax baseListSyntax;
			if ((baseListSyntax = this.BaseList) == null)
			{
				baseListSyntax = SyntaxFactory.BaseList(default(SeparatedSyntaxList<BaseTypeSyntax>));
			}
			BaseListSyntax baseListSyntax2 = baseListSyntax;
			return this.WithBaseList(baseListSyntax2.WithTypes(baseListSyntax2.Types.AddRange(items)));
		}

		// Token: 0x06003C51 RID: 15441 RVA: 0x0010522D File Offset: 0x0010342D
		internal override TypeDeclarationSyntax AddConstraintClausesCore(params TypeParameterConstraintClauseSyntax[] items)
		{
			return this.AddConstraintClauses(items);
		}

		// Token: 0x06003C52 RID: 15442 RVA: 0x00105238 File Offset: 0x00103438
		public new RecordDeclarationSyntax AddConstraintClauses(params TypeParameterConstraintClauseSyntax[] items)
		{
			return this.WithConstraintClauses(this.ConstraintClauses.AddRange(items));
		}

		// Token: 0x06003C53 RID: 15443 RVA: 0x0010525A File Offset: 0x0010345A
		internal override TypeDeclarationSyntax AddMembersCore(params MemberDeclarationSyntax[] items)
		{
			return this.AddMembers(items);
		}

		// Token: 0x06003C54 RID: 15444 RVA: 0x00105264 File Offset: 0x00103464
		public new RecordDeclarationSyntax AddMembers(params MemberDeclarationSyntax[] items)
		{
			return this.WithMembers(this.Members.AddRange(items));
		}

		// Token: 0x04001415 RID: 5141
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x04001416 RID: 5142
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeParameterListSyntax typeParameterList;

		// Token: 0x04001417 RID: 5143
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ParameterListSyntax parameterList;

		// Token: 0x04001418 RID: 5144
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private BaseListSyntax baseList;

		// Token: 0x04001419 RID: 5145
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode constraintClauses;

		// Token: 0x0400141A RID: 5146
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode members;
	}
}
