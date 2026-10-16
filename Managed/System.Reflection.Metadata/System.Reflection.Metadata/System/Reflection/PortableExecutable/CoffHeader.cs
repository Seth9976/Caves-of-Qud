using System;

namespace System.Reflection.PortableExecutable
{
	/// <summary>Represents the header of a COFF file.</summary>
	// Token: 0x02000027 RID: 39
	public sealed class CoffHeader
	{
		/// <summary>Gets the type of the target machine.</summary>
		/// <returns>The type of the target machine.</returns>
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000513A File Offset: 0x0000333A
		public Machine Machine { get; }

		/// <summary>Gets the number of sections. This indicates the size of the section table, which immediately follows the headers.</summary>
		/// <returns>The number of sections.</returns>
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00005142 File Offset: 0x00003342
		public short NumberOfSections { get; }

		/// <summary>Gets a value that indicates when the file was created.</summary>
		/// <returns>The low 32 bits of the number of seconds since 00:00 January 1, 1970, which indicates when the file was created.</returns>
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000174 RID: 372 RVA: 0x0000514A File Offset: 0x0000334A
		public int TimeDateStamp { get; }

		/// <summary>Gets the file pointer to the COFF symbol table.</summary>
		/// <returns>The file pointer to the COFF symbol table, or zero if no COFF symbol table is present. This value should be zero for a PE image.</returns>
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00005152 File Offset: 0x00003352
		public int PointerToSymbolTable { get; }

		/// <summary>Gets the number of entries in the symbol table. This data can be used to locate the string table, which immediately follows the symbol table. This value should be zero for a PE image.</summary>
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000176 RID: 374 RVA: 0x0000515A File Offset: 0x0000335A
		public int NumberOfSymbols { get; }

		/// <summary>Gets the size of the optional header, which is required for executable files but not for object files. This value should be zero for an object file.</summary>
		/// <returns>The size of the optional header.</returns>
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00005162 File Offset: 0x00003362
		public short SizeOfOptionalHeader { get; }

		/// <summary>Gets the flags that indicate the attributes of the file.</summary>
		/// <returns>The flags that indicate the attributes of the file.</returns>
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000178 RID: 376 RVA: 0x0000516A File Offset: 0x0000336A
		public Characteristics Characteristics { get; }

		// Token: 0x06000179 RID: 377 RVA: 0x00005174 File Offset: 0x00003374
		internal CoffHeader(ref PEBinaryReader reader)
		{
			this.Machine = (Machine)reader.ReadUInt16();
			this.NumberOfSections = reader.ReadInt16();
			this.TimeDateStamp = reader.ReadInt32();
			this.PointerToSymbolTable = reader.ReadInt32();
			this.NumberOfSymbols = reader.ReadInt32();
			this.SizeOfOptionalHeader = reader.ReadInt16();
			this.Characteristics = (Characteristics)reader.ReadUInt16();
		}

		// Token: 0x0400009D RID: 157
		internal const int Size = 20;
	}
}
