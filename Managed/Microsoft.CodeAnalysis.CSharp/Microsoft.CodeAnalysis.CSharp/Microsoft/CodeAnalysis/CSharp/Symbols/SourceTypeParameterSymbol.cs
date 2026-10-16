using System;
using System.Collections.Immutable;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000551 RID: 1361
	internal sealed class SourceTypeParameterSymbol : SourceTypeParameterSymbolBase
	{
		// Token: 0x060073B0 RID: 29616 RVA: 0x001B0BF6 File Offset: 0x001AEDF6
		public SourceTypeParameterSymbol(SourceNamedTypeSymbol owner, string name, int ordinal, VarianceKind varianceKind, ImmutableArray<Location> locations, ImmutableArray<SyntaxReference> syntaxRefs)
			: base(name, ordinal, locations, syntaxRefs)
		{
			this._owner = owner;
			this._varianceKind = varianceKind;
		}

		// Token: 0x17001F9A RID: 8090
		// (get) Token: 0x060073B1 RID: 29617 RVA: 0x001B0C13 File Offset: 0x001AEE13
		public override TypeParameterKind TypeParameterKind
		{
			get
			{
				return TypeParameterKind.Type;
			}
		}

		// Token: 0x17001F9B RID: 8091
		// (get) Token: 0x060073B2 RID: 29618 RVA: 0x001B0C16 File Offset: 0x001AEE16
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x17001F9C RID: 8092
		// (get) Token: 0x060073B3 RID: 29619 RVA: 0x001B0C1E File Offset: 0x001AEE1E
		public override VarianceKind Variance
		{
			get
			{
				return this._varianceKind;
			}
		}

		// Token: 0x17001F9D RID: 8093
		// (get) Token: 0x060073B4 RID: 29620 RVA: 0x001B0C26 File Offset: 0x001AEE26
		public override bool HasConstructorConstraint
		{
			get
			{
				return (this.GetConstraintKinds() & TypeParameterConstraintKind.Constructor) > TypeParameterConstraintKind.None;
			}
		}

		// Token: 0x17001F9E RID: 8094
		// (get) Token: 0x060073B5 RID: 29621 RVA: 0x001B0C33 File Offset: 0x001AEE33
		public override bool HasValueTypeConstraint
		{
			get
			{
				return (this.GetConstraintKinds() & TypeParameterConstraintKind.AllValueTypeKinds) > TypeParameterConstraintKind.None;
			}
		}

		// Token: 0x17001F9F RID: 8095
		// (get) Token: 0x060073B6 RID: 29622 RVA: 0x001B0C41 File Offset: 0x001AEE41
		public override bool IsValueTypeFromConstraintTypes
		{
			get
			{
				return (this.GetConstraintKinds() & TypeParameterConstraintKind.ValueTypeFromConstraintTypes) > TypeParameterConstraintKind.None;
			}
		}

		// Token: 0x17001FA0 RID: 8096
		// (get) Token: 0x060073B7 RID: 29623 RVA: 0x001B0C52 File Offset: 0x001AEE52
		public override bool HasReferenceTypeConstraint
		{
			get
			{
				return (this.GetConstraintKinds() & TypeParameterConstraintKind.ReferenceType) > TypeParameterConstraintKind.None;
			}
		}

		// Token: 0x17001FA1 RID: 8097
		// (get) Token: 0x060073B8 RID: 29624 RVA: 0x001B0C5F File Offset: 0x001AEE5F
		public override bool IsReferenceTypeFromConstraintTypes
		{
			get
			{
				return (this.GetConstraintKinds() & TypeParameterConstraintKind.ReferenceTypeFromConstraintTypes) > TypeParameterConstraintKind.None;
			}
		}

		// Token: 0x17001FA2 RID: 8098
		// (get) Token: 0x060073B9 RID: 29625 RVA: 0x001B0C70 File Offset: 0x001AEE70
		internal override bool? ReferenceTypeConstraintIsNullable
		{
			get
			{
				return base.CalculateReferenceTypeConstraintIsNullable(this.GetConstraintKinds());
			}
		}

		// Token: 0x17001FA3 RID: 8099
		// (get) Token: 0x060073BA RID: 29626 RVA: 0x001B0C7E File Offset: 0x001AEE7E
		public override bool HasNotNullConstraint
		{
			get
			{
				return (this.GetConstraintKinds() & TypeParameterConstraintKind.NotNull) > TypeParameterConstraintKind.None;
			}
		}

		// Token: 0x17001FA4 RID: 8100
		// (get) Token: 0x060073BB RID: 29627 RVA: 0x001B0C90 File Offset: 0x001AEE90
		internal override bool? IsNotNullable
		{
			get
			{
				if ((this.GetConstraintKinds() & TypeParameterConstraintKind.ObliviousNullabilityIfReferenceType) != TypeParameterConstraintKind.None)
				{
					return default(bool?);
				}
				return base.CalculateIsNotNullable();
			}
		}

		// Token: 0x17001FA5 RID: 8101
		// (get) Token: 0x060073BC RID: 29628 RVA: 0x001B0CB8 File Offset: 0x001AEEB8
		public override bool HasUnmanagedTypeConstraint
		{
			get
			{
				return (this.GetConstraintKinds() & TypeParameterConstraintKind.Unmanaged) > TypeParameterConstraintKind.None;
			}
		}

		// Token: 0x17001FA6 RID: 8102
		// (get) Token: 0x060073BD RID: 29629 RVA: 0x001B0CC5 File Offset: 0x001AEEC5
		protected override ImmutableArray<TypeParameterSymbol> ContainerTypeParameters
		{
			get
			{
				return this._owner.TypeParameters;
			}
		}

		// Token: 0x060073BE RID: 29630 RVA: 0x001B0CD4 File Offset: 0x001AEED4
		protected override TypeParameterBounds ResolveBounds(ConsList<TypeParameterSymbol> inProgress, BindingDiagnosticBag diagnostics)
		{
			ImmutableArray<TypeWithAnnotations> typeParameterConstraintTypes = this._owner.GetTypeParameterConstraintTypes(this.Ordinal);
			if (typeParameterConstraintTypes.IsEmpty && this.GetConstraintKinds() == TypeParameterConstraintKind.None)
			{
				return null;
			}
			return this.ResolveBounds(this.ContainingAssembly.CorLibrary, inProgress.Prepend(this), typeParameterConstraintTypes, false, this.DeclaringCompilation, diagnostics);
		}

		// Token: 0x060073BF RID: 29631 RVA: 0x001B0D27 File Offset: 0x001AEF27
		private TypeParameterConstraintKind GetConstraintKinds()
		{
			return this._owner.GetTypeParameterConstraintKind(this.Ordinal);
		}

		// Token: 0x04001C18 RID: 7192
		private readonly SourceNamedTypeSymbol _owner;

		// Token: 0x04001C19 RID: 7193
		private readonly VarianceKind _varianceKind;
	}
}
