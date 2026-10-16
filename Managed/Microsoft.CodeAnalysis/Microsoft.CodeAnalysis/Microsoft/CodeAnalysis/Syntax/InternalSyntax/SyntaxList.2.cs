using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Syntax.InternalSyntax
{
	// Token: 0x020002B5 RID: 693
	[NullableContext(2)]
	[Nullable(0)]
	internal struct SyntaxList<[Nullable(0)] TNode> : IEquatable<SyntaxList<TNode>> where TNode : GreenNode
	{
		// Token: 0x06001D5D RID: 7517 RVA: 0x00055AB6 File Offset: 0x00053CB6
		internal SyntaxList(GreenNode node)
		{
			this._node = node;
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06001D5E RID: 7518 RVA: 0x00055ABF File Offset: 0x00053CBF
		internal GreenNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06001D5F RID: 7519 RVA: 0x00055AC7 File Offset: 0x00053CC7
		public int Count
		{
			get
			{
				if (this._node == null)
				{
					return 0;
				}
				if (!this._node.IsList)
				{
					return 1;
				}
				return this._node.SlotCount;
			}
		}

		// Token: 0x170007B7 RID: 1975
		public TNode this[int index]
		{
			get
			{
				if (this._node == null)
				{
					return default(TNode);
				}
				if (this._node.IsList)
				{
					return (TNode)((object)this._node.GetSlot(index));
				}
				if (index == 0)
				{
					return (TNode)((object)this._node);
				}
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x00055B42 File Offset: 0x00053D42
		[NullableContext(1)]
		internal TNode GetRequiredItem(int index)
		{
			return this[index];
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x00055B4C File Offset: 0x00053D4C
		internal GreenNode ItemUntyped(int index)
		{
			GreenNode node = this._node;
			if (node.IsList)
			{
				return node.GetSlot(index);
			}
			return node;
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x00055B71 File Offset: 0x00053D71
		public bool Any()
		{
			return this._node != null;
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x00055B7C File Offset: 0x00053D7C
		public bool Any(int kind)
		{
			SyntaxList<TNode>.Enumerator enumerator = this.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.RawKind == kind)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06001D65 RID: 7525 RVA: 0x00055BB4 File Offset: 0x00053DB4
		[Nullable(1)]
		internal TNode[] Nodes
		{
			[NullableContext(1)]
			get
			{
				TNode[] array = new TNode[this.Count];
				for (int i = 0; i < this.Count; i++)
				{
					array[i] = this.GetRequiredItem(i);
				}
				return array;
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06001D66 RID: 7526 RVA: 0x00055BF0 File Offset: 0x00053DF0
		public TNode Last
		{
			get
			{
				GreenNode node = this._node;
				if (node.IsList)
				{
					return (TNode)((object)node.GetSlot(node.SlotCount - 1));
				}
				return (TNode)((object)node);
			}
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x00055C26 File Offset: 0x00053E26
		[NullableContext(0)]
		public SyntaxList<TNode>.Enumerator GetEnumerator()
		{
			return new SyntaxList<TNode>.Enumerator(this);
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x00055C34 File Offset: 0x00053E34
		internal void CopyTo(int offset, [Nullable(new byte[] { 1, 0, 1 })] ArrayElement<GreenNode>[] array, int arrayOffset, int count)
		{
			for (int i = 0; i < count; i++)
			{
				array[arrayOffset + i].Value = this.GetRequiredItem(i + offset);
			}
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x00055C6A File Offset: 0x00053E6A
		public static bool operator ==([Nullable(new byte[] { 0, 1 })] SyntaxList<TNode> left, [Nullable(new byte[] { 0, 1 })] SyntaxList<TNode> right)
		{
			return left._node == right._node;
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x00055C7A File Offset: 0x00053E7A
		public static bool operator !=([Nullable(new byte[] { 0, 1 })] SyntaxList<TNode> left, [Nullable(new byte[] { 0, 1 })] SyntaxList<TNode> right)
		{
			return left._node != right._node;
		}

		// Token: 0x06001D6B RID: 7531 RVA: 0x00055C8D File Offset: 0x00053E8D
		public bool Equals([Nullable(new byte[] { 0, 1 })] SyntaxList<TNode> other)
		{
			return this._node == other._node;
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x00055C9D File Offset: 0x00053E9D
		public override bool Equals(object obj)
		{
			return obj is SyntaxList<TNode> && this.Equals((SyntaxList<TNode>)obj);
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x00055CB5 File Offset: 0x00053EB5
		public override int GetHashCode()
		{
			if (this._node == null)
			{
				return 0;
			}
			return this._node.GetHashCode();
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x00055CCC File Offset: 0x00053ECC
		[NullableContext(0)]
		[return: Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<TOther> AsSeparatedList<TOther>() where TOther : GreenNode
		{
			return new SeparatedSyntaxList<TOther>(this);
		}

		// Token: 0x06001D6F RID: 7535 RVA: 0x00055CDE File Offset: 0x00053EDE
		[NullableContext(1)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static implicit operator SyntaxList<TNode>(TNode node)
		{
			return new SyntaxList<TNode>(node);
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x00055CEB File Offset: 0x00053EEB
		[return: Nullable(new byte[] { 0, 1 })]
		public static implicit operator SyntaxList<TNode>([Nullable(new byte[] { 0, 1 })] SyntaxList<GreenNode> nodes)
		{
			return new SyntaxList<TNode>(nodes._node);
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x00055CF8 File Offset: 0x00053EF8
		[return: Nullable(new byte[] { 0, 1 })]
		public static implicit operator SyntaxList<GreenNode>([Nullable(new byte[] { 0, 1 })] SyntaxList<TNode> nodes)
		{
			return new SyntaxList<GreenNode>(nodes.Node);
		}

		// Token: 0x04000D6A RID: 3434
		private readonly GreenNode _node;

		// Token: 0x0200065D RID: 1629
		[NullableContext(1)]
		[Nullable(0)]
		internal struct Enumerator
		{
			// Token: 0x060038ED RID: 14573 RVA: 0x0009E136 File Offset: 0x0009C336
			internal Enumerator([Nullable(new byte[] { 0, 1 })] SyntaxList<TNode> list)
			{
				this._list = list;
				this._index = -1;
			}

			// Token: 0x060038EE RID: 14574 RVA: 0x0009E148 File Offset: 0x0009C348
			public bool MoveNext()
			{
				int num = this._index + 1;
				if (num < this._list.Count)
				{
					this._index = num;
					return true;
				}
				return false;
			}

			// Token: 0x17000FC6 RID: 4038
			// (get) Token: 0x060038EF RID: 14575 RVA: 0x0009E17C File Offset: 0x0009C37C
			public TNode Current
			{
				get
				{
					return this._list[this._index];
				}
			}

			// Token: 0x04001872 RID: 6258
			[Nullable(new byte[] { 0, 1 })]
			private readonly SyntaxList<TNode> _list;

			// Token: 0x04001873 RID: 6259
			private int _index;
		}
	}
}
