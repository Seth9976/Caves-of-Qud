using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	// Token: 0x02000121 RID: 289
	[Guid("A95664D2-9614-4F35-A746-DE8DB63617E6")]
	[InterfaceType(1)]
	internal interface IMMDeviceEnumerator
	{
		// Token: 0x06000636 RID: 1590
		int EnumAudioEndpoints(DataFlow dataFlow, DeviceState stateMask, out IMMDeviceCollection devices);

		// Token: 0x06000637 RID: 1591
		[PreserveSig]
		int GetDefaultAudioEndpoint(DataFlow dataFlow, Role role, out IMMDevice endpoint);

		// Token: 0x06000638 RID: 1592
		int GetDevice(string id, out IMMDevice deviceName);

		// Token: 0x06000639 RID: 1593
		int RegisterEndpointNotificationCallback(IMMNotificationClient client);

		// Token: 0x0600063A RID: 1594
		int UnregisterEndpointNotificationCallback(IMMNotificationClient client);
	}
}
