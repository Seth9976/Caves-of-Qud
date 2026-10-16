using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200026E RID: 622
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct ChildSyntaxList : IEquatable<ChildSyntaxList>, IReadOnlyList<SyntaxNodeOrToken>, IEnumerable<SyntaxNodeOrToken>, IEnumerable, IReadOnlyCollection<SyntaxNodeOrToken>
	{
		// Token: 0x06001784 RID: 6020 RVA: 0x00046343 File Offset: 0x00044543
		internal ChildSyntaxList(SyntaxNode node)
		{
			this._node = node;
			this._count = ChildSyntaxList.CountNodes(node.Green);
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001785 RID: 6021 RVA: 0x0004635D File Offset: 0x0004455D
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x00046368 File Offset: 0x00044568
		internal static int CountNodes(GreenNode green)
		{
			int num = 0;
			int i = 0;
			int slotCount = green.SlotCount;
			while (i < slotCount)
			{
				GreenNode slot = green.GetSlot(i);
				if (slot != null)
				{
					if (!slot.IsList)
					{
						num++;
					}
					else
					{
						num += slot.SlotCount;
					}
				}
				i++;
			}
			return num;
		}

		// Token: 0x17000607 RID: 1543
		public SyntaxNodeOrToken this[int index]
		{
			get
			{
				if (index < this._count)
				{
					return ChildSyntaxList.ItemInternal(this._node, index);
				}
				throw new ArgumentOutOfRangeException("index");
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001788 RID: 6024 RVA: 0x000463CF File Offset: 0x000445CF
		[Nullable(2)]
		internal SyntaxNode Node
		{
			[NullableContext(2)]
			get
			{
				return this._node;
			}
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x000463D7 File Offset: 0x000445D7
		private static int Occupancy(GreenNode green)
		{
			if (!green.IsList)
			{
				return 1;
			}
			return green.SlotCount;
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x000463EC File Offset: 0x000445EC
		internal static SyntaxNodeOrToken ItemInternal(SyntaxNode node, int index)
		{
			GreenNode green = node.Green;
			int num = index;
			int num2 = 0;
			int num3 = node.Position;
			GreenNode greenNode;
			for (;;)
			{
				greenNode = green.GetSlot(num2);
				if (greenNode != null)
				{
					int num4 = ChildSyntaxList.Occupancy(greenNode);
					if (num < num4)
					{
						break;
					}
					num -= num4;
					num3 += greenNode.FullWidth;
				}
				num2++;
			}
			SyntaxNode nodeSlot = node.GetNodeSlot(num2);
			if (!greenNode.IsList)
			{
				if (nodeSlot != null)
				{
					return nodeSlot;
				}
			}
			else if (nodeSlot != null)
			{
				SyntaxNode nodeSlot2 = nodeSlot.GetNodeSlot(num);
				if (nodeSlot2 != null)
				{
					return nodeSlot2;
				}
				greenNode = greenNode.GetSlot(num);
				num3 = nodeSlot.GetChildPosition(num);
			}
			else
			{
				num3 += greenNode.GetSlotOffset(num);
				greenNode = greenNode.GetSlot(num);
			}
			return new SyntaxNodeOrToken(node, greenNode, num3, index);
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x000464A4 File Offset: 0x000446A4
		internal static SyntaxNodeOrToken ChildThatContainsPosition(SyntaxNode node, int targetPosition)
		{
			GreenNode greenNode = node.Green;
			int num = node.Position;
			int num2 = 0;
			int num3 = 0;
			GreenNode slot;
			for (;;)
			{
				slot = greenNode.GetSlot(num3);
				if (slot != null)
				{
					int num4 = num + slot.FullWidth;
					if (targetPosition < num4)
					{
						break;
					}
					num = num4;
					num2 += ChildSyntaxList.Occupancy(slot);
				}
				num3++;
			}
			greenNode = slot;
			SyntaxNode syntaxNode = node.GetNodeSlot(num3);
			if (!greenNode.IsList)
			{
				if (syntaxNode != null)
				{
					return syntaxNode;
				}
			}
			else
			{
				num3 = greenNode.FindSlotIndexContainingOffset(targetPosition - num);
				if (syntaxNode != null)
				{
					syntaxNode = syntaxNode.GetNodeSlot(num3);
					if (syntaxNode != null)
					{
						return syntaxNode;
					}
				}
				num += greenNode.GetSlotOffset(num3);
				greenNode = greenNode.GetSlot(num3);
				num2 += num3;
			}
			return new SyntaxNodeOrToken(node, greenNode, num, num2);
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x00046558 File Offset: 0x00044758
		[return: Nullable(2)]
		internal static SyntaxNode ItemInternalAsNode(SyntaxNode node, int index)
		{
			GreenNode green = node.Green;
			int num = index;
			int num2 = 0;
			GreenNode slot;
			for (;;)
			{
				slot = green.GetSlot(num2);
				if (slot != null)
				{
					int num3 = ChildSyntaxList.Occupancy(slot);
					if (num < num3)
					{
						break;
					}
					num -= num3;
				}
				num2++;
			}
			SyntaxNode nodeSlot = node.GetNodeSlot(num2);
			if (slot.IsList && nodeSlot != null)
			{
				return nodeSlot.GetNodeSlot(num);
			}
			return nodeSlot;
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x0600178D RID: 6029 RVA: 0x000465B3 File Offset: 0x000447B3
		private SyntaxNodeOrToken[] Nodes
		{
			get
			{
				return Enumerable.ToArray<SyntaxNodeOrToken>(this);
			}
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x000465C5 File Offset: 0x000447C5
		public bool Any()
		{
			return this._count != 0;
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x000465D0 File Offset: 0x000447D0
		public SyntaxNodeOrToken First()
		{
			if (this.Any())
			{
				return this[0];
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x000465E7 File Offset: 0x000447E7
		public SyntaxNodeOrToken Last()
		{
			if (this.Any())
			{
				return this[this._count - 1];
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x00046605 File Offset: 0x00044805
		public ChildSyntaxList.Reversed Reverse()
		{
			return new ChildSyntaxList.Reversed(this._node, this._count);
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x00046618 File Offset: 0x00044818
		public ChildSyntaxList.Enumerator GetEnumerator()
		{
			if (this._node == null)
			{
				return default(ChildSyntaxList.Enumerator);
			}
			return new ChildSyntaxList.Enumerator(this._node, this._count);
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x00046648 File Offset: 0x00044848
		IEnumerator<SyntaxNodeOrToken> IEnumerable<SyntaxNodeOrToken>.GetEnumerator()
		{
			if (this._node == null)
			{
				return SpecializedCollections.EmptyEnumerator<SyntaxNodeOrToken>();
			}
			return new ChildSyntaxList.EnumeratorImpl(this._node, this._count);
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x00046669 File Offset: 0x00044869
		IEnumerator IEnumerable.GetEnumerator()
		{
			if (this._node == null)
			{
				return SpecializedCollections.EmptyEnumerator<SyntaxNodeOrToken>();
			}
			return new ChildSyntaxList.EnumeratorImpl(this._node, this._count);
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x0004668C File Offset: 0x0004488C
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is ChildSyntaxList)
			{
				ChildSyntaxList childSyntaxList = (ChildSyntaxList)obj;
				return this.Equals(childSyntaxList);
			}
			return false;
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x000466B1 File Offset: 0x000448B1
		public bool Equals(ChildSyntaxList other)
		{
			return this._node == other._node;
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x000466C1 File Offset: 0x000448C1
		public override int GetHashCode()
		{
			SyntaxNode node = this._node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x000466D4 File Offset: 0x000448D4
		public static bool operator ==(ChildSyntaxList list1, ChildSyntaxList list2)
		{
			return list1.Equals(list2);
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x000466DE File Offset: 0x000448DE
		public static bool operator !=(ChildSyntaxList list1, ChildSyntaxList list2)
		{
			return !list1.Equals(list2);
		}

		// Token: 0x040009E8 RID: 2536
		[Nullable(2)]
		private readonly SyntaxNode _node;

		// Token: 0x040009E9 RID: 2537
		private readonly int _count;

		// Token: 0x020005F1 RID: 1521
		[Nullable(0)]
		public struct Enumerator
		{
			// Token: 0x060036F9 RID: 14073 RVA: 0x0009A3DF File Offset: 0x000985DF
			internal Enumerator(SyntaxNode node, int count)
			{
				this._node = node;
				this._count = count;
				this._childIndex = -1;
			}

			// Token: 0x060036FA RID: 14074 RVA: 0x0009A3F6 File Offset: 0x000985F6
			internal void InitializeFrom(SyntaxNode node)
			{
				this._node = node;
				this._count = ChildSyntaxList.CountNodes(node.Green);
				this._childIndex = -1;
			}

			// Token: 0x060036FB RID: 14075 RVA: 0x0009A418 File Offset: 0x00098618
			[MemberNotNullWhen(true, "_node")]
			public bool MoveNext()
			{
				int num = this._childIndex + 1;
				if (num < this._count)
				{
					this._childIndex = num;
					return true;
				}
				return false;
			}

			// Token: 0x17000F91 RID: 3985
			// (get) Token: 0x060036FC RID: 14076 RVA: 0x0009A441 File Offset: 0x00098641
			public SyntaxNodeOrToken Current
			{
				get
				{
					return ChildSyntaxList.ItemInternal(this._node, this._childIndex);
				}
			}

			// Token: 0x060036FD RID: 14077 RVA: 0x0009A454 File Offset: 0x00098654
			public void Reset()
			{
				this._childIndex = -1;
			}

			// Token: 0x060036FE RID: 14078 RVA: 0x0009A45D File Offset: 0x0009865D
			internal bool TryMoveNextAndGetCurrent(out SyntaxNodeOrToken current)
			{
				if (!this.MoveNext())
				{
					current = default(SyntaxNodeOrToken);
					return false;
				}
				current = ChildSyntaxList.ItemInternal(this._node, this._childIndex);
				return true;
			}

			// Token: 0x060036FF RID: 14079 RVA: 0x0009A488 File Offset: 0x00098688
			[NullableContext(2)]
			internal SyntaxNode TryMoveNextAndGetCurrentAsNode()
			{
				while (this.MoveNext())
				{
					SyntaxNode syntaxNode = ChildSyntaxList.ItemInternalAsNode(this._node, this._childIndex);
					if (syntaxNode != null)
					{
						return syntaxNode;
					}
				}
				return null;
			}

			// Token: 0x04001734 RID: 5940
			[Nullable(2)]
			private SyntaxNode _node;

			// Token: 0x04001735 RID: 5941
			private int _count;

			// Token: 0x04001736 RID: 5942
			private int _childIndex;
		}

		// Token: 0x020005F2 RID: 1522
		[NullableContext(0)]
		private class EnumeratorImpl : IEnumerator<SyntaxNodeOrToken>, IEnumerator, IDisposable
		{
			// Token: 0x06003700 RID: 14080 RVA: 0x0009A4B7 File Offset: 0x000986B7
			[NullableContext(1)]
			internal EnumeratorImpl(SyntaxNode node, int count)
			{
				this._enumerator = new ChildSyntaxList.Enumerator(node, count);
			}

			// Token: 0x17000F92 RID: 3986
			// (get) Token: 0x06003701 RID: 14081 RVA: 0x0009A4CC File Offset: 0x000986CC
			public SyntaxNodeOrToken Current
			{
				get
				{
					return this._enumerator.Current;
				}
			}

			// Token: 0x17000F93 RID: 3987
			// (get) Token: 0x06003702 RID: 14082 RVA: 0x0009A4D9 File Offset: 0x000986D9
			[Nullable(1)]
			object IEnumerator.Current
			{
				[NullableContext(1)]
				get
				{
					return this._enumerator.Current;
				}
			}

			// Token: 0x06003703 RID: 14083 RVA: 0x0009A4EB File Offset: 0x000986EB
			public bool MoveNext()
			{
				return this._enumerator.MoveNext();
			}

			// Token: 0x06003704 RID: 14084 RVA: 0x0009A4F8 File Offset: 0x000986F8
			public void Reset()
			{
				this._enumerator.Reset();
			}

			// Token: 0x06003705 RID: 14085 RVA: 0x0009A505 File Offset: 0x00098705
			public void Dispose()
			{
			}

			// Token: 0x04001737 RID: 5943
			private ChildSyntaxList.Enumerator _enumerator;
		}

		// Token: 0x020005F3 RID: 1523
		[NullableContext(0)]
		public readonly struct Reversed : IEnumerable<SyntaxNodeOrToken>, IEnumerable, IEquatable<ChildSyntaxList.Reversed>
		{
			// Token: 0x06003706 RID: 14086 RVA: 0x0009A507 File Offset: 0x00098707
			[NullableContext(1)]
			internal Reversed(SyntaxNode node, int count)
			{
				this._node = node;
				this._count = count;
			}

			// Token: 0x06003707 RID: 14087 RVA: 0x0009A517 File Offset: 0x00098717
			public ChildSyntaxList.Reversed.Enumerator GetEnumerator()
			{
				return new ChildSyntaxList.Reversed.Enumerator(this._node, this._count);
			}

			// Token: 0x06003708 RID: 14088 RVA: 0x0009A52A File Offset: 0x0009872A
			[NullableContext(1)]
			IEnumerator<SyntaxNodeOrToken> IEnumerable<SyntaxNodeOrToken>.GetEnumerator()
			{
				if (this._node == null)
				{
					return SpecializedCollections.EmptyEnumerator<SyntaxNodeOrToken>();
				}
				return new ChildSyntaxList.Reversed.EnumeratorImpl(this._node, this._count);
			}

			// Token: 0x06003709 RID: 14089 RVA: 0x0009A54B File Offset: 0x0009874B
			[NullableContext(1)]
			IEnumerator IEnumerable.GetEnumerator()
			{
				if (this._node == null)
				{
					return SpecializedCollections.EmptyEnumerator<SyntaxNodeOrToken>();
				}
				return new ChildSyntaxList.Reversed.EnumeratorImpl(this._node, this._count);
			}

			// Token: 0x0600370A RID: 14090 RVA: 0x0009A56C File Offset: 0x0009876C
			public override int GetHashCode()
			{
				if (this._node == null)
				{
					return 0;
				}
				return Hash.Combine(this._node.GetHashCode(), this._count);
			}

			// Token: 0x0600370B RID: 14091 RVA: 0x0009A590 File Offset: 0x00098790
			[NullableContext(2)]
			public override bool Equals(object obj)
			{
				if (obj is ChildSyntaxList.Reversed)
				{
					ChildSyntaxList.Reversed reversed = (ChildSyntaxList.Reversed)obj;
					return this.Equals(reversed);
				}
				return false;
			}

			// Token: 0x0600370C RID: 14092 RVA: 0x0009A5B5 File Offset: 0x000987B5
			public bool Equals(ChildSyntaxList.Reversed other)
			{
				return this._node == other._node && this._count == other._count;
			}

			// Token: 0x04001738 RID: 5944
			[Nullable(2)]
			private readonly SyntaxNode _node;

			// Token: 0x04001739 RID: 5945
			private readonly int _count;

			// Token: 0x020007BF RID: 1983
			public struct Enumerator
			{
				// Token: 0x06003FCF RID: 16335 RVA: 0x000B264C File Offset: 0x000B084C
				[NullableContext(1)]
				internal Enumerator(SyntaxNode node, int count)
				{
					this._node = node;
					this._count = count;
					this._childIndex = count;
				}

				// Token: 0x06003FD0 RID: 16336 RVA: 0x000B2664 File Offset: 0x000B0864
				[MemberNotNullWhen(true, "_node")]
				public bool MoveNext()
				{
					int num = this._childIndex - 1;
					this._childIndex = num;
					return num >= 0;
				}

				// Token: 0x17001185 RID: 4485
				// (get) Token: 0x06003FD1 RID: 16337 RVA: 0x000B2688 File Offset: 0x000B0888
				public SyntaxNodeOrToken Current
				{
					get
					{
						return ChildSyntaxList.ItemInternal(this._node, this._childIndex);
					}
				}

				// Token: 0x06003FD2 RID: 16338 RVA: 0x000B269B File Offset: 0x000B089B
				public void Reset()
				{
					this._childIndex = this._count;
				}

				// Token: 0x04001DD6 RID: 7638
				[Nullable(2)]
				private readonly SyntaxNode _node;

				// Token: 0x04001DD7 RID: 7639
				private readonly int _count;

				// Token: 0x04001DD8 RID: 7640
				private int _childIndex;
			}

			// Token: 0x020007C0 RID: 1984
			private class EnumeratorImpl : IEnumerator<SyntaxNodeOrToken>, IEnumerator, IDisposable
			{
				// Token: 0x06003FD3 RID: 16339 RVA: 0x000B26A9 File Offset: 0x000B08A9
				[NullableContext(1)]
				internal EnumeratorImpl(SyntaxNode node, int count)
				{
					this._enumerator = new ChildSyntaxList.Reversed.Enumerator(node, count);
				}

				// Token: 0x17001186 RID: 4486
				// (get) Token: 0x06003FD4 RID: 16340 RVA: 0x000B26BE File Offset: 0x000B08BE
				public SyntaxNodeOrToken Current
				{
					get
					{
						return this._enumerator.Current;
					}
				}

				// Token: 0x17001187 RID: 4487
				// (get) Token: 0x06003FD5 RID: 16341 RVA: 0x000B26CB File Offset: 0x000B08CB
				[Nullable(1)]
				object IEnumerator.Current
				{
					[NullableContext(1)]
					get
					{
						return this._enumerator.Current;
					}
				}

				// Token: 0x06003FD6 RID: 16342 RVA: 0x000B26DD File Offset: 0x000B08DD
				public bool MoveNext()
				{
					return this._enumerator.MoveNext();
				}

				// Token: 0x06003FD7 RID: 16343 RVA: 0x000B26EA File Offset: 0x000B08EA
				public void Reset()
				{
					this._enumerator.Reset();
				}

				// Token: 0x06003FD8 RID: 16344 RVA: 0x000B26F7 File Offset: 0x000B08F7
				public void Dispose()
				{
				}

				// Token: 0x04001DD9 RID: 7641
				private ChildSyntaxList.Reversed.Enumerator _enumerator;
			}
		}
	}
}
