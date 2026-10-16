using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000257 RID: 599
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class DestructorDeclarationSyntax : BaseMethodDeclarationSyntax
	{
		// Token: 0x060038FA RID: 14586 RVA: 0x000FC3E0 File Offset: 0x000FA5E0
		public DestructorDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken tildeToken, SyntaxToken identifier, ParameterListSyntax parameterList, BlockSyntax body, SyntaxToken semicolonToken)
		{
			return this.Update(attributeLists, modifiers, tildeToken, identifier, parameterList, body, null, semicolonToken);
		}

		// Token: 0x060038FB RID: 14587 RVA: 0x000FC3FF File Offset: 0x000FA5FF
		internal DestructorDeclarationSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x060038FC RID: 14588 RVA: 0x000FC40A File Offset: 0x000FA60A
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x060038FD RID: 14589 RVA: 0x000FC420 File Offset: 0x000FA620
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

		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x060038FE RID: 14590 RVA: 0x000FC45C File Offset: 0x000FA65C
		public SyntaxToken TildeToken
		{
			get
			{
				return new SyntaxToken(this, ((DestructorDeclarationSyntax)base.Green).tildeToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x060038FF RID: 14591 RVA: 0x000FC482 File Offset: 0x000FA682
		public SyntaxToken Identifier
		{
			get
			{
				return new SyntaxToken(this, ((DestructorDeclarationSyntax)base.Green).identifier, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x06003900 RID: 14592 RVA: 0x000FC4A8 File Offset: 0x000FA6A8
		public override ParameterListSyntax ParameterList
		{
			get
			{
				return base.GetRed<ParameterListSyntax>(ref this.parameterList, 4);
			}
		}

		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x06003901 RID: 14593 RVA: 0x000FC4B7 File Offset: 0x000FA6B7
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override BlockSyntax Body
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<BlockSyntax>(ref this.body, 5);
			}
		}

		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x06003902 RID: 14594 RVA: 0x000FC4C6 File Offset: 0x000FA6C6
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override ArrowExpressionClauseSyntax ExpressionBody
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<ArrowExpressionClauseSyntax>(ref this.expressionBody, 6);
			}
		}

		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x06003903 RID: 14595 RVA: 0x000FC4D8 File Offset: 0x000FA6D8
		public override SyntaxToken SemicolonToken
		{
			get
			{
				SyntaxToken semicolonToken = ((DestructorDeclarationSyntax)base.Green).semicolonToken;
				if (semicolonToken == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, semicolonToken, this.GetChildPosition(7), base.GetChildIndex(7));
			}
		}

		// Token: 0x06003904 RID: 14596 RVA: 0x000FC518 File Offset: 0x000FA718
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			switch (index)
			{
			case 0:
				return base.GetRedAtZero(ref this.attributeLists);
			case 4:
				return base.GetRed<ParameterListSyntax>(ref this.parameterList, 4);
			case 5:
				return base.GetRed<BlockSyntax>(ref this.body, 5);
			case 6:
				return base.GetRed<ArrowExpressionClauseSyntax>(ref this.expressionBody, 6);
			}
			return null;
		}

		// Token: 0x06003905 RID: 14597 RVA: 0x000FC58C File Offset: 0x000FA78C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			switch (index)
			{
			case 0:
				return this.attributeLists;
			case 4:
				return this.parameterList;
			case 5:
				return this.body;
			case 6:
				return this.expressionBody;
			}
			return null;
		}

		// Token: 0x06003906 RID: 14598 RVA: 0x000FC5E4 File Offset: 0x000FA7E4
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitDestructorDeclaration(this);
		}

		// Token: 0x06003907 RID: 14599 RVA: 0x000FC5ED File Offset: 0x000FA7ED
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitDestructorDeclaration(this);
		}

		// Token: 0x06003908 RID: 14600 RVA: 0x000FC5F8 File Offset: 0x000FA7F8
		public DestructorDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken tildeToken, SyntaxToken identifier, ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax body, [global::System.Runtime.CompilerServices.Nullable(2)] ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
		{
			if (!(attributeLists != this.AttributeLists) && !(modifiers != this.Modifiers) && !(tildeToken != this.TildeToken) && !(identifier != this.Identifier) && parameterList == this.ParameterList && body == this.Body && expressionBody == this.ExpressionBody && !(semicolonToken != this.SemicolonToken))
			{
				return this;
			}
			DestructorDeclarationSyntax destructorDeclarationSyntax = SyntaxFactory.DestructorDeclaration(attributeLists, modifiers, tildeToken, identifier, parameterList, body, expressionBody, semicolonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return destructorDeclarationSyntax;
			}
			return destructorDeclarationSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003909 RID: 14601 RVA: 0x000FC697 File Offset: 0x000FA897
		internal override MemberDeclarationSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x0600390A RID: 14602 RVA: 0x000FC6A0 File Offset: 0x000FA8A0
		public new DestructorDeclarationSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.Modifiers, this.TildeToken, this.Identifier, this.ParameterList, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x0600390B RID: 14603 RVA: 0x000FC6DE File Offset: 0x000FA8DE
		internal override MemberDeclarationSyntax WithModifiersCore(SyntaxTokenList modifiers)
		{
			return this.WithModifiers(modifiers);
		}

		// Token: 0x0600390C RID: 14604 RVA: 0x000FC6E8 File Offset: 0x000FA8E8
		public new DestructorDeclarationSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return this.Update(this.AttributeLists, modifiers, this.TildeToken, this.Identifier, this.ParameterList, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x0600390D RID: 14605 RVA: 0x000FC728 File Offset: 0x000FA928
		public DestructorDeclarationSyntax WithTildeToken(SyntaxToken tildeToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, tildeToken, this.Identifier, this.ParameterList, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x0600390E RID: 14606 RVA: 0x000FC768 File Offset: 0x000FA968
		public DestructorDeclarationSyntax WithIdentifier(SyntaxToken identifier)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.TildeToken, identifier, this.ParameterList, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x0600390F RID: 14607 RVA: 0x000FC7A6 File Offset: 0x000FA9A6
		internal override BaseMethodDeclarationSyntax WithParameterListCore(ParameterListSyntax parameterList)
		{
			return this.WithParameterList(parameterList);
		}

		// Token: 0x06003910 RID: 14608 RVA: 0x000FC7B0 File Offset: 0x000FA9B0
		public new DestructorDeclarationSyntax WithParameterList(ParameterListSyntax parameterList)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.TildeToken, this.Identifier, parameterList, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06003911 RID: 14609 RVA: 0x000FC7EE File Offset: 0x000FA9EE
		internal override BaseMethodDeclarationSyntax WithBodyCore([global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax body)
		{
			return this.WithBody(body);
		}

		// Token: 0x06003912 RID: 14610 RVA: 0x000FC7F8 File Offset: 0x000FA9F8
		public new DestructorDeclarationSyntax WithBody([global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax body)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.TildeToken, this.Identifier, this.ParameterList, body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06003913 RID: 14611 RVA: 0x000FC836 File Offset: 0x000FAA36
		internal override BaseMethodDeclarationSyntax WithExpressionBodyCore([global::System.Runtime.CompilerServices.Nullable(2)] ArrowExpressionClauseSyntax expressionBody)
		{
			return this.WithExpressionBody(expressionBody);
		}

		// Token: 0x06003914 RID: 14612 RVA: 0x000FC840 File Offset: 0x000FAA40
		public new DestructorDeclarationSyntax WithExpressionBody([global::System.Runtime.CompilerServices.Nullable(2)] ArrowExpressionClauseSyntax expressionBody)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.TildeToken, this.Identifier, this.ParameterList, this.Body, expressionBody, this.SemicolonToken);
		}

		// Token: 0x06003915 RID: 14613 RVA: 0x000FC87E File Offset: 0x000FAA7E
		internal override BaseMethodDeclarationSyntax WithSemicolonTokenCore(SyntaxToken semicolonToken)
		{
			return this.WithSemicolonToken(semicolonToken);
		}

		// Token: 0x06003916 RID: 14614 RVA: 0x000FC888 File Offset: 0x000FAA88
		public new DestructorDeclarationSyntax WithSemicolonToken(SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.TildeToken, this.Identifier, this.ParameterList, this.Body, this.ExpressionBody, semicolonToken);
		}

		// Token: 0x06003917 RID: 14615 RVA: 0x000FC8C6 File Offset: 0x000FAAC6
		internal override MemberDeclarationSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x06003918 RID: 14616 RVA: 0x000FC8D0 File Offset: 0x000FAAD0
		public new DestructorDeclarationSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x06003919 RID: 14617 RVA: 0x000FC8F2 File Offset: 0x000FAAF2
		internal override MemberDeclarationSyntax AddModifiersCore(params SyntaxToken[] items)
		{
			return this.AddModifiers(items);
		}

		// Token: 0x0600391A RID: 14618 RVA: 0x000FC8FC File Offset: 0x000FAAFC
		public new DestructorDeclarationSyntax AddModifiers(params SyntaxToken[] items)
		{
			return this.WithModifiers(this.Modifiers.AddRange(items));
		}

		// Token: 0x0600391B RID: 14619 RVA: 0x000FC91E File Offset: 0x000FAB1E
		internal override BaseMethodDeclarationSyntax AddParameterListParametersCore(params ParameterSyntax[] items)
		{
			return this.AddParameterListParameters(items);
		}

		// Token: 0x0600391C RID: 14620 RVA: 0x000FC928 File Offset: 0x000FAB28
		public new DestructorDeclarationSyntax AddParameterListParameters(params ParameterSyntax[] items)
		{
			return this.WithParameterList(this.ParameterList.WithParameters(this.ParameterList.Parameters.AddRange(items)));
		}

		// Token: 0x0600391D RID: 14621 RVA: 0x000FC95A File Offset: 0x000FAB5A
		internal override BaseMethodDeclarationSyntax AddBodyAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddBodyAttributeLists(items);
		}

		// Token: 0x0600391E RID: 14622 RVA: 0x000FC964 File Offset: 0x000FAB64
		public new DestructorDeclarationSyntax AddBodyAttributeLists(params AttributeListSyntax[] items)
		{
			BlockSyntax blockSyntax;
			if ((blockSyntax = this.Body) == null)
			{
				blockSyntax = SyntaxFactory.Block(default(SyntaxList<StatementSyntax>));
			}
			BlockSyntax blockSyntax2 = blockSyntax;
			return this.WithBody(blockSyntax2.WithAttributeLists(blockSyntax2.AttributeLists.AddRange(items)));
		}

		// Token: 0x0600391F RID: 14623 RVA: 0x000FC9A5 File Offset: 0x000FABA5
		internal override BaseMethodDeclarationSyntax AddBodyStatementsCore(params StatementSyntax[] items)
		{
			return this.AddBodyStatements(items);
		}

		// Token: 0x06003920 RID: 14624 RVA: 0x000FC9B0 File Offset: 0x000FABB0
		public new DestructorDeclarationSyntax AddBodyStatements(params StatementSyntax[] items)
		{
			BlockSyntax blockSyntax;
			if ((blockSyntax = this.Body) == null)
			{
				blockSyntax = SyntaxFactory.Block(default(SyntaxList<StatementSyntax>));
			}
			BlockSyntax blockSyntax2 = blockSyntax;
			return this.WithBody(blockSyntax2.WithStatements(blockSyntax2.Statements.AddRange(items)));
		}

		// Token: 0x040013B4 RID: 5044
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x040013B5 RID: 5045
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ParameterListSyntax parameterList;

		// Token: 0x040013B6 RID: 5046
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private BlockSyntax body;

		// Token: 0x040013B7 RID: 5047
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ArrowExpressionClauseSyntax expressionBody;
	}
}
