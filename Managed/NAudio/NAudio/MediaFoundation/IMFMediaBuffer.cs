using System;
using System.Runtime.InteropServices;

namespace NAudio.MediaFoundation
{
	// Token: 0x02000052 RID: 82
	[InterfaceType(1)]
	[Guid("045FA593-8799-42b8-BC8D-8968C6453507")]
	[ComImport]
	public interface IMFMediaBuffer
	{
		// Token: 0x060001DD RID: 477
		void Lock(out IntPtr ppbBuffer, out int pcbMaxLength, out int pcbCurrentLength);

		// Token: 0x060001DE RID: 478
		void Unlock();

		// Token: 0x060001DF RID: 479
		void GetCurrentLength(out int pcbCurrentLength);

		// Token: 0x060001E0 RID: 480
		void SetCurrentLength(int cbCurrentLength);

		// Token: 0x060001E1 RID: 481
		void GetMaxLength(out int pcbMaxLength);
	}
}
