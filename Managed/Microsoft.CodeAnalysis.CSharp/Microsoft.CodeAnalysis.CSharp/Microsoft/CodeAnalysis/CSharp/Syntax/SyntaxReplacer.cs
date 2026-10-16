using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000284 RID: 644
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal static class SyntaxReplacer
	{
		// Token: 0x06003D07 RID: 15623 RVA: 0x00107D14 File Offset: 0x00105F14
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		internal static SyntaxNode Replace<[global::System.Runtime.CompilerServices.Nullable(0)] TNode>([global::System.Runtime.CompilerServices.Nullable(1)] SyntaxNode root, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] IEnumerable<TNode> nodes = null, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1, 1 })] Func<TNode, TNode, SyntaxNode> computeReplacementNode = null, IEnumerable<SyntaxToken> tokens = null, Func<SyntaxToken, SyntaxToken, SyntaxToken> computeReplacementToken = null, IEnumerable<SyntaxTrivia> trivia = null, Func<SyntaxTrivia, SyntaxTrivia, SyntaxTrivia> computeReplacementTrivia = null) where TNode : SyntaxNode
		{
			SyntaxReplacer.Replacer<TNode> replacer = new SyntaxReplacer.Replacer<TNode>(nodes, computeReplacementNode, tokens, computeReplacementToken, trivia, computeReplacementTrivia);
			if (replacer.HasWork)
			{
				return replacer.Visit(root);
			}
			return root;
		}

		// Token: 0x06003D08 RID: 15624 RVA: 0x00107D44 File Offset: 0x00105F44
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal static SyntaxToken Replace(SyntaxToken root, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] IEnumerable<SyntaxNode> nodes = null, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1, 1 })] Func<SyntaxNode, SyntaxNode, SyntaxNode> computeReplacementNode = null, IEnumerable<SyntaxToken> tokens = null, Func<SyntaxToken, SyntaxToken, SyntaxToken> computeReplacementToken = null, IEnumerable<SyntaxTrivia> trivia = null, Func<SyntaxTrivia, SyntaxTrivia, SyntaxTrivia> computeReplacementTrivia = null)
		{
			SyntaxReplacer.Replacer<SyntaxNode> replacer = new SyntaxReplacer.Replacer<SyntaxNode>(nodes, computeReplacementNode, tokens, computeReplacementToken, trivia, computeReplacementTrivia);
			if (replacer.HasWork)
			{
				return replacer.VisitToken(root);
			}
			return root;
		}

		// Token: 0x06003D09 RID: 15625 RVA: 0x00107D71 File Offset: 0x00105F71
		internal static SyntaxNode ReplaceNodeInList(SyntaxNode root, SyntaxNode originalNode, IEnumerable<SyntaxNode> newNodes)
		{
			return new SyntaxReplacer.NodeListEditor(originalNode, newNodes, SyntaxReplacer.ListEditKind.Replace).Visit(root);
		}

		// Token: 0x06003D0A RID: 15626 RVA: 0x00107D81 File Offset: 0x00105F81
		internal static SyntaxNode InsertNodeInList(SyntaxNode root, SyntaxNode nodeInList, IEnumerable<SyntaxNode> nodesToInsert, bool insertBefore)
		{
			return new SyntaxReplacer.NodeListEditor(nodeInList, nodesToInsert, insertBefore ? SyntaxReplacer.ListEditKind.InsertBefore : SyntaxReplacer.ListEditKind.InsertAfter).Visit(root);
		}

		// Token: 0x06003D0B RID: 15627 RVA: 0x00107D97 File Offset: 0x00105F97
		public static SyntaxNode ReplaceTokenInList(SyntaxNode root, SyntaxToken tokenInList, IEnumerable<SyntaxToken> newTokens)
		{
			return new SyntaxReplacer.TokenListEditor(tokenInList, newTokens, SyntaxReplacer.ListEditKind.Replace).Visit(root);
		}

		// Token: 0x06003D0C RID: 15628 RVA: 0x00107DA7 File Offset: 0x00105FA7
		public static SyntaxNode InsertTokenInList(SyntaxNode root, SyntaxToken tokenInList, IEnumerable<SyntaxToken> newTokens, bool insertBefore)
		{
			return new SyntaxReplacer.TokenListEditor(tokenInList, newTokens, insertBefore ? SyntaxReplacer.ListEditKind.InsertBefore : SyntaxReplacer.ListEditKind.InsertAfter).Visit(root);
		}

		// Token: 0x06003D0D RID: 15629 RVA: 0x00107DBD File Offset: 0x00105FBD
		public static SyntaxNode ReplaceTriviaInList(SyntaxNode root, SyntaxTrivia triviaInList, IEnumerable<SyntaxTrivia> newTrivia)
		{
			return new SyntaxReplacer.TriviaListEditor(triviaInList, newTrivia, SyntaxReplacer.ListEditKind.Replace).Visit(root);
		}

		// Token: 0x06003D0E RID: 15630 RVA: 0x00107DCD File Offset: 0x00105FCD
		public static SyntaxNode InsertTriviaInList(SyntaxNode root, SyntaxTrivia triviaInList, IEnumerable<SyntaxTrivia> newTrivia, bool insertBefore)
		{
			return new SyntaxReplacer.TriviaListEditor(triviaInList, newTrivia, insertBefore ? SyntaxReplacer.ListEditKind.InsertBefore : SyntaxReplacer.ListEditKind.InsertAfter).Visit(root);
		}

		// Token: 0x06003D0F RID: 15631 RVA: 0x00107DE3 File Offset: 0x00105FE3
		public static SyntaxToken ReplaceTriviaInList(SyntaxToken root, SyntaxTrivia triviaInList, IEnumerable<SyntaxTrivia> newTrivia)
		{
			return new SyntaxReplacer.TriviaListEditor(triviaInList, newTrivia, SyntaxReplacer.ListEditKind.Replace).VisitToken(root);
		}

		// Token: 0x06003D10 RID: 15632 RVA: 0x00107DF3 File Offset: 0x00105FF3
		public static SyntaxToken InsertTriviaInList(SyntaxToken root, SyntaxTrivia triviaInList, IEnumerable<SyntaxTrivia> newTrivia, bool insertBefore)
		{
			return new SyntaxReplacer.TriviaListEditor(triviaInList, newTrivia, insertBefore ? SyntaxReplacer.ListEditKind.InsertBefore : SyntaxReplacer.ListEditKind.InsertAfter).VisitToken(root);
		}

		// Token: 0x06003D11 RID: 15633 RVA: 0x00107E09 File Offset: 0x00106009
		private static InvalidOperationException GetItemNotListElementException()
		{
			return new InvalidOperationException(CodeAnalysisResources.MissingListItem);
		}

		// Token: 0x0200078A RID: 1930
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private class Replacer<[global::System.Runtime.CompilerServices.Nullable(0)] TNode> : CSharpSyntaxRewriter where TNode : SyntaxNode
		{
			// Token: 0x06008891 RID: 34961 RVA: 0x001EDAE4 File Offset: 0x001EBCE4
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			public Replacer([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] IEnumerable<TNode> nodes, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1, 1 })] Func<TNode, TNode, SyntaxNode> computeReplacementNode, IEnumerable<SyntaxToken> tokens, Func<SyntaxToken, SyntaxToken, SyntaxToken> computeReplacementToken, IEnumerable<SyntaxTrivia> trivia, Func<SyntaxTrivia, SyntaxTrivia, SyntaxTrivia> computeReplacementTrivia)
				: base(false)
			{
				this._computeReplacementNode = computeReplacementNode;
				this._computeReplacementToken = computeReplacementToken;
				this._computeReplacementTrivia = computeReplacementTrivia;
				this._nodeSet = ((nodes != null) ? new HashSet<SyntaxNode>(nodes) : SyntaxReplacer.Replacer<TNode>.s_noNodes);
				this._tokenSet = ((tokens != null) ? new HashSet<SyntaxToken>(tokens) : SyntaxReplacer.Replacer<TNode>.s_noTokens);
				this._triviaSet = ((trivia != null) ? new HashSet<SyntaxTrivia>(trivia) : SyntaxReplacer.Replacer<TNode>.s_noTrivia);
				this._spanSet = new HashSet<TextSpan>(Enumerable.Concat<TextSpan>(Enumerable.Select<SyntaxNode, TextSpan>(this._nodeSet, (SyntaxNode n) => n.FullSpan), Enumerable.Concat<TextSpan>(Enumerable.Select<SyntaxToken, TextSpan>(this._tokenSet, (SyntaxToken t) => t.FullSpan), Enumerable.Select<SyntaxTrivia, TextSpan>(this._triviaSet, (SyntaxTrivia t) => t.FullSpan))));
				this._totalSpan = SyntaxReplacer.Replacer<TNode>.ComputeTotalSpan(this._spanSet);
				bool flag;
				if (!Enumerable.Any<SyntaxNode>(this._nodeSet, (SyntaxNode n) => n.IsPartOfStructuredTrivia()))
				{
					if (!Enumerable.Any<SyntaxToken>(this._tokenSet, (SyntaxToken t) => t.IsPartOfStructuredTrivia()))
					{
						flag = Enumerable.Any<SyntaxTrivia>(this._triviaSet, (SyntaxTrivia t) => t.IsPartOfStructuredTrivia());
						goto IL_018C;
					}
				}
				flag = true;
				IL_018C:
				this._visitIntoStructuredTrivia = flag;
				this._shouldVisitTrivia = this._triviaSet.Count > 0 || this._visitIntoStructuredTrivia;
			}

			// Token: 0x17002735 RID: 10037
			// (get) Token: 0x06008892 RID: 34962 RVA: 0x001EDC9F File Offset: 0x001EBE9F
			public override bool VisitIntoStructuredTrivia
			{
				get
				{
					return this._visitIntoStructuredTrivia;
				}
			}

			// Token: 0x17002736 RID: 10038
			// (get) Token: 0x06008893 RID: 34963 RVA: 0x001EDCA7 File Offset: 0x001EBEA7
			public bool HasWork
			{
				get
				{
					return this._nodeSet.Count + this._tokenSet.Count + this._triviaSet.Count > 0;
				}
			}

			// Token: 0x06008894 RID: 34964 RVA: 0x001EDCD0 File Offset: 0x001EBED0
			private static TextSpan ComputeTotalSpan(IEnumerable<TextSpan> spans)
			{
				bool flag = true;
				int num = 0;
				int num2 = 0;
				foreach (TextSpan textSpan in spans)
				{
					if (flag)
					{
						num = textSpan.Start;
						num2 = textSpan.End;
						flag = false;
					}
					else
					{
						num = Math.Min(num, textSpan.Start);
						num2 = Math.Max(num2, textSpan.End);
					}
				}
				return new TextSpan(num, num2 - num);
			}

			// Token: 0x06008895 RID: 34965 RVA: 0x001EDD54 File Offset: 0x001EBF54
			private bool ShouldVisit(TextSpan span)
			{
				if (!span.IntersectsWith(this._totalSpan))
				{
					return false;
				}
				foreach (TextSpan textSpan in this._spanSet)
				{
					if (span.IntersectsWith(textSpan))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06008896 RID: 34966 RVA: 0x001EDDC4 File Offset: 0x001EBFC4
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			[return: NotNullIfNotNull("node")]
			public override SyntaxNode Visit(SyntaxNode node)
			{
				SyntaxNode syntaxNode = node;
				if (node != null)
				{
					if (this.ShouldVisit(node.FullSpan))
					{
						syntaxNode = base.Visit(node);
					}
					if (this._nodeSet.Contains(node) && this._computeReplacementNode != null)
					{
						syntaxNode = this._computeReplacementNode.Invoke((TNode)((object)node), (TNode)((object)syntaxNode));
					}
				}
				return syntaxNode;
			}

			// Token: 0x06008897 RID: 34967 RVA: 0x001EDE1C File Offset: 0x001EC01C
			public override SyntaxToken VisitToken(SyntaxToken token)
			{
				SyntaxToken syntaxToken = token;
				if (this._shouldVisitTrivia && this.ShouldVisit(token.FullSpan))
				{
					syntaxToken = base.VisitToken(token);
				}
				if (this._tokenSet.Contains(token) && this._computeReplacementToken != null)
				{
					syntaxToken = this._computeReplacementToken.Invoke(token, syntaxToken);
				}
				return syntaxToken;
			}

			// Token: 0x06008898 RID: 34968 RVA: 0x001EDE70 File Offset: 0x001EC070
			public override SyntaxTrivia VisitListElement(SyntaxTrivia trivia)
			{
				SyntaxTrivia syntaxTrivia = trivia;
				if (this.VisitIntoStructuredTrivia && trivia.HasStructure && this.ShouldVisit(trivia.FullSpan))
				{
					syntaxTrivia = this.VisitTrivia(trivia);
				}
				if (this._triviaSet.Contains(trivia) && this._computeReplacementTrivia != null)
				{
					syntaxTrivia = this._computeReplacementTrivia.Invoke(trivia, syntaxTrivia);
				}
				return syntaxTrivia;
			}

			// Token: 0x040023CF RID: 9167
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1, 1 })]
			private readonly Func<TNode, TNode, SyntaxNode> _computeReplacementNode;

			// Token: 0x040023D0 RID: 9168
			[global::System.Runtime.CompilerServices.Nullable(2)]
			private readonly Func<SyntaxToken, SyntaxToken, SyntaxToken> _computeReplacementToken;

			// Token: 0x040023D1 RID: 9169
			[global::System.Runtime.CompilerServices.Nullable(2)]
			private readonly Func<SyntaxTrivia, SyntaxTrivia, SyntaxTrivia> _computeReplacementTrivia;

			// Token: 0x040023D2 RID: 9170
			private readonly HashSet<SyntaxNode> _nodeSet;

			// Token: 0x040023D3 RID: 9171
			private readonly HashSet<SyntaxToken> _tokenSet;

			// Token: 0x040023D4 RID: 9172
			private readonly HashSet<SyntaxTrivia> _triviaSet;

			// Token: 0x040023D5 RID: 9173
			private readonly HashSet<TextSpan> _spanSet;

			// Token: 0x040023D6 RID: 9174
			private readonly TextSpan _totalSpan;

			// Token: 0x040023D7 RID: 9175
			private readonly bool _visitIntoStructuredTrivia;

			// Token: 0x040023D8 RID: 9176
			private readonly bool _shouldVisitTrivia;

			// Token: 0x040023D9 RID: 9177
			private static readonly HashSet<SyntaxNode> s_noNodes = new HashSet<SyntaxNode>();

			// Token: 0x040023DA RID: 9178
			private static readonly HashSet<SyntaxToken> s_noTokens = new HashSet<SyntaxToken>();

			// Token: 0x040023DB RID: 9179
			private static readonly HashSet<SyntaxTrivia> s_noTrivia = new HashSet<SyntaxTrivia>();
		}

		// Token: 0x0200078B RID: 1931
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private enum ListEditKind
		{
			// Token: 0x040023DD RID: 9181
			InsertBefore,
			// Token: 0x040023DE RID: 9182
			InsertAfter,
			// Token: 0x040023DF RID: 9183
			Replace
		}

		// Token: 0x0200078C RID: 1932
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private abstract class BaseListEditor : CSharpSyntaxRewriter
		{
			// Token: 0x0600889A RID: 34970 RVA: 0x001EDEEC File Offset: 0x001EC0EC
			public BaseListEditor(TextSpan elementSpan, SyntaxReplacer.ListEditKind editKind, bool visitTrivia, bool visitIntoStructuredTrivia)
				: base(false)
			{
				this._elementSpan = elementSpan;
				this.editKind = editKind;
				this._visitTrivia = visitTrivia || visitIntoStructuredTrivia;
				this._visitIntoStructuredTrivia = visitIntoStructuredTrivia;
			}

			// Token: 0x17002737 RID: 10039
			// (get) Token: 0x0600889B RID: 34971 RVA: 0x001EDF15 File Offset: 0x001EC115
			public override bool VisitIntoStructuredTrivia
			{
				get
				{
					return this._visitIntoStructuredTrivia;
				}
			}

			// Token: 0x0600889C RID: 34972 RVA: 0x001EDF1D File Offset: 0x001EC11D
			private bool ShouldVisit(TextSpan span)
			{
				return span.IntersectsWith(this._elementSpan);
			}

			// Token: 0x0600889D RID: 34973 RVA: 0x001EDF34 File Offset: 0x001EC134
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			[return: NotNullIfNotNull("node")]
			public override SyntaxNode Visit(SyntaxNode node)
			{
				SyntaxNode syntaxNode = node;
				if (node != null && this.ShouldVisit(node.FullSpan))
				{
					syntaxNode = base.Visit(node);
				}
				return syntaxNode;
			}

			// Token: 0x0600889E RID: 34974 RVA: 0x001EDF60 File Offset: 0x001EC160
			public override SyntaxToken VisitToken(SyntaxToken token)
			{
				SyntaxToken syntaxToken = token;
				if (this._visitTrivia && this.ShouldVisit(token.FullSpan))
				{
					syntaxToken = base.VisitToken(token);
				}
				return syntaxToken;
			}

			// Token: 0x0600889F RID: 34975 RVA: 0x001EDF90 File Offset: 0x001EC190
			public override SyntaxTrivia VisitListElement(SyntaxTrivia trivia)
			{
				SyntaxTrivia syntaxTrivia = trivia;
				if (this.VisitIntoStructuredTrivia && trivia.HasStructure && this.ShouldVisit(trivia.FullSpan))
				{
					syntaxTrivia = this.VisitTrivia(trivia);
				}
				return syntaxTrivia;
			}

			// Token: 0x040023E0 RID: 9184
			private readonly TextSpan _elementSpan;

			// Token: 0x040023E1 RID: 9185
			private readonly bool _visitTrivia;

			// Token: 0x040023E2 RID: 9186
			private readonly bool _visitIntoStructuredTrivia;

			// Token: 0x040023E3 RID: 9187
			protected readonly SyntaxReplacer.ListEditKind editKind;
		}

		// Token: 0x0200078D RID: 1933
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private class NodeListEditor : SyntaxReplacer.BaseListEditor
		{
			// Token: 0x060088A0 RID: 34976 RVA: 0x001EDFC8 File Offset: 0x001EC1C8
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			public NodeListEditor(SyntaxNode originalNode, IEnumerable<SyntaxNode> replacementNodes, SyntaxReplacer.ListEditKind editKind)
				: base(originalNode.Span, editKind, false, originalNode.IsPartOfStructuredTrivia())
			{
				this._originalNode = originalNode;
				this._newNodes = replacementNodes;
			}

			// Token: 0x060088A1 RID: 34977 RVA: 0x001EDFEC File Offset: 0x001EC1EC
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			[return: NotNullIfNotNull("node")]
			public override SyntaxNode Visit(SyntaxNode node)
			{
				if (node == this._originalNode)
				{
					throw SyntaxReplacer.GetItemNotListElementException();
				}
				return base.Visit(node);
			}

			// Token: 0x060088A2 RID: 34978 RVA: 0x001EE004 File Offset: 0x001EC204
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			public override SeparatedSyntaxList<TNode> VisitList<TNode>([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<TNode> list)
			{
				if (this._originalNode is TNode)
				{
					int num = list.IndexOf((TNode)((object)this._originalNode));
					if (num >= 0 && num < list.Count)
					{
						switch (this.editKind)
						{
						case SyntaxReplacer.ListEditKind.InsertBefore:
							return list.InsertRange(num, Enumerable.Cast<TNode>(this._newNodes));
						case SyntaxReplacer.ListEditKind.InsertAfter:
							return list.InsertRange(num + 1, Enumerable.Cast<TNode>(this._newNodes));
						case SyntaxReplacer.ListEditKind.Replace:
							return list.ReplaceRange((TNode)((object)this._originalNode), Enumerable.Cast<TNode>(this._newNodes));
						}
					}
				}
				return base.VisitList<TNode>(list);
			}

			// Token: 0x060088A3 RID: 34979 RVA: 0x001EE0AC File Offset: 0x001EC2AC
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			public override SyntaxList<TNode> VisitList<TNode>([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TNode> list)
			{
				if (this._originalNode is TNode)
				{
					int num = list.IndexOf((TNode)((object)this._originalNode));
					if (num >= 0 && num < list.Count)
					{
						switch (this.editKind)
						{
						case SyntaxReplacer.ListEditKind.InsertBefore:
							return list.InsertRange(num, Enumerable.Cast<TNode>(this._newNodes));
						case SyntaxReplacer.ListEditKind.InsertAfter:
							return list.InsertRange(num + 1, Enumerable.Cast<TNode>(this._newNodes));
						case SyntaxReplacer.ListEditKind.Replace:
							return list.ReplaceRange((TNode)((object)this._originalNode), Enumerable.Cast<TNode>(this._newNodes));
						}
					}
				}
				return base.VisitList<TNode>(list);
			}

			// Token: 0x040023E4 RID: 9188
			[global::System.Runtime.CompilerServices.Nullable(1)]
			private readonly SyntaxNode _originalNode;

			// Token: 0x040023E5 RID: 9189
			[global::System.Runtime.CompilerServices.Nullable(1)]
			private readonly IEnumerable<SyntaxNode> _newNodes;
		}

		// Token: 0x0200078E RID: 1934
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private class TokenListEditor : SyntaxReplacer.BaseListEditor
		{
			// Token: 0x060088A4 RID: 34980 RVA: 0x001EE154 File Offset: 0x001EC354
			public TokenListEditor(SyntaxToken originalToken, IEnumerable<SyntaxToken> newTokens, SyntaxReplacer.ListEditKind editKind)
				: base(originalToken.Span, editKind, false, originalToken.IsPartOfStructuredTrivia())
			{
				this._originalToken = originalToken;
				this._newTokens = newTokens;
			}

			// Token: 0x060088A5 RID: 34981 RVA: 0x001EE17A File Offset: 0x001EC37A
			public override SyntaxToken VisitToken(SyntaxToken token)
			{
				if (token == this._originalToken)
				{
					throw SyntaxReplacer.GetItemNotListElementException();
				}
				return base.VisitToken(token);
			}

			// Token: 0x060088A6 RID: 34982 RVA: 0x001EE198 File Offset: 0x001EC398
			public override SyntaxTokenList VisitList(SyntaxTokenList list)
			{
				int num = list.IndexOf(this._originalToken);
				if (num >= 0 && num < list.Count)
				{
					switch (this.editKind)
					{
					case SyntaxReplacer.ListEditKind.InsertBefore:
						return list.InsertRange(num, this._newTokens);
					case SyntaxReplacer.ListEditKind.InsertAfter:
						return list.InsertRange(num + 1, this._newTokens);
					case SyntaxReplacer.ListEditKind.Replace:
						return list.ReplaceRange(this._originalToken, this._newTokens);
					}
				}
				return base.VisitList(list);
			}

			// Token: 0x040023E6 RID: 9190
			private readonly SyntaxToken _originalToken;

			// Token: 0x040023E7 RID: 9191
			private readonly IEnumerable<SyntaxToken> _newTokens;
		}

		// Token: 0x0200078F RID: 1935
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private class TriviaListEditor : SyntaxReplacer.BaseListEditor
		{
			// Token: 0x060088A7 RID: 34983 RVA: 0x001EE217 File Offset: 0x001EC417
			public TriviaListEditor(SyntaxTrivia originalTrivia, IEnumerable<SyntaxTrivia> newTrivia, SyntaxReplacer.ListEditKind editKind)
				: base(originalTrivia.Span, editKind, true, originalTrivia.IsPartOfStructuredTrivia())
			{
				this._originalTrivia = originalTrivia;
				this._newTrivia = newTrivia;
			}

			// Token: 0x060088A8 RID: 34984 RVA: 0x001EE240 File Offset: 0x001EC440
			public override SyntaxTriviaList VisitList(SyntaxTriviaList list)
			{
				int num = list.IndexOf(this._originalTrivia);
				if (num >= 0 && num < list.Count)
				{
					switch (this.editKind)
					{
					case SyntaxReplacer.ListEditKind.InsertBefore:
						return list.InsertRange(num, this._newTrivia);
					case SyntaxReplacer.ListEditKind.InsertAfter:
						return list.InsertRange(num + 1, this._newTrivia);
					case SyntaxReplacer.ListEditKind.Replace:
						return list.ReplaceRange(this._originalTrivia, this._newTrivia);
					}
				}
				return base.VisitList(list);
			}

			// Token: 0x040023E8 RID: 9192
			private readonly SyntaxTrivia _originalTrivia;

			// Token: 0x040023E9 RID: 9193
			private readonly IEnumerable<SyntaxTrivia> _newTrivia;
		}
	}
}
