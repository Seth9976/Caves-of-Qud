using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000276 RID: 630
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct SeparatedSyntaxList<[Nullable(0)] TNode> : IEquatable<SeparatedSyntaxList<TNode>>, IReadOnlyList<TNode>, IEnumerable<TNode>, IEnumerable, IReadOnlyCollection<TNode> where TNode : SyntaxNode
	{
		// Token: 0x06001810 RID: 6160 RVA: 0x00047974 File Offset: 0x00045B74
		internal SeparatedSyntaxList(SyntaxNodeOrTokenList list)
		{
			this = default(SeparatedSyntaxList<TNode>);
			int count = list.Count;
			this._count = count + 1 >> 1;
			this._separatorCount = count >> 1;
			this._list = list;
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x000479AC File Offset: 0x00045BAC
		[Conditional("DEBUG")]
		private static void Validate(SyntaxNodeOrTokenList list)
		{
			for (int i = 0; i < list.Count; i++)
			{
				SyntaxNodeOrToken syntaxNodeOrToken = list[i];
				int num = i & 1;
			}
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x000479D8 File Offset: 0x00045BD8
		internal SeparatedSyntaxList(SyntaxNode node, int index)
		{
			this = new SeparatedSyntaxList<TNode>(new SyntaxNodeOrTokenList(node, index));
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001813 RID: 6163 RVA: 0x000479E7 File Offset: 0x00045BE7
		[Nullable(2)]
		internal SyntaxNode Node
		{
			[NullableContext(2)]
			get
			{
				return this._list.Node;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001814 RID: 6164 RVA: 0x000479F4 File Offset: 0x00045BF4
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001815 RID: 6165 RVA: 0x000479FC File Offset: 0x00045BFC
		public int SeparatorCount
		{
			get
			{
				return this._separatorCount;
			}
		}

		// Token: 0x1700062D RID: 1581
		public TNode this[int index]
		{
			get
			{
				SyntaxNode node = this._list.Node;
				if (node != null)
				{
					if (!node.IsList)
					{
						if (index == 0)
						{
							return (TNode)((object)node);
						}
					}
					else if (index < this._count)
					{
						return (TNode)((object)node.GetRequiredNodeSlot(index << 1));
					}
				}
				throw new ArgumentOutOfRangeException("index");
			}
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x00047A54 File Offset: 0x00045C54
		public SyntaxToken GetSeparator(int index)
		{
			SyntaxNode node = this._list.Node;
			if (node != null && index < this._separatorCount)
			{
				index = (index << 1) + 1;
				GreenNode requiredSlot = node.Green.GetRequiredSlot(index);
				return new SyntaxToken(node.Parent, requiredSlot, node.GetChildPosition(index), this._list.index + index);
			}
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x00047AB8 File Offset: 0x00045CB8
		public IEnumerable<SyntaxToken> GetSeparators()
		{
			return Enumerable.Select<SyntaxNodeOrToken, SyntaxToken>(Enumerable.Where<SyntaxNodeOrToken>(this._list, (SyntaxNodeOrToken n) => n.IsToken), (SyntaxNodeOrToken n) => n.AsToken());
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001819 RID: 6169 RVA: 0x00047B18 File Offset: 0x00045D18
		public TextSpan FullSpan
		{
			get
			{
				return this._list.FullSpan;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x0600181A RID: 6170 RVA: 0x00047B25 File Offset: 0x00045D25
		public TextSpan Span
		{
			get
			{
				return this._list.Span;
			}
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x00047B32 File Offset: 0x00045D32
		public override string ToString()
		{
			return this._list.ToString();
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x00047B45 File Offset: 0x00045D45
		public string ToFullString()
		{
			return this._list.ToFullString();
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x00047B52 File Offset: 0x00045D52
		public TNode First()
		{
			return this[0];
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x00047B5C File Offset: 0x00045D5C
		[NullableContext(2)]
		public TNode FirstOrDefault()
		{
			if (this.Any())
			{
				return this[0];
			}
			return default(TNode);
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x00047B82 File Offset: 0x00045D82
		public TNode Last()
		{
			return this[this.Count - 1];
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x00047B94 File Offset: 0x00045D94
		[NullableContext(2)]
		public TNode LastOrDefault()
		{
			if (this.Any())
			{
				return this[this.Count - 1];
			}
			return default(TNode);
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x00047BC1 File Offset: 0x00045DC1
		public bool Contains(TNode node)
		{
			return this.IndexOf(node) >= 0;
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x00047BD0 File Offset: 0x00045DD0
		public int IndexOf(TNode node)
		{
			int i = 0;
			int count = this.Count;
			while (i < count)
			{
				if (object.Equals(this[i], node))
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x00047C0C File Offset: 0x00045E0C
		public int IndexOf(Func<TNode, bool> predicate)
		{
			int i = 0;
			int count = this.Count;
			while (i < count)
			{
				if (predicate.Invoke(this[i]))
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x00047C40 File Offset: 0x00045E40
		internal int IndexOf(int rawKind)
		{
			int i = 0;
			int count = this.Count;
			while (i < count)
			{
				if (this[i].RawKind == rawKind)
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x00047C78 File Offset: 0x00045E78
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

		// Token: 0x06001826 RID: 6182 RVA: 0x00047CB4 File Offset: 0x00045EB4
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

		// Token: 0x06001827 RID: 6183 RVA: 0x00047CE6 File Offset: 0x00045EE6
		public bool Any()
		{
			return this._list.Any();
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x00047CF4 File Offset: 0x00045EF4
		internal bool Any(Func<TNode, bool> predicate)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (predicate.Invoke(this[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x00047D24 File Offset: 0x00045F24
		public SyntaxNodeOrTokenList GetWithSeparators()
		{
			return this._list;
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x00047D2C File Offset: 0x00045F2C
		public static bool operator ==([Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<TNode> left, [Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<TNode> right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x00047D36 File Offset: 0x00045F36
		public static bool operator !=([Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<TNode> left, [Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<TNode> right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x00047D43 File Offset: 0x00045F43
		public bool Equals([Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<TNode> other)
		{
			return this._list == other._list;
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x00047D58 File Offset: 0x00045F58
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is SeparatedSyntaxList<TNode>)
			{
				SeparatedSyntaxList<TNode> separatedSyntaxList = (SeparatedSyntaxList<TNode>)obj;
				return this.Equals(separatedSyntaxList);
			}
			return false;
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x00047D7D File Offset: 0x00045F7D
		public override int GetHashCode()
		{
			return this._list.GetHashCode();
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x00047D90 File Offset: 0x00045F90
		[return: Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<TNode> Add(TNode node)
		{
			return this.Insert(this.Count, node);
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x00047D9F File Offset: 0x00045F9F
		[return: Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<TNode> AddRange(IEnumerable<TNode> nodes)
		{
			return this.InsertRange(this.Count, nodes);
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x00047DAE File Offset: 0x00045FAE
		[return: Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<TNode> Insert(int index, TNode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			return this.InsertRange(index, new TNode[] { node });
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x00047DD8 File Offset: 0x00045FD8
		[return: Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<TNode> InsertRange(int index, IEnumerable<TNode> nodes)
		{
			if (nodes == null)
			{
				throw new ArgumentNullException("nodes");
			}
			if (index < 0 || index > this.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			SyntaxNodeOrTokenList withSeparators = this.GetWithSeparators();
			int num = ((index < this.Count) ? withSeparators.IndexOf(this[index]) : withSeparators.Count);
			if (num > 0 && num < withSeparators.Count)
			{
				SyntaxNodeOrToken syntaxNodeOrToken = withSeparators[num - 1];
				if (syntaxNodeOrToken.IsToken)
				{
					SyntaxToken syntaxToken = syntaxNodeOrToken.AsToken();
					if (!SeparatedSyntaxList<TNode>.KeepSeparatorWithPreviousNode(in syntaxToken))
					{
						num--;
					}
				}
			}
			List<SyntaxNodeOrToken> list = new List<SyntaxNodeOrToken>();
			foreach (TNode tnode in nodes)
			{
				if (tnode != null)
				{
					if (list.Count > 0 || (num > 0 && withSeparators[num - 1].IsNode))
					{
						list.Add(tnode.Green.CreateSeparator<TNode>(tnode));
					}
					list.Add(tnode);
				}
			}
			if (num < withSeparators.Count && withSeparators[num].IsNode)
			{
				SyntaxNode syntaxNode = withSeparators[num].AsNode();
				list.Add(syntaxNode.Green.CreateSeparator<TNode>(syntaxNode));
			}
			return new SeparatedSyntaxList<TNode>(withSeparators.InsertRange(num, list));
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x00047F6C File Offset: 0x0004616C
		private static bool KeepSeparatorWithPreviousNode(in SyntaxToken separator)
		{
			foreach (SyntaxTrivia syntaxTrivia in separator.TrailingTrivia)
			{
				if (syntaxTrivia.UnderlyingNode.IsTriviaWithEndOfLine())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x00047FAC File Offset: 0x000461AC
		[return: Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<TNode> RemoveAt(int index)
		{
			if (index < 0 || index > this.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return this.Remove(this[index]);
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x00047FD4 File Offset: 0x000461D4
		[return: Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<TNode> Remove(TNode node)
		{
			SyntaxNodeOrTokenList syntaxNodeOrTokenList = this.GetWithSeparators();
			int num = syntaxNodeOrTokenList.IndexOf(node);
			if (num >= 0 && num <= syntaxNodeOrTokenList.Count)
			{
				syntaxNodeOrTokenList = syntaxNodeOrTokenList.RemoveAt(num);
				if (num < syntaxNodeOrTokenList.Count && syntaxNodeOrTokenList[num].IsToken)
				{
					syntaxNodeOrTokenList = syntaxNodeOrTokenList.RemoveAt(num);
				}
				else if (num > 0 && syntaxNodeOrTokenList[num - 1].IsToken)
				{
					syntaxNodeOrTokenList = syntaxNodeOrTokenList.RemoveAt(num - 1);
				}
				return new SeparatedSyntaxList<TNode>(syntaxNodeOrTokenList);
			}
			return this;
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x0004806C File Offset: 0x0004626C
		[return: Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<TNode> Replace(TNode nodeInList, TNode newNode)
		{
			if (newNode == null)
			{
				throw new ArgumentNullException("newNode");
			}
			int num = this.IndexOf(nodeInList);
			if (num >= 0 && num < this.Count)
			{
				return new SeparatedSyntaxList<TNode>(this.GetWithSeparators().Replace(nodeInList, newNode));
			}
			throw new ArgumentOutOfRangeException("nodeInList");
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x000480D8 File Offset: 0x000462D8
		[return: Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<TNode> ReplaceRange(TNode nodeInList, IEnumerable<TNode> newNodes)
		{
			if (newNodes == null)
			{
				throw new ArgumentNullException("newNodes");
			}
			int num = this.IndexOf(nodeInList);
			if (num < 0 || num >= this.Count)
			{
				throw new ArgumentOutOfRangeException("nodeInList");
			}
			List<TNode> list = Enumerable.ToList<TNode>(newNodes);
			if (list.Count == 0)
			{
				return this.Remove(nodeInList);
			}
			SeparatedSyntaxList<TNode> separatedSyntaxList = this.Replace(nodeInList, list[0]);
			if (list.Count > 1)
			{
				list.RemoveAt(0);
				return separatedSyntaxList.InsertRange(num + 1, list);
			}
			return separatedSyntaxList;
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x00048158 File Offset: 0x00046358
		[return: Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<TNode> ReplaceSeparator(SyntaxToken separatorToken, SyntaxToken newSeparator)
		{
			SyntaxNodeOrTokenList withSeparators = this.GetWithSeparators();
			int num = withSeparators.IndexOf(separatorToken);
			if (num < 0)
			{
				throw new ArgumentException("separatorToken");
			}
			if (newSeparator.RawKind != withSeparators[num].RawKind || newSeparator.Language != withSeparators[num].Language)
			{
				throw new ArgumentException("newSeparator");
			}
			return new SeparatedSyntaxList<TNode>(withSeparators.Replace(separatorToken, newSeparator));
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001839 RID: 6201 RVA: 0x000481E3 File Offset: 0x000463E3
		private TNode[] Nodes
		{
			get
			{
				return Enumerable.ToArray<TNode>(this);
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x0600183A RID: 6202 RVA: 0x000481F5 File Offset: 0x000463F5
		private SyntaxNodeOrToken[] NodesWithSeparators
		{
			get
			{
				return Enumerable.ToArray<SyntaxNodeOrToken>(this._list);
			}
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x00048207 File Offset: 0x00046407
		[NullableContext(0)]
		public SeparatedSyntaxList<TNode>.Enumerator GetEnumerator()
		{
			return new SeparatedSyntaxList<TNode>.Enumerator(in this);
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x0004820F File Offset: 0x0004640F
		IEnumerator<TNode> IEnumerable<TNode>.GetEnumerator()
		{
			if (this.Any())
			{
				return new SeparatedSyntaxList<TNode>.EnumeratorImpl(in this);
			}
			return SpecializedCollections.EmptyEnumerator<TNode>();
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x00048225 File Offset: 0x00046425
		IEnumerator IEnumerable.GetEnumerator()
		{
			if (this.Any())
			{
				return new SeparatedSyntaxList<TNode>.EnumeratorImpl(in this);
			}
			return SpecializedCollections.EmptyEnumerator<TNode>();
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x0004823B File Offset: 0x0004643B
		[return: Nullable(new byte[] { 0, 1 })]
		public static implicit operator SeparatedSyntaxList<SyntaxNode>([Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<TNode> nodes)
		{
			return new SeparatedSyntaxList<SyntaxNode>(nodes._list);
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x00048248 File Offset: 0x00046448
		[return: Nullable(new byte[] { 0, 1 })]
		public static implicit operator SeparatedSyntaxList<TNode>([Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<SyntaxNode> nodes)
		{
			return new SeparatedSyntaxList<TNode>(nodes._list);
		}

		// Token: 0x040009FB RID: 2555
		private readonly SyntaxNodeOrTokenList _list;

		// Token: 0x040009FC RID: 2556
		private readonly int _count;

		// Token: 0x040009FD RID: 2557
		private readonly int _separatorCount;

		// Token: 0x020005FB RID: 1531
		[Nullable(0)]
		public struct Enumerator
		{
			// Token: 0x0600372B RID: 14123 RVA: 0x0009AA20 File Offset: 0x00098C20
			internal Enumerator([Nullable(new byte[] { 0, 1 })] in SeparatedSyntaxList<TNode> list)
			{
				this._list = list;
				this._index = -1;
			}

			// Token: 0x0600372C RID: 14124 RVA: 0x0009AA38 File Offset: 0x00098C38
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

			// Token: 0x17000F9A RID: 3994
			// (get) Token: 0x0600372D RID: 14125 RVA: 0x0009AA66 File Offset: 0x00098C66
			public TNode Current
			{
				get
				{
					return this._list[this._index];
				}
			}

			// Token: 0x0600372E RID: 14126 RVA: 0x0009AA79 File Offset: 0x00098C79
			public void Reset()
			{
				this._index = -1;
			}

			// Token: 0x0600372F RID: 14127 RVA: 0x0009AA82 File Offset: 0x00098C82
			[NullableContext(2)]
			public override bool Equals(object obj)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06003730 RID: 14128 RVA: 0x0009AA89 File Offset: 0x00098C89
			public override int GetHashCode()
			{
				throw new NotSupportedException();
			}

			// Token: 0x0400176B RID: 5995
			[Nullable(new byte[] { 0, 1 })]
			private readonly SeparatedSyntaxList<TNode> _list;

			// Token: 0x0400176C RID: 5996
			private int _index;
		}

		// Token: 0x020005FC RID: 1532
		[NullableContext(0)]
		private class EnumeratorImpl : IEnumerator<TNode>, IEnumerator, IDisposable
		{
			// Token: 0x06003731 RID: 14129 RVA: 0x0009AA90 File Offset: 0x00098C90
			internal EnumeratorImpl([Nullable(new byte[] { 0, 1 })] in SeparatedSyntaxList<TNode> list)
			{
				this._e = new SeparatedSyntaxList<TNode>.Enumerator(in list);
			}

			// Token: 0x17000F9B RID: 3995
			// (get) Token: 0x06003732 RID: 14130 RVA: 0x0009AAA4 File Offset: 0x00098CA4
			[Nullable(1)]
			public TNode Current
			{
				[NullableContext(1)]
				get
				{
					return this._e.Current;
				}
			}

			// Token: 0x17000F9C RID: 3996
			// (get) Token: 0x06003733 RID: 14131 RVA: 0x0009AAB1 File Offset: 0x00098CB1
			[Nullable(1)]
			object IEnumerator.Current
			{
				[NullableContext(1)]
				get
				{
					return this._e.Current;
				}
			}

			// Token: 0x06003734 RID: 14132 RVA: 0x0009AAC3 File Offset: 0x00098CC3
			public void Dispose()
			{
			}

			// Token: 0x06003735 RID: 14133 RVA: 0x0009AAC5 File Offset: 0x00098CC5
			public bool MoveNext()
			{
				return this._e.MoveNext();
			}

			// Token: 0x06003736 RID: 14134 RVA: 0x0009AAD2 File Offset: 0x00098CD2
			public void Reset()
			{
				this._e.Reset();
			}

			// Token: 0x0400176D RID: 5997
			private SeparatedSyntaxList<TNode>.Enumerator _e;
		}
	}
}
