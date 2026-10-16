using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Syntax.InternalSyntax
{
	// Token: 0x020002B4 RID: 692
	internal class SyntaxListPool
	{
		// Token: 0x06001D54 RID: 7508 RVA: 0x00055943 File Offset: 0x00053B43
		internal SyntaxListPool()
		{
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x00055958 File Offset: 0x00053B58
		[NullableContext(1)]
		internal SyntaxListBuilder Allocate()
		{
			SyntaxListBuilder syntaxListBuilder;
			if (this._freeIndex > 0)
			{
				this._freeIndex--;
				syntaxListBuilder = this._freeList[this._freeIndex].Value;
				this._freeList[this._freeIndex].Value = null;
			}
			else
			{
				syntaxListBuilder = new SyntaxListBuilder(10);
			}
			return syntaxListBuilder;
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x000559B5 File Offset: 0x00053BB5
		[return: Nullable(new byte[] { 0, 1 })]
		internal SyntaxListBuilder<TNode> Allocate<TNode>() where TNode : GreenNode
		{
			return new SyntaxListBuilder<TNode>(this.Allocate());
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x000559C2 File Offset: 0x00053BC2
		[return: Nullable(new byte[] { 0, 1 })]
		internal SeparatedSyntaxListBuilder<TNode> AllocateSeparated<TNode>() where TNode : GreenNode
		{
			return new SeparatedSyntaxListBuilder<TNode>(this.Allocate());
		}

		// Token: 0x06001D58 RID: 7512 RVA: 0x000559D0 File Offset: 0x00053BD0
		internal void Free<TNode>([Nullable(new byte[] { 0, 1 })] in SeparatedSyntaxListBuilder<TNode> item) where TNode : GreenNode
		{
			SeparatedSyntaxListBuilder<TNode> separatedSyntaxListBuilder = item;
			this.Free(separatedSyntaxListBuilder.UnderlyingBuilder);
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x000559F4 File Offset: 0x00053BF4
		[NullableContext(1)]
		internal void Free(SyntaxListBuilder item)
		{
			item.Clear();
			if (this._freeIndex >= this._freeList.Length)
			{
				this.Grow();
			}
			this._freeList[this._freeIndex].Value = item;
			this._freeIndex++;
		}

		// Token: 0x06001D5A RID: 7514 RVA: 0x00055A44 File Offset: 0x00053C44
		private void Grow()
		{
			ArrayElement<SyntaxListBuilder>[] array = new ArrayElement<SyntaxListBuilder>[this._freeList.Length * 2];
			Array.Copy(this._freeList, array, this._freeList.Length);
			this._freeList = array;
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x00055A7C File Offset: 0x00053C7C
		[return: Nullable(new byte[] { 0, 1 })]
		public SyntaxList<TNode> ToListAndFree<TNode>([Nullable(new byte[] { 0, 1 })] SyntaxListBuilder<TNode> item) where TNode : GreenNode
		{
			SyntaxList<TNode> syntaxList = item.ToList();
			this.Free(item);
			return syntaxList;
		}

		// Token: 0x06001D5C RID: 7516 RVA: 0x00055A94 File Offset: 0x00053C94
		[return: Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<TNode> ToListAndFree<TNode>([Nullable(new byte[] { 0, 1 })] in SeparatedSyntaxListBuilder<TNode> item) where TNode : GreenNode
		{
			SeparatedSyntaxListBuilder<TNode> separatedSyntaxListBuilder = item;
			SeparatedSyntaxList<TNode> separatedSyntaxList = separatedSyntaxListBuilder.ToList();
			this.Free<TNode>(in item);
			return separatedSyntaxList;
		}

		// Token: 0x04000D68 RID: 3432
		[Nullable(new byte[] { 1, 0, 2 })]
		private ArrayElement<SyntaxListBuilder>[] _freeList = new ArrayElement<SyntaxListBuilder>[10];

		// Token: 0x04000D69 RID: 3433
		private int _freeIndex;
	}
}
