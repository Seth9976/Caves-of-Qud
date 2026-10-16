using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Syntax.InternalSyntax
{
	// Token: 0x020002AE RID: 686
	[NullableContext(2)]
	[Nullable(0)]
	internal struct SeparatedSyntaxListBuilder<[Nullable(0)] TNode> where TNode : GreenNode
	{
		// Token: 0x06001D04 RID: 7428 RVA: 0x000550D3 File Offset: 0x000532D3
		public SeparatedSyntaxListBuilder(int size)
		{
			this = new SeparatedSyntaxListBuilder<TNode>(new SyntaxListBuilder(size));
		}

		// Token: 0x06001D05 RID: 7429 RVA: 0x000550E1 File Offset: 0x000532E1
		[return: Nullable(new byte[] { 0, 1 })]
		public static SeparatedSyntaxListBuilder<TNode> Create()
		{
			return new SeparatedSyntaxListBuilder<TNode>(8);
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x000550E9 File Offset: 0x000532E9
		[NullableContext(1)]
		internal SeparatedSyntaxListBuilder(SyntaxListBuilder builder)
		{
			this._builder = builder;
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06001D07 RID: 7431 RVA: 0x000550F2 File Offset: 0x000532F2
		public bool IsNull
		{
			get
			{
				return this._builder == null;
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06001D08 RID: 7432 RVA: 0x000550FD File Offset: 0x000532FD
		public int Count
		{
			get
			{
				return this._builder.Count;
			}
		}

		// Token: 0x170007AC RID: 1964
		public GreenNode this[int index]
		{
			get
			{
				return this._builder[index];
			}
			set
			{
				this._builder[index] = value;
			}
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x00055127 File Offset: 0x00053327
		public void Clear()
		{
			this._builder.Clear();
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x00055134 File Offset: 0x00053334
		public void RemoveLast()
		{
			this._builder.RemoveLast();
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x00055141 File Offset: 0x00053341
		[NullableContext(1)]
		[return: Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxListBuilder<TNode> Add(TNode node)
		{
			this._builder.Add(node);
			return this;
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x0005515A File Offset: 0x0005335A
		[NullableContext(1)]
		public void AddSeparator(GreenNode separatorToken)
		{
			this._builder.Add(separatorToken);
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x00055168 File Offset: 0x00053368
		[NullableContext(1)]
		public void AddRange(TNode[] items, int offset, int length)
		{
			this._builder.AddRange(items, offset, length);
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x00055188 File Offset: 0x00053388
		public void AddRange([Nullable(new byte[] { 0, 1 })] in SeparatedSyntaxList<TNode> nodes)
		{
			SyntaxListBuilder builder = this._builder;
			SeparatedSyntaxList<TNode> separatedSyntaxList = nodes;
			builder.AddRange(separatedSyntaxList.GetWithSeparators());
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x000551B0 File Offset: 0x000533B0
		public void AddRange([Nullable(new byte[] { 0, 1 })] in SeparatedSyntaxList<TNode> nodes, int count)
		{
			SeparatedSyntaxList<TNode> separatedSyntaxList = nodes;
			SyntaxList<GreenNode> withSeparators = separatedSyntaxList.GetWithSeparators();
			this._builder.AddRange(withSeparators, this.Count, Math.Min(count * 2, withSeparators.Count));
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x000551ED File Offset: 0x000533ED
		public bool Any(int kind)
		{
			return this._builder.Any(kind);
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x000551FC File Offset: 0x000533FC
		[return: Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<TNode> ToList()
		{
			if (this._builder != null)
			{
				return new SeparatedSyntaxList<TNode>(new SyntaxList<GreenNode>(this._builder.ToListNode()));
			}
			return default(SeparatedSyntaxList<TNode>);
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06001D14 RID: 7444 RVA: 0x00055230 File Offset: 0x00053430
		internal SyntaxListBuilder UnderlyingBuilder
		{
			get
			{
				return this._builder;
			}
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x00055238 File Offset: 0x00053438
		[return: Nullable(new byte[] { 0, 1 })]
		public static implicit operator SeparatedSyntaxList<TNode>([Nullable(new byte[] { 0, 1 })] in SeparatedSyntaxListBuilder<TNode> builder)
		{
			SeparatedSyntaxListBuilder<TNode> separatedSyntaxListBuilder = builder;
			return separatedSyntaxListBuilder.ToList();
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x00055253 File Offset: 0x00053453
		public static implicit operator SyntaxListBuilder([Nullable(new byte[] { 0, 1 })] in SeparatedSyntaxListBuilder<TNode> builder)
		{
			return builder._builder;
		}

		// Token: 0x04000D63 RID: 3427
		private readonly SyntaxListBuilder _builder;
	}
}
