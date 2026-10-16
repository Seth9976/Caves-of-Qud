using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.Collections;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200027C RID: 636
	[NullableContext(1)]
	[Nullable(0)]
	public static class SyntaxNodeExtensions
	{
		// Token: 0x0600193D RID: 6461 RVA: 0x0004BD02 File Offset: 0x00049F02
		public static TRoot ReplaceSyntax<[Nullable(0)] TRoot>(this TRoot root, IEnumerable<SyntaxNode> nodes, Func<SyntaxNode, SyntaxNode, SyntaxNode> computeReplacementNode, IEnumerable<SyntaxToken> tokens, Func<SyntaxToken, SyntaxToken, SyntaxToken> computeReplacementToken, IEnumerable<SyntaxTrivia> trivia, Func<SyntaxTrivia, SyntaxTrivia, SyntaxTrivia> computeReplacementTrivia) where TRoot : SyntaxNode
		{
			return (TRoot)((object)root.ReplaceCore<SyntaxNode>(nodes, computeReplacementNode, tokens, computeReplacementToken, trivia, computeReplacementTrivia));
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x0004BD1D File Offset: 0x00049F1D
		public static TRoot ReplaceNodes<[Nullable(0)] TRoot, [Nullable(0)] TNode>(this TRoot root, IEnumerable<TNode> nodes, Func<TNode, TNode, SyntaxNode> computeReplacementNode) where TRoot : SyntaxNode where TNode : SyntaxNode
		{
			return (TRoot)((object)root.ReplaceCore<TNode>(nodes, computeReplacementNode, null, null, null, null));
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x0004BD38 File Offset: 0x00049F38
		public static TRoot ReplaceNode<[Nullable(0)] TRoot>(this TRoot root, SyntaxNode oldNode, SyntaxNode newNode) where TRoot : SyntaxNode
		{
			if (oldNode == newNode)
			{
				return root;
			}
			return (TRoot)((object)root.ReplaceCore<SyntaxNode>(new SyntaxNode[] { oldNode }, (SyntaxNode o, SyntaxNode r) => newNode, null, null, null, null));
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x0004BD87 File Offset: 0x00049F87
		public static TRoot ReplaceNode<[Nullable(0)] TRoot>(this TRoot root, SyntaxNode oldNode, IEnumerable<SyntaxNode> newNodes) where TRoot : SyntaxNode
		{
			return (TRoot)((object)root.ReplaceNodeInListCore(oldNode, newNodes));
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x0004BD9B File Offset: 0x00049F9B
		public static TRoot InsertNodesBefore<[Nullable(0)] TRoot>(this TRoot root, SyntaxNode nodeInList, IEnumerable<SyntaxNode> newNodes) where TRoot : SyntaxNode
		{
			return (TRoot)((object)root.InsertNodesInListCore(nodeInList, newNodes, true));
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x0004BDB0 File Offset: 0x00049FB0
		public static TRoot InsertNodesAfter<[Nullable(0)] TRoot>(this TRoot root, SyntaxNode nodeInList, IEnumerable<SyntaxNode> newNodes) where TRoot : SyntaxNode
		{
			return (TRoot)((object)root.InsertNodesInListCore(nodeInList, newNodes, false));
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x0004BDC5 File Offset: 0x00049FC5
		public static TRoot ReplaceToken<[Nullable(0)] TRoot>(this TRoot root, SyntaxToken tokenInList, IEnumerable<SyntaxToken> newTokens) where TRoot : SyntaxNode
		{
			return (TRoot)((object)root.ReplaceTokenInListCore(tokenInList, newTokens));
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x0004BDD9 File Offset: 0x00049FD9
		public static TRoot InsertTokensBefore<[Nullable(0)] TRoot>(this TRoot root, SyntaxToken tokenInList, IEnumerable<SyntaxToken> newTokens) where TRoot : SyntaxNode
		{
			return (TRoot)((object)root.InsertTokensInListCore(tokenInList, newTokens, true));
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x0004BDEE File Offset: 0x00049FEE
		public static TRoot InsertTokensAfter<[Nullable(0)] TRoot>(this TRoot root, SyntaxToken tokenInList, IEnumerable<SyntaxToken> newTokens) where TRoot : SyntaxNode
		{
			return (TRoot)((object)root.InsertTokensInListCore(tokenInList, newTokens, false));
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x0004BE03 File Offset: 0x0004A003
		public static TRoot ReplaceTrivia<[Nullable(0)] TRoot>(this TRoot root, SyntaxTrivia oldTrivia, IEnumerable<SyntaxTrivia> newTrivia) where TRoot : SyntaxNode
		{
			return (TRoot)((object)root.ReplaceTriviaInListCore(oldTrivia, newTrivia));
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x0004BE17 File Offset: 0x0004A017
		public static TRoot InsertTriviaBefore<[Nullable(0)] TRoot>(this TRoot root, SyntaxTrivia trivia, IEnumerable<SyntaxTrivia> newTrivia) where TRoot : SyntaxNode
		{
			return (TRoot)((object)root.InsertTriviaInListCore(trivia, newTrivia, true));
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x0004BE2C File Offset: 0x0004A02C
		public static TRoot InsertTriviaAfter<[Nullable(0)] TRoot>(this TRoot root, SyntaxTrivia trivia, IEnumerable<SyntaxTrivia> newTrivia) where TRoot : SyntaxNode
		{
			return (TRoot)((object)root.InsertTriviaInListCore(trivia, newTrivia, false));
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x0004BE41 File Offset: 0x0004A041
		public static TRoot ReplaceTokens<[Nullable(0)] TRoot>(this TRoot root, IEnumerable<SyntaxToken> tokens, Func<SyntaxToken, SyntaxToken, SyntaxToken> computeReplacementToken) where TRoot : SyntaxNode
		{
			return (TRoot)((object)root.ReplaceCore<SyntaxNode>(null, null, tokens, computeReplacementToken, null, null));
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x0004BE5C File Offset: 0x0004A05C
		public static TRoot ReplaceToken<[Nullable(0)] TRoot>(this TRoot root, SyntaxToken oldToken, SyntaxToken newToken) where TRoot : SyntaxNode
		{
			return (TRoot)((object)root.ReplaceCore<SyntaxNode>(null, null, new SyntaxToken[] { oldToken }, (SyntaxToken o, SyntaxToken r) => newToken, null, null));
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x0004BEA4 File Offset: 0x0004A0A4
		public static TRoot ReplaceTrivia<[Nullable(0)] TRoot>(this TRoot root, IEnumerable<SyntaxTrivia> trivia, Func<SyntaxTrivia, SyntaxTrivia, SyntaxTrivia> computeReplacementTrivia) where TRoot : SyntaxNode
		{
			return (TRoot)((object)root.ReplaceCore<SyntaxNode>(null, null, null, null, trivia, computeReplacementTrivia));
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x0004BEBC File Offset: 0x0004A0BC
		public static TRoot ReplaceTrivia<[Nullable(0)] TRoot>(this TRoot root, SyntaxTrivia trivia, SyntaxTrivia newTrivia) where TRoot : SyntaxNode
		{
			return (TRoot)((object)root.ReplaceCore<SyntaxNode>(null, null, null, null, new SyntaxTrivia[] { trivia }, (SyntaxTrivia o, SyntaxTrivia r) => newTrivia));
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x0004BF04 File Offset: 0x0004A104
		[return: Nullable(2)]
		public static TRoot RemoveNode<[Nullable(0)] TRoot>(this TRoot root, SyntaxNode node, SyntaxRemoveOptions options) where TRoot : SyntaxNode
		{
			return (TRoot)((object)root.RemoveNodesCore(new SyntaxNode[] { node }, options));
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x0004BF21 File Offset: 0x0004A121
		[return: Nullable(2)]
		public static TRoot RemoveNodes<[Nullable(0)] TRoot>(this TRoot root, IEnumerable<SyntaxNode> nodes, SyntaxRemoveOptions options) where TRoot : SyntaxNode
		{
			return (TRoot)((object)root.RemoveNodesCore(nodes, options));
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x0004BF35 File Offset: 0x0004A135
		public static TNode NormalizeWhitespace<[Nullable(0)] TNode>(this TNode node, string indentation, bool elasticTrivia) where TNode : SyntaxNode
		{
			return (TNode)((object)node.NormalizeWhitespaceCore(indentation, "\r\n", elasticTrivia));
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x0004BF4E File Offset: 0x0004A14E
		public static TNode NormalizeWhitespace<[Nullable(0)] TNode>(this TNode node, string indentation = "    ", string eol = "\r\n", bool elasticTrivia = false) where TNode : SyntaxNode
		{
			return (TNode)((object)node.NormalizeWhitespaceCore(indentation, eol, elasticTrivia));
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x0004BF63 File Offset: 0x0004A163
		public static TSyntax WithTriviaFrom<[Nullable(0)] TSyntax>(this TSyntax syntax, SyntaxNode node) where TSyntax : SyntaxNode
		{
			return syntax.WithLeadingTrivia(node.GetLeadingTrivia()).WithTrailingTrivia(node.GetTrailingTrivia());
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x0004BF7C File Offset: 0x0004A17C
		public static TSyntax WithoutTrivia<[Nullable(0)] TSyntax>(this TSyntax syntax) where TSyntax : SyntaxNode
		{
			return syntax.WithoutLeadingTrivia<TSyntax>().WithoutTrailingTrivia<TSyntax>();
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x0004BF8C File Offset: 0x0004A18C
		public static SyntaxToken WithoutTrivia(this SyntaxToken token)
		{
			return token.WithTrailingTrivia(default(SyntaxTriviaList)).WithLeadingTrivia(default(SyntaxTriviaList));
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x0004BFBC File Offset: 0x0004A1BC
		public static TSyntax WithLeadingTrivia<[Nullable(0)] TSyntax>(this TSyntax node, SyntaxTriviaList trivia) where TSyntax : SyntaxNode
		{
			SyntaxToken firstToken = node.GetFirstToken(true, false, false, false);
			SyntaxToken syntaxToken = firstToken.WithLeadingTrivia(trivia);
			return node.ReplaceToken(firstToken, syntaxToken);
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x0004BFEC File Offset: 0x0004A1EC
		public static TSyntax WithLeadingTrivia<[Nullable(0)] TSyntax>(this TSyntax node, [Nullable(2)] IEnumerable<SyntaxTrivia> trivia) where TSyntax : SyntaxNode
		{
			SyntaxToken firstToken = node.GetFirstToken(true, false, false, false);
			SyntaxToken syntaxToken = firstToken.WithLeadingTrivia(trivia);
			return node.ReplaceToken(firstToken, syntaxToken);
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x0004C01A File Offset: 0x0004A21A
		public static TSyntax WithoutLeadingTrivia<[Nullable(0)] TSyntax>(this TSyntax node) where TSyntax : SyntaxNode
		{
			return node.WithLeadingTrivia(null);
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x0004C023 File Offset: 0x0004A223
		public static TSyntax WithLeadingTrivia<[Nullable(0)] TSyntax>(this TSyntax node, [Nullable(2)] params SyntaxTrivia[] trivia) where TSyntax : SyntaxNode
		{
			return node.WithLeadingTrivia(trivia);
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x0004C02C File Offset: 0x0004A22C
		public static TSyntax WithTrailingTrivia<[Nullable(0)] TSyntax>(this TSyntax node, SyntaxTriviaList trivia) where TSyntax : SyntaxNode
		{
			SyntaxToken lastToken = node.GetLastToken(true, false, false, false);
			SyntaxToken syntaxToken = lastToken.WithTrailingTrivia(trivia);
			return node.ReplaceToken(lastToken, syntaxToken);
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x0004C05C File Offset: 0x0004A25C
		public static TSyntax WithTrailingTrivia<[Nullable(0)] TSyntax>(this TSyntax node, [Nullable(2)] IEnumerable<SyntaxTrivia> trivia) where TSyntax : SyntaxNode
		{
			SyntaxToken lastToken = node.GetLastToken(true, false, false, false);
			SyntaxToken syntaxToken = lastToken.WithTrailingTrivia(trivia);
			return node.ReplaceToken(lastToken, syntaxToken);
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x0004C08A File Offset: 0x0004A28A
		public static TSyntax WithoutTrailingTrivia<[Nullable(0)] TSyntax>(this TSyntax node) where TSyntax : SyntaxNode
		{
			return node.WithTrailingTrivia(null);
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x0004C093 File Offset: 0x0004A293
		public static TSyntax WithTrailingTrivia<[Nullable(0)] TSyntax>(this TSyntax node, [Nullable(2)] params SyntaxTrivia[] trivia) where TSyntax : SyntaxNode
		{
			return node.WithTrailingTrivia(trivia);
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x0004C09C File Offset: 0x0004A29C
		[NullableContext(2)]
		[return: NotNullIfNotNull("node")]
		internal static SyntaxNode AsRootOfNewTreeWithOptionsFrom(this SyntaxNode node, [Nullable(1)] SyntaxTree oldTree)
		{
			if (node == null)
			{
				return null;
			}
			return oldTree.WithRootAndOptions(node, oldTree.Options).GetRoot(default(CancellationToken));
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x0004C0CC File Offset: 0x0004A2CC
		public static TRoot TrackNodes<[Nullable(0)] TRoot>(this TRoot root, IEnumerable<SyntaxNode> nodes) where TRoot : SyntaxNode
		{
			if (nodes == null)
			{
				throw new ArgumentNullException("nodes");
			}
			foreach (SyntaxNode syntaxNode in nodes)
			{
				if (!SyntaxNodeExtensions.IsDescendant(root, syntaxNode))
				{
					throw new ArgumentException(CodeAnalysisResources.InvalidNodeToTrack);
				}
				SyntaxNodeExtensions.s_nodeToIdMap.GetValue(syntaxNode, (SyntaxNode n) => new SyntaxAnnotation("Id"));
			}
			return root.ReplaceNodes(nodes, delegate(SyntaxNode n, SyntaxNode r)
			{
				if (!n.HasAnnotation(SyntaxNodeExtensions.GetId(n)))
				{
					return r.WithAdditionalAnnotations(new SyntaxAnnotation[] { SyntaxNodeExtensions.GetId(n) });
				}
				return r;
			});
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x0004C188 File Offset: 0x0004A388
		public static TRoot TrackNodes<[Nullable(0)] TRoot>(this TRoot root, params SyntaxNode[] nodes) where TRoot : SyntaxNode
		{
			return root.TrackNodes(nodes);
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x0004C191 File Offset: 0x0004A391
		public static IEnumerable<TNode> GetCurrentNodes<[Nullable(0)] TNode>(this SyntaxNode root, TNode node) where TNode : SyntaxNode
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			return Enumerable.OfType<TNode>(SyntaxNodeExtensions.GetCurrentNodeFromTrueRoots(SyntaxNodeExtensions.GetRoot(root), node));
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x0004C1BC File Offset: 0x0004A3BC
		[return: Nullable(2)]
		public static TNode GetCurrentNode<[Nullable(0)] TNode>(this SyntaxNode root, TNode node) where TNode : SyntaxNode
		{
			return Enumerable.SingleOrDefault<TNode>(root.GetCurrentNodes(node));
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x0004C1CA File Offset: 0x0004A3CA
		public static IEnumerable<TNode> GetCurrentNodes<[Nullable(0)] TNode>(this SyntaxNode root, IEnumerable<TNode> nodes) where TNode : SyntaxNode
		{
			if (nodes == null)
			{
				throw new ArgumentNullException("nodes");
			}
			SyntaxNode trueRoot = SyntaxNodeExtensions.GetRoot(root);
			foreach (TNode tnode in nodes)
			{
				foreach (TNode tnode2 in Enumerable.OfType<TNode>(SyntaxNodeExtensions.GetCurrentNodeFromTrueRoots(trueRoot, tnode)))
				{
					yield return tnode2;
				}
				IEnumerator<TNode> enumerator2 = null;
			}
			IEnumerator<TNode> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x0004C1E4 File Offset: 0x0004A3E4
		private static IReadOnlyList<SyntaxNode> GetCurrentNodeFromTrueRoots(SyntaxNode trueRoot, SyntaxNode node)
		{
			SyntaxAnnotation id = SyntaxNodeExtensions.GetId(node);
			if (id != null)
			{
				return SyntaxNodeExtensions.s_rootToCurrentNodesMap.GetValue(trueRoot, (SyntaxNode r) => new SyntaxNodeExtensions.CurrentNodes(r)).GetNodes(id);
			}
			return SpecializedCollections.EmptyReadOnlyList<SyntaxNode>();
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x0004C234 File Offset: 0x0004A434
		[return: Nullable(2)]
		private static SyntaxAnnotation GetId(SyntaxNode original)
		{
			SyntaxAnnotation syntaxAnnotation;
			SyntaxNodeExtensions.s_nodeToIdMap.TryGetValue(original, ref syntaxAnnotation);
			return syntaxAnnotation;
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x0004C250 File Offset: 0x0004A450
		private static SyntaxNode GetRoot(SyntaxNode node)
		{
			for (;;)
			{
				if (node.Parent == null)
				{
					if (!node.IsStructuredTrivia)
					{
						break;
					}
					node = ((IStructuredTriviaSyntax)node).ParentTrivia.Token.Parent;
				}
				else
				{
					node = node.Parent;
				}
			}
			return node;
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x0004C298 File Offset: 0x0004A498
		private static bool IsDescendant(SyntaxNode root, SyntaxNode node)
		{
			while (node != null)
			{
				if (node == root)
				{
					return true;
				}
				if (node.Parent != null)
				{
					node = node.Parent;
				}
				else
				{
					if (!node.IsStructuredTrivia)
					{
						break;
					}
					node = ((IStructuredTriviaSyntax)node).ParentTrivia.Token.Parent;
				}
			}
			return false;
		}

		// Token: 0x04000A16 RID: 2582
		internal const string DefaultIndentation = "    ";

		// Token: 0x04000A17 RID: 2583
		internal const string DefaultEOL = "\r\n";

		// Token: 0x04000A18 RID: 2584
		private static readonly ConditionalWeakTable<SyntaxNode, SyntaxAnnotation> s_nodeToIdMap = new ConditionalWeakTable<SyntaxNode, SyntaxAnnotation>();

		// Token: 0x04000A19 RID: 2585
		private static readonly ConditionalWeakTable<SyntaxNode, SyntaxNodeExtensions.CurrentNodes> s_rootToCurrentNodesMap = new ConditionalWeakTable<SyntaxNode, SyntaxNodeExtensions.CurrentNodes>();

		// Token: 0x04000A1A RID: 2586
		internal const string IdAnnotationKind = "Id";

		// Token: 0x0200061D RID: 1565
		[Nullable(0)]
		private class CurrentNodes
		{
			// Token: 0x060037EB RID: 14315 RVA: 0x0009C41C File Offset: 0x0009A61C
			public CurrentNodes(SyntaxNode root)
			{
				SegmentedDictionary<SyntaxAnnotation, List<SyntaxNode>> segmentedDictionary = new SegmentedDictionary<SyntaxAnnotation, List<SyntaxNode>>();
				foreach (SyntaxNode syntaxNode in Enumerable.Select<SyntaxNodeOrToken, SyntaxNode>(root.GetAnnotatedNodesAndTokens("Id"), (SyntaxNodeOrToken n) => n.AsNode()))
				{
					foreach (SyntaxAnnotation syntaxAnnotation in syntaxNode.GetAnnotations("Id"))
					{
						List<SyntaxNode> list;
						if (!segmentedDictionary.TryGetValue(syntaxAnnotation, out list))
						{
							list = new List<SyntaxNode>();
							segmentedDictionary.Add(syntaxAnnotation, list);
						}
						list.Add(syntaxNode);
					}
				}
				this._idToNodeMap = segmentedDictionary.ToImmutableSegmentedDictionary<KeyValuePair<SyntaxAnnotation, List<SyntaxNode>>, SyntaxAnnotation, IReadOnlyList<SyntaxNode>>((KeyValuePair<SyntaxAnnotation, List<SyntaxNode>> kv) => kv.Key, (KeyValuePair<SyntaxAnnotation, List<SyntaxNode>> kv) => ImmutableArray.CreateRange<SyntaxNode>(kv.Value));
			}

			// Token: 0x060037EC RID: 14316 RVA: 0x0009C544 File Offset: 0x0009A744
			public IReadOnlyList<SyntaxNode> GetNodes(SyntaxAnnotation id)
			{
				IReadOnlyList<SyntaxNode> readOnlyList;
				if (this._idToNodeMap.TryGetValue(id, out readOnlyList))
				{
					return readOnlyList;
				}
				return SpecializedCollections.EmptyReadOnlyList<SyntaxNode>();
			}

			// Token: 0x040017FF RID: 6143
			[Nullable(new byte[] { 0, 1, 1, 1 })]
			private readonly ImmutableSegmentedDictionary<SyntaxAnnotation, IReadOnlyList<SyntaxNode>> _idToNodeMap;
		}
	}
}
