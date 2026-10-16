using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	// Token: 0x0200002C RID: 44
	[Guid("F4B1A599-7266-4319-A8CA-E70ACB11E8CD")]
	[InterfaceType(1)]
	public interface IAudioSessionControl
	{
		// Token: 0x060000B0 RID: 176
		[PreserveSig]
		int GetState(out AudioSessionState state);

		// Token: 0x060000B1 RID: 177
		[PreserveSig]
		int GetDisplayName([MarshalAs(21)] out string displayName);

		// Token: 0x060000B2 RID: 178
		[PreserveSig]
		int SetDisplayName([MarshalAs(21)] [In] string displayName, [MarshalAs(43)] [In] Guid eventContext);

		// Token: 0x060000B3 RID: 179
		[PreserveSig]
		int GetIconPath([MarshalAs(21)] out string iconPath);

		// Token: 0x060000B4 RID: 180
		[PreserveSig]
		int SetIconPath([MarshalAs(21)] [In] string iconPath, [MarshalAs(43)] [In] Guid eventContext);

		// Token: 0x060000B5 RID: 181
		[PreserveSig]
		int GetGroupingParam(out Guid groupingId);

		// Token: 0x060000B6 RID: 182
		[PreserveSig]
		int SetGroupingParam([MarshalAs(43)] [In] Guid groupingId, [MarshalAs(43)] [In] Guid eventContext);

		// Token: 0x060000B7 RID: 183
		[PreserveSig]
		int RegisterAudioSessionNotification([In] IAudioSessionEvents client);

		// Token: 0x060000B8 RID: 184
		[PreserveSig]
		int UnregisterAudioSessionNotification([In] IAudioSessionEvents client);
	}
}
