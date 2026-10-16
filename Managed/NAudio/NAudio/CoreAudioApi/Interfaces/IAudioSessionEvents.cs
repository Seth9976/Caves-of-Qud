using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	// Token: 0x0200001C RID: 28
	[Guid("24918ACC-64B3-37C1-8CA9-74A66E9957A8")]
	[InterfaceType(1)]
	public interface IAudioSessionEvents
	{
		// Token: 0x06000075 RID: 117
		[PreserveSig]
		int OnDisplayNameChanged([MarshalAs(21)] [In] string displayName, [In] ref Guid eventContext);

		// Token: 0x06000076 RID: 118
		[PreserveSig]
		int OnIconPathChanged([MarshalAs(21)] [In] string iconPath, [In] ref Guid eventContext);

		// Token: 0x06000077 RID: 119
		[PreserveSig]
		int OnSimpleVolumeChanged([MarshalAs(11)] [In] float volume, [MarshalAs(2)] [In] bool isMuted, [In] ref Guid eventContext);

		// Token: 0x06000078 RID: 120
		[PreserveSig]
		int OnChannelVolumeChanged([MarshalAs(8)] [In] uint channelCount, [MarshalAs(31)] [In] IntPtr newVolumes, [MarshalAs(8)] [In] uint channelIndex, [In] ref Guid eventContext);

		// Token: 0x06000079 RID: 121
		[PreserveSig]
		int OnGroupingParamChanged([In] ref Guid groupingId, [In] ref Guid eventContext);

		// Token: 0x0600007A RID: 122
		[PreserveSig]
		int OnStateChanged([In] AudioSessionState state);

		// Token: 0x0600007B RID: 123
		[PreserveSig]
		int OnSessionDisconnected([In] AudioSessionDisconnectReason disconnectReason);
	}
}
