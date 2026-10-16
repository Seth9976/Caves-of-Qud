using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	// Token: 0x0200002D RID: 45
	[InterfaceType(1)]
	[Guid("bfb7ff88-7239-4fc9-8fa2-07c950be9c6d")]
	public interface IAudioSessionControl2 : IAudioSessionControl
	{
		// Token: 0x060000B9 RID: 185
		[PreserveSig]
		int GetState(out AudioSessionState state);

		// Token: 0x060000BA RID: 186
		[PreserveSig]
		int GetDisplayName([MarshalAs(21)] out string displayName);

		// Token: 0x060000BB RID: 187
		[PreserveSig]
		int SetDisplayName([MarshalAs(21)] [In] string displayName, [MarshalAs(43)] [In] Guid eventContext);

		// Token: 0x060000BC RID: 188
		[PreserveSig]
		int GetIconPath([MarshalAs(21)] out string iconPath);

		// Token: 0x060000BD RID: 189
		[PreserveSig]
		int SetIconPath([MarshalAs(21)] [In] string iconPath, [MarshalAs(43)] [In] Guid eventContext);

		// Token: 0x060000BE RID: 190
		[PreserveSig]
		int GetGroupingParam(out Guid groupingId);

		// Token: 0x060000BF RID: 191
		[PreserveSig]
		int SetGroupingParam([MarshalAs(43)] [In] Guid groupingId, [MarshalAs(43)] [In] Guid eventContext);

		// Token: 0x060000C0 RID: 192
		[PreserveSig]
		int RegisterAudioSessionNotification([In] IAudioSessionEvents client);

		// Token: 0x060000C1 RID: 193
		[PreserveSig]
		int UnregisterAudioSessionNotification([In] IAudioSessionEvents client);

		// Token: 0x060000C2 RID: 194
		[PreserveSig]
		int GetSessionIdentifier([MarshalAs(21)] out string retVal);

		// Token: 0x060000C3 RID: 195
		[PreserveSig]
		int GetSessionInstanceIdentifier([MarshalAs(21)] out string retVal);

		// Token: 0x060000C4 RID: 196
		[PreserveSig]
		int GetProcessId(out uint retVal);

		// Token: 0x060000C5 RID: 197
		[PreserveSig]
		int IsSystemSoundsSession();

		// Token: 0x060000C6 RID: 198
		[PreserveSig]
		int SetDuckingPreference(bool optOut);
	}
}
