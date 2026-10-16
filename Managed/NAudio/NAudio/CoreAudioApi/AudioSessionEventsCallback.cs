using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	// Token: 0x0200001D RID: 29
	public class AudioSessionEventsCallback : IAudioSessionEvents
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00004842 File Offset: 0x00002A42
		public AudioSessionEventsCallback(IAudioSessionEventsHandler handler)
		{
			this.audioSessionEventsHandler = handler;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004851 File Offset: 0x00002A51
		public int OnDisplayNameChanged([MarshalAs(21)] [In] string displayName, [In] ref Guid eventContext)
		{
			this.audioSessionEventsHandler.OnDisplayNameChanged(displayName);
			return 0;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004860 File Offset: 0x00002A60
		public int OnIconPathChanged([MarshalAs(21)] [In] string iconPath, [In] ref Guid eventContext)
		{
			this.audioSessionEventsHandler.OnIconPathChanged(iconPath);
			return 0;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000486F File Offset: 0x00002A6F
		public int OnSimpleVolumeChanged([MarshalAs(11)] [In] float volume, [MarshalAs(2)] [In] bool isMuted, [In] ref Guid eventContext)
		{
			this.audioSessionEventsHandler.OnVolumeChanged(volume, isMuted);
			return 0;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000487F File Offset: 0x00002A7F
		public int OnChannelVolumeChanged([MarshalAs(8)] [In] uint channelCount, [MarshalAs(31)] [In] IntPtr newVolumes, [MarshalAs(8)] [In] uint channelIndex, [In] ref Guid eventContext)
		{
			this.audioSessionEventsHandler.OnChannelVolumeChanged(channelCount, newVolumes, channelIndex);
			return 0;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004890 File Offset: 0x00002A90
		public int OnGroupingParamChanged([In] ref Guid groupingId, [In] ref Guid eventContext)
		{
			this.audioSessionEventsHandler.OnGroupingParamChanged(ref groupingId);
			return 0;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000489F File Offset: 0x00002A9F
		public int OnStateChanged([In] AudioSessionState state)
		{
			this.audioSessionEventsHandler.OnStateChanged(state);
			return 0;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000048AE File Offset: 0x00002AAE
		public int OnSessionDisconnected([In] AudioSessionDisconnectReason disconnectReason)
		{
			this.audioSessionEventsHandler.OnSessionDisconnected(disconnectReason);
			return 0;
		}

		// Token: 0x04000062 RID: 98
		private readonly IAudioSessionEventsHandler audioSessionEventsHandler;
	}
}
