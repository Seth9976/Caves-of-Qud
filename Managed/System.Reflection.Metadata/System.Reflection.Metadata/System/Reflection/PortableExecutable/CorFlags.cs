using System;

namespace System.Reflection.PortableExecutable
{
	/// <summary>Represents the runtime flags for a .NET executable image.</summary>
	// Token: 0x02000028 RID: 40
	[Flags]
	public enum CorFlags
	{
		/// <summary>Indicates that this image only contains IL code.</summary>
		// Token: 0x0400009F RID: 159
		ILOnly = 1,
		/// <summary>Indicates that this image can only be loaded into a 32-bit process.</summary>
		// Token: 0x040000A0 RID: 160
		Requires32Bit = 2,
		/// <summary>Indicates that this image is a library that only contains IL code.</summary>
		// Token: 0x040000A1 RID: 161
		ILLibrary = 4,
		/// <summary>Indicates that this image has a strong name signature.</summary>
		// Token: 0x040000A2 RID: 162
		StrongNameSigned = 8,
		/// <summary>Reserved, shall be 0.</summary>
		// Token: 0x040000A3 RID: 163
		NativeEntryPoint = 16,
		/// <summary>Reserved, shall be 0.</summary>
		// Token: 0x040000A4 RID: 164
		TrackDebugData = 65536,
		/// <summary>Indicates that this image should be run as a 32-bit process on a 64-bit operating system.</summary>
		// Token: 0x040000A5 RID: 165
		Prefers32Bit = 131072
	}
}
