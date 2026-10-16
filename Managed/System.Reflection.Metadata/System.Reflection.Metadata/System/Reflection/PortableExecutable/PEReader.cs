using System;
using System.Collections.Immutable;
using System.IO;
using System.IO.Compression;
using System.Reflection.Internal;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;

namespace System.Reflection.PortableExecutable
{
	/// <summary>Provides a reader for Portable Executable format (PE) files.</summary>
	// Token: 0x02000038 RID: 56
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PEReader : IDisposable
	{
		/// <summary>Gets a value that indicates if the PE image has been loaded into memory by the OS loader.</summary>
		/// <returns>
		///   <see langword="true" /> if the PE image has been loaded into memory by the OS loader; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00005FA0 File Offset: 0x000041A0
		public bool IsLoadedImage { get; }

		/// <summary>Creates a Portable Executable reader over a PE image stored in memory.</summary>
		/// <param name="peImage">A pointer to the start of the PE image.</param>
		/// <param name="size">The size of the PE image.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="peImage" /> is <see cref="F:System.IntPtr.Zero" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="size" /> is negative.</exception>
		// Token: 0x060001EF RID: 495 RVA: 0x00005FA8 File Offset: 0x000041A8
		[NullableContext(0)]
		public unsafe PEReader(byte* peImage, int size)
			: this(peImage, size, false)
		{
		}

		/// <summary>Creates a Portable Executable reader over a PE image stored in memory. A flag indicates whether the image has already been loaded into memory.</summary>
		/// <param name="peImage">A pointer to the start of the PE image.</param>
		/// <param name="size">The size of the PE image.</param>
		/// <param name="isLoadedImage">
		///   <see langword="true" /> if the PE image has been loaded into memory by the OS loader; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="peImage" /> is <see cref="F:System.IntPtr.Zero" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="size" /> is negative.</exception>
		// Token: 0x060001F0 RID: 496 RVA: 0x00005FB3 File Offset: 0x000041B3
		[NullableContext(0)]
		public unsafe PEReader(byte* peImage, int size, bool isLoadedImage)
		{
			if (peImage == null)
			{
				throw new ArgumentNullException("peImage");
			}
			if (size < 0)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			this._peImage = new ExternalMemoryBlockProvider(peImage, size);
			this.IsLoadedImage = isLoadedImage;
		}

		/// <summary>Creates a Portable Executable reader over a PE image stored in a stream.</summary>
		/// <param name="peStream">PE image stream.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="peStream" /> is <see langword="null" />.</exception>
		// Token: 0x060001F1 RID: 497 RVA: 0x00005FEE File Offset: 0x000041EE
		public PEReader(Stream peStream)
			: this(peStream, PEStreamOptions.Default)
		{
		}

		/// <summary>Creates a Portable Executable reader over a PE image stored in a stream beginning at its current position and ending at the end of the stream.</summary>
		/// <param name="peStream">A PE image stream.</param>
		/// <param name="options">Options specifying how sections of the PE image are read from the stream.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="peStream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> has an invalid value.</exception>
		/// <exception cref="T:System.IO.IOException">Error reading from the stream (only when prefetching data).</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <see cref="F:System.Reflection.PortableExecutable.PEStreamOptions.PrefetchMetadata" /> is specified, and the PE headers of the image are invalid.</exception>
		// Token: 0x060001F2 RID: 498 RVA: 0x00005FF8 File Offset: 0x000041F8
		public PEReader(Stream peStream, PEStreamOptions options)
			: this(peStream, options, 0)
		{
		}

		/// <summary>Creates a Portable Executable reader over a PE image of the given size beginning at the stream's current position.</summary>
		/// <param name="peStream">A PE image stream.</param>
		/// <param name="options">Options specifying how sections of the PE image are read from the stream.</param>
		/// <param name="size">The PE image size.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="size" /> is negative or extends past the end of the stream.</exception>
		/// <exception cref="T:System.IO.IOException">Error reading from the stream (only when prefetching data).</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <see cref="F:System.Reflection.PortableExecutable.PEStreamOptions.PrefetchMetadata" /> is specified, and the PE headers of the image are invalid.</exception>
		// Token: 0x060001F3 RID: 499 RVA: 0x00006004 File Offset: 0x00004204
		public PEReader(Stream peStream, PEStreamOptions options, int size)
		{
			if (peStream == null)
			{
				throw new ArgumentNullException("peStream");
			}
			if (!peStream.CanRead || !peStream.CanSeek)
			{
				throw new ArgumentException(SR.StreamMustSupportReadAndSeek, "peStream");
			}
			if (!options.IsValid())
			{
				throw new ArgumentOutOfRangeException("options");
			}
			this.IsLoadedImage = (options & PEStreamOptions.IsLoadedImage) > PEStreamOptions.Default;
			long position = peStream.Position;
			int andValidateSize = StreamExtensions.GetAndValidateSize(peStream, size, "peStream");
			bool flag = true;
			try
			{
				bool flag2 = FileStreamReadLightUp.IsFileStream(peStream);
				if ((options & (PEStreamOptions.PrefetchMetadata | PEStreamOptions.PrefetchEntireImage)) == PEStreamOptions.Default)
				{
					this._peImage = new StreamMemoryBlockProvider(peStream, position, andValidateSize, flag2, (options & PEStreamOptions.LeaveOpen) > PEStreamOptions.Default);
					flag = false;
				}
				else if ((options & PEStreamOptions.PrefetchEntireImage) != PEStreamOptions.Default)
				{
					NativeHeapMemoryBlock nativeHeapMemoryBlock = StreamMemoryBlockProvider.ReadMemoryBlockNoLock(peStream, flag2, position, andValidateSize);
					this._lazyImageBlock = nativeHeapMemoryBlock;
					this._peImage = new ExternalMemoryBlockProvider(nativeHeapMemoryBlock.Pointer, nativeHeapMemoryBlock.Size);
					if ((options & PEStreamOptions.PrefetchMetadata) != PEStreamOptions.Default)
					{
						this.InitializePEHeaders();
					}
				}
				else
				{
					this._lazyPEHeaders = new PEHeaders(peStream);
					this._lazyMetadataBlock = StreamMemoryBlockProvider.ReadMemoryBlockNoLock(peStream, flag2, (long)this._lazyPEHeaders.MetadataStartOffset, this._lazyPEHeaders.MetadataSize);
				}
			}
			finally
			{
				if (flag && (options & PEStreamOptions.LeaveOpen) == PEStreamOptions.Default)
				{
					peStream.Dispose();
				}
			}
		}

		/// <summary>Creates a Portable Executable reader over a PE image stored in a byte array.</summary>
		/// <param name="peImage">An immutable array of bytes representing the PE image.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="peImage" /> is <see langword="null" />.</exception>
		// Token: 0x060001F4 RID: 500 RVA: 0x00006130 File Offset: 0x00004330
		[NullableContext(0)]
		public PEReader(ImmutableArray<byte> peImage)
		{
			if (peImage.IsDefault)
			{
				throw new ArgumentNullException("peImage");
			}
			this._peImage = new ByteArrayMemoryProvider(peImage);
		}

		/// <summary>Disposes all memory allocated by the reader.</summary>
		// Token: 0x060001F5 RID: 501 RVA: 0x00006158 File Offset: 0x00004358
		public void Dispose()
		{
			this._lazyPEHeaders = null;
			MemoryBlockProvider peImage = this._peImage;
			if (peImage != null)
			{
				peImage.Dispose();
			}
			this._peImage = null;
			AbstractMemoryBlock lazyImageBlock = this._lazyImageBlock;
			if (lazyImageBlock != null)
			{
				lazyImageBlock.Dispose();
			}
			this._lazyImageBlock = null;
			AbstractMemoryBlock lazyMetadataBlock = this._lazyMetadataBlock;
			if (lazyMetadataBlock != null)
			{
				lazyMetadataBlock.Dispose();
			}
			this._lazyMetadataBlock = null;
			AbstractMemoryBlock[] lazyPESectionBlocks = this._lazyPESectionBlocks;
			if (lazyPESectionBlocks != null)
			{
				foreach (AbstractMemoryBlock abstractMemoryBlock in lazyPESectionBlocks)
				{
					if (abstractMemoryBlock != null)
					{
						abstractMemoryBlock.Dispose();
					}
				}
				this._lazyPESectionBlocks = null;
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000061E4 File Offset: 0x000043E4
		private MemoryBlockProvider GetPEImage()
		{
			MemoryBlockProvider peImage = this._peImage;
			if (peImage == null)
			{
				if (this._lazyPEHeaders == null)
				{
					Throw.PEReaderDisposed();
				}
				Throw.InvalidOperation_PEImageNotAvailable();
			}
			return peImage;
		}

		/// <summary>Gets the PE headers.</summary>
		/// <exception cref="T:System.BadImageFormatException">The headers contain invalid data.</exception>
		/// <exception cref="T:System.IO.IOException">Error reading from the stream.</exception>
		/// <returns>The PE headers for this PE image.</returns>
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000620E File Offset: 0x0000440E
		public PEHeaders PEHeaders
		{
			get
			{
				if (this._lazyPEHeaders == null)
				{
					this.InitializePEHeaders();
				}
				return this._lazyPEHeaders;
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00006224 File Offset: 0x00004424
		private void InitializePEHeaders()
		{
			StreamConstraints streamConstraints;
			Stream stream = this.GetPEImage().GetStream(out streamConstraints);
			PEHeaders peheaders;
			if (streamConstraints.GuardOpt != null)
			{
				object guardOpt = streamConstraints.GuardOpt;
				lock (guardOpt)
				{
					peheaders = PEReader.ReadPEHeadersNoLock(stream, streamConstraints.ImageStart, streamConstraints.ImageSize, this.IsLoadedImage);
					goto IL_0067;
				}
			}
			peheaders = PEReader.ReadPEHeadersNoLock(stream, streamConstraints.ImageStart, streamConstraints.ImageSize, this.IsLoadedImage);
			IL_0067:
			Interlocked.CompareExchange<PEHeaders>(ref this._lazyPEHeaders, peheaders, null);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x000062B8 File Offset: 0x000044B8
		private static PEHeaders ReadPEHeadersNoLock(Stream stream, long imageStartPosition, int imageSize, bool isLoadedImage)
		{
			stream.Seek(imageStartPosition, 0);
			return new PEHeaders(stream, imageSize, isLoadedImage);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x000062CC File Offset: 0x000044CC
		private AbstractMemoryBlock GetEntireImageBlock()
		{
			if (this._lazyImageBlock == null)
			{
				AbstractMemoryBlock memoryBlock = this.GetPEImage().GetMemoryBlock();
				if (Interlocked.CompareExchange<AbstractMemoryBlock>(ref this._lazyImageBlock, memoryBlock, null) != null)
				{
					memoryBlock.Dispose();
				}
			}
			return this._lazyImageBlock;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00006308 File Offset: 0x00004508
		private AbstractMemoryBlock GetMetadataBlock()
		{
			if (!this.HasMetadata)
			{
				throw new InvalidOperationException(SR.PEImageDoesNotHaveMetadata);
			}
			if (this._lazyMetadataBlock == null)
			{
				AbstractMemoryBlock memoryBlock = this.GetPEImage().GetMemoryBlock(this.PEHeaders.MetadataStartOffset, this.PEHeaders.MetadataSize);
				if (Interlocked.CompareExchange<AbstractMemoryBlock>(ref this._lazyMetadataBlock, memoryBlock, null) != null)
				{
					memoryBlock.Dispose();
				}
			}
			return this._lazyMetadataBlock;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00006370 File Offset: 0x00004570
		private AbstractMemoryBlock GetPESectionBlock(int index)
		{
			MemoryBlockProvider peimage = this.GetPEImage();
			if (this._lazyPESectionBlocks == null)
			{
				Interlocked.CompareExchange<AbstractMemoryBlock[]>(ref this._lazyPESectionBlocks, new AbstractMemoryBlock[this.PEHeaders.SectionHeaders.Length], null);
			}
			AbstractMemoryBlock abstractMemoryBlock;
			if (this.IsLoadedImage)
			{
				abstractMemoryBlock = peimage.GetMemoryBlock(this.PEHeaders.SectionHeaders[index].VirtualAddress, this.PEHeaders.SectionHeaders[index].VirtualSize);
			}
			else
			{
				int num = Math.Min(this.PEHeaders.SectionHeaders[index].VirtualSize, this.PEHeaders.SectionHeaders[index].SizeOfRawData);
				abstractMemoryBlock = peimage.GetMemoryBlock(this.PEHeaders.SectionHeaders[index].PointerToRawData, num);
			}
			if (Interlocked.CompareExchange<AbstractMemoryBlock>(ref this._lazyPESectionBlocks[index], abstractMemoryBlock, null) != null)
			{
				abstractMemoryBlock.Dispose();
			}
			return this._lazyPESectionBlocks[index];
		}

		/// <summary>Gets a value that indicates if the reader can access the entire PE image.</summary>
		/// <returns>
		///   <see langword="true" /> if the reader can access the entire PE image; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00006481 File Offset: 0x00004681
		public bool IsEntireImageAvailable
		{
			get
			{
				return this._lazyImageBlock != null || this._peImage != null;
			}
		}

		/// <summary>Gets a <see cref="T:System.Reflection.PortableExecutable.PEMemoryBlock" /> object containing the entire PE image.</summary>
		/// <exception cref="T:System.InvalidOperationException">The entire PE image is not available.</exception>
		/// <returns>A memory block that contains the entire PE image.</returns>
		// Token: 0x060001FE RID: 510 RVA: 0x00006496 File Offset: 0x00004696
		public PEMemoryBlock GetEntireImage()
		{
			return new PEMemoryBlock(this.GetEntireImageBlock(), 0);
		}

		/// <summary>Gets a value that indicates if the PE image contains CLI metadata.</summary>
		/// <exception cref="T:System.BadImageFormatException">The PE headers contain invalid data.</exception>
		/// <exception cref="T:System.IO.IOException">Error reading from the underlying stream.</exception>
		/// <returns>
		///   <see langword="true" /> if the PE image contains CLI metadata; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060001FF RID: 511 RVA: 0x000064A4 File Offset: 0x000046A4
		public bool HasMetadata
		{
			get
			{
				return this.PEHeaders.MetadataSize > 0;
			}
		}

		/// <summary>Loads a PE section that contains CLI metadata.</summary>
		/// <exception cref="T:System.InvalidOperationException">The PE image doesn't contain metadata (<see cref="P:System.Reflection.PortableExecutable.PEReader.HasMetadata" /> returns <see langword="false" />).</exception>
		/// <exception cref="T:System.BadImageFormatException">The PE headers contain invalid data.</exception>
		/// <exception cref="T:System.IO.IOException">IO error while reading from the underlying stream.</exception>
		/// <returns>A memory block that contains the CLI metadata.</returns>
		// Token: 0x06000200 RID: 512 RVA: 0x000064B4 File Offset: 0x000046B4
		public PEMemoryBlock GetMetadata()
		{
			return new PEMemoryBlock(this.GetMetadataBlock(), 0);
		}

		/// <summary>Loads the PE section that contains the specified relative virtual address into memory and returns a memory block that starts at that address and ends at the end of the containing section.</summary>
		/// <param name="relativeVirtualAddress">The Relative Virtual Address of the data to read.</param>
		/// <exception cref="T:System.BadImageFormatException">The PE headers contain invalid data.</exception>
		/// <exception cref="T:System.IO.IOException">An IO error occurred while reading from the underlying stream.</exception>
		/// <exception cref="T:System.InvalidOperationException">The PE image is not available.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="relativeVirtualAddress" /> is negative.</exception>
		/// <returns>A memory block that starts at <paramref name="relativeVirtualAddress" /> and ends at the end of the containing section, or an empty block if <paramref name="relativeVirtualAddress" /> doesn't represent a location in any of the PE sections of this PE image.</returns>
		// Token: 0x06000201 RID: 513 RVA: 0x000064C4 File Offset: 0x000046C4
		public PEMemoryBlock GetSectionData(int relativeVirtualAddress)
		{
			if (relativeVirtualAddress < 0)
			{
				Throw.ArgumentOutOfRange("relativeVirtualAddress");
			}
			int containingSectionIndex = this.PEHeaders.GetContainingSectionIndex(relativeVirtualAddress);
			if (containingSectionIndex < 0)
			{
				return default(PEMemoryBlock);
			}
			AbstractMemoryBlock pesectionBlock = this.GetPESectionBlock(containingSectionIndex);
			int num = relativeVirtualAddress - this.PEHeaders.SectionHeaders[containingSectionIndex].VirtualAddress;
			if (num > pesectionBlock.Size)
			{
				return default(PEMemoryBlock);
			}
			return new PEMemoryBlock(pesectionBlock, num);
		}

		/// <summary>Loads the PE section with the specified name into memory and returns a memory block that spans the section.</summary>
		/// <param name="sectionName">The name of the section.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sectionName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The PE image is not available.</exception>
		/// <returns>A memory block that spans the section, or an empty block if no section of the given <paramref name="sectionName" /> exists in this PE image.</returns>
		// Token: 0x06000202 RID: 514 RVA: 0x00006540 File Offset: 0x00004740
		public PEMemoryBlock GetSectionData(string sectionName)
		{
			if (sectionName == null)
			{
				Throw.ArgumentNull("sectionName");
			}
			int num = this.PEHeaders.IndexOfSection(sectionName);
			if (num < 0)
			{
				return default(PEMemoryBlock);
			}
			return new PEMemoryBlock(this.GetPESectionBlock(num), 0);
		}

		/// <summary>Reads all Debug Directory table entries.</summary>
		/// <exception cref="T:System.BadImageFormatException">Bad format of the entry.</exception>
		/// <exception cref="T:System.IO.IOException">IO error while reading from the underlying stream.</exception>
		/// <exception cref="T:System.InvalidOperationException">The PE image is not available.</exception>
		/// <returns>An array of Debug Directory table entries.</returns>
		// Token: 0x06000203 RID: 515 RVA: 0x00006584 File Offset: 0x00004784
		[NullableContext(0)]
		public ImmutableArray<DebugDirectoryEntry> ReadDebugDirectory()
		{
			DirectoryEntry debugTableDirectory = this.PEHeaders.PEHeader.DebugTableDirectory;
			if (debugTableDirectory.Size == 0)
			{
				return ImmutableArray<DebugDirectoryEntry>.Empty;
			}
			int num;
			if (!this.PEHeaders.TryGetDirectoryOffset(debugTableDirectory, out num))
			{
				throw new BadImageFormatException(SR.InvalidDirectoryRVA);
			}
			if (debugTableDirectory.Size % 28 != 0)
			{
				throw new BadImageFormatException(SR.InvalidDirectorySize);
			}
			ImmutableArray<DebugDirectoryEntry> immutableArray;
			using (AbstractMemoryBlock memoryBlock = this.GetPEImage().GetMemoryBlock(num, debugTableDirectory.Size))
			{
				immutableArray = PEReader.ReadDebugDirectoryEntries(memoryBlock.GetReader());
			}
			return immutableArray;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00006620 File Offset: 0x00004820
		[NullableContext(0)]
		internal static ImmutableArray<DebugDirectoryEntry> ReadDebugDirectoryEntries(BlobReader reader)
		{
			int num = reader.Length / 28;
			ImmutableArray<DebugDirectoryEntry>.Builder builder = ImmutableArray.CreateBuilder<DebugDirectoryEntry>(num);
			for (int i = 0; i < num; i++)
			{
				int num2 = reader.ReadInt32();
				if (num2 != 0)
				{
					throw new BadImageFormatException(SR.InvalidDebugDirectoryEntryCharacteristics);
				}
				uint num3 = reader.ReadUInt32();
				ushort num4 = reader.ReadUInt16();
				ushort num5 = reader.ReadUInt16();
				DebugDirectoryEntryType debugDirectoryEntryType = (DebugDirectoryEntryType)reader.ReadInt32();
				int num6 = reader.ReadInt32();
				int num7 = reader.ReadInt32();
				int num8 = reader.ReadInt32();
				builder.Add(new DebugDirectoryEntry(num3, num4, num5, debugDirectoryEntryType, num6, num7, num8));
			}
			return builder.MoveToImmutable();
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000066C0 File Offset: 0x000048C0
		private AbstractMemoryBlock GetDebugDirectoryEntryDataBlock(DebugDirectoryEntry entry)
		{
			int num = (this.IsLoadedImage ? entry.DataRelativeVirtualAddress : entry.DataPointer);
			return this.GetPEImage().GetMemoryBlock(num, entry.DataSize);
		}

		/// <summary>Reads the data pointed to by the specified Debug Directory entry and interprets it as CodeView.</summary>
		/// <param name="entry">A Debug Directory entry instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="entry" /> is not a CodeView entry.</exception>
		/// <exception cref="T:System.BadImageFormatException">Bad format of the data.</exception>
		/// <exception cref="T:System.IO.IOException">IO error while reading from the underlying stream.</exception>
		/// <exception cref="T:System.InvalidOperationException">The PE image is not available.</exception>
		/// <returns>A code view debug directory data instance.</returns>
		// Token: 0x06000206 RID: 518 RVA: 0x000066FC File Offset: 0x000048FC
		public CodeViewDebugDirectoryData ReadCodeViewDebugDirectoryData(DebugDirectoryEntry entry)
		{
			if (entry.Type != DebugDirectoryEntryType.CodeView)
			{
				Throw.InvalidArgument(SR.Format(SR.UnexpectedDebugDirectoryType, "CodeView"), "entry");
			}
			CodeViewDebugDirectoryData codeViewDebugDirectoryData;
			using (AbstractMemoryBlock debugDirectoryEntryDataBlock = this.GetDebugDirectoryEntryDataBlock(entry))
			{
				codeViewDebugDirectoryData = PEReader.DecodeCodeViewDebugDirectoryData(debugDirectoryEntryDataBlock);
			}
			return codeViewDebugDirectoryData;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00006758 File Offset: 0x00004958
		internal static CodeViewDebugDirectoryData DecodeCodeViewDebugDirectoryData(AbstractMemoryBlock block)
		{
			BlobReader reader = block.GetReader();
			if (reader.ReadByte() != 82 || reader.ReadByte() != 83 || reader.ReadByte() != 68 || reader.ReadByte() != 83)
			{
				throw new BadImageFormatException(SR.UnexpectedCodeViewDataSignature);
			}
			Guid guid = reader.ReadGuid();
			int num = reader.ReadInt32();
			string text = reader.ReadUtf8NullTerminated();
			return new CodeViewDebugDirectoryData(guid, num, text);
		}

		/// <summary>Reads the data pointed to by the specified Debug Directory entry and interprets it as a PDB Checksum entry.</summary>
		/// <param name="entry">The Debug Directory entry whose data is to be read.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="entry" /> is not a PDB Checksum entry.</exception>
		/// <exception cref="T:System.BadImageFormatException">Bad format of the data.</exception>
		/// <exception cref="T:System.IO.IOException">IO error while reading from the underlying stream.</exception>
		/// <exception cref="T:System.InvalidOperationException">The PE image is not available.</exception>
		/// <returns>The PDB Checksum entry.</returns>
		// Token: 0x06000208 RID: 520 RVA: 0x000067C4 File Offset: 0x000049C4
		public PdbChecksumDebugDirectoryData ReadPdbChecksumDebugDirectoryData(DebugDirectoryEntry entry)
		{
			if (entry.Type != DebugDirectoryEntryType.PdbChecksum)
			{
				Throw.InvalidArgument(SR.Format(SR.UnexpectedDebugDirectoryType, "PdbChecksum"), "entry");
			}
			PdbChecksumDebugDirectoryData pdbChecksumDebugDirectoryData;
			using (AbstractMemoryBlock debugDirectoryEntryDataBlock = this.GetDebugDirectoryEntryDataBlock(entry))
			{
				pdbChecksumDebugDirectoryData = PEReader.DecodePdbChecksumDebugDirectoryData(debugDirectoryEntryDataBlock);
			}
			return pdbChecksumDebugDirectoryData;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00006824 File Offset: 0x00004A24
		internal static PdbChecksumDebugDirectoryData DecodePdbChecksumDebugDirectoryData(AbstractMemoryBlock block)
		{
			BlobReader reader = block.GetReader();
			string text = reader.ReadUtf8NullTerminated();
			byte[] array = reader.ReadBytes(reader.RemainingBytes);
			if (text.Length == 0 || array.Length == 0)
			{
				throw new BadImageFormatException(SR.InvalidPdbChecksumDataFormat);
			}
			return new PdbChecksumDebugDirectoryData(text, ImmutableByteArrayInterop.DangerousCreateFromUnderlyingArray(ref array));
		}

		/// <summary>Opens a Portable PDB associated with this PE image.</summary>
		/// <param name="peImagePath">The path to the PE image. The path is used to locate the PDB file located in the directory containing the PE file.</param>
		/// <param name="pdbFileStreamProvider">If specified, called to open a <see cref="T:System.IO.Stream" /> for a given file path. The provider is expected to either return a readable and seekable <see cref="T:System.IO.Stream" />, or <see langword="null" /> if the target file doesn't exist or should be ignored for some reason. The provider should throw <see cref="T:System.IO.IOException" /> if it fails to open the file due to an unexpected IO error.</param>
		/// <param name="pdbReaderProvider">If successful, a new instance of <see cref="T:System.Reflection.Metadata.MetadataReaderProvider" /> to be used to read the Portable PDB.</param>
		/// <param name="pdbPath">If successful and the PDB is found in a file, the path to the file, or <see langword="null" /> if the PDB is embedded in the PE image itself.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="peImagePath" /> or <paramref name="pdbFileStreamProvider" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream returned from <paramref name="pdbFileStreamProvider" /> doesn't support read and seek operations.</exception>
		/// <exception cref="T:System.BadImageFormatException">No matching PDB file was found due to an error: The PE image or the PDB is invalid.</exception>
		/// <exception cref="T:System.IO.IOException">No matching PDB file was found due to an error: An IO error occurred while reading the PE image or the PDB.</exception>
		/// <returns>
		///   <see langword="true" /> if the PE image has a PDB associated with it and the PDB has been successfully opened; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600020A RID: 522 RVA: 0x00006874 File Offset: 0x00004A74
		[NullableContext(2)]
		public bool TryOpenAssociatedPortablePdb([Nullable(1)] string peImagePath, [Nullable(new byte[] { 1, 1, 2 })] Func<string, Stream> pdbFileStreamProvider, out MetadataReaderProvider pdbReaderProvider, out string pdbPath)
		{
			if (peImagePath == null)
			{
				Throw.ArgumentNull("peImagePath");
			}
			if (pdbFileStreamProvider == null)
			{
				Throw.ArgumentNull("pdbFileStreamProvider");
			}
			pdbReaderProvider = null;
			pdbPath = null;
			string directoryName;
			try
			{
				directoryName = Path.GetDirectoryName(peImagePath);
			}
			catch (Exception ex)
			{
				throw new ArgumentException(ex.Message, "peImagePath");
			}
			Exception ex2 = null;
			ImmutableArray<DebugDirectoryEntry> immutableArray = this.ReadDebugDirectory();
			DebugDirectoryEntry debugDirectoryEntry = immutableArray.FirstOrDefault<DebugDirectoryEntry>((DebugDirectoryEntry e) => e.IsPortableCodeView);
			if (debugDirectoryEntry.DataSize != 0 && this.TryOpenCodeViewPortablePdb(debugDirectoryEntry, directoryName, pdbFileStreamProvider, out pdbReaderProvider, out pdbPath, ref ex2))
			{
				return true;
			}
			DebugDirectoryEntry debugDirectoryEntry2 = immutableArray.FirstOrDefault<DebugDirectoryEntry>((DebugDirectoryEntry e) => e.Type == DebugDirectoryEntryType.EmbeddedPortablePdb);
			if (debugDirectoryEntry2.DataSize != 0)
			{
				bool flag = false;
				pdbReaderProvider = null;
				this.TryOpenEmbeddedPortablePdb(debugDirectoryEntry2, ref flag, ref pdbReaderProvider, ref ex2);
				if (flag)
				{
					return true;
				}
			}
			if (ex2 != null)
			{
				ExceptionDispatchInfo.Capture(ex2).Throw();
			}
			return false;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00006974 File Offset: 0x00004B74
		private bool TryOpenCodeViewPortablePdb(DebugDirectoryEntry codeViewEntry, string peImageDirectory, Func<string, Stream> pdbFileStreamProvider, out MetadataReaderProvider provider, out string pdbPath, ref Exception errorToReport)
		{
			pdbPath = null;
			provider = null;
			CodeViewDebugDirectoryData codeViewDebugDirectoryData;
			try
			{
				codeViewDebugDirectoryData = this.ReadCodeViewDebugDirectoryData(codeViewEntry);
			}
			catch (Exception ex) when (ex is BadImageFormatException || ex is IOException)
			{
				errorToReport = errorToReport ?? ex;
				return false;
			}
			BlobContentId blobContentId = new BlobContentId(codeViewDebugDirectoryData.Guid, codeViewEntry.Stamp);
			string text = PathUtilities.CombinePathWithRelativePath(peImageDirectory, PathUtilities.GetFileName(codeViewDebugDirectoryData.Path, true));
			if (this.TryOpenPortablePdbFile(text, blobContentId, pdbFileStreamProvider, out provider, ref errorToReport))
			{
				pdbPath = text;
				return true;
			}
			return false;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00006A1C File Offset: 0x00004C1C
		private bool TryOpenPortablePdbFile(string path, BlobContentId id, Func<string, Stream> pdbFileStreamProvider, out MetadataReaderProvider provider, ref Exception errorToReport)
		{
			provider = null;
			MetadataReaderProvider metadataReaderProvider = null;
			bool flag;
			try
			{
				Stream stream;
				try
				{
					stream = pdbFileStreamProvider.Invoke(path);
				}
				catch (FileNotFoundException)
				{
					stream = null;
				}
				if (stream == null)
				{
					flag = false;
				}
				else
				{
					if (!stream.CanRead || !stream.CanSeek)
					{
						throw new InvalidOperationException(SR.StreamMustSupportReadAndSeek);
					}
					metadataReaderProvider = MetadataReaderProvider.FromPortablePdbStream(stream, MetadataStreamOptions.Default, 0);
					if (new BlobContentId(metadataReaderProvider.GetMetadataReader(MetadataReaderOptions.Default, null).DebugMetadataHeader.Id) != id)
					{
						flag = false;
					}
					else
					{
						provider = metadataReaderProvider;
						flag = true;
					}
				}
			}
			catch (Exception ex) when (ex is BadImageFormatException || ex is IOException)
			{
				errorToReport = errorToReport ?? ex;
				flag = false;
			}
			finally
			{
				if (provider == null && metadataReaderProvider != null)
				{
					metadataReaderProvider.Dispose();
				}
			}
			return flag;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00006B08 File Offset: 0x00004D08
		private void TryOpenEmbeddedPortablePdb(DebugDirectoryEntry embeddedPdbEntry, ref bool openedEmbeddedPdb, ref MetadataReaderProvider provider, ref Exception errorToReport)
		{
			provider = null;
			MetadataReaderProvider metadataReaderProvider = null;
			try
			{
				metadataReaderProvider = this.ReadEmbeddedPortablePdbDebugDirectoryData(embeddedPdbEntry);
				metadataReaderProvider.GetMetadataReader(MetadataReaderOptions.Default, null);
				provider = metadataReaderProvider;
				openedEmbeddedPdb = true;
			}
			catch (Exception ex) when (ex is BadImageFormatException || ex is IOException)
			{
				errorToReport = errorToReport ?? ex;
				openedEmbeddedPdb = false;
			}
			finally
			{
				if (metadataReaderProvider == null && metadataReaderProvider != null)
				{
					metadataReaderProvider.Dispose();
				}
			}
		}

		/// <summary>Reads the data pointed to by the specified Debug Directory entry and interprets it as an Embedded Portable PDB blob.</summary>
		/// <param name="entry">The Debug Directory entry whose data is to be read.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="entry" /> is not a <see cref="F:System.Reflection.PortableExecutable.DebugDirectoryEntryType.EmbeddedPortablePdb" /> entry.</exception>
		/// <exception cref="T:System.BadImageFormatException">Bad format of the data.</exception>
		/// <exception cref="T:System.InvalidOperationException">PE image not available.</exception>
		/// <returns>The provider of a metadata reader for reading a Portable PDB image.</returns>
		// Token: 0x0600020E RID: 526 RVA: 0x00006B94 File Offset: 0x00004D94
		public MetadataReaderProvider ReadEmbeddedPortablePdbDebugDirectoryData(DebugDirectoryEntry entry)
		{
			if (entry.Type != DebugDirectoryEntryType.EmbeddedPortablePdb)
			{
				Throw.InvalidArgument(SR.Format(SR.UnexpectedDebugDirectoryType, "EmbeddedPortablePdb"), "entry");
			}
			PEReader.ValidateEmbeddedPortablePdbVersion(entry);
			MetadataReaderProvider metadataReaderProvider;
			using (AbstractMemoryBlock debugDirectoryEntryDataBlock = this.GetDebugDirectoryEntryDataBlock(entry))
			{
				ImmutableArray<byte> immutableArray = PEReader.DecodeEmbeddedPortablePdbDebugDirectoryData(debugDirectoryEntryDataBlock);
				metadataReaderProvider = MetadataReaderProvider.FromPortablePdbImage(immutableArray);
			}
			return metadataReaderProvider;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00006C00 File Offset: 0x00004E00
		internal static void ValidateEmbeddedPortablePdbVersion(DebugDirectoryEntry entry)
		{
			ushort majorVersion = entry.MajorVersion;
			if (majorVersion < 256)
			{
				throw new BadImageFormatException(SR.Format(SR.UnsupportedFormatVersion, PortablePdbVersions.Format(majorVersion)));
			}
			ushort minorVersion = entry.MinorVersion;
			if (minorVersion != 256)
			{
				throw new BadImageFormatException(SR.Format(SR.UnsupportedFormatVersion, PortablePdbVersions.Format(minorVersion)));
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00006C5C File Offset: 0x00004E5C
		[NullableContext(0)]
		internal static ImmutableArray<byte> DecodeEmbeddedPortablePdbDebugDirectoryData([Nullable(1)] AbstractMemoryBlock block)
		{
			BlobReader reader = block.GetReader();
			if (reader.ReadUInt32() != 1111773261U)
			{
				throw new BadImageFormatException(SR.UnexpectedEmbeddedPortablePdbDataSignature);
			}
			int num = reader.ReadInt32();
			byte[] array;
			try
			{
				array = new byte[num];
			}
			catch
			{
				throw new BadImageFormatException(SR.DataTooBig);
			}
			ReadOnlyUnmanagedMemoryStream readOnlyUnmanagedMemoryStream = new ReadOnlyUnmanagedMemoryStream(reader.CurrentPointer, reader.RemainingBytes);
			DeflateStream deflateStream = new DeflateStream(readOnlyUnmanagedMemoryStream, 0, true);
			if (num > 0)
			{
				int num2;
				try
				{
					num2 = deflateStream.TryReadAll(array, 0, array.Length);
				}
				catch (InvalidDataException ex)
				{
					throw new BadImageFormatException(ex.Message, ex.InnerException);
				}
				if (num2 != array.Length)
				{
					throw new BadImageFormatException(SR.SizeMismatch);
				}
			}
			if (deflateStream.ReadByte() != -1)
			{
				throw new BadImageFormatException(SR.SizeMismatch);
			}
			return ImmutableByteArrayInterop.DangerousCreateFromUnderlyingArray(ref array);
		}

		// Token: 0x0400017E RID: 382
		private MemoryBlockProvider _peImage;

		// Token: 0x0400017F RID: 383
		private PEHeaders _lazyPEHeaders;

		// Token: 0x04000180 RID: 384
		private AbstractMemoryBlock _lazyMetadataBlock;

		// Token: 0x04000181 RID: 385
		private AbstractMemoryBlock _lazyImageBlock;

		// Token: 0x04000182 RID: 386
		private AbstractMemoryBlock[] _lazyPESectionBlocks;
	}
}
