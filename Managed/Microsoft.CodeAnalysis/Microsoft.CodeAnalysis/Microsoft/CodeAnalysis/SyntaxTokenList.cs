using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis.Syntax;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000283 RID: 643
	[NullableContext(1)]
	[Nullable(0)]
	[StructLayout(3)]
	public readonly struct SyntaxTokenList : IEquatable<SyntaxTokenList>, IReadOnlyList<SyntaxToken>, IEnumerable<SyntaxToken>, IEnumerable, IReadOnlyCollection<SyntaxToken>
	{
		// Token: 0x06001A25 RID: 6693 RVA: 0x0004DF97 File Offset: 0x0004C197
		[NullableContext(2)]
		internal SyntaxTokenList(SyntaxNode parent, GreenNode tokenOrList, int position, int index)
		{
			this._parent = parent;
			this.Node = tokenOrList;
			this.Position = position;
			this._index = index;
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x0004DFB6 File Offset: 0x0004C1B6
		public SyntaxTokenList(SyntaxToken token)
		{
			this._parent = token.Parent;
			this.Node = token.Node;
			this.Position = token.Position;
			this._index = 0;
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x0004DFE6 File Offset: 0x0004C1E6
		public SyntaxTokenList(params SyntaxToken[] tokens)
		{
			this = new SyntaxTokenList(null, SyntaxTokenList.CreateNode(tokens), 0, 0);
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x0004DFF7 File Offset: 0x0004C1F7
		public SyntaxTokenList(IEnumerable<SyntaxToken> tokens)
		{
			this = new SyntaxTokenList(null, SyntaxTokenList.CreateNode(tokens), 0, 0);
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x0004E008 File Offset: 0x0004C208
		[return: Nullable(2)]
		private static GreenNode CreateNode(SyntaxToken[] tokens)
		{
			if (tokens == null)
			{
				return null;
			}
			SyntaxTokenListBuilder syntaxTokenListBuilder = new SyntaxTokenListBuilder(tokens.Length);
			for (int i = 0; i < tokens.Length; i++)
			{
				GreenNode node = tokens[i].Node;
				syntaxTokenListBuilder.Add(node);
			}
			return syntaxTokenListBuilder.ToList().Node;
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x0004E054 File Offset: 0x0004C254
		[return: Nullable(2)]
		private static GreenNode CreateNode(IEnumerable<SyntaxToken> tokens)
		{
			if (tokens == null)
			{
				return null;
			}
			SyntaxTokenListBuilder syntaxTokenListBuilder = SyntaxTokenListBuilder.Create();
			foreach (SyntaxToken syntaxToken in tokens)
			{
				syntaxTokenListBuilder.Add(syntaxToken.Node);
			}
			return syntaxTokenListBuilder.ToList().Node;
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001A2B RID: 6699 RVA: 0x0004E0BC File Offset: 0x0004C2BC
		[Nullable(2)]
		internal GreenNode Node
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001A2C RID: 6700 RVA: 0x0004E0C4 File Offset: 0x0004C2C4
		internal int Position { get; }

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001A2D RID: 6701 RVA: 0x0004E0CC File Offset: 0x0004C2CC
		public int Count
		{
			get
			{
				if (this.Node == null)
				{
					return 0;
				}
				if (!this.Node.IsList)
				{
					return 1;
				}
				return this.Node.SlotCount;
			}
		}

		// Token: 0x17000699 RID: 1689
		public SyntaxToken this[int index]
		{
			get
			{
				if (this.Node != null)
				{
					if (this.Node.IsList)
					{
						if (index < this.Node.SlotCount)
						{
							return new SyntaxToken(this._parent, this.Node.GetSlot(index), this.Position + this.Node.GetSlotOffset(index), this._index + index);
						}
					}
					else if (index == 0)
					{
						return new SyntaxToken(this._parent, this.Node, this.Position, this._index);
					}
				}
				throw new ArgumentOutOfRangeException("index");
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001A2F RID: 6703 RVA: 0x0004E184 File Offset: 0x0004C384
		public TextSpan FullSpan
		{
			get
			{
				if (this.Node == null)
				{
					return default(TextSpan);
				}
				return new TextSpan(this.Position, this.Node.FullWidth);
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001A30 RID: 6704 RVA: 0x0004E1BC File Offset: 0x0004C3BC
		public TextSpan Span
		{
			get
			{
				if (this.Node == null)
				{
					return default(TextSpan);
				}
				return TextSpan.FromBounds(this.Position + this.Node.GetLeadingTriviaWidth(), this.Position + this.Node.FullWidth - this.Node.GetTrailingTriviaWidth());
			}
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x0004E210 File Offset: 0x0004C410
		public override string ToString()
		{
			if (this.Node == null)
			{
				return string.Empty;
			}
			return this.Node.ToString();
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x0004E22B File Offset: 0x0004C42B
		public string ToFullString()
		{
			if (this.Node == null)
			{
				return string.Empty;
			}
			return this.Node.ToFullString();
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x0004E246 File Offset: 0x0004C446
		public SyntaxToken First()
		{
			if (this.Any())
			{
				return this[0];
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x0004E25D File Offset: 0x0004C45D
		public SyntaxToken Last()
		{
			if (this.Any())
			{
				return this[this.Count - 1];
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x0004E27B File Offset: 0x0004C47B
		public bool Any()
		{
			return this.Node != null;
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x0004E286 File Offset: 0x0004C486
		public SyntaxTokenList.Reversed Reverse()
		{
			return new SyntaxTokenList.Reversed(this);
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x0004E294 File Offset: 0x0004C494
		internal void CopyTo(int offset, [Nullable(new byte[] { 1, 2 })] GreenNode[] array, int arrayOffset, int count)
		{
			for (int i = 0; i < count; i++)
			{
				array[arrayOffset + i] = this.GetGreenNodeAt(offset + i);
			}
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x0004E2BC File Offset: 0x0004C4BC
		[NullableContext(2)]
		private GreenNode GetGreenNodeAt(int i)
		{
			return SyntaxTokenList.GetGreenNodeAt(this.Node, i);
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x0004E2CA File Offset: 0x0004C4CA
		[return: Nullable(2)]
		private static GreenNode GetGreenNodeAt(GreenNode node, int i)
		{
			if (!node.IsList)
			{
				return node;
			}
			return node.GetSlot(i);
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x0004E2E0 File Offset: 0x0004C4E0
		public int IndexOf(SyntaxToken tokenInList)
		{
			int i = 0;
			int count = this.Count;
			while (i < count)
			{
				if (this[i] == tokenInList)
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x0004E314 File Offset: 0x0004C514
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

		// Token: 0x06001A3C RID: 6716 RVA: 0x0004E349 File Offset: 0x0004C549
		public SyntaxTokenList Add(SyntaxToken token)
		{
			return this.Insert(this.Count, token);
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x0004E358 File Offset: 0x0004C558
		public SyntaxTokenList AddRange(IEnumerable<SyntaxToken> tokens)
		{
			return this.InsertRange(this.Count, tokens);
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x0004E368 File Offset: 0x0004C568
		public SyntaxTokenList Insert(int index, SyntaxToken token)
		{
			if (token == default(SyntaxToken))
			{
				throw new ArgumentOutOfRangeException("token");
			}
			return this.InsertRange(index, new SyntaxToken[] { token });
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x0004E3A8 File Offset: 0x0004C5A8
		public SyntaxTokenList InsertRange(int index, IEnumerable<SyntaxToken> tokens)
		{
			if (index < 0 || index > this.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (tokens == null)
			{
				throw new ArgumentNullException("tokens");
			}
			if (Enumerable.ToList<SyntaxToken>(tokens).Count == 0)
			{
				return this;
			}
			List<SyntaxToken> list = Enumerable.ToList<SyntaxToken>(this);
			list.InsertRange(index, tokens);
			if (list.Count == 0)
			{
				return this;
			}
			return new SyntaxTokenList(null, GreenNode.CreateList<SyntaxToken>(list, (SyntaxToken n) => n.RequiredNode), 0, 0);
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x0004E444 File Offset: 0x0004C644
		public SyntaxTokenList RemoveAt(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			List<SyntaxToken> list = Enumerable.ToList<SyntaxToken>(this);
			list.RemoveAt(index);
			return new SyntaxTokenList(null, GreenNode.CreateList<SyntaxToken>(list, (SyntaxToken n) => n.RequiredNode), 0, 0);
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x0004E4B0 File Offset: 0x0004C6B0
		public SyntaxTokenList Remove(SyntaxToken tokenInList)
		{
			int num = this.IndexOf(tokenInList);
			if (num >= 0 && num <= this.Count)
			{
				return this.RemoveAt(num);
			}
			return this;
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x0004E4E0 File Offset: 0x0004C6E0
		public SyntaxTokenList Replace(SyntaxToken tokenInList, SyntaxToken newToken)
		{
			if (newToken == default(SyntaxToken))
			{
				throw new ArgumentOutOfRangeException("newToken");
			}
			return this.ReplaceRange(tokenInList, new SyntaxToken[] { newToken });
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x0004E520 File Offset: 0x0004C720
		public SyntaxTokenList ReplaceRange(SyntaxToken tokenInList, IEnumerable<SyntaxToken> newTokens)
		{
			int num = this.IndexOf(tokenInList);
			if (num >= 0 && num <= this.Count)
			{
				List<SyntaxToken> list = Enumerable.ToList<SyntaxToken>(this);
				list.RemoveAt(num);
				list.InsertRange(num, newTokens);
				return new SyntaxTokenList(null, GreenNode.CreateList<SyntaxToken>(list, (SyntaxToken n) => n.RequiredNode), 0, 0);
			}
			throw new ArgumentOutOfRangeException("tokenInList");
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001A44 RID: 6724 RVA: 0x0004E59A File Offset: 0x0004C79A
		private SyntaxToken[] Nodes
		{
			get
			{
				return Enumerable.ToArray<SyntaxToken>(this);
			}
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x0004E5AC File Offset: 0x0004C7AC
		public SyntaxTokenList.Enumerator GetEnumerator()
		{
			return new SyntaxTokenList.Enumerator(in this);
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x0004E5B4 File Offset: 0x0004C7B4
		IEnumerator<SyntaxToken> IEnumerable<SyntaxToken>.GetEnumerator()
		{
			if (this.Node == null)
			{
				return SpecializedCollections.EmptyEnumerator<SyntaxToken>();
			}
			return new SyntaxTokenList.EnumeratorImpl(in this);
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x0004E5CA File Offset: 0x0004C7CA
		IEnumerator IEnumerable.GetEnumerator()
		{
			if (this.Node == null)
			{
				return SpecializedCollections.EmptyEnumerator<SyntaxToken>();
			}
			return new SyntaxTokenList.EnumeratorImpl(in this);
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x0004E5E0 File Offset: 0x0004C7E0
		public static bool operator ==(SyntaxTokenList left, SyntaxTokenList right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x0004E5EA File Offset: 0x0004C7EA
		public static bool operator !=(SyntaxTokenList left, SyntaxTokenList right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x0004E5F7 File Offset: 0x0004C7F7
		public bool Equals(SyntaxTokenList other)
		{
			return this.Node == other.Node && this._parent == other._parent && this._index == other._index;
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x0004E628 File Offset: 0x0004C828
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is SyntaxTokenList)
			{
				SyntaxTokenList syntaxTokenList = (SyntaxTokenList)obj;
				return this.Equals(syntaxTokenList);
			}
			return false;
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x0004E64D File Offset: 0x0004C84D
		public override int GetHashCode()
		{
			return Hash.Combine<GreenNode>(this.Node, this._index);
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x0004E660 File Offset: 0x0004C860
		public static SyntaxTokenList Create(SyntaxToken token)
		{
			return new SyntaxTokenList(token);
		}

		// Token: 0x04000A32 RID: 2610
		[Nullable(2)]
		private readonly SyntaxNode _parent;

		// Token: 0x04000A33 RID: 2611
		private readonly int _index;

		// Token: 0x02000628 RID: 1576
		[NullableContext(2)]
		[Nullable(0)]
		[StructLayout(3)]
		public struct Enumerator
		{
			// Token: 0x06003818 RID: 14360 RVA: 0x0009C934 File Offset: 0x0009AB34
			internal Enumerator(in SyntaxTokenList list)
			{
				this._parent = list._parent;
				this._singleNodeOrList = list.Node;
				this._baseIndex = list._index;
				this._count = list.Count;
				this._index = -1;
				this._current = null;
				this._position = list.Position;
			}

			// Token: 0x06003819 RID: 14361 RVA: 0x0009C98C File Offset: 0x0009AB8C
			public bool MoveNext()
			{
				if (this._count == 0 || this._count <= this._index + 1)
				{
					this._current = null;
					return false;
				}
				this._index++;
				if (this._current != null)
				{
					this._position += this._current.FullWidth;
				}
				this._current = SyntaxTokenList.GetGreenNodeAt(this._singleNodeOrList, this._index);
				return true;
			}

			// Token: 0x17000FB7 RID: 4023
			// (get) Token: 0x0600381A RID: 14362 RVA: 0x0009CA00 File Offset: 0x0009AC00
			public SyntaxToken Current
			{
				get
				{
					if (this._current == null)
					{
						throw new InvalidOperationException();
					}
					return new SyntaxToken(this._parent, this._current, this._position, this._baseIndex + this._index);
				}
			}

			// Token: 0x0600381B RID: 14363 RVA: 0x0009CA34 File Offset: 0x0009AC34
			public override bool Equals(object obj)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600381C RID: 14364 RVA: 0x0009CA3B File Offset: 0x0009AC3B
			public override int GetHashCode()
			{
				throw new NotSupportedException();
			}

			// Token: 0x0400181B RID: 6171
			private readonly SyntaxNode _parent;

			// Token: 0x0400181C RID: 6172
			private readonly GreenNode _singleNodeOrList;

			// Token: 0x0400181D RID: 6173
			private readonly int _baseIndex;

			// Token: 0x0400181E RID: 6174
			private readonly int _count;

			// Token: 0x0400181F RID: 6175
			private int _index;

			// Token: 0x04001820 RID: 6176
			private GreenNode _current;

			// Token: 0x04001821 RID: 6177
			private int _position;
		}

		// Token: 0x02000629 RID: 1577
		[NullableContext(0)]
		private class EnumeratorImpl : IEnumerator<SyntaxToken>, IEnumerator, IDisposable
		{
			// Token: 0x0600381D RID: 14365 RVA: 0x0009CA42 File Offset: 0x0009AC42
			internal EnumeratorImpl(in SyntaxTokenList list)
			{
				this._enumerator = new SyntaxTokenList.Enumerator(in list);
			}

			// Token: 0x17000FB8 RID: 4024
			// (get) Token: 0x0600381E RID: 14366 RVA: 0x0009CA56 File Offset: 0x0009AC56
			public SyntaxToken Current
			{
				get
				{
					return this._enumerator.Current;
				}
			}

			// Token: 0x17000FB9 RID: 4025
			// (get) Token: 0x0600381F RID: 14367 RVA: 0x0009CA63 File Offset: 0x0009AC63
			[Nullable(1)]
			object IEnumerator.Current
			{
				[NullableContext(1)]
				get
				{
					return this._enumerator.Current;
				}
			}

			// Token: 0x06003820 RID: 14368 RVA: 0x0009CA75 File Offset: 0x0009AC75
			public bool MoveNext()
			{
				return this._enumerator.MoveNext();
			}

			// Token: 0x06003821 RID: 14369 RVA: 0x0009CA82 File Offset: 0x0009AC82
			public void Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06003822 RID: 14370 RVA: 0x0009CA89 File Offset: 0x0009AC89
			public void Dispose()
			{
			}

			// Token: 0x04001822 RID: 6178
			private SyntaxTokenList.Enumerator _enumerator;
		}

		// Token: 0x0200062A RID: 1578
		[NullableContext(0)]
		public readonly struct Reversed : IEnumerable<SyntaxToken>, IEnumerable, IEquatable<SyntaxTokenList.Reversed>
		{
			// Token: 0x06003823 RID: 14371 RVA: 0x0009CA8B File Offset: 0x0009AC8B
			public Reversed(SyntaxTokenList list)
			{
				this._list = list;
			}

			// Token: 0x06003824 RID: 14372 RVA: 0x0009CA94 File Offset: 0x0009AC94
			public SyntaxTokenList.Reversed.Enumerator GetEnumerator()
			{
				return new SyntaxTokenList.Reversed.Enumerator(in this._list);
			}

			// Token: 0x06003825 RID: 14373 RVA: 0x0009CAA1 File Offset: 0x0009ACA1
			[NullableContext(1)]
			IEnumerator<SyntaxToken> IEnumerable<SyntaxToken>.GetEnumerator()
			{
				if (this._list.Count == 0)
				{
					return SpecializedCollections.EmptyEnumerator<SyntaxToken>();
				}
				return new SyntaxTokenList.Reversed.EnumeratorImpl(in this._list);
			}

			// Token: 0x06003826 RID: 14374 RVA: 0x0009CAC1 File Offset: 0x0009ACC1
			[NullableContext(1)]
			IEnumerator IEnumerable.GetEnumerator()
			{
				if (this._list.Count == 0)
				{
					return SpecializedCollections.EmptyEnumerator<SyntaxToken>();
				}
				return new SyntaxTokenList.Reversed.EnumeratorImpl(in this._list);
			}

			// Token: 0x06003827 RID: 14375 RVA: 0x0009CAE4 File Offset: 0x0009ACE4
			[NullableContext(2)]
			public override bool Equals(object obj)
			{
				if (obj is SyntaxTokenList.Reversed)
				{
					SyntaxTokenList.Reversed reversed = (SyntaxTokenList.Reversed)obj;
					return this.Equals(reversed);
				}
				return false;
			}

			// Token: 0x06003828 RID: 14376 RVA: 0x0009CB09 File Offset: 0x0009AD09
			public bool Equals(SyntaxTokenList.Reversed other)
			{
				return this._list.Equals(other._list);
			}

			// Token: 0x06003829 RID: 14377 RVA: 0x0009CB1C File Offset: 0x0009AD1C
			public override int GetHashCode()
			{
				return this._list.GetHashCode();
			}

			// Token: 0x04001823 RID: 6179
			private readonly SyntaxTokenList _list;

			// Token: 0x020007C6 RID: 1990
			[NullableContext(2)]
			[Nullable(0)]
			[StructLayout(3)]
			public struct Enumerator
			{
				// Token: 0x06003FE4 RID: 16356 RVA: 0x000B276C File Offset: 0x000B096C
				internal Enumerator(in SyntaxTokenList list)
				{
					this = default(SyntaxTokenList.Reversed.Enumerator);
					if (list.Any())
					{
						this._parent = list._parent;
						this._singleNodeOrList = list.Node;
						this._baseIndex = list._index;
						this._count = list.Count;
						this._index = this._count;
						this._current = null;
						SyntaxToken syntaxToken = list.Last();
						this._position = syntaxToken.Position + syntaxToken.FullWidth;
					}
				}

				// Token: 0x06003FE5 RID: 16357 RVA: 0x000B27E8 File Offset: 0x000B09E8
				public bool MoveNext()
				{
					if (this._count == 0 || this._index <= 0)
					{
						this._current = null;
						return false;
					}
					this._index--;
					this._current = SyntaxTokenList.GetGreenNodeAt(this._singleNodeOrList, this._index);
					this._position -= this._current.FullWidth;
					return true;
				}

				// Token: 0x17001188 RID: 4488
				// (get) Token: 0x06003FE6 RID: 16358 RVA: 0x000B284D File Offset: 0x000B0A4D
				public SyntaxToken Current
				{
					get
					{
						if (this._current == null)
						{
							throw new InvalidOperationException();
						}
						return new SyntaxToken(this._parent, this._current, this._position, this._baseIndex + this._index);
					}
				}

				// Token: 0x06003FE7 RID: 16359 RVA: 0x000B2881 File Offset: 0x000B0A81
				public override bool Equals(object obj)
				{
					throw new NotSupportedException();
				}

				// Token: 0x06003FE8 RID: 16360 RVA: 0x000B2888 File Offset: 0x000B0A88
				public override int GetHashCode()
				{
					throw new NotSupportedException();
				}

				// Token: 0x04001DE7 RID: 7655
				private readonly SyntaxNode _parent;

				// Token: 0x04001DE8 RID: 7656
				private readonly GreenNode _singleNodeOrList;

				// Token: 0x04001DE9 RID: 7657
				private readonly int _baseIndex;

				// Token: 0x04001DEA RID: 7658
				private readonly int _count;

				// Token: 0x04001DEB RID: 7659
				private int _index;

				// Token: 0x04001DEC RID: 7660
				private GreenNode _current;

				// Token: 0x04001DED RID: 7661
				private int _position;
			}

			// Token: 0x020007C7 RID: 1991
			private class EnumeratorImpl : IEnumerator<SyntaxToken>, IEnumerator, IDisposable
			{
				// Token: 0x06003FE9 RID: 16361 RVA: 0x000B288F File Offset: 0x000B0A8F
				internal EnumeratorImpl(in SyntaxTokenList list)
				{
					this._enumerator = new SyntaxTokenList.Reversed.Enumerator(in list);
				}

				// Token: 0x17001189 RID: 4489
				// (get) Token: 0x06003FEA RID: 16362 RVA: 0x000B28A3 File Offset: 0x000B0AA3
				public SyntaxToken Current
				{
					get
					{
						return this._enumerator.Current;
					}
				}

				// Token: 0x1700118A RID: 4490
				// (get) Token: 0x06003FEB RID: 16363 RVA: 0x000B28B0 File Offset: 0x000B0AB0
				[Nullable(1)]
				object IEnumerator.Current
				{
					[NullableContext(1)]
					get
					{
						return this._enumerator.Current;
					}
				}

				// Token: 0x06003FEC RID: 16364 RVA: 0x000B28C2 File Offset: 0x000B0AC2
				public bool MoveNext()
				{
					return this._enumerator.MoveNext();
				}

				// Token: 0x06003FED RID: 16365 RVA: 0x000B28CF File Offset: 0x000B0ACF
				public void Reset()
				{
					throw new NotSupportedException();
				}

				// Token: 0x06003FEE RID: 16366 RVA: 0x000B28D6 File Offset: 0x000B0AD6
				public void Dispose()
				{
				}

				// Token: 0x04001DEE RID: 7662
				private SyntaxTokenList.Reversed.Enumerator _enumerator;
			}
		}
	}
}
