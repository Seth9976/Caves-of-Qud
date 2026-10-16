using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Syntax
{
	// Token: 0x020002A6 RID: 678
	[NullableContext(1)]
	[Nullable(0)]
	internal struct SyntaxListBuilder<[Nullable(0)] TNode> where TNode : SyntaxNode
	{
		// Token: 0x06001CAE RID: 7342 RVA: 0x000544C4 File Offset: 0x000526C4
		public SyntaxListBuilder(int size)
		{
			this = new SyntaxListBuilder<TNode>(new SyntaxListBuilder(size));
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x000544D2 File Offset: 0x000526D2
		[return: Nullable(new byte[] { 0, 1 })]
		public static SyntaxListBuilder<TNode> Create()
		{
			return new SyntaxListBuilder<TNode>(8);
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x000544DA File Offset: 0x000526DA
		[NullableContext(2)]
		internal SyntaxListBuilder(SyntaxListBuilder builder)
		{
			this._builder = builder;
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06001CB1 RID: 7345 RVA: 0x000544E3 File Offset: 0x000526E3
		public bool IsNull
		{
			get
			{
				return this._builder == null;
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06001CB2 RID: 7346 RVA: 0x000544EE File Offset: 0x000526EE
		public int Count
		{
			get
			{
				return this._builder.Count;
			}
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x000544FB File Offset: 0x000526FB
		public void Clear()
		{
			this._builder.Clear();
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x00054508 File Offset: 0x00052708
		[return: Nullable(new byte[] { 0, 1 })]
		public SyntaxListBuilder<TNode> Add(TNode node)
		{
			this._builder.Add(node);
			return this;
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x00054524 File Offset: 0x00052724
		public void AddRange(TNode[] items, int offset, int length)
		{
			this._builder.AddRange(items, offset, length);
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x00054541 File Offset: 0x00052741
		public void AddRange([Nullable(new byte[] { 0, 1 })] SyntaxList<TNode> nodes)
		{
			this._builder.AddRange<TNode>(nodes);
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x0005454F File Offset: 0x0005274F
		public void AddRange([Nullable(new byte[] { 0, 1 })] SyntaxList<TNode> nodes, int offset, int length)
		{
			this._builder.AddRange<TNode>(nodes, offset, length);
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x0005455F File Offset: 0x0005275F
		public bool Any(int kind)
		{
			return this._builder.Any(kind);
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x0005456D File Offset: 0x0005276D
		[return: Nullable(new byte[] { 0, 1 })]
		public SyntaxList<TNode> ToList()
		{
			return this._builder.ToList();
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x0005457F File Offset: 0x0005277F
		[NullableContext(2)]
		public static implicit operator SyntaxListBuilder([Nullable(new byte[] { 0, 1 })] SyntaxListBuilder<TNode> builder)
		{
			return builder._builder;
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x00054588 File Offset: 0x00052788
		[return: Nullable(new byte[] { 0, 1 })]
		public static implicit operator SyntaxList<TNode>([Nullable(new byte[] { 0, 1 })] SyntaxListBuilder<TNode> builder)
		{
			if (builder._builder != null)
			{
				return builder.ToList();
			}
			return default(SyntaxList<TNode>);
		}

		// Token: 0x04000D58 RID: 3416
		[Nullable(2)]
		private readonly SyntaxListBuilder _builder;
	}
}
