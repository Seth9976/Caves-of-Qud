using System;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000223 RID: 547
	internal static class SymbolKindExtensions
	{
		// Token: 0x060027F2 RID: 10226 RVA: 0x000DAAF4 File Offset: 0x000D8CF4
		public static LocalizableErrorArgument Localize(this SymbolKind kind)
		{
			switch (kind)
			{
			case SymbolKind.Alias:
				return MessageID.IDS_SK_ALIAS.Localize();
			case SymbolKind.Event:
				return MessageID.IDS_SK_EVENT.Localize();
			case SymbolKind.Field:
				return MessageID.IDS_SK_FIELD.Localize();
			case SymbolKind.Label:
				return MessageID.IDS_SK_LABEL.Localize();
			case SymbolKind.Local:
			case SymbolKind.Parameter:
			case SymbolKind.RangeVariable:
				return MessageID.IDS_SK_VARIABLE.Localize();
			case SymbolKind.Method:
				return MessageID.IDS_SK_METHOD.Localize();
			case SymbolKind.NamedType:
				return MessageID.IDS_SK_TYPE.Localize();
			case SymbolKind.Namespace:
				return MessageID.IDS_SK_NAMESPACE.Localize();
			case SymbolKind.Property:
				return MessageID.IDS_SK_PROPERTY.Localize();
			case SymbolKind.TypeParameter:
				return MessageID.IDS_SK_TYVAR.Localize();
			case SymbolKind.Preprocessing:
				throw ExceptionUtilities.UnexpectedValue(kind);
			}
			return MessageID.IDS_SK_UNKNOWN.Localize();
		}
	}
}
