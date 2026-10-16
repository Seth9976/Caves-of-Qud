using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Syntax.InternalSyntax
{
	// Token: 0x020002AD RID: 685
	[NullableContext(2)]
	[Nullable(0)]
	internal struct SeparatedSyntaxList<[Nullable(0)] TNode> : IEquatable<SeparatedSyntaxList<TNode>> where TNode : GreenNode
	{
		// Token: 0x06001CF6 RID: 7414 RVA: 0x00054F53 File Offset: 0x00053153
		internal SeparatedSyntaxList([Nullable(new byte[] { 0, 1 })] SyntaxList<GreenNode> list)
		{
			this._list = list;
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x00054F5C File Offset: 0x0005315C
		[Conditional("DEBUG")]
		private static void Validate([Nullable(new byte[] { 0, 1 })] SyntaxList<GreenNode> list)
		{
			for (int i = 0; i < list.Count; i++)
			{
				list.GetRequiredItem(i);
				int num = i & 1;
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06001CF8 RID: 7416 RVA: 0x00054F88 File Offset: 0x00053188
		internal GreenNode Node
		{
			get
			{
				return this._list.Node;
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06001CF9 RID: 7417 RVA: 0x00054FA4 File Offset: 0x000531A4
		public int Count
		{
			get
			{
				return this._list.Count + 1 >> 1;
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06001CFA RID: 7418 RVA: 0x00054FC4 File Offset: 0x000531C4
		public int SeparatorCount
		{
			get
			{
				return this._list.Count >> 1;
			}
		}

		// Token: 0x170007A9 RID: 1961
		public TNode this[int index]
		{
			get
			{
				return (TNode)((object)this._list[index << 1]);
			}
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x00055008 File Offset: 0x00053208
		public GreenNode GetSeparator(int index)
		{
			return this._list[(index << 1) + 1];
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x00055028 File Offset: 0x00053228
		[return: Nullable(new byte[] { 0, 1 })]
		public SyntaxList<GreenNode> GetWithSeparators()
		{
			return this._list;
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x00055030 File Offset: 0x00053230
		public static bool operator ==([Nullable(new byte[] { 0, 1 })] in SeparatedSyntaxList<TNode> left, [Nullable(new byte[] { 0, 1 })] in SeparatedSyntaxList<TNode> right)
		{
			SeparatedSyntaxList<TNode> separatedSyntaxList = left;
			return separatedSyntaxList.Equals(right);
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x00055054 File Offset: 0x00053254
		public static bool operator !=([Nullable(new byte[] { 0, 1 })] in SeparatedSyntaxList<TNode> left, [Nullable(new byte[] { 0, 1 })] in SeparatedSyntaxList<TNode> right)
		{
			SeparatedSyntaxList<TNode> separatedSyntaxList = left;
			return !separatedSyntaxList.Equals(right);
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x00055078 File Offset: 0x00053278
		public bool Equals([Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<TNode> other)
		{
			return this._list == other._list;
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x0005508B File Offset: 0x0005328B
		public override bool Equals(object obj)
		{
			return obj is SeparatedSyntaxList<TNode> && this.Equals((SeparatedSyntaxList<TNode>)obj);
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x000550A4 File Offset: 0x000532A4
		public override int GetHashCode()
		{
			return this._list.GetHashCode();
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x000550C5 File Offset: 0x000532C5
		[return: Nullable(new byte[] { 0, 1 })]
		public static implicit operator SeparatedSyntaxList<GreenNode>([Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<TNode> list)
		{
			return new SeparatedSyntaxList<GreenNode>(list.GetWithSeparators());
		}

		// Token: 0x04000D62 RID: 3426
		[Nullable(new byte[] { 0, 1 })]
		private readonly SyntaxList<GreenNode> _list;
	}
}
