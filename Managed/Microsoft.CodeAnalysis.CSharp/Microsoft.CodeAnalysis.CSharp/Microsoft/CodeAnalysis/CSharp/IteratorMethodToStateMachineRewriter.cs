using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001FF RID: 511
	internal sealed class IteratorMethodToStateMachineRewriter : MethodToStateMachineRewriter
	{
		// Token: 0x06002388 RID: 9096 RVA: 0x000B8DB0 File Offset: 0x000B6FB0
		internal IteratorMethodToStateMachineRewriter(SyntheticBoundNodeFactory F, MethodSymbol originalMethod, FieldSymbol state, FieldSymbol current, IReadOnlySet<Symbol> hoistedVariables, IReadOnlyDictionary<Symbol, CapturedSymbolReplacement> nonReusableLocalProxies, SynthesizedLocalOrdinalsDispenser synthesizedLocalOrdinals, VariableSlotAllocator slotAllocatorOpt, int nextFreeHoistedLocalSlot, BindingDiagnosticBag diagnostics)
			: base(F, originalMethod, state, hoistedVariables, nonReusableLocalProxies, synthesizedLocalOrdinals, slotAllocatorOpt, nextFreeHoistedLocalSlot, diagnostics, false)
		{
			this._current = current;
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x000B8DF0 File Offset: 0x000B6FF0
		internal void GenerateMoveNextAndDispose(BoundStatement body, SynthesizedImplementationMethod moveNextMethod, SynthesizedImplementationMethod disposeMethod)
		{
			this._yieldsInTryAnalysis = new IteratorMethodToStateMachineRewriter.YieldsInTryAnalysis(body);
			if (this._yieldsInTryAnalysis.ContainsYieldsInTrys())
			{
				this._tryNestingLevel++;
			}
			this.F.CurrentFunction = moveNextMethod;
			int num;
			GeneratedLabelSymbol generatedLabelSymbol;
			base.AddState(out num, out generatedLabelSymbol);
			BoundStatement boundStatement = (BoundStatement)this.Visit(body);
			boundStatement = this.F.Block((this.cachedThis == null) ? ImmutableArray.Create<LocalSymbol>(this.cachedState) : ImmutableArray.Create<LocalSymbol>(this.cachedState, this.cachedThis), new BoundStatement[]
			{
				this.F.HiddenSequencePoint(null),
				this.F.Assignment(this.F.Local(this.cachedState), this.F.Field(this.F.This(), this.stateField), false),
				base.CacheThisIfNeeded(),
				base.Dispatch(),
				this.GenerateReturn(true),
				this.F.Label(generatedLabelSymbol),
				this.F.Assignment(this.F.Field(this.F.This(), this.stateField), this.F.Literal(-1), false),
				boundStatement
			});
			if (this._yieldsInTryAnalysis.ContainsYieldsInTrys())
			{
				BoundBlock boundBlock = this.F.Block(new BoundStatement[] { this.F.ExpressionStatement(this.F.Call(this.F.This(), disposeMethod)) });
				boundStatement = this.F.Fault((BoundBlock)boundStatement, boundBlock);
			}
			boundStatement = this.HandleReturn(boundStatement);
			this.F.CloseMethod(this.F.SequencePoint(body.Syntax, boundStatement));
			this.F.CurrentFunction = disposeMethod;
			IteratorMethodToStateMachineRewriter.IteratorFinallyFrame currentFinallyFrame = this._currentFinallyFrame;
			if (currentFinallyFrame.knownStates == null)
			{
				this.F.CloseMethod(this.F.Return(null));
				return;
			}
			LocalSymbol localSymbol = this.F.SynthesizedLocal(this.stateField.Type, null, false, RefKind.None, SynthesizedLocalKind.LoweringTemp);
			BoundLocal boundLocal = this.F.Local(localSymbol);
			BoundBlock boundBlock2 = this.F.Block(ImmutableArray.Create<LocalSymbol>(localSymbol), new BoundStatement[]
			{
				this.F.Assignment(this.F.Local(localSymbol), this.F.Field(this.F.This(), this.stateField), false),
				this.EmitFinallyFrame(currentFinallyFrame, boundLocal),
				this.F.Return(null)
			});
			this.F.CloseMethod(boundBlock2);
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x000B9088 File Offset: 0x000B7288
		private BoundStatement HandleReturn(BoundStatement newBody)
		{
			if (this._exitLabel == null)
			{
				newBody = this.F.Block(new BoundStatement[]
				{
					newBody,
					this.F.Return(this.F.Literal(false))
				});
			}
			else
			{
				newBody = this.F.Block(ImmutableArray.Create<LocalSymbol>(this._methodValue), new BoundStatement[]
				{
					newBody,
					this.F.Assignment(this.F.Local(this._methodValue), this.F.Literal(true), false),
					this.F.Label(this._exitLabel),
					this.F.Return(this.F.Local(this._methodValue))
				});
			}
			return newBody;
		}

		// Token: 0x0600238B RID: 9099 RVA: 0x000B9158 File Offset: 0x000B7358
		private BoundStatement EmitFinallyFrame(IteratorMethodToStateMachineRewriter.IteratorFinallyFrame frame, BoundLocal state)
		{
			BoundStatement boundStatement = null;
			if (frame.knownStates != null)
			{
				GeneratedLabelSymbol breakLabel = this.F.GenerateLabel("break");
				IEnumerable<SyntheticBoundNodeFactory.SyntheticSwitchSection> enumerable = Enumerable.Select<IGrouping<IteratorMethodToStateMachineRewriter.IteratorFinallyFrame, int>, SyntheticBoundNodeFactory.SyntheticSwitchSection>(Enumerable.GroupBy<KeyValuePair<int, IteratorMethodToStateMachineRewriter.IteratorFinallyFrame>, IteratorMethodToStateMachineRewriter.IteratorFinallyFrame, int>(frame.knownStates, delegate(KeyValuePair<int, IteratorMethodToStateMachineRewriter.IteratorFinallyFrame> ft)
				{
					KeyValuePair<int, IteratorMethodToStateMachineRewriter.IteratorFinallyFrame> keyValuePair = ft;
					return keyValuePair.Value;
				}, delegate(KeyValuePair<int, IteratorMethodToStateMachineRewriter.IteratorFinallyFrame> ft)
				{
					KeyValuePair<int, IteratorMethodToStateMachineRewriter.IteratorFinallyFrame> keyValuePair2 = ft;
					return keyValuePair2.Key;
				}), (IGrouping<IteratorMethodToStateMachineRewriter.IteratorFinallyFrame, int> g) => this.F.SwitchSection(new List<int>(g), new BoundStatement[]
				{
					this.EmitFinallyFrame(g.Key, state),
					this.F.Goto(breakLabel)
				}));
				boundStatement = this.F.Block(new BoundStatement[]
				{
					this.F.Switch(state, enumerable.ToImmutableArray<SyntheticBoundNodeFactory.SyntheticSwitchSection>()),
					this.F.Label(breakLabel)
				});
			}
			if (!frame.IsRoot())
			{
				BoundBlock boundBlock = ((boundStatement != null) ? this.F.Block(new BoundStatement[] { boundStatement }) : this.F.Block());
				boundStatement = this.F.Try(boundBlock, ImmutableArray<BoundCatchBlock>.Empty, this.F.Block(new BoundStatement[] { this.F.ExpressionStatement(this.F.Call(this.F.This(), frame.handler)) }), null);
			}
			return boundStatement;
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x000B92B4 File Offset: 0x000B74B4
		protected override BoundStatement GenerateReturn(bool finished)
		{
			BoundLiteral boundLiteral = this.F.Literal(!finished);
			if (this._tryNestingLevel == 0)
			{
				return this.F.Return(boundLiteral);
			}
			if (this._exitLabel == null)
			{
				this._exitLabel = this.F.GenerateLabel("exitLabel");
				this._methodValue = this.F.SynthesizedLocal(boundLiteral.Type, null, false, RefKind.None, SynthesizedLocalKind.LoweringTemp);
			}
			BoundGotoStatement boundGotoStatement = this.F.Goto(this._exitLabel);
			if (finished)
			{
				boundGotoStatement = (BoundGotoStatement)this.VisitGotoStatement(boundGotoStatement);
			}
			return this.F.Block(new BoundStatement[]
			{
				this.F.Assignment(this.F.Local(this._methodValue), boundLiteral, false),
				boundGotoStatement
			});
		}

		// Token: 0x0600238D RID: 9101 RVA: 0x000B9378 File Offset: 0x000B7578
		public override BoundNode VisitYieldBreakStatement(BoundYieldBreakStatement node)
		{
			return this.GenerateReturn(true);
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x000B9384 File Offset: 0x000B7584
		public override BoundNode VisitYieldReturnStatement(BoundYieldReturnStatement node)
		{
			int num;
			GeneratedLabelSymbol generatedLabelSymbol;
			base.AddState(out num, out generatedLabelSymbol);
			this._currentFinallyFrame.AddState(num);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			return this.F.Block(new BoundStatement[]
			{
				this.F.Assignment(this.F.Field(this.F.This(), this._current), boundExpression, false),
				this.F.Assignment(this.F.Field(this.F.This(), this.stateField), this.F.Literal(num), false),
				this.GenerateReturn(false),
				this.F.Label(generatedLabelSymbol),
				this.F.HiddenSequencePoint(null),
				this.F.Assignment(this.F.Field(this.F.This(), this.stateField), this.F.Literal(this._currentFinallyFrame.finalizeState), false)
			});
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x000B9498 File Offset: 0x000B7698
		public override BoundNode VisitGotoStatement(BoundGotoStatement node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.CaseExpressionOpt);
			BoundLabel boundLabel = (BoundLabel)this.Visit(node.LabelExpressionOpt);
			LabelSymbol labelSymbol = this._currentFinallyFrame.ProxyLabelIfNeeded(node.Label);
			return node.Update(labelSymbol, boundExpression, boundLabel);
		}

		// Token: 0x06002390 RID: 9104 RVA: 0x000B94E4 File Offset: 0x000B76E4
		public override BoundNode VisitConditionalGoto(BoundConditionalGoto node)
		{
			return base.VisitConditionalGoto(node);
		}

		// Token: 0x06002391 RID: 9105 RVA: 0x000B94F0 File Offset: 0x000B76F0
		public override BoundNode VisitTryStatement(BoundTryStatement node)
		{
			if (!this.ContainsYields(node))
			{
				this._tryNestingLevel++;
				BoundNode boundNode = node.Update((BoundBlock)this.Visit(node.TryBlock), base.VisitList<BoundCatchBlock>(node.CatchBlocks), (BoundBlock)this.Visit(node.FinallyBlockOpt), node.FinallyLabelOpt, node.PreferFaultHandler);
				this._tryNestingLevel--;
				return boundNode;
			}
			IteratorMethodToStateMachineRewriter.IteratorFinallyFrame iteratorFinallyFrame = this.PushFrame(node);
			this._tryNestingLevel++;
			BoundStatement boundStatement = (BoundStatement)this.Visit(node.TryBlock);
			IteratorFinallyMethodSymbol handler = iteratorFinallyFrame.handler;
			MethodSymbol currentFunction = this.F.CurrentFunction;
			this.F.CurrentFunction = handler;
			BoundStatement boundStatement2 = (BoundStatement)this.Visit(node.FinallyBlockOpt);
			this._tryNestingLevel--;
			this.PopFrame();
			boundStatement2 = this.F.Block((this.cachedThis != null) ? ImmutableArray.Create<LocalSymbol>(this.cachedThis) : ImmutableArray<LocalSymbol>.Empty, new BoundStatement[]
			{
				this.F.Assignment(this.F.Field(this.F.This(), this.stateField), this.F.Literal(iteratorFinallyFrame.parent.finalizeState), false),
				base.CacheThisIfNeeded(),
				boundStatement2,
				this.F.Return(null)
			});
			this.F.CloseMethod(boundStatement2);
			this.F.CurrentFunction = currentFunction;
			ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance();
			instance.Add(this.F.Assignment(this.F.Field(this.F.This(), this.stateField), this.F.Literal(iteratorFinallyFrame.finalizeState), false));
			instance.Add(boundStatement);
			instance.Add(this.F.ExpressionStatement(this.F.Call(this.F.This(), handler)));
			if (iteratorFinallyFrame.proxyLabels != null)
			{
				GeneratedLabelSymbol generatedLabelSymbol = this.F.GenerateLabel("dropThrough");
				instance.Add(this.F.Goto(generatedLabelSymbol));
				IteratorMethodToStateMachineRewriter.IteratorFinallyFrame parent = iteratorFinallyFrame.parent;
				foreach (KeyValuePair<LabelSymbol, LabelSymbol> keyValuePair in iteratorFinallyFrame.proxyLabels)
				{
					LabelSymbol value = keyValuePair.Value;
					LabelSymbol key = keyValuePair.Key;
					instance.Add(this.F.Label(value));
					instance.Add(this.F.ExpressionStatement(this.F.Call(this.F.This(), handler)));
					LabelSymbol labelSymbol = parent.ProxyLabelIfNeeded(key);
					instance.Add(this.F.Goto(labelSymbol));
				}
				instance.Add(this.F.Label(generatedLabelSymbol));
			}
			return this.F.Block(instance.ToImmutableAndFree());
		}

		// Token: 0x06002392 RID: 9106 RVA: 0x000B97FC File Offset: 0x000B79FC
		private IteratorMethodToStateMachineRewriter.IteratorFinallyFrame PushFrame(BoundTryStatement statement)
		{
			int nextFinalizeState = this._nextFinalizeState;
			this._nextFinalizeState = nextFinalizeState - 1;
			int num = nextFinalizeState;
			IteratorFinallyMethodSymbol iteratorFinallyMethodSymbol = this.MakeSynthesizedFinally(num);
			IteratorMethodToStateMachineRewriter.IteratorFinallyFrame iteratorFinallyFrame = new IteratorMethodToStateMachineRewriter.IteratorFinallyFrame(this._currentFinallyFrame, num, iteratorFinallyMethodSymbol, this._yieldsInTryAnalysis.Labels(statement));
			iteratorFinallyFrame.AddState(num);
			this._currentFinallyFrame = iteratorFinallyFrame;
			return iteratorFinallyFrame;
		}

		// Token: 0x06002393 RID: 9107 RVA: 0x000B984C File Offset: 0x000B7A4C
		private void PopFrame()
		{
			IteratorMethodToStateMachineRewriter.IteratorFinallyFrame currentFinallyFrame = this._currentFinallyFrame;
			this._currentFinallyFrame = currentFinallyFrame.parent;
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x000B986C File Offset: 0x000B7A6C
		private bool ContainsYields(BoundTryStatement statement)
		{
			return this._yieldsInTryAnalysis.ContainsYields(statement);
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x000B987C File Offset: 0x000B7A7C
		private IteratorFinallyMethodSymbol MakeSynthesizedFinally(int state)
		{
			IteratorStateMachine iteratorStateMachine = (IteratorStateMachine)this.F.CurrentType;
			IteratorFinallyMethodSymbol iteratorFinallyMethodSymbol = new IteratorFinallyMethodSymbol(iteratorStateMachine, GeneratedNames.MakeIteratorFinallyMethodName(state));
			this.F.ModuleBuilderOpt.AddSynthesizedDefinition(iteratorStateMachine, iteratorFinallyMethodSymbol.GetCciAdapter());
			return iteratorFinallyMethodSymbol;
		}

		// Token: 0x040010B8 RID: 4280
		private readonly FieldSymbol _current;

		// Token: 0x040010B9 RID: 4281
		private IteratorMethodToStateMachineRewriter.YieldsInTryAnalysis _yieldsInTryAnalysis;

		// Token: 0x040010BA RID: 4282
		private int _tryNestingLevel;

		// Token: 0x040010BB RID: 4283
		private LabelSymbol _exitLabel;

		// Token: 0x040010BC RID: 4284
		private LocalSymbol _methodValue;

		// Token: 0x040010BD RID: 4285
		private IteratorMethodToStateMachineRewriter.IteratorFinallyFrame _currentFinallyFrame = new IteratorMethodToStateMachineRewriter.IteratorFinallyFrame();

		// Token: 0x040010BE RID: 4286
		private int _nextFinalizeState = -3;

		// Token: 0x02000725 RID: 1829
		private sealed class IteratorFinallyFrame
		{
			// Token: 0x060085D8 RID: 34264 RVA: 0x001E4F08 File Offset: 0x001E3108
			public IteratorFinallyFrame(IteratorMethodToStateMachineRewriter.IteratorFinallyFrame parent, int finalizeState, IteratorFinallyMethodSymbol handler, HashSet<LabelSymbol> labels)
			{
				this.parent = parent;
				this.finalizeState = finalizeState;
				this.handler = handler;
				this.labels = labels;
			}

			// Token: 0x060085D9 RID: 34265 RVA: 0x001E4F2D File Offset: 0x001E312D
			public IteratorFinallyFrame()
			{
				this.finalizeState = -1;
			}

			// Token: 0x060085DA RID: 34266 RVA: 0x001E4F3C File Offset: 0x001E313C
			public bool IsRoot()
			{
				return this.parent == null;
			}

			// Token: 0x060085DB RID: 34267 RVA: 0x001E4F47 File Offset: 0x001E3147
			public void AddState(int state)
			{
				if (this.parent != null)
				{
					this.parent.AddState(state, this);
				}
			}

			// Token: 0x060085DC RID: 34268 RVA: 0x001E4F60 File Offset: 0x001E3160
			private void AddState(int state, IteratorMethodToStateMachineRewriter.IteratorFinallyFrame innerHandler)
			{
				Dictionary<int, IteratorMethodToStateMachineRewriter.IteratorFinallyFrame> dictionary = this.knownStates;
				if (dictionary == null)
				{
					dictionary = (this.knownStates = new Dictionary<int, IteratorMethodToStateMachineRewriter.IteratorFinallyFrame>());
				}
				dictionary.Add(state, innerHandler);
				if (this.parent != null)
				{
					this.parent.AddState(state, this);
				}
			}

			// Token: 0x060085DD RID: 34269 RVA: 0x001E4FA4 File Offset: 0x001E31A4
			public LabelSymbol ProxyLabelIfNeeded(LabelSymbol label)
			{
				if (this.IsRoot() || (this.labels != null && this.labels.Contains(label)))
				{
					return label;
				}
				Dictionary<LabelSymbol, LabelSymbol> dictionary = this.proxyLabels;
				if (dictionary == null)
				{
					dictionary = (this.proxyLabels = new Dictionary<LabelSymbol, LabelSymbol>());
				}
				LabelSymbol labelSymbol;
				if (!dictionary.TryGetValue(label, ref labelSymbol))
				{
					labelSymbol = new GeneratedLabelSymbol("proxy" + label.Name);
					dictionary.Add(label, labelSymbol);
				}
				return labelSymbol;
			}

			// Token: 0x040022A7 RID: 8871
			public readonly int finalizeState;

			// Token: 0x040022A8 RID: 8872
			public readonly IteratorMethodToStateMachineRewriter.IteratorFinallyFrame parent;

			// Token: 0x040022A9 RID: 8873
			public readonly IteratorFinallyMethodSymbol handler;

			// Token: 0x040022AA RID: 8874
			public Dictionary<int, IteratorMethodToStateMachineRewriter.IteratorFinallyFrame> knownStates;

			// Token: 0x040022AB RID: 8875
			public readonly HashSet<LabelSymbol> labels;

			// Token: 0x040022AC RID: 8876
			public Dictionary<LabelSymbol, LabelSymbol> proxyLabels;
		}

		// Token: 0x02000726 RID: 1830
		private sealed class YieldsInTryAnalysis : LabelCollector
		{
			// Token: 0x060085DE RID: 34270 RVA: 0x001E5012 File Offset: 0x001E3212
			public YieldsInTryAnalysis(BoundStatement body)
			{
				this._seenYield = false;
				this.Visit(body);
			}

			// Token: 0x060085DF RID: 34271 RVA: 0x001E5029 File Offset: 0x001E3229
			public bool ContainsYields(BoundTryStatement statement)
			{
				return this._labelsInYieldingTrys != null && this._labelsInYieldingTrys.ContainsKey(statement);
			}

			// Token: 0x060085E0 RID: 34272 RVA: 0x001E5041 File Offset: 0x001E3241
			public bool ContainsYieldsInTrys()
			{
				return this._labelsInYieldingTrys != null;
			}

			// Token: 0x060085E1 RID: 34273 RVA: 0x001E504C File Offset: 0x001E324C
			internal HashSet<LabelSymbol> Labels(BoundTryStatement statement)
			{
				return this._labelsInYieldingTrys[statement];
			}

			// Token: 0x060085E2 RID: 34274 RVA: 0x001E505C File Offset: 0x001E325C
			public override BoundNode VisitTryStatement(BoundTryStatement node)
			{
				bool seenYield = this._seenYield;
				HashSet<LabelSymbol> currentLabels = this.currentLabels;
				this._seenYield = false;
				this.currentLabels = null;
				base.VisitTryStatement(node);
				if (this._seenYield)
				{
					Dictionary<BoundTryStatement, HashSet<LabelSymbol>> dictionary = this._labelsInYieldingTrys;
					if (dictionary == null)
					{
						dictionary = (this._labelsInYieldingTrys = new Dictionary<BoundTryStatement, HashSet<LabelSymbol>>());
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
				this._seenYield = this._seenYield || seenYield;
				return null;
			}

			// Token: 0x060085E3 RID: 34275 RVA: 0x001E50F1 File Offset: 0x001E32F1
			public override BoundNode VisitYieldReturnStatement(BoundYieldReturnStatement node)
			{
				this._seenYield = true;
				return base.VisitYieldReturnStatement(node);
			}

			// Token: 0x060085E4 RID: 34276 RVA: 0x001E5101 File Offset: 0x001E3301
			public override BoundNode VisitExpressionStatement(BoundExpressionStatement node)
			{
				return null;
			}

			// Token: 0x040022AD RID: 8877
			private Dictionary<BoundTryStatement, HashSet<LabelSymbol>> _labelsInYieldingTrys;

			// Token: 0x040022AE RID: 8878
			private bool _seenYield;
		}
	}
}
