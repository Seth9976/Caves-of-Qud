using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000287 RID: 647
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public abstract class TypeDeclarationSyntax : BaseTypeDeclarationSyntax
	{
		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x06003D3B RID: 15675 RVA: 0x00108350 File Offset: 0x00106550
		public int Arity
		{
			get
			{
				if (this.TypeParameterList != null)
				{
					return this.TypeParameterList.Parameters.Count;
				}
				return 0;
			}
		}

		// Token: 0x06003D3C RID: 15676 RVA: 0x0010837A File Offset: 0x0010657A
		public new TypeDeclarationSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return (TypeDeclarationSyntax)this.AddAttributeListsCore(items);
		}

		// Token: 0x06003D3D RID: 15677 RVA: 0x00108388 File Offset: 0x00106588
		public new TypeDeclarationSyntax AddModifiers(params SyntaxToken[] items)
		{
			return (TypeDeclarationSyntax)this.AddModifiersCore(items);
		}

		// Token: 0x06003D3E RID: 15678 RVA: 0x00108396 File Offset: 0x00106596
		public new TypeDeclarationSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return (TypeDeclarationSyntax)this.WithAttributeListsCore(attributeLists);
		}

		// Token: 0x06003D3F RID: 15679 RVA: 0x001083A4 File Offset: 0x001065A4
		public new TypeDeclarationSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return (TypeDeclarationSyntax)this.WithModifiersCore(modifiers);
		}

		// Token: 0x06003D40 RID: 15680 RVA: 0x001083B2 File Offset: 0x001065B2
		internal TypeDeclarationSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x06003D41 RID: 15681
		public abstract SyntaxToken Keyword { get; }

		// Token: 0x06003D42 RID: 15682 RVA: 0x001083BD File Offset: 0x001065BD
		public TypeDeclarationSyntax WithKeyword(SyntaxToken keyword)
		{
			return this.WithKeywordCore(keyword);
		}

		// Token: 0x06003D43 RID: 15683
		internal abstract TypeDeclarationSyntax WithKeywordCore(SyntaxToken keyword);

		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x06003D44 RID: 15684
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public abstract TypeParameterListSyntax TypeParameterList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x06003D45 RID: 15685 RVA: 0x001083C6 File Offset: 0x001065C6
		public TypeDeclarationSyntax WithTypeParameterList([global::System.Runtime.CompilerServices.Nullable(2)] TypeParameterListSyntax typeParameterList)
		{
			return this.WithTypeParameterListCore(typeParameterList);
		}

		// Token: 0x06003D46 RID: 15686
		internal abstract TypeDeclarationSyntax WithTypeParameterListCore([global::System.Runtime.CompilerServices.Nullable(2)] TypeParameterListSyntax typeParameterList);

		// Token: 0x06003D47 RID: 15687 RVA: 0x001083CF File Offset: 0x001065CF
		public TypeDeclarationSyntax AddTypeParameterListParameters(params TypeParameterSyntax[] items)
		{
			return this.AddTypeParameterListParametersCore(items);
		}

		// Token: 0x06003D48 RID: 15688
		internal abstract TypeDeclarationSyntax AddTypeParameterListParametersCore(params TypeParameterSyntax[] items);

		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x06003D49 RID: 15689
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public abstract SyntaxList<TypeParameterConstraintClauseSyntax> ConstraintClauses
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06003D4A RID: 15690 RVA: 0x001083D8 File Offset: 0x001065D8
		public TypeDeclarationSyntax WithConstraintClauses([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses)
		{
			return this.WithConstraintClausesCore(constraintClauses);
		}

		// Token: 0x06003D4B RID: 15691
		internal abstract TypeDeclarationSyntax WithConstraintClausesCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses);

		// Token: 0x06003D4C RID: 15692 RVA: 0x001083E1 File Offset: 0x001065E1
		public TypeDeclarationSyntax AddConstraintClauses(params TypeParameterConstraintClauseSyntax[] items)
		{
			return this.AddConstraintClausesCore(items);
		}

		// Token: 0x06003D4D RID: 15693
		internal abstract TypeDeclarationSyntax AddConstraintClausesCore(params TypeParameterConstraintClauseSyntax[] items);

		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x06003D4E RID: 15694
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public abstract SyntaxList<MemberDeclarationSyntax> Members
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06003D4F RID: 15695 RVA: 0x001083EA File Offset: 0x001065EA
		public TypeDeclarationSyntax WithMembers([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members)
		{
			return this.WithMembersCore(members);
		}

		// Token: 0x06003D50 RID: 15696
		internal abstract TypeDeclarationSyntax WithMembersCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members);

		// Token: 0x06003D51 RID: 15697 RVA: 0x001083F3 File Offset: 0x001065F3
		public TypeDeclarationSyntax AddMembers(params MemberDeclarationSyntax[] items)
		{
			return this.AddMembersCore(items);
		}

		// Token: 0x06003D52 RID: 15698
		internal abstract TypeDeclarationSyntax AddMembersCore(params MemberDeclarationSyntax[] items);

		// Token: 0x06003D53 RID: 15699 RVA: 0x001083FC File Offset: 0x001065FC
		public new TypeDeclarationSyntax WithIdentifier(SyntaxToken identifier)
		{
			return (TypeDeclarationSyntax)this.WithIdentifierCore(identifier);
		}

		// Token: 0x06003D54 RID: 15700 RVA: 0x0010840A File Offset: 0x0010660A
		public new TypeDeclarationSyntax WithBaseList([global::System.Runtime.CompilerServices.Nullable(2)] BaseListSyntax baseList)
		{
			return (TypeDeclarationSyntax)this.WithBaseListCore(baseList);
		}

		// Token: 0x06003D55 RID: 15701 RVA: 0x00108418 File Offset: 0x00106618
		public new TypeDeclarationSyntax WithOpenBraceToken(SyntaxToken openBraceToken)
		{
			return (TypeDeclarationSyntax)this.WithOpenBraceTokenCore(openBraceToken);
		}

		// Token: 0x06003D56 RID: 15702 RVA: 0x00108426 File Offset: 0x00106626
		public new TypeDeclarationSyntax WithCloseBraceToken(SyntaxToken closeBraceToken)
		{
			return (TypeDeclarationSyntax)this.WithCloseBraceTokenCore(closeBraceToken);
		}

		// Token: 0x06003D57 RID: 15703 RVA: 0x00108434 File Offset: 0x00106634
		public new TypeDeclarationSyntax WithSemicolonToken(SyntaxToken semicolonToken)
		{
			return (TypeDeclarationSyntax)this.WithSemicolonTokenCore(semicolonToken);
		}

		// Token: 0x06003D58 RID: 15704 RVA: 0x00108442 File Offset: 0x00106642
		public new BaseTypeDeclarationSyntax AddBaseListTypes(params BaseTypeSyntax[] items)
		{
			return this.AddBaseListTypesCore(items);
		}
	}
}
