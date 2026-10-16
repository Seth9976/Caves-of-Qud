using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000282 RID: 642
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal static class SyntaxNodeRemover
	{
		// Token: 0x06003CDD RID: 15581 RVA: 0x001066A8 File Offset: 0x001048A8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal static TRoot RemoveNodes<[global::System.Runtime.CompilerServices.Nullable(0)] TRoot>(TRoot root, IEnumerable<SyntaxNode> nodes, SyntaxRemoveOptions options) where TRoot : SyntaxNode
		{
			if (nodes == null)
			{
				return root;
			}
			if (Enumerable.ToArray<SyntaxNode>(nodes).Length == 0)
			{
				return root;
			}
			SyntaxNodeRemover.SyntaxRemover syntaxRemover = new SyntaxNodeRemover.SyntaxRemover(Enumerable.ToArray<SyntaxNode>(nodes), options);
			SyntaxNode syntaxNode = syntaxRemover.Visit(root);
			SyntaxTriviaList residualTrivia = syntaxRemover.ResidualTrivia;
			if (syntaxNode != null && residualTrivia.Count > 0)
			{
				syntaxNode = syntaxNode.WithTrailingTrivia(Enumerable.Concat<SyntaxTrivia>(syntaxNode.GetTrailingTrivia(), residualTrivia));
			}
			return (TRoot)((object)syntaxNode);
		}

		// Token: 0x02000788 RID: 1928
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private class SyntaxRemover : CSharpSyntaxRewriter
		{
			// Token: 0x0600887A RID: 34938 RVA: 0x001ED020 File Offset: 0x001EB220
			public SyntaxRemover(SyntaxNode[] nodesToRemove, SyntaxRemoveOptions options)
				: base(Enumerable.Any<SyntaxNode>(nodesToRemove, (SyntaxNode n) => n.IsPartOfStructuredTrivia()))
			{
				this._nodesToRemove = new HashSet<SyntaxNode>(nodesToRemove);
				this._options = options;
				this._searchSpan = SyntaxNodeRemover.SyntaxRemover.ComputeTotalSpan(nodesToRemove);
				this._residualTrivia = SyntaxTriviaListBuilder.Create();
			}

			// Token: 0x0600887B RID: 34939 RVA: 0x001ED084 File Offset: 0x001EB284
			private static TextSpan ComputeTotalSpan(SyntaxNode[] nodes)
			{
				TextSpan fullSpan = nodes[0].FullSpan;
				int num = fullSpan.Start;
				int num2 = fullSpan.End;
				for (int i = 1; i < nodes.Length; i++)
				{
					TextSpan fullSpan2 = nodes[i].FullSpan;
					num = Math.Min(num, fullSpan2.Start);
					num2 = Math.Max(num2, fullSpan2.End);
				}
				return new TextSpan(num, num2 - num);
			}

			// Token: 0x17002734 RID: 10036
			// (get) Token: 0x0600887C RID: 34940 RVA: 0x001ED0E8 File Offset: 0x001EB2E8
			internal SyntaxTriviaList ResidualTrivia
			{
				get
				{
					if (this._residualTrivia != null)
					{
						return this._residualTrivia.ToList();
					}
					return default(SyntaxTriviaList);
				}
			}

			// Token: 0x0600887D RID: 34941 RVA: 0x001ED114 File Offset: 0x001EB314
			private void AddResidualTrivia(SyntaxTriviaList trivia, bool requiresNewLine = false)
			{
				if (requiresNewLine)
				{
					this.AddEndOfLine(new SyntaxTrivia?(SyntaxNodeRemover.SyntaxRemover.GetEndOfLine(trivia) ?? SyntaxFactory.CarriageReturnLineFeed));
				}
				this._residualTrivia.Add(in trivia);
			}

			// Token: 0x0600887E RID: 34942 RVA: 0x001ED15C File Offset: 0x001EB35C
			private void AddEndOfLine(SyntaxTrivia? eolTrivia)
			{
				if (eolTrivia == null)
				{
					return;
				}
				if (this._residualTrivia.Count == 0 || !SyntaxNodeRemover.SyntaxRemover.IsEndOfLine(this._residualTrivia[this._residualTrivia.Count - 1]))
				{
					this._residualTrivia.Add(eolTrivia.Value);
				}
			}

			// Token: 0x0600887F RID: 34943 RVA: 0x001ED1B2 File Offset: 0x001EB3B2
			private static bool IsEndOfLine(SyntaxTrivia trivia)
			{
				return trivia.Kind() == SyntaxKind.EndOfLineTrivia || trivia.Kind() == SyntaxKind.SingleLineCommentTrivia || trivia.IsDirective;
			}

			// Token: 0x06008880 RID: 34944 RVA: 0x001ED1D8 File Offset: 0x001EB3D8
			private static SyntaxTrivia? GetEndOfLine(SyntaxTriviaList list)
			{
				foreach (SyntaxTrivia syntaxTrivia in list)
				{
					if (syntaxTrivia.Kind() == SyntaxKind.EndOfLineTrivia)
					{
						return new SyntaxTrivia?(syntaxTrivia);
					}
					if (syntaxTrivia.IsDirective)
					{
						DirectiveTriviaSyntax directiveTriviaSyntax = syntaxTrivia.GetStructure() as DirectiveTriviaSyntax;
						if (directiveTriviaSyntax != null)
						{
							return SyntaxNodeRemover.SyntaxRemover.GetEndOfLine(directiveTriviaSyntax.EndOfDirectiveToken.TrailingTrivia);
						}
					}
				}
				return default(SyntaxTrivia?);
			}

			// Token: 0x06008881 RID: 34945 RVA: 0x001ED24B File Offset: 0x001EB44B
			private bool IsForRemoval(SyntaxNode node)
			{
				return this._nodesToRemove.Contains(node);
			}

			// Token: 0x06008882 RID: 34946 RVA: 0x001ED25C File Offset: 0x001EB45C
			private bool ShouldVisit(SyntaxNode node)
			{
				return node.FullSpan.IntersectsWith(this._searchSpan) || (this._residualTrivia != null && this._residualTrivia.Count > 0);
			}

			// Token: 0x06008883 RID: 34947 RVA: 0x001ED29C File Offset: 0x001EB49C
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			[return: NotNullIfNotNull("node")]
			public override SyntaxNode Visit(SyntaxNode node)
			{
				SyntaxNode syntaxNode = node;
				if (node != null)
				{
					if (this.IsForRemoval(node))
					{
						this.AddTrivia(node);
						syntaxNode = null;
					}
					else if (this.ShouldVisit(node))
					{
						syntaxNode = base.Visit(node);
					}
				}
				return syntaxNode;
			}

			// Token: 0x06008884 RID: 34948 RVA: 0x001ED2D4 File Offset: 0x001EB4D4
			public override SyntaxToken VisitToken(SyntaxToken token)
			{
				SyntaxToken syntaxToken = token;
				if (this.VisitIntoStructuredTrivia)
				{
					syntaxToken = base.VisitToken(token);
				}
				if (syntaxToken.Kind() != SyntaxKind.None && this._residualTrivia != null && this._residualTrivia.Count > 0)
				{
					SyntaxTriviaListBuilder residualTrivia = this._residualTrivia;
					SyntaxTriviaList leadingTrivia = syntaxToken.LeadingTrivia;
					residualTrivia.Add(in leadingTrivia);
					syntaxToken = syntaxToken.WithLeadingTrivia(this._residualTrivia.ToList());
					this._residualTrivia.Clear();
				}
				return syntaxToken;
			}

			// Token: 0x06008885 RID: 34949 RVA: 0x001ED348 File Offset: 0x001EB548
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			public override SeparatedSyntaxList<TNode> VisitList<TNode>([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<TNode> list)
			{
				SyntaxNodeOrTokenList withSeparators = list.GetWithSeparators();
				bool flag = false;
				SyntaxNodeOrTokenListBuilder syntaxNodeOrTokenListBuilder = null;
				int i = 0;
				int count = withSeparators.Count;
				while (i < count)
				{
					SyntaxNodeOrToken syntaxNodeOrToken = withSeparators[i];
					SyntaxNodeOrToken syntaxNodeOrToken2;
					if (syntaxNodeOrToken.IsToken)
					{
						if (flag)
						{
							flag = false;
							syntaxNodeOrToken2 = default(SyntaxNodeOrToken);
						}
						else
						{
							syntaxNodeOrToken2 = this.VisitListSeparator(syntaxNodeOrToken.AsToken());
						}
					}
					else
					{
						TNode tnode = (TNode)((object)syntaxNodeOrToken.AsNode());
						if (this.IsForRemoval(tnode))
						{
							if (syntaxNodeOrTokenListBuilder == null)
							{
								syntaxNodeOrTokenListBuilder = new SyntaxNodeOrTokenListBuilder(count);
								syntaxNodeOrTokenListBuilder.Add(withSeparators, 0, i);
							}
							bool flag2;
							bool flag3;
							CommonSyntaxNodeRemover.GetSeparatorInfo(withSeparators, i, 8539, out flag2, out flag3);
							if (!flag3 && syntaxNodeOrTokenListBuilder.Count > 0 && syntaxNodeOrTokenListBuilder[syntaxNodeOrTokenListBuilder.Count - 1].IsToken)
							{
								SyntaxToken syntaxToken = syntaxNodeOrTokenListBuilder[syntaxNodeOrTokenListBuilder.Count - 1].AsToken();
								this.AddTrivia(syntaxToken, tnode);
								syntaxNodeOrTokenListBuilder.RemoveLast();
							}
							else if (flag2)
							{
								SyntaxToken syntaxToken2 = withSeparators[i + 1].AsToken();
								this.AddTrivia(tnode, syntaxToken2);
								flag = true;
							}
							else
							{
								this.AddTrivia(tnode);
							}
							syntaxNodeOrToken2 = default(SyntaxNodeOrToken);
						}
						else
						{
							syntaxNodeOrToken2 = this.VisitListElement<TNode>(tnode);
						}
					}
					if (syntaxNodeOrToken != syntaxNodeOrToken2 && syntaxNodeOrTokenListBuilder == null)
					{
						syntaxNodeOrTokenListBuilder = new SyntaxNodeOrTokenListBuilder(count);
						syntaxNodeOrTokenListBuilder.Add(withSeparators, 0, i);
					}
					if (syntaxNodeOrTokenListBuilder != null && syntaxNodeOrToken2.Kind() != SyntaxKind.None)
					{
						syntaxNodeOrTokenListBuilder.Add(in syntaxNodeOrToken2);
					}
					i++;
				}
				if (syntaxNodeOrTokenListBuilder != null)
				{
					return syntaxNodeOrTokenListBuilder.ToList().AsSeparatedList<TNode>();
				}
				return list;
			}

			// Token: 0x06008886 RID: 34950 RVA: 0x001ED4F0 File Offset: 0x001EB6F0
			private void AddTrivia(SyntaxNode node)
			{
				if ((this._options & SyntaxRemoveOptions.KeepLeadingTrivia) != SyntaxRemoveOptions.KeepNoTrivia)
				{
					this.AddResidualTrivia(node.GetLeadingTrivia(), false);
				}
				else if ((this._options & SyntaxRemoveOptions.KeepEndOfLine) != SyntaxRemoveOptions.KeepNoTrivia)
				{
					this.AddEndOfLine(SyntaxNodeRemover.SyntaxRemover.GetEndOfLine(node.GetLeadingTrivia()));
				}
				if ((this._options & (SyntaxRemoveOptions.KeepUnbalancedDirectives | SyntaxRemoveOptions.KeepDirectives)) != SyntaxRemoveOptions.KeepNoTrivia)
				{
					this.AddDirectives(node, this.GetRemovedSpan(node.Span, node.FullSpan));
				}
				if ((this._options & SyntaxRemoveOptions.KeepTrailingTrivia) != SyntaxRemoveOptions.KeepNoTrivia)
				{
					this.AddResidualTrivia(node.GetTrailingTrivia(), false);
				}
				else if ((this._options & SyntaxRemoveOptions.KeepEndOfLine) != SyntaxRemoveOptions.KeepNoTrivia)
				{
					this.AddEndOfLine(SyntaxNodeRemover.SyntaxRemover.GetEndOfLine(node.GetTrailingTrivia()));
				}
				if ((this._options & SyntaxRemoveOptions.AddElasticMarker) != SyntaxRemoveOptions.KeepNoTrivia)
				{
					this.AddResidualTrivia(SyntaxFactory.TriviaList(SyntaxFactory.ElasticMarker), false);
				}
			}

			// Token: 0x06008887 RID: 34951 RVA: 0x001ED5A8 File Offset: 0x001EB7A8
			private void AddTrivia(SyntaxToken token, SyntaxNode node)
			{
				if ((this._options & SyntaxRemoveOptions.KeepLeadingTrivia) != SyntaxRemoveOptions.KeepNoTrivia)
				{
					this.AddResidualTrivia(token.LeadingTrivia, false);
					this.AddResidualTrivia(token.TrailingTrivia, false);
					this.AddResidualTrivia(node.GetLeadingTrivia(), false);
				}
				else if ((this._options & SyntaxRemoveOptions.KeepEndOfLine) != SyntaxRemoveOptions.KeepNoTrivia)
				{
					SyntaxTrivia? endOfLine = SyntaxNodeRemover.SyntaxRemover.GetEndOfLine(token.LeadingTrivia);
					SyntaxTrivia? syntaxTrivia = ((endOfLine != null) ? endOfLine : SyntaxNodeRemover.SyntaxRemover.GetEndOfLine(token.TrailingTrivia));
					this.AddEndOfLine(syntaxTrivia);
				}
				if ((this._options & (SyntaxRemoveOptions.KeepUnbalancedDirectives | SyntaxRemoveOptions.KeepDirectives)) != SyntaxRemoveOptions.KeepNoTrivia)
				{
					TextSpan textSpan = TextSpan.FromBounds(token.Span.Start, node.Span.End);
					TextSpan textSpan2 = TextSpan.FromBounds(token.FullSpan.Start, node.FullSpan.End);
					this.AddDirectives(node.Parent, this.GetRemovedSpan(textSpan, textSpan2));
				}
				if ((this._options & SyntaxRemoveOptions.KeepTrailingTrivia) != SyntaxRemoveOptions.KeepNoTrivia)
				{
					this.AddResidualTrivia(node.GetTrailingTrivia(), false);
				}
				else if ((this._options & SyntaxRemoveOptions.KeepEndOfLine) != SyntaxRemoveOptions.KeepNoTrivia)
				{
					this.AddEndOfLine(SyntaxNodeRemover.SyntaxRemover.GetEndOfLine(node.GetTrailingTrivia()));
				}
				if ((this._options & SyntaxRemoveOptions.AddElasticMarker) != SyntaxRemoveOptions.KeepNoTrivia)
				{
					this.AddResidualTrivia(SyntaxFactory.TriviaList(SyntaxFactory.ElasticMarker), false);
				}
			}

			// Token: 0x06008888 RID: 34952 RVA: 0x001ED6DC File Offset: 0x001EB8DC
			private void AddTrivia(SyntaxNode node, SyntaxToken token)
			{
				if ((this._options & SyntaxRemoveOptions.KeepLeadingTrivia) != SyntaxRemoveOptions.KeepNoTrivia)
				{
					this.AddResidualTrivia(node.GetLeadingTrivia(), false);
				}
				else if ((this._options & SyntaxRemoveOptions.KeepEndOfLine) != SyntaxRemoveOptions.KeepNoTrivia)
				{
					this.AddEndOfLine(SyntaxNodeRemover.SyntaxRemover.GetEndOfLine(node.GetLeadingTrivia()));
				}
				if ((this._options & (SyntaxRemoveOptions.KeepUnbalancedDirectives | SyntaxRemoveOptions.KeepDirectives)) != SyntaxRemoveOptions.KeepNoTrivia)
				{
					TextSpan textSpan = TextSpan.FromBounds(node.Span.Start, token.Span.End);
					TextSpan textSpan2 = TextSpan.FromBounds(node.FullSpan.Start, token.FullSpan.End);
					this.AddDirectives(node.Parent, this.GetRemovedSpan(textSpan, textSpan2));
				}
				if ((this._options & SyntaxRemoveOptions.KeepTrailingTrivia) != SyntaxRemoveOptions.KeepNoTrivia)
				{
					this.AddResidualTrivia(node.GetTrailingTrivia(), false);
					this.AddResidualTrivia(token.LeadingTrivia, false);
					this.AddResidualTrivia(token.TrailingTrivia, false);
				}
				else if ((this._options & SyntaxRemoveOptions.KeepEndOfLine) != SyntaxRemoveOptions.KeepNoTrivia)
				{
					SyntaxTrivia? endOfLine = SyntaxNodeRemover.SyntaxRemover.GetEndOfLine(node.GetTrailingTrivia());
					SyntaxTrivia? syntaxTrivia = ((endOfLine != null) ? endOfLine : SyntaxNodeRemover.SyntaxRemover.GetEndOfLine(token.TrailingTrivia));
					this.AddEndOfLine(syntaxTrivia);
				}
				if ((this._options & SyntaxRemoveOptions.AddElasticMarker) != SyntaxRemoveOptions.KeepNoTrivia)
				{
					this.AddResidualTrivia(SyntaxFactory.TriviaList(SyntaxFactory.ElasticMarker), false);
				}
			}

			// Token: 0x06008889 RID: 34953 RVA: 0x001ED810 File Offset: 0x001EBA10
			private TextSpan GetRemovedSpan(TextSpan span, TextSpan fullSpan)
			{
				TextSpan textSpan = fullSpan;
				if ((this._options & SyntaxRemoveOptions.KeepLeadingTrivia) != SyntaxRemoveOptions.KeepNoTrivia)
				{
					textSpan = TextSpan.FromBounds(span.Start, textSpan.End);
				}
				if ((this._options & SyntaxRemoveOptions.KeepTrailingTrivia) != SyntaxRemoveOptions.KeepNoTrivia)
				{
					textSpan = TextSpan.FromBounds(textSpan.Start, span.End);
				}
				return textSpan;
			}

			// Token: 0x0600888A RID: 34954 RVA: 0x001ED85C File Offset: 0x001EBA5C
			private void AddDirectives(SyntaxNode node, TextSpan span)
			{
				if (node.ContainsDirectives)
				{
					if (this._directivesToKeep == null)
					{
						this._directivesToKeep = new HashSet<SyntaxNode>();
					}
					else
					{
						this._directivesToKeep.Clear();
					}
					Func<DirectiveTriviaSyntax, bool> <>9__3;
					Func<DirectiveTriviaSyntax, bool> <>9__4;
					foreach (DirectiveTriviaSyntax directiveTriviaSyntax in Enumerable.Select<SyntaxTrivia, DirectiveTriviaSyntax>(Enumerable.Where<SyntaxTrivia>(node.DescendantTrivia(span, (SyntaxNode n) => n.ContainsDirectives, true), (SyntaxTrivia tr) => tr.IsDirective), (SyntaxTrivia tr) => (DirectiveTriviaSyntax)tr.GetStructure()))
					{
						if ((this._options & SyntaxRemoveOptions.KeepDirectives) != SyntaxRemoveOptions.KeepNoTrivia)
						{
							this._directivesToKeep.Add(directiveTriviaSyntax);
						}
						else if (directiveTriviaSyntax.Kind() == SyntaxKind.DefineDirectiveTrivia || directiveTriviaSyntax.Kind() == SyntaxKind.UndefDirectiveTrivia)
						{
							this._directivesToKeep.Add(directiveTriviaSyntax);
						}
						else if (SyntaxNodeRemover.SyntaxRemover.HasRelatedDirectives(directiveTriviaSyntax))
						{
							List<DirectiveTriviaSyntax> relatedDirectives = directiveTriviaSyntax.GetRelatedDirectives();
							IEnumerable<DirectiveTriviaSyntax> enumerable = relatedDirectives;
							Func<DirectiveTriviaSyntax, bool> func;
							if ((func = <>9__3) == null)
							{
								func = (<>9__3 = (DirectiveTriviaSyntax rd) => rd.FullSpan.OverlapsWith(span));
							}
							if (!Enumerable.All<DirectiveTriviaSyntax>(enumerable, func))
							{
								IEnumerable<DirectiveTriviaSyntax> enumerable2 = relatedDirectives;
								Func<DirectiveTriviaSyntax, bool> func2;
								if ((func2 = <>9__4) == null)
								{
									func2 = (<>9__4 = (DirectiveTriviaSyntax rd) => rd.FullSpan.OverlapsWith(span));
								}
								foreach (DirectiveTriviaSyntax directiveTriviaSyntax2 in Enumerable.Where<DirectiveTriviaSyntax>(enumerable2, func2))
								{
									this._directivesToKeep.Add(directiveTriviaSyntax2);
								}
							}
						}
						if (this._directivesToKeep.Contains(directiveTriviaSyntax))
						{
							this.AddResidualTrivia(SyntaxFactory.TriviaList(directiveTriviaSyntax.ParentTrivia), true);
						}
					}
				}
			}

			// Token: 0x0600888B RID: 34955 RVA: 0x001EDA74 File Offset: 0x001EBC74
			private static bool HasRelatedDirectives(DirectiveTriviaSyntax directive)
			{
				SyntaxKind syntaxKind = directive.Kind();
				return syntaxKind - SyntaxKind.IfDirectiveTrivia <= 5;
			}

			// Token: 0x040023C6 RID: 9158
			private readonly HashSet<SyntaxNode> _nodesToRemove;

			// Token: 0x040023C7 RID: 9159
			private readonly SyntaxRemoveOptions _options;

			// Token: 0x040023C8 RID: 9160
			private readonly TextSpan _searchSpan;

			// Token: 0x040023C9 RID: 9161
			private readonly SyntaxTriviaListBuilder _residualTrivia;

			// Token: 0x040023CA RID: 9162
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
			private HashSet<SyntaxNode> _directivesToKeep;
		}
	}
}
