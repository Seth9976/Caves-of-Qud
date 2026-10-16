using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200031E RID: 798
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class OperatorDeclarationSyntax : BaseMethodDeclarationSyntax
	{
		// Token: 0x060044C0 RID: 17600 RVA: 0x0011560B File Offset: 0x0011380B
		internal OperatorDeclarationSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170011AA RID: 4522
		// (get) Token: 0x060044C1 RID: 17601 RVA: 0x00115616 File Offset: 0x00113816
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x170011AB RID: 4523
		// (get) Token: 0x060044C2 RID: 17602 RVA: 0x0011562C File Offset: 0x0011382C
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

		// Token: 0x170011AC RID: 4524
		// (get) Token: 0x060044C3 RID: 17603 RVA: 0x00115668 File Offset: 0x00113868
		public TypeSyntax ReturnType
		{
			get
			{
				return base.GetRed<TypeSyntax>(ref this.returnType, 2);
			}
		}

		// Token: 0x170011AD RID: 4525
		// (get) Token: 0x060044C4 RID: 17604 RVA: 0x00115677 File Offset: 0x00113877
		public SyntaxToken OperatorKeyword
		{
			get
			{
				return new SyntaxToken(this, ((OperatorDeclarationSyntax)base.Green).operatorKeyword, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x170011AE RID: 4526
		// (get) Token: 0x060044C5 RID: 17605 RVA: 0x0011569D File Offset: 0x0011389D
		public SyntaxToken OperatorToken
		{
			get
			{
				return new SyntaxToken(this, ((OperatorDeclarationSyntax)base.Green).operatorToken, this.GetChildPosition(4), base.GetChildIndex(4));
			}
		}

		// Token: 0x170011AF RID: 4527
		// (get) Token: 0x060044C6 RID: 17606 RVA: 0x001156C3 File Offset: 0x001138C3
		public override ParameterListSyntax ParameterList
		{
			get
			{
				return base.GetRed<ParameterListSyntax>(ref this.parameterList, 5);
			}
		}

		// Token: 0x170011B0 RID: 4528
		// (get) Token: 0x060044C7 RID: 17607 RVA: 0x001156D2 File Offset: 0x001138D2
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override BlockSyntax Body
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<BlockSyntax>(ref this.body, 6);
			}
		}

		// Token: 0x170011B1 RID: 4529
		// (get) Token: 0x060044C8 RID: 17608 RVA: 0x001156E1 File Offset: 0x001138E1
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override ArrowExpressionClauseSyntax ExpressionBody
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<ArrowExpressionClauseSyntax>(ref this.expressionBody, 7);
			}
		}

		// Token: 0x170011B2 RID: 4530
		// (get) Token: 0x060044C9 RID: 17609 RVA: 0x001156F0 File Offset: 0x001138F0
		public override SyntaxToken SemicolonToken
		{
			get
			{
				SyntaxToken semicolonToken = ((OperatorDeclarationSyntax)base.Green).semicolonToken;
				if (semicolonToken == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, semicolonToken, this.GetChildPosition(8), base.GetChildIndex(8));
			}
		}

		// Token: 0x060044CA RID: 17610 RVA: 0x00115730 File Offset: 0x00113930
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			switch (index)
			{
			case 0:
				return base.GetRedAtZero(ref this.attributeLists);
			case 2:
				return base.GetRed<TypeSyntax>(ref this.returnType, 2);
			case 5:
				return base.GetRed<ParameterListSyntax>(ref this.parameterList, 5);
			case 6:
				return base.GetRed<BlockSyntax>(ref this.body, 6);
			case 7:
				return base.GetRed<ArrowExpressionClauseSyntax>(ref this.expressionBody, 7);
			}
			return null;
		}

		// Token: 0x060044CB RID: 17611 RVA: 0x001157B8 File Offset: 0x001139B8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			switch (index)
			{
			case 0:
				return this.attributeLists;
			case 2:
				return this.returnType;
			case 5:
				return this.parameterList;
			case 6:
				return this.body;
			case 7:
				return this.expressionBody;
			}
			return null;
		}

		// Token: 0x060044CC RID: 17612 RVA: 0x0011581D File Offset: 0x00113A1D
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitOperatorDeclaration(this);
		}

		// Token: 0x060044CD RID: 17613 RVA: 0x00115826 File Offset: 0x00113A26
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitOperatorDeclaration(this);
		}

		// Token: 0x060044CE RID: 17614 RVA: 0x00115830 File Offset: 0x00113A30
		public OperatorDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax returnType, SyntaxToken operatorKeyword, SyntaxToken operatorToken, ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax body, [global::System.Runtime.CompilerServices.Nullable(2)] ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
		{
			if (!(attributeLists != this.AttributeLists) && !(modifiers != this.Modifiers) && returnType == this.ReturnType && !(operatorKeyword != this.OperatorKeyword) && !(operatorToken != this.OperatorToken) && parameterList == this.ParameterList && body == this.Body && expressionBody == this.ExpressionBody && !(semicolonToken != this.SemicolonToken))
			{
				return this;
			}
			OperatorDeclarationSyntax operatorDeclarationSyntax = SyntaxFactory.OperatorDeclaration(attributeLists, modifiers, returnType, operatorKeyword, operatorToken, parameterList, body, expressionBody, semicolonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return operatorDeclarationSyntax;
			}
			return operatorDeclarationSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060044CF RID: 17615 RVA: 0x001158DB File Offset: 0x00113ADB
		internal override MemberDeclarationSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x060044D0 RID: 17616 RVA: 0x001158E4 File Offset: 0x00113AE4
		public new OperatorDeclarationSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.Modifiers, this.ReturnType, this.OperatorKeyword, this.OperatorToken, this.ParameterList, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x060044D1 RID: 17617 RVA: 0x00115928 File Offset: 0x00113B28
		internal override MemberDeclarationSyntax WithModifiersCore(SyntaxTokenList modifiers)
		{
			return this.WithModifiers(modifiers);
		}

		// Token: 0x060044D2 RID: 17618 RVA: 0x00115934 File Offset: 0x00113B34
		public new OperatorDeclarationSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return this.Update(this.AttributeLists, modifiers, this.ReturnType, this.OperatorKeyword, this.OperatorToken, this.ParameterList, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x060044D3 RID: 17619 RVA: 0x00115978 File Offset: 0x00113B78
		public OperatorDeclarationSyntax WithReturnType(TypeSyntax returnType)
		{
			return this.Update(this.AttributeLists, this.Modifiers, returnType, this.OperatorKeyword, this.OperatorToken, this.ParameterList, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x060044D4 RID: 17620 RVA: 0x001159BC File Offset: 0x00113BBC
		public OperatorDeclarationSyntax WithOperatorKeyword(SyntaxToken operatorKeyword)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.ReturnType, operatorKeyword, this.OperatorToken, this.ParameterList, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x060044D5 RID: 17621 RVA: 0x00115A00 File Offset: 0x00113C00
		public OperatorDeclarationSyntax WithOperatorToken(SyntaxToken operatorToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.ReturnType, this.OperatorKeyword, operatorToken, this.ParameterList, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x060044D6 RID: 17622 RVA: 0x00115A44 File Offset: 0x00113C44
		internal override BaseMethodDeclarationSyntax WithParameterListCore(ParameterListSyntax parameterList)
		{
			return this.WithParameterList(parameterList);
		}

		// Token: 0x060044D7 RID: 17623 RVA: 0x00115A50 File Offset: 0x00113C50
		public new OperatorDeclarationSyntax WithParameterList(ParameterListSyntax parameterList)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.ReturnType, this.OperatorKeyword, this.OperatorToken, parameterList, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x060044D8 RID: 17624 RVA: 0x00115A94 File Offset: 0x00113C94
		internal override BaseMethodDeclarationSyntax WithBodyCore([global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax body)
		{
			return this.WithBody(body);
		}

		// Token: 0x060044D9 RID: 17625 RVA: 0x00115AA0 File Offset: 0x00113CA0
		public new OperatorDeclarationSyntax WithBody([global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax body)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.ReturnType, this.OperatorKeyword, this.OperatorToken, this.ParameterList, body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x060044DA RID: 17626 RVA: 0x00115AE4 File Offset: 0x00113CE4
		internal override BaseMethodDeclarationSyntax WithExpressionBodyCore([global::System.Runtime.CompilerServices.Nullable(2)] ArrowExpressionClauseSyntax expressionBody)
		{
			return this.WithExpressionBody(expressionBody);
		}

		// Token: 0x060044DB RID: 17627 RVA: 0x00115AF0 File Offset: 0x00113CF0
		public new OperatorDeclarationSyntax WithExpressionBody([global::System.Runtime.CompilerServices.Nullable(2)] ArrowExpressionClauseSyntax expressionBody)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.ReturnType, this.OperatorKeyword, this.OperatorToken, this.ParameterList, this.Body, expressionBody, this.SemicolonToken);
		}

		// Token: 0x060044DC RID: 17628 RVA: 0x00115B34 File Offset: 0x00113D34
		internal override BaseMethodDeclarationSyntax WithSemicolonTokenCore(SyntaxToken semicolonToken)
		{
			return this.WithSemicolonToken(semicolonToken);
		}

		// Token: 0x060044DD RID: 17629 RVA: 0x00115B40 File Offset: 0x00113D40
		public new OperatorDeclarationSyntax WithSemicolonToken(SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.ReturnType, this.OperatorKeyword, this.OperatorToken, this.ParameterList, this.Body, this.ExpressionBody, semicolonToken);
		}

		// Token: 0x060044DE RID: 17630 RVA: 0x00115B84 File Offset: 0x00113D84
		internal override MemberDeclarationSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x060044DF RID: 17631 RVA: 0x00115B90 File Offset: 0x00113D90
		public new OperatorDeclarationSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x060044E0 RID: 17632 RVA: 0x00115BB2 File Offset: 0x00113DB2
		internal override MemberDeclarationSyntax AddModifiersCore(params SyntaxToken[] items)
		{
			return this.AddModifiers(items);
		}

		// Token: 0x060044E1 RID: 17633 RVA: 0x00115BBC File Offset: 0x00113DBC
		public new OperatorDeclarationSyntax AddModifiers(params SyntaxToken[] items)
		{
			return this.WithModifiers(this.Modifiers.AddRange(items));
		}

		// Token: 0x060044E2 RID: 17634 RVA: 0x00115BDE File Offset: 0x00113DDE
		internal override BaseMethodDeclarationSyntax AddParameterListParametersCore(params ParameterSyntax[] items)
		{
			return this.AddParameterListParameters(items);
		}

		// Token: 0x060044E3 RID: 17635 RVA: 0x00115BE8 File Offset: 0x00113DE8
		public new OperatorDeclarationSyntax AddParameterListParameters(params ParameterSyntax[] items)
		{
			return this.WithParameterList(this.ParameterList.WithParameters(this.ParameterList.Parameters.AddRange(items)));
		}

		// Token: 0x060044E4 RID: 17636 RVA: 0x00115C1A File Offset: 0x00113E1A
		internal override BaseMethodDeclarationSyntax AddBodyAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddBodyAttributeLists(items);
		}

		// Token: 0x060044E5 RID: 17637 RVA: 0x00115C24 File Offset: 0x00113E24
		public new OperatorDeclarationSyntax AddBodyAttributeLists(params AttributeListSyntax[] items)
		{
			BlockSyntax blockSyntax;
			if ((blockSyntax = this.Body) == null)
			{
				blockSyntax = SyntaxFactory.Block(default(SyntaxList<StatementSyntax>));
			}
			BlockSyntax blockSyntax2 = blockSyntax;
			return this.WithBody(blockSyntax2.WithAttributeLists(blockSyntax2.AttributeLists.AddRange(items)));
		}

		// Token: 0x060044E6 RID: 17638 RVA: 0x00115C65 File Offset: 0x00113E65
		internal override BaseMethodDeclarationSyntax AddBodyStatementsCore(params StatementSyntax[] items)
		{
			return this.AddBodyStatements(items);
		}

		// Token: 0x060044E7 RID: 17639 RVA: 0x00115C70 File Offset: 0x00113E70
		public new OperatorDeclarationSyntax AddBodyStatements(params StatementSyntax[] items)
		{
			BlockSyntax blockSyntax;
			if ((blockSyntax = this.Body) == null)
			{
				blockSyntax = SyntaxFactory.Block(default(SyntaxList<StatementSyntax>));
			}
			BlockSyntax blockSyntax2 = blockSyntax;
			return this.WithBody(blockSyntax2.WithStatements(blockSyntax2.Statements.AddRange(items)));
		}

		// Token: 0x040014FD RID: 5373
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x040014FE RID: 5374
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax returnType;

		// Token: 0x040014FF RID: 5375
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ParameterListSyntax parameterList;

		// Token: 0x04001500 RID: 5376
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private BlockSyntax body;

		// Token: 0x04001501 RID: 5377
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ArrowExpressionClauseSyntax expressionBody;
	}
}
