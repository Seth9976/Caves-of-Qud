using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001FC RID: 508
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class Instrumenter
	{
		// Token: 0x06002331 RID: 9009 RVA: 0x000B8B27 File Offset: 0x000B6D27
		private static BoundStatement InstrumentStatement(BoundStatement original, BoundStatement rewritten)
		{
			return rewritten;
		}

		// Token: 0x06002332 RID: 9010 RVA: 0x000B8B2A File Offset: 0x000B6D2A
		public virtual BoundStatement InstrumentNoOpStatement(BoundNoOpStatement original, BoundStatement rewritten)
		{
			return Instrumenter.InstrumentStatement(original, rewritten);
		}

		// Token: 0x06002333 RID: 9011 RVA: 0x000B8B33 File Offset: 0x000B6D33
		public virtual BoundStatement InstrumentYieldBreakStatement(BoundYieldBreakStatement original, BoundStatement rewritten)
		{
			return rewritten;
		}

		// Token: 0x06002334 RID: 9012 RVA: 0x000B8B36 File Offset: 0x000B6D36
		public virtual BoundStatement InstrumentYieldReturnStatement(BoundYieldReturnStatement original, BoundStatement rewritten)
		{
			return Instrumenter.InstrumentStatement(original, rewritten);
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x000B8B3F File Offset: 0x000B6D3F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public virtual BoundStatement CreateBlockPrologue([global::System.Runtime.CompilerServices.Nullable(1)] BoundBlock original, out LocalSymbol synthesizedLocal)
		{
			synthesizedLocal = null;
			return null;
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x000B8B45 File Offset: 0x000B6D45
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundStatement CreateBlockEpilogue(BoundBlock original)
		{
			return null;
		}

		// Token: 0x06002337 RID: 9015 RVA: 0x000B8B48 File Offset: 0x000B6D48
		public virtual BoundStatement InstrumentThrowStatement(BoundThrowStatement original, BoundStatement rewritten)
		{
			return Instrumenter.InstrumentStatement(original, rewritten);
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x000B8B51 File Offset: 0x000B6D51
		public virtual BoundStatement InstrumentContinueStatement(BoundContinueStatement original, BoundStatement rewritten)
		{
			return Instrumenter.InstrumentStatement(original, rewritten);
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x000B8B5A File Offset: 0x000B6D5A
		public virtual BoundStatement InstrumentGotoStatement(BoundGotoStatement original, BoundStatement rewritten)
		{
			return Instrumenter.InstrumentStatement(original, rewritten);
		}

		// Token: 0x0600233A RID: 9018 RVA: 0x000B8B63 File Offset: 0x000B6D63
		public virtual BoundStatement InstrumentExpressionStatement(BoundExpressionStatement original, BoundStatement rewritten)
		{
			return Instrumenter.InstrumentStatement(original, rewritten);
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x000B8B6C File Offset: 0x000B6D6C
		public virtual BoundStatement InstrumentFieldOrPropertyInitializer(BoundStatement original, BoundStatement rewritten)
		{
			return Instrumenter.InstrumentStatement(original, rewritten);
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x000B8B75 File Offset: 0x000B6D75
		public virtual BoundStatement InstrumentBreakStatement(BoundBreakStatement original, BoundStatement rewritten)
		{
			return Instrumenter.InstrumentStatement(original, rewritten);
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x000B8B7E File Offset: 0x000B6D7E
		public virtual BoundExpression InstrumentDoStatementCondition(BoundDoStatement original, BoundExpression rewrittenCondition, SyntheticBoundNodeFactory factory)
		{
			return rewrittenCondition;
		}

		// Token: 0x0600233E RID: 9022 RVA: 0x000B8B81 File Offset: 0x000B6D81
		public virtual BoundExpression InstrumentWhileStatementCondition(BoundWhileStatement original, BoundExpression rewrittenCondition, SyntheticBoundNodeFactory factory)
		{
			return rewrittenCondition;
		}

		// Token: 0x0600233F RID: 9023 RVA: 0x000B8B84 File Offset: 0x000B6D84
		public virtual BoundStatement InstrumentDoStatementConditionalGotoStart(BoundDoStatement original, BoundStatement ifConditionGotoStart)
		{
			return ifConditionGotoStart;
		}

		// Token: 0x06002340 RID: 9024 RVA: 0x000B8B87 File Offset: 0x000B6D87
		public virtual BoundStatement InstrumentWhileStatementConditionalGotoStartOrBreak(BoundWhileStatement original, BoundStatement ifConditionGotoStart)
		{
			return ifConditionGotoStart;
		}

		// Token: 0x06002341 RID: 9025 RVA: 0x000B8B8A File Offset: 0x000B6D8A
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("collectionVarDecl")]
		public virtual BoundStatement InstrumentForEachStatementCollectionVarDeclaration([global::System.Runtime.CompilerServices.Nullable(1)] BoundForEachStatement original, BoundStatement collectionVarDecl)
		{
			return collectionVarDecl;
		}

		// Token: 0x06002342 RID: 9026 RVA: 0x000B8B8D File Offset: 0x000B6D8D
		public virtual BoundStatement InstrumentForEachStatement(BoundForEachStatement original, BoundStatement rewritten)
		{
			return Instrumenter.InstrumentStatement(original, rewritten);
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x000B8B96 File Offset: 0x000B6D96
		public virtual BoundStatement InstrumentForEachStatementIterationVarDeclaration(BoundForEachStatement original, BoundStatement iterationVarDecl)
		{
			return iterationVarDecl;
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x000B8B99 File Offset: 0x000B6D99
		public virtual BoundStatement InstrumentForEachStatementDeconstructionVariablesDeclaration(BoundForEachStatement original, BoundStatement iterationVarDecl)
		{
			return iterationVarDecl;
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x000B8B9C File Offset: 0x000B6D9C
		public virtual BoundStatement InstrumentForEachStatementConditionalGotoStart(BoundForEachStatement original, BoundStatement branchBack)
		{
			return branchBack;
		}

		// Token: 0x06002346 RID: 9030 RVA: 0x000B8B9F File Offset: 0x000B6D9F
		public virtual BoundStatement InstrumentForStatementConditionalGotoStartOrBreak(BoundForStatement original, BoundStatement branchBack)
		{
			return branchBack;
		}

		// Token: 0x06002347 RID: 9031 RVA: 0x000B8BA2 File Offset: 0x000B6DA2
		public virtual BoundExpression InstrumentForStatementCondition(BoundForStatement original, BoundExpression rewrittenCondition, SyntheticBoundNodeFactory factory)
		{
			return rewrittenCondition;
		}

		// Token: 0x06002348 RID: 9032 RVA: 0x000B8BA5 File Offset: 0x000B6DA5
		public virtual BoundStatement InstrumentIfStatement(BoundIfStatement original, BoundStatement rewritten)
		{
			return Instrumenter.InstrumentStatement(original, rewritten);
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x000B8BAE File Offset: 0x000B6DAE
		public virtual BoundExpression InstrumentIfStatementCondition(BoundIfStatement original, BoundExpression rewrittenCondition, SyntheticBoundNodeFactory factory)
		{
			return rewrittenCondition;
		}

		// Token: 0x0600234A RID: 9034 RVA: 0x000B8BB1 File Offset: 0x000B6DB1
		public virtual BoundStatement InstrumentLabelStatement(BoundLabeledStatement original, BoundStatement rewritten)
		{
			return Instrumenter.InstrumentStatement(original, rewritten);
		}

		// Token: 0x0600234B RID: 9035 RVA: 0x000B8BBA File Offset: 0x000B6DBA
		public virtual BoundStatement InstrumentLocalInitialization(BoundLocalDeclaration original, BoundStatement rewritten)
		{
			return Instrumenter.InstrumentStatement(original, rewritten);
		}

		// Token: 0x0600234C RID: 9036 RVA: 0x000B8BC3 File Offset: 0x000B6DC3
		public virtual BoundStatement InstrumentLockTargetCapture(BoundLockStatement original, BoundStatement lockTargetCapture)
		{
			return lockTargetCapture;
		}

		// Token: 0x0600234D RID: 9037 RVA: 0x000B8BC6 File Offset: 0x000B6DC6
		public virtual BoundStatement InstrumentReturnStatement(BoundReturnStatement original, BoundStatement rewritten)
		{
			return rewritten;
		}

		// Token: 0x0600234E RID: 9038 RVA: 0x000B8BC9 File Offset: 0x000B6DC9
		public virtual BoundStatement InstrumentSwitchStatement(BoundSwitchStatement original, BoundStatement rewritten)
		{
			return Instrumenter.InstrumentStatement(original, rewritten);
		}

		// Token: 0x0600234F RID: 9039 RVA: 0x000B8BD2 File Offset: 0x000B6DD2
		public virtual BoundStatement InstrumentSwitchWhenClauseConditionalGotoBody(BoundExpression original, BoundStatement ifConditionGotoBody)
		{
			return ifConditionGotoBody;
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x000B8BD5 File Offset: 0x000B6DD5
		public virtual BoundStatement InstrumentUsingTargetCapture(BoundUsingStatement original, BoundStatement usingTargetCapture)
		{
			return usingTargetCapture;
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x000B8BD8 File Offset: 0x000B6DD8
		public virtual BoundExpression InstrumentCatchClauseFilter(BoundCatchBlock original, BoundExpression rewrittenFilter, SyntheticBoundNodeFactory factory)
		{
			return rewrittenFilter;
		}

		// Token: 0x06002352 RID: 9042 RVA: 0x000B8BDB File Offset: 0x000B6DDB
		public virtual BoundExpression InstrumentSwitchStatementExpression(BoundStatement original, BoundExpression rewrittenExpression, SyntheticBoundNodeFactory factory)
		{
			return rewrittenExpression;
		}

		// Token: 0x06002353 RID: 9043 RVA: 0x000B8BDE File Offset: 0x000B6DDE
		public virtual BoundExpression InstrumentSwitchExpressionArmExpression(BoundExpression original, BoundExpression rewrittenExpression, SyntheticBoundNodeFactory factory)
		{
			return rewrittenExpression;
		}

		// Token: 0x06002354 RID: 9044 RVA: 0x000B8BE1 File Offset: 0x000B6DE1
		public virtual BoundStatement InstrumentSwitchBindCasePatternVariables(BoundStatement bindings)
		{
			return bindings;
		}

		// Token: 0x040010B4 RID: 4276
		public static readonly Instrumenter NoOp = new Instrumenter();
	}
}
