using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001EB RID: 491
	internal sealed class AsyncIteratorMethodToStateMachineRewriter : AsyncMethodToStateMachineRewriter
	{
		// Token: 0x0600217C RID: 8572 RVA: 0x000ADFEC File Offset: 0x000AC1EC
		internal AsyncIteratorMethodToStateMachineRewriter(MethodSymbol method, int methodOrdinal, AsyncMethodBuilderMemberCollection asyncMethodBuilderMemberCollection, AsyncIteratorInfo asyncIteratorInfo, SyntheticBoundNodeFactory F, FieldSymbol state, FieldSymbol builder, IReadOnlySet<Symbol> hoistedVariables, IReadOnlyDictionary<Symbol, CapturedSymbolReplacement> nonReusableLocalProxies, SynthesizedLocalOrdinalsDispenser synthesizedLocalOrdinals, VariableSlotAllocator slotAllocatorOpt, int nextFreeHoistedLocalSlot, BindingDiagnosticBag diagnostics)
			: base(method, methodOrdinal, asyncMethodBuilderMemberCollection, F, state, builder, hoistedVariables, nonReusableLocalProxies, synthesizedLocalOrdinals, slotAllocatorOpt, nextFreeHoistedLocalSlot, diagnostics)
		{
			this._asyncIteratorInfo = asyncIteratorInfo;
			this._currentDisposalLabel = this._exprReturnLabel;
			this._exprReturnLabelTrue = F.GenerateLabel("yieldReturn");
		}

		// Token: 0x0600217D RID: 8573 RVA: 0x000AE044 File Offset: 0x000AC244
		protected override BoundStatement GenerateSetResultCall()
		{
			ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance();
			this.AddDisposeCombinedTokensIfNeeded(instance);
			instance.AddRange(new BoundStatement[]
			{
				this.GenerateCompleteOnBuilder(),
				this.<GenerateSetResultCall>g__generateSetResultOnPromise|5_0(false),
				this.F.Return(null),
				this.F.Label(this._exprReturnLabelTrue),
				this.<GenerateSetResultCall>g__generateSetResultOnPromise|5_0(true)
			});
			return this.F.Block(instance.ToImmutableAndFree());
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x000AE0BC File Offset: 0x000AC2BC
		private BoundExpressionStatement GenerateCompleteOnBuilder()
		{
			return this.F.ExpressionStatement(this.F.Call(this.F.Field(this.F.This(), this._asyncMethodBuilderField), this._asyncMethodBuilderMemberCollection.SetResult, ImmutableArray<BoundExpression>.Empty));
		}

		// Token: 0x0600217F RID: 8575 RVA: 0x000AE10C File Offset: 0x000AC30C
		private void AddDisposeCombinedTokensIfNeeded(ArrayBuilder<BoundStatement> builder)
		{
			if (this._asyncIteratorInfo.CombinedTokensField != null)
			{
				BoundFieldAccess boundFieldAccess = this.F.Field(this.F.This(), this._asyncIteratorInfo.CombinedTokensField);
				TypeSymbol type = boundFieldAccess.Type;
				builder.Add(this.F.If(this.F.ObjectNotEqual(boundFieldAccess, this.F.Null(type)), this.F.Block(new BoundStatement[]
				{
					this.F.ExpressionStatement(this.F.Call(boundFieldAccess, this.F.WellKnownMethod(WellKnownMember.System_Threading_CancellationTokenSource__Dispose))),
					this.F.Assignment(boundFieldAccess, this.F.Null(type), false)
				}), null));
			}
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x000AE1D4 File Offset: 0x000AC3D4
		protected override BoundStatement GenerateSetExceptionCall(LocalSymbol exceptionLocal)
		{
			ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance();
			this.AddDisposeCombinedTokensIfNeeded(instance);
			instance.Add(this.GenerateCompleteOnBuilder());
			instance.Add(this.F.ExpressionStatement(this.F.Call(this.F.InstanceField(this._asyncIteratorInfo.PromiseOfValueOrEndField), this._asyncIteratorInfo.SetExceptionMethod, this.F.Local(exceptionLocal))));
			return this.F.Block(instance.ToImmutableAndFree());
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x000AE254 File Offset: 0x000AC454
		private BoundStatement GenerateJumpToCurrentDisposalLabel()
		{
			return this.F.If(this.F.InstanceField(this._asyncIteratorInfo.DisposeModeField), this.F.Goto(this._currentDisposalLabel), null);
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x000AE289 File Offset: 0x000AC489
		private BoundStatement AppendJumpToCurrentDisposalLabel(BoundStatement node)
		{
			return this.F.Block(new BoundStatement[]
			{
				node,
				this.GenerateJumpToCurrentDisposalLabel()
			});
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x000AE2A9 File Offset: 0x000AC4A9
		protected override BoundBinaryOperator ShouldEnterFinallyBlock()
		{
			return this.F.IntEqual(this.F.Local(this.cachedState), this.F.Literal(-1));
		}

		// Token: 0x06002184 RID: 8580 RVA: 0x000AE2D4 File Offset: 0x000AC4D4
		protected override BoundStatement VisitBody(BoundStatement body)
		{
			int nextYieldReturnState = this._nextYieldReturnState;
			this._nextYieldReturnState = nextYieldReturnState - 1;
			int num = nextYieldReturnState;
			GeneratedLabelSymbol generatedLabelSymbol;
			base.AddState(num, out generatedLabelSymbol);
			BoundStatement boundStatement = (BoundStatement)this.Visit(body);
			return this.F.Block(new BoundStatement[]
			{
				this.F.Label(generatedLabelSymbol),
				this.GenerateJumpToCurrentDisposalLabel(),
				base.GenerateSetBothStates(-1),
				boundStatement
			});
		}

		// Token: 0x06002185 RID: 8581 RVA: 0x000AE340 File Offset: 0x000AC540
		public override BoundNode VisitYieldReturnStatement(BoundYieldReturnStatement node)
		{
			int nextYieldReturnState = this._nextYieldReturnState;
			this._nextYieldReturnState = nextYieldReturnState - 1;
			int num = nextYieldReturnState;
			GeneratedLabelSymbol generatedLabelSymbol;
			base.AddState(num, out generatedLabelSymbol);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance();
			instance.Add(this.F.Assignment(this.F.InstanceField(this._asyncIteratorInfo.CurrentField), boundExpression, false));
			instance.Add(base.GenerateSetBothStates(num));
			instance.Add(this.F.Goto(this._exprReturnLabelTrue));
			instance.Add(this.F.Label(generatedLabelSymbol));
			instance.Add(this.F.HiddenSequencePoint(null));
			instance.Add(base.GenerateSetBothStates(-1));
			instance.Add(this.GenerateJumpToCurrentDisposalLabel());
			instance.Add(this.F.HiddenSequencePoint(null));
			return this.F.Block(instance.ToImmutableAndFree());
		}

		// Token: 0x06002186 RID: 8582 RVA: 0x000AE430 File Offset: 0x000AC630
		public override BoundNode VisitYieldBreakStatement(BoundYieldBreakStatement node)
		{
			return this.F.Block(new BoundStatement[]
			{
				this.SetDisposeMode(true),
				this.F.Goto(this._currentDisposalLabel)
			});
		}

		// Token: 0x06002187 RID: 8583 RVA: 0x000AE461 File Offset: 0x000AC661
		private BoundExpressionStatement SetDisposeMode(bool value)
		{
			return this.F.Assignment(this.F.InstanceField(this._asyncIteratorInfo.DisposeModeField), this.F.Literal(value), false);
		}

		// Token: 0x06002188 RID: 8584 RVA: 0x000AE494 File Offset: 0x000AC694
		public override BoundNode VisitTryStatement(BoundTryStatement node)
		{
			LabelSymbol currentDisposalLabel = this._currentDisposalLabel;
			if (node.FinallyBlockOpt != null)
			{
				GeneratedLabelSymbol generatedLabelSymbol = this.F.GenerateLabel("finallyEntry");
				this._currentDisposalLabel = generatedLabelSymbol;
				node = node.Update(this.F.Block(new BoundStatement[]
				{
					node.TryBlock,
					this.F.Label(generatedLabelSymbol)
				}), node.CatchBlocks, node.FinallyBlockOpt, node.FinallyLabelOpt, node.PreferFaultHandler);
			}
			else if (node.FinallyLabelOpt != null)
			{
				this._currentDisposalLabel = node.FinallyLabelOpt;
			}
			BoundStatement boundStatement = (BoundStatement)base.VisitTryStatement(node);
			this._currentDisposalLabel = currentDisposalLabel;
			if (node.FinallyBlockOpt != null && this._currentDisposalLabel != null)
			{
				boundStatement = this.AppendJumpToCurrentDisposalLabel(boundStatement);
			}
			return boundStatement;
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x000AE554 File Offset: 0x000AC754
		protected override BoundBlock VisitFinally(BoundBlock finallyBlock)
		{
			LabelSymbol currentDisposalLabel = this._currentDisposalLabel;
			this._currentDisposalLabel = null;
			BoundBlock boundBlock = base.VisitFinally(finallyBlock);
			this._currentDisposalLabel = currentDisposalLabel;
			return boundBlock;
		}

		// Token: 0x0600218A RID: 8586 RVA: 0x000AE580 File Offset: 0x000AC780
		public override BoundNode VisitExtractedFinallyBlock(BoundExtractedFinallyBlock extractedFinally)
		{
			BoundStatement boundStatement = this.VisitFinally(extractedFinally.FinallyBlock);
			if (this._currentDisposalLabel != null)
			{
				boundStatement = this.AppendJumpToCurrentDisposalLabel(boundStatement);
			}
			return boundStatement;
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x000AE5AC File Offset: 0x000AC7AC
		[CompilerGenerated]
		private BoundExpressionStatement <GenerateSetResultCall>g__generateSetResultOnPromise|5_0(bool result)
		{
			BoundFieldAccess boundFieldAccess = this.F.InstanceField(this._asyncIteratorInfo.PromiseOfValueOrEndField);
			return this.F.ExpressionStatement(this.F.Call(boundFieldAccess, this._asyncIteratorInfo.SetResultMethod, this.F.Literal(result)));
		}

		// Token: 0x04001045 RID: 4165
		private readonly AsyncIteratorInfo _asyncIteratorInfo;

		// Token: 0x04001046 RID: 4166
		private LabelSymbol _currentDisposalLabel;

		// Token: 0x04001047 RID: 4167
		private readonly LabelSymbol _exprReturnLabelTrue;

		// Token: 0x04001048 RID: 4168
		private int _nextYieldReturnState = -3;
	}
}
