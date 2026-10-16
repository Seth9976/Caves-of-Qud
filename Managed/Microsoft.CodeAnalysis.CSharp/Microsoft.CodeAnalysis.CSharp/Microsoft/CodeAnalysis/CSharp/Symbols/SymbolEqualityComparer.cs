using System;
using System.Collections.Generic;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000569 RID: 1385
	internal sealed class SymbolEqualityComparer : EqualityComparer<Symbol>
	{
		// Token: 0x17002038 RID: 8248
		// (get) Token: 0x060074D8 RID: 29912 RVA: 0x001B3D8C File Offset: 0x001B1F8C
		internal static EqualityComparer<Symbol> IncludeNullability
		{
			get
			{
				return SymbolEqualityComparer.ConsiderEverything;
			}
		}

		// Token: 0x060074D9 RID: 29913 RVA: 0x001B3D93 File Offset: 0x001B1F93
		private SymbolEqualityComparer(TypeCompareKind comparison)
		{
			this._comparison = comparison;
		}

		// Token: 0x060074DA RID: 29914 RVA: 0x001B3DA2 File Offset: 0x001B1FA2
		public override int GetHashCode(Symbol obj)
		{
			if (obj != null)
			{
				return obj.GetHashCode();
			}
			return 0;
		}

		// Token: 0x060074DB RID: 29915 RVA: 0x001B3DAF File Offset: 0x001B1FAF
		public override bool Equals(Symbol x, Symbol y)
		{
			if (x != null)
			{
				return x.Equals(y, this._comparison);
			}
			return y == null;
		}

		// Token: 0x04001C63 RID: 7267
		internal static readonly EqualityComparer<Symbol> ConsiderEverything = new SymbolEqualityComparer(TypeCompareKind.ConsiderEverything);

		// Token: 0x04001C64 RID: 7268
		internal static readonly EqualityComparer<Symbol> IgnoringTupleNamesAndNullability = new SymbolEqualityComparer(TypeCompareKind.IgnoreTupleNames | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes);

		// Token: 0x04001C65 RID: 7269
		internal static readonly EqualityComparer<Symbol> IgnoringDynamicTupleNamesAndNullability = new SymbolEqualityComparer(TypeCompareKind.IgnoreDynamic | TypeCompareKind.IgnoreTupleNames | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes);

		// Token: 0x04001C66 RID: 7270
		internal static readonly EqualityComparer<Symbol> IgnoringNullable = new SymbolEqualityComparer(TypeCompareKind.IgnoreNullableModifiersForReferenceTypes);

		// Token: 0x04001C67 RID: 7271
		internal static readonly EqualityComparer<Symbol> ObliviousNullableModifierMatchesAny = new SymbolEqualityComparer(TypeCompareKind.ObliviousNullableModifierMatchesAny);

		// Token: 0x04001C68 RID: 7272
		internal static readonly EqualityComparer<Symbol> AllIgnoreOptions = new SymbolEqualityComparer(TypeCompareKind.AllIgnoreOptions);

		// Token: 0x04001C69 RID: 7273
		internal static readonly EqualityComparer<Symbol> AllIgnoreOptionsPlusNullableWithUnknownMatchesAny = new SymbolEqualityComparer(TypeCompareKind.IgnoreCustomModifiersAndArraySizesAndLowerBounds | TypeCompareKind.IgnoreDynamic | TypeCompareKind.IgnoreTupleNames | TypeCompareKind.ObliviousNullableModifierMatchesAny | TypeCompareKind.IgnoreNativeIntegers);

		// Token: 0x04001C6A RID: 7274
		internal static readonly EqualityComparer<Symbol> CLRSignature = new SymbolEqualityComparer(TypeCompareKind.CLRSignatureCompareOptions);

		// Token: 0x04001C6B RID: 7275
		private readonly TypeCompareKind _comparison;
	}
}
