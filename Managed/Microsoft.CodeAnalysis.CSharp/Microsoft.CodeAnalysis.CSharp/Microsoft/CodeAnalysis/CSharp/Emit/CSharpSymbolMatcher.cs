using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Emit
{
	// Token: 0x02000483 RID: 1155
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class CSharpSymbolMatcher : SymbolMatcher
	{
		// Token: 0x06006072 RID: 24690 RVA: 0x00174D07 File Offset: 0x00172F07
		public CSharpSymbolMatcher(IReadOnlyDictionary<AnonymousTypeKey, AnonymousTypeValue> anonymousTypeMap, SourceAssemblySymbol sourceAssembly, EmitContext sourceContext, SourceAssemblySymbol otherAssembly, EmitContext otherContext, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 0, 1 })] ImmutableDictionary<ISymbolInternal, ImmutableArray<ISymbolInternal>> otherSynthesizedMembersOpt)
		{
			this._defs = new CSharpSymbolMatcher.MatchDefsToSource(sourceContext, otherContext);
			this._symbols = new CSharpSymbolMatcher.MatchSymbols(anonymousTypeMap, sourceAssembly, otherAssembly, otherSynthesizedMembersOpt, new CSharpSymbolMatcher.DeepTranslator(otherAssembly.GetSpecialType(SpecialType.System_Object)));
		}

		// Token: 0x06006073 RID: 24691 RVA: 0x00174D3B File Offset: 0x00172F3B
		public CSharpSymbolMatcher(IReadOnlyDictionary<AnonymousTypeKey, AnonymousTypeValue> anonymousTypeMap, SourceAssemblySymbol sourceAssembly, EmitContext sourceContext, PEAssemblySymbol otherAssembly)
		{
			this._defs = new CSharpSymbolMatcher.MatchDefsToMetadata(sourceContext, otherAssembly);
			this._symbols = new CSharpSymbolMatcher.MatchSymbols(anonymousTypeMap, sourceAssembly, otherAssembly, null, null);
		}

		// Token: 0x06006074 RID: 24692 RVA: 0x00174D64 File Offset: 0x00172F64
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override IDefinition MapDefinition(IDefinition definition)
		{
			Symbol symbol = definition.GetInternalSymbol() as Symbol;
			if (symbol != null)
			{
				Symbol symbol2 = this._symbols.Visit(symbol);
				return (IDefinition)((symbol2 != null) ? symbol2.GetCciAdapter() : null);
			}
			return this._defs.VisitDef(definition);
		}

		// Token: 0x06006075 RID: 24693 RVA: 0x00174DAC File Offset: 0x00172FAC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override INamespace MapNamespace(INamespace @namespace)
		{
			NamespaceSymbol namespaceSymbol = @namespace.GetInternalSymbol() as NamespaceSymbol;
			if (namespaceSymbol != null)
			{
				Symbol symbol = this._symbols.Visit(namespaceSymbol);
				return (INamespace)((symbol != null) ? symbol.GetCciAdapter() : null);
			}
			return null;
		}

		// Token: 0x06006076 RID: 24694 RVA: 0x00174DE8 File Offset: 0x00172FE8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override ITypeReference MapReference(ITypeReference reference)
		{
			Symbol symbol = reference.GetInternalSymbol() as Symbol;
			if (symbol != null)
			{
				Symbol symbol2 = this._symbols.Visit(symbol);
				return (ITypeReference)((symbol2 != null) ? symbol2.GetCciAdapter() : null);
			}
			return null;
		}

		// Token: 0x06006077 RID: 24695 RVA: 0x00174E23 File Offset: 0x00173023
		internal bool TryGetAnonymousTypeName(AnonymousTypeManager.AnonymousTypeTemplateSymbol template, [global::System.Runtime.CompilerServices.Nullable(2)] [NotNullWhen(true)] out string name, out int index)
		{
			return this._symbols.TryGetAnonymousTypeName(template, out name, out index);
		}

		// Token: 0x0400194E RID: 6478
		private readonly CSharpSymbolMatcher.MatchDefs _defs;

		// Token: 0x0400194F RID: 6479
		private readonly CSharpSymbolMatcher.MatchSymbols _symbols;

		// Token: 0x020008A5 RID: 2213
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private abstract class MatchDefs
		{
			// Token: 0x06008C3B RID: 35899 RVA: 0x001F2613 File Offset: 0x001F0813
			public MatchDefs(EmitContext sourceContext)
			{
				this._sourceContext = sourceContext;
			}

			// Token: 0x06008C3C RID: 35900 RVA: 0x001F2632 File Offset: 0x001F0832
			[return: global::System.Runtime.CompilerServices.Nullable(2)]
			public IDefinition VisitDef(IDefinition def)
			{
				return this._matches.GetOrAdd(def, new Func<IDefinition, IDefinition>(this.VisitDefInternal));
			}

			// Token: 0x06008C3D RID: 35901 RVA: 0x001F264C File Offset: 0x001F084C
			[return: global::System.Runtime.CompilerServices.Nullable(2)]
			private IDefinition VisitDefInternal(IDefinition def)
			{
				ITypeDefinition typeDefinition = def as ITypeDefinition;
				if (typeDefinition == null)
				{
					ITypeDefinitionMember typeDefinitionMember = def as ITypeDefinitionMember;
					if (typeDefinitionMember != null)
					{
						ITypeDefinition typeDefinition2 = (ITypeDefinition)this.VisitDef(typeDefinitionMember.ContainingTypeDefinition);
						if (typeDefinition2 == null)
						{
							return null;
						}
						IFieldDefinition fieldDefinition = def as IFieldDefinition;
						if (fieldDefinition != null)
						{
							return CSharpSymbolMatcher.MatchDefs.VisitTypeMembers<IFieldDefinition>(typeDefinition2, fieldDefinition, new Func<ITypeDefinition, IEnumerable<IFieldDefinition>>(this.GetFields), (IFieldDefinition a, IFieldDefinition b) => StringOrdinalComparer.Equals(a.Name, b.Name));
						}
					}
					throw ExceptionUtilities.UnexpectedValue(def);
				}
				INamespaceTypeDefinition namespaceTypeDefinition = typeDefinition.AsNamespaceTypeDefinition(this._sourceContext);
				if (namespaceTypeDefinition != null)
				{
					return this.VisitNamespaceType(namespaceTypeDefinition);
				}
				INestedTypeDefinition nestedTypeDefinition = typeDefinition.AsNestedTypeDefinition(this._sourceContext);
				ITypeDefinition typeDefinition3 = (ITypeDefinition)this.VisitDef(nestedTypeDefinition.ContainingTypeDefinition);
				if (typeDefinition3 == null)
				{
					return null;
				}
				return CSharpSymbolMatcher.MatchDefs.VisitTypeMembers<INestedTypeDefinition>(typeDefinition3, nestedTypeDefinition, new Func<ITypeDefinition, IEnumerable<INestedTypeDefinition>>(this.GetNestedTypes), (INestedTypeDefinition a, INestedTypeDefinition b) => StringOrdinalComparer.Equals(a.Name, b.Name));
			}

			// Token: 0x06008C3E RID: 35902
			protected abstract IEnumerable<INamespaceTypeDefinition> GetTopLevelTypes();

			// Token: 0x06008C3F RID: 35903
			protected abstract IEnumerable<INestedTypeDefinition> GetNestedTypes(ITypeDefinition def);

			// Token: 0x06008C40 RID: 35904
			protected abstract IEnumerable<IFieldDefinition> GetFields(ITypeDefinition def);

			// Token: 0x06008C41 RID: 35905 RVA: 0x001F2744 File Offset: 0x001F0944
			[return: global::System.Runtime.CompilerServices.Nullable(2)]
			private INamespaceTypeDefinition VisitNamespaceType(INamespaceTypeDefinition def)
			{
				if (!string.IsNullOrEmpty(def.NamespaceName))
				{
					return null;
				}
				INamespaceTypeDefinition namespaceTypeDefinition;
				this.GetTopLevelTypesByName().TryGetValue(def.Name, ref namespaceTypeDefinition);
				return namespaceTypeDefinition;
			}

			// Token: 0x06008C42 RID: 35906 RVA: 0x001F2778 File Offset: 0x001F0978
			private IReadOnlyDictionary<string, INamespaceTypeDefinition> GetTopLevelTypesByName()
			{
				if (this._lazyTopLevelTypes == null)
				{
					Dictionary<string, INamespaceTypeDefinition> dictionary = new Dictionary<string, INamespaceTypeDefinition>(StringOrdinalComparer.Instance);
					foreach (INamespaceTypeDefinition namespaceTypeDefinition in this.GetTopLevelTypes())
					{
						if (string.IsNullOrEmpty(namespaceTypeDefinition.NamespaceName))
						{
							dictionary.Add(namespaceTypeDefinition.Name, namespaceTypeDefinition);
						}
					}
					Interlocked.CompareExchange<IReadOnlyDictionary<string, INamespaceTypeDefinition>>(ref this._lazyTopLevelTypes, dictionary, null);
				}
				return this._lazyTopLevelTypes;
			}

			// Token: 0x06008C43 RID: 35907 RVA: 0x001F2800 File Offset: 0x001F0A00
			private static T VisitTypeMembers<T>(ITypeDefinition otherContainer, T member, Func<ITypeDefinition, IEnumerable<T>> getMembers, Func<T, T, bool> predicate) where T : class, ITypeDefinitionMember
			{
				return Enumerable.FirstOrDefault<T>(getMembers.Invoke(otherContainer), (T otherMember) => predicate.Invoke(member, otherMember));
			}

			// Token: 0x040025D5 RID: 9685
			private readonly EmitContext _sourceContext;

			// Token: 0x040025D6 RID: 9686
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 2 })]
			private readonly ConcurrentDictionary<IDefinition, IDefinition> _matches = new ConcurrentDictionary<IDefinition, IDefinition>(ReferenceEqualityComparer.Instance);

			// Token: 0x040025D7 RID: 9687
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1 })]
			private IReadOnlyDictionary<string, INamespaceTypeDefinition> _lazyTopLevelTypes;
		}

		// Token: 0x020008A6 RID: 2214
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private sealed class MatchDefsToMetadata : CSharpSymbolMatcher.MatchDefs
		{
			// Token: 0x06008C44 RID: 35908 RVA: 0x001F2839 File Offset: 0x001F0A39
			public MatchDefsToMetadata(EmitContext sourceContext, PEAssemblySymbol otherAssembly)
				: base(sourceContext)
			{
				this._otherAssembly = otherAssembly;
			}

			// Token: 0x06008C45 RID: 35909 RVA: 0x001F2849 File Offset: 0x001F0A49
			protected override IEnumerable<INamespaceTypeDefinition> GetTopLevelTypes()
			{
				ArrayBuilder<INamespaceTypeDefinition> instance = ArrayBuilder<INamespaceTypeDefinition>.GetInstance();
				CSharpSymbolMatcher.MatchDefsToMetadata.GetTopLevelTypes(instance, this._otherAssembly.GlobalNamespace);
				return instance.ToArrayAndFree();
			}

			// Token: 0x06008C46 RID: 35910 RVA: 0x001F2866 File Offset: 0x001F0A66
			protected override IEnumerable<INestedTypeDefinition> GetNestedTypes(ITypeDefinition def)
			{
				return Enumerable.Cast<INestedTypeDefinition>(((PENamedTypeSymbol)def).GetTypeMembers());
			}

			// Token: 0x06008C47 RID: 35911 RVA: 0x001F287D File Offset: 0x001F0A7D
			protected override IEnumerable<IFieldDefinition> GetFields(ITypeDefinition def)
			{
				return Enumerable.Cast<IFieldDefinition>(((PENamedTypeSymbol)def).GetFieldsToEmit());
			}

			// Token: 0x06008C48 RID: 35912 RVA: 0x001F2890 File Offset: 0x001F0A90
			private static void GetTopLevelTypes(ArrayBuilder<INamespaceTypeDefinition> builder, NamespaceSymbol @namespace)
			{
				foreach (Symbol symbol in @namespace.GetMembers())
				{
					if (symbol.Kind == SymbolKind.Namespace)
					{
						CSharpSymbolMatcher.MatchDefsToMetadata.GetTopLevelTypes(builder, (NamespaceSymbol)symbol);
					}
					else
					{
						builder.Add((INamespaceTypeDefinition)symbol.GetCciAdapter());
					}
				}
			}

			// Token: 0x040025D8 RID: 9688
			private readonly PEAssemblySymbol _otherAssembly;
		}

		// Token: 0x020008A7 RID: 2215
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private sealed class MatchDefsToSource : CSharpSymbolMatcher.MatchDefs
		{
			// Token: 0x06008C49 RID: 35913 RVA: 0x001F28E8 File Offset: 0x001F0AE8
			public MatchDefsToSource(EmitContext sourceContext, EmitContext otherContext)
				: base(sourceContext)
			{
				this._otherContext = otherContext;
			}

			// Token: 0x06008C4A RID: 35914 RVA: 0x001F28F8 File Offset: 0x001F0AF8
			protected override IEnumerable<INamespaceTypeDefinition> GetTopLevelTypes()
			{
				return this._otherContext.Module.GetTopLevelTypeDefinitions(this._otherContext);
			}

			// Token: 0x06008C4B RID: 35915 RVA: 0x001F2910 File Offset: 0x001F0B10
			protected override IEnumerable<INestedTypeDefinition> GetNestedTypes(ITypeDefinition def)
			{
				return def.GetNestedTypes(this._otherContext);
			}

			// Token: 0x06008C4C RID: 35916 RVA: 0x001F291E File Offset: 0x001F0B1E
			protected override IEnumerable<IFieldDefinition> GetFields(ITypeDefinition def)
			{
				return def.GetFields(this._otherContext);
			}

			// Token: 0x040025D9 RID: 9689
			private readonly EmitContext _otherContext;
		}

		// Token: 0x020008A8 RID: 2216
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		private sealed class MatchSymbols : CSharpSymbolVisitor<Symbol>
		{
			// Token: 0x06008C4D RID: 35917 RVA: 0x001F292C File Offset: 0x001F0B2C
			public MatchSymbols(IReadOnlyDictionary<AnonymousTypeKey, AnonymousTypeValue> anonymousTypeMap, SourceAssemblySymbol sourceAssembly, AssemblySymbol otherAssembly, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 0, 1 })] ImmutableDictionary<ISymbolInternal, ImmutableArray<ISymbolInternal>> otherSynthesizedMembers, [global::System.Runtime.CompilerServices.Nullable(2)] CSharpSymbolMatcher.DeepTranslator deepTranslator)
			{
				this._anonymousTypeMap = anonymousTypeMap;
				this._sourceAssembly = sourceAssembly;
				this._otherAssembly = otherAssembly;
				this._otherSynthesizedMembers = otherSynthesizedMembers;
				this._comparer = new CSharpSymbolMatcher.MatchSymbols.SymbolComparer(this, deepTranslator);
			}

			// Token: 0x06008C4E RID: 35918 RVA: 0x001F298C File Offset: 0x001F0B8C
			internal bool TryGetAnonymousTypeName(AnonymousTypeManager.AnonymousTypeTemplateSymbol type, [global::System.Runtime.CompilerServices.Nullable(2)] [NotNullWhen(true)] out string name, out int index)
			{
				AnonymousTypeValue anonymousTypeValue;
				if (this.TryFindAnonymousType(type, out anonymousTypeValue))
				{
					name = anonymousTypeValue.Name;
					index = anonymousTypeValue.UniqueIndex;
					return true;
				}
				name = null;
				index = -1;
				return false;
			}

			// Token: 0x06008C4F RID: 35919 RVA: 0x001F29BD File Offset: 0x001F0BBD
			public override Symbol DefaultVisit(Symbol symbol)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x06008C50 RID: 35920 RVA: 0x001F29C4 File Offset: 0x001F0BC4
			[return: global::System.Runtime.CompilerServices.Nullable(2)]
			public override Symbol Visit(Symbol symbol)
			{
				return this._matches.GetOrAdd(symbol, new Func<Symbol, Symbol>(base.Visit));
			}

			// Token: 0x06008C51 RID: 35921 RVA: 0x001F29E0 File Offset: 0x001F0BE0
			[return: global::System.Runtime.CompilerServices.Nullable(2)]
			public override Symbol VisitArrayType(ArrayTypeSymbol symbol)
			{
				TypeSymbol typeSymbol = (TypeSymbol)this.Visit(symbol.ElementType);
				if (typeSymbol == null)
				{
					return null;
				}
				ImmutableArray<CustomModifier> immutableArray = this.VisitCustomModifiers(symbol.ElementTypeWithAnnotations.CustomModifiers);
				if (symbol.IsSZArray)
				{
					return ArrayTypeSymbol.CreateSZArray(this._otherAssembly, symbol.ElementTypeWithAnnotations.WithTypeAndModifiers(typeSymbol, immutableArray));
				}
				return ArrayTypeSymbol.CreateMDArray(this._otherAssembly, symbol.ElementTypeWithAnnotations.WithTypeAndModifiers(typeSymbol, immutableArray), symbol.Rank, symbol.Sizes, symbol.LowerBounds);
			}

			// Token: 0x06008C52 RID: 35922 RVA: 0x001F2A6A File Offset: 0x001F0C6A
			[return: global::System.Runtime.CompilerServices.Nullable(2)]
			public override Symbol VisitEvent(EventSymbol symbol)
			{
				return this.VisitNamedTypeMember<EventSymbol>(symbol, new Func<EventSymbol, EventSymbol, bool>(this.AreEventsEqual));
			}

			// Token: 0x06008C53 RID: 35923 RVA: 0x001F2A7F File Offset: 0x001F0C7F
			[return: global::System.Runtime.CompilerServices.Nullable(2)]
			public override Symbol VisitField(FieldSymbol symbol)
			{
				return this.VisitNamedTypeMember<FieldSymbol>(symbol, new Func<FieldSymbol, FieldSymbol, bool>(this.AreFieldsEqual));
			}

			// Token: 0x06008C54 RID: 35924 RVA: 0x001F2A94 File Offset: 0x001F0C94
			[return: global::System.Runtime.CompilerServices.Nullable(2)]
			public override Symbol VisitMethod(MethodSymbol symbol)
			{
				return this.VisitNamedTypeMember<MethodSymbol>(symbol, new Func<MethodSymbol, MethodSymbol, bool>(this.AreMethodsEqual));
			}

			// Token: 0x06008C55 RID: 35925 RVA: 0x001F2AAC File Offset: 0x001F0CAC
			[return: global::System.Runtime.CompilerServices.Nullable(2)]
			public override Symbol VisitModule(ModuleSymbol module)
			{
				AssemblySymbol assemblySymbol = (AssemblySymbol)this.Visit(module.ContainingAssembly);
				if (assemblySymbol == null)
				{
					return null;
				}
				if (module.Ordinal == 0)
				{
					return assemblySymbol.Modules[0];
				}
				for (int i = 1; i < assemblySymbol.Modules.Length; i++)
				{
					ModuleSymbol moduleSymbol = assemblySymbol.Modules[i];
					if (StringComparer.Ordinal.Equals(moduleSymbol.Name, module.Name))
					{
						return moduleSymbol;
					}
				}
				return null;
			}

			// Token: 0x06008C56 RID: 35926 RVA: 0x001F2B2C File Offset: 0x001F0D2C
			[return: global::System.Runtime.CompilerServices.Nullable(2)]
			public override Symbol VisitAssembly(AssemblySymbol assembly)
			{
				if (assembly.IsLinked)
				{
					return assembly;
				}
				if (CSharpSymbolMatcher.MatchSymbols.IdentityEqualIgnoringVersionWildcard(assembly, this._sourceAssembly))
				{
					return this._otherAssembly;
				}
				foreach (AssemblySymbol assemblySymbol in this._otherAssembly.Modules[0].ReferencedAssemblySymbols)
				{
					if (CSharpSymbolMatcher.MatchSymbols.IdentityEqualIgnoringVersionWildcard(assembly, assemblySymbol))
					{
						return assemblySymbol;
					}
				}
				return null;
			}

			// Token: 0x06008C57 RID: 35927 RVA: 0x001F2B9C File Offset: 0x001F0D9C
			private static bool IdentityEqualIgnoringVersionWildcard(AssemblySymbol left, AssemblySymbol right)
			{
				AssemblyIdentity identity = left.Identity;
				AssemblyIdentity identity2 = right.Identity;
				return AssemblyIdentityComparer.SimpleNameComparer.Equals(identity.Name, identity2.Name) && (left.AssemblyVersionPattern ?? identity.Version).Equals(right.AssemblyVersionPattern ?? identity2.Version) && AssemblyIdentity.EqualIgnoringNameAndVersion(identity, identity2);
			}

			// Token: 0x06008C58 RID: 35928 RVA: 0x001F2C00 File Offset: 0x001F0E00
			[return: global::System.Runtime.CompilerServices.Nullable(2)]
			public override Symbol VisitNamespace(NamespaceSymbol @namespace)
			{
				Symbol symbol = this.Visit(@namespace.ContainingSymbol);
				SymbolKind kind = symbol.Kind;
				if (kind == SymbolKind.NetModule)
				{
					return ((ModuleSymbol)symbol).GlobalNamespace;
				}
				if (kind != SymbolKind.Namespace)
				{
					throw ExceptionUtilities.UnexpectedValue(symbol.Kind);
				}
				return this.FindMatchingMember<NamespaceSymbol>(symbol, @namespace, new Func<NamespaceSymbol, NamespaceSymbol, bool>(this.AreNamespacesEqual));
			}

			// Token: 0x06008C59 RID: 35929 RVA: 0x001F2C5E File Offset: 0x001F0E5E
			public override Symbol VisitDynamicType(DynamicTypeSymbol symbol)
			{
				return this._otherAssembly.GetSpecialType(SpecialType.System_Object);
			}

			// Token: 0x06008C5A RID: 35930 RVA: 0x001F2C6C File Offset: 0x001F0E6C
			[return: global::System.Runtime.CompilerServices.Nullable(2)]
			public override Symbol VisitNamedType(NamedTypeSymbol sourceType)
			{
				NamedTypeSymbol originalDefinition = sourceType.OriginalDefinition;
				if (originalDefinition != sourceType)
				{
					CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
					ImmutableArray<TypeWithAnnotations> allTypeArguments = sourceType.GetAllTypeArguments(ref discarded);
					NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)this.Visit(originalDefinition);
					if (namedTypeSymbol == null)
					{
						return null;
					}
					ImmutableArray<TypeParameterSymbol> allTypeParameters = namedTypeSymbol.GetAllTypeParameters();
					bool translationFailed = false;
					ImmutableArray<TypeWithAnnotations> immutableArray = allTypeArguments.SelectAsArray(delegate(TypeWithAnnotations t, CSharpSymbolMatcher.MatchSymbols v)
					{
						TypeSymbol typeSymbol = (TypeSymbol)v.Visit(t.Type);
						if (typeSymbol == null)
						{
							translationFailed = true;
							typeSymbol = t.Type;
						}
						return t.WithTypeAndModifiers(typeSymbol, v.VisitCustomModifiers(t.CustomModifiers));
					}, this);
					if (translationFailed)
					{
						return null;
					}
					return new TypeMap(allTypeParameters, immutableArray, true).SubstituteNamedType(namedTypeSymbol);
				}
				else
				{
					Symbol symbol = this.Visit(sourceType.ContainingSymbol);
					if (symbol == null)
					{
						return null;
					}
					SymbolKind kind = symbol.Kind;
					if (kind == SymbolKind.NamedType)
					{
						return this.FindMatchingMember<NamedTypeSymbol>(symbol, sourceType, new Func<NamedTypeSymbol, NamedTypeSymbol, bool>(this.AreNamedTypesEqual));
					}
					if (kind != SymbolKind.Namespace)
					{
						throw ExceptionUtilities.UnexpectedValue(symbol.Kind);
					}
					AnonymousTypeManager.AnonymousTypeTemplateSymbol anonymousTypeTemplateSymbol = sourceType as AnonymousTypeManager.AnonymousTypeTemplateSymbol;
					if (anonymousTypeTemplateSymbol != null)
					{
						AnonymousTypeValue anonymousTypeValue;
						this.TryFindAnonymousType(anonymousTypeTemplateSymbol, out anonymousTypeValue);
						ITypeDefinition type = anonymousTypeValue.Type;
						return (NamedTypeSymbol)((type != null) ? type.GetInternalSymbol() : null);
					}
					if (sourceType.IsAnonymousType)
					{
						return this.Visit(AnonymousTypeManager.TranslateAnonymousTypeSymbol(sourceType));
					}
					return this.FindMatchingMember<NamedTypeSymbol>(symbol, sourceType, new Func<NamedTypeSymbol, NamedTypeSymbol, bool>(this.AreNamedTypesEqual));
				}
			}

			// Token: 0x06008C5B RID: 35931 RVA: 0x001F2D95 File Offset: 0x001F0F95
			public override Symbol VisitParameter(ParameterSymbol parameter)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x06008C5C RID: 35932 RVA: 0x001F2D9C File Offset: 0x001F0F9C
			[return: global::System.Runtime.CompilerServices.Nullable(2)]
			public override Symbol VisitPointerType(PointerTypeSymbol symbol)
			{
				TypeSymbol typeSymbol = (TypeSymbol)this.Visit(symbol.PointedAtType);
				if (typeSymbol == null)
				{
					return null;
				}
				ImmutableArray<CustomModifier> immutableArray = this.VisitCustomModifiers(symbol.PointedAtTypeWithAnnotations.CustomModifiers);
				return new PointerTypeSymbol(symbol.PointedAtTypeWithAnnotations.WithTypeAndModifiers(typeSymbol, immutableArray));
			}

			// Token: 0x06008C5D RID: 35933 RVA: 0x001F2DEC File Offset: 0x001F0FEC
			[return: global::System.Runtime.CompilerServices.Nullable(2)]
			public override Symbol VisitFunctionPointerType(FunctionPointerTypeSymbol symbol)
			{
				FunctionPointerMethodSymbol signature = symbol.Signature;
				TypeSymbol typeSymbol = (TypeSymbol)this.Visit(signature.ReturnType);
				if (typeSymbol == null)
				{
					return null;
				}
				ImmutableArray<CustomModifier> immutableArray = this.VisitCustomModifiers(signature.RefCustomModifiers);
				TypeWithAnnotations typeWithAnnotations = signature.ReturnTypeWithAnnotations.WithTypeAndModifiers(typeSymbol, this.VisitCustomModifiers(signature.ReturnTypeWithAnnotations.CustomModifiers));
				ImmutableArray<TypeWithAnnotations> immutableArray2 = ImmutableArray<TypeWithAnnotations>.Empty;
				ImmutableArray<ImmutableArray<CustomModifier>> immutableArray3 = default(ImmutableArray<ImmutableArray<CustomModifier>>);
				if (signature.ParameterCount > 0)
				{
					ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance(signature.ParameterCount);
					ArrayBuilder<ImmutableArray<CustomModifier>> instance2 = ArrayBuilder<ImmutableArray<CustomModifier>>.GetInstance(signature.ParameterCount);
					foreach (ParameterSymbol parameterSymbol in signature.Parameters)
					{
						TypeSymbol typeSymbol2 = (TypeSymbol)this.Visit(parameterSymbol.Type);
						if (typeSymbol2 == null)
						{
							instance.Free();
							instance2.Free();
							return null;
						}
						instance2.Add(this.VisitCustomModifiers(parameterSymbol.RefCustomModifiers));
						instance.Add(parameterSymbol.TypeWithAnnotations.WithTypeAndModifiers(typeSymbol2, this.VisitCustomModifiers(parameterSymbol.TypeWithAnnotations.CustomModifiers)));
					}
					immutableArray2 = instance.ToImmutableAndFree();
					immutableArray3 = instance2.ToImmutableAndFree();
				}
				return symbol.SubstituteTypeSymbol(typeWithAnnotations, immutableArray2, immutableArray, immutableArray3);
			}

			// Token: 0x06008C5E RID: 35934 RVA: 0x001F2F2D File Offset: 0x001F112D
			[return: global::System.Runtime.CompilerServices.Nullable(2)]
			public override Symbol VisitProperty(PropertySymbol symbol)
			{
				return this.VisitNamedTypeMember<PropertySymbol>(symbol, new Func<PropertySymbol, PropertySymbol, bool>(this.ArePropertiesEqual));
			}

			// Token: 0x06008C5F RID: 35935 RVA: 0x001F2F44 File Offset: 0x001F1144
			public override Symbol VisitTypeParameter(TypeParameterSymbol symbol)
			{
				IndexedTypeParameterSymbol indexedTypeParameterSymbol = symbol as IndexedTypeParameterSymbol;
				if (indexedTypeParameterSymbol != null)
				{
					return indexedTypeParameterSymbol;
				}
				Symbol symbol2 = this.Visit(symbol.ContainingSymbol);
				SymbolKind kind = symbol2.Kind;
				ImmutableArray<TypeParameterSymbol> immutableArray;
				if (kind != SymbolKind.ErrorType)
				{
					if (kind == SymbolKind.Method)
					{
						immutableArray = ((MethodSymbol)symbol2).TypeParameters;
						goto IL_005F;
					}
					if (kind != SymbolKind.NamedType)
					{
						throw ExceptionUtilities.UnexpectedValue(symbol2.Kind);
					}
				}
				immutableArray = ((NamedTypeSymbol)symbol2).TypeParameters;
				IL_005F:
				ImmutableArray<TypeParameterSymbol> immutableArray2 = immutableArray;
				return immutableArray2[symbol.Ordinal];
			}

			// Token: 0x06008C60 RID: 35936 RVA: 0x001F2FBF File Offset: 0x001F11BF
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			private ImmutableArray<CustomModifier> VisitCustomModifiers([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<CustomModifier> modifiers)
			{
				return modifiers.SelectAsArray<CustomModifier, CustomModifier>(new Func<CustomModifier, CustomModifier>(this.VisitCustomModifier));
			}

			// Token: 0x06008C61 RID: 35937 RVA: 0x001F2FD4 File Offset: 0x001F11D4
			private CustomModifier VisitCustomModifier(CustomModifier modifier)
			{
				NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)this.Visit(((CSharpCustomModifier)modifier).ModifierSymbol);
				if (!modifier.IsOptional)
				{
					return CSharpCustomModifier.CreateRequired(namedTypeSymbol);
				}
				return CSharpCustomModifier.CreateOptional(namedTypeSymbol);
			}

			// Token: 0x06008C62 RID: 35938 RVA: 0x001F300D File Offset: 0x001F120D
			internal bool TryFindAnonymousType(AnonymousTypeManager.AnonymousTypeTemplateSymbol type, out AnonymousTypeValue otherType)
			{
				return this._anonymousTypeMap.TryGetValue(type.GetAnonymousTypeKey(), ref otherType);
			}

			// Token: 0x06008C63 RID: 35939 RVA: 0x001F3024 File Offset: 0x001F1224
			[return: global::System.Runtime.CompilerServices.Nullable(2)]
			private Symbol VisitNamedTypeMember<[global::System.Runtime.CompilerServices.Nullable(0)] T>(T member, Func<T, T, bool> predicate) where T : Symbol
			{
				NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)this.Visit(member.ContainingType);
				if (namedTypeSymbol == null)
				{
					return null;
				}
				return this.FindMatchingMember<T>(namedTypeSymbol, member, predicate);
			}

			// Token: 0x06008C64 RID: 35940 RVA: 0x001F305C File Offset: 0x001F125C
			[return: global::System.Runtime.CompilerServices.Nullable(2)]
			private T FindMatchingMember<[global::System.Runtime.CompilerServices.Nullable(0)] T>(ISymbolInternal otherTypeOrNamespace, T sourceMember, Func<T, T, bool> predicate) where T : Symbol
			{
				ImmutableArray<ISymbolInternal> immutableArray;
				if (this._otherMembers.GetOrAdd(otherTypeOrNamespace, new Func<ISymbolInternal, IReadOnlyDictionary<string, ImmutableArray<ISymbolInternal>>>(this.GetAllEmittedMembers)).TryGetValue(sourceMember.MetadataName, ref immutableArray))
				{
					foreach (ISymbolInternal symbolInternal in immutableArray)
					{
						T t = symbolInternal as T;
						if (t != null && predicate.Invoke(sourceMember, t))
						{
							return t;
						}
					}
				}
				return default(T);
			}

			// Token: 0x06008C65 RID: 35941 RVA: 0x001F30D7 File Offset: 0x001F12D7
			private bool AreArrayTypesEqual(ArrayTypeSymbol type, ArrayTypeSymbol other)
			{
				return type.HasSameShapeAs(other) && this.AreTypesEqual(type.ElementType, other.ElementType);
			}

			// Token: 0x06008C66 RID: 35942 RVA: 0x001F30F6 File Offset: 0x001F12F6
			private bool AreEventsEqual(EventSymbol @event, EventSymbol other)
			{
				return this._comparer.Equals(@event.Type, other.Type);
			}

			// Token: 0x06008C67 RID: 35943 RVA: 0x001F310F File Offset: 0x001F130F
			private bool AreFieldsEqual(FieldSymbol field, FieldSymbol other)
			{
				return this._comparer.Equals(field.Type, other.Type);
			}

			// Token: 0x06008C68 RID: 35944 RVA: 0x001F3128 File Offset: 0x001F1328
			private bool AreMethodsEqual(MethodSymbol method, MethodSymbol other)
			{
				method = CSharpSymbolMatcher.MatchSymbols.SubstituteTypeParameters(method);
				other = CSharpSymbolMatcher.MatchSymbols.SubstituteTypeParameters(other);
				return this._comparer.Equals(method.ReturnType, other.ReturnType) && method.RefKind.Equals(other.RefKind) && method.Parameters.SequenceEqual<ParameterSymbol, ParameterSymbol>(other.Parameters, new Func<ParameterSymbol, ParameterSymbol, bool>(this.AreParametersEqual)) && method.TypeParameters.SequenceEqual<TypeParameterSymbol, TypeParameterSymbol>(other.TypeParameters, new Func<TypeParameterSymbol, TypeParameterSymbol, bool>(this.AreTypesEqual));
			}

			// Token: 0x06008C69 RID: 35945 RVA: 0x001F31C0 File Offset: 0x001F13C0
			private static MethodSymbol SubstituteTypeParameters(MethodSymbol method)
			{
				int length = method.TypeParameters.Length;
				if (length == 0)
				{
					return method;
				}
				return method.Construct(IndexedTypeParameterSymbol.Take(length));
			}

			// Token: 0x06008C6A RID: 35946 RVA: 0x001F31ED File Offset: 0x001F13ED
			private bool AreNamedTypesEqual(NamedTypeSymbol type, NamedTypeSymbol other)
			{
				return type.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics.SequenceEqual<TypeWithAnnotations, TypeWithAnnotations>(other.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics, new Func<TypeWithAnnotations, TypeWithAnnotations, bool>(this.AreTypesEqual));
			}

			// Token: 0x06008C6B RID: 35947 RVA: 0x001F320C File Offset: 0x001F140C
			private bool AreNamespacesEqual(NamespaceSymbol @namespace, NamespaceSymbol other)
			{
				return true;
			}

			// Token: 0x06008C6C RID: 35948 RVA: 0x001F320F File Offset: 0x001F140F
			private bool AreParametersEqual(ParameterSymbol parameter, ParameterSymbol other)
			{
				return StringOrdinalComparer.Equals(parameter.MetadataName, other.MetadataName) && parameter.RefKind == other.RefKind && this._comparer.Equals(parameter.Type, other.Type);
			}

			// Token: 0x06008C6D RID: 35949 RVA: 0x001F324B File Offset: 0x001F144B
			private bool ArePointerTypesEqual(PointerTypeSymbol type, PointerTypeSymbol other)
			{
				return this.AreTypesEqual(type.PointedAtType, other.PointedAtType);
			}

			// Token: 0x06008C6E RID: 35950 RVA: 0x001F3260 File Offset: 0x001F1460
			private bool AreFunctionPointerTypesEqual(FunctionPointerTypeSymbol type, FunctionPointerTypeSymbol other)
			{
				FunctionPointerMethodSymbol signature = type.Signature;
				FunctionPointerMethodSymbol signature2 = other.Signature;
				return signature.RefKind == signature2.RefKind && this.AreTypesEqual(signature.ReturnTypeWithAnnotations, signature2.ReturnTypeWithAnnotations) && signature.Parameters.SequenceEqual<ParameterSymbol, ParameterSymbol>(signature2.Parameters, new Func<ParameterSymbol, ParameterSymbol, bool>(this.AreFunctionPointerParametersEqual));
			}

			// Token: 0x06008C6F RID: 35951 RVA: 0x001F32BC File Offset: 0x001F14BC
			private bool AreFunctionPointerParametersEqual(ParameterSymbol param, ParameterSymbol otherParam)
			{
				return param.RefKind == otherParam.RefKind && this.AreTypesEqual(param.TypeWithAnnotations, otherParam.TypeWithAnnotations);
			}

			// Token: 0x06008C70 RID: 35952 RVA: 0x001F32E0 File Offset: 0x001F14E0
			[Conditional("DEBUG")]
			private static void ValidateFunctionPointerParamOrReturn(TypeWithAnnotations type, RefKind refKind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<CustomModifier> refCustomModifiers, bool allowOut)
			{
			}

			// Token: 0x06008C71 RID: 35953 RVA: 0x001F32E4 File Offset: 0x001F14E4
			private bool ArePropertiesEqual(PropertySymbol property, PropertySymbol other)
			{
				return this._comparer.Equals(property.Type, other.Type) && property.RefKind.Equals(other.RefKind) && property.Parameters.SequenceEqual<ParameterSymbol, ParameterSymbol>(other.Parameters, new Func<ParameterSymbol, ParameterSymbol, bool>(this.AreParametersEqual));
			}

			// Token: 0x06008C72 RID: 35954 RVA: 0x001F334A File Offset: 0x001F154A
			private static bool AreTypeParametersEqual(TypeParameterSymbol type, TypeParameterSymbol other)
			{
				return true;
			}

			// Token: 0x06008C73 RID: 35955 RVA: 0x001F334D File Offset: 0x001F154D
			private bool AreTypesEqual(TypeWithAnnotations type, TypeWithAnnotations other)
			{
				return this.AreTypesEqual(type.Type, other.Type);
			}

			// Token: 0x06008C74 RID: 35956 RVA: 0x001F3364 File Offset: 0x001F1564
			private bool AreTypesEqual(TypeSymbol type, TypeSymbol other)
			{
				if (type.Kind != other.Kind)
				{
					return false;
				}
				SymbolKind kind = type.Kind;
				if (kind <= SymbolKind.NamedType)
				{
					if (kind == SymbolKind.ArrayType)
					{
						return this.AreArrayTypesEqual((ArrayTypeSymbol)type, (ArrayTypeSymbol)other);
					}
					if (kind == SymbolKind.ErrorType || kind == SymbolKind.NamedType)
					{
						return this.AreNamedTypesEqual((NamedTypeSymbol)type, (NamedTypeSymbol)other);
					}
				}
				else
				{
					if (kind == SymbolKind.PointerType)
					{
						return this.ArePointerTypesEqual((PointerTypeSymbol)type, (PointerTypeSymbol)other);
					}
					if (kind == SymbolKind.TypeParameter)
					{
						return CSharpSymbolMatcher.MatchSymbols.AreTypeParametersEqual((TypeParameterSymbol)type, (TypeParameterSymbol)other);
					}
					if (kind == SymbolKind.FunctionPointerType)
					{
						return this.AreFunctionPointerTypesEqual((FunctionPointerTypeSymbol)type, (FunctionPointerTypeSymbol)other);
					}
				}
				throw ExceptionUtilities.UnexpectedValue(type.Kind);
			}

			// Token: 0x06008C75 RID: 35957 RVA: 0x001F341C File Offset: 0x001F161C
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 0, 1 })]
			private IReadOnlyDictionary<string, ImmutableArray<ISymbolInternal>> GetAllEmittedMembers(ISymbolInternal symbol)
			{
				ArrayBuilder<ISymbolInternal> instance = ArrayBuilder<ISymbolInternal>.GetInstance();
				if (symbol.Kind == SymbolKind.NamedType)
				{
					NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)symbol;
					instance.AddRange(namedTypeSymbol.GetEventsToEmit());
					instance.AddRange(namedTypeSymbol.GetFieldsToEmit());
					instance.AddRange(namedTypeSymbol.GetMethodsToEmit());
					instance.AddRange<NamedTypeSymbol>(namedTypeSymbol.GetTypeMembers());
					instance.AddRange(namedTypeSymbol.GetPropertiesToEmit());
				}
				else
				{
					instance.AddRange<Symbol>(((NamespaceSymbol)symbol).GetMembers());
				}
				ImmutableArray<ISymbolInternal> immutableArray;
				if (this._otherSynthesizedMembers != null && this._otherSynthesizedMembers.TryGetValue(symbol, out immutableArray))
				{
					instance.AddRange(immutableArray);
				}
				IReadOnlyDictionary<string, ImmutableArray<ISymbolInternal>> readOnlyDictionary = instance.ToDictionary<string>(([global::System.Runtime.CompilerServices.Nullable(1)] ISymbolInternal s) => s.MetadataName, StringOrdinalComparer.Instance);
				instance.Free();
				return readOnlyDictionary;
			}

			// Token: 0x06008C76 RID: 35958 RVA: 0x001F34E0 File Offset: 0x001F16E0
			[CompilerGenerated]
			internal static bool <ValidateFunctionPointerParamOrReturn>g__verifyRefModifiers|42_0([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<CustomModifier> modifiers, RefKind refKind, bool allowOut)
			{
				if (refKind != RefKind.Out)
				{
					if (refKind != RefKind.In)
					{
						goto IL_001A;
					}
				}
				else if (!allowOut)
				{
					goto IL_001A;
				}
				return modifiers.Length == 1;
				IL_001A:
				return modifiers.IsEmpty;
			}

			// Token: 0x040025DA RID: 9690
			private readonly IReadOnlyDictionary<AnonymousTypeKey, AnonymousTypeValue> _anonymousTypeMap;

			// Token: 0x040025DB RID: 9691
			private readonly SourceAssemblySymbol _sourceAssembly;

			// Token: 0x040025DC RID: 9692
			private readonly AssemblySymbol _otherAssembly;

			// Token: 0x040025DD RID: 9693
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 0, 1 })]
			private readonly ImmutableDictionary<ISymbolInternal, ImmutableArray<ISymbolInternal>> _otherSynthesizedMembers;

			// Token: 0x040025DE RID: 9694
			private readonly CSharpSymbolMatcher.MatchSymbols.SymbolComparer _comparer;

			// Token: 0x040025DF RID: 9695
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 2 })]
			private readonly ConcurrentDictionary<Symbol, Symbol> _matches = new ConcurrentDictionary<Symbol, Symbol>(ReferenceEqualityComparer.Instance);

			// Token: 0x040025E0 RID: 9696
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 1, 1, 0, 1 })]
			private readonly ConcurrentDictionary<ISymbolInternal, IReadOnlyDictionary<string, ImmutableArray<ISymbolInternal>>> _otherMembers = new ConcurrentDictionary<ISymbolInternal, IReadOnlyDictionary<string, ImmutableArray<ISymbolInternal>>>(ReferenceEqualityComparer.Instance);

			// Token: 0x020009FE RID: 2558
			[global::System.Runtime.CompilerServices.Nullable(0)]
			private sealed class SymbolComparer
			{
				// Token: 0x060093DE RID: 37854 RVA: 0x0020376B File Offset: 0x0020196B
				public SymbolComparer(CSharpSymbolMatcher.MatchSymbols matcher, [global::System.Runtime.CompilerServices.Nullable(2)] CSharpSymbolMatcher.DeepTranslator deepTranslator)
				{
					this._matcher = matcher;
					this._deepTranslator = deepTranslator;
				}

				// Token: 0x060093DF RID: 37855 RVA: 0x00203784 File Offset: 0x00201984
				public bool Equals(TypeSymbol source, TypeSymbol other)
				{
					TypeSymbol typeSymbol = (TypeSymbol)this._matcher.Visit(source);
					TypeSymbol typeSymbol2 = ((this._deepTranslator != null) ? ((TypeSymbol)this._deepTranslator.Visit(other)) : other);
					return typeSymbol != null && typeSymbol.Equals(typeSymbol2, TypeCompareKind.IgnoreDynamic | TypeCompareKind.IgnoreTupleNames | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes);
				}

				// Token: 0x04002A49 RID: 10825
				private readonly CSharpSymbolMatcher.MatchSymbols _matcher;

				// Token: 0x04002A4A RID: 10826
				[global::System.Runtime.CompilerServices.Nullable(2)]
				private readonly CSharpSymbolMatcher.DeepTranslator _deepTranslator;
			}
		}

		// Token: 0x020008A9 RID: 2217
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal sealed class DeepTranslator : CSharpSymbolVisitor<Symbol>
		{
			// Token: 0x06008C77 RID: 35959 RVA: 0x001F350E File Offset: 0x001F170E
			public DeepTranslator(NamedTypeSymbol systemObject)
			{
				this._matches = new ConcurrentDictionary<Symbol, Symbol>(ReferenceEqualityComparer.Instance);
				this._systemObject = systemObject;
			}

			// Token: 0x06008C78 RID: 35960 RVA: 0x001F352D File Offset: 0x001F172D
			public override Symbol DefaultVisit(Symbol symbol)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x06008C79 RID: 35961 RVA: 0x001F3534 File Offset: 0x001F1734
			public override Symbol Visit(Symbol symbol)
			{
				return this._matches.GetOrAdd(symbol, base.Visit(symbol));
			}

			// Token: 0x06008C7A RID: 35962 RVA: 0x001F354C File Offset: 0x001F174C
			public override Symbol VisitArrayType(ArrayTypeSymbol symbol)
			{
				TypeSymbol typeSymbol = (TypeSymbol)this.Visit(symbol.ElementType);
				ImmutableArray<CustomModifier> immutableArray = this.VisitCustomModifiers(symbol.ElementTypeWithAnnotations.CustomModifiers);
				if (symbol.IsSZArray)
				{
					return ArrayTypeSymbol.CreateSZArray(symbol.BaseTypeNoUseSiteDiagnostics.ContainingAssembly, symbol.ElementTypeWithAnnotations.WithTypeAndModifiers(typeSymbol, immutableArray));
				}
				return ArrayTypeSymbol.CreateMDArray(symbol.BaseTypeNoUseSiteDiagnostics.ContainingAssembly, symbol.ElementTypeWithAnnotations.WithTypeAndModifiers(typeSymbol, immutableArray), symbol.Rank, symbol.Sizes, symbol.LowerBounds);
			}

			// Token: 0x06008C7B RID: 35963 RVA: 0x001F35DB File Offset: 0x001F17DB
			public override Symbol VisitDynamicType(DynamicTypeSymbol symbol)
			{
				return this._systemObject;
			}

			// Token: 0x06008C7C RID: 35964 RVA: 0x001F35E4 File Offset: 0x001F17E4
			public override Symbol VisitNamedType(NamedTypeSymbol type)
			{
				NamedTypeSymbol originalDefinition = type.OriginalDefinition;
				if (originalDefinition != type)
				{
					CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
					ImmutableArray<TypeWithAnnotations> immutableArray = type.GetAllTypeArguments(ref discarded).SelectAsArray((TypeWithAnnotations t, CSharpSymbolMatcher.DeepTranslator v) => t.WithTypeAndModifiers((TypeSymbol)v.Visit(t.Type), v.VisitCustomModifiers(t.CustomModifiers)), this);
					NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)this.Visit(originalDefinition);
					return new TypeMap(namedTypeSymbol.GetAllTypeParameters(), immutableArray, true).SubstituteNamedType(namedTypeSymbol);
				}
				if (type.IsAnonymousType)
				{
					return this.Visit(AnonymousTypeManager.TranslateAnonymousTypeSymbol(type));
				}
				return type;
			}

			// Token: 0x06008C7D RID: 35965 RVA: 0x001F3668 File Offset: 0x001F1868
			public override Symbol VisitPointerType(PointerTypeSymbol symbol)
			{
				TypeSymbol typeSymbol = (TypeSymbol)this.Visit(symbol.PointedAtType);
				ImmutableArray<CustomModifier> immutableArray = this.VisitCustomModifiers(symbol.PointedAtTypeWithAnnotations.CustomModifiers);
				return new PointerTypeSymbol(symbol.PointedAtTypeWithAnnotations.WithTypeAndModifiers(typeSymbol, immutableArray));
			}

			// Token: 0x06008C7E RID: 35966 RVA: 0x001F36B4 File Offset: 0x001F18B4
			public override Symbol VisitFunctionPointerType(FunctionPointerTypeSymbol symbol)
			{
				FunctionPointerMethodSymbol signature = symbol.Signature;
				TypeSymbol typeSymbol = (TypeSymbol)this.Visit(signature.ReturnType);
				TypeWithAnnotations typeWithAnnotations = signature.ReturnTypeWithAnnotations.WithTypeAndModifiers(typeSymbol, this.VisitCustomModifiers(signature.ReturnTypeWithAnnotations.CustomModifiers));
				ImmutableArray<CustomModifier> immutableArray = this.VisitCustomModifiers(signature.RefCustomModifiers);
				ImmutableArray<TypeWithAnnotations> immutableArray2 = ImmutableArray<TypeWithAnnotations>.Empty;
				ImmutableArray<ImmutableArray<CustomModifier>> immutableArray3 = default(ImmutableArray<ImmutableArray<CustomModifier>>);
				if (signature.ParameterCount > 0)
				{
					ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance(signature.ParameterCount);
					ArrayBuilder<ImmutableArray<CustomModifier>> instance2 = ArrayBuilder<ImmutableArray<CustomModifier>>.GetInstance(signature.ParameterCount);
					foreach (ParameterSymbol parameterSymbol in signature.Parameters)
					{
						TypeSymbol typeSymbol2 = (TypeSymbol)this.Visit(parameterSymbol.Type);
						instance.Add(parameterSymbol.TypeWithAnnotations.WithTypeAndModifiers(typeSymbol2, this.VisitCustomModifiers(parameterSymbol.TypeWithAnnotations.CustomModifiers)));
						instance2.Add(this.VisitCustomModifiers(parameterSymbol.RefCustomModifiers));
					}
					immutableArray2 = instance.ToImmutableAndFree();
					immutableArray3 = instance2.ToImmutableAndFree();
				}
				return symbol.SubstituteTypeSymbol(typeWithAnnotations, immutableArray2, immutableArray, immutableArray3);
			}

			// Token: 0x06008C7F RID: 35967 RVA: 0x001F37DC File Offset: 0x001F19DC
			public override Symbol VisitTypeParameter(TypeParameterSymbol symbol)
			{
				return symbol;
			}

			// Token: 0x06008C80 RID: 35968 RVA: 0x001F37DF File Offset: 0x001F19DF
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			private ImmutableArray<CustomModifier> VisitCustomModifiers([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<CustomModifier> modifiers)
			{
				return modifiers.SelectAsArray<CustomModifier, CustomModifier>(new Func<CustomModifier, CustomModifier>(this.VisitCustomModifier));
			}

			// Token: 0x06008C81 RID: 35969 RVA: 0x001F37F4 File Offset: 0x001F19F4
			private CustomModifier VisitCustomModifier(CustomModifier modifier)
			{
				NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)this.Visit(((CSharpCustomModifier)modifier).ModifierSymbol);
				if (!modifier.IsOptional)
				{
					return CSharpCustomModifier.CreateRequired(namedTypeSymbol);
				}
				return CSharpCustomModifier.CreateOptional(namedTypeSymbol);
			}

			// Token: 0x040025E1 RID: 9697
			private readonly ConcurrentDictionary<Symbol, Symbol> _matches;

			// Token: 0x040025E2 RID: 9698
			private readonly NamedTypeSymbol _systemObject;
		}
	}
}
