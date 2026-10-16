using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection.Metadata;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004B4 RID: 1204
	internal sealed class PointerTypeSymbol : TypeSymbol, IPointerTypeReference, ITypeReference, IReference
	{
		// Token: 0x0600660B RID: 26123 RVA: 0x001818D4 File Offset: 0x0017FAD4
		ITypeReference IPointerTypeReference.GetTargetType(EmitContext context)
		{
			ITypeReference typeReference = ((PEModuleBuilder)context.Module).Translate(this.AdaptedPointerTypeSymbol.PointedAtType, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics);
			if (this.AdaptedPointerTypeSymbol.PointedAtTypeWithAnnotations.CustomModifiers.Length == 0)
			{
				return typeReference;
			}
			return new ModifiedTypeReference(typeReference, ImmutableArray<ICustomModifier>.CastUp<CustomModifier>(this.AdaptedPointerTypeSymbol.PointedAtTypeWithAnnotations.CustomModifiers));
		}

		// Token: 0x1700191F RID: 6431
		// (get) Token: 0x0600660C RID: 26124 RVA: 0x0018194B File Offset: 0x0017FB4B
		bool ITypeReference.IsEnum
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001920 RID: 6432
		// (get) Token: 0x0600660D RID: 26125 RVA: 0x0018194E File Offset: 0x0017FB4E
		bool ITypeReference.IsValueType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600660E RID: 26126 RVA: 0x00181951 File Offset: 0x0017FB51
		ITypeDefinition ITypeReference.GetResolvedType(EmitContext context)
		{
			return null;
		}

		// Token: 0x17001921 RID: 6433
		// (get) Token: 0x0600660F RID: 26127 RVA: 0x00181954 File Offset: 0x0017FB54
		Microsoft.Cci.PrimitiveTypeCode ITypeReference.TypeCode
		{
			get
			{
				return Microsoft.Cci.PrimitiveTypeCode.Pointer;
			}
		}

		// Token: 0x17001922 RID: 6434
		// (get) Token: 0x06006610 RID: 26128 RVA: 0x00181958 File Offset: 0x0017FB58
		TypeDefinitionHandle ITypeReference.TypeDef
		{
			get
			{
				return default(TypeDefinitionHandle);
			}
		}

		// Token: 0x17001923 RID: 6435
		// (get) Token: 0x06006611 RID: 26129 RVA: 0x0018196E File Offset: 0x0017FB6E
		IGenericMethodParameterReference ITypeReference.AsGenericMethodParameterReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001924 RID: 6436
		// (get) Token: 0x06006612 RID: 26130 RVA: 0x00181971 File Offset: 0x0017FB71
		IGenericTypeInstanceReference ITypeReference.AsGenericTypeInstanceReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001925 RID: 6437
		// (get) Token: 0x06006613 RID: 26131 RVA: 0x00181974 File Offset: 0x0017FB74
		IGenericTypeParameterReference ITypeReference.AsGenericTypeParameterReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06006614 RID: 26132 RVA: 0x00181977 File Offset: 0x0017FB77
		INamespaceTypeDefinition ITypeReference.AsNamespaceTypeDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x17001926 RID: 6438
		// (get) Token: 0x06006615 RID: 26133 RVA: 0x0018197A File Offset: 0x0017FB7A
		INamespaceTypeReference ITypeReference.AsNamespaceTypeReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06006616 RID: 26134 RVA: 0x0018197D File Offset: 0x0017FB7D
		INestedTypeDefinition ITypeReference.AsNestedTypeDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x17001927 RID: 6439
		// (get) Token: 0x06006617 RID: 26135 RVA: 0x00181980 File Offset: 0x0017FB80
		INestedTypeReference ITypeReference.AsNestedTypeReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001928 RID: 6440
		// (get) Token: 0x06006618 RID: 26136 RVA: 0x00181983 File Offset: 0x0017FB83
		ISpecializedNestedTypeReference ITypeReference.AsSpecializedNestedTypeReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06006619 RID: 26137 RVA: 0x00181986 File Offset: 0x0017FB86
		ITypeDefinition ITypeReference.AsTypeDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x0600661A RID: 26138 RVA: 0x00181989 File Offset: 0x0017FB89
		void IReference.Dispatch(MetadataVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600661B RID: 26139 RVA: 0x00181992 File Offset: 0x0017FB92
		IDefinition IReference.AsDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x17001929 RID: 6441
		// (get) Token: 0x0600661C RID: 26140 RVA: 0x00181995 File Offset: 0x0017FB95
		internal PointerTypeSymbol AdaptedPointerTypeSymbol
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600661D RID: 26141 RVA: 0x00181998 File Offset: 0x0017FB98
		internal new PointerTypeSymbol GetCciAdapter()
		{
			return this;
		}

		// Token: 0x0600661E RID: 26142 RVA: 0x0018199B File Offset: 0x0017FB9B
		internal PointerTypeSymbol(TypeWithAnnotations pointedAtType)
		{
			this._pointedAtType = pointedAtType;
		}

		// Token: 0x1700192A RID: 6442
		// (get) Token: 0x0600661F RID: 26143 RVA: 0x001819AA File Offset: 0x0017FBAA
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return Accessibility.NotApplicable;
			}
		}

		// Token: 0x1700192B RID: 6443
		// (get) Token: 0x06006620 RID: 26144 RVA: 0x001819AD File Offset: 0x0017FBAD
		public override bool IsStatic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700192C RID: 6444
		// (get) Token: 0x06006621 RID: 26145 RVA: 0x001819B0 File Offset: 0x0017FBB0
		public override bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700192D RID: 6445
		// (get) Token: 0x06006622 RID: 26146 RVA: 0x001819B3 File Offset: 0x0017FBB3
		public override bool IsSealed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700192E RID: 6446
		// (get) Token: 0x06006623 RID: 26147 RVA: 0x001819B6 File Offset: 0x0017FBB6
		public TypeWithAnnotations PointedAtTypeWithAnnotations
		{
			get
			{
				return this._pointedAtType;
			}
		}

		// Token: 0x1700192F RID: 6447
		// (get) Token: 0x06006624 RID: 26148 RVA: 0x001819C0 File Offset: 0x0017FBC0
		public TypeSymbol PointedAtType
		{
			get
			{
				return this.PointedAtTypeWithAnnotations.Type;
			}
		}

		// Token: 0x17001930 RID: 6448
		// (get) Token: 0x06006625 RID: 26149 RVA: 0x001819DB File Offset: 0x0017FBDB
		internal override NamedTypeSymbol BaseTypeNoUseSiteDiagnostics
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06006626 RID: 26150 RVA: 0x001819DE File Offset: 0x0017FBDE
		internal override ImmutableArray<NamedTypeSymbol> InterfacesNoUseSiteDiagnostics(ConsList<TypeSymbol> basesBeingResolved)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x17001931 RID: 6449
		// (get) Token: 0x06006627 RID: 26151 RVA: 0x001819E5 File Offset: 0x0017FBE5
		public override bool IsReferenceType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001932 RID: 6450
		// (get) Token: 0x06006628 RID: 26152 RVA: 0x001819E8 File Offset: 0x0017FBE8
		public override bool IsValueType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006629 RID: 26153 RVA: 0x001819EB File Offset: 0x0017FBEB
		internal sealed override ManagedKind GetManagedKind(ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			return ManagedKind.Unmanaged;
		}

		// Token: 0x17001933 RID: 6451
		// (get) Token: 0x0600662A RID: 26154 RVA: 0x001819EE File Offset: 0x0017FBEE
		public sealed override bool IsRefLikeType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001934 RID: 6452
		// (get) Token: 0x0600662B RID: 26155 RVA: 0x001819F1 File Offset: 0x0017FBF1
		public sealed override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001935 RID: 6453
		// (get) Token: 0x0600662C RID: 26156 RVA: 0x001819F4 File Offset: 0x0017FBF4
		internal sealed override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600662D RID: 26157 RVA: 0x001819F7 File Offset: 0x0017FBF7
		public override ImmutableArray<Symbol> GetMembers()
		{
			return ImmutableArray<Symbol>.Empty;
		}

		// Token: 0x0600662E RID: 26158 RVA: 0x001819FE File Offset: 0x0017FBFE
		public override ImmutableArray<Symbol> GetMembers(string name)
		{
			return ImmutableArray<Symbol>.Empty;
		}

		// Token: 0x0600662F RID: 26159 RVA: 0x00181A05 File Offset: 0x0017FC05
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers()
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x06006630 RID: 26160 RVA: 0x00181A0C File Offset: 0x0017FC0C
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x06006631 RID: 26161 RVA: 0x00181A13 File Offset: 0x0017FC13
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name, int arity)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x17001936 RID: 6454
		// (get) Token: 0x06006632 RID: 26162 RVA: 0x00181A1A File Offset: 0x0017FC1A
		public override SymbolKind Kind
		{
			get
			{
				return SymbolKind.PointerType;
			}
		}

		// Token: 0x17001937 RID: 6455
		// (get) Token: 0x06006633 RID: 26163 RVA: 0x00181A1E File Offset: 0x0017FC1E
		public override TypeKind TypeKind
		{
			get
			{
				return TypeKind.Pointer;
			}
		}

		// Token: 0x17001938 RID: 6456
		// (get) Token: 0x06006634 RID: 26164 RVA: 0x00181A22 File Offset: 0x0017FC22
		public override Symbol ContainingSymbol
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001939 RID: 6457
		// (get) Token: 0x06006635 RID: 26165 RVA: 0x00181A25 File Offset: 0x0017FC25
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return ImmutableArray<Location>.Empty;
			}
		}

		// Token: 0x1700193A RID: 6458
		// (get) Token: 0x06006636 RID: 26166 RVA: 0x00181A2C File Offset: 0x0017FC2C
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x06006637 RID: 26167 RVA: 0x00181A33 File Offset: 0x0017FC33
		internal override TResult Accept<TArgument, TResult>(CSharpSymbolVisitor<TArgument, TResult> visitor, TArgument argument)
		{
			return visitor.VisitPointerType(this, argument);
		}

		// Token: 0x06006638 RID: 26168 RVA: 0x00181A3D File Offset: 0x0017FC3D
		public override void Accept(CSharpSymbolVisitor visitor)
		{
			visitor.VisitPointerType(this);
		}

		// Token: 0x06006639 RID: 26169 RVA: 0x00181A46 File Offset: 0x0017FC46
		public override TResult Accept<TResult>(CSharpSymbolVisitor<TResult> visitor)
		{
			return visitor.VisitPointerType(this);
		}

		// Token: 0x0600663A RID: 26170 RVA: 0x00181A50 File Offset: 0x0017FC50
		public override int GetHashCode()
		{
			int num = 0;
			TypeSymbol typeSymbol = this;
			while (typeSymbol.TypeKind == TypeKind.Pointer)
			{
				num++;
				typeSymbol = ((PointerTypeSymbol)typeSymbol).PointedAtType;
			}
			return Hash.Combine<TypeSymbol>(typeSymbol, num);
		}

		// Token: 0x0600663B RID: 26171 RVA: 0x00181A84 File Offset: 0x0017FC84
		internal override bool Equals(TypeSymbol t2, TypeCompareKind comparison)
		{
			return this.Equals(t2 as PointerTypeSymbol, comparison);
		}

		// Token: 0x0600663C RID: 26172 RVA: 0x00181A93 File Offset: 0x0017FC93
		private bool Equals(PointerTypeSymbol other, TypeCompareKind comparison)
		{
			return this == other || (other != null && other._pointedAtType.Equals(this._pointedAtType, comparison));
		}

		// Token: 0x0600663D RID: 26173 RVA: 0x00181AB8 File Offset: 0x0017FCB8
		internal override void AddNullableTransforms(ArrayBuilder<byte> transforms)
		{
			this.PointedAtTypeWithAnnotations.AddNullableTransforms(transforms);
		}

		// Token: 0x0600663E RID: 26174 RVA: 0x00181AD4 File Offset: 0x0017FCD4
		internal override bool ApplyNullableTransforms(byte defaultTransformFlag, ImmutableArray<byte> transforms, ref int position, out TypeSymbol result)
		{
			TypeWithAnnotations typeWithAnnotations;
			if (!this.PointedAtTypeWithAnnotations.ApplyNullableTransforms(defaultTransformFlag, transforms, ref position, out typeWithAnnotations))
			{
				result = this;
				return false;
			}
			result = this.WithPointedAtType(typeWithAnnotations);
			return true;
		}

		// Token: 0x0600663F RID: 26175 RVA: 0x00181B07 File Offset: 0x0017FD07
		internal override TypeSymbol SetNullabilityForReferenceTypes(Func<TypeWithAnnotations, TypeWithAnnotations> transform)
		{
			return this.WithPointedAtType(transform.Invoke(this.PointedAtTypeWithAnnotations));
		}

		// Token: 0x06006640 RID: 26176 RVA: 0x00181B1C File Offset: 0x0017FD1C
		internal override TypeSymbol MergeEquivalentTypes(TypeSymbol other, VarianceKind variance)
		{
			TypeWithAnnotations typeWithAnnotations = this.PointedAtTypeWithAnnotations.MergeEquivalentTypes(((PointerTypeSymbol)other).PointedAtTypeWithAnnotations, VarianceKind.None);
			return this.WithPointedAtType(typeWithAnnotations);
		}

		// Token: 0x06006641 RID: 26177 RVA: 0x00181B4C File Offset: 0x0017FD4C
		internal PointerTypeSymbol WithPointedAtType(TypeWithAnnotations newPointedAtType)
		{
			if (!this.PointedAtTypeWithAnnotations.IsSameAs(newPointedAtType))
			{
				return new PointerTypeSymbol(newPointedAtType);
			}
			return this;
		}

		// Token: 0x06006642 RID: 26178 RVA: 0x00181B74 File Offset: 0x0017FD74
		internal override UseSiteInfo<AssemblySymbol> GetUseSiteInfo()
		{
			UseSiteInfo<AssemblySymbol> useSiteInfo = default(UseSiteInfo<AssemblySymbol>);
			base.DeriveUseSiteInfoFromType(ref useSiteInfo, this.PointedAtTypeWithAnnotations, Symbol.AllowedRequiredModifierType.None);
			return useSiteInfo;
		}

		// Token: 0x06006643 RID: 26179 RVA: 0x00181B9C File Offset: 0x0017FD9C
		internal override bool GetUnificationUseSiteDiagnosticRecursive(ref DiagnosticInfo result, Symbol owner, ref HashSet<TypeSymbol> checkedTypes)
		{
			return this.PointedAtTypeWithAnnotations.GetUnificationUseSiteDiagnosticRecursive(ref result, owner, ref checkedTypes);
		}

		// Token: 0x06006644 RID: 26180 RVA: 0x00181BBA File Offset: 0x0017FDBA
		protected override ISymbol CreateISymbol()
		{
			return new PointerTypeSymbol(this, base.DefaultNullableAnnotation);
		}

		// Token: 0x06006645 RID: 26181 RVA: 0x00181BC8 File Offset: 0x0017FDC8
		protected override ITypeSymbol CreateITypeSymbol(NullableAnnotation nullableAnnotation)
		{
			return new PointerTypeSymbol(this, nullableAnnotation);
		}

		// Token: 0x1700193B RID: 6459
		// (get) Token: 0x06006646 RID: 26182 RVA: 0x00181BD1 File Offset: 0x0017FDD1
		internal override bool IsRecord
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700193C RID: 6460
		// (get) Token: 0x06006647 RID: 26183 RVA: 0x00181BD4 File Offset: 0x0017FDD4
		internal override bool IsRecordStruct
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040019A1 RID: 6561
		private readonly TypeWithAnnotations _pointedAtType;
	}
}
