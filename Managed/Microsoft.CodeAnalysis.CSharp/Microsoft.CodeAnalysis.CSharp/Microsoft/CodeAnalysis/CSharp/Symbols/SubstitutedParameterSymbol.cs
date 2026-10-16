using System;
using System.Collections.Immutable;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000565 RID: 1381
	internal sealed class SubstitutedParameterSymbol : WrappedParameterSymbol
	{
		// Token: 0x060074AD RID: 29869 RVA: 0x001B366B File Offset: 0x001B186B
		internal SubstitutedParameterSymbol(MethodSymbol containingSymbol, TypeMap map, ParameterSymbol originalParameter)
			: this(containingSymbol, map, originalParameter)
		{
		}

		// Token: 0x060074AE RID: 29870 RVA: 0x001B3676 File Offset: 0x001B1876
		internal SubstitutedParameterSymbol(PropertySymbol containingSymbol, TypeMap map, ParameterSymbol originalParameter)
			: this(containingSymbol, map, originalParameter)
		{
		}

		// Token: 0x060074AF RID: 29871 RVA: 0x001B3681 File Offset: 0x001B1881
		private SubstitutedParameterSymbol(Symbol containingSymbol, TypeMap map, ParameterSymbol originalParameter)
			: base(originalParameter)
		{
			this._containingSymbol = containingSymbol;
			this._mapOrType = map;
		}

		// Token: 0x17002020 RID: 8224
		// (get) Token: 0x060074B0 RID: 29872 RVA: 0x001B3698 File Offset: 0x001B1898
		public override ParameterSymbol OriginalDefinition
		{
			get
			{
				return this._underlyingParameter.OriginalDefinition;
			}
		}

		// Token: 0x17002021 RID: 8225
		// (get) Token: 0x060074B1 RID: 29873 RVA: 0x001B36A5 File Offset: 0x001B18A5
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._containingSymbol;
			}
		}

		// Token: 0x17002022 RID: 8226
		// (get) Token: 0x060074B2 RID: 29874 RVA: 0x001B36B0 File Offset: 0x001B18B0
		public override TypeWithAnnotations TypeWithAnnotations
		{
			get
			{
				object mapOrType = this._mapOrType;
				if (mapOrType is TypeWithAnnotations)
				{
					return (TypeWithAnnotations)mapOrType;
				}
				TypeWithAnnotations typeWithAnnotations = ((TypeMap)mapOrType).SubstituteType(this._underlyingParameter.TypeWithAnnotations);
				if (typeWithAnnotations.CustomModifiers.IsEmpty && this._underlyingParameter.TypeWithAnnotations.CustomModifiers.IsEmpty && this._underlyingParameter.RefCustomModifiers.IsEmpty)
				{
					this._mapOrType = typeWithAnnotations;
				}
				return typeWithAnnotations;
			}
		}

		// Token: 0x17002023 RID: 8227
		// (get) Token: 0x060074B3 RID: 29875 RVA: 0x001B3740 File Offset: 0x001B1940
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			get
			{
				TypeMap typeMap = this._mapOrType as TypeMap;
				if (typeMap == null)
				{
					return this._underlyingParameter.RefCustomModifiers;
				}
				return typeMap.SubstituteCustomModifiers(this._underlyingParameter.RefCustomModifiers);
			}
		}

		// Token: 0x060074B4 RID: 29876 RVA: 0x001B377C File Offset: 0x001B197C
		public sealed override bool Equals(Symbol obj, TypeCompareKind compareKind)
		{
			if (this == obj)
			{
				return true;
			}
			SubstitutedParameterSymbol substitutedParameterSymbol = obj as SubstitutedParameterSymbol;
			return substitutedParameterSymbol != null && this.Ordinal == substitutedParameterSymbol.Ordinal && this.ContainingSymbol.Equals(substitutedParameterSymbol.ContainingSymbol, compareKind);
		}

		// Token: 0x060074B5 RID: 29877 RVA: 0x001B37BB File Offset: 0x001B19BB
		public sealed override int GetHashCode()
		{
			return Hash.Combine<Symbol>(this.ContainingSymbol, this._underlyingParameter.Ordinal);
		}

		// Token: 0x04001C58 RID: 7256
		private object _mapOrType;

		// Token: 0x04001C59 RID: 7257
		private readonly Symbol _containingSymbol;
	}
}
