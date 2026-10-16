using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection.Internal;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	/// <summary>The MetadataBuilder class writes metadata for an assembly in a highly performant manner. It is designed for use by compilers and other assembly generation tools.</summary>
	// Token: 0x020000E0 RID: 224
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MetadataBuilder
	{
		// Token: 0x06000809 RID: 2057 RVA: 0x000133EC File Offset: 0x000115EC
		[NullableContext(0)]
		[return: Nullable(1)]
		internal SerializedMetadata GetSerializedMetadata(ImmutableArray<int> externalRowCounts, int metadataVersionByteCount, bool isStandaloneDebugMetadata)
		{
			MetadataBuilder.HeapBlobBuilder heapBlobBuilder = new MetadataBuilder.HeapBlobBuilder(this._stringHeapCapacity);
			ImmutableArray<int> immutableArray = MetadataBuilder.SerializeStringHeap(heapBlobBuilder, this._strings, this._stringHeapStartOffset);
			ImmutableArray<int> immutableArray2 = ImmutableArray.Create<int>(this._userStringBuilder.Count, heapBlobBuilder.Count, this._blobHeapSize, this._guidBuilder.Count);
			MetadataSizes metadataSizes = new MetadataSizes(this.GetRowCounts(), externalRowCounts, immutableArray2, metadataVersionByteCount, isStandaloneDebugMetadata);
			return new SerializedMetadata(metadataSizes, heapBlobBuilder, immutableArray);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00013458 File Offset: 0x00011658
		internal static void SerializeMetadataHeader(BlobBuilder builder, string metadataVersion, MetadataSizes sizes)
		{
			int count = builder.Count;
			builder.WriteUInt32(1112167234U);
			builder.WriteUInt16(1);
			builder.WriteUInt16(1);
			builder.WriteUInt32(0U);
			builder.WriteInt32(sizes.MetadataVersionPaddedLength);
			int count2 = builder.Count;
			builder.WriteUTF8(metadataVersion, true);
			builder.WriteByte(0);
			int count3 = builder.Count;
			for (int i = 0; i < sizes.MetadataVersionPaddedLength - (count3 - count2); i++)
			{
				builder.WriteByte(0);
			}
			builder.WriteUInt16(0);
			builder.WriteUInt16((ushort)(5 + (sizes.IsEncDelta ? 1 : 0) + (sizes.IsStandaloneDebugMetadata ? 1 : 0)));
			int metadataHeaderSize = sizes.MetadataHeaderSize;
			if (sizes.IsStandaloneDebugMetadata)
			{
				MetadataBuilder.SerializeStreamHeader(ref metadataHeaderSize, sizes.StandalonePdbStreamSize, "#Pdb", builder);
			}
			MetadataBuilder.SerializeStreamHeader(ref metadataHeaderSize, sizes.MetadataTableStreamSize, sizes.IsCompressed ? "#~" : "#-", builder);
			MetadataBuilder.SerializeStreamHeader(ref metadataHeaderSize, sizes.GetAlignedHeapSize(HeapIndex.String), "#Strings", builder);
			MetadataBuilder.SerializeStreamHeader(ref metadataHeaderSize, sizes.GetAlignedHeapSize(HeapIndex.UserString), "#US", builder);
			MetadataBuilder.SerializeStreamHeader(ref metadataHeaderSize, sizes.GetAlignedHeapSize(HeapIndex.Guid), "#GUID", builder);
			MetadataBuilder.SerializeStreamHeader(ref metadataHeaderSize, sizes.GetAlignedHeapSize(HeapIndex.Blob), "#Blob", builder);
			if (sizes.IsEncDelta)
			{
				MetadataBuilder.SerializeStreamHeader(ref metadataHeaderSize, 0, "#JTD", builder);
			}
			int count4 = builder.Count;
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x000135B0 File Offset: 0x000117B0
		private static void SerializeStreamHeader(ref int offsetFromStartOfMetadata, int alignedStreamSize, string streamName, BlobBuilder builder)
		{
			int metadataStreamHeaderSize = MetadataSizes.GetMetadataStreamHeaderSize(streamName);
			builder.WriteInt32(offsetFromStartOfMetadata);
			builder.WriteInt32(alignedStreamSize);
			for (int i = 0; i < streamName.Length; i++)
			{
				char c = streamName.get_Chars(i);
				builder.WriteByte((byte)c);
			}
			uint num = (uint)(8 + streamName.Length);
			while ((ulong)num < (ulong)((long)metadataStreamHeaderSize))
			{
				builder.WriteByte(0);
				num += 1U;
			}
			offsetFromStartOfMetadata += alignedStreamSize;
		}

		/// <summary>Sets the capacity of the specified table.</summary>
		/// <param name="table">The table index.</param>
		/// <param name="rowCount">The number of rows in the table.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="table" /> is not a valid table index.
		///
		/// -or-
		///
		/// <paramref name="rowCount" /> is negative.</exception>
		// Token: 0x0600080C RID: 2060 RVA: 0x0001361C File Offset: 0x0001181C
		public void SetCapacity(TableIndex table, int rowCount)
		{
			if (rowCount < 0)
			{
				Throw.ArgumentOutOfRange("rowCount");
			}
			switch (table)
			{
			case TableIndex.Module:
			case TableIndex.FieldPtr:
			case TableIndex.MethodPtr:
			case TableIndex.ParamPtr:
			case TableIndex.EventPtr:
			case TableIndex.PropertyPtr:
			case TableIndex.Assembly:
			case TableIndex.AssemblyProcessor:
			case TableIndex.AssemblyOS:
			case TableIndex.AssemblyRefProcessor:
			case TableIndex.AssemblyRefOS:
				return;
			case TableIndex.TypeRef:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.TypeRefRow>(this._typeRefTable, rowCount);
				return;
			case TableIndex.TypeDef:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.TypeDefRow>(this._typeDefTable, rowCount);
				return;
			case TableIndex.Field:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.FieldDefRow>(this._fieldTable, rowCount);
				return;
			case TableIndex.MethodDef:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.MethodRow>(this._methodDefTable, rowCount);
				return;
			case TableIndex.Param:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.ParamRow>(this._paramTable, rowCount);
				return;
			case TableIndex.InterfaceImpl:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.InterfaceImplRow>(this._interfaceImplTable, rowCount);
				return;
			case TableIndex.MemberRef:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.MemberRefRow>(this._memberRefTable, rowCount);
				return;
			case TableIndex.Constant:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.ConstantRow>(this._constantTable, rowCount);
				return;
			case TableIndex.CustomAttribute:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.CustomAttributeRow>(this._customAttributeTable, rowCount);
				return;
			case TableIndex.FieldMarshal:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.FieldMarshalRow>(this._fieldMarshalTable, rowCount);
				return;
			case TableIndex.DeclSecurity:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.DeclSecurityRow>(this._declSecurityTable, rowCount);
				return;
			case TableIndex.ClassLayout:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.ClassLayoutRow>(this._classLayoutTable, rowCount);
				return;
			case TableIndex.FieldLayout:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.FieldLayoutRow>(this._fieldLayoutTable, rowCount);
				return;
			case TableIndex.StandAloneSig:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.StandaloneSigRow>(this._standAloneSigTable, rowCount);
				return;
			case TableIndex.EventMap:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.EventMapRow>(this._eventMapTable, rowCount);
				return;
			case TableIndex.Event:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.EventRow>(this._eventTable, rowCount);
				return;
			case TableIndex.PropertyMap:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.PropertyMapRow>(this._propertyMapTable, rowCount);
				return;
			case TableIndex.Property:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.PropertyRow>(this._propertyTable, rowCount);
				return;
			case TableIndex.MethodSemantics:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.MethodSemanticsRow>(this._methodSemanticsTable, rowCount);
				return;
			case TableIndex.MethodImpl:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.MethodImplRow>(this._methodImplTable, rowCount);
				return;
			case TableIndex.ModuleRef:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.ModuleRefRow>(this._moduleRefTable, rowCount);
				return;
			case TableIndex.TypeSpec:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.TypeSpecRow>(this._typeSpecTable, rowCount);
				return;
			case TableIndex.ImplMap:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.ImplMapRow>(this._implMapTable, rowCount);
				return;
			case TableIndex.FieldRva:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.FieldRvaRow>(this._fieldRvaTable, rowCount);
				return;
			case TableIndex.EncLog:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.EncLogRow>(this._encLogTable, rowCount);
				return;
			case TableIndex.EncMap:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.EncMapRow>(this._encMapTable, rowCount);
				return;
			case TableIndex.AssemblyRef:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.AssemblyRefTableRow>(this._assemblyRefTable, rowCount);
				return;
			case TableIndex.File:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.FileTableRow>(this._fileTable, rowCount);
				return;
			case TableIndex.ExportedType:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.ExportedTypeRow>(this._exportedTypeTable, rowCount);
				return;
			case TableIndex.ManifestResource:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.ManifestResourceRow>(this._manifestResourceTable, rowCount);
				return;
			case TableIndex.NestedClass:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.NestedClassRow>(this._nestedClassTable, rowCount);
				return;
			case TableIndex.GenericParam:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.GenericParamRow>(this._genericParamTable, rowCount);
				return;
			case TableIndex.MethodSpec:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.MethodSpecRow>(this._methodSpecTable, rowCount);
				return;
			case TableIndex.GenericParamConstraint:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.GenericParamConstraintRow>(this._genericParamConstraintTable, rowCount);
				return;
			case TableIndex.Document:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.DocumentRow>(this._documentTable, rowCount);
				return;
			case TableIndex.MethodDebugInformation:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.MethodDebugInformationRow>(this._methodDebugInformationTable, rowCount);
				return;
			case TableIndex.LocalScope:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.LocalScopeRow>(this._localScopeTable, rowCount);
				return;
			case TableIndex.LocalVariable:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.LocalVariableRow>(this._localVariableTable, rowCount);
				return;
			case TableIndex.LocalConstant:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.LocalConstantRow>(this._localConstantTable, rowCount);
				return;
			case TableIndex.ImportScope:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.ImportScopeRow>(this._importScopeTable, rowCount);
				return;
			case TableIndex.StateMachineMethod:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.StateMachineMethodRow>(this._stateMachineMethodTable, rowCount);
				return;
			case TableIndex.CustomDebugInformation:
				MetadataBuilder.SetTableCapacity<MetadataBuilder.CustomDebugInformationRow>(this._customDebugInformationTable, rowCount);
				return;
			}
			throw new ArgumentOutOfRangeException("table");
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0001394F File Offset: 0x00011B4F
		private static void SetTableCapacity<T>(List<T> table, int rowCount)
		{
			if (rowCount > table.Count)
			{
				table.Capacity = rowCount;
			}
		}

		/// <summary>Returns the current number of items in the specified table.</summary>
		/// <param name="table">The table index.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="table" /> is not a valid table index.</exception>
		/// <returns>The number of items in the table.</returns>
		// Token: 0x0600080E RID: 2062 RVA: 0x00013964 File Offset: 0x00011B64
		public int GetRowCount(TableIndex table)
		{
			switch (table)
			{
			case TableIndex.Module:
				if (this._moduleRow == null)
				{
					return 0;
				}
				return 1;
			case TableIndex.TypeRef:
				return this._typeRefTable.Count;
			case TableIndex.TypeDef:
				return this._typeDefTable.Count;
			case TableIndex.FieldPtr:
			case TableIndex.MethodPtr:
			case TableIndex.ParamPtr:
			case TableIndex.EventPtr:
			case TableIndex.PropertyPtr:
			case TableIndex.AssemblyProcessor:
			case TableIndex.AssemblyOS:
			case TableIndex.AssemblyRefProcessor:
			case TableIndex.AssemblyRefOS:
				return 0;
			case TableIndex.Field:
				return this._fieldTable.Count;
			case TableIndex.MethodDef:
				return this._methodDefTable.Count;
			case TableIndex.Param:
				return this._paramTable.Count;
			case TableIndex.InterfaceImpl:
				return this._interfaceImplTable.Count;
			case TableIndex.MemberRef:
				return this._memberRefTable.Count;
			case TableIndex.Constant:
				return this._constantTable.Count;
			case TableIndex.CustomAttribute:
				return this._customAttributeTable.Count;
			case TableIndex.FieldMarshal:
				return this._fieldMarshalTable.Count;
			case TableIndex.DeclSecurity:
				return this._declSecurityTable.Count;
			case TableIndex.ClassLayout:
				return this._classLayoutTable.Count;
			case TableIndex.FieldLayout:
				return this._fieldLayoutTable.Count;
			case TableIndex.StandAloneSig:
				return this._standAloneSigTable.Count;
			case TableIndex.EventMap:
				return this._eventMapTable.Count;
			case TableIndex.Event:
				return this._eventTable.Count;
			case TableIndex.PropertyMap:
				return this._propertyMapTable.Count;
			case TableIndex.Property:
				return this._propertyTable.Count;
			case TableIndex.MethodSemantics:
				return this._methodSemanticsTable.Count;
			case TableIndex.MethodImpl:
				return this._methodImplTable.Count;
			case TableIndex.ModuleRef:
				return this._moduleRefTable.Count;
			case TableIndex.TypeSpec:
				return this._typeSpecTable.Count;
			case TableIndex.ImplMap:
				return this._implMapTable.Count;
			case TableIndex.FieldRva:
				return this._fieldRvaTable.Count;
			case TableIndex.EncLog:
				return this._encLogTable.Count;
			case TableIndex.EncMap:
				return this._encMapTable.Count;
			case TableIndex.Assembly:
				if (this._assemblyRow == null)
				{
					return 0;
				}
				return 1;
			case TableIndex.AssemblyRef:
				return this._assemblyRefTable.Count;
			case TableIndex.File:
				return this._fileTable.Count;
			case TableIndex.ExportedType:
				return this._exportedTypeTable.Count;
			case TableIndex.ManifestResource:
				return this._manifestResourceTable.Count;
			case TableIndex.NestedClass:
				return this._nestedClassTable.Count;
			case TableIndex.GenericParam:
				return this._genericParamTable.Count;
			case TableIndex.MethodSpec:
				return this._methodSpecTable.Count;
			case TableIndex.GenericParamConstraint:
				return this._genericParamConstraintTable.Count;
			case TableIndex.Document:
				return this._documentTable.Count;
			case TableIndex.MethodDebugInformation:
				return this._methodDebugInformationTable.Count;
			case TableIndex.LocalScope:
				return this._localScopeTable.Count;
			case TableIndex.LocalVariable:
				return this._localVariableTable.Count;
			case TableIndex.LocalConstant:
				return this._localConstantTable.Count;
			case TableIndex.ImportScope:
				return this._importScopeTable.Count;
			case TableIndex.StateMachineMethod:
				return this._stateMachineMethodTable.Count;
			case TableIndex.CustomDebugInformation:
				return this._customDebugInformationTable.Count;
			}
			throw new ArgumentOutOfRangeException("table");
		}

		/// <summary>Returns the current number of items in each table.</summary>
		/// <returns>An array of size <see cref="F:System.Reflection.Metadata.Ecma335.MetadataTokens.TableCount" />, with each item filled with the current row count of the corresponding table.</returns>
		// Token: 0x0600080F RID: 2063 RVA: 0x00013C84 File Offset: 0x00011E84
		[NullableContext(0)]
		public ImmutableArray<int> GetRowCounts()
		{
			ImmutableArray<int>.Builder builder = ImmutableArray.CreateBuilder<int>(MetadataTokens.TableCount);
			builder.Count = MetadataTokens.TableCount;
			builder[32] = ((this._assemblyRow != null) ? 1 : 0);
			builder[35] = this._assemblyRefTable.Count;
			builder[15] = this._classLayoutTable.Count;
			builder[11] = this._constantTable.Count;
			builder[12] = this._customAttributeTable.Count;
			builder[14] = this._declSecurityTable.Count;
			builder[30] = this._encLogTable.Count;
			builder[31] = this._encMapTable.Count;
			builder[18] = this._eventMapTable.Count;
			builder[20] = this._eventTable.Count;
			builder[39] = this._exportedTypeTable.Count;
			builder[16] = this._fieldLayoutTable.Count;
			builder[13] = this._fieldMarshalTable.Count;
			builder[29] = this._fieldRvaTable.Count;
			builder[4] = this._fieldTable.Count;
			builder[38] = this._fileTable.Count;
			builder[44] = this._genericParamConstraintTable.Count;
			builder[42] = this._genericParamTable.Count;
			builder[28] = this._implMapTable.Count;
			builder[9] = this._interfaceImplTable.Count;
			builder[40] = this._manifestResourceTable.Count;
			builder[10] = this._memberRefTable.Count;
			builder[25] = this._methodImplTable.Count;
			builder[24] = this._methodSemanticsTable.Count;
			builder[43] = this._methodSpecTable.Count;
			builder[6] = this._methodDefTable.Count;
			builder[26] = this._moduleRefTable.Count;
			builder[0] = ((this._moduleRow != null) ? 1 : 0);
			builder[41] = this._nestedClassTable.Count;
			builder[8] = this._paramTable.Count;
			builder[21] = this._propertyMapTable.Count;
			builder[23] = this._propertyTable.Count;
			builder[17] = this._standAloneSigTable.Count;
			builder[2] = this._typeDefTable.Count;
			builder[1] = this._typeRefTable.Count;
			builder[27] = this._typeSpecTable.Count;
			builder[48] = this._documentTable.Count;
			builder[49] = this._methodDebugInformationTable.Count;
			builder[50] = this._localScopeTable.Count;
			builder[51] = this._localVariableTable.Count;
			builder[52] = this._localConstantTable.Count;
			builder[54] = this._stateMachineMethodTable.Count;
			builder[53] = this._importScopeTable.Count;
			builder[55] = this._customDebugInformationTable.Count;
			return builder.MoveToImmutable();
		}

		/// <param name="generation" />
		/// <param name="moduleName" />
		/// <param name="mvid" />
		/// <param name="encId" />
		/// <param name="encBaseId" />
		// Token: 0x06000810 RID: 2064 RVA: 0x00013FF8 File Offset: 0x000121F8
		public ModuleDefinitionHandle AddModule(int generation, StringHandle moduleName, GuidHandle mvid, GuidHandle encId, GuidHandle encBaseId)
		{
			if (generation > 65535)
			{
				Throw.ArgumentOutOfRange("generation");
			}
			if (this._moduleRow != null)
			{
				Throw.InvalidOperation(SR.ModuleAlreadyAdded);
			}
			this._moduleRow = new MetadataBuilder.ModuleRow?(new MetadataBuilder.ModuleRow
			{
				Generation = (ushort)generation,
				Name = moduleName,
				ModuleVersionId = mvid,
				EncId = encId,
				EncBaseId = encBaseId
			});
			return EntityHandle.ModuleDefinition;
		}

		/// <param name="name" />
		/// <param name="version" />
		/// <param name="culture" />
		/// <param name="publicKey" />
		/// <param name="flags" />
		/// <param name="hashAlgorithm" />
		// Token: 0x06000811 RID: 2065 RVA: 0x00014074 File Offset: 0x00012274
		public AssemblyDefinitionHandle AddAssembly(StringHandle name, Version version, StringHandle culture, BlobHandle publicKey, AssemblyFlags flags, AssemblyHashAlgorithm hashAlgorithm)
		{
			if (version == null)
			{
				Throw.ArgumentNull("version");
			}
			if (this._assemblyRow != null)
			{
				Throw.InvalidOperation(SR.AssemblyAlreadyAdded);
			}
			this._assemblyRow = new MetadataBuilder.AssemblyRow?(new MetadataBuilder.AssemblyRow
			{
				Flags = (ushort)flags,
				HashAlgorithm = (uint)hashAlgorithm,
				Version = version,
				AssemblyKey = publicKey,
				AssemblyName = name,
				AssemblyCulture = culture
			});
			return EntityHandle.AssemblyDefinition;
		}

		/// <param name="name" />
		/// <param name="version" />
		/// <param name="culture" />
		/// <param name="publicKeyOrToken" />
		/// <param name="flags" />
		/// <param name="hashValue" />
		// Token: 0x06000812 RID: 2066 RVA: 0x000140F8 File Offset: 0x000122F8
		public AssemblyReferenceHandle AddAssemblyReference(StringHandle name, Version version, StringHandle culture, BlobHandle publicKeyOrToken, AssemblyFlags flags, BlobHandle hashValue)
		{
			if (version == null)
			{
				Throw.ArgumentNull("version");
			}
			this._assemblyRefTable.Add(new MetadataBuilder.AssemblyRefTableRow
			{
				Name = name,
				Version = version,
				Culture = culture,
				PublicKeyToken = publicKeyOrToken,
				Flags = (uint)flags,
				HashValue = hashValue
			});
			return AssemblyReferenceHandle.FromRowId(this._assemblyRefTable.Count);
		}

		/// <summary>Adds a type definition.</summary>
		/// <param name="attributes">The type attributes.</param>
		/// <param name="namespace">The type namespace.</param>
		/// <param name="name">The type name.</param>
		/// <param name="baseType">The base type entity handle, which can be one of the following: <see cref="T:System.Reflection.Metadata.TypeDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.TypeReferenceHandle" />, <see cref="T:System.Reflection.Metadata.TypeSpecificationHandle" />, or <see langword="null" />.</param>
		/// <param name="fieldList">If the type declares fields, set this to the handle of the first one. Otherwise, set this to the handle of the first field declared by the next type definition. If no type defines any fields in the module, <see langword="System.Reflection.Metadata.Ecma335.MetadataTokens.FieldDefinitionHandle(1)" />.</param>
		/// <param name="methodList">If the type declares methods, the handle of the first one. Otherwise, the handle of the first method declared by the next type definition. If no type defines any methods in the module, <see langword="System.Reflection.Metadata.Ecma335.MetadataTokens.MethodDefinitionHandle(1)" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="baseType" /> doesn't have the expected handle kind.</exception>
		/// <returns>A handle to the added type definition.</returns>
		// Token: 0x06000813 RID: 2067 RVA: 0x00014170 File Offset: 0x00012370
		public TypeDefinitionHandle AddTypeDefinition(TypeAttributes attributes, StringHandle @namespace, StringHandle name, EntityHandle baseType, FieldDefinitionHandle fieldList, MethodDefinitionHandle methodList)
		{
			this._typeDefTable.Add(new MetadataBuilder.TypeDefRow
			{
				Flags = attributes,
				Name = name,
				Namespace = @namespace,
				Extends = (baseType.IsNil ? 0 : CodedIndex.TypeDefOrRefOrSpec(baseType)),
				FieldList = fieldList.RowId,
				MethodList = methodList.RowId
			});
			return TypeDefinitionHandle.FromRowId(this._typeDefTable.Count);
		}

		/// <summary>Defines a type layout of a type definition.</summary>
		/// <param name="type">The type definition.</param>
		/// <param name="packingSize">Specifies that fields should be placed within the type instance at byte addresses which are a multiple of <paramref name="packingSize" />, or at natural alignment for that field type, whichever is smaller. Its value should be one of the following: 0, 1, 2, 4, 8, 16, 32, 64, or 128. A value of zero indicates that the packing size used should match the default for the current platform.</param>
		/// <param name="size">Indicates a minimum size of the type instance and is intended to allow for padding. The amount of memory allocated is the maximum of the size calculated from the layout and <paramref name="size" />. Note that if this directive applies to a value type, then the size will be less than 1 MB.</param>
		// Token: 0x06000814 RID: 2068 RVA: 0x000141F0 File Offset: 0x000123F0
		public void AddTypeLayout(TypeDefinitionHandle type, ushort packingSize, uint size)
		{
			this._classLayoutTable.Add(new MetadataBuilder.ClassLayoutRow
			{
				Parent = type.RowId,
				PackingSize = packingSize,
				ClassSize = size
			});
		}

		/// <summary>Adds an interface implementation to a type.</summary>
		/// <param name="type">The type implementing the interface.</param>
		/// <param name="implementedInterface">The interface being implemented, which can be one of the following: <see cref="T:System.Reflection.Metadata.TypeDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.TypeReferenceHandle" />, or <see cref="T:System.Reflection.Metadata.TypeSpecificationHandle" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="implementedInterface" /> doesn't have the expected handle kind.</exception>
		/// <returns>A handle to the added interface implementation.</returns>
		// Token: 0x06000815 RID: 2069 RVA: 0x00014230 File Offset: 0x00012430
		public InterfaceImplementationHandle AddInterfaceImplementation(TypeDefinitionHandle type, EntityHandle implementedInterface)
		{
			this._interfaceImplTable.Add(new MetadataBuilder.InterfaceImplRow
			{
				Class = type.RowId,
				Interface = CodedIndex.TypeDefOrRefOrSpec(implementedInterface)
			});
			return InterfaceImplementationHandle.FromRowId(this._interfaceImplTable.Count);
		}

		/// <summary>Defines a nesting relationship to specified type definitions.</summary>
		/// <param name="type">The nested type definition handle.</param>
		/// <param name="enclosingType">The enclosing type definition handle.</param>
		// Token: 0x06000816 RID: 2070 RVA: 0x0001427C File Offset: 0x0001247C
		public void AddNestedType(TypeDefinitionHandle type, TypeDefinitionHandle enclosingType)
		{
			this._nestedClassTable.Add(new MetadataBuilder.NestedClassRow
			{
				NestedClass = type.RowId,
				EnclosingClass = enclosingType.RowId
			});
		}

		/// <summary>Adds a type reference.</summary>
		/// <param name="resolutionScope">The entity declaring the target type, which can be one of the following: <see cref="T:System.Reflection.Metadata.ModuleDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.ModuleReferenceHandle" />, <see cref="T:System.Reflection.Metadata.AssemblyReferenceHandle" />, <see cref="T:System.Reflection.Metadata.TypeReferenceHandle" />, or <see langword="null" />.</param>
		/// <param name="namespace">The type reference namespace.</param>
		/// <param name="name">The type reference name.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="resolutionScope" /> doesn't have the expected handle kind.</exception>
		/// <returns>A handle to the added type reference.</returns>
		// Token: 0x06000817 RID: 2071 RVA: 0x000142BC File Offset: 0x000124BC
		public TypeReferenceHandle AddTypeReference(EntityHandle resolutionScope, StringHandle @namespace, StringHandle name)
		{
			this._typeRefTable.Add(new MetadataBuilder.TypeRefRow
			{
				ResolutionScope = (resolutionScope.IsNil ? 0 : CodedIndex.ResolutionScope(resolutionScope)),
				Name = name,
				Namespace = @namespace
			});
			return TypeReferenceHandle.FromRowId(this._typeRefTable.Count);
		}

		/// <param name="signature" />
		// Token: 0x06000818 RID: 2072 RVA: 0x00014318 File Offset: 0x00012518
		public TypeSpecificationHandle AddTypeSpecification(BlobHandle signature)
		{
			this._typeSpecTable.Add(new MetadataBuilder.TypeSpecRow
			{
				Signature = signature
			});
			return TypeSpecificationHandle.FromRowId(this._typeSpecTable.Count);
		}

		/// <param name="signature" />
		// Token: 0x06000819 RID: 2073 RVA: 0x00014354 File Offset: 0x00012554
		public StandaloneSignatureHandle AddStandaloneSignature(BlobHandle signature)
		{
			this._standAloneSigTable.Add(new MetadataBuilder.StandaloneSigRow
			{
				Signature = signature
			});
			return StandaloneSignatureHandle.FromRowId(this._standAloneSigTable.Count);
		}

		/// <summary>Adds a property definition.</summary>
		/// <param name="attributes">The property attributes.</param>
		/// <param name="name">The property name.</param>
		/// <param name="signature">The signature of the property.</param>
		/// <returns>A handle to the added property definition.</returns>
		// Token: 0x0600081A RID: 2074 RVA: 0x00014390 File Offset: 0x00012590
		public PropertyDefinitionHandle AddProperty(PropertyAttributes attributes, StringHandle name, BlobHandle signature)
		{
			this._propertyTable.Add(new MetadataBuilder.PropertyRow
			{
				PropFlags = attributes,
				Name = name,
				Type = signature
			});
			return PropertyDefinitionHandle.FromRowId(this._propertyTable.Count);
		}

		/// <param name="declaringType" />
		/// <param name="propertyList" />
		// Token: 0x0600081B RID: 2075 RVA: 0x000143DC File Offset: 0x000125DC
		public void AddPropertyMap(TypeDefinitionHandle declaringType, PropertyDefinitionHandle propertyList)
		{
			this._propertyMapTable.Add(new MetadataBuilder.PropertyMapRow
			{
				Parent = declaringType.RowId,
				PropertyList = propertyList.RowId
			});
		}

		/// <summary>Adds an event definition.</summary>
		/// <param name="attributes">The event attributes.</param>
		/// <param name="name">The event name.</param>
		/// <param name="type">The type of the event: a <see cref="T:System.Reflection.Metadata.TypeDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.TypeReferenceHandle" />, or <see cref="T:System.Reflection.Metadata.TypeSpecificationHandle" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> doesn't have the expected handle kind.</exception>
		/// <returns>A handle to the added event definition.</returns>
		// Token: 0x0600081C RID: 2076 RVA: 0x0001441C File Offset: 0x0001261C
		public EventDefinitionHandle AddEvent(EventAttributes attributes, StringHandle name, EntityHandle type)
		{
			this._eventTable.Add(new MetadataBuilder.EventRow
			{
				EventFlags = attributes,
				Name = name,
				EventType = CodedIndex.TypeDefOrRefOrSpec(type)
			});
			return EventDefinitionHandle.FromRowId(this._eventTable.Count);
		}

		/// <param name="declaringType" />
		/// <param name="eventList" />
		// Token: 0x0600081D RID: 2077 RVA: 0x0001446C File Offset: 0x0001266C
		public void AddEventMap(TypeDefinitionHandle declaringType, EventDefinitionHandle eventList)
		{
			this._eventMapTable.Add(new MetadataBuilder.EventMapRow
			{
				Parent = declaringType.RowId,
				EventList = eventList.RowId
			});
		}

		/// <summary>Adds a default value for a parameter, field or property.</summary>
		/// <param name="parent">The parent entity handle, which can be one of the following: <see cref="T:System.Reflection.Metadata.ParameterHandle" />, <see cref="T:System.Reflection.Metadata.FieldDefinitionHandle" />, or <see cref="T:System.Reflection.Metadata.PropertyDefinitionHandle" />.</param>
		/// <param name="value">The constant value.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="parent" /> doesn't have the expected handle kind.</exception>
		/// <returns>A handle to the added constant.</returns>
		// Token: 0x0600081E RID: 2078 RVA: 0x000144AC File Offset: 0x000126AC
		[NullableContext(2)]
		public ConstantHandle AddConstant(EntityHandle parent, object value)
		{
			int num = CodedIndex.HasConstant(parent);
			this._constantTableNeedsSorting |= num < this._constantTableLastParent;
			this._constantTableLastParent = num;
			this._constantTable.Add(new MetadataBuilder.ConstantRow
			{
				Type = (byte)MetadataWriterUtilities.GetConstantTypeCode(value),
				Parent = num,
				Value = this.GetOrAddConstantBlob(value)
			});
			return ConstantHandle.FromRowId(this._constantTable.Count);
		}

		/// <summary>Associates a method (a getter, a setter, an adder, etc.) with a property or an event.</summary>
		/// <param name="association">The association entity handle, which can be one of the following: <see cref="T:System.Reflection.Metadata.EventDefinitionHandle" /> or <see cref="T:System.Reflection.Metadata.PropertyDefinitionHandle" />.</param>
		/// <param name="semantics">The method semantics attributes.</param>
		/// <param name="methodDefinition">The method definition.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="association" /> doesn't have the expected handle kind.</exception>
		// Token: 0x0600081F RID: 2079 RVA: 0x00014524 File Offset: 0x00012724
		public void AddMethodSemantics(EntityHandle association, MethodSemanticsAttributes semantics, MethodDefinitionHandle methodDefinition)
		{
			int num = CodedIndex.HasSemantics(association);
			this._methodSemanticsTableNeedsSorting |= num < this._methodSemanticsTableLastAssociation;
			this._methodSemanticsTableLastAssociation = num;
			this._methodSemanticsTable.Add(new MetadataBuilder.MethodSemanticsRow
			{
				Association = num,
				Method = methodDefinition.RowId,
				Semantic = (ushort)semantics
			});
		}

		/// <summary>Adds a custom attribute.</summary>
		/// <param name="parent">An entity to attach the custom attribute to: a <see cref="T:System.Reflection.Metadata.MethodDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.FieldDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.TypeReferenceHandle" />, <see cref="T:System.Reflection.Metadata.TypeDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.ParameterHandle" />, <see cref="T:System.Reflection.Metadata.InterfaceImplementationHandle" />, <see cref="T:System.Reflection.Metadata.MemberReferenceHandle" />, <see cref="T:System.Reflection.Metadata.ModuleDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.DeclarativeSecurityAttributeHandle" />, <see cref="T:System.Reflection.Metadata.PropertyDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.EventDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.StandaloneSignatureHandle" />, <see cref="T:System.Reflection.Metadata.ModuleReferenceHandle" />, <see cref="T:System.Reflection.Metadata.TypeSpecificationHandle" />, <see cref="T:System.Reflection.Metadata.AssemblyDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.AssemblyReferenceHandle" />, <see cref="T:System.Reflection.Metadata.AssemblyFileHandle" />, <see cref="T:System.Reflection.Metadata.ExportedTypeHandle" />, <see cref="T:System.Reflection.Metadata.ManifestResourceHandle" />, <see cref="T:System.Reflection.Metadata.GenericParameterHandle" />, <see cref="T:System.Reflection.Metadata.GenericParameterConstraintHandle" />, or a <see cref="T:System.Reflection.Metadata.MethodSpecificationHandle" />.</param>
		/// <param name="constructor">A custom attribute constructor: a <see cref="T:System.Reflection.Metadata.MethodDefinitionHandle" /> or <see cref="T:System.Reflection.Metadata.MemberReferenceHandle" />.</param>
		/// <param name="value">A custom attribute value blob.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="parent" /> doesn't have the expected handle kind.</exception>
		/// <returns>A handle to the added custom attribute.</returns>
		// Token: 0x06000820 RID: 2080 RVA: 0x00014588 File Offset: 0x00012788
		public CustomAttributeHandle AddCustomAttribute(EntityHandle parent, EntityHandle constructor, BlobHandle value)
		{
			int num = CodedIndex.HasCustomAttribute(parent);
			this._customAttributeTableNeedsSorting |= num < this._customAttributeTableLastParent;
			this._customAttributeTableLastParent = num;
			this._customAttributeTable.Add(new MetadataBuilder.CustomAttributeRow
			{
				Parent = num,
				Type = CodedIndex.CustomAttributeType(constructor),
				Value = value
			});
			return CustomAttributeHandle.FromRowId(this._customAttributeTable.Count);
		}

		/// <summary>Adds a method specification (an instantiation).</summary>
		/// <param name="method">The generic method entity handle, which can be one of the following: <see cref="T:System.Reflection.Metadata.MethodDefinitionHandle" /> or <see cref="T:System.Reflection.Metadata.MemberReferenceHandle" />.</param>
		/// <param name="instantiation">The instantiation blob encoding the generic arguments of the method.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="method" /> doesn't have the expected handle kind.</exception>
		/// <returns>A handle to the added method specification.</returns>
		// Token: 0x06000821 RID: 2081 RVA: 0x000145FC File Offset: 0x000127FC
		public MethodSpecificationHandle AddMethodSpecification(EntityHandle method, BlobHandle instantiation)
		{
			this._methodSpecTable.Add(new MetadataBuilder.MethodSpecRow
			{
				Method = CodedIndex.MethodDefOrRef(method),
				Instantiation = instantiation
			});
			return MethodSpecificationHandle.FromRowId(this._methodSpecTable.Count);
		}

		/// <param name="moduleName" />
		// Token: 0x06000822 RID: 2082 RVA: 0x00014644 File Offset: 0x00012844
		public ModuleReferenceHandle AddModuleReference(StringHandle moduleName)
		{
			this._moduleRefTable.Add(new MetadataBuilder.ModuleRefRow
			{
				Name = moduleName
			});
			return ModuleReferenceHandle.FromRowId(this._moduleRefTable.Count);
		}

		/// <summary>Adds a parameter definition.</summary>
		/// <param name="attributes">The parameter attributes.</param>
		/// <param name="name">Optional. The parameter name.</param>
		/// <param name="sequenceNumber">The sequence number of the parameter. A value of 0 refers to the owner method's return type; its parameters are then numbered from 1 onward.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="sequenceNumber" /> is greater than <see cref="F:System.UInt16.MaxValue">UInt16.MaxValue</see>.</exception>
		/// <returns>A handle to the added parameter.</returns>
		// Token: 0x06000823 RID: 2083 RVA: 0x00014680 File Offset: 0x00012880
		public ParameterHandle AddParameter(ParameterAttributes attributes, StringHandle name, int sequenceNumber)
		{
			if (sequenceNumber > 65535)
			{
				Throw.ArgumentOutOfRange("sequenceNumber");
			}
			this._paramTable.Add(new MetadataBuilder.ParamRow
			{
				Flags = attributes,
				Name = name,
				Sequence = (ushort)sequenceNumber
			});
			return ParameterHandle.FromRowId(this._paramTable.Count);
		}

		/// <summary>Adds a generic parameter definition.</summary>
		/// <param name="parent">The parent entity handle, which can be either a <see cref="T:System.Reflection.Metadata.TypeDefinitionHandle" /> or <see cref="T:System.Reflection.Metadata.MethodDefinitionHandle" />.</param>
		/// <param name="attributes">The generic parameter attributes.</param>
		/// <param name="name">The parameter name.</param>
		/// <param name="index">The zero-based parameter index.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="parent" /> doesn't have the expected handle kind.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is greater than <see cref="F:System.UInt16.MaxValue">UInt16.MaxValue</see>.</exception>
		/// <returns>A handle to the added generic parameter.</returns>
		// Token: 0x06000824 RID: 2084 RVA: 0x000146E0 File Offset: 0x000128E0
		public GenericParameterHandle AddGenericParameter(EntityHandle parent, GenericParameterAttributes attributes, StringHandle name, int index)
		{
			if (index > 65535)
			{
				Throw.ArgumentOutOfRange("index");
			}
			this._genericParamTable.Add(new MetadataBuilder.GenericParamRow
			{
				Flags = attributes,
				Name = name,
				Number = (ushort)index,
				Owner = CodedIndex.TypeOrMethodDef(parent)
			});
			return GenericParameterHandle.FromRowId(this._genericParamTable.Count);
		}

		/// <summary>Adds a type constraint to a generic parameter.</summary>
		/// <param name="genericParameter">The generic parameter to constrain.</param>
		/// <param name="constraint">The type constraint, which can be one of the following: a <see cref="T:System.Reflection.Metadata.TypeDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.TypeReferenceHandle" /> or a <see cref="T:System.Reflection.Metadata.TypeSpecificationHandle" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="genericParameter" /> doesn't have the expected handle kind.</exception>
		/// <returns>A handle to the added generic parameter constraint.</returns>
		// Token: 0x06000825 RID: 2085 RVA: 0x0001474C File Offset: 0x0001294C
		public GenericParameterConstraintHandle AddGenericParameterConstraint(GenericParameterHandle genericParameter, EntityHandle constraint)
		{
			this._genericParamConstraintTable.Add(new MetadataBuilder.GenericParamConstraintRow
			{
				Owner = genericParameter.RowId,
				Constraint = CodedIndex.TypeDefOrRefOrSpec(constraint)
			});
			return GenericParameterConstraintHandle.FromRowId(this._genericParamConstraintTable.Count);
		}

		/// <summary>Adds a field definition.</summary>
		/// <param name="attributes">The field attributes.</param>
		/// <param name="name">The field name.</param>
		/// <param name="signature">The field signature. Use <see cref="M:System.Reflection.Metadata.Ecma335.BlobEncoder.FieldSignature" /> to construct the blob.</param>
		/// <returns>A handle to the added field definition.</returns>
		// Token: 0x06000826 RID: 2086 RVA: 0x00014798 File Offset: 0x00012998
		public FieldDefinitionHandle AddFieldDefinition(FieldAttributes attributes, StringHandle name, BlobHandle signature)
		{
			this._fieldTable.Add(new MetadataBuilder.FieldDefRow
			{
				Flags = attributes,
				Name = name,
				Signature = signature
			});
			return FieldDefinitionHandle.FromRowId(this._fieldTable.Count);
		}

		/// <summary>Defines a field layout of a field definition.</summary>
		/// <param name="field">The field definition handle.</param>
		/// <param name="offset">The byte offset of the field within the declaring type instance.</param>
		// Token: 0x06000827 RID: 2087 RVA: 0x000147E4 File Offset: 0x000129E4
		public void AddFieldLayout(FieldDefinitionHandle field, int offset)
		{
			this._fieldLayoutTable.Add(new MetadataBuilder.FieldLayoutRow
			{
				Field = field.RowId,
				Offset = offset
			});
		}

		/// <summary>Adds marshalling information to a field or a parameter.</summary>
		/// <param name="parent">The parent entity handle, which can be one of the following: <see cref="T:System.Reflection.Metadata.ParameterHandle" /> or <see cref="T:System.Reflection.Metadata.FieldDefinitionHandle" />.</param>
		/// <param name="descriptor">The descriptor blob.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="parent" /> doesn't have the expected handle kind.</exception>
		// Token: 0x06000828 RID: 2088 RVA: 0x0001481C File Offset: 0x00012A1C
		public void AddMarshallingDescriptor(EntityHandle parent, BlobHandle descriptor)
		{
			int num = CodedIndex.HasFieldMarshal(parent);
			this._fieldMarshalTableNeedsSorting |= num < this._fieldMarshalTableLastParent;
			this._fieldMarshalTableLastParent = num;
			this._fieldMarshalTable.Add(new MetadataBuilder.FieldMarshalRow
			{
				Parent = num,
				NativeType = descriptor
			});
		}

		/// <summary>Adds a mapping from a field to its initial value stored in the PE image.</summary>
		/// <param name="field">The field definition handle.</param>
		/// <param name="offset">The offset within the block in the PE image that stores initial values of mapped fields (usually in the .text section).</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is negative.</exception>
		// Token: 0x06000829 RID: 2089 RVA: 0x00014874 File Offset: 0x00012A74
		public void AddFieldRelativeVirtualAddress(FieldDefinitionHandle field, int offset)
		{
			if (offset < 0)
			{
				Throw.ArgumentOutOfRange("offset");
			}
			this._fieldRvaTable.Add(new MetadataBuilder.FieldRvaRow
			{
				Field = field.RowId,
				Offset = offset
			});
		}

		/// <summary>Adds a method definition.</summary>
		/// <param name="attributes">The method attributes.</param>
		/// <param name="implAttributes">The method implementation attributes.</param>
		/// <param name="name">The method name.</param>
		/// <param name="signature">The method signature.</param>
		/// <param name="bodyOffset">Offset within the block in the PE image that stores method bodies (the IL stream), or -1 if the method doesn't have a body.</param>
		/// <param name="parameterList">If the method declares parameters in the Params table, set this to the handle of the first one. Otherwise, set this to the handle of the first parameter declared by the next method definition. If no parameters are declared in the module, <see langword="System.Reflection.Metadata.Ecma335.MetadataTokens.ParameterHandle(1)" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bodyOffset" /> is less than -1.</exception>
		/// <returns>A handle to the added method definition.</returns>
		// Token: 0x0600082A RID: 2090 RVA: 0x000148BC File Offset: 0x00012ABC
		public MethodDefinitionHandle AddMethodDefinition(MethodAttributes attributes, MethodImplAttributes implAttributes, StringHandle name, BlobHandle signature, int bodyOffset, ParameterHandle parameterList)
		{
			if (bodyOffset < -1)
			{
				Throw.ArgumentOutOfRange("bodyOffset");
			}
			this._methodDefTable.Add(new MetadataBuilder.MethodRow
			{
				Flags = attributes,
				ImplFlags = implAttributes,
				Name = name,
				Signature = signature,
				BodyOffset = bodyOffset,
				ParamList = parameterList.RowId
			});
			return MethodDefinitionHandle.FromRowId(this._methodDefTable.Count);
		}

		/// <summary>Adds import information to a method definition.</summary>
		/// <param name="method">The method definition handle.</param>
		/// <param name="attributes">The method import attributes.</param>
		/// <param name="name">The unmanaged method name.</param>
		/// <param name="module">The module containing the unmanaged method.</param>
		// Token: 0x0600082B RID: 2091 RVA: 0x00014938 File Offset: 0x00012B38
		public void AddMethodImport(MethodDefinitionHandle method, MethodImportAttributes attributes, StringHandle name, ModuleReferenceHandle module)
		{
			this._implMapTable.Add(new MetadataBuilder.ImplMapRow
			{
				MemberForwarded = CodedIndex.MemberForwarded(method),
				ImportName = name,
				ImportScope = module.RowId,
				MappingFlags = (ushort)attributes
			});
		}

		/// <summary>Defines an implementation for a method declaration within a type.</summary>
		/// <param name="type">The type definition.</param>
		/// <param name="methodBody">The method body entity handle, which can be one of the following: <see cref="T:System.Reflection.Metadata.MethodDefinitionHandle" /> or <see cref="T:System.Reflection.Metadata.MemberReferenceHandle" />.</param>
		/// <param name="methodDeclaration">The method declaration entity handle, which can be one of the following: <see cref="T:System.Reflection.Metadata.MethodDefinitionHandle" /> or <see cref="T:System.Reflection.Metadata.MemberReferenceHandle" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="methodBody" /> or <paramref name="methodDeclaration" /> doesn't have the expected handle kind.</exception>
		/// <returns>A handle to the added method implementation.</returns>
		// Token: 0x0600082C RID: 2092 RVA: 0x0001498C File Offset: 0x00012B8C
		public MethodImplementationHandle AddMethodImplementation(TypeDefinitionHandle type, EntityHandle methodBody, EntityHandle methodDeclaration)
		{
			this._methodImplTable.Add(new MetadataBuilder.MethodImplRow
			{
				Class = type.RowId,
				MethodBody = CodedIndex.MethodDefOrRef(methodBody),
				MethodDecl = CodedIndex.MethodDefOrRef(methodDeclaration)
			});
			return MethodImplementationHandle.FromRowId(this._methodImplTable.Count);
		}

		/// <summary>Adds a MemberRef table row.</summary>
		/// <param name="parent">The containing entity, which can be one of the following: <see cref="T:System.Reflection.Metadata.TypeDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.TypeReferenceHandle" />, <see cref="T:System.Reflection.Metadata.ModuleReferenceHandle" />, <see cref="T:System.Reflection.Metadata.MethodDefinitionHandle" />, or <see cref="T:System.Reflection.Metadata.TypeSpecificationHandle" />.</param>
		/// <param name="name">The member name.</param>
		/// <param name="signature">The member signature.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="parent" /> doesn't have the expected handle kind.</exception>
		/// <returns>A handle to the added member reference.</returns>
		// Token: 0x0600082D RID: 2093 RVA: 0x000149E8 File Offset: 0x00012BE8
		public MemberReferenceHandle AddMemberReference(EntityHandle parent, StringHandle name, BlobHandle signature)
		{
			this._memberRefTable.Add(new MetadataBuilder.MemberRefRow
			{
				Class = CodedIndex.MemberRefParent(parent),
				Name = name,
				Signature = signature
			});
			return MemberReferenceHandle.FromRowId(this._memberRefTable.Count);
		}

		/// <summary>Adds a manifest resource.</summary>
		/// <param name="attributes">The manifest resource attributes.</param>
		/// <param name="name">The name of the manifest resource.</param>
		/// <param name="implementation">The implementation entity handle, which can be one of the following: <see cref="T:System.Reflection.Metadata.AssemblyFileHandle" />, <see cref="T:System.Reflection.Metadata.AssemblyReferenceHandle" />, or <see langword="null" />.</param>
		/// <param name="offset">Specifies the byte offset within the referenced file at which this resource record begins.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="implementation" /> doesn't have the expected handle kind.</exception>
		/// <returns>A handle to the added manifest resource.</returns>
		// Token: 0x0600082E RID: 2094 RVA: 0x00014A38 File Offset: 0x00012C38
		public ManifestResourceHandle AddManifestResource(ManifestResourceAttributes attributes, StringHandle name, EntityHandle implementation, uint offset)
		{
			this._manifestResourceTable.Add(new MetadataBuilder.ManifestResourceRow
			{
				Flags = (uint)attributes,
				Name = name,
				Implementation = (implementation.IsNil ? 0 : CodedIndex.Implementation(implementation)),
				Offset = offset
			});
			return ManifestResourceHandle.FromRowId(this._manifestResourceTable.Count);
		}

		/// <param name="name" />
		/// <param name="hashValue" />
		/// <param name="containsMetadata" />
		// Token: 0x0600082F RID: 2095 RVA: 0x00014A9C File Offset: 0x00012C9C
		public AssemblyFileHandle AddAssemblyFile(StringHandle name, BlobHandle hashValue, bool containsMetadata)
		{
			this._fileTable.Add(new MetadataBuilder.FileTableRow
			{
				FileName = name,
				Flags = (containsMetadata ? 0U : 1U),
				HashValue = hashValue
			});
			return AssemblyFileHandle.FromRowId(this._fileTable.Count);
		}

		/// <summary>Adds an exported type.</summary>
		/// <param name="attributes">The type attributes.</param>
		/// <param name="namespace">The type namespace.</param>
		/// <param name="name">The type name.</param>
		/// <param name="implementation">The implementation entity handle, which can be one of the following: an <see cref="T:System.Reflection.Metadata.AssemblyFileHandle" />, <see cref="T:System.Reflection.Metadata.ExportedTypeHandle" />, or <see cref="T:System.Reflection.Metadata.AssemblyReferenceHandle" />.</param>
		/// <param name="typeDefinitionId">The type definition ID.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="implementation" /> doesn't have the expected handle kind.</exception>
		/// <returns>A handle to the added exported type.</returns>
		// Token: 0x06000830 RID: 2096 RVA: 0x00014AEC File Offset: 0x00012CEC
		public ExportedTypeHandle AddExportedType(TypeAttributes attributes, StringHandle @namespace, StringHandle name, EntityHandle implementation, int typeDefinitionId)
		{
			this._exportedTypeTable.Add(new MetadataBuilder.ExportedTypeRow
			{
				Flags = attributes,
				Implementation = CodedIndex.Implementation(implementation),
				TypeNamespace = @namespace,
				TypeName = name,
				TypeDefId = typeDefinitionId
			});
			return ExportedTypeHandle.FromRowId(this._exportedTypeTable.Count);
		}

		/// <summary>Adds a declarative security attribute to a type, method, or assembly.</summary>
		/// <param name="parent">The parent entity handle, which can be one of the following: a <see cref="T:System.Reflection.Metadata.TypeDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.MethodDefinitionHandle" />, or a <see cref="T:System.Reflection.Metadata.AssemblyDefinitionHandle" />.</param>
		/// <param name="action">A declarative security action.</param>
		/// <param name="permissionSet">The permission set blob.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="parent" /> doesn't have the expected handle kind.</exception>
		/// <returns>A handle to the added declarative security attribute.</returns>
		// Token: 0x06000831 RID: 2097 RVA: 0x00014B4C File Offset: 0x00012D4C
		public DeclarativeSecurityAttributeHandle AddDeclarativeSecurityAttribute(EntityHandle parent, DeclarativeSecurityAction action, BlobHandle permissionSet)
		{
			int num = CodedIndex.HasDeclSecurity(parent);
			this._declSecurityTableNeedsSorting |= num < this._declSecurityTableLastParent;
			this._declSecurityTableLastParent = num;
			this._declSecurityTable.Add(new MetadataBuilder.DeclSecurityRow
			{
				Parent = num,
				Action = (ushort)action,
				PermissionSet = permissionSet
			});
			return DeclarativeSecurityAttributeHandle.FromRowId(this._declSecurityTable.Count);
		}

		/// <param name="entity" />
		/// <param name="code" />
		// Token: 0x06000832 RID: 2098 RVA: 0x00014BBC File Offset: 0x00012DBC
		public void AddEncLogEntry(EntityHandle entity, EditAndContinueOperation code)
		{
			this._encLogTable.Add(new MetadataBuilder.EncLogRow
			{
				Token = entity.Token,
				FuncCode = (byte)code
			});
		}

		/// <param name="entity" />
		// Token: 0x06000833 RID: 2099 RVA: 0x00014BF4 File Offset: 0x00012DF4
		public void AddEncMapEntry(EntityHandle entity)
		{
			this._encMapTable.Add(new MetadataBuilder.EncMapRow
			{
				Token = entity.Token
			});
		}

		/// <summary>Adds document debug information.</summary>
		/// <param name="name">The document name blob.</param>
		/// <param name="hashAlgorithm">THe GUID of the hash algorithm used to calculate the value of <paramref name="hash" />.</param>
		/// <param name="hash">The hash of the document content.</param>
		/// <param name="language">The GUID of the language.</param>
		/// <returns>A handle to the added document.</returns>
		// Token: 0x06000834 RID: 2100 RVA: 0x00014C24 File Offset: 0x00012E24
		public DocumentHandle AddDocument(BlobHandle name, GuidHandle hashAlgorithm, BlobHandle hash, GuidHandle language)
		{
			this._documentTable.Add(new MetadataBuilder.DocumentRow
			{
				Name = name,
				HashAlgorithm = hashAlgorithm,
				Hash = hash,
				Language = language
			});
			return DocumentHandle.FromRowId(this._documentTable.Count);
		}

		/// <summary>Adds method debug information.</summary>
		/// <param name="document">The handle of a single document containing all sequence points of the method, or <see langword="null" /> if the method doesn't have sequence points or spans multiple documents.</param>
		/// <param name="sequencePoints">The sequence Points blob, or <see langword="null" /> if the method doesn't have sequence points.</param>
		/// <returns>A handle to the added method debug information.</returns>
		// Token: 0x06000835 RID: 2101 RVA: 0x00014C78 File Offset: 0x00012E78
		public MethodDebugInformationHandle AddMethodDebugInformation(DocumentHandle document, BlobHandle sequencePoints)
		{
			this._methodDebugInformationTable.Add(new MetadataBuilder.MethodDebugInformationRow
			{
				Document = document.RowId,
				SequencePoints = sequencePoints
			});
			return MethodDebugInformationHandle.FromRowId(this._methodDebugInformationTable.Count);
		}

		/// <summary>Adds local scope debug information.</summary>
		/// <param name="method">The containing method.</param>
		/// <param name="importScope">The handle of the associated import scope.</param>
		/// <param name="variableList">If the scope declares variables, set this to the handle of the first one. Otherwise, set this to the handle of the first variable declared by the next scope definition. If no scope defines any variables, <see langword="System.Reflection.Metadata.Ecma335.MetadataTokens.LocalVariableHandle(1)" />.</param>
		/// <param name="constantList">If the scope declares constants, set this the handle of the first one. Otherwise, set this to the handle of the first constant declared by the next scope definition. If no scope defines any constants, <see langword="System.Reflection.Metadata.Ecma335.MetadataTokens.LocalConstantHandle(1)" />.</param>
		/// <param name="startOffset">The offset of the first instruction covered by the scope.</param>
		/// <param name="length">The length (in bytes) of the scope.</param>
		/// <returns>A handle to the added local scope.</returns>
		// Token: 0x06000836 RID: 2102 RVA: 0x00014CC0 File Offset: 0x00012EC0
		public LocalScopeHandle AddLocalScope(MethodDefinitionHandle method, ImportScopeHandle importScope, LocalVariableHandle variableList, LocalConstantHandle constantList, int startOffset, int length)
		{
			this._localScopeTable.Add(new MetadataBuilder.LocalScopeRow
			{
				Method = method.RowId,
				ImportScope = importScope.RowId,
				VariableList = variableList.RowId,
				ConstantList = constantList.RowId,
				StartOffset = startOffset,
				Length = length
			});
			return LocalScopeHandle.FromRowId(this._localScopeTable.Count);
		}

		/// <summary>Adds local variable debug information.</summary>
		/// <param name="attributes">The local variable attributes.</param>
		/// <param name="index">The zero-base index of the local variable in the local signature.</param>
		/// <param name="name">The name of the variable.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is greater than <see cref="F:System.UInt16.MaxValue">UInt16.MaxValue</see>.</exception>
		/// <returns>A handle to the added local variable.</returns>
		// Token: 0x06000837 RID: 2103 RVA: 0x00014D3C File Offset: 0x00012F3C
		public LocalVariableHandle AddLocalVariable(LocalVariableAttributes attributes, int index, StringHandle name)
		{
			if (index > 65535)
			{
				Throw.ArgumentOutOfRange("index");
			}
			this._localVariableTable.Add(new MetadataBuilder.LocalVariableRow
			{
				Attributes = (ushort)attributes,
				Index = (ushort)index,
				Name = name
			});
			return LocalVariableHandle.FromRowId(this._localVariableTable.Count);
		}

		/// <summary>Adds local constant debug information.</summary>
		/// <param name="name">The name of the variable.</param>
		/// <param name="signature">The LocalConstantSig blob.</param>
		/// <returns>A handle to the added local constant.</returns>
		// Token: 0x06000838 RID: 2104 RVA: 0x00014D9C File Offset: 0x00012F9C
		public LocalConstantHandle AddLocalConstant(StringHandle name, BlobHandle signature)
		{
			this._localConstantTable.Add(new MetadataBuilder.LocalConstantRow
			{
				Name = name,
				Signature = signature
			});
			return LocalConstantHandle.FromRowId(this._localConstantTable.Count);
		}

		/// <summary>Adds local scope debug information.</summary>
		/// <param name="parentScope">The parent scope handle.</param>
		/// <param name="imports">The import scope handle.</param>
		/// <returns>A handle to the added import scope.</returns>
		// Token: 0x06000839 RID: 2105 RVA: 0x00014DE0 File Offset: 0x00012FE0
		public ImportScopeHandle AddImportScope(ImportScopeHandle parentScope, BlobHandle imports)
		{
			this._importScopeTable.Add(new MetadataBuilder.ImportScopeRow
			{
				Parent = parentScope.RowId,
				Imports = imports
			});
			return ImportScopeHandle.FromRowId(this._importScopeTable.Count);
		}

		/// <summary>Adds state machine method debug information.</summary>
		/// <param name="moveNextMethod">The handle of the <see langword="MoveNext" /> method of the state machine (the compiler-generated method).</param>
		/// <param name="kickoffMethod">The handle of the kickoff method (the user defined iterator/async method).</param>
		// Token: 0x0600083A RID: 2106 RVA: 0x00014E28 File Offset: 0x00013028
		public void AddStateMachineMethod(MethodDefinitionHandle moveNextMethod, MethodDefinitionHandle kickoffMethod)
		{
			this._stateMachineMethodTable.Add(new MetadataBuilder.StateMachineMethodRow
			{
				MoveNextMethod = moveNextMethod.RowId,
				KickoffMethod = kickoffMethod.RowId
			});
		}

		/// <summary>Adds custom debug information.</summary>
		/// <param name="parent">An entity to attach the debug information to: a <see cref="T:System.Reflection.Metadata.MethodDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.FieldDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.TypeReferenceHandle" />, <see cref="T:System.Reflection.Metadata.TypeDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.ParameterHandle" />, <see cref="T:System.Reflection.Metadata.InterfaceImplementationHandle" />, <see cref="T:System.Reflection.Metadata.MemberReferenceHandle" />, <see cref="T:System.Reflection.Metadata.ModuleDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.DeclarativeSecurityAttributeHandle" />, <see cref="T:System.Reflection.Metadata.PropertyDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.EventDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.StandaloneSignatureHandle" />, <see cref="T:System.Reflection.Metadata.ModuleReferenceHandle" />, <see cref="T:System.Reflection.Metadata.TypeSpecificationHandle" />, <see cref="T:System.Reflection.Metadata.AssemblyDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.AssemblyReferenceHandle" />, <see cref="T:System.Reflection.Metadata.AssemblyFileHandle" />, <see cref="T:System.Reflection.Metadata.ExportedTypeHandle" />, <see cref="T:System.Reflection.Metadata.ManifestResourceHandle" />, <see cref="T:System.Reflection.Metadata.GenericParameterHandle" />, <see cref="T:System.Reflection.Metadata.GenericParameterConstraintHandle" />, <see cref="T:System.Reflection.Metadata.MethodSpecificationHandle" />, <see cref="T:System.Reflection.Metadata.DocumentHandle" />, <see cref="T:System.Reflection.Metadata.LocalScopeHandle" />, <see cref="T:System.Reflection.Metadata.LocalVariableHandle" />, <see cref="T:System.Reflection.Metadata.LocalConstantHandle" />, or a <see cref="T:System.Reflection.Metadata.ImportScopeHandle" />.</param>
		/// <param name="kind">The information kind. Determines the structure of the <paramref name="value" /> blob.</param>
		/// <param name="value">The custom debug information blob.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="parent" /> doesn't have the expected handle kind.</exception>
		/// <returns>A handle to the added custom debug information.</returns>
		// Token: 0x0600083B RID: 2107 RVA: 0x00014E68 File Offset: 0x00013068
		public CustomDebugInformationHandle AddCustomDebugInformation(EntityHandle parent, GuidHandle kind, BlobHandle value)
		{
			this._customDebugInformationTable.Add(new MetadataBuilder.CustomDebugInformationRow
			{
				Parent = CodedIndex.HasCustomDebugInformation(parent),
				Kind = kind,
				Value = value
			});
			return CustomDebugInformationHandle.FromRowId(this._customDebugInformationTable.Count);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00014EB8 File Offset: 0x000130B8
		internal void ValidateOrder()
		{
			this.ValidateClassLayoutTable();
			this.ValidateFieldLayoutTable();
			this.ValidateFieldRvaTable();
			this.ValidateGenericParamTable();
			this.ValidateGenericParamConstaintTable();
			this.ValidateImplMapTable();
			this.ValidateInterfaceImplTable();
			this.ValidateMethodImplTable();
			this.ValidateNestedClassTable();
			this.ValidateLocalScopeTable();
			this.ValidateStateMachineMethodTable();
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00014F08 File Offset: 0x00013108
		private void ValidateClassLayoutTable()
		{
			for (int i = 1; i < this._classLayoutTable.Count; i++)
			{
				if (this._classLayoutTable[i - 1].Parent >= this._classLayoutTable[i].Parent)
				{
					Throw.InvalidOperation_TableNotSorted(TableIndex.ClassLayout);
				}
			}
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00014F58 File Offset: 0x00013158
		private void ValidateFieldLayoutTable()
		{
			for (int i = 1; i < this._fieldLayoutTable.Count; i++)
			{
				if (this._fieldLayoutTable[i - 1].Field >= this._fieldLayoutTable[i].Field)
				{
					Throw.InvalidOperation_TableNotSorted(TableIndex.FieldLayout);
				}
			}
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00014FA8 File Offset: 0x000131A8
		private void ValidateFieldRvaTable()
		{
			for (int i = 1; i < this._fieldRvaTable.Count; i++)
			{
				if (this._fieldRvaTable[i - 1].Field >= this._fieldRvaTable[i].Field)
				{
					Throw.InvalidOperation_TableNotSorted(TableIndex.FieldRva);
				}
			}
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00014FF8 File Offset: 0x000131F8
		private void ValidateGenericParamTable()
		{
			if (this._genericParamTable.Count == 0)
			{
				return;
			}
			MetadataBuilder.GenericParamRow genericParamRow = this._genericParamTable[0];
			int i = 1;
			while (i < this._genericParamTable.Count)
			{
				MetadataBuilder.GenericParamRow genericParamRow2 = this._genericParamTable[i];
				if (genericParamRow2.Owner <= genericParamRow.Owner && (genericParamRow.Owner != genericParamRow2.Owner || genericParamRow2.Number <= genericParamRow.Number))
				{
					Throw.InvalidOperation_TableNotSorted(TableIndex.GenericParam);
				}
				i++;
				genericParamRow = genericParamRow2;
			}
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00015078 File Offset: 0x00013278
		private void ValidateGenericParamConstaintTable()
		{
			for (int i = 1; i < this._genericParamConstraintTable.Count; i++)
			{
				if (this._genericParamConstraintTable[i - 1].Owner > this._genericParamConstraintTable[i].Owner)
				{
					Throw.InvalidOperation_TableNotSorted(TableIndex.GenericParamConstraint);
				}
			}
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x000150C8 File Offset: 0x000132C8
		private void ValidateImplMapTable()
		{
			for (int i = 1; i < this._implMapTable.Count; i++)
			{
				if (this._implMapTable[i - 1].MemberForwarded >= this._implMapTable[i].MemberForwarded)
				{
					Throw.InvalidOperation_TableNotSorted(TableIndex.ImplMap);
				}
			}
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00015118 File Offset: 0x00013318
		private void ValidateInterfaceImplTable()
		{
			if (this._interfaceImplTable.Count == 0)
			{
				return;
			}
			MetadataBuilder.InterfaceImplRow interfaceImplRow = this._interfaceImplTable[0];
			int i = 1;
			while (i < this._interfaceImplTable.Count)
			{
				MetadataBuilder.InterfaceImplRow interfaceImplRow2 = this._interfaceImplTable[i];
				if (interfaceImplRow2.Class <= interfaceImplRow.Class && (interfaceImplRow.Class != interfaceImplRow2.Class || interfaceImplRow2.Interface <= interfaceImplRow.Interface))
				{
					Throw.InvalidOperation_TableNotSorted(TableIndex.InterfaceImpl);
				}
				i++;
				interfaceImplRow = interfaceImplRow2;
			}
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00015198 File Offset: 0x00013398
		private void ValidateMethodImplTable()
		{
			for (int i = 1; i < this._methodImplTable.Count; i++)
			{
				if (this._methodImplTable[i - 1].Class > this._methodImplTable[i].Class)
				{
					Throw.InvalidOperation_TableNotSorted(TableIndex.MethodImpl);
				}
			}
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x000151E8 File Offset: 0x000133E8
		private void ValidateNestedClassTable()
		{
			for (int i = 1; i < this._nestedClassTable.Count; i++)
			{
				if (this._nestedClassTable[i - 1].NestedClass >= this._nestedClassTable[i].NestedClass)
				{
					Throw.InvalidOperation_TableNotSorted(TableIndex.NestedClass);
				}
			}
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00015238 File Offset: 0x00013438
		private void ValidateLocalScopeTable()
		{
			if (this._localScopeTable.Count == 0)
			{
				return;
			}
			MetadataBuilder.LocalScopeRow localScopeRow = this._localScopeTable[0];
			int i = 1;
			while (i < this._localScopeTable.Count)
			{
				MetadataBuilder.LocalScopeRow localScopeRow2 = this._localScopeTable[i];
				if (localScopeRow2.Method <= localScopeRow.Method && (localScopeRow2.Method != localScopeRow.Method || (localScopeRow2.StartOffset <= localScopeRow.StartOffset && (localScopeRow2.StartOffset != localScopeRow.StartOffset || localScopeRow.Length < localScopeRow2.Length))))
				{
					Throw.InvalidOperation_TableNotSorted(TableIndex.LocalScope);
				}
				i++;
				localScopeRow = localScopeRow2;
			}
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x000152D4 File Offset: 0x000134D4
		private void ValidateStateMachineMethodTable()
		{
			for (int i = 1; i < this._stateMachineMethodTable.Count; i++)
			{
				if (this._stateMachineMethodTable[i - 1].MoveNextMethod >= this._stateMachineMethodTable[i].MoveNextMethod)
				{
					Throw.InvalidOperation_TableNotSorted(TableIndex.StateMachineMethod);
				}
			}
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00015324 File Offset: 0x00013524
		internal void SerializeMetadataTables(BlobBuilder writer, MetadataSizes metadataSizes, [Nullable(0)] ImmutableArray<int> stringMap, int methodBodyStreamRva, int mappedFieldDataStreamRva)
		{
			int count = writer.Count;
			this.SerializeTablesHeader(writer, metadataSizes);
			if (metadataSizes.IsPresent(TableIndex.Module))
			{
				this.SerializeModuleTable(writer, stringMap, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.TypeRef))
			{
				this.SerializeTypeRefTable(writer, stringMap, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.TypeDef))
			{
				this.SerializeTypeDefTable(writer, stringMap, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.Field))
			{
				this.SerializeFieldTable(writer, stringMap, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.MethodDef))
			{
				this.SerializeMethodDefTable(writer, stringMap, metadataSizes, methodBodyStreamRva);
			}
			if (metadataSizes.IsPresent(TableIndex.Param))
			{
				this.SerializeParamTable(writer, stringMap, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.InterfaceImpl))
			{
				this.SerializeInterfaceImplTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.MemberRef))
			{
				this.SerializeMemberRefTable(writer, stringMap, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.Constant))
			{
				this.SerializeConstantTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.CustomAttribute))
			{
				this.SerializeCustomAttributeTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.FieldMarshal))
			{
				this.SerializeFieldMarshalTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.DeclSecurity))
			{
				this.SerializeDeclSecurityTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.ClassLayout))
			{
				this.SerializeClassLayoutTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.FieldLayout))
			{
				this.SerializeFieldLayoutTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.StandAloneSig))
			{
				this.SerializeStandAloneSigTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.EventMap))
			{
				this.SerializeEventMapTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.Event))
			{
				this.SerializeEventTable(writer, stringMap, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.PropertyMap))
			{
				this.SerializePropertyMapTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.Property))
			{
				this.SerializePropertyTable(writer, stringMap, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.MethodSemantics))
			{
				this.SerializeMethodSemanticsTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.MethodImpl))
			{
				this.SerializeMethodImplTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.ModuleRef))
			{
				this.SerializeModuleRefTable(writer, stringMap, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.TypeSpec))
			{
				this.SerializeTypeSpecTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.ImplMap))
			{
				this.SerializeImplMapTable(writer, stringMap, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.FieldRva))
			{
				this.SerializeFieldRvaTable(writer, metadataSizes, mappedFieldDataStreamRva);
			}
			if (metadataSizes.IsPresent(TableIndex.EncLog))
			{
				this.SerializeEncLogTable(writer);
			}
			if (metadataSizes.IsPresent(TableIndex.EncMap))
			{
				this.SerializeEncMapTable(writer);
			}
			if (metadataSizes.IsPresent(TableIndex.Assembly))
			{
				this.SerializeAssemblyTable(writer, stringMap, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.AssemblyRef))
			{
				this.SerializeAssemblyRefTable(writer, stringMap, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.File))
			{
				this.SerializeFileTable(writer, stringMap, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.ExportedType))
			{
				this.SerializeExportedTypeTable(writer, stringMap, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.ManifestResource))
			{
				this.SerializeManifestResourceTable(writer, stringMap, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.NestedClass))
			{
				this.SerializeNestedClassTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.GenericParam))
			{
				this.SerializeGenericParamTable(writer, stringMap, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.MethodSpec))
			{
				this.SerializeMethodSpecTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.GenericParamConstraint))
			{
				this.SerializeGenericParamConstraintTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.Document))
			{
				this.SerializeDocumentTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.MethodDebugInformation))
			{
				this.SerializeMethodDebugInformationTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.LocalScope))
			{
				this.SerializeLocalScopeTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.LocalVariable))
			{
				this.SerializeLocalVariableTable(writer, stringMap, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.LocalConstant))
			{
				this.SerializeLocalConstantTable(writer, stringMap, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.ImportScope))
			{
				this.SerializeImportScopeTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.StateMachineMethod))
			{
				this.SerializeStateMachineMethodTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.CustomDebugInformation))
			{
				this.SerializeCustomDebugInformationTable(writer, metadataSizes);
			}
			writer.WriteByte(0);
			writer.Align(4);
			int count2 = writer.Count;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0001567C File Offset: 0x0001387C
		private void SerializeTablesHeader(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			int count = writer.Count;
			HeapSizeFlag heapSizeFlag = (HeapSizeFlag)0;
			if (!metadataSizes.StringReferenceIsSmall)
			{
				heapSizeFlag |= HeapSizeFlag.StringHeapLarge;
			}
			if (!metadataSizes.GuidReferenceIsSmall)
			{
				heapSizeFlag |= HeapSizeFlag.GuidHeapLarge;
			}
			if (!metadataSizes.BlobReferenceIsSmall)
			{
				heapSizeFlag |= HeapSizeFlag.BlobHeapLarge;
			}
			if (metadataSizes.IsEncDelta)
			{
				heapSizeFlag |= (HeapSizeFlag)160;
			}
			ulong num = metadataSizes.PresentTablesMask & 55169095435288576UL;
			ulong num2 = num | (metadataSizes.IsStandaloneDebugMetadata ? 0UL : 24190111578624UL);
			writer.WriteUInt32(0U);
			writer.WriteByte(2);
			writer.WriteByte(0);
			writer.WriteByte((byte)heapSizeFlag);
			writer.WriteByte(1);
			writer.WriteUInt64(metadataSizes.PresentTablesMask);
			writer.WriteUInt64(num2);
			MetadataWriterUtilities.SerializeRowCounts(writer, metadataSizes.RowCounts);
			int count2 = writer.Count;
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0001573C File Offset: 0x0001393C
		internal void SerializeModuleTable(BlobBuilder writer, [Nullable(0)] ImmutableArray<int> stringMap, MetadataSizes metadataSizes)
		{
			if (this._moduleRow != null)
			{
				writer.WriteUInt16(this._moduleRow.Value.Generation);
				writer.WriteReference(this.SerializeHandle(stringMap, this._moduleRow.Value.Name), metadataSizes.StringReferenceIsSmall);
				writer.WriteReference(this.SerializeHandle(this._moduleRow.Value.ModuleVersionId), metadataSizes.GuidReferenceIsSmall);
				writer.WriteReference(this.SerializeHandle(this._moduleRow.Value.EncId), metadataSizes.GuidReferenceIsSmall);
				writer.WriteReference(this.SerializeHandle(this._moduleRow.Value.EncBaseId), metadataSizes.GuidReferenceIsSmall);
			}
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x000157F8 File Offset: 0x000139F8
		private void SerializeEncLogTable(BlobBuilder writer)
		{
			foreach (MetadataBuilder.EncLogRow encLogRow in this._encLogTable)
			{
				writer.WriteInt32(encLogRow.Token);
				writer.WriteUInt32((uint)encLogRow.FuncCode);
			}
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0001585C File Offset: 0x00013A5C
		private void SerializeEncMapTable(BlobBuilder writer)
		{
			foreach (MetadataBuilder.EncMapRow encMapRow in this._encMapTable)
			{
				writer.WriteInt32(encMapRow.Token);
			}
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x000158B4 File Offset: 0x00013AB4
		private void SerializeTypeRefTable(BlobBuilder writer, ImmutableArray<int> stringMap, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.TypeRefRow typeRefRow in this._typeRefTable)
			{
				writer.WriteReference(typeRefRow.ResolutionScope, metadataSizes.ResolutionScopeCodedIndexIsSmall);
				writer.WriteReference(this.SerializeHandle(stringMap, typeRefRow.Name), metadataSizes.StringReferenceIsSmall);
				writer.WriteReference(this.SerializeHandle(stringMap, typeRefRow.Namespace), metadataSizes.StringReferenceIsSmall);
			}
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00015944 File Offset: 0x00013B44
		private void SerializeTypeDefTable(BlobBuilder writer, ImmutableArray<int> stringMap, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.TypeDefRow typeDefRow in this._typeDefTable)
			{
				writer.WriteUInt32(typeDefRow.Flags);
				writer.WriteReference(this.SerializeHandle(stringMap, typeDefRow.Name), metadataSizes.StringReferenceIsSmall);
				writer.WriteReference(this.SerializeHandle(stringMap, typeDefRow.Namespace), metadataSizes.StringReferenceIsSmall);
				writer.WriteReference(typeDefRow.Extends, metadataSizes.TypeDefOrRefCodedIndexIsSmall);
				writer.WriteReference(typeDefRow.FieldList, metadataSizes.FieldDefReferenceIsSmall);
				writer.WriteReference(typeDefRow.MethodList, metadataSizes.MethodDefReferenceIsSmall);
			}
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00015A08 File Offset: 0x00013C08
		private void SerializeFieldTable(BlobBuilder writer, ImmutableArray<int> stringMap, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.FieldDefRow fieldDefRow in this._fieldTable)
			{
				writer.WriteUInt16(fieldDefRow.Flags);
				writer.WriteReference(this.SerializeHandle(stringMap, fieldDefRow.Name), metadataSizes.StringReferenceIsSmall);
				writer.WriteReference(this.SerializeHandle(fieldDefRow.Signature), metadataSizes.BlobReferenceIsSmall);
			}
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00015A94 File Offset: 0x00013C94
		private void SerializeMethodDefTable(BlobBuilder writer, ImmutableArray<int> stringMap, MetadataSizes metadataSizes, int methodBodyStreamRva)
		{
			foreach (MetadataBuilder.MethodRow methodRow in this._methodDefTable)
			{
				if (methodRow.BodyOffset == -1)
				{
					writer.WriteUInt32(0U);
				}
				else
				{
					writer.WriteInt32(methodBodyStreamRva + methodRow.BodyOffset);
				}
				writer.WriteUInt16(methodRow.ImplFlags);
				writer.WriteUInt16(methodRow.Flags);
				writer.WriteReference(this.SerializeHandle(stringMap, methodRow.Name), metadataSizes.StringReferenceIsSmall);
				writer.WriteReference(this.SerializeHandle(methodRow.Signature), metadataSizes.BlobReferenceIsSmall);
				writer.WriteReference(methodRow.ParamList, metadataSizes.ParameterReferenceIsSmall);
			}
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00015B64 File Offset: 0x00013D64
		private void SerializeParamTable(BlobBuilder writer, ImmutableArray<int> stringMap, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.ParamRow paramRow in this._paramTable)
			{
				writer.WriteUInt16(paramRow.Flags);
				writer.WriteUInt16(paramRow.Sequence);
				writer.WriteReference(this.SerializeHandle(stringMap, paramRow.Name), metadataSizes.StringReferenceIsSmall);
			}
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00015BE4 File Offset: 0x00013DE4
		private void SerializeInterfaceImplTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.InterfaceImplRow interfaceImplRow in this._interfaceImplTable)
			{
				writer.WriteReference(interfaceImplRow.Class, metadataSizes.TypeDefReferenceIsSmall);
				writer.WriteReference(interfaceImplRow.Interface, metadataSizes.TypeDefOrRefCodedIndexIsSmall);
			}
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00015C54 File Offset: 0x00013E54
		private void SerializeMemberRefTable(BlobBuilder writer, ImmutableArray<int> stringMap, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.MemberRefRow memberRefRow in this._memberRefTable)
			{
				writer.WriteReference(memberRefRow.Class, metadataSizes.MemberRefParentCodedIndexIsSmall);
				writer.WriteReference(this.SerializeHandle(stringMap, memberRefRow.Name), metadataSizes.StringReferenceIsSmall);
				writer.WriteReference(this.SerializeHandle(memberRefRow.Signature), metadataSizes.BlobReferenceIsSmall);
			}
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00015CE4 File Offset: 0x00013EE4
		private void SerializeConstantTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			IEnumerable<MetadataBuilder.ConstantRow> enumerable;
			if (!this._constantTableNeedsSorting)
			{
				IEnumerable<MetadataBuilder.ConstantRow> constantTable = this._constantTable;
				enumerable = constantTable;
			}
			else
			{
				enumerable = this._constantTable.OrderBy<MetadataBuilder.ConstantRow>((MetadataBuilder.ConstantRow x, MetadataBuilder.ConstantRow y) => x.Parent - y.Parent);
			}
			IEnumerable<MetadataBuilder.ConstantRow> enumerable2 = enumerable;
			foreach (MetadataBuilder.ConstantRow constantRow in enumerable2)
			{
				writer.WriteByte(constantRow.Type);
				writer.WriteByte(0);
				writer.WriteReference(constantRow.Parent, metadataSizes.HasConstantCodedIndexIsSmall);
				writer.WriteReference(this.SerializeHandle(constantRow.Value), metadataSizes.BlobReferenceIsSmall);
			}
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00015DA0 File Offset: 0x00013FA0
		private void SerializeCustomAttributeTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			IEnumerable<MetadataBuilder.CustomAttributeRow> enumerable;
			if (!this._customAttributeTableNeedsSorting)
			{
				IEnumerable<MetadataBuilder.CustomAttributeRow> customAttributeTable = this._customAttributeTable;
				enumerable = customAttributeTable;
			}
			else
			{
				enumerable = this._customAttributeTable.OrderBy<MetadataBuilder.CustomAttributeRow>((MetadataBuilder.CustomAttributeRow x, MetadataBuilder.CustomAttributeRow y) => x.Parent - y.Parent);
			}
			IEnumerable<MetadataBuilder.CustomAttributeRow> enumerable2 = enumerable;
			foreach (MetadataBuilder.CustomAttributeRow customAttributeRow in enumerable2)
			{
				writer.WriteReference(customAttributeRow.Parent, metadataSizes.HasCustomAttributeCodedIndexIsSmall);
				writer.WriteReference(customAttributeRow.Type, metadataSizes.CustomAttributeTypeCodedIndexIsSmall);
				writer.WriteReference(this.SerializeHandle(customAttributeRow.Value), metadataSizes.BlobReferenceIsSmall);
			}
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00015E5C File Offset: 0x0001405C
		private void SerializeFieldMarshalTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			IEnumerable<MetadataBuilder.FieldMarshalRow> enumerable;
			if (!this._fieldMarshalTableNeedsSorting)
			{
				IEnumerable<MetadataBuilder.FieldMarshalRow> fieldMarshalTable = this._fieldMarshalTable;
				enumerable = fieldMarshalTable;
			}
			else
			{
				enumerable = this._fieldMarshalTable.OrderBy<MetadataBuilder.FieldMarshalRow>((MetadataBuilder.FieldMarshalRow x, MetadataBuilder.FieldMarshalRow y) => x.Parent - y.Parent);
			}
			IEnumerable<MetadataBuilder.FieldMarshalRow> enumerable2 = enumerable;
			foreach (MetadataBuilder.FieldMarshalRow fieldMarshalRow in enumerable2)
			{
				writer.WriteReference(fieldMarshalRow.Parent, metadataSizes.HasFieldMarshalCodedIndexIsSmall);
				writer.WriteReference(this.SerializeHandle(fieldMarshalRow.NativeType), metadataSizes.BlobReferenceIsSmall);
			}
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00015F04 File Offset: 0x00014104
		private void SerializeDeclSecurityTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			IEnumerable<MetadataBuilder.DeclSecurityRow> enumerable;
			if (!this._declSecurityTableNeedsSorting)
			{
				IEnumerable<MetadataBuilder.DeclSecurityRow> declSecurityTable = this._declSecurityTable;
				enumerable = declSecurityTable;
			}
			else
			{
				enumerable = this._declSecurityTable.OrderBy<MetadataBuilder.DeclSecurityRow>((MetadataBuilder.DeclSecurityRow x, MetadataBuilder.DeclSecurityRow y) => x.Parent - y.Parent);
			}
			IEnumerable<MetadataBuilder.DeclSecurityRow> enumerable2 = enumerable;
			foreach (MetadataBuilder.DeclSecurityRow declSecurityRow in enumerable2)
			{
				writer.WriteUInt16(declSecurityRow.Action);
				writer.WriteReference(declSecurityRow.Parent, metadataSizes.DeclSecurityCodedIndexIsSmall);
				writer.WriteReference(this.SerializeHandle(declSecurityRow.PermissionSet), metadataSizes.BlobReferenceIsSmall);
			}
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00015FB8 File Offset: 0x000141B8
		private void SerializeClassLayoutTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.ClassLayoutRow classLayoutRow in this._classLayoutTable)
			{
				writer.WriteUInt16(classLayoutRow.PackingSize);
				writer.WriteUInt32(classLayoutRow.ClassSize);
				writer.WriteReference(classLayoutRow.Parent, metadataSizes.TypeDefReferenceIsSmall);
			}
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00016030 File Offset: 0x00014230
		private void SerializeFieldLayoutTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.FieldLayoutRow fieldLayoutRow in this._fieldLayoutTable)
			{
				writer.WriteInt32(fieldLayoutRow.Offset);
				writer.WriteReference(fieldLayoutRow.Field, metadataSizes.FieldDefReferenceIsSmall);
			}
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0001609C File Offset: 0x0001429C
		private void SerializeStandAloneSigTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.StandaloneSigRow standaloneSigRow in this._standAloneSigTable)
			{
				writer.WriteReference(this.SerializeHandle(standaloneSigRow.Signature), metadataSizes.BlobReferenceIsSmall);
			}
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00016100 File Offset: 0x00014300
		private void SerializeEventMapTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.EventMapRow eventMapRow in this._eventMapTable)
			{
				writer.WriteReference(eventMapRow.Parent, metadataSizes.TypeDefReferenceIsSmall);
				writer.WriteReference(eventMapRow.EventList, metadataSizes.EventDefReferenceIsSmall);
			}
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00016170 File Offset: 0x00014370
		private void SerializeEventTable(BlobBuilder writer, ImmutableArray<int> stringMap, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.EventRow eventRow in this._eventTable)
			{
				writer.WriteUInt16(eventRow.EventFlags);
				writer.WriteReference(this.SerializeHandle(stringMap, eventRow.Name), metadataSizes.StringReferenceIsSmall);
				writer.WriteReference(eventRow.EventType, metadataSizes.TypeDefOrRefCodedIndexIsSmall);
			}
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x000161F4 File Offset: 0x000143F4
		private void SerializePropertyMapTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.PropertyMapRow propertyMapRow in this._propertyMapTable)
			{
				writer.WriteReference(propertyMapRow.Parent, metadataSizes.TypeDefReferenceIsSmall);
				writer.WriteReference(propertyMapRow.PropertyList, metadataSizes.PropertyDefReferenceIsSmall);
			}
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00016264 File Offset: 0x00014464
		private void SerializePropertyTable(BlobBuilder writer, ImmutableArray<int> stringMap, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.PropertyRow propertyRow in this._propertyTable)
			{
				writer.WriteUInt16(propertyRow.PropFlags);
				writer.WriteReference(this.SerializeHandle(stringMap, propertyRow.Name), metadataSizes.StringReferenceIsSmall);
				writer.WriteReference(this.SerializeHandle(propertyRow.Type), metadataSizes.BlobReferenceIsSmall);
			}
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x000162F0 File Offset: 0x000144F0
		private void SerializeMethodSemanticsTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			IEnumerable<MetadataBuilder.MethodSemanticsRow> enumerable;
			if (!this._methodSemanticsTableNeedsSorting)
			{
				IEnumerable<MetadataBuilder.MethodSemanticsRow> methodSemanticsTable = this._methodSemanticsTable;
				enumerable = methodSemanticsTable;
			}
			else
			{
				enumerable = this._methodSemanticsTable.OrderBy<MetadataBuilder.MethodSemanticsRow>((MetadataBuilder.MethodSemanticsRow x, MetadataBuilder.MethodSemanticsRow y) => x.Association - y.Association);
			}
			IEnumerable<MetadataBuilder.MethodSemanticsRow> enumerable2 = enumerable;
			foreach (MetadataBuilder.MethodSemanticsRow methodSemanticsRow in enumerable2)
			{
				writer.WriteUInt16(methodSemanticsRow.Semantic);
				writer.WriteReference(methodSemanticsRow.Method, metadataSizes.MethodDefReferenceIsSmall);
				writer.WriteReference(methodSemanticsRow.Association, metadataSizes.HasSemanticsCodedIndexIsSmall);
			}
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x000163A0 File Offset: 0x000145A0
		private void SerializeMethodImplTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.MethodImplRow methodImplRow in this._methodImplTable)
			{
				writer.WriteReference(methodImplRow.Class, metadataSizes.TypeDefReferenceIsSmall);
				writer.WriteReference(methodImplRow.MethodBody, metadataSizes.MethodDefOrRefCodedIndexIsSmall);
				writer.WriteReference(methodImplRow.MethodDecl, metadataSizes.MethodDefOrRefCodedIndexIsSmall);
			}
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00016424 File Offset: 0x00014624
		private void SerializeModuleRefTable(BlobBuilder writer, ImmutableArray<int> stringMap, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.ModuleRefRow moduleRefRow in this._moduleRefTable)
			{
				writer.WriteReference(this.SerializeHandle(stringMap, moduleRefRow.Name), metadataSizes.StringReferenceIsSmall);
			}
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0001648C File Offset: 0x0001468C
		private void SerializeTypeSpecTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.TypeSpecRow typeSpecRow in this._typeSpecTable)
			{
				writer.WriteReference(this.SerializeHandle(typeSpecRow.Signature), metadataSizes.BlobReferenceIsSmall);
			}
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x000164F0 File Offset: 0x000146F0
		private void SerializeImplMapTable(BlobBuilder writer, ImmutableArray<int> stringMap, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.ImplMapRow implMapRow in this._implMapTable)
			{
				writer.WriteUInt16(implMapRow.MappingFlags);
				writer.WriteReference(implMapRow.MemberForwarded, metadataSizes.MemberForwardedCodedIndexIsSmall);
				writer.WriteReference(this.SerializeHandle(stringMap, implMapRow.ImportName), metadataSizes.StringReferenceIsSmall);
				writer.WriteReference(implMapRow.ImportScope, metadataSizes.ModuleRefReferenceIsSmall);
			}
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00016588 File Offset: 0x00014788
		private void SerializeFieldRvaTable(BlobBuilder writer, MetadataSizes metadataSizes, int mappedFieldDataStreamRva)
		{
			foreach (MetadataBuilder.FieldRvaRow fieldRvaRow in this._fieldRvaTable)
			{
				writer.WriteInt32(mappedFieldDataStreamRva + fieldRvaRow.Offset);
				writer.WriteReference(fieldRvaRow.Field, metadataSizes.FieldDefReferenceIsSmall);
			}
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x000165F4 File Offset: 0x000147F4
		private void SerializeAssemblyTable(BlobBuilder writer, ImmutableArray<int> stringMap, MetadataSizes metadataSizes)
		{
			if (this._assemblyRow != null)
			{
				Version version = this._assemblyRow.Value.Version;
				writer.WriteUInt32(this._assemblyRow.Value.HashAlgorithm);
				writer.WriteUInt16((ushort)version.Major);
				writer.WriteUInt16((ushort)version.Minor);
				writer.WriteUInt16((ushort)version.Build);
				writer.WriteUInt16((ushort)version.Revision);
				writer.WriteUInt32((uint)this._assemblyRow.Value.Flags);
				writer.WriteReference(this.SerializeHandle(this._assemblyRow.Value.AssemblyKey), metadataSizes.BlobReferenceIsSmall);
				writer.WriteReference(this.SerializeHandle(stringMap, this._assemblyRow.Value.AssemblyName), metadataSizes.StringReferenceIsSmall);
				writer.WriteReference(this.SerializeHandle(stringMap, this._assemblyRow.Value.AssemblyCulture), metadataSizes.StringReferenceIsSmall);
			}
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x000166EC File Offset: 0x000148EC
		private void SerializeAssemblyRefTable(BlobBuilder writer, ImmutableArray<int> stringMap, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.AssemblyRefTableRow assemblyRefTableRow in this._assemblyRefTable)
			{
				writer.WriteUInt16((ushort)assemblyRefTableRow.Version.Major);
				writer.WriteUInt16((ushort)assemblyRefTableRow.Version.Minor);
				writer.WriteUInt16((ushort)assemblyRefTableRow.Version.Build);
				writer.WriteUInt16((ushort)assemblyRefTableRow.Version.Revision);
				writer.WriteUInt32(assemblyRefTableRow.Flags);
				writer.WriteReference(this.SerializeHandle(assemblyRefTableRow.PublicKeyToken), metadataSizes.BlobReferenceIsSmall);
				writer.WriteReference(this.SerializeHandle(stringMap, assemblyRefTableRow.Name), metadataSizes.StringReferenceIsSmall);
				writer.WriteReference(this.SerializeHandle(stringMap, assemblyRefTableRow.Culture), metadataSizes.StringReferenceIsSmall);
				writer.WriteReference(this.SerializeHandle(assemblyRefTableRow.HashValue), metadataSizes.BlobReferenceIsSmall);
			}
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x000167F4 File Offset: 0x000149F4
		private void SerializeFileTable(BlobBuilder writer, ImmutableArray<int> stringMap, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.FileTableRow fileTableRow in this._fileTable)
			{
				writer.WriteUInt32(fileTableRow.Flags);
				writer.WriteReference(this.SerializeHandle(stringMap, fileTableRow.FileName), metadataSizes.StringReferenceIsSmall);
				writer.WriteReference(this.SerializeHandle(fileTableRow.HashValue), metadataSizes.BlobReferenceIsSmall);
			}
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00016880 File Offset: 0x00014A80
		private void SerializeExportedTypeTable(BlobBuilder writer, ImmutableArray<int> stringMap, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.ExportedTypeRow exportedTypeRow in this._exportedTypeTable)
			{
				writer.WriteUInt32(exportedTypeRow.Flags);
				writer.WriteInt32(exportedTypeRow.TypeDefId);
				writer.WriteReference(this.SerializeHandle(stringMap, exportedTypeRow.TypeName), metadataSizes.StringReferenceIsSmall);
				writer.WriteReference(this.SerializeHandle(stringMap, exportedTypeRow.TypeNamespace), metadataSizes.StringReferenceIsSmall);
				writer.WriteReference(exportedTypeRow.Implementation, metadataSizes.ImplementationCodedIndexIsSmall);
			}
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00016928 File Offset: 0x00014B28
		private void SerializeManifestResourceTable(BlobBuilder writer, ImmutableArray<int> stringMap, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.ManifestResourceRow manifestResourceRow in this._manifestResourceTable)
			{
				writer.WriteUInt32(manifestResourceRow.Offset);
				writer.WriteUInt32(manifestResourceRow.Flags);
				writer.WriteReference(this.SerializeHandle(stringMap, manifestResourceRow.Name), metadataSizes.StringReferenceIsSmall);
				writer.WriteReference(manifestResourceRow.Implementation, metadataSizes.ImplementationCodedIndexIsSmall);
			}
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x000169B8 File Offset: 0x00014BB8
		private void SerializeNestedClassTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.NestedClassRow nestedClassRow in this._nestedClassTable)
			{
				writer.WriteReference(nestedClassRow.NestedClass, metadataSizes.TypeDefReferenceIsSmall);
				writer.WriteReference(nestedClassRow.EnclosingClass, metadataSizes.TypeDefReferenceIsSmall);
			}
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00016A28 File Offset: 0x00014C28
		private void SerializeGenericParamTable(BlobBuilder writer, ImmutableArray<int> stringMap, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.GenericParamRow genericParamRow in this._genericParamTable)
			{
				writer.WriteUInt16(genericParamRow.Number);
				writer.WriteUInt16(genericParamRow.Flags);
				writer.WriteReference(genericParamRow.Owner, metadataSizes.TypeOrMethodDefCodedIndexIsSmall);
				writer.WriteReference(this.SerializeHandle(stringMap, genericParamRow.Name), metadataSizes.StringReferenceIsSmall);
			}
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00016AB8 File Offset: 0x00014CB8
		private void SerializeGenericParamConstraintTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.GenericParamConstraintRow genericParamConstraintRow in this._genericParamConstraintTable)
			{
				writer.WriteReference(genericParamConstraintRow.Owner, metadataSizes.GenericParamReferenceIsSmall);
				writer.WriteReference(genericParamConstraintRow.Constraint, metadataSizes.TypeDefOrRefCodedIndexIsSmall);
			}
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00016B28 File Offset: 0x00014D28
		private void SerializeMethodSpecTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.MethodSpecRow methodSpecRow in this._methodSpecTable)
			{
				writer.WriteReference(methodSpecRow.Method, metadataSizes.MethodDefOrRefCodedIndexIsSmall);
				writer.WriteReference(this.SerializeHandle(methodSpecRow.Instantiation), metadataSizes.BlobReferenceIsSmall);
			}
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00016BA0 File Offset: 0x00014DA0
		private void SerializeDocumentTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.DocumentRow documentRow in this._documentTable)
			{
				writer.WriteReference(this.SerializeHandle(documentRow.Name), metadataSizes.BlobReferenceIsSmall);
				writer.WriteReference(this.SerializeHandle(documentRow.HashAlgorithm), metadataSizes.GuidReferenceIsSmall);
				writer.WriteReference(this.SerializeHandle(documentRow.Hash), metadataSizes.BlobReferenceIsSmall);
				writer.WriteReference(this.SerializeHandle(documentRow.Language), metadataSizes.GuidReferenceIsSmall);
			}
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00016C4C File Offset: 0x00014E4C
		private void SerializeMethodDebugInformationTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.MethodDebugInformationRow methodDebugInformationRow in this._methodDebugInformationTable)
			{
				writer.WriteReference(methodDebugInformationRow.Document, metadataSizes.DocumentReferenceIsSmall);
				writer.WriteReference(this.SerializeHandle(methodDebugInformationRow.SequencePoints), metadataSizes.BlobReferenceIsSmall);
			}
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00016CC4 File Offset: 0x00014EC4
		private void SerializeLocalScopeTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.LocalScopeRow localScopeRow in this._localScopeTable)
			{
				writer.WriteReference(localScopeRow.Method, metadataSizes.MethodDefReferenceIsSmall);
				writer.WriteReference(localScopeRow.ImportScope, metadataSizes.ImportScopeReferenceIsSmall);
				writer.WriteReference(localScopeRow.VariableList, metadataSizes.LocalVariableReferenceIsSmall);
				writer.WriteReference(localScopeRow.ConstantList, metadataSizes.LocalConstantReferenceIsSmall);
				writer.WriteInt32(localScopeRow.StartOffset);
				writer.WriteInt32(localScopeRow.Length);
			}
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00016D70 File Offset: 0x00014F70
		private void SerializeLocalVariableTable(BlobBuilder writer, ImmutableArray<int> stringMap, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.LocalVariableRow localVariableRow in this._localVariableTable)
			{
				writer.WriteUInt16(localVariableRow.Attributes);
				writer.WriteUInt16(localVariableRow.Index);
				writer.WriteReference(this.SerializeHandle(stringMap, localVariableRow.Name), metadataSizes.StringReferenceIsSmall);
			}
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00016DF0 File Offset: 0x00014FF0
		private void SerializeLocalConstantTable(BlobBuilder writer, ImmutableArray<int> stringMap, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.LocalConstantRow localConstantRow in this._localConstantTable)
			{
				writer.WriteReference(this.SerializeHandle(stringMap, localConstantRow.Name), metadataSizes.StringReferenceIsSmall);
				writer.WriteReference(this.SerializeHandle(localConstantRow.Signature), metadataSizes.BlobReferenceIsSmall);
			}
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00016E70 File Offset: 0x00015070
		private void SerializeImportScopeTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.ImportScopeRow importScopeRow in this._importScopeTable)
			{
				writer.WriteReference(importScopeRow.Parent, metadataSizes.ImportScopeReferenceIsSmall);
				writer.WriteReference(this.SerializeHandle(importScopeRow.Imports), metadataSizes.BlobReferenceIsSmall);
			}
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00016EE8 File Offset: 0x000150E8
		private void SerializeStateMachineMethodTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.StateMachineMethodRow stateMachineMethodRow in this._stateMachineMethodTable)
			{
				writer.WriteReference(stateMachineMethodRow.MoveNextMethod, metadataSizes.MethodDefReferenceIsSmall);
				writer.WriteReference(stateMachineMethodRow.KickoffMethod, metadataSizes.MethodDefReferenceIsSmall);
			}
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00016F58 File Offset: 0x00015158
		private void SerializeCustomDebugInformationTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.CustomDebugInformationRow customDebugInformationRow in this._customDebugInformationTable.OrderBy<MetadataBuilder.CustomDebugInformationRow>(delegate(MetadataBuilder.CustomDebugInformationRow x, MetadataBuilder.CustomDebugInformationRow y)
			{
				int num = x.Parent - y.Parent;
				if (num == 0)
				{
					return x.Kind.Index - y.Kind.Index;
				}
				return num;
			}))
			{
				writer.WriteReference(customDebugInformationRow.Parent, metadataSizes.HasCustomDebugInformationCodedIndexIsSmall);
				writer.WriteReference(this.SerializeHandle(customDebugInformationRow.Kind), metadataSizes.GuidReferenceIsSmall);
				writer.WriteReference(this.SerializeHandle(customDebugInformationRow.Value), metadataSizes.BlobReferenceIsSmall);
			}
		}

		/// <summary>Creates a builder for metadata tables and heaps.</summary>
		/// <param name="userStringHeapStartOffset">The start offset of the User String heap. The cumulative size of User String heaps of all previous EnC generations should be 0 unless the metadata is EnC delta metadata.</param>
		/// <param name="stringHeapStartOffset">The start offset of the String heap. The cumulative size of String heaps of all previous EnC generations should be 0 unless the metadata is EnC delta metadata.</param>
		/// <param name="blobHeapStartOffset">The start offset of the Blob heap. The cumulative size of Blob heaps of all previous EnC generations should be 0 unless the metadata is EnC delta metadata.</param>
		/// <param name="guidHeapStartOffset">The start offset of the Guid heap. The cumulative size of Guid heaps of all previous EnC generations should be 0 unless the metadata is EnC delta metadata.</param>
		/// <exception cref="T:System.Reflection.Metadata.ImageFormatLimitationException">Offset is too big.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Offset is negative.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="guidHeapStartOffset" /> is not a multiple of size of GUID.</exception>
		// Token: 0x06000876 RID: 2166 RVA: 0x00017004 File Offset: 0x00015204
		public MetadataBuilder(int userStringHeapStartOffset = 0, int stringHeapStartOffset = 0, int blobHeapStartOffset = 0, int guidHeapStartOffset = 0)
		{
			if (userStringHeapStartOffset >= 16777215)
			{
				Throw.HeapSizeLimitExceeded(HeapIndex.UserString);
			}
			if (userStringHeapStartOffset < 0)
			{
				Throw.ArgumentOutOfRange("userStringHeapStartOffset");
			}
			if (stringHeapStartOffset < 0)
			{
				Throw.ArgumentOutOfRange("stringHeapStartOffset");
			}
			if (blobHeapStartOffset < 0)
			{
				Throw.ArgumentOutOfRange("blobHeapStartOffset");
			}
			if (guidHeapStartOffset < 0)
			{
				Throw.ArgumentOutOfRange("guidHeapStartOffset");
			}
			if (guidHeapStartOffset % 16 != 0)
			{
				throw new ArgumentException(SR.Format(SR.ValueMustBeMultiple, 16), "guidHeapStartOffset");
			}
			this._userStringBuilder.WriteByte(0);
			this._blobs.Add(ImmutableArray<byte>.Empty, default(BlobHandle));
			this._blobHeapSize = 1;
			this._userStringHeapStartOffset = userStringHeapStartOffset;
			this._stringHeapStartOffset = stringHeapStartOffset;
			this._blobHeapStartOffset = blobHeapStartOffset;
			this._guidBuilder.WriteBytes(0, guidHeapStartOffset);
		}

		/// <summary>Sets the capacity of the specified heap.</summary>
		/// <param name="heap">The heap index.</param>
		/// <param name="byteCount">The number of bytes.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="heap" /> is not a valid heap index.
		///
		/// -or-
		///
		/// <paramref name="byteCount" /> is negative.</exception>
		// Token: 0x06000877 RID: 2167 RVA: 0x00017308 File Offset: 0x00015508
		public void SetCapacity(HeapIndex heap, int byteCount)
		{
			if (byteCount < 0)
			{
				Throw.ArgumentOutOfRange("byteCount");
			}
			switch (heap)
			{
			case HeapIndex.UserString:
				this._userStringBuilder.SetCapacity(byteCount);
				return;
			case HeapIndex.String:
				this._stringHeapCapacity = byteCount;
				return;
			case HeapIndex.Blob:
				break;
			case HeapIndex.Guid:
				this._guidBuilder.SetCapacity(byteCount);
				return;
			default:
				Throw.ArgumentOutOfRange("heap");
				break;
			}
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00017367 File Offset: 0x00015567
		[NullableContext(0)]
		internal int SerializeHandle(ImmutableArray<int> map, StringHandle handle)
		{
			return map[handle.GetWriterVirtualIndex()];
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00017377 File Offset: 0x00015577
		internal int SerializeHandle(BlobHandle handle)
		{
			return handle.GetHeapOffset();
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00017380 File Offset: 0x00015580
		internal int SerializeHandle(GuidHandle handle)
		{
			return handle.Index;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00017389 File Offset: 0x00015589
		internal int SerializeHandle(UserStringHandle handle)
		{
			return handle.GetHeapOffset();
		}

		/// <summary>Adds the specified blob from an immutable byte array to the Blob heap, if it's not there already.</summary>
		/// <param name="value">The blob builder instance containing the blob.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <returns>A handle to the added or existing blob.</returns>
		// Token: 0x0600087C RID: 2172 RVA: 0x00017392 File Offset: 0x00015592
		public BlobHandle GetOrAddBlob(BlobBuilder value)
		{
			if (value == null)
			{
				Throw.ArgumentNull("value");
			}
			return this.GetOrAddBlob(value.ToImmutableArray());
		}

		/// <summary>Adds the specified blob to the Blob heap, if it's not there already.</summary>
		/// <param name="value">The array containing the blob.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <returns>A handle to the added or existing blob.</returns>
		// Token: 0x0600087D RID: 2173 RVA: 0x000173AD File Offset: 0x000155AD
		public BlobHandle GetOrAddBlob(byte[] value)
		{
			if (value == null)
			{
				Throw.ArgumentNull("value");
			}
			return this.GetOrAddBlob(ImmutableArray.Create<byte>(value));
		}

		/// <summary>Adds the specified blob from a byte array to the Blob heap, if it's not there already.</summary>
		/// <param name="value">The array containing the blob.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <returns>A handle to the added or existing blob.</returns>
		// Token: 0x0600087E RID: 2174 RVA: 0x000173C8 File Offset: 0x000155C8
		[NullableContext(0)]
		public BlobHandle GetOrAddBlob(ImmutableArray<byte> value)
		{
			if (value.IsDefault)
			{
				Throw.ArgumentNull("value");
			}
			BlobHandle blobHandle;
			if (!this._blobs.TryGetValue(value, ref blobHandle))
			{
				blobHandle = BlobHandle.FromOffset(this._blobHeapStartOffset + this._blobHeapSize);
				this._blobs.Add(value, blobHandle);
				this._blobHeapSize += BlobWriterImpl.GetCompressedIntegerSize(value.Length) + value.Length;
			}
			return blobHandle;
		}

		/// <summary>Encodes a constant value to a blob and adds it to the Blob heap, if it's not there already. Uses UTF16 to encode string constants.</summary>
		/// <param name="value">The constant value to add.</param>
		/// <returns>A handle to the added or existing blob.</returns>
		// Token: 0x0600087F RID: 2175 RVA: 0x0001743C File Offset: 0x0001563C
		[NullableContext(2)]
		public BlobHandle GetOrAddConstantBlob(object value)
		{
			string text = value as string;
			if (text != null)
			{
				return this.GetOrAddBlobUTF16(text);
			}
			PooledBlobBuilder instance = PooledBlobBuilder.GetInstance();
			instance.WriteConstant(value);
			BlobHandle orAddBlob = this.GetOrAddBlob(instance);
			instance.Free();
			return orAddBlob;
		}

		/// <summary>Encodes a string using UTF16 encoding to a blob and adds it to the Blob heap, if it's not there already.</summary>
		/// <param name="value">The string to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <returns>A handle to the added or existing blob.</returns>
		// Token: 0x06000880 RID: 2176 RVA: 0x00017478 File Offset: 0x00015678
		public BlobHandle GetOrAddBlobUTF16(string value)
		{
			PooledBlobBuilder instance = PooledBlobBuilder.GetInstance();
			instance.WriteUTF16(value);
			BlobHandle orAddBlob = this.GetOrAddBlob(instance);
			instance.Free();
			return orAddBlob;
		}

		/// <summary>Encodes a string using UTF8 encoding to a blob and adds it to the Blob heap, if it's not there already.</summary>
		/// <param name="value">The value to add.</param>
		/// <param name="allowUnpairedSurrogates">
		///   <see langword="true" /> to encode the unpaired surrogates as specified; <see langword="false" /> to replace them with the U+FFFD character.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <returns>A handle to the added or existing blob.</returns>
		// Token: 0x06000881 RID: 2177 RVA: 0x000174A4 File Offset: 0x000156A4
		public BlobHandle GetOrAddBlobUTF8(string value, bool allowUnpairedSurrogates = true)
		{
			PooledBlobBuilder instance = PooledBlobBuilder.GetInstance();
			instance.WriteUTF8(value, allowUnpairedSurrogates);
			BlobHandle orAddBlob = this.GetOrAddBlob(instance);
			instance.Free();
			return orAddBlob;
		}

		/// <summary>Encodes a debug document name and adds it to the Blob heap, if it's not there already.</summary>
		/// <param name="value">The document name to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <returns>A handle to the added or existing document name blob.</returns>
		// Token: 0x06000882 RID: 2178 RVA: 0x000174D0 File Offset: 0x000156D0
		public BlobHandle GetOrAddDocumentName(string value)
		{
			if (value == null)
			{
				Throw.ArgumentNull("value");
			}
			char c = MetadataBuilder.ChooseSeparator(value);
			PooledBlobBuilder instance = PooledBlobBuilder.GetInstance();
			instance.WriteByte((byte)c);
			PooledBlobBuilder instance2 = PooledBlobBuilder.GetInstance();
			int num = 0;
			for (;;)
			{
				int num2 = value.IndexOf(c, num);
				instance2.WriteUTF8(value, num, ((num2 >= 0) ? num2 : value.Length) - num, true, false);
				instance.WriteCompressedInteger(this.GetOrAddBlob(instance2).GetHeapOffset());
				if (num2 == -1)
				{
					goto IL_008C;
				}
				if (num2 == value.Length - 1)
				{
					break;
				}
				instance2.Clear();
				num = num2 + 1;
			}
			instance.WriteByte(0);
			IL_008C:
			instance2.Free();
			BlobHandle orAddBlob = this.GetOrAddBlob(instance);
			instance.Free();
			return orAddBlob;
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00017580 File Offset: 0x00015780
		private static char ChooseSeparator(string str)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < str.Length; i++)
			{
				char c = str.get_Chars(i);
				if (c == '/')
				{
					num++;
				}
				else if (c == '\\')
				{
					num2++;
				}
			}
			if (num < num2)
			{
				return '\\';
			}
			return '/';
		}

		/// <summary>Adds the specified Guid to the Guid heap, if it's not there already.</summary>
		/// <param name="guid">The Guid to add.</param>
		/// <returns>A handle to the added or existing Guid.</returns>
		// Token: 0x06000884 RID: 2180 RVA: 0x000175CC File Offset: 0x000157CC
		public GuidHandle GetOrAddGuid(Guid guid)
		{
			if (guid == Guid.Empty)
			{
				return default(GuidHandle);
			}
			GuidHandle newGuidHandle;
			if (this._guids.TryGetValue(guid, ref newGuidHandle))
			{
				return newGuidHandle;
			}
			newGuidHandle = this.GetNewGuidHandle();
			this._guids.Add(guid, newGuidHandle);
			this._guidBuilder.WriteGuid(guid);
			return newGuidHandle;
		}

		/// <summary>Reserves space on the Guid heap for a GUID.</summary>
		/// <exception cref="T:System.Reflection.Metadata.ImageFormatLimitationException">The remaining space on the heap is too small to fit the string.</exception>
		/// <returns>A handle to the reserved Guid and a <see cref="T:System.Reflection.Metadata.Blob" /> representing the GUID blob as stored on the heap.</returns>
		// Token: 0x06000885 RID: 2181 RVA: 0x00017624 File Offset: 0x00015824
		[NullableContext(0)]
		public ReservedBlob<GuidHandle> ReserveGuid()
		{
			GuidHandle newGuidHandle = this.GetNewGuidHandle();
			Blob blob = this._guidBuilder.ReserveBytes(16);
			return new ReservedBlob<GuidHandle>(newGuidHandle, blob);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0001764D File Offset: 0x0001584D
		private GuidHandle GetNewGuidHandle()
		{
			return GuidHandle.FromIndex((this._guidBuilder.Count >> 4) + 1);
		}

		/// <summary>Adds the specified string to the string heap, if it's not there already.</summary>
		/// <param name="value">The string to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <returns>A handle to the added or existing string.</returns>
		// Token: 0x06000887 RID: 2183 RVA: 0x00017664 File Offset: 0x00015864
		public StringHandle GetOrAddString(string value)
		{
			if (value == null)
			{
				Throw.ArgumentNull("value");
			}
			StringHandle stringHandle;
			if (value.Length == 0)
			{
				stringHandle = default(StringHandle);
			}
			else if (!this._strings.TryGetValue(value, ref stringHandle))
			{
				stringHandle = StringHandle.FromWriterVirtualIndex(this._strings.Count + 1);
				this._strings.Add(value, stringHandle);
			}
			return stringHandle;
		}

		/// <summary>Reserves space on the user string heap for a string of the specified length.</summary>
		/// <param name="length">The number of characters to reserve.</param>
		/// <exception cref="T:System.Reflection.Metadata.ImageFormatLimitationException">The remaining space on the heap is too small to fit the string.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="length" /> is negative.</exception>
		/// <returns>A handle to the reserved user string and a <see cref="T:System.Reflection.Metadata.Blob" /> representing the entire User String blob (including its length and terminal character). The handle may be used in <see cref="M:System.Reflection.Metadata.Ecma335.InstructionEncoder.LoadString(System.Reflection.Metadata.UserStringHandle)" />.
		///  Use <see cref="M:System.Reflection.Metadata.BlobWriter.WriteUserString(System.String)" /> to fill in the blob content.</returns>
		// Token: 0x06000888 RID: 2184 RVA: 0x000176C4 File Offset: 0x000158C4
		[NullableContext(0)]
		public ReservedBlob<UserStringHandle> ReserveUserString(int length)
		{
			if (length < 0)
			{
				Throw.ArgumentOutOfRange("length");
			}
			UserStringHandle newUserStringHandle = this.GetNewUserStringHandle();
			int userStringByteLength = BlobUtilities.GetUserStringByteLength(length);
			Blob blob = this._userStringBuilder.ReserveBytes(BlobWriterImpl.GetCompressedIntegerSize(userStringByteLength) + userStringByteLength);
			return new ReservedBlob<UserStringHandle>(newUserStringHandle, blob);
		}

		/// <summary>Adds the specified string to the user string heap, if it's not there already.</summary>
		/// <param name="value">The string to add.</param>
		/// <exception cref="T:System.Reflection.Metadata.ImageFormatLimitationException">The remaining space on the heap is too small to fit the string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <returns>A handle to the added or existing string. This value may be used in <see cref="M:System.Reflection.Metadata.Ecma335.InstructionEncoder.LoadString(System.Reflection.Metadata.UserStringHandle)" />.</returns>
		// Token: 0x06000889 RID: 2185 RVA: 0x00017708 File Offset: 0x00015908
		public UserStringHandle GetOrAddUserString(string value)
		{
			if (value == null)
			{
				Throw.ArgumentNull("value");
			}
			UserStringHandle newUserStringHandle;
			if (!this._userStrings.TryGetValue(value, ref newUserStringHandle))
			{
				newUserStringHandle = this.GetNewUserStringHandle();
				this._userStrings.Add(value, newUserStringHandle);
				this._userStringBuilder.WriteUserString(value);
			}
			return newUserStringHandle;
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00017754 File Offset: 0x00015954
		private UserStringHandle GetNewUserStringHandle()
		{
			int num = this._userStringHeapStartOffset + this._userStringBuilder.Count;
			if (num >= 16777216)
			{
				Throw.HeapSizeLimitExceeded(HeapIndex.UserString);
			}
			return UserStringHandle.FromOffset(num);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00017788 File Offset: 0x00015988
		private static ImmutableArray<int> SerializeStringHeap(BlobBuilder heapBuilder, Dictionary<string, StringHandle> strings, int stringHeapStartOffset)
		{
			List<KeyValuePair<string, StringHandle>> list = new List<KeyValuePair<string, StringHandle>>(strings);
			list.Sort(MetadataBuilder.SuffixSort.Instance);
			int num = list.Count + 1;
			ImmutableArray<int>.Builder builder = ImmutableArray.CreateBuilder<int>(num);
			builder.Count = num;
			builder[0] = 0;
			heapBuilder.WriteByte(0);
			string text = string.Empty;
			foreach (KeyValuePair<string, StringHandle> keyValuePair in list)
			{
				int num2 = stringHeapStartOffset + heapBuilder.Count;
				if (text.EndsWith(keyValuePair.Key, 4) && !BlobUtilities.IsLowSurrogateChar((int)keyValuePair.Key.get_Chars(0)))
				{
					builder[keyValuePair.Value.GetWriterVirtualIndex()] = num2 - (BlobUtilities.GetUTF8ByteCount(keyValuePair.Key) + 1);
				}
				else
				{
					builder[keyValuePair.Value.GetWriterVirtualIndex()] = num2;
					heapBuilder.WriteUTF8(keyValuePair.Key, false);
					heapBuilder.WriteByte(0);
				}
				text = keyValuePair.Key;
			}
			return builder.MoveToImmutable();
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x000178A8 File Offset: 0x00015AA8
		internal void WriteHeapsTo(BlobBuilder builder, BlobBuilder stringHeap)
		{
			MetadataBuilder.WriteAligned(stringHeap, builder);
			MetadataBuilder.WriteAligned(this._userStringBuilder, builder);
			MetadataBuilder.WriteAligned(this._guidBuilder, builder);
			this.WriteAlignedBlobHeap(builder);
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x000178D0 File Offset: 0x00015AD0
		private void WriteAlignedBlobHeap(BlobBuilder builder)
		{
			int num = BitArithmetic.Align(this._blobHeapSize, 4) - this._blobHeapSize;
			BlobWriter blobWriter = new BlobWriter(builder.ReserveBytes(this._blobHeapSize + num));
			int blobHeapStartOffset = this._blobHeapStartOffset;
			foreach (KeyValuePair<ImmutableArray<byte>, BlobHandle> keyValuePair in this._blobs)
			{
				int heapOffset = keyValuePair.Value.GetHeapOffset();
				ImmutableArray<byte> key = keyValuePair.Key;
				blobWriter.Offset = ((heapOffset == 0) ? 0 : (heapOffset - blobHeapStartOffset));
				blobWriter.WriteCompressedInteger(key.Length);
				blobWriter.WriteBytes(key);
			}
			blobWriter.Offset = this._blobHeapSize;
			blobWriter.WriteBytes(0, num);
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x000179A8 File Offset: 0x00015BA8
		private static void WriteAligned(BlobBuilder source, BlobBuilder target)
		{
			int count = source.Count;
			target.LinkSuffix(source);
			target.WriteBytes(0, BitArithmetic.Align(count, 4) - count);
		}

		// Token: 0x040004F1 RID: 1265
		private const byte MetadataFormatMajorVersion = 2;

		// Token: 0x040004F2 RID: 1266
		private const byte MetadataFormatMinorVersion = 0;

		// Token: 0x040004F3 RID: 1267
		private MetadataBuilder.ModuleRow? _moduleRow;

		// Token: 0x040004F4 RID: 1268
		private MetadataBuilder.AssemblyRow? _assemblyRow;

		// Token: 0x040004F5 RID: 1269
		private readonly List<MetadataBuilder.ClassLayoutRow> _classLayoutTable = new List<MetadataBuilder.ClassLayoutRow>();

		// Token: 0x040004F6 RID: 1270
		private readonly List<MetadataBuilder.ConstantRow> _constantTable = new List<MetadataBuilder.ConstantRow>();

		// Token: 0x040004F7 RID: 1271
		private int _constantTableLastParent;

		// Token: 0x040004F8 RID: 1272
		private bool _constantTableNeedsSorting;

		// Token: 0x040004F9 RID: 1273
		private readonly List<MetadataBuilder.CustomAttributeRow> _customAttributeTable = new List<MetadataBuilder.CustomAttributeRow>();

		// Token: 0x040004FA RID: 1274
		private int _customAttributeTableLastParent;

		// Token: 0x040004FB RID: 1275
		private bool _customAttributeTableNeedsSorting;

		// Token: 0x040004FC RID: 1276
		private readonly List<MetadataBuilder.DeclSecurityRow> _declSecurityTable = new List<MetadataBuilder.DeclSecurityRow>();

		// Token: 0x040004FD RID: 1277
		private int _declSecurityTableLastParent;

		// Token: 0x040004FE RID: 1278
		private bool _declSecurityTableNeedsSorting;

		// Token: 0x040004FF RID: 1279
		private readonly List<MetadataBuilder.EncLogRow> _encLogTable = new List<MetadataBuilder.EncLogRow>();

		// Token: 0x04000500 RID: 1280
		private readonly List<MetadataBuilder.EncMapRow> _encMapTable = new List<MetadataBuilder.EncMapRow>();

		// Token: 0x04000501 RID: 1281
		private readonly List<MetadataBuilder.EventRow> _eventTable = new List<MetadataBuilder.EventRow>();

		// Token: 0x04000502 RID: 1282
		private readonly List<MetadataBuilder.EventMapRow> _eventMapTable = new List<MetadataBuilder.EventMapRow>();

		// Token: 0x04000503 RID: 1283
		private readonly List<MetadataBuilder.ExportedTypeRow> _exportedTypeTable = new List<MetadataBuilder.ExportedTypeRow>();

		// Token: 0x04000504 RID: 1284
		private readonly List<MetadataBuilder.FieldLayoutRow> _fieldLayoutTable = new List<MetadataBuilder.FieldLayoutRow>();

		// Token: 0x04000505 RID: 1285
		private readonly List<MetadataBuilder.FieldMarshalRow> _fieldMarshalTable = new List<MetadataBuilder.FieldMarshalRow>();

		// Token: 0x04000506 RID: 1286
		private int _fieldMarshalTableLastParent;

		// Token: 0x04000507 RID: 1287
		private bool _fieldMarshalTableNeedsSorting;

		// Token: 0x04000508 RID: 1288
		private readonly List<MetadataBuilder.FieldRvaRow> _fieldRvaTable = new List<MetadataBuilder.FieldRvaRow>();

		// Token: 0x04000509 RID: 1289
		private readonly List<MetadataBuilder.FieldDefRow> _fieldTable = new List<MetadataBuilder.FieldDefRow>();

		// Token: 0x0400050A RID: 1290
		private readonly List<MetadataBuilder.FileTableRow> _fileTable = new List<MetadataBuilder.FileTableRow>();

		// Token: 0x0400050B RID: 1291
		private readonly List<MetadataBuilder.GenericParamConstraintRow> _genericParamConstraintTable = new List<MetadataBuilder.GenericParamConstraintRow>();

		// Token: 0x0400050C RID: 1292
		private readonly List<MetadataBuilder.GenericParamRow> _genericParamTable = new List<MetadataBuilder.GenericParamRow>();

		// Token: 0x0400050D RID: 1293
		private readonly List<MetadataBuilder.ImplMapRow> _implMapTable = new List<MetadataBuilder.ImplMapRow>();

		// Token: 0x0400050E RID: 1294
		private readonly List<MetadataBuilder.InterfaceImplRow> _interfaceImplTable = new List<MetadataBuilder.InterfaceImplRow>();

		// Token: 0x0400050F RID: 1295
		private readonly List<MetadataBuilder.ManifestResourceRow> _manifestResourceTable = new List<MetadataBuilder.ManifestResourceRow>();

		// Token: 0x04000510 RID: 1296
		private readonly List<MetadataBuilder.MemberRefRow> _memberRefTable = new List<MetadataBuilder.MemberRefRow>();

		// Token: 0x04000511 RID: 1297
		private readonly List<MetadataBuilder.MethodImplRow> _methodImplTable = new List<MetadataBuilder.MethodImplRow>();

		// Token: 0x04000512 RID: 1298
		private readonly List<MetadataBuilder.MethodSemanticsRow> _methodSemanticsTable = new List<MetadataBuilder.MethodSemanticsRow>();

		// Token: 0x04000513 RID: 1299
		private int _methodSemanticsTableLastAssociation;

		// Token: 0x04000514 RID: 1300
		private bool _methodSemanticsTableNeedsSorting;

		// Token: 0x04000515 RID: 1301
		private readonly List<MetadataBuilder.MethodSpecRow> _methodSpecTable = new List<MetadataBuilder.MethodSpecRow>();

		// Token: 0x04000516 RID: 1302
		private readonly List<MetadataBuilder.MethodRow> _methodDefTable = new List<MetadataBuilder.MethodRow>();

		// Token: 0x04000517 RID: 1303
		private readonly List<MetadataBuilder.ModuleRefRow> _moduleRefTable = new List<MetadataBuilder.ModuleRefRow>();

		// Token: 0x04000518 RID: 1304
		private readonly List<MetadataBuilder.NestedClassRow> _nestedClassTable = new List<MetadataBuilder.NestedClassRow>();

		// Token: 0x04000519 RID: 1305
		private readonly List<MetadataBuilder.ParamRow> _paramTable = new List<MetadataBuilder.ParamRow>();

		// Token: 0x0400051A RID: 1306
		private readonly List<MetadataBuilder.PropertyMapRow> _propertyMapTable = new List<MetadataBuilder.PropertyMapRow>();

		// Token: 0x0400051B RID: 1307
		private readonly List<MetadataBuilder.PropertyRow> _propertyTable = new List<MetadataBuilder.PropertyRow>();

		// Token: 0x0400051C RID: 1308
		private readonly List<MetadataBuilder.TypeDefRow> _typeDefTable = new List<MetadataBuilder.TypeDefRow>();

		// Token: 0x0400051D RID: 1309
		private readonly List<MetadataBuilder.TypeRefRow> _typeRefTable = new List<MetadataBuilder.TypeRefRow>();

		// Token: 0x0400051E RID: 1310
		private readonly List<MetadataBuilder.TypeSpecRow> _typeSpecTable = new List<MetadataBuilder.TypeSpecRow>();

		// Token: 0x0400051F RID: 1311
		private readonly List<MetadataBuilder.AssemblyRefTableRow> _assemblyRefTable = new List<MetadataBuilder.AssemblyRefTableRow>();

		// Token: 0x04000520 RID: 1312
		private readonly List<MetadataBuilder.StandaloneSigRow> _standAloneSigTable = new List<MetadataBuilder.StandaloneSigRow>();

		// Token: 0x04000521 RID: 1313
		private readonly List<MetadataBuilder.DocumentRow> _documentTable = new List<MetadataBuilder.DocumentRow>();

		// Token: 0x04000522 RID: 1314
		private readonly List<MetadataBuilder.MethodDebugInformationRow> _methodDebugInformationTable = new List<MetadataBuilder.MethodDebugInformationRow>();

		// Token: 0x04000523 RID: 1315
		private readonly List<MetadataBuilder.LocalScopeRow> _localScopeTable = new List<MetadataBuilder.LocalScopeRow>();

		// Token: 0x04000524 RID: 1316
		private readonly List<MetadataBuilder.LocalVariableRow> _localVariableTable = new List<MetadataBuilder.LocalVariableRow>();

		// Token: 0x04000525 RID: 1317
		private readonly List<MetadataBuilder.LocalConstantRow> _localConstantTable = new List<MetadataBuilder.LocalConstantRow>();

		// Token: 0x04000526 RID: 1318
		private readonly List<MetadataBuilder.ImportScopeRow> _importScopeTable = new List<MetadataBuilder.ImportScopeRow>();

		// Token: 0x04000527 RID: 1319
		private readonly List<MetadataBuilder.StateMachineMethodRow> _stateMachineMethodTable = new List<MetadataBuilder.StateMachineMethodRow>();

		// Token: 0x04000528 RID: 1320
		private readonly List<MetadataBuilder.CustomDebugInformationRow> _customDebugInformationTable = new List<MetadataBuilder.CustomDebugInformationRow>();

		// Token: 0x04000529 RID: 1321
		private const int UserStringHeapSizeLimit = 16777216;

		// Token: 0x0400052A RID: 1322
		private readonly Dictionary<string, UserStringHandle> _userStrings = new Dictionary<string, UserStringHandle>(256);

		// Token: 0x0400052B RID: 1323
		private readonly MetadataBuilder.HeapBlobBuilder _userStringBuilder = new MetadataBuilder.HeapBlobBuilder(4096);

		// Token: 0x0400052C RID: 1324
		private readonly int _userStringHeapStartOffset;

		// Token: 0x0400052D RID: 1325
		private readonly Dictionary<string, StringHandle> _strings = new Dictionary<string, StringHandle>(256);

		// Token: 0x0400052E RID: 1326
		private readonly int _stringHeapStartOffset;

		// Token: 0x0400052F RID: 1327
		private int _stringHeapCapacity = 4096;

		// Token: 0x04000530 RID: 1328
		private readonly Dictionary<ImmutableArray<byte>, BlobHandle> _blobs = new Dictionary<ImmutableArray<byte>, BlobHandle>(1024, ByteSequenceComparer.Instance);

		// Token: 0x04000531 RID: 1329
		private readonly int _blobHeapStartOffset;

		// Token: 0x04000532 RID: 1330
		private int _blobHeapSize;

		// Token: 0x04000533 RID: 1331
		private readonly Dictionary<Guid, GuidHandle> _guids = new Dictionary<Guid, GuidHandle>();

		// Token: 0x04000534 RID: 1332
		private readonly MetadataBuilder.HeapBlobBuilder _guidBuilder = new MetadataBuilder.HeapBlobBuilder(16);

		// Token: 0x020001B9 RID: 441
		private struct AssemblyRefTableRow
		{
			// Token: 0x04000A9A RID: 2714
			public Version Version;

			// Token: 0x04000A9B RID: 2715
			public BlobHandle PublicKeyToken;

			// Token: 0x04000A9C RID: 2716
			public StringHandle Name;

			// Token: 0x04000A9D RID: 2717
			public StringHandle Culture;

			// Token: 0x04000A9E RID: 2718
			public uint Flags;

			// Token: 0x04000A9F RID: 2719
			public BlobHandle HashValue;
		}

		// Token: 0x020001BA RID: 442
		private struct ModuleRow
		{
			// Token: 0x04000AA0 RID: 2720
			public ushort Generation;

			// Token: 0x04000AA1 RID: 2721
			public StringHandle Name;

			// Token: 0x04000AA2 RID: 2722
			public GuidHandle ModuleVersionId;

			// Token: 0x04000AA3 RID: 2723
			public GuidHandle EncId;

			// Token: 0x04000AA4 RID: 2724
			public GuidHandle EncBaseId;
		}

		// Token: 0x020001BB RID: 443
		private struct AssemblyRow
		{
			// Token: 0x04000AA5 RID: 2725
			public uint HashAlgorithm;

			// Token: 0x04000AA6 RID: 2726
			public Version Version;

			// Token: 0x04000AA7 RID: 2727
			public ushort Flags;

			// Token: 0x04000AA8 RID: 2728
			public BlobHandle AssemblyKey;

			// Token: 0x04000AA9 RID: 2729
			public StringHandle AssemblyName;

			// Token: 0x04000AAA RID: 2730
			public StringHandle AssemblyCulture;
		}

		// Token: 0x020001BC RID: 444
		private struct ClassLayoutRow
		{
			// Token: 0x04000AAB RID: 2731
			public ushort PackingSize;

			// Token: 0x04000AAC RID: 2732
			public uint ClassSize;

			// Token: 0x04000AAD RID: 2733
			public int Parent;
		}

		// Token: 0x020001BD RID: 445
		private struct ConstantRow
		{
			// Token: 0x04000AAE RID: 2734
			public byte Type;

			// Token: 0x04000AAF RID: 2735
			public int Parent;

			// Token: 0x04000AB0 RID: 2736
			public BlobHandle Value;
		}

		// Token: 0x020001BE RID: 446
		private struct CustomAttributeRow
		{
			// Token: 0x04000AB1 RID: 2737
			public int Parent;

			// Token: 0x04000AB2 RID: 2738
			public int Type;

			// Token: 0x04000AB3 RID: 2739
			public BlobHandle Value;
		}

		// Token: 0x020001BF RID: 447
		private struct DeclSecurityRow
		{
			// Token: 0x04000AB4 RID: 2740
			public ushort Action;

			// Token: 0x04000AB5 RID: 2741
			public int Parent;

			// Token: 0x04000AB6 RID: 2742
			public BlobHandle PermissionSet;
		}

		// Token: 0x020001C0 RID: 448
		private struct EncLogRow
		{
			// Token: 0x04000AB7 RID: 2743
			public int Token;

			// Token: 0x04000AB8 RID: 2744
			public byte FuncCode;
		}

		// Token: 0x020001C1 RID: 449
		private struct EncMapRow
		{
			// Token: 0x04000AB9 RID: 2745
			public int Token;
		}

		// Token: 0x020001C2 RID: 450
		private struct EventRow
		{
			// Token: 0x04000ABA RID: 2746
			public ushort EventFlags;

			// Token: 0x04000ABB RID: 2747
			public StringHandle Name;

			// Token: 0x04000ABC RID: 2748
			public int EventType;
		}

		// Token: 0x020001C3 RID: 451
		private struct EventMapRow
		{
			// Token: 0x04000ABD RID: 2749
			public int Parent;

			// Token: 0x04000ABE RID: 2750
			public int EventList;
		}

		// Token: 0x020001C4 RID: 452
		private struct ExportedTypeRow
		{
			// Token: 0x04000ABF RID: 2751
			public uint Flags;

			// Token: 0x04000AC0 RID: 2752
			public int TypeDefId;

			// Token: 0x04000AC1 RID: 2753
			public StringHandle TypeName;

			// Token: 0x04000AC2 RID: 2754
			public StringHandle TypeNamespace;

			// Token: 0x04000AC3 RID: 2755
			public int Implementation;
		}

		// Token: 0x020001C5 RID: 453
		private struct FieldLayoutRow
		{
			// Token: 0x04000AC4 RID: 2756
			public int Offset;

			// Token: 0x04000AC5 RID: 2757
			public int Field;
		}

		// Token: 0x020001C6 RID: 454
		private struct FieldMarshalRow
		{
			// Token: 0x04000AC6 RID: 2758
			public int Parent;

			// Token: 0x04000AC7 RID: 2759
			public BlobHandle NativeType;
		}

		// Token: 0x020001C7 RID: 455
		private struct FieldRvaRow
		{
			// Token: 0x04000AC8 RID: 2760
			public int Offset;

			// Token: 0x04000AC9 RID: 2761
			public int Field;
		}

		// Token: 0x020001C8 RID: 456
		private struct FieldDefRow
		{
			// Token: 0x04000ACA RID: 2762
			public ushort Flags;

			// Token: 0x04000ACB RID: 2763
			public StringHandle Name;

			// Token: 0x04000ACC RID: 2764
			public BlobHandle Signature;
		}

		// Token: 0x020001C9 RID: 457
		private struct FileTableRow
		{
			// Token: 0x04000ACD RID: 2765
			public uint Flags;

			// Token: 0x04000ACE RID: 2766
			public StringHandle FileName;

			// Token: 0x04000ACF RID: 2767
			public BlobHandle HashValue;
		}

		// Token: 0x020001CA RID: 458
		private struct GenericParamConstraintRow
		{
			// Token: 0x04000AD0 RID: 2768
			public int Owner;

			// Token: 0x04000AD1 RID: 2769
			public int Constraint;
		}

		// Token: 0x020001CB RID: 459
		private struct GenericParamRow
		{
			// Token: 0x04000AD2 RID: 2770
			public ushort Number;

			// Token: 0x04000AD3 RID: 2771
			public ushort Flags;

			// Token: 0x04000AD4 RID: 2772
			public int Owner;

			// Token: 0x04000AD5 RID: 2773
			public StringHandle Name;
		}

		// Token: 0x020001CC RID: 460
		private struct ImplMapRow
		{
			// Token: 0x04000AD6 RID: 2774
			public ushort MappingFlags;

			// Token: 0x04000AD7 RID: 2775
			public int MemberForwarded;

			// Token: 0x04000AD8 RID: 2776
			public StringHandle ImportName;

			// Token: 0x04000AD9 RID: 2777
			public int ImportScope;
		}

		// Token: 0x020001CD RID: 461
		private struct InterfaceImplRow
		{
			// Token: 0x04000ADA RID: 2778
			public int Class;

			// Token: 0x04000ADB RID: 2779
			public int Interface;
		}

		// Token: 0x020001CE RID: 462
		private struct ManifestResourceRow
		{
			// Token: 0x04000ADC RID: 2780
			public uint Offset;

			// Token: 0x04000ADD RID: 2781
			public uint Flags;

			// Token: 0x04000ADE RID: 2782
			public StringHandle Name;

			// Token: 0x04000ADF RID: 2783
			public int Implementation;
		}

		// Token: 0x020001CF RID: 463
		private struct MemberRefRow
		{
			// Token: 0x04000AE0 RID: 2784
			public int Class;

			// Token: 0x04000AE1 RID: 2785
			public StringHandle Name;

			// Token: 0x04000AE2 RID: 2786
			public BlobHandle Signature;
		}

		// Token: 0x020001D0 RID: 464
		private struct MethodImplRow
		{
			// Token: 0x04000AE3 RID: 2787
			public int Class;

			// Token: 0x04000AE4 RID: 2788
			public int MethodBody;

			// Token: 0x04000AE5 RID: 2789
			public int MethodDecl;
		}

		// Token: 0x020001D1 RID: 465
		private struct MethodSemanticsRow
		{
			// Token: 0x04000AE6 RID: 2790
			public ushort Semantic;

			// Token: 0x04000AE7 RID: 2791
			public int Method;

			// Token: 0x04000AE8 RID: 2792
			public int Association;
		}

		// Token: 0x020001D2 RID: 466
		private struct MethodSpecRow
		{
			// Token: 0x04000AE9 RID: 2793
			public int Method;

			// Token: 0x04000AEA RID: 2794
			public BlobHandle Instantiation;
		}

		// Token: 0x020001D3 RID: 467
		private struct MethodRow
		{
			// Token: 0x04000AEB RID: 2795
			public int BodyOffset;

			// Token: 0x04000AEC RID: 2796
			public ushort ImplFlags;

			// Token: 0x04000AED RID: 2797
			public ushort Flags;

			// Token: 0x04000AEE RID: 2798
			public StringHandle Name;

			// Token: 0x04000AEF RID: 2799
			public BlobHandle Signature;

			// Token: 0x04000AF0 RID: 2800
			public int ParamList;
		}

		// Token: 0x020001D4 RID: 468
		private struct ModuleRefRow
		{
			// Token: 0x04000AF1 RID: 2801
			public StringHandle Name;
		}

		// Token: 0x020001D5 RID: 469
		private struct NestedClassRow
		{
			// Token: 0x04000AF2 RID: 2802
			public int NestedClass;

			// Token: 0x04000AF3 RID: 2803
			public int EnclosingClass;
		}

		// Token: 0x020001D6 RID: 470
		private struct ParamRow
		{
			// Token: 0x04000AF4 RID: 2804
			public ushort Flags;

			// Token: 0x04000AF5 RID: 2805
			public ushort Sequence;

			// Token: 0x04000AF6 RID: 2806
			public StringHandle Name;
		}

		// Token: 0x020001D7 RID: 471
		private struct PropertyMapRow
		{
			// Token: 0x04000AF7 RID: 2807
			public int Parent;

			// Token: 0x04000AF8 RID: 2808
			public int PropertyList;
		}

		// Token: 0x020001D8 RID: 472
		private struct PropertyRow
		{
			// Token: 0x04000AF9 RID: 2809
			public ushort PropFlags;

			// Token: 0x04000AFA RID: 2810
			public StringHandle Name;

			// Token: 0x04000AFB RID: 2811
			public BlobHandle Type;
		}

		// Token: 0x020001D9 RID: 473
		private struct TypeDefRow
		{
			// Token: 0x04000AFC RID: 2812
			public uint Flags;

			// Token: 0x04000AFD RID: 2813
			public StringHandle Name;

			// Token: 0x04000AFE RID: 2814
			public StringHandle Namespace;

			// Token: 0x04000AFF RID: 2815
			public int Extends;

			// Token: 0x04000B00 RID: 2816
			public int FieldList;

			// Token: 0x04000B01 RID: 2817
			public int MethodList;
		}

		// Token: 0x020001DA RID: 474
		private struct TypeRefRow
		{
			// Token: 0x04000B02 RID: 2818
			public int ResolutionScope;

			// Token: 0x04000B03 RID: 2819
			public StringHandle Name;

			// Token: 0x04000B04 RID: 2820
			public StringHandle Namespace;
		}

		// Token: 0x020001DB RID: 475
		private struct TypeSpecRow
		{
			// Token: 0x04000B05 RID: 2821
			public BlobHandle Signature;
		}

		// Token: 0x020001DC RID: 476
		private struct StandaloneSigRow
		{
			// Token: 0x04000B06 RID: 2822
			public BlobHandle Signature;
		}

		// Token: 0x020001DD RID: 477
		private struct DocumentRow
		{
			// Token: 0x04000B07 RID: 2823
			public BlobHandle Name;

			// Token: 0x04000B08 RID: 2824
			public GuidHandle HashAlgorithm;

			// Token: 0x04000B09 RID: 2825
			public BlobHandle Hash;

			// Token: 0x04000B0A RID: 2826
			public GuidHandle Language;
		}

		// Token: 0x020001DE RID: 478
		private struct MethodDebugInformationRow
		{
			// Token: 0x04000B0B RID: 2827
			public int Document;

			// Token: 0x04000B0C RID: 2828
			public BlobHandle SequencePoints;
		}

		// Token: 0x020001DF RID: 479
		private struct LocalScopeRow
		{
			// Token: 0x04000B0D RID: 2829
			public int Method;

			// Token: 0x04000B0E RID: 2830
			public int ImportScope;

			// Token: 0x04000B0F RID: 2831
			public int VariableList;

			// Token: 0x04000B10 RID: 2832
			public int ConstantList;

			// Token: 0x04000B11 RID: 2833
			public int StartOffset;

			// Token: 0x04000B12 RID: 2834
			public int Length;
		}

		// Token: 0x020001E0 RID: 480
		private struct LocalVariableRow
		{
			// Token: 0x04000B13 RID: 2835
			public ushort Attributes;

			// Token: 0x04000B14 RID: 2836
			public ushort Index;

			// Token: 0x04000B15 RID: 2837
			public StringHandle Name;
		}

		// Token: 0x020001E1 RID: 481
		private struct LocalConstantRow
		{
			// Token: 0x04000B16 RID: 2838
			public StringHandle Name;

			// Token: 0x04000B17 RID: 2839
			public BlobHandle Signature;
		}

		// Token: 0x020001E2 RID: 482
		private struct ImportScopeRow
		{
			// Token: 0x04000B18 RID: 2840
			public int Parent;

			// Token: 0x04000B19 RID: 2841
			public BlobHandle Imports;
		}

		// Token: 0x020001E3 RID: 483
		private struct StateMachineMethodRow
		{
			// Token: 0x04000B1A RID: 2842
			public int MoveNextMethod;

			// Token: 0x04000B1B RID: 2843
			public int KickoffMethod;
		}

		// Token: 0x020001E4 RID: 484
		private struct CustomDebugInformationRow
		{
			// Token: 0x04000B1C RID: 2844
			public int Parent;

			// Token: 0x04000B1D RID: 2845
			public GuidHandle Kind;

			// Token: 0x04000B1E RID: 2846
			public BlobHandle Value;
		}

		// Token: 0x020001E5 RID: 485
		private sealed class HeapBlobBuilder : BlobBuilder
		{
			// Token: 0x06000D06 RID: 3334 RVA: 0x000263D2 File Offset: 0x000245D2
			public HeapBlobBuilder(int capacity)
				: base(capacity)
			{
			}

			// Token: 0x06000D07 RID: 3335 RVA: 0x000263DB File Offset: 0x000245DB
			protected override BlobBuilder AllocateChunk(int minimalSize)
			{
				return new MetadataBuilder.HeapBlobBuilder(Math.Max(Math.Max(minimalSize, base.ChunkCapacity), this._capacityExpansion));
			}

			// Token: 0x06000D08 RID: 3336 RVA: 0x000263F9 File Offset: 0x000245F9
			internal void SetCapacity(int capacity)
			{
				this._capacityExpansion = Math.Max(0, capacity - base.Count - base.FreeBytes);
			}

			// Token: 0x04000B1F RID: 2847
			private int _capacityExpansion;
		}

		// Token: 0x020001E6 RID: 486
		private sealed class SuffixSort : IComparer<KeyValuePair<string, StringHandle>>
		{
			// Token: 0x06000D09 RID: 3337 RVA: 0x00026418 File Offset: 0x00024618
			public int Compare(KeyValuePair<string, StringHandle> xPair, KeyValuePair<string, StringHandle> yPair)
			{
				string key = xPair.Key;
				string key2 = yPair.Key;
				int num = key.Length - 1;
				int num2 = key2.Length - 1;
				while ((num >= 0) & (num2 >= 0))
				{
					if (key.get_Chars(num) < key2.get_Chars(num2))
					{
						return -1;
					}
					if (key.get_Chars(num) > key2.get_Chars(num2))
					{
						return 1;
					}
					num--;
					num2--;
				}
				return key2.Length.CompareTo(key.Length);
			}

			// Token: 0x04000B20 RID: 2848
			internal static MetadataBuilder.SuffixSort Instance = new MetadataBuilder.SuffixSort();
		}
	}
}
