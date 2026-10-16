using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	// Token: 0x02000033 RID: 51
	[Guid("77AA99A0-1BD6-484F-8BC7-2C654C9A9B6F")]
	[InterfaceType(1)]
	internal interface IAudioSessionManager2 : IAudioSessionManager
	{
		// Token: 0x060000D2 RID: 210
		[PreserveSig]
		int GetAudioSessionControl([MarshalAs(43)] [In] [Optional] Guid sessionId, [MarshalAs(8)] [In] uint streamFlags, [MarshalAs(28)] out IAudioSessionControl sessionControl);

		// Token: 0x060000D3 RID: 211
		[PreserveSig]
		int GetSimpleAudioVolume([MarshalAs(43)] [In] [Optional] Guid sessionId, [MarshalAs(8)] [In] uint streamFlags, [MarshalAs(28)] out ISimpleAudioVolume audioVolume);

		// Token: 0x060000D4 RID: 212
		[PreserveSig]
		int GetSessionEnumerator(out IAudioSessionEnumerator sessionEnum);

		// Token: 0x060000D5 RID: 213
		[PreserveSig]
		int RegisterSessionNotification(IAudioSessionNotification sessionNotification);

		// Token: 0x060000D6 RID: 214
		[PreserveSig]
		int UnregisterSessionNotification(IAudioSessionNotification sessionNotification);

		// Token: 0x060000D7 RID: 215
		[PreserveSig]
		int RegisterDuckNotification(string sessionID, IAudioSessionNotification audioVolumeDuckNotification);

		// Token: 0x060000D8 RID: 216
		[PreserveSig]
		int UnregisterDuckNotification(IntPtr audioVolumeDuckNotification);
	}
}
