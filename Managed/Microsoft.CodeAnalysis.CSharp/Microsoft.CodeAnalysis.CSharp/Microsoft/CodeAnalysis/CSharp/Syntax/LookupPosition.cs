using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200026C RID: 620
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal static class LookupPosition
	{
		// Token: 0x06003B01 RID: 15105 RVA: 0x001014B6 File Offset: 0x000FF6B6
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal static bool IsInBlock(int position, BlockSyntax blockOpt)
		{
			return blockOpt != null && LookupPosition.IsBeforeToken(position, blockOpt, blockOpt.CloseBraceToken);
		}

		// Token: 0x06003B02 RID: 15106 RVA: 0x001014CA File Offset: 0x000FF6CA
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal static bool IsInExpressionBody(int position, ArrowExpressionClauseSyntax expressionBodyOpt, SyntaxToken semicolonToken)
		{
			return expressionBodyOpt != null && LookupPosition.IsBeforeToken(position, expressionBodyOpt, semicolonToken);
		}

		// Token: 0x06003B03 RID: 15107 RVA: 0x001014D9 File Offset: 0x000FF6D9
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private static bool IsInBody(int position, BlockSyntax blockOpt, ArrowExpressionClauseSyntax exprOpt, SyntaxToken semiOpt)
		{
			return LookupPosition.IsInExpressionBody(position, exprOpt, semiOpt) || LookupPosition.IsInBlock(position, blockOpt);
		}

		// Token: 0x06003B04 RID: 15108 RVA: 0x001014EE File Offset: 0x000FF6EE
		internal static bool IsInBody(int position, PropertyDeclarationSyntax property)
		{
			return LookupPosition.IsInBody(position, null, property.GetExpressionBodySyntax(), property.SemicolonToken);
		}

		// Token: 0x06003B05 RID: 15109 RVA: 0x00101503 File Offset: 0x000FF703
		internal static bool IsInBody(int position, IndexerDeclarationSyntax indexer)
		{
			return LookupPosition.IsInBody(position, null, indexer.GetExpressionBodySyntax(), indexer.SemicolonToken);
		}

		// Token: 0x06003B06 RID: 15110 RVA: 0x00101518 File Offset: 0x000FF718
		internal static bool IsInBody(int position, AccessorDeclarationSyntax method)
		{
			return LookupPosition.IsInBody(position, method.Body, method.GetExpressionBodySyntax(), method.SemicolonToken);
		}

		// Token: 0x06003B07 RID: 15111 RVA: 0x00101532 File Offset: 0x000FF732
		internal static bool IsInBody(int position, BaseMethodDeclarationSyntax method)
		{
			return LookupPosition.IsInBody(position, method.Body, method.GetExpressionBodySyntax(), method.SemicolonToken);
		}

		// Token: 0x06003B08 RID: 15112 RVA: 0x0010154C File Offset: 0x000FF74C
		internal static bool IsBetweenTokens(int position, SyntaxToken firstIncluded, SyntaxToken firstExcluded)
		{
			return position >= firstIncluded.SpanStart && LookupPosition.IsBeforeToken(position, firstExcluded);
		}

		// Token: 0x06003B09 RID: 15113 RVA: 0x00101561 File Offset: 0x000FF761
		private static bool IsBeforeToken(int position, CSharpSyntaxNode node, SyntaxToken firstExcluded)
		{
			return LookupPosition.IsBeforeToken(position, firstExcluded) && position >= node.SpanStart;
		}

		// Token: 0x06003B0A RID: 15114 RVA: 0x0010157A File Offset: 0x000FF77A
		private static bool IsBeforeToken(int position, SyntaxToken firstExcluded)
		{
			return firstExcluded.Kind() == SyntaxKind.None || position < firstExcluded.SpanStart;
		}

		// Token: 0x06003B0B RID: 15115 RVA: 0x00101590 File Offset: 0x000FF790
		internal static bool IsInAttributeSpecification(int position, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributesSyntaxList)
		{
			int count = attributesSyntaxList.Count;
			if (count == 0)
			{
				return false;
			}
			SyntaxToken openBracketToken = attributesSyntaxList[0].OpenBracketToken;
			SyntaxToken closeBracketToken = attributesSyntaxList[count - 1].CloseBracketToken;
			return LookupPosition.IsBetweenTokens(position, openBracketToken, closeBracketToken);
		}

		// Token: 0x06003B0C RID: 15116 RVA: 0x001015D0 File Offset: 0x000FF7D0
		internal static bool IsInTypeParameterList(int position, TypeDeclarationSyntax typeDecl)
		{
			TypeParameterListSyntax typeParameterList = typeDecl.TypeParameterList;
			return typeParameterList != null && LookupPosition.IsBeforeToken(position, typeParameterList, typeParameterList.GreaterThanToken);
		}

		// Token: 0x06003B0D RID: 15117 RVA: 0x001015F8 File Offset: 0x000FF7F8
		internal static bool IsInParameterList(int position, BaseMethodDeclarationSyntax methodDecl)
		{
			ParameterListSyntax parameterList = methodDecl.ParameterList;
			return LookupPosition.IsBeforeToken(position, parameterList, parameterList.CloseParenToken);
		}

		// Token: 0x06003B0E RID: 15118 RVA: 0x00101619 File Offset: 0x000FF819
		internal static bool IsInParameterList(int position, ParameterListSyntax parameterList)
		{
			return parameterList != null && LookupPosition.IsBeforeToken(position, parameterList, parameterList.CloseParenToken);
		}

		// Token: 0x06003B0F RID: 15119 RVA: 0x00101630 File Offset: 0x000FF830
		internal static bool IsInMethodDeclaration(int position, BaseMethodDeclarationSyntax methodDecl)
		{
			BlockSyntax body = methodDecl.Body;
			if (body == null)
			{
				return LookupPosition.IsBeforeToken(position, methodDecl, methodDecl.SemicolonToken);
			}
			return LookupPosition.IsBeforeToken(position, methodDecl, body.CloseBraceToken) || LookupPosition.IsInExpressionBody(position, methodDecl.GetExpressionBodySyntax(), methodDecl.SemicolonToken);
		}

		// Token: 0x06003B10 RID: 15120 RVA: 0x00101678 File Offset: 0x000FF878
		internal static bool IsInMethodDeclaration(int position, AccessorDeclarationSyntax accessorDecl)
		{
			BlockSyntax body = accessorDecl.Body;
			SyntaxToken syntaxToken = ((body == null) ? accessorDecl.SemicolonToken : body.CloseBraceToken);
			return LookupPosition.IsBeforeToken(position, accessorDecl, syntaxToken);
		}

		// Token: 0x06003B11 RID: 15121 RVA: 0x001016A6 File Offset: 0x000FF8A6
		internal static bool IsInDelegateDeclaration(int position, DelegateDeclarationSyntax delegateDecl)
		{
			return LookupPosition.IsBeforeToken(position, delegateDecl, delegateDecl.SemicolonToken);
		}

		// Token: 0x06003B12 RID: 15122 RVA: 0x001016B5 File Offset: 0x000FF8B5
		internal static bool IsInTypeDeclaration(int position, BaseTypeDeclarationSyntax typeDecl)
		{
			return LookupPosition.IsBeforeToken(position, typeDecl, typeDecl.CloseBraceToken);
		}

		// Token: 0x06003B13 RID: 15123 RVA: 0x001016C4 File Offset: 0x000FF8C4
		internal static bool IsInNamespaceDeclaration(int position, NamespaceDeclarationSyntax namespaceDecl)
		{
			return LookupPosition.IsBetweenTokens(position, namespaceDecl.NamespaceKeyword, namespaceDecl.CloseBraceToken);
		}

		// Token: 0x06003B14 RID: 15124 RVA: 0x001016D8 File Offset: 0x000FF8D8
		internal static bool IsInConstructorParameterScope(int position, ConstructorDeclarationSyntax constructorDecl)
		{
			ConstructorInitializerSyntax initializer = constructorDecl.Initializer;
			if (constructorDecl.Body == null && constructorDecl.ExpressionBody == null)
			{
				SyntaxToken nextToken = SyntaxNavigator.Instance.GetNextToken(constructorDecl, null, null);
				if (initializer != null)
				{
					return LookupPosition.IsBetweenTokens(position, initializer.ColonToken, nextToken);
				}
				return position >= constructorDecl.ParameterList.CloseParenToken.Span.End && LookupPosition.IsBeforeToken(position, nextToken);
			}
			else
			{
				if (initializer != null)
				{
					return LookupPosition.IsBetweenTokens(position, initializer.ColonToken, (constructorDecl.SemicolonToken.Kind() == SyntaxKind.None) ? constructorDecl.Body.CloseBraceToken : constructorDecl.SemicolonToken);
				}
				return LookupPosition.IsInBody(position, constructorDecl);
			}
		}

		// Token: 0x06003B15 RID: 15125 RVA: 0x00101780 File Offset: 0x000FF980
		internal static bool IsInMethodTypeParameterScope(int position, MethodDeclarationSyntax methodDecl)
		{
			if (methodDecl.TypeParameterList == null)
			{
				return false;
			}
			if (methodDecl.ReturnType.FullSpan.Contains(position))
			{
				return true;
			}
			if (LookupPosition.IsInAttributeSpecification(position, methodDecl.AttributeLists))
			{
				return false;
			}
			ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier = methodDecl.ExplicitInterfaceSpecifier;
			SyntaxToken syntaxToken = ((explicitInterfaceSpecifier == null) ? methodDecl.Identifier : explicitInterfaceSpecifier.GetFirstToken(false, false, false, false));
			TypeParameterListSyntax typeParameterList = methodDecl.TypeParameterList;
			SyntaxToken syntaxToken2 = ((typeParameterList == null) ? methodDecl.ParameterList.OpenParenToken : typeParameterList.LessThanToken);
			return !LookupPosition.IsBetweenTokens(position, syntaxToken, syntaxToken2);
		}

		// Token: 0x06003B16 RID: 15126 RVA: 0x00101808 File Offset: 0x000FFA08
		internal static bool IsInStatementScope(int position, StatementSyntax statement)
		{
			if (statement.Kind() == SyntaxKind.EmptyStatement)
			{
				return false;
			}
			SyntaxToken firstIncludedToken = LookupPosition.GetFirstIncludedToken(statement);
			return firstIncludedToken != default(SyntaxToken) && LookupPosition.IsBetweenTokens(position, firstIncludedToken, LookupPosition.GetFirstExcludedToken(statement));
		}

		// Token: 0x06003B17 RID: 15127 RVA: 0x0010184C File Offset: 0x000FFA4C
		internal static bool IsInSwitchSectionScope(int position, SwitchSectionSyntax section)
		{
			return section.Span.Contains(position);
		}

		// Token: 0x06003B18 RID: 15128 RVA: 0x00101868 File Offset: 0x000FFA68
		internal static bool IsInCatchBlockScope(int position, CatchClauseSyntax catchClause)
		{
			return LookupPosition.IsBetweenTokens(position, catchClause.Block.OpenBraceToken, catchClause.Block.CloseBraceToken);
		}

		// Token: 0x06003B19 RID: 15129 RVA: 0x00101886 File Offset: 0x000FFA86
		internal static bool IsInCatchFilterScope(int position, CatchFilterClauseSyntax filterClause)
		{
			return LookupPosition.IsBetweenTokens(position, filterClause.OpenParenToken, filterClause.CloseParenToken);
		}

		// Token: 0x06003B1A RID: 15130 RVA: 0x0010189C File Offset: 0x000FFA9C
		private static SyntaxToken GetFirstIncludedToken(StatementSyntax statement)
		{
			SyntaxKind syntaxKind = statement.Kind();
			switch (syntaxKind)
			{
			case SyntaxKind.Block:
				return ((BlockSyntax)statement).OpenBraceToken;
			case SyntaxKind.LocalDeclarationStatement:
			case SyntaxKind.ExpressionStatement:
				return statement.GetFirstToken(false, false, false, false);
			case SyntaxKind.VariableDeclaration:
			case SyntaxKind.VariableDeclarator:
			case SyntaxKind.EqualsValueClause:
			case SyntaxKind.ElseClause:
			case SyntaxKind.SwitchSection:
			case SyntaxKind.CaseSwitchLabel:
			case SyntaxKind.DefaultSwitchLabel:
			case SyntaxKind.CatchClause:
			case SyntaxKind.CatchDeclaration:
			case SyntaxKind.CatchFilterClause:
			case SyntaxKind.FinallyClause:
				goto IL_01ED;
			case SyntaxKind.EmptyStatement:
				return default(SyntaxToken);
			case SyntaxKind.LabeledStatement:
				return ((LabeledStatementSyntax)statement).Identifier;
			case SyntaxKind.GotoStatement:
			case SyntaxKind.GotoCaseStatement:
			case SyntaxKind.GotoDefaultStatement:
				return ((GotoStatementSyntax)statement).GotoKeyword;
			case SyntaxKind.BreakStatement:
				return ((BreakStatementSyntax)statement).BreakKeyword;
			case SyntaxKind.ContinueStatement:
				return ((ContinueStatementSyntax)statement).ContinueKeyword;
			case SyntaxKind.ReturnStatement:
				return ((ReturnStatementSyntax)statement).ReturnKeyword;
			case SyntaxKind.YieldReturnStatement:
			case SyntaxKind.YieldBreakStatement:
				return ((YieldStatementSyntax)statement).YieldKeyword;
			case SyntaxKind.ThrowStatement:
				return ((ThrowStatementSyntax)statement).ThrowKeyword;
			case SyntaxKind.WhileStatement:
				return ((WhileStatementSyntax)statement).WhileKeyword;
			case SyntaxKind.DoStatement:
				return ((DoStatementSyntax)statement).DoKeyword;
			case SyntaxKind.ForStatement:
				return ((ForStatementSyntax)statement).OpenParenToken.GetNextToken(false, false, false, false);
			case SyntaxKind.ForEachStatement:
				break;
			case SyntaxKind.UsingStatement:
				return ((UsingStatementSyntax)statement).UsingKeyword;
			case SyntaxKind.FixedStatement:
				return ((FixedStatementSyntax)statement).FixedKeyword;
			case SyntaxKind.CheckedStatement:
			case SyntaxKind.UncheckedStatement:
				return ((CheckedStatementSyntax)statement).Keyword;
			case SyntaxKind.UnsafeStatement:
				return ((UnsafeStatementSyntax)statement).UnsafeKeyword;
			case SyntaxKind.LockStatement:
				return ((LockStatementSyntax)statement).LockKeyword;
			case SyntaxKind.IfStatement:
				return ((IfStatementSyntax)statement).IfKeyword;
			case SyntaxKind.SwitchStatement:
				return ((SwitchStatementSyntax)statement).Expression.GetFirstToken(false, false, false, false);
			case SyntaxKind.TryStatement:
				return ((TryStatementSyntax)statement).TryKeyword;
			case SyntaxKind.LocalFunctionStatement:
				return statement.GetFirstToken(false, false, false, false);
			default:
				if (syntaxKind != SyntaxKind.ForEachVariableStatement)
				{
					goto IL_01ED;
				}
				break;
			}
			return ((CommonForEachStatementSyntax)statement).OpenParenToken.GetNextToken(false, false, false, false);
			IL_01ED:
			throw ExceptionUtilities.UnexpectedValue(statement.Kind());
		}

		// Token: 0x06003B1B RID: 15131 RVA: 0x00101AA8 File Offset: 0x000FFCA8
		internal static SyntaxToken GetFirstExcludedToken(StatementSyntax statement)
		{
			SyntaxKind syntaxKind = statement.Kind();
			switch (syntaxKind)
			{
			case SyntaxKind.Block:
				return ((BlockSyntax)statement).CloseBraceToken;
			case SyntaxKind.LocalDeclarationStatement:
				return ((LocalDeclarationStatementSyntax)statement).SemicolonToken;
			case SyntaxKind.VariableDeclaration:
			case SyntaxKind.VariableDeclarator:
			case SyntaxKind.EqualsValueClause:
			case SyntaxKind.ElseClause:
			case SyntaxKind.SwitchSection:
			case SyntaxKind.CaseSwitchLabel:
			case SyntaxKind.DefaultSwitchLabel:
			case SyntaxKind.CatchClause:
			case SyntaxKind.CatchDeclaration:
			case SyntaxKind.CatchFilterClause:
			case SyntaxKind.FinallyClause:
				goto IL_02A7;
			case SyntaxKind.ExpressionStatement:
				return ((ExpressionStatementSyntax)statement).SemicolonToken;
			case SyntaxKind.EmptyStatement:
				return ((EmptyStatementSyntax)statement).SemicolonToken;
			case SyntaxKind.LabeledStatement:
				return LookupPosition.GetFirstExcludedToken(((LabeledStatementSyntax)statement).Statement);
			case SyntaxKind.GotoStatement:
			case SyntaxKind.GotoCaseStatement:
			case SyntaxKind.GotoDefaultStatement:
				return ((GotoStatementSyntax)statement).SemicolonToken;
			case SyntaxKind.BreakStatement:
				return ((BreakStatementSyntax)statement).SemicolonToken;
			case SyntaxKind.ContinueStatement:
				return ((ContinueStatementSyntax)statement).SemicolonToken;
			case SyntaxKind.ReturnStatement:
				return ((ReturnStatementSyntax)statement).SemicolonToken;
			case SyntaxKind.YieldReturnStatement:
			case SyntaxKind.YieldBreakStatement:
				return ((YieldStatementSyntax)statement).SemicolonToken;
			case SyntaxKind.ThrowStatement:
				return ((ThrowStatementSyntax)statement).SemicolonToken;
			case SyntaxKind.WhileStatement:
				return LookupPosition.GetFirstExcludedToken(((WhileStatementSyntax)statement).Statement);
			case SyntaxKind.DoStatement:
				return ((DoStatementSyntax)statement).SemicolonToken;
			case SyntaxKind.ForStatement:
				return LookupPosition.GetFirstExcludedToken(((ForStatementSyntax)statement).Statement);
			case SyntaxKind.ForEachStatement:
				break;
			case SyntaxKind.UsingStatement:
				return LookupPosition.GetFirstExcludedToken(((UsingStatementSyntax)statement).Statement);
			case SyntaxKind.FixedStatement:
				return LookupPosition.GetFirstExcludedToken(((FixedStatementSyntax)statement).Statement);
			case SyntaxKind.CheckedStatement:
			case SyntaxKind.UncheckedStatement:
				return ((CheckedStatementSyntax)statement).Block.CloseBraceToken;
			case SyntaxKind.UnsafeStatement:
				return ((UnsafeStatementSyntax)statement).Block.CloseBraceToken;
			case SyntaxKind.LockStatement:
				return LookupPosition.GetFirstExcludedToken(((LockStatementSyntax)statement).Statement);
			case SyntaxKind.IfStatement:
			{
				IfStatementSyntax ifStatementSyntax = (IfStatementSyntax)statement;
				ElseClauseSyntax @else = ifStatementSyntax.Else;
				return LookupPosition.GetFirstExcludedToken((@else == null) ? ifStatementSyntax.Statement : @else.Statement);
			}
			case SyntaxKind.SwitchStatement:
				return ((SwitchStatementSyntax)statement).CloseBraceToken;
			case SyntaxKind.TryStatement:
			{
				TryStatementSyntax tryStatementSyntax = (TryStatementSyntax)statement;
				FinallyClauseSyntax @finally = tryStatementSyntax.Finally;
				if (@finally != null)
				{
					return @finally.Block.CloseBraceToken;
				}
				CatchClauseSyntax catchClauseSyntax = tryStatementSyntax.Catches.LastOrDefault();
				if (catchClauseSyntax != null)
				{
					return catchClauseSyntax.Block.CloseBraceToken;
				}
				return tryStatementSyntax.Block.CloseBraceToken;
			}
			case SyntaxKind.LocalFunctionStatement:
			{
				LocalFunctionStatementSyntax localFunctionStatementSyntax = (LocalFunctionStatementSyntax)statement;
				if (localFunctionStatementSyntax.Body != null)
				{
					return LookupPosition.GetFirstExcludedToken(localFunctionStatementSyntax.Body);
				}
				if (localFunctionStatementSyntax.SemicolonToken != default(SyntaxToken))
				{
					return localFunctionStatementSyntax.SemicolonToken;
				}
				return localFunctionStatementSyntax.ParameterList.GetLastToken(false, false, false, false);
			}
			default:
				if (syntaxKind != SyntaxKind.ForEachVariableStatement)
				{
					goto IL_02A7;
				}
				break;
			}
			return LookupPosition.GetFirstExcludedToken(((CommonForEachStatementSyntax)statement).Statement);
			IL_02A7:
			throw ExceptionUtilities.UnexpectedValue(statement.Kind());
		}

		// Token: 0x06003B1C RID: 15132 RVA: 0x00101D6C File Offset: 0x000FFF6C
		internal static bool IsInAnonymousFunctionOrQuery(int position, SyntaxNode lambdaExpressionOrQueryNode)
		{
			CSharpSyntaxNode csharpSyntaxNode;
			SyntaxToken syntaxToken;
			switch (lambdaExpressionOrQueryNode.Kind())
			{
			case SyntaxKind.AnonymousMethodExpression:
				csharpSyntaxNode = ((AnonymousMethodExpressionSyntax)lambdaExpressionOrQueryNode).Block;
				syntaxToken = csharpSyntaxNode.GetFirstToken(true, false, false, false);
				break;
			case SyntaxKind.SimpleLambdaExpression:
			{
				SimpleLambdaExpressionSyntax simpleLambdaExpressionSyntax = (SimpleLambdaExpressionSyntax)lambdaExpressionOrQueryNode;
				syntaxToken = simpleLambdaExpressionSyntax.ArrowToken;
				csharpSyntaxNode = simpleLambdaExpressionSyntax.Body;
				break;
			}
			case SyntaxKind.ParenthesizedLambdaExpression:
			{
				ParenthesizedLambdaExpressionSyntax parenthesizedLambdaExpressionSyntax = (ParenthesizedLambdaExpressionSyntax)lambdaExpressionOrQueryNode;
				syntaxToken = parenthesizedLambdaExpressionSyntax.ArrowToken;
				csharpSyntaxNode = parenthesizedLambdaExpressionSyntax.Body;
				break;
			}
			default:
				syntaxToken = lambdaExpressionOrQueryNode.GetFirstToken(false, false, false, false).GetNextToken(false, false, false, false);
				return LookupPosition.IsBetweenTokens(position, syntaxToken, lambdaExpressionOrQueryNode.GetLastToken(false, false, false, false).GetNextToken(false, false, false, false));
			}
			StatementSyntax statementSyntax = csharpSyntaxNode as StatementSyntax;
			SyntaxToken syntaxToken2 = ((statementSyntax != null) ? LookupPosition.GetFirstExcludedToken(statementSyntax) : SyntaxNavigator.Instance.GetNextToken(csharpSyntaxNode, null, null));
			return LookupPosition.IsBetweenTokens(position, syntaxToken, syntaxToken2);
		}

		// Token: 0x06003B1D RID: 15133 RVA: 0x00101E3E File Offset: 0x0010003E
		internal static bool IsInXmlAttributeValue(int position, XmlAttributeSyntax attribute)
		{
			return LookupPosition.IsBetweenTokens(position, attribute.StartQuoteToken, attribute.EndQuoteToken);
		}
	}
}
