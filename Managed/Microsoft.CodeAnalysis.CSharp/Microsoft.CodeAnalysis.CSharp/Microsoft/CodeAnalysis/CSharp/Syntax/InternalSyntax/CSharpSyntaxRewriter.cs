using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200036C RID: 876
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
	internal class CSharpSyntaxRewriter : CSharpSyntaxVisitor<CSharpSyntaxNode>
	{
		// Token: 0x06004C5A RID: 19546 RVA: 0x0013BDB5 File Offset: 0x00139FB5
		public CSharpSyntaxRewriter(bool visitIntoStructuredTrivia = false)
		{
			this.VisitIntoStructuredTrivia = visitIntoStructuredTrivia;
		}

		// Token: 0x06004C5B RID: 19547 RVA: 0x0013BDC4 File Offset: 0x00139FC4
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public override CSharpSyntaxNode VisitToken(SyntaxToken token)
		{
			SyntaxList<CSharpSyntaxNode> syntaxList = this.VisitList<CSharpSyntaxNode>(token.LeadingTrivia);
			SyntaxList<CSharpSyntaxNode> syntaxList2 = this.VisitList<CSharpSyntaxNode>(token.TrailingTrivia);
			if (syntaxList != token.LeadingTrivia || syntaxList2 != token.TrailingTrivia)
			{
				if (syntaxList != token.LeadingTrivia)
				{
					token = token.TokenWithLeadingTrivia(syntaxList.Node);
				}
				if (syntaxList2 != token.TrailingTrivia)
				{
					token = token.TokenWithTrailingTrivia(syntaxList2.Node);
				}
			}
			return token;
		}

		// Token: 0x06004C5C RID: 19548 RVA: 0x0013BE44 File Offset: 0x0013A044
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public SyntaxList<TNode> VisitList<TNode>(SyntaxList<TNode> list) where TNode : CSharpSyntaxNode
		{
			SyntaxListBuilder syntaxListBuilder = null;
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				TNode tnode = list[i];
				CSharpSyntaxNode csharpSyntaxNode = this.Visit(tnode);
				if (tnode != csharpSyntaxNode && syntaxListBuilder == null)
				{
					syntaxListBuilder = new SyntaxListBuilder(count);
					syntaxListBuilder.AddRange<TNode>(list, 0, i);
				}
				if (syntaxListBuilder != null)
				{
					syntaxListBuilder.Add(csharpSyntaxNode);
				}
				i++;
			}
			if (syntaxListBuilder != null)
			{
				return syntaxListBuilder.ToList();
			}
			return list;
		}

		// Token: 0x06004C5D RID: 19549 RVA: 0x0013BEB8 File Offset: 0x0013A0B8
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public SeparatedSyntaxList<TNode> VisitList<TNode>(SeparatedSyntaxList<TNode> list) where TNode : CSharpSyntaxNode
		{
			SyntaxList<CSharpSyntaxNode> syntaxList = list.GetWithSeparators();
			SyntaxList<CSharpSyntaxNode> syntaxList2 = this.VisitList<CSharpSyntaxNode>(syntaxList);
			if (syntaxList2 != syntaxList)
			{
				return syntaxList2.AsSeparatedList<TNode>();
			}
			return list;
		}

		// Token: 0x06004C5E RID: 19550 RVA: 0x0013BEEC File Offset: 0x0013A0EC
		public override CSharpSyntaxNode VisitIdentifierName(IdentifierNameSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.Identifier));
		}

		// Token: 0x06004C5F RID: 19551 RVA: 0x0013BF05 File Offset: 0x0013A105
		public override CSharpSyntaxNode VisitQualifiedName(QualifiedNameSyntax node)
		{
			return node.Update((NameSyntax)this.Visit(node.Left), (SyntaxToken)this.Visit(node.DotToken), (SimpleNameSyntax)this.Visit(node.Right));
		}

		// Token: 0x06004C60 RID: 19552 RVA: 0x0013BF40 File Offset: 0x0013A140
		public override CSharpSyntaxNode VisitGenericName(GenericNameSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.Identifier), (TypeArgumentListSyntax)this.Visit(node.TypeArgumentList));
		}

		// Token: 0x06004C61 RID: 19553 RVA: 0x0013BF6A File Offset: 0x0013A16A
		public override CSharpSyntaxNode VisitTypeArgumentList(TypeArgumentListSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.LessThanToken), this.VisitList<TypeSyntax>(node.Arguments), (SyntaxToken)this.Visit(node.GreaterThanToken));
		}

		// Token: 0x06004C62 RID: 19554 RVA: 0x0013BFA0 File Offset: 0x0013A1A0
		public override CSharpSyntaxNode VisitAliasQualifiedName(AliasQualifiedNameSyntax node)
		{
			return node.Update((IdentifierNameSyntax)this.Visit(node.Alias), (SyntaxToken)this.Visit(node.ColonColonToken), (SimpleNameSyntax)this.Visit(node.Name));
		}

		// Token: 0x06004C63 RID: 19555 RVA: 0x0013BFDB File Offset: 0x0013A1DB
		public override CSharpSyntaxNode VisitPredefinedType(PredefinedTypeSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.Keyword));
		}

		// Token: 0x06004C64 RID: 19556 RVA: 0x0013BFF4 File Offset: 0x0013A1F4
		public override CSharpSyntaxNode VisitArrayType(ArrayTypeSyntax node)
		{
			return node.Update((TypeSyntax)this.Visit(node.ElementType), this.VisitList<ArrayRankSpecifierSyntax>(node.RankSpecifiers));
		}

		// Token: 0x06004C65 RID: 19557 RVA: 0x0013C019 File Offset: 0x0013A219
		public override CSharpSyntaxNode VisitArrayRankSpecifier(ArrayRankSpecifierSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.OpenBracketToken), this.VisitList<ExpressionSyntax>(node.Sizes), (SyntaxToken)this.Visit(node.CloseBracketToken));
		}

		// Token: 0x06004C66 RID: 19558 RVA: 0x0013C04F File Offset: 0x0013A24F
		public override CSharpSyntaxNode VisitPointerType(PointerTypeSyntax node)
		{
			return node.Update((TypeSyntax)this.Visit(node.ElementType), (SyntaxToken)this.Visit(node.AsteriskToken));
		}

		// Token: 0x06004C67 RID: 19559 RVA: 0x0013C07C File Offset: 0x0013A27C
		public override CSharpSyntaxNode VisitFunctionPointerType(FunctionPointerTypeSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.DelegateKeyword), (SyntaxToken)this.Visit(node.AsteriskToken), (FunctionPointerCallingConventionSyntax)this.Visit(node.CallingConvention), (FunctionPointerParameterListSyntax)this.Visit(node.ParameterList));
		}

		// Token: 0x06004C68 RID: 19560 RVA: 0x0013C0D3 File Offset: 0x0013A2D3
		public override CSharpSyntaxNode VisitFunctionPointerParameterList(FunctionPointerParameterListSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.LessThanToken), this.VisitList<FunctionPointerParameterSyntax>(node.Parameters), (SyntaxToken)this.Visit(node.GreaterThanToken));
		}

		// Token: 0x06004C69 RID: 19561 RVA: 0x0013C109 File Offset: 0x0013A309
		public override CSharpSyntaxNode VisitFunctionPointerCallingConvention(FunctionPointerCallingConventionSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.ManagedOrUnmanagedKeyword), (FunctionPointerUnmanagedCallingConventionListSyntax)this.Visit(node.UnmanagedCallingConventionList));
		}

		// Token: 0x06004C6A RID: 19562 RVA: 0x0013C133 File Offset: 0x0013A333
		public override CSharpSyntaxNode VisitFunctionPointerUnmanagedCallingConventionList(FunctionPointerUnmanagedCallingConventionListSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.OpenBracketToken), this.VisitList<FunctionPointerUnmanagedCallingConventionSyntax>(node.CallingConventions), (SyntaxToken)this.Visit(node.CloseBracketToken));
		}

		// Token: 0x06004C6B RID: 19563 RVA: 0x0013C169 File Offset: 0x0013A369
		public override CSharpSyntaxNode VisitFunctionPointerUnmanagedCallingConvention(FunctionPointerUnmanagedCallingConventionSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.Name));
		}

		// Token: 0x06004C6C RID: 19564 RVA: 0x0013C182 File Offset: 0x0013A382
		public override CSharpSyntaxNode VisitNullableType(NullableTypeSyntax node)
		{
			return node.Update((TypeSyntax)this.Visit(node.ElementType), (SyntaxToken)this.Visit(node.QuestionToken));
		}

		// Token: 0x06004C6D RID: 19565 RVA: 0x0013C1AC File Offset: 0x0013A3AC
		public override CSharpSyntaxNode VisitTupleType(TupleTypeSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.OpenParenToken), this.VisitList<TupleElementSyntax>(node.Elements), (SyntaxToken)this.Visit(node.CloseParenToken));
		}

		// Token: 0x06004C6E RID: 19566 RVA: 0x0013C1E2 File Offset: 0x0013A3E2
		public override CSharpSyntaxNode VisitTupleElement(TupleElementSyntax node)
		{
			return node.Update((TypeSyntax)this.Visit(node.Type), (SyntaxToken)this.Visit(node.Identifier));
		}

		// Token: 0x06004C6F RID: 19567 RVA: 0x0013C20C File Offset: 0x0013A40C
		public override CSharpSyntaxNode VisitOmittedTypeArgument(OmittedTypeArgumentSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.OmittedTypeArgumentToken));
		}

		// Token: 0x06004C70 RID: 19568 RVA: 0x0013C225 File Offset: 0x0013A425
		public override CSharpSyntaxNode VisitRefType(RefTypeSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.RefKeyword), (SyntaxToken)this.Visit(node.ReadOnlyKeyword), (TypeSyntax)this.Visit(node.Type));
		}

		// Token: 0x06004C71 RID: 19569 RVA: 0x0013C260 File Offset: 0x0013A460
		public override CSharpSyntaxNode VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.OpenParenToken), (ExpressionSyntax)this.Visit(node.Expression), (SyntaxToken)this.Visit(node.CloseParenToken));
		}

		// Token: 0x06004C72 RID: 19570 RVA: 0x0013C29B File Offset: 0x0013A49B
		public override CSharpSyntaxNode VisitTupleExpression(TupleExpressionSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.OpenParenToken), this.VisitList<ArgumentSyntax>(node.Arguments), (SyntaxToken)this.Visit(node.CloseParenToken));
		}

		// Token: 0x06004C73 RID: 19571 RVA: 0x0013C2D1 File Offset: 0x0013A4D1
		public override CSharpSyntaxNode VisitPrefixUnaryExpression(PrefixUnaryExpressionSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.OperatorToken), (ExpressionSyntax)this.Visit(node.Operand));
		}

		// Token: 0x06004C74 RID: 19572 RVA: 0x0013C2FB File Offset: 0x0013A4FB
		public override CSharpSyntaxNode VisitAwaitExpression(AwaitExpressionSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.AwaitKeyword), (ExpressionSyntax)this.Visit(node.Expression));
		}

		// Token: 0x06004C75 RID: 19573 RVA: 0x0013C325 File Offset: 0x0013A525
		public override CSharpSyntaxNode VisitPostfixUnaryExpression(PostfixUnaryExpressionSyntax node)
		{
			return node.Update((ExpressionSyntax)this.Visit(node.Operand), (SyntaxToken)this.Visit(node.OperatorToken));
		}

		// Token: 0x06004C76 RID: 19574 RVA: 0x0013C34F File Offset: 0x0013A54F
		public override CSharpSyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
		{
			return node.Update((ExpressionSyntax)this.Visit(node.Expression), (SyntaxToken)this.Visit(node.OperatorToken), (SimpleNameSyntax)this.Visit(node.Name));
		}

		// Token: 0x06004C77 RID: 19575 RVA: 0x0013C38A File Offset: 0x0013A58A
		public override CSharpSyntaxNode VisitConditionalAccessExpression(ConditionalAccessExpressionSyntax node)
		{
			return node.Update((ExpressionSyntax)this.Visit(node.Expression), (SyntaxToken)this.Visit(node.OperatorToken), (ExpressionSyntax)this.Visit(node.WhenNotNull));
		}

		// Token: 0x06004C78 RID: 19576 RVA: 0x0013C3C5 File Offset: 0x0013A5C5
		public override CSharpSyntaxNode VisitMemberBindingExpression(MemberBindingExpressionSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.OperatorToken), (SimpleNameSyntax)this.Visit(node.Name));
		}

		// Token: 0x06004C79 RID: 19577 RVA: 0x0013C3EF File Offset: 0x0013A5EF
		public override CSharpSyntaxNode VisitElementBindingExpression(ElementBindingExpressionSyntax node)
		{
			return node.Update((BracketedArgumentListSyntax)this.Visit(node.ArgumentList));
		}

		// Token: 0x06004C7A RID: 19578 RVA: 0x0013C408 File Offset: 0x0013A608
		public override CSharpSyntaxNode VisitRangeExpression(RangeExpressionSyntax node)
		{
			return node.Update((ExpressionSyntax)this.Visit(node.LeftOperand), (SyntaxToken)this.Visit(node.OperatorToken), (ExpressionSyntax)this.Visit(node.RightOperand));
		}

		// Token: 0x06004C7B RID: 19579 RVA: 0x0013C443 File Offset: 0x0013A643
		public override CSharpSyntaxNode VisitImplicitElementAccess(ImplicitElementAccessSyntax node)
		{
			return node.Update((BracketedArgumentListSyntax)this.Visit(node.ArgumentList));
		}

		// Token: 0x06004C7C RID: 19580 RVA: 0x0013C45C File Offset: 0x0013A65C
		public override CSharpSyntaxNode VisitBinaryExpression(BinaryExpressionSyntax node)
		{
			return node.Update((ExpressionSyntax)this.Visit(node.Left), (SyntaxToken)this.Visit(node.OperatorToken), (ExpressionSyntax)this.Visit(node.Right));
		}

		// Token: 0x06004C7D RID: 19581 RVA: 0x0013C497 File Offset: 0x0013A697
		public override CSharpSyntaxNode VisitAssignmentExpression(AssignmentExpressionSyntax node)
		{
			return node.Update((ExpressionSyntax)this.Visit(node.Left), (SyntaxToken)this.Visit(node.OperatorToken), (ExpressionSyntax)this.Visit(node.Right));
		}

		// Token: 0x06004C7E RID: 19582 RVA: 0x0013C4D4 File Offset: 0x0013A6D4
		public override CSharpSyntaxNode VisitConditionalExpression(ConditionalExpressionSyntax node)
		{
			return node.Update((ExpressionSyntax)this.Visit(node.Condition), (SyntaxToken)this.Visit(node.QuestionToken), (ExpressionSyntax)this.Visit(node.WhenTrue), (SyntaxToken)this.Visit(node.ColonToken), (ExpressionSyntax)this.Visit(node.WhenFalse));
		}

		// Token: 0x06004C7F RID: 19583 RVA: 0x0013C53C File Offset: 0x0013A73C
		public override CSharpSyntaxNode VisitThisExpression(ThisExpressionSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.Token));
		}

		// Token: 0x06004C80 RID: 19584 RVA: 0x0013C555 File Offset: 0x0013A755
		public override CSharpSyntaxNode VisitBaseExpression(BaseExpressionSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.Token));
		}

		// Token: 0x06004C81 RID: 19585 RVA: 0x0013C56E File Offset: 0x0013A76E
		public override CSharpSyntaxNode VisitLiteralExpression(LiteralExpressionSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.Token));
		}

		// Token: 0x06004C82 RID: 19586 RVA: 0x0013C588 File Offset: 0x0013A788
		public override CSharpSyntaxNode VisitMakeRefExpression(MakeRefExpressionSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.Keyword), (SyntaxToken)this.Visit(node.OpenParenToken), (ExpressionSyntax)this.Visit(node.Expression), (SyntaxToken)this.Visit(node.CloseParenToken));
		}

		// Token: 0x06004C83 RID: 19587 RVA: 0x0013C5E0 File Offset: 0x0013A7E0
		public override CSharpSyntaxNode VisitRefTypeExpression(RefTypeExpressionSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.Keyword), (SyntaxToken)this.Visit(node.OpenParenToken), (ExpressionSyntax)this.Visit(node.Expression), (SyntaxToken)this.Visit(node.CloseParenToken));
		}

		// Token: 0x06004C84 RID: 19588 RVA: 0x0013C638 File Offset: 0x0013A838
		public override CSharpSyntaxNode VisitRefValueExpression(RefValueExpressionSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.Keyword), (SyntaxToken)this.Visit(node.OpenParenToken), (ExpressionSyntax)this.Visit(node.Expression), (SyntaxToken)this.Visit(node.Comma), (TypeSyntax)this.Visit(node.Type), (SyntaxToken)this.Visit(node.CloseParenToken));
		}

		// Token: 0x06004C85 RID: 19589 RVA: 0x0013C6B4 File Offset: 0x0013A8B4
		public override CSharpSyntaxNode VisitCheckedExpression(CheckedExpressionSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.Keyword), (SyntaxToken)this.Visit(node.OpenParenToken), (ExpressionSyntax)this.Visit(node.Expression), (SyntaxToken)this.Visit(node.CloseParenToken));
		}

		// Token: 0x06004C86 RID: 19590 RVA: 0x0013C70C File Offset: 0x0013A90C
		public override CSharpSyntaxNode VisitDefaultExpression(DefaultExpressionSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.Keyword), (SyntaxToken)this.Visit(node.OpenParenToken), (TypeSyntax)this.Visit(node.Type), (SyntaxToken)this.Visit(node.CloseParenToken));
		}

		// Token: 0x06004C87 RID: 19591 RVA: 0x0013C764 File Offset: 0x0013A964
		public override CSharpSyntaxNode VisitTypeOfExpression(TypeOfExpressionSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.Keyword), (SyntaxToken)this.Visit(node.OpenParenToken), (TypeSyntax)this.Visit(node.Type), (SyntaxToken)this.Visit(node.CloseParenToken));
		}

		// Token: 0x06004C88 RID: 19592 RVA: 0x0013C7BC File Offset: 0x0013A9BC
		public override CSharpSyntaxNode VisitSizeOfExpression(SizeOfExpressionSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.Keyword), (SyntaxToken)this.Visit(node.OpenParenToken), (TypeSyntax)this.Visit(node.Type), (SyntaxToken)this.Visit(node.CloseParenToken));
		}

		// Token: 0x06004C89 RID: 19593 RVA: 0x0013C813 File Offset: 0x0013AA13
		public override CSharpSyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node)
		{
			return node.Update((ExpressionSyntax)this.Visit(node.Expression), (ArgumentListSyntax)this.Visit(node.ArgumentList));
		}

		// Token: 0x06004C8A RID: 19594 RVA: 0x0013C83D File Offset: 0x0013AA3D
		public override CSharpSyntaxNode VisitElementAccessExpression(ElementAccessExpressionSyntax node)
		{
			return node.Update((ExpressionSyntax)this.Visit(node.Expression), (BracketedArgumentListSyntax)this.Visit(node.ArgumentList));
		}

		// Token: 0x06004C8B RID: 19595 RVA: 0x0013C867 File Offset: 0x0013AA67
		public override CSharpSyntaxNode VisitArgumentList(ArgumentListSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.OpenParenToken), this.VisitList<ArgumentSyntax>(node.Arguments), (SyntaxToken)this.Visit(node.CloseParenToken));
		}

		// Token: 0x06004C8C RID: 19596 RVA: 0x0013C89D File Offset: 0x0013AA9D
		public override CSharpSyntaxNode VisitBracketedArgumentList(BracketedArgumentListSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.OpenBracketToken), this.VisitList<ArgumentSyntax>(node.Arguments), (SyntaxToken)this.Visit(node.CloseBracketToken));
		}

		// Token: 0x06004C8D RID: 19597 RVA: 0x0013C8D3 File Offset: 0x0013AAD3
		public override CSharpSyntaxNode VisitArgument(ArgumentSyntax node)
		{
			return node.Update((NameColonSyntax)this.Visit(node.NameColon), (SyntaxToken)this.Visit(node.RefKindKeyword), (ExpressionSyntax)this.Visit(node.Expression));
		}

		// Token: 0x06004C8E RID: 19598 RVA: 0x0013C90E File Offset: 0x0013AB0E
		public override CSharpSyntaxNode VisitNameColon(NameColonSyntax node)
		{
			return node.Update((IdentifierNameSyntax)this.Visit(node.Name), (SyntaxToken)this.Visit(node.ColonToken));
		}

		// Token: 0x06004C8F RID: 19599 RVA: 0x0013C938 File Offset: 0x0013AB38
		public override CSharpSyntaxNode VisitDeclarationExpression(DeclarationExpressionSyntax node)
		{
			return node.Update((TypeSyntax)this.Visit(node.Type), (VariableDesignationSyntax)this.Visit(node.Designation));
		}

		// Token: 0x06004C90 RID: 19600 RVA: 0x0013C964 File Offset: 0x0013AB64
		public override CSharpSyntaxNode VisitCastExpression(CastExpressionSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.OpenParenToken), (TypeSyntax)this.Visit(node.Type), (SyntaxToken)this.Visit(node.CloseParenToken), (ExpressionSyntax)this.Visit(node.Expression));
		}

		// Token: 0x06004C91 RID: 19601 RVA: 0x0013C9BC File Offset: 0x0013ABBC
		public override CSharpSyntaxNode VisitAnonymousMethodExpression(AnonymousMethodExpressionSyntax node)
		{
			return node.Update(this.VisitList<SyntaxToken>(node.Modifiers), (SyntaxToken)this.Visit(node.DelegateKeyword), (ParameterListSyntax)this.Visit(node.ParameterList), (BlockSyntax)this.Visit(node.Block), (ExpressionSyntax)this.Visit(node.ExpressionBody));
		}

		// Token: 0x06004C92 RID: 19602 RVA: 0x0013CA20 File Offset: 0x0013AC20
		public override CSharpSyntaxNode VisitSimpleLambdaExpression(SimpleLambdaExpressionSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList<SyntaxToken>(node.Modifiers), (ParameterSyntax)this.Visit(node.Parameter), (SyntaxToken)this.Visit(node.ArrowToken), (BlockSyntax)this.Visit(node.Block), (ExpressionSyntax)this.Visit(node.ExpressionBody));
		}

		// Token: 0x06004C93 RID: 19603 RVA: 0x0013CA8F File Offset: 0x0013AC8F
		public override CSharpSyntaxNode VisitRefExpression(RefExpressionSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.RefKeyword), (ExpressionSyntax)this.Visit(node.Expression));
		}

		// Token: 0x06004C94 RID: 19604 RVA: 0x0013CABC File Offset: 0x0013ACBC
		public override CSharpSyntaxNode VisitParenthesizedLambdaExpression(ParenthesizedLambdaExpressionSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList<SyntaxToken>(node.Modifiers), (ParameterListSyntax)this.Visit(node.ParameterList), (SyntaxToken)this.Visit(node.ArrowToken), (BlockSyntax)this.Visit(node.Block), (ExpressionSyntax)this.Visit(node.ExpressionBody));
		}

		// Token: 0x06004C95 RID: 19605 RVA: 0x0013CB2B File Offset: 0x0013AD2B
		public override CSharpSyntaxNode VisitInitializerExpression(InitializerExpressionSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.OpenBraceToken), this.VisitList<ExpressionSyntax>(node.Expressions), (SyntaxToken)this.Visit(node.CloseBraceToken));
		}

		// Token: 0x06004C96 RID: 19606 RVA: 0x0013CB61 File Offset: 0x0013AD61
		public override CSharpSyntaxNode VisitImplicitObjectCreationExpression(ImplicitObjectCreationExpressionSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.NewKeyword), (ArgumentListSyntax)this.Visit(node.ArgumentList), (InitializerExpressionSyntax)this.Visit(node.Initializer));
		}

		// Token: 0x06004C97 RID: 19607 RVA: 0x0013CB9C File Offset: 0x0013AD9C
		public override CSharpSyntaxNode VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.NewKeyword), (TypeSyntax)this.Visit(node.Type), (ArgumentListSyntax)this.Visit(node.ArgumentList), (InitializerExpressionSyntax)this.Visit(node.Initializer));
		}

		// Token: 0x06004C98 RID: 19608 RVA: 0x0013CBF3 File Offset: 0x0013ADF3
		public override CSharpSyntaxNode VisitWithExpression(WithExpressionSyntax node)
		{
			return node.Update((ExpressionSyntax)this.Visit(node.Expression), (SyntaxToken)this.Visit(node.WithKeyword), (InitializerExpressionSyntax)this.Visit(node.Initializer));
		}

		// Token: 0x06004C99 RID: 19609 RVA: 0x0013CC2E File Offset: 0x0013AE2E
		public override CSharpSyntaxNode VisitAnonymousObjectMemberDeclarator(AnonymousObjectMemberDeclaratorSyntax node)
		{
			return node.Update((NameEqualsSyntax)this.Visit(node.NameEquals), (ExpressionSyntax)this.Visit(node.Expression));
		}

		// Token: 0x06004C9A RID: 19610 RVA: 0x0013CC58 File Offset: 0x0013AE58
		public override CSharpSyntaxNode VisitAnonymousObjectCreationExpression(AnonymousObjectCreationExpressionSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.NewKeyword), (SyntaxToken)this.Visit(node.OpenBraceToken), this.VisitList<AnonymousObjectMemberDeclaratorSyntax>(node.Initializers), (SyntaxToken)this.Visit(node.CloseBraceToken));
		}

		// Token: 0x06004C9B RID: 19611 RVA: 0x0013CCAA File Offset: 0x0013AEAA
		public override CSharpSyntaxNode VisitArrayCreationExpression(ArrayCreationExpressionSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.NewKeyword), (ArrayTypeSyntax)this.Visit(node.Type), (InitializerExpressionSyntax)this.Visit(node.Initializer));
		}

		// Token: 0x06004C9C RID: 19612 RVA: 0x0013CCE8 File Offset: 0x0013AEE8
		public override CSharpSyntaxNode VisitImplicitArrayCreationExpression(ImplicitArrayCreationExpressionSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.NewKeyword), (SyntaxToken)this.Visit(node.OpenBracketToken), this.VisitList<SyntaxToken>(node.Commas), (SyntaxToken)this.Visit(node.CloseBracketToken), (InitializerExpressionSyntax)this.Visit(node.Initializer));
		}

		// Token: 0x06004C9D RID: 19613 RVA: 0x0013CD4B File Offset: 0x0013AF4B
		public override CSharpSyntaxNode VisitStackAllocArrayCreationExpression(StackAllocArrayCreationExpressionSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.StackAllocKeyword), (TypeSyntax)this.Visit(node.Type), (InitializerExpressionSyntax)this.Visit(node.Initializer));
		}

		// Token: 0x06004C9E RID: 19614 RVA: 0x0013CD88 File Offset: 0x0013AF88
		public override CSharpSyntaxNode VisitImplicitStackAllocArrayCreationExpression(ImplicitStackAllocArrayCreationExpressionSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.StackAllocKeyword), (SyntaxToken)this.Visit(node.OpenBracketToken), (SyntaxToken)this.Visit(node.CloseBracketToken), (InitializerExpressionSyntax)this.Visit(node.Initializer));
		}

		// Token: 0x06004C9F RID: 19615 RVA: 0x0013CDDF File Offset: 0x0013AFDF
		public override CSharpSyntaxNode VisitQueryExpression(QueryExpressionSyntax node)
		{
			return node.Update((FromClauseSyntax)this.Visit(node.FromClause), (QueryBodySyntax)this.Visit(node.Body));
		}

		// Token: 0x06004CA0 RID: 19616 RVA: 0x0013CE09 File Offset: 0x0013B009
		public override CSharpSyntaxNode VisitQueryBody(QueryBodySyntax node)
		{
			return node.Update(this.VisitList<QueryClauseSyntax>(node.Clauses), (SelectOrGroupClauseSyntax)this.Visit(node.SelectOrGroup), (QueryContinuationSyntax)this.Visit(node.Continuation));
		}

		// Token: 0x06004CA1 RID: 19617 RVA: 0x0013CE40 File Offset: 0x0013B040
		public override CSharpSyntaxNode VisitFromClause(FromClauseSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.FromKeyword), (TypeSyntax)this.Visit(node.Type), (SyntaxToken)this.Visit(node.Identifier), (SyntaxToken)this.Visit(node.InKeyword), (ExpressionSyntax)this.Visit(node.Expression));
		}

		// Token: 0x06004CA2 RID: 19618 RVA: 0x0013CEA8 File Offset: 0x0013B0A8
		public override CSharpSyntaxNode VisitLetClause(LetClauseSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.LetKeyword), (SyntaxToken)this.Visit(node.Identifier), (SyntaxToken)this.Visit(node.EqualsToken), (ExpressionSyntax)this.Visit(node.Expression));
		}

		// Token: 0x06004CA3 RID: 19619 RVA: 0x0013CF00 File Offset: 0x0013B100
		public override CSharpSyntaxNode VisitJoinClause(JoinClauseSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.JoinKeyword), (TypeSyntax)this.Visit(node.Type), (SyntaxToken)this.Visit(node.Identifier), (SyntaxToken)this.Visit(node.InKeyword), (ExpressionSyntax)this.Visit(node.InExpression), (SyntaxToken)this.Visit(node.OnKeyword), (ExpressionSyntax)this.Visit(node.LeftExpression), (SyntaxToken)this.Visit(node.EqualsKeyword), (ExpressionSyntax)this.Visit(node.RightExpression), (JoinIntoClauseSyntax)this.Visit(node.Into));
		}

		// Token: 0x06004CA4 RID: 19620 RVA: 0x0013CFBD File Offset: 0x0013B1BD
		public override CSharpSyntaxNode VisitJoinIntoClause(JoinIntoClauseSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.IntoKeyword), (SyntaxToken)this.Visit(node.Identifier));
		}

		// Token: 0x06004CA5 RID: 19621 RVA: 0x0013CFE7 File Offset: 0x0013B1E7
		public override CSharpSyntaxNode VisitWhereClause(WhereClauseSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.WhereKeyword), (ExpressionSyntax)this.Visit(node.Condition));
		}

		// Token: 0x06004CA6 RID: 19622 RVA: 0x0013D011 File Offset: 0x0013B211
		public override CSharpSyntaxNode VisitOrderByClause(OrderByClauseSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.OrderByKeyword), this.VisitList<OrderingSyntax>(node.Orderings));
		}

		// Token: 0x06004CA7 RID: 19623 RVA: 0x0013D036 File Offset: 0x0013B236
		public override CSharpSyntaxNode VisitOrdering(OrderingSyntax node)
		{
			return node.Update((ExpressionSyntax)this.Visit(node.Expression), (SyntaxToken)this.Visit(node.AscendingOrDescendingKeyword));
		}

		// Token: 0x06004CA8 RID: 19624 RVA: 0x0013D060 File Offset: 0x0013B260
		public override CSharpSyntaxNode VisitSelectClause(SelectClauseSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.SelectKeyword), (ExpressionSyntax)this.Visit(node.Expression));
		}

		// Token: 0x06004CA9 RID: 19625 RVA: 0x0013D08C File Offset: 0x0013B28C
		public override CSharpSyntaxNode VisitGroupClause(GroupClauseSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.GroupKeyword), (ExpressionSyntax)this.Visit(node.GroupExpression), (SyntaxToken)this.Visit(node.ByKeyword), (ExpressionSyntax)this.Visit(node.ByExpression));
		}

		// Token: 0x06004CAA RID: 19626 RVA: 0x0013D0E3 File Offset: 0x0013B2E3
		public override CSharpSyntaxNode VisitQueryContinuation(QueryContinuationSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.IntoKeyword), (SyntaxToken)this.Visit(node.Identifier), (QueryBodySyntax)this.Visit(node.Body));
		}

		// Token: 0x06004CAB RID: 19627 RVA: 0x0013D11E File Offset: 0x0013B31E
		public override CSharpSyntaxNode VisitOmittedArraySizeExpression(OmittedArraySizeExpressionSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.OmittedArraySizeExpressionToken));
		}

		// Token: 0x06004CAC RID: 19628 RVA: 0x0013D137 File Offset: 0x0013B337
		public override CSharpSyntaxNode VisitInterpolatedStringExpression(InterpolatedStringExpressionSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.StringStartToken), this.VisitList<InterpolatedStringContentSyntax>(node.Contents), (SyntaxToken)this.Visit(node.StringEndToken));
		}

		// Token: 0x06004CAD RID: 19629 RVA: 0x0013D16D File Offset: 0x0013B36D
		public override CSharpSyntaxNode VisitIsPatternExpression(IsPatternExpressionSyntax node)
		{
			return node.Update((ExpressionSyntax)this.Visit(node.Expression), (SyntaxToken)this.Visit(node.IsKeyword), (PatternSyntax)this.Visit(node.Pattern));
		}

		// Token: 0x06004CAE RID: 19630 RVA: 0x0013D1A8 File Offset: 0x0013B3A8
		public override CSharpSyntaxNode VisitThrowExpression(ThrowExpressionSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.ThrowKeyword), (ExpressionSyntax)this.Visit(node.Expression));
		}

		// Token: 0x06004CAF RID: 19631 RVA: 0x0013D1D2 File Offset: 0x0013B3D2
		public override CSharpSyntaxNode VisitWhenClause(WhenClauseSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.WhenKeyword), (ExpressionSyntax)this.Visit(node.Condition));
		}

		// Token: 0x06004CB0 RID: 19632 RVA: 0x0013D1FC File Offset: 0x0013B3FC
		public override CSharpSyntaxNode VisitDiscardPattern(DiscardPatternSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.UnderscoreToken));
		}

		// Token: 0x06004CB1 RID: 19633 RVA: 0x0013D215 File Offset: 0x0013B415
		public override CSharpSyntaxNode VisitDeclarationPattern(DeclarationPatternSyntax node)
		{
			return node.Update((TypeSyntax)this.Visit(node.Type), (VariableDesignationSyntax)this.Visit(node.Designation));
		}

		// Token: 0x06004CB2 RID: 19634 RVA: 0x0013D23F File Offset: 0x0013B43F
		public override CSharpSyntaxNode VisitVarPattern(VarPatternSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.VarKeyword), (VariableDesignationSyntax)this.Visit(node.Designation));
		}

		// Token: 0x06004CB3 RID: 19635 RVA: 0x0013D26C File Offset: 0x0013B46C
		public override CSharpSyntaxNode VisitRecursivePattern(RecursivePatternSyntax node)
		{
			return node.Update((TypeSyntax)this.Visit(node.Type), (PositionalPatternClauseSyntax)this.Visit(node.PositionalPatternClause), (PropertyPatternClauseSyntax)this.Visit(node.PropertyPatternClause), (VariableDesignationSyntax)this.Visit(node.Designation));
		}

		// Token: 0x06004CB4 RID: 19636 RVA: 0x0013D2C3 File Offset: 0x0013B4C3
		public override CSharpSyntaxNode VisitPositionalPatternClause(PositionalPatternClauseSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.OpenParenToken), this.VisitList<SubpatternSyntax>(node.Subpatterns), (SyntaxToken)this.Visit(node.CloseParenToken));
		}

		// Token: 0x06004CB5 RID: 19637 RVA: 0x0013D2F9 File Offset: 0x0013B4F9
		public override CSharpSyntaxNode VisitPropertyPatternClause(PropertyPatternClauseSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.OpenBraceToken), this.VisitList<SubpatternSyntax>(node.Subpatterns), (SyntaxToken)this.Visit(node.CloseBraceToken));
		}

		// Token: 0x06004CB6 RID: 19638 RVA: 0x0013D32F File Offset: 0x0013B52F
		public override CSharpSyntaxNode VisitSubpattern(SubpatternSyntax node)
		{
			return node.Update((NameColonSyntax)this.Visit(node.NameColon), (PatternSyntax)this.Visit(node.Pattern));
		}

		// Token: 0x06004CB7 RID: 19639 RVA: 0x0013D359 File Offset: 0x0013B559
		public override CSharpSyntaxNode VisitConstantPattern(ConstantPatternSyntax node)
		{
			return node.Update((ExpressionSyntax)this.Visit(node.Expression));
		}

		// Token: 0x06004CB8 RID: 19640 RVA: 0x0013D372 File Offset: 0x0013B572
		public override CSharpSyntaxNode VisitParenthesizedPattern(ParenthesizedPatternSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.OpenParenToken), (PatternSyntax)this.Visit(node.Pattern), (SyntaxToken)this.Visit(node.CloseParenToken));
		}

		// Token: 0x06004CB9 RID: 19641 RVA: 0x0013D3AD File Offset: 0x0013B5AD
		public override CSharpSyntaxNode VisitRelationalPattern(RelationalPatternSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.OperatorToken), (ExpressionSyntax)this.Visit(node.Expression));
		}

		// Token: 0x06004CBA RID: 19642 RVA: 0x0013D3D7 File Offset: 0x0013B5D7
		public override CSharpSyntaxNode VisitTypePattern(TypePatternSyntax node)
		{
			return node.Update((TypeSyntax)this.Visit(node.Type));
		}

		// Token: 0x06004CBB RID: 19643 RVA: 0x0013D3F0 File Offset: 0x0013B5F0
		public override CSharpSyntaxNode VisitBinaryPattern(BinaryPatternSyntax node)
		{
			return node.Update((PatternSyntax)this.Visit(node.Left), (SyntaxToken)this.Visit(node.OperatorToken), (PatternSyntax)this.Visit(node.Right));
		}

		// Token: 0x06004CBC RID: 19644 RVA: 0x0013D42B File Offset: 0x0013B62B
		public override CSharpSyntaxNode VisitUnaryPattern(UnaryPatternSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.OperatorToken), (PatternSyntax)this.Visit(node.Pattern));
		}

		// Token: 0x06004CBD RID: 19645 RVA: 0x0013D455 File Offset: 0x0013B655
		public override CSharpSyntaxNode VisitInterpolatedStringText(InterpolatedStringTextSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.TextToken));
		}

		// Token: 0x06004CBE RID: 19646 RVA: 0x0013D470 File Offset: 0x0013B670
		public override CSharpSyntaxNode VisitInterpolation(InterpolationSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.OpenBraceToken), (ExpressionSyntax)this.Visit(node.Expression), (InterpolationAlignmentClauseSyntax)this.Visit(node.AlignmentClause), (InterpolationFormatClauseSyntax)this.Visit(node.FormatClause), (SyntaxToken)this.Visit(node.CloseBraceToken));
		}

		// Token: 0x06004CBF RID: 19647 RVA: 0x0013D4D8 File Offset: 0x0013B6D8
		public override CSharpSyntaxNode VisitInterpolationAlignmentClause(InterpolationAlignmentClauseSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.CommaToken), (ExpressionSyntax)this.Visit(node.Value));
		}

		// Token: 0x06004CC0 RID: 19648 RVA: 0x0013D502 File Offset: 0x0013B702
		public override CSharpSyntaxNode VisitInterpolationFormatClause(InterpolationFormatClauseSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.ColonToken), (SyntaxToken)this.Visit(node.FormatStringToken));
		}

		// Token: 0x06004CC1 RID: 19649 RVA: 0x0013D52C File Offset: 0x0013B72C
		public override CSharpSyntaxNode VisitGlobalStatement(GlobalStatementSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList<SyntaxToken>(node.Modifiers), (StatementSyntax)this.Visit(node.Statement));
		}

		// Token: 0x06004CC2 RID: 19650 RVA: 0x0013D560 File Offset: 0x0013B760
		public override CSharpSyntaxNode VisitBlock(BlockSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), (SyntaxToken)this.Visit(node.OpenBraceToken), this.VisitList<StatementSyntax>(node.Statements), (SyntaxToken)this.Visit(node.CloseBraceToken));
		}

		// Token: 0x06004CC3 RID: 19651 RVA: 0x0013D5B0 File Offset: 0x0013B7B0
		public override CSharpSyntaxNode VisitLocalFunctionStatement(LocalFunctionStatementSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList<SyntaxToken>(node.Modifiers), (TypeSyntax)this.Visit(node.ReturnType), (SyntaxToken)this.Visit(node.Identifier), (TypeParameterListSyntax)this.Visit(node.TypeParameterList), (ParameterListSyntax)this.Visit(node.ParameterList), this.VisitList<TypeParameterConstraintClauseSyntax>(node.ConstraintClauses), (BlockSyntax)this.Visit(node.Body), (ArrowExpressionClauseSyntax)this.Visit(node.ExpressionBody), (SyntaxToken)this.Visit(node.SemicolonToken));
		}

		// Token: 0x06004CC4 RID: 19652 RVA: 0x0013D660 File Offset: 0x0013B860
		public override CSharpSyntaxNode VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), (SyntaxToken)this.Visit(node.AwaitKeyword), (SyntaxToken)this.Visit(node.UsingKeyword), this.VisitList<SyntaxToken>(node.Modifiers), (VariableDeclarationSyntax)this.Visit(node.Declaration), (SyntaxToken)this.Visit(node.SemicolonToken));
		}

		// Token: 0x06004CC5 RID: 19653 RVA: 0x0013D6CF File Offset: 0x0013B8CF
		public override CSharpSyntaxNode VisitVariableDeclaration(VariableDeclarationSyntax node)
		{
			return node.Update((TypeSyntax)this.Visit(node.Type), this.VisitList<VariableDeclaratorSyntax>(node.Variables));
		}

		// Token: 0x06004CC6 RID: 19654 RVA: 0x0013D6F4 File Offset: 0x0013B8F4
		public override CSharpSyntaxNode VisitVariableDeclarator(VariableDeclaratorSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.Identifier), (BracketedArgumentListSyntax)this.Visit(node.ArgumentList), (EqualsValueClauseSyntax)this.Visit(node.Initializer));
		}

		// Token: 0x06004CC7 RID: 19655 RVA: 0x0013D72F File Offset: 0x0013B92F
		public override CSharpSyntaxNode VisitEqualsValueClause(EqualsValueClauseSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.EqualsToken), (ExpressionSyntax)this.Visit(node.Value));
		}

		// Token: 0x06004CC8 RID: 19656 RVA: 0x0013D759 File Offset: 0x0013B959
		public override CSharpSyntaxNode VisitSingleVariableDesignation(SingleVariableDesignationSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.Identifier));
		}

		// Token: 0x06004CC9 RID: 19657 RVA: 0x0013D772 File Offset: 0x0013B972
		public override CSharpSyntaxNode VisitDiscardDesignation(DiscardDesignationSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.UnderscoreToken));
		}

		// Token: 0x06004CCA RID: 19658 RVA: 0x0013D78B File Offset: 0x0013B98B
		public override CSharpSyntaxNode VisitParenthesizedVariableDesignation(ParenthesizedVariableDesignationSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.OpenParenToken), this.VisitList<VariableDesignationSyntax>(node.Variables), (SyntaxToken)this.Visit(node.CloseParenToken));
		}

		// Token: 0x06004CCB RID: 19659 RVA: 0x0013D7C1 File Offset: 0x0013B9C1
		public override CSharpSyntaxNode VisitExpressionStatement(ExpressionStatementSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), (ExpressionSyntax)this.Visit(node.Expression), (SyntaxToken)this.Visit(node.SemicolonToken));
		}

		// Token: 0x06004CCC RID: 19660 RVA: 0x0013D7F7 File Offset: 0x0013B9F7
		public override CSharpSyntaxNode VisitEmptyStatement(EmptyStatementSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), (SyntaxToken)this.Visit(node.SemicolonToken));
		}

		// Token: 0x06004CCD RID: 19661 RVA: 0x0013D81C File Offset: 0x0013BA1C
		public override CSharpSyntaxNode VisitLabeledStatement(LabeledStatementSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), (SyntaxToken)this.Visit(node.Identifier), (SyntaxToken)this.Visit(node.ColonToken), (StatementSyntax)this.Visit(node.Statement));
		}

		// Token: 0x06004CCE RID: 19662 RVA: 0x0013D870 File Offset: 0x0013BA70
		public override CSharpSyntaxNode VisitGotoStatement(GotoStatementSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), (SyntaxToken)this.Visit(node.GotoKeyword), (SyntaxToken)this.Visit(node.CaseOrDefaultKeyword), (ExpressionSyntax)this.Visit(node.Expression), (SyntaxToken)this.Visit(node.SemicolonToken));
		}

		// Token: 0x06004CCF RID: 19663 RVA: 0x0013D8D3 File Offset: 0x0013BAD3
		public override CSharpSyntaxNode VisitBreakStatement(BreakStatementSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), (SyntaxToken)this.Visit(node.BreakKeyword), (SyntaxToken)this.Visit(node.SemicolonToken));
		}

		// Token: 0x06004CD0 RID: 19664 RVA: 0x0013D909 File Offset: 0x0013BB09
		public override CSharpSyntaxNode VisitContinueStatement(ContinueStatementSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), (SyntaxToken)this.Visit(node.ContinueKeyword), (SyntaxToken)this.Visit(node.SemicolonToken));
		}

		// Token: 0x06004CD1 RID: 19665 RVA: 0x0013D940 File Offset: 0x0013BB40
		public override CSharpSyntaxNode VisitReturnStatement(ReturnStatementSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), (SyntaxToken)this.Visit(node.ReturnKeyword), (ExpressionSyntax)this.Visit(node.Expression), (SyntaxToken)this.Visit(node.SemicolonToken));
		}

		// Token: 0x06004CD2 RID: 19666 RVA: 0x0013D994 File Offset: 0x0013BB94
		public override CSharpSyntaxNode VisitThrowStatement(ThrowStatementSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), (SyntaxToken)this.Visit(node.ThrowKeyword), (ExpressionSyntax)this.Visit(node.Expression), (SyntaxToken)this.Visit(node.SemicolonToken));
		}

		// Token: 0x06004CD3 RID: 19667 RVA: 0x0013D9E8 File Offset: 0x0013BBE8
		public override CSharpSyntaxNode VisitYieldStatement(YieldStatementSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), (SyntaxToken)this.Visit(node.YieldKeyword), (SyntaxToken)this.Visit(node.ReturnOrBreakKeyword), (ExpressionSyntax)this.Visit(node.Expression), (SyntaxToken)this.Visit(node.SemicolonToken));
		}

		// Token: 0x06004CD4 RID: 19668 RVA: 0x0013DA4C File Offset: 0x0013BC4C
		public override CSharpSyntaxNode VisitWhileStatement(WhileStatementSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), (SyntaxToken)this.Visit(node.WhileKeyword), (SyntaxToken)this.Visit(node.OpenParenToken), (ExpressionSyntax)this.Visit(node.Condition), (SyntaxToken)this.Visit(node.CloseParenToken), (StatementSyntax)this.Visit(node.Statement));
		}

		// Token: 0x06004CD5 RID: 19669 RVA: 0x0013DAC0 File Offset: 0x0013BCC0
		public override CSharpSyntaxNode VisitDoStatement(DoStatementSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), (SyntaxToken)this.Visit(node.DoKeyword), (StatementSyntax)this.Visit(node.Statement), (SyntaxToken)this.Visit(node.WhileKeyword), (SyntaxToken)this.Visit(node.OpenParenToken), (ExpressionSyntax)this.Visit(node.Condition), (SyntaxToken)this.Visit(node.CloseParenToken), (SyntaxToken)this.Visit(node.SemicolonToken));
		}

		// Token: 0x06004CD6 RID: 19670 RVA: 0x0013DB58 File Offset: 0x0013BD58
		public override CSharpSyntaxNode VisitForStatement(ForStatementSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), (SyntaxToken)this.Visit(node.ForKeyword), (SyntaxToken)this.Visit(node.OpenParenToken), (VariableDeclarationSyntax)this.Visit(node.Declaration), this.VisitList<ExpressionSyntax>(node.Initializers), (SyntaxToken)this.Visit(node.FirstSemicolonToken), (ExpressionSyntax)this.Visit(node.Condition), (SyntaxToken)this.Visit(node.SecondSemicolonToken), this.VisitList<ExpressionSyntax>(node.Incrementors), (SyntaxToken)this.Visit(node.CloseParenToken), (StatementSyntax)this.Visit(node.Statement));
		}

		// Token: 0x06004CD7 RID: 19671 RVA: 0x0013DC18 File Offset: 0x0013BE18
		public override CSharpSyntaxNode VisitForEachStatement(ForEachStatementSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), (SyntaxToken)this.Visit(node.AwaitKeyword), (SyntaxToken)this.Visit(node.ForEachKeyword), (SyntaxToken)this.Visit(node.OpenParenToken), (TypeSyntax)this.Visit(node.Type), (SyntaxToken)this.Visit(node.Identifier), (SyntaxToken)this.Visit(node.InKeyword), (ExpressionSyntax)this.Visit(node.Expression), (SyntaxToken)this.Visit(node.CloseParenToken), (StatementSyntax)this.Visit(node.Statement));
		}

		// Token: 0x06004CD8 RID: 19672 RVA: 0x0013DCD0 File Offset: 0x0013BED0
		public override CSharpSyntaxNode VisitForEachVariableStatement(ForEachVariableStatementSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), (SyntaxToken)this.Visit(node.AwaitKeyword), (SyntaxToken)this.Visit(node.ForEachKeyword), (SyntaxToken)this.Visit(node.OpenParenToken), (ExpressionSyntax)this.Visit(node.Variable), (SyntaxToken)this.Visit(node.InKeyword), (ExpressionSyntax)this.Visit(node.Expression), (SyntaxToken)this.Visit(node.CloseParenToken), (StatementSyntax)this.Visit(node.Statement));
		}

		// Token: 0x06004CD9 RID: 19673 RVA: 0x0013DD78 File Offset: 0x0013BF78
		public override CSharpSyntaxNode VisitUsingStatement(UsingStatementSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), (SyntaxToken)this.Visit(node.AwaitKeyword), (SyntaxToken)this.Visit(node.UsingKeyword), (SyntaxToken)this.Visit(node.OpenParenToken), (VariableDeclarationSyntax)this.Visit(node.Declaration), (ExpressionSyntax)this.Visit(node.Expression), (SyntaxToken)this.Visit(node.CloseParenToken), (StatementSyntax)this.Visit(node.Statement));
		}

		// Token: 0x06004CDA RID: 19674 RVA: 0x0013DE10 File Offset: 0x0013C010
		public override CSharpSyntaxNode VisitFixedStatement(FixedStatementSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), (SyntaxToken)this.Visit(node.FixedKeyword), (SyntaxToken)this.Visit(node.OpenParenToken), (VariableDeclarationSyntax)this.Visit(node.Declaration), (SyntaxToken)this.Visit(node.CloseParenToken), (StatementSyntax)this.Visit(node.Statement));
		}

		// Token: 0x06004CDB RID: 19675 RVA: 0x0013DE84 File Offset: 0x0013C084
		public override CSharpSyntaxNode VisitCheckedStatement(CheckedStatementSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), (SyntaxToken)this.Visit(node.Keyword), (BlockSyntax)this.Visit(node.Block));
		}

		// Token: 0x06004CDC RID: 19676 RVA: 0x0013DEBA File Offset: 0x0013C0BA
		public override CSharpSyntaxNode VisitUnsafeStatement(UnsafeStatementSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), (SyntaxToken)this.Visit(node.UnsafeKeyword), (BlockSyntax)this.Visit(node.Block));
		}

		// Token: 0x06004CDD RID: 19677 RVA: 0x0013DEF0 File Offset: 0x0013C0F0
		public override CSharpSyntaxNode VisitLockStatement(LockStatementSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), (SyntaxToken)this.Visit(node.LockKeyword), (SyntaxToken)this.Visit(node.OpenParenToken), (ExpressionSyntax)this.Visit(node.Expression), (SyntaxToken)this.Visit(node.CloseParenToken), (StatementSyntax)this.Visit(node.Statement));
		}

		// Token: 0x06004CDE RID: 19678 RVA: 0x0013DF64 File Offset: 0x0013C164
		public override CSharpSyntaxNode VisitIfStatement(IfStatementSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), (SyntaxToken)this.Visit(node.IfKeyword), (SyntaxToken)this.Visit(node.OpenParenToken), (ExpressionSyntax)this.Visit(node.Condition), (SyntaxToken)this.Visit(node.CloseParenToken), (StatementSyntax)this.Visit(node.Statement), (ElseClauseSyntax)this.Visit(node.Else));
		}

		// Token: 0x06004CDF RID: 19679 RVA: 0x0013DFE9 File Offset: 0x0013C1E9
		public override CSharpSyntaxNode VisitElseClause(ElseClauseSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.ElseKeyword), (StatementSyntax)this.Visit(node.Statement));
		}

		// Token: 0x06004CE0 RID: 19680 RVA: 0x0013E014 File Offset: 0x0013C214
		public override CSharpSyntaxNode VisitSwitchStatement(SwitchStatementSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), (SyntaxToken)this.Visit(node.SwitchKeyword), (SyntaxToken)this.Visit(node.OpenParenToken), (ExpressionSyntax)this.Visit(node.Expression), (SyntaxToken)this.Visit(node.CloseParenToken), (SyntaxToken)this.Visit(node.OpenBraceToken), this.VisitList<SwitchSectionSyntax>(node.Sections), (SyntaxToken)this.Visit(node.CloseBraceToken));
		}

		// Token: 0x06004CE1 RID: 19681 RVA: 0x0013E0A5 File Offset: 0x0013C2A5
		public override CSharpSyntaxNode VisitSwitchSection(SwitchSectionSyntax node)
		{
			return node.Update(this.VisitList<SwitchLabelSyntax>(node.Labels), this.VisitList<StatementSyntax>(node.Statements));
		}

		// Token: 0x06004CE2 RID: 19682 RVA: 0x0013E0C8 File Offset: 0x0013C2C8
		public override CSharpSyntaxNode VisitCasePatternSwitchLabel(CasePatternSwitchLabelSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.Keyword), (PatternSyntax)this.Visit(node.Pattern), (WhenClauseSyntax)this.Visit(node.WhenClause), (SyntaxToken)this.Visit(node.ColonToken));
		}

		// Token: 0x06004CE3 RID: 19683 RVA: 0x0013E11F File Offset: 0x0013C31F
		public override CSharpSyntaxNode VisitCaseSwitchLabel(CaseSwitchLabelSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.Keyword), (ExpressionSyntax)this.Visit(node.Value), (SyntaxToken)this.Visit(node.ColonToken));
		}

		// Token: 0x06004CE4 RID: 19684 RVA: 0x0013E15A File Offset: 0x0013C35A
		public override CSharpSyntaxNode VisitDefaultSwitchLabel(DefaultSwitchLabelSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.Keyword), (SyntaxToken)this.Visit(node.ColonToken));
		}

		// Token: 0x06004CE5 RID: 19685 RVA: 0x0013E184 File Offset: 0x0013C384
		public override CSharpSyntaxNode VisitSwitchExpression(SwitchExpressionSyntax node)
		{
			return node.Update((ExpressionSyntax)this.Visit(node.GoverningExpression), (SyntaxToken)this.Visit(node.SwitchKeyword), (SyntaxToken)this.Visit(node.OpenBraceToken), this.VisitList<SwitchExpressionArmSyntax>(node.Arms), (SyntaxToken)this.Visit(node.CloseBraceToken));
		}

		// Token: 0x06004CE6 RID: 19686 RVA: 0x0013E1E8 File Offset: 0x0013C3E8
		public override CSharpSyntaxNode VisitSwitchExpressionArm(SwitchExpressionArmSyntax node)
		{
			return node.Update((PatternSyntax)this.Visit(node.Pattern), (WhenClauseSyntax)this.Visit(node.WhenClause), (SyntaxToken)this.Visit(node.EqualsGreaterThanToken), (ExpressionSyntax)this.Visit(node.Expression));
		}

		// Token: 0x06004CE7 RID: 19687 RVA: 0x0013E240 File Offset: 0x0013C440
		public override CSharpSyntaxNode VisitTryStatement(TryStatementSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), (SyntaxToken)this.Visit(node.TryKeyword), (BlockSyntax)this.Visit(node.Block), this.VisitList<CatchClauseSyntax>(node.Catches), (FinallyClauseSyntax)this.Visit(node.Finally));
		}

		// Token: 0x06004CE8 RID: 19688 RVA: 0x0013E2A0 File Offset: 0x0013C4A0
		public override CSharpSyntaxNode VisitCatchClause(CatchClauseSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.CatchKeyword), (CatchDeclarationSyntax)this.Visit(node.Declaration), (CatchFilterClauseSyntax)this.Visit(node.Filter), (BlockSyntax)this.Visit(node.Block));
		}

		// Token: 0x06004CE9 RID: 19689 RVA: 0x0013E2F8 File Offset: 0x0013C4F8
		public override CSharpSyntaxNode VisitCatchDeclaration(CatchDeclarationSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.OpenParenToken), (TypeSyntax)this.Visit(node.Type), (SyntaxToken)this.Visit(node.Identifier), (SyntaxToken)this.Visit(node.CloseParenToken));
		}

		// Token: 0x06004CEA RID: 19690 RVA: 0x0013E350 File Offset: 0x0013C550
		public override CSharpSyntaxNode VisitCatchFilterClause(CatchFilterClauseSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.WhenKeyword), (SyntaxToken)this.Visit(node.OpenParenToken), (ExpressionSyntax)this.Visit(node.FilterExpression), (SyntaxToken)this.Visit(node.CloseParenToken));
		}

		// Token: 0x06004CEB RID: 19691 RVA: 0x0013E3A7 File Offset: 0x0013C5A7
		public override CSharpSyntaxNode VisitFinallyClause(FinallyClauseSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.FinallyKeyword), (BlockSyntax)this.Visit(node.Block));
		}

		// Token: 0x06004CEC RID: 19692 RVA: 0x0013E3D4 File Offset: 0x0013C5D4
		public override CSharpSyntaxNode VisitCompilationUnit(CompilationUnitSyntax node)
		{
			return node.Update(this.VisitList<ExternAliasDirectiveSyntax>(node.Externs), this.VisitList<UsingDirectiveSyntax>(node.Usings), this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList<MemberDeclarationSyntax>(node.Members), (SyntaxToken)this.Visit(node.EndOfFileToken));
		}

		// Token: 0x06004CED RID: 19693 RVA: 0x0013E428 File Offset: 0x0013C628
		public override CSharpSyntaxNode VisitExternAliasDirective(ExternAliasDirectiveSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.ExternKeyword), (SyntaxToken)this.Visit(node.AliasKeyword), (SyntaxToken)this.Visit(node.Identifier), (SyntaxToken)this.Visit(node.SemicolonToken));
		}

		// Token: 0x06004CEE RID: 19694 RVA: 0x0013E480 File Offset: 0x0013C680
		public override CSharpSyntaxNode VisitUsingDirective(UsingDirectiveSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.GlobalKeyword), (SyntaxToken)this.Visit(node.UsingKeyword), (SyntaxToken)this.Visit(node.StaticKeyword), (NameEqualsSyntax)this.Visit(node.Alias), (NameSyntax)this.Visit(node.Name), (SyntaxToken)this.Visit(node.SemicolonToken));
		}

		// Token: 0x06004CEF RID: 19695 RVA: 0x0013E4FC File Offset: 0x0013C6FC
		public override CSharpSyntaxNode VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList<SyntaxToken>(node.Modifiers), (SyntaxToken)this.Visit(node.NamespaceKeyword), (NameSyntax)this.Visit(node.Name), (SyntaxToken)this.Visit(node.OpenBraceToken), this.VisitList<ExternAliasDirectiveSyntax>(node.Externs), this.VisitList<UsingDirectiveSyntax>(node.Usings), this.VisitList<MemberDeclarationSyntax>(node.Members), (SyntaxToken)this.Visit(node.CloseBraceToken), (SyntaxToken)this.Visit(node.SemicolonToken));
		}

		// Token: 0x06004CF0 RID: 19696 RVA: 0x0013E5A0 File Offset: 0x0013C7A0
		public override CSharpSyntaxNode VisitAttributeList(AttributeListSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.OpenBracketToken), (AttributeTargetSpecifierSyntax)this.Visit(node.Target), this.VisitList<AttributeSyntax>(node.Attributes), (SyntaxToken)this.Visit(node.CloseBracketToken));
		}

		// Token: 0x06004CF1 RID: 19697 RVA: 0x0013E5F2 File Offset: 0x0013C7F2
		public override CSharpSyntaxNode VisitAttributeTargetSpecifier(AttributeTargetSpecifierSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.Identifier), (SyntaxToken)this.Visit(node.ColonToken));
		}

		// Token: 0x06004CF2 RID: 19698 RVA: 0x0013E61C File Offset: 0x0013C81C
		public override CSharpSyntaxNode VisitAttribute(AttributeSyntax node)
		{
			return node.Update((NameSyntax)this.Visit(node.Name), (AttributeArgumentListSyntax)this.Visit(node.ArgumentList));
		}

		// Token: 0x06004CF3 RID: 19699 RVA: 0x0013E646 File Offset: 0x0013C846
		public override CSharpSyntaxNode VisitAttributeArgumentList(AttributeArgumentListSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.OpenParenToken), this.VisitList<AttributeArgumentSyntax>(node.Arguments), (SyntaxToken)this.Visit(node.CloseParenToken));
		}

		// Token: 0x06004CF4 RID: 19700 RVA: 0x0013E67C File Offset: 0x0013C87C
		public override CSharpSyntaxNode VisitAttributeArgument(AttributeArgumentSyntax node)
		{
			return node.Update((NameEqualsSyntax)this.Visit(node.NameEquals), (NameColonSyntax)this.Visit(node.NameColon), (ExpressionSyntax)this.Visit(node.Expression));
		}

		// Token: 0x06004CF5 RID: 19701 RVA: 0x0013E6B7 File Offset: 0x0013C8B7
		public override CSharpSyntaxNode VisitNameEquals(NameEqualsSyntax node)
		{
			return node.Update((IdentifierNameSyntax)this.Visit(node.Name), (SyntaxToken)this.Visit(node.EqualsToken));
		}

		// Token: 0x06004CF6 RID: 19702 RVA: 0x0013E6E1 File Offset: 0x0013C8E1
		public override CSharpSyntaxNode VisitTypeParameterList(TypeParameterListSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.LessThanToken), this.VisitList<TypeParameterSyntax>(node.Parameters), (SyntaxToken)this.Visit(node.GreaterThanToken));
		}

		// Token: 0x06004CF7 RID: 19703 RVA: 0x0013E717 File Offset: 0x0013C917
		public override CSharpSyntaxNode VisitTypeParameter(TypeParameterSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), (SyntaxToken)this.Visit(node.VarianceKeyword), (SyntaxToken)this.Visit(node.Identifier));
		}

		// Token: 0x06004CF8 RID: 19704 RVA: 0x0013E750 File Offset: 0x0013C950
		public override CSharpSyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList<SyntaxToken>(node.Modifiers), (SyntaxToken)this.Visit(node.Keyword), (SyntaxToken)this.Visit(node.Identifier), (TypeParameterListSyntax)this.Visit(node.TypeParameterList), (BaseListSyntax)this.Visit(node.BaseList), this.VisitList<TypeParameterConstraintClauseSyntax>(node.ConstraintClauses), (SyntaxToken)this.Visit(node.OpenBraceToken), this.VisitList<MemberDeclarationSyntax>(node.Members), (SyntaxToken)this.Visit(node.CloseBraceToken), (SyntaxToken)this.Visit(node.SemicolonToken));
		}

		// Token: 0x06004CF9 RID: 19705 RVA: 0x0013E80C File Offset: 0x0013CA0C
		public override CSharpSyntaxNode VisitStructDeclaration(StructDeclarationSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList<SyntaxToken>(node.Modifiers), (SyntaxToken)this.Visit(node.Keyword), (SyntaxToken)this.Visit(node.Identifier), (TypeParameterListSyntax)this.Visit(node.TypeParameterList), (BaseListSyntax)this.Visit(node.BaseList), this.VisitList<TypeParameterConstraintClauseSyntax>(node.ConstraintClauses), (SyntaxToken)this.Visit(node.OpenBraceToken), this.VisitList<MemberDeclarationSyntax>(node.Members), (SyntaxToken)this.Visit(node.CloseBraceToken), (SyntaxToken)this.Visit(node.SemicolonToken));
		}

		// Token: 0x06004CFA RID: 19706 RVA: 0x0013E8C8 File Offset: 0x0013CAC8
		public override CSharpSyntaxNode VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList<SyntaxToken>(node.Modifiers), (SyntaxToken)this.Visit(node.Keyword), (SyntaxToken)this.Visit(node.Identifier), (TypeParameterListSyntax)this.Visit(node.TypeParameterList), (BaseListSyntax)this.Visit(node.BaseList), this.VisitList<TypeParameterConstraintClauseSyntax>(node.ConstraintClauses), (SyntaxToken)this.Visit(node.OpenBraceToken), this.VisitList<MemberDeclarationSyntax>(node.Members), (SyntaxToken)this.Visit(node.CloseBraceToken), (SyntaxToken)this.Visit(node.SemicolonToken));
		}

		// Token: 0x06004CFB RID: 19707 RVA: 0x0013E984 File Offset: 0x0013CB84
		public override CSharpSyntaxNode VisitRecordDeclaration(RecordDeclarationSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList<SyntaxToken>(node.Modifiers), (SyntaxToken)this.Visit(node.Keyword), (SyntaxToken)this.Visit(node.ClassOrStructKeyword), (SyntaxToken)this.Visit(node.Identifier), (TypeParameterListSyntax)this.Visit(node.TypeParameterList), (ParameterListSyntax)this.Visit(node.ParameterList), (BaseListSyntax)this.Visit(node.BaseList), this.VisitList<TypeParameterConstraintClauseSyntax>(node.ConstraintClauses), (SyntaxToken)this.Visit(node.OpenBraceToken), this.VisitList<MemberDeclarationSyntax>(node.Members), (SyntaxToken)this.Visit(node.CloseBraceToken), (SyntaxToken)this.Visit(node.SemicolonToken));
		}

		// Token: 0x06004CFC RID: 19708 RVA: 0x0013EA60 File Offset: 0x0013CC60
		public override CSharpSyntaxNode VisitEnumDeclaration(EnumDeclarationSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList<SyntaxToken>(node.Modifiers), (SyntaxToken)this.Visit(node.EnumKeyword), (SyntaxToken)this.Visit(node.Identifier), (BaseListSyntax)this.Visit(node.BaseList), (SyntaxToken)this.Visit(node.OpenBraceToken), this.VisitList<EnumMemberDeclarationSyntax>(node.Members), (SyntaxToken)this.Visit(node.CloseBraceToken), (SyntaxToken)this.Visit(node.SemicolonToken));
		}

		// Token: 0x06004CFD RID: 19709 RVA: 0x0013EB00 File Offset: 0x0013CD00
		public override CSharpSyntaxNode VisitDelegateDeclaration(DelegateDeclarationSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList<SyntaxToken>(node.Modifiers), (SyntaxToken)this.Visit(node.DelegateKeyword), (TypeSyntax)this.Visit(node.ReturnType), (SyntaxToken)this.Visit(node.Identifier), (TypeParameterListSyntax)this.Visit(node.TypeParameterList), (ParameterListSyntax)this.Visit(node.ParameterList), this.VisitList<TypeParameterConstraintClauseSyntax>(node.ConstraintClauses), (SyntaxToken)this.Visit(node.SemicolonToken));
		}

		// Token: 0x06004CFE RID: 19710 RVA: 0x0013EBA0 File Offset: 0x0013CDA0
		public override CSharpSyntaxNode VisitEnumMemberDeclaration(EnumMemberDeclarationSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList<SyntaxToken>(node.Modifiers), (SyntaxToken)this.Visit(node.Identifier), (EqualsValueClauseSyntax)this.Visit(node.EqualsValue));
		}

		// Token: 0x06004CFF RID: 19711 RVA: 0x0013EBED File Offset: 0x0013CDED
		public override CSharpSyntaxNode VisitBaseList(BaseListSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.ColonToken), this.VisitList<BaseTypeSyntax>(node.Types));
		}

		// Token: 0x06004D00 RID: 19712 RVA: 0x0013EC12 File Offset: 0x0013CE12
		public override CSharpSyntaxNode VisitSimpleBaseType(SimpleBaseTypeSyntax node)
		{
			return node.Update((TypeSyntax)this.Visit(node.Type));
		}

		// Token: 0x06004D01 RID: 19713 RVA: 0x0013EC2B File Offset: 0x0013CE2B
		public override CSharpSyntaxNode VisitPrimaryConstructorBaseType(PrimaryConstructorBaseTypeSyntax node)
		{
			return node.Update((TypeSyntax)this.Visit(node.Type), (ArgumentListSyntax)this.Visit(node.ArgumentList));
		}

		// Token: 0x06004D02 RID: 19714 RVA: 0x0013EC58 File Offset: 0x0013CE58
		public override CSharpSyntaxNode VisitTypeParameterConstraintClause(TypeParameterConstraintClauseSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.WhereKeyword), (IdentifierNameSyntax)this.Visit(node.Name), (SyntaxToken)this.Visit(node.ColonToken), this.VisitList<TypeParameterConstraintSyntax>(node.Constraints));
		}

		// Token: 0x06004D03 RID: 19715 RVA: 0x0013ECAA File Offset: 0x0013CEAA
		public override CSharpSyntaxNode VisitConstructorConstraint(ConstructorConstraintSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.NewKeyword), (SyntaxToken)this.Visit(node.OpenParenToken), (SyntaxToken)this.Visit(node.CloseParenToken));
		}

		// Token: 0x06004D04 RID: 19716 RVA: 0x0013ECE5 File Offset: 0x0013CEE5
		public override CSharpSyntaxNode VisitClassOrStructConstraint(ClassOrStructConstraintSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.ClassOrStructKeyword), (SyntaxToken)this.Visit(node.QuestionToken));
		}

		// Token: 0x06004D05 RID: 19717 RVA: 0x0013ED0F File Offset: 0x0013CF0F
		public override CSharpSyntaxNode VisitTypeConstraint(TypeConstraintSyntax node)
		{
			return node.Update((TypeSyntax)this.Visit(node.Type));
		}

		// Token: 0x06004D06 RID: 19718 RVA: 0x0013ED28 File Offset: 0x0013CF28
		public override CSharpSyntaxNode VisitDefaultConstraint(DefaultConstraintSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.DefaultKeyword));
		}

		// Token: 0x06004D07 RID: 19719 RVA: 0x0013ED44 File Offset: 0x0013CF44
		public override CSharpSyntaxNode VisitFieldDeclaration(FieldDeclarationSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList<SyntaxToken>(node.Modifiers), (VariableDeclarationSyntax)this.Visit(node.Declaration), (SyntaxToken)this.Visit(node.SemicolonToken));
		}

		// Token: 0x06004D08 RID: 19720 RVA: 0x0013ED94 File Offset: 0x0013CF94
		public override CSharpSyntaxNode VisitEventFieldDeclaration(EventFieldDeclarationSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList<SyntaxToken>(node.Modifiers), (SyntaxToken)this.Visit(node.EventKeyword), (VariableDeclarationSyntax)this.Visit(node.Declaration), (SyntaxToken)this.Visit(node.SemicolonToken));
		}

		// Token: 0x06004D09 RID: 19721 RVA: 0x0013EDF2 File Offset: 0x0013CFF2
		public override CSharpSyntaxNode VisitExplicitInterfaceSpecifier(ExplicitInterfaceSpecifierSyntax node)
		{
			return node.Update((NameSyntax)this.Visit(node.Name), (SyntaxToken)this.Visit(node.DotToken));
		}

		// Token: 0x06004D0A RID: 19722 RVA: 0x0013EE1C File Offset: 0x0013D01C
		public override CSharpSyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList<SyntaxToken>(node.Modifiers), (TypeSyntax)this.Visit(node.ReturnType), (ExplicitInterfaceSpecifierSyntax)this.Visit(node.ExplicitInterfaceSpecifier), (SyntaxToken)this.Visit(node.Identifier), (TypeParameterListSyntax)this.Visit(node.TypeParameterList), (ParameterListSyntax)this.Visit(node.ParameterList), this.VisitList<TypeParameterConstraintClauseSyntax>(node.ConstraintClauses), (BlockSyntax)this.Visit(node.Body), (ArrowExpressionClauseSyntax)this.Visit(node.ExpressionBody), (SyntaxToken)this.Visit(node.SemicolonToken));
		}

		// Token: 0x06004D0B RID: 19723 RVA: 0x0013EEDC File Offset: 0x0013D0DC
		public override CSharpSyntaxNode VisitOperatorDeclaration(OperatorDeclarationSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList<SyntaxToken>(node.Modifiers), (TypeSyntax)this.Visit(node.ReturnType), (SyntaxToken)this.Visit(node.OperatorKeyword), (SyntaxToken)this.Visit(node.OperatorToken), (ParameterListSyntax)this.Visit(node.ParameterList), (BlockSyntax)this.Visit(node.Body), (ArrowExpressionClauseSyntax)this.Visit(node.ExpressionBody), (SyntaxToken)this.Visit(node.SemicolonToken));
		}

		// Token: 0x06004D0C RID: 19724 RVA: 0x0013EF80 File Offset: 0x0013D180
		public override CSharpSyntaxNode VisitConversionOperatorDeclaration(ConversionOperatorDeclarationSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList<SyntaxToken>(node.Modifiers), (SyntaxToken)this.Visit(node.ImplicitOrExplicitKeyword), (SyntaxToken)this.Visit(node.OperatorKeyword), (TypeSyntax)this.Visit(node.Type), (ParameterListSyntax)this.Visit(node.ParameterList), (BlockSyntax)this.Visit(node.Body), (ArrowExpressionClauseSyntax)this.Visit(node.ExpressionBody), (SyntaxToken)this.Visit(node.SemicolonToken));
		}

		// Token: 0x06004D0D RID: 19725 RVA: 0x0013F024 File Offset: 0x0013D224
		public override CSharpSyntaxNode VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList<SyntaxToken>(node.Modifiers), (SyntaxToken)this.Visit(node.Identifier), (ParameterListSyntax)this.Visit(node.ParameterList), (ConstructorInitializerSyntax)this.Visit(node.Initializer), (BlockSyntax)this.Visit(node.Body), (ArrowExpressionClauseSyntax)this.Visit(node.ExpressionBody), (SyntaxToken)this.Visit(node.SemicolonToken));
		}

		// Token: 0x06004D0E RID: 19726 RVA: 0x0013F0B5 File Offset: 0x0013D2B5
		public override CSharpSyntaxNode VisitConstructorInitializer(ConstructorInitializerSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.ColonToken), (SyntaxToken)this.Visit(node.ThisOrBaseKeyword), (ArgumentListSyntax)this.Visit(node.ArgumentList));
		}

		// Token: 0x06004D0F RID: 19727 RVA: 0x0013F0F0 File Offset: 0x0013D2F0
		public override CSharpSyntaxNode VisitDestructorDeclaration(DestructorDeclarationSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList<SyntaxToken>(node.Modifiers), (SyntaxToken)this.Visit(node.TildeToken), (SyntaxToken)this.Visit(node.Identifier), (ParameterListSyntax)this.Visit(node.ParameterList), (BlockSyntax)this.Visit(node.Body), (ArrowExpressionClauseSyntax)this.Visit(node.ExpressionBody), (SyntaxToken)this.Visit(node.SemicolonToken));
		}

		// Token: 0x06004D10 RID: 19728 RVA: 0x0013F184 File Offset: 0x0013D384
		public override CSharpSyntaxNode VisitPropertyDeclaration(PropertyDeclarationSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList<SyntaxToken>(node.Modifiers), (TypeSyntax)this.Visit(node.Type), (ExplicitInterfaceSpecifierSyntax)this.Visit(node.ExplicitInterfaceSpecifier), (SyntaxToken)this.Visit(node.Identifier), (AccessorListSyntax)this.Visit(node.AccessorList), (ArrowExpressionClauseSyntax)this.Visit(node.ExpressionBody), (EqualsValueClauseSyntax)this.Visit(node.Initializer), (SyntaxToken)this.Visit(node.SemicolonToken));
		}

		// Token: 0x06004D11 RID: 19729 RVA: 0x0013F226 File Offset: 0x0013D426
		public override CSharpSyntaxNode VisitArrowExpressionClause(ArrowExpressionClauseSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.ArrowToken), (ExpressionSyntax)this.Visit(node.Expression));
		}

		// Token: 0x06004D12 RID: 19730 RVA: 0x0013F250 File Offset: 0x0013D450
		public override CSharpSyntaxNode VisitEventDeclaration(EventDeclarationSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList<SyntaxToken>(node.Modifiers), (SyntaxToken)this.Visit(node.EventKeyword), (TypeSyntax)this.Visit(node.Type), (ExplicitInterfaceSpecifierSyntax)this.Visit(node.ExplicitInterfaceSpecifier), (SyntaxToken)this.Visit(node.Identifier), (AccessorListSyntax)this.Visit(node.AccessorList), (SyntaxToken)this.Visit(node.SemicolonToken));
		}

		// Token: 0x06004D13 RID: 19731 RVA: 0x0013F2E4 File Offset: 0x0013D4E4
		public override CSharpSyntaxNode VisitIndexerDeclaration(IndexerDeclarationSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList<SyntaxToken>(node.Modifiers), (TypeSyntax)this.Visit(node.Type), (ExplicitInterfaceSpecifierSyntax)this.Visit(node.ExplicitInterfaceSpecifier), (SyntaxToken)this.Visit(node.ThisKeyword), (BracketedParameterListSyntax)this.Visit(node.ParameterList), (AccessorListSyntax)this.Visit(node.AccessorList), (ArrowExpressionClauseSyntax)this.Visit(node.ExpressionBody), (SyntaxToken)this.Visit(node.SemicolonToken));
		}

		// Token: 0x06004D14 RID: 19732 RVA: 0x0013F386 File Offset: 0x0013D586
		public override CSharpSyntaxNode VisitAccessorList(AccessorListSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.OpenBraceToken), this.VisitList<AccessorDeclarationSyntax>(node.Accessors), (SyntaxToken)this.Visit(node.CloseBraceToken));
		}

		// Token: 0x06004D15 RID: 19733 RVA: 0x0013F3BC File Offset: 0x0013D5BC
		public override CSharpSyntaxNode VisitAccessorDeclaration(AccessorDeclarationSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList<SyntaxToken>(node.Modifiers), (SyntaxToken)this.Visit(node.Keyword), (BlockSyntax)this.Visit(node.Body), (ArrowExpressionClauseSyntax)this.Visit(node.ExpressionBody), (SyntaxToken)this.Visit(node.SemicolonToken));
		}

		// Token: 0x06004D16 RID: 19734 RVA: 0x0013F42B File Offset: 0x0013D62B
		public override CSharpSyntaxNode VisitParameterList(ParameterListSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.OpenParenToken), this.VisitList<ParameterSyntax>(node.Parameters), (SyntaxToken)this.Visit(node.CloseParenToken));
		}

		// Token: 0x06004D17 RID: 19735 RVA: 0x0013F461 File Offset: 0x0013D661
		public override CSharpSyntaxNode VisitBracketedParameterList(BracketedParameterListSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.OpenBracketToken), this.VisitList<ParameterSyntax>(node.Parameters), (SyntaxToken)this.Visit(node.CloseBracketToken));
		}

		// Token: 0x06004D18 RID: 19736 RVA: 0x0013F498 File Offset: 0x0013D698
		public override CSharpSyntaxNode VisitParameter(ParameterSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList<SyntaxToken>(node.Modifiers), (TypeSyntax)this.Visit(node.Type), (SyntaxToken)this.Visit(node.Identifier), (EqualsValueClauseSyntax)this.Visit(node.Default));
		}

		// Token: 0x06004D19 RID: 19737 RVA: 0x0013F4F6 File Offset: 0x0013D6F6
		public override CSharpSyntaxNode VisitFunctionPointerParameter(FunctionPointerParameterSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList<SyntaxToken>(node.Modifiers), (TypeSyntax)this.Visit(node.Type));
		}

		// Token: 0x06004D1A RID: 19738 RVA: 0x0013F527 File Offset: 0x0013D727
		public override CSharpSyntaxNode VisitIncompleteMember(IncompleteMemberSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList<SyntaxToken>(node.Modifiers), (TypeSyntax)this.Visit(node.Type));
		}

		// Token: 0x06004D1B RID: 19739 RVA: 0x0013F558 File Offset: 0x0013D758
		public override CSharpSyntaxNode VisitSkippedTokensTrivia(SkippedTokensTriviaSyntax node)
		{
			return node.Update(this.VisitList<SyntaxToken>(node.Tokens));
		}

		// Token: 0x06004D1C RID: 19740 RVA: 0x0013F56C File Offset: 0x0013D76C
		public override CSharpSyntaxNode VisitDocumentationCommentTrivia(DocumentationCommentTriviaSyntax node)
		{
			return node.Update(this.VisitList<XmlNodeSyntax>(node.Content), (SyntaxToken)this.Visit(node.EndOfComment));
		}

		// Token: 0x06004D1D RID: 19741 RVA: 0x0013F591 File Offset: 0x0013D791
		public override CSharpSyntaxNode VisitTypeCref(TypeCrefSyntax node)
		{
			return node.Update((TypeSyntax)this.Visit(node.Type));
		}

		// Token: 0x06004D1E RID: 19742 RVA: 0x0013F5AA File Offset: 0x0013D7AA
		public override CSharpSyntaxNode VisitQualifiedCref(QualifiedCrefSyntax node)
		{
			return node.Update((TypeSyntax)this.Visit(node.Container), (SyntaxToken)this.Visit(node.DotToken), (MemberCrefSyntax)this.Visit(node.Member));
		}

		// Token: 0x06004D1F RID: 19743 RVA: 0x0013F5E5 File Offset: 0x0013D7E5
		public override CSharpSyntaxNode VisitNameMemberCref(NameMemberCrefSyntax node)
		{
			return node.Update((TypeSyntax)this.Visit(node.Name), (CrefParameterListSyntax)this.Visit(node.Parameters));
		}

		// Token: 0x06004D20 RID: 19744 RVA: 0x0013F60F File Offset: 0x0013D80F
		public override CSharpSyntaxNode VisitIndexerMemberCref(IndexerMemberCrefSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.ThisKeyword), (CrefBracketedParameterListSyntax)this.Visit(node.Parameters));
		}

		// Token: 0x06004D21 RID: 19745 RVA: 0x0013F639 File Offset: 0x0013D839
		public override CSharpSyntaxNode VisitOperatorMemberCref(OperatorMemberCrefSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.OperatorKeyword), (SyntaxToken)this.Visit(node.OperatorToken), (CrefParameterListSyntax)this.Visit(node.Parameters));
		}

		// Token: 0x06004D22 RID: 19746 RVA: 0x0013F674 File Offset: 0x0013D874
		public override CSharpSyntaxNode VisitConversionOperatorMemberCref(ConversionOperatorMemberCrefSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.ImplicitOrExplicitKeyword), (SyntaxToken)this.Visit(node.OperatorKeyword), (TypeSyntax)this.Visit(node.Type), (CrefParameterListSyntax)this.Visit(node.Parameters));
		}

		// Token: 0x06004D23 RID: 19747 RVA: 0x0013F6CB File Offset: 0x0013D8CB
		public override CSharpSyntaxNode VisitCrefParameterList(CrefParameterListSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.OpenParenToken), this.VisitList<CrefParameterSyntax>(node.Parameters), (SyntaxToken)this.Visit(node.CloseParenToken));
		}

		// Token: 0x06004D24 RID: 19748 RVA: 0x0013F701 File Offset: 0x0013D901
		public override CSharpSyntaxNode VisitCrefBracketedParameterList(CrefBracketedParameterListSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.OpenBracketToken), this.VisitList<CrefParameterSyntax>(node.Parameters), (SyntaxToken)this.Visit(node.CloseBracketToken));
		}

		// Token: 0x06004D25 RID: 19749 RVA: 0x0013F737 File Offset: 0x0013D937
		public override CSharpSyntaxNode VisitCrefParameter(CrefParameterSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.RefKindKeyword), (TypeSyntax)this.Visit(node.Type));
		}

		// Token: 0x06004D26 RID: 19750 RVA: 0x0013F761 File Offset: 0x0013D961
		public override CSharpSyntaxNode VisitXmlElement(XmlElementSyntax node)
		{
			return node.Update((XmlElementStartTagSyntax)this.Visit(node.StartTag), this.VisitList<XmlNodeSyntax>(node.Content), (XmlElementEndTagSyntax)this.Visit(node.EndTag));
		}

		// Token: 0x06004D27 RID: 19751 RVA: 0x0013F798 File Offset: 0x0013D998
		public override CSharpSyntaxNode VisitXmlElementStartTag(XmlElementStartTagSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.LessThanToken), (XmlNameSyntax)this.Visit(node.Name), this.VisitList<XmlAttributeSyntax>(node.Attributes), (SyntaxToken)this.Visit(node.GreaterThanToken));
		}

		// Token: 0x06004D28 RID: 19752 RVA: 0x0013F7EA File Offset: 0x0013D9EA
		public override CSharpSyntaxNode VisitXmlElementEndTag(XmlElementEndTagSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.LessThanSlashToken), (XmlNameSyntax)this.Visit(node.Name), (SyntaxToken)this.Visit(node.GreaterThanToken));
		}

		// Token: 0x06004D29 RID: 19753 RVA: 0x0013F828 File Offset: 0x0013DA28
		public override CSharpSyntaxNode VisitXmlEmptyElement(XmlEmptyElementSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.LessThanToken), (XmlNameSyntax)this.Visit(node.Name), this.VisitList<XmlAttributeSyntax>(node.Attributes), (SyntaxToken)this.Visit(node.SlashGreaterThanToken));
		}

		// Token: 0x06004D2A RID: 19754 RVA: 0x0013F87A File Offset: 0x0013DA7A
		public override CSharpSyntaxNode VisitXmlName(XmlNameSyntax node)
		{
			return node.Update((XmlPrefixSyntax)this.Visit(node.Prefix), (SyntaxToken)this.Visit(node.LocalName));
		}

		// Token: 0x06004D2B RID: 19755 RVA: 0x0013F8A4 File Offset: 0x0013DAA4
		public override CSharpSyntaxNode VisitXmlPrefix(XmlPrefixSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.Prefix), (SyntaxToken)this.Visit(node.ColonToken));
		}

		// Token: 0x06004D2C RID: 19756 RVA: 0x0013F8D0 File Offset: 0x0013DAD0
		public override CSharpSyntaxNode VisitXmlTextAttribute(XmlTextAttributeSyntax node)
		{
			return node.Update((XmlNameSyntax)this.Visit(node.Name), (SyntaxToken)this.Visit(node.EqualsToken), (SyntaxToken)this.Visit(node.StartQuoteToken), this.VisitList<SyntaxToken>(node.TextTokens), (SyntaxToken)this.Visit(node.EndQuoteToken));
		}

		// Token: 0x06004D2D RID: 19757 RVA: 0x0013F934 File Offset: 0x0013DB34
		public override CSharpSyntaxNode VisitXmlCrefAttribute(XmlCrefAttributeSyntax node)
		{
			return node.Update((XmlNameSyntax)this.Visit(node.Name), (SyntaxToken)this.Visit(node.EqualsToken), (SyntaxToken)this.Visit(node.StartQuoteToken), (CrefSyntax)this.Visit(node.Cref), (SyntaxToken)this.Visit(node.EndQuoteToken));
		}

		// Token: 0x06004D2E RID: 19758 RVA: 0x0013F99C File Offset: 0x0013DB9C
		public override CSharpSyntaxNode VisitXmlNameAttribute(XmlNameAttributeSyntax node)
		{
			return node.Update((XmlNameSyntax)this.Visit(node.Name), (SyntaxToken)this.Visit(node.EqualsToken), (SyntaxToken)this.Visit(node.StartQuoteToken), (IdentifierNameSyntax)this.Visit(node.Identifier), (SyntaxToken)this.Visit(node.EndQuoteToken));
		}

		// Token: 0x06004D2F RID: 19759 RVA: 0x0013FA04 File Offset: 0x0013DC04
		public override CSharpSyntaxNode VisitXmlText(XmlTextSyntax node)
		{
			return node.Update(this.VisitList<SyntaxToken>(node.TextTokens));
		}

		// Token: 0x06004D30 RID: 19760 RVA: 0x0013FA18 File Offset: 0x0013DC18
		public override CSharpSyntaxNode VisitXmlCDataSection(XmlCDataSectionSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.StartCDataToken), this.VisitList<SyntaxToken>(node.TextTokens), (SyntaxToken)this.Visit(node.EndCDataToken));
		}

		// Token: 0x06004D31 RID: 19761 RVA: 0x0013FA50 File Offset: 0x0013DC50
		public override CSharpSyntaxNode VisitXmlProcessingInstruction(XmlProcessingInstructionSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.StartProcessingInstructionToken), (XmlNameSyntax)this.Visit(node.Name), this.VisitList<SyntaxToken>(node.TextTokens), (SyntaxToken)this.Visit(node.EndProcessingInstructionToken));
		}

		// Token: 0x06004D32 RID: 19762 RVA: 0x0013FAA2 File Offset: 0x0013DCA2
		public override CSharpSyntaxNode VisitXmlComment(XmlCommentSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.LessThanExclamationMinusMinusToken), this.VisitList<SyntaxToken>(node.TextTokens), (SyntaxToken)this.Visit(node.MinusMinusGreaterThanToken));
		}

		// Token: 0x06004D33 RID: 19763 RVA: 0x0013FAD8 File Offset: 0x0013DCD8
		public override CSharpSyntaxNode VisitIfDirectiveTrivia(IfDirectiveTriviaSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.HashToken), (SyntaxToken)this.Visit(node.IfKeyword), (ExpressionSyntax)this.Visit(node.Condition), (SyntaxToken)this.Visit(node.EndOfDirectiveToken), node.IsActive, node.BranchTaken, node.ConditionValue);
		}

		// Token: 0x06004D34 RID: 19764 RVA: 0x0013FB44 File Offset: 0x0013DD44
		public override CSharpSyntaxNode VisitElifDirectiveTrivia(ElifDirectiveTriviaSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.HashToken), (SyntaxToken)this.Visit(node.ElifKeyword), (ExpressionSyntax)this.Visit(node.Condition), (SyntaxToken)this.Visit(node.EndOfDirectiveToken), node.IsActive, node.BranchTaken, node.ConditionValue);
		}

		// Token: 0x06004D35 RID: 19765 RVA: 0x0013FBB0 File Offset: 0x0013DDB0
		public override CSharpSyntaxNode VisitElseDirectiveTrivia(ElseDirectiveTriviaSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.HashToken), (SyntaxToken)this.Visit(node.ElseKeyword), (SyntaxToken)this.Visit(node.EndOfDirectiveToken), node.IsActive, node.BranchTaken);
		}

		// Token: 0x06004D36 RID: 19766 RVA: 0x0013FC04 File Offset: 0x0013DE04
		public override CSharpSyntaxNode VisitEndIfDirectiveTrivia(EndIfDirectiveTriviaSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.HashToken), (SyntaxToken)this.Visit(node.EndIfKeyword), (SyntaxToken)this.Visit(node.EndOfDirectiveToken), node.IsActive);
		}

		// Token: 0x06004D37 RID: 19767 RVA: 0x0013FC50 File Offset: 0x0013DE50
		public override CSharpSyntaxNode VisitRegionDirectiveTrivia(RegionDirectiveTriviaSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.HashToken), (SyntaxToken)this.Visit(node.RegionKeyword), (SyntaxToken)this.Visit(node.EndOfDirectiveToken), node.IsActive);
		}

		// Token: 0x06004D38 RID: 19768 RVA: 0x0013FC9C File Offset: 0x0013DE9C
		public override CSharpSyntaxNode VisitEndRegionDirectiveTrivia(EndRegionDirectiveTriviaSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.HashToken), (SyntaxToken)this.Visit(node.EndRegionKeyword), (SyntaxToken)this.Visit(node.EndOfDirectiveToken), node.IsActive);
		}

		// Token: 0x06004D39 RID: 19769 RVA: 0x0013FCE8 File Offset: 0x0013DEE8
		public override CSharpSyntaxNode VisitErrorDirectiveTrivia(ErrorDirectiveTriviaSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.HashToken), (SyntaxToken)this.Visit(node.ErrorKeyword), (SyntaxToken)this.Visit(node.EndOfDirectiveToken), node.IsActive);
		}

		// Token: 0x06004D3A RID: 19770 RVA: 0x0013FD34 File Offset: 0x0013DF34
		public override CSharpSyntaxNode VisitWarningDirectiveTrivia(WarningDirectiveTriviaSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.HashToken), (SyntaxToken)this.Visit(node.WarningKeyword), (SyntaxToken)this.Visit(node.EndOfDirectiveToken), node.IsActive);
		}

		// Token: 0x06004D3B RID: 19771 RVA: 0x0013FD80 File Offset: 0x0013DF80
		public override CSharpSyntaxNode VisitBadDirectiveTrivia(BadDirectiveTriviaSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.HashToken), (SyntaxToken)this.Visit(node.Identifier), (SyntaxToken)this.Visit(node.EndOfDirectiveToken), node.IsActive);
		}

		// Token: 0x06004D3C RID: 19772 RVA: 0x0013FDCC File Offset: 0x0013DFCC
		public override CSharpSyntaxNode VisitDefineDirectiveTrivia(DefineDirectiveTriviaSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.HashToken), (SyntaxToken)this.Visit(node.DefineKeyword), (SyntaxToken)this.Visit(node.Name), (SyntaxToken)this.Visit(node.EndOfDirectiveToken), node.IsActive);
		}

		// Token: 0x06004D3D RID: 19773 RVA: 0x0013FE2C File Offset: 0x0013E02C
		public override CSharpSyntaxNode VisitUndefDirectiveTrivia(UndefDirectiveTriviaSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.HashToken), (SyntaxToken)this.Visit(node.UndefKeyword), (SyntaxToken)this.Visit(node.Name), (SyntaxToken)this.Visit(node.EndOfDirectiveToken), node.IsActive);
		}

		// Token: 0x06004D3E RID: 19774 RVA: 0x0013FE8C File Offset: 0x0013E08C
		public override CSharpSyntaxNode VisitLineDirectiveTrivia(LineDirectiveTriviaSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.HashToken), (SyntaxToken)this.Visit(node.LineKeyword), (SyntaxToken)this.Visit(node.Line), (SyntaxToken)this.Visit(node.File), (SyntaxToken)this.Visit(node.EndOfDirectiveToken), node.IsActive);
		}

		// Token: 0x06004D3F RID: 19775 RVA: 0x0013FEFC File Offset: 0x0013E0FC
		public override CSharpSyntaxNode VisitPragmaWarningDirectiveTrivia(PragmaWarningDirectiveTriviaSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.HashToken), (SyntaxToken)this.Visit(node.PragmaKeyword), (SyntaxToken)this.Visit(node.WarningKeyword), (SyntaxToken)this.Visit(node.DisableOrRestoreKeyword), this.VisitList<ExpressionSyntax>(node.ErrorCodes), (SyntaxToken)this.Visit(node.EndOfDirectiveToken), node.IsActive);
		}

		// Token: 0x06004D40 RID: 19776 RVA: 0x0013FF78 File Offset: 0x0013E178
		public override CSharpSyntaxNode VisitPragmaChecksumDirectiveTrivia(PragmaChecksumDirectiveTriviaSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.HashToken), (SyntaxToken)this.Visit(node.PragmaKeyword), (SyntaxToken)this.Visit(node.ChecksumKeyword), (SyntaxToken)this.Visit(node.File), (SyntaxToken)this.Visit(node.Guid), (SyntaxToken)this.Visit(node.Bytes), (SyntaxToken)this.Visit(node.EndOfDirectiveToken), node.IsActive);
		}

		// Token: 0x06004D41 RID: 19777 RVA: 0x00140008 File Offset: 0x0013E208
		public override CSharpSyntaxNode VisitReferenceDirectiveTrivia(ReferenceDirectiveTriviaSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.HashToken), (SyntaxToken)this.Visit(node.ReferenceKeyword), (SyntaxToken)this.Visit(node.File), (SyntaxToken)this.Visit(node.EndOfDirectiveToken), node.IsActive);
		}

		// Token: 0x06004D42 RID: 19778 RVA: 0x00140068 File Offset: 0x0013E268
		public override CSharpSyntaxNode VisitLoadDirectiveTrivia(LoadDirectiveTriviaSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.HashToken), (SyntaxToken)this.Visit(node.LoadKeyword), (SyntaxToken)this.Visit(node.File), (SyntaxToken)this.Visit(node.EndOfDirectiveToken), node.IsActive);
		}

		// Token: 0x06004D43 RID: 19779 RVA: 0x001400C8 File Offset: 0x0013E2C8
		public override CSharpSyntaxNode VisitShebangDirectiveTrivia(ShebangDirectiveTriviaSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.HashToken), (SyntaxToken)this.Visit(node.ExclamationToken), (SyntaxToken)this.Visit(node.EndOfDirectiveToken), node.IsActive);
		}

		// Token: 0x06004D44 RID: 19780 RVA: 0x00140114 File Offset: 0x0013E314
		public override CSharpSyntaxNode VisitNullableDirectiveTrivia(NullableDirectiveTriviaSyntax node)
		{
			return node.Update((SyntaxToken)this.Visit(node.HashToken), (SyntaxToken)this.Visit(node.NullableKeyword), (SyntaxToken)this.Visit(node.SettingToken), (SyntaxToken)this.Visit(node.TargetToken), (SyntaxToken)this.Visit(node.EndOfDirectiveToken), node.IsActive);
		}

		// Token: 0x040015BD RID: 5565
		protected readonly bool VisitIntoStructuredTrivia;
	}
}
