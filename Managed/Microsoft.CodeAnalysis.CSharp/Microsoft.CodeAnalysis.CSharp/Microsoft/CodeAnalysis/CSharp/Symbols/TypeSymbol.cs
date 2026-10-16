using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020005A8 RID: 1448
	internal abstract class TypeSymbol : NamespaceOrTypeSymbol, ITypeSymbolInternal, INamespaceOrTypeSymbolInternal, ISymbolInternal
	{
		// Token: 0x060078CA RID: 30922 RVA: 0x001B9CF4 File Offset: 0x001B7EF4
		private TypeSymbol.InterfaceInfo GetInterfaceInfo()
		{
			TypeSymbol.InterfaceInfo interfaceInfo = this._lazyInterfaceInfo;
			if (interfaceInfo != null)
			{
				return interfaceInfo;
			}
			for (TypeSymbol typeSymbol = this; typeSymbol != null; typeSymbol = typeSymbol.BaseTypeNoUseSiteDiagnostics)
			{
				if (!((typeSymbol.TypeKind == TypeKind.TypeParameter) ? ((TypeParameterSymbol)typeSymbol).EffectiveInterfacesNoUseSiteDiagnostics : typeSymbol.InterfacesNoUseSiteDiagnostics(null)).IsEmpty)
				{
					interfaceInfo = new TypeSymbol.InterfaceInfo();
					return Interlocked.CompareExchange<TypeSymbol.InterfaceInfo>(ref this._lazyInterfaceInfo, interfaceInfo, null) ?? interfaceInfo;
				}
			}
			return this._lazyInterfaceInfo = TypeSymbol.s_noInterfaces;
		}

		// Token: 0x1700228A RID: 8842
		// (get) Token: 0x060078CB RID: 30923 RVA: 0x001B9D6A File Offset: 0x001B7F6A
		public new TypeSymbol OriginalDefinition
		{
			get
			{
				return this.OriginalTypeSymbolDefinition;
			}
		}

		// Token: 0x1700228B RID: 8843
		// (get) Token: 0x060078CC RID: 30924 RVA: 0x001B9D72 File Offset: 0x001B7F72
		protected virtual TypeSymbol OriginalTypeSymbolDefinition
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700228C RID: 8844
		// (get) Token: 0x060078CD RID: 30925 RVA: 0x001B9D75 File Offset: 0x001B7F75
		protected sealed override Symbol OriginalSymbolDefinition
		{
			get
			{
				return this.OriginalTypeSymbolDefinition;
			}
		}

		// Token: 0x1700228D RID: 8845
		// (get) Token: 0x060078CE RID: 30926
		internal abstract NamedTypeSymbol BaseTypeNoUseSiteDiagnostics { get; }

		// Token: 0x060078CF RID: 30927 RVA: 0x001B9D80 File Offset: 0x001B7F80
		internal NamedTypeSymbol BaseTypeWithDefinitionUseSiteDiagnostics(ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			NamedTypeSymbol baseTypeNoUseSiteDiagnostics = this.BaseTypeNoUseSiteDiagnostics;
			if (baseTypeNoUseSiteDiagnostics != null)
			{
				baseTypeNoUseSiteDiagnostics.OriginalDefinition.AddUseSiteInfo(ref useSiteInfo, true);
			}
			return baseTypeNoUseSiteDiagnostics;
		}

		// Token: 0x060078D0 RID: 30928 RVA: 0x001B9DA8 File Offset: 0x001B7FA8
		internal NamedTypeSymbol BaseTypeOriginalDefinition(ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			NamedTypeSymbol namedTypeSymbol = this.BaseTypeNoUseSiteDiagnostics;
			if (namedTypeSymbol != null)
			{
				namedTypeSymbol = namedTypeSymbol.OriginalDefinition;
				namedTypeSymbol.AddUseSiteInfo(ref useSiteInfo, true);
			}
			return namedTypeSymbol;
		}

		// Token: 0x060078D1 RID: 30929
		internal abstract ImmutableArray<NamedTypeSymbol> InterfacesNoUseSiteDiagnostics(ConsList<TypeSymbol> basesBeingResolved = null);

		// Token: 0x1700228E RID: 8846
		// (get) Token: 0x060078D2 RID: 30930 RVA: 0x001B9DCF File Offset: 0x001B7FCF
		internal ImmutableArray<NamedTypeSymbol> AllInterfacesNoUseSiteDiagnostics
		{
			get
			{
				return this.GetAllInterfaces();
			}
		}

		// Token: 0x060078D3 RID: 30931 RVA: 0x001B9DD8 File Offset: 0x001B7FD8
		internal ImmutableArray<NamedTypeSymbol> AllInterfacesWithDefinitionUseSiteDiagnostics(ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			ImmutableArray<NamedTypeSymbol> allInterfacesNoUseSiteDiagnostics = this.AllInterfacesNoUseSiteDiagnostics;
			TypeSymbol typeSymbol = this;
			do
			{
				typeSymbol = typeSymbol.BaseTypeWithDefinitionUseSiteDiagnostics(ref useSiteInfo);
			}
			while (typeSymbol != null);
			foreach (NamedTypeSymbol namedTypeSymbol in allInterfacesNoUseSiteDiagnostics)
			{
				namedTypeSymbol.OriginalDefinition.AddUseSiteInfo(ref useSiteInfo, true);
			}
			return allInterfacesNoUseSiteDiagnostics;
		}

		// Token: 0x1700228F RID: 8847
		// (get) Token: 0x060078D4 RID: 30932 RVA: 0x001B9E20 File Offset: 0x001B8020
		internal TypeSymbol EffectiveTypeNoUseSiteDiagnostics
		{
			get
			{
				if (!this.IsTypeParameter())
				{
					return this;
				}
				return ((TypeParameterSymbol)this).EffectiveBaseClassNoUseSiteDiagnostics;
			}
		}

		// Token: 0x060078D5 RID: 30933 RVA: 0x001B9E37 File Offset: 0x001B8037
		internal TypeSymbol EffectiveType(ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (!this.IsTypeParameter())
			{
				return this;
			}
			return ((TypeParameterSymbol)this).EffectiveBaseClass(ref useSiteInfo);
		}

		// Token: 0x060078D6 RID: 30934 RVA: 0x001B9E50 File Offset: 0x001B8050
		internal bool IsDerivedFrom(TypeSymbol type, TypeCompareKind comparison, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (this == type)
			{
				return false;
			}
			for (NamedTypeSymbol namedTypeSymbol = this.BaseTypeWithDefinitionUseSiteDiagnostics(ref useSiteInfo); namedTypeSymbol != null; namedTypeSymbol = namedTypeSymbol.BaseTypeWithDefinitionUseSiteDiagnostics(ref useSiteInfo))
			{
				if (type.Equals(namedTypeSymbol, comparison))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060078D7 RID: 30935 RVA: 0x001B9E85 File Offset: 0x001B8085
		internal bool IsEqualToOrDerivedFrom(TypeSymbol type, TypeCompareKind comparison, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			return this.Equals(type, comparison) || this.IsDerivedFrom(type, comparison, ref useSiteInfo);
		}

		// Token: 0x060078D8 RID: 30936 RVA: 0x001B9E9C File Offset: 0x001B809C
		internal virtual bool Equals(TypeSymbol t2, TypeCompareKind compareKind)
		{
			return this == t2;
		}

		// Token: 0x060078D9 RID: 30937 RVA: 0x001B9EA4 File Offset: 0x001B80A4
		public sealed override bool Equals(Symbol other, TypeCompareKind compareKind)
		{
			TypeSymbol typeSymbol = other as TypeSymbol;
			return typeSymbol != null && this.Equals(typeSymbol, compareKind);
		}

		// Token: 0x060078DA RID: 30938 RVA: 0x001B9EC5 File Offset: 0x001B80C5
		public override int GetHashCode()
		{
			return RuntimeHelpers.GetHashCode(this);
		}

		// Token: 0x060078DB RID: 30939 RVA: 0x001B9ED0 File Offset: 0x001B80D0
		protected virtual ImmutableArray<NamedTypeSymbol> GetAllInterfaces()
		{
			TypeSymbol.InterfaceInfo interfaceInfo = this.GetInterfaceInfo();
			if (interfaceInfo == TypeSymbol.s_noInterfaces)
			{
				return ImmutableArray<NamedTypeSymbol>.Empty;
			}
			if (interfaceInfo.allInterfaces.IsDefault)
			{
				ImmutableInterlocked.InterlockedInitialize<NamedTypeSymbol>(ref interfaceInfo.allInterfaces, this.MakeAllInterfaces());
			}
			return interfaceInfo.allInterfaces;
		}

		// Token: 0x060078DC RID: 30940 RVA: 0x001B9F18 File Offset: 0x001B8118
		protected virtual ImmutableArray<NamedTypeSymbol> MakeAllInterfaces()
		{
			ArrayBuilder<NamedTypeSymbol> instance = ArrayBuilder<NamedTypeSymbol>.GetInstance();
			HashSet<NamedTypeSymbol> hashSet = new HashSet<NamedTypeSymbol>(SymbolEqualityComparer.ConsiderEverything);
			for (TypeSymbol typeSymbol = this; typeSymbol != null; typeSymbol = typeSymbol.BaseTypeNoUseSiteDiagnostics)
			{
				ImmutableArray<NamedTypeSymbol> immutableArray = ((typeSymbol.TypeKind == TypeKind.TypeParameter) ? ((TypeParameterSymbol)typeSymbol).EffectiveInterfacesNoUseSiteDiagnostics : typeSymbol.InterfacesNoUseSiteDiagnostics(null));
				for (int i = immutableArray.Length - 1; i >= 0; i--)
				{
					TypeSymbol.<MakeAllInterfaces>g__addAllInterfaces|29_0(immutableArray[i], hashSet, instance);
				}
			}
			instance.ReverseContents();
			return instance.ToImmutableAndFree();
		}

		// Token: 0x17002290 RID: 8848
		// (get) Token: 0x060078DD RID: 30941 RVA: 0x001B9F98 File Offset: 0x001B8198
		internal MultiDictionary<NamedTypeSymbol, NamedTypeSymbol> InterfacesAndTheirBaseInterfacesNoUseSiteDiagnostics
		{
			get
			{
				TypeSymbol.InterfaceInfo interfaceInfo = this.GetInterfaceInfo();
				if (interfaceInfo == TypeSymbol.s_noInterfaces)
				{
					return TypeSymbol.InterfaceInfo.EmptyInterfacesAndTheirBaseInterfaces;
				}
				if (interfaceInfo.interfacesAndTheirBaseInterfaces == null)
				{
					Interlocked.CompareExchange<MultiDictionary<NamedTypeSymbol, NamedTypeSymbol>>(ref interfaceInfo.interfacesAndTheirBaseInterfaces, TypeSymbol.MakeInterfacesAndTheirBaseInterfaces(this.InterfacesNoUseSiteDiagnostics(null)), null);
				}
				return interfaceInfo.interfacesAndTheirBaseInterfaces;
			}
		}

		// Token: 0x060078DE RID: 30942 RVA: 0x001B9FE4 File Offset: 0x001B81E4
		internal MultiDictionary<NamedTypeSymbol, NamedTypeSymbol> InterfacesAndTheirBaseInterfacesWithDefinitionUseSiteDiagnostics(ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			MultiDictionary<NamedTypeSymbol, NamedTypeSymbol> interfacesAndTheirBaseInterfacesNoUseSiteDiagnostics = this.InterfacesAndTheirBaseInterfacesNoUseSiteDiagnostics;
			foreach (NamedTypeSymbol namedTypeSymbol in interfacesAndTheirBaseInterfacesNoUseSiteDiagnostics.Keys)
			{
				namedTypeSymbol.OriginalDefinition.AddUseSiteInfo(ref useSiteInfo, true);
			}
			return interfacesAndTheirBaseInterfacesNoUseSiteDiagnostics;
		}

		// Token: 0x060078DF RID: 30943 RVA: 0x001BA044 File Offset: 0x001B8244
		private static MultiDictionary<NamedTypeSymbol, NamedTypeSymbol> MakeInterfacesAndTheirBaseInterfaces(ImmutableArray<NamedTypeSymbol> declaredInterfaces)
		{
			MultiDictionary<NamedTypeSymbol, NamedTypeSymbol> multiDictionary = new MultiDictionary<NamedTypeSymbol, NamedTypeSymbol>(declaredInterfaces.Length, SymbolEqualityComparer.CLRSignature, SymbolEqualityComparer.ConsiderEverything);
			foreach (NamedTypeSymbol namedTypeSymbol in declaredInterfaces)
			{
				if (multiDictionary.Add(namedTypeSymbol, namedTypeSymbol))
				{
					foreach (NamedTypeSymbol namedTypeSymbol2 in namedTypeSymbol.AllInterfacesNoUseSiteDiagnostics)
					{
						multiDictionary.Add(namedTypeSymbol2, namedTypeSymbol2);
					}
				}
			}
			return multiDictionary;
		}

		// Token: 0x060078E0 RID: 30944 RVA: 0x001BA0C0 File Offset: 0x001B82C0
		public Symbol FindImplementationForInterfaceMember(Symbol interfaceMember)
		{
			if (interfaceMember == null)
			{
				throw new ArgumentNullException("interfaceMember");
			}
			if (!interfaceMember.IsImplementableInterfaceMember())
			{
				return null;
			}
			if (this.IsInterfaceType())
			{
				CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
				return TypeSymbol.FindMostSpecificImplementation(interfaceMember, (NamedTypeSymbol)this, ref discarded);
			}
			return this.FindImplementationForInterfaceMemberInNonInterface(interfaceMember, false);
		}

		// Token: 0x17002291 RID: 8849
		// (get) Token: 0x060078E1 RID: 30945
		public abstract bool IsReferenceType { get; }

		// Token: 0x17002292 RID: 8850
		// (get) Token: 0x060078E2 RID: 30946
		public abstract bool IsValueType { get; }

		// Token: 0x060078E3 RID: 30947 RVA: 0x001BA10A File Offset: 0x001B830A
		internal TypeSymbol()
		{
		}

		// Token: 0x17002293 RID: 8851
		// (get) Token: 0x060078E4 RID: 30948
		public abstract TypeKind TypeKind { get; }

		// Token: 0x17002294 RID: 8852
		// (get) Token: 0x060078E5 RID: 30949 RVA: 0x001BA112 File Offset: 0x001B8312
		public virtual SpecialType SpecialType
		{
			get
			{
				return SpecialType.None;
			}
		}

		// Token: 0x17002295 RID: 8853
		// (get) Token: 0x060078E6 RID: 30950 RVA: 0x001BA118 File Offset: 0x001B8318
		internal PrimitiveTypeCode PrimitiveTypeCode
		{
			get
			{
				TypeKind typeKind = this.TypeKind;
				PrimitiveTypeCode primitiveTypeCode;
				if (typeKind != TypeKind.Pointer)
				{
					if (typeKind != TypeKind.FunctionPointer)
					{
						primitiveTypeCode = SpecialTypes.GetTypeCode(this.SpecialType);
					}
					else
					{
						primitiveTypeCode = PrimitiveTypeCode.FunctionPointer;
					}
				}
				else
				{
					primitiveTypeCode = PrimitiveTypeCode.Pointer;
				}
				return primitiveTypeCode;
			}
		}

		// Token: 0x17002296 RID: 8854
		// (get) Token: 0x060078E7 RID: 30951 RVA: 0x001BA14F File Offset: 0x001B834F
		protected override int HighestPriorityUseSiteError
		{
			get
			{
				return 648;
			}
		}

		// Token: 0x17002297 RID: 8855
		// (get) Token: 0x060078E8 RID: 30952 RVA: 0x001BA158 File Offset: 0x001B8358
		public sealed override bool HasUnsupportedMetadata
		{
			get
			{
				DiagnosticInfo diagnosticInfo = this.GetUseSiteInfo().DiagnosticInfo;
				return diagnosticInfo != null && diagnosticInfo.Code == 648;
			}
		}

		// Token: 0x060078E9 RID: 30953
		internal abstract bool GetUnificationUseSiteDiagnosticRecursive(ref DiagnosticInfo result, Symbol owner, ref HashSet<TypeSymbol> checkedTypes);

		// Token: 0x17002298 RID: 8856
		// (get) Token: 0x060078EA RID: 30954 RVA: 0x001BA183 File Offset: 0x001B8383
		public virtual bool IsAnonymousType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002299 RID: 8857
		// (get) Token: 0x060078EB RID: 30955 RVA: 0x001BA186 File Offset: 0x001B8386
		public virtual bool IsTupleType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700229A RID: 8858
		// (get) Token: 0x060078EC RID: 30956 RVA: 0x001BA189 File Offset: 0x001B8389
		internal virtual bool IsNativeIntegerType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060078ED RID: 30957 RVA: 0x001BA18C File Offset: 0x001B838C
		public bool IsTupleTypeOfCardinality(int targetCardinality)
		{
			return this.IsTupleType && this.TupleElementTypesWithAnnotations.Length == targetCardinality;
		}

		// Token: 0x1700229B RID: 8859
		// (get) Token: 0x060078EE RID: 30958 RVA: 0x001BA1B4 File Offset: 0x001B83B4
		public virtual ImmutableArray<TypeWithAnnotations> TupleElementTypesWithAnnotations
		{
			get
			{
				return default(ImmutableArray<TypeWithAnnotations>);
			}
		}

		// Token: 0x1700229C RID: 8860
		// (get) Token: 0x060078EF RID: 30959 RVA: 0x001BA1CC File Offset: 0x001B83CC
		public virtual ImmutableArray<string> TupleElementNames
		{
			get
			{
				return default(ImmutableArray<string>);
			}
		}

		// Token: 0x1700229D RID: 8861
		// (get) Token: 0x060078F0 RID: 30960 RVA: 0x001BA1E4 File Offset: 0x001B83E4
		public virtual ImmutableArray<FieldSymbol> TupleElements
		{
			get
			{
				return default(ImmutableArray<FieldSymbol>);
			}
		}

		// Token: 0x060078F1 RID: 30961 RVA: 0x001BA1FA File Offset: 0x001B83FA
		internal bool IsManagedType([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			return this.GetManagedKind(ref useSiteInfo) == ManagedKind.Managed;
		}

		// Token: 0x1700229E RID: 8862
		// (get) Token: 0x060078F2 RID: 30962 RVA: 0x001BA208 File Offset: 0x001B8408
		internal bool IsManagedTypeNoUseSiteDiagnostics
		{
			get
			{
				CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
				return this.IsManagedType(ref discarded);
			}
		}

		// Token: 0x060078F3 RID: 30963
		internal abstract ManagedKind GetManagedKind([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo);

		// Token: 0x1700229F RID: 8863
		// (get) Token: 0x060078F4 RID: 30964 RVA: 0x001BA224 File Offset: 0x001B8424
		internal ManagedKind ManagedKindNoUseSiteDiagnostics
		{
			get
			{
				CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
				return this.GetManagedKind(ref discarded);
			}
		}

		// Token: 0x060078F5 RID: 30965 RVA: 0x001BA240 File Offset: 0x001B8440
		internal bool NeedsNullableAttribute()
		{
			return TypeWithAnnotations.NeedsNullableAttribute(default(TypeWithAnnotations), this);
		}

		// Token: 0x060078F6 RID: 30966
		internal abstract void AddNullableTransforms(ArrayBuilder<byte> transforms);

		// Token: 0x060078F7 RID: 30967
		internal abstract bool ApplyNullableTransforms(byte defaultTransformFlag, ImmutableArray<byte> transforms, ref int position, out TypeSymbol result);

		// Token: 0x060078F8 RID: 30968
		internal abstract TypeSymbol SetNullabilityForReferenceTypes(Func<TypeWithAnnotations, TypeWithAnnotations> transform);

		// Token: 0x060078F9 RID: 30969 RVA: 0x001BA25C File Offset: 0x001B845C
		internal TypeSymbol SetUnknownNullabilityForReferenceTypes()
		{
			return this.SetNullabilityForReferenceTypes(TypeSymbol.s_setUnknownNullability);
		}

		// Token: 0x060078FA RID: 30970
		internal abstract TypeSymbol MergeEquivalentTypes(TypeSymbol other, VarianceKind variance);

		// Token: 0x170022A0 RID: 8864
		// (get) Token: 0x060078FB RID: 30971
		public abstract bool IsRefLikeType { get; }

		// Token: 0x170022A1 RID: 8865
		// (get) Token: 0x060078FC RID: 30972
		public abstract bool IsReadOnly { get; }

		// Token: 0x060078FD RID: 30973 RVA: 0x001BA269 File Offset: 0x001B8469
		public string ToDisplayString(NullableFlowState topLevelNullability, SymbolDisplayFormat format = null)
		{
			return SymbolDisplay.ToDisplayString((ITypeSymbol)base.ISymbol, topLevelNullability, format);
		}

		// Token: 0x060078FE RID: 30974 RVA: 0x001BA27D File Offset: 0x001B847D
		public ImmutableArray<SymbolDisplayPart> ToDisplayParts(NullableFlowState topLevelNullability, SymbolDisplayFormat format = null)
		{
			return SymbolDisplay.ToDisplayParts((ITypeSymbol)base.ISymbol, topLevelNullability, format);
		}

		// Token: 0x060078FF RID: 30975 RVA: 0x001BA291 File Offset: 0x001B8491
		public string ToMinimalDisplayString(SemanticModel semanticModel, NullableFlowState topLevelNullability, int position, SymbolDisplayFormat format = null)
		{
			return SymbolDisplay.ToMinimalDisplayString((ITypeSymbol)base.ISymbol, topLevelNullability, semanticModel, position, format);
		}

		// Token: 0x06007900 RID: 30976 RVA: 0x001BA2A8 File Offset: 0x001B84A8
		public ImmutableArray<SymbolDisplayPart> ToMinimalDisplayParts(SemanticModel semanticModel, NullableFlowState topLevelNullability, int position, SymbolDisplayFormat format = null)
		{
			return SymbolDisplay.ToMinimalDisplayParts((ITypeSymbol)base.ISymbol, topLevelNullability, semanticModel, position, format);
		}

		// Token: 0x06007901 RID: 30977 RVA: 0x001BA2C0 File Offset: 0x001B84C0
		protected TypeSymbol.SymbolAndDiagnostics FindImplementationForInterfaceMemberInNonInterfaceWithDiagnostics(Symbol interfaceMember, bool ignoreImplementationInInterfacesIfResultIsNotReady = false)
		{
			if (this.IsInterfaceType())
			{
				return TypeSymbol.SymbolAndDiagnostics.Empty;
			}
			NamedTypeSymbol containingType = interfaceMember.ContainingType;
			if (containingType == null || !containingType.IsInterface)
			{
				return TypeSymbol.SymbolAndDiagnostics.Empty;
			}
			SymbolKind kind = interfaceMember.Kind;
			if (kind != SymbolKind.Event && kind != SymbolKind.Method && kind != SymbolKind.Property)
			{
				return TypeSymbol.SymbolAndDiagnostics.Empty;
			}
			TypeSymbol.InterfaceInfo interfaceInfo = this.GetInterfaceInfo();
			if (interfaceInfo == TypeSymbol.s_noInterfaces)
			{
				return TypeSymbol.SymbolAndDiagnostics.Empty;
			}
			ConcurrentDictionary<Symbol, TypeSymbol.SymbolAndDiagnostics> implementationForInterfaceMemberMap = interfaceInfo.ImplementationForInterfaceMemberMap;
			TypeSymbol.SymbolAndDiagnostics symbolAndDiagnostics;
			if (implementationForInterfaceMemberMap.TryGetValue(interfaceMember, ref symbolAndDiagnostics))
			{
				return symbolAndDiagnostics;
			}
			bool flag;
			symbolAndDiagnostics = this.ComputeImplementationAndDiagnosticsForInterfaceMember(interfaceMember, ignoreImplementationInInterfacesIfResultIsNotReady, out flag);
			if (!flag)
			{
				implementationForInterfaceMemberMap.TryAdd(interfaceMember, symbolAndDiagnostics);
			}
			return symbolAndDiagnostics;
		}

		// Token: 0x06007902 RID: 30978 RVA: 0x001BA354 File Offset: 0x001B8554
		internal Symbol FindImplementationForInterfaceMemberInNonInterface(Symbol interfaceMember, bool ignoreImplementationInInterfacesIfResultIsNotReady = false)
		{
			return this.FindImplementationForInterfaceMemberInNonInterfaceWithDiagnostics(interfaceMember, ignoreImplementationInInterfacesIfResultIsNotReady).Symbol;
		}

		// Token: 0x06007903 RID: 30979 RVA: 0x001BA364 File Offset: 0x001B8564
		private TypeSymbol.SymbolAndDiagnostics ComputeImplementationAndDiagnosticsForInterfaceMember(Symbol interfaceMember, bool ignoreImplementationInInterfaces, out bool implementationInInterfacesMightChangeResult)
		{
			BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance(true, this.DeclaringCompilation != null);
			return new TypeSymbol.SymbolAndDiagnostics(TypeSymbol.ComputeImplementationForInterfaceMember(interfaceMember, this, instance, ignoreImplementationInInterfaces, out implementationInInterfacesMightChangeResult), instance.ToReadOnlyAndFree());
		}

		// Token: 0x06007904 RID: 30980 RVA: 0x001BA398 File Offset: 0x001B8598
		private static Symbol ComputeImplementationForInterfaceMember(Symbol interfaceMember, TypeSymbol implementingType, BindingDiagnosticBag diagnostics, bool ignoreImplementationInInterfaces, out bool implementationInInterfacesMightChangeResult)
		{
			NamedTypeSymbol containingType = interfaceMember.ContainingType;
			bool flag = false;
			bool dangerous_IsFromSomeCompilation = implementingType.Dangerous_IsFromSomeCompilation;
			Symbol symbol = null;
			Symbol symbol2 = null;
			bool flag2 = interfaceMember.DeclaredAccessibility == Accessibility.Public && !interfaceMember.IsEventOrPropertyWithImplementableNonPublicAccessor();
			TypeSymbol typeSymbol = null;
			bool flag3 = false;
			CSharpCompilation declaringCompilation = implementingType.DeclaringCompilation;
			CompoundUseSiteInfo<AssemblySymbol> compoundUseSiteInfo = ((declaringCompilation != null) ? new CompoundUseSiteInfo<AssemblySymbol>(diagnostics, declaringCompilation.Assembly) : CompoundUseSiteInfo<AssemblySymbol>.DiscardedDependencies);
			for (TypeSymbol typeSymbol2 = implementingType; typeSymbol2 != null; typeSymbol2 = typeSymbol2.BaseTypeWithDefinitionUseSiteDiagnostics(ref compoundUseSiteInfo))
			{
				MultiDictionary<Symbol, Symbol>.ValueSet explicitImplementationForInterfaceMember = typeSymbol2.GetExplicitImplementationForInterfaceMember(interfaceMember);
				if (explicitImplementationForInterfaceMember.Count == 1)
				{
					implementationInInterfacesMightChangeResult = false;
					return explicitImplementationForInterfaceMember.Single();
				}
				if (explicitImplementationForInterfaceMember.Count > 1)
				{
					if (typeSymbol2 == implementingType || flag3)
					{
						diagnostics.Add(ErrorCode.ERR_DuplicateExplicitImpl, implementingType.Locations[0], new object[] { interfaceMember });
					}
					implementationInInterfacesMightChangeResult = false;
					return null;
				}
				Symbol symbol3;
				if (TypeSymbol.IsExplicitlyImplementedViaAccessors(interfaceMember, typeSymbol2, out symbol3))
				{
					implementationInInterfacesMightChangeResult = false;
					return symbol3;
				}
				if ((!flag || (!flag2 && typeSymbol == null)) && typeSymbol2.InterfacesAndTheirBaseInterfacesWithDefinitionUseSiteDiagnostics(ref compoundUseSiteInfo).ContainsKey(containingType))
				{
					flag = true;
					if (typeSymbol2 == implementingType)
					{
						flag3 = true;
					}
					else if (!flag2 && typeSymbol == null)
					{
						typeSymbol = typeSymbol2;
					}
				}
				if (flag)
				{
					Symbol symbol4;
					Symbol symbol5;
					TypeSymbol.FindPotentialImplicitImplementationMemberDeclaredInType(interfaceMember, dangerous_IsFromSomeCompilation, typeSymbol2, out symbol4, out symbol5);
					if (symbol4 != null)
					{
						symbol = symbol4;
						break;
					}
					if (symbol2 == null)
					{
						symbol2 = symbol5;
					}
				}
			}
			bool flag4 = true;
			if (interfaceMember.IsAccessor())
			{
				bool flag5 = symbol != null;
				TypeSymbol.CheckForImplementationOfCorrespondingPropertyOrEvent((MethodSymbol)interfaceMember, implementingType, dangerous_IsFromSomeCompilation, ref symbol);
				if (flag5 && symbol == null)
				{
					flag4 = false;
				}
			}
			Symbol symbol6 = null;
			if (symbol == null && flag && flag4)
			{
				if (ignoreImplementationInInterfaces)
				{
					implementationInInterfacesMightChangeResult = true;
				}
				else
				{
					symbol6 = TypeSymbol.FindMostSpecificImplementationInInterfaces(interfaceMember, implementingType, ref compoundUseSiteInfo, diagnostics);
					implementationInInterfacesMightChangeResult = false;
				}
			}
			else
			{
				implementationInInterfacesMightChangeResult = false;
			}
			diagnostics.Add((compoundUseSiteInfo.Diagnostics == null || !flag3) ? Location.None : TypeSymbol.GetInterfaceLocation(interfaceMember, implementingType), compoundUseSiteInfo);
			if (symbol6 != null)
			{
				if (flag3)
				{
					TypeSymbol.ReportDefaultInterfaceImplementationMatchDiagnostics(interfaceMember, implementingType, symbol6, diagnostics);
				}
				return symbol6;
			}
			if (flag3)
			{
				if (symbol != null)
				{
					if (!flag2)
					{
						if (interfaceMember.Kind == SymbolKind.Method && typeSymbol == null)
						{
							diagnostics.Add(ErrorCode.ERR_ImplicitImplementationOfNonPublicInterfaceMember, TypeSymbol.GetInterfaceLocation(interfaceMember, implementingType), new object[] { implementingType, interfaceMember, symbol });
						}
					}
					else
					{
						TypeSymbol.ReportImplicitImplementationMatchDiagnostics(interfaceMember, implementingType, symbol, diagnostics);
					}
				}
				else if (symbol2 != null)
				{
					TypeSymbol.ReportImplicitImplementationMismatchDiagnostics(interfaceMember, implementingType, symbol2, diagnostics);
				}
			}
			return symbol;
		}

		// Token: 0x06007905 RID: 30981 RVA: 0x001BA5C0 File Offset: 0x001B87C0
		private static Symbol FindMostSpecificImplementationInInterfaces(Symbol interfaceMember, TypeSymbol implementingType, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, BindingDiagnosticBag diagnostics)
		{
			ValueTuple<MethodSymbol, MethodSymbol> implementableAccessors = TypeSymbol.GetImplementableAccessors(interfaceMember);
			MethodSymbol item = implementableAccessors.Item1;
			MethodSymbol item2 = implementableAccessors.Item2;
			if (TypeSymbol.<FindMostSpecificImplementationInInterfaces>g__stopLookup|89_0(item, implementingType) || TypeSymbol.<FindMostSpecificImplementationInInterfaces>g__stopLookup|89_0(item2, implementingType))
			{
				return null;
			}
			Symbol symbol2;
			Symbol symbol3;
			Symbol symbol = TypeSymbol.FindMostSpecificImplementationInBases(interfaceMember, implementingType, ref useSiteInfo, out symbol2, out symbol3);
			if (symbol2 != null)
			{
				diagnostics.Add(ErrorCode.ERR_MostSpecificImplementationIsNotFound, TypeSymbol.GetInterfaceLocation(interfaceMember, implementingType), new object[] { interfaceMember, symbol2, symbol3 });
			}
			return symbol;
		}

		// Token: 0x06007906 RID: 30982 RVA: 0x001BA62C File Offset: 0x001B882C
		private static Symbol FindMostSpecificImplementation(Symbol interfaceMember, NamedTypeSymbol implementingInterface, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			MultiDictionary<Symbol, Symbol>.ValueSet valueSet = TypeSymbol.FindImplementationInInterface(interfaceMember, implementingInterface);
			int count = valueSet.Count;
			if (count != 0)
			{
				if (count != 1)
				{
					return null;
				}
				Symbol symbol = valueSet.Single();
				if (symbol.IsAbstract)
				{
					return null;
				}
				return symbol;
			}
			else
			{
				ValueTuple<MethodSymbol, MethodSymbol> implementableAccessors = TypeSymbol.GetImplementableAccessors(interfaceMember);
				MethodSymbol item = implementableAccessors.Item1;
				MethodSymbol item2 = implementableAccessors.Item2;
				if ((item != null && TypeSymbol.FindImplementationInInterface(item, implementingInterface).Count != 0) || (item2 != null && TypeSymbol.FindImplementationInInterface(item2, implementingInterface).Count != 0))
				{
					return null;
				}
				Symbol symbol2;
				Symbol symbol3;
				return TypeSymbol.FindMostSpecificImplementationInBases(interfaceMember, implementingInterface, ref useSiteInfo, out symbol2, out symbol3);
			}
		}

		// Token: 0x06007907 RID: 30983 RVA: 0x001BA6B8 File Offset: 0x001B88B8
		private static Symbol FindMostSpecificImplementationInBases(Symbol interfaceMember, TypeSymbol implementingType, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, out Symbol conflictingImplementation1, out Symbol conflictingImplementation2)
		{
			ImmutableArray<NamedTypeSymbol> immutableArray = implementingType.AllInterfacesWithDefinitionUseSiteDiagnostics(ref useSiteInfo);
			if (immutableArray.IsEmpty)
			{
				conflictingImplementation1 = null;
				conflictingImplementation2 = null;
				return null;
			}
			ValueTuple<MethodSymbol, MethodSymbol> implementableAccessors = TypeSymbol.GetImplementableAccessors(interfaceMember);
			MethodSymbol item = implementableAccessors.Item1;
			MethodSymbol item2 = implementableAccessors.Item2;
			if (item == null && item2 == null)
			{
				return TypeSymbol.<FindMostSpecificImplementationInBases>g__findMostSpecificImplementationInBases|91_1(interfaceMember, immutableArray, ref useSiteInfo, out conflictingImplementation1, out conflictingImplementation2);
			}
			Symbol symbol2;
			Symbol symbol3;
			Symbol symbol = TypeSymbol.<FindMostSpecificImplementationInBases>g__findMostSpecificImplementationInBases|91_1(item ?? item2, immutableArray, ref useSiteInfo, out symbol2, out symbol3);
			if (symbol == null && symbol2 == null)
			{
				conflictingImplementation1 = null;
				conflictingImplementation2 = null;
				return null;
			}
			if (item == null || item2 == null)
			{
				if (symbol != null)
				{
					conflictingImplementation1 = null;
					conflictingImplementation2 = null;
					return TypeSymbol.<FindMostSpecificImplementationInBases>g__findImplementationInInterface|91_0(interfaceMember, symbol, null);
				}
				conflictingImplementation1 = TypeSymbol.<FindMostSpecificImplementationInBases>g__findImplementationInInterface|91_0(interfaceMember, symbol2, null);
				conflictingImplementation2 = TypeSymbol.<FindMostSpecificImplementationInBases>g__findImplementationInInterface|91_0(interfaceMember, symbol3, null);
				if (conflictingImplementation1 == null != (conflictingImplementation2 == null))
				{
					conflictingImplementation1 = null;
					conflictingImplementation2 = null;
				}
				return null;
			}
			else
			{
				Symbol symbol5;
				Symbol symbol6;
				Symbol symbol4 = TypeSymbol.<FindMostSpecificImplementationInBases>g__findMostSpecificImplementationInBases|91_1(item2, immutableArray, ref useSiteInfo, out symbol5, out symbol6);
				if ((symbol4 == null && symbol5 == null) || symbol == null != (symbol4 == null))
				{
					conflictingImplementation1 = null;
					conflictingImplementation2 = null;
					return null;
				}
				if (symbol != null)
				{
					conflictingImplementation1 = null;
					conflictingImplementation2 = null;
					return TypeSymbol.<FindMostSpecificImplementationInBases>g__findImplementationInInterface|91_0(interfaceMember, symbol, symbol4);
				}
				conflictingImplementation1 = TypeSymbol.<FindMostSpecificImplementationInBases>g__findImplementationInInterface|91_0(interfaceMember, symbol2, symbol5);
				conflictingImplementation2 = TypeSymbol.<FindMostSpecificImplementationInBases>g__findImplementationInInterface|91_0(interfaceMember, symbol3, symbol6);
				if (conflictingImplementation1 == null != (conflictingImplementation2 == null))
				{
					conflictingImplementation1 = null;
					conflictingImplementation2 = null;
				}
				return null;
			}
		}

		// Token: 0x06007908 RID: 30984 RVA: 0x001BA7D8 File Offset: 0x001B89D8
		internal static MultiDictionary<Symbol, Symbol>.ValueSet FindImplementationInInterface(Symbol interfaceMember, NamedTypeSymbol interfaceType)
		{
			NamedTypeSymbol containingType = interfaceMember.ContainingType;
			if (!containingType.Equals(interfaceType, TypeCompareKind.CLRSignatureCompareOptions))
			{
				return interfaceType.GetExplicitImplementationForInterfaceMember(interfaceMember);
			}
			if (!interfaceMember.IsAbstract)
			{
				if (!containingType.Equals(interfaceType, TypeCompareKind.ConsiderEverything))
				{
					interfaceMember = interfaceMember.OriginalDefinition.SymbolAsMember(interfaceType);
				}
				return new MultiDictionary<Symbol, Symbol>.ValueSet(interfaceMember, null);
			}
			return default(MultiDictionary<Symbol, Symbol>.ValueSet);
		}

		// Token: 0x06007909 RID: 30985 RVA: 0x001BA830 File Offset: 0x001B8A30
		[return: TupleElementNames(new string[] { "interfaceAccessor1", "interfaceAccessor2" })]
		private static ValueTuple<MethodSymbol, MethodSymbol> GetImplementableAccessors(Symbol interfaceMember)
		{
			SymbolKind kind = interfaceMember.Kind;
			MethodSymbol methodSymbol;
			MethodSymbol methodSymbol2;
			if (kind != SymbolKind.Event)
			{
				if (kind == SymbolKind.Property)
				{
					PropertySymbol propertySymbol = (PropertySymbol)interfaceMember;
					methodSymbol = propertySymbol.GetMethod;
					methodSymbol2 = propertySymbol.SetMethod;
				}
				else
				{
					methodSymbol = null;
					methodSymbol2 = null;
				}
			}
			else
			{
				EventSymbol eventSymbol = (EventSymbol)interfaceMember;
				methodSymbol = eventSymbol.AddMethod;
				methodSymbol2 = eventSymbol.RemoveMethod;
			}
			if (!methodSymbol.IsImplementable())
			{
				methodSymbol = null;
			}
			if (!methodSymbol2.IsImplementable())
			{
				methodSymbol2 = null;
			}
			return new ValueTuple<MethodSymbol, MethodSymbol>(methodSymbol, methodSymbol2);
		}

		// Token: 0x0600790A RID: 30986 RVA: 0x001BA898 File Offset: 0x001B8A98
		private static bool IsExplicitlyImplementedViaAccessors(Symbol interfaceMember, TypeSymbol currType, out Symbol implementingMember)
		{
			ValueTuple<MethodSymbol, MethodSymbol> implementableAccessors = TypeSymbol.GetImplementableAccessors(interfaceMember);
			MethodSymbol item = implementableAccessors.Item1;
			MethodSymbol item2 = implementableAccessors.Item2;
			Symbol symbol;
			Symbol symbol2;
			if (TypeSymbol.TryGetExplicitImplementationAssociatedPropertyOrEvent(item, currType, out symbol) | TypeSymbol.TryGetExplicitImplementationAssociatedPropertyOrEvent(item2, currType, out symbol2))
			{
				if (symbol == null || symbol2 == null || symbol == symbol2)
				{
					implementingMember = symbol ?? symbol2;
					if (implementingMember != null && implementingMember.Dangerous_IsFromSomeCompilation)
					{
						implementingMember = null;
					}
				}
				else
				{
					implementingMember = null;
				}
				return true;
			}
			implementingMember = null;
			return false;
		}

		// Token: 0x0600790B RID: 30987 RVA: 0x001BA900 File Offset: 0x001B8B00
		private static bool TryGetExplicitImplementationAssociatedPropertyOrEvent(MethodSymbol interfaceAccessor, TypeSymbol currType, out Symbol associated)
		{
			if (interfaceAccessor != null)
			{
				MultiDictionary<Symbol, Symbol>.ValueSet explicitImplementationForInterfaceMember = currType.GetExplicitImplementationForInterfaceMember(interfaceAccessor);
				if (explicitImplementationForInterfaceMember.Count == 1)
				{
					Symbol symbol = explicitImplementationForInterfaceMember.Single();
					associated = ((symbol.Kind == SymbolKind.Method) ? ((MethodSymbol)symbol).AssociatedSymbol : null);
					return true;
				}
			}
			associated = null;
			return false;
		}

		// Token: 0x0600790C RID: 30988 RVA: 0x001BA94C File Offset: 0x001B8B4C
		private static void CheckForImplementationOfCorrespondingPropertyOrEvent(MethodSymbol interfaceMethod, TypeSymbol implementingType, bool implementingTypeIsFromSomeCompilation, ref Symbol implicitImpl)
		{
			Symbol associatedSymbol = interfaceMethod.AssociatedSymbol;
			Symbol symbol = implementingType.FindImplementationForInterfaceMemberInNonInterface(associatedSymbol, true);
			MethodSymbol methodSymbol = null;
			if (symbol != null && !symbol.ContainingType.IsInterface)
			{
				MethodKind methodKind = interfaceMethod.MethodKind;
				if (methodKind <= MethodKind.EventRemove)
				{
					if (methodKind == MethodKind.EventAdd)
					{
						methodSymbol = ((EventSymbol)symbol).GetOwnOrInheritedAddMethod();
						goto IL_008C;
					}
					if (methodKind == MethodKind.EventRemove)
					{
						methodSymbol = ((EventSymbol)symbol).GetOwnOrInheritedRemoveMethod();
						goto IL_008C;
					}
				}
				else
				{
					if (methodKind == MethodKind.PropertyGet)
					{
						methodSymbol = ((PropertySymbol)symbol).GetOwnOrInheritedGetMethod();
						goto IL_008C;
					}
					if (methodKind == MethodKind.PropertySet)
					{
						methodSymbol = ((PropertySymbol)symbol).GetOwnOrInheritedSetMethod();
						goto IL_008C;
					}
				}
				throw ExceptionUtilities.UnexpectedValue(interfaceMethod.MethodKind);
			}
			IL_008C:
			if (methodSymbol == implicitImpl)
			{
				return;
			}
			if (methodSymbol == null && implicitImpl != null && implicitImpl.IsAccessor())
			{
				implicitImpl = null;
				return;
			}
			if (methodSymbol != null && (implicitImpl == null || TypeSymbol.Equals(methodSymbol.ContainingType, implicitImpl.ContainingType, TypeCompareKind.ConsiderEverything)))
			{
				MethodSymbol methodSymbol2 = new SignatureOnlyMethodSymbol(methodSymbol.Name, interfaceMethod.ContainingType, interfaceMethod.MethodKind, interfaceMethod.CallingConvention, interfaceMethod.TypeParameters, interfaceMethod.Parameters, interfaceMethod.RefKind, interfaceMethod.IsInitOnly, interfaceMethod.ReturnTypeWithAnnotations, interfaceMethod.RefCustomModifiers, interfaceMethod.ExplicitInterfaceImplementations);
				if (TypeSymbol.IsInterfaceMemberImplementation(methodSymbol, methodSymbol2, implementingTypeIsFromSomeCompilation))
				{
					implicitImpl = methodSymbol;
				}
			}
		}

		// Token: 0x0600790D RID: 30989 RVA: 0x001BAA78 File Offset: 0x001B8C78
		private static void ReportDefaultInterfaceImplementationMatchDiagnostics(Symbol interfaceMember, TypeSymbol implementingType, Symbol implicitImpl, BindingDiagnosticBag diagnostics)
		{
			if (interfaceMember.Kind == SymbolKind.Method && implementingType.ContainingModule != implicitImpl.ContainingModule)
			{
				LanguageVersion languageVersion = MessageID.IDS_DefaultInterfaceImplementation.RequiredVersion();
				CSharpCompilation declaringCompilation = implementingType.DeclaringCompilation;
				LanguageVersion? languageVersion2 = ((declaringCompilation != null) ? new LanguageVersion?(declaringCompilation.LanguageVersion) : default(LanguageVersion?));
				LanguageVersion languageVersion3 = languageVersion;
				LanguageVersion? languageVersion4 = languageVersion2;
				if ((languageVersion3 > languageVersion4.GetValueOrDefault()) & (languageVersion4 != null))
				{
					diagnostics.Add(ErrorCode.ERR_LanguageVersionDoesNotSupportDefaultInterfaceImplementationForMember, TypeSymbol.GetInterfaceLocation(interfaceMember, implementingType), new object[]
					{
						implicitImpl,
						interfaceMember,
						implementingType,
						MessageID.IDS_DefaultInterfaceImplementation.Localize(),
						languageVersion2.GetValueOrDefault().ToDisplayString(),
						new CSharpRequiredLanguageVersion(languageVersion)
					});
				}
				if (!implementingType.ContainingAssembly.RuntimeSupportsDefaultInterfaceImplementation)
				{
					diagnostics.Add(ErrorCode.ERR_RuntimeDoesNotSupportDefaultInterfaceImplementationForMember, TypeSymbol.GetInterfaceLocation(interfaceMember, implementingType), new object[] { implicitImpl, interfaceMember, implementingType });
				}
			}
		}

		// Token: 0x0600790E RID: 30990 RVA: 0x001BAB6C File Offset: 0x001B8D6C
		private static void ReportImplicitImplementationMatchDiagnostics(Symbol interfaceMember, TypeSymbol implementingType, Symbol implicitImpl, BindingDiagnosticBag diagnostics)
		{
			bool flag = false;
			if (interfaceMember.Kind == SymbolKind.Method)
			{
				MethodSymbol methodSymbol = (MethodSymbol)interfaceMember;
				bool flag2 = implicitImpl.IsAccessor();
				bool flag3 = methodSymbol.IsAccessor();
				if (flag3 && !flag2 && !methodSymbol.IsIndexedPropertyAccessor())
				{
					diagnostics.Add(ErrorCode.ERR_MethodImplementingAccessor, TypeSymbol.GetImplicitImplementationDiagnosticLocation(interfaceMember, implementingType, implicitImpl), new object[] { implicitImpl, methodSymbol, implementingType });
				}
				else if (!flag3 && flag2)
				{
					diagnostics.Add(ErrorCode.ERR_AccessorImplementingMethod, TypeSymbol.GetImplicitImplementationDiagnosticLocation(interfaceMember, implementingType, implicitImpl), new object[] { implicitImpl, methodSymbol, implementingType });
				}
				else
				{
					MethodSymbol methodSymbol2 = (MethodSymbol)implicitImpl;
					if (methodSymbol2.IsConditional)
					{
						diagnostics.Add(ErrorCode.ERR_InterfaceImplementedByConditional, TypeSymbol.GetImplicitImplementationDiagnosticLocation(interfaceMember, implementingType, implicitImpl), new object[] { implicitImpl, methodSymbol, implementingType });
					}
					else if (TypeSymbol.ReportAnyMismatchedConstraints(methodSymbol, implementingType, methodSymbol2, diagnostics))
					{
						flag = true;
					}
				}
			}
			if (implicitImpl.ContainsTupleNames() && MemberSignatureComparer.ConsideringTupleNamesCreatesDifference(implicitImpl, interfaceMember))
			{
				diagnostics.Add(ErrorCode.ERR_ImplBadTupleNames, TypeSymbol.GetImplicitImplementationDiagnosticLocation(interfaceMember, implementingType, implicitImpl), new object[] { implicitImpl, interfaceMember });
				flag = true;
			}
			if (!flag && implementingType.DeclaringCompilation != null)
			{
				TypeSymbol.CheckNullableReferenceTypeMismatchOnImplementingMember(implementingType, implicitImpl, interfaceMember, false, diagnostics);
			}
			if (!implicitImpl.ContainingType.IsDefinition)
			{
				foreach (Symbol symbol in implicitImpl.ContainingType.GetMembers(implicitImpl.Name))
				{
					if (symbol.DeclaredAccessibility == Accessibility.Public && !symbol.IsStatic && !(symbol == implicitImpl) && MemberSignatureComparer.RuntimeImplicitImplementationComparer.Equals(interfaceMember, symbol) && !symbol.IsAccessor())
					{
						diagnostics.Add(ErrorCode.WRN_MultipleRuntimeImplementationMatches, TypeSymbol.GetImplicitImplementationDiagnosticLocation(interfaceMember, implementingType, symbol), new object[] { symbol, interfaceMember, implementingType });
					}
				}
			}
		}

		// Token: 0x0600790F RID: 30991 RVA: 0x001BAD34 File Offset: 0x001B8F34
		internal static void CheckNullableReferenceTypeMismatchOnImplementingMember(TypeSymbol implementingType, Symbol implementingMember, Symbol interfaceMember, bool isExplicit, BindingDiagnosticBag diagnostics)
		{
			if (!implementingMember.IsImplicitlyDeclared && !implementingMember.IsAccessor())
			{
				CSharpCompilation declaringCompilation = implementingType.DeclaringCompilation;
				if (interfaceMember.Kind == SymbolKind.Event)
				{
					EventSymbol eventSymbol = (EventSymbol)implementingMember;
					EventSymbol eventSymbol2 = (EventSymbol)interfaceMember;
					SourceMemberContainerTypeSymbol.CheckValidNullableEventOverride<ValueTuple<TypeSymbol, bool>>(declaringCompilation, eventSymbol2, eventSymbol, diagnostics, delegate(BindingDiagnosticBag diagnostics, EventSymbol implementedEvent, EventSymbol implementingEvent, [TupleElementNames(new string[] { "implementingType", "isExplicit" })] ValueTuple<TypeSymbol, bool> arg)
					{
						if (arg.Item2)
						{
							diagnostics.Add(ErrorCode.WRN_NullabilityMismatchInTypeOnExplicitImplementation, implementingEvent.Locations[0], new object[]
							{
								new FormattedSymbol(implementedEvent, SymbolDisplayFormat.MinimallyQualifiedFormat)
							});
							return;
						}
						diagnostics.Add(ErrorCode.WRN_NullabilityMismatchInTypeOnImplicitImplementation, TypeSymbol.GetImplicitImplementationDiagnosticLocation(implementedEvent, arg.Item1, implementingEvent), new object[]
						{
							new FormattedSymbol(implementingEvent, SymbolDisplayFormat.MinimallyQualifiedFormat),
							new FormattedSymbol(implementedEvent, SymbolDisplayFormat.MinimallyQualifiedFormat)
						});
					}, new ValueTuple<TypeSymbol, bool>(implementingType, isExplicit));
					return;
				}
				ReportMismatchInReturnType<ValueTuple<TypeSymbol, bool>> reportMismatchInReturnType = delegate(BindingDiagnosticBag diagnostics, MethodSymbol implementedMethod, MethodSymbol implementingMethod, bool topLevel, [TupleElementNames(new string[] { "implementingType", "isExplicit" })] ValueTuple<TypeSymbol, bool> arg)
				{
					if (arg.Item2)
					{
						diagnostics.Add(topLevel ? ErrorCode.WRN_TopLevelNullabilityMismatchInReturnTypeOnExplicitImplementation : ErrorCode.WRN_NullabilityMismatchInReturnTypeOnExplicitImplementation, implementingMethod.Locations[0], new object[]
						{
							new FormattedSymbol(implementedMethod.ConstructedFrom, SymbolDisplayFormat.MinimallyQualifiedFormat)
						});
						return;
					}
					diagnostics.Add(topLevel ? ErrorCode.WRN_TopLevelNullabilityMismatchInReturnTypeOnImplicitImplementation : ErrorCode.WRN_NullabilityMismatchInReturnTypeOnImplicitImplementation, TypeSymbol.GetImplicitImplementationDiagnosticLocation(implementedMethod, arg.Item1, implementingMethod), new object[]
					{
						new FormattedSymbol(implementingMethod, SymbolDisplayFormat.MinimallyQualifiedFormat),
						new FormattedSymbol(implementedMethod.ConstructedFrom, SymbolDisplayFormat.MinimallyQualifiedFormat)
					});
				};
				ReportMismatchInParameterType<ValueTuple<TypeSymbol, bool>> reportMismatchInParameterType = delegate(BindingDiagnosticBag diagnostics, MethodSymbol implementedMethod, MethodSymbol implementingMethod, ParameterSymbol implementingParameter, bool topLevel, [TupleElementNames(new string[] { "implementingType", "isExplicit" })] ValueTuple<TypeSymbol, bool> arg)
				{
					if (arg.Item2)
					{
						diagnostics.Add(topLevel ? ErrorCode.WRN_TopLevelNullabilityMismatchInParameterTypeOnExplicitImplementation : ErrorCode.WRN_NullabilityMismatchInParameterTypeOnExplicitImplementation, implementingMethod.Locations[0], new object[]
						{
							new FormattedSymbol(implementingParameter, SymbolDisplayFormat.ShortFormat),
							new FormattedSymbol(implementedMethod.ConstructedFrom, SymbolDisplayFormat.MinimallyQualifiedFormat)
						});
						return;
					}
					diagnostics.Add(topLevel ? ErrorCode.WRN_TopLevelNullabilityMismatchInParameterTypeOnImplicitImplementation : ErrorCode.WRN_NullabilityMismatchInParameterTypeOnImplicitImplementation, TypeSymbol.GetImplicitImplementationDiagnosticLocation(implementedMethod, arg.Item1, implementingMethod), new object[]
					{
						new FormattedSymbol(implementingParameter, SymbolDisplayFormat.ShortFormat),
						new FormattedSymbol(implementingMethod, SymbolDisplayFormat.MinimallyQualifiedFormat),
						new FormattedSymbol(implementedMethod.ConstructedFrom, SymbolDisplayFormat.MinimallyQualifiedFormat)
					});
				};
				SymbolKind kind = interfaceMember.Kind;
				if (kind == SymbolKind.Method)
				{
					MethodSymbol methodSymbol = (MethodSymbol)implementingMember;
					MethodSymbol methodSymbol2 = (MethodSymbol)interfaceMember;
					if (methodSymbol2.IsGenericMethod)
					{
						methodSymbol2 = methodSymbol2.Construct(TypeMap.TypeParametersAsTypeSymbolsWithIgnoredAnnotations(methodSymbol.TypeParameters));
					}
					SourceMemberContainerTypeSymbol.CheckValidNullableMethodOverride<ValueTuple<TypeSymbol, bool>>(declaringCompilation, methodSymbol2, methodSymbol, diagnostics, reportMismatchInReturnType, reportMismatchInParameterType, new ValueTuple<TypeSymbol, bool>(implementingType, isExplicit), false);
					return;
				}
				if (kind != SymbolKind.Property)
				{
					throw ExceptionUtilities.UnexpectedValue(interfaceMember.Kind);
				}
				PropertySymbol propertySymbol = (PropertySymbol)implementingMember;
				PropertySymbol propertySymbol2 = (PropertySymbol)interfaceMember;
				if (propertySymbol2.GetMethod.IsImplementable())
				{
					MethodSymbol ownOrInheritedGetMethod = propertySymbol.GetOwnOrInheritedGetMethod();
					CSharpCompilation csharpCompilation = declaringCompilation;
					MethodSymbol getMethod = propertySymbol2.GetMethod;
					MethodSymbol methodSymbol3 = ownOrInheritedGetMethod;
					ReportMismatchInReturnType<ValueTuple<TypeSymbol, bool>> reportMismatchInReturnType2 = reportMismatchInReturnType;
					ReportMismatchInParameterType<ValueTuple<TypeSymbol, bool>> reportMismatchInParameterType2;
					if (propertySymbol2.SetMethod.IsImplementable() && !(((ownOrInheritedGetMethod != null) ? ownOrInheritedGetMethod.AssociatedSymbol : null) != propertySymbol))
					{
						MethodSymbol ownOrInheritedSetMethod = propertySymbol.GetOwnOrInheritedSetMethod();
						if (!(((ownOrInheritedSetMethod != null) ? ownOrInheritedSetMethod.AssociatedSymbol : null) != propertySymbol))
						{
							reportMismatchInParameterType2 = null;
							goto IL_013A;
						}
					}
					reportMismatchInParameterType2 = reportMismatchInParameterType;
					IL_013A:
					SourceMemberContainerTypeSymbol.CheckValidNullableMethodOverride<ValueTuple<TypeSymbol, bool>>(csharpCompilation, getMethod, methodSymbol3, diagnostics, reportMismatchInReturnType2, reportMismatchInParameterType2, new ValueTuple<TypeSymbol, bool>(implementingType, isExplicit), false);
				}
				if (propertySymbol2.SetMethod.IsImplementable())
				{
					SourceMemberContainerTypeSymbol.CheckValidNullableMethodOverride<ValueTuple<TypeSymbol, bool>>(declaringCompilation, propertySymbol2.SetMethod, propertySymbol.GetOwnOrInheritedSetMethod(), diagnostics, null, reportMismatchInParameterType, new ValueTuple<TypeSymbol, bool>(implementingType, isExplicit), false);
					return;
				}
			}
		}

		// Token: 0x06007910 RID: 30992 RVA: 0x001BAF10 File Offset: 0x001B9110
		private static void ReportImplicitImplementationMismatchDiagnostics(Symbol interfaceMember, TypeSymbol implementingType, Symbol closestMismatch, BindingDiagnosticBag diagnostics)
		{
			Location interfaceLocation = TypeSymbol.GetInterfaceLocation(interfaceMember, implementingType);
			if (closestMismatch.IsStatic)
			{
				diagnostics.Add(ErrorCode.ERR_CloseUnimplementedInterfaceMemberStatic, interfaceLocation, new object[] { implementingType, interfaceMember, closestMismatch });
				return;
			}
			if (closestMismatch.DeclaredAccessibility != Accessibility.Public)
			{
				ErrorCode errorCode = (interfaceMember.IsAccessor() ? ErrorCode.ERR_UnimplementedInterfaceAccessor : ErrorCode.ERR_CloseUnimplementedInterfaceMemberNotPublic);
				diagnostics.Add(errorCode, interfaceLocation, new object[] { implementingType, interfaceMember, closestMismatch });
				return;
			}
			if (TypeSymbol.HaveInitOnlyMismatch(interfaceMember, closestMismatch))
			{
				diagnostics.Add(ErrorCode.ERR_CloseUnimplementedInterfaceMemberWrongInitOnly, interfaceLocation, new object[] { implementingType, interfaceMember, closestMismatch });
				return;
			}
			RefKind refKind = RefKind.None;
			SymbolKind symbolKind = interfaceMember.Kind;
			TypeSymbol typeSymbol;
			if (symbolKind != SymbolKind.Event)
			{
				if (symbolKind != SymbolKind.Method)
				{
					if (symbolKind != SymbolKind.Property)
					{
						throw ExceptionUtilities.UnexpectedValue(interfaceMember.Kind);
					}
					PropertySymbol propertySymbol = (PropertySymbol)interfaceMember;
					refKind = propertySymbol.RefKind;
					typeSymbol = propertySymbol.Type;
				}
				else
				{
					MethodSymbol methodSymbol = (MethodSymbol)interfaceMember;
					refKind = methodSymbol.RefKind;
					typeSymbol = methodSymbol.ReturnType;
				}
			}
			else
			{
				typeSymbol = ((EventSymbol)interfaceMember).Type;
			}
			bool flag = false;
			symbolKind = closestMismatch.Kind;
			if (symbolKind != SymbolKind.Method)
			{
				if (symbolKind == SymbolKind.Property)
				{
					flag = ((PropertySymbol)closestMismatch).RefKind != refKind;
				}
			}
			else
			{
				flag = ((MethodSymbol)closestMismatch).RefKind != refKind;
			}
			DiagnosticInfo diagnosticInfo;
			if (typeSymbol != null && (diagnosticInfo = typeSymbol.GetUseSiteInfo().DiagnosticInfo) != null && diagnosticInfo.DefaultSeverity == DiagnosticSeverity.Error)
			{
				diagnostics.Add(diagnosticInfo, interfaceLocation);
				return;
			}
			if (flag)
			{
				diagnostics.Add(ErrorCode.ERR_CloseUnimplementedInterfaceMemberWrongRefReturn, interfaceLocation, new object[] { implementingType, interfaceMember, closestMismatch });
				return;
			}
			diagnostics.Add(ErrorCode.ERR_CloseUnimplementedInterfaceMemberWrongReturnType, interfaceLocation, new object[] { implementingType, interfaceMember, closestMismatch, typeSymbol });
		}

		// Token: 0x06007911 RID: 30993 RVA: 0x001BB0C8 File Offset: 0x001B92C8
		internal static bool HaveInitOnlyMismatch(Symbol one, Symbol other)
		{
			MethodSymbol methodSymbol = one as MethodSymbol;
			if (methodSymbol == null)
			{
				return false;
			}
			MethodSymbol methodSymbol2 = other as MethodSymbol;
			return methodSymbol2 != null && methodSymbol.IsInitOnly != methodSymbol2.IsInitOnly;
		}

		// Token: 0x06007912 RID: 30994 RVA: 0x001BB100 File Offset: 0x001B9300
		private static Location GetInterfaceLocation(Symbol interfaceMember, TypeSymbol implementingType)
		{
			NamedTypeSymbol containingType = interfaceMember.ContainingType;
			SourceMemberContainerTypeSymbol sourceMemberContainerTypeSymbol = null;
			if (implementingType.InterfacesAndTheirBaseInterfacesNoUseSiteDiagnostics[containingType].Contains(containingType))
			{
				sourceMemberContainerTypeSymbol = implementingType as SourceMemberContainerTypeSymbol;
			}
			return ((sourceMemberContainerTypeSymbol != null) ? sourceMemberContainerTypeSymbol.GetImplementsLocation(containingType) : null) ?? implementingType.Locations.FirstOrNone();
		}

		// Token: 0x06007913 RID: 30995 RVA: 0x001BB150 File Offset: 0x001B9350
		private static bool ReportAnyMismatchedConstraints(MethodSymbol interfaceMethod, TypeSymbol implementingType, MethodSymbol implicitImpl, BindingDiagnosticBag diagnostics)
		{
			bool flag = false;
			int arity = interfaceMethod.Arity;
			if (arity > 0)
			{
				ImmutableArray<TypeParameterSymbol> typeParameters = interfaceMethod.TypeParameters;
				ImmutableArray<TypeParameterSymbol> typeParameters2 = implicitImpl.TypeParameters;
				ImmutableArray<TypeWithAnnotations> immutableArray = IndexedTypeParameterSymbol.Take(arity);
				TypeMap typeMap = new TypeMap(typeParameters, immutableArray, true);
				TypeMap typeMap2 = new TypeMap(typeParameters2, immutableArray, true);
				for (int i = 0; i < arity; i++)
				{
					TypeParameterSymbol typeParameterSymbol = typeParameters[i];
					TypeParameterSymbol typeParameterSymbol2 = typeParameters2[i];
					if (!MemberSignatureComparer.HaveSameConstraints(typeParameterSymbol, typeMap, typeParameterSymbol2, typeMap2))
					{
						diagnostics.Add(ErrorCode.ERR_ImplBadConstraints, TypeSymbol.GetImplicitImplementationDiagnosticLocation(interfaceMethod, implementingType, implicitImpl), new object[] { typeParameterSymbol2.Name, implicitImpl, typeParameterSymbol.Name, interfaceMethod });
					}
					else if (!MemberSignatureComparer.HaveSameNullabilityInConstraints(typeParameterSymbol, typeMap, typeParameterSymbol2, typeMap2))
					{
						diagnostics.Add(ErrorCode.WRN_NullabilityMismatchInConstraintsOnImplicitImplementation, TypeSymbol.GetImplicitImplementationDiagnosticLocation(interfaceMethod, implementingType, implicitImpl), new object[] { typeParameterSymbol2.Name, implicitImpl, typeParameterSymbol.Name, interfaceMethod });
					}
				}
			}
			return flag;
		}

		// Token: 0x06007914 RID: 30996 RVA: 0x001BB254 File Offset: 0x001B9454
		internal static Location GetImplicitImplementationDiagnosticLocation(Symbol interfaceMember, TypeSymbol implementingType, Symbol member)
		{
			if (TypeSymbol.Equals(member.ContainingType, implementingType, TypeCompareKind.ConsiderEverything))
			{
				return member.Locations[0];
			}
			NamedTypeSymbol containingType = interfaceMember.ContainingType;
			SourceMemberContainerTypeSymbol sourceMemberContainerTypeSymbol = implementingType as SourceMemberContainerTypeSymbol;
			return ((sourceMemberContainerTypeSymbol != null) ? sourceMemberContainerTypeSymbol.GetImplementsLocation(containingType) : null) ?? implementingType.Locations[0];
		}

		// Token: 0x06007915 RID: 30997 RVA: 0x001BB2B0 File Offset: 0x001B94B0
		private static void FindPotentialImplicitImplementationMemberDeclaredInType(Symbol interfaceMember, bool implementingTypeIsFromSomeCompilation, TypeSymbol currType, out Symbol implicitImpl, out Symbol closeMismatch)
		{
			implicitImpl = null;
			closeMismatch = null;
			foreach (Symbol symbol in currType.GetMembers(interfaceMember.Name))
			{
				if (symbol.Kind == interfaceMember.Kind)
				{
					if (TypeSymbol.IsInterfaceMemberImplementation(symbol, interfaceMember, implementingTypeIsFromSomeCompilation))
					{
						implicitImpl = symbol;
						return;
					}
					if (closeMismatch == null && implementingTypeIsFromSomeCompilation && interfaceMember.DeclaredAccessibility == Accessibility.Public && !interfaceMember.IsEventOrPropertyWithImplementableNonPublicAccessor() && MemberSignatureComparer.CSharpCloseImplicitImplementationComparer.Equals(interfaceMember, symbol))
					{
						closeMismatch = symbol;
					}
				}
			}
		}

		// Token: 0x06007916 RID: 30998 RVA: 0x001BB335 File Offset: 0x001B9535
		private static bool IsInterfaceMemberImplementation(Symbol candidateMember, Symbol interfaceMember, bool implementingTypeIsFromSomeCompilation)
		{
			if (candidateMember.DeclaredAccessibility != Accessibility.Public || candidateMember.IsStatic)
			{
				return false;
			}
			if (TypeSymbol.HaveInitOnlyMismatch(candidateMember, interfaceMember))
			{
				return false;
			}
			if (implementingTypeIsFromSomeCompilation)
			{
				return MemberSignatureComparer.CSharpImplicitImplementationComparer.Equals(interfaceMember, candidateMember);
			}
			return MemberSignatureComparer.RuntimeImplicitImplementationComparer.Equals(interfaceMember, candidateMember);
		}

		// Token: 0x06007917 RID: 30999 RVA: 0x001BB374 File Offset: 0x001B9574
		protected MultiDictionary<Symbol, Symbol>.ValueSet GetExplicitImplementationForInterfaceMember(Symbol interfaceMember)
		{
			TypeSymbol.InterfaceInfo interfaceInfo = this.GetInterfaceInfo();
			if (interfaceInfo == TypeSymbol.s_noInterfaces)
			{
				return default(MultiDictionary<Symbol, Symbol>.ValueSet);
			}
			if (interfaceInfo.explicitInterfaceImplementationMap == null)
			{
				Interlocked.CompareExchange<MultiDictionary<Symbol, Symbol>>(ref interfaceInfo.explicitInterfaceImplementationMap, this.MakeExplicitInterfaceImplementationMap(), null);
			}
			return interfaceInfo.explicitInterfaceImplementationMap[interfaceMember];
		}

		// Token: 0x06007918 RID: 31000 RVA: 0x001BB3C4 File Offset: 0x001B95C4
		private MultiDictionary<Symbol, Symbol> MakeExplicitInterfaceImplementationMap()
		{
			MultiDictionary<Symbol, Symbol> multiDictionary = new MultiDictionary<Symbol, Symbol>(TypeSymbol.ExplicitInterfaceImplementationTargetMemberEqualityComparer.Instance);
			foreach (Symbol symbol in this.GetMembersUnordered())
			{
				foreach (Symbol symbol2 in symbol.GetExplicitInterfaceImplementations())
				{
					multiDictionary.Add(symbol2, symbol);
				}
			}
			return multiDictionary;
		}

		// Token: 0x170022A2 RID: 8866
		// (get) Token: 0x06007919 RID: 31001 RVA: 0x001BB42D File Offset: 0x001B962D
		internal ImmutableHashSet<Symbol> AbstractMembers
		{
			get
			{
				if (this._lazyAbstractMembers == null)
				{
					Interlocked.CompareExchange<ImmutableHashSet<Symbol>>(ref this._lazyAbstractMembers, this.ComputeAbstractMembers(), null);
				}
				return this._lazyAbstractMembers;
			}
		}

		// Token: 0x0600791A RID: 31002 RVA: 0x001BB450 File Offset: 0x001B9650
		private ImmutableHashSet<Symbol> ComputeAbstractMembers()
		{
			ImmutableHashSet<Symbol> immutableHashSet = ImmutableHashSet.Create<Symbol>();
			ImmutableHashSet<Symbol> immutableHashSet2 = ImmutableHashSet.Create<Symbol>();
			foreach (Symbol symbol in this.GetMembersUnordered())
			{
				if (this.IsAbstract && symbol.IsAbstract && symbol.Kind != SymbolKind.NamedType)
				{
					immutableHashSet = immutableHashSet.Add(symbol);
				}
				Symbol symbol2 = null;
				SymbolKind kind = symbol.Kind;
				if (kind != SymbolKind.Event)
				{
					if (kind != SymbolKind.Method)
					{
						if (kind == SymbolKind.Property)
						{
							symbol2 = ((PropertySymbol)symbol).OverriddenProperty;
						}
					}
					else
					{
						symbol2 = ((MethodSymbol)symbol).OverriddenMethod;
					}
				}
				else
				{
					symbol2 = ((EventSymbol)symbol).OverriddenEvent;
				}
				if (symbol2 != null)
				{
					immutableHashSet2 = immutableHashSet2.Add(symbol2);
				}
			}
			if (this.BaseTypeNoUseSiteDiagnostics != null && this.BaseTypeNoUseSiteDiagnostics.IsAbstract)
			{
				foreach (Symbol symbol3 in this.BaseTypeNoUseSiteDiagnostics.AbstractMembers)
				{
					if (!immutableHashSet2.Contains(symbol3))
					{
						immutableHashSet = immutableHashSet.Add(symbol3);
					}
				}
			}
			return immutableHashSet;
		}

		// Token: 0x0600791B RID: 31003 RVA: 0x001BB580 File Offset: 0x001B9780
		[Obsolete("Use TypeWithAnnotations.Is method.", true)]
		internal bool Equals(TypeWithAnnotations other)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600791C RID: 31004 RVA: 0x001BB587 File Offset: 0x001B9787
		public static bool Equals(TypeSymbol left, TypeSymbol right, TypeCompareKind comparison)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right, comparison);
		}

		// Token: 0x0600791D RID: 31005 RVA: 0x001BB599 File Offset: 0x001B9799
		[Obsolete("Use 'TypeSymbol.Equals(TypeSymbol, TypeSymbol, TypeCompareKind)' method.", true)]
		public static bool operator ==(TypeSymbol left, TypeSymbol right)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600791E RID: 31006 RVA: 0x001BB5A0 File Offset: 0x001B97A0
		[Obsolete("Use 'TypeSymbol.Equals(TypeSymbol, TypeSymbol, TypeCompareKind)' method.", true)]
		public static bool operator !=(TypeSymbol left, TypeSymbol right)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600791F RID: 31007 RVA: 0x001BB5A7 File Offset: 0x001B97A7
		[Obsolete("Use 'TypeSymbol.Equals(TypeSymbol, TypeSymbol, TypeCompareKind)' method.", true)]
		public static bool operator ==(Symbol left, TypeSymbol right)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06007920 RID: 31008 RVA: 0x001BB5AE File Offset: 0x001B97AE
		[Obsolete("Use 'TypeSymbol.Equals(TypeSymbol, TypeSymbol, TypeCompareKind)' method.", true)]
		public static bool operator !=(Symbol left, TypeSymbol right)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06007921 RID: 31009 RVA: 0x001BB5B5 File Offset: 0x001B97B5
		[Obsolete("Use 'TypeSymbol.Equals(TypeSymbol, TypeSymbol, TypeCompareKind)' method.", true)]
		public static bool operator ==(TypeSymbol left, Symbol right)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06007922 RID: 31010 RVA: 0x001BB5BC File Offset: 0x001B97BC
		[Obsolete("Use 'TypeSymbol.Equals(TypeSymbol, TypeSymbol, TypeCompareKind)' method.", true)]
		public static bool operator !=(TypeSymbol left, Symbol right)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06007923 RID: 31011 RVA: 0x001BB5C3 File Offset: 0x001B97C3
		internal ITypeSymbol GetITypeSymbol(NullableAnnotation nullableAnnotation)
		{
			if (nullableAnnotation == this.DefaultNullableAnnotation)
			{
				return (ITypeSymbol)base.ISymbol;
			}
			return this.CreateITypeSymbol(nullableAnnotation);
		}

		// Token: 0x170022A3 RID: 8867
		// (get) Token: 0x06007924 RID: 31012 RVA: 0x001BB5E1 File Offset: 0x001B97E1
		internal NullableAnnotation DefaultNullableAnnotation
		{
			get
			{
				return NullableAnnotationExtensions.ToPublicAnnotation(this, NullableAnnotation.Oblivious);
			}
		}

		// Token: 0x06007925 RID: 31013
		protected abstract ITypeSymbol CreateITypeSymbol(NullableAnnotation nullableAnnotation);

		// Token: 0x170022A4 RID: 8868
		// (get) Token: 0x06007926 RID: 31014 RVA: 0x001BB5EA File Offset: 0x001B97EA
		TypeKind ITypeSymbolInternal.TypeKind
		{
			get
			{
				return this.TypeKind;
			}
		}

		// Token: 0x170022A5 RID: 8869
		// (get) Token: 0x06007927 RID: 31015 RVA: 0x001BB5F2 File Offset: 0x001B97F2
		SpecialType ITypeSymbolInternal.SpecialType
		{
			get
			{
				return this.SpecialType;
			}
		}

		// Token: 0x170022A6 RID: 8870
		// (get) Token: 0x06007928 RID: 31016 RVA: 0x001BB5FA File Offset: 0x001B97FA
		bool ITypeSymbolInternal.IsReferenceType
		{
			get
			{
				return this.IsReferenceType;
			}
		}

		// Token: 0x170022A7 RID: 8871
		// (get) Token: 0x06007929 RID: 31017 RVA: 0x001BB602 File Offset: 0x001B9802
		bool ITypeSymbolInternal.IsValueType
		{
			get
			{
				return this.IsValueType;
			}
		}

		// Token: 0x0600792A RID: 31018 RVA: 0x001BB60A File Offset: 0x001B980A
		ITypeSymbol ITypeSymbolInternal.GetITypeSymbol()
		{
			return this.GetITypeSymbol(this.DefaultNullableAnnotation);
		}

		// Token: 0x170022A8 RID: 8872
		// (get) Token: 0x0600792B RID: 31019
		internal abstract bool IsRecord { get; }

		// Token: 0x170022A9 RID: 8873
		// (get) Token: 0x0600792C RID: 31020
		internal abstract bool IsRecordStruct { get; }

		// Token: 0x0600792E RID: 31022 RVA: 0x001BB63C File Offset: 0x001B983C
		[CompilerGenerated]
		internal static void <MakeAllInterfaces>g__addAllInterfaces|29_0(NamedTypeSymbol @interface, HashSet<NamedTypeSymbol> visited, ArrayBuilder<NamedTypeSymbol> result)
		{
			if (visited.Add(@interface))
			{
				ImmutableArray<NamedTypeSymbol> immutableArray = @interface.InterfacesNoUseSiteDiagnostics(null);
				for (int i = immutableArray.Length - 1; i >= 0; i--)
				{
					TypeSymbol.<MakeAllInterfaces>g__addAllInterfaces|29_0(immutableArray[i], visited, result);
				}
				result.Add(@interface);
			}
		}

		// Token: 0x0600792F RID: 31023 RVA: 0x001BB684 File Offset: 0x001B9884
		[CompilerGenerated]
		internal static bool <FindMostSpecificImplementationInInterfaces>g__stopLookup|89_0(MethodSymbol interfaceAccessor, TypeSymbol implementingType)
		{
			if (interfaceAccessor == null)
			{
				return false;
			}
			TypeSymbol.SymbolAndDiagnostics symbolAndDiagnostics = implementingType.FindImplementationForInterfaceMemberInNonInterfaceWithDiagnostics(interfaceAccessor, false);
			if (symbolAndDiagnostics.Symbol != null)
			{
				return !symbolAndDiagnostics.Symbol.ContainingType.IsInterface;
			}
			return !symbolAndDiagnostics.Diagnostics.Diagnostics.Any<Diagnostic>((Diagnostic d) => d.Code == 8705);
		}

		// Token: 0x06007930 RID: 31024 RVA: 0x001BB6F0 File Offset: 0x001B98F0
		[CompilerGenerated]
		internal static Symbol <FindMostSpecificImplementationInBases>g__findImplementationInInterface|91_0(Symbol interfaceMember, Symbol inplementingAccessor1, Symbol implementingAccessor2)
		{
			NamedTypeSymbol containingType = inplementingAccessor1.ContainingType;
			if (implementingAccessor2 != null && !containingType.Equals(implementingAccessor2.ContainingType, TypeCompareKind.ConsiderEverything))
			{
				return null;
			}
			MultiDictionary<Symbol, Symbol>.ValueSet valueSet = TypeSymbol.FindImplementationInInterface(interfaceMember, containingType);
			if (valueSet.Count == 1)
			{
				return valueSet.Single();
			}
			return null;
		}

		// Token: 0x06007931 RID: 31025 RVA: 0x001BB734 File Offset: 0x001B9934
		[CompilerGenerated]
		internal static Symbol <FindMostSpecificImplementationInBases>g__findMostSpecificImplementationInBases|91_1(Symbol interfaceMember, ImmutableArray<NamedTypeSymbol> allInterfaces, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, out Symbol conflictingImplementation1, out Symbol conflictingImplementation2)
		{
			ArrayBuilder<ValueTuple<MultiDictionary<Symbol, Symbol>.ValueSet, MultiDictionary<NamedTypeSymbol, NamedTypeSymbol>>> instance = ArrayBuilder<ValueTuple<MultiDictionary<Symbol, Symbol>.ValueSet, MultiDictionary<NamedTypeSymbol, NamedTypeSymbol>>>.GetInstance();
			foreach (NamedTypeSymbol namedTypeSymbol in allInterfaces)
			{
				if (namedTypeSymbol.IsInterface)
				{
					MultiDictionary<Symbol, Symbol>.ValueSet valueSet = TypeSymbol.FindImplementationInInterface(interfaceMember, namedTypeSymbol);
					if (valueSet.Count != 0)
					{
						for (int i = 0; i < instance.Count; i++)
						{
							ValueTuple<MultiDictionary<Symbol, Symbol>.ValueSet, MultiDictionary<NamedTypeSymbol, NamedTypeSymbol>> valueTuple = instance[i];
							MultiDictionary<Symbol, Symbol>.ValueSet item = valueTuple.Item1;
							MultiDictionary<NamedTypeSymbol, NamedTypeSymbol> multiDictionary = valueTuple.Item2;
							NamedTypeSymbol containingType = Enumerable.First<Symbol>(item).ContainingType;
							if (containingType.Equals(namedTypeSymbol, TypeCompareKind.CLRSignatureCompareOptions))
							{
								instance[i] = new ValueTuple<MultiDictionary<Symbol, Symbol>.ValueSet, MultiDictionary<NamedTypeSymbol, NamedTypeSymbol>>(valueSet, multiDictionary);
								valueSet = default(MultiDictionary<Symbol, Symbol>.ValueSet);
								break;
							}
							if (multiDictionary == null)
							{
								multiDictionary = containingType.InterfacesAndTheirBaseInterfacesWithDefinitionUseSiteDiagnostics(ref useSiteInfo);
								instance[i] = new ValueTuple<MultiDictionary<Symbol, Symbol>.ValueSet, MultiDictionary<NamedTypeSymbol, NamedTypeSymbol>>(item, multiDictionary);
							}
							if (multiDictionary.ContainsKey(namedTypeSymbol))
							{
								valueSet = default(MultiDictionary<Symbol, Symbol>.ValueSet);
								break;
							}
						}
						if (valueSet.Count != 0)
						{
							if (instance.Count != 0)
							{
								MultiDictionary<NamedTypeSymbol, NamedTypeSymbol> multiDictionary2 = namedTypeSymbol.InterfacesAndTheirBaseInterfacesWithDefinitionUseSiteDiagnostics(ref useSiteInfo);
								for (int j = instance.Count - 1; j >= 0; j--)
								{
									if (multiDictionary2.ContainsKey(Enumerable.First<Symbol>(instance[j].Item1).ContainingType))
									{
										instance.RemoveAt(j);
									}
								}
								instance.Add(new ValueTuple<MultiDictionary<Symbol, Symbol>.ValueSet, MultiDictionary<NamedTypeSymbol, NamedTypeSymbol>>(valueSet, multiDictionary2));
							}
							else
							{
								instance.Add(new ValueTuple<MultiDictionary<Symbol, Symbol>.ValueSet, MultiDictionary<NamedTypeSymbol, NamedTypeSymbol>>(valueSet, null));
							}
						}
					}
				}
			}
			int count = instance.Count;
			Symbol symbol;
			if (count != 0)
			{
				if (count != 1)
				{
					symbol = null;
					conflictingImplementation1 = Enumerable.First<Symbol>(instance[0].Item1);
					conflictingImplementation2 = Enumerable.First<Symbol>(instance[1].Item1);
				}
				else
				{
					MultiDictionary<Symbol, Symbol>.ValueSet item2 = instance[0].Item1;
					if (item2.Count == 1)
					{
						symbol = item2.Single();
						if (symbol.IsAbstract)
						{
							symbol = null;
						}
					}
					else
					{
						symbol = null;
					}
					conflictingImplementation1 = null;
					conflictingImplementation2 = null;
				}
			}
			else
			{
				symbol = null;
				conflictingImplementation1 = null;
				conflictingImplementation2 = null;
			}
			instance.Free();
			return symbol;
		}

		// Token: 0x04001D15 RID: 7445
		internal const string ImplicitTypeName = "<invalid-global-code>";

		// Token: 0x04001D16 RID: 7446
		private static readonly TypeSymbol.InterfaceInfo s_noInterfaces = new TypeSymbol.InterfaceInfo();

		// Token: 0x04001D17 RID: 7447
		private ImmutableHashSet<Symbol> _lazyAbstractMembers;

		// Token: 0x04001D18 RID: 7448
		private TypeSymbol.InterfaceInfo _lazyInterfaceInfo;

		// Token: 0x04001D19 RID: 7449
		private static readonly Func<TypeWithAnnotations, TypeWithAnnotations> s_setUnknownNullability = (TypeWithAnnotations type) => type.SetUnknownNullabilityForReferenceTypes();

		// Token: 0x0200095A RID: 2394
		private class InterfaceInfo
		{
			// Token: 0x170028E1 RID: 10465
			// (get) Token: 0x060090F4 RID: 37108 RVA: 0x001FCB80 File Offset: 0x001FAD80
			public ConcurrentDictionary<Symbol, TypeSymbol.SymbolAndDiagnostics> ImplementationForInterfaceMemberMap
			{
				get
				{
					ConcurrentDictionary<Symbol, TypeSymbol.SymbolAndDiagnostics> concurrentDictionary = this._implementationForInterfaceMemberMap;
					if (concurrentDictionary != null)
					{
						return concurrentDictionary;
					}
					concurrentDictionary = new ConcurrentDictionary<Symbol, TypeSymbol.SymbolAndDiagnostics>(1, 1, SymbolEqualityComparer.ConsiderEverything);
					return Interlocked.CompareExchange<ConcurrentDictionary<Symbol, TypeSymbol.SymbolAndDiagnostics>>(ref this._implementationForInterfaceMemberMap, concurrentDictionary, null) ?? concurrentDictionary;
				}
			}

			// Token: 0x060090F5 RID: 37109 RVA: 0x001FCBB8 File Offset: 0x001FADB8
			internal bool IsDefaultValue()
			{
				return this.allInterfaces.IsDefault && this.interfacesAndTheirBaseInterfaces == null && this._implementationForInterfaceMemberMap == null && this.explicitInterfaceImplementationMap == null;
			}

			// Token: 0x0400285B RID: 10331
			internal ImmutableArray<NamedTypeSymbol> allInterfaces;

			// Token: 0x0400285C RID: 10332
			internal MultiDictionary<NamedTypeSymbol, NamedTypeSymbol> interfacesAndTheirBaseInterfaces;

			// Token: 0x0400285D RID: 10333
			internal static readonly MultiDictionary<NamedTypeSymbol, NamedTypeSymbol> EmptyInterfacesAndTheirBaseInterfaces = new MultiDictionary<NamedTypeSymbol, NamedTypeSymbol>(0, SymbolEqualityComparer.CLRSignature, null);

			// Token: 0x0400285E RID: 10334
			private ConcurrentDictionary<Symbol, TypeSymbol.SymbolAndDiagnostics> _implementationForInterfaceMemberMap;

			// Token: 0x0400285F RID: 10335
			internal MultiDictionary<Symbol, Symbol> explicitInterfaceImplementationMap;
		}

		// Token: 0x0200095B RID: 2395
		protected class ExplicitInterfaceImplementationTargetMemberEqualityComparer : IEqualityComparer<Symbol>
		{
			// Token: 0x060090F8 RID: 37112 RVA: 0x001FCBFD File Offset: 0x001FADFD
			private ExplicitInterfaceImplementationTargetMemberEqualityComparer()
			{
			}

			// Token: 0x060090F9 RID: 37113 RVA: 0x001FCC05 File Offset: 0x001FAE05
			public bool Equals(Symbol x, Symbol y)
			{
				return x.OriginalDefinition == y.OriginalDefinition && x.ContainingType.Equals(y.ContainingType, TypeCompareKind.CLRSignatureCompareOptions);
			}

			// Token: 0x060090FA RID: 37114 RVA: 0x001FCC2F File Offset: 0x001FAE2F
			public int GetHashCode(Symbol obj)
			{
				return obj.OriginalDefinition.GetHashCode();
			}

			// Token: 0x04002860 RID: 10336
			public static readonly TypeSymbol.ExplicitInterfaceImplementationTargetMemberEqualityComparer Instance = new TypeSymbol.ExplicitInterfaceImplementationTargetMemberEqualityComparer();
		}

		// Token: 0x0200095C RID: 2396
		protected class SymbolAndDiagnostics
		{
			// Token: 0x060090FC RID: 37116 RVA: 0x001FCC48 File Offset: 0x001FAE48
			public SymbolAndDiagnostics(Symbol symbol, ImmutableBindingDiagnostic<AssemblySymbol> diagnostics)
			{
				this.Symbol = symbol;
				this.Diagnostics = diagnostics;
			}

			// Token: 0x04002861 RID: 10337
			public static readonly TypeSymbol.SymbolAndDiagnostics Empty = new TypeSymbol.SymbolAndDiagnostics(null, ImmutableBindingDiagnostic<AssemblySymbol>.Empty);

			// Token: 0x04002862 RID: 10338
			public readonly Symbol Symbol;

			// Token: 0x04002863 RID: 10339
			public readonly ImmutableBindingDiagnostic<AssemblySymbol> Diagnostics;
		}
	}
}
