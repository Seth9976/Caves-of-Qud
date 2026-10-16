using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000567 RID: 1383
	internal class SubstitutedTypeParameterSymbol : WrappedTypeParameterSymbol
	{
		// Token: 0x060074C5 RID: 29893 RVA: 0x001B39F9 File Offset: 0x001B1BF9
		internal SubstitutedTypeParameterSymbol(Symbol newContainer, TypeMap map, TypeParameterSymbol substitutedFrom, int ordinal)
			: base(substitutedFrom)
		{
			this._container = newContainer;
			this._map = map;
			this._ordinal = ordinal;
		}

		// Token: 0x17002030 RID: 8240
		// (get) Token: 0x060074C6 RID: 29894 RVA: 0x001B3A18 File Offset: 0x001B1C18
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._container;
			}
		}

		// Token: 0x17002031 RID: 8241
		// (get) Token: 0x060074C7 RID: 29895 RVA: 0x001B3A20 File Offset: 0x001B1C20
		public override TypeParameterSymbol OriginalDefinition
		{
			get
			{
				if (!(this.ContainingSymbol.OriginalDefinition != this._underlyingTypeParameter.ContainingSymbol.OriginalDefinition))
				{
					return this._underlyingTypeParameter.OriginalDefinition;
				}
				return this;
			}
		}

		// Token: 0x17002032 RID: 8242
		// (get) Token: 0x060074C8 RID: 29896 RVA: 0x001B3A54 File Offset: 0x001B1C54
		public override TypeParameterSymbol ReducedFrom
		{
			get
			{
				if (this._container.Kind == SymbolKind.Method)
				{
					MethodSymbol reducedFrom = ((MethodSymbol)this._container).ReducedFrom;
					if (reducedFrom != null)
					{
						return reducedFrom.TypeParameters[this.Ordinal];
					}
				}
				return null;
			}
		}

		// Token: 0x17002033 RID: 8243
		// (get) Token: 0x060074C9 RID: 29897 RVA: 0x001B3A9A File Offset: 0x001B1C9A
		public override int Ordinal
		{
			get
			{
				return this._ordinal;
			}
		}

		// Token: 0x17002034 RID: 8244
		// (get) Token: 0x060074CA RID: 29898 RVA: 0x001B3AA2 File Offset: 0x001B1CA2
		public override string Name
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x060074CB RID: 29899 RVA: 0x001B3AAC File Offset: 0x001B1CAC
		internal override ImmutableArray<TypeWithAnnotations> GetConstraintTypes(ConsList<TypeParameterSymbol> inProgress)
		{
			ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance();
			this._map.SubstituteConstraintTypesDistinctWithoutModifiers(this._underlyingTypeParameter, this._underlyingTypeParameter.GetConstraintTypes(inProgress), instance, null);
			TypeWithAnnotations typeWithAnnotations = default(TypeWithAnnotations);
			for (int i = instance.Count - 1; i >= 0; i--)
			{
				if (ConstraintsHelper.IsObjectConstraint(instance[i], ref typeWithAnnotations))
				{
					instance.RemoveAt(i);
				}
			}
			if (typeWithAnnotations.HasType && ConstraintsHelper.IsObjectConstraintSignificant(base.CalculateIsNotNullableFromNonTypeConstraints(), typeWithAnnotations))
			{
				if (instance.Count == 0)
				{
					if (typeWithAnnotations.NullableAnnotation.IsOblivious() && !this.HasReferenceTypeConstraint)
					{
						typeWithAnnotations = default(TypeWithAnnotations);
					}
				}
				else
				{
					ArrayBuilder<TypeWithAnnotations>.Enumerator enumerator = instance.GetEnumerator();
					while (enumerator.MoveNext())
					{
						bool flag;
						if (!ConstraintsHelper.IsObjectConstraintSignificant(TypeParameterSymbol.IsNotNullableFromConstraintType(enumerator.Current, out flag), typeWithAnnotations))
						{
							typeWithAnnotations = default(TypeWithAnnotations);
							break;
						}
					}
				}
				if (typeWithAnnotations.HasType)
				{
					instance.Insert(0, typeWithAnnotations);
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x17002035 RID: 8245
		// (get) Token: 0x060074CC RID: 29900 RVA: 0x001B3B98 File Offset: 0x001B1D98
		internal override bool? IsNotNullable
		{
			get
			{
				if (this._underlyingTypeParameter.ConstraintTypesNoUseSiteDiagnostics.IsEmpty)
				{
					return this._underlyingTypeParameter.IsNotNullable;
				}
				if (!this.HasNotNullConstraint && !this.HasValueTypeConstraint && !this.HasReferenceTypeConstraint)
				{
					ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance();
					this._map.SubstituteConstraintTypesDistinctWithoutModifiers(this._underlyingTypeParameter, this._underlyingTypeParameter.GetConstraintTypes(ConsList<TypeParameterSymbol>.Empty), instance, null);
					return TypeParameterSymbol.IsNotNullableFromConstraintTypes(instance.ToImmutableAndFree());
				}
				return base.CalculateIsNotNullable();
			}
		}

		// Token: 0x060074CD RID: 29901 RVA: 0x001B3C19 File Offset: 0x001B1E19
		internal override ImmutableArray<NamedTypeSymbol> GetInterfaces(ConsList<TypeParameterSymbol> inProgress)
		{
			return this._map.SubstituteNamedTypes(this._underlyingTypeParameter.GetInterfaces(inProgress));
		}

		// Token: 0x060074CE RID: 29902 RVA: 0x001B3C32 File Offset: 0x001B1E32
		internal override NamedTypeSymbol GetEffectiveBaseClass(ConsList<TypeParameterSymbol> inProgress)
		{
			return this._map.SubstituteNamedType(this._underlyingTypeParameter.GetEffectiveBaseClass(inProgress));
		}

		// Token: 0x060074CF RID: 29903 RVA: 0x001B3C4C File Offset: 0x001B1E4C
		internal override TypeSymbol GetDeducedBaseType(ConsList<TypeParameterSymbol> inProgress)
		{
			return this._map.SubstituteType(this._underlyingTypeParameter.GetDeducedBaseType(inProgress)).AsTypeSymbolOnly();
		}

		// Token: 0x04001C5F RID: 7263
		private readonly Symbol _container;

		// Token: 0x04001C60 RID: 7264
		private readonly TypeMap _map;

		// Token: 0x04001C61 RID: 7265
		private readonly int _ordinal;
	}
}
