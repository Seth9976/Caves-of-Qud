using System;
using System.Reflection.Internal;
using System.Runtime.CompilerServices;

namespace System.Reflection.PortableExecutable
{
	/// <summary>Defines the header for a portable executable (PE) file.</summary>
	// Token: 0x02000024 RID: 36
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PEHeaderBuilder
	{
		/// <summary>The target machine's CPU architecture.</summary>
		/// <returns>One of the enumeration values representing the different CPU architectures.</returns>
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00004EAF File Offset: 0x000030AF
		public Machine Machine { get; }

		/// <summary>Returns the image characteristics.</summary>
		/// <returns>An object representing the image characteristics.</returns>
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00004EB7 File Offset: 0x000030B7
		public Characteristics ImageCharacteristics { get; }

		/// <summary>The linker major version number.</summary>
		/// <returns>A number the size of a <see cref="T:System.Byte" /> representing the linker major version number.</returns>
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00004EBF File Offset: 0x000030BF
		public byte MajorLinkerVersion { get; }

		/// <summary>The linker minor version number.</summary>
		/// <returns>A number the size of a <see cref="T:System.Byte" /> representing the linker minor version number.</returns>
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00004EC7 File Offset: 0x000030C7
		public byte MinorLinkerVersion { get; }

		/// <summary>The preferred address of the first byte of image when loaded into memory; must be a multiple of 64K.</summary>
		/// <returns>A number representing the preferred address of the first byte of image when loaded into memory.</returns>
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00004ECF File Offset: 0x000030CF
		public ulong ImageBase { get; }

		/// <summary>The alignment (in bytes) of sections when they are loaded into memory.</summary>
		/// <returns>A number representing the alignment (in bytes) of sections when they are loaded into memory. It must be greater than or equal to the file alignment. The default is the page size for the architecture.</returns>
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00004ED7 File Offset: 0x000030D7
		public int SectionAlignment { get; }

		/// <summary>The alignment factor (in bytes) that is used to align the raw data of sections in the image file. The value should be a power of 2 between 512 and 64K, inclusive. The default is 512. If the section alignment is less than the architecture's page size, then file alignment must match the section alignment.</summary>
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00004EDF File Offset: 0x000030DF
		public int FileAlignment { get; }

		/// <summary>The major version number of the required operating system.</summary>
		/// <returns>A number the size of a <see cref="T:System.UInt16" /> representing the major version number of the required operating system.</returns>
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00004EE7 File Offset: 0x000030E7
		public ushort MajorOperatingSystemVersion { get; }

		/// <summary>The minor version number of the required operating system.</summary>
		/// <returns>A number the size of a <see cref="T:System.UInt16" /> representing the minor version number of the required operating system.</returns>
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00004EEF File Offset: 0x000030EF
		public ushort MinorOperatingSystemVersion { get; }

		/// <summary>The major version number of the image.</summary>
		/// <returns>A number the size of a <see cref="T:System.UInt16" /> representing the major version number of the image.</returns>
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00004EF7 File Offset: 0x000030F7
		public ushort MajorImageVersion { get; }

		/// <summary>The minor version number of the image.</summary>
		/// <returns>A number the size of a <see cref="T:System.UInt16" /> representing the minor version number of the image.</returns>
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00004EFF File Offset: 0x000030FF
		public ushort MinorImageVersion { get; }

		/// <summary>The major version number of the subsystem.</summary>
		/// <returns>A number the size of a <see cref="T:System.UInt16" /> representing the major version number of the subsystem.</returns>
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00004F07 File Offset: 0x00003107
		public ushort MajorSubsystemVersion { get; }

		/// <summary>The minor version number of the subsystem.</summary>
		/// <returns>A number the size of a <see cref="T:System.UInt16" /> representing the minor version number of the subsystem.</returns>
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00004F0F File Offset: 0x0000310F
		public ushort MinorSubsystemVersion { get; }

		/// <summary>The subsystem that is required to run this image.</summary>
		/// <returns>A <see cref="T:System.Reflection.PortableExecutable.Subsystem" /> instance.</returns>
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00004F17 File Offset: 0x00003117
		public Subsystem Subsystem { get; }

		/// <summary>Returns the dynamic linker library characteristics.</summary>
		/// <returns>An object that describes the dynamic linker library characteristics.</returns>
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00004F1F File Offset: 0x0000311F
		public DllCharacteristics DllCharacteristics { get; }

		/// <summary>The size of the stack to reserve. Only <see cref="P:System.Reflection.PortableExecutable.PEHeaderBuilder.SizeOfStackCommit" /> is committed; the rest is made available one page at a time until the reserve size is reached.</summary>
		/// <returns>A number representing the size of the stack to reserve.</returns>
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00004F27 File Offset: 0x00003127
		public ulong SizeOfStackReserve { get; }

		/// <summary>The size of the stack to commit.</summary>
		/// <returns>A number representing the size of the stack to commit.</returns>
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00004F2F File Offset: 0x0000312F
		public ulong SizeOfStackCommit { get; }

		/// <summary>The size of the local heap space to reserve. Only <see cref="P:System.Reflection.PortableExecutable.PEHeaderBuilder.SizeOfHeapCommit" /> is committed; the rest is made available one page at a time until the reserve size is reached.</summary>
		/// <returns>A number representing the size of the local heap space to reserve.</returns>
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00004F37 File Offset: 0x00003137
		public ulong SizeOfHeapReserve { get; }

		/// <summary>The size of the local heap space to commit.</summary>
		/// <returns>A number representing the size of the local heap space to commit.</returns>
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00004F3F File Offset: 0x0000313F
		public ulong SizeOfHeapCommit { get; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.PortableExecutable.PEHeader" /> class.</summary>
		/// <param name="machine">The target machine's CPU architecture.</param>
		/// <param name="sectionAlignment">The alignment (in bytes) of sections when they are loaded into memory. It must be greater than or equal to <paramref name="fileAlignment" />. The default is the page size for the architecture.</param>
		/// <param name="fileAlignment">The alignment factor (in bytes) that is used to align the raw data of sections in the image file. The value should be a power of 2 between 512 and 64K, inclusive. The default is 512. If the <paramref name="sectionAlignment" /> is less than the architecture's page size, then <paramref name="fileAlignment" /> must match <paramref name="sectionAlignment" />.</param>
		/// <param name="imageBase">The preferred address of the first byte of image when loaded into memory; must be a multiple of 64K.</param>
		/// <param name="majorLinkerVersion">The linker major version number.</param>
		/// <param name="minorLinkerVersion">The linker minor version number.</param>
		/// <param name="majorOperatingSystemVersion">The major version number of the required operating system.</param>
		/// <param name="minorOperatingSystemVersion">The minor version number of the required operating system.</param>
		/// <param name="majorImageVersion">The major version number of the image.</param>
		/// <param name="minorImageVersion">The minor version number of the image.</param>
		/// <param name="majorSubsystemVersion">The major version number of the subsystem.</param>
		/// <param name="minorSubsystemVersion">The minor version number of the subsystem.</param>
		/// <param name="subsystem">The subsystem required to run the image.</param>
		/// <param name="dllCharacteristics">An object describing the characteristics of the dynamic link library.</param>
		/// <param name="imageCharacteristics">An object describing the characteristics of the image.</param>
		/// <param name="sizeOfStackReserve">The size of the stack to reserve. Only <paramref name="sizeOfStackCommit" /> is committed; the rest is made available one page at a time until the reserve size is reached.</param>
		/// <param name="sizeOfStackCommit">The size of the stack to commit.</param>
		/// <param name="sizeOfHeapReserve">The size of the local heap space to reserve. Only <paramref name="sizeOfHeapCommit" /> is committed; the rest is made available one page at a time until the reserve size is reached.</param>
		/// <param name="sizeOfHeapCommit">The size of the local heap space to commit.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="fileAlignment" /> is not power of 2 between 512 and 64K.
		/// -or-
		///
		/// <paramref name="sectionAlignment" /> not power of 2.
		///
		/// -or-
		///
		/// <paramref name="sectionAlignment" /> is less than <paramref name="fileAlignment" />.</exception>
		// Token: 0x06000168 RID: 360 RVA: 0x00004F48 File Offset: 0x00003148
		public PEHeaderBuilder(Machine machine = Machine.Unknown, int sectionAlignment = 8192, int fileAlignment = 512, ulong imageBase = 4194304UL, byte majorLinkerVersion = 48, byte minorLinkerVersion = 0, ushort majorOperatingSystemVersion = 4, ushort minorOperatingSystemVersion = 0, ushort majorImageVersion = 0, ushort minorImageVersion = 0, ushort majorSubsystemVersion = 4, ushort minorSubsystemVersion = 0, Subsystem subsystem = Subsystem.WindowsCui, DllCharacteristics dllCharacteristics = DllCharacteristics.DynamicBase | DllCharacteristics.NxCompatible | DllCharacteristics.NoSeh | DllCharacteristics.TerminalServerAware, Characteristics imageCharacteristics = Characteristics.Dll, ulong sizeOfStackReserve = 1048576UL, ulong sizeOfStackCommit = 4096UL, ulong sizeOfHeapReserve = 1048576UL, ulong sizeOfHeapCommit = 4096UL)
		{
			if (fileAlignment < 512 || fileAlignment > 65536 || BitArithmetic.CountBits(fileAlignment) != 1)
			{
				Throw.ArgumentOutOfRange("fileAlignment");
			}
			if (sectionAlignment < fileAlignment || BitArithmetic.CountBits(sectionAlignment) != 1)
			{
				Throw.ArgumentOutOfRange("sectionAlignment");
			}
			this.Machine = machine;
			this.SectionAlignment = sectionAlignment;
			this.FileAlignment = fileAlignment;
			this.ImageBase = imageBase;
			this.MajorLinkerVersion = majorLinkerVersion;
			this.MinorLinkerVersion = minorLinkerVersion;
			this.MajorOperatingSystemVersion = majorOperatingSystemVersion;
			this.MinorOperatingSystemVersion = minorOperatingSystemVersion;
			this.MajorImageVersion = majorImageVersion;
			this.MinorImageVersion = minorImageVersion;
			this.MajorSubsystemVersion = majorSubsystemVersion;
			this.MinorSubsystemVersion = minorSubsystemVersion;
			this.Subsystem = subsystem;
			this.DllCharacteristics = dllCharacteristics;
			this.ImageCharacteristics = imageCharacteristics;
			this.SizeOfStackReserve = sizeOfStackReserve;
			this.SizeOfStackCommit = sizeOfStackCommit;
			this.SizeOfHeapReserve = sizeOfHeapReserve;
			this.SizeOfHeapCommit = sizeOfHeapCommit;
		}

		/// <summary>Creates an executable header.</summary>
		/// <returns>A <see cref="T:System.Reflection.PortableExecutable.PEHeaderBuilder" /> instance representing the executable header.</returns>
		// Token: 0x06000169 RID: 361 RVA: 0x0000502C File Offset: 0x0000322C
		public static PEHeaderBuilder CreateExecutableHeader()
		{
			return new PEHeaderBuilder(Machine.Unknown, 8192, 512, 4194304UL, 48, 0, 4, 0, 0, 0, 4, 0, Subsystem.WindowsCui, DllCharacteristics.DynamicBase | DllCharacteristics.NxCompatible | DllCharacteristics.NoSeh | DllCharacteristics.TerminalServerAware, Characteristics.ExecutableImage, 1048576UL, 4096UL, 1048576UL, 4096UL);
		}

		/// <summary>Creates a library header.</summary>
		/// <returns>A <see cref="T:System.Reflection.PortableExecutable.PEHeaderBuilder" /> instance representing the library header.</returns>
		// Token: 0x0600016A RID: 362 RVA: 0x00005078 File Offset: 0x00003278
		public static PEHeaderBuilder CreateLibraryHeader()
		{
			return new PEHeaderBuilder(Machine.Unknown, 8192, 512, 4194304UL, 48, 0, 4, 0, 0, 0, 4, 0, Subsystem.WindowsCui, DllCharacteristics.DynamicBase | DllCharacteristics.NxCompatible | DllCharacteristics.NoSeh | DllCharacteristics.TerminalServerAware, Characteristics.ExecutableImage | Characteristics.Dll, 1048576UL, 4096UL, 1048576UL, 4096UL);
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600016B RID: 363 RVA: 0x000050C7 File Offset: 0x000032C7
		internal bool Is32Bit
		{
			get
			{
				return this.Machine != Machine.Amd64 && this.Machine != Machine.IA64 && this.Machine != Machine.Arm64;
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000050F5 File Offset: 0x000032F5
		internal int ComputeSizeOfPEHeaders(int sectionCount)
		{
			return PEBuilder.DosHeaderSize + 4 + 20 + PEHeader.Size(this.Is32Bit) + 40 * sectionCount;
		}
	}
}
