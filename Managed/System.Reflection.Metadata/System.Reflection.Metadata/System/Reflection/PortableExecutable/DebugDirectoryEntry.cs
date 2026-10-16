using System;

namespace System.Reflection.PortableExecutable
{
	/// <summary>Identifies the location, size and format of a block of debug information.</summary>
	// Token: 0x0200002B RID: 43
	public readonly struct DebugDirectoryEntry
	{
		/// <summary>Get the time and date that the debug data was created if the PE/COFF file is not deterministic; otherwise, gets a value based on the hash of the content.</summary>
		/// <returns>for a non-deterministic PE/COFF file, the time and date that the debug data was created; otherwise, a value based on the hash of the content.</returns>
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00005301 File Offset: 0x00003501
		public uint Stamp { get; }

		/// <summary>Gets the major version number of the debug data format.</summary>
		/// <returns>The major version number of the debug data format.</returns>
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00005309 File Offset: 0x00003509
		public ushort MajorVersion { get; }

		/// <summary>Gets the minor version number of the debug data format.</summary>
		/// <returns>The minor version number of the debug data format.</returns>
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00005311 File Offset: 0x00003511
		public ushort MinorVersion { get; }

		/// <summary>Gets the format of the debugging information.</summary>
		/// <returns>The format of the debugging information.</returns>
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00005319 File Offset: 0x00003519
		public DebugDirectoryEntryType Type { get; }

		/// <summary>Gets the size of the debug data (not including the debug directory itself).</summary>
		/// <returns>the size of the debug data (excluding the debug directory).</returns>
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00005321 File Offset: 0x00003521
		public int DataSize { get; }

		/// <summary>Gets the address of the debug data when loaded, relative to the image base.</summary>
		/// <returns>The address of the debug data relative to the image base.</returns>
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00005329 File Offset: 0x00003529
		public int DataRelativeVirtualAddress { get; }

		/// <summary>Gets the file pointer to the debug data.</summary>
		/// <returns>The file pointer to the debug data.</returns>
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00005331 File Offset: 0x00003531
		public int DataPointer { get; }

		/// <summary>Gets a value that indicates if the entry is a <see cref="F:System.Reflection.PortableExecutable.DebugDirectoryEntryType.CodeView" /> entry that points to a Portable PDB.</summary>
		/// <returns>
		///   <see langword="true" /> if the entry is a <see cref="F:System.Reflection.PortableExecutable.DebugDirectoryEntryType.CodeView" /> entry pointing to a Portable PDB; otherwise, <see langword="false" />.</returns>
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00005339 File Offset: 0x00003539
		public bool IsPortableCodeView
		{
			get
			{
				return this.MinorVersion == 20557;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.PortableExecutable.DebugDirectoryEntry" /> structure.</summary>
		/// <param name="stamp" />
		/// <param name="majorVersion" />
		/// <param name="minorVersion" />
		/// <param name="type" />
		/// <param name="dataSize" />
		/// <param name="dataRelativeVirtualAddress" />
		/// <param name="dataPointer" />
		// Token: 0x06000192 RID: 402 RVA: 0x00005348 File Offset: 0x00003548
		public DebugDirectoryEntry(uint stamp, ushort majorVersion, ushort minorVersion, DebugDirectoryEntryType type, int dataSize, int dataRelativeVirtualAddress, int dataPointer)
		{
			this.Stamp = stamp;
			this.MajorVersion = majorVersion;
			this.MinorVersion = minorVersion;
			this.Type = type;
			this.DataSize = dataSize;
			this.DataRelativeVirtualAddress = dataRelativeVirtualAddress;
			this.DataPointer = dataPointer;
		}

		// Token: 0x040000B4 RID: 180
		internal const int Size = 28;
	}
}
