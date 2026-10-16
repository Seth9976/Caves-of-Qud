using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Syntax.InternalSyntax
{
	// Token: 0x020002B3 RID: 691
	[NullableContext(1)]
	[Nullable(0)]
	internal struct SyntaxListBuilder<[Nullable(0)] TNode> where TNode : GreenNode
	{
		// Token: 0x06001D42 RID: 7490 RVA: 0x0005581A File Offset: 0x00053A1A
		public SyntaxListBuilder(int size)
		{
			this = new SyntaxListBuilder<TNode>(new SyntaxListBuilder(size));
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x00055828 File Offset: 0x00053A28
		[return: Nullable(new byte[] { 0, 1 })]
		public static SyntaxListBuilder<TNode> Create()
		{
			return new SyntaxListBuilder<TNode>(8);
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x00055830 File Offset: 0x00053A30
		internal SyntaxListBuilder(SyntaxListBuilder builder)
		{
			this._builder = builder;
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06001D45 RID: 7493 RVA: 0x00055839 File Offset: 0x00053A39
		public bool IsNull
		{
			get
			{
				return this._builder == null;
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06001D46 RID: 7494 RVA: 0x00055844 File Offset: 0x00053A44
		public int Count
		{
			get
			{
				return this._builder.Count;
			}
		}

		// Token: 0x170007B4 RID: 1972
		[Nullable(2)]
		public TNode this[int index]
		{
			[NullableContext(2)]
			get
			{
				return (TNode)((object)this._builder[index]);
			}
			[NullableContext(2)]
			set
			{
				this._builder[index] = value;
			}
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x00055878 File Offset: 0x00053A78
		public void Clear()
		{
			this._builder.Clear();
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x00055885 File Offset: 0x00053A85
		[return: Nullable(new byte[] { 0, 1 })]
		public SyntaxListBuilder<TNode> Add(TNode node)
		{
			this._builder.Add(node);
			return this;
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x000558A0 File Offset: 0x00053AA0
		public void AddRange(TNode[] items, int offset, int length)
		{
			this._builder.AddRange(items, offset, length);
		}

		// Token: 0x06001D4C RID: 7500 RVA: 0x000558BD File Offset: 0x00053ABD
		public void AddRange([Nullable(new byte[] { 0, 1 })] SyntaxList<TNode> nodes)
		{
			this._builder.AddRange<TNode>(nodes);
		}

		// Token: 0x06001D4D RID: 7501 RVA: 0x000558CB File Offset: 0x00053ACB
		public void AddRange([Nullable(new byte[] { 0, 1 })] SyntaxList<TNode> nodes, int offset, int length)
		{
			this._builder.AddRange<TNode>(nodes, offset, length);
		}

		// Token: 0x06001D4E RID: 7502 RVA: 0x000558DB File Offset: 0x00053ADB
		public bool Any(int kind)
		{
			return this._builder.Any(kind);
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x000558E9 File Offset: 0x00053AE9
		[return: Nullable(new byte[] { 0, 1 })]
		public SyntaxList<TNode> ToList()
		{
			return this._builder.ToList();
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x000558FB File Offset: 0x00053AFB
		[NullableContext(2)]
		public GreenNode ToListNode()
		{
			return this._builder.ToListNode();
		}

		// Token: 0x06001D51 RID: 7505 RVA: 0x00055908 File Offset: 0x00053B08
		public static implicit operator SyntaxListBuilder([Nullable(new byte[] { 0, 1 })] SyntaxListBuilder<TNode> builder)
		{
			return builder._builder;
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x00055910 File Offset: 0x00053B10
		[return: Nullable(new byte[] { 0, 1 })]
		public static implicit operator SyntaxList<TNode>([Nullable(new byte[] { 0, 1 })] SyntaxListBuilder<TNode> builder)
		{
			if (builder._builder != null)
			{
				return builder.ToList();
			}
			return default(SyntaxList<TNode>);
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x00055936 File Offset: 0x00053B36
		[NullableContext(0)]
		[return: Nullable(new byte[] { 0, 1 })]
		public SyntaxList<TDerived> ToList<TDerived>() where TDerived : GreenNode
		{
			return new SyntaxList<TDerived>(this.ToListNode());
		}

		// Token: 0x04000D67 RID: 3431
		private readonly SyntaxListBuilder _builder;
	}
}
