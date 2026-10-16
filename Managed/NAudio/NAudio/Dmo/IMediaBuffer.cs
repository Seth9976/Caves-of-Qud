using System;
using System.Runtime.InteropServices;
using System.Security;

namespace NAudio.Dmo
{
	// Token: 0x0200008F RID: 143
	[InterfaceType(1)]
	[SuppressUnmanagedCodeSecurity]
	[Guid("59eff8b9-938c-4a26-82f2-95cb84cdc837")]
	[ComImport]
	public interface IMediaBuffer
	{
		// Token: 0x06000309 RID: 777
		[PreserveSig]
		int SetLength(int length);

		// Token: 0x0600030A RID: 778
		[PreserveSig]
		int GetMaxLength(out int maxLength);

		// Token: 0x0600030B RID: 779
		[PreserveSig]
		int GetBufferAndLength(IntPtr bufferPointerPointer, IntPtr validDataLengthPointer);
	}
}
