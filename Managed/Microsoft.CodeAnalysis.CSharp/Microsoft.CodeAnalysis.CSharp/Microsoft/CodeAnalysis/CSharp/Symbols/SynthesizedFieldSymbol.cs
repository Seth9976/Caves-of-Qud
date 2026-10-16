using System;
using System.Collections.Immutable;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200058F RID: 1423
	internal sealed class SynthesizedFieldSymbol : SynthesizedFieldSymbolBase
	{
		// Token: 0x060076B1 RID: 30385 RVA: 0x001B7A44 File Offset: 0x001B5C44
		public SynthesizedFieldSymbol(NamedTypeSymbol containingType, TypeSymbol type, string name, bool isPublic = false, bool isReadOnly = false, bool isStatic = false)
			: base(containingType, name, isPublic, isReadOnly, isStatic)
		{
			this._type = TypeWithAnnotations.Create(type, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
		}

		// Token: 0x17002108 RID: 8456
		// (get) Token: 0x060076B2 RID: 30386 RVA: 0x001B7A75 File Offset: 0x001B5C75
		internal override bool SuppressDynamicAttribute
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060076B3 RID: 30387 RVA: 0x001B7A78 File Offset: 0x001B5C78
		internal override TypeWithAnnotations GetFieldType(ConsList<FieldSymbol> fieldsBeingBound)
		{
			return this._type;
		}

		// Token: 0x04001CC9 RID: 7369
		private readonly TypeWithAnnotations _type;
	}
}
