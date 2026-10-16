using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200027F RID: 639
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct SyntaxNodeOrTokenList : IEquatable<SyntaxNodeOrTokenList>, IReadOnlyCollection<SyntaxNodeOrToken>, IEnumerable<SyntaxNodeOrToken>, IEnumerable
	{
		// Token: 0x060019B2 RID: 6578 RVA: 0x0004D056 File Offset: 0x0004B256
		[NullableContext(2)]
		internal SyntaxNodeOrTokenList(SyntaxNode node, int index)
		{
			this = default(SyntaxNodeOrTokenList);
			if (node != null)
			{
				this._node = node;
				this.index = index;
			}
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x0004D070 File Offset: 0x0004B270
		public SyntaxNodeOrTokenList(IEnumerable<SyntaxNodeOrToken> nodesAndTokens)
		{
			this = new SyntaxNodeOrTokenList(SyntaxNodeOrTokenList.CreateNode(nodesAndTokens), 0);
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x0004D07F File Offset: 0x0004B27F
		public SyntaxNodeOrTokenList(params SyntaxNodeOrToken[] nodesAndTokens)
		{
			this = new SyntaxNodeOrTokenList(nodesAndTokens);
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x0004D088 File Offset: 0x0004B288
		[return: Nullable(2)]
		private static SyntaxNode CreateNode(IEnumerable<SyntaxNodeOrToken> nodesAndTokens)
		{
			if (nodesAndTokens == null)
			{
				throw new ArgumentNullException("nodesAndTokens");
			}
			SyntaxNodeOrTokenListBuilder syntaxNodeOrTokenListBuilder = new SyntaxNodeOrTokenListBuilder(8);
			syntaxNodeOrTokenListBuilder.Add(nodesAndTokens);
			return syntaxNodeOrTokenListBuilder.ToList().Node;
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x060019B6 RID: 6582 RVA: 0x0004D0BD File Offset: 0x0004B2BD
		[Nullable(2)]
		internal SyntaxNode Node
		{
			[NullableContext(2)]
			get
			{
				return this._node;
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x060019B7 RID: 6583 RVA: 0x0004D0C5 File Offset: 0x0004B2C5
		internal int Position
		{
			get
			{
				SyntaxNode node = this._node;
				if (node == null)
				{
					return 0;
				}
				return node.Position;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x060019B8 RID: 6584 RVA: 0x0004D0D8 File Offset: 0x0004B2D8
		[Nullable(2)]
		internal SyntaxNode Parent
		{
			[NullableContext(2)]
			get
			{
				SyntaxNode node = this._node;
				if (node == null)
				{
					return null;
				}
				return node.Parent;
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x060019B9 RID: 6585 RVA: 0x0004D0EB File Offset: 0x0004B2EB
		public int Count
		{
			get
			{
				if (this._node == null)
				{
					return 0;
				}
				if (!this._node.Green.IsList)
				{
					return 1;
				}
				return this._node.SlotCount;
			}
		}

		// Token: 0x17000673 RID: 1651
		public SyntaxNodeOrToken this[int index]
		{
			get
			{
				if (this._node != null)
				{
					if (!this._node.IsList)
					{
						if (index == 0)
						{
							return this._node;
						}
					}
					else if (index < this._node.SlotCount)
					{
						GreenNode requiredSlot = this._node.Green.GetRequiredSlot(index);
						if (requiredSlot.IsToken)
						{
							return new SyntaxToken(this.Parent, requiredSlot, this._node.GetChildPosition(index), this.index + index);
						}
						return this._node.GetRequiredNodeSlot(index);
					}
				}
				throw new ArgumentOutOfRangeException("index");
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x060019BB RID: 6587 RVA: 0x0004D1B4 File Offset: 0x0004B3B4
		public TextSpan FullSpan
		{
			get
			{
				SyntaxNode node = this._node;
				if (node == null)
				{
					return default(TextSpan);
				}
				return node.FullSpan;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x060019BC RID: 6588 RVA: 0x0004D1DC File Offset: 0x0004B3DC
		public TextSpan Span
		{
			get
			{
				SyntaxNode node = this._node;
				if (node == null)
				{
					return default(TextSpan);
				}
				return node.Span;
			}
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x0004D202 File Offset: 0x0004B402
		public override string ToString()
		{
			if (this._node == null)
			{
				return string.Empty;
			}
			return this._node.ToString();
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x0004D21D File Offset: 0x0004B41D
		public string ToFullString()
		{
			if (this._node == null)
			{
				return string.Empty;
			}
			return this._node.ToFullString();
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x0004D238 File Offset: 0x0004B438
		public SyntaxNodeOrToken First()
		{
			return this[0];
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x0004D244 File Offset: 0x0004B444
		public SyntaxNodeOrToken FirstOrDefault()
		{
			if (!this.Any())
			{
				return default(SyntaxNodeOrToken);
			}
			return this[0];
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x0004D26A File Offset: 0x0004B46A
		public SyntaxNodeOrToken Last()
		{
			return this[this.Count - 1];
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x0004D27C File Offset: 0x0004B47C
		public SyntaxNodeOrToken LastOrDefault()
		{
			if (!this.Any())
			{
				return default(SyntaxNodeOrToken);
			}
			return this[this.Count - 1];
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x0004D2AC File Offset: 0x0004B4AC
		public int IndexOf(SyntaxNodeOrToken nodeOrToken)
		{
			int num = 0;
			using (SyntaxNodeOrTokenList.Enumerator enumerator = this.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == nodeOrToken)
					{
						return num;
					}
					num++;
				}
			}
			return -1;
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x0004D308 File Offset: 0x0004B508
		public bool Any()
		{
			return this._node != null;
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x0004D314 File Offset: 0x0004B514
		internal void CopyTo(int offset, [Nullable(new byte[] { 1, 2 })] GreenNode[] array, int arrayOffset, int count)
		{
			for (int i = 0; i < count; i++)
			{
				array[arrayOffset + i] = this[i + offset].UnderlyingNode;
			}
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x0004D344 File Offset: 0x0004B544
		public SyntaxNodeOrTokenList Add(SyntaxNodeOrToken nodeOrToken)
		{
			return this.Insert(this.Count, nodeOrToken);
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x0004D353 File Offset: 0x0004B553
		public SyntaxNodeOrTokenList AddRange(IEnumerable<SyntaxNodeOrToken> nodesOrTokens)
		{
			return this.InsertRange(this.Count, nodesOrTokens);
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x0004D364 File Offset: 0x0004B564
		public SyntaxNodeOrTokenList Insert(int index, SyntaxNodeOrToken nodeOrToken)
		{
			if (nodeOrToken == default(SyntaxNodeOrToken))
			{
				throw new ArgumentOutOfRangeException("nodeOrToken");
			}
			return this.InsertRange(index, SpecializedCollections.SingletonEnumerable<SyntaxNodeOrToken>(nodeOrToken));
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x0004D39C File Offset: 0x0004B59C
		public SyntaxNodeOrTokenList InsertRange(int index, IEnumerable<SyntaxNodeOrToken> nodesAndTokens)
		{
			if (index < 0 || index > this.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (nodesAndTokens == null)
			{
				throw new ArgumentNullException("nodesAndTokens");
			}
			if (nodesAndTokens.IsEmpty<SyntaxNodeOrToken>())
			{
				return this;
			}
			List<SyntaxNodeOrToken> list = Enumerable.ToList<SyntaxNodeOrToken>(this);
			list.InsertRange(index, nodesAndTokens);
			return SyntaxNodeOrTokenList.CreateList(list);
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x0004D3FC File Offset: 0x0004B5FC
		private static SyntaxNodeOrTokenList CreateList(List<SyntaxNodeOrToken> items)
		{
			if (items.Count == 0)
			{
				return default(SyntaxNodeOrTokenList);
			}
			GreenNode greenNode = GreenNode.CreateList<SyntaxNodeOrToken>(items, (SyntaxNodeOrToken n) => n.RequiredUnderlyingNode);
			if (greenNode.IsToken)
			{
				greenNode = Microsoft.CodeAnalysis.Syntax.InternalSyntax.SyntaxList.List(new ArrayElement<GreenNode>[]
				{
					new ArrayElement<GreenNode>
					{
						Value = greenNode
					}
				});
			}
			return new SyntaxNodeOrTokenList(greenNode.CreateRed(), 0);
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x0004D479 File Offset: 0x0004B679
		public SyntaxNodeOrTokenList RemoveAt(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			List<SyntaxNodeOrToken> list = Enumerable.ToList<SyntaxNodeOrToken>(this);
			list.RemoveAt(index);
			return SyntaxNodeOrTokenList.CreateList(list);
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x0004D4B0 File Offset: 0x0004B6B0
		public SyntaxNodeOrTokenList Remove(SyntaxNodeOrToken nodeOrTokenInList)
		{
			int num = this.IndexOf(nodeOrTokenInList);
			if (num >= 0 && num < this.Count)
			{
				return this.RemoveAt(num);
			}
			return this;
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x0004D4E0 File Offset: 0x0004B6E0
		public SyntaxNodeOrTokenList Replace(SyntaxNodeOrToken nodeOrTokenInList, SyntaxNodeOrToken newNodeOrToken)
		{
			if (newNodeOrToken == default(SyntaxNodeOrToken))
			{
				throw new ArgumentOutOfRangeException("newNodeOrToken");
			}
			return this.ReplaceRange(nodeOrTokenInList, new SyntaxNodeOrToken[] { newNodeOrToken });
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x0004D520 File Offset: 0x0004B720
		public SyntaxNodeOrTokenList ReplaceRange(SyntaxNodeOrToken nodeOrTokenInList, IEnumerable<SyntaxNodeOrToken> newNodesAndTokens)
		{
			int num = this.IndexOf(nodeOrTokenInList);
			if (num >= 0 && num < this.Count)
			{
				List<SyntaxNodeOrToken> list = Enumerable.ToList<SyntaxNodeOrToken>(this);
				list.RemoveAt(num);
				list.InsertRange(num, newNodesAndTokens);
				return SyntaxNodeOrTokenList.CreateList(list);
			}
			throw new ArgumentOutOfRangeException("nodeOrTokenInList");
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x060019CF RID: 6607 RVA: 0x0004D571 File Offset: 0x0004B771
		private SyntaxNodeOrToken[] Nodes
		{
			get
			{
				return Enumerable.ToArray<SyntaxNodeOrToken>(this);
			}
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x0004D583 File Offset: 0x0004B783
		public SyntaxNodeOrTokenList.Enumerator GetEnumerator()
		{
			return new SyntaxNodeOrTokenList.Enumerator(in this);
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x0004D58C File Offset: 0x0004B78C
		IEnumerator<SyntaxNodeOrToken> IEnumerable<SyntaxNodeOrToken>.GetEnumerator()
		{
			if (this._node != null)
			{
				return this.GetEnumerator();
			}
			return SpecializedCollections.EmptyEnumerator<SyntaxNodeOrToken>();
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x0004D5B4 File Offset: 0x0004B7B4
		IEnumerator IEnumerable.GetEnumerator()
		{
			if (this._node != null)
			{
				return this.GetEnumerator();
			}
			return SpecializedCollections.EmptyEnumerator<SyntaxNodeOrToken>();
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x0004D5DC File Offset: 0x0004B7DC
		public static bool operator ==(SyntaxNodeOrTokenList left, SyntaxNodeOrTokenList right)
		{
			return left.Equals(right);
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x0004D5E6 File Offset: 0x0004B7E6
		public static bool operator !=(SyntaxNodeOrTokenList left, SyntaxNodeOrTokenList right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x0004D5F3 File Offset: 0x0004B7F3
		public bool Equals(SyntaxNodeOrTokenList other)
		{
			return this._node == other._node;
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x0004D603 File Offset: 0x0004B803
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is SyntaxNodeOrTokenList && this.Equals((SyntaxNodeOrTokenList)obj);
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x0004D61B File Offset: 0x0004B81B
		public override int GetHashCode()
		{
			SyntaxNode node = this._node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x04000A20 RID: 2592
		[Nullable(2)]
		private readonly SyntaxNode _node;

		// Token: 0x04000A21 RID: 2593
		internal readonly int index;

		// Token: 0x02000625 RID: 1573
		[NullableContext(0)]
		public struct Enumerator : IEnumerator<SyntaxNodeOrToken>, IEnumerator, IDisposable
		{
			// Token: 0x06003807 RID: 14343 RVA: 0x0009C857 File Offset: 0x0009AA57
			internal Enumerator(in SyntaxNodeOrTokenList list)
			{
				this = default(SyntaxNodeOrTokenList.Enumerator);
				this._list = list;
				this._index = -1;
			}

			// Token: 0x06003808 RID: 14344 RVA: 0x0009C873 File Offset: 0x0009AA73
			public bool MoveNext()
			{
				if (this._index < this._list.Count)
				{
					this._index++;
				}
				return this._index < this._list.Count;
			}

			// Token: 0x17000FB5 RID: 4021
			// (get) Token: 0x06003809 RID: 14345 RVA: 0x0009C8A9 File Offset: 0x0009AAA9
			public SyntaxNodeOrToken Current
			{
				get
				{
					return this._list[this._index];
				}
			}

			// Token: 0x17000FB6 RID: 4022
			// (get) Token: 0x0600380A RID: 14346 RVA: 0x0009C8BC File Offset: 0x0009AABC
			[Nullable(1)]
			object IEnumerator.Current
			{
				[NullableContext(1)]
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600380B RID: 14347 RVA: 0x0009C8C9 File Offset: 0x0009AAC9
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600380C RID: 14348 RVA: 0x0009C8D0 File Offset: 0x0009AAD0
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600380D RID: 14349 RVA: 0x0009C8D2 File Offset: 0x0009AAD2
			[NullableContext(2)]
			public override bool Equals(object obj)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600380E RID: 14350 RVA: 0x0009C8D9 File Offset: 0x0009AAD9
			public override int GetHashCode()
			{
				throw new NotSupportedException();
			}

			// Token: 0x04001814 RID: 6164
			private readonly SyntaxNodeOrTokenList _list;

			// Token: 0x04001815 RID: 6165
			private int _index;
		}
	}
}
