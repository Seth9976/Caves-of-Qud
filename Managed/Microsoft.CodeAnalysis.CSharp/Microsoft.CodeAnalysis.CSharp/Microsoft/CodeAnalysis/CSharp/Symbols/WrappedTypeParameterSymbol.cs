using System;
using System.Collections.Immutable;
using System.Globalization;
using System.Threading;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020005B6 RID: 1462
	internal abstract class WrappedTypeParameterSymbol : TypeParameterSymbol
	{
		// Token: 0x06007AE9 RID: 31465 RVA: 0x001BFCD1 File Offset: 0x001BDED1
		public WrappedTypeParameterSymbol(TypeParameterSymbol underlyingTypeParameter)
		{
			this._underlyingTypeParameter = underlyingTypeParameter;
		}

		// Token: 0x17002369 RID: 9065
		// (get) Token: 0x06007AEA RID: 31466 RVA: 0x001BFCE0 File Offset: 0x001BDEE0
		public TypeParameterSymbol UnderlyingTypeParameter
		{
			get
			{
				return this._underlyingTypeParameter;
			}
		}

		// Token: 0x1700236A RID: 9066
		// (get) Token: 0x06007AEB RID: 31467 RVA: 0x001BFCE8 File Offset: 0x001BDEE8
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return this._underlyingTypeParameter.IsImplicitlyDeclared;
			}
		}

		// Token: 0x1700236B RID: 9067
		// (get) Token: 0x06007AEC RID: 31468 RVA: 0x001BFCF5 File Offset: 0x001BDEF5
		public override TypeParameterKind TypeParameterKind
		{
			get
			{
				return this._underlyingTypeParameter.TypeParameterKind;
			}
		}

		// Token: 0x1700236C RID: 9068
		// (get) Token: 0x06007AED RID: 31469 RVA: 0x001BFD02 File Offset: 0x001BDF02
		public override int Ordinal
		{
			get
			{
				return this._underlyingTypeParameter.Ordinal;
			}
		}

		// Token: 0x1700236D RID: 9069
		// (get) Token: 0x06007AEE RID: 31470 RVA: 0x001BFD0F File Offset: 0x001BDF0F
		public override bool HasConstructorConstraint
		{
			get
			{
				return this._underlyingTypeParameter.HasConstructorConstraint;
			}
		}

		// Token: 0x1700236E RID: 9070
		// (get) Token: 0x06007AEF RID: 31471 RVA: 0x001BFD1C File Offset: 0x001BDF1C
		public override bool HasReferenceTypeConstraint
		{
			get
			{
				return this._underlyingTypeParameter.HasReferenceTypeConstraint;
			}
		}

		// Token: 0x1700236F RID: 9071
		// (get) Token: 0x06007AF0 RID: 31472 RVA: 0x001BFD29 File Offset: 0x001BDF29
		public override bool IsReferenceTypeFromConstraintTypes
		{
			get
			{
				return this._underlyingTypeParameter.IsReferenceTypeFromConstraintTypes || TypeParameterSymbol.CalculateIsReferenceTypeFromConstraintTypes(base.ConstraintTypesNoUseSiteDiagnostics);
			}
		}

		// Token: 0x17002370 RID: 9072
		// (get) Token: 0x06007AF1 RID: 31473 RVA: 0x001BFD45 File Offset: 0x001BDF45
		internal override bool? ReferenceTypeConstraintIsNullable
		{
			get
			{
				return this._underlyingTypeParameter.ReferenceTypeConstraintIsNullable;
			}
		}

		// Token: 0x17002371 RID: 9073
		// (get) Token: 0x06007AF2 RID: 31474 RVA: 0x001BFD52 File Offset: 0x001BDF52
		public override bool HasNotNullConstraint
		{
			get
			{
				return this._underlyingTypeParameter.HasNotNullConstraint;
			}
		}

		// Token: 0x17002372 RID: 9074
		// (get) Token: 0x06007AF3 RID: 31475 RVA: 0x001BFD5F File Offset: 0x001BDF5F
		public override bool HasUnmanagedTypeConstraint
		{
			get
			{
				return this._underlyingTypeParameter.HasUnmanagedTypeConstraint;
			}
		}

		// Token: 0x17002373 RID: 9075
		// (get) Token: 0x06007AF4 RID: 31476 RVA: 0x001BFD6C File Offset: 0x001BDF6C
		public override bool HasValueTypeConstraint
		{
			get
			{
				return this._underlyingTypeParameter.HasValueTypeConstraint;
			}
		}

		// Token: 0x17002374 RID: 9076
		// (get) Token: 0x06007AF5 RID: 31477 RVA: 0x001BFD79 File Offset: 0x001BDF79
		public override bool IsValueTypeFromConstraintTypes
		{
			get
			{
				return this._underlyingTypeParameter.IsValueTypeFromConstraintTypes || TypeParameterSymbol.CalculateIsValueTypeFromConstraintTypes(base.ConstraintTypesNoUseSiteDiagnostics);
			}
		}

		// Token: 0x17002375 RID: 9077
		// (get) Token: 0x06007AF6 RID: 31478 RVA: 0x001BFD95 File Offset: 0x001BDF95
		public override VarianceKind Variance
		{
			get
			{
				return this._underlyingTypeParameter.Variance;
			}
		}

		// Token: 0x17002376 RID: 9078
		// (get) Token: 0x06007AF7 RID: 31479 RVA: 0x001BFDA2 File Offset: 0x001BDFA2
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return this._underlyingTypeParameter.Locations;
			}
		}

		// Token: 0x17002377 RID: 9079
		// (get) Token: 0x06007AF8 RID: 31480 RVA: 0x001BFDAF File Offset: 0x001BDFAF
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return this._underlyingTypeParameter.DeclaringSyntaxReferences;
			}
		}

		// Token: 0x17002378 RID: 9080
		// (get) Token: 0x06007AF9 RID: 31481 RVA: 0x001BFDBC File Offset: 0x001BDFBC
		public override string Name
		{
			get
			{
				return this._underlyingTypeParameter.Name;
			}
		}

		// Token: 0x06007AFA RID: 31482 RVA: 0x001BFDC9 File Offset: 0x001BDFC9
		public override string GetDocumentationCommentXml(CultureInfo preferredCulture = null, bool expandIncludes = false, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this._underlyingTypeParameter.GetDocumentationCommentXml(preferredCulture, expandIncludes, cancellationToken);
		}

		// Token: 0x06007AFB RID: 31483 RVA: 0x001BFDD9 File Offset: 0x001BDFD9
		internal override void EnsureAllConstraintsAreResolved()
		{
			this._underlyingTypeParameter.EnsureAllConstraintsAreResolved();
		}

		// Token: 0x06007AFC RID: 31484 RVA: 0x001BFDE6 File Offset: 0x001BDFE6
		public override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			return this._underlyingTypeParameter.GetAttributes();
		}

		// Token: 0x04001D32 RID: 7474
		protected readonly TypeParameterSymbol _underlyingTypeParameter;
	}
}
