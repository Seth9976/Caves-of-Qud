using System;
using System.Collections.Generic;
using System.Reflection.Internal;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	/// <summary>Provides extension methods for working with certain raw elements of the ECMA-335 metadata tables and heaps.</summary>
	// Token: 0x0200010E RID: 270
	[NullableContext(1)]
	[Nullable(0)]
	public static class MetadataReaderExtensions
	{
		/// <summary>Returns the number of rows in the specified table.</summary>
		/// <param name="reader" />
		/// <param name="tableIndex" />
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reader" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="tableIndex" /> is not a valid table index.</exception>
		// Token: 0x060009E8 RID: 2536 RVA: 0x0001C5B0 File Offset: 0x0001A7B0
		public static int GetTableRowCount(this MetadataReader reader, TableIndex tableIndex)
		{
			if (reader == null)
			{
				Throw.ArgumentNull("reader");
			}
			if ((int)tableIndex >= MetadataTokens.TableCount)
			{
				Throw.TableIndexOutOfRange();
			}
			return reader.TableRowCounts[(int)tableIndex];
		}

		/// <summary>Returns the size of a row in the specified table.</summary>
		/// <param name="reader" />
		/// <param name="tableIndex" />
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reader" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="tableIndex" /> is not a valid table index.</exception>
		// Token: 0x060009E9 RID: 2537 RVA: 0x0001C5D4 File Offset: 0x0001A7D4
		public static int GetTableRowSize(this MetadataReader reader, TableIndex tableIndex)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			switch (tableIndex)
			{
			case TableIndex.Module:
				return reader.ModuleTable.RowSize;
			case TableIndex.TypeRef:
				return reader.TypeRefTable.RowSize;
			case TableIndex.TypeDef:
				return reader.TypeDefTable.RowSize;
			case TableIndex.FieldPtr:
				return reader.FieldPtrTable.RowSize;
			case TableIndex.Field:
				return reader.FieldTable.RowSize;
			case TableIndex.MethodPtr:
				return reader.MethodPtrTable.RowSize;
			case TableIndex.MethodDef:
				return reader.MethodDefTable.RowSize;
			case TableIndex.ParamPtr:
				return reader.ParamPtrTable.RowSize;
			case TableIndex.Param:
				return reader.ParamTable.RowSize;
			case TableIndex.InterfaceImpl:
				return reader.InterfaceImplTable.RowSize;
			case TableIndex.MemberRef:
				return reader.MemberRefTable.RowSize;
			case TableIndex.Constant:
				return reader.ConstantTable.RowSize;
			case TableIndex.CustomAttribute:
				return reader.CustomAttributeTable.RowSize;
			case TableIndex.FieldMarshal:
				return reader.FieldMarshalTable.RowSize;
			case TableIndex.DeclSecurity:
				return reader.DeclSecurityTable.RowSize;
			case TableIndex.ClassLayout:
				return reader.ClassLayoutTable.RowSize;
			case TableIndex.FieldLayout:
				return reader.FieldLayoutTable.RowSize;
			case TableIndex.StandAloneSig:
				return reader.StandAloneSigTable.RowSize;
			case TableIndex.EventMap:
				return reader.EventMapTable.RowSize;
			case TableIndex.EventPtr:
				return reader.EventPtrTable.RowSize;
			case TableIndex.Event:
				return reader.EventTable.RowSize;
			case TableIndex.PropertyMap:
				return reader.PropertyMapTable.RowSize;
			case TableIndex.PropertyPtr:
				return reader.PropertyPtrTable.RowSize;
			case TableIndex.Property:
				return reader.PropertyTable.RowSize;
			case TableIndex.MethodSemantics:
				return reader.MethodSemanticsTable.RowSize;
			case TableIndex.MethodImpl:
				return reader.MethodImplTable.RowSize;
			case TableIndex.ModuleRef:
				return reader.ModuleRefTable.RowSize;
			case TableIndex.TypeSpec:
				return reader.TypeSpecTable.RowSize;
			case TableIndex.ImplMap:
				return reader.ImplMapTable.RowSize;
			case TableIndex.FieldRva:
				return reader.FieldRvaTable.RowSize;
			case TableIndex.EncLog:
				return reader.EncLogTable.RowSize;
			case TableIndex.EncMap:
				return reader.EncMapTable.RowSize;
			case TableIndex.Assembly:
				return reader.AssemblyTable.RowSize;
			case TableIndex.AssemblyProcessor:
				return reader.AssemblyProcessorTable.RowSize;
			case TableIndex.AssemblyOS:
				return reader.AssemblyOSTable.RowSize;
			case TableIndex.AssemblyRef:
				return reader.AssemblyRefTable.RowSize;
			case TableIndex.AssemblyRefProcessor:
				return reader.AssemblyRefProcessorTable.RowSize;
			case TableIndex.AssemblyRefOS:
				return reader.AssemblyRefOSTable.RowSize;
			case TableIndex.File:
				return reader.FileTable.RowSize;
			case TableIndex.ExportedType:
				return reader.ExportedTypeTable.RowSize;
			case TableIndex.ManifestResource:
				return reader.ManifestResourceTable.RowSize;
			case TableIndex.NestedClass:
				return reader.NestedClassTable.RowSize;
			case TableIndex.GenericParam:
				return reader.GenericParamTable.RowSize;
			case TableIndex.MethodSpec:
				return reader.MethodSpecTable.RowSize;
			case TableIndex.GenericParamConstraint:
				return reader.GenericParamConstraintTable.RowSize;
			case TableIndex.Document:
				return reader.DocumentTable.RowSize;
			case TableIndex.MethodDebugInformation:
				return reader.MethodDebugInformationTable.RowSize;
			case TableIndex.LocalScope:
				return reader.LocalScopeTable.RowSize;
			case TableIndex.LocalVariable:
				return reader.LocalVariableTable.RowSize;
			case TableIndex.LocalConstant:
				return reader.LocalConstantTable.RowSize;
			case TableIndex.ImportScope:
				return reader.ImportScopeTable.RowSize;
			case TableIndex.StateMachineMethod:
				return reader.StateMachineMethodTable.RowSize;
			case TableIndex.CustomDebugInformation:
				return reader.CustomDebugInformationTable.RowSize;
			}
			throw new ArgumentOutOfRangeException("tableIndex");
		}

		/// <summary>Returns the offset from the start of metadata to the specified table.</summary>
		/// <param name="reader" />
		/// <param name="tableIndex" />
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reader" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="tableIndex" /> is not a valid table index.</exception>
		// Token: 0x060009EA RID: 2538 RVA: 0x0001CA50 File Offset: 0x0001AC50
		public static int GetTableMetadataOffset(this MetadataReader reader, TableIndex tableIndex)
		{
			if (reader == null)
			{
				Throw.ArgumentNull("reader");
			}
			return (int)((long)(reader.GetTableMetadataBlock(tableIndex).Pointer - reader.Block.Pointer));
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0001CA7C File Offset: 0x0001AC7C
		private static MemoryBlock GetTableMetadataBlock(this MetadataReader reader, TableIndex tableIndex)
		{
			switch (tableIndex)
			{
			case TableIndex.Module:
				return reader.ModuleTable.Block;
			case TableIndex.TypeRef:
				return reader.TypeRefTable.Block;
			case TableIndex.TypeDef:
				return reader.TypeDefTable.Block;
			case TableIndex.FieldPtr:
				return reader.FieldPtrTable.Block;
			case TableIndex.Field:
				return reader.FieldTable.Block;
			case TableIndex.MethodPtr:
				return reader.MethodPtrTable.Block;
			case TableIndex.MethodDef:
				return reader.MethodDefTable.Block;
			case TableIndex.ParamPtr:
				return reader.ParamPtrTable.Block;
			case TableIndex.Param:
				return reader.ParamTable.Block;
			case TableIndex.InterfaceImpl:
				return reader.InterfaceImplTable.Block;
			case TableIndex.MemberRef:
				return reader.MemberRefTable.Block;
			case TableIndex.Constant:
				return reader.ConstantTable.Block;
			case TableIndex.CustomAttribute:
				return reader.CustomAttributeTable.Block;
			case TableIndex.FieldMarshal:
				return reader.FieldMarshalTable.Block;
			case TableIndex.DeclSecurity:
				return reader.DeclSecurityTable.Block;
			case TableIndex.ClassLayout:
				return reader.ClassLayoutTable.Block;
			case TableIndex.FieldLayout:
				return reader.FieldLayoutTable.Block;
			case TableIndex.StandAloneSig:
				return reader.StandAloneSigTable.Block;
			case TableIndex.EventMap:
				return reader.EventMapTable.Block;
			case TableIndex.EventPtr:
				return reader.EventPtrTable.Block;
			case TableIndex.Event:
				return reader.EventTable.Block;
			case TableIndex.PropertyMap:
				return reader.PropertyMapTable.Block;
			case TableIndex.PropertyPtr:
				return reader.PropertyPtrTable.Block;
			case TableIndex.Property:
				return reader.PropertyTable.Block;
			case TableIndex.MethodSemantics:
				return reader.MethodSemanticsTable.Block;
			case TableIndex.MethodImpl:
				return reader.MethodImplTable.Block;
			case TableIndex.ModuleRef:
				return reader.ModuleRefTable.Block;
			case TableIndex.TypeSpec:
				return reader.TypeSpecTable.Block;
			case TableIndex.ImplMap:
				return reader.ImplMapTable.Block;
			case TableIndex.FieldRva:
				return reader.FieldRvaTable.Block;
			case TableIndex.EncLog:
				return reader.EncLogTable.Block;
			case TableIndex.EncMap:
				return reader.EncMapTable.Block;
			case TableIndex.Assembly:
				return reader.AssemblyTable.Block;
			case TableIndex.AssemblyProcessor:
				return reader.AssemblyProcessorTable.Block;
			case TableIndex.AssemblyOS:
				return reader.AssemblyOSTable.Block;
			case TableIndex.AssemblyRef:
				return reader.AssemblyRefTable.Block;
			case TableIndex.AssemblyRefProcessor:
				return reader.AssemblyRefProcessorTable.Block;
			case TableIndex.AssemblyRefOS:
				return reader.AssemblyRefOSTable.Block;
			case TableIndex.File:
				return reader.FileTable.Block;
			case TableIndex.ExportedType:
				return reader.ExportedTypeTable.Block;
			case TableIndex.ManifestResource:
				return reader.ManifestResourceTable.Block;
			case TableIndex.NestedClass:
				return reader.NestedClassTable.Block;
			case TableIndex.GenericParam:
				return reader.GenericParamTable.Block;
			case TableIndex.MethodSpec:
				return reader.MethodSpecTable.Block;
			case TableIndex.GenericParamConstraint:
				return reader.GenericParamConstraintTable.Block;
			case TableIndex.Document:
				return reader.DocumentTable.Block;
			case TableIndex.MethodDebugInformation:
				return reader.MethodDebugInformationTable.Block;
			case TableIndex.LocalScope:
				return reader.LocalScopeTable.Block;
			case TableIndex.LocalVariable:
				return reader.LocalVariableTable.Block;
			case TableIndex.LocalConstant:
				return reader.LocalConstantTable.Block;
			case TableIndex.ImportScope:
				return reader.ImportScopeTable.Block;
			case TableIndex.StateMachineMethod:
				return reader.StateMachineMethodTable.Block;
			case TableIndex.CustomDebugInformation:
				return reader.CustomDebugInformationTable.Block;
			}
			throw new ArgumentOutOfRangeException("tableIndex");
		}

		/// <summary>Returns the size of the specified heap.</summary>
		/// <param name="reader" />
		/// <param name="heapIndex" />
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reader" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="heapIndex" /> is not a valid heap index.</exception>
		// Token: 0x060009EC RID: 2540 RVA: 0x0001CEEA File Offset: 0x0001B0EA
		public static int GetHeapSize(this MetadataReader reader, HeapIndex heapIndex)
		{
			if (reader == null)
			{
				Throw.ArgumentNull("reader");
			}
			return reader.GetMetadataBlock(heapIndex).Length;
		}

		/// <summary>Returns the offset from the start of metadata to the specified heap.</summary>
		/// <param name="reader" />
		/// <param name="heapIndex" />
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reader" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="heapIndex" /> is not a valid heap index.</exception>
		// Token: 0x060009ED RID: 2541 RVA: 0x0001CF05 File Offset: 0x0001B105
		public static int GetHeapMetadataOffset(this MetadataReader reader, HeapIndex heapIndex)
		{
			if (reader == null)
			{
				Throw.ArgumentNull("reader");
			}
			return (int)((long)(reader.GetMetadataBlock(heapIndex).Pointer - reader.Block.Pointer));
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0001CF30 File Offset: 0x0001B130
		private static MemoryBlock GetMetadataBlock(this MetadataReader reader, HeapIndex heapIndex)
		{
			MemoryBlock memoryBlock;
			switch (heapIndex)
			{
			case HeapIndex.UserString:
				memoryBlock = reader.UserStringHeap.Block;
				break;
			case HeapIndex.String:
				memoryBlock = reader.StringHeap.Block;
				break;
			case HeapIndex.Blob:
				memoryBlock = reader.BlobHeap.Block;
				break;
			case HeapIndex.Guid:
				memoryBlock = reader.GuidHeap.Block;
				break;
			default:
				throw new ArgumentOutOfRangeException("heapIndex");
			}
			return memoryBlock;
		}

		/// <summary>Returns the a handle to the UserString that follows the given one in the UserString heap or a nil handle if it is the last one.</summary>
		/// <param name="reader" />
		/// <param name="handle" />
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reader" /> is <see langword="null" />.</exception>
		// Token: 0x060009EF RID: 2543 RVA: 0x0001CF99 File Offset: 0x0001B199
		public static UserStringHandle GetNextHandle(this MetadataReader reader, UserStringHandle handle)
		{
			if (reader == null)
			{
				Throw.ArgumentNull("reader");
			}
			return reader.UserStringHeap.GetNextHandle(handle);
		}

		/// <summary>Returns the handle to the <see cref="T:System.Reflection.Metadata.Blob" /> that follows the given one in the <see cref="T:System.Reflection.Metadata.Blob" /> heap or a nil handle if it is the last one.</summary>
		/// <param name="reader" />
		/// <param name="handle" />
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reader" /> is <see langword="null" />.</exception>
		// Token: 0x060009F0 RID: 2544 RVA: 0x0001CFB4 File Offset: 0x0001B1B4
		public static BlobHandle GetNextHandle(this MetadataReader reader, BlobHandle handle)
		{
			if (reader == null)
			{
				Throw.ArgumentNull("reader");
			}
			return reader.BlobHeap.GetNextHandle(handle);
		}

		/// <summary>Returns the a handle to the string that follows the given one in the string heap, or a nil handle if it is the last one.</summary>
		/// <param name="reader" />
		/// <param name="handle" />
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reader" /> is <see langword="null" />.</exception>
		// Token: 0x060009F1 RID: 2545 RVA: 0x0001CFCF File Offset: 0x0001B1CF
		public static StringHandle GetNextHandle(this MetadataReader reader, StringHandle handle)
		{
			if (reader == null)
			{
				Throw.ArgumentNull("reader");
			}
			return reader.StringHeap.GetNextHandle(handle);
		}

		/// <summary>Enumerates entries of EnC log.</summary>
		/// <param name="reader" />
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reader" /> is <see langword="null" />.</exception>
		// Token: 0x060009F2 RID: 2546 RVA: 0x0001CFEA File Offset: 0x0001B1EA
		public static IEnumerable<EditAndContinueLogEntry> GetEditAndContinueLogEntries(this MetadataReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			int num;
			for (int rid = 1; rid <= reader.EncLogTable.NumberOfRows; rid = num + 1)
			{
				yield return new EditAndContinueLogEntry(new EntityHandle(reader.EncLogTable.GetToken(rid)), reader.EncLogTable.GetFuncCode(rid));
				num = rid;
			}
			yield break;
		}

		/// <summary>Enumerates entries of EnC map.</summary>
		/// <param name="reader" />
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reader" /> is <see langword="null" />.</exception>
		// Token: 0x060009F3 RID: 2547 RVA: 0x0001CFFA File Offset: 0x0001B1FA
		public static IEnumerable<EntityHandle> GetEditAndContinueMapEntries(this MetadataReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			int num;
			for (int rid = 1; rid <= reader.EncMapTable.NumberOfRows; rid = num + 1)
			{
				yield return new EntityHandle(reader.EncMapTable.GetToken(rid));
				num = rid;
			}
			yield break;
		}

		/// <summary>Enumerate types that define one or more properties.</summary>
		/// <param name="reader" />
		/// <returns>The resulting sequence corresponds exactly to entries in the property map table, that is, the n-th returned <see cref="T:System.Reflection.Metadata.TypeDefinitionHandle" /> is stored in n-th row of the property map.</returns>
		// Token: 0x060009F4 RID: 2548 RVA: 0x0001D00A File Offset: 0x0001B20A
		public static IEnumerable<TypeDefinitionHandle> GetTypesWithProperties(this MetadataReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			int num;
			for (int rid = 1; rid <= reader.PropertyMapTable.NumberOfRows; rid = num + 1)
			{
				yield return reader.PropertyMapTable.GetParentType(rid);
				num = rid;
			}
			yield break;
		}

		/// <summary>Enumerate types that define one or more events.</summary>
		/// <param name="reader" />
		/// <returns>The resulting sequence corresponds exactly to entries in EventMap table, i.e. n-th returned <see cref="T:System.Reflection.Metadata.TypeDefinitionHandle" /> is stored in n-th row of EventMap.</returns>
		// Token: 0x060009F5 RID: 2549 RVA: 0x0001D01A File Offset: 0x0001B21A
		public static IEnumerable<TypeDefinitionHandle> GetTypesWithEvents(this MetadataReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			int num;
			for (int rid = 1; rid <= reader.EventMapTable.NumberOfRows; rid = num + 1)
			{
				yield return reader.EventMapTable.GetParentType(rid);
				num = rid;
			}
			yield break;
		}

		/// <summary>Given a type handle and a raw type kind found in a signature blob determines whether the target type is a value type or a reference type.</summary>
		/// <param name="reader" />
		/// <param name="typeHandle" />
		/// <param name="rawTypeKind" />
		// Token: 0x060009F6 RID: 2550 RVA: 0x0001D02C File Offset: 0x0001B22C
		public static SignatureTypeKind ResolveSignatureTypeKind(this MetadataReader reader, EntityHandle typeHandle, byte rawTypeKind)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (rawTypeKind == 0)
			{
				return SignatureTypeKind.Unknown;
			}
			if (rawTypeKind - 17 > 1)
			{
				throw new ArgumentOutOfRangeException("rawTypeKind");
			}
			HandleKind kind = typeHandle.Kind;
			if (kind == HandleKind.TypeReference)
			{
				TypeRefSignatureTreatment signatureTreatment = reader.GetTypeReference((TypeReferenceHandle)typeHandle).SignatureTreatment;
				SignatureTypeKind signatureTypeKind;
				switch (signatureTreatment)
				{
				case TypeRefSignatureTreatment.None:
					signatureTypeKind = (SignatureTypeKind)rawTypeKind;
					break;
				case TypeRefSignatureTreatment.ProjectedToClass:
					signatureTypeKind = SignatureTypeKind.Class;
					break;
				case TypeRefSignatureTreatment.ProjectedToValueType:
					signatureTypeKind = SignatureTypeKind.ValueType;
					break;
				default:
					throw ExceptionUtilities.UnexpectedValue(signatureTreatment);
				}
				return signatureTypeKind;
			}
			if (kind == HandleKind.TypeDefinition)
			{
				return (SignatureTypeKind)rawTypeKind;
			}
			if (kind != HandleKind.TypeSpecification)
			{
				throw new ArgumentOutOfRangeException("typeHandle", SR.Format(SR.UnexpectedHandleKind, typeHandle.Kind));
			}
			return SignatureTypeKind.Unknown;
		}
	}
}
