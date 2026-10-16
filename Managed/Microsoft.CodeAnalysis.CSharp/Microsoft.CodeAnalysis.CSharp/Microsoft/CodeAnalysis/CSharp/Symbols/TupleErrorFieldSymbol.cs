using System;
using System.Collections.Immutable;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020005A1 RID: 1441
	internal sealed class TupleErrorFieldSymbol : SynthesizedFieldSymbolBase
	{
		// Token: 0x06007885 RID: 30853 RVA: 0x001B9398 File Offset: 0x001B7598
		public TupleErrorFieldSymbol(NamedTypeSymbol container, string name, int tupleElementIndex, Location location, TypeWithAnnotations type, DiagnosticInfo useSiteDiagnosticInfo, bool isImplicitlyDeclared, TupleErrorFieldSymbol correspondingDefaultFieldOpt)
			: base(container, name, true, false, false)
		{
			this._type = type;
			this._locations = ((location == null) ? ImmutableArray<Location>.Empty : ImmutableArray.Create<Location>(location));
			this._useSiteDiagnosticInfo = useSiteDiagnosticInfo;
			this._tupleElementIndex = ((correspondingDefaultFieldOpt == null) ? (tupleElementIndex << 1) : ((tupleElementIndex << 1) + 1));
			this._isImplicitlyDeclared = isImplicitlyDeclared;
			this._correspondingDefaultField = correspondingDefaultFieldOpt ?? this;
		}

		// Token: 0x1700226F RID: 8815
		// (get) Token: 0x06007886 RID: 30854 RVA: 0x001B9407 File Offset: 0x001B7607
		public override int TupleElementIndex
		{
			get
			{
				if (this._tupleElementIndex < 0)
				{
					return -1;
				}
				return this._tupleElementIndex >> 1;
			}
		}

		// Token: 0x17002270 RID: 8816
		// (get) Token: 0x06007887 RID: 30855 RVA: 0x001B941C File Offset: 0x001B761C
		public override bool IsDefaultTupleElement
		{
			get
			{
				return (this._tupleElementIndex & -2147483647) == 0;
			}
		}

		// Token: 0x17002271 RID: 8817
		// (get) Token: 0x06007888 RID: 30856 RVA: 0x001B942D File Offset: 0x001B762D
		public override bool IsExplicitlyNamedTupleElement
		{
			get
			{
				return this._tupleElementIndex >= 0 && !this._isImplicitlyDeclared;
			}
		}

		// Token: 0x17002272 RID: 8818
		// (get) Token: 0x06007889 RID: 30857 RVA: 0x001B9443 File Offset: 0x001B7643
		public override FieldSymbol TupleUnderlyingField
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17002273 RID: 8819
		// (get) Token: 0x0600788A RID: 30858 RVA: 0x001B9446 File Offset: 0x001B7646
		public override FieldSymbol OriginalDefinition
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17002274 RID: 8820
		// (get) Token: 0x0600788B RID: 30859 RVA: 0x001B9449 File Offset: 0x001B7649
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return this._locations;
			}
		}

		// Token: 0x17002275 RID: 8821
		// (get) Token: 0x0600788C RID: 30860 RVA: 0x001B9451 File Offset: 0x001B7651
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				if (!this._isImplicitlyDeclared)
				{
					return Symbol.GetDeclaringSyntaxReferenceHelper<CSharpSyntaxNode>(this._locations);
				}
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x17002276 RID: 8822
		// (get) Token: 0x0600788D RID: 30861 RVA: 0x001B946C File Offset: 0x001B766C
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return this._isImplicitlyDeclared;
			}
		}

		// Token: 0x17002277 RID: 8823
		// (get) Token: 0x0600788E RID: 30862 RVA: 0x001B9474 File Offset: 0x001B7674
		public override FieldSymbol CorrespondingTupleField
		{
			get
			{
				return this._correspondingDefaultField;
			}
		}

		// Token: 0x17002278 RID: 8824
		// (get) Token: 0x0600788F RID: 30863 RVA: 0x001B947C File Offset: 0x001B767C
		internal override bool SuppressDynamicAttribute
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007890 RID: 30864 RVA: 0x001B947F File Offset: 0x001B767F
		internal override TypeWithAnnotations GetFieldType(ConsList<FieldSymbol> fieldsBeingBound)
		{
			return this._type;
		}

		// Token: 0x06007891 RID: 30865 RVA: 0x001B9487 File Offset: 0x001B7687
		internal override UseSiteInfo<AssemblySymbol> GetUseSiteInfo()
		{
			return new UseSiteInfo<AssemblySymbol>(this._useSiteDiagnosticInfo);
		}

		// Token: 0x06007892 RID: 30866 RVA: 0x001B9494 File Offset: 0x001B7694
		public sealed override int GetHashCode()
		{
			return Hash.Combine(this.ContainingType.GetHashCode(), this._tupleElementIndex.GetHashCode());
		}

		// Token: 0x06007893 RID: 30867 RVA: 0x001B94BF File Offset: 0x001B76BF
		public override bool Equals(Symbol obj, TypeCompareKind compareKind)
		{
			return this.Equals(obj as TupleErrorFieldSymbol, compareKind);
		}

		// Token: 0x06007894 RID: 30868 RVA: 0x001B94CE File Offset: 0x001B76CE
		public bool Equals(TupleErrorFieldSymbol other, TypeCompareKind compareKind)
		{
			return other == this || (other != null && this._tupleElementIndex == other._tupleElementIndex && TypeSymbol.Equals(this.ContainingType, other.ContainingType, compareKind));
		}

		// Token: 0x06007895 RID: 30869 RVA: 0x001B94FC File Offset: 0x001B76FC
		internal override FieldSymbol AsMember(NamedTypeSymbol newOwner)
		{
			if (newOwner == this.ContainingType)
			{
				return this;
			}
			TupleErrorFieldSymbol tupleErrorFieldSymbol = null;
			if (this._correspondingDefaultField != this)
			{
				tupleErrorFieldSymbol = (TupleErrorFieldSymbol)this._correspondingDefaultField.AsMember(newOwner);
			}
			return new TupleErrorFieldSymbol(newOwner, this.Name, this.TupleElementIndex, this._locations.IsEmpty ? null : this.Locations[0], newOwner.TupleElementTypesWithAnnotations[this.TupleElementIndex], this._useSiteDiagnosticInfo, this._isImplicitlyDeclared, tupleErrorFieldSymbol);
		}

		// Token: 0x04001D00 RID: 7424
		private readonly TypeWithAnnotations _type;

		// Token: 0x04001D01 RID: 7425
		private readonly int _tupleElementIndex;

		// Token: 0x04001D02 RID: 7426
		private readonly ImmutableArray<Location> _locations;

		// Token: 0x04001D03 RID: 7427
		private readonly DiagnosticInfo _useSiteDiagnosticInfo;

		// Token: 0x04001D04 RID: 7428
		private readonly TupleErrorFieldSymbol _correspondingDefaultField;

		// Token: 0x04001D05 RID: 7429
		private readonly bool _isImplicitlyDeclared;
	}
}
