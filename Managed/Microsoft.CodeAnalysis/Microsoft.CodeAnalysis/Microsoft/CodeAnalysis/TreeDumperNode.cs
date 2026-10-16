using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200028E RID: 654
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class TreeDumperNode
	{
		// Token: 0x06001AF2 RID: 6898 RVA: 0x0004FDF1 File Offset: 0x0004DFF1
		public TreeDumperNode(string text, [Nullable(2)] object value, [Nullable(new byte[] { 2, 1 })] IEnumerable<TreeDumperNode> children)
		{
			this.Text = text;
			this.Value = value;
			this.Children = children ?? SpecializedCollections.EmptyEnumerable<TreeDumperNode>();
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x0004FE17 File Offset: 0x0004E017
		public TreeDumperNode(string text)
			: this(text, null, null)
		{
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06001AF4 RID: 6900 RVA: 0x0004FE22 File Offset: 0x0004E022
		[Nullable(2)]
		public object Value
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001AF5 RID: 6901 RVA: 0x0004FE2A File Offset: 0x0004E02A
		public string Text { get; }

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001AF6 RID: 6902 RVA: 0x0004FE32 File Offset: 0x0004E032
		public IEnumerable<TreeDumperNode> Children { get; }

		// Token: 0x170006C6 RID: 1734
		[Nullable(2)]
		public TreeDumperNode this[string child]
		{
			[return: Nullable(2)]
			get
			{
				return Enumerable.FirstOrDefault<TreeDumperNode>(this.Children, (TreeDumperNode c) => c.Text == child);
			}
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x0004FE6D File Offset: 0x0004E06D
		[return: Nullable(new byte[] { 1, 0, 2, 1 })]
		public IEnumerable<KeyValuePair<TreeDumperNode, TreeDumperNode>> PreorderTraversal()
		{
			Stack<KeyValuePair<TreeDumperNode, TreeDumperNode>> stack = new Stack<KeyValuePair<TreeDumperNode, TreeDumperNode>>();
			stack.Push(new KeyValuePair<TreeDumperNode, TreeDumperNode>(null, this));
			while (stack.Count != 0)
			{
				KeyValuePair<TreeDumperNode, TreeDumperNode> currentEdge = stack.Pop();
				yield return currentEdge;
				TreeDumperNode value = currentEdge.Value;
				foreach (TreeDumperNode treeDumperNode in Enumerable.Reverse<TreeDumperNode>(Enumerable.Where<TreeDumperNode>(value.Children, (TreeDumperNode x) => x != null)))
				{
					stack.Push(new KeyValuePair<TreeDumperNode, TreeDumperNode>(value, treeDumperNode));
				}
				currentEdge = default(KeyValuePair<TreeDumperNode, TreeDumperNode>);
			}
			yield break;
		}
	}
}
