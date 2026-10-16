using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200001F RID: 31
	internal abstract class ExpressionVariableFinder<TFieldOrLocalSymbol> : CSharpSyntaxWalker where TFieldOrLocalSymbol : Symbol
	{
		// Token: 0x06000570 RID: 1392 RVA: 0x00035760 File Offset: 0x00033960
		protected void FindExpressionVariables(ArrayBuilder<TFieldOrLocalSymbol> builder, CSharpSyntaxNode node)
		{
			ArrayBuilder<TFieldOrLocalSymbol> variablesBuilder = this._variablesBuilder;
			this._variablesBuilder = builder;
			this.VisitNodeToBind(node);
			this._variablesBuilder = variablesBuilder;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00035789 File Offset: 0x00033989
		public override void Visit(SyntaxNode node)
		{
			if (node != null)
			{
				((CSharpSyntaxNode)node).Accept(this);
			}
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0003579A File Offset: 0x0003399A
		public override void VisitSwitchExpression(SwitchExpressionSyntax node)
		{
			this.Visit(node.GoverningExpression);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x000357A8 File Offset: 0x000339A8
		public override void VisitSwitchExpressionArm(SwitchExpressionArmSyntax node)
		{
			SyntaxNode nodeToBind = this._nodeToBind;
			this._nodeToBind = node;
			this.Visit(node.Pattern);
			WhenClauseSyntax whenClause = node.WhenClause;
			this.Visit((whenClause != null) ? whenClause.Condition : null);
			this.Visit(node.Expression);
			this._nodeToBind = nodeToBind;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x000357FC File Offset: 0x000339FC
		public override void VisitVariableDeclarator(VariableDeclaratorSyntax node)
		{
			if (node.ArgumentList != null)
			{
				foreach (ArgumentSyntax argumentSyntax in node.ArgumentList.Arguments)
				{
					this.Visit(argumentSyntax.Expression);
				}
			}
			this.VisitNodeToBind(node.Initializer);
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00035850 File Offset: 0x00033A50
		public override void VisitGotoStatement(GotoStatementSyntax node)
		{
			if (node.Kind() == SyntaxKind.GotoCaseStatement)
			{
				this.Visit(node.Expression);
			}
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0003586C File Offset: 0x00033A6C
		private void VisitNodeToBind(CSharpSyntaxNode node)
		{
			SyntaxNode nodeToBind = this._nodeToBind;
			this._nodeToBind = node;
			this.Visit(node);
			this._nodeToBind = nodeToBind;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00035898 File Offset: 0x00033A98
		protected void FindExpressionVariables(ArrayBuilder<TFieldOrLocalSymbol> builder, SeparatedSyntaxList<ExpressionSyntax> nodes)
		{
			ArrayBuilder<TFieldOrLocalSymbol> variablesBuilder = this._variablesBuilder;
			this._variablesBuilder = builder;
			foreach (ExpressionSyntax expressionSyntax in nodes)
			{
				this.VisitNodeToBind(expressionSyntax);
			}
			this._variablesBuilder = variablesBuilder;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x000358DC File Offset: 0x00033ADC
		public override void VisitEqualsValueClause(EqualsValueClauseSyntax node)
		{
			this.VisitNodeToBind(node.Value);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x000358EA File Offset: 0x00033AEA
		public override void VisitArrowExpressionClause(ArrowExpressionClauseSyntax node)
		{
			this.VisitNodeToBind(node.Expression);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x000358F8 File Offset: 0x00033AF8
		public override void VisitSwitchSection(SwitchSectionSyntax node)
		{
			foreach (SwitchLabelSyntax switchLabelSyntax in node.Labels)
			{
				SyntaxKind syntaxKind = switchLabelSyntax.Kind();
				if (syntaxKind != SyntaxKind.CaseSwitchLabel)
				{
					if (syntaxKind == SyntaxKind.CasePatternSwitchLabel)
					{
						CasePatternSwitchLabelSyntax casePatternSwitchLabelSyntax = (CasePatternSwitchLabelSyntax)switchLabelSyntax;
						SyntaxNode nodeToBind = this._nodeToBind;
						this._nodeToBind = casePatternSwitchLabelSyntax;
						this.Visit(casePatternSwitchLabelSyntax.Pattern);
						if (casePatternSwitchLabelSyntax.WhenClause != null)
						{
							this.VisitNodeToBind(casePatternSwitchLabelSyntax.WhenClause.Condition);
						}
						this._nodeToBind = nodeToBind;
					}
				}
				else
				{
					CaseSwitchLabelSyntax caseSwitchLabelSyntax = (CaseSwitchLabelSyntax)switchLabelSyntax;
					this.VisitNodeToBind(caseSwitchLabelSyntax.Value);
				}
			}
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x000359A0 File Offset: 0x00033BA0
		public override void VisitAttribute(AttributeSyntax node)
		{
			if (node.ArgumentList != null)
			{
				foreach (AttributeArgumentSyntax attributeArgumentSyntax in node.ArgumentList.Arguments)
				{
					this.VisitNodeToBind(attributeArgumentSyntax.Expression);
				}
			}
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x000359E8 File Offset: 0x00033BE8
		public override void VisitThrowStatement(ThrowStatementSyntax node)
		{
			this.VisitNodeToBind(node.Expression);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x000359F6 File Offset: 0x00033BF6
		public override void VisitReturnStatement(ReturnStatementSyntax node)
		{
			this.VisitNodeToBind(node.Expression);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00035A04 File Offset: 0x00033C04
		public override void VisitYieldStatement(YieldStatementSyntax node)
		{
			this.VisitNodeToBind(node.Expression);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00035A12 File Offset: 0x00033C12
		public override void VisitExpressionStatement(ExpressionStatementSyntax node)
		{
			this.VisitNodeToBind(node.Expression);
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00035A20 File Offset: 0x00033C20
		public override void VisitLockStatement(LockStatementSyntax node)
		{
			this.VisitNodeToBind(node.Expression);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00035A2E File Offset: 0x00033C2E
		public override void VisitIfStatement(IfStatementSyntax node)
		{
			this.VisitNodeToBind(node.Condition);
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00035A3C File Offset: 0x00033C3C
		public override void VisitSwitchStatement(SwitchStatementSyntax node)
		{
			this.VisitNodeToBind(node.Expression);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00035A4C File Offset: 0x00033C4C
		public override void VisitDeclarationPattern(DeclarationPatternSyntax node)
		{
			VariableDesignationSyntax designation = node.Designation;
			if (designation != null && designation.Kind() == SyntaxKind.SingleVariableDesignation)
			{
				TFieldOrLocalSymbol tfieldOrLocalSymbol = this.MakePatternVariable(node.Type, (SingleVariableDesignationSyntax)node.Designation, this._nodeToBind);
				if (tfieldOrLocalSymbol != null)
				{
					this._variablesBuilder.Add(tfieldOrLocalSymbol);
				}
			}
			base.VisitDeclarationPattern(node);
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00035AAD File Offset: 0x00033CAD
		public override void VisitVarPattern(VarPatternSyntax node)
		{
			this.VisitPatternDesignation(node.Designation);
			base.VisitVarPattern(node);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00035AC4 File Offset: 0x00033CC4
		private void VisitPatternDesignation(VariableDesignationSyntax node)
		{
			SyntaxKind syntaxKind = node.Kind();
			if (syntaxKind != SyntaxKind.SingleVariableDesignation)
			{
				if (syntaxKind == SyntaxKind.ParenthesizedVariableDesignation)
				{
					foreach (VariableDesignationSyntax variableDesignationSyntax in ((ParenthesizedVariableDesignationSyntax)node).Variables)
					{
						this.VisitPatternDesignation(variableDesignationSyntax);
					}
					return;
				}
				if (syntaxKind != SyntaxKind.DiscardDesignation)
				{
					throw ExceptionUtilities.UnexpectedValue(node.Kind());
				}
			}
			else
			{
				TFieldOrLocalSymbol tfieldOrLocalSymbol = this.MakePatternVariable(null, (SingleVariableDesignationSyntax)node, this._nodeToBind);
				if (tfieldOrLocalSymbol != null)
				{
					this._variablesBuilder.Add(tfieldOrLocalSymbol);
					return;
				}
			}
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00035B60 File Offset: 0x00033D60
		public override void VisitRecursivePattern(RecursivePatternSyntax node)
		{
			TFieldOrLocalSymbol tfieldOrLocalSymbol = this.MakePatternVariable(node, this._nodeToBind);
			if (tfieldOrLocalSymbol != null)
			{
				this._variablesBuilder.Add(tfieldOrLocalSymbol);
			}
			base.VisitRecursivePattern(node);
		}

		// Token: 0x06000587 RID: 1415
		protected abstract TFieldOrLocalSymbol MakePatternVariable(TypeSyntax type, SingleVariableDesignationSyntax designation, SyntaxNode nodeToBind);

		// Token: 0x06000588 RID: 1416
		protected abstract TFieldOrLocalSymbol MakePatternVariable(RecursivePatternSyntax node, SyntaxNode nodeToBind);

		// Token: 0x06000589 RID: 1417 RVA: 0x00035B96 File Offset: 0x00033D96
		public override void VisitParenthesizedLambdaExpression(ParenthesizedLambdaExpressionSyntax node)
		{
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00035B98 File Offset: 0x00033D98
		public override void VisitSimpleLambdaExpression(SimpleLambdaExpressionSyntax node)
		{
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00035B9A File Offset: 0x00033D9A
		public override void VisitAnonymousMethodExpression(AnonymousMethodExpressionSyntax node)
		{
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00035B9C File Offset: 0x00033D9C
		public override void VisitQueryExpression(QueryExpressionSyntax node)
		{
			this.VisitNodeToBind(node.FromClause.Expression);
			this.Visit(node.Body);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00035BBC File Offset: 0x00033DBC
		public override void VisitQueryBody(QueryBodySyntax node)
		{
			foreach (QueryClauseSyntax queryClauseSyntax in node.Clauses)
			{
				if (queryClauseSyntax.Kind() == SyntaxKind.JoinClause)
				{
					this.VisitNodeToBind(((JoinClauseSyntax)queryClauseSyntax).InExpression);
				}
			}
			this.Visit(node.Continuation);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00035C18 File Offset: 0x00033E18
		public override void VisitBinaryExpression(BinaryExpressionSyntax node)
		{
			ArrayBuilder<ExpressionSyntax> instance = ArrayBuilder<ExpressionSyntax>.GetInstance();
			ExpressionSyntax expressionSyntax = node;
			do
			{
				BinaryExpressionSyntax binaryExpressionSyntax = (BinaryExpressionSyntax)expressionSyntax;
				instance.Push(binaryExpressionSyntax.Right);
				expressionSyntax = binaryExpressionSyntax.Left;
			}
			while (expressionSyntax is BinaryExpressionSyntax);
			this.Visit(expressionSyntax);
			while (instance.Count > 0)
			{
				this.Visit(instance.Pop<ExpressionSyntax>());
			}
			instance.Free();
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00035C74 File Offset: 0x00033E74
		public override void VisitDeclarationExpression(DeclarationExpressionSyntax node)
		{
			ArgumentSyntax argumentSyntax = node.Parent as ArgumentSyntax;
			BaseArgumentListSyntax baseArgumentListSyntax = ((argumentSyntax != null) ? argumentSyntax.Parent : null) as BaseArgumentListSyntax;
			this.VisitDeclarationExpressionDesignation(node, node.Designation, baseArgumentListSyntax);
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00035CAC File Offset: 0x00033EAC
		private void VisitDeclarationExpressionDesignation(DeclarationExpressionSyntax node, VariableDesignationSyntax designation, BaseArgumentListSyntax argumentListSyntaxOpt)
		{
			SyntaxKind syntaxKind = designation.Kind();
			if (syntaxKind != SyntaxKind.SingleVariableDesignation)
			{
				if (syntaxKind == SyntaxKind.ParenthesizedVariableDesignation)
				{
					foreach (VariableDesignationSyntax variableDesignationSyntax in ((ParenthesizedVariableDesignationSyntax)designation).Variables)
					{
						this.VisitDeclarationExpressionDesignation(node, variableDesignationSyntax, argumentListSyntaxOpt);
					}
					return;
				}
				if (syntaxKind != SyntaxKind.DiscardDesignation)
				{
					throw ExceptionUtilities.UnexpectedValue(designation.Kind());
				}
			}
			else
			{
				TFieldOrLocalSymbol tfieldOrLocalSymbol = this.MakeDeclarationExpressionVariable(node, (SingleVariableDesignationSyntax)designation, argumentListSyntaxOpt, this._nodeToBind);
				if (tfieldOrLocalSymbol != null)
				{
					this._variablesBuilder.Add(tfieldOrLocalSymbol);
					return;
				}
			}
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00035D48 File Offset: 0x00033F48
		public override void VisitAssignmentExpression(AssignmentExpressionSyntax node)
		{
			if (node.IsDeconstruction())
			{
				this.CollectVariablesFromDeconstruction(node.Left, node);
			}
			else
			{
				this.Visit(node.Left);
			}
			this.Visit(node.Right);
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00035D79 File Offset: 0x00033F79
		public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
		{
			if (node.Initializer != null)
			{
				this.VisitNodeToBind(node.Initializer);
			}
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00035D90 File Offset: 0x00033F90
		public override void VisitRecordDeclaration(RecordDeclarationSyntax node)
		{
			PrimaryConstructorBaseTypeSyntax primaryConstructorBaseTypeIfClass = node.PrimaryConstructorBaseTypeIfClass;
			if (primaryConstructorBaseTypeIfClass != null)
			{
				this.VisitNodeToBind(primaryConstructorBaseTypeIfClass);
			}
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00035DB0 File Offset: 0x00033FB0
		private void CollectVariablesFromDeconstruction(ExpressionSyntax possibleTupleDeclaration, AssignmentExpressionSyntax deconstruction)
		{
			SyntaxKind syntaxKind = possibleTupleDeclaration.Kind();
			if (syntaxKind == SyntaxKind.TupleExpression)
			{
				foreach (ArgumentSyntax argumentSyntax in ((TupleExpressionSyntax)possibleTupleDeclaration).Arguments)
				{
					this.CollectVariablesFromDeconstruction(argumentSyntax.Expression, deconstruction);
				}
				return;
			}
			if (syntaxKind != SyntaxKind.DeclarationExpression)
			{
				this.Visit(possibleTupleDeclaration);
				return;
			}
			DeclarationExpressionSyntax declarationExpressionSyntax = (DeclarationExpressionSyntax)possibleTupleDeclaration;
			this.CollectVariablesFromDeconstruction(declarationExpressionSyntax.Designation, declarationExpressionSyntax.Type, deconstruction);
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00035E30 File Offset: 0x00034030
		private void CollectVariablesFromDeconstruction(VariableDesignationSyntax designation, TypeSyntax closestTypeSyntax, AssignmentExpressionSyntax deconstruction)
		{
			SyntaxKind syntaxKind = designation.Kind();
			if (syntaxKind != SyntaxKind.SingleVariableDesignation)
			{
				if (syntaxKind == SyntaxKind.ParenthesizedVariableDesignation)
				{
					foreach (VariableDesignationSyntax variableDesignationSyntax in ((ParenthesizedVariableDesignationSyntax)designation).Variables)
					{
						this.CollectVariablesFromDeconstruction(variableDesignationSyntax, closestTypeSyntax, deconstruction);
					}
					return;
				}
				if (syntaxKind != SyntaxKind.DiscardDesignation)
				{
					throw ExceptionUtilities.UnexpectedValue(designation.Kind());
				}
			}
			else
			{
				SingleVariableDesignationSyntax singleVariableDesignationSyntax = (SingleVariableDesignationSyntax)designation;
				TFieldOrLocalSymbol tfieldOrLocalSymbol = this.MakeDeconstructionVariable(closestTypeSyntax, singleVariableDesignationSyntax, deconstruction);
				if (tfieldOrLocalSymbol != null)
				{
					this._variablesBuilder.Add(tfieldOrLocalSymbol);
					return;
				}
			}
		}

		// Token: 0x06000596 RID: 1430
		protected abstract TFieldOrLocalSymbol MakeDeclarationExpressionVariable(DeclarationExpressionSyntax node, SingleVariableDesignationSyntax designation, BaseArgumentListSyntax argumentListSyntax, SyntaxNode nodeToBind);

		// Token: 0x06000597 RID: 1431
		protected abstract TFieldOrLocalSymbol MakeDeconstructionVariable(TypeSyntax closestTypeSyntax, SingleVariableDesignationSyntax designation, AssignmentExpressionSyntax deconstruction);

		// Token: 0x06000598 RID: 1432 RVA: 0x00035EC9 File Offset: 0x000340C9
		protected ExpressionVariableFinder()
			: base(SyntaxWalkerDepth.Node)
		{
		}

		// Token: 0x04000057 RID: 87
		private ArrayBuilder<TFieldOrLocalSymbol> _variablesBuilder;

		// Token: 0x04000058 RID: 88
		private SyntaxNode _nodeToBind;
	}
}
