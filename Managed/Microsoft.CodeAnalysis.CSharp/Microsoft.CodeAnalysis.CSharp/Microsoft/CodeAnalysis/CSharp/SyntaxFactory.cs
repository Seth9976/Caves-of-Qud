using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;
using Microsoft.CodeAnalysis.Syntax;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000229 RID: 553
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public static class SyntaxFactory
	{
		// Token: 0x0600283F RID: 10303 RVA: 0x000DB77C File Offset: 0x000D997C
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AnonymousMethodExpressionSyntax AnonymousMethodExpression()
		{
			return SyntaxFactory.AnonymousMethodExpression(default(SyntaxToken), SyntaxFactory.Token(SyntaxKind.DelegateKeyword), null, SyntaxFactory.Block(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax>)), null);
		}

		// Token: 0x06002840 RID: 10304 RVA: 0x000DB7B1 File Offset: 0x000D99B1
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AnonymousMethodExpressionSyntax AnonymousMethodExpression(SyntaxToken asyncKeyword, SyntaxToken delegateKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax block, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expressionBody)
		{
			return SyntaxFactory.AnonymousMethodExpression(SyntaxFactory.TokenList(asyncKeyword), delegateKeyword, parameterList, block, expressionBody);
		}

		// Token: 0x06002841 RID: 10305 RVA: 0x000DB7C4 File Offset: 0x000D99C4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax Block(SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax> statements, SyntaxToken closeBraceToken)
		{
			return SyntaxFactory.Block(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), openBraceToken, statements, closeBraceToken);
		}

		// Token: 0x06002842 RID: 10306 RVA: 0x000DB7E4 File Offset: 0x000D99E4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.BreakStatementSyntax BreakStatement(SyntaxToken breakKeyword, SyntaxToken semicolonToken)
		{
			return SyntaxFactory.BreakStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), breakKeyword, semicolonToken);
		}

		// Token: 0x06002843 RID: 10307 RVA: 0x000DB804 File Offset: 0x000D9A04
		public static Microsoft.CodeAnalysis.CSharp.Syntax.CheckedStatementSyntax CheckedStatement(SyntaxKind kind, SyntaxToken keyword, Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax block)
		{
			return SyntaxFactory.CheckedStatement(kind, default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), keyword, block);
		}

		// Token: 0x06002844 RID: 10308 RVA: 0x000DB824 File Offset: 0x000D9A24
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ConstructorDeclarationSyntax ConstructorDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, Microsoft.CodeAnalysis.CSharp.Syntax.ConstructorInitializerSyntax initializer, Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax body)
		{
			return SyntaxFactory.ConstructorDeclaration(attributeLists, modifiers, identifier, parameterList, initializer, body, null, default(SyntaxToken));
		}

		// Token: 0x06002845 RID: 10309 RVA: 0x000DB848 File Offset: 0x000D9A48
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ConstructorDeclarationSyntax ConstructorDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, Microsoft.CodeAnalysis.CSharp.Syntax.ConstructorInitializerSyntax initializer, Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax body, SyntaxToken semicolonToken)
		{
			return SyntaxFactory.ConstructorDeclaration(attributeLists, modifiers, identifier, parameterList, initializer, body, null, semicolonToken);
		}

		// Token: 0x06002846 RID: 10310 RVA: 0x000DB85C File Offset: 0x000D9A5C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ConstructorDeclarationSyntax ConstructorDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, Microsoft.CodeAnalysis.CSharp.Syntax.ConstructorInitializerSyntax initializer, Microsoft.CodeAnalysis.CSharp.Syntax.ArrowExpressionClauseSyntax expressionBody)
		{
			return SyntaxFactory.ConstructorDeclaration(attributeLists, modifiers, identifier, parameterList, initializer, null, expressionBody, default(SyntaxToken));
		}

		// Token: 0x06002847 RID: 10311 RVA: 0x000DB880 File Offset: 0x000D9A80
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ConstructorDeclarationSyntax ConstructorDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, Microsoft.CodeAnalysis.CSharp.Syntax.ConstructorInitializerSyntax initializer, Microsoft.CodeAnalysis.CSharp.Syntax.ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
		{
			return SyntaxFactory.ConstructorDeclaration(attributeLists, modifiers, identifier, parameterList, initializer, null, expressionBody, semicolonToken);
		}

		// Token: 0x06002848 RID: 10312 RVA: 0x000DB894 File Offset: 0x000D9A94
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ContinueStatementSyntax ContinueStatement(SyntaxToken continueKeyword, SyntaxToken semicolonToken)
		{
			return SyntaxFactory.ContinueStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), continueKeyword, semicolonToken);
		}

		// Token: 0x06002849 RID: 10313 RVA: 0x000DB8B4 File Offset: 0x000D9AB4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DestructorDeclarationSyntax DestructorDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax body)
		{
			return SyntaxFactory.DestructorDeclaration(attributeLists, modifiers, SyntaxFactory.Token(SyntaxKind.TildeToken), identifier, parameterList, body, null, default(SyntaxToken));
		}

		// Token: 0x0600284A RID: 10314 RVA: 0x000DB8E0 File Offset: 0x000D9AE0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DestructorDeclarationSyntax DestructorDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken tildeToken, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax body, SyntaxToken semicolonToken)
		{
			return SyntaxFactory.DestructorDeclaration(attributeLists, modifiers, tildeToken, identifier, parameterList, body, null, semicolonToken);
		}

		// Token: 0x0600284B RID: 10315 RVA: 0x000DB8F4 File Offset: 0x000D9AF4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DestructorDeclarationSyntax DestructorDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, Microsoft.CodeAnalysis.CSharp.Syntax.ArrowExpressionClauseSyntax expressionBody)
		{
			return SyntaxFactory.DestructorDeclaration(attributeLists, modifiers, SyntaxFactory.Token(SyntaxKind.TildeToken), identifier, parameterList, null, expressionBody, default(SyntaxToken));
		}

		// Token: 0x0600284C RID: 10316 RVA: 0x000DB920 File Offset: 0x000D9B20
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DestructorDeclarationSyntax DestructorDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken tildeToken, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, Microsoft.CodeAnalysis.CSharp.Syntax.ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
		{
			return SyntaxFactory.DestructorDeclaration(attributeLists, modifiers, tildeToken, identifier, parameterList, null, expressionBody, semicolonToken);
		}

		// Token: 0x0600284D RID: 10317 RVA: 0x000DB934 File Offset: 0x000D9B34
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DoStatementSyntax DoStatement(SyntaxToken doKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement, SyntaxToken whileKeyword, SyntaxToken openParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax condition, SyntaxToken closeParenToken, SyntaxToken semicolonToken)
		{
			return SyntaxFactory.DoStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), doKeyword, statement, whileKeyword, openParenToken, condition, closeParenToken, semicolonToken);
		}

		// Token: 0x0600284E RID: 10318 RVA: 0x000DB95C File Offset: 0x000D9B5C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.EmptyStatementSyntax EmptyStatement(SyntaxToken semicolonToken)
		{
			return SyntaxFactory.EmptyStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), semicolonToken);
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x000DB978 File Offset: 0x000D9B78
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionStatementSyntax ExpressionStatement(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SyntaxToken semicolonToken)
		{
			return SyntaxFactory.ExpressionStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), expression, semicolonToken);
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x000DB998 File Offset: 0x000D9B98
		public static Microsoft.CodeAnalysis.CSharp.Syntax.FixedStatementSyntax FixedStatement(SyntaxToken fixedKeyword, SyntaxToken openParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclarationSyntax declaration, SyntaxToken closeParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			return SyntaxFactory.FixedStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), fixedKeyword, openParenToken, declaration, closeParenToken, statement);
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x000DB9BC File Offset: 0x000D9BBC
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ForEachStatementSyntax ForEachStatement(SyntaxToken forEachKeyword, SyntaxToken openParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SyntaxToken closeParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			return SyntaxFactory.ForEachStatement(default(SyntaxToken), forEachKeyword, openParenToken, type, identifier, inKeyword, expression, closeParenToken, statement);
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x000DB9E4 File Offset: 0x000D9BE4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ForEachStatementSyntax ForEachStatement(SyntaxToken awaitKeyword, SyntaxToken forEachKeyword, SyntaxToken openParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SyntaxToken closeParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			return SyntaxFactory.ForEachStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), awaitKeyword, forEachKeyword, openParenToken, type, identifier, inKeyword, expression, closeParenToken, statement);
		}

		// Token: 0x06002853 RID: 10323 RVA: 0x000DBA10 File Offset: 0x000D9C10
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ForEachVariableStatementSyntax ForEachVariableStatement(SyntaxToken forEachKeyword, SyntaxToken openParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax variable, SyntaxToken inKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SyntaxToken closeParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			return SyntaxFactory.ForEachVariableStatement(default(SyntaxToken), forEachKeyword, openParenToken, variable, inKeyword, expression, closeParenToken, statement);
		}

		// Token: 0x06002854 RID: 10324 RVA: 0x000DBA38 File Offset: 0x000D9C38
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ForEachVariableStatementSyntax ForEachVariableStatement(SyntaxToken awaitKeyword, SyntaxToken forEachKeyword, SyntaxToken openParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax variable, SyntaxToken inKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SyntaxToken closeParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			return SyntaxFactory.ForEachVariableStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), awaitKeyword, forEachKeyword, openParenToken, variable, inKeyword, expression, closeParenToken, statement);
		}

		// Token: 0x06002855 RID: 10325 RVA: 0x000DBA60 File Offset: 0x000D9C60
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ForStatementSyntax ForStatement(Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax> initializers, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax condition, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax> incrementors, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			return SyntaxFactory.ForStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), declaration, initializers, condition, incrementors, statement);
		}

		// Token: 0x06002856 RID: 10326 RVA: 0x000DBA84 File Offset: 0x000D9C84
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ForStatementSyntax ForStatement(SyntaxToken forKeyword, SyntaxToken openParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax> initializers, SyntaxToken firstSemicolonToken, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax condition, SyntaxToken secondSemicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax> incrementors, SyntaxToken closeParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			return SyntaxFactory.ForStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), forKeyword, openParenToken, declaration, initializers, firstSemicolonToken, condition, secondSemicolonToken, incrementors, closeParenToken, statement);
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x000DBAB0 File Offset: 0x000D9CB0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.GotoStatementSyntax GotoStatement(SyntaxKind kind, SyntaxToken caseOrDefaultKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			return SyntaxFactory.GotoStatement(kind, default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), caseOrDefaultKeyword, expression);
		}

		// Token: 0x06002858 RID: 10328 RVA: 0x000DBAD0 File Offset: 0x000D9CD0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.GotoStatementSyntax GotoStatement(SyntaxKind kind, SyntaxToken gotoKeyword, SyntaxToken caseOrDefaultKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SyntaxToken semicolonToken)
		{
			return SyntaxFactory.GotoStatement(kind, default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), gotoKeyword, caseOrDefaultKeyword, expression, semicolonToken);
		}

		// Token: 0x06002859 RID: 10329 RVA: 0x000DBAF4 File Offset: 0x000D9CF4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.IfStatementSyntax IfStatement(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax condition, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement, Microsoft.CodeAnalysis.CSharp.Syntax.ElseClauseSyntax @else)
		{
			return SyntaxFactory.IfStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), condition, statement, @else);
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x000DBB14 File Offset: 0x000D9D14
		public static Microsoft.CodeAnalysis.CSharp.Syntax.IfStatementSyntax IfStatement(SyntaxToken ifKeyword, SyntaxToken openParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax condition, SyntaxToken closeParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement, Microsoft.CodeAnalysis.CSharp.Syntax.ElseClauseSyntax @else)
		{
			return SyntaxFactory.IfStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), ifKeyword, openParenToken, condition, closeParenToken, statement, @else);
		}

		// Token: 0x0600285B RID: 10331 RVA: 0x000DBB37 File Offset: 0x000D9D37
		public static Microsoft.CodeAnalysis.CSharp.Syntax.IndexerDeclarationSyntax IndexerDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, Microsoft.CodeAnalysis.CSharp.Syntax.ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, Microsoft.CodeAnalysis.CSharp.Syntax.BracketedParameterListSyntax parameterList, Microsoft.CodeAnalysis.CSharp.Syntax.AccessorListSyntax accessorList)
		{
			return SyntaxFactory.IndexerDeclaration(attributeLists, modifiers, type, explicitInterfaceSpecifier, parameterList, accessorList, null);
		}

		// Token: 0x0600285C RID: 10332 RVA: 0x000DBB48 File Offset: 0x000D9D48
		public static Microsoft.CodeAnalysis.CSharp.Syntax.LabeledStatementSyntax LabeledStatement(SyntaxToken identifier, SyntaxToken colonToken, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			return SyntaxFactory.LabeledStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), identifier, colonToken, statement);
		}

		// Token: 0x0600285D RID: 10333 RVA: 0x000DBB68 File Offset: 0x000D9D68
		public static Microsoft.CodeAnalysis.CSharp.Syntax.LocalDeclarationStatementSyntax LocalDeclarationStatement(SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclarationSyntax declaration, SyntaxToken semicolonToken)
		{
			return SyntaxFactory.LocalDeclarationStatement(default(SyntaxToken), default(SyntaxToken), modifiers, declaration, semicolonToken);
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x000DBB90 File Offset: 0x000D9D90
		public static Microsoft.CodeAnalysis.CSharp.Syntax.LocalDeclarationStatementSyntax LocalDeclarationStatement(SyntaxToken awaitKeyword, SyntaxToken usingKeyword, SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclarationSyntax declaration, SyntaxToken semicolonToken)
		{
			return SyntaxFactory.LocalDeclarationStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), awaitKeyword, usingKeyword, modifiers, declaration, semicolonToken);
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x000DBBB4 File Offset: 0x000D9DB4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.LocalDeclarationStatementSyntax LocalDeclarationStatement(SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclarationSyntax declaration)
		{
			return SyntaxFactory.LocalDeclarationStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), modifiers, declaration);
		}

		// Token: 0x06002860 RID: 10336 RVA: 0x000DBBD4 File Offset: 0x000D9DD4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.LocalFunctionStatementSyntax LocalFunctionStatement(SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax returnType, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterListSyntax typeParameterList, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax> constraintClauses, Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax body, Microsoft.CodeAnalysis.CSharp.Syntax.ArrowExpressionClauseSyntax expressionBody)
		{
			return SyntaxFactory.LocalFunctionStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), modifiers, returnType, identifier, typeParameterList, parameterList, constraintClauses, body, expressionBody, default(SyntaxToken));
		}

		// Token: 0x06002861 RID: 10337 RVA: 0x000DBC04 File Offset: 0x000D9E04
		public static Microsoft.CodeAnalysis.CSharp.Syntax.LocalFunctionStatementSyntax LocalFunctionStatement(SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax returnType, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterListSyntax typeParameterList, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax> constraintClauses, Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax body, Microsoft.CodeAnalysis.CSharp.Syntax.ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
		{
			return SyntaxFactory.LocalFunctionStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), modifiers, returnType, identifier, typeParameterList, parameterList, constraintClauses, body, expressionBody, semicolonToken);
		}

		// Token: 0x06002862 RID: 10338 RVA: 0x000DBC30 File Offset: 0x000D9E30
		public static Microsoft.CodeAnalysis.CSharp.Syntax.LockStatementSyntax LockStatement(SyntaxToken lockKeyword, SyntaxToken openParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SyntaxToken closeParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			return SyntaxFactory.LockStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), lockKeyword, openParenToken, expression, closeParenToken, statement);
		}

		// Token: 0x06002863 RID: 10339 RVA: 0x000DBC54 File Offset: 0x000D9E54
		public static Microsoft.CodeAnalysis.CSharp.Syntax.MethodDeclarationSyntax MethodDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax returnType, Microsoft.CodeAnalysis.CSharp.Syntax.ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterListSyntax typeParameterList, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax> constraintClauses, Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax body, SyntaxToken semicolonToken)
		{
			return SyntaxFactory.MethodDeclaration(attributeLists, modifiers, returnType, explicitInterfaceSpecifier, identifier, typeParameterList, parameterList, constraintClauses, body, null, semicolonToken);
		}

		// Token: 0x06002864 RID: 10340 RVA: 0x000DBC77 File Offset: 0x000D9E77
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ParenthesizedLambdaExpressionSyntax ParenthesizedLambdaExpression(SyntaxToken asyncKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, SyntaxToken arrowToken, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expressionBody)
		{
			return SyntaxFactory.ParenthesizedLambdaExpression(SyntaxFactory.TokenList(asyncKeyword), parameterList, arrowToken, block, expressionBody);
		}

		// Token: 0x06002865 RID: 10341 RVA: 0x000DBC8C File Offset: 0x000D9E8C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ParenthesizedLambdaExpressionSyntax ParenthesizedLambdaExpression(Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expressionBody)
		{
			return SyntaxFactory.ParenthesizedLambdaExpression(default(SyntaxTokenList), parameterList, block, expressionBody);
		}

		// Token: 0x06002866 RID: 10342 RVA: 0x000DBCAC File Offset: 0x000D9EAC
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ParenthesizedLambdaExpressionSyntax ParenthesizedLambdaExpression(SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, SyntaxToken arrowToken, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expressionBody)
		{
			return SyntaxFactory.ParenthesizedLambdaExpression(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), modifiers, parameterList, arrowToken, block, expressionBody);
		}

		// Token: 0x06002867 RID: 10343 RVA: 0x000DBCD0 File Offset: 0x000D9ED0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ParenthesizedLambdaExpressionSyntax ParenthesizedLambdaExpression(SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expressionBody)
		{
			return SyntaxFactory.ParenthesizedLambdaExpression(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), modifiers, parameterList, block, expressionBody);
		}

		// Token: 0x06002868 RID: 10344 RVA: 0x000DBCF0 File Offset: 0x000D9EF0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AccessorDeclarationSyntax AccessorDeclaration(SyntaxKind kind, Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax body)
		{
			return SyntaxFactory.AccessorDeclaration(kind, default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), SyntaxFactory.Token(SyntaxFactory.GetAccessorDeclarationKeywordKind(kind)), body, null, default(SyntaxToken));
		}

		// Token: 0x06002869 RID: 10345 RVA: 0x000DBD2C File Offset: 0x000D9F2C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public static Microsoft.CodeAnalysis.CSharp.Syntax.RecordDeclarationSyntax RecordDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterListSyntax typeParameterList, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, Microsoft.CodeAnalysis.CSharp.Syntax.BaseListSyntax baseList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
		{
			return SyntaxFactory.RecordDeclaration(SyntaxKind.RecordDeclaration, attributeLists, modifiers, keyword, default(SyntaxToken), identifier, typeParameterList, parameterList, baseList, constraintClauses, openBraceToken, members, closeBraceToken, semicolonToken);
		}

		// Token: 0x0600286A RID: 10346 RVA: 0x000DBD60 File Offset: 0x000D9F60
		public static Microsoft.CodeAnalysis.CSharp.Syntax.RecordDeclarationSyntax RecordDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterListSyntax typeParameterList, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, Microsoft.CodeAnalysis.CSharp.Syntax.BaseListSyntax baseList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax> constraintClauses, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax> members)
		{
			return SyntaxFactory.RecordDeclaration(SyntaxKind.RecordDeclaration, attributeLists, modifiers, keyword, default(SyntaxToken), identifier, typeParameterList, parameterList, baseList, constraintClauses, default(SyntaxToken), members, default(SyntaxToken), default(SyntaxToken));
		}

		// Token: 0x0600286B RID: 10347 RVA: 0x000DBDA9 File Offset: 0x000D9FA9
		public static Microsoft.CodeAnalysis.CSharp.Syntax.RecordDeclarationSyntax RecordDeclaration(SyntaxToken keyword, string identifier)
		{
			return SyntaxFactory.RecordDeclaration(keyword, SyntaxFactory.Identifier(identifier));
		}

		// Token: 0x0600286C RID: 10348 RVA: 0x000DBDB8 File Offset: 0x000D9FB8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.RecordDeclarationSyntax RecordDeclaration(SyntaxToken keyword, SyntaxToken identifier)
		{
			return SyntaxFactory.RecordDeclaration(SyntaxKind.RecordDeclaration, default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), keyword, default(SyntaxToken), identifier, null, null, null, default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax>), default(SyntaxToken), default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax>), default(SyntaxToken), default(SyntaxToken));
		}

		// Token: 0x0600286D RID: 10349 RVA: 0x000DBE20 File Offset: 0x000DA020
		public static Microsoft.CodeAnalysis.CSharp.Syntax.RefTypeSyntax RefType(SyntaxToken refKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type)
		{
			return SyntaxFactory.RefType(refKeyword, default(SyntaxToken), type);
		}

		// Token: 0x0600286E RID: 10350 RVA: 0x000DBE40 File Offset: 0x000DA040
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ReturnStatementSyntax ReturnStatement(SyntaxToken returnKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SyntaxToken semicolonToken)
		{
			return SyntaxFactory.ReturnStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), returnKeyword, expression, semicolonToken);
		}

		// Token: 0x0600286F RID: 10351 RVA: 0x000DBE60 File Offset: 0x000DA060
		public static Microsoft.CodeAnalysis.CSharp.Syntax.SimpleLambdaExpressionSyntax SimpleLambdaExpression(SyntaxToken asyncKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax parameter, SyntaxToken arrowToken, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expressionBody)
		{
			return SyntaxFactory.SimpleLambdaExpression(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), SyntaxFactory.TokenList(asyncKeyword), parameter, arrowToken, block, expressionBody);
		}

		// Token: 0x06002870 RID: 10352 RVA: 0x000DBE88 File Offset: 0x000DA088
		public static Microsoft.CodeAnalysis.CSharp.Syntax.SimpleLambdaExpressionSyntax SimpleLambdaExpression(Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax parameter, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expressionBody)
		{
			return SyntaxFactory.SimpleLambdaExpression(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), parameter, block, expressionBody);
		}

		// Token: 0x06002871 RID: 10353 RVA: 0x000DBEB0 File Offset: 0x000DA0B0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.SimpleLambdaExpressionSyntax SimpleLambdaExpression(SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax parameter, SyntaxToken arrowToken, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expressionBody)
		{
			return SyntaxFactory.SimpleLambdaExpression(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), modifiers, parameter, arrowToken, block, expressionBody);
		}

		// Token: 0x06002872 RID: 10354 RVA: 0x000DBED4 File Offset: 0x000DA0D4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.SimpleLambdaExpressionSyntax SimpleLambdaExpression(SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax parameter, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expressionBody)
		{
			return SyntaxFactory.SimpleLambdaExpression(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), modifiers, parameter, block, expressionBody);
		}

		// Token: 0x06002873 RID: 10355 RVA: 0x000DBEF3 File Offset: 0x000DA0F3
		public static Microsoft.CodeAnalysis.CSharp.Syntax.StackAllocArrayCreationExpressionSyntax StackAllocArrayCreationExpression(SyntaxToken stackAllocKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type)
		{
			return SyntaxFactory.StackAllocArrayCreationExpression(stackAllocKeyword, type, null);
		}

		// Token: 0x06002874 RID: 10356 RVA: 0x000DBF00 File Offset: 0x000DA100
		public static Microsoft.CodeAnalysis.CSharp.Syntax.SwitchStatementSyntax SwitchStatement(SyntaxToken switchKeyword, SyntaxToken openParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SyntaxToken closeParenToken, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.SwitchSectionSyntax> sections, SyntaxToken closeBraceToken)
		{
			return SyntaxFactory.SwitchStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), switchKeyword, openParenToken, expression, closeParenToken, openBraceToken, sections, closeBraceToken);
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06002875 RID: 10357 RVA: 0x000DBF25 File Offset: 0x000DA125
		public static SyntaxTrivia CarriageReturnLineFeed { get; } = SyntaxFactory.CarriageReturnLineFeed;

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06002876 RID: 10358 RVA: 0x000DBF2C File Offset: 0x000DA12C
		public static SyntaxTrivia LineFeed { get; } = SyntaxFactory.LineFeed;

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06002877 RID: 10359 RVA: 0x000DBF33 File Offset: 0x000DA133
		public static SyntaxTrivia CarriageReturn { get; } = SyntaxFactory.CarriageReturn;

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06002878 RID: 10360 RVA: 0x000DBF3A File Offset: 0x000DA13A
		public static SyntaxTrivia Space { get; } = SyntaxFactory.Space;

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06002879 RID: 10361 RVA: 0x000DBF41 File Offset: 0x000DA141
		public static SyntaxTrivia Tab { get; } = SyntaxFactory.Tab;

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x0600287A RID: 10362 RVA: 0x000DBF48 File Offset: 0x000DA148
		public static SyntaxTrivia ElasticCarriageReturnLineFeed { get; } = SyntaxFactory.ElasticCarriageReturnLineFeed;

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x0600287B RID: 10363 RVA: 0x000DBF4F File Offset: 0x000DA14F
		public static SyntaxTrivia ElasticLineFeed { get; } = SyntaxFactory.ElasticLineFeed;

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x0600287C RID: 10364 RVA: 0x000DBF56 File Offset: 0x000DA156
		public static SyntaxTrivia ElasticCarriageReturn { get; } = SyntaxFactory.ElasticCarriageReturn;

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x0600287D RID: 10365 RVA: 0x000DBF5D File Offset: 0x000DA15D
		public static SyntaxTrivia ElasticSpace { get; } = SyntaxFactory.ElasticSpace;

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x0600287E RID: 10366 RVA: 0x000DBF64 File Offset: 0x000DA164
		public static SyntaxTrivia ElasticTab { get; } = SyntaxFactory.ElasticTab;

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x0600287F RID: 10367 RVA: 0x000DBF6B File Offset: 0x000DA16B
		public static SyntaxTrivia ElasticMarker { get; } = SyntaxFactory.ElasticZeroSpace;

		// Token: 0x06002880 RID: 10368 RVA: 0x000DBF72 File Offset: 0x000DA172
		public static SyntaxTrivia EndOfLine(string text)
		{
			return SyntaxFactory.EndOfLine(text, false);
		}

		// Token: 0x06002881 RID: 10369 RVA: 0x000DBF80 File Offset: 0x000DA180
		public static SyntaxTrivia ElasticEndOfLine(string text)
		{
			return SyntaxFactory.EndOfLine(text, true);
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x000DBF8E File Offset: 0x000DA18E
		[Obsolete("Use SyntaxFactory.EndOfLine or SyntaxFactory.ElasticEndOfLine")]
		[EditorBrowsable(1)]
		public static SyntaxTrivia EndOfLine(string text, bool elastic)
		{
			return SyntaxFactory.EndOfLine(text, elastic);
		}

		// Token: 0x06002883 RID: 10371 RVA: 0x000DBF9C File Offset: 0x000DA19C
		public static SyntaxTrivia Whitespace(string text)
		{
			return SyntaxFactory.Whitespace(text, false);
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x000DBFAA File Offset: 0x000DA1AA
		public static SyntaxTrivia ElasticWhitespace(string text)
		{
			return SyntaxFactory.Whitespace(text, false);
		}

		// Token: 0x06002885 RID: 10373 RVA: 0x000DBFB8 File Offset: 0x000DA1B8
		[Obsolete("Use SyntaxFactory.Whitespace or SyntaxFactory.ElasticWhitespace")]
		[EditorBrowsable(1)]
		public static SyntaxTrivia Whitespace(string text, bool elastic)
		{
			return SyntaxFactory.Whitespace(text, elastic);
		}

		// Token: 0x06002886 RID: 10374 RVA: 0x000DBFC6 File Offset: 0x000DA1C6
		public static SyntaxTrivia Comment(string text)
		{
			return SyntaxFactory.Comment(text);
		}

		// Token: 0x06002887 RID: 10375 RVA: 0x000DBFD3 File Offset: 0x000DA1D3
		public static SyntaxTrivia DisabledText(string text)
		{
			return SyntaxFactory.DisabledText(text);
		}

		// Token: 0x06002888 RID: 10376 RVA: 0x000DBFE0 File Offset: 0x000DA1E0
		public static SyntaxTrivia PreprocessingMessage(string text)
		{
			return SyntaxFactory.PreprocessingMessage(text);
		}

		// Token: 0x06002889 RID: 10377 RVA: 0x000DBFF0 File Offset: 0x000DA1F0
		public static SyntaxTrivia SyntaxTrivia(SyntaxKind kind, string text)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			if (kind - SyntaxKind.EndOfLineTrivia <= 4 || kind == SyntaxKind.DisabledTextTrivia)
			{
				SyntaxToken syntaxToken = default(SyntaxToken);
				return new SyntaxTrivia(in syntaxToken, new SyntaxTrivia(kind, text, null, null), 0, 0);
			}
			throw new ArgumentException("kind");
		}

		// Token: 0x0600288A RID: 10378 RVA: 0x000DC044 File Offset: 0x000DA244
		public static SyntaxToken Token(SyntaxKind kind)
		{
			return new SyntaxToken(SyntaxFactory.Token(SyntaxFactory.ElasticMarker.UnderlyingNode, kind, SyntaxFactory.ElasticMarker.UnderlyingNode));
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x000DC076 File Offset: 0x000DA276
		public static SyntaxToken Token(SyntaxTriviaList leading, SyntaxKind kind, SyntaxTriviaList trailing)
		{
			return new SyntaxToken(SyntaxFactory.Token(leading.Node, kind, trailing.Node));
		}

		// Token: 0x0600288C RID: 10380 RVA: 0x000DC094 File Offset: 0x000DA294
		public static SyntaxToken Token(SyntaxTriviaList leading, SyntaxKind kind, string text, string valueText, SyntaxTriviaList trailing)
		{
			switch (kind)
			{
			case SyntaxKind.IdentifierToken:
				throw new ArgumentException(CSharpResources.UseVerbatimIdentifier, "kind");
			case SyntaxKind.NumericLiteralToken:
				throw new ArgumentException(CSharpResources.UseLiteralForNumeric, "kind");
			case SyntaxKind.CharacterLiteralToken:
				throw new ArgumentException(CSharpResources.UseLiteralForTokens, "kind");
			default:
				if (!SyntaxFacts.IsAnyToken(kind))
				{
					throw new ArgumentException(string.Format(CSharpResources.ThisMethodCanOnlyBeUsedToCreateTokens, kind), "kind");
				}
				return new SyntaxToken(SyntaxFactory.Token(leading.Node, kind, text, valueText, trailing.Node));
			}
		}

		// Token: 0x0600288D RID: 10381 RVA: 0x000DC12C File Offset: 0x000DA32C
		public static SyntaxToken MissingToken(SyntaxKind kind)
		{
			return new SyntaxToken(SyntaxFactory.MissingToken(SyntaxFactory.ElasticMarker.UnderlyingNode, kind, SyntaxFactory.ElasticMarker.UnderlyingNode));
		}

		// Token: 0x0600288E RID: 10382 RVA: 0x000DC15E File Offset: 0x000DA35E
		public static SyntaxToken MissingToken(SyntaxTriviaList leading, SyntaxKind kind, SyntaxTriviaList trailing)
		{
			return new SyntaxToken(SyntaxFactory.MissingToken(leading.Node, kind, trailing.Node));
		}

		// Token: 0x0600288F RID: 10383 RVA: 0x000DC17C File Offset: 0x000DA37C
		public static SyntaxToken Identifier(string text)
		{
			return new SyntaxToken(SyntaxFactory.Identifier(SyntaxFactory.ElasticMarker.UnderlyingNode, text, SyntaxFactory.ElasticMarker.UnderlyingNode));
		}

		// Token: 0x06002890 RID: 10384 RVA: 0x000DC1AE File Offset: 0x000DA3AE
		public static SyntaxToken Identifier(SyntaxTriviaList leading, string text, SyntaxTriviaList trailing)
		{
			return new SyntaxToken(SyntaxFactory.Identifier(leading.Node, text, trailing.Node));
		}

		// Token: 0x06002891 RID: 10385 RVA: 0x000DC1CC File Offset: 0x000DA3CC
		public static SyntaxToken VerbatimIdentifier(SyntaxTriviaList leading, string text, string valueText, SyntaxTriviaList trailing)
		{
			if (text.StartsWith("@", 4))
			{
				throw new ArgumentException("text should not start with an @ character.");
			}
			return new SyntaxToken(SyntaxFactory.Identifier(SyntaxKind.IdentifierName, leading.Node, "@" + text, valueText, trailing.Node));
		}

		// Token: 0x06002892 RID: 10386 RVA: 0x000DC21B File Offset: 0x000DA41B
		public static SyntaxToken Identifier(SyntaxTriviaList leading, SyntaxKind contextualKind, string text, string valueText, SyntaxTriviaList trailing)
		{
			return new SyntaxToken(SyntaxFactory.Identifier(contextualKind, leading.Node, text, valueText, trailing.Node));
		}

		// Token: 0x06002893 RID: 10387 RVA: 0x000DC238 File Offset: 0x000DA438
		public static SyntaxToken Literal(int value)
		{
			return SyntaxFactory.Literal(ObjectDisplay.FormatLiteral(value, ObjectDisplayOptions.None, null), value);
		}

		// Token: 0x06002894 RID: 10388 RVA: 0x000DC248 File Offset: 0x000DA448
		public static SyntaxToken Literal(string text, int value)
		{
			return new SyntaxToken(SyntaxFactory.Literal(SyntaxFactory.ElasticMarker.UnderlyingNode, text, value, SyntaxFactory.ElasticMarker.UnderlyingNode));
		}

		// Token: 0x06002895 RID: 10389 RVA: 0x000DC27B File Offset: 0x000DA47B
		public static SyntaxToken Literal(SyntaxTriviaList leading, string text, int value, SyntaxTriviaList trailing)
		{
			return new SyntaxToken(SyntaxFactory.Literal(leading.Node, text, value, trailing.Node));
		}

		// Token: 0x06002896 RID: 10390 RVA: 0x000DC297 File Offset: 0x000DA497
		public static SyntaxToken Literal(uint value)
		{
			return SyntaxFactory.Literal(ObjectDisplay.FormatLiteral(value, ObjectDisplayOptions.IncludeTypeSuffix, null), value);
		}

		// Token: 0x06002897 RID: 10391 RVA: 0x000DC2A8 File Offset: 0x000DA4A8
		public static SyntaxToken Literal(string text, uint value)
		{
			return new SyntaxToken(SyntaxFactory.Literal(SyntaxFactory.ElasticMarker.UnderlyingNode, text, value, SyntaxFactory.ElasticMarker.UnderlyingNode));
		}

		// Token: 0x06002898 RID: 10392 RVA: 0x000DC2DB File Offset: 0x000DA4DB
		public static SyntaxToken Literal(SyntaxTriviaList leading, string text, uint value, SyntaxTriviaList trailing)
		{
			return new SyntaxToken(SyntaxFactory.Literal(leading.Node, text, value, trailing.Node));
		}

		// Token: 0x06002899 RID: 10393 RVA: 0x000DC2F7 File Offset: 0x000DA4F7
		public static SyntaxToken Literal(long value)
		{
			return SyntaxFactory.Literal(ObjectDisplay.FormatLiteral(value, ObjectDisplayOptions.IncludeTypeSuffix, null), value);
		}

		// Token: 0x0600289A RID: 10394 RVA: 0x000DC308 File Offset: 0x000DA508
		public static SyntaxToken Literal(string text, long value)
		{
			return new SyntaxToken(SyntaxFactory.Literal(SyntaxFactory.ElasticMarker.UnderlyingNode, text, value, SyntaxFactory.ElasticMarker.UnderlyingNode));
		}

		// Token: 0x0600289B RID: 10395 RVA: 0x000DC33B File Offset: 0x000DA53B
		public static SyntaxToken Literal(SyntaxTriviaList leading, string text, long value, SyntaxTriviaList trailing)
		{
			return new SyntaxToken(SyntaxFactory.Literal(leading.Node, text, value, trailing.Node));
		}

		// Token: 0x0600289C RID: 10396 RVA: 0x000DC357 File Offset: 0x000DA557
		public static SyntaxToken Literal(ulong value)
		{
			return SyntaxFactory.Literal(ObjectDisplay.FormatLiteral(value, ObjectDisplayOptions.IncludeTypeSuffix, null), value);
		}

		// Token: 0x0600289D RID: 10397 RVA: 0x000DC368 File Offset: 0x000DA568
		public static SyntaxToken Literal(string text, ulong value)
		{
			return new SyntaxToken(SyntaxFactory.Literal(SyntaxFactory.ElasticMarker.UnderlyingNode, text, value, SyntaxFactory.ElasticMarker.UnderlyingNode));
		}

		// Token: 0x0600289E RID: 10398 RVA: 0x000DC39B File Offset: 0x000DA59B
		public static SyntaxToken Literal(SyntaxTriviaList leading, string text, ulong value, SyntaxTriviaList trailing)
		{
			return new SyntaxToken(SyntaxFactory.Literal(leading.Node, text, value, trailing.Node));
		}

		// Token: 0x0600289F RID: 10399 RVA: 0x000DC3B7 File Offset: 0x000DA5B7
		public static SyntaxToken Literal(float value)
		{
			return SyntaxFactory.Literal(ObjectDisplay.FormatLiteral(value, ObjectDisplayOptions.IncludeTypeSuffix, null), value);
		}

		// Token: 0x060028A0 RID: 10400 RVA: 0x000DC3C8 File Offset: 0x000DA5C8
		public static SyntaxToken Literal(string text, float value)
		{
			return new SyntaxToken(SyntaxFactory.Literal(SyntaxFactory.ElasticMarker.UnderlyingNode, text, value, SyntaxFactory.ElasticMarker.UnderlyingNode));
		}

		// Token: 0x060028A1 RID: 10401 RVA: 0x000DC3FB File Offset: 0x000DA5FB
		public static SyntaxToken Literal(SyntaxTriviaList leading, string text, float value, SyntaxTriviaList trailing)
		{
			return new SyntaxToken(SyntaxFactory.Literal(leading.Node, text, value, trailing.Node));
		}

		// Token: 0x060028A2 RID: 10402 RVA: 0x000DC417 File Offset: 0x000DA617
		public static SyntaxToken Literal(double value)
		{
			return SyntaxFactory.Literal(ObjectDisplay.FormatLiteral(value, ObjectDisplayOptions.None, null), value);
		}

		// Token: 0x060028A3 RID: 10403 RVA: 0x000DC428 File Offset: 0x000DA628
		public static SyntaxToken Literal(string text, double value)
		{
			return new SyntaxToken(SyntaxFactory.Literal(SyntaxFactory.ElasticMarker.UnderlyingNode, text, value, SyntaxFactory.ElasticMarker.UnderlyingNode));
		}

		// Token: 0x060028A4 RID: 10404 RVA: 0x000DC45B File Offset: 0x000DA65B
		public static SyntaxToken Literal(SyntaxTriviaList leading, string text, double value, SyntaxTriviaList trailing)
		{
			return new SyntaxToken(SyntaxFactory.Literal(leading.Node, text, value, trailing.Node));
		}

		// Token: 0x060028A5 RID: 10405 RVA: 0x000DC477 File Offset: 0x000DA677
		public static SyntaxToken Literal(decimal value)
		{
			return SyntaxFactory.Literal(ObjectDisplay.FormatLiteral(value, ObjectDisplayOptions.IncludeTypeSuffix, null), value);
		}

		// Token: 0x060028A6 RID: 10406 RVA: 0x000DC488 File Offset: 0x000DA688
		public static SyntaxToken Literal(string text, decimal value)
		{
			return new SyntaxToken(SyntaxFactory.Literal(SyntaxFactory.ElasticMarker.UnderlyingNode, text, value, SyntaxFactory.ElasticMarker.UnderlyingNode));
		}

		// Token: 0x060028A7 RID: 10407 RVA: 0x000DC4BB File Offset: 0x000DA6BB
		public static SyntaxToken Literal(SyntaxTriviaList leading, string text, decimal value, SyntaxTriviaList trailing)
		{
			return new SyntaxToken(SyntaxFactory.Literal(leading.Node, text, value, trailing.Node));
		}

		// Token: 0x060028A8 RID: 10408 RVA: 0x000DC4D7 File Offset: 0x000DA6D7
		public static SyntaxToken Literal(string value)
		{
			return SyntaxFactory.Literal(SymbolDisplay.FormatLiteral(value, true), value);
		}

		// Token: 0x060028A9 RID: 10409 RVA: 0x000DC4E8 File Offset: 0x000DA6E8
		public static SyntaxToken Literal(string text, string value)
		{
			return new SyntaxToken(SyntaxFactory.Literal(SyntaxFactory.ElasticMarker.UnderlyingNode, text, value, SyntaxFactory.ElasticMarker.UnderlyingNode));
		}

		// Token: 0x060028AA RID: 10410 RVA: 0x000DC51B File Offset: 0x000DA71B
		public static SyntaxToken Literal(SyntaxTriviaList leading, string text, string value, SyntaxTriviaList trailing)
		{
			return new SyntaxToken(SyntaxFactory.Literal(leading.Node, text, value, trailing.Node));
		}

		// Token: 0x060028AB RID: 10411 RVA: 0x000DC537 File Offset: 0x000DA737
		public static SyntaxToken Literal(char value)
		{
			return SyntaxFactory.Literal(ObjectDisplay.FormatLiteral(value, ObjectDisplayOptions.UseQuotes | ObjectDisplayOptions.EscapeNonPrintableCharacters), value);
		}

		// Token: 0x060028AC RID: 10412 RVA: 0x000DC548 File Offset: 0x000DA748
		public static SyntaxToken Literal(string text, char value)
		{
			return new SyntaxToken(SyntaxFactory.Literal(SyntaxFactory.ElasticMarker.UnderlyingNode, text, value, SyntaxFactory.ElasticMarker.UnderlyingNode));
		}

		// Token: 0x060028AD RID: 10413 RVA: 0x000DC57B File Offset: 0x000DA77B
		public static SyntaxToken Literal(SyntaxTriviaList leading, string text, char value, SyntaxTriviaList trailing)
		{
			return new SyntaxToken(SyntaxFactory.Literal(leading.Node, text, value, trailing.Node));
		}

		// Token: 0x060028AE RID: 10414 RVA: 0x000DC597 File Offset: 0x000DA797
		public static SyntaxToken BadToken(SyntaxTriviaList leading, string text, SyntaxTriviaList trailing)
		{
			return new SyntaxToken(SyntaxFactory.BadToken(leading.Node, text, trailing.Node));
		}

		// Token: 0x060028AF RID: 10415 RVA: 0x000DC5B2 File Offset: 0x000DA7B2
		public static SyntaxToken XmlTextLiteral(SyntaxTriviaList leading, string text, string value, SyntaxTriviaList trailing)
		{
			return new SyntaxToken(SyntaxFactory.XmlTextLiteral(leading.Node, text, value, trailing.Node));
		}

		// Token: 0x060028B0 RID: 10416 RVA: 0x000DC5CE File Offset: 0x000DA7CE
		public static SyntaxToken XmlEntity(SyntaxTriviaList leading, string text, string value, SyntaxTriviaList trailing)
		{
			return new SyntaxToken(SyntaxFactory.XmlEntity(leading.Node, text, value, trailing.Node));
		}

		// Token: 0x060028B1 RID: 10417 RVA: 0x000DC5EC File Offset: 0x000DA7EC
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DocumentationCommentTriviaSyntax DocumentationComment(params Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax[] content)
		{
			return SyntaxFactory.DocumentationCommentTrivia(SyntaxKind.SingleLineDocumentationCommentTrivia, SyntaxFactory.List<Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax>(content)).WithLeadingTrivia(new SyntaxTrivia[] { SyntaxFactory.DocumentationCommentExterior("/// ") }).WithTrailingTrivia(new SyntaxTrivia[] { SyntaxFactory.EndOfLine("") });
		}

		// Token: 0x060028B2 RID: 10418 RVA: 0x000DC641 File Offset: 0x000DA841
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementSyntax XmlSummaryElement(params Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax[] content)
		{
			return SyntaxFactory.XmlSummaryElement(SyntaxFactory.List<Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax>(content));
		}

		// Token: 0x060028B3 RID: 10419 RVA: 0x000DC64E File Offset: 0x000DA84E
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementSyntax XmlSummaryElement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax> content)
		{
			return SyntaxFactory.XmlMultiLineElement("summary", content);
		}

		// Token: 0x060028B4 RID: 10420 RVA: 0x000DC65B File Offset: 0x000DA85B
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlEmptyElementSyntax XmlSeeElement(Microsoft.CodeAnalysis.CSharp.Syntax.CrefSyntax cref)
		{
			return SyntaxFactory.XmlEmptyElement("see").AddAttributes(new Microsoft.CodeAnalysis.CSharp.Syntax.XmlAttributeSyntax[] { SyntaxFactory.XmlCrefAttribute(cref) });
		}

		// Token: 0x060028B5 RID: 10421 RVA: 0x000DC67B File Offset: 0x000DA87B
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlEmptyElementSyntax XmlSeeAlsoElement(Microsoft.CodeAnalysis.CSharp.Syntax.CrefSyntax cref)
		{
			return SyntaxFactory.XmlEmptyElement("seealso").AddAttributes(new Microsoft.CodeAnalysis.CSharp.Syntax.XmlAttributeSyntax[] { SyntaxFactory.XmlCrefAttribute(cref) });
		}

		// Token: 0x060028B6 RID: 10422 RVA: 0x000DC69B File Offset: 0x000DA89B
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementSyntax XmlSeeAlsoElement(Uri linkAddress, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax> linkText)
		{
			Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementSyntax xmlElementSyntax = SyntaxFactory.XmlElement("seealso", linkText);
			return xmlElementSyntax.WithStartTag(xmlElementSyntax.StartTag.AddAttributes(new Microsoft.CodeAnalysis.CSharp.Syntax.XmlAttributeSyntax[] { SyntaxFactory.XmlTextAttribute("cref", linkAddress.ToString()) }));
		}

		// Token: 0x060028B7 RID: 10423 RVA: 0x000DC6D1 File Offset: 0x000DA8D1
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlEmptyElementSyntax XmlThreadSafetyElement()
		{
			return SyntaxFactory.XmlThreadSafetyElement(true, false);
		}

		// Token: 0x060028B8 RID: 10424 RVA: 0x000DC6DC File Offset: 0x000DA8DC
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlEmptyElementSyntax XmlThreadSafetyElement(bool isStatic, bool isInstance)
		{
			return SyntaxFactory.XmlEmptyElement("threadsafety").AddAttributes(new Microsoft.CodeAnalysis.CSharp.Syntax.XmlAttributeSyntax[]
			{
				SyntaxFactory.XmlTextAttribute("static", isStatic.ToString().ToLowerInvariant()),
				SyntaxFactory.XmlTextAttribute("instance", isInstance.ToString().ToLowerInvariant())
			});
		}

		// Token: 0x060028B9 RID: 10425 RVA: 0x000DC730 File Offset: 0x000DA930
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlNameAttributeSyntax XmlNameAttribute(string parameterName)
		{
			return SyntaxFactory.XmlNameAttribute(SyntaxFactory.XmlName("name"), SyntaxFactory.Token(SyntaxKind.DoubleQuoteToken), parameterName, SyntaxFactory.Token(SyntaxKind.DoubleQuoteToken)).WithLeadingTrivia(new SyntaxTrivia[] { SyntaxFactory.Whitespace(" ") });
		}

		// Token: 0x060028BA RID: 10426 RVA: 0x000DC77D File Offset: 0x000DA97D
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlEmptyElementSyntax XmlPreliminaryElement()
		{
			return SyntaxFactory.XmlEmptyElement("preliminary");
		}

		// Token: 0x060028BB RID: 10427 RVA: 0x000DC789 File Offset: 0x000DA989
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlCrefAttributeSyntax XmlCrefAttribute(Microsoft.CodeAnalysis.CSharp.Syntax.CrefSyntax cref)
		{
			return SyntaxFactory.XmlCrefAttribute(cref, SyntaxKind.DoubleQuoteToken);
		}

		// Token: 0x060028BC RID: 10428 RVA: 0x000DC798 File Offset: 0x000DA998
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlCrefAttributeSyntax XmlCrefAttribute(Microsoft.CodeAnalysis.CSharp.Syntax.CrefSyntax cref, SyntaxKind quoteKind)
		{
			cref = cref.ReplaceTokens(cref.DescendantTokens(null, false), new Func<SyntaxToken, SyntaxToken, SyntaxToken>(SyntaxFactory.XmlReplaceBracketTokens));
			return SyntaxFactory.XmlCrefAttribute(SyntaxFactory.XmlName("cref"), SyntaxFactory.Token(quoteKind), cref, SyntaxFactory.Token(quoteKind)).WithLeadingTrivia(new SyntaxTrivia[] { SyntaxFactory.Whitespace(" ") });
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x000DC7F9 File Offset: 0x000DA9F9
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementSyntax XmlRemarksElement(params Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax[] content)
		{
			return SyntaxFactory.XmlRemarksElement(SyntaxFactory.List<Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax>(content));
		}

		// Token: 0x060028BE RID: 10430 RVA: 0x000DC806 File Offset: 0x000DAA06
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementSyntax XmlRemarksElement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax> content)
		{
			return SyntaxFactory.XmlMultiLineElement("remarks", content);
		}

		// Token: 0x060028BF RID: 10431 RVA: 0x000DC813 File Offset: 0x000DAA13
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementSyntax XmlReturnsElement(params Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax[] content)
		{
			return SyntaxFactory.XmlReturnsElement(SyntaxFactory.List<Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax>(content));
		}

		// Token: 0x060028C0 RID: 10432 RVA: 0x000DC820 File Offset: 0x000DAA20
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementSyntax XmlReturnsElement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax> content)
		{
			return SyntaxFactory.XmlMultiLineElement("returns", content);
		}

		// Token: 0x060028C1 RID: 10433 RVA: 0x000DC82D File Offset: 0x000DAA2D
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementSyntax XmlValueElement(params Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax[] content)
		{
			return SyntaxFactory.XmlValueElement(SyntaxFactory.List<Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax>(content));
		}

		// Token: 0x060028C2 RID: 10434 RVA: 0x000DC83A File Offset: 0x000DAA3A
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementSyntax XmlValueElement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax> content)
		{
			return SyntaxFactory.XmlMultiLineElement("value", content);
		}

		// Token: 0x060028C3 RID: 10435 RVA: 0x000DC847 File Offset: 0x000DAA47
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementSyntax XmlExceptionElement(Microsoft.CodeAnalysis.CSharp.Syntax.CrefSyntax cref, params Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax[] content)
		{
			return SyntaxFactory.XmlExceptionElement(cref, SyntaxFactory.List<Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax>(content));
		}

		// Token: 0x060028C4 RID: 10436 RVA: 0x000DC855 File Offset: 0x000DAA55
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementSyntax XmlExceptionElement(Microsoft.CodeAnalysis.CSharp.Syntax.CrefSyntax cref, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax> content)
		{
			Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementSyntax xmlElementSyntax = SyntaxFactory.XmlElement("exception", content);
			return xmlElementSyntax.WithStartTag(xmlElementSyntax.StartTag.AddAttributes(new Microsoft.CodeAnalysis.CSharp.Syntax.XmlAttributeSyntax[] { SyntaxFactory.XmlCrefAttribute(cref) }));
		}

		// Token: 0x060028C5 RID: 10437 RVA: 0x000DC881 File Offset: 0x000DAA81
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementSyntax XmlPermissionElement(Microsoft.CodeAnalysis.CSharp.Syntax.CrefSyntax cref, params Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax[] content)
		{
			return SyntaxFactory.XmlPermissionElement(cref, SyntaxFactory.List<Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax>(content));
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x000DC88F File Offset: 0x000DAA8F
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementSyntax XmlPermissionElement(Microsoft.CodeAnalysis.CSharp.Syntax.CrefSyntax cref, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax> content)
		{
			Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementSyntax xmlElementSyntax = SyntaxFactory.XmlElement("permission", content);
			return xmlElementSyntax.WithStartTag(xmlElementSyntax.StartTag.AddAttributes(new Microsoft.CodeAnalysis.CSharp.Syntax.XmlAttributeSyntax[] { SyntaxFactory.XmlCrefAttribute(cref) }));
		}

		// Token: 0x060028C7 RID: 10439 RVA: 0x000DC8BB File Offset: 0x000DAABB
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementSyntax XmlExampleElement(params Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax[] content)
		{
			return SyntaxFactory.XmlExampleElement(SyntaxFactory.List<Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax>(content));
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x000DC8C8 File Offset: 0x000DAAC8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementSyntax XmlExampleElement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax> content)
		{
			Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementSyntax xmlElementSyntax = SyntaxFactory.XmlElement("example", content);
			return xmlElementSyntax.WithStartTag(xmlElementSyntax.StartTag);
		}

		// Token: 0x060028C9 RID: 10441 RVA: 0x000DC8E0 File Offset: 0x000DAAE0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementSyntax XmlParaElement(params Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax[] content)
		{
			return SyntaxFactory.XmlParaElement(SyntaxFactory.List<Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax>(content));
		}

		// Token: 0x060028CA RID: 10442 RVA: 0x000DC8ED File Offset: 0x000DAAED
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementSyntax XmlParaElement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax> content)
		{
			return SyntaxFactory.XmlElement("para", content);
		}

		// Token: 0x060028CB RID: 10443 RVA: 0x000DC8FA File Offset: 0x000DAAFA
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementSyntax XmlParamElement(string parameterName, params Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax[] content)
		{
			return SyntaxFactory.XmlParamElement(parameterName, SyntaxFactory.List<Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax>(content));
		}

		// Token: 0x060028CC RID: 10444 RVA: 0x000DC908 File Offset: 0x000DAB08
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementSyntax XmlParamElement(string parameterName, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax> content)
		{
			Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementSyntax xmlElementSyntax = SyntaxFactory.XmlElement("param", content);
			return xmlElementSyntax.WithStartTag(xmlElementSyntax.StartTag.AddAttributes(new Microsoft.CodeAnalysis.CSharp.Syntax.XmlAttributeSyntax[] { SyntaxFactory.XmlNameAttribute(parameterName) }));
		}

		// Token: 0x060028CD RID: 10445 RVA: 0x000DC934 File Offset: 0x000DAB34
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlEmptyElementSyntax XmlParamRefElement(string parameterName)
		{
			return SyntaxFactory.XmlEmptyElement("paramref").AddAttributes(new Microsoft.CodeAnalysis.CSharp.Syntax.XmlAttributeSyntax[] { SyntaxFactory.XmlNameAttribute(parameterName) });
		}

		// Token: 0x060028CE RID: 10446 RVA: 0x000DC954 File Offset: 0x000DAB54
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlEmptyElementSyntax XmlNullKeywordElement()
		{
			return SyntaxFactory.XmlKeywordElement("null");
		}

		// Token: 0x060028CF RID: 10447 RVA: 0x000DC960 File Offset: 0x000DAB60
		private static Microsoft.CodeAnalysis.CSharp.Syntax.XmlEmptyElementSyntax XmlKeywordElement(string keyword)
		{
			return SyntaxFactory.XmlEmptyElement("see").AddAttributes(new Microsoft.CodeAnalysis.CSharp.Syntax.XmlAttributeSyntax[] { SyntaxFactory.XmlTextAttribute("langword", keyword) });
		}

		// Token: 0x060028D0 RID: 10448 RVA: 0x000DC985 File Offset: 0x000DAB85
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementSyntax XmlPlaceholderElement(params Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax[] content)
		{
			return SyntaxFactory.XmlPlaceholderElement(SyntaxFactory.List<Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax>(content));
		}

		// Token: 0x060028D1 RID: 10449 RVA: 0x000DC992 File Offset: 0x000DAB92
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementSyntax XmlPlaceholderElement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax> content)
		{
			return SyntaxFactory.XmlElement("placeholder", content);
		}

		// Token: 0x060028D2 RID: 10450 RVA: 0x000DC99F File Offset: 0x000DAB9F
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlEmptyElementSyntax XmlEmptyElement(string localName)
		{
			return SyntaxFactory.XmlEmptyElement(SyntaxFactory.XmlName(localName));
		}

		// Token: 0x060028D3 RID: 10451 RVA: 0x000DC9AC File Offset: 0x000DABAC
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementSyntax XmlElement(string localName, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax> content)
		{
			return SyntaxFactory.XmlElement(SyntaxFactory.XmlName(localName), content);
		}

		// Token: 0x060028D4 RID: 10452 RVA: 0x000DC9BA File Offset: 0x000DABBA
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementSyntax XmlElement(Microsoft.CodeAnalysis.CSharp.Syntax.XmlNameSyntax name, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax> content)
		{
			return SyntaxFactory.XmlElement(SyntaxFactory.XmlElementStartTag(name), content, SyntaxFactory.XmlElementEndTag(name));
		}

		// Token: 0x060028D5 RID: 10453 RVA: 0x000DC9CE File Offset: 0x000DABCE
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlTextAttributeSyntax XmlTextAttribute(string name, string value)
		{
			return SyntaxFactory.XmlTextAttribute(name, new SyntaxToken[] { SyntaxFactory.XmlTextLiteral(value) });
		}

		// Token: 0x060028D6 RID: 10454 RVA: 0x000DC9E9 File Offset: 0x000DABE9
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlTextAttributeSyntax XmlTextAttribute(string name, params SyntaxToken[] textTokens)
		{
			return SyntaxFactory.XmlTextAttribute(SyntaxFactory.XmlName(name), SyntaxKind.DoubleQuoteToken, SyntaxFactory.TokenList(textTokens));
		}

		// Token: 0x060028D7 RID: 10455 RVA: 0x000DCA01 File Offset: 0x000DAC01
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlTextAttributeSyntax XmlTextAttribute(string name, SyntaxKind quoteKind, SyntaxTokenList textTokens)
		{
			return SyntaxFactory.XmlTextAttribute(SyntaxFactory.XmlName(name), quoteKind, textTokens);
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x000DCA10 File Offset: 0x000DAC10
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlTextAttributeSyntax XmlTextAttribute(Microsoft.CodeAnalysis.CSharp.Syntax.XmlNameSyntax name, SyntaxKind quoteKind, SyntaxTokenList textTokens)
		{
			return SyntaxFactory.XmlTextAttribute(name, SyntaxFactory.Token(quoteKind), textTokens, SyntaxFactory.Token(quoteKind)).WithLeadingTrivia(new SyntaxTrivia[] { SyntaxFactory.Whitespace(" ") });
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x000DCA41 File Offset: 0x000DAC41
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementSyntax XmlMultiLineElement(string localName, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax> content)
		{
			return SyntaxFactory.XmlMultiLineElement(SyntaxFactory.XmlName(localName), content);
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x000DCA4F File Offset: 0x000DAC4F
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementSyntax XmlMultiLineElement(Microsoft.CodeAnalysis.CSharp.Syntax.XmlNameSyntax name, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax> content)
		{
			return SyntaxFactory.XmlElement(SyntaxFactory.XmlElementStartTag(name), content, SyntaxFactory.XmlElementEndTag(name));
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x000DCA63 File Offset: 0x000DAC63
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlTextSyntax XmlNewLine(string text)
		{
			return SyntaxFactory.XmlText(new SyntaxToken[] { SyntaxFactory.XmlTextNewLine(text) });
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x000DCA7D File Offset: 0x000DAC7D
		public static SyntaxToken XmlTextNewLine(string text)
		{
			return SyntaxFactory.XmlTextNewLine(text, true);
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x000DCA86 File Offset: 0x000DAC86
		public static SyntaxToken XmlTextNewLine(SyntaxTriviaList leading, string text, string value, SyntaxTriviaList trailing)
		{
			return new SyntaxToken(SyntaxFactory.XmlTextNewLine(leading.Node, text, value, trailing.Node));
		}

		// Token: 0x060028DE RID: 10462 RVA: 0x000DCAA4 File Offset: 0x000DACA4
		public static SyntaxToken XmlTextNewLine(string text, bool continueXmlDocumentationComment)
		{
			SyntaxToken syntaxToken = new SyntaxToken(SyntaxFactory.XmlTextNewLine(SyntaxFactory.ElasticMarker.UnderlyingNode, text, text, SyntaxFactory.ElasticMarker.UnderlyingNode));
			if (continueXmlDocumentationComment)
			{
				syntaxToken = syntaxToken.WithTrailingTrivia(syntaxToken.TrailingTrivia.Add(SyntaxFactory.DocumentationCommentExterior("/// ")));
			}
			return syntaxToken;
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x000DCAFE File Offset: 0x000DACFE
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlTextSyntax XmlText(string value)
		{
			return SyntaxFactory.XmlText(new SyntaxToken[] { SyntaxFactory.XmlTextLiteral(value) });
		}

		// Token: 0x060028E0 RID: 10464 RVA: 0x000DCB18 File Offset: 0x000DAD18
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlTextSyntax XmlText(params SyntaxToken[] textTokens)
		{
			return SyntaxFactory.XmlText(SyntaxFactory.TokenList(textTokens));
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x000DCB28 File Offset: 0x000DAD28
		public static SyntaxToken XmlTextLiteral(string value)
		{
			string text = new XText(value).ToString();
			return SyntaxFactory.XmlTextLiteral(SyntaxFactory.TriviaList(), text, value, SyntaxFactory.TriviaList());
		}

		// Token: 0x060028E2 RID: 10466 RVA: 0x000DCB54 File Offset: 0x000DAD54
		public static SyntaxToken XmlTextLiteral(string text, string value)
		{
			return new SyntaxToken(SyntaxFactory.XmlTextLiteral(SyntaxFactory.ElasticMarker.UnderlyingNode, text, value, SyntaxFactory.ElasticMarker.UnderlyingNode));
		}

		// Token: 0x060028E3 RID: 10467 RVA: 0x000DCB88 File Offset: 0x000DAD88
		private static SyntaxToken XmlReplaceBracketTokens(SyntaxToken originalToken, SyntaxToken rewrittenToken)
		{
			if (rewrittenToken.IsKind(SyntaxKind.LessThanToken) && string.Equals("<", rewrittenToken.Text, 4))
			{
				return SyntaxFactory.Token(rewrittenToken.LeadingTrivia, SyntaxKind.LessThanToken, "{", rewrittenToken.ValueText, rewrittenToken.TrailingTrivia);
			}
			if (rewrittenToken.IsKind(SyntaxKind.GreaterThanToken) && string.Equals(">", rewrittenToken.Text, 4))
			{
				return SyntaxFactory.Token(rewrittenToken.LeadingTrivia, SyntaxKind.GreaterThanToken, "}", rewrittenToken.ValueText, rewrittenToken.TrailingTrivia);
			}
			return rewrittenToken;
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x000DCC22 File Offset: 0x000DAE22
		public static SyntaxTrivia DocumentationCommentExterior(string text)
		{
			return SyntaxFactory.DocumentationCommentExteriorTrivia(text);
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x000DCC30 File Offset: 0x000DAE30
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public static SyntaxList<TNode> List<TNode>() where TNode : SyntaxNode
		{
			return default(SyntaxList<TNode>);
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x000DCC46 File Offset: 0x000DAE46
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public static SyntaxList<TNode> SingletonList<[global::System.Runtime.CompilerServices.Nullable(0)] TNode>(TNode node) where TNode : SyntaxNode
		{
			return new SyntaxList<TNode>(node);
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x000DCC4E File Offset: 0x000DAE4E
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public static SyntaxList<TNode> List<[global::System.Runtime.CompilerServices.Nullable(0)] TNode>(IEnumerable<TNode> nodes) where TNode : SyntaxNode
		{
			return new SyntaxList<TNode>(nodes);
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x000DCC58 File Offset: 0x000DAE58
		public static SyntaxTokenList TokenList()
		{
			return default(SyntaxTokenList);
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x000DCC6E File Offset: 0x000DAE6E
		public static SyntaxTokenList TokenList(SyntaxToken token)
		{
			return new SyntaxTokenList(token);
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x000DCC76 File Offset: 0x000DAE76
		public static SyntaxTokenList TokenList(params SyntaxToken[] tokens)
		{
			return new SyntaxTokenList(tokens);
		}

		// Token: 0x060028EB RID: 10475 RVA: 0x000DCC7E File Offset: 0x000DAE7E
		public static SyntaxTokenList TokenList(IEnumerable<SyntaxToken> tokens)
		{
			return new SyntaxTokenList(tokens);
		}

		// Token: 0x060028EC RID: 10476 RVA: 0x000DCC88 File Offset: 0x000DAE88
		public static SyntaxTrivia Trivia(Microsoft.CodeAnalysis.CSharp.Syntax.StructuredTriviaSyntax node)
		{
			SyntaxToken syntaxToken = default(SyntaxToken);
			return new SyntaxTrivia(in syntaxToken, node.Green, 0, 0);
		}

		// Token: 0x060028ED RID: 10477 RVA: 0x000DCCAC File Offset: 0x000DAEAC
		public static SyntaxTriviaList TriviaList()
		{
			return default(SyntaxTriviaList);
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x000DCCC2 File Offset: 0x000DAEC2
		public static SyntaxTriviaList TriviaList(SyntaxTrivia trivia)
		{
			return new SyntaxTriviaList(trivia);
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x000DCCCA File Offset: 0x000DAECA
		public static SyntaxTriviaList TriviaList(params SyntaxTrivia[] trivias)
		{
			return new SyntaxTriviaList(trivias);
		}

		// Token: 0x060028F0 RID: 10480 RVA: 0x000DCCD2 File Offset: 0x000DAED2
		public static SyntaxTriviaList TriviaList(IEnumerable<SyntaxTrivia> trivias)
		{
			return new SyntaxTriviaList(trivias);
		}

		// Token: 0x060028F1 RID: 10481 RVA: 0x000DCCDC File Offset: 0x000DAEDC
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public static SeparatedSyntaxList<TNode> SeparatedList<TNode>() where TNode : SyntaxNode
		{
			return default(SeparatedSyntaxList<TNode>);
		}

		// Token: 0x060028F2 RID: 10482 RVA: 0x000DCCF2 File Offset: 0x000DAEF2
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public static SeparatedSyntaxList<TNode> SingletonSeparatedList<[global::System.Runtime.CompilerServices.Nullable(0)] TNode>(TNode node) where TNode : SyntaxNode
		{
			return new SeparatedSyntaxList<TNode>(new SyntaxNodeOrTokenList(node, 0));
		}

		// Token: 0x060028F3 RID: 10483 RVA: 0x000DCD08 File Offset: 0x000DAF08
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public static SeparatedSyntaxList<TNode> SeparatedList<TNode>([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] IEnumerable<TNode> nodes) where TNode : SyntaxNode
		{
			SeparatedSyntaxList<TNode> separatedSyntaxList;
			if (nodes == null)
			{
				separatedSyntaxList = default(SeparatedSyntaxList<TNode>);
				return separatedSyntaxList;
			}
			ICollection<TNode> collection = nodes as ICollection<TNode>;
			if (collection != null && collection.Count == 0)
			{
				separatedSyntaxList = default(SeparatedSyntaxList<TNode>);
				return separatedSyntaxList;
			}
			using (IEnumerator<TNode> enumerator = nodes.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					separatedSyntaxList = default(SeparatedSyntaxList<TNode>);
					separatedSyntaxList = separatedSyntaxList;
				}
				else
				{
					TNode tnode = enumerator.Current;
					if (!enumerator.MoveNext())
					{
						separatedSyntaxList = SyntaxFactory.SingletonSeparatedList<TNode>(tnode);
					}
					else
					{
						Microsoft.CodeAnalysis.Syntax.SeparatedSyntaxListBuilder<TNode> separatedSyntaxListBuilder = new Microsoft.CodeAnalysis.Syntax.SeparatedSyntaxListBuilder<TNode>((collection != null) ? collection.Count : 3);
						separatedSyntaxListBuilder.Add(tnode);
						SyntaxToken syntaxToken = SyntaxFactory.Token(SyntaxKind.CommaToken);
						do
						{
							separatedSyntaxListBuilder.AddSeparator(in syntaxToken);
							separatedSyntaxListBuilder.Add(enumerator.Current);
						}
						while (enumerator.MoveNext());
						separatedSyntaxList = separatedSyntaxListBuilder.ToList();
					}
				}
			}
			return separatedSyntaxList;
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x000DCDE0 File Offset: 0x000DAFE0
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public static SeparatedSyntaxList<TNode> SeparatedList<[global::System.Runtime.CompilerServices.Nullable(0)] TNode>(IEnumerable<TNode> nodes, IEnumerable<SyntaxToken> separators) where TNode : SyntaxNode
		{
			if (nodes != null)
			{
				IEnumerator<TNode> enumerator = nodes.GetEnumerator();
				Microsoft.CodeAnalysis.Syntax.SeparatedSyntaxListBuilder<TNode> separatedSyntaxListBuilder = Microsoft.CodeAnalysis.Syntax.SeparatedSyntaxListBuilder<TNode>.Create();
				if (separators != null)
				{
					foreach (SyntaxToken syntaxToken in separators)
					{
						if (!enumerator.MoveNext())
						{
							throw new ArgumentException("nodes must not be empty.", "nodes");
						}
						separatedSyntaxListBuilder.Add(enumerator.Current);
						separatedSyntaxListBuilder.AddSeparator(in syntaxToken);
					}
				}
				if (enumerator.MoveNext())
				{
					separatedSyntaxListBuilder.Add(enumerator.Current);
					if (enumerator.MoveNext())
					{
						throw new ArgumentException("separators must have 1 fewer element than nodes", "separators");
					}
				}
				return separatedSyntaxListBuilder.ToList();
			}
			if (separators != null)
			{
				throw new ArgumentException("When nodes is null, separators must also be null.", "separators");
			}
			return default(SeparatedSyntaxList<TNode>);
		}

		// Token: 0x060028F5 RID: 10485 RVA: 0x000DCEBC File Offset: 0x000DB0BC
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public static SeparatedSyntaxList<TNode> SeparatedList<[global::System.Runtime.CompilerServices.Nullable(0)] TNode>(IEnumerable<SyntaxNodeOrToken> nodesAndTokens) where TNode : SyntaxNode
		{
			return SyntaxFactory.SeparatedList<TNode>(SyntaxFactory.NodeOrTokenList(nodesAndTokens));
		}

		// Token: 0x060028F6 RID: 10486 RVA: 0x000DCEC9 File Offset: 0x000DB0C9
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public static SeparatedSyntaxList<TNode> SeparatedList<TNode>(SyntaxNodeOrTokenList nodesAndTokens) where TNode : SyntaxNode
		{
			if (!SyntaxFactory.HasSeparatedNodeTokenPattern(nodesAndTokens))
			{
				throw new ArgumentException(CodeAnalysisResources.NodeOrTokenOutOfSequence);
			}
			if (!SyntaxFactory.NodesAreCorrectType<TNode>(nodesAndTokens))
			{
				throw new ArgumentException(CodeAnalysisResources.UnexpectedTypeOfNodeInList);
			}
			return new SeparatedSyntaxList<TNode>(nodesAndTokens);
		}

		// Token: 0x060028F7 RID: 10487 RVA: 0x000DCEF8 File Offset: 0x000DB0F8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private static bool NodesAreCorrectType<TNode>(SyntaxNodeOrTokenList list)
		{
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				SyntaxNodeOrToken syntaxNodeOrToken = list[i];
				if (syntaxNodeOrToken.IsNode && !(syntaxNodeOrToken.AsNode() is TNode))
				{
					return false;
				}
				i++;
			}
			return true;
		}

		// Token: 0x060028F8 RID: 10488 RVA: 0x000DCF3C File Offset: 0x000DB13C
		private static bool HasSeparatedNodeTokenPattern(SyntaxNodeOrTokenList list)
		{
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				if (list[i].IsToken == ((i & 1) == 0))
				{
					return false;
				}
				i++;
			}
			return true;
		}

		// Token: 0x060028F9 RID: 10489 RVA: 0x000DCF78 File Offset: 0x000DB178
		public static SyntaxNodeOrTokenList NodeOrTokenList()
		{
			return default(SyntaxNodeOrTokenList);
		}

		// Token: 0x060028FA RID: 10490 RVA: 0x000DCF8E File Offset: 0x000DB18E
		public static SyntaxNodeOrTokenList NodeOrTokenList(IEnumerable<SyntaxNodeOrToken> nodesAndTokens)
		{
			return new SyntaxNodeOrTokenList(nodesAndTokens);
		}

		// Token: 0x060028FB RID: 10491 RVA: 0x000DCF96 File Offset: 0x000DB196
		public static SyntaxNodeOrTokenList NodeOrTokenList(params SyntaxNodeOrToken[] nodesAndTokens)
		{
			return new SyntaxNodeOrTokenList(nodesAndTokens);
		}

		// Token: 0x060028FC RID: 10492 RVA: 0x000DCF9E File Offset: 0x000DB19E
		public static Microsoft.CodeAnalysis.CSharp.Syntax.IdentifierNameSyntax IdentifierName(string name)
		{
			return SyntaxFactory.IdentifierName(SyntaxFactory.Identifier(name));
		}

		// Token: 0x060028FD RID: 10493 RVA: 0x000DCFAB File Offset: 0x000DB1AB
		public static SyntaxTree SyntaxTree(SyntaxNode root, [global::System.Runtime.CompilerServices.Nullable(2)] ParseOptions options = null, string path = "", [global::System.Runtime.CompilerServices.Nullable(2)] Encoding encoding = null)
		{
			return CSharpSyntaxTree.Create((CSharpSyntaxNode)root, (CSharpParseOptions)options, path, encoding);
		}

		// Token: 0x060028FE RID: 10494 RVA: 0x000DCFC0 File Offset: 0x000DB1C0
		public static SyntaxTree ParseSyntaxTree(string text, [global::System.Runtime.CompilerServices.Nullable(2)] ParseOptions options = null, string path = "", [global::System.Runtime.CompilerServices.Nullable(2)] Encoding encoding = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return CSharpSyntaxTree.ParseText(text, (CSharpParseOptions)options, path, encoding, cancellationToken);
		}

		// Token: 0x060028FF RID: 10495 RVA: 0x000DCFD2 File Offset: 0x000DB1D2
		public static SyntaxTree ParseSyntaxTree(SourceText text, [global::System.Runtime.CompilerServices.Nullable(2)] ParseOptions options = null, string path = "", CancellationToken cancellationToken = default(CancellationToken))
		{
			return CSharpSyntaxTree.ParseText(text, (CSharpParseOptions)options, path, cancellationToken);
		}

		// Token: 0x06002900 RID: 10496 RVA: 0x000DCFE2 File Offset: 0x000DB1E2
		public static SyntaxTriviaList ParseLeadingTrivia(string text, int offset = 0)
		{
			return SyntaxFactory.ParseLeadingTrivia(text, CSharpParseOptions.Default, offset);
		}

		// Token: 0x06002901 RID: 10497 RVA: 0x000DCFF0 File Offset: 0x000DB1F0
		internal static SyntaxTriviaList ParseLeadingTrivia(string text, [global::System.Runtime.CompilerServices.Nullable(2)] CSharpParseOptions options, int offset = 0)
		{
			SyntaxTriviaList syntaxTriviaList;
			using (Lexer lexer = new Lexer(SyntaxFactory.MakeSourceText(text, offset), options, true, false))
			{
				syntaxTriviaList = lexer.LexSyntaxLeadingTrivia();
			}
			return syntaxTriviaList;
		}

		// Token: 0x06002902 RID: 10498 RVA: 0x000DD034 File Offset: 0x000DB234
		public static SyntaxTriviaList ParseTrailingTrivia(string text, int offset = 0)
		{
			SyntaxTriviaList syntaxTriviaList;
			using (Lexer lexer = new Lexer(SyntaxFactory.MakeSourceText(text, offset), CSharpParseOptions.Default, true, false))
			{
				syntaxTriviaList = lexer.LexSyntaxTrailingTrivia();
			}
			return syntaxTriviaList;
		}

		// Token: 0x06002903 RID: 10499 RVA: 0x000DD07C File Offset: 0x000DB27C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal static Microsoft.CodeAnalysis.CSharp.Syntax.CrefSyntax ParseCref(string text)
		{
			Microsoft.CodeAnalysis.CSharp.Syntax.XmlAttributeSyntax xmlAttributeSyntax = ((Microsoft.CodeAnalysis.CSharp.Syntax.XmlEmptyElementSyntax)((Microsoft.CodeAnalysis.CSharp.Syntax.DocumentationCommentTriviaSyntax)SyntaxFactory.ParseLeadingTrivia(string.Format("/// <see cref=\"{0}\"/>", text), CSharpParseOptions.Default.WithDocumentationMode(DocumentationMode.Diagnose), 0).First().GetStructure()).Content[1]).Attributes[0];
			if (xmlAttributeSyntax.Kind() != SyntaxKind.XmlCrefAttribute)
			{
				return null;
			}
			return ((Microsoft.CodeAnalysis.CSharp.Syntax.XmlCrefAttributeSyntax)xmlAttributeSyntax).Cref;
		}

		// Token: 0x06002904 RID: 10500 RVA: 0x000DD0F8 File Offset: 0x000DB2F8
		public static SyntaxToken ParseToken(string text, int offset = 0)
		{
			SyntaxToken syntaxToken;
			using (Lexer lexer = new Lexer(SyntaxFactory.MakeSourceText(text, offset), CSharpParseOptions.Default, true, false))
			{
				syntaxToken = new SyntaxToken(lexer.Lex(LexerMode.Syntax));
			}
			return syntaxToken;
		}

		// Token: 0x06002905 RID: 10501 RVA: 0x000DD144 File Offset: 0x000DB344
		public static IEnumerable<SyntaxToken> ParseTokens(string text, int offset = 0, int initialTokenPosition = 0, [global::System.Runtime.CompilerServices.Nullable(2)] CSharpParseOptions options = null)
		{
			using (Lexer lexer = new Lexer(SyntaxFactory.MakeSourceText(text, offset), options ?? CSharpParseOptions.Default, true, false))
			{
				int position = initialTokenPosition;
				for (;;)
				{
					SyntaxToken token = lexer.Lex(LexerMode.Syntax);
					yield return new SyntaxToken(null, token, position, 0);
					position += token.FullWidth;
					if (token.Kind == SyntaxKind.EndOfFileToken)
					{
						break;
					}
					token = null;
				}
			}
			Lexer lexer = null;
			yield break;
			yield break;
		}

		// Token: 0x06002906 RID: 10502 RVA: 0x000DD16C File Offset: 0x000DB36C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.NameSyntax ParseName(string text, int offset = 0, bool consumeFullText = true)
		{
			Microsoft.CodeAnalysis.CSharp.Syntax.NameSyntax nameSyntax2;
			using (Lexer lexer = SyntaxFactory.MakeLexer(text, offset, null))
			{
				using (LanguageParser languageParser = SyntaxFactory.MakeParser(lexer))
				{
					Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.NameSyntax nameSyntax = languageParser.ParseName();
					if (consumeFullText)
					{
						nameSyntax = languageParser.ConsumeUnexpectedTokens<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.NameSyntax>(nameSyntax);
					}
					nameSyntax2 = (Microsoft.CodeAnalysis.CSharp.Syntax.NameSyntax)nameSyntax.CreateRed();
				}
			}
			return nameSyntax2;
		}

		// Token: 0x06002907 RID: 10503 RVA: 0x000DD1DC File Offset: 0x000DB3DC
		[EditorBrowsable(1)]
		public static Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax ParseTypeName(string text, int offset, bool consumeFullText)
		{
			return SyntaxFactory.ParseTypeName(text, offset, null, consumeFullText);
		}

		// Token: 0x06002908 RID: 10504 RVA: 0x000DD1E8 File Offset: 0x000DB3E8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax ParseTypeName(string text, int offset = 0, [global::System.Runtime.CompilerServices.Nullable(2)] ParseOptions options = null, bool consumeFullText = true)
		{
			Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax typeSyntax2;
			using (Lexer lexer = SyntaxFactory.MakeLexer(text, offset, (CSharpParseOptions)options))
			{
				using (LanguageParser languageParser = SyntaxFactory.MakeParser(lexer))
				{
					Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax typeSyntax = languageParser.ParseTypeName();
					if (consumeFullText)
					{
						typeSyntax = languageParser.ConsumeUnexpectedTokens<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax>(typeSyntax);
					}
					typeSyntax2 = (Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax)typeSyntax.CreateRed();
				}
			}
			return typeSyntax2;
		}

		// Token: 0x06002909 RID: 10505 RVA: 0x000DD25C File Offset: 0x000DB45C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax ParseExpression(string text, int offset = 0, [global::System.Runtime.CompilerServices.Nullable(2)] ParseOptions options = null, bool consumeFullText = true)
		{
			Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expressionSyntax2;
			using (Lexer lexer = SyntaxFactory.MakeLexer(text, offset, (CSharpParseOptions)options))
			{
				using (LanguageParser languageParser = SyntaxFactory.MakeParser(lexer))
				{
					Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax expressionSyntax = languageParser.ParseExpression();
					if (consumeFullText)
					{
						expressionSyntax = languageParser.ConsumeUnexpectedTokens<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax>(expressionSyntax);
					}
					expressionSyntax2 = (Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax)expressionSyntax.CreateRed();
				}
			}
			return expressionSyntax2;
		}

		// Token: 0x0600290A RID: 10506 RVA: 0x000DD2D0 File Offset: 0x000DB4D0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax ParseStatement(string text, int offset = 0, [global::System.Runtime.CompilerServices.Nullable(2)] ParseOptions options = null, bool consumeFullText = true)
		{
			Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statementSyntax2;
			using (Lexer lexer = SyntaxFactory.MakeLexer(text, offset, (CSharpParseOptions)options))
			{
				using (LanguageParser languageParser = SyntaxFactory.MakeParser(lexer))
				{
					Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.StatementSyntax statementSyntax = languageParser.ParseStatement();
					if (consumeFullText)
					{
						statementSyntax = languageParser.ConsumeUnexpectedTokens<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.StatementSyntax>(statementSyntax);
					}
					statementSyntax2 = (Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax)statementSyntax.CreateRed();
				}
			}
			return statementSyntax2;
		}

		// Token: 0x0600290B RID: 10507 RVA: 0x000DD344 File Offset: 0x000DB544
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax ParseMemberDeclaration([global::System.Runtime.CompilerServices.Nullable(1)] string text, int offset = 0, ParseOptions options = null, bool consumeFullText = true)
		{
			Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax memberDeclarationSyntax2;
			using (Lexer lexer = SyntaxFactory.MakeLexer(text, offset, (CSharpParseOptions)options))
			{
				using (LanguageParser languageParser = SyntaxFactory.MakeParser(lexer))
				{
					Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.MemberDeclarationSyntax memberDeclarationSyntax = languageParser.ParseMemberDeclaration();
					if (memberDeclarationSyntax == null)
					{
						memberDeclarationSyntax2 = null;
					}
					else
					{
						memberDeclarationSyntax2 = (Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax)(consumeFullText ? languageParser.ConsumeUnexpectedTokens<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.MemberDeclarationSyntax>(memberDeclarationSyntax) : memberDeclarationSyntax).CreateRed();
					}
				}
			}
			return memberDeclarationSyntax2;
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x000DD3C0 File Offset: 0x000DB5C0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax ParseCompilationUnit(string text, int offset = 0, [global::System.Runtime.CompilerServices.Nullable(2)] CSharpParseOptions options = null)
		{
			Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax compilationUnitSyntax;
			using (Lexer lexer = SyntaxFactory.MakeLexer(text, offset, options))
			{
				using (LanguageParser languageParser = SyntaxFactory.MakeParser(lexer))
				{
					compilationUnitSyntax = (Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax)languageParser.ParseCompilationUnit().CreateRed();
				}
			}
			return compilationUnitSyntax;
		}

		// Token: 0x0600290D RID: 10509 RVA: 0x000DD424 File Offset: 0x000DB624
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax ParseParameterList(string text, int offset = 0, [global::System.Runtime.CompilerServices.Nullable(2)] ParseOptions options = null, bool consumeFullText = true)
		{
			Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterListSyntax2;
			using (Lexer lexer = SyntaxFactory.MakeLexer(text, offset, (CSharpParseOptions)options))
			{
				using (LanguageParser languageParser = SyntaxFactory.MakeParser(lexer))
				{
					Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ParameterListSyntax parameterListSyntax = languageParser.ParseParenthesizedParameterList();
					if (consumeFullText)
					{
						parameterListSyntax = languageParser.ConsumeUnexpectedTokens<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ParameterListSyntax>(parameterListSyntax);
					}
					parameterListSyntax2 = (Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax)parameterListSyntax.CreateRed();
				}
			}
			return parameterListSyntax2;
		}

		// Token: 0x0600290E RID: 10510 RVA: 0x000DD498 File Offset: 0x000DB698
		public static Microsoft.CodeAnalysis.CSharp.Syntax.BracketedParameterListSyntax ParseBracketedParameterList(string text, int offset = 0, [global::System.Runtime.CompilerServices.Nullable(2)] ParseOptions options = null, bool consumeFullText = true)
		{
			Microsoft.CodeAnalysis.CSharp.Syntax.BracketedParameterListSyntax bracketedParameterListSyntax2;
			using (Lexer lexer = SyntaxFactory.MakeLexer(text, offset, (CSharpParseOptions)options))
			{
				using (LanguageParser languageParser = SyntaxFactory.MakeParser(lexer))
				{
					Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.BracketedParameterListSyntax bracketedParameterListSyntax = languageParser.ParseBracketedParameterList();
					if (consumeFullText)
					{
						bracketedParameterListSyntax = languageParser.ConsumeUnexpectedTokens<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.BracketedParameterListSyntax>(bracketedParameterListSyntax);
					}
					bracketedParameterListSyntax2 = (Microsoft.CodeAnalysis.CSharp.Syntax.BracketedParameterListSyntax)bracketedParameterListSyntax.CreateRed();
				}
			}
			return bracketedParameterListSyntax2;
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x000DD50C File Offset: 0x000DB70C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ArgumentListSyntax ParseArgumentList(string text, int offset = 0, [global::System.Runtime.CompilerServices.Nullable(2)] ParseOptions options = null, bool consumeFullText = true)
		{
			Microsoft.CodeAnalysis.CSharp.Syntax.ArgumentListSyntax argumentListSyntax2;
			using (Lexer lexer = SyntaxFactory.MakeLexer(text, offset, (CSharpParseOptions)options))
			{
				using (LanguageParser languageParser = SyntaxFactory.MakeParser(lexer))
				{
					Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ArgumentListSyntax argumentListSyntax = languageParser.ParseParenthesizedArgumentList();
					if (consumeFullText)
					{
						argumentListSyntax = languageParser.ConsumeUnexpectedTokens<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ArgumentListSyntax>(argumentListSyntax);
					}
					argumentListSyntax2 = (Microsoft.CodeAnalysis.CSharp.Syntax.ArgumentListSyntax)argumentListSyntax.CreateRed();
				}
			}
			return argumentListSyntax2;
		}

		// Token: 0x06002910 RID: 10512 RVA: 0x000DD580 File Offset: 0x000DB780
		public static Microsoft.CodeAnalysis.CSharp.Syntax.BracketedArgumentListSyntax ParseBracketedArgumentList(string text, int offset = 0, [global::System.Runtime.CompilerServices.Nullable(2)] ParseOptions options = null, bool consumeFullText = true)
		{
			Microsoft.CodeAnalysis.CSharp.Syntax.BracketedArgumentListSyntax bracketedArgumentListSyntax2;
			using (Lexer lexer = SyntaxFactory.MakeLexer(text, offset, (CSharpParseOptions)options))
			{
				using (LanguageParser languageParser = SyntaxFactory.MakeParser(lexer))
				{
					Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.BracketedArgumentListSyntax bracketedArgumentListSyntax = languageParser.ParseBracketedArgumentList();
					if (consumeFullText)
					{
						bracketedArgumentListSyntax = languageParser.ConsumeUnexpectedTokens<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.BracketedArgumentListSyntax>(bracketedArgumentListSyntax);
					}
					bracketedArgumentListSyntax2 = (Microsoft.CodeAnalysis.CSharp.Syntax.BracketedArgumentListSyntax)bracketedArgumentListSyntax.CreateRed();
				}
			}
			return bracketedArgumentListSyntax2;
		}

		// Token: 0x06002911 RID: 10513 RVA: 0x000DD5F4 File Offset: 0x000DB7F4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AttributeArgumentListSyntax ParseAttributeArgumentList(string text, int offset = 0, [global::System.Runtime.CompilerServices.Nullable(2)] ParseOptions options = null, bool consumeFullText = true)
		{
			Microsoft.CodeAnalysis.CSharp.Syntax.AttributeArgumentListSyntax attributeArgumentListSyntax2;
			using (Lexer lexer = SyntaxFactory.MakeLexer(text, offset, (CSharpParseOptions)options))
			{
				using (LanguageParser languageParser = SyntaxFactory.MakeParser(lexer))
				{
					Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeArgumentListSyntax attributeArgumentListSyntax = languageParser.ParseAttributeArgumentList();
					if (consumeFullText)
					{
						attributeArgumentListSyntax = languageParser.ConsumeUnexpectedTokens<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeArgumentListSyntax>(attributeArgumentListSyntax);
					}
					attributeArgumentListSyntax2 = (Microsoft.CodeAnalysis.CSharp.Syntax.AttributeArgumentListSyntax)attributeArgumentListSyntax.CreateRed();
				}
			}
			return attributeArgumentListSyntax2;
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x000DD668 File Offset: 0x000DB868
		private static SourceText MakeSourceText(string text, int offset)
		{
			return SourceText.From(text, Encoding.UTF8, SourceHashAlgorithm.Sha1).GetSubText(offset);
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x000DD67C File Offset: 0x000DB87C
		private static Lexer MakeLexer(string text, int offset, [global::System.Runtime.CompilerServices.Nullable(2)] CSharpParseOptions options = null)
		{
			return new Lexer(SyntaxFactory.MakeSourceText(text, offset), options ?? CSharpParseOptions.Default, true, false);
		}

		// Token: 0x06002914 RID: 10516 RVA: 0x000DD698 File Offset: 0x000DB898
		private static LanguageParser MakeParser(Lexer lexer)
		{
			return new LanguageParser(lexer, null, null, LexerMode.Syntax, default(CancellationToken));
		}

		// Token: 0x06002915 RID: 10517 RVA: 0x000DD6B7 File Offset: 0x000DB8B7
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static bool AreEquivalent(SyntaxTree oldTree, SyntaxTree newTree, bool topLevel)
		{
			return (oldTree == null && newTree == null) || (oldTree != null && newTree != null && SyntaxEquivalence.AreEquivalent(oldTree, newTree, null, topLevel));
		}

		// Token: 0x06002916 RID: 10518 RVA: 0x000DD6D2 File Offset: 0x000DB8D2
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static bool AreEquivalent(SyntaxNode oldNode, SyntaxNode newNode, bool topLevel)
		{
			return SyntaxEquivalence.AreEquivalent(oldNode, newNode, null, topLevel);
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x000DD6DD File Offset: 0x000DB8DD
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static bool AreEquivalent(SyntaxNode oldNode, SyntaxNode newNode, Func<SyntaxKind, bool> ignoreChildNode = null)
		{
			return SyntaxEquivalence.AreEquivalent(oldNode, newNode, ignoreChildNode, false);
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x000DD6E8 File Offset: 0x000DB8E8
		public static bool AreEquivalent(SyntaxToken oldToken, SyntaxToken newToken)
		{
			return SyntaxEquivalence.AreEquivalent(oldToken, newToken);
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x000DD6F1 File Offset: 0x000DB8F1
		public static bool AreEquivalent(SyntaxTokenList oldList, SyntaxTokenList newList)
		{
			return SyntaxEquivalence.AreEquivalent(oldList, newList);
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x000DD6FA File Offset: 0x000DB8FA
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public static bool AreEquivalent<TNode>([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TNode> oldList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TNode> newList, bool topLevel) where TNode : CSharpSyntaxNode
		{
			return SyntaxEquivalence.AreEquivalent(oldList.Node, newList.Node, null, topLevel);
		}

		// Token: 0x0600291B RID: 10523 RVA: 0x000DD711 File Offset: 0x000DB911
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public static bool AreEquivalent<TNode>([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TNode> oldList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TNode> newList, [global::System.Runtime.CompilerServices.Nullable(2)] Func<SyntaxKind, bool> ignoreChildNode = null) where TNode : SyntaxNode
		{
			return SyntaxEquivalence.AreEquivalent(oldList.Node, newList.Node, ignoreChildNode, false);
		}

		// Token: 0x0600291C RID: 10524 RVA: 0x000DD728 File Offset: 0x000DB928
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public static bool AreEquivalent<TNode>([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<TNode> oldList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<TNode> newList, bool topLevel) where TNode : SyntaxNode
		{
			return SyntaxEquivalence.AreEquivalent(oldList.Node, newList.Node, null, topLevel);
		}

		// Token: 0x0600291D RID: 10525 RVA: 0x000DD73F File Offset: 0x000DB93F
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public static bool AreEquivalent<TNode>([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<TNode> oldList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<TNode> newList, [global::System.Runtime.CompilerServices.Nullable(2)] Func<SyntaxKind, bool> ignoreChildNode = null) where TNode : SyntaxNode
		{
			return SyntaxEquivalence.AreEquivalent(oldList.Node, newList.Node, ignoreChildNode, false);
		}

		// Token: 0x0600291E RID: 10526 RVA: 0x000DD758 File Offset: 0x000DB958
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal static Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax GetStandaloneType(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax node)
		{
			if (node != null)
			{
				Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expressionSyntax = node.Parent as Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax;
				if (expressionSyntax != null && (node.Kind() == SyntaxKind.IdentifierName || node.Kind() == SyntaxKind.GenericName))
				{
					SyntaxKind syntaxKind = expressionSyntax.Kind();
					if (syntaxKind != SyntaxKind.QualifiedName)
					{
						if (syntaxKind == SyntaxKind.AliasQualifiedName)
						{
							Microsoft.CodeAnalysis.CSharp.Syntax.AliasQualifiedNameSyntax aliasQualifiedNameSyntax = (Microsoft.CodeAnalysis.CSharp.Syntax.AliasQualifiedNameSyntax)expressionSyntax;
							if (aliasQualifiedNameSyntax.Name == node)
							{
								return aliasQualifiedNameSyntax;
							}
						}
					}
					else
					{
						Microsoft.CodeAnalysis.CSharp.Syntax.QualifiedNameSyntax qualifiedNameSyntax = (Microsoft.CodeAnalysis.CSharp.Syntax.QualifiedNameSyntax)expressionSyntax;
						if (qualifiedNameSyntax.Right == node)
						{
							return qualifiedNameSyntax;
						}
					}
				}
			}
			return node;
		}

		// Token: 0x0600291F RID: 10527 RVA: 0x000DD7CF File Offset: 0x000DB9CF
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax GetStandaloneExpression(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			return (SyntaxFactory.GetStandaloneNode(expression) as Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax) ?? expression;
		}

		// Token: 0x06002920 RID: 10528 RVA: 0x000DD7E4 File Offset: 0x000DB9E4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal static CSharpSyntaxNode GetStandaloneNode(CSharpSyntaxNode node)
		{
			if (node == null || (!(node is Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax) && !(node is Microsoft.CodeAnalysis.CSharp.Syntax.CrefSyntax)))
			{
				return node;
			}
			SyntaxKind syntaxKind = node.Kind();
			if (syntaxKind <= SyntaxKind.IdentifierName)
			{
				if (syntaxKind - SyntaxKind.NameMemberCref <= 3 || syntaxKind == SyntaxKind.IdentifierName)
				{
					goto IL_0052;
				}
			}
			else if (syntaxKind == SyntaxKind.GenericName || syntaxKind == SyntaxKind.ArrayType || syntaxKind == SyntaxKind.NullableType)
			{
				goto IL_0052;
			}
			return node;
			IL_0052:
			CSharpSyntaxNode parent = node.Parent;
			if (parent == null)
			{
				return node;
			}
			syntaxKind = parent.Kind();
			if (syntaxKind <= SyntaxKind.AliasQualifiedName)
			{
				if (syntaxKind <= SyntaxKind.NameMemberCref)
				{
					if (syntaxKind != SyntaxKind.QualifiedCref)
					{
						if (syntaxKind == SyntaxKind.NameMemberCref)
						{
							if (((Microsoft.CodeAnalysis.CSharp.Syntax.NameMemberCrefSyntax)parent).Name == node)
							{
								CSharpSyntaxNode parent2 = parent.Parent;
								if (parent2 == null || parent2.Kind() != SyntaxKind.QualifiedCref)
								{
									return parent;
								}
								return parent2;
							}
						}
					}
					else if (((Microsoft.CodeAnalysis.CSharp.Syntax.QualifiedCrefSyntax)parent).Member == node)
					{
						return parent;
					}
				}
				else if (syntaxKind != SyntaxKind.QualifiedName)
				{
					if (syntaxKind == SyntaxKind.AliasQualifiedName)
					{
						if (((Microsoft.CodeAnalysis.CSharp.Syntax.AliasQualifiedNameSyntax)parent).Name == node)
						{
							return parent;
						}
					}
				}
				else if (((Microsoft.CodeAnalysis.CSharp.Syntax.QualifiedNameSyntax)parent).Right == node)
				{
					return parent;
				}
			}
			else if (syntaxKind <= SyntaxKind.StackAllocArrayCreationExpression)
			{
				if (syntaxKind != SyntaxKind.NameColon)
				{
					switch (syntaxKind)
					{
					case SyntaxKind.ObjectCreationExpression:
						if (node.Kind() == SyntaxKind.NullableType && ((Microsoft.CodeAnalysis.CSharp.Syntax.ObjectCreationExpressionSyntax)parent).Type == node)
						{
							return parent;
						}
						break;
					case SyntaxKind.ArrayCreationExpression:
						if (((Microsoft.CodeAnalysis.CSharp.Syntax.ArrayCreationExpressionSyntax)parent).Type == node)
						{
							return parent;
						}
						break;
					case SyntaxKind.StackAllocArrayCreationExpression:
						if (((Microsoft.CodeAnalysis.CSharp.Syntax.StackAllocArrayCreationExpressionSyntax)parent).Type == node)
						{
							return parent;
						}
						break;
					}
				}
				else if (parent.Parent.IsKind(SyntaxKind.Subpattern))
				{
					return parent.Parent;
				}
			}
			else if (syntaxKind - SyntaxKind.SimpleMemberAccessExpression > 1)
			{
				if (syntaxKind == SyntaxKind.MemberBindingExpression)
				{
					if (((Microsoft.CodeAnalysis.CSharp.Syntax.MemberBindingExpressionSyntax)parent).Name == node)
					{
						return parent;
					}
				}
			}
			else if (((Microsoft.CodeAnalysis.CSharp.Syntax.MemberAccessExpressionSyntax)parent).Name == node)
			{
				return parent;
			}
			return node;
		}

		// Token: 0x06002921 RID: 10529 RVA: 0x000DD9C4 File Offset: 0x000DBBC4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal static Microsoft.CodeAnalysis.CSharp.Syntax.ConditionalAccessExpressionSyntax FindConditionalAccessNodeForBinding(CSharpSyntaxNode node)
		{
			CSharpSyntaxNode csharpSyntaxNode = node;
			while (csharpSyntaxNode != null)
			{
				csharpSyntaxNode = csharpSyntaxNode.Parent;
				if (csharpSyntaxNode.Kind() == SyntaxKind.ConditionalAccessExpression)
				{
					Microsoft.CodeAnalysis.CSharp.Syntax.ConditionalAccessExpressionSyntax conditionalAccessExpressionSyntax = (Microsoft.CodeAnalysis.CSharp.Syntax.ConditionalAccessExpressionSyntax)csharpSyntaxNode;
					if (conditionalAccessExpressionSyntax.OperatorToken.EndPosition == node.Position)
					{
						return conditionalAccessExpressionSyntax;
					}
				}
			}
			return null;
		}

		// Token: 0x06002922 RID: 10530 RVA: 0x000DDA0C File Offset: 0x000DBC0C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax GetNonGenericExpression(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			if (expression != null)
			{
				SyntaxKind syntaxKind = expression.Kind();
				if (syntaxKind != SyntaxKind.QualifiedName)
				{
					if (syntaxKind != SyntaxKind.AliasQualifiedName)
					{
						if (syntaxKind - SyntaxKind.SimpleMemberAccessExpression <= 1)
						{
							Microsoft.CodeAnalysis.CSharp.Syntax.MemberAccessExpressionSyntax memberAccessExpressionSyntax = (Microsoft.CodeAnalysis.CSharp.Syntax.MemberAccessExpressionSyntax)expression;
							if (memberAccessExpressionSyntax.Name.Kind() == SyntaxKind.GenericName)
							{
								Microsoft.CodeAnalysis.CSharp.Syntax.GenericNameSyntax genericNameSyntax = (Microsoft.CodeAnalysis.CSharp.Syntax.GenericNameSyntax)memberAccessExpressionSyntax.Name;
								return SyntaxFactory.BinaryExpression(expression.Kind(), memberAccessExpressionSyntax.Expression, memberAccessExpressionSyntax.OperatorToken, SyntaxFactory.IdentifierName(genericNameSyntax.Identifier));
							}
						}
					}
					else
					{
						Microsoft.CodeAnalysis.CSharp.Syntax.AliasQualifiedNameSyntax aliasQualifiedNameSyntax = (Microsoft.CodeAnalysis.CSharp.Syntax.AliasQualifiedNameSyntax)expression;
						if (aliasQualifiedNameSyntax.Name.Kind() == SyntaxKind.GenericName)
						{
							Microsoft.CodeAnalysis.CSharp.Syntax.GenericNameSyntax genericNameSyntax2 = (Microsoft.CodeAnalysis.CSharp.Syntax.GenericNameSyntax)aliasQualifiedNameSyntax.Name;
							return SyntaxFactory.AliasQualifiedName(aliasQualifiedNameSyntax.Alias, aliasQualifiedNameSyntax.ColonColonToken, SyntaxFactory.IdentifierName(genericNameSyntax2.Identifier));
						}
					}
				}
				else
				{
					Microsoft.CodeAnalysis.CSharp.Syntax.QualifiedNameSyntax qualifiedNameSyntax = (Microsoft.CodeAnalysis.CSharp.Syntax.QualifiedNameSyntax)expression;
					if (qualifiedNameSyntax.Right.Kind() == SyntaxKind.GenericName)
					{
						Microsoft.CodeAnalysis.CSharp.Syntax.GenericNameSyntax genericNameSyntax3 = (Microsoft.CodeAnalysis.CSharp.Syntax.GenericNameSyntax)qualifiedNameSyntax.Right;
						return SyntaxFactory.QualifiedName(qualifiedNameSyntax.Left, qualifiedNameSyntax.DotToken, SyntaxFactory.IdentifierName(genericNameSyntax3.Identifier));
					}
				}
			}
			return expression;
		}

		// Token: 0x06002923 RID: 10531 RVA: 0x000DDB1C File Offset: 0x000DBD1C
		public static bool IsCompleteSubmission(SyntaxTree tree)
		{
			if (tree == null)
			{
				throw new ArgumentNullException("tree");
			}
			if (tree.Options.Kind != SourceCodeKind.Script)
			{
				throw new ArgumentException(CSharpResources.SyntaxTreeIsNotASubmission);
			}
			if (!tree.HasCompilationUnitRoot)
			{
				return false;
			}
			Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax compilationUnitSyntax = (Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax)tree.GetRoot(default(CancellationToken));
			if (!compilationUnitSyntax.HasErrors)
			{
				return true;
			}
			foreach (Diagnostic diagnostic in compilationUnitSyntax.EndOfFileToken.GetDiagnostics())
			{
				ErrorCode errorCode = (ErrorCode)diagnostic.Code;
				if (errorCode == ErrorCode.ERR_EndifDirectiveExpected || errorCode == ErrorCode.ERR_OpenEndedComment || errorCode == ErrorCode.ERR_EndRegionDirectiveExpected)
				{
					return false;
				}
			}
			SyntaxNode syntaxNode = Enumerable.LastOrDefault<SyntaxNode>(compilationUnitSyntax.ChildNodes());
			if (syntaxNode == null)
			{
				return true;
			}
			if (syntaxNode.HasTrailingTrivia && syntaxNode.ContainsDiagnostics && SyntaxFactory.HasUnterminatedMultiLineComment(syntaxNode.GetTrailingTrivia()))
			{
				return false;
			}
			if (syntaxNode.IsKind(SyntaxKind.IncompleteMember))
			{
				return false;
			}
			if (!syntaxNode.IsKind(SyntaxKind.GlobalStatement))
			{
				return !syntaxNode.GetLastToken(true, true, true, true).IsMissing;
			}
			Microsoft.CodeAnalysis.CSharp.Syntax.GlobalStatementSyntax globalStatementSyntax = (Microsoft.CodeAnalysis.CSharp.Syntax.GlobalStatementSyntax)syntaxNode;
			SyntaxToken syntaxToken = syntaxNode.GetLastToken(true, true, true, true);
			if (syntaxToken.IsMissing)
			{
				if (tree.Options.Kind == SourceCodeKind.Regular || !globalStatementSyntax.Statement.IsKind(SyntaxKind.ExpressionStatement) || !syntaxToken.IsKind(SyntaxKind.SemicolonToken))
				{
					return false;
				}
				syntaxToken = syntaxToken.GetPreviousToken(SyntaxToken.Any, Microsoft.CodeAnalysis.SyntaxTrivia.Any);
				if (syntaxToken.IsMissing)
				{
					return false;
				}
			}
			foreach (Diagnostic diagnostic2 in syntaxToken.GetDiagnostics())
			{
				ErrorCode errorCode = (ErrorCode)diagnostic2.Code;
				if (errorCode <= ErrorCode.ERR_EOFExpected)
				{
					if (errorCode != ErrorCode.ERR_NewlineInConst && errorCode != ErrorCode.ERR_EOFExpected)
					{
						continue;
					}
				}
				else if (errorCode != ErrorCode.ERR_UnterminatedStringLit && errorCode != ErrorCode.ERR_GlobalDefinitionOrStatementExpected)
				{
					continue;
				}
				return false;
			}
			return true;
		}

		// Token: 0x06002924 RID: 10532 RVA: 0x000DDD2C File Offset: 0x000DBF2C
		private static bool HasUnterminatedMultiLineComment(SyntaxTriviaList triviaList)
		{
			foreach (SyntaxTrivia syntaxTrivia in triviaList)
			{
				if (syntaxTrivia.ContainsDiagnostics && syntaxTrivia.Kind() == SyntaxKind.MultiLineCommentTrivia)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002925 RID: 10533 RVA: 0x000DDD6D File Offset: 0x000DBF6D
		public static Microsoft.CodeAnalysis.CSharp.Syntax.CaseSwitchLabelSyntax CaseSwitchLabel(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax value)
		{
			return SyntaxFactory.CaseSwitchLabel(SyntaxFactory.Token(SyntaxKind.CaseKeyword), value, SyntaxFactory.Token(SyntaxKind.ColonToken));
		}

		// Token: 0x06002926 RID: 10534 RVA: 0x000DDD89 File Offset: 0x000DBF89
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DefaultSwitchLabelSyntax DefaultSwitchLabel()
		{
			return SyntaxFactory.DefaultSwitchLabel(SyntaxFactory.Token(SyntaxKind.DefaultKeyword), SyntaxFactory.Token(SyntaxKind.ColonToken));
		}

		// Token: 0x06002927 RID: 10535 RVA: 0x000DDDA4 File Offset: 0x000DBFA4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax Block(params Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax[] statements)
		{
			return SyntaxFactory.Block(SyntaxFactory.List<Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax>(statements));
		}

		// Token: 0x06002928 RID: 10536 RVA: 0x000DDDB1 File Offset: 0x000DBFB1
		public static Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax Block(IEnumerable<Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax> statements)
		{
			return SyntaxFactory.Block(SyntaxFactory.List<Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax>(statements));
		}

		// Token: 0x06002929 RID: 10537 RVA: 0x000DDDBE File Offset: 0x000DBFBE
		public static Microsoft.CodeAnalysis.CSharp.Syntax.PropertyDeclarationSyntax PropertyDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, Microsoft.CodeAnalysis.CSharp.Syntax.ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.AccessorListSyntax accessorList)
		{
			return SyntaxFactory.PropertyDeclaration(attributeLists, modifiers, type, explicitInterfaceSpecifier, identifier, accessorList, null, null);
		}

		// Token: 0x0600292A RID: 10538 RVA: 0x000DDDD0 File Offset: 0x000DBFD0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ConversionOperatorDeclarationSyntax ConversionOperatorDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken implicitOrExplicitKeyword, SyntaxToken operatorKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax body, SyntaxToken semicolonToken)
		{
			return SyntaxFactory.ConversionOperatorDeclaration(attributeLists, modifiers, implicitOrExplicitKeyword, operatorKeyword, type, parameterList, body, null, semicolonToken);
		}

		// Token: 0x0600292B RID: 10539 RVA: 0x000DDDF0 File Offset: 0x000DBFF0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.OperatorDeclarationSyntax OperatorDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax returnType, SyntaxToken operatorKeyword, SyntaxToken operatorToken, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax body, SyntaxToken semicolonToken)
		{
			return SyntaxFactory.OperatorDeclaration(attributeLists, modifiers, returnType, operatorKeyword, operatorToken, parameterList, body, null, semicolonToken);
		}

		// Token: 0x0600292C RID: 10540 RVA: 0x000DDE10 File Offset: 0x000DC010
		public static Microsoft.CodeAnalysis.CSharp.Syntax.UsingDirectiveSyntax UsingDirective(Microsoft.CodeAnalysis.CSharp.Syntax.NameEqualsSyntax alias, Microsoft.CodeAnalysis.CSharp.Syntax.NameSyntax name)
		{
			return SyntaxFactory.UsingDirective(SyntaxFactory.Token(SyntaxKind.UsingKeyword), default(SyntaxToken), alias, name, SyntaxFactory.Token(SyntaxKind.SemicolonToken));
		}

		// Token: 0x0600292D RID: 10541 RVA: 0x000DDE44 File Offset: 0x000DC044
		public static Microsoft.CodeAnalysis.CSharp.Syntax.UsingDirectiveSyntax UsingDirective(SyntaxToken usingKeyword, SyntaxToken staticKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.NameEqualsSyntax alias, Microsoft.CodeAnalysis.CSharp.Syntax.NameSyntax name, SyntaxToken semicolonToken)
		{
			return SyntaxFactory.UsingDirective(default(SyntaxToken), usingKeyword, staticKeyword, alias, name, semicolonToken);
		}

		// Token: 0x0600292E RID: 10542 RVA: 0x000DDE68 File Offset: 0x000DC068
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ClassOrStructConstraintSyntax ClassOrStructConstraint(SyntaxKind kind, SyntaxToken classOrStructKeyword)
		{
			return SyntaxFactory.ClassOrStructConstraint(kind, classOrStructKeyword, default(SyntaxToken));
		}

		// Token: 0x0600292F RID: 10543 RVA: 0x000DDE85 File Offset: 0x000DC085
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AccessorDeclarationSyntax AccessorDeclaration(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax body)
		{
			return SyntaxFactory.AccessorDeclaration(kind, attributeLists, modifiers, body, null);
		}

		// Token: 0x06002930 RID: 10544 RVA: 0x000DDE91 File Offset: 0x000DC091
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AccessorDeclarationSyntax AccessorDeclaration(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax body, SyntaxToken semicolonToken)
		{
			return SyntaxFactory.AccessorDeclaration(kind, attributeLists, modifiers, keyword, body, null, semicolonToken);
		}

		// Token: 0x06002931 RID: 10545 RVA: 0x000DDEA1 File Offset: 0x000DC0A1
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AccessorDeclarationSyntax AccessorDeclaration(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.ArrowExpressionClauseSyntax expressionBody)
		{
			return SyntaxFactory.AccessorDeclaration(kind, attributeLists, modifiers, null, expressionBody);
		}

		// Token: 0x06002932 RID: 10546 RVA: 0x000DDEAD File Offset: 0x000DC0AD
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AccessorDeclarationSyntax AccessorDeclaration(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, Microsoft.CodeAnalysis.CSharp.Syntax.ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
		{
			return SyntaxFactory.AccessorDeclaration(kind, attributeLists, modifiers, keyword, null, expressionBody, semicolonToken);
		}

		// Token: 0x06002933 RID: 10547 RVA: 0x000DDEC0 File Offset: 0x000DC0C0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.EnumMemberDeclarationSyntax EnumMemberDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.EqualsValueClauseSyntax equalsValue)
		{
			return SyntaxFactory.EnumMemberDeclaration(attributeLists, default(SyntaxTokenList), identifier, equalsValue);
		}

		// Token: 0x06002934 RID: 10548 RVA: 0x000DDEE0 File Offset: 0x000DC0E0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.NamespaceDeclarationSyntax NamespaceDeclaration(Microsoft.CodeAnalysis.CSharp.Syntax.NameSyntax name, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ExternAliasDirectiveSyntax> externs, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.UsingDirectiveSyntax> usings, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax> members)
		{
			return SyntaxFactory.NamespaceDeclaration(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), name, externs, usings, members);
		}

		// Token: 0x06002935 RID: 10549 RVA: 0x000DDF08 File Offset: 0x000DC108
		public static Microsoft.CodeAnalysis.CSharp.Syntax.NamespaceDeclarationSyntax NamespaceDeclaration(SyntaxToken namespaceKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.NameSyntax name, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ExternAliasDirectiveSyntax> externs, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.UsingDirectiveSyntax> usings, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
		{
			return SyntaxFactory.NamespaceDeclaration(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), namespaceKeyword, name, openBraceToken, externs, usings, members, closeBraceToken, semicolonToken);
		}

		// Token: 0x06002936 RID: 10550 RVA: 0x000DDF38 File Offset: 0x000DC138
		public static Microsoft.CodeAnalysis.CSharp.Syntax.EventDeclarationSyntax EventDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken eventKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, Microsoft.CodeAnalysis.CSharp.Syntax.ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.AccessorListSyntax accessorList)
		{
			return SyntaxFactory.EventDeclaration(attributeLists, modifiers, eventKeyword, type, explicitInterfaceSpecifier, identifier, accessorList, default(SyntaxToken));
		}

		// Token: 0x06002937 RID: 10551 RVA: 0x000DDF5D File Offset: 0x000DC15D
		public static Microsoft.CodeAnalysis.CSharp.Syntax.EventDeclarationSyntax EventDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken eventKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, Microsoft.CodeAnalysis.CSharp.Syntax.ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, SyntaxToken semicolonToken)
		{
			return SyntaxFactory.EventDeclaration(attributeLists, modifiers, eventKeyword, type, explicitInterfaceSpecifier, identifier, null, semicolonToken);
		}

		// Token: 0x06002938 RID: 10552 RVA: 0x000DDF70 File Offset: 0x000DC170
		public static Microsoft.CodeAnalysis.CSharp.Syntax.SwitchStatementSyntax SwitchStatement(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.SwitchSectionSyntax> sections)
		{
			bool flag = !(expression is Microsoft.CodeAnalysis.CSharp.Syntax.TupleExpressionSyntax);
			SyntaxToken syntaxToken = (flag ? SyntaxFactory.Token(SyntaxKind.OpenParenToken) : default(SyntaxToken));
			SyntaxToken syntaxToken2 = (flag ? SyntaxFactory.Token(SyntaxKind.CloseParenToken) : default(SyntaxToken));
			return SyntaxFactory.SwitchStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), SyntaxFactory.Token(SyntaxKind.SwitchKeyword), syntaxToken, expression, syntaxToken2, SyntaxFactory.Token(SyntaxKind.OpenBraceToken), sections, SyntaxFactory.Token(SyntaxKind.CloseBraceToken));
		}

		// Token: 0x06002939 RID: 10553 RVA: 0x000DDFEC File Offset: 0x000DC1EC
		public static Microsoft.CodeAnalysis.CSharp.Syntax.SwitchStatementSyntax SwitchStatement(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			return SyntaxFactory.SwitchStatement(expression, default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.SwitchSectionSyntax>));
		}

		// Token: 0x0600293A RID: 10554 RVA: 0x000DE008 File Offset: 0x000DC208
		public static Microsoft.CodeAnalysis.CSharp.Syntax.SimpleLambdaExpressionSyntax SimpleLambdaExpression(Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax parameter, CSharpSyntaxNode body)
		{
			Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax blockSyntax = body as Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax;
			if (blockSyntax == null)
			{
				return SyntaxFactory.SimpleLambdaExpression(parameter, null, (Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax)body);
			}
			return SyntaxFactory.SimpleLambdaExpression(parameter, blockSyntax, null);
		}

		// Token: 0x0600293B RID: 10555 RVA: 0x000DE038 File Offset: 0x000DC238
		public static Microsoft.CodeAnalysis.CSharp.Syntax.SimpleLambdaExpressionSyntax SimpleLambdaExpression(SyntaxToken asyncKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax parameter, SyntaxToken arrowToken, CSharpSyntaxNode body)
		{
			Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax blockSyntax = body as Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax;
			if (blockSyntax == null)
			{
				return SyntaxFactory.SimpleLambdaExpression(asyncKeyword, parameter, arrowToken, null, (Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax)body);
			}
			return SyntaxFactory.SimpleLambdaExpression(asyncKeyword, parameter, arrowToken, blockSyntax, null);
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x000DE06C File Offset: 0x000DC26C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ParenthesizedLambdaExpressionSyntax ParenthesizedLambdaExpression(CSharpSyntaxNode body)
		{
			return SyntaxFactory.ParenthesizedLambdaExpression(SyntaxFactory.ParameterList(default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax>)), body);
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x000DE090 File Offset: 0x000DC290
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ParenthesizedLambdaExpressionSyntax ParenthesizedLambdaExpression(Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, CSharpSyntaxNode body)
		{
			Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax blockSyntax = body as Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax;
			if (blockSyntax == null)
			{
				return SyntaxFactory.ParenthesizedLambdaExpression(parameterList, null, (Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax)body);
			}
			return SyntaxFactory.ParenthesizedLambdaExpression(parameterList, blockSyntax, null);
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x000DE0C0 File Offset: 0x000DC2C0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ParenthesizedLambdaExpressionSyntax ParenthesizedLambdaExpression(SyntaxToken asyncKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, SyntaxToken arrowToken, CSharpSyntaxNode body)
		{
			Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax blockSyntax = body as Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax;
			if (blockSyntax == null)
			{
				return SyntaxFactory.ParenthesizedLambdaExpression(asyncKeyword, parameterList, arrowToken, null, (Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax)body);
			}
			return SyntaxFactory.ParenthesizedLambdaExpression(asyncKeyword, parameterList, arrowToken, blockSyntax, null);
		}

		// Token: 0x0600293F RID: 10559 RVA: 0x000DE0F1 File Offset: 0x000DC2F1
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AnonymousMethodExpressionSyntax AnonymousMethodExpression(CSharpSyntaxNode body)
		{
			return SyntaxFactory.AnonymousMethodExpression(null, body);
		}

		// Token: 0x06002940 RID: 10560 RVA: 0x000DE0FC File Offset: 0x000DC2FC
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AnonymousMethodExpressionSyntax AnonymousMethodExpression([global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, CSharpSyntaxNode body)
		{
			Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax blockSyntax = body as Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax;
			if (blockSyntax == null)
			{
				throw new ArgumentException("body");
			}
			return SyntaxFactory.AnonymousMethodExpression(default(SyntaxTokenList), SyntaxFactory.Token(SyntaxKind.DelegateKeyword), parameterList, blockSyntax, null);
		}

		// Token: 0x06002941 RID: 10561 RVA: 0x000DE13C File Offset: 0x000DC33C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AnonymousMethodExpressionSyntax AnonymousMethodExpression(SyntaxToken asyncKeyword, SyntaxToken delegateKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, CSharpSyntaxNode body)
		{
			Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax blockSyntax = body as Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax;
			if (blockSyntax == null)
			{
				throw new ArgumentException("body");
			}
			return SyntaxFactory.AnonymousMethodExpression(asyncKeyword, delegateKeyword, parameterList, blockSyntax, null);
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x000DE168 File Offset: 0x000DC368
		[Obsolete("The diagnosticOptions parameter is obsolete due to performance problems, if you are passing non-null use CompilationOptions.SyntaxTreeOptionsProvider instead", false)]
		[EditorBrowsable(1)]
		public static SyntaxTree ParseSyntaxTree(string text, [global::System.Runtime.CompilerServices.Nullable(2)] ParseOptions options, string path, [global::System.Runtime.CompilerServices.Nullable(2)] Encoding encoding, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ImmutableDictionary<string, ReportDiagnostic> diagnosticOptions, CancellationToken cancellationToken)
		{
			return SyntaxFactory.ParseSyntaxTree(SourceText.From(text, encoding, SourceHashAlgorithm.Sha1), options, path, diagnosticOptions, default(bool?), cancellationToken);
		}

		// Token: 0x06002943 RID: 10563 RVA: 0x000DE194 File Offset: 0x000DC394
		[Obsolete("The diagnosticOptions parameter is obsolete due to performance problems, if you are passing non-null use CompilationOptions.SyntaxTreeOptionsProvider instead", false)]
		[EditorBrowsable(1)]
		public static SyntaxTree ParseSyntaxTree(SourceText text, [global::System.Runtime.CompilerServices.Nullable(2)] ParseOptions options, string path, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ImmutableDictionary<string, ReportDiagnostic> diagnosticOptions, CancellationToken cancellationToken)
		{
			return CSharpSyntaxTree.ParseText(text, (CSharpParseOptions)options, path, diagnosticOptions, default(bool?), cancellationToken);
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x000DE1BA File Offset: 0x000DC3BA
		[EditorBrowsable(1)]
		[Obsolete("The diagnosticOptions and isGeneratedCode parameters are obsolete due to performance problems, if you are using them use CompilationOptions.SyntaxTreeOptionsProvider instead", false)]
		public static SyntaxTree ParseSyntaxTree(string text, [global::System.Runtime.CompilerServices.Nullable(2)] ParseOptions options, string path, [global::System.Runtime.CompilerServices.Nullable(2)] Encoding encoding, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ImmutableDictionary<string, ReportDiagnostic> diagnosticOptions, bool? isGeneratedCode, CancellationToken cancellationToken)
		{
			return SyntaxFactory.ParseSyntaxTree(SourceText.From(text, encoding, SourceHashAlgorithm.Sha1), options, path, diagnosticOptions, isGeneratedCode, cancellationToken);
		}

		// Token: 0x06002945 RID: 10565 RVA: 0x000DE1D1 File Offset: 0x000DC3D1
		[EditorBrowsable(1)]
		[Obsolete("The diagnosticOptions and isGeneratedCode parameters are obsolete due to performance problems, if you are using them use CompilationOptions.SyntaxTreeOptionsProvider instead", false)]
		public static SyntaxTree ParseSyntaxTree(SourceText text, [global::System.Runtime.CompilerServices.Nullable(2)] ParseOptions options, string path, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ImmutableDictionary<string, ReportDiagnostic> diagnosticOptions, bool? isGeneratedCode, CancellationToken cancellationToken)
		{
			return CSharpSyntaxTree.ParseText(text, (CSharpParseOptions)options, path, diagnosticOptions, isGeneratedCode, cancellationToken);
		}

		// Token: 0x06002946 RID: 10566 RVA: 0x000DE1E8 File Offset: 0x000DC3E8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ThrowStatementSyntax ThrowStatement(SyntaxToken throwKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SyntaxToken semicolonToken)
		{
			return SyntaxFactory.ThrowStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), throwKeyword, expression, semicolonToken);
		}

		// Token: 0x06002947 RID: 10567 RVA: 0x000DE208 File Offset: 0x000DC408
		public static Microsoft.CodeAnalysis.CSharp.Syntax.TryStatementSyntax TryStatement(Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.CatchClauseSyntax> catches, Microsoft.CodeAnalysis.CSharp.Syntax.FinallyClauseSyntax @finally)
		{
			return SyntaxFactory.TryStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), block, catches, @finally);
		}

		// Token: 0x06002948 RID: 10568 RVA: 0x000DE228 File Offset: 0x000DC428
		public static Microsoft.CodeAnalysis.CSharp.Syntax.TryStatementSyntax TryStatement(SyntaxToken tryKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.CatchClauseSyntax> catches, Microsoft.CodeAnalysis.CSharp.Syntax.FinallyClauseSyntax @finally)
		{
			return SyntaxFactory.TryStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), tryKeyword, block, catches, @finally);
		}

		// Token: 0x06002949 RID: 10569 RVA: 0x000DE247 File Offset: 0x000DC447
		internal static SyntaxKind GetTypeDeclarationKeywordKind(DeclarationKind kind)
		{
			switch (kind)
			{
			case DeclarationKind.Class:
				return SyntaxKind.ClassKeyword;
			case DeclarationKind.Interface:
				return SyntaxKind.InterfaceKeyword;
			case DeclarationKind.Struct:
				return SyntaxKind.StructKeyword;
			default:
				throw ExceptionUtilities.UnexpectedValue(kind);
			}
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x000DE27C File Offset: 0x000DC47C
		private static SyntaxKind GetTypeDeclarationKeywordKind(SyntaxKind kind)
		{
			switch (kind)
			{
			case SyntaxKind.ClassDeclaration:
				return SyntaxKind.ClassKeyword;
			case SyntaxKind.StructDeclaration:
				return SyntaxKind.StructKeyword;
			case SyntaxKind.InterfaceDeclaration:
				return SyntaxKind.InterfaceKeyword;
			default:
				if (kind != SyntaxKind.RecordDeclaration && kind != SyntaxKind.RecordStructDeclaration)
				{
					throw ExceptionUtilities.UnexpectedValue(kind);
				}
				return SyntaxKind.RecordKeyword;
			}
		}

		// Token: 0x0600294B RID: 10571 RVA: 0x000DE2D8 File Offset: 0x000DC4D8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.TypeDeclarationSyntax TypeDeclaration(SyntaxKind kind, SyntaxToken identifier)
		{
			return SyntaxFactory.TypeDeclaration(kind, default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), SyntaxFactory.Token(SyntaxFactory.GetTypeDeclarationKeywordKind(kind)), identifier, null, null, default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax>), SyntaxFactory.Token(SyntaxKind.OpenBraceToken), default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax>), SyntaxFactory.Token(SyntaxKind.CloseBraceToken), default(SyntaxToken));
		}

		// Token: 0x0600294C RID: 10572 RVA: 0x000DE33B File Offset: 0x000DC53B
		public static Microsoft.CodeAnalysis.CSharp.Syntax.TypeDeclarationSyntax TypeDeclaration(SyntaxKind kind, string identifier)
		{
			return SyntaxFactory.TypeDeclaration(kind, SyntaxFactory.Identifier(identifier));
		}

		// Token: 0x0600294D RID: 10573 RVA: 0x000DE34C File Offset: 0x000DC54C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public static Microsoft.CodeAnalysis.CSharp.Syntax.TypeDeclarationSyntax TypeDeclaration(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributes, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterListSyntax typeParameterList, Microsoft.CodeAnalysis.CSharp.Syntax.BaseListSyntax baseList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
		{
			switch (kind)
			{
			case SyntaxKind.ClassDeclaration:
				return SyntaxFactory.ClassDeclaration(attributes, modifiers, keyword, identifier, typeParameterList, baseList, constraintClauses, openBraceToken, members, closeBraceToken, semicolonToken);
			case SyntaxKind.StructDeclaration:
				return SyntaxFactory.StructDeclaration(attributes, modifiers, keyword, identifier, typeParameterList, baseList, constraintClauses, openBraceToken, members, closeBraceToken, semicolonToken);
			case SyntaxKind.InterfaceDeclaration:
				return SyntaxFactory.InterfaceDeclaration(attributes, modifiers, keyword, identifier, typeParameterList, baseList, constraintClauses, openBraceToken, members, closeBraceToken, semicolonToken);
			default:
				if (kind == SyntaxKind.RecordDeclaration)
				{
					return SyntaxFactory.RecordDeclaration(SyntaxKind.RecordDeclaration, attributes, modifiers, keyword, default(SyntaxToken), identifier, typeParameterList, null, baseList, constraintClauses, openBraceToken, members, closeBraceToken, semicolonToken);
				}
				if (kind != SyntaxKind.RecordStructDeclaration)
				{
					throw ExceptionUtilities.UnexpectedValue(kind);
				}
				return SyntaxFactory.RecordDeclaration(SyntaxKind.RecordStructDeclaration, attributes, modifiers, keyword, SyntaxFactory.Token(SyntaxKind.StructKeyword), identifier, typeParameterList, null, baseList, constraintClauses, openBraceToken, members, closeBraceToken, semicolonToken);
			}
		}

		// Token: 0x0600294E RID: 10574 RVA: 0x000DE430 File Offset: 0x000DC630
		public static Microsoft.CodeAnalysis.CSharp.Syntax.UnsafeStatementSyntax UnsafeStatement(SyntaxToken unsafeKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax block)
		{
			return SyntaxFactory.UnsafeStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), unsafeKeyword, block);
		}

		// Token: 0x0600294F RID: 10575 RVA: 0x000DE450 File Offset: 0x000DC650
		public static Microsoft.CodeAnalysis.CSharp.Syntax.UsingStatementSyntax UsingStatement(SyntaxToken usingKeyword, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SyntaxToken closeParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			return SyntaxFactory.UsingStatement(default(SyntaxToken), usingKeyword, openParenToken, declaration, expression, closeParenToken, statement);
		}

		// Token: 0x06002950 RID: 10576 RVA: 0x000DE474 File Offset: 0x000DC674
		public static Microsoft.CodeAnalysis.CSharp.Syntax.UsingStatementSyntax UsingStatement(SyntaxToken awaitKeyword, SyntaxToken usingKeyword, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SyntaxToken closeParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			return SyntaxFactory.UsingStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), awaitKeyword, usingKeyword, openParenToken, declaration, expression, closeParenToken, statement);
		}

		// Token: 0x06002951 RID: 10577 RVA: 0x000DE49C File Offset: 0x000DC69C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.UsingStatementSyntax UsingStatement([global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			return SyntaxFactory.UsingStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), declaration, expression, statement);
		}

		// Token: 0x06002952 RID: 10578 RVA: 0x000DE4BC File Offset: 0x000DC6BC
		public static Microsoft.CodeAnalysis.CSharp.Syntax.WhileStatementSyntax WhileStatement(SyntaxToken whileKeyword, SyntaxToken openParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax condition, SyntaxToken closeParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			return SyntaxFactory.WhileStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), whileKeyword, openParenToken, condition, closeParenToken, statement);
		}

		// Token: 0x06002953 RID: 10579 RVA: 0x000DE4E0 File Offset: 0x000DC6E0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.YieldStatementSyntax YieldStatement(SyntaxKind kind, SyntaxToken yieldKeyword, SyntaxToken returnOrBreakKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SyntaxToken semicolonToken)
		{
			return SyntaxFactory.YieldStatement(kind, default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), yieldKeyword, returnOrBreakKeyword, expression, semicolonToken);
		}

		// Token: 0x06002954 RID: 10580 RVA: 0x000DE504 File Offset: 0x000DC704
		public static Microsoft.CodeAnalysis.CSharp.Syntax.IdentifierNameSyntax IdentifierName(SyntaxToken identifier)
		{
			SyntaxKind syntaxKind = identifier.Kind();
			if (syntaxKind != SyntaxKind.GlobalKeyword && syntaxKind != SyntaxKind.IdentifierToken)
			{
				throw new ArgumentException("identifier");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.IdentifierNameSyntax)SyntaxFactory.IdentifierName((SyntaxToken)identifier.Node).CreateRed();
		}

		// Token: 0x06002955 RID: 10581 RVA: 0x000DE550 File Offset: 0x000DC750
		public static Microsoft.CodeAnalysis.CSharp.Syntax.QualifiedNameSyntax QualifiedName(Microsoft.CodeAnalysis.CSharp.Syntax.NameSyntax left, SyntaxToken dotToken, Microsoft.CodeAnalysis.CSharp.Syntax.SimpleNameSyntax right)
		{
			if (left == null)
			{
				throw new ArgumentNullException("left");
			}
			if (dotToken.Kind() != SyntaxKind.DotToken)
			{
				throw new ArgumentException("dotToken");
			}
			if (right == null)
			{
				throw new ArgumentNullException("right");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.QualifiedNameSyntax)SyntaxFactory.QualifiedName((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.NameSyntax)left.Green, (SyntaxToken)dotToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.SimpleNameSyntax)right.Green).CreateRed();
		}

		// Token: 0x06002956 RID: 10582 RVA: 0x000DE5C2 File Offset: 0x000DC7C2
		public static Microsoft.CodeAnalysis.CSharp.Syntax.QualifiedNameSyntax QualifiedName(Microsoft.CodeAnalysis.CSharp.Syntax.NameSyntax left, Microsoft.CodeAnalysis.CSharp.Syntax.SimpleNameSyntax right)
		{
			return SyntaxFactory.QualifiedName(left, SyntaxFactory.Token(SyntaxKind.DotToken), right);
		}

		// Token: 0x06002957 RID: 10583 RVA: 0x000DE5D8 File Offset: 0x000DC7D8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.GenericNameSyntax GenericName(SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.TypeArgumentListSyntax typeArgumentList)
		{
			if (identifier.Kind() != SyntaxKind.IdentifierToken)
			{
				throw new ArgumentException("identifier");
			}
			if (typeArgumentList == null)
			{
				throw new ArgumentNullException("typeArgumentList");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.GenericNameSyntax)SyntaxFactory.GenericName((SyntaxToken)identifier.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeArgumentListSyntax)typeArgumentList.Green).CreateRed();
		}

		// Token: 0x06002958 RID: 10584 RVA: 0x000DE634 File Offset: 0x000DC834
		public static Microsoft.CodeAnalysis.CSharp.Syntax.GenericNameSyntax GenericName(SyntaxToken identifier)
		{
			return SyntaxFactory.GenericName(identifier, SyntaxFactory.TypeArgumentList(default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax>)));
		}

		// Token: 0x06002959 RID: 10585 RVA: 0x000DE658 File Offset: 0x000DC858
		public static Microsoft.CodeAnalysis.CSharp.Syntax.GenericNameSyntax GenericName(string identifier)
		{
			return SyntaxFactory.GenericName(SyntaxFactory.Identifier(identifier), SyntaxFactory.TypeArgumentList(default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax>)));
		}

		// Token: 0x0600295A RID: 10586 RVA: 0x000DE680 File Offset: 0x000DC880
		public static Microsoft.CodeAnalysis.CSharp.Syntax.TypeArgumentListSyntax TypeArgumentList(SyntaxToken lessThanToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax> arguments, SyntaxToken greaterThanToken)
		{
			if (lessThanToken.Kind() != SyntaxKind.LessThanToken)
			{
				throw new ArgumentException("lessThanToken");
			}
			if (greaterThanToken.Kind() != SyntaxKind.GreaterThanToken)
			{
				throw new ArgumentException("greaterThanToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.TypeArgumentListSyntax)SyntaxFactory.TypeArgumentList((SyntaxToken)lessThanToken.Node, arguments.Node.ToGreenSeparatedList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax>(), (SyntaxToken)greaterThanToken.Node).CreateRed();
		}

		// Token: 0x0600295B RID: 10587 RVA: 0x000DE6F0 File Offset: 0x000DC8F0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.TypeArgumentListSyntax TypeArgumentList([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax> arguments = default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax>))
		{
			return SyntaxFactory.TypeArgumentList(SyntaxFactory.Token(SyntaxKind.LessThanToken), arguments, SyntaxFactory.Token(SyntaxKind.GreaterThanToken));
		}

		// Token: 0x0600295C RID: 10588 RVA: 0x000DE70C File Offset: 0x000DC90C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AliasQualifiedNameSyntax AliasQualifiedName(Microsoft.CodeAnalysis.CSharp.Syntax.IdentifierNameSyntax alias, SyntaxToken colonColonToken, Microsoft.CodeAnalysis.CSharp.Syntax.SimpleNameSyntax name)
		{
			if (alias == null)
			{
				throw new ArgumentNullException("alias");
			}
			if (colonColonToken.Kind() != SyntaxKind.ColonColonToken)
			{
				throw new ArgumentException("colonColonToken");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.AliasQualifiedNameSyntax)SyntaxFactory.AliasQualifiedName((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.IdentifierNameSyntax)alias.Green, (SyntaxToken)colonColonToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.SimpleNameSyntax)name.Green).CreateRed();
		}

		// Token: 0x0600295D RID: 10589 RVA: 0x000DE77E File Offset: 0x000DC97E
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AliasQualifiedNameSyntax AliasQualifiedName(Microsoft.CodeAnalysis.CSharp.Syntax.IdentifierNameSyntax alias, Microsoft.CodeAnalysis.CSharp.Syntax.SimpleNameSyntax name)
		{
			return SyntaxFactory.AliasQualifiedName(alias, SyntaxFactory.Token(SyntaxKind.ColonColonToken), name);
		}

		// Token: 0x0600295E RID: 10590 RVA: 0x000DE791 File Offset: 0x000DC991
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AliasQualifiedNameSyntax AliasQualifiedName(string alias, Microsoft.CodeAnalysis.CSharp.Syntax.SimpleNameSyntax name)
		{
			return SyntaxFactory.AliasQualifiedName(SyntaxFactory.IdentifierName(alias), SyntaxFactory.Token(SyntaxKind.ColonColonToken), name);
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x000DE7AC File Offset: 0x000DC9AC
		public static Microsoft.CodeAnalysis.CSharp.Syntax.PredefinedTypeSyntax PredefinedType(SyntaxToken keyword)
		{
			SyntaxKind syntaxKind = keyword.Kind();
			if (syntaxKind - SyntaxKind.BoolKeyword > 15)
			{
				throw new ArgumentException("keyword");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.PredefinedTypeSyntax)SyntaxFactory.PredefinedType((SyntaxToken)keyword.Node).CreateRed();
		}

		// Token: 0x06002960 RID: 10592 RVA: 0x000DE7F1 File Offset: 0x000DC9F1
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ArrayTypeSyntax ArrayType(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax elementType, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ArrayRankSpecifierSyntax> rankSpecifiers)
		{
			if (elementType == null)
			{
				throw new ArgumentNullException("elementType");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ArrayTypeSyntax)SyntaxFactory.ArrayType((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)elementType.Green, rankSpecifiers.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ArrayRankSpecifierSyntax>()).CreateRed();
		}

		// Token: 0x06002961 RID: 10593 RVA: 0x000DE828 File Offset: 0x000DCA28
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ArrayTypeSyntax ArrayType(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax elementType)
		{
			return SyntaxFactory.ArrayType(elementType, default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ArrayRankSpecifierSyntax>));
		}

		// Token: 0x06002962 RID: 10594 RVA: 0x000DE844 File Offset: 0x000DCA44
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ArrayRankSpecifierSyntax ArrayRankSpecifier(SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax> sizes, SyntaxToken closeBracketToken)
		{
			if (openBracketToken.Kind() != SyntaxKind.OpenBracketToken)
			{
				throw new ArgumentException("openBracketToken");
			}
			if (closeBracketToken.Kind() != SyntaxKind.CloseBracketToken)
			{
				throw new ArgumentException("closeBracketToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ArrayRankSpecifierSyntax)SyntaxFactory.ArrayRankSpecifier((SyntaxToken)openBracketToken.Node, sizes.Node.ToGreenSeparatedList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax>(), (SyntaxToken)closeBracketToken.Node).CreateRed();
		}

		// Token: 0x06002963 RID: 10595 RVA: 0x000DE8B4 File Offset: 0x000DCAB4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ArrayRankSpecifierSyntax ArrayRankSpecifier([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax> sizes = default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax>))
		{
			return SyntaxFactory.ArrayRankSpecifier(SyntaxFactory.Token(SyntaxKind.OpenBracketToken), sizes, SyntaxFactory.Token(SyntaxKind.CloseBracketToken));
		}

		// Token: 0x06002964 RID: 10596 RVA: 0x000DE8D0 File Offset: 0x000DCAD0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.PointerTypeSyntax PointerType(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax elementType, SyntaxToken asteriskToken)
		{
			if (elementType == null)
			{
				throw new ArgumentNullException("elementType");
			}
			if (asteriskToken.Kind() != SyntaxKind.AsteriskToken)
			{
				throw new ArgumentException("asteriskToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.PointerTypeSyntax)SyntaxFactory.PointerType((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)elementType.Green, (SyntaxToken)asteriskToken.Node).CreateRed();
		}

		// Token: 0x06002965 RID: 10597 RVA: 0x000DE929 File Offset: 0x000DCB29
		public static Microsoft.CodeAnalysis.CSharp.Syntax.PointerTypeSyntax PointerType(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax elementType)
		{
			return SyntaxFactory.PointerType(elementType, SyntaxFactory.Token(SyntaxKind.AsteriskToken));
		}

		// Token: 0x06002966 RID: 10598 RVA: 0x000DE93C File Offset: 0x000DCB3C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.FunctionPointerTypeSyntax FunctionPointerType(SyntaxToken delegateKeyword, SyntaxToken asteriskToken, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.FunctionPointerCallingConventionSyntax callingConvention, Microsoft.CodeAnalysis.CSharp.Syntax.FunctionPointerParameterListSyntax parameterList)
		{
			if (delegateKeyword.Kind() != SyntaxKind.DelegateKeyword)
			{
				throw new ArgumentException("delegateKeyword");
			}
			if (asteriskToken.Kind() != SyntaxKind.AsteriskToken)
			{
				throw new ArgumentException("asteriskToken");
			}
			if (parameterList == null)
			{
				throw new ArgumentNullException("parameterList");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.FunctionPointerTypeSyntax)SyntaxFactory.FunctionPointerType((SyntaxToken)delegateKeyword.Node, (SyntaxToken)asteriskToken.Node, (callingConvention == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.FunctionPointerCallingConventionSyntax)callingConvention.Green), (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.FunctionPointerParameterListSyntax)parameterList.Green).CreateRed();
		}

		// Token: 0x06002967 RID: 10599 RVA: 0x000DE9CA File Offset: 0x000DCBCA
		public static Microsoft.CodeAnalysis.CSharp.Syntax.FunctionPointerTypeSyntax FunctionPointerType([global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.FunctionPointerCallingConventionSyntax callingConvention, Microsoft.CodeAnalysis.CSharp.Syntax.FunctionPointerParameterListSyntax parameterList)
		{
			return SyntaxFactory.FunctionPointerType(SyntaxFactory.Token(SyntaxKind.DelegateKeyword), SyntaxFactory.Token(SyntaxKind.AsteriskToken), callingConvention, parameterList);
		}

		// Token: 0x06002968 RID: 10600 RVA: 0x000DE9E8 File Offset: 0x000DCBE8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.FunctionPointerTypeSyntax FunctionPointerType()
		{
			return SyntaxFactory.FunctionPointerType(SyntaxFactory.Token(SyntaxKind.DelegateKeyword), SyntaxFactory.Token(SyntaxKind.AsteriskToken), null, SyntaxFactory.FunctionPointerParameterList(default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.FunctionPointerParameterSyntax>)));
		}

		// Token: 0x06002969 RID: 10601 RVA: 0x000DEA20 File Offset: 0x000DCC20
		public static Microsoft.CodeAnalysis.CSharp.Syntax.FunctionPointerParameterListSyntax FunctionPointerParameterList(SyntaxToken lessThanToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.FunctionPointerParameterSyntax> parameters, SyntaxToken greaterThanToken)
		{
			if (lessThanToken.Kind() != SyntaxKind.LessThanToken)
			{
				throw new ArgumentException("lessThanToken");
			}
			if (greaterThanToken.Kind() != SyntaxKind.GreaterThanToken)
			{
				throw new ArgumentException("greaterThanToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.FunctionPointerParameterListSyntax)SyntaxFactory.FunctionPointerParameterList((SyntaxToken)lessThanToken.Node, parameters.Node.ToGreenSeparatedList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.FunctionPointerParameterSyntax>(), (SyntaxToken)greaterThanToken.Node).CreateRed();
		}

		// Token: 0x0600296A RID: 10602 RVA: 0x000DEA90 File Offset: 0x000DCC90
		public static Microsoft.CodeAnalysis.CSharp.Syntax.FunctionPointerParameterListSyntax FunctionPointerParameterList([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.FunctionPointerParameterSyntax> parameters = default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.FunctionPointerParameterSyntax>))
		{
			return SyntaxFactory.FunctionPointerParameterList(SyntaxFactory.Token(SyntaxKind.LessThanToken), parameters, SyntaxFactory.Token(SyntaxKind.GreaterThanToken));
		}

		// Token: 0x0600296B RID: 10603 RVA: 0x000DEAAC File Offset: 0x000DCCAC
		public static Microsoft.CodeAnalysis.CSharp.Syntax.FunctionPointerCallingConventionSyntax FunctionPointerCallingConvention(SyntaxToken managedOrUnmanagedKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.FunctionPointerUnmanagedCallingConventionListSyntax unmanagedCallingConventionList)
		{
			SyntaxKind syntaxKind = managedOrUnmanagedKeyword.Kind();
			if (syntaxKind - SyntaxKind.ManagedKeyword > 1)
			{
				throw new ArgumentException("managedOrUnmanagedKeyword");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.FunctionPointerCallingConventionSyntax)SyntaxFactory.FunctionPointerCallingConvention((SyntaxToken)managedOrUnmanagedKeyword.Node, (unmanagedCallingConventionList == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.FunctionPointerUnmanagedCallingConventionListSyntax)unmanagedCallingConventionList.Green)).CreateRed();
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x000DEB01 File Offset: 0x000DCD01
		public static Microsoft.CodeAnalysis.CSharp.Syntax.FunctionPointerCallingConventionSyntax FunctionPointerCallingConvention(SyntaxToken managedOrUnmanagedKeyword)
		{
			return SyntaxFactory.FunctionPointerCallingConvention(managedOrUnmanagedKeyword, null);
		}

		// Token: 0x0600296D RID: 10605 RVA: 0x000DEB0C File Offset: 0x000DCD0C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.FunctionPointerUnmanagedCallingConventionListSyntax FunctionPointerUnmanagedCallingConventionList(SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.FunctionPointerUnmanagedCallingConventionSyntax> callingConventions, SyntaxToken closeBracketToken)
		{
			if (openBracketToken.Kind() != SyntaxKind.OpenBracketToken)
			{
				throw new ArgumentException("openBracketToken");
			}
			if (closeBracketToken.Kind() != SyntaxKind.CloseBracketToken)
			{
				throw new ArgumentException("closeBracketToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.FunctionPointerUnmanagedCallingConventionListSyntax)SyntaxFactory.FunctionPointerUnmanagedCallingConventionList((SyntaxToken)openBracketToken.Node, callingConventions.Node.ToGreenSeparatedList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.FunctionPointerUnmanagedCallingConventionSyntax>(), (SyntaxToken)closeBracketToken.Node).CreateRed();
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x000DEB7C File Offset: 0x000DCD7C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.FunctionPointerUnmanagedCallingConventionListSyntax FunctionPointerUnmanagedCallingConventionList([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.FunctionPointerUnmanagedCallingConventionSyntax> callingConventions = default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.FunctionPointerUnmanagedCallingConventionSyntax>))
		{
			return SyntaxFactory.FunctionPointerUnmanagedCallingConventionList(SyntaxFactory.Token(SyntaxKind.OpenBracketToken), callingConventions, SyntaxFactory.Token(SyntaxKind.CloseBracketToken));
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x000DEB98 File Offset: 0x000DCD98
		public static Microsoft.CodeAnalysis.CSharp.Syntax.FunctionPointerUnmanagedCallingConventionSyntax FunctionPointerUnmanagedCallingConvention(SyntaxToken name)
		{
			if (name.Kind() != SyntaxKind.IdentifierToken)
			{
				throw new ArgumentException("name");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.FunctionPointerUnmanagedCallingConventionSyntax)SyntaxFactory.FunctionPointerUnmanagedCallingConvention((SyntaxToken)name.Node).CreateRed();
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x000DEBD0 File Offset: 0x000DCDD0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.NullableTypeSyntax NullableType(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax elementType, SyntaxToken questionToken)
		{
			if (elementType == null)
			{
				throw new ArgumentNullException("elementType");
			}
			if (questionToken.Kind() != SyntaxKind.QuestionToken)
			{
				throw new ArgumentException("questionToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.NullableTypeSyntax)SyntaxFactory.NullableType((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)elementType.Green, (SyntaxToken)questionToken.Node).CreateRed();
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x000DEC29 File Offset: 0x000DCE29
		public static Microsoft.CodeAnalysis.CSharp.Syntax.NullableTypeSyntax NullableType(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax elementType)
		{
			return SyntaxFactory.NullableType(elementType, SyntaxFactory.Token(SyntaxKind.QuestionToken));
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x000DEC3C File Offset: 0x000DCE3C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.TupleTypeSyntax TupleType(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TupleElementSyntax> elements, SyntaxToken closeParenToken)
		{
			if (openParenToken.Kind() != SyntaxKind.OpenParenToken)
			{
				throw new ArgumentException("openParenToken");
			}
			if (closeParenToken.Kind() != SyntaxKind.CloseParenToken)
			{
				throw new ArgumentException("closeParenToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.TupleTypeSyntax)SyntaxFactory.TupleType((SyntaxToken)openParenToken.Node, elements.Node.ToGreenSeparatedList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TupleElementSyntax>(), (SyntaxToken)closeParenToken.Node).CreateRed();
		}

		// Token: 0x06002973 RID: 10611 RVA: 0x000DECAC File Offset: 0x000DCEAC
		public static Microsoft.CodeAnalysis.CSharp.Syntax.TupleTypeSyntax TupleType([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TupleElementSyntax> elements = default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TupleElementSyntax>))
		{
			return SyntaxFactory.TupleType(SyntaxFactory.Token(SyntaxKind.OpenParenToken), elements, SyntaxFactory.Token(SyntaxKind.CloseParenToken));
		}

		// Token: 0x06002974 RID: 10612 RVA: 0x000DECC8 File Offset: 0x000DCEC8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.TupleElementSyntax TupleElement(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, SyntaxToken identifier)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			SyntaxKind syntaxKind = identifier.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind != SyntaxKind.IdentifierToken)
			{
				throw new ArgumentException("identifier");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.TupleElementSyntax)SyntaxFactory.TupleElement((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)type.Green, (SyntaxToken)identifier.Node).CreateRed();
		}

		// Token: 0x06002975 RID: 10613 RVA: 0x000DED28 File Offset: 0x000DCF28
		public static Microsoft.CodeAnalysis.CSharp.Syntax.TupleElementSyntax TupleElement(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type)
		{
			return SyntaxFactory.TupleElement(type, default(SyntaxToken));
		}

		// Token: 0x06002976 RID: 10614 RVA: 0x000DED44 File Offset: 0x000DCF44
		public static Microsoft.CodeAnalysis.CSharp.Syntax.OmittedTypeArgumentSyntax OmittedTypeArgument(SyntaxToken omittedTypeArgumentToken)
		{
			if (omittedTypeArgumentToken.Kind() != SyntaxKind.OmittedTypeArgumentToken)
			{
				throw new ArgumentException("omittedTypeArgumentToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.OmittedTypeArgumentSyntax)SyntaxFactory.OmittedTypeArgument((SyntaxToken)omittedTypeArgumentToken.Node).CreateRed();
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x000DED79 File Offset: 0x000DCF79
		public static Microsoft.CodeAnalysis.CSharp.Syntax.OmittedTypeArgumentSyntax OmittedTypeArgument()
		{
			return SyntaxFactory.OmittedTypeArgument(SyntaxFactory.Token(SyntaxKind.OmittedTypeArgumentToken));
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x000DED8C File Offset: 0x000DCF8C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.RefTypeSyntax RefType(SyntaxToken refKeyword, SyntaxToken readOnlyKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type)
		{
			if (refKeyword.Kind() != SyntaxKind.RefKeyword)
			{
				throw new ArgumentException("refKeyword");
			}
			SyntaxKind syntaxKind = readOnlyKeyword.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind != SyntaxKind.ReadOnlyKeyword)
			{
				throw new ArgumentException("readOnlyKeyword");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.RefTypeSyntax)SyntaxFactory.RefType((SyntaxToken)refKeyword.Node, (SyntaxToken)readOnlyKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)type.Green).CreateRed();
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x000DEE10 File Offset: 0x000DD010
		public static Microsoft.CodeAnalysis.CSharp.Syntax.RefTypeSyntax RefType(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type)
		{
			return SyntaxFactory.RefType(SyntaxFactory.Token(SyntaxKind.RefKeyword), default(SyntaxToken), type);
		}

		// Token: 0x0600297A RID: 10618 RVA: 0x000DEE38 File Offset: 0x000DD038
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ParenthesizedExpressionSyntax ParenthesizedExpression(SyntaxToken openParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SyntaxToken closeParenToken)
		{
			if (openParenToken.Kind() != SyntaxKind.OpenParenToken)
			{
				throw new ArgumentException("openParenToken");
			}
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (closeParenToken.Kind() != SyntaxKind.CloseParenToken)
			{
				throw new ArgumentException("closeParenToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ParenthesizedExpressionSyntax)SyntaxFactory.ParenthesizedExpression((SyntaxToken)openParenToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green, (SyntaxToken)closeParenToken.Node).CreateRed();
		}

		// Token: 0x0600297B RID: 10619 RVA: 0x000DEEB5 File Offset: 0x000DD0B5
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ParenthesizedExpressionSyntax ParenthesizedExpression(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			return SyntaxFactory.ParenthesizedExpression(SyntaxFactory.Token(SyntaxKind.OpenParenToken), expression, SyntaxFactory.Token(SyntaxKind.CloseParenToken));
		}

		// Token: 0x0600297C RID: 10620 RVA: 0x000DEED4 File Offset: 0x000DD0D4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.TupleExpressionSyntax TupleExpression(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ArgumentSyntax> arguments, SyntaxToken closeParenToken)
		{
			if (openParenToken.Kind() != SyntaxKind.OpenParenToken)
			{
				throw new ArgumentException("openParenToken");
			}
			if (closeParenToken.Kind() != SyntaxKind.CloseParenToken)
			{
				throw new ArgumentException("closeParenToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.TupleExpressionSyntax)SyntaxFactory.TupleExpression((SyntaxToken)openParenToken.Node, arguments.Node.ToGreenSeparatedList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ArgumentSyntax>(), (SyntaxToken)closeParenToken.Node).CreateRed();
		}

		// Token: 0x0600297D RID: 10621 RVA: 0x000DEF44 File Offset: 0x000DD144
		public static Microsoft.CodeAnalysis.CSharp.Syntax.TupleExpressionSyntax TupleExpression([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ArgumentSyntax> arguments = default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ArgumentSyntax>))
		{
			return SyntaxFactory.TupleExpression(SyntaxFactory.Token(SyntaxKind.OpenParenToken), arguments, SyntaxFactory.Token(SyntaxKind.CloseParenToken));
		}

		// Token: 0x0600297E RID: 10622 RVA: 0x000DEF60 File Offset: 0x000DD160
		public static Microsoft.CodeAnalysis.CSharp.Syntax.PrefixUnaryExpressionSyntax PrefixUnaryExpression(SyntaxKind kind, SyntaxToken operatorToken, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax operand)
		{
			if (kind - SyntaxKind.UnaryPlusExpression > 7 && kind != SyntaxKind.IndexExpression)
			{
				throw new ArgumentException("kind");
			}
			SyntaxKind syntaxKind = operatorToken.Kind();
			switch (syntaxKind)
			{
			case SyntaxKind.TildeToken:
			case SyntaxKind.ExclamationToken:
			case SyntaxKind.CaretToken:
			case SyntaxKind.AmpersandToken:
			case SyntaxKind.AsteriskToken:
			case SyntaxKind.MinusToken:
			case SyntaxKind.PlusToken:
				goto IL_0071;
			case SyntaxKind.DollarToken:
			case SyntaxKind.PercentToken:
			case SyntaxKind.OpenParenToken:
			case SyntaxKind.CloseParenToken:
				break;
			default:
				if (syntaxKind - SyntaxKind.MinusMinusToken <= 1)
				{
					goto IL_0071;
				}
				break;
			}
			throw new ArgumentException("operatorToken");
			IL_0071:
			if (operand == null)
			{
				throw new ArgumentNullException("operand");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.PrefixUnaryExpressionSyntax)SyntaxFactory.PrefixUnaryExpression(kind, (SyntaxToken)operatorToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)operand.Green).CreateRed();
		}

		// Token: 0x0600297F RID: 10623 RVA: 0x000DF013 File Offset: 0x000DD213
		public static Microsoft.CodeAnalysis.CSharp.Syntax.PrefixUnaryExpressionSyntax PrefixUnaryExpression(SyntaxKind kind, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax operand)
		{
			return SyntaxFactory.PrefixUnaryExpression(kind, SyntaxFactory.Token(SyntaxFactory.GetPrefixUnaryExpressionOperatorTokenKind(kind)), operand);
		}

		// Token: 0x06002980 RID: 10624 RVA: 0x000DF028 File Offset: 0x000DD228
		private static SyntaxKind GetPrefixUnaryExpressionOperatorTokenKind(SyntaxKind kind)
		{
			switch (kind)
			{
			case SyntaxKind.UnaryPlusExpression:
				return SyntaxKind.PlusToken;
			case SyntaxKind.UnaryMinusExpression:
				return SyntaxKind.MinusToken;
			case SyntaxKind.BitwiseNotExpression:
				return SyntaxKind.TildeToken;
			case SyntaxKind.LogicalNotExpression:
				return SyntaxKind.ExclamationToken;
			case SyntaxKind.PreIncrementExpression:
				return SyntaxKind.PlusPlusToken;
			case SyntaxKind.PreDecrementExpression:
				return SyntaxKind.MinusMinusToken;
			case SyntaxKind.PointerIndirectionExpression:
				return SyntaxKind.AsteriskToken;
			case SyntaxKind.AddressOfExpression:
				return SyntaxKind.AmpersandToken;
			case SyntaxKind.IndexExpression:
				return SyntaxKind.CaretToken;
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x06002981 RID: 10625 RVA: 0x000DF0C4 File Offset: 0x000DD2C4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AwaitExpressionSyntax AwaitExpression(SyntaxToken awaitKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			if (awaitKeyword.Kind() != SyntaxKind.AwaitKeyword)
			{
				throw new ArgumentException("awaitKeyword");
			}
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.AwaitExpressionSyntax)SyntaxFactory.AwaitExpression((SyntaxToken)awaitKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green).CreateRed();
		}

		// Token: 0x06002982 RID: 10626 RVA: 0x000DF11D File Offset: 0x000DD31D
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AwaitExpressionSyntax AwaitExpression(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			return SyntaxFactory.AwaitExpression(SyntaxFactory.Token(SyntaxKind.AwaitKeyword), expression);
		}

		// Token: 0x06002983 RID: 10627 RVA: 0x000DF130 File Offset: 0x000DD330
		public static Microsoft.CodeAnalysis.CSharp.Syntax.PostfixUnaryExpressionSyntax PostfixUnaryExpression(SyntaxKind kind, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax operand, SyntaxToken operatorToken)
		{
			if (kind - SyntaxKind.PostIncrementExpression > 1 && kind != SyntaxKind.SuppressNullableWarningExpression)
			{
				throw new ArgumentException("kind");
			}
			if (operand == null)
			{
				throw new ArgumentNullException("operand");
			}
			SyntaxKind syntaxKind = operatorToken.Kind();
			if (syntaxKind != SyntaxKind.ExclamationToken && syntaxKind - SyntaxKind.MinusMinusToken > 1)
			{
				throw new ArgumentException("operatorToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.PostfixUnaryExpressionSyntax)SyntaxFactory.PostfixUnaryExpression(kind, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)operand.Green, (SyntaxToken)operatorToken.Node).CreateRed();
		}

		// Token: 0x06002984 RID: 10628 RVA: 0x000DF1B3 File Offset: 0x000DD3B3
		public static Microsoft.CodeAnalysis.CSharp.Syntax.PostfixUnaryExpressionSyntax PostfixUnaryExpression(SyntaxKind kind, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax operand)
		{
			return SyntaxFactory.PostfixUnaryExpression(kind, operand, SyntaxFactory.Token(SyntaxFactory.GetPostfixUnaryExpressionOperatorTokenKind(kind)));
		}

		// Token: 0x06002985 RID: 10629 RVA: 0x000DF1C8 File Offset: 0x000DD3C8
		private static SyntaxKind GetPostfixUnaryExpressionOperatorTokenKind(SyntaxKind kind)
		{
			SyntaxKind syntaxKind;
			if (kind != SyntaxKind.PostIncrementExpression)
			{
				if (kind != SyntaxKind.PostDecrementExpression)
				{
					if (kind != SyntaxKind.SuppressNullableWarningExpression)
					{
						throw new ArgumentOutOfRangeException();
					}
					syntaxKind = SyntaxKind.ExclamationToken;
				}
				else
				{
					syntaxKind = SyntaxKind.MinusMinusToken;
				}
			}
			else
			{
				syntaxKind = SyntaxKind.PlusPlusToken;
			}
			return syntaxKind;
		}

		// Token: 0x06002986 RID: 10630 RVA: 0x000DF210 File Offset: 0x000DD410
		public static Microsoft.CodeAnalysis.CSharp.Syntax.MemberAccessExpressionSyntax MemberAccessExpression(SyntaxKind kind, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SyntaxToken operatorToken, Microsoft.CodeAnalysis.CSharp.Syntax.SimpleNameSyntax name)
		{
			if (kind - SyntaxKind.SimpleMemberAccessExpression > 1)
			{
				throw new ArgumentException("kind");
			}
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			SyntaxKind syntaxKind = operatorToken.Kind();
			if (syntaxKind != SyntaxKind.DotToken && syntaxKind != SyntaxKind.MinusGreaterThanToken)
			{
				throw new ArgumentException("operatorToken");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.MemberAccessExpressionSyntax)SyntaxFactory.MemberAccessExpression(kind, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green, (SyntaxToken)operatorToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.SimpleNameSyntax)name.Green).CreateRed();
		}

		// Token: 0x06002987 RID: 10631 RVA: 0x000DF2A2 File Offset: 0x000DD4A2
		public static Microsoft.CodeAnalysis.CSharp.Syntax.MemberAccessExpressionSyntax MemberAccessExpression(SyntaxKind kind, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, Microsoft.CodeAnalysis.CSharp.Syntax.SimpleNameSyntax name)
		{
			return SyntaxFactory.MemberAccessExpression(kind, expression, SyntaxFactory.Token(SyntaxFactory.GetMemberAccessExpressionOperatorTokenKind(kind)), name);
		}

		// Token: 0x06002988 RID: 10632 RVA: 0x000DF2B8 File Offset: 0x000DD4B8
		private static SyntaxKind GetMemberAccessExpressionOperatorTokenKind(SyntaxKind kind)
		{
			SyntaxKind syntaxKind;
			if (kind != SyntaxKind.SimpleMemberAccessExpression)
			{
				if (kind != SyntaxKind.PointerMemberAccessExpression)
				{
					throw new ArgumentOutOfRangeException();
				}
				syntaxKind = SyntaxKind.MinusGreaterThanToken;
			}
			else
			{
				syntaxKind = SyntaxKind.DotToken;
			}
			return syntaxKind;
		}

		// Token: 0x06002989 RID: 10633 RVA: 0x000DF2F0 File Offset: 0x000DD4F0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ConditionalAccessExpressionSyntax ConditionalAccessExpression(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SyntaxToken operatorToken, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax whenNotNull)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (operatorToken.Kind() != SyntaxKind.QuestionToken)
			{
				throw new ArgumentException("operatorToken");
			}
			if (whenNotNull == null)
			{
				throw new ArgumentNullException("whenNotNull");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ConditionalAccessExpressionSyntax)SyntaxFactory.ConditionalAccessExpression((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green, (SyntaxToken)operatorToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)whenNotNull.Green).CreateRed();
		}

		// Token: 0x0600298A RID: 10634 RVA: 0x000DF362 File Offset: 0x000DD562
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ConditionalAccessExpressionSyntax ConditionalAccessExpression(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax whenNotNull)
		{
			return SyntaxFactory.ConditionalAccessExpression(expression, SyntaxFactory.Token(SyntaxKind.QuestionToken), whenNotNull);
		}

		// Token: 0x0600298B RID: 10635 RVA: 0x000DF378 File Offset: 0x000DD578
		public static Microsoft.CodeAnalysis.CSharp.Syntax.MemberBindingExpressionSyntax MemberBindingExpression(SyntaxToken operatorToken, Microsoft.CodeAnalysis.CSharp.Syntax.SimpleNameSyntax name)
		{
			if (operatorToken.Kind() != SyntaxKind.DotToken)
			{
				throw new ArgumentException("operatorToken");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.MemberBindingExpressionSyntax)SyntaxFactory.MemberBindingExpression((SyntaxToken)operatorToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.SimpleNameSyntax)name.Green).CreateRed();
		}

		// Token: 0x0600298C RID: 10636 RVA: 0x000DF3D1 File Offset: 0x000DD5D1
		public static Microsoft.CodeAnalysis.CSharp.Syntax.MemberBindingExpressionSyntax MemberBindingExpression(Microsoft.CodeAnalysis.CSharp.Syntax.SimpleNameSyntax name)
		{
			return SyntaxFactory.MemberBindingExpression(SyntaxFactory.Token(SyntaxKind.DotToken), name);
		}

		// Token: 0x0600298D RID: 10637 RVA: 0x000DF3E3 File Offset: 0x000DD5E3
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ElementBindingExpressionSyntax ElementBindingExpression(Microsoft.CodeAnalysis.CSharp.Syntax.BracketedArgumentListSyntax argumentList)
		{
			if (argumentList == null)
			{
				throw new ArgumentNullException("argumentList");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ElementBindingExpressionSyntax)SyntaxFactory.ElementBindingExpression((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.BracketedArgumentListSyntax)argumentList.Green).CreateRed();
		}

		// Token: 0x0600298E RID: 10638 RVA: 0x000DF410 File Offset: 0x000DD610
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ElementBindingExpressionSyntax ElementBindingExpression()
		{
			return SyntaxFactory.ElementBindingExpression(SyntaxFactory.BracketedArgumentList(default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ArgumentSyntax>)));
		}

		// Token: 0x0600298F RID: 10639 RVA: 0x000DF430 File Offset: 0x000DD630
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public static Microsoft.CodeAnalysis.CSharp.Syntax.RangeExpressionSyntax RangeExpression(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax leftOperand, SyntaxToken operatorToken, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax rightOperand)
		{
			if (operatorToken.Kind() != SyntaxKind.DotDotToken)
			{
				throw new ArgumentException("operatorToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.RangeExpressionSyntax)SyntaxFactory.RangeExpression((leftOperand == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)leftOperand.Green), (SyntaxToken)operatorToken.Node, (rightOperand == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)rightOperand.Green)).CreateRed();
		}

		// Token: 0x06002990 RID: 10640 RVA: 0x000DF492 File Offset: 0x000DD692
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public static Microsoft.CodeAnalysis.CSharp.Syntax.RangeExpressionSyntax RangeExpression(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax leftOperand, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax rightOperand)
		{
			return SyntaxFactory.RangeExpression(leftOperand, SyntaxFactory.Token(SyntaxKind.DotDotToken), rightOperand);
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x000DF4A5 File Offset: 0x000DD6A5
		public static Microsoft.CodeAnalysis.CSharp.Syntax.RangeExpressionSyntax RangeExpression()
		{
			return SyntaxFactory.RangeExpression(null, SyntaxFactory.Token(SyntaxKind.DotDotToken), null);
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x000DF4B8 File Offset: 0x000DD6B8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ImplicitElementAccessSyntax ImplicitElementAccess(Microsoft.CodeAnalysis.CSharp.Syntax.BracketedArgumentListSyntax argumentList)
		{
			if (argumentList == null)
			{
				throw new ArgumentNullException("argumentList");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ImplicitElementAccessSyntax)SyntaxFactory.ImplicitElementAccess((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.BracketedArgumentListSyntax)argumentList.Green).CreateRed();
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x000DF4E4 File Offset: 0x000DD6E4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ImplicitElementAccessSyntax ImplicitElementAccess()
		{
			return SyntaxFactory.ImplicitElementAccess(SyntaxFactory.BracketedArgumentList(default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ArgumentSyntax>)));
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x000DF504 File Offset: 0x000DD704
		public static Microsoft.CodeAnalysis.CSharp.Syntax.BinaryExpressionSyntax BinaryExpression(SyntaxKind kind, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax left, SyntaxToken operatorToken, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax right)
		{
			if (kind - SyntaxKind.AddExpression > 20)
			{
				throw new ArgumentException("kind");
			}
			if (left == null)
			{
				throw new ArgumentNullException("left");
			}
			SyntaxKind syntaxKind = operatorToken.Kind();
			if (syntaxKind <= SyntaxKind.LessThanToken)
			{
				if (syntaxKind <= SyntaxKind.PlusToken)
				{
					if (syntaxKind - SyntaxKind.PercentToken <= 3 || syntaxKind - SyntaxKind.MinusToken <= 1)
					{
						goto IL_00E6;
					}
				}
				else if (syntaxKind == SyntaxKind.BarToken || syntaxKind == SyntaxKind.LessThanToken)
				{
					goto IL_00E6;
				}
			}
			else if (syntaxKind <= SyntaxKind.SlashToken)
			{
				if (syntaxKind == SyntaxKind.GreaterThanToken || syntaxKind == SyntaxKind.SlashToken)
				{
					goto IL_00E6;
				}
			}
			else
			{
				switch (syntaxKind)
				{
				case SyntaxKind.BarBarToken:
				case SyntaxKind.AmpersandAmpersandToken:
				case SyntaxKind.QuestionQuestionToken:
				case SyntaxKind.ExclamationEqualsToken:
				case SyntaxKind.EqualsEqualsToken:
				case SyntaxKind.LessThanEqualsToken:
				case SyntaxKind.LessThanLessThanToken:
				case SyntaxKind.GreaterThanEqualsToken:
				case SyntaxKind.GreaterThanGreaterThanToken:
					goto IL_00E6;
				case SyntaxKind.MinusMinusToken:
				case SyntaxKind.PlusPlusToken:
				case SyntaxKind.ColonColonToken:
				case SyntaxKind.MinusGreaterThanToken:
				case SyntaxKind.EqualsGreaterThanToken:
				case SyntaxKind.LessThanLessThanEqualsToken:
					break;
				default:
					if (syntaxKind - SyntaxKind.IsKeyword <= 1)
					{
						goto IL_00E6;
					}
					break;
				}
			}
			throw new ArgumentException("operatorToken");
			IL_00E6:
			if (right == null)
			{
				throw new ArgumentNullException("right");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.BinaryExpressionSyntax)SyntaxFactory.BinaryExpression(kind, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)left.Green, (SyntaxToken)operatorToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)right.Green).CreateRed();
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x000DF637 File Offset: 0x000DD837
		public static Microsoft.CodeAnalysis.CSharp.Syntax.BinaryExpressionSyntax BinaryExpression(SyntaxKind kind, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax left, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax right)
		{
			return SyntaxFactory.BinaryExpression(kind, left, SyntaxFactory.Token(SyntaxFactory.GetBinaryExpressionOperatorTokenKind(kind)), right);
		}

		// Token: 0x06002996 RID: 10646 RVA: 0x000DF64C File Offset: 0x000DD84C
		private static SyntaxKind GetBinaryExpressionOperatorTokenKind(SyntaxKind kind)
		{
			SyntaxKind syntaxKind;
			switch (kind)
			{
			case SyntaxKind.AddExpression:
				syntaxKind = SyntaxKind.PlusToken;
				break;
			case SyntaxKind.SubtractExpression:
				syntaxKind = SyntaxKind.MinusToken;
				break;
			case SyntaxKind.MultiplyExpression:
				syntaxKind = SyntaxKind.AsteriskToken;
				break;
			case SyntaxKind.DivideExpression:
				syntaxKind = SyntaxKind.SlashToken;
				break;
			case SyntaxKind.ModuloExpression:
				syntaxKind = SyntaxKind.PercentToken;
				break;
			case SyntaxKind.LeftShiftExpression:
				syntaxKind = SyntaxKind.LessThanLessThanToken;
				break;
			case SyntaxKind.RightShiftExpression:
				syntaxKind = SyntaxKind.GreaterThanGreaterThanToken;
				break;
			case SyntaxKind.LogicalOrExpression:
				syntaxKind = SyntaxKind.BarBarToken;
				break;
			case SyntaxKind.LogicalAndExpression:
				syntaxKind = SyntaxKind.AmpersandAmpersandToken;
				break;
			case SyntaxKind.BitwiseOrExpression:
				syntaxKind = SyntaxKind.BarToken;
				break;
			case SyntaxKind.BitwiseAndExpression:
				syntaxKind = SyntaxKind.AmpersandToken;
				break;
			case SyntaxKind.ExclusiveOrExpression:
				syntaxKind = SyntaxKind.CaretToken;
				break;
			case SyntaxKind.EqualsExpression:
				syntaxKind = SyntaxKind.EqualsEqualsToken;
				break;
			case SyntaxKind.NotEqualsExpression:
				syntaxKind = SyntaxKind.ExclamationEqualsToken;
				break;
			case SyntaxKind.LessThanExpression:
				syntaxKind = SyntaxKind.LessThanToken;
				break;
			case SyntaxKind.LessThanOrEqualExpression:
				syntaxKind = SyntaxKind.LessThanEqualsToken;
				break;
			case SyntaxKind.GreaterThanExpression:
				syntaxKind = SyntaxKind.GreaterThanToken;
				break;
			case SyntaxKind.GreaterThanOrEqualExpression:
				syntaxKind = SyntaxKind.GreaterThanEqualsToken;
				break;
			case SyntaxKind.IsExpression:
				syntaxKind = SyntaxKind.IsKeyword;
				break;
			case SyntaxKind.AsExpression:
				syntaxKind = SyntaxKind.AsKeyword;
				break;
			case SyntaxKind.CoalesceExpression:
				syntaxKind = SyntaxKind.QuestionQuestionToken;
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			return syntaxKind;
		}

		// Token: 0x06002997 RID: 10647 RVA: 0x000DF77C File Offset: 0x000DD97C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AssignmentExpressionSyntax AssignmentExpression(SyntaxKind kind, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax left, SyntaxToken operatorToken, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax right)
		{
			if (kind - SyntaxKind.SimpleAssignmentExpression > 11)
			{
				throw new ArgumentException("kind");
			}
			if (left == null)
			{
				throw new ArgumentNullException("left");
			}
			SyntaxKind syntaxKind = operatorToken.Kind();
			if (syntaxKind != SyntaxKind.EqualsToken && syntaxKind != SyntaxKind.LessThanLessThanEqualsToken && syntaxKind - SyntaxKind.GreaterThanGreaterThanEqualsToken > 9)
			{
				throw new ArgumentException("operatorToken");
			}
			if (right == null)
			{
				throw new ArgumentNullException("right");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.AssignmentExpressionSyntax)SyntaxFactory.AssignmentExpression(kind, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)left.Green, (SyntaxToken)operatorToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)right.Green).CreateRed();
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x000DF81A File Offset: 0x000DDA1A
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AssignmentExpressionSyntax AssignmentExpression(SyntaxKind kind, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax left, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax right)
		{
			return SyntaxFactory.AssignmentExpression(kind, left, SyntaxFactory.Token(SyntaxFactory.GetAssignmentExpressionOperatorTokenKind(kind)), right);
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x000DF830 File Offset: 0x000DDA30
		private static SyntaxKind GetAssignmentExpressionOperatorTokenKind(SyntaxKind kind)
		{
			SyntaxKind syntaxKind;
			switch (kind)
			{
			case SyntaxKind.SimpleAssignmentExpression:
				syntaxKind = SyntaxKind.EqualsToken;
				break;
			case SyntaxKind.AddAssignmentExpression:
				syntaxKind = SyntaxKind.PlusEqualsToken;
				break;
			case SyntaxKind.SubtractAssignmentExpression:
				syntaxKind = SyntaxKind.MinusEqualsToken;
				break;
			case SyntaxKind.MultiplyAssignmentExpression:
				syntaxKind = SyntaxKind.AsteriskEqualsToken;
				break;
			case SyntaxKind.DivideAssignmentExpression:
				syntaxKind = SyntaxKind.SlashEqualsToken;
				break;
			case SyntaxKind.ModuloAssignmentExpression:
				syntaxKind = SyntaxKind.PercentEqualsToken;
				break;
			case SyntaxKind.AndAssignmentExpression:
				syntaxKind = SyntaxKind.AmpersandEqualsToken;
				break;
			case SyntaxKind.ExclusiveOrAssignmentExpression:
				syntaxKind = SyntaxKind.CaretEqualsToken;
				break;
			case SyntaxKind.OrAssignmentExpression:
				syntaxKind = SyntaxKind.BarEqualsToken;
				break;
			case SyntaxKind.LeftShiftAssignmentExpression:
				syntaxKind = SyntaxKind.LessThanLessThanEqualsToken;
				break;
			case SyntaxKind.RightShiftAssignmentExpression:
				syntaxKind = SyntaxKind.GreaterThanGreaterThanEqualsToken;
				break;
			case SyntaxKind.CoalesceAssignmentExpression:
				syntaxKind = SyntaxKind.QuestionQuestionEqualsToken;
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			return syntaxKind;
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x000DF8E4 File Offset: 0x000DDAE4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ConditionalExpressionSyntax ConditionalExpression(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax condition, SyntaxToken questionToken, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax whenTrue, SyntaxToken colonToken, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax whenFalse)
		{
			if (condition == null)
			{
				throw new ArgumentNullException("condition");
			}
			if (questionToken.Kind() != SyntaxKind.QuestionToken)
			{
				throw new ArgumentException("questionToken");
			}
			if (whenTrue == null)
			{
				throw new ArgumentNullException("whenTrue");
			}
			if (colonToken.Kind() != SyntaxKind.ColonToken)
			{
				throw new ArgumentException("colonToken");
			}
			if (whenFalse == null)
			{
				throw new ArgumentNullException("whenFalse");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ConditionalExpressionSyntax)SyntaxFactory.ConditionalExpression((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)condition.Green, (SyntaxToken)questionToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)whenTrue.Green, (SyntaxToken)colonToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)whenFalse.Green).CreateRed();
		}

		// Token: 0x0600299B RID: 10651 RVA: 0x000DF995 File Offset: 0x000DDB95
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ConditionalExpressionSyntax ConditionalExpression(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax condition, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax whenTrue, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax whenFalse)
		{
			return SyntaxFactory.ConditionalExpression(condition, SyntaxFactory.Token(SyntaxKind.QuestionToken), whenTrue, SyntaxFactory.Token(SyntaxKind.ColonToken), whenFalse);
		}

		// Token: 0x0600299C RID: 10652 RVA: 0x000DF9B3 File Offset: 0x000DDBB3
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ThisExpressionSyntax ThisExpression(SyntaxToken token)
		{
			if (token.Kind() != SyntaxKind.ThisKeyword)
			{
				throw new ArgumentException("token");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ThisExpressionSyntax)SyntaxFactory.ThisExpression((SyntaxToken)token.Node).CreateRed();
		}

		// Token: 0x0600299D RID: 10653 RVA: 0x000DF9E8 File Offset: 0x000DDBE8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ThisExpressionSyntax ThisExpression()
		{
			return SyntaxFactory.ThisExpression(SyntaxFactory.Token(SyntaxKind.ThisKeyword));
		}

		// Token: 0x0600299E RID: 10654 RVA: 0x000DF9F9 File Offset: 0x000DDBF9
		public static Microsoft.CodeAnalysis.CSharp.Syntax.BaseExpressionSyntax BaseExpression(SyntaxToken token)
		{
			if (token.Kind() != SyntaxKind.BaseKeyword)
			{
				throw new ArgumentException("token");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.BaseExpressionSyntax)SyntaxFactory.BaseExpression((SyntaxToken)token.Node).CreateRed();
		}

		// Token: 0x0600299F RID: 10655 RVA: 0x000DFA2E File Offset: 0x000DDC2E
		public static Microsoft.CodeAnalysis.CSharp.Syntax.BaseExpressionSyntax BaseExpression()
		{
			return SyntaxFactory.BaseExpression(SyntaxFactory.Token(SyntaxKind.BaseKeyword));
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x000DFA40 File Offset: 0x000DDC40
		public static Microsoft.CodeAnalysis.CSharp.Syntax.LiteralExpressionSyntax LiteralExpression(SyntaxKind kind, SyntaxToken token)
		{
			if (kind - SyntaxKind.ArgListExpression > 7)
			{
				throw new ArgumentException("kind");
			}
			SyntaxKind syntaxKind = token.Kind();
			if (syntaxKind <= SyntaxKind.DefaultKeyword)
			{
				if (syntaxKind - SyntaxKind.NullKeyword <= 2 || syntaxKind == SyntaxKind.DefaultKeyword)
				{
					goto IL_0055;
				}
			}
			else if (syntaxKind == SyntaxKind.ArgListKeyword || syntaxKind - SyntaxKind.NumericLiteralToken <= 2)
			{
				goto IL_0055;
			}
			throw new ArgumentException("token");
			IL_0055:
			return (Microsoft.CodeAnalysis.CSharp.Syntax.LiteralExpressionSyntax)SyntaxFactory.LiteralExpression(kind, (SyntaxToken)token.Node).CreateRed();
		}

		// Token: 0x060029A1 RID: 10657 RVA: 0x000DFABE File Offset: 0x000DDCBE
		public static Microsoft.CodeAnalysis.CSharp.Syntax.LiteralExpressionSyntax LiteralExpression(SyntaxKind kind)
		{
			return SyntaxFactory.LiteralExpression(kind, SyntaxFactory.Token(SyntaxFactory.GetLiteralExpressionTokenKind(kind)));
		}

		// Token: 0x060029A2 RID: 10658 RVA: 0x000DFAD4 File Offset: 0x000DDCD4
		private static SyntaxKind GetLiteralExpressionTokenKind(SyntaxKind kind)
		{
			SyntaxKind syntaxKind;
			switch (kind)
			{
			case SyntaxKind.ArgListExpression:
				syntaxKind = SyntaxKind.ArgListKeyword;
				break;
			case SyntaxKind.NumericLiteralExpression:
				syntaxKind = SyntaxKind.NumericLiteralToken;
				break;
			case SyntaxKind.StringLiteralExpression:
				syntaxKind = SyntaxKind.StringLiteralToken;
				break;
			case SyntaxKind.CharacterLiteralExpression:
				syntaxKind = SyntaxKind.CharacterLiteralToken;
				break;
			case SyntaxKind.TrueLiteralExpression:
				syntaxKind = SyntaxKind.TrueKeyword;
				break;
			case SyntaxKind.FalseLiteralExpression:
				syntaxKind = SyntaxKind.FalseKeyword;
				break;
			case SyntaxKind.NullLiteralExpression:
				syntaxKind = SyntaxKind.NullKeyword;
				break;
			case SyntaxKind.DefaultLiteralExpression:
				syntaxKind = SyntaxKind.DefaultKeyword;
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			return syntaxKind;
		}

		// Token: 0x060029A3 RID: 10659 RVA: 0x000DFB58 File Offset: 0x000DDD58
		public static Microsoft.CodeAnalysis.CSharp.Syntax.MakeRefExpressionSyntax MakeRefExpression(SyntaxToken keyword, SyntaxToken openParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SyntaxToken closeParenToken)
		{
			if (keyword.Kind() != SyntaxKind.MakeRefKeyword)
			{
				throw new ArgumentException("keyword");
			}
			if (openParenToken.Kind() != SyntaxKind.OpenParenToken)
			{
				throw new ArgumentException("openParenToken");
			}
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (closeParenToken.Kind() != SyntaxKind.CloseParenToken)
			{
				throw new ArgumentException("closeParenToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.MakeRefExpressionSyntax)SyntaxFactory.MakeRefExpression((SyntaxToken)keyword.Node, (SyntaxToken)openParenToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green, (SyntaxToken)closeParenToken.Node).CreateRed();
		}

		// Token: 0x060029A4 RID: 10660 RVA: 0x000DFBF9 File Offset: 0x000DDDF9
		public static Microsoft.CodeAnalysis.CSharp.Syntax.MakeRefExpressionSyntax MakeRefExpression(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			return SyntaxFactory.MakeRefExpression(SyntaxFactory.Token(SyntaxKind.MakeRefKeyword), SyntaxFactory.Token(SyntaxKind.OpenParenToken), expression, SyntaxFactory.Token(SyntaxKind.CloseParenToken));
		}

		// Token: 0x060029A5 RID: 10661 RVA: 0x000DFC20 File Offset: 0x000DDE20
		public static Microsoft.CodeAnalysis.CSharp.Syntax.RefTypeExpressionSyntax RefTypeExpression(SyntaxToken keyword, SyntaxToken openParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SyntaxToken closeParenToken)
		{
			if (keyword.Kind() != SyntaxKind.RefTypeKeyword)
			{
				throw new ArgumentException("keyword");
			}
			if (openParenToken.Kind() != SyntaxKind.OpenParenToken)
			{
				throw new ArgumentException("openParenToken");
			}
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (closeParenToken.Kind() != SyntaxKind.CloseParenToken)
			{
				throw new ArgumentException("closeParenToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.RefTypeExpressionSyntax)SyntaxFactory.RefTypeExpression((SyntaxToken)keyword.Node, (SyntaxToken)openParenToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green, (SyntaxToken)closeParenToken.Node).CreateRed();
		}

		// Token: 0x060029A6 RID: 10662 RVA: 0x000DFCC1 File Offset: 0x000DDEC1
		public static Microsoft.CodeAnalysis.CSharp.Syntax.RefTypeExpressionSyntax RefTypeExpression(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			return SyntaxFactory.RefTypeExpression(SyntaxFactory.Token(SyntaxKind.RefTypeKeyword), SyntaxFactory.Token(SyntaxKind.OpenParenToken), expression, SyntaxFactory.Token(SyntaxKind.CloseParenToken));
		}

		// Token: 0x060029A7 RID: 10663 RVA: 0x000DFCE8 File Offset: 0x000DDEE8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.RefValueExpressionSyntax RefValueExpression(SyntaxToken keyword, SyntaxToken openParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SyntaxToken comma, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, SyntaxToken closeParenToken)
		{
			if (keyword.Kind() != SyntaxKind.RefValueKeyword)
			{
				throw new ArgumentException("keyword");
			}
			if (openParenToken.Kind() != SyntaxKind.OpenParenToken)
			{
				throw new ArgumentException("openParenToken");
			}
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (comma.Kind() != SyntaxKind.CommaToken)
			{
				throw new ArgumentException("comma");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (closeParenToken.Kind() != SyntaxKind.CloseParenToken)
			{
				throw new ArgumentException("closeParenToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.RefValueExpressionSyntax)SyntaxFactory.RefValueExpression((SyntaxToken)keyword.Node, (SyntaxToken)openParenToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green, (SyntaxToken)comma.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)type.Green, (SyntaxToken)closeParenToken.Node).CreateRed();
		}

		// Token: 0x060029A8 RID: 10664 RVA: 0x000DFDC9 File Offset: 0x000DDFC9
		public static Microsoft.CodeAnalysis.CSharp.Syntax.RefValueExpressionSyntax RefValueExpression(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type)
		{
			return SyntaxFactory.RefValueExpression(SyntaxFactory.Token(SyntaxKind.RefValueKeyword), SyntaxFactory.Token(SyntaxKind.OpenParenToken), expression, SyntaxFactory.Token(SyntaxKind.CommaToken), type, SyntaxFactory.Token(SyntaxKind.CloseParenToken));
		}

		// Token: 0x060029A9 RID: 10665 RVA: 0x000DFDFC File Offset: 0x000DDFFC
		public static Microsoft.CodeAnalysis.CSharp.Syntax.CheckedExpressionSyntax CheckedExpression(SyntaxKind kind, SyntaxToken keyword, SyntaxToken openParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SyntaxToken closeParenToken)
		{
			if (kind - SyntaxKind.CheckedExpression > 1)
			{
				throw new ArgumentException("kind");
			}
			SyntaxKind syntaxKind = keyword.Kind();
			if (syntaxKind - SyntaxKind.CheckedKeyword > 1)
			{
				throw new ArgumentException("keyword");
			}
			if (openParenToken.Kind() != SyntaxKind.OpenParenToken)
			{
				throw new ArgumentException("openParenToken");
			}
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (closeParenToken.Kind() != SyntaxKind.CloseParenToken)
			{
				throw new ArgumentException("closeParenToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.CheckedExpressionSyntax)SyntaxFactory.CheckedExpression(kind, (SyntaxToken)keyword.Node, (SyntaxToken)openParenToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green, (SyntaxToken)closeParenToken.Node).CreateRed();
		}

		// Token: 0x060029AA RID: 10666 RVA: 0x000DFEB8 File Offset: 0x000DE0B8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.CheckedExpressionSyntax CheckedExpression(SyntaxKind kind, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			return SyntaxFactory.CheckedExpression(kind, SyntaxFactory.Token(SyntaxFactory.GetCheckedExpressionKeywordKind(kind)), SyntaxFactory.Token(SyntaxKind.OpenParenToken), expression, SyntaxFactory.Token(SyntaxKind.CloseParenToken));
		}

		// Token: 0x060029AB RID: 10667 RVA: 0x000DFEE0 File Offset: 0x000DE0E0
		private static SyntaxKind GetCheckedExpressionKeywordKind(SyntaxKind kind)
		{
			SyntaxKind syntaxKind;
			if (kind != SyntaxKind.CheckedExpression)
			{
				if (kind != SyntaxKind.UncheckedExpression)
				{
					throw new ArgumentOutOfRangeException();
				}
				syntaxKind = SyntaxKind.UncheckedKeyword;
			}
			else
			{
				syntaxKind = SyntaxKind.CheckedKeyword;
			}
			return syntaxKind;
		}

		// Token: 0x060029AC RID: 10668 RVA: 0x000DFF18 File Offset: 0x000DE118
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DefaultExpressionSyntax DefaultExpression(SyntaxToken keyword, SyntaxToken openParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, SyntaxToken closeParenToken)
		{
			if (keyword.Kind() != SyntaxKind.DefaultKeyword)
			{
				throw new ArgumentException("keyword");
			}
			if (openParenToken.Kind() != SyntaxKind.OpenParenToken)
			{
				throw new ArgumentException("openParenToken");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (closeParenToken.Kind() != SyntaxKind.CloseParenToken)
			{
				throw new ArgumentException("closeParenToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.DefaultExpressionSyntax)SyntaxFactory.DefaultExpression((SyntaxToken)keyword.Node, (SyntaxToken)openParenToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)type.Green, (SyntaxToken)closeParenToken.Node).CreateRed();
		}

		// Token: 0x060029AD RID: 10669 RVA: 0x000DFFB9 File Offset: 0x000DE1B9
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DefaultExpressionSyntax DefaultExpression(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type)
		{
			return SyntaxFactory.DefaultExpression(SyntaxFactory.Token(SyntaxKind.DefaultKeyword), SyntaxFactory.Token(SyntaxKind.OpenParenToken), type, SyntaxFactory.Token(SyntaxKind.CloseParenToken));
		}

		// Token: 0x060029AE RID: 10670 RVA: 0x000DFFE0 File Offset: 0x000DE1E0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.TypeOfExpressionSyntax TypeOfExpression(SyntaxToken keyword, SyntaxToken openParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, SyntaxToken closeParenToken)
		{
			if (keyword.Kind() != SyntaxKind.TypeOfKeyword)
			{
				throw new ArgumentException("keyword");
			}
			if (openParenToken.Kind() != SyntaxKind.OpenParenToken)
			{
				throw new ArgumentException("openParenToken");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (closeParenToken.Kind() != SyntaxKind.CloseParenToken)
			{
				throw new ArgumentException("closeParenToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.TypeOfExpressionSyntax)SyntaxFactory.TypeOfExpression((SyntaxToken)keyword.Node, (SyntaxToken)openParenToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)type.Green, (SyntaxToken)closeParenToken.Node).CreateRed();
		}

		// Token: 0x060029AF RID: 10671 RVA: 0x000E0081 File Offset: 0x000DE281
		public static Microsoft.CodeAnalysis.CSharp.Syntax.TypeOfExpressionSyntax TypeOfExpression(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type)
		{
			return SyntaxFactory.TypeOfExpression(SyntaxFactory.Token(SyntaxKind.TypeOfKeyword), SyntaxFactory.Token(SyntaxKind.OpenParenToken), type, SyntaxFactory.Token(SyntaxKind.CloseParenToken));
		}

		// Token: 0x060029B0 RID: 10672 RVA: 0x000E00A8 File Offset: 0x000DE2A8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.SizeOfExpressionSyntax SizeOfExpression(SyntaxToken keyword, SyntaxToken openParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, SyntaxToken closeParenToken)
		{
			if (keyword.Kind() != SyntaxKind.SizeOfKeyword)
			{
				throw new ArgumentException("keyword");
			}
			if (openParenToken.Kind() != SyntaxKind.OpenParenToken)
			{
				throw new ArgumentException("openParenToken");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (closeParenToken.Kind() != SyntaxKind.CloseParenToken)
			{
				throw new ArgumentException("closeParenToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.SizeOfExpressionSyntax)SyntaxFactory.SizeOfExpression((SyntaxToken)keyword.Node, (SyntaxToken)openParenToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)type.Green, (SyntaxToken)closeParenToken.Node).CreateRed();
		}

		// Token: 0x060029B1 RID: 10673 RVA: 0x000E0149 File Offset: 0x000DE349
		public static Microsoft.CodeAnalysis.CSharp.Syntax.SizeOfExpressionSyntax SizeOfExpression(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type)
		{
			return SyntaxFactory.SizeOfExpression(SyntaxFactory.Token(SyntaxKind.SizeOfKeyword), SyntaxFactory.Token(SyntaxKind.OpenParenToken), type, SyntaxFactory.Token(SyntaxKind.CloseParenToken));
		}

		// Token: 0x060029B2 RID: 10674 RVA: 0x000E0170 File Offset: 0x000DE370
		public static Microsoft.CodeAnalysis.CSharp.Syntax.InvocationExpressionSyntax InvocationExpression(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, Microsoft.CodeAnalysis.CSharp.Syntax.ArgumentListSyntax argumentList)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (argumentList == null)
			{
				throw new ArgumentNullException("argumentList");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.InvocationExpressionSyntax)SyntaxFactory.InvocationExpression((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ArgumentListSyntax)argumentList.Green).CreateRed();
		}

		// Token: 0x060029B3 RID: 10675 RVA: 0x000E01C0 File Offset: 0x000DE3C0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.InvocationExpressionSyntax InvocationExpression(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			return SyntaxFactory.InvocationExpression(expression, SyntaxFactory.ArgumentList(default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ArgumentSyntax>)));
		}

		// Token: 0x060029B4 RID: 10676 RVA: 0x000E01E4 File Offset: 0x000DE3E4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ElementAccessExpressionSyntax ElementAccessExpression(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, Microsoft.CodeAnalysis.CSharp.Syntax.BracketedArgumentListSyntax argumentList)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (argumentList == null)
			{
				throw new ArgumentNullException("argumentList");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ElementAccessExpressionSyntax)SyntaxFactory.ElementAccessExpression((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.BracketedArgumentListSyntax)argumentList.Green).CreateRed();
		}

		// Token: 0x060029B5 RID: 10677 RVA: 0x000E0234 File Offset: 0x000DE434
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ElementAccessExpressionSyntax ElementAccessExpression(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			return SyntaxFactory.ElementAccessExpression(expression, SyntaxFactory.BracketedArgumentList(default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ArgumentSyntax>)));
		}

		// Token: 0x060029B6 RID: 10678 RVA: 0x000E0258 File Offset: 0x000DE458
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ArgumentListSyntax ArgumentList(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ArgumentSyntax> arguments, SyntaxToken closeParenToken)
		{
			if (openParenToken.Kind() != SyntaxKind.OpenParenToken)
			{
				throw new ArgumentException("openParenToken");
			}
			if (closeParenToken.Kind() != SyntaxKind.CloseParenToken)
			{
				throw new ArgumentException("closeParenToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ArgumentListSyntax)SyntaxFactory.ArgumentList((SyntaxToken)openParenToken.Node, arguments.Node.ToGreenSeparatedList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ArgumentSyntax>(), (SyntaxToken)closeParenToken.Node).CreateRed();
		}

		// Token: 0x060029B7 RID: 10679 RVA: 0x000E02C8 File Offset: 0x000DE4C8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ArgumentListSyntax ArgumentList([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ArgumentSyntax> arguments = default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ArgumentSyntax>))
		{
			return SyntaxFactory.ArgumentList(SyntaxFactory.Token(SyntaxKind.OpenParenToken), arguments, SyntaxFactory.Token(SyntaxKind.CloseParenToken));
		}

		// Token: 0x060029B8 RID: 10680 RVA: 0x000E02E4 File Offset: 0x000DE4E4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.BracketedArgumentListSyntax BracketedArgumentList(SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ArgumentSyntax> arguments, SyntaxToken closeBracketToken)
		{
			if (openBracketToken.Kind() != SyntaxKind.OpenBracketToken)
			{
				throw new ArgumentException("openBracketToken");
			}
			if (closeBracketToken.Kind() != SyntaxKind.CloseBracketToken)
			{
				throw new ArgumentException("closeBracketToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.BracketedArgumentListSyntax)SyntaxFactory.BracketedArgumentList((SyntaxToken)openBracketToken.Node, arguments.Node.ToGreenSeparatedList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ArgumentSyntax>(), (SyntaxToken)closeBracketToken.Node).CreateRed();
		}

		// Token: 0x060029B9 RID: 10681 RVA: 0x000E0354 File Offset: 0x000DE554
		public static Microsoft.CodeAnalysis.CSharp.Syntax.BracketedArgumentListSyntax BracketedArgumentList([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ArgumentSyntax> arguments = default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ArgumentSyntax>))
		{
			return SyntaxFactory.BracketedArgumentList(SyntaxFactory.Token(SyntaxKind.OpenBracketToken), arguments, SyntaxFactory.Token(SyntaxKind.CloseBracketToken));
		}

		// Token: 0x060029BA RID: 10682 RVA: 0x000E0370 File Offset: 0x000DE570
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ArgumentSyntax Argument([global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.NameColonSyntax nameColon, SyntaxToken refKindKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			SyntaxKind syntaxKind = refKindKeyword.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind - SyntaxKind.RefKeyword > 2)
			{
				throw new ArgumentException("refKindKeyword");
			}
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ArgumentSyntax)SyntaxFactory.Argument((nameColon == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.NameColonSyntax)nameColon.Green), (SyntaxToken)refKindKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green).CreateRed();
		}

		// Token: 0x060029BB RID: 10683 RVA: 0x000E03E4 File Offset: 0x000DE5E4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ArgumentSyntax Argument(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			return SyntaxFactory.Argument(null, default(SyntaxToken), expression);
		}

		// Token: 0x060029BC RID: 10684 RVA: 0x000E0404 File Offset: 0x000DE604
		public static Microsoft.CodeAnalysis.CSharp.Syntax.NameColonSyntax NameColon(Microsoft.CodeAnalysis.CSharp.Syntax.IdentifierNameSyntax name, SyntaxToken colonToken)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (colonToken.Kind() != SyntaxKind.ColonToken)
			{
				throw new ArgumentException("colonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.NameColonSyntax)SyntaxFactory.NameColon((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.IdentifierNameSyntax)name.Green, (SyntaxToken)colonToken.Node).CreateRed();
		}

		// Token: 0x060029BD RID: 10685 RVA: 0x000E045D File Offset: 0x000DE65D
		public static Microsoft.CodeAnalysis.CSharp.Syntax.NameColonSyntax NameColon(Microsoft.CodeAnalysis.CSharp.Syntax.IdentifierNameSyntax name)
		{
			return SyntaxFactory.NameColon(name, SyntaxFactory.Token(SyntaxKind.ColonToken));
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x000E046F File Offset: 0x000DE66F
		public static Microsoft.CodeAnalysis.CSharp.Syntax.NameColonSyntax NameColon(string name)
		{
			return SyntaxFactory.NameColon(SyntaxFactory.IdentifierName(name), SyntaxFactory.Token(SyntaxKind.ColonToken));
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x000E0488 File Offset: 0x000DE688
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DeclarationExpressionSyntax DeclarationExpression(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, Microsoft.CodeAnalysis.CSharp.Syntax.VariableDesignationSyntax designation)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (designation == null)
			{
				throw new ArgumentNullException("designation");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.DeclarationExpressionSyntax)SyntaxFactory.DeclarationExpression((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)type.Green, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.VariableDesignationSyntax)designation.Green).CreateRed();
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x000E04D8 File Offset: 0x000DE6D8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.CastExpressionSyntax CastExpression(SyntaxToken openParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, SyntaxToken closeParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			if (openParenToken.Kind() != SyntaxKind.OpenParenToken)
			{
				throw new ArgumentException("openParenToken");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (closeParenToken.Kind() != SyntaxKind.CloseParenToken)
			{
				throw new ArgumentException("closeParenToken");
			}
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.CastExpressionSyntax)SyntaxFactory.CastExpression((SyntaxToken)openParenToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)type.Green, (SyntaxToken)closeParenToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green).CreateRed();
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x000E056E File Offset: 0x000DE76E
		public static Microsoft.CodeAnalysis.CSharp.Syntax.CastExpressionSyntax CastExpression(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			return SyntaxFactory.CastExpression(SyntaxFactory.Token(SyntaxKind.OpenParenToken), type, SyntaxFactory.Token(SyntaxKind.CloseParenToken), expression);
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x000E058C File Offset: 0x000DE78C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AnonymousMethodExpressionSyntax AnonymousMethodExpression(SyntaxTokenList modifiers, SyntaxToken delegateKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expressionBody)
		{
			if (delegateKeyword.Kind() != SyntaxKind.DelegateKeyword)
			{
				throw new ArgumentException("delegateKeyword");
			}
			if (block == null)
			{
				throw new ArgumentNullException("block");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.AnonymousMethodExpressionSyntax)SyntaxFactory.AnonymousMethodExpression(modifiers.Node.ToGreenList<SyntaxToken>(), (SyntaxToken)delegateKeyword.Node, (parameterList == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ParameterListSyntax)parameterList.Green), (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.BlockSyntax)block.Green, (expressionBody == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expressionBody.Green)).CreateRed();
		}

		// Token: 0x060029C3 RID: 10691 RVA: 0x000E0618 File Offset: 0x000DE818
		public static Microsoft.CodeAnalysis.CSharp.Syntax.SimpleLambdaExpressionSyntax SimpleLambdaExpression([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax parameter, SyntaxToken arrowToken, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expressionBody)
		{
			if (parameter == null)
			{
				throw new ArgumentNullException("parameter");
			}
			if (arrowToken.Kind() != SyntaxKind.EqualsGreaterThanToken)
			{
				throw new ArgumentException("arrowToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.SimpleLambdaExpressionSyntax)SyntaxFactory.SimpleLambdaExpression(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), modifiers.Node.ToGreenList<SyntaxToken>(), (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ParameterSyntax)parameter.Green, (SyntaxToken)arrowToken.Node, (block == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.BlockSyntax)block.Green), (expressionBody == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expressionBody.Green)).CreateRed();
		}

		// Token: 0x060029C4 RID: 10692 RVA: 0x000E06AF File Offset: 0x000DE8AF
		public static Microsoft.CodeAnalysis.CSharp.Syntax.SimpleLambdaExpressionSyntax SimpleLambdaExpression([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax parameter, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expressionBody)
		{
			return SyntaxFactory.SimpleLambdaExpression(attributeLists, modifiers, parameter, SyntaxFactory.Token(SyntaxKind.EqualsGreaterThanToken), block, expressionBody);
		}

		// Token: 0x060029C5 RID: 10693 RVA: 0x000E06C8 File Offset: 0x000DE8C8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.SimpleLambdaExpressionSyntax SimpleLambdaExpression(Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax parameter)
		{
			return SyntaxFactory.SimpleLambdaExpression(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), parameter, SyntaxFactory.Token(SyntaxKind.EqualsGreaterThanToken), null, null);
		}

		// Token: 0x060029C6 RID: 10694 RVA: 0x000E06FC File Offset: 0x000DE8FC
		public static Microsoft.CodeAnalysis.CSharp.Syntax.RefExpressionSyntax RefExpression(SyntaxToken refKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			if (refKeyword.Kind() != SyntaxKind.RefKeyword)
			{
				throw new ArgumentException("refKeyword");
			}
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.RefExpressionSyntax)SyntaxFactory.RefExpression((SyntaxToken)refKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green).CreateRed();
		}

		// Token: 0x060029C7 RID: 10695 RVA: 0x000E0755 File Offset: 0x000DE955
		public static Microsoft.CodeAnalysis.CSharp.Syntax.RefExpressionSyntax RefExpression(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			return SyntaxFactory.RefExpression(SyntaxFactory.Token(SyntaxKind.RefKeyword), expression);
		}

		// Token: 0x060029C8 RID: 10696 RVA: 0x000E0768 File Offset: 0x000DE968
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ParenthesizedLambdaExpressionSyntax ParenthesizedLambdaExpression([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, SyntaxToken arrowToken, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expressionBody)
		{
			if (parameterList == null)
			{
				throw new ArgumentNullException("parameterList");
			}
			if (arrowToken.Kind() != SyntaxKind.EqualsGreaterThanToken)
			{
				throw new ArgumentException("arrowToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ParenthesizedLambdaExpressionSyntax)SyntaxFactory.ParenthesizedLambdaExpression(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), modifiers.Node.ToGreenList<SyntaxToken>(), (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ParameterListSyntax)parameterList.Green, (SyntaxToken)arrowToken.Node, (block == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.BlockSyntax)block.Green), (expressionBody == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expressionBody.Green)).CreateRed();
		}

		// Token: 0x060029C9 RID: 10697 RVA: 0x000E07FF File Offset: 0x000DE9FF
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ParenthesizedLambdaExpressionSyntax ParenthesizedLambdaExpression([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expressionBody)
		{
			return SyntaxFactory.ParenthesizedLambdaExpression(attributeLists, modifiers, parameterList, SyntaxFactory.Token(SyntaxKind.EqualsGreaterThanToken), block, expressionBody);
		}

		// Token: 0x060029CA RID: 10698 RVA: 0x000E0818 File Offset: 0x000DEA18
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ParenthesizedLambdaExpressionSyntax ParenthesizedLambdaExpression()
		{
			return SyntaxFactory.ParenthesizedLambdaExpression(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), SyntaxFactory.ParameterList(default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax>)), SyntaxFactory.Token(SyntaxKind.EqualsGreaterThanToken), null, null);
		}

		// Token: 0x060029CB RID: 10699 RVA: 0x000E0858 File Offset: 0x000DEA58
		public static Microsoft.CodeAnalysis.CSharp.Syntax.InitializerExpressionSyntax InitializerExpression(SyntaxKind kind, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax> expressions, SyntaxToken closeBraceToken)
		{
			if (kind - SyntaxKind.ObjectInitializerExpression > 2 && kind != SyntaxKind.ComplexElementInitializerExpression && kind != SyntaxKind.WithInitializerExpression)
			{
				throw new ArgumentException("kind");
			}
			if (openBraceToken.Kind() != SyntaxKind.OpenBraceToken)
			{
				throw new ArgumentException("openBraceToken");
			}
			if (closeBraceToken.Kind() != SyntaxKind.CloseBraceToken)
			{
				throw new ArgumentException("closeBraceToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.InitializerExpressionSyntax)SyntaxFactory.InitializerExpression(kind, (SyntaxToken)openBraceToken.Node, expressions.Node.ToGreenSeparatedList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax>(), (SyntaxToken)closeBraceToken.Node).CreateRed();
		}

		// Token: 0x060029CC RID: 10700 RVA: 0x000E08EE File Offset: 0x000DEAEE
		public static Microsoft.CodeAnalysis.CSharp.Syntax.InitializerExpressionSyntax InitializerExpression(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax> expressions = default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax>))
		{
			return SyntaxFactory.InitializerExpression(kind, SyntaxFactory.Token(SyntaxKind.OpenBraceToken), expressions, SyntaxFactory.Token(SyntaxKind.CloseBraceToken));
		}

		// Token: 0x060029CD RID: 10701 RVA: 0x000E090C File Offset: 0x000DEB0C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ImplicitObjectCreationExpressionSyntax ImplicitObjectCreationExpression(SyntaxToken newKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.ArgumentListSyntax argumentList, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.InitializerExpressionSyntax initializer)
		{
			if (newKeyword.Kind() != SyntaxKind.NewKeyword)
			{
				throw new ArgumentException("newKeyword");
			}
			if (argumentList == null)
			{
				throw new ArgumentNullException("argumentList");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ImplicitObjectCreationExpressionSyntax)SyntaxFactory.ImplicitObjectCreationExpression((SyntaxToken)newKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ArgumentListSyntax)argumentList.Green, (initializer == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.InitializerExpressionSyntax)initializer.Green)).CreateRed();
		}

		// Token: 0x060029CE RID: 10702 RVA: 0x000E0976 File Offset: 0x000DEB76
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ImplicitObjectCreationExpressionSyntax ImplicitObjectCreationExpression(Microsoft.CodeAnalysis.CSharp.Syntax.ArgumentListSyntax argumentList, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.InitializerExpressionSyntax initializer)
		{
			return SyntaxFactory.ImplicitObjectCreationExpression(SyntaxFactory.Token(SyntaxKind.NewKeyword), argumentList, initializer);
		}

		// Token: 0x060029CF RID: 10703 RVA: 0x000E098C File Offset: 0x000DEB8C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ImplicitObjectCreationExpressionSyntax ImplicitObjectCreationExpression()
		{
			return SyntaxFactory.ImplicitObjectCreationExpression(SyntaxFactory.Token(SyntaxKind.NewKeyword), SyntaxFactory.ArgumentList(default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ArgumentSyntax>)), null);
		}

		// Token: 0x060029D0 RID: 10704 RVA: 0x000E09B8 File Offset: 0x000DEBB8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ObjectCreationExpressionSyntax ObjectCreationExpression(SyntaxToken newKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ArgumentListSyntax argumentList, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.InitializerExpressionSyntax initializer)
		{
			if (newKeyword.Kind() != SyntaxKind.NewKeyword)
			{
				throw new ArgumentException("newKeyword");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ObjectCreationExpressionSyntax)SyntaxFactory.ObjectCreationExpression((SyntaxToken)newKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)type.Green, (argumentList == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ArgumentListSyntax)argumentList.Green), (initializer == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.InitializerExpressionSyntax)initializer.Green)).CreateRed();
		}

		// Token: 0x060029D1 RID: 10705 RVA: 0x000E0A33 File Offset: 0x000DEC33
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ObjectCreationExpressionSyntax ObjectCreationExpression(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ArgumentListSyntax argumentList, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.InitializerExpressionSyntax initializer)
		{
			return SyntaxFactory.ObjectCreationExpression(SyntaxFactory.Token(SyntaxKind.NewKeyword), type, argumentList, initializer);
		}

		// Token: 0x060029D2 RID: 10706 RVA: 0x000E0A47 File Offset: 0x000DEC47
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ObjectCreationExpressionSyntax ObjectCreationExpression(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type)
		{
			return SyntaxFactory.ObjectCreationExpression(SyntaxFactory.Token(SyntaxKind.NewKeyword), type, null, null);
		}

		// Token: 0x060029D3 RID: 10707 RVA: 0x000E0A5C File Offset: 0x000DEC5C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.WithExpressionSyntax WithExpression(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SyntaxToken withKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.InitializerExpressionSyntax initializer)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (withKeyword.Kind() != SyntaxKind.WithKeyword)
			{
				throw new ArgumentException("withKeyword");
			}
			if (initializer == null)
			{
				throw new ArgumentNullException("initializer");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.WithExpressionSyntax)SyntaxFactory.WithExpression((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green, (SyntaxToken)withKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.InitializerExpressionSyntax)initializer.Green).CreateRed();
		}

		// Token: 0x060029D4 RID: 10708 RVA: 0x000E0ACE File Offset: 0x000DECCE
		public static Microsoft.CodeAnalysis.CSharp.Syntax.WithExpressionSyntax WithExpression(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, Microsoft.CodeAnalysis.CSharp.Syntax.InitializerExpressionSyntax initializer)
		{
			return SyntaxFactory.WithExpression(expression, SyntaxFactory.Token(SyntaxKind.WithKeyword), initializer);
		}

		// Token: 0x060029D5 RID: 10709 RVA: 0x000E0AE1 File Offset: 0x000DECE1
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AnonymousObjectMemberDeclaratorSyntax AnonymousObjectMemberDeclarator([global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.NameEqualsSyntax nameEquals, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.AnonymousObjectMemberDeclaratorSyntax)SyntaxFactory.AnonymousObjectMemberDeclarator((nameEquals == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.NameEqualsSyntax)nameEquals.Green), (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green).CreateRed();
		}

		// Token: 0x060029D6 RID: 10710 RVA: 0x000E0B1C File Offset: 0x000DED1C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AnonymousObjectMemberDeclaratorSyntax AnonymousObjectMemberDeclarator(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			return SyntaxFactory.AnonymousObjectMemberDeclarator(null, expression);
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x000E0B28 File Offset: 0x000DED28
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AnonymousObjectCreationExpressionSyntax AnonymousObjectCreationExpression(SyntaxToken newKeyword, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AnonymousObjectMemberDeclaratorSyntax> initializers, SyntaxToken closeBraceToken)
		{
			if (newKeyword.Kind() != SyntaxKind.NewKeyword)
			{
				throw new ArgumentException("newKeyword");
			}
			if (openBraceToken.Kind() != SyntaxKind.OpenBraceToken)
			{
				throw new ArgumentException("openBraceToken");
			}
			if (closeBraceToken.Kind() != SyntaxKind.CloseBraceToken)
			{
				throw new ArgumentException("closeBraceToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.AnonymousObjectCreationExpressionSyntax)SyntaxFactory.AnonymousObjectCreationExpression((SyntaxToken)newKeyword.Node, (SyntaxToken)openBraceToken.Node, initializers.Node.ToGreenSeparatedList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AnonymousObjectMemberDeclaratorSyntax>(), (SyntaxToken)closeBraceToken.Node).CreateRed();
		}

		// Token: 0x060029D8 RID: 10712 RVA: 0x000E0BBC File Offset: 0x000DEDBC
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AnonymousObjectCreationExpressionSyntax AnonymousObjectCreationExpression([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AnonymousObjectMemberDeclaratorSyntax> initializers = default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AnonymousObjectMemberDeclaratorSyntax>))
		{
			return SyntaxFactory.AnonymousObjectCreationExpression(SyntaxFactory.Token(SyntaxKind.NewKeyword), SyntaxFactory.Token(SyntaxKind.OpenBraceToken), initializers, SyntaxFactory.Token(SyntaxKind.CloseBraceToken));
		}

		// Token: 0x060029D9 RID: 10713 RVA: 0x000E0BE4 File Offset: 0x000DEDE4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ArrayCreationExpressionSyntax ArrayCreationExpression(SyntaxToken newKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.ArrayTypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.InitializerExpressionSyntax initializer)
		{
			if (newKeyword.Kind() != SyntaxKind.NewKeyword)
			{
				throw new ArgumentException("newKeyword");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ArrayCreationExpressionSyntax)SyntaxFactory.ArrayCreationExpression((SyntaxToken)newKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ArrayTypeSyntax)type.Green, (initializer == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.InitializerExpressionSyntax)initializer.Green)).CreateRed();
		}

		// Token: 0x060029DA RID: 10714 RVA: 0x000E0C4E File Offset: 0x000DEE4E
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ArrayCreationExpressionSyntax ArrayCreationExpression(Microsoft.CodeAnalysis.CSharp.Syntax.ArrayTypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.InitializerExpressionSyntax initializer)
		{
			return SyntaxFactory.ArrayCreationExpression(SyntaxFactory.Token(SyntaxKind.NewKeyword), type, initializer);
		}

		// Token: 0x060029DB RID: 10715 RVA: 0x000E0C61 File Offset: 0x000DEE61
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ArrayCreationExpressionSyntax ArrayCreationExpression(Microsoft.CodeAnalysis.CSharp.Syntax.ArrayTypeSyntax type)
		{
			return SyntaxFactory.ArrayCreationExpression(SyntaxFactory.Token(SyntaxKind.NewKeyword), type, null);
		}

		// Token: 0x060029DC RID: 10716 RVA: 0x000E0C74 File Offset: 0x000DEE74
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ImplicitArrayCreationExpressionSyntax ImplicitArrayCreationExpression(SyntaxToken newKeyword, SyntaxToken openBracketToken, SyntaxTokenList commas, SyntaxToken closeBracketToken, Microsoft.CodeAnalysis.CSharp.Syntax.InitializerExpressionSyntax initializer)
		{
			if (newKeyword.Kind() != SyntaxKind.NewKeyword)
			{
				throw new ArgumentException("newKeyword");
			}
			if (openBracketToken.Kind() != SyntaxKind.OpenBracketToken)
			{
				throw new ArgumentException("openBracketToken");
			}
			if (closeBracketToken.Kind() != SyntaxKind.CloseBracketToken)
			{
				throw new ArgumentException("closeBracketToken");
			}
			if (initializer == null)
			{
				throw new ArgumentNullException("initializer");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ImplicitArrayCreationExpressionSyntax)SyntaxFactory.ImplicitArrayCreationExpression((SyntaxToken)newKeyword.Node, (SyntaxToken)openBracketToken.Node, commas.Node.ToGreenList<SyntaxToken>(), (SyntaxToken)closeBracketToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.InitializerExpressionSyntax)initializer.Green).CreateRed();
		}

		// Token: 0x060029DD RID: 10717 RVA: 0x000E0D23 File Offset: 0x000DEF23
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ImplicitArrayCreationExpressionSyntax ImplicitArrayCreationExpression(SyntaxTokenList commas, Microsoft.CodeAnalysis.CSharp.Syntax.InitializerExpressionSyntax initializer)
		{
			return SyntaxFactory.ImplicitArrayCreationExpression(SyntaxFactory.Token(SyntaxKind.NewKeyword), SyntaxFactory.Token(SyntaxKind.OpenBracketToken), commas, SyntaxFactory.Token(SyntaxKind.CloseBracketToken), initializer);
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x000E0D4C File Offset: 0x000DEF4C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ImplicitArrayCreationExpressionSyntax ImplicitArrayCreationExpression(Microsoft.CodeAnalysis.CSharp.Syntax.InitializerExpressionSyntax initializer)
		{
			return SyntaxFactory.ImplicitArrayCreationExpression(SyntaxFactory.Token(SyntaxKind.NewKeyword), SyntaxFactory.Token(SyntaxKind.OpenBracketToken), default(SyntaxTokenList), SyntaxFactory.Token(SyntaxKind.CloseBracketToken), initializer);
		}

		// Token: 0x060029DF RID: 10719 RVA: 0x000E0D88 File Offset: 0x000DEF88
		public static Microsoft.CodeAnalysis.CSharp.Syntax.StackAllocArrayCreationExpressionSyntax StackAllocArrayCreationExpression(SyntaxToken stackAllocKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.InitializerExpressionSyntax initializer)
		{
			if (stackAllocKeyword.Kind() != SyntaxKind.StackAllocKeyword)
			{
				throw new ArgumentException("stackAllocKeyword");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.StackAllocArrayCreationExpressionSyntax)SyntaxFactory.StackAllocArrayCreationExpression((SyntaxToken)stackAllocKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)type.Green, (initializer == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.InitializerExpressionSyntax)initializer.Green)).CreateRed();
		}

		// Token: 0x060029E0 RID: 10720 RVA: 0x000E0DF2 File Offset: 0x000DEFF2
		public static Microsoft.CodeAnalysis.CSharp.Syntax.StackAllocArrayCreationExpressionSyntax StackAllocArrayCreationExpression(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.InitializerExpressionSyntax initializer)
		{
			return SyntaxFactory.StackAllocArrayCreationExpression(SyntaxFactory.Token(SyntaxKind.StackAllocKeyword), type, initializer);
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x000E0E05 File Offset: 0x000DF005
		public static Microsoft.CodeAnalysis.CSharp.Syntax.StackAllocArrayCreationExpressionSyntax StackAllocArrayCreationExpression(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type)
		{
			return SyntaxFactory.StackAllocArrayCreationExpression(SyntaxFactory.Token(SyntaxKind.StackAllocKeyword), type, null);
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x000E0E18 File Offset: 0x000DF018
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ImplicitStackAllocArrayCreationExpressionSyntax ImplicitStackAllocArrayCreationExpression(SyntaxToken stackAllocKeyword, SyntaxToken openBracketToken, SyntaxToken closeBracketToken, Microsoft.CodeAnalysis.CSharp.Syntax.InitializerExpressionSyntax initializer)
		{
			if (stackAllocKeyword.Kind() != SyntaxKind.StackAllocKeyword)
			{
				throw new ArgumentException("stackAllocKeyword");
			}
			if (openBracketToken.Kind() != SyntaxKind.OpenBracketToken)
			{
				throw new ArgumentException("openBracketToken");
			}
			if (closeBracketToken.Kind() != SyntaxKind.CloseBracketToken)
			{
				throw new ArgumentException("closeBracketToken");
			}
			if (initializer == null)
			{
				throw new ArgumentNullException("initializer");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ImplicitStackAllocArrayCreationExpressionSyntax)SyntaxFactory.ImplicitStackAllocArrayCreationExpression((SyntaxToken)stackAllocKeyword.Node, (SyntaxToken)openBracketToken.Node, (SyntaxToken)closeBracketToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.InitializerExpressionSyntax)initializer.Green).CreateRed();
		}

		// Token: 0x060029E3 RID: 10723 RVA: 0x000E0EB9 File Offset: 0x000DF0B9
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ImplicitStackAllocArrayCreationExpressionSyntax ImplicitStackAllocArrayCreationExpression(Microsoft.CodeAnalysis.CSharp.Syntax.InitializerExpressionSyntax initializer)
		{
			return SyntaxFactory.ImplicitStackAllocArrayCreationExpression(SyntaxFactory.Token(SyntaxKind.StackAllocKeyword), SyntaxFactory.Token(SyntaxKind.OpenBracketToken), SyntaxFactory.Token(SyntaxKind.CloseBracketToken), initializer);
		}

		// Token: 0x060029E4 RID: 10724 RVA: 0x000E0EE0 File Offset: 0x000DF0E0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.QueryExpressionSyntax QueryExpression(Microsoft.CodeAnalysis.CSharp.Syntax.FromClauseSyntax fromClause, Microsoft.CodeAnalysis.CSharp.Syntax.QueryBodySyntax body)
		{
			if (fromClause == null)
			{
				throw new ArgumentNullException("fromClause");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.QueryExpressionSyntax)SyntaxFactory.QueryExpression((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.FromClauseSyntax)fromClause.Green, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.QueryBodySyntax)body.Green).CreateRed();
		}

		// Token: 0x060029E5 RID: 10725 RVA: 0x000E0F30 File Offset: 0x000DF130
		public static Microsoft.CodeAnalysis.CSharp.Syntax.QueryBodySyntax QueryBody([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.QueryClauseSyntax> clauses, Microsoft.CodeAnalysis.CSharp.Syntax.SelectOrGroupClauseSyntax selectOrGroup, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.QueryContinuationSyntax continuation)
		{
			if (selectOrGroup == null)
			{
				throw new ArgumentNullException("selectOrGroup");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.QueryBodySyntax)SyntaxFactory.QueryBody(clauses.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.QueryClauseSyntax>(), (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.SelectOrGroupClauseSyntax)selectOrGroup.Green, (continuation == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.QueryContinuationSyntax)continuation.Green)).CreateRed();
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x000E0F84 File Offset: 0x000DF184
		public static Microsoft.CodeAnalysis.CSharp.Syntax.QueryBodySyntax QueryBody(Microsoft.CodeAnalysis.CSharp.Syntax.SelectOrGroupClauseSyntax selectOrGroup)
		{
			return SyntaxFactory.QueryBody(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.QueryClauseSyntax>), selectOrGroup, null);
		}

		// Token: 0x060029E7 RID: 10727 RVA: 0x000E0FA4 File Offset: 0x000DF1A4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.FromClauseSyntax FromClause(SyntaxToken fromKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			if (fromKeyword.Kind() != SyntaxKind.FromKeyword)
			{
				throw new ArgumentException("fromKeyword");
			}
			if (identifier.Kind() != SyntaxKind.IdentifierToken)
			{
				throw new ArgumentException("identifier");
			}
			if (inKeyword.Kind() != SyntaxKind.InKeyword)
			{
				throw new ArgumentException("inKeyword");
			}
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.FromClauseSyntax)SyntaxFactory.FromClause((SyntaxToken)fromKeyword.Node, (type == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)type.Green), (SyntaxToken)identifier.Node, (SyntaxToken)inKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green).CreateRed();
		}

		// Token: 0x060029E8 RID: 10728 RVA: 0x000E1058 File Offset: 0x000DF258
		public static Microsoft.CodeAnalysis.CSharp.Syntax.FromClauseSyntax FromClause([global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			return SyntaxFactory.FromClause(SyntaxFactory.Token(SyntaxKind.FromKeyword), type, identifier, SyntaxFactory.Token(SyntaxKind.InKeyword), expression);
		}

		// Token: 0x060029E9 RID: 10729 RVA: 0x000E1076 File Offset: 0x000DF276
		public static Microsoft.CodeAnalysis.CSharp.Syntax.FromClauseSyntax FromClause(SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			return SyntaxFactory.FromClause(SyntaxFactory.Token(SyntaxKind.FromKeyword), null, identifier, SyntaxFactory.Token(SyntaxKind.InKeyword), expression);
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x000E1094 File Offset: 0x000DF294
		public static Microsoft.CodeAnalysis.CSharp.Syntax.FromClauseSyntax FromClause(string identifier, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			return SyntaxFactory.FromClause(SyntaxFactory.Token(SyntaxKind.FromKeyword), null, SyntaxFactory.Identifier(identifier), SyntaxFactory.Token(SyntaxKind.InKeyword), expression);
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x000E10B8 File Offset: 0x000DF2B8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.LetClauseSyntax LetClause(SyntaxToken letKeyword, SyntaxToken identifier, SyntaxToken equalsToken, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			if (letKeyword.Kind() != SyntaxKind.LetKeyword)
			{
				throw new ArgumentException("letKeyword");
			}
			if (identifier.Kind() != SyntaxKind.IdentifierToken)
			{
				throw new ArgumentException("identifier");
			}
			if (equalsToken.Kind() != SyntaxKind.EqualsToken)
			{
				throw new ArgumentException("equalsToken");
			}
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.LetClauseSyntax)SyntaxFactory.LetClause((SyntaxToken)letKeyword.Node, (SyntaxToken)identifier.Node, (SyntaxToken)equalsToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green).CreateRed();
		}

		// Token: 0x060029EC RID: 10732 RVA: 0x000E1159 File Offset: 0x000DF359
		public static Microsoft.CodeAnalysis.CSharp.Syntax.LetClauseSyntax LetClause(SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			return SyntaxFactory.LetClause(SyntaxFactory.Token(SyntaxKind.LetKeyword), identifier, SyntaxFactory.Token(SyntaxKind.EqualsToken), expression);
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x000E1176 File Offset: 0x000DF376
		public static Microsoft.CodeAnalysis.CSharp.Syntax.LetClauseSyntax LetClause(string identifier, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			return SyntaxFactory.LetClause(SyntaxFactory.Token(SyntaxKind.LetKeyword), SyntaxFactory.Identifier(identifier), SyntaxFactory.Token(SyntaxKind.EqualsToken), expression);
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x000E1198 File Offset: 0x000DF398
		public static Microsoft.CodeAnalysis.CSharp.Syntax.JoinClauseSyntax JoinClause(SyntaxToken joinKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax inExpression, SyntaxToken onKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax leftExpression, SyntaxToken equalsKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax rightExpression, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.JoinIntoClauseSyntax into)
		{
			if (joinKeyword.Kind() != SyntaxKind.JoinKeyword)
			{
				throw new ArgumentException("joinKeyword");
			}
			if (identifier.Kind() != SyntaxKind.IdentifierToken)
			{
				throw new ArgumentException("identifier");
			}
			if (inKeyword.Kind() != SyntaxKind.InKeyword)
			{
				throw new ArgumentException("inKeyword");
			}
			if (inExpression == null)
			{
				throw new ArgumentNullException("inExpression");
			}
			if (onKeyword.Kind() != SyntaxKind.OnKeyword)
			{
				throw new ArgumentException("onKeyword");
			}
			if (leftExpression == null)
			{
				throw new ArgumentNullException("leftExpression");
			}
			if (equalsKeyword.Kind() != SyntaxKind.EqualsKeyword)
			{
				throw new ArgumentException("equalsKeyword");
			}
			if (rightExpression == null)
			{
				throw new ArgumentNullException("rightExpression");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.JoinClauseSyntax)SyntaxFactory.JoinClause((SyntaxToken)joinKeyword.Node, (type == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)type.Green), (SyntaxToken)identifier.Node, (SyntaxToken)inKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)inExpression.Green, (SyntaxToken)onKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)leftExpression.Green, (SyntaxToken)equalsKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)rightExpression.Green, (into == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.JoinIntoClauseSyntax)into.Green)).CreateRed();
		}

		// Token: 0x060029EF RID: 10735 RVA: 0x000E12E0 File Offset: 0x000DF4E0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.JoinClauseSyntax JoinClause([global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax inExpression, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax leftExpression, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax rightExpression, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.JoinIntoClauseSyntax into)
		{
			return SyntaxFactory.JoinClause(SyntaxFactory.Token(SyntaxKind.JoinKeyword), type, identifier, SyntaxFactory.Token(SyntaxKind.InKeyword), inExpression, SyntaxFactory.Token(SyntaxKind.OnKeyword), leftExpression, SyntaxFactory.Token(SyntaxKind.EqualsKeyword), rightExpression, into);
		}

		// Token: 0x060029F0 RID: 10736 RVA: 0x000E1324 File Offset: 0x000DF524
		public static Microsoft.CodeAnalysis.CSharp.Syntax.JoinClauseSyntax JoinClause(SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax inExpression, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax leftExpression, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax rightExpression)
		{
			return SyntaxFactory.JoinClause(SyntaxFactory.Token(SyntaxKind.JoinKeyword), null, identifier, SyntaxFactory.Token(SyntaxKind.InKeyword), inExpression, SyntaxFactory.Token(SyntaxKind.OnKeyword), leftExpression, SyntaxFactory.Token(SyntaxKind.EqualsKeyword), rightExpression, null);
		}

		// Token: 0x060029F1 RID: 10737 RVA: 0x000E1364 File Offset: 0x000DF564
		public static Microsoft.CodeAnalysis.CSharp.Syntax.JoinClauseSyntax JoinClause(string identifier, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax inExpression, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax leftExpression, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax rightExpression)
		{
			return SyntaxFactory.JoinClause(SyntaxFactory.Token(SyntaxKind.JoinKeyword), null, SyntaxFactory.Identifier(identifier), SyntaxFactory.Token(SyntaxKind.InKeyword), inExpression, SyntaxFactory.Token(SyntaxKind.OnKeyword), leftExpression, SyntaxFactory.Token(SyntaxKind.EqualsKeyword), rightExpression, null);
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x000E13AC File Offset: 0x000DF5AC
		public static Microsoft.CodeAnalysis.CSharp.Syntax.JoinIntoClauseSyntax JoinIntoClause(SyntaxToken intoKeyword, SyntaxToken identifier)
		{
			if (intoKeyword.Kind() != SyntaxKind.IntoKeyword)
			{
				throw new ArgumentException("intoKeyword");
			}
			if (identifier.Kind() != SyntaxKind.IdentifierToken)
			{
				throw new ArgumentException("identifier");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.JoinIntoClauseSyntax)SyntaxFactory.JoinIntoClause((SyntaxToken)intoKeyword.Node, (SyntaxToken)identifier.Node).CreateRed();
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x000E1410 File Offset: 0x000DF610
		public static Microsoft.CodeAnalysis.CSharp.Syntax.JoinIntoClauseSyntax JoinIntoClause(SyntaxToken identifier)
		{
			return SyntaxFactory.JoinIntoClause(SyntaxFactory.Token(SyntaxKind.IntoKeyword), identifier);
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x000E1422 File Offset: 0x000DF622
		public static Microsoft.CodeAnalysis.CSharp.Syntax.JoinIntoClauseSyntax JoinIntoClause(string identifier)
		{
			return SyntaxFactory.JoinIntoClause(SyntaxFactory.Token(SyntaxKind.IntoKeyword), SyntaxFactory.Identifier(identifier));
		}

		// Token: 0x060029F5 RID: 10741 RVA: 0x000E143C File Offset: 0x000DF63C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.WhereClauseSyntax WhereClause(SyntaxToken whereKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax condition)
		{
			if (whereKeyword.Kind() != SyntaxKind.WhereKeyword)
			{
				throw new ArgumentException("whereKeyword");
			}
			if (condition == null)
			{
				throw new ArgumentNullException("condition");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.WhereClauseSyntax)SyntaxFactory.WhereClause((SyntaxToken)whereKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)condition.Green).CreateRed();
		}

		// Token: 0x060029F6 RID: 10742 RVA: 0x000E1495 File Offset: 0x000DF695
		public static Microsoft.CodeAnalysis.CSharp.Syntax.WhereClauseSyntax WhereClause(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax condition)
		{
			return SyntaxFactory.WhereClause(SyntaxFactory.Token(SyntaxKind.WhereKeyword), condition);
		}

		// Token: 0x060029F7 RID: 10743 RVA: 0x000E14A8 File Offset: 0x000DF6A8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.OrderByClauseSyntax OrderByClause(SyntaxToken orderByKeyword, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.OrderingSyntax> orderings)
		{
			if (orderByKeyword.Kind() != SyntaxKind.OrderByKeyword)
			{
				throw new ArgumentException("orderByKeyword");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.OrderByClauseSyntax)SyntaxFactory.OrderByClause((SyntaxToken)orderByKeyword.Node, orderings.Node.ToGreenSeparatedList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.OrderingSyntax>()).CreateRed();
		}

		// Token: 0x060029F8 RID: 10744 RVA: 0x000E14F4 File Offset: 0x000DF6F4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.OrderByClauseSyntax OrderByClause([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.OrderingSyntax> orderings = default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.OrderingSyntax>))
		{
			return SyntaxFactory.OrderByClause(SyntaxFactory.Token(SyntaxKind.OrderByKeyword), orderings);
		}

		// Token: 0x060029F9 RID: 10745 RVA: 0x000E1508 File Offset: 0x000DF708
		public static Microsoft.CodeAnalysis.CSharp.Syntax.OrderingSyntax Ordering(SyntaxKind kind, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SyntaxToken ascendingOrDescendingKeyword)
		{
			if (kind - SyntaxKind.AscendingOrdering > 1)
			{
				throw new ArgumentException("kind");
			}
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			SyntaxKind syntaxKind = ascendingOrDescendingKeyword.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind - SyntaxKind.AscendingKeyword > 1)
			{
				throw new ArgumentException("ascendingOrDescendingKeyword");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.OrderingSyntax)SyntaxFactory.Ordering(kind, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green, (SyntaxToken)ascendingOrDescendingKeyword.Node).CreateRed();
		}

		// Token: 0x060029FA RID: 10746 RVA: 0x000E1580 File Offset: 0x000DF780
		public static Microsoft.CodeAnalysis.CSharp.Syntax.OrderingSyntax Ordering(SyntaxKind kind, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			return SyntaxFactory.Ordering(kind, expression, default(SyntaxToken));
		}

		// Token: 0x060029FB RID: 10747 RVA: 0x000E15A0 File Offset: 0x000DF7A0
		private static SyntaxKind GetOrderingAscendingOrDescendingKeywordKind(SyntaxKind kind)
		{
			SyntaxKind syntaxKind;
			if (kind != SyntaxKind.AscendingOrdering)
			{
				if (kind != SyntaxKind.DescendingOrdering)
				{
					throw new ArgumentOutOfRangeException();
				}
				syntaxKind = SyntaxKind.DescendingKeyword;
			}
			else
			{
				syntaxKind = SyntaxKind.AscendingKeyword;
			}
			return syntaxKind;
		}

		// Token: 0x060029FC RID: 10748 RVA: 0x000E15D8 File Offset: 0x000DF7D8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.SelectClauseSyntax SelectClause(SyntaxToken selectKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			if (selectKeyword.Kind() != SyntaxKind.SelectKeyword)
			{
				throw new ArgumentException("selectKeyword");
			}
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.SelectClauseSyntax)SyntaxFactory.SelectClause((SyntaxToken)selectKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green).CreateRed();
		}

		// Token: 0x060029FD RID: 10749 RVA: 0x000E1631 File Offset: 0x000DF831
		public static Microsoft.CodeAnalysis.CSharp.Syntax.SelectClauseSyntax SelectClause(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			return SyntaxFactory.SelectClause(SyntaxFactory.Token(SyntaxKind.SelectKeyword), expression);
		}

		// Token: 0x060029FE RID: 10750 RVA: 0x000E1644 File Offset: 0x000DF844
		public static Microsoft.CodeAnalysis.CSharp.Syntax.GroupClauseSyntax GroupClause(SyntaxToken groupKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax groupExpression, SyntaxToken byKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax byExpression)
		{
			if (groupKeyword.Kind() != SyntaxKind.GroupKeyword)
			{
				throw new ArgumentException("groupKeyword");
			}
			if (groupExpression == null)
			{
				throw new ArgumentNullException("groupExpression");
			}
			if (byKeyword.Kind() != SyntaxKind.ByKeyword)
			{
				throw new ArgumentException("byKeyword");
			}
			if (byExpression == null)
			{
				throw new ArgumentNullException("byExpression");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.GroupClauseSyntax)SyntaxFactory.GroupClause((SyntaxToken)groupKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)groupExpression.Green, (SyntaxToken)byKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)byExpression.Green).CreateRed();
		}

		// Token: 0x060029FF RID: 10751 RVA: 0x000E16DA File Offset: 0x000DF8DA
		public static Microsoft.CodeAnalysis.CSharp.Syntax.GroupClauseSyntax GroupClause(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax groupExpression, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax byExpression)
		{
			return SyntaxFactory.GroupClause(SyntaxFactory.Token(SyntaxKind.GroupKeyword), groupExpression, SyntaxFactory.Token(SyntaxKind.ByKeyword), byExpression);
		}

		// Token: 0x06002A00 RID: 10752 RVA: 0x000E16F8 File Offset: 0x000DF8F8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.QueryContinuationSyntax QueryContinuation(SyntaxToken intoKeyword, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.QueryBodySyntax body)
		{
			if (intoKeyword.Kind() != SyntaxKind.IntoKeyword)
			{
				throw new ArgumentException("intoKeyword");
			}
			if (identifier.Kind() != SyntaxKind.IdentifierToken)
			{
				throw new ArgumentException("identifier");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.QueryContinuationSyntax)SyntaxFactory.QueryContinuation((SyntaxToken)intoKeyword.Node, (SyntaxToken)identifier.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.QueryBodySyntax)body.Green).CreateRed();
		}

		// Token: 0x06002A01 RID: 10753 RVA: 0x000E1775 File Offset: 0x000DF975
		public static Microsoft.CodeAnalysis.CSharp.Syntax.QueryContinuationSyntax QueryContinuation(SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.QueryBodySyntax body)
		{
			return SyntaxFactory.QueryContinuation(SyntaxFactory.Token(SyntaxKind.IntoKeyword), identifier, body);
		}

		// Token: 0x06002A02 RID: 10754 RVA: 0x000E1788 File Offset: 0x000DF988
		public static Microsoft.CodeAnalysis.CSharp.Syntax.QueryContinuationSyntax QueryContinuation(string identifier, Microsoft.CodeAnalysis.CSharp.Syntax.QueryBodySyntax body)
		{
			return SyntaxFactory.QueryContinuation(SyntaxFactory.Token(SyntaxKind.IntoKeyword), SyntaxFactory.Identifier(identifier), body);
		}

		// Token: 0x06002A03 RID: 10755 RVA: 0x000E17A0 File Offset: 0x000DF9A0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.OmittedArraySizeExpressionSyntax OmittedArraySizeExpression(SyntaxToken omittedArraySizeExpressionToken)
		{
			if (omittedArraySizeExpressionToken.Kind() != SyntaxKind.OmittedArraySizeExpressionToken)
			{
				throw new ArgumentException("omittedArraySizeExpressionToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.OmittedArraySizeExpressionSyntax)SyntaxFactory.OmittedArraySizeExpression((SyntaxToken)omittedArraySizeExpressionToken.Node).CreateRed();
		}

		// Token: 0x06002A04 RID: 10756 RVA: 0x000E17D5 File Offset: 0x000DF9D5
		public static Microsoft.CodeAnalysis.CSharp.Syntax.OmittedArraySizeExpressionSyntax OmittedArraySizeExpression()
		{
			return SyntaxFactory.OmittedArraySizeExpression(SyntaxFactory.Token(SyntaxKind.OmittedArraySizeExpressionToken));
		}

		// Token: 0x06002A05 RID: 10757 RVA: 0x000E17E8 File Offset: 0x000DF9E8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.InterpolatedStringExpressionSyntax InterpolatedStringExpression(SyntaxToken stringStartToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.InterpolatedStringContentSyntax> contents, SyntaxToken stringEndToken)
		{
			SyntaxKind syntaxKind = stringStartToken.Kind();
			if (syntaxKind != SyntaxKind.InterpolatedStringStartToken && syntaxKind != SyntaxKind.InterpolatedVerbatimStringStartToken)
			{
				throw new ArgumentException("stringStartToken");
			}
			if (stringEndToken.Kind() != SyntaxKind.InterpolatedStringEndToken)
			{
				throw new ArgumentException("stringEndToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.InterpolatedStringExpressionSyntax)SyntaxFactory.InterpolatedStringExpression((SyntaxToken)stringStartToken.Node, contents.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.InterpolatedStringContentSyntax>(), (SyntaxToken)stringEndToken.Node).CreateRed();
		}

		// Token: 0x06002A06 RID: 10758 RVA: 0x000E1862 File Offset: 0x000DFA62
		public static Microsoft.CodeAnalysis.CSharp.Syntax.InterpolatedStringExpressionSyntax InterpolatedStringExpression(SyntaxToken stringStartToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.InterpolatedStringContentSyntax> contents)
		{
			return SyntaxFactory.InterpolatedStringExpression(stringStartToken, contents, SyntaxFactory.Token(SyntaxKind.InterpolatedStringEndToken));
		}

		// Token: 0x06002A07 RID: 10759 RVA: 0x000E1878 File Offset: 0x000DFA78
		public static Microsoft.CodeAnalysis.CSharp.Syntax.InterpolatedStringExpressionSyntax InterpolatedStringExpression(SyntaxToken stringStartToken)
		{
			return SyntaxFactory.InterpolatedStringExpression(stringStartToken, default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.InterpolatedStringContentSyntax>), SyntaxFactory.Token(SyntaxKind.InterpolatedStringEndToken));
		}

		// Token: 0x06002A08 RID: 10760 RVA: 0x000E18A0 File Offset: 0x000DFAA0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.IsPatternExpressionSyntax IsPatternExpression(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SyntaxToken isKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.PatternSyntax pattern)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (isKeyword.Kind() != SyntaxKind.IsKeyword)
			{
				throw new ArgumentException("isKeyword");
			}
			if (pattern == null)
			{
				throw new ArgumentNullException("pattern");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.IsPatternExpressionSyntax)SyntaxFactory.IsPatternExpression((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green, (SyntaxToken)isKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.PatternSyntax)pattern.Green).CreateRed();
		}

		// Token: 0x06002A09 RID: 10761 RVA: 0x000E1912 File Offset: 0x000DFB12
		public static Microsoft.CodeAnalysis.CSharp.Syntax.IsPatternExpressionSyntax IsPatternExpression(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, Microsoft.CodeAnalysis.CSharp.Syntax.PatternSyntax pattern)
		{
			return SyntaxFactory.IsPatternExpression(expression, SyntaxFactory.Token(SyntaxKind.IsKeyword), pattern);
		}

		// Token: 0x06002A0A RID: 10762 RVA: 0x000E1928 File Offset: 0x000DFB28
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ThrowExpressionSyntax ThrowExpression(SyntaxToken throwKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			if (throwKeyword.Kind() != SyntaxKind.ThrowKeyword)
			{
				throw new ArgumentException("throwKeyword");
			}
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ThrowExpressionSyntax)SyntaxFactory.ThrowExpression((SyntaxToken)throwKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green).CreateRed();
		}

		// Token: 0x06002A0B RID: 10763 RVA: 0x000E1981 File Offset: 0x000DFB81
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ThrowExpressionSyntax ThrowExpression(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			return SyntaxFactory.ThrowExpression(SyntaxFactory.Token(SyntaxKind.ThrowKeyword), expression);
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x000E1994 File Offset: 0x000DFB94
		public static Microsoft.CodeAnalysis.CSharp.Syntax.WhenClauseSyntax WhenClause(SyntaxToken whenKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax condition)
		{
			if (whenKeyword.Kind() != SyntaxKind.WhenKeyword)
			{
				throw new ArgumentException("whenKeyword");
			}
			if (condition == null)
			{
				throw new ArgumentNullException("condition");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.WhenClauseSyntax)SyntaxFactory.WhenClause((SyntaxToken)whenKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)condition.Green).CreateRed();
		}

		// Token: 0x06002A0D RID: 10765 RVA: 0x000E19ED File Offset: 0x000DFBED
		public static Microsoft.CodeAnalysis.CSharp.Syntax.WhenClauseSyntax WhenClause(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax condition)
		{
			return SyntaxFactory.WhenClause(SyntaxFactory.Token(SyntaxKind.WhenKeyword), condition);
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x000E19FF File Offset: 0x000DFBFF
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DiscardPatternSyntax DiscardPattern(SyntaxToken underscoreToken)
		{
			if (underscoreToken.Kind() != SyntaxKind.UnderscoreToken)
			{
				throw new ArgumentException("underscoreToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.DiscardPatternSyntax)SyntaxFactory.DiscardPattern((SyntaxToken)underscoreToken.Node).CreateRed();
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x000E1A34 File Offset: 0x000DFC34
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DiscardPatternSyntax DiscardPattern()
		{
			return SyntaxFactory.DiscardPattern(SyntaxFactory.Token(SyntaxKind.UnderscoreToken));
		}

		// Token: 0x06002A10 RID: 10768 RVA: 0x000E1A48 File Offset: 0x000DFC48
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DeclarationPatternSyntax DeclarationPattern(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, Microsoft.CodeAnalysis.CSharp.Syntax.VariableDesignationSyntax designation)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (designation == null)
			{
				throw new ArgumentNullException("designation");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.DeclarationPatternSyntax)SyntaxFactory.DeclarationPattern((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)type.Green, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.VariableDesignationSyntax)designation.Green).CreateRed();
		}

		// Token: 0x06002A11 RID: 10769 RVA: 0x000E1A98 File Offset: 0x000DFC98
		public static Microsoft.CodeAnalysis.CSharp.Syntax.VarPatternSyntax VarPattern(SyntaxToken varKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.VariableDesignationSyntax designation)
		{
			if (varKeyword.Kind() != SyntaxKind.VarKeyword)
			{
				throw new ArgumentException("varKeyword");
			}
			if (designation == null)
			{
				throw new ArgumentNullException("designation");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.VarPatternSyntax)SyntaxFactory.VarPattern((SyntaxToken)varKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.VariableDesignationSyntax)designation.Green).CreateRed();
		}

		// Token: 0x06002A12 RID: 10770 RVA: 0x000E1AF1 File Offset: 0x000DFCF1
		public static Microsoft.CodeAnalysis.CSharp.Syntax.VarPatternSyntax VarPattern(Microsoft.CodeAnalysis.CSharp.Syntax.VariableDesignationSyntax designation)
		{
			return SyntaxFactory.VarPattern(SyntaxFactory.Token(SyntaxKind.VarKeyword), designation);
		}

		// Token: 0x06002A13 RID: 10771 RVA: 0x000E1B04 File Offset: 0x000DFD04
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public static Microsoft.CodeAnalysis.CSharp.Syntax.RecursivePatternSyntax RecursivePattern(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, Microsoft.CodeAnalysis.CSharp.Syntax.PositionalPatternClauseSyntax positionalPatternClause, Microsoft.CodeAnalysis.CSharp.Syntax.PropertyPatternClauseSyntax propertyPatternClause, Microsoft.CodeAnalysis.CSharp.Syntax.VariableDesignationSyntax designation)
		{
			return (Microsoft.CodeAnalysis.CSharp.Syntax.RecursivePatternSyntax)SyntaxFactory.RecursivePattern((type == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)type.Green), (positionalPatternClause == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.PositionalPatternClauseSyntax)positionalPatternClause.Green), (propertyPatternClause == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.PropertyPatternClauseSyntax)propertyPatternClause.Green), (designation == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.VariableDesignationSyntax)designation.Green)).CreateRed();
		}

		// Token: 0x06002A14 RID: 10772 RVA: 0x000E1B64 File Offset: 0x000DFD64
		public static Microsoft.CodeAnalysis.CSharp.Syntax.RecursivePatternSyntax RecursivePattern()
		{
			return SyntaxFactory.RecursivePattern(null, null, null, null);
		}

		// Token: 0x06002A15 RID: 10773 RVA: 0x000E1B70 File Offset: 0x000DFD70
		public static Microsoft.CodeAnalysis.CSharp.Syntax.PositionalPatternClauseSyntax PositionalPatternClause(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.SubpatternSyntax> subpatterns, SyntaxToken closeParenToken)
		{
			if (openParenToken.Kind() != SyntaxKind.OpenParenToken)
			{
				throw new ArgumentException("openParenToken");
			}
			if (closeParenToken.Kind() != SyntaxKind.CloseParenToken)
			{
				throw new ArgumentException("closeParenToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.PositionalPatternClauseSyntax)SyntaxFactory.PositionalPatternClause((SyntaxToken)openParenToken.Node, subpatterns.Node.ToGreenSeparatedList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.SubpatternSyntax>(), (SyntaxToken)closeParenToken.Node).CreateRed();
		}

		// Token: 0x06002A16 RID: 10774 RVA: 0x000E1BE0 File Offset: 0x000DFDE0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.PositionalPatternClauseSyntax PositionalPatternClause([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.SubpatternSyntax> subpatterns = default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.SubpatternSyntax>))
		{
			return SyntaxFactory.PositionalPatternClause(SyntaxFactory.Token(SyntaxKind.OpenParenToken), subpatterns, SyntaxFactory.Token(SyntaxKind.CloseParenToken));
		}

		// Token: 0x06002A17 RID: 10775 RVA: 0x000E1BFC File Offset: 0x000DFDFC
		public static Microsoft.CodeAnalysis.CSharp.Syntax.PropertyPatternClauseSyntax PropertyPatternClause(SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.SubpatternSyntax> subpatterns, SyntaxToken closeBraceToken)
		{
			if (openBraceToken.Kind() != SyntaxKind.OpenBraceToken)
			{
				throw new ArgumentException("openBraceToken");
			}
			if (closeBraceToken.Kind() != SyntaxKind.CloseBraceToken)
			{
				throw new ArgumentException("closeBraceToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.PropertyPatternClauseSyntax)SyntaxFactory.PropertyPatternClause((SyntaxToken)openBraceToken.Node, subpatterns.Node.ToGreenSeparatedList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.SubpatternSyntax>(), (SyntaxToken)closeBraceToken.Node).CreateRed();
		}

		// Token: 0x06002A18 RID: 10776 RVA: 0x000E1C6C File Offset: 0x000DFE6C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.PropertyPatternClauseSyntax PropertyPatternClause([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.SubpatternSyntax> subpatterns = default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.SubpatternSyntax>))
		{
			return SyntaxFactory.PropertyPatternClause(SyntaxFactory.Token(SyntaxKind.OpenBraceToken), subpatterns, SyntaxFactory.Token(SyntaxKind.CloseBraceToken));
		}

		// Token: 0x06002A19 RID: 10777 RVA: 0x000E1C88 File Offset: 0x000DFE88
		public static Microsoft.CodeAnalysis.CSharp.Syntax.SubpatternSyntax Subpattern([global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.NameColonSyntax nameColon, Microsoft.CodeAnalysis.CSharp.Syntax.PatternSyntax pattern)
		{
			if (pattern == null)
			{
				throw new ArgumentNullException("pattern");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.SubpatternSyntax)SyntaxFactory.Subpattern((nameColon == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.NameColonSyntax)nameColon.Green), (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.PatternSyntax)pattern.Green).CreateRed();
		}

		// Token: 0x06002A1A RID: 10778 RVA: 0x000E1CC3 File Offset: 0x000DFEC3
		public static Microsoft.CodeAnalysis.CSharp.Syntax.SubpatternSyntax Subpattern(Microsoft.CodeAnalysis.CSharp.Syntax.PatternSyntax pattern)
		{
			return SyntaxFactory.Subpattern(null, pattern);
		}

		// Token: 0x06002A1B RID: 10779 RVA: 0x000E1CCC File Offset: 0x000DFECC
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ConstantPatternSyntax ConstantPattern(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ConstantPatternSyntax)SyntaxFactory.ConstantPattern((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green).CreateRed();
		}

		// Token: 0x06002A1C RID: 10780 RVA: 0x000E1CF8 File Offset: 0x000DFEF8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ParenthesizedPatternSyntax ParenthesizedPattern(SyntaxToken openParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.PatternSyntax pattern, SyntaxToken closeParenToken)
		{
			if (openParenToken.Kind() != SyntaxKind.OpenParenToken)
			{
				throw new ArgumentException("openParenToken");
			}
			if (pattern == null)
			{
				throw new ArgumentNullException("pattern");
			}
			if (closeParenToken.Kind() != SyntaxKind.CloseParenToken)
			{
				throw new ArgumentException("closeParenToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ParenthesizedPatternSyntax)SyntaxFactory.ParenthesizedPattern((SyntaxToken)openParenToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.PatternSyntax)pattern.Green, (SyntaxToken)closeParenToken.Node).CreateRed();
		}

		// Token: 0x06002A1D RID: 10781 RVA: 0x000E1D75 File Offset: 0x000DFF75
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ParenthesizedPatternSyntax ParenthesizedPattern(Microsoft.CodeAnalysis.CSharp.Syntax.PatternSyntax pattern)
		{
			return SyntaxFactory.ParenthesizedPattern(SyntaxFactory.Token(SyntaxKind.OpenParenToken), pattern, SyntaxFactory.Token(SyntaxKind.CloseParenToken));
		}

		// Token: 0x06002A1E RID: 10782 RVA: 0x000E1D94 File Offset: 0x000DFF94
		public static Microsoft.CodeAnalysis.CSharp.Syntax.RelationalPatternSyntax RelationalPattern(SyntaxToken operatorToken, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			SyntaxKind syntaxKind = operatorToken.Kind();
			if (syntaxKind != SyntaxKind.LessThanToken && syntaxKind != SyntaxKind.GreaterThanToken)
			{
				switch (syntaxKind)
				{
				case SyntaxKind.ExclamationEqualsToken:
				case SyntaxKind.EqualsEqualsToken:
				case SyntaxKind.LessThanEqualsToken:
				case SyntaxKind.GreaterThanEqualsToken:
					break;
				default:
					throw new ArgumentException("operatorToken");
				}
			}
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.RelationalPatternSyntax)SyntaxFactory.RelationalPattern((SyntaxToken)operatorToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green).CreateRed();
		}

		// Token: 0x06002A1F RID: 10783 RVA: 0x000E1E1F File Offset: 0x000E001F
		public static Microsoft.CodeAnalysis.CSharp.Syntax.TypePatternSyntax TypePattern(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.TypePatternSyntax)SyntaxFactory.TypePattern((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)type.Green).CreateRed();
		}

		// Token: 0x06002A20 RID: 10784 RVA: 0x000E1E4C File Offset: 0x000E004C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.BinaryPatternSyntax BinaryPattern(SyntaxKind kind, Microsoft.CodeAnalysis.CSharp.Syntax.PatternSyntax left, SyntaxToken operatorToken, Microsoft.CodeAnalysis.CSharp.Syntax.PatternSyntax right)
		{
			if (kind - SyntaxKind.OrPattern > 1)
			{
				throw new ArgumentException("kind");
			}
			if (left == null)
			{
				throw new ArgumentNullException("left");
			}
			SyntaxKind syntaxKind = operatorToken.Kind();
			if (syntaxKind - SyntaxKind.OrKeyword > 1)
			{
				throw new ArgumentException("operatorToken");
			}
			if (right == null)
			{
				throw new ArgumentNullException("right");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.BinaryPatternSyntax)SyntaxFactory.BinaryPattern(kind, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.PatternSyntax)left.Green, (SyntaxToken)operatorToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.PatternSyntax)right.Green).CreateRed();
		}

		// Token: 0x06002A21 RID: 10785 RVA: 0x000E1ED8 File Offset: 0x000E00D8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.BinaryPatternSyntax BinaryPattern(SyntaxKind kind, Microsoft.CodeAnalysis.CSharp.Syntax.PatternSyntax left, Microsoft.CodeAnalysis.CSharp.Syntax.PatternSyntax right)
		{
			return SyntaxFactory.BinaryPattern(kind, left, SyntaxFactory.Token(SyntaxFactory.GetBinaryPatternOperatorTokenKind(kind)), right);
		}

		// Token: 0x06002A22 RID: 10786 RVA: 0x000E1EF0 File Offset: 0x000E00F0
		private static SyntaxKind GetBinaryPatternOperatorTokenKind(SyntaxKind kind)
		{
			SyntaxKind syntaxKind;
			if (kind != SyntaxKind.OrPattern)
			{
				if (kind != SyntaxKind.AndPattern)
				{
					throw new ArgumentOutOfRangeException();
				}
				syntaxKind = SyntaxKind.AndKeyword;
			}
			else
			{
				syntaxKind = SyntaxKind.OrKeyword;
			}
			return syntaxKind;
		}

		// Token: 0x06002A23 RID: 10787 RVA: 0x000E1F28 File Offset: 0x000E0128
		public static Microsoft.CodeAnalysis.CSharp.Syntax.UnaryPatternSyntax UnaryPattern(SyntaxToken operatorToken, Microsoft.CodeAnalysis.CSharp.Syntax.PatternSyntax pattern)
		{
			if (operatorToken.Kind() != SyntaxKind.NotKeyword)
			{
				throw new ArgumentException("operatorToken");
			}
			if (pattern == null)
			{
				throw new ArgumentNullException("pattern");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.UnaryPatternSyntax)SyntaxFactory.UnaryPattern((SyntaxToken)operatorToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.PatternSyntax)pattern.Green).CreateRed();
		}

		// Token: 0x06002A24 RID: 10788 RVA: 0x000E1F81 File Offset: 0x000E0181
		public static Microsoft.CodeAnalysis.CSharp.Syntax.UnaryPatternSyntax UnaryPattern(Microsoft.CodeAnalysis.CSharp.Syntax.PatternSyntax pattern)
		{
			return SyntaxFactory.UnaryPattern(SyntaxFactory.Token(SyntaxKind.NotKeyword), pattern);
		}

		// Token: 0x06002A25 RID: 10789 RVA: 0x000E1F93 File Offset: 0x000E0193
		public static Microsoft.CodeAnalysis.CSharp.Syntax.InterpolatedStringTextSyntax InterpolatedStringText(SyntaxToken textToken)
		{
			if (textToken.Kind() != SyntaxKind.InterpolatedStringTextToken)
			{
				throw new ArgumentException("textToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.InterpolatedStringTextSyntax)SyntaxFactory.InterpolatedStringText((SyntaxToken)textToken.Node).CreateRed();
		}

		// Token: 0x06002A26 RID: 10790 RVA: 0x000E1FC8 File Offset: 0x000E01C8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.InterpolatedStringTextSyntax InterpolatedStringText()
		{
			return SyntaxFactory.InterpolatedStringText(SyntaxFactory.Token(SyntaxKind.InterpolatedStringTextToken));
		}

		// Token: 0x06002A27 RID: 10791 RVA: 0x000E1FDC File Offset: 0x000E01DC
		public static Microsoft.CodeAnalysis.CSharp.Syntax.InterpolationSyntax Interpolation(SyntaxToken openBraceToken, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.InterpolationAlignmentClauseSyntax alignmentClause, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.InterpolationFormatClauseSyntax formatClause, SyntaxToken closeBraceToken)
		{
			if (openBraceToken.Kind() != SyntaxKind.OpenBraceToken)
			{
				throw new ArgumentException("openBraceToken");
			}
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (closeBraceToken.Kind() != SyntaxKind.CloseBraceToken)
			{
				throw new ArgumentException("closeBraceToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.InterpolationSyntax)SyntaxFactory.Interpolation((SyntaxToken)openBraceToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green, (alignmentClause == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.InterpolationAlignmentClauseSyntax)alignmentClause.Green), (formatClause == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.InterpolationFormatClauseSyntax)formatClause.Green), (SyntaxToken)closeBraceToken.Node).CreateRed();
		}

		// Token: 0x06002A28 RID: 10792 RVA: 0x000E207C File Offset: 0x000E027C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.InterpolationSyntax Interpolation(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.InterpolationAlignmentClauseSyntax alignmentClause, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.InterpolationFormatClauseSyntax formatClause)
		{
			return SyntaxFactory.Interpolation(SyntaxFactory.Token(SyntaxKind.OpenBraceToken), expression, alignmentClause, formatClause, SyntaxFactory.Token(SyntaxKind.CloseBraceToken));
		}

		// Token: 0x06002A29 RID: 10793 RVA: 0x000E209A File Offset: 0x000E029A
		public static Microsoft.CodeAnalysis.CSharp.Syntax.InterpolationSyntax Interpolation(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			return SyntaxFactory.Interpolation(SyntaxFactory.Token(SyntaxKind.OpenBraceToken), expression, null, null, SyntaxFactory.Token(SyntaxKind.CloseBraceToken));
		}

		// Token: 0x06002A2A RID: 10794 RVA: 0x000E20B8 File Offset: 0x000E02B8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.InterpolationAlignmentClauseSyntax InterpolationAlignmentClause(SyntaxToken commaToken, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.InterpolationAlignmentClauseSyntax)SyntaxFactory.InterpolationAlignmentClause((SyntaxToken)commaToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)value.Green).CreateRed();
		}

		// Token: 0x06002A2B RID: 10795 RVA: 0x000E20F0 File Offset: 0x000E02F0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.InterpolationFormatClauseSyntax InterpolationFormatClause(SyntaxToken colonToken, SyntaxToken formatStringToken)
		{
			if (formatStringToken.Kind() != SyntaxKind.InterpolatedStringTextToken)
			{
				throw new ArgumentException("formatStringToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.InterpolationFormatClauseSyntax)SyntaxFactory.InterpolationFormatClause((SyntaxToken)colonToken.Node, (SyntaxToken)formatStringToken.Node).CreateRed();
		}

		// Token: 0x06002A2C RID: 10796 RVA: 0x000E213C File Offset: 0x000E033C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.InterpolationFormatClauseSyntax InterpolationFormatClause(SyntaxToken colonToken)
		{
			return SyntaxFactory.InterpolationFormatClause(colonToken, SyntaxFactory.Token(SyntaxKind.InterpolatedStringTextToken));
		}

		// Token: 0x06002A2D RID: 10797 RVA: 0x000E2150 File Offset: 0x000E0350
		public static Microsoft.CodeAnalysis.CSharp.Syntax.GlobalStatementSyntax GlobalStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			if (statement == null)
			{
				throw new ArgumentNullException("statement");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.GlobalStatementSyntax)SyntaxFactory.GlobalStatement(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), modifiers.Node.ToGreenList<SyntaxToken>(), (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.StatementSyntax)statement.Green).CreateRed();
		}

		// Token: 0x06002A2E RID: 10798 RVA: 0x000E21A0 File Offset: 0x000E03A0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.GlobalStatementSyntax GlobalStatement(Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			return SyntaxFactory.GlobalStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), statement);
		}

		// Token: 0x06002A2F RID: 10799 RVA: 0x000E21C8 File Offset: 0x000E03C8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax Block([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax> statements, SyntaxToken closeBraceToken)
		{
			if (openBraceToken.Kind() != SyntaxKind.OpenBraceToken)
			{
				throw new ArgumentException("openBraceToken");
			}
			if (closeBraceToken.Kind() != SyntaxKind.CloseBraceToken)
			{
				throw new ArgumentException("closeBraceToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax)SyntaxFactory.Block(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), (SyntaxToken)openBraceToken.Node, statements.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.StatementSyntax>(), (SyntaxToken)closeBraceToken.Node).CreateRed();
		}

		// Token: 0x06002A30 RID: 10800 RVA: 0x000E2244 File Offset: 0x000E0444
		public static Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax Block([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax> statements)
		{
			return SyntaxFactory.Block(attributeLists, SyntaxFactory.Token(SyntaxKind.OpenBraceToken), statements, SyntaxFactory.Token(SyntaxKind.CloseBraceToken));
		}

		// Token: 0x06002A31 RID: 10801 RVA: 0x000E2264 File Offset: 0x000E0464
		public static Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax Block([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax> statements = default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax>))
		{
			return SyntaxFactory.Block(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), SyntaxFactory.Token(SyntaxKind.OpenBraceToken), statements, SyntaxFactory.Token(SyntaxKind.CloseBraceToken));
		}

		// Token: 0x06002A32 RID: 10802 RVA: 0x000E2294 File Offset: 0x000E0494
		public static Microsoft.CodeAnalysis.CSharp.Syntax.LocalFunctionStatementSyntax LocalFunctionStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax returnType, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterListSyntax typeParameterList, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax> constraintClauses, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax body, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
		{
			if (returnType == null)
			{
				throw new ArgumentNullException("returnType");
			}
			if (identifier.Kind() != SyntaxKind.IdentifierToken)
			{
				throw new ArgumentException("identifier");
			}
			if (parameterList == null)
			{
				throw new ArgumentNullException("parameterList");
			}
			SyntaxKind syntaxKind = semicolonToken.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind != SyntaxKind.SemicolonToken)
			{
				throw new ArgumentException("semicolonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.LocalFunctionStatementSyntax)SyntaxFactory.LocalFunctionStatement(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), modifiers.Node.ToGreenList<SyntaxToken>(), (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)returnType.Green, (SyntaxToken)identifier.Node, (typeParameterList == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeParameterListSyntax)typeParameterList.Green), (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ParameterListSyntax)parameterList.Green, constraintClauses.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeParameterConstraintClauseSyntax>(), (body == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.BlockSyntax)body.Green), (expressionBody == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ArrowExpressionClauseSyntax)expressionBody.Green), (SyntaxToken)semicolonToken.Node).CreateRed();
		}

		// Token: 0x06002A33 RID: 10803 RVA: 0x000E2390 File Offset: 0x000E0590
		public static Microsoft.CodeAnalysis.CSharp.Syntax.LocalFunctionStatementSyntax LocalFunctionStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax returnType, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterListSyntax typeParameterList, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax> constraintClauses, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax body, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ArrowExpressionClauseSyntax expressionBody)
		{
			return SyntaxFactory.LocalFunctionStatement(attributeLists, modifiers, returnType, identifier, typeParameterList, parameterList, constraintClauses, body, expressionBody, default(SyntaxToken));
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x000E23BC File Offset: 0x000E05BC
		public static Microsoft.CodeAnalysis.CSharp.Syntax.LocalFunctionStatementSyntax LocalFunctionStatement(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax returnType, SyntaxToken identifier)
		{
			return SyntaxFactory.LocalFunctionStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), returnType, identifier, null, SyntaxFactory.ParameterList(default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax>)), default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax>), null, null, default(SyntaxToken));
		}

		// Token: 0x06002A35 RID: 10805 RVA: 0x000E2408 File Offset: 0x000E0608
		public static Microsoft.CodeAnalysis.CSharp.Syntax.LocalFunctionStatementSyntax LocalFunctionStatement(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax returnType, string identifier)
		{
			return SyntaxFactory.LocalFunctionStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), returnType, SyntaxFactory.Identifier(identifier), null, SyntaxFactory.ParameterList(default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax>)), default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax>), null, null, default(SyntaxToken));
		}

		// Token: 0x06002A36 RID: 10806 RVA: 0x000E2458 File Offset: 0x000E0658
		public static Microsoft.CodeAnalysis.CSharp.Syntax.LocalDeclarationStatementSyntax LocalDeclarationStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxToken awaitKeyword, SyntaxToken usingKeyword, SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclarationSyntax declaration, SyntaxToken semicolonToken)
		{
			SyntaxKind syntaxKind = awaitKeyword.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind != SyntaxKind.AwaitKeyword)
			{
				throw new ArgumentException("awaitKeyword");
			}
			syntaxKind = usingKeyword.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind != SyntaxKind.UsingKeyword)
			{
				throw new ArgumentException("usingKeyword");
			}
			if (declaration == null)
			{
				throw new ArgumentNullException("declaration");
			}
			if (semicolonToken.Kind() != SyntaxKind.SemicolonToken)
			{
				throw new ArgumentException("semicolonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.LocalDeclarationStatementSyntax)SyntaxFactory.LocalDeclarationStatement(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), (SyntaxToken)awaitKeyword.Node, (SyntaxToken)usingKeyword.Node, modifiers.Node.ToGreenList<SyntaxToken>(), (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.VariableDeclarationSyntax)declaration.Green, (SyntaxToken)semicolonToken.Node).CreateRed();
		}

		// Token: 0x06002A37 RID: 10807 RVA: 0x000E2520 File Offset: 0x000E0720
		public static Microsoft.CodeAnalysis.CSharp.Syntax.LocalDeclarationStatementSyntax LocalDeclarationStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclarationSyntax declaration)
		{
			return SyntaxFactory.LocalDeclarationStatement(attributeLists, default(SyntaxToken), default(SyntaxToken), modifiers, declaration, SyntaxFactory.Token(SyntaxKind.SemicolonToken));
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x000E2554 File Offset: 0x000E0754
		public static Microsoft.CodeAnalysis.CSharp.Syntax.LocalDeclarationStatementSyntax LocalDeclarationStatement(Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclarationSyntax declaration)
		{
			return SyntaxFactory.LocalDeclarationStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxToken), default(SyntaxToken), default(SyntaxTokenList), declaration, SyntaxFactory.Token(SyntaxKind.SemicolonToken));
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x000E2595 File Offset: 0x000E0795
		public static Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclarationSyntax VariableDeclaration(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclaratorSyntax> variables)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclarationSyntax)SyntaxFactory.VariableDeclaration((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)type.Green, variables.Node.ToGreenSeparatedList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.VariableDeclaratorSyntax>()).CreateRed();
		}

		// Token: 0x06002A3A RID: 10810 RVA: 0x000E25CC File Offset: 0x000E07CC
		public static Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclarationSyntax VariableDeclaration(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type)
		{
			return SyntaxFactory.VariableDeclaration(type, default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclaratorSyntax>));
		}

		// Token: 0x06002A3B RID: 10811 RVA: 0x000E25E8 File Offset: 0x000E07E8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public static Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclaratorSyntax VariableDeclarator(SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.BracketedArgumentListSyntax argumentList, Microsoft.CodeAnalysis.CSharp.Syntax.EqualsValueClauseSyntax initializer)
		{
			if (identifier.Kind() != SyntaxKind.IdentifierToken)
			{
				throw new ArgumentException("identifier");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclaratorSyntax)SyntaxFactory.VariableDeclarator((SyntaxToken)identifier.Node, (argumentList == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.BracketedArgumentListSyntax)argumentList.Green), (initializer == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.EqualsValueClauseSyntax)initializer.Green)).CreateRed();
		}

		// Token: 0x06002A3C RID: 10812 RVA: 0x000E264A File Offset: 0x000E084A
		public static Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclaratorSyntax VariableDeclarator(SyntaxToken identifier)
		{
			return SyntaxFactory.VariableDeclarator(identifier, null, null);
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x000E2654 File Offset: 0x000E0854
		public static Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclaratorSyntax VariableDeclarator(string identifier)
		{
			return SyntaxFactory.VariableDeclarator(SyntaxFactory.Identifier(identifier), null, null);
		}

		// Token: 0x06002A3E RID: 10814 RVA: 0x000E2664 File Offset: 0x000E0864
		public static Microsoft.CodeAnalysis.CSharp.Syntax.EqualsValueClauseSyntax EqualsValueClause(SyntaxToken equalsToken, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax value)
		{
			if (equalsToken.Kind() != SyntaxKind.EqualsToken)
			{
				throw new ArgumentException("equalsToken");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.EqualsValueClauseSyntax)SyntaxFactory.EqualsValueClause((SyntaxToken)equalsToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)value.Green).CreateRed();
		}

		// Token: 0x06002A3F RID: 10815 RVA: 0x000E26BD File Offset: 0x000E08BD
		public static Microsoft.CodeAnalysis.CSharp.Syntax.EqualsValueClauseSyntax EqualsValueClause(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax value)
		{
			return SyntaxFactory.EqualsValueClause(SyntaxFactory.Token(SyntaxKind.EqualsToken), value);
		}

		// Token: 0x06002A40 RID: 10816 RVA: 0x000E26CF File Offset: 0x000E08CF
		public static Microsoft.CodeAnalysis.CSharp.Syntax.SingleVariableDesignationSyntax SingleVariableDesignation(SyntaxToken identifier)
		{
			if (identifier.Kind() != SyntaxKind.IdentifierToken)
			{
				throw new ArgumentException("identifier");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.SingleVariableDesignationSyntax)SyntaxFactory.SingleVariableDesignation((SyntaxToken)identifier.Node).CreateRed();
		}

		// Token: 0x06002A41 RID: 10817 RVA: 0x000E2704 File Offset: 0x000E0904
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DiscardDesignationSyntax DiscardDesignation(SyntaxToken underscoreToken)
		{
			if (underscoreToken.Kind() != SyntaxKind.UnderscoreToken)
			{
				throw new ArgumentException("underscoreToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.DiscardDesignationSyntax)SyntaxFactory.DiscardDesignation((SyntaxToken)underscoreToken.Node).CreateRed();
		}

		// Token: 0x06002A42 RID: 10818 RVA: 0x000E2739 File Offset: 0x000E0939
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DiscardDesignationSyntax DiscardDesignation()
		{
			return SyntaxFactory.DiscardDesignation(SyntaxFactory.Token(SyntaxKind.UnderscoreToken));
		}

		// Token: 0x06002A43 RID: 10819 RVA: 0x000E274C File Offset: 0x000E094C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ParenthesizedVariableDesignationSyntax ParenthesizedVariableDesignation(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.VariableDesignationSyntax> variables, SyntaxToken closeParenToken)
		{
			if (openParenToken.Kind() != SyntaxKind.OpenParenToken)
			{
				throw new ArgumentException("openParenToken");
			}
			if (closeParenToken.Kind() != SyntaxKind.CloseParenToken)
			{
				throw new ArgumentException("closeParenToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ParenthesizedVariableDesignationSyntax)SyntaxFactory.ParenthesizedVariableDesignation((SyntaxToken)openParenToken.Node, variables.Node.ToGreenSeparatedList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.VariableDesignationSyntax>(), (SyntaxToken)closeParenToken.Node).CreateRed();
		}

		// Token: 0x06002A44 RID: 10820 RVA: 0x000E27BC File Offset: 0x000E09BC
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ParenthesizedVariableDesignationSyntax ParenthesizedVariableDesignation([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.VariableDesignationSyntax> variables = default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.VariableDesignationSyntax>))
		{
			return SyntaxFactory.ParenthesizedVariableDesignation(SyntaxFactory.Token(SyntaxKind.OpenParenToken), variables, SyntaxFactory.Token(SyntaxKind.CloseParenToken));
		}

		// Token: 0x06002A45 RID: 10821 RVA: 0x000E27D8 File Offset: 0x000E09D8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionStatementSyntax ExpressionStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SyntaxToken semicolonToken)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (semicolonToken.Kind() != SyntaxKind.SemicolonToken)
			{
				throw new ArgumentException("semicolonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionStatementSyntax)SyntaxFactory.ExpressionStatement(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green, (SyntaxToken)semicolonToken.Node).CreateRed();
		}

		// Token: 0x06002A46 RID: 10822 RVA: 0x000E283D File Offset: 0x000E0A3D
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionStatementSyntax ExpressionStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			return SyntaxFactory.ExpressionStatement(attributeLists, expression, SyntaxFactory.Token(SyntaxKind.SemicolonToken));
		}

		// Token: 0x06002A47 RID: 10823 RVA: 0x000E2850 File Offset: 0x000E0A50
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionStatementSyntax ExpressionStatement(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			return SyntaxFactory.ExpressionStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), expression, SyntaxFactory.Token(SyntaxKind.SemicolonToken));
		}

		// Token: 0x06002A48 RID: 10824 RVA: 0x000E2878 File Offset: 0x000E0A78
		public static Microsoft.CodeAnalysis.CSharp.Syntax.EmptyStatementSyntax EmptyStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxToken semicolonToken)
		{
			if (semicolonToken.Kind() != SyntaxKind.SemicolonToken)
			{
				throw new ArgumentException("semicolonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.EmptyStatementSyntax)SyntaxFactory.EmptyStatement(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), (SyntaxToken)semicolonToken.Node).CreateRed();
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x000E28C4 File Offset: 0x000E0AC4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.EmptyStatementSyntax EmptyStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists)
		{
			return SyntaxFactory.EmptyStatement(attributeLists, SyntaxFactory.Token(SyntaxKind.SemicolonToken));
		}

		// Token: 0x06002A4A RID: 10826 RVA: 0x000E28D8 File Offset: 0x000E0AD8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.EmptyStatementSyntax EmptyStatement()
		{
			return SyntaxFactory.EmptyStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), SyntaxFactory.Token(SyntaxKind.SemicolonToken));
		}

		// Token: 0x06002A4B RID: 10827 RVA: 0x000E2900 File Offset: 0x000E0B00
		public static Microsoft.CodeAnalysis.CSharp.Syntax.LabeledStatementSyntax LabeledStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxToken identifier, SyntaxToken colonToken, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			if (identifier.Kind() != SyntaxKind.IdentifierToken)
			{
				throw new ArgumentException("identifier");
			}
			if (colonToken.Kind() != SyntaxKind.ColonToken)
			{
				throw new ArgumentException("colonToken");
			}
			if (statement == null)
			{
				throw new ArgumentNullException("statement");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.LabeledStatementSyntax)SyntaxFactory.LabeledStatement(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), (SyntaxToken)identifier.Node, (SyntaxToken)colonToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.StatementSyntax)statement.Green).CreateRed();
		}

		// Token: 0x06002A4C RID: 10828 RVA: 0x000E2989 File Offset: 0x000E0B89
		public static Microsoft.CodeAnalysis.CSharp.Syntax.LabeledStatementSyntax LabeledStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			return SyntaxFactory.LabeledStatement(attributeLists, identifier, SyntaxFactory.Token(SyntaxKind.ColonToken), statement);
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x000E29A0 File Offset: 0x000E0BA0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.LabeledStatementSyntax LabeledStatement(SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			return SyntaxFactory.LabeledStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), identifier, SyntaxFactory.Token(SyntaxKind.ColonToken), statement);
		}

		// Token: 0x06002A4E RID: 10830 RVA: 0x000E29C8 File Offset: 0x000E0BC8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.LabeledStatementSyntax LabeledStatement(string identifier, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			return SyntaxFactory.LabeledStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), SyntaxFactory.Identifier(identifier), SyntaxFactory.Token(SyntaxKind.ColonToken), statement);
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x000E29F4 File Offset: 0x000E0BF4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.GotoStatementSyntax GotoStatement(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxToken gotoKeyword, SyntaxToken caseOrDefaultKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SyntaxToken semicolonToken)
		{
			if (kind - SyntaxKind.GotoStatement > 2)
			{
				throw new ArgumentException("kind");
			}
			if (gotoKeyword.Kind() != SyntaxKind.GotoKeyword)
			{
				throw new ArgumentException("gotoKeyword");
			}
			SyntaxKind syntaxKind = caseOrDefaultKeyword.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind - SyntaxKind.CaseKeyword > 1)
			{
				throw new ArgumentException("caseOrDefaultKeyword");
			}
			if (semicolonToken.Kind() != SyntaxKind.SemicolonToken)
			{
				throw new ArgumentException("semicolonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.GotoStatementSyntax)SyntaxFactory.GotoStatement(kind, attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), (SyntaxToken)gotoKeyword.Node, (SyntaxToken)caseOrDefaultKeyword.Node, (expression == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green), (SyntaxToken)semicolonToken.Node).CreateRed();
		}

		// Token: 0x06002A50 RID: 10832 RVA: 0x000E2AB9 File Offset: 0x000E0CB9
		public static Microsoft.CodeAnalysis.CSharp.Syntax.GotoStatementSyntax GotoStatement(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxToken caseOrDefaultKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			return SyntaxFactory.GotoStatement(kind, attributeLists, SyntaxFactory.Token(SyntaxKind.GotoKeyword), caseOrDefaultKeyword, expression, SyntaxFactory.Token(SyntaxKind.SemicolonToken));
		}

		// Token: 0x06002A51 RID: 10833 RVA: 0x000E2AD8 File Offset: 0x000E0CD8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.GotoStatementSyntax GotoStatement(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression = null)
		{
			return SyntaxFactory.GotoStatement(kind, default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), SyntaxFactory.Token(SyntaxKind.GotoKeyword), default(SyntaxToken), expression, SyntaxFactory.Token(SyntaxKind.SemicolonToken));
		}

		// Token: 0x06002A52 RID: 10834 RVA: 0x000E2B14 File Offset: 0x000E0D14
		public static Microsoft.CodeAnalysis.CSharp.Syntax.BreakStatementSyntax BreakStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxToken breakKeyword, SyntaxToken semicolonToken)
		{
			if (breakKeyword.Kind() != SyntaxKind.BreakKeyword)
			{
				throw new ArgumentException("breakKeyword");
			}
			if (semicolonToken.Kind() != SyntaxKind.SemicolonToken)
			{
				throw new ArgumentException("semicolonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.BreakStatementSyntax)SyntaxFactory.BreakStatement(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), (SyntaxToken)breakKeyword.Node, (SyntaxToken)semicolonToken.Node).CreateRed();
		}

		// Token: 0x06002A53 RID: 10835 RVA: 0x000E2B84 File Offset: 0x000E0D84
		public static Microsoft.CodeAnalysis.CSharp.Syntax.BreakStatementSyntax BreakStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists)
		{
			return SyntaxFactory.BreakStatement(attributeLists, SyntaxFactory.Token(SyntaxKind.BreakKeyword), SyntaxFactory.Token(SyntaxKind.SemicolonToken));
		}

		// Token: 0x06002A54 RID: 10836 RVA: 0x000E2BA0 File Offset: 0x000E0DA0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.BreakStatementSyntax BreakStatement()
		{
			return SyntaxFactory.BreakStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), SyntaxFactory.Token(SyntaxKind.BreakKeyword), SyntaxFactory.Token(SyntaxKind.SemicolonToken));
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x000E2BD0 File Offset: 0x000E0DD0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ContinueStatementSyntax ContinueStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxToken continueKeyword, SyntaxToken semicolonToken)
		{
			if (continueKeyword.Kind() != SyntaxKind.ContinueKeyword)
			{
				throw new ArgumentException("continueKeyword");
			}
			if (semicolonToken.Kind() != SyntaxKind.SemicolonToken)
			{
				throw new ArgumentException("semicolonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ContinueStatementSyntax)SyntaxFactory.ContinueStatement(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), (SyntaxToken)continueKeyword.Node, (SyntaxToken)semicolonToken.Node).CreateRed();
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x000E2C40 File Offset: 0x000E0E40
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ContinueStatementSyntax ContinueStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists)
		{
			return SyntaxFactory.ContinueStatement(attributeLists, SyntaxFactory.Token(SyntaxKind.ContinueKeyword), SyntaxFactory.Token(SyntaxKind.SemicolonToken));
		}

		// Token: 0x06002A57 RID: 10839 RVA: 0x000E2C5C File Offset: 0x000E0E5C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ContinueStatementSyntax ContinueStatement()
		{
			return SyntaxFactory.ContinueStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), SyntaxFactory.Token(SyntaxKind.ContinueKeyword), SyntaxFactory.Token(SyntaxKind.SemicolonToken));
		}

		// Token: 0x06002A58 RID: 10840 RVA: 0x000E2C8C File Offset: 0x000E0E8C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ReturnStatementSyntax ReturnStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxToken returnKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SyntaxToken semicolonToken)
		{
			if (returnKeyword.Kind() != SyntaxKind.ReturnKeyword)
			{
				throw new ArgumentException("returnKeyword");
			}
			if (semicolonToken.Kind() != SyntaxKind.SemicolonToken)
			{
				throw new ArgumentException("semicolonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ReturnStatementSyntax)SyntaxFactory.ReturnStatement(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), (SyntaxToken)returnKeyword.Node, (expression == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green), (SyntaxToken)semicolonToken.Node).CreateRed();
		}

		// Token: 0x06002A59 RID: 10841 RVA: 0x000E2D0D File Offset: 0x000E0F0D
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ReturnStatementSyntax ReturnStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			return SyntaxFactory.ReturnStatement(attributeLists, SyntaxFactory.Token(SyntaxKind.ReturnKeyword), expression, SyntaxFactory.Token(SyntaxKind.SemicolonToken));
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x000E2D2C File Offset: 0x000E0F2C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ReturnStatementSyntax ReturnStatement([global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression = null)
		{
			return SyntaxFactory.ReturnStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), SyntaxFactory.Token(SyntaxKind.ReturnKeyword), expression, SyntaxFactory.Token(SyntaxKind.SemicolonToken));
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x000E2D5C File Offset: 0x000E0F5C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ThrowStatementSyntax ThrowStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxToken throwKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SyntaxToken semicolonToken)
		{
			if (throwKeyword.Kind() != SyntaxKind.ThrowKeyword)
			{
				throw new ArgumentException("throwKeyword");
			}
			if (semicolonToken.Kind() != SyntaxKind.SemicolonToken)
			{
				throw new ArgumentException("semicolonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ThrowStatementSyntax)SyntaxFactory.ThrowStatement(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), (SyntaxToken)throwKeyword.Node, (expression == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green), (SyntaxToken)semicolonToken.Node).CreateRed();
		}

		// Token: 0x06002A5C RID: 10844 RVA: 0x000E2DDD File Offset: 0x000E0FDD
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ThrowStatementSyntax ThrowStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			return SyntaxFactory.ThrowStatement(attributeLists, SyntaxFactory.Token(SyntaxKind.ThrowKeyword), expression, SyntaxFactory.Token(SyntaxKind.SemicolonToken));
		}

		// Token: 0x06002A5D RID: 10845 RVA: 0x000E2DFC File Offset: 0x000E0FFC
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ThrowStatementSyntax ThrowStatement([global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression = null)
		{
			return SyntaxFactory.ThrowStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), SyntaxFactory.Token(SyntaxKind.ThrowKeyword), expression, SyntaxFactory.Token(SyntaxKind.SemicolonToken));
		}

		// Token: 0x06002A5E RID: 10846 RVA: 0x000E2E2C File Offset: 0x000E102C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.YieldStatementSyntax YieldStatement(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxToken yieldKeyword, SyntaxToken returnOrBreakKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SyntaxToken semicolonToken)
		{
			if (kind - SyntaxKind.YieldReturnStatement > 1)
			{
				throw new ArgumentException("kind");
			}
			if (yieldKeyword.Kind() != SyntaxKind.YieldKeyword)
			{
				throw new ArgumentException("yieldKeyword");
			}
			SyntaxKind syntaxKind = returnOrBreakKeyword.Kind();
			if (syntaxKind != SyntaxKind.BreakKeyword && syntaxKind != SyntaxKind.ReturnKeyword)
			{
				throw new ArgumentException("returnOrBreakKeyword");
			}
			if (semicolonToken.Kind() != SyntaxKind.SemicolonToken)
			{
				throw new ArgumentException("semicolonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.YieldStatementSyntax)SyntaxFactory.YieldStatement(kind, attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), (SyntaxToken)yieldKeyword.Node, (SyntaxToken)returnOrBreakKeyword.Node, (expression == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green), (SyntaxToken)semicolonToken.Node).CreateRed();
		}

		// Token: 0x06002A5F RID: 10847 RVA: 0x000E2EF4 File Offset: 0x000E10F4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.YieldStatementSyntax YieldStatement(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			return SyntaxFactory.YieldStatement(kind, attributeLists, SyntaxFactory.Token(SyntaxKind.YieldKeyword), SyntaxFactory.Token(SyntaxFactory.GetYieldStatementReturnOrBreakKeywordKind(kind)), expression, SyntaxFactory.Token(SyntaxKind.SemicolonToken));
		}

		// Token: 0x06002A60 RID: 10848 RVA: 0x000E2F20 File Offset: 0x000E1120
		public static Microsoft.CodeAnalysis.CSharp.Syntax.YieldStatementSyntax YieldStatement(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression = null)
		{
			return SyntaxFactory.YieldStatement(kind, default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), SyntaxFactory.Token(SyntaxKind.YieldKeyword), SyntaxFactory.Token(SyntaxFactory.GetYieldStatementReturnOrBreakKeywordKind(kind)), expression, SyntaxFactory.Token(SyntaxKind.SemicolonToken));
		}

		// Token: 0x06002A61 RID: 10849 RVA: 0x000E2F5C File Offset: 0x000E115C
		private static SyntaxKind GetYieldStatementReturnOrBreakKeywordKind(SyntaxKind kind)
		{
			SyntaxKind syntaxKind;
			if (kind != SyntaxKind.YieldReturnStatement)
			{
				if (kind != SyntaxKind.YieldBreakStatement)
				{
					throw new ArgumentOutOfRangeException();
				}
				syntaxKind = SyntaxKind.BreakKeyword;
			}
			else
			{
				syntaxKind = SyntaxKind.ReturnKeyword;
			}
			return syntaxKind;
		}

		// Token: 0x06002A62 RID: 10850 RVA: 0x000E2F94 File Offset: 0x000E1194
		public static Microsoft.CodeAnalysis.CSharp.Syntax.WhileStatementSyntax WhileStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxToken whileKeyword, SyntaxToken openParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax condition, SyntaxToken closeParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			if (whileKeyword.Kind() != SyntaxKind.WhileKeyword)
			{
				throw new ArgumentException("whileKeyword");
			}
			if (openParenToken.Kind() != SyntaxKind.OpenParenToken)
			{
				throw new ArgumentException("openParenToken");
			}
			if (condition == null)
			{
				throw new ArgumentNullException("condition");
			}
			if (closeParenToken.Kind() != SyntaxKind.CloseParenToken)
			{
				throw new ArgumentException("closeParenToken");
			}
			if (statement == null)
			{
				throw new ArgumentNullException("statement");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.WhileStatementSyntax)SyntaxFactory.WhileStatement(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), (SyntaxToken)whileKeyword.Node, (SyntaxToken)openParenToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)condition.Green, (SyntaxToken)closeParenToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.StatementSyntax)statement.Green).CreateRed();
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x000E305D File Offset: 0x000E125D
		public static Microsoft.CodeAnalysis.CSharp.Syntax.WhileStatementSyntax WhileStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax condition, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			return SyntaxFactory.WhileStatement(attributeLists, SyntaxFactory.Token(SyntaxKind.WhileKeyword), SyntaxFactory.Token(SyntaxKind.OpenParenToken), condition, SyntaxFactory.Token(SyntaxKind.CloseParenToken), statement);
		}

		// Token: 0x06002A64 RID: 10852 RVA: 0x000E3088 File Offset: 0x000E1288
		public static Microsoft.CodeAnalysis.CSharp.Syntax.WhileStatementSyntax WhileStatement(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax condition, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			return SyntaxFactory.WhileStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), SyntaxFactory.Token(SyntaxKind.WhileKeyword), SyntaxFactory.Token(SyntaxKind.OpenParenToken), condition, SyntaxFactory.Token(SyntaxKind.CloseParenToken), statement);
		}

		// Token: 0x06002A65 RID: 10853 RVA: 0x000E30C4 File Offset: 0x000E12C4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DoStatementSyntax DoStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxToken doKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement, SyntaxToken whileKeyword, SyntaxToken openParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax condition, SyntaxToken closeParenToken, SyntaxToken semicolonToken)
		{
			if (doKeyword.Kind() != SyntaxKind.DoKeyword)
			{
				throw new ArgumentException("doKeyword");
			}
			if (statement == null)
			{
				throw new ArgumentNullException("statement");
			}
			if (whileKeyword.Kind() != SyntaxKind.WhileKeyword)
			{
				throw new ArgumentException("whileKeyword");
			}
			if (openParenToken.Kind() != SyntaxKind.OpenParenToken)
			{
				throw new ArgumentException("openParenToken");
			}
			if (condition == null)
			{
				throw new ArgumentNullException("condition");
			}
			if (closeParenToken.Kind() != SyntaxKind.CloseParenToken)
			{
				throw new ArgumentException("closeParenToken");
			}
			if (semicolonToken.Kind() != SyntaxKind.SemicolonToken)
			{
				throw new ArgumentException("semicolonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.DoStatementSyntax)SyntaxFactory.DoStatement(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), (SyntaxToken)doKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.StatementSyntax)statement.Green, (SyntaxToken)whileKeyword.Node, (SyntaxToken)openParenToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)condition.Green, (SyntaxToken)closeParenToken.Node, (SyntaxToken)semicolonToken.Node).CreateRed();
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x000E31D7 File Offset: 0x000E13D7
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DoStatementSyntax DoStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax condition)
		{
			return SyntaxFactory.DoStatement(attributeLists, SyntaxFactory.Token(SyntaxKind.DoKeyword), statement, SyntaxFactory.Token(SyntaxKind.WhileKeyword), SyntaxFactory.Token(SyntaxKind.OpenParenToken), condition, SyntaxFactory.Token(SyntaxKind.CloseParenToken), SyntaxFactory.Token(SyntaxKind.SemicolonToken));
		}

		// Token: 0x06002A67 RID: 10855 RVA: 0x000E3214 File Offset: 0x000E1414
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DoStatementSyntax DoStatement(Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax condition)
		{
			return SyntaxFactory.DoStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), SyntaxFactory.Token(SyntaxKind.DoKeyword), statement, SyntaxFactory.Token(SyntaxKind.WhileKeyword), SyntaxFactory.Token(SyntaxKind.OpenParenToken), condition, SyntaxFactory.Token(SyntaxKind.CloseParenToken), SyntaxFactory.Token(SyntaxKind.SemicolonToken));
		}

		// Token: 0x06002A68 RID: 10856 RVA: 0x000E3264 File Offset: 0x000E1464
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ForStatementSyntax ForStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxToken forKeyword, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax> initializers, SyntaxToken firstSemicolonToken, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax condition, SyntaxToken secondSemicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax> incrementors, SyntaxToken closeParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			if (forKeyword.Kind() != SyntaxKind.ForKeyword)
			{
				throw new ArgumentException("forKeyword");
			}
			if (openParenToken.Kind() != SyntaxKind.OpenParenToken)
			{
				throw new ArgumentException("openParenToken");
			}
			if (firstSemicolonToken.Kind() != SyntaxKind.SemicolonToken)
			{
				throw new ArgumentException("firstSemicolonToken");
			}
			if (secondSemicolonToken.Kind() != SyntaxKind.SemicolonToken)
			{
				throw new ArgumentException("secondSemicolonToken");
			}
			if (closeParenToken.Kind() != SyntaxKind.CloseParenToken)
			{
				throw new ArgumentException("closeParenToken");
			}
			if (statement == null)
			{
				throw new ArgumentNullException("statement");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ForStatementSyntax)SyntaxFactory.ForStatement(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), (SyntaxToken)forKeyword.Node, (SyntaxToken)openParenToken.Node, (declaration == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.VariableDeclarationSyntax)declaration.Green), initializers.Node.ToGreenSeparatedList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax>(), (SyntaxToken)firstSemicolonToken.Node, (condition == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)condition.Green), (SyntaxToken)secondSemicolonToken.Node, incrementors.Node.ToGreenSeparatedList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax>(), (SyntaxToken)closeParenToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.StatementSyntax)statement.Green).CreateRed();
		}

		// Token: 0x06002A69 RID: 10857 RVA: 0x000E339C File Offset: 0x000E159C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ForStatementSyntax ForStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax> initializers, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax condition, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax> incrementors, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			return SyntaxFactory.ForStatement(attributeLists, SyntaxFactory.Token(SyntaxKind.ForKeyword), SyntaxFactory.Token(SyntaxKind.OpenParenToken), declaration, initializers, SyntaxFactory.Token(SyntaxKind.SemicolonToken), condition, SyntaxFactory.Token(SyntaxKind.SemicolonToken), incrementors, SyntaxFactory.Token(SyntaxKind.CloseParenToken), statement);
		}

		// Token: 0x06002A6A RID: 10858 RVA: 0x000E33E8 File Offset: 0x000E15E8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ForStatementSyntax ForStatement(Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			return SyntaxFactory.ForStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), SyntaxFactory.Token(SyntaxKind.ForKeyword), SyntaxFactory.Token(SyntaxKind.OpenParenToken), null, default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax>), SyntaxFactory.Token(SyntaxKind.SemicolonToken), null, SyntaxFactory.Token(SyntaxKind.SemicolonToken), default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax>), SyntaxFactory.Token(SyntaxKind.CloseParenToken), statement);
		}

		// Token: 0x06002A6B RID: 10859 RVA: 0x000E344C File Offset: 0x000E164C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ForEachStatementSyntax ForEachStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxToken awaitKeyword, SyntaxToken forEachKeyword, SyntaxToken openParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SyntaxToken closeParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			SyntaxKind syntaxKind = awaitKeyword.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind != SyntaxKind.AwaitKeyword)
			{
				throw new ArgumentException("awaitKeyword");
			}
			if (forEachKeyword.Kind() != SyntaxKind.ForEachKeyword)
			{
				throw new ArgumentException("forEachKeyword");
			}
			if (openParenToken.Kind() != SyntaxKind.OpenParenToken)
			{
				throw new ArgumentException("openParenToken");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (identifier.Kind() != SyntaxKind.IdentifierToken)
			{
				throw new ArgumentException("identifier");
			}
			if (inKeyword.Kind() != SyntaxKind.InKeyword)
			{
				throw new ArgumentException("inKeyword");
			}
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (closeParenToken.Kind() != SyntaxKind.CloseParenToken)
			{
				throw new ArgumentException("closeParenToken");
			}
			if (statement == null)
			{
				throw new ArgumentNullException("statement");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ForEachStatementSyntax)SyntaxFactory.ForEachStatement(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), (SyntaxToken)awaitKeyword.Node, (SyntaxToken)forEachKeyword.Node, (SyntaxToken)openParenToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)type.Green, (SyntaxToken)identifier.Node, (SyntaxToken)inKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green, (SyntaxToken)closeParenToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.StatementSyntax)statement.Green).CreateRed();
		}

		// Token: 0x06002A6C RID: 10860 RVA: 0x000E35A8 File Offset: 0x000E17A8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ForEachStatementSyntax ForEachStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			return SyntaxFactory.ForEachStatement(attributeLists, default(SyntaxToken), SyntaxFactory.Token(SyntaxKind.ForEachKeyword), SyntaxFactory.Token(SyntaxKind.OpenParenToken), type, identifier, SyntaxFactory.Token(SyntaxKind.InKeyword), expression, SyntaxFactory.Token(SyntaxKind.CloseParenToken), statement);
		}

		// Token: 0x06002A6D RID: 10861 RVA: 0x000E35F4 File Offset: 0x000E17F4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ForEachStatementSyntax ForEachStatement(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			return SyntaxFactory.ForEachStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxToken), SyntaxFactory.Token(SyntaxKind.ForEachKeyword), SyntaxFactory.Token(SyntaxKind.OpenParenToken), type, identifier, SyntaxFactory.Token(SyntaxKind.InKeyword), expression, SyntaxFactory.Token(SyntaxKind.CloseParenToken), statement);
		}

		// Token: 0x06002A6E RID: 10862 RVA: 0x000E3644 File Offset: 0x000E1844
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ForEachStatementSyntax ForEachStatement(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, string identifier, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			return SyntaxFactory.ForEachStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxToken), SyntaxFactory.Token(SyntaxKind.ForEachKeyword), SyntaxFactory.Token(SyntaxKind.OpenParenToken), type, SyntaxFactory.Identifier(identifier), SyntaxFactory.Token(SyntaxKind.InKeyword), expression, SyntaxFactory.Token(SyntaxKind.CloseParenToken), statement);
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x000E369C File Offset: 0x000E189C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ForEachVariableStatementSyntax ForEachVariableStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxToken awaitKeyword, SyntaxToken forEachKeyword, SyntaxToken openParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax variable, SyntaxToken inKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SyntaxToken closeParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			SyntaxKind syntaxKind = awaitKeyword.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind != SyntaxKind.AwaitKeyword)
			{
				throw new ArgumentException("awaitKeyword");
			}
			if (forEachKeyword.Kind() != SyntaxKind.ForEachKeyword)
			{
				throw new ArgumentException("forEachKeyword");
			}
			if (openParenToken.Kind() != SyntaxKind.OpenParenToken)
			{
				throw new ArgumentException("openParenToken");
			}
			if (variable == null)
			{
				throw new ArgumentNullException("variable");
			}
			if (inKeyword.Kind() != SyntaxKind.InKeyword)
			{
				throw new ArgumentException("inKeyword");
			}
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (closeParenToken.Kind() != SyntaxKind.CloseParenToken)
			{
				throw new ArgumentException("closeParenToken");
			}
			if (statement == null)
			{
				throw new ArgumentNullException("statement");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ForEachVariableStatementSyntax)SyntaxFactory.ForEachVariableStatement(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), (SyntaxToken)awaitKeyword.Node, (SyntaxToken)forEachKeyword.Node, (SyntaxToken)openParenToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)variable.Green, (SyntaxToken)inKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green, (SyntaxToken)closeParenToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.StatementSyntax)statement.Green).CreateRed();
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x000E37D0 File Offset: 0x000E19D0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ForEachVariableStatementSyntax ForEachVariableStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax variable, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			return SyntaxFactory.ForEachVariableStatement(attributeLists, default(SyntaxToken), SyntaxFactory.Token(SyntaxKind.ForEachKeyword), SyntaxFactory.Token(SyntaxKind.OpenParenToken), variable, SyntaxFactory.Token(SyntaxKind.InKeyword), expression, SyntaxFactory.Token(SyntaxKind.CloseParenToken), statement);
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x000E3818 File Offset: 0x000E1A18
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ForEachVariableStatementSyntax ForEachVariableStatement(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax variable, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			return SyntaxFactory.ForEachVariableStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxToken), SyntaxFactory.Token(SyntaxKind.ForEachKeyword), SyntaxFactory.Token(SyntaxKind.OpenParenToken), variable, SyntaxFactory.Token(SyntaxKind.InKeyword), expression, SyntaxFactory.Token(SyntaxKind.CloseParenToken), statement);
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x000E3868 File Offset: 0x000E1A68
		public static Microsoft.CodeAnalysis.CSharp.Syntax.UsingStatementSyntax UsingStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxToken awaitKeyword, SyntaxToken usingKeyword, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SyntaxToken closeParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			SyntaxKind syntaxKind = awaitKeyword.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind != SyntaxKind.AwaitKeyword)
			{
				throw new ArgumentException("awaitKeyword");
			}
			if (usingKeyword.Kind() != SyntaxKind.UsingKeyword)
			{
				throw new ArgumentException("usingKeyword");
			}
			if (openParenToken.Kind() != SyntaxKind.OpenParenToken)
			{
				throw new ArgumentException("openParenToken");
			}
			if (closeParenToken.Kind() != SyntaxKind.CloseParenToken)
			{
				throw new ArgumentException("closeParenToken");
			}
			if (statement == null)
			{
				throw new ArgumentNullException("statement");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.UsingStatementSyntax)SyntaxFactory.UsingStatement(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), (SyntaxToken)awaitKeyword.Node, (SyntaxToken)usingKeyword.Node, (SyntaxToken)openParenToken.Node, (declaration == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.VariableDeclarationSyntax)declaration.Green), (expression == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green), (SyntaxToken)closeParenToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.StatementSyntax)statement.Green).CreateRed();
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x000E3968 File Offset: 0x000E1B68
		public static Microsoft.CodeAnalysis.CSharp.Syntax.UsingStatementSyntax UsingStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			return SyntaxFactory.UsingStatement(attributeLists, default(SyntaxToken), SyntaxFactory.Token(SyntaxKind.UsingKeyword), SyntaxFactory.Token(SyntaxKind.OpenParenToken), declaration, expression, SyntaxFactory.Token(SyntaxKind.CloseParenToken), statement);
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x000E39A8 File Offset: 0x000E1BA8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.UsingStatementSyntax UsingStatement(Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			return SyntaxFactory.UsingStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxToken), SyntaxFactory.Token(SyntaxKind.UsingKeyword), SyntaxFactory.Token(SyntaxKind.OpenParenToken), null, null, SyntaxFactory.Token(SyntaxKind.CloseParenToken), statement);
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x000E39F0 File Offset: 0x000E1BF0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.FixedStatementSyntax FixedStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxToken fixedKeyword, SyntaxToken openParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclarationSyntax declaration, SyntaxToken closeParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			if (fixedKeyword.Kind() != SyntaxKind.FixedKeyword)
			{
				throw new ArgumentException("fixedKeyword");
			}
			if (openParenToken.Kind() != SyntaxKind.OpenParenToken)
			{
				throw new ArgumentException("openParenToken");
			}
			if (declaration == null)
			{
				throw new ArgumentNullException("declaration");
			}
			if (closeParenToken.Kind() != SyntaxKind.CloseParenToken)
			{
				throw new ArgumentException("closeParenToken");
			}
			if (statement == null)
			{
				throw new ArgumentNullException("statement");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.FixedStatementSyntax)SyntaxFactory.FixedStatement(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), (SyntaxToken)fixedKeyword.Node, (SyntaxToken)openParenToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.VariableDeclarationSyntax)declaration.Green, (SyntaxToken)closeParenToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.StatementSyntax)statement.Green).CreateRed();
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x000E3AB9 File Offset: 0x000E1CB9
		public static Microsoft.CodeAnalysis.CSharp.Syntax.FixedStatementSyntax FixedStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclarationSyntax declaration, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			return SyntaxFactory.FixedStatement(attributeLists, SyntaxFactory.Token(SyntaxKind.FixedKeyword), SyntaxFactory.Token(SyntaxKind.OpenParenToken), declaration, SyntaxFactory.Token(SyntaxKind.CloseParenToken), statement);
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x000E3AE4 File Offset: 0x000E1CE4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.FixedStatementSyntax FixedStatement(Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclarationSyntax declaration, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			return SyntaxFactory.FixedStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), SyntaxFactory.Token(SyntaxKind.FixedKeyword), SyntaxFactory.Token(SyntaxKind.OpenParenToken), declaration, SyntaxFactory.Token(SyntaxKind.CloseParenToken), statement);
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x000E3B20 File Offset: 0x000E1D20
		public static Microsoft.CodeAnalysis.CSharp.Syntax.CheckedStatementSyntax CheckedStatement(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxToken keyword, Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax block)
		{
			if (kind - SyntaxKind.CheckedStatement > 1)
			{
				throw new ArgumentException("kind");
			}
			SyntaxKind syntaxKind = keyword.Kind();
			if (syntaxKind - SyntaxKind.CheckedKeyword > 1)
			{
				throw new ArgumentException("keyword");
			}
			if (block == null)
			{
				throw new ArgumentNullException("block");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.CheckedStatementSyntax)SyntaxFactory.CheckedStatement(kind, attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), (SyntaxToken)keyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.BlockSyntax)block.Green).CreateRed();
		}

		// Token: 0x06002A79 RID: 10873 RVA: 0x000E3B9F File Offset: 0x000E1D9F
		public static Microsoft.CodeAnalysis.CSharp.Syntax.CheckedStatementSyntax CheckedStatement(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax block)
		{
			return SyntaxFactory.CheckedStatement(kind, attributeLists, SyntaxFactory.Token(SyntaxFactory.GetCheckedStatementKeywordKind(kind)), block);
		}

		// Token: 0x06002A7A RID: 10874 RVA: 0x000E3BB4 File Offset: 0x000E1DB4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.CheckedStatementSyntax CheckedStatement(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax block = null)
		{
			return SyntaxFactory.CheckedStatement(kind, default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), SyntaxFactory.Token(SyntaxFactory.GetCheckedStatementKeywordKind(kind)), block ?? SyntaxFactory.Block(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax>)));
		}

		// Token: 0x06002A7B RID: 10875 RVA: 0x000E3BF0 File Offset: 0x000E1DF0
		private static SyntaxKind GetCheckedStatementKeywordKind(SyntaxKind kind)
		{
			SyntaxKind syntaxKind;
			if (kind != SyntaxKind.CheckedStatement)
			{
				if (kind != SyntaxKind.UncheckedStatement)
				{
					throw new ArgumentOutOfRangeException();
				}
				syntaxKind = SyntaxKind.UncheckedKeyword;
			}
			else
			{
				syntaxKind = SyntaxKind.CheckedKeyword;
			}
			return syntaxKind;
		}

		// Token: 0x06002A7C RID: 10876 RVA: 0x000E3C28 File Offset: 0x000E1E28
		public static Microsoft.CodeAnalysis.CSharp.Syntax.UnsafeStatementSyntax UnsafeStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxToken unsafeKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax block)
		{
			if (unsafeKeyword.Kind() != SyntaxKind.UnsafeKeyword)
			{
				throw new ArgumentException("unsafeKeyword");
			}
			if (block == null)
			{
				throw new ArgumentNullException("block");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.UnsafeStatementSyntax)SyntaxFactory.UnsafeStatement(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), (SyntaxToken)unsafeKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.BlockSyntax)block.Green).CreateRed();
		}

		// Token: 0x06002A7D RID: 10877 RVA: 0x000E3C8D File Offset: 0x000E1E8D
		public static Microsoft.CodeAnalysis.CSharp.Syntax.UnsafeStatementSyntax UnsafeStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax block)
		{
			return SyntaxFactory.UnsafeStatement(attributeLists, SyntaxFactory.Token(SyntaxKind.UnsafeKeyword), block);
		}

		// Token: 0x06002A7E RID: 10878 RVA: 0x000E3CA0 File Offset: 0x000E1EA0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.UnsafeStatementSyntax UnsafeStatement([global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax block = null)
		{
			return SyntaxFactory.UnsafeStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), SyntaxFactory.Token(SyntaxKind.UnsafeKeyword), block ?? SyntaxFactory.Block(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax>)));
		}

		// Token: 0x06002A7F RID: 10879 RVA: 0x000E3CD8 File Offset: 0x000E1ED8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.LockStatementSyntax LockStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxToken lockKeyword, SyntaxToken openParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SyntaxToken closeParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			if (lockKeyword.Kind() != SyntaxKind.LockKeyword)
			{
				throw new ArgumentException("lockKeyword");
			}
			if (openParenToken.Kind() != SyntaxKind.OpenParenToken)
			{
				throw new ArgumentException("openParenToken");
			}
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (closeParenToken.Kind() != SyntaxKind.CloseParenToken)
			{
				throw new ArgumentException("closeParenToken");
			}
			if (statement == null)
			{
				throw new ArgumentNullException("statement");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.LockStatementSyntax)SyntaxFactory.LockStatement(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), (SyntaxToken)lockKeyword.Node, (SyntaxToken)openParenToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green, (SyntaxToken)closeParenToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.StatementSyntax)statement.Green).CreateRed();
		}

		// Token: 0x06002A80 RID: 10880 RVA: 0x000E3DA1 File Offset: 0x000E1FA1
		public static Microsoft.CodeAnalysis.CSharp.Syntax.LockStatementSyntax LockStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			return SyntaxFactory.LockStatement(attributeLists, SyntaxFactory.Token(SyntaxKind.LockKeyword), SyntaxFactory.Token(SyntaxKind.OpenParenToken), expression, SyntaxFactory.Token(SyntaxKind.CloseParenToken), statement);
		}

		// Token: 0x06002A81 RID: 10881 RVA: 0x000E3DCC File Offset: 0x000E1FCC
		public static Microsoft.CodeAnalysis.CSharp.Syntax.LockStatementSyntax LockStatement(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			return SyntaxFactory.LockStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), SyntaxFactory.Token(SyntaxKind.LockKeyword), SyntaxFactory.Token(SyntaxKind.OpenParenToken), expression, SyntaxFactory.Token(SyntaxKind.CloseParenToken), statement);
		}

		// Token: 0x06002A82 RID: 10882 RVA: 0x000E3E08 File Offset: 0x000E2008
		public static Microsoft.CodeAnalysis.CSharp.Syntax.IfStatementSyntax IfStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxToken ifKeyword, SyntaxToken openParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax condition, SyntaxToken closeParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ElseClauseSyntax @else)
		{
			if (ifKeyword.Kind() != SyntaxKind.IfKeyword)
			{
				throw new ArgumentException("ifKeyword");
			}
			if (openParenToken.Kind() != SyntaxKind.OpenParenToken)
			{
				throw new ArgumentException("openParenToken");
			}
			if (condition == null)
			{
				throw new ArgumentNullException("condition");
			}
			if (closeParenToken.Kind() != SyntaxKind.CloseParenToken)
			{
				throw new ArgumentException("closeParenToken");
			}
			if (statement == null)
			{
				throw new ArgumentNullException("statement");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.IfStatementSyntax)SyntaxFactory.IfStatement(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), (SyntaxToken)ifKeyword.Node, (SyntaxToken)openParenToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)condition.Green, (SyntaxToken)closeParenToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.StatementSyntax)statement.Green, (@else == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ElseClauseSyntax)@else.Green)).CreateRed();
		}

		// Token: 0x06002A83 RID: 10883 RVA: 0x000E3EE4 File Offset: 0x000E20E4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.IfStatementSyntax IfStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax condition, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ElseClauseSyntax @else)
		{
			return SyntaxFactory.IfStatement(attributeLists, SyntaxFactory.Token(SyntaxKind.IfKeyword), SyntaxFactory.Token(SyntaxKind.OpenParenToken), condition, SyntaxFactory.Token(SyntaxKind.CloseParenToken), statement, @else);
		}

		// Token: 0x06002A84 RID: 10884 RVA: 0x000E3F10 File Offset: 0x000E2110
		public static Microsoft.CodeAnalysis.CSharp.Syntax.IfStatementSyntax IfStatement(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax condition, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			return SyntaxFactory.IfStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), SyntaxFactory.Token(SyntaxKind.IfKeyword), SyntaxFactory.Token(SyntaxKind.OpenParenToken), condition, SyntaxFactory.Token(SyntaxKind.CloseParenToken), statement, null);
		}

		// Token: 0x06002A85 RID: 10885 RVA: 0x000E3F4C File Offset: 0x000E214C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ElseClauseSyntax ElseClause(SyntaxToken elseKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			if (elseKeyword.Kind() != SyntaxKind.ElseKeyword)
			{
				throw new ArgumentException("elseKeyword");
			}
			if (statement == null)
			{
				throw new ArgumentNullException("statement");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ElseClauseSyntax)SyntaxFactory.ElseClause((SyntaxToken)elseKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.StatementSyntax)statement.Green).CreateRed();
		}

		// Token: 0x06002A86 RID: 10886 RVA: 0x000E3FA5 File Offset: 0x000E21A5
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ElseClauseSyntax ElseClause(Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			return SyntaxFactory.ElseClause(SyntaxFactory.Token(SyntaxKind.ElseKeyword), statement);
		}

		// Token: 0x06002A87 RID: 10887 RVA: 0x000E3FB8 File Offset: 0x000E21B8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.SwitchStatementSyntax SwitchStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxToken switchKeyword, SyntaxToken openParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SyntaxToken closeParenToken, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.SwitchSectionSyntax> sections, SyntaxToken closeBraceToken)
		{
			if (switchKeyword.Kind() != SyntaxKind.SwitchKeyword)
			{
				throw new ArgumentException("switchKeyword");
			}
			SyntaxKind syntaxKind = openParenToken.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind != SyntaxKind.OpenParenToken)
			{
				throw new ArgumentException("openParenToken");
			}
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			syntaxKind = closeParenToken.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind != SyntaxKind.CloseParenToken)
			{
				throw new ArgumentException("closeParenToken");
			}
			if (openBraceToken.Kind() != SyntaxKind.OpenBraceToken)
			{
				throw new ArgumentException("openBraceToken");
			}
			if (closeBraceToken.Kind() != SyntaxKind.CloseBraceToken)
			{
				throw new ArgumentException("closeBraceToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.SwitchStatementSyntax)SyntaxFactory.SwitchStatement(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), (SyntaxToken)switchKeyword.Node, (SyntaxToken)openParenToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green, (SyntaxToken)closeParenToken.Node, (SyntaxToken)openBraceToken.Node, sections.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.SwitchSectionSyntax>(), (SyntaxToken)closeBraceToken.Node).CreateRed();
		}

		// Token: 0x06002A88 RID: 10888 RVA: 0x000E40C6 File Offset: 0x000E22C6
		public static Microsoft.CodeAnalysis.CSharp.Syntax.SwitchSectionSyntax SwitchSection([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.SwitchLabelSyntax> labels, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax> statements)
		{
			return (Microsoft.CodeAnalysis.CSharp.Syntax.SwitchSectionSyntax)SyntaxFactory.SwitchSection(labels.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.SwitchLabelSyntax>(), statements.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.StatementSyntax>()).CreateRed();
		}

		// Token: 0x06002A89 RID: 10889 RVA: 0x000E40F0 File Offset: 0x000E22F0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.SwitchSectionSyntax SwitchSection()
		{
			return SyntaxFactory.SwitchSection(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.SwitchLabelSyntax>), default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax>));
		}

		// Token: 0x06002A8A RID: 10890 RVA: 0x000E4114 File Offset: 0x000E2314
		public static Microsoft.CodeAnalysis.CSharp.Syntax.CasePatternSwitchLabelSyntax CasePatternSwitchLabel(SyntaxToken keyword, Microsoft.CodeAnalysis.CSharp.Syntax.PatternSyntax pattern, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.WhenClauseSyntax whenClause, SyntaxToken colonToken)
		{
			if (keyword.Kind() != SyntaxKind.CaseKeyword)
			{
				throw new ArgumentException("keyword");
			}
			if (pattern == null)
			{
				throw new ArgumentNullException("pattern");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.CasePatternSwitchLabelSyntax)SyntaxFactory.CasePatternSwitchLabel((SyntaxToken)keyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.PatternSyntax)pattern.Green, (whenClause == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.WhenClauseSyntax)whenClause.Green), (SyntaxToken)colonToken.Node).CreateRed();
		}

		// Token: 0x06002A8B RID: 10891 RVA: 0x000E418A File Offset: 0x000E238A
		public static Microsoft.CodeAnalysis.CSharp.Syntax.CasePatternSwitchLabelSyntax CasePatternSwitchLabel(Microsoft.CodeAnalysis.CSharp.Syntax.PatternSyntax pattern, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.WhenClauseSyntax whenClause, SyntaxToken colonToken)
		{
			return SyntaxFactory.CasePatternSwitchLabel(SyntaxFactory.Token(SyntaxKind.CaseKeyword), pattern, whenClause, colonToken);
		}

		// Token: 0x06002A8C RID: 10892 RVA: 0x000E419E File Offset: 0x000E239E
		public static Microsoft.CodeAnalysis.CSharp.Syntax.CasePatternSwitchLabelSyntax CasePatternSwitchLabel(Microsoft.CodeAnalysis.CSharp.Syntax.PatternSyntax pattern, SyntaxToken colonToken)
		{
			return SyntaxFactory.CasePatternSwitchLabel(SyntaxFactory.Token(SyntaxKind.CaseKeyword), pattern, null, colonToken);
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x000E41B4 File Offset: 0x000E23B4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.CaseSwitchLabelSyntax CaseSwitchLabel(SyntaxToken keyword, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax value, SyntaxToken colonToken)
		{
			if (keyword.Kind() != SyntaxKind.CaseKeyword)
			{
				throw new ArgumentException("keyword");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.CaseSwitchLabelSyntax)SyntaxFactory.CaseSwitchLabel((SyntaxToken)keyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)value.Green, (SyntaxToken)colonToken.Node).CreateRed();
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x000E4219 File Offset: 0x000E2419
		public static Microsoft.CodeAnalysis.CSharp.Syntax.CaseSwitchLabelSyntax CaseSwitchLabel(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax value, SyntaxToken colonToken)
		{
			return SyntaxFactory.CaseSwitchLabel(SyntaxFactory.Token(SyntaxKind.CaseKeyword), value, colonToken);
		}

		// Token: 0x06002A8F RID: 10895 RVA: 0x000E422C File Offset: 0x000E242C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DefaultSwitchLabelSyntax DefaultSwitchLabel(SyntaxToken keyword, SyntaxToken colonToken)
		{
			if (keyword.Kind() != SyntaxKind.DefaultKeyword)
			{
				throw new ArgumentException("keyword");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.DefaultSwitchLabelSyntax)SyntaxFactory.DefaultSwitchLabel((SyntaxToken)keyword.Node, (SyntaxToken)colonToken.Node).CreateRed();
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x000E4278 File Offset: 0x000E2478
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DefaultSwitchLabelSyntax DefaultSwitchLabel(SyntaxToken colonToken)
		{
			return SyntaxFactory.DefaultSwitchLabel(SyntaxFactory.Token(SyntaxKind.DefaultKeyword), colonToken);
		}

		// Token: 0x06002A91 RID: 10897 RVA: 0x000E428C File Offset: 0x000E248C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.SwitchExpressionSyntax SwitchExpression(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax governingExpression, SyntaxToken switchKeyword, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.SwitchExpressionArmSyntax> arms, SyntaxToken closeBraceToken)
		{
			if (governingExpression == null)
			{
				throw new ArgumentNullException("governingExpression");
			}
			if (switchKeyword.Kind() != SyntaxKind.SwitchKeyword)
			{
				throw new ArgumentException("switchKeyword");
			}
			if (openBraceToken.Kind() != SyntaxKind.OpenBraceToken)
			{
				throw new ArgumentException("openBraceToken");
			}
			if (closeBraceToken.Kind() != SyntaxKind.CloseBraceToken)
			{
				throw new ArgumentException("closeBraceToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.SwitchExpressionSyntax)SyntaxFactory.SwitchExpression((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)governingExpression.Green, (SyntaxToken)switchKeyword.Node, (SyntaxToken)openBraceToken.Node, arms.Node.ToGreenSeparatedList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.SwitchExpressionArmSyntax>(), (SyntaxToken)closeBraceToken.Node).CreateRed();
		}

		// Token: 0x06002A92 RID: 10898 RVA: 0x000E433A File Offset: 0x000E253A
		public static Microsoft.CodeAnalysis.CSharp.Syntax.SwitchExpressionSyntax SwitchExpression(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax governingExpression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.SwitchExpressionArmSyntax> arms)
		{
			return SyntaxFactory.SwitchExpression(governingExpression, SyntaxFactory.Token(SyntaxKind.SwitchKeyword), SyntaxFactory.Token(SyntaxKind.OpenBraceToken), arms, SyntaxFactory.Token(SyntaxKind.CloseBraceToken));
		}

		// Token: 0x06002A93 RID: 10899 RVA: 0x000E4364 File Offset: 0x000E2564
		public static Microsoft.CodeAnalysis.CSharp.Syntax.SwitchExpressionSyntax SwitchExpression(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax governingExpression)
		{
			return SyntaxFactory.SwitchExpression(governingExpression, SyntaxFactory.Token(SyntaxKind.SwitchKeyword), SyntaxFactory.Token(SyntaxKind.OpenBraceToken), default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.SwitchExpressionArmSyntax>), SyntaxFactory.Token(SyntaxKind.CloseBraceToken));
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x000E43A0 File Offset: 0x000E25A0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.SwitchExpressionArmSyntax SwitchExpressionArm(Microsoft.CodeAnalysis.CSharp.Syntax.PatternSyntax pattern, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.WhenClauseSyntax whenClause, SyntaxToken equalsGreaterThanToken, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			if (pattern == null)
			{
				throw new ArgumentNullException("pattern");
			}
			if (equalsGreaterThanToken.Kind() != SyntaxKind.EqualsGreaterThanToken)
			{
				throw new ArgumentException("equalsGreaterThanToken");
			}
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.SwitchExpressionArmSyntax)SyntaxFactory.SwitchExpressionArm((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.PatternSyntax)pattern.Green, (whenClause == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.WhenClauseSyntax)whenClause.Green), (SyntaxToken)equalsGreaterThanToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green).CreateRed();
		}

		// Token: 0x06002A95 RID: 10901 RVA: 0x000E4423 File Offset: 0x000E2623
		public static Microsoft.CodeAnalysis.CSharp.Syntax.SwitchExpressionArmSyntax SwitchExpressionArm(Microsoft.CodeAnalysis.CSharp.Syntax.PatternSyntax pattern, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.WhenClauseSyntax whenClause, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			return SyntaxFactory.SwitchExpressionArm(pattern, whenClause, SyntaxFactory.Token(SyntaxKind.EqualsGreaterThanToken), expression);
		}

		// Token: 0x06002A96 RID: 10902 RVA: 0x000E4437 File Offset: 0x000E2637
		public static Microsoft.CodeAnalysis.CSharp.Syntax.SwitchExpressionArmSyntax SwitchExpressionArm(Microsoft.CodeAnalysis.CSharp.Syntax.PatternSyntax pattern, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			return SyntaxFactory.SwitchExpressionArm(pattern, null, SyntaxFactory.Token(SyntaxKind.EqualsGreaterThanToken), expression);
		}

		// Token: 0x06002A97 RID: 10903 RVA: 0x000E444C File Offset: 0x000E264C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.TryStatementSyntax TryStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxToken tryKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.CatchClauseSyntax> catches, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.FinallyClauseSyntax @finally)
		{
			if (tryKeyword.Kind() != SyntaxKind.TryKeyword)
			{
				throw new ArgumentException("tryKeyword");
			}
			if (block == null)
			{
				throw new ArgumentNullException("block");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.TryStatementSyntax)SyntaxFactory.TryStatement(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), (SyntaxToken)tryKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.BlockSyntax)block.Green, catches.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.CatchClauseSyntax>(), (@finally == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.FinallyClauseSyntax)@finally.Green)).CreateRed();
		}

		// Token: 0x06002A98 RID: 10904 RVA: 0x000E44D0 File Offset: 0x000E26D0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.TryStatementSyntax TryStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.CatchClauseSyntax> catches, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.FinallyClauseSyntax @finally)
		{
			return SyntaxFactory.TryStatement(attributeLists, SyntaxFactory.Token(SyntaxKind.TryKeyword), block, catches, @finally);
		}

		// Token: 0x06002A99 RID: 10905 RVA: 0x000E44E8 File Offset: 0x000E26E8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.TryStatementSyntax TryStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.CatchClauseSyntax> catches = default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.CatchClauseSyntax>))
		{
			return SyntaxFactory.TryStatement(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), SyntaxFactory.Token(SyntaxKind.TryKeyword), SyntaxFactory.Block(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax>)), catches, null);
		}

		// Token: 0x06002A9A RID: 10906 RVA: 0x000E4520 File Offset: 0x000E2720
		public static Microsoft.CodeAnalysis.CSharp.Syntax.CatchClauseSyntax CatchClause(SyntaxToken catchKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.CatchDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.CatchFilterClauseSyntax filter, Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax block)
		{
			if (catchKeyword.Kind() != SyntaxKind.CatchKeyword)
			{
				throw new ArgumentException("catchKeyword");
			}
			if (block == null)
			{
				throw new ArgumentNullException("block");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.CatchClauseSyntax)SyntaxFactory.CatchClause((SyntaxToken)catchKeyword.Node, (declaration == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.CatchDeclarationSyntax)declaration.Green), (filter == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.CatchFilterClauseSyntax)filter.Green), (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.BlockSyntax)block.Green).CreateRed();
		}

		// Token: 0x06002A9B RID: 10907 RVA: 0x000E459B File Offset: 0x000E279B
		public static Microsoft.CodeAnalysis.CSharp.Syntax.CatchClauseSyntax CatchClause([global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.CatchDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.CatchFilterClauseSyntax filter, Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax block)
		{
			return SyntaxFactory.CatchClause(SyntaxFactory.Token(SyntaxKind.CatchKeyword), declaration, filter, block);
		}

		// Token: 0x06002A9C RID: 10908 RVA: 0x000E45B0 File Offset: 0x000E27B0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.CatchClauseSyntax CatchClause()
		{
			return SyntaxFactory.CatchClause(SyntaxFactory.Token(SyntaxKind.CatchKeyword), null, null, SyntaxFactory.Block(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax>)));
		}

		// Token: 0x06002A9D RID: 10909 RVA: 0x000E45DC File Offset: 0x000E27DC
		public static Microsoft.CodeAnalysis.CSharp.Syntax.CatchDeclarationSyntax CatchDeclaration(SyntaxToken openParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, SyntaxToken identifier, SyntaxToken closeParenToken)
		{
			if (openParenToken.Kind() != SyntaxKind.OpenParenToken)
			{
				throw new ArgumentException("openParenToken");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			SyntaxKind syntaxKind = identifier.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind != SyntaxKind.IdentifierToken)
			{
				throw new ArgumentException("identifier");
			}
			if (closeParenToken.Kind() != SyntaxKind.CloseParenToken)
			{
				throw new ArgumentException("closeParenToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.CatchDeclarationSyntax)SyntaxFactory.CatchDeclaration((SyntaxToken)openParenToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)type.Green, (SyntaxToken)identifier.Node, (SyntaxToken)closeParenToken.Node).CreateRed();
		}

		// Token: 0x06002A9E RID: 10910 RVA: 0x000E4682 File Offset: 0x000E2882
		public static Microsoft.CodeAnalysis.CSharp.Syntax.CatchDeclarationSyntax CatchDeclaration(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, SyntaxToken identifier)
		{
			return SyntaxFactory.CatchDeclaration(SyntaxFactory.Token(SyntaxKind.OpenParenToken), type, identifier, SyntaxFactory.Token(SyntaxKind.CloseParenToken));
		}

		// Token: 0x06002A9F RID: 10911 RVA: 0x000E46A0 File Offset: 0x000E28A0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.CatchDeclarationSyntax CatchDeclaration(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type)
		{
			return SyntaxFactory.CatchDeclaration(SyntaxFactory.Token(SyntaxKind.OpenParenToken), type, default(SyntaxToken), SyntaxFactory.Token(SyntaxKind.CloseParenToken));
		}

		// Token: 0x06002AA0 RID: 10912 RVA: 0x000E46D0 File Offset: 0x000E28D0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.CatchFilterClauseSyntax CatchFilterClause(SyntaxToken whenKeyword, SyntaxToken openParenToken, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax filterExpression, SyntaxToken closeParenToken)
		{
			if (whenKeyword.Kind() != SyntaxKind.WhenKeyword)
			{
				throw new ArgumentException("whenKeyword");
			}
			if (openParenToken.Kind() != SyntaxKind.OpenParenToken)
			{
				throw new ArgumentException("openParenToken");
			}
			if (filterExpression == null)
			{
				throw new ArgumentNullException("filterExpression");
			}
			if (closeParenToken.Kind() != SyntaxKind.CloseParenToken)
			{
				throw new ArgumentException("closeParenToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.CatchFilterClauseSyntax)SyntaxFactory.CatchFilterClause((SyntaxToken)whenKeyword.Node, (SyntaxToken)openParenToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)filterExpression.Green, (SyntaxToken)closeParenToken.Node).CreateRed();
		}

		// Token: 0x06002AA1 RID: 10913 RVA: 0x000E4771 File Offset: 0x000E2971
		public static Microsoft.CodeAnalysis.CSharp.Syntax.CatchFilterClauseSyntax CatchFilterClause(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax filterExpression)
		{
			return SyntaxFactory.CatchFilterClause(SyntaxFactory.Token(SyntaxKind.WhenKeyword), SyntaxFactory.Token(SyntaxKind.OpenParenToken), filterExpression, SyntaxFactory.Token(SyntaxKind.CloseParenToken));
		}

		// Token: 0x06002AA2 RID: 10914 RVA: 0x000E4798 File Offset: 0x000E2998
		public static Microsoft.CodeAnalysis.CSharp.Syntax.FinallyClauseSyntax FinallyClause(SyntaxToken finallyKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax block)
		{
			if (finallyKeyword.Kind() != SyntaxKind.FinallyKeyword)
			{
				throw new ArgumentException("finallyKeyword");
			}
			if (block == null)
			{
				throw new ArgumentNullException("block");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.FinallyClauseSyntax)SyntaxFactory.FinallyClause((SyntaxToken)finallyKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.BlockSyntax)block.Green).CreateRed();
		}

		// Token: 0x06002AA3 RID: 10915 RVA: 0x000E47F4 File Offset: 0x000E29F4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.FinallyClauseSyntax FinallyClause([global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax block = null)
		{
			return SyntaxFactory.FinallyClause(SyntaxFactory.Token(SyntaxKind.FinallyKeyword), block ?? SyntaxFactory.Block(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax>)));
		}

		// Token: 0x06002AA4 RID: 10916 RVA: 0x000E4824 File Offset: 0x000E2A24
		public static Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax CompilationUnit([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ExternAliasDirectiveSyntax> externs, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.UsingDirectiveSyntax> usings, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax> members, SyntaxToken endOfFileToken)
		{
			if (endOfFileToken.Kind() != SyntaxKind.EndOfFileToken)
			{
				throw new ArgumentException("endOfFileToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax)SyntaxFactory.CompilationUnit(externs.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExternAliasDirectiveSyntax>(), usings.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.UsingDirectiveSyntax>(), attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), members.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.MemberDeclarationSyntax>(), (SyntaxToken)endOfFileToken.Node).CreateRed();
		}

		// Token: 0x06002AA5 RID: 10917 RVA: 0x000E4895 File Offset: 0x000E2A95
		public static Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax CompilationUnit([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ExternAliasDirectiveSyntax> externs, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.UsingDirectiveSyntax> usings, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax> members)
		{
			return SyntaxFactory.CompilationUnit(externs, usings, attributeLists, members, SyntaxFactory.Token(SyntaxKind.EndOfFileToken));
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x000E48AC File Offset: 0x000E2AAC
		public static Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax CompilationUnit()
		{
			return SyntaxFactory.CompilationUnit(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ExternAliasDirectiveSyntax>), default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.UsingDirectiveSyntax>), default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax>), SyntaxFactory.Token(SyntaxKind.EndOfFileToken));
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x000E48EC File Offset: 0x000E2AEC
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ExternAliasDirectiveSyntax ExternAliasDirective(SyntaxToken externKeyword, SyntaxToken aliasKeyword, SyntaxToken identifier, SyntaxToken semicolonToken)
		{
			if (externKeyword.Kind() != SyntaxKind.ExternKeyword)
			{
				throw new ArgumentException("externKeyword");
			}
			if (aliasKeyword.Kind() != SyntaxKind.AliasKeyword)
			{
				throw new ArgumentException("aliasKeyword");
			}
			if (identifier.Kind() != SyntaxKind.IdentifierToken)
			{
				throw new ArgumentException("identifier");
			}
			if (semicolonToken.Kind() != SyntaxKind.SemicolonToken)
			{
				throw new ArgumentException("semicolonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ExternAliasDirectiveSyntax)SyntaxFactory.ExternAliasDirective((SyntaxToken)externKeyword.Node, (SyntaxToken)aliasKeyword.Node, (SyntaxToken)identifier.Node, (SyntaxToken)semicolonToken.Node).CreateRed();
		}

		// Token: 0x06002AA8 RID: 10920 RVA: 0x000E4998 File Offset: 0x000E2B98
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ExternAliasDirectiveSyntax ExternAliasDirective(SyntaxToken identifier)
		{
			return SyntaxFactory.ExternAliasDirective(SyntaxFactory.Token(SyntaxKind.ExternKeyword), SyntaxFactory.Token(SyntaxKind.AliasKeyword), identifier, SyntaxFactory.Token(SyntaxKind.SemicolonToken));
		}

		// Token: 0x06002AA9 RID: 10921 RVA: 0x000E49BE File Offset: 0x000E2BBE
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ExternAliasDirectiveSyntax ExternAliasDirective(string identifier)
		{
			return SyntaxFactory.ExternAliasDirective(SyntaxFactory.Token(SyntaxKind.ExternKeyword), SyntaxFactory.Token(SyntaxKind.AliasKeyword), SyntaxFactory.Identifier(identifier), SyntaxFactory.Token(SyntaxKind.SemicolonToken));
		}

		// Token: 0x06002AAA RID: 10922 RVA: 0x000E49EC File Offset: 0x000E2BEC
		public static Microsoft.CodeAnalysis.CSharp.Syntax.UsingDirectiveSyntax UsingDirective(SyntaxToken globalKeyword, SyntaxToken usingKeyword, SyntaxToken staticKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.NameEqualsSyntax alias, Microsoft.CodeAnalysis.CSharp.Syntax.NameSyntax name, SyntaxToken semicolonToken)
		{
			SyntaxKind syntaxKind = globalKeyword.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind != SyntaxKind.GlobalKeyword)
			{
				throw new ArgumentException("globalKeyword");
			}
			if (usingKeyword.Kind() != SyntaxKind.UsingKeyword)
			{
				throw new ArgumentException("usingKeyword");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (semicolonToken.Kind() != SyntaxKind.SemicolonToken)
			{
				throw new ArgumentException("semicolonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.UsingDirectiveSyntax)SyntaxFactory.UsingDirective((SyntaxToken)globalKeyword.Node, (SyntaxToken)usingKeyword.Node, (SyntaxToken)staticKeyword.Node, (alias == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.NameEqualsSyntax)alias.Green), (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.NameSyntax)name.Green, (SyntaxToken)semicolonToken.Node).CreateRed();
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x000E4AB4 File Offset: 0x000E2CB4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.UsingDirectiveSyntax UsingDirective(SyntaxToken staticKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.NameEqualsSyntax alias, Microsoft.CodeAnalysis.CSharp.Syntax.NameSyntax name)
		{
			return SyntaxFactory.UsingDirective(default(SyntaxToken), SyntaxFactory.Token(SyntaxKind.UsingKeyword), staticKeyword, alias, name, SyntaxFactory.Token(SyntaxKind.SemicolonToken));
		}

		// Token: 0x06002AAC RID: 10924 RVA: 0x000E4AE8 File Offset: 0x000E2CE8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.UsingDirectiveSyntax UsingDirective(Microsoft.CodeAnalysis.CSharp.Syntax.NameSyntax name)
		{
			return SyntaxFactory.UsingDirective(default(SyntaxToken), SyntaxFactory.Token(SyntaxKind.UsingKeyword), default(SyntaxToken), null, name, SyntaxFactory.Token(SyntaxKind.SemicolonToken));
		}

		// Token: 0x06002AAD RID: 10925 RVA: 0x000E4B24 File Offset: 0x000E2D24
		public static Microsoft.CodeAnalysis.CSharp.Syntax.NamespaceDeclarationSyntax NamespaceDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken namespaceKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.NameSyntax name, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ExternAliasDirectiveSyntax> externs, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.UsingDirectiveSyntax> usings, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
		{
			if (namespaceKeyword.Kind() != SyntaxKind.NamespaceKeyword)
			{
				throw new ArgumentException("namespaceKeyword");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (openBraceToken.Kind() != SyntaxKind.OpenBraceToken)
			{
				throw new ArgumentException("openBraceToken");
			}
			if (closeBraceToken.Kind() != SyntaxKind.CloseBraceToken)
			{
				throw new ArgumentException("closeBraceToken");
			}
			SyntaxKind syntaxKind = semicolonToken.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind != SyntaxKind.SemicolonToken)
			{
				throw new ArgumentException("semicolonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.NamespaceDeclarationSyntax)SyntaxFactory.NamespaceDeclaration(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), modifiers.Node.ToGreenList<SyntaxToken>(), (SyntaxToken)namespaceKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.NameSyntax)name.Green, (SyntaxToken)openBraceToken.Node, externs.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExternAliasDirectiveSyntax>(), usings.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.UsingDirectiveSyntax>(), members.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.MemberDeclarationSyntax>(), (SyntaxToken)closeBraceToken.Node, (SyntaxToken)semicolonToken.Node).CreateRed();
		}

		// Token: 0x06002AAE RID: 10926 RVA: 0x000E4C30 File Offset: 0x000E2E30
		public static Microsoft.CodeAnalysis.CSharp.Syntax.NamespaceDeclarationSyntax NamespaceDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.NameSyntax name, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ExternAliasDirectiveSyntax> externs, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.UsingDirectiveSyntax> usings, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax> members)
		{
			return SyntaxFactory.NamespaceDeclaration(attributeLists, modifiers, SyntaxFactory.Token(SyntaxKind.NamespaceKeyword), name, SyntaxFactory.Token(SyntaxKind.OpenBraceToken), externs, usings, members, SyntaxFactory.Token(SyntaxKind.CloseBraceToken), default(SyntaxToken));
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x000E4C74 File Offset: 0x000E2E74
		public static Microsoft.CodeAnalysis.CSharp.Syntax.NamespaceDeclarationSyntax NamespaceDeclaration(Microsoft.CodeAnalysis.CSharp.Syntax.NameSyntax name)
		{
			return SyntaxFactory.NamespaceDeclaration(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), SyntaxFactory.Token(SyntaxKind.NamespaceKeyword), name, SyntaxFactory.Token(SyntaxKind.OpenBraceToken), default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ExternAliasDirectiveSyntax>), default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.UsingDirectiveSyntax>), default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax>), SyntaxFactory.Token(SyntaxKind.CloseBraceToken), default(SyntaxToken));
		}

		// Token: 0x06002AB0 RID: 10928 RVA: 0x000E4CE0 File Offset: 0x000E2EE0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax AttributeList(SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.AttributeTargetSpecifierSyntax target, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeSyntax> attributes, SyntaxToken closeBracketToken)
		{
			if (openBracketToken.Kind() != SyntaxKind.OpenBracketToken)
			{
				throw new ArgumentException("openBracketToken");
			}
			if (closeBracketToken.Kind() != SyntaxKind.CloseBracketToken)
			{
				throw new ArgumentException("closeBracketToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax)SyntaxFactory.AttributeList((SyntaxToken)openBracketToken.Node, (target == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeTargetSpecifierSyntax)target.Green), attributes.Node.ToGreenSeparatedList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeSyntax>(), (SyntaxToken)closeBracketToken.Node).CreateRed();
		}

		// Token: 0x06002AB1 RID: 10929 RVA: 0x000E4D61 File Offset: 0x000E2F61
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax AttributeList([global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.AttributeTargetSpecifierSyntax target, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeSyntax> attributes)
		{
			return SyntaxFactory.AttributeList(SyntaxFactory.Token(SyntaxKind.OpenBracketToken), target, attributes, SyntaxFactory.Token(SyntaxKind.CloseBracketToken));
		}

		// Token: 0x06002AB2 RID: 10930 RVA: 0x000E4D7E File Offset: 0x000E2F7E
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax AttributeList([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeSyntax> attributes = default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeSyntax>))
		{
			return SyntaxFactory.AttributeList(SyntaxFactory.Token(SyntaxKind.OpenBracketToken), null, attributes, SyntaxFactory.Token(SyntaxKind.CloseBracketToken));
		}

		// Token: 0x06002AB3 RID: 10931 RVA: 0x000E4D9C File Offset: 0x000E2F9C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AttributeTargetSpecifierSyntax AttributeTargetSpecifier(SyntaxToken identifier, SyntaxToken colonToken)
		{
			if (colonToken.Kind() != SyntaxKind.ColonToken)
			{
				throw new ArgumentException("colonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.AttributeTargetSpecifierSyntax)SyntaxFactory.AttributeTargetSpecifier((SyntaxToken)identifier.Node, (SyntaxToken)colonToken.Node).CreateRed();
		}

		// Token: 0x06002AB4 RID: 10932 RVA: 0x000E4DE8 File Offset: 0x000E2FE8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AttributeTargetSpecifierSyntax AttributeTargetSpecifier(SyntaxToken identifier)
		{
			return SyntaxFactory.AttributeTargetSpecifier(identifier, SyntaxFactory.Token(SyntaxKind.ColonToken));
		}

		// Token: 0x06002AB5 RID: 10933 RVA: 0x000E4DFA File Offset: 0x000E2FFA
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AttributeSyntax Attribute(Microsoft.CodeAnalysis.CSharp.Syntax.NameSyntax name, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.AttributeArgumentListSyntax argumentList)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.AttributeSyntax)SyntaxFactory.Attribute((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.NameSyntax)name.Green, (argumentList == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeArgumentListSyntax)argumentList.Green)).CreateRed();
		}

		// Token: 0x06002AB6 RID: 10934 RVA: 0x000E4E35 File Offset: 0x000E3035
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AttributeSyntax Attribute(Microsoft.CodeAnalysis.CSharp.Syntax.NameSyntax name)
		{
			return SyntaxFactory.Attribute(name, null);
		}

		// Token: 0x06002AB7 RID: 10935 RVA: 0x000E4E40 File Offset: 0x000E3040
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AttributeArgumentListSyntax AttributeArgumentList(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeArgumentSyntax> arguments, SyntaxToken closeParenToken)
		{
			if (openParenToken.Kind() != SyntaxKind.OpenParenToken)
			{
				throw new ArgumentException("openParenToken");
			}
			if (closeParenToken.Kind() != SyntaxKind.CloseParenToken)
			{
				throw new ArgumentException("closeParenToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.AttributeArgumentListSyntax)SyntaxFactory.AttributeArgumentList((SyntaxToken)openParenToken.Node, arguments.Node.ToGreenSeparatedList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeArgumentSyntax>(), (SyntaxToken)closeParenToken.Node).CreateRed();
		}

		// Token: 0x06002AB8 RID: 10936 RVA: 0x000E4EB0 File Offset: 0x000E30B0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AttributeArgumentListSyntax AttributeArgumentList([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeArgumentSyntax> arguments = default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeArgumentSyntax>))
		{
			return SyntaxFactory.AttributeArgumentList(SyntaxFactory.Token(SyntaxKind.OpenParenToken), arguments, SyntaxFactory.Token(SyntaxKind.CloseParenToken));
		}

		// Token: 0x06002AB9 RID: 10937 RVA: 0x000E4ECC File Offset: 0x000E30CC
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AttributeArgumentSyntax AttributeArgument([global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.NameEqualsSyntax nameEquals, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.NameColonSyntax nameColon, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.AttributeArgumentSyntax)SyntaxFactory.AttributeArgument((nameEquals == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.NameEqualsSyntax)nameEquals.Green), (nameColon == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.NameColonSyntax)nameColon.Green), (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green).CreateRed();
		}

		// Token: 0x06002ABA RID: 10938 RVA: 0x000E4F23 File Offset: 0x000E3123
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AttributeArgumentSyntax AttributeArgument(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			return SyntaxFactory.AttributeArgument(null, null, expression);
		}

		// Token: 0x06002ABB RID: 10939 RVA: 0x000E4F30 File Offset: 0x000E3130
		public static Microsoft.CodeAnalysis.CSharp.Syntax.NameEqualsSyntax NameEquals(Microsoft.CodeAnalysis.CSharp.Syntax.IdentifierNameSyntax name, SyntaxToken equalsToken)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (equalsToken.Kind() != SyntaxKind.EqualsToken)
			{
				throw new ArgumentException("equalsToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.NameEqualsSyntax)SyntaxFactory.NameEquals((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.IdentifierNameSyntax)name.Green, (SyntaxToken)equalsToken.Node).CreateRed();
		}

		// Token: 0x06002ABC RID: 10940 RVA: 0x000E4F89 File Offset: 0x000E3189
		public static Microsoft.CodeAnalysis.CSharp.Syntax.NameEqualsSyntax NameEquals(Microsoft.CodeAnalysis.CSharp.Syntax.IdentifierNameSyntax name)
		{
			return SyntaxFactory.NameEquals(name, SyntaxFactory.Token(SyntaxKind.EqualsToken));
		}

		// Token: 0x06002ABD RID: 10941 RVA: 0x000E4F9B File Offset: 0x000E319B
		public static Microsoft.CodeAnalysis.CSharp.Syntax.NameEqualsSyntax NameEquals(string name)
		{
			return SyntaxFactory.NameEquals(SyntaxFactory.IdentifierName(name), SyntaxFactory.Token(SyntaxKind.EqualsToken));
		}

		// Token: 0x06002ABE RID: 10942 RVA: 0x000E4FB4 File Offset: 0x000E31B4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterListSyntax TypeParameterList(SyntaxToken lessThanToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterSyntax> parameters, SyntaxToken greaterThanToken)
		{
			if (lessThanToken.Kind() != SyntaxKind.LessThanToken)
			{
				throw new ArgumentException("lessThanToken");
			}
			if (greaterThanToken.Kind() != SyntaxKind.GreaterThanToken)
			{
				throw new ArgumentException("greaterThanToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterListSyntax)SyntaxFactory.TypeParameterList((SyntaxToken)lessThanToken.Node, parameters.Node.ToGreenSeparatedList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeParameterSyntax>(), (SyntaxToken)greaterThanToken.Node).CreateRed();
		}

		// Token: 0x06002ABF RID: 10943 RVA: 0x000E5024 File Offset: 0x000E3224
		public static Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterListSyntax TypeParameterList([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterSyntax> parameters = default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterSyntax>))
		{
			return SyntaxFactory.TypeParameterList(SyntaxFactory.Token(SyntaxKind.LessThanToken), parameters, SyntaxFactory.Token(SyntaxKind.GreaterThanToken));
		}

		// Token: 0x06002AC0 RID: 10944 RVA: 0x000E5040 File Offset: 0x000E3240
		public static Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterSyntax TypeParameter([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxToken varianceKeyword, SyntaxToken identifier)
		{
			SyntaxKind syntaxKind = varianceKeyword.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind - SyntaxKind.OutKeyword > 1)
			{
				throw new ArgumentException("varianceKeyword");
			}
			if (identifier.Kind() != SyntaxKind.IdentifierToken)
			{
				throw new ArgumentException("identifier");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterSyntax)SyntaxFactory.TypeParameter(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), (SyntaxToken)varianceKeyword.Node, (SyntaxToken)identifier.Node).CreateRed();
		}

		// Token: 0x06002AC1 RID: 10945 RVA: 0x000E50B8 File Offset: 0x000E32B8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterSyntax TypeParameter(SyntaxToken identifier)
		{
			return SyntaxFactory.TypeParameter(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxToken), identifier);
		}

		// Token: 0x06002AC2 RID: 10946 RVA: 0x000E50E0 File Offset: 0x000E32E0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterSyntax TypeParameter(string identifier)
		{
			return SyntaxFactory.TypeParameter(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxToken), SyntaxFactory.Identifier(identifier));
		}

		// Token: 0x06002AC3 RID: 10947 RVA: 0x000E510C File Offset: 0x000E330C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ClassDeclarationSyntax ClassDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterListSyntax typeParameterList, Microsoft.CodeAnalysis.CSharp.Syntax.BaseListSyntax baseList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
		{
			if (keyword.Kind() != SyntaxKind.ClassKeyword)
			{
				throw new ArgumentException("keyword");
			}
			if (identifier.Kind() != SyntaxKind.IdentifierToken)
			{
				throw new ArgumentException("identifier");
			}
			if (openBraceToken.Kind() != SyntaxKind.OpenBraceToken)
			{
				throw new ArgumentException("openBraceToken");
			}
			if (closeBraceToken.Kind() != SyntaxKind.CloseBraceToken)
			{
				throw new ArgumentException("closeBraceToken");
			}
			SyntaxKind syntaxKind = semicolonToken.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind != SyntaxKind.SemicolonToken)
			{
				throw new ArgumentException("semicolonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ClassDeclarationSyntax)SyntaxFactory.ClassDeclaration(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), modifiers.Node.ToGreenList<SyntaxToken>(), (SyntaxToken)keyword.Node, (SyntaxToken)identifier.Node, (typeParameterList == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeParameterListSyntax)typeParameterList.Green), (baseList == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.BaseListSyntax)baseList.Green), constraintClauses.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeParameterConstraintClauseSyntax>(), (SyntaxToken)openBraceToken.Node, members.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.MemberDeclarationSyntax>(), (SyntaxToken)closeBraceToken.Node, (SyntaxToken)semicolonToken.Node).CreateRed();
		}

		// Token: 0x06002AC4 RID: 10948 RVA: 0x000E523C File Offset: 0x000E343C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ClassDeclarationSyntax ClassDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterListSyntax typeParameterList, Microsoft.CodeAnalysis.CSharp.Syntax.BaseListSyntax baseList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax> constraintClauses, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax> members)
		{
			return SyntaxFactory.ClassDeclaration(attributeLists, modifiers, SyntaxFactory.Token(SyntaxKind.ClassKeyword), identifier, typeParameterList, baseList, constraintClauses, SyntaxFactory.Token(SyntaxKind.OpenBraceToken), members, SyntaxFactory.Token(SyntaxKind.CloseBraceToken), default(SyntaxToken));
		}

		// Token: 0x06002AC5 RID: 10949 RVA: 0x000E5280 File Offset: 0x000E3480
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ClassDeclarationSyntax ClassDeclaration(SyntaxToken identifier)
		{
			return SyntaxFactory.ClassDeclaration(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), SyntaxFactory.Token(SyntaxKind.ClassKeyword), identifier, null, null, default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax>), SyntaxFactory.Token(SyntaxKind.OpenBraceToken), default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax>), SyntaxFactory.Token(SyntaxKind.CloseBraceToken), default(SyntaxToken));
		}

		// Token: 0x06002AC6 RID: 10950 RVA: 0x000E52E4 File Offset: 0x000E34E4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ClassDeclarationSyntax ClassDeclaration(string identifier)
		{
			return SyntaxFactory.ClassDeclaration(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), SyntaxFactory.Token(SyntaxKind.ClassKeyword), SyntaxFactory.Identifier(identifier), null, null, default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax>), SyntaxFactory.Token(SyntaxKind.OpenBraceToken), default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax>), SyntaxFactory.Token(SyntaxKind.CloseBraceToken), default(SyntaxToken));
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x000E534C File Offset: 0x000E354C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public static Microsoft.CodeAnalysis.CSharp.Syntax.StructDeclarationSyntax StructDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterListSyntax typeParameterList, Microsoft.CodeAnalysis.CSharp.Syntax.BaseListSyntax baseList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
		{
			if (keyword.Kind() != SyntaxKind.StructKeyword)
			{
				throw new ArgumentException("keyword");
			}
			if (identifier.Kind() != SyntaxKind.IdentifierToken)
			{
				throw new ArgumentException("identifier");
			}
			if (openBraceToken.Kind() != SyntaxKind.OpenBraceToken)
			{
				throw new ArgumentException("openBraceToken");
			}
			if (closeBraceToken.Kind() != SyntaxKind.CloseBraceToken)
			{
				throw new ArgumentException("closeBraceToken");
			}
			SyntaxKind syntaxKind = semicolonToken.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind != SyntaxKind.SemicolonToken)
			{
				throw new ArgumentException("semicolonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.StructDeclarationSyntax)SyntaxFactory.StructDeclaration(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), modifiers.Node.ToGreenList<SyntaxToken>(), (SyntaxToken)keyword.Node, (SyntaxToken)identifier.Node, (typeParameterList == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeParameterListSyntax)typeParameterList.Green), (baseList == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.BaseListSyntax)baseList.Green), constraintClauses.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeParameterConstraintClauseSyntax>(), (SyntaxToken)openBraceToken.Node, members.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.MemberDeclarationSyntax>(), (SyntaxToken)closeBraceToken.Node, (SyntaxToken)semicolonToken.Node).CreateRed();
		}

		// Token: 0x06002AC8 RID: 10952 RVA: 0x000E547C File Offset: 0x000E367C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public static Microsoft.CodeAnalysis.CSharp.Syntax.StructDeclarationSyntax StructDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterListSyntax typeParameterList, Microsoft.CodeAnalysis.CSharp.Syntax.BaseListSyntax baseList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax> constraintClauses, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax> members)
		{
			return SyntaxFactory.StructDeclaration(attributeLists, modifiers, SyntaxFactory.Token(SyntaxKind.StructKeyword), identifier, typeParameterList, baseList, constraintClauses, SyntaxFactory.Token(SyntaxKind.OpenBraceToken), members, SyntaxFactory.Token(SyntaxKind.CloseBraceToken), default(SyntaxToken));
		}

		// Token: 0x06002AC9 RID: 10953 RVA: 0x000E54C0 File Offset: 0x000E36C0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.StructDeclarationSyntax StructDeclaration(SyntaxToken identifier)
		{
			return SyntaxFactory.StructDeclaration(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), SyntaxFactory.Token(SyntaxKind.StructKeyword), identifier, null, null, default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax>), SyntaxFactory.Token(SyntaxKind.OpenBraceToken), default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax>), SyntaxFactory.Token(SyntaxKind.CloseBraceToken), default(SyntaxToken));
		}

		// Token: 0x06002ACA RID: 10954 RVA: 0x000E5524 File Offset: 0x000E3724
		public static Microsoft.CodeAnalysis.CSharp.Syntax.StructDeclarationSyntax StructDeclaration(string identifier)
		{
			return SyntaxFactory.StructDeclaration(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), SyntaxFactory.Token(SyntaxKind.StructKeyword), SyntaxFactory.Identifier(identifier), null, null, default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax>), SyntaxFactory.Token(SyntaxKind.OpenBraceToken), default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax>), SyntaxFactory.Token(SyntaxKind.CloseBraceToken), default(SyntaxToken));
		}

		// Token: 0x06002ACB RID: 10955 RVA: 0x000E558C File Offset: 0x000E378C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public static Microsoft.CodeAnalysis.CSharp.Syntax.InterfaceDeclarationSyntax InterfaceDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterListSyntax typeParameterList, Microsoft.CodeAnalysis.CSharp.Syntax.BaseListSyntax baseList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
		{
			if (keyword.Kind() != SyntaxKind.InterfaceKeyword)
			{
				throw new ArgumentException("keyword");
			}
			if (identifier.Kind() != SyntaxKind.IdentifierToken)
			{
				throw new ArgumentException("identifier");
			}
			if (openBraceToken.Kind() != SyntaxKind.OpenBraceToken)
			{
				throw new ArgumentException("openBraceToken");
			}
			if (closeBraceToken.Kind() != SyntaxKind.CloseBraceToken)
			{
				throw new ArgumentException("closeBraceToken");
			}
			SyntaxKind syntaxKind = semicolonToken.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind != SyntaxKind.SemicolonToken)
			{
				throw new ArgumentException("semicolonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.InterfaceDeclarationSyntax)SyntaxFactory.InterfaceDeclaration(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), modifiers.Node.ToGreenList<SyntaxToken>(), (SyntaxToken)keyword.Node, (SyntaxToken)identifier.Node, (typeParameterList == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeParameterListSyntax)typeParameterList.Green), (baseList == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.BaseListSyntax)baseList.Green), constraintClauses.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeParameterConstraintClauseSyntax>(), (SyntaxToken)openBraceToken.Node, members.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.MemberDeclarationSyntax>(), (SyntaxToken)closeBraceToken.Node, (SyntaxToken)semicolonToken.Node).CreateRed();
		}

		// Token: 0x06002ACC RID: 10956 RVA: 0x000E56BC File Offset: 0x000E38BC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public static Microsoft.CodeAnalysis.CSharp.Syntax.InterfaceDeclarationSyntax InterfaceDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterListSyntax typeParameterList, Microsoft.CodeAnalysis.CSharp.Syntax.BaseListSyntax baseList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax> constraintClauses, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax> members)
		{
			return SyntaxFactory.InterfaceDeclaration(attributeLists, modifiers, SyntaxFactory.Token(SyntaxKind.InterfaceKeyword), identifier, typeParameterList, baseList, constraintClauses, SyntaxFactory.Token(SyntaxKind.OpenBraceToken), members, SyntaxFactory.Token(SyntaxKind.CloseBraceToken), default(SyntaxToken));
		}

		// Token: 0x06002ACD RID: 10957 RVA: 0x000E5700 File Offset: 0x000E3900
		public static Microsoft.CodeAnalysis.CSharp.Syntax.InterfaceDeclarationSyntax InterfaceDeclaration(SyntaxToken identifier)
		{
			return SyntaxFactory.InterfaceDeclaration(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), SyntaxFactory.Token(SyntaxKind.InterfaceKeyword), identifier, null, null, default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax>), SyntaxFactory.Token(SyntaxKind.OpenBraceToken), default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax>), SyntaxFactory.Token(SyntaxKind.CloseBraceToken), default(SyntaxToken));
		}

		// Token: 0x06002ACE RID: 10958 RVA: 0x000E5764 File Offset: 0x000E3964
		public static Microsoft.CodeAnalysis.CSharp.Syntax.InterfaceDeclarationSyntax InterfaceDeclaration(string identifier)
		{
			return SyntaxFactory.InterfaceDeclaration(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), SyntaxFactory.Token(SyntaxKind.InterfaceKeyword), SyntaxFactory.Identifier(identifier), null, null, default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax>), SyntaxFactory.Token(SyntaxKind.OpenBraceToken), default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax>), SyntaxFactory.Token(SyntaxKind.CloseBraceToken), default(SyntaxToken));
		}

		// Token: 0x06002ACF RID: 10959 RVA: 0x000E57CC File Offset: 0x000E39CC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public static Microsoft.CodeAnalysis.CSharp.Syntax.RecordDeclarationSyntax RecordDeclaration(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken classOrStructKeyword, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterListSyntax typeParameterList, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, Microsoft.CodeAnalysis.CSharp.Syntax.BaseListSyntax baseList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
		{
			if (kind != SyntaxKind.RecordDeclaration && kind != SyntaxKind.RecordStructDeclaration)
			{
				throw new ArgumentException("kind");
			}
			SyntaxKind syntaxKind = classOrStructKeyword.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind - SyntaxKind.ClassKeyword > 1)
			{
				throw new ArgumentException("classOrStructKeyword");
			}
			if (identifier.Kind() != SyntaxKind.IdentifierToken)
			{
				throw new ArgumentException("identifier");
			}
			syntaxKind = openBraceToken.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind != SyntaxKind.OpenBraceToken)
			{
				throw new ArgumentException("openBraceToken");
			}
			syntaxKind = closeBraceToken.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind != SyntaxKind.CloseBraceToken)
			{
				throw new ArgumentException("closeBraceToken");
			}
			syntaxKind = semicolonToken.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind != SyntaxKind.SemicolonToken)
			{
				throw new ArgumentException("semicolonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.RecordDeclarationSyntax)SyntaxFactory.RecordDeclaration(kind, attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), modifiers.Node.ToGreenList<SyntaxToken>(), (SyntaxToken)keyword.Node, (SyntaxToken)classOrStructKeyword.Node, (SyntaxToken)identifier.Node, (typeParameterList == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeParameterListSyntax)typeParameterList.Green), (parameterList == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ParameterListSyntax)parameterList.Green), (baseList == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.BaseListSyntax)baseList.Green), constraintClauses.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeParameterConstraintClauseSyntax>(), (SyntaxToken)openBraceToken.Node, members.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.MemberDeclarationSyntax>(), (SyntaxToken)closeBraceToken.Node, (SyntaxToken)semicolonToken.Node).CreateRed();
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x000E5948 File Offset: 0x000E3B48
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public static Microsoft.CodeAnalysis.CSharp.Syntax.RecordDeclarationSyntax RecordDeclaration(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterListSyntax typeParameterList, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, Microsoft.CodeAnalysis.CSharp.Syntax.BaseListSyntax baseList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax> constraintClauses, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax> members)
		{
			return SyntaxFactory.RecordDeclaration(kind, attributeLists, modifiers, keyword, default(SyntaxToken), identifier, typeParameterList, parameterList, baseList, constraintClauses, default(SyntaxToken), members, default(SyntaxToken), default(SyntaxToken));
		}

		// Token: 0x06002AD1 RID: 10961 RVA: 0x000E5990 File Offset: 0x000E3B90
		public static Microsoft.CodeAnalysis.CSharp.Syntax.RecordDeclarationSyntax RecordDeclaration(SyntaxKind kind, SyntaxToken keyword, SyntaxToken identifier)
		{
			return SyntaxFactory.RecordDeclaration(kind, default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), keyword, default(SyntaxToken), identifier, null, null, null, default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax>), default(SyntaxToken), default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax>), default(SyntaxToken), default(SyntaxToken));
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x000E59F4 File Offset: 0x000E3BF4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.RecordDeclarationSyntax RecordDeclaration(SyntaxKind kind, SyntaxToken keyword, string identifier)
		{
			return SyntaxFactory.RecordDeclaration(kind, default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), keyword, default(SyntaxToken), SyntaxFactory.Identifier(identifier), null, null, null, default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax>), default(SyntaxToken), default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax>), default(SyntaxToken), default(SyntaxToken));
		}

		// Token: 0x06002AD3 RID: 10963 RVA: 0x000E5A5C File Offset: 0x000E3C5C
		private static SyntaxKind GetRecordDeclarationClassOrStructKeywordKind(SyntaxKind kind)
		{
			SyntaxKind syntaxKind;
			if (kind != SyntaxKind.RecordDeclaration)
			{
				if (kind != SyntaxKind.RecordStructDeclaration)
				{
					throw new ArgumentOutOfRangeException();
				}
				syntaxKind = SyntaxKind.StructKeyword;
			}
			else
			{
				syntaxKind = SyntaxKind.ClassKeyword;
			}
			return syntaxKind;
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x000E5A94 File Offset: 0x000E3C94
		public static Microsoft.CodeAnalysis.CSharp.Syntax.EnumDeclarationSyntax EnumDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken enumKeyword, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.BaseListSyntax baseList, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.EnumMemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
		{
			if (enumKeyword.Kind() != SyntaxKind.EnumKeyword)
			{
				throw new ArgumentException("enumKeyword");
			}
			if (identifier.Kind() != SyntaxKind.IdentifierToken)
			{
				throw new ArgumentException("identifier");
			}
			if (openBraceToken.Kind() != SyntaxKind.OpenBraceToken)
			{
				throw new ArgumentException("openBraceToken");
			}
			if (closeBraceToken.Kind() != SyntaxKind.CloseBraceToken)
			{
				throw new ArgumentException("closeBraceToken");
			}
			SyntaxKind syntaxKind = semicolonToken.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind != SyntaxKind.SemicolonToken)
			{
				throw new ArgumentException("semicolonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.EnumDeclarationSyntax)SyntaxFactory.EnumDeclaration(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), modifiers.Node.ToGreenList<SyntaxToken>(), (SyntaxToken)enumKeyword.Node, (SyntaxToken)identifier.Node, (baseList == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.BaseListSyntax)baseList.Green), (SyntaxToken)openBraceToken.Node, members.Node.ToGreenSeparatedList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.EnumMemberDeclarationSyntax>(), (SyntaxToken)closeBraceToken.Node, (SyntaxToken)semicolonToken.Node).CreateRed();
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x000E5BA4 File Offset: 0x000E3DA4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.EnumDeclarationSyntax EnumDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.BaseListSyntax baseList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.EnumMemberDeclarationSyntax> members)
		{
			return SyntaxFactory.EnumDeclaration(attributeLists, modifiers, SyntaxFactory.Token(SyntaxKind.EnumKeyword), identifier, baseList, SyntaxFactory.Token(SyntaxKind.OpenBraceToken), members, SyntaxFactory.Token(SyntaxKind.CloseBraceToken), default(SyntaxToken));
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x000E5BE4 File Offset: 0x000E3DE4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.EnumDeclarationSyntax EnumDeclaration(SyntaxToken identifier)
		{
			return SyntaxFactory.EnumDeclaration(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), SyntaxFactory.Token(SyntaxKind.EnumKeyword), identifier, null, SyntaxFactory.Token(SyntaxKind.OpenBraceToken), default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.EnumMemberDeclarationSyntax>), SyntaxFactory.Token(SyntaxKind.CloseBraceToken), default(SyntaxToken));
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x000E5C3C File Offset: 0x000E3E3C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.EnumDeclarationSyntax EnumDeclaration(string identifier)
		{
			return SyntaxFactory.EnumDeclaration(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), SyntaxFactory.Token(SyntaxKind.EnumKeyword), SyntaxFactory.Identifier(identifier), null, SyntaxFactory.Token(SyntaxKind.OpenBraceToken), default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.EnumMemberDeclarationSyntax>), SyntaxFactory.Token(SyntaxKind.CloseBraceToken), default(SyntaxToken));
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x000E5C98 File Offset: 0x000E3E98
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DelegateDeclarationSyntax DelegateDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken delegateKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax returnType, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterListSyntax typeParameterList, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken semicolonToken)
		{
			if (delegateKeyword.Kind() != SyntaxKind.DelegateKeyword)
			{
				throw new ArgumentException("delegateKeyword");
			}
			if (returnType == null)
			{
				throw new ArgumentNullException("returnType");
			}
			if (identifier.Kind() != SyntaxKind.IdentifierToken)
			{
				throw new ArgumentException("identifier");
			}
			if (parameterList == null)
			{
				throw new ArgumentNullException("parameterList");
			}
			if (semicolonToken.Kind() != SyntaxKind.SemicolonToken)
			{
				throw new ArgumentException("semicolonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.DelegateDeclarationSyntax)SyntaxFactory.DelegateDeclaration(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), modifiers.Node.ToGreenList<SyntaxToken>(), (SyntaxToken)delegateKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)returnType.Green, (SyntaxToken)identifier.Node, (typeParameterList == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeParameterListSyntax)typeParameterList.Green), (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ParameterListSyntax)parameterList.Green, constraintClauses.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeParameterConstraintClauseSyntax>(), (SyntaxToken)semicolonToken.Node).CreateRed();
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x000E5D90 File Offset: 0x000E3F90
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DelegateDeclarationSyntax DelegateDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax returnType, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterListSyntax typeParameterList, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax> constraintClauses)
		{
			return SyntaxFactory.DelegateDeclaration(attributeLists, modifiers, SyntaxFactory.Token(SyntaxKind.DelegateKeyword), returnType, identifier, typeParameterList, parameterList, constraintClauses, SyntaxFactory.Token(SyntaxKind.SemicolonToken));
		}

		// Token: 0x06002ADA RID: 10970 RVA: 0x000E5DC0 File Offset: 0x000E3FC0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DelegateDeclarationSyntax DelegateDeclaration(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax returnType, SyntaxToken identifier)
		{
			return SyntaxFactory.DelegateDeclaration(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), SyntaxFactory.Token(SyntaxKind.DelegateKeyword), returnType, identifier, null, SyntaxFactory.ParameterList(default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax>)), default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax>), SyntaxFactory.Token(SyntaxKind.SemicolonToken));
		}

		// Token: 0x06002ADB RID: 10971 RVA: 0x000E5E14 File Offset: 0x000E4014
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DelegateDeclarationSyntax DelegateDeclaration(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax returnType, string identifier)
		{
			return SyntaxFactory.DelegateDeclaration(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), SyntaxFactory.Token(SyntaxKind.DelegateKeyword), returnType, SyntaxFactory.Identifier(identifier), null, SyntaxFactory.ParameterList(default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax>)), default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax>), SyntaxFactory.Token(SyntaxKind.SemicolonToken));
		}

		// Token: 0x06002ADC RID: 10972 RVA: 0x000E5E6C File Offset: 0x000E406C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.EnumMemberDeclarationSyntax EnumMemberDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.EqualsValueClauseSyntax equalsValue)
		{
			if (identifier.Kind() != SyntaxKind.IdentifierToken)
			{
				throw new ArgumentException("identifier");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.EnumMemberDeclarationSyntax)SyntaxFactory.EnumMemberDeclaration(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), modifiers.Node.ToGreenList<SyntaxToken>(), (SyntaxToken)identifier.Node, (equalsValue == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.EqualsValueClauseSyntax)equalsValue.Green)).CreateRed();
		}

		// Token: 0x06002ADD RID: 10973 RVA: 0x000E5ED8 File Offset: 0x000E40D8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.EnumMemberDeclarationSyntax EnumMemberDeclaration(SyntaxToken identifier)
		{
			return SyntaxFactory.EnumMemberDeclaration(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), identifier, null);
		}

		// Token: 0x06002ADE RID: 10974 RVA: 0x000E5F00 File Offset: 0x000E4100
		public static Microsoft.CodeAnalysis.CSharp.Syntax.EnumMemberDeclarationSyntax EnumMemberDeclaration(string identifier)
		{
			return SyntaxFactory.EnumMemberDeclaration(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), SyntaxFactory.Identifier(identifier), null);
		}

		// Token: 0x06002ADF RID: 10975 RVA: 0x000E5F2C File Offset: 0x000E412C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.BaseListSyntax BaseList(SyntaxToken colonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.BaseTypeSyntax> types)
		{
			if (colonToken.Kind() != SyntaxKind.ColonToken)
			{
				throw new ArgumentException("colonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.BaseListSyntax)SyntaxFactory.BaseList((SyntaxToken)colonToken.Node, types.Node.ToGreenSeparatedList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.BaseTypeSyntax>()).CreateRed();
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x000E5F78 File Offset: 0x000E4178
		public static Microsoft.CodeAnalysis.CSharp.Syntax.BaseListSyntax BaseList([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.BaseTypeSyntax> types = default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.BaseTypeSyntax>))
		{
			return SyntaxFactory.BaseList(SyntaxFactory.Token(SyntaxKind.ColonToken), types);
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x000E5F8A File Offset: 0x000E418A
		public static Microsoft.CodeAnalysis.CSharp.Syntax.SimpleBaseTypeSyntax SimpleBaseType(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.SimpleBaseTypeSyntax)SyntaxFactory.SimpleBaseType((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)type.Green).CreateRed();
		}

		// Token: 0x06002AE2 RID: 10978 RVA: 0x000E5FB4 File Offset: 0x000E41B4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.PrimaryConstructorBaseTypeSyntax PrimaryConstructorBaseType(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, Microsoft.CodeAnalysis.CSharp.Syntax.ArgumentListSyntax argumentList)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (argumentList == null)
			{
				throw new ArgumentNullException("argumentList");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.PrimaryConstructorBaseTypeSyntax)SyntaxFactory.PrimaryConstructorBaseType((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)type.Green, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ArgumentListSyntax)argumentList.Green).CreateRed();
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x000E6004 File Offset: 0x000E4204
		public static Microsoft.CodeAnalysis.CSharp.Syntax.PrimaryConstructorBaseTypeSyntax PrimaryConstructorBaseType(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type)
		{
			return SyntaxFactory.PrimaryConstructorBaseType(type, SyntaxFactory.ArgumentList(default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ArgumentSyntax>)));
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x000E6028 File Offset: 0x000E4228
		public static Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax TypeParameterConstraintClause(SyntaxToken whereKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.IdentifierNameSyntax name, SyntaxToken colonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintSyntax> constraints)
		{
			if (whereKeyword.Kind() != SyntaxKind.WhereKeyword)
			{
				throw new ArgumentException("whereKeyword");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (colonToken.Kind() != SyntaxKind.ColonToken)
			{
				throw new ArgumentException("colonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax)SyntaxFactory.TypeParameterConstraintClause((SyntaxToken)whereKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.IdentifierNameSyntax)name.Green, (SyntaxToken)colonToken.Node, constraints.Node.ToGreenSeparatedList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeParameterConstraintSyntax>()).CreateRed();
		}

		// Token: 0x06002AE5 RID: 10981 RVA: 0x000E60B1 File Offset: 0x000E42B1
		public static Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax TypeParameterConstraintClause(Microsoft.CodeAnalysis.CSharp.Syntax.IdentifierNameSyntax name, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintSyntax> constraints)
		{
			return SyntaxFactory.TypeParameterConstraintClause(SyntaxFactory.Token(SyntaxKind.WhereKeyword), name, SyntaxFactory.Token(SyntaxKind.ColonToken), constraints);
		}

		// Token: 0x06002AE6 RID: 10982 RVA: 0x000E60D0 File Offset: 0x000E42D0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax TypeParameterConstraintClause(Microsoft.CodeAnalysis.CSharp.Syntax.IdentifierNameSyntax name)
		{
			return SyntaxFactory.TypeParameterConstraintClause(SyntaxFactory.Token(SyntaxKind.WhereKeyword), name, SyntaxFactory.Token(SyntaxKind.ColonToken), default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintSyntax>));
		}

		// Token: 0x06002AE7 RID: 10983 RVA: 0x000E6100 File Offset: 0x000E4300
		public static Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax TypeParameterConstraintClause(string name)
		{
			return SyntaxFactory.TypeParameterConstraintClause(SyntaxFactory.Token(SyntaxKind.WhereKeyword), SyntaxFactory.IdentifierName(name), SyntaxFactory.Token(SyntaxKind.ColonToken), default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintSyntax>));
		}

		// Token: 0x06002AE8 RID: 10984 RVA: 0x000E6138 File Offset: 0x000E4338
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ConstructorConstraintSyntax ConstructorConstraint(SyntaxToken newKeyword, SyntaxToken openParenToken, SyntaxToken closeParenToken)
		{
			if (newKeyword.Kind() != SyntaxKind.NewKeyword)
			{
				throw new ArgumentException("newKeyword");
			}
			if (openParenToken.Kind() != SyntaxKind.OpenParenToken)
			{
				throw new ArgumentException("openParenToken");
			}
			if (closeParenToken.Kind() != SyntaxKind.CloseParenToken)
			{
				throw new ArgumentException("closeParenToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ConstructorConstraintSyntax)SyntaxFactory.ConstructorConstraint((SyntaxToken)newKeyword.Node, (SyntaxToken)openParenToken.Node, (SyntaxToken)closeParenToken.Node).CreateRed();
		}

		// Token: 0x06002AE9 RID: 10985 RVA: 0x000E61C0 File Offset: 0x000E43C0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ConstructorConstraintSyntax ConstructorConstraint()
		{
			return SyntaxFactory.ConstructorConstraint(SyntaxFactory.Token(SyntaxKind.NewKeyword), SyntaxFactory.Token(SyntaxKind.OpenParenToken), SyntaxFactory.Token(SyntaxKind.CloseParenToken));
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x000E61E8 File Offset: 0x000E43E8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ClassOrStructConstraintSyntax ClassOrStructConstraint(SyntaxKind kind, SyntaxToken classOrStructKeyword, SyntaxToken questionToken)
		{
			if (kind - SyntaxKind.ClassConstraint > 1)
			{
				throw new ArgumentException("kind");
			}
			SyntaxKind syntaxKind = classOrStructKeyword.Kind();
			if (syntaxKind - SyntaxKind.ClassKeyword > 1)
			{
				throw new ArgumentException("classOrStructKeyword");
			}
			syntaxKind = questionToken.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind != SyntaxKind.QuestionToken)
			{
				throw new ArgumentException("questionToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ClassOrStructConstraintSyntax)SyntaxFactory.ClassOrStructConstraint(kind, (SyntaxToken)classOrStructKeyword.Node, (SyntaxToken)questionToken.Node).CreateRed();
		}

		// Token: 0x06002AEB RID: 10987 RVA: 0x000E626C File Offset: 0x000E446C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ClassOrStructConstraintSyntax ClassOrStructConstraint(SyntaxKind kind)
		{
			return SyntaxFactory.ClassOrStructConstraint(kind, SyntaxFactory.Token(SyntaxFactory.GetClassOrStructConstraintClassOrStructKeywordKind(kind)), default(SyntaxToken));
		}

		// Token: 0x06002AEC RID: 10988 RVA: 0x000E6294 File Offset: 0x000E4494
		private static SyntaxKind GetClassOrStructConstraintClassOrStructKeywordKind(SyntaxKind kind)
		{
			SyntaxKind syntaxKind;
			if (kind != SyntaxKind.ClassConstraint)
			{
				if (kind != SyntaxKind.StructConstraint)
				{
					throw new ArgumentOutOfRangeException();
				}
				syntaxKind = SyntaxKind.StructKeyword;
			}
			else
			{
				syntaxKind = SyntaxKind.ClassKeyword;
			}
			return syntaxKind;
		}

		// Token: 0x06002AED RID: 10989 RVA: 0x000E62CA File Offset: 0x000E44CA
		public static Microsoft.CodeAnalysis.CSharp.Syntax.TypeConstraintSyntax TypeConstraint(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.TypeConstraintSyntax)SyntaxFactory.TypeConstraint((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)type.Green).CreateRed();
		}

		// Token: 0x06002AEE RID: 10990 RVA: 0x000E62F4 File Offset: 0x000E44F4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DefaultConstraintSyntax DefaultConstraint(SyntaxToken defaultKeyword)
		{
			if (defaultKeyword.Kind() != SyntaxKind.DefaultKeyword)
			{
				throw new ArgumentException("defaultKeyword");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.DefaultConstraintSyntax)SyntaxFactory.DefaultConstraint((SyntaxToken)defaultKeyword.Node).CreateRed();
		}

		// Token: 0x06002AEF RID: 10991 RVA: 0x000E6329 File Offset: 0x000E4529
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DefaultConstraintSyntax DefaultConstraint()
		{
			return SyntaxFactory.DefaultConstraint(SyntaxFactory.Token(SyntaxKind.DefaultKeyword));
		}

		// Token: 0x06002AF0 RID: 10992 RVA: 0x000E633C File Offset: 0x000E453C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.FieldDeclarationSyntax FieldDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclarationSyntax declaration, SyntaxToken semicolonToken)
		{
			if (declaration == null)
			{
				throw new ArgumentNullException("declaration");
			}
			if (semicolonToken.Kind() != SyntaxKind.SemicolonToken)
			{
				throw new ArgumentException("semicolonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.FieldDeclarationSyntax)SyntaxFactory.FieldDeclaration(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), modifiers.Node.ToGreenList<SyntaxToken>(), (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.VariableDeclarationSyntax)declaration.Green, (SyntaxToken)semicolonToken.Node).CreateRed();
		}

		// Token: 0x06002AF1 RID: 10993 RVA: 0x000E63AD File Offset: 0x000E45AD
		public static Microsoft.CodeAnalysis.CSharp.Syntax.FieldDeclarationSyntax FieldDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclarationSyntax declaration)
		{
			return SyntaxFactory.FieldDeclaration(attributeLists, modifiers, declaration, SyntaxFactory.Token(SyntaxKind.SemicolonToken));
		}

		// Token: 0x06002AF2 RID: 10994 RVA: 0x000E63C4 File Offset: 0x000E45C4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.FieldDeclarationSyntax FieldDeclaration(Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclarationSyntax declaration)
		{
			return SyntaxFactory.FieldDeclaration(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), declaration, SyntaxFactory.Token(SyntaxKind.SemicolonToken));
		}

		// Token: 0x06002AF3 RID: 10995 RVA: 0x000E63F4 File Offset: 0x000E45F4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.EventFieldDeclarationSyntax EventFieldDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken eventKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclarationSyntax declaration, SyntaxToken semicolonToken)
		{
			if (eventKeyword.Kind() != SyntaxKind.EventKeyword)
			{
				throw new ArgumentException("eventKeyword");
			}
			if (declaration == null)
			{
				throw new ArgumentNullException("declaration");
			}
			if (semicolonToken.Kind() != SyntaxKind.SemicolonToken)
			{
				throw new ArgumentException("semicolonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.EventFieldDeclarationSyntax)SyntaxFactory.EventFieldDeclaration(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), modifiers.Node.ToGreenList<SyntaxToken>(), (SyntaxToken)eventKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.VariableDeclarationSyntax)declaration.Green, (SyntaxToken)semicolonToken.Node).CreateRed();
		}

		// Token: 0x06002AF4 RID: 10996 RVA: 0x000E648A File Offset: 0x000E468A
		public static Microsoft.CodeAnalysis.CSharp.Syntax.EventFieldDeclarationSyntax EventFieldDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclarationSyntax declaration)
		{
			return SyntaxFactory.EventFieldDeclaration(attributeLists, modifiers, SyntaxFactory.Token(SyntaxKind.EventKeyword), declaration, SyntaxFactory.Token(SyntaxKind.SemicolonToken));
		}

		// Token: 0x06002AF5 RID: 10997 RVA: 0x000E64A8 File Offset: 0x000E46A8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.EventFieldDeclarationSyntax EventFieldDeclaration(Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclarationSyntax declaration)
		{
			return SyntaxFactory.EventFieldDeclaration(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), SyntaxFactory.Token(SyntaxKind.EventKeyword), declaration, SyntaxFactory.Token(SyntaxKind.SemicolonToken));
		}

		// Token: 0x06002AF6 RID: 10998 RVA: 0x000E64E4 File Offset: 0x000E46E4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ExplicitInterfaceSpecifierSyntax ExplicitInterfaceSpecifier(Microsoft.CodeAnalysis.CSharp.Syntax.NameSyntax name, SyntaxToken dotToken)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (dotToken.Kind() != SyntaxKind.DotToken)
			{
				throw new ArgumentException("dotToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ExplicitInterfaceSpecifierSyntax)SyntaxFactory.ExplicitInterfaceSpecifier((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.NameSyntax)name.Green, (SyntaxToken)dotToken.Node).CreateRed();
		}

		// Token: 0x06002AF7 RID: 10999 RVA: 0x000E653D File Offset: 0x000E473D
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ExplicitInterfaceSpecifierSyntax ExplicitInterfaceSpecifier(Microsoft.CodeAnalysis.CSharp.Syntax.NameSyntax name)
		{
			return SyntaxFactory.ExplicitInterfaceSpecifier(name, SyntaxFactory.Token(SyntaxKind.DotToken));
		}

		// Token: 0x06002AF8 RID: 11000 RVA: 0x000E6550 File Offset: 0x000E4750
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public static Microsoft.CodeAnalysis.CSharp.Syntax.MethodDeclarationSyntax MethodDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax returnType, Microsoft.CodeAnalysis.CSharp.Syntax.ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterListSyntax typeParameterList, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax> constraintClauses, Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax body, Microsoft.CodeAnalysis.CSharp.Syntax.ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
		{
			if (returnType == null)
			{
				throw new ArgumentNullException("returnType");
			}
			if (identifier.Kind() != SyntaxKind.IdentifierToken)
			{
				throw new ArgumentException("identifier");
			}
			if (parameterList == null)
			{
				throw new ArgumentNullException("parameterList");
			}
			SyntaxKind syntaxKind = semicolonToken.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind != SyntaxKind.SemicolonToken)
			{
				throw new ArgumentException("semicolonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.MethodDeclarationSyntax)SyntaxFactory.MethodDeclaration(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), modifiers.Node.ToGreenList<SyntaxToken>(), (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)returnType.Green, (explicitInterfaceSpecifier == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExplicitInterfaceSpecifierSyntax)explicitInterfaceSpecifier.Green), (SyntaxToken)identifier.Node, (typeParameterList == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeParameterListSyntax)typeParameterList.Green), (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ParameterListSyntax)parameterList.Green, constraintClauses.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeParameterConstraintClauseSyntax>(), (body == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.BlockSyntax)body.Green), (expressionBody == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ArrowExpressionClauseSyntax)expressionBody.Green), (SyntaxToken)semicolonToken.Node).CreateRed();
		}

		// Token: 0x06002AF9 RID: 11001 RVA: 0x000E6660 File Offset: 0x000E4860
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public static Microsoft.CodeAnalysis.CSharp.Syntax.MethodDeclarationSyntax MethodDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax returnType, Microsoft.CodeAnalysis.CSharp.Syntax.ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterListSyntax typeParameterList, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax> constraintClauses, Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax body, Microsoft.CodeAnalysis.CSharp.Syntax.ArrowExpressionClauseSyntax expressionBody)
		{
			return SyntaxFactory.MethodDeclaration(attributeLists, modifiers, returnType, explicitInterfaceSpecifier, identifier, typeParameterList, parameterList, constraintClauses, body, expressionBody, default(SyntaxToken));
		}

		// Token: 0x06002AFA RID: 11002 RVA: 0x000E668C File Offset: 0x000E488C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.MethodDeclarationSyntax MethodDeclaration(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax returnType, SyntaxToken identifier)
		{
			return SyntaxFactory.MethodDeclaration(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), returnType, null, identifier, null, SyntaxFactory.ParameterList(default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax>)), default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax>), null, null, default(SyntaxToken));
		}

		// Token: 0x06002AFB RID: 11003 RVA: 0x000E66D8 File Offset: 0x000E48D8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.MethodDeclarationSyntax MethodDeclaration(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax returnType, string identifier)
		{
			return SyntaxFactory.MethodDeclaration(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), returnType, null, SyntaxFactory.Identifier(identifier), null, SyntaxFactory.ParameterList(default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax>)), default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintClauseSyntax>), null, null, default(SyntaxToken));
		}

		// Token: 0x06002AFC RID: 11004 RVA: 0x000E6728 File Offset: 0x000E4928
		public static Microsoft.CodeAnalysis.CSharp.Syntax.OperatorDeclarationSyntax OperatorDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax returnType, SyntaxToken operatorKeyword, SyntaxToken operatorToken, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax body, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
		{
			if (returnType == null)
			{
				throw new ArgumentNullException("returnType");
			}
			if (operatorKeyword.Kind() != SyntaxKind.OperatorKeyword)
			{
				throw new ArgumentException("operatorKeyword");
			}
			SyntaxKind syntaxKind = operatorToken.Kind();
			if (syntaxKind <= SyntaxKind.GreaterThanToken)
			{
				switch (syntaxKind)
				{
				case SyntaxKind.TildeToken:
				case SyntaxKind.ExclamationToken:
				case SyntaxKind.PercentToken:
				case SyntaxKind.CaretToken:
				case SyntaxKind.AmpersandToken:
				case SyntaxKind.AsteriskToken:
				case SyntaxKind.MinusToken:
				case SyntaxKind.PlusToken:
				case SyntaxKind.BarToken:
					goto IL_0107;
				case SyntaxKind.DollarToken:
				case SyntaxKind.OpenParenToken:
				case SyntaxKind.CloseParenToken:
				case SyntaxKind.EqualsToken:
				case SyntaxKind.OpenBraceToken:
				case SyntaxKind.CloseBraceToken:
				case SyntaxKind.OpenBracketToken:
				case SyntaxKind.CloseBracketToken:
					break;
				default:
					if (syntaxKind == SyntaxKind.LessThanToken || syntaxKind == SyntaxKind.GreaterThanToken)
					{
						goto IL_0107;
					}
					break;
				}
			}
			else if (syntaxKind <= SyntaxKind.GreaterThanGreaterThanToken)
			{
				if (syntaxKind == SyntaxKind.SlashToken)
				{
					goto IL_0107;
				}
				switch (syntaxKind)
				{
				case SyntaxKind.MinusMinusToken:
				case SyntaxKind.PlusPlusToken:
				case SyntaxKind.ExclamationEqualsToken:
				case SyntaxKind.EqualsEqualsToken:
				case SyntaxKind.LessThanEqualsToken:
				case SyntaxKind.LessThanLessThanToken:
				case SyntaxKind.GreaterThanEqualsToken:
				case SyntaxKind.GreaterThanGreaterThanToken:
					goto IL_0107;
				}
			}
			else if (syntaxKind - SyntaxKind.TrueKeyword <= 1 || syntaxKind == SyntaxKind.IsKeyword)
			{
				goto IL_0107;
			}
			throw new ArgumentException("operatorToken");
			IL_0107:
			if (parameterList == null)
			{
				throw new ArgumentNullException("parameterList");
			}
			syntaxKind = semicolonToken.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind != SyntaxKind.SemicolonToken)
			{
				throw new ArgumentException("semicolonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.OperatorDeclarationSyntax)SyntaxFactory.OperatorDeclaration(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), modifiers.Node.ToGreenList<SyntaxToken>(), (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)returnType.Green, (SyntaxToken)operatorKeyword.Node, (SyntaxToken)operatorToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ParameterListSyntax)parameterList.Green, (body == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.BlockSyntax)body.Green), (expressionBody == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ArrowExpressionClauseSyntax)expressionBody.Green), (SyntaxToken)semicolonToken.Node).CreateRed();
		}

		// Token: 0x06002AFD RID: 11005 RVA: 0x000E68F4 File Offset: 0x000E4AF4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.OperatorDeclarationSyntax OperatorDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax returnType, SyntaxToken operatorToken, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax body, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ArrowExpressionClauseSyntax expressionBody)
		{
			return SyntaxFactory.OperatorDeclaration(attributeLists, modifiers, returnType, SyntaxFactory.Token(SyntaxKind.OperatorKeyword), operatorToken, parameterList, body, expressionBody, default(SyntaxToken));
		}

		// Token: 0x06002AFE RID: 11006 RVA: 0x000E6924 File Offset: 0x000E4B24
		public static Microsoft.CodeAnalysis.CSharp.Syntax.OperatorDeclarationSyntax OperatorDeclaration(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax returnType, SyntaxToken operatorToken)
		{
			return SyntaxFactory.OperatorDeclaration(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), returnType, SyntaxFactory.Token(SyntaxKind.OperatorKeyword), operatorToken, SyntaxFactory.ParameterList(default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax>)), null, null, default(SyntaxToken));
		}

		// Token: 0x06002AFF RID: 11007 RVA: 0x000E6970 File Offset: 0x000E4B70
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ConversionOperatorDeclarationSyntax ConversionOperatorDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken implicitOrExplicitKeyword, SyntaxToken operatorKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax body, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
		{
			SyntaxKind syntaxKind = implicitOrExplicitKeyword.Kind();
			if (syntaxKind - SyntaxKind.ExplicitKeyword > 1)
			{
				throw new ArgumentException("implicitOrExplicitKeyword");
			}
			if (operatorKeyword.Kind() != SyntaxKind.OperatorKeyword)
			{
				throw new ArgumentException("operatorKeyword");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (parameterList == null)
			{
				throw new ArgumentNullException("parameterList");
			}
			syntaxKind = semicolonToken.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind != SyntaxKind.SemicolonToken)
			{
				throw new ArgumentException("semicolonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ConversionOperatorDeclarationSyntax)SyntaxFactory.ConversionOperatorDeclaration(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), modifiers.Node.ToGreenList<SyntaxToken>(), (SyntaxToken)implicitOrExplicitKeyword.Node, (SyntaxToken)operatorKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)type.Green, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ParameterListSyntax)parameterList.Green, (body == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.BlockSyntax)body.Green), (expressionBody == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ArrowExpressionClauseSyntax)expressionBody.Green), (SyntaxToken)semicolonToken.Node).CreateRed();
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x000E6A78 File Offset: 0x000E4C78
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ConversionOperatorDeclarationSyntax ConversionOperatorDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken implicitOrExplicitKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax body, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ArrowExpressionClauseSyntax expressionBody)
		{
			return SyntaxFactory.ConversionOperatorDeclaration(attributeLists, modifiers, implicitOrExplicitKeyword, SyntaxFactory.Token(SyntaxKind.OperatorKeyword), type, parameterList, body, expressionBody, default(SyntaxToken));
		}

		// Token: 0x06002B01 RID: 11009 RVA: 0x000E6AA8 File Offset: 0x000E4CA8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ConversionOperatorDeclarationSyntax ConversionOperatorDeclaration(SyntaxToken implicitOrExplicitKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type)
		{
			return SyntaxFactory.ConversionOperatorDeclaration(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), implicitOrExplicitKeyword, SyntaxFactory.Token(SyntaxKind.OperatorKeyword), type, SyntaxFactory.ParameterList(default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax>)), null, null, default(SyntaxToken));
		}

		// Token: 0x06002B02 RID: 11010 RVA: 0x000E6AF4 File Offset: 0x000E4CF4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ConstructorDeclarationSyntax ConstructorDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, Microsoft.CodeAnalysis.CSharp.Syntax.ConstructorInitializerSyntax initializer, Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax body, Microsoft.CodeAnalysis.CSharp.Syntax.ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
		{
			if (identifier.Kind() != SyntaxKind.IdentifierToken)
			{
				throw new ArgumentException("identifier");
			}
			if (parameterList == null)
			{
				throw new ArgumentNullException("parameterList");
			}
			SyntaxKind syntaxKind = semicolonToken.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind != SyntaxKind.SemicolonToken)
			{
				throw new ArgumentException("semicolonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ConstructorDeclarationSyntax)SyntaxFactory.ConstructorDeclaration(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), modifiers.Node.ToGreenList<SyntaxToken>(), (SyntaxToken)identifier.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ParameterListSyntax)parameterList.Green, (initializer == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ConstructorInitializerSyntax)initializer.Green), (body == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.BlockSyntax)body.Green), (expressionBody == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ArrowExpressionClauseSyntax)expressionBody.Green), (SyntaxToken)semicolonToken.Node).CreateRed();
		}

		// Token: 0x06002B03 RID: 11011 RVA: 0x000E6BC8 File Offset: 0x000E4DC8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ConstructorDeclarationSyntax ConstructorDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, Microsoft.CodeAnalysis.CSharp.Syntax.ConstructorInitializerSyntax initializer, Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax body, Microsoft.CodeAnalysis.CSharp.Syntax.ArrowExpressionClauseSyntax expressionBody)
		{
			return SyntaxFactory.ConstructorDeclaration(attributeLists, modifiers, identifier, parameterList, initializer, body, expressionBody, default(SyntaxToken));
		}

		// Token: 0x06002B04 RID: 11012 RVA: 0x000E6BF0 File Offset: 0x000E4DF0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ConstructorDeclarationSyntax ConstructorDeclaration(SyntaxToken identifier)
		{
			return SyntaxFactory.ConstructorDeclaration(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), identifier, SyntaxFactory.ParameterList(default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax>)), null, null, null, default(SyntaxToken));
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x000E6C30 File Offset: 0x000E4E30
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ConstructorDeclarationSyntax ConstructorDeclaration(string identifier)
		{
			return SyntaxFactory.ConstructorDeclaration(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), SyntaxFactory.Identifier(identifier), SyntaxFactory.ParameterList(default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax>)), null, null, null, default(SyntaxToken));
		}

		// Token: 0x06002B06 RID: 11014 RVA: 0x000E6C74 File Offset: 0x000E4E74
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ConstructorInitializerSyntax ConstructorInitializer(SyntaxKind kind, SyntaxToken colonToken, SyntaxToken thisOrBaseKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.ArgumentListSyntax argumentList)
		{
			if (kind - SyntaxKind.BaseConstructorInitializer > 1)
			{
				throw new ArgumentException("kind");
			}
			if (colonToken.Kind() != SyntaxKind.ColonToken)
			{
				throw new ArgumentException("colonToken");
			}
			SyntaxKind syntaxKind = thisOrBaseKeyword.Kind();
			if (syntaxKind - SyntaxKind.ThisKeyword > 1)
			{
				throw new ArgumentException("thisOrBaseKeyword");
			}
			if (argumentList == null)
			{
				throw new ArgumentNullException("argumentList");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ConstructorInitializerSyntax)SyntaxFactory.ConstructorInitializer(kind, (SyntaxToken)colonToken.Node, (SyntaxToken)thisOrBaseKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ArgumentListSyntax)argumentList.Green).CreateRed();
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x000E6D0C File Offset: 0x000E4F0C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ConstructorInitializerSyntax ConstructorInitializer(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ArgumentListSyntax argumentList = null)
		{
			return SyntaxFactory.ConstructorInitializer(kind, SyntaxFactory.Token(SyntaxKind.ColonToken), SyntaxFactory.Token(SyntaxFactory.GetConstructorInitializerThisOrBaseKeywordKind(kind)), argumentList ?? SyntaxFactory.ArgumentList(default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ArgumentSyntax>)));
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x000E6D48 File Offset: 0x000E4F48
		private static SyntaxKind GetConstructorInitializerThisOrBaseKeywordKind(SyntaxKind kind)
		{
			SyntaxKind syntaxKind;
			if (kind != SyntaxKind.BaseConstructorInitializer)
			{
				if (kind != SyntaxKind.ThisConstructorInitializer)
				{
					throw new ArgumentOutOfRangeException();
				}
				syntaxKind = SyntaxKind.ThisKeyword;
			}
			else
			{
				syntaxKind = SyntaxKind.BaseKeyword;
			}
			return syntaxKind;
		}

		// Token: 0x06002B09 RID: 11017 RVA: 0x000E6D80 File Offset: 0x000E4F80
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DestructorDeclarationSyntax DestructorDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken tildeToken, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax body, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
		{
			if (tildeToken.Kind() != SyntaxKind.TildeToken)
			{
				throw new ArgumentException("tildeToken");
			}
			if (identifier.Kind() != SyntaxKind.IdentifierToken)
			{
				throw new ArgumentException("identifier");
			}
			if (parameterList == null)
			{
				throw new ArgumentNullException("parameterList");
			}
			SyntaxKind syntaxKind = semicolonToken.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind != SyntaxKind.SemicolonToken)
			{
				throw new ArgumentException("semicolonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.DestructorDeclarationSyntax)SyntaxFactory.DestructorDeclaration(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), modifiers.Node.ToGreenList<SyntaxToken>(), (SyntaxToken)tildeToken.Node, (SyntaxToken)identifier.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ParameterListSyntax)parameterList.Green, (body == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.BlockSyntax)body.Green), (expressionBody == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ArrowExpressionClauseSyntax)expressionBody.Green), (SyntaxToken)semicolonToken.Node).CreateRed();
		}

		// Token: 0x06002B0A RID: 11018 RVA: 0x000E6E68 File Offset: 0x000E5068
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DestructorDeclarationSyntax DestructorDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax body, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ArrowExpressionClauseSyntax expressionBody)
		{
			return SyntaxFactory.DestructorDeclaration(attributeLists, modifiers, SyntaxFactory.Token(SyntaxKind.TildeToken), identifier, parameterList, body, expressionBody, default(SyntaxToken));
		}

		// Token: 0x06002B0B RID: 11019 RVA: 0x000E6E98 File Offset: 0x000E5098
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DestructorDeclarationSyntax DestructorDeclaration(SyntaxToken identifier)
		{
			return SyntaxFactory.DestructorDeclaration(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), SyntaxFactory.Token(SyntaxKind.TildeToken), identifier, SyntaxFactory.ParameterList(default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax>)), null, null, default(SyntaxToken));
		}

		// Token: 0x06002B0C RID: 11020 RVA: 0x000E6EE0 File Offset: 0x000E50E0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DestructorDeclarationSyntax DestructorDeclaration(string identifier)
		{
			return SyntaxFactory.DestructorDeclaration(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), SyntaxFactory.Token(SyntaxKind.TildeToken), SyntaxFactory.Identifier(identifier), SyntaxFactory.ParameterList(default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax>)), null, null, default(SyntaxToken));
		}

		// Token: 0x06002B0D RID: 11021 RVA: 0x000E6F30 File Offset: 0x000E5130
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public static Microsoft.CodeAnalysis.CSharp.Syntax.PropertyDeclarationSyntax PropertyDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, Microsoft.CodeAnalysis.CSharp.Syntax.ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.AccessorListSyntax accessorList, Microsoft.CodeAnalysis.CSharp.Syntax.ArrowExpressionClauseSyntax expressionBody, Microsoft.CodeAnalysis.CSharp.Syntax.EqualsValueClauseSyntax initializer, SyntaxToken semicolonToken)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (identifier.Kind() != SyntaxKind.IdentifierToken)
			{
				throw new ArgumentException("identifier");
			}
			SyntaxKind syntaxKind = semicolonToken.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind != SyntaxKind.SemicolonToken)
			{
				throw new ArgumentException("semicolonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.PropertyDeclarationSyntax)SyntaxFactory.PropertyDeclaration(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), modifiers.Node.ToGreenList<SyntaxToken>(), (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)type.Green, (explicitInterfaceSpecifier == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExplicitInterfaceSpecifierSyntax)explicitInterfaceSpecifier.Green), (SyntaxToken)identifier.Node, (accessorList == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AccessorListSyntax)accessorList.Green), (expressionBody == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ArrowExpressionClauseSyntax)expressionBody.Green), (initializer == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.EqualsValueClauseSyntax)initializer.Green), (SyntaxToken)semicolonToken.Node).CreateRed();
		}

		// Token: 0x06002B0E RID: 11022 RVA: 0x000E7018 File Offset: 0x000E5218
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public static Microsoft.CodeAnalysis.CSharp.Syntax.PropertyDeclarationSyntax PropertyDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, Microsoft.CodeAnalysis.CSharp.Syntax.ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.AccessorListSyntax accessorList, Microsoft.CodeAnalysis.CSharp.Syntax.ArrowExpressionClauseSyntax expressionBody, Microsoft.CodeAnalysis.CSharp.Syntax.EqualsValueClauseSyntax initializer)
		{
			return SyntaxFactory.PropertyDeclaration(attributeLists, modifiers, type, explicitInterfaceSpecifier, identifier, accessorList, expressionBody, initializer, default(SyntaxToken));
		}

		// Token: 0x06002B0F RID: 11023 RVA: 0x000E7040 File Offset: 0x000E5240
		public static Microsoft.CodeAnalysis.CSharp.Syntax.PropertyDeclarationSyntax PropertyDeclaration(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, SyntaxToken identifier)
		{
			return SyntaxFactory.PropertyDeclaration(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), type, null, identifier, null, null, null, default(SyntaxToken));
		}

		// Token: 0x06002B10 RID: 11024 RVA: 0x000E7074 File Offset: 0x000E5274
		public static Microsoft.CodeAnalysis.CSharp.Syntax.PropertyDeclarationSyntax PropertyDeclaration(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, string identifier)
		{
			return SyntaxFactory.PropertyDeclaration(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), type, null, SyntaxFactory.Identifier(identifier), null, null, null, default(SyntaxToken));
		}

		// Token: 0x06002B11 RID: 11025 RVA: 0x000E70AC File Offset: 0x000E52AC
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ArrowExpressionClauseSyntax ArrowExpressionClause(SyntaxToken arrowToken, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			if (arrowToken.Kind() != SyntaxKind.EqualsGreaterThanToken)
			{
				throw new ArgumentException("arrowToken");
			}
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ArrowExpressionClauseSyntax)SyntaxFactory.ArrowExpressionClause((SyntaxToken)arrowToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)expression.Green).CreateRed();
		}

		// Token: 0x06002B12 RID: 11026 RVA: 0x000E7105 File Offset: 0x000E5305
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ArrowExpressionClauseSyntax ArrowExpressionClause(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			return SyntaxFactory.ArrowExpressionClause(SyntaxFactory.Token(SyntaxKind.EqualsGreaterThanToken), expression);
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x000E7118 File Offset: 0x000E5318
		public static Microsoft.CodeAnalysis.CSharp.Syntax.EventDeclarationSyntax EventDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken eventKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.AccessorListSyntax accessorList, SyntaxToken semicolonToken)
		{
			if (eventKeyword.Kind() != SyntaxKind.EventKeyword)
			{
				throw new ArgumentException("eventKeyword");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (identifier.Kind() != SyntaxKind.IdentifierToken)
			{
				throw new ArgumentException("identifier");
			}
			SyntaxKind syntaxKind = semicolonToken.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind != SyntaxKind.SemicolonToken)
			{
				throw new ArgumentException("semicolonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.EventDeclarationSyntax)SyntaxFactory.EventDeclaration(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), modifiers.Node.ToGreenList<SyntaxToken>(), (SyntaxToken)eventKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)type.Green, (explicitInterfaceSpecifier == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExplicitInterfaceSpecifierSyntax)explicitInterfaceSpecifier.Green), (SyntaxToken)identifier.Node, (accessorList == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AccessorListSyntax)accessorList.Green), (SyntaxToken)semicolonToken.Node).CreateRed();
		}

		// Token: 0x06002B14 RID: 11028 RVA: 0x000E7200 File Offset: 0x000E5400
		public static Microsoft.CodeAnalysis.CSharp.Syntax.EventDeclarationSyntax EventDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.AccessorListSyntax accessorList)
		{
			return SyntaxFactory.EventDeclaration(attributeLists, modifiers, SyntaxFactory.Token(SyntaxKind.EventKeyword), type, explicitInterfaceSpecifier, identifier, accessorList, default(SyntaxToken));
		}

		// Token: 0x06002B15 RID: 11029 RVA: 0x000E7230 File Offset: 0x000E5430
		public static Microsoft.CodeAnalysis.CSharp.Syntax.EventDeclarationSyntax EventDeclaration(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, SyntaxToken identifier)
		{
			return SyntaxFactory.EventDeclaration(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), SyntaxFactory.Token(SyntaxKind.EventKeyword), type, null, identifier, null, default(SyntaxToken));
		}

		// Token: 0x06002B16 RID: 11030 RVA: 0x000E726C File Offset: 0x000E546C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.EventDeclarationSyntax EventDeclaration(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, string identifier)
		{
			return SyntaxFactory.EventDeclaration(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), SyntaxFactory.Token(SyntaxKind.EventKeyword), type, null, SyntaxFactory.Identifier(identifier), null, default(SyntaxToken));
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x000E72AC File Offset: 0x000E54AC
		public static Microsoft.CodeAnalysis.CSharp.Syntax.IndexerDeclarationSyntax IndexerDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken thisKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.BracketedParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.AccessorListSyntax accessorList, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (thisKeyword.Kind() != SyntaxKind.ThisKeyword)
			{
				throw new ArgumentException("thisKeyword");
			}
			if (parameterList == null)
			{
				throw new ArgumentNullException("parameterList");
			}
			SyntaxKind syntaxKind = semicolonToken.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind != SyntaxKind.SemicolonToken)
			{
				throw new ArgumentException("semicolonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.IndexerDeclarationSyntax)SyntaxFactory.IndexerDeclaration(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), modifiers.Node.ToGreenList<SyntaxToken>(), (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)type.Green, (explicitInterfaceSpecifier == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExplicitInterfaceSpecifierSyntax)explicitInterfaceSpecifier.Green), (SyntaxToken)thisKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.BracketedParameterListSyntax)parameterList.Green, (accessorList == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AccessorListSyntax)accessorList.Green), (expressionBody == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ArrowExpressionClauseSyntax)expressionBody.Green), (SyntaxToken)semicolonToken.Node).CreateRed();
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x000E739C File Offset: 0x000E559C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.IndexerDeclarationSyntax IndexerDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, Microsoft.CodeAnalysis.CSharp.Syntax.BracketedParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.AccessorListSyntax accessorList, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.ArrowExpressionClauseSyntax expressionBody)
		{
			return SyntaxFactory.IndexerDeclaration(attributeLists, modifiers, type, explicitInterfaceSpecifier, SyntaxFactory.Token(SyntaxKind.ThisKeyword), parameterList, accessorList, expressionBody, default(SyntaxToken));
		}

		// Token: 0x06002B19 RID: 11033 RVA: 0x000E73CC File Offset: 0x000E55CC
		public static Microsoft.CodeAnalysis.CSharp.Syntax.IndexerDeclarationSyntax IndexerDeclaration(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type)
		{
			return SyntaxFactory.IndexerDeclaration(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), type, null, SyntaxFactory.Token(SyntaxKind.ThisKeyword), SyntaxFactory.BracketedParameterList(default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax>)), null, null, default(SyntaxToken));
		}

		// Token: 0x06002B1A RID: 11034 RVA: 0x000E7418 File Offset: 0x000E5618
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AccessorListSyntax AccessorList(SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AccessorDeclarationSyntax> accessors, SyntaxToken closeBraceToken)
		{
			if (openBraceToken.Kind() != SyntaxKind.OpenBraceToken)
			{
				throw new ArgumentException("openBraceToken");
			}
			if (closeBraceToken.Kind() != SyntaxKind.CloseBraceToken)
			{
				throw new ArgumentException("closeBraceToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.AccessorListSyntax)SyntaxFactory.AccessorList((SyntaxToken)openBraceToken.Node, accessors.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AccessorDeclarationSyntax>(), (SyntaxToken)closeBraceToken.Node).CreateRed();
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x000E7488 File Offset: 0x000E5688
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AccessorListSyntax AccessorList([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AccessorDeclarationSyntax> accessors = default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AccessorDeclarationSyntax>))
		{
			return SyntaxFactory.AccessorList(SyntaxFactory.Token(SyntaxKind.OpenBraceToken), accessors, SyntaxFactory.Token(SyntaxKind.CloseBraceToken));
		}

		// Token: 0x06002B1C RID: 11036 RVA: 0x000E74A4 File Offset: 0x000E56A4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AccessorDeclarationSyntax AccessorDeclaration(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, SyntaxToken keyword, Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax body, Microsoft.CodeAnalysis.CSharp.Syntax.ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
		{
			if (kind - SyntaxKind.GetAccessorDeclaration > 4 && kind != SyntaxKind.InitAccessorDeclaration)
			{
				throw new ArgumentException("kind");
			}
			SyntaxKind syntaxKind = keyword.Kind();
			if (syntaxKind - SyntaxKind.GetKeyword > 3 && syntaxKind != SyntaxKind.InitKeyword && syntaxKind != SyntaxKind.IdentifierToken)
			{
				throw new ArgumentException("keyword");
			}
			syntaxKind = semicolonToken.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind != SyntaxKind.SemicolonToken)
			{
				throw new ArgumentException("semicolonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.AccessorDeclarationSyntax)SyntaxFactory.AccessorDeclaration(kind, attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), modifiers.Node.ToGreenList<SyntaxToken>(), (SyntaxToken)keyword.Node, (body == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.BlockSyntax)body.Green), (expressionBody == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ArrowExpressionClauseSyntax)expressionBody.Green), (SyntaxToken)semicolonToken.Node).CreateRed();
		}

		// Token: 0x06002B1D RID: 11037 RVA: 0x000E7580 File Offset: 0x000E5780
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AccessorDeclarationSyntax AccessorDeclaration(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax body, Microsoft.CodeAnalysis.CSharp.Syntax.ArrowExpressionClauseSyntax expressionBody)
		{
			return SyntaxFactory.AccessorDeclaration(kind, attributeLists, modifiers, SyntaxFactory.Token(SyntaxFactory.GetAccessorDeclarationKeywordKind(kind)), body, expressionBody, default(SyntaxToken));
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x000E75AC File Offset: 0x000E57AC
		public static Microsoft.CodeAnalysis.CSharp.Syntax.AccessorDeclarationSyntax AccessorDeclaration(SyntaxKind kind)
		{
			return SyntaxFactory.AccessorDeclaration(kind, default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), SyntaxFactory.Token(SyntaxFactory.GetAccessorDeclarationKeywordKind(kind)), null, null, default(SyntaxToken));
		}

		// Token: 0x06002B1F RID: 11039 RVA: 0x000E75E8 File Offset: 0x000E57E8
		private static SyntaxKind GetAccessorDeclarationKeywordKind(SyntaxKind kind)
		{
			SyntaxKind syntaxKind;
			switch (kind)
			{
			case SyntaxKind.GetAccessorDeclaration:
				syntaxKind = SyntaxKind.GetKeyword;
				break;
			case SyntaxKind.SetAccessorDeclaration:
				syntaxKind = SyntaxKind.SetKeyword;
				break;
			case SyntaxKind.AddAccessorDeclaration:
				syntaxKind = SyntaxKind.AddKeyword;
				break;
			case SyntaxKind.RemoveAccessorDeclaration:
				syntaxKind = SyntaxKind.RemoveKeyword;
				break;
			case SyntaxKind.UnknownAccessorDeclaration:
				syntaxKind = SyntaxKind.IdentifierToken;
				break;
			default:
				if (kind != SyntaxKind.InitAccessorDeclaration)
				{
					throw new ArgumentOutOfRangeException();
				}
				syntaxKind = SyntaxKind.InitKeyword;
				break;
			}
			return syntaxKind;
		}

		// Token: 0x06002B20 RID: 11040 RVA: 0x000E7658 File Offset: 0x000E5858
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax ParameterList(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax> parameters, SyntaxToken closeParenToken)
		{
			if (openParenToken.Kind() != SyntaxKind.OpenParenToken)
			{
				throw new ArgumentException("openParenToken");
			}
			if (closeParenToken.Kind() != SyntaxKind.CloseParenToken)
			{
				throw new ArgumentException("closeParenToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax)SyntaxFactory.ParameterList((SyntaxToken)openParenToken.Node, parameters.Node.ToGreenSeparatedList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ParameterSyntax>(), (SyntaxToken)closeParenToken.Node).CreateRed();
		}

		// Token: 0x06002B21 RID: 11041 RVA: 0x000E76C8 File Offset: 0x000E58C8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ParameterListSyntax ParameterList([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax> parameters = default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax>))
		{
			return SyntaxFactory.ParameterList(SyntaxFactory.Token(SyntaxKind.OpenParenToken), parameters, SyntaxFactory.Token(SyntaxKind.CloseParenToken));
		}

		// Token: 0x06002B22 RID: 11042 RVA: 0x000E76E4 File Offset: 0x000E58E4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.BracketedParameterListSyntax BracketedParameterList(SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax> parameters, SyntaxToken closeBracketToken)
		{
			if (openBracketToken.Kind() != SyntaxKind.OpenBracketToken)
			{
				throw new ArgumentException("openBracketToken");
			}
			if (closeBracketToken.Kind() != SyntaxKind.CloseBracketToken)
			{
				throw new ArgumentException("closeBracketToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.BracketedParameterListSyntax)SyntaxFactory.BracketedParameterList((SyntaxToken)openBracketToken.Node, parameters.Node.ToGreenSeparatedList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ParameterSyntax>(), (SyntaxToken)closeBracketToken.Node).CreateRed();
		}

		// Token: 0x06002B23 RID: 11043 RVA: 0x000E7754 File Offset: 0x000E5954
		public static Microsoft.CodeAnalysis.CSharp.Syntax.BracketedParameterListSyntax BracketedParameterList([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax> parameters = default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax>))
		{
			return SyntaxFactory.BracketedParameterList(SyntaxFactory.Token(SyntaxKind.OpenBracketToken), parameters, SyntaxFactory.Token(SyntaxKind.CloseBracketToken));
		}

		// Token: 0x06002B24 RID: 11044 RVA: 0x000E7770 File Offset: 0x000E5970
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax Parameter([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, SyntaxToken identifier, Microsoft.CodeAnalysis.CSharp.Syntax.EqualsValueClauseSyntax @default)
		{
			SyntaxKind syntaxKind = identifier.Kind();
			if (syntaxKind != SyntaxKind.ArgListKeyword && syntaxKind != SyntaxKind.IdentifierToken)
			{
				throw new ArgumentException("identifier");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax)SyntaxFactory.Parameter(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), modifiers.Node.ToGreenList<SyntaxToken>(), (type == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)type.Green), (SyntaxToken)identifier.Node, (@default == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.EqualsValueClauseSyntax)@default.Green)).CreateRed();
		}

		// Token: 0x06002B25 RID: 11045 RVA: 0x000E77F8 File Offset: 0x000E59F8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax Parameter(SyntaxToken identifier)
		{
			return SyntaxFactory.Parameter(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), null, identifier, null);
		}

		// Token: 0x06002B26 RID: 11046 RVA: 0x000E7820 File Offset: 0x000E5A20
		public static Microsoft.CodeAnalysis.CSharp.Syntax.FunctionPointerParameterSyntax FunctionPointerParameter([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.FunctionPointerParameterSyntax)SyntaxFactory.FunctionPointerParameter(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), modifiers.Node.ToGreenList<SyntaxToken>(), (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)type.Green).CreateRed();
		}

		// Token: 0x06002B27 RID: 11047 RVA: 0x000E7870 File Offset: 0x000E5A70
		public static Microsoft.CodeAnalysis.CSharp.Syntax.FunctionPointerParameterSyntax FunctionPointerParameter(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type)
		{
			return SyntaxFactory.FunctionPointerParameter(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), type);
		}

		// Token: 0x06002B28 RID: 11048 RVA: 0x000E7895 File Offset: 0x000E5A95
		public static Microsoft.CodeAnalysis.CSharp.Syntax.IncompleteMemberSyntax IncompleteMember([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type)
		{
			return (Microsoft.CodeAnalysis.CSharp.Syntax.IncompleteMemberSyntax)SyntaxFactory.IncompleteMember(attributeLists.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AttributeListSyntax>(), modifiers.Node.ToGreenList<SyntaxToken>(), (type == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)type.Green)).CreateRed();
		}

		// Token: 0x06002B29 RID: 11049 RVA: 0x000E78D0 File Offset: 0x000E5AD0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.IncompleteMemberSyntax IncompleteMember([global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type = null)
		{
			return SyntaxFactory.IncompleteMember(default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax>), default(SyntaxTokenList), type);
		}

		// Token: 0x06002B2A RID: 11050 RVA: 0x000E78F5 File Offset: 0x000E5AF5
		public static Microsoft.CodeAnalysis.CSharp.Syntax.SkippedTokensTriviaSyntax SkippedTokensTrivia(SyntaxTokenList tokens)
		{
			return (Microsoft.CodeAnalysis.CSharp.Syntax.SkippedTokensTriviaSyntax)SyntaxFactory.SkippedTokensTrivia(tokens.Node.ToGreenList<SyntaxToken>()).CreateRed();
		}

		// Token: 0x06002B2B RID: 11051 RVA: 0x000E7914 File Offset: 0x000E5B14
		public static Microsoft.CodeAnalysis.CSharp.Syntax.SkippedTokensTriviaSyntax SkippedTokensTrivia()
		{
			return SyntaxFactory.SkippedTokensTrivia(default(SyntaxTokenList));
		}

		// Token: 0x06002B2C RID: 11052 RVA: 0x000E7930 File Offset: 0x000E5B30
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DocumentationCommentTriviaSyntax DocumentationCommentTrivia(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax> content, SyntaxToken endOfComment)
		{
			if (kind - SyntaxKind.SingleLineDocumentationCommentTrivia > 1)
			{
				throw new ArgumentException("kind");
			}
			if (endOfComment.Kind() != SyntaxKind.EndOfDocumentationCommentToken)
			{
				throw new ArgumentException("endOfComment");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.DocumentationCommentTriviaSyntax)SyntaxFactory.DocumentationCommentTrivia(kind, content.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.XmlNodeSyntax>(), (SyntaxToken)endOfComment.Node).CreateRed();
		}

		// Token: 0x06002B2D RID: 11053 RVA: 0x000E7992 File Offset: 0x000E5B92
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DocumentationCommentTriviaSyntax DocumentationCommentTrivia(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax> content = default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax>))
		{
			return SyntaxFactory.DocumentationCommentTrivia(kind, content, SyntaxFactory.Token(SyntaxKind.EndOfDocumentationCommentToken));
		}

		// Token: 0x06002B2E RID: 11054 RVA: 0x000E79A5 File Offset: 0x000E5BA5
		public static Microsoft.CodeAnalysis.CSharp.Syntax.TypeCrefSyntax TypeCref(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.TypeCrefSyntax)SyntaxFactory.TypeCref((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)type.Green).CreateRed();
		}

		// Token: 0x06002B2F RID: 11055 RVA: 0x000E79D0 File Offset: 0x000E5BD0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.QualifiedCrefSyntax QualifiedCref(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax container, SyntaxToken dotToken, Microsoft.CodeAnalysis.CSharp.Syntax.MemberCrefSyntax member)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			if (dotToken.Kind() != SyntaxKind.DotToken)
			{
				throw new ArgumentException("dotToken");
			}
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.QualifiedCrefSyntax)SyntaxFactory.QualifiedCref((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)container.Green, (SyntaxToken)dotToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.MemberCrefSyntax)member.Green).CreateRed();
		}

		// Token: 0x06002B30 RID: 11056 RVA: 0x000E7A42 File Offset: 0x000E5C42
		public static Microsoft.CodeAnalysis.CSharp.Syntax.QualifiedCrefSyntax QualifiedCref(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax container, Microsoft.CodeAnalysis.CSharp.Syntax.MemberCrefSyntax member)
		{
			return SyntaxFactory.QualifiedCref(container, SyntaxFactory.Token(SyntaxKind.DotToken), member);
		}

		// Token: 0x06002B31 RID: 11057 RVA: 0x000E7A55 File Offset: 0x000E5C55
		public static Microsoft.CodeAnalysis.CSharp.Syntax.NameMemberCrefSyntax NameMemberCref(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax name, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.CrefParameterListSyntax parameters)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.NameMemberCrefSyntax)SyntaxFactory.NameMemberCref((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)name.Green, (parameters == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.CrefParameterListSyntax)parameters.Green)).CreateRed();
		}

		// Token: 0x06002B32 RID: 11058 RVA: 0x000E7A90 File Offset: 0x000E5C90
		public static Microsoft.CodeAnalysis.CSharp.Syntax.NameMemberCrefSyntax NameMemberCref(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax name)
		{
			return SyntaxFactory.NameMemberCref(name, null);
		}

		// Token: 0x06002B33 RID: 11059 RVA: 0x000E7A9C File Offset: 0x000E5C9C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.IndexerMemberCrefSyntax IndexerMemberCref(SyntaxToken thisKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.CrefBracketedParameterListSyntax parameters)
		{
			if (thisKeyword.Kind() != SyntaxKind.ThisKeyword)
			{
				throw new ArgumentException("thisKeyword");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.IndexerMemberCrefSyntax)SyntaxFactory.IndexerMemberCref((SyntaxToken)thisKeyword.Node, (parameters == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.CrefBracketedParameterListSyntax)parameters.Green)).CreateRed();
		}

		// Token: 0x06002B34 RID: 11060 RVA: 0x000E7AED File Offset: 0x000E5CED
		public static Microsoft.CodeAnalysis.CSharp.Syntax.IndexerMemberCrefSyntax IndexerMemberCref([global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.CrefBracketedParameterListSyntax parameters = null)
		{
			return SyntaxFactory.IndexerMemberCref(SyntaxFactory.Token(SyntaxKind.ThisKeyword), parameters);
		}

		// Token: 0x06002B35 RID: 11061 RVA: 0x000E7B00 File Offset: 0x000E5D00
		public static Microsoft.CodeAnalysis.CSharp.Syntax.OperatorMemberCrefSyntax OperatorMemberCref(SyntaxToken operatorKeyword, SyntaxToken operatorToken, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.CrefParameterListSyntax parameters)
		{
			if (operatorKeyword.Kind() != SyntaxKind.OperatorKeyword)
			{
				throw new ArgumentException("operatorKeyword");
			}
			SyntaxKind syntaxKind = operatorToken.Kind();
			if (syntaxKind <= SyntaxKind.GreaterThanToken)
			{
				switch (syntaxKind)
				{
				case SyntaxKind.TildeToken:
				case SyntaxKind.ExclamationToken:
				case SyntaxKind.PercentToken:
				case SyntaxKind.CaretToken:
				case SyntaxKind.AmpersandToken:
				case SyntaxKind.AsteriskToken:
				case SyntaxKind.MinusToken:
				case SyntaxKind.PlusToken:
				case SyntaxKind.BarToken:
					goto IL_00E6;
				case SyntaxKind.DollarToken:
				case SyntaxKind.OpenParenToken:
				case SyntaxKind.CloseParenToken:
				case SyntaxKind.EqualsToken:
				case SyntaxKind.OpenBraceToken:
				case SyntaxKind.CloseBraceToken:
				case SyntaxKind.OpenBracketToken:
				case SyntaxKind.CloseBracketToken:
					break;
				default:
					if (syntaxKind == SyntaxKind.LessThanToken || syntaxKind == SyntaxKind.GreaterThanToken)
					{
						goto IL_00E6;
					}
					break;
				}
			}
			else
			{
				if (syntaxKind == SyntaxKind.SlashToken)
				{
					goto IL_00E6;
				}
				switch (syntaxKind)
				{
				case SyntaxKind.MinusMinusToken:
				case SyntaxKind.PlusPlusToken:
				case SyntaxKind.ExclamationEqualsToken:
				case SyntaxKind.EqualsEqualsToken:
				case SyntaxKind.LessThanEqualsToken:
				case SyntaxKind.LessThanLessThanToken:
				case SyntaxKind.GreaterThanEqualsToken:
				case SyntaxKind.GreaterThanGreaterThanToken:
					goto IL_00E6;
				case SyntaxKind.ColonColonToken:
				case SyntaxKind.QuestionQuestionToken:
				case SyntaxKind.MinusGreaterThanToken:
				case SyntaxKind.EqualsGreaterThanToken:
				case SyntaxKind.LessThanLessThanEqualsToken:
					break;
				default:
					if (syntaxKind - SyntaxKind.TrueKeyword <= 1)
					{
						goto IL_00E6;
					}
					break;
				}
			}
			throw new ArgumentException("operatorToken");
			IL_00E6:
			return (Microsoft.CodeAnalysis.CSharp.Syntax.OperatorMemberCrefSyntax)SyntaxFactory.OperatorMemberCref((SyntaxToken)operatorKeyword.Node, (SyntaxToken)operatorToken.Node, (parameters == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.CrefParameterListSyntax)parameters.Green)).CreateRed();
		}

		// Token: 0x06002B36 RID: 11062 RVA: 0x000E7C2B File Offset: 0x000E5E2B
		public static Microsoft.CodeAnalysis.CSharp.Syntax.OperatorMemberCrefSyntax OperatorMemberCref(SyntaxToken operatorToken, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.CrefParameterListSyntax parameters)
		{
			return SyntaxFactory.OperatorMemberCref(SyntaxFactory.Token(SyntaxKind.OperatorKeyword), operatorToken, parameters);
		}

		// Token: 0x06002B37 RID: 11063 RVA: 0x000E7C3E File Offset: 0x000E5E3E
		public static Microsoft.CodeAnalysis.CSharp.Syntax.OperatorMemberCrefSyntax OperatorMemberCref(SyntaxToken operatorToken)
		{
			return SyntaxFactory.OperatorMemberCref(SyntaxFactory.Token(SyntaxKind.OperatorKeyword), operatorToken, null);
		}

		// Token: 0x06002B38 RID: 11064 RVA: 0x000E7C54 File Offset: 0x000E5E54
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ConversionOperatorMemberCrefSyntax ConversionOperatorMemberCref(SyntaxToken implicitOrExplicitKeyword, SyntaxToken operatorKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.CrefParameterListSyntax parameters)
		{
			SyntaxKind syntaxKind = implicitOrExplicitKeyword.Kind();
			if (syntaxKind - SyntaxKind.ExplicitKeyword > 1)
			{
				throw new ArgumentException("implicitOrExplicitKeyword");
			}
			if (operatorKeyword.Kind() != SyntaxKind.OperatorKeyword)
			{
				throw new ArgumentException("operatorKeyword");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ConversionOperatorMemberCrefSyntax)SyntaxFactory.ConversionOperatorMemberCref((SyntaxToken)implicitOrExplicitKeyword.Node, (SyntaxToken)operatorKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)type.Green, (parameters == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.CrefParameterListSyntax)parameters.Green)).CreateRed();
		}

		// Token: 0x06002B39 RID: 11065 RVA: 0x000E7CE6 File Offset: 0x000E5EE6
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ConversionOperatorMemberCrefSyntax ConversionOperatorMemberCref(SyntaxToken implicitOrExplicitKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.CrefParameterListSyntax parameters)
		{
			return SyntaxFactory.ConversionOperatorMemberCref(implicitOrExplicitKeyword, SyntaxFactory.Token(SyntaxKind.OperatorKeyword), type, parameters);
		}

		// Token: 0x06002B3A RID: 11066 RVA: 0x000E7CFA File Offset: 0x000E5EFA
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ConversionOperatorMemberCrefSyntax ConversionOperatorMemberCref(SyntaxToken implicitOrExplicitKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type)
		{
			return SyntaxFactory.ConversionOperatorMemberCref(implicitOrExplicitKeyword, SyntaxFactory.Token(SyntaxKind.OperatorKeyword), type, null);
		}

		// Token: 0x06002B3B RID: 11067 RVA: 0x000E7D10 File Offset: 0x000E5F10
		public static Microsoft.CodeAnalysis.CSharp.Syntax.CrefParameterListSyntax CrefParameterList(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.CrefParameterSyntax> parameters, SyntaxToken closeParenToken)
		{
			if (openParenToken.Kind() != SyntaxKind.OpenParenToken)
			{
				throw new ArgumentException("openParenToken");
			}
			if (closeParenToken.Kind() != SyntaxKind.CloseParenToken)
			{
				throw new ArgumentException("closeParenToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.CrefParameterListSyntax)SyntaxFactory.CrefParameterList((SyntaxToken)openParenToken.Node, parameters.Node.ToGreenSeparatedList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.CrefParameterSyntax>(), (SyntaxToken)closeParenToken.Node).CreateRed();
		}

		// Token: 0x06002B3C RID: 11068 RVA: 0x000E7D80 File Offset: 0x000E5F80
		public static Microsoft.CodeAnalysis.CSharp.Syntax.CrefParameterListSyntax CrefParameterList([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.CrefParameterSyntax> parameters = default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.CrefParameterSyntax>))
		{
			return SyntaxFactory.CrefParameterList(SyntaxFactory.Token(SyntaxKind.OpenParenToken), parameters, SyntaxFactory.Token(SyntaxKind.CloseParenToken));
		}

		// Token: 0x06002B3D RID: 11069 RVA: 0x000E7D9C File Offset: 0x000E5F9C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.CrefBracketedParameterListSyntax CrefBracketedParameterList(SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.CrefParameterSyntax> parameters, SyntaxToken closeBracketToken)
		{
			if (openBracketToken.Kind() != SyntaxKind.OpenBracketToken)
			{
				throw new ArgumentException("openBracketToken");
			}
			if (closeBracketToken.Kind() != SyntaxKind.CloseBracketToken)
			{
				throw new ArgumentException("closeBracketToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.CrefBracketedParameterListSyntax)SyntaxFactory.CrefBracketedParameterList((SyntaxToken)openBracketToken.Node, parameters.Node.ToGreenSeparatedList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.CrefParameterSyntax>(), (SyntaxToken)closeBracketToken.Node).CreateRed();
		}

		// Token: 0x06002B3E RID: 11070 RVA: 0x000E7E0C File Offset: 0x000E600C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.CrefBracketedParameterListSyntax CrefBracketedParameterList([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.CrefParameterSyntax> parameters = default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.CrefParameterSyntax>))
		{
			return SyntaxFactory.CrefBracketedParameterList(SyntaxFactory.Token(SyntaxKind.OpenBracketToken), parameters, SyntaxFactory.Token(SyntaxKind.CloseBracketToken));
		}

		// Token: 0x06002B3F RID: 11071 RVA: 0x000E7E28 File Offset: 0x000E6028
		public static Microsoft.CodeAnalysis.CSharp.Syntax.CrefParameterSyntax CrefParameter(SyntaxToken refKindKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type)
		{
			SyntaxKind syntaxKind = refKindKeyword.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind - SyntaxKind.RefKeyword > 2)
			{
				throw new ArgumentException("refKindKeyword");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.CrefParameterSyntax)SyntaxFactory.CrefParameter((SyntaxToken)refKindKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeSyntax)type.Green).CreateRed();
		}

		// Token: 0x06002B40 RID: 11072 RVA: 0x000E7E88 File Offset: 0x000E6088
		public static Microsoft.CodeAnalysis.CSharp.Syntax.CrefParameterSyntax CrefParameter(Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type)
		{
			return SyntaxFactory.CrefParameter(default(SyntaxToken), type);
		}

		// Token: 0x06002B41 RID: 11073 RVA: 0x000E7EA4 File Offset: 0x000E60A4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementSyntax XmlElement(Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementStartTagSyntax startTag, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax> content, Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementEndTagSyntax endTag)
		{
			if (startTag == null)
			{
				throw new ArgumentNullException("startTag");
			}
			if (endTag == null)
			{
				throw new ArgumentNullException("endTag");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementSyntax)SyntaxFactory.XmlElement((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.XmlElementStartTagSyntax)startTag.Green, content.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.XmlNodeSyntax>(), (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.XmlElementEndTagSyntax)endTag.Green).CreateRed();
		}

		// Token: 0x06002B42 RID: 11074 RVA: 0x000E7F00 File Offset: 0x000E6100
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementSyntax XmlElement(Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementStartTagSyntax startTag, Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementEndTagSyntax endTag)
		{
			return SyntaxFactory.XmlElement(startTag, default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax>), endTag);
		}

		// Token: 0x06002B43 RID: 11075 RVA: 0x000E7F20 File Offset: 0x000E6120
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementStartTagSyntax XmlElementStartTag(SyntaxToken lessThanToken, Microsoft.CodeAnalysis.CSharp.Syntax.XmlNameSyntax name, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.XmlAttributeSyntax> attributes, SyntaxToken greaterThanToken)
		{
			if (lessThanToken.Kind() != SyntaxKind.LessThanToken)
			{
				throw new ArgumentException("lessThanToken");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (greaterThanToken.Kind() != SyntaxKind.GreaterThanToken)
			{
				throw new ArgumentException("greaterThanToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementStartTagSyntax)SyntaxFactory.XmlElementStartTag((SyntaxToken)lessThanToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.XmlNameSyntax)name.Green, attributes.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.XmlAttributeSyntax>(), (SyntaxToken)greaterThanToken.Node).CreateRed();
		}

		// Token: 0x06002B44 RID: 11076 RVA: 0x000E7FA9 File Offset: 0x000E61A9
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementStartTagSyntax XmlElementStartTag(Microsoft.CodeAnalysis.CSharp.Syntax.XmlNameSyntax name, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.XmlAttributeSyntax> attributes)
		{
			return SyntaxFactory.XmlElementStartTag(SyntaxFactory.Token(SyntaxKind.LessThanToken), name, attributes, SyntaxFactory.Token(SyntaxKind.GreaterThanToken));
		}

		// Token: 0x06002B45 RID: 11077 RVA: 0x000E7FC8 File Offset: 0x000E61C8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementStartTagSyntax XmlElementStartTag(Microsoft.CodeAnalysis.CSharp.Syntax.XmlNameSyntax name)
		{
			return SyntaxFactory.XmlElementStartTag(SyntaxFactory.Token(SyntaxKind.LessThanToken), name, default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.XmlAttributeSyntax>), SyntaxFactory.Token(SyntaxKind.GreaterThanToken));
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x000E7FF8 File Offset: 0x000E61F8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementEndTagSyntax XmlElementEndTag(SyntaxToken lessThanSlashToken, Microsoft.CodeAnalysis.CSharp.Syntax.XmlNameSyntax name, SyntaxToken greaterThanToken)
		{
			if (lessThanSlashToken.Kind() != SyntaxKind.LessThanSlashToken)
			{
				throw new ArgumentException("lessThanSlashToken");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (greaterThanToken.Kind() != SyntaxKind.GreaterThanToken)
			{
				throw new ArgumentException("greaterThanToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementEndTagSyntax)SyntaxFactory.XmlElementEndTag((SyntaxToken)lessThanSlashToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.XmlNameSyntax)name.Green, (SyntaxToken)greaterThanToken.Node).CreateRed();
		}

		// Token: 0x06002B47 RID: 11079 RVA: 0x000E8075 File Offset: 0x000E6275
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlElementEndTagSyntax XmlElementEndTag(Microsoft.CodeAnalysis.CSharp.Syntax.XmlNameSyntax name)
		{
			return SyntaxFactory.XmlElementEndTag(SyntaxFactory.Token(SyntaxKind.LessThanSlashToken), name, SyntaxFactory.Token(SyntaxKind.GreaterThanToken));
		}

		// Token: 0x06002B48 RID: 11080 RVA: 0x000E8094 File Offset: 0x000E6294
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlEmptyElementSyntax XmlEmptyElement(SyntaxToken lessThanToken, Microsoft.CodeAnalysis.CSharp.Syntax.XmlNameSyntax name, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.XmlAttributeSyntax> attributes, SyntaxToken slashGreaterThanToken)
		{
			if (lessThanToken.Kind() != SyntaxKind.LessThanToken)
			{
				throw new ArgumentException("lessThanToken");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (slashGreaterThanToken.Kind() != SyntaxKind.SlashGreaterThanToken)
			{
				throw new ArgumentException("slashGreaterThanToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.XmlEmptyElementSyntax)SyntaxFactory.XmlEmptyElement((SyntaxToken)lessThanToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.XmlNameSyntax)name.Green, attributes.Node.ToGreenList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.XmlAttributeSyntax>(), (SyntaxToken)slashGreaterThanToken.Node).CreateRed();
		}

		// Token: 0x06002B49 RID: 11081 RVA: 0x000E811D File Offset: 0x000E631D
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlEmptyElementSyntax XmlEmptyElement(Microsoft.CodeAnalysis.CSharp.Syntax.XmlNameSyntax name, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.XmlAttributeSyntax> attributes)
		{
			return SyntaxFactory.XmlEmptyElement(SyntaxFactory.Token(SyntaxKind.LessThanToken), name, attributes, SyntaxFactory.Token(SyntaxKind.SlashGreaterThanToken));
		}

		// Token: 0x06002B4A RID: 11082 RVA: 0x000E813C File Offset: 0x000E633C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlEmptyElementSyntax XmlEmptyElement(Microsoft.CodeAnalysis.CSharp.Syntax.XmlNameSyntax name)
		{
			return SyntaxFactory.XmlEmptyElement(SyntaxFactory.Token(SyntaxKind.LessThanToken), name, default(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.XmlAttributeSyntax>), SyntaxFactory.Token(SyntaxKind.SlashGreaterThanToken));
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x000E816C File Offset: 0x000E636C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlNameSyntax XmlName([global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Syntax.XmlPrefixSyntax prefix, SyntaxToken localName)
		{
			if (localName.Kind() != SyntaxKind.IdentifierToken)
			{
				throw new ArgumentException("localName");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.XmlNameSyntax)SyntaxFactory.XmlName((prefix == null) ? null : ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.XmlPrefixSyntax)prefix.Green), (SyntaxToken)localName.Node).CreateRed();
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x000E81BD File Offset: 0x000E63BD
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlNameSyntax XmlName(SyntaxToken localName)
		{
			return SyntaxFactory.XmlName(null, localName);
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x000E81C6 File Offset: 0x000E63C6
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlNameSyntax XmlName(string localName)
		{
			return SyntaxFactory.XmlName(null, SyntaxFactory.Identifier(localName));
		}

		// Token: 0x06002B4E RID: 11086 RVA: 0x000E81D4 File Offset: 0x000E63D4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlPrefixSyntax XmlPrefix(SyntaxToken prefix, SyntaxToken colonToken)
		{
			if (prefix.Kind() != SyntaxKind.IdentifierToken)
			{
				throw new ArgumentException("prefix");
			}
			if (colonToken.Kind() != SyntaxKind.ColonToken)
			{
				throw new ArgumentException("colonToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.XmlPrefixSyntax)SyntaxFactory.XmlPrefix((SyntaxToken)prefix.Node, (SyntaxToken)colonToken.Node).CreateRed();
		}

		// Token: 0x06002B4F RID: 11087 RVA: 0x000E8238 File Offset: 0x000E6438
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlPrefixSyntax XmlPrefix(SyntaxToken prefix)
		{
			return SyntaxFactory.XmlPrefix(prefix, SyntaxFactory.Token(SyntaxKind.ColonToken));
		}

		// Token: 0x06002B50 RID: 11088 RVA: 0x000E824A File Offset: 0x000E644A
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlPrefixSyntax XmlPrefix(string prefix)
		{
			return SyntaxFactory.XmlPrefix(SyntaxFactory.Identifier(prefix), SyntaxFactory.Token(SyntaxKind.ColonToken));
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x000E8264 File Offset: 0x000E6464
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlTextAttributeSyntax XmlTextAttribute(Microsoft.CodeAnalysis.CSharp.Syntax.XmlNameSyntax name, SyntaxToken equalsToken, SyntaxToken startQuoteToken, SyntaxTokenList textTokens, SyntaxToken endQuoteToken)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (equalsToken.Kind() != SyntaxKind.EqualsToken)
			{
				throw new ArgumentException("equalsToken");
			}
			SyntaxKind syntaxKind = startQuoteToken.Kind();
			if (syntaxKind - SyntaxKind.DoubleQuoteToken > 1)
			{
				throw new ArgumentException("startQuoteToken");
			}
			syntaxKind = endQuoteToken.Kind();
			if (syntaxKind - SyntaxKind.DoubleQuoteToken > 1)
			{
				throw new ArgumentException("endQuoteToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.XmlTextAttributeSyntax)SyntaxFactory.XmlTextAttribute((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.XmlNameSyntax)name.Green, (SyntaxToken)equalsToken.Node, (SyntaxToken)startQuoteToken.Node, textTokens.Node.ToGreenList<SyntaxToken>(), (SyntaxToken)endQuoteToken.Node).CreateRed();
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x000E831A File Offset: 0x000E651A
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlTextAttributeSyntax XmlTextAttribute(Microsoft.CodeAnalysis.CSharp.Syntax.XmlNameSyntax name, SyntaxToken startQuoteToken, SyntaxTokenList textTokens, SyntaxToken endQuoteToken)
		{
			return SyntaxFactory.XmlTextAttribute(name, SyntaxFactory.Token(SyntaxKind.EqualsToken), startQuoteToken, textTokens, endQuoteToken);
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x000E8330 File Offset: 0x000E6530
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlTextAttributeSyntax XmlTextAttribute(Microsoft.CodeAnalysis.CSharp.Syntax.XmlNameSyntax name, SyntaxToken startQuoteToken, SyntaxToken endQuoteToken)
		{
			return SyntaxFactory.XmlTextAttribute(name, SyntaxFactory.Token(SyntaxKind.EqualsToken), startQuoteToken, default(SyntaxTokenList), endQuoteToken);
		}

		// Token: 0x06002B54 RID: 11092 RVA: 0x000E8358 File Offset: 0x000E6558
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlCrefAttributeSyntax XmlCrefAttribute(Microsoft.CodeAnalysis.CSharp.Syntax.XmlNameSyntax name, SyntaxToken equalsToken, SyntaxToken startQuoteToken, Microsoft.CodeAnalysis.CSharp.Syntax.CrefSyntax cref, SyntaxToken endQuoteToken)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (equalsToken.Kind() != SyntaxKind.EqualsToken)
			{
				throw new ArgumentException("equalsToken");
			}
			SyntaxKind syntaxKind = startQuoteToken.Kind();
			if (syntaxKind - SyntaxKind.DoubleQuoteToken > 1)
			{
				throw new ArgumentException("startQuoteToken");
			}
			if (cref == null)
			{
				throw new ArgumentNullException("cref");
			}
			syntaxKind = endQuoteToken.Kind();
			if (syntaxKind - SyntaxKind.DoubleQuoteToken > 1)
			{
				throw new ArgumentException("endQuoteToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.XmlCrefAttributeSyntax)SyntaxFactory.XmlCrefAttribute((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.XmlNameSyntax)name.Green, (SyntaxToken)equalsToken.Node, (SyntaxToken)startQuoteToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.CrefSyntax)cref.Green, (SyntaxToken)endQuoteToken.Node).CreateRed();
		}

		// Token: 0x06002B55 RID: 11093 RVA: 0x000E841B File Offset: 0x000E661B
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlCrefAttributeSyntax XmlCrefAttribute(Microsoft.CodeAnalysis.CSharp.Syntax.XmlNameSyntax name, SyntaxToken startQuoteToken, Microsoft.CodeAnalysis.CSharp.Syntax.CrefSyntax cref, SyntaxToken endQuoteToken)
		{
			return SyntaxFactory.XmlCrefAttribute(name, SyntaxFactory.Token(SyntaxKind.EqualsToken), startQuoteToken, cref, endQuoteToken);
		}

		// Token: 0x06002B56 RID: 11094 RVA: 0x000E8430 File Offset: 0x000E6630
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlNameAttributeSyntax XmlNameAttribute(Microsoft.CodeAnalysis.CSharp.Syntax.XmlNameSyntax name, SyntaxToken equalsToken, SyntaxToken startQuoteToken, Microsoft.CodeAnalysis.CSharp.Syntax.IdentifierNameSyntax identifier, SyntaxToken endQuoteToken)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (equalsToken.Kind() != SyntaxKind.EqualsToken)
			{
				throw new ArgumentException("equalsToken");
			}
			SyntaxKind syntaxKind = startQuoteToken.Kind();
			if (syntaxKind - SyntaxKind.DoubleQuoteToken > 1)
			{
				throw new ArgumentException("startQuoteToken");
			}
			if (identifier == null)
			{
				throw new ArgumentNullException("identifier");
			}
			syntaxKind = endQuoteToken.Kind();
			if (syntaxKind - SyntaxKind.DoubleQuoteToken > 1)
			{
				throw new ArgumentException("endQuoteToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.XmlNameAttributeSyntax)SyntaxFactory.XmlNameAttribute((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.XmlNameSyntax)name.Green, (SyntaxToken)equalsToken.Node, (SyntaxToken)startQuoteToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.IdentifierNameSyntax)identifier.Green, (SyntaxToken)endQuoteToken.Node).CreateRed();
		}

		// Token: 0x06002B57 RID: 11095 RVA: 0x000E84F3 File Offset: 0x000E66F3
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlNameAttributeSyntax XmlNameAttribute(Microsoft.CodeAnalysis.CSharp.Syntax.XmlNameSyntax name, SyntaxToken startQuoteToken, Microsoft.CodeAnalysis.CSharp.Syntax.IdentifierNameSyntax identifier, SyntaxToken endQuoteToken)
		{
			return SyntaxFactory.XmlNameAttribute(name, SyntaxFactory.Token(SyntaxKind.EqualsToken), startQuoteToken, identifier, endQuoteToken);
		}

		// Token: 0x06002B58 RID: 11096 RVA: 0x000E8508 File Offset: 0x000E6708
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlNameAttributeSyntax XmlNameAttribute(Microsoft.CodeAnalysis.CSharp.Syntax.XmlNameSyntax name, SyntaxToken startQuoteToken, string identifier, SyntaxToken endQuoteToken)
		{
			return SyntaxFactory.XmlNameAttribute(name, SyntaxFactory.Token(SyntaxKind.EqualsToken), startQuoteToken, SyntaxFactory.IdentifierName(identifier), endQuoteToken);
		}

		// Token: 0x06002B59 RID: 11097 RVA: 0x000E8522 File Offset: 0x000E6722
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlTextSyntax XmlText(SyntaxTokenList textTokens)
		{
			return (Microsoft.CodeAnalysis.CSharp.Syntax.XmlTextSyntax)SyntaxFactory.XmlText(textTokens.Node.ToGreenList<SyntaxToken>()).CreateRed();
		}

		// Token: 0x06002B5A RID: 11098 RVA: 0x000E8540 File Offset: 0x000E6740
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlTextSyntax XmlText()
		{
			return SyntaxFactory.XmlText(default(SyntaxTokenList));
		}

		// Token: 0x06002B5B RID: 11099 RVA: 0x000E855C File Offset: 0x000E675C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlCDataSectionSyntax XmlCDataSection(SyntaxToken startCDataToken, SyntaxTokenList textTokens, SyntaxToken endCDataToken)
		{
			if (startCDataToken.Kind() != SyntaxKind.XmlCDataStartToken)
			{
				throw new ArgumentException("startCDataToken");
			}
			if (endCDataToken.Kind() != SyntaxKind.XmlCDataEndToken)
			{
				throw new ArgumentException("endCDataToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.XmlCDataSectionSyntax)SyntaxFactory.XmlCDataSection((SyntaxToken)startCDataToken.Node, textTokens.Node.ToGreenList<SyntaxToken>(), (SyntaxToken)endCDataToken.Node).CreateRed();
		}

		// Token: 0x06002B5C RID: 11100 RVA: 0x000E85CC File Offset: 0x000E67CC
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlCDataSectionSyntax XmlCDataSection(SyntaxTokenList textTokens = default(SyntaxTokenList))
		{
			return SyntaxFactory.XmlCDataSection(SyntaxFactory.Token(SyntaxKind.XmlCDataStartToken), textTokens, SyntaxFactory.Token(SyntaxKind.XmlCDataEndToken));
		}

		// Token: 0x06002B5D RID: 11101 RVA: 0x000E85E8 File Offset: 0x000E67E8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlProcessingInstructionSyntax XmlProcessingInstruction(SyntaxToken startProcessingInstructionToken, Microsoft.CodeAnalysis.CSharp.Syntax.XmlNameSyntax name, SyntaxTokenList textTokens, SyntaxToken endProcessingInstructionToken)
		{
			if (startProcessingInstructionToken.Kind() != SyntaxKind.XmlProcessingInstructionStartToken)
			{
				throw new ArgumentException("startProcessingInstructionToken");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (endProcessingInstructionToken.Kind() != SyntaxKind.XmlProcessingInstructionEndToken)
			{
				throw new ArgumentException("endProcessingInstructionToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.XmlProcessingInstructionSyntax)SyntaxFactory.XmlProcessingInstruction((SyntaxToken)startProcessingInstructionToken.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.XmlNameSyntax)name.Green, textTokens.Node.ToGreenList<SyntaxToken>(), (SyntaxToken)endProcessingInstructionToken.Node).CreateRed();
		}

		// Token: 0x06002B5E RID: 11102 RVA: 0x000E8671 File Offset: 0x000E6871
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlProcessingInstructionSyntax XmlProcessingInstruction(Microsoft.CodeAnalysis.CSharp.Syntax.XmlNameSyntax name, SyntaxTokenList textTokens)
		{
			return SyntaxFactory.XmlProcessingInstruction(SyntaxFactory.Token(SyntaxKind.XmlProcessingInstructionStartToken), name, textTokens, SyntaxFactory.Token(SyntaxKind.XmlProcessingInstructionEndToken));
		}

		// Token: 0x06002B5F RID: 11103 RVA: 0x000E8690 File Offset: 0x000E6890
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlProcessingInstructionSyntax XmlProcessingInstruction(Microsoft.CodeAnalysis.CSharp.Syntax.XmlNameSyntax name)
		{
			return SyntaxFactory.XmlProcessingInstruction(SyntaxFactory.Token(SyntaxKind.XmlProcessingInstructionStartToken), name, default(SyntaxTokenList), SyntaxFactory.Token(SyntaxKind.XmlProcessingInstructionEndToken));
		}

		// Token: 0x06002B60 RID: 11104 RVA: 0x000E86C0 File Offset: 0x000E68C0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlCommentSyntax XmlComment(SyntaxToken lessThanExclamationMinusMinusToken, SyntaxTokenList textTokens, SyntaxToken minusMinusGreaterThanToken)
		{
			if (lessThanExclamationMinusMinusToken.Kind() != SyntaxKind.XmlCommentStartToken)
			{
				throw new ArgumentException("lessThanExclamationMinusMinusToken");
			}
			if (minusMinusGreaterThanToken.Kind() != SyntaxKind.XmlCommentEndToken)
			{
				throw new ArgumentException("minusMinusGreaterThanToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.XmlCommentSyntax)SyntaxFactory.XmlComment((SyntaxToken)lessThanExclamationMinusMinusToken.Node, textTokens.Node.ToGreenList<SyntaxToken>(), (SyntaxToken)minusMinusGreaterThanToken.Node).CreateRed();
		}

		// Token: 0x06002B61 RID: 11105 RVA: 0x000E8730 File Offset: 0x000E6930
		public static Microsoft.CodeAnalysis.CSharp.Syntax.XmlCommentSyntax XmlComment(SyntaxTokenList textTokens = default(SyntaxTokenList))
		{
			return SyntaxFactory.XmlComment(SyntaxFactory.Token(SyntaxKind.XmlCommentStartToken), textTokens, SyntaxFactory.Token(SyntaxKind.XmlCommentEndToken));
		}

		// Token: 0x06002B62 RID: 11106 RVA: 0x000E874C File Offset: 0x000E694C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.IfDirectiveTriviaSyntax IfDirectiveTrivia(SyntaxToken hashToken, SyntaxToken ifKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax condition, SyntaxToken endOfDirectiveToken, bool isActive, bool branchTaken, bool conditionValue)
		{
			if (hashToken.Kind() != SyntaxKind.HashToken)
			{
				throw new ArgumentException("hashToken");
			}
			if (ifKeyword.Kind() != SyntaxKind.IfKeyword)
			{
				throw new ArgumentException("ifKeyword");
			}
			if (condition == null)
			{
				throw new ArgumentNullException("condition");
			}
			if (endOfDirectiveToken.Kind() != SyntaxKind.EndOfDirectiveToken)
			{
				throw new ArgumentException("endOfDirectiveToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.IfDirectiveTriviaSyntax)SyntaxFactory.IfDirectiveTrivia((SyntaxToken)hashToken.Node, (SyntaxToken)ifKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)condition.Green, (SyntaxToken)endOfDirectiveToken.Node, isActive, branchTaken, conditionValue).CreateRed();
		}

		// Token: 0x06002B63 RID: 11107 RVA: 0x000E87F3 File Offset: 0x000E69F3
		public static Microsoft.CodeAnalysis.CSharp.Syntax.IfDirectiveTriviaSyntax IfDirectiveTrivia(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax condition, bool isActive, bool branchTaken, bool conditionValue)
		{
			return SyntaxFactory.IfDirectiveTrivia(SyntaxFactory.Token(SyntaxKind.HashToken), SyntaxFactory.Token(SyntaxKind.IfKeyword), condition, SyntaxFactory.Token(SyntaxKind.EndOfDirectiveToken), isActive, branchTaken, conditionValue);
		}

		// Token: 0x06002B64 RID: 11108 RVA: 0x000E881C File Offset: 0x000E6A1C
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ElifDirectiveTriviaSyntax ElifDirectiveTrivia(SyntaxToken hashToken, SyntaxToken elifKeyword, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax condition, SyntaxToken endOfDirectiveToken, bool isActive, bool branchTaken, bool conditionValue)
		{
			if (hashToken.Kind() != SyntaxKind.HashToken)
			{
				throw new ArgumentException("hashToken");
			}
			if (elifKeyword.Kind() != SyntaxKind.ElifKeyword)
			{
				throw new ArgumentException("elifKeyword");
			}
			if (condition == null)
			{
				throw new ArgumentNullException("condition");
			}
			if (endOfDirectiveToken.Kind() != SyntaxKind.EndOfDirectiveToken)
			{
				throw new ArgumentException("endOfDirectiveToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ElifDirectiveTriviaSyntax)SyntaxFactory.ElifDirectiveTrivia((SyntaxToken)hashToken.Node, (SyntaxToken)elifKeyword.Node, (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax)condition.Green, (SyntaxToken)endOfDirectiveToken.Node, isActive, branchTaken, conditionValue).CreateRed();
		}

		// Token: 0x06002B65 RID: 11109 RVA: 0x000E88C3 File Offset: 0x000E6AC3
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ElifDirectiveTriviaSyntax ElifDirectiveTrivia(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax condition, bool isActive, bool branchTaken, bool conditionValue)
		{
			return SyntaxFactory.ElifDirectiveTrivia(SyntaxFactory.Token(SyntaxKind.HashToken), SyntaxFactory.Token(SyntaxKind.ElifKeyword), condition, SyntaxFactory.Token(SyntaxKind.EndOfDirectiveToken), isActive, branchTaken, conditionValue);
		}

		// Token: 0x06002B66 RID: 11110 RVA: 0x000E88EC File Offset: 0x000E6AEC
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ElseDirectiveTriviaSyntax ElseDirectiveTrivia(SyntaxToken hashToken, SyntaxToken elseKeyword, SyntaxToken endOfDirectiveToken, bool isActive, bool branchTaken)
		{
			if (hashToken.Kind() != SyntaxKind.HashToken)
			{
				throw new ArgumentException("hashToken");
			}
			if (elseKeyword.Kind() != SyntaxKind.ElseKeyword)
			{
				throw new ArgumentException("elseKeyword");
			}
			if (endOfDirectiveToken.Kind() != SyntaxKind.EndOfDirectiveToken)
			{
				throw new ArgumentException("endOfDirectiveToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ElseDirectiveTriviaSyntax)SyntaxFactory.ElseDirectiveTrivia((SyntaxToken)hashToken.Node, (SyntaxToken)elseKeyword.Node, (SyntaxToken)endOfDirectiveToken.Node, isActive, branchTaken).CreateRed();
		}

		// Token: 0x06002B67 RID: 11111 RVA: 0x000E8977 File Offset: 0x000E6B77
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ElseDirectiveTriviaSyntax ElseDirectiveTrivia(bool isActive, bool branchTaken)
		{
			return SyntaxFactory.ElseDirectiveTrivia(SyntaxFactory.Token(SyntaxKind.HashToken), SyntaxFactory.Token(SyntaxKind.ElseKeyword), SyntaxFactory.Token(SyntaxKind.EndOfDirectiveToken), isActive, branchTaken);
		}

		// Token: 0x06002B68 RID: 11112 RVA: 0x000E89A0 File Offset: 0x000E6BA0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.EndIfDirectiveTriviaSyntax EndIfDirectiveTrivia(SyntaxToken hashToken, SyntaxToken endIfKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (hashToken.Kind() != SyntaxKind.HashToken)
			{
				throw new ArgumentException("hashToken");
			}
			if (endIfKeyword.Kind() != SyntaxKind.EndIfKeyword)
			{
				throw new ArgumentException("endIfKeyword");
			}
			if (endOfDirectiveToken.Kind() != SyntaxKind.EndOfDirectiveToken)
			{
				throw new ArgumentException("endOfDirectiveToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.EndIfDirectiveTriviaSyntax)SyntaxFactory.EndIfDirectiveTrivia((SyntaxToken)hashToken.Node, (SyntaxToken)endIfKeyword.Node, (SyntaxToken)endOfDirectiveToken.Node, isActive).CreateRed();
		}

		// Token: 0x06002B69 RID: 11113 RVA: 0x000E8A29 File Offset: 0x000E6C29
		public static Microsoft.CodeAnalysis.CSharp.Syntax.EndIfDirectiveTriviaSyntax EndIfDirectiveTrivia(bool isActive)
		{
			return SyntaxFactory.EndIfDirectiveTrivia(SyntaxFactory.Token(SyntaxKind.HashToken), SyntaxFactory.Token(SyntaxKind.EndIfKeyword), SyntaxFactory.Token(SyntaxKind.EndOfDirectiveToken), isActive);
		}

		// Token: 0x06002B6A RID: 11114 RVA: 0x000E8A50 File Offset: 0x000E6C50
		public static Microsoft.CodeAnalysis.CSharp.Syntax.RegionDirectiveTriviaSyntax RegionDirectiveTrivia(SyntaxToken hashToken, SyntaxToken regionKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (hashToken.Kind() != SyntaxKind.HashToken)
			{
				throw new ArgumentException("hashToken");
			}
			if (regionKeyword.Kind() != SyntaxKind.RegionKeyword)
			{
				throw new ArgumentException("regionKeyword");
			}
			if (endOfDirectiveToken.Kind() != SyntaxKind.EndOfDirectiveToken)
			{
				throw new ArgumentException("endOfDirectiveToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.RegionDirectiveTriviaSyntax)SyntaxFactory.RegionDirectiveTrivia((SyntaxToken)hashToken.Node, (SyntaxToken)regionKeyword.Node, (SyntaxToken)endOfDirectiveToken.Node, isActive).CreateRed();
		}

		// Token: 0x06002B6B RID: 11115 RVA: 0x000E8AD9 File Offset: 0x000E6CD9
		public static Microsoft.CodeAnalysis.CSharp.Syntax.RegionDirectiveTriviaSyntax RegionDirectiveTrivia(bool isActive)
		{
			return SyntaxFactory.RegionDirectiveTrivia(SyntaxFactory.Token(SyntaxKind.HashToken), SyntaxFactory.Token(SyntaxKind.RegionKeyword), SyntaxFactory.Token(SyntaxKind.EndOfDirectiveToken), isActive);
		}

		// Token: 0x06002B6C RID: 11116 RVA: 0x000E8B00 File Offset: 0x000E6D00
		public static Microsoft.CodeAnalysis.CSharp.Syntax.EndRegionDirectiveTriviaSyntax EndRegionDirectiveTrivia(SyntaxToken hashToken, SyntaxToken endRegionKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (hashToken.Kind() != SyntaxKind.HashToken)
			{
				throw new ArgumentException("hashToken");
			}
			if (endRegionKeyword.Kind() != SyntaxKind.EndRegionKeyword)
			{
				throw new ArgumentException("endRegionKeyword");
			}
			if (endOfDirectiveToken.Kind() != SyntaxKind.EndOfDirectiveToken)
			{
				throw new ArgumentException("endOfDirectiveToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.EndRegionDirectiveTriviaSyntax)SyntaxFactory.EndRegionDirectiveTrivia((SyntaxToken)hashToken.Node, (SyntaxToken)endRegionKeyword.Node, (SyntaxToken)endOfDirectiveToken.Node, isActive).CreateRed();
		}

		// Token: 0x06002B6D RID: 11117 RVA: 0x000E8B89 File Offset: 0x000E6D89
		public static Microsoft.CodeAnalysis.CSharp.Syntax.EndRegionDirectiveTriviaSyntax EndRegionDirectiveTrivia(bool isActive)
		{
			return SyntaxFactory.EndRegionDirectiveTrivia(SyntaxFactory.Token(SyntaxKind.HashToken), SyntaxFactory.Token(SyntaxKind.EndRegionKeyword), SyntaxFactory.Token(SyntaxKind.EndOfDirectiveToken), isActive);
		}

		// Token: 0x06002B6E RID: 11118 RVA: 0x000E8BB0 File Offset: 0x000E6DB0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ErrorDirectiveTriviaSyntax ErrorDirectiveTrivia(SyntaxToken hashToken, SyntaxToken errorKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (hashToken.Kind() != SyntaxKind.HashToken)
			{
				throw new ArgumentException("hashToken");
			}
			if (errorKeyword.Kind() != SyntaxKind.ErrorKeyword)
			{
				throw new ArgumentException("errorKeyword");
			}
			if (endOfDirectiveToken.Kind() != SyntaxKind.EndOfDirectiveToken)
			{
				throw new ArgumentException("endOfDirectiveToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ErrorDirectiveTriviaSyntax)SyntaxFactory.ErrorDirectiveTrivia((SyntaxToken)hashToken.Node, (SyntaxToken)errorKeyword.Node, (SyntaxToken)endOfDirectiveToken.Node, isActive).CreateRed();
		}

		// Token: 0x06002B6F RID: 11119 RVA: 0x000E8C39 File Offset: 0x000E6E39
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ErrorDirectiveTriviaSyntax ErrorDirectiveTrivia(bool isActive)
		{
			return SyntaxFactory.ErrorDirectiveTrivia(SyntaxFactory.Token(SyntaxKind.HashToken), SyntaxFactory.Token(SyntaxKind.ErrorKeyword), SyntaxFactory.Token(SyntaxKind.EndOfDirectiveToken), isActive);
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x000E8C60 File Offset: 0x000E6E60
		public static Microsoft.CodeAnalysis.CSharp.Syntax.WarningDirectiveTriviaSyntax WarningDirectiveTrivia(SyntaxToken hashToken, SyntaxToken warningKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (hashToken.Kind() != SyntaxKind.HashToken)
			{
				throw new ArgumentException("hashToken");
			}
			if (warningKeyword.Kind() != SyntaxKind.WarningKeyword)
			{
				throw new ArgumentException("warningKeyword");
			}
			if (endOfDirectiveToken.Kind() != SyntaxKind.EndOfDirectiveToken)
			{
				throw new ArgumentException("endOfDirectiveToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.WarningDirectiveTriviaSyntax)SyntaxFactory.WarningDirectiveTrivia((SyntaxToken)hashToken.Node, (SyntaxToken)warningKeyword.Node, (SyntaxToken)endOfDirectiveToken.Node, isActive).CreateRed();
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x000E8CE9 File Offset: 0x000E6EE9
		public static Microsoft.CodeAnalysis.CSharp.Syntax.WarningDirectiveTriviaSyntax WarningDirectiveTrivia(bool isActive)
		{
			return SyntaxFactory.WarningDirectiveTrivia(SyntaxFactory.Token(SyntaxKind.HashToken), SyntaxFactory.Token(SyntaxKind.WarningKeyword), SyntaxFactory.Token(SyntaxKind.EndOfDirectiveToken), isActive);
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x000E8D10 File Offset: 0x000E6F10
		public static Microsoft.CodeAnalysis.CSharp.Syntax.BadDirectiveTriviaSyntax BadDirectiveTrivia(SyntaxToken hashToken, SyntaxToken identifier, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (hashToken.Kind() != SyntaxKind.HashToken)
			{
				throw new ArgumentException("hashToken");
			}
			if (endOfDirectiveToken.Kind() != SyntaxKind.EndOfDirectiveToken)
			{
				throw new ArgumentException("endOfDirectiveToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.BadDirectiveTriviaSyntax)SyntaxFactory.BadDirectiveTrivia((SyntaxToken)hashToken.Node, (SyntaxToken)identifier.Node, (SyntaxToken)endOfDirectiveToken.Node, isActive).CreateRed();
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x000E8D81 File Offset: 0x000E6F81
		public static Microsoft.CodeAnalysis.CSharp.Syntax.BadDirectiveTriviaSyntax BadDirectiveTrivia(SyntaxToken identifier, bool isActive)
		{
			return SyntaxFactory.BadDirectiveTrivia(SyntaxFactory.Token(SyntaxKind.HashToken), identifier, SyntaxFactory.Token(SyntaxKind.EndOfDirectiveToken), isActive);
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x000E8DA0 File Offset: 0x000E6FA0
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DefineDirectiveTriviaSyntax DefineDirectiveTrivia(SyntaxToken hashToken, SyntaxToken defineKeyword, SyntaxToken name, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (hashToken.Kind() != SyntaxKind.HashToken)
			{
				throw new ArgumentException("hashToken");
			}
			if (defineKeyword.Kind() != SyntaxKind.DefineKeyword)
			{
				throw new ArgumentException("defineKeyword");
			}
			if (name.Kind() != SyntaxKind.IdentifierToken)
			{
				throw new ArgumentException("name");
			}
			if (endOfDirectiveToken.Kind() != SyntaxKind.EndOfDirectiveToken)
			{
				throw new ArgumentException("endOfDirectiveToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.DefineDirectiveTriviaSyntax)SyntaxFactory.DefineDirectiveTrivia((SyntaxToken)hashToken.Node, (SyntaxToken)defineKeyword.Node, (SyntaxToken)name.Node, (SyntaxToken)endOfDirectiveToken.Node, isActive).CreateRed();
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x000E8E4E File Offset: 0x000E704E
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DefineDirectiveTriviaSyntax DefineDirectiveTrivia(SyntaxToken name, bool isActive)
		{
			return SyntaxFactory.DefineDirectiveTrivia(SyntaxFactory.Token(SyntaxKind.HashToken), SyntaxFactory.Token(SyntaxKind.DefineKeyword), name, SyntaxFactory.Token(SyntaxKind.EndOfDirectiveToken), isActive);
		}

		// Token: 0x06002B76 RID: 11126 RVA: 0x000E8E75 File Offset: 0x000E7075
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DefineDirectiveTriviaSyntax DefineDirectiveTrivia(string name, bool isActive)
		{
			return SyntaxFactory.DefineDirectiveTrivia(SyntaxFactory.Token(SyntaxKind.HashToken), SyntaxFactory.Token(SyntaxKind.DefineKeyword), SyntaxFactory.Identifier(name), SyntaxFactory.Token(SyntaxKind.EndOfDirectiveToken), isActive);
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x000E8EA4 File Offset: 0x000E70A4
		public static Microsoft.CodeAnalysis.CSharp.Syntax.UndefDirectiveTriviaSyntax UndefDirectiveTrivia(SyntaxToken hashToken, SyntaxToken undefKeyword, SyntaxToken name, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (hashToken.Kind() != SyntaxKind.HashToken)
			{
				throw new ArgumentException("hashToken");
			}
			if (undefKeyword.Kind() != SyntaxKind.UndefKeyword)
			{
				throw new ArgumentException("undefKeyword");
			}
			if (name.Kind() != SyntaxKind.IdentifierToken)
			{
				throw new ArgumentException("name");
			}
			if (endOfDirectiveToken.Kind() != SyntaxKind.EndOfDirectiveToken)
			{
				throw new ArgumentException("endOfDirectiveToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.UndefDirectiveTriviaSyntax)SyntaxFactory.UndefDirectiveTrivia((SyntaxToken)hashToken.Node, (SyntaxToken)undefKeyword.Node, (SyntaxToken)name.Node, (SyntaxToken)endOfDirectiveToken.Node, isActive).CreateRed();
		}

		// Token: 0x06002B78 RID: 11128 RVA: 0x000E8F52 File Offset: 0x000E7152
		public static Microsoft.CodeAnalysis.CSharp.Syntax.UndefDirectiveTriviaSyntax UndefDirectiveTrivia(SyntaxToken name, bool isActive)
		{
			return SyntaxFactory.UndefDirectiveTrivia(SyntaxFactory.Token(SyntaxKind.HashToken), SyntaxFactory.Token(SyntaxKind.UndefKeyword), name, SyntaxFactory.Token(SyntaxKind.EndOfDirectiveToken), isActive);
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x000E8F79 File Offset: 0x000E7179
		public static Microsoft.CodeAnalysis.CSharp.Syntax.UndefDirectiveTriviaSyntax UndefDirectiveTrivia(string name, bool isActive)
		{
			return SyntaxFactory.UndefDirectiveTrivia(SyntaxFactory.Token(SyntaxKind.HashToken), SyntaxFactory.Token(SyntaxKind.UndefKeyword), SyntaxFactory.Identifier(name), SyntaxFactory.Token(SyntaxKind.EndOfDirectiveToken), isActive);
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x000E8FA8 File Offset: 0x000E71A8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.LineDirectiveTriviaSyntax LineDirectiveTrivia(SyntaxToken hashToken, SyntaxToken lineKeyword, SyntaxToken line, SyntaxToken file, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (hashToken.Kind() != SyntaxKind.HashToken)
			{
				throw new ArgumentException("hashToken");
			}
			if (lineKeyword.Kind() != SyntaxKind.LineKeyword)
			{
				throw new ArgumentException("lineKeyword");
			}
			SyntaxKind syntaxKind = line.Kind();
			if (syntaxKind != SyntaxKind.DefaultKeyword && syntaxKind != SyntaxKind.HiddenKeyword && syntaxKind != SyntaxKind.NumericLiteralToken)
			{
				throw new ArgumentException("line");
			}
			syntaxKind = file.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind != SyntaxKind.StringLiteralToken)
			{
				throw new ArgumentException("file");
			}
			if (endOfDirectiveToken.Kind() != SyntaxKind.EndOfDirectiveToken)
			{
				throw new ArgumentException("endOfDirectiveToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.LineDirectiveTriviaSyntax)SyntaxFactory.LineDirectiveTrivia((SyntaxToken)hashToken.Node, (SyntaxToken)lineKeyword.Node, (SyntaxToken)line.Node, (SyntaxToken)file.Node, (SyntaxToken)endOfDirectiveToken.Node, isActive).CreateRed();
		}

		// Token: 0x06002B7B RID: 11131 RVA: 0x000E9092 File Offset: 0x000E7292
		public static Microsoft.CodeAnalysis.CSharp.Syntax.LineDirectiveTriviaSyntax LineDirectiveTrivia(SyntaxToken line, SyntaxToken file, bool isActive)
		{
			return SyntaxFactory.LineDirectiveTrivia(SyntaxFactory.Token(SyntaxKind.HashToken), SyntaxFactory.Token(SyntaxKind.LineKeyword), line, file, SyntaxFactory.Token(SyntaxKind.EndOfDirectiveToken), isActive);
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x000E90BC File Offset: 0x000E72BC
		public static Microsoft.CodeAnalysis.CSharp.Syntax.LineDirectiveTriviaSyntax LineDirectiveTrivia(SyntaxToken line, bool isActive)
		{
			return SyntaxFactory.LineDirectiveTrivia(SyntaxFactory.Token(SyntaxKind.HashToken), SyntaxFactory.Token(SyntaxKind.LineKeyword), line, default(SyntaxToken), SyntaxFactory.Token(SyntaxKind.EndOfDirectiveToken), isActive);
		}

		// Token: 0x06002B7D RID: 11133 RVA: 0x000E90F8 File Offset: 0x000E72F8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.PragmaWarningDirectiveTriviaSyntax PragmaWarningDirectiveTrivia(SyntaxToken hashToken, SyntaxToken pragmaKeyword, SyntaxToken warningKeyword, SyntaxToken disableOrRestoreKeyword, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax> errorCodes, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (hashToken.Kind() != SyntaxKind.HashToken)
			{
				throw new ArgumentException("hashToken");
			}
			if (pragmaKeyword.Kind() != SyntaxKind.PragmaKeyword)
			{
				throw new ArgumentException("pragmaKeyword");
			}
			if (warningKeyword.Kind() != SyntaxKind.WarningKeyword)
			{
				throw new ArgumentException("warningKeyword");
			}
			SyntaxKind syntaxKind = disableOrRestoreKeyword.Kind();
			if (syntaxKind - SyntaxKind.DisableKeyword > 1)
			{
				throw new ArgumentException("disableOrRestoreKeyword");
			}
			if (endOfDirectiveToken.Kind() != SyntaxKind.EndOfDirectiveToken)
			{
				throw new ArgumentException("endOfDirectiveToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.PragmaWarningDirectiveTriviaSyntax)SyntaxFactory.PragmaWarningDirectiveTrivia((SyntaxToken)hashToken.Node, (SyntaxToken)pragmaKeyword.Node, (SyntaxToken)warningKeyword.Node, (SyntaxToken)disableOrRestoreKeyword.Node, errorCodes.Node.ToGreenSeparatedList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ExpressionSyntax>(), (SyntaxToken)endOfDirectiveToken.Node, isActive).CreateRed();
		}

		// Token: 0x06002B7E RID: 11134 RVA: 0x000E91DB File Offset: 0x000E73DB
		public static Microsoft.CodeAnalysis.CSharp.Syntax.PragmaWarningDirectiveTriviaSyntax PragmaWarningDirectiveTrivia(SyntaxToken disableOrRestoreKeyword, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax> errorCodes, bool isActive)
		{
			return SyntaxFactory.PragmaWarningDirectiveTrivia(SyntaxFactory.Token(SyntaxKind.HashToken), SyntaxFactory.Token(SyntaxKind.PragmaKeyword), SyntaxFactory.Token(SyntaxKind.WarningKeyword), disableOrRestoreKeyword, errorCodes, SyntaxFactory.Token(SyntaxKind.EndOfDirectiveToken), isActive);
		}

		// Token: 0x06002B7F RID: 11135 RVA: 0x000E9210 File Offset: 0x000E7410
		public static Microsoft.CodeAnalysis.CSharp.Syntax.PragmaWarningDirectiveTriviaSyntax PragmaWarningDirectiveTrivia(SyntaxToken disableOrRestoreKeyword, bool isActive)
		{
			return SyntaxFactory.PragmaWarningDirectiveTrivia(SyntaxFactory.Token(SyntaxKind.HashToken), SyntaxFactory.Token(SyntaxKind.PragmaKeyword), SyntaxFactory.Token(SyntaxKind.WarningKeyword), disableOrRestoreKeyword, default(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax>), SyntaxFactory.Token(SyntaxKind.EndOfDirectiveToken), isActive);
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x000E9258 File Offset: 0x000E7458
		public static Microsoft.CodeAnalysis.CSharp.Syntax.PragmaChecksumDirectiveTriviaSyntax PragmaChecksumDirectiveTrivia(SyntaxToken hashToken, SyntaxToken pragmaKeyword, SyntaxToken checksumKeyword, SyntaxToken file, SyntaxToken guid, SyntaxToken bytes, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (hashToken.Kind() != SyntaxKind.HashToken)
			{
				throw new ArgumentException("hashToken");
			}
			if (pragmaKeyword.Kind() != SyntaxKind.PragmaKeyword)
			{
				throw new ArgumentException("pragmaKeyword");
			}
			if (checksumKeyword.Kind() != SyntaxKind.ChecksumKeyword)
			{
				throw new ArgumentException("checksumKeyword");
			}
			if (file.Kind() != SyntaxKind.StringLiteralToken)
			{
				throw new ArgumentException("file");
			}
			if (guid.Kind() != SyntaxKind.StringLiteralToken)
			{
				throw new ArgumentException("guid");
			}
			if (bytes.Kind() != SyntaxKind.StringLiteralToken)
			{
				throw new ArgumentException("bytes");
			}
			if (endOfDirectiveToken.Kind() != SyntaxKind.EndOfDirectiveToken)
			{
				throw new ArgumentException("endOfDirectiveToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.PragmaChecksumDirectiveTriviaSyntax)SyntaxFactory.PragmaChecksumDirectiveTrivia((SyntaxToken)hashToken.Node, (SyntaxToken)pragmaKeyword.Node, (SyntaxToken)checksumKeyword.Node, (SyntaxToken)file.Node, (SyntaxToken)guid.Node, (SyntaxToken)bytes.Node, (SyntaxToken)endOfDirectiveToken.Node, isActive).CreateRed();
		}

		// Token: 0x06002B81 RID: 11137 RVA: 0x000E9375 File Offset: 0x000E7575
		public static Microsoft.CodeAnalysis.CSharp.Syntax.PragmaChecksumDirectiveTriviaSyntax PragmaChecksumDirectiveTrivia(SyntaxToken file, SyntaxToken guid, SyntaxToken bytes, bool isActive)
		{
			return SyntaxFactory.PragmaChecksumDirectiveTrivia(SyntaxFactory.Token(SyntaxKind.HashToken), SyntaxFactory.Token(SyntaxKind.PragmaKeyword), SyntaxFactory.Token(SyntaxKind.ChecksumKeyword), file, guid, bytes, SyntaxFactory.Token(SyntaxKind.EndOfDirectiveToken), isActive);
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x000E93A8 File Offset: 0x000E75A8
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ReferenceDirectiveTriviaSyntax ReferenceDirectiveTrivia(SyntaxToken hashToken, SyntaxToken referenceKeyword, SyntaxToken file, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (hashToken.Kind() != SyntaxKind.HashToken)
			{
				throw new ArgumentException("hashToken");
			}
			if (referenceKeyword.Kind() != SyntaxKind.ReferenceKeyword)
			{
				throw new ArgumentException("referenceKeyword");
			}
			if (file.Kind() != SyntaxKind.StringLiteralToken)
			{
				throw new ArgumentException("file");
			}
			if (endOfDirectiveToken.Kind() != SyntaxKind.EndOfDirectiveToken)
			{
				throw new ArgumentException("endOfDirectiveToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ReferenceDirectiveTriviaSyntax)SyntaxFactory.ReferenceDirectiveTrivia((SyntaxToken)hashToken.Node, (SyntaxToken)referenceKeyword.Node, (SyntaxToken)file.Node, (SyntaxToken)endOfDirectiveToken.Node, isActive).CreateRed();
		}

		// Token: 0x06002B83 RID: 11139 RVA: 0x000E9456 File Offset: 0x000E7656
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ReferenceDirectiveTriviaSyntax ReferenceDirectiveTrivia(SyntaxToken file, bool isActive)
		{
			return SyntaxFactory.ReferenceDirectiveTrivia(SyntaxFactory.Token(SyntaxKind.HashToken), SyntaxFactory.Token(SyntaxKind.ReferenceKeyword), file, SyntaxFactory.Token(SyntaxKind.EndOfDirectiveToken), isActive);
		}

		// Token: 0x06002B84 RID: 11140 RVA: 0x000E9480 File Offset: 0x000E7680
		public static Microsoft.CodeAnalysis.CSharp.Syntax.LoadDirectiveTriviaSyntax LoadDirectiveTrivia(SyntaxToken hashToken, SyntaxToken loadKeyword, SyntaxToken file, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (hashToken.Kind() != SyntaxKind.HashToken)
			{
				throw new ArgumentException("hashToken");
			}
			if (loadKeyword.Kind() != SyntaxKind.LoadKeyword)
			{
				throw new ArgumentException("loadKeyword");
			}
			if (file.Kind() != SyntaxKind.StringLiteralToken)
			{
				throw new ArgumentException("file");
			}
			if (endOfDirectiveToken.Kind() != SyntaxKind.EndOfDirectiveToken)
			{
				throw new ArgumentException("endOfDirectiveToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.LoadDirectiveTriviaSyntax)SyntaxFactory.LoadDirectiveTrivia((SyntaxToken)hashToken.Node, (SyntaxToken)loadKeyword.Node, (SyntaxToken)file.Node, (SyntaxToken)endOfDirectiveToken.Node, isActive).CreateRed();
		}

		// Token: 0x06002B85 RID: 11141 RVA: 0x000E952E File Offset: 0x000E772E
		public static Microsoft.CodeAnalysis.CSharp.Syntax.LoadDirectiveTriviaSyntax LoadDirectiveTrivia(SyntaxToken file, bool isActive)
		{
			return SyntaxFactory.LoadDirectiveTrivia(SyntaxFactory.Token(SyntaxKind.HashToken), SyntaxFactory.Token(SyntaxKind.LoadKeyword), file, SyntaxFactory.Token(SyntaxKind.EndOfDirectiveToken), isActive);
		}

		// Token: 0x06002B86 RID: 11142 RVA: 0x000E9558 File Offset: 0x000E7758
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ShebangDirectiveTriviaSyntax ShebangDirectiveTrivia(SyntaxToken hashToken, SyntaxToken exclamationToken, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (hashToken.Kind() != SyntaxKind.HashToken)
			{
				throw new ArgumentException("hashToken");
			}
			if (exclamationToken.Kind() != SyntaxKind.ExclamationToken)
			{
				throw new ArgumentException("exclamationToken");
			}
			if (endOfDirectiveToken.Kind() != SyntaxKind.EndOfDirectiveToken)
			{
				throw new ArgumentException("endOfDirectiveToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.ShebangDirectiveTriviaSyntax)SyntaxFactory.ShebangDirectiveTrivia((SyntaxToken)hashToken.Node, (SyntaxToken)exclamationToken.Node, (SyntaxToken)endOfDirectiveToken.Node, isActive).CreateRed();
		}

		// Token: 0x06002B87 RID: 11143 RVA: 0x000E95E1 File Offset: 0x000E77E1
		public static Microsoft.CodeAnalysis.CSharp.Syntax.ShebangDirectiveTriviaSyntax ShebangDirectiveTrivia(bool isActive)
		{
			return SyntaxFactory.ShebangDirectiveTrivia(SyntaxFactory.Token(SyntaxKind.HashToken), SyntaxFactory.Token(SyntaxKind.ExclamationToken), SyntaxFactory.Token(SyntaxKind.EndOfDirectiveToken), isActive);
		}

		// Token: 0x06002B88 RID: 11144 RVA: 0x000E9608 File Offset: 0x000E7808
		public static Microsoft.CodeAnalysis.CSharp.Syntax.NullableDirectiveTriviaSyntax NullableDirectiveTrivia(SyntaxToken hashToken, SyntaxToken nullableKeyword, SyntaxToken settingToken, SyntaxToken targetToken, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (hashToken.Kind() != SyntaxKind.HashToken)
			{
				throw new ArgumentException("hashToken");
			}
			if (nullableKeyword.Kind() != SyntaxKind.NullableKeyword)
			{
				throw new ArgumentException("nullableKeyword");
			}
			SyntaxKind syntaxKind = settingToken.Kind();
			if (syntaxKind - SyntaxKind.DisableKeyword > 1 && syntaxKind != SyntaxKind.EnableKeyword)
			{
				throw new ArgumentException("settingToken");
			}
			syntaxKind = targetToken.Kind();
			if (syntaxKind != SyntaxKind.None && syntaxKind - SyntaxKind.WarningsKeyword > 1)
			{
				throw new ArgumentException("targetToken");
			}
			if (endOfDirectiveToken.Kind() != SyntaxKind.EndOfDirectiveToken)
			{
				throw new ArgumentException("endOfDirectiveToken");
			}
			return (Microsoft.CodeAnalysis.CSharp.Syntax.NullableDirectiveTriviaSyntax)SyntaxFactory.NullableDirectiveTrivia((SyntaxToken)hashToken.Node, (SyntaxToken)nullableKeyword.Node, (SyntaxToken)settingToken.Node, (SyntaxToken)targetToken.Node, (SyntaxToken)endOfDirectiveToken.Node, isActive).CreateRed();
		}

		// Token: 0x06002B89 RID: 11145 RVA: 0x000E96EE File Offset: 0x000E78EE
		public static Microsoft.CodeAnalysis.CSharp.Syntax.NullableDirectiveTriviaSyntax NullableDirectiveTrivia(SyntaxToken settingToken, SyntaxToken targetToken, bool isActive)
		{
			return SyntaxFactory.NullableDirectiveTrivia(SyntaxFactory.Token(SyntaxKind.HashToken), SyntaxFactory.Token(SyntaxKind.NullableKeyword), settingToken, targetToken, SyntaxFactory.Token(SyntaxKind.EndOfDirectiveToken), isActive);
		}

		// Token: 0x06002B8A RID: 11146 RVA: 0x000E9718 File Offset: 0x000E7918
		public static Microsoft.CodeAnalysis.CSharp.Syntax.NullableDirectiveTriviaSyntax NullableDirectiveTrivia(SyntaxToken settingToken, bool isActive)
		{
			return SyntaxFactory.NullableDirectiveTrivia(SyntaxFactory.Token(SyntaxKind.HashToken), SyntaxFactory.Token(SyntaxKind.NullableKeyword), settingToken, default(SyntaxToken), SyntaxFactory.Token(SyntaxKind.EndOfDirectiveToken), isActive);
		}
	}
}
