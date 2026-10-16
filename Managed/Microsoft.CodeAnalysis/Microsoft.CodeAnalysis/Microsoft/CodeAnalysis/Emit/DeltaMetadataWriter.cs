using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Emit
{
	// Token: 0x02000482 RID: 1154
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class DeltaMetadataWriter : MetadataWriter
	{
		// Token: 0x06002D3D RID: 11581 RVA: 0x0007F600 File Offset: 0x0007D800
		public DeltaMetadataWriter(EmitContext context, CommonMessageProvider messageProvider, EmitBaseline previousGeneration, Guid encId, DefinitionMap definitionMap, SymbolChanges changes, CancellationToken cancellationToken)
			: base(DeltaMetadataWriter.MakeTablesBuilder(previousGeneration), (context.Module.DebugInformationFormat == DebugInformationFormat.PortablePdb) ? new MetadataBuilder(0, 0, 0, 0) : null, null, context, messageProvider, false, false, false, cancellationToken)
		{
			this._previousGeneration = previousGeneration;
			this._encId = encId;
			this._definitionMap = definitionMap;
			this._changes = changes;
			ImmutableArray<int> tableSizes = previousGeneration.TableSizes;
			this._typeDefs = new DeltaMetadataWriter.DefinitionIndex<ITypeDefinition>(new DeltaMetadataWriter.DefinitionIndex<ITypeDefinition>.TryGetExistingIndex(this.TryGetExistingTypeDefIndex), tableSizes[2]);
			this._eventDefs = new DeltaMetadataWriter.DefinitionIndex<IEventDefinition>(new DeltaMetadataWriter.DefinitionIndex<IEventDefinition>.TryGetExistingIndex(this.TryGetExistingEventDefIndex), tableSizes[20]);
			this._fieldDefs = new DeltaMetadataWriter.DefinitionIndex<IFieldDefinition>(new DeltaMetadataWriter.DefinitionIndex<IFieldDefinition>.TryGetExistingIndex(this.TryGetExistingFieldDefIndex), tableSizes[4]);
			this._methodDefs = new DeltaMetadataWriter.DefinitionIndex<IMethodDefinition>(new DeltaMetadataWriter.DefinitionIndex<IMethodDefinition>.TryGetExistingIndex(this.TryGetExistingMethodDefIndex), tableSizes[6]);
			this._propertyDefs = new DeltaMetadataWriter.DefinitionIndex<IPropertyDefinition>(new DeltaMetadataWriter.DefinitionIndex<IPropertyDefinition>.TryGetExistingIndex(this.TryGetExistingPropertyDefIndex), tableSizes[23]);
			this._parameterDefs = new DeltaMetadataWriter.ParameterDefinitionIndex(tableSizes[8]);
			this._parameterDefList = new List<KeyValuePair<IMethodDefinition, IParameterDefinition>>();
			this._genericParameters = new DeltaMetadataWriter.GenericParameterIndex(tableSizes[42]);
			this._eventMap = new DeltaMetadataWriter.EventOrPropertyMapIndex(new DeltaMetadataWriter.EventOrPropertyMapIndex.TryGetExistingIndex(this.TryGetExistingEventMapIndex), tableSizes[18]);
			this._propertyMap = new DeltaMetadataWriter.EventOrPropertyMapIndex(new DeltaMetadataWriter.EventOrPropertyMapIndex.TryGetExistingIndex(this.TryGetExistingPropertyMapIndex), tableSizes[21]);
			this._methodImpls = new DeltaMetadataWriter.MethodImplIndex(this, tableSizes[25]);
			this._assemblyRefIndex = new MetadataWriter.HeapOrReferenceIndex<AssemblyIdentity>(this, tableSizes[35]);
			this._moduleRefIndex = new MetadataWriter.HeapOrReferenceIndex<string>(this, tableSizes[26]);
			this._memberRefIndex = new MetadataWriter.InstanceAndStructuralReferenceIndex<ITypeMemberReference>(this, new MemberRefComparer(this), tableSizes[10]);
			this._methodSpecIndex = new MetadataWriter.InstanceAndStructuralReferenceIndex<IGenericMethodInstanceReference>(this, new MethodSpecComparer(this), tableSizes[43]);
			this._typeRefIndex = new MetadataWriter.TypeReferenceIndex(this, tableSizes[1]);
			this._typeSpecIndex = new MetadataWriter.InstanceAndStructuralReferenceIndex<ITypeReference>(this, new TypeSpecComparer(this), tableSizes[27]);
			this._standAloneSignatureIndex = new MetadataWriter.HeapOrReferenceIndex<BlobHandle>(this, tableSizes[17]);
			this._addedOrChangedMethods = new Dictionary<IMethodDefinition, AddedOrChangedMethodInfo>(SymbolEquivalentEqualityComparer.Instance);
		}

		// Token: 0x06002D3E RID: 11582 RVA: 0x0007F839 File Offset: 0x0007DA39
		private static MetadataBuilder MakeTablesBuilder(EmitBaseline previousGeneration)
		{
			return new MetadataBuilder(previousGeneration.UserStringStreamLength, previousGeneration.StringStreamLength, previousGeneration.BlobStreamLength, previousGeneration.GuidStreamLength);
		}

		// Token: 0x06002D3F RID: 11583 RVA: 0x0007F858 File Offset: 0x0007DA58
		[NullableContext(0)]
		private ImmutableArray<int> GetDeltaTableSizes(ImmutableArray<int> rowCounts)
		{
			int[] array = new int[MetadataTokens.TableCount];
			rowCounts.CopyTo(array);
			array[1] = this._typeRefIndex.Rows.Count;
			array[2] = this._typeDefs.GetAdded().Count;
			array[4] = this._fieldDefs.GetAdded().Count;
			array[6] = this._methodDefs.GetAdded().Count;
			array[8] = this._parameterDefs.GetAdded().Count;
			array[10] = this._memberRefIndex.Rows.Count;
			array[17] = this._standAloneSignatureIndex.Rows.Count;
			array[18] = this._eventMap.GetAdded().Count;
			array[20] = this._eventDefs.GetAdded().Count;
			array[21] = this._propertyMap.GetAdded().Count;
			array[23] = this._propertyDefs.GetAdded().Count;
			array[25] = this._methodImpls.GetAdded().Count;
			array[26] = this._moduleRefIndex.Rows.Count;
			array[27] = this._typeSpecIndex.Rows.Count;
			array[35] = this._assemblyRefIndex.Rows.Count;
			array[42] = this._genericParameters.GetAdded().Count;
			array[43] = this._methodSpecIndex.Rows.Count;
			return ImmutableArray.Create<int>(array);
		}

		// Token: 0x06002D40 RID: 11584 RVA: 0x0007F9D0 File Offset: 0x0007DBD0
		internal EmitBaseline GetDelta(EmitBaseline baseline, Compilation compilation, Guid encId, MetadataSizes metadataSizes)
		{
			Dictionary<int, AddedOrChangedMethodInfo> dictionary = new Dictionary<int, AddedOrChangedMethodInfo>();
			foreach (KeyValuePair<IMethodDefinition, AddedOrChangedMethodInfo> keyValuePair in this._addedOrChangedMethods)
			{
				dictionary.Add(MetadataTokens.GetRowNumber(this.GetMethodDefinitionHandle(keyValuePair.Key)), keyValuePair.Value);
			}
			ImmutableArray<int> tableEntriesAdded = this._previousGeneration.TableEntriesAdded;
			ImmutableArray<int> deltaTableSizes = this.GetDeltaTableSizes(metadataSizes.RowCounts);
			int[] array = new int[MetadataTokens.TableCount];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = tableEntriesAdded[i] + deltaTableSizes[i];
			}
			ImmutableDictionary<ISymbolInternal, ImmutableArray<ISymbolInternal>> immutableDictionary = ((baseline.Ordinal == 0) ? this.module.GetAllSynthesizedMembers() : baseline.SynthesizedMembers);
			return baseline.With(compilation, this.module, baseline.Ordinal + 1, encId, DeltaMetadataWriter.AddRange<ITypeDefinition, int>(this._previousGeneration.TypesAdded, this._typeDefs.GetAdded(), false, SymbolEquivalentEqualityComparer.Instance), DeltaMetadataWriter.AddRange<IEventDefinition, int>(this._previousGeneration.EventsAdded, this._eventDefs.GetAdded(), false, SymbolEquivalentEqualityComparer.Instance), DeltaMetadataWriter.AddRange<IFieldDefinition, int>(this._previousGeneration.FieldsAdded, this._fieldDefs.GetAdded(), false, SymbolEquivalentEqualityComparer.Instance), DeltaMetadataWriter.AddRange<IMethodDefinition, int>(this._previousGeneration.MethodsAdded, this._methodDefs.GetAdded(), false, SymbolEquivalentEqualityComparer.Instance), DeltaMetadataWriter.AddRange<IPropertyDefinition, int>(this._previousGeneration.PropertiesAdded, this._propertyDefs.GetAdded(), false, SymbolEquivalentEqualityComparer.Instance), DeltaMetadataWriter.AddRange<int, int>(this._previousGeneration.EventMapAdded, this._eventMap.GetAdded(), false, null), DeltaMetadataWriter.AddRange<int, int>(this._previousGeneration.PropertyMapAdded, this._propertyMap.GetAdded(), false, null), DeltaMetadataWriter.AddRange<MethodImplKey, int>(this._previousGeneration.MethodImplsAdded, this._methodImpls.GetAdded(), false, null), ImmutableArray.Create<int>(array), metadataSizes.GetAlignedHeapSize(HeapIndex.Blob) + this._previousGeneration.BlobStreamLengthAdded, metadataSizes.HeapSizes[1] + this._previousGeneration.StringStreamLengthAdded, metadataSizes.GetAlignedHeapSize(HeapIndex.UserString) + this._previousGeneration.UserStringStreamLengthAdded, metadataSizes.HeapSizes[3], ((IPEDeltaAssemblyBuilder)this.module).GetAnonymousTypeMap(), immutableDictionary, DeltaMetadataWriter.AddRange<int, AddedOrChangedMethodInfo>(this._previousGeneration.AddedOrChangedMethods, dictionary, true, null), baseline.DebugInformationProvider, baseline.LocalSignatureProvider);
		}

		// Token: 0x06002D41 RID: 11585 RVA: 0x0007FC54 File Offset: 0x0007DE54
		private static IReadOnlyDictionary<K, V> AddRange<K, [Nullable(2)] V>(IReadOnlyDictionary<K, V> previous, IReadOnlyDictionary<K, V> current, bool replace = false, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<K> comparer = null)
		{
			if (previous.Count == 0)
			{
				return current;
			}
			if (current.Count == 0)
			{
				return previous;
			}
			Dictionary<K, V> dictionary = new Dictionary<K, V>(comparer);
			foreach (KeyValuePair<K, V> keyValuePair in previous)
			{
				dictionary.Add(keyValuePair.Key, keyValuePair.Value);
			}
			foreach (KeyValuePair<K, V> keyValuePair2 in current)
			{
				dictionary[keyValuePair2.Key] = keyValuePair2.Value;
			}
			return dictionary;
		}

		// Token: 0x06002D42 RID: 11586 RVA: 0x0007FD0C File Offset: 0x0007DF0C
		public void GetMethodTokens(ICollection<MethodDefinitionHandle> methods)
		{
			foreach (IMethodDefinition methodDefinition in this._methodDefs.GetRows())
			{
				if (!this._methodDefs.IsAddedNotChanged(methodDefinition))
				{
					IMethodBody body = methodDefinition.GetBody(this.Context);
					if (body != null && body.SequencePoints.Length > 0)
					{
						methods.Add(MetadataTokens.MethodDefinitionHandle(this._methodDefs[methodDefinition]));
					}
				}
			}
		}

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x06002D43 RID: 11587 RVA: 0x0007FDA4 File Offset: 0x0007DFA4
		protected override ushort Generation
		{
			get
			{
				return (ushort)(this._previousGeneration.Ordinal + 1);
			}
		}

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x06002D44 RID: 11588 RVA: 0x0007FDB4 File Offset: 0x0007DFB4
		protected override Guid EncId
		{
			get
			{
				return this._encId;
			}
		}

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x06002D45 RID: 11589 RVA: 0x0007FDBC File Offset: 0x0007DFBC
		protected override Guid EncBaseId
		{
			get
			{
				return this._previousGeneration.EncId;
			}
		}

		// Token: 0x06002D46 RID: 11590 RVA: 0x0007FDC9 File Offset: 0x0007DFC9
		protected override EventDefinitionHandle GetEventDefinitionHandle(IEventDefinition def)
		{
			return MetadataTokens.EventDefinitionHandle(this._eventDefs[def]);
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x0007FDDC File Offset: 0x0007DFDC
		protected override IReadOnlyList<IEventDefinition> GetEventDefs()
		{
			return this._eventDefs.GetRows();
		}

		// Token: 0x06002D48 RID: 11592 RVA: 0x0007FDE9 File Offset: 0x0007DFE9
		protected override FieldDefinitionHandle GetFieldDefinitionHandle(IFieldDefinition def)
		{
			return MetadataTokens.FieldDefinitionHandle(this._fieldDefs[def]);
		}

		// Token: 0x06002D49 RID: 11593 RVA: 0x0007FDFC File Offset: 0x0007DFFC
		protected override IReadOnlyList<IFieldDefinition> GetFieldDefs()
		{
			return this._fieldDefs.GetRows();
		}

		// Token: 0x06002D4A RID: 11594 RVA: 0x0007FE0C File Offset: 0x0007E00C
		protected override bool TryGetTypeDefinitionHandle(ITypeDefinition def, out TypeDefinitionHandle handle)
		{
			int num;
			bool flag = this._typeDefs.TryGetValue(def, out num);
			handle = MetadataTokens.TypeDefinitionHandle(num);
			return flag;
		}

		// Token: 0x06002D4B RID: 11595 RVA: 0x0007FE33 File Offset: 0x0007E033
		protected override TypeDefinitionHandle GetTypeDefinitionHandle(ITypeDefinition def)
		{
			return MetadataTokens.TypeDefinitionHandle(this._typeDefs[def]);
		}

		// Token: 0x06002D4C RID: 11596 RVA: 0x0007FE46 File Offset: 0x0007E046
		protected override ITypeDefinition GetTypeDef(TypeDefinitionHandle handle)
		{
			return this._typeDefs[MetadataTokens.GetRowNumber(handle)];
		}

		// Token: 0x06002D4D RID: 11597 RVA: 0x0007FE5E File Offset: 0x0007E05E
		protected override IReadOnlyList<ITypeDefinition> GetTypeDefs()
		{
			return this._typeDefs.GetRows();
		}

		// Token: 0x06002D4E RID: 11598 RVA: 0x0007FE6C File Offset: 0x0007E06C
		protected override bool TryGetMethodDefinitionHandle(IMethodDefinition def, out MethodDefinitionHandle handle)
		{
			int num;
			bool flag = this._methodDefs.TryGetValue(def, out num);
			handle = MetadataTokens.MethodDefinitionHandle(num);
			return flag;
		}

		// Token: 0x06002D4F RID: 11599 RVA: 0x0007FE93 File Offset: 0x0007E093
		protected override MethodDefinitionHandle GetMethodDefinitionHandle(IMethodDefinition def)
		{
			return MetadataTokens.MethodDefinitionHandle(this._methodDefs[def]);
		}

		// Token: 0x06002D50 RID: 11600 RVA: 0x0007FEA6 File Offset: 0x0007E0A6
		protected override IMethodDefinition GetMethodDef(MethodDefinitionHandle index)
		{
			return this._methodDefs[MetadataTokens.GetRowNumber(index)];
		}

		// Token: 0x06002D51 RID: 11601 RVA: 0x0007FEBE File Offset: 0x0007E0BE
		protected override IReadOnlyList<IMethodDefinition> GetMethodDefs()
		{
			return this._methodDefs.GetRows();
		}

		// Token: 0x06002D52 RID: 11602 RVA: 0x0007FECB File Offset: 0x0007E0CB
		protected override PropertyDefinitionHandle GetPropertyDefIndex(IPropertyDefinition def)
		{
			return MetadataTokens.PropertyDefinitionHandle(this._propertyDefs[def]);
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x0007FEDE File Offset: 0x0007E0DE
		protected override IReadOnlyList<IPropertyDefinition> GetPropertyDefs()
		{
			return this._propertyDefs.GetRows();
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x0007FEEB File Offset: 0x0007E0EB
		protected override ParameterHandle GetParameterHandle(IParameterDefinition def)
		{
			return MetadataTokens.ParameterHandle(this._parameterDefs[def]);
		}

		// Token: 0x06002D55 RID: 11605 RVA: 0x0007FEFE File Offset: 0x0007E0FE
		protected override IReadOnlyList<IParameterDefinition> GetParameterDefs()
		{
			return this._parameterDefs.GetRows();
		}

		// Token: 0x06002D56 RID: 11606 RVA: 0x0007FF0B File Offset: 0x0007E10B
		protected override IReadOnlyList<IGenericParameter> GetGenericParameters()
		{
			return this._genericParameters.GetRows();
		}

		// Token: 0x06002D57 RID: 11607 RVA: 0x0007FF18 File Offset: 0x0007E118
		protected override FieldDefinitionHandle GetFirstFieldDefinitionHandle(INamedTypeDefinition typeDef)
		{
			return default(FieldDefinitionHandle);
		}

		// Token: 0x06002D58 RID: 11608 RVA: 0x0007FF30 File Offset: 0x0007E130
		protected override MethodDefinitionHandle GetFirstMethodDefinitionHandle(INamedTypeDefinition typeDef)
		{
			return default(MethodDefinitionHandle);
		}

		// Token: 0x06002D59 RID: 11609 RVA: 0x0007FF48 File Offset: 0x0007E148
		protected override ParameterHandle GetFirstParameterHandle(IMethodDefinition methodDef)
		{
			return default(ParameterHandle);
		}

		// Token: 0x06002D5A RID: 11610 RVA: 0x0007FF60 File Offset: 0x0007E160
		protected override AssemblyReferenceHandle GetOrAddAssemblyReferenceHandle(IAssemblyReference reference)
		{
			AssemblyIdentity assemblyIdentity = reference.Identity;
			Version assemblyVersionPattern = reference.AssemblyVersionPattern;
			if (assemblyVersionPattern != null)
			{
				assemblyIdentity = this._previousGeneration.InitialBaseline.LazyMetadataSymbols.AssemblyReferenceIdentityMap[assemblyIdentity.WithVersion(assemblyVersionPattern)];
			}
			return MetadataTokens.AssemblyReferenceHandle(this._assemblyRefIndex.GetOrAdd(assemblyIdentity));
		}

		// Token: 0x06002D5B RID: 11611 RVA: 0x0007FFB1 File Offset: 0x0007E1B1
		protected override IReadOnlyList<AssemblyIdentity> GetAssemblyRefs()
		{
			return this._assemblyRefIndex.Rows;
		}

		// Token: 0x06002D5C RID: 11612 RVA: 0x0007FFBE File Offset: 0x0007E1BE
		protected override ModuleReferenceHandle GetOrAddModuleReferenceHandle(string reference)
		{
			return MetadataTokens.ModuleReferenceHandle(this._moduleRefIndex.GetOrAdd(reference));
		}

		// Token: 0x06002D5D RID: 11613 RVA: 0x0007FFD1 File Offset: 0x0007E1D1
		protected override IReadOnlyList<string> GetModuleRefs()
		{
			return this._moduleRefIndex.Rows;
		}

		// Token: 0x06002D5E RID: 11614 RVA: 0x0007FFDE File Offset: 0x0007E1DE
		protected override MemberReferenceHandle GetOrAddMemberReferenceHandle(ITypeMemberReference reference)
		{
			return MetadataTokens.MemberReferenceHandle(this._memberRefIndex.GetOrAdd(reference));
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x0007FFF1 File Offset: 0x0007E1F1
		protected override IReadOnlyList<ITypeMemberReference> GetMemberRefs()
		{
			return this._memberRefIndex.Rows;
		}

		// Token: 0x06002D60 RID: 11616 RVA: 0x0007FFFE File Offset: 0x0007E1FE
		protected override MethodSpecificationHandle GetOrAddMethodSpecificationHandle(IGenericMethodInstanceReference reference)
		{
			return MetadataTokens.MethodSpecificationHandle(this._methodSpecIndex.GetOrAdd(reference));
		}

		// Token: 0x06002D61 RID: 11617 RVA: 0x00080011 File Offset: 0x0007E211
		protected override IReadOnlyList<IGenericMethodInstanceReference> GetMethodSpecs()
		{
			return this._methodSpecIndex.Rows;
		}

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x06002D62 RID: 11618 RVA: 0x0008001E File Offset: 0x0007E21E
		protected override int GreatestMethodDefIndex
		{
			get
			{
				return this._methodDefs.NextRowId;
			}
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x0008002C File Offset: 0x0007E22C
		protected override bool TryGetTypeReferenceHandle(ITypeReference reference, out TypeReferenceHandle handle)
		{
			int num;
			bool flag = this._typeRefIndex.TryGetValue(reference, out num);
			handle = MetadataTokens.TypeReferenceHandle(num);
			return flag;
		}

		// Token: 0x06002D64 RID: 11620 RVA: 0x00080053 File Offset: 0x0007E253
		protected override TypeReferenceHandle GetOrAddTypeReferenceHandle(ITypeReference reference)
		{
			return MetadataTokens.TypeReferenceHandle(this._typeRefIndex.GetOrAdd(reference));
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x00080066 File Offset: 0x0007E266
		protected override IReadOnlyList<ITypeReference> GetTypeRefs()
		{
			return this._typeRefIndex.Rows;
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x00080073 File Offset: 0x0007E273
		protected override TypeSpecificationHandle GetOrAddTypeSpecificationHandle(ITypeReference reference)
		{
			return MetadataTokens.TypeSpecificationHandle(this._typeSpecIndex.GetOrAdd(reference));
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x00080086 File Offset: 0x0007E286
		protected override IReadOnlyList<ITypeReference> GetTypeSpecs()
		{
			return this._typeSpecIndex.Rows;
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x00080093 File Offset: 0x0007E293
		protected override StandaloneSignatureHandle GetOrAddStandaloneSignatureHandle(BlobHandle blobIndex)
		{
			return MetadataTokens.StandaloneSignatureHandle(this._standAloneSignatureIndex.GetOrAdd(blobIndex));
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x000800A6 File Offset: 0x0007E2A6
		protected override IReadOnlyList<BlobHandle> GetStandaloneSignatureBlobHandles()
		{
			return this._standAloneSignatureIndex.Rows;
		}

		// Token: 0x06002D6A RID: 11626 RVA: 0x000800B3 File Offset: 0x0007E2B3
		protected override void OnIndicesCreated()
		{
			((IPEDeltaAssemblyBuilder)this.module).OnCreatedIndices(this.Context.Diagnostics);
		}

		// Token: 0x06002D6B RID: 11627 RVA: 0x000800D0 File Offset: 0x0007E2D0
		protected override void CreateIndicesForNonTypeMembers(ITypeDefinition typeDef)
		{
			SymbolChange change = this._changes.GetChange(typeDef);
			switch (change)
			{
			case SymbolChange.None:
				return;
			case SymbolChange.ContainsChanges:
				goto IL_008E;
			case SymbolChange.Updated:
				break;
			case SymbolChange.Added:
			{
				this._typeDefs.Add(typeDef);
				IEnumerable<IGenericTypeParameter> consolidatedTypeParameters = base.GetConsolidatedTypeParameters(typeDef);
				if (consolidatedTypeParameters == null)
				{
					goto IL_008E;
				}
				using (IEnumerator<IGenericTypeParameter> enumerator = consolidatedTypeParameters.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IGenericTypeParameter genericTypeParameter = enumerator.Current;
						this._genericParameters.Add(genericTypeParameter);
					}
					goto IL_008E;
				}
				break;
			}
			default:
				throw ExceptionUtilities.UnexpectedValue(change);
			}
			this._typeDefs.AddUpdated(typeDef);
			IL_008E:
			int num;
			this._typeDefs.TryGetValue(typeDef, out num);
			foreach (IEventDefinition eventDefinition in typeDef.GetEvents(this.Context))
			{
				int num2;
				if (!this._eventMap.TryGetValue(num, out num2))
				{
					this._eventMap.Add(num);
				}
				this.AddDefIfNecessary<IEventDefinition>(this._eventDefs, eventDefinition);
			}
			foreach (IFieldDefinition fieldDefinition in typeDef.GetFields(this.Context))
			{
				this.AddDefIfNecessary<IFieldDefinition>(this._fieldDefs, fieldDefinition);
			}
			foreach (IMethodDefinition methodDefinition in typeDef.GetMethods(this.Context))
			{
				if (this.AddDefIfNecessary<IMethodDefinition>(this._methodDefs, methodDefinition))
				{
					foreach (IParameterDefinition parameterDefinition in base.GetParametersToEmit(methodDefinition))
					{
						this._parameterDefs.Add(parameterDefinition);
						this._parameterDefList.Add(KeyValuePairUtil.Create<IMethodDefinition, IParameterDefinition>(methodDefinition, parameterDefinition));
					}
					if (methodDefinition.GenericParameterCount > 0)
					{
						foreach (IGenericMethodParameter genericMethodParameter in methodDefinition.GenericParameters)
						{
							this._genericParameters.Add(genericMethodParameter);
						}
					}
				}
			}
			foreach (IPropertyDefinition propertyDefinition in typeDef.GetProperties(this.Context))
			{
				int num3;
				if (!this._propertyMap.TryGetValue(num, out num3))
				{
					this._propertyMap.Add(num);
				}
				this.AddDefIfNecessary<IPropertyDefinition>(this._propertyDefs, propertyDefinition);
			}
			ArrayBuilder<int> instance = ArrayBuilder<int>.GetInstance();
			foreach (Microsoft.Cci.MethodImplementation methodImplementation in typeDef.GetExplicitImplementationOverrides(this.Context))
			{
				IMethodDefinition methodDefinition2 = (IMethodDefinition)methodImplementation.ImplementingMethod.AsDefinition(this.Context);
				int num4;
				this._methodDefs.TryGetValue(methodDefinition2, out num4);
				MethodImplKey methodImplKey = new MethodImplKey(num4, 1);
				int num5;
				if (!this._methodImpls.TryGetValue(methodImplKey, out num5))
				{
					instance.Add(num4);
					this.methodImplList.Add(methodImplementation);
				}
			}
			foreach (int num6 in instance)
			{
				int num7 = 1;
				MethodImplKey methodImplKey2;
				for (;;)
				{
					methodImplKey2 = new MethodImplKey(num6, num7);
					int num8;
					if (!this._methodImpls.TryGetValue(methodImplKey2, out num8))
					{
						break;
					}
					num7++;
				}
				this._methodImpls.Add(methodImplKey2);
			}
			instance.Free();
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x000804A0 File Offset: 0x0007E6A0
		private bool AddDefIfNecessary<T>(DeltaMetadataWriter.DefinitionIndex<T> defIndex, T def) where T : class, IDefinition
		{
			switch (this._changes.GetChange(def))
			{
			case SymbolChange.ContainsChanges:
				return false;
			case SymbolChange.Updated:
				defIndex.AddUpdated(def);
				return false;
			case SymbolChange.Added:
				defIndex.Add(def);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x000804EA File Offset: 0x0007E6EA
		protected override ReferenceIndexer CreateReferenceVisitor()
		{
			return new DeltaMetadataWriter.DeltaReferenceIndexer(this);
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x000804F4 File Offset: 0x0007E6F4
		protected override void ReportReferencesToAddedSymbols()
		{
			foreach (ITypeReference typeReference in this.GetTypeRefs())
			{
				this.ReportReferencesToAddedSymbol(typeReference.GetInternalSymbol());
			}
			foreach (ITypeMemberReference typeMemberReference in this.GetMemberRefs())
			{
				this.ReportReferencesToAddedSymbol(typeMemberReference.GetInternalSymbol());
			}
		}

		// Token: 0x06002D6F RID: 11631 RVA: 0x00080588 File Offset: 0x0007E788
		[NullableContext(2)]
		private void ReportReferencesToAddedSymbol(ISymbolInternal symbol)
		{
			if (symbol != null && this._changes.IsAdded(symbol.GetISymbol()))
			{
				this.Context.Diagnostics.Add(this.messageProvider.CreateDiagnostic(this.messageProvider.ERR_EncReferenceToAddedMember, MetadataWriter.GetSymbolLocation(symbol), new object[]
				{
					symbol.Name,
					symbol.ContainingAssembly.Name
				}));
			}
		}

		// Token: 0x06002D70 RID: 11632 RVA: 0x000805F4 File Offset: 0x0007E7F4
		protected override StandaloneSignatureHandle SerializeLocalVariablesSignature(IMethodBody body)
		{
			ImmutableArray<ILocalDefinition> localVariables = body.LocalVariables;
			ArrayBuilder<EncLocalInfo> instance = ArrayBuilder<EncLocalInfo>.GetInstance();
			StandaloneSignatureHandle standaloneSignatureHandle;
			if (localVariables.Length > 0)
			{
				PooledBlobBuilder instance2 = PooledBlobBuilder.GetInstance(1024);
				LocalVariablesEncoder localVariablesEncoder = new BlobEncoder(instance2).LocalVariableSignature(localVariables.Length);
				foreach (ILocalDefinition localDefinition in localVariables)
				{
					byte[] array = localDefinition.Signature;
					if (array == null)
					{
						int count = instance2.Count;
						base.SerializeLocalVariableType(localVariablesEncoder.AddVariable(), localDefinition);
						array = instance2.ToArray(count, instance2.Count - count);
					}
					else
					{
						instance2.WriteBytes(array);
					}
					instance.Add(this.CreateEncLocalInfo(localDefinition, array));
				}
				BlobHandle orAddBlob = this.metadata.GetOrAddBlob(instance2);
				standaloneSignatureHandle = this.GetOrAddStandaloneSignatureHandle(orAddBlob);
				instance2.Free();
			}
			else
			{
				standaloneSignatureHandle = default(StandaloneSignatureHandle);
			}
			AddedOrChangedMethodInfo addedOrChangedMethodInfo = new AddedOrChangedMethodInfo(body.MethodId, instance.ToImmutable(), body.LambdaDebugInfo, body.ClosureDebugInfo, body.StateMachineTypeName, body.StateMachineHoistedLocalSlots, body.StateMachineAwaiterSlots);
			this._addedOrChangedMethods.Add(body.MethodDefinition, addedOrChangedMethodInfo);
			instance.Free();
			return standaloneSignatureHandle;
		}

		// Token: 0x06002D71 RID: 11633 RVA: 0x00080728 File Offset: 0x0007E928
		private EncLocalInfo CreateEncLocalInfo(ILocalDefinition localDef, byte[] signature)
		{
			if (localDef.SlotInfo.Id.IsNone)
			{
				return new EncLocalInfo(signature);
			}
			ITypeReference typeReference = localDef.Type;
			ITypeSymbolInternal typeSymbolInternal = typeReference.GetInternalSymbol() as ITypeSymbolInternal;
			if (typeSymbolInternal != null)
			{
				typeReference = this.Context.Module.EncTranslateType(typeSymbolInternal, this.Context.Diagnostics);
			}
			return new EncLocalInfo(localDef.SlotInfo, typeReference, localDef.Constraints, signature);
		}

		// Token: 0x06002D72 RID: 11634 RVA: 0x00080798 File Offset: 0x0007E998
		[NullableContext(0)]
		protected override void PopulateEncLogTableRows(ImmutableArray<int> rowCounts)
		{
			ImmutableArray<int> tableSizes = this._previousGeneration.TableSizes;
			ImmutableArray<int> deltaTableSizes = this.GetDeltaTableSizes(rowCounts);
			this.PopulateEncLogTableRows(TableIndex.AssemblyRef, tableSizes, deltaTableSizes);
			this.PopulateEncLogTableRows(TableIndex.ModuleRef, tableSizes, deltaTableSizes);
			this.PopulateEncLogTableRows(TableIndex.MemberRef, tableSizes, deltaTableSizes);
			this.PopulateEncLogTableRows(TableIndex.MethodSpec, tableSizes, deltaTableSizes);
			this.PopulateEncLogTableRows(TableIndex.TypeRef, tableSizes, deltaTableSizes);
			this.PopulateEncLogTableRows(TableIndex.TypeSpec, tableSizes, deltaTableSizes);
			this.PopulateEncLogTableRows(TableIndex.StandAloneSig, tableSizes, deltaTableSizes);
			this.PopulateEncLogTableRows<ITypeDefinition>(this._typeDefs, TableIndex.TypeDef);
			this.PopulateEncLogTableRows(TableIndex.EventMap, tableSizes, deltaTableSizes);
			this.PopulateEncLogTableRows(TableIndex.PropertyMap, tableSizes, deltaTableSizes);
			this.PopulateEncLogTableEventsOrProperties<IEventDefinition>(this._eventDefs, TableIndex.Event, EditAndContinueOperation.AddEvent, this._eventMap, TableIndex.EventMap);
			this.PopulateEncLogTableFieldsOrMethods<IFieldDefinition>(this._fieldDefs, TableIndex.Field, EditAndContinueOperation.AddField);
			this.PopulateEncLogTableFieldsOrMethods<IMethodDefinition>(this._methodDefs, TableIndex.MethodDef, EditAndContinueOperation.AddMethod);
			this.PopulateEncLogTableEventsOrProperties<IPropertyDefinition>(this._propertyDefs, TableIndex.Property, EditAndContinueOperation.AddProperty, this._propertyMap, TableIndex.PropertyMap);
			this.PopulateEncLogTableParameters();
			this.PopulateEncLogTableRows(TableIndex.Constant, tableSizes, deltaTableSizes);
			this.PopulateEncLogTableRows(TableIndex.CustomAttribute, tableSizes, deltaTableSizes);
			this.PopulateEncLogTableRows(TableIndex.DeclSecurity, tableSizes, deltaTableSizes);
			this.PopulateEncLogTableRows(TableIndex.ClassLayout, tableSizes, deltaTableSizes);
			this.PopulateEncLogTableRows(TableIndex.FieldLayout, tableSizes, deltaTableSizes);
			this.PopulateEncLogTableRows(TableIndex.MethodSemantics, tableSizes, deltaTableSizes);
			this.PopulateEncLogTableRows(TableIndex.MethodImpl, tableSizes, deltaTableSizes);
			this.PopulateEncLogTableRows(TableIndex.ImplMap, tableSizes, deltaTableSizes);
			this.PopulateEncLogTableRows(TableIndex.FieldRva, tableSizes, deltaTableSizes);
			this.PopulateEncLogTableRows(TableIndex.NestedClass, tableSizes, deltaTableSizes);
			this.PopulateEncLogTableRows(TableIndex.GenericParam, tableSizes, deltaTableSizes);
			this.PopulateEncLogTableRows(TableIndex.InterfaceImpl, tableSizes, deltaTableSizes);
			this.PopulateEncLogTableRows(TableIndex.GenericParamConstraint, tableSizes, deltaTableSizes);
		}

		// Token: 0x06002D73 RID: 11635 RVA: 0x000808F4 File Offset: 0x0007EAF4
		private void PopulateEncLogTableEventsOrProperties<T>(DeltaMetadataWriter.DefinitionIndex<T> index, TableIndex table, EditAndContinueOperation addCode, DeltaMetadataWriter.EventOrPropertyMapIndex map, TableIndex mapTable) where T : class, ITypeDefinitionMember
		{
			foreach (T t in index.GetRows())
			{
				if (index.IsAddedNotChanged(t))
				{
					int num = this._typeDefs[t.ContainingTypeDefinition];
					int num2;
					map.TryGetValue(num, out num2);
					this.metadata.AddEncLogEntry(MetadataTokens.Handle(mapTable, num2), addCode);
				}
				this.metadata.AddEncLogEntry(MetadataTokens.Handle(table, index[t]), EditAndContinueOperation.Default);
			}
		}

		// Token: 0x06002D74 RID: 11636 RVA: 0x00080994 File Offset: 0x0007EB94
		private void PopulateEncLogTableFieldsOrMethods<T>(DeltaMetadataWriter.DefinitionIndex<T> index, TableIndex tableIndex, EditAndContinueOperation addCode) where T : class, ITypeDefinitionMember
		{
			foreach (T t in index.GetRows())
			{
				if (index.IsAddedNotChanged(t))
				{
					this.metadata.AddEncLogEntry(MetadataTokens.TypeDefinitionHandle(this._typeDefs[(INamedTypeDefinition)t.ContainingTypeDefinition]), addCode);
				}
				this.metadata.AddEncLogEntry(MetadataTokens.Handle(tableIndex, index[t]), EditAndContinueOperation.Default);
			}
		}

		// Token: 0x06002D75 RID: 11637 RVA: 0x00080A30 File Offset: 0x0007EC30
		private void PopulateEncLogTableParameters()
		{
			int firstRowId = this._parameterDefs.FirstRowId;
			for (int i = 0; i < this._parameterDefList.Count; i++)
			{
				IMethodDefinition key = this._parameterDefList[i].Key;
				this.metadata.AddEncLogEntry(MetadataTokens.MethodDefinitionHandle(this._methodDefs[key]), EditAndContinueOperation.AddParameter);
				this.metadata.AddEncLogEntry(MetadataTokens.ParameterHandle(firstRowId + i), EditAndContinueOperation.Default);
			}
		}

		// Token: 0x06002D76 RID: 11638 RVA: 0x00080AB0 File Offset: 0x0007ECB0
		private void PopulateEncLogTableRows<T>(DeltaMetadataWriter.DefinitionIndex<T> index, TableIndex tableIndex) where T : class, IDefinition
		{
			foreach (T t in index.GetRows())
			{
				this.metadata.AddEncLogEntry(MetadataTokens.Handle(tableIndex, index[t]), EditAndContinueOperation.Default);
			}
		}

		// Token: 0x06002D77 RID: 11639 RVA: 0x00080B10 File Offset: 0x0007ED10
		[NullableContext(0)]
		private void PopulateEncLogTableRows(TableIndex tableIndex, ImmutableArray<int> previousSizes, ImmutableArray<int> deltaSizes)
		{
			this.PopulateEncLogTableRows(tableIndex, previousSizes[(int)tableIndex] + 1, deltaSizes[(int)tableIndex]);
		}

		// Token: 0x06002D78 RID: 11640 RVA: 0x00080B2C File Offset: 0x0007ED2C
		private void PopulateEncLogTableRows(TableIndex tableIndex, int firstRowId, int tokenCount)
		{
			for (int i = 0; i < tokenCount; i++)
			{
				this.metadata.AddEncLogEntry(MetadataTokens.Handle(tableIndex, firstRowId + i), EditAndContinueOperation.Default);
			}
		}

		// Token: 0x06002D79 RID: 11641 RVA: 0x00080B5C File Offset: 0x0007ED5C
		[NullableContext(0)]
		protected override void PopulateEncMapTableRows(ImmutableArray<int> rowCounts)
		{
			ArrayBuilder<EntityHandle> instance = ArrayBuilder<EntityHandle>.GetInstance();
			ImmutableArray<int> tableSizes = this._previousGeneration.TableSizes;
			ImmutableArray<int> deltaTableSizes = this.GetDeltaTableSizes(rowCounts);
			DeltaMetadataWriter.AddReferencedTokens(instance, TableIndex.AssemblyRef, tableSizes, deltaTableSizes);
			DeltaMetadataWriter.AddReferencedTokens(instance, TableIndex.ModuleRef, tableSizes, deltaTableSizes);
			DeltaMetadataWriter.AddReferencedTokens(instance, TableIndex.MemberRef, tableSizes, deltaTableSizes);
			DeltaMetadataWriter.AddReferencedTokens(instance, TableIndex.MethodSpec, tableSizes, deltaTableSizes);
			DeltaMetadataWriter.AddReferencedTokens(instance, TableIndex.TypeRef, tableSizes, deltaTableSizes);
			DeltaMetadataWriter.AddReferencedTokens(instance, TableIndex.TypeSpec, tableSizes, deltaTableSizes);
			DeltaMetadataWriter.AddReferencedTokens(instance, TableIndex.StandAloneSig, tableSizes, deltaTableSizes);
			DeltaMetadataWriter.AddDefinitionTokens<ITypeDefinition>(instance, this._typeDefs, TableIndex.TypeDef);
			DeltaMetadataWriter.AddDefinitionTokens<IEventDefinition>(instance, this._eventDefs, TableIndex.Event);
			DeltaMetadataWriter.AddDefinitionTokens<IFieldDefinition>(instance, this._fieldDefs, TableIndex.Field);
			DeltaMetadataWriter.AddDefinitionTokens<IMethodDefinition>(instance, this._methodDefs, TableIndex.MethodDef);
			DeltaMetadataWriter.AddDefinitionTokens<IPropertyDefinition>(instance, this._propertyDefs, TableIndex.Property);
			DeltaMetadataWriter.AddReferencedTokens(instance, TableIndex.Param, tableSizes, deltaTableSizes);
			DeltaMetadataWriter.AddReferencedTokens(instance, TableIndex.Constant, tableSizes, deltaTableSizes);
			DeltaMetadataWriter.AddReferencedTokens(instance, TableIndex.CustomAttribute, tableSizes, deltaTableSizes);
			DeltaMetadataWriter.AddReferencedTokens(instance, TableIndex.DeclSecurity, tableSizes, deltaTableSizes);
			DeltaMetadataWriter.AddReferencedTokens(instance, TableIndex.ClassLayout, tableSizes, deltaTableSizes);
			DeltaMetadataWriter.AddReferencedTokens(instance, TableIndex.FieldLayout, tableSizes, deltaTableSizes);
			DeltaMetadataWriter.AddReferencedTokens(instance, TableIndex.EventMap, tableSizes, deltaTableSizes);
			DeltaMetadataWriter.AddReferencedTokens(instance, TableIndex.PropertyMap, tableSizes, deltaTableSizes);
			DeltaMetadataWriter.AddReferencedTokens(instance, TableIndex.MethodSemantics, tableSizes, deltaTableSizes);
			DeltaMetadataWriter.AddReferencedTokens(instance, TableIndex.MethodImpl, tableSizes, deltaTableSizes);
			DeltaMetadataWriter.AddReferencedTokens(instance, TableIndex.ImplMap, tableSizes, deltaTableSizes);
			DeltaMetadataWriter.AddReferencedTokens(instance, TableIndex.FieldRva, tableSizes, deltaTableSizes);
			DeltaMetadataWriter.AddReferencedTokens(instance, TableIndex.NestedClass, tableSizes, deltaTableSizes);
			DeltaMetadataWriter.AddReferencedTokens(instance, TableIndex.GenericParam, tableSizes, deltaTableSizes);
			DeltaMetadataWriter.AddReferencedTokens(instance, TableIndex.InterfaceImpl, tableSizes, deltaTableSizes);
			DeltaMetadataWriter.AddReferencedTokens(instance, TableIndex.GenericParamConstraint, tableSizes, deltaTableSizes);
			instance.Sort(HandleComparer.Default);
			foreach (EntityHandle entityHandle in instance)
			{
				this.metadata.AddEncMapEntry(entityHandle);
			}
			instance.Free();
			if (this._debugMetadataOpt != null)
			{
				ArrayBuilder<EntityHandle> instance2 = ArrayBuilder<EntityHandle>.GetInstance();
				DeltaMetadataWriter.AddDefinitionTokens<IMethodDefinition>(instance2, this._methodDefs, TableIndex.MethodDebugInformation);
				instance2.Sort(HandleComparer.Default);
				foreach (EntityHandle entityHandle2 in instance2)
				{
					this._debugMetadataOpt.AddEncMapEntry(entityHandle2);
				}
				instance2.Free();
			}
		}

		// Token: 0x06002D7A RID: 11642 RVA: 0x00080D3D File Offset: 0x0007EF3D
		[NullableContext(0)]
		private static void AddReferencedTokens([Nullable(1)] ArrayBuilder<EntityHandle> builder, TableIndex tableIndex, ImmutableArray<int> previousSizes, ImmutableArray<int> deltaSizes)
		{
			DeltaMetadataWriter.AddReferencedTokens(builder, tableIndex, previousSizes[(int)tableIndex] + 1, deltaSizes[(int)tableIndex]);
		}

		// Token: 0x06002D7B RID: 11643 RVA: 0x00080D58 File Offset: 0x0007EF58
		private static void AddReferencedTokens(ArrayBuilder<EntityHandle> builder, TableIndex tableIndex, int firstRowId, int nTokens)
		{
			for (int i = 0; i < nTokens; i++)
			{
				builder.Add(MetadataTokens.Handle(tableIndex, firstRowId + i));
			}
		}

		// Token: 0x06002D7C RID: 11644 RVA: 0x00080D80 File Offset: 0x0007EF80
		private static void AddDefinitionTokens<T>(ArrayBuilder<EntityHandle> tokens, DeltaMetadataWriter.DefinitionIndex<T> index, TableIndex tableIndex) where T : class, IDefinition
		{
			foreach (T t in index.GetRows())
			{
				tokens.Add(MetadataTokens.Handle(tableIndex, index[t]));
			}
		}

		// Token: 0x06002D7D RID: 11645 RVA: 0x00080DDC File Offset: 0x0007EFDC
		protected override void PopulateEventMapTableRows()
		{
			foreach (int num in this._eventMap.GetRows())
			{
				this.metadata.AddEventMap(MetadataTokens.TypeDefinitionHandle(num), MetadataTokens.EventDefinitionHandle(this._eventMap[num]));
			}
		}

		// Token: 0x06002D7E RID: 11646 RVA: 0x00080E4C File Offset: 0x0007F04C
		protected override void PopulatePropertyMapTableRows()
		{
			foreach (int num in this._propertyMap.GetRows())
			{
				this.metadata.AddPropertyMap(MetadataTokens.TypeDefinitionHandle(num), MetadataTokens.PropertyDefinitionHandle(this._propertyMap[num]));
			}
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x00080EBC File Offset: 0x0007F0BC
		private bool TryGetExistingTypeDefIndex(ITypeDefinition item, out int index)
		{
			if (this._previousGeneration.TypesAdded.TryGetValue(item, ref index))
			{
				return true;
			}
			TypeDefinitionHandle typeDefinitionHandle;
			if (this._definitionMap.TryGetTypeHandle(item, out typeDefinitionHandle))
			{
				index = MetadataTokens.GetRowNumber(typeDefinitionHandle);
				return true;
			}
			index = 0;
			return false;
		}

		// Token: 0x06002D80 RID: 11648 RVA: 0x00080F04 File Offset: 0x0007F104
		private bool TryGetExistingEventDefIndex(IEventDefinition item, out int index)
		{
			if (this._previousGeneration.EventsAdded.TryGetValue(item, ref index))
			{
				return true;
			}
			EventDefinitionHandle eventDefinitionHandle;
			if (this._definitionMap.TryGetEventHandle(item, out eventDefinitionHandle))
			{
				index = MetadataTokens.GetRowNumber(eventDefinitionHandle);
				return true;
			}
			index = 0;
			return false;
		}

		// Token: 0x06002D81 RID: 11649 RVA: 0x00080F4C File Offset: 0x0007F14C
		private bool TryGetExistingFieldDefIndex(IFieldDefinition item, out int index)
		{
			if (this._previousGeneration.FieldsAdded.TryGetValue(item, ref index))
			{
				return true;
			}
			FieldDefinitionHandle fieldDefinitionHandle;
			if (this._definitionMap.TryGetFieldHandle(item, out fieldDefinitionHandle))
			{
				index = MetadataTokens.GetRowNumber(fieldDefinitionHandle);
				return true;
			}
			index = 0;
			return false;
		}

		// Token: 0x06002D82 RID: 11650 RVA: 0x00080F94 File Offset: 0x0007F194
		private bool TryGetExistingMethodDefIndex(IMethodDefinition item, out int index)
		{
			if (this._previousGeneration.MethodsAdded.TryGetValue(item, ref index))
			{
				return true;
			}
			MethodDefinitionHandle methodDefinitionHandle;
			if (this._definitionMap.TryGetMethodHandle(item, out methodDefinitionHandle))
			{
				index = MetadataTokens.GetRowNumber(methodDefinitionHandle);
				return true;
			}
			index = 0;
			return false;
		}

		// Token: 0x06002D83 RID: 11651 RVA: 0x00080FDC File Offset: 0x0007F1DC
		private bool TryGetExistingPropertyDefIndex(IPropertyDefinition item, out int index)
		{
			if (this._previousGeneration.PropertiesAdded.TryGetValue(item, ref index))
			{
				return true;
			}
			PropertyDefinitionHandle propertyDefinitionHandle;
			if (this._definitionMap.TryGetPropertyHandle(item, out propertyDefinitionHandle))
			{
				index = MetadataTokens.GetRowNumber(propertyDefinitionHandle);
				return true;
			}
			index = 0;
			return false;
		}

		// Token: 0x06002D84 RID: 11652 RVA: 0x00081022 File Offset: 0x0007F222
		private bool TryGetExistingEventMapIndex(int item, out int index)
		{
			if (this._previousGeneration.EventMapAdded.TryGetValue(item, ref index))
			{
				return true;
			}
			if (this._previousGeneration.TypeToEventMap.TryGetValue(item, ref index))
			{
				return true;
			}
			index = 0;
			return false;
		}

		// Token: 0x06002D85 RID: 11653 RVA: 0x00081054 File Offset: 0x0007F254
		private bool TryGetExistingPropertyMapIndex(int item, out int index)
		{
			if (this._previousGeneration.PropertyMapAdded.TryGetValue(item, ref index))
			{
				return true;
			}
			if (this._previousGeneration.TypeToPropertyMap.TryGetValue(item, ref index))
			{
				return true;
			}
			index = 0;
			return false;
		}

		// Token: 0x06002D86 RID: 11654 RVA: 0x00081086 File Offset: 0x0007F286
		private bool TryGetExistingMethodImplIndex(MethodImplKey item, out int index)
		{
			if (this._previousGeneration.MethodImplsAdded.TryGetValue(item, ref index))
			{
				return true;
			}
			if (this._previousGeneration.MethodImpls.TryGetValue(item, ref index))
			{
				return true;
			}
			index = 0;
			return false;
		}

		// Token: 0x040011F0 RID: 4592
		private readonly EmitBaseline _previousGeneration;

		// Token: 0x040011F1 RID: 4593
		private readonly Guid _encId;

		// Token: 0x040011F2 RID: 4594
		private readonly DefinitionMap _definitionMap;

		// Token: 0x040011F3 RID: 4595
		private readonly SymbolChanges _changes;

		// Token: 0x040011F4 RID: 4596
		private readonly DeltaMetadataWriter.DefinitionIndex<ITypeDefinition> _typeDefs;

		// Token: 0x040011F5 RID: 4597
		private readonly DeltaMetadataWriter.DefinitionIndex<IEventDefinition> _eventDefs;

		// Token: 0x040011F6 RID: 4598
		private readonly DeltaMetadataWriter.DefinitionIndex<IFieldDefinition> _fieldDefs;

		// Token: 0x040011F7 RID: 4599
		private readonly DeltaMetadataWriter.DefinitionIndex<IMethodDefinition> _methodDefs;

		// Token: 0x040011F8 RID: 4600
		private readonly DeltaMetadataWriter.DefinitionIndex<IPropertyDefinition> _propertyDefs;

		// Token: 0x040011F9 RID: 4601
		private readonly DeltaMetadataWriter.ParameterDefinitionIndex _parameterDefs;

		// Token: 0x040011FA RID: 4602
		[Nullable(new byte[] { 1, 0, 1, 1 })]
		private readonly List<KeyValuePair<IMethodDefinition, IParameterDefinition>> _parameterDefList;

		// Token: 0x040011FB RID: 4603
		private readonly DeltaMetadataWriter.GenericParameterIndex _genericParameters;

		// Token: 0x040011FC RID: 4604
		private readonly DeltaMetadataWriter.EventOrPropertyMapIndex _eventMap;

		// Token: 0x040011FD RID: 4605
		private readonly DeltaMetadataWriter.EventOrPropertyMapIndex _propertyMap;

		// Token: 0x040011FE RID: 4606
		private readonly DeltaMetadataWriter.MethodImplIndex _methodImpls;

		// Token: 0x040011FF RID: 4607
		private readonly MetadataWriter.HeapOrReferenceIndex<AssemblyIdentity> _assemblyRefIndex;

		// Token: 0x04001200 RID: 4608
		private readonly MetadataWriter.HeapOrReferenceIndex<string> _moduleRefIndex;

		// Token: 0x04001201 RID: 4609
		private readonly MetadataWriter.InstanceAndStructuralReferenceIndex<ITypeMemberReference> _memberRefIndex;

		// Token: 0x04001202 RID: 4610
		private readonly MetadataWriter.InstanceAndStructuralReferenceIndex<IGenericMethodInstanceReference> _methodSpecIndex;

		// Token: 0x04001203 RID: 4611
		private readonly MetadataWriter.TypeReferenceIndex _typeRefIndex;

		// Token: 0x04001204 RID: 4612
		private readonly MetadataWriter.InstanceAndStructuralReferenceIndex<ITypeReference> _typeSpecIndex;

		// Token: 0x04001205 RID: 4613
		private readonly MetadataWriter.HeapOrReferenceIndex<BlobHandle> _standAloneSignatureIndex;

		// Token: 0x04001206 RID: 4614
		private readonly Dictionary<IMethodDefinition, AddedOrChangedMethodInfo> _addedOrChangedMethods;

		// Token: 0x02000731 RID: 1841
		[Nullable(0)]
		private abstract class DefinitionIndexBase<T>
		{
			// Token: 0x06003BD0 RID: 15312 RVA: 0x000AC10B File Offset: 0x000AA30B
			public DefinitionIndexBase(int lastRowId, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> comparer = null)
			{
				this.added = new Dictionary<T, int>(comparer);
				this.rows = new List<T>();
				this._firstRowId = lastRowId + 1;
			}

			// Token: 0x06003BD1 RID: 15313
			public abstract bool TryGetValue(T item, out int index);

			// Token: 0x17001010 RID: 4112
			public int this[T item]
			{
				get
				{
					int num;
					this.TryGetValue(item, out num);
					return num;
				}
			}

			// Token: 0x06003BD3 RID: 15315 RVA: 0x000AC14C File Offset: 0x000AA34C
			public IReadOnlyDictionary<T, int> GetAdded()
			{
				this.Freeze();
				return this.added;
			}

			// Token: 0x06003BD4 RID: 15316 RVA: 0x000AC15A File Offset: 0x000AA35A
			public IReadOnlyList<T> GetRows()
			{
				this.Freeze();
				return this.rows;
			}

			// Token: 0x17001011 RID: 4113
			// (get) Token: 0x06003BD5 RID: 15317 RVA: 0x000AC168 File Offset: 0x000AA368
			public int FirstRowId
			{
				get
				{
					return this._firstRowId;
				}
			}

			// Token: 0x17001012 RID: 4114
			// (get) Token: 0x06003BD6 RID: 15318 RVA: 0x000AC170 File Offset: 0x000AA370
			public int NextRowId
			{
				get
				{
					return this.added.Count + this._firstRowId;
				}
			}

			// Token: 0x17001013 RID: 4115
			// (get) Token: 0x06003BD7 RID: 15319 RVA: 0x000AC184 File Offset: 0x000AA384
			public bool IsFrozen
			{
				get
				{
					return this._frozen;
				}
			}

			// Token: 0x06003BD8 RID: 15320 RVA: 0x000AC18C File Offset: 0x000AA38C
			protected virtual void OnFrozen()
			{
			}

			// Token: 0x06003BD9 RID: 15321 RVA: 0x000AC18E File Offset: 0x000AA38E
			private void Freeze()
			{
				if (!this._frozen)
				{
					this._frozen = true;
					this.OnFrozen();
				}
			}

			// Token: 0x04001CAF RID: 7343
			protected readonly Dictionary<T, int> added;

			// Token: 0x04001CB0 RID: 7344
			protected readonly List<T> rows;

			// Token: 0x04001CB1 RID: 7345
			private readonly int _firstRowId;

			// Token: 0x04001CB2 RID: 7346
			private bool _frozen;
		}

		// Token: 0x02000732 RID: 1842
		[Nullable(new byte[] { 0, 1 })]
		private sealed class DefinitionIndex<T> : DeltaMetadataWriter.DefinitionIndexBase<T> where T : class, IDefinition
		{
			// Token: 0x06003BDA RID: 15322 RVA: 0x000AC1A5 File Offset: 0x000AA3A5
			public DefinitionIndex([Nullable(new byte[] { 1, 0 })] DeltaMetadataWriter.DefinitionIndex<T>.TryGetExistingIndex tryGetExistingIndex, int lastRowId)
				: base(lastRowId, ReferenceEqualityComparer.Instance)
			{
				this._tryGetExistingIndex = tryGetExistingIndex;
				this._map = new Dictionary<int, T>();
			}

			// Token: 0x06003BDB RID: 15323 RVA: 0x000AC1C5 File Offset: 0x000AA3C5
			public override bool TryGetValue(T item, out int index)
			{
				if (this.added.TryGetValue(item, ref index))
				{
					return true;
				}
				if (this._tryGetExistingIndex(item, out index))
				{
					this._map[index] = item;
					return true;
				}
				return false;
			}

			// Token: 0x17001014 RID: 4116
			public T this[int rowId]
			{
				get
				{
					return this._map[rowId];
				}
			}

			// Token: 0x06003BDD RID: 15325 RVA: 0x000AC208 File Offset: 0x000AA408
			public void Add(T item)
			{
				int nextRowId = base.NextRowId;
				this.added.Add(item, nextRowId);
				this._map[nextRowId] = item;
				this.rows.Add(item);
			}

			// Token: 0x06003BDE RID: 15326 RVA: 0x000AC242 File Offset: 0x000AA442
			public void AddUpdated(T item)
			{
				this.rows.Add(item);
			}

			// Token: 0x06003BDF RID: 15327 RVA: 0x000AC250 File Offset: 0x000AA450
			public bool IsAddedNotChanged(T item)
			{
				return this.added.ContainsKey(item);
			}

			// Token: 0x06003BE0 RID: 15328 RVA: 0x000AC25E File Offset: 0x000AA45E
			protected override void OnFrozen()
			{
				this.rows.Sort(new Comparison<T>(this.CompareRows));
			}

			// Token: 0x06003BE1 RID: 15329 RVA: 0x000AC277 File Offset: 0x000AA477
			private int CompareRows(T x, T y)
			{
				return base[x] - base[y];
			}

			// Token: 0x04001CB3 RID: 7347
			[Nullable(new byte[] { 1, 0 })]
			private readonly DeltaMetadataWriter.DefinitionIndex<T>.TryGetExistingIndex _tryGetExistingIndex;

			// Token: 0x04001CB4 RID: 7348
			private readonly Dictionary<int, T> _map;

			// Token: 0x020007F0 RID: 2032
			// (Invoke) Token: 0x06004070 RID: 16496
			[NullableContext(0)]
			public delegate bool TryGetExistingIndex(T item, out int index);
		}

		// Token: 0x02000733 RID: 1843
		[Nullable(new byte[] { 0, 1 })]
		private sealed class ParameterDefinitionIndex : DeltaMetadataWriter.DefinitionIndexBase<IParameterDefinition>
		{
			// Token: 0x06003BE2 RID: 15330 RVA: 0x000AC288 File Offset: 0x000AA488
			public ParameterDefinitionIndex(int lastRowId)
				: base(lastRowId, ReferenceEqualityComparer.Instance)
			{
			}

			// Token: 0x06003BE3 RID: 15331 RVA: 0x000AC296 File Offset: 0x000AA496
			public override bool TryGetValue(IParameterDefinition item, out int index)
			{
				return this.added.TryGetValue(item, ref index);
			}

			// Token: 0x06003BE4 RID: 15332 RVA: 0x000AC2A8 File Offset: 0x000AA4A8
			public void Add(IParameterDefinition item)
			{
				int nextRowId = base.NextRowId;
				this.added.Add(item, nextRowId);
				this.rows.Add(item);
			}
		}

		// Token: 0x02000734 RID: 1844
		[Nullable(new byte[] { 0, 1 })]
		private sealed class GenericParameterIndex : DeltaMetadataWriter.DefinitionIndexBase<IGenericParameter>
		{
			// Token: 0x06003BE5 RID: 15333 RVA: 0x000AC2D5 File Offset: 0x000AA4D5
			public GenericParameterIndex(int lastRowId)
				: base(lastRowId, ReferenceEqualityComparer.Instance)
			{
			}

			// Token: 0x06003BE6 RID: 15334 RVA: 0x000AC2E3 File Offset: 0x000AA4E3
			public override bool TryGetValue(IGenericParameter item, out int index)
			{
				return this.added.TryGetValue(item, ref index);
			}

			// Token: 0x06003BE7 RID: 15335 RVA: 0x000AC2F4 File Offset: 0x000AA4F4
			public void Add(IGenericParameter item)
			{
				int nextRowId = base.NextRowId;
				this.added.Add(item, nextRowId);
				this.rows.Add(item);
			}
		}

		// Token: 0x02000735 RID: 1845
		[NullableContext(0)]
		private sealed class EventOrPropertyMapIndex : DeltaMetadataWriter.DefinitionIndexBase<int>
		{
			// Token: 0x06003BE8 RID: 15336 RVA: 0x000AC321 File Offset: 0x000AA521
			[NullableContext(1)]
			public EventOrPropertyMapIndex(DeltaMetadataWriter.EventOrPropertyMapIndex.TryGetExistingIndex tryGetExistingIndex, int lastRowId)
				: base(lastRowId, null)
			{
				this._tryGetExistingIndex = tryGetExistingIndex;
			}

			// Token: 0x06003BE9 RID: 15337 RVA: 0x000AC332 File Offset: 0x000AA532
			public override bool TryGetValue(int item, out int index)
			{
				if (this.added.TryGetValue(item, ref index))
				{
					return true;
				}
				if (this._tryGetExistingIndex(item, out index))
				{
					return true;
				}
				index = 0;
				return false;
			}

			// Token: 0x06003BEA RID: 15338 RVA: 0x000AC35C File Offset: 0x000AA55C
			public void Add(int item)
			{
				int nextRowId = base.NextRowId;
				this.added.Add(item, nextRowId);
				this.rows.Add(item);
			}

			// Token: 0x04001CB5 RID: 7349
			[Nullable(1)]
			private readonly DeltaMetadataWriter.EventOrPropertyMapIndex.TryGetExistingIndex _tryGetExistingIndex;

			// Token: 0x020007F1 RID: 2033
			// (Invoke) Token: 0x06004074 RID: 16500
			public delegate bool TryGetExistingIndex(int item, out int index);
		}

		// Token: 0x02000736 RID: 1846
		[Nullable(0)]
		private sealed class MethodImplIndex : DeltaMetadataWriter.DefinitionIndexBase<MethodImplKey>
		{
			// Token: 0x06003BEB RID: 15339 RVA: 0x000AC389 File Offset: 0x000AA589
			public MethodImplIndex(DeltaMetadataWriter writer, int lastRowId)
				: base(lastRowId, null)
			{
				this._writer = writer;
			}

			// Token: 0x06003BEC RID: 15340 RVA: 0x000AC39A File Offset: 0x000AA59A
			public override bool TryGetValue(MethodImplKey item, out int index)
			{
				if (this.added.TryGetValue(item, ref index))
				{
					return true;
				}
				if (this._writer.TryGetExistingMethodImplIndex(item, out index))
				{
					return true;
				}
				index = 0;
				return false;
			}

			// Token: 0x06003BED RID: 15341 RVA: 0x000AC3C4 File Offset: 0x000AA5C4
			public void Add(MethodImplKey item)
			{
				int nextRowId = base.NextRowId;
				this.added.Add(item, nextRowId);
				this.rows.Add(item);
			}

			// Token: 0x04001CB6 RID: 7350
			private readonly DeltaMetadataWriter _writer;
		}

		// Token: 0x02000737 RID: 1847
		[Nullable(0)]
		private sealed class DeltaReferenceIndexer : ReferenceIndexer
		{
			// Token: 0x06003BEE RID: 15342 RVA: 0x000AC3F1 File Offset: 0x000AA5F1
			public DeltaReferenceIndexer(DeltaMetadataWriter writer)
				: base(writer)
			{
				this._changes = writer._changes;
			}

			// Token: 0x06003BEF RID: 15343 RVA: 0x000AC406 File Offset: 0x000AA606
			public override void Visit(CommonPEModuleBuilder module)
			{
				base.Visit(module.GetTopLevelTypeDefinitions(this.metadataWriter.Context));
			}

			// Token: 0x06003BF0 RID: 15344 RVA: 0x000AC41F File Offset: 0x000AA61F
			public override void Visit(IEventDefinition eventDefinition)
			{
				base.Visit(eventDefinition);
			}

			// Token: 0x06003BF1 RID: 15345 RVA: 0x000AC428 File Offset: 0x000AA628
			public override void Visit(IFieldDefinition fieldDefinition)
			{
				base.Visit(fieldDefinition);
			}

			// Token: 0x06003BF2 RID: 15346 RVA: 0x000AC431 File Offset: 0x000AA631
			public override void Visit(ILocalDefinition localDefinition)
			{
				if (localDefinition.Signature == null)
				{
					base.Visit(localDefinition);
				}
			}

			// Token: 0x06003BF3 RID: 15347 RVA: 0x000AC442 File Offset: 0x000AA642
			public override void Visit(IMethodDefinition method)
			{
				base.Visit(method);
			}

			// Token: 0x06003BF4 RID: 15348 RVA: 0x000AC44C File Offset: 0x000AA64C
			public override void Visit(Microsoft.Cci.MethodImplementation methodImplementation)
			{
				IMethodDefinition methodDefinition = (IMethodDefinition)methodImplementation.ImplementingMethod.AsDefinition(this.Context);
				if (this._changes.GetChange(methodDefinition) == SymbolChange.Added)
				{
					base.Visit(methodImplementation);
				}
			}

			// Token: 0x06003BF5 RID: 15349 RVA: 0x000AC486 File Offset: 0x000AA686
			public override void Visit(INamespaceTypeDefinition namespaceTypeDefinition)
			{
				base.Visit(namespaceTypeDefinition);
			}

			// Token: 0x06003BF6 RID: 15350 RVA: 0x000AC48F File Offset: 0x000AA68F
			public override void Visit(INestedTypeDefinition nestedTypeDefinition)
			{
				base.Visit(nestedTypeDefinition);
			}

			// Token: 0x06003BF7 RID: 15351 RVA: 0x000AC498 File Offset: 0x000AA698
			public override void Visit(IPropertyDefinition propertyDefinition)
			{
				base.Visit(propertyDefinition);
			}

			// Token: 0x06003BF8 RID: 15352 RVA: 0x000AC4A1 File Offset: 0x000AA6A1
			public override void Visit(ITypeDefinition typeDefinition)
			{
				if (this.ShouldVisit(typeDefinition))
				{
					base.Visit(typeDefinition);
				}
			}

			// Token: 0x06003BF9 RID: 15353 RVA: 0x000AC4B3 File Offset: 0x000AA6B3
			public override void Visit(ITypeDefinitionMember typeMember)
			{
				if (this.ShouldVisit(typeMember))
				{
					base.Visit(typeMember);
				}
			}

			// Token: 0x06003BFA RID: 15354 RVA: 0x000AC4C5 File Offset: 0x000AA6C5
			private bool ShouldVisit(IDefinition def)
			{
				return this._changes.GetChange(def) > SymbolChange.None;
			}

			// Token: 0x04001CB7 RID: 7351
			private readonly SymbolChanges _changes;
		}
	}
}
