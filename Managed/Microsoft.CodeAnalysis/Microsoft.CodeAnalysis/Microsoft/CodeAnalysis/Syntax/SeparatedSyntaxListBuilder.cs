using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Syntax
{
	// Token: 0x020002A2 RID: 674
	[NullableContext(1)]
	[Nullable(0)]
	internal struct SeparatedSyntaxListBuilder<[Nullable(0)] TNode> where TNode : SyntaxNode
	{
		// Token: 0x06001C77 RID: 7287 RVA: 0x00053D42 File Offset: 0x00051F42
		public SeparatedSyntaxListBuilder(int size)
		{
			this = new SeparatedSyntaxListBuilder<TNode>(new SyntaxListBuilder(size));
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x00053D50 File Offset: 0x00051F50
		[return: Nullable(new byte[] { 0, 1 })]
		public static SeparatedSyntaxListBuilder<TNode> Create()
		{
			return new SeparatedSyntaxListBuilder<TNode>(8);
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x00053D58 File Offset: 0x00051F58
		internal SeparatedSyntaxListBuilder(SyntaxListBuilder builder)
		{
			this._builder = builder;
			this._expectedSeparator = false;
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06001C7A RID: 7290 RVA: 0x00053D68 File Offset: 0x00051F68
		public bool IsNull
		{
			get
			{
				return this._builder == null;
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06001C7B RID: 7291 RVA: 0x00053D73 File Offset: 0x00051F73
		public int Count
		{
			get
			{
				return this._builder.Count;
			}
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x00053D80 File Offset: 0x00051F80
		public void Clear()
		{
			this._builder.Clear();
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x00053D8D File Offset: 0x00051F8D
		private void CheckExpectedElement()
		{
			if (this._expectedSeparator)
			{
				throw new InvalidOperationException(CodeAnalysisResources.SeparatorIsExpected);
			}
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x00053DA2 File Offset: 0x00051FA2
		private void CheckExpectedSeparator()
		{
			if (!this._expectedSeparator)
			{
				throw new InvalidOperationException(CodeAnalysisResources.ElementIsExpected);
			}
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x00053DB7 File Offset: 0x00051FB7
		[return: Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxListBuilder<TNode> Add(TNode node)
		{
			this.CheckExpectedElement();
			this._expectedSeparator = true;
			this._builder.Add(node);
			return this;
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x00053DDD File Offset: 0x00051FDD
		[return: Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxListBuilder<TNode> AddSeparator(in SyntaxToken separatorToken)
		{
			this.CheckExpectedSeparator();
			this._expectedSeparator = false;
			this._builder.AddInternal(separatorToken.Node);
			return this;
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x00053E04 File Offset: 0x00052004
		[return: Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxListBuilder<TNode> AddRange([Nullable(new byte[] { 0, 1 })] in SeparatedSyntaxList<TNode> nodes)
		{
			this.CheckExpectedElement();
			SyntaxNodeOrTokenList withSeparators = nodes.GetWithSeparators();
			this._builder.AddRange(withSeparators);
			this._expectedSeparator = (this._builder.Count & 1) != 0;
			return this;
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x00053E48 File Offset: 0x00052048
		[return: Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxListBuilder<TNode> AddRange([Nullable(new byte[] { 0, 1 })] in SeparatedSyntaxList<TNode> nodes, int count)
		{
			this.CheckExpectedElement();
			SyntaxNodeOrTokenList withSeparators = nodes.GetWithSeparators();
			this._builder.AddRange(withSeparators, this.Count, Math.Min(count << 1, withSeparators.Count));
			this._expectedSeparator = (this._builder.Count & 1) != 0;
			return this;
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x00053EA0 File Offset: 0x000520A0
		[return: Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<TNode> ToList()
		{
			if (this._builder == null)
			{
				return default(SeparatedSyntaxList<TNode>);
			}
			return this._builder.ToSeparatedList<TNode>();
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x00053ECC File Offset: 0x000520CC
		[NullableContext(0)]
		[return: Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<TDerived> ToList<TDerived>() where TDerived : TNode
		{
			if (this._builder == null)
			{
				return default(SeparatedSyntaxList<TDerived>);
			}
			return this._builder.ToSeparatedList<TDerived>();
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x00053EF6 File Offset: 0x000520F6
		public static implicit operator SyntaxListBuilder([Nullable(new byte[] { 0, 1 })] in SeparatedSyntaxListBuilder<TNode> builder)
		{
			return builder._builder;
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x00053F00 File Offset: 0x00052100
		[return: Nullable(new byte[] { 0, 1 })]
		public static implicit operator SeparatedSyntaxList<TNode>([Nullable(new byte[] { 0, 1 })] in SeparatedSyntaxListBuilder<TNode> builder)
		{
			if (builder._builder != null)
			{
				SeparatedSyntaxListBuilder<TNode> separatedSyntaxListBuilder = builder;
				return separatedSyntaxListBuilder.ToList();
			}
			return default(SeparatedSyntaxList<TNode>);
		}

		// Token: 0x04000D54 RID: 3412
		private readonly SyntaxListBuilder _builder;

		// Token: 0x04000D55 RID: 3413
		private bool _expectedSeparator;
	}
}
