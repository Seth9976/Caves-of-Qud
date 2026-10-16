using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.Symbols;

namespace Microsoft.CodeAnalysis.Emit
{
	// Token: 0x02000484 RID: 1156
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class EmitBaseline
	{
		// Token: 0x06002D8B RID: 11659 RVA: 0x00081114 File Offset: 0x0007F314
		public static EmitBaseline CreateInitialBaseline(ModuleMetadata module, Func<MethodDefinitionHandle, EditAndContinueMethodDebugInformation> debugInformationProvider)
		{
			if (module == null)
			{
				throw new ArgumentNullException("module");
			}
			if (!module.Module.HasIL)
			{
				throw new ArgumentException(CodeAnalysisResources.PEImageNotAvailable, "module");
			}
			bool flag = module.Module.PEReaderOpt.ReadDebugDirectory().Any<DebugDirectoryEntry>((DebugDirectoryEntry entry) => entry.IsPortableCodeView);
			Func<MethodDefinitionHandle, StandaloneSignatureHandle> func = delegate(MethodDefinitionHandle methodHandle)
			{
				StandaloneSignatureHandle standaloneSignatureHandle;
				try
				{
					MethodBodyBlock methodBodyOrThrow = module.Module.GetMethodBodyOrThrow(methodHandle);
					StandaloneSignatureHandle standaloneSignatureHandle2;
					if (methodBodyOrThrow == null)
					{
						standaloneSignatureHandle = default(StandaloneSignatureHandle);
						standaloneSignatureHandle2 = standaloneSignatureHandle;
					}
					else
					{
						standaloneSignatureHandle2 = methodBodyOrThrow.LocalSignature;
					}
					standaloneSignatureHandle = standaloneSignatureHandle2;
				}
				catch (Exception ex) when (ex is BadImageFormatException || ex is IOException)
				{
					throw new InvalidDataException(ex.Message, ex);
				}
				return standaloneSignatureHandle;
			};
			return EmitBaseline.CreateInitialBaseline(module, debugInformationProvider, func, flag);
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x000811B8 File Offset: 0x0007F3B8
		public static EmitBaseline CreateInitialBaseline(ModuleMetadata module, Func<MethodDefinitionHandle, EditAndContinueMethodDebugInformation> debugInformationProvider, Func<MethodDefinitionHandle, StandaloneSignatureHandle> localSignatureProvider, bool hasPortableDebugInformation)
		{
			if (module == null)
			{
				throw new ArgumentNullException("module");
			}
			if (debugInformationProvider == null)
			{
				throw new ArgumentNullException("debugInformationProvider");
			}
			if (localSignatureProvider == null)
			{
				throw new ArgumentNullException("localSignatureProvider");
			}
			MetadataReader metadataReader = module.MetadataReader;
			return new EmitBaseline(null, module, null, null, module.GetModuleVersionId(), 0, default(Guid), hasPortableDebugInformation, new Dictionary<ITypeDefinition, int>(), new Dictionary<IEventDefinition, int>(), new Dictionary<IFieldDefinition, int>(), new Dictionary<IMethodDefinition, int>(), new Dictionary<IPropertyDefinition, int>(), new Dictionary<int, int>(), new Dictionary<int, int>(), new Dictionary<MethodImplKey, int>(), EmitBaseline.s_emptyTableSizes, 0, 0, 0, 0, null, ImmutableDictionary<ISymbolInternal, ImmutableArray<ISymbolInternal>>.Empty, new Dictionary<int, AddedOrChangedMethodInfo>(), debugInformationProvider, localSignatureProvider, EmitBaseline.CalculateTypeEventMap(metadataReader), EmitBaseline.CalculateTypePropertyMap(metadataReader), EmitBaseline.CalculateMethodImpls(metadataReader));
		}

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x06002D8D RID: 11661 RVA: 0x00081260 File Offset: 0x0007F460
		internal EmitBaseline InitialBaseline { get; }

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x06002D8E RID: 11662 RVA: 0x00081268 File Offset: 0x0007F468
		public ModuleMetadata OriginalMetadata { get; }

		// Token: 0x06002D8F RID: 11663 RVA: 0x00081270 File Offset: 0x0007F470
		private EmitBaseline([Nullable(2)] EmitBaseline initialBaseline, ModuleMetadata module, [Nullable(2)] Compilation compilation, [Nullable(2)] CommonPEModuleBuilder moduleBuilder, Guid moduleVersionId, int ordinal, Guid encId, bool hasPortablePdb, IReadOnlyDictionary<ITypeDefinition, int> typesAdded, IReadOnlyDictionary<IEventDefinition, int> eventsAdded, IReadOnlyDictionary<IFieldDefinition, int> fieldsAdded, IReadOnlyDictionary<IMethodDefinition, int> methodsAdded, IReadOnlyDictionary<IPropertyDefinition, int> propertiesAdded, IReadOnlyDictionary<int, int> eventMapAdded, IReadOnlyDictionary<int, int> propertyMapAdded, IReadOnlyDictionary<MethodImplKey, int> methodImplsAdded, [Nullable(0)] ImmutableArray<int> tableEntriesAdded, int blobStreamLengthAdded, int stringStreamLengthAdded, int userStringStreamLengthAdded, int guidStreamLengthAdded, [Nullable(2)] IReadOnlyDictionary<AnonymousTypeKey, AnonymousTypeValue> anonymousTypeMap, [Nullable(new byte[] { 1, 1, 0, 1 })] ImmutableDictionary<ISymbolInternal, ImmutableArray<ISymbolInternal>> synthesizedMembers, IReadOnlyDictionary<int, AddedOrChangedMethodInfo> methodsAddedOrChanged, Func<MethodDefinitionHandle, EditAndContinueMethodDebugInformation> debugInformationProvider, Func<MethodDefinitionHandle, StandaloneSignatureHandle> localSignatureProvider, IReadOnlyDictionary<int, int> typeToEventMap, IReadOnlyDictionary<int, int> typeToPropertyMap, IReadOnlyDictionary<MethodImplKey, int> methodImpls)
		{
			MetadataReader metadataReader = module.Module.MetadataReader;
			this.InitialBaseline = initialBaseline ?? this;
			this.OriginalMetadata = module;
			this.Compilation = compilation;
			this.PEModuleBuilder = moduleBuilder;
			this.ModuleVersionId = moduleVersionId;
			this.Ordinal = ordinal;
			this.EncId = encId;
			this.HasPortablePdb = hasPortablePdb;
			this.TypesAdded = typesAdded;
			this.EventsAdded = eventsAdded;
			this.FieldsAdded = fieldsAdded;
			this.MethodsAdded = methodsAdded;
			this.PropertiesAdded = propertiesAdded;
			this.EventMapAdded = eventMapAdded;
			this.PropertyMapAdded = propertyMapAdded;
			this.MethodImplsAdded = methodImplsAdded;
			this.TableEntriesAdded = tableEntriesAdded;
			this.BlobStreamLengthAdded = blobStreamLengthAdded;
			this.StringStreamLengthAdded = stringStreamLengthAdded;
			this.UserStringStreamLengthAdded = userStringStreamLengthAdded;
			this.GuidStreamLengthAdded = guidStreamLengthAdded;
			this._anonymousTypeMap = anonymousTypeMap;
			this.SynthesizedMembers = synthesizedMembers;
			this.AddedOrChangedMethods = methodsAddedOrChanged;
			this.DebugInformationProvider = debugInformationProvider;
			this.LocalSignatureProvider = localSignatureProvider;
			this.TableSizes = EmitBaseline.CalculateTableSizes(metadataReader, this.TableEntriesAdded);
			this.TypeToEventMap = typeToEventMap;
			this.TypeToPropertyMap = typeToPropertyMap;
			this.MethodImpls = methodImpls;
		}

		// Token: 0x06002D90 RID: 11664 RVA: 0x0008138C File Offset: 0x0007F58C
		internal EmitBaseline With(Compilation compilation, CommonPEModuleBuilder moduleBuilder, int ordinal, Guid encId, IReadOnlyDictionary<ITypeDefinition, int> typesAdded, IReadOnlyDictionary<IEventDefinition, int> eventsAdded, IReadOnlyDictionary<IFieldDefinition, int> fieldsAdded, IReadOnlyDictionary<IMethodDefinition, int> methodsAdded, IReadOnlyDictionary<IPropertyDefinition, int> propertiesAdded, IReadOnlyDictionary<int, int> eventMapAdded, IReadOnlyDictionary<int, int> propertyMapAdded, IReadOnlyDictionary<MethodImplKey, int> methodImplsAdded, [Nullable(0)] ImmutableArray<int> tableEntriesAdded, int blobStreamLengthAdded, int stringStreamLengthAdded, int userStringStreamLengthAdded, int guidStreamLengthAdded, IReadOnlyDictionary<AnonymousTypeKey, AnonymousTypeValue> anonymousTypeMap, [Nullable(new byte[] { 1, 1, 0, 1 })] ImmutableDictionary<ISymbolInternal, ImmutableArray<ISymbolInternal>> synthesizedMembers, IReadOnlyDictionary<int, AddedOrChangedMethodInfo> addedOrChangedMethods, Func<MethodDefinitionHandle, EditAndContinueMethodDebugInformation> debugInformationProvider, Func<MethodDefinitionHandle, StandaloneSignatureHandle> localSignatureProvider)
		{
			return new EmitBaseline(this.InitialBaseline, this.OriginalMetadata, compilation, moduleBuilder, this.ModuleVersionId, ordinal, encId, this.HasPortablePdb, typesAdded, eventsAdded, fieldsAdded, methodsAdded, propertiesAdded, eventMapAdded, propertyMapAdded, methodImplsAdded, tableEntriesAdded, blobStreamLengthAdded, stringStreamLengthAdded, userStringStreamLengthAdded, guidStreamLengthAdded, anonymousTypeMap, synthesizedMembers, addedOrChangedMethods, debugInformationProvider, localSignatureProvider, this.TypeToEventMap, this.TypeToPropertyMap, this.MethodImpls);
		}

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x06002D91 RID: 11665 RVA: 0x000813F1 File Offset: 0x0007F5F1
		internal IReadOnlyDictionary<AnonymousTypeKey, AnonymousTypeValue> AnonymousTypeMap
		{
			get
			{
				if (this.Ordinal > 0)
				{
					return this._anonymousTypeMap;
				}
				return this.LazyMetadataSymbols.AnonymousTypes;
			}
		}

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x06002D92 RID: 11666 RVA: 0x0008140E File Offset: 0x0007F60E
		internal MetadataReader MetadataReader
		{
			get
			{
				return this.OriginalMetadata.MetadataReader;
			}
		}

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x06002D93 RID: 11667 RVA: 0x0008141B File Offset: 0x0007F61B
		internal int BlobStreamLength
		{
			get
			{
				return this.BlobStreamLengthAdded + this.MetadataReader.GetHeapSize(HeapIndex.Blob);
			}
		}

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x06002D94 RID: 11668 RVA: 0x00081430 File Offset: 0x0007F630
		internal int StringStreamLength
		{
			get
			{
				return this.StringStreamLengthAdded + this.MetadataReader.GetHeapSize(HeapIndex.String);
			}
		}

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x06002D95 RID: 11669 RVA: 0x00081445 File Offset: 0x0007F645
		internal int UserStringStreamLength
		{
			get
			{
				return this.UserStringStreamLengthAdded + this.MetadataReader.GetHeapSize(HeapIndex.UserString);
			}
		}

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x06002D96 RID: 11670 RVA: 0x0008145A File Offset: 0x0007F65A
		internal int GuidStreamLength
		{
			get
			{
				return this.GuidStreamLengthAdded + this.MetadataReader.GetHeapSize(HeapIndex.Guid);
			}
		}

		// Token: 0x06002D97 RID: 11671 RVA: 0x00081470 File Offset: 0x0007F670
		[NullableContext(0)]
		private static ImmutableArray<int> CalculateTableSizes([Nullable(1)] MetadataReader reader, ImmutableArray<int> delta)
		{
			int[] array = new int[MetadataTokens.TableCount];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = reader.GetTableRowCount((TableIndex)i) + delta[i];
			}
			return ImmutableArray.Create<int>(array);
		}

		// Token: 0x06002D98 RID: 11672 RVA: 0x000814B0 File Offset: 0x0007F6B0
		private static Dictionary<int, int> CalculateTypePropertyMap(MetadataReader reader)
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			int num = 1;
			foreach (TypeDefinitionHandle typeDefinitionHandle in reader.GetTypesWithProperties())
			{
				dictionary.Add(reader.GetRowNumber(typeDefinitionHandle), num);
				num++;
			}
			return dictionary;
		}

		// Token: 0x06002D99 RID: 11673 RVA: 0x00081518 File Offset: 0x0007F718
		private static Dictionary<int, int> CalculateTypeEventMap(MetadataReader reader)
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			int num = 1;
			foreach (TypeDefinitionHandle typeDefinitionHandle in reader.GetTypesWithEvents())
			{
				dictionary.Add(reader.GetRowNumber(typeDefinitionHandle), num);
				num++;
			}
			return dictionary;
		}

		// Token: 0x06002D9A RID: 11674 RVA: 0x00081580 File Offset: 0x0007F780
		private static Dictionary<MethodImplKey, int> CalculateMethodImpls(MetadataReader reader)
		{
			Dictionary<MethodImplKey, int> dictionary = new Dictionary<MethodImplKey, int>();
			int tableRowCount = reader.GetTableRowCount(TableIndex.MethodImpl);
			for (int i = 1; i <= tableRowCount; i++)
			{
				int rowNumber = MetadataTokens.GetRowNumber(reader.GetMethodImplementation(MetadataTokens.MethodImplementationHandle(i)).MethodBody);
				int num = 1;
				MethodImplKey methodImplKey;
				for (;;)
				{
					methodImplKey = new MethodImplKey(rowNumber, num);
					if (!dictionary.ContainsKey(methodImplKey))
					{
						break;
					}
					num++;
				}
				dictionary.Add(methodImplKey, i);
			}
			return dictionary;
		}

		// Token: 0x06002D9B RID: 11675 RVA: 0x000815F0 File Offset: 0x0007F7F0
		internal int GetNextAnonymousTypeIndex(bool fromDelegates = false)
		{
			int num = 0;
			foreach (KeyValuePair<AnonymousTypeKey, AnonymousTypeValue> keyValuePair in this.AnonymousTypeMap)
			{
				if (fromDelegates == keyValuePair.Key.IsDelegate)
				{
					int uniqueIndex = keyValuePair.Value.UniqueIndex;
					if (uniqueIndex >= num)
					{
						num = uniqueIndex + 1;
					}
				}
			}
			return num;
		}

		// Token: 0x04001209 RID: 4617
		[Nullable(0)]
		private static readonly ImmutableArray<int> s_emptyTableSizes = ImmutableArray.Create<int>(new int[MetadataTokens.TableCount]);

		// Token: 0x0400120C RID: 4620
		[Nullable(2)]
		internal EmitBaseline.MetadataSymbols LazyMetadataSymbols;

		// Token: 0x0400120D RID: 4621
		[Nullable(2)]
		internal readonly Compilation Compilation;

		// Token: 0x0400120E RID: 4622
		[Nullable(2)]
		internal readonly CommonPEModuleBuilder PEModuleBuilder;

		// Token: 0x0400120F RID: 4623
		internal readonly Guid ModuleVersionId;

		// Token: 0x04001210 RID: 4624
		internal readonly bool HasPortablePdb;

		// Token: 0x04001211 RID: 4625
		internal readonly int Ordinal;

		// Token: 0x04001212 RID: 4626
		internal readonly Guid EncId;

		// Token: 0x04001213 RID: 4627
		internal readonly IReadOnlyDictionary<ITypeDefinition, int> TypesAdded;

		// Token: 0x04001214 RID: 4628
		internal readonly IReadOnlyDictionary<IEventDefinition, int> EventsAdded;

		// Token: 0x04001215 RID: 4629
		internal readonly IReadOnlyDictionary<IFieldDefinition, int> FieldsAdded;

		// Token: 0x04001216 RID: 4630
		internal readonly IReadOnlyDictionary<IMethodDefinition, int> MethodsAdded;

		// Token: 0x04001217 RID: 4631
		internal readonly IReadOnlyDictionary<IPropertyDefinition, int> PropertiesAdded;

		// Token: 0x04001218 RID: 4632
		internal readonly IReadOnlyDictionary<int, int> EventMapAdded;

		// Token: 0x04001219 RID: 4633
		internal readonly IReadOnlyDictionary<int, int> PropertyMapAdded;

		// Token: 0x0400121A RID: 4634
		internal readonly IReadOnlyDictionary<MethodImplKey, int> MethodImplsAdded;

		// Token: 0x0400121B RID: 4635
		[Nullable(0)]
		internal readonly ImmutableArray<int> TableEntriesAdded;

		// Token: 0x0400121C RID: 4636
		internal readonly int BlobStreamLengthAdded;

		// Token: 0x0400121D RID: 4637
		internal readonly int StringStreamLengthAdded;

		// Token: 0x0400121E RID: 4638
		internal readonly int UserStringStreamLengthAdded;

		// Token: 0x0400121F RID: 4639
		internal readonly int GuidStreamLengthAdded;

		// Token: 0x04001220 RID: 4640
		internal readonly IReadOnlyDictionary<int, AddedOrChangedMethodInfo> AddedOrChangedMethods;

		// Token: 0x04001221 RID: 4641
		internal readonly Func<MethodDefinitionHandle, EditAndContinueMethodDebugInformation> DebugInformationProvider;

		// Token: 0x04001222 RID: 4642
		internal readonly Func<MethodDefinitionHandle, StandaloneSignatureHandle> LocalSignatureProvider;

		// Token: 0x04001223 RID: 4643
		[Nullable(0)]
		internal readonly ImmutableArray<int> TableSizes;

		// Token: 0x04001224 RID: 4644
		internal readonly IReadOnlyDictionary<int, int> TypeToEventMap;

		// Token: 0x04001225 RID: 4645
		internal readonly IReadOnlyDictionary<int, int> TypeToPropertyMap;

		// Token: 0x04001226 RID: 4646
		internal readonly IReadOnlyDictionary<MethodImplKey, int> MethodImpls;

		// Token: 0x04001227 RID: 4647
		[Nullable(2)]
		private readonly IReadOnlyDictionary<AnonymousTypeKey, AnonymousTypeValue> _anonymousTypeMap;

		// Token: 0x04001228 RID: 4648
		[Nullable(new byte[] { 1, 1, 0, 1 })]
		internal readonly ImmutableDictionary<ISymbolInternal, ImmutableArray<ISymbolInternal>> SynthesizedMembers;

		// Token: 0x02000738 RID: 1848
		[Nullable(0)]
		internal sealed class MetadataSymbols
		{
			// Token: 0x06003BFB RID: 15355 RVA: 0x000AC4D6 File Offset: 0x000AA6D6
			public MetadataSymbols(IReadOnlyDictionary<AnonymousTypeKey, AnonymousTypeValue> anonymousTypes, object metadataDecoder, ImmutableDictionary<AssemblyIdentity, AssemblyIdentity> assemblyReferenceIdentityMap)
			{
				this.AnonymousTypes = anonymousTypes;
				this.MetadataDecoder = metadataDecoder;
				this.AssemblyReferenceIdentityMap = assemblyReferenceIdentityMap;
			}

			// Token: 0x04001CB8 RID: 7352
			public readonly IReadOnlyDictionary<AnonymousTypeKey, AnonymousTypeValue> AnonymousTypes;

			// Token: 0x04001CB9 RID: 7353
			public readonly ImmutableDictionary<AssemblyIdentity, AssemblyIdentity> AssemblyReferenceIdentityMap;

			// Token: 0x04001CBA RID: 7354
			public readonly object MetadataDecoder;
		}
	}
}
