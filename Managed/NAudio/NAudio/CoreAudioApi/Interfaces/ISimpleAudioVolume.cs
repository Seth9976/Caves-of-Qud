using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	// Token: 0x02000035 RID: 53
	[InterfaceType(1)]
	[Guid("87CE5498-68D6-44E5-9215-6DA47EF883D8")]
	internal interface ISimpleAudioVolume
	{
		// Token: 0x060000DE RID: 222
		[PreserveSig]
		int SetMasterVolume([MarshalAs(11)] [In] float levelNorm, [MarshalAs(43)] [In] Guid eventContext);

		// Token: 0x060000DF RID: 223
		[PreserveSig]
		int GetMasterVolume([MarshalAs(11)] out float levelNorm);

		// Token: 0x060000E0 RID: 224
		[PreserveSig]
		int SetMute([MarshalAs(2)] [In] bool isMuted, [MarshalAs(43)] [In] Guid eventContext);

		// Token: 0x060000E1 RID: 225
		[PreserveSig]
		int GetMute([MarshalAs(2)] out bool isMuted);
	}
}
