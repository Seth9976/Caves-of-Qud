using System;

namespace System.Reflection.PortableExecutable
{
	/// <summary>Describes the subsystem requirement for the image.</summary>
	// Token: 0x02000032 RID: 50
	public enum Subsystem : ushort
	{
		/// <summary>Unknown subsystem.</summary>
		// Token: 0x040000F6 RID: 246
		Unknown,
		/// <summary>The image doesn't require a subsystem.</summary>
		// Token: 0x040000F7 RID: 247
		Native,
		/// <summary>The image runs in the Windows GUI subsystem.</summary>
		// Token: 0x040000F8 RID: 248
		WindowsGui,
		/// <summary>The image runs in the Windows character subsystem.</summary>
		// Token: 0x040000F9 RID: 249
		WindowsCui,
		/// <summary>The image runs in the OS/2 character subsystem.</summary>
		// Token: 0x040000FA RID: 250
		OS2Cui = 5,
		/// <summary>The image runs in the Posix character subsystem.</summary>
		// Token: 0x040000FB RID: 251
		PosixCui = 7,
		/// <summary>The image is a native Win9x driver.</summary>
		// Token: 0x040000FC RID: 252
		NativeWindows,
		/// <summary>The image runs in the Windows CE subsystem.</summary>
		// Token: 0x040000FD RID: 253
		WindowsCEGui,
		/// <summary>Extensible Firmware Interface (EFI) application.</summary>
		// Token: 0x040000FE RID: 254
		EfiApplication,
		/// <summary>EFI driver with boot services.</summary>
		// Token: 0x040000FF RID: 255
		EfiBootServiceDriver,
		/// <summary>EFI driver with run-time services.</summary>
		// Token: 0x04000100 RID: 256
		EfiRuntimeDriver,
		/// <summary>EFI ROM image.</summary>
		// Token: 0x04000101 RID: 257
		EfiRom,
		/// <summary>Xbox system.</summary>
		// Token: 0x04000102 RID: 258
		Xbox,
		/// <summary>Boot application.</summary>
		// Token: 0x04000103 RID: 259
		WindowsBootApplication = 16
	}
}
