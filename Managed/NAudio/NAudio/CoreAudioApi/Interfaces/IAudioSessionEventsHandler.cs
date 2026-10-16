using System;

namespace NAudio.CoreAudioApi.Interfaces
{
	// Token: 0x02000031 RID: 49
	public interface IAudioSessionEventsHandler
	{
		// Token: 0x060000C9 RID: 201
		void OnVolumeChanged(float volume, bool isMuted);

		// Token: 0x060000CA RID: 202
		void OnDisplayNameChanged(string displayName);

		// Token: 0x060000CB RID: 203
		void OnIconPathChanged(string iconPath);

		// Token: 0x060000CC RID: 204
		void OnChannelVolumeChanged(uint channelCount, IntPtr newVolumes, uint channelIndex);

		// Token: 0x060000CD RID: 205
		void OnGroupingParamChanged(ref Guid groupingId);

		// Token: 0x060000CE RID: 206
		void OnStateChanged(AudioSessionState state);

		// Token: 0x060000CF RID: 207
		void OnSessionDisconnected(AudioSessionDisconnectReason disconnectReason);
	}
}
