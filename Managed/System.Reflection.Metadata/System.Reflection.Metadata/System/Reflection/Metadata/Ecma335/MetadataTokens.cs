using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200010F RID: 271
	[NullableContext(1)]
	[Nullable(0)]
	public static class MetadataTokens
	{
		/// <summary>Gets the row number of a metadata table entry that corresponds to the specified <paramref name="handle" /> in the context of <paramref name="reader" />.</summary>
		/// <param name="reader" />
		/// <param name="handle" />
		/// <exception cref="T:System.ArgumentException">The <paramref name="handle" /> is not a valid metadata table handle.</exception>
		/// <returns>One based row number.</returns>
		// Token: 0x060009F7 RID: 2551 RVA: 0x0001D0E6 File Offset: 0x0001B2E6
		public static int GetRowNumber(this MetadataReader reader, EntityHandle handle)
		{
			if (handle.IsVirtual)
			{
				return MetadataTokens.MapVirtualHandleRowId(reader, handle);
			}
			return handle.RowId;
		}

		/// <summary>Gets the offset of metadata heap data that corresponds to the specified <paramref name="handle" /> in the context of <paramref name="reader" />.</summary>
		/// <param name="reader" />
		/// <param name="handle" />
		/// <exception cref="T:System.NotSupportedException">The operation is not supported for the specified <paramref name="handle" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="handle" /> is invalid.</exception>
		/// <returns>Zero based offset, or -1 if <paramref name="handle" /> isn't a metadata heap handle.</returns>
		// Token: 0x060009F8 RID: 2552 RVA: 0x0001D105 File Offset: 0x0001B305
		public static int GetHeapOffset(this MetadataReader reader, Handle handle)
		{
			if (!handle.IsHeapHandle)
			{
				Throw.HeapHandleRequired();
			}
			if (handle.IsVirtual)
			{
				return MetadataTokens.MapVirtualHandleRowId(reader, handle);
			}
			return handle.Offset;
		}

		/// <summary>Gets the metadata token of the specified <paramref name="handle" /> in the context of <paramref name="reader" />.</summary>
		/// <param name="reader" />
		/// <param name="handle" />
		/// <exception cref="T:System.NotSupportedException">The operation is not supported for the specified <paramref name="handle" />.</exception>
		/// <returns>Metadata token.</returns>
		// Token: 0x060009F9 RID: 2553 RVA: 0x0001D12D File Offset: 0x0001B32D
		public static int GetToken(this MetadataReader reader, EntityHandle handle)
		{
			if (handle.IsVirtual)
			{
				return (int)(handle.Type | (uint)MetadataTokens.MapVirtualHandleRowId(reader, handle));
			}
			return handle.Token;
		}

		/// <summary>Gets the metadata token of the specified <paramref name="handle" /> in the context of <paramref name="reader" />.</summary>
		/// <param name="reader" />
		/// <param name="handle" />
		/// <exception cref="T:System.ArgumentException">Handle represents a metadata entity that doesn't have a token.
		///       A token can only be retrieved for a metadata table handle or a heap handle of type <see cref="F:System.Reflection.Metadata.HandleKind.UserString" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The operation is not supported for the specified <paramref name="handle" />.</exception>
		/// <returns>Metadata token.</returns>
		// Token: 0x060009FA RID: 2554 RVA: 0x0001D154 File Offset: 0x0001B354
		public static int GetToken(this MetadataReader reader, Handle handle)
		{
			if (!handle.IsEntityOrUserStringHandle)
			{
				Throw.EntityOrUserStringHandleRequired();
			}
			if (handle.IsVirtual)
			{
				return (int)(handle.EntityHandleType | (uint)MetadataTokens.MapVirtualHandleRowId(reader, handle));
			}
			return handle.Token;
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0001D184 File Offset: 0x0001B384
		private static int MapVirtualHandleRowId(MetadataReader reader, Handle handle)
		{
			HandleKind kind = handle.Kind;
			if (kind == HandleKind.AssemblyReference)
			{
				return reader.AssemblyRefTable.NumberOfNonVirtualRows + 1 + handle.RowId;
			}
			if (kind != HandleKind.Blob && kind != HandleKind.String)
			{
				Throw.InvalidArgument_UnexpectedHandleKind(handle.Kind);
				return 0;
			}
			throw new NotSupportedException(SR.CantGetOffsetForVirtualHeapHandle);
		}

		/// <summary>Gets the row number of a metadata table entry that corresponds to the specified <paramref name="handle" />.</summary>
		/// <param name="handle" />
		/// <returns>One based row number, or -1 if <paramref name="handle" /> can only be interpreted in a context of a specific <see cref="T:System.Reflection.Metadata.MetadataReader" />.
		///  See <see cref="M:System.Reflection.Metadata.Ecma335.MetadataTokens.GetRowNumber(System.Reflection.Metadata.MetadataReader,System.Reflection.Metadata.EntityHandle)" />.</returns>
		// Token: 0x060009FC RID: 2556 RVA: 0x0001D1D9 File Offset: 0x0001B3D9
		public static int GetRowNumber(EntityHandle handle)
		{
			if (!handle.IsVirtual)
			{
				return handle.RowId;
			}
			return -1;
		}

		/// <summary>Gets the offset of metadata heap data that corresponds to the specified <paramref name="handle" />.</summary>
		/// <param name="handle" />
		/// <returns>An offset in the corresponding heap, or -1 if <paramref name="handle" /> can only be interpreted in a context of a specific <see cref="T:System.Reflection.Metadata.MetadataReader" /> or <see cref="T:System.Reflection.Metadata.Ecma335.MetadataBuilder" />.</returns>
		// Token: 0x060009FD RID: 2557 RVA: 0x0001D1ED File Offset: 0x0001B3ED
		public static int GetHeapOffset(Handle handle)
		{
			if (!handle.IsHeapHandle)
			{
				Throw.HeapHandleRequired();
			}
			if (handle.IsVirtual)
			{
				return -1;
			}
			return handle.Offset;
		}

		/// <summary>Gets the offset of metadata heap data that corresponds to the specified <paramref name="handle" />.</summary>
		/// <param name="handle" />
		/// <returns>Zero based offset, or -1 if <paramref name="handle" /> can only be interpreted in a context of a specific <see cref="T:System.Reflection.Metadata.MetadataReader" /> or <see cref="T:System.Reflection.Metadata.Ecma335.MetadataBuilder" />.</returns>
		// Token: 0x060009FE RID: 2558 RVA: 0x0001D20F File Offset: 0x0001B40F
		public static int GetHeapOffset(BlobHandle handle)
		{
			if (!handle.IsVirtual)
			{
				return handle.GetHeapOffset();
			}
			return -1;
		}

		/// <summary>Gets the offset of metadata heap data that corresponds to the specified <paramref name="handle" />.</summary>
		/// <param name="handle" />
		/// <returns>1-based index into the #Guid heap. Unlike other heaps, which are essentially byte arrays, the #Guid heap is an array of 16-byte GUIDs.</returns>
		// Token: 0x060009FF RID: 2559 RVA: 0x0001D223 File Offset: 0x0001B423
		public static int GetHeapOffset(GuidHandle handle)
		{
			return handle.Index;
		}

		/// <summary>Gets the offset of metadata heap data that corresponds to the specified <paramref name="handle" />.</summary>
		/// <param name="handle" />
		/// <returns>Zero-based offset.</returns>
		// Token: 0x06000A00 RID: 2560 RVA: 0x0001D22C File Offset: 0x0001B42C
		public static int GetHeapOffset(UserStringHandle handle)
		{
			return handle.GetHeapOffset();
		}

		/// <summary>Gets the offset of metadata heap data that corresponds to the specified <paramref name="handle" />.</summary>
		/// <param name="handle" />
		/// <returns>Zero-based offset, or -1 if <paramref name="handle" /> can only be interpreted in a context of a specific <see cref="T:System.Reflection.Metadata.MetadataReader" /> or <see cref="T:System.Reflection.Metadata.Ecma335.MetadataBuilder" />.</returns>
		// Token: 0x06000A01 RID: 2561 RVA: 0x0001D235 File Offset: 0x0001B435
		public static int GetHeapOffset(StringHandle handle)
		{
			if (!handle.IsVirtual)
			{
				return handle.GetHeapOffset();
			}
			return -1;
		}

		/// <summary>Gets the metadata token of the specified <paramref name="handle" />.</summary>
		/// <param name="handle" />
		/// <exception cref="T:System.ArgumentException">Handle represents a metadata entity that doesn't have a token.
		///       A token can only be retrieved for a metadata table handle or a heap handle of type <see cref="F:System.Reflection.Metadata.HandleKind.UserString" />.</exception>
		/// <returns>Metadata token, or 0 if <paramref name="handle" /> can only be interpreted in a context of a specific <see cref="T:System.Reflection.Metadata.MetadataReader" />.</returns>
		// Token: 0x06000A02 RID: 2562 RVA: 0x0001D249 File Offset: 0x0001B449
		public static int GetToken(Handle handle)
		{
			if (!handle.IsEntityOrUserStringHandle)
			{
				Throw.EntityOrUserStringHandleRequired();
			}
			if (handle.IsVirtual)
			{
				return 0;
			}
			return handle.Token;
		}

		/// <summary>Gets the metadata token of the specified <paramref name="handle" />.</summary>
		/// <param name="handle" />
		/// <returns>Metadata token, or 0 if <paramref name="handle" /> can only be interpreted in a context of a specific <see cref="T:System.Reflection.Metadata.MetadataReader" />.</returns>
		// Token: 0x06000A03 RID: 2563 RVA: 0x0001D26B File Offset: 0x0001B46B
		public static int GetToken(EntityHandle handle)
		{
			if (!handle.IsVirtual)
			{
				return handle.Token;
			}
			return 0;
		}

		/// <summary>Gets the <see cref="T:System.Reflection.Metadata.Ecma335.TableIndex" /> of the table corresponding to the specified <see cref="T:System.Reflection.Metadata.HandleKind" />.</summary>
		/// <param name="type">Handle type.</param>
		/// <param name="index">Table index.</param>
		/// <returns>
		///   <see langword="true" /> if the handle type corresponds to an Ecma335 or Portable PDB table; <see langword="false" /> otherwise.</returns>
		// Token: 0x06000A04 RID: 2564 RVA: 0x0001D27F File Offset: 0x0001B47F
		public static bool TryGetTableIndex(HandleKind type, out TableIndex index)
		{
			if ((int)type < MetadataTokens.TableCount && ((1L << (int)type) & 71811071505072127L) != 0L)
			{
				index = (TableIndex)type;
				return true;
			}
			index = TableIndex.Module;
			return false;
		}

		/// <summary>Gets the <see cref="T:System.Reflection.Metadata.Ecma335.HeapIndex" /> of the heap corresponding to the specified <see cref="T:System.Reflection.Metadata.HandleKind" />.</summary>
		/// <param name="type">Handle type.</param>
		/// <param name="index">Heap index.</param>
		/// <returns>
		///   <see langword="true" /> if the handle type corresponds to an Ecma335 heap; <see langword="false" /> otherwise.</returns>
		// Token: 0x06000A05 RID: 2565 RVA: 0x0001D2A5 File Offset: 0x0001B4A5
		public static bool TryGetHeapIndex(HandleKind type, out HeapIndex index)
		{
			switch (type)
			{
			case HandleKind.UserString:
				index = HeapIndex.UserString;
				return true;
			case HandleKind.Blob:
				index = HeapIndex.Blob;
				return true;
			case HandleKind.Guid:
				index = HeapIndex.Guid;
				return true;
			default:
				if (type != HandleKind.String && type != HandleKind.NamespaceDefinition)
				{
					index = HeapIndex.UserString;
					return false;
				}
				index = HeapIndex.String;
				return true;
			}
		}

		/// <summary>Creates a handle from a token value.</summary>
		/// <param name="token" />
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="token" /> is not a valid metadata token.
		///       It must encode a metadata table entity or an offset in the <see cref="F:System.Reflection.Metadata.HandleKind.UserString" /> heap.</exception>
		// Token: 0x06000A06 RID: 2566 RVA: 0x0001D2E0 File Offset: 0x0001B4E0
		public static Handle Handle(int token)
		{
			if (!TokenTypeIds.IsEntityOrUserStringToken((uint)token))
			{
				Throw.InvalidToken();
			}
			return global::System.Reflection.Metadata.Handle.FromVToken((uint)token);
		}

		/// <summary>Creates an entity handle from a token value.</summary>
		/// <param name="token" />
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="token" /> is not a valid metadata entity token.</exception>
		// Token: 0x06000A07 RID: 2567 RVA: 0x0001D2F5 File Offset: 0x0001B4F5
		public static EntityHandle EntityHandle(int token)
		{
			if (!TokenTypeIds.IsEntityToken((uint)token))
			{
				Throw.InvalidToken();
			}
			return new EntityHandle((uint)token);
		}

		/// <summary>Creates an <see cref="T:System.Reflection.Metadata.EntityHandle" /> from a token value.</summary>
		/// <param name="tableIndex" />
		/// <param name="rowNumber" />
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="tableIndex" /> is not a valid table index.</exception>
		// Token: 0x06000A08 RID: 2568 RVA: 0x0001D30A File Offset: 0x0001B50A
		public static EntityHandle EntityHandle(TableIndex tableIndex, int rowNumber)
		{
			return MetadataTokens.Handle(tableIndex, rowNumber);
		}

		/// <summary>Creates an <see cref="T:System.Reflection.Metadata.EntityHandle" /> from a token value.</summary>
		/// <param name="tableIndex" />
		/// <param name="rowNumber" />
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="tableIndex" /> is not a valid table index.</exception>
		// Token: 0x06000A09 RID: 2569 RVA: 0x0001D314 File Offset: 0x0001B514
		public static EntityHandle Handle(TableIndex tableIndex, int rowNumber)
		{
			int num = (int)((int)tableIndex << 24) | rowNumber;
			if (!TokenTypeIds.IsEntityOrUserStringToken((uint)num))
			{
				Throw.TableIndexOutOfRange();
			}
			return new EntityHandle((uint)num);
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0001D33B File Offset: 0x0001B53B
		private static int ToRowId(int rowNumber)
		{
			return rowNumber & 16777215;
		}

		/// <param name="rowNumber" />
		// Token: 0x06000A0B RID: 2571 RVA: 0x0001D344 File Offset: 0x0001B544
		public static MethodDefinitionHandle MethodDefinitionHandle(int rowNumber)
		{
			return global::System.Reflection.Metadata.MethodDefinitionHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		/// <param name="rowNumber" />
		// Token: 0x06000A0C RID: 2572 RVA: 0x0001D351 File Offset: 0x0001B551
		public static MethodImplementationHandle MethodImplementationHandle(int rowNumber)
		{
			return global::System.Reflection.Metadata.MethodImplementationHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		/// <param name="rowNumber" />
		// Token: 0x06000A0D RID: 2573 RVA: 0x0001D35E File Offset: 0x0001B55E
		public static MethodSpecificationHandle MethodSpecificationHandle(int rowNumber)
		{
			return global::System.Reflection.Metadata.MethodSpecificationHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		/// <param name="rowNumber" />
		// Token: 0x06000A0E RID: 2574 RVA: 0x0001D36B File Offset: 0x0001B56B
		public static TypeDefinitionHandle TypeDefinitionHandle(int rowNumber)
		{
			return global::System.Reflection.Metadata.TypeDefinitionHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		/// <param name="rowNumber" />
		// Token: 0x06000A0F RID: 2575 RVA: 0x0001D378 File Offset: 0x0001B578
		public static ExportedTypeHandle ExportedTypeHandle(int rowNumber)
		{
			return global::System.Reflection.Metadata.ExportedTypeHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		/// <param name="rowNumber" />
		// Token: 0x06000A10 RID: 2576 RVA: 0x0001D385 File Offset: 0x0001B585
		public static TypeReferenceHandle TypeReferenceHandle(int rowNumber)
		{
			return global::System.Reflection.Metadata.TypeReferenceHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		/// <param name="rowNumber" />
		// Token: 0x06000A11 RID: 2577 RVA: 0x0001D392 File Offset: 0x0001B592
		public static TypeSpecificationHandle TypeSpecificationHandle(int rowNumber)
		{
			return global::System.Reflection.Metadata.TypeSpecificationHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		/// <param name="rowNumber" />
		// Token: 0x06000A12 RID: 2578 RVA: 0x0001D39F File Offset: 0x0001B59F
		public static InterfaceImplementationHandle InterfaceImplementationHandle(int rowNumber)
		{
			return global::System.Reflection.Metadata.InterfaceImplementationHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		/// <param name="rowNumber" />
		// Token: 0x06000A13 RID: 2579 RVA: 0x0001D3AC File Offset: 0x0001B5AC
		public static MemberReferenceHandle MemberReferenceHandle(int rowNumber)
		{
			return global::System.Reflection.Metadata.MemberReferenceHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		/// <param name="rowNumber" />
		// Token: 0x06000A14 RID: 2580 RVA: 0x0001D3B9 File Offset: 0x0001B5B9
		public static FieldDefinitionHandle FieldDefinitionHandle(int rowNumber)
		{
			return global::System.Reflection.Metadata.FieldDefinitionHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		/// <param name="rowNumber" />
		// Token: 0x06000A15 RID: 2581 RVA: 0x0001D3C6 File Offset: 0x0001B5C6
		public static EventDefinitionHandle EventDefinitionHandle(int rowNumber)
		{
			return global::System.Reflection.Metadata.EventDefinitionHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		/// <param name="rowNumber" />
		// Token: 0x06000A16 RID: 2582 RVA: 0x0001D3D3 File Offset: 0x0001B5D3
		public static PropertyDefinitionHandle PropertyDefinitionHandle(int rowNumber)
		{
			return global::System.Reflection.Metadata.PropertyDefinitionHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		/// <param name="rowNumber" />
		// Token: 0x06000A17 RID: 2583 RVA: 0x0001D3E0 File Offset: 0x0001B5E0
		public static StandaloneSignatureHandle StandaloneSignatureHandle(int rowNumber)
		{
			return global::System.Reflection.Metadata.StandaloneSignatureHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		/// <param name="rowNumber" />
		// Token: 0x06000A18 RID: 2584 RVA: 0x0001D3ED File Offset: 0x0001B5ED
		public static ParameterHandle ParameterHandle(int rowNumber)
		{
			return global::System.Reflection.Metadata.ParameterHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		/// <param name="rowNumber" />
		// Token: 0x06000A19 RID: 2585 RVA: 0x0001D3FA File Offset: 0x0001B5FA
		public static GenericParameterHandle GenericParameterHandle(int rowNumber)
		{
			return global::System.Reflection.Metadata.GenericParameterHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		/// <param name="rowNumber" />
		// Token: 0x06000A1A RID: 2586 RVA: 0x0001D407 File Offset: 0x0001B607
		public static GenericParameterConstraintHandle GenericParameterConstraintHandle(int rowNumber)
		{
			return global::System.Reflection.Metadata.GenericParameterConstraintHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		/// <param name="rowNumber" />
		// Token: 0x06000A1B RID: 2587 RVA: 0x0001D414 File Offset: 0x0001B614
		public static ModuleReferenceHandle ModuleReferenceHandle(int rowNumber)
		{
			return global::System.Reflection.Metadata.ModuleReferenceHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		/// <param name="rowNumber" />
		// Token: 0x06000A1C RID: 2588 RVA: 0x0001D421 File Offset: 0x0001B621
		public static AssemblyReferenceHandle AssemblyReferenceHandle(int rowNumber)
		{
			return global::System.Reflection.Metadata.AssemblyReferenceHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		/// <param name="rowNumber" />
		// Token: 0x06000A1D RID: 2589 RVA: 0x0001D42E File Offset: 0x0001B62E
		public static CustomAttributeHandle CustomAttributeHandle(int rowNumber)
		{
			return global::System.Reflection.Metadata.CustomAttributeHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		/// <param name="rowNumber" />
		// Token: 0x06000A1E RID: 2590 RVA: 0x0001D43B File Offset: 0x0001B63B
		public static DeclarativeSecurityAttributeHandle DeclarativeSecurityAttributeHandle(int rowNumber)
		{
			return global::System.Reflection.Metadata.DeclarativeSecurityAttributeHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		/// <param name="rowNumber" />
		// Token: 0x06000A1F RID: 2591 RVA: 0x0001D448 File Offset: 0x0001B648
		public static ConstantHandle ConstantHandle(int rowNumber)
		{
			return global::System.Reflection.Metadata.ConstantHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		/// <param name="rowNumber" />
		// Token: 0x06000A20 RID: 2592 RVA: 0x0001D455 File Offset: 0x0001B655
		public static ManifestResourceHandle ManifestResourceHandle(int rowNumber)
		{
			return global::System.Reflection.Metadata.ManifestResourceHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		/// <param name="rowNumber" />
		// Token: 0x06000A21 RID: 2593 RVA: 0x0001D462 File Offset: 0x0001B662
		public static AssemblyFileHandle AssemblyFileHandle(int rowNumber)
		{
			return global::System.Reflection.Metadata.AssemblyFileHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		/// <param name="rowNumber" />
		// Token: 0x06000A22 RID: 2594 RVA: 0x0001D46F File Offset: 0x0001B66F
		public static DocumentHandle DocumentHandle(int rowNumber)
		{
			return global::System.Reflection.Metadata.DocumentHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		/// <param name="rowNumber" />
		// Token: 0x06000A23 RID: 2595 RVA: 0x0001D47C File Offset: 0x0001B67C
		public static MethodDebugInformationHandle MethodDebugInformationHandle(int rowNumber)
		{
			return global::System.Reflection.Metadata.MethodDebugInformationHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		/// <param name="rowNumber" />
		// Token: 0x06000A24 RID: 2596 RVA: 0x0001D489 File Offset: 0x0001B689
		public static LocalScopeHandle LocalScopeHandle(int rowNumber)
		{
			return global::System.Reflection.Metadata.LocalScopeHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		/// <param name="rowNumber" />
		// Token: 0x06000A25 RID: 2597 RVA: 0x0001D496 File Offset: 0x0001B696
		public static LocalVariableHandle LocalVariableHandle(int rowNumber)
		{
			return global::System.Reflection.Metadata.LocalVariableHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		/// <param name="rowNumber" />
		// Token: 0x06000A26 RID: 2598 RVA: 0x0001D4A3 File Offset: 0x0001B6A3
		public static LocalConstantHandle LocalConstantHandle(int rowNumber)
		{
			return global::System.Reflection.Metadata.LocalConstantHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		/// <param name="rowNumber" />
		// Token: 0x06000A27 RID: 2599 RVA: 0x0001D4B0 File Offset: 0x0001B6B0
		public static ImportScopeHandle ImportScopeHandle(int rowNumber)
		{
			return global::System.Reflection.Metadata.ImportScopeHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		/// <param name="rowNumber" />
		// Token: 0x06000A28 RID: 2600 RVA: 0x0001D4BD File Offset: 0x0001B6BD
		public static CustomDebugInformationHandle CustomDebugInformationHandle(int rowNumber)
		{
			return global::System.Reflection.Metadata.CustomDebugInformationHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		/// <param name="offset" />
		// Token: 0x06000A29 RID: 2601 RVA: 0x0001D4CA File Offset: 0x0001B6CA
		public static UserStringHandle UserStringHandle(int offset)
		{
			return global::System.Reflection.Metadata.UserStringHandle.FromOffset(offset & 16777215);
		}

		/// <param name="offset" />
		// Token: 0x06000A2A RID: 2602 RVA: 0x0001D4D8 File Offset: 0x0001B6D8
		public static StringHandle StringHandle(int offset)
		{
			return global::System.Reflection.Metadata.StringHandle.FromOffset(offset);
		}

		/// <param name="offset" />
		// Token: 0x06000A2B RID: 2603 RVA: 0x0001D4E0 File Offset: 0x0001B6E0
		public static BlobHandle BlobHandle(int offset)
		{
			return global::System.Reflection.Metadata.BlobHandle.FromOffset(offset);
		}

		/// <param name="offset" />
		// Token: 0x06000A2C RID: 2604 RVA: 0x0001D4E8 File Offset: 0x0001B6E8
		public static GuidHandle GuidHandle(int offset)
		{
			return global::System.Reflection.Metadata.GuidHandle.FromIndex(offset);
		}

		/// <param name="offset" />
		// Token: 0x06000A2D RID: 2605 RVA: 0x0001D4F0 File Offset: 0x0001B6F0
		public static DocumentNameBlobHandle DocumentNameBlobHandle(int offset)
		{
			return global::System.Reflection.Metadata.DocumentNameBlobHandle.FromOffset(offset);
		}

		/// <summary>Maximum number of tables that can be present in Ecma335 metadata.</summary>
		// Token: 0x040005B8 RID: 1464
		public static readonly int TableCount = 64;

		/// <summary>Maximum number of tables that can be present in Ecma335 metadata.</summary>
		// Token: 0x040005B9 RID: 1465
		public static readonly int HeapCount = 4;
	}
}
