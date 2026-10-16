using System;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000261 RID: 609
	internal static class SymbolKindExtensions
	{
		// Token: 0x06001734 RID: 5940 RVA: 0x00045CE8 File Offset: 0x00043EE8
		public static int ToSortOrder(this SymbolKind kind)
		{
			switch (kind)
			{
			case SymbolKind.Alias:
				return 6;
			case SymbolKind.ArrayType:
				return 7;
			case SymbolKind.Assembly:
				return 8;
			case SymbolKind.DynamicType:
				return 16;
			case SymbolKind.Event:
				return 3;
			case SymbolKind.Field:
				return 0;
			case SymbolKind.Label:
				return 10;
			case SymbolKind.Local:
				return 11;
			case SymbolKind.Method:
				return 1;
			case SymbolKind.NetModule:
				return 12;
			case SymbolKind.NamedType:
				return 4;
			case SymbolKind.Namespace:
				return 5;
			case SymbolKind.Parameter:
				return 13;
			case SymbolKind.Property:
				return 2;
			case SymbolKind.RangeVariable:
				return 14;
			case SymbolKind.TypeParameter:
				return 15;
			case SymbolKind.Preprocessing:
				return 17;
			}
			throw ExceptionUtilities.UnexpectedValue(kind);
		}
	}
}
