using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Emit;
using Roslyn.Utilities;

namespace Microsoft.Cci
{
	// Token: 0x0200008C RID: 140
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class FullMetadataWriter : MetadataWriter
	{
		// Token: 0x0600037A RID: 890 RVA: 0x0000D114 File Offset: 0x0000B314
		public static MetadataWriter Create(EmitContext context, CommonMessageProvider messageProvider, bool metadataOnly, bool deterministic, bool emitTestCoverageData, bool hasPdbStream, CancellationToken cancellationToken)
		{
			MetadataBuilder metadataBuilder = new MetadataBuilder(0, 0, 0, 0);
			DebugInformationFormat debugInformationFormat = context.Module.DebugInformationFormat;
			MetadataBuilder metadataBuilder2;
			if (debugInformationFormat != DebugInformationFormat.PortablePdb)
			{
				if (debugInformationFormat != DebugInformationFormat.Embedded)
				{
					metadataBuilder2 = null;
				}
				else
				{
					metadataBuilder2 = (metadataOnly ? null : new MetadataBuilder(0, 0, 0, 0));
				}
			}
			else
			{
				metadataBuilder2 = (hasPdbStream ? new MetadataBuilder(0, 0, 0, 0) : null);
			}
			DynamicAnalysisDataWriter dynamicAnalysisDataWriter = (emitTestCoverageData ? new DynamicAnalysisDataWriter(context.Module.DebugDocumentCount, context.Module.HintNumberOfMethodDefinitions) : null);
			return new FullMetadataWriter(context, metadataBuilder, metadataBuilder2, dynamicAnalysisDataWriter, messageProvider, metadataOnly, deterministic, emitTestCoverageData, cancellationToken);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000D19C File Offset: 0x0000B39C
		private FullMetadataWriter(EmitContext context, MetadataBuilder builder, [Nullable(2)] MetadataBuilder debugBuilderOpt, [Nullable(2)] DynamicAnalysisDataWriter dynamicAnalysisDataWriterOpt, CommonMessageProvider messageProvider, bool metadataOnly, bool deterministic, bool emitTestCoverageData, CancellationToken cancellationToken)
			: base(builder, debugBuilderOpt, dynamicAnalysisDataWriterOpt, context, messageProvider, metadataOnly, deterministic, emitTestCoverageData, cancellationToken)
		{
			int hintNumberOfMethodDefinitions = this.module.HintNumberOfMethodDefinitions;
			int num = hintNumberOfMethodDefinitions / 6;
			int num2 = num * 4;
			int num3 = hintNumberOfMethodDefinitions / 4;
			this._typeDefs = new FullMetadataWriter.DefinitionIndex<ITypeDefinition>(num);
			this._eventDefs = new FullMetadataWriter.DefinitionIndex<IEventDefinition>(0);
			this._fieldDefs = new FullMetadataWriter.DefinitionIndex<IFieldDefinition>(num2);
			this._methodDefs = new FullMetadataWriter.DefinitionIndex<IMethodDefinition>(hintNumberOfMethodDefinitions);
			this._propertyDefs = new FullMetadataWriter.DefinitionIndex<IPropertyDefinition>(num3);
			this._parameterDefs = new FullMetadataWriter.DefinitionIndex<IParameterDefinition>(hintNumberOfMethodDefinitions);
			this._genericParameters = new FullMetadataWriter.DefinitionIndex<IGenericParameter>(0);
			this._fieldDefIndex = new Dictionary<ITypeDefinition, int>(num, ReferenceEqualityComparer.Instance);
			this._methodDefIndex = new Dictionary<ITypeDefinition, int>(num, ReferenceEqualityComparer.Instance);
			this._parameterListIndex = new Dictionary<IMethodDefinition, int>(hintNumberOfMethodDefinitions, ReferenceEqualityComparer.Instance);
			this._assemblyRefIndex = new MetadataWriter.HeapOrReferenceIndex<AssemblyIdentity>(this, 0);
			this._moduleRefIndex = new MetadataWriter.HeapOrReferenceIndex<string>(this, 0);
			this._memberRefIndex = new MetadataWriter.InstanceAndStructuralReferenceIndex<ITypeMemberReference>(this, new MemberRefComparer(this), 0);
			this._methodSpecIndex = new MetadataWriter.InstanceAndStructuralReferenceIndex<IGenericMethodInstanceReference>(this, new MethodSpecComparer(this), 0);
			this._typeRefIndex = new MetadataWriter.TypeReferenceIndex(this, 0);
			this._typeSpecIndex = new MetadataWriter.InstanceAndStructuralReferenceIndex<ITypeReference>(this, new TypeSpecComparer(this), 0);
			this._standAloneSignatureIndex = new MetadataWriter.HeapOrReferenceIndex<BlobHandle>(this, 0);
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000D2CA File Offset: 0x0000B4CA
		protected override ushort Generation
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000D2CD File Offset: 0x0000B4CD
		protected override Guid EncId
		{
			get
			{
				return Guid.Empty;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0000D2D4 File Offset: 0x0000B4D4
		protected override Guid EncBaseId
		{
			get
			{
				return Guid.Empty;
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000D2DC File Offset: 0x0000B4DC
		protected override bool TryGetTypeDefinitionHandle(ITypeDefinition def, out TypeDefinitionHandle handle)
		{
			int num;
			bool flag = this._typeDefs.TryGetValue(def, out num);
			handle = MetadataTokens.TypeDefinitionHandle(num);
			return flag;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000D308 File Offset: 0x0000B508
		protected override TypeDefinitionHandle GetTypeDefinitionHandle(ITypeDefinition def)
		{
			return MetadataTokens.TypeDefinitionHandle(this._typeDefs[def]);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000D32C File Offset: 0x0000B52C
		protected override ITypeDefinition GetTypeDef(TypeDefinitionHandle handle)
		{
			return this._typeDefs[MetadataTokens.GetRowNumber(handle)];
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000D354 File Offset: 0x0000B554
		protected override IReadOnlyList<ITypeDefinition> GetTypeDefs()
		{
			return this._typeDefs.Rows;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000D370 File Offset: 0x0000B570
		protected override EventDefinitionHandle GetEventDefinitionHandle(IEventDefinition def)
		{
			return MetadataTokens.EventDefinitionHandle(this._eventDefs[def]);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000D394 File Offset: 0x0000B594
		protected override IReadOnlyList<IEventDefinition> GetEventDefs()
		{
			return this._eventDefs.Rows;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000D3B0 File Offset: 0x0000B5B0
		protected override FieldDefinitionHandle GetFieldDefinitionHandle(IFieldDefinition def)
		{
			return MetadataTokens.FieldDefinitionHandle(this._fieldDefs[def]);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000D3D4 File Offset: 0x0000B5D4
		protected override IReadOnlyList<IFieldDefinition> GetFieldDefs()
		{
			return this._fieldDefs.Rows;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000D3F0 File Offset: 0x0000B5F0
		protected override bool TryGetMethodDefinitionHandle(IMethodDefinition def, out MethodDefinitionHandle handle)
		{
			int num;
			bool flag = this._methodDefs.TryGetValue(def, out num);
			handle = MetadataTokens.MethodDefinitionHandle(num);
			return flag;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000D41C File Offset: 0x0000B61C
		protected override MethodDefinitionHandle GetMethodDefinitionHandle(IMethodDefinition def)
		{
			return MetadataTokens.MethodDefinitionHandle(this._methodDefs[def]);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000D440 File Offset: 0x0000B640
		protected override IMethodDefinition GetMethodDef(MethodDefinitionHandle handle)
		{
			return this._methodDefs[MetadataTokens.GetRowNumber(handle)];
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000D468 File Offset: 0x0000B668
		protected override IReadOnlyList<IMethodDefinition> GetMethodDefs()
		{
			return this._methodDefs.Rows;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000D484 File Offset: 0x0000B684
		protected override PropertyDefinitionHandle GetPropertyDefIndex(IPropertyDefinition def)
		{
			return MetadataTokens.PropertyDefinitionHandle(this._propertyDefs[def]);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000D4A8 File Offset: 0x0000B6A8
		protected override IReadOnlyList<IPropertyDefinition> GetPropertyDefs()
		{
			return this._propertyDefs.Rows;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000D4C4 File Offset: 0x0000B6C4
		protected override ParameterHandle GetParameterHandle(IParameterDefinition def)
		{
			return MetadataTokens.ParameterHandle(this._parameterDefs[def]);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000D4E8 File Offset: 0x0000B6E8
		protected override IReadOnlyList<IParameterDefinition> GetParameterDefs()
		{
			return this._parameterDefs.Rows;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000D504 File Offset: 0x0000B704
		protected override IReadOnlyList<IGenericParameter> GetGenericParameters()
		{
			return this._genericParameters.Rows;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000D51F File Offset: 0x0000B71F
		protected override FieldDefinitionHandle GetFirstFieldDefinitionHandle(INamedTypeDefinition typeDef)
		{
			return MetadataTokens.FieldDefinitionHandle(this._fieldDefIndex[typeDef]);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000D532 File Offset: 0x0000B732
		protected override MethodDefinitionHandle GetFirstMethodDefinitionHandle(INamedTypeDefinition typeDef)
		{
			return MetadataTokens.MethodDefinitionHandle(this._methodDefIndex[typeDef]);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000D545 File Offset: 0x0000B745
		protected override ParameterHandle GetFirstParameterHandle(IMethodDefinition methodDef)
		{
			return MetadataTokens.ParameterHandle(this._parameterListIndex[methodDef]);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000D558 File Offset: 0x0000B758
		protected override AssemblyReferenceHandle GetOrAddAssemblyReferenceHandle(IAssemblyReference reference)
		{
			return MetadataTokens.AssemblyReferenceHandle(this._assemblyRefIndex.GetOrAdd(reference.Identity));
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000D570 File Offset: 0x0000B770
		protected override IReadOnlyList<AssemblyIdentity> GetAssemblyRefs()
		{
			return this._assemblyRefIndex.Rows;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000D57D File Offset: 0x0000B77D
		protected override ModuleReferenceHandle GetOrAddModuleReferenceHandle(string reference)
		{
			return MetadataTokens.ModuleReferenceHandle(this._moduleRefIndex.GetOrAdd(reference));
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000D590 File Offset: 0x0000B790
		protected override IReadOnlyList<string> GetModuleRefs()
		{
			return this._moduleRefIndex.Rows;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000D59D File Offset: 0x0000B79D
		protected override MemberReferenceHandle GetOrAddMemberReferenceHandle(ITypeMemberReference reference)
		{
			return MetadataTokens.MemberReferenceHandle(this._memberRefIndex.GetOrAdd(reference));
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000D5B0 File Offset: 0x0000B7B0
		protected override IReadOnlyList<ITypeMemberReference> GetMemberRefs()
		{
			return this._memberRefIndex.Rows;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000D5BD File Offset: 0x0000B7BD
		protected override MethodSpecificationHandle GetOrAddMethodSpecificationHandle(IGenericMethodInstanceReference reference)
		{
			return MetadataTokens.MethodSpecificationHandle(this._methodSpecIndex.GetOrAdd(reference));
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000D5D0 File Offset: 0x0000B7D0
		protected override IReadOnlyList<IGenericMethodInstanceReference> GetMethodSpecs()
		{
			return this._methodSpecIndex.Rows;
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000D5E0 File Offset: 0x0000B7E0
		protected override int GreatestMethodDefIndex
		{
			get
			{
				return this._methodDefs.NextRowId;
			}
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000D5FC File Offset: 0x0000B7FC
		protected override bool TryGetTypeReferenceHandle(ITypeReference reference, out TypeReferenceHandle handle)
		{
			int num;
			bool flag = this._typeRefIndex.TryGetValue(reference, out num);
			handle = MetadataTokens.TypeReferenceHandle(num);
			return flag;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000D623 File Offset: 0x0000B823
		protected override TypeReferenceHandle GetOrAddTypeReferenceHandle(ITypeReference reference)
		{
			return MetadataTokens.TypeReferenceHandle(this._typeRefIndex.GetOrAdd(reference));
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000D636 File Offset: 0x0000B836
		protected override IReadOnlyList<ITypeReference> GetTypeRefs()
		{
			return this._typeRefIndex.Rows;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000D643 File Offset: 0x0000B843
		protected override TypeSpecificationHandle GetOrAddTypeSpecificationHandle(ITypeReference reference)
		{
			return MetadataTokens.TypeSpecificationHandle(this._typeSpecIndex.GetOrAdd(reference));
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000D656 File Offset: 0x0000B856
		protected override IReadOnlyList<ITypeReference> GetTypeSpecs()
		{
			return this._typeSpecIndex.Rows;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000D663 File Offset: 0x0000B863
		protected override StandaloneSignatureHandle GetOrAddStandaloneSignatureHandle(BlobHandle blobIndex)
		{
			return MetadataTokens.StandaloneSignatureHandle(this._standAloneSignatureIndex.GetOrAdd(blobIndex));
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000D676 File Offset: 0x0000B876
		protected override IReadOnlyList<BlobHandle> GetStandaloneSignatureBlobHandles()
		{
			return this._standAloneSignatureIndex.Rows;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000D683 File Offset: 0x0000B883
		protected override ReferenceIndexer CreateReferenceVisitor()
		{
			return new FullMetadataWriter.FullReferenceIndexer(this);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000D68B File Offset: 0x0000B88B
		protected override void ReportReferencesToAddedSymbols()
		{
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000D68D File Offset: 0x0000B88D
		[NullableContext(0)]
		protected override void PopulateEncLogTableRows(ImmutableArray<int> rowCounts)
		{
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000D68F File Offset: 0x0000B88F
		[NullableContext(0)]
		protected override void PopulateEncMapTableRows(ImmutableArray<int> rowCounts)
		{
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000D694 File Offset: 0x0000B894
		protected override void PopulateEventMapTableRows()
		{
			ITypeDefinition typeDefinition = null;
			foreach (IEventDefinition eventDefinition in this.GetEventDefs())
			{
				if (eventDefinition.ContainingTypeDefinition != typeDefinition)
				{
					typeDefinition = eventDefinition.ContainingTypeDefinition;
					this.metadata.AddEventMap(this.GetTypeDefinitionHandle(typeDefinition), this.GetEventDefinitionHandle(eventDefinition));
				}
			}
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000D708 File Offset: 0x0000B908
		protected override void PopulatePropertyMapTableRows()
		{
			ITypeDefinition typeDefinition = null;
			foreach (IPropertyDefinition propertyDefinition in this.GetPropertyDefs())
			{
				if (propertyDefinition.ContainingTypeDefinition != typeDefinition)
				{
					typeDefinition = propertyDefinition.ContainingTypeDefinition;
					this.metadata.AddPropertyMap(this.GetTypeDefinitionHandle(typeDefinition), this.GetPropertyDefIndex(propertyDefinition));
				}
			}
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000D77C File Offset: 0x0000B97C
		protected override void CreateIndicesForNonTypeMembers(ITypeDefinition typeDef)
		{
			this._typeDefs.Add(typeDef);
			IEnumerable<IGenericTypeParameter> consolidatedTypeParameters = base.GetConsolidatedTypeParameters(typeDef);
			if (consolidatedTypeParameters != null)
			{
				foreach (IGenericTypeParameter genericTypeParameter in consolidatedTypeParameters)
				{
					this._genericParameters.Add(genericTypeParameter);
				}
			}
			foreach (MethodImplementation methodImplementation in typeDef.GetExplicitImplementationOverrides(this.Context))
			{
				this.methodImplList.Add(methodImplementation);
			}
			foreach (IEventDefinition eventDefinition in typeDef.GetEvents(this.Context))
			{
				this._eventDefs.Add(eventDefinition);
			}
			this._fieldDefIndex.Add(typeDef, this._fieldDefs.NextRowId);
			foreach (IFieldDefinition fieldDefinition in typeDef.GetFields(this.Context))
			{
				this._fieldDefs.Add(fieldDefinition);
			}
			this._methodDefIndex.Add(typeDef, this._methodDefs.NextRowId);
			foreach (IMethodDefinition methodDefinition in typeDef.GetMethods(this.Context))
			{
				this.CreateIndicesFor(methodDefinition);
				this._methodDefs.Add(methodDefinition);
			}
			foreach (IPropertyDefinition propertyDefinition in typeDef.GetProperties(this.Context))
			{
				this._propertyDefs.Add(propertyDefinition);
			}
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000D9BC File Offset: 0x0000BBBC
		private void CreateIndicesFor(IMethodDefinition methodDef)
		{
			this._parameterListIndex.Add(methodDef, this._parameterDefs.NextRowId);
			foreach (IParameterDefinition parameterDefinition in base.GetParametersToEmit(methodDef))
			{
				this._parameterDefs.Add(parameterDefinition);
			}
			if (methodDef.GenericParameterCount > 0)
			{
				foreach (IGenericMethodParameter genericMethodParameter in methodDef.GenericParameters)
				{
					this._genericParameters.Add(genericMethodParameter);
				}
			}
		}

		// Token: 0x0400017F RID: 383
		[Nullable(new byte[] { 0, 1 })]
		private readonly FullMetadataWriter.DefinitionIndex<ITypeDefinition> _typeDefs;

		// Token: 0x04000180 RID: 384
		[Nullable(new byte[] { 0, 1 })]
		private readonly FullMetadataWriter.DefinitionIndex<IEventDefinition> _eventDefs;

		// Token: 0x04000181 RID: 385
		[Nullable(new byte[] { 0, 1 })]
		private readonly FullMetadataWriter.DefinitionIndex<IFieldDefinition> _fieldDefs;

		// Token: 0x04000182 RID: 386
		[Nullable(new byte[] { 0, 1 })]
		private readonly FullMetadataWriter.DefinitionIndex<IMethodDefinition> _methodDefs;

		// Token: 0x04000183 RID: 387
		[Nullable(new byte[] { 0, 1 })]
		private readonly FullMetadataWriter.DefinitionIndex<IPropertyDefinition> _propertyDefs;

		// Token: 0x04000184 RID: 388
		[Nullable(new byte[] { 0, 1 })]
		private readonly FullMetadataWriter.DefinitionIndex<IParameterDefinition> _parameterDefs;

		// Token: 0x04000185 RID: 389
		[Nullable(new byte[] { 0, 1 })]
		private readonly FullMetadataWriter.DefinitionIndex<IGenericParameter> _genericParameters;

		// Token: 0x04000186 RID: 390
		private readonly Dictionary<ITypeDefinition, int> _fieldDefIndex;

		// Token: 0x04000187 RID: 391
		private readonly Dictionary<ITypeDefinition, int> _methodDefIndex;

		// Token: 0x04000188 RID: 392
		private readonly Dictionary<IMethodDefinition, int> _parameterListIndex;

		// Token: 0x04000189 RID: 393
		private readonly MetadataWriter.HeapOrReferenceIndex<AssemblyIdentity> _assemblyRefIndex;

		// Token: 0x0400018A RID: 394
		private readonly MetadataWriter.HeapOrReferenceIndex<string> _moduleRefIndex;

		// Token: 0x0400018B RID: 395
		private readonly MetadataWriter.InstanceAndStructuralReferenceIndex<ITypeMemberReference> _memberRefIndex;

		// Token: 0x0400018C RID: 396
		private readonly MetadataWriter.InstanceAndStructuralReferenceIndex<IGenericMethodInstanceReference> _methodSpecIndex;

		// Token: 0x0400018D RID: 397
		private readonly MetadataWriter.TypeReferenceIndex _typeRefIndex;

		// Token: 0x0400018E RID: 398
		private readonly MetadataWriter.InstanceAndStructuralReferenceIndex<ITypeReference> _typeSpecIndex;

		// Token: 0x0400018F RID: 399
		private readonly MetadataWriter.HeapOrReferenceIndex<BlobHandle> _standAloneSignatureIndex;

		// Token: 0x0200051F RID: 1311
		[NullableContext(0)]
		private sealed class FullReferenceIndexer : ReferenceIndexer
		{
			// Token: 0x0600334A RID: 13130 RVA: 0x000914C7 File Offset: 0x0008F6C7
			[NullableContext(1)]
			internal FullReferenceIndexer(MetadataWriter metadataWriter)
				: base(metadataWriter)
			{
			}
		}

		// Token: 0x02000520 RID: 1312
		[Nullable(0)]
		private struct DefinitionIndex<T> where T : class, IReference
		{
			// Token: 0x0600334B RID: 13131 RVA: 0x000914D0 File Offset: 0x0008F6D0
			public DefinitionIndex(int capacity)
			{
				this._index = new Dictionary<T, int>(capacity, ReferenceEqualityComparer.Instance);
				this._rows = new List<T>(capacity);
			}

			// Token: 0x0600334C RID: 13132 RVA: 0x000914EF File Offset: 0x0008F6EF
			public bool TryGetValue(T item, out int rowId)
			{
				return this._index.TryGetValue(item, ref rowId);
			}

			// Token: 0x17000EB8 RID: 3768
			public int this[T item]
			{
				get
				{
					return this._index[item];
				}
			}

			// Token: 0x17000EB9 RID: 3769
			public T this[int rowId]
			{
				get
				{
					return this._rows[rowId - 1];
				}
			}

			// Token: 0x17000EBA RID: 3770
			// (get) Token: 0x0600334F RID: 13135 RVA: 0x0009151C File Offset: 0x0008F71C
			public IReadOnlyList<T> Rows
			{
				get
				{
					return this._rows;
				}
			}

			// Token: 0x17000EBB RID: 3771
			// (get) Token: 0x06003350 RID: 13136 RVA: 0x00091524 File Offset: 0x0008F724
			public int NextRowId
			{
				get
				{
					return this._rows.Count + 1;
				}
			}

			// Token: 0x06003351 RID: 13137 RVA: 0x00091533 File Offset: 0x0008F733
			public void Add(T item)
			{
				this._index.Add(item, this.NextRowId);
				this._rows.Add(item);
			}

			// Token: 0x04001499 RID: 5273
			private readonly Dictionary<T, int> _index;

			// Token: 0x0400149A RID: 5274
			private readonly List<T> _rows;
		}
	}
}
