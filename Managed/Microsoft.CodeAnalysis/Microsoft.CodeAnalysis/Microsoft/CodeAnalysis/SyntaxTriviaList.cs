using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Syntax;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000288 RID: 648
	[NullableContext(1)]
	[Nullable(0)]
	[StructLayout(3)]
	public readonly struct SyntaxTriviaList : IEquatable<SyntaxTriviaList>, IReadOnlyList<SyntaxTrivia>, IEnumerable<SyntaxTrivia>, IEnumerable, IReadOnlyCollection<SyntaxTrivia>
	{
		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001AAE RID: 6830 RVA: 0x0004F098 File Offset: 0x0004D298
		public static SyntaxTriviaList Empty
		{
			get
			{
				return default(SyntaxTriviaList);
			}
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x0004F0AE File Offset: 0x0004D2AE
		[NullableContext(2)]
		internal SyntaxTriviaList(in SyntaxToken token, GreenNode node, int position, int index = 0)
		{
			this.Token = token;
			this.Node = node;
			this.Position = position;
			this.Index = index;
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x0004F0D2 File Offset: 0x0004D2D2
		[NullableContext(2)]
		internal SyntaxTriviaList(in SyntaxToken token, GreenNode node)
		{
			this.Token = token;
			this.Node = node;
			this.Position = token.Position;
			this.Index = 0;
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x0004F0FA File Offset: 0x0004D2FA
		public SyntaxTriviaList(SyntaxTrivia trivia)
		{
			this.Token = default(SyntaxToken);
			this.Node = trivia.UnderlyingNode;
			this.Position = 0;
			this.Index = 0;
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x0004F124 File Offset: 0x0004D324
		public SyntaxTriviaList(params SyntaxTrivia[] trivias)
		{
			SyntaxToken syntaxToken = default(SyntaxToken);
			this = new SyntaxTriviaList(in syntaxToken, SyntaxTriviaList.CreateNode(trivias), 0, 0);
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x0004F148 File Offset: 0x0004D348
		[NullableContext(2)]
		public SyntaxTriviaList(IEnumerable<SyntaxTrivia> trivias)
		{
			SyntaxToken syntaxToken = default(SyntaxToken);
			this = new SyntaxTriviaList(in syntaxToken, SyntaxTriviaListBuilder.Create(trivias).Node, 0, 0);
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x0004F174 File Offset: 0x0004D374
		[NullableContext(2)]
		private static GreenNode CreateNode(SyntaxTrivia[] trivias)
		{
			if (trivias == null)
			{
				return null;
			}
			SyntaxTriviaListBuilder syntaxTriviaListBuilder = new SyntaxTriviaListBuilder(trivias.Length);
			syntaxTriviaListBuilder.Add(trivias);
			return syntaxTriviaListBuilder.ToList().Node;
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001AB5 RID: 6837 RVA: 0x0004F1A2 File Offset: 0x0004D3A2
		internal SyntaxToken Token { get; }

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001AB6 RID: 6838 RVA: 0x0004F1AA File Offset: 0x0004D3AA
		[Nullable(2)]
		internal GreenNode Node
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06001AB7 RID: 6839 RVA: 0x0004F1B2 File Offset: 0x0004D3B2
		internal int Position { get; }

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06001AB8 RID: 6840 RVA: 0x0004F1BA File Offset: 0x0004D3BA
		internal int Index { get; }

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001AB9 RID: 6841 RVA: 0x0004F1C2 File Offset: 0x0004D3C2
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

		// Token: 0x06001ABA RID: 6842 RVA: 0x0004F1E8 File Offset: 0x0004D3E8
		public SyntaxTrivia ElementAt(int index)
		{
			return this[index];
		}

		// Token: 0x170006BE RID: 1726
		public SyntaxTrivia this[int index]
		{
			get
			{
				if (this.Node != null)
				{
					if (this.Node.IsList)
					{
						if (index < this.Node.SlotCount)
						{
							SyntaxToken syntaxToken = this.Token;
							return new SyntaxTrivia(in syntaxToken, this.Node.GetSlot(index), this.Position + this.Node.GetSlotOffset(index), this.Index + index);
						}
					}
					else if (index == 0)
					{
						SyntaxToken syntaxToken = this.Token;
						return new SyntaxTrivia(in syntaxToken, this.Node, this.Position, this.Index);
					}
				}
				throw new ArgumentOutOfRangeException("index");
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001ABC RID: 6844 RVA: 0x0004F288 File Offset: 0x0004D488
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

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001ABD RID: 6845 RVA: 0x0004F2C0 File Offset: 0x0004D4C0
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

		// Token: 0x06001ABE RID: 6846 RVA: 0x0004F314 File Offset: 0x0004D514
		public SyntaxTrivia First()
		{
			if (this.Any())
			{
				return this[0];
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x0004F32B File Offset: 0x0004D52B
		public SyntaxTrivia Last()
		{
			if (this.Any())
			{
				return this[this.Count - 1];
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x0004F349 File Offset: 0x0004D549
		public bool Any()
		{
			return this.Node != null;
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x0004F354 File Offset: 0x0004D554
		public SyntaxTriviaList.Reversed Reverse()
		{
			return new SyntaxTriviaList.Reversed(this);
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x0004F361 File Offset: 0x0004D561
		public SyntaxTriviaList.Enumerator GetEnumerator()
		{
			return new SyntaxTriviaList.Enumerator(in this);
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x0004F36C File Offset: 0x0004D56C
		public int IndexOf(SyntaxTrivia triviaInList)
		{
			int i = 0;
			int count = this.Count;
			while (i < count)
			{
				if (this[i] == triviaInList)
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x0004F3A0 File Offset: 0x0004D5A0
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

		// Token: 0x06001AC5 RID: 6853 RVA: 0x0004F3D5 File Offset: 0x0004D5D5
		public SyntaxTriviaList Add(SyntaxTrivia trivia)
		{
			return this.Insert(this.Count, trivia);
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x0004F3E4 File Offset: 0x0004D5E4
		public SyntaxTriviaList AddRange(IEnumerable<SyntaxTrivia> trivia)
		{
			return this.InsertRange(this.Count, trivia);
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x0004F3F4 File Offset: 0x0004D5F4
		public SyntaxTriviaList Insert(int index, SyntaxTrivia trivia)
		{
			if (trivia == default(SyntaxTrivia))
			{
				throw new ArgumentOutOfRangeException("trivia");
			}
			return this.InsertRange(index, new SyntaxTrivia[] { trivia });
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x0004F432 File Offset: 0x0004D632
		private static SyntaxTriviaListBuilder GetBuilder()
		{
			return SyntaxTriviaList.s_builderPool.Allocate();
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x0004F43E File Offset: 0x0004D63E
		private static void ClearAndFreeBuilder(SyntaxTriviaListBuilder builder)
		{
			if (builder.Count <= 16)
			{
				builder.Clear();
				SyntaxTriviaList.s_builderPool.Free(builder);
			}
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x0004F45C File Offset: 0x0004D65C
		public SyntaxTriviaList InsertRange(int index, IEnumerable<SyntaxTrivia> trivia)
		{
			int count = this.Count;
			if (index < 0 || index > count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (trivia == null)
			{
				throw new ArgumentNullException("trivia");
			}
			ICollection<SyntaxTrivia> collection = trivia as ICollection<SyntaxTrivia>;
			if (collection != null && collection.Count == 0)
			{
				return this;
			}
			SyntaxTriviaListBuilder builder = SyntaxTriviaList.GetBuilder();
			SyntaxTriviaList syntaxTriviaList;
			try
			{
				for (int i = 0; i < index; i++)
				{
					builder.Add(this[i]);
				}
				builder.AddRange(trivia);
				for (int j = index; j < count; j++)
				{
					builder.Add(this[j]);
				}
				syntaxTriviaList = ((builder.Count == count) ? this : builder.ToList());
			}
			finally
			{
				SyntaxTriviaList.ClearAndFreeBuilder(builder);
			}
			return syntaxTriviaList;
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x0004F524 File Offset: 0x0004D724
		public SyntaxTriviaList RemoveAt(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			List<SyntaxTrivia> list = Enumerable.ToList<SyntaxTrivia>(this);
			list.RemoveAt(index);
			SyntaxToken syntaxToken = default(SyntaxToken);
			return new SyntaxTriviaList(in syntaxToken, GreenNode.CreateList<SyntaxTrivia>(list, (SyntaxTrivia n) => n.RequiredUnderlyingNode), 0, 0);
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x0004F598 File Offset: 0x0004D798
		public SyntaxTriviaList Remove(SyntaxTrivia triviaInList)
		{
			int num = this.IndexOf(triviaInList);
			if (num >= 0 && num < this.Count)
			{
				return this.RemoveAt(num);
			}
			return this;
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x0004F5C8 File Offset: 0x0004D7C8
		public SyntaxTriviaList Replace(SyntaxTrivia triviaInList, SyntaxTrivia newTrivia)
		{
			if (newTrivia == default(SyntaxTrivia))
			{
				throw new ArgumentOutOfRangeException("newTrivia");
			}
			return this.ReplaceRange(triviaInList, new SyntaxTrivia[] { newTrivia });
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x0004F608 File Offset: 0x0004D808
		public SyntaxTriviaList ReplaceRange(SyntaxTrivia triviaInList, IEnumerable<SyntaxTrivia> newTrivia)
		{
			int num = this.IndexOf(triviaInList);
			if (num >= 0 && num < this.Count)
			{
				List<SyntaxTrivia> list = Enumerable.ToList<SyntaxTrivia>(this);
				list.RemoveAt(num);
				list.InsertRange(num, newTrivia);
				SyntaxToken syntaxToken = default(SyntaxToken);
				return new SyntaxTriviaList(in syntaxToken, GreenNode.CreateList<SyntaxTrivia>(list, (SyntaxTrivia n) => n.RequiredUnderlyingNode), 0, 0);
			}
			throw new ArgumentOutOfRangeException("triviaInList");
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001ACF RID: 6863 RVA: 0x0004F68A File Offset: 0x0004D88A
		private SyntaxTrivia[] Nodes
		{
			get
			{
				return Enumerable.ToArray<SyntaxTrivia>(this);
			}
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x0004F69C File Offset: 0x0004D89C
		IEnumerator<SyntaxTrivia> IEnumerable<SyntaxTrivia>.GetEnumerator()
		{
			if (this.Node == null)
			{
				return SpecializedCollections.EmptyEnumerator<SyntaxTrivia>();
			}
			return new SyntaxTriviaList.EnumeratorImpl(in this);
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x0004F6B2 File Offset: 0x0004D8B2
		IEnumerator IEnumerable.GetEnumerator()
		{
			if (this.Node == null)
			{
				return SpecializedCollections.EmptyEnumerator<SyntaxTrivia>();
			}
			return new SyntaxTriviaList.EnumeratorImpl(in this);
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x0004F6C8 File Offset: 0x0004D8C8
		[NullableContext(2)]
		private GreenNode GetGreenNodeAt(int i)
		{
			return SyntaxTriviaList.GetGreenNodeAt(this.Node, i);
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x0004F6D6 File Offset: 0x0004D8D6
		[return: Nullable(2)]
		private static GreenNode GetGreenNodeAt(GreenNode node, int i)
		{
			if (!node.IsList)
			{
				return node;
			}
			return node.GetSlot(i);
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x0004F6EC File Offset: 0x0004D8EC
		public bool Equals(SyntaxTriviaList other)
		{
			return this.Node == other.Node && this.Index == other.Index && this.Token.Equals(other.Token);
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x0004F72E File Offset: 0x0004D92E
		public static bool operator ==(SyntaxTriviaList left, SyntaxTriviaList right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x0004F738 File Offset: 0x0004D938
		public static bool operator !=(SyntaxTriviaList left, SyntaxTriviaList right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x0004F748 File Offset: 0x0004D948
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is SyntaxTriviaList)
			{
				SyntaxTriviaList syntaxTriviaList = (SyntaxTriviaList)obj;
				return this.Equals(syntaxTriviaList);
			}
			return false;
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x0004F770 File Offset: 0x0004D970
		public override int GetHashCode()
		{
			return Hash.Combine(this.Token.GetHashCode(), Hash.Combine<GreenNode>(this.Node, this.Index));
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x0004F7A8 File Offset: 0x0004D9A8
		internal void CopyTo(int offset, SyntaxTrivia[] array, int arrayOffset, int count)
		{
			if (offset < 0 || count < 0 || this.Count < offset + count)
			{
				throw new IndexOutOfRangeException();
			}
			if (count == 0)
			{
				return;
			}
			SyntaxTrivia syntaxTrivia = this[offset];
			array[arrayOffset] = syntaxTrivia;
			int num = syntaxTrivia.Position;
			SyntaxTrivia syntaxTrivia2 = syntaxTrivia;
			for (int i = 1; i < count; i++)
			{
				num += syntaxTrivia2.FullWidth;
				SyntaxToken token = this.Token;
				syntaxTrivia2 = new SyntaxTrivia(in token, this.GetGreenNodeAt(offset + i), num, this.Index + i);
				array[arrayOffset + i] = syntaxTrivia2;
			}
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x0004F832 File Offset: 0x0004DA32
		public override string ToString()
		{
			if (this.Node == null)
			{
				return string.Empty;
			}
			return this.Node.ToString();
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x0004F84D File Offset: 0x0004DA4D
		public string ToFullString()
		{
			if (this.Node == null)
			{
				return string.Empty;
			}
			return this.Node.ToFullString();
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x0004F868 File Offset: 0x0004DA68
		public static SyntaxTriviaList Create(SyntaxTrivia trivia)
		{
			return new SyntaxTriviaList(trivia);
		}

		// Token: 0x04000A43 RID: 2627
		private static readonly ObjectPool<SyntaxTriviaListBuilder> s_builderPool = new ObjectPool<SyntaxTriviaListBuilder>(() => SyntaxTriviaListBuilder.Create());

		// Token: 0x0200062E RID: 1582
		[NullableContext(2)]
		[Nullable(0)]
		[StructLayout(3)]
		public struct Enumerator
		{
			// Token: 0x06003834 RID: 14388 RVA: 0x0009CBA0 File Offset: 0x0009ADA0
			internal Enumerator(in SyntaxTriviaList list)
			{
				this._token = list.Token;
				this._singleNodeOrList = list.Node;
				this._baseIndex = list.Index;
				this._count = list.Count;
				this._index = -1;
				this._current = null;
				this._position = list.Position;
			}

			// Token: 0x06003835 RID: 14389 RVA: 0x0009CBF8 File Offset: 0x0009ADF8
			[NullableContext(1)]
			private void InitializeFrom(in SyntaxToken token, GreenNode greenNode, int index, int position)
			{
				this._token = token;
				this._singleNodeOrList = greenNode;
				this._baseIndex = index;
				this._count = (greenNode.IsList ? greenNode.SlotCount : 1);
				this._index = -1;
				this._current = null;
				this._position = position;
			}

			// Token: 0x06003836 RID: 14390 RVA: 0x0009CC4C File Offset: 0x0009AE4C
			internal void InitializeFromLeadingTrivia(in SyntaxToken token)
			{
				GreenNode leadingTriviaCore = token.Node.GetLeadingTriviaCore();
				this.InitializeFrom(in token, leadingTriviaCore, 0, token.Position);
			}

			// Token: 0x06003837 RID: 14391 RVA: 0x0009CC74 File Offset: 0x0009AE74
			internal void InitializeFromTrailingTrivia(in SyntaxToken token)
			{
				GreenNode leadingTriviaCore = token.Node.GetLeadingTriviaCore();
				int num = 0;
				if (leadingTriviaCore != null)
				{
					num = (leadingTriviaCore.IsList ? leadingTriviaCore.SlotCount : 1);
				}
				GreenNode trailingTriviaCore = token.Node.GetTrailingTriviaCore();
				int num2 = token.Position + token.FullWidth;
				if (trailingTriviaCore != null)
				{
					num2 -= trailingTriviaCore.FullWidth;
				}
				this.InitializeFrom(in token, trailingTriviaCore, num, num2);
			}

			// Token: 0x06003838 RID: 14392 RVA: 0x0009CCD4 File Offset: 0x0009AED4
			public bool MoveNext()
			{
				int num = this._index + 1;
				if (num >= this._count)
				{
					this._current = null;
					return false;
				}
				this._index = num;
				if (this._current != null)
				{
					this._position += this._current.FullWidth;
				}
				this._current = SyntaxTriviaList.GetGreenNodeAt(this._singleNodeOrList, num);
				return true;
			}

			// Token: 0x17000FBA RID: 4026
			// (get) Token: 0x06003839 RID: 14393 RVA: 0x0009CD36 File Offset: 0x0009AF36
			public SyntaxTrivia Current
			{
				get
				{
					if (this._current == null)
					{
						throw new InvalidOperationException();
					}
					return new SyntaxTrivia(in this._token, this._current, this._position, this._baseIndex + this._index);
				}
			}

			// Token: 0x0600383A RID: 14394 RVA: 0x0009CD6A File Offset: 0x0009AF6A
			internal bool TryMoveNextAndGetCurrent(out SyntaxTrivia current)
			{
				if (!this.MoveNext())
				{
					current = default(SyntaxTrivia);
					return false;
				}
				current = new SyntaxTrivia(in this._token, this._current, this._position, this._baseIndex + this._index);
				return true;
			}

			// Token: 0x0400182A RID: 6186
			private SyntaxToken _token;

			// Token: 0x0400182B RID: 6187
			private GreenNode _singleNodeOrList;

			// Token: 0x0400182C RID: 6188
			private int _baseIndex;

			// Token: 0x0400182D RID: 6189
			private int _count;

			// Token: 0x0400182E RID: 6190
			private int _index;

			// Token: 0x0400182F RID: 6191
			private GreenNode _current;

			// Token: 0x04001830 RID: 6192
			private int _position;
		}

		// Token: 0x0200062F RID: 1583
		[NullableContext(0)]
		private class EnumeratorImpl : IEnumerator<SyntaxTrivia>, IEnumerator, IDisposable
		{
			// Token: 0x0600383B RID: 14395 RVA: 0x0009CDA8 File Offset: 0x0009AFA8
			internal EnumeratorImpl(in SyntaxTriviaList list)
			{
				this._enumerator = new SyntaxTriviaList.Enumerator(in list);
			}

			// Token: 0x17000FBB RID: 4027
			// (get) Token: 0x0600383C RID: 14396 RVA: 0x0009CDBC File Offset: 0x0009AFBC
			public SyntaxTrivia Current
			{
				get
				{
					return this._enumerator.Current;
				}
			}

			// Token: 0x17000FBC RID: 4028
			// (get) Token: 0x0600383D RID: 14397 RVA: 0x0009CDC9 File Offset: 0x0009AFC9
			[Nullable(1)]
			object IEnumerator.Current
			{
				[NullableContext(1)]
				get
				{
					return this._enumerator.Current;
				}
			}

			// Token: 0x0600383E RID: 14398 RVA: 0x0009CDDB File Offset: 0x0009AFDB
			public bool MoveNext()
			{
				return this._enumerator.MoveNext();
			}

			// Token: 0x0600383F RID: 14399 RVA: 0x0009CDE8 File Offset: 0x0009AFE8
			public void Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06003840 RID: 14400 RVA: 0x0009CDEF File Offset: 0x0009AFEF
			public void Dispose()
			{
			}

			// Token: 0x04001831 RID: 6193
			private SyntaxTriviaList.Enumerator _enumerator;
		}

		// Token: 0x02000630 RID: 1584
		[NullableContext(0)]
		public readonly struct Reversed : IEnumerable<SyntaxTrivia>, IEnumerable, IEquatable<SyntaxTriviaList.Reversed>
		{
			// Token: 0x06003841 RID: 14401 RVA: 0x0009CDF1 File Offset: 0x0009AFF1
			public Reversed(SyntaxTriviaList list)
			{
				this._list = list;
			}

			// Token: 0x06003842 RID: 14402 RVA: 0x0009CDFA File Offset: 0x0009AFFA
			public SyntaxTriviaList.Reversed.Enumerator GetEnumerator()
			{
				return new SyntaxTriviaList.Reversed.Enumerator(in this._list);
			}

			// Token: 0x06003843 RID: 14403 RVA: 0x0009CE07 File Offset: 0x0009B007
			[NullableContext(1)]
			IEnumerator<SyntaxTrivia> IEnumerable<SyntaxTrivia>.GetEnumerator()
			{
				if (this._list.Count == 0)
				{
					return SpecializedCollections.EmptyEnumerator<SyntaxTrivia>();
				}
				return new SyntaxTriviaList.Reversed.ReversedEnumeratorImpl(in this._list);
			}

			// Token: 0x06003844 RID: 14404 RVA: 0x0009CE27 File Offset: 0x0009B027
			[NullableContext(1)]
			IEnumerator IEnumerable.GetEnumerator()
			{
				if (this._list.Count == 0)
				{
					return SpecializedCollections.EmptyEnumerator<SyntaxTrivia>();
				}
				return new SyntaxTriviaList.Reversed.ReversedEnumeratorImpl(in this._list);
			}

			// Token: 0x06003845 RID: 14405 RVA: 0x0009CE47 File Offset: 0x0009B047
			public override int GetHashCode()
			{
				return this._list.GetHashCode();
			}

			// Token: 0x06003846 RID: 14406 RVA: 0x0009CE5A File Offset: 0x0009B05A
			[NullableContext(2)]
			public override bool Equals(object obj)
			{
				return obj is SyntaxTriviaList.Reversed && this.Equals((SyntaxTriviaList.Reversed)obj);
			}

			// Token: 0x06003847 RID: 14407 RVA: 0x0009CE72 File Offset: 0x0009B072
			public bool Equals(SyntaxTriviaList.Reversed other)
			{
				return this._list.Equals(other._list);
			}

			// Token: 0x04001832 RID: 6194
			private readonly SyntaxTriviaList _list;

			// Token: 0x020007C8 RID: 1992
			[NullableContext(2)]
			[Nullable(0)]
			[StructLayout(3)]
			public struct Enumerator
			{
				// Token: 0x06003FEF RID: 16367 RVA: 0x000B28D8 File Offset: 0x000B0AD8
				internal Enumerator(in SyntaxTriviaList list)
				{
					this = default(SyntaxTriviaList.Reversed.Enumerator);
					if (list.Node != null)
					{
						this._token = list.Token;
						this._singleNodeOrList = list.Node;
						this._baseIndex = list.Index;
						this._count = list.Count;
						this._index = this._count;
						this._current = null;
						SyntaxTrivia syntaxTrivia = list.Last();
						this._position = syntaxTrivia.Position + syntaxTrivia.FullWidth;
					}
				}

				// Token: 0x06003FF0 RID: 16368 RVA: 0x000B2954 File Offset: 0x000B0B54
				public bool MoveNext()
				{
					if (this._count == 0 || this._index <= 0)
					{
						this._current = null;
						return false;
					}
					this._index--;
					this._current = SyntaxTriviaList.GetGreenNodeAt(this._singleNodeOrList, this._index);
					this._position -= this._current.FullWidth;
					return true;
				}

				// Token: 0x1700118B RID: 4491
				// (get) Token: 0x06003FF1 RID: 16369 RVA: 0x000B29B9 File Offset: 0x000B0BB9
				public SyntaxTrivia Current
				{
					get
					{
						if (this._current == null)
						{
							throw new InvalidOperationException();
						}
						return new SyntaxTrivia(in this._token, this._current, this._position, this._baseIndex + this._index);
					}
				}

				// Token: 0x04001DEF RID: 7663
				private readonly SyntaxToken _token;

				// Token: 0x04001DF0 RID: 7664
				private readonly GreenNode _singleNodeOrList;

				// Token: 0x04001DF1 RID: 7665
				private readonly int _baseIndex;

				// Token: 0x04001DF2 RID: 7666
				private readonly int _count;

				// Token: 0x04001DF3 RID: 7667
				private int _index;

				// Token: 0x04001DF4 RID: 7668
				private GreenNode _current;

				// Token: 0x04001DF5 RID: 7669
				private int _position;
			}

			// Token: 0x020007C9 RID: 1993
			private class ReversedEnumeratorImpl : IEnumerator<SyntaxTrivia>, IEnumerator, IDisposable
			{
				// Token: 0x06003FF2 RID: 16370 RVA: 0x000B29ED File Offset: 0x000B0BED
				internal ReversedEnumeratorImpl(in SyntaxTriviaList list)
				{
					this._enumerator = new SyntaxTriviaList.Reversed.Enumerator(in list);
				}

				// Token: 0x1700118C RID: 4492
				// (get) Token: 0x06003FF3 RID: 16371 RVA: 0x000B2A01 File Offset: 0x000B0C01
				public SyntaxTrivia Current
				{
					get
					{
						return this._enumerator.Current;
					}
				}

				// Token: 0x1700118D RID: 4493
				// (get) Token: 0x06003FF4 RID: 16372 RVA: 0x000B2A0E File Offset: 0x000B0C0E
				[Nullable(1)]
				object IEnumerator.Current
				{
					[NullableContext(1)]
					get
					{
						return this._enumerator.Current;
					}
				}

				// Token: 0x06003FF5 RID: 16373 RVA: 0x000B2A20 File Offset: 0x000B0C20
				public bool MoveNext()
				{
					return this._enumerator.MoveNext();
				}

				// Token: 0x06003FF6 RID: 16374 RVA: 0x000B2A2D File Offset: 0x000B0C2D
				public void Reset()
				{
					throw new NotSupportedException();
				}

				// Token: 0x06003FF7 RID: 16375 RVA: 0x000B2A34 File Offset: 0x000B0C34
				public void Dispose()
				{
				}

				// Token: 0x04001DF6 RID: 7670
				private SyntaxTriviaList.Reversed.Enumerator _enumerator;
			}
		}
	}
}
