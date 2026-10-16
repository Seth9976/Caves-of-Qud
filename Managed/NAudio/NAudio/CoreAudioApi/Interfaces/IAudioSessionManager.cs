using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	// Token: 0x02000032 RID: 50
	[Guid("BFA971F1-4D5E-40BB-935E-967039BFBEE4")]
	[InterfaceType(1)]
	internal interface IAudioSessionManager
	{
		// Token: 0x060000D0 RID: 208
		[PreserveSig]
		int GetAudioSessionControl([MarshalAs(43)] [In] [Optional] Guid sessionId, [MarshalAs(8)] [In] uint streamFlags, [MarshalAs(28)] out IAudioSessionControl sessionControl);

		// Token: 0x060000D1 RID: 209
		[PreserveSig]
		int GetSimpleAudioVolume([MarshalAs(43)] [In] [Optional] Guid sessionId, [MarshalAs(8)] [In] uint streamFlags, [MarshalAs(28)] out ISimpleAudioVolume audioVolume);
	}
}
