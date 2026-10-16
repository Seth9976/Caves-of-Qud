using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Reflection.Internal;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Reflection.Metadata
{
	/// <summary>Reads metadata as defined by the ECMA 335 CLI specification.</summary>
	// Token: 0x020000A6 RID: 166
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MetadataReader
	{
		// Token: 0x060005F2 RID: 1522 RVA: 0x0000D584 File Offset: 0x0000B784
		internal AssemblyName GetAssemblyName(StringHandle nameHandle, Version version, StringHandle cultureHandle, BlobHandle publicKeyOrTokenHandle, AssemblyHashAlgorithm assemblyHashAlgorithm, AssemblyFlags flags)
		{
			string @string = this.GetString(nameHandle);
			string text = ((!cultureHandle.IsNil) ? this.GetString(cultureHandle) : null);
			byte[] array = ((!publicKeyOrTokenHandle.IsNil) ? this.GetBlobBytes(publicKeyOrTokenHandle) : null);
			AssemblyName assemblyName = new AssemblyName(@string)
			{
				Version = version,
				CultureName = text,
				HashAlgorithm = assemblyHashAlgorithm,
				Flags = this.GetAssemblyNameFlags(flags),
				ContentType = this.GetContentTypeFromAssemblyFlags(flags)
			};
			bool flag = (flags & AssemblyFlags.PublicKey) > (AssemblyFlags)0;
			if (flag)
			{
				assemblyName.SetPublicKey(array);
			}
			else
			{
				assemblyName.SetPublicKeyToken(array);
			}
			return assemblyName;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0000D620 File Offset: 0x0000B820
		private AssemblyNameFlags GetAssemblyNameFlags(AssemblyFlags flags)
		{
			AssemblyNameFlags assemblyNameFlags = 0;
			if ((flags & AssemblyFlags.PublicKey) != (AssemblyFlags)0)
			{
				assemblyNameFlags |= 1;
			}
			if ((flags & AssemblyFlags.Retargetable) != (AssemblyFlags)0)
			{
				assemblyNameFlags |= 256;
			}
			if ((flags & AssemblyFlags.EnableJitCompileTracking) != (AssemblyFlags)0)
			{
				assemblyNameFlags |= 32768;
			}
			if ((flags & AssemblyFlags.DisableJitCompileOptimizer) != (AssemblyFlags)0)
			{
				assemblyNameFlags |= 16384;
			}
			return assemblyNameFlags;
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0000D66C File Offset: 0x0000B86C
		private AssemblyContentType GetContentTypeFromAssemblyFlags(AssemblyFlags flags)
		{
			return (flags & AssemblyFlags.ContentTypeMask) >> 9;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.Metadata.MetadataReader" /> class from the metadata stored at the given memory location.</summary>
		/// <param name="metadata">A pointer to the first byte in a block of metadata.</param>
		/// <param name="length">The number of bytes in the block.</param>
		// Token: 0x060005F5 RID: 1525 RVA: 0x0000D678 File Offset: 0x0000B878
		[NullableContext(0)]
		public unsafe MetadataReader(byte* metadata, int length)
			: this(metadata, length, MetadataReaderOptions.Default, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.Metadata.MetadataReader" /> class from the metadata stored at the given memory location.</summary>
		/// <param name="metadata" />
		/// <param name="length" />
		/// <param name="options" />
		// Token: 0x060005F6 RID: 1526 RVA: 0x0000D685 File Offset: 0x0000B885
		[NullableContext(0)]
		public unsafe MetadataReader(byte* metadata, int length, MetadataReaderOptions options)
			: this(metadata, length, options, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.Metadata.MetadataReader" /> class from the metadata stored at the given memory location.</summary>
		/// <param name="metadata" />
		/// <param name="length" />
		/// <param name="options" />
		/// <param name="utf8Decoder" />
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="length" /> is not positive.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="metadata" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The encoding of <paramref name="utf8Decoder" /> is not <see cref="T:System.Text.UTF8Encoding" />.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current platform is big-endian.</exception>
		/// <exception cref="T:System.BadImageFormatException">Bad metadata header.</exception>
		// Token: 0x060005F7 RID: 1527 RVA: 0x0000D692 File Offset: 0x0000B892
		[NullableContext(0)]
		public unsafe MetadataReader(byte* metadata, int length, MetadataReaderOptions options, [Nullable(2)] MetadataStringDecoder utf8Decoder)
			: this(metadata, length, options, utf8Decoder, null)
		{
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0000D6A0 File Offset: 0x0000B8A0
		[NullableContext(2)]
		internal unsafe MetadataReader([Nullable(0)] byte* metadata, int length, MetadataReaderOptions options, MetadataStringDecoder utf8Decoder, object memoryOwner)
		{
			if (length < 0)
			{
				Throw.ArgumentOutOfRange("length");
			}
			if (metadata == null)
			{
				Throw.ArgumentNull("metadata");
			}
			if (utf8Decoder == null)
			{
				utf8Decoder = MetadataStringDecoder.DefaultUTF8;
			}
			if (!(utf8Decoder.Encoding is UTF8Encoding))
			{
				Throw.InvalidArgument(SR.MetadataStringDecoderEncodingMustBeUtf8, "utf8Decoder");
			}
			this.Block = new MemoryBlock(metadata, length);
			this._memoryOwnerObj = memoryOwner;
			this._options = options;
			this.UTF8Decoder = utf8Decoder;
			BlobReader blobReader = new BlobReader(this.Block);
			this.ReadMetadataHeader(ref blobReader, out this._versionString);
			this._metadataKind = this.GetMetadataKind(this._versionString);
			StreamHeader[] array = this.ReadStreamHeaders(ref blobReader);
			MemoryBlock memoryBlock;
			MemoryBlock memoryBlock2;
			this.InitializeStreamReaders(in this.Block, array, out this._metadataStreamKind, out memoryBlock, out memoryBlock2);
			int[] array2;
			if (memoryBlock2.Length > 0)
			{
				int num = (int)((long)(memoryBlock2.Pointer - metadata));
				MetadataReader.ReadStandalonePortablePdbStream(memoryBlock2, num, out this._debugMetadataHeader, out array2);
			}
			else
			{
				array2 = null;
			}
			BlobReader blobReader2 = new BlobReader(memoryBlock);
			HeapSizes heapSizes;
			int[] array3;
			this.ReadMetadataTableHeader(ref blobReader2, out heapSizes, out array3, out this._sortedTables);
			this.InitializeTableReaders(blobReader2.GetMemoryBlockAt(0, blobReader2.RemainingBytes), heapSizes, array3, array2);
			if (memoryBlock2.Length == 0 && this.ModuleTable.NumberOfRows < 1)
			{
				throw new BadImageFormatException(SR.Format(SR.ModuleTableInvalidNumberOfRows, this.ModuleTable.NumberOfRows));
			}
			this.NamespaceCache = new NamespaceCache(this);
			if (this._metadataKind != MetadataKind.Ecma335)
			{
				this.WinMDMscorlibRef = this.FindMscorlibAssemblyRefNoProjection();
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0000D820 File Offset: 0x0000BA20
		private void ReadMetadataHeader(ref BlobReader memReader, out string versionString)
		{
			if (memReader.RemainingBytes < 16)
			{
				throw new BadImageFormatException(SR.MetadataHeaderTooSmall);
			}
			uint num = memReader.ReadUInt32();
			if (num != 1112167234U)
			{
				throw new BadImageFormatException(SR.MetadataSignature);
			}
			memReader.ReadUInt16();
			memReader.ReadUInt16();
			memReader.ReadUInt32();
			int num2 = memReader.ReadInt32();
			if (memReader.RemainingBytes < num2)
			{
				throw new BadImageFormatException(SR.NotEnoughSpaceForVersionString);
			}
			int num3;
			versionString = memReader.GetMemoryBlockAt(0, num2).PeekUtf8NullTerminated(0, null, this.UTF8Decoder, out num3, '\0');
			memReader.Offset += num2;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0000D8B7 File Offset: 0x0000BAB7
		private MetadataKind GetMetadataKind(string versionString)
		{
			if ((this._options & MetadataReaderOptions.Default) == MetadataReaderOptions.None)
			{
				return MetadataKind.Ecma335;
			}
			if (!versionString.Contains("WindowsRuntime"))
			{
				return MetadataKind.Ecma335;
			}
			if (versionString.Contains("CLR"))
			{
				return MetadataKind.ManagedWindowsMetadata;
			}
			return MetadataKind.WindowsMetadata;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0000D8E4 File Offset: 0x0000BAE4
		private StreamHeader[] ReadStreamHeaders(ref BlobReader memReader)
		{
			memReader.ReadUInt16();
			int num = (int)memReader.ReadInt16();
			StreamHeader[] array = new StreamHeader[num];
			for (int i = 0; i < array.Length; i++)
			{
				if (memReader.RemainingBytes < 8)
				{
					throw new BadImageFormatException(SR.StreamHeaderTooSmall);
				}
				array[i].Offset = memReader.ReadUInt32();
				array[i].Size = memReader.ReadInt32();
				array[i].Name = memReader.ReadUtf8NullTerminated();
				bool flag = memReader.TryAlign(4);
				if (!flag || memReader.RemainingBytes == 0)
				{
					throw new BadImageFormatException(SR.NotEnoughSpaceForStreamHeaderName);
				}
			}
			return array;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0000D980 File Offset: 0x0000BB80
		private void InitializeStreamReaders(in MemoryBlock metadataRoot, StreamHeader[] streamHeaders, out MetadataStreamKind metadataStreamKind, out MemoryBlock metadataTableStream, out MemoryBlock standalonePdbStream)
		{
			metadataTableStream = default(MemoryBlock);
			standalonePdbStream = default(MemoryBlock);
			metadataStreamKind = MetadataStreamKind.Illegal;
			foreach (StreamHeader streamHeader in streamHeaders)
			{
				string name = streamHeader.Name;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
				if (num <= 617129517U)
				{
					if (num <= 368124450U)
					{
						if (num != 154065968U)
						{
							if (num == 368124450U)
							{
								if (name == "#US")
								{
									if ((long)metadataRoot.Length < (long)((ulong)streamHeader.Offset + (ulong)((long)streamHeader.Size)))
									{
										throw new BadImageFormatException(SR.NotEnoughSpaceForBlobStream);
									}
									this.UserStringHeap = new UserStringHeap(metadataRoot.GetMemoryBlockAt((int)streamHeader.Offset, streamHeader.Size));
								}
							}
						}
						else if (name == "#JTD")
						{
							if ((long)metadataRoot.Length < (long)((ulong)streamHeader.Offset + (ulong)((long)streamHeader.Size)))
							{
								throw new BadImageFormatException(SR.NotEnoughSpaceForMetadataStream);
							}
							this.IsMinimalDelta = true;
						}
					}
					else if (num != 491825896U)
					{
						if (num == 617129517U)
						{
							if (name == "#-")
							{
								if ((long)metadataRoot.Length < (long)((ulong)streamHeader.Offset + (ulong)((long)streamHeader.Size)))
								{
									throw new BadImageFormatException(SR.NotEnoughSpaceForMetadataStream);
								}
								metadataStreamKind = MetadataStreamKind.Uncompressed;
								metadataTableStream = metadataRoot.GetMemoryBlockAt((int)streamHeader.Offset, streamHeader.Size);
							}
						}
					}
					else if (name == "#Strings")
					{
						if ((long)metadataRoot.Length < (long)((ulong)streamHeader.Offset + (ulong)((long)streamHeader.Size)))
						{
							throw new BadImageFormatException(SR.NotEnoughSpaceForStringStream);
						}
						this.StringHeap = new StringHeap(metadataRoot.GetMemoryBlockAt((int)streamHeader.Offset, streamHeader.Size), this._metadataKind);
					}
				}
				else if (num <= 1422005491U)
				{
					if (num != 1372122372U)
					{
						if (num == 1422005491U)
						{
							if (name == "#GUID")
							{
								if ((long)metadataRoot.Length < (long)((ulong)streamHeader.Offset + (ulong)((long)streamHeader.Size)))
								{
									throw new BadImageFormatException(SR.NotEnoughSpaceForGUIDStream);
								}
								this.GuidHeap = new GuidHeap(metadataRoot.GetMemoryBlockAt((int)streamHeader.Offset, streamHeader.Size));
							}
						}
					}
					else if (name == "#~")
					{
						if ((long)metadataRoot.Length < (long)((ulong)streamHeader.Offset + (ulong)((long)streamHeader.Size)))
						{
							throw new BadImageFormatException(SR.NotEnoughSpaceForMetadataStream);
						}
						metadataStreamKind = MetadataStreamKind.Compressed;
						metadataTableStream = metadataRoot.GetMemoryBlockAt((int)streamHeader.Offset, streamHeader.Size);
					}
				}
				else if (num != 1638201209U)
				{
					if (num == 2979271308U)
					{
						if (name == "#Pdb")
						{
							if ((long)metadataRoot.Length < (long)((ulong)streamHeader.Offset + (ulong)((long)streamHeader.Size)))
							{
								throw new BadImageFormatException(SR.NotEnoughSpaceForMetadataStream);
							}
							standalonePdbStream = metadataRoot.GetMemoryBlockAt((int)streamHeader.Offset, streamHeader.Size);
						}
					}
				}
				else if (name == "#Blob")
				{
					if ((long)metadataRoot.Length < (long)((ulong)streamHeader.Offset + (ulong)((long)streamHeader.Size)))
					{
						throw new BadImageFormatException(SR.NotEnoughSpaceForBlobStream);
					}
					this.BlobHeap = new BlobHeap(metadataRoot.GetMemoryBlockAt((int)streamHeader.Offset, streamHeader.Size), this._metadataKind);
				}
			}
			if (this.IsMinimalDelta && metadataStreamKind != MetadataStreamKind.Uncompressed)
			{
				throw new BadImageFormatException(SR.InvalidMetadataStreamFormat);
			}
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0000DD20 File Offset: 0x0000BF20
		private void ReadMetadataTableHeader(ref BlobReader reader, out HeapSizes heapSizes, out int[] metadataTableRowCounts, out TableMask sortedTables)
		{
			if (reader.RemainingBytes < 24)
			{
				throw new BadImageFormatException(SR.MetadataTableHeaderTooSmall);
			}
			reader.ReadUInt32();
			reader.ReadByte();
			reader.ReadByte();
			heapSizes = (HeapSizes)reader.ReadByte();
			reader.ReadByte();
			ulong num = reader.ReadUInt64();
			sortedTables = (TableMask)reader.ReadUInt64();
			ulong num2 = 71811071505072127UL;
			if ((num & ~(num2 != 0UL)) != 0UL)
			{
				throw new BadImageFormatException(SR.Format(SR.UnknownTables, num));
			}
			if (this._metadataStreamKind == MetadataStreamKind.Compressed && (num & (ulong)(-2142764888)) != 0UL)
			{
				throw new BadImageFormatException(SR.IllegalTablesInCompressedMetadataStream);
			}
			metadataTableRowCounts = MetadataReader.ReadMetadataTableRowCounts(ref reader, num);
			if ((heapSizes & HeapSizes.ExtraData) == HeapSizes.ExtraData)
			{
				reader.ReadUInt32();
			}
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0000DDD4 File Offset: 0x0000BFD4
		private static int[] ReadMetadataTableRowCounts(ref BlobReader memReader, ulong presentTableMask)
		{
			ulong num = 1UL;
			int[] array = new int[MetadataTokens.TableCount];
			for (int i = 0; i < array.Length; i++)
			{
				if ((presentTableMask & num) != 0UL)
				{
					if (memReader.RemainingBytes < 4)
					{
						throw new BadImageFormatException(SR.TableRowCountSpaceTooSmall);
					}
					uint num2 = memReader.ReadUInt32();
					if (num2 > 16777215U)
					{
						throw new BadImageFormatException(SR.Format(SR.InvalidRowCount, num2));
					}
					array[i] = (int)num2;
				}
				num <<= 1;
			}
			return array;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0000DE44 File Offset: 0x0000C044
		internal static void ReadStandalonePortablePdbStream(MemoryBlock pdbStreamBlock, int pdbStreamOffset, out DebugMetadataHeader debugMetadataHeader, out int[] externalTableRowCounts)
		{
			BlobReader blobReader = new BlobReader(pdbStreamBlock);
			byte[] array = blobReader.ReadBytes(20);
			uint num = blobReader.ReadUInt32();
			int num2 = (int)(num & 16777215U);
			if (num != 0U && ((num & 2130706432U) != 100663296U || num2 == 0))
			{
				throw new BadImageFormatException(SR.Format(SR.InvalidEntryPointToken, num));
			}
			ulong num3 = blobReader.ReadUInt64();
			if ((num3 & 18446709124491641000UL) != 0UL)
			{
				throw new BadImageFormatException(SR.Format(SR.UnknownTables, num3));
			}
			externalTableRowCounts = MetadataReader.ReadMetadataTableRowCounts(ref blobReader, num3);
			debugMetadataHeader = new DebugMetadataHeader(ImmutableByteArrayInterop.DangerousCreateFromUnderlyingArray(ref array), MethodDefinitionHandle.FromRowId(num2), pdbStreamOffset);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0000DEEB File Offset: 0x0000C0EB
		private int GetReferenceSize(int[] rowCounts, TableIndex index)
		{
			if ((long)rowCounts[(int)index] >= 65536L || this.IsMinimalDelta)
			{
				return 4;
			}
			return 2;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0000DF04 File Offset: 0x0000C104
		private void InitializeTableReaders(MemoryBlock metadataTablesMemoryBlock, HeapSizes heapSizes, int[] rowCounts, int[] externalRowCountsOpt)
		{
			this.TableRowCounts = rowCounts;
			int num = ((this.GetReferenceSize(rowCounts, TableIndex.FieldPtr) > 2) ? 4 : this.GetReferenceSize(rowCounts, TableIndex.Field));
			int num2 = ((this.GetReferenceSize(rowCounts, TableIndex.MethodPtr) > 2) ? 4 : this.GetReferenceSize(rowCounts, TableIndex.MethodDef));
			int num3 = ((this.GetReferenceSize(rowCounts, TableIndex.ParamPtr) > 2) ? 4 : this.GetReferenceSize(rowCounts, TableIndex.Param));
			int num4 = ((this.GetReferenceSize(rowCounts, TableIndex.EventPtr) > 2) ? 4 : this.GetReferenceSize(rowCounts, TableIndex.Event));
			int num5 = ((this.GetReferenceSize(rowCounts, TableIndex.PropertyPtr) > 2) ? 4 : this.GetReferenceSize(rowCounts, TableIndex.Property));
			int num6 = this.ComputeCodedTokenSize(16384, rowCounts, TableMask.TypeRef | TableMask.TypeDef | TableMask.TypeSpec);
			int num7 = this.ComputeCodedTokenSize(16384, rowCounts, TableMask.Field | TableMask.Param | TableMask.Property);
			int num8 = this.ComputeCodedTokenSize(2048, rowCounts, TableMask.Module | TableMask.TypeRef | TableMask.TypeDef | TableMask.Field | TableMask.MethodDef | TableMask.Param | TableMask.InterfaceImpl | TableMask.MemberRef | TableMask.DeclSecurity | TableMask.StandAloneSig | TableMask.Event | TableMask.Property | TableMask.ModuleRef | TableMask.TypeSpec | TableMask.Assembly | TableMask.AssemblyRef | TableMask.File | TableMask.ExportedType | TableMask.ManifestResource | TableMask.GenericParam | TableMask.MethodSpec | TableMask.GenericParamConstraint);
			int num9 = this.ComputeCodedTokenSize(32768, rowCounts, TableMask.Field | TableMask.Param);
			int num10 = this.ComputeCodedTokenSize(16384, rowCounts, TableMask.TypeDef | TableMask.MethodDef | TableMask.Assembly);
			int num11 = this.ComputeCodedTokenSize(8192, rowCounts, TableMask.TypeRef | TableMask.TypeDef | TableMask.MethodDef | TableMask.ModuleRef | TableMask.TypeSpec);
			int num12 = this.ComputeCodedTokenSize(32768, rowCounts, TableMask.Event | TableMask.Property);
			int num13 = this.ComputeCodedTokenSize(32768, rowCounts, TableMask.MethodDef | TableMask.MemberRef);
			int num14 = this.ComputeCodedTokenSize(32768, rowCounts, TableMask.Field | TableMask.MethodDef);
			int num15 = this.ComputeCodedTokenSize(16384, rowCounts, TableMask.AssemblyRef | TableMask.File | TableMask.ExportedType);
			int num16 = this.ComputeCodedTokenSize(8192, rowCounts, TableMask.MethodDef | TableMask.MemberRef);
			int num17 = this.ComputeCodedTokenSize(16384, rowCounts, TableMask.Module | TableMask.TypeRef | TableMask.ModuleRef | TableMask.AssemblyRef);
			int num18 = this.ComputeCodedTokenSize(32768, rowCounts, TableMask.TypeDef | TableMask.MethodDef);
			int num19 = (((heapSizes & HeapSizes.StringHeapLarge) == HeapSizes.StringHeapLarge) ? 4 : 2);
			int num20 = (((heapSizes & HeapSizes.GuidHeapLarge) == HeapSizes.GuidHeapLarge) ? 4 : 2);
			int num21 = (((heapSizes & HeapSizes.BlobHeapLarge) == HeapSizes.BlobHeapLarge) ? 4 : 2);
			int num22 = 0;
			this.ModuleTable = new ModuleTableReader(rowCounts[0], num19, num20, metadataTablesMemoryBlock, num22);
			num22 += this.ModuleTable.Block.Length;
			this.TypeRefTable = new TypeRefTableReader(rowCounts[1], num17, num19, metadataTablesMemoryBlock, num22);
			num22 += this.TypeRefTable.Block.Length;
			this.TypeDefTable = new TypeDefTableReader(rowCounts[2], num, num2, num6, num19, metadataTablesMemoryBlock, num22);
			num22 += this.TypeDefTable.Block.Length;
			this.FieldPtrTable = new FieldPtrTableReader(rowCounts[3], this.GetReferenceSize(rowCounts, TableIndex.Field), metadataTablesMemoryBlock, num22);
			num22 += this.FieldPtrTable.Block.Length;
			this.FieldTable = new FieldTableReader(rowCounts[4], num19, num21, metadataTablesMemoryBlock, num22);
			num22 += this.FieldTable.Block.Length;
			this.MethodPtrTable = new MethodPtrTableReader(rowCounts[5], this.GetReferenceSize(rowCounts, TableIndex.MethodDef), metadataTablesMemoryBlock, num22);
			num22 += this.MethodPtrTable.Block.Length;
			this.MethodDefTable = new MethodTableReader(rowCounts[6], num3, num19, num21, metadataTablesMemoryBlock, num22);
			num22 += this.MethodDefTable.Block.Length;
			this.ParamPtrTable = new ParamPtrTableReader(rowCounts[7], this.GetReferenceSize(rowCounts, TableIndex.Param), metadataTablesMemoryBlock, num22);
			num22 += this.ParamPtrTable.Block.Length;
			this.ParamTable = new ParamTableReader(rowCounts[8], num19, metadataTablesMemoryBlock, num22);
			num22 += this.ParamTable.Block.Length;
			this.InterfaceImplTable = new InterfaceImplTableReader(rowCounts[9], this.IsDeclaredSorted(TableMask.InterfaceImpl), this.GetReferenceSize(rowCounts, TableIndex.TypeDef), num6, metadataTablesMemoryBlock, num22);
			num22 += this.InterfaceImplTable.Block.Length;
			this.MemberRefTable = new MemberRefTableReader(rowCounts[10], num11, num19, num21, metadataTablesMemoryBlock, num22);
			num22 += this.MemberRefTable.Block.Length;
			this.ConstantTable = new ConstantTableReader(rowCounts[11], this.IsDeclaredSorted(TableMask.Constant), num7, num21, metadataTablesMemoryBlock, num22);
			num22 += this.ConstantTable.Block.Length;
			this.CustomAttributeTable = new CustomAttributeTableReader(rowCounts[12], this.IsDeclaredSorted(TableMask.CustomAttribute), num8, num16, num21, metadataTablesMemoryBlock, num22);
			num22 += this.CustomAttributeTable.Block.Length;
			this.FieldMarshalTable = new FieldMarshalTableReader(rowCounts[13], this.IsDeclaredSorted(TableMask.FieldMarshal), num9, num21, metadataTablesMemoryBlock, num22);
			num22 += this.FieldMarshalTable.Block.Length;
			this.DeclSecurityTable = new DeclSecurityTableReader(rowCounts[14], this.IsDeclaredSorted(TableMask.DeclSecurity), num10, num21, metadataTablesMemoryBlock, num22);
			num22 += this.DeclSecurityTable.Block.Length;
			this.ClassLayoutTable = new ClassLayoutTableReader(rowCounts[15], this.IsDeclaredSorted(TableMask.ClassLayout), this.GetReferenceSize(rowCounts, TableIndex.TypeDef), metadataTablesMemoryBlock, num22);
			num22 += this.ClassLayoutTable.Block.Length;
			this.FieldLayoutTable = new FieldLayoutTableReader(rowCounts[16], this.IsDeclaredSorted(TableMask.FieldLayout), this.GetReferenceSize(rowCounts, TableIndex.Field), metadataTablesMemoryBlock, num22);
			num22 += this.FieldLayoutTable.Block.Length;
			this.StandAloneSigTable = new StandAloneSigTableReader(rowCounts[17], num21, metadataTablesMemoryBlock, num22);
			num22 += this.StandAloneSigTable.Block.Length;
			this.EventMapTable = new EventMapTableReader(rowCounts[18], this.GetReferenceSize(rowCounts, TableIndex.TypeDef), num4, metadataTablesMemoryBlock, num22);
			num22 += this.EventMapTable.Block.Length;
			this.EventPtrTable = new EventPtrTableReader(rowCounts[19], this.GetReferenceSize(rowCounts, TableIndex.Event), metadataTablesMemoryBlock, num22);
			num22 += this.EventPtrTable.Block.Length;
			this.EventTable = new EventTableReader(rowCounts[20], num6, num19, metadataTablesMemoryBlock, num22);
			num22 += this.EventTable.Block.Length;
			this.PropertyMapTable = new PropertyMapTableReader(rowCounts[21], this.GetReferenceSize(rowCounts, TableIndex.TypeDef), num5, metadataTablesMemoryBlock, num22);
			num22 += this.PropertyMapTable.Block.Length;
			this.PropertyPtrTable = new PropertyPtrTableReader(rowCounts[22], this.GetReferenceSize(rowCounts, TableIndex.Property), metadataTablesMemoryBlock, num22);
			num22 += this.PropertyPtrTable.Block.Length;
			this.PropertyTable = new PropertyTableReader(rowCounts[23], num19, num21, metadataTablesMemoryBlock, num22);
			num22 += this.PropertyTable.Block.Length;
			this.MethodSemanticsTable = new MethodSemanticsTableReader(rowCounts[24], this.IsDeclaredSorted(TableMask.MethodSemantics), this.GetReferenceSize(rowCounts, TableIndex.MethodDef), num12, metadataTablesMemoryBlock, num22);
			num22 += this.MethodSemanticsTable.Block.Length;
			this.MethodImplTable = new MethodImplTableReader(rowCounts[25], this.IsDeclaredSorted(TableMask.MethodImpl), this.GetReferenceSize(rowCounts, TableIndex.TypeDef), num13, metadataTablesMemoryBlock, num22);
			num22 += this.MethodImplTable.Block.Length;
			this.ModuleRefTable = new ModuleRefTableReader(rowCounts[26], num19, metadataTablesMemoryBlock, num22);
			num22 += this.ModuleRefTable.Block.Length;
			this.TypeSpecTable = new TypeSpecTableReader(rowCounts[27], num21, metadataTablesMemoryBlock, num22);
			num22 += this.TypeSpecTable.Block.Length;
			this.ImplMapTable = new ImplMapTableReader(rowCounts[28], this.IsDeclaredSorted(TableMask.ImplMap), this.GetReferenceSize(rowCounts, TableIndex.ModuleRef), num14, num19, metadataTablesMemoryBlock, num22);
			num22 += this.ImplMapTable.Block.Length;
			this.FieldRvaTable = new FieldRVATableReader(rowCounts[29], this.IsDeclaredSorted(TableMask.FieldRva), this.GetReferenceSize(rowCounts, TableIndex.Field), metadataTablesMemoryBlock, num22);
			num22 += this.FieldRvaTable.Block.Length;
			this.EncLogTable = new EnCLogTableReader(rowCounts[30], metadataTablesMemoryBlock, num22, this._metadataStreamKind);
			num22 += this.EncLogTable.Block.Length;
			this.EncMapTable = new EnCMapTableReader(rowCounts[31], metadataTablesMemoryBlock, num22);
			num22 += this.EncMapTable.Block.Length;
			this.AssemblyTable = new AssemblyTableReader(rowCounts[32], num19, num21, metadataTablesMemoryBlock, num22);
			num22 += this.AssemblyTable.Block.Length;
			this.AssemblyProcessorTable = new AssemblyProcessorTableReader(rowCounts[33], metadataTablesMemoryBlock, num22);
			num22 += this.AssemblyProcessorTable.Block.Length;
			this.AssemblyOSTable = new AssemblyOSTableReader(rowCounts[34], metadataTablesMemoryBlock, num22);
			num22 += this.AssemblyOSTable.Block.Length;
			this.AssemblyRefTable = new AssemblyRefTableReader(rowCounts[35], num19, num21, metadataTablesMemoryBlock, num22, this._metadataKind);
			num22 += this.AssemblyRefTable.Block.Length;
			this.AssemblyRefProcessorTable = new AssemblyRefProcessorTableReader(rowCounts[36], this.GetReferenceSize(rowCounts, TableIndex.AssemblyRef), metadataTablesMemoryBlock, num22);
			num22 += this.AssemblyRefProcessorTable.Block.Length;
			this.AssemblyRefOSTable = new AssemblyRefOSTableReader(rowCounts[37], this.GetReferenceSize(rowCounts, TableIndex.AssemblyRef), metadataTablesMemoryBlock, num22);
			num22 += this.AssemblyRefOSTable.Block.Length;
			this.FileTable = new FileTableReader(rowCounts[38], num19, num21, metadataTablesMemoryBlock, num22);
			num22 += this.FileTable.Block.Length;
			this.ExportedTypeTable = new ExportedTypeTableReader(rowCounts[39], num15, num19, metadataTablesMemoryBlock, num22);
			num22 += this.ExportedTypeTable.Block.Length;
			this.ManifestResourceTable = new ManifestResourceTableReader(rowCounts[40], num15, num19, metadataTablesMemoryBlock, num22);
			num22 += this.ManifestResourceTable.Block.Length;
			this.NestedClassTable = new NestedClassTableReader(rowCounts[41], this.IsDeclaredSorted(TableMask.NestedClass), this.GetReferenceSize(rowCounts, TableIndex.TypeDef), metadataTablesMemoryBlock, num22);
			num22 += this.NestedClassTable.Block.Length;
			this.GenericParamTable = new GenericParamTableReader(rowCounts[42], this.IsDeclaredSorted(TableMask.GenericParam), num18, num19, metadataTablesMemoryBlock, num22);
			num22 += this.GenericParamTable.Block.Length;
			this.MethodSpecTable = new MethodSpecTableReader(rowCounts[43], num13, num21, metadataTablesMemoryBlock, num22);
			num22 += this.MethodSpecTable.Block.Length;
			this.GenericParamConstraintTable = new GenericParamConstraintTableReader(rowCounts[44], this.IsDeclaredSorted(TableMask.GenericParamConstraint), this.GetReferenceSize(rowCounts, TableIndex.GenericParam), num6, metadataTablesMemoryBlock, num22);
			num22 += this.GenericParamConstraintTable.Block.Length;
			int[] array = ((externalRowCountsOpt != null) ? MetadataReader.CombineRowCounts(rowCounts, externalRowCountsOpt, TableIndex.Document) : rowCounts);
			int referenceSize = this.GetReferenceSize(array, TableIndex.MethodDef);
			int num23 = this.ComputeCodedTokenSize(2048, array, TableMask.Module | TableMask.TypeRef | TableMask.TypeDef | TableMask.Field | TableMask.MethodDef | TableMask.Param | TableMask.InterfaceImpl | TableMask.MemberRef | TableMask.DeclSecurity | TableMask.StandAloneSig | TableMask.Event | TableMask.Property | TableMask.ModuleRef | TableMask.TypeSpec | TableMask.Assembly | TableMask.AssemblyRef | TableMask.File | TableMask.ExportedType | TableMask.ManifestResource | TableMask.GenericParam | TableMask.MethodSpec | TableMask.GenericParamConstraint | TableMask.Document | TableMask.LocalScope | TableMask.LocalVariable | TableMask.LocalConstant | TableMask.ImportScope);
			this.DocumentTable = new DocumentTableReader(rowCounts[48], num20, num21, metadataTablesMemoryBlock, num22);
			num22 += this.DocumentTable.Block.Length;
			this.MethodDebugInformationTable = new MethodDebugInformationTableReader(rowCounts[49], this.GetReferenceSize(rowCounts, TableIndex.Document), num21, metadataTablesMemoryBlock, num22);
			num22 += this.MethodDebugInformationTable.Block.Length;
			this.LocalScopeTable = new LocalScopeTableReader(rowCounts[50], this.IsDeclaredSorted(TableMask.LocalScope), referenceSize, this.GetReferenceSize(rowCounts, TableIndex.ImportScope), this.GetReferenceSize(rowCounts, TableIndex.LocalVariable), this.GetReferenceSize(rowCounts, TableIndex.LocalConstant), metadataTablesMemoryBlock, num22);
			num22 += this.LocalScopeTable.Block.Length;
			this.LocalVariableTable = new LocalVariableTableReader(rowCounts[51], num19, metadataTablesMemoryBlock, num22);
			num22 += this.LocalVariableTable.Block.Length;
			this.LocalConstantTable = new LocalConstantTableReader(rowCounts[52], num19, num21, metadataTablesMemoryBlock, num22);
			num22 += this.LocalConstantTable.Block.Length;
			this.ImportScopeTable = new ImportScopeTableReader(rowCounts[53], this.GetReferenceSize(rowCounts, TableIndex.ImportScope), num21, metadataTablesMemoryBlock, num22);
			num22 += this.ImportScopeTable.Block.Length;
			this.StateMachineMethodTable = new StateMachineMethodTableReader(rowCounts[54], this.IsDeclaredSorted(TableMask.StateMachineMethod), referenceSize, metadataTablesMemoryBlock, num22);
			num22 += this.StateMachineMethodTable.Block.Length;
			this.CustomDebugInformationTable = new CustomDebugInformationTableReader(rowCounts[55], this.IsDeclaredSorted(TableMask.CustomDebugInformation), num23, num20, num21, metadataTablesMemoryBlock, num22);
			num22 += this.CustomDebugInformationTable.Block.Length;
			if (num22 > metadataTablesMemoryBlock.Length)
			{
				throw new BadImageFormatException(SR.MetadataTablesTooSmall);
			}
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0000EB48 File Offset: 0x0000CD48
		private static int[] CombineRowCounts(int[] local, int[] external, TableIndex firstLocalTableIndex)
		{
			int[] array = new int[local.Length];
			for (int i = 0; i < (int)firstLocalTableIndex; i++)
			{
				array[i] = external[i];
			}
			for (int j = (int)firstLocalTableIndex; j < array.Length; j++)
			{
				array[j] = local[j];
			}
			return array;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0000EB88 File Offset: 0x0000CD88
		private int ComputeCodedTokenSize(int largeRowSize, int[] rowCounts, TableMask tablesReferenced)
		{
			if (this.IsMinimalDelta)
			{
				return 4;
			}
			bool flag = true;
			ulong num = (ulong)tablesReferenced;
			for (int i = 0; i < MetadataTokens.TableCount; i++)
			{
				if ((num & 1UL) != 0UL)
				{
					flag = flag && rowCounts[i] < largeRowSize;
				}
				num >>= 1;
			}
			if (!flag)
			{
				return 4;
			}
			return 2;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0000EBD0 File Offset: 0x0000CDD0
		private bool IsDeclaredSorted(TableMask index)
		{
			return (this._sortedTables & index) > (TableMask)0UL;
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x0000EBDE File Offset: 0x0000CDDE
		internal bool UseFieldPtrTable
		{
			get
			{
				return this.FieldPtrTable.NumberOfRows > 0;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x0000EBEE File Offset: 0x0000CDEE
		internal bool UseMethodPtrTable
		{
			get
			{
				return this.MethodPtrTable.NumberOfRows > 0;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x0000EBFE File Offset: 0x0000CDFE
		internal bool UseParamPtrTable
		{
			get
			{
				return this.ParamPtrTable.NumberOfRows > 0;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x0000EC0E File Offset: 0x0000CE0E
		internal bool UseEventPtrTable
		{
			get
			{
				return this.EventPtrTable.NumberOfRows > 0;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x0000EC1E File Offset: 0x0000CE1E
		internal bool UsePropertyPtrTable
		{
			get
			{
				return this.PropertyPtrTable.NumberOfRows > 0;
			}
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0000EC30 File Offset: 0x0000CE30
		internal void GetFieldRange(TypeDefinitionHandle typeDef, out int firstFieldRowId, out int lastFieldRowId)
		{
			int rowId = typeDef.RowId;
			firstFieldRowId = this.TypeDefTable.GetFieldStart(rowId);
			if (firstFieldRowId == 0)
			{
				firstFieldRowId = 1;
				lastFieldRowId = 0;
				return;
			}
			if (rowId == this.TypeDefTable.NumberOfRows)
			{
				lastFieldRowId = (this.UseFieldPtrTable ? this.FieldPtrTable.NumberOfRows : this.FieldTable.NumberOfRows);
				return;
			}
			lastFieldRowId = this.TypeDefTable.GetFieldStart(rowId + 1) - 1;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0000ECA4 File Offset: 0x0000CEA4
		internal void GetMethodRange(TypeDefinitionHandle typeDef, out int firstMethodRowId, out int lastMethodRowId)
		{
			int rowId = typeDef.RowId;
			firstMethodRowId = this.TypeDefTable.GetMethodStart(rowId);
			if (firstMethodRowId == 0)
			{
				firstMethodRowId = 1;
				lastMethodRowId = 0;
				return;
			}
			if (rowId == this.TypeDefTable.NumberOfRows)
			{
				lastMethodRowId = (this.UseMethodPtrTable ? this.MethodPtrTable.NumberOfRows : this.MethodDefTable.NumberOfRows);
				return;
			}
			lastMethodRowId = this.TypeDefTable.GetMethodStart(rowId + 1) - 1;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0000ED18 File Offset: 0x0000CF18
		internal void GetEventRange(TypeDefinitionHandle typeDef, out int firstEventRowId, out int lastEventRowId)
		{
			int num = this.EventMapTable.FindEventMapRowIdFor(typeDef);
			if (num == 0)
			{
				firstEventRowId = 1;
				lastEventRowId = 0;
				return;
			}
			firstEventRowId = this.EventMapTable.GetEventListStartFor(num);
			if (num == this.EventMapTable.NumberOfRows)
			{
				lastEventRowId = (this.UseEventPtrTable ? this.EventPtrTable.NumberOfRows : this.EventTable.NumberOfRows);
				return;
			}
			lastEventRowId = this.EventMapTable.GetEventListStartFor(num + 1) - 1;
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0000ED90 File Offset: 0x0000CF90
		internal void GetPropertyRange(TypeDefinitionHandle typeDef, out int firstPropertyRowId, out int lastPropertyRowId)
		{
			int num = this.PropertyMapTable.FindPropertyMapRowIdFor(typeDef);
			if (num == 0)
			{
				firstPropertyRowId = 1;
				lastPropertyRowId = 0;
				return;
			}
			firstPropertyRowId = this.PropertyMapTable.GetPropertyListStartFor(num);
			if (num == this.PropertyMapTable.NumberOfRows)
			{
				lastPropertyRowId = (this.UsePropertyPtrTable ? this.PropertyPtrTable.NumberOfRows : this.PropertyTable.NumberOfRows);
				return;
			}
			lastPropertyRowId = this.PropertyMapTable.GetPropertyListStartFor(num + 1) - 1;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0000EE08 File Offset: 0x0000D008
		internal void GetParameterRange(MethodDefinitionHandle methodDef, out int firstParamRowId, out int lastParamRowId)
		{
			int rowId = methodDef.RowId;
			firstParamRowId = this.MethodDefTable.GetParamStart(rowId);
			if (firstParamRowId == 0)
			{
				firstParamRowId = 1;
				lastParamRowId = 0;
				return;
			}
			if (rowId == this.MethodDefTable.NumberOfRows)
			{
				lastParamRowId = (this.UseParamPtrTable ? this.ParamPtrTable.NumberOfRows : this.ParamTable.NumberOfRows);
				return;
			}
			lastParamRowId = this.MethodDefTable.GetParamStart(rowId + 1) - 1;
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0000EE7C File Offset: 0x0000D07C
		internal void GetLocalVariableRange(LocalScopeHandle scope, out int firstVariableRowId, out int lastVariableRowId)
		{
			int rowId = scope.RowId;
			firstVariableRowId = this.LocalScopeTable.GetVariableStart(rowId);
			if (firstVariableRowId == 0)
			{
				firstVariableRowId = 1;
				lastVariableRowId = 0;
				return;
			}
			if (rowId == this.LocalScopeTable.NumberOfRows)
			{
				lastVariableRowId = this.LocalVariableTable.NumberOfRows;
				return;
			}
			lastVariableRowId = this.LocalScopeTable.GetVariableStart(rowId + 1) - 1;
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0000EED8 File Offset: 0x0000D0D8
		internal void GetLocalConstantRange(LocalScopeHandle scope, out int firstConstantRowId, out int lastConstantRowId)
		{
			int rowId = scope.RowId;
			firstConstantRowId = this.LocalScopeTable.GetConstantStart(rowId);
			if (firstConstantRowId == 0)
			{
				firstConstantRowId = 1;
				lastConstantRowId = 0;
				return;
			}
			if (rowId == this.LocalScopeTable.NumberOfRows)
			{
				lastConstantRowId = this.LocalConstantTable.NumberOfRows;
				return;
			}
			lastConstantRowId = this.LocalScopeTable.GetConstantStart(rowId + 1) - 1;
		}

		/// <summary>Gets the pointer to the underlying data.</summary>
		/// <returns>The pointer to the underlying data.</returns>
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0000EF34 File Offset: 0x0000D134
		[Nullable(0)]
		public unsafe byte* MetadataPointer
		{
			[NullableContext(0)]
			get
			{
				return this.Block.Pointer;
			}
		}

		/// <summary>Gets the length of the underlying data.</summary>
		/// <returns>The length of the underlying data.</returns>
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x0000EF41 File Offset: 0x0000D141
		public int MetadataLength
		{
			get
			{
				return this.Block.Length;
			}
		}

		/// <summary>Gets the <see cref="T:System.Reflection.Metadata.MetadataReaderOptions" /> passed to the constructor.</summary>
		/// <returns>A bitwise combination of the enumeration values that describes the <see cref="T:System.Reflection.Metadata.MetadataReaderOptions" /> enum value.</returns>
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x0000EF4E File Offset: 0x0000D14E
		public MetadataReaderOptions Options
		{
			get
			{
				return this._options;
			}
		}

		/// <summary>Gets the version string read from metadata header.</summary>
		/// <returns>The version string read from metadata header.</returns>
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x0000EF56 File Offset: 0x0000D156
		public string MetadataVersion
		{
			get
			{
				return this._versionString;
			}
		}

		/// <summary>Gets the information decoded from #Pdb stream, or <see langword="null" /> if the stream is not present.</summary>
		/// <returns>The information decoded from #Pdb stream, or <see langword="null" /> if the stream is not present.</returns>
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x0000EF5E File Offset: 0x0000D15E
		[Nullable(2)]
		public DebugMetadataHeader DebugMetadataHeader
		{
			[NullableContext(2)]
			get
			{
				return this._debugMetadataHeader;
			}
		}

		/// <summary>Gets the metadata kind.</summary>
		/// <returns>One of the enumeration values that specifies the metadata kind.</returns>
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x0000EF66 File Offset: 0x0000D166
		public MetadataKind MetadataKind
		{
			get
			{
				return this._metadataKind;
			}
		}

		/// <summary>Gets the comparer used to compare strings stored in metadata.</summary>
		/// <returns>The comparer used to compare strings stored in metadata.</returns>
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x0000EF6E File Offset: 0x0000D16E
		public MetadataStringComparer StringComparer
		{
			get
			{
				return new MetadataStringComparer(this);
			}
		}

		/// <summary>Gets the decoder used by the reader to produce string instances from UTF8-encoded byte sequences.</summary>
		/// <returns>The decoder used by the reader to produce string instances from UTF8-encoded byte sequences.</returns>
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x0000EF76 File Offset: 0x0000D176
		public MetadataStringDecoder UTF8Decoder { get; }

		/// <summary>Gets a value that indicates whether the metadata represents an assembly.</summary>
		/// <returns>
		///   <see langword="true" /> if the metadata represents an assembly; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x0000EF7E File Offset: 0x0000D17E
		public bool IsAssembly
		{
			get
			{
				return this.AssemblyTable.NumberOfRows == 1;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x0000EF8E File Offset: 0x0000D18E
		public AssemblyReferenceHandleCollection AssemblyReferences
		{
			get
			{
				return new AssemblyReferenceHandleCollection(this);
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x0000EF96 File Offset: 0x0000D196
		public TypeDefinitionHandleCollection TypeDefinitions
		{
			get
			{
				return new TypeDefinitionHandleCollection(this.TypeDefTable.NumberOfRows);
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x0000EFA8 File Offset: 0x0000D1A8
		public TypeReferenceHandleCollection TypeReferences
		{
			get
			{
				return new TypeReferenceHandleCollection(this.TypeRefTable.NumberOfRows);
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x0000EFBA File Offset: 0x0000D1BA
		public CustomAttributeHandleCollection CustomAttributes
		{
			get
			{
				return new CustomAttributeHandleCollection(this);
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x0000EFC2 File Offset: 0x0000D1C2
		public DeclarativeSecurityAttributeHandleCollection DeclarativeSecurityAttributes
		{
			get
			{
				return new DeclarativeSecurityAttributeHandleCollection(this);
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x0000EFCA File Offset: 0x0000D1CA
		public MemberReferenceHandleCollection MemberReferences
		{
			get
			{
				return new MemberReferenceHandleCollection(this.MemberRefTable.NumberOfRows);
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x0000EFDC File Offset: 0x0000D1DC
		public ManifestResourceHandleCollection ManifestResources
		{
			get
			{
				return new ManifestResourceHandleCollection(this.ManifestResourceTable.NumberOfRows);
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x0000EFEE File Offset: 0x0000D1EE
		public AssemblyFileHandleCollection AssemblyFiles
		{
			get
			{
				return new AssemblyFileHandleCollection(this.FileTable.NumberOfRows);
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x0000F000 File Offset: 0x0000D200
		public ExportedTypeHandleCollection ExportedTypes
		{
			get
			{
				return new ExportedTypeHandleCollection(this.ExportedTypeTable.NumberOfRows);
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x0000F012 File Offset: 0x0000D212
		public MethodDefinitionHandleCollection MethodDefinitions
		{
			get
			{
				return new MethodDefinitionHandleCollection(this);
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x0000F01A File Offset: 0x0000D21A
		public FieldDefinitionHandleCollection FieldDefinitions
		{
			get
			{
				return new FieldDefinitionHandleCollection(this);
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x0000F022 File Offset: 0x0000D222
		public EventDefinitionHandleCollection EventDefinitions
		{
			get
			{
				return new EventDefinitionHandleCollection(this);
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x0000F02A File Offset: 0x0000D22A
		public PropertyDefinitionHandleCollection PropertyDefinitions
		{
			get
			{
				return new PropertyDefinitionHandleCollection(this);
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x0000F032 File Offset: 0x0000D232
		public DocumentHandleCollection Documents
		{
			get
			{
				return new DocumentHandleCollection(this);
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x0000F03A File Offset: 0x0000D23A
		public MethodDebugInformationHandleCollection MethodDebugInformation
		{
			get
			{
				return new MethodDebugInformationHandleCollection(this);
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x0000F042 File Offset: 0x0000D242
		public LocalScopeHandleCollection LocalScopes
		{
			get
			{
				return new LocalScopeHandleCollection(this, 0);
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x0000F04C File Offset: 0x0000D24C
		public LocalVariableHandleCollection LocalVariables
		{
			get
			{
				return new LocalVariableHandleCollection(this, default(LocalScopeHandle));
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x0000F068 File Offset: 0x0000D268
		public LocalConstantHandleCollection LocalConstants
		{
			get
			{
				return new LocalConstantHandleCollection(this, default(LocalScopeHandle));
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0000F084 File Offset: 0x0000D284
		public ImportScopeCollection ImportScopes
		{
			get
			{
				return new ImportScopeCollection(this);
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x0000F08C File Offset: 0x0000D28C
		public CustomDebugInformationHandleCollection CustomDebugInformation
		{
			get
			{
				return new CustomDebugInformationHandleCollection(this);
			}
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0000F094 File Offset: 0x0000D294
		public AssemblyDefinition GetAssemblyDefinition()
		{
			if (!this.IsAssembly)
			{
				throw new InvalidOperationException(SR.MetadataImageDoesNotRepresentAnAssembly);
			}
			return new AssemblyDefinition(this);
		}

		/// <param name="handle" />
		// Token: 0x0600062F RID: 1583 RVA: 0x0000F0AF File Offset: 0x0000D2AF
		public string GetString(StringHandle handle)
		{
			return this.StringHeap.GetString(handle, this.UTF8Decoder);
		}

		/// <param name="handle" />
		// Token: 0x06000630 RID: 1584 RVA: 0x0000F0C3 File Offset: 0x0000D2C3
		public string GetString(NamespaceDefinitionHandle handle)
		{
			if (handle.HasFullName)
			{
				return this.StringHeap.GetString(handle.GetFullName(), this.UTF8Decoder);
			}
			return this.NamespaceCache.GetFullName(handle);
		}

		/// <param name="handle" />
		// Token: 0x06000631 RID: 1585 RVA: 0x0000F0F3 File Offset: 0x0000D2F3
		public byte[] GetBlobBytes(BlobHandle handle)
		{
			return this.BlobHeap.GetBytes(handle);
		}

		/// <param name="handle" />
		// Token: 0x06000632 RID: 1586 RVA: 0x0000F104 File Offset: 0x0000D304
		[NullableContext(0)]
		public ImmutableArray<byte> GetBlobContent(BlobHandle handle)
		{
			byte[] blobBytes = this.GetBlobBytes(handle);
			return ImmutableByteArrayInterop.DangerousCreateFromUnderlyingArray(ref blobBytes);
		}

		/// <param name="handle" />
		// Token: 0x06000633 RID: 1587 RVA: 0x0000F120 File Offset: 0x0000D320
		public BlobReader GetBlobReader(BlobHandle handle)
		{
			return this.BlobHeap.GetBlobReader(handle);
		}

		/// <param name="handle" />
		// Token: 0x06000634 RID: 1588 RVA: 0x0000F12E File Offset: 0x0000D32E
		public BlobReader GetBlobReader(StringHandle handle)
		{
			return this.StringHeap.GetBlobReader(handle);
		}

		/// <param name="handle" />
		// Token: 0x06000635 RID: 1589 RVA: 0x0000F13C File Offset: 0x0000D33C
		public string GetUserString(UserStringHandle handle)
		{
			return this.UserStringHeap.GetString(handle);
		}

		/// <param name="handle" />
		// Token: 0x06000636 RID: 1590 RVA: 0x0000F14A File Offset: 0x0000D34A
		public Guid GetGuid(GuidHandle handle)
		{
			return this.GuidHeap.GetGuid(handle);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0000F158 File Offset: 0x0000D358
		public ModuleDefinition GetModuleDefinition()
		{
			if (this._debugMetadataHeader != null)
			{
				throw new InvalidOperationException(SR.StandaloneDebugMetadataImageDoesNotContainModuleTable);
			}
			return new ModuleDefinition(this);
		}

		/// <param name="handle" />
		// Token: 0x06000638 RID: 1592 RVA: 0x0000F173 File Offset: 0x0000D373
		public AssemblyReference GetAssemblyReference(AssemblyReferenceHandle handle)
		{
			return new AssemblyReference(this, handle.Value);
		}

		/// <param name="handle" />
		// Token: 0x06000639 RID: 1593 RVA: 0x0000F182 File Offset: 0x0000D382
		public TypeDefinition GetTypeDefinition(TypeDefinitionHandle handle)
		{
			return new TypeDefinition(this, this.GetTypeDefTreatmentAndRowId(handle));
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0000F194 File Offset: 0x0000D394
		public NamespaceDefinition GetNamespaceDefinitionRoot()
		{
			NamespaceData rootNamespace = this.NamespaceCache.GetRootNamespace();
			return new NamespaceDefinition(rootNamespace);
		}

		/// <param name="handle" />
		// Token: 0x0600063B RID: 1595 RVA: 0x0000F1B4 File Offset: 0x0000D3B4
		public NamespaceDefinition GetNamespaceDefinition(NamespaceDefinitionHandle handle)
		{
			NamespaceData namespaceData = this.NamespaceCache.GetNamespaceData(handle);
			return new NamespaceDefinition(namespaceData);
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0000F1D4 File Offset: 0x0000D3D4
		private uint GetTypeDefTreatmentAndRowId(TypeDefinitionHandle handle)
		{
			if (this._metadataKind == MetadataKind.Ecma335)
			{
				return (uint)handle.RowId;
			}
			return this.CalculateTypeDefTreatmentAndRowId(handle);
		}

		/// <param name="handle" />
		// Token: 0x0600063D RID: 1597 RVA: 0x0000F1ED File Offset: 0x0000D3ED
		public TypeReference GetTypeReference(TypeReferenceHandle handle)
		{
			return new TypeReference(this, this.GetTypeRefTreatmentAndRowId(handle));
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0000F1FC File Offset: 0x0000D3FC
		private uint GetTypeRefTreatmentAndRowId(TypeReferenceHandle handle)
		{
			if (this._metadataKind == MetadataKind.Ecma335)
			{
				return (uint)handle.RowId;
			}
			return this.CalculateTypeRefTreatmentAndRowId(handle);
		}

		/// <param name="handle" />
		// Token: 0x0600063F RID: 1599 RVA: 0x0000F215 File Offset: 0x0000D415
		public ExportedType GetExportedType(ExportedTypeHandle handle)
		{
			return new ExportedType(this, handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x06000640 RID: 1600 RVA: 0x0000F224 File Offset: 0x0000D424
		public CustomAttributeHandleCollection GetCustomAttributes(EntityHandle handle)
		{
			return new CustomAttributeHandleCollection(this, handle);
		}

		/// <param name="handle" />
		// Token: 0x06000641 RID: 1601 RVA: 0x0000F22D File Offset: 0x0000D42D
		public CustomAttribute GetCustomAttribute(CustomAttributeHandle handle)
		{
			return new CustomAttribute(this, this.GetCustomAttributeTreatmentAndRowId(handle));
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0000F23C File Offset: 0x0000D43C
		private uint GetCustomAttributeTreatmentAndRowId(CustomAttributeHandle handle)
		{
			if (this._metadataKind == MetadataKind.Ecma335)
			{
				return (uint)handle.RowId;
			}
			return MetadataReader.TreatmentAndRowId(1, handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x06000643 RID: 1603 RVA: 0x0000F25B File Offset: 0x0000D45B
		public DeclarativeSecurityAttribute GetDeclarativeSecurityAttribute(DeclarativeSecurityAttributeHandle handle)
		{
			return new DeclarativeSecurityAttribute(this, handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x06000644 RID: 1604 RVA: 0x0000F26A File Offset: 0x0000D46A
		public Constant GetConstant(ConstantHandle handle)
		{
			return new Constant(this, handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x06000645 RID: 1605 RVA: 0x0000F279 File Offset: 0x0000D479
		public MethodDefinition GetMethodDefinition(MethodDefinitionHandle handle)
		{
			return new MethodDefinition(this, this.GetMethodDefTreatmentAndRowId(handle));
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0000F288 File Offset: 0x0000D488
		private uint GetMethodDefTreatmentAndRowId(MethodDefinitionHandle handle)
		{
			if (this._metadataKind == MetadataKind.Ecma335)
			{
				return (uint)handle.RowId;
			}
			return this.CalculateMethodDefTreatmentAndRowId(handle);
		}

		/// <param name="handle" />
		// Token: 0x06000647 RID: 1607 RVA: 0x0000F2A1 File Offset: 0x0000D4A1
		public FieldDefinition GetFieldDefinition(FieldDefinitionHandle handle)
		{
			return new FieldDefinition(this, this.GetFieldDefTreatmentAndRowId(handle));
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0000F2B0 File Offset: 0x0000D4B0
		private uint GetFieldDefTreatmentAndRowId(FieldDefinitionHandle handle)
		{
			if (this._metadataKind == MetadataKind.Ecma335)
			{
				return (uint)handle.RowId;
			}
			return this.CalculateFieldDefTreatmentAndRowId(handle);
		}

		/// <param name="handle" />
		// Token: 0x06000649 RID: 1609 RVA: 0x0000F2C9 File Offset: 0x0000D4C9
		public PropertyDefinition GetPropertyDefinition(PropertyDefinitionHandle handle)
		{
			return new PropertyDefinition(this, handle);
		}

		/// <param name="handle" />
		// Token: 0x0600064A RID: 1610 RVA: 0x0000F2D2 File Offset: 0x0000D4D2
		public EventDefinition GetEventDefinition(EventDefinitionHandle handle)
		{
			return new EventDefinition(this, handle);
		}

		/// <param name="handle" />
		// Token: 0x0600064B RID: 1611 RVA: 0x0000F2DB File Offset: 0x0000D4DB
		public MethodImplementation GetMethodImplementation(MethodImplementationHandle handle)
		{
			return new MethodImplementation(this, handle);
		}

		/// <param name="handle" />
		// Token: 0x0600064C RID: 1612 RVA: 0x0000F2E4 File Offset: 0x0000D4E4
		public MemberReference GetMemberReference(MemberReferenceHandle handle)
		{
			return new MemberReference(this, this.GetMemberRefTreatmentAndRowId(handle));
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0000F2F3 File Offset: 0x0000D4F3
		private uint GetMemberRefTreatmentAndRowId(MemberReferenceHandle handle)
		{
			if (this._metadataKind == MetadataKind.Ecma335)
			{
				return (uint)handle.RowId;
			}
			return this.CalculateMemberRefTreatmentAndRowId(handle);
		}

		/// <param name="handle" />
		// Token: 0x0600064E RID: 1614 RVA: 0x0000F30C File Offset: 0x0000D50C
		public MethodSpecification GetMethodSpecification(MethodSpecificationHandle handle)
		{
			return new MethodSpecification(this, handle);
		}

		/// <param name="handle" />
		// Token: 0x0600064F RID: 1615 RVA: 0x0000F315 File Offset: 0x0000D515
		public Parameter GetParameter(ParameterHandle handle)
		{
			return new Parameter(this, handle);
		}

		/// <param name="handle" />
		// Token: 0x06000650 RID: 1616 RVA: 0x0000F31E File Offset: 0x0000D51E
		public GenericParameter GetGenericParameter(GenericParameterHandle handle)
		{
			return new GenericParameter(this, handle);
		}

		/// <param name="handle" />
		// Token: 0x06000651 RID: 1617 RVA: 0x0000F327 File Offset: 0x0000D527
		public GenericParameterConstraint GetGenericParameterConstraint(GenericParameterConstraintHandle handle)
		{
			return new GenericParameterConstraint(this, handle);
		}

		/// <param name="handle" />
		// Token: 0x06000652 RID: 1618 RVA: 0x0000F330 File Offset: 0x0000D530
		public ManifestResource GetManifestResource(ManifestResourceHandle handle)
		{
			return new ManifestResource(this, handle);
		}

		/// <param name="handle" />
		// Token: 0x06000653 RID: 1619 RVA: 0x0000F339 File Offset: 0x0000D539
		public AssemblyFile GetAssemblyFile(AssemblyFileHandle handle)
		{
			return new AssemblyFile(this, handle);
		}

		/// <param name="handle" />
		// Token: 0x06000654 RID: 1620 RVA: 0x0000F342 File Offset: 0x0000D542
		public StandaloneSignature GetStandaloneSignature(StandaloneSignatureHandle handle)
		{
			return new StandaloneSignature(this, handle);
		}

		/// <param name="handle" />
		// Token: 0x06000655 RID: 1621 RVA: 0x0000F34B File Offset: 0x0000D54B
		public TypeSpecification GetTypeSpecification(TypeSpecificationHandle handle)
		{
			return new TypeSpecification(this, handle);
		}

		/// <param name="handle" />
		// Token: 0x06000656 RID: 1622 RVA: 0x0000F354 File Offset: 0x0000D554
		public ModuleReference GetModuleReference(ModuleReferenceHandle handle)
		{
			return new ModuleReference(this, handle);
		}

		/// <param name="handle" />
		// Token: 0x06000657 RID: 1623 RVA: 0x0000F35D File Offset: 0x0000D55D
		public InterfaceImplementation GetInterfaceImplementation(InterfaceImplementationHandle handle)
		{
			return new InterfaceImplementation(this, handle);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0000F368 File Offset: 0x0000D568
		internal TypeDefinitionHandle GetDeclaringType(MethodDefinitionHandle methodDef)
		{
			int num;
			if (this.UseMethodPtrTable)
			{
				num = this.MethodPtrTable.GetRowIdForMethodDefRow(methodDef.RowId);
			}
			else
			{
				num = methodDef.RowId;
			}
			return this.TypeDefTable.FindTypeContainingMethod(num, this.MethodDefTable.NumberOfRows);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0000F3B4 File Offset: 0x0000D5B4
		internal TypeDefinitionHandle GetDeclaringType(FieldDefinitionHandle fieldDef)
		{
			int num;
			if (this.UseFieldPtrTable)
			{
				num = this.FieldPtrTable.GetRowIdForFieldDefRow(fieldDef.RowId);
			}
			else
			{
				num = fieldDef.RowId;
			}
			return this.TypeDefTable.FindTypeContainingField(num, this.FieldTable.NumberOfRows);
		}

		/// <param name="handle" />
		// Token: 0x0600065A RID: 1626 RVA: 0x0000F3FD File Offset: 0x0000D5FD
		public string GetString(DocumentNameBlobHandle handle)
		{
			return this.BlobHeap.GetDocumentName(handle);
		}

		/// <param name="handle" />
		// Token: 0x0600065B RID: 1627 RVA: 0x0000F40B File Offset: 0x0000D60B
		public Document GetDocument(DocumentHandle handle)
		{
			return new Document(this, handle);
		}

		/// <param name="handle" />
		// Token: 0x0600065C RID: 1628 RVA: 0x0000F414 File Offset: 0x0000D614
		public MethodDebugInformation GetMethodDebugInformation(MethodDebugInformationHandle handle)
		{
			return new MethodDebugInformation(this, handle);
		}

		/// <param name="handle" />
		// Token: 0x0600065D RID: 1629 RVA: 0x0000F41D File Offset: 0x0000D61D
		public MethodDebugInformation GetMethodDebugInformation(MethodDefinitionHandle handle)
		{
			return new MethodDebugInformation(this, MethodDebugInformationHandle.FromRowId(handle.RowId));
		}

		/// <param name="handle" />
		// Token: 0x0600065E RID: 1630 RVA: 0x0000F431 File Offset: 0x0000D631
		public LocalScope GetLocalScope(LocalScopeHandle handle)
		{
			return new LocalScope(this, handle);
		}

		/// <param name="handle" />
		// Token: 0x0600065F RID: 1631 RVA: 0x0000F43A File Offset: 0x0000D63A
		public LocalVariable GetLocalVariable(LocalVariableHandle handle)
		{
			return new LocalVariable(this, handle);
		}

		/// <param name="handle" />
		// Token: 0x06000660 RID: 1632 RVA: 0x0000F443 File Offset: 0x0000D643
		public LocalConstant GetLocalConstant(LocalConstantHandle handle)
		{
			return new LocalConstant(this, handle);
		}

		/// <param name="handle" />
		// Token: 0x06000661 RID: 1633 RVA: 0x0000F44C File Offset: 0x0000D64C
		public ImportScope GetImportScope(ImportScopeHandle handle)
		{
			return new ImportScope(this, handle);
		}

		/// <param name="handle" />
		// Token: 0x06000662 RID: 1634 RVA: 0x0000F455 File Offset: 0x0000D655
		public CustomDebugInformation GetCustomDebugInformation(CustomDebugInformationHandle handle)
		{
			return new CustomDebugInformation(this, handle);
		}

		/// <param name="handle" />
		// Token: 0x06000663 RID: 1635 RVA: 0x0000F45E File Offset: 0x0000D65E
		public CustomDebugInformationHandleCollection GetCustomDebugInformation(EntityHandle handle)
		{
			return new CustomDebugInformationHandleCollection(this, handle);
		}

		/// <param name="handle" />
		// Token: 0x06000664 RID: 1636 RVA: 0x0000F467 File Offset: 0x0000D667
		public LocalScopeHandleCollection GetLocalScopes(MethodDefinitionHandle handle)
		{
			return new LocalScopeHandleCollection(this, handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x06000665 RID: 1637 RVA: 0x0000F476 File Offset: 0x0000D676
		public LocalScopeHandleCollection GetLocalScopes(MethodDebugInformationHandle handle)
		{
			return new LocalScopeHandleCollection(this, handle.RowId);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0000F488 File Offset: 0x0000D688
		private void InitializeNestedTypesMap()
		{
			Dictionary<TypeDefinitionHandle, ImmutableArray<TypeDefinitionHandle>.Builder> dictionary = new Dictionary<TypeDefinitionHandle, ImmutableArray<TypeDefinitionHandle>.Builder>();
			int numberOfRows = this.NestedClassTable.NumberOfRows;
			ImmutableArray<TypeDefinitionHandle>.Builder builder = null;
			TypeDefinitionHandle typeDefinitionHandle = default(TypeDefinitionHandle);
			for (int i = 1; i <= numberOfRows; i++)
			{
				TypeDefinitionHandle enclosingClass = this.NestedClassTable.GetEnclosingClass(i);
				if (enclosingClass != typeDefinitionHandle)
				{
					if (!dictionary.TryGetValue(enclosingClass, ref builder))
					{
						builder = ImmutableArray.CreateBuilder<TypeDefinitionHandle>();
						dictionary.Add(enclosingClass, builder);
					}
					typeDefinitionHandle = enclosingClass;
				}
				builder.Add(this.NestedClassTable.GetNestedClass(i));
			}
			Dictionary<TypeDefinitionHandle, ImmutableArray<TypeDefinitionHandle>> dictionary2 = new Dictionary<TypeDefinitionHandle, ImmutableArray<TypeDefinitionHandle>>();
			foreach (KeyValuePair<TypeDefinitionHandle, ImmutableArray<TypeDefinitionHandle>.Builder> keyValuePair in dictionary)
			{
				dictionary2.Add(keyValuePair.Key, keyValuePair.Value.ToImmutable());
			}
			this._lazyNestedTypesMap = dictionary2;
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0000F570 File Offset: 0x0000D770
		[NullableContext(0)]
		internal ImmutableArray<TypeDefinitionHandle> GetNestedTypes(TypeDefinitionHandle typeDef)
		{
			if (this._lazyNestedTypesMap == null)
			{
				this.InitializeNestedTypesMap();
			}
			ImmutableArray<TypeDefinitionHandle> immutableArray;
			if (this._lazyNestedTypesMap.TryGetValue(typeDef, ref immutableArray))
			{
				return immutableArray;
			}
			return ImmutableArray<TypeDefinitionHandle>.Empty;
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0000F5A4 File Offset: 0x0000D7A4
		private TypeDefTreatment GetWellKnownTypeDefinitionTreatment(TypeDefinitionHandle typeDef)
		{
			MetadataReader.InitializeProjectedTypes();
			StringHandle name = this.TypeDefTable.GetName(typeDef);
			int num = this.StringHeap.BinarySearchRaw(MetadataReader.s_projectedTypeNames, name);
			if (num < 0)
			{
				return TypeDefTreatment.None;
			}
			StringHandle @namespace = this.TypeDefTable.GetNamespace(typeDef);
			if (this.StringHeap.EqualsRaw(@namespace, StringHeap.GetVirtualString(MetadataReader.s_projectionInfos[num].ClrNamespace)))
			{
				return MetadataReader.s_projectionInfos[num].Treatment;
			}
			if (this.StringHeap.EqualsRaw(@namespace, MetadataReader.s_projectionInfos[num].WinRTNamespace))
			{
				return MetadataReader.s_projectionInfos[num].Treatment | TypeDefTreatment.MarkInternalFlag;
			}
			return TypeDefTreatment.None;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0000F650 File Offset: 0x0000D850
		private int GetProjectionIndexForTypeReference(TypeReferenceHandle typeRef, out bool isIDisposable)
		{
			MetadataReader.InitializeProjectedTypes();
			int num = this.StringHeap.BinarySearchRaw(MetadataReader.s_projectedTypeNames, this.TypeRefTable.GetName(typeRef));
			if (num >= 0 && this.StringHeap.EqualsRaw(this.TypeRefTable.GetNamespace(typeRef), MetadataReader.s_projectionInfos[num].WinRTNamespace))
			{
				isIDisposable = MetadataReader.s_projectionInfos[num].IsIDisposable;
				return num;
			}
			isIDisposable = false;
			return -1;
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0000F6C4 File Offset: 0x0000D8C4
		internal static AssemblyReferenceHandle GetProjectedAssemblyRef(int projectionIndex)
		{
			return AssemblyReferenceHandle.FromVirtualIndex(MetadataReader.s_projectionInfos[projectionIndex].AssemblyRef);
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0000F6DB File Offset: 0x0000D8DB
		internal static StringHandle GetProjectedName(int projectionIndex)
		{
			return StringHandle.FromVirtualIndex(MetadataReader.s_projectionInfos[projectionIndex].ClrName);
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0000F6F2 File Offset: 0x0000D8F2
		internal static StringHandle GetProjectedNamespace(int projectionIndex)
		{
			return StringHandle.FromVirtualIndex(MetadataReader.s_projectionInfos[projectionIndex].ClrNamespace);
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0000F709 File Offset: 0x0000D909
		internal static TypeRefSignatureTreatment GetProjectedSignatureTreatment(int projectionIndex)
		{
			return MetadataReader.s_projectionInfos[projectionIndex].SignatureTreatment;
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0000F71C File Offset: 0x0000D91C
		private static void InitializeProjectedTypes()
		{
			if (MetadataReader.s_projectedTypeNames == null || MetadataReader.s_projectionInfos == null)
			{
				AssemblyReferenceHandle.VirtualIndex virtualIndex = AssemblyReferenceHandle.VirtualIndex.System_Runtime_WindowsRuntime;
				AssemblyReferenceHandle.VirtualIndex virtualIndex2 = AssemblyReferenceHandle.VirtualIndex.System_Runtime;
				AssemblyReferenceHandle.VirtualIndex virtualIndex3 = AssemblyReferenceHandle.VirtualIndex.System_ObjectModel;
				AssemblyReferenceHandle.VirtualIndex virtualIndex4 = AssemblyReferenceHandle.VirtualIndex.System_Runtime_WindowsRuntime_UI_Xaml;
				AssemblyReferenceHandle.VirtualIndex virtualIndex5 = AssemblyReferenceHandle.VirtualIndex.System_Runtime_InteropServices_WindowsRuntime;
				AssemblyReferenceHandle.VirtualIndex virtualIndex6 = AssemblyReferenceHandle.VirtualIndex.System_Numerics_Vectors;
				string[] array = new string[50];
				MetadataReader.ProjectionInfo[] array2 = new MetadataReader.ProjectionInfo[50];
				int num = 0;
				int num2 = 0;
				array[num++] = "AttributeTargets";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation.Metadata", StringHandle.VirtualIndex.System, StringHandle.VirtualIndex.AttributeTargets, virtualIndex2, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "AttributeUsageAttribute";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation.Metadata", StringHandle.VirtualIndex.System, StringHandle.VirtualIndex.AttributeUsageAttribute, virtualIndex2, TypeDefTreatment.RedirectedToClrAttribute, TypeRefSignatureTreatment.None, false);
				array[num++] = "Color";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI", StringHandle.VirtualIndex.Windows_UI, StringHandle.VirtualIndex.Color, virtualIndex, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "CornerRadius";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml", StringHandle.VirtualIndex.Windows_UI_Xaml, StringHandle.VirtualIndex.CornerRadius, virtualIndex4, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "DateTime";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation", StringHandle.VirtualIndex.System, StringHandle.VirtualIndex.DateTimeOffset, virtualIndex2, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "Duration";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml", StringHandle.VirtualIndex.Windows_UI_Xaml, StringHandle.VirtualIndex.Duration, virtualIndex4, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "DurationType";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml", StringHandle.VirtualIndex.Windows_UI_Xaml, StringHandle.VirtualIndex.DurationType, virtualIndex4, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "EventHandler`1";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation", StringHandle.VirtualIndex.System, StringHandle.VirtualIndex.EventHandler1, virtualIndex2, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "EventRegistrationToken";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation", StringHandle.VirtualIndex.System_Runtime_InteropServices_WindowsRuntime, StringHandle.VirtualIndex.EventRegistrationToken, virtualIndex5, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "GeneratorPosition";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml.Controls.Primitives", StringHandle.VirtualIndex.Windows_UI_Xaml_Controls_Primitives, StringHandle.VirtualIndex.GeneratorPosition, virtualIndex4, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "GridLength";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml", StringHandle.VirtualIndex.Windows_UI_Xaml, StringHandle.VirtualIndex.GridLength, virtualIndex4, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "GridUnitType";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml", StringHandle.VirtualIndex.Windows_UI_Xaml, StringHandle.VirtualIndex.GridUnitType, virtualIndex4, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "HResult";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation", StringHandle.VirtualIndex.System, StringHandle.VirtualIndex.Exception, virtualIndex2, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.ProjectedToClass, false);
				array[num++] = "IBindableIterable";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml.Interop", StringHandle.VirtualIndex.System_Collections, StringHandle.VirtualIndex.IEnumerable, virtualIndex2, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "IBindableVector";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml.Interop", StringHandle.VirtualIndex.System_Collections, StringHandle.VirtualIndex.IList, virtualIndex2, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "IClosable";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation", StringHandle.VirtualIndex.System, StringHandle.VirtualIndex.IDisposable, virtualIndex2, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, true);
				array[num++] = "ICommand";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml.Input", StringHandle.VirtualIndex.System_Windows_Input, StringHandle.VirtualIndex.ICommand, virtualIndex3, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "IIterable`1";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation.Collections", StringHandle.VirtualIndex.System_Collections_Generic, StringHandle.VirtualIndex.IEnumerable1, virtualIndex2, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "IKeyValuePair`2";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation.Collections", StringHandle.VirtualIndex.System_Collections_Generic, StringHandle.VirtualIndex.KeyValuePair2, virtualIndex2, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.ProjectedToValueType, false);
				array[num++] = "IMapView`2";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation.Collections", StringHandle.VirtualIndex.System_Collections_Generic, StringHandle.VirtualIndex.IReadOnlyDictionary2, virtualIndex2, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "IMap`2";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation.Collections", StringHandle.VirtualIndex.System_Collections_Generic, StringHandle.VirtualIndex.IDictionary2, virtualIndex2, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "INotifyCollectionChanged";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml.Interop", StringHandle.VirtualIndex.System_Collections_Specialized, StringHandle.VirtualIndex.INotifyCollectionChanged, virtualIndex3, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "INotifyPropertyChanged";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml.Data", StringHandle.VirtualIndex.System_ComponentModel, StringHandle.VirtualIndex.INotifyPropertyChanged, virtualIndex3, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "IReference`1";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation", StringHandle.VirtualIndex.System, StringHandle.VirtualIndex.Nullable1, virtualIndex2, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.ProjectedToValueType, false);
				array[num++] = "IVectorView`1";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation.Collections", StringHandle.VirtualIndex.System_Collections_Generic, StringHandle.VirtualIndex.IReadOnlyList1, virtualIndex2, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "IVector`1";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation.Collections", StringHandle.VirtualIndex.System_Collections_Generic, StringHandle.VirtualIndex.IList1, virtualIndex2, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "KeyTime";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml.Media.Animation", StringHandle.VirtualIndex.Windows_UI_Xaml_Media_Animation, StringHandle.VirtualIndex.KeyTime, virtualIndex4, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "Matrix";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml.Media", StringHandle.VirtualIndex.Windows_UI_Xaml_Media, StringHandle.VirtualIndex.Matrix, virtualIndex4, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "Matrix3D";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml.Media.Media3D", StringHandle.VirtualIndex.Windows_UI_Xaml_Media_Media3D, StringHandle.VirtualIndex.Matrix3D, virtualIndex4, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "Matrix3x2";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation.Numerics", StringHandle.VirtualIndex.System_Numerics, StringHandle.VirtualIndex.Matrix3x2, virtualIndex6, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "Matrix4x4";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation.Numerics", StringHandle.VirtualIndex.System_Numerics, StringHandle.VirtualIndex.Matrix4x4, virtualIndex6, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "NotifyCollectionChangedAction";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml.Interop", StringHandle.VirtualIndex.System_Collections_Specialized, StringHandle.VirtualIndex.NotifyCollectionChangedAction, virtualIndex3, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "NotifyCollectionChangedEventArgs";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml.Interop", StringHandle.VirtualIndex.System_Collections_Specialized, StringHandle.VirtualIndex.NotifyCollectionChangedEventArgs, virtualIndex3, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "NotifyCollectionChangedEventHandler";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml.Interop", StringHandle.VirtualIndex.System_Collections_Specialized, StringHandle.VirtualIndex.NotifyCollectionChangedEventHandler, virtualIndex3, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "Plane";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation.Numerics", StringHandle.VirtualIndex.System_Numerics, StringHandle.VirtualIndex.Plane, virtualIndex6, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "Point";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation", StringHandle.VirtualIndex.Windows_Foundation, StringHandle.VirtualIndex.Point, virtualIndex, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "PropertyChangedEventArgs";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml.Data", StringHandle.VirtualIndex.System_ComponentModel, StringHandle.VirtualIndex.PropertyChangedEventArgs, virtualIndex3, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "PropertyChangedEventHandler";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml.Data", StringHandle.VirtualIndex.System_ComponentModel, StringHandle.VirtualIndex.PropertyChangedEventHandler, virtualIndex3, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "Quaternion";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation.Numerics", StringHandle.VirtualIndex.System_Numerics, StringHandle.VirtualIndex.Quaternion, virtualIndex6, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "Rect";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation", StringHandle.VirtualIndex.Windows_Foundation, StringHandle.VirtualIndex.Rect, virtualIndex, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "RepeatBehavior";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml.Media.Animation", StringHandle.VirtualIndex.Windows_UI_Xaml_Media_Animation, StringHandle.VirtualIndex.RepeatBehavior, virtualIndex4, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "RepeatBehaviorType";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml.Media.Animation", StringHandle.VirtualIndex.Windows_UI_Xaml_Media_Animation, StringHandle.VirtualIndex.RepeatBehaviorType, virtualIndex4, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "Size";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation", StringHandle.VirtualIndex.Windows_Foundation, StringHandle.VirtualIndex.Size, virtualIndex, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "Thickness";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml", StringHandle.VirtualIndex.Windows_UI_Xaml, StringHandle.VirtualIndex.Thickness, virtualIndex4, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "TimeSpan";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation", StringHandle.VirtualIndex.System, StringHandle.VirtualIndex.TimeSpan, virtualIndex2, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "TypeName";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.UI.Xaml.Interop", StringHandle.VirtualIndex.System, StringHandle.VirtualIndex.Type, virtualIndex2, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.ProjectedToClass, false);
				array[num++] = "Uri";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation", StringHandle.VirtualIndex.System, StringHandle.VirtualIndex.Uri, virtualIndex2, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "Vector2";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation.Numerics", StringHandle.VirtualIndex.System_Numerics, StringHandle.VirtualIndex.Vector2, virtualIndex6, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "Vector3";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation.Numerics", StringHandle.VirtualIndex.System_Numerics, StringHandle.VirtualIndex.Vector3, virtualIndex6, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				array[num++] = "Vector4";
				array2[num2++] = new MetadataReader.ProjectionInfo("Windows.Foundation.Numerics", StringHandle.VirtualIndex.System_Numerics, StringHandle.VirtualIndex.Vector4, virtualIndex6, TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment.None, false);
				MetadataReader.s_projectedTypeNames = array;
				MetadataReader.s_projectionInfos = array2;
			}
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x000100A4 File Offset: 0x0000E2A4
		[Conditional("DEBUG")]
		private static void AssertSorted(string[] keys)
		{
			for (int i = 0; i < keys.Length - 1; i++)
			{
			}
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x000100C1 File Offset: 0x0000E2C1
		internal static string[] GetProjectedTypeNames()
		{
			MetadataReader.InitializeProjectedTypes();
			return MetadataReader.s_projectedTypeNames;
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x000100CD File Offset: 0x0000E2CD
		private static uint TreatmentAndRowId(byte treatment, int rowId)
		{
			return (uint)(((int)treatment << 24) | rowId);
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x000100D8 File Offset: 0x0000E2D8
		[MethodImpl(8)]
		internal uint CalculateTypeDefTreatmentAndRowId(TypeDefinitionHandle handle)
		{
			TypeAttributes flags = this.TypeDefTable.GetFlags(handle);
			EntityHandle extends = this.TypeDefTable.GetExtends(handle);
			TypeDefTreatment typeDefTreatment;
			if ((flags & 16384) != null)
			{
				if (this._metadataKind == MetadataKind.WindowsMetadata)
				{
					typeDefTreatment = this.GetWellKnownTypeDefinitionTreatment(handle);
					if (typeDefTreatment != TypeDefTreatment.None)
					{
						return MetadataReader.TreatmentAndRowId((byte)typeDefTreatment, handle.RowId);
					}
					if (extends.Kind == HandleKind.TypeReference && this.IsSystemAttribute((TypeReferenceHandle)extends))
					{
						typeDefTreatment = TypeDefTreatment.NormalAttribute;
					}
					else
					{
						typeDefTreatment = TypeDefTreatment.NormalNonAttribute;
					}
				}
				else if (this._metadataKind == MetadataKind.ManagedWindowsMetadata && this.NeedsWinRTPrefix(flags, extends))
				{
					typeDefTreatment = TypeDefTreatment.PrefixWinRTName;
				}
				else
				{
					typeDefTreatment = TypeDefTreatment.None;
				}
				if ((typeDefTreatment == TypeDefTreatment.PrefixWinRTName || typeDefTreatment == TypeDefTreatment.NormalNonAttribute) && (flags & 32) == null && this.HasAttribute(handle, "Windows.UI.Xaml", "TreatAsAbstractComposableClassAttribute"))
				{
					typeDefTreatment |= TypeDefTreatment.MarkAbstractFlag;
				}
			}
			else if (this._metadataKind == MetadataKind.ManagedWindowsMetadata && this.IsClrImplementationType(handle))
			{
				typeDefTreatment = TypeDefTreatment.UnmangleWinRTName;
			}
			else
			{
				typeDefTreatment = TypeDefTreatment.None;
			}
			return MetadataReader.TreatmentAndRowId((byte)typeDefTreatment, handle.RowId);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x000101B8 File Offset: 0x0000E3B8
		private bool IsClrImplementationType(TypeDefinitionHandle typeDef)
		{
			TypeAttributes flags = this.TypeDefTable.GetFlags(typeDef);
			return (flags & 1031) == 1024 && this.StringHeap.StartsWithRaw(this.TypeDefTable.GetName(typeDef), "<CLR>");
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00010200 File Offset: 0x0000E400
		internal uint CalculateTypeRefTreatmentAndRowId(TypeReferenceHandle handle)
		{
			bool flag;
			int projectionIndexForTypeReference = this.GetProjectionIndexForTypeReference(handle, out flag);
			if (projectionIndexForTypeReference >= 0)
			{
				return MetadataReader.TreatmentAndRowId(3, projectionIndexForTypeReference);
			}
			return MetadataReader.TreatmentAndRowId((byte)this.GetSpecialTypeRefTreatment(handle), handle.RowId);
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00010238 File Offset: 0x0000E438
		private TypeRefTreatment GetSpecialTypeRefTreatment(TypeReferenceHandle handle)
		{
			if (this.StringHeap.EqualsRaw(this.TypeRefTable.GetNamespace(handle), "System"))
			{
				StringHandle name = this.TypeRefTable.GetName(handle);
				if (this.StringHeap.EqualsRaw(name, "MulticastDelegate"))
				{
					return TypeRefTreatment.SystemDelegate;
				}
				if (this.StringHeap.EqualsRaw(name, "Attribute"))
				{
					return TypeRefTreatment.SystemAttribute;
				}
			}
			return TypeRefTreatment.None;
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0001029B File Offset: 0x0000E49B
		private bool IsSystemAttribute(TypeReferenceHandle handle)
		{
			return this.StringHeap.EqualsRaw(this.TypeRefTable.GetNamespace(handle), "System") && this.StringHeap.EqualsRaw(this.TypeRefTable.GetName(handle), "Attribute");
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x000102DC File Offset: 0x0000E4DC
		private bool NeedsWinRTPrefix(TypeAttributes flags, EntityHandle extends)
		{
			if ((flags & 39) != 1)
			{
				return false;
			}
			if (extends.Kind != HandleKind.TypeReference)
			{
				return false;
			}
			TypeReferenceHandle typeReferenceHandle = (TypeReferenceHandle)extends;
			if (this.StringHeap.EqualsRaw(this.TypeRefTable.GetNamespace(typeReferenceHandle), "System"))
			{
				StringHandle name = this.TypeRefTable.GetName(typeReferenceHandle);
				if (this.StringHeap.EqualsRaw(name, "MulticastDelegate") || this.StringHeap.EqualsRaw(name, "ValueType") || this.StringHeap.EqualsRaw(name, "Attribute"))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0001036C File Offset: 0x0000E56C
		private uint CalculateMethodDefTreatmentAndRowId(MethodDefinitionHandle methodDef)
		{
			MethodDefTreatment methodDefTreatment = MethodDefTreatment.Implementation;
			TypeDefinitionHandle declaringType = this.GetDeclaringType(methodDef);
			TypeAttributes flags = this.TypeDefTable.GetFlags(declaringType);
			if ((flags & 16384) != null)
			{
				if (this.IsClrImplementationType(declaringType))
				{
					methodDefTreatment = MethodDefTreatment.Implementation;
				}
				else if (flags.IsNested())
				{
					methodDefTreatment = MethodDefTreatment.Implementation;
				}
				else if ((flags & 32) != null)
				{
					methodDefTreatment = MethodDefTreatment.InterfaceMethod;
				}
				else if (this._metadataKind == MetadataKind.ManagedWindowsMetadata && (flags & 1) == null)
				{
					methodDefTreatment = MethodDefTreatment.Implementation;
				}
				else
				{
					methodDefTreatment = MethodDefTreatment.Other;
					EntityHandle extends = this.TypeDefTable.GetExtends(declaringType);
					if (extends.Kind == HandleKind.TypeReference)
					{
						TypeRefTreatment specialTypeRefTreatment = this.GetSpecialTypeRefTreatment((TypeReferenceHandle)extends);
						if (specialTypeRefTreatment != TypeRefTreatment.SystemDelegate)
						{
							if (specialTypeRefTreatment == TypeRefTreatment.SystemAttribute)
							{
								methodDefTreatment = MethodDefTreatment.AttributeMethod;
							}
						}
						else
						{
							methodDefTreatment = MethodDefTreatment.DelegateMethod | MethodDefTreatment.MarkPublicFlag;
						}
					}
				}
			}
			if (methodDefTreatment == MethodDefTreatment.Other)
			{
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				foreach (MethodImplementationHandle methodImplementationHandle in new MethodImplementationHandleCollection(this, declaringType))
				{
					MethodImplementation methodImplementation = this.GetMethodImplementation(methodImplementationHandle);
					if (methodImplementation.MethodBody == methodDef)
					{
						EntityHandle methodDeclaration = methodImplementation.MethodDeclaration;
						if (methodDeclaration.Kind == HandleKind.MemberReference && this.ImplementsRedirectedInterface((MemberReferenceHandle)methodDeclaration, out flag3))
						{
							flag = true;
							if (flag3)
							{
								break;
							}
						}
						else
						{
							flag2 = true;
						}
					}
				}
				if (flag3)
				{
					methodDefTreatment = MethodDefTreatment.DisposeMethod;
				}
				else if (flag && !flag2)
				{
					methodDefTreatment = MethodDefTreatment.HiddenInterfaceImplementation;
				}
			}
			if (methodDefTreatment == MethodDefTreatment.Other)
			{
				methodDefTreatment |= this.GetMethodTreatmentFromCustomAttributes(methodDef);
			}
			return MetadataReader.TreatmentAndRowId((byte)methodDefTreatment, methodDef.RowId);
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x000104D8 File Offset: 0x0000E6D8
		private MethodDefTreatment GetMethodTreatmentFromCustomAttributes(MethodDefinitionHandle methodDef)
		{
			MethodDefTreatment methodDefTreatment = MethodDefTreatment.None;
			foreach (CustomAttributeHandle customAttributeHandle in this.GetCustomAttributes(methodDef))
			{
				StringHandle stringHandle;
				StringHandle stringHandle2;
				if (this.GetAttributeTypeNameRaw(customAttributeHandle, out stringHandle, out stringHandle2) && this.StringHeap.EqualsRaw(stringHandle, "Windows.UI.Xaml"))
				{
					if (this.StringHeap.EqualsRaw(stringHandle2, "TreatAsPublicMethodAttribute"))
					{
						methodDefTreatment |= MethodDefTreatment.MarkPublicFlag;
					}
					if (this.StringHeap.EqualsRaw(stringHandle2, "TreatAsAbstractMethodAttribute"))
					{
						methodDefTreatment |= MethodDefTreatment.MarkAbstractFlag;
					}
				}
			}
			return methodDefTreatment;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00010584 File Offset: 0x0000E784
		private uint CalculateFieldDefTreatmentAndRowId(FieldDefinitionHandle handle)
		{
			FieldAttributes flags = this.FieldTable.GetFlags(handle);
			FieldDefTreatment fieldDefTreatment = FieldDefTreatment.None;
			if ((flags & 1024) != null && this.StringHeap.EqualsRaw(this.FieldTable.GetName(handle), "value__"))
			{
				TypeDefinitionHandle declaringType = this.GetDeclaringType(handle);
				EntityHandle extends = this.TypeDefTable.GetExtends(declaringType);
				if (extends.Kind == HandleKind.TypeReference)
				{
					TypeReferenceHandle typeReferenceHandle = (TypeReferenceHandle)extends;
					if (this.StringHeap.EqualsRaw(this.TypeRefTable.GetName(typeReferenceHandle), "Enum") && this.StringHeap.EqualsRaw(this.TypeRefTable.GetNamespace(typeReferenceHandle), "System"))
					{
						fieldDefTreatment = FieldDefTreatment.EnumValue;
					}
				}
			}
			return MetadataReader.TreatmentAndRowId((byte)fieldDefTreatment, handle.RowId);
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00010640 File Offset: 0x0000E840
		private uint CalculateMemberRefTreatmentAndRowId(MemberReferenceHandle handle)
		{
			bool flag;
			MemberRefTreatment memberRefTreatment;
			if (this.ImplementsRedirectedInterface(handle, out flag) && flag)
			{
				memberRefTreatment = MemberRefTreatment.Dispose;
			}
			else
			{
				memberRefTreatment = MemberRefTreatment.None;
			}
			return MetadataReader.TreatmentAndRowId((byte)memberRefTreatment, handle.RowId);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00010670 File Offset: 0x0000E870
		private bool ImplementsRedirectedInterface(MemberReferenceHandle memberRef, out bool isIDisposable)
		{
			isIDisposable = false;
			EntityHandle @class = this.MemberRefTable.GetClass(memberRef);
			TypeReferenceHandle typeReferenceHandle;
			if (@class.Kind == HandleKind.TypeReference)
			{
				typeReferenceHandle = (TypeReferenceHandle)@class;
			}
			else
			{
				if (@class.Kind != HandleKind.TypeSpecification)
				{
					return false;
				}
				BlobHandle signature = this.TypeSpecTable.GetSignature((TypeSpecificationHandle)@class);
				BlobReader blobReader = new BlobReader(this.BlobHeap.GetMemoryBlock(signature));
				if (blobReader.Length < 2 || blobReader.ReadByte() != 21 || blobReader.ReadByte() != 18)
				{
					return false;
				}
				EntityHandle entityHandle = blobReader.ReadTypeHandle();
				if (entityHandle.Kind != HandleKind.TypeReference)
				{
					return false;
				}
				typeReferenceHandle = (TypeReferenceHandle)entityHandle;
			}
			return this.GetProjectionIndexForTypeReference(typeReferenceHandle, out isIDisposable) >= 0;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00010724 File Offset: 0x0000E924
		private int FindMscorlibAssemblyRefNoProjection()
		{
			for (int i = 1; i <= this.AssemblyRefTable.NumberOfNonVirtualRows; i++)
			{
				if (this.StringHeap.EqualsRaw(this.AssemblyRefTable.GetName(i), "mscorlib"))
				{
					return i;
				}
			}
			throw new BadImageFormatException(SR.WinMDMissingMscorlibRef);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00010774 File Offset: 0x0000E974
		internal CustomAttributeValueTreatment CalculateCustomAttributeValueTreatment(CustomAttributeHandle handle)
		{
			EntityHandle parent = this.CustomAttributeTable.GetParent(handle);
			if (!this.IsWindowsAttributeUsageAttribute(parent, handle))
			{
				return CustomAttributeValueTreatment.None;
			}
			TypeDefinitionHandle typeDefinitionHandle = (TypeDefinitionHandle)parent;
			if (this.StringHeap.EqualsRaw(this.TypeDefTable.GetNamespace(typeDefinitionHandle), "Windows.Foundation.Metadata"))
			{
				if (this.StringHeap.EqualsRaw(this.TypeDefTable.GetName(typeDefinitionHandle), "VersionAttribute"))
				{
					return CustomAttributeValueTreatment.AttributeUsageVersionAttribute;
				}
				if (this.StringHeap.EqualsRaw(this.TypeDefTable.GetName(typeDefinitionHandle), "DeprecatedAttribute"))
				{
					return CustomAttributeValueTreatment.AttributeUsageDeprecatedAttribute;
				}
			}
			if (!this.HasAttribute(typeDefinitionHandle, "Windows.Foundation.Metadata", "AllowMultipleAttribute"))
			{
				return CustomAttributeValueTreatment.AttributeUsageAllowSingle;
			}
			return CustomAttributeValueTreatment.AttributeUsageAllowMultiple;
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001081C File Offset: 0x0000EA1C
		private bool IsWindowsAttributeUsageAttribute(EntityHandle targetType, CustomAttributeHandle attributeHandle)
		{
			if (targetType.Kind != HandleKind.TypeDefinition)
			{
				return false;
			}
			EntityHandle constructor = this.CustomAttributeTable.GetConstructor(attributeHandle);
			if (constructor.Kind != HandleKind.MemberReference)
			{
				return false;
			}
			EntityHandle @class = this.MemberRefTable.GetClass((MemberReferenceHandle)constructor);
			if (@class.Kind != HandleKind.TypeReference)
			{
				return false;
			}
			TypeReferenceHandle typeReferenceHandle = (TypeReferenceHandle)@class;
			return this.StringHeap.EqualsRaw(this.TypeRefTable.GetName(typeReferenceHandle), "AttributeUsageAttribute") && this.StringHeap.EqualsRaw(this.TypeRefTable.GetNamespace(typeReferenceHandle), "Windows.Foundation.Metadata");
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x000108B0 File Offset: 0x0000EAB0
		private bool HasAttribute(EntityHandle token, string asciiNamespaceName, string asciiTypeName)
		{
			foreach (CustomAttributeHandle customAttributeHandle in this.GetCustomAttributes(token))
			{
				StringHandle stringHandle;
				StringHandle stringHandle2;
				if (this.GetAttributeTypeNameRaw(customAttributeHandle, out stringHandle, out stringHandle2) && this.StringHeap.EqualsRaw(stringHandle2, asciiTypeName) && this.StringHeap.EqualsRaw(stringHandle, asciiNamespaceName))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00010938 File Offset: 0x0000EB38
		private bool GetAttributeTypeNameRaw(CustomAttributeHandle caHandle, out StringHandle namespaceName, out StringHandle typeName)
		{
			namespaceName = (typeName = default(StringHandle));
			EntityHandle attributeTypeRaw = this.GetAttributeTypeRaw(caHandle);
			if (attributeTypeRaw.IsNil)
			{
				return false;
			}
			if (attributeTypeRaw.Kind == HandleKind.TypeReference)
			{
				TypeReferenceHandle typeReferenceHandle = (TypeReferenceHandle)attributeTypeRaw;
				EntityHandle resolutionScope = this.TypeRefTable.GetResolutionScope(typeReferenceHandle);
				if (!resolutionScope.IsNil && resolutionScope.Kind == HandleKind.TypeReference)
				{
					return false;
				}
				typeName = this.TypeRefTable.GetName(typeReferenceHandle);
				namespaceName = this.TypeRefTable.GetNamespace(typeReferenceHandle);
			}
			else
			{
				if (attributeTypeRaw.Kind != HandleKind.TypeDefinition)
				{
					return false;
				}
				TypeDefinitionHandle typeDefinitionHandle = (TypeDefinitionHandle)attributeTypeRaw;
				if (this.TypeDefTable.GetFlags(typeDefinitionHandle).IsNested())
				{
					return false;
				}
				typeName = this.TypeDefTable.GetName(typeDefinitionHandle);
				namespaceName = this.TypeDefTable.GetNamespace(typeDefinitionHandle);
			}
			return true;
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00010A1C File Offset: 0x0000EC1C
		private EntityHandle GetAttributeTypeRaw(CustomAttributeHandle handle)
		{
			EntityHandle constructor = this.CustomAttributeTable.GetConstructor(handle);
			if (constructor.Kind == HandleKind.MethodDefinition)
			{
				return this.GetDeclaringType((MethodDefinitionHandle)constructor);
			}
			if (constructor.Kind == HandleKind.MemberReference)
			{
				EntityHandle @class = this.MemberRefTable.GetClass((MemberReferenceHandle)constructor);
				HandleKind kind = @class.Kind;
				if (kind == HandleKind.TypeReference || kind == HandleKind.TypeDefinition)
				{
					return @class;
				}
			}
			return default(EntityHandle);
		}

		// Token: 0x040003C9 RID: 969
		internal readonly NamespaceCache NamespaceCache;

		// Token: 0x040003CA RID: 970
		internal readonly MemoryBlock Block;

		// Token: 0x040003CB RID: 971
		internal readonly int WinMDMscorlibRef;

		// Token: 0x040003CC RID: 972
		private readonly object _memoryOwnerObj;

		// Token: 0x040003CD RID: 973
		private readonly MetadataReaderOptions _options;

		// Token: 0x040003CE RID: 974
		private Dictionary<TypeDefinitionHandle, ImmutableArray<TypeDefinitionHandle>> _lazyNestedTypesMap;

		// Token: 0x040003CF RID: 975
		private readonly string _versionString;

		// Token: 0x040003D0 RID: 976
		private readonly MetadataKind _metadataKind;

		// Token: 0x040003D1 RID: 977
		private readonly MetadataStreamKind _metadataStreamKind;

		// Token: 0x040003D2 RID: 978
		private readonly DebugMetadataHeader _debugMetadataHeader;

		// Token: 0x040003D3 RID: 979
		internal StringHeap StringHeap;

		// Token: 0x040003D4 RID: 980
		internal BlobHeap BlobHeap;

		// Token: 0x040003D5 RID: 981
		internal GuidHeap GuidHeap;

		// Token: 0x040003D6 RID: 982
		internal UserStringHeap UserStringHeap;

		// Token: 0x040003D7 RID: 983
		internal bool IsMinimalDelta;

		// Token: 0x040003D8 RID: 984
		private readonly TableMask _sortedTables;

		// Token: 0x040003D9 RID: 985
		internal int[] TableRowCounts;

		// Token: 0x040003DA RID: 986
		internal ModuleTableReader ModuleTable;

		// Token: 0x040003DB RID: 987
		internal TypeRefTableReader TypeRefTable;

		// Token: 0x040003DC RID: 988
		internal TypeDefTableReader TypeDefTable;

		// Token: 0x040003DD RID: 989
		internal FieldPtrTableReader FieldPtrTable;

		// Token: 0x040003DE RID: 990
		internal FieldTableReader FieldTable;

		// Token: 0x040003DF RID: 991
		internal MethodPtrTableReader MethodPtrTable;

		// Token: 0x040003E0 RID: 992
		internal MethodTableReader MethodDefTable;

		// Token: 0x040003E1 RID: 993
		internal ParamPtrTableReader ParamPtrTable;

		// Token: 0x040003E2 RID: 994
		internal ParamTableReader ParamTable;

		// Token: 0x040003E3 RID: 995
		internal InterfaceImplTableReader InterfaceImplTable;

		// Token: 0x040003E4 RID: 996
		internal MemberRefTableReader MemberRefTable;

		// Token: 0x040003E5 RID: 997
		internal ConstantTableReader ConstantTable;

		// Token: 0x040003E6 RID: 998
		internal CustomAttributeTableReader CustomAttributeTable;

		// Token: 0x040003E7 RID: 999
		internal FieldMarshalTableReader FieldMarshalTable;

		// Token: 0x040003E8 RID: 1000
		internal DeclSecurityTableReader DeclSecurityTable;

		// Token: 0x040003E9 RID: 1001
		internal ClassLayoutTableReader ClassLayoutTable;

		// Token: 0x040003EA RID: 1002
		internal FieldLayoutTableReader FieldLayoutTable;

		// Token: 0x040003EB RID: 1003
		internal StandAloneSigTableReader StandAloneSigTable;

		// Token: 0x040003EC RID: 1004
		internal EventMapTableReader EventMapTable;

		// Token: 0x040003ED RID: 1005
		internal EventPtrTableReader EventPtrTable;

		// Token: 0x040003EE RID: 1006
		internal EventTableReader EventTable;

		// Token: 0x040003EF RID: 1007
		internal PropertyMapTableReader PropertyMapTable;

		// Token: 0x040003F0 RID: 1008
		internal PropertyPtrTableReader PropertyPtrTable;

		// Token: 0x040003F1 RID: 1009
		internal PropertyTableReader PropertyTable;

		// Token: 0x040003F2 RID: 1010
		internal MethodSemanticsTableReader MethodSemanticsTable;

		// Token: 0x040003F3 RID: 1011
		internal MethodImplTableReader MethodImplTable;

		// Token: 0x040003F4 RID: 1012
		internal ModuleRefTableReader ModuleRefTable;

		// Token: 0x040003F5 RID: 1013
		internal TypeSpecTableReader TypeSpecTable;

		// Token: 0x040003F6 RID: 1014
		internal ImplMapTableReader ImplMapTable;

		// Token: 0x040003F7 RID: 1015
		internal FieldRVATableReader FieldRvaTable;

		// Token: 0x040003F8 RID: 1016
		internal EnCLogTableReader EncLogTable;

		// Token: 0x040003F9 RID: 1017
		internal EnCMapTableReader EncMapTable;

		// Token: 0x040003FA RID: 1018
		internal AssemblyTableReader AssemblyTable;

		// Token: 0x040003FB RID: 1019
		internal AssemblyProcessorTableReader AssemblyProcessorTable;

		// Token: 0x040003FC RID: 1020
		internal AssemblyOSTableReader AssemblyOSTable;

		// Token: 0x040003FD RID: 1021
		internal AssemblyRefTableReader AssemblyRefTable;

		// Token: 0x040003FE RID: 1022
		internal AssemblyRefProcessorTableReader AssemblyRefProcessorTable;

		// Token: 0x040003FF RID: 1023
		internal AssemblyRefOSTableReader AssemblyRefOSTable;

		// Token: 0x04000400 RID: 1024
		internal FileTableReader FileTable;

		// Token: 0x04000401 RID: 1025
		internal ExportedTypeTableReader ExportedTypeTable;

		// Token: 0x04000402 RID: 1026
		internal ManifestResourceTableReader ManifestResourceTable;

		// Token: 0x04000403 RID: 1027
		internal NestedClassTableReader NestedClassTable;

		// Token: 0x04000404 RID: 1028
		internal GenericParamTableReader GenericParamTable;

		// Token: 0x04000405 RID: 1029
		internal MethodSpecTableReader MethodSpecTable;

		// Token: 0x04000406 RID: 1030
		internal GenericParamConstraintTableReader GenericParamConstraintTable;

		// Token: 0x04000407 RID: 1031
		internal DocumentTableReader DocumentTable;

		// Token: 0x04000408 RID: 1032
		internal MethodDebugInformationTableReader MethodDebugInformationTable;

		// Token: 0x04000409 RID: 1033
		internal LocalScopeTableReader LocalScopeTable;

		// Token: 0x0400040A RID: 1034
		internal LocalVariableTableReader LocalVariableTable;

		// Token: 0x0400040B RID: 1035
		internal LocalConstantTableReader LocalConstantTable;

		// Token: 0x0400040C RID: 1036
		internal ImportScopeTableReader ImportScopeTable;

		// Token: 0x0400040D RID: 1037
		internal StateMachineMethodTableReader StateMachineMethodTable;

		// Token: 0x0400040E RID: 1038
		internal CustomDebugInformationTableReader CustomDebugInformationTable;

		// Token: 0x0400040F RID: 1039
		private const int SmallIndexSize = 2;

		// Token: 0x04000410 RID: 1040
		private const int LargeIndexSize = 4;

		// Token: 0x04000412 RID: 1042
		internal const string ClrPrefix = "<CLR>";

		// Token: 0x04000413 RID: 1043
		internal static readonly byte[] WinRTPrefix = new byte[] { 60, 87, 105, 110, 82, 84, 62 };

		// Token: 0x04000414 RID: 1044
		private static string[] s_projectedTypeNames;

		// Token: 0x04000415 RID: 1045
		private static MetadataReader.ProjectionInfo[] s_projectionInfos;

		// Token: 0x020001AD RID: 429
		private readonly struct ProjectionInfo
		{
			// Token: 0x06000CBE RID: 3262 RVA: 0x00025A24 File Offset: 0x00023C24
			public ProjectionInfo(string winRtNamespace, StringHandle.VirtualIndex clrNamespace, StringHandle.VirtualIndex clrName, AssemblyReferenceHandle.VirtualIndex clrAssembly, TypeDefTreatment treatment = TypeDefTreatment.RedirectedToClrType, TypeRefSignatureTreatment signatureTreatment = TypeRefSignatureTreatment.None, bool isIDisposable = false)
			{
				this.WinRTNamespace = winRtNamespace;
				this.ClrNamespace = clrNamespace;
				this.ClrName = clrName;
				this.AssemblyRef = clrAssembly;
				this.Treatment = treatment;
				this.SignatureTreatment = signatureTreatment;
				this.IsIDisposable = isIDisposable;
			}

			// Token: 0x04000A68 RID: 2664
			public readonly string WinRTNamespace;

			// Token: 0x04000A69 RID: 2665
			public readonly StringHandle.VirtualIndex ClrNamespace;

			// Token: 0x04000A6A RID: 2666
			public readonly StringHandle.VirtualIndex ClrName;

			// Token: 0x04000A6B RID: 2667
			public readonly AssemblyReferenceHandle.VirtualIndex AssemblyRef;

			// Token: 0x04000A6C RID: 2668
			public readonly TypeDefTreatment Treatment;

			// Token: 0x04000A6D RID: 2669
			public readonly TypeRefSignatureTreatment SignatureTreatment;

			// Token: 0x04000A6E RID: 2670
			public readonly bool IsIDisposable;
		}
	}
}
