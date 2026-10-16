using System;
using System.Threading;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004EB RID: 1259
	internal struct LexicalSortKey
	{
		// Token: 0x17001B67 RID: 7015
		// (get) Token: 0x06006A7B RID: 27259 RVA: 0x0018C54B File Offset: 0x0018A74B
		public int TreeOrdinal
		{
			get
			{
				return this._treeOrdinal;
			}
		}

		// Token: 0x17001B68 RID: 7016
		// (get) Token: 0x06006A7C RID: 27260 RVA: 0x0018C553 File Offset: 0x0018A753
		public int Position
		{
			get
			{
				return this._position;
			}
		}

		// Token: 0x06006A7D RID: 27261 RVA: 0x0018C55C File Offset: 0x0018A75C
		public static LexicalSortKey GetSynthesizedMemberKey(int offset)
		{
			return new LexicalSortKey
			{
				_treeOrdinal = int.MaxValue,
				_position = 2147483645 - offset
			};
		}

		// Token: 0x06006A7E RID: 27262 RVA: 0x0018C58C File Offset: 0x0018A78C
		private LexicalSortKey(int treeOrdinal, int position)
		{
			this._treeOrdinal = treeOrdinal;
			this._position = position;
		}

		// Token: 0x06006A7F RID: 27263 RVA: 0x0018C59C File Offset: 0x0018A79C
		private LexicalSortKey(SyntaxTree tree, int position, CSharpCompilation compilation)
		{
			this = new LexicalSortKey((tree == null) ? (-1) : compilation.GetSyntaxTreeOrdinal(tree), position);
		}

		// Token: 0x06006A80 RID: 27264 RVA: 0x0018C5B4 File Offset: 0x0018A7B4
		public LexicalSortKey(SyntaxReference syntaxRef, CSharpCompilation compilation)
		{
			this = new LexicalSortKey(syntaxRef.SyntaxTree, syntaxRef.Span.Start, compilation);
		}

		// Token: 0x06006A81 RID: 27265 RVA: 0x0018C5DC File Offset: 0x0018A7DC
		public LexicalSortKey(Location location, CSharpCompilation compilation)
		{
			this = new LexicalSortKey(location.SourceTree, location.SourceSpan.Start, compilation);
		}

		// Token: 0x06006A82 RID: 27266 RVA: 0x0018C604 File Offset: 0x0018A804
		public LexicalSortKey(CSharpSyntaxNode node, CSharpCompilation compilation)
		{
			this = new LexicalSortKey(node.SyntaxTree, node.SpanStart, compilation);
		}

		// Token: 0x06006A83 RID: 27267 RVA: 0x0018C619 File Offset: 0x0018A819
		public LexicalSortKey(SyntaxToken token, CSharpCompilation compilation)
		{
			this = new LexicalSortKey(token.SyntaxTree, token.SpanStart, compilation);
		}

		// Token: 0x06006A84 RID: 27268 RVA: 0x0018C630 File Offset: 0x0018A830
		public static int Compare(LexicalSortKey xSortKey, LexicalSortKey ySortKey)
		{
			if (xSortKey.TreeOrdinal == ySortKey.TreeOrdinal)
			{
				return xSortKey.Position - ySortKey.Position;
			}
			if (xSortKey.TreeOrdinal < 0)
			{
				return 1;
			}
			if (ySortKey.TreeOrdinal < 0)
			{
				return -1;
			}
			return xSortKey.TreeOrdinal - ySortKey.TreeOrdinal;
		}

		// Token: 0x06006A85 RID: 27269 RVA: 0x0018C684 File Offset: 0x0018A884
		public static LexicalSortKey First(LexicalSortKey xSortKey, LexicalSortKey ySortKey)
		{
			if (LexicalSortKey.Compare(xSortKey, ySortKey) <= 0)
			{
				return xSortKey;
			}
			return ySortKey;
		}

		// Token: 0x17001B69 RID: 7017
		// (get) Token: 0x06006A86 RID: 27270 RVA: 0x0018C693 File Offset: 0x0018A893
		public bool IsInitialized
		{
			get
			{
				return Volatile.Read(ref this._position) >= 0;
			}
		}

		// Token: 0x06006A87 RID: 27271 RVA: 0x0018C6A6 File Offset: 0x0018A8A6
		public void SetFrom(LexicalSortKey other)
		{
			this._treeOrdinal = other._treeOrdinal;
			Volatile.Write(ref this._position, other._position);
		}

		// Token: 0x04001A75 RID: 6773
		private int _treeOrdinal;

		// Token: 0x04001A76 RID: 6774
		private int _position;

		// Token: 0x04001A77 RID: 6775
		public static readonly LexicalSortKey NotInSource = new LexicalSortKey
		{
			_treeOrdinal = -1,
			_position = 0
		};

		// Token: 0x04001A78 RID: 6776
		public static readonly LexicalSortKey NotInitialized = new LexicalSortKey
		{
			_treeOrdinal = -1,
			_position = -1
		};

		// Token: 0x04001A79 RID: 6777
		public static readonly LexicalSortKey SynthesizedCtor = new LexicalSortKey
		{
			_treeOrdinal = int.MaxValue,
			_position = 2147483646
		};

		// Token: 0x04001A7A RID: 6778
		public static readonly LexicalSortKey SynthesizedCCtor = new LexicalSortKey
		{
			_treeOrdinal = int.MaxValue,
			_position = int.MaxValue
		};
	}
}
