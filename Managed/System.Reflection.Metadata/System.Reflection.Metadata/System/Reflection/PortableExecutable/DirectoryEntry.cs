using System;

namespace System.Reflection.PortableExecutable
{
	/// <summary>Represents a directory entry in the Portable Executable (PE) image.</summary>
	// Token: 0x0200002D RID: 45
	public readonly struct DirectoryEntry
	{
		/// <summary>Instantiates a new instance of the <see cref="T:System.Reflection.PortableExecutable.DirectoryEntry" /> structure.</summary>
		/// <param name="relativeVirtualAddress">The Relative Virtual Address of the entry.</param>
		/// <param name="size">The size of the entry, in bytes.</param>
		// Token: 0x06000193 RID: 403 RVA: 0x0000537F File Offset: 0x0000357F
		public DirectoryEntry(int relativeVirtualAddress, int size)
		{
			this.RelativeVirtualAddress = relativeVirtualAddress;
			this.Size = size;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000538F File Offset: 0x0000358F
		internal DirectoryEntry(ref PEBinaryReader reader)
		{
			this.RelativeVirtualAddress = reader.ReadInt32();
			this.Size = reader.ReadInt32();
		}

		/// <summary>Specifies the address of the entry relative to the base address of the image.</summary>
		// Token: 0x040000C3 RID: 195
		public readonly int RelativeVirtualAddress;

		/// <summary>Specifies the size of the entry, in bytes.</summary>
		// Token: 0x040000C4 RID: 196
		public readonly int Size;
	}
}
