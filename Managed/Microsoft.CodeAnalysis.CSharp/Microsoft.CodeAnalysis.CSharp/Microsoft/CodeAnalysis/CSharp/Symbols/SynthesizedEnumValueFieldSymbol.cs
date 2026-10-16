using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200058D RID: 1421
	internal sealed class SynthesizedEnumValueFieldSymbol : SynthesizedFieldSymbolBase
	{
		// Token: 0x060076A4 RID: 30372 RVA: 0x001B78FC File Offset: 0x001B5AFC
		public SynthesizedEnumValueFieldSymbol(SourceNamedTypeSymbol containingEnum)
			: base(containingEnum, "value__", true, false, false)
		{
		}

		// Token: 0x17002101 RID: 8449
		// (get) Token: 0x060076A5 RID: 30373 RVA: 0x001B790D File Offset: 0x001B5B0D
		internal override bool SuppressDynamicAttribute
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060076A6 RID: 30374 RVA: 0x001B7910 File Offset: 0x001B5B10
		internal override TypeWithAnnotations GetFieldType(ConsList<FieldSymbol> fieldsBeingBound)
		{
			return TypeWithAnnotations.Create(((SourceNamedTypeSymbol)this.ContainingType).EnumUnderlyingType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
		}

		// Token: 0x060076A7 RID: 30375 RVA: 0x001B793C File Offset: 0x001B5B3C
		internal override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
		{
		}
	}
}
