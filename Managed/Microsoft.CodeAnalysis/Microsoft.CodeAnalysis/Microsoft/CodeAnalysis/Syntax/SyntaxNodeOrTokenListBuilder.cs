using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.Syntax
{
	// Token: 0x020002A7 RID: 679
	[NullableContext(1)]
	[Nullable(0)]
	internal class SyntaxNodeOrTokenListBuilder
	{
		// Token: 0x06001CBC RID: 7356 RVA: 0x000545AE File Offset: 0x000527AE
		public SyntaxNodeOrTokenListBuilder(int size)
		{
			this._nodes = new GreenNode[size];
			this._count = 0;
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x000545C9 File Offset: 0x000527C9
		public static SyntaxNodeOrTokenListBuilder Create()
		{
			return new SyntaxNodeOrTokenListBuilder(8);
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06001CBE RID: 7358 RVA: 0x000545D1 File Offset: 0x000527D1
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x000545D9 File Offset: 0x000527D9
		public void Clear()
		{
			this._count = 0;
		}

		// Token: 0x1700079E RID: 1950
		public SyntaxNodeOrToken this[int index]
		{
			get
			{
				GreenNode greenNode = this._nodes[index];
				if (greenNode.IsToken)
				{
					return new SyntaxNodeOrToken(null, greenNode, 0, 0);
				}
				return greenNode.CreateRed();
			}
			set
			{
				this._nodes[index] = value.UnderlyingNode;
			}
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x00054628 File Offset: 0x00052828
		internal void Add(GreenNode item)
		{
			if (this._count >= this._nodes.Length)
			{
				this.Grow((this._count == 0) ? 8 : (this._nodes.Length * 2));
			}
			GreenNode[] nodes = this._nodes;
			int count = this._count;
			this._count = count + 1;
			nodes[count] = item;
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x00054679 File Offset: 0x00052879
		public void Add(SyntaxNode item)
		{
			this.Add(item.Green);
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x00054687 File Offset: 0x00052887
		public void Add(in SyntaxToken item)
		{
			this.Add(item.Node);
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x00054695 File Offset: 0x00052895
		public void Add(in SyntaxNodeOrToken item)
		{
			this.Add(item.UnderlyingNode);
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x000546A3 File Offset: 0x000528A3
		public void Add(SyntaxNodeOrTokenList list)
		{
			this.Add(list, 0, list.Count);
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x000546B4 File Offset: 0x000528B4
		public void Add(SyntaxNodeOrTokenList list, int offset, int length)
		{
			if (this._count + length > this._nodes.Length)
			{
				this.Grow(this._count + length);
			}
			list.CopyTo(offset, this._nodes, this._count, length);
			this._count += length;
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x00054704 File Offset: 0x00052904
		public void Add(IEnumerable<SyntaxNodeOrToken> nodeOrTokens)
		{
			foreach (SyntaxNodeOrToken syntaxNodeOrToken in nodeOrTokens)
			{
				this.Add(in syntaxNodeOrToken);
			}
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x00054750 File Offset: 0x00052950
		internal void RemoveLast()
		{
			this._count--;
			this._nodes[this._count] = null;
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x00054770 File Offset: 0x00052970
		private void Grow(int size)
		{
			GreenNode[] array = new GreenNode[size];
			Array.Copy(this._nodes, array, this._nodes.Length);
			this._nodes = array;
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x000547A0 File Offset: 0x000529A0
		public SyntaxNodeOrTokenList ToList()
		{
			if (this._count <= 0)
			{
				return default(SyntaxNodeOrTokenList);
			}
			switch (this._count)
			{
			case 1:
				if (this._nodes[0].IsToken)
				{
					return new SyntaxNodeOrTokenList(SyntaxList.List(new GreenNode[] { this._nodes[0] }).CreateRed(), 0);
				}
				return new SyntaxNodeOrTokenList(this._nodes[0].CreateRed(), 0);
			case 2:
				return new SyntaxNodeOrTokenList(SyntaxList.List(this._nodes[0], this._nodes[1]).CreateRed(), 0);
			case 3:
				return new SyntaxNodeOrTokenList(SyntaxList.List(this._nodes[0], this._nodes[1], this._nodes[2]).CreateRed(), 0);
			default:
			{
				ArrayElement<GreenNode>[] array = new ArrayElement<GreenNode>[this._count];
				for (int i = 0; i < this._count; i++)
				{
					array[i].Value = this._nodes[i];
				}
				return new SyntaxNodeOrTokenList(SyntaxList.List(array).CreateRed(), 0);
			}
			}
		}

		// Token: 0x04000D59 RID: 3417
		[Nullable(new byte[] { 1, 2 })]
		private GreenNode[] _nodes;

		// Token: 0x04000D5A RID: 3418
		private int _count;
	}
}
