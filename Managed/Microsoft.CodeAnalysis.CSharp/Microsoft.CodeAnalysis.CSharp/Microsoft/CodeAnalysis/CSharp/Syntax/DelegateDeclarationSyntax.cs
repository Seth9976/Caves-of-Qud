using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000256 RID: 598
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class DelegateDeclarationSyntax : MemberDeclarationSyntax
	{
		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x060038D8 RID: 14552 RVA: 0x000FBD80 File Offset: 0x000F9F80
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

		// Token: 0x060038D9 RID: 14553 RVA: 0x000FBDAA File Offset: 0x000F9FAA
		internal DelegateDeclarationSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x060038DA RID: 14554 RVA: 0x000FBDB5 File Offset: 0x000F9FB5
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x060038DB RID: 14555 RVA: 0x000FBDCC File Offset: 0x000F9FCC
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

		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x060038DC RID: 14556 RVA: 0x000FBE08 File Offset: 0x000FA008
		public SyntaxToken DelegateKeyword
		{
			get
			{
				return new SyntaxToken(this, ((DelegateDeclarationSyntax)base.Green).delegateKeyword, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x060038DD RID: 14557 RVA: 0x000FBE2E File Offset: 0x000FA02E
		public TypeSyntax ReturnType
		{
			get
			{
				return base.GetRed<TypeSyntax>(ref this.returnType, 3);
			}
		}

		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x060038DE RID: 14558 RVA: 0x000FBE3D File Offset: 0x000FA03D
		public SyntaxToken Identifier
		{
			get
			{
				return new SyntaxToken(this, ((DelegateDeclarationSyntax)base.Green).identifier, this.GetChildPosition(4), base.GetChildIndex(4));
			}
		}

		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x060038DF RID: 14559 RVA: 0x000FBE63 File Offset: 0x000FA063
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public TypeParameterListSyntax TypeParameterList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<TypeParameterListSyntax>(ref this.typeParameterList, 5);
			}
		}

		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x060038E0 RID: 14560 RVA: 0x000FBE72 File Offset: 0x000FA072
		public ParameterListSyntax ParameterList
		{
			get
			{
				return base.GetRed<ParameterListSyntax>(ref this.parameterList, 6);
			}
		}

		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x060038E1 RID: 14561 RVA: 0x000FBE81 File Offset: 0x000FA081
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<TypeParameterConstraintClauseSyntax> ConstraintClauses
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<TypeParameterConstraintClauseSyntax>(base.GetRed(ref this.constraintClauses, 7));
			}
		}

		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x060038E2 RID: 14562 RVA: 0x000FBE95 File Offset: 0x000FA095
		public SyntaxToken SemicolonToken
		{
			get
			{
				return new SyntaxToken(this, ((DelegateDeclarationSyntax)base.Green).semicolonToken, this.GetChildPosition(8), base.GetChildIndex(8));
			}
		}

		// Token: 0x060038E3 RID: 14563 RVA: 0x000FBEBC File Offset: 0x000FA0BC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			switch (index)
			{
			case 0:
				return base.GetRedAtZero(ref this.attributeLists);
			case 3:
				return base.GetRed<TypeSyntax>(ref this.returnType, 3);
			case 5:
				return base.GetRed<TypeParameterListSyntax>(ref this.typeParameterList, 5);
			case 6:
				return base.GetRed<ParameterListSyntax>(ref this.parameterList, 6);
			case 7:
				return base.GetRed(ref this.constraintClauses, 7);
			}
			return null;
		}

		// Token: 0x060038E4 RID: 14564 RVA: 0x000FBF44 File Offset: 0x000FA144
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			switch (index)
			{
			case 0:
				return this.attributeLists;
			case 3:
				return this.returnType;
			case 5:
				return this.typeParameterList;
			case 6:
				return this.parameterList;
			case 7:
				return this.constraintClauses;
			}
			return null;
		}

		// Token: 0x060038E5 RID: 14565 RVA: 0x000FBFA9 File Offset: 0x000FA1A9
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitDelegateDeclaration(this);
		}

		// Token: 0x060038E6 RID: 14566 RVA: 0x000FBFB2 File Offset: 0x000FA1B2
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitDelegateDeclaration(this);
		}

		// Token: 0x060038E7 RID: 14567 RVA: 0x000FBFBC File Offset: 0x000FA1BC
		public DelegateDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken delegateKeyword, TypeSyntax returnType, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken semicolonToken)
		{
			if (!(attributeLists != this.AttributeLists) && !(modifiers != this.Modifiers) && !(delegateKeyword != this.DelegateKeyword) && returnType == this.ReturnType && !(identifier != this.Identifier) && typeParameterList == this.TypeParameterList && parameterList == this.ParameterList && !(constraintClauses != this.ConstraintClauses) && !(semicolonToken != this.SemicolonToken))
			{
				return this;
			}
			DelegateDeclarationSyntax delegateDeclarationSyntax = SyntaxFactory.DelegateDeclaration(attributeLists, modifiers, delegateKeyword, returnType, identifier, typeParameterList, parameterList, constraintClauses, semicolonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return delegateDeclarationSyntax;
			}
			return delegateDeclarationSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060038E8 RID: 14568 RVA: 0x000FC06C File Offset: 0x000FA26C
		internal override MemberDeclarationSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x060038E9 RID: 14569 RVA: 0x000FC078 File Offset: 0x000FA278
		public new DelegateDeclarationSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.Modifiers, this.DelegateKeyword, this.ReturnType, this.Identifier, this.TypeParameterList, this.ParameterList, this.ConstraintClauses, this.SemicolonToken);
		}

		// Token: 0x060038EA RID: 14570 RVA: 0x000FC0BC File Offset: 0x000FA2BC
		internal override MemberDeclarationSyntax WithModifiersCore(SyntaxTokenList modifiers)
		{
			return this.WithModifiers(modifiers);
		}

		// Token: 0x060038EB RID: 14571 RVA: 0x000FC0C8 File Offset: 0x000FA2C8
		public new DelegateDeclarationSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return this.Update(this.AttributeLists, modifiers, this.DelegateKeyword, this.ReturnType, this.Identifier, this.TypeParameterList, this.ParameterList, this.ConstraintClauses, this.SemicolonToken);
		}

		// Token: 0x060038EC RID: 14572 RVA: 0x000FC10C File Offset: 0x000FA30C
		public DelegateDeclarationSyntax WithDelegateKeyword(SyntaxToken delegateKeyword)
		{
			return this.Update(this.AttributeLists, this.Modifiers, delegateKeyword, this.ReturnType, this.Identifier, this.TypeParameterList, this.ParameterList, this.ConstraintClauses, this.SemicolonToken);
		}

		// Token: 0x060038ED RID: 14573 RVA: 0x000FC150 File Offset: 0x000FA350
		public DelegateDeclarationSyntax WithReturnType(TypeSyntax returnType)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.DelegateKeyword, returnType, this.Identifier, this.TypeParameterList, this.ParameterList, this.ConstraintClauses, this.SemicolonToken);
		}

		// Token: 0x060038EE RID: 14574 RVA: 0x000FC194 File Offset: 0x000FA394
		public DelegateDeclarationSyntax WithIdentifier(SyntaxToken identifier)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.DelegateKeyword, this.ReturnType, identifier, this.TypeParameterList, this.ParameterList, this.ConstraintClauses, this.SemicolonToken);
		}

		// Token: 0x060038EF RID: 14575 RVA: 0x000FC1D8 File Offset: 0x000FA3D8
		public DelegateDeclarationSyntax WithTypeParameterList([global::System.Runtime.CompilerServices.Nullable(2)] TypeParameterListSyntax typeParameterList)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.DelegateKeyword, this.ReturnType, this.Identifier, typeParameterList, this.ParameterList, this.ConstraintClauses, this.SemicolonToken);
		}

		// Token: 0x060038F0 RID: 14576 RVA: 0x000FC21C File Offset: 0x000FA41C
		public DelegateDeclarationSyntax WithParameterList(ParameterListSyntax parameterList)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.DelegateKeyword, this.ReturnType, this.Identifier, this.TypeParameterList, parameterList, this.ConstraintClauses, this.SemicolonToken);
		}

		// Token: 0x060038F1 RID: 14577 RVA: 0x000FC260 File Offset: 0x000FA460
		public DelegateDeclarationSyntax WithConstraintClauses([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.DelegateKeyword, this.ReturnType, this.Identifier, this.TypeParameterList, this.ParameterList, constraintClauses, this.SemicolonToken);
		}

		// Token: 0x060038F2 RID: 14578 RVA: 0x000FC2A4 File Offset: 0x000FA4A4
		public DelegateDeclarationSyntax WithSemicolonToken(SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.DelegateKeyword, this.ReturnType, this.Identifier, this.TypeParameterList, this.ParameterList, this.ConstraintClauses, semicolonToken);
		}

		// Token: 0x060038F3 RID: 14579 RVA: 0x000FC2E8 File Offset: 0x000FA4E8
		internal override MemberDeclarationSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x060038F4 RID: 14580 RVA: 0x000FC2F4 File Offset: 0x000FA4F4
		public new DelegateDeclarationSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x060038F5 RID: 14581 RVA: 0x000FC316 File Offset: 0x000FA516
		internal override MemberDeclarationSyntax AddModifiersCore(params SyntaxToken[] items)
		{
			return this.AddModifiers(items);
		}

		// Token: 0x060038F6 RID: 14582 RVA: 0x000FC320 File Offset: 0x000FA520
		public new DelegateDeclarationSyntax AddModifiers(params SyntaxToken[] items)
		{
			return this.WithModifiers(this.Modifiers.AddRange(items));
		}

		// Token: 0x060038F7 RID: 14583 RVA: 0x000FC344 File Offset: 0x000FA544
		public DelegateDeclarationSyntax AddTypeParameterListParameters(params TypeParameterSyntax[] items)
		{
			TypeParameterListSyntax typeParameterListSyntax;
			if ((typeParameterListSyntax = this.TypeParameterList) == null)
			{
				typeParameterListSyntax = SyntaxFactory.TypeParameterList(default(SeparatedSyntaxList<TypeParameterSyntax>));
			}
			TypeParameterListSyntax typeParameterListSyntax2 = typeParameterListSyntax;
			return this.WithTypeParameterList(typeParameterListSyntax2.WithParameters(typeParameterListSyntax2.Parameters.AddRange(items)));
		}

		// Token: 0x060038F8 RID: 14584 RVA: 0x000FC388 File Offset: 0x000FA588
		public DelegateDeclarationSyntax AddParameterListParameters(params ParameterSyntax[] items)
		{
			return this.WithParameterList(this.ParameterList.WithParameters(this.ParameterList.Parameters.AddRange(items)));
		}

		// Token: 0x060038F9 RID: 14585 RVA: 0x000FC3BC File Offset: 0x000FA5BC
		public DelegateDeclarationSyntax AddConstraintClauses(params TypeParameterConstraintClauseSyntax[] items)
		{
			return this.WithConstraintClauses(this.ConstraintClauses.AddRange(items));
		}

		// Token: 0x040013AF RID: 5039
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x040013B0 RID: 5040
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax returnType;

		// Token: 0x040013B1 RID: 5041
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeParameterListSyntax typeParameterList;

		// Token: 0x040013B2 RID: 5042
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ParameterListSyntax parameterList;

		// Token: 0x040013B3 RID: 5043
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode constraintClauses;
	}
}
