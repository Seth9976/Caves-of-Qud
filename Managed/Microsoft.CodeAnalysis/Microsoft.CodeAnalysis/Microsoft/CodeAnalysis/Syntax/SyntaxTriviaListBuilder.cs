using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.Syntax
{
	// Token: 0x020002A9 RID: 681
	[NullableContext(1)]
	[Nullable(0)]
	internal class SyntaxTriviaListBuilder
	{
		// Token: 0x06001CD9 RID: 7385 RVA: 0x00054AC2 File Offset: 0x00052CC2
		public SyntaxTriviaListBuilder(int size)
		{
			this._nodes = new SyntaxTrivia[size];
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x00054AD6 File Offset: 0x00052CD6
		public static SyntaxTriviaListBuilder Create()
		{
			return new SyntaxTriviaListBuilder(4);
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x00054AE0 File Offset: 0x00052CE0
		[NullableContext(2)]
		public static SyntaxTriviaList Create(IEnumerable<SyntaxTrivia> trivia)
		{
			if (trivia == null)
			{
				return default(SyntaxTriviaList);
			}
			SyntaxTriviaListBuilder syntaxTriviaListBuilder = SyntaxTriviaListBuilder.Create();
			syntaxTriviaListBuilder.AddRange(trivia);
			return syntaxTriviaListBuilder.ToList();
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06001CDC RID: 7388 RVA: 0x00054B0B File Offset: 0x00052D0B
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x00054B13 File Offset: 0x00052D13
		public void Clear()
		{
			this._count = 0;
		}

		// Token: 0x170007A1 RID: 1953
		public SyntaxTrivia this[int index]
		{
			get
			{
				if (index < 0 || index > this._count)
				{
					throw new IndexOutOfRangeException();
				}
				return this._nodes[index];
			}
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x00054B40 File Offset: 0x00052D40
		[NullableContext(2)]
		public void AddRange(IEnumerable<SyntaxTrivia> items)
		{
			if (items != null)
			{
				foreach (SyntaxTrivia syntaxTrivia in items)
				{
					this.Add(syntaxTrivia);
				}
			}
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x00054B8C File Offset: 0x00052D8C
		public SyntaxTriviaListBuilder Add(SyntaxTrivia item)
		{
			if (this._count >= this._nodes.Length)
			{
				this.Grow((this._count == 0) ? 8 : (this._nodes.Length * 2));
			}
			SyntaxTrivia[] nodes = this._nodes;
			int count = this._count;
			this._count = count + 1;
			nodes[count] = item;
			return this;
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x00054BE2 File Offset: 0x00052DE2
		public void Add(SyntaxTrivia[] items)
		{
			this.Add(items, 0, items.Length);
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x00054BF0 File Offset: 0x00052DF0
		public void Add(SyntaxTrivia[] items, int offset, int length)
		{
			if (this._count + length > this._nodes.Length)
			{
				this.Grow(this._count + length);
			}
			Array.Copy(items, offset, this._nodes, this._count, length);
			this._count += length;
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x00054C3F File Offset: 0x00052E3F
		public void Add(in SyntaxTriviaList list)
		{
			this.Add(in list, 0, list.Count);
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x00054C50 File Offset: 0x00052E50
		public void Add(in SyntaxTriviaList list, int offset, int length)
		{
			if (this._count + length > this._nodes.Length)
			{
				this.Grow(this._count + length);
			}
			list.CopyTo(offset, this._nodes, this._count, length);
			this._count += length;
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x00054CA0 File Offset: 0x00052EA0
		private void Grow(int size)
		{
			SyntaxTrivia[] array = new SyntaxTrivia[size];
			Array.Copy(this._nodes, array, this._nodes.Length);
			this._nodes = array;
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x00054CCF File Offset: 0x00052ECF
		public static implicit operator SyntaxTriviaList(SyntaxTriviaListBuilder builder)
		{
			return builder.ToList();
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x00054CD8 File Offset: 0x00052ED8
		public SyntaxTriviaList ToList()
		{
			if (this._count <= 0)
			{
				return default(SyntaxTriviaList);
			}
			switch (this._count)
			{
			case 1:
			{
				SyntaxToken syntaxToken = default(SyntaxToken);
				return new SyntaxTriviaList(in syntaxToken, this._nodes[0].UnderlyingNode, 0, 0);
			}
			case 2:
			{
				SyntaxToken syntaxToken = default(SyntaxToken);
				return new SyntaxTriviaList(in syntaxToken, SyntaxList.List(this._nodes[0].UnderlyingNode, this._nodes[1].UnderlyingNode), 0, 0);
			}
			case 3:
			{
				SyntaxToken syntaxToken = default(SyntaxToken);
				return new SyntaxTriviaList(in syntaxToken, SyntaxList.List(this._nodes[0].UnderlyingNode, this._nodes[1].UnderlyingNode, this._nodes[2].UnderlyingNode), 0, 0);
			}
			default:
			{
				ArrayElement<GreenNode>[] array = new ArrayElement<GreenNode>[this._count];
				for (int i = 0; i < this._count; i++)
				{
					array[i].Value = this._nodes[i].UnderlyingNode;
				}
				SyntaxToken syntaxToken = default(SyntaxToken);
				return new SyntaxTriviaList(in syntaxToken, SyntaxList.List(array), 0, 0);
			}
			}
		}

		// Token: 0x04000D5D RID: 3421
		private SyntaxTrivia[] _nodes;

		// Token: 0x04000D5E RID: 3422
		private int _count;
	}
}
