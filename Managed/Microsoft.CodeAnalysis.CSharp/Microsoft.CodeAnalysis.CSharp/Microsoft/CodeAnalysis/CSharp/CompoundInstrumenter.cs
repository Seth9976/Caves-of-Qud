using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001F9 RID: 505
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class CompoundInstrumenter : Instrumenter
	{
		// Token: 0x060022B4 RID: 8884 RVA: 0x000B6FA9 File Offset: 0x000B51A9
		public CompoundInstrumenter(Instrumenter previous)
		{
			this.Previous = previous;
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x060022B5 RID: 8885 RVA: 0x000B6FB8 File Offset: 0x000B51B8
		public Instrumenter Previous { get; }

		// Token: 0x060022B6 RID: 8886 RVA: 0x000B6FC0 File Offset: 0x000B51C0
		public override BoundStatement InstrumentNoOpStatement(BoundNoOpStatement original, BoundStatement rewritten)
		{
			return this.Previous.InstrumentNoOpStatement(original, rewritten);
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x000B6FCF File Offset: 0x000B51CF
		public override BoundStatement InstrumentYieldBreakStatement(BoundYieldBreakStatement original, BoundStatement rewritten)
		{
			return this.Previous.InstrumentYieldBreakStatement(original, rewritten);
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x000B6FDE File Offset: 0x000B51DE
		public override BoundStatement InstrumentYieldReturnStatement(BoundYieldReturnStatement original, BoundStatement rewritten)
		{
			return this.Previous.InstrumentYieldReturnStatement(original, rewritten);
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x000B6FED File Offset: 0x000B51ED
		public override BoundStatement InstrumentThrowStatement(BoundThrowStatement original, BoundStatement rewritten)
		{
			return this.Previous.InstrumentThrowStatement(original, rewritten);
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x000B6FFC File Offset: 0x000B51FC
		public override BoundStatement InstrumentContinueStatement(BoundContinueStatement original, BoundStatement rewritten)
		{
			return this.Previous.InstrumentContinueStatement(original, rewritten);
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x000B700B File Offset: 0x000B520B
		public override BoundStatement InstrumentGotoStatement(BoundGotoStatement original, BoundStatement rewritten)
		{
			return this.Previous.InstrumentGotoStatement(original, rewritten);
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x000B701A File Offset: 0x000B521A
		public override BoundStatement InstrumentExpressionStatement(BoundExpressionStatement original, BoundStatement rewritten)
		{
			return this.Previous.InstrumentExpressionStatement(original, rewritten);
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x000B7029 File Offset: 0x000B5229
		public override BoundStatement InstrumentFieldOrPropertyInitializer(BoundStatement original, BoundStatement rewritten)
		{
			return this.Previous.InstrumentFieldOrPropertyInitializer(original, rewritten);
		}

		// Token: 0x060022BE RID: 8894 RVA: 0x000B7038 File Offset: 0x000B5238
		public override BoundStatement InstrumentBreakStatement(BoundBreakStatement original, BoundStatement rewritten)
		{
			return this.Previous.InstrumentBreakStatement(original, rewritten);
		}

		// Token: 0x060022BF RID: 8895 RVA: 0x000B7047 File Offset: 0x000B5247
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override BoundStatement CreateBlockPrologue([global::System.Runtime.CompilerServices.Nullable(1)] BoundBlock original, out LocalSymbol synthesizedLocal)
		{
			return this.Previous.CreateBlockPrologue(original, out synthesizedLocal);
		}

		// Token: 0x060022C0 RID: 8896 RVA: 0x000B7056 File Offset: 0x000B5256
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundStatement CreateBlockEpilogue(BoundBlock original)
		{
			return this.Previous.CreateBlockEpilogue(original);
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x000B7064 File Offset: 0x000B5264
		public override BoundExpression InstrumentDoStatementCondition(BoundDoStatement original, BoundExpression rewrittenCondition, SyntheticBoundNodeFactory factory)
		{
			return this.Previous.InstrumentDoStatementCondition(original, rewrittenCondition, factory);
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x000B7074 File Offset: 0x000B5274
		public override BoundStatement InstrumentDoStatementConditionalGotoStart(BoundDoStatement original, BoundStatement ifConditionGotoStart)
		{
			return this.Previous.InstrumentDoStatementConditionalGotoStart(original, ifConditionGotoStart);
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x000B7083 File Offset: 0x000B5283
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override BoundStatement InstrumentForEachStatementCollectionVarDeclaration([global::System.Runtime.CompilerServices.Nullable(1)] BoundForEachStatement original, BoundStatement collectionVarDecl)
		{
			return this.Previous.InstrumentForEachStatementCollectionVarDeclaration(original, collectionVarDecl);
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x000B7092 File Offset: 0x000B5292
		public override BoundStatement InstrumentForEachStatement(BoundForEachStatement original, BoundStatement rewritten)
		{
			return this.Previous.InstrumentForEachStatement(original, rewritten);
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x000B70A1 File Offset: 0x000B52A1
		public override BoundStatement InstrumentForEachStatementIterationVarDeclaration(BoundForEachStatement original, BoundStatement iterationVarDecl)
		{
			return this.Previous.InstrumentForEachStatementIterationVarDeclaration(original, iterationVarDecl);
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x000B70B0 File Offset: 0x000B52B0
		public override BoundStatement InstrumentForStatementConditionalGotoStartOrBreak(BoundForStatement original, BoundStatement branchBack)
		{
			return this.Previous.InstrumentForStatementConditionalGotoStartOrBreak(original, branchBack);
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x000B70BF File Offset: 0x000B52BF
		public override BoundStatement InstrumentForEachStatementConditionalGotoStart(BoundForEachStatement original, BoundStatement branchBack)
		{
			return this.Previous.InstrumentForEachStatementConditionalGotoStart(original, branchBack);
		}

		// Token: 0x060022C8 RID: 8904 RVA: 0x000B70CE File Offset: 0x000B52CE
		public override BoundExpression InstrumentForStatementCondition(BoundForStatement original, BoundExpression rewrittenCondition, SyntheticBoundNodeFactory factory)
		{
			return this.Previous.InstrumentForStatementCondition(original, rewrittenCondition, factory);
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x000B70DE File Offset: 0x000B52DE
		public override BoundStatement InstrumentIfStatement(BoundIfStatement original, BoundStatement rewritten)
		{
			return this.Previous.InstrumentIfStatement(original, rewritten);
		}

		// Token: 0x060022CA RID: 8906 RVA: 0x000B70ED File Offset: 0x000B52ED
		public override BoundExpression InstrumentIfStatementCondition(BoundIfStatement original, BoundExpression rewrittenCondition, SyntheticBoundNodeFactory factory)
		{
			return this.Previous.InstrumentIfStatementCondition(original, rewrittenCondition, factory);
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x000B70FD File Offset: 0x000B52FD
		public override BoundStatement InstrumentLabelStatement(BoundLabeledStatement original, BoundStatement rewritten)
		{
			return this.Previous.InstrumentLabelStatement(original, rewritten);
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x000B710C File Offset: 0x000B530C
		public override BoundStatement InstrumentLocalInitialization(BoundLocalDeclaration original, BoundStatement rewritten)
		{
			return this.Previous.InstrumentLocalInitialization(original, rewritten);
		}

		// Token: 0x060022CD RID: 8909 RVA: 0x000B711B File Offset: 0x000B531B
		public override BoundStatement InstrumentLockTargetCapture(BoundLockStatement original, BoundStatement lockTargetCapture)
		{
			return this.Previous.InstrumentLockTargetCapture(original, lockTargetCapture);
		}

		// Token: 0x060022CE RID: 8910 RVA: 0x000B712A File Offset: 0x000B532A
		public override BoundStatement InstrumentReturnStatement(BoundReturnStatement original, BoundStatement rewritten)
		{
			return this.Previous.InstrumentReturnStatement(original, rewritten);
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x000B7139 File Offset: 0x000B5339
		public override BoundStatement InstrumentSwitchStatement(BoundSwitchStatement original, BoundStatement rewritten)
		{
			return this.Previous.InstrumentSwitchStatement(original, rewritten);
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x000B7148 File Offset: 0x000B5348
		public override BoundStatement InstrumentSwitchWhenClauseConditionalGotoBody(BoundExpression original, BoundStatement ifConditionGotoBody)
		{
			return this.Previous.InstrumentSwitchWhenClauseConditionalGotoBody(original, ifConditionGotoBody);
		}

		// Token: 0x060022D1 RID: 8913 RVA: 0x000B7157 File Offset: 0x000B5357
		public override BoundStatement InstrumentUsingTargetCapture(BoundUsingStatement original, BoundStatement usingTargetCapture)
		{
			return this.Previous.InstrumentUsingTargetCapture(original, usingTargetCapture);
		}

		// Token: 0x060022D2 RID: 8914 RVA: 0x000B7166 File Offset: 0x000B5366
		public override BoundExpression InstrumentWhileStatementCondition(BoundWhileStatement original, BoundExpression rewrittenCondition, SyntheticBoundNodeFactory factory)
		{
			return this.Previous.InstrumentWhileStatementCondition(original, rewrittenCondition, factory);
		}

		// Token: 0x060022D3 RID: 8915 RVA: 0x000B7176 File Offset: 0x000B5376
		public override BoundStatement InstrumentWhileStatementConditionalGotoStartOrBreak(BoundWhileStatement original, BoundStatement ifConditionGotoStart)
		{
			return this.Previous.InstrumentWhileStatementConditionalGotoStartOrBreak(original, ifConditionGotoStart);
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x000B7185 File Offset: 0x000B5385
		public override BoundExpression InstrumentCatchClauseFilter(BoundCatchBlock original, BoundExpression rewrittenFilter, SyntheticBoundNodeFactory factory)
		{
			return this.Previous.InstrumentCatchClauseFilter(original, rewrittenFilter, factory);
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x000B7195 File Offset: 0x000B5395
		public override BoundExpression InstrumentSwitchStatementExpression(BoundStatement original, BoundExpression rewrittenExpression, SyntheticBoundNodeFactory factory)
		{
			return this.Previous.InstrumentSwitchStatementExpression(original, rewrittenExpression, factory);
		}

		// Token: 0x060022D6 RID: 8918 RVA: 0x000B71A5 File Offset: 0x000B53A5
		public override BoundExpression InstrumentSwitchExpressionArmExpression(BoundExpression original, BoundExpression rewrittenExpression, SyntheticBoundNodeFactory factory)
		{
			return this.Previous.InstrumentSwitchExpressionArmExpression(original, rewrittenExpression, factory);
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x000B71B5 File Offset: 0x000B53B5
		public override BoundStatement InstrumentSwitchBindCasePatternVariables(BoundStatement bindings)
		{
			return this.Previous.InstrumentSwitchBindCasePatternVariables(bindings);
		}

		// Token: 0x060022D8 RID: 8920 RVA: 0x000B71C3 File Offset: 0x000B53C3
		public override BoundStatement InstrumentForEachStatementDeconstructionVariablesDeclaration(BoundForEachStatement original, BoundStatement iterationVarDecl)
		{
			return this.Previous.InstrumentForEachStatementDeconstructionVariablesDeclaration(original, iterationVarDecl);
		}
	}
}
