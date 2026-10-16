using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Syntax.InternalSyntax
{
	// Token: 0x020002B1 RID: 689
	[NullableContext(2)]
	[Nullable(0)]
	internal class SyntaxListBuilder
	{
		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06001D2A RID: 7466 RVA: 0x00055474 File Offset: 0x00053674
		// (set) Token: 0x06001D2B RID: 7467 RVA: 0x0005547C File Offset: 0x0005367C
		public int Count { get; private set; }

		// Token: 0x06001D2C RID: 7468 RVA: 0x00055485 File Offset: 0x00053685
		public SyntaxListBuilder(int size)
		{
			this._nodes = new ArrayElement<GreenNode>[size];
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x00055499 File Offset: 0x00053699
		[NullableContext(1)]
		public static SyntaxListBuilder Create()
		{
			return new SyntaxListBuilder(8);
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x000554A1 File Offset: 0x000536A1
		public void Clear()
		{
			this.Count = 0;
		}

		// Token: 0x170007B1 RID: 1969
		public GreenNode this[int index]
		{
			get
			{
				return this._nodes[index];
			}
			set
			{
				this._nodes[index].Value = value;
			}
		}

		// Token: 0x06001D31 RID: 7473 RVA: 0x000554D4 File Offset: 0x000536D4
		public void Add(GreenNode item)
		{
			if (item == null)
			{
				return;
			}
			if (item.IsList)
			{
				int slotCount = item.SlotCount;
				this.EnsureAdditionalCapacity(slotCount);
				for (int i = 0; i < slotCount; i++)
				{
					this.Add(item.GetSlot(i));
				}
				return;
			}
			this.EnsureAdditionalCapacity(1);
			ArrayElement<GreenNode>[] nodes = this._nodes;
			int count = this.Count;
			this.Count = count + 1;
			nodes[count].Value = item;
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x0005553E File Offset: 0x0005373E
		[NullableContext(1)]
		public void AddRange(GreenNode[] items)
		{
			this.AddRange(items, 0, items.Length);
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x0005554C File Offset: 0x0005374C
		[NullableContext(1)]
		public void AddRange(GreenNode[] items, int offset, int length)
		{
			this.EnsureAdditionalCapacity(length - offset);
			int count = this.Count;
			for (int i = offset; i < length; i++)
			{
				this.Add(items[i]);
			}
		}

		// Token: 0x06001D34 RID: 7476 RVA: 0x00055580 File Offset: 0x00053780
		[Conditional("DEBUG")]
		private void Validate(int start, int end)
		{
			for (int i = start; i < end; i++)
			{
			}
		}

		// Token: 0x06001D35 RID: 7477 RVA: 0x00055599 File Offset: 0x00053799
		public void AddRange([Nullable(new byte[] { 0, 1 })] SyntaxList<GreenNode> list)
		{
			this.AddRange(list, 0, list.Count);
		}

		// Token: 0x06001D36 RID: 7478 RVA: 0x000555AC File Offset: 0x000537AC
		public void AddRange([Nullable(new byte[] { 0, 1 })] SyntaxList<GreenNode> list, int offset, int length)
		{
			this.EnsureAdditionalCapacity(length - offset);
			int count = this.Count;
			for (int i = offset; i < length; i++)
			{
				this.Add(list[i]);
			}
		}

		// Token: 0x06001D37 RID: 7479 RVA: 0x000555E3 File Offset: 0x000537E3
		[NullableContext(0)]
		public void AddRange<TNode>([Nullable(new byte[] { 0, 1 })] SyntaxList<TNode> list) where TNode : GreenNode
		{
			this.AddRange<TNode>(list, 0, list.Count);
		}

		// Token: 0x06001D38 RID: 7480 RVA: 0x000555F4 File Offset: 0x000537F4
		[NullableContext(0)]
		public void AddRange<TNode>([Nullable(new byte[] { 0, 1 })] SyntaxList<TNode> list, int offset, int length) where TNode : GreenNode
		{
			this.AddRange(new SyntaxList<GreenNode>(list.Node), offset, length);
		}

		// Token: 0x06001D39 RID: 7481 RVA: 0x0005560C File Offset: 0x0005380C
		public void RemoveLast()
		{
			int count = this.Count;
			this.Count = count - 1;
			this._nodes[this.Count].Value = null;
		}

		// Token: 0x06001D3A RID: 7482 RVA: 0x00055640 File Offset: 0x00053840
		private void EnsureAdditionalCapacity(int additionalCount)
		{
			int num = this._nodes.Length;
			int num2 = this.Count + additionalCount;
			if (num2 <= num)
			{
				return;
			}
			int num3 = ((num2 < 8) ? 8 : ((num2 >= 1073741823) ? int.MaxValue : Math.Max(num2, num * 2)));
			Array.Resize<ArrayElement<GreenNode>>(ref this._nodes, num3);
		}

		// Token: 0x06001D3B RID: 7483 RVA: 0x00055690 File Offset: 0x00053890
		public bool Any(int kind)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (this._nodes[i].Value.RawKind == kind)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001D3C RID: 7484 RVA: 0x000556CC File Offset: 0x000538CC
		[NullableContext(1)]
		public GreenNode[] ToArray()
		{
			GreenNode[] array = new GreenNode[this.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this._nodes[i];
			}
			return array;
		}

		// Token: 0x06001D3D RID: 7485 RVA: 0x00055708 File Offset: 0x00053908
		internal GreenNode ToListNode()
		{
			switch (this.Count)
			{
			case 0:
				return null;
			case 1:
				return this._nodes[0];
			case 2:
				return SyntaxList.List(this._nodes[0], this._nodes[1]);
			case 3:
				return SyntaxList.List(this._nodes[0], this._nodes[1], this._nodes[2]);
			default:
			{
				ArrayElement<GreenNode>[] array = new ArrayElement<GreenNode>[this.Count];
				Array.Copy(this._nodes, array, this.Count);
				return SyntaxList.List(array);
			}
			}
		}

		// Token: 0x06001D3E RID: 7486 RVA: 0x000557CD File Offset: 0x000539CD
		[return: Nullable(new byte[] { 0, 1 })]
		public SyntaxList<GreenNode> ToList()
		{
			return new SyntaxList<GreenNode>(this.ToListNode());
		}

		// Token: 0x06001D3F RID: 7487 RVA: 0x000557DA File Offset: 0x000539DA
		[NullableContext(0)]
		[return: Nullable(new byte[] { 0, 1 })]
		public SyntaxList<TNode> ToList<TNode>() where TNode : GreenNode
		{
			return new SyntaxList<TNode>(this.ToListNode());
		}

		// Token: 0x04000D65 RID: 3429
		[Nullable(new byte[] { 1, 0, 2 })]
		private ArrayElement<GreenNode>[] _nodes;
	}
}
