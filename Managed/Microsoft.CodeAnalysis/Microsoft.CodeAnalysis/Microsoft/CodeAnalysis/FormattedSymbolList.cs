using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001FF RID: 511
	internal sealed class FormattedSymbolList : IFormattable
	{
		// Token: 0x06001487 RID: 5255 RVA: 0x00040D67 File Offset: 0x0003EF67
		internal FormattedSymbolList(IEnumerable<ISymbol> symbols, SymbolDisplayFormat symbolDisplayFormat = null)
		{
			this._symbols = symbols;
			this._symbolDisplayFormat = symbolDisplayFormat;
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x00040D80 File Offset: 0x0003EF80
		public override string ToString()
		{
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			StringBuilder builder = instance.Builder;
			bool flag = true;
			foreach (ISymbol symbol in this._symbols)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					builder.Append(", ");
				}
				builder.Append(symbol.ToDisplayString(this._symbolDisplayFormat));
			}
			return instance.ToStringAndFree();
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x00040E04 File Offset: 0x0003F004
		string IFormattable.ToString(string format, IFormatProvider formatProvider)
		{
			return this.ToString();
		}

		// Token: 0x040006FE RID: 1790
		private readonly IEnumerable<ISymbol> _symbols;

		// Token: 0x040006FF RID: 1791
		private readonly SymbolDisplayFormat _symbolDisplayFormat;
	}
}
