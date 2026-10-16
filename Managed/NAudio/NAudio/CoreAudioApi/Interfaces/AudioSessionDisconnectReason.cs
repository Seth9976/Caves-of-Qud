using System;

namespace NAudio.CoreAudioApi.Interfaces
{
	// Token: 0x02000030 RID: 48
	public enum AudioSessionDisconnectReason
	{
		// Token: 0x040000A0 RID: 160
		DisconnectReasonDeviceRemoval,
		// Token: 0x040000A1 RID: 161
		DisconnectReasonServerShutdown,
		// Token: 0x040000A2 RID: 162
		DisconnectReasonFormatChanged,
		// Token: 0x040000A3 RID: 163
		DisconnectReasonSessionLogoff,
		// Token: 0x040000A4 RID: 164
		DisconnectReasonSessionDisconnected,
		// Token: 0x040000A5 RID: 165
		DisconnectReasonExclusiveModeOverride
	}
}
