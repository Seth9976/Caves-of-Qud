using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.Collections;
using Microsoft.CodeAnalysis.Debugging;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Symbols;
using Microsoft.DiaSymReader;
using Roslyn.Utilities;

namespace Microsoft.Cci
{
	// Token: 0x020000AD RID: 173
	internal abstract class MetadataWriter
	{
		// Token: 0x060004C7 RID: 1223 RVA: 0x0000EE74 File Offset: 0x0000D074
		protected MetadataWriter(MetadataBuilder metadata, MetadataBuilder debugMetadataOpt, DynamicAnalysisDataWriter dynamicAnalysisDataWriterOpt, EmitContext context, CommonMessageProvider messageProvider, bool metadataOnly, bool deterministic, bool emitTestCoverageData, CancellationToken cancellationToken)
		{
			this.module = context.Module;
			this._deterministic = deterministic;
			this.MetadataOnly = metadataOnly;
			this.EmitTestCoverageData = emitTestCoverageData;
			this._signatureIndex = new Dictionary<ISignature, KeyValuePair<BlobHandle, ImmutableArray<byte>>>(this.module.HintNumberOfMethodDefinitions, ReferenceEqualityComparer.Instance);
			this._numTypeDefsEstimate = this.module.HintNumberOfMethodDefinitions / 6;
			this.Context = context;
			this.messageProvider = messageProvider;
			this._cancellationToken = cancellationToken;
			this.metadata = metadata;
			this._debugMetadataOpt = debugMetadataOpt;
			this._dynamicAnalysisDataWriterOpt = dynamicAnalysisDataWriterOpt;
			this._smallMethodBodies = new Dictionary<ValueTuple<ImmutableArray<byte>, bool>, int>(MetadataWriter.ByteSequenceBoolTupleComparer.Instance);
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x0000EFAA File Offset: 0x0000D1AA
		private int NumberOfTypeDefsEstimate
		{
			get
			{
				return this._numTypeDefsEstimate;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0000EFB2 File Offset: 0x0000D1B2
		internal bool IsFullMetadata
		{
			get
			{
				return this.Generation == 0;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x0000EFBD File Offset: 0x0000D1BD
		private bool IsMinimalDelta
		{
			get
			{
				return !this.IsFullMetadata;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x0000EFC8 File Offset: 0x0000D1C8
		private bool EmitAssemblyDefinition
		{
			get
			{
				return this.module.OutputKind != OutputKind.NetModule && !this.IsMinimalDelta;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060004CC RID: 1228
		protected abstract ushort Generation { get; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060004CD RID: 1229
		protected abstract Guid EncId { get; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060004CE RID: 1230
		protected abstract Guid EncBaseId { get; }

		// Token: 0x060004CF RID: 1231
		protected abstract bool TryGetTypeDefinitionHandle(ITypeDefinition def, out TypeDefinitionHandle handle);

		// Token: 0x060004D0 RID: 1232
		protected abstract TypeDefinitionHandle GetTypeDefinitionHandle(ITypeDefinition def);

		// Token: 0x060004D1 RID: 1233
		protected abstract ITypeDefinition GetTypeDef(TypeDefinitionHandle handle);

		// Token: 0x060004D2 RID: 1234
		protected abstract IReadOnlyList<ITypeDefinition> GetTypeDefs();

		// Token: 0x060004D3 RID: 1235
		protected abstract EventDefinitionHandle GetEventDefinitionHandle(IEventDefinition def);

		// Token: 0x060004D4 RID: 1236
		protected abstract IReadOnlyList<IEventDefinition> GetEventDefs();

		// Token: 0x060004D5 RID: 1237
		protected abstract FieldDefinitionHandle GetFieldDefinitionHandle(IFieldDefinition def);

		// Token: 0x060004D6 RID: 1238
		protected abstract IReadOnlyList<IFieldDefinition> GetFieldDefs();

		// Token: 0x060004D7 RID: 1239
		protected abstract bool TryGetMethodDefinitionHandle(IMethodDefinition def, out MethodDefinitionHandle handle);

		// Token: 0x060004D8 RID: 1240
		protected abstract MethodDefinitionHandle GetMethodDefinitionHandle(IMethodDefinition def);

		// Token: 0x060004D9 RID: 1241
		protected abstract IMethodDefinition GetMethodDef(MethodDefinitionHandle handle);

		// Token: 0x060004DA RID: 1242
		protected abstract IReadOnlyList<IMethodDefinition> GetMethodDefs();

		// Token: 0x060004DB RID: 1243
		protected abstract PropertyDefinitionHandle GetPropertyDefIndex(IPropertyDefinition def);

		// Token: 0x060004DC RID: 1244
		protected abstract IReadOnlyList<IPropertyDefinition> GetPropertyDefs();

		// Token: 0x060004DD RID: 1245
		protected abstract ParameterHandle GetParameterHandle(IParameterDefinition def);

		// Token: 0x060004DE RID: 1246
		protected abstract IReadOnlyList<IParameterDefinition> GetParameterDefs();

		// Token: 0x060004DF RID: 1247
		protected abstract IReadOnlyList<IGenericParameter> GetGenericParameters();

		// Token: 0x060004E0 RID: 1248
		protected abstract FieldDefinitionHandle GetFirstFieldDefinitionHandle(INamedTypeDefinition typeDef);

		// Token: 0x060004E1 RID: 1249
		protected abstract MethodDefinitionHandle GetFirstMethodDefinitionHandle(INamedTypeDefinition typeDef);

		// Token: 0x060004E2 RID: 1250
		protected abstract ParameterHandle GetFirstParameterHandle(IMethodDefinition methodDef);

		// Token: 0x060004E3 RID: 1251
		protected abstract AssemblyReferenceHandle GetOrAddAssemblyReferenceHandle(IAssemblyReference reference);

		// Token: 0x060004E4 RID: 1252
		protected abstract IReadOnlyList<AssemblyIdentity> GetAssemblyRefs();

		// Token: 0x060004E5 RID: 1253
		protected abstract ModuleReferenceHandle GetOrAddModuleReferenceHandle(string reference);

		// Token: 0x060004E6 RID: 1254
		protected abstract IReadOnlyList<string> GetModuleRefs();

		// Token: 0x060004E7 RID: 1255
		protected abstract MemberReferenceHandle GetOrAddMemberReferenceHandle(ITypeMemberReference reference);

		// Token: 0x060004E8 RID: 1256
		protected abstract IReadOnlyList<ITypeMemberReference> GetMemberRefs();

		// Token: 0x060004E9 RID: 1257
		protected abstract MethodSpecificationHandle GetOrAddMethodSpecificationHandle(IGenericMethodInstanceReference reference);

		// Token: 0x060004EA RID: 1258
		protected abstract IReadOnlyList<IGenericMethodInstanceReference> GetMethodSpecs();

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060004EB RID: 1259
		protected abstract int GreatestMethodDefIndex { get; }

		// Token: 0x060004EC RID: 1260
		protected abstract bool TryGetTypeReferenceHandle(ITypeReference reference, out TypeReferenceHandle handle);

		// Token: 0x060004ED RID: 1261
		protected abstract TypeReferenceHandle GetOrAddTypeReferenceHandle(ITypeReference reference);

		// Token: 0x060004EE RID: 1262
		protected abstract IReadOnlyList<ITypeReference> GetTypeRefs();

		// Token: 0x060004EF RID: 1263
		protected abstract TypeSpecificationHandle GetOrAddTypeSpecificationHandle(ITypeReference reference);

		// Token: 0x060004F0 RID: 1264
		protected abstract IReadOnlyList<ITypeReference> GetTypeSpecs();

		// Token: 0x060004F1 RID: 1265
		protected abstract StandaloneSignatureHandle GetOrAddStandaloneSignatureHandle(BlobHandle handle);

		// Token: 0x060004F2 RID: 1266
		protected abstract IReadOnlyList<BlobHandle> GetStandaloneSignatureBlobHandles();

		// Token: 0x060004F3 RID: 1267
		protected abstract void CreateIndicesForNonTypeMembers(ITypeDefinition typeDef);

		// Token: 0x060004F4 RID: 1268
		protected abstract ReferenceIndexer CreateReferenceVisitor();

		// Token: 0x060004F5 RID: 1269
		protected abstract void PopulateEventMapTableRows();

		// Token: 0x060004F6 RID: 1270
		protected abstract void PopulatePropertyMapTableRows();

		// Token: 0x060004F7 RID: 1271
		protected abstract void PopulateEncLogTableRows(ImmutableArray<int> rowCounts);

		// Token: 0x060004F8 RID: 1272
		protected abstract void PopulateEncMapTableRows(ImmutableArray<int> rowCounts);

		// Token: 0x060004F9 RID: 1273
		protected abstract void ReportReferencesToAddedSymbols();

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x0000EFE3 File Offset: 0x0000D1E3
		internal bool EmitPortableDebugMetadata
		{
			get
			{
				return this._debugMetadataOpt != null;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x0000EFEE File Offset: 0x0000D1EE
		internal CommonPEModuleBuilder Module
		{
			get
			{
				return this.module;
			}
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0000EFF8 File Offset: 0x0000D1F8
		private void CreateMethodBodyReferenceIndex()
		{
			ReadOnlySpan<object> readOnlySpan = this.module.ReferencesInIL();
			this._pseudoSymbolTokenToTokenMap = new EntityHandle[readOnlySpan.Length];
			this._pseudoSymbolTokenToReferenceMap = readOnlySpan.ToArray();
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000F030 File Offset: 0x0000D230
		private void CreateIndices()
		{
			this._cancellationToken.ThrowIfCancellationRequested();
			this.CreateUserStringIndices();
			this.CreateInitialAssemblyRefIndex();
			this.CreateInitialFileRefIndex();
			this.CreateIndicesForModule();
			this._referenceVisitor = this.CreateReferenceVisitor();
			this._referenceVisitor.Visit(this.module);
			this.CreateMethodBodyReferenceIndex();
			this.OnIndicesCreated();
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0000F08C File Offset: 0x0000D28C
		private void CreateUserStringIndices()
		{
			this._pseudoStringTokenToStringMap = new List<string>();
			foreach (string text in this.module.GetStrings())
			{
				this._pseudoStringTokenToStringMap.Add(text);
			}
			this._pseudoStringTokenToTokenMap = new UserStringHandle[this._pseudoStringTokenToStringMap.Count];
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0000F104 File Offset: 0x0000D304
		private void CreateIndicesForModule()
		{
			Queue<INestedTypeDefinition> queue = new Queue<INestedTypeDefinition>();
			using (IEnumerator<INamespaceTypeDefinition> enumerator = this.module.GetTopLevelTypeDefinitions(this.Context).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					INamespaceTypeDefinition namespaceTypeDefinition = enumerator.Current;
					this.CreateIndicesFor(namespaceTypeDefinition, queue);
				}
				goto IL_0051;
			}
			IL_0042:
			INestedTypeDefinition nestedTypeDefinition = queue.Dequeue();
			this.CreateIndicesFor(nestedTypeDefinition, queue);
			IL_0051:
			if (queue.Count <= 0)
			{
				return;
			}
			goto IL_0042;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0000F17C File Offset: 0x0000D37C
		protected virtual void OnIndicesCreated()
		{
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0000F180 File Offset: 0x0000D380
		private void CreateIndicesFor(ITypeDefinition typeDef, Queue<INestedTypeDefinition> nestedTypes)
		{
			this._cancellationToken.ThrowIfCancellationRequested();
			this.CreateIndicesForNonTypeMembers(typeDef);
			foreach (INestedTypeDefinition nestedTypeDefinition in typeDef.GetNestedTypes(this.Context))
			{
				nestedTypes.Enqueue(nestedTypeDefinition);
			}
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0000F1E8 File Offset: 0x0000D3E8
		protected IEnumerable<IGenericTypeParameter> GetConsolidatedTypeParameters(ITypeDefinition typeDef)
		{
			if (typeDef.AsNestedTypeDefinition(this.Context) != null)
			{
				return this.GetConsolidatedTypeParameters(typeDef, typeDef);
			}
			if (typeDef.IsGeneric)
			{
				return typeDef.GenericParameters;
			}
			return null;
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0000F214 File Offset: 0x0000D414
		private List<IGenericTypeParameter> GetConsolidatedTypeParameters(ITypeDefinition typeDef, ITypeDefinition owner)
		{
			List<IGenericTypeParameter> list = null;
			INestedTypeDefinition nestedTypeDefinition = typeDef.AsNestedTypeDefinition(this.Context);
			if (nestedTypeDefinition != null)
			{
				list = this.GetConsolidatedTypeParameters(nestedTypeDefinition.ContainingTypeDefinition, owner);
			}
			if (typeDef.GenericParameterCount > 0)
			{
				ushort num = 0;
				if (list == null)
				{
					list = new List<IGenericTypeParameter>();
				}
				else
				{
					num = (ushort)list.Count;
				}
				if (typeDef == owner && num == 0)
				{
					list.AddRange(typeDef.GenericParameters);
				}
				else
				{
					foreach (IGenericTypeParameter genericTypeParameter in typeDef.GenericParameters)
					{
						List<IGenericTypeParameter> list2 = list;
						ushort num2 = num;
						num = num2 + 1;
						list2.Add(new InheritedTypeParameter(num2, owner, genericTypeParameter));
					}
				}
			}
			return list;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000F2C4 File Offset: 0x0000D4C4
		protected ImmutableArray<IParameterDefinition> GetParametersToEmit(IMethodDefinition methodDef)
		{
			if (methodDef.ParameterCount == 0 && !methodDef.ReturnValueIsMarshalledExplicitly && !IteratorHelper.EnumerableIsNotEmpty<ICustomAttribute>(methodDef.GetReturnValueAttributes(this.Context)))
			{
				return ImmutableArray<IParameterDefinition>.Empty;
			}
			return this.GetParametersToEmitCore(methodDef);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0000F2F8 File Offset: 0x0000D4F8
		private ImmutableArray<IParameterDefinition> GetParametersToEmitCore(IMethodDefinition methodDef)
		{
			ArrayBuilder<IParameterDefinition> arrayBuilder = null;
			ImmutableArray<IParameterDefinition> parameters = methodDef.Parameters;
			if (methodDef.ReturnValueIsMarshalledExplicitly || IteratorHelper.EnumerableIsNotEmpty<ICustomAttribute>(methodDef.GetReturnValueAttributes(this.Context)))
			{
				arrayBuilder = ArrayBuilder<IParameterDefinition>.GetInstance(parameters.Length + 1);
				arrayBuilder.Add(new ReturnValueParameter(methodDef));
			}
			for (int i = 0; i < parameters.Length; i++)
			{
				IParameterDefinition parameterDefinition = parameters[i];
				if (parameterDefinition.Name != string.Empty || parameterDefinition.HasDefaultValue || parameterDefinition.IsOptional || parameterDefinition.IsOut || parameterDefinition.IsMarshalledExplicitly || IteratorHelper.EnumerableIsNotEmpty<ICustomAttribute>(parameterDefinition.GetAttributes(this.Context)))
				{
					if (arrayBuilder != null)
					{
						arrayBuilder.Add(parameterDefinition);
					}
				}
				else if (arrayBuilder == null)
				{
					arrayBuilder = ArrayBuilder<IParameterDefinition>.GetInstance(parameters.Length);
					arrayBuilder.AddRange(parameters, i);
				}
			}
			if (arrayBuilder == null)
			{
				return parameters;
			}
			return arrayBuilder.ToImmutableAndFree();
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000F3D4 File Offset: 0x0000D5D4
		public static IUnitReference GetDefiningUnitReference(ITypeReference typeReference, EmitContext context)
		{
			for (INestedTypeReference nestedTypeReference = typeReference.AsNestedTypeReference; nestedTypeReference != null; nestedTypeReference = typeReference.AsNestedTypeReference)
			{
				if (nestedTypeReference.AsGenericTypeInstanceReference != null)
				{
					return null;
				}
				typeReference = nestedTypeReference.GetContainingType(context);
			}
			INamespaceTypeReference asNamespaceTypeReference = typeReference.AsNamespaceTypeReference;
			if (asNamespaceTypeReference == null)
			{
				return null;
			}
			return asNamespaceTypeReference.GetUnit(context);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0000F41C File Offset: 0x0000D61C
		private void CreateInitialAssemblyRefIndex()
		{
			foreach (IAssemblyReference assemblyReference in this.module.GetAssemblyReferences(this.Context))
			{
				this.GetOrAddAssemblyReferenceHandle(assemblyReference);
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0000F478 File Offset: 0x0000D678
		private void CreateInitialFileRefIndex()
		{
			foreach (IFileReference fileReference in this.module.GetFiles(this.Context))
			{
				string fileName = fileReference.FileName;
				if (!this._fileRefIndex.ContainsKey(fileName))
				{
					this._fileRefList.Add(fileReference);
					this._fileRefIndex.Add(fileName, this._fileRefList.Count);
				}
			}
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0000F504 File Offset: 0x0000D704
		internal AssemblyReferenceHandle GetAssemblyReferenceHandle(IAssemblyReference assemblyReference)
		{
			IAssemblyReference containingAssembly = this.module.GetContainingAssembly(this.Context);
			if (containingAssembly != null && assemblyReference == containingAssembly)
			{
				return default(AssemblyReferenceHandle);
			}
			return this.GetOrAddAssemblyReferenceHandle(assemblyReference);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0000F53B File Offset: 0x0000D73B
		internal ModuleReferenceHandle GetModuleReferenceHandle(string moduleName)
		{
			return this.GetOrAddModuleReferenceHandle(moduleName);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0000F544 File Offset: 0x0000D744
		private BlobHandle GetCustomAttributeSignatureIndex(ICustomAttribute customAttribute)
		{
			BlobHandle orAddBlob;
			if (this._customAttributeSignatureIndex.TryGetValue(customAttribute, ref orAddBlob))
			{
				return orAddBlob;
			}
			PooledBlobBuilder instance = PooledBlobBuilder.GetInstance(1024);
			this.SerializeCustomAttributeSignature(customAttribute, instance);
			orAddBlob = this.metadata.GetOrAddBlob(instance);
			this._customAttributeSignatureIndex.Add(customAttribute, orAddBlob);
			instance.Free();
			return orAddBlob;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0000F598 File Offset: 0x0000D798
		private EntityHandle GetCustomAttributeTypeCodedIndex(IMethodReference methodReference)
		{
			IMethodDefinition methodDefinition = null;
			IUnitReference definingUnitReference = MetadataWriter.GetDefiningUnitReference(methodReference.GetContainingType(this.Context), this.Context);
			if (definingUnitReference != null && definingUnitReference == this.module)
			{
				methodDefinition = methodReference.GetResolvedMethod(this.Context);
			}
			if (methodDefinition == null)
			{
				return this.GetMemberReferenceHandle(methodReference);
			}
			return this.GetMethodDefinitionHandle(methodDefinition);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0000F5F4 File Offset: 0x0000D7F4
		public static EventAttributes GetEventAttributes(IEventDefinition eventDef)
		{
			EventAttributes eventAttributes = 0;
			if (eventDef.IsSpecialName)
			{
				eventAttributes |= 512;
			}
			if (eventDef.IsRuntimeSpecial)
			{
				eventAttributes |= 1024;
			}
			return eventAttributes;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0000F624 File Offset: 0x0000D824
		public static FieldAttributes GetFieldAttributes(IFieldDefinition fieldDef)
		{
			FieldAttributes fieldAttributes = fieldDef.Visibility;
			if (fieldDef.IsStatic)
			{
				fieldAttributes |= 16;
			}
			if (fieldDef.IsReadOnly)
			{
				fieldAttributes |= 32;
			}
			if (fieldDef.IsCompileTimeConstant)
			{
				fieldAttributes |= 64;
			}
			if (fieldDef.IsNotSerialized)
			{
				fieldAttributes |= 128;
			}
			if (!fieldDef.MappedData.IsDefault)
			{
				fieldAttributes |= 256;
			}
			if (fieldDef.IsSpecialName)
			{
				fieldAttributes |= 512;
			}
			if (fieldDef.IsRuntimeSpecial)
			{
				fieldAttributes |= 1024;
			}
			if (fieldDef.IsMarshalledExplicitly)
			{
				fieldAttributes |= 4096;
			}
			if (fieldDef.IsCompileTimeConstant)
			{
				fieldAttributes |= 32768;
			}
			return fieldAttributes;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000F6C8 File Offset: 0x0000D8C8
		internal BlobHandle GetFieldSignatureIndex(IFieldReference fieldReference)
		{
			ISpecializedFieldReference asSpecializedFieldReference = fieldReference.AsSpecializedFieldReference;
			if (asSpecializedFieldReference != null)
			{
				fieldReference = asSpecializedFieldReference.UnspecializedVersion;
			}
			BlobHandle orAddBlob;
			if (this._fieldSignatureIndex.TryGetValue(fieldReference, ref orAddBlob))
			{
				return orAddBlob;
			}
			PooledBlobBuilder instance = PooledBlobBuilder.GetInstance(1024);
			this.SerializeFieldSignature(fieldReference, instance);
			orAddBlob = this.metadata.GetOrAddBlob(instance);
			this._fieldSignatureIndex.Add(fieldReference, orAddBlob);
			instance.Free();
			return orAddBlob;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000F730 File Offset: 0x0000D930
		internal EntityHandle GetFieldHandle(IFieldReference fieldReference)
		{
			IFieldDefinition fieldDefinition = null;
			IUnitReference definingUnitReference = MetadataWriter.GetDefiningUnitReference(fieldReference.GetContainingType(this.Context), this.Context);
			if (definingUnitReference != null && definingUnitReference == this.module)
			{
				fieldDefinition = fieldReference.GetResolvedField(this.Context);
			}
			if (fieldDefinition == null)
			{
				return this.GetMemberReferenceHandle(fieldReference);
			}
			return this.GetFieldDefinitionHandle(fieldDefinition);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0000F78C File Offset: 0x0000D98C
		internal AssemblyFileHandle GetAssemblyFileHandle(IFileReference fileReference)
		{
			string fileName = fileReference.FileName;
			int count;
			if (!this._fileRefIndex.TryGetValue(fileName, ref count))
			{
				this._fileRefList.Add(fileReference);
				this._fileRefIndex.Add(fileName, count = this._fileRefList.Count);
			}
			return MetadataTokens.AssemblyFileHandle(count);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0000F7DB File Offset: 0x0000D9DB
		private AssemblyFileHandle GetAssemblyFileHandle(IModuleReference mref)
		{
			return MetadataTokens.AssemblyFileHandle(this._fileRefIndex[mref.Name]);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0000F7F4 File Offset: 0x0000D9F4
		private static GenericParameterAttributes GetGenericParameterAttributes(IGenericParameter genPar)
		{
			GenericParameterAttributes genericParameterAttributes = 0;
			TypeParameterVariance variance = genPar.Variance;
			if (variance != TypeParameterVariance.Covariant)
			{
				if (variance == TypeParameterVariance.Contravariant)
				{
					genericParameterAttributes |= 2;
				}
			}
			else
			{
				genericParameterAttributes |= 1;
			}
			if (genPar.MustBeReferenceType)
			{
				genericParameterAttributes |= 4;
			}
			if (genPar.MustBeValueType)
			{
				genericParameterAttributes |= 8;
			}
			if (genPar.MustHaveDefaultConstructor)
			{
				genericParameterAttributes |= 16;
			}
			return genericParameterAttributes;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0000F844 File Offset: 0x0000DA44
		private EntityHandle GetExportedTypeImplementation(INamespaceTypeReference namespaceRef)
		{
			IUnitReference unit = namespaceRef.GetUnit(this.Context);
			IAssemblyReference assemblyReference = unit as IAssemblyReference;
			if (assemblyReference != null)
			{
				return this.GetAssemblyReferenceHandle(assemblyReference);
			}
			IModuleReference moduleReference = (IModuleReference)unit;
			assemblyReference = moduleReference.GetContainingAssembly(this.Context);
			if (assemblyReference != null && assemblyReference != this.module.GetContainingAssembly(this.Context))
			{
				return this.GetAssemblyReferenceHandle(assemblyReference);
			}
			return this.GetAssemblyFileHandle(moduleReference);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0000F8B9 File Offset: 0x0000DAB9
		private static uint GetManagedResourceOffset(ManagedResource resource, BlobBuilder resourceWriter)
		{
			if (resource.ExternalFile != null)
			{
				return resource.Offset;
			}
			uint count = (uint)resourceWriter.Count;
			resource.WriteData(resourceWriter);
			return count;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0000F8D7 File Offset: 0x0000DAD7
		private static uint GetManagedResourceOffset(BlobBuilder resource, BlobBuilder resourceWriter)
		{
			uint count = (uint)resourceWriter.Count;
			resourceWriter.WriteInt32(resource.Count);
			resource.WriteContentTo(resourceWriter);
			resourceWriter.Align(8);
			return count;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0000F8FC File Offset: 0x0000DAFC
		public static string GetMangledName(INamedTypeReference namedType)
		{
			string name = namedType.Name;
			if (!namedType.MangleName)
			{
				return name;
			}
			return MetadataHelpers.ComposeAritySuffixedMetadataName(name, (int)namedType.GenericParameterCount);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0000F926 File Offset: 0x0000DB26
		internal MemberReferenceHandle GetMemberReferenceHandle(ITypeMemberReference memberRef)
		{
			return this.GetOrAddMemberReferenceHandle(memberRef);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0000F930 File Offset: 0x0000DB30
		internal EntityHandle GetMemberReferenceParent(ITypeMemberReference memberRef)
		{
			ITypeDefinition typeDefinition = memberRef.GetContainingType(this.Context).AsTypeDefinition(this.Context);
			if (typeDefinition != null)
			{
				TypeDefinitionHandle typeDefinitionHandle;
				this.TryGetTypeDefinitionHandle(typeDefinition, out typeDefinitionHandle);
				if (!typeDefinitionHandle.IsNil)
				{
					if (memberRef is IFieldReference)
					{
						return typeDefinitionHandle;
					}
					IMethodReference methodReference = memberRef as IMethodReference;
					if (methodReference != null)
					{
						MethodDefinitionHandle methodDefinitionHandle;
						if (methodReference.AcceptsExtraArguments && this.TryGetMethodDefinitionHandle(methodReference.GetResolvedMethod(this.Context), out methodDefinitionHandle))
						{
							return methodDefinitionHandle;
						}
						return typeDefinitionHandle;
					}
				}
			}
			ITypeReference containingType = memberRef.GetContainingType(this.Context);
			if (!containingType.IsTypeSpecification())
			{
				return this.GetTypeReferenceHandle(containingType);
			}
			return this.GetTypeSpecificationHandle(containingType);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0000F9E0 File Offset: 0x0000DBE0
		internal EntityHandle GetMethodDefinitionOrReferenceHandle(IMethodReference methodReference)
		{
			IMethodDefinition methodDefinition = null;
			IUnitReference definingUnitReference = MetadataWriter.GetDefiningUnitReference(methodReference.GetContainingType(this.Context), this.Context);
			if (definingUnitReference != null && definingUnitReference == this.module)
			{
				methodDefinition = methodReference.GetResolvedMethod(this.Context);
			}
			if (methodDefinition == null)
			{
				return this.GetMemberReferenceHandle(methodReference);
			}
			return this.GetMethodDefinitionHandle(methodDefinition);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0000FA3C File Offset: 0x0000DC3C
		public static MethodAttributes GetMethodAttributes(IMethodDefinition methodDef)
		{
			MethodAttributes methodAttributes = methodDef.Visibility;
			if (methodDef.IsStatic)
			{
				methodAttributes |= 16;
			}
			if (methodDef.IsSealed)
			{
				methodAttributes |= 32;
			}
			if (methodDef.IsVirtual)
			{
				methodAttributes |= 64;
			}
			if (methodDef.IsHiddenBySignature)
			{
				methodAttributes |= 128;
			}
			if (methodDef.IsNewSlot)
			{
				methodAttributes |= 256;
			}
			if (methodDef.IsAccessCheckedOnOverride)
			{
				methodAttributes |= 512;
			}
			if (methodDef.IsAbstract)
			{
				methodAttributes |= 1024;
			}
			if (methodDef.IsSpecialName)
			{
				methodAttributes |= 2048;
			}
			if (methodDef.IsRuntimeSpecial)
			{
				methodAttributes |= 4096;
			}
			if (methodDef.IsPlatformInvoke)
			{
				methodAttributes |= 8192;
			}
			if (methodDef.HasDeclarativeSecurity)
			{
				methodAttributes |= 16384;
			}
			if (methodDef.RequiresSecurityObject)
			{
				methodAttributes |= 32768;
			}
			return methodAttributes;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0000FB08 File Offset: 0x0000DD08
		internal BlobHandle GetMethodSpecificationSignatureHandle(IGenericMethodInstanceReference methodInstanceReference)
		{
			BlobHandle orAddBlob;
			if (this._methodInstanceSignatureIndex.TryGetValue(methodInstanceReference, ref orAddBlob))
			{
				return orAddBlob;
			}
			PooledBlobBuilder instance = PooledBlobBuilder.GetInstance(1024);
			GenericTypeArgumentsEncoder genericTypeArgumentsEncoder = new BlobEncoder(instance).MethodSpecificationSignature((int)methodInstanceReference.GetGenericMethod(this.Context).GenericParameterCount);
			foreach (ITypeReference typeReference in methodInstanceReference.GetGenericArguments(this.Context))
			{
				this.SerializeTypeReference(genericTypeArgumentsEncoder.AddArgument(), typeReference);
			}
			orAddBlob = this.metadata.GetOrAddBlob(instance);
			this._methodInstanceSignatureIndex.Add(methodInstanceReference, orAddBlob);
			instance.Free();
			return orAddBlob;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0000FBC8 File Offset: 0x0000DDC8
		private BlobHandle GetMarshallingDescriptorHandle(IMarshallingInformation marshallingInformation)
		{
			BlobHandle orAddBlob;
			if (this._marshallingDescriptorIndex.TryGetValue(marshallingInformation, ref orAddBlob))
			{
				return orAddBlob;
			}
			PooledBlobBuilder instance = PooledBlobBuilder.GetInstance(1024);
			this.SerializeMarshallingDescriptor(marshallingInformation, instance);
			orAddBlob = this.metadata.GetOrAddBlob(instance);
			this._marshallingDescriptorIndex.Add(marshallingInformation, orAddBlob);
			instance.Free();
			return orAddBlob;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0000FC1B File Offset: 0x0000DE1B
		private BlobHandle GetMarshallingDescriptorHandle(ImmutableArray<byte> descriptor)
		{
			return this.metadata.GetOrAddBlob(descriptor);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0000FC2C File Offset: 0x0000DE2C
		private BlobHandle GetMemberReferenceSignatureHandle(ITypeMemberReference memberRef)
		{
			IFieldReference fieldReference = memberRef as IFieldReference;
			BlobHandle blobHandle;
			if (fieldReference == null)
			{
				IMethodReference methodReference = memberRef as IMethodReference;
				if (methodReference == null)
				{
					throw ExceptionUtilities.Unreachable;
				}
				blobHandle = this.GetMethodSignatureHandle(methodReference);
			}
			else
			{
				blobHandle = this.GetFieldSignatureIndex(fieldReference);
			}
			return blobHandle;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0000FC6C File Offset: 0x0000DE6C
		internal BlobHandle GetMethodSignatureHandle(IMethodReference methodReference)
		{
			ImmutableArray<byte> immutableArray;
			return this.GetMethodSignatureHandleAndBlob(methodReference, out immutableArray);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0000FC84 File Offset: 0x0000DE84
		internal byte[] GetMethodSignature(IMethodReference methodReference)
		{
			ImmutableArray<byte> immutableArray;
			this.GetMethodSignatureHandleAndBlob(methodReference, out immutableArray);
			return immutableArray.ToArray<byte>();
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0000FCA4 File Offset: 0x0000DEA4
		private BlobHandle GetMethodSignatureHandleAndBlob(IMethodReference methodReference, out ImmutableArray<byte> signatureBlob)
		{
			ISpecializedMethodReference asSpecializedMethodReference = methodReference.AsSpecializedMethodReference;
			if (asSpecializedMethodReference != null)
			{
				methodReference = asSpecializedMethodReference.UnspecializedVersion;
			}
			KeyValuePair<BlobHandle, ImmutableArray<byte>> keyValuePair;
			if (this._signatureIndex.TryGetValue(methodReference, ref keyValuePair))
			{
				signatureBlob = keyValuePair.Value;
				return keyValuePair.Key;
			}
			PooledBlobBuilder instance = PooledBlobBuilder.GetInstance(1024);
			MethodSignatureEncoder methodSignatureEncoder = new BlobEncoder(instance).MethodSignature(new SignatureHeader((byte)methodReference.CallingConvention).CallingConvention, (int)methodReference.GenericParameterCount, (methodReference.CallingConvention & CallingConvention.HasThis) > CallingConvention.Default);
			this.SerializeReturnValueAndParameters(methodSignatureEncoder, methodReference, methodReference.ExtraParameters);
			signatureBlob = instance.ToImmutableArray();
			BlobHandle orAddBlob = this.metadata.GetOrAddBlob(signatureBlob);
			this._signatureIndex.Add(methodReference, KeyValuePairUtil.Create<BlobHandle, ImmutableArray<byte>>(orAddBlob, signatureBlob));
			instance.Free();
			return orAddBlob;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0000FD78 File Offset: 0x0000DF78
		private BlobHandle GetMethodSpecificationBlobHandle(IGenericMethodInstanceReference genericMethodInstanceReference)
		{
			PooledBlobBuilder instance = PooledBlobBuilder.GetInstance(1024);
			this.SerializeMethodSpecificationSignature(instance, genericMethodInstanceReference);
			BlobHandle orAddBlob = this.metadata.GetOrAddBlob(instance);
			instance.Free();
			return orAddBlob;
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0000FDAA File Offset: 0x0000DFAA
		private MethodSpecificationHandle GetMethodSpecificationHandle(IGenericMethodInstanceReference methodSpec)
		{
			return this.GetOrAddMethodSpecificationHandle(methodSpec);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0000FDB4 File Offset: 0x0000DFB4
		internal EntityHandle GetMethodHandle(IMethodReference methodReference)
		{
			IMethodDefinition methodDefinition = null;
			IUnitReference definingUnitReference = MetadataWriter.GetDefiningUnitReference(methodReference.GetContainingType(this.Context), this.Context);
			if (definingUnitReference != null && definingUnitReference == this.module)
			{
				methodDefinition = methodReference.GetResolvedMethod(this.Context);
			}
			MethodDefinitionHandle methodDefinitionHandle;
			if (methodDefinition != null && (methodReference == methodDefinition || !methodReference.AcceptsExtraArguments) && this.TryGetMethodDefinitionHandle(methodDefinition, out methodDefinitionHandle))
			{
				return methodDefinitionHandle;
			}
			IGenericMethodInstanceReference asGenericMethodInstanceReference = methodReference.AsGenericMethodInstanceReference;
			if (asGenericMethodInstanceReference == null)
			{
				return this.GetMemberReferenceHandle(methodReference);
			}
			return this.GetMethodSpecificationHandle(asGenericMethodInstanceReference);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0000FE38 File Offset: 0x0000E038
		internal EntityHandle GetStandaloneSignatureHandle(ISignature signature)
		{
			PooledBlobBuilder instance = PooledBlobBuilder.GetInstance(1024);
			MethodSignatureEncoder methodSignatureEncoder = new BlobEncoder(instance).MethodSignature(signature.CallingConvention.ToSignatureConvention(), 0, false);
			this.SerializeReturnValueAndParameters(methodSignatureEncoder, signature, ImmutableArray<IParameterTypeInformation>.Empty);
			BlobHandle orAddBlob = this.metadata.GetOrAddBlob(instance);
			return this.GetOrAddStandaloneSignatureHandle(orAddBlob);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0000FE94 File Offset: 0x0000E094
		public static ParameterAttributes GetParameterAttributes(IParameterDefinition parDef)
		{
			ParameterAttributes parameterAttributes = 0;
			if (parDef.IsIn)
			{
				parameterAttributes |= 1;
			}
			if (parDef.IsOut)
			{
				parameterAttributes |= 2;
			}
			if (parDef.IsOptional)
			{
				parameterAttributes |= 16;
			}
			if (parDef.HasDefaultValue)
			{
				parameterAttributes |= 4096;
			}
			if (parDef.IsMarshalledExplicitly)
			{
				parameterAttributes |= 8192;
			}
			return parameterAttributes;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0000FEEC File Offset: 0x0000E0EC
		private BlobHandle GetPermissionSetBlobHandle(ImmutableArray<ICustomAttribute> permissionSet)
		{
			PooledBlobBuilder instance = PooledBlobBuilder.GetInstance(1024);
			BlobHandle orAddBlob;
			try
			{
				instance.WriteByte(46);
				instance.WriteCompressedInteger(permissionSet.Length);
				this.SerializePermissionSet(permissionSet, instance);
				orAddBlob = this.metadata.GetOrAddBlob(instance);
			}
			finally
			{
				instance.Free();
			}
			return orAddBlob;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0000FF48 File Offset: 0x0000E148
		public static PropertyAttributes GetPropertyAttributes(IPropertyDefinition propertyDef)
		{
			PropertyAttributes propertyAttributes = 0;
			if (propertyDef.IsSpecialName)
			{
				propertyAttributes |= 512;
			}
			if (propertyDef.IsRuntimeSpecial)
			{
				propertyAttributes |= 1024;
			}
			if (propertyDef.HasDefaultValue)
			{
				propertyAttributes |= 4096;
			}
			return propertyAttributes;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0000FF88 File Offset: 0x0000E188
		private BlobHandle GetPropertySignatureHandle(IPropertyDefinition propertyDef)
		{
			KeyValuePair<BlobHandle, ImmutableArray<byte>> keyValuePair;
			if (this._signatureIndex.TryGetValue(propertyDef, ref keyValuePair))
			{
				return keyValuePair.Key;
			}
			PooledBlobBuilder instance = PooledBlobBuilder.GetInstance(1024);
			MethodSignatureEncoder methodSignatureEncoder = new BlobEncoder(instance).PropertySignature((propertyDef.CallingConvention & CallingConvention.HasThis) > CallingConvention.Default);
			this.SerializeReturnValueAndParameters(methodSignatureEncoder, propertyDef, ImmutableArray<IParameterTypeInformation>.Empty);
			ImmutableArray<byte> immutableArray = instance.ToImmutableArray();
			BlobHandle orAddBlob = this.metadata.GetOrAddBlob(immutableArray);
			this._signatureIndex.Add(propertyDef, KeyValuePairUtil.Create<BlobHandle, ImmutableArray<byte>>(orAddBlob, immutableArray));
			instance.Free();
			return orAddBlob;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00010010 File Offset: 0x0000E210
		private EntityHandle GetResolutionScopeHandle(IUnitReference unitReference)
		{
			IAssemblyReference assemblyReference = unitReference as IAssemblyReference;
			if (assemblyReference != null)
			{
				return this.GetAssemblyReferenceHandle(assemblyReference);
			}
			IModuleReference moduleReference = (IModuleReference)unitReference;
			assemblyReference = moduleReference.GetContainingAssembly(this.Context);
			if (assemblyReference != null && assemblyReference != this.module.GetContainingAssembly(this.Context))
			{
				return this.GetAssemblyReferenceHandle(assemblyReference);
			}
			return this.GetModuleReferenceHandle(moduleReference.Name);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0001007D File Offset: 0x0000E27D
		private StringHandle GetStringHandleForPathAndCheckLength(string path, INamedEntity errorEntity = null)
		{
			this.CheckPathLength(path, errorEntity);
			return this.metadata.GetOrAddString(path);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00010093 File Offset: 0x0000E293
		private StringHandle GetStringHandleForNameAndCheckLength(string name, INamedEntity errorEntity = null)
		{
			this.CheckNameLength(name, errorEntity);
			return this.metadata.GetOrAddString(name);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x000100AC File Offset: 0x0000E2AC
		private StringHandle GetStringHandleForNamespaceAndCheckLength(INamespaceTypeReference namespaceType, string mangledTypeName)
		{
			string namespaceName = namespaceType.NamespaceName;
			if (namespaceName.Length == 0)
			{
				return default(StringHandle);
			}
			this.CheckNamespaceLength(namespaceName, mangledTypeName, namespaceType);
			return this.metadata.GetOrAddString(namespaceName);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x000100E8 File Offset: 0x0000E2E8
		private void CheckNameLength(string name, INamedEntity errorEntity)
		{
			if (MetadataWriter.IsTooLongInternal(name, 1023))
			{
				Location namedEntityLocation = MetadataWriter.GetNamedEntityLocation(errorEntity);
				this.Context.Diagnostics.Add(this.messageProvider.CreateDiagnostic(this.messageProvider.ERR_MetadataNameTooLong, namedEntityLocation, new object[] { name }));
			}
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0001013C File Offset: 0x0000E33C
		private void CheckPathLength(string path, INamedEntity errorEntity = null)
		{
			if (MetadataWriter.IsTooLongInternal(path, 259))
			{
				Location namedEntityLocation = MetadataWriter.GetNamedEntityLocation(errorEntity);
				this.Context.Diagnostics.Add(this.messageProvider.CreateDiagnostic(this.messageProvider.ERR_MetadataNameTooLong, namedEntityLocation, new object[] { path }));
			}
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00010190 File Offset: 0x0000E390
		private void CheckNamespaceLength(string namespaceName, string mangledTypeName, INamespaceTypeReference errorEntity)
		{
			if (namespaceName.Length + 1 + mangledTypeName.Length > 341 && MetadataWriter.s_utf8Encoding.GetByteCount(namespaceName) + 1 + MetadataWriter.s_utf8Encoding.GetByteCount(mangledTypeName) > 1023)
			{
				Location namedEntityLocation = MetadataWriter.GetNamedEntityLocation(errorEntity);
				this.Context.Diagnostics.Add(this.messageProvider.CreateDiagnostic(this.messageProvider.ERR_MetadataNameTooLong, namedEntityLocation, new object[] { namespaceName + "." + mangledTypeName }));
			}
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00010218 File Offset: 0x0000E418
		internal bool IsUsingStringTooLong(string usingString, INamedEntity errorEntity = null)
		{
			if (MetadataWriter.IsTooLongInternal(usingString, 2046))
			{
				Location namedEntityLocation = MetadataWriter.GetNamedEntityLocation(errorEntity);
				this.Context.Diagnostics.Add(this.messageProvider.CreateDiagnostic(this.messageProvider.WRN_PdbUsingNameTooLong, namedEntityLocation, new object[] { usingString }));
				return true;
			}
			return false;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00010270 File Offset: 0x0000E470
		internal bool IsLocalNameTooLong(ILocalDefinition localDefinition)
		{
			string name = localDefinition.Name;
			if (MetadataWriter.IsTooLongInternal(name, 2046))
			{
				this.Context.Diagnostics.Add(this.messageProvider.CreateDiagnostic(this.messageProvider.WRN_PdbLocalNameTooLong, localDefinition.Location, new object[] { name }));
				return true;
			}
			return false;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x000102CA File Offset: 0x0000E4CA
		internal static bool IsTooLongInternal(string str, int maxLength)
		{
			return str.Length >= maxLength / 3 && MetadataWriter.s_utf8Encoding.GetByteCount(str) > maxLength;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x000102E8 File Offset: 0x0000E4E8
		private static Location GetNamedEntityLocation(INamedEntity errorEntity)
		{
			INamespace @namespace = errorEntity as INamespace;
			ISymbolInternal symbolInternal;
			if (@namespace != null)
			{
				symbolInternal = @namespace.GetInternalSymbol();
			}
			else
			{
				IReference reference = errorEntity as IReference;
				symbolInternal = ((reference != null) ? reference.GetInternalSymbol() : null);
			}
			return MetadataWriter.GetSymbolLocation(symbolInternal);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00010324 File Offset: 0x0000E524
		protected static Location GetSymbolLocation(ISymbolInternal symbolOpt)
		{
			if (symbolOpt == null || symbolOpt.Locations.IsDefaultOrEmpty)
			{
				return Location.None;
			}
			return symbolOpt.Locations[0];
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00010359 File Offset: 0x0000E559
		internal TypeAttributes GetTypeAttributes(ITypeDefinition typeDef)
		{
			return MetadataWriter.GetTypeAttributes(typeDef, this.Context);
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00010368 File Offset: 0x0000E568
		public static TypeAttributes GetTypeAttributes(ITypeDefinition typeDef, EmitContext context)
		{
			TypeAttributes typeAttributes = 0;
			LayoutKind layout = typeDef.Layout;
			if (layout != null)
			{
				if (layout == 2)
				{
					typeAttributes |= 16;
				}
			}
			else
			{
				typeAttributes |= 8;
			}
			if (typeDef.IsInterface)
			{
				typeAttributes |= 32;
			}
			if (typeDef.IsAbstract)
			{
				typeAttributes |= 128;
			}
			if (typeDef.IsSealed)
			{
				typeAttributes |= 256;
			}
			if (typeDef.IsSpecialName)
			{
				typeAttributes |= 1024;
			}
			if (typeDef.IsRuntimeSpecial)
			{
				typeAttributes |= 2048;
			}
			if (typeDef.IsComObject)
			{
				typeAttributes |= 4096;
			}
			if (typeDef.IsSerializable)
			{
				typeAttributes |= 8192;
			}
			if (typeDef.IsWindowsRuntimeImport)
			{
				typeAttributes |= 16384;
			}
			CharSet stringFormat = typeDef.StringFormat;
			if (stringFormat != 3)
			{
				if (stringFormat == 4)
				{
					typeAttributes |= 131072;
				}
			}
			else
			{
				typeAttributes |= 65536;
			}
			if (typeDef.HasDeclarativeSecurity)
			{
				typeAttributes |= 262144;
			}
			if (typeDef.IsBeforeFieldInit)
			{
				typeAttributes |= 1048576;
			}
			if (typeDef.AsNestedTypeDefinition(context) != null)
			{
				switch (((ITypeDefinitionMember)typeDef).Visibility)
				{
				case TypeMemberVisibility.Private:
					typeAttributes |= 3;
					break;
				case TypeMemberVisibility.FamilyAndAssembly:
					typeAttributes |= 6;
					break;
				case TypeMemberVisibility.Assembly:
					typeAttributes |= 5;
					break;
				case TypeMemberVisibility.Family:
					typeAttributes |= 4;
					break;
				case TypeMemberVisibility.FamilyOrAssembly:
					typeAttributes |= 7;
					break;
				case TypeMemberVisibility.Public:
					typeAttributes |= 2;
					break;
				}
				return typeAttributes;
			}
			INamespaceTypeDefinition namespaceTypeDefinition = typeDef.AsNamespaceTypeDefinition(context);
			if (namespaceTypeDefinition != null && namespaceTypeDefinition.IsPublic)
			{
				typeAttributes |= 1;
			}
			return typeAttributes;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x000104C8 File Offset: 0x0000E6C8
		private EntityHandle GetDeclaringTypeOrMethodHandle(IGenericParameter genPar)
		{
			IGenericTypeParameter asGenericTypeParameter = genPar.AsGenericTypeParameter;
			if (asGenericTypeParameter != null)
			{
				return this.GetTypeDefinitionHandle(asGenericTypeParameter.DefiningType);
			}
			IGenericMethodParameter asGenericMethodParameter = genPar.AsGenericMethodParameter;
			if (asGenericMethodParameter != null)
			{
				return this.GetMethodDefinitionHandle(asGenericMethodParameter.DefiningMethod);
			}
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00010514 File Offset: 0x0000E714
		private TypeReferenceHandle GetTypeReferenceHandle(ITypeReference typeReference)
		{
			TypeReferenceHandle typeReferenceHandle;
			if (this.TryGetTypeReferenceHandle(typeReference, out typeReferenceHandle))
			{
				return typeReferenceHandle;
			}
			INestedTypeReference asNestedTypeReference = typeReference.AsNestedTypeReference;
			if (asNestedTypeReference != null)
			{
				this.GetTypeReferenceHandle(asNestedTypeReference.GetContainingType(this.Context));
			}
			return this.GetOrAddTypeReferenceHandle(typeReference);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00010552 File Offset: 0x0000E752
		private TypeSpecificationHandle GetTypeSpecificationHandle(ITypeReference typeReference)
		{
			return this.GetOrAddTypeSpecificationHandle(typeReference);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0001055B File Offset: 0x0000E75B
		internal ITypeDefinition GetTypeDefinition(int token)
		{
			return this.GetTypeDef(MetadataTokens.TypeDefinitionHandle(token));
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00010569 File Offset: 0x0000E769
		internal IMethodDefinition GetMethodDefinition(int token)
		{
			return this.GetMethodDef(MetadataTokens.MethodDefinitionHandle(token));
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00010577 File Offset: 0x0000E777
		internal INestedTypeReference GetNestedTypeReference(int token)
		{
			return this.GetTypeDef(MetadataTokens.TypeDefinitionHandle(token)).AsNestedTypeReference;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0001058C File Offset: 0x0000E78C
		internal BlobHandle GetTypeSpecSignatureIndex(ITypeReference typeReference)
		{
			BlobHandle orAddBlob;
			if (this._typeSpecSignatureIndex.TryGetValue(typeReference, ref orAddBlob))
			{
				return orAddBlob;
			}
			PooledBlobBuilder instance = PooledBlobBuilder.GetInstance(1024);
			this.SerializeTypeReference(new BlobEncoder(instance).TypeSpecificationSignature(), typeReference);
			orAddBlob = this.metadata.GetOrAddBlob(instance);
			this._typeSpecSignatureIndex.Add(typeReference, orAddBlob);
			instance.Free();
			return orAddBlob;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x000105EC File Offset: 0x0000E7EC
		internal EntityHandle GetTypeHandle(ITypeReference typeReference, bool treatRefAsPotentialTypeSpec = true)
		{
			ITypeDefinition typeDefinition = typeReference.AsTypeDefinition(this.Context);
			TypeDefinitionHandle typeDefinitionHandle;
			if (typeDefinition != null && this.TryGetTypeDefinitionHandle(typeDefinition, out typeDefinitionHandle))
			{
				return typeDefinitionHandle;
			}
			if (!treatRefAsPotentialTypeSpec || !typeReference.IsTypeSpecification())
			{
				return this.GetTypeReferenceHandle(typeReference);
			}
			return this.GetTypeSpecificationHandle(typeReference);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00010640 File Offset: 0x0000E840
		internal EntityHandle GetDefinitionHandle(IDefinition definition)
		{
			ITypeDefinition typeDefinition = definition as ITypeDefinition;
			EntityHandle entityHandle;
			if (typeDefinition == null)
			{
				IMethodDefinition methodDefinition = definition as IMethodDefinition;
				if (methodDefinition == null)
				{
					IFieldDefinition fieldDefinition = definition as IFieldDefinition;
					if (fieldDefinition == null)
					{
						IEventDefinition eventDefinition = definition as IEventDefinition;
						if (eventDefinition == null)
						{
							IPropertyDefinition propertyDefinition = definition as IPropertyDefinition;
							if (propertyDefinition == null)
							{
								throw ExceptionUtilities.Unreachable;
							}
							entityHandle = this.GetPropertyDefIndex(propertyDefinition);
						}
						else
						{
							entityHandle = this.GetEventDefinitionHandle(eventDefinition);
						}
					}
					else
					{
						entityHandle = this.GetFieldDefinitionHandle(fieldDefinition);
					}
				}
				else
				{
					entityHandle = this.GetMethodDefinitionHandle(methodDefinition);
				}
			}
			else
			{
				entityHandle = this.GetTypeDefinitionHandle(typeDefinition);
			}
			return entityHandle;
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x000106DC File Offset: 0x0000E8DC
		public void WriteMetadataAndIL(PdbWriter nativePdbWriterOpt, Stream metadataStream, Stream ilStream, Stream portablePdbStreamOpt, out MetadataSizes metadataSizes)
		{
			if (nativePdbWriterOpt != null)
			{
				nativePdbWriterOpt.SetMetadataEmitter(this);
			}
			BlobBuilder blobBuilder = new BlobBuilder(1024);
			BlobBuilder blobBuilder2 = new BlobBuilder(4096);
			BlobBuilder blobBuilder3 = new BlobBuilder(0);
			BlobBuilder blobBuilder4 = new BlobBuilder(0);
			blobBuilder.WriteUInt32(0U);
			Blob blob;
			Blob blob2;
			this.BuildMetadataAndIL(nativePdbWriterOpt, blobBuilder, blobBuilder3, blobBuilder4, out blob, out blob2);
			ImmutableArray<int> rowCounts = this.metadata.GetRowCounts();
			this.PopulateEncTables(rowCounts);
			MetadataRootBuilder metadataRootBuilder = new MetadataRootBuilder(this.metadata, this.module.SerializationProperties.TargetRuntimeVersion, true);
			metadataRootBuilder.Serialize(blobBuilder2, 0, 0);
			metadataSizes = metadataRootBuilder.Sizes;
			try
			{
				blobBuilder.WriteContentTo(ilStream);
				blobBuilder2.WriteContentTo(metadataStream);
			}
			catch (Exception ex) when (!(ex is OperationCanceledException))
			{
				throw new PeWritingException(ex);
			}
			if (portablePdbStreamOpt != null)
			{
				PortablePdbBuilder portablePdbBuilder = this.GetPortablePdbBuilder(rowCounts, default(MethodDefinitionHandle), null);
				BlobBuilder blobBuilder5 = new BlobBuilder(256);
				portablePdbBuilder.Serialize(blobBuilder5);
				try
				{
					blobBuilder5.WriteContentTo(portablePdbStreamOpt);
				}
				catch (Exception ex2) when (!(ex2 is OperationCanceledException))
				{
					throw new SymUnmanagedWriterException(ex2.Message, ex2);
				}
			}
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00010830 File Offset: 0x0000EA30
		public void BuildMetadataAndIL(PdbWriter nativePdbWriterOpt, BlobBuilder ilBuilder, BlobBuilder mappedFieldDataBuilder, BlobBuilder managedResourceDataBuilder, out Blob mvidFixup, out Blob mvidStringFixup)
		{
			this.CreateIndices();
			if (this._debugMetadataOpt != null)
			{
				DebugDocumentsBuilder debugDocumentsBuilder = this.Module.DebugDocumentsBuilder;
				foreach (SyntaxTree syntaxTree in this.Module.CommonCompilation.SyntaxTrees)
				{
					DebugSourceDocument debugSourceDocument = debugDocumentsBuilder.TryGetDebugDocument(syntaxTree.FilePath, null);
					if (debugSourceDocument != null && !this._documentIndex.ContainsKey(debugSourceDocument))
					{
						this.AddDocument(debugSourceDocument, this._documentIndex);
					}
				}
				RebuildData rebuildData = this.Context.RebuildData;
				if (rebuildData != null)
				{
					this._usingNonSourceDocumentNameEnumerator = true;
					this._nonSourceDocumentNameEnumerator = rebuildData.NonSourceFileDocumentNames.GetEnumerator();
				}
				this.DefineModuleImportScope();
				if (this.module.SourceLinkStreamOpt != null)
				{
					this.EmbedSourceLink(this.module.SourceLinkStreamOpt);
				}
				this.EmbedCompilationOptions(this.module);
				this.EmbedMetadataReferenceInformation(this.module);
			}
			int[] array;
			if (this.MetadataOnly)
			{
				array = this.SerializeThrowNullMethodBodies(ilBuilder);
				mvidStringFixup = default(Blob);
			}
			else
			{
				array = this.SerializeMethodBodies(ilBuilder, nativePdbWriterOpt, out mvidStringFixup);
			}
			this._cancellationToken.ThrowIfCancellationRequested();
			this._tableIndicesAreComplete = true;
			this.ReportReferencesToAddedSymbols();
			BlobBuilder blobBuilder = null;
			if (this._dynamicAnalysisDataWriterOpt != null)
			{
				blobBuilder = new BlobBuilder(256);
				this._dynamicAnalysisDataWriterOpt.SerializeMetadataTables(blobBuilder);
			}
			this.PopulateTypeSystemTables(array, mappedFieldDataBuilder, managedResourceDataBuilder, blobBuilder, out mvidFixup);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x000109AC File Offset: 0x0000EBAC
		public void PopulateEncTables(ImmutableArray<int> typeSystemRowCounts)
		{
			this.PopulateEncLogTableRows(typeSystemRowCounts);
			this.PopulateEncMapTableRows(typeSystemRowCounts);
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x000109BC File Offset: 0x0000EBBC
		public MetadataRootBuilder GetRootBuilder()
		{
			return new MetadataRootBuilder(this.metadata, this.module.SerializationProperties.TargetRuntimeVersion, true);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x000109DA File Offset: 0x0000EBDA
		public PortablePdbBuilder GetPortablePdbBuilder(ImmutableArray<int> typeSystemRowCounts, MethodDefinitionHandle debugEntryPoint, Func<IEnumerable<Blob>, BlobContentId> deterministicIdProviderOpt)
		{
			return new PortablePdbBuilder(this._debugMetadataOpt, typeSystemRowCounts, debugEntryPoint, deterministicIdProviderOpt);
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x000109EC File Offset: 0x0000EBEC
		internal void GetEntryPoints(out MethodDefinitionHandle entryPointHandle, out MethodDefinitionHandle debugEntryPointHandle)
		{
			if (!this.IsFullMetadata || this.MetadataOnly)
			{
				entryPointHandle = (debugEntryPointHandle = default(MethodDefinitionHandle));
				return;
			}
			IMethodReference peentryPoint = this.module.PEEntryPoint;
			entryPointHandle = ((peentryPoint != null) ? ((MethodDefinitionHandle)this.GetMethodHandle((IMethodDefinition)peentryPoint.AsDefinition(this.Context))) : default(MethodDefinitionHandle));
			IMethodReference debugEntryPoint = this.module.DebugEntryPoint;
			if (debugEntryPoint != null && debugEntryPoint != peentryPoint)
			{
				debugEntryPointHandle = (MethodDefinitionHandle)this.GetMethodHandle((IMethodDefinition)debugEntryPoint.AsDefinition(this.Context));
				return;
			}
			debugEntryPointHandle = entryPointHandle;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00010AA3 File Offset: 0x0000ECA3
		private ImmutableArray<IGenericParameter> GetSortedGenericParameters()
		{
			return this.GetGenericParameters().OrderBy<IGenericParameter>(delegate(IGenericParameter x, IGenericParameter y)
			{
				int num = CodedIndex.TypeOrMethodDef(this.GetDeclaringTypeOrMethodHandle(x)) - CodedIndex.TypeOrMethodDef(this.GetDeclaringTypeOrMethodHandle(y));
				if (num != 0)
				{
					return num;
				}
				return (int)(x.Index - y.Index);
			}).ToImmutableArray<IGenericParameter>();
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00010AC4 File Offset: 0x0000ECC4
		private void PopulateTypeSystemTables(int[] methodBodyOffsets, BlobBuilder mappedFieldDataWriter, BlobBuilder resourceWriter, BlobBuilder dynamicAnalysisDataOpt, out Blob mvidFixup)
		{
			ImmutableArray<IGenericParameter> sortedGenericParameters = this.GetSortedGenericParameters();
			this.PopulateAssemblyRefTableRows();
			this.PopulateAssemblyTableRows();
			this.PopulateClassLayoutTableRows();
			this.PopulateConstantTableRows();
			this.PopulateDeclSecurityTableRows();
			this.PopulateEventMapTableRows();
			this.PopulateEventTableRows();
			this.PopulateExportedTypeTableRows();
			this.PopulateFieldLayoutTableRows();
			this.PopulateFieldMarshalTableRows();
			this.PopulateFieldRvaTableRows(mappedFieldDataWriter);
			this.PopulateFieldTableRows();
			this.PopulateFileTableRows();
			this.PopulateGenericParameters(sortedGenericParameters);
			this.PopulateImplMapTableRows();
			this.PopulateInterfaceImplTableRows();
			this.PopulateManifestResourceTableRows(resourceWriter, dynamicAnalysisDataOpt);
			this.PopulateMemberRefTableRows();
			this.PopulateMethodImplTableRows();
			this.PopulateMethodTableRows(methodBodyOffsets);
			this.PopulateMethodSemanticsTableRows();
			this.PopulateMethodSpecTableRows();
			this.PopulateModuleRefTableRows();
			this.PopulateModuleTableRow(out mvidFixup);
			this.PopulateNestedClassTableRows();
			this.PopulateParamTableRows();
			this.PopulatePropertyMapTableRows();
			this.PopulatePropertyTableRows();
			this.PopulateTypeDefTableRows();
			this.PopulateTypeRefTableRows();
			this.PopulateTypeSpecTableRows();
			this.PopulateStandaloneSignatures();
			this.PopulateCustomAttributeTableRows(sortedGenericParameters);
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00010BA8 File Offset: 0x0000EDA8
		private void PopulateAssemblyRefTableRows()
		{
			IReadOnlyList<AssemblyIdentity> assemblyRefs = this.GetAssemblyRefs();
			this.metadata.SetCapacity(TableIndex.AssemblyRef, assemblyRefs.Count);
			foreach (AssemblyIdentity assemblyIdentity in assemblyRefs)
			{
				this.metadata.AddAssemblyReference(this.GetStringHandleForPathAndCheckLength(assemblyIdentity.Name, null), assemblyIdentity.Version, this.metadata.GetOrAddString(assemblyIdentity.CultureName), this.metadata.GetOrAddBlob(assemblyIdentity.PublicKeyToken), (assemblyIdentity.ContentType << 9) | (assemblyIdentity.IsRetargetable ? 256 : 0), default(BlobHandle));
			}
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00010C68 File Offset: 0x0000EE68
		private void PopulateAssemblyTableRows()
		{
			if (!this.EmitAssemblyDefinition)
			{
				return;
			}
			ISourceAssemblySymbolInternal sourceAssemblyOpt = this.module.SourceAssemblyOpt;
			AssemblyFlags assemblyFlags = sourceAssemblyOpt.AssemblyFlags & ~AssemblyFlags.PublicKey;
			if (!sourceAssemblyOpt.Identity.PublicKey.IsDefaultOrEmpty)
			{
				assemblyFlags |= AssemblyFlags.PublicKey;
			}
			MetadataBuilder metadataBuilder = this.metadata;
			AssemblyFlags assemblyFlags2 = assemblyFlags;
			AssemblyHashAlgorithm hashAlgorithm = sourceAssemblyOpt.HashAlgorithm;
			Version version = sourceAssemblyOpt.Identity.Version;
			BlobHandle orAddBlob = this.metadata.GetOrAddBlob(sourceAssemblyOpt.Identity.PublicKey);
			metadataBuilder.AddAssembly(this.GetStringHandleForPathAndCheckLength(this.module.Name, this.module), version, this.metadata.GetOrAddString(sourceAssemblyOpt.Identity.CultureName), orAddBlob, assemblyFlags2, hashAlgorithm);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00010D1C File Offset: 0x0000EF1C
		private void PopulateCustomAttributeTableRows(ImmutableArray<IGenericParameter> sortedGenericParameters)
		{
			if (this.IsFullMetadata)
			{
				this.AddAssemblyAttributesToTable();
			}
			this.AddCustomAttributesToTable<IMethodDefinition>(this.GetMethodDefs(), (IMethodDefinition def) => this.GetMethodDefinitionHandle(def));
			this.AddCustomAttributesToTable<IFieldDefinition>(this.GetFieldDefs(), (IFieldDefinition def) => this.GetFieldDefinitionHandle(def));
			IReadOnlyList<ITypeDefinition> typeDefs = this.GetTypeDefs();
			this.AddCustomAttributesToTable<ITypeDefinition>(typeDefs, (ITypeDefinition def) => this.GetTypeDefinitionHandle(def));
			this.AddCustomAttributesToTable<IParameterDefinition>(this.GetParameterDefs(), (IParameterDefinition def) => this.GetParameterHandle(def));
			if (this.IsFullMetadata)
			{
				this.AddModuleAttributesToTable(this.module);
			}
			this.AddCustomAttributesToTable<IPropertyDefinition>(this.GetPropertyDefs(), (IPropertyDefinition def) => this.GetPropertyDefIndex(def));
			this.AddCustomAttributesToTable<IEventDefinition>(this.GetEventDefs(), (IEventDefinition def) => this.GetEventDefinitionHandle(def));
			this.AddCustomAttributesToTable<IGenericParameter>(sortedGenericParameters, TableIndex.GenericParam);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00010DEC File Offset: 0x0000EFEC
		private void AddAssemblyAttributesToTable()
		{
			bool flag = this.module.OutputKind == OutputKind.NetModule;
			if (flag)
			{
				this.AddAssemblyAttributesToTable(Enumerable.Select<SecurityAttribute, ICustomAttribute>(this.module.GetSourceAssemblySecurityAttributes(), (SecurityAttribute sa) => sa.Attribute), true, true);
			}
			this.AddAssemblyAttributesToTable(this.module.GetSourceAssemblyAttributes(this.Context.IsRefAssembly), flag, false);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00010E60 File Offset: 0x0000F060
		private void AddAssemblyAttributesToTable(IEnumerable<ICustomAttribute> assemblyAttributes, bool needsDummyParent, bool isSecurity)
		{
			EntityHandle entityHandle = Handle.AssemblyDefinition;
			foreach (ICustomAttribute customAttribute in assemblyAttributes)
			{
				if (needsDummyParent)
				{
					entityHandle = this.GetDummyAssemblyAttributeParent(isSecurity, customAttribute.AllowMultiple);
				}
				this.AddCustomAttributeToTable(entityHandle, customAttribute);
			}
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00010ECC File Offset: 0x0000F0CC
		private TypeReferenceHandle GetDummyAssemblyAttributeParent(bool isSecurity, bool allowMultiple)
		{
			int num = (isSecurity ? 1 : 0);
			int num2 = (allowMultiple ? 1 : 0);
			if (this._dummyAssemblyAttributeParent[num, num2].IsNil)
			{
				this._dummyAssemblyAttributeParent[num, num2] = this.metadata.AddTypeReference(this.GetResolutionScopeHandle(this.module.GetCorLibrary(this.Context)), this.metadata.GetOrAddString("System.Runtime.CompilerServices"), this.metadata.GetOrAddString("AssemblyAttributesGoHere" + MetadataWriter.dummyAssemblyAttributeParentQualifier[num, num2]));
			}
			return this._dummyAssemblyAttributeParent[num, num2];
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00010F6C File Offset: 0x0000F16C
		private void AddModuleAttributesToTable(CommonPEModuleBuilder module)
		{
			foreach (ICustomAttribute customAttribute in module.GetSourceModuleAttributes())
			{
				this.AddCustomAttributeToTable(EntityHandle.ModuleDefinition, customAttribute);
			}
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00010FC4 File Offset: 0x0000F1C4
		private void AddCustomAttributesToTable<T>(IEnumerable<T> parentList, TableIndex tableIndex) where T : IReference
		{
			int num = 1;
			foreach (T t in parentList)
			{
				EntityHandle entityHandle = MetadataTokens.Handle(tableIndex, num++);
				foreach (ICustomAttribute customAttribute in t.GetAttributes(this.Context))
				{
					this.AddCustomAttributeToTable(entityHandle, customAttribute);
				}
			}
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00011064 File Offset: 0x0000F264
		private void AddCustomAttributesToTable<T>(IEnumerable<T> parentList, Func<T, EntityHandle> getDefinitionHandle) where T : IReference
		{
			foreach (T t in parentList)
			{
				EntityHandle entityHandle = getDefinitionHandle.Invoke(t);
				foreach (ICustomAttribute customAttribute in t.GetAttributes(this.Context))
				{
					this.AddCustomAttributeToTable(entityHandle, customAttribute);
				}
			}
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x000110FC File Offset: 0x0000F2FC
		private void AddCustomAttributesToTable(EntityHandle handle, ImmutableArray<ICustomAttribute> attributes)
		{
			foreach (ICustomAttribute customAttribute in attributes)
			{
				this.AddCustomAttributeToTable(handle, customAttribute);
			}
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0001112C File Offset: 0x0000F32C
		private void AddCustomAttributesToTable(IEnumerable<TypeReferenceWithAttributes> typeRefsWithAttributes)
		{
			foreach (TypeReferenceWithAttributes typeReferenceWithAttributes in typeRefsWithAttributes)
			{
				EntityHandle typeHandle = this.GetTypeHandle(typeReferenceWithAttributes.TypeRef, true);
				foreach (ICustomAttribute customAttribute in typeReferenceWithAttributes.Attributes)
				{
					this.AddCustomAttributeToTable(typeHandle, customAttribute);
				}
			}
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x000111AC File Offset: 0x0000F3AC
		private void AddCustomAttributeToTable(EntityHandle parentHandle, ICustomAttribute customAttribute)
		{
			IMethodReference methodReference = customAttribute.Constructor(this.Context, true);
			if (methodReference != null)
			{
				this.metadata.AddCustomAttribute(parentHandle, this.GetCustomAttributeTypeCodedIndex(methodReference), this.GetCustomAttributeSignatureIndex(customAttribute));
			}
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x000111E8 File Offset: 0x0000F3E8
		private void PopulateDeclSecurityTableRows()
		{
			if (this.module.OutputKind != OutputKind.NetModule)
			{
				this.PopulateDeclSecurityTableRowsFor(EntityHandle.AssemblyDefinition, this.module.GetSourceAssemblySecurityAttributes());
			}
			foreach (ITypeDefinition typeDefinition in this.GetTypeDefs())
			{
				if (typeDefinition.HasDeclarativeSecurity)
				{
					this.PopulateDeclSecurityTableRowsFor(this.GetTypeDefinitionHandle(typeDefinition), typeDefinition.SecurityAttributes);
				}
			}
			foreach (IMethodDefinition methodDefinition in this.GetMethodDefs())
			{
				if (methodDefinition.HasDeclarativeSecurity)
				{
					this.PopulateDeclSecurityTableRowsFor(this.GetMethodDefinitionHandle(methodDefinition), methodDefinition.SecurityAttributes);
				}
			}
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x000112CC File Offset: 0x0000F4CC
		private void PopulateDeclSecurityTableRowsFor(EntityHandle parentHandle, IEnumerable<SecurityAttribute> attributes)
		{
			OrderPreservingMultiDictionary<DeclarativeSecurityAction, ICustomAttribute> orderPreservingMultiDictionary = null;
			foreach (SecurityAttribute securityAttribute in attributes)
			{
				orderPreservingMultiDictionary = orderPreservingMultiDictionary ?? OrderPreservingMultiDictionary<DeclarativeSecurityAction, ICustomAttribute>.GetInstance();
				orderPreservingMultiDictionary.Add(securityAttribute.Action, securityAttribute.Attribute);
			}
			if (orderPreservingMultiDictionary == null)
			{
				return;
			}
			foreach (DeclarativeSecurityAction declarativeSecurityAction in orderPreservingMultiDictionary.Keys)
			{
				this.metadata.AddDeclarativeSecurityAttribute(parentHandle, declarativeSecurityAction, this.GetPermissionSetBlobHandle(orderPreservingMultiDictionary[declarativeSecurityAction]));
			}
			orderPreservingMultiDictionary.Free();
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00011394 File Offset: 0x0000F594
		private void PopulateEventTableRows()
		{
			IReadOnlyList<IEventDefinition> eventDefs = this.GetEventDefs();
			this.metadata.SetCapacity(TableIndex.Event, eventDefs.Count);
			foreach (IEventDefinition eventDefinition in eventDefs)
			{
				this.metadata.AddEvent(MetadataWriter.GetEventAttributes(eventDefinition), this.GetStringHandleForNameAndCheckLength(eventDefinition.Name, eventDefinition), this.GetTypeHandle(eventDefinition.GetType(this.Context), true));
			}
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00011424 File Offset: 0x0000F624
		private void PopulateExportedTypeTableRows()
		{
			if (!this.IsFullMetadata)
			{
				return;
			}
			ImmutableArray<ExportedType> exportedTypes = this.module.GetExportedTypes(this.Context.Diagnostics);
			if (exportedTypes.Length == 0)
			{
				return;
			}
			this.metadata.SetCapacity(TableIndex.ExportedType, exportedTypes.Length);
			foreach (ExportedType exportedType in exportedTypes)
			{
				INamespaceTypeReference asNamespaceTypeReference;
				StringHandle stringHandle;
				StringHandle stringHandle2;
				EntityHandle entityHandle;
				TypeAttributes typeAttributes;
				if ((asNamespaceTypeReference = exportedType.Type.AsNamespaceTypeReference) != null)
				{
					string mangledName = MetadataWriter.GetMangledName(asNamespaceTypeReference);
					stringHandle = this.GetStringHandleForNameAndCheckLength(mangledName, asNamespaceTypeReference);
					stringHandle2 = this.GetStringHandleForNamespaceAndCheckLength(asNamespaceTypeReference, mangledName);
					entityHandle = this.GetExportedTypeImplementation(asNamespaceTypeReference);
					typeAttributes = (exportedType.IsForwarder ? 2097152 : 1);
				}
				else
				{
					INestedTypeReference asNestedTypeReference;
					if ((asNestedTypeReference = exportedType.Type.AsNestedTypeReference) == null)
					{
						throw ExceptionUtilities.UnexpectedValue(exportedType);
					}
					stringHandle = this.GetStringHandleForNameAndCheckLength(MetadataWriter.GetMangledName(asNestedTypeReference), asNestedTypeReference);
					stringHandle2 = default(StringHandle);
					entityHandle = MetadataTokens.ExportedTypeHandle(exportedType.ParentIndex + 1);
					typeAttributes = (exportedType.IsForwarder ? 0 : 2);
				}
				this.metadata.AddExportedType(typeAttributes, stringHandle2, stringHandle, entityHandle, exportedType.IsForwarder ? 0 : MetadataTokens.GetToken(exportedType.Type.TypeDef));
			}
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0001156C File Offset: 0x0000F76C
		private void PopulateFieldLayoutTableRows()
		{
			foreach (IFieldDefinition fieldDefinition in this.GetFieldDefs())
			{
				if (fieldDefinition.ContainingTypeDefinition.Layout == 2 && !fieldDefinition.IsStatic)
				{
					this.metadata.AddFieldLayout(this.GetFieldDefinitionHandle(fieldDefinition), fieldDefinition.Offset);
				}
			}
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x000115E0 File Offset: 0x0000F7E0
		private void PopulateFieldMarshalTableRows()
		{
			foreach (IFieldDefinition fieldDefinition in this.GetFieldDefs())
			{
				if (fieldDefinition.IsMarshalledExplicitly)
				{
					IMarshallingInformation marshallingInformation = fieldDefinition.MarshallingInformation;
					BlobHandle blobHandle = ((marshallingInformation != null) ? this.GetMarshallingDescriptorHandle(marshallingInformation) : this.GetMarshallingDescriptorHandle(fieldDefinition.MarshallingDescriptor));
					this.metadata.AddMarshallingDescriptor(this.GetFieldDefinitionHandle(fieldDefinition), blobHandle);
				}
			}
			foreach (IParameterDefinition parameterDefinition in this.GetParameterDefs())
			{
				if (parameterDefinition.IsMarshalledExplicitly)
				{
					IMarshallingInformation marshallingInformation2 = parameterDefinition.MarshallingInformation;
					BlobHandle blobHandle2 = ((marshallingInformation2 != null) ? this.GetMarshallingDescriptorHandle(marshallingInformation2) : this.GetMarshallingDescriptorHandle(parameterDefinition.MarshallingDescriptor));
					this.metadata.AddMarshallingDescriptor(this.GetParameterHandle(parameterDefinition), blobHandle2);
				}
			}
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x000116EC File Offset: 0x0000F8EC
		private void PopulateFieldRvaTableRows(BlobBuilder mappedFieldDataWriter)
		{
			foreach (IFieldDefinition fieldDefinition in this.GetFieldDefs())
			{
				if (!fieldDefinition.MappedData.IsDefault)
				{
					int count = mappedFieldDataWriter.Count;
					mappedFieldDataWriter.WriteBytes(fieldDefinition.MappedData);
					mappedFieldDataWriter.Align(8);
					this.metadata.AddFieldRelativeVirtualAddress(this.GetFieldDefinitionHandle(fieldDefinition), count);
				}
			}
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00011770 File Offset: 0x0000F970
		private void PopulateFieldTableRows()
		{
			IReadOnlyList<IFieldDefinition> fieldDefs = this.GetFieldDefs();
			this.metadata.SetCapacity(TableIndex.Field, fieldDefs.Count);
			foreach (IFieldDefinition fieldDefinition in fieldDefs)
			{
				if (fieldDefinition.IsContextualNamedEntity)
				{
					((IContextualNamedEntity)fieldDefinition).AssociateWithMetadataWriter(this);
				}
				this.metadata.AddFieldDefinition(MetadataWriter.GetFieldAttributes(fieldDefinition), this.GetStringHandleForNameAndCheckLength(fieldDefinition.Name, fieldDefinition), this.GetFieldSignatureIndex(fieldDefinition));
			}
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00011804 File Offset: 0x0000FA04
		private void PopulateConstantTableRows()
		{
			foreach (IFieldDefinition fieldDefinition in this.GetFieldDefs())
			{
				MetadataConstant compileTimeValue = fieldDefinition.GetCompileTimeValue(this.Context);
				if (compileTimeValue != null)
				{
					this.metadata.AddConstant(this.GetFieldDefinitionHandle(fieldDefinition), compileTimeValue.Value);
				}
			}
			foreach (IParameterDefinition parameterDefinition in this.GetParameterDefs())
			{
				MetadataConstant defaultValue = parameterDefinition.GetDefaultValue(this.Context);
				if (defaultValue != null)
				{
					this.metadata.AddConstant(this.GetParameterHandle(parameterDefinition), defaultValue.Value);
				}
			}
			foreach (IPropertyDefinition propertyDefinition in this.GetPropertyDefs())
			{
				if (propertyDefinition.HasDefaultValue)
				{
					this.metadata.AddConstant(this.GetPropertyDefIndex(propertyDefinition), propertyDefinition.DefaultValue.Value);
				}
			}
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0001194C File Offset: 0x0000FB4C
		private void PopulateFileTableRows()
		{
			ISourceAssemblySymbolInternal sourceAssemblyOpt = this.module.SourceAssemblyOpt;
			if (sourceAssemblyOpt == null)
			{
				return;
			}
			AssemblyHashAlgorithm hashAlgorithm = sourceAssemblyOpt.HashAlgorithm;
			this.metadata.SetCapacity(TableIndex.File, this._fileRefList.Count);
			foreach (IFileReference fileReference in this._fileRefList)
			{
				this.metadata.AddAssemblyFile(this.GetStringHandleForPathAndCheckLength(fileReference.FileName, null), this.metadata.GetOrAddBlob(fileReference.GetHashValue(hashAlgorithm)), fileReference.HasMetadata);
			}
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x000119F8 File Offset: 0x0000FBF8
		private void PopulateGenericParameters(ImmutableArray<IGenericParameter> sortedGenericParameters)
		{
			foreach (IGenericParameter genericParameter in sortedGenericParameters)
			{
				GenericParameterHandle genericParameterHandle = this.metadata.AddGenericParameter(this.GetDeclaringTypeOrMethodHandle(genericParameter), MetadataWriter.GetGenericParameterAttributes(genericParameter), this.GetStringHandleForNameAndCheckLength(genericParameter.Name, genericParameter), (int)genericParameter.Index);
				foreach (TypeReferenceWithAttributes typeReferenceWithAttributes in genericParameter.GetConstraints(this.Context))
				{
					GenericParameterConstraintHandle genericParameterConstraintHandle = this.metadata.AddGenericParameterConstraint(genericParameterHandle, this.GetTypeHandle(typeReferenceWithAttributes.TypeRef, true));
					this.AddCustomAttributesToTable(genericParameterConstraintHandle, typeReferenceWithAttributes.Attributes);
				}
			}
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00011AC4 File Offset: 0x0000FCC4
		private void PopulateImplMapTableRows()
		{
			foreach (IMethodDefinition methodDefinition in this.GetMethodDefs())
			{
				if (methodDefinition.IsPlatformInvoke)
				{
					IPlatformInvokeInformation platformInvokeData = methodDefinition.PlatformInvokeData;
					string entryPointName = platformInvokeData.EntryPointName;
					StringHandle stringHandle = ((entryPointName != null && entryPointName != methodDefinition.Name) ? this.GetStringHandleForNameAndCheckLength(entryPointName, methodDefinition) : this.metadata.GetOrAddString(methodDefinition.Name));
					this.metadata.AddMethodImport(this.GetMethodDefinitionHandle(methodDefinition), platformInvokeData.Flags, stringHandle, this.GetModuleReferenceHandle(platformInvokeData.ModuleName));
				}
			}
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00011B74 File Offset: 0x0000FD74
		private void PopulateInterfaceImplTableRows()
		{
			foreach (ITypeDefinition typeDefinition in this.GetTypeDefs())
			{
				TypeDefinitionHandle typeDefinitionHandle = this.GetTypeDefinitionHandle(typeDefinition);
				foreach (TypeReferenceWithAttributes typeReferenceWithAttributes in typeDefinition.Interfaces(this.Context))
				{
					InterfaceImplementationHandle interfaceImplementationHandle = this.metadata.AddInterfaceImplementation(typeDefinitionHandle, this.GetTypeHandle(typeReferenceWithAttributes.TypeRef, true));
					this.AddCustomAttributesToTable(interfaceImplementationHandle, typeReferenceWithAttributes.Attributes);
				}
			}
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00011C30 File Offset: 0x0000FE30
		private void PopulateManifestResourceTableRows(BlobBuilder resourceDataWriter, BlobBuilder dynamicAnalysisDataOpt)
		{
			if (dynamicAnalysisDataOpt != null)
			{
				this.metadata.AddManifestResource(ManifestResourceAttributes.Private, this.metadata.GetOrAddString("<DynamicAnalysisData>"), default(EntityHandle), MetadataWriter.GetManagedResourceOffset(dynamicAnalysisDataOpt, resourceDataWriter));
			}
			foreach (ManagedResource managedResource in this.module.GetResources(this.Context))
			{
				EntityHandle entityHandle;
				if (managedResource.ExternalFile != null)
				{
					entityHandle = this.GetAssemblyFileHandle(managedResource.ExternalFile);
				}
				else
				{
					entityHandle = default(EntityHandle);
				}
				this.metadata.AddManifestResource(managedResource.IsPublic ? ManifestResourceAttributes.Public : ManifestResourceAttributes.Private, this.GetStringHandleForNameAndCheckLength(managedResource.Name, null), entityHandle, MetadataWriter.GetManagedResourceOffset(managedResource, resourceDataWriter));
			}
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00011CF0 File Offset: 0x0000FEF0
		private void PopulateMemberRefTableRows()
		{
			IReadOnlyList<ITypeMemberReference> memberRefs = this.GetMemberRefs();
			this.metadata.SetCapacity(TableIndex.MemberRef, memberRefs.Count);
			foreach (ITypeMemberReference typeMemberReference in memberRefs)
			{
				this.metadata.AddMemberReference(this.GetMemberReferenceParent(typeMemberReference), this.GetStringHandleForNameAndCheckLength(typeMemberReference.Name, typeMemberReference), this.GetMemberReferenceSignatureHandle(typeMemberReference));
			}
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00011D74 File Offset: 0x0000FF74
		private void PopulateMethodImplTableRows()
		{
			this.metadata.SetCapacity(TableIndex.MethodImpl, this.methodImplList.Count);
			foreach (MethodImplementation methodImplementation in this.methodImplList)
			{
				this.metadata.AddMethodImplementation(this.GetTypeDefinitionHandle(methodImplementation.ContainingType), this.GetMethodDefinitionOrReferenceHandle(methodImplementation.ImplementingMethod), this.GetMethodDefinitionOrReferenceHandle(methodImplementation.ImplementedMethod));
			}
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00011E0C File Offset: 0x0001000C
		private void PopulateMethodSpecTableRows()
		{
			IReadOnlyList<IGenericMethodInstanceReference> methodSpecs = this.GetMethodSpecs();
			this.metadata.SetCapacity(TableIndex.MethodSpec, methodSpecs.Count);
			foreach (IGenericMethodInstanceReference genericMethodInstanceReference in methodSpecs)
			{
				this.metadata.AddMethodSpecification(this.GetMethodDefinitionOrReferenceHandle(genericMethodInstanceReference.GetGenericMethod(this.Context)), this.GetMethodSpecificationBlobHandle(genericMethodInstanceReference));
			}
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00011E8C File Offset: 0x0001008C
		private void PopulateMethodTableRows(int[] methodBodyOffsets)
		{
			IReadOnlyList<IMethodDefinition> methodDefs = this.GetMethodDefs();
			this.metadata.SetCapacity(TableIndex.MethodDef, methodDefs.Count);
			int num = 0;
			foreach (IMethodDefinition methodDefinition in methodDefs)
			{
				this.metadata.AddMethodDefinition(MetadataWriter.GetMethodAttributes(methodDefinition), methodDefinition.GetImplementationAttributes(this.Context), this.GetStringHandleForNameAndCheckLength(methodDefinition.Name, methodDefinition), this.GetMethodSignatureHandle(methodDefinition), methodBodyOffsets[num], this.GetFirstParameterHandle(methodDefinition));
				num++;
			}
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00011F28 File Offset: 0x00010128
		private void PopulateMethodSemanticsTableRows()
		{
			IReadOnlyList<IPropertyDefinition> propertyDefs = this.GetPropertyDefs();
			IReadOnlyList<IEventDefinition> eventDefs = this.GetEventDefs();
			this.metadata.SetCapacity(TableIndex.MethodSemantics, propertyDefs.Count * 2 + eventDefs.Count * 2);
			foreach (IPropertyDefinition propertyDefinition in this.GetPropertyDefs())
			{
				PropertyDefinitionHandle propertyDefIndex = this.GetPropertyDefIndex(propertyDefinition);
				foreach (IMethodReference methodReference in propertyDefinition.GetAccessors(this.Context))
				{
					MethodSemanticsAttributes methodSemanticsAttributes;
					if (methodReference == propertyDefinition.Setter)
					{
						methodSemanticsAttributes = MethodSemanticsAttributes.Setter;
					}
					else if (methodReference == propertyDefinition.Getter)
					{
						methodSemanticsAttributes = MethodSemanticsAttributes.Getter;
					}
					else
					{
						methodSemanticsAttributes = MethodSemanticsAttributes.Other;
					}
					this.metadata.AddMethodSemantics(propertyDefIndex, methodSemanticsAttributes, this.GetMethodDefinitionHandle(methodReference.GetResolvedMethod(this.Context)));
				}
			}
			foreach (IEventDefinition eventDefinition in this.GetEventDefs())
			{
				EventDefinitionHandle eventDefinitionHandle = this.GetEventDefinitionHandle(eventDefinition);
				foreach (IMethodReference methodReference2 in eventDefinition.GetAccessors(this.Context))
				{
					MethodSemanticsAttributes methodSemanticsAttributes2;
					if (methodReference2 == eventDefinition.Adder)
					{
						methodSemanticsAttributes2 = MethodSemanticsAttributes.Adder;
					}
					else if (methodReference2 == eventDefinition.Remover)
					{
						methodSemanticsAttributes2 = MethodSemanticsAttributes.Remover;
					}
					else if (methodReference2 == eventDefinition.Caller)
					{
						methodSemanticsAttributes2 = MethodSemanticsAttributes.Raiser;
					}
					else
					{
						methodSemanticsAttributes2 = MethodSemanticsAttributes.Other;
					}
					this.metadata.AddMethodSemantics(eventDefinitionHandle, methodSemanticsAttributes2, this.GetMethodDefinitionHandle(methodReference2.GetResolvedMethod(this.Context)));
				}
			}
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0001211C File Offset: 0x0001031C
		private void PopulateModuleRefTableRows()
		{
			IReadOnlyList<string> moduleRefs = this.GetModuleRefs();
			this.metadata.SetCapacity(TableIndex.ModuleRef, moduleRefs.Count);
			foreach (string text in moduleRefs)
			{
				this.metadata.AddModuleReference(this.GetStringHandleForPathAndCheckLength(text, null));
			}
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0001218C File Offset: 0x0001038C
		private void PopulateModuleTableRow(out Blob mvidFixup)
		{
			this.CheckPathLength(this.module.ModuleName, null);
			Guid persistentIdentifier = this.module.SerializationProperties.PersistentIdentifier;
			GuidHandle guidHandle;
			if (persistentIdentifier != default(Guid))
			{
				guidHandle = this.metadata.GetOrAddGuid(persistentIdentifier);
				mvidFixup = default(Blob);
			}
			else
			{
				ReservedBlob<GuidHandle> reservedBlob = this.metadata.ReserveGuid();
				mvidFixup = reservedBlob.Content;
				guidHandle = reservedBlob.Handle;
				reservedBlob.CreateWriter().WriteBytes(0, mvidFixup.Length);
			}
			this.metadata.AddModule((int)this.Generation, this.metadata.GetOrAddString(this.module.ModuleName), guidHandle, this.metadata.GetOrAddGuid(this.EncId), this.metadata.GetOrAddGuid(this.EncBaseId));
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00012268 File Offset: 0x00010468
		private void PopulateParamTableRows()
		{
			IReadOnlyList<IParameterDefinition> parameterDefs = this.GetParameterDefs();
			this.metadata.SetCapacity(TableIndex.Param, parameterDefs.Count);
			foreach (IParameterDefinition parameterDefinition in parameterDefs)
			{
				MetadataBuilder metadataBuilder = this.metadata;
				ParameterAttributes parameterAttributes = MetadataWriter.GetParameterAttributes(parameterDefinition);
				int num = (int)((parameterDefinition is ReturnValueParameter) ? 0 : (parameterDefinition.Index + 1));
				metadataBuilder.AddParameter(parameterAttributes, this.GetStringHandleForNameAndCheckLength(parameterDefinition.Name, parameterDefinition), num);
			}
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x000122F8 File Offset: 0x000104F8
		private void PopulatePropertyTableRows()
		{
			IReadOnlyList<IPropertyDefinition> propertyDefs = this.GetPropertyDefs();
			this.metadata.SetCapacity(TableIndex.Property, propertyDefs.Count);
			foreach (IPropertyDefinition propertyDefinition in propertyDefs)
			{
				this.metadata.AddProperty(MetadataWriter.GetPropertyAttributes(propertyDefinition), this.GetStringHandleForNameAndCheckLength(propertyDefinition.Name, propertyDefinition), this.GetPropertySignatureHandle(propertyDefinition));
			}
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0001237C File Offset: 0x0001057C
		private void PopulateTypeDefTableRows()
		{
			IReadOnlyList<ITypeDefinition> typeDefs = this.GetTypeDefs();
			this.metadata.SetCapacity(TableIndex.TypeDef, typeDefs.Count);
			foreach (ITypeDefinition typeDefinition in typeDefs)
			{
				INamedTypeDefinition namedTypeDefinition = (INamedTypeDefinition)typeDefinition;
				INamespaceTypeDefinition namespaceTypeDefinition = namedTypeDefinition.AsNamespaceTypeDefinition(this.Context);
				string mangledName = MetadataWriter.GetMangledName(namedTypeDefinition);
				ITypeReference baseClass = namedTypeDefinition.GetBaseClass(this.Context);
				this.metadata.AddTypeDefinition(this.GetTypeAttributes(namedTypeDefinition), (namespaceTypeDefinition != null) ? this.GetStringHandleForNamespaceAndCheckLength(namespaceTypeDefinition, mangledName) : default(StringHandle), this.GetStringHandleForNameAndCheckLength(mangledName, namedTypeDefinition), (baseClass != null) ? this.GetTypeHandle(baseClass, true) : default(EntityHandle), this.GetFirstFieldDefinitionHandle(namedTypeDefinition), this.GetFirstMethodDefinitionHandle(namedTypeDefinition));
			}
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00012460 File Offset: 0x00010660
		private void PopulateNestedClassTableRows()
		{
			foreach (ITypeDefinition typeDefinition in this.GetTypeDefs())
			{
				INestedTypeDefinition nestedTypeDefinition = typeDefinition.AsNestedTypeDefinition(this.Context);
				if (nestedTypeDefinition != null)
				{
					this.metadata.AddNestedType(this.GetTypeDefinitionHandle(typeDefinition), this.GetTypeDefinitionHandle(nestedTypeDefinition.ContainingTypeDefinition));
				}
			}
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x000124D4 File Offset: 0x000106D4
		private void PopulateClassLayoutTableRows()
		{
			foreach (ITypeDefinition typeDefinition in this.GetTypeDefs())
			{
				if (typeDefinition.Alignment != 0 || typeDefinition.SizeOf != 0U)
				{
					this.metadata.AddTypeLayout(this.GetTypeDefinitionHandle(typeDefinition), typeDefinition.Alignment, typeDefinition.SizeOf);
				}
			}
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00012548 File Offset: 0x00010748
		private void PopulateTypeRefTableRows()
		{
			IReadOnlyList<ITypeReference> typeRefs = this.GetTypeRefs();
			this.metadata.SetCapacity(TableIndex.TypeRef, typeRefs.Count);
			foreach (ITypeReference typeReference in typeRefs)
			{
				INestedTypeReference asNestedTypeReference = typeReference.AsNestedTypeReference;
				EntityHandle entityHandle;
				StringHandle stringHandle;
				StringHandle stringHandle2;
				if (asNestedTypeReference != null)
				{
					ISpecializedNestedTypeReference asSpecializedNestedTypeReference = asNestedTypeReference.AsSpecializedNestedTypeReference;
					ITypeReference typeReference2;
					if (asSpecializedNestedTypeReference != null)
					{
						typeReference2 = asSpecializedNestedTypeReference.GetUnspecializedVersion(this.Context).GetContainingType(this.Context);
					}
					else
					{
						typeReference2 = asNestedTypeReference.GetContainingType(this.Context);
					}
					entityHandle = this.GetTypeReferenceHandle(typeReference2);
					stringHandle = this.GetStringHandleForNameAndCheckLength(MetadataWriter.GetMangledName(asNestedTypeReference), asNestedTypeReference);
					stringHandle2 = default(StringHandle);
				}
				else
				{
					INamespaceTypeReference asNamespaceTypeReference = typeReference.AsNamespaceTypeReference;
					if (asNamespaceTypeReference == null)
					{
						throw ExceptionUtilities.UnexpectedValue(typeReference);
					}
					entityHandle = this.GetResolutionScopeHandle(asNamespaceTypeReference.GetUnit(this.Context));
					string mangledName = MetadataWriter.GetMangledName(asNamespaceTypeReference);
					stringHandle = this.GetStringHandleForNameAndCheckLength(mangledName, asNamespaceTypeReference);
					stringHandle2 = this.GetStringHandleForNamespaceAndCheckLength(asNamespaceTypeReference, mangledName);
				}
				this.metadata.AddTypeReference(entityHandle, stringHandle2, stringHandle);
			}
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00012670 File Offset: 0x00010870
		private void PopulateTypeSpecTableRows()
		{
			IReadOnlyList<ITypeReference> typeSpecs = this.GetTypeSpecs();
			this.metadata.SetCapacity(TableIndex.TypeSpec, typeSpecs.Count);
			foreach (ITypeReference typeReference in typeSpecs)
			{
				this.metadata.AddTypeSpecification(this.GetTypeSpecSignatureIndex(typeReference));
			}
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x000126E0 File Offset: 0x000108E0
		private void PopulateStandaloneSignatures()
		{
			foreach (BlobHandle blobHandle in this.GetStandaloneSignatureBlobHandles())
			{
				this.metadata.AddStandaloneSignature(blobHandle);
			}
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00012734 File Offset: 0x00010934
		private int[] SerializeThrowNullMethodBodies(BlobBuilder ilBuilder)
		{
			IReadOnlyList<IMethodDefinition> methodDefs = this.GetMethodDefs();
			int[] array = new int[methodDefs.Count];
			int num = -1;
			int num2 = 0;
			using (IEnumerator<IMethodDefinition> enumerator = methodDefs.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.HasBody())
					{
						if (num == -1)
						{
							num = ilBuilder.Count;
							ilBuilder.WriteBytes(MetadataWriter.ThrowNullEncodedBody);
						}
						array[num2] = num;
					}
					else
					{
						array[num2] = -1;
					}
					num2++;
				}
			}
			return array;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x000127B8 File Offset: 0x000109B8
		private int[] SerializeMethodBodies(BlobBuilder ilBuilder, PdbWriter nativePdbWriterOpt, out Blob mvidStringFixup)
		{
			CustomDebugInfoWriter customDebugInfoWriter = ((nativePdbWriterOpt != null) ? new CustomDebugInfoWriter(nativePdbWriterOpt) : null);
			IReadOnlyList<IMethodDefinition> methodDefs = this.GetMethodDefs();
			int[] array = new int[methodDefs.Count];
			LocalVariableHandle localVariableHandle = default(LocalVariableHandle);
			LocalConstantHandle localConstantHandle = default(LocalConstantHandle);
			MethodBodyStreamEncoder methodBodyStreamEncoder = new MethodBodyStreamEncoder(ilBuilder);
			UserStringHandle userStringHandle = default(UserStringHandle);
			mvidStringFixup = default(Blob);
			int num = 1;
			foreach (IMethodDefinition methodDefinition in methodDefs)
			{
				this._cancellationToken.ThrowIfCancellationRequested();
				IMethodBody methodBody;
				StandaloneSignatureHandle standaloneSignatureHandle;
				int num2;
				if (methodDefinition.HasBody())
				{
					methodBody = methodDefinition.GetBody(this.Context);
					if (methodBody != null)
					{
						standaloneSignatureHandle = this.SerializeLocalVariablesSignature(methodBody);
						num2 = this.SerializeMethodBody(methodBodyStreamEncoder, methodBody, standaloneSignatureHandle, ref userStringHandle, ref mvidStringFixup);
						if (nativePdbWriterOpt != null)
						{
							nativePdbWriterOpt.SerializeDebugInfo(methodBody, standaloneSignatureHandle, customDebugInfoWriter);
						}
					}
					else
					{
						num2 = 0;
						standaloneSignatureHandle = default(StandaloneSignatureHandle);
					}
				}
				else
				{
					num2 = -1;
					methodBody = null;
					standaloneSignatureHandle = default(StandaloneSignatureHandle);
				}
				if (this._debugMetadataOpt != null)
				{
					this.SerializeMethodDebugInfo(methodBody, num, standaloneSignatureHandle, ref localVariableHandle, ref localConstantHandle);
				}
				DynamicAnalysisDataWriter dynamicAnalysisDataWriterOpt = this._dynamicAnalysisDataWriterOpt;
				if (dynamicAnalysisDataWriterOpt != null)
				{
					dynamicAnalysisDataWriterOpt.SerializeMethodDynamicAnalysisData(methodBody);
				}
				array[num - 1] = num2;
				num++;
			}
			return array;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x000128F8 File Offset: 0x00010AF8
		private int SerializeMethodBody(MethodBodyStreamEncoder encoder, IMethodBody methodBody, StandaloneSignatureHandle localSignatureHandleOpt, ref UserStringHandle mvidStringHandle, ref Blob mvidStringFixup)
		{
			int length = methodBody.IL.Length;
			ImmutableArray<ExceptionHandlerRegion> exceptionRegions = methodBody.ExceptionRegions;
			bool flag = length < 64 && methodBody.MaxStack <= 8 && localSignatureHandleOpt.IsNil && exceptionRegions.Length == 0;
			ValueTuple<ImmutableArray<byte>, bool> valueTuple = new ValueTuple<ImmutableArray<byte>, bool>(methodBody.IL, methodBody.AreLocalsZeroed);
			int num;
			if (!this._deterministic && flag && this._smallMethodBodies.TryGetValue(valueTuple, ref num))
			{
				return num;
			}
			MethodBodyStreamEncoder.MethodBody methodBody2 = encoder.AddMethodBody(methodBody.IL.Length, (int)methodBody.MaxStack, exceptionRegions.Length, MetadataWriter.MayUseSmallExceptionHeaders(exceptionRegions), localSignatureHandleOpt, methodBody.AreLocalsZeroed ? MethodBodyAttributes.InitLocals : MethodBodyAttributes.None, methodBody.HasStackalloc);
			if (flag && !this._deterministic)
			{
				this._smallMethodBodies.Add(valueTuple, methodBody2.Offset);
			}
			this.WriteInstructions(methodBody2.Instructions, methodBody.IL, ref mvidStringHandle, ref mvidStringFixup);
			this.SerializeMethodBodyExceptionHandlerTable(methodBody2.ExceptionRegions, exceptionRegions);
			return methodBody2.Offset;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x000129FC File Offset: 0x00010BFC
		protected virtual StandaloneSignatureHandle SerializeLocalVariablesSignature(IMethodBody body)
		{
			ImmutableArray<ILocalDefinition> localVariables = body.LocalVariables;
			if (localVariables.Length == 0)
			{
				return default(StandaloneSignatureHandle);
			}
			PooledBlobBuilder instance = PooledBlobBuilder.GetInstance(1024);
			LocalVariablesEncoder localVariablesEncoder = new BlobEncoder(instance).LocalVariableSignature(localVariables.Length);
			foreach (ILocalDefinition localDefinition in localVariables)
			{
				this.SerializeLocalVariableType(localVariablesEncoder.AddVariable(), localDefinition);
			}
			BlobHandle orAddBlob = this.metadata.GetOrAddBlob(instance);
			StandaloneSignatureHandle orAddStandaloneSignatureHandle = this.GetOrAddStandaloneSignatureHandle(orAddBlob);
			instance.Free();
			return orAddStandaloneSignatureHandle;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00012A8C File Offset: 0x00010C8C
		protected void SerializeLocalVariableType(LocalVariableTypeEncoder encoder, ILocalDefinition local)
		{
			if (local.CustomModifiers.Length > 0)
			{
				this.SerializeCustomModifiers(encoder.CustomModifiers(), local.CustomModifiers);
			}
			if (this.module.IsPlatformType(local.Type, PlatformType.SystemTypedReference))
			{
				encoder.TypedReference();
				return;
			}
			this.SerializeTypeReference(encoder.Type(local.IsReference, local.IsPinned), local.Type);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00012AFC File Offset: 0x00010CFC
		internal StandaloneSignatureHandle SerializeLocalConstantStandAloneSignature(ILocalDefinition localConstant)
		{
			PooledBlobBuilder instance = PooledBlobBuilder.GetInstance(1024);
			SignatureTypeEncoder signatureTypeEncoder = new BlobEncoder(instance).FieldSignature();
			if (localConstant.CustomModifiers.Length > 0)
			{
				this.SerializeCustomModifiers(signatureTypeEncoder.CustomModifiers(), localConstant.CustomModifiers);
			}
			this.SerializeTypeReference(signatureTypeEncoder, localConstant.Type);
			BlobHandle orAddBlob = this.metadata.GetOrAddBlob(instance);
			StandaloneSignatureHandle orAddStandaloneSignatureHandle = this.GetOrAddStandaloneSignatureHandle(orAddBlob);
			instance.Free();
			return orAddStandaloneSignatureHandle;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00012B6F File Offset: 0x00010D6F
		private static byte ReadByte(ImmutableArray<byte> buffer, int pos)
		{
			return buffer[pos];
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00012B79 File Offset: 0x00010D79
		private static int ReadInt32(ImmutableArray<byte> buffer, int pos)
		{
			return (int)buffer[pos] | ((int)buffer[pos + 1] << 8) | ((int)buffer[pos + 2] << 16) | ((int)buffer[pos + 3] << 24);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00012BAC File Offset: 0x00010DAC
		private EntityHandle GetHandle(object reference)
		{
			ITypeReference typeReference = reference as ITypeReference;
			EntityHandle entityHandle;
			if (typeReference == null)
			{
				IFieldReference fieldReference = reference as IFieldReference;
				if (fieldReference == null)
				{
					IMethodReference methodReference = reference as IMethodReference;
					if (methodReference == null)
					{
						ISignature signature = reference as ISignature;
						if (signature == null)
						{
							throw ExceptionUtilities.UnexpectedValue(reference);
						}
						entityHandle = this.GetStandaloneSignatureHandle(signature);
					}
					else
					{
						entityHandle = this.GetMethodHandle(methodReference);
					}
				}
				else
				{
					entityHandle = this.GetFieldHandle(fieldReference);
				}
			}
			else
			{
				entityHandle = this.GetTypeHandle(typeReference, true);
			}
			return entityHandle;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00012C1C File Offset: 0x00010E1C
		private EntityHandle ResolveEntityHandleFromPseudoToken(int pseudoSymbolToken)
		{
			object obj = this._pseudoSymbolTokenToReferenceMap[pseudoSymbolToken];
			if (obj != null)
			{
				IReference reference = obj as IReference;
				if (reference != null)
				{
					this._referenceVisitor.VisitMethodBodyReference(reference);
				}
				else
				{
					ISignature signature = obj as ISignature;
					if (signature != null)
					{
						this._referenceVisitor.VisitSignature(signature);
					}
				}
				EntityHandle handle = this.GetHandle(obj);
				this._pseudoSymbolTokenToTokenMap[pseudoSymbolToken] = handle;
				this._pseudoSymbolTokenToReferenceMap[pseudoSymbolToken] = null;
				return handle;
			}
			return this._pseudoSymbolTokenToTokenMap[pseudoSymbolToken];
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00012C94 File Offset: 0x00010E94
		private UserStringHandle ResolveUserStringHandleFromPseudoToken(int pseudoStringToken)
		{
			string text = this._pseudoStringTokenToStringMap[pseudoStringToken];
			if (text != null)
			{
				UserStringHandle orAddUserString = this.GetOrAddUserString(text);
				this._pseudoStringTokenToTokenMap[pseudoStringToken] = orAddUserString;
				this._pseudoStringTokenToStringMap[pseudoStringToken] = null;
				return orAddUserString;
			}
			return this._pseudoStringTokenToTokenMap[pseudoStringToken];
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00012CE4 File Offset: 0x00010EE4
		private UserStringHandle GetOrAddUserString(string str)
		{
			if (!this._userStringTokenOverflow)
			{
				try
				{
					return this.metadata.GetOrAddUserString(str);
				}
				catch (ImageFormatLimitationException)
				{
					this.Context.Diagnostics.Add(this.messageProvider.CreateDiagnostic(this.messageProvider.ERR_TooManyUserStrings, NoLocation.Singleton));
					this._userStringTokenOverflow = true;
				}
			}
			return default(UserStringHandle);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00012D58 File Offset: 0x00010F58
		private ReservedBlob<UserStringHandle> ReserveUserString(int length)
		{
			if (!this._userStringTokenOverflow)
			{
				try
				{
					return this.metadata.ReserveUserString(length);
				}
				catch (ImageFormatLimitationException)
				{
					this.Context.Diagnostics.Add(this.messageProvider.CreateDiagnostic(this.messageProvider.ERR_TooManyUserStrings, NoLocation.Singleton));
					this._userStringTokenOverflow = true;
				}
			}
			return default(ReservedBlob<UserStringHandle>);
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00012DCC File Offset: 0x00010FCC
		private void WriteInstructions(Blob finalIL, ImmutableArray<byte> generatedIL, ref UserStringHandle mvidStringHandle, ref Blob mvidStringFixup)
		{
			BlobWriter blobWriter = new BlobWriter(finalIL);
			blobWriter.WriteBytes(generatedIL);
			blobWriter.Offset = 0;
			int i = 0;
			while (i < generatedIL.Length)
			{
				OperandType operandType = InstructionOperandTypes.ReadOperandType(generatedIL, ref i);
				switch (operandType)
				{
				case 0:
				case 2:
				case 17:
					i += 4;
					continue;
				case 1:
				case 4:
				case 9:
				case 12:
				case 13:
				{
					int num = MetadataWriter.ReadInt32(generatedIL, i);
					int num2 = 0;
					if (operandType == 12)
					{
						int num3 = num & -16777216;
						if (num3 != 0 && num != -1)
						{
							blobWriter.Offset = i - 1;
							blobWriter.WriteByte(32);
							uint num4 = (uint)num3;
							if (num4 != 536870912U)
							{
								if (num4 != 1073741824U)
								{
									if (num4 != 2147483648U)
									{
										throw ExceptionUtilities.UnexpectedValue(num3);
									}
									num2 = MetadataTokens.GetToken((MethodDefinitionHandle)this.ResolveEntityHandleFromPseudoToken(num & 16777215)) & 16777215;
								}
								else
								{
									num2 = this.GreatestMethodDefIndex;
								}
							}
							else
							{
								num2 = this._dynamicAnalysisDataWriterOpt.GetOrAddDocument(this.module.GetSourceDocumentFromIndex((uint)(num & 16777215)));
							}
						}
					}
					blobWriter.Offset = i;
					blobWriter.WriteInt32((num2 == 0) ? MetadataTokens.GetToken(this.ResolveEntityHandleFromPseudoToken(num)) : num2);
					i += 4;
					continue;
				}
				case 3:
				case 7:
					i += 8;
					continue;
				case 5:
					continue;
				case 10:
				{
					blobWriter.Offset = i;
					int num5 = MetadataWriter.ReadInt32(generatedIL, i);
					UserStringHandle userStringHandle;
					if (num5 == -2147483648)
					{
						if (mvidStringHandle.IsNil)
						{
							ReservedBlob<UserStringHandle> reservedBlob = this.ReserveUserString(36);
							mvidStringHandle = reservedBlob.Handle;
							mvidStringFixup = reservedBlob.Content;
						}
						userStringHandle = mvidStringHandle;
					}
					else
					{
						userStringHandle = this.ResolveUserStringHandleFromPseudoToken(num5);
					}
					blobWriter.WriteInt32(MetadataTokens.GetToken(userStringHandle));
					i += 4;
					continue;
				}
				case 11:
				{
					int num6 = MetadataWriter.ReadInt32(generatedIL, i);
					i += (num6 + 1) * 4;
					continue;
				}
				case 14:
					i += 2;
					continue;
				case 15:
				case 16:
				case 18:
					i++;
					continue;
				}
				throw ExceptionUtilities.UnexpectedValue(operandType);
			}
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00012FFC File Offset: 0x000111FC
		private void SerializeMethodBodyExceptionHandlerTable(ExceptionRegionEncoder encoder, ImmutableArray<ExceptionHandlerRegion> regions)
		{
			foreach (ExceptionHandlerRegion exceptionHandlerRegion in regions)
			{
				ITypeReference exceptionType = exceptionHandlerRegion.ExceptionType;
				encoder.Add(exceptionHandlerRegion.HandlerKind, exceptionHandlerRegion.TryStartOffset, exceptionHandlerRegion.TryLength, exceptionHandlerRegion.HandlerStartOffset, exceptionHandlerRegion.HandlerLength, (exceptionType != null) ? this.GetTypeHandle(exceptionType, true) : default(EntityHandle), exceptionHandlerRegion.FilterDecisionStartOffset);
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00013070 File Offset: 0x00011270
		private static bool MayUseSmallExceptionHeaders(ImmutableArray<ExceptionHandlerRegion> exceptionRegions)
		{
			if (!ExceptionRegionEncoder.IsSmallRegionCount(exceptionRegions.Length))
			{
				return false;
			}
			foreach (ExceptionHandlerRegion exceptionHandlerRegion in exceptionRegions)
			{
				if (!ExceptionRegionEncoder.IsSmallExceptionRegion(exceptionHandlerRegion.TryStartOffset, exceptionHandlerRegion.TryLength) || !ExceptionRegionEncoder.IsSmallExceptionRegion(exceptionHandlerRegion.HandlerStartOffset, exceptionHandlerRegion.HandlerLength))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x000130D4 File Offset: 0x000112D4
		private void SerializeParameterInformation(ParameterTypeEncoder encoder, IParameterTypeInformation parameterTypeInformation)
		{
			ITypeReference type = parameterTypeInformation.GetType(this.Context);
			if (this.module.IsPlatformType(type, PlatformType.SystemTypedReference))
			{
				this.SerializeCustomModifiers(encoder.CustomModifiers(), parameterTypeInformation.CustomModifiers);
				encoder.TypedReference();
				return;
			}
			this.SerializeCustomModifiers(encoder.CustomModifiers(), parameterTypeInformation.RefCustomModifiers);
			SignatureTypeEncoder signatureTypeEncoder = encoder.Type(parameterTypeInformation.IsByReference);
			this.SerializeCustomModifiers(signatureTypeEncoder.CustomModifiers(), parameterTypeInformation.CustomModifiers);
			this.SerializeTypeReference(signatureTypeEncoder, type);
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00013158 File Offset: 0x00011358
		private void SerializeFieldSignature(IFieldReference fieldReference, BlobBuilder builder)
		{
			SignatureTypeEncoder signatureTypeEncoder = new BlobEncoder(builder).FieldSignature();
			this.SerializeTypeReference(signatureTypeEncoder, fieldReference.GetType(this.Context));
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00013188 File Offset: 0x00011388
		private void SerializeMethodSpecificationSignature(BlobBuilder builder, IGenericMethodInstanceReference genericMethodInstanceReference)
		{
			GenericTypeArgumentsEncoder genericTypeArgumentsEncoder = new BlobEncoder(builder).MethodSpecificationSignature((int)genericMethodInstanceReference.GetGenericMethod(this.Context).GenericParameterCount);
			foreach (ITypeReference typeReference in genericMethodInstanceReference.GetGenericArguments(this.Context))
			{
				this.SerializeTypeReference(genericTypeArgumentsEncoder.AddArgument(), typeReference);
			}
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00013204 File Offset: 0x00011404
		private void SerializeCustomAttributeSignature(ICustomAttribute customAttribute, BlobBuilder builder)
		{
			ImmutableArray<IParameterTypeInformation> parameters = customAttribute.Constructor(this.Context, false).GetParameters(this.Context);
			ImmutableArray<IMetadataExpression> arguments = customAttribute.GetArguments(this.Context);
			FixedArgumentsEncoder fixedArgumentsEncoder;
			CustomAttributeNamedArgumentsEncoder customAttributeNamedArgumentsEncoder;
			new BlobEncoder(builder).CustomAttributeSignature(out fixedArgumentsEncoder, out customAttributeNamedArgumentsEncoder);
			for (int i = 0; i < parameters.Length; i++)
			{
				this.SerializeMetadataExpression(fixedArgumentsEncoder.AddArgument(), arguments[i], parameters[i].GetType(this.Context));
			}
			this.SerializeCustomAttributeNamedArguments(customAttributeNamedArgumentsEncoder.Count((int)customAttribute.NamedArgumentCount), customAttribute);
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x000132A0 File Offset: 0x000114A0
		private void SerializeCustomAttributeNamedArguments(NamedArgumentsEncoder encoder, ICustomAttribute customAttribute)
		{
			foreach (IMetadataNamedArgument metadataNamedArgument in customAttribute.GetNamedArguments(this.Context))
			{
				NamedArgumentTypeEncoder namedArgumentTypeEncoder;
				NameEncoder nameEncoder;
				LiteralEncoder literalEncoder;
				encoder.AddArgument(metadataNamedArgument.IsField, out namedArgumentTypeEncoder, out nameEncoder, out literalEncoder);
				this.SerializeNamedArgumentType(namedArgumentTypeEncoder, metadataNamedArgument.Type);
				nameEncoder.Name(metadataNamedArgument.ArgumentName);
				this.SerializeMetadataExpression(literalEncoder, metadataNamedArgument.ArgumentValue, metadataNamedArgument.Type);
			}
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00013318 File Offset: 0x00011518
		private void SerializeNamedArgumentType(NamedArgumentTypeEncoder encoder, ITypeReference type)
		{
			IArrayTypeReference arrayTypeReference = type as IArrayTypeReference;
			if (arrayTypeReference != null)
			{
				this.SerializeCustomAttributeArrayType(encoder.SZArray(), arrayTypeReference);
				return;
			}
			if (this.module.IsPlatformType(type, PlatformType.SystemObject))
			{
				encoder.Object();
				return;
			}
			this.SerializeCustomAttributeElementType(encoder.ScalarType(), type);
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00013364 File Offset: 0x00011564
		private void SerializeMetadataExpression(LiteralEncoder encoder, IMetadataExpression expression, ITypeReference targetType)
		{
			MetadataCreateArray metadataCreateArray = expression as MetadataCreateArray;
			if (metadataCreateArray != null)
			{
				IArrayTypeReference arrayTypeReference = targetType as IArrayTypeReference;
				VectorEncoder vectorEncoder;
				ITypeReference typeReference;
				if (arrayTypeReference == null)
				{
					CustomAttributeArrayTypeEncoder customAttributeArrayTypeEncoder;
					encoder.TaggedVector(out customAttributeArrayTypeEncoder, out vectorEncoder);
					this.SerializeCustomAttributeArrayType(customAttributeArrayTypeEncoder, metadataCreateArray.ArrayType);
					typeReference = metadataCreateArray.ElementType;
				}
				else
				{
					vectorEncoder = encoder.Vector();
					typeReference = arrayTypeReference.GetElementType(this.Context);
				}
				LiteralsEncoder literalsEncoder = vectorEncoder.Count(metadataCreateArray.Elements.Length);
				foreach (IMetadataExpression metadataExpression in metadataCreateArray.Elements)
				{
					this.SerializeMetadataExpression(literalsEncoder.AddLiteral(), metadataExpression, typeReference);
				}
				return;
			}
			MetadataConstant metadataConstant = expression as MetadataConstant;
			ScalarEncoder scalarEncoder;
			if (this.module.IsPlatformType(targetType, PlatformType.SystemObject))
			{
				CustomAttributeElementTypeEncoder customAttributeElementTypeEncoder;
				encoder.TaggedScalar(out customAttributeElementTypeEncoder, out scalarEncoder);
				if (metadataConstant != null && metadataConstant.Value == null && this.module.IsPlatformType(metadataConstant.Type, PlatformType.SystemObject))
				{
					customAttributeElementTypeEncoder.String();
				}
				else
				{
					this.SerializeCustomAttributeElementType(customAttributeElementTypeEncoder, expression.Type);
				}
			}
			else
			{
				scalarEncoder = encoder.Scalar();
			}
			if (metadataConstant == null)
			{
				scalarEncoder.SystemType(((MetadataTypeOf)expression).TypeToGet.GetSerializedTypeName(this.Context));
				return;
			}
			if (metadataConstant.Type is IArrayTypeReference)
			{
				scalarEncoder.NullArray();
				return;
			}
			scalarEncoder.Constant(metadataConstant.Value);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x000134BC File Offset: 0x000116BC
		private void SerializeMarshallingDescriptor(IMarshallingInformation marshallingInformation, BlobBuilder writer)
		{
			writer.WriteCompressedInteger(marshallingInformation.UnmanagedType);
			UnmanagedType unmanagedType = marshallingInformation.UnmanagedType;
			switch (unmanagedType)
			{
			case 23:
				writer.WriteCompressedInteger(marshallingInformation.NumberOfElements);
				return;
			case 24:
			case 27:
				break;
			case 25:
			case 26:
			case 28:
				if (marshallingInformation.IidParameterIndex >= 0)
				{
					writer.WriteCompressedInteger(marshallingInformation.IidParameterIndex);
				}
				break;
			case 29:
				if (marshallingInformation.SafeArrayElementSubtype >= VarEnum.VT_EMPTY)
				{
					writer.WriteCompressedInteger((int)marshallingInformation.SafeArrayElementSubtype);
					ITypeReference safeArrayElementUserDefinedSubtype = marshallingInformation.GetSafeArrayElementUserDefinedSubtype(this.Context);
					if (safeArrayElementUserDefinedSubtype != null)
					{
						this.SerializeTypeName(safeArrayElementUserDefinedSubtype, writer);
						return;
					}
				}
				break;
			case 30:
				writer.WriteCompressedInteger(marshallingInformation.NumberOfElements);
				if (marshallingInformation.ElementType >= 0)
				{
					writer.WriteCompressedInteger(marshallingInformation.ElementType);
					return;
				}
				break;
			default:
				if (unmanagedType != 42)
				{
					if (unmanagedType != 44)
					{
						return;
					}
					writer.WriteUInt16(0);
					object customMarshaller = marshallingInformation.GetCustomMarshaller(this.Context);
					ITypeReference typeReference = customMarshaller as ITypeReference;
					if (typeReference == null)
					{
						if (customMarshaller != null)
						{
							writer.WriteSerializedString((string)customMarshaller);
						}
						else
						{
							writer.WriteByte(0);
						}
					}
					else
					{
						this.SerializeTypeName(typeReference, writer);
					}
					string customMarshallerRuntimeArgument = marshallingInformation.CustomMarshallerRuntimeArgument;
					if (customMarshallerRuntimeArgument != null)
					{
						writer.WriteSerializedString(customMarshallerRuntimeArgument);
						return;
					}
					writer.WriteByte(0);
					return;
				}
				else
				{
					writer.WriteCompressedInteger(marshallingInformation.ElementType);
					if (marshallingInformation.ParamIndex >= 0)
					{
						writer.WriteCompressedInteger((int)marshallingInformation.ParamIndex);
						if (marshallingInformation.NumberOfElements >= 0)
						{
							writer.WriteCompressedInteger(marshallingInformation.NumberOfElements);
							writer.WriteByte(1);
							return;
						}
					}
					else if (marshallingInformation.NumberOfElements >= 0)
					{
						writer.WriteByte(0);
						writer.WriteCompressedInteger(marshallingInformation.NumberOfElements);
						writer.WriteByte(0);
						return;
					}
				}
				break;
			}
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0001364C File Offset: 0x0001184C
		private void SerializeTypeName(ITypeReference typeReference, BlobBuilder writer)
		{
			writer.WriteSerializedString(typeReference.GetSerializedTypeName(this.Context));
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00013660 File Offset: 0x00011860
		internal static string StrongName(IAssemblyReference assemblyReference)
		{
			AssemblyIdentity identity = assemblyReference.Identity;
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			StringBuilder builder = instance.Builder;
			builder.Append(identity.Name);
			builder.AppendFormat(CultureInfo.InvariantCulture, ", Version={0}.{1}.{2}.{3}", new object[]
			{
				identity.Version.Major,
				identity.Version.Minor,
				identity.Version.Build,
				identity.Version.Revision
			});
			if (!string.IsNullOrEmpty(identity.CultureName))
			{
				builder.AppendFormat(CultureInfo.InvariantCulture, ", Culture={0}", identity.CultureName);
			}
			else
			{
				builder.Append(", Culture=neutral");
			}
			builder.Append(", PublicKeyToken=");
			if (identity.PublicKeyToken.Length > 0)
			{
				foreach (byte b in identity.PublicKeyToken)
				{
					builder.Append(b.ToString("x2"));
				}
			}
			else
			{
				builder.Append("null");
			}
			if (identity.IsRetargetable)
			{
				builder.Append(", Retargetable=Yes");
			}
			if (identity.ContentType == 1)
			{
				builder.Append(", ContentType=WindowsRuntime");
			}
			return instance.ToStringAndFree();
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x000137B4 File Offset: 0x000119B4
		private void SerializePermissionSet(ImmutableArray<ICustomAttribute> permissionSet, BlobBuilder writer)
		{
			EmitContext context = this.Context;
			foreach (ICustomAttribute customAttribute in permissionSet)
			{
				bool flag = true;
				string text = customAttribute.GetType(context).GetSerializedTypeName(context, ref flag);
				if (!flag)
				{
					INamespaceTypeReference asNamespaceTypeReference = customAttribute.GetType(context).AsNamespaceTypeReference;
					IAssemblyReference assemblyReference = ((asNamespaceTypeReference != null) ? asNamespaceTypeReference.GetUnit(context) : null) as IAssemblyReference;
					if (assemblyReference != null)
					{
						text = text + ", " + MetadataWriter.StrongName(assemblyReference);
					}
				}
				writer.WriteSerializedString(text);
				PooledBlobBuilder instance = PooledBlobBuilder.GetInstance(1024);
				NamedArgumentsEncoder namedArgumentsEncoder = new BlobEncoder(instance).PermissionSetArguments((int)customAttribute.NamedArgumentCount);
				this.SerializeCustomAttributeNamedArguments(namedArgumentsEncoder, customAttribute);
				writer.WriteCompressedInteger(instance.Count);
				instance.WriteContentTo(writer);
				instance.Free();
			}
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0001388C File Offset: 0x00011A8C
		private void SerializeReturnValueAndParameters(MethodSignatureEncoder encoder, ISignature signature, ImmutableArray<IParameterTypeInformation> varargParameters)
		{
			ImmutableArray<IParameterTypeInformation> parameters = signature.GetParameters(this.Context);
			ITypeReference type = signature.GetType(this.Context);
			ReturnTypeEncoder returnTypeEncoder;
			ParametersEncoder parametersEncoder;
			encoder.Parameters(parameters.Length + varargParameters.Length, out returnTypeEncoder, out parametersEncoder);
			if (this.module.IsPlatformType(type, PlatformType.SystemTypedReference))
			{
				this.SerializeCustomModifiers(returnTypeEncoder.CustomModifiers(), signature.ReturnValueCustomModifiers);
				returnTypeEncoder.TypedReference();
			}
			else if (this.module.IsPlatformType(type, PlatformType.SystemVoid))
			{
				this.SerializeCustomModifiers(returnTypeEncoder.CustomModifiers(), signature.ReturnValueCustomModifiers);
				returnTypeEncoder.Void();
			}
			else
			{
				this.SerializeCustomModifiers(returnTypeEncoder.CustomModifiers(), signature.RefCustomModifiers);
				SignatureTypeEncoder signatureTypeEncoder = returnTypeEncoder.Type(signature.ReturnValueIsByRef);
				this.SerializeCustomModifiers(signatureTypeEncoder.CustomModifiers(), signature.ReturnValueCustomModifiers);
				this.SerializeTypeReference(signatureTypeEncoder, type);
			}
			foreach (IParameterTypeInformation parameterTypeInformation in parameters)
			{
				this.SerializeParameterInformation(parametersEncoder.AddParameter(), parameterTypeInformation);
			}
			if (varargParameters.Length > 0)
			{
				parametersEncoder = parametersEncoder.StartVarArgs();
				foreach (IParameterTypeInformation parameterTypeInformation2 in varargParameters)
				{
					this.SerializeParameterInformation(parametersEncoder.AddParameter(), parameterTypeInformation2);
				}
			}
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x000139CC File Offset: 0x00011BCC
		private void SerializeTypeReference(SignatureTypeEncoder encoder, ITypeReference typeReference)
		{
			PrimitiveTypeCode typeCode;
			IFunctionPointerTypeReference functionPointerTypeReference;
			IGenericTypeParameterReference asGenericTypeParameterReference;
			IArrayTypeReference arrayTypeReference;
			for (;;)
			{
				IModifiedTypeReference modifiedTypeReference = typeReference as IModifiedTypeReference;
				if (modifiedTypeReference != null)
				{
					this.SerializeCustomModifiers(encoder.CustomModifiers(), modifiedTypeReference.CustomModifiers);
					typeReference = modifiedTypeReference.UnmodifiedType;
				}
				else
				{
					typeCode = typeReference.TypeCode;
					if (typeCode != PrimitiveTypeCode.Pointer && typeCode - PrimitiveTypeCode.NotPrimitive > 1)
					{
						break;
					}
					IPointerTypeReference pointerTypeReference = typeReference as IPointerTypeReference;
					if (pointerTypeReference != null)
					{
						typeReference = pointerTypeReference.GetTargetType(this.Context);
						encoder = encoder.Pointer();
					}
					else
					{
						functionPointerTypeReference = typeReference as IFunctionPointerTypeReference;
						if (functionPointerTypeReference != null)
						{
							goto Block_4;
						}
						asGenericTypeParameterReference = typeReference.AsGenericTypeParameterReference;
						if (asGenericTypeParameterReference != null)
						{
							goto Block_5;
						}
						arrayTypeReference = typeReference as IArrayTypeReference;
						if (arrayTypeReference == null)
						{
							goto IL_012A;
						}
						typeReference = arrayTypeReference.GetElementType(this.Context);
						if (!arrayTypeReference.IsSZArray)
						{
							goto IL_00F9;
						}
						encoder = encoder.SZArray();
					}
				}
			}
			MetadataWriter.SerializePrimitiveType(encoder, typeCode);
			return;
			Block_4:
			ISignature signature = functionPointerTypeReference.Signature;
			MethodSignatureEncoder methodSignatureEncoder = encoder.FunctionPointer(signature.CallingConvention.ToSignatureConvention(), FunctionPointerAttributes.None, 0);
			this.SerializeReturnValueAndParameters(methodSignatureEncoder, signature, ImmutableArray<IParameterTypeInformation>.Empty);
			return;
			Block_5:
			encoder.GenericTypeParameter(this.GetNumberOfInheritedTypeParameters(asGenericTypeParameterReference.DefiningType) + (int)asGenericTypeParameterReference.Index);
			return;
			IL_00F9:
			SignatureTypeEncoder signatureTypeEncoder;
			ArrayShapeEncoder arrayShapeEncoder;
			encoder.Array(out signatureTypeEncoder, out arrayShapeEncoder);
			this.SerializeTypeReference(signatureTypeEncoder, typeReference);
			arrayShapeEncoder.Shape(arrayTypeReference.Rank, arrayTypeReference.Sizes, arrayTypeReference.LowerBounds);
			return;
			IL_012A:
			if (this.module.IsPlatformType(typeReference, PlatformType.SystemObject))
			{
				encoder.Object();
				return;
			}
			IGenericMethodParameterReference asGenericMethodParameterReference = typeReference.AsGenericMethodParameterReference;
			if (asGenericMethodParameterReference != null)
			{
				encoder.GenericMethodTypeParameter((int)asGenericMethodParameterReference.Index);
				return;
			}
			if (typeReference.IsTypeSpecification())
			{
				ITypeReference uninstantiatedGenericType = typeReference.GetUninstantiatedGenericType(this.Context);
				ArrayBuilder<ITypeReference> instance = ArrayBuilder<ITypeReference>.GetInstance();
				typeReference.GetConsolidatedTypeArguments(instance, this.Context);
				GenericTypeArgumentsEncoder genericTypeArgumentsEncoder = encoder.GenericInstantiation(this.GetTypeHandle(uninstantiatedGenericType, false), instance.Count, typeReference.IsValueType);
				foreach (ITypeReference typeReference2 in instance)
				{
					this.SerializeTypeReference(genericTypeArgumentsEncoder.AddArgument(), typeReference2);
				}
				instance.Free();
				return;
			}
			encoder.Type(this.GetTypeHandle(typeReference, true), typeReference.IsValueType);
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00013BC8 File Offset: 0x00011DC8
		private static void SerializePrimitiveType(SignatureTypeEncoder encoder, PrimitiveTypeCode primitiveType)
		{
			switch (primitiveType)
			{
			case PrimitiveTypeCode.Boolean:
				encoder.Boolean();
				return;
			case PrimitiveTypeCode.Char:
				encoder.Char();
				return;
			case PrimitiveTypeCode.Int8:
				encoder.SByte();
				return;
			case PrimitiveTypeCode.Float32:
				encoder.Single();
				return;
			case PrimitiveTypeCode.Float64:
				encoder.Double();
				return;
			case PrimitiveTypeCode.Int16:
				encoder.Int16();
				return;
			case PrimitiveTypeCode.Int32:
				encoder.Int32();
				return;
			case PrimitiveTypeCode.Int64:
				encoder.Int64();
				return;
			case PrimitiveTypeCode.IntPtr:
				encoder.IntPtr();
				return;
			case PrimitiveTypeCode.String:
				encoder.String();
				return;
			case PrimitiveTypeCode.UInt8:
				encoder.Byte();
				return;
			case PrimitiveTypeCode.UInt16:
				encoder.UInt16();
				return;
			case PrimitiveTypeCode.UInt32:
				encoder.UInt32();
				return;
			case PrimitiveTypeCode.UInt64:
				encoder.UInt64();
				return;
			case PrimitiveTypeCode.UIntPtr:
				encoder.UIntPtr();
				return;
			case PrimitiveTypeCode.Void:
				encoder.Builder.WriteByte(1);
				return;
			}
			throw ExceptionUtilities.UnexpectedValue(primitiveType);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00013CBC File Offset: 0x00011EBC
		private void SerializeCustomAttributeArrayType(CustomAttributeArrayTypeEncoder encoder, IArrayTypeReference arrayTypeReference)
		{
			ITypeReference elementType = arrayTypeReference.GetElementType(this.Context);
			if (this.module.IsPlatformType(elementType, PlatformType.SystemObject))
			{
				encoder.ObjectArray();
				return;
			}
			this.SerializeCustomAttributeElementType(encoder.ElementType(), elementType);
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00013CFC File Offset: 0x00011EFC
		private void SerializeCustomAttributeElementType(CustomAttributeElementTypeEncoder encoder, ITypeReference typeReference)
		{
			PrimitiveTypeCode typeCode = typeReference.TypeCode;
			if (typeCode != PrimitiveTypeCode.NotPrimitive)
			{
				MetadataWriter.SerializePrimitiveType(encoder, typeCode);
				return;
			}
			if (this.module.IsPlatformType(typeReference, PlatformType.SystemType))
			{
				encoder.SystemType();
				return;
			}
			encoder.Enum(typeReference.GetSerializedTypeName(this.Context));
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00013D48 File Offset: 0x00011F48
		private static void SerializePrimitiveType(CustomAttributeElementTypeEncoder encoder, PrimitiveTypeCode primitiveType)
		{
			switch (primitiveType)
			{
			case PrimitiveTypeCode.Boolean:
				encoder.Boolean();
				return;
			case PrimitiveTypeCode.Char:
				encoder.Char();
				return;
			case PrimitiveTypeCode.Int8:
				encoder.SByte();
				return;
			case PrimitiveTypeCode.Float32:
				encoder.Single();
				return;
			case PrimitiveTypeCode.Float64:
				encoder.Double();
				return;
			case PrimitiveTypeCode.Int16:
				encoder.Int16();
				return;
			case PrimitiveTypeCode.Int32:
				encoder.Int32();
				return;
			case PrimitiveTypeCode.Int64:
				encoder.Int64();
				return;
			case PrimitiveTypeCode.String:
				encoder.String();
				return;
			case PrimitiveTypeCode.UInt8:
				encoder.Byte();
				return;
			case PrimitiveTypeCode.UInt16:
				encoder.UInt16();
				return;
			case PrimitiveTypeCode.UInt32:
				encoder.UInt32();
				return;
			case PrimitiveTypeCode.UInt64:
				encoder.UInt64();
				return;
			}
			throw ExceptionUtilities.UnexpectedValue(primitiveType);
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00013E10 File Offset: 0x00012010
		private void SerializeCustomModifiers(CustomModifiersEncoder encoder, ImmutableArray<ICustomModifier> modifiers)
		{
			foreach (ICustomModifier customModifier in modifiers)
			{
				encoder = encoder.AddModifier(this.GetTypeHandle(customModifier.GetModifier(this.Context), true), customModifier.IsOptional);
			}
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00013E5C File Offset: 0x0001205C
		private int GetNumberOfInheritedTypeParameters(ITypeReference type)
		{
			INestedTypeReference nestedTypeReference = type.AsNestedTypeReference;
			if (nestedTypeReference == null)
			{
				return 0;
			}
			ISpecializedNestedTypeReference asSpecializedNestedTypeReference = nestedTypeReference.AsSpecializedNestedTypeReference;
			if (asSpecializedNestedTypeReference != null)
			{
				nestedTypeReference = asSpecializedNestedTypeReference.GetUnspecializedVersion(this.Context);
			}
			int num = 0;
			type = nestedTypeReference.GetContainingType(this.Context);
			for (nestedTypeReference = type.AsNestedTypeReference; nestedTypeReference != null; nestedTypeReference = type.AsNestedTypeReference)
			{
				num += (int)nestedTypeReference.GenericParameterCount;
				type = nestedTypeReference.GetContainingType(this.Context);
			}
			return num + (int)type.AsNamespaceTypeReference.GenericParameterCount;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00013ED8 File Offset: 0x000120D8
		internal static EditAndContinueMethodDebugInformation GetEncMethodDebugInfo(IMethodBody methodBody)
		{
			ImmutableArray<EncHoistedLocalInfo> stateMachineHoistedLocalSlots = methodBody.StateMachineHoistedLocalSlots;
			ImmutableArray<LocalSlotDebugInfo> immutableArray;
			if (stateMachineHoistedLocalSlots.IsDefault)
			{
				immutableArray = MetadataWriter.GetLocalSlotDebugInfos(methodBody.LocalVariables);
			}
			else
			{
				immutableArray = MetadataWriter.GetLocalSlotDebugInfos(stateMachineHoistedLocalSlots);
			}
			return new EditAndContinueMethodDebugInformation(methodBody.MethodId.Ordinal, immutableArray, methodBody.ClosureDebugInfo, methodBody.LambdaDebugInfo);
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00013F28 File Offset: 0x00012128
		internal static ImmutableArray<LocalSlotDebugInfo> GetLocalSlotDebugInfos(ImmutableArray<ILocalDefinition> locals)
		{
			if (!locals.Any<ILocalDefinition>((ILocalDefinition variable) => !variable.SlotInfo.Id.IsNone))
			{
				return ImmutableArray<LocalSlotDebugInfo>.Empty;
			}
			return locals.SelectAsArray<ILocalDefinition, LocalSlotDebugInfo>((ILocalDefinition variable) => variable.SlotInfo);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00013F88 File Offset: 0x00012188
		internal static ImmutableArray<LocalSlotDebugInfo> GetLocalSlotDebugInfos(ImmutableArray<EncHoistedLocalInfo> locals)
		{
			if (!locals.Any<EncHoistedLocalInfo>((EncHoistedLocalInfo variable) => !variable.SlotInfo.Id.IsNone))
			{
				return ImmutableArray<LocalSlotDebugInfo>.Empty;
			}
			return locals.SelectAsArray<EncHoistedLocalInfo, LocalSlotDebugInfo>((EncHoistedLocalInfo variable) => variable.SlotInfo);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00013FE8 File Offset: 0x000121E8
		private void SerializeMethodDebugInfo(IMethodBody bodyOpt, int methodRid, StandaloneSignatureHandle localSignatureHandleOpt, ref LocalVariableHandle lastLocalVariableHandle, ref LocalConstantHandle lastLocalConstantHandle)
		{
			if (bodyOpt == null)
			{
				this._debugMetadataOpt.AddMethodDebugInformation(default(DocumentHandle), default(BlobHandle));
				return;
			}
			if (bodyOpt.StateMachineTypeName == null && bodyOpt.SequencePoints.IsEmpty)
			{
				this._debugMetadataOpt.AddMethodDebugInformation(default(DocumentHandle), default(BlobHandle));
				return;
			}
			MethodDefinitionHandle methodDefinitionHandle = MetadataTokens.MethodDefinitionHandle(methodRid);
			IImportScope importScope = bodyOpt.ImportScope;
			ImportScopeHandle importScopeHandle = ((importScope != null) ? this.GetImportScopeIndex(importScope, this._scopeIndex) : default(ImportScopeHandle));
			DocumentHandle documentHandle;
			BlobHandle blobHandle = this.SerializeSequencePoints(localSignatureHandleOpt, bodyOpt.SequencePoints, this._documentIndex, out documentHandle);
			this._debugMetadataOpt.AddMethodDebugInformation(documentHandle, blobHandle);
			if (bodyOpt.LocalScopes.Length == 0)
			{
				this._debugMetadataOpt.AddLocalScope(methodDefinitionHandle, importScopeHandle, MetadataWriter.NextHandle(lastLocalVariableHandle), MetadataWriter.NextHandle(lastLocalConstantHandle), 0, bodyOpt.IL.Length);
			}
			else
			{
				foreach (LocalScope localScope in bodyOpt.LocalScopes)
				{
					this._debugMetadataOpt.AddLocalScope(methodDefinitionHandle, importScopeHandle, MetadataWriter.NextHandle(lastLocalVariableHandle), MetadataWriter.NextHandle(lastLocalConstantHandle), localScope.StartOffset, localScope.Length);
					foreach (ILocalDefinition localDefinition in localScope.Variables)
					{
						lastLocalVariableHandle = this._debugMetadataOpt.AddLocalVariable(localDefinition.PdbAttributes, localDefinition.SlotIndex, this._debugMetadataOpt.GetOrAddString(localDefinition.Name));
						this.SerializeLocalInfo(localDefinition, lastLocalVariableHandle);
					}
					foreach (ILocalDefinition localDefinition2 in localScope.Constants)
					{
						MetadataConstant compileTimeValue = localDefinition2.CompileTimeValue;
						lastLocalConstantHandle = this._debugMetadataOpt.AddLocalConstant(this._debugMetadataOpt.GetOrAddString(localDefinition2.Name), this.SerializeLocalConstantSignature(localDefinition2));
						this.SerializeLocalInfo(localDefinition2, lastLocalConstantHandle);
					}
				}
			}
			StateMachineMoveNextBodyDebugInfo moveNextBodyInfo = bodyOpt.MoveNextBodyInfo;
			if (moveNextBodyInfo != null)
			{
				this._debugMetadataOpt.AddStateMachineMethod(methodDefinitionHandle, this.GetMethodDefinitionHandle(moveNextBodyInfo.KickoffMethod));
				AsyncMoveNextBodyDebugInfo asyncMoveNextBodyDebugInfo = moveNextBodyInfo as AsyncMoveNextBodyDebugInfo;
				if (asyncMoveNextBodyDebugInfo != null)
				{
					this.SerializeAsyncMethodSteppingInfo(asyncMoveNextBodyDebugInfo, methodDefinitionHandle);
				}
			}
			this.SerializeStateMachineLocalScopes(bodyOpt, methodDefinitionHandle);
			if (this.Context.Module.CommonCompilation.Options.EnableEditAndContinue && this.IsFullMetadata)
			{
				this.SerializeEncMethodDebugInformation(bodyOpt, methodDefinitionHandle);
			}
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x000142A1 File Offset: 0x000124A1
		private static LocalVariableHandle NextHandle(LocalVariableHandle handle)
		{
			return MetadataTokens.LocalVariableHandle(MetadataTokens.GetRowNumber(handle) + 1);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x000142B5 File Offset: 0x000124B5
		private static LocalConstantHandle NextHandle(LocalConstantHandle handle)
		{
			return MetadataTokens.LocalConstantHandle(MetadataTokens.GetRowNumber(handle) + 1);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x000142CC File Offset: 0x000124CC
		private BlobHandle SerializeLocalConstantSignature(ILocalDefinition localConstant)
		{
			BlobBuilder blobBuilder = new BlobBuilder(256);
			CustomModifiersEncoder customModifiersEncoder = new CustomModifiersEncoder(blobBuilder);
			this.SerializeCustomModifiers(customModifiersEncoder, localConstant.CustomModifiers);
			ITypeReference type = localConstant.Type;
			PrimitiveTypeCode typeCode = type.TypeCode;
			object value = localConstant.CompileTimeValue.Value;
			if (value is decimal)
			{
				blobBuilder.WriteByte(17);
				blobBuilder.WriteCompressedInteger(CodedIndex.TypeDefOrRefOrSpec(this.GetTypeHandle(type, true)));
				blobBuilder.WriteDecimal((decimal)value);
			}
			else if (value is DateTime)
			{
				blobBuilder.WriteByte(17);
				blobBuilder.WriteCompressedInteger(CodedIndex.TypeDefOrRefOrSpec(this.GetTypeHandle(type, true)));
				blobBuilder.WriteDateTime((DateTime)value);
			}
			else if (typeCode == PrimitiveTypeCode.String)
			{
				blobBuilder.WriteByte(14);
				if (value == null)
				{
					blobBuilder.WriteByte(byte.MaxValue);
				}
				else
				{
					blobBuilder.WriteUTF16((string)value);
				}
			}
			else if (value != null)
			{
				blobBuilder.WriteByte((byte)MetadataWriter.GetConstantTypeCode(value));
				blobBuilder.WriteConstant(value);
				if (type.IsEnum)
				{
					blobBuilder.WriteCompressedInteger(CodedIndex.TypeDefOrRefOrSpec(this.GetTypeHandle(type, true)));
				}
			}
			else if (this.module.IsPlatformType(type, PlatformType.SystemObject))
			{
				blobBuilder.WriteByte(28);
			}
			else
			{
				blobBuilder.WriteByte(type.IsValueType ? 17 : 18);
				blobBuilder.WriteCompressedInteger(CodedIndex.TypeDefOrRefOrSpec(this.GetTypeHandle(type, true)));
			}
			return this._debugMetadataOpt.GetOrAddBlob(blobBuilder);
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00014434 File Offset: 0x00012634
		private static SignatureTypeCode GetConstantTypeCode(object value)
		{
			if (value == null)
			{
				return (SignatureTypeCode)18;
			}
			if (value.GetType() == typeof(int))
			{
				return SignatureTypeCode.Int32;
			}
			if (value.GetType() == typeof(string))
			{
				return SignatureTypeCode.String;
			}
			if (value.GetType() == typeof(bool))
			{
				return SignatureTypeCode.Boolean;
			}
			if (value.GetType() == typeof(char))
			{
				return SignatureTypeCode.Char;
			}
			if (value.GetType() == typeof(byte))
			{
				return SignatureTypeCode.Byte;
			}
			if (value.GetType() == typeof(long))
			{
				return SignatureTypeCode.Int64;
			}
			if (value.GetType() == typeof(double))
			{
				return SignatureTypeCode.Double;
			}
			if (value.GetType() == typeof(short))
			{
				return SignatureTypeCode.Int16;
			}
			if (value.GetType() == typeof(ushort))
			{
				return SignatureTypeCode.UInt16;
			}
			if (value.GetType() == typeof(uint))
			{
				return SignatureTypeCode.UInt32;
			}
			if (value.GetType() == typeof(sbyte))
			{
				return SignatureTypeCode.SByte;
			}
			if (value.GetType() == typeof(ulong))
			{
				return SignatureTypeCode.UInt64;
			}
			if (value.GetType() == typeof(float))
			{
				return SignatureTypeCode.Single;
			}
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00014598 File Offset: 0x00012798
		private void SerializeImport(BlobBuilder writer, AssemblyReferenceAlias alias)
		{
			writer.WriteByte(6);
			writer.WriteCompressedInteger(MetadataTokens.GetHeapOffset(this._debugMetadataOpt.GetOrAddBlobUTF8(alias.Name, true)));
			writer.WriteCompressedInteger(MetadataTokens.GetRowNumber(this.GetOrAddAssemblyReferenceHandle(alias.Assembly)));
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x000145E8 File Offset: 0x000127E8
		private void SerializeImport(BlobBuilder writer, UsedNamespaceOrType import)
		{
			if (import.TargetXmlNamespaceOpt != null)
			{
				writer.WriteByte(4);
				writer.WriteCompressedInteger(MetadataTokens.GetHeapOffset(this._debugMetadataOpt.GetOrAddBlobUTF8(import.AliasOpt, true)));
				writer.WriteCompressedInteger(MetadataTokens.GetHeapOffset(this._debugMetadataOpt.GetOrAddBlobUTF8(import.TargetXmlNamespaceOpt, true)));
				return;
			}
			if (import.TargetTypeOpt != null)
			{
				if (import.AliasOpt != null)
				{
					writer.WriteByte(9);
					writer.WriteCompressedInteger(MetadataTokens.GetHeapOffset(this._debugMetadataOpt.GetOrAddBlobUTF8(import.AliasOpt, true)));
				}
				else
				{
					writer.WriteByte(3);
				}
				writer.WriteCompressedInteger(CodedIndex.TypeDefOrRefOrSpec(this.GetTypeHandle(import.TargetTypeOpt, true)));
				return;
			}
			if (import.TargetNamespaceOpt != null)
			{
				if (import.TargetAssemblyOpt != null)
				{
					if (import.AliasOpt != null)
					{
						writer.WriteByte(8);
						writer.WriteCompressedInteger(MetadataTokens.GetHeapOffset(this._debugMetadataOpt.GetOrAddBlobUTF8(import.AliasOpt, true)));
					}
					else
					{
						writer.WriteByte(2);
					}
					writer.WriteCompressedInteger(MetadataTokens.GetRowNumber(this.GetAssemblyReferenceHandle(import.TargetAssemblyOpt)));
				}
				else if (import.AliasOpt != null)
				{
					writer.WriteByte(7);
					writer.WriteCompressedInteger(MetadataTokens.GetHeapOffset(this._debugMetadataOpt.GetOrAddBlobUTF8(import.AliasOpt, true)));
				}
				else
				{
					writer.WriteByte(1);
				}
				string text = TypeNameSerializer.BuildQualifiedNamespaceName(import.TargetNamespaceOpt);
				writer.WriteCompressedInteger(MetadataTokens.GetHeapOffset(this._debugMetadataOpt.GetOrAddBlobUTF8(text, true)));
				return;
			}
			writer.WriteByte(5);
			writer.WriteCompressedInteger(MetadataTokens.GetHeapOffset(this._debugMetadataOpt.GetOrAddBlobUTF8(import.AliasOpt, true)));
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0001477C File Offset: 0x0001297C
		private void DefineModuleImportScope()
		{
			BlobBuilder blobBuilder = new BlobBuilder(256);
			this.SerializeModuleDefaultNamespace();
			foreach (AssemblyReferenceAlias assemblyReferenceAlias in this.module.GetAssemblyReferenceAliases(this.Context))
			{
				this.SerializeImport(blobBuilder, assemblyReferenceAlias);
			}
			foreach (UsedNamespaceOrType usedNamespaceOrType in this.module.GetImports())
			{
				this.SerializeImport(blobBuilder, usedNamespaceOrType);
			}
			this._debugMetadataOpt.AddImportScope(default(ImportScopeHandle), this._debugMetadataOpt.GetOrAddBlob(blobBuilder));
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00014824 File Offset: 0x00012A24
		private ImportScopeHandle GetImportScopeIndex(IImportScope scope, Dictionary<IImportScope, ImportScopeHandle> scopeIndex)
		{
			ImportScopeHandle importScopeHandle;
			if (scopeIndex.TryGetValue(scope, ref importScopeHandle))
			{
				return importScopeHandle;
			}
			ImportScopeHandle importScopeHandle2 = ((scope.Parent != null) ? this.GetImportScopeIndex(scope.Parent, scopeIndex) : MetadataWriter.ModuleImportScopeHandle);
			ImportScopeHandle importScopeHandle3 = this._debugMetadataOpt.AddImportScope(importScopeHandle2, this.SerializeImportsBlob(scope));
			scopeIndex.Add(scope, importScopeHandle3);
			return importScopeHandle3;
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00014878 File Offset: 0x00012A78
		private BlobHandle SerializeImportsBlob(IImportScope scope)
		{
			BlobBuilder blobBuilder = new BlobBuilder(256);
			foreach (UsedNamespaceOrType usedNamespaceOrType in scope.GetUsedNamespaces())
			{
				this.SerializeImport(blobBuilder, usedNamespaceOrType);
			}
			return this._debugMetadataOpt.GetOrAddBlob(blobBuilder);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x000148C8 File Offset: 0x00012AC8
		private void SerializeModuleDefaultNamespace()
		{
			if (this.module.DefaultNamespace == null)
			{
				return;
			}
			this._debugMetadataOpt.AddCustomDebugInformation(EntityHandle.ModuleDefinition, this._debugMetadataOpt.GetOrAddGuid(PortableCustomDebugInfoKinds.DefaultNamespace), this._debugMetadataOpt.GetOrAddBlobUTF8(this.module.DefaultNamespace, true));
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00014920 File Offset: 0x00012B20
		private void SerializeLocalInfo(ILocalDefinition local, EntityHandle parent)
		{
			ImmutableArray<bool> dynamicTransformFlags = local.DynamicTransformFlags;
			if (!dynamicTransformFlags.IsEmpty)
			{
				ImmutableArray<byte> immutableArray = MetadataWriter.SerializeBitVector(dynamicTransformFlags);
				this._debugMetadataOpt.AddCustomDebugInformation(parent, this._debugMetadataOpt.GetOrAddGuid(PortableCustomDebugInfoKinds.DynamicLocalVariables), this._debugMetadataOpt.GetOrAddBlob(immutableArray));
			}
			ImmutableArray<string> tupleElementNames = local.TupleElementNames;
			if (!tupleElementNames.IsEmpty)
			{
				BlobBuilder blobBuilder = new BlobBuilder(256);
				MetadataWriter.SerializeTupleElementNames(blobBuilder, tupleElementNames);
				this._debugMetadataOpt.AddCustomDebugInformation(parent, this._debugMetadataOpt.GetOrAddGuid(PortableCustomDebugInfoKinds.TupleElementNames), this._debugMetadataOpt.GetOrAddBlob(blobBuilder));
			}
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x000149B8 File Offset: 0x00012BB8
		private static ImmutableArray<byte> SerializeBitVector(ImmutableArray<bool> vector)
		{
			ArrayBuilder<byte> instance = ArrayBuilder<byte>.GetInstance();
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < vector.Length; i++)
			{
				if (vector[i])
				{
					num |= 1 << num2;
				}
				if (num2 == 7)
				{
					instance.Add((byte)num);
					num = 0;
					num2 = 0;
				}
				else
				{
					num2++;
				}
			}
			if (num != 0)
			{
				instance.Add((byte)num);
			}
			else
			{
				int num3 = instance.Count - 1;
				while (instance[num3] == 0)
				{
					num3--;
				}
				instance.Clip(num3 + 1);
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00014A44 File Offset: 0x00012C44
		private static void SerializeTupleElementNames(BlobBuilder builder, ImmutableArray<string> names)
		{
			foreach (string text in names)
			{
				MetadataWriter.WriteUtf8String(builder, text ?? string.Empty);
			}
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00014A7C File Offset: 0x00012C7C
		private static void WriteUtf8String(BlobBuilder builder, string str)
		{
			builder.WriteUTF8(str, true);
			builder.WriteByte(0);
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00014A90 File Offset: 0x00012C90
		private void SerializeAsyncMethodSteppingInfo(AsyncMoveNextBodyDebugInfo asyncInfo, MethodDefinitionHandle moveNextMethod)
		{
			BlobBuilder blobBuilder = new BlobBuilder(256);
			blobBuilder.WriteUInt32((uint)((long)asyncInfo.CatchHandlerOffset + 1L));
			for (int i = 0; i < asyncInfo.ResumeOffsets.Length; i++)
			{
				blobBuilder.WriteUInt32((uint)asyncInfo.YieldOffsets[i]);
				blobBuilder.WriteUInt32((uint)asyncInfo.ResumeOffsets[i]);
				blobBuilder.WriteCompressedInteger(MetadataTokens.GetRowNumber(moveNextMethod));
			}
			this._debugMetadataOpt.AddCustomDebugInformation(moveNextMethod, this._debugMetadataOpt.GetOrAddGuid(PortableCustomDebugInfoKinds.AsyncMethodSteppingInformationBlob), this._debugMetadataOpt.GetOrAddBlob(blobBuilder));
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00014B3C File Offset: 0x00012D3C
		private void SerializeStateMachineLocalScopes(IMethodBody methodBody, MethodDefinitionHandle method)
		{
			ImmutableArray<StateMachineHoistedLocalScope> stateMachineHoistedLocalScopes = methodBody.StateMachineHoistedLocalScopes;
			if (stateMachineHoistedLocalScopes.IsDefaultOrEmpty)
			{
				return;
			}
			BlobBuilder blobBuilder = new BlobBuilder(256);
			foreach (StateMachineHoistedLocalScope stateMachineHoistedLocalScope in stateMachineHoistedLocalScopes)
			{
				blobBuilder.WriteUInt32((uint)stateMachineHoistedLocalScope.StartOffset);
				blobBuilder.WriteUInt32((uint)stateMachineHoistedLocalScope.Length);
			}
			this._debugMetadataOpt.AddCustomDebugInformation(method, this._debugMetadataOpt.GetOrAddGuid(PortableCustomDebugInfoKinds.StateMachineHoistedLocalScopes), this._debugMetadataOpt.GetOrAddBlob(blobBuilder));
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00014BC8 File Offset: 0x00012DC8
		private BlobHandle SerializeSequencePoints(StandaloneSignatureHandle localSignatureHandleOpt, ImmutableArray<SequencePoint> sequencePoints, Dictionary<DebugSourceDocument, DocumentHandle> documentIndex, out DocumentHandle singleDocumentHandle)
		{
			if (sequencePoints.Length == 0)
			{
				singleDocumentHandle = default(DocumentHandle);
				return default(BlobHandle);
			}
			BlobBuilder blobBuilder = new BlobBuilder(256);
			int num = -1;
			int num2 = -1;
			blobBuilder.WriteCompressedInteger(MetadataTokens.GetRowNumber(localSignatureHandleOpt));
			DebugSourceDocument debugSourceDocument = MetadataWriter.TryGetSingleDocument(sequencePoints);
			singleDocumentHandle = ((debugSourceDocument != null) ? this.GetOrAddDocument(debugSourceDocument, documentIndex) : default(DocumentHandle));
			for (int i = 0; i < sequencePoints.Length; i++)
			{
				DebugSourceDocument document = sequencePoints[i].Document;
				if (debugSourceDocument != document)
				{
					DocumentHandle orAddDocument = this.GetOrAddDocument(document, documentIndex);
					if (debugSourceDocument != null)
					{
						blobBuilder.WriteCompressedInteger(0);
					}
					blobBuilder.WriteCompressedInteger(MetadataTokens.GetRowNumber(orAddDocument));
					debugSourceDocument = document;
				}
				if (i > 0)
				{
					blobBuilder.WriteCompressedInteger(sequencePoints[i].Offset - sequencePoints[i - 1].Offset);
				}
				else
				{
					blobBuilder.WriteCompressedInteger(sequencePoints[i].Offset);
				}
				if (sequencePoints[i].IsHidden)
				{
					blobBuilder.WriteInt16(0);
				}
				else
				{
					this.SerializeDeltaLinesAndColumns(blobBuilder, sequencePoints[i]);
					if (num < 0)
					{
						blobBuilder.WriteCompressedInteger(sequencePoints[i].StartLine);
						blobBuilder.WriteCompressedInteger((int)sequencePoints[i].StartColumn);
					}
					else
					{
						blobBuilder.WriteCompressedSignedInteger(sequencePoints[i].StartLine - num);
						blobBuilder.WriteCompressedSignedInteger((int)sequencePoints[i].StartColumn - num2);
					}
					num = sequencePoints[i].StartLine;
					num2 = (int)sequencePoints[i].StartColumn;
				}
			}
			return this._debugMetadataOpt.GetOrAddBlob(blobBuilder);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00014D88 File Offset: 0x00012F88
		private static DebugSourceDocument TryGetSingleDocument(ImmutableArray<SequencePoint> sequencePoints)
		{
			DebugSourceDocument document = sequencePoints[0].Document;
			for (int i = 1; i < sequencePoints.Length; i++)
			{
				if (sequencePoints[i].Document != document)
				{
					return null;
				}
			}
			return document;
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00014DC8 File Offset: 0x00012FC8
		private void SerializeDeltaLinesAndColumns(BlobBuilder writer, SequencePoint sequencePoint)
		{
			int num = sequencePoint.EndLine - sequencePoint.StartLine;
			int num2 = (int)(sequencePoint.EndColumn - sequencePoint.StartColumn);
			writer.WriteCompressedInteger(num);
			if (num == 0)
			{
				writer.WriteCompressedInteger(num2);
				return;
			}
			writer.WriteCompressedSignedInteger(num2);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00014E0C File Offset: 0x0001300C
		private DocumentHandle GetOrAddDocument(DebugSourceDocument document, Dictionary<DebugSourceDocument, DocumentHandle> index)
		{
			DocumentHandle documentHandle;
			if (index.TryGetValue(document, ref documentHandle))
			{
				return documentHandle;
			}
			return this.AddDocument(document, index);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00014E30 File Offset: 0x00013030
		private DocumentHandle AddDocument(DebugSourceDocument document, Dictionary<DebugSourceDocument, DocumentHandle> index)
		{
			DebugSourceInfo sourceInfo = document.GetSourceInfo();
			string text = document.Location;
			if (this._usingNonSourceDocumentNameEnumerator)
			{
				this._nonSourceDocumentNameEnumerator.MoveNext();
				text = this._nonSourceDocumentNameEnumerator.Current;
			}
			DocumentHandle documentHandle = this._debugMetadataOpt.AddDocument(this._debugMetadataOpt.GetOrAddDocumentName(text), sourceInfo.Checksum.IsDefault ? default(GuidHandle) : this._debugMetadataOpt.GetOrAddGuid(sourceInfo.ChecksumAlgorithmId), sourceInfo.Checksum.IsDefault ? default(BlobHandle) : this._debugMetadataOpt.GetOrAddBlob(sourceInfo.Checksum), this._debugMetadataOpt.GetOrAddGuid(document.Language));
			index.Add(document, documentHandle);
			if (new ImmutableArray<byte>?(sourceInfo.EmbeddedTextBlob) != default(ImmutableArray<byte>?))
			{
				this._debugMetadataOpt.AddCustomDebugInformation(documentHandle, this._debugMetadataOpt.GetOrAddGuid(PortableCustomDebugInfoKinds.EmbeddedSource), this._debugMetadataOpt.GetOrAddBlob(sourceInfo.EmbeddedTextBlob));
			}
			return documentHandle;
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00014F48 File Offset: 0x00013148
		public void AddRemainingDebugDocuments(IReadOnlyDictionary<string, DebugSourceDocument> documents)
		{
			foreach (KeyValuePair<string, DebugSourceDocument> keyValuePair in Enumerable.OrderBy<KeyValuePair<string, DebugSourceDocument>, string>(Enumerable.Where<KeyValuePair<string, DebugSourceDocument>>(documents, (KeyValuePair<string, DebugSourceDocument> kvp) => !this._documentIndex.ContainsKey(kvp.Value)), (KeyValuePair<string, DebugSourceDocument> kvp) => kvp.Key))
			{
				this.AddDocument(keyValuePair.Value, this._documentIndex);
			}
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00014FD4 File Offset: 0x000131D4
		private void SerializeEncMethodDebugInformation(IMethodBody methodBody, MethodDefinitionHandle method)
		{
			EditAndContinueMethodDebugInformation encMethodDebugInfo = MetadataWriter.GetEncMethodDebugInfo(methodBody);
			if (!encMethodDebugInfo.LocalSlots.IsDefaultOrEmpty)
			{
				BlobBuilder blobBuilder = new BlobBuilder(256);
				encMethodDebugInfo.SerializeLocalSlots(blobBuilder);
				this._debugMetadataOpt.AddCustomDebugInformation(method, this._debugMetadataOpt.GetOrAddGuid(PortableCustomDebugInfoKinds.EncLocalSlotMap), this._debugMetadataOpt.GetOrAddBlob(blobBuilder));
			}
			if (!encMethodDebugInfo.Lambdas.IsDefaultOrEmpty)
			{
				BlobBuilder blobBuilder2 = new BlobBuilder(256);
				encMethodDebugInfo.SerializeLambdaMap(blobBuilder2);
				this._debugMetadataOpt.AddCustomDebugInformation(method, this._debugMetadataOpt.GetOrAddGuid(PortableCustomDebugInfoKinds.EncLambdaAndClosureMap), this._debugMetadataOpt.GetOrAddBlob(blobBuilder2));
			}
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00015090 File Offset: 0x00013290
		private void EmbedSourceLink(Stream stream)
		{
			byte[] array;
			try
			{
				array = stream.ReadAllBytes();
			}
			catch (Exception ex) when (!(ex is OperationCanceledException))
			{
				throw new SymUnmanagedWriterException(ex.Message, ex);
			}
			this._debugMetadataOpt.AddCustomDebugInformation(EntityHandle.ModuleDefinition, this._debugMetadataOpt.GetOrAddGuid(PortableCustomDebugInfoKinds.SourceLink), this._debugMetadataOpt.GetOrAddBlob(array));
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00015114 File Offset: 0x00013314
		private void EmbedCompilationOptions(CommonPEModuleBuilder module)
		{
			MetadataWriter.<>c__DisplayClass300_0 CS$<>8__locals1;
			CS$<>8__locals1.builder = new BlobBuilder(256);
			RebuildData rebuildData = this.Context.RebuildData;
			if (rebuildData != null)
			{
				BlobReader optionsBlobReader = rebuildData.OptionsBlobReader;
				CS$<>8__locals1.builder.WriteBytes(optionsBlobReader.ReadBytes(optionsBlobReader.RemainingBytes));
			}
			else
			{
				string informationalVersion = CustomAttributeExtensions.GetCustomAttribute<AssemblyInformationalVersionAttribute>(typeof(Compilation).Assembly).InformationalVersion;
				MetadataWriter.<EmbedCompilationOptions>g__WriteValue|300_0("version", 2.ToString(), ref CS$<>8__locals1);
				MetadataWriter.<EmbedCompilationOptions>g__WriteValue|300_0("compiler-version", informationalVersion, ref CS$<>8__locals1);
				MetadataWriter.<EmbedCompilationOptions>g__WriteValue|300_0("language", module.CommonCompilation.Options.Language, ref CS$<>8__locals1);
				MetadataWriter.<EmbedCompilationOptions>g__WriteValue|300_0("source-file-count", Enumerable.Count<SyntaxTree>(module.CommonCompilation.SyntaxTrees).ToString(), ref CS$<>8__locals1);
				MetadataWriter.<EmbedCompilationOptions>g__WriteValue|300_0("output-kind", module.OutputKind.ToString(), ref CS$<>8__locals1);
				if (module.EmitOptions.FallbackSourceFileEncoding != null)
				{
					MetadataWriter.<EmbedCompilationOptions>g__WriteValue|300_0("fallback-encoding", module.EmitOptions.FallbackSourceFileEncoding.WebName, ref CS$<>8__locals1);
				}
				if (module.EmitOptions.DefaultSourceFileEncoding != null)
				{
					MetadataWriter.<EmbedCompilationOptions>g__WriteValue|300_0("default-encoding", module.EmitOptions.DefaultSourceFileEncoding.WebName, ref CS$<>8__locals1);
				}
				int num = 0;
				DesktopAssemblyIdentityComparer desktopAssemblyIdentityComparer = module.CommonCompilation.Options.AssemblyIdentityComparer as DesktopAssemblyIdentityComparer;
				if (desktopAssemblyIdentityComparer != null)
				{
					num |= (desktopAssemblyIdentityComparer.PortabilityPolicy.SuppressSilverlightLibraryAssembliesPortability ? 1 : 0);
					num |= (desktopAssemblyIdentityComparer.PortabilityPolicy.SuppressSilverlightPlatformAssembliesPortability ? 2 : 0);
				}
				if (num != 0)
				{
					MetadataWriter.<EmbedCompilationOptions>g__WriteValue|300_0("portability-policy", num.ToString(), ref CS$<>8__locals1);
				}
				OptimizationLevel optimizationLevel = module.CommonCompilation.Options.OptimizationLevel;
				bool debugPlusMode = module.CommonCompilation.Options.DebugPlusMode;
				OptimizationLevel optimizationLevel2 = optimizationLevel;
				bool flag = debugPlusMode;
				ValueTuple<OptimizationLevel, bool> defaultValues = OptimizationLevelFacts.DefaultValues;
				if (optimizationLevel2 != defaultValues.Item1 || flag != defaultValues.Item2)
				{
					MetadataWriter.<EmbedCompilationOptions>g__WriteValue|300_0("optimization", optimizationLevel.ToPdbSerializedString(debugPlusMode), ref CS$<>8__locals1);
				}
				MetadataWriter.<EmbedCompilationOptions>g__WriteValue|300_0("platform", module.CommonCompilation.Options.Platform.ToString(), ref CS$<>8__locals1);
				AssemblyInformationalVersionAttribute customAttribute = CustomAttributeExtensions.GetCustomAttribute<AssemblyInformationalVersionAttribute>(typeof(object).Assembly);
				string text = ((customAttribute != null) ? customAttribute.InformationalVersion : null);
				MetadataWriter.<EmbedCompilationOptions>g__WriteValue|300_0("runtime-version", text, ref CS$<>8__locals1);
				module.CommonCompilation.SerializePdbEmbeddedCompilationOptions(CS$<>8__locals1.builder);
			}
			this._debugMetadataOpt.AddCustomDebugInformation(EntityHandle.ModuleDefinition, this._debugMetadataOpt.GetOrAddGuid(PortableCustomDebugInfoKinds.CompilationOptions), this._debugMetadataOpt.GetOrAddBlob(CS$<>8__locals1.builder));
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x000153B4 File Offset: 0x000135B4
		private void EmbedMetadataReferenceInformation(CommonPEModuleBuilder module)
		{
			BlobBuilder blobBuilder = new BlobBuilder(256);
			CommonReferenceManager boundReferenceManager = module.CommonCompilation.GetBoundReferenceManager();
			foreach (ValueTuple<IAssemblySymbolInternal, ImmutableArray<string>> valueTuple in boundReferenceManager.GetReferencedAssemblyAliases())
			{
				PortableExecutableReference portableExecutableReference = boundReferenceManager.GetMetadataReference(valueTuple.Item1) as PortableExecutableReference;
				if (portableExecutableReference != null && portableExecutableReference.FilePath != null)
				{
					string fileName = PathUtilities.GetFileName(portableExecutableReference.FilePath, true);
					IAssemblySymbol assemblySymbol = valueTuple.Item1.GetISymbol() as IAssemblySymbol;
					PEReader pereader = ((assemblySymbol != null) ? assemblySymbol.GetMetadata().GetAssembly().ManifestModule.PEReaderOpt : null);
					if (pereader != null)
					{
						blobBuilder.WriteUTF8(fileName, true);
						blobBuilder.WriteByte(0);
						ImmutableArray<string> item = valueTuple.Item2;
						if (item.Length > 0)
						{
							blobBuilder.WriteUTF8(string.Join(",", valueTuple.Item2.OrderBy<string>(StringComparer.Ordinal)), true);
						}
						blobBuilder.WriteByte(0);
						byte b = (portableExecutableReference.Properties.EmbedInteropTypes ? 2 : 0);
						int num = (int)b;
						MetadataImageKind kind = portableExecutableReference.Properties.Kind;
						int num2;
						if (kind != MetadataImageKind.Assembly)
						{
							if (kind != MetadataImageKind.Module)
							{
								throw ExceptionUtilities.UnexpectedValue(portableExecutableReference.Properties.Kind);
							}
							num2 = 0;
						}
						else
						{
							num2 = 1;
						}
						b = (byte)(num | num2);
						blobBuilder.WriteByte(b);
						blobBuilder.WriteInt32(pereader.PEHeaders.CoffHeader.TimeDateStamp);
						blobBuilder.WriteInt32(pereader.PEHeaders.PEHeader.SizeOfImage);
						MetadataReader metadataReader = pereader.GetMetadataReader();
						blobBuilder.WriteGuid(metadataReader.GetGuid(metadataReader.GetModuleDefinition().Mvid));
					}
				}
			}
			this._debugMetadataOpt.AddCustomDebugInformation(EntityHandle.ModuleDefinition, this._debugMetadataOpt.GetOrAddGuid(PortableCustomDebugInfoKinds.CompilationMetadataReferences), this._debugMetadataOpt.GetOrAddBlob(blobBuilder));
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x000155D4 File Offset: 0x000137D4
		// Note: this type is marked as 'beforefieldinit'.
		static MetadataWriter()
		{
			string[,] array = new string[2, 2];
			array[0, 0] = "";
			array[0, 1] = "M";
			array[1, 0] = "S";
			array[1, 1] = "SM";
			MetadataWriter.dummyAssemblyAttributeParentQualifier = array;
			MetadataWriter.ModuleImportScopeHandle = MetadataTokens.ImportScopeHandle(1);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x000156EC File Offset: 0x000138EC
		[CompilerGenerated]
		internal static void <EmbedCompilationOptions>g__WriteValue|300_0(string key, string value, ref MetadataWriter.<>c__DisplayClass300_0 A_2)
		{
			A_2.builder.WriteUTF8(key, true);
			A_2.builder.WriteByte(0);
			A_2.builder.WriteUTF8(value, true);
			A_2.builder.WriteByte(0);
		}

		// Token: 0x040001AD RID: 429
		internal static readonly Encoding s_utf8Encoding = Encoding.UTF8;

		// Token: 0x040001AE RID: 430
		internal const int NameLengthLimit = 1023;

		// Token: 0x040001AF RID: 431
		internal const int PathLengthLimit = 259;

		// Token: 0x040001B0 RID: 432
		internal const int PdbLengthLimit = 2046;

		// Token: 0x040001B1 RID: 433
		private readonly int _numTypeDefsEstimate;

		// Token: 0x040001B2 RID: 434
		private readonly bool _deterministic;

		// Token: 0x040001B3 RID: 435
		internal readonly bool MetadataOnly;

		// Token: 0x040001B4 RID: 436
		internal readonly bool EmitTestCoverageData;

		// Token: 0x040001B5 RID: 437
		private readonly Dictionary<ValueTuple<ImmutableArray<byte>, bool>, int> _smallMethodBodies;

		// Token: 0x040001B6 RID: 438
		private const byte TinyFormat = 2;

		// Token: 0x040001B7 RID: 439
		private const int ThrowNullCodeSize = 2;

		// Token: 0x040001B8 RID: 440
		private static readonly ImmutableArray<byte> ThrowNullEncodedBody = ImmutableArray.Create<byte>(10, 20, 122);

		// Token: 0x040001B9 RID: 441
		private readonly CancellationToken _cancellationToken;

		// Token: 0x040001BA RID: 442
		protected readonly CommonPEModuleBuilder module;

		// Token: 0x040001BB RID: 443
		public readonly EmitContext Context;

		// Token: 0x040001BC RID: 444
		protected readonly CommonMessageProvider messageProvider;

		// Token: 0x040001BD RID: 445
		private bool _tableIndicesAreComplete;

		// Token: 0x040001BE RID: 446
		private bool _usingNonSourceDocumentNameEnumerator;

		// Token: 0x040001BF RID: 447
		private ImmutableArray<string>.Enumerator _nonSourceDocumentNameEnumerator;

		// Token: 0x040001C0 RID: 448
		private EntityHandle[] _pseudoSymbolTokenToTokenMap;

		// Token: 0x040001C1 RID: 449
		private object[] _pseudoSymbolTokenToReferenceMap;

		// Token: 0x040001C2 RID: 450
		private UserStringHandle[] _pseudoStringTokenToTokenMap;

		// Token: 0x040001C3 RID: 451
		private bool _userStringTokenOverflow;

		// Token: 0x040001C4 RID: 452
		private List<string> _pseudoStringTokenToStringMap;

		// Token: 0x040001C5 RID: 453
		private ReferenceIndexer _referenceVisitor;

		// Token: 0x040001C6 RID: 454
		protected readonly MetadataBuilder metadata;

		// Token: 0x040001C7 RID: 455
		protected readonly MetadataBuilder _debugMetadataOpt;

		// Token: 0x040001C8 RID: 456
		private readonly DynamicAnalysisDataWriter _dynamicAnalysisDataWriterOpt;

		// Token: 0x040001C9 RID: 457
		private readonly Dictionary<ICustomAttribute, BlobHandle> _customAttributeSignatureIndex = new Dictionary<ICustomAttribute, BlobHandle>();

		// Token: 0x040001CA RID: 458
		private readonly Dictionary<ITypeReference, BlobHandle> _typeSpecSignatureIndex = new Dictionary<ITypeReference, BlobHandle>(ReferenceEqualityComparer.Instance);

		// Token: 0x040001CB RID: 459
		private readonly Dictionary<string, int> _fileRefIndex = new Dictionary<string, int>(32);

		// Token: 0x040001CC RID: 460
		private readonly List<IFileReference> _fileRefList = new List<IFileReference>(32);

		// Token: 0x040001CD RID: 461
		private readonly Dictionary<IFieldReference, BlobHandle> _fieldSignatureIndex = new Dictionary<IFieldReference, BlobHandle>(ReferenceEqualityComparer.Instance);

		// Token: 0x040001CE RID: 462
		private readonly Dictionary<ISignature, KeyValuePair<BlobHandle, ImmutableArray<byte>>> _signatureIndex;

		// Token: 0x040001CF RID: 463
		private readonly Dictionary<IMarshallingInformation, BlobHandle> _marshallingDescriptorIndex = new Dictionary<IMarshallingInformation, BlobHandle>();

		// Token: 0x040001D0 RID: 464
		protected readonly List<MethodImplementation> methodImplList = new List<MethodImplementation>();

		// Token: 0x040001D1 RID: 465
		private readonly Dictionary<IGenericMethodInstanceReference, BlobHandle> _methodInstanceSignatureIndex = new Dictionary<IGenericMethodInstanceReference, BlobHandle>(ReferenceEqualityComparer.Instance);

		// Token: 0x040001D2 RID: 466
		internal const string dummyAssemblyAttributeParentNamespace = "System.Runtime.CompilerServices";

		// Token: 0x040001D3 RID: 467
		internal const string dummyAssemblyAttributeParentName = "AssemblyAttributesGoHere";

		// Token: 0x040001D4 RID: 468
		internal static readonly string[,] dummyAssemblyAttributeParentQualifier;

		// Token: 0x040001D5 RID: 469
		private readonly TypeReferenceHandle[,] _dummyAssemblyAttributeParent = new TypeReferenceHandle[2, 2];

		// Token: 0x040001D6 RID: 470
		internal const uint LiteralMethodDefinitionToken = 2147483648U;

		// Token: 0x040001D7 RID: 471
		internal const uint LiteralGreatestMethodDefinitionToken = 1073741824U;

		// Token: 0x040001D8 RID: 472
		internal const uint SourceDocumentIndex = 536870912U;

		// Token: 0x040001D9 RID: 473
		internal const uint ModuleVersionIdStringToken = 2147483648U;

		// Token: 0x040001DA RID: 474
		private readonly Dictionary<DebugSourceDocument, DocumentHandle> _documentIndex = new Dictionary<DebugSourceDocument, DocumentHandle>();

		// Token: 0x040001DB RID: 475
		private readonly Dictionary<IImportScope, ImportScopeHandle> _scopeIndex = new Dictionary<IImportScope, ImportScopeHandle>(MetadataWriter.ImportScopeEqualityComparer.Instance);

		// Token: 0x040001DC RID: 476
		private static readonly ImportScopeHandle ModuleImportScopeHandle;

		// Token: 0x040001DD RID: 477
		private const int CompilationOptionsSchemaVersion = 2;

		// Token: 0x02000521 RID: 1313
		protected abstract class HeapOrReferenceIndexBase<T>
		{
			// Token: 0x06003352 RID: 13138 RVA: 0x00091553 File Offset: 0x0008F753
			protected HeapOrReferenceIndexBase(MetadataWriter writer, int lastRowId)
			{
				this._writer = writer;
				this._rows = new List<T>();
				this._firstRowId = lastRowId + 1;
			}

			// Token: 0x06003353 RID: 13139
			public abstract bool TryGetValue(T item, out int index);

			// Token: 0x06003354 RID: 13140 RVA: 0x00091578 File Offset: 0x0008F778
			public int GetOrAdd(T item)
			{
				int num;
				if (!this.TryGetValue(item, out num))
				{
					num = this.Add(item);
				}
				return num;
			}

			// Token: 0x17000EBC RID: 3772
			// (get) Token: 0x06003355 RID: 13141 RVA: 0x00091599 File Offset: 0x0008F799
			public IReadOnlyList<T> Rows
			{
				get
				{
					return this._rows;
				}
			}

			// Token: 0x06003356 RID: 13142 RVA: 0x000915A4 File Offset: 0x0008F7A4
			public int Add(T item)
			{
				int num = this._firstRowId + this._rows.Count;
				this.AddItem(item, num);
				this._rows.Add(item);
				return num;
			}

			// Token: 0x06003357 RID: 13143
			protected abstract void AddItem(T item, int index);

			// Token: 0x0400149B RID: 5275
			private readonly MetadataWriter _writer;

			// Token: 0x0400149C RID: 5276
			private readonly List<T> _rows;

			// Token: 0x0400149D RID: 5277
			private readonly int _firstRowId;
		}

		// Token: 0x02000522 RID: 1314
		protected sealed class HeapOrReferenceIndex<T> : MetadataWriter.HeapOrReferenceIndexBase<T>
		{
			// Token: 0x06003358 RID: 13144 RVA: 0x000915D9 File Offset: 0x0008F7D9
			public HeapOrReferenceIndex(MetadataWriter writer, int lastRowId = 0)
				: this(writer, new Dictionary<T, int>(), lastRowId)
			{
			}

			// Token: 0x06003359 RID: 13145 RVA: 0x000915E8 File Offset: 0x0008F7E8
			private HeapOrReferenceIndex(MetadataWriter writer, Dictionary<T, int> index, int lastRowId)
				: base(writer, lastRowId)
			{
				this._index = index;
			}

			// Token: 0x0600335A RID: 13146 RVA: 0x000915F9 File Offset: 0x0008F7F9
			public override bool TryGetValue(T item, out int index)
			{
				return this._index.TryGetValue(item, ref index);
			}

			// Token: 0x0600335B RID: 13147 RVA: 0x00091608 File Offset: 0x0008F808
			protected override void AddItem(T item, int index)
			{
				this._index.Add(item, index);
			}

			// Token: 0x0400149E RID: 5278
			private readonly Dictionary<T, int> _index;
		}

		// Token: 0x02000523 RID: 1315
		protected sealed class TypeReferenceIndex : MetadataWriter.HeapOrReferenceIndexBase<ITypeReference>
		{
			// Token: 0x0600335C RID: 13148 RVA: 0x00091617 File Offset: 0x0008F817
			public TypeReferenceIndex(MetadataWriter writer, int lastRowId = 0)
				: this(writer, new Dictionary<ITypeReference, int>(ReferenceEqualityComparer.Instance), lastRowId)
			{
			}

			// Token: 0x0600335D RID: 13149 RVA: 0x0009162B File Offset: 0x0008F82B
			private TypeReferenceIndex(MetadataWriter writer, Dictionary<ITypeReference, int> index, int lastRowId)
				: base(writer, lastRowId)
			{
				this._index = index;
			}

			// Token: 0x0600335E RID: 13150 RVA: 0x0009163C File Offset: 0x0008F83C
			public override bool TryGetValue(ITypeReference item, out int index)
			{
				return this._index.TryGetValue(item, ref index);
			}

			// Token: 0x0600335F RID: 13151 RVA: 0x0009164B File Offset: 0x0008F84B
			protected override void AddItem(ITypeReference item, int index)
			{
				this._index.Add(item, index);
			}

			// Token: 0x0400149F RID: 5279
			private readonly Dictionary<ITypeReference, int> _index;
		}

		// Token: 0x02000524 RID: 1316
		protected sealed class InstanceAndStructuralReferenceIndex<T> : MetadataWriter.HeapOrReferenceIndexBase<T> where T : class, IReference
		{
			// Token: 0x06003360 RID: 13152 RVA: 0x0009165A File Offset: 0x0008F85A
			public InstanceAndStructuralReferenceIndex(MetadataWriter writer, IEqualityComparer<T> structuralComparer, int lastRowId = 0)
				: base(writer, lastRowId)
			{
				this._instanceIndex = new Dictionary<T, int>(ReferenceEqualityComparer.Instance);
				this._structuralIndex = new Dictionary<T, int>(structuralComparer);
			}

			// Token: 0x06003361 RID: 13153 RVA: 0x00091680 File Offset: 0x0008F880
			public override bool TryGetValue(T item, out int index)
			{
				if (this._instanceIndex.TryGetValue(item, ref index))
				{
					return true;
				}
				if (this._structuralIndex.TryGetValue(item, ref index))
				{
					this._instanceIndex.Add(item, index);
					return true;
				}
				return false;
			}

			// Token: 0x06003362 RID: 13154 RVA: 0x000916B3 File Offset: 0x0008F8B3
			protected override void AddItem(T item, int index)
			{
				this._instanceIndex.Add(item, index);
				this._structuralIndex.Add(item, index);
			}

			// Token: 0x040014A0 RID: 5280
			private readonly Dictionary<T, int> _instanceIndex;

			// Token: 0x040014A1 RID: 5281
			private readonly Dictionary<T, int> _structuralIndex;
		}

		// Token: 0x02000525 RID: 1317
		private class ByteSequenceBoolTupleComparer : IEqualityComparer<ValueTuple<ImmutableArray<byte>, bool>>
		{
			// Token: 0x06003363 RID: 13155 RVA: 0x000916CF File Offset: 0x0008F8CF
			private ByteSequenceBoolTupleComparer()
			{
			}

			// Token: 0x06003364 RID: 13156 RVA: 0x000916D7 File Offset: 0x0008F8D7
			bool IEqualityComparer<ValueTuple<ImmutableArray<byte>, bool>>.Equals(ValueTuple<ImmutableArray<byte>, bool> x, ValueTuple<ImmutableArray<byte>, bool> y)
			{
				return x.Item2 == y.Item2 && ByteSequenceComparer.Equals(x.Item1, y.Item1);
			}

			// Token: 0x06003365 RID: 13157 RVA: 0x000916FA File Offset: 0x0008F8FA
			int IEqualityComparer<ValueTuple<ImmutableArray<byte>, bool>>.GetHashCode(ValueTuple<ImmutableArray<byte>, bool> x)
			{
				return Hash.Combine(ByteSequenceComparer.GetHashCode(x.Item1), x.Item2.GetHashCode());
			}

			// Token: 0x040014A2 RID: 5282
			internal static readonly MetadataWriter.ByteSequenceBoolTupleComparer Instance = new MetadataWriter.ByteSequenceBoolTupleComparer();
		}

		// Token: 0x02000526 RID: 1318
		internal sealed class ImportScopeEqualityComparer : IEqualityComparer<IImportScope>
		{
			// Token: 0x06003367 RID: 13159 RVA: 0x00091724 File Offset: 0x0008F924
			public bool Equals(IImportScope x, IImportScope y)
			{
				return x == y || (x != null && y != null && this.Equals(x.Parent, y.Parent) && x.GetUsedNamespaces().SequenceEqual<UsedNamespaceOrType, UsedNamespaceOrType>(y.GetUsedNamespaces(), null));
			}

			// Token: 0x06003368 RID: 13160 RVA: 0x0009175A File Offset: 0x0008F95A
			public int GetHashCode(IImportScope obj)
			{
				return Hash.Combine(Hash.CombineValues<UsedNamespaceOrType>(obj.GetUsedNamespaces(), int.MaxValue), (obj.Parent != null) ? this.GetHashCode(obj.Parent) : 0);
			}

			// Token: 0x040014A3 RID: 5283
			public static readonly MetadataWriter.ImportScopeEqualityComparer Instance = new MetadataWriter.ImportScopeEqualityComparer();
		}
	}
}
