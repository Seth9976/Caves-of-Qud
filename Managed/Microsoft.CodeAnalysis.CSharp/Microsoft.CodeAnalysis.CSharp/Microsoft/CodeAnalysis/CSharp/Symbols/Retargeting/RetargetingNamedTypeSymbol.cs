using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.Retargeting
{
	// Token: 0x020005D8 RID: 1496
	internal sealed class RetargetingNamedTypeSymbol : WrappedNamedTypeSymbol
	{
		// Token: 0x06007D1D RID: 32029 RVA: 0x001C27D3 File Offset: 0x001C09D3
		public RetargetingNamedTypeSymbol(RetargetingModuleSymbol retargetingModule, NamedTypeSymbol underlyingType, NamedTypeSymbol.TupleExtraData tupleData = null)
			: base(underlyingType, tupleData)
		{
			this._retargetingModule = retargetingModule;
		}

		// Token: 0x06007D1E RID: 32030 RVA: 0x001C2805 File Offset: 0x001C0A05
		protected override NamedTypeSymbol WithTupleDataCore(NamedTypeSymbol.TupleExtraData newData)
		{
			return new RetargetingNamedTypeSymbol(this._retargetingModule, this._underlyingType, newData);
		}

		// Token: 0x170024DB RID: 9435
		// (get) Token: 0x06007D1F RID: 32031 RVA: 0x001C2819 File Offset: 0x001C0A19
		private RetargetingModuleSymbol.RetargetingSymbolTranslator RetargetingTranslator
		{
			get
			{
				return this._retargetingModule.RetargetingTranslator;
			}
		}

		// Token: 0x170024DC RID: 9436
		// (get) Token: 0x06007D20 RID: 32032 RVA: 0x001C2828 File Offset: 0x001C0A28
		public override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			get
			{
				if (this._lazyTypeParameters.IsDefault)
				{
					if (this.Arity == 0)
					{
						this._lazyTypeParameters = ImmutableArray<TypeParameterSymbol>.Empty;
					}
					else
					{
						ImmutableInterlocked.InterlockedCompareExchange<TypeParameterSymbol>(ref this._lazyTypeParameters, this.RetargetingTranslator.Retarget(this._underlyingType.TypeParameters), default(ImmutableArray<TypeParameterSymbol>));
					}
				}
				return this._lazyTypeParameters;
			}
		}

		// Token: 0x170024DD RID: 9437
		// (get) Token: 0x06007D21 RID: 32033 RVA: 0x001C2888 File Offset: 0x001C0A88
		internal override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotationsNoUseSiteDiagnostics
		{
			get
			{
				return base.GetTypeParametersAsTypeArguments();
			}
		}

		// Token: 0x170024DE RID: 9438
		// (get) Token: 0x06007D22 RID: 32034 RVA: 0x001C2890 File Offset: 0x001C0A90
		public override NamedTypeSymbol ConstructedFrom
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170024DF RID: 9439
		// (get) Token: 0x06007D23 RID: 32035 RVA: 0x001C2894 File Offset: 0x001C0A94
		public override NamedTypeSymbol EnumUnderlyingType
		{
			get
			{
				NamedTypeSymbol enumUnderlyingType = this._underlyingType.EnumUnderlyingType;
				if (enumUnderlyingType != null)
				{
					return this.RetargetingTranslator.Retarget(enumUnderlyingType, RetargetOptions.RetargetPrimitiveTypesByTypeCode);
				}
				return null;
			}
		}

		// Token: 0x170024E0 RID: 9440
		// (get) Token: 0x06007D24 RID: 32036 RVA: 0x001C28BF File Offset: 0x001C0ABF
		public override IEnumerable<string> MemberNames
		{
			get
			{
				return this._underlyingType.MemberNames;
			}
		}

		// Token: 0x06007D25 RID: 32037 RVA: 0x001C28CC File Offset: 0x001C0ACC
		public override ImmutableArray<Symbol> GetMembers()
		{
			return this.RetargetingTranslator.Retarget(this._underlyingType.GetMembers());
		}

		// Token: 0x06007D26 RID: 32038 RVA: 0x001C28E4 File Offset: 0x001C0AE4
		internal override ImmutableArray<Symbol> GetMembersUnordered()
		{
			return this.RetargetingTranslator.Retarget(this._underlyingType.GetMembersUnordered());
		}

		// Token: 0x06007D27 RID: 32039 RVA: 0x001C28FC File Offset: 0x001C0AFC
		public override ImmutableArray<Symbol> GetMembers(string name)
		{
			return this.RetargetingTranslator.Retarget(this._underlyingType.GetMembers(name));
		}

		// Token: 0x06007D28 RID: 32040 RVA: 0x001C2918 File Offset: 0x001C0B18
		public override void InitializeTupleFieldDefinitionsToIndexMap()
		{
			SmallDictionary<FieldSymbol, int> smallDictionary = new SmallDictionary<FieldSymbol, int>(ReferenceEqualityComparer.Instance);
			foreach (KeyValuePair<FieldSymbol, int> keyValuePair in this._underlyingType.TupleFieldDefinitionsToIndexMap)
			{
				FieldSymbol fieldSymbol;
				int num;
				keyValuePair.Deconstruct<FieldSymbol, int>(out fieldSymbol, out num);
				FieldSymbol fieldSymbol2 = fieldSymbol;
				int num2 = num;
				smallDictionary.Add(this.RetargetingTranslator.Retarget(fieldSymbol2), num2);
			}
			base.TupleData.SetFieldDefinitionsToIndexMap(smallDictionary);
		}

		// Token: 0x06007D29 RID: 32041 RVA: 0x001C2981 File Offset: 0x001C0B81
		internal override IEnumerable<FieldSymbol> GetFieldsToEmit()
		{
			foreach (FieldSymbol fieldSymbol in this._underlyingType.GetFieldsToEmit())
			{
				yield return this.RetargetingTranslator.Retarget(fieldSymbol);
			}
			IEnumerator<FieldSymbol> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06007D2A RID: 32042 RVA: 0x001C2991 File Offset: 0x001C0B91
		internal override IEnumerable<MethodSymbol> GetMethodsToEmit()
		{
			bool isInterface = this._underlyingType.IsInterfaceType();
			foreach (MethodSymbol methodSymbol in this._underlyingType.GetMethodsToEmit())
			{
				int gapSize = (isInterface ? ModuleExtensions.GetVTableGapSize(methodSymbol.MetadataName) : 0);
				if (gapSize > 0)
				{
					do
					{
						yield return null;
						int num = gapSize;
						gapSize = num - 1;
					}
					while (gapSize > 0);
				}
				else
				{
					yield return this.RetargetingTranslator.Retarget(methodSymbol);
				}
			}
			IEnumerator<MethodSymbol> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06007D2B RID: 32043 RVA: 0x001C29A1 File Offset: 0x001C0BA1
		internal override IEnumerable<PropertySymbol> GetPropertiesToEmit()
		{
			foreach (PropertySymbol propertySymbol in this._underlyingType.GetPropertiesToEmit())
			{
				yield return this.RetargetingTranslator.Retarget(propertySymbol);
			}
			IEnumerator<PropertySymbol> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06007D2C RID: 32044 RVA: 0x001C29B1 File Offset: 0x001C0BB1
		internal override IEnumerable<EventSymbol> GetEventsToEmit()
		{
			foreach (EventSymbol eventSymbol in this._underlyingType.GetEventsToEmit())
			{
				yield return this.RetargetingTranslator.Retarget(eventSymbol);
			}
			IEnumerator<EventSymbol> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06007D2D RID: 32045 RVA: 0x001C29C1 File Offset: 0x001C0BC1
		internal override ImmutableArray<Symbol> GetEarlyAttributeDecodingMembers()
		{
			return this.RetargetingTranslator.Retarget(this._underlyingType.GetEarlyAttributeDecodingMembers());
		}

		// Token: 0x06007D2E RID: 32046 RVA: 0x001C29D9 File Offset: 0x001C0BD9
		internal override ImmutableArray<Symbol> GetEarlyAttributeDecodingMembers(string name)
		{
			return this.RetargetingTranslator.Retarget(this._underlyingType.GetEarlyAttributeDecodingMembers(name));
		}

		// Token: 0x06007D2F RID: 32047 RVA: 0x001C29F2 File Offset: 0x001C0BF2
		internal override ImmutableArray<NamedTypeSymbol> GetTypeMembersUnordered()
		{
			return this.RetargetingTranslator.Retarget(this._underlyingType.GetTypeMembersUnordered());
		}

		// Token: 0x06007D30 RID: 32048 RVA: 0x001C2A0A File Offset: 0x001C0C0A
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers()
		{
			return this.RetargetingTranslator.Retarget(this._underlyingType.GetTypeMembers());
		}

		// Token: 0x06007D31 RID: 32049 RVA: 0x001C2A22 File Offset: 0x001C0C22
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name)
		{
			return this.RetargetingTranslator.Retarget(this._underlyingType.GetTypeMembers(name));
		}

		// Token: 0x06007D32 RID: 32050 RVA: 0x001C2A3B File Offset: 0x001C0C3B
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name, int arity)
		{
			return this.RetargetingTranslator.Retarget(this._underlyingType.GetTypeMembers(name, arity));
		}

		// Token: 0x170024E1 RID: 9441
		// (get) Token: 0x06007D33 RID: 32051 RVA: 0x001C2A55 File Offset: 0x001C0C55
		public override Symbol ContainingSymbol
		{
			get
			{
				return this.RetargetingTranslator.Retarget(this._underlyingType.ContainingSymbol);
			}
		}

		// Token: 0x06007D34 RID: 32052 RVA: 0x001C2A6D File Offset: 0x001C0C6D
		public override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			return this.RetargetingTranslator.GetRetargetedAttributes(this._underlyingType.GetAttributes(), ref this._lazyCustomAttributes);
		}

		// Token: 0x06007D35 RID: 32053 RVA: 0x001C2A8B File Offset: 0x001C0C8B
		internal override IEnumerable<CSharpAttributeData> GetCustomAttributesToEmit(PEModuleBuilder moduleBuilder)
		{
			return this.RetargetingTranslator.RetargetAttributes(this._underlyingType.GetCustomAttributesToEmit(moduleBuilder));
		}

		// Token: 0x170024E2 RID: 9442
		// (get) Token: 0x06007D36 RID: 32054 RVA: 0x001C2AA4 File Offset: 0x001C0CA4
		public override AssemblySymbol ContainingAssembly
		{
			get
			{
				return this._retargetingModule.ContainingAssembly;
			}
		}

		// Token: 0x170024E3 RID: 9443
		// (get) Token: 0x06007D37 RID: 32055 RVA: 0x001C2AB1 File Offset: 0x001C0CB1
		internal override ModuleSymbol ContainingModule
		{
			get
			{
				return this._retargetingModule;
			}
		}

		// Token: 0x06007D38 RID: 32056 RVA: 0x001C2AB9 File Offset: 0x001C0CB9
		internal override NamedTypeSymbol LookupMetadataType(ref MetadataTypeName typeName)
		{
			return this.RetargetingTranslator.Retarget(this._underlyingType.LookupMetadataType(ref typeName), RetargetOptions.RetargetPrimitiveTypesByName);
		}

		// Token: 0x06007D39 RID: 32057 RVA: 0x001C2AD4 File Offset: 0x001C0CD4
		private static ExtendedErrorTypeSymbol CyclicInheritanceError(RetargetingNamedTypeSymbol type, TypeSymbol declaredBase)
		{
			CSDiagnosticInfo csdiagnosticInfo = new CSDiagnosticInfo(ErrorCode.ERR_ImportedCircularBase, new object[] { declaredBase, type });
			return new ExtendedErrorTypeSymbol(declaredBase, LookupResultKind.NotReferencable, csdiagnosticInfo, true);
		}

		// Token: 0x170024E4 RID: 9444
		// (get) Token: 0x06007D3A RID: 32058 RVA: 0x001C2B04 File Offset: 0x001C0D04
		internal override NamedTypeSymbol BaseTypeNoUseSiteDiagnostics
		{
			get
			{
				if (this._lazyBaseType == ErrorTypeSymbol.UnknownResultType)
				{
					NamedTypeSymbol namedTypeSymbol = this.GetDeclaredBaseType(null);
					if (namedTypeSymbol == null)
					{
						NamedTypeSymbol baseTypeNoUseSiteDiagnostics = this._underlyingType.BaseTypeNoUseSiteDiagnostics;
						if (baseTypeNoUseSiteDiagnostics != null)
						{
							namedTypeSymbol = this.RetargetingTranslator.Retarget(baseTypeNoUseSiteDiagnostics, RetargetOptions.RetargetPrimitiveTypesByName);
						}
					}
					if (namedTypeSymbol != null && BaseTypeAnalysis.TypeDependsOn(namedTypeSymbol, this))
					{
						return RetargetingNamedTypeSymbol.CyclicInheritanceError(this, namedTypeSymbol);
					}
					Interlocked.CompareExchange<NamedTypeSymbol>(ref this._lazyBaseType, namedTypeSymbol, ErrorTypeSymbol.UnknownResultType);
				}
				return this._lazyBaseType;
			}
		}

		// Token: 0x06007D3B RID: 32059 RVA: 0x001C2B74 File Offset: 0x001C0D74
		internal override ImmutableArray<NamedTypeSymbol> InterfacesNoUseSiteDiagnostics(ConsList<TypeSymbol> basesBeingResolved)
		{
			if (this._lazyInterfaces.IsDefault)
			{
				ImmutableArray<NamedTypeSymbol> declaredInterfaces = this.GetDeclaredInterfaces(basesBeingResolved);
				if (!this.IsInterface)
				{
					return declaredInterfaces;
				}
				ImmutableArray<NamedTypeSymbol> immutableArray = declaredInterfaces.SelectAsArray<NamedTypeSymbol, NamedTypeSymbol>(delegate(NamedTypeSymbol t)
				{
					if (!BaseTypeAnalysis.TypeDependsOn(t, this))
					{
						return t;
					}
					return RetargetingNamedTypeSymbol.CyclicInheritanceError(this, t);
				});
				ImmutableInterlocked.InterlockedCompareExchange<NamedTypeSymbol>(ref this._lazyInterfaces, immutableArray, default(ImmutableArray<NamedTypeSymbol>));
			}
			return this._lazyInterfaces;
		}

		// Token: 0x06007D3C RID: 32060 RVA: 0x001C2BCF File Offset: 0x001C0DCF
		internal override ImmutableArray<NamedTypeSymbol> GetInterfacesToEmit()
		{
			return this.RetargetingTranslator.Retarget(this._underlyingType.GetInterfacesToEmit());
		}

		// Token: 0x06007D3D RID: 32061 RVA: 0x001C2BE8 File Offset: 0x001C0DE8
		internal override NamedTypeSymbol GetDeclaredBaseType(ConsList<TypeSymbol> basesBeingResolved)
		{
			if (this._lazyDeclaredBaseType == ErrorTypeSymbol.UnknownResultType)
			{
				NamedTypeSymbol declaredBaseType = this._underlyingType.GetDeclaredBaseType(basesBeingResolved);
				NamedTypeSymbol namedTypeSymbol = ((declaredBaseType != null) ? this.RetargetingTranslator.Retarget(declaredBaseType, RetargetOptions.RetargetPrimitiveTypesByName) : null);
				Interlocked.CompareExchange<NamedTypeSymbol>(ref this._lazyDeclaredBaseType, namedTypeSymbol, ErrorTypeSymbol.UnknownResultType);
			}
			return this._lazyDeclaredBaseType;
		}

		// Token: 0x06007D3E RID: 32062 RVA: 0x001C2C3C File Offset: 0x001C0E3C
		internal override ImmutableArray<NamedTypeSymbol> GetDeclaredInterfaces(ConsList<TypeSymbol> basesBeingResolved)
		{
			if (this._lazyDeclaredInterfaces.IsDefault)
			{
				ImmutableArray<NamedTypeSymbol> declaredInterfaces = this._underlyingType.GetDeclaredInterfaces(basesBeingResolved);
				ImmutableArray<NamedTypeSymbol> immutableArray = this.RetargetingTranslator.Retarget(declaredInterfaces);
				ImmutableInterlocked.InterlockedCompareExchange<NamedTypeSymbol>(ref this._lazyDeclaredInterfaces, immutableArray, default(ImmutableArray<NamedTypeSymbol>));
			}
			return this._lazyDeclaredInterfaces;
		}

		// Token: 0x06007D3F RID: 32063 RVA: 0x001C2C8C File Offset: 0x001C0E8C
		internal override UseSiteInfo<AssemblySymbol> GetUseSiteInfo()
		{
			if (!this._lazyCachedUseSiteInfo.IsInitialized)
			{
				AssemblySymbol primaryDependency = base.PrimaryDependency;
				this._lazyCachedUseSiteInfo.Initialize(primaryDependency, new UseSiteInfo<AssemblySymbol>(primaryDependency).AdjustDiagnosticInfo(base.CalculateUseSiteDiagnostic()));
			}
			return this._lazyCachedUseSiteInfo.ToUseSiteInfo(base.PrimaryDependency);
		}

		// Token: 0x170024E5 RID: 9445
		// (get) Token: 0x06007D40 RID: 32064 RVA: 0x001C2CE0 File Offset: 0x001C0EE0
		internal override NamedTypeSymbol ComImportCoClass
		{
			get
			{
				NamedTypeSymbol comImportCoClass = this._underlyingType.ComImportCoClass;
				if (comImportCoClass != null)
				{
					return this.RetargetingTranslator.Retarget(comImportCoClass, RetargetOptions.RetargetPrimitiveTypesByName);
				}
				return null;
			}
		}

		// Token: 0x170024E6 RID: 9446
		// (get) Token: 0x06007D41 RID: 32065 RVA: 0x001C2D0B File Offset: 0x001C0F0B
		internal override bool IsComImport
		{
			get
			{
				return this._underlyingType.IsComImport;
			}
		}

		// Token: 0x170024E7 RID: 9447
		// (get) Token: 0x06007D42 RID: 32066 RVA: 0x001C2D18 File Offset: 0x001C0F18
		internal sealed override CSharpCompilation DeclaringCompilation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170024E8 RID: 9448
		// (get) Token: 0x06007D43 RID: 32067 RVA: 0x001C2D1B File Offset: 0x001C0F1B
		public sealed override bool AreLocalsZeroed
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x06007D44 RID: 32068 RVA: 0x001C2D22 File Offset: 0x001C0F22
		internal sealed override NamedTypeSymbol AsNativeInteger()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x170024E9 RID: 9449
		// (get) Token: 0x06007D45 RID: 32069 RVA: 0x001C2D29 File Offset: 0x001C0F29
		internal sealed override NamedTypeSymbol NativeIntegerUnderlyingType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170024EA RID: 9450
		// (get) Token: 0x06007D46 RID: 32070 RVA: 0x001C2D2C File Offset: 0x001C0F2C
		internal sealed override bool IsRecord
		{
			get
			{
				return this._underlyingType.IsRecord;
			}
		}

		// Token: 0x170024EB RID: 9451
		// (get) Token: 0x06007D47 RID: 32071 RVA: 0x001C2D39 File Offset: 0x001C0F39
		internal sealed override bool IsRecordStruct
		{
			get
			{
				return this._underlyingType.IsRecordStruct;
			}
		}

		// Token: 0x06007D48 RID: 32072 RVA: 0x001C2D46 File Offset: 0x001C0F46
		internal sealed override bool HasPossibleWellKnownCloneMethod()
		{
			return this._underlyingType.HasPossibleWellKnownCloneMethod();
		}

		// Token: 0x04001D7B RID: 7547
		private readonly RetargetingModuleSymbol _retargetingModule;

		// Token: 0x04001D7C RID: 7548
		private ImmutableArray<TypeParameterSymbol> _lazyTypeParameters;

		// Token: 0x04001D7D RID: 7549
		private NamedTypeSymbol _lazyBaseType = ErrorTypeSymbol.UnknownResultType;

		// Token: 0x04001D7E RID: 7550
		private ImmutableArray<NamedTypeSymbol> _lazyInterfaces;

		// Token: 0x04001D7F RID: 7551
		private NamedTypeSymbol _lazyDeclaredBaseType = ErrorTypeSymbol.UnknownResultType;

		// Token: 0x04001D80 RID: 7552
		private ImmutableArray<NamedTypeSymbol> _lazyDeclaredInterfaces;

		// Token: 0x04001D81 RID: 7553
		private ImmutableArray<CSharpAttributeData> _lazyCustomAttributes;

		// Token: 0x04001D82 RID: 7554
		private CachedUseSiteInfo<AssemblySymbol> _lazyCachedUseSiteInfo = CachedUseSiteInfo<AssemblySymbol>.Uninitialized;
	}
}
