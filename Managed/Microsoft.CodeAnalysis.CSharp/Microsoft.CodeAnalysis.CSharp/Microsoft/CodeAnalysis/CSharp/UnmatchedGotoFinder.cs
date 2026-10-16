using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000217 RID: 535
	internal sealed class UnmatchedGotoFinder : BoundTreeWalkerWithStackGuardWithoutRecursionOnTheLeftOfBinaryOperator
	{
		// Token: 0x060026D0 RID: 9936 RVA: 0x000D23E3 File Offset: 0x000D05E3
		private UnmatchedGotoFinder(Dictionary<BoundNode, HashSet<LabelSymbol>> unmatchedLabelsCache, int recursionDepth)
			: base(recursionDepth)
		{
			this._unmatchedLabelsCache = unmatchedLabelsCache;
		}

		// Token: 0x060026D1 RID: 9937 RVA: 0x000D23F4 File Offset: 0x000D05F4
		public static HashSet<LabelSymbol> Find(BoundNode node, Dictionary<BoundNode, HashSet<LabelSymbol>> unmatchedLabelsCache, int recursionDepth)
		{
			UnmatchedGotoFinder unmatchedGotoFinder = new UnmatchedGotoFinder(unmatchedLabelsCache, recursionDepth);
			unmatchedGotoFinder.Visit(node);
			HashSet<LabelSymbol> gotos = unmatchedGotoFinder._gotos;
			HashSet<LabelSymbol> targets = unmatchedGotoFinder._targets;
			if (gotos != null && targets != null)
			{
				gotos.RemoveAll<LabelSymbol>(targets);
			}
			return gotos;
		}

		// Token: 0x060026D2 RID: 9938 RVA: 0x000D242C File Offset: 0x000D062C
		public override BoundNode Visit(BoundNode node)
		{
			HashSet<LabelSymbol> hashSet;
			if (node != null && this._unmatchedLabelsCache.TryGetValue(node, ref hashSet))
			{
				if (hashSet != null)
				{
					foreach (LabelSymbol labelSymbol in hashSet)
					{
						this.AddGoto(labelSymbol);
					}
				}
				return null;
			}
			return base.Visit(node);
		}

		// Token: 0x060026D3 RID: 9939 RVA: 0x000D249C File Offset: 0x000D069C
		public override BoundNode VisitGotoStatement(BoundGotoStatement node)
		{
			this.AddGoto(node.Label);
			return base.VisitGotoStatement(node);
		}

		// Token: 0x060026D4 RID: 9940 RVA: 0x000D24B1 File Offset: 0x000D06B1
		public override BoundNode VisitConditionalGoto(BoundConditionalGoto node)
		{
			this.AddGoto(node.Label);
			return base.VisitConditionalGoto(node);
		}

		// Token: 0x060026D5 RID: 9941 RVA: 0x000D24C8 File Offset: 0x000D06C8
		public override BoundNode VisitSwitchDispatch(BoundSwitchDispatch node)
		{
			this.AddGoto(node.DefaultLabel);
			foreach (ValueTuple<ConstantValue, LabelSymbol> valueTuple in node.Cases)
			{
				LabelSymbol item = valueTuple.Item2;
				this.AddGoto(item);
			}
			return base.VisitSwitchDispatch(node);
		}

		// Token: 0x060026D6 RID: 9942 RVA: 0x000D2516 File Offset: 0x000D0716
		public override BoundNode VisitLabelStatement(BoundLabelStatement node)
		{
			this.AddTarget(node.Label);
			return base.VisitLabelStatement(node);
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x000D252B File Offset: 0x000D072B
		public override BoundNode VisitLabeledStatement(BoundLabeledStatement node)
		{
			this.AddTarget(node.Label);
			return base.VisitLabeledStatement(node);
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x000D2540 File Offset: 0x000D0740
		private void AddGoto(LabelSymbol label)
		{
			if (this._gotos == null)
			{
				this._gotos = new HashSet<LabelSymbol>();
			}
			this._gotos.Add(label);
		}

		// Token: 0x060026D9 RID: 9945 RVA: 0x000D2562 File Offset: 0x000D0762
		private void AddTarget(LabelSymbol label)
		{
			if (this._targets == null)
			{
				this._targets = new HashSet<LabelSymbol>();
			}
			this._targets.Add(label);
		}

		// Token: 0x04001123 RID: 4387
		private readonly Dictionary<BoundNode, HashSet<LabelSymbol>> _unmatchedLabelsCache;

		// Token: 0x04001124 RID: 4388
		private HashSet<LabelSymbol> _gotos;

		// Token: 0x04001125 RID: 4389
		private HashSet<LabelSymbol> _targets;
	}
}
