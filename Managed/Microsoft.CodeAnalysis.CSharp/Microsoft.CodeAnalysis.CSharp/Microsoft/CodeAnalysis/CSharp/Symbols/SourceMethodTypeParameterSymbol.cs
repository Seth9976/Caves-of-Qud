using System;
using System.Collections.Immutable;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000552 RID: 1362
	internal sealed class SourceMethodTypeParameterSymbol : SourceTypeParameterSymbolBase
	{
		// Token: 0x060073C0 RID: 29632 RVA: 0x001B0D3A File Offset: 0x001AEF3A
		public SourceMethodTypeParameterSymbol(SourceMethodSymbol owner, string name, int ordinal, ImmutableArray<Location> locations, ImmutableArray<SyntaxReference> syntaxRefs)
			: base(name, ordinal, locations, syntaxRefs)
		{
			this._owner = owner;
		}

		// Token: 0x060073C1 RID: 29633 RVA: 0x001B0D4F File Offset: 0x001AEF4F
		internal override void AddDeclarationDiagnostics(BindingDiagnosticBag diagnostics)
		{
			this._owner.AddDeclarationDiagnostics(diagnostics);
		}

		// Token: 0x17001FA7 RID: 8103
		// (get) Token: 0x060073C2 RID: 29634 RVA: 0x001B0D5D File Offset: 0x001AEF5D
		public override TypeParameterKind TypeParameterKind
		{
			get
			{
				return TypeParameterKind.Method;
			}
		}

		// Token: 0x17001FA8 RID: 8104
		// (get) Token: 0x060073C3 RID: 29635 RVA: 0x001B0D60 File Offset: 0x001AEF60
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x17001FA9 RID: 8105
		// (get) Token: 0x060073C4 RID: 29636 RVA: 0x001B0D68 File Offset: 0x001AEF68
		public override bool HasConstructorConstraint
		{
			get
			{
				return (this.GetConstraintKinds() & TypeParameterConstraintKind.Constructor) > TypeParameterConstraintKind.None;
			}
		}

		// Token: 0x17001FAA RID: 8106
		// (get) Token: 0x060073C5 RID: 29637 RVA: 0x001B0D75 File Offset: 0x001AEF75
		public override bool HasValueTypeConstraint
		{
			get
			{
				return (this.GetConstraintKinds() & TypeParameterConstraintKind.AllValueTypeKinds) > TypeParameterConstraintKind.None;
			}
		}

		// Token: 0x17001FAB RID: 8107
		// (get) Token: 0x060073C6 RID: 29638 RVA: 0x001B0D83 File Offset: 0x001AEF83
		public override bool IsValueTypeFromConstraintTypes
		{
			get
			{
				return (this.GetConstraintKinds() & TypeParameterConstraintKind.ValueTypeFromConstraintTypes) > TypeParameterConstraintKind.None;
			}
		}

		// Token: 0x17001FAC RID: 8108
		// (get) Token: 0x060073C7 RID: 29639 RVA: 0x001B0D94 File Offset: 0x001AEF94
		public override bool HasReferenceTypeConstraint
		{
			get
			{
				return (this.GetConstraintKinds() & TypeParameterConstraintKind.ReferenceType) > TypeParameterConstraintKind.None;
			}
		}

		// Token: 0x17001FAD RID: 8109
		// (get) Token: 0x060073C8 RID: 29640 RVA: 0x001B0DA1 File Offset: 0x001AEFA1
		public override bool IsReferenceTypeFromConstraintTypes
		{
			get
			{
				return (this.GetConstraintKinds() & TypeParameterConstraintKind.ReferenceTypeFromConstraintTypes) > TypeParameterConstraintKind.None;
			}
		}

		// Token: 0x17001FAE RID: 8110
		// (get) Token: 0x060073C9 RID: 29641 RVA: 0x001B0DB2 File Offset: 0x001AEFB2
		public override bool HasNotNullConstraint
		{
			get
			{
				return (this.GetConstraintKinds() & TypeParameterConstraintKind.NotNull) > TypeParameterConstraintKind.None;
			}
		}

		// Token: 0x17001FAF RID: 8111
		// (get) Token: 0x060073CA RID: 29642 RVA: 0x001B0DC3 File Offset: 0x001AEFC3
		internal override bool? ReferenceTypeConstraintIsNullable
		{
			get
			{
				return base.CalculateReferenceTypeConstraintIsNullable(this.GetConstraintKinds());
			}
		}

		// Token: 0x17001FB0 RID: 8112
		// (get) Token: 0x060073CB RID: 29643 RVA: 0x001B0DD4 File Offset: 0x001AEFD4
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

		// Token: 0x17001FB1 RID: 8113
		// (get) Token: 0x060073CC RID: 29644 RVA: 0x001B0DFC File Offset: 0x001AEFFC
		public override bool HasUnmanagedTypeConstraint
		{
			get
			{
				return (this.GetConstraintKinds() & TypeParameterConstraintKind.Unmanaged) > TypeParameterConstraintKind.None;
			}
		}

		// Token: 0x17001FB2 RID: 8114
		// (get) Token: 0x060073CD RID: 29645 RVA: 0x001B0E09 File Offset: 0x001AF009
		protected override ImmutableArray<TypeParameterSymbol> ContainerTypeParameters
		{
			get
			{
				return this._owner.TypeParameters;
			}
		}

		// Token: 0x060073CE RID: 29646 RVA: 0x001B0E18 File Offset: 0x001AF018
		protected override TypeParameterBounds ResolveBounds(ConsList<TypeParameterSymbol> inProgress, BindingDiagnosticBag diagnostics)
		{
			ImmutableArray<ImmutableArray<TypeWithAnnotations>> typeParameterConstraintTypes = this._owner.GetTypeParameterConstraintTypes();
			ImmutableArray<TypeWithAnnotations> immutableArray = (typeParameterConstraintTypes.IsEmpty ? ImmutableArray<TypeWithAnnotations>.Empty : typeParameterConstraintTypes[this.Ordinal]);
			if (immutableArray.IsEmpty && this.GetConstraintKinds() == TypeParameterConstraintKind.None)
			{
				return null;
			}
			return this.ResolveBounds(this.ContainingAssembly.CorLibrary, inProgress.Prepend(this), immutableArray, false, this.DeclaringCompilation, diagnostics);
		}

		// Token: 0x060073CF RID: 29647 RVA: 0x001B0E84 File Offset: 0x001AF084
		private TypeParameterConstraintKind GetConstraintKinds()
		{
			ImmutableArray<TypeParameterConstraintKind> typeParameterConstraintKinds = this._owner.GetTypeParameterConstraintKinds();
			if (!typeParameterConstraintKinds.IsEmpty)
			{
				return typeParameterConstraintKinds[this.Ordinal];
			}
			return TypeParameterConstraintKind.None;
		}

		// Token: 0x04001C1A RID: 7194
		private readonly SourceMethodSymbol _owner;
	}
}
