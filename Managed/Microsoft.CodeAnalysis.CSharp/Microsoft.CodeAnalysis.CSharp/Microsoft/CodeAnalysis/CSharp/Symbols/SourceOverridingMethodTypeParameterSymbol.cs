using System;
using System.Collections.Immutable;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000556 RID: 1366
	internal sealed class SourceOverridingMethodTypeParameterSymbol : SourceTypeParameterSymbolBase
	{
		// Token: 0x060073DA RID: 29658 RVA: 0x001B0FDC File Offset: 0x001AF1DC
		public SourceOverridingMethodTypeParameterSymbol(OverriddenMethodTypeParameterMapBase map, string name, int ordinal, ImmutableArray<Location> locations, ImmutableArray<SyntaxReference> syntaxRefs)
			: base(name, ordinal, locations, syntaxRefs)
		{
			this._map = map;
		}

		// Token: 0x17001FB6 RID: 8118
		// (get) Token: 0x060073DB RID: 29659 RVA: 0x001B0FF1 File Offset: 0x001AF1F1
		public SourceOrdinaryMethodSymbol Owner
		{
			get
			{
				return this._map.OverridingMethod;
			}
		}

		// Token: 0x17001FB7 RID: 8119
		// (get) Token: 0x060073DC RID: 29660 RVA: 0x001B0FFE File Offset: 0x001AF1FE
		public override TypeParameterKind TypeParameterKind
		{
			get
			{
				return TypeParameterKind.Method;
			}
		}

		// Token: 0x17001FB8 RID: 8120
		// (get) Token: 0x060073DD RID: 29661 RVA: 0x001B1001 File Offset: 0x001AF201
		public override Symbol ContainingSymbol
		{
			get
			{
				return this.Owner;
			}
		}

		// Token: 0x17001FB9 RID: 8121
		// (get) Token: 0x060073DE RID: 29662 RVA: 0x001B100C File Offset: 0x001AF20C
		public override bool HasConstructorConstraint
		{
			get
			{
				TypeParameterSymbol overriddenTypeParameter = this.OverriddenTypeParameter;
				return overriddenTypeParameter != null && overriddenTypeParameter.HasConstructorConstraint;
			}
		}

		// Token: 0x17001FBA RID: 8122
		// (get) Token: 0x060073DF RID: 29663 RVA: 0x001B102C File Offset: 0x001AF22C
		public override bool HasValueTypeConstraint
		{
			get
			{
				TypeParameterSymbol overriddenTypeParameter = this.OverriddenTypeParameter;
				return overriddenTypeParameter != null && overriddenTypeParameter.HasValueTypeConstraint;
			}
		}

		// Token: 0x17001FBB RID: 8123
		// (get) Token: 0x060073E0 RID: 29664 RVA: 0x001B104C File Offset: 0x001AF24C
		public override bool IsValueTypeFromConstraintTypes
		{
			get
			{
				TypeParameterSymbol overriddenTypeParameter = this.OverriddenTypeParameter;
				return overriddenTypeParameter != null && (overriddenTypeParameter.IsValueTypeFromConstraintTypes || TypeParameterSymbol.CalculateIsValueTypeFromConstraintTypes(base.ConstraintTypesNoUseSiteDiagnostics));
			}
		}

		// Token: 0x17001FBC RID: 8124
		// (get) Token: 0x060073E1 RID: 29665 RVA: 0x001B107C File Offset: 0x001AF27C
		public override bool HasReferenceTypeConstraint
		{
			get
			{
				TypeParameterSymbol overriddenTypeParameter = this.OverriddenTypeParameter;
				return overriddenTypeParameter != null && overriddenTypeParameter.HasReferenceTypeConstraint;
			}
		}

		// Token: 0x17001FBD RID: 8125
		// (get) Token: 0x060073E2 RID: 29666 RVA: 0x001B109C File Offset: 0x001AF29C
		public override bool IsReferenceTypeFromConstraintTypes
		{
			get
			{
				TypeParameterSymbol overriddenTypeParameter = this.OverriddenTypeParameter;
				return overriddenTypeParameter != null && (overriddenTypeParameter.IsReferenceTypeFromConstraintTypes || TypeParameterSymbol.CalculateIsReferenceTypeFromConstraintTypes(base.ConstraintTypesNoUseSiteDiagnostics));
			}
		}

		// Token: 0x17001FBE RID: 8126
		// (get) Token: 0x060073E3 RID: 29667 RVA: 0x001B10CC File Offset: 0x001AF2CC
		internal override bool? ReferenceTypeConstraintIsNullable
		{
			get
			{
				TypeParameterSymbol overriddenTypeParameter = this.OverriddenTypeParameter;
				if (overriddenTypeParameter == null)
				{
					return new bool?(false);
				}
				return overriddenTypeParameter.ReferenceTypeConstraintIsNullable;
			}
		}

		// Token: 0x17001FBF RID: 8127
		// (get) Token: 0x060073E4 RID: 29668 RVA: 0x001B10F0 File Offset: 0x001AF2F0
		public override bool HasNotNullConstraint
		{
			get
			{
				TypeParameterSymbol overriddenTypeParameter = this.OverriddenTypeParameter;
				return overriddenTypeParameter != null && overriddenTypeParameter.HasNotNullConstraint;
			}
		}

		// Token: 0x17001FC0 RID: 8128
		// (get) Token: 0x060073E5 RID: 29669 RVA: 0x001B1104 File Offset: 0x001AF304
		internal override bool? IsNotNullable
		{
			get
			{
				TypeParameterSymbol overriddenTypeParameter = this.OverriddenTypeParameter;
				if (overriddenTypeParameter == null)
				{
					return default(bool?);
				}
				return overriddenTypeParameter.IsNotNullable;
			}
		}

		// Token: 0x17001FC1 RID: 8129
		// (get) Token: 0x060073E6 RID: 29670 RVA: 0x001B112C File Offset: 0x001AF32C
		public override bool HasUnmanagedTypeConstraint
		{
			get
			{
				TypeParameterSymbol overriddenTypeParameter = this.OverriddenTypeParameter;
				return overriddenTypeParameter != null && overriddenTypeParameter.HasUnmanagedTypeConstraint;
			}
		}

		// Token: 0x17001FC2 RID: 8130
		// (get) Token: 0x060073E7 RID: 29671 RVA: 0x001B114B File Offset: 0x001AF34B
		protected override ImmutableArray<TypeParameterSymbol> ContainerTypeParameters
		{
			get
			{
				return this.Owner.TypeParameters;
			}
		}

		// Token: 0x060073E8 RID: 29672 RVA: 0x001B1158 File Offset: 0x001AF358
		protected override TypeParameterBounds ResolveBounds(ConsList<TypeParameterSymbol> inProgress, BindingDiagnosticBag diagnostics)
		{
			TypeParameterSymbol overriddenTypeParameter = this.OverriddenTypeParameter;
			if (overriddenTypeParameter == null)
			{
				return null;
			}
			ImmutableArray<TypeWithAnnotations> immutableArray = this._map.TypeMap.SubstituteTypes(overriddenTypeParameter.ConstraintTypesNoUseSiteDiagnostics);
			return this.ResolveBounds(this.ContainingAssembly.CorLibrary, inProgress.Prepend(this), immutableArray, true, this.DeclaringCompilation, diagnostics);
		}

		// Token: 0x17001FC3 RID: 8131
		// (get) Token: 0x060073E9 RID: 29673 RVA: 0x001B11A9 File Offset: 0x001AF3A9
		private TypeParameterSymbol OverriddenTypeParameter
		{
			get
			{
				return this._map.GetOverriddenTypeParameter(this.Ordinal);
			}
		}

		// Token: 0x04001C1E RID: 7198
		private readonly OverriddenMethodTypeParameterMapBase _map;
	}
}
