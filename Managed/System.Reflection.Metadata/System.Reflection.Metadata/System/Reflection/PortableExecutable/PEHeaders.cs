using System;
using System.Collections.Immutable;
using System.IO;
using System.Reflection.Internal;
using System.Runtime.CompilerServices;

namespace System.Reflection.PortableExecutable
{
	/// <summary>Defines a type that reads PE (Portable Executable) and COFF (Common Object File Format) headers from a stream.</summary>
	// Token: 0x02000036 RID: 54
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PEHeaders
	{
		/// <summary>Instantiates a new instance of the <see cref="T:System.Reflection.PortableExecutable.PEHeaders" /> class that reads the PE headers from the current location in the specified stream.</summary>
		/// <param name="peStream">A stream containing the PE image starting at the stream's current position and ending at the end of the stream.</param>
		/// <exception cref="T:System.BadImageFormatException">The data read from the stream has an invalid format.</exception>
		/// <exception cref="T:System.IO.IOException">Error reading from the stream.</exception>
		/// <exception cref="T:System.ArgumentException">The stream does not support seek operations.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="peStream" /> is <see langword="null" />.</exception>
		// Token: 0x060001CF RID: 463 RVA: 0x00005947 File Offset: 0x00003B47
		public PEHeaders(Stream peStream)
			: this(peStream, 0)
		{
		}

		/// <summary>Instantiates a new instance of the <see cref="T:System.Reflection.PortableExecutable.PEHeaders" /> class that reads the PE headers from a stream that represents a PE image of a specified size.</summary>
		/// <param name="peStream">A stream containing PE image of the given size starting at its current position.</param>
		/// <param name="size">The size of the PE image.</param>
		/// <exception cref="T:System.BadImageFormatException">The data read from the stream has an invalid format.</exception>
		/// <exception cref="T:System.IO.IOException">Error reading from the stream.</exception>
		/// <exception cref="T:System.ArgumentException">The stream does not support seek operations.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="peStream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="size" /> is negative or extends past the end of the stream.</exception>
		// Token: 0x060001D0 RID: 464 RVA: 0x00005951 File Offset: 0x00003B51
		public PEHeaders(Stream peStream, int size)
			: this(peStream, size, false)
		{
		}

		/// <summary>Instantiates a new instance of the <see cref="T:System.Reflection.PortableExecutable.PEHeaders" /> class that reads the PE headers from a stream that represents a PE image of a specified size and indicates whether the PE image has been loaded into memory.</summary>
		/// <param name="peStream">The stream containing PE image of the given size starting at its current position.</param>
		/// <param name="size">The size of the PE image.</param>
		/// <param name="isLoadedImage">
		///   <see langword="true" /> if the PE image has been loaded into memory by the OS loader; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.BadImageFormatException">The data read from the stream has invalid format.</exception>
		/// <exception cref="T:System.IO.IOException">Error reading from the stream.</exception>
		/// <exception cref="T:System.ArgumentException">The stream does not support seek operations.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="peStream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="size" /> is negative or extends past the end of the stream.</exception>
		// Token: 0x060001D1 RID: 465 RVA: 0x0000595C File Offset: 0x00003B5C
		public PEHeaders(Stream peStream, int size, bool isLoadedImage)
		{
			if (peStream == null)
			{
				throw new ArgumentNullException("peStream");
			}
			if (!peStream.CanRead || !peStream.CanSeek)
			{
				throw new ArgumentException(SR.StreamMustSupportReadAndSeek, "peStream");
			}
			this._isLoadedImage = isLoadedImage;
			int andValidateSize = StreamExtensions.GetAndValidateSize(peStream, size, "peStream");
			PEBinaryReader pebinaryReader = new PEBinaryReader(peStream, andValidateSize);
			bool flag;
			this.SkipDosHeader(ref pebinaryReader, out flag);
			this._coffHeaderStartOffset = pebinaryReader.CurrentOffset;
			this._coffHeader = new CoffHeader(ref pebinaryReader);
			if (!flag)
			{
				this._peHeaderStartOffset = pebinaryReader.CurrentOffset;
				this._peHeader = new PEHeader(ref pebinaryReader);
			}
			this._sectionHeaders = this.ReadSectionHeaders(ref pebinaryReader);
			int num;
			if (!flag && this.TryCalculateCorHeaderOffset((long)andValidateSize, out num))
			{
				this._corHeaderStartOffset = num;
				pebinaryReader.Seek(num);
				this._corHeader = new CorHeader(ref pebinaryReader);
			}
			this.CalculateMetadataLocation((long)andValidateSize, out this._metadataStartOffset, out this._metadataSize);
		}

		/// <summary>Gets the offset (in bytes) from the start of the PE image to the start of the CLI metadata.</summary>
		/// <returns>The offset (in bytes) from the start of the PE image to the start of the CLI metadata, or -1 if the image does not contain metadata.</returns>
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00005A64 File Offset: 0x00003C64
		public int MetadataStartOffset
		{
			get
			{
				return this._metadataStartOffset;
			}
		}

		/// <summary>Gets the size of the CLI metadata.</summary>
		/// <returns>the size of the CLI metadata, or 0 if the image does not contain metadata.</returns>
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00005A6C File Offset: 0x00003C6C
		public int MetadataSize
		{
			get
			{
				return this._metadataSize;
			}
		}

		/// <summary>Gets the COFF header of the image.</summary>
		/// <returns>The COFF header of the image.</returns>
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00005A74 File Offset: 0x00003C74
		public CoffHeader CoffHeader
		{
			get
			{
				return this._coffHeader;
			}
		}

		/// <summary>Gets the byte offset from the start of the PE image to the start of the COFF header.</summary>
		/// <returns>The byte offset from the start of the PE image to the start of the COFF header.</returns>
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00005A7C File Offset: 0x00003C7C
		public int CoffHeaderStartOffset
		{
			get
			{
				return this._coffHeaderStartOffset;
			}
		}

		/// <summary>Gets a value that indicates whether the image is Coff only.</summary>
		/// <returns>
		///   <see langword="true" /> if the image is Coff only; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00005A84 File Offset: 0x00003C84
		public bool IsCoffOnly
		{
			get
			{
				return this._peHeader == null;
			}
		}

		/// <summary>Gets the image's PE header.</summary>
		/// <returns>The image's PE header, or <see langword="null" /> if the image is COFF only.</returns>
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00005A8F File Offset: 0x00003C8F
		[Nullable(2)]
		public PEHeader PEHeader
		{
			[NullableContext(2)]
			get
			{
				return this._peHeader;
			}
		}

		/// <summary>Gets the byte offset of the header from the start of the image.</summary>
		/// <returns>The byte offset of the header from the start of the image.</returns>
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00005A97 File Offset: 0x00003C97
		public int PEHeaderStartOffset
		{
			get
			{
				return this._peHeaderStartOffset;
			}
		}

		/// <summary>Gets the PE section headers.</summary>
		/// <returns>An array containing the PE section headers.</returns>
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00005A9F File Offset: 0x00003C9F
		[Nullable(0)]
		public ImmutableArray<SectionHeader> SectionHeaders
		{
			[NullableContext(0)]
			get
			{
				return this._sectionHeaders;
			}
		}

		/// <summary>Gets the COR header.</summary>
		/// <returns>The COR header, or <see langword="null" /> if the image does not have one.</returns>
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00005AA7 File Offset: 0x00003CA7
		[Nullable(2)]
		public CorHeader CorHeader
		{
			[NullableContext(2)]
			get
			{
				return this._corHeader;
			}
		}

		/// <summary>Gets the byte offset from the start of the image to the COR header.</summary>
		/// <returns>The byte offset from the start of the image to the COR header, or -1 if the image does not have a COR header.</returns>
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00005AAF File Offset: 0x00003CAF
		public int CorHeaderStartOffset
		{
			get
			{
				return this._corHeaderStartOffset;
			}
		}

		/// <summary>Gets a value that indicates whether the image represents a Windows console application.</summary>
		/// <returns>
		///   <see langword="true" /> if the image is a Windows console applications; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00005AB7 File Offset: 0x00003CB7
		public bool IsConsoleApplication
		{
			get
			{
				return this._peHeader != null && this._peHeader.Subsystem == Subsystem.WindowsCui;
			}
		}

		/// <summary>Gets a value that indicates whether the image represents a dynamic link library.</summary>
		/// <returns>
		///   <see langword="true" /> if the image is a DLL; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00005AD1 File Offset: 0x00003CD1
		public bool IsDll
		{
			get
			{
				return (this._coffHeader.Characteristics & Characteristics.Dll) > (Characteristics)0;
			}
		}

		/// <summary>Gets a value that indicates whether the image represents an executable.</summary>
		/// <returns>
		///   <see langword="true" /> if the image is an executable; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00005AE7 File Offset: 0x00003CE7
		public bool IsExe
		{
			get
			{
				return (this._coffHeader.Characteristics & Characteristics.Dll) == (Characteristics)0;
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00005B00 File Offset: 0x00003D00
		private bool TryCalculateCorHeaderOffset(long peStreamSize, out int startOffset)
		{
			if (!this.TryGetDirectoryOffset(this._peHeader.CorHeaderTableDirectory, out startOffset, false))
			{
				startOffset = -1;
				return false;
			}
			int size = this._peHeader.CorHeaderTableDirectory.Size;
			if (size < 72)
			{
				throw new BadImageFormatException(SR.InvalidCorHeaderSize);
			}
			return true;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00005B4C File Offset: 0x00003D4C
		private void SkipDosHeader(ref PEBinaryReader reader, out bool isCOFFOnly)
		{
			ushort num = reader.ReadUInt16();
			if (num != 23117)
			{
				if (num == 0 && reader.ReadUInt16() == 65535)
				{
					throw new BadImageFormatException(SR.UnknownFileFormat);
				}
				isCOFFOnly = true;
				reader.Seek(0);
			}
			else
			{
				isCOFFOnly = false;
			}
			if (!isCOFFOnly)
			{
				reader.Seek(60);
				int num2 = reader.ReadInt32();
				reader.Seek(num2);
				uint num3 = reader.ReadUInt32();
				if (num3 != 17744U)
				{
					throw new BadImageFormatException(SR.InvalidPESignature);
				}
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00005BC8 File Offset: 0x00003DC8
		private ImmutableArray<SectionHeader> ReadSectionHeaders(ref PEBinaryReader reader)
		{
			int numberOfSections = (int)this._coffHeader.NumberOfSections;
			if (numberOfSections < 0)
			{
				throw new BadImageFormatException(SR.InvalidNumberOfSections);
			}
			ImmutableArray<SectionHeader>.Builder builder = ImmutableArray.CreateBuilder<SectionHeader>(numberOfSections);
			for (int i = 0; i < numberOfSections; i++)
			{
				builder.Add(new SectionHeader(ref reader));
			}
			return builder.MoveToImmutable();
		}

		/// <summary>Gets the offset (in bytes) from the start of the image to the given directory data.</summary>
		/// <param name="directory">The PE directory entry.</param>
		/// <param name="offset">When the method returns, contains the offset from the start of the image to the given directory data.</param>
		/// <returns>
		///   <see langword="true" /> if the directory data is found; <see langword="false" /> otherwise.</returns>
		// Token: 0x060001E2 RID: 482 RVA: 0x00005C15 File Offset: 0x00003E15
		public bool TryGetDirectoryOffset(DirectoryEntry directory, out int offset)
		{
			return this.TryGetDirectoryOffset(directory, out offset, true);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00005C20 File Offset: 0x00003E20
		internal bool TryGetDirectoryOffset(DirectoryEntry directory, out int offset, bool canCrossSectionBoundary)
		{
			int containingSectionIndex = this.GetContainingSectionIndex(directory.RelativeVirtualAddress);
			if (containingSectionIndex < 0)
			{
				offset = -1;
				return false;
			}
			int num = directory.RelativeVirtualAddress - this._sectionHeaders[containingSectionIndex].VirtualAddress;
			if (!canCrossSectionBoundary && directory.Size > this._sectionHeaders[containingSectionIndex].VirtualSize - num)
			{
				throw new BadImageFormatException(SR.SectionTooSmall);
			}
			offset = (this._isLoadedImage ? directory.RelativeVirtualAddress : (this._sectionHeaders[containingSectionIndex].PointerToRawData + num));
			return true;
		}

		/// <summary>Searches sections of the PE image for the section that contains the specified Relative Virtual Address.</summary>
		/// <param name="relativeVirtualAddress">The relative virtual address to search for.</param>
		/// <returns>The index of the section that contains <paramref name="relativeVirtualAddress" />, or -1 if there the search is unsuccessful.</returns>
		// Token: 0x060001E4 RID: 484 RVA: 0x00005CC0 File Offset: 0x00003EC0
		public int GetContainingSectionIndex(int relativeVirtualAddress)
		{
			for (int i = 0; i < this._sectionHeaders.Length; i++)
			{
				if (this._sectionHeaders[i].VirtualAddress <= relativeVirtualAddress && relativeVirtualAddress < this._sectionHeaders[i].VirtualAddress + this._sectionHeaders[i].VirtualSize)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00005D38 File Offset: 0x00003F38
		internal int IndexOfSection(string name)
		{
			for (int i = 0; i < this.SectionHeaders.Length; i++)
			{
				if (this.SectionHeaders[i].Name.Equals(name, 4))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00005D84 File Offset: 0x00003F84
		private void CalculateMetadataLocation(long peImageSize, out int start, out int size)
		{
			if (this.IsCoffOnly)
			{
				int num = this.IndexOfSection(".cormeta");
				if (num == -1)
				{
					start = -1;
					size = 0;
					return;
				}
				if (this._isLoadedImage)
				{
					start = this.SectionHeaders[num].VirtualAddress;
					size = this.SectionHeaders[num].VirtualSize;
				}
				else
				{
					start = this.SectionHeaders[num].PointerToRawData;
					size = this.SectionHeaders[num].SizeOfRawData;
				}
			}
			else
			{
				if (this._corHeader == null)
				{
					start = 0;
					size = 0;
					return;
				}
				if (!this.TryGetDirectoryOffset(this._corHeader.MetadataDirectory, out start, false))
				{
					throw new BadImageFormatException(SR.MissingDataDirectory);
				}
				size = this._corHeader.MetadataDirectory.Size;
			}
			if (start < 0 || (long)start >= peImageSize || size <= 0 || (long)start > peImageSize - (long)size)
			{
				throw new BadImageFormatException(SR.InvalidMetadataSectionSpan);
			}
		}

		// Token: 0x0400016D RID: 365
		private readonly CoffHeader _coffHeader;

		// Token: 0x0400016E RID: 366
		private readonly PEHeader _peHeader;

		// Token: 0x0400016F RID: 367
		private readonly ImmutableArray<SectionHeader> _sectionHeaders;

		// Token: 0x04000170 RID: 368
		private readonly CorHeader _corHeader;

		// Token: 0x04000171 RID: 369
		private readonly bool _isLoadedImage;

		// Token: 0x04000172 RID: 370
		private readonly int _metadataStartOffset = -1;

		// Token: 0x04000173 RID: 371
		private readonly int _metadataSize;

		// Token: 0x04000174 RID: 372
		private readonly int _coffHeaderStartOffset = -1;

		// Token: 0x04000175 RID: 373
		private readonly int _corHeaderStartOffset = -1;

		// Token: 0x04000176 RID: 374
		private readonly int _peHeaderStartOffset = -1;

		// Token: 0x04000177 RID: 375
		internal const ushort DosSignature = 23117;

		// Token: 0x04000178 RID: 376
		internal const int PESignatureOffsetLocation = 60;

		// Token: 0x04000179 RID: 377
		internal const uint PESignature = 17744U;

		// Token: 0x0400017A RID: 378
		internal const int PESignatureSize = 4;
	}
}
