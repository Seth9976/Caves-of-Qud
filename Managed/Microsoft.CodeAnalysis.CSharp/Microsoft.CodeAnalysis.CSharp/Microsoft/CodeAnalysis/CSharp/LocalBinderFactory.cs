using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000030 RID: 48
	internal sealed class LocalBinderFactory : CSharpSyntaxWalker
	{
		// Token: 0x06000627 RID: 1575 RVA: 0x000396A0 File Offset: 0x000378A0
		private void Visit(CSharpSyntaxNode syntax, Binder enclosing)
		{
			if (this._enclosing == enclosing)
			{
				this.Visit(syntax);
				return;
			}
			Binder enclosing2 = this._enclosing;
			this._enclosing = enclosing;
			this.Visit(syntax);
			this._enclosing = enclosing2;
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x000396DC File Offset: 0x000378DC
		private void VisitRankSpecifiers(TypeSyntax type, Binder enclosing)
		{
			Action<ArrayRankSpecifierSyntax, ValueTuple<LocalBinderFactory, Binder>> action = delegate([global::System.Runtime.CompilerServices.Nullable(1)] ArrayRankSpecifierSyntax rankSpecifier, [TupleElementNames(new string[] { "localBinderFactory", "binder" })] ValueTuple<LocalBinderFactory, Binder> args)
			{
				foreach (ExpressionSyntax expressionSyntax in rankSpecifier.Sizes)
				{
					if (expressionSyntax.Kind() != SyntaxKind.OmittedArraySizeExpression)
					{
						args.Item1.Visit(expressionSyntax, args.Item2);
					}
				}
			};
			ValueTuple<LocalBinderFactory, Binder> valueTuple = new ValueTuple<LocalBinderFactory, Binder>(this, enclosing);
			type.VisitRankSpecifiers<ValueTuple<LocalBinderFactory, Binder>>(action, in valueTuple);
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00039718 File Offset: 0x00037918
		public static SmallDictionary<SyntaxNode, Binder> BuildMap(Symbol containingMemberOrLambda, SyntaxNode syntax, Binder enclosing, Action<Binder, SyntaxNode> binderUpdatedHandler = null)
		{
			LocalBinderFactory localBinderFactory = new LocalBinderFactory(containingMemberOrLambda, syntax, enclosing);
			ExpressionSyntax expressionSyntax = syntax as ExpressionSyntax;
			StatementSyntax statementSyntax;
			if (expressionSyntax != null)
			{
				enclosing = new ExpressionVariableBinder(syntax, enclosing);
				if (binderUpdatedHandler != null)
				{
					binderUpdatedHandler.Invoke(enclosing, syntax);
				}
				localBinderFactory.AddToMap(syntax, enclosing);
				localBinderFactory.Visit(expressionSyntax, enclosing);
			}
			else if (syntax.Kind() != SyntaxKind.Block && (statementSyntax = syntax as StatementSyntax) != null)
			{
				CSharpSyntaxNode csharpSyntaxNode;
				enclosing = localBinderFactory.GetBinderForPossibleEmbeddedStatement(statementSyntax, enclosing, out csharpSyntaxNode);
				if (binderUpdatedHandler != null)
				{
					binderUpdatedHandler.Invoke(enclosing, csharpSyntaxNode);
				}
				if (csharpSyntaxNode != null)
				{
					localBinderFactory.AddToMap(csharpSyntaxNode, enclosing);
				}
				localBinderFactory.Visit(statementSyntax, enclosing);
			}
			else
			{
				if (binderUpdatedHandler != null)
				{
					binderUpdatedHandler.Invoke(enclosing, null);
				}
				localBinderFactory.Visit((CSharpSyntaxNode)syntax, enclosing);
			}
			return localBinderFactory._map;
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x000397C0 File Offset: 0x000379C0
		public override void VisitCompilationUnit(CompilationUnitSyntax node)
		{
			foreach (MemberDeclarationSyntax memberDeclarationSyntax in node.Members)
			{
				if (memberDeclarationSyntax.Kind() == SyntaxKind.GlobalStatement)
				{
					this.Visit(memberDeclarationSyntax);
				}
			}
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00039803 File Offset: 0x00037A03
		private LocalBinderFactory(Symbol containingMemberOrLambda, SyntaxNode root, Binder enclosing)
			: base(SyntaxWalkerDepth.Node)
		{
			this._map = new SmallDictionary<SyntaxNode, Binder>(ReferenceEqualityComparer.Instance);
			this._containingMemberOrLambda = containingMemberOrLambda;
			this._enclosing = enclosing;
			this._root = root;
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00039831 File Offset: 0x00037A31
		public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
		{
			this.Visit(node.Body);
			this.Visit(node.ExpressionBody);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0003984C File Offset: 0x00037A4C
		public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
		{
			Binder binder = new ExpressionVariableBinder(node, this._enclosing);
			this.AddToMap(node, binder);
			this.Visit(node.Initializer, binder);
			this.Visit(node.Body, binder);
			this.Visit(node.ExpressionBody, binder);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00039898 File Offset: 0x00037A98
		public override void VisitRecordDeclaration(RecordDeclarationSyntax node)
		{
			Binder binder = new ExpressionVariableBinder(node, this._enclosing);
			this.AddToMap(node, binder);
			this.Visit(node.PrimaryConstructorBaseTypeIfClass, binder);
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x000398C8 File Offset: 0x00037AC8
		public override void VisitPrimaryConstructorBaseType(PrimaryConstructorBaseTypeSyntax node)
		{
			Binder binder = this._enclosing.WithAdditionalFlags(BinderFlags.ConstructorInitializer);
			this.AddToMap(node, binder);
			this.VisitConstructorInitializerArgumentList(node, node.ArgumentList, binder);
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x000398F8 File Offset: 0x00037AF8
		public override void VisitDestructorDeclaration(DestructorDeclarationSyntax node)
		{
			this.Visit(node.Body);
			this.Visit(node.ExpressionBody);
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00039912 File Offset: 0x00037B12
		public override void VisitAccessorDeclaration(AccessorDeclarationSyntax node)
		{
			this.Visit(node.Body);
			this.Visit(node.ExpressionBody);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0003992C File Offset: 0x00037B2C
		public override void VisitConversionOperatorDeclaration(ConversionOperatorDeclarationSyntax node)
		{
			this.Visit(node.Body);
			this.Visit(node.ExpressionBody);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00039946 File Offset: 0x00037B46
		public override void VisitOperatorDeclaration(OperatorDeclarationSyntax node)
		{
			this.Visit(node.Body);
			this.Visit(node.ExpressionBody);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00039960 File Offset: 0x00037B60
		public override void VisitSimpleLambdaExpression(SimpleLambdaExpressionSyntax node)
		{
			this.VisitLambdaExpression(node);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0003996C File Offset: 0x00037B6C
		private void VisitLambdaExpression(LambdaExpressionSyntax node)
		{
			if (this._root != node)
			{
				return;
			}
			CSharpSyntaxNode body = node.Body;
			if (body.Kind() == SyntaxKind.Block)
			{
				this.VisitBlock((BlockSyntax)body);
				return;
			}
			ExpressionVariableBinder expressionVariableBinder = new ExpressionVariableBinder(body, this._enclosing);
			this.AddToMap(body, expressionVariableBinder);
			this.Visit(body, expressionVariableBinder);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x000399C1 File Offset: 0x00037BC1
		public override void VisitParenthesizedLambdaExpression(ParenthesizedLambdaExpressionSyntax node)
		{
			this.VisitLambdaExpression(node);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x000399CC File Offset: 0x00037BCC
		public override void VisitLocalFunctionStatement(LocalFunctionStatementSyntax node)
		{
			Symbol containingMemberOrLambda = this._containingMemberOrLambda;
			Binder binder = this._enclosing;
			LocalFunctionSymbol localFunctionSymbol = LocalBinderFactory.FindLocalFunction(node, this._enclosing);
			if (localFunctionSymbol != null)
			{
				this._containingMemberOrLambda = localFunctionSymbol;
				binder = (localFunctionSymbol.IsGenericMethod ? new WithMethodTypeParametersBinder(localFunctionSymbol, this._enclosing) : this._enclosing);
				binder = binder.WithUnsafeRegionIfNecessary(node.Modifiers);
				binder = new InMethodBinder(localFunctionSymbol, binder);
			}
			BlockSyntax body = node.Body;
			if (body != null)
			{
				this.Visit(body, binder);
			}
			ArrowExpressionClauseSyntax expressionBody = node.ExpressionBody;
			if (expressionBody != null)
			{
				this.Visit(expressionBody, binder);
			}
			this._containingMemberOrLambda = containingMemberOrLambda;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00039A60 File Offset: 0x00037C60
		private static LocalFunctionSymbol FindLocalFunction(LocalFunctionStatementSyntax node, Binder enclosing)
		{
			LocalFunctionSymbol localFunctionSymbol = null;
			Binder binder = enclosing;
			while (binder != null && !binder.IsLocalFunctionsScopeBinder)
			{
				binder = binder.Next;
			}
			if (binder != null)
			{
				foreach (LocalFunctionSymbol localFunctionSymbol2 in binder.LocalFunctions)
				{
					if (localFunctionSymbol2.Locations[0] == node.Identifier.GetLocation())
					{
						localFunctionSymbol = localFunctionSymbol2;
					}
				}
			}
			return localFunctionSymbol;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00039AD8 File Offset: 0x00037CD8
		public override void VisitArrowExpressionClause(ArrowExpressionClauseSyntax node)
		{
			ExpressionVariableBinder expressionVariableBinder = new ExpressionVariableBinder(node, this._enclosing);
			this.AddToMap(node, expressionVariableBinder);
			this.Visit(node.Expression, expressionVariableBinder);
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00039B08 File Offset: 0x00037D08
		public override void VisitEqualsValueClause(EqualsValueClauseSyntax node)
		{
			ExpressionVariableBinder expressionVariableBinder = new ExpressionVariableBinder(node, this._enclosing);
			this.AddToMap(node, expressionVariableBinder);
			this.Visit(node.Value, expressionVariableBinder);
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00039B38 File Offset: 0x00037D38
		public override void VisitAttribute(AttributeSyntax node)
		{
			ExpressionVariableBinder expressionVariableBinder = new ExpressionVariableBinder(node, this._enclosing);
			this.AddToMap(node, expressionVariableBinder);
			AttributeArgumentListSyntax argumentList = node.ArgumentList;
			if (argumentList != null && argumentList.Arguments.Count > 0)
			{
				foreach (AttributeArgumentSyntax attributeArgumentSyntax in node.ArgumentList.Arguments)
				{
					this.Visit(attributeArgumentSyntax.Expression, expressionVariableBinder);
				}
			}
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00039BB0 File Offset: 0x00037DB0
		public override void VisitConstructorInitializer(ConstructorInitializerSyntax node)
		{
			Binder binder = this._enclosing.WithAdditionalFlags(BinderFlags.ConstructorInitializer);
			this.AddToMap(node, binder);
			this.VisitConstructorInitializerArgumentList(node, node.ArgumentList, binder);
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00039BE0 File Offset: 0x00037DE0
		private void VisitConstructorInitializerArgumentList(CSharpSyntaxNode node, ArgumentListSyntax argumentList, Binder binder)
		{
			if (argumentList != null)
			{
				if (this._root == node)
				{
					binder = new ExpressionVariableBinder(argumentList, binder);
					this.AddToMap(argumentList, binder);
				}
				this.Visit(argumentList, binder);
			}
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00039C07 File Offset: 0x00037E07
		public override void VisitAnonymousMethodExpression(AnonymousMethodExpressionSyntax node)
		{
			if (this._root != node)
			{
				return;
			}
			this.VisitBlock(node.Block);
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00039C1F File Offset: 0x00037E1F
		public override void VisitGlobalStatement(GlobalStatementSyntax node)
		{
			this.Visit(node.Statement);
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00039C30 File Offset: 0x00037E30
		public override void VisitBlock(BlockSyntax node)
		{
			BlockBinder blockBinder = new BlockBinder(this._enclosing, node);
			this.AddToMap(node, blockBinder);
			foreach (StatementSyntax statementSyntax in node.Statements)
			{
				this.Visit(statementSyntax, blockBinder);
			}
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00039C7C File Offset: 0x00037E7C
		public override void VisitUsingStatement(UsingStatementSyntax node)
		{
			UsingStatementBinder usingStatementBinder = new UsingStatementBinder(this._enclosing, node);
			this.AddToMap(node, usingStatementBinder);
			ExpressionSyntax expression = node.Expression;
			VariableDeclarationSyntax declaration = node.Declaration;
			if (expression != null)
			{
				this.Visit(expression, usingStatementBinder);
			}
			else
			{
				this.VisitRankSpecifiers(declaration.Type, usingStatementBinder);
				foreach (VariableDeclaratorSyntax variableDeclaratorSyntax in declaration.Variables)
				{
					this.Visit(variableDeclaratorSyntax, usingStatementBinder);
				}
			}
			this.VisitPossibleEmbeddedStatement(node.Statement, usingStatementBinder);
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00039D00 File Offset: 0x00037F00
		public override void VisitWhileStatement(WhileStatementSyntax node)
		{
			WhileBinder whileBinder = new WhileBinder(this._enclosing, node);
			this.AddToMap(node, whileBinder);
			this.Visit(node.Condition, whileBinder);
			this.VisitPossibleEmbeddedStatement(node.Statement, whileBinder);
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00039D3C File Offset: 0x00037F3C
		public override void VisitDoStatement(DoStatementSyntax node)
		{
			WhileBinder whileBinder = new WhileBinder(this._enclosing, node);
			this.AddToMap(node, whileBinder);
			this.Visit(node.Condition, whileBinder);
			this.VisitPossibleEmbeddedStatement(node.Statement, whileBinder);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00039D78 File Offset: 0x00037F78
		public override void VisitForStatement(ForStatementSyntax node)
		{
			Binder binder = new ForLoopBinder(this._enclosing, node);
			this.AddToMap(node, binder);
			VariableDeclarationSyntax declaration = node.Declaration;
			if (declaration != null)
			{
				this.VisitRankSpecifiers(declaration.Type, binder);
				foreach (VariableDeclaratorSyntax variableDeclaratorSyntax in declaration.Variables)
				{
					this.Visit(variableDeclaratorSyntax, binder);
				}
			}
			else
			{
				foreach (ExpressionSyntax expressionSyntax in node.Initializers)
				{
					this.Visit(expressionSyntax, binder);
				}
			}
			ExpressionSyntax condition = node.Condition;
			if (condition != null)
			{
				binder = new ExpressionVariableBinder(condition, binder);
				this.AddToMap(condition, binder);
				this.Visit(condition, binder);
			}
			SeparatedSyntaxList<ExpressionSyntax> incrementors = node.Incrementors;
			if (incrementors.Count > 0)
			{
				ExpressionListVariableBinder expressionListVariableBinder = new ExpressionListVariableBinder(incrementors, binder);
				this.AddToMap(incrementors.First(), expressionListVariableBinder);
				foreach (ExpressionSyntax expressionSyntax2 in incrementors)
				{
					this.Visit(expressionSyntax2, expressionListVariableBinder);
				}
			}
			this.VisitPossibleEmbeddedStatement(node.Statement, binder);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00039E90 File Offset: 0x00038090
		private void VisitCommonForEachStatement(CommonForEachStatementSyntax node)
		{
			ExpressionVariableBinder expressionVariableBinder = new ExpressionVariableBinder(node.Expression, this._enclosing);
			this.AddToMap(node.Expression, expressionVariableBinder);
			this.Visit(node.Expression, expressionVariableBinder);
			ForEachLoopBinder forEachLoopBinder = new ForEachLoopBinder(expressionVariableBinder, node);
			this.AddToMap(node, forEachLoopBinder);
			this.VisitPossibleEmbeddedStatement(node.Statement, forEachLoopBinder);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00039EE6 File Offset: 0x000380E6
		public override void VisitForEachStatement(ForEachStatementSyntax node)
		{
			this.VisitCommonForEachStatement(node);
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00039EEF File Offset: 0x000380EF
		public override void VisitForEachVariableStatement(ForEachVariableStatementSyntax node)
		{
			this.VisitCommonForEachStatement(node);
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00039EF8 File Offset: 0x000380F8
		public override void VisitCheckedStatement(CheckedStatementSyntax node)
		{
			Binder binder = this._enclosing.WithCheckedOrUncheckedRegion(node.Kind() == SyntaxKind.CheckedStatement);
			this.AddToMap(node, binder);
			this.Visit(node.Block, binder);
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00039F34 File Offset: 0x00038134
		public override void VisitUnsafeStatement(UnsafeStatementSyntax node)
		{
			Binder binder = this._enclosing.WithAdditionalFlags(BinderFlags.UnsafeRegion);
			this.AddToMap(node, binder);
			this.Visit(node.Block, binder);
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00039F68 File Offset: 0x00038168
		public override void VisitFixedStatement(FixedStatementSyntax node)
		{
			FixedStatementBinder fixedStatementBinder = new FixedStatementBinder(this._enclosing, node);
			this.AddToMap(node, fixedStatementBinder);
			if (node.Declaration != null)
			{
				this.VisitRankSpecifiers(node.Declaration.Type, fixedStatementBinder);
				foreach (VariableDeclaratorSyntax variableDeclaratorSyntax in node.Declaration.Variables)
				{
					this.Visit(variableDeclaratorSyntax, fixedStatementBinder);
				}
			}
			this.VisitPossibleEmbeddedStatement(node.Statement, fixedStatementBinder);
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00039FE0 File Offset: 0x000381E0
		public override void VisitLockStatement(LockStatementSyntax node)
		{
			LockBinder lockBinder = new LockBinder(this._enclosing, node);
			this.AddToMap(node, lockBinder);
			this.Visit(node.Expression, lockBinder);
			StatementSyntax statement = node.Statement;
			Binder binder = lockBinder.WithAdditionalFlags(BinderFlags.InLockBody);
			if (binder != lockBinder)
			{
				this.AddToMap(statement, binder);
			}
			this.VisitPossibleEmbeddedStatement(statement, binder);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0003A038 File Offset: 0x00038238
		public override void VisitSwitchStatement(SwitchStatementSyntax node)
		{
			this.AddToMap(node.Expression, this._enclosing);
			this.Visit(node.Expression, this._enclosing);
			SwitchBinder switchBinder = SwitchBinder.Create(this._enclosing, node);
			this.AddToMap(node, switchBinder);
			foreach (SwitchSectionSyntax switchSectionSyntax in node.Sections)
			{
				this.Visit(switchSectionSyntax, switchBinder);
			}
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0003A0A8 File Offset: 0x000382A8
		public override void VisitSwitchSection(SwitchSectionSyntax node)
		{
			ExpressionVariableBinder expressionVariableBinder = new ExpressionVariableBinder(node, this._enclosing);
			this.AddToMap(node, expressionVariableBinder);
			foreach (SwitchLabelSyntax switchLabelSyntax in node.Labels)
			{
				SyntaxKind syntaxKind = switchLabelSyntax.Kind();
				if (syntaxKind != SyntaxKind.CaseSwitchLabel)
				{
					if (syntaxKind == SyntaxKind.CasePatternSwitchLabel)
					{
						CasePatternSwitchLabelSyntax casePatternSwitchLabelSyntax = (CasePatternSwitchLabelSyntax)switchLabelSyntax;
						this.Visit(casePatternSwitchLabelSyntax.Pattern, expressionVariableBinder);
						if (casePatternSwitchLabelSyntax.WhenClause != null)
						{
							this.Visit(casePatternSwitchLabelSyntax.WhenClause.Condition, expressionVariableBinder);
						}
					}
				}
				else
				{
					CaseSwitchLabelSyntax caseSwitchLabelSyntax = (CaseSwitchLabelSyntax)switchLabelSyntax;
					this.Visit(caseSwitchLabelSyntax.Value, expressionVariableBinder);
				}
			}
			foreach (StatementSyntax statementSyntax in node.Statements)
			{
				this.Visit(statementSyntax, expressionVariableBinder);
			}
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0003A180 File Offset: 0x00038380
		public override void VisitSwitchExpression(SwitchExpressionSyntax node)
		{
			SwitchExpressionBinder switchExpressionBinder = new SwitchExpressionBinder(node, this._enclosing);
			this.AddToMap(node, switchExpressionBinder);
			this.Visit(node.GoverningExpression, switchExpressionBinder);
			foreach (SwitchExpressionArmSyntax switchExpressionArmSyntax in node.Arms)
			{
				ExpressionVariableBinder expressionVariableBinder = new ExpressionVariableBinder(switchExpressionArmSyntax, switchExpressionBinder);
				SwitchExpressionArmBinder switchExpressionArmBinder = new SwitchExpressionArmBinder(switchExpressionArmSyntax, expressionVariableBinder, switchExpressionBinder);
				this.AddToMap(switchExpressionArmSyntax, switchExpressionArmBinder);
				this.Visit(switchExpressionArmSyntax.Pattern, switchExpressionArmBinder);
				if (switchExpressionArmSyntax.WhenClause != null)
				{
					this.Visit(switchExpressionArmSyntax.WhenClause, switchExpressionArmBinder);
				}
				this.Visit(switchExpressionArmSyntax.Expression, switchExpressionArmBinder);
			}
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0003A220 File Offset: 0x00038420
		public override void VisitIfStatement(IfStatementSyntax node)
		{
			this.Visit(node.Condition, this._enclosing);
			this.VisitPossibleEmbeddedStatement(node.Statement, this._enclosing);
			this.Visit(node.Else, this._enclosing);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0003A258 File Offset: 0x00038458
		public override void VisitElseClause(ElseClauseSyntax node)
		{
			this.VisitPossibleEmbeddedStatement(node.Statement, this._enclosing);
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0003A26C File Offset: 0x0003846C
		public override void VisitLabeledStatement(LabeledStatementSyntax node)
		{
			this.Visit(node.Statement, this._enclosing);
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0003A280 File Offset: 0x00038480
		public override void VisitTryStatement(TryStatementSyntax node)
		{
			if (node.Catches.Any())
			{
				this.Visit(node.Block, this._enclosing.WithAdditionalFlags(BinderFlags.InTryBlockOfTryCatch));
			}
			else
			{
				this.Visit(node.Block, this._enclosing);
			}
			foreach (CatchClauseSyntax catchClauseSyntax in node.Catches)
			{
				this.Visit(catchClauseSyntax, this._enclosing);
			}
			if (node.Finally != null)
			{
				this.Visit(node.Finally, this._enclosing);
			}
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0003A318 File Offset: 0x00038518
		public override void VisitCatchClause(CatchClauseSyntax node)
		{
			CatchClauseBinder catchClauseBinder = new CatchClauseBinder(this._enclosing, node);
			this.AddToMap(node, catchClauseBinder);
			if (node.Filter != null)
			{
				Binder binder = catchClauseBinder.WithAdditionalFlags(BinderFlags.InCatchFilter);
				this.AddToMap(node.Filter, binder);
				this.Visit(node.Filter, binder);
			}
			this.Visit(node.Block, catchClauseBinder);
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0003A375 File Offset: 0x00038575
		public override void VisitCatchFilterClause(CatchFilterClauseSyntax node)
		{
			this.Visit(node.FilterExpression);
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0003A384 File Offset: 0x00038584
		public override void VisitFinallyClause(FinallyClauseSyntax node)
		{
			BinderFlags binderFlags = BinderFlags.InFinallyBlock;
			if (this._enclosing.Flags.Includes(BinderFlags.InCatchBlock))
			{
				binderFlags |= BinderFlags.InNestedFinallyBlock;
			}
			this.Visit(node.Block, this._enclosing.WithAdditionalFlags(binderFlags));
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0003A3CE File Offset: 0x000385CE
		public override void VisitYieldStatement(YieldStatementSyntax node)
		{
			if (node.Expression != null)
			{
				this.Visit(node.Expression, this._enclosing);
			}
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0003A3EA File Offset: 0x000385EA
		public override void VisitExpressionStatement(ExpressionStatementSyntax node)
		{
			this.Visit(node.Expression, this._enclosing);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0003A400 File Offset: 0x00038600
		public override void VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
		{
			this.VisitRankSpecifiers(node.Declaration.Type, this._enclosing);
			foreach (VariableDeclaratorSyntax variableDeclaratorSyntax in node.Declaration.Variables)
			{
				this.Visit(variableDeclaratorSyntax);
			}
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0003A452 File Offset: 0x00038652
		public override void VisitVariableDeclarator(VariableDeclaratorSyntax node)
		{
			this.Visit(node.ArgumentList);
			EqualsValueClauseSyntax initializer = node.Initializer;
			this.Visit((initializer != null) ? initializer.Value : null);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0003A478 File Offset: 0x00038678
		public override void VisitReturnStatement(ReturnStatementSyntax node)
		{
			if (node.Expression != null)
			{
				this.Visit(node.Expression, this._enclosing);
			}
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0003A494 File Offset: 0x00038694
		public override void VisitThrowStatement(ThrowStatementSyntax node)
		{
			if (node.Expression != null)
			{
				this.Visit(node.Expression, this._enclosing);
			}
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0003A4B0 File Offset: 0x000386B0
		public override void VisitBinaryExpression(BinaryExpressionSyntax node)
		{
			for (;;)
			{
				this.Visit(node.Right);
				BinaryExpressionSyntax binaryExpressionSyntax = node.Left as BinaryExpressionSyntax;
				if (binaryExpressionSyntax == null)
				{
					break;
				}
				node = binaryExpressionSyntax;
			}
			this.Visit(node.Left);
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0003A4E9 File Offset: 0x000386E9
		public override void DefaultVisit(SyntaxNode node)
		{
			base.DefaultVisit(node);
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0003A4F2 File Offset: 0x000386F2
		private void AddToMap(SyntaxNode node, Binder binder)
		{
			this._map[node] = binder;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0003A504 File Offset: 0x00038704
		private Binder GetBinderForPossibleEmbeddedStatement(StatementSyntax statement, Binder enclosing, out CSharpSyntaxNode embeddedScopeDesignator)
		{
			SyntaxKind syntaxKind = statement.Kind();
			if (syntaxKind <= SyntaxKind.YieldReturnStatement)
			{
				if (syntaxKind <= SyntaxKind.ExpressionStatement)
				{
					if (syntaxKind != SyntaxKind.LocalDeclarationStatement && syntaxKind != SyntaxKind.ExpressionStatement)
					{
						goto IL_0090;
					}
				}
				else if (syntaxKind != SyntaxKind.LabeledStatement && syntaxKind - SyntaxKind.ReturnStatement > 1)
				{
					goto IL_0090;
				}
			}
			else if (syntaxKind <= SyntaxKind.IfStatement)
			{
				if (syntaxKind != SyntaxKind.ThrowStatement && syntaxKind - SyntaxKind.LockStatement > 1)
				{
					goto IL_0090;
				}
			}
			else
			{
				if (syntaxKind == SyntaxKind.SwitchStatement)
				{
					SwitchStatementSyntax switchStatementSyntax = (SwitchStatementSyntax)statement;
					embeddedScopeDesignator = switchStatementSyntax.Expression;
					return new ExpressionVariableBinder(switchStatementSyntax.Expression, enclosing);
				}
				if (syntaxKind != SyntaxKind.LocalFunctionStatement)
				{
					goto IL_0090;
				}
			}
			embeddedScopeDesignator = statement;
			return new EmbeddedStatementBinder(enclosing, statement);
			IL_0090:
			embeddedScopeDesignator = null;
			return enclosing;
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0003A5A8 File Offset: 0x000387A8
		private void VisitPossibleEmbeddedStatement(StatementSyntax statement, Binder enclosing)
		{
			if (statement != null)
			{
				CSharpSyntaxNode csharpSyntaxNode;
				enclosing = this.GetBinderForPossibleEmbeddedStatement(statement, enclosing, out csharpSyntaxNode);
				if (csharpSyntaxNode != null)
				{
					this.AddToMap(csharpSyntaxNode, enclosing);
				}
				this.Visit(statement, enclosing);
			}
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0003A5D7 File Offset: 0x000387D7
		public override void VisitQueryExpression(QueryExpressionSyntax node)
		{
			this.Visit(node.FromClause.Expression);
			this.Visit(node.Body);
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0003A5F8 File Offset: 0x000387F8
		public override void VisitQueryBody(QueryBodySyntax node)
		{
			foreach (QueryClauseSyntax queryClauseSyntax in node.Clauses)
			{
				if (queryClauseSyntax.Kind() == SyntaxKind.JoinClause)
				{
					this.Visit(((JoinClauseSyntax)queryClauseSyntax).InExpression);
				}
			}
			this.Visit(node.Continuation);
		}

		// Token: 0x04000089 RID: 137
		private readonly SmallDictionary<SyntaxNode, Binder> _map;

		// Token: 0x0400008A RID: 138
		private Symbol _containingMemberOrLambda;

		// Token: 0x0400008B RID: 139
		private Binder _enclosing;

		// Token: 0x0400008C RID: 140
		private readonly SyntaxNode _root;
	}
}
