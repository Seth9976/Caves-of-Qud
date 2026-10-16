using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000547 RID: 1351
	internal sealed class SourceNamespaceSymbol : NamespaceSymbol
	{
		// Token: 0x0600723B RID: 29243 RVA: 0x001AA3A8 File Offset: 0x001A85A8
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public Imports GetImports(CSharpSyntaxNode declarationSyntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved)
		{
			CompilationUnitSyntax compilationUnitSyntax = declarationSyntax as CompilationUnitSyntax;
			if (compilationUnitSyntax == null)
			{
				NamespaceDeclarationSyntax namespaceDeclarationSyntax = declarationSyntax as NamespaceDeclarationSyntax;
				if (namespaceDeclarationSyntax == null)
				{
					throw ExceptionUtilities.UnexpectedValue(declarationSyntax);
				}
				if (!namespaceDeclarationSyntax.Externs.Any() && !namespaceDeclarationSyntax.Usings.Any())
				{
					return Imports.Empty;
				}
			}
			else if (!compilationUnitSyntax.Externs.Any() && !compilationUnitSyntax.Usings.Any())
			{
				return this.GetGlobalUsingImports(basesBeingResolved);
			}
			return this.GetAliasesAndUsings(declarationSyntax).GetImports(this, declarationSyntax, basesBeingResolved);
		}

		// Token: 0x0600723C RID: 29244 RVA: 0x001AA42F File Offset: 0x001A862F
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private SourceNamespaceSymbol.AliasesAndUsings GetAliasesAndUsings(CSharpSyntaxNode declarationSyntax)
		{
			return this._aliasesAndUsings[this.GetMatchingNamespaceDeclaration(declarationSyntax)];
		}

		// Token: 0x0600723D RID: 29245 RVA: 0x001AA444 File Offset: 0x001A8644
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private SingleNamespaceDeclaration GetMatchingNamespaceDeclaration(CSharpSyntaxNode declarationSyntax)
		{
			foreach (SingleNamespaceDeclaration singleNamespaceDeclaration in this._mergedDeclaration.Declarations)
			{
				SyntaxReference syntaxReference = singleNamespaceDeclaration.SyntaxReference;
				if (syntaxReference.SyntaxTree == declarationSyntax.SyntaxTree && syntaxReference.GetSyntax(default(CancellationToken)) == declarationSyntax)
				{
					return singleNamespaceDeclaration;
				}
			}
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600723E RID: 29246 RVA: 0x001AA4A8 File Offset: 0x001A86A8
		public ImmutableArray<AliasAndExternAliasDirective> GetExternAliases([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxNode declarationSyntax)
		{
			CompilationUnitSyntax compilationUnitSyntax = declarationSyntax as CompilationUnitSyntax;
			if (compilationUnitSyntax == null)
			{
				NamespaceDeclarationSyntax namespaceDeclarationSyntax = declarationSyntax as NamespaceDeclarationSyntax;
				if (namespaceDeclarationSyntax == null)
				{
					throw ExceptionUtilities.UnexpectedValue(declarationSyntax);
				}
				if (!namespaceDeclarationSyntax.Externs.Any())
				{
					return ImmutableArray<AliasAndExternAliasDirective>.Empty;
				}
			}
			else if (!compilationUnitSyntax.Externs.Any())
			{
				return ImmutableArray<AliasAndExternAliasDirective>.Empty;
			}
			return this.GetAliasesAndUsings(declarationSyntax).GetExternAliases(this, declarationSyntax);
		}

		// Token: 0x0600723F RID: 29247 RVA: 0x001AA50C File Offset: 0x001A870C
		public ImmutableArray<AliasAndUsingDirective> GetUsingAliases([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxNode declarationSyntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved)
		{
			CompilationUnitSyntax compilationUnitSyntax = declarationSyntax as CompilationUnitSyntax;
			if (compilationUnitSyntax == null)
			{
				NamespaceDeclarationSyntax namespaceDeclarationSyntax = declarationSyntax as NamespaceDeclarationSyntax;
				if (namespaceDeclarationSyntax == null)
				{
					throw ExceptionUtilities.UnexpectedValue(declarationSyntax);
				}
				if (!namespaceDeclarationSyntax.Usings.Any())
				{
					return ImmutableArray<AliasAndUsingDirective>.Empty;
				}
			}
			else if (!compilationUnitSyntax.Usings.Any())
			{
				return ImmutableArray<AliasAndUsingDirective>.Empty;
			}
			return this.GetAliasesAndUsings(declarationSyntax).GetUsingAliases(this, declarationSyntax, basesBeingResolved);
		}

		// Token: 0x06007240 RID: 29248 RVA: 0x001AA574 File Offset: 0x001A8774
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public ImmutableDictionary<string, AliasAndUsingDirective> GetUsingAliasesMap(CSharpSyntaxNode declarationSyntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved)
		{
			CompilationUnitSyntax compilationUnitSyntax = declarationSyntax as CompilationUnitSyntax;
			if (compilationUnitSyntax == null)
			{
				NamespaceDeclarationSyntax namespaceDeclarationSyntax = declarationSyntax as NamespaceDeclarationSyntax;
				if (namespaceDeclarationSyntax == null)
				{
					throw ExceptionUtilities.UnexpectedValue(declarationSyntax);
				}
				if (!namespaceDeclarationSyntax.Usings.Any())
				{
					return ImmutableDictionary<string, AliasAndUsingDirective>.Empty;
				}
			}
			else if (!compilationUnitSyntax.Usings.Any())
			{
				return this.GetGlobalUsingAliasesMap(basesBeingResolved);
			}
			return this.GetAliasesAndUsings(declarationSyntax).GetUsingAliasesMap(this, declarationSyntax, basesBeingResolved);
		}

		// Token: 0x06007241 RID: 29249 RVA: 0x001AA5DC File Offset: 0x001A87DC
		public ImmutableArray<NamespaceOrTypeAndUsingDirective> GetUsingNamespacesOrTypes([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxNode declarationSyntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved)
		{
			CompilationUnitSyntax compilationUnitSyntax = declarationSyntax as CompilationUnitSyntax;
			if (compilationUnitSyntax == null)
			{
				NamespaceDeclarationSyntax namespaceDeclarationSyntax = declarationSyntax as NamespaceDeclarationSyntax;
				if (namespaceDeclarationSyntax == null)
				{
					throw ExceptionUtilities.UnexpectedValue(declarationSyntax);
				}
				if (!namespaceDeclarationSyntax.Usings.Any())
				{
					return ImmutableArray<NamespaceOrTypeAndUsingDirective>.Empty;
				}
			}
			else if (!compilationUnitSyntax.Usings.Any())
			{
				return this.GetGlobalUsingNamespacesOrTypes(basesBeingResolved);
			}
			return this.GetAliasesAndUsings(declarationSyntax).GetUsingNamespacesOrTypes(this, declarationSyntax, basesBeingResolved);
		}

		// Token: 0x06007242 RID: 29250 RVA: 0x001AA644 File Offset: 0x001A8844
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private Imports GetGlobalUsingImports([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved)
		{
			return this.GetMergedGlobalAliasesAndUsings(basesBeingResolved, default(CancellationToken)).Imports;
		}

		// Token: 0x06007243 RID: 29251 RVA: 0x001AA668 File Offset: 0x001A8868
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private ImmutableDictionary<string, AliasAndUsingDirective> GetGlobalUsingAliasesMap([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved)
		{
			return this.GetMergedGlobalAliasesAndUsings(basesBeingResolved, default(CancellationToken)).UsingAliasesMap;
		}

		// Token: 0x06007244 RID: 29252 RVA: 0x001AA68C File Offset: 0x001A888C
		private ImmutableArray<NamespaceOrTypeAndUsingDirective> GetGlobalUsingNamespacesOrTypes([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved)
		{
			return this.GetMergedGlobalAliasesAndUsings(basesBeingResolved, default(CancellationToken)).UsingNamespacesOrTypes;
		}

		// Token: 0x06007245 RID: 29253 RVA: 0x001AA6B0 File Offset: 0x001A88B0
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private SourceNamespaceSymbol.MergedGlobalAliasesAndUsings GetMergedGlobalAliasesAndUsings([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (this._lazyMergedGlobalAliasesAndUsings == null)
			{
				if (!this.IsGlobalNamespace)
				{
					this._lazyMergedGlobalAliasesAndUsings = SourceNamespaceSymbol.MergedGlobalAliasesAndUsings.Empty;
				}
				else
				{
					ImmutableDictionary<string, AliasAndUsingDirective> immutableDictionary = null;
					ArrayBuilder<NamespaceOrTypeAndUsingDirective> arrayBuilder = ArrayBuilder<NamespaceOrTypeAndUsingDirective>.GetInstance();
					PooledHashSet<NamespaceOrTypeSymbol> pooledSymbolHashSetInstance = SpecializedSymbolCollections.GetPooledSymbolHashSetInstance<NamespaceOrTypeSymbol>();
					DiagnosticBag diagnosticBag = DiagnosticBag.GetInstance();
					try
					{
						bool flag = false;
						foreach (SingleNamespaceDeclaration singleNamespaceDeclaration in this._mergedDeclaration.Declarations)
						{
							if (singleNamespaceDeclaration.HasExternAliases)
							{
								flag = true;
							}
							if (singleNamespaceDeclaration.HasGlobalUsings)
							{
								ImmutableDictionary<string, AliasAndUsingDirective> globalUsingAliasesMap = this._aliasesAndUsings[singleNamespaceDeclaration].GetGlobalUsingAliasesMap(this, singleNamespaceDeclaration.SyntaxReference, basesBeingResolved);
								cancellationToken.ThrowIfCancellationRequested();
								if (!globalUsingAliasesMap.IsEmpty)
								{
									if (immutableDictionary == null)
									{
										immutableDictionary = globalUsingAliasesMap;
									}
									else
									{
										ImmutableDictionary<string, AliasAndUsingDirective>.Builder builder = immutableDictionary.ToBuilder();
										bool flag2 = false;
										foreach (KeyValuePair<string, AliasAndUsingDirective> keyValuePair in globalUsingAliasesMap)
										{
											if (builder.ContainsKey(keyValuePair.Key))
											{
												diagnosticBag.Add(ErrorCode.ERR_DuplicateAlias, keyValuePair.Value.Alias.Locations[0], new object[] { keyValuePair.Key });
											}
											else
											{
												builder.Add(keyValuePair);
												flag2 = true;
											}
										}
										if (flag2)
										{
											immutableDictionary = builder.ToImmutable();
										}
										cancellationToken.ThrowIfCancellationRequested();
									}
								}
								ImmutableArray<NamespaceOrTypeAndUsingDirective> globalUsingNamespacesOrTypes = this._aliasesAndUsings[singleNamespaceDeclaration].GetGlobalUsingNamespacesOrTypes(this, singleNamespaceDeclaration.SyntaxReference, basesBeingResolved);
								if (!globalUsingNamespacesOrTypes.IsEmpty)
								{
									if (arrayBuilder.Count == 0)
									{
										arrayBuilder.AddRange(globalUsingNamespacesOrTypes);
										pooledSymbolHashSetInstance.AddAll<NamespaceOrTypeSymbol>(globalUsingNamespacesOrTypes.Select<NamespaceOrTypeAndUsingDirective, NamespaceOrTypeSymbol>((NamespaceOrTypeAndUsingDirective n) => n.NamespaceOrType));
									}
									else
									{
										foreach (NamespaceOrTypeAndUsingDirective namespaceOrTypeAndUsingDirective in globalUsingNamespacesOrTypes)
										{
											if (!pooledSymbolHashSetInstance.Add(namespaceOrTypeAndUsingDirective.NamespaceOrType))
											{
												diagnosticBag.Add(ErrorCode.WRN_DuplicateUsing, namespaceOrTypeAndUsingDirective.UsingDirective.Name.Location, new object[] { namespaceOrTypeAndUsingDirective.NamespaceOrType });
											}
											else
											{
												arrayBuilder.Add(namespaceOrTypeAndUsingDirective);
											}
										}
									}
								}
								cancellationToken.ThrowIfCancellationRequested();
							}
						}
						if (flag && immutableDictionary != null)
						{
							foreach (SingleNamespaceDeclaration singleNamespaceDeclaration2 in this._mergedDeclaration.Declarations)
							{
								if (singleNamespaceDeclaration2.HasExternAliases)
								{
									ImmutableArray<AliasAndExternAliasDirective> externAliases = this._aliasesAndUsings[singleNamespaceDeclaration2].GetExternAliases(this, singleNamespaceDeclaration2.SyntaxReference);
									ImmutableDictionary<string, AliasAndUsingDirective> immutableDictionary2 = ImmutableDictionary<string, AliasAndUsingDirective>.Empty;
									if (singleNamespaceDeclaration2.HasGlobalUsings)
									{
										immutableDictionary2 = this._aliasesAndUsings[singleNamespaceDeclaration2].GetGlobalUsingAliasesMap(this, singleNamespaceDeclaration2.SyntaxReference, basesBeingResolved);
									}
									foreach (AliasAndExternAliasDirective aliasAndExternAliasDirective in externAliases)
									{
										if (!aliasAndExternAliasDirective.SkipInLookup && !immutableDictionary2.ContainsKey(aliasAndExternAliasDirective.Alias.Name) && immutableDictionary.ContainsKey(aliasAndExternAliasDirective.Alias.Name))
										{
											diagnosticBag.Add(ErrorCode.ERR_DuplicateAlias, aliasAndExternAliasDirective.Alias.Locations[0], new object[] { aliasAndExternAliasDirective.Alias.Name });
										}
									}
								}
							}
						}
						Interlocked.CompareExchange<SourceNamespaceSymbol.MergedGlobalAliasesAndUsings>(ref this._lazyMergedGlobalAliasesAndUsings, new SourceNamespaceSymbol.MergedGlobalAliasesAndUsings
						{
							UsingAliasesMap = (immutableDictionary ?? ImmutableDictionary<string, AliasAndUsingDirective>.Empty),
							UsingNamespacesOrTypes = arrayBuilder.ToImmutableAndFree(),
							Diagnostics = diagnosticBag.ToReadOnlyAndFree()
						}, null);
						arrayBuilder = null;
						diagnosticBag = null;
					}
					finally
					{
						pooledSymbolHashSetInstance.Free();
						if (arrayBuilder != null)
						{
							arrayBuilder.Free();
						}
						if (diagnosticBag != null)
						{
							diagnosticBag.Free();
						}
					}
				}
			}
			return this._lazyMergedGlobalAliasesAndUsings;
		}

		// Token: 0x06007246 RID: 29254 RVA: 0x001AAA94 File Offset: 0x001A8C94
		internal SourceNamespaceSymbol(SourceModuleSymbol module, Symbol container, MergedNamespaceDeclaration mergedDeclaration, BindingDiagnosticBag diagnostics)
		{
			this._module = module;
			this._container = container;
			this._mergedDeclaration = mergedDeclaration;
			ImmutableDictionary<SingleNamespaceDeclaration, SourceNamespaceSymbol.AliasesAndUsings>.Builder builder = ImmutableDictionary.CreateBuilder<SingleNamespaceDeclaration, SourceNamespaceSymbol.AliasesAndUsings>(ReferenceEqualityComparer.Instance);
			foreach (SingleNamespaceDeclaration singleNamespaceDeclaration in mergedDeclaration.Declarations)
			{
				if (singleNamespaceDeclaration.HasExternAliases || singleNamespaceDeclaration.HasGlobalUsings || singleNamespaceDeclaration.HasUsings)
				{
					builder.Add(singleNamespaceDeclaration, new SourceNamespaceSymbol.AliasesAndUsings());
				}
				diagnostics.AddRange<Diagnostic>(singleNamespaceDeclaration.Diagnostics);
			}
			this._aliasesAndUsings = builder.ToImmutable();
		}

		// Token: 0x17001EE3 RID: 7907
		// (get) Token: 0x06007247 RID: 29255 RVA: 0x001AAB31 File Offset: 0x001A8D31
		internal MergedNamespaceDeclaration MergedDeclaration
		{
			get
			{
				return this._mergedDeclaration;
			}
		}

		// Token: 0x17001EE4 RID: 7908
		// (get) Token: 0x06007248 RID: 29256 RVA: 0x001AAB39 File Offset: 0x001A8D39
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._container;
			}
		}

		// Token: 0x17001EE5 RID: 7909
		// (get) Token: 0x06007249 RID: 29257 RVA: 0x001AAB41 File Offset: 0x001A8D41
		public override AssemblySymbol ContainingAssembly
		{
			get
			{
				return this._module.ContainingAssembly;
			}
		}

		// Token: 0x17001EE6 RID: 7910
		// (get) Token: 0x0600724A RID: 29258 RVA: 0x001AAB4E File Offset: 0x001A8D4E
		public override string Name
		{
			get
			{
				return this._mergedDeclaration.Name;
			}
		}

		// Token: 0x0600724B RID: 29259 RVA: 0x001AAB5B File Offset: 0x001A8D5B
		internal override LexicalSortKey GetLexicalSortKey()
		{
			if (!this._lazyLexicalSortKey.IsInitialized)
			{
				this._lazyLexicalSortKey.SetFrom(this._mergedDeclaration.GetLexicalSortKey(this.DeclaringCompilation));
			}
			return this._lazyLexicalSortKey;
		}

		// Token: 0x17001EE7 RID: 7911
		// (get) Token: 0x0600724C RID: 29260 RVA: 0x001AAB8C File Offset: 0x001A8D8C
		public override ImmutableArray<Location> Locations
		{
			get
			{
				if (this._locations.IsDefault)
				{
					ImmutableInterlocked.InterlockedCompareExchange<Location>(ref this._locations, this._mergedDeclaration.NameLocations, default(ImmutableArray<Location>));
				}
				return this._locations;
			}
		}

		// Token: 0x17001EE8 RID: 7912
		// (get) Token: 0x0600724D RID: 29261 RVA: 0x001AABCC File Offset: 0x001A8DCC
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return this.ComputeDeclaringReferencesCore();
			}
		}

		// Token: 0x0600724E RID: 29262 RVA: 0x001AABD4 File Offset: 0x001A8DD4
		private ImmutableArray<SyntaxReference> ComputeDeclaringReferencesCore()
		{
			return this._mergedDeclaration.Declarations.SelectAsArray<SingleNamespaceDeclaration, SyntaxReference>(SourceNamespaceSymbol.s_declaringSyntaxReferencesSelector);
		}

		// Token: 0x0600724F RID: 29263 RVA: 0x001AABEC File Offset: 0x001A8DEC
		internal override ImmutableArray<Symbol> GetMembersUnordered()
		{
			ImmutableArray<Symbol> immutableArray = this._lazyAllMembers;
			if (immutableArray.IsDefault)
			{
				ImmutableArray<Symbol> immutableArray2 = StaticCast<Symbol>.From<NamespaceOrTypeSymbol>(this.GetNameToMembersMap().Flatten<string, NamespaceOrTypeSymbol>(null));
				ImmutableInterlocked.InterlockedInitialize<Symbol>(ref this._lazyAllMembers, immutableArray2);
				immutableArray = this._lazyAllMembers;
			}
			return immutableArray.ConditionallyDeOrder<Symbol>();
		}

		// Token: 0x06007250 RID: 29264 RVA: 0x001AAC38 File Offset: 0x001A8E38
		public override ImmutableArray<Symbol> GetMembers()
		{
			if ((this._flags & 1) != 0)
			{
				return this._lazyAllMembers;
			}
			ImmutableArray<Symbol> immutableArray = this.GetMembersUnordered();
			if (immutableArray.Length >= 2)
			{
				immutableArray = immutableArray.Sort(LexicalOrderSymbolComparer.Instance);
				ImmutableInterlocked.InterlockedExchange<Symbol>(ref this._lazyAllMembers, immutableArray);
			}
			ThreadSafeFlagOperations.Set(ref this._flags, 1);
			return immutableArray;
		}

		// Token: 0x06007251 RID: 29265 RVA: 0x001AAC90 File Offset: 0x001A8E90
		public override ImmutableArray<Symbol> GetMembers(string name)
		{
			ImmutableArray<NamespaceOrTypeSymbol> immutableArray;
			if (!this.GetNameToMembersMap().TryGetValue(name, ref immutableArray))
			{
				return ImmutableArray<Symbol>.Empty;
			}
			return immutableArray.Cast<NamespaceOrTypeSymbol, Symbol>();
		}

		// Token: 0x06007252 RID: 29266 RVA: 0x001AACBC File Offset: 0x001A8EBC
		internal override ImmutableArray<NamedTypeSymbol> GetTypeMembersUnordered()
		{
			if (this._lazyTypeMembersUnordered.IsDefault)
			{
				ImmutableArray<NamedTypeSymbol> immutableArray = this.GetNameToTypeMembersMap().Flatten<string, NamedTypeSymbol>(null);
				ImmutableInterlocked.InterlockedInitialize<NamedTypeSymbol>(ref this._lazyTypeMembersUnordered, immutableArray);
			}
			return this._lazyTypeMembersUnordered;
		}

		// Token: 0x06007253 RID: 29267 RVA: 0x001AACF6 File Offset: 0x001A8EF6
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers()
		{
			return this.GetNameToTypeMembersMap().Flatten<string, NamedTypeSymbol>(LexicalOrderSymbolComparer.Instance);
		}

		// Token: 0x06007254 RID: 29268 RVA: 0x001AAD08 File Offset: 0x001A8F08
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name)
		{
			ImmutableArray<NamedTypeSymbol> immutableArray;
			if (!this.GetNameToTypeMembersMap().TryGetValue(name, ref immutableArray))
			{
				return ImmutableArray<NamedTypeSymbol>.Empty;
			}
			return immutableArray;
		}

		// Token: 0x06007255 RID: 29269 RVA: 0x001AAD2C File Offset: 0x001A8F2C
		public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name, int arity)
		{
			return this.GetTypeMembers(name).WhereAsArray((NamedTypeSymbol s, int arity) => s.Arity == arity, arity);
		}

		// Token: 0x17001EE9 RID: 7913
		// (get) Token: 0x06007256 RID: 29270 RVA: 0x001AAD5A File Offset: 0x001A8F5A
		internal override ModuleSymbol ContainingModule
		{
			get
			{
				return this._module;
			}
		}

		// Token: 0x17001EEA RID: 7914
		// (get) Token: 0x06007257 RID: 29271 RVA: 0x001AAD62 File Offset: 0x001A8F62
		internal override NamespaceExtent Extent
		{
			get
			{
				return new NamespaceExtent(this._module);
			}
		}

		// Token: 0x06007258 RID: 29272 RVA: 0x001AAD70 File Offset: 0x001A8F70
		private Dictionary<string, ImmutableArray<NamespaceOrTypeSymbol>> GetNameToMembersMap()
		{
			if (this._nameToMembersMap == null)
			{
				BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
				if (Interlocked.CompareExchange<Dictionary<string, ImmutableArray<NamespaceOrTypeSymbol>>>(ref this._nameToMembersMap, this.MakeNameToMembersMap(instance), null) == null)
				{
					this.AddDeclarationDiagnostics(instance);
					this.RegisterDeclaredCorTypes();
					this.DeclaringCompilation.SymbolDeclaredEvent(this);
					this._state.NotePartComplete(CompletionPart.Members);
				}
				instance.Free();
			}
			return this._nameToMembersMap;
		}

		// Token: 0x06007259 RID: 29273 RVA: 0x001AADD6 File Offset: 0x001A8FD6
		private Dictionary<string, ImmutableArray<NamedTypeSymbol>> GetNameToTypeMembersMap()
		{
			if (this._nameToTypeMembersMap == null)
			{
				Interlocked.CompareExchange<Dictionary<string, ImmutableArray<NamedTypeSymbol>>>(ref this._nameToTypeMembersMap, SourceNamespaceSymbol.GetTypesFromMemberMap(this.GetNameToMembersMap()), null);
			}
			return this._nameToTypeMembersMap;
		}

		// Token: 0x0600725A RID: 29274 RVA: 0x001AAE00 File Offset: 0x001A9000
		private static Dictionary<string, ImmutableArray<NamedTypeSymbol>> GetTypesFromMemberMap(Dictionary<string, ImmutableArray<NamespaceOrTypeSymbol>> map)
		{
			Dictionary<string, ImmutableArray<NamedTypeSymbol>> dictionary = new Dictionary<string, ImmutableArray<NamedTypeSymbol>>(StringOrdinalComparer.Instance);
			foreach (KeyValuePair<string, ImmutableArray<NamespaceOrTypeSymbol>> keyValuePair in map)
			{
				ImmutableArray<NamespaceOrTypeSymbol> value = keyValuePair.Value;
				bool flag = false;
				bool flag2 = false;
				ImmutableArray<NamespaceOrTypeSymbol>.Enumerator enumerator2 = value.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.Kind == SymbolKind.NamedType)
					{
						flag = true;
						if (flag2)
						{
							break;
						}
					}
					else
					{
						flag2 = true;
						if (flag)
						{
							break;
						}
					}
				}
				if (flag)
				{
					if (flag2)
					{
						dictionary.Add(keyValuePair.Key, value.OfType<NamedTypeSymbol>().AsImmutable<NamedTypeSymbol>());
					}
					else
					{
						dictionary.Add(keyValuePair.Key, value.As<NamedTypeSymbol>());
					}
				}
			}
			return dictionary;
		}

		// Token: 0x0600725B RID: 29275 RVA: 0x001AAED4 File Offset: 0x001A90D4
		private Dictionary<string, ImmutableArray<NamespaceOrTypeSymbol>> MakeNameToMembersMap(BindingDiagnosticBag diagnostics)
		{
			SourceNamespaceSymbol.NameToSymbolMapBuilder nameToSymbolMapBuilder = new SourceNamespaceSymbol.NameToSymbolMapBuilder(this._mergedDeclaration.Children.Length);
			foreach (MergedNamespaceOrTypeDeclaration mergedNamespaceOrTypeDeclaration in this._mergedDeclaration.Children)
			{
				nameToSymbolMapBuilder.Add(this.BuildSymbol(mergedNamespaceOrTypeDeclaration, diagnostics));
			}
			Dictionary<string, ImmutableArray<NamespaceOrTypeSymbol>> dictionary = nameToSymbolMapBuilder.CreateMap();
			SourceNamespaceSymbol.CheckMembers(this, dictionary, diagnostics);
			return dictionary;
		}

		// Token: 0x0600725C RID: 29276 RVA: 0x001AAF44 File Offset: 0x001A9144
		private static void CheckMembers(NamespaceSymbol @namespace, Dictionary<string, ImmutableArray<NamespaceOrTypeSymbol>> result, BindingDiagnosticBag diagnostics)
		{
			Symbol[] array = new Symbol[10];
			MergedNamespaceSymbol mergedNamespaceSymbol = null;
			if (@namespace.ContainingAssembly.Modules.Length > 1)
			{
				mergedNamespaceSymbol = @namespace.ContainingAssembly.GetAssemblyNamespace(@namespace) as MergedNamespaceSymbol;
			}
			foreach (string text in result.Keys)
			{
				Array.Clear(array, 0, array.Length);
				foreach (NamespaceOrTypeSymbol namespaceOrTypeSymbol in result[text])
				{
					NamedTypeSymbol namedTypeSymbol = namespaceOrTypeSymbol as NamedTypeSymbol;
					int num = ((namedTypeSymbol != null) ? namedTypeSymbol.Arity : 0);
					if (num >= array.Length)
					{
						Array.Resize<Symbol>(ref array, num + 1);
					}
					Symbol symbol = array[num];
					if (symbol == null && mergedNamespaceSymbol != null)
					{
						foreach (NamespaceSymbol namespaceSymbol in mergedNamespaceSymbol.ConstituentNamespaces)
						{
							if (namespaceSymbol != @namespace)
							{
								ImmutableArray<NamedTypeSymbol> typeMembers = namespaceSymbol.GetTypeMembers(namespaceOrTypeSymbol.Name, num);
								if (typeMembers.Length > 0)
								{
									symbol = typeMembers[0];
									break;
								}
							}
						}
					}
					if (symbol != null)
					{
						SourceNamedTypeSymbol sourceNamedTypeSymbol = namedTypeSymbol as SourceNamedTypeSymbol;
						if (sourceNamedTypeSymbol != null && sourceNamedTypeSymbol.IsPartial)
						{
							SourceNamedTypeSymbol sourceNamedTypeSymbol2 = symbol as SourceNamedTypeSymbol;
							if (sourceNamedTypeSymbol2 != null && sourceNamedTypeSymbol2.IsPartial)
							{
								diagnostics.Add(ErrorCode.ERR_PartialTypeKindConflict, namespaceOrTypeSymbol.Locations.FirstOrNone(), new object[] { namespaceOrTypeSymbol });
								goto IL_0178;
							}
						}
						diagnostics.Add(ErrorCode.ERR_DuplicateNameInNS, namespaceOrTypeSymbol.Locations.FirstOrNone(), new object[] { text, @namespace });
					}
					IL_0178:
					array[num] = namespaceOrTypeSymbol;
					if (namedTypeSymbol != null)
					{
						Accessibility declaredAccessibility = namedTypeSymbol.DeclaredAccessibility;
						if (declaredAccessibility != Accessibility.Public && declaredAccessibility != Accessibility.Internal)
						{
							diagnostics.Add(ErrorCode.ERR_NoNamespacePrivate, namespaceOrTypeSymbol.Locations.FirstOrNone());
						}
					}
				}
			}
		}

		// Token: 0x0600725D RID: 29277 RVA: 0x001AB144 File Offset: 0x001A9344
		private NamespaceOrTypeSymbol BuildSymbol(MergedNamespaceOrTypeDeclaration declaration, BindingDiagnosticBag diagnostics)
		{
			switch (declaration.Kind)
			{
			case DeclarationKind.Namespace:
				return new SourceNamespaceSymbol(this._module, this, (MergedNamespaceDeclaration)declaration, diagnostics);
			case DeclarationKind.Class:
			case DeclarationKind.Interface:
			case DeclarationKind.Struct:
			case DeclarationKind.Enum:
			case DeclarationKind.Delegate:
			case DeclarationKind.Record:
			case DeclarationKind.RecordStruct:
				return new SourceNamedTypeSymbol(this, (MergedTypeDeclaration)declaration, diagnostics, null);
			case DeclarationKind.Script:
			case DeclarationKind.Submission:
			case DeclarationKind.ImplicitClass:
				return new ImplicitNamedTypeSymbol(this, (MergedTypeDeclaration)declaration, diagnostics);
			case DeclarationKind.SimpleProgram:
				return new SimpleProgramNamedTypeSymbol(this, (MergedTypeDeclaration)declaration, diagnostics);
			default:
				throw ExceptionUtilities.UnexpectedValue(declaration.Kind);
			}
		}

		// Token: 0x0600725E RID: 29278 RVA: 0x001AB1E0 File Offset: 0x001A93E0
		private void RegisterDeclaredCorTypes()
		{
			AssemblySymbol containingAssembly = this.ContainingAssembly;
			if (containingAssembly.KeepLookingForDeclaredSpecialTypes)
			{
				foreach (ImmutableArray<NamespaceOrTypeSymbol> immutableArray in this._nameToMembersMap.Values)
				{
					foreach (NamespaceOrTypeSymbol namespaceOrTypeSymbol in immutableArray)
					{
						NamedTypeSymbol namedTypeSymbol = namespaceOrTypeSymbol as NamedTypeSymbol;
						if (namedTypeSymbol != null && namedTypeSymbol.SpecialType != SpecialType.None)
						{
							containingAssembly.RegisterDeclaredSpecialType(namedTypeSymbol);
							if (!containingAssembly.KeepLookingForDeclaredSpecialTypes)
							{
								return;
							}
						}
					}
				}
			}
		}

		// Token: 0x0600725F RID: 29279 RVA: 0x001AB280 File Offset: 0x001A9480
		internal override bool IsDefinedInSourceTree(SyntaxTree tree, TextSpan? definedWithinSpan, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (this.IsGlobalNamespace)
			{
				return true;
			}
			foreach (SingleNamespaceOrTypeDeclaration singleNamespaceOrTypeDeclaration in this._mergedDeclaration.Declarations)
			{
				cancellationToken.ThrowIfCancellationRequested();
				SyntaxReference syntaxReference = singleNamespaceOrTypeDeclaration.SyntaxReference;
				if (syntaxReference.SyntaxTree == tree)
				{
					if (definedWithinSpan == null)
					{
						return true;
					}
					if (NamespaceDeclarationSyntaxReference.GetSyntax(syntaxReference, cancellationToken).FullSpan.IntersectsWith(definedWithinSpan.Value))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06007260 RID: 29280 RVA: 0x001AB300 File Offset: 0x001A9500
		internal override void ForceComplete(SourceLocation locationOpt, CancellationToken cancellationToken)
		{
			for (;;)
			{
				cancellationToken.ThrowIfCancellationRequested();
				CompletionPart nextIncompletePart = this._state.NextIncompletePart;
				if (nextIncompletePart == CompletionPart.None)
				{
					break;
				}
				if (nextIncompletePart != CompletionPart.Members)
				{
					if (nextIncompletePart != CompletionPart.MembersCompleted)
					{
						this._state.NotePartComplete(CompletionPart.Attributes | CompletionPart.ReturnTypeAttributes | CompletionPart.Parameters | CompletionPart.Type | CompletionPart.StartBaseType | CompletionPart.FinishBaseType | CompletionPart.StartInterfaces | CompletionPart.FinishInterfaces | CompletionPart.EnumUnderlyingType | CompletionPart.TypeArguments | CompletionPart.TypeParameters | CompletionPart.TypeMembers | CompletionPart.SynthesizedExplicitImplementations | CompletionPart.StartMemberChecks | CompletionPart.FinishMemberChecks);
					}
					else
					{
						SingleNamespaceDeclaration singleNamespaceDeclaration = null;
						foreach (SingleNamespaceDeclaration singleNamespaceDeclaration2 in this._mergedDeclaration.Declarations)
						{
							if ((locationOpt == null || locationOpt.SourceTree == singleNamespaceDeclaration2.SyntaxReference.SyntaxTree) && (singleNamespaceDeclaration2.HasGlobalUsings || singleNamespaceDeclaration2.HasUsings || singleNamespaceDeclaration2.HasExternAliases))
							{
								singleNamespaceDeclaration = singleNamespaceDeclaration2;
								this._aliasesAndUsings[singleNamespaceDeclaration2].Complete(this, singleNamespaceDeclaration2.SyntaxReference, cancellationToken);
							}
						}
						if (this.IsGlobalNamespace && (locationOpt == null || singleNamespaceDeclaration != null))
						{
							this.GetMergedGlobalAliasesAndUsings(null, cancellationToken).Complete(this, cancellationToken);
						}
						ImmutableArray<Symbol> members = this.GetMembers();
						bool flag = true;
						if (this.DeclaringCompilation.Options.ConcurrentBuild)
						{
							RoslynParallel.For(0, members.Length, UICultureUtilities.WithCurrentUICulture<int>(delegate(int i)
							{
								Symbol.ForceCompleteMemberByLocation(locationOpt, members[i], cancellationToken);
							}), cancellationToken);
							ImmutableArray<Symbol>.Enumerator enumerator2 = members.GetEnumerator();
							while (enumerator2.MoveNext())
							{
								if (!enumerator2.Current.HasComplete(CompletionPart.All))
								{
									flag = false;
									break;
								}
							}
						}
						else
						{
							foreach (Symbol symbol in members)
							{
								Symbol.ForceCompleteMemberByLocation(locationOpt, symbol, cancellationToken);
								flag = flag && symbol.HasComplete(CompletionPart.All);
							}
						}
						if (!flag)
						{
							goto IL_0254;
						}
						this._state.NotePartComplete(CompletionPart.MembersCompleted);
					}
				}
				else
				{
					this.GetNameToMembersMap();
				}
				this._state.SpinWaitComplete(nextIncompletePart, cancellationToken);
			}
			return;
			IL_0254:
			CompletionPart completionPart = ((locationOpt == null) ? CompletionPart.NamespaceSymbolAll : CompletionPart.Members);
			this._state.SpinWaitComplete(completionPart, cancellationToken);
		}

		// Token: 0x06007261 RID: 29281 RVA: 0x001AB58E File Offset: 0x001A978E
		internal override bool HasComplete(CompletionPart part)
		{
			return this._state.HasComplete(part);
		}

		// Token: 0x04001BCB RID: 7115
		private readonly SourceModuleSymbol _module;

		// Token: 0x04001BCC RID: 7116
		private readonly Symbol _container;

		// Token: 0x04001BCD RID: 7117
		private readonly MergedNamespaceDeclaration _mergedDeclaration;

		// Token: 0x04001BCE RID: 7118
		private SymbolCompletionState _state;

		// Token: 0x04001BCF RID: 7119
		private ImmutableArray<Location> _locations;

		// Token: 0x04001BD0 RID: 7120
		private Dictionary<string, ImmutableArray<NamespaceOrTypeSymbol>> _nameToMembersMap;

		// Token: 0x04001BD1 RID: 7121
		private Dictionary<string, ImmutableArray<NamedTypeSymbol>> _nameToTypeMembersMap;

		// Token: 0x04001BD2 RID: 7122
		private ImmutableArray<Symbol> _lazyAllMembers;

		// Token: 0x04001BD3 RID: 7123
		private ImmutableArray<NamedTypeSymbol> _lazyTypeMembersUnordered;

		// Token: 0x04001BD4 RID: 7124
		private readonly ImmutableDictionary<SingleNamespaceDeclaration, SourceNamespaceSymbol.AliasesAndUsings> _aliasesAndUsings;

		// Token: 0x04001BD5 RID: 7125
		private SourceNamespaceSymbol.MergedGlobalAliasesAndUsings _lazyMergedGlobalAliasesAndUsings;

		// Token: 0x04001BD6 RID: 7126
		private const int LazyAllMembersIsSorted = 1;

		// Token: 0x04001BD7 RID: 7127
		private int _flags;

		// Token: 0x04001BD8 RID: 7128
		private LexicalSortKey _lazyLexicalSortKey = LexicalSortKey.NotInitialized;

		// Token: 0x04001BD9 RID: 7129
		private static readonly Func<SingleNamespaceDeclaration, SyntaxReference> s_declaringSyntaxReferencesSelector = (SingleNamespaceDeclaration d) => new NamespaceDeclarationSyntaxReference(d.SyntaxReference);

		// Token: 0x02000936 RID: 2358
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private class AliasesAndUsings
		{
			// Token: 0x0600903F RID: 36927 RVA: 0x001FAD0A File Offset: 0x001F8F0A
			[return: global::System.Runtime.CompilerServices.Nullable(0)]
			internal ImmutableArray<AliasAndExternAliasDirective> GetExternAliases(SourceNamespaceSymbol declaringSymbol, CSharpSyntaxNode declarationSyntax)
			{
				return this.GetExternAliasesAndDiagnostics(declaringSymbol, declarationSyntax).ExternAliases;
			}

			// Token: 0x06009040 RID: 36928 RVA: 0x001FAD1C File Offset: 0x001F8F1C
			[return: global::System.Runtime.CompilerServices.Nullable(0)]
			internal ImmutableArray<AliasAndExternAliasDirective> GetExternAliases(SourceNamespaceSymbol declaringSymbol, SyntaxReference declarationSyntax)
			{
				SourceNamespaceSymbol.AliasesAndUsings.ExternAliasesAndDiagnostics externAliasesAndDiagnostics;
				if ((externAliasesAndDiagnostics = this._lazyExternAliases) == null)
				{
					externAliasesAndDiagnostics = this.GetExternAliasesAndDiagnostics(declaringSymbol, (CSharpSyntaxNode)declarationSyntax.GetSyntax(default(CancellationToken)));
				}
				return externAliasesAndDiagnostics.ExternAliases;
			}

			// Token: 0x06009041 RID: 36929 RVA: 0x001FAD54 File Offset: 0x001F8F54
			private SourceNamespaceSymbol.AliasesAndUsings.ExternAliasesAndDiagnostics GetExternAliasesAndDiagnostics(SourceNamespaceSymbol declaringSymbol, CSharpSyntaxNode declarationSyntax)
			{
				if (this._lazyExternAliases == null)
				{
					CompilationUnitSyntax compilationUnitSyntax = declarationSyntax as CompilationUnitSyntax;
					SyntaxList<ExternAliasDirectiveSyntax> syntaxList;
					if (compilationUnitSyntax == null)
					{
						NamespaceDeclarationSyntax namespaceDeclarationSyntax = declarationSyntax as NamespaceDeclarationSyntax;
						if (namespaceDeclarationSyntax == null)
						{
							throw ExceptionUtilities.UnexpectedValue(declarationSyntax);
						}
						syntaxList = namespaceDeclarationSyntax.Externs;
					}
					else
					{
						syntaxList = compilationUnitSyntax.Externs;
					}
					if (!syntaxList.Any())
					{
						this._lazyExternAliases = SourceNamespaceSymbol.AliasesAndUsings.ExternAliasesAndDiagnostics.Empty;
					}
					else
					{
						DiagnosticBag instance = DiagnosticBag.GetInstance();
						Interlocked.CompareExchange<SourceNamespaceSymbol.AliasesAndUsings.ExternAliasesAndDiagnostics>(ref this._lazyExternAliases, new SourceNamespaceSymbol.AliasesAndUsings.ExternAliasesAndDiagnostics
						{
							ExternAliases = SourceNamespaceSymbol.AliasesAndUsings.<GetExternAliasesAndDiagnostics>g__buildExternAliases|7_0(syntaxList, declaringSymbol, instance),
							Diagnostics = instance.ToReadOnlyAndFree()
						}, null);
					}
				}
				return this._lazyExternAliases;
			}

			// Token: 0x06009042 RID: 36930 RVA: 0x001FADE6 File Offset: 0x001F8FE6
			[return: global::System.Runtime.CompilerServices.Nullable(0)]
			internal ImmutableArray<AliasAndUsingDirective> GetUsingAliases(SourceNamespaceSymbol declaringSymbol, CSharpSyntaxNode declarationSyntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved)
			{
				return this.GetUsingsAndDiagnostics(declaringSymbol, declarationSyntax, basesBeingResolved).UsingAliases;
			}

			// Token: 0x06009043 RID: 36931 RVA: 0x001FADF6 File Offset: 0x001F8FF6
			[return: global::System.Runtime.CompilerServices.Nullable(0)]
			internal ImmutableArray<AliasAndUsingDirective> GetGlobalUsingAliases(SourceNamespaceSymbol declaringSymbol, CSharpSyntaxNode declarationSyntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved)
			{
				return this.GetGlobalUsingsAndDiagnostics(declaringSymbol, declarationSyntax, basesBeingResolved).UsingAliases;
			}

			// Token: 0x06009044 RID: 36932 RVA: 0x001FAE06 File Offset: 0x001F9006
			internal ImmutableDictionary<string, AliasAndUsingDirective> GetUsingAliasesMap(SourceNamespaceSymbol declaringSymbol, CSharpSyntaxNode declarationSyntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved)
			{
				return this.GetUsingsAndDiagnostics(declaringSymbol, declarationSyntax, basesBeingResolved).UsingAliasesMap ?? ImmutableDictionary<string, AliasAndUsingDirective>.Empty;
			}

			// Token: 0x06009045 RID: 36933 RVA: 0x001FAE20 File Offset: 0x001F9020
			internal ImmutableDictionary<string, AliasAndUsingDirective> GetGlobalUsingAliasesMap(SourceNamespaceSymbol declaringSymbol, SyntaxReference declarationSyntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved)
			{
				SourceNamespaceSymbol.AliasesAndUsings.UsingsAndDiagnostics usingsAndDiagnostics;
				if ((usingsAndDiagnostics = this._lazyGlobalUsings) == null)
				{
					usingsAndDiagnostics = this.GetGlobalUsingsAndDiagnostics(declaringSymbol, (CSharpSyntaxNode)declarationSyntax.GetSyntax(default(CancellationToken)), basesBeingResolved);
				}
				return usingsAndDiagnostics.UsingAliasesMap ?? ImmutableDictionary<string, AliasAndUsingDirective>.Empty;
			}

			// Token: 0x06009046 RID: 36934 RVA: 0x001FAE61 File Offset: 0x001F9061
			[return: global::System.Runtime.CompilerServices.Nullable(0)]
			internal ImmutableArray<NamespaceOrTypeAndUsingDirective> GetUsingNamespacesOrTypes(SourceNamespaceSymbol declaringSymbol, CSharpSyntaxNode declarationSyntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved)
			{
				return this.GetUsingsAndDiagnostics(declaringSymbol, declarationSyntax, basesBeingResolved).UsingNamespacesOrTypes;
			}

			// Token: 0x06009047 RID: 36935 RVA: 0x001FAE71 File Offset: 0x001F9071
			private SourceNamespaceSymbol.AliasesAndUsings.UsingsAndDiagnostics GetUsingsAndDiagnostics(SourceNamespaceSymbol declaringSymbol, CSharpSyntaxNode declarationSyntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved)
			{
				return this.GetUsingsAndDiagnostics(ref this._lazyUsings, declaringSymbol, declarationSyntax, basesBeingResolved, false);
			}

			// Token: 0x06009048 RID: 36936 RVA: 0x001FAE84 File Offset: 0x001F9084
			[return: global::System.Runtime.CompilerServices.Nullable(0)]
			internal ImmutableArray<NamespaceOrTypeAndUsingDirective> GetGlobalUsingNamespacesOrTypes(SourceNamespaceSymbol declaringSymbol, SyntaxReference declarationSyntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved)
			{
				SourceNamespaceSymbol.AliasesAndUsings.UsingsAndDiagnostics usingsAndDiagnostics;
				if ((usingsAndDiagnostics = this._lazyGlobalUsings) == null)
				{
					usingsAndDiagnostics = this.GetGlobalUsingsAndDiagnostics(declaringSymbol, (CSharpSyntaxNode)declarationSyntax.GetSyntax(default(CancellationToken)), basesBeingResolved);
				}
				return usingsAndDiagnostics.UsingNamespacesOrTypes;
			}

			// Token: 0x06009049 RID: 36937 RVA: 0x001FAEBC File Offset: 0x001F90BC
			private SourceNamespaceSymbol.AliasesAndUsings.UsingsAndDiagnostics GetGlobalUsingsAndDiagnostics(SourceNamespaceSymbol declaringSymbol, CSharpSyntaxNode declarationSyntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved)
			{
				return this.GetUsingsAndDiagnostics(ref this._lazyGlobalUsings, declaringSymbol, declarationSyntax, basesBeingResolved, true);
			}

			// Token: 0x0600904A RID: 36938 RVA: 0x001FAED0 File Offset: 0x001F90D0
			private SourceNamespaceSymbol.AliasesAndUsings.UsingsAndDiagnostics GetUsingsAndDiagnostics([global::System.Runtime.CompilerServices.Nullable(2)] ref SourceNamespaceSymbol.AliasesAndUsings.UsingsAndDiagnostics usings, SourceNamespaceSymbol declaringSymbol, CSharpSyntaxNode declarationSyntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved, bool onlyGlobal)
			{
				if (usings == null)
				{
					CompilationUnitSyntax compilationUnitSyntax = declarationSyntax as CompilationUnitSyntax;
					bool? flag;
					SyntaxList<UsingDirectiveSyntax> syntaxList;
					if (compilationUnitSyntax == null)
					{
						NamespaceDeclarationSyntax namespaceDeclarationSyntax = declarationSyntax as NamespaceDeclarationSyntax;
						if (namespaceDeclarationSyntax == null)
						{
							throw ExceptionUtilities.UnexpectedValue(declarationSyntax);
						}
						flag = default(bool?);
						syntaxList = namespaceDeclarationSyntax.Usings;
					}
					else
					{
						flag = new bool?(onlyGlobal);
						syntaxList = compilationUnitSyntax.Usings;
					}
					SourceNamespaceSymbol.AliasesAndUsings.UsingsAndDiagnostics usingsAndDiagnostics;
					if (!syntaxList.Any())
					{
						bool? flag2 = flag;
						bool flag3 = false;
						if (!((flag2.GetValueOrDefault() == flag3) & (flag2 != null)))
						{
							usingsAndDiagnostics = SourceNamespaceSymbol.AliasesAndUsings.UsingsAndDiagnostics.Empty;
						}
						else
						{
							usingsAndDiagnostics = new SourceNamespaceSymbol.AliasesAndUsings.UsingsAndDiagnostics
							{
								UsingAliases = this.GetGlobalUsingAliases(declaringSymbol, declarationSyntax, basesBeingResolved),
								UsingAliasesMap = declaringSymbol.GetGlobalUsingAliasesMap(basesBeingResolved),
								UsingNamespacesOrTypes = declaringSymbol.GetGlobalUsingNamespacesOrTypes(basesBeingResolved),
								Diagnostics = null
							};
						}
					}
					else
					{
						usingsAndDiagnostics = this.<GetUsingsAndDiagnostics>g__buildUsings|16_0(syntaxList, declaringSymbol, declarationSyntax, flag, basesBeingResolved);
					}
					Interlocked.CompareExchange<SourceNamespaceSymbol.AliasesAndUsings.UsingsAndDiagnostics>(ref usings, usingsAndDiagnostics, null);
				}
				return usings;
			}

			// Token: 0x0600904B RID: 36939 RVA: 0x001FAFA6 File Offset: 0x001F91A6
			internal Imports GetImports(SourceNamespaceSymbol declaringSymbol, CSharpSyntaxNode declarationSyntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved)
			{
				if (this._lazyImports == null)
				{
					Interlocked.CompareExchange<Imports>(ref this._lazyImports, Imports.Create(this.GetUsingAliasesMap(declaringSymbol, declarationSyntax, basesBeingResolved), this.GetUsingNamespacesOrTypes(declaringSymbol, declarationSyntax, basesBeingResolved), this.GetExternAliases(declaringSymbol, declarationSyntax)), null);
				}
				return this._lazyImports;
			}

			// Token: 0x0600904C RID: 36940 RVA: 0x001FAFE4 File Offset: 0x001F91E4
			internal void Complete(SourceNamespaceSymbol declaringSymbol, SyntaxReference declarationSyntax, CancellationToken cancellationToken)
			{
				SourceNamespaceSymbol.AliasesAndUsings.ExternAliasesAndDiagnostics externAliasesAndDiagnostics = this._lazyExternAliases ?? this.GetExternAliasesAndDiagnostics(declaringSymbol, (CSharpSyntaxNode)declarationSyntax.GetSyntax(cancellationToken));
				cancellationToken.ThrowIfCancellationRequested();
				SourceNamespaceSymbol.AliasesAndUsings.UsingsAndDiagnostics usingsAndDiagnostics = this._lazyGlobalUsings ?? (declaringSymbol.IsGlobalNamespace ? this.GetGlobalUsingsAndDiagnostics(declaringSymbol, (CSharpSyntaxNode)declarationSyntax.GetSyntax(cancellationToken), null) : SourceNamespaceSymbol.AliasesAndUsings.UsingsAndDiagnostics.Empty);
				cancellationToken.ThrowIfCancellationRequested();
				SourceNamespaceSymbol.AliasesAndUsings.UsingsAndDiagnostics usingsAndDiagnostics2 = this._lazyUsings ?? this.GetUsingsAndDiagnostics(declaringSymbol, (CSharpSyntaxNode)declarationSyntax.GetSyntax(cancellationToken), null);
				cancellationToken.ThrowIfCancellationRequested();
				for (;;)
				{
					cancellationToken.ThrowIfCancellationRequested();
					CompletionPart nextIncompletePart = this._state.NextIncompletePart;
					if (nextIncompletePart == CompletionPart.None)
					{
						break;
					}
					if (nextIncompletePart != CompletionPart.StartBaseType)
					{
						if (nextIncompletePart != CompletionPart.FinishBaseType)
						{
							this._state.NotePartComplete(CompletionPart.Attributes | CompletionPart.ReturnTypeAttributes | CompletionPart.Parameters | CompletionPart.Type | CompletionPart.StartInterfaces | CompletionPart.FinishInterfaces | CompletionPart.EnumUnderlyingType | CompletionPart.TypeArguments | CompletionPart.TypeParameters | CompletionPart.Members | CompletionPart.TypeMembers | CompletionPart.SynthesizedExplicitImplementations | CompletionPart.StartMemberChecks | CompletionPart.FinishMemberChecks | CompletionPart.MembersCompleted);
						}
						else
						{
							this._state.SpinWaitComplete(CompletionPart.FinishBaseType, cancellationToken);
						}
					}
					else if (this._state.NotePartComplete(CompletionPart.StartBaseType))
					{
						SourceNamespaceSymbol.AliasesAndUsings.Validate(declaringSymbol, declarationSyntax, externAliasesAndDiagnostics, usingsAndDiagnostics2, usingsAndDiagnostics.Diagnostics);
						this._state.NotePartComplete(CompletionPart.FinishBaseType);
					}
					this._state.SpinWaitComplete(nextIncompletePart, cancellationToken);
				}
			}

			// Token: 0x0600904D RID: 36941 RVA: 0x001FB0F4 File Offset: 0x001F92F4
			private static void Validate(SourceNamespaceSymbol declaringSymbol, SyntaxReference declarationSyntax, SourceNamespaceSymbol.AliasesAndUsings.ExternAliasesAndDiagnostics externAliasesAndDiagnostics, SourceNamespaceSymbol.AliasesAndUsings.UsingsAndDiagnostics usingsAndDiagnostics, [global::System.Runtime.CompilerServices.Nullable(2)] DiagnosticBag globalUsingDiagnostics)
			{
				SourceNamespaceSymbol.AliasesAndUsings.<>c__DisplayClass19_0 CS$<>8__locals1;
				CS$<>8__locals1.compilation = declaringSymbol.DeclaringCompilation;
				DiagnosticBag declarationDiagnostics = CS$<>8__locals1.compilation.DeclarationDiagnostics;
				CS$<>8__locals1.diagnostics = BindingDiagnosticBag.GetInstance();
				if (usingsAndDiagnostics.UsingAliasesMap != null)
				{
					foreach (KeyValuePair<string, AliasAndUsingDirective> keyValuePair in usingsAndDiagnostics.UsingAliasesMap)
					{
						string text;
						AliasAndUsingDirective aliasAndUsingDirective;
						keyValuePair.Deconstruct<string, AliasAndUsingDirective>(out text, out aliasAndUsingDirective);
						AliasAndUsingDirective aliasAndUsingDirective2 = aliasAndUsingDirective;
						if (aliasAndUsingDirective2.UsingDirectiveReference.SyntaxTree == declarationSyntax.SyntaxTree)
						{
							NamespaceOrTypeSymbol aliasTarget = aliasAndUsingDirective2.Alias.GetAliasTarget(null);
							CS$<>8__locals1.diagnostics.Clear();
							AliasSymbolFromSyntax aliasSymbolFromSyntax = aliasAndUsingDirective2.Alias as AliasSymbolFromSyntax;
							if (aliasSymbolFromSyntax != null)
							{
								CS$<>8__locals1.diagnostics.AddRange(aliasSymbolFromSyntax.AliasTargetDiagnostics, false);
							}
							aliasAndUsingDirective2.Alias.CheckConstraints(CS$<>8__locals1.diagnostics);
							declarationDiagnostics.AddRange(CS$<>8__locals1.diagnostics.DiagnosticBag);
							SourceNamespaceSymbol.AliasesAndUsings.<Validate>g__recordImportDependencies|19_0(aliasAndUsingDirective2.UsingDirective, aliasTarget, ref CS$<>8__locals1);
						}
					}
				}
				TypeConversions typeConversions = new TypeConversions(CS$<>8__locals1.compilation.SourceAssembly.CorLibrary, false);
				foreach (NamespaceOrTypeAndUsingDirective namespaceOrTypeAndUsingDirective in usingsAndDiagnostics.UsingNamespacesOrTypes)
				{
					if (namespaceOrTypeAndUsingDirective.UsingDirectiveReference.SyntaxTree == declarationSyntax.SyntaxTree)
					{
						CS$<>8__locals1.diagnostics.Clear();
						CS$<>8__locals1.diagnostics.AddDependencies(namespaceOrTypeAndUsingDirective.Dependencies);
						NamespaceOrTypeSymbol namespaceOrType = namespaceOrTypeAndUsingDirective.NamespaceOrType;
						UsingDirectiveSyntax usingDirective = namespaceOrTypeAndUsingDirective.UsingDirective;
						if (namespaceOrType.IsType)
						{
							TypeSymbol typeSymbol = (TypeSymbol)namespaceOrType;
							Location location = usingDirective.Name.Location;
							typeSymbol.CheckAllConstraints(CS$<>8__locals1.compilation, typeConversions, location, CS$<>8__locals1.diagnostics);
						}
						declarationDiagnostics.AddRange(CS$<>8__locals1.diagnostics.DiagnosticBag);
						SourceNamespaceSymbol.AliasesAndUsings.<Validate>g__recordImportDependencies|19_0(usingDirective, namespaceOrType, ref CS$<>8__locals1);
					}
				}
				foreach (AliasAndExternAliasDirective aliasAndExternAliasDirective in externAliasesAndDiagnostics.ExternAliases)
				{
					if (!aliasAndExternAliasDirective.SkipInLookup)
					{
						NamespaceSymbol namespaceSymbol = (NamespaceSymbol)aliasAndExternAliasDirective.Alias.GetAliasTarget(null);
						AliasSymbolFromSyntax aliasSymbolFromSyntax2 = aliasAndExternAliasDirective.Alias as AliasSymbolFromSyntax;
						if (aliasSymbolFromSyntax2 != null)
						{
							declarationDiagnostics.AddRange(aliasSymbolFromSyntax2.AliasTargetDiagnostics.DiagnosticBag);
						}
						if (!Compilation.ReportUnusedImportsInTree(aliasAndExternAliasDirective.ExternAliasDirective.SyntaxTree))
						{
							CS$<>8__locals1.diagnostics.Clear();
							CS$<>8__locals1.diagnostics.AddAssembliesUsedByNamespaceReference(namespaceSymbol);
							CS$<>8__locals1.compilation.AddUsedAssemblies(CS$<>8__locals1.diagnostics.DependenciesBag);
						}
					}
				}
				declarationDiagnostics.AddRange<Diagnostic>(externAliasesAndDiagnostics.Diagnostics);
				DiagnosticBag diagnostics = usingsAndDiagnostics.Diagnostics;
				if (diagnostics != null && !diagnostics.IsEmptyWithoutResolution)
				{
					declarationDiagnostics.AddRange(usingsAndDiagnostics.Diagnostics.AsEnumerable());
				}
				if (globalUsingDiagnostics != null && !globalUsingDiagnostics.IsEmptyWithoutResolution)
				{
					declarationDiagnostics.AddRange(globalUsingDiagnostics.AsEnumerable());
				}
				CS$<>8__locals1.diagnostics.Free();
			}

			// Token: 0x0600904F RID: 36943 RVA: 0x001FB3EC File Offset: 0x001F95EC
			[CompilerGenerated]
			[return: global::System.Runtime.CompilerServices.Nullable(0)]
			internal static ImmutableArray<AliasAndExternAliasDirective> <GetExternAliasesAndDiagnostics>g__buildExternAliases|7_0([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<ExternAliasDirectiveSyntax> syntaxList, SourceNamespaceSymbol declaringSymbol, DiagnosticBag diagnostics)
			{
				CSharpCompilation declaringCompilation = declaringSymbol.DeclaringCompilation;
				ArrayBuilder<AliasAndExternAliasDirective> instance = ArrayBuilder<AliasAndExternAliasDirective>.GetInstance();
				foreach (ExternAliasDirectiveSyntax externAliasDirectiveSyntax in syntaxList)
				{
					declaringCompilation.RecordImport(externAliasDirectiveSyntax);
					bool flag = false;
					if (declaringCompilation.IsSubmission)
					{
						diagnostics.Add(ErrorCode.ERR_ExternAliasNotAllowed, externAliasDirectiveSyntax.Location);
						flag = true;
					}
					else
					{
						foreach (AliasAndExternAliasDirective aliasAndExternAliasDirective in instance)
						{
							if (aliasAndExternAliasDirective.Alias.Name == externAliasDirectiveSyntax.Identifier.ValueText)
							{
								diagnostics.Add(ErrorCode.ERR_DuplicateAlias, aliasAndExternAliasDirective.Alias.Locations[0], new object[] { aliasAndExternAliasDirective.Alias.Name });
								break;
							}
						}
						if (externAliasDirectiveSyntax.Identifier.ContextualKind() == SyntaxKind.GlobalKeyword)
						{
							diagnostics.Add(ErrorCode.ERR_GlobalExternAlias, externAliasDirectiveSyntax.Identifier.GetLocation());
						}
					}
					instance.Add(new AliasAndExternAliasDirective(new AliasSymbolFromSyntax(declaringSymbol, externAliasDirectiveSyntax), externAliasDirectiveSyntax, flag));
				}
				return instance.ToImmutableAndFree();
			}

			// Token: 0x06009050 RID: 36944 RVA: 0x001FB514 File Offset: 0x001F9714
			[CompilerGenerated]
			private SourceNamespaceSymbol.AliasesAndUsings.UsingsAndDiagnostics <GetUsingsAndDiagnostics>g__buildUsings|16_0([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<UsingDirectiveSyntax> usingDirectives, SourceNamespaceSymbol declaringSymbol, CSharpSyntaxNode declarationSyntax, bool? applyIsGlobalFilter, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved)
			{
				ImmutableArray<AliasAndExternAliasDirective> externAliases = this.GetExternAliases(declaringSymbol, declarationSyntax);
				ImmutableDictionary<string, AliasAndUsingDirective> immutableDictionary = ImmutableDictionary<string, AliasAndUsingDirective>.Empty;
				ImmutableArray<NamespaceOrTypeAndUsingDirective> immutableArray = ImmutableArray<NamespaceOrTypeAndUsingDirective>.Empty;
				ImmutableArray<AliasAndUsingDirective> immutableArray2 = ImmutableArray<AliasAndUsingDirective>.Empty;
				bool? flag = applyIsGlobalFilter;
				bool flag2 = false;
				if ((flag.GetValueOrDefault() == flag2) & (flag != null))
				{
					immutableDictionary = declaringSymbol.GetGlobalUsingAliasesMap(basesBeingResolved);
					immutableArray = declaringSymbol.GetGlobalUsingNamespacesOrTypes(basesBeingResolved);
					immutableArray2 = this.GetGlobalUsingAliases(declaringSymbol, declarationSyntax, basesBeingResolved);
				}
				DiagnosticBag diagnosticBag = new DiagnosticBag();
				CSharpCompilation declaringCompilation = declaringSymbol.DeclaringCompilation;
				ArrayBuilder<NamespaceOrTypeAndUsingDirective> arrayBuilder = null;
				ImmutableDictionary<string, AliasAndUsingDirective>.Builder builder = null;
				ArrayBuilder<AliasAndUsingDirective> arrayBuilder2 = null;
				Binder binder = null;
				PooledHashSet<NamespaceOrTypeSymbol> pooledHashSet = null;
				foreach (UsingDirectiveSyntax usingDirectiveSyntax in usingDirectives)
				{
					if (applyIsGlobalFilter == null || usingDirectiveSyntax.GlobalKeyword.IsKind(SyntaxKind.GlobalKeyword) == applyIsGlobalFilter.GetValueOrDefault())
					{
						declaringCompilation.RecordImport(usingDirectiveSyntax);
						if (usingDirectiveSyntax.Alias != null)
						{
							SyntaxToken identifier = usingDirectiveSyntax.Alias.Name.Identifier;
							Location location = usingDirectiveSyntax.Alias.Name.Location;
							if (identifier.ContextualKind() == SyntaxKind.GlobalKeyword)
							{
								diagnosticBag.Add(ErrorCode.WRN_GlobalAliasDefn, location);
							}
							if (usingDirectiveSyntax.StaticKeyword != default(SyntaxToken))
							{
								diagnosticBag.Add(ErrorCode.ERR_NoAliasHere, location);
							}
							SourceMemberContainerTypeSymbol.ReportTypeNamedRecord(identifier.Text, declaringCompilation, diagnosticBag, location);
							string valueText = identifier.ValueText;
							bool flag3 = false;
							if ((builder != null) ? builder.ContainsKey(valueText) : immutableDictionary.ContainsKey(valueText))
							{
								flag3 = true;
								if (!usingDirectiveSyntax.Name.IsMissing)
								{
									diagnosticBag.Add(ErrorCode.ERR_DuplicateAlias, location, new object[] { valueText });
								}
							}
							else
							{
								ImmutableArray<AliasAndExternAliasDirective>.Enumerator enumerator2 = externAliases.GetEnumerator();
								while (enumerator2.MoveNext())
								{
									if (enumerator2.Current.Alias.Name == valueText)
									{
										diagnosticBag.Add(ErrorCode.ERR_DuplicateAlias, usingDirectiveSyntax.Location, new object[] { valueText });
										break;
									}
								}
							}
							AliasAndUsingDirective aliasAndUsingDirective = new AliasAndUsingDirective(new AliasSymbolFromSyntax(declaringSymbol, usingDirectiveSyntax), usingDirectiveSyntax);
							if (arrayBuilder2 == null)
							{
								arrayBuilder2 = ArrayBuilder<AliasAndUsingDirective>.GetInstance();
								arrayBuilder2.AddRange(immutableArray2);
							}
							arrayBuilder2.Add(aliasAndUsingDirective);
							if (!flag3)
							{
								if (builder == null)
								{
									builder = immutableDictionary.ToBuilder();
								}
								builder.Add(valueText, aliasAndUsingDirective);
							}
						}
						else if (!usingDirectiveSyntax.Name.IsMissing)
						{
							BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
							if (binder == null)
							{
								binder = declaringCompilation.GetBinderFactory(declarationSyntax.SyntaxTree, false).GetBinder(usingDirectiveSyntax.Name, null, null).WithAdditionalFlags(BinderFlags.SuppressConstraintChecks);
							}
							NamespaceOrTypeSymbol namespaceOrTypeSymbol = binder.BindNamespaceOrTypeSymbol(usingDirectiveSyntax.Name, instance, basesBeingResolved).NamespaceOrTypeSymbol;
							if (namespaceOrTypeSymbol.Kind == SymbolKind.Namespace)
							{
								if (usingDirectiveSyntax.StaticKeyword != default(SyntaxToken))
								{
									diagnosticBag.Add(ErrorCode.ERR_BadUsingType, usingDirectiveSyntax.Name.Location, new object[] { namespaceOrTypeSymbol });
								}
								else if (!SourceNamespaceSymbol.AliasesAndUsings.<GetUsingsAndDiagnostics>g__getOrCreateUniqueUsings|16_1(ref pooledHashSet, immutableArray).Add(namespaceOrTypeSymbol))
								{
									diagnosticBag.Add(ErrorCode.WRN_DuplicateUsing, usingDirectiveSyntax.Name.Location, new object[] { namespaceOrTypeSymbol });
								}
								else
								{
									SourceNamespaceSymbol.AliasesAndUsings.<GetUsingsAndDiagnostics>g__getOrCreateUsingsBuilder|16_2(ref arrayBuilder, immutableArray).Add(new NamespaceOrTypeAndUsingDirective(namespaceOrTypeSymbol, usingDirectiveSyntax, default(ImmutableArray<AssemblySymbol>)));
								}
							}
							else if (namespaceOrTypeSymbol.Kind == SymbolKind.NamedType)
							{
								if (usingDirectiveSyntax.StaticKeyword == default(SyntaxToken))
								{
									diagnosticBag.Add(ErrorCode.ERR_BadUsingNamespace, usingDirectiveSyntax.Name.Location, new object[] { namespaceOrTypeSymbol });
								}
								else
								{
									NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)namespaceOrTypeSymbol;
									if (!SourceNamespaceSymbol.AliasesAndUsings.<GetUsingsAndDiagnostics>g__getOrCreateUniqueUsings|16_1(ref pooledHashSet, immutableArray).Add(namedTypeSymbol))
									{
										diagnosticBag.Add(ErrorCode.WRN_DuplicateUsing, usingDirectiveSyntax.Name.Location, new object[] { namedTypeSymbol });
									}
									else
									{
										binder.ReportDiagnosticsIfObsolete(diagnosticBag, namedTypeSymbol, usingDirectiveSyntax.Name, false);
										SourceNamespaceSymbol.AliasesAndUsings.<GetUsingsAndDiagnostics>g__getOrCreateUsingsBuilder|16_2(ref arrayBuilder, immutableArray).Add(new NamespaceOrTypeAndUsingDirective(namedTypeSymbol, usingDirectiveSyntax, instance.DependenciesBag.ToImmutableArray<AssemblySymbol>()));
									}
								}
							}
							else if (namespaceOrTypeSymbol.Kind != SymbolKind.ErrorType)
							{
								diagnosticBag.Add(ErrorCode.ERR_BadSKknown, usingDirectiveSyntax.Name.Location, new object[]
								{
									usingDirectiveSyntax.Name,
									namespaceOrTypeSymbol.GetKindText(),
									MessageID.IDS_SK_TYPE_OR_NAMESPACE.Localize()
								});
							}
							diagnosticBag.AddRange(instance.DiagnosticBag);
							instance.Free();
						}
					}
				}
				if (pooledHashSet != null)
				{
					pooledHashSet.Free();
				}
				if (diagnosticBag.IsEmptyWithoutResolution)
				{
					diagnosticBag = null;
				}
				return new SourceNamespaceSymbol.AliasesAndUsings.UsingsAndDiagnostics
				{
					UsingAliases = ((arrayBuilder2 != null) ? arrayBuilder2.ToImmutableAndFree() : immutableArray2),
					UsingAliasesMap = (((builder != null) ? builder.ToImmutable() : null) ?? immutableDictionary),
					UsingNamespacesOrTypes = ((arrayBuilder != null) ? arrayBuilder.ToImmutableAndFree() : immutableArray),
					Diagnostics = diagnosticBag
				};
			}

			// Token: 0x06009051 RID: 36945 RVA: 0x001FB9F7 File Offset: 0x001F9BF7
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			[CompilerGenerated]
			[return: global::System.Runtime.CompilerServices.Nullable(1)]
			internal static PooledHashSet<NamespaceOrTypeSymbol> <GetUsingsAndDiagnostics>g__getOrCreateUniqueUsings|16_1([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ref PooledHashSet<NamespaceOrTypeSymbol> uniqueUsings, ImmutableArray<NamespaceOrTypeAndUsingDirective> globalUsingNamespacesOrTypes)
			{
				if (uniqueUsings == null)
				{
					uniqueUsings = SpecializedSymbolCollections.GetPooledSymbolHashSetInstance<NamespaceOrTypeSymbol>();
					uniqueUsings.AddAll<NamespaceOrTypeSymbol>(globalUsingNamespacesOrTypes.Select<NamespaceOrTypeAndUsingDirective, NamespaceOrTypeSymbol>((NamespaceOrTypeAndUsingDirective n) => n.NamespaceOrType));
				}
				return uniqueUsings;
			}

			// Token: 0x06009052 RID: 36946 RVA: 0x001FBA33 File Offset: 0x001F9C33
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			[CompilerGenerated]
			[return: global::System.Runtime.CompilerServices.Nullable(1)]
			internal static ArrayBuilder<NamespaceOrTypeAndUsingDirective> <GetUsingsAndDiagnostics>g__getOrCreateUsingsBuilder|16_2([global::System.Runtime.CompilerServices.Nullable(2)] ref ArrayBuilder<NamespaceOrTypeAndUsingDirective> usings, ImmutableArray<NamespaceOrTypeAndUsingDirective> globalUsingNamespacesOrTypes)
			{
				if (usings == null)
				{
					usings = ArrayBuilder<NamespaceOrTypeAndUsingDirective>.GetInstance();
					usings.AddRange(globalUsingNamespacesOrTypes);
				}
				return usings;
			}

			// Token: 0x06009053 RID: 36947 RVA: 0x001FBA4C File Offset: 0x001F9C4C
			[CompilerGenerated]
			internal static void <Validate>g__recordImportDependencies|19_0(UsingDirectiveSyntax usingDirective, NamespaceOrTypeSymbol target, ref SourceNamespaceSymbol.AliasesAndUsings.<>c__DisplayClass19_0 A_2)
			{
				if (Compilation.ReportUnusedImportsInTree(usingDirective.SyntaxTree))
				{
					A_2.compilation.RecordImportDependencies(usingDirective, A_2.diagnostics.DependenciesBag.ToImmutableArray<AssemblySymbol>());
					return;
				}
				if (target.IsNamespace)
				{
					A_2.diagnostics.AddAssembliesUsedByNamespaceReference((NamespaceSymbol)target);
				}
				A_2.compilation.AddUsedAssemblies(A_2.diagnostics.DependenciesBag);
			}

			// Token: 0x04002803 RID: 10243
			[global::System.Runtime.CompilerServices.Nullable(2)]
			private SourceNamespaceSymbol.AliasesAndUsings.ExternAliasesAndDiagnostics _lazyExternAliases;

			// Token: 0x04002804 RID: 10244
			[global::System.Runtime.CompilerServices.Nullable(2)]
			private SourceNamespaceSymbol.AliasesAndUsings.UsingsAndDiagnostics _lazyGlobalUsings;

			// Token: 0x04002805 RID: 10245
			[global::System.Runtime.CompilerServices.Nullable(2)]
			private SourceNamespaceSymbol.AliasesAndUsings.UsingsAndDiagnostics _lazyUsings;

			// Token: 0x04002806 RID: 10246
			[global::System.Runtime.CompilerServices.Nullable(2)]
			private Imports _lazyImports;

			// Token: 0x04002807 RID: 10247
			private SymbolCompletionState _state;

			// Token: 0x02000A07 RID: 2567
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			private class ExternAliasesAndDiagnostics
			{
				// Token: 0x17002941 RID: 10561
				// (get) Token: 0x060093F9 RID: 37881 RVA: 0x002039EE File Offset: 0x00201BEE
				// (set) Token: 0x060093FA RID: 37882 RVA: 0x002039F6 File Offset: 0x00201BF6
				public ImmutableArray<AliasAndExternAliasDirective> ExternAliases { get; set; }

				// Token: 0x17002942 RID: 10562
				// (get) Token: 0x060093FB RID: 37883 RVA: 0x002039FF File Offset: 0x00201BFF
				// (set) Token: 0x060093FC RID: 37884 RVA: 0x00203A07 File Offset: 0x00201C07
				[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
				public ImmutableArray<Diagnostic> Diagnostics
				{
					[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
					get;
					[param: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
					set;
				}

				// Token: 0x04002A5D RID: 10845
				[global::System.Runtime.CompilerServices.Nullable(1)]
				public static readonly SourceNamespaceSymbol.AliasesAndUsings.ExternAliasesAndDiagnostics Empty = new SourceNamespaceSymbol.AliasesAndUsings.ExternAliasesAndDiagnostics
				{
					ExternAliases = ImmutableArray<AliasAndExternAliasDirective>.Empty,
					Diagnostics = ImmutableArray<Diagnostic>.Empty
				};
			}

			// Token: 0x02000A08 RID: 2568
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			private class UsingsAndDiagnostics
			{
				// Token: 0x17002943 RID: 10563
				// (get) Token: 0x060093FF RID: 37887 RVA: 0x00203A3A File Offset: 0x00201C3A
				// (set) Token: 0x06009400 RID: 37888 RVA: 0x00203A42 File Offset: 0x00201C42
				public ImmutableArray<AliasAndUsingDirective> UsingAliases { get; set; }

				// Token: 0x17002944 RID: 10564
				// (get) Token: 0x06009401 RID: 37889 RVA: 0x00203A4B File Offset: 0x00201C4B
				// (set) Token: 0x06009402 RID: 37890 RVA: 0x00203A53 File Offset: 0x00201C53
				[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
				public ImmutableDictionary<string, AliasAndUsingDirective> UsingAliasesMap
				{
					[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
					get;
					[param: global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
					set;
				}

				// Token: 0x17002945 RID: 10565
				// (get) Token: 0x06009403 RID: 37891 RVA: 0x00203A5C File Offset: 0x00201C5C
				// (set) Token: 0x06009404 RID: 37892 RVA: 0x00203A64 File Offset: 0x00201C64
				public ImmutableArray<NamespaceOrTypeAndUsingDirective> UsingNamespacesOrTypes { get; set; }

				// Token: 0x17002946 RID: 10566
				// (get) Token: 0x06009405 RID: 37893 RVA: 0x00203A6D File Offset: 0x00201C6D
				// (set) Token: 0x06009406 RID: 37894 RVA: 0x00203A75 File Offset: 0x00201C75
				[global::System.Runtime.CompilerServices.Nullable(2)]
				public DiagnosticBag Diagnostics
				{
					[global::System.Runtime.CompilerServices.NullableContext(2)]
					get;
					[global::System.Runtime.CompilerServices.NullableContext(2)]
					set;
				}

				// Token: 0x04002A60 RID: 10848
				[global::System.Runtime.CompilerServices.Nullable(1)]
				public static readonly SourceNamespaceSymbol.AliasesAndUsings.UsingsAndDiagnostics Empty = new SourceNamespaceSymbol.AliasesAndUsings.UsingsAndDiagnostics
				{
					UsingAliases = ImmutableArray<AliasAndUsingDirective>.Empty,
					UsingAliasesMap = null,
					UsingNamespacesOrTypes = ImmutableArray<NamespaceOrTypeAndUsingDirective>.Empty,
					Diagnostics = null
				};
			}
		}

		// Token: 0x02000937 RID: 2359
		private class MergedGlobalAliasesAndUsings
		{
			// Token: 0x170028AA RID: 10410
			// (get) Token: 0x06009054 RID: 36948 RVA: 0x001FBAB2 File Offset: 0x001F9CB2
			// (set) Token: 0x06009055 RID: 36949 RVA: 0x001FBABA File Offset: 0x001F9CBA
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
			public ImmutableDictionary<string, AliasAndUsingDirective> UsingAliasesMap
			{
				[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
				get;
				[param: global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
				set;
			}

			// Token: 0x170028AB RID: 10411
			// (get) Token: 0x06009056 RID: 36950 RVA: 0x001FBAC3 File Offset: 0x001F9CC3
			// (set) Token: 0x06009057 RID: 36951 RVA: 0x001FBACB File Offset: 0x001F9CCB
			public ImmutableArray<NamespaceOrTypeAndUsingDirective> UsingNamespacesOrTypes { get; set; }

			// Token: 0x170028AC RID: 10412
			// (get) Token: 0x06009058 RID: 36952 RVA: 0x001FBAD4 File Offset: 0x001F9CD4
			// (set) Token: 0x06009059 RID: 36953 RVA: 0x001FBADC File Offset: 0x001F9CDC
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			public ImmutableArray<Diagnostic> Diagnostics
			{
				[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
				get;
				[param: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
				set;
			}

			// Token: 0x170028AD RID: 10413
			// (get) Token: 0x0600905A RID: 36954 RVA: 0x001FBAE5 File Offset: 0x001F9CE5
			[global::System.Runtime.CompilerServices.Nullable(1)]
			public Imports Imports
			{
				[global::System.Runtime.CompilerServices.NullableContext(1)]
				get
				{
					if (this._lazyImports == null)
					{
						Interlocked.CompareExchange<Imports>(ref this._lazyImports, Imports.Create(this.UsingAliasesMap ?? ImmutableDictionary<string, AliasAndUsingDirective>.Empty, this.UsingNamespacesOrTypes, ImmutableArray<AliasAndExternAliasDirective>.Empty), null);
					}
					return this._lazyImports;
				}
			}

			// Token: 0x0600905B RID: 36955 RVA: 0x001FBB24 File Offset: 0x001F9D24
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			internal void Complete(SourceNamespaceSymbol declaringSymbol, CancellationToken cancellationToken)
			{
				for (;;)
				{
					cancellationToken.ThrowIfCancellationRequested();
					CompletionPart nextIncompletePart = this._state.NextIncompletePart;
					if (nextIncompletePart == CompletionPart.None)
					{
						break;
					}
					if (nextIncompletePart != CompletionPart.StartBaseType)
					{
						if (nextIncompletePart != CompletionPart.FinishBaseType)
						{
							this._state.NotePartComplete(CompletionPart.Attributes | CompletionPart.ReturnTypeAttributes | CompletionPart.Parameters | CompletionPart.Type | CompletionPart.StartInterfaces | CompletionPart.FinishInterfaces | CompletionPart.EnumUnderlyingType | CompletionPart.TypeArguments | CompletionPart.TypeParameters | CompletionPart.Members | CompletionPart.TypeMembers | CompletionPart.SynthesizedExplicitImplementations | CompletionPart.StartMemberChecks | CompletionPart.FinishMemberChecks | CompletionPart.MembersCompleted);
						}
						else
						{
							this._state.SpinWaitComplete(CompletionPart.FinishBaseType, cancellationToken);
						}
					}
					else if (this._state.NotePartComplete(CompletionPart.StartBaseType))
					{
						if (!this.Diagnostics.IsDefaultOrEmpty)
						{
							declaringSymbol.DeclaringCompilation.DeclarationDiagnostics.AddRange<Diagnostic>(this.Diagnostics);
						}
						this._state.NotePartComplete(CompletionPart.FinishBaseType);
					}
					this._state.SpinWaitComplete(nextIncompletePart, cancellationToken);
				}
			}

			// Token: 0x04002808 RID: 10248
			[global::System.Runtime.CompilerServices.Nullable(2)]
			private Imports _lazyImports;

			// Token: 0x04002809 RID: 10249
			private SymbolCompletionState _state;

			// Token: 0x0400280A RID: 10250
			[global::System.Runtime.CompilerServices.Nullable(1)]
			public static readonly SourceNamespaceSymbol.MergedGlobalAliasesAndUsings Empty = new SourceNamespaceSymbol.MergedGlobalAliasesAndUsings
			{
				UsingAliasesMap = ImmutableDictionary<string, AliasAndUsingDirective>.Empty,
				UsingNamespacesOrTypes = ImmutableArray<NamespaceOrTypeAndUsingDirective>.Empty,
				Diagnostics = ImmutableArray<Diagnostic>.Empty,
				_lazyImports = Imports.Empty
			};
		}

		// Token: 0x02000938 RID: 2360
		private struct NameToSymbolMapBuilder
		{
			// Token: 0x0600905E RID: 36958 RVA: 0x001FBC0B File Offset: 0x001F9E0B
			public NameToSymbolMapBuilder(int capacity)
			{
				this._dictionary = new Dictionary<string, object>(capacity, StringOrdinalComparer.Instance);
			}

			// Token: 0x0600905F RID: 36959 RVA: 0x001FBC20 File Offset: 0x001F9E20
			public void Add(NamespaceOrTypeSymbol symbol)
			{
				string name = symbol.Name;
				object obj;
				if (this._dictionary.TryGetValue(name, ref obj))
				{
					ArrayBuilder<NamespaceOrTypeSymbol> arrayBuilder = obj as ArrayBuilder<NamespaceOrTypeSymbol>;
					if (arrayBuilder == null)
					{
						arrayBuilder = ArrayBuilder<NamespaceOrTypeSymbol>.GetInstance();
						arrayBuilder.Add((NamespaceOrTypeSymbol)obj);
						this._dictionary[name] = arrayBuilder;
					}
					arrayBuilder.Add(symbol);
					return;
				}
				this._dictionary[name] = symbol;
			}

			// Token: 0x06009060 RID: 36960 RVA: 0x001FBC84 File Offset: 0x001F9E84
			public Dictionary<string, ImmutableArray<NamespaceOrTypeSymbol>> CreateMap()
			{
				Dictionary<string, ImmutableArray<NamespaceOrTypeSymbol>> dictionary = new Dictionary<string, ImmutableArray<NamespaceOrTypeSymbol>>(this._dictionary.Count, StringOrdinalComparer.Instance);
				foreach (KeyValuePair<string, object> keyValuePair in this._dictionary)
				{
					object value = keyValuePair.Value;
					ArrayBuilder<NamespaceOrTypeSymbol> arrayBuilder = value as ArrayBuilder<NamespaceOrTypeSymbol>;
					ImmutableArray<NamespaceOrTypeSymbol> immutableArray;
					if (arrayBuilder != null)
					{
						bool flag = false;
						int num = 0;
						while (num < arrayBuilder.Count && !flag)
						{
							flag |= arrayBuilder[num].Kind == SymbolKind.Namespace;
							num++;
						}
						immutableArray = (flag ? arrayBuilder.ToImmutable() : StaticCast<NamespaceOrTypeSymbol>.From<NamedTypeSymbol>(arrayBuilder.ToDowncastedImmutable<NamedTypeSymbol>()));
						arrayBuilder.Free();
					}
					else
					{
						NamespaceOrTypeSymbol namespaceOrTypeSymbol = (NamespaceOrTypeSymbol)value;
						immutableArray = ((namespaceOrTypeSymbol.Kind == SymbolKind.Namespace) ? ImmutableArray.Create<NamespaceOrTypeSymbol>(namespaceOrTypeSymbol) : StaticCast<NamespaceOrTypeSymbol>.From<NamedTypeSymbol>(ImmutableArray.Create<NamedTypeSymbol>((NamedTypeSymbol)namespaceOrTypeSymbol)));
					}
					dictionary.Add(keyValuePair.Key, immutableArray);
				}
				return dictionary;
			}

			// Token: 0x0400280E RID: 10254
			private readonly Dictionary<string, object> _dictionary;
		}
	}
}
