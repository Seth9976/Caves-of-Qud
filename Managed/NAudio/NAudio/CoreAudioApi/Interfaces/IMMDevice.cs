using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	// Token: 0x0200011F RID: 287
	[InterfaceType(1)]
	[Guid("D666063F-1587-4E43-81F1-B948E807363F")]
	internal interface IMMDevice
	{
		// Token: 0x06000630 RID: 1584
		int Activate(ref Guid id, ClsCtx clsCtx, IntPtr activationParams, [MarshalAs(25)] out object interfacePointer);

		// Token: 0x06000631 RID: 1585
		int OpenPropertyStore(StorageAccessMode stgmAccess, out IPropertyStore properties);

		// Token: 0x06000632 RID: 1586
		int GetId([MarshalAs(21)] out string id);

		// Token: 0x06000633 RID: 1587
		int GetState(out DeviceState state);
	}
}
