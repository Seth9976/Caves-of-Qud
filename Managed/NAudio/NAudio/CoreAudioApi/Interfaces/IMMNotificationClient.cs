using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	// Token: 0x02000123 RID: 291
	[Guid("7991EEC9-7E89-4D85-8390-6C703CEC60C0")]
	[InterfaceType(1)]
	public interface IMMNotificationClient
	{
		// Token: 0x0600063C RID: 1596
		void OnDeviceStateChanged([MarshalAs(21)] string deviceId, [MarshalAs(7)] DeviceState newState);

		// Token: 0x0600063D RID: 1597
		void OnDeviceAdded([MarshalAs(21)] string pwstrDeviceId);

		// Token: 0x0600063E RID: 1598
		void OnDeviceRemoved([MarshalAs(21)] string deviceId);

		// Token: 0x0600063F RID: 1599
		void OnDefaultDeviceChanged(DataFlow flow, Role role, [MarshalAs(21)] string defaultDeviceId);

		// Token: 0x06000640 RID: 1600
		void OnPropertyValueChanged([MarshalAs(21)] string pwstrDeviceId, PropertyKey key);
	}
}
