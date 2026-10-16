using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.Syntax
{
	// Token: 0x020002A8 RID: 680
	[NullableContext(1)]
	[Nullable(0)]
	internal class SyntaxTokenListBuilder
	{
		// Token: 0x06001CCC RID: 7372 RVA: 0x000548B4 File Offset: 0x00052AB4
		public SyntaxTokenListBuilder(int size)
		{
			this._nodes = new GreenNode[size];
			this._count = 0;
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x000548CF File Offset: 0x00052ACF
		public static SyntaxTokenListBuilder Create()
		{
			return new SyntaxTokenListBuilder(8);
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06001CCE RID: 7374 RVA: 0x000548D7 File Offset: 0x00052AD7
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x000548DF File Offset: 0x00052ADF
		public void Add(SyntaxToken item)
		{
			this.Add(item.Node);
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x000548F0 File Offset: 0x00052AF0
		internal void Add(GreenNode item)
		{
			this.CheckSpace(1);
			GreenNode[] nodes = this._nodes;
			int count = this._count;
			this._count = count + 1;
			nodes[count] = item;
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x0005491D File Offset: 0x00052B1D
		public void Add(SyntaxTokenList list)
		{
			this.Add(list, 0, list.Count);
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x0005492E File Offset: 0x00052B2E
		public void Add(SyntaxTokenList list, int offset, int length)
		{
			this.CheckSpace(length);
			list.CopyTo(offset, this._nodes, this._count, length);
			this._count += length;
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x0005495A File Offset: 0x00052B5A
		public void Add(SyntaxToken[] list)
		{
			this.Add(list, 0, list.Length);
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x00054968 File Offset: 0x00052B68
		public void Add(SyntaxToken[] list, int offset, int length)
		{
			this.CheckSpace(length);
			for (int i = 0; i < length; i++)
			{
				this._nodes[this._count + i] = list[offset + i].Node;
			}
			this._count += length;
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x000549B4 File Offset: 0x00052BB4
		private void CheckSpace(int delta)
		{
			int num = this._count + delta;
			if (num > this._nodes.Length)
			{
				this.Grow(num);
			}
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x000549DC File Offset: 0x00052BDC
		private void Grow(int newSize)
		{
			GreenNode[] array = new GreenNode[newSize];
			Array.Copy(this._nodes, array, this._nodes.Length);
			this._nodes = array;
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x00054A0C File Offset: 0x00052C0C
		public SyntaxTokenList ToList()
		{
			if (this._count <= 0)
			{
				return default(SyntaxTokenList);
			}
			switch (this._count)
			{
			case 1:
				return new SyntaxTokenList(null, this._nodes[0], 0, 0);
			case 2:
				return new SyntaxTokenList(null, SyntaxList.List(this._nodes[0], this._nodes[1]), 0, 0);
			case 3:
				return new SyntaxTokenList(null, SyntaxList.List(this._nodes[0], this._nodes[1], this._nodes[2]), 0, 0);
			default:
				return new SyntaxTokenList(null, SyntaxList.List(this._nodes, this._count), 0, 0);
			}
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x00054ABA File Offset: 0x00052CBA
		public static implicit operator SyntaxTokenList(SyntaxTokenListBuilder builder)
		{
			return builder.ToList();
		}

		// Token: 0x04000D5B RID: 3419
		[Nullable(new byte[] { 1, 2 })]
		private GreenNode[] _nodes;

		// Token: 0x04000D5C RID: 3420
		private int _count;
	}
}
