using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000018 RID: 24
	internal sealed class ContextualAttributeBinder : Binder
	{
		// Token: 0x06000522 RID: 1314 RVA: 0x00033410 File Offset: 0x00031610
		public ContextualAttributeBinder(Binder enclosing, Symbol symbol)
			: base(enclosing, enclosing.Flags | BinderFlags.InContextualAttributeBinder)
		{
			this._attributeTarget = symbol;
			this._attributedMember = ContextualAttributeBinder.GetAttributedMember(symbol);
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x00033438 File Offset: 0x00031638
		internal Symbol AttributedMember
		{
			get
			{
				return this._attributedMember;
			}
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00033440 File Offset: 0x00031640
		private static Symbol GetAttributedMember(Symbol symbol)
		{
			while (symbol != null)
			{
				SymbolKind kind = symbol.Kind;
				if (kind == SymbolKind.Event || kind == SymbolKind.Method || kind == SymbolKind.Property)
				{
					return symbol;
				}
				symbol = symbol.ContainingSymbol;
			}
			return symbol;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00033472 File Offset: 0x00031672
		internal Symbol AttributeTarget
		{
			get
			{
				return this._attributeTarget;
			}
		}

		// Token: 0x0400004A RID: 74
		private readonly Symbol _attributeTarget;

		// Token: 0x0400004B RID: 75
		private readonly Symbol _attributedMember;
	}
}
