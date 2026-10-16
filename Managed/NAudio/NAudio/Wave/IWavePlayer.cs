using System;

namespace NAudio.Wave
{
	// Token: 0x020001A6 RID: 422
	public interface IWavePlayer : IDisposable
	{
		// Token: 0x06000887 RID: 2183
		void Play();

		// Token: 0x06000888 RID: 2184
		void Stop();

		// Token: 0x06000889 RID: 2185
		void Pause();

		// Token: 0x0600088A RID: 2186
		void Init(IWaveProvider waveProvider);

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600088B RID: 2187
		PlaybackState PlaybackState { get; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600088C RID: 2188
		// (set) Token: 0x0600088D RID: 2189
		[Obsolete("Not intending to keep supporting this going forward: set the volume on your input WaveProvider instead")]
		float Volume { get; set; }

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x0600088E RID: 2190
		// (remove) Token: 0x0600088F RID: 2191
		event EventHandler<StoppedEventArgs> PlaybackStopped;
	}
}
