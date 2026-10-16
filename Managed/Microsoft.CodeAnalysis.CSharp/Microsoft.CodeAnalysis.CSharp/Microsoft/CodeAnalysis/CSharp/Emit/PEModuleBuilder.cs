using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.CSharp.Emit.NoPia;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Emit
{
	// Token: 0x02000493 RID: 1171
	internal abstract class PEModuleBuilder : PEModuleBuilder<CSharpCompilation, SourceModuleSymbol, AssemblySymbol, TypeSymbol, NamedTypeSymbol, MethodSymbol, SyntaxNode, EmbeddedTypesManager, ModuleCompilationState>
	{
		// Token: 0x170016B0 RID: 5808
		// (get) Token: 0x0600612C RID: 24876 RVA: 0x001765C3 File Offset: 0x001747C3
		public override EmbeddedTypesManager EmbeddedTypesManagerOpt
		{
			get
			{
				return this._embeddedTypesManagerOpt;
			}
		}

		// Token: 0x0600612D RID: 24877 RVA: 0x001765CB File Offset: 0x001747CB
		internal EmbeddableAttributes GetNeedsGeneratedAttributes()
		{
			this._needsGeneratedAttributes_IsFrozen = true;
			return this.GetNeedsGeneratedAttributesInternal();
		}

		// Token: 0x0600612E RID: 24878 RVA: 0x001765DA File Offset: 0x001747DA
		private EmbeddableAttributes GetNeedsGeneratedAttributesInternal()
		{
			return (EmbeddableAttributes)(this._needsGeneratedAttributes | (int)this.Compilation.GetNeedsGeneratedAttributes());
		}

		// Token: 0x0600612F RID: 24879 RVA: 0x001765EE File Offset: 0x001747EE
		private void SetNeedsGeneratedAttributes(EmbeddableAttributes attributes)
		{
			ThreadSafeFlagOperations.Set(ref this._needsGeneratedAttributes, (int)attributes);
		}

		// Token: 0x06006130 RID: 24880 RVA: 0x00176600 File Offset: 0x00174800
		internal PEModuleBuilder(SourceModuleSymbol sourceModule, EmitOptions emitOptions, OutputKind outputKind, ModulePropertiesForSerialization serializationProperties, IEnumerable<ResourceDescription> manifestResources)
			: base(sourceModule.ContainingSourceAssembly.DeclaringCompilation, sourceModule, serializationProperties, manifestResources, outputKind, emitOptions, new ModuleCompilationState())
		{
			string metadataName = sourceModule.MetadataName;
			this._metadataName = ((metadataName != "?") ? metadataName : (emitOptions.OutputNameOverride ?? metadataName));
			this.AssemblyOrModuleSymbolToModuleRefMap.Add(sourceModule, this);
			if (sourceModule.AnyReferencedAssembliesAreLinked)
			{
				this._embeddedTypesManagerOpt = new EmbeddedTypesManager(this);
			}
		}

		// Token: 0x170016B1 RID: 5809
		// (get) Token: 0x06006131 RID: 24881 RVA: 0x00176699 File Offset: 0x00174899
		public override string Name
		{
			get
			{
				return this._metadataName;
			}
		}

		// Token: 0x170016B2 RID: 5810
		// (get) Token: 0x06006132 RID: 24882 RVA: 0x001766A1 File Offset: 0x001748A1
		internal sealed override string ModuleName
		{
			get
			{
				return this._metadataName;
			}
		}

		// Token: 0x06006133 RID: 24883 RVA: 0x001766AC File Offset: 0x001748AC
		internal sealed override ICustomAttribute SynthesizeAttribute(WellKnownMember attributeConstructor)
		{
			return this.Compilation.TrySynthesizeAttribute(attributeConstructor, default(ImmutableArray<TypedConstant>), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false);
		}

		// Token: 0x06006134 RID: 24884 RVA: 0x001766D8 File Offset: 0x001748D8
		public sealed override IEnumerable<ICustomAttribute> GetSourceAssemblyAttributes(bool isRefAssembly)
		{
			return this.SourceModule.ContainingSourceAssembly.GetCustomAttributesToEmit(this, isRefAssembly, this.OutputKind.IsNetModule());
		}

		// Token: 0x06006135 RID: 24885 RVA: 0x001766F7 File Offset: 0x001748F7
		public sealed override IEnumerable<SecurityAttribute> GetSourceAssemblySecurityAttributes()
		{
			return this.SourceModule.ContainingSourceAssembly.GetSecurityAttributes();
		}

		// Token: 0x06006136 RID: 24886 RVA: 0x00176709 File Offset: 0x00174909
		public sealed override IEnumerable<ICustomAttribute> GetSourceModuleAttributes()
		{
			return this.SourceModule.GetCustomAttributesToEmit(this);
		}

		// Token: 0x170016B3 RID: 5811
		// (get) Token: 0x06006137 RID: 24887 RVA: 0x00176717 File Offset: 0x00174917
		internal sealed override AssemblySymbol CorLibrary
		{
			get
			{
				return this.SourceModule.ContainingSourceAssembly.CorLibrary;
			}
		}

		// Token: 0x170016B4 RID: 5812
		// (get) Token: 0x06006138 RID: 24888 RVA: 0x00176729 File Offset: 0x00174929
		public sealed override bool GenerateVisualBasicStylePdb
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170016B5 RID: 5813
		// (get) Token: 0x06006139 RID: 24889 RVA: 0x0017672C File Offset: 0x0017492C
		public sealed override IEnumerable<string> LinkedAssembliesDebugInfo
		{
			get
			{
				return SpecializedCollections.EmptyEnumerable<string>();
			}
		}

		// Token: 0x0600613A RID: 24890 RVA: 0x00176733 File Offset: 0x00174933
		public sealed override ImmutableArray<UsedNamespaceOrType> GetImports()
		{
			return ImmutableArray<UsedNamespaceOrType>.Empty;
		}

		// Token: 0x170016B6 RID: 5814
		// (get) Token: 0x0600613B RID: 24891 RVA: 0x0017673A File Offset: 0x0017493A
		public sealed override string DefaultNamespace
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600613C RID: 24892 RVA: 0x0017673D File Offset: 0x0017493D
		protected sealed override IEnumerable<IAssemblyReference> GetAssemblyReferencesFromAddedModules(DiagnosticBag diagnostics)
		{
			ImmutableArray<ModuleSymbol> modules = this.SourceModule.ContainingAssembly.Modules;
			int num;
			for (int i = 1; i < modules.Length; i = num + 1)
			{
				foreach (AssemblySymbol assemblySymbol in modules[i].GetReferencedAssemblySymbols())
				{
					yield return this.Translate(assemblySymbol, diagnostics);
				}
				ImmutableArray<AssemblySymbol>.Enumerator enumerator = default(ImmutableArray<AssemblySymbol>.Enumerator);
				num = i;
			}
			yield break;
		}

		// Token: 0x0600613D RID: 24893 RVA: 0x00176754 File Offset: 0x00174954
		private void ValidateReferencedAssembly(AssemblySymbol assembly, AssemblyReference asmRef, DiagnosticBag diagnostics)
		{
			AssemblyIdentity identity = this.SourceModule.ContainingAssembly.Identity;
			AssemblyIdentity identity2 = asmRef.Identity;
			if (identity.IsStrongName && !identity2.IsStrongName && asmRef.Identity.ContentType != 1)
			{
				diagnostics.Add(new CSDiagnosticInfo(ErrorCode.WRN_ReferencedAssemblyDoesNotHaveStrongName, new object[] { assembly }), NoLocation.Singleton);
			}
			if (this.OutputKind != OutputKind.NetModule && !string.IsNullOrEmpty(identity2.CultureName) && !string.Equals(identity2.CultureName, identity.CultureName, 5))
			{
				diagnostics.Add(new CSDiagnosticInfo(ErrorCode.WRN_RefCultureMismatch, new object[] { assembly, identity2.CultureName }), NoLocation.Singleton);
			}
			Machine machine = assembly.Machine;
			if (assembly != assembly.CorLibrary && (machine != Machine.I386 || assembly.Bit32Required))
			{
				Machine machine2 = this.SourceModule.Machine;
				if ((machine2 != Machine.I386 || this.SourceModule.Bit32Required) && machine2 != machine)
				{
					diagnostics.Add(new CSDiagnosticInfo(ErrorCode.WRN_ConflictingMachineAssembly, new object[] { assembly }), NoLocation.Singleton);
				}
			}
			if (this._embeddedTypesManagerOpt != null && this._embeddedTypesManagerOpt.IsFrozen)
			{
				this._embeddedTypesManagerOpt.ReportIndirectReferencesToLinkedAssemblies(assembly, diagnostics);
			}
		}

		// Token: 0x0600613E RID: 24894 RVA: 0x0017688E File Offset: 0x00174A8E
		internal sealed override IEnumerable<INestedTypeDefinition> GetSynthesizedNestedTypes(NamedTypeSymbol container)
		{
			return null;
		}

		// Token: 0x0600613F RID: 24895 RVA: 0x00176894 File Offset: 0x00174A94
		public sealed override MultiDictionary<DebugSourceDocument, DefinitionWithLocation> GetSymbolToLocationMap()
		{
			MultiDictionary<DebugSourceDocument, DefinitionWithLocation> multiDictionary = new MultiDictionary<DebugSourceDocument, DefinitionWithLocation>();
			Stack<NamespaceOrTypeSymbol> stack = new Stack<NamespaceOrTypeSymbol>();
			stack.Push(this.SourceModule.GlobalNamespace);
			while (stack.Count > 0)
			{
				NamespaceOrTypeSymbol namespaceOrTypeSymbol = stack.Pop();
				SymbolKind kind = namespaceOrTypeSymbol.Kind;
				if (kind != SymbolKind.NamedType)
				{
					if (kind != SymbolKind.Namespace)
					{
						throw ExceptionUtilities.UnexpectedValue(namespaceOrTypeSymbol.Kind);
					}
					Location location = this.GetSmallestSourceLocationOrNull(namespaceOrTypeSymbol);
					if (location != null)
					{
						foreach (Symbol symbol in namespaceOrTypeSymbol.GetMembers())
						{
							SymbolKind symbolKind = symbol.Kind;
							if (symbolKind - SymbolKind.NamedType > 1)
							{
								throw ExceptionUtilities.UnexpectedValue(symbol.Kind);
							}
							stack.Push((NamespaceOrTypeSymbol)symbol);
						}
					}
				}
				else
				{
					Location location = this.GetSmallestSourceLocationOrNull(namespaceOrTypeSymbol);
					if (location != null)
					{
						this.AddSymbolLocation(multiDictionary, location, (IDefinition)namespaceOrTypeSymbol.GetCciAdapter());
						foreach (Symbol symbol2 in namespaceOrTypeSymbol.GetMembers())
						{
							SymbolKind symbolKind = symbol2.Kind;
							switch (symbolKind)
							{
							case SymbolKind.Event:
							{
								this.AddSymbolLocation(multiDictionary, symbol2);
								FieldSymbol associatedField = ((EventSymbol)symbol2).AssociatedField;
								if (associatedField != null)
								{
									this.AddSymbolLocation(multiDictionary, associatedField);
									continue;
								}
								continue;
							}
							case SymbolKind.Field:
								if (!(symbol2 is TupleErrorFieldSymbol))
								{
									this.AddSymbolLocation(multiDictionary, symbol2);
									continue;
								}
								continue;
							case SymbolKind.Label:
							case SymbolKind.Local:
							case SymbolKind.NetModule:
								break;
							case SymbolKind.Method:
								if (((MethodSymbol)symbol2).ShouldEmit())
								{
									this.AddSymbolLocation(multiDictionary, symbol2);
									continue;
								}
								continue;
							case SymbolKind.NamedType:
								stack.Push((NamespaceOrTypeSymbol)symbol2);
								continue;
							default:
								if (symbolKind == SymbolKind.Property)
								{
									this.AddSymbolLocation(multiDictionary, symbol2);
									continue;
								}
								break;
							}
							throw ExceptionUtilities.UnexpectedValue(symbol2.Kind);
						}
					}
				}
			}
			return multiDictionary;
		}

		// Token: 0x06006140 RID: 24896 RVA: 0x00176A7C File Offset: 0x00174C7C
		private void AddSymbolLocation(MultiDictionary<DebugSourceDocument, DefinitionWithLocation> result, Symbol symbol)
		{
			Location smallestSourceLocationOrNull = this.GetSmallestSourceLocationOrNull(symbol);
			if (smallestSourceLocationOrNull != null)
			{
				this.AddSymbolLocation(result, smallestSourceLocationOrNull, (IDefinition)symbol.GetCciAdapter());
			}
		}

		// Token: 0x06006141 RID: 24897 RVA: 0x00176AB0 File Offset: 0x00174CB0
		private void AddSymbolLocation(MultiDictionary<DebugSourceDocument, DefinitionWithLocation> result, Location location, IDefinition definition)
		{
			FileLinePositionSpan lineSpan = location.GetLineSpan();
			DebugSourceDocument debugSourceDocument = this.DebugDocumentsBuilder.TryGetDebugDocument(lineSpan.Path, location.SourceTree.FilePath);
			if (debugSourceDocument != null)
			{
				result.Add(debugSourceDocument, new DefinitionWithLocation(definition, lineSpan.StartLinePosition.Line, lineSpan.StartLinePosition.Character, lineSpan.EndLinePosition.Line, lineSpan.EndLinePosition.Character));
			}
		}

		// Token: 0x06006142 RID: 24898 RVA: 0x00176B30 File Offset: 0x00174D30
		private Location GetSmallestSourceLocationOrNull(Symbol symbol)
		{
			CSharpCompilation declaringCompilation = symbol.DeclaringCompilation;
			Location location = null;
			foreach (Location location2 in symbol.Locations)
			{
				if (location2.IsInSource && (location == null || declaringCompilation.CompareSourceLocations(location, location2) > 0))
				{
					location = location2;
				}
			}
			return location;
		}

		// Token: 0x170016B7 RID: 5815
		// (get) Token: 0x06006143 RID: 24899 RVA: 0x00176B8B File Offset: 0x00174D8B
		internal virtual bool IgnoreAccessibility
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170016B8 RID: 5816
		// (get) Token: 0x06006144 RID: 24900 RVA: 0x00176B8E File Offset: 0x00174D8E
		internal virtual bool IsEncDelta
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006145 RID: 24901 RVA: 0x00176B91 File Offset: 0x00174D91
		internal virtual NamedTypeSymbol GetDynamicOperationContextType(NamedTypeSymbol contextType)
		{
			return contextType;
		}

		// Token: 0x06006146 RID: 24902 RVA: 0x00176B94 File Offset: 0x00174D94
		internal virtual VariableSlotAllocator TryCreateVariableSlotAllocator(MethodSymbol method, MethodSymbol topLevelMethod, DiagnosticBag diagnostics)
		{
			return null;
		}

		// Token: 0x06006147 RID: 24903 RVA: 0x00176B97 File Offset: 0x00174D97
		internal virtual ImmutableArray<AnonymousTypeKey> GetPreviousAnonymousTypes()
		{
			return ImmutableArray<AnonymousTypeKey>.Empty;
		}

		// Token: 0x06006148 RID: 24904 RVA: 0x00176B9E File Offset: 0x00174D9E
		internal virtual int GetNextAnonymousTypeIndex()
		{
			return 0;
		}

		// Token: 0x06006149 RID: 24905 RVA: 0x00176BA1 File Offset: 0x00174DA1
		internal virtual bool TryGetAnonymousTypeName(AnonymousTypeManager.AnonymousTypeTemplateSymbol template, out string name, out int index)
		{
			name = null;
			index = -1;
			return false;
		}

		// Token: 0x0600614A RID: 24906 RVA: 0x00176BAA File Offset: 0x00174DAA
		public sealed override IEnumerable<INamespaceTypeDefinition> GetAnonymousTypeDefinitions(EmitContext context)
		{
			if (context.MetadataOnly)
			{
				return SpecializedCollections.EmptyEnumerable<INamespaceTypeDefinition>();
			}
			return this.Compilation.AnonymousTypeManager.GetAllCreatedTemplates();
		}

		// Token: 0x0600614B RID: 24907 RVA: 0x00176BD0 File Offset: 0x00174DD0
		public override IEnumerable<INamespaceTypeDefinition> GetTopLevelSourceTypeDefinitions(EmitContext context)
		{
			Stack<NamespaceSymbol> namespacesToProcess = new Stack<NamespaceSymbol>();
			namespacesToProcess.Push(this.SourceModule.GlobalNamespace);
			while (namespacesToProcess.Count > 0)
			{
				NamespaceSymbol namespaceSymbol = namespacesToProcess.Pop();
				foreach (Symbol symbol in namespaceSymbol.GetMembers())
				{
					if (symbol.Kind == SymbolKind.Namespace)
					{
						namespacesToProcess.Push((NamespaceSymbol)symbol);
					}
					else
					{
						yield return ((NamedTypeSymbol)symbol).GetCciAdapter();
					}
				}
				ImmutableArray<Symbol>.Enumerator enumerator = default(ImmutableArray<Symbol>.Enumerator);
			}
			yield break;
		}

		// Token: 0x0600614C RID: 24908 RVA: 0x00176BE0 File Offset: 0x00174DE0
		private static void GetExportedTypes(NamespaceOrTypeSymbol symbol, int parentIndex, ArrayBuilder<ExportedType> builder)
		{
			int num;
			if (symbol.Kind == SymbolKind.NamedType)
			{
				if (symbol.DeclaredAccessibility != Accessibility.Public)
				{
					return;
				}
				num = builder.Count;
				builder.Add(new ExportedType((ITypeReference)symbol.GetCciAdapter(), parentIndex, false));
			}
			else
			{
				num = -1;
			}
			foreach (Symbol symbol2 in symbol.GetMembers())
			{
				NamespaceOrTypeSymbol namespaceOrTypeSymbol = symbol2 as NamespaceOrTypeSymbol;
				if (namespaceOrTypeSymbol != null)
				{
					PEModuleBuilder.GetExportedTypes(namespaceOrTypeSymbol, num, builder);
				}
			}
		}

		// Token: 0x0600614D RID: 24909 RVA: 0x00176C56 File Offset: 0x00174E56
		public sealed override ImmutableArray<ExportedType> GetExportedTypes(DiagnosticBag diagnostics)
		{
			if (this._lazyExportedTypes.IsDefault)
			{
				this._lazyExportedTypes = this.CalculateExportedTypes();
				if (this._lazyExportedTypes.Length > 0)
				{
					this.ReportExportedTypeNameCollisions(this._lazyExportedTypes, diagnostics);
				}
			}
			return this._lazyExportedTypes;
		}

		// Token: 0x0600614E RID: 24910 RVA: 0x00176C94 File Offset: 0x00174E94
		private ImmutableArray<ExportedType> CalculateExportedTypes()
		{
			SourceAssemblySymbol containingSourceAssembly = this.SourceModule.ContainingSourceAssembly;
			ArrayBuilder<ExportedType> instance = ArrayBuilder<ExportedType>.GetInstance();
			if (!this.OutputKind.IsNetModule())
			{
				ImmutableArray<ModuleSymbol> modules = containingSourceAssembly.Modules;
				for (int i = 1; i < modules.Length; i++)
				{
					PEModuleBuilder.GetExportedTypes(modules[i].GlobalNamespace, -1, instance);
				}
			}
			PEModuleBuilder.GetForwardedTypes(containingSourceAssembly, instance);
			return instance.ToImmutableAndFree();
		}

		// Token: 0x0600614F RID: 24911 RVA: 0x00176CFC File Offset: 0x00174EFC
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal static HashSet<NamedTypeSymbol> GetForwardedTypes(SourceAssemblySymbol sourceAssembly, [global::System.Runtime.CompilerServices.Nullable(2)] ArrayBuilder<ExportedType> builder)
		{
			HashSet<NamedTypeSymbol> hashSet = new HashSet<NamedTypeSymbol>();
			PEModuleBuilder.GetForwardedTypes(hashSet, sourceAssembly.GetSourceDecodedWellKnownAttributeData(), builder);
			if (!sourceAssembly.DeclaringCompilation.Options.OutputKind.IsNetModule())
			{
				PEModuleBuilder.GetForwardedTypes(hashSet, sourceAssembly.GetNetModuleDecodedWellKnownAttributeData(), builder);
			}
			return hashSet;
		}

		// Token: 0x06006150 RID: 24912 RVA: 0x00176D44 File Offset: 0x00174F44
		private void ReportExportedTypeNameCollisions(ImmutableArray<ExportedType> exportedTypes, DiagnosticBag diagnostics)
		{
			SourceAssemblySymbol containingSourceAssembly = this.SourceModule.ContainingSourceAssembly;
			Dictionary<string, NamedTypeSymbol> dictionary = new Dictionary<string, NamedTypeSymbol>(StringOrdinalComparer.Instance);
			foreach (ExportedType exportedType in exportedTypes)
			{
				NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)exportedType.Type.GetInternalSymbol();
				if (namedTypeSymbol.IsTopLevelType())
				{
					string text = MetadataHelpers.BuildQualifiedName(((INamespaceTypeReference)namedTypeSymbol.GetCciAdapter()).NamespaceName, MetadataWriter.GetMangledName(namedTypeSymbol.GetCciAdapter()));
					NamedTypeSymbol namedTypeSymbol2;
					if (base.ContainsTopLevelType(text))
					{
						if (namedTypeSymbol.ContainingAssembly == containingSourceAssembly)
						{
							diagnostics.Add(ErrorCode.ERR_ExportedTypeConflictsWithDeclaration, NoLocation.Singleton, new object[] { namedTypeSymbol, namedTypeSymbol.ContainingModule });
						}
						else
						{
							diagnostics.Add(ErrorCode.ERR_ForwardedTypeConflictsWithDeclaration, NoLocation.Singleton, new object[] { namedTypeSymbol });
						}
					}
					else if (dictionary.TryGetValue(text, ref namedTypeSymbol2))
					{
						if (namedTypeSymbol.ContainingAssembly == containingSourceAssembly)
						{
							diagnostics.Add(ErrorCode.ERR_ExportedTypesConflict, NoLocation.Singleton, new object[] { namedTypeSymbol, namedTypeSymbol.ContainingModule, namedTypeSymbol2, namedTypeSymbol2.ContainingModule });
						}
						else if (namedTypeSymbol2.ContainingAssembly == containingSourceAssembly)
						{
							diagnostics.Add(ErrorCode.ERR_ForwardedTypeConflictsWithExportedType, NoLocation.Singleton, new object[] { namedTypeSymbol, namedTypeSymbol.ContainingAssembly, namedTypeSymbol2, namedTypeSymbol2.ContainingModule });
						}
						else
						{
							diagnostics.Add(ErrorCode.ERR_ForwardedTypesConflict, NoLocation.Singleton, new object[] { namedTypeSymbol, namedTypeSymbol.ContainingAssembly, namedTypeSymbol2, namedTypeSymbol2.ContainingAssembly });
						}
					}
					else
					{
						dictionary.Add(text, namedTypeSymbol);
					}
				}
			}
		}

		// Token: 0x06006151 RID: 24913 RVA: 0x00176EE8 File Offset: 0x001750E8
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private static void GetForwardedTypes(HashSet<NamedTypeSymbol> seenTopLevelTypes, CommonAssemblyWellKnownAttributeData<NamedTypeSymbol> wellKnownAttributeData, [global::System.Runtime.CompilerServices.Nullable(2)] ArrayBuilder<ExportedType> builder)
		{
			if (wellKnownAttributeData != null)
			{
				HashSet<NamedTypeSymbol> forwardedTypes = wellKnownAttributeData.ForwardedTypes;
				int? num = ((forwardedTypes != null) ? new int?(forwardedTypes.Count) : default(int?));
				int num2 = 0;
				if ((num.GetValueOrDefault() > num2) & (num != null))
				{
					ArrayBuilder<ValueTuple<NamedTypeSymbol, int>> instance = ArrayBuilder<ValueTuple<NamedTypeSymbol, int>>.GetInstance();
					IEnumerable<NamedTypeSymbol> enumerable = wellKnownAttributeData.ForwardedTypes;
					if (builder != null)
					{
						enumerable = Enumerable.OrderBy<NamedTypeSymbol, string>(enumerable, (NamedTypeSymbol t) => t.OriginalDefinition.ToDisplayString(SymbolDisplayFormat.QualifiedNameArityFormat));
					}
					foreach (NamedTypeSymbol namedTypeSymbol in enumerable)
					{
						NamedTypeSymbol originalDefinition = namedTypeSymbol.OriginalDefinition;
						if (seenTopLevelTypes.Add(originalDefinition) && builder != null)
						{
							instance.Push(new ValueTuple<NamedTypeSymbol, int>(originalDefinition, -1));
							while (instance.Count > 0)
							{
								ValueTuple<NamedTypeSymbol, int> valueTuple = instance.Pop<ValueTuple<NamedTypeSymbol, int>>();
								NamedTypeSymbol item = valueTuple.Item1;
								int item2 = valueTuple.Item2;
								if (item.DeclaredAccessibility != Accessibility.Private)
								{
									int count = builder.Count;
									builder.Add(new ExportedType(item.GetCciAdapter(), item2, true));
									ImmutableArray<NamedTypeSymbol> typeMembers = item.GetTypeMembers();
									for (int i = typeMembers.Length - 1; i >= 0; i--)
									{
										instance.Push(new ValueTuple<NamedTypeSymbol, int>(typeMembers[i], count));
									}
								}
							}
						}
					}
					instance.Free();
				}
			}
		}

		// Token: 0x06006152 RID: 24914 RVA: 0x00177058 File Offset: 0x00175258
		internal IEnumerable<AssemblySymbol> GetReferencedAssembliesUsedSoFar()
		{
			foreach (AssemblySymbol assemblySymbol in this.SourceModule.GetReferencedAssemblySymbols())
			{
				if (!assemblySymbol.IsLinked && !assemblySymbol.IsMissing && this.AssemblyOrModuleSymbolToModuleRefMap.ContainsKey(assemblySymbol))
				{
					yield return assemblySymbol;
				}
			}
			ImmutableArray<AssemblySymbol>.Enumerator enumerator = default(ImmutableArray<AssemblySymbol>.Enumerator);
			yield break;
		}

		// Token: 0x06006153 RID: 24915 RVA: 0x00177068 File Offset: 0x00175268
		private NamedTypeSymbol GetUntranslatedSpecialType(SpecialType specialType, SyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics)
		{
			NamedTypeSymbol specialType2 = this.SourceModule.ContainingAssembly.GetSpecialType(specialType);
			DiagnosticInfo diagnosticInfo = specialType2.GetUseSiteInfo().DiagnosticInfo;
			if (diagnosticInfo != null)
			{
				Symbol.ReportUseSiteDiagnostic(diagnosticInfo, diagnostics, (syntaxNodeOpt != null) ? syntaxNodeOpt.Location : NoLocation.Singleton);
			}
			return specialType2;
		}

		// Token: 0x06006154 RID: 24916 RVA: 0x001770B0 File Offset: 0x001752B0
		internal sealed override INamedTypeReference GetSpecialType(SpecialType specialType, SyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics)
		{
			return this.Translate(this.GetUntranslatedSpecialType(specialType, syntaxNodeOpt, diagnostics), syntaxNodeOpt, diagnostics, false, true);
		}

		// Token: 0x06006155 RID: 24917 RVA: 0x001770D2 File Offset: 0x001752D2
		public sealed override IMethodReference GetInitArrayHelper()
		{
			MethodSymbol methodSymbol = (MethodSymbol)this.Compilation.GetWellKnownTypeMember(WellKnownMember.System_Runtime_CompilerServices_RuntimeHelpers__InitializeArrayArrayRuntimeFieldHandle);
			if (methodSymbol == null)
			{
				return null;
			}
			return methodSymbol.GetCciAdapter();
		}

		// Token: 0x06006156 RID: 24918 RVA: 0x001770F4 File Offset: 0x001752F4
		public sealed override bool IsPlatformType(ITypeReference typeRef, PlatformType platformType)
		{
			NamedTypeSymbol namedTypeSymbol = typeRef.GetInternalSymbol() as NamedTypeSymbol;
			if (namedTypeSymbol == null)
			{
				return false;
			}
			if (platformType == PlatformType.SystemType)
			{
				return namedTypeSymbol == this.Compilation.GetWellKnownType(WellKnownType.System_Type);
			}
			return namedTypeSymbol.SpecialType == (SpecialType)platformType;
		}

		// Token: 0x06006157 RID: 24919 RVA: 0x00177134 File Offset: 0x00175334
		protected sealed override IAssemblyReference GetCorLibraryReferenceToEmit(EmitContext context)
		{
			AssemblySymbol corLibrary = this.CorLibrary;
			if (!corLibrary.IsMissing && !corLibrary.IsLinked && corLibrary != this.SourceModule.ContainingAssembly)
			{
				return this.Translate(corLibrary, context.Diagnostics);
			}
			return null;
		}

		// Token: 0x06006158 RID: 24920 RVA: 0x00177178 File Offset: 0x00175378
		internal sealed override IAssemblyReference Translate(AssemblySymbol assembly, DiagnosticBag diagnostics)
		{
			if (this.SourceModule.ContainingAssembly == assembly)
			{
				return (IAssemblyReference)this;
			}
			IModuleReference moduleReference;
			if (this.AssemblyOrModuleSymbolToModuleRefMap.TryGetValue(assembly, ref moduleReference))
			{
				return (IAssemblyReference)moduleReference;
			}
			AssemblyReference assemblyReference = new AssemblyReference(assembly);
			AssemblyReference assemblyReference2 = (AssemblyReference)this.AssemblyOrModuleSymbolToModuleRefMap.GetOrAdd(assembly, assemblyReference);
			if (assemblyReference2 == assemblyReference)
			{
				this.ValidateReferencedAssembly(assembly, assemblyReference2, diagnostics);
			}
			this.AssemblyOrModuleSymbolToModuleRefMap.TryAdd(assembly.Modules[0], assemblyReference2);
			return assemblyReference2;
		}

		// Token: 0x06006159 RID: 24921 RVA: 0x001771F8 File Offset: 0x001753F8
		internal IModuleReference Translate(ModuleSymbol module, DiagnosticBag diagnostics)
		{
			if (this.SourceModule == module)
			{
				return this;
			}
			if (module == null)
			{
				return null;
			}
			IModuleReference moduleReference;
			if (this.AssemblyOrModuleSymbolToModuleRefMap.TryGetValue(module, ref moduleReference))
			{
				return moduleReference;
			}
			moduleReference = this.TranslateModule(module, diagnostics);
			moduleReference = this.AssemblyOrModuleSymbolToModuleRefMap.GetOrAdd(module, moduleReference);
			return moduleReference;
		}

		// Token: 0x0600615A RID: 24922 RVA: 0x00177240 File Offset: 0x00175440
		protected virtual IModuleReference TranslateModule(ModuleSymbol module, DiagnosticBag diagnostics)
		{
			AssemblySymbol containingAssembly = module.ContainingAssembly;
			if (containingAssembly != null && containingAssembly.Modules[0] == module)
			{
				IModuleReference moduleReference = new AssemblyReference(containingAssembly);
				IModuleReference orAdd = this.AssemblyOrModuleSymbolToModuleRefMap.GetOrAdd(containingAssembly, moduleReference);
				if (orAdd == moduleReference)
				{
					this.ValidateReferencedAssembly(containingAssembly, (AssemblyReference)moduleReference, diagnostics);
				}
				else
				{
					moduleReference = orAdd;
				}
				return moduleReference;
			}
			return new ModuleReference(this, module);
		}

		// Token: 0x0600615B RID: 24923 RVA: 0x001772A0 File Offset: 0x001754A0
		internal INamedTypeReference Translate(NamedTypeSymbol namedTypeSymbol, SyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics, bool fromImplements = false, bool needDeclaration = false)
		{
			if (namedTypeSymbol.IsAnonymousType)
			{
				namedTypeSymbol = AnonymousTypeManager.TranslateAnonymousTypeSymbol(namedTypeSymbol);
			}
			else if (namedTypeSymbol.IsTupleType)
			{
				this.CheckTupleUnderlyingType(namedTypeSymbol, syntaxNodeOpt, diagnostics);
			}
			if (namedTypeSymbol.OriginalDefinition.Kind == SymbolKind.ErrorType)
			{
				ErrorTypeSymbol errorTypeSymbol = (ErrorTypeSymbol)namedTypeSymbol.OriginalDefinition;
				DiagnosticInfo diagnosticInfo = errorTypeSymbol.GetUseSiteInfo().DiagnosticInfo ?? errorTypeSymbol.ErrorInfo;
				if (diagnosticInfo == null && namedTypeSymbol.Kind == SymbolKind.ErrorType)
				{
					errorTypeSymbol = (ErrorTypeSymbol)namedTypeSymbol;
					diagnosticInfo = errorTypeSymbol.GetUseSiteInfo().DiagnosticInfo ?? errorTypeSymbol.ErrorInfo;
				}
				if (this._reportedErrorTypesMap.Add(errorTypeSymbol))
				{
					DiagnosticInfo diagnosticInfo2;
					if ((diagnosticInfo2 = diagnosticInfo) == null)
					{
						diagnosticInfo2 = new CSDiagnosticInfo(ErrorCode.ERR_BogusType, new object[] { string.Empty });
					}
					diagnostics.Add(new CSDiagnostic(diagnosticInfo2, (syntaxNodeOpt == null) ? NoLocation.Singleton : syntaxNodeOpt.Location, false));
				}
				return ErrorType.Singleton;
			}
			if (!namedTypeSymbol.IsDefinition)
			{
				if (!namedTypeSymbol.IsUnboundGenericType)
				{
					return (INamedTypeReference)this.GetCciAdapter(namedTypeSymbol);
				}
				namedTypeSymbol = namedTypeSymbol.OriginalDefinition;
			}
			else if (!needDeclaration)
			{
				NamedTypeSymbol containingType = namedTypeSymbol.ContainingType;
				if (namedTypeSymbol.Arity > 0)
				{
					object obj;
					if (this._genericInstanceMap.TryGetValue(namedTypeSymbol, ref obj))
					{
						return (INamedTypeReference)obj;
					}
					INamedTypeReference namedTypeReference;
					if (containingType != null)
					{
						if (PEModuleBuilder.IsGenericType(containingType))
						{
							namedTypeReference = new SpecializedGenericNestedTypeInstanceReference(namedTypeSymbol);
						}
						else
						{
							namedTypeReference = new GenericNestedTypeInstanceReference(namedTypeSymbol);
						}
					}
					else
					{
						namedTypeReference = new GenericNamespaceTypeInstanceReference(namedTypeSymbol);
					}
					return (INamedTypeReference)this._genericInstanceMap.GetOrAdd(namedTypeSymbol, namedTypeReference);
				}
				else if (PEModuleBuilder.IsGenericType(containingType))
				{
					object obj;
					if (this._genericInstanceMap.TryGetValue(namedTypeSymbol, ref obj))
					{
						return (INamedTypeReference)obj;
					}
					INamedTypeReference namedTypeReference = new SpecializedNestedTypeReference(namedTypeSymbol);
					return (INamedTypeReference)this._genericInstanceMap.GetOrAdd(namedTypeSymbol, namedTypeReference);
				}
				else
				{
					NamedTypeSymbol nativeIntegerUnderlyingType = namedTypeSymbol.NativeIntegerUnderlyingType;
					if (nativeIntegerUnderlyingType != null)
					{
						namedTypeSymbol = nativeIntegerUnderlyingType;
					}
				}
			}
			EmbeddedTypesManager embeddedTypesManagerOpt = this._embeddedTypesManagerOpt;
			return ((embeddedTypesManagerOpt != null) ? embeddedTypesManagerOpt.EmbedTypeIfNeedTo(namedTypeSymbol, fromImplements, syntaxNodeOpt, diagnostics) : null) ?? namedTypeSymbol.GetCciAdapter();
		}

		// Token: 0x0600615C RID: 24924 RVA: 0x0017747A File Offset: 0x0017567A
		private object GetCciAdapter(Symbol symbol)
		{
			return this._genericInstanceMap.GetOrAdd(symbol, (Symbol s) => s.GetCciAdapter());
		}

		// Token: 0x0600615D RID: 24925 RVA: 0x001774A8 File Offset: 0x001756A8
		private void CheckTupleUnderlyingType(NamedTypeSymbol namedTypeSymbol, SyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics)
		{
			NamedTypeSymbol baseTypeNoUseSiteDiagnostics = namedTypeSymbol.BaseTypeNoUseSiteDiagnostics;
			if (baseTypeNoUseSiteDiagnostics != null && baseTypeNoUseSiteDiagnostics.SpecialType == SpecialType.System_ValueType)
			{
				return;
			}
			if (!this._reportedErrorTypesMap.Add(namedTypeSymbol))
			{
				return;
			}
			Location location = ((syntaxNodeOpt == null) ? NoLocation.Singleton : syntaxNodeOpt.Location);
			if (baseTypeNoUseSiteDiagnostics != null)
			{
				DiagnosticInfo diagnosticInfo = baseTypeNoUseSiteDiagnostics.GetUseSiteInfo().DiagnosticInfo;
				if (diagnosticInfo != null && diagnosticInfo.Severity == DiagnosticSeverity.Error)
				{
					diagnostics.Add(diagnosticInfo, location);
					return;
				}
			}
			diagnostics.Add(new CSDiagnostic(new CSDiagnosticInfo(ErrorCode.ERR_PredefinedValueTupleTypeMustBeStruct, new object[] { namedTypeSymbol.MetadataName }), location, false));
		}

		// Token: 0x0600615E RID: 24926 RVA: 0x00177533 File Offset: 0x00175733
		public static bool IsGenericType(NamedTypeSymbol toCheck)
		{
			while (toCheck != null)
			{
				if (toCheck.Arity > 0)
				{
					return true;
				}
				toCheck = toCheck.ContainingType;
			}
			return false;
		}

		// Token: 0x0600615F RID: 24927 RVA: 0x0017754E File Offset: 0x0017574E
		internal static IGenericParameterReference Translate(TypeParameterSymbol param)
		{
			if (!param.IsDefinition)
			{
				throw new InvalidOperationException(string.Format(CSharpResources.GenericParameterDefinition, param.Name));
			}
			return param.GetCciAdapter();
		}

		// Token: 0x06006160 RID: 24928 RVA: 0x00177574 File Offset: 0x00175774
		internal sealed override ITypeReference Translate(TypeSymbol typeSymbol, SyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics)
		{
			SymbolKind kind = typeSymbol.Kind;
			if (kind <= SymbolKind.NamedType)
			{
				switch (kind)
				{
				case SymbolKind.ArrayType:
					return this.Translate((ArrayTypeSymbol)typeSymbol);
				case SymbolKind.Assembly:
					goto IL_008F;
				case SymbolKind.DynamicType:
					return this.Translate((DynamicTypeSymbol)typeSymbol, syntaxNodeOpt, diagnostics);
				case SymbolKind.ErrorType:
					break;
				default:
					if (kind != SymbolKind.NamedType)
					{
						goto IL_008F;
					}
					break;
				}
				return this.Translate((NamedTypeSymbol)typeSymbol, syntaxNodeOpt, diagnostics, false, false);
			}
			if (kind == SymbolKind.PointerType)
			{
				return this.Translate((PointerTypeSymbol)typeSymbol);
			}
			if (kind == SymbolKind.TypeParameter)
			{
				return PEModuleBuilder.Translate((TypeParameterSymbol)typeSymbol);
			}
			if (kind == SymbolKind.FunctionPointerType)
			{
				return this.Translate((FunctionPointerTypeSymbol)typeSymbol);
			}
			IL_008F:
			throw ExceptionUtilities.UnexpectedValue(typeSymbol.Kind);
		}

		// Token: 0x06006161 RID: 24929 RVA: 0x00177620 File Offset: 0x00175820
		internal IFieldReference Translate(FieldSymbol fieldSymbol, SyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics, bool needDeclaration = false)
		{
			if (!fieldSymbol.IsDefinition)
			{
				return (IFieldReference)this.GetCciAdapter(fieldSymbol);
			}
			if (needDeclaration || !PEModuleBuilder.IsGenericType(fieldSymbol.ContainingType))
			{
				EmbeddedTypesManager embeddedTypesManagerOpt = this._embeddedTypesManagerOpt;
				return ((embeddedTypesManagerOpt != null) ? embeddedTypesManagerOpt.EmbedFieldIfNeedTo(fieldSymbol.GetCciAdapter(), syntaxNodeOpt, diagnostics) : null) ?? fieldSymbol.GetCciAdapter();
			}
			object obj;
			if (this._genericInstanceMap.TryGetValue(fieldSymbol, ref obj))
			{
				return (IFieldReference)obj;
			}
			IFieldReference fieldReference = new SpecializedFieldReference(fieldSymbol);
			return (IFieldReference)this._genericInstanceMap.GetOrAdd(fieldSymbol, fieldReference);
		}

		// Token: 0x06006162 RID: 24930 RVA: 0x001776AC File Offset: 0x001758AC
		public static TypeMemberVisibility MemberVisibility(Symbol symbol)
		{
			switch (symbol.DeclaredAccessibility)
			{
			case Accessibility.Private:
			{
				NamedTypeSymbol containingType = symbol.ContainingType;
				if (containingType != null && containingType.TypeKind == TypeKind.Submission)
				{
					return TypeMemberVisibility.Public;
				}
				return TypeMemberVisibility.Private;
			}
			case Accessibility.ProtectedAndInternal:
				return TypeMemberVisibility.FamilyAndAssembly;
			case Accessibility.Protected:
				if (symbol.ContainingType.TypeKind == TypeKind.Submission)
				{
					return TypeMemberVisibility.Public;
				}
				return TypeMemberVisibility.Family;
			case Accessibility.Internal:
				if (symbol.ContainingAssembly.IsInteractive)
				{
					return TypeMemberVisibility.Public;
				}
				return TypeMemberVisibility.Assembly;
			case Accessibility.ProtectedOrInternal:
				if (symbol.ContainingAssembly.IsInteractive)
				{
					return TypeMemberVisibility.Public;
				}
				return TypeMemberVisibility.FamilyOrAssembly;
			case Accessibility.Public:
				return TypeMemberVisibility.Public;
			default:
				throw ExceptionUtilities.UnexpectedValue(symbol.DeclaredAccessibility);
			}
		}

		// Token: 0x06006163 RID: 24931 RVA: 0x00177747 File Offset: 0x00175947
		internal sealed override IMethodReference Translate(MethodSymbol symbol, DiagnosticBag diagnostics, bool needDeclaration)
		{
			return this.Translate(symbol, null, diagnostics, null, needDeclaration);
		}

		// Token: 0x06006164 RID: 24932 RVA: 0x00177754 File Offset: 0x00175954
		internal IMethodReference Translate(MethodSymbol methodSymbol, SyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics, BoundArgListOperator optArgList = null, bool needDeclaration = false)
		{
			IMethodReference methodReference = this.Translate(methodSymbol, syntaxNodeOpt, diagnostics, needDeclaration);
			if (optArgList != null && optArgList.Arguments.Length > 0)
			{
				IParameterTypeInformation[] array = new IParameterTypeInformation[optArgList.Arguments.Length];
				int num = methodSymbol.ParameterCount;
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new ArgListParameterTypeInformation(num, !optArgList.ArgumentRefKindsOpt.IsDefaultOrEmpty && optArgList.ArgumentRefKindsOpt[i] > RefKind.None, this.Translate(optArgList.Arguments[i].Type, syntaxNodeOpt, diagnostics));
					num++;
				}
				return new ExpandedVarargsMethodReference(methodReference, array.AsImmutableOrNull<IParameterTypeInformation>());
			}
			return methodReference;
		}

		// Token: 0x06006165 RID: 24933 RVA: 0x0017781C File Offset: 0x00175A1C
		private IMethodReference Translate(MethodSymbol methodSymbol, SyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics, bool needDeclaration)
		{
			NamedTypeSymbol containingType = methodSymbol.ContainingType;
			if (containingType.IsAnonymousType)
			{
				methodSymbol = AnonymousTypeManager.TranslateAnonymousTypeMethodSymbol(methodSymbol);
			}
			if (!methodSymbol.IsDefinition)
			{
				return (IMethodReference)this.GetCciAdapter(methodSymbol);
			}
			if (!needDeclaration)
			{
				bool isGenericMethod = methodSymbol.IsGenericMethod;
				bool flag = PEModuleBuilder.IsGenericType(containingType);
				if (isGenericMethod || flag)
				{
					object obj;
					if (this._genericInstanceMap.TryGetValue(methodSymbol, ref obj))
					{
						return (IMethodReference)obj;
					}
					IMethodReference methodReference;
					if (isGenericMethod)
					{
						if (flag)
						{
							methodReference = new SpecializedGenericMethodInstanceReference(methodSymbol);
						}
						else
						{
							methodReference = new GenericMethodInstanceReference(methodSymbol);
						}
					}
					else
					{
						methodReference = new SpecializedMethodReference(methodSymbol);
					}
					return (IMethodReference)this._genericInstanceMap.GetOrAdd(methodSymbol, methodReference);
				}
				else
				{
					NativeIntegerMethodSymbol nativeIntegerMethodSymbol = methodSymbol as NativeIntegerMethodSymbol;
					if (nativeIntegerMethodSymbol != null)
					{
						MethodSymbol underlyingMethod = nativeIntegerMethodSymbol.UnderlyingMethod;
						if (underlyingMethod != null)
						{
							methodSymbol = underlyingMethod;
						}
					}
				}
			}
			if (this._embeddedTypesManagerOpt != null)
			{
				return this._embeddedTypesManagerOpt.EmbedMethodIfNeedTo(methodSymbol.GetCciAdapter(), syntaxNodeOpt, diagnostics);
			}
			return methodSymbol.GetCciAdapter();
		}

		// Token: 0x06006166 RID: 24934 RVA: 0x001778FC File Offset: 0x00175AFC
		internal IMethodReference TranslateOverriddenMethodReference(MethodSymbol methodSymbol, CSharpSyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics)
		{
			IMethodReference methodReference;
			if (PEModuleBuilder.IsGenericType(methodSymbol.ContainingType))
			{
				if (methodSymbol.IsDefinition)
				{
					object obj;
					if (this._genericInstanceMap.TryGetValue(methodSymbol, ref obj))
					{
						methodReference = (IMethodReference)obj;
					}
					else
					{
						methodReference = new SpecializedMethodReference(methodSymbol);
						methodReference = (IMethodReference)this._genericInstanceMap.GetOrAdd(methodSymbol, methodReference);
					}
				}
				else
				{
					methodReference = new SpecializedMethodReference(methodSymbol);
				}
			}
			else if (this._embeddedTypesManagerOpt != null)
			{
				methodReference = this._embeddedTypesManagerOpt.EmbedMethodIfNeedTo(methodSymbol.GetCciAdapter(), syntaxNodeOpt, diagnostics);
			}
			else
			{
				methodReference = methodSymbol.GetCciAdapter();
			}
			return methodReference;
		}

		// Token: 0x06006167 RID: 24935 RVA: 0x00177982 File Offset: 0x00175B82
		internal ImmutableArray<IParameterTypeInformation> Translate(ImmutableArray<ParameterSymbol> @params)
		{
			if (!@params.Any<ParameterSymbol>() || !PEModuleBuilder.MustBeWrapped(@params.First<ParameterSymbol>()))
			{
				return StaticCast<IParameterTypeInformation>.From<ParameterSymbol>(@params);
			}
			return this.TranslateAll(@params);
		}

		// Token: 0x06006168 RID: 24936 RVA: 0x001779AA File Offset: 0x00175BAA
		private static bool MustBeWrapped(ParameterSymbol param)
		{
			return param.IsDefinition && PEModuleBuilder.ContainerIsGeneric(param.ContainingSymbol);
		}

		// Token: 0x06006169 RID: 24937 RVA: 0x001779C4 File Offset: 0x00175BC4
		private ImmutableArray<IParameterTypeInformation> TranslateAll(ImmutableArray<ParameterSymbol> @params)
		{
			ArrayBuilder<IParameterTypeInformation> instance = ArrayBuilder<IParameterTypeInformation>.GetInstance();
			foreach (ParameterSymbol parameterSymbol in @params)
			{
				instance.Add(this.CreateParameterTypeInformationWrapper(parameterSymbol));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x0600616A RID: 24938 RVA: 0x00177A08 File Offset: 0x00175C08
		private IParameterTypeInformation CreateParameterTypeInformationWrapper(ParameterSymbol param)
		{
			object obj;
			if (this._genericInstanceMap.TryGetValue(param, ref obj))
			{
				return (IParameterTypeInformation)obj;
			}
			IParameterTypeInformation parameterTypeInformation = new ParameterTypeInformation(param);
			return (IParameterTypeInformation)this._genericInstanceMap.GetOrAdd(param, parameterTypeInformation);
		}

		// Token: 0x0600616B RID: 24939 RVA: 0x00177A47 File Offset: 0x00175C47
		private static bool ContainerIsGeneric(Symbol container)
		{
			return (container.Kind == SymbolKind.Method && ((MethodSymbol)container).IsGenericMethod) || PEModuleBuilder.IsGenericType(container.ContainingType);
		}

		// Token: 0x0600616C RID: 24940 RVA: 0x00177A6D File Offset: 0x00175C6D
		internal ITypeReference Translate(DynamicTypeSymbol symbol, SyntaxNode syntaxNodeOpt, DiagnosticBag diagnostics)
		{
			return this.GetSpecialType(SpecialType.System_Object, syntaxNodeOpt, diagnostics);
		}

		// Token: 0x0600616D RID: 24941 RVA: 0x00177A78 File Offset: 0x00175C78
		internal IArrayTypeReference Translate(ArrayTypeSymbol symbol)
		{
			return (IArrayTypeReference)this.GetCciAdapter(symbol);
		}

		// Token: 0x0600616E RID: 24942 RVA: 0x00177A86 File Offset: 0x00175C86
		internal IPointerTypeReference Translate(PointerTypeSymbol symbol)
		{
			return (IPointerTypeReference)this.GetCciAdapter(symbol);
		}

		// Token: 0x0600616F RID: 24943 RVA: 0x00177A94 File Offset: 0x00175C94
		internal IFunctionPointerTypeReference Translate(FunctionPointerTypeSymbol symbol)
		{
			return (IFunctionPointerTypeReference)this.GetCciAdapter(symbol);
		}

		// Token: 0x06006170 RID: 24944 RVA: 0x00177AA4 File Offset: 0x00175CA4
		public NamedTypeSymbol SetFixedImplementationType(SourceMemberFieldSymbol field)
		{
			if (this._fixedImplementationTypes == null)
			{
				Interlocked.CompareExchange<Dictionary<FieldSymbol, NamedTypeSymbol>>(ref this._fixedImplementationTypes, new Dictionary<FieldSymbol, NamedTypeSymbol>(), null);
			}
			Dictionary<FieldSymbol, NamedTypeSymbol> fixedImplementationTypes = this._fixedImplementationTypes;
			NamedTypeSymbol namedTypeSymbol2;
			lock (fixedImplementationTypes)
			{
				NamedTypeSymbol namedTypeSymbol;
				if (this._fixedImplementationTypes.TryGetValue(field, ref namedTypeSymbol))
				{
					namedTypeSymbol2 = namedTypeSymbol;
				}
				else
				{
					namedTypeSymbol = new FixedFieldImplementationType(field);
					this._fixedImplementationTypes.Add(field, namedTypeSymbol);
					base.AddSynthesizedDefinition(namedTypeSymbol.ContainingType, namedTypeSymbol.GetCciAdapter());
					namedTypeSymbol2 = namedTypeSymbol;
				}
			}
			return namedTypeSymbol2;
		}

		// Token: 0x06006171 RID: 24945 RVA: 0x00177B38 File Offset: 0x00175D38
		internal NamedTypeSymbol GetFixedImplementationType(FieldSymbol field)
		{
			NamedTypeSymbol namedTypeSymbol;
			this._fixedImplementationTypes.TryGetValue(field, ref namedTypeSymbol);
			return namedTypeSymbol;
		}

		// Token: 0x06006172 RID: 24946 RVA: 0x00177B55 File Offset: 0x00175D55
		protected override IMethodDefinition CreatePrivateImplementationDetailsStaticConstructor(PrivateImplementationDetails details, SyntaxNode syntaxOpt, DiagnosticBag diagnostics)
		{
			return new SynthesizedPrivateImplementationDetailsStaticConstructor(this.SourceModule, details, this.GetUntranslatedSpecialType(SpecialType.System_Void, syntaxOpt, diagnostics)).GetCciAdapter();
		}

		// Token: 0x06006173 RID: 24947
		internal abstract SynthesizedAttributeData SynthesizeEmbeddedAttribute();

		// Token: 0x06006174 RID: 24948 RVA: 0x00177B71 File Offset: 0x00175D71
		internal SynthesizedAttributeData SynthesizeIsReadOnlyAttribute(Symbol symbol)
		{
			if (this.Compilation.SourceModule != symbol.ContainingModule)
			{
				return null;
			}
			return this.TrySynthesizeIsReadOnlyAttribute();
		}

		// Token: 0x06006175 RID: 24949 RVA: 0x00177B8E File Offset: 0x00175D8E
		internal SynthesizedAttributeData SynthesizeIsUnmanagedAttribute(Symbol symbol)
		{
			if (this.Compilation.SourceModule != symbol.ContainingModule)
			{
				return null;
			}
			return this.TrySynthesizeIsUnmanagedAttribute();
		}

		// Token: 0x06006176 RID: 24950 RVA: 0x00177BAB File Offset: 0x00175DAB
		internal SynthesizedAttributeData SynthesizeIsByRefLikeAttribute(Symbol symbol)
		{
			if (this.Compilation.SourceModule != symbol.ContainingModule)
			{
				return null;
			}
			return this.TrySynthesizeIsByRefLikeAttribute();
		}

		// Token: 0x06006177 RID: 24951 RVA: 0x00177BC8 File Offset: 0x00175DC8
		internal SynthesizedAttributeData SynthesizeNullableAttributeIfNecessary(Symbol symbol, byte? nullableContextValue, TypeWithAnnotations type)
		{
			if (this.Compilation.SourceModule != symbol.ContainingModule)
			{
				return null;
			}
			ArrayBuilder<byte> instance = ArrayBuilder<byte>.GetInstance();
			type.AddNullableTransforms(instance);
			SynthesizedAttributeData synthesizedAttributeData;
			if (!instance.Any())
			{
				synthesizedAttributeData = null;
			}
			else
			{
				byte? commonValue = MostCommonNullableValueBuilder.GetCommonValue(instance);
				if (commonValue != null)
				{
					synthesizedAttributeData = this.SynthesizeNullableAttributeIfNecessary(nullableContextValue, commonValue.GetValueOrDefault());
				}
				else
				{
					NamedTypeSymbol specialType = this.Compilation.GetSpecialType(SpecialType.System_Byte);
					ArrayTypeSymbol arrayTypeSymbol = ArrayTypeSymbol.CreateSZArray(specialType.ContainingAssembly, TypeWithAnnotations.Create(specialType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)));
					ImmutableArray<TypedConstant> immutableArray = instance.SelectAsArray((byte flag, NamedTypeSymbol byteType) => new TypedConstant(byteType, TypedConstantKind.Primitive, flag), specialType);
					synthesizedAttributeData = this.SynthesizeNullableAttribute(WellKnownMember.System_Runtime_CompilerServices_NullableAttribute__ctorTransformFlags, ImmutableArray.Create<TypedConstant>(new TypedConstant(arrayTypeSymbol, immutableArray)));
				}
			}
			instance.Free();
			return synthesizedAttributeData;
		}

		// Token: 0x06006178 RID: 24952 RVA: 0x00177CA0 File Offset: 0x00175EA0
		internal SynthesizedAttributeData SynthesizeNullableAttributeIfNecessary(byte? nullableContextValue, byte nullableValue)
		{
			byte? b = nullableContextValue;
			int? num = ((b != null) ? new int?((int)b.GetValueOrDefault()) : default(int?));
			if ((((int)nullableValue == num.GetValueOrDefault()) & (num != null)) || (nullableContextValue == null && nullableValue == 0))
			{
				return null;
			}
			NamedTypeSymbol specialType = this.Compilation.GetSpecialType(SpecialType.System_Byte);
			return this.SynthesizeNullableAttribute(WellKnownMember.System_Runtime_CompilerServices_NullableAttribute__ctorByte, ImmutableArray.Create<TypedConstant>(new TypedConstant(specialType, TypedConstantKind.Primitive, nullableValue)));
		}

		// Token: 0x06006179 RID: 24953 RVA: 0x00177D20 File Offset: 0x00175F20
		internal virtual SynthesizedAttributeData SynthesizeNullableAttribute(WellKnownMember member, ImmutableArray<TypedConstant> arguments)
		{
			return this.Compilation.TrySynthesizeAttribute(member, arguments, default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), true);
		}

		// Token: 0x0600617A RID: 24954 RVA: 0x00177D44 File Offset: 0x00175F44
		internal SynthesizedAttributeData SynthesizeNullableContextAttribute(Symbol symbol, byte value)
		{
			ModuleSymbol sourceModule = this.Compilation.SourceModule;
			if (sourceModule != symbol && sourceModule != symbol.ContainingModule)
			{
				return null;
			}
			return this.SynthesizeNullableContextAttribute(ImmutableArray.Create<TypedConstant>(new TypedConstant(this.Compilation.GetSpecialType(SpecialType.System_Byte), TypedConstantKind.Primitive, value)));
		}

		// Token: 0x0600617B RID: 24955 RVA: 0x00177D90 File Offset: 0x00175F90
		internal virtual SynthesizedAttributeData SynthesizeNullableContextAttribute(ImmutableArray<TypedConstant> arguments)
		{
			return this.Compilation.TrySynthesizeAttribute(WellKnownMember.System_Runtime_CompilerServices_NullableContextAttribute__ctor, arguments, default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), true);
		}

		// Token: 0x0600617C RID: 24956 RVA: 0x00177DB8 File Offset: 0x00175FB8
		internal SynthesizedAttributeData SynthesizePreserveBaseOverridesAttribute()
		{
			return this.Compilation.TrySynthesizeAttribute(SpecialMember.System_Runtime_CompilerServices_PreserveBaseOverridesAttribute__ctor, true);
		}

		// Token: 0x0600617D RID: 24957 RVA: 0x00177DC8 File Offset: 0x00175FC8
		internal SynthesizedAttributeData SynthesizeNativeIntegerAttribute(Symbol symbol, TypeSymbol type)
		{
			if (this.Compilation.SourceModule != symbol.ContainingModule)
			{
				return null;
			}
			ArrayBuilder<bool> instance = ArrayBuilder<bool>.GetInstance();
			CSharpCompilation.NativeIntegerTransformsEncoder.Encode(instance, type);
			SynthesizedAttributeData synthesizedAttributeData;
			if (instance.Count == 1 && instance[0])
			{
				synthesizedAttributeData = this.SynthesizeNativeIntegerAttribute(WellKnownMember.System_Runtime_CompilerServices_NativeIntegerAttribute__ctor, ImmutableArray<TypedConstant>.Empty);
			}
			else
			{
				NamedTypeSymbol specialType = this.Compilation.GetSpecialType(SpecialType.System_Boolean);
				ImmutableArray<TypedConstant> immutableArray = instance.SelectAsArray((bool flag, NamedTypeSymbol constantType) => new TypedConstant(constantType, TypedConstantKind.Primitive, flag), specialType);
				ImmutableArray<TypedConstant> immutableArray2 = ImmutableArray.Create<TypedConstant>(new TypedConstant(ArrayTypeSymbol.CreateSZArray(specialType.ContainingAssembly, TypeWithAnnotations.Create(specialType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>))), immutableArray));
				synthesizedAttributeData = this.SynthesizeNativeIntegerAttribute(WellKnownMember.System_Runtime_CompilerServices_NativeIntegerAttribute__ctorTransformFlags, immutableArray2);
			}
			instance.Free();
			return synthesizedAttributeData;
		}

		// Token: 0x0600617E RID: 24958 RVA: 0x00177E90 File Offset: 0x00176090
		internal virtual SynthesizedAttributeData SynthesizeNativeIntegerAttribute(WellKnownMember member, ImmutableArray<TypedConstant> arguments)
		{
			return this.Compilation.TrySynthesizeAttribute(member, arguments, default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), true);
		}

		// Token: 0x0600617F RID: 24959 RVA: 0x00177EB4 File Offset: 0x001760B4
		internal bool ShouldEmitNullablePublicOnlyAttribute()
		{
			return this.Compilation.GetUsesNullableAttributes() && this.Compilation.EmitNullablePublicOnly;
		}

		// Token: 0x06006180 RID: 24960 RVA: 0x00177ED0 File Offset: 0x001760D0
		internal virtual SynthesizedAttributeData SynthesizeNullablePublicOnlyAttribute(ImmutableArray<TypedConstant> arguments)
		{
			return this.Compilation.TrySynthesizeAttribute(WellKnownMember.System_Runtime_CompilerServices_NullablePublicOnlyAttribute__ctor, arguments, default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false);
		}

		// Token: 0x06006181 RID: 24961 RVA: 0x00177EF8 File Offset: 0x001760F8
		protected virtual SynthesizedAttributeData TrySynthesizeIsReadOnlyAttribute()
		{
			return this.Compilation.TrySynthesizeAttribute(WellKnownMember.System_Runtime_CompilerServices_IsReadOnlyAttribute__ctor, default(ImmutableArray<TypedConstant>), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false);
		}

		// Token: 0x06006182 RID: 24962 RVA: 0x00177F28 File Offset: 0x00176128
		protected virtual SynthesizedAttributeData TrySynthesizeIsUnmanagedAttribute()
		{
			return this.Compilation.TrySynthesizeAttribute(WellKnownMember.System_Runtime_CompilerServices_IsUnmanagedAttribute__ctor, default(ImmutableArray<TypedConstant>), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false);
		}

		// Token: 0x06006183 RID: 24963 RVA: 0x00177F58 File Offset: 0x00176158
		protected virtual SynthesizedAttributeData TrySynthesizeIsByRefLikeAttribute()
		{
			return this.Compilation.TrySynthesizeAttribute(WellKnownMember.System_Runtime_CompilerServices_IsByRefLikeAttribute__ctor, default(ImmutableArray<TypedConstant>), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false);
		}

		// Token: 0x06006184 RID: 24964 RVA: 0x00177F88 File Offset: 0x00176188
		private void EnsureEmbeddableAttributeExists(EmbeddableAttributes attribute)
		{
			if ((this.GetNeedsGeneratedAttributesInternal() & attribute) != (EmbeddableAttributes)0)
			{
				return;
			}
			if (this.Compilation.CheckIfAttributeShouldBeEmbedded(attribute, null, null))
			{
				this.SetNeedsGeneratedAttributes(attribute);
			}
		}

		// Token: 0x06006185 RID: 24965 RVA: 0x00177FAC File Offset: 0x001761AC
		internal void EnsureIsReadOnlyAttributeExists()
		{
			this.EnsureEmbeddableAttributeExists(EmbeddableAttributes.IsReadOnlyAttribute);
		}

		// Token: 0x06006186 RID: 24966 RVA: 0x00177FB5 File Offset: 0x001761B5
		internal void EnsureIsUnmanagedAttributeExists()
		{
			this.EnsureEmbeddableAttributeExists(EmbeddableAttributes.IsUnmanagedAttribute);
		}

		// Token: 0x06006187 RID: 24967 RVA: 0x00177FBE File Offset: 0x001761BE
		internal void EnsureNullableAttributeExists()
		{
			this.EnsureEmbeddableAttributeExists(EmbeddableAttributes.NullableAttribute);
		}

		// Token: 0x06006188 RID: 24968 RVA: 0x00177FC7 File Offset: 0x001761C7
		internal void EnsureNativeIntegerAttributeExists()
		{
			this.EnsureEmbeddableAttributeExists(EmbeddableAttributes.NativeIntegerAttribute);
		}

		// Token: 0x06006189 RID: 24969 RVA: 0x00177FD1 File Offset: 0x001761D1
		public override IEnumerable<INamespaceTypeDefinition> GetAdditionalTopLevelTypeDefinitions(EmitContext context)
		{
			return this.GetAdditionalTopLevelTypes();
		}

		// Token: 0x0600618A RID: 24970 RVA: 0x00177FDE File Offset: 0x001761DE
		public override IEnumerable<INamespaceTypeDefinition> GetEmbeddedTypeDefinitions(EmitContext context)
		{
			return this.GetEmbeddedTypes(context.Diagnostics);
		}

		// Token: 0x0600618B RID: 24971 RVA: 0x00177FF1 File Offset: 0x001761F1
		public sealed override ImmutableArray<NamedTypeSymbol> GetEmbeddedTypes(DiagnosticBag diagnostics)
		{
			return this.GetEmbeddedTypes(new BindingDiagnosticBag(diagnostics));
		}

		// Token: 0x0600618C RID: 24972 RVA: 0x00177FFF File Offset: 0x001761FF
		internal virtual ImmutableArray<NamedTypeSymbol> GetEmbeddedTypes(BindingDiagnosticBag diagnostics)
		{
			return base.GetEmbeddedTypes(diagnostics.DiagnosticBag);
		}

		// Token: 0x0400196C RID: 6508
		protected readonly ConcurrentDictionary<Symbol, IModuleReference> AssemblyOrModuleSymbolToModuleRefMap = new ConcurrentDictionary<Symbol, IModuleReference>();

		// Token: 0x0400196D RID: 6509
		private readonly ConcurrentDictionary<Symbol, object> _genericInstanceMap = new ConcurrentDictionary<Symbol, object>(SymbolEqualityComparer.ConsiderEverything);

		// Token: 0x0400196E RID: 6510
		private readonly ConcurrentSet<TypeSymbol> _reportedErrorTypesMap = new ConcurrentSet<TypeSymbol>();

		// Token: 0x0400196F RID: 6511
		private readonly EmbeddedTypesManager _embeddedTypesManagerOpt;

		// Token: 0x04001970 RID: 6512
		private readonly string _metadataName;

		// Token: 0x04001971 RID: 6513
		private ImmutableArray<ExportedType> _lazyExportedTypes;

		// Token: 0x04001972 RID: 6514
		private Dictionary<FieldSymbol, NamedTypeSymbol> _fixedImplementationTypes;

		// Token: 0x04001973 RID: 6515
		private int _needsGeneratedAttributes;

		// Token: 0x04001974 RID: 6516
		private bool _needsGeneratedAttributes_IsFrozen;
	}
}
