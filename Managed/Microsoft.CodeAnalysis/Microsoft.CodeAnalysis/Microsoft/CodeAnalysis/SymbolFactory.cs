using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.Cci;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001AC RID: 428
	internal abstract class SymbolFactory<ModuleSymbol, TypeSymbol> where TypeSymbol : class
	{
		// Token: 0x060011E0 RID: 4576
		internal abstract TypeSymbol GetUnsupportedMetadataTypeSymbol(ModuleSymbol moduleSymbol, BadImageFormatException exception);

		// Token: 0x060011E1 RID: 4577
		internal abstract TypeSymbol MakeUnboundIfGeneric(ModuleSymbol moduleSymbol, TypeSymbol type);

		// Token: 0x060011E2 RID: 4578
		internal abstract TypeSymbol GetSZArrayTypeSymbol(ModuleSymbol moduleSymbol, TypeSymbol elementType, ImmutableArray<ModifierInfo<TypeSymbol>> customModifiers);

		// Token: 0x060011E3 RID: 4579
		internal abstract TypeSymbol GetMDArrayTypeSymbol(ModuleSymbol moduleSymbol, int rank, TypeSymbol elementType, ImmutableArray<ModifierInfo<TypeSymbol>> customModifiers, ImmutableArray<int> sizes, ImmutableArray<int> lowerBounds);

		// Token: 0x060011E4 RID: 4580
		internal abstract TypeSymbol SubstituteTypeParameters(ModuleSymbol moduleSymbol, TypeSymbol generic, ImmutableArray<KeyValuePair<TypeSymbol, ImmutableArray<ModifierInfo<TypeSymbol>>>> arguments, ImmutableArray<bool> refersToNoPiaLocalType);

		// Token: 0x060011E5 RID: 4581
		internal abstract TypeSymbol MakePointerTypeSymbol(ModuleSymbol moduleSymbol, TypeSymbol type, ImmutableArray<ModifierInfo<TypeSymbol>> customModifiers);

		// Token: 0x060011E6 RID: 4582
		internal abstract TypeSymbol MakeFunctionPointerTypeSymbol(CallingConvention callingConvention, ImmutableArray<ParamInfo<TypeSymbol>> returnAndParamTypes);

		// Token: 0x060011E7 RID: 4583
		internal abstract TypeSymbol GetSpecialType(ModuleSymbol moduleSymbol, SpecialType specialType);

		// Token: 0x060011E8 RID: 4584
		internal abstract TypeSymbol GetSystemTypeSymbol(ModuleSymbol moduleSymbol);

		// Token: 0x060011E9 RID: 4585
		internal abstract TypeSymbol GetEnumUnderlyingType(ModuleSymbol moduleSymbol, TypeSymbol type);

		// Token: 0x060011EA RID: 4586
		internal abstract PrimitiveTypeCode GetPrimitiveTypeCode(ModuleSymbol moduleSymbol, TypeSymbol type);
	}
}
