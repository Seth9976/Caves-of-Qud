using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000279 RID: 633
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct SyntaxList<[Nullable(0)] TNode> : IReadOnlyList<TNode>, IEnumerable<TNode>, IEnumerable, IReadOnlyCollection<TNode>, IEquatable<SyntaxList<TNode>> where TNode : SyntaxNode
	{
		// Token: 0x0600186E RID: 6254 RVA: 0x0004973E File Offset: 0x0004793E
		[NullableContext(2)]
		internal SyntaxList(SyntaxNode node)
		{
			this._node = node;
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x00049747 File Offset: 0x00047947
		[NullableContext(2)]
		public SyntaxList(TNode node)
		{
			this = new SyntaxList<TNode>(node);
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x00049755 File Offset: 0x00047955
		public SyntaxList([Nullable(new byte[] { 2, 1 })] IEnumerable<TNode> nodes)
		{
			this = new SyntaxList<TNode>(SyntaxList<TNode>.CreateNode(nodes));
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x00049764 File Offset: 0x00047964
		[NullableContext(2)]
		private static SyntaxNode CreateNode([Nullable(new byte[] { 2, 1 })] IEnumerable<TNode> nodes)
		{
			if (nodes == null)
			{
				return null;
			}
			ICollection<TNode> collection = nodes as ICollection<TNode>;
			SyntaxListBuilder<TNode> syntaxListBuilder = ((collection != null) ? new SyntaxListBuilder<TNode>(collection.Count) : SyntaxListBuilder<TNode>.Create());
			foreach (TNode tnode in nodes)
			{
				syntaxListBuilder.Add(tnode);
			}
			return syntaxListBuilder.ToList().Node;
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001872 RID: 6258 RVA: 0x000497E0 File Offset: 0x000479E0
		[Nullable(2)]
		internal SyntaxNode Node
		{
			[NullableContext(2)]
			get
			{
				return this._node;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001873 RID: 6259 RVA: 0x000497E8 File Offset: 0x000479E8
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

		// Token: 0x17000638 RID: 1592
		public TNode this[int index]
		{
			get
			{
				if (this._node != null)
				{
					if (this._node.IsList)
					{
						if (index < this._node.SlotCount)
						{
							return (TNode)((object)this._node.GetNodeSlot(index));
						}
					}
					else if (index == 0)
					{
						return (TNode)((object)this._node);
					}
				}
				throw new ArgumentOutOfRangeException("index");
			}
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x0004986B File Offset: 0x00047A6B
		[NullableContext(2)]
		internal SyntaxNode ItemInternal(int index)
		{
			SyntaxNode node = this._node;
			if (node != null && node.IsList)
			{
				return this._node.GetNodeSlot(index);
			}
			return this._node;
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001876 RID: 6262 RVA: 0x00049894 File Offset: 0x00047A94
		public TextSpan FullSpan
		{
			get
			{
				if (this.Count == 0)
				{
					return default(TextSpan);
				}
				return TextSpan.FromBounds(this[0].FullSpan.Start, this[this.Count - 1].FullSpan.End);
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001877 RID: 6263 RVA: 0x000498F4 File Offset: 0x00047AF4
		public TextSpan Span
		{
			get
			{
				if (this.Count == 0)
				{
					return default(TextSpan);
				}
				return TextSpan.FromBounds(this[0].Span.Start, this[this.Count - 1].Span.End);
			}
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x00049951 File Offset: 0x00047B51
		public override string ToString()
		{
			if (this._node == null)
			{
				return string.Empty;
			}
			return this._node.ToString();
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x0004996C File Offset: 0x00047B6C
		public string ToFullString()
		{
			if (this._node == null)
			{
				return string.Empty;
			}
			return this._node.ToFullString();
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x00049987 File Offset: 0x00047B87
		[return: Nullable(new byte[] { 0, 1 })]
		public SyntaxList<TNode> Add(TNode node)
		{
			return this.Insert(this.Count, node);
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x00049996 File Offset: 0x00047B96
		[return: Nullable(new byte[] { 0, 1 })]
		public SyntaxList<TNode> AddRange(IEnumerable<TNode> nodes)
		{
			return this.InsertRange(this.Count, nodes);
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x000499A5 File Offset: 0x00047BA5
		[return: Nullable(new byte[] { 0, 1 })]
		public SyntaxList<TNode> Insert(int index, TNode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			return this.InsertRange(index, new TNode[] { node });
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x000499D0 File Offset: 0x00047BD0
		[return: Nullable(new byte[] { 0, 1 })]
		public SyntaxList<TNode> InsertRange(int index, IEnumerable<TNode> nodes)
		{
			if (index < 0 || index > this.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (nodes == null)
			{
				throw new ArgumentNullException("nodes");
			}
			List<TNode> list = Enumerable.ToList<TNode>(this);
			list.InsertRange(index, nodes);
			if (list.Count == 0)
			{
				return this;
			}
			return SyntaxList<TNode>.CreateList(list);
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x00049A31 File Offset: 0x00047C31
		[return: Nullable(new byte[] { 0, 1 })]
		public SyntaxList<TNode> RemoveAt(int index)
		{
			if (index < 0 || index > this.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return this.Remove(this[index]);
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x00049A58 File Offset: 0x00047C58
		[return: Nullable(new byte[] { 0, 1 })]
		public SyntaxList<TNode> Remove(TNode node)
		{
			return SyntaxList<TNode>.CreateList(Enumerable.ToList<TNode>(Enumerable.Where<TNode>(this, (TNode x) => x != node)));
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x00049A98 File Offset: 0x00047C98
		[return: Nullable(new byte[] { 0, 1 })]
		public SyntaxList<TNode> Replace(TNode nodeInList, TNode newNode)
		{
			return this.ReplaceRange(nodeInList, new TNode[] { newNode });
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x00049AB0 File Offset: 0x00047CB0
		[return: Nullable(new byte[] { 0, 1 })]
		public SyntaxList<TNode> ReplaceRange(TNode nodeInList, IEnumerable<TNode> newNodes)
		{
			if (nodeInList == null)
			{
				throw new ArgumentNullException("nodeInList");
			}
			if (newNodes == null)
			{
				throw new ArgumentNullException("newNodes");
			}
			int num = this.IndexOf(nodeInList);
			if (num >= 0 && num < this.Count)
			{
				List<TNode> list = Enumerable.ToList<TNode>(this);
				list.RemoveAt(num);
				list.InsertRange(num, newNodes);
				return SyntaxList<TNode>.CreateList(list);
			}
			throw new ArgumentException("nodeInList");
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x00049B24 File Offset: 0x00047D24
		[return: Nullable(new byte[] { 0, 1 })]
		private static SyntaxList<TNode> CreateList(List<TNode> items)
		{
			if (items.Count == 0)
			{
				return default(SyntaxList<TNode>);
			}
			return new SyntaxList<TNode>(GreenNode.CreateList<TNode>(items, (TNode n) => n.Green).CreateRed());
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x00049B72 File Offset: 0x00047D72
		public TNode First()
		{
			return this[0];
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x00049B7C File Offset: 0x00047D7C
		[NullableContext(2)]
		public TNode FirstOrDefault()
		{
			if (this.Any())
			{
				return this[0];
			}
			return default(TNode);
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x00049BA2 File Offset: 0x00047DA2
		public TNode Last()
		{
			return this[this.Count - 1];
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x00049BB4 File Offset: 0x00047DB4
		[NullableContext(2)]
		public TNode LastOrDefault()
		{
			if (this.Any())
			{
				return this[this.Count - 1];
			}
			return default(TNode);
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x00049BE1 File Offset: 0x00047DE1
		public bool Any()
		{
			return this._node != null;
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x00049BEC File Offset: 0x00047DEC
		internal bool All(Func<TNode, bool> predicate)
		{
			foreach (TNode tnode in this)
			{
				if (!predicate.Invoke(tnode))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001889 RID: 6281 RVA: 0x00049C1F File Offset: 0x00047E1F
		private TNode[] Nodes
		{
			get
			{
				return Enumerable.ToArray<TNode>(this);
			}
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x00049C31 File Offset: 0x00047E31
		[NullableContext(0)]
		public SyntaxList<TNode>.Enumerator GetEnumerator()
		{
			return new SyntaxList<TNode>.Enumerator(this);
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x00049C3E File Offset: 0x00047E3E
		IEnumerator<TNode> IEnumerable<TNode>.GetEnumerator()
		{
			if (this.Any())
			{
				return new SyntaxList<TNode>.EnumeratorImpl(in this);
			}
			return SpecializedCollections.EmptyEnumerator<TNode>();
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x00049C54 File Offset: 0x00047E54
		IEnumerator IEnumerable.GetEnumerator()
		{
			if (this.Any())
			{
				return new SyntaxList<TNode>.EnumeratorImpl(in this);
			}
			return SpecializedCollections.EmptyEnumerator<TNode>();
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x00049C6A File Offset: 0x00047E6A
		public static bool operator ==([Nullable(new byte[] { 0, 1 })] SyntaxList<TNode> left, [Nullable(new byte[] { 0, 1 })] SyntaxList<TNode> right)
		{
			return left._node == right._node;
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x00049C7A File Offset: 0x00047E7A
		public static bool operator !=([Nullable(new byte[] { 0, 1 })] SyntaxList<TNode> left, [Nullable(new byte[] { 0, 1 })] SyntaxList<TNode> right)
		{
			return left._node != right._node;
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x00049C8D File Offset: 0x00047E8D
		public bool Equals([Nullable(new byte[] { 0, 1 })] SyntaxList<TNode> other)
		{
			return this._node == other._node;
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x00049C9D File Offset: 0x00047E9D
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is SyntaxList<TNode> && this.Equals((SyntaxList<TNode>)obj);
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x00049CB5 File Offset: 0x00047EB5
		public override int GetHashCode()
		{
			SyntaxNode node = this._node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x00049CC8 File Offset: 0x00047EC8
		[return: Nullable(new byte[] { 0, 1 })]
		public static implicit operator SyntaxList<TNode>([Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxNode> nodes)
		{
			return new SyntaxList<TNode>(nodes._node);
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x00049CD5 File Offset: 0x00047ED5
		[return: Nullable(new byte[] { 0, 1 })]
		public static implicit operator SyntaxList<SyntaxNode>([Nullable(new byte[] { 0, 1 })] SyntaxList<TNode> nodes)
		{
			return new SyntaxList<SyntaxNode>(nodes.Node);
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x00049CE4 File Offset: 0x00047EE4
		public int IndexOf(TNode node)
		{
			int num = 0;
			SyntaxList<TNode>.Enumerator enumerator = this.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (object.Equals(enumerator.Current, node))
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x00049D28 File Offset: 0x00047F28
		public int IndexOf(Func<TNode, bool> predicate)
		{
			int num = 0;
			foreach (TNode tnode in this)
			{
				if (predicate.Invoke(tnode))
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x00049D64 File Offset: 0x00047F64
		internal int IndexOf(int rawKind)
		{
			int num = 0;
			SyntaxList<TNode>.Enumerator enumerator = this.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.RawKind == rawKind)
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x00049DA0 File Offset: 0x00047FA0
		public int LastIndexOf(TNode node)
		{
			for (int i = this.Count - 1; i >= 0; i--)
			{
				if (object.Equals(this[i], node))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x00049DDC File Offset: 0x00047FDC
		public int LastIndexOf(Func<TNode, bool> predicate)
		{
			for (int i = this.Count - 1; i >= 0; i--)
			{
				if (predicate.Invoke(this[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x04000A0C RID: 2572
		[Nullable(2)]
		private readonly SyntaxNode _node;

		// Token: 0x02000604 RID: 1540
		[Nullable(0)]
		public struct Enumerator
		{
			// Token: 0x06003744 RID: 14148 RVA: 0x0009AB85 File Offset: 0x00098D85
			internal Enumerator([Nullable(new byte[] { 0, 1 })] SyntaxList<TNode> list)
			{
				this._list = list;
				this._index = -1;
			}

			// Token: 0x06003745 RID: 14149 RVA: 0x0009AB98 File Offset: 0x00098D98
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

			// Token: 0x17000F9D RID: 3997
			// (get) Token: 0x06003746 RID: 14150 RVA: 0x0009ABC6 File Offset: 0x00098DC6
			public TNode Current
			{
				get
				{
					return (TNode)((object)this._list.ItemInternal(this._index));
				}
			}

			// Token: 0x06003747 RID: 14151 RVA: 0x0009ABDE File Offset: 0x00098DDE
			public void Reset()
			{
				this._index = -1;
			}

			// Token: 0x06003748 RID: 14152 RVA: 0x0009ABE7 File Offset: 0x00098DE7
			[NullableContext(2)]
			public override bool Equals(object obj)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06003749 RID: 14153 RVA: 0x0009ABEE File Offset: 0x00098DEE
			public override int GetHashCode()
			{
				throw new NotSupportedException();
			}

			// Token: 0x04001784 RID: 6020
			[Nullable(new byte[] { 0, 1 })]
			private readonly SyntaxList<TNode> _list;

			// Token: 0x04001785 RID: 6021
			private int _index;
		}

		// Token: 0x02000605 RID: 1541
		[NullableContext(0)]
		private class EnumeratorImpl : IEnumerator<TNode>, IEnumerator, IDisposable
		{
			// Token: 0x0600374A RID: 14154 RVA: 0x0009ABF5 File Offset: 0x00098DF5
			internal EnumeratorImpl([Nullable(new byte[] { 0, 1 })] in SyntaxList<TNode> list)
			{
				this._e = new SyntaxList<TNode>.Enumerator(list);
			}

			// Token: 0x0600374B RID: 14155 RVA: 0x0009AC0E File Offset: 0x00098E0E
			public bool MoveNext()
			{
				return this._e.MoveNext();
			}

			// Token: 0x17000F9E RID: 3998
			// (get) Token: 0x0600374C RID: 14156 RVA: 0x0009AC1B File Offset: 0x00098E1B
			[Nullable(1)]
			public TNode Current
			{
				[NullableContext(1)]
				get
				{
					return this._e.Current;
				}
			}

			// Token: 0x0600374D RID: 14157 RVA: 0x0009AC28 File Offset: 0x00098E28
			void IDisposable.Dispose()
			{
			}

			// Token: 0x17000F9F RID: 3999
			// (get) Token: 0x0600374E RID: 14158 RVA: 0x0009AC2A File Offset: 0x00098E2A
			[Nullable(1)]
			object IEnumerator.Current
			{
				[NullableContext(1)]
				get
				{
					return this._e.Current;
				}
			}

			// Token: 0x0600374F RID: 14159 RVA: 0x0009AC3C File Offset: 0x00098E3C
			void IEnumerator.Reset()
			{
				this._e.Reset();
			}

			// Token: 0x04001786 RID: 6022
			private SyntaxList<TNode>.Enumerator _e;
		}
	}
}
