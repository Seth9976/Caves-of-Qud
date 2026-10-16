using System;

namespace System.Reflection.PortableExecutable
{
	/// <summary>Represents the Common Object File Format (COFF) file characteristics.</summary>
	// Token: 0x02000030 RID: 48
	[Flags]
	public enum Characteristics : ushort
	{
		/// <summary>Indicates that the image doesn't contain base relocations and must therefore be loaded at its preferred base address.</summary>
		// Token: 0x040000E3 RID: 227
		RelocsStripped = 1,
		/// <summary>Indicates that the image file is valid and can be run.</summary>
		// Token: 0x040000E4 RID: 228
		ExecutableImage = 2,
		/// <summary>Indicates that COFF line numbers have been removed from the file. This flag is deprecated and should be zero.</summary>
		// Token: 0x040000E5 RID: 229
		LineNumsStripped = 4,
		/// <summary>Indicates that COFF symbol table entries for local symbols have been removed from the file. This flag is deprecated and should be zero.</summary>
		// Token: 0x040000E6 RID: 230
		LocalSymsStripped = 8,
		/// <summary>Indicates that the operating system should aggressively trim the working set for this file. This flag is deprecated and should be zero.</summary>
		// Token: 0x040000E7 RID: 231
		AggressiveWSTrim = 16,
		/// <summary>Indicates that this executable file can handle memory addresses greater than 2 GB.</summary>
		// Token: 0x040000E8 RID: 232
		LargeAddressAware = 32,
		/// <summary>Indicates that this file uses a little endian byte order. This flag is deprecated and should be zero.</summary>
		// Token: 0x040000E9 RID: 233
		BytesReversedLo = 128,
		/// <summary>Indicates that this file is for a 32-bit machine.</summary>
		// Token: 0x040000EA RID: 234
		Bit32Machine = 256,
		/// <summary>Indicates that debugging information is removed from the image file.</summary>
		// Token: 0x040000EB RID: 235
		DebugStripped = 512,
		/// <summary>Indicates that the image should be fully loaded and copied into the swap file if it's located on a removable media.</summary>
		// Token: 0x040000EC RID: 236
		RemovableRunFromSwap = 1024,
		/// <summary>Indicates that the image should be fully loaded and copied into the swap file if it's located on a network media.</summary>
		// Token: 0x040000ED RID: 237
		NetRunFromSwap = 2048,
		/// <summary>Indicates that the image file is a system file, not a user program.</summary>
		// Token: 0x040000EE RID: 238
		System = 4096,
		/// <summary>Indicates that the image file is a dynamic-link library (DLL).</summary>
		// Token: 0x040000EF RID: 239
		Dll = 8192,
		/// <summary>Indicates that the file should be run only on a uniprocessor machine.</summary>
		// Token: 0x040000F0 RID: 240
		UpSystemOnly = 16384,
		/// <summary>Indicates that this file uses a big endian byte order. This flag is deprecated and should be zero.</summary>
		// Token: 0x040000F1 RID: 241
		BytesReversedHi = 32768
	}
}
