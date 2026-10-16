using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.CSharp.Emit.NoPia;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Emit
{
	// Token: 0x02000485 RID: 1157
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class PEDeltaAssemblyBuilder : PEAssemblyBuilderBase, IPEDeltaAssemblyBuilder
	{
		// Token: 0x0600607A RID: 24698 RVA: 0x00175038 File Offset: 0x00173238
		public PEDeltaAssemblyBuilder(SourceAssemblySymbol sourceAssembly, EmitOptions emitOptions, OutputKind outputKind, ModulePropertiesForSerialization serializationProperties, IEnumerable<ResourceDescription> manifestResources, EmitBaseline previousGeneration, IEnumerable<SemanticEdit> edits, Func<ISymbol, bool> isAddedSymbol)
			: base(sourceAssembly, emitOptions, outputKind, serializationProperties, manifestResources, ImmutableArray<NamedTypeSymbol>.Empty)
		{
			EmitBaseline initialBaseline = previousGeneration.InitialBaseline;
			EmitContext emitContext = new EmitContext(this, null, new DiagnosticBag(), false, true);
			EmitBaseline.MetadataSymbols orCreateMetadataSymbols = PEDeltaAssemblyBuilder.GetOrCreateMetadataSymbols(initialBaseline, sourceAssembly.DeclaringCompilation);
			MetadataDecoder metadataDecoder = (MetadataDecoder)orCreateMetadataSymbols.MetadataDecoder;
			PEAssemblySymbol peassemblySymbol = (PEAssemblySymbol)metadataDecoder.ModuleSymbol.ContainingAssembly;
			CSharpSymbolMatcher csharpSymbolMatcher = new CSharpSymbolMatcher(orCreateMetadataSymbols.AnonymousTypes, sourceAssembly, emitContext, peassemblySymbol);
			CSharpSymbolMatcher csharpSymbolMatcher2 = null;
			if (previousGeneration.Ordinal > 0)
			{
				SourceAssemblySymbol sourceAssembly2 = ((CSharpCompilation)previousGeneration.Compilation).SourceAssembly;
				EmitContext emitContext2 = new EmitContext((PEModuleBuilder)previousGeneration.PEModuleBuilder, null, new DiagnosticBag(), false, true);
				csharpSymbolMatcher2 = new CSharpSymbolMatcher(previousGeneration.AnonymousTypeMap, sourceAssembly, emitContext, sourceAssembly2, emitContext2, previousGeneration.SynthesizedMembers);
			}
			this._previousDefinitions = new CSharpDefinitionMap(edits, metadataDecoder, csharpSymbolMatcher, csharpSymbolMatcher2);
			this._previousGeneration = previousGeneration;
			this._changes = new CSharpSymbolChanges(this._previousDefinitions, edits, isAddedSymbol);
			this._deepTranslator = new CSharpSymbolMatcher.DeepTranslator(sourceAssembly.GetSpecialType(SpecialType.System_Object));
		}

		// Token: 0x17001668 RID: 5736
		// (get) Token: 0x0600607B RID: 24699 RVA: 0x0017513A File Offset: 0x0017333A
		public override int CurrentGenerationOrdinal
		{
			get
			{
				return this._previousGeneration.Ordinal + 1;
			}
		}

		// Token: 0x0600607C RID: 24700 RVA: 0x0017514C File Offset: 0x0017334C
		internal override ITypeReference EncTranslateLocalVariableType(TypeSymbol type, DiagnosticBag diagnostics)
		{
			TypeSymbol typeSymbol = (TypeSymbol)this._deepTranslator.Visit(type);
			return this.Translate(typeSymbol ?? type, null, diagnostics);
		}

		// Token: 0x0600607D RID: 24701 RVA: 0x0017517C File Offset: 0x0017337C
		private static EmitBaseline.MetadataSymbols GetOrCreateMetadataSymbols(EmitBaseline initialBaseline, CSharpCompilation compilation)
		{
			if (initialBaseline.LazyMetadataSymbols != null)
			{
				return initialBaseline.LazyMetadataSymbols;
			}
			ModuleMetadata originalMetadata = initialBaseline.OriginalMetadata;
			ImmutableDictionary<AssemblyIdentity, AssemblyIdentity> immutableDictionary;
			MetadataDecoder metadataDecoder = new MetadataDecoder(compilation.RemoveAllSyntaxTrees().GetBoundReferenceManager().CreatePEAssemblyForAssemblyMetadata(AssemblyMetadata.Create(originalMetadata), MetadataImportOptions.All, out immutableDictionary)
				.PrimaryModule);
			EmitBaseline.MetadataSymbols metadataSymbols = new EmitBaseline.MetadataSymbols(PEDeltaAssemblyBuilder.GetAnonymousTypeMapFromMetadata(originalMetadata.MetadataReader, metadataDecoder), metadataDecoder, immutableDictionary);
			return InterlockedOperations.Initialize<EmitBaseline.MetadataSymbols>(ref initialBaseline.LazyMetadataSymbols, metadataSymbols);
		}

		// Token: 0x0600607E RID: 24702 RVA: 0x001751E4 File Offset: 0x001733E4
		internal static IReadOnlyDictionary<AnonymousTypeKey, AnonymousTypeValue> GetAnonymousTypeMapFromMetadata(MetadataReader reader, MetadataDecoder metadataDecoder)
		{
			Dictionary<AnonymousTypeKey, AnonymousTypeValue> dictionary = new Dictionary<AnonymousTypeKey, AnonymousTypeValue>();
			foreach (TypeDefinitionHandle typeDefinitionHandle in reader.TypeDefinitions)
			{
				TypeDefinition typeDefinition = reader.GetTypeDefinition(typeDefinitionHandle);
				if (typeDefinition.Namespace.IsNil && reader.StringComparer.StartsWith(typeDefinition.Name, "<>f__AnonymousType"))
				{
					short num;
					string text = MetadataHelpers.InferTypeArityAndUnmangleMetadataName(reader.GetString(typeDefinition.Name), out num);
					int num2;
					if (GeneratedNames.TryParseAnonymousTypeTemplateName(text, out num2))
					{
						ArrayBuilder<AnonymousTypeKeyField> instance = ArrayBuilder<AnonymousTypeKeyField>.GetInstance();
						if (PEDeltaAssemblyBuilder.TryGetAnonymousTypeKey(reader, typeDefinition, instance))
						{
							NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)metadataDecoder.GetTypeOfToken(typeDefinitionHandle);
							AnonymousTypeKey anonymousTypeKey = new AnonymousTypeKey(instance.ToImmutable(), false);
							AnonymousTypeValue anonymousTypeValue = new AnonymousTypeValue(text, num2, namedTypeSymbol.GetCciAdapter());
							dictionary.Add(anonymousTypeKey, anonymousTypeValue);
						}
						instance.Free();
					}
				}
			}
			return dictionary;
		}

		// Token: 0x0600607F RID: 24703 RVA: 0x001752F4 File Offset: 0x001734F4
		private static bool TryGetAnonymousTypeKey(MetadataReader reader, TypeDefinition def, ArrayBuilder<AnonymousTypeKeyField> builder)
		{
			foreach (GenericParameterHandle genericParameterHandle in def.GetGenericParameters())
			{
				string text;
				if (!GeneratedNames.TryParseAnonymousTypeParameterName(reader.GetString(reader.GetGenericParameter(genericParameterHandle).Name), out text))
				{
					return false;
				}
				builder.Add(new AnonymousTypeKeyField(text, false, false));
			}
			return true;
		}

		// Token: 0x17001669 RID: 5737
		// (get) Token: 0x06006080 RID: 24704 RVA: 0x0017537C File Offset: 0x0017357C
		internal EmitBaseline PreviousGeneration
		{
			get
			{
				return this._previousGeneration;
			}
		}

		// Token: 0x1700166A RID: 5738
		// (get) Token: 0x06006081 RID: 24705 RVA: 0x00175384 File Offset: 0x00173584
		internal CSharpDefinitionMap PreviousDefinitions
		{
			get
			{
				return this._previousDefinitions;
			}
		}

		// Token: 0x1700166B RID: 5739
		// (get) Token: 0x06006082 RID: 24706 RVA: 0x0017538C File Offset: 0x0017358C
		internal override bool SupportsPrivateImplClass
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006083 RID: 24707 RVA: 0x0017538F File Offset: 0x0017358F
		public IReadOnlyDictionary<AnonymousTypeKey, AnonymousTypeValue> GetAnonymousTypeMap()
		{
			return this.Compilation.AnonymousTypeManager.GetAnonymousTypeMap();
		}

		// Token: 0x06006084 RID: 24708 RVA: 0x001753A1 File Offset: 0x001735A1
		public override IEnumerable<INamespaceTypeDefinition> GetTopLevelTypeDefinitions(EmitContext context)
		{
			foreach (INamespaceTypeDefinition namespaceTypeDefinition in this.GetAnonymousTypeDefinitions(context))
			{
				yield return namespaceTypeDefinition;
			}
			IEnumerator<INamespaceTypeDefinition> enumerator = null;
			foreach (INamespaceTypeDefinition namespaceTypeDefinition2 in base.GetTopLevelTypeDefinitionsCore(context))
			{
				yield return namespaceTypeDefinition2;
			}
			enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06006085 RID: 24709 RVA: 0x001753B8 File Offset: 0x001735B8
		public override IEnumerable<INamespaceTypeDefinition> GetTopLevelSourceTypeDefinitions(EmitContext context)
		{
			return this._changes.GetTopLevelSourceTypeDefinitions(context);
		}

		// Token: 0x06006086 RID: 24710 RVA: 0x001753C6 File Offset: 0x001735C6
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal override VariableSlotAllocator TryCreateVariableSlotAllocator(MethodSymbol method, MethodSymbol topLevelMethod, DiagnosticBag diagnostics)
		{
			return this._previousDefinitions.TryCreateVariableSlotAllocator(this._previousGeneration, this.Compilation, method, topLevelMethod, diagnostics);
		}

		// Token: 0x06006087 RID: 24711 RVA: 0x001753E2 File Offset: 0x001735E2
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal override ImmutableArray<AnonymousTypeKey> GetPreviousAnonymousTypes()
		{
			return ImmutableArray.CreateRange<AnonymousTypeKey>(this._previousGeneration.AnonymousTypeMap.Keys);
		}

		// Token: 0x06006088 RID: 24712 RVA: 0x001753F9 File Offset: 0x001735F9
		internal override int GetNextAnonymousTypeIndex()
		{
			return this._previousGeneration.GetNextAnonymousTypeIndex(false);
		}

		// Token: 0x06006089 RID: 24713 RVA: 0x00175407 File Offset: 0x00173607
		internal override bool TryGetAnonymousTypeName(AnonymousTypeManager.AnonymousTypeTemplateSymbol template, [global::System.Runtime.CompilerServices.Nullable(2)] [NotNullWhen(true)] out string name, out int index)
		{
			return this._previousDefinitions.TryGetAnonymousTypeName(template, out name, out index);
		}

		// Token: 0x1700166C RID: 5740
		// (get) Token: 0x0600608A RID: 24714 RVA: 0x00175417 File Offset: 0x00173617
		internal SymbolChanges Changes
		{
			get
			{
				return this._changes;
			}
		}

		// Token: 0x0600608B RID: 24715 RVA: 0x00175420 File Offset: 0x00173620
		public void OnCreatedIndices(DiagnosticBag diagnostics)
		{
			EmbeddedTypesManager embeddedTypesManagerOpt = this.EmbeddedTypesManagerOpt;
			if (embeddedTypesManagerOpt != null)
			{
				foreach (NamedTypeSymbol namedTypeSymbol in embeddedTypesManagerOpt.EmbeddedTypesMap.Keys)
				{
					diagnostics.Add(new CSDiagnosticInfo(ErrorCode.ERR_EncNoPIAReference, new object[] { namedTypeSymbol.AdaptedSymbol }), Location.None);
				}
			}
		}

		// Token: 0x1700166D RID: 5741
		// (get) Token: 0x0600608C RID: 24716 RVA: 0x0017549C File Offset: 0x0017369C
		internal override bool IsEncDelta
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04001950 RID: 6480
		private readonly EmitBaseline _previousGeneration;

		// Token: 0x04001951 RID: 6481
		private readonly CSharpDefinitionMap _previousDefinitions;

		// Token: 0x04001952 RID: 6482
		private readonly SymbolChanges _changes;

		// Token: 0x04001953 RID: 6483
		private readonly CSharpSymbolMatcher.DeepTranslator _deepTranslator;
	}
}
