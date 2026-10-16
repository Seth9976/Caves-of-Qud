using System;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001FA RID: 506
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class DebugInfoInjector : CompoundInstrumenter
	{
		// Token: 0x060022D9 RID: 8921 RVA: 0x000B71D2 File Offset: 0x000B53D2
		public DebugInfoInjector(Instrumenter previous)
			: base(previous)
		{
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x000B71DB File Offset: 0x000B53DB
		public override BoundStatement InstrumentNoOpStatement(BoundNoOpStatement original, BoundStatement rewritten)
		{
			return DebugInfoInjector.AddSequencePoint(base.InstrumentNoOpStatement(original, rewritten));
		}

		// Token: 0x060022DB RID: 8923 RVA: 0x000B71EA File Offset: 0x000B53EA
		public override BoundStatement InstrumentBreakStatement(BoundBreakStatement original, BoundStatement rewritten)
		{
			return DebugInfoInjector.AddSequencePoint(base.InstrumentBreakStatement(original, rewritten));
		}

		// Token: 0x060022DC RID: 8924 RVA: 0x000B71F9 File Offset: 0x000B53F9
		public override BoundStatement InstrumentContinueStatement(BoundContinueStatement original, BoundStatement rewritten)
		{
			return DebugInfoInjector.AddSequencePoint(base.InstrumentContinueStatement(original, rewritten));
		}

		// Token: 0x060022DD RID: 8925 RVA: 0x000B7208 File Offset: 0x000B5408
		public override BoundStatement InstrumentExpressionStatement(BoundExpressionStatement original, BoundStatement rewritten)
		{
			rewritten = base.InstrumentExpressionStatement(original, rewritten);
			if (original.IsConstructorInitializer())
			{
				SyntaxKind syntaxKind = original.Syntax.Kind();
				if (syntaxKind == SyntaxKind.ConstructorDeclaration)
				{
					ConstructorDeclarationSyntax constructorDeclarationSyntax = (ConstructorDeclarationSyntax)original.Syntax;
					return new BoundSequencePointWithSpan(constructorDeclarationSyntax, rewritten, DebugInfoInjector.CreateSpanForConstructorInitializer(constructorDeclarationSyntax), false);
				}
				if (syntaxKind - SyntaxKind.BaseConstructorInitializer <= 1)
				{
					ConstructorInitializerSyntax constructorInitializerSyntax = (ConstructorInitializerSyntax)original.Syntax;
					return new BoundSequencePointWithSpan(constructorInitializerSyntax, rewritten, DebugInfoInjector.CreateSpanForConstructorInitializer((ConstructorDeclarationSyntax)constructorInitializerSyntax.Parent), false);
				}
			}
			return DebugInfoInjector.AddSequencePoint(rewritten);
		}

		// Token: 0x060022DE RID: 8926 RVA: 0x000B7290 File Offset: 0x000B5490
		public override BoundStatement InstrumentFieldOrPropertyInitializer(BoundStatement original, BoundStatement rewritten)
		{
			rewritten = base.InstrumentFieldOrPropertyInitializer(original, rewritten);
			SyntaxNode syntax = original.Syntax;
			if (rewritten.Kind == BoundKind.Block)
			{
				BoundBlock boundBlock = (BoundBlock)rewritten;
				return boundBlock.Update(boundBlock.Locals, boundBlock.LocalFunctions, ImmutableArray.Create<BoundStatement>(DebugInfoInjector.InstrumentFieldOrPropertyInitializer(boundBlock.Statements.Single<BoundStatement>(), syntax)));
			}
			return DebugInfoInjector.InstrumentFieldOrPropertyInitializer(rewritten, syntax);
		}

		// Token: 0x060022DF RID: 8927 RVA: 0x000B72F0 File Offset: 0x000B54F0
		private static BoundStatement InstrumentFieldOrPropertyInitializer(BoundStatement rewritten, SyntaxNode syntax)
		{
			if (syntax.IsKind(SyntaxKind.Parameter))
			{
				return DebugInfoInjector.AddSequencePoint(rewritten);
			}
			SyntaxNode parent = syntax.Parent.Parent;
			SyntaxKind syntaxKind = parent.Kind();
			if (syntaxKind == SyntaxKind.VariableDeclarator)
			{
				return DebugInfoInjector.AddSequencePoint((VariableDeclaratorSyntax)parent, rewritten);
			}
			if (syntaxKind != SyntaxKind.PropertyDeclaration)
			{
				throw ExceptionUtilities.UnexpectedValue(parent.Kind());
			}
			return DebugInfoInjector.AddSequencePoint((PropertyDeclarationSyntax)parent, rewritten);
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x000B7360 File Offset: 0x000B5560
		public override BoundStatement InstrumentGotoStatement(BoundGotoStatement original, BoundStatement rewritten)
		{
			return DebugInfoInjector.AddSequencePoint(base.InstrumentGotoStatement(original, rewritten));
		}

		// Token: 0x060022E1 RID: 8929 RVA: 0x000B736F File Offset: 0x000B556F
		public override BoundStatement InstrumentThrowStatement(BoundThrowStatement original, BoundStatement rewritten)
		{
			return DebugInfoInjector.AddSequencePoint(base.InstrumentThrowStatement(original, rewritten));
		}

		// Token: 0x060022E2 RID: 8930 RVA: 0x000B7380 File Offset: 0x000B5580
		public override BoundStatement InstrumentYieldBreakStatement(BoundYieldBreakStatement original, BoundStatement rewritten)
		{
			rewritten = base.InstrumentYieldBreakStatement(original, rewritten);
			if (original.WasCompilerGenerated && original.Syntax.Kind() == SyntaxKind.Block)
			{
				return new BoundSequencePointWithSpan(original.Syntax, rewritten, ((BlockSyntax)original.Syntax).CloseBraceToken.Span, false);
			}
			return DebugInfoInjector.AddSequencePoint(rewritten);
		}

		// Token: 0x060022E3 RID: 8931 RVA: 0x000B73DD File Offset: 0x000B55DD
		public override BoundStatement InstrumentYieldReturnStatement(BoundYieldReturnStatement original, BoundStatement rewritten)
		{
			return DebugInfoInjector.AddSequencePoint(base.InstrumentYieldReturnStatement(original, rewritten));
		}

		// Token: 0x060022E4 RID: 8932 RVA: 0x000B73EC File Offset: 0x000B55EC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override BoundStatement CreateBlockPrologue([global::System.Runtime.CompilerServices.Nullable(1)] BoundBlock original, out LocalSymbol synthesizedLocal)
		{
			BoundStatement boundStatement = base.CreateBlockPrologue(original, out synthesizedLocal);
			if (original.Syntax.Kind() == SyntaxKind.Block && !original.WasCompilerGenerated)
			{
				TextSpan span = ((BlockSyntax)original.Syntax).OpenBraceToken.Span;
				return new BoundSequencePointWithSpan(original.Syntax, boundStatement, span, false);
			}
			if (boundStatement != null)
			{
				return new BoundSequencePoint(original.Syntax, boundStatement, false);
			}
			return null;
		}

		// Token: 0x060022E5 RID: 8933 RVA: 0x000B7458 File Offset: 0x000B5658
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundStatement CreateBlockEpilogue(BoundBlock original)
		{
			BoundStatement boundStatement = base.CreateBlockEpilogue(original);
			if (original.Syntax.Kind() == SyntaxKind.Block && !original.WasCompilerGenerated)
			{
				SyntaxNode parent = original.Syntax.Parent;
				if (parent == null || (!parent.IsAnonymousFunction() && !(parent is BaseMethodDeclarationSyntax)))
				{
					TextSpan span = ((BlockSyntax)original.Syntax).CloseBraceToken.Span;
					return new BoundSequencePointWithSpan(original.Syntax, boundStatement, span, false);
				}
			}
			return boundStatement;
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x000B74CF File Offset: 0x000B56CF
		public override BoundExpression InstrumentDoStatementCondition(BoundDoStatement original, BoundExpression rewrittenCondition, SyntheticBoundNodeFactory factory)
		{
			return DebugInfoInjector.AddConditionSequencePoint(base.InstrumentDoStatementCondition(original, rewrittenCondition, factory), original.Syntax, factory);
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x000B74E6 File Offset: 0x000B56E6
		public override BoundExpression InstrumentWhileStatementCondition(BoundWhileStatement original, BoundExpression rewrittenCondition, SyntheticBoundNodeFactory factory)
		{
			return DebugInfoInjector.AddConditionSequencePoint(base.InstrumentWhileStatementCondition(original, rewrittenCondition, factory), original.Syntax, factory);
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x000B7500 File Offset: 0x000B5700
		public override BoundStatement InstrumentDoStatementConditionalGotoStart(BoundDoStatement original, BoundStatement ifConditionGotoStart)
		{
			DoStatementSyntax doStatementSyntax = (DoStatementSyntax)original.Syntax;
			TextSpan textSpan = TextSpan.FromBounds(doStatementSyntax.WhileKeyword.SpanStart, doStatementSyntax.SemicolonToken.Span.End);
			return new BoundSequencePointWithSpan(doStatementSyntax, base.InstrumentDoStatementConditionalGotoStart(original, ifConditionGotoStart), textSpan, false);
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x000B7554 File Offset: 0x000B5754
		public override BoundStatement InstrumentWhileStatementConditionalGotoStartOrBreak(BoundWhileStatement original, BoundStatement ifConditionGotoStart)
		{
			WhileStatementSyntax whileStatementSyntax = (WhileStatementSyntax)original.Syntax;
			TextSpan textSpan = TextSpan.FromBounds(whileStatementSyntax.WhileKeyword.SpanStart, whileStatementSyntax.CloseParenToken.Span.End);
			return new BoundSequencePointWithSpan(whileStatementSyntax, base.InstrumentWhileStatementConditionalGotoStartOrBreak(original, ifConditionGotoStart), textSpan, false);
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x000B75A7 File Offset: 0x000B57A7
		private static BoundExpression AddConditionSequencePoint(BoundExpression condition, BoundStatement containingStatement, SyntheticBoundNodeFactory factory)
		{
			return DebugInfoInjector.AddConditionSequencePoint(condition, containingStatement.Syntax, factory);
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x000B75B6 File Offset: 0x000B57B6
		public override BoundStatement InstrumentForEachStatementCollectionVarDeclaration(BoundForEachStatement original, [global::System.Runtime.CompilerServices.Nullable(2)] BoundStatement collectionVarDecl)
		{
			return new BoundSequencePoint(((CommonForEachStatementSyntax)original.Syntax).Expression, base.InstrumentForEachStatementCollectionVarDeclaration(original, collectionVarDecl), false);
		}

		// Token: 0x060022EC RID: 8940 RVA: 0x000B75D8 File Offset: 0x000B57D8
		public override BoundStatement InstrumentForEachStatementDeconstructionVariablesDeclaration(BoundForEachStatement original, BoundStatement iterationVarDecl)
		{
			ForEachVariableStatementSyntax forEachVariableStatementSyntax = (ForEachVariableStatementSyntax)original.Syntax;
			return new BoundSequencePointWithSpan(forEachVariableStatementSyntax, base.InstrumentForEachStatementDeconstructionVariablesDeclaration(original, iterationVarDecl), forEachVariableStatementSyntax.Variable.Span, false);
		}

		// Token: 0x060022ED RID: 8941 RVA: 0x000B760C File Offset: 0x000B580C
		public override BoundStatement InstrumentForEachStatement(BoundForEachStatement original, BoundStatement rewritten)
		{
			CommonForEachStatementSyntax commonForEachStatementSyntax = (CommonForEachStatementSyntax)original.Syntax;
			TextSpan textSpan = ((commonForEachStatementSyntax.AwaitKeyword != default(SyntaxToken)) ? TextSpan.FromBounds(commonForEachStatementSyntax.AwaitKeyword.Span.Start, commonForEachStatementSyntax.ForEachKeyword.Span.End) : commonForEachStatementSyntax.ForEachKeyword.Span);
			BoundSequencePointWithSpan boundSequencePointWithSpan = new BoundSequencePointWithSpan(commonForEachStatementSyntax, null, textSpan, false);
			return new BoundStatementList(commonForEachStatementSyntax, ImmutableArray.Create<BoundStatement>(boundSequencePointWithSpan, base.InstrumentForEachStatement(original, rewritten)), false);
		}

		// Token: 0x060022EE RID: 8942 RVA: 0x000B76A0 File Offset: 0x000B58A0
		public override BoundStatement InstrumentForEachStatementIterationVarDeclaration(BoundForEachStatement original, BoundStatement iterationVarDecl)
		{
			SyntaxKind syntaxKind = original.Syntax.Kind();
			TextSpan textSpan;
			if (syntaxKind != SyntaxKind.ForEachStatement)
			{
				if (syntaxKind != SyntaxKind.ForEachVariableStatement)
				{
					throw ExceptionUtilities.UnexpectedValue(original.Syntax.Kind());
				}
				textSpan = ((ForEachVariableStatementSyntax)original.Syntax).Variable.Span;
			}
			else
			{
				ForEachStatementSyntax forEachStatementSyntax = (ForEachStatementSyntax)original.Syntax;
				textSpan = TextSpan.FromBounds(forEachStatementSyntax.Type.SpanStart, forEachStatementSyntax.Identifier.Span.End);
			}
			return new BoundSequencePointWithSpan(original.Syntax, base.InstrumentForEachStatementIterationVarDeclaration(original, iterationVarDecl), textSpan, false);
		}

		// Token: 0x060022EF RID: 8943 RVA: 0x000B7744 File Offset: 0x000B5944
		public override BoundStatement InstrumentForStatementConditionalGotoStartOrBreak(BoundForStatement original, BoundStatement branchBack)
		{
			BoundExpression condition = original.Condition;
			return BoundSequencePoint.Create((condition != null) ? condition.Syntax : null, base.InstrumentForStatementConditionalGotoStartOrBreak(original, branchBack), false, false);
		}

		// Token: 0x060022F0 RID: 8944 RVA: 0x000B7768 File Offset: 0x000B5968
		public override BoundStatement InstrumentForEachStatementConditionalGotoStart(BoundForEachStatement original, BoundStatement branchBack)
		{
			CommonForEachStatementSyntax commonForEachStatementSyntax = (CommonForEachStatementSyntax)original.Syntax;
			return new BoundSequencePointWithSpan(commonForEachStatementSyntax, base.InstrumentForEachStatementConditionalGotoStart(original, branchBack), commonForEachStatementSyntax.InKeyword.Span, false);
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x000B779E File Offset: 0x000B599E
		public override BoundExpression InstrumentForStatementCondition(BoundForStatement original, BoundExpression rewrittenCondition, SyntheticBoundNodeFactory factory)
		{
			return DebugInfoInjector.AddConditionSequencePoint(base.InstrumentForStatementCondition(original, rewrittenCondition, factory), original.Syntax, factory);
		}

		// Token: 0x060022F2 RID: 8946 RVA: 0x000B77B8 File Offset: 0x000B59B8
		public override BoundStatement InstrumentIfStatement(BoundIfStatement original, BoundStatement rewritten)
		{
			IfStatementSyntax ifStatementSyntax = (IfStatementSyntax)original.Syntax;
			return new BoundSequencePointWithSpan(ifStatementSyntax, base.InstrumentIfStatement(original, rewritten), TextSpan.FromBounds(ifStatementSyntax.IfKeyword.SpanStart, ifStatementSyntax.CloseParenToken.Span.End), original.HasErrors);
		}

		// Token: 0x060022F3 RID: 8947 RVA: 0x000B780E File Offset: 0x000B5A0E
		public override BoundExpression InstrumentIfStatementCondition(BoundIfStatement original, BoundExpression rewrittenCondition, SyntheticBoundNodeFactory factory)
		{
			return DebugInfoInjector.AddConditionSequencePoint(base.InstrumentIfStatementCondition(original, rewrittenCondition, factory), original.Syntax, factory);
		}

		// Token: 0x060022F4 RID: 8948 RVA: 0x000B7828 File Offset: 0x000B5A28
		public override BoundStatement InstrumentLabelStatement(BoundLabeledStatement original, BoundStatement rewritten)
		{
			LabeledStatementSyntax labeledStatementSyntax = (LabeledStatementSyntax)original.Syntax;
			TextSpan textSpan = TextSpan.FromBounds(labeledStatementSyntax.Identifier.SpanStart, labeledStatementSyntax.ColonToken.Span.End);
			return new BoundSequencePointWithSpan(labeledStatementSyntax, base.InstrumentLabelStatement(original, rewritten), textSpan, false);
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x000B787C File Offset: 0x000B5A7C
		public override BoundStatement InstrumentLocalInitialization(BoundLocalDeclaration original, BoundStatement rewritten)
		{
			return DebugInfoInjector.AddSequencePoint((original.Syntax.Kind() == SyntaxKind.VariableDeclarator) ? ((VariableDeclaratorSyntax)original.Syntax) : ((LocalDeclarationStatementSyntax)original.Syntax).Declaration.Variables.First(), base.InstrumentLocalInitialization(original, rewritten));
		}

		// Token: 0x060022F6 RID: 8950 RVA: 0x000B78D4 File Offset: 0x000B5AD4
		public override BoundStatement InstrumentLockTargetCapture(BoundLockStatement original, BoundStatement lockTargetCapture)
		{
			LockStatementSyntax lockStatementSyntax = (LockStatementSyntax)original.Syntax;
			return new BoundSequencePointWithSpan(lockStatementSyntax, base.InstrumentLockTargetCapture(original, lockTargetCapture), TextSpan.FromBounds(lockStatementSyntax.LockKeyword.SpanStart, lockStatementSyntax.CloseParenToken.Span.End), false);
		}

		// Token: 0x060022F7 RID: 8951 RVA: 0x000B7928 File Offset: 0x000B5B28
		public override BoundStatement InstrumentReturnStatement(BoundReturnStatement original, BoundStatement rewritten)
		{
			rewritten = base.InstrumentReturnStatement(original, rewritten);
			if (original.WasCompilerGenerated && original.ExpressionOpt == null && original.Syntax.Kind() == SyntaxKind.Block)
			{
				return new BoundSequencePointWithSpan(original.Syntax, rewritten, ((BlockSyntax)original.Syntax).CloseBraceToken.Span, false);
			}
			return new BoundSequencePoint(original.Syntax, rewritten, false);
		}

		// Token: 0x060022F8 RID: 8952 RVA: 0x000B7994 File Offset: 0x000B5B94
		public override BoundStatement InstrumentSwitchStatement(BoundSwitchStatement original, BoundStatement rewritten)
		{
			SwitchStatementSyntax switchStatementSyntax = (SwitchStatementSyntax)original.Syntax;
			TextSpan textSpan = TextSpan.FromBounds(switchStatementSyntax.SwitchKeyword.SpanStart, (switchStatementSyntax.CloseParenToken != default(SyntaxToken)) ? switchStatementSyntax.CloseParenToken.Span.End : switchStatementSyntax.Expression.Span.End);
			return new BoundSequencePointWithSpan(switchStatementSyntax, base.InstrumentSwitchStatement(original, rewritten), textSpan, false);
		}

		// Token: 0x060022F9 RID: 8953 RVA: 0x000B7A14 File Offset: 0x000B5C14
		public override BoundStatement InstrumentSwitchWhenClauseConditionalGotoBody(BoundExpression original, BoundStatement ifConditionGotoBody)
		{
			WhenClauseSyntax whenClauseSyntax = original.Syntax.FirstAncestorOrSelf<WhenClauseSyntax>(null, true);
			return new BoundSequencePointWithSpan(whenClauseSyntax, base.InstrumentSwitchWhenClauseConditionalGotoBody(original, ifConditionGotoBody), whenClauseSyntax.Span, false);
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x000B7A44 File Offset: 0x000B5C44
		public override BoundStatement InstrumentUsingTargetCapture(BoundUsingStatement original, BoundStatement usingTargetCapture)
		{
			return DebugInfoInjector.AddSequencePoint((UsingStatementSyntax)original.Syntax, base.InstrumentUsingTargetCapture(original, usingTargetCapture));
		}

		// Token: 0x060022FB RID: 8955 RVA: 0x000B7A60 File Offset: 0x000B5C60
		public override BoundExpression InstrumentCatchClauseFilter(BoundCatchBlock original, BoundExpression rewrittenFilter, SyntheticBoundNodeFactory factory)
		{
			rewrittenFilter = base.InstrumentCatchClauseFilter(original, rewrittenFilter, factory);
			CatchFilterClauseSyntax filter = ((CatchClauseSyntax)original.Syntax).Filter;
			return DebugInfoInjector.AddConditionSequencePoint(new BoundSequencePointExpression(filter, rewrittenFilter, rewrittenFilter.Type, false), filter, factory);
		}

		// Token: 0x060022FC RID: 8956 RVA: 0x000B7A9E File Offset: 0x000B5C9E
		public override BoundExpression InstrumentSwitchStatementExpression(BoundStatement original, BoundExpression rewrittenExpression, SyntheticBoundNodeFactory factory)
		{
			return DebugInfoInjector.AddConditionSequencePoint(base.InstrumentSwitchStatementExpression(original, rewrittenExpression, factory), original.Syntax, factory);
		}

		// Token: 0x060022FD RID: 8957 RVA: 0x000B7AB5 File Offset: 0x000B5CB5
		public override BoundExpression InstrumentSwitchExpressionArmExpression(BoundExpression original, BoundExpression rewrittenExpression, SyntheticBoundNodeFactory factory)
		{
			return new BoundSequencePointExpression(original.Syntax, base.InstrumentSwitchExpressionArmExpression(original, rewrittenExpression, factory), rewrittenExpression.Type, false);
		}

		// Token: 0x060022FE RID: 8958 RVA: 0x000B7AD2 File Offset: 0x000B5CD2
		public override BoundStatement InstrumentSwitchBindCasePatternVariables(BoundStatement bindings)
		{
			return BoundSequencePoint.CreateHidden(base.InstrumentSwitchBindCasePatternVariables(bindings), false);
		}

		// Token: 0x060022FF RID: 8959 RVA: 0x000B7AE1 File Offset: 0x000B5CE1
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private static BoundStatement AddSequencePoint(BoundStatement node)
		{
			return new BoundSequencePoint(node.Syntax, node, false);
		}

		// Token: 0x06002300 RID: 8960 RVA: 0x000B7AF0 File Offset: 0x000B5CF0
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static BoundStatement AddSequencePoint(VariableDeclaratorSyntax declaratorSyntax, BoundStatement rewrittenStatement)
		{
			SyntaxNode syntaxNode;
			TextSpan? textSpan;
			DebugInfoInjector.GetBreakpointSpan(declaratorSyntax, out syntaxNode, out textSpan);
			BoundStatement boundStatement = BoundSequencePoint.Create(declaratorSyntax, textSpan, rewrittenStatement, false);
			boundStatement.WasCompilerGenerated = rewrittenStatement.WasCompilerGenerated;
			return boundStatement;
		}

		// Token: 0x06002301 RID: 8961 RVA: 0x000B7B1C File Offset: 0x000B5D1C
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static BoundStatement AddSequencePoint(PropertyDeclarationSyntax declarationSyntax, BoundStatement rewrittenStatement)
		{
			int spanStart = declarationSyntax.Initializer.Value.SpanStart;
			int end = declarationSyntax.Initializer.Span.End;
			TextSpan textSpan = TextSpan.FromBounds(spanStart, end);
			BoundStatement boundStatement = BoundSequencePoint.Create(declarationSyntax, new TextSpan?(textSpan), rewrittenStatement, false);
			boundStatement.WasCompilerGenerated = rewrittenStatement.WasCompilerGenerated;
			return boundStatement;
		}

		// Token: 0x06002302 RID: 8962 RVA: 0x000B7B70 File Offset: 0x000B5D70
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static BoundStatement AddSequencePoint(UsingStatementSyntax usingSyntax, BoundStatement rewrittenStatement)
		{
			int start = usingSyntax.Span.Start;
			int end = usingSyntax.CloseParenToken.Span.End;
			TextSpan textSpan = TextSpan.FromBounds(start, end);
			return new BoundSequencePointWithSpan(usingSyntax, rewrittenStatement, textSpan, false);
		}

		// Token: 0x06002303 RID: 8963 RVA: 0x000B7BB4 File Offset: 0x000B5DB4
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private static TextSpan CreateSpanForConstructorInitializer(ConstructorDeclarationSyntax constructorSyntax)
		{
			if (constructorSyntax.Initializer != null)
			{
				int spanStart = constructorSyntax.Initializer.ThisOrBaseKeyword.SpanStart;
				int end = constructorSyntax.Initializer.ArgumentList.CloseParenToken.Span.End;
				return TextSpan.FromBounds(spanStart, end);
			}
			if (constructorSyntax.Modifiers.Any(SyntaxKind.StaticKeyword))
			{
				int spanStart2 = constructorSyntax.Body.OpenBraceToken.SpanStart;
				int end2 = constructorSyntax.Body.OpenBraceToken.Span.End;
				return TextSpan.FromBounds(spanStart2, end2);
			}
			return DebugInfoInjector.CreateSpan(constructorSyntax.Modifiers, constructorSyntax.Identifier, constructorSyntax.ParameterList.CloseParenToken);
		}

		// Token: 0x06002304 RID: 8964 RVA: 0x000B7C74 File Offset: 0x000B5E74
		private static TextSpan CreateSpan(SyntaxTokenList startOpt, SyntaxNodeOrToken startFallbackOpt, SyntaxNodeOrToken endOpt)
		{
			int num;
			if (startOpt.Count > 0)
			{
				num = startOpt.First().SpanStart;
			}
			else if (startFallbackOpt != default(SyntaxNodeOrToken))
			{
				num = startFallbackOpt.SpanStart;
			}
			else
			{
				num = endOpt.SpanStart;
			}
			int num2;
			if (endOpt != default(SyntaxNodeOrToken))
			{
				num2 = DebugInfoInjector.GetEndPosition(endOpt);
			}
			else
			{
				num2 = DebugInfoInjector.GetEndPosition(startFallbackOpt);
			}
			return TextSpan.FromBounds(num, num2);
		}

		// Token: 0x06002305 RID: 8965 RVA: 0x000B7CE8 File Offset: 0x000B5EE8
		private static int GetEndPosition(SyntaxNodeOrToken nodeOrToken)
		{
			if (nodeOrToken.IsToken)
			{
				return nodeOrToken.Span.End;
			}
			return nodeOrToken.AsNode().GetLastToken(false, false, false, false).Span.End;
		}

		// Token: 0x06002306 RID: 8966 RVA: 0x000B7D30 File Offset: 0x000B5F30
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static void GetBreakpointSpan(VariableDeclaratorSyntax declaratorSyntax, out SyntaxNode node, out TextSpan? part)
		{
			VariableDeclarationSyntax variableDeclarationSyntax = (VariableDeclarationSyntax)declaratorSyntax.Parent;
			if (variableDeclarationSyntax.Variables.First() == declaratorSyntax)
			{
				SyntaxKind syntaxKind = variableDeclarationSyntax.Parent.Kind();
				if (syntaxKind <= SyntaxKind.ForStatement)
				{
					if (syntaxKind == SyntaxKind.LocalDeclarationStatement)
					{
						LocalDeclarationStatementSyntax localDeclarationStatementSyntax = (LocalDeclarationStatementSyntax)variableDeclarationSyntax.Parent;
						DebugInfoInjector.GetFirstLocalOrFieldBreakpointSpan((localDeclarationStatementSyntax.UsingKeyword == default(SyntaxToken)) ? default(SyntaxToken?) : new SyntaxToken?((localDeclarationStatementSyntax.AwaitKeyword == default(SyntaxToken)) ? localDeclarationStatementSyntax.UsingKeyword : localDeclarationStatementSyntax.AwaitKeyword), declaratorSyntax, out node, out part);
						return;
					}
					if (syntaxKind != SyntaxKind.ForStatement)
					{
						goto IL_0133;
					}
				}
				else if (syntaxKind - SyntaxKind.UsingStatement > 1)
				{
					if (syntaxKind - SyntaxKind.FieldDeclaration <= 1)
					{
						SyntaxTokenList modifiers = ((BaseFieldDeclarationSyntax)variableDeclarationSyntax.Parent).Modifiers;
						DebugInfoInjector.GetFirstLocalOrFieldBreakpointSpan(modifiers.Any() ? new SyntaxToken?(modifiers[0]) : default(SyntaxToken?), declaratorSyntax, out node, out part);
						return;
					}
					goto IL_0133;
				}
				node = variableDeclarationSyntax;
				part = new TextSpan?(TextSpan.FromBounds(variableDeclarationSyntax.SpanStart, declaratorSyntax.Span.End));
				return;
				IL_0133:
				throw ExceptionUtilities.UnexpectedValue(variableDeclarationSyntax.Parent.Kind());
			}
			node = declaratorSyntax;
			part = default(TextSpan?);
		}

		// Token: 0x06002307 RID: 8967 RVA: 0x000B7E90 File Offset: 0x000B6090
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static void GetFirstLocalOrFieldBreakpointSpan(SyntaxToken? firstToken, VariableDeclaratorSyntax declaratorSyntax, out SyntaxNode node, out TextSpan? part)
		{
			VariableDeclarationSyntax variableDeclarationSyntax = (VariableDeclarationSyntax)declaratorSyntax.Parent;
			int num = ((firstToken != null) ? firstToken.GetValueOrDefault().SpanStart : variableDeclarationSyntax.SpanStart);
			int num2;
			if (variableDeclarationSyntax.Variables.Count == 1)
			{
				num2 = variableDeclarationSyntax.Parent.Span.End;
			}
			else
			{
				num2 = declaratorSyntax.Span.End;
			}
			part = new TextSpan?(TextSpan.FromBounds(num, num2));
			node = variableDeclarationSyntax.Parent;
		}

		// Token: 0x06002308 RID: 8968 RVA: 0x000B7F20 File Offset: 0x000B6120
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private static BoundExpression AddConditionSequencePoint(BoundExpression condition, SyntaxNode synthesizedVariableSyntax, SyntheticBoundNodeFactory factory)
		{
			if (!factory.Compilation.Options.EnableEditAndContinue)
			{
				return condition;
			}
			LocalSymbol localSymbol = factory.SynthesizedLocal(condition.Type, synthesizedVariableSyntax, false, RefKind.None, SynthesizedLocalKind.ConditionalBranchDiscriminator);
			BoundExpression boundExpression = ((condition.ConstantValue == null) ? new BoundSequencePointExpression(null, factory.Local(localSymbol), condition.Type, false) : condition);
			return new BoundSequence(condition.Syntax, ImmutableArray.Create<LocalSymbol>(localSymbol), ImmutableArray.Create<BoundExpression>(factory.AssignmentExpression(factory.Local(localSymbol), condition, false)), boundExpression, condition.Type, false);
		}

		// Token: 0x040010A7 RID: 4263
		public static readonly DebugInfoInjector Singleton = new DebugInfoInjector(Instrumenter.NoOp);
	}
}
