using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Security.Cryptography;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.Emit.NoPia;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Emit
{
	// Token: 0x02000479 RID: 1145
	internal abstract class CommonPEModuleBuilder : IUnit, IUnitReference, IReference, INamedEntity, IDefinition, IModuleReference
	{
		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x06002C8D RID: 11405 RVA: 0x0007DC9E File Offset: 0x0007BE9E
		// (set) Token: 0x06002C8E RID: 11406 RVA: 0x0007DCA6 File Offset: 0x0007BEA6
		internal ConcurrentDictionary<IMethodSymbolInternal, CompilationTestData.MethodData> TestData { get; private set; }

		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x06002C8F RID: 11407 RVA: 0x0007DCAF File Offset: 0x0007BEAF
		internal EmitOptions EmitOptions { get; }

		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x06002C90 RID: 11408 RVA: 0x0007DCB7 File Offset: 0x0007BEB7
		internal DebugInformationFormat DebugInformationFormat
		{
			get
			{
				return this.EmitOptions.DebugInformationFormat;
			}
		}

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x06002C91 RID: 11409 RVA: 0x0007DCC4 File Offset: 0x0007BEC4
		internal HashAlgorithmName PdbChecksumAlgorithm
		{
			get
			{
				return this.EmitOptions.PdbChecksumAlgorithm;
			}
		}

		// Token: 0x06002C92 RID: 11410 RVA: 0x0007DCD4 File Offset: 0x0007BED4
		public CommonPEModuleBuilder(IEnumerable<ResourceDescription> manifestResources, EmitOptions emitOptions, OutputKind outputKind, ModulePropertiesForSerialization serializationProperties, Compilation compilation)
		{
			this.ManifestResources = manifestResources;
			this.DebugDocumentsBuilder = new DebugDocumentsBuilder(compilation.Options.SourceReferenceResolver, compilation.IsCaseSensitive);
			this.OutputKind = outputKind;
			this.SerializationProperties = serializationProperties;
			this._methodBodyMap = new ConcurrentDictionary<IMethodSymbolInternal, IMethodBody>(ReferenceEqualityComparer.Instance);
			this.EmitOptions = emitOptions;
		}

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x06002C93 RID: 11411
		public abstract int CurrentGenerationOrdinal { get; }

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x06002C94 RID: 11412
		public abstract string Name { get; }

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x06002C95 RID: 11413
		internal abstract string ModuleName { get; }

		// Token: 0x06002C96 RID: 11414
		internal abstract IAssemblyReference Translate(IAssemblySymbolInternal symbol, DiagnosticBag diagnostics);

		// Token: 0x06002C97 RID: 11415
		internal abstract ITypeReference Translate(ITypeSymbolInternal symbol, SyntaxNode syntaxOpt, DiagnosticBag diagnostics);

		// Token: 0x06002C98 RID: 11416
		internal abstract IMethodReference Translate(IMethodSymbolInternal symbol, DiagnosticBag diagnostics, bool needDeclaration);

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x06002C99 RID: 11417
		internal abstract bool SupportsPrivateImplClass { get; }

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x06002C9A RID: 11418
		internal abstract Compilation CommonCompilation { get; }

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x06002C9B RID: 11419
		internal abstract IModuleSymbolInternal CommonSourceModule { get; }

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x06002C9C RID: 11420
		internal abstract IAssemblySymbolInternal CommonCorLibrary { get; }

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x06002C9D RID: 11421
		internal abstract CommonModuleCompilationState CommonModuleCompilationState { get; }

		// Token: 0x06002C9E RID: 11422
		internal abstract void CompilationFinished();

		// Token: 0x06002C9F RID: 11423
		internal abstract ImmutableDictionary<ISymbolInternal, ImmutableArray<ISymbolInternal>> GetAllSynthesizedMembers();

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x06002CA0 RID: 11424
		internal abstract CommonEmbeddedTypesManager CommonEmbeddedTypesManagerOpt { get; }

		// Token: 0x06002CA1 RID: 11425
		internal abstract ITypeReference EncTranslateType(ITypeSymbolInternal type, DiagnosticBag diagnostics);

		// Token: 0x06002CA2 RID: 11426
		public abstract IEnumerable<ICustomAttribute> GetSourceAssemblyAttributes(bool isRefAssembly);

		// Token: 0x06002CA3 RID: 11427
		public abstract IEnumerable<SecurityAttribute> GetSourceAssemblySecurityAttributes();

		// Token: 0x06002CA4 RID: 11428
		public abstract IEnumerable<ICustomAttribute> GetSourceModuleAttributes();

		// Token: 0x06002CA5 RID: 11429
		internal abstract ICustomAttribute SynthesizeAttribute(WellKnownMember attributeConstructor);

		// Token: 0x06002CA6 RID: 11430
		public abstract ImmutableArray<ExportedType> GetExportedTypes(DiagnosticBag diagnostics);

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x06002CA7 RID: 11431
		public abstract bool GenerateVisualBasicStylePdb { get; }

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x06002CA8 RID: 11432
		public abstract IEnumerable<string> LinkedAssembliesDebugInfo { get; }

		// Token: 0x06002CA9 RID: 11433
		public abstract ImmutableArray<UsedNamespaceOrType> GetImports();

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x06002CAA RID: 11434
		public abstract string DefaultNamespace { get; }

		// Token: 0x06002CAB RID: 11435
		protected abstract IAssemblyReference GetCorLibraryReferenceToEmit(EmitContext context);

		// Token: 0x06002CAC RID: 11436
		protected abstract IEnumerable<IAssemblyReference> GetAssemblyReferencesFromAddedModules(DiagnosticBag diagnostics);

		// Token: 0x06002CAD RID: 11437
		protected abstract void AddEmbeddedResourcesFromAddedModules(ArrayBuilder<ManagedResource> builder, DiagnosticBag diagnostics);

		// Token: 0x06002CAE RID: 11438
		public abstract ITypeReference GetPlatformType(PlatformType platformType, EmitContext context);

		// Token: 0x06002CAF RID: 11439
		public abstract bool IsPlatformType(ITypeReference typeRef, PlatformType platformType);

		// Token: 0x06002CB0 RID: 11440
		public abstract IEnumerable<INamespaceTypeDefinition> GetTopLevelTypeDefinitions(EmitContext context);

		// Token: 0x06002CB1 RID: 11441 RVA: 0x0007DD5E File Offset: 0x0007BF5E
		public IEnumerable<INamespaceTypeDefinition> GetTopLevelTypeDefinitionsCore(EmitContext context)
		{
			foreach (INamespaceTypeDefinition namespaceTypeDefinition in this.GetAdditionalTopLevelTypeDefinitions(context))
			{
				yield return namespaceTypeDefinition;
			}
			IEnumerator<INamespaceTypeDefinition> enumerator = null;
			foreach (INamespaceTypeDefinition namespaceTypeDefinition2 in this.GetEmbeddedTypeDefinitions(context))
			{
				yield return namespaceTypeDefinition2;
			}
			enumerator = null;
			foreach (INamespaceTypeDefinition namespaceTypeDefinition3 in this.GetTopLevelSourceTypeDefinitions(context))
			{
				yield return namespaceTypeDefinition3;
			}
			enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06002CB2 RID: 11442
		public abstract IEnumerable<INamespaceTypeDefinition> GetAdditionalTopLevelTypeDefinitions(EmitContext context);

		// Token: 0x06002CB3 RID: 11443
		public abstract IEnumerable<INamespaceTypeDefinition> GetAnonymousTypeDefinitions(EmitContext context);

		// Token: 0x06002CB4 RID: 11444
		public abstract IEnumerable<INamespaceTypeDefinition> GetEmbeddedTypeDefinitions(EmitContext context);

		// Token: 0x06002CB5 RID: 11445
		public abstract IEnumerable<INamespaceTypeDefinition> GetTopLevelSourceTypeDefinitions(EmitContext context);

		// Token: 0x06002CB6 RID: 11446
		public abstract IEnumerable<IFileReference> GetFiles(EmitContext context);

		// Token: 0x06002CB7 RID: 11447
		public abstract MultiDictionary<DebugSourceDocument, DefinitionWithLocation> GetSymbolToLocationMap();

		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x06002CB8 RID: 11448 RVA: 0x0007DD75 File Offset: 0x0007BF75
		public int DebugDocumentCount
		{
			get
			{
				return this.DebugDocumentsBuilder.DebugDocumentCount;
			}
		}

		// Token: 0x06002CB9 RID: 11449 RVA: 0x0007DD82 File Offset: 0x0007BF82
		public void Dispatch(MetadataVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06002CBA RID: 11450 RVA: 0x0007DD8B File Offset: 0x0007BF8B
		IEnumerable<ICustomAttribute> IReference.GetAttributes(EmitContext context)
		{
			return SpecializedCollections.EmptyEnumerable<ICustomAttribute>();
		}

		// Token: 0x06002CBB RID: 11451 RVA: 0x0007DD92 File Offset: 0x0007BF92
		IDefinition IReference.AsDefinition(EmitContext context)
		{
			return this;
		}

		// Token: 0x06002CBC RID: 11452 RVA: 0x0007DD95 File Offset: 0x0007BF95
		ISymbolInternal IReference.GetInternalSymbol()
		{
			return null;
		}

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x06002CBD RID: 11453
		public abstract ISourceAssemblySymbolInternal SourceAssemblyOpt { get; }

		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x06002CBE RID: 11454 RVA: 0x0007DD98 File Offset: 0x0007BF98
		public int HintNumberOfMethodDefinitions
		{
			get
			{
				return (int)((double)this._methodBodyMap.Count * 1.5);
			}
		}

		// Token: 0x06002CBF RID: 11455 RVA: 0x0007DDB4 File Offset: 0x0007BFB4
		internal IMethodBody GetMethodBody(IMethodSymbolInternal methodSymbol)
		{
			IMethodBody methodBody;
			if (this._methodBodyMap.TryGetValue(methodSymbol, ref methodBody))
			{
				return methodBody;
			}
			return null;
		}

		// Token: 0x06002CC0 RID: 11456 RVA: 0x0007DDD4 File Offset: 0x0007BFD4
		public void SetMethodBody(IMethodSymbolInternal methodSymbol, IMethodBody body)
		{
			this._methodBodyMap.Add(methodSymbol, body);
		}

		// Token: 0x06002CC1 RID: 11457 RVA: 0x0007DDE3 File Offset: 0x0007BFE3
		internal void SetPEEntryPoint(IMethodSymbolInternal method, DiagnosticBag diagnostics)
		{
			this.PEEntryPoint = this.Translate(method, diagnostics, true);
		}

		// Token: 0x06002CC2 RID: 11458 RVA: 0x0007DDF4 File Offset: 0x0007BFF4
		internal void SetDebugEntryPoint(IMethodSymbolInternal method, DiagnosticBag diagnostics)
		{
			this.DebugEntryPoint = this.Translate(method, diagnostics, true);
		}

		// Token: 0x06002CC3 RID: 11459 RVA: 0x0007DE05 File Offset: 0x0007C005
		private bool IsSourceDefinition(IMethodSymbolInternal method)
		{
			return method.ContainingModule == this.CommonSourceModule && method.IsDefinition;
		}

		// Token: 0x06002CC4 RID: 11460 RVA: 0x0007DE1D File Offset: 0x0007C01D
		public IAssemblyReference GetCorLibrary(EmitContext context)
		{
			return this.Translate(this.CommonCorLibrary, context.Diagnostics);
		}

		// Token: 0x06002CC5 RID: 11461 RVA: 0x0007DE31 File Offset: 0x0007C031
		public IAssemblyReference GetContainingAssembly(EmitContext context)
		{
			if (this.OutputKind != OutputKind.NetModule)
			{
				return (IAssemblyReference)this;
			}
			return null;
		}

		// Token: 0x06002CC6 RID: 11462 RVA: 0x0007DE44 File Offset: 0x0007C044
		public IEnumerable<string> GetStrings()
		{
			return this._stringsInILMap.GetAllItems();
		}

		// Token: 0x06002CC7 RID: 11463 RVA: 0x0007DE54 File Offset: 0x0007C054
		public uint GetFakeSymbolTokenForIL(IReference symbol, SyntaxNode syntaxNode, DiagnosticBag diagnostics)
		{
			bool flag;
			uint orAddTokenFor = this._referencesInILMap.GetOrAddTokenFor(symbol, out flag);
			if (flag)
			{
				ReferenceDependencyWalker.VisitReference(symbol, new EmitContext(this, syntaxNode, diagnostics, false, true));
			}
			return orAddTokenFor;
		}

		// Token: 0x06002CC8 RID: 11464 RVA: 0x0007DE84 File Offset: 0x0007C084
		public uint GetFakeSymbolTokenForIL(ISignature symbol, SyntaxNode syntaxNode, DiagnosticBag diagnostics)
		{
			bool flag;
			uint orAddTokenFor = this._referencesInILMap.GetOrAddTokenFor(symbol, out flag);
			if (flag)
			{
				ReferenceDependencyWalker.VisitSignature(symbol, new EmitContext(this, syntaxNode, diagnostics, false, true));
			}
			return orAddTokenFor;
		}

		// Token: 0x06002CC9 RID: 11465 RVA: 0x0007DEB2 File Offset: 0x0007C0B2
		public uint GetSourceDocumentIndexForIL(DebugSourceDocument document)
		{
			return this._sourceDocumentsInILMap.GetOrAddTokenFor(document);
		}

		// Token: 0x06002CCA RID: 11466 RVA: 0x0007DEC0 File Offset: 0x0007C0C0
		internal DebugSourceDocument GetSourceDocumentFromIndex(uint token)
		{
			return this._sourceDocumentsInILMap.GetItem(token);
		}

		// Token: 0x06002CCB RID: 11467 RVA: 0x0007DECE File Offset: 0x0007C0CE
		public object GetReferenceFromToken(uint token)
		{
			return this._referencesInILMap.GetItem(token);
		}

		// Token: 0x06002CCC RID: 11468 RVA: 0x0007DEDC File Offset: 0x0007C0DC
		public uint GetFakeStringTokenForIL(string str)
		{
			return this._stringsInILMap.GetOrAddTokenFor(str);
		}

		// Token: 0x06002CCD RID: 11469 RVA: 0x0007DEEA File Offset: 0x0007C0EA
		public string GetStringFromToken(uint token)
		{
			return this._stringsInILMap.GetItem(token);
		}

		// Token: 0x06002CCE RID: 11470 RVA: 0x0007DEF8 File Offset: 0x0007C0F8
		public ReadOnlySpan<object> ReferencesInIL()
		{
			return this._referencesInILMap.GetAllItems();
		}

		// Token: 0x06002CCF RID: 11471 RVA: 0x0007DF08 File Offset: 0x0007C108
		public ImmutableArray<AssemblyReferenceAlias> GetAssemblyReferenceAliases(EmitContext context)
		{
			if (this._lazyAssemblyReferenceAliases.IsDefault)
			{
				ImmutableInterlocked.InterlockedCompareExchange<AssemblyReferenceAlias>(ref this._lazyAssemblyReferenceAliases, this.CalculateAssemblyReferenceAliases(context), default(ImmutableArray<AssemblyReferenceAlias>));
			}
			return this._lazyAssemblyReferenceAliases;
		}

		// Token: 0x06002CD0 RID: 11472 RVA: 0x0007DF44 File Offset: 0x0007C144
		private ImmutableArray<AssemblyReferenceAlias> CalculateAssemblyReferenceAliases(EmitContext context)
		{
			ArrayBuilder<AssemblyReferenceAlias> instance = ArrayBuilder<AssemblyReferenceAlias>.GetInstance();
			foreach (ValueTuple<IAssemblySymbolInternal, ImmutableArray<string>> valueTuple in this.CommonCompilation.GetBoundReferenceManager().GetReferencedAssemblyAliases())
			{
				IAssemblySymbolInternal item = valueTuple.Item1;
				ImmutableArray<string> item2 = valueTuple.Item2;
				for (int i = 0; i < item2.Length; i++)
				{
					string text = item2[i];
					if (text != MetadataReferenceProperties.GlobalAlias && item2.IndexOf(text, 0, i) < 0)
					{
						instance.Add(new AssemblyReferenceAlias(text, this.Translate(item, context.Diagnostics)));
					}
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06002CD1 RID: 11473 RVA: 0x0007E004 File Offset: 0x0007C204
		public IEnumerable<IAssemblyReference> GetAssemblyReferences(EmitContext context)
		{
			IAssemblyReference corLibraryReferenceToEmit = this.GetCorLibraryReferenceToEmit(context);
			if (corLibraryReferenceToEmit != null)
			{
				yield return corLibraryReferenceToEmit;
			}
			if (this.OutputKind != OutputKind.NetModule)
			{
				foreach (IAssemblyReference assemblyReference in this.GetAssemblyReferencesFromAddedModules(context.Diagnostics))
				{
					yield return assemblyReference;
				}
				IEnumerator<IAssemblyReference> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06002CD2 RID: 11474 RVA: 0x0007E01C File Offset: 0x0007C21C
		public ImmutableArray<ManagedResource> GetResources(EmitContext context)
		{
			if (context.IsRefAssembly)
			{
				return ImmutableArray<ManagedResource>.Empty;
			}
			if (this._lazyManagedResources.IsDefault)
			{
				ArrayBuilder<ManagedResource> instance = ArrayBuilder<ManagedResource>.GetInstance();
				foreach (ResourceDescription resourceDescription in this.ManifestResources)
				{
					instance.Add(resourceDescription.ToManagedResource(this));
				}
				if (this.OutputKind != OutputKind.NetModule)
				{
					this.AddEmbeddedResourcesFromAddedModules(instance, context.Diagnostics);
				}
				this._lazyManagedResources = instance.ToImmutableAndFree();
			}
			return this._lazyManagedResources;
		}

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x06002CD3 RID: 11475 RVA: 0x0007E0BC File Offset: 0x0007C2BC
		// (set) Token: 0x06002CD4 RID: 11476 RVA: 0x0007E0C4 File Offset: 0x0007C2C4
		public IEnumerable<EmbeddedText> EmbeddedTexts
		{
			get
			{
				return this._embeddedTexts;
			}
			set
			{
				this._embeddedTexts = value;
			}
		}

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x06002CD5 RID: 11477 RVA: 0x0007E0CD File Offset: 0x0007C2CD
		internal bool SaveTestData
		{
			get
			{
				return this.TestData != null;
			}
		}

		// Token: 0x06002CD6 RID: 11478 RVA: 0x0007E0D8 File Offset: 0x0007C2D8
		internal void SetMethodTestData(IMethodSymbolInternal method, ILBuilder builder)
		{
			this.TestData.Add(method, new CompilationTestData.MethodData(builder, method));
		}

		// Token: 0x06002CD7 RID: 11479 RVA: 0x0007E0ED File Offset: 0x0007C2ED
		internal void SetMethodTestData(ConcurrentDictionary<IMethodSymbolInternal, CompilationTestData.MethodData> methods)
		{
			this.TestData = methods;
		}

		// Token: 0x040011BB RID: 4539
		internal readonly DebugDocumentsBuilder DebugDocumentsBuilder;

		// Token: 0x040011BC RID: 4540
		internal readonly IEnumerable<ResourceDescription> ManifestResources;

		// Token: 0x040011BD RID: 4541
		internal readonly ModulePropertiesForSerialization SerializationProperties;

		// Token: 0x040011BE RID: 4542
		internal readonly OutputKind OutputKind;

		// Token: 0x040011BF RID: 4543
		internal Stream RawWin32Resources;

		// Token: 0x040011C0 RID: 4544
		internal IEnumerable<IWin32Resource> Win32Resources;

		// Token: 0x040011C1 RID: 4545
		internal ResourceSection Win32ResourceSection;

		// Token: 0x040011C2 RID: 4546
		internal Stream SourceLinkStreamOpt;

		// Token: 0x040011C3 RID: 4547
		internal IMethodReference PEEntryPoint;

		// Token: 0x040011C4 RID: 4548
		internal IMethodReference DebugEntryPoint;

		// Token: 0x040011C5 RID: 4549
		private readonly ConcurrentDictionary<IMethodSymbolInternal, IMethodBody> _methodBodyMap;

		// Token: 0x040011C6 RID: 4550
		private readonly TokenMap _referencesInILMap = new TokenMap();

		// Token: 0x040011C7 RID: 4551
		private readonly ItemTokenMap<string> _stringsInILMap = new ItemTokenMap<string>();

		// Token: 0x040011C8 RID: 4552
		private readonly ItemTokenMap<DebugSourceDocument> _sourceDocumentsInILMap = new ItemTokenMap<DebugSourceDocument>();

		// Token: 0x040011C9 RID: 4553
		private ImmutableArray<AssemblyReferenceAlias> _lazyAssemblyReferenceAliases;

		// Token: 0x040011CA RID: 4554
		private ImmutableArray<ManagedResource> _lazyManagedResources;

		// Token: 0x040011CB RID: 4555
		private IEnumerable<EmbeddedText> _embeddedTexts = SpecializedCollections.EmptyEnumerable<EmbeddedText>();
	}
}
