using System;

namespace System.Reflection.PortableExecutable
{
	/// <summary>Describes the characteristics of a dynamic link library.</summary>
	// Token: 0x02000033 RID: 51
	[Flags]
	public enum DllCharacteristics : ushort
	{
		/// <summary>Reserved.</summary>
		// Token: 0x04000105 RID: 261
		ProcessInit = 1,
		/// <summary>Reserved.</summary>
		// Token: 0x04000106 RID: 262
		ProcessTerm = 2,
		/// <summary>Reserved.</summary>
		// Token: 0x04000107 RID: 263
		ThreadInit = 4,
		/// <summary>Reserved.</summary>
		// Token: 0x04000108 RID: 264
		ThreadTerm = 8,
		/// <summary>The image can handle a high entropy 64-bit virtual address space.</summary>
		// Token: 0x04000109 RID: 265
		HighEntropyVirtualAddressSpace = 32,
		/// <summary>The DLL can be relocated.</summary>
		// Token: 0x0400010A RID: 266
		DynamicBase = 64,
		/// <summary>The image is NX compatible.</summary>
		// Token: 0x0400010B RID: 267
		NxCompatible = 256,
		/// <summary>The image understands isolation and doesn't want it.</summary>
		// Token: 0x0400010C RID: 268
		NoIsolation = 512,
		/// <summary>The image does not use SEH. No SE handler may reside in this image.</summary>
		// Token: 0x0400010D RID: 269
		NoSeh = 1024,
		/// <summary>Do not bind this image.</summary>
		// Token: 0x0400010E RID: 270
		NoBind = 2048,
		/// <summary>The image must run inside an AppContainer.</summary>
		// Token: 0x0400010F RID: 271
		AppContainer = 4096,
		/// <summary>The driver uses the WDM model.</summary>
		// Token: 0x04000110 RID: 272
		WdmDriver = 8192,
		/// <summary>The image is Terminal Server aware.</summary>
		// Token: 0x04000111 RID: 273
		TerminalServerAware = 32768
	}
}
