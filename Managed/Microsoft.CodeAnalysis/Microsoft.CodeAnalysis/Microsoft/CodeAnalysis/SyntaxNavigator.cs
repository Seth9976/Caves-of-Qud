using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200027A RID: 634
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class SyntaxNavigator
	{
		// Token: 0x06001899 RID: 6297 RVA: 0x00049E0E File Offset: 0x0004800E
		private SyntaxNavigator()
		{
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x00049E18 File Offset: 0x00048018
		[NullableContext(2)]
		private static Func<SyntaxTrivia, bool> GetStepIntoFunction(bool skipped, bool directives, bool docComments)
		{
			SyntaxNavigator.SyntaxKinds syntaxKinds = (skipped ? SyntaxNavigator.SyntaxKinds.SkippedTokens : ((SyntaxNavigator.SyntaxKinds)0)) | (directives ? SyntaxNavigator.SyntaxKinds.Directives : ((SyntaxNavigator.SyntaxKinds)0)) | (docComments ? SyntaxNavigator.SyntaxKinds.DocComments : ((SyntaxNavigator.SyntaxKinds)0));
			return SyntaxNavigator.s_stepIntoFunctions[(int)syntaxKinds];
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x00049E44 File Offset: 0x00048044
		private static Func<SyntaxToken, bool> GetPredicateFunction(bool includeZeroWidth)
		{
			if (!includeZeroWidth)
			{
				return SyntaxToken.NonZeroWidth;
			}
			return SyntaxToken.Any;
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x00049E54 File Offset: 0x00048054
		[NullableContext(2)]
		private static bool Matches(Func<SyntaxToken, bool> predicate, SyntaxToken token)
		{
			return predicate == null || predicate == SyntaxToken.Any || predicate.Invoke(token);
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x00049E6A File Offset: 0x0004806A
		internal SyntaxToken GetFirstToken(in SyntaxNode current, bool includeZeroWidth, bool includeSkipped, bool includeDirectives, bool includeDocumentationComments)
		{
			return this.GetFirstToken(current, SyntaxNavigator.GetPredicateFunction(includeZeroWidth), SyntaxNavigator.GetStepIntoFunction(includeSkipped, includeDirectives, includeDocumentationComments));
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x00049E84 File Offset: 0x00048084
		internal SyntaxToken GetLastToken(in SyntaxNode current, bool includeZeroWidth, bool includeSkipped, bool includeDirectives, bool includeDocumentationComments)
		{
			return this.GetLastToken(current, SyntaxNavigator.GetPredicateFunction(includeZeroWidth), SyntaxNavigator.GetStepIntoFunction(includeSkipped, includeDirectives, includeDocumentationComments));
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x00049E9E File Offset: 0x0004809E
		internal SyntaxToken GetPreviousToken(in SyntaxToken current, bool includeZeroWidth, bool includeSkipped, bool includeDirectives, bool includeDocumentationComments)
		{
			return this.GetPreviousToken(in current, SyntaxNavigator.GetPredicateFunction(includeZeroWidth), SyntaxNavigator.GetStepIntoFunction(includeSkipped, includeDirectives, includeDocumentationComments));
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x00049EB7 File Offset: 0x000480B7
		internal SyntaxToken GetNextToken(in SyntaxToken current, bool includeZeroWidth, bool includeSkipped, bool includeDirectives, bool includeDocumentationComments)
		{
			return this.GetNextToken(in current, SyntaxNavigator.GetPredicateFunction(includeZeroWidth), SyntaxNavigator.GetStepIntoFunction(includeSkipped, includeDirectives, includeDocumentationComments));
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x00049ED0 File Offset: 0x000480D0
		internal SyntaxToken GetPreviousToken(in SyntaxToken current, Func<SyntaxToken, bool> predicate, [Nullable(2)] Func<SyntaxTrivia, bool> stepInto)
		{
			return this.GetPreviousToken(in current, predicate, stepInto != null, stepInto);
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x00049EDF File Offset: 0x000480DF
		internal SyntaxToken GetNextToken(in SyntaxToken current, Func<SyntaxToken, bool> predicate, [Nullable(2)] Func<SyntaxTrivia, bool> stepInto)
		{
			return this.GetNextToken(in current, predicate, stepInto != null, stepInto);
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x00049EF0 File Offset: 0x000480F0
		[NullableContext(2)]
		internal SyntaxToken GetFirstToken([Nullable(1)] SyntaxNode current, Func<SyntaxToken, bool> predicate, Func<SyntaxTrivia, bool> stepInto)
		{
			Stack<ChildSyntaxList.Enumerator> stack = SyntaxNavigator.s_childEnumeratorStackPool.Allocate();
			SyntaxToken syntaxToken;
			try
			{
				stack.Push(current.ChildNodesAndTokens().GetEnumerator());
				while (stack.Count > 0)
				{
					ChildSyntaxList.Enumerator enumerator = stack.Pop();
					if (enumerator.MoveNext())
					{
						SyntaxNodeOrToken syntaxNodeOrToken = enumerator.Current;
						if (syntaxNodeOrToken.IsToken)
						{
							SyntaxToken firstToken = this.GetFirstToken(syntaxNodeOrToken.AsToken(), predicate, stepInto);
							if (firstToken.RawKind != 0)
							{
								return firstToken;
							}
						}
						stack.Push(enumerator);
						if (syntaxNodeOrToken.IsNode)
						{
							stack.Push(syntaxNodeOrToken.AsNode().ChildNodesAndTokens().GetEnumerator());
						}
					}
				}
				syntaxToken = default(SyntaxToken);
			}
			finally
			{
				stack.Clear();
				SyntaxNavigator.s_childEnumeratorStackPool.Free(stack);
			}
			return syntaxToken;
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x00049FC4 File Offset: 0x000481C4
		internal SyntaxToken GetLastToken(SyntaxNode current, Func<SyntaxToken, bool> predicate, [Nullable(2)] Func<SyntaxTrivia, bool> stepInto)
		{
			Stack<ChildSyntaxList.Reversed.Enumerator> stack = SyntaxNavigator.s_childReversedEnumeratorStackPool.Allocate();
			SyntaxToken syntaxToken;
			try
			{
				stack.Push(current.ChildNodesAndTokens().Reverse().GetEnumerator());
				while (stack.Count > 0)
				{
					ChildSyntaxList.Reversed.Enumerator enumerator = stack.Pop();
					if (enumerator.MoveNext())
					{
						SyntaxNodeOrToken syntaxNodeOrToken = enumerator.Current;
						if (syntaxNodeOrToken.IsToken)
						{
							SyntaxToken lastToken = this.GetLastToken(syntaxNodeOrToken.AsToken(), predicate, stepInto);
							if (lastToken.RawKind != 0)
							{
								return lastToken;
							}
						}
						stack.Push(enumerator);
						if (syntaxNodeOrToken.IsNode)
						{
							stack.Push(syntaxNodeOrToken.AsNode().ChildNodesAndTokens().Reverse()
								.GetEnumerator());
						}
					}
				}
				syntaxToken = default(SyntaxToken);
			}
			finally
			{
				stack.Clear();
				SyntaxNavigator.s_childReversedEnumeratorStackPool.Free(stack);
			}
			return syntaxToken;
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x0004A0AC File Offset: 0x000482AC
		private SyntaxToken GetFirstToken(SyntaxTriviaList triviaList, [Nullable(2)] Func<SyntaxToken, bool> predicate, Func<SyntaxTrivia, bool> stepInto)
		{
			foreach (SyntaxTrivia syntaxTrivia in triviaList)
			{
				SyntaxNode syntaxNode;
				if (syntaxTrivia.TryGetStructure(out syntaxNode) && stepInto.Invoke(syntaxTrivia))
				{
					SyntaxToken firstToken = this.GetFirstToken(syntaxNode, predicate, stepInto);
					if (firstToken.RawKind != 0)
					{
						return firstToken;
					}
				}
			}
			return default(SyntaxToken);
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x0004A108 File Offset: 0x00048308
		private SyntaxToken GetLastToken(SyntaxTriviaList list, Func<SyntaxToken, bool> predicate, Func<SyntaxTrivia, bool> stepInto)
		{
			foreach (SyntaxTrivia syntaxTrivia in list.Reverse())
			{
				SyntaxToken syntaxToken;
				if (this.TryGetLastTokenForStructuredTrivia(syntaxTrivia, predicate, stepInto, out syntaxToken))
				{
					return syntaxToken;
				}
			}
			return default(SyntaxToken);
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x0004A154 File Offset: 0x00048354
		private bool TryGetLastTokenForStructuredTrivia(SyntaxTrivia trivia, Func<SyntaxToken, bool> predicate, [Nullable(2)] Func<SyntaxTrivia, bool> stepInto, out SyntaxToken token)
		{
			token = default(SyntaxToken);
			SyntaxNode syntaxNode;
			if (!trivia.TryGetStructure(out syntaxNode) || stepInto == null || !stepInto.Invoke(trivia))
			{
				return false;
			}
			token = this.GetLastToken(syntaxNode, predicate, stepInto);
			return token.RawKind != 0;
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x0004A19C File Offset: 0x0004839C
		[NullableContext(2)]
		private SyntaxToken GetFirstToken(SyntaxToken token, Func<SyntaxToken, bool> predicate, Func<SyntaxTrivia, bool> stepInto)
		{
			if (stepInto != null)
			{
				SyntaxToken firstToken = this.GetFirstToken(token.LeadingTrivia, predicate, stepInto);
				if (firstToken.RawKind != 0)
				{
					return firstToken;
				}
			}
			if (SyntaxNavigator.Matches(predicate, token))
			{
				return token;
			}
			if (stepInto != null)
			{
				SyntaxToken firstToken2 = this.GetFirstToken(token.TrailingTrivia, predicate, stepInto);
				if (firstToken2.RawKind != 0)
				{
					return firstToken2;
				}
			}
			return default(SyntaxToken);
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x0004A1FC File Offset: 0x000483FC
		private SyntaxToken GetLastToken(SyntaxToken token, Func<SyntaxToken, bool> predicate, [Nullable(2)] Func<SyntaxTrivia, bool> stepInto)
		{
			if (stepInto != null)
			{
				SyntaxToken lastToken = this.GetLastToken(token.TrailingTrivia, predicate, stepInto);
				if (lastToken.RawKind != 0)
				{
					return lastToken;
				}
			}
			if (SyntaxNavigator.Matches(predicate, token))
			{
				return token;
			}
			if (stepInto != null)
			{
				SyntaxToken lastToken2 = this.GetLastToken(token.LeadingTrivia, predicate, stepInto);
				if (lastToken2.RawKind != 0)
				{
					return lastToken2;
				}
			}
			return default(SyntaxToken);
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x0004A25C File Offset: 0x0004845C
		[NullableContext(2)]
		internal SyntaxToken GetNextToken(SyntaxTrivia current, Func<SyntaxToken, bool> predicate, Func<SyntaxTrivia, bool> stepInto)
		{
			bool flag = false;
			SyntaxTrivia syntaxTrivia = current;
			SyntaxToken syntaxToken = current.Token;
			SyntaxToken syntaxToken2 = this.GetNextToken(syntaxTrivia, syntaxToken.LeadingTrivia, predicate, stepInto, ref flag);
			if (syntaxToken2.RawKind != 0)
			{
				return syntaxToken2;
			}
			if (flag && (predicate == null || predicate == SyntaxToken.Any || predicate.Invoke(current.Token)))
			{
				return current.Token;
			}
			SyntaxTrivia syntaxTrivia2 = current;
			syntaxToken = current.Token;
			syntaxToken2 = this.GetNextToken(syntaxTrivia2, syntaxToken.TrailingTrivia, predicate, stepInto, ref flag);
			if (syntaxToken2.RawKind != 0)
			{
				return syntaxToken2;
			}
			syntaxToken = current.Token;
			return this.GetNextToken(in syntaxToken, predicate, false, stepInto);
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x0004A2F4 File Offset: 0x000484F4
		internal SyntaxToken GetPreviousToken(SyntaxTrivia current, Func<SyntaxToken, bool> predicate, [Nullable(2)] Func<SyntaxTrivia, bool> stepInto)
		{
			bool flag = false;
			SyntaxTrivia syntaxTrivia = current;
			SyntaxToken syntaxToken = current.Token;
			SyntaxToken syntaxToken2 = this.GetPreviousToken(syntaxTrivia, syntaxToken.TrailingTrivia, predicate, stepInto, ref flag);
			if (syntaxToken2.RawKind != 0)
			{
				return syntaxToken2;
			}
			if (flag && SyntaxNavigator.Matches(predicate, current.Token))
			{
				return current.Token;
			}
			SyntaxTrivia syntaxTrivia2 = current;
			syntaxToken = current.Token;
			syntaxToken2 = this.GetPreviousToken(syntaxTrivia2, syntaxToken.LeadingTrivia, predicate, stepInto, ref flag);
			if (syntaxToken2.RawKind != 0)
			{
				return syntaxToken2;
			}
			syntaxToken = current.Token;
			return this.GetPreviousToken(in syntaxToken, predicate, false, stepInto);
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x0004A37C File Offset: 0x0004857C
		[NullableContext(2)]
		private SyntaxToken GetNextToken(SyntaxTrivia current, SyntaxTriviaList list, Func<SyntaxToken, bool> predicate, Func<SyntaxTrivia, bool> stepInto, ref bool returnNext)
		{
			foreach (SyntaxTrivia syntaxTrivia in list)
			{
				if (returnNext)
				{
					SyntaxNode syntaxNode;
					if (syntaxTrivia.TryGetStructure(out syntaxNode) && stepInto != null && stepInto.Invoke(syntaxTrivia))
					{
						SyntaxToken firstToken = this.GetFirstToken(syntaxNode, predicate, stepInto);
						if (firstToken.RawKind != 0)
						{
							return firstToken;
						}
					}
				}
				else if (syntaxTrivia == current)
				{
					returnNext = true;
				}
			}
			return default(SyntaxToken);
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x0004A3F0 File Offset: 0x000485F0
		private SyntaxToken GetPreviousToken(SyntaxTrivia current, SyntaxTriviaList list, Func<SyntaxToken, bool> predicate, [Nullable(2)] Func<SyntaxTrivia, bool> stepInto, ref bool returnPrevious)
		{
			foreach (SyntaxTrivia syntaxTrivia in list.Reverse())
			{
				if (returnPrevious)
				{
					SyntaxToken syntaxToken;
					if (this.TryGetLastTokenForStructuredTrivia(syntaxTrivia, predicate, stepInto, out syntaxToken))
					{
						return syntaxToken;
					}
				}
				else if (syntaxTrivia == current)
				{
					returnPrevious = true;
				}
			}
			return default(SyntaxToken);
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x0004A44C File Offset: 0x0004864C
		[NullableContext(2)]
		internal SyntaxToken GetNextToken([Nullable(1)] SyntaxNode node, Func<SyntaxToken, bool> predicate, Func<SyntaxTrivia, bool> stepInto)
		{
			while (node.Parent != null)
			{
				bool flag = false;
				foreach (SyntaxNodeOrToken syntaxNodeOrToken in node.Parent.ChildNodesAndTokens())
				{
					if (flag)
					{
						if (syntaxNodeOrToken.IsToken)
						{
							SyntaxToken firstToken = this.GetFirstToken(syntaxNodeOrToken.AsToken(), predicate, stepInto);
							if (firstToken.RawKind != 0)
							{
								return firstToken;
							}
						}
						else
						{
							SyntaxToken firstToken2 = this.GetFirstToken(syntaxNodeOrToken.AsNode(), predicate, stepInto);
							if (firstToken2.RawKind != 0)
							{
								return firstToken2;
							}
						}
					}
					else if (syntaxNodeOrToken.IsNode && syntaxNodeOrToken.AsNode() == node)
					{
						flag = true;
					}
				}
				node = node.Parent;
			}
			if (node.IsStructuredTrivia)
			{
				return this.GetNextToken(((IStructuredTriviaSyntax)node).ParentTrivia, predicate, stepInto);
			}
			return default(SyntaxToken);
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x0004A51C File Offset: 0x0004871C
		internal SyntaxToken GetPreviousToken(SyntaxNode node, Func<SyntaxToken, bool> predicate, [Nullable(2)] Func<SyntaxTrivia, bool> stepInto)
		{
			while (node.Parent != null)
			{
				bool flag = false;
				foreach (SyntaxNodeOrToken syntaxNodeOrToken in node.Parent.ChildNodesAndTokens().Reverse())
				{
					if (flag)
					{
						if (syntaxNodeOrToken.IsToken)
						{
							SyntaxToken lastToken = this.GetLastToken(syntaxNodeOrToken.AsToken(), predicate, stepInto);
							if (lastToken.RawKind != 0)
							{
								return lastToken;
							}
						}
						else
						{
							SyntaxToken lastToken2 = this.GetLastToken(syntaxNodeOrToken.AsNode(), predicate, stepInto);
							if (lastToken2.RawKind != 0)
							{
								return lastToken2;
							}
						}
					}
					else if (syntaxNodeOrToken.IsNode && syntaxNodeOrToken.AsNode() == node)
					{
						flag = true;
					}
				}
				node = node.Parent;
			}
			if (node.IsStructuredTrivia)
			{
				return this.GetPreviousToken(((IStructuredTriviaSyntax)node).ParentTrivia, predicate, stepInto);
			}
			return default(SyntaxToken);
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x0004A5F4 File Offset: 0x000487F4
		[NullableContext(2)]
		internal SyntaxToken GetNextToken(in SyntaxToken current, Func<SyntaxToken, bool> predicate, bool searchInsideCurrentTokenTrailingTrivia, Func<SyntaxTrivia, bool> stepInto)
		{
			if (current.Parent != null)
			{
				if (searchInsideCurrentTokenTrailingTrivia)
				{
					SyntaxToken firstToken = this.GetFirstToken(current.TrailingTrivia, predicate, stepInto);
					if (firstToken.RawKind != 0)
					{
						return firstToken;
					}
				}
				bool flag = false;
				foreach (SyntaxNodeOrToken syntaxNodeOrToken in current.Parent.ChildNodesAndTokens())
				{
					if (flag)
					{
						if (syntaxNodeOrToken.IsToken)
						{
							SyntaxToken firstToken2 = this.GetFirstToken(syntaxNodeOrToken.AsToken(), predicate, stepInto);
							if (firstToken2.RawKind != 0)
							{
								return firstToken2;
							}
						}
						else
						{
							SyntaxToken firstToken3 = this.GetFirstToken(syntaxNodeOrToken.AsNode(), predicate, stepInto);
							if (firstToken3.RawKind != 0)
							{
								return firstToken3;
							}
						}
					}
					else if (syntaxNodeOrToken.IsToken && syntaxNodeOrToken.AsToken() == current)
					{
						flag = true;
					}
				}
				return this.GetNextToken(current.Parent, predicate, stepInto);
			}
			return default(SyntaxToken);
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x0004A6D8 File Offset: 0x000488D8
		internal SyntaxToken GetPreviousToken(in SyntaxToken current, Func<SyntaxToken, bool> predicate, bool searchInsideCurrentTokenLeadingTrivia, [Nullable(2)] Func<SyntaxTrivia, bool> stepInto)
		{
			if (current.Parent != null)
			{
				if (searchInsideCurrentTokenLeadingTrivia)
				{
					SyntaxToken lastToken = this.GetLastToken(current.LeadingTrivia, predicate, stepInto);
					if (lastToken.RawKind != 0)
					{
						return lastToken;
					}
				}
				bool flag = false;
				foreach (SyntaxNodeOrToken syntaxNodeOrToken in current.Parent.ChildNodesAndTokens().Reverse())
				{
					if (flag)
					{
						if (syntaxNodeOrToken.IsToken)
						{
							SyntaxToken lastToken2 = this.GetLastToken(syntaxNodeOrToken.AsToken(), predicate, stepInto);
							if (lastToken2.RawKind != 0)
							{
								return lastToken2;
							}
						}
						else
						{
							SyntaxToken lastToken3 = this.GetLastToken(syntaxNodeOrToken.AsNode(), predicate, stepInto);
							if (lastToken3.RawKind != 0)
							{
								return lastToken3;
							}
						}
					}
					else if (syntaxNodeOrToken.IsToken && syntaxNodeOrToken.AsToken() == current)
					{
						flag = true;
					}
				}
				return this.GetPreviousToken(current.Parent, predicate, stepInto);
			}
			return default(SyntaxToken);
		}

		// Token: 0x04000A0D RID: 2573
		private const int None = 0;

		// Token: 0x04000A0E RID: 2574
		public static readonly SyntaxNavigator Instance = new SyntaxNavigator();

		// Token: 0x04000A0F RID: 2575
		[Nullable(new byte[] { 1, 2 })]
		private static readonly Func<SyntaxTrivia, bool>[] s_stepIntoFunctions = new Func<SyntaxTrivia, bool>[]
		{
			default(Func<SyntaxTrivia, bool>),
			(SyntaxTrivia t) => t.IsDocumentationCommentTrivia,
			(SyntaxTrivia t) => t.IsDirective,
			(SyntaxTrivia t) => t.IsDirective || t.IsDocumentationCommentTrivia,
			(SyntaxTrivia t) => t.IsSkippedTokensTrivia,
			(SyntaxTrivia t) => t.IsSkippedTokensTrivia || t.IsDocumentationCommentTrivia,
			(SyntaxTrivia t) => t.IsSkippedTokensTrivia || t.IsDirective,
			(SyntaxTrivia t) => t.IsSkippedTokensTrivia || t.IsDirective || t.IsDocumentationCommentTrivia
		};

		// Token: 0x04000A10 RID: 2576
		private static readonly ObjectPool<Stack<ChildSyntaxList.Enumerator>> s_childEnumeratorStackPool = new ObjectPool<Stack<ChildSyntaxList.Enumerator>>(() => new Stack<ChildSyntaxList.Enumerator>(), 10);

		// Token: 0x04000A11 RID: 2577
		private static readonly ObjectPool<Stack<ChildSyntaxList.Reversed.Enumerator>> s_childReversedEnumeratorStackPool = new ObjectPool<Stack<ChildSyntaxList.Reversed.Enumerator>>(() => new Stack<ChildSyntaxList.Reversed.Enumerator>(), 10);

		// Token: 0x02000608 RID: 1544
		[NullableContext(0)]
		[Flags]
		private enum SyntaxKinds
		{
			// Token: 0x0400178B RID: 6027
			DocComments = 1,
			// Token: 0x0400178C RID: 6028
			Directives = 2,
			// Token: 0x0400178D RID: 6029
			SkippedTokens = 4
		}
	}
}
