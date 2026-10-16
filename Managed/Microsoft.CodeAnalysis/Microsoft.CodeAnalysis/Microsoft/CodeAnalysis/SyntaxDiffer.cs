using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000278 RID: 632
	[NullableContext(1)]
	[Nullable(0)]
	internal class SyntaxDiffer
	{
		// Token: 0x06001850 RID: 6224 RVA: 0x000483AC File Offset: 0x000465AC
		private SyntaxDiffer(SyntaxNode oldNode, SyntaxNode newNode, bool computeNewText)
		{
			this._oldNodes.Push(oldNode);
			this._newNodes.Push(newNode);
			this._oldSpan = oldNode.FullSpan;
			this._computeNewText = computeNewText;
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x00048430 File Offset: 0x00046630
		internal static IList<TextChange> GetTextChanges(SyntaxTree before, SyntaxTree after)
		{
			if (before == after)
			{
				return SpecializedCollections.EmptyList<TextChange>();
			}
			if (before == null)
			{
				return new TextChange[]
				{
					new TextChange(new TextSpan(0, 0), after.GetText(default(CancellationToken)).ToString())
				};
			}
			if (after == null)
			{
				throw new ArgumentNullException("after");
			}
			return SyntaxDiffer.GetTextChanges(before.GetRoot(default(CancellationToken)), after.GetRoot(default(CancellationToken)));
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x000484A9 File Offset: 0x000466A9
		internal static IList<TextChange> GetTextChanges(SyntaxNode oldNode, SyntaxNode newNode)
		{
			return new SyntaxDiffer(oldNode, newNode, true).ComputeTextChangesFromOld();
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x000484B8 File Offset: 0x000466B8
		private IList<TextChange> ComputeTextChangesFromOld()
		{
			this.ComputeChangeRecords();
			return Enumerable.ToList<TextChange>(Enumerable.Select<SyntaxDiffer.ChangeRangeWithText, TextChange>(this.ReduceChanges(this._changes), (SyntaxDiffer.ChangeRangeWithText c) => new TextChange(c.Range.Span, c.NewText)));
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x000484F8 File Offset: 0x000466F8
		[NullableContext(2)]
		[return: Nullable(1)]
		internal static IList<TextSpan> GetPossiblyDifferentTextSpans(SyntaxTree before, SyntaxTree after)
		{
			if (before == after)
			{
				return SpecializedCollections.EmptyList<TextSpan>();
			}
			if (before == null)
			{
				return new TextSpan[]
				{
					new TextSpan(0, after.GetText(default(CancellationToken)).Length)
				};
			}
			if (after == null)
			{
				throw new ArgumentNullException("after");
			}
			return SyntaxDiffer.GetPossiblyDifferentTextSpans(before.GetRoot(default(CancellationToken)), after.GetRoot(default(CancellationToken)));
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x0004856B File Offset: 0x0004676B
		internal static IList<TextSpan> GetPossiblyDifferentTextSpans(SyntaxNode oldNode, SyntaxNode newNode)
		{
			return new SyntaxDiffer(oldNode, newNode, false).ComputeSpansInNew();
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x0004857C File Offset: 0x0004677C
		private IList<TextSpan> ComputeSpansInNew()
		{
			this.ComputeChangeRecords();
			List<SyntaxDiffer.ChangeRangeWithText> list = this.ReduceChanges(this._changes);
			List<TextSpan> list2 = new List<TextSpan>();
			int num = 0;
			foreach (SyntaxDiffer.ChangeRangeWithText changeRangeWithText in list)
			{
				if (changeRangeWithText.Range.NewLength > 0)
				{
					int num2 = changeRangeWithText.Range.Span.Start + num;
					list2.Add(new TextSpan(num2, changeRangeWithText.Range.NewLength));
				}
				num += changeRangeWithText.Range.NewLength - changeRangeWithText.Range.Span.Length;
			}
			return list2;
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x00048644 File Offset: 0x00046844
		private void ComputeChangeRecords()
		{
			while (this._newNodes.Count != 0)
			{
				if (this._oldNodes.Count == 0)
				{
					if (this._newNodes.Count > 0)
					{
						this.RecordInsertNew(this._newNodes.Count);
						return;
					}
					return;
				}
				else
				{
					SyntaxDiffer.DiffAction nextAction = this.GetNextAction();
					switch (nextAction.Operation)
					{
					case SyntaxDiffer.DiffOp.SkipBoth:
						SyntaxDiffer.RemoveFirst(this._oldNodes, nextAction.Count);
						SyntaxDiffer.RemoveFirst(this._newNodes, nextAction.Count);
						break;
					case SyntaxDiffer.DiffOp.ReduceOld:
						SyntaxDiffer.ReplaceFirstWithChildren(this._oldNodes);
						break;
					case SyntaxDiffer.DiffOp.ReduceNew:
						SyntaxDiffer.ReplaceFirstWithChildren(this._newNodes);
						break;
					case SyntaxDiffer.DiffOp.ReduceBoth:
						SyntaxDiffer.ReplaceFirstWithChildren(this._oldNodes);
						SyntaxDiffer.ReplaceFirstWithChildren(this._newNodes);
						break;
					case SyntaxDiffer.DiffOp.InsertNew:
						this.RecordInsertNew(nextAction.Count);
						break;
					case SyntaxDiffer.DiffOp.DeleteOld:
						this.RecordDeleteOld(nextAction.Count);
						break;
					case SyntaxDiffer.DiffOp.ReplaceOldWithNew:
						this.RecordReplaceOldWithNew(nextAction.Count, nextAction.Count);
						break;
					}
				}
			}
			if (this._oldNodes.Count > 0)
			{
				this.RecordDeleteOld(this._oldNodes.Count);
				return;
			}
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x00048784 File Offset: 0x00046984
		private SyntaxDiffer.DiffAction GetNextAction()
		{
			SyntaxNodeOrToken syntaxNodeOrToken = this._oldNodes.Peek();
			bool isToken = syntaxNodeOrToken.IsToken;
			syntaxNodeOrToken = this._newNodes.Peek();
			bool isToken2 = syntaxNodeOrToken.IsToken;
			Stack<SyntaxNodeOrToken> newNodes = this._newNodes;
			syntaxNodeOrToken = this._oldNodes.Peek();
			int num;
			int num2;
			this.FindBestMatch(newNodes, in syntaxNodeOrToken, out num, out num2, 0);
			Stack<SyntaxNodeOrToken> oldNodes = this._oldNodes;
			syntaxNodeOrToken = this._newNodes.Peek();
			int num3;
			int num4;
			this.FindBestMatch(oldNodes, in syntaxNodeOrToken, out num3, out num4, 0);
			if (num == 0 && num3 == 0)
			{
				syntaxNodeOrToken = this._oldNodes.Peek();
				SyntaxNodeOrToken syntaxNodeOrToken2 = this._newNodes.Peek();
				if (SyntaxDiffer.AreIdentical(in syntaxNodeOrToken, in syntaxNodeOrToken2))
				{
					return new SyntaxDiffer.DiffAction(SyntaxDiffer.DiffOp.SkipBoth, 1);
				}
				if (!isToken && !isToken2)
				{
					return new SyntaxDiffer.DiffAction(SyntaxDiffer.DiffOp.ReduceBoth, 1);
				}
				return new SyntaxDiffer.DiffAction(SyntaxDiffer.DiffOp.ReplaceOldWithNew, 1);
			}
			else
			{
				if (num < 0 && num3 < 0)
				{
					if (!isToken && !isToken2)
					{
						syntaxNodeOrToken = this._oldNodes.Peek();
						SyntaxNodeOrToken syntaxNodeOrToken2 = this._newNodes.Peek();
						int similarity = this.GetSimilarity(in syntaxNodeOrToken, in syntaxNodeOrToken2);
						syntaxNodeOrToken = this._oldNodes.Peek();
						int length = syntaxNodeOrToken.FullSpan.Length;
						syntaxNodeOrToken = this._newNodes.Peek();
						if (similarity >= Math.Max(length, syntaxNodeOrToken.FullSpan.Length))
						{
							return new SyntaxDiffer.DiffAction(SyntaxDiffer.DiffOp.ReduceBoth, 1);
						}
					}
					return new SyntaxDiffer.DiffAction(SyntaxDiffer.DiffOp.ReplaceOldWithNew, 1);
				}
				if (num3 < 0 || num2 >= num4)
				{
					if (num > 0)
					{
						Stack<SyntaxNodeOrToken> oldNodes2 = this._oldNodes;
						syntaxNodeOrToken = this._oldNodes.Peek();
						int num5;
						int num6;
						this.FindBestMatch(oldNodes2, in syntaxNodeOrToken, out num5, out num6, 1);
						if (num5 < 1 || num6 < num2)
						{
							return new SyntaxDiffer.DiffAction(SyntaxDiffer.DiffOp.InsertNew, num);
						}
					}
					if (isToken2)
					{
						return new SyntaxDiffer.DiffAction(SyntaxDiffer.DiffOp.ReplaceOldWithNew, 1);
					}
					syntaxNodeOrToken = this._oldNodes.Peek();
					SyntaxNodeOrToken syntaxNodeOrToken2 = this._newNodes.Peek();
					if (SyntaxDiffer.AreSimilar(in syntaxNodeOrToken, in syntaxNodeOrToken2))
					{
						return new SyntaxDiffer.DiffAction(SyntaxDiffer.DiffOp.ReduceBoth, 1);
					}
					return new SyntaxDiffer.DiffAction(SyntaxDiffer.DiffOp.ReduceNew, 1);
				}
				else
				{
					if (num3 > 0)
					{
						return new SyntaxDiffer.DiffAction(SyntaxDiffer.DiffOp.DeleteOld, num3);
					}
					if (isToken)
					{
						return new SyntaxDiffer.DiffAction(SyntaxDiffer.DiffOp.ReplaceOldWithNew, 1);
					}
					syntaxNodeOrToken = this._oldNodes.Peek();
					SyntaxNodeOrToken syntaxNodeOrToken2 = this._newNodes.Peek();
					if (SyntaxDiffer.AreSimilar(in syntaxNodeOrToken, in syntaxNodeOrToken2))
					{
						return new SyntaxDiffer.DiffAction(SyntaxDiffer.DiffOp.ReduceBoth, 1);
					}
					return new SyntaxDiffer.DiffAction(SyntaxDiffer.DiffOp.ReduceOld, 1);
				}
			}
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x000489A4 File Offset: 0x00046BA4
		private static void ReplaceFirstWithChildren(Stack<SyntaxNodeOrToken> stack)
		{
			SyntaxNodeOrToken syntaxNodeOrToken = stack.Pop();
			int num = 0;
			SyntaxNodeOrToken[] array = new SyntaxNodeOrToken[syntaxNodeOrToken.ChildNodesAndTokens().Count];
			foreach (SyntaxNodeOrToken syntaxNodeOrToken2 in syntaxNodeOrToken.ChildNodesAndTokens())
			{
				if (syntaxNodeOrToken2.FullSpan.Length > 0)
				{
					array[num] = syntaxNodeOrToken2;
					num++;
				}
			}
			for (int i = num - 1; i >= 0; i--)
			{
				stack.Push(array[i]);
			}
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x00048A34 File Offset: 0x00046C34
		private void FindBestMatch(Stack<SyntaxNodeOrToken> stack, in SyntaxNodeOrToken node, out int index, out int similarity, int startIndex = 0)
		{
			index = -1;
			similarity = -1;
			int num = 0;
			foreach (SyntaxNodeOrToken syntaxNodeOrToken in stack)
			{
				if (num >= 8)
				{
					break;
				}
				if (num >= startIndex)
				{
					if (SyntaxDiffer.AreIdentical(in syntaxNodeOrToken, in node))
					{
						int length = node.FullSpan.Length;
						if (length > similarity)
						{
							index = num;
							similarity = length;
							break;
						}
					}
					else if (SyntaxDiffer.AreSimilar(in syntaxNodeOrToken, in node))
					{
						int similarity2 = this.GetSimilarity(in syntaxNodeOrToken, in node);
						if (similarity2 == node.FullSpan.Length && node.IsToken && syntaxNodeOrToken.ToFullString() == node.ToFullString())
						{
							index = num;
							similarity = similarity2;
							break;
						}
						if (similarity2 > similarity)
						{
							index = num;
							similarity = similarity2;
						}
					}
					else
					{
						int num2 = 0;
						foreach (SyntaxNodeOrToken syntaxNodeOrToken2 in syntaxNodeOrToken.ChildNodesAndTokens())
						{
							if (num2 >= 8)
							{
								break;
							}
							num2++;
							if (SyntaxDiffer.AreIdentical(in syntaxNodeOrToken2, in node))
							{
								index = num;
								similarity = node.FullSpan.Length;
								return;
							}
							if (SyntaxDiffer.AreSimilar(in syntaxNodeOrToken2, in node))
							{
								int similarity3 = this.GetSimilarity(in syntaxNodeOrToken2, in node);
								if (similarity3 > similarity)
								{
									index = num;
									similarity = similarity3;
								}
							}
						}
					}
				}
				num++;
			}
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x00048BB8 File Offset: 0x00046DB8
		private int GetSimilarity(in SyntaxNodeOrToken node1, in SyntaxNodeOrToken node2)
		{
			int num = 0;
			this._nodeSimilaritySet.Clear();
			this._tokenTextSimilaritySet.Clear();
			if (node1.IsToken && node2.IsToken)
			{
				string text = node1.ToString();
				string text2 = node2.ToString();
				if (text == text2)
				{
					num += text.Length;
				}
				foreach (SyntaxTrivia syntaxTrivia in node1.GetLeadingTrivia())
				{
					this._nodeSimilaritySet.Add(syntaxTrivia.UnderlyingNode);
				}
				foreach (SyntaxTrivia syntaxTrivia2 in node1.GetTrailingTrivia())
				{
					this._nodeSimilaritySet.Add(syntaxTrivia2.UnderlyingNode);
				}
				foreach (SyntaxTrivia syntaxTrivia3 in node2.GetLeadingTrivia())
				{
					if (this._nodeSimilaritySet.Contains(syntaxTrivia3.UnderlyingNode))
					{
						num += syntaxTrivia3.FullSpan.Length;
					}
				}
				foreach (SyntaxTrivia syntaxTrivia4 in node2.GetTrailingTrivia())
				{
					if (this._nodeSimilaritySet.Contains(syntaxTrivia4.UnderlyingNode))
					{
						num += syntaxTrivia4.FullSpan.Length;
					}
				}
			}
			else
			{
				foreach (SyntaxNodeOrToken syntaxNodeOrToken in node1.ChildNodesAndTokens())
				{
					this._nodeSimilaritySet.Add(syntaxNodeOrToken.UnderlyingNode);
					if (syntaxNodeOrToken.IsToken)
					{
						this._tokenTextSimilaritySet.Add(syntaxNodeOrToken.ToString());
					}
				}
				foreach (SyntaxNodeOrToken syntaxNodeOrToken2 in node2.ChildNodesAndTokens())
				{
					if (this._nodeSimilaritySet.Contains(syntaxNodeOrToken2.UnderlyingNode))
					{
						num += syntaxNodeOrToken2.FullSpan.Length;
					}
					else if (syntaxNodeOrToken2.IsToken)
					{
						string text3 = syntaxNodeOrToken2.ToString();
						if (this._tokenTextSimilaritySet.Contains(text3))
						{
							num += text3.Length;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x00048E02 File Offset: 0x00047002
		private static bool AreIdentical(in SyntaxNodeOrToken node1, in SyntaxNodeOrToken node2)
		{
			return node1.UnderlyingNode == node2.UnderlyingNode;
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x00048E12 File Offset: 0x00047012
		private static bool AreSimilar(in SyntaxNodeOrToken node1, in SyntaxNodeOrToken node2)
		{
			return node1.RawKind == node2.RawKind;
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x00048E24 File Offset: 0x00047024
		private void RecordDeleteOld(int oldNodeCount)
		{
			TextSpan span = SyntaxDiffer.GetSpan(this._oldNodes, 0, oldNodeCount);
			Queue<SyntaxNodeOrToken> queue = SyntaxDiffer.CopyFirst(this._oldNodes, oldNodeCount);
			SyntaxDiffer.RemoveFirst(this._oldNodes, oldNodeCount);
			this.RecordChange(new SyntaxDiffer.ChangeRecord(new TextChangeRange(span, 0), queue, null));
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x00048E6C File Offset: 0x0004706C
		private void RecordReplaceOldWithNew(int oldNodeCount, int newNodeCount)
		{
			if (oldNodeCount == 1 && newNodeCount == 1)
			{
				SyntaxNodeOrToken syntaxNodeOrToken = this._oldNodes.Pop();
				TextSpan fullSpan = syntaxNodeOrToken.FullSpan;
				SyntaxNodeOrToken syntaxNodeOrToken2 = this._newNodes.Pop();
				this.RecordChange(new TextChangeRange(fullSpan, syntaxNodeOrToken2.FullSpan.Length), in syntaxNodeOrToken, syntaxNodeOrToken2);
				return;
			}
			TextSpan span = SyntaxDiffer.GetSpan(this._oldNodes, 0, oldNodeCount);
			Queue<SyntaxNodeOrToken> queue = SyntaxDiffer.CopyFirst(this._oldNodes, oldNodeCount);
			SyntaxDiffer.RemoveFirst(this._oldNodes, oldNodeCount);
			TextSpan span2 = SyntaxDiffer.GetSpan(this._newNodes, 0, newNodeCount);
			Queue<SyntaxNodeOrToken> queue2 = SyntaxDiffer.CopyFirst(this._newNodes, newNodeCount);
			SyntaxDiffer.RemoveFirst(this._newNodes, newNodeCount);
			this.RecordChange(new SyntaxDiffer.ChangeRecord(new TextChangeRange(span, span2.Length), queue, queue2));
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x00048F30 File Offset: 0x00047130
		private void RecordInsertNew(int newNodeCount)
		{
			TextSpan span = SyntaxDiffer.GetSpan(this._newNodes, 0, newNodeCount);
			Queue<SyntaxNodeOrToken> queue = SyntaxDiffer.CopyFirst(this._newNodes, newNodeCount);
			SyntaxDiffer.RemoveFirst(this._newNodes, newNodeCount);
			int num = ((this._oldNodes.Count > 0) ? this._oldNodes.Peek().Position : this._oldSpan.End);
			this.RecordChange(new SyntaxDiffer.ChangeRecord(new TextChangeRange(new TextSpan(num, 0), span.Length), null, queue));
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x00048FB4 File Offset: 0x000471B4
		private void RecordChange(SyntaxDiffer.ChangeRecord change)
		{
			if (this._changes.Count > 0)
			{
				SyntaxDiffer.ChangeRecord changeRecord = this._changes[this._changes.Count - 1];
				if (changeRecord.Range.Span.End == change.Range.Span.Start)
				{
					this._changes[this._changes.Count - 1] = new SyntaxDiffer.ChangeRecord(new TextChangeRange(new TextSpan(changeRecord.Range.Span.Start, changeRecord.Range.Span.Length + change.Range.Span.Length), changeRecord.Range.NewLength + change.Range.NewLength), SyntaxDiffer.Combine(changeRecord.OldNodes, change.OldNodes), SyntaxDiffer.Combine(changeRecord.NewNodes, change.NewNodes));
					return;
				}
			}
			this._changes.Add(change);
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x000490C4 File Offset: 0x000472C4
		private void RecordChange(TextChangeRange textChangeRange, in SyntaxNodeOrToken removedNode, SyntaxNodeOrToken insertedNode)
		{
			if (this._changes.Count > 0)
			{
				SyntaxDiffer.ChangeRecord changeRecord = this._changes[this._changes.Count - 1];
				if (changeRecord.Range.Span.End == textChangeRange.Span.Start)
				{
					Queue<SyntaxNodeOrToken> oldNodes = changeRecord.OldNodes;
					if (oldNodes != null)
					{
						oldNodes.Enqueue(removedNode);
					}
					Queue<SyntaxNodeOrToken> newNodes = changeRecord.NewNodes;
					if (newNodes != null)
					{
						newNodes.Enqueue(insertedNode);
					}
					this._changes[this._changes.Count - 1] = new SyntaxDiffer.ChangeRecord(new TextChangeRange(new TextSpan(changeRecord.Range.Span.Start, changeRecord.Range.Span.Length + textChangeRange.Span.Length), changeRecord.Range.NewLength + textChangeRange.NewLength), changeRecord.OldNodes ?? SyntaxDiffer.<RecordChange>g__CreateQueue|30_0(removedNode), changeRecord.NewNodes ?? SyntaxDiffer.<RecordChange>g__CreateQueue|30_0(insertedNode));
					return;
				}
			}
			this._changes.Add(new SyntaxDiffer.ChangeRecord(textChangeRange, SyntaxDiffer.<RecordChange>g__CreateQueue|30_0(removedNode), SyntaxDiffer.<RecordChange>g__CreateQueue|30_0(insertedNode)));
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x00049208 File Offset: 0x00047408
		private static TextSpan GetSpan(Stack<SyntaxNodeOrToken> stack, int first, int length)
		{
			int num = -1;
			int num2 = -1;
			int num3 = 0;
			foreach (SyntaxNodeOrToken syntaxNodeOrToken in stack)
			{
				if (num3 == first)
				{
					num = syntaxNodeOrToken.Position;
				}
				if (num3 == first + length - 1)
				{
					num2 = syntaxNodeOrToken.EndPosition;
					break;
				}
				num3++;
			}
			return TextSpan.FromBounds(num, num2);
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x00049280 File Offset: 0x00047480
		private static TextSpan GetSpan(Queue<SyntaxNodeOrToken> queue, int first, int length)
		{
			int num = -1;
			int num2 = -1;
			int num3 = 0;
			foreach (SyntaxNodeOrToken syntaxNodeOrToken in queue)
			{
				if (num3 == first)
				{
					num = syntaxNodeOrToken.Position;
				}
				if (num3 == first + length - 1)
				{
					num2 = syntaxNodeOrToken.EndPosition;
					break;
				}
				num3++;
			}
			return TextSpan.FromBounds(num, num2);
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x000492F8 File Offset: 0x000474F8
		[NullableContext(2)]
		private static Queue<SyntaxNodeOrToken> Combine(Queue<SyntaxNodeOrToken> first, Queue<SyntaxNodeOrToken> next)
		{
			if (first == null || first.Count == 0)
			{
				return next;
			}
			if (next == null || next.Count == 0)
			{
				return first;
			}
			foreach (SyntaxNodeOrToken syntaxNodeOrToken in next)
			{
				first.Enqueue(syntaxNodeOrToken);
			}
			return first;
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x00049364 File Offset: 0x00047564
		[return: Nullable(2)]
		private static Queue<SyntaxNodeOrToken> CopyFirst(Stack<SyntaxNodeOrToken> stack, int n)
		{
			if (n == 0)
			{
				return null;
			}
			Queue<SyntaxNodeOrToken> queue = new Queue<SyntaxNodeOrToken>(n);
			int num = n;
			foreach (SyntaxNodeOrToken syntaxNodeOrToken in stack)
			{
				if (num == 0)
				{
					break;
				}
				queue.Enqueue(syntaxNodeOrToken);
				num--;
			}
			return queue;
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x000493CC File Offset: 0x000475CC
		private static SyntaxNodeOrToken[] ToArray(Stack<SyntaxNodeOrToken> stack, int n)
		{
			SyntaxNodeOrToken[] array = new SyntaxNodeOrToken[n];
			int num = n - 1;
			foreach (SyntaxNodeOrToken syntaxNodeOrToken in stack)
			{
				array[num] = syntaxNodeOrToken;
				num--;
				if (num < 0)
				{
					break;
				}
			}
			return array;
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x00049434 File Offset: 0x00047634
		private static void RemoveFirst(Stack<SyntaxNodeOrToken> stack, int count)
		{
			for (int i = 0; i < count; i++)
			{
				stack.Pop();
			}
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x00049454 File Offset: 0x00047654
		private List<SyntaxDiffer.ChangeRangeWithText> ReduceChanges(List<SyntaxDiffer.ChangeRecord> changeRecords)
		{
			List<SyntaxDiffer.ChangeRangeWithText> list = new List<SyntaxDiffer.ChangeRangeWithText>(changeRecords.Count);
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			foreach (SyntaxDiffer.ChangeRecord changeRecord in changeRecords)
			{
				if (changeRecord.Range.Span.Length > 0 && changeRecord.Range.NewLength > 0)
				{
					TextChangeRange range = changeRecord.Range;
					SyntaxDiffer.CopyText(changeRecord.OldNodes, stringBuilder);
					SyntaxDiffer.CopyText(changeRecord.NewNodes, stringBuilder2);
					int num;
					int num2;
					SyntaxDiffer.GetCommonEdgeLengths(stringBuilder, stringBuilder2, out num, out num2);
					if (num > 0 || num2 > 0)
					{
						range = new TextChangeRange(new TextSpan(range.Span.Start + num, range.Span.Length - (num + num2)), range.NewLength - (num + num2));
						if (num2 > 0)
						{
							stringBuilder2.Remove(stringBuilder2.Length - num2, num2);
						}
						if (num > 0)
						{
							stringBuilder2.Remove(0, num);
						}
					}
					if (range.Span.Length > 0 || range.NewLength > 0)
					{
						list.Add(new SyntaxDiffer.ChangeRangeWithText(range, this._computeNewText ? stringBuilder2.ToString() : null));
					}
				}
				else
				{
					list.Add(new SyntaxDiffer.ChangeRangeWithText(changeRecord.Range, this._computeNewText ? SyntaxDiffer.GetText(changeRecord.NewNodes) : null));
				}
			}
			return list;
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x000495FC File Offset: 0x000477FC
		private static void GetCommonEdgeLengths(StringBuilder oldText, StringBuilder newText, out int commonLeadingCount, out int commonTrailingCount)
		{
			int num = Math.Min(oldText.Length, newText.Length);
			commonLeadingCount = 0;
			while (commonLeadingCount < num && oldText.get_Chars(commonLeadingCount) == newText.get_Chars(commonLeadingCount))
			{
				commonLeadingCount++;
			}
			num -= commonLeadingCount;
			commonTrailingCount = 0;
			while (commonTrailingCount < num && oldText.get_Chars(oldText.Length - commonTrailingCount - 1) == newText.get_Chars(newText.Length - commonTrailingCount - 1))
			{
				commonTrailingCount++;
			}
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x00049678 File Offset: 0x00047878
		private static string GetText([Nullable(2)] Queue<SyntaxNodeOrToken> queue)
		{
			if (queue == null || queue.Count == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(SyntaxDiffer.GetSpan(queue, 0, queue.Count).Length);
			SyntaxDiffer.CopyText(queue, stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x000496C0 File Offset: 0x000478C0
		private static void CopyText([Nullable(2)] Queue<SyntaxNodeOrToken> queue, StringBuilder builder)
		{
			builder.Length = 0;
			if (queue != null && queue.Count > 0)
			{
				StringWriter stringWriter = new StringWriter(builder);
				foreach (SyntaxNodeOrToken syntaxNodeOrToken in queue)
				{
					syntaxNodeOrToken.WriteTo(stringWriter);
				}
				stringWriter.Flush();
			}
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x00049730 File Offset: 0x00047930
		[CompilerGenerated]
		internal static Queue<SyntaxNodeOrToken> <RecordChange>g__CreateQueue|30_0(SyntaxNodeOrToken nodeOrToken)
		{
			Queue<SyntaxNodeOrToken> queue = new Queue<SyntaxNodeOrToken>();
			queue.Enqueue(nodeOrToken);
			return queue;
		}

		// Token: 0x04000A03 RID: 2563
		private const int InitialStackSize = 8;

		// Token: 0x04000A04 RID: 2564
		private const int MaxSearchLength = 8;

		// Token: 0x04000A05 RID: 2565
		private readonly Stack<SyntaxNodeOrToken> _oldNodes = new Stack<SyntaxNodeOrToken>(8);

		// Token: 0x04000A06 RID: 2566
		private readonly Stack<SyntaxNodeOrToken> _newNodes = new Stack<SyntaxNodeOrToken>(8);

		// Token: 0x04000A07 RID: 2567
		private readonly List<SyntaxDiffer.ChangeRecord> _changes = new List<SyntaxDiffer.ChangeRecord>();

		// Token: 0x04000A08 RID: 2568
		private readonly TextSpan _oldSpan;

		// Token: 0x04000A09 RID: 2569
		private readonly bool _computeNewText;

		// Token: 0x04000A0A RID: 2570
		private readonly HashSet<GreenNode> _nodeSimilaritySet = new HashSet<GreenNode>();

		// Token: 0x04000A0B RID: 2571
		private readonly HashSet<string> _tokenTextSimilaritySet = new HashSet<string>();

		// Token: 0x020005FF RID: 1535
		[NullableContext(0)]
		private enum DiffOp
		{
			// Token: 0x04001773 RID: 6003
			None,
			// Token: 0x04001774 RID: 6004
			SkipBoth,
			// Token: 0x04001775 RID: 6005
			ReduceOld,
			// Token: 0x04001776 RID: 6006
			ReduceNew,
			// Token: 0x04001777 RID: 6007
			ReduceBoth,
			// Token: 0x04001778 RID: 6008
			InsertNew,
			// Token: 0x04001779 RID: 6009
			DeleteOld,
			// Token: 0x0400177A RID: 6010
			ReplaceOldWithNew
		}

		// Token: 0x02000600 RID: 1536
		[NullableContext(0)]
		private struct DiffAction
		{
			// Token: 0x0600373E RID: 14142 RVA: 0x0009AB21 File Offset: 0x00098D21
			public DiffAction(SyntaxDiffer.DiffOp operation, int count)
			{
				this.Operation = operation;
				this.Count = count;
			}

			// Token: 0x0400177B RID: 6011
			public readonly SyntaxDiffer.DiffOp Operation;

			// Token: 0x0400177C RID: 6012
			public readonly int Count;
		}

		// Token: 0x02000601 RID: 1537
		[NullableContext(2)]
		[Nullable(0)]
		private readonly struct ChangeRecord
		{
			// Token: 0x0600373F RID: 14143 RVA: 0x0009AB31 File Offset: 0x00098D31
			internal ChangeRecord(TextChangeRange range, Queue<SyntaxNodeOrToken> oldNodes, Queue<SyntaxNodeOrToken> newNodes)
			{
				this.Range = range;
				this.OldNodes = oldNodes;
				this.NewNodes = newNodes;
			}

			// Token: 0x0400177D RID: 6013
			public readonly TextChangeRange Range;

			// Token: 0x0400177E RID: 6014
			public readonly Queue<SyntaxNodeOrToken> OldNodes;

			// Token: 0x0400177F RID: 6015
			public readonly Queue<SyntaxNodeOrToken> NewNodes;
		}

		// Token: 0x02000602 RID: 1538
		[NullableContext(2)]
		[Nullable(0)]
		private readonly struct ChangeRangeWithText
		{
			// Token: 0x06003740 RID: 14144 RVA: 0x0009AB48 File Offset: 0x00098D48
			public ChangeRangeWithText(TextChangeRange range, string newText)
			{
				this.Range = range;
				this.NewText = newText;
			}

			// Token: 0x04001780 RID: 6016
			public readonly TextChangeRange Range;

			// Token: 0x04001781 RID: 6017
			public readonly string NewText;
		}
	}
}
