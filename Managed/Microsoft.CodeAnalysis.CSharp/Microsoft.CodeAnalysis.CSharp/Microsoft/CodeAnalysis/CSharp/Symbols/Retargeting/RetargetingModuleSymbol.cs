using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.Retargeting
{
	// Token: 0x020005D7 RID: 1495
	internal sealed class RetargetingModuleSymbol : NonMissingModuleSymbol
	{
		// Token: 0x06007CFF RID: 31999 RVA: 0x001C2430 File Offset: 0x001C0630
		public RetargetingModuleSymbol(RetargetingAssemblySymbol retargetingAssembly, SourceModuleSymbol underlyingModule)
		{
			this._retargetingAssembly = retargetingAssembly;
			this._underlyingModule = underlyingModule;
			this.RetargetingTranslator = new RetargetingModuleSymbol.RetargetingSymbolTranslator(this);
			this._createRetargetingMethod = new Func<Symbol, RetargetingMethodSymbol>(this.CreateRetargetingMethod);
			this._createRetargetingNamespace = new Func<Symbol, RetargetingNamespaceSymbol>(this.CreateRetargetingNamespace);
			this._createRetargetingNamedType = new Func<Symbol, RetargetingNamedTypeSymbol>(this.CreateRetargetingNamedType);
			this._createRetargetingField = new Func<Symbol, FieldSymbol>(this.CreateRetargetingField);
			this._createRetargetingProperty = new Func<Symbol, RetargetingPropertySymbol>(this.CreateRetargetingProperty);
			this._createRetargetingEvent = new Func<Symbol, RetargetingEventSymbol>(this.CreateRetargetingEvent);
			this._createRetargetingTypeParameter = new Func<Symbol, RetargetingTypeParameterSymbol>(this.CreateRetargetingTypeParameter);
		}

		// Token: 0x170024CA RID: 9418
		// (get) Token: 0x06007D00 RID: 32000 RVA: 0x001C24F3 File Offset: 0x001C06F3
		internal override int Ordinal
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170024CB RID: 9419
		// (get) Token: 0x06007D01 RID: 32001 RVA: 0x001C24F6 File Offset: 0x001C06F6
		internal override Machine Machine
		{
			get
			{
				return this._underlyingModule.Machine;
			}
		}

		// Token: 0x170024CC RID: 9420
		// (get) Token: 0x06007D02 RID: 32002 RVA: 0x001C2503 File Offset: 0x001C0703
		internal override bool Bit32Required
		{
			get
			{
				return this._underlyingModule.Bit32Required;
			}
		}

		// Token: 0x170024CD RID: 9421
		// (get) Token: 0x06007D03 RID: 32003 RVA: 0x001C2510 File Offset: 0x001C0710
		public SourceModuleSymbol UnderlyingModule
		{
			get
			{
				return this._underlyingModule;
			}
		}

		// Token: 0x170024CE RID: 9422
		// (get) Token: 0x06007D04 RID: 32004 RVA: 0x001C2518 File Offset: 0x001C0718
		public override NamespaceSymbol GlobalNamespace
		{
			get
			{
				return this.RetargetingTranslator.Retarget(this._underlyingModule.GlobalNamespace);
			}
		}

		// Token: 0x170024CF RID: 9423
		// (get) Token: 0x06007D05 RID: 32005 RVA: 0x001C2530 File Offset: 0x001C0730
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return this._underlyingModule.IsImplicitlyDeclared;
			}
		}

		// Token: 0x170024D0 RID: 9424
		// (get) Token: 0x06007D06 RID: 32006 RVA: 0x001C253D File Offset: 0x001C073D
		public override string Name
		{
			get
			{
				return this._underlyingModule.Name;
			}
		}

		// Token: 0x06007D07 RID: 32007 RVA: 0x001C254A File Offset: 0x001C074A
		public override string GetDocumentationCommentXml(CultureInfo preferredCulture = null, bool expandIncludes = false, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this._underlyingModule.GetDocumentationCommentXml(preferredCulture, expandIncludes, cancellationToken);
		}

		// Token: 0x170024D1 RID: 9425
		// (get) Token: 0x06007D08 RID: 32008 RVA: 0x001C255A File Offset: 0x001C075A
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._retargetingAssembly;
			}
		}

		// Token: 0x170024D2 RID: 9426
		// (get) Token: 0x06007D09 RID: 32009 RVA: 0x001C2562 File Offset: 0x001C0762
		public override AssemblySymbol ContainingAssembly
		{
			get
			{
				return this._retargetingAssembly;
			}
		}

		// Token: 0x170024D3 RID: 9427
		// (get) Token: 0x06007D0A RID: 32010 RVA: 0x001C256A File Offset: 0x001C076A
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return this._underlyingModule.Locations;
			}
		}

		// Token: 0x06007D0B RID: 32011 RVA: 0x001C2578 File Offset: 0x001C0778
		internal override void SetReferences(ModuleReferences<AssemblySymbol> moduleReferences, SourceAssemblySymbol originatingSourceAssemblyDebugOnly)
		{
			base.SetReferences(moduleReferences, originatingSourceAssemblyDebugOnly);
			this._retargetingAssemblyMap.Clear();
			ImmutableArray<AssemblySymbol> referencedAssemblySymbols = this._underlyingModule.GetReferencedAssemblySymbols();
			ImmutableArray<AssemblySymbol> symbols = moduleReferences.Symbols;
			int i = 0;
			int num = 0;
			while (i < symbols.Length)
			{
				while (referencedAssemblySymbols[num].IsLinked)
				{
					num++;
				}
				RetargetingModuleSymbol.DestinationData destinationData;
				if (symbols[i] != referencedAssemblySymbols[num] && !this._retargetingAssemblyMap.TryGetValue(referencedAssemblySymbols[num], ref destinationData))
				{
					this._retargetingAssemblyMap.Add(referencedAssemblySymbols[num], new RetargetingModuleSymbol.DestinationData
					{
						To = symbols[i]
					});
				}
				i++;
				num++;
			}
		}

		// Token: 0x06007D0C RID: 32012 RVA: 0x001C262C File Offset: 0x001C082C
		internal bool RetargetingDefinitions(AssemblySymbol from, out AssemblySymbol to)
		{
			RetargetingModuleSymbol.DestinationData destinationData;
			if (!this._retargetingAssemblyMap.TryGetValue(from, ref destinationData))
			{
				to = null;
				return false;
			}
			to = destinationData.To;
			return true;
		}

		// Token: 0x170024D4 RID: 9428
		// (get) Token: 0x06007D0D RID: 32013 RVA: 0x001C2657 File Offset: 0x001C0857
		internal override ICollection<string> TypeNames
		{
			get
			{
				return this._underlyingModule.TypeNames;
			}
		}

		// Token: 0x170024D5 RID: 9429
		// (get) Token: 0x06007D0E RID: 32014 RVA: 0x001C2664 File Offset: 0x001C0864
		internal override ICollection<string> NamespaceNames
		{
			get
			{
				return this._underlyingModule.NamespaceNames;
			}
		}

		// Token: 0x06007D0F RID: 32015 RVA: 0x001C2671 File Offset: 0x001C0871
		public override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			return this.RetargetingTranslator.GetRetargetedAttributes(this._underlyingModule.GetAttributes(), ref this._lazyCustomAttributes);
		}

		// Token: 0x170024D6 RID: 9430
		// (get) Token: 0x06007D10 RID: 32016 RVA: 0x001C268F File Offset: 0x001C088F
		internal override bool HasAssemblyCompilationRelaxationsAttribute
		{
			get
			{
				return this._underlyingModule.HasAssemblyCompilationRelaxationsAttribute;
			}
		}

		// Token: 0x170024D7 RID: 9431
		// (get) Token: 0x06007D11 RID: 32017 RVA: 0x001C269C File Offset: 0x001C089C
		internal override bool HasAssemblyRuntimeCompatibilityAttribute
		{
			get
			{
				return this._underlyingModule.HasAssemblyRuntimeCompatibilityAttribute;
			}
		}

		// Token: 0x170024D8 RID: 9432
		// (get) Token: 0x06007D12 RID: 32018 RVA: 0x001C26A9 File Offset: 0x001C08A9
		internal override CharSet? DefaultMarshallingCharSet
		{
			get
			{
				return this._underlyingModule.DefaultMarshallingCharSet;
			}
		}

		// Token: 0x170024D9 RID: 9433
		// (get) Token: 0x06007D13 RID: 32019 RVA: 0x001C26B6 File Offset: 0x001C08B6
		internal sealed override CSharpCompilation DeclaringCompilation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007D14 RID: 32020 RVA: 0x001C26B9 File Offset: 0x001C08B9
		public override ModuleMetadata GetMetadata()
		{
			return this._underlyingModule.GetMetadata();
		}

		// Token: 0x170024DA RID: 9434
		// (get) Token: 0x06007D15 RID: 32021 RVA: 0x001C26C6 File Offset: 0x001C08C6
		public sealed override bool AreLocalsZeroed
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x06007D16 RID: 32022 RVA: 0x001C26CD File Offset: 0x001C08CD
		private RetargetingMethodSymbol CreateRetargetingMethod(Symbol symbol)
		{
			return new RetargetingMethodSymbol(this, (MethodSymbol)symbol);
		}

		// Token: 0x06007D17 RID: 32023 RVA: 0x001C26DB File Offset: 0x001C08DB
		private RetargetingNamespaceSymbol CreateRetargetingNamespace(Symbol symbol)
		{
			return new RetargetingNamespaceSymbol(this, (NamespaceSymbol)symbol);
		}

		// Token: 0x06007D18 RID: 32024 RVA: 0x001C26E9 File Offset: 0x001C08E9
		private RetargetingNamedTypeSymbol CreateRetargetingNamedType(Symbol symbol)
		{
			return new RetargetingNamedTypeSymbol(this, (NamedTypeSymbol)symbol, null);
		}

		// Token: 0x06007D19 RID: 32025 RVA: 0x001C26F8 File Offset: 0x001C08F8
		private FieldSymbol CreateRetargetingField(Symbol symbol)
		{
			TupleErrorFieldSymbol tupleErrorFieldSymbol = symbol as TupleErrorFieldSymbol;
			if (tupleErrorFieldSymbol != null)
			{
				FieldSymbol correspondingTupleField = tupleErrorFieldSymbol.CorrespondingTupleField;
				TupleErrorFieldSymbol tupleErrorFieldSymbol2 = ((correspondingTupleField == tupleErrorFieldSymbol) ? null : ((TupleErrorFieldSymbol)this.RetargetingTranslator.Retarget(correspondingTupleField)));
				return new TupleErrorFieldSymbol(this.RetargetingTranslator.Retarget(tupleErrorFieldSymbol.ContainingType, RetargetOptions.RetargetPrimitiveTypesByName), tupleErrorFieldSymbol.Name, tupleErrorFieldSymbol.TupleElementIndex, tupleErrorFieldSymbol.Locations.IsEmpty ? null : tupleErrorFieldSymbol.Locations[0], this.RetargetingTranslator.Retarget(tupleErrorFieldSymbol.TypeWithAnnotations, RetargetOptions.RetargetPrimitiveTypesByTypeCode, null), tupleErrorFieldSymbol.GetUseSiteInfo().DiagnosticInfo, tupleErrorFieldSymbol.IsImplicitlyDeclared, tupleErrorFieldSymbol2);
			}
			return new RetargetingFieldSymbol(this, (FieldSymbol)symbol);
		}

		// Token: 0x06007D1A RID: 32026 RVA: 0x001C27A9 File Offset: 0x001C09A9
		private RetargetingPropertySymbol CreateRetargetingProperty(Symbol symbol)
		{
			return new RetargetingPropertySymbol(this, (PropertySymbol)symbol);
		}

		// Token: 0x06007D1B RID: 32027 RVA: 0x001C27B7 File Offset: 0x001C09B7
		private RetargetingEventSymbol CreateRetargetingEvent(Symbol symbol)
		{
			return new RetargetingEventSymbol(this, (EventSymbol)symbol);
		}

		// Token: 0x06007D1C RID: 32028 RVA: 0x001C27C5 File Offset: 0x001C09C5
		private RetargetingTypeParameterSymbol CreateRetargetingTypeParameter(Symbol symbol)
		{
			return new RetargetingTypeParameterSymbol(this, (TypeParameterSymbol)symbol);
		}

		// Token: 0x04001D6E RID: 7534
		private readonly RetargetingAssemblySymbol _retargetingAssembly;

		// Token: 0x04001D6F RID: 7535
		private readonly SourceModuleSymbol _underlyingModule;

		// Token: 0x04001D70 RID: 7536
		private readonly Dictionary<AssemblySymbol, RetargetingModuleSymbol.DestinationData> _retargetingAssemblyMap = new Dictionary<AssemblySymbol, RetargetingModuleSymbol.DestinationData>();

		// Token: 0x04001D71 RID: 7537
		internal readonly RetargetingModuleSymbol.RetargetingSymbolTranslator RetargetingTranslator;

		// Token: 0x04001D72 RID: 7538
		private ImmutableArray<CSharpAttributeData> _lazyCustomAttributes;

		// Token: 0x04001D73 RID: 7539
		private readonly ConcurrentDictionary<Symbol, Symbol> _symbolMap = new ConcurrentDictionary<Symbol, Symbol>(2, 4);

		// Token: 0x04001D74 RID: 7540
		private readonly Func<Symbol, RetargetingMethodSymbol> _createRetargetingMethod;

		// Token: 0x04001D75 RID: 7541
		private readonly Func<Symbol, RetargetingNamespaceSymbol> _createRetargetingNamespace;

		// Token: 0x04001D76 RID: 7542
		private readonly Func<Symbol, RetargetingTypeParameterSymbol> _createRetargetingTypeParameter;

		// Token: 0x04001D77 RID: 7543
		private readonly Func<Symbol, RetargetingNamedTypeSymbol> _createRetargetingNamedType;

		// Token: 0x04001D78 RID: 7544
		private readonly Func<Symbol, FieldSymbol> _createRetargetingField;

		// Token: 0x04001D79 RID: 7545
		private readonly Func<Symbol, RetargetingPropertySymbol> _createRetargetingProperty;

		// Token: 0x04001D7A RID: 7546
		private readonly Func<Symbol, RetargetingEventSymbol> _createRetargetingEvent;

		// Token: 0x02000972 RID: 2418
		private struct DestinationData
		{
			// Token: 0x170028F2 RID: 10482
			// (get) Token: 0x0600919E RID: 37278 RVA: 0x001FDB43 File Offset: 0x001FBD43
			public ConcurrentDictionary<NamedTypeSymbol, NamedTypeSymbol> SymbolMap
			{
				get
				{
					return LazyInitializer.EnsureInitialized<ConcurrentDictionary<NamedTypeSymbol, NamedTypeSymbol>>(ref this._symbolMap);
				}
			}

			// Token: 0x040028AB RID: 10411
			public AssemblySymbol To;

			// Token: 0x040028AC RID: 10412
			private ConcurrentDictionary<NamedTypeSymbol, NamedTypeSymbol> _symbolMap;
		}

		// Token: 0x02000973 RID: 2419
		internal class RetargetingSymbolTranslator : CSharpSymbolVisitor<RetargetOptions, Symbol>
		{
			// Token: 0x0600919F RID: 37279 RVA: 0x001FDB50 File Offset: 0x001FBD50
			public RetargetingSymbolTranslator(RetargetingModuleSymbol retargetingModule)
			{
				this._retargetingModule = retargetingModule;
			}

			// Token: 0x170028F3 RID: 10483
			// (get) Token: 0x060091A0 RID: 37280 RVA: 0x001FDB5F File Offset: 0x001FBD5F
			private ConcurrentDictionary<Symbol, Symbol> SymbolMap
			{
				get
				{
					return this._retargetingModule._symbolMap;
				}
			}

			// Token: 0x170028F4 RID: 10484
			// (get) Token: 0x060091A1 RID: 37281 RVA: 0x001FDB6C File Offset: 0x001FBD6C
			private RetargetingAssemblySymbol RetargetingAssembly
			{
				get
				{
					return this._retargetingModule._retargetingAssembly;
				}
			}

			// Token: 0x170028F5 RID: 10485
			// (get) Token: 0x060091A2 RID: 37282 RVA: 0x001FDB79 File Offset: 0x001FBD79
			private SourceModuleSymbol UnderlyingModule
			{
				get
				{
					return this._retargetingModule._underlyingModule;
				}
			}

			// Token: 0x170028F6 RID: 10486
			// (get) Token: 0x060091A3 RID: 37283 RVA: 0x001FDB86 File Offset: 0x001FBD86
			private Dictionary<AssemblySymbol, RetargetingModuleSymbol.DestinationData> RetargetingAssemblyMap
			{
				get
				{
					return this._retargetingModule._retargetingAssemblyMap;
				}
			}

			// Token: 0x060091A4 RID: 37284 RVA: 0x001FDB93 File Offset: 0x001FBD93
			public Symbol Retarget(Symbol symbol)
			{
				return symbol.Accept<RetargetOptions, Symbol>(this, RetargetOptions.RetargetPrimitiveTypesByName);
			}

			// Token: 0x060091A5 RID: 37285 RVA: 0x001FDB9D File Offset: 0x001FBD9D
			public MarshalPseudoCustomAttributeData Retarget(MarshalPseudoCustomAttributeData marshallingInfo)
			{
				if (marshallingInfo == null)
				{
					return null;
				}
				return marshallingInfo.WithTranslatedTypes<TypeSymbol, RetargetingModuleSymbol.RetargetingSymbolTranslator>((TypeSymbol type, RetargetingModuleSymbol.RetargetingSymbolTranslator translator) => translator.Retarget(type, RetargetOptions.RetargetPrimitiveTypesByTypeCode), this);
			}

			// Token: 0x060091A6 RID: 37286 RVA: 0x001FDBCA File Offset: 0x001FBDCA
			public TypeSymbol Retarget(TypeSymbol symbol, RetargetOptions options)
			{
				return (TypeSymbol)symbol.Accept<RetargetOptions, Symbol>(this, options);
			}

			// Token: 0x060091A7 RID: 37287 RVA: 0x001FDBDC File Offset: 0x001FBDDC
			public TypeWithAnnotations Retarget(TypeWithAnnotations underlyingType, RetargetOptions options, NamedTypeSymbol asDynamicIfNoPiaContainingType = null)
			{
				TypeSymbol typeSymbol = this.Retarget(underlyingType.Type, options);
				if (asDynamicIfNoPiaContainingType != null)
				{
					typeSymbol = typeSymbol.AsDynamicIfNoPia(asDynamicIfNoPiaContainingType);
				}
				bool flag;
				ImmutableArray<CustomModifier> immutableArray = this.RetargetModifiers(underlyingType.CustomModifiers, out flag);
				if (flag || !TypeSymbol.Equals(underlyingType.Type, typeSymbol, TypeCompareKind.ConsiderEverything))
				{
					return underlyingType.WithTypeAndModifiers(typeSymbol, immutableArray);
				}
				return underlyingType;
			}

			// Token: 0x060091A8 RID: 37288 RVA: 0x001FDC31 File Offset: 0x001FBE31
			public NamespaceSymbol Retarget(NamespaceSymbol ns)
			{
				return (NamespaceSymbol)this.SymbolMap.GetOrAdd(ns, this._retargetingModule._createRetargetingNamespace);
			}

			// Token: 0x060091A9 RID: 37289 RVA: 0x001FDC50 File Offset: 0x001FBE50
			private NamedTypeSymbol RetargetNamedTypeDefinition(NamedTypeSymbol type, RetargetOptions options)
			{
				if (type.IsNativeIntegerType)
				{
					NamedTypeSymbol namedTypeSymbol = this.RetargetNamedTypeDefinition(type.NativeIntegerUnderlyingType, options);
					if (namedTypeSymbol.SpecialType != SpecialType.None)
					{
						return namedTypeSymbol.AsNativeInteger();
					}
					return namedTypeSymbol;
				}
				else
				{
					if (options == RetargetOptions.RetargetPrimitiveTypesByTypeCode)
					{
						PrimitiveTypeCode primitiveTypeCode = type.PrimitiveTypeCode;
						if (primitiveTypeCode != PrimitiveTypeCode.NotPrimitive)
						{
							return this.RetargetingAssembly.GetPrimitiveType(primitiveTypeCode);
						}
					}
					if (type.Kind == SymbolKind.ErrorType)
					{
						return RetargetingModuleSymbol.RetargetingSymbolTranslator.Retarget((ErrorTypeSymbol)type);
					}
					AssemblySymbol containingAssembly = type.ContainingAssembly;
					bool flag;
					if (containingAssembly == this.RetargetingAssembly.UnderlyingAssembly)
					{
						flag = type.IsExplicitDefinitionOfNoPiaLocalType;
					}
					else
					{
						flag = containingAssembly.IsLinked;
					}
					if (flag)
					{
						return this.RetargetNoPiaLocalType(type);
					}
					if (containingAssembly == this.RetargetingAssembly.UnderlyingAssembly)
					{
						return this.RetargetNamedTypeDefinitionFromUnderlyingAssembly(type);
					}
					RetargetingModuleSymbol.DestinationData destinationData;
					if (!this.RetargetingAssemblyMap.TryGetValue(containingAssembly, ref destinationData))
					{
						return type;
					}
					type = RetargetingModuleSymbol.RetargetingSymbolTranslator.PerformTypeRetargeting(ref destinationData, type);
					this.RetargetingAssemblyMap[containingAssembly] = destinationData;
					return type;
				}
			}

			// Token: 0x060091AA RID: 37290 RVA: 0x001FDD2C File Offset: 0x001FBF2C
			private NamedTypeSymbol RetargetNamedTypeDefinitionFromUnderlyingAssembly(NamedTypeSymbol type)
			{
				ModuleSymbol containingModule = type.ContainingModule;
				if (containingModule == this.UnderlyingModule)
				{
					for (NamedTypeSymbol namedTypeSymbol = type.ContainingType; namedTypeSymbol != null; namedTypeSymbol = namedTypeSymbol.ContainingType)
					{
						if (namedTypeSymbol.IsExplicitDefinitionOfNoPiaLocalType)
						{
							return (NamedTypeSymbol)this.SymbolMap.GetOrAdd(type, new UnsupportedMetadataTypeSymbol(null));
						}
					}
					return (NamedTypeSymbol)this.SymbolMap.GetOrAdd(type, this._retargetingModule._createRetargetingNamedType);
				}
				PEModuleSymbol pemoduleSymbol = (PEModuleSymbol)this.RetargetingAssembly.Modules[containingModule.Ordinal];
				return RetargetingModuleSymbol.RetargetingSymbolTranslator.RetargetNamedTypeDefinition((PENamedTypeSymbol)type, pemoduleSymbol);
			}

			// Token: 0x060091AB RID: 37291 RVA: 0x001FDDC4 File Offset: 0x001FBFC4
			private NamedTypeSymbol RetargetNoPiaLocalType(NamedTypeSymbol type)
			{
				ConcurrentDictionary<NamedTypeSymbol, NamedTypeSymbol> noPiaUnificationMap = this.RetargetingAssembly.NoPiaUnificationMap;
				NamedTypeSymbol orAdd;
				if (noPiaUnificationMap.TryGetValue(type, ref orAdd))
				{
					return orAdd;
				}
				NamedTypeSymbol namedTypeSymbol;
				if (type.ContainingSymbol.Kind != SymbolKind.NamedType && type.Arity == 0)
				{
					bool isInterface = type.IsInterface;
					bool flag = false;
					string text = null;
					string text2 = null;
					if (isInterface)
					{
						flag = type.GetGuidString(out text);
					}
					MetadataTypeName metadataTypeName = MetadataTypeName.FromFullName(type.ToDisplayString(SymbolDisplayFormat.QualifiedNameOnlyFormat), false, type.Arity);
					string text3 = null;
					if (type.ContainingModule == this._retargetingModule.UnderlyingModule)
					{
						foreach (CSharpAttributeData csharpAttributeData in type.GetAttributes())
						{
							int targetAttributeSignatureIndex = csharpAttributeData.GetTargetAttributeSignatureIndex(type, AttributeDescription.TypeIdentifierAttribute);
							if (targetAttributeSignatureIndex != -1)
							{
								if (targetAttributeSignatureIndex == 1 && csharpAttributeData.CommonConstructorArguments.Length == 2)
								{
									text2 = csharpAttributeData.CommonConstructorArguments[0].ValueInternal as string;
									text3 = csharpAttributeData.CommonConstructorArguments[1].ValueInternal as string;
									break;
								}
								break;
							}
						}
					}
					else if (!flag || !isInterface)
					{
						type.ContainingAssembly.GetGuidString(out text2);
						text3 = metadataTypeName.FullName;
					}
					namedTypeSymbol = MetadataDecoder.SubstituteNoPiaLocalType(ref metadataTypeName, isInterface, type.BaseTypeNoUseSiteDiagnostics, text, text2, text3, this.RetargetingAssembly);
				}
				else
				{
					namedTypeSymbol = new UnsupportedMetadataTypeSymbol(null);
				}
				orAdd = noPiaUnificationMap.GetOrAdd(type, namedTypeSymbol);
				return orAdd;
			}

			// Token: 0x060091AC RID: 37292 RVA: 0x001FDF40 File Offset: 0x001FC140
			private static NamedTypeSymbol RetargetNamedTypeDefinition(PENamedTypeSymbol type, PEModuleSymbol addedModule)
			{
				TypeSymbol typeSymbol;
				if (addedModule.TypeHandleToTypeMap.TryGetValue(type.Handle, ref typeSymbol))
				{
					return (NamedTypeSymbol)typeSymbol;
				}
				NamedTypeSymbol containingType = type.ContainingType;
				NamedTypeSymbol namedTypeSymbol;
				if (containingType != null)
				{
					NamespaceOrTypeSymbol namespaceOrTypeSymbol = RetargetingModuleSymbol.RetargetingSymbolTranslator.RetargetNamedTypeDefinition((PENamedTypeSymbol)containingType, addedModule);
					MetadataTypeName metadataTypeName = MetadataTypeName.FromTypeName(type.MetadataName, false, type.Arity);
					namedTypeSymbol = namespaceOrTypeSymbol.LookupMetadataType(ref metadataTypeName);
				}
				else
				{
					MetadataTypeName metadataTypeName = MetadataTypeName.FromNamespaceAndTypeName(type.ContainingNamespace.ToDisplayString(SymbolDisplayFormat.QualifiedNameOnlyFormat), type.MetadataName, false, type.Arity);
					namedTypeSymbol = addedModule.LookupTopLevelMetadataType(ref metadataTypeName);
				}
				return namedTypeSymbol;
			}

			// Token: 0x060091AD RID: 37293 RVA: 0x001FDFCC File Offset: 0x001FC1CC
			private static NamedTypeSymbol PerformTypeRetargeting(ref RetargetingModuleSymbol.DestinationData destination, NamedTypeSymbol type)
			{
				NamedTypeSymbol orAdd;
				if (!destination.SymbolMap.TryGetValue(type, ref orAdd))
				{
					NamedTypeSymbol containingType = type.ContainingType;
					NamedTypeSymbol namedTypeSymbol;
					if (containingType != null)
					{
						NamespaceOrTypeSymbol namespaceOrTypeSymbol = RetargetingModuleSymbol.RetargetingSymbolTranslator.PerformTypeRetargeting(ref destination, containingType);
						MetadataTypeName metadataTypeName = MetadataTypeName.FromTypeName(type.MetadataName, false, type.Arity);
						namedTypeSymbol = namespaceOrTypeSymbol.LookupMetadataType(ref metadataTypeName);
					}
					else
					{
						MetadataTypeName metadataTypeName = MetadataTypeName.FromNamespaceAndTypeName(type.ContainingNamespace.ToDisplayString(SymbolDisplayFormat.QualifiedNameOnlyFormat), type.MetadataName, false, type.Arity);
						namedTypeSymbol = destination.To.LookupTopLevelMetadataType(ref metadataTypeName, true);
					}
					orAdd = destination.SymbolMap.GetOrAdd(type, namedTypeSymbol);
				}
				return orAdd;
			}

			// Token: 0x060091AE RID: 37294 RVA: 0x001FE058 File Offset: 0x001FC258
			public NamedTypeSymbol Retarget(NamedTypeSymbol type, RetargetOptions options)
			{
				NamedTypeSymbol originalDefinition = type.OriginalDefinition;
				NamedTypeSymbol namedTypeSymbol = this.RetargetNamedTypeDefinition(originalDefinition, options);
				if (type == originalDefinition)
				{
					return namedTypeSymbol;
				}
				if (namedTypeSymbol.Kind == SymbolKind.ErrorType && !namedTypeSymbol.IsGenericType)
				{
					return namedTypeSymbol;
				}
				if (type.IsUnboundGenericType)
				{
					if (namedTypeSymbol == originalDefinition)
					{
						return type;
					}
					return namedTypeSymbol.AsUnboundGenericType();
				}
				else
				{
					NamedTypeSymbol namedTypeSymbol2 = type;
					ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance();
					int num = int.MaxValue;
					while (namedTypeSymbol2 != null)
					{
						if (num == 2147483647 && !namedTypeSymbol2.IsInterface)
						{
							num = instance.Count;
						}
						instance.AddRange(namedTypeSymbol2.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics);
						namedTypeSymbol2 = namedTypeSymbol2.ContainingType;
					}
					bool flag = !originalDefinition.Equals(namedTypeSymbol);
					ArrayBuilder<TypeWithAnnotations> instance2 = ArrayBuilder<TypeWithAnnotations>.GetInstance(instance.Count);
					foreach (TypeWithAnnotations typeWithAnnotations in instance)
					{
						TypeWithAnnotations typeWithAnnotations2 = this.Retarget(typeWithAnnotations, RetargetOptions.RetargetPrimitiveTypesByTypeCode, null);
						if (!flag && !typeWithAnnotations2.IsSameAs(typeWithAnnotations))
						{
							flag = true;
						}
						instance2.Add(typeWithAnnotations2);
					}
					bool flag2 = this.IsNoPiaIllegalGenericInstantiation(instance, instance2, num);
					instance.Free();
					NamedTypeSymbol namedTypeSymbol3;
					if (!flag)
					{
						namedTypeSymbol3 = type;
					}
					else
					{
						namedTypeSymbol2 = namedTypeSymbol;
						ArrayBuilder<TypeParameterSymbol> instance3 = ArrayBuilder<TypeParameterSymbol>.GetInstance(instance2.Count);
						while (namedTypeSymbol2 != null)
						{
							if (namedTypeSymbol2.Arity > 0)
							{
								instance3.AddRange(namedTypeSymbol2.TypeParameters);
							}
							namedTypeSymbol2 = namedTypeSymbol2.ContainingType;
						}
						namedTypeSymbol3 = new TypeMap(instance3.ToImmutableAndFree(), instance2.ToImmutable(), false).SubstituteNamedType(namedTypeSymbol).WithTupleDataFrom(type);
					}
					instance2.Free();
					if (flag2)
					{
						return new NoPiaIllegalGenericInstantiationSymbol(this._retargetingModule, namedTypeSymbol3);
					}
					return namedTypeSymbol3;
				}
			}

			// Token: 0x060091AF RID: 37295 RVA: 0x001FE1CC File Offset: 0x001FC3CC
			private bool IsNoPiaIllegalGenericInstantiation(ArrayBuilder<TypeWithAnnotations> oldArguments, ArrayBuilder<TypeWithAnnotations> newArguments, int startOfNonInterfaceArguments)
			{
				if (this.UnderlyingModule.ContainsExplicitDefinitionOfNoPiaLocalTypes)
				{
					for (int i = startOfNonInterfaceArguments; i < oldArguments.Count; i++)
					{
						if (this.IsOrClosedOverAnExplicitLocalType(oldArguments[i].Type))
						{
							return true;
						}
					}
				}
				ImmutableArray<AssemblySymbol> assembliesToEmbedTypesFrom = this.UnderlyingModule.GetAssembliesToEmbedTypesFrom();
				if (assembliesToEmbedTypesFrom.Length > 0)
				{
					for (int j = startOfNonInterfaceArguments; j < oldArguments.Count; j++)
					{
						if (MetadataDecoder.IsOrClosedOverATypeFromAssemblies(oldArguments[j].Type, assembliesToEmbedTypesFrom))
						{
							return true;
						}
					}
				}
				ImmutableArray<AssemblySymbol> linkedReferencedAssemblies = this.RetargetingAssembly.GetLinkedReferencedAssemblies();
				if (!linkedReferencedAssemblies.IsDefaultOrEmpty)
				{
					for (int k = startOfNonInterfaceArguments; k < newArguments.Count; k++)
					{
						if (MetadataDecoder.IsOrClosedOverATypeFromAssemblies(newArguments[k].Type, linkedReferencedAssemblies))
						{
							return true;
						}
					}
				}
				return false;
			}

			// Token: 0x060091B0 RID: 37296 RVA: 0x001FE29C File Offset: 0x001FC49C
			private bool IsOrClosedOverAnExplicitLocalType(TypeSymbol symbol)
			{
				SymbolKind kind = symbol.Kind;
				if (kind <= SymbolKind.NamedType)
				{
					switch (kind)
					{
					case SymbolKind.ArrayType:
						return this.IsOrClosedOverAnExplicitLocalType(((ArrayTypeSymbol)symbol).ElementType);
					case SymbolKind.Assembly:
						goto IL_00CC;
					case SymbolKind.DynamicType:
						return false;
					case SymbolKind.ErrorType:
						break;
					default:
						if (kind != SymbolKind.NamedType)
						{
							goto IL_00CC;
						}
						break;
					}
					NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)symbol;
					if (symbol.OriginalDefinition.ContainingModule == this._retargetingModule.UnderlyingModule && namedTypeSymbol.IsExplicitDefinitionOfNoPiaLocalType)
					{
						return true;
					}
					for (;;)
					{
						foreach (TypeWithAnnotations typeWithAnnotations in namedTypeSymbol.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics)
						{
							if (this.IsOrClosedOverAnExplicitLocalType(typeWithAnnotations.Type))
							{
								return true;
							}
						}
						namedTypeSymbol = namedTypeSymbol.ContainingType;
						if (namedTypeSymbol == null)
						{
							return false;
						}
					}
					return true;
				}
				else
				{
					if (kind == SymbolKind.PointerType)
					{
						return this.IsOrClosedOverAnExplicitLocalType(((PointerTypeSymbol)symbol).PointedAtType);
					}
					if (kind == SymbolKind.TypeParameter)
					{
						return false;
					}
				}
				IL_00CC:
				throw ExceptionUtilities.UnexpectedValue(symbol.Kind);
			}

			// Token: 0x060091B1 RID: 37297 RVA: 0x001FE385 File Offset: 0x001FC585
			public virtual TypeParameterSymbol Retarget(TypeParameterSymbol typeParameter)
			{
				return (TypeParameterSymbol)this.SymbolMap.GetOrAdd(typeParameter, this._retargetingModule._createRetargetingTypeParameter);
			}

			// Token: 0x060091B2 RID: 37298 RVA: 0x001FE3A4 File Offset: 0x001FC5A4
			public ArrayTypeSymbol Retarget(ArrayTypeSymbol type)
			{
				TypeWithAnnotations elementTypeWithAnnotations = type.ElementTypeWithAnnotations;
				TypeWithAnnotations typeWithAnnotations = this.Retarget(elementTypeWithAnnotations, RetargetOptions.RetargetPrimitiveTypesByTypeCode, null);
				if (elementTypeWithAnnotations.IsSameAs(typeWithAnnotations))
				{
					return type;
				}
				if (type.IsSZArray)
				{
					return ArrayTypeSymbol.CreateSZArray(this.RetargetingAssembly, typeWithAnnotations);
				}
				return ArrayTypeSymbol.CreateMDArray(this.RetargetingAssembly, typeWithAnnotations, type.Rank, type.Sizes, type.LowerBounds);
			}

			// Token: 0x060091B3 RID: 37299 RVA: 0x001FE404 File Offset: 0x001FC604
			internal ImmutableArray<CustomModifier> RetargetModifiers(ImmutableArray<CustomModifier> oldModifiers, out bool modifiersHaveChanged)
			{
				ArrayBuilder<CustomModifier> arrayBuilder = null;
				for (int i = 0; i < oldModifiers.Length; i++)
				{
					CustomModifier customModifier = oldModifiers[i];
					NamedTypeSymbol modifierSymbol = ((CSharpCustomModifier)customModifier).ModifierSymbol;
					NamedTypeSymbol namedTypeSymbol = this.Retarget(modifierSymbol, RetargetOptions.RetargetPrimitiveTypesByName);
					if (!namedTypeSymbol.Equals(modifierSymbol))
					{
						if (arrayBuilder == null)
						{
							arrayBuilder = ArrayBuilder<CustomModifier>.GetInstance(oldModifiers.Length);
							arrayBuilder.AddRange(oldModifiers, i);
						}
						arrayBuilder.Add(customModifier.IsOptional ? CSharpCustomModifier.CreateOptional(namedTypeSymbol) : CSharpCustomModifier.CreateRequired(namedTypeSymbol));
					}
					else if (arrayBuilder != null)
					{
						arrayBuilder.Add(customModifier);
					}
				}
				modifiersHaveChanged = arrayBuilder != null;
				if (!modifiersHaveChanged)
				{
					return oldModifiers;
				}
				return arrayBuilder.ToImmutableAndFree();
			}

			// Token: 0x060091B4 RID: 37300 RVA: 0x001FE4A4 File Offset: 0x001FC6A4
			public PointerTypeSymbol Retarget(PointerTypeSymbol type)
			{
				TypeWithAnnotations pointedAtTypeWithAnnotations = type.PointedAtTypeWithAnnotations;
				TypeWithAnnotations typeWithAnnotations = this.Retarget(pointedAtTypeWithAnnotations, RetargetOptions.RetargetPrimitiveTypesByTypeCode, null);
				if (pointedAtTypeWithAnnotations.IsSameAs(typeWithAnnotations))
				{
					return type;
				}
				return new PointerTypeSymbol(typeWithAnnotations);
			}

			// Token: 0x060091B5 RID: 37301 RVA: 0x001FE4D4 File Offset: 0x001FC6D4
			public FunctionPointerTypeSymbol Retarget(FunctionPointerTypeSymbol type)
			{
				FunctionPointerMethodSymbol signature = type.Signature;
				TypeWithAnnotations typeWithAnnotations = this.Retarget(signature.ReturnTypeWithAnnotations, RetargetOptions.RetargetPrimitiveTypesByTypeCode, null);
				bool flag;
				ImmutableArray<CustomModifier> immutableArray = this.RetargetModifiers(signature.RefCustomModifiers, out flag);
				flag = flag || !signature.ReturnTypeWithAnnotations.IsSameAs(typeWithAnnotations);
				ImmutableArray<TypeWithAnnotations> immutableArray2 = ImmutableArray<TypeWithAnnotations>.Empty;
				ImmutableArray<ImmutableArray<CustomModifier>> immutableArray3 = default(ImmutableArray<ImmutableArray<CustomModifier>>);
				int parameterCount = signature.ParameterCount;
				if (parameterCount > 0)
				{
					ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance(parameterCount);
					ArrayBuilder<ImmutableArray<CustomModifier>> instance2 = ArrayBuilder<ImmutableArray<CustomModifier>>.GetInstance(parameterCount);
					bool flag2 = false;
					foreach (ParameterSymbol parameterSymbol in signature.Parameters)
					{
						TypeWithAnnotations typeWithAnnotations2 = this.Retarget(parameterSymbol.TypeWithAnnotations, RetargetOptions.RetargetPrimitiveTypesByTypeCode, null);
						bool flag3;
						ImmutableArray<CustomModifier> immutableArray4 = this.RetargetModifiers(parameterSymbol.RefCustomModifiers, out flag3);
						instance.Add(typeWithAnnotations2);
						instance2.Add(immutableArray4);
						flag2 = flag2 || !parameterSymbol.TypeWithAnnotations.IsSameAs(typeWithAnnotations2) || flag3;
					}
					if (flag2)
					{
						immutableArray2 = instance.ToImmutableAndFree();
						immutableArray3 = instance2.ToImmutableAndFree();
						flag = true;
					}
					else
					{
						instance.Free();
						instance2.Free();
						immutableArray2 = signature.ParameterTypesWithAnnotations;
					}
				}
				if (flag)
				{
					return type.SubstituteTypeSymbol(typeWithAnnotations, immutableArray2, immutableArray, immutableArray3);
				}
				return type;
			}

			// Token: 0x060091B6 RID: 37302 RVA: 0x001FE610 File Offset: 0x001FC810
			public static ErrorTypeSymbol Retarget(ErrorTypeSymbol type)
			{
				DiagnosticInfo diagnosticInfo = type.GetUseSiteInfo().DiagnosticInfo;
				if (diagnosticInfo != null && diagnosticInfo.Severity == DiagnosticSeverity.Error)
				{
					return type;
				}
				ExtendedErrorTypeSymbol extendedErrorTypeSymbol = type as ExtendedErrorTypeSymbol;
				ExtendedErrorTypeSymbol extendedErrorTypeSymbol2;
				if ((extendedErrorTypeSymbol2 = ((extendedErrorTypeSymbol != null) ? extendedErrorTypeSymbol.AsUnreported() : null)) == null)
				{
					LookupResultKind resultKind = type.ResultKind;
					DiagnosticInfo diagnosticInfo2;
					if ((diagnosticInfo2 = type.ErrorInfo) == null)
					{
						ErrorCode errorCode = ErrorCode.ERR_ErrorInReferencedAssembly;
						object[] array = new object[1];
						int num = 0;
						AssemblySymbol containingAssembly = type.ContainingAssembly;
						array[num] = ((containingAssembly != null) ? containingAssembly.Identity.GetDisplayName(false) : null) ?? string.Empty;
						diagnosticInfo2 = new CSDiagnosticInfo(errorCode, array);
					}
					extendedErrorTypeSymbol2 = new ExtendedErrorTypeSymbol(type, resultKind, diagnosticInfo2, true);
				}
				return extendedErrorTypeSymbol2;
			}

			// Token: 0x060091B7 RID: 37303 RVA: 0x001FE69C File Offset: 0x001FC89C
			public ImmutableArray<Symbol> Retarget(ImmutableArray<Symbol> arr)
			{
				ArrayBuilder<Symbol> instance = ArrayBuilder<Symbol>.GetInstance(arr.Length);
				foreach (Symbol symbol in arr)
				{
					instance.Add(this.Retarget(symbol));
				}
				return instance.ToImmutableAndFree();
			}

			// Token: 0x060091B8 RID: 37304 RVA: 0x001FE6E4 File Offset: 0x001FC8E4
			public ImmutableArray<NamedTypeSymbol> Retarget(ImmutableArray<NamedTypeSymbol> sequence)
			{
				ArrayBuilder<NamedTypeSymbol> instance = ArrayBuilder<NamedTypeSymbol>.GetInstance(sequence.Length);
				foreach (NamedTypeSymbol namedTypeSymbol in sequence)
				{
					instance.Add(this.Retarget(namedTypeSymbol, RetargetOptions.RetargetPrimitiveTypesByName));
				}
				return instance.ToImmutableAndFree();
			}

			// Token: 0x060091B9 RID: 37305 RVA: 0x001FE730 File Offset: 0x001FC930
			public ImmutableArray<TypeSymbol> Retarget(ImmutableArray<TypeSymbol> sequence)
			{
				ArrayBuilder<TypeSymbol> instance = ArrayBuilder<TypeSymbol>.GetInstance(sequence.Length);
				foreach (TypeSymbol typeSymbol in sequence)
				{
					instance.Add(this.Retarget(typeSymbol, RetargetOptions.RetargetPrimitiveTypesByName));
				}
				return instance.ToImmutableAndFree();
			}

			// Token: 0x060091BA RID: 37306 RVA: 0x001FE77C File Offset: 0x001FC97C
			public ImmutableArray<TypeWithAnnotations> Retarget(ImmutableArray<TypeWithAnnotations> sequence)
			{
				ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance(sequence.Length);
				foreach (TypeWithAnnotations typeWithAnnotations in sequence)
				{
					instance.Add(this.Retarget(typeWithAnnotations, RetargetOptions.RetargetPrimitiveTypesByName, null));
				}
				return instance.ToImmutableAndFree();
			}

			// Token: 0x060091BB RID: 37307 RVA: 0x001FE7C8 File Offset: 0x001FC9C8
			public ImmutableArray<TypeParameterSymbol> Retarget(ImmutableArray<TypeParameterSymbol> list)
			{
				ArrayBuilder<TypeParameterSymbol> instance = ArrayBuilder<TypeParameterSymbol>.GetInstance(list.Length);
				foreach (TypeParameterSymbol typeParameterSymbol in list)
				{
					instance.Add(this.Retarget(typeParameterSymbol));
				}
				return instance.ToImmutableAndFree();
			}

			// Token: 0x060091BC RID: 37308 RVA: 0x001FE810 File Offset: 0x001FCA10
			public MethodSymbol Retarget(MethodSymbol method)
			{
				return (MethodSymbol)this.SymbolMap.GetOrAdd(method, this._retargetingModule._createRetargetingMethod);
			}

			// Token: 0x060091BD RID: 37309 RVA: 0x001FE830 File Offset: 0x001FCA30
			public MethodSymbol Retarget(MethodSymbol method, IEqualityComparer<MethodSymbol> retargetedMethodComparer)
			{
				if (method.ContainingModule == this.UnderlyingModule && method == method.OriginalDefinition)
				{
					return this.Retarget(method);
				}
				NamedTypeSymbol containingType = method.ContainingType;
				NamedTypeSymbol namedTypeSymbol = this.Retarget(containingType, RetargetOptions.RetargetPrimitiveTypesByName);
				if (namedTypeSymbol != containingType)
				{
					return this.FindMethodInRetargetedType(method, namedTypeSymbol, retargetedMethodComparer);
				}
				return method;
			}

			// Token: 0x060091BE RID: 37310 RVA: 0x001FE87B File Offset: 0x001FCA7B
			public FieldSymbol Retarget(FieldSymbol field)
			{
				return (FieldSymbol)this.SymbolMap.GetOrAdd(field, this._retargetingModule._createRetargetingField);
			}

			// Token: 0x060091BF RID: 37311 RVA: 0x001FE899 File Offset: 0x001FCA99
			public PropertySymbol Retarget(PropertySymbol property)
			{
				return (PropertySymbol)this.SymbolMap.GetOrAdd(property, this._retargetingModule._createRetargetingProperty);
			}

			// Token: 0x060091C0 RID: 37312 RVA: 0x001FE8B8 File Offset: 0x001FCAB8
			public PropertySymbol Retarget(PropertySymbol property, IEqualityComparer<PropertySymbol> retargetedPropertyComparer)
			{
				if (property.ContainingModule == this.UnderlyingModule && property == property.OriginalDefinition)
				{
					return this.Retarget(property);
				}
				NamedTypeSymbol containingType = property.ContainingType;
				NamedTypeSymbol namedTypeSymbol = this.Retarget(containingType, RetargetOptions.RetargetPrimitiveTypesByName);
				if (namedTypeSymbol != containingType)
				{
					return this.FindPropertyInRetargetedType(property, namedTypeSymbol, retargetedPropertyComparer);
				}
				return property;
			}

			// Token: 0x060091C1 RID: 37313 RVA: 0x001FE904 File Offset: 0x001FCB04
			public EventSymbol Retarget(EventSymbol @event)
			{
				if (@event.ContainingModule == this.UnderlyingModule && @event == @event.OriginalDefinition)
				{
					return (EventSymbol)this.SymbolMap.GetOrAdd(@event, this._retargetingModule._createRetargetingEvent);
				}
				NamedTypeSymbol containingType = @event.ContainingType;
				NamedTypeSymbol namedTypeSymbol = this.Retarget(containingType, RetargetOptions.RetargetPrimitiveTypesByName);
				if (namedTypeSymbol != containingType)
				{
					return this.FindEventInRetargetedType(@event, namedTypeSymbol);
				}
				return @event;
			}

			// Token: 0x060091C2 RID: 37314 RVA: 0x001FE963 File Offset: 0x001FCB63
			private MethodSymbol FindMethodInRetargetedType(MethodSymbol method, NamedTypeSymbol retargetedType, IEqualityComparer<MethodSymbol> retargetedMethodComparer)
			{
				return RetargetingModuleSymbol.RetargetingSymbolTranslator.RetargetedTypeMethodFinder.Find(this, method, retargetedType, retargetedMethodComparer);
			}

			// Token: 0x060091C3 RID: 37315 RVA: 0x001FE970 File Offset: 0x001FCB70
			private PropertySymbol FindPropertyInRetargetedType(PropertySymbol property, NamedTypeSymbol retargetedType, IEqualityComparer<PropertySymbol> retargetedPropertyComparer)
			{
				ArrayBuilder<ParameterSymbol> instance = ArrayBuilder<ParameterSymbol>.GetInstance(property.Parameters.Length);
				bool flag;
				foreach (ParameterSymbol parameterSymbol in property.Parameters)
				{
					instance.Add(new SignatureOnlyParameterSymbol(this.Retarget(parameterSymbol.TypeWithAnnotations, RetargetOptions.RetargetPrimitiveTypesByTypeCode, null), this.RetargetModifiers(parameterSymbol.RefCustomModifiers, out flag), parameterSymbol.IsParams, parameterSymbol.RefKind));
				}
				SignatureOnlyPropertySymbol signatureOnlyPropertySymbol = new SignatureOnlyPropertySymbol(property.Name, retargetedType, instance.ToImmutableAndFree(), property.RefKind, this.Retarget(property.TypeWithAnnotations, RetargetOptions.RetargetPrimitiveTypesByTypeCode, null), this.RetargetModifiers(property.RefCustomModifiers, out flag), property.IsStatic, ImmutableArray<PropertySymbol>.Empty);
				foreach (Symbol symbol in retargetedType.GetMembers(property.Name))
				{
					if (symbol.Kind == SymbolKind.Property)
					{
						PropertySymbol propertySymbol = (PropertySymbol)symbol;
						if (retargetedPropertyComparer.Equals(propertySymbol, signatureOnlyPropertySymbol))
						{
							return propertySymbol;
						}
					}
				}
				return null;
			}

			// Token: 0x060091C4 RID: 37316 RVA: 0x001FEA7C File Offset: 0x001FCC7C
			private EventSymbol FindEventInRetargetedType(EventSymbol @event, NamedTypeSymbol retargetedType)
			{
				TypeWithAnnotations typeWithAnnotations = this.Retarget(@event.TypeWithAnnotations, RetargetOptions.RetargetPrimitiveTypesByTypeCode, null);
				foreach (Symbol symbol in retargetedType.GetMembers(@event.Name))
				{
					if (symbol.Kind == SymbolKind.Event)
					{
						EventSymbol eventSymbol = (EventSymbol)symbol;
						if (TypeSymbol.Equals(eventSymbol.Type, typeWithAnnotations.Type, TypeCompareKind.ConsiderEverything))
						{
							return eventSymbol;
						}
					}
				}
				return null;
			}

			// Token: 0x060091C5 RID: 37317 RVA: 0x001FEAEC File Offset: 0x001FCCEC
			internal ImmutableArray<CustomModifier> RetargetModifiers(ImmutableArray<CustomModifier> oldModifiers, ref ImmutableArray<CustomModifier> lazyCustomModifiers)
			{
				if (lazyCustomModifiers.IsDefault)
				{
					bool flag;
					ImmutableArray<CustomModifier> immutableArray = this.RetargetModifiers(oldModifiers, out flag);
					ImmutableInterlocked.InterlockedCompareExchange<CustomModifier>(ref lazyCustomModifiers, immutableArray, default(ImmutableArray<CustomModifier>));
				}
				return lazyCustomModifiers;
			}

			// Token: 0x060091C6 RID: 37318 RVA: 0x001FEB22 File Offset: 0x001FCD22
			private ImmutableArray<CSharpAttributeData> RetargetAttributes(ImmutableArray<CSharpAttributeData> oldAttributes)
			{
				return oldAttributes.SelectAsArray((CSharpAttributeData a, RetargetingModuleSymbol.RetargetingSymbolTranslator t) => t.RetargetAttributeData(a), this);
			}

			// Token: 0x060091C7 RID: 37319 RVA: 0x001FEB4A File Offset: 0x001FCD4A
			internal IEnumerable<CSharpAttributeData> RetargetAttributes(IEnumerable<CSharpAttributeData> attributes)
			{
				foreach (CSharpAttributeData csharpAttributeData in attributes)
				{
					yield return this.RetargetAttributeData(csharpAttributeData);
				}
				IEnumerator<CSharpAttributeData> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x060091C8 RID: 37320 RVA: 0x001FEB64 File Offset: 0x001FCD64
			private CSharpAttributeData RetargetAttributeData(CSharpAttributeData oldAttributeData)
			{
				SourceAttributeData sourceAttributeData = (SourceAttributeData)oldAttributeData;
				MethodSymbol attributeConstructor = sourceAttributeData.AttributeConstructor;
				MethodSymbol methodSymbol = ((attributeConstructor == null) ? null : this.Retarget(attributeConstructor, MemberSignatureComparer.RetargetedExplicitImplementationComparer));
				NamedTypeSymbol attributeClass = sourceAttributeData.AttributeClass;
				NamedTypeSymbol namedTypeSymbol;
				if (methodSymbol != null)
				{
					namedTypeSymbol = methodSymbol.ContainingType;
				}
				else if (attributeClass != null)
				{
					namedTypeSymbol = this.Retarget(attributeClass, RetargetOptions.RetargetPrimitiveTypesByTypeCode);
				}
				else
				{
					namedTypeSymbol = null;
				}
				ImmutableArray<TypedConstant> commonConstructorArguments = sourceAttributeData.CommonConstructorArguments;
				ImmutableArray<TypedConstant> immutableArray = this.RetargetAttributeConstructorArguments(commonConstructorArguments);
				ImmutableArray<KeyValuePair<string, TypedConstant>> commonNamedArguments = sourceAttributeData.CommonNamedArguments;
				ImmutableArray<KeyValuePair<string, TypedConstant>> immutableArray2 = this.RetargetAttributeNamedArguments(commonNamedArguments);
				return new RetargetingAttributeData(sourceAttributeData.ApplicationSyntaxReference, namedTypeSymbol, methodSymbol, immutableArray, sourceAttributeData.ConstructorArgumentsSourceIndices, immutableArray2, sourceAttributeData.HasErrors, sourceAttributeData.IsConditionallyOmitted);
			}

			// Token: 0x060091C9 RID: 37321 RVA: 0x001FEC00 File Offset: 0x001FCE00
			private ImmutableArray<TypedConstant> RetargetAttributeConstructorArguments(ImmutableArray<TypedConstant> constructorArguments)
			{
				ImmutableArray<TypedConstant> immutableArray = constructorArguments;
				bool flag = false;
				if (!constructorArguments.IsDefault && constructorArguments.Any<TypedConstant>())
				{
					ArrayBuilder<TypedConstant> instance = ArrayBuilder<TypedConstant>.GetInstance(constructorArguments.Length);
					foreach (TypedConstant typedConstant in constructorArguments)
					{
						TypedConstant typedConstant2 = this.RetargetTypedConstant(typedConstant, ref flag);
						instance.Add(typedConstant2);
					}
					if (flag)
					{
						immutableArray = instance.ToImmutable();
					}
					instance.Free();
				}
				return immutableArray;
			}

			// Token: 0x060091CA RID: 37322 RVA: 0x001FEC70 File Offset: 0x001FCE70
			private TypedConstant RetargetTypedConstant(TypedConstant oldConstant, ref bool typedConstantChanged)
			{
				TypeSymbol typeSymbol = (TypeSymbol)oldConstant.TypeInternal;
				TypeSymbol typeSymbol2 = ((typeSymbol == null) ? null : this.Retarget(typeSymbol, RetargetOptions.RetargetPrimitiveTypesByTypeCode));
				if (oldConstant.Kind == TypedConstantKind.Array)
				{
					ImmutableArray<TypedConstant> immutableArray = this.RetargetAttributeConstructorArguments(oldConstant.Values);
					if (!TypeSymbol.Equals(typeSymbol2, typeSymbol, TypeCompareKind.ConsiderEverything) || immutableArray != oldConstant.Values)
					{
						typedConstantChanged = true;
						return new TypedConstant(typeSymbol2, immutableArray);
					}
					return oldConstant;
				}
				else
				{
					object valueInternal = oldConstant.ValueInternal;
					object obj;
					if (oldConstant.Kind == TypedConstantKind.Type && valueInternal != null)
					{
						obj = this.Retarget((TypeSymbol)valueInternal, RetargetOptions.RetargetPrimitiveTypesByTypeCode);
					}
					else
					{
						obj = valueInternal;
					}
					if (!TypeSymbol.Equals(typeSymbol2, typeSymbol, TypeCompareKind.ConsiderEverything) || obj != valueInternal)
					{
						typedConstantChanged = true;
						return new TypedConstant(typeSymbol2, oldConstant.Kind, obj);
					}
					return oldConstant;
				}
			}

			// Token: 0x060091CB RID: 37323 RVA: 0x001FED24 File Offset: 0x001FCF24
			private ImmutableArray<KeyValuePair<string, TypedConstant>> RetargetAttributeNamedArguments(ImmutableArray<KeyValuePair<string, TypedConstant>> namedArguments)
			{
				ImmutableArray<KeyValuePair<string, TypedConstant>> immutableArray = namedArguments;
				bool flag = false;
				if (namedArguments.Any<KeyValuePair<string, TypedConstant>>())
				{
					ArrayBuilder<KeyValuePair<string, TypedConstant>> instance = ArrayBuilder<KeyValuePair<string, TypedConstant>>.GetInstance(namedArguments.Length);
					foreach (KeyValuePair<string, TypedConstant> keyValuePair in namedArguments)
					{
						TypedConstant value = keyValuePair.Value;
						bool flag2 = false;
						TypedConstant typedConstant = this.RetargetTypedConstant(value, ref flag2);
						if (flag2)
						{
							instance.Add(new KeyValuePair<string, TypedConstant>(keyValuePair.Key, typedConstant));
							flag = true;
						}
						else
						{
							instance.Add(keyValuePair);
						}
					}
					if (flag)
					{
						immutableArray = instance.ToImmutable();
					}
					instance.Free();
				}
				return immutableArray;
			}

			// Token: 0x060091CC RID: 37324 RVA: 0x001FEDB4 File Offset: 0x001FCFB4
			internal ImmutableArray<CSharpAttributeData> GetRetargetedAttributes(ImmutableArray<CSharpAttributeData> underlyingAttributes, ref ImmutableArray<CSharpAttributeData> lazyCustomAttributes)
			{
				if (lazyCustomAttributes.IsDefault)
				{
					ImmutableArray<CSharpAttributeData> immutableArray = this.RetargetAttributes(underlyingAttributes);
					ImmutableInterlocked.InterlockedCompareExchange<CSharpAttributeData>(ref lazyCustomAttributes, immutableArray, default(ImmutableArray<CSharpAttributeData>));
				}
				return lazyCustomAttributes;
			}

			// Token: 0x060091CD RID: 37325 RVA: 0x001FEDE8 File Offset: 0x001FCFE8
			public override Symbol VisitModule(ModuleSymbol symbol, RetargetOptions options)
			{
				return this._retargetingModule;
			}

			// Token: 0x060091CE RID: 37326 RVA: 0x001FEDF0 File Offset: 0x001FCFF0
			public override Symbol VisitNamespace(NamespaceSymbol symbol, RetargetOptions options)
			{
				return this.Retarget(symbol);
			}

			// Token: 0x060091CF RID: 37327 RVA: 0x001FEDF9 File Offset: 0x001FCFF9
			public override Symbol VisitNamedType(NamedTypeSymbol symbol, RetargetOptions options)
			{
				return this.Retarget(symbol, options);
			}

			// Token: 0x060091D0 RID: 37328 RVA: 0x001FEE03 File Offset: 0x001FD003
			public override Symbol VisitArrayType(ArrayTypeSymbol symbol, RetargetOptions options)
			{
				return this.Retarget(symbol);
			}

			// Token: 0x060091D1 RID: 37329 RVA: 0x001FEE0C File Offset: 0x001FD00C
			public override Symbol VisitPointerType(PointerTypeSymbol symbol, RetargetOptions options)
			{
				return this.Retarget(symbol);
			}

			// Token: 0x060091D2 RID: 37330 RVA: 0x001FEE15 File Offset: 0x001FD015
			public override Symbol VisitFunctionPointerType(FunctionPointerTypeSymbol symbol, RetargetOptions argument)
			{
				return this.Retarget(symbol);
			}

			// Token: 0x060091D3 RID: 37331 RVA: 0x001FEE1E File Offset: 0x001FD01E
			public override Symbol VisitMethod(MethodSymbol symbol, RetargetOptions options)
			{
				return this.Retarget(symbol);
			}

			// Token: 0x060091D4 RID: 37332 RVA: 0x001FEE27 File Offset: 0x001FD027
			public override Symbol VisitParameter(ParameterSymbol symbol, RetargetOptions options)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x060091D5 RID: 37333 RVA: 0x001FEE2E File Offset: 0x001FD02E
			public override Symbol VisitField(FieldSymbol symbol, RetargetOptions options)
			{
				return this.Retarget(symbol);
			}

			// Token: 0x060091D6 RID: 37334 RVA: 0x001FEE37 File Offset: 0x001FD037
			public override Symbol VisitProperty(PropertySymbol symbol, RetargetOptions argument)
			{
				return this.Retarget(symbol);
			}

			// Token: 0x060091D7 RID: 37335 RVA: 0x001FEE40 File Offset: 0x001FD040
			public override Symbol VisitTypeParameter(TypeParameterSymbol symbol, RetargetOptions options)
			{
				return this.Retarget(symbol);
			}

			// Token: 0x060091D8 RID: 37336 RVA: 0x001FEE49 File Offset: 0x001FD049
			public override Symbol VisitErrorType(ErrorTypeSymbol symbol, RetargetOptions options)
			{
				return RetargetingModuleSymbol.RetargetingSymbolTranslator.Retarget(symbol);
			}

			// Token: 0x060091D9 RID: 37337 RVA: 0x001FEE51 File Offset: 0x001FD051
			public override Symbol VisitEvent(EventSymbol symbol, RetargetOptions options)
			{
				return this.Retarget(symbol);
			}

			// Token: 0x060091DA RID: 37338 RVA: 0x001FEE5A File Offset: 0x001FD05A
			public override Symbol VisitDynamicType(DynamicTypeSymbol symbol, RetargetOptions argument)
			{
				return symbol;
			}

			// Token: 0x040028AD RID: 10413
			private readonly RetargetingModuleSymbol _retargetingModule;

			// Token: 0x02000A0C RID: 2572
			private class RetargetedTypeMethodFinder : RetargetingModuleSymbol.RetargetingSymbolTranslator
			{
				// Token: 0x0600940F RID: 37903 RVA: 0x00203AF5 File Offset: 0x00201CF5
				private RetargetedTypeMethodFinder(RetargetingModuleSymbol retargetingModule)
					: base(retargetingModule)
				{
				}

				// Token: 0x06009410 RID: 37904 RVA: 0x00203AFE File Offset: 0x00201CFE
				public static MethodSymbol Find(RetargetingModuleSymbol.RetargetingSymbolTranslator translator, MethodSymbol method, NamedTypeSymbol retargetedType, IEqualityComparer<MethodSymbol> retargetedMethodComparer)
				{
					if (!method.IsGenericMethod)
					{
						return RetargetingModuleSymbol.RetargetingSymbolTranslator.RetargetedTypeMethodFinder.FindWorker(translator, method, retargetedType, retargetedMethodComparer);
					}
					return RetargetingModuleSymbol.RetargetingSymbolTranslator.RetargetedTypeMethodFinder.FindWorker(new RetargetingModuleSymbol.RetargetingSymbolTranslator.RetargetedTypeMethodFinder(translator._retargetingModule), method, retargetedType, retargetedMethodComparer);
				}

				// Token: 0x06009411 RID: 37905 RVA: 0x00203B28 File Offset: 0x00201D28
				private static MethodSymbol FindWorker(RetargetingModuleSymbol.RetargetingSymbolTranslator translator, MethodSymbol method, NamedTypeSymbol retargetedType, IEqualityComparer<MethodSymbol> retargetedMethodComparer)
				{
					ArrayBuilder<ParameterSymbol> instance = ArrayBuilder<ParameterSymbol>.GetInstance(method.Parameters.Length);
					bool flag;
					foreach (ParameterSymbol parameterSymbol in method.Parameters)
					{
						instance.Add(new SignatureOnlyParameterSymbol(translator.Retarget(parameterSymbol.TypeWithAnnotations, RetargetOptions.RetargetPrimitiveTypesByTypeCode, null), translator.RetargetModifiers(parameterSymbol.RefCustomModifiers, out flag), parameterSymbol.IsParams, parameterSymbol.RefKind));
					}
					SignatureOnlyMethodSymbol signatureOnlyMethodSymbol = new SignatureOnlyMethodSymbol(method.Name, retargetedType, method.MethodKind, method.CallingConvention, IndexedTypeParameterSymbol.TakeSymbols(method.Arity), instance.ToImmutableAndFree(), method.RefKind, method.IsInitOnly, translator.Retarget(method.ReturnTypeWithAnnotations, RetargetOptions.RetargetPrimitiveTypesByTypeCode, null), translator.RetargetModifiers(method.RefCustomModifiers, out flag), ImmutableArray<MethodSymbol>.Empty);
					foreach (Symbol symbol in retargetedType.GetMembers(method.Name))
					{
						if (symbol.Kind == SymbolKind.Method)
						{
							MethodSymbol methodSymbol = (MethodSymbol)symbol;
							if (retargetedMethodComparer.Equals(methodSymbol, signatureOnlyMethodSymbol))
							{
								return methodSymbol;
							}
						}
					}
					return null;
				}

				// Token: 0x06009412 RID: 37906 RVA: 0x00203C49 File Offset: 0x00201E49
				public override TypeParameterSymbol Retarget(TypeParameterSymbol typeParameter)
				{
					if (typeParameter.ContainingModule == base.UnderlyingModule)
					{
						return base.Retarget(typeParameter);
					}
					return IndexedTypeParameterSymbol.GetTypeParameter(typeParameter.Ordinal);
				}
			}
		}
	}
}
