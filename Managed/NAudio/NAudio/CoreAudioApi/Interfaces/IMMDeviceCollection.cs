using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	// Token: 0x02000120 RID: 288
	[Guid("0BD7A1BE-7A1A-44DB-8397-CC5392387B5E")]
	[InterfaceType(1)]
	internal interface IMMDeviceCollection
	{
		// Token: 0x06000634 RID: 1588
		int GetCount(out int numDevices);

		// Token: 0x06000635 RID: 1589
		int Item(int deviceNumber, out IMMDevice device);
	}
}
