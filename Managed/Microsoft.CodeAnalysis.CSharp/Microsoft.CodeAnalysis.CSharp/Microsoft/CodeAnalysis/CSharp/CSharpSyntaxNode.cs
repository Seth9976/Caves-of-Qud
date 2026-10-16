using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200022A RID: 554
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public abstract class CSharpSyntaxNode : SyntaxNode, IFormattable
	{
		// Token: 0x06002B8C RID: 11148 RVA: 0x000E9806 File Offset: 0x000E7A06
		internal CSharpSyntaxNode(GreenNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x06002B8D RID: 11149 RVA: 0x000E9811 File Offset: 0x000E7A11
		internal CSharpSyntaxNode(GreenNode green, int position, SyntaxTree syntaxTree)
			: base(green, position, syntaxTree)
		{
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06002B8E RID: 11150 RVA: 0x000E981C File Offset: 0x000E7A1C
		internal new SyntaxTree SyntaxTree
		{
			get
			{
				return this._syntaxTree ?? CSharpSyntaxNode.ComputeSyntaxTree(this);
			}
		}

		// Token: 0x06002B8F RID: 11151 RVA: 0x000E9830 File Offset: 0x000E7A30
		private static SyntaxTree ComputeSyntaxTree(CSharpSyntaxNode node)
		{
			ArrayBuilder<CSharpSyntaxNode> arrayBuilder = null;
			SyntaxTree syntaxTree;
			for (;;)
			{
				syntaxTree = node._syntaxTree;
				if (syntaxTree != null)
				{
					goto IL_005E;
				}
				CSharpSyntaxNode parent = node.Parent;
				if (parent == null)
				{
					break;
				}
				syntaxTree = parent._syntaxTree;
				if (syntaxTree != null)
				{
					goto Block_3;
				}
				ArrayBuilder<CSharpSyntaxNode> arrayBuilder2;
				if ((arrayBuilder2 = arrayBuilder) == null)
				{
					arrayBuilder2 = (arrayBuilder = ArrayBuilder<CSharpSyntaxNode>.GetInstance());
				}
				arrayBuilder2.Add(node);
				node = parent;
			}
			Interlocked.CompareExchange<SyntaxTree>(ref node._syntaxTree, CSharpSyntaxTree.CreateWithoutClone(node), null);
			syntaxTree = node._syntaxTree;
			goto IL_005E;
			Block_3:
			node._syntaxTree = syntaxTree;
			IL_005E:
			if (arrayBuilder != null)
			{
				foreach (CSharpSyntaxNode csharpSyntaxNode in arrayBuilder)
				{
					if (csharpSyntaxNode._syntaxTree != null)
					{
						break;
					}
					csharpSyntaxNode._syntaxTree = syntaxTree;
				}
				arrayBuilder.Free();
			}
			return syntaxTree;
		}

		// Token: 0x06002B90 RID: 11152
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public abstract TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor);

		// Token: 0x06002B91 RID: 11153
		public abstract void Accept(CSharpSyntaxVisitor visitor);

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06002B92 RID: 11154 RVA: 0x000E98D1 File Offset: 0x000E7AD1
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal new CSharpSyntaxNode Parent
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return (CSharpSyntaxNode)base.Parent;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06002B93 RID: 11155 RVA: 0x000E98DE File Offset: 0x000E7ADE
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal new CSharpSyntaxNode ParentOrStructuredTriviaParent
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return (CSharpSyntaxNode)base.ParentOrStructuredTriviaParent;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06002B94 RID: 11156 RVA: 0x000E98EB File Offset: 0x000E7AEB
		internal CSharpSyntaxNode CsGreen
		{
			get
			{
				return (CSharpSyntaxNode)base.Green;
			}
		}

		// Token: 0x06002B95 RID: 11157 RVA: 0x000E98F8 File Offset: 0x000E7AF8
		public SyntaxKind Kind()
		{
			return (SyntaxKind)base.Green.RawKind;
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06002B96 RID: 11158 RVA: 0x000E9906 File Offset: 0x000E7B06
		public override string Language
		{
			get
			{
				return "C#";
			}
		}

		// Token: 0x06002B97 RID: 11159 RVA: 0x000E9910 File Offset: 0x000E7B10
		public new SyntaxTriviaList GetLeadingTrivia()
		{
			return this.GetFirstToken(true, false, false, false).LeadingTrivia;
		}

		// Token: 0x06002B98 RID: 11160 RVA: 0x000E9930 File Offset: 0x000E7B30
		public new SyntaxTriviaList GetTrailingTrivia()
		{
			return this.GetLastToken(true, false, false, false).TrailingTrivia;
		}

		// Token: 0x06002B99 RID: 11161 RVA: 0x000E9950 File Offset: 0x000E7B50
		public static SyntaxNode DeserializeFrom(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new InvalidOperationException(CodeAnalysisResources.TheStreamCannotBeReadFrom);
			}
			SyntaxNode syntaxNode;
			using (ObjectReader objectReader = ObjectReader.TryGetReader(stream, true, cancellationToken))
			{
				if (objectReader == null)
				{
					throw new ArgumentException(CodeAnalysisResources.Stream_contains_invalid_data, "stream");
				}
				syntaxNode = ((CSharpSyntaxNode)objectReader.ReadValue()).CreateRed();
			}
			return syntaxNode;
		}

		// Token: 0x06002B9A RID: 11162 RVA: 0x000E99C8 File Offset: 0x000E7BC8
		public new Location GetLocation()
		{
			return new SourceLocation(this);
		}

		// Token: 0x06002B9B RID: 11163 RVA: 0x000E99D0 File Offset: 0x000E7BD0
		internal new SyntaxReference GetReference()
		{
			return this.SyntaxTree.GetReference(this);
		}

		// Token: 0x06002B9C RID: 11164 RVA: 0x000E99DE File Offset: 0x000E7BDE
		public new IEnumerable<Diagnostic> GetDiagnostics()
		{
			return this.SyntaxTree.GetDiagnostics(this);
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x000E99EC File Offset: 0x000E7BEC
		internal IList<Microsoft.CodeAnalysis.CSharp.Syntax.DirectiveTriviaSyntax> GetDirectives([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] Func<Microsoft.CodeAnalysis.CSharp.Syntax.DirectiveTriviaSyntax, bool> filter = null)
		{
			return this.GetDirectives<Microsoft.CodeAnalysis.CSharp.Syntax.DirectiveTriviaSyntax>(filter);
		}

		// Token: 0x06002B9E RID: 11166 RVA: 0x000E9A08 File Offset: 0x000E7C08
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public Microsoft.CodeAnalysis.CSharp.Syntax.DirectiveTriviaSyntax GetFirstDirective([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] Func<Microsoft.CodeAnalysis.CSharp.Syntax.DirectiveTriviaSyntax, bool> predicate = null)
		{
			foreach (SyntaxNodeOrToken syntaxNodeOrToken in base.ChildNodesAndTokens())
			{
				if (syntaxNodeOrToken.ContainsDirectives)
				{
					SyntaxNode syntaxNode;
					if (syntaxNodeOrToken.AsNode(out syntaxNode))
					{
						Microsoft.CodeAnalysis.CSharp.Syntax.DirectiveTriviaSyntax firstDirective = syntaxNode.GetFirstDirective(predicate);
						if (firstDirective != null)
						{
							return firstDirective;
						}
					}
					else
					{
						foreach (SyntaxTrivia syntaxTrivia in syntaxNodeOrToken.AsToken().LeadingTrivia)
						{
							if (syntaxTrivia.IsDirective)
							{
								Microsoft.CodeAnalysis.CSharp.Syntax.DirectiveTriviaSyntax directiveTriviaSyntax = (Microsoft.CodeAnalysis.CSharp.Syntax.DirectiveTriviaSyntax)syntaxTrivia.GetStructure();
								if (predicate == null || predicate.Invoke(directiveTriviaSyntax))
								{
									return directiveTriviaSyntax;
								}
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x000E9AB8 File Offset: 0x000E7CB8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public Microsoft.CodeAnalysis.CSharp.Syntax.DirectiveTriviaSyntax GetLastDirective([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] Func<Microsoft.CodeAnalysis.CSharp.Syntax.DirectiveTriviaSyntax, bool> predicate = null)
		{
			foreach (SyntaxNodeOrToken syntaxNodeOrToken in base.ChildNodesAndTokens().Reverse())
			{
				if (syntaxNodeOrToken.ContainsDirectives)
				{
					SyntaxNode syntaxNode;
					if (syntaxNodeOrToken.AsNode(out syntaxNode))
					{
						Microsoft.CodeAnalysis.CSharp.Syntax.DirectiveTriviaSyntax lastDirective = syntaxNode.GetLastDirective(predicate);
						if (lastDirective != null)
						{
							return lastDirective;
						}
					}
					else
					{
						foreach (SyntaxTrivia syntaxTrivia in syntaxNodeOrToken.AsToken().LeadingTrivia.Reverse())
						{
							if (syntaxTrivia.IsDirective)
							{
								Microsoft.CodeAnalysis.CSharp.Syntax.DirectiveTriviaSyntax directiveTriviaSyntax = (Microsoft.CodeAnalysis.CSharp.Syntax.DirectiveTriviaSyntax)syntaxTrivia.GetStructure();
								if (predicate == null || predicate.Invoke(directiveTriviaSyntax))
								{
									return directiveTriviaSyntax;
								}
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x000E9B7A File Offset: 0x000E7D7A
		public new SyntaxToken GetFirstToken(bool includeZeroWidth = false, bool includeSkipped = false, bool includeDirectives = false, bool includeDocumentationComments = false)
		{
			return base.GetFirstToken(includeZeroWidth, includeSkipped, includeDirectives, includeDocumentationComments);
		}

		// Token: 0x06002BA1 RID: 11169 RVA: 0x000E9B87 File Offset: 0x000E7D87
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal SyntaxToken GetFirstToken(Func<SyntaxToken, bool> predicate, Func<SyntaxTrivia, bool> stepInto = null)
		{
			return SyntaxNavigator.Instance.GetFirstToken(this, predicate, stepInto);
		}

		// Token: 0x06002BA2 RID: 11170 RVA: 0x000E9B96 File Offset: 0x000E7D96
		public new SyntaxToken GetLastToken(bool includeZeroWidth = false, bool includeSkipped = false, bool includeDirectives = false, bool includeDocumentationComments = false)
		{
			return base.GetLastToken(includeZeroWidth, includeSkipped, includeDirectives, includeDocumentationComments);
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x000E9BA3 File Offset: 0x000E7DA3
		public new SyntaxToken FindToken(int position, bool findInsideTrivia = false)
		{
			return base.FindToken(position, findInsideTrivia);
		}

		// Token: 0x06002BA4 RID: 11172 RVA: 0x000E9BB0 File Offset: 0x000E7DB0
		internal SyntaxToken FindTokenIncludingCrefAndNameAttributes(int position)
		{
			SyntaxToken syntaxToken = this.FindToken(position, false);
			SyntaxTrivia triviaFromSyntaxToken = SyntaxNode.GetTriviaFromSyntaxToken(position, in syntaxToken);
			if (!SyntaxFacts.IsDocumentationCommentTrivia(triviaFromSyntaxToken.Kind()))
			{
				return syntaxToken;
			}
			SyntaxToken syntaxToken2 = ((CSharpSyntaxNode)triviaFromSyntaxToken.GetStructure()).FindTokenInternal(position);
			CSharpSyntaxNode csharpSyntaxNode = (CSharpSyntaxNode)syntaxToken2.Parent;
			while (csharpSyntaxNode != null)
			{
				if (csharpSyntaxNode.Kind() == SyntaxKind.XmlCrefAttribute || csharpSyntaxNode.Kind() == SyntaxKind.XmlNameAttribute)
				{
					if (!LookupPosition.IsInXmlAttributeValue(position, (Microsoft.CodeAnalysis.CSharp.Syntax.XmlAttributeSyntax)csharpSyntaxNode))
					{
						return syntaxToken;
					}
					return syntaxToken2;
				}
				else
				{
					csharpSyntaxNode = csharpSyntaxNode.Parent;
				}
			}
			return syntaxToken;
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x000E9C37 File Offset: 0x000E7E37
		public new SyntaxTrivia FindTrivia(int position, Func<SyntaxTrivia, bool> stepInto)
		{
			return base.FindTrivia(position, stepInto);
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x000E9C41 File Offset: 0x000E7E41
		public new SyntaxTrivia FindTrivia(int position, bool findInsideTrivia = false)
		{
			return base.FindTrivia(position, findInsideTrivia);
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x000E9C4B File Offset: 0x000E7E4B
		protected override bool EquivalentToCore(SyntaxNode other)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06002BA8 RID: 11176 RVA: 0x000E9C52 File Offset: 0x000E7E52
		protected override SyntaxTree SyntaxTreeCore
		{
			get
			{
				return this.SyntaxTree;
			}
		}

		// Token: 0x06002BA9 RID: 11177 RVA: 0x000E9C5A File Offset: 0x000E7E5A
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		protected internal override SyntaxNode ReplaceCore<[global::System.Runtime.CompilerServices.Nullable(0)] TNode>([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] IEnumerable<TNode> nodes = null, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1, 1 })] Func<TNode, TNode, SyntaxNode> computeReplacementNode = null, IEnumerable<SyntaxToken> tokens = null, Func<SyntaxToken, SyntaxToken, SyntaxToken> computeReplacementToken = null, IEnumerable<SyntaxTrivia> trivia = null, Func<SyntaxTrivia, SyntaxTrivia, SyntaxTrivia> computeReplacementTrivia = null)
		{
			return SyntaxReplacer.Replace<TNode>(this, nodes, computeReplacementNode, tokens, computeReplacementToken, trivia, computeReplacementTrivia).AsRootOfNewTreeWithOptionsFrom(this.SyntaxTree);
		}

		// Token: 0x06002BAA RID: 11178 RVA: 0x000E9C76 File Offset: 0x000E7E76
		protected internal override SyntaxNode ReplaceNodeInListCore(SyntaxNode originalNode, IEnumerable<SyntaxNode> replacementNodes)
		{
			return SyntaxReplacer.ReplaceNodeInList(this, originalNode, replacementNodes).AsRootOfNewTreeWithOptionsFrom(this.SyntaxTree);
		}

		// Token: 0x06002BAB RID: 11179 RVA: 0x000E9C8B File Offset: 0x000E7E8B
		protected internal override SyntaxNode InsertNodesInListCore(SyntaxNode nodeInList, IEnumerable<SyntaxNode> nodesToInsert, bool insertBefore)
		{
			return SyntaxReplacer.InsertNodeInList(this, nodeInList, nodesToInsert, insertBefore).AsRootOfNewTreeWithOptionsFrom(this.SyntaxTree);
		}

		// Token: 0x06002BAC RID: 11180 RVA: 0x000E9CA1 File Offset: 0x000E7EA1
		protected internal override SyntaxNode ReplaceTokenInListCore(SyntaxToken originalToken, IEnumerable<SyntaxToken> newTokens)
		{
			return SyntaxReplacer.ReplaceTokenInList(this, originalToken, newTokens).AsRootOfNewTreeWithOptionsFrom(this.SyntaxTree);
		}

		// Token: 0x06002BAD RID: 11181 RVA: 0x000E9CB6 File Offset: 0x000E7EB6
		protected internal override SyntaxNode InsertTokensInListCore(SyntaxToken originalToken, IEnumerable<SyntaxToken> newTokens, bool insertBefore)
		{
			return SyntaxReplacer.InsertTokenInList(this, originalToken, newTokens, insertBefore).AsRootOfNewTreeWithOptionsFrom(this.SyntaxTree);
		}

		// Token: 0x06002BAE RID: 11182 RVA: 0x000E9CCC File Offset: 0x000E7ECC
		protected internal override SyntaxNode ReplaceTriviaInListCore(SyntaxTrivia originalTrivia, IEnumerable<SyntaxTrivia> newTrivia)
		{
			return SyntaxReplacer.ReplaceTriviaInList(this, originalTrivia, newTrivia).AsRootOfNewTreeWithOptionsFrom(this.SyntaxTree);
		}

		// Token: 0x06002BAF RID: 11183 RVA: 0x000E9CE1 File Offset: 0x000E7EE1
		protected internal override SyntaxNode InsertTriviaInListCore(SyntaxTrivia originalTrivia, IEnumerable<SyntaxTrivia> newTrivia, bool insertBefore)
		{
			return SyntaxReplacer.InsertTriviaInList(this, originalTrivia, newTrivia, insertBefore).AsRootOfNewTreeWithOptionsFrom(this.SyntaxTree);
		}

		// Token: 0x06002BB0 RID: 11184 RVA: 0x000E9CF7 File Offset: 0x000E7EF7
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		protected internal override SyntaxNode RemoveNodesCore(IEnumerable<SyntaxNode> nodes, SyntaxRemoveOptions options)
		{
			return SyntaxNodeRemover.RemoveNodes<CSharpSyntaxNode>(this, Enumerable.Cast<CSharpSyntaxNode>(nodes), options).AsRootOfNewTreeWithOptionsFrom(this.SyntaxTree);
		}

		// Token: 0x06002BB1 RID: 11185 RVA: 0x000E9D11 File Offset: 0x000E7F11
		protected internal override SyntaxNode NormalizeWhitespaceCore(string indentation, string eol, bool elasticTrivia)
		{
			return SyntaxNormalizer.Normalize<CSharpSyntaxNode>(this, indentation, eol, elasticTrivia).AsRootOfNewTreeWithOptionsFrom(this.SyntaxTree);
		}

		// Token: 0x06002BB2 RID: 11186 RVA: 0x000E9D27 File Offset: 0x000E7F27
		protected override bool IsEquivalentToCore(SyntaxNode node, bool topLevel = false)
		{
			return SyntaxFactory.AreEquivalent(this, (CSharpSyntaxNode)node, topLevel);
		}

		// Token: 0x06002BB3 RID: 11187 RVA: 0x000E9D38 File Offset: 0x000E7F38
		internal override bool ShouldCreateWeakList()
		{
			if (this.Kind() == SyntaxKind.Block)
			{
				CSharpSyntaxNode parent = this.Parent;
				if (parent is Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax || parent is Microsoft.CodeAnalysis.CSharp.Syntax.AccessorDeclarationSyntax)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002BB4 RID: 11188 RVA: 0x000E9D6C File Offset: 0x000E7F6C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		string IFormattable.ToString(string format, IFormatProvider formatProvider)
		{
			return this.ToString();
		}
	}
}
