using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.Syntax
{
	// Token: 0x020002A4 RID: 676
	[NullableContext(1)]
	[Nullable(0)]
	internal class SyntaxListBuilder
	{
		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06001C94 RID: 7316 RVA: 0x00053F92 File Offset: 0x00052192
		// (set) Token: 0x06001C95 RID: 7317 RVA: 0x00053F9A File Offset: 0x0005219A
		public int Count { get; private set; }

		// Token: 0x06001C96 RID: 7318 RVA: 0x00053FA3 File Offset: 0x000521A3
		public SyntaxListBuilder(int size)
		{
			this._nodes = new ArrayElement<GreenNode>[size];
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x00053FB7 File Offset: 0x000521B7
		public void Clear()
		{
			this.Count = 0;
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x00053FC0 File Offset: 0x000521C0
		public void Add(SyntaxNode item)
		{
			this.AddInternal(item.Green);
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x00053FD0 File Offset: 0x000521D0
		internal void AddInternal(GreenNode item)
		{
			if (item == null)
			{
				throw new ArgumentNullException();
			}
			if (this.Count >= this._nodes.Length)
			{
				this.Grow((this.Count == 0) ? 8 : (this._nodes.Length * 2));
			}
			ArrayElement<GreenNode>[] nodes = this._nodes;
			int count = this.Count;
			this.Count = count + 1;
			nodes[count].Value = item;
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x00054033 File Offset: 0x00052233
		public void AddRange(SyntaxNode[] items)
		{
			this.AddRange(items, 0, items.Length);
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x00054040 File Offset: 0x00052240
		public void AddRange(SyntaxNode[] items, int offset, int length)
		{
			if (this.Count + length > this._nodes.Length)
			{
				this.Grow(this.Count + length);
			}
			int i = offset;
			int num = this.Count;
			while (i < offset + length)
			{
				this._nodes[num].Value = items[i].Green;
				i++;
				num++;
			}
			int count = this.Count;
			this.Count += length;
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x000540B4 File Offset: 0x000522B4
		[Conditional("DEBUG")]
		private void Validate(int start, int end)
		{
			for (int i = start; i < end; i++)
			{
				if (this._nodes[i].Value == null)
				{
					throw new ArgumentException("Cannot add a null node.");
				}
			}
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x000540EB File Offset: 0x000522EB
		public void AddRange([Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxNode> list)
		{
			this.AddRange(list, 0, list.Count);
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x000540FC File Offset: 0x000522FC
		public void AddRange([Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxNode> list, int offset, int count)
		{
			if (this.Count + count > this._nodes.Length)
			{
				this.Grow(this.Count + count);
			}
			int num = this.Count;
			int i = offset;
			int num2 = offset + count;
			while (i < num2)
			{
				this._nodes[num].Value = list.ItemInternal(i).Green;
				num++;
				i++;
			}
			int count2 = this.Count;
			this.Count += count;
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x00054177 File Offset: 0x00052377
		[NullableContext(0)]
		public void AddRange<TNode>([Nullable(new byte[] { 0, 1 })] SyntaxList<TNode> list) where TNode : SyntaxNode
		{
			this.AddRange<TNode>(list, 0, list.Count);
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x00054188 File Offset: 0x00052388
		[NullableContext(0)]
		public void AddRange<TNode>([Nullable(new byte[] { 0, 1 })] SyntaxList<TNode> list, int offset, int count) where TNode : SyntaxNode
		{
			this.AddRange(new SyntaxList<SyntaxNode>(list.Node), offset, count);
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x0005419E File Offset: 0x0005239E
		public void AddRange(SyntaxNodeOrTokenList list)
		{
			this.AddRange(list, 0, list.Count);
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x000541B0 File Offset: 0x000523B0
		public void AddRange(SyntaxNodeOrTokenList list, int offset, int count)
		{
			if (this.Count + count > this._nodes.Length)
			{
				this.Grow(this.Count + count);
			}
			int num = this.Count;
			int i = offset;
			int num2 = offset + count;
			while (i < num2)
			{
				this._nodes[num].Value = list[i].UnderlyingNode;
				num++;
				i++;
			}
			int count2 = this.Count;
			this.Count += count;
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x0005422E File Offset: 0x0005242E
		public void AddRange(SyntaxTokenList list)
		{
			this.AddRange(list, 0, list.Count);
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x0005423F File Offset: 0x0005243F
		public void AddRange(SyntaxTokenList list, int offset, int length)
		{
			this.AddRange(new SyntaxList<SyntaxNode>(list.Node.CreateRed()), offset, length);
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x0005425C File Offset: 0x0005245C
		private void Grow(int size)
		{
			ArrayElement<GreenNode>[] array = new ArrayElement<GreenNode>[size];
			Array.Copy(this._nodes, array, this._nodes.Length);
			this._nodes = array;
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x0005428C File Offset: 0x0005248C
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

		// Token: 0x06001CA7 RID: 7335 RVA: 0x000542C8 File Offset: 0x000524C8
		[NullableContext(2)]
		internal GreenNode ToListNode()
		{
			switch (this.Count)
			{
			case 0:
				return null;
			case 1:
				return this._nodes[0].Value;
			case 2:
				return SyntaxList.List(this._nodes[0].Value, this._nodes[1].Value);
			case 3:
				return SyntaxList.List(this._nodes[0].Value, this._nodes[1].Value, this._nodes[2].Value);
			default:
			{
				ArrayElement<GreenNode>[] array = new ArrayElement<GreenNode>[this.Count];
				for (int i = 0; i < this.Count; i++)
				{
					array[i].Value = this._nodes[i].Value;
				}
				return SyntaxList.List(array);
			}
			}
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x000543AC File Offset: 0x000525AC
		[return: Nullable(new byte[] { 0, 1 })]
		public static implicit operator SyntaxList<SyntaxNode>(SyntaxListBuilder builder)
		{
			if (builder == null)
			{
				return default(SyntaxList<SyntaxNode>);
			}
			return builder.ToList();
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x000543CC File Offset: 0x000525CC
		internal void RemoveLast()
		{
			this.Count--;
			this._nodes[this.Count] = default(ArrayElement<GreenNode>);
		}

		// Token: 0x04000D56 RID: 3414
		[Nullable(new byte[] { 1, 0, 2 })]
		private ArrayElement<GreenNode>[] _nodes;
	}
}
