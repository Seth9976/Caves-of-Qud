using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000146 RID: 326
	internal abstract class AbstractFlowPass<TLocalState, TLocalFunctionState> : BoundTreeVisitor where TLocalState : AbstractFlowPass<TLocalState, TLocalFunctionState>.ILocalState where TLocalFunctionState : AbstractFlowPass<TLocalState, TLocalFunctionState>.AbstractLocalFunctionState
	{
		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001999 RID: 6553 RVA: 0x000881B7 File Offset: 0x000863B7
		// (set) Token: 0x0600199A RID: 6554 RVA: 0x000881BF File Offset: 0x000863BF
		private protected ArrayBuilder<AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch> PendingBranches { protected get; private set; }

		// Token: 0x0600199B RID: 6555 RVA: 0x000881C8 File Offset: 0x000863C8
		protected void SetConditionalState(TLocalState whenTrue, TLocalState whenFalse)
		{
			this.IsConditionalState = true;
			this.State = default(TLocalState);
			this.StateWhenTrue = whenTrue;
			this.StateWhenFalse = whenFalse;
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x000881EC File Offset: 0x000863EC
		protected void SetState(TLocalState newState)
		{
			this.StateWhenTrue = (this.StateWhenFalse = default(TLocalState));
			this.IsConditionalState = false;
			this.State = newState;
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x0008821F File Offset: 0x0008641F
		protected void Split()
		{
			if (!this.IsConditionalState)
			{
				this.SetConditionalState(this.State, this.State.Clone());
			}
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x00088246 File Offset: 0x00086446
		protected void Unsplit()
		{
			if (this.IsConditionalState)
			{
				this.Join(ref this.StateWhenTrue, ref this.StateWhenFalse);
				this.SetState(this.StateWhenTrue);
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x0600199F RID: 6559 RVA: 0x0008826F File Offset: 0x0008646F
		protected DiagnosticBag Diagnostics { get; }

		// Token: 0x060019A0 RID: 6560 RVA: 0x00088278 File Offset: 0x00086478
		protected AbstractFlowPass(CSharpCompilation compilation, Symbol symbol, BoundNode node, BoundNode firstInRegion = null, BoundNode lastInRegion = null, bool trackRegions = false, bool nonMonotonicTransferFunction = false)
		{
			if (firstInRegion != null && lastInRegion != null)
			{
				trackRegions = true;
			}
			if (trackRegions)
			{
				int spanStart = firstInRegion.Syntax.SpanStart;
				int num = lastInRegion.Syntax.Span.End - spanStart;
				this.RegionSpan = new TextSpan(spanStart, num);
			}
			this.PendingBranches = ArrayBuilder<AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch>.GetInstance();
			this._labelsSeen = PooledHashSet<BoundStatement>.GetInstance();
			this._labels = PooledDictionary<LabelSymbol, TLocalState>.GetInstance();
			this.Diagnostics = DiagnosticBag.GetInstance();
			this.compilation = compilation;
			this._symbol = symbol;
			this.CurrentSymbol = symbol;
			this.methodMainNode = node;
			this.firstInRegion = firstInRegion;
			this.lastInRegion = lastInRegion;
			this._loopHeadState = new Dictionary<BoundLoopStatement, TLocalState>(ReferenceEqualityComparer.Instance);
			this.TrackingRegions = trackRegions;
			this._nonMonotonicTransfer = nonMonotonicTransferFunction;
		}

		// Token: 0x060019A1 RID: 6561
		protected abstract string Dump(TLocalState state);

		// Token: 0x060019A2 RID: 6562 RVA: 0x00088343 File Offset: 0x00086543
		protected string Dump()
		{
			if (!this.IsConditionalState)
			{
				return this.Dump(this.State);
			}
			return "true: " + this.Dump(this.StateWhenTrue) + " false: " + this.Dump(this.StateWhenFalse);
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x00088381 File Offset: 0x00086581
		protected virtual void EnterRegion()
		{
			this.regionPlace = RegionPlace.Inside;
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x0008838A File Offset: 0x0008658A
		protected virtual void LeaveRegion()
		{
			this.regionPlace = RegionPlace.After;
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x00088393 File Offset: 0x00086593
		protected bool RegionContains(TextSpan span)
		{
			if (span.Length == 0)
			{
				return this.RegionSpan.Contains(span.Start);
			}
			return this.RegionSpan.Contains(span);
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x060019A6 RID: 6566 RVA: 0x000883BD File Offset: 0x000865BD
		protected bool IsInside
		{
			get
			{
				return this.regionPlace == RegionPlace.Inside;
			}
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x000883C8 File Offset: 0x000865C8
		protected virtual void EnterParameters(ImmutableArray<ParameterSymbol> parameters)
		{
			foreach (ParameterSymbol parameterSymbol in parameters)
			{
				this.EnterParameter(parameterSymbol);
			}
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x000883F7 File Offset: 0x000865F7
		protected virtual void EnterParameter(ParameterSymbol parameter)
		{
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x000883FC File Offset: 0x000865FC
		protected virtual void LeaveParameters(ImmutableArray<ParameterSymbol> parameters, SyntaxNode syntax, Location location)
		{
			foreach (ParameterSymbol parameterSymbol in parameters)
			{
				this.LeaveParameter(parameterSymbol, syntax, location);
			}
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x0008842D File Offset: 0x0008662D
		protected virtual void LeaveParameter(ParameterSymbol parameter, SyntaxNode syntax, Location location)
		{
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x0008842F File Offset: 0x0008662F
		public override BoundNode Visit(BoundNode node)
		{
			return this.VisitAlways(node);
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x00088438 File Offset: 0x00086638
		protected BoundNode VisitAlways(BoundNode node)
		{
			BoundNode boundNode = null;
			if (node != null)
			{
				if (this.TrackingRegions)
				{
					if (node == this.firstInRegion && this.regionPlace == RegionPlace.Before)
					{
						this.EnterRegion();
					}
					boundNode = this.VisitWithStackGuard(node);
					if (node == this.lastInRegion && this.regionPlace == RegionPlace.Inside)
					{
						this.LeaveRegion();
					}
				}
				else
				{
					boundNode = this.VisitWithStackGuard(node);
				}
			}
			return boundNode;
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x00088494 File Offset: 0x00086694
		[DebuggerStepThrough]
		private BoundNode VisitWithStackGuard(BoundNode node)
		{
			BoundExpression boundExpression = node as BoundExpression;
			if (boundExpression != null)
			{
				return base.VisitExpressionWithStackGuard(ref this._recursionDepth, boundExpression);
			}
			return base.Visit(node);
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x000884C0 File Offset: 0x000866C0
		[DebuggerStepThrough]
		protected override BoundExpression VisitExpressionWithoutStackGuard(BoundExpression node)
		{
			return (BoundExpression)base.Visit(node);
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x000884CE File Offset: 0x000866CE
		protected override bool ConvertInsufficientExecutionStackExceptionToCancelledByStackGuardException()
		{
			return false;
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x000884D4 File Offset: 0x000866D4
		protected virtual ImmutableArray<AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch> Scan(ref bool badRegion)
		{
			AbstractFlowPass<TLocalState, TLocalFunctionState>.SavedPending savedPending = this.SavePending();
			this.Visit(this.methodMainNode);
			this.Unsplit();
			this.RestorePending(savedPending);
			if (this.TrackingRegions && this.regionPlace != RegionPlace.After)
			{
				badRegion = true;
			}
			return this.RemoveReturns();
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x0008851C File Offset: 0x0008671C
		protected ImmutableArray<AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch> Analyze(ref bool badRegion, Optional<TLocalState> initialState = default(Optional<TLocalState>))
		{
			ImmutableArray<AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch> immutableArray;
			do
			{
				this.regionPlace = RegionPlace.Before;
				this.State = (initialState.HasValue ? initialState.Value : this.TopState());
				this.PendingBranches.Clear();
				this.stateChangedAfterUse = false;
				this.Diagnostics.Clear();
				immutableArray = this.Scan(ref badRegion);
			}
			while (this.stateChangedAfterUse);
			return immutableArray;
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x0008857C File Offset: 0x0008677C
		protected virtual void Free()
		{
			this.Diagnostics.Free();
			this.PendingBranches.Free();
			this._labelsSeen.Free();
			this._labels.Free();
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x060019B3 RID: 6579 RVA: 0x000885AC File Offset: 0x000867AC
		protected ImmutableArray<ParameterSymbol> MethodParameters
		{
			get
			{
				MethodSymbol methodSymbol = this._symbol as MethodSymbol;
				if (methodSymbol != null)
				{
					return methodSymbol.Parameters;
				}
				return ImmutableArray<ParameterSymbol>.Empty;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x060019B4 RID: 6580 RVA: 0x000885D4 File Offset: 0x000867D4
		protected ParameterSymbol MethodThisParameter
		{
			get
			{
				ParameterSymbol parameterSymbol = null;
				MethodSymbol methodSymbol = this._symbol as MethodSymbol;
				if (methodSymbol != null)
				{
					methodSymbol.TryGetThisParameter(out parameterSymbol);
				}
				return parameterSymbol;
			}
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x00088600 File Offset: 0x00086800
		protected bool ShouldAnalyzeOutParameters(out Location location)
		{
			MethodSymbol methodSymbol = this._symbol as MethodSymbol;
			if (methodSymbol == null || methodSymbol.Locations.Length != 1)
			{
				location = null;
				return false;
			}
			location = methodSymbol.Locations[0];
			return true;
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x00088644 File Offset: 0x00086844
		protected virtual TLocalState LabelState(LabelSymbol label)
		{
			TLocalState tlocalState;
			if (this._labels.TryGetValue(label, ref tlocalState))
			{
				return tlocalState;
			}
			tlocalState = this.UnreachableState();
			this._labels.Add(label, tlocalState);
			return tlocalState;
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x00088678 File Offset: 0x00086878
		protected virtual ImmutableArray<AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch> RemoveReturns()
		{
			ImmutableArray<AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch> immutableArray = this.PendingBranches.ToImmutable();
			this.PendingBranches.Clear();
			return immutableArray;
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x00088690 File Offset: 0x00086890
		protected void SetUnreachable()
		{
			this.State = this.UnreachableState();
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x000886A0 File Offset: 0x000868A0
		protected void VisitLvalue(BoundExpression node)
		{
			if (this.TrackingRegions && node == this.firstInRegion && this.regionPlace == RegionPlace.Before)
			{
				this.EnterRegion();
			}
			BoundKind? boundKind = ((node != null) ? new BoundKind?(node.Kind) : default(BoundKind?));
			if (boundKind != null)
			{
				BoundKind valueOrDefault = boundKind.GetValueOrDefault();
				if (valueOrDefault <= BoundKind.Parameter)
				{
					switch (valueOrDefault)
					{
					case BoundKind.ThisReference:
					case BoundKind.BaseReference:
						goto IL_018A;
					case BoundKind.PreviousSubmissionReference:
					case BoundKind.HostObjectMemberReference:
						break;
					case BoundKind.Local:
						this.VisitLvalue((BoundLocal)node);
						goto IL_018A;
					default:
						if (valueOrDefault == BoundKind.Parameter)
						{
							this.VisitLvalueParameter((BoundParameter)node);
							goto IL_018A;
						}
						break;
					}
				}
				else
				{
					if (valueOrDefault - BoundKind.TupleLiteral <= 1)
					{
						((BoundTupleExpression)node).VisitAllElements<AbstractFlowPass<TLocalState, TLocalFunctionState>>(delegate([global::System.Runtime.CompilerServices.Nullable(1)] BoundExpression x, AbstractFlowPass<TLocalState, TLocalFunctionState> self)
						{
							self.VisitLvalue(x);
						}, this);
						goto IL_018A;
					}
					switch (valueOrDefault)
					{
					case BoundKind.FieldAccess:
					{
						BoundFieldAccess boundFieldAccess = (BoundFieldAccess)node;
						this.VisitFieldAccessInternal(boundFieldAccess.ReceiverOpt, boundFieldAccess.FieldSymbol);
						goto IL_018A;
					}
					case BoundKind.PropertyAccess:
					{
						BoundPropertyAccess boundPropertyAccess = (BoundPropertyAccess)node;
						if (Binder.AccessingAutoPropertyFromConstructor(boundPropertyAccess, this._symbol))
						{
							SourcePropertySymbolBase sourcePropertySymbolBase = boundPropertyAccess.PropertySymbol as SourcePropertySymbolBase;
							SynthesizedBackingFieldSymbol synthesizedBackingFieldSymbol = ((sourcePropertySymbolBase != null) ? sourcePropertySymbolBase.BackingField : null);
							if (synthesizedBackingFieldSymbol != null)
							{
								this.VisitFieldAccessInternal(boundPropertyAccess.ReceiverOpt, synthesizedBackingFieldSymbol);
								goto IL_018A;
							}
						}
						break;
					}
					case BoundKind.EventAccess:
					{
						BoundEventAccess boundEventAccess = (BoundEventAccess)node;
						this.VisitFieldAccessInternal(boundEventAccess.ReceiverOpt, boundEventAccess.EventSymbol.AssociatedField);
						goto IL_018A;
					}
					}
				}
			}
			this.VisitRvalue(node, false);
			IL_018A:
			if (this.TrackingRegions && node == this.lastInRegion && this.regionPlace == RegionPlace.Inside)
			{
				this.LeaveRegion();
			}
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x00088857 File Offset: 0x00086A57
		protected virtual void VisitLvalue(BoundLocal node)
		{
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x00088859 File Offset: 0x00086A59
		protected void VisitCondition(BoundExpression node)
		{
			this.Visit(node);
			this.AdjustConditionalState(node);
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x0008886C File Offset: 0x00086A6C
		private void AdjustConditionalState(BoundExpression node)
		{
			if (AbstractFlowPass<TLocalState, TLocalFunctionState>.IsConstantTrue(node))
			{
				this.Unsplit();
				this.SetConditionalState(this.State, this.UnreachableState());
			}
			else if (AbstractFlowPass<TLocalState, TLocalFunctionState>.IsConstantFalse(node))
			{
				this.Unsplit();
				this.SetConditionalState(this.UnreachableState(), this.State);
			}
			else if (node.Type == null || node.Type.SpecialType != SpecialType.System_Boolean)
			{
				this.Unsplit();
			}
			this.Split();
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x000888DF File Offset: 0x00086ADF
		protected virtual void VisitRvalue(BoundExpression node, bool isKnownToBeAnLvalue = false)
		{
			this.Visit(node);
			this.Unsplit();
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x000888EF File Offset: 0x00086AEF
		[DebuggerHidden]
		protected virtual void VisitStatement(BoundStatement statement)
		{
			this.Visit(statement);
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x000888F9 File Offset: 0x00086AF9
		protected static bool IsConstantTrue(BoundExpression node)
		{
			return node.ConstantValue == ConstantValue.True;
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x0008890B File Offset: 0x00086B0B
		protected static bool IsConstantFalse(BoundExpression node)
		{
			return node.ConstantValue == ConstantValue.False;
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x0008891D File Offset: 0x00086B1D
		protected static bool IsConstantNull(BoundExpression node)
		{
			return node.ConstantValue == ConstantValue.Null;
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x00088930 File Offset: 0x00086B30
		private void LoopHead(BoundLoopStatement node)
		{
			TLocalState tlocalState;
			if (this._loopHeadState.TryGetValue(node, ref tlocalState))
			{
				this.Join(ref this.State, ref tlocalState);
			}
			this._loopHeadState[node] = this.State.Clone();
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x0008897C File Offset: 0x00086B7C
		private void LoopTail(BoundLoopStatement node)
		{
			TLocalState tlocalState = this._loopHeadState[node];
			if (this.Join(ref tlocalState, ref this.State))
			{
				this._loopHeadState[node] = tlocalState;
				this.stateChangedAfterUse = true;
			}
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x000889BC File Offset: 0x00086BBC
		private void ResolveBreaks(TLocalState breakState, LabelSymbol label)
		{
			ArrayBuilder<AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch> pendingBranches = this.PendingBranches;
			int count = pendingBranches.Count;
			if (count != 0)
			{
				int num = 0;
				for (int i = 0; i < count; i++)
				{
					AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch pendingBranch = pendingBranches[i];
					if (pendingBranch.Label == label)
					{
						this.Join(ref breakState, ref pendingBranch.State);
					}
					else
					{
						if (num != i)
						{
							pendingBranches[num] = pendingBranch;
						}
						num++;
					}
				}
				pendingBranches.Clip(num);
			}
			this.SetState(breakState);
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x00088A34 File Offset: 0x00086C34
		private void ResolveContinues(LabelSymbol continueLabel)
		{
			ArrayBuilder<AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch> pendingBranches = this.PendingBranches;
			int count = pendingBranches.Count;
			if (count != 0)
			{
				int num = 0;
				for (int i = 0; i < count; i++)
				{
					AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch pendingBranch = pendingBranches[i];
					if (pendingBranch.Label == continueLabel)
					{
						this.Join(ref this.State, ref pendingBranch.State);
					}
					else
					{
						if (num != i)
						{
							pendingBranches[num] = pendingBranch;
						}
						num++;
					}
				}
				pendingBranches.Clip(num);
			}
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x00088AA6 File Offset: 0x00086CA6
		protected virtual void NoteBranch(AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch pending, BoundNode gotoStmt, BoundStatement target)
		{
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x00088AA8 File Offset: 0x00086CA8
		private bool ResolveBranches(LabelSymbol label, BoundStatement target)
		{
			bool flag = false;
			ArrayBuilder<AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch> pendingBranches = this.PendingBranches;
			int count = pendingBranches.Count;
			if (count != 0)
			{
				int num = 0;
				for (int i = 0; i < count; i++)
				{
					AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch pendingBranch = pendingBranches[i];
					if (pendingBranch.Label == label)
					{
						this.ResolveBranch(pendingBranch, label, target, ref flag);
					}
					else
					{
						if (num != i)
						{
							pendingBranches[num] = pendingBranch;
						}
						num++;
					}
				}
				pendingBranches.Clip(num);
			}
			return flag;
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x00088B1C File Offset: 0x00086D1C
		protected virtual void ResolveBranch(AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch pending, LabelSymbol label, BoundStatement target, ref bool labelStateChanged)
		{
			TLocalState tlocalState = this.LabelState(label);
			if (target != null)
			{
				this.NoteBranch(pending, pending.Branch, target);
			}
			if (this.Join(ref tlocalState, ref pending.State))
			{
				labelStateChanged = true;
				this._labels[label] = tlocalState;
			}
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x00088B63 File Offset: 0x00086D63
		protected AbstractFlowPass<TLocalState, TLocalFunctionState>.SavedPending SavePending()
		{
			AbstractFlowPass<TLocalState, TLocalFunctionState>.SavedPending savedPending = new AbstractFlowPass<TLocalState, TLocalFunctionState>.SavedPending(this.PendingBranches, this._labelsSeen);
			this.PendingBranches = ArrayBuilder<AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch>.GetInstance();
			this._labelsSeen = PooledHashSet<BoundStatement>.GetInstance();
			return savedPending;
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x00088B8C File Offset: 0x00086D8C
		protected void RestorePending(AbstractFlowPass<TLocalState, TLocalFunctionState>.SavedPending oldPending)
		{
			foreach (BoundStatement boundStatement in this._labelsSeen)
			{
				BoundKind kind = boundStatement.Kind;
				if (kind != BoundKind.LabelStatement)
				{
					if (kind != BoundKind.LabeledStatement)
					{
						if (kind != BoundKind.SwitchSection)
						{
							throw ExceptionUtilities.UnexpectedValue(boundStatement.Kind);
						}
						BoundSwitchSection boundSwitchSection = (BoundSwitchSection)boundStatement;
						foreach (BoundSwitchLabel boundSwitchLabel in boundSwitchSection.SwitchLabels)
						{
							this.stateChangedAfterUse |= this.ResolveBranches(boundSwitchLabel.Label, boundSwitchSection);
						}
					}
					else
					{
						BoundLabeledStatement boundLabeledStatement = (BoundLabeledStatement)boundStatement;
						this.stateChangedAfterUse |= this.ResolveBranches(boundLabeledStatement.Label, boundLabeledStatement);
					}
				}
				else
				{
					BoundLabelStatement boundLabelStatement = (BoundLabelStatement)boundStatement;
					this.stateChangedAfterUse |= this.ResolveBranches(boundLabelStatement.Label, boundLabelStatement);
				}
			}
			oldPending.PendingBranches.AddRange(this.PendingBranches);
			this.PendingBranches.Free();
			this.PendingBranches = oldPending.PendingBranches;
			this._labelsSeen.Free();
			this._labelsSeen = oldPending.LabelsSeen;
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x00088CE4 File Offset: 0x00086EE4
		public override BoundNode DefaultVisit(BoundNode node)
		{
			this.Diagnostics.Add(ErrorCode.ERR_InternalError, node.Syntax.Location);
			return null;
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x00088D03 File Offset: 0x00086F03
		public override BoundNode VisitAttribute(BoundAttribute node)
		{
			return null;
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x00088D06 File Offset: 0x00086F06
		public override BoundNode VisitThrowExpression(BoundThrowExpression node)
		{
			this.VisitRvalue(node.Expression, false);
			this.SetUnreachable();
			return node;
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x00088D1C File Offset: 0x00086F1C
		public override BoundNode VisitPassByCopy(BoundPassByCopy node)
		{
			this.VisitRvalue(node.Expression, false);
			return node;
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x00088D2C File Offset: 0x00086F2C
		public override BoundNode VisitIsPatternExpression(BoundIsPatternExpression node)
		{
			this.VisitRvalue(node.Expression, false);
			BoundPattern boundPattern;
			bool flag = node.Pattern.IsNegated(out boundPattern);
			this.VisitPattern(boundPattern);
			ImmutableHashSet<LabelSymbol> reachableLabels = node.DecisionDag.ReachableLabels;
			if (!reachableLabels.Contains(node.WhenTrueLabel))
			{
				this.SetState(this.StateWhenFalse);
				this.SetConditionalState(this.UnreachableState(), this.State);
			}
			else if (!reachableLabels.Contains(node.WhenFalseLabel))
			{
				this.SetState(this.StateWhenTrue);
				this.SetConditionalState(this.State, this.UnreachableState());
			}
			if (flag)
			{
				this.SetConditionalState(this.StateWhenFalse, this.StateWhenTrue);
			}
			return node;
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x00088DD5 File Offset: 0x00086FD5
		public virtual void VisitPattern(BoundPattern pattern)
		{
			this.Split();
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x00088DDD File Offset: 0x00086FDD
		public override BoundNode VisitConstantPattern(BoundConstantPattern node)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x00088DE4 File Offset: 0x00086FE4
		public override BoundNode VisitTupleLiteral(BoundTupleLiteral node)
		{
			return this.VisitTupleExpression(node);
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x00088DED File Offset: 0x00086FED
		public override BoundNode VisitConvertedTupleLiteral(BoundConvertedTupleLiteral node)
		{
			return this.VisitTupleExpression(node);
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x00088DF8 File Offset: 0x00086FF8
		private BoundNode VisitTupleExpression(BoundTupleExpression node)
		{
			this.VisitArguments(node.Arguments, default(ImmutableArray<RefKind>), null);
			return null;
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x00088E1C File Offset: 0x0008701C
		public override BoundNode VisitTupleBinaryOperator(BoundTupleBinaryOperator node)
		{
			this.VisitRvalue(node.Left, false);
			this.VisitRvalue(node.Right, false);
			return null;
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x00088E39 File Offset: 0x00087039
		public override BoundNode VisitDynamicObjectCreationExpression(BoundDynamicObjectCreationExpression node)
		{
			this.VisitArguments(node.Arguments, node.ArgumentRefKindsOpt, null);
			this.VisitRvalue(node.InitializerExpressionOpt, false);
			return null;
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x00088E5C File Offset: 0x0008705C
		public override BoundNode VisitDynamicIndexerAccess(BoundDynamicIndexerAccess node)
		{
			this.VisitRvalue(node.Receiver, false);
			this.VisitArguments(node.Arguments, node.ArgumentRefKindsOpt, null);
			return null;
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x00088E7F File Offset: 0x0008707F
		public override BoundNode VisitDynamicMemberAccess(BoundDynamicMemberAccess node)
		{
			this.VisitRvalue(node.Receiver, false);
			return null;
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x00088E8F File Offset: 0x0008708F
		public override BoundNode VisitDynamicInvocation(BoundDynamicInvocation node)
		{
			this.VisitRvalue(node.Expression, false);
			this.VisitArguments(node.Arguments, node.ArgumentRefKindsOpt, null);
			return null;
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x00088EB4 File Offset: 0x000870B4
		protected BoundNode VisitInterpolatedStringBase(BoundInterpolatedStringBase node)
		{
			foreach (BoundExpression boundExpression in node.Parts)
			{
				this.VisitRvalue(boundExpression, false);
			}
			return null;
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x00088EEC File Offset: 0x000870EC
		public override BoundNode VisitInterpolatedString(BoundInterpolatedString node)
		{
			return this.VisitInterpolatedStringBase(node);
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x00088EF5 File Offset: 0x000870F5
		public override BoundNode VisitUnconvertedInterpolatedString(BoundUnconvertedInterpolatedString node)
		{
			return this.VisitInterpolatedStringBase(node);
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x00088EFE File Offset: 0x000870FE
		public override BoundNode VisitStringInsert(BoundStringInsert node)
		{
			this.VisitRvalue(node.Value, false);
			if (node.Alignment != null)
			{
				this.VisitRvalue(node.Alignment, false);
			}
			if (node.Format != null)
			{
				this.VisitRvalue(node.Format, false);
			}
			return null;
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x00088F38 File Offset: 0x00087138
		public override BoundNode VisitArgList(BoundArgList node)
		{
			return null;
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x00088F3B File Offset: 0x0008713B
		public override BoundNode VisitArgListOperator(BoundArgListOperator node)
		{
			this.VisitArguments(node.Arguments, node.ArgumentRefKindsOpt, null);
			return null;
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x00088F51 File Offset: 0x00087151
		public override BoundNode VisitRefTypeOperator(BoundRefTypeOperator node)
		{
			this.VisitRvalue(node.Operand, false);
			return null;
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x00088F61 File Offset: 0x00087161
		public override BoundNode VisitMakeRefOperator(BoundMakeRefOperator node)
		{
			this.VisitRvalue(node.Operand, true);
			return null;
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x00088F71 File Offset: 0x00087171
		public override BoundNode VisitRefValueOperator(BoundRefValueOperator node)
		{
			this.VisitRvalue(node.Operand, false);
			return null;
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x00088F81 File Offset: 0x00087181
		public override BoundNode VisitGlobalStatementInitializer(BoundGlobalStatementInitializer node)
		{
			this.VisitStatement(node.Statement);
			return null;
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x00088F90 File Offset: 0x00087190
		public override BoundNode VisitLambda(BoundLambda node)
		{
			return null;
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x00088F93 File Offset: 0x00087193
		public override BoundNode VisitLocal(BoundLocal node)
		{
			return null;
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x00088F98 File Offset: 0x00087198
		public override BoundNode VisitLocalDeclaration(BoundLocalDeclaration node)
		{
			if (node.InitializerOpt != null)
			{
				this.VisitRvalue(node.InitializerOpt, node.LocalSymbol.RefKind > RefKind.None);
				if (node.LocalSymbol.RefKind != RefKind.None)
				{
					this.WriteArgument(node.InitializerOpt, node.LocalSymbol.RefKind, null);
				}
			}
			return null;
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x00088FED File Offset: 0x000871ED
		public override BoundNode VisitBlock(BoundBlock node)
		{
			this.VisitStatements(node.Statements);
			return null;
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x00088FFC File Offset: 0x000871FC
		private void VisitStatements(ImmutableArray<BoundStatement> statements)
		{
			foreach (BoundStatement boundStatement in statements)
			{
				this.VisitStatement(boundStatement);
			}
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x0008902B File Offset: 0x0008722B
		public override BoundNode VisitScope(BoundScope node)
		{
			this.VisitStatements(node.Statements);
			return null;
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x0008903A File Offset: 0x0008723A
		public override BoundNode VisitExpressionStatement(BoundExpressionStatement node)
		{
			this.VisitRvalue(node.Expression, false);
			return null;
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x0008904C File Offset: 0x0008724C
		public override BoundNode VisitCall(BoundCall node)
		{
			bool flag = node.Method.CallsAreOmitted(node.SyntaxTree);
			TLocalState tlocalState = default(TLocalState);
			if (flag)
			{
				tlocalState = this.State.Clone();
				this.SetUnreachable();
			}
			this.VisitReceiverBeforeCall(node.ReceiverOpt, node.Method);
			this.VisitArgumentsBeforeCall(node.Arguments, node.ArgumentRefKindsOpt);
			MethodSymbol method = node.Method;
			LocalFunctionSymbol localFunctionSymbol = ((method != null) ? method.OriginalDefinition : null) as LocalFunctionSymbol;
			if (localFunctionSymbol != null)
			{
				this.VisitLocalFunctionUse(localFunctionSymbol, node.Syntax, true);
			}
			this.VisitArgumentsAfterCall(node.Arguments, node.ArgumentRefKindsOpt, node.Method);
			this.VisitReceiverAfterCall(node.ReceiverOpt, node.Method);
			if (flag)
			{
				this.State = tlocalState;
			}
			return null;
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x00089110 File Offset: 0x00087310
		protected void VisitLocalFunctionUse(LocalFunctionSymbol symbol, SyntaxNode syntax, bool isCall)
		{
			TLocalFunctionState orCreateLocalFuncUsages = this.GetOrCreateLocalFuncUsages(symbol);
			this.VisitLocalFunctionUse(symbol, orCreateLocalFuncUsages, syntax, isCall);
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x00089130 File Offset: 0x00087330
		protected virtual void VisitLocalFunctionUse(LocalFunctionSymbol symbol, TLocalFunctionState localFunctionState, SyntaxNode syntax, bool isCall)
		{
			if (isCall)
			{
				this.Join(ref this.State, ref localFunctionState.StateFromBottom);
				this.Meet(ref this.State, ref localFunctionState.StateFromTop);
			}
			localFunctionState.Visited = true;
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x0008917D File Offset: 0x0008737D
		private void VisitReceiverBeforeCall(BoundExpression receiverOpt, MethodSymbol method)
		{
			if (method == null || method.MethodKind != MethodKind.Constructor)
			{
				this.VisitRvalue(receiverOpt, false);
			}
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x00089194 File Offset: 0x00087394
		private void VisitReceiverAfterCall(BoundExpression receiverOpt, MethodSymbol method)
		{
			if (receiverOpt == null)
			{
				return;
			}
			if (method == null)
			{
				this.WriteArgument(receiverOpt, RefKind.Ref, null);
				return;
			}
			ParameterSymbol parameterSymbol;
			if (method.TryGetThisParameter(out parameterSymbol) && parameterSymbol != null && !AbstractFlowPass<TLocalState, TLocalFunctionState>.TypeIsImmutable(parameterSymbol.Type))
			{
				RefKind refKind = parameterSymbol.RefKind;
				if (refKind.IsWritableReference())
				{
					this.WriteArgument(receiverOpt, refKind, method);
				}
			}
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x000891E4 File Offset: 0x000873E4
		private static bool TypeIsImmutable(TypeSymbol t)
		{
			SpecialType specialType = t.SpecialType;
			return specialType - SpecialType.System_Boolean <= 12 || specialType == SpecialType.System_DateTime || t.IsNullableType();
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x0008920C File Offset: 0x0008740C
		public override BoundNode VisitIndexerAccess(BoundIndexerAccess node)
		{
			MethodSymbol readMethod = AbstractFlowPass<TLocalState, TLocalFunctionState>.GetReadMethod(node.Indexer);
			this.VisitReceiverBeforeCall(node.ReceiverOpt, readMethod);
			this.VisitArguments(node.Arguments, node.ArgumentRefKindsOpt, readMethod);
			if (readMethod != null)
			{
				this.VisitReceiverAfterCall(node.ReceiverOpt, readMethod);
			}
			return null;
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x00089258 File Offset: 0x00087458
		public override BoundNode VisitIndexOrRangePatternIndexerAccess(BoundIndexOrRangePatternIndexerAccess node)
		{
			this.VisitRvalue(node.Receiver, false);
			MethodSymbol methodSymbol = AbstractFlowPass<TLocalState, TLocalFunctionState>.GetReadMethod(node.LengthOrCountProperty);
			this.VisitReceiverAfterCall(node.Receiver, methodSymbol);
			this.VisitRvalue(node.Argument, false);
			Symbol patternSymbol = node.PatternSymbol;
			PropertySymbol propertySymbol = patternSymbol as PropertySymbol;
			MethodSymbol methodSymbol3;
			if (propertySymbol == null)
			{
				MethodSymbol methodSymbol2 = patternSymbol as MethodSymbol;
				if (methodSymbol2 == null)
				{
					throw ExceptionUtilities.UnexpectedValue(node.PatternSymbol);
				}
				methodSymbol3 = methodSymbol2;
			}
			else
			{
				methodSymbol3 = AbstractFlowPass<TLocalState, TLocalFunctionState>.GetReadMethod(propertySymbol);
			}
			methodSymbol = methodSymbol3;
			this.VisitReceiverAfterCall(node.Receiver, methodSymbol);
			return null;
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x000892E1 File Offset: 0x000874E1
		public override BoundNode VisitEventAssignmentOperator(BoundEventAssignmentOperator node)
		{
			this.VisitRvalue(node.ReceiverOpt, false);
			this.VisitRvalue(node.Argument, false);
			return null;
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x000892FE File Offset: 0x000874FE
		protected virtual void VisitArguments(ImmutableArray<BoundExpression> arguments, ImmutableArray<RefKind> refKindsOpt, MethodSymbol method)
		{
			this.VisitArgumentsBeforeCall(arguments, refKindsOpt);
			this.VisitArgumentsAfterCall(arguments, refKindsOpt, method);
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x00089314 File Offset: 0x00087514
		private void VisitArgumentsBeforeCall(ImmutableArray<BoundExpression> arguments, ImmutableArray<RefKind> refKindsOpt)
		{
			for (int i = 0; i < arguments.Length; i++)
			{
				RefKind refKind = AbstractFlowPass<TLocalState, TLocalFunctionState>.GetRefKind(refKindsOpt, i);
				if (refKind != RefKind.Out)
				{
					this.VisitRvalue(arguments[i], refKind > RefKind.None);
				}
				else
				{
					this.VisitLvalue(arguments[i]);
				}
			}
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x00089364 File Offset: 0x00087564
		private void VisitArgumentsAfterCall(ImmutableArray<BoundExpression> arguments, ImmutableArray<RefKind> refKindsOpt, MethodSymbol method)
		{
			for (int i = 0; i < arguments.Length; i++)
			{
				RefKind refKind = AbstractFlowPass<TLocalState, TLocalFunctionState>.GetRefKind(refKindsOpt, i);
				if (refKind != RefKind.None)
				{
					this.WriteArgument(arguments[i], refKind, method);
				}
			}
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x0008939E File Offset: 0x0008759E
		protected static RefKind GetRefKind(ImmutableArray<RefKind> refKindsOpt, int index)
		{
			if (!refKindsOpt.IsDefault && refKindsOpt.Length > index)
			{
				return refKindsOpt[index];
			}
			return RefKind.None;
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x000893BD File Offset: 0x000875BD
		protected virtual void WriteArgument(BoundExpression arg, RefKind refKind, MethodSymbol method)
		{
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x000893C0 File Offset: 0x000875C0
		public override BoundNode VisitBadExpression(BoundBadExpression node)
		{
			foreach (BoundExpression boundExpression in node.ChildBoundNodes)
			{
				this.VisitRvalue(boundExpression, false);
			}
			return null;
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x000893F8 File Offset: 0x000875F8
		public override BoundNode VisitBadStatement(BoundBadStatement node)
		{
			foreach (BoundNode boundNode in node.ChildBoundNodes)
			{
				if (boundNode is BoundStatement)
				{
					this.VisitStatement(boundNode as BoundStatement);
				}
				else
				{
					this.VisitRvalue(boundNode as BoundExpression, false);
				}
			}
			return null;
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x0008944C File Offset: 0x0008764C
		public override BoundNode VisitArrayInitialization(BoundArrayInitialization node)
		{
			foreach (BoundExpression boundExpression in node.Initializers)
			{
				this.VisitRvalue(boundExpression, false);
			}
			return null;
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x00089484 File Offset: 0x00087684
		public override BoundNode VisitDelegateCreationExpression(BoundDelegateCreationExpression node)
		{
			BoundMethodGroup boundMethodGroup = node.Argument as BoundMethodGroup;
			if (boundMethodGroup != null)
			{
				if (node.MethodOpt != null && node.MethodOpt.RequiresInstanceReceiver)
				{
					if (this.TrackingRegions)
					{
						if (boundMethodGroup == this.firstInRegion && this.regionPlace == RegionPlace.Before)
						{
							this.EnterRegion();
						}
						this.VisitRvalue(boundMethodGroup.ReceiverOpt, false);
						if (boundMethodGroup == this.lastInRegion && this.IsInside)
						{
							this.LeaveRegion();
						}
					}
					else
					{
						this.VisitRvalue(boundMethodGroup.ReceiverOpt, false);
					}
				}
				else
				{
					MethodSymbol methodOpt = node.MethodOpt;
					LocalFunctionSymbol localFunctionSymbol = ((methodOpt != null) ? methodOpt.OriginalDefinition : null) as LocalFunctionSymbol;
					if (localFunctionSymbol != null)
					{
						this.VisitLocalFunctionUse(localFunctionSymbol, node.Syntax, false);
					}
				}
			}
			else
			{
				this.VisitRvalue(node.Argument, false);
			}
			return null;
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x00089545 File Offset: 0x00087745
		public override BoundNode VisitTypeExpression(BoundTypeExpression node)
		{
			return null;
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x00089548 File Offset: 0x00087748
		public override BoundNode VisitTypeOrValueExpression(BoundTypeOrValueExpression node)
		{
			return this.Visit(node.Data.ValueExpression);
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x00089569 File Offset: 0x00087769
		public override BoundNode VisitLiteral(BoundLiteral node)
		{
			return null;
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x0008956C File Offset: 0x0008776C
		public override BoundNode VisitMethodDefIndex(BoundMethodDefIndex node)
		{
			return null;
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x0008956F File Offset: 0x0008776F
		public override BoundNode VisitMaximumMethodDefIndex(BoundMaximumMethodDefIndex node)
		{
			return null;
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x00089572 File Offset: 0x00087772
		public override BoundNode VisitModuleVersionId(BoundModuleVersionId node)
		{
			return null;
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x00089575 File Offset: 0x00087775
		public override BoundNode VisitModuleVersionIdString(BoundModuleVersionIdString node)
		{
			return null;
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x00089578 File Offset: 0x00087778
		public override BoundNode VisitInstrumentationPayloadRoot(BoundInstrumentationPayloadRoot node)
		{
			return null;
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x0008957B File Offset: 0x0008777B
		public override BoundNode VisitSourceDocumentIndex(BoundSourceDocumentIndex node)
		{
			return null;
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x00089580 File Offset: 0x00087780
		public override BoundNode VisitConversion(BoundConversion node)
		{
			if (node.ConversionKind == ConversionKind.MethodGroup)
			{
				if (node.IsExtensionMethod || (node.SymbolOpt != null && node.SymbolOpt.RequiresInstanceReceiver))
				{
					BoundExpression receiverOpt = ((BoundMethodGroup)node.Operand).ReceiverOpt;
					if (this.TrackingRegions)
					{
						if (node.Operand == this.firstInRegion && this.regionPlace == RegionPlace.Before)
						{
							this.EnterRegion();
						}
						this.VisitRvalue(receiverOpt, false);
						if (node.Operand == this.lastInRegion && this.IsInside)
						{
							this.LeaveRegion();
						}
					}
					else
					{
						this.VisitRvalue(receiverOpt, false);
					}
				}
				else
				{
					MethodSymbol symbolOpt = node.SymbolOpt;
					LocalFunctionSymbol localFunctionSymbol = ((symbolOpt != null) ? symbolOpt.OriginalDefinition : null) as LocalFunctionSymbol;
					if (localFunctionSymbol != null)
					{
						this.VisitLocalFunctionUse(localFunctionSymbol, node.Syntax, false);
					}
				}
			}
			else
			{
				this.Visit(node.Operand);
			}
			return null;
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x00089658 File Offset: 0x00087858
		public override BoundNode VisitIfStatement(BoundIfStatement node)
		{
			this.VisitCondition(node.Condition);
			TLocalState tlocalState = this.StateWhenTrue;
			TLocalState stateWhenFalse = this.StateWhenFalse;
			this.SetState(tlocalState);
			this.VisitStatement(node.Consequence);
			tlocalState = this.State;
			this.SetState(stateWhenFalse);
			if (node.AlternativeOpt != null)
			{
				this.VisitStatement(node.AlternativeOpt);
			}
			this.Join(ref this.State, ref tlocalState);
			return null;
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x000896C4 File Offset: 0x000878C4
		public override BoundNode VisitTryStatement(BoundTryStatement node)
		{
			AbstractFlowPass<TLocalState, TLocalFunctionState>.SavedPending savedPending = this.SavePending();
			TLocalState tlocalState = this.State.Clone();
			AbstractFlowPass<TLocalState, TLocalFunctionState>.SavedPending savedPending2 = this.SavePending();
			this.VisitTryBlockWithAnyTransferFunction(node.TryBlock, node, ref tlocalState);
			TLocalState tlocalState2 = tlocalState.Clone();
			TLocalState state = this.State;
			foreach (BoundCatchBlock boundCatchBlock in node.CatchBlocks)
			{
				this.SetState(tlocalState.Clone());
				this.VisitCatchBlockWithAnyTransferFunction(boundCatchBlock, ref tlocalState2);
				this.Join(ref state, ref this.State);
			}
			this.RestorePending(savedPending2);
			if (node.FinallyBlockOpt != null)
			{
				this.SetState(tlocalState2);
				AbstractFlowPass<TLocalState, TLocalFunctionState>.SavedPending savedPending3 = this.SavePending();
				TLocalState tlocalState3 = this.ReachableBottomState();
				this.VisitFinallyBlockWithAnyTransferFunction(node.FinallyBlockOpt, ref tlocalState3);
				foreach (AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch pendingBranch in savedPending3.PendingBranches)
				{
					if (pendingBranch.Branch != null && pendingBranch.Branch.Kind != BoundKind.YieldReturnStatement)
					{
						this.<VisitTryStatement>g__updatePendingBranchState|139_0(ref pendingBranch.State, ref tlocalState3);
						if (pendingBranch.IsConditionalState)
						{
							this.<VisitTryStatement>g__updatePendingBranchState|139_0(ref pendingBranch.StateWhenTrue, ref tlocalState3);
							this.<VisitTryStatement>g__updatePendingBranchState|139_0(ref pendingBranch.StateWhenFalse, ref tlocalState3);
						}
					}
				}
				this.RestorePending(savedPending3);
				this.Meet(ref state, ref this.State);
				if (this._nonMonotonicTransfer)
				{
					this.Join(ref state, ref tlocalState3);
				}
			}
			this.SetState(state);
			this.RestorePending(savedPending);
			return null;
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x00089849 File Offset: 0x00087A49
		protected virtual void JoinTryBlockState(ref TLocalState self, ref TLocalState other)
		{
			this.Join(ref self, ref other);
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x00089854 File Offset: 0x00087A54
		private void VisitTryBlockWithAnyTransferFunction(BoundStatement tryBlock, BoundTryStatement node, ref TLocalState tryState)
		{
			if (this._nonMonotonicTransfer)
			{
				Optional<TLocalState> optional = this.NonMonotonicState;
				this.NonMonotonicState = this.ReachableBottomState();
				this.VisitTryBlock(tryBlock, node, ref tryState);
				TLocalState value = this.NonMonotonicState.Value;
				this.Join(ref tryState, ref value);
				if (optional.HasValue)
				{
					TLocalState value2 = optional.Value;
					this.JoinTryBlockState(ref value2, ref value);
					optional = value2;
				}
				this.NonMonotonicState = optional;
				return;
			}
			this.VisitTryBlock(tryBlock, node, ref tryState);
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x000898D3 File Offset: 0x00087AD3
		protected virtual void VisitTryBlock(BoundStatement tryBlock, BoundTryStatement node, ref TLocalState tryState)
		{
			this.VisitStatement(tryBlock);
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x000898DC File Offset: 0x00087ADC
		private void VisitCatchBlockWithAnyTransferFunction(BoundCatchBlock catchBlock, ref TLocalState finallyState)
		{
			if (this._nonMonotonicTransfer)
			{
				Optional<TLocalState> optional = this.NonMonotonicState;
				this.NonMonotonicState = this.ReachableBottomState();
				this.VisitCatchBlock(catchBlock, ref finallyState);
				TLocalState value = this.NonMonotonicState.Value;
				this.Join(ref finallyState, ref value);
				if (optional.HasValue)
				{
					TLocalState value2 = optional.Value;
					this.JoinTryBlockState(ref value2, ref value);
					optional = value2;
				}
				this.NonMonotonicState = optional;
				return;
			}
			this.VisitCatchBlock(catchBlock, ref finallyState);
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x0008995C File Offset: 0x00087B5C
		protected virtual void VisitCatchBlock(BoundCatchBlock catchBlock, ref TLocalState finallyState)
		{
			if (catchBlock.ExceptionSourceOpt != null)
			{
				this.VisitLvalue(catchBlock.ExceptionSourceOpt);
			}
			if (catchBlock.ExceptionFilterPrologueOpt != null)
			{
				this.VisitStatementList(catchBlock.ExceptionFilterPrologueOpt);
			}
			if (catchBlock.ExceptionFilterOpt != null)
			{
				this.VisitCondition(catchBlock.ExceptionFilterOpt);
				this.SetState(this.StateWhenTrue);
			}
			this.VisitStatement(catchBlock.Body);
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x000899C0 File Offset: 0x00087BC0
		private void VisitFinallyBlockWithAnyTransferFunction(BoundStatement finallyBlock, ref TLocalState stateMovedUp)
		{
			if (this._nonMonotonicTransfer)
			{
				Optional<TLocalState> optional = this.NonMonotonicState;
				this.NonMonotonicState = this.ReachableBottomState();
				this.VisitFinallyBlock(finallyBlock, ref stateMovedUp);
				TLocalState value = this.NonMonotonicState.Value;
				this.Join(ref stateMovedUp, ref value);
				if (optional.HasValue)
				{
					TLocalState value2 = optional.Value;
					this.JoinTryBlockState(ref value2, ref value);
					optional = value2;
				}
				this.NonMonotonicState = optional;
				return;
			}
			this.VisitFinallyBlock(finallyBlock, ref stateMovedUp);
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x00089A3D File Offset: 0x00087C3D
		protected virtual void VisitFinallyBlock(BoundStatement finallyBlock, ref TLocalState stateMovedUp)
		{
			this.VisitStatement(finallyBlock);
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x00089A46 File Offset: 0x00087C46
		public override BoundNode VisitExtractedFinallyBlock(BoundExtractedFinallyBlock node)
		{
			return this.VisitBlock(node.FinallyBlock);
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x00089A54 File Offset: 0x00087C54
		public override BoundNode VisitReturnStatement(BoundReturnStatement node)
		{
			BoundNode boundNode = this.VisitReturnStatementNoAdjust(node);
			this.PendingBranches.Add(new AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch(node, this.State, null, false, default(TLocalState), default(TLocalState)));
			this.SetUnreachable();
			return boundNode;
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x00089A99 File Offset: 0x00087C99
		protected virtual BoundNode VisitReturnStatementNoAdjust(BoundReturnStatement node)
		{
			this.VisitRvalue(node.ExpressionOpt, node.RefKind > RefKind.None);
			if (node.RefKind != RefKind.None)
			{
				this.WriteArgument(node.ExpressionOpt, node.RefKind, null);
			}
			return null;
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x00089ACC File Offset: 0x00087CCC
		public override BoundNode VisitThisReference(BoundThisReference node)
		{
			return null;
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x00089ACF File Offset: 0x00087CCF
		public override BoundNode VisitPreviousSubmissionReference(BoundPreviousSubmissionReference node)
		{
			return null;
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x00089AD2 File Offset: 0x00087CD2
		public override BoundNode VisitHostObjectMemberReference(BoundHostObjectMemberReference node)
		{
			return null;
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x00089AD5 File Offset: 0x00087CD5
		public override BoundNode VisitParameter(BoundParameter node)
		{
			return null;
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x00089AD8 File Offset: 0x00087CD8
		protected virtual void VisitLvalueParameter(BoundParameter node)
		{
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x00089ADA File Offset: 0x00087CDA
		public override BoundNode VisitObjectCreationExpression(BoundObjectCreationExpression node)
		{
			this.VisitArguments(node.Arguments, node.ArgumentRefKindsOpt, node.Constructor);
			this.VisitRvalue(node.InitializerExpressionOpt, false);
			return null;
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x00089B02 File Offset: 0x00087D02
		public override BoundNode VisitNewT(BoundNewT node)
		{
			this.VisitRvalue(node.InitializerExpressionOpt, false);
			return null;
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x00089B12 File Offset: 0x00087D12
		public override BoundNode VisitNoPiaObjectCreationExpression(BoundNoPiaObjectCreationExpression node)
		{
			this.VisitRvalue(node.InitializerExpressionOpt, false);
			return null;
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x00089B22 File Offset: 0x00087D22
		protected virtual void PropertySetter(BoundExpression node, BoundExpression receiver, MethodSymbol setter, BoundExpression value = null)
		{
			this.VisitReceiverAfterCall(receiver, setter);
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x00089B2C File Offset: 0x00087D2C
		private bool RegularPropertyAccess(BoundExpression expr)
		{
			return expr.Kind == BoundKind.PropertyAccess && !Binder.AccessingAutoPropertyFromConstructor((BoundPropertyAccess)expr, this._symbol);
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x00089B54 File Offset: 0x00087D54
		public override BoundNode VisitAssignmentOperator(BoundAssignmentOperator node)
		{
			if (this.RegularPropertyAccess(node.Left))
			{
				BoundPropertyAccess boundPropertyAccess = (BoundPropertyAccess)node.Left;
				PropertySymbol propertySymbol = boundPropertyAccess.PropertySymbol;
				if (propertySymbol.RefKind == RefKind.None)
				{
					MethodSymbol writeMethod = AbstractFlowPass<TLocalState, TLocalFunctionState>.GetWriteMethod(propertySymbol);
					this.VisitReceiverBeforeCall(boundPropertyAccess.ReceiverOpt, writeMethod);
					this.VisitRvalue(node.Right, false);
					this.PropertySetter(node, boundPropertyAccess.ReceiverOpt, writeMethod, node.Right);
					return null;
				}
			}
			this.VisitLvalue(node.Left);
			this.VisitRvalue(node.Right, node.IsRef);
			if (node.IsRef)
			{
				RefKind refKind = ((node.Left.Kind == BoundKind.BadExpression) ? RefKind.Ref : node.Left.GetRefKind());
				this.WriteArgument(node.Right, refKind, null);
			}
			return null;
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x00089C14 File Offset: 0x00087E14
		public override BoundNode VisitDeconstructionAssignmentOperator(BoundDeconstructionAssignmentOperator node)
		{
			this.VisitLvalue(node.Left);
			this.VisitRvalue(node.Right, false);
			return null;
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x00089C30 File Offset: 0x00087E30
		public sealed override BoundNode VisitOutDeconstructVarPendingInference(OutDeconstructVarPendingInference node)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x00089C37 File Offset: 0x00087E37
		public override BoundNode VisitCompoundAssignmentOperator(BoundCompoundAssignmentOperator node)
		{
			this.VisitCompoundAssignmentTarget(node);
			this.VisitRvalue(node.Right, false);
			this.AfterRightHasBeenVisited(node);
			return null;
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x00089C58 File Offset: 0x00087E58
		protected void VisitCompoundAssignmentTarget(BoundCompoundAssignmentOperator node)
		{
			if (this.RegularPropertyAccess(node.Left))
			{
				BoundPropertyAccess boundPropertyAccess = (BoundPropertyAccess)node.Left;
				PropertySymbol propertySymbol = boundPropertyAccess.PropertySymbol;
				if (propertySymbol.RefKind == RefKind.None)
				{
					MethodSymbol readMethod = AbstractFlowPass<TLocalState, TLocalFunctionState>.GetReadMethod(propertySymbol);
					this.VisitReceiverBeforeCall(boundPropertyAccess.ReceiverOpt, readMethod);
					this.VisitReceiverAfterCall(boundPropertyAccess.ReceiverOpt, readMethod);
					return;
				}
			}
			this.VisitRvalue(node.Left, true);
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x00089CC0 File Offset: 0x00087EC0
		protected void AfterRightHasBeenVisited(BoundCompoundAssignmentOperator node)
		{
			if (this.RegularPropertyAccess(node.Left))
			{
				BoundPropertyAccess boundPropertyAccess = (BoundPropertyAccess)node.Left;
				PropertySymbol propertySymbol = boundPropertyAccess.PropertySymbol;
				if (propertySymbol.RefKind == RefKind.None)
				{
					MethodSymbol writeMethod = AbstractFlowPass<TLocalState, TLocalFunctionState>.GetWriteMethod(propertySymbol);
					this.PropertySetter(node, boundPropertyAccess.ReceiverOpt, writeMethod, null);
					this.VisitReceiverAfterCall(boundPropertyAccess.ReceiverOpt, writeMethod);
					return;
				}
			}
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x00089D1A File Offset: 0x00087F1A
		public override BoundNode VisitFieldAccess(BoundFieldAccess node)
		{
			this.VisitFieldAccessInternal(node.ReceiverOpt, node.FieldSymbol);
			return null;
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x00089D30 File Offset: 0x00087F30
		private void VisitFieldAccessInternal(BoundExpression receiverOpt, FieldSymbol fieldSymbol)
		{
			if (fieldSymbol != null && (fieldSymbol.IsFixedSizeBuffer || (!fieldSymbol.IsStatic && fieldSymbol.ContainingType.TypeKind == TypeKind.Struct && receiverOpt != null && receiverOpt.Kind != BoundKind.TypeExpression && receiverOpt.Type != null && !receiverOpt.Type.IsPrimitiveRecursiveStruct())))
			{
				this.VisitLvalue(receiverOpt);
				return;
			}
			this.VisitRvalue(receiverOpt, false);
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x00089D9D File Offset: 0x00087F9D
		public override BoundNode VisitFieldInfo(BoundFieldInfo node)
		{
			return null;
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x00089DA0 File Offset: 0x00087FA0
		public override BoundNode VisitMethodInfo(BoundMethodInfo node)
		{
			return null;
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x00089DA4 File Offset: 0x00087FA4
		public override BoundNode VisitPropertyAccess(BoundPropertyAccess node)
		{
			PropertySymbol propertySymbol = node.PropertySymbol;
			if (Binder.AccessingAutoPropertyFromConstructor(node, this._symbol))
			{
				SourcePropertySymbolBase sourcePropertySymbolBase = propertySymbol as SourcePropertySymbolBase;
				SynthesizedBackingFieldSymbol synthesizedBackingFieldSymbol = ((sourcePropertySymbolBase != null) ? sourcePropertySymbolBase.BackingField : null);
				if (synthesizedBackingFieldSymbol != null)
				{
					this.VisitFieldAccessInternal(node.ReceiverOpt, synthesizedBackingFieldSymbol);
					return null;
				}
			}
			MethodSymbol readMethod = AbstractFlowPass<TLocalState, TLocalFunctionState>.GetReadMethod(propertySymbol);
			this.VisitReceiverBeforeCall(node.ReceiverOpt, readMethod);
			this.VisitReceiverAfterCall(node.ReceiverOpt, readMethod);
			return null;
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x00089E13 File Offset: 0x00088013
		public override BoundNode VisitEventAccess(BoundEventAccess node)
		{
			this.VisitFieldAccessInternal(node.ReceiverOpt, node.EventSymbol.AssociatedField);
			return null;
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x00089E2D File Offset: 0x0008802D
		public override BoundNode VisitRangeVariable(BoundRangeVariable node)
		{
			return null;
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x00089E30 File Offset: 0x00088030
		public override BoundNode VisitQueryClause(BoundQueryClause node)
		{
			this.VisitRvalue(node.UnoptimizedForm ?? node.Value, false);
			return null;
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x00089E4C File Offset: 0x0008804C
		private BoundNode VisitMultipleLocalDeclarationsBase(BoundMultipleLocalDeclarationsBase node)
		{
			foreach (BoundLocalDeclaration boundLocalDeclaration in node.LocalDeclarations)
			{
				this.Visit(boundLocalDeclaration);
			}
			return null;
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x00089E84 File Offset: 0x00088084
		public override BoundNode VisitMultipleLocalDeclarations(BoundMultipleLocalDeclarations node)
		{
			return this.VisitMultipleLocalDeclarationsBase(node);
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x00089E90 File Offset: 0x00088090
		public override BoundNode VisitUsingLocalDeclarations(BoundUsingLocalDeclarations node)
		{
			if (this.AwaitUsingAndForeachAddsPendingBranch && node.AwaitOpt != null)
			{
				this.PendingBranches.Add(new AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch(node, this.State, null, false, default(TLocalState), default(TLocalState)));
			}
			return this.VisitMultipleLocalDeclarationsBase(node);
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x00089EE0 File Offset: 0x000880E0
		public override BoundNode VisitWhileStatement(BoundWhileStatement node)
		{
			this.LoopHead(node);
			this.VisitCondition(node.Condition);
			TLocalState stateWhenTrue = this.StateWhenTrue;
			TLocalState stateWhenFalse = this.StateWhenFalse;
			this.SetState(stateWhenTrue);
			this.VisitStatement(node.Body);
			this.ResolveContinues(node.ContinueLabel);
			this.LoopTail(node);
			this.ResolveBreaks(stateWhenFalse, node.BreakLabel);
			return null;
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x00089F42 File Offset: 0x00088142
		public override BoundNode VisitWithExpression(BoundWithExpression node)
		{
			this.VisitRvalue(node.Receiver, false);
			this.VisitObjectOrCollectionInitializerExpression(node.InitializerExpression.Initializers);
			return null;
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x00089F64 File Offset: 0x00088164
		public override BoundNode VisitArrayAccess(BoundArrayAccess node)
		{
			this.VisitRvalue(node.Expression, false);
			foreach (BoundExpression boundExpression in node.Indices)
			{
				this.VisitRvalue(boundExpression, false);
			}
			return null;
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x00089FA9 File Offset: 0x000881A9
		public override BoundNode VisitBinaryOperator(BoundBinaryOperator node)
		{
			if (node.OperatorKind.IsLogical())
			{
				this.VisitBinaryLogicalOperatorChildren(node);
			}
			else
			{
				this.VisitBinaryOperatorChildren(node);
			}
			return null;
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x00089FC9 File Offset: 0x000881C9
		public override BoundNode VisitUserDefinedConditionalLogicalOperator(BoundUserDefinedConditionalLogicalOperator node)
		{
			this.VisitBinaryLogicalOperatorChildren(node);
			return null;
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x00089FD4 File Offset: 0x000881D4
		private void VisitBinaryLogicalOperatorChildren(BoundExpression node)
		{
			ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance();
			BoundExpression boundExpression = node;
			BoundExpression boundExpression2;
			for (;;)
			{
				BoundKind kind = boundExpression.Kind;
				if (kind == BoundKind.BinaryOperator)
				{
					BoundBinaryOperator boundBinaryOperator = (BoundBinaryOperator)boundExpression;
					if (!boundBinaryOperator.OperatorKind.IsLogical())
					{
						break;
					}
					boundExpression2 = boundExpression;
					boundExpression = boundBinaryOperator.Left;
				}
				else
				{
					if (kind != BoundKind.UserDefinedConditionalLogicalOperator)
					{
						break;
					}
					boundExpression2 = boundExpression;
					boundExpression = ((BoundUserDefinedConditionalLogicalOperator)boundExpression2).Left;
				}
				instance.Push(boundExpression2);
			}
			this.VisitCondition(boundExpression);
			for (;;)
			{
				boundExpression2 = instance.Pop<BoundExpression>();
				BoundKind kind2 = boundExpression2.Kind;
				BinaryOperatorKind binaryOperatorKind;
				BoundExpression boundExpression3;
				if (kind2 != BoundKind.BinaryOperator)
				{
					if (kind2 != BoundKind.UserDefinedConditionalLogicalOperator)
					{
						break;
					}
					BoundUserDefinedConditionalLogicalOperator boundUserDefinedConditionalLogicalOperator = (BoundUserDefinedConditionalLogicalOperator)boundExpression2;
					binaryOperatorKind = boundUserDefinedConditionalLogicalOperator.OperatorKind;
					boundExpression3 = boundUserDefinedConditionalLogicalOperator.Right;
				}
				else
				{
					BoundBinaryOperator boundBinaryOperator2 = (BoundBinaryOperator)boundExpression2;
					binaryOperatorKind = boundBinaryOperator2.OperatorKind;
					boundExpression3 = boundBinaryOperator2.Right;
				}
				bool flag = binaryOperatorKind.Operator() == BinaryOperatorKind.And;
				bool flag2 = binaryOperatorKind.OperandTypes() == BinaryOperatorKind.Bool;
				TLocalState stateWhenTrue = this.StateWhenTrue;
				TLocalState stateWhenFalse = this.StateWhenFalse;
				this.SetState(flag ? stateWhenTrue : stateWhenFalse);
				this.AfterLeftChildOfBinaryLogicalOperatorHasBeenVisited(boundExpression2, boundExpression3, flag, flag2, ref stateWhenTrue, ref stateWhenFalse);
				if (instance.Count == 0)
				{
					goto IL_0117;
				}
				this.AdjustConditionalState(boundExpression2);
			}
			throw ExceptionUtilities.UnexpectedValue(boundExpression2.Kind);
			IL_0117:
			instance.Free();
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x0008A0FE File Offset: 0x000882FE
		protected virtual void AfterLeftChildOfBinaryLogicalOperatorHasBeenVisited(BoundExpression binary, BoundExpression right, bool isAnd, bool isBool, ref TLocalState leftTrue, ref TLocalState leftFalse)
		{
			this.Visit(right);
			this.AfterRightChildOfBinaryLogicalOperatorHasBeenVisited(binary, right, isAnd, isBool, ref leftTrue, ref leftFalse);
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x0008A118 File Offset: 0x00088318
		protected void AfterRightChildOfBinaryLogicalOperatorHasBeenVisited(BoundExpression binary, BoundExpression right, bool isAnd, bool isBool, ref TLocalState leftTrue, ref TLocalState leftFalse)
		{
			this.AdjustConditionalState(right);
			if (!isBool)
			{
				this.Unsplit();
				this.Split();
			}
			TLocalState stateWhenTrue = this.StateWhenTrue;
			TLocalState stateWhenFalse = this.StateWhenFalse;
			if (isAnd)
			{
				this.Join(ref stateWhenFalse, ref leftFalse);
			}
			else
			{
				this.Join(ref stateWhenTrue, ref leftTrue);
			}
			this.SetConditionalState(stateWhenTrue, stateWhenFalse);
			if (!isBool)
			{
				this.Unsplit();
			}
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x0008A178 File Offset: 0x00088378
		private void VisitBinaryOperatorChildren(BoundBinaryOperator node)
		{
			ArrayBuilder<BoundBinaryOperator> instance = ArrayBuilder<BoundBinaryOperator>.GetInstance();
			BoundBinaryOperator boundBinaryOperator = node;
			do
			{
				instance.Push(boundBinaryOperator);
				boundBinaryOperator = boundBinaryOperator.Left as BoundBinaryOperator;
			}
			while (boundBinaryOperator != null && !boundBinaryOperator.OperatorKind.IsLogical());
			this.VisitBinaryOperatorChildren(instance);
			instance.Free();
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x0008A1C0 File Offset: 0x000883C0
		protected virtual void VisitBinaryOperatorChildren(ArrayBuilder<BoundBinaryOperator> stack)
		{
			BoundBinaryOperator boundBinaryOperator = stack.Pop<BoundBinaryOperator>();
			this.VisitRvalue(boundBinaryOperator.Left, false);
			for (;;)
			{
				this.VisitRvalue(boundBinaryOperator.Right, false);
				if (stack.Count == 0)
				{
					break;
				}
				this.Unsplit();
				boundBinaryOperator = stack.Pop<BoundBinaryOperator>();
			}
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x0008A205 File Offset: 0x00088405
		public override BoundNode VisitUnaryOperator(BoundUnaryOperator node)
		{
			if (node.OperatorKind == UnaryOperatorKind.BoolLogicalNegation)
			{
				this.VisitCondition(node.Operand);
				this.SetConditionalState(this.StateWhenFalse, this.StateWhenTrue);
			}
			else
			{
				this.VisitRvalue(node.Operand, false);
			}
			return null;
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x0008A242 File Offset: 0x00088442
		public override BoundNode VisitRangeExpression(BoundRangeExpression node)
		{
			if (node.LeftOperandOpt != null)
			{
				this.VisitRvalue(node.LeftOperandOpt, false);
			}
			if (node.RightOperandOpt != null)
			{
				this.VisitRvalue(node.RightOperandOpt, false);
			}
			return null;
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x0008A26F File Offset: 0x0008846F
		public override BoundNode VisitFromEndIndexExpression(BoundFromEndIndexExpression node)
		{
			this.VisitRvalue(node.Operand, false);
			return null;
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x0008A280 File Offset: 0x00088480
		public override BoundNode VisitAwaitExpression(BoundAwaitExpression node)
		{
			this.VisitRvalue(node.Expression, false);
			this.PendingBranches.Add(new AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch(node, this.State, null, false, default(TLocalState), default(TLocalState)));
			return null;
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x0008A2C8 File Offset: 0x000884C8
		public override BoundNode VisitIncrementOperator(BoundIncrementOperator node)
		{
			if (this.RegularPropertyAccess(node.Operand))
			{
				BoundPropertyAccess boundPropertyAccess = (BoundPropertyAccess)node.Operand;
				PropertySymbol propertySymbol = boundPropertyAccess.PropertySymbol;
				if (propertySymbol.RefKind == RefKind.None)
				{
					MethodSymbol readMethod = AbstractFlowPass<TLocalState, TLocalFunctionState>.GetReadMethod(propertySymbol);
					MethodSymbol writeMethod = AbstractFlowPass<TLocalState, TLocalFunctionState>.GetWriteMethod(propertySymbol);
					this.VisitReceiverBeforeCall(boundPropertyAccess.ReceiverOpt, readMethod);
					this.VisitReceiverAfterCall(boundPropertyAccess.ReceiverOpt, readMethod);
					this.PropertySetter(node, boundPropertyAccess.ReceiverOpt, writeMethod, null);
					return null;
				}
			}
			this.VisitRvalue(node.Operand, false);
			return null;
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x0008A348 File Offset: 0x00088548
		public override BoundNode VisitArrayCreation(BoundArrayCreation node)
		{
			foreach (BoundExpression boundExpression in node.Bounds)
			{
				this.VisitRvalue(boundExpression, false);
			}
			if (node.InitializerOpt != null)
			{
				this.VisitArrayInitializationInternal(node, node.InitializerOpt);
			}
			return null;
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x0008A398 File Offset: 0x00088598
		private void VisitArrayInitializationInternal(BoundArrayCreation arrayCreation, BoundArrayInitialization node)
		{
			foreach (BoundExpression boundExpression in node.Initializers)
			{
				if (boundExpression.Kind == BoundKind.ArrayInitialization)
				{
					this.VisitArrayInitializationInternal(arrayCreation, (BoundArrayInitialization)boundExpression);
				}
				else
				{
					this.VisitRvalue(boundExpression, false);
				}
			}
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x0008A3EC File Offset: 0x000885EC
		public override BoundNode VisitForStatement(BoundForStatement node)
		{
			if (node.Initializer != null)
			{
				this.VisitStatement(node.Initializer);
			}
			this.LoopHead(node);
			TLocalState tlocalState;
			TLocalState tlocalState2;
			if (node.Condition != null)
			{
				this.VisitCondition(node.Condition);
				tlocalState = this.StateWhenTrue;
				tlocalState2 = this.StateWhenFalse;
			}
			else
			{
				tlocalState = this.State;
				tlocalState2 = this.UnreachableState();
			}
			this.SetState(tlocalState);
			this.VisitStatement(node.Body);
			this.ResolveContinues(node.ContinueLabel);
			if (node.Increment != null)
			{
				this.VisitStatement(node.Increment);
			}
			this.LoopTail(node);
			this.ResolveBreaks(tlocalState2, node.BreakLabel);
			return null;
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x0008A490 File Offset: 0x00088690
		public override BoundNode VisitForEachStatement(BoundForEachStatement node)
		{
			this.VisitForEachExpression(node);
			TLocalState tlocalState = this.State.Clone();
			this.LoopHead(node);
			this.VisitForEachIterationVariables(node);
			this.VisitStatement(node.Body);
			this.ResolveContinues(node.ContinueLabel);
			this.LoopTail(node);
			this.ResolveBreaks(tlocalState, node.BreakLabel);
			if (this.AwaitUsingAndForeachAddsPendingBranch && ((CommonForEachStatementSyntax)node.Syntax).AwaitKeyword != default(SyntaxToken))
			{
				this.PendingBranches.Add(new AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch(node, this.State, null, false, default(TLocalState), default(TLocalState)));
			}
			return null;
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x0008A544 File Offset: 0x00088744
		protected virtual void VisitForEachExpression(BoundForEachStatement node)
		{
			this.VisitRvalue(node.Expression, false);
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x0008A553 File Offset: 0x00088753
		public virtual void VisitForEachIterationVariables(BoundForEachStatement node)
		{
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x0008A555 File Offset: 0x00088755
		public override BoundNode VisitAsOperator(BoundAsOperator node)
		{
			this.VisitRvalue(node.Operand, false);
			return null;
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x0008A565 File Offset: 0x00088765
		public override BoundNode VisitIsOperator(BoundIsOperator node)
		{
			this.VisitRvalue(node.Operand, false);
			return null;
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x0008A575 File Offset: 0x00088775
		public override BoundNode VisitMethodGroup(BoundMethodGroup node)
		{
			if (node.ReceiverOpt != null)
			{
				this.VisitRvalue(node.ReceiverOpt, false);
			}
			return null;
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x0008A590 File Offset: 0x00088790
		public override BoundNode VisitNullCoalescingOperator(BoundNullCoalescingOperator node)
		{
			this.VisitRvalue(node.LeftOperand, false);
			if (AbstractFlowPass<TLocalState, TLocalFunctionState>.IsConstantNull(node.LeftOperand))
			{
				this.VisitRvalue(node.RightOperand, false);
			}
			else
			{
				TLocalState tlocalState = this.State.Clone();
				if (node.LeftOperand.ConstantValue != null)
				{
					this.SetUnreachable();
				}
				this.VisitRvalue(node.RightOperand, false);
				this.Join(ref this.State, ref tlocalState);
			}
			return null;
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x0008A610 File Offset: 0x00088810
		public override BoundNode VisitConditionalAccess(BoundConditionalAccess node)
		{
			this.VisitRvalue(node.Receiver, false);
			if (node.Receiver.ConstantValue != null && !AbstractFlowPass<TLocalState, TLocalFunctionState>.IsConstantNull(node.Receiver))
			{
				this.VisitRvalue(node.AccessExpression, false);
			}
			else
			{
				TLocalState tlocalState = this.State.Clone();
				if (AbstractFlowPass<TLocalState, TLocalFunctionState>.IsConstantNull(node.Receiver))
				{
					this.SetUnreachable();
				}
				this.VisitRvalue(node.AccessExpression, false);
				this.Join(ref this.State, ref tlocalState);
			}
			return null;
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x0008A69C File Offset: 0x0008889C
		public override BoundNode VisitLoweredConditionalAccess(BoundLoweredConditionalAccess node)
		{
			this.VisitRvalue(node.Receiver, false);
			TLocalState tlocalState = this.State.Clone();
			this.VisitRvalue(node.WhenNotNull, false);
			this.Join(ref this.State, ref tlocalState);
			if (node.WhenNullOpt != null)
			{
				tlocalState = this.State.Clone();
				this.VisitRvalue(node.WhenNullOpt, false);
				this.Join(ref this.State, ref tlocalState);
			}
			return null;
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x0008A71B File Offset: 0x0008891B
		public override BoundNode VisitConditionalReceiver(BoundConditionalReceiver node)
		{
			return null;
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x0008A720 File Offset: 0x00088920
		public override BoundNode VisitComplexConditionalReceiver(BoundComplexConditionalReceiver node)
		{
			TLocalState tlocalState = this.State.Clone();
			this.VisitRvalue(node.ValueTypeReceiver, false);
			this.Join(ref this.State, ref tlocalState);
			tlocalState = this.State.Clone();
			this.VisitRvalue(node.ReferenceTypeReceiver, false);
			this.Join(ref this.State, ref tlocalState);
			return null;
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x0008A78C File Offset: 0x0008898C
		public override BoundNode VisitSequence(BoundSequence node)
		{
			ImmutableArray<BoundExpression> sideEffects = node.SideEffects;
			if (!sideEffects.IsEmpty)
			{
				foreach (BoundExpression boundExpression in sideEffects)
				{
					this.VisitRvalue(boundExpression, false);
				}
			}
			this.VisitRvalue(node.Value, false);
			return null;
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x0008A7DA File Offset: 0x000889DA
		public override BoundNode VisitSequencePoint(BoundSequencePoint node)
		{
			if (node.StatementOpt != null)
			{
				this.VisitStatement(node.StatementOpt);
			}
			return null;
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x0008A7F1 File Offset: 0x000889F1
		public override BoundNode VisitSequencePointExpression(BoundSequencePointExpression node)
		{
			this.VisitRvalue(node.Expression, false);
			return null;
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x0008A801 File Offset: 0x00088A01
		public override BoundNode VisitSequencePointWithSpan(BoundSequencePointWithSpan node)
		{
			if (node.StatementOpt != null)
			{
				this.VisitStatement(node.StatementOpt);
			}
			return null;
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x0008A818 File Offset: 0x00088A18
		public override BoundNode VisitStatementList(BoundStatementList node)
		{
			return this.VisitStatementListWorker(node);
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x0008A824 File Offset: 0x00088A24
		private BoundNode VisitStatementListWorker(BoundStatementList node)
		{
			foreach (BoundStatement boundStatement in node.Statements)
			{
				this.VisitStatement(boundStatement);
			}
			return null;
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x0008A85B File Offset: 0x00088A5B
		public override BoundNode VisitTypeOrInstanceInitializers(BoundTypeOrInstanceInitializers node)
		{
			return this.VisitStatementListWorker(node);
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x0008A864 File Offset: 0x00088A64
		public override BoundNode VisitUnboundLambda(UnboundLambda node)
		{
			return this.VisitLambda(node.BindForErrorRecovery());
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x0008A874 File Offset: 0x00088A74
		public override BoundNode VisitBreakStatement(BoundBreakStatement node)
		{
			this.PendingBranches.Add(new AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch(node, this.State, node.Label, false, default(TLocalState), default(TLocalState)));
			this.SetUnreachable();
			return null;
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x0008A8B8 File Offset: 0x00088AB8
		public override BoundNode VisitContinueStatement(BoundContinueStatement node)
		{
			this.PendingBranches.Add(new AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch(node, this.State, node.Label, false, default(TLocalState), default(TLocalState)));
			this.SetUnreachable();
			return null;
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x0008A8FC File Offset: 0x00088AFC
		public override BoundNode VisitUnconvertedConditionalOperator(BoundUnconvertedConditionalOperator node)
		{
			return this.VisitConditionalOperatorCore(node, false, node.Condition, node.Consequence, node.Alternative);
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x0008A918 File Offset: 0x00088B18
		public override BoundNode VisitConditionalOperator(BoundConditionalOperator node)
		{
			return this.VisitConditionalOperatorCore(node, node.IsRef, node.Condition, node.Consequence, node.Alternative);
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x0008A93C File Offset: 0x00088B3C
		protected virtual BoundNode VisitConditionalOperatorCore(BoundExpression node, bool isByRef, BoundExpression condition, BoundExpression consequence, BoundExpression alternative)
		{
			this.VisitCondition(condition);
			TLocalState tlocalState = this.StateWhenTrue;
			TLocalState stateWhenFalse = this.StateWhenFalse;
			if (AbstractFlowPass<TLocalState, TLocalFunctionState>.IsConstantTrue(condition))
			{
				this.VisitConditionalOperand(stateWhenFalse, alternative, isByRef);
				this.VisitConditionalOperand(tlocalState, consequence, isByRef);
			}
			else if (AbstractFlowPass<TLocalState, TLocalFunctionState>.IsConstantFalse(condition))
			{
				this.VisitConditionalOperand(tlocalState, consequence, isByRef);
				this.VisitConditionalOperand(stateWhenFalse, alternative, isByRef);
			}
			else
			{
				this.VisitConditionalOperand(tlocalState, consequence, isByRef);
				this.Unsplit();
				tlocalState = this.State;
				this.VisitConditionalOperand(stateWhenFalse, alternative, isByRef);
				this.Unsplit();
				this.Join(ref this.State, ref tlocalState);
			}
			return null;
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x0008A9D1 File Offset: 0x00088BD1
		private void VisitConditionalOperand(TLocalState state, BoundExpression operand, bool isByRef)
		{
			this.SetState(state);
			if (isByRef)
			{
				this.VisitLvalue(operand);
				this.WriteArgument(operand, RefKind.Ref, null);
				return;
			}
			this.Visit(operand);
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x0008A9F6 File Offset: 0x00088BF6
		public override BoundNode VisitBaseReference(BoundBaseReference node)
		{
			return null;
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x0008A9FC File Offset: 0x00088BFC
		public override BoundNode VisitDoStatement(BoundDoStatement node)
		{
			this.LoopHead(node);
			this.VisitStatement(node.Body);
			this.ResolveContinues(node.ContinueLabel);
			this.VisitCondition(node.Condition);
			TLocalState stateWhenFalse = this.StateWhenFalse;
			this.SetState(this.StateWhenTrue);
			this.LoopTail(node);
			this.ResolveBreaks(stateWhenFalse, node.BreakLabel);
			return null;
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x0008AA5C File Offset: 0x00088C5C
		public override BoundNode VisitGotoStatement(BoundGotoStatement node)
		{
			this.PendingBranches.Add(new AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch(node, this.State, node.Label, false, default(TLocalState), default(TLocalState)));
			this.SetUnreachable();
			return null;
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x0008AAA0 File Offset: 0x00088CA0
		protected void VisitLabel(LabelSymbol label, BoundStatement node)
		{
			this.ResolveBranches(label, node);
			TLocalState tlocalState = this.LabelState(label);
			this.Join(ref this.State, ref tlocalState);
			this._labels[label] = this.State.Clone();
			this._labelsSeen.Add(node);
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x0008AAF7 File Offset: 0x00088CF7
		protected virtual void VisitLabel(BoundLabeledStatement node)
		{
			this.VisitLabel(node.Label, node);
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x0008AB06 File Offset: 0x00088D06
		public override BoundNode VisitLabelStatement(BoundLabelStatement node)
		{
			this.VisitLabel(node.Label, node);
			return null;
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x0008AB16 File Offset: 0x00088D16
		public override BoundNode VisitLabeledStatement(BoundLabeledStatement node)
		{
			this.VisitLabel(node);
			this.VisitStatement(node.Body);
			return null;
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x0008AB2C File Offset: 0x00088D2C
		public override BoundNode VisitLockStatement(BoundLockStatement node)
		{
			this.VisitRvalue(node.Argument, false);
			this.VisitStatement(node.Body);
			return null;
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x0008AB48 File Offset: 0x00088D48
		public override BoundNode VisitNoOpStatement(BoundNoOpStatement node)
		{
			return null;
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x0008AB4B File Offset: 0x00088D4B
		public override BoundNode VisitNamespaceExpression(BoundNamespaceExpression node)
		{
			return null;
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x0008AB50 File Offset: 0x00088D50
		public override BoundNode VisitUsingStatement(BoundUsingStatement node)
		{
			if (node.ExpressionOpt != null)
			{
				this.VisitRvalue(node.ExpressionOpt, false);
			}
			if (node.DeclarationsOpt != null)
			{
				this.VisitStatement(node.DeclarationsOpt);
			}
			this.VisitStatement(node.Body);
			if (this.AwaitUsingAndForeachAddsPendingBranch && node.AwaitOpt != null)
			{
				this.PendingBranches.Add(new AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch(node, this.State, null, false, default(TLocalState), default(TLocalState)));
			}
			return null;
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001A64 RID: 6756
		public abstract bool AwaitUsingAndForeachAddsPendingBranch { get; }

		// Token: 0x06001A65 RID: 6757 RVA: 0x0008ABCE File Offset: 0x00088DCE
		public override BoundNode VisitFixedStatement(BoundFixedStatement node)
		{
			this.VisitStatement(node.Declarations);
			this.VisitStatement(node.Body);
			return null;
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x0008ABE9 File Offset: 0x00088DE9
		public override BoundNode VisitFixedLocalCollectionInitializer(BoundFixedLocalCollectionInitializer node)
		{
			this.VisitRvalue(node.Expression, false);
			return null;
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x0008ABFC File Offset: 0x00088DFC
		public override BoundNode VisitThrowStatement(BoundThrowStatement node)
		{
			BoundExpression expressionOpt = node.ExpressionOpt;
			this.VisitRvalue(expressionOpt, false);
			this.SetUnreachable();
			return null;
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x0008AC20 File Offset: 0x00088E20
		public override BoundNode VisitYieldBreakStatement(BoundYieldBreakStatement node)
		{
			this.PendingBranches.Add(new AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch(node, this.State, null, false, default(TLocalState), default(TLocalState)));
			this.SetUnreachable();
			return null;
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x0008AC60 File Offset: 0x00088E60
		public override BoundNode VisitYieldReturnStatement(BoundYieldReturnStatement node)
		{
			this.VisitRvalue(node.Expression, false);
			this.PendingBranches.Add(new AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch(node, this.State, null, false, default(TLocalState), default(TLocalState)));
			return null;
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x0008ACA6 File Offset: 0x00088EA6
		public override BoundNode VisitDefaultLiteral(BoundDefaultLiteral node)
		{
			return null;
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x0008ACA9 File Offset: 0x00088EA9
		public override BoundNode VisitDefaultExpression(BoundDefaultExpression node)
		{
			return null;
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x0008ACAC File Offset: 0x00088EAC
		public override BoundNode VisitUnconvertedObjectCreationExpression(BoundUnconvertedObjectCreationExpression node)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x0008ACB3 File Offset: 0x00088EB3
		public override BoundNode VisitTypeOfOperator(BoundTypeOfOperator node)
		{
			this.VisitTypeExpression(node.SourceType);
			return null;
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x0008ACC4 File Offset: 0x00088EC4
		public override BoundNode VisitNameOfOperator(BoundNameOfOperator node)
		{
			TLocalState state = this.State;
			this.SetState(this.UnreachableState());
			this.Visit(node.Argument);
			this.SetState(state);
			return null;
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x0008ACF9 File Offset: 0x00088EF9
		public override BoundNode VisitAddressOfOperator(BoundAddressOfOperator node)
		{
			this.VisitAddressOfOperand(node.Operand, false);
			return null;
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x0008AD09 File Offset: 0x00088F09
		protected void VisitAddressOfOperand(BoundExpression operand, bool shouldReadOperand)
		{
			if (shouldReadOperand)
			{
				this.VisitRvalue(operand, false);
			}
			else
			{
				this.VisitLvalue(operand);
			}
			this.WriteArgument(operand, RefKind.Out, null);
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x0008AD28 File Offset: 0x00088F28
		public override BoundNode VisitPointerIndirectionOperator(BoundPointerIndirectionOperator node)
		{
			this.VisitRvalue(node.Operand, false);
			return null;
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x0008AD38 File Offset: 0x00088F38
		public override BoundNode VisitPointerElementAccess(BoundPointerElementAccess node)
		{
			this.VisitRvalue(node.Expression, false);
			this.VisitRvalue(node.Index, false);
			return null;
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x0008AD55 File Offset: 0x00088F55
		public override BoundNode VisitSizeOfOperator(BoundSizeOfOperator node)
		{
			return null;
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x0008AD58 File Offset: 0x00088F58
		private BoundNode VisitStackAllocArrayCreationBase(BoundStackAllocArrayCreationBase node)
		{
			this.VisitRvalue(node.Count, false);
			if (node.InitializerOpt != null && !node.InitializerOpt.Initializers.IsDefault)
			{
				foreach (BoundExpression boundExpression in node.InitializerOpt.Initializers)
				{
					this.VisitRvalue(boundExpression, false);
				}
			}
			return null;
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x0008ADBF File Offset: 0x00088FBF
		public override BoundNode VisitStackAllocArrayCreation(BoundStackAllocArrayCreation node)
		{
			return this.VisitStackAllocArrayCreationBase(node);
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x0008ADC8 File Offset: 0x00088FC8
		public override BoundNode VisitConvertedStackAllocExpression(BoundConvertedStackAllocExpression node)
		{
			return this.VisitStackAllocArrayCreationBase(node);
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x0008ADD4 File Offset: 0x00088FD4
		public override BoundNode VisitAnonymousObjectCreationExpression(BoundAnonymousObjectCreationExpression node)
		{
			this.VisitArguments(node.Arguments, default(ImmutableArray<RefKind>), node.Constructor);
			return null;
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x0008ADFD File Offset: 0x00088FFD
		public override BoundNode VisitArrayLength(BoundArrayLength node)
		{
			this.VisitRvalue(node.Expression, false);
			return null;
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x0008AE10 File Offset: 0x00089010
		public override BoundNode VisitConditionalGoto(BoundConditionalGoto node)
		{
			this.VisitCondition(node.Condition);
			if (node.JumpIfTrue)
			{
				this.PendingBranches.Add(new AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch(node, this.StateWhenTrue, node.Label, false, default(TLocalState), default(TLocalState)));
				this.SetState(this.StateWhenFalse);
			}
			else
			{
				this.PendingBranches.Add(new AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch(node, this.StateWhenFalse, node.Label, false, default(TLocalState), default(TLocalState)));
				this.SetState(this.StateWhenTrue);
			}
			return null;
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x0008AEAC File Offset: 0x000890AC
		public override BoundNode VisitObjectInitializerExpression(BoundObjectInitializerExpression node)
		{
			return this.VisitObjectOrCollectionInitializerExpression(node.Initializers);
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x0008AEBA File Offset: 0x000890BA
		public override BoundNode VisitCollectionInitializerExpression(BoundCollectionInitializerExpression node)
		{
			return this.VisitObjectOrCollectionInitializerExpression(node.Initializers);
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x0008AEC8 File Offset: 0x000890C8
		private BoundNode VisitObjectOrCollectionInitializerExpression(ImmutableArray<BoundExpression> initializers)
		{
			foreach (BoundExpression boundExpression in initializers)
			{
				this.VisitRvalue(boundExpression, false);
			}
			return null;
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x0008AEFC File Offset: 0x000890FC
		public override BoundNode VisitObjectInitializerMember(BoundObjectInitializerMember node)
		{
			if (!node.Arguments.IsDefaultOrEmpty)
			{
				MethodSymbol methodSymbol = null;
				Symbol memberSymbol = node.MemberSymbol;
				if (memberSymbol != null && memberSymbol.Kind == SymbolKind.Property)
				{
					methodSymbol = AbstractFlowPass<TLocalState, TLocalFunctionState>.GetReadMethod((PropertySymbol)node.MemberSymbol);
				}
				this.VisitArguments(node.Arguments, node.ArgumentRefKindsOpt, methodSymbol);
			}
			return null;
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x0008AF58 File Offset: 0x00089158
		public override BoundNode VisitDynamicObjectInitializerMember(BoundDynamicObjectInitializerMember node)
		{
			return null;
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x0008AF5C File Offset: 0x0008915C
		public override BoundNode VisitCollectionElementInitializer(BoundCollectionElementInitializer node)
		{
			if (node.AddMethod.CallsAreOmitted(node.SyntaxTree))
			{
				TLocalState tlocalState = this.State.Clone();
				this.SetUnreachable();
				this.VisitArguments(node.Arguments, default(ImmutableArray<RefKind>), node.AddMethod);
				this.State = tlocalState;
			}
			else
			{
				this.VisitArguments(node.Arguments, default(ImmutableArray<RefKind>), node.AddMethod);
			}
			return null;
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x0008AFD8 File Offset: 0x000891D8
		public override BoundNode VisitDynamicCollectionElementInitializer(BoundDynamicCollectionElementInitializer node)
		{
			this.VisitArguments(node.Arguments, default(ImmutableArray<RefKind>), null);
			return null;
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x0008AFFC File Offset: 0x000891FC
		public override BoundNode VisitImplicitReceiver(BoundImplicitReceiver node)
		{
			return null;
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x0008AFFF File Offset: 0x000891FF
		public override BoundNode VisitFieldEqualsValue(BoundFieldEqualsValue node)
		{
			this.VisitRvalue(node.Value, false);
			return null;
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x0008B00F File Offset: 0x0008920F
		public override BoundNode VisitPropertyEqualsValue(BoundPropertyEqualsValue node)
		{
			this.VisitRvalue(node.Value, false);
			return null;
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x0008B01F File Offset: 0x0008921F
		public override BoundNode VisitParameterEqualsValue(BoundParameterEqualsValue node)
		{
			this.VisitRvalue(node.Value, false);
			return null;
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x0008B02F File Offset: 0x0008922F
		public override BoundNode VisitDeconstructValuePlaceholder(BoundDeconstructValuePlaceholder node)
		{
			return null;
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x0008B032 File Offset: 0x00089232
		public override BoundNode VisitObjectOrCollectionValuePlaceholder(BoundObjectOrCollectionValuePlaceholder node)
		{
			return null;
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x0008B035 File Offset: 0x00089235
		public override BoundNode VisitAwaitableValuePlaceholder(BoundAwaitableValuePlaceholder node)
		{
			return null;
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x0008B038 File Offset: 0x00089238
		public sealed override BoundNode VisitOutVariablePendingInference(OutVariablePendingInference node)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x0008B03F File Offset: 0x0008923F
		public sealed override BoundNode VisitDeconstructionVariablePendingInference(DeconstructionVariablePendingInference node)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x0008B046 File Offset: 0x00089246
		public override BoundNode VisitDiscardExpression(BoundDiscardExpression node)
		{
			return null;
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x0008B049 File Offset: 0x00089249
		private static MethodSymbol GetReadMethod(PropertySymbol property)
		{
			return property.GetOwnOrInheritedGetMethod() ?? property.SetMethod;
		}

		// Token: 0x06001A8C RID: 6796 RVA: 0x0008B05B File Offset: 0x0008925B
		private static MethodSymbol GetWriteMethod(PropertySymbol property)
		{
			return property.GetOwnOrInheritedSetMethod() ?? property.GetMethod;
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x0008B06D File Offset: 0x0008926D
		public override BoundNode VisitConstructorMethodBody(BoundConstructorMethodBody node)
		{
			this.Visit(node.Initializer);
			this.VisitMethodBodies(node.BlockBody, node.ExpressionBody);
			return null;
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x0008B08F File Offset: 0x0008928F
		public override BoundNode VisitNonConstructorMethodBody(BoundNonConstructorMethodBody node)
		{
			this.VisitMethodBodies(node.BlockBody, node.ExpressionBody);
			return null;
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x0008B0A4 File Offset: 0x000892A4
		public override BoundNode VisitNullCoalescingAssignmentOperator(BoundNullCoalescingAssignmentOperator node)
		{
			TLocalState tlocalState2;
			if (this.RegularPropertyAccess(node.LeftOperand))
			{
				BoundPropertyAccess boundPropertyAccess = (BoundPropertyAccess)node.LeftOperand;
				PropertySymbol propertySymbol = boundPropertyAccess.PropertySymbol;
				if (propertySymbol.RefKind == RefKind.None)
				{
					MethodSymbol ownOrInheritedGetMethod = propertySymbol.GetOwnOrInheritedGetMethod();
					this.VisitReceiverBeforeCall(boundPropertyAccess.ReceiverOpt, ownOrInheritedGetMethod);
					this.VisitReceiverAfterCall(boundPropertyAccess.ReceiverOpt, ownOrInheritedGetMethod);
					TLocalState tlocalState = this.State.Clone();
					this.AdjustStateForNullCoalescingAssignmentNonNullCase(node);
					tlocalState2 = this.State.Clone();
					this.SetState(tlocalState);
					this.VisitAssignmentOfNullCoalescingAssignment(node, boundPropertyAccess);
					goto IL_00D1;
				}
			}
			this.VisitRvalue(node.LeftOperand, true);
			TLocalState tlocalState3 = this.State.Clone();
			this.AdjustStateForNullCoalescingAssignmentNonNullCase(node);
			tlocalState2 = this.State.Clone();
			this.SetState(tlocalState3);
			this.VisitAssignmentOfNullCoalescingAssignment(node, null);
			IL_00D1:
			this.Join(ref this.State, ref tlocalState2);
			return null;
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x0008B192 File Offset: 0x00089392
		public override BoundNode VisitReadOnlySpanFromArray(BoundReadOnlySpanFromArray node)
		{
			this.VisitRvalue(node.Operand, false);
			return null;
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x0008B1A2 File Offset: 0x000893A2
		public override BoundNode VisitFunctionPointerInvocation(BoundFunctionPointerInvocation node)
		{
			this.Visit(node.InvokedExpression);
			this.VisitArguments(node.Arguments, node.ArgumentRefKindsOpt, node.FunctionPointer.Signature);
			return null;
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x0008B1CF File Offset: 0x000893CF
		public override BoundNode VisitUnconvertedAddressOfOperator(BoundUnconvertedAddressOfOperator node)
		{
			this.Visit(node.Operand);
			return null;
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x0008B1E0 File Offset: 0x000893E0
		protected virtual void VisitAssignmentOfNullCoalescingAssignment(BoundNullCoalescingAssignmentOperator node, BoundPropertyAccess propertyAccessOpt)
		{
			this.VisitRvalue(node.RightOperand, false);
			if (propertyAccessOpt != null)
			{
				MethodSymbol ownOrInheritedSetMethod = propertyAccessOpt.PropertySymbol.GetOwnOrInheritedSetMethod();
				this.PropertySetter(node, propertyAccessOpt.ReceiverOpt, ownOrInheritedSetMethod, null);
			}
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x0008B218 File Offset: 0x00089418
		public override BoundNode VisitSavePreviousSequencePoint(BoundSavePreviousSequencePoint node)
		{
			return null;
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x0008B21B File Offset: 0x0008941B
		public override BoundNode VisitRestorePreviousSequencePoint(BoundRestorePreviousSequencePoint node)
		{
			return null;
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x0008B21E File Offset: 0x0008941E
		public override BoundNode VisitStepThroughSequencePoint(BoundStepThroughSequencePoint node)
		{
			return null;
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x0008B221 File Offset: 0x00089421
		protected virtual void AdjustStateForNullCoalescingAssignmentNonNullCase(BoundNullCoalescingAssignmentOperator node)
		{
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x0008B224 File Offset: 0x00089424
		private void VisitMethodBodies(BoundBlock blockBody, BoundBlock expressionBody)
		{
			if (blockBody == null)
			{
				this.Visit(expressionBody);
				return;
			}
			if (expressionBody == null)
			{
				this.Visit(blockBody);
				return;
			}
			TLocalState tlocalState = this.State.Clone();
			this.Visit(blockBody);
			TLocalState state = this.State;
			this.SetState(tlocalState);
			this.Visit(expressionBody);
			this.Join(ref this.State, ref state);
		}

		// Token: 0x06001A99 RID: 6809
		protected abstract TLocalState TopState();

		// Token: 0x06001A9A RID: 6810
		protected abstract TLocalState UnreachableState();

		// Token: 0x06001A9B RID: 6811 RVA: 0x0008B288 File Offset: 0x00089488
		protected virtual TLocalState ReachableBottomState()
		{
			return default(TLocalState);
		}

		// Token: 0x06001A9C RID: 6812
		protected abstract bool Join(ref TLocalState self, ref TLocalState other);

		// Token: 0x06001A9D RID: 6813
		protected abstract bool Meet(ref TLocalState self, ref TLocalState other);

		// Token: 0x06001A9E RID: 6814
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected abstract TLocalFunctionState CreateLocalFunctionState(LocalFunctionSymbol symbol);

		// Token: 0x06001A9F RID: 6815 RVA: 0x0008B2A0 File Offset: 0x000894A0
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected TLocalFunctionState GetOrCreateLocalFuncUsages(LocalFunctionSymbol localFunc)
		{
			if (this._localFuncVarUsages == null)
			{
				this._localFuncVarUsages = new SmallDictionary<LocalFunctionSymbol, TLocalFunctionState>();
			}
			TLocalFunctionState tlocalFunctionState;
			if (!this._localFuncVarUsages.TryGetValue(localFunc, out tlocalFunctionState))
			{
				tlocalFunctionState = this.CreateLocalFunctionState(localFunc);
				this._localFuncVarUsages[localFunc] = tlocalFunctionState;
			}
			return tlocalFunctionState;
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x0008B2E8 File Offset: 0x000894E8
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLocalFunctionStatement(BoundLocalFunctionStatement localFunc)
		{
			if (localFunc.Symbol.IsExtern)
			{
				return null;
			}
			Symbol currentSymbol = this.CurrentSymbol;
			LocalFunctionSymbol symbol = localFunc.Symbol;
			this.CurrentSymbol = symbol;
			AbstractFlowPass<TLocalState, TLocalFunctionState>.SavedPending savedPending = this.SavePending();
			TLocalState state = this.State;
			this.State = this.TopState();
			Optional<TLocalState> nonMonotonicState = this.NonMonotonicState;
			if (this._nonMonotonicTransfer)
			{
				this.NonMonotonicState = this.ReachableBottomState();
			}
			if (!localFunc.WasCompilerGenerated)
			{
				this.EnterParameters(symbol.Parameters);
			}
			TLocalFunctionState orCreateLocalFuncUsages = this.GetOrCreateLocalFuncUsages(symbol);
			TLocalFunctionState tlocalFunctionState = this.LocalFunctionStart(orCreateLocalFuncUsages);
			AbstractFlowPass<TLocalState, TLocalFunctionState>.SavedPending savedPending2 = this.SavePending();
			if (symbol.IsIterator)
			{
				this.PendingBranches.Add(new AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch(null, this.State, null, false, default(TLocalState), default(TLocalState)));
			}
			this.VisitAlways(localFunc.Body);
			this.RestorePending(savedPending2);
			ImmutableArray<AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch> immutableArray = this.RemoveReturns();
			this.RestorePending(savedPending);
			Location location = null;
			if (!symbol.Locations.IsDefaultOrEmpty)
			{
				location = symbol.Locations[0];
			}
			this.LeaveParameters(symbol.Parameters, localFunc.Syntax, location);
			TLocalState state2 = this.State;
			foreach (AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch pendingBranch in immutableArray)
			{
				this.State = pendingBranch.State;
				BoundNode branch = pendingBranch.Branch;
				this.LeaveParameters(symbol.Parameters, (branch != null) ? branch.Syntax : null, (branch != null && !branch.WasCompilerGenerated) ? null : location);
				this.Join(ref state2, ref this.State);
			}
			if (this.RecordStateChange(tlocalFunctionState, orCreateLocalFuncUsages, ref state2) && orCreateLocalFuncUsages.Visited)
			{
				this.stateChangedAfterUse = true;
				orCreateLocalFuncUsages.Visited = false;
			}
			this.State = state;
			this.NonMonotonicState = nonMonotonicState;
			this.CurrentSymbol = currentSymbol;
			return null;
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x0008B4D4 File Offset: 0x000896D4
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private bool RecordStateChange(TLocalFunctionState savedState, TLocalFunctionState currentState, ref TLocalState stateAtReturn)
		{
			bool flag = this.LocalFunctionEnd(savedState, currentState, ref stateAtReturn);
			flag |= this.Join(ref currentState.StateFromTop, ref stateAtReturn);
			if (this.NonMonotonicState.HasValue)
			{
				TLocalState value = this.NonMonotonicState.Value;
				this.Meet(ref value, ref stateAtReturn);
				flag |= this.Join(ref currentState.StateFromBottom, ref value);
			}
			return flag;
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x0008B53A File Offset: 0x0008973A
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected virtual TLocalFunctionState LocalFunctionStart(TLocalFunctionState state)
		{
			return state;
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x0008B53D File Offset: 0x0008973D
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected virtual bool LocalFunctionEnd(TLocalFunctionState savedState, TLocalFunctionState currentState, ref TLocalState stateAtReturn)
		{
			return false;
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x0008B540 File Offset: 0x00089740
		public override BoundNode VisitSwitchStatement(BoundSwitchStatement node)
		{
			TLocalState tlocalState = this.VisitSwitchStatementDispatch(node);
			ImmutableArray<BoundSwitchSection> switchSections = node.SwitchSections;
			int num = switchSections.Length - 1;
			for (int i = 0; i <= num; i++)
			{
				this.VisitSwitchSection(switchSections[i], i == num);
				this.Join(ref tlocalState, ref this.State);
			}
			this.ResolveBreaks(tlocalState, node.BreakLabel);
			return null;
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x0008B5A4 File Offset: 0x000897A4
		protected virtual TLocalState VisitSwitchStatementDispatch(BoundSwitchStatement node)
		{
			this.VisitRvalue(node.Expression, false);
			TLocalState tlocalState = this.State.Clone();
			ImmutableHashSet<LabelSymbol> reachableLabels = node.DecisionDag.ReachableLabels;
			foreach (BoundSwitchSection boundSwitchSection in node.SwitchSections)
			{
				foreach (BoundSwitchLabel boundSwitchLabel in boundSwitchSection.SwitchLabels)
				{
					if (reachableLabels.Contains(boundSwitchLabel.Label) || boundSwitchLabel.HasErrors || (boundSwitchLabel == node.DefaultLabel && node.Expression.ConstantValue == null && this.IsTraditionalSwitch(node)))
					{
						this.SetState(tlocalState.Clone());
					}
					else
					{
						this.SetUnreachable();
					}
					this.VisitPattern(boundSwitchLabel.Pattern);
					this.SetState(this.StateWhenTrue);
					if (boundSwitchLabel.WhenClause != null)
					{
						this.VisitCondition(boundSwitchLabel.WhenClause);
						this.SetState(this.StateWhenTrue);
					}
					this.PendingBranches.Add(new AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch(boundSwitchLabel, this.State, boundSwitchLabel.Label, false, default(TLocalState), default(TLocalState)));
				}
			}
			TLocalState tlocalState2 = this.UnreachableState();
			if (node.DecisionDag.ReachableLabels.Contains(node.BreakLabel) || (node.DefaultLabel == null && node.Expression.ConstantValue == null && this.IsTraditionalSwitch(node)))
			{
				this.Join(ref tlocalState2, ref tlocalState);
			}
			return tlocalState2;
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x0008B748 File Offset: 0x00089948
		private bool IsTraditionalSwitch(BoundSwitchStatement node)
		{
			if (this.compilation.LanguageVersion >= MessageID.IDS_FeatureRecursivePatterns.RequiredVersion())
			{
				return false;
			}
			if (!node.Expression.Type.IsValidV6SwitchGoverningType(false))
			{
				return false;
			}
			foreach (SwitchSectionSyntax switchSectionSyntax in ((SwitchStatementSyntax)node.Syntax).Sections)
			{
				SyntaxList<SwitchLabelSyntax>.Enumerator enumerator2 = switchSectionSyntax.Labels.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.Kind() == SyntaxKind.CasePatternSwitchLabel)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x0008B7E0 File Offset: 0x000899E0
		protected virtual void VisitSwitchSection(BoundSwitchSection node, bool isLastSection)
		{
			this.SetState(this.UnreachableState());
			foreach (BoundSwitchLabel boundSwitchLabel in node.SwitchLabels)
			{
				this.VisitLabel(boundSwitchLabel.Label, node);
			}
			this.VisitStatementList(node);
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x0008B830 File Offset: 0x00089A30
		public override BoundNode VisitSwitchDispatch(BoundSwitchDispatch node)
		{
			this.VisitRvalue(node.Expression, false);
			TLocalState tlocalState = this.State.Clone();
			this.PendingBranches.Add(new AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch(node, tlocalState, node.DefaultLabel, false, default(TLocalState), default(TLocalState)));
			foreach (ValueTuple<ConstantValue, LabelSymbol> valueTuple in node.Cases)
			{
				LabelSymbol item = valueTuple.Item2;
				this.PendingBranches.Add(new AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch(node, tlocalState, item, false, default(TLocalState), default(TLocalState)));
			}
			this.SetUnreachable();
			return null;
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x0008B8DD File Offset: 0x00089ADD
		public override BoundNode VisitConvertedSwitchExpression(BoundConvertedSwitchExpression node)
		{
			return this.VisitSwitchExpression(node);
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x0008B8E6 File Offset: 0x00089AE6
		public override BoundNode VisitUnconvertedSwitchExpression(BoundUnconvertedSwitchExpression node)
		{
			return this.VisitSwitchExpression(node);
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x0008B8F0 File Offset: 0x00089AF0
		private BoundNode VisitSwitchExpression(BoundSwitchExpression node)
		{
			this.VisitRvalue(node.Expression, false);
			TLocalState state = this.State;
			TLocalState tlocalState = this.UnreachableState();
			ImmutableHashSet<LabelSymbol> reachableLabels = node.DecisionDag.ReachableLabels;
			foreach (BoundSwitchExpressionArm boundSwitchExpressionArm in node.SwitchArms)
			{
				this.SetState(state.Clone());
				this.VisitPattern(boundSwitchExpressionArm.Pattern);
				this.SetState(this.StateWhenTrue);
				if (!reachableLabels.Contains(boundSwitchExpressionArm.Label) || boundSwitchExpressionArm.Pattern.HasErrors)
				{
					this.SetUnreachable();
				}
				if (boundSwitchExpressionArm.WhenClause != null)
				{
					this.VisitCondition(boundSwitchExpressionArm.WhenClause);
					this.SetState(this.StateWhenTrue);
				}
				this.VisitRvalue(boundSwitchExpressionArm.Value, false);
				this.Join(ref tlocalState, ref this.State);
			}
			this.SetState(tlocalState);
			return node;
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x0008B9E4 File Offset: 0x00089BE4
		[CompilerGenerated]
		private void <VisitTryStatement>g__updatePendingBranchState|139_0(ref TLocalState stateToUpdate, ref TLocalState stateMovedUpInFinally)
		{
			this.Meet(ref stateToUpdate, ref this.State);
			if (this._nonMonotonicTransfer)
			{
				this.Join(ref stateToUpdate, ref stateMovedUpInFinally);
			}
		}

		// Token: 0x04000DE8 RID: 3560
		protected int _recursionDepth;

		// Token: 0x04000DE9 RID: 3561
		protected readonly CSharpCompilation compilation;

		// Token: 0x04000DEA RID: 3562
		protected readonly Symbol _symbol;

		// Token: 0x04000DEB RID: 3563
		protected Symbol CurrentSymbol;

		// Token: 0x04000DEC RID: 3564
		protected readonly BoundNode methodMainNode;

		// Token: 0x04000DED RID: 3565
		private readonly PooledDictionary<LabelSymbol, TLocalState> _labels;

		// Token: 0x04000DEE RID: 3566
		protected bool stateChangedAfterUse;

		// Token: 0x04000DEF RID: 3567
		private PooledHashSet<BoundStatement> _labelsSeen;

		// Token: 0x04000DF1 RID: 3569
		protected TLocalState State;

		// Token: 0x04000DF2 RID: 3570
		protected TLocalState StateWhenTrue;

		// Token: 0x04000DF3 RID: 3571
		protected TLocalState StateWhenFalse;

		// Token: 0x04000DF4 RID: 3572
		protected bool IsConditionalState;

		// Token: 0x04000DF5 RID: 3573
		private readonly bool _nonMonotonicTransfer;

		// Token: 0x04000DF7 RID: 3575
		protected RegionPlace regionPlace;

		// Token: 0x04000DF8 RID: 3576
		protected readonly BoundNode firstInRegion;

		// Token: 0x04000DF9 RID: 3577
		protected readonly BoundNode lastInRegion;

		// Token: 0x04000DFA RID: 3578
		protected readonly bool TrackingRegions;

		// Token: 0x04000DFB RID: 3579
		private readonly Dictionary<BoundLoopStatement, TLocalState> _loopHeadState;

		// Token: 0x04000DFC RID: 3580
		protected readonly TextSpan RegionSpan;

		// Token: 0x04000DFD RID: 3581
		protected Optional<TLocalState> NonMonotonicState;

		// Token: 0x04000DFE RID: 3582
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1 })]
		private SmallDictionary<LocalFunctionSymbol, TLocalFunctionState> _localFuncVarUsages;

		// Token: 0x020006DC RID: 1756
		internal class PendingBranch
		{
			// Token: 0x060084A9 RID: 33961 RVA: 0x001E0D34 File Offset: 0x001DEF34
			public PendingBranch(BoundNode branch, TLocalState state, LabelSymbol label, bool isConditionalState = false, TLocalState stateWhenTrue = default(TLocalState), TLocalState stateWhenFalse = default(TLocalState))
			{
				this.Branch = branch;
				this.State = state.Clone();
				this.IsConditionalState = isConditionalState;
				if (isConditionalState)
				{
					this.StateWhenTrue = stateWhenTrue.Clone();
					this.StateWhenFalse = stateWhenFalse.Clone();
				}
				this.Label = label;
			}

			// Token: 0x040021C6 RID: 8646
			public readonly BoundNode Branch;

			// Token: 0x040021C7 RID: 8647
			public bool IsConditionalState;

			// Token: 0x040021C8 RID: 8648
			public TLocalState State;

			// Token: 0x040021C9 RID: 8649
			public TLocalState StateWhenTrue;

			// Token: 0x040021CA RID: 8650
			public TLocalState StateWhenFalse;

			// Token: 0x040021CB RID: 8651
			public readonly LabelSymbol Label;
		}

		// Token: 0x020006DD RID: 1757
		protected struct SavedPending
		{
			// Token: 0x060084AA RID: 33962 RVA: 0x001E0D9A File Offset: 0x001DEF9A
			public SavedPending(ArrayBuilder<AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch> pendingBranches, PooledHashSet<BoundStatement> labelsSeen)
			{
				this.PendingBranches = pendingBranches;
				this.LabelsSeen = labelsSeen;
			}

			// Token: 0x040021CC RID: 8652
			public readonly ArrayBuilder<AbstractFlowPass<TLocalState, TLocalFunctionState>.PendingBranch> PendingBranches;

			// Token: 0x040021CD RID: 8653
			public readonly PooledHashSet<BoundStatement> LabelsSeen;
		}

		// Token: 0x020006DE RID: 1758
		internal interface ILocalState
		{
			// Token: 0x060084AB RID: 33963
			TLocalState Clone();

			// Token: 0x170026A4 RID: 9892
			// (get) Token: 0x060084AC RID: 33964
			bool Reachable { get; }
		}

		// Token: 0x020006DF RID: 1759
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal abstract class AbstractLocalFunctionState
		{
			// Token: 0x060084AD RID: 33965 RVA: 0x001E0DAA File Offset: 0x001DEFAA
			public AbstractLocalFunctionState(TLocalState stateFromBottom, TLocalState stateFromTop)
			{
				this.StateFromBottom = stateFromBottom;
				this.StateFromTop = stateFromTop;
			}

			// Token: 0x040021CE RID: 8654
			public TLocalState StateFromBottom;

			// Token: 0x040021CF RID: 8655
			public TLocalState StateFromTop;

			// Token: 0x040021D0 RID: 8656
			public bool Visited;
		}
	}
}
