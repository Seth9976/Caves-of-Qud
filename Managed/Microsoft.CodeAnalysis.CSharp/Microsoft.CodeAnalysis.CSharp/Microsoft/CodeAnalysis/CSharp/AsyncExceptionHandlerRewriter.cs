using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001E9 RID: 489
	internal sealed class AsyncExceptionHandlerRewriter : BoundTreeRewriterWithStackGuardWithoutRecursionOnTheLeftOfBinaryOperator
	{
		// Token: 0x06002161 RID: 8545 RVA: 0x000ACFF1 File Offset: 0x000AB1F1
		private AsyncExceptionHandlerRewriter(MethodSymbol containingMethod, NamedTypeSymbol containingType, SyntheticBoundNodeFactory factory, AsyncExceptionHandlerRewriter.AwaitInFinallyAnalysis analysis)
		{
			this._F = factory;
			this._F.CurrentFunction = containingMethod;
			this._analysis = analysis;
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x000AD020 File Offset: 0x000AB220
		public static BoundStatement Rewrite(MethodSymbol containingSymbol, NamedTypeSymbol containingType, BoundStatement statement, TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
		{
			AsyncExceptionHandlerRewriter.AwaitInFinallyAnalysis awaitInFinallyAnalysis = new AsyncExceptionHandlerRewriter.AwaitInFinallyAnalysis(statement);
			if (!awaitInFinallyAnalysis.ContainsAwaitInHandlers())
			{
				return statement;
			}
			SyntheticBoundNodeFactory syntheticBoundNodeFactory = new SyntheticBoundNodeFactory(containingSymbol, statement.Syntax, compilationState, diagnostics);
			return (BoundStatement)new AsyncExceptionHandlerRewriter(containingSymbol, containingType, syntheticBoundNodeFactory, awaitInFinallyAnalysis).Visit(statement);
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x000AD064 File Offset: 0x000AB264
		public override BoundNode VisitTryStatement(BoundTryStatement node)
		{
			SyntaxNode syntax = node.Syntax;
			BoundStatement boundStatement;
			BoundBlock boundBlock;
			if (this._analysis.FinallyContainsAwaits(node))
			{
				AsyncExceptionHandlerRewriter.AwaitFinallyFrame awaitFinallyFrame = this.PushFrame(node);
				boundStatement = this.RewriteFinalizedRegion(node);
				boundBlock = (BoundBlock)this.VisitBlock(node.FinallyBlockOpt);
				this.PopFrame();
				NamedTypeSymbol namedTypeSymbol = this._F.SpecialType(SpecialType.System_Object);
				SynthesizedLocal synthesizedLocal = new SynthesizedLocal(this._F.CurrentFunction, TypeWithAnnotations.Create(namedTypeSymbol, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), SynthesizedLocalKind.TryAwaitPendingException, syntax, false, RefKind.None);
				GeneratedLabelSymbol generatedLabelSymbol = this._F.GenerateLabel("finallyLabel");
				SynthesizedLocal synthesizedLocal2 = new SynthesizedLocal(this._F.CurrentFunction, TypeWithAnnotations.Create(this._F.SpecialType(SpecialType.System_Int32), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), SynthesizedLocalKind.TryAwaitPendingBranch, syntax, false, RefKind.None);
				BoundCatchBlock boundCatchBlock = this._F.Catch(this._F.Local(synthesizedLocal), this._F.Block());
				BoundStatement boundStatement2 = this._F.Try(this._F.Block(new BoundStatement[]
				{
					boundStatement,
					this._F.HiddenSequencePoint(null),
					this._F.Goto(generatedLabelSymbol),
					this.PendBranches(awaitFinallyFrame, synthesizedLocal2, generatedLabelSymbol)
				}), ImmutableArray.Create<BoundCatchBlock>(boundCatchBlock), null, generatedLabelSymbol);
				BoundBlock boundBlock2 = this._F.Block(new BoundStatement[]
				{
					this._F.HiddenSequencePoint(null),
					this._F.Label(generatedLabelSymbol),
					boundBlock,
					this._F.HiddenSequencePoint(null),
					this.UnpendException(synthesizedLocal),
					this.UnpendBranches(awaitFinallyFrame, synthesizedLocal2, synthesizedLocal)
				});
				BoundStatement boundStatement3 = boundBlock2;
				if (this._F.CurrentFunction.IsAsync && this._F.CurrentFunction.IsIterator)
				{
					boundStatement3 = this._F.ExtractedFinallyBlock(boundBlock2);
				}
				ArrayBuilder<LocalSymbol> instance = ArrayBuilder<LocalSymbol>.GetInstance();
				ArrayBuilder<BoundStatement> instance2 = ArrayBuilder<BoundStatement>.GetInstance();
				instance2.Add(this._F.HiddenSequencePoint(null));
				instance.Add(synthesizedLocal);
				instance2.Add(this._F.Assignment(this._F.Local(synthesizedLocal), this._F.Default(synthesizedLocal.Type), false));
				instance.Add(synthesizedLocal2);
				instance2.Add(this._F.Assignment(this._F.Local(synthesizedLocal2), this._F.Default(synthesizedLocal2.Type), false));
				LocalSymbol returnValue = awaitFinallyFrame.returnValue;
				if (returnValue != null)
				{
					instance.Add(returnValue);
				}
				instance2.Add(boundStatement2);
				instance2.Add(boundStatement3);
				return this._F.Block(instance.ToImmutableAndFree(), instance2.ToImmutableAndFree());
			}
			boundStatement = this.RewriteFinalizedRegion(node);
			boundBlock = (BoundBlock)this.Visit(node.FinallyBlockOpt);
			if (boundBlock == null)
			{
				return boundStatement;
			}
			BoundTryStatement boundTryStatement = boundStatement as BoundTryStatement;
			if (boundTryStatement != null)
			{
				return boundTryStatement.Update(boundTryStatement.TryBlock, boundTryStatement.CatchBlocks, boundBlock, boundTryStatement.FinallyLabelOpt, boundTryStatement.PreferFaultHandler);
			}
			return this._F.Try((BoundBlock)boundStatement, ImmutableArray<BoundCatchBlock>.Empty, boundBlock, null);
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x000AD384 File Offset: 0x000AB584
		private BoundBlock PendBranches(AsyncExceptionHandlerRewriter.AwaitFinallyFrame frame, LocalSymbol pendingBranchVar, LabelSymbol finallyLabel)
		{
			ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance();
			List<LabelSymbol> proxiedLabels = frame.proxiedLabels;
			Dictionary<LabelSymbol, LabelSymbol> proxyLabels = frame.proxyLabels;
			int i = 1;
			if (proxiedLabels != null)
			{
				int count = proxiedLabels.Count;
				while (i <= count)
				{
					LabelSymbol labelSymbol = proxiedLabels[i - 1];
					LabelSymbol labelSymbol2 = proxyLabels[labelSymbol];
					this.PendBranch(instance, labelSymbol2, i, pendingBranchVar, finallyLabel);
					i++;
				}
			}
			GeneratedLabelSymbol returnProxyLabel = frame.returnProxyLabel;
			if (returnProxyLabel != null)
			{
				this.PendBranch(instance, returnProxyLabel, i, pendingBranchVar, finallyLabel);
			}
			return this._F.Block(instance.ToImmutableAndFree());
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x000AD410 File Offset: 0x000AB610
		private void PendBranch(ArrayBuilder<BoundStatement> bodyStatements, LabelSymbol proxy, int i, LocalSymbol pendingBranchVar, LabelSymbol finallyLabel)
		{
			bodyStatements.Add(this._F.Label(proxy));
			bodyStatements.Add(this._F.Assignment(this._F.Local(pendingBranchVar), this._F.Literal(i), false));
			bodyStatements.Add(this._F.Goto(finallyLabel));
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x000AD470 File Offset: 0x000AB670
		private BoundStatement UnpendBranches(AsyncExceptionHandlerRewriter.AwaitFinallyFrame frame, SynthesizedLocal pendingBranchVar, SynthesizedLocal pendingException)
		{
			AsyncExceptionHandlerRewriter.AwaitFinallyFrame parentOpt = frame.ParentOpt;
			List<LabelSymbol> proxiedLabels = frame.proxiedLabels;
			int i = 1;
			ArrayBuilder<SyntheticBoundNodeFactory.SyntheticSwitchSection> instance = ArrayBuilder<SyntheticBoundNodeFactory.SyntheticSwitchSection>.GetInstance();
			if (proxiedLabels != null)
			{
				int count = proxiedLabels.Count;
				while (i <= count)
				{
					LabelSymbol labelSymbol = proxiedLabels[i - 1];
					LabelSymbol labelSymbol2 = parentOpt.ProxyLabelIfNeeded(labelSymbol);
					SyntheticBoundNodeFactory.SyntheticSwitchSection syntheticSwitchSection = this._F.SwitchSection(i, new BoundStatement[] { this._F.Goto(labelSymbol2) });
					instance.Add(syntheticSwitchSection);
					i++;
				}
			}
			if (frame.returnProxyLabel != null)
			{
				BoundLocal boundLocal = null;
				if (frame.returnValue != null)
				{
					boundLocal = this._F.Local(frame.returnValue);
				}
				SynthesizedLocal synthesizedLocal;
				LabelSymbol labelSymbol3 = parentOpt.ProxyReturnIfNeeded(this._F.CurrentFunction, boundLocal, out synthesizedLocal);
				BoundStatement boundStatement;
				if (labelSymbol3 == null)
				{
					boundStatement = new BoundReturnStatement(this._F.Syntax, RefKind.None, boundLocal, false);
				}
				else if (boundLocal == null)
				{
					boundStatement = this._F.Goto(labelSymbol3);
				}
				else
				{
					boundStatement = this._F.Block(new BoundStatement[]
					{
						this._F.Assignment(this._F.Local(synthesizedLocal), boundLocal, false),
						this._F.Goto(labelSymbol3)
					});
				}
				SyntheticBoundNodeFactory.SyntheticSwitchSection syntheticSwitchSection2 = this._F.SwitchSection(i, new BoundStatement[] { boundStatement });
				instance.Add(syntheticSwitchSection2);
			}
			return this._F.Switch(this._F.Local(pendingBranchVar), instance.ToImmutableAndFree());
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x000AD5EC File Offset: 0x000AB7EC
		public override BoundNode VisitGotoStatement(BoundGotoStatement node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.CaseExpressionOpt);
			BoundLabel boundLabel = (BoundLabel)this.Visit(node.LabelExpressionOpt);
			LabelSymbol labelSymbol = this._currentAwaitFinallyFrame.ProxyLabelIfNeeded(node.Label);
			return node.Update(labelSymbol, boundExpression, boundLabel);
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x000AD638 File Offset: 0x000AB838
		public override BoundNode VisitConditionalGoto(BoundConditionalGoto node)
		{
			return base.VisitConditionalGoto(node);
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x000AD644 File Offset: 0x000AB844
		public override BoundNode VisitReturnStatement(BoundReturnStatement node)
		{
			SynthesizedLocal synthesizedLocal;
			LabelSymbol labelSymbol = this._currentAwaitFinallyFrame.ProxyReturnIfNeeded(this._F.CurrentFunction, node.ExpressionOpt, out synthesizedLocal);
			if (labelSymbol == null)
			{
				return base.VisitReturnStatement(node);
			}
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ExpressionOpt);
			if (boundExpression != null)
			{
				return this._F.Block(new BoundStatement[]
				{
					this._F.Assignment(this._F.Local(synthesizedLocal), boundExpression, false),
					this._F.Goto(labelSymbol)
				});
			}
			return this._F.Goto(labelSymbol);
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x000AD6E0 File Offset: 0x000AB8E0
		private BoundStatement UnpendException(LocalSymbol pendingExceptionLocal)
		{
			LocalSymbol localSymbol = this._F.SynthesizedLocal(this._F.SpecialType(SpecialType.System_Object), null, false, RefKind.None, SynthesizedLocalKind.LoweringTemp);
			BoundExpressionStatement boundExpressionStatement = this._F.Assignment(this._F.Local(localSymbol), this._F.Local(pendingExceptionLocal), false);
			BoundStatement boundStatement = this.Rethrow(localSymbol);
			return this._F.Block(ImmutableArray.Create<LocalSymbol>(localSymbol), new BoundStatement[]
			{
				boundExpressionStatement,
				this._F.If(this._F.ObjectNotEqual(this._F.Local(localSymbol), this._F.Null(localSymbol.Type)), boundStatement, null)
			});
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x000AD78C File Offset: 0x000AB98C
		private BoundStatement Rethrow(LocalSymbol obj)
		{
			BoundStatement boundStatement = this._F.Throw(this._F.Local(obj));
			MethodSymbol methodSymbol = this._F.WellKnownMethod(WellKnownMember.System_Runtime_ExceptionServices_ExceptionDispatchInfo__Capture, true);
			MethodSymbol methodSymbol2 = this._F.WellKnownMethod(WellKnownMember.System_Runtime_ExceptionServices_ExceptionDispatchInfo__Throw, true);
			if (methodSymbol != null && methodSymbol2 != null)
			{
				LocalSymbol localSymbol = this._F.SynthesizedLocal(this._F.WellKnownType(WellKnownType.System_Exception), null, false, RefKind.None, SynthesizedLocalKind.LoweringTemp);
				BoundExpressionStatement boundExpressionStatement = this._F.Assignment(this._F.Local(localSymbol), this._F.As(this._F.Local(obj), localSymbol.Type), false);
				boundStatement = this._F.Block(ImmutableArray.Create<LocalSymbol>(localSymbol), new BoundStatement[]
				{
					boundExpressionStatement,
					this._F.If(this._F.ObjectEqual(this._F.Local(localSymbol), this._F.Null(localSymbol.Type)), boundStatement, null),
					this._F.ExpressionStatement(this._F.Call(this._F.StaticCall(methodSymbol.ContainingType, methodSymbol, new BoundExpression[] { this._F.Local(localSymbol) }), methodSymbol2))
				});
			}
			return boundStatement;
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x000AD8D4 File Offset: 0x000ABAD4
		private BoundStatement RewriteFinalizedRegion(BoundTryStatement node)
		{
			BoundBlock boundBlock = (BoundBlock)this.VisitBlock(node.TryBlock);
			if (node.CatchBlocks.IsDefaultOrEmpty)
			{
				return boundBlock;
			}
			AsyncExceptionHandlerRewriter.AwaitCatchFrame currentAwaitCatchFrame = this._currentAwaitCatchFrame;
			this._currentAwaitCatchFrame = null;
			ImmutableArray<BoundCatchBlock> immutableArray = base.VisitList<BoundCatchBlock>(node.CatchBlocks);
			BoundStatement boundStatement = this._F.Try(boundBlock, immutableArray, null, null);
			AsyncExceptionHandlerRewriter.AwaitCatchFrame currentAwaitCatchFrame2 = this._currentAwaitCatchFrame;
			if (currentAwaitCatchFrame2 != null)
			{
				GeneratedLabelSymbol generatedLabelSymbol = this._F.GenerateLabel("handled");
				List<BoundBlock> handlers = currentAwaitCatchFrame2.handlers;
				ArrayBuilder<SyntheticBoundNodeFactory.SyntheticSwitchSection> instance = ArrayBuilder<SyntheticBoundNodeFactory.SyntheticSwitchSection>.GetInstance(handlers.Count);
				int i = 0;
				int count = handlers.Count;
				while (i < count)
				{
					instance.Add(this._F.SwitchSection(i + 1, new BoundStatement[] { this._F.Block(new BoundStatement[]
					{
						handlers[i],
						this._F.Goto(generatedLabelSymbol)
					}) }));
					i++;
				}
				boundStatement = this._F.Block(ImmutableArray.Create<LocalSymbol>(currentAwaitCatchFrame2.pendingCaughtException, currentAwaitCatchFrame2.pendingCatch).AddRange(currentAwaitCatchFrame2.GetHoistedLocals()), new BoundStatement[]
				{
					this._F.HiddenSequencePoint(null),
					this._F.Assignment(this._F.Local(currentAwaitCatchFrame2.pendingCatch), this._F.Default(currentAwaitCatchFrame2.pendingCatch.Type), false),
					boundStatement,
					this._F.HiddenSequencePoint(null),
					this._F.Switch(this._F.Local(currentAwaitCatchFrame2.pendingCatch), instance.ToImmutableAndFree()),
					this._F.HiddenSequencePoint(null),
					this._F.Label(generatedLabelSymbol)
				});
			}
			this._currentAwaitCatchFrame = currentAwaitCatchFrame;
			return boundStatement;
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x000ADAB0 File Offset: 0x000ABCB0
		public override BoundNode VisitCatchBlock(BoundCatchBlock node)
		{
			if (!this._analysis.CatchContainsAwait(node))
			{
				AsyncExceptionHandlerRewriter.AwaitCatchFrame currentAwaitCatchFrame = this._currentAwaitCatchFrame;
				this._currentAwaitCatchFrame = null;
				BoundNode boundNode = base.VisitCatchBlock(node);
				this._currentAwaitCatchFrame = currentAwaitCatchFrame;
				return boundNode;
			}
			AsyncExceptionHandlerRewriter.AwaitCatchFrame awaitCatchFrame = this._currentAwaitCatchFrame;
			if (awaitCatchFrame == null)
			{
				TryStatementSyntax tryStatementSyntax = (TryStatementSyntax)node.Syntax.Parent;
				awaitCatchFrame = (this._currentAwaitCatchFrame = new AsyncExceptionHandlerRewriter.AwaitCatchFrame(this._F, tryStatementSyntax));
			}
			TypeSymbol typeSymbol = node.ExceptionTypeOpt ?? this._F.SpecialType(SpecialType.System_Object);
			LocalSymbol localSymbol = this._F.SynthesizedLocal(typeSymbol, null, false, RefKind.None, SynthesizedLocalKind.LoweringTemp);
			BoundAssignmentOperator boundAssignmentOperator = this._F.AssignmentExpression(this._F.Local(awaitCatchFrame.pendingCaughtException), this._F.Convert(awaitCatchFrame.pendingCaughtException.Type, this._F.Local(localSymbol)), false);
			BoundExpressionStatement boundExpressionStatement = this._F.Assignment(this._F.Local(awaitCatchFrame.pendingCatch), this._F.Literal(awaitCatchFrame.handlers.Count + 1), false);
			BoundStatementList exceptionFilterPrologueOpt = node.ExceptionFilterPrologueOpt;
			BoundExpression exceptionFilterOpt = node.ExceptionFilterOpt;
			BoundCatchBlock boundCatchBlock;
			ImmutableArray<LocalSymbol> immutableArray;
			if (exceptionFilterOpt == null)
			{
				boundCatchBlock = node.Update(ImmutableArray.Create<LocalSymbol>(localSymbol), this._F.Local(localSymbol), typeSymbol, exceptionFilterPrologueOpt, null, this._F.Block(new BoundStatement[]
				{
					this._F.HiddenSequencePoint(null),
					this._F.ExpressionStatement(boundAssignmentOperator),
					boundExpressionStatement
				}), node.IsSynthesizedAsyncCatchAll);
				immutableArray = node.Locals;
			}
			else
			{
				immutableArray = ImmutableArray<LocalSymbol>.Empty;
				foreach (LocalSymbol localSymbol2 in node.Locals)
				{
					awaitCatchFrame.HoistLocal(localSymbol2, this._F);
				}
				BoundExpression exceptionSourceOpt = node.ExceptionSourceOpt;
				BoundStatementList boundStatementList = (BoundStatementList)this.Visit(exceptionFilterPrologueOpt);
				BoundExpression boundExpression = (BoundExpression)this.Visit(exceptionFilterOpt);
				BoundExpression boundExpression2 = ((exceptionSourceOpt == null) ? this._F.MakeSequence(new BoundExpression[] { boundAssignmentOperator, boundExpression }) : this._F.MakeSequence(new BoundExpression[]
				{
					boundAssignmentOperator,
					this.AssignCatchSource((BoundExpression)this.Visit(exceptionSourceOpt), awaitCatchFrame),
					boundExpression
				}));
				boundCatchBlock = node.Update(ImmutableArray.Create<LocalSymbol>(localSymbol), this._F.Local(localSymbol), typeSymbol, boundStatementList, boundExpression2, this._F.Block(new BoundStatement[]
				{
					this._F.HiddenSequencePoint(null),
					boundExpressionStatement
				}), node.IsSynthesizedAsyncCatchAll);
			}
			ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance();
			instance.Add(this._F.HiddenSequencePoint(null));
			if (exceptionFilterOpt == null)
			{
				BoundExpression exceptionSourceOpt2 = node.ExceptionSourceOpt;
				if (exceptionSourceOpt2 != null)
				{
					BoundExpression boundExpression3 = this.AssignCatchSource((BoundExpression)this.Visit(exceptionSourceOpt2), awaitCatchFrame);
					instance.Add(this._F.ExpressionStatement(boundExpression3));
				}
			}
			instance.Add((BoundStatement)this.Visit(node.Body));
			BoundBlock boundBlock = this._F.Block(immutableArray, instance.ToImmutableAndFree());
			awaitCatchFrame.handlers.Add(boundBlock);
			return boundCatchBlock;
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x000ADDC8 File Offset: 0x000ABFC8
		private BoundExpression AssignCatchSource(BoundExpression rewrittenSource, AsyncExceptionHandlerRewriter.AwaitCatchFrame currentAwaitCatchFrame)
		{
			BoundExpression boundExpression = null;
			if (rewrittenSource != null)
			{
				boundExpression = this._F.AssignmentExpression(rewrittenSource, this._F.Convert(rewrittenSource.Type, this._F.Local(currentAwaitCatchFrame.pendingCaughtException)), false);
			}
			return boundExpression;
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x000ADE0C File Offset: 0x000AC00C
		public override BoundNode VisitLocal(BoundLocal node)
		{
			AsyncExceptionHandlerRewriter.AwaitCatchFrame currentAwaitCatchFrame = this._currentAwaitCatchFrame;
			LocalSymbol localSymbol;
			if (currentAwaitCatchFrame == null || !currentAwaitCatchFrame.TryGetHoistedLocal(node.LocalSymbol, out localSymbol))
			{
				return base.VisitLocal(node);
			}
			return node.Update(localSymbol, node.ConstantValueOpt, localSymbol.Type);
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x000ADE4E File Offset: 0x000AC04E
		public override BoundNode VisitThrowStatement(BoundThrowStatement node)
		{
			if (node.ExpressionOpt != null || this._currentAwaitCatchFrame == null)
			{
				return base.VisitThrowStatement(node);
			}
			return this.Rethrow(this._currentAwaitCatchFrame.pendingCaughtException);
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x000ADE7C File Offset: 0x000AC07C
		public override BoundNode VisitLambda(BoundLambda node)
		{
			MethodSymbol currentFunction = this._F.CurrentFunction;
			AsyncExceptionHandlerRewriter.AwaitFinallyFrame currentAwaitFinallyFrame = this._currentAwaitFinallyFrame;
			this._F.CurrentFunction = node.Symbol;
			this._currentAwaitFinallyFrame = new AsyncExceptionHandlerRewriter.AwaitFinallyFrame();
			BoundNode boundNode = base.VisitLambda(node);
			this._F.CurrentFunction = currentFunction;
			this._currentAwaitFinallyFrame = currentAwaitFinallyFrame;
			return boundNode;
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x000ADED4 File Offset: 0x000AC0D4
		public override BoundNode VisitLocalFunctionStatement(BoundLocalFunctionStatement node)
		{
			MethodSymbol currentFunction = this._F.CurrentFunction;
			AsyncExceptionHandlerRewriter.AwaitFinallyFrame currentAwaitFinallyFrame = this._currentAwaitFinallyFrame;
			this._F.CurrentFunction = node.Symbol;
			this._currentAwaitFinallyFrame = new AsyncExceptionHandlerRewriter.AwaitFinallyFrame();
			BoundNode boundNode = base.VisitLocalFunctionStatement(node);
			this._F.CurrentFunction = currentFunction;
			this._currentAwaitFinallyFrame = currentAwaitFinallyFrame;
			return boundNode;
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x000ADF2C File Offset: 0x000AC12C
		private AsyncExceptionHandlerRewriter.AwaitFinallyFrame PushFrame(BoundTryStatement statement)
		{
			AsyncExceptionHandlerRewriter.AwaitFinallyFrame awaitFinallyFrame = new AsyncExceptionHandlerRewriter.AwaitFinallyFrame(this._currentAwaitFinallyFrame, this._analysis.Labels(statement), (StatementSyntax)statement.Syntax);
			this._currentAwaitFinallyFrame = awaitFinallyFrame;
			return awaitFinallyFrame;
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x000ADF64 File Offset: 0x000AC164
		private void PopFrame()
		{
			AsyncExceptionHandlerRewriter.AwaitFinallyFrame currentAwaitFinallyFrame = this._currentAwaitFinallyFrame;
			this._currentAwaitFinallyFrame = currentAwaitFinallyFrame.ParentOpt;
		}

		// Token: 0x0400103B RID: 4155
		private readonly SyntheticBoundNodeFactory _F;

		// Token: 0x0400103C RID: 4156
		private readonly AsyncExceptionHandlerRewriter.AwaitInFinallyAnalysis _analysis;

		// Token: 0x0400103D RID: 4157
		private AsyncExceptionHandlerRewriter.AwaitCatchFrame _currentAwaitCatchFrame;

		// Token: 0x0400103E RID: 4158
		private AsyncExceptionHandlerRewriter.AwaitFinallyFrame _currentAwaitFinallyFrame = new AsyncExceptionHandlerRewriter.AwaitFinallyFrame();

		// Token: 0x02000714 RID: 1812
		private sealed class AwaitInFinallyAnalysis : LabelCollector
		{
			// Token: 0x0600857D RID: 34173 RVA: 0x001E28A5 File Offset: 0x001E0AA5
			public AwaitInFinallyAnalysis(BoundStatement body)
			{
				this._seenAwait = false;
				this.Visit(body);
			}

			// Token: 0x0600857E RID: 34174 RVA: 0x001E28BC File Offset: 0x001E0ABC
			public bool FinallyContainsAwaits(BoundTryStatement statement)
			{
				return this._labelsInInterestingTry != null && this._labelsInInterestingTry.ContainsKey(statement);
			}

			// Token: 0x0600857F RID: 34175 RVA: 0x001E28D4 File Offset: 0x001E0AD4
			internal bool CatchContainsAwait(BoundCatchBlock node)
			{
				return this._awaitContainingCatches != null && this._awaitContainingCatches.Contains(node);
			}

			// Token: 0x06008580 RID: 34176 RVA: 0x001E28EC File Offset: 0x001E0AEC
			public bool ContainsAwaitInHandlers()
			{
				return this._labelsInInterestingTry != null || this._awaitContainingCatches != null;
			}

			// Token: 0x06008581 RID: 34177 RVA: 0x001E2901 File Offset: 0x001E0B01
			internal HashSet<LabelSymbol> Labels(BoundTryStatement statement)
			{
				return this._labelsInInterestingTry[statement];
			}

			// Token: 0x06008582 RID: 34178 RVA: 0x001E2910 File Offset: 0x001E0B10
			public override BoundNode VisitTryStatement(BoundTryStatement node)
			{
				HashSet<LabelSymbol> currentLabels = this.currentLabels;
				this.currentLabels = null;
				this.Visit(node.TryBlock);
				base.VisitList<BoundCatchBlock>(node.CatchBlocks);
				bool seenAwait = this._seenAwait;
				this._seenAwait = false;
				this.Visit(node.FinallyBlockOpt);
				if (this._seenAwait)
				{
					Dictionary<BoundTryStatement, HashSet<LabelSymbol>> dictionary = this._labelsInInterestingTry;
					if (dictionary == null)
					{
						dictionary = (this._labelsInInterestingTry = new Dictionary<BoundTryStatement, HashSet<LabelSymbol>>());
					}
					dictionary.Add(node, this.currentLabels);
					this.currentLabels = currentLabels;
				}
				else if (this.currentLabels == null)
				{
					this.currentLabels = currentLabels;
				}
				else if (currentLabels != null)
				{
					this.currentLabels.UnionWith(currentLabels);
				}
				this._seenAwait = this._seenAwait || seenAwait;
				return null;
			}

			// Token: 0x06008583 RID: 34179 RVA: 0x001E29C4 File Offset: 0x001E0BC4
			public override BoundNode VisitCatchBlock(BoundCatchBlock node)
			{
				bool seenAwait = this._seenAwait;
				this._seenAwait = false;
				BoundNode boundNode = base.VisitCatchBlock(node);
				if (this._seenAwait)
				{
					if (this._awaitContainingCatches == null)
					{
						this._awaitContainingCatches = new HashSet<BoundCatchBlock>();
					}
					this._awaitContainingCatches.Add(node);
				}
				this._seenAwait = this._seenAwait || seenAwait;
				return boundNode;
			}

			// Token: 0x06008584 RID: 34180 RVA: 0x001E2A20 File Offset: 0x001E0C20
			public override BoundNode VisitAwaitExpression(BoundAwaitExpression node)
			{
				this._seenAwait = true;
				return base.VisitAwaitExpression(node);
			}

			// Token: 0x06008585 RID: 34181 RVA: 0x001E2A30 File Offset: 0x001E0C30
			public override BoundNode VisitLambda(BoundLambda node)
			{
				HashSet<LabelSymbol> currentLabels = this.currentLabels;
				bool seenAwait = this._seenAwait;
				this.currentLabels = null;
				this._seenAwait = false;
				base.VisitLambda(node);
				this.currentLabels = currentLabels;
				this._seenAwait = seenAwait;
				return null;
			}

			// Token: 0x06008586 RID: 34182 RVA: 0x001E2A70 File Offset: 0x001E0C70
			public override BoundNode VisitLocalFunctionStatement(BoundLocalFunctionStatement node)
			{
				HashSet<LabelSymbol> currentLabels = this.currentLabels;
				bool seenAwait = this._seenAwait;
				this.currentLabels = null;
				this._seenAwait = false;
				base.VisitLocalFunctionStatement(node);
				this.currentLabels = currentLabels;
				this._seenAwait = seenAwait;
				return null;
			}

			// Token: 0x04002274 RID: 8820
			private Dictionary<BoundTryStatement, HashSet<LabelSymbol>> _labelsInInterestingTry;

			// Token: 0x04002275 RID: 8821
			private HashSet<BoundCatchBlock> _awaitContainingCatches;

			// Token: 0x04002276 RID: 8822
			private bool _seenAwait;
		}

		// Token: 0x02000715 RID: 1813
		private sealed class AwaitFinallyFrame
		{
			// Token: 0x06008587 RID: 34183 RVA: 0x001E2AB0 File Offset: 0x001E0CB0
			public AwaitFinallyFrame()
			{
			}

			// Token: 0x06008588 RID: 34184 RVA: 0x001E2AB8 File Offset: 0x001E0CB8
			public AwaitFinallyFrame(AsyncExceptionHandlerRewriter.AwaitFinallyFrame parent, HashSet<LabelSymbol> labelsOpt, StatementSyntax statementSyntax)
			{
				this.ParentOpt = parent;
				this.LabelsOpt = labelsOpt;
				this._statementSyntaxOpt = statementSyntax;
			}

			// Token: 0x06008589 RID: 34185 RVA: 0x001E2AD5 File Offset: 0x001E0CD5
			public bool IsRoot()
			{
				return this.ParentOpt == null;
			}

			// Token: 0x0600858A RID: 34186 RVA: 0x001E2AE0 File Offset: 0x001E0CE0
			public LabelSymbol ProxyLabelIfNeeded(LabelSymbol label)
			{
				if (this.IsRoot() || (this.LabelsOpt != null && this.LabelsOpt.Contains(label)))
				{
					return label;
				}
				Dictionary<LabelSymbol, LabelSymbol> dictionary = this.proxyLabels;
				List<LabelSymbol> list = this.proxiedLabels;
				if (dictionary == null)
				{
					dictionary = (this.proxyLabels = new Dictionary<LabelSymbol, LabelSymbol>());
					list = (this.proxiedLabels = new List<LabelSymbol>());
				}
				LabelSymbol labelSymbol;
				if (!dictionary.TryGetValue(label, ref labelSymbol))
				{
					labelSymbol = new GeneratedLabelSymbol("proxy" + label.Name);
					dictionary.Add(label, labelSymbol);
					list.Add(label);
				}
				return labelSymbol;
			}

			// Token: 0x0600858B RID: 34187 RVA: 0x001E2B6C File Offset: 0x001E0D6C
			public LabelSymbol ProxyReturnIfNeeded(MethodSymbol containingMethod, BoundExpression valueOpt, out SynthesizedLocal returnValue)
			{
				returnValue = null;
				if (this.IsRoot())
				{
					return null;
				}
				GeneratedLabelSymbol generatedLabelSymbol = this.returnProxyLabel;
				if (generatedLabelSymbol == null)
				{
					generatedLabelSymbol = (this.returnProxyLabel = new GeneratedLabelSymbol("returnProxy"));
				}
				if (valueOpt != null)
				{
					returnValue = this.returnValue;
					if (returnValue == null)
					{
						SynthesizedLocal synthesizedLocal;
						returnValue = (synthesizedLocal = new SynthesizedLocal(containingMethod, TypeWithAnnotations.Create(valueOpt.Type, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), SynthesizedLocalKind.AsyncMethodReturnValue, this._statementSyntaxOpt, false, RefKind.None));
						this.returnValue = synthesizedLocal;
					}
				}
				return generatedLabelSymbol;
			}

			// Token: 0x04002277 RID: 8823
			public readonly AsyncExceptionHandlerRewriter.AwaitFinallyFrame ParentOpt;

			// Token: 0x04002278 RID: 8824
			public readonly HashSet<LabelSymbol> LabelsOpt;

			// Token: 0x04002279 RID: 8825
			private readonly StatementSyntax _statementSyntaxOpt;

			// Token: 0x0400227A RID: 8826
			public Dictionary<LabelSymbol, LabelSymbol> proxyLabels;

			// Token: 0x0400227B RID: 8827
			public List<LabelSymbol> proxiedLabels;

			// Token: 0x0400227C RID: 8828
			public GeneratedLabelSymbol returnProxyLabel;

			// Token: 0x0400227D RID: 8829
			public SynthesizedLocal returnValue;
		}

		// Token: 0x02000716 RID: 1814
		private sealed class AwaitCatchFrame
		{
			// Token: 0x0600858C RID: 34188 RVA: 0x001E2BF0 File Offset: 0x001E0DF0
			public AwaitCatchFrame(SyntheticBoundNodeFactory F, TryStatementSyntax tryStatementSyntax)
			{
				this.pendingCaughtException = new SynthesizedLocal(F.CurrentFunction, TypeWithAnnotations.Create(F.SpecialType(SpecialType.System_Object), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), SynthesizedLocalKind.TryAwaitPendingCaughtException, tryStatementSyntax, false, RefKind.None);
				this.pendingCatch = new SynthesizedLocal(F.CurrentFunction, TypeWithAnnotations.Create(F.SpecialType(SpecialType.System_Int32), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), SynthesizedLocalKind.TryAwaitPendingCatch, tryStatementSyntax, false, RefKind.None);
				this.handlers = new List<BoundBlock>();
				this._hoistedLocals = new Dictionary<LocalSymbol, LocalSymbol>();
				this._orderedHoistedLocals = new List<LocalSymbol>();
			}

			// Token: 0x0600858D RID: 34189 RVA: 0x001E2C80 File Offset: 0x001E0E80
			public void HoistLocal(LocalSymbol local, SyntheticBoundNodeFactory F)
			{
				if (!Enumerable.Any<LocalSymbol>(this._hoistedLocals.Keys, (LocalSymbol l) => l.Name == local.Name && TypeSymbol.Equals(l.Type, local.Type, TypeCompareKind.ConsiderEverything)))
				{
					this._hoistedLocals.Add(local, local);
					this._orderedHoistedLocals.Add(local);
					return;
				}
				LocalSymbol localSymbol = F.SynthesizedLocal(local.Type, this.pendingCatch.SyntaxOpt, false, RefKind.None, SynthesizedLocalKind.ExceptionFilterAwaitHoistedExceptionLocal);
				this._hoistedLocals.Add(local, localSymbol);
				this._orderedHoistedLocals.Add(localSymbol);
			}

			// Token: 0x0600858E RID: 34190 RVA: 0x001E2D20 File Offset: 0x001E0F20
			public IEnumerable<LocalSymbol> GetHoistedLocals()
			{
				return this._orderedHoistedLocals;
			}

			// Token: 0x0600858F RID: 34191 RVA: 0x001E2D28 File Offset: 0x001E0F28
			public bool TryGetHoistedLocal(LocalSymbol originalLocal, out LocalSymbol hoistedLocal)
			{
				return this._hoistedLocals.TryGetValue(originalLocal, ref hoistedLocal);
			}

			// Token: 0x0400227E RID: 8830
			public readonly SynthesizedLocal pendingCaughtException;

			// Token: 0x0400227F RID: 8831
			public readonly SynthesizedLocal pendingCatch;

			// Token: 0x04002280 RID: 8832
			public readonly List<BoundBlock> handlers;

			// Token: 0x04002281 RID: 8833
			private readonly Dictionary<LocalSymbol, LocalSymbol> _hoistedLocals;

			// Token: 0x04002282 RID: 8834
			private readonly List<LocalSymbol> _orderedHoistedLocals;
		}
	}
}
