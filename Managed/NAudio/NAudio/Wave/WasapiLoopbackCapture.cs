using System;
using NAudio.CoreAudioApi;

namespace NAudio.Wave
{
	// Token: 0x02000179 RID: 377
	public class WasapiLoopbackCapture : WasapiCapture
	{
		// Token: 0x0600078C RID: 1932 RVA: 0x00015F82 File Offset: 0x00014182
		public WasapiLoopbackCapture()
			: this(WasapiLoopbackCapture.GetDefaultLoopbackCaptureDevice())
		{
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x00015F8F File Offset: 0x0001418F
		public WasapiLoopbackCapture(MMDevice captureDevice)
			: base(captureDevice)
		{
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00015F98 File Offset: 0x00014198
		public static MMDevice GetDefaultLoopbackCaptureDevice()
		{
			MMDeviceEnumerator mmdeviceEnumerator = new MMDeviceEnumerator();
			return mmdeviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600078F RID: 1935 RVA: 0x00015FB3 File Offset: 0x000141B3
		// (set) Token: 0x06000790 RID: 1936 RVA: 0x00015FBB File Offset: 0x000141BB
		public override WaveFormat WaveFormat
		{
			get
			{
				return base.WaveFormat;
			}
			set
			{
				throw new InvalidOperationException("WaveFormat cannot be set for WASAPI Loopback Capture");
			}
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00015FC7 File Offset: 0x000141C7
		protected override AudioClientStreamFlags GetAudioClientStreamFlags()
		{
			return AudioClientStreamFlags.Loopback;
		}
	}
}
