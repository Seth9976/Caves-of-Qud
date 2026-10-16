using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.Emit.NoPia;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Emit
{
	// Token: 0x0200047A RID: 1146
	internal abstract class PEModuleBuilder<TCompilation, TSourceModuleSymbol, TAssemblySymbol, TTypeSymbol, TNamedTypeSymbol, TMethodSymbol, TSyntaxNode, TEmbeddedTypesManager, TModuleCompilationState> : CommonPEModuleBuilder, ITokenDeferral where TCompilation : Compilation where TSourceModuleSymbol : class, IModuleSymbolInternal where TAssemblySymbol : class, IAssemblySymbolInternal where TTypeSymbol : class, ITypeSymbolInternal where TNamedTypeSymbol : class, TTypeSymbol, INamedTypeSymbolInternal where TMethodSymbol : class, IMethodSymbolInternal where TSyntaxNode : SyntaxNode where TEmbeddedTypesManager : CommonEmbeddedTypesManager where TModuleCompilationState : ModuleCompilationState<TNamedTypeSymbol, TMethodSymbol>
	{
		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x06002CD8 RID: 11480 RVA: 0x0007E0F6 File Offset: 0x0007C2F6
		public RootModuleType RootModuleType { get; } = new RootModuleType();

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x06002CD9 RID: 11481
		public abstract TEmbeddedTypesManager EmbeddedTypesManagerOpt { get; }

		// Token: 0x06002CDA RID: 11482 RVA: 0x0007E100 File Offset: 0x0007C300
		protected PEModuleBuilder(TCompilation compilation, TSourceModuleSymbol sourceModule, ModulePropertiesForSerialization serializationProperties, IEnumerable<ResourceDescription> manifestResources, OutputKind outputKind, EmitOptions emitOptions, TModuleCompilationState compilationState)
			: base(manifestResources, emitOptions, outputKind, serializationProperties, compilation)
		{
			this.Compilation = compilation;
			this.SourceModule = sourceModule;
			this.CompilationState = compilationState;
		}

		// Token: 0x06002CDB RID: 11483 RVA: 0x0007E151 File Offset: 0x0007C351
		internal sealed override void CompilationFinished()
		{
			this.CompilationState.Freeze();
		}

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x06002CDC RID: 11484 RVA: 0x0007E163 File Offset: 0x0007C363
		internal override IAssemblySymbolInternal CommonCorLibrary
		{
			get
			{
				return this.CorLibrary;
			}
		}

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x06002CDD RID: 11485
		internal abstract TAssemblySymbol CorLibrary { get; }

		// Token: 0x06002CDE RID: 11486
		internal abstract INamedTypeReference GetSpecialType(SpecialType specialType, TSyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics);

		// Token: 0x06002CDF RID: 11487 RVA: 0x0007E170 File Offset: 0x0007C370
		internal sealed override ITypeReference EncTranslateType(ITypeSymbolInternal type, DiagnosticBag diagnostics)
		{
			return this.EncTranslateLocalVariableType((TTypeSymbol)((object)type), diagnostics);
		}

		// Token: 0x06002CE0 RID: 11488 RVA: 0x0007E180 File Offset: 0x0007C380
		internal virtual ITypeReference EncTranslateLocalVariableType(TTypeSymbol type, DiagnosticBag diagnostics)
		{
			return this.Translate(type, default(TSyntaxNode), diagnostics);
		}

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x06002CE1 RID: 11489 RVA: 0x0007E19E File Offset: 0x0007C39E
		protected bool HaveDeterminedTopLevelTypes
		{
			get
			{
				return this._namesOfTopLevelTypes != null;
			}
		}

		// Token: 0x06002CE2 RID: 11490 RVA: 0x0007E1A9 File Offset: 0x0007C3A9
		protected bool ContainsTopLevelType(string fullEmittedName)
		{
			return this._namesOfTopLevelTypes.Contains(fullEmittedName);
		}

		// Token: 0x06002CE3 RID: 11491 RVA: 0x0007E1B7 File Offset: 0x0007C3B7
		public override IEnumerable<INamespaceTypeDefinition> GetTopLevelTypeDefinitions(EmitContext context)
		{
			TypeReferenceIndexer typeReferenceIndexer = null;
			HashSet<string> names;
			if (this._namesOfTopLevelTypes == null)
			{
				names = new HashSet<string>();
			}
			else
			{
				names = null;
			}
			if (this.EmbeddedTypesManagerOpt != null && !this.EmbeddedTypesManagerOpt.IsFrozen)
			{
				typeReferenceIndexer = new TypeReferenceIndexer(context);
				base.Dispatch(typeReferenceIndexer);
			}
			PEModuleBuilder<TCompilation, TSourceModuleSymbol, TAssemblySymbol, TTypeSymbol, TNamedTypeSymbol, TMethodSymbol, TSyntaxNode, TEmbeddedTypesManager, TModuleCompilationState>.AddTopLevelType(names, this.RootModuleType);
			PEModuleBuilder<TCompilation, TSourceModuleSymbol, TAssemblySymbol, TTypeSymbol, TNamedTypeSymbol, TMethodSymbol, TSyntaxNode, TEmbeddedTypesManager, TModuleCompilationState>.VisitTopLevelType(typeReferenceIndexer, this.RootModuleType);
			yield return this.RootModuleType;
			foreach (INamespaceTypeDefinition namespaceTypeDefinition in this.GetAnonymousTypeDefinitions(context))
			{
				PEModuleBuilder<TCompilation, TSourceModuleSymbol, TAssemblySymbol, TTypeSymbol, TNamedTypeSymbol, TMethodSymbol, TSyntaxNode, TEmbeddedTypesManager, TModuleCompilationState>.AddTopLevelType(names, namespaceTypeDefinition);
				PEModuleBuilder<TCompilation, TSourceModuleSymbol, TAssemblySymbol, TTypeSymbol, TNamedTypeSymbol, TMethodSymbol, TSyntaxNode, TEmbeddedTypesManager, TModuleCompilationState>.VisitTopLevelType(typeReferenceIndexer, namespaceTypeDefinition);
				yield return namespaceTypeDefinition;
			}
			IEnumerator<INamespaceTypeDefinition> enumerator = null;
			foreach (INamespaceTypeDefinition namespaceTypeDefinition2 in base.GetTopLevelTypeDefinitionsCore(context))
			{
				PEModuleBuilder<TCompilation, TSourceModuleSymbol, TAssemblySymbol, TTypeSymbol, TNamedTypeSymbol, TMethodSymbol, TSyntaxNode, TEmbeddedTypesManager, TModuleCompilationState>.AddTopLevelType(names, namespaceTypeDefinition2);
				PEModuleBuilder<TCompilation, TSourceModuleSymbol, TAssemblySymbol, TTypeSymbol, TNamedTypeSymbol, TMethodSymbol, TSyntaxNode, TEmbeddedTypesManager, TModuleCompilationState>.VisitTopLevelType(typeReferenceIndexer, namespaceTypeDefinition2);
				yield return namespaceTypeDefinition2;
			}
			enumerator = null;
			PrivateImplementationDetails privateImplClass = this.PrivateImplClass;
			if (privateImplClass != null)
			{
				PEModuleBuilder<TCompilation, TSourceModuleSymbol, TAssemblySymbol, TTypeSymbol, TNamedTypeSymbol, TMethodSymbol, TSyntaxNode, TEmbeddedTypesManager, TModuleCompilationState>.AddTopLevelType(names, privateImplClass);
				PEModuleBuilder<TCompilation, TSourceModuleSymbol, TAssemblySymbol, TTypeSymbol, TNamedTypeSymbol, TMethodSymbol, TSyntaxNode, TEmbeddedTypesManager, TModuleCompilationState>.VisitTopLevelType(typeReferenceIndexer, privateImplClass);
				yield return privateImplClass;
			}
			if (this.EmbeddedTypesManagerOpt != null)
			{
				foreach (INamespaceTypeDefinition namespaceTypeDefinition3 in this.EmbeddedTypesManagerOpt.GetTypes(context.Diagnostics, names))
				{
					PEModuleBuilder<TCompilation, TSourceModuleSymbol, TAssemblySymbol, TTypeSymbol, TNamedTypeSymbol, TMethodSymbol, TSyntaxNode, TEmbeddedTypesManager, TModuleCompilationState>.AddTopLevelType(names, namespaceTypeDefinition3);
					yield return namespaceTypeDefinition3;
				}
				ImmutableArray<INamespaceTypeDefinition>.Enumerator enumerator2 = default(ImmutableArray<INamespaceTypeDefinition>.Enumerator);
			}
			if (names != null)
			{
				this._namesOfTopLevelTypes = names;
			}
			yield break;
			yield break;
		}

		// Token: 0x06002CE4 RID: 11492 RVA: 0x0007E1CE File Offset: 0x0007C3CE
		public virtual ImmutableArray<TNamedTypeSymbol> GetAdditionalTopLevelTypes()
		{
			return ImmutableArray<TNamedTypeSymbol>.Empty;
		}

		// Token: 0x06002CE5 RID: 11493 RVA: 0x0007E1D5 File Offset: 0x0007C3D5
		public virtual ImmutableArray<TNamedTypeSymbol> GetEmbeddedTypes(DiagnosticBag diagnostics)
		{
			return ImmutableArray<TNamedTypeSymbol>.Empty;
		}

		// Token: 0x06002CE6 RID: 11494
		internal abstract IAssemblyReference Translate(TAssemblySymbol symbol, DiagnosticBag diagnostics);

		// Token: 0x06002CE7 RID: 11495
		internal abstract ITypeReference Translate(TTypeSymbol symbol, TSyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics);

		// Token: 0x06002CE8 RID: 11496
		internal abstract IMethodReference Translate(TMethodSymbol symbol, DiagnosticBag diagnostics, bool needDeclaration);

		// Token: 0x06002CE9 RID: 11497 RVA: 0x0007E1DC File Offset: 0x0007C3DC
		internal sealed override IAssemblyReference Translate(IAssemblySymbolInternal symbol, DiagnosticBag diagnostics)
		{
			return this.Translate((TAssemblySymbol)((object)symbol), diagnostics);
		}

		// Token: 0x06002CEA RID: 11498 RVA: 0x0007E1EB File Offset: 0x0007C3EB
		internal sealed override ITypeReference Translate(ITypeSymbolInternal symbol, SyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics)
		{
			return this.Translate((TTypeSymbol)((object)symbol), (TSyntaxNode)((object)syntaxNodeOpt), diagnostics);
		}

		// Token: 0x06002CEB RID: 11499 RVA: 0x0007E200 File Offset: 0x0007C400
		internal sealed override IMethodReference Translate(IMethodSymbolInternal symbol, DiagnosticBag diagnostics, bool needDeclaration)
		{
			return this.Translate((TMethodSymbol)((object)symbol), diagnostics, needDeclaration);
		}

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x06002CEC RID: 11500 RVA: 0x0007E210 File Offset: 0x0007C410
		internal sealed override IModuleSymbolInternal CommonSourceModule
		{
			get
			{
				return this.SourceModule;
			}
		}

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x06002CED RID: 11501 RVA: 0x0007E21D File Offset: 0x0007C41D
		internal sealed override Compilation CommonCompilation
		{
			get
			{
				return this.Compilation;
			}
		}

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06002CEE RID: 11502 RVA: 0x0007E22A File Offset: 0x0007C42A
		internal sealed override CommonModuleCompilationState CommonModuleCompilationState
		{
			get
			{
				return this.CompilationState;
			}
		}

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06002CEF RID: 11503 RVA: 0x0007E237 File Offset: 0x0007C437
		internal sealed override CommonEmbeddedTypesManager CommonEmbeddedTypesManagerOpt
		{
			get
			{
				return this.EmbeddedTypesManagerOpt;
			}
		}

		// Token: 0x06002CF0 RID: 11504 RVA: 0x0007E244 File Offset: 0x0007C444
		internal MetadataConstant CreateConstant(TTypeSymbol type, object value, TSyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics)
		{
			return new MetadataConstant(this.Translate(type, syntaxNodeOpt, diagnostics), value);
		}

		// Token: 0x06002CF1 RID: 11505 RVA: 0x0007E256 File Offset: 0x0007C456
		private static void AddTopLevelType(HashSet<string> names, INamespaceTypeDefinition type)
		{
			if (names != null)
			{
				names.Add(MetadataHelpers.BuildQualifiedName(type.NamespaceName, MetadataWriter.GetMangledName(type)));
			}
		}

		// Token: 0x06002CF2 RID: 11506 RVA: 0x0007E273 File Offset: 0x0007C473
		private static void VisitTopLevelType(TypeReferenceIndexer noPiaIndexer, INamespaceTypeDefinition type)
		{
			if (noPiaIndexer != null)
			{
				noPiaIndexer.Visit(type);
			}
		}

		// Token: 0x06002CF3 RID: 11507 RVA: 0x0007E280 File Offset: 0x0007C480
		internal IFieldReference GetModuleVersionId(ITypeReference mvidType, TSyntaxNode syntaxOpt, DiagnosticBag diagnostics)
		{
			PrivateImplementationDetails privateImplClass = this.GetPrivateImplClass(syntaxOpt, diagnostics);
			this.EnsurePrivateImplementationDetailsStaticConstructor(privateImplClass, syntaxOpt, diagnostics);
			return privateImplClass.GetModuleVersionId(mvidType);
		}

		// Token: 0x06002CF4 RID: 11508 RVA: 0x0007E2A8 File Offset: 0x0007C4A8
		internal IFieldReference GetInstrumentationPayloadRoot(int analysisKind, ITypeReference payloadType, TSyntaxNode syntaxOpt, DiagnosticBag diagnostics)
		{
			PrivateImplementationDetails privateImplClass = this.GetPrivateImplClass(syntaxOpt, diagnostics);
			this.EnsurePrivateImplementationDetailsStaticConstructor(privateImplClass, syntaxOpt, diagnostics);
			return privateImplClass.GetOrAddInstrumentationPayloadRoot(analysisKind, payloadType);
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x0007E2D1 File Offset: 0x0007C4D1
		private void EnsurePrivateImplementationDetailsStaticConstructor(PrivateImplementationDetails details, TSyntaxNode syntaxOpt, DiagnosticBag diagnostics)
		{
			if (details.GetMethod(".cctor") == null)
			{
				details.TryAddSynthesizedMethod(this.CreatePrivateImplementationDetailsStaticConstructor(details, syntaxOpt, diagnostics));
			}
		}

		// Token: 0x06002CF6 RID: 11510
		protected abstract IMethodDefinition CreatePrivateImplementationDetailsStaticConstructor(PrivateImplementationDetails details, TSyntaxNode syntaxOpt, DiagnosticBag diagnostics);

		// Token: 0x06002CF7 RID: 11511
		internal abstract IEnumerable<INestedTypeDefinition> GetSynthesizedNestedTypes(TNamedTypeSymbol container);

		// Token: 0x06002CF8 RID: 11512 RVA: 0x0007E2F0 File Offset: 0x0007C4F0
		public IEnumerable<INestedTypeDefinition> GetSynthesizedTypes(TNamedTypeSymbol container)
		{
			IEnumerable<INestedTypeDefinition> synthesizedNestedTypes = this.GetSynthesizedNestedTypes(container);
			IEnumerable<INestedTypeDefinition> enumerable = null;
			PEModuleBuilder<TCompilation, TSourceModuleSymbol, TAssemblySymbol, TTypeSymbol, TNamedTypeSymbol, TMethodSymbol, TSyntaxNode, TEmbeddedTypesManager, TModuleCompilationState>.SynthesizedDefinitions synthesizedDefinitions;
			if (this._synthesizedTypeMembers.TryGetValue(container, ref synthesizedDefinitions))
			{
				enumerable = synthesizedDefinitions.NestedTypes;
			}
			if (synthesizedNestedTypes == null)
			{
				return enumerable;
			}
			if (enumerable == null)
			{
				return synthesizedNestedTypes;
			}
			return Enumerable.Concat<INestedTypeDefinition>(synthesizedNestedTypes, enumerable);
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x0007E32F File Offset: 0x0007C52F
		private PEModuleBuilder<TCompilation, TSourceModuleSymbol, TAssemblySymbol, TTypeSymbol, TNamedTypeSymbol, TMethodSymbol, TSyntaxNode, TEmbeddedTypesManager, TModuleCompilationState>.SynthesizedDefinitions GetOrAddSynthesizedDefinitions(TNamedTypeSymbol container)
		{
			return this._synthesizedTypeMembers.GetOrAdd(container, (TNamedTypeSymbol _) => new PEModuleBuilder<TCompilation, TSourceModuleSymbol, TAssemblySymbol, TTypeSymbol, TNamedTypeSymbol, TMethodSymbol, TSyntaxNode, TEmbeddedTypesManager, TModuleCompilationState>.SynthesizedDefinitions());
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x0007E35C File Offset: 0x0007C55C
		public void AddSynthesizedDefinition(TNamedTypeSymbol container, IMethodDefinition method)
		{
			PEModuleBuilder<TCompilation, TSourceModuleSymbol, TAssemblySymbol, TTypeSymbol, TNamedTypeSymbol, TMethodSymbol, TSyntaxNode, TEmbeddedTypesManager, TModuleCompilationState>.SynthesizedDefinitions orAddSynthesizedDefinitions = this.GetOrAddSynthesizedDefinitions(container);
			if (orAddSynthesizedDefinitions.Methods == null)
			{
				Interlocked.CompareExchange<ConcurrentQueue<IMethodDefinition>>(ref orAddSynthesizedDefinitions.Methods, new ConcurrentQueue<IMethodDefinition>(), null);
			}
			orAddSynthesizedDefinitions.Methods.Enqueue(method);
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x0007E398 File Offset: 0x0007C598
		public void AddSynthesizedDefinition(TNamedTypeSymbol container, IPropertyDefinition property)
		{
			PEModuleBuilder<TCompilation, TSourceModuleSymbol, TAssemblySymbol, TTypeSymbol, TNamedTypeSymbol, TMethodSymbol, TSyntaxNode, TEmbeddedTypesManager, TModuleCompilationState>.SynthesizedDefinitions orAddSynthesizedDefinitions = this.GetOrAddSynthesizedDefinitions(container);
			if (orAddSynthesizedDefinitions.Properties == null)
			{
				Interlocked.CompareExchange<ConcurrentQueue<IPropertyDefinition>>(ref orAddSynthesizedDefinitions.Properties, new ConcurrentQueue<IPropertyDefinition>(), null);
			}
			orAddSynthesizedDefinitions.Properties.Enqueue(property);
		}

		// Token: 0x06002CFC RID: 11516 RVA: 0x0007E3D4 File Offset: 0x0007C5D4
		public void AddSynthesizedDefinition(TNamedTypeSymbol container, IFieldDefinition field)
		{
			PEModuleBuilder<TCompilation, TSourceModuleSymbol, TAssemblySymbol, TTypeSymbol, TNamedTypeSymbol, TMethodSymbol, TSyntaxNode, TEmbeddedTypesManager, TModuleCompilationState>.SynthesizedDefinitions orAddSynthesizedDefinitions = this.GetOrAddSynthesizedDefinitions(container);
			if (orAddSynthesizedDefinitions.Fields == null)
			{
				Interlocked.CompareExchange<ConcurrentQueue<IFieldDefinition>>(ref orAddSynthesizedDefinitions.Fields, new ConcurrentQueue<IFieldDefinition>(), null);
			}
			orAddSynthesizedDefinitions.Fields.Enqueue(field);
		}

		// Token: 0x06002CFD RID: 11517 RVA: 0x0007E410 File Offset: 0x0007C610
		public void AddSynthesizedDefinition(TNamedTypeSymbol container, INestedTypeDefinition nestedType)
		{
			PEModuleBuilder<TCompilation, TSourceModuleSymbol, TAssemblySymbol, TTypeSymbol, TNamedTypeSymbol, TMethodSymbol, TSyntaxNode, TEmbeddedTypesManager, TModuleCompilationState>.SynthesizedDefinitions orAddSynthesizedDefinitions = this.GetOrAddSynthesizedDefinitions(container);
			if (orAddSynthesizedDefinitions.NestedTypes == null)
			{
				Interlocked.CompareExchange<ConcurrentQueue<INestedTypeDefinition>>(ref orAddSynthesizedDefinitions.NestedTypes, new ConcurrentQueue<INestedTypeDefinition>(), null);
			}
			orAddSynthesizedDefinitions.NestedTypes.Enqueue(nestedType);
		}

		// Token: 0x06002CFE RID: 11518 RVA: 0x0007E44C File Offset: 0x0007C64C
		public void AddSynthesizedDefinition(INamespaceSymbolInternal container, INamespaceOrTypeSymbolInternal typeOrNamespace)
		{
			if (this._lazySynthesizedNamespaceMembers == null)
			{
				Interlocked.CompareExchange<ConcurrentDictionary<INamespaceSymbolInternal, ConcurrentQueue<INamespaceOrTypeSymbolInternal>>>(ref this._lazySynthesizedNamespaceMembers, new ConcurrentDictionary<INamespaceSymbolInternal, ConcurrentQueue<INamespaceOrTypeSymbolInternal>>(), null);
			}
			this._lazySynthesizedNamespaceMembers.GetOrAdd(container, (INamespaceSymbolInternal _) => new ConcurrentQueue<INamespaceOrTypeSymbolInternal>()).Enqueue(typeOrNamespace);
		}

		// Token: 0x06002CFF RID: 11519 RVA: 0x0007E4A4 File Offset: 0x0007C6A4
		public IEnumerable<IFieldDefinition> GetSynthesizedFields(TNamedTypeSymbol container)
		{
			PEModuleBuilder<TCompilation, TSourceModuleSymbol, TAssemblySymbol, TTypeSymbol, TNamedTypeSymbol, TMethodSymbol, TSyntaxNode, TEmbeddedTypesManager, TModuleCompilationState>.SynthesizedDefinitions synthesizedDefinitions;
			if (!this._synthesizedTypeMembers.TryGetValue(container, ref synthesizedDefinitions))
			{
				return null;
			}
			return synthesizedDefinitions.Fields;
		}

		// Token: 0x06002D00 RID: 11520 RVA: 0x0007E4CC File Offset: 0x0007C6CC
		public IEnumerable<IPropertyDefinition> GetSynthesizedProperties(TNamedTypeSymbol container)
		{
			PEModuleBuilder<TCompilation, TSourceModuleSymbol, TAssemblySymbol, TTypeSymbol, TNamedTypeSymbol, TMethodSymbol, TSyntaxNode, TEmbeddedTypesManager, TModuleCompilationState>.SynthesizedDefinitions synthesizedDefinitions;
			if (!this._synthesizedTypeMembers.TryGetValue(container, ref synthesizedDefinitions))
			{
				return null;
			}
			return synthesizedDefinitions.Properties;
		}

		// Token: 0x06002D01 RID: 11521 RVA: 0x0007E4F4 File Offset: 0x0007C6F4
		public IEnumerable<IMethodDefinition> GetSynthesizedMethods(TNamedTypeSymbol container)
		{
			PEModuleBuilder<TCompilation, TSourceModuleSymbol, TAssemblySymbol, TTypeSymbol, TNamedTypeSymbol, TMethodSymbol, TSyntaxNode, TEmbeddedTypesManager, TModuleCompilationState>.SynthesizedDefinitions synthesizedDefinitions;
			if (!this._synthesizedTypeMembers.TryGetValue(container, ref synthesizedDefinitions))
			{
				return null;
			}
			return synthesizedDefinitions.Methods;
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x0007E51C File Offset: 0x0007C71C
		internal override ImmutableDictionary<ISymbolInternal, ImmutableArray<ISymbolInternal>> GetAllSynthesizedMembers()
		{
			ImmutableDictionary<ISymbolInternal, ImmutableArray<ISymbolInternal>>.Builder builder = ImmutableDictionary.CreateBuilder<ISymbolInternal, ImmutableArray<ISymbolInternal>>();
			foreach (KeyValuePair<TNamedTypeSymbol, PEModuleBuilder<TCompilation, TSourceModuleSymbol, TAssemblySymbol, TTypeSymbol, TNamedTypeSymbol, TMethodSymbol, TSyntaxNode, TEmbeddedTypesManager, TModuleCompilationState>.SynthesizedDefinitions> keyValuePair in this._synthesizedTypeMembers)
			{
				builder.Add(keyValuePair.Key, keyValuePair.Value.GetAllMembers());
			}
			ConcurrentDictionary<INamespaceSymbolInternal, ConcurrentQueue<INamespaceOrTypeSymbolInternal>> lazySynthesizedNamespaceMembers = this._lazySynthesizedNamespaceMembers;
			if (lazySynthesizedNamespaceMembers != null)
			{
				foreach (KeyValuePair<INamespaceSymbolInternal, ConcurrentQueue<INamespaceOrTypeSymbolInternal>> keyValuePair2 in lazySynthesizedNamespaceMembers)
				{
					builder.Add(keyValuePair2.Key, keyValuePair2.Value.ToImmutableArray<ISymbolInternal>());
				}
			}
			return builder.ToImmutable();
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x0007E5E8 File Offset: 0x0007C7E8
		IFieldReference ITokenDeferral.GetFieldForData(ImmutableArray<byte> data, SyntaxNode syntaxNode, DiagnosticBag diagnostics)
		{
			return this.GetPrivateImplClass((TSyntaxNode)((object)syntaxNode), diagnostics).CreateDataField(data);
		}

		// Token: 0x06002D04 RID: 11524
		public abstract IMethodReference GetInitArrayHelper();

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x06002D05 RID: 11525 RVA: 0x0007E600 File Offset: 0x0007C800
		public ArrayMethods ArrayMethods
		{
			get
			{
				ArrayMethods arrayMethods = this._lazyArrayMethods;
				if (arrayMethods == null)
				{
					arrayMethods = new ArrayMethods();
					if (Interlocked.CompareExchange<ArrayMethods>(ref this._lazyArrayMethods, arrayMethods, null) != null)
					{
						arrayMethods = this._lazyArrayMethods;
					}
				}
				return arrayMethods;
			}
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x0007E634 File Offset: 0x0007C834
		internal PrivateImplementationDetails GetPrivateImplClass(TSyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics)
		{
			PrivateImplementationDetails privateImplementationDetails = this._privateImplementationDetails;
			if (privateImplementationDetails == null && this.SupportsPrivateImplClass)
			{
				privateImplementationDetails = new PrivateImplementationDetails(this, this.SourceModule.Name, this.Compilation.GetSubmissionSlotIndex(), this.GetSpecialType(SpecialType.System_Object, syntaxNodeOpt, diagnostics), this.GetSpecialType(SpecialType.System_ValueType, syntaxNodeOpt, diagnostics), this.GetSpecialType(SpecialType.System_Byte, syntaxNodeOpt, diagnostics), this.GetSpecialType(SpecialType.System_Int16, syntaxNodeOpt, diagnostics), this.GetSpecialType(SpecialType.System_Int32, syntaxNodeOpt, diagnostics), this.GetSpecialType(SpecialType.System_Int64, syntaxNodeOpt, diagnostics), this.SynthesizeAttribute(WellKnownMember.System_Runtime_CompilerServices_CompilerGeneratedAttribute__ctor));
				if (Interlocked.CompareExchange<PrivateImplementationDetails>(ref this._privateImplementationDetails, privateImplementationDetails, null) != null)
				{
					privateImplementationDetails = this._privateImplementationDetails;
				}
			}
			return privateImplementationDetails;
		}

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x06002D07 RID: 11527 RVA: 0x0007E6D6 File Offset: 0x0007C8D6
		internal PrivateImplementationDetails PrivateImplClass
		{
			get
			{
				return this._privateImplementationDetails;
			}
		}

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x06002D08 RID: 11528 RVA: 0x0007E6DE File Offset: 0x0007C8DE
		internal override bool SupportsPrivateImplClass
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x0007E6E1 File Offset: 0x0007C8E1
		public sealed override ITypeReference GetPlatformType(PlatformType platformType, EmitContext context)
		{
			if (platformType == PlatformType.SystemType)
			{
				throw ExceptionUtilities.UnexpectedValue(platformType);
			}
			return this.GetSpecialType((SpecialType)platformType, (TSyntaxNode)((object)context.SyntaxNode), context.Diagnostics);
		}

		// Token: 0x040011CE RID: 4558
		internal readonly TSourceModuleSymbol SourceModule;

		// Token: 0x040011CF RID: 4559
		internal readonly TCompilation Compilation;

		// Token: 0x040011D0 RID: 4560
		private PrivateImplementationDetails _privateImplementationDetails;

		// Token: 0x040011D1 RID: 4561
		private ArrayMethods _lazyArrayMethods;

		// Token: 0x040011D2 RID: 4562
		private HashSet<string> _namesOfTopLevelTypes;

		// Token: 0x040011D3 RID: 4563
		internal readonly TModuleCompilationState CompilationState;

		// Token: 0x040011D5 RID: 4565
		private readonly ConcurrentDictionary<TNamedTypeSymbol, PEModuleBuilder<TCompilation, TSourceModuleSymbol, TAssemblySymbol, TTypeSymbol, TNamedTypeSymbol, TMethodSymbol, TSyntaxNode, TEmbeddedTypesManager, TModuleCompilationState>.SynthesizedDefinitions> _synthesizedTypeMembers = new ConcurrentDictionary<TNamedTypeSymbol, PEModuleBuilder<TCompilation, TSourceModuleSymbol, TAssemblySymbol, TTypeSymbol, TNamedTypeSymbol, TMethodSymbol, TSyntaxNode, TEmbeddedTypesManager, TModuleCompilationState>.SynthesizedDefinitions>(ReferenceEqualityComparer.Instance);

		// Token: 0x040011D6 RID: 4566
		private ConcurrentDictionary<INamespaceSymbolInternal, ConcurrentQueue<INamespaceOrTypeSymbolInternal>> _lazySynthesizedNamespaceMembers;

		// Token: 0x0200072B RID: 1835
		private sealed class SynthesizedDefinitions
		{
			// Token: 0x06003BBC RID: 15292 RVA: 0x000ABB48 File Offset: 0x000A9D48
			public ImmutableArray<ISymbolInternal> GetAllMembers()
			{
				ArrayBuilder<ISymbolInternal> instance = ArrayBuilder<ISymbolInternal>.GetInstance();
				if (this.Fields != null)
				{
					foreach (IFieldDefinition fieldDefinition in this.Fields)
					{
						instance.Add(fieldDefinition.GetInternalSymbol());
					}
				}
				if (this.Methods != null)
				{
					foreach (IMethodDefinition methodDefinition in this.Methods)
					{
						instance.Add(methodDefinition.GetInternalSymbol());
					}
				}
				if (this.Properties != null)
				{
					foreach (IPropertyDefinition propertyDefinition in this.Properties)
					{
						instance.Add(propertyDefinition.GetInternalSymbol());
					}
				}
				if (this.NestedTypes != null)
				{
					foreach (INestedTypeDefinition nestedTypeDefinition in this.NestedTypes)
					{
						instance.Add(nestedTypeDefinition.GetInternalSymbol());
					}
				}
				return instance.ToImmutableAndFree();
			}

			// Token: 0x04001C96 RID: 7318
			public ConcurrentQueue<INestedTypeDefinition> NestedTypes;

			// Token: 0x04001C97 RID: 7319
			public ConcurrentQueue<IMethodDefinition> Methods;

			// Token: 0x04001C98 RID: 7320
			public ConcurrentQueue<IPropertyDefinition> Properties;

			// Token: 0x04001C99 RID: 7321
			public ConcurrentQueue<IFieldDefinition> Fields;
		}
	}
}
