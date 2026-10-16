using System;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200014C RID: 332
	internal class ControlFlowPass : AbstractFlowPass<ControlFlowPass.LocalState, ControlFlowPass.LocalFunctionState>
	{
		// Token: 0x06001AC7 RID: 6855 RVA: 0x0008C082 File Offset: 0x0008A282
		protected override void Free()
		{
			this._labelsDefined.Free();
			this._labelsUsed.Free();
			this._usingDeclarations.Free();
			base.Free();
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x0008C0AB File Offset: 0x0008A2AB
		internal ControlFlowPass(CSharpCompilation compilation, Symbol member, BoundNode node)
			: base(compilation, member, node, null, null, false, false)
		{
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x0008C0DB File Offset: 0x0008A2DB
		internal ControlFlowPass(CSharpCompilation compilation, Symbol member, BoundNode node, BoundNode firstInRegion, BoundNode lastInRegion)
			: base(compilation, member, node, firstInRegion, lastInRegion, false, false)
		{
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x0008C10D File Offset: 0x0008A30D
		protected override ControlFlowPass.LocalFunctionState CreateLocalFunctionState(LocalFunctionSymbol symbol)
		{
			return new ControlFlowPass.LocalFunctionState(this.UnreachableState());
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x0008C11C File Offset: 0x0008A31C
		protected override bool Meet(ref ControlFlowPass.LocalState self, ref ControlFlowPass.LocalState other)
		{
			ControlFlowPass.LocalState localState = self;
			self.Alive &= other.Alive;
			self.Reported &= other.Reported;
			return self.Alive != localState.Alive;
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x0008C164 File Offset: 0x0008A364
		protected override bool Join(ref ControlFlowPass.LocalState self, ref ControlFlowPass.LocalState other)
		{
			ControlFlowPass.LocalState localState = self;
			self.Alive |= other.Alive;
			self.Reported &= other.Reported;
			return self.Alive != localState.Alive;
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x0008C1AC File Offset: 0x0008A3AC
		protected override string Dump(ControlFlowPass.LocalState state)
		{
			return string.Concat(new string[]
			{
				"[alive: ",
				state.Alive.ToString(),
				"; reported: ",
				state.Reported.ToString(),
				"]"
			});
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x0008C1FA File Offset: 0x0008A3FA
		protected override ControlFlowPass.LocalState TopState()
		{
			return new ControlFlowPass.LocalState(true, false);
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x0008C203 File Offset: 0x0008A403
		protected override ControlFlowPass.LocalState UnreachableState()
		{
			return new ControlFlowPass.LocalState(false, this.State.Reported);
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x0008C218 File Offset: 0x0008A418
		protected override ControlFlowPass.LocalState LabelState(LabelSymbol label)
		{
			ControlFlowPass.LocalState localState = base.LabelState(label);
			localState.Reported = false;
			return localState;
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x0008C236 File Offset: 0x0008A436
		public override BoundNode Visit(BoundNode node)
		{
			if (!(node is BoundExpression))
			{
				return base.Visit(node);
			}
			return null;
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x0008C24C File Offset: 0x0008A44C
		protected override ImmutableArray<AbstractFlowPass<ControlFlowPass.LocalState, ControlFlowPass.LocalFunctionState>.PendingBranch> Scan(ref bool badRegion)
		{
			base.Diagnostics.Clear();
			ImmutableArray<AbstractFlowPass<ControlFlowPass.LocalState, ControlFlowPass.LocalFunctionState>.PendingBranch> immutableArray = base.Scan(ref badRegion);
			foreach (LabelSymbol labelSymbol in this._labelsDefined.Keys)
			{
				if (!this._labelsUsed.Contains(labelSymbol))
				{
					base.Diagnostics.Add(ErrorCode.WRN_UnreferencedLabel, labelSymbol.Locations[0]);
				}
			}
			return immutableArray;
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x0008C2E0 File Offset: 0x0008A4E0
		public static bool Analyze(CSharpCompilation compilation, Symbol member, BoundBlock block, DiagnosticBag diagnostics)
		{
			ControlFlowPass controlFlowPass = new ControlFlowPass(compilation, member, block);
			if (diagnostics != null)
			{
				controlFlowPass._convertInsufficientExecutionStackExceptionToCancelledByStackGuardException = true;
			}
			bool flag2;
			try
			{
				bool flag = false;
				flag2 = controlFlowPass.Analyze(ref flag, diagnostics);
			}
			catch (BoundTreeVisitor.CancelledByStackGuardException ex) when (diagnostics != null)
			{
				ex.AddAnError(diagnostics);
				flag2 = true;
			}
			finally
			{
				controlFlowPass.Free();
			}
			return flag2;
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x0008C358 File Offset: 0x0008A558
		protected override bool ConvertInsufficientExecutionStackExceptionToCancelledByStackGuardException()
		{
			return this._convertInsufficientExecutionStackExceptionToCancelledByStackGuardException;
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x0008C360 File Offset: 0x0008A560
		protected bool Analyze(ref bool badRegion, DiagnosticBag diagnostics)
		{
			base.Analyze(ref badRegion, default(Optional<ControlFlowPass.LocalState>));
			if (diagnostics != null)
			{
				diagnostics.AddRange(base.Diagnostics);
			}
			return this.State.Alive;
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x0008C398 File Offset: 0x0008A598
		protected override ImmutableArray<AbstractFlowPass<ControlFlowPass.LocalState, ControlFlowPass.LocalFunctionState>.PendingBranch> RemoveReturns()
		{
			ImmutableArray<AbstractFlowPass<ControlFlowPass.LocalState, ControlFlowPass.LocalFunctionState>.PendingBranch> immutableArray = base.RemoveReturns();
			foreach (AbstractFlowPass<ControlFlowPass.LocalState, ControlFlowPass.LocalFunctionState>.PendingBranch pendingBranch in immutableArray)
			{
				if (pendingBranch.Branch != null)
				{
					BoundKind kind = pendingBranch.Branch.Kind;
					if (kind != BoundKind.ReturnStatement)
					{
						if (kind - BoundKind.BreakStatement > 1)
						{
							if (kind == BoundKind.GotoStatement)
							{
								SyntaxToken syntaxToken = pendingBranch.Branch.Syntax.GetFirstToken(false, false, false, false);
								SourceLocation sourceLocation = new SourceLocation(in syntaxToken);
								base.Diagnostics.Add(ErrorCode.ERR_LabelNotFound, sourceLocation, new object[] { ((BoundGotoStatement)pendingBranch.Branch).Label.Name });
							}
						}
						else
						{
							SyntaxToken syntaxToken = pendingBranch.Branch.Syntax.GetFirstToken(false, false, false, false);
							SourceLocation sourceLocation2 = new SourceLocation(in syntaxToken);
							base.Diagnostics.Add(ErrorCode.ERR_BadDelegateLeave, sourceLocation2);
						}
					}
				}
			}
			return immutableArray;
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x0008C480 File Offset: 0x0008A680
		protected override void VisitStatement(BoundStatement statement)
		{
			BoundKind kind = statement.Kind;
			if (kind <= BoundKind.NoOpStatement)
			{
				if (kind != BoundKind.Block && kind - BoundKind.LocalFunctionStatement > 1)
				{
					goto IL_0041;
				}
			}
			else if (kind != BoundKind.ThrowStatement && kind != BoundKind.LabeledStatement)
			{
				if (kind != BoundKind.StatementList)
				{
					goto IL_0041;
				}
				base.VisitStatementList((BoundStatementList)statement);
				return;
			}
			base.VisitStatement(statement);
			return;
			IL_0041:
			this.CheckReachable(statement);
			base.VisitStatement(statement);
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x0008C4DC File Offset: 0x0008A6DC
		private void CheckReachable(BoundStatement statement)
		{
			if (!this.State.Alive && !this.State.Reported && !statement.WasCompilerGenerated && statement.Syntax.Span.Length != 0)
			{
				SyntaxToken firstToken = statement.Syntax.GetFirstToken(false, false, false, false);
				base.Diagnostics.Add(ErrorCode.WRN_UnreachableCode, new SourceLocation(in firstToken));
				this.State.Reported = true;
			}
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x0008C554 File Offset: 0x0008A754
		protected override void VisitTryBlock(BoundStatement tryBlock, BoundTryStatement node, ref ControlFlowPass.LocalState tryState)
		{
			if (node.CatchBlocks.IsEmpty)
			{
				base.VisitTryBlock(tryBlock, node, ref tryState);
				return;
			}
			AbstractFlowPass<ControlFlowPass.LocalState, ControlFlowPass.LocalFunctionState>.SavedPending savedPending = base.SavePending();
			base.VisitTryBlock(tryBlock, node, ref tryState);
			base.RestorePending(savedPending);
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x0008C594 File Offset: 0x0008A794
		protected override void VisitCatchBlock(BoundCatchBlock catchBlock, ref ControlFlowPass.LocalState finallyState)
		{
			AbstractFlowPass<ControlFlowPass.LocalState, ControlFlowPass.LocalFunctionState>.SavedPending savedPending = base.SavePending();
			base.VisitCatchBlock(catchBlock, ref finallyState);
			base.RestorePending(savedPending);
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x0008C5B8 File Offset: 0x0008A7B8
		protected override void VisitFinallyBlock(BoundStatement finallyBlock, ref ControlFlowPass.LocalState endState)
		{
			AbstractFlowPass<ControlFlowPass.LocalState, ControlFlowPass.LocalFunctionState>.SavedPending savedPending = base.SavePending();
			AbstractFlowPass<ControlFlowPass.LocalState, ControlFlowPass.LocalFunctionState>.SavedPending savedPending2 = base.SavePending();
			base.VisitFinallyBlock(finallyBlock, ref endState);
			base.RestorePending(savedPending2);
			foreach (AbstractFlowPass<ControlFlowPass.LocalState, ControlFlowPass.LocalFunctionState>.PendingBranch pendingBranch in base.PendingBranches)
			{
				if (pendingBranch.Branch != null)
				{
					SyntaxToken firstToken = pendingBranch.Branch.Syntax.GetFirstToken(false, false, false, false);
					SourceLocation sourceLocation = new SourceLocation(in firstToken);
					BoundKind kind = pendingBranch.Branch.Kind;
					if (kind - BoundKind.YieldReturnStatement > 1)
					{
						base.Diagnostics.Add(ErrorCode.ERR_BadFinallyLeave, sourceLocation);
					}
				}
			}
			base.RestorePending(savedPending);
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001ADC RID: 6876 RVA: 0x0008C657 File Offset: 0x0008A857
		public sealed override bool AwaitUsingAndForeachAddsPendingBranch
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x0008C65A File Offset: 0x0008A85A
		protected override void VisitLabel(BoundLabeledStatement node)
		{
			this._labelsDefined[node.Label] = this._currentBlock;
			base.VisitLabel(node);
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x0008C67A File Offset: 0x0008A87A
		public override BoundNode VisitLabeledStatement(BoundLabeledStatement node)
		{
			this.VisitLabel(node);
			this.CheckReachable(node);
			this.VisitStatement(node.Body);
			return null;
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x0008C698 File Offset: 0x0008A898
		public override BoundNode VisitGotoStatement(BoundGotoStatement node)
		{
			this._labelsUsed.Add(node.Label);
			Location location = node.Syntax.Location;
			int start = location.SourceSpan.Start;
			int start2 = node.Label.Locations[0].SourceSpan.Start;
			foreach (ValueTuple<LocalSymbol, BoundBlock> valueTuple in this._usingDeclarations)
			{
				int start3 = valueTuple.Item1.Locations[0].SourceSpan.Start;
				if (start < start3 && start2 > start3)
				{
					base.Diagnostics.Add(ErrorCode.ERR_GoToForwardJumpOverUsingVar, location);
					break;
				}
				if (start > start3 && start2 < start3 && this._labelsDefined[node.Label] == valueTuple.Item2)
				{
					base.Diagnostics.Add(ErrorCode.ERR_GoToBackwardJumpOverUsingVar, location);
					break;
				}
			}
			return base.VisitGotoStatement(node);
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x0008C7A0 File Offset: 0x0008A9A0
		protected override void VisitSwitchSection(BoundSwitchSection node, bool isLastSection)
		{
			base.VisitSwitchSection(node, isLastSection);
			if (this.State.Alive)
			{
				SyntaxNode syntax = node.SwitchLabels.Last<BoundSwitchLabel>().Syntax;
				base.Diagnostics.Add(isLastSection ? ErrorCode.ERR_SwitchFallOut : ErrorCode.ERR_SwitchFallThrough, new SourceLocation(syntax), new object[] { syntax.ToString() });
			}
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x0008C804 File Offset: 0x0008AA04
		public override BoundNode VisitBlock(BoundBlock node)
		{
			BoundBlock currentBlock = this._currentBlock;
			this._currentBlock = node;
			int count = this._usingDeclarations.Count;
			foreach (LocalSymbol localSymbol in node.Locals)
			{
				if (localSymbol.IsUsing)
				{
					this._usingDeclarations.Add(new ValueTuple<LocalSymbol, BoundBlock>(localSymbol, node));
				}
			}
			BoundNode boundNode = base.VisitBlock(node);
			this._usingDeclarations.Clip(count);
			this._currentBlock = currentBlock;
			return boundNode;
		}

		// Token: 0x04000E0B RID: 3595
		private readonly PooledDictionary<LabelSymbol, BoundBlock> _labelsDefined = PooledDictionary<LabelSymbol, BoundBlock>.GetInstance();

		// Token: 0x04000E0C RID: 3596
		private readonly PooledHashSet<LabelSymbol> _labelsUsed = PooledHashSet<LabelSymbol>.GetInstance();

		// Token: 0x04000E0D RID: 3597
		protected bool _convertInsufficientExecutionStackExceptionToCancelledByStackGuardException;

		// Token: 0x04000E0E RID: 3598
		[TupleElementNames(new string[] { "symbol", "block" })]
		private readonly ArrayBuilder<ValueTuple<LocalSymbol, BoundBlock>> _usingDeclarations = ArrayBuilder<ValueTuple<LocalSymbol, BoundBlock>>.GetInstance();

		// Token: 0x04000E0F RID: 3599
		private BoundBlock _currentBlock;

		// Token: 0x020006E2 RID: 1762
		internal struct LocalState : AbstractFlowPass<ControlFlowPass.LocalState, ControlFlowPass.LocalFunctionState>.ILocalState
		{
			// Token: 0x060084B4 RID: 33972 RVA: 0x001E0E0D File Offset: 0x001DF00D
			internal LocalState(bool live, bool reported)
			{
				this.Alive = live;
				this.Reported = reported;
			}

			// Token: 0x060084B5 RID: 33973 RVA: 0x001E0E1D File Offset: 0x001DF01D
			public ControlFlowPass.LocalState Clone()
			{
				return this;
			}

			// Token: 0x170026A5 RID: 9893
			// (get) Token: 0x060084B6 RID: 33974 RVA: 0x001E0E25 File Offset: 0x001DF025
			public bool Reachable
			{
				get
				{
					return this.Alive;
				}
			}

			// Token: 0x040021D5 RID: 8661
			internal bool Alive;

			// Token: 0x040021D6 RID: 8662
			internal bool Reported;
		}

		// Token: 0x020006E3 RID: 1763
		internal sealed class LocalFunctionState : AbstractFlowPass<ControlFlowPass.LocalState, ControlFlowPass.LocalFunctionState>.AbstractLocalFunctionState
		{
			// Token: 0x060084B7 RID: 33975 RVA: 0x001E0E2D File Offset: 0x001DF02D
			public LocalFunctionState(ControlFlowPass.LocalState unreachableState)
				: base(unreachableState.Clone(), unreachableState.Clone())
			{
			}
		}
	}
}
