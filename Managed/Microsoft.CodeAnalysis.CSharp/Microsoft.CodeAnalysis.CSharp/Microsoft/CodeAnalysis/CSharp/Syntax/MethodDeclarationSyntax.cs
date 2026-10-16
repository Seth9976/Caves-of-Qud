using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200026D RID: 621
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class MethodDeclarationSyntax : BaseMethodDeclarationSyntax
	{
		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x06003B1E RID: 15134 RVA: 0x00101E54 File Offset: 0x00100054
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

		// Token: 0x06003B1F RID: 15135 RVA: 0x00101E7E File Offset: 0x0010007E
		internal MethodDeclarationSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x06003B20 RID: 15136 RVA: 0x00101E89 File Offset: 0x00100089
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x06003B21 RID: 15137 RVA: 0x00101EA0 File Offset: 0x001000A0
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

		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x06003B22 RID: 15138 RVA: 0x00101EDC File Offset: 0x001000DC
		public TypeSyntax ReturnType
		{
			get
			{
				return base.GetRed<TypeSyntax>(ref this.returnType, 2);
			}
		}

		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x06003B23 RID: 15139 RVA: 0x00101EEB File Offset: 0x001000EB
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ExplicitInterfaceSpecifierSyntax ExplicitInterfaceSpecifier
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<ExplicitInterfaceSpecifierSyntax>(ref this.explicitInterfaceSpecifier, 3);
			}
		}

		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x06003B24 RID: 15140 RVA: 0x00101EFA File Offset: 0x001000FA
		public SyntaxToken Identifier
		{
			get
			{
				return new SyntaxToken(this, ((MethodDeclarationSyntax)base.Green).identifier, this.GetChildPosition(4), base.GetChildIndex(4));
			}
		}

		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x06003B25 RID: 15141 RVA: 0x00101F20 File Offset: 0x00100120
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public TypeParameterListSyntax TypeParameterList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<TypeParameterListSyntax>(ref this.typeParameterList, 5);
			}
		}

		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x06003B26 RID: 15142 RVA: 0x00101F2F File Offset: 0x0010012F
		public override ParameterListSyntax ParameterList
		{
			get
			{
				return base.GetRed<ParameterListSyntax>(ref this.parameterList, 6);
			}
		}

		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x06003B27 RID: 15143 RVA: 0x00101F3E File Offset: 0x0010013E
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<TypeParameterConstraintClauseSyntax> ConstraintClauses
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<TypeParameterConstraintClauseSyntax>(base.GetRed(ref this.constraintClauses, 7));
			}
		}

		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x06003B28 RID: 15144 RVA: 0x00101F52 File Offset: 0x00100152
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override BlockSyntax Body
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<BlockSyntax>(ref this.body, 8);
			}
		}

		// Token: 0x17000F8D RID: 3981
		// (get) Token: 0x06003B29 RID: 15145 RVA: 0x00101F61 File Offset: 0x00100161
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override ArrowExpressionClauseSyntax ExpressionBody
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<ArrowExpressionClauseSyntax>(ref this.expressionBody, 9);
			}
		}

		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x06003B2A RID: 15146 RVA: 0x00101F74 File Offset: 0x00100174
		public override SyntaxToken SemicolonToken
		{
			get
			{
				SyntaxToken semicolonToken = ((MethodDeclarationSyntax)base.Green).semicolonToken;
				if (semicolonToken == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, semicolonToken, this.GetChildPosition(10), base.GetChildIndex(10));
			}
		}

		// Token: 0x06003B2B RID: 15147 RVA: 0x00101FB8 File Offset: 0x001001B8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			switch (index)
			{
			case 0:
				return base.GetRedAtZero(ref this.attributeLists);
			case 2:
				return base.GetRed<TypeSyntax>(ref this.returnType, 2);
			case 3:
				return base.GetRed<ExplicitInterfaceSpecifierSyntax>(ref this.explicitInterfaceSpecifier, 3);
			case 5:
				return base.GetRed<TypeParameterListSyntax>(ref this.typeParameterList, 5);
			case 6:
				return base.GetRed<ParameterListSyntax>(ref this.parameterList, 6);
			case 7:
				return base.GetRed(ref this.constraintClauses, 7);
			case 8:
				return base.GetRed<BlockSyntax>(ref this.body, 8);
			case 9:
				return base.GetRed<ArrowExpressionClauseSyntax>(ref this.expressionBody, 9);
			}
			return null;
		}

		// Token: 0x06003B2C RID: 15148 RVA: 0x0010207C File Offset: 0x0010027C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			switch (index)
			{
			case 0:
				return this.attributeLists;
			case 2:
				return this.returnType;
			case 3:
				return this.explicitInterfaceSpecifier;
			case 5:
				return this.typeParameterList;
			case 6:
				return this.parameterList;
			case 7:
				return this.constraintClauses;
			case 8:
				return this.body;
			case 9:
				return this.expressionBody;
			}
			return null;
		}

		// Token: 0x06003B2D RID: 15149 RVA: 0x00102104 File Offset: 0x00100304
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitMethodDeclaration(this);
		}

		// Token: 0x06003B2E RID: 15150 RVA: 0x0010210D File Offset: 0x0010030D
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitMethodDeclaration(this);
		}

		// Token: 0x06003B2F RID: 15151 RVA: 0x00102118 File Offset: 0x00100318
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public MethodDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSyntax returnType, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, [global::System.Runtime.CompilerServices.Nullable(1)] ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
		{
			if (!(attributeLists != this.AttributeLists) && !(modifiers != this.Modifiers) && returnType == this.ReturnType && explicitInterfaceSpecifier == this.ExplicitInterfaceSpecifier && !(identifier != this.Identifier) && typeParameterList == this.TypeParameterList && parameterList == this.ParameterList && !(constraintClauses != this.ConstraintClauses) && body == this.Body && expressionBody == this.ExpressionBody && !(semicolonToken != this.SemicolonToken))
			{
				return this;
			}
			MethodDeclarationSyntax methodDeclarationSyntax = SyntaxFactory.MethodDeclaration(attributeLists, modifiers, returnType, explicitInterfaceSpecifier, identifier, typeParameterList, parameterList, constraintClauses, body, expressionBody, semicolonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return methodDeclarationSyntax;
			}
			return methodDeclarationSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003B30 RID: 15152 RVA: 0x001021DB File Offset: 0x001003DB
		internal override MemberDeclarationSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x06003B31 RID: 15153 RVA: 0x001021E4 File Offset: 0x001003E4
		public new MethodDeclarationSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.Modifiers, this.ReturnType, this.ExplicitInterfaceSpecifier, this.Identifier, this.TypeParameterList, this.ParameterList, this.ConstraintClauses, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06003B32 RID: 15154 RVA: 0x00102234 File Offset: 0x00100434
		internal override MemberDeclarationSyntax WithModifiersCore(SyntaxTokenList modifiers)
		{
			return this.WithModifiers(modifiers);
		}

		// Token: 0x06003B33 RID: 15155 RVA: 0x00102240 File Offset: 0x00100440
		public new MethodDeclarationSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return this.Update(this.AttributeLists, modifiers, this.ReturnType, this.ExplicitInterfaceSpecifier, this.Identifier, this.TypeParameterList, this.ParameterList, this.ConstraintClauses, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06003B34 RID: 15156 RVA: 0x00102290 File Offset: 0x00100490
		public MethodDeclarationSyntax WithReturnType(TypeSyntax returnType)
		{
			return this.Update(this.AttributeLists, this.Modifiers, returnType, this.ExplicitInterfaceSpecifier, this.Identifier, this.TypeParameterList, this.ParameterList, this.ConstraintClauses, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06003B35 RID: 15157 RVA: 0x001022E0 File Offset: 0x001004E0
		public MethodDeclarationSyntax WithExplicitInterfaceSpecifier([global::System.Runtime.CompilerServices.Nullable(2)] ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.ReturnType, explicitInterfaceSpecifier, this.Identifier, this.TypeParameterList, this.ParameterList, this.ConstraintClauses, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06003B36 RID: 15158 RVA: 0x00102330 File Offset: 0x00100530
		public MethodDeclarationSyntax WithIdentifier(SyntaxToken identifier)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.ReturnType, this.ExplicitInterfaceSpecifier, identifier, this.TypeParameterList, this.ParameterList, this.ConstraintClauses, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06003B37 RID: 15159 RVA: 0x00102380 File Offset: 0x00100580
		public MethodDeclarationSyntax WithTypeParameterList([global::System.Runtime.CompilerServices.Nullable(2)] TypeParameterListSyntax typeParameterList)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.ReturnType, this.ExplicitInterfaceSpecifier, this.Identifier, typeParameterList, this.ParameterList, this.ConstraintClauses, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06003B38 RID: 15160 RVA: 0x001023D0 File Offset: 0x001005D0
		internal override BaseMethodDeclarationSyntax WithParameterListCore(ParameterListSyntax parameterList)
		{
			return this.WithParameterList(parameterList);
		}

		// Token: 0x06003B39 RID: 15161 RVA: 0x001023DC File Offset: 0x001005DC
		public new MethodDeclarationSyntax WithParameterList(ParameterListSyntax parameterList)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.ReturnType, this.ExplicitInterfaceSpecifier, this.Identifier, this.TypeParameterList, parameterList, this.ConstraintClauses, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06003B3A RID: 15162 RVA: 0x0010242C File Offset: 0x0010062C
		public MethodDeclarationSyntax WithConstraintClauses([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.ReturnType, this.ExplicitInterfaceSpecifier, this.Identifier, this.TypeParameterList, this.ParameterList, constraintClauses, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06003B3B RID: 15163 RVA: 0x0010247C File Offset: 0x0010067C
		internal override BaseMethodDeclarationSyntax WithBodyCore([global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax body)
		{
			return this.WithBody(body);
		}

		// Token: 0x06003B3C RID: 15164 RVA: 0x00102488 File Offset: 0x00100688
		public new MethodDeclarationSyntax WithBody([global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax body)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.ReturnType, this.ExplicitInterfaceSpecifier, this.Identifier, this.TypeParameterList, this.ParameterList, this.ConstraintClauses, body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06003B3D RID: 15165 RVA: 0x001024D8 File Offset: 0x001006D8
		internal override BaseMethodDeclarationSyntax WithExpressionBodyCore([global::System.Runtime.CompilerServices.Nullable(2)] ArrowExpressionClauseSyntax expressionBody)
		{
			return this.WithExpressionBody(expressionBody);
		}

		// Token: 0x06003B3E RID: 15166 RVA: 0x001024E4 File Offset: 0x001006E4
		public new MethodDeclarationSyntax WithExpressionBody([global::System.Runtime.CompilerServices.Nullable(2)] ArrowExpressionClauseSyntax expressionBody)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.ReturnType, this.ExplicitInterfaceSpecifier, this.Identifier, this.TypeParameterList, this.ParameterList, this.ConstraintClauses, this.Body, expressionBody, this.SemicolonToken);
		}

		// Token: 0x06003B3F RID: 15167 RVA: 0x00102534 File Offset: 0x00100734
		internal override BaseMethodDeclarationSyntax WithSemicolonTokenCore(SyntaxToken semicolonToken)
		{
			return this.WithSemicolonToken(semicolonToken);
		}

		// Token: 0x06003B40 RID: 15168 RVA: 0x00102540 File Offset: 0x00100740
		public new MethodDeclarationSyntax WithSemicolonToken(SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.ReturnType, this.ExplicitInterfaceSpecifier, this.Identifier, this.TypeParameterList, this.ParameterList, this.ConstraintClauses, this.Body, this.ExpressionBody, semicolonToken);
		}

		// Token: 0x06003B41 RID: 15169 RVA: 0x00102590 File Offset: 0x00100790
		internal override MemberDeclarationSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x06003B42 RID: 15170 RVA: 0x0010259C File Offset: 0x0010079C
		public new MethodDeclarationSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x06003B43 RID: 15171 RVA: 0x001025BE File Offset: 0x001007BE
		internal override MemberDeclarationSyntax AddModifiersCore(params SyntaxToken[] items)
		{
			return this.AddModifiers(items);
		}

		// Token: 0x06003B44 RID: 15172 RVA: 0x001025C8 File Offset: 0x001007C8
		public new MethodDeclarationSyntax AddModifiers(params SyntaxToken[] items)
		{
			return this.WithModifiers(this.Modifiers.AddRange(items));
		}

		// Token: 0x06003B45 RID: 15173 RVA: 0x001025EC File Offset: 0x001007EC
		public MethodDeclarationSyntax AddTypeParameterListParameters(params TypeParameterSyntax[] items)
		{
			TypeParameterListSyntax typeParameterListSyntax;
			if ((typeParameterListSyntax = this.TypeParameterList) == null)
			{
				typeParameterListSyntax = SyntaxFactory.TypeParameterList(default(SeparatedSyntaxList<TypeParameterSyntax>));
			}
			TypeParameterListSyntax typeParameterListSyntax2 = typeParameterListSyntax;
			return this.WithTypeParameterList(typeParameterListSyntax2.WithParameters(typeParameterListSyntax2.Parameters.AddRange(items)));
		}

		// Token: 0x06003B46 RID: 15174 RVA: 0x0010262D File Offset: 0x0010082D
		internal override BaseMethodDeclarationSyntax AddParameterListParametersCore(params ParameterSyntax[] items)
		{
			return this.AddParameterListParameters(items);
		}

		// Token: 0x06003B47 RID: 15175 RVA: 0x00102638 File Offset: 0x00100838
		public new MethodDeclarationSyntax AddParameterListParameters(params ParameterSyntax[] items)
		{
			return this.WithParameterList(this.ParameterList.WithParameters(this.ParameterList.Parameters.AddRange(items)));
		}

		// Token: 0x06003B48 RID: 15176 RVA: 0x0010266C File Offset: 0x0010086C
		public MethodDeclarationSyntax AddConstraintClauses(params TypeParameterConstraintClauseSyntax[] items)
		{
			return this.WithConstraintClauses(this.ConstraintClauses.AddRange(items));
		}

		// Token: 0x06003B49 RID: 15177 RVA: 0x0010268E File Offset: 0x0010088E
		internal override BaseMethodDeclarationSyntax AddBodyAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddBodyAttributeLists(items);
		}

		// Token: 0x06003B4A RID: 15178 RVA: 0x00102698 File Offset: 0x00100898
		public new MethodDeclarationSyntax AddBodyAttributeLists(params AttributeListSyntax[] items)
		{
			BlockSyntax blockSyntax;
			if ((blockSyntax = this.Body) == null)
			{
				blockSyntax = SyntaxFactory.Block(default(SyntaxList<StatementSyntax>));
			}
			BlockSyntax blockSyntax2 = blockSyntax;
			return this.WithBody(blockSyntax2.WithAttributeLists(blockSyntax2.AttributeLists.AddRange(items)));
		}

		// Token: 0x06003B4B RID: 15179 RVA: 0x001026D9 File Offset: 0x001008D9
		internal override BaseMethodDeclarationSyntax AddBodyStatementsCore(params StatementSyntax[] items)
		{
			return this.AddBodyStatements(items);
		}

		// Token: 0x06003B4C RID: 15180 RVA: 0x001026E4 File Offset: 0x001008E4
		public new MethodDeclarationSyntax AddBodyStatements(params StatementSyntax[] items)
		{
			BlockSyntax blockSyntax;
			if ((blockSyntax = this.Body) == null)
			{
				blockSyntax = SyntaxFactory.Block(default(SyntaxList<StatementSyntax>));
			}
			BlockSyntax blockSyntax2 = blockSyntax;
			return this.WithBody(blockSyntax2.WithStatements(blockSyntax2.Statements.AddRange(items)));
		}

		// Token: 0x040013F1 RID: 5105
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x040013F2 RID: 5106
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax returnType;

		// Token: 0x040013F3 RID: 5107
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier;

		// Token: 0x040013F4 RID: 5108
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeParameterListSyntax typeParameterList;

		// Token: 0x040013F5 RID: 5109
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ParameterListSyntax parameterList;

		// Token: 0x040013F6 RID: 5110
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode constraintClauses;

		// Token: 0x040013F7 RID: 5111
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private BlockSyntax body;

		// Token: 0x040013F8 RID: 5112
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ArrowExpressionClauseSyntax expressionBody;
	}
}
