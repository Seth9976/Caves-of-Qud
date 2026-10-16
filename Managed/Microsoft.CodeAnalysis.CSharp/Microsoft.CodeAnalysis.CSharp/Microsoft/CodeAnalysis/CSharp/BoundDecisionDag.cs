using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000087 RID: 135
	internal sealed class BoundDecisionDag : BoundNode
	{
		// Token: 0x06000B3F RID: 2879 RVA: 0x00052210 File Offset: 0x00050410
		internal static ImmutableArray<BoundDecisionDagNode> Successors(BoundDecisionDagNode node)
		{
			BoundEvaluationDecisionDagNode boundEvaluationDecisionDagNode = node as BoundEvaluationDecisionDagNode;
			if (boundEvaluationDecisionDagNode != null)
			{
				return ImmutableArray.Create<BoundDecisionDagNode>(boundEvaluationDecisionDagNode.Next);
			}
			BoundTestDecisionDagNode boundTestDecisionDagNode = node as BoundTestDecisionDagNode;
			if (boundTestDecisionDagNode != null)
			{
				return ImmutableArray.Create<BoundDecisionDagNode>(boundTestDecisionDagNode.WhenFalse, boundTestDecisionDagNode.WhenTrue);
			}
			if (node is BoundLeafDecisionDagNode)
			{
				return ImmutableArray<BoundDecisionDagNode>.Empty;
			}
			BoundWhenDecisionDagNode boundWhenDecisionDagNode = node as BoundWhenDecisionDagNode;
			if (boundWhenDecisionDagNode == null)
			{
				throw ExceptionUtilities.UnexpectedValue(node.Kind);
			}
			if (boundWhenDecisionDagNode.WhenFalse == null)
			{
				return ImmutableArray.Create<BoundDecisionDagNode>(boundWhenDecisionDagNode.WhenTrue);
			}
			return ImmutableArray.Create<BoundDecisionDagNode>(boundWhenDecisionDagNode.WhenTrue, boundWhenDecisionDagNode.WhenFalse);
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x000522A0 File Offset: 0x000504A0
		public ImmutableHashSet<LabelSymbol> ReachableLabels
		{
			get
			{
				if (this._reachableLabels == null)
				{
					ImmutableHashSet<LabelSymbol>.Builder builder = ImmutableHashSet.CreateBuilder<LabelSymbol>(SymbolEqualityComparer.ConsiderEverything);
					foreach (BoundDecisionDagNode boundDecisionDagNode in this.TopologicallySortedNodes)
					{
						BoundLeafDecisionDagNode boundLeafDecisionDagNode = boundDecisionDagNode as BoundLeafDecisionDagNode;
						if (boundLeafDecisionDagNode != null)
						{
							builder.Add(boundLeafDecisionDagNode.Label);
						}
					}
					this._reachableLabels = builder.ToImmutableHashSet<LabelSymbol>();
				}
				return this._reachableLabels;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x00052309 File Offset: 0x00050509
		public ImmutableArray<BoundDecisionDagNode> TopologicallySortedNodes
		{
			get
			{
				if (this._topologicallySortedNodes.IsDefault)
				{
					TopologicalSort.TryIterativeSort<BoundDecisionDagNode>(new BoundDecisionDagNode[] { this.RootNode }, new Func<BoundDecisionDagNode, ImmutableArray<BoundDecisionDagNode>>(BoundDecisionDag.Successors), out this._topologicallySortedNodes);
				}
				return this._topologicallySortedNodes;
			}
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x00052348 File Offset: 0x00050548
		public BoundDecisionDag Rewrite(Func<BoundDecisionDagNode, Func<BoundDecisionDagNode, BoundDecisionDagNode>, BoundDecisionDagNode> makeReplacement)
		{
			ImmutableArray<BoundDecisionDagNode> topologicallySortedNodes = this.TopologicallySortedNodes;
			PooledDictionary<BoundDecisionDagNode, BoundDecisionDagNode> replacement = PooledDictionary<BoundDecisionDagNode, BoundDecisionDagNode>.GetInstance();
			Func<BoundDecisionDagNode, BoundDecisionDagNode> func = (BoundDecisionDagNode n) => replacement[n];
			for (int i = topologicallySortedNodes.Length - 1; i >= 0; i--)
			{
				BoundDecisionDagNode boundDecisionDagNode = topologicallySortedNodes[i];
				BoundDecisionDagNode boundDecisionDagNode2 = makeReplacement.Invoke(boundDecisionDagNode, func);
				replacement.Add(boundDecisionDagNode, boundDecisionDagNode2);
			}
			BoundDecisionDagNode boundDecisionDagNode3 = replacement[this.RootNode];
			replacement.Free();
			return this.Update(boundDecisionDagNode3);
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x000523DC File Offset: 0x000505DC
		public static BoundDecisionDagNode TrivialReplacement(BoundDecisionDagNode dag, Func<BoundDecisionDagNode, BoundDecisionDagNode> replacement)
		{
			BoundEvaluationDecisionDagNode boundEvaluationDecisionDagNode = dag as BoundEvaluationDecisionDagNode;
			if (boundEvaluationDecisionDagNode != null)
			{
				return boundEvaluationDecisionDagNode.Update(boundEvaluationDecisionDagNode.Evaluation, replacement.Invoke(boundEvaluationDecisionDagNode.Next));
			}
			BoundTestDecisionDagNode boundTestDecisionDagNode = dag as BoundTestDecisionDagNode;
			if (boundTestDecisionDagNode != null)
			{
				return boundTestDecisionDagNode.Update(boundTestDecisionDagNode.Test, replacement.Invoke(boundTestDecisionDagNode.WhenTrue), replacement.Invoke(boundTestDecisionDagNode.WhenFalse));
			}
			BoundWhenDecisionDagNode boundWhenDecisionDagNode = dag as BoundWhenDecisionDagNode;
			if (boundWhenDecisionDagNode != null)
			{
				return boundWhenDecisionDagNode.Update(boundWhenDecisionDagNode.Bindings, boundWhenDecisionDagNode.WhenExpression, replacement.Invoke(boundWhenDecisionDagNode.WhenTrue), (boundWhenDecisionDagNode.WhenFalse != null) ? replacement.Invoke(boundWhenDecisionDagNode.WhenFalse) : null);
			}
			BoundLeafDecisionDagNode boundLeafDecisionDagNode = dag as BoundLeafDecisionDagNode;
			if (boundLeafDecisionDagNode == null)
			{
				throw ExceptionUtilities.UnexpectedValue(dag);
			}
			return boundLeafDecisionDagNode;
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00052490 File Offset: 0x00050690
		public BoundDecisionDag SimplifyDecisionDagIfConstantInput(BoundExpression input)
		{
			BoundDecisionDag.<>c__DisplayClass9_0 CS$<>8__locals1 = new BoundDecisionDag.<>c__DisplayClass9_0();
			CS$<>8__locals1.input = input;
			if (CS$<>8__locals1.input.ConstantValue == null)
			{
				return this;
			}
			CS$<>8__locals1.inputConstant = CS$<>8__locals1.input.ConstantValue;
			return this.Rewrite(new Func<BoundDecisionDagNode, Func<BoundDecisionDagNode, BoundDecisionDagNode>, BoundDecisionDagNode>(CS$<>8__locals1.<SimplifyDecisionDagIfConstantInput>g__makeReplacement|0));
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x000524E2 File Offset: 0x000506E2
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public BoundDecisionDag(SyntaxNode syntax, BoundDecisionDagNode rootNode, bool hasErrors = false)
			: base(BoundKind.DecisionDag, syntax, hasErrors || rootNode.HasErrors())
		{
			this.RootNode = rootNode;
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x00052500 File Offset: 0x00050700
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public BoundDecisionDagNode RootNode
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get;
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00052508 File Offset: 0x00050708
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitDecisionDag(this);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00052511 File Offset: 0x00050711
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public BoundDecisionDag Update(BoundDecisionDagNode rootNode)
		{
			if (rootNode != this.RootNode)
			{
				BoundDecisionDag boundDecisionDag = new BoundDecisionDag(this.Syntax, rootNode, base.HasErrors);
				boundDecisionDag.CopyAttributes(this);
				return boundDecisionDag;
			}
			return this;
		}

		// Token: 0x0400048E RID: 1166
		private ImmutableHashSet<LabelSymbol> _reachableLabels;

		// Token: 0x0400048F RID: 1167
		private ImmutableArray<BoundDecisionDagNode> _topologicallySortedNodes;
	}
}
