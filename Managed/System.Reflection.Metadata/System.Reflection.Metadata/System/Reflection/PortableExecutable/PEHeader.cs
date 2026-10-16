using System;

namespace System.Reflection.PortableExecutable
{
	/// <summary>Represents the Portable Executable (PE) file header.</summary>
	// Token: 0x02000035 RID: 53
	public sealed class PEHeader
	{
		/// <summary>Gets a value that identifies the format of the image file.</summary>
		/// <returns>The format of the image file.</returns>
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00005526 File Offset: 0x00003726
		public PEMagic Magic { get; }

		/// <summary>Gets the linker major version number.</summary>
		/// <returns>The linker major version number.</returns>
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000552E File Offset: 0x0000372E
		public byte MajorLinkerVersion { get; }

		/// <summary>Gets the linker minor version number.</summary>
		/// <returns>The linker minor version number.</returns>
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00005536 File Offset: 0x00003736
		public byte MinorLinkerVersion { get; }

		/// <summary>Gets the size of the code (text) section, or the sum of all code sections if there are multiple sections.</summary>
		/// <returns>the size of the code (text) section, or the sum of all code sections if there are multiple sections.</returns>
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000553E File Offset: 0x0000373E
		public int SizeOfCode { get; }

		/// <summary>Gets the size of the initialized data section, or the sum of all such sections if there are multiple data sections.</summary>
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00005546 File Offset: 0x00003746
		public int SizeOfInitializedData { get; }

		/// <summary>Gets the size of the uninitialized data section (BSS), or the sum of all such sections if there are multiple BSS sections.</summary>
		/// <returns>The size of the uninitialized data section (BSS) or the sum of all such sections.</returns>
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000554E File Offset: 0x0000374E
		public int SizeOfUninitializedData { get; }

		/// <summary>Gets the address of the entry point relative to the image base when the PE file is loaded into memory.</summary>
		/// <returns>The address of the entry point relative to the image base.</returns>
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00005556 File Offset: 0x00003756
		public int AddressOfEntryPoint { get; }

		/// <summary>Gets the address of the beginning-of-code section relative to the image base when the image is loaded into memory.</summary>
		/// <returns>The address of the beginning-of-code section relative to the image base.</returns>
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000555E File Offset: 0x0000375E
		public int BaseOfCode { get; }

		/// <summary>Gets the address of the beginning-of-data section relative to the image base when the image is loaded into memory.</summary>
		/// <returns>The address of the beginning-of-data section relative to the image base.</returns>
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00005566 File Offset: 0x00003766
		public int BaseOfData { get; }

		/// <summary>Gets the preferred address of the first byte of the image when it is loaded into memory.</summary>
		/// <returns>The preferred address, which is a multiple of 64K.</returns>
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000556E File Offset: 0x0000376E
		public ulong ImageBase { get; }

		/// <summary>Gets the alignment (in bytes) of sections when they are loaded into memory.</summary>
		/// <returns>A number greater than or equal to <see cref="P:System.Reflection.PortableExecutable.PEHeader.FileAlignment" />. The default is the page size for the architecture.</returns>
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00005576 File Offset: 0x00003776
		public int SectionAlignment { get; }

		/// <summary>Gets the alignment factor (in bytes) that is used to align the raw data of sections in the image file.</summary>
		/// <returns>A power of 2 between 512 and 64K, inclusive. The default is 512.</returns>
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000557E File Offset: 0x0000377E
		public int FileAlignment { get; }

		/// <summary>Gets the major version number of the required operating system.</summary>
		/// <returns>The major version number of the required operating system.</returns>
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00005586 File Offset: 0x00003786
		public ushort MajorOperatingSystemVersion { get; }

		/// <summary>Gets the minor version number of the required operating system.</summary>
		/// <returns>The minor version number of the required operating system.</returns>
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000558E File Offset: 0x0000378E
		public ushort MinorOperatingSystemVersion { get; }

		/// <summary>Gets the major version number of the image.</summary>
		/// <returns>The major version number of the image.</returns>
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00005596 File Offset: 0x00003796
		public ushort MajorImageVersion { get; }

		/// <summary>Gets the minor version number of the image.</summary>
		/// <returns>The minor version number of the image.</returns>
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000559E File Offset: 0x0000379E
		public ushort MinorImageVersion { get; }

		/// <summary>Gets the major version number of the subsystem.</summary>
		/// <returns>The major version number of the subsystem.</returns>
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x000055A6 File Offset: 0x000037A6
		public ushort MajorSubsystemVersion { get; }

		/// <summary>Gets the minor version number of the subsystem.</summary>
		/// <returns>The minor version number of the subsystem.</returns>
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x000055AE File Offset: 0x000037AE
		public ushort MinorSubsystemVersion { get; }

		/// <summary>Gets the size (in bytes) of the image, including all headers, as the image is loaded in memory.</summary>
		/// <returns>The size (in bytes) of the image, which is a multiple of <see cref="P:System.Reflection.PortableExecutable.PEHeader.SectionAlignment" />.</returns>
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x000055B6 File Offset: 0x000037B6
		public int SizeOfImage { get; }

		/// <summary>Gets the combined size of an MS DOS stub, PE header, and section headers rounded up to a multiple of FileAlignment.</summary>
		/// <returns>The combined size of an MS DOS stub, PE header, and section headers rounded up to a multiple of FileAlignment.</returns>
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x000055BE File Offset: 0x000037BE
		public int SizeOfHeaders { get; }

		/// <summary>Gets the image file checksum.</summary>
		/// <returns>The image file checksum.</returns>
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x000055C6 File Offset: 0x000037C6
		public uint CheckSum { get; }

		/// <summary>Gets the name of the subsystem that is required to run this image.</summary>
		/// <returns>The name of the subsystem that is required to run this image.</returns>
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x000055CE File Offset: 0x000037CE
		public Subsystem Subsystem { get; }

		/// <summary>Gets the characteristics of a dynamic link library.</summary>
		/// <returns>A bitwise combination of flags that represents the characteristics of a dynamic link library.</returns>
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x000055D6 File Offset: 0x000037D6
		public DllCharacteristics DllCharacteristics { get; }

		/// <summary>Gets the size of the stack to reserve. Only <see cref="P:System.Reflection.PortableExecutable.PEHeader.SizeOfStackCommit" /> is committed; the rest is made available one page at a time until the reserve size is reached.</summary>
		/// <returns>The size of the stack to reserve.</returns>
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x000055DE File Offset: 0x000037DE
		public ulong SizeOfStackReserve { get; }

		/// <summary>Gets the size of the stack to commit.</summary>
		/// <returns>The size of the stack to commit.</returns>
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060001BA RID: 442 RVA: 0x000055E6 File Offset: 0x000037E6
		public ulong SizeOfStackCommit { get; }

		/// <summary>Gets the size of the local heap space to reserve. Only <see cref="P:System.Reflection.PortableExecutable.PEHeader.SizeOfHeapCommit" /> is committed; the rest is made available one page at a time until the reserve size is reached.</summary>
		/// <returns>The size of the local heap space to reserve.</returns>
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060001BB RID: 443 RVA: 0x000055EE File Offset: 0x000037EE
		public ulong SizeOfHeapReserve { get; }

		/// <summary>Gets the size of the local heap space to commit.</summary>
		/// <returns>the size of the local heap space to commit.</returns>
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060001BC RID: 444 RVA: 0x000055F6 File Offset: 0x000037F6
		public ulong SizeOfHeapCommit { get; }

		/// <summary>Gets the number of data-directory entries in the remainder of the <see cref="T:System.Reflection.PortableExecutable.PEHeader" />. Each describes a location and size.</summary>
		/// <returns>The number of data-directory entries in the remainder of the <see cref="T:System.Reflection.PortableExecutable.PEHeader" />.</returns>
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060001BD RID: 445 RVA: 0x000055FE File Offset: 0x000037FE
		public int NumberOfRvaAndSizes { get; }

		/// <summary>Gets the Export Table entry.</summary>
		/// <returns>The Export Table entry.</returns>
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00005606 File Offset: 0x00003806
		public DirectoryEntry ExportTableDirectory { get; }

		/// <summary>Gets the Import Table entry.</summary>
		/// <returns>The Import Table entry.</returns>
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0000560E File Offset: 0x0000380E
		public DirectoryEntry ImportTableDirectory { get; }

		/// <summary>Gets the Resource Table entry.</summary>
		/// <returns>The Resource Table entry.</returns>
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00005616 File Offset: 0x00003816
		public DirectoryEntry ResourceTableDirectory { get; }

		/// <summary>Gets the Exception Table entry.</summary>
		/// <returns>The Exception Table entry.</returns>
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000561E File Offset: 0x0000381E
		public DirectoryEntry ExceptionTableDirectory { get; }

		/// <summary>Gets the Certificate Table entry, which points to a table of attribute certificates.</summary>
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00005626 File Offset: 0x00003826
		public DirectoryEntry CertificateTableDirectory { get; }

		/// <summary>Gets the Base Relocations Table entry.</summary>
		/// <returns>The Base Relocations Table entry.</returns>
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000562E File Offset: 0x0000382E
		public DirectoryEntry BaseRelocationTableDirectory { get; }

		/// <summary>Gets the Debug Table entry.</summary>
		/// <returns>The Debug Table entry.</returns>
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00005636 File Offset: 0x00003836
		public DirectoryEntry DebugTableDirectory { get; }

		/// <summary>Gets the Copyright Table entry.</summary>
		/// <returns>The Copyright Table entry.</returns>
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000563E File Offset: 0x0000383E
		public DirectoryEntry CopyrightTableDirectory { get; }

		/// <summary>Gets the Global Pointer Table entry.</summary>
		/// <returns>The Global Pointer Table entry.</returns>
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00005646 File Offset: 0x00003846
		public DirectoryEntry GlobalPointerTableDirectory { get; }

		/// <summary>Gets the Thread-Local Storage Table entry.</summary>
		/// <returns>The Thread-Local Storage Table entry.</returns>
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000564E File Offset: 0x0000384E
		public DirectoryEntry ThreadLocalStorageTableDirectory { get; }

		/// <summary>Gets the Load Configuration Table entry.</summary>
		/// <returns>The Load Configuration Table entry.</returns>
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00005656 File Offset: 0x00003856
		public DirectoryEntry LoadConfigTableDirectory { get; }

		/// <summary>Gets the Bound Import Table entry.</summary>
		/// <returns>The Bound Import Table entry.</returns>
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000565E File Offset: 0x0000385E
		public DirectoryEntry BoundImportTableDirectory { get; }

		/// <summary>Gets the Import Address Table entry.</summary>
		/// <returns>The Import Address Table entry.</returns>
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00005666 File Offset: 0x00003866
		public DirectoryEntry ImportAddressTableDirectory { get; }

		/// <summary>Gets the Delay-Load Import Table entry.</summary>
		/// <returns>The Delay-Load Import Table entry.</returns>
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000566E File Offset: 0x0000386E
		public DirectoryEntry DelayImportTableDirectory { get; }

		/// <summary>Gets the CLI Header Table entry.</summary>
		/// <returns>The CLI Header Table entry.</returns>
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00005676 File Offset: 0x00003876
		public DirectoryEntry CorHeaderTableDirectory { get; }

		// Token: 0x060001CD RID: 461 RVA: 0x0000567E File Offset: 0x0000387E
		internal static int Size(bool is32Bit)
		{
			return 72 + 4 * (is32Bit ? 4 : 8) + 4 + 4 + 128;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00005698 File Offset: 0x00003898
		internal PEHeader(ref PEBinaryReader reader)
		{
			PEMagic pemagic = (PEMagic)reader.ReadUInt16();
			if (pemagic != PEMagic.PE32 && pemagic != PEMagic.PE32Plus)
			{
				throw new BadImageFormatException(SR.UnknownPEMagicValue);
			}
			this.Magic = pemagic;
			this.MajorLinkerVersion = reader.ReadByte();
			this.MinorLinkerVersion = reader.ReadByte();
			this.SizeOfCode = reader.ReadInt32();
			this.SizeOfInitializedData = reader.ReadInt32();
			this.SizeOfUninitializedData = reader.ReadInt32();
			this.AddressOfEntryPoint = reader.ReadInt32();
			this.BaseOfCode = reader.ReadInt32();
			if (pemagic == PEMagic.PE32Plus)
			{
				this.BaseOfData = 0;
			}
			else
			{
				this.BaseOfData = reader.ReadInt32();
			}
			if (pemagic == PEMagic.PE32Plus)
			{
				this.ImageBase = reader.ReadUInt64();
			}
			else
			{
				this.ImageBase = (ulong)reader.ReadUInt32();
			}
			this.SectionAlignment = reader.ReadInt32();
			this.FileAlignment = reader.ReadInt32();
			this.MajorOperatingSystemVersion = reader.ReadUInt16();
			this.MinorOperatingSystemVersion = reader.ReadUInt16();
			this.MajorImageVersion = reader.ReadUInt16();
			this.MinorImageVersion = reader.ReadUInt16();
			this.MajorSubsystemVersion = reader.ReadUInt16();
			this.MinorSubsystemVersion = reader.ReadUInt16();
			reader.ReadUInt32();
			this.SizeOfImage = reader.ReadInt32();
			this.SizeOfHeaders = reader.ReadInt32();
			this.CheckSum = reader.ReadUInt32();
			this.Subsystem = (Subsystem)reader.ReadUInt16();
			this.DllCharacteristics = (DllCharacteristics)reader.ReadUInt16();
			if (pemagic == PEMagic.PE32Plus)
			{
				this.SizeOfStackReserve = reader.ReadUInt64();
				this.SizeOfStackCommit = reader.ReadUInt64();
				this.SizeOfHeapReserve = reader.ReadUInt64();
				this.SizeOfHeapCommit = reader.ReadUInt64();
			}
			else
			{
				this.SizeOfStackReserve = (ulong)reader.ReadUInt32();
				this.SizeOfStackCommit = (ulong)reader.ReadUInt32();
				this.SizeOfHeapReserve = (ulong)reader.ReadUInt32();
				this.SizeOfHeapCommit = (ulong)reader.ReadUInt32();
			}
			reader.ReadUInt32();
			this.NumberOfRvaAndSizes = reader.ReadInt32();
			this.ExportTableDirectory = new DirectoryEntry(ref reader);
			this.ImportTableDirectory = new DirectoryEntry(ref reader);
			this.ResourceTableDirectory = new DirectoryEntry(ref reader);
			this.ExceptionTableDirectory = new DirectoryEntry(ref reader);
			this.CertificateTableDirectory = new DirectoryEntry(ref reader);
			this.BaseRelocationTableDirectory = new DirectoryEntry(ref reader);
			this.DebugTableDirectory = new DirectoryEntry(ref reader);
			this.CopyrightTableDirectory = new DirectoryEntry(ref reader);
			this.GlobalPointerTableDirectory = new DirectoryEntry(ref reader);
			this.ThreadLocalStorageTableDirectory = new DirectoryEntry(ref reader);
			this.LoadConfigTableDirectory = new DirectoryEntry(ref reader);
			this.BoundImportTableDirectory = new DirectoryEntry(ref reader);
			this.ImportAddressTableDirectory = new DirectoryEntry(ref reader);
			this.DelayImportTableDirectory = new DirectoryEntry(ref reader);
			this.CorHeaderTableDirectory = new DirectoryEntry(ref reader);
			new DirectoryEntry(ref reader);
		}

		// Token: 0x0400016C RID: 364
		internal const int OffsetOfChecksum = 64;
	}
}
