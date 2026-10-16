using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.PortableExecutable
{
	/// <summary>Provides information about the section header of a PE/COFF file.</summary>
	// Token: 0x0200003B RID: 59
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct SectionHeader
	{
		/// <summary>Gets the name of the section.</summary>
		/// <returns>The name of the section.</returns>
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00006D41 File Offset: 0x00004F41
		public string Name { get; }

		/// <summary>Gets the total size of the section when loaded into memory.</summary>
		/// <returns>The total size of the section when loaded into memory.</returns>
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00006D49 File Offset: 0x00004F49
		public int VirtualSize { get; }

		/// <summary>Gets the virtual addess of the section.</summary>
		/// <returns>The virtual address of the section.</returns>
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00006D51 File Offset: 0x00004F51
		public int VirtualAddress { get; }

		/// <summary>Gets the size of the section (for object files) or the size of the initialized data on disk (for image files).</summary>
		/// <returns>The size of the section (for object files) or the size of the initialized data on disk (for image files).</returns>
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00006D59 File Offset: 0x00004F59
		public int SizeOfRawData { get; }

		/// <summary>Gets the file pointer to the first page of the section within the COFF file.</summary>
		/// <returns>The file pointer to the first page of the section within the COFF file.</returns>
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00006D61 File Offset: 0x00004F61
		public int PointerToRawData { get; }

		/// <summary>Gets the file pointer to the beginning of relocation entries for the section.</summary>
		/// <returns>The file pointer to the beginning of relocation entries for the section. It is set to zero for PE images or if there are no relocations.</returns>
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00006D69 File Offset: 0x00004F69
		public int PointerToRelocations { get; }

		/// <summary>Gets the file pointer to the beginning of line-number entries for the section.</summary>
		/// <returns>The file pointer to the beginning of line-number entries for the section, or zero if there are no COFF line numbers.</returns>
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00006D71 File Offset: 0x00004F71
		public int PointerToLineNumbers { get; }

		/// <summary>Gets the number of relocation entries for the section.</summary>
		/// <returns>The number of relocation entries for the section. Its value is zero for PE images.</returns>
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00006D79 File Offset: 0x00004F79
		public ushort NumberOfRelocations { get; }

		/// <summary>Gets the number of line-number entries for the section.</summary>
		/// <returns>The number of line-number entries for the section.</returns>
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00006D81 File Offset: 0x00004F81
		public ushort NumberOfLineNumbers { get; }

		/// <summary>Gets the flags that describe the characteristics of the section.</summary>
		/// <returns>The flags that describe the characteristics of the section.</returns>
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00006D89 File Offset: 0x00004F89
		public SectionCharacteristics SectionCharacteristics { get; }

		// Token: 0x0600021C RID: 540 RVA: 0x00006D94 File Offset: 0x00004F94
		internal SectionHeader(ref PEBinaryReader reader)
		{
			this.Name = reader.ReadNullPaddedUTF8(8);
			this.VirtualSize = reader.ReadInt32();
			this.VirtualAddress = reader.ReadInt32();
			this.SizeOfRawData = reader.ReadInt32();
			this.PointerToRawData = reader.ReadInt32();
			this.PointerToRelocations = reader.ReadInt32();
			this.PointerToLineNumbers = reader.ReadInt32();
			this.NumberOfRelocations = reader.ReadUInt16();
			this.NumberOfLineNumbers = reader.ReadUInt16();
			this.SectionCharacteristics = (SectionCharacteristics)reader.ReadUInt32();
		}

		// Token: 0x04000193 RID: 403
		internal const int NameSize = 8;

		// Token: 0x04000194 RID: 404
		internal const int Size = 40;
	}
}
