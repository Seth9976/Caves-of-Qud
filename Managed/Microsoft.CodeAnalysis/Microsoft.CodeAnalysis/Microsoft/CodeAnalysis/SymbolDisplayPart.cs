using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200020E RID: 526
	[NullableContext(1)]
	[Nullable(0)]
	public struct SymbolDisplayPart
	{
		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x000414F9 File Offset: 0x0003F6F9
		public SymbolDisplayPartKind Kind
		{
			get
			{
				return this._kind;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060014BD RID: 5309 RVA: 0x00041501 File Offset: 0x0003F701
		[Nullable(2)]
		public ISymbol Symbol
		{
			[NullableContext(2)]
			get
			{
				return this._symbol;
			}
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x00041509 File Offset: 0x0003F709
		public SymbolDisplayPart(SymbolDisplayPartKind kind, [Nullable(2)] ISymbol symbol, string text)
		{
			if (!kind.IsValid())
			{
				throw new ArgumentOutOfRangeException("kind");
			}
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			this._kind = kind;
			this._text = text;
			this._symbol = symbol;
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x00041541 File Offset: 0x0003F741
		public override string ToString()
		{
			return this._text;
		}

		// Token: 0x04000762 RID: 1890
		private readonly SymbolDisplayPartKind _kind;

		// Token: 0x04000763 RID: 1891
		private readonly string _text;

		// Token: 0x04000764 RID: 1892
		[Nullable(2)]
		private readonly ISymbol _symbol;
	}
}
