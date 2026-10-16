using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	// Token: 0x02000020 RID: 32
	[Guid("641DD20B-4D41-49CC-ABA3-174B9477BB08")]
	[InterfaceType(1)]
	public interface IAudioSessionNotification
	{
		// Token: 0x06000093 RID: 147
		[PreserveSig]
		int OnSessionCreated(IAudioSessionControl newSession);
	}
}
