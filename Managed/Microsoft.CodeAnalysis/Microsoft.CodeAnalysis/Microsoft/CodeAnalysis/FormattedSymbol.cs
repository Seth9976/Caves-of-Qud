using System;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001FE RID: 510
	internal sealed class FormattedSymbol : IFormattable
	{
		// Token: 0x06001482 RID: 5250 RVA: 0x00040CD8 File Offset: 0x0003EED8
		internal FormattedSymbol(ISymbolInternal symbol, SymbolDisplayFormat symbolDisplayFormat)
		{
			this._symbol = symbol;
			this._symbolDisplayFormat = symbolDisplayFormat;
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x00040CEE File Offset: 0x0003EEEE
		public override string ToString()
		{
			return this._symbol.GetISymbol().ToDisplayString(this._symbolDisplayFormat);
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x00040D08 File Offset: 0x0003EF08
		public override bool Equals(object obj)
		{
			FormattedSymbol formattedSymbol = obj as FormattedSymbol;
			return formattedSymbol != null && this._symbol.Equals(formattedSymbol._symbol) && this._symbolDisplayFormat == formattedSymbol._symbolDisplayFormat;
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x00040D42 File Offset: 0x0003EF42
		public override int GetHashCode()
		{
			return Hash.Combine(this._symbol.GetHashCode(), this._symbolDisplayFormat.GetHashCode());
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x00040D5F File Offset: 0x0003EF5F
		string IFormattable.ToString(string format, IFormatProvider formatProvider)
		{
			return this.ToString();
		}

		// Token: 0x040006FC RID: 1788
		private readonly ISymbolInternal _symbol;

		// Token: 0x040006FD RID: 1789
		private readonly SymbolDisplayFormat _symbolDisplayFormat;
	}
}
