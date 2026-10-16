using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;
using NAudio.Wave;

namespace NAudio.CoreAudioApi
{
	// Token: 0x0200000B RID: 11
	public class AudioClient : IDisposable
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00003DA2 File Offset: 0x00001FA2
		internal AudioClient(IAudioClient audioClientInterface)
		{
			this.audioClientInterface = audioClientInterface;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00003DB4 File Offset: 0x00001FB4
		public WaveFormat MixFormat
		{
			get
			{
				if (this.mixFormat == null)
				{
					IntPtr intPtr;
					Marshal.ThrowExceptionForHR(this.audioClientInterface.GetMixFormat(out intPtr));
					WaveFormat waveFormat = WaveFormat.MarshalFromPtr(intPtr);
					Marshal.FreeCoTaskMem(intPtr);
					this.mixFormat = waveFormat;
				}
				return this.mixFormat;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003DF8 File Offset: 0x00001FF8
		public void Initialize(AudioClientShareMode shareMode, AudioClientStreamFlags streamFlags, long bufferDuration, long periodicity, WaveFormat waveFormat, Guid audioSessionGuid)
		{
			this.shareMode = shareMode;
			int num = this.audioClientInterface.Initialize(shareMode, streamFlags, bufferDuration, periodicity, waveFormat, ref audioSessionGuid);
			Marshal.ThrowExceptionForHR(num);
			this.mixFormat = null;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00003E30 File Offset: 0x00002030
		public int BufferSize
		{
			get
			{
				uint num;
				Marshal.ThrowExceptionForHR(this.audioClientInterface.GetBufferSize(out num));
				return (int)num;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00003E50 File Offset: 0x00002050
		public long StreamLatency
		{
			get
			{
				return this.audioClientInterface.GetStreamLatency();
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00003E60 File Offset: 0x00002060
		public int CurrentPadding
		{
			get
			{
				int num;
				Marshal.ThrowExceptionForHR(this.audioClientInterface.GetCurrentPadding(out num));
				return num;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00003E80 File Offset: 0x00002080
		public long DefaultDevicePeriod
		{
			get
			{
				long num;
				long num2;
				Marshal.ThrowExceptionForHR(this.audioClientInterface.GetDevicePeriod(out num, out num2));
				return num;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00003EA4 File Offset: 0x000020A4
		public long MinimumDevicePeriod
		{
			get
			{
				long num;
				long num2;
				Marshal.ThrowExceptionForHR(this.audioClientInterface.GetDevicePeriod(out num, out num2));
				return num2;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00003EC8 File Offset: 0x000020C8
		public AudioStreamVolume AudioStreamVolume
		{
			get
			{
				if (this.shareMode == AudioClientShareMode.Exclusive)
				{
					throw new InvalidOperationException("AudioStreamVolume is ONLY supported for shared audio streams.");
				}
				if (this.audioStreamVolume == null)
				{
					Guid guid;
					guid..ctor("93014887-242D-4068-8A15-CF5E93B90FE3");
					object obj;
					Marshal.ThrowExceptionForHR(this.audioClientInterface.GetService(guid, out obj));
					this.audioStreamVolume = new AudioStreamVolume((IAudioStreamVolume)obj);
				}
				return this.audioStreamVolume;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00003F28 File Offset: 0x00002128
		public AudioClockClient AudioClockClient
		{
			get
			{
				if (this.audioClockClient == null)
				{
					Guid guid;
					guid..ctor("CD63314F-3FBA-4a1b-812C-EF96358728E7");
					object obj;
					Marshal.ThrowExceptionForHR(this.audioClientInterface.GetService(guid, out obj));
					this.audioClockClient = new AudioClockClient((IAudioClock)obj);
				}
				return this.audioClockClient;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00003F74 File Offset: 0x00002174
		public AudioRenderClient AudioRenderClient
		{
			get
			{
				if (this.audioRenderClient == null)
				{
					Guid guid;
					guid..ctor("F294ACFC-3146-4483-A7BF-ADDCA7C260E2");
					object obj;
					Marshal.ThrowExceptionForHR(this.audioClientInterface.GetService(guid, out obj));
					this.audioRenderClient = new AudioRenderClient((IAudioRenderClient)obj);
				}
				return this.audioRenderClient;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00003FC0 File Offset: 0x000021C0
		public AudioCaptureClient AudioCaptureClient
		{
			get
			{
				if (this.audioCaptureClient == null)
				{
					Guid guid;
					guid..ctor("c8adbd64-e71e-48a0-a4de-185c395cd317");
					object obj;
					Marshal.ThrowExceptionForHR(this.audioClientInterface.GetService(guid, out obj));
					this.audioCaptureClient = new AudioCaptureClient((IAudioCaptureClient)obj);
				}
				return this.audioCaptureClient;
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000400C File Offset: 0x0000220C
		public bool IsFormatSupported(AudioClientShareMode shareMode, WaveFormat desiredFormat)
		{
			WaveFormatExtensible waveFormatExtensible;
			return this.IsFormatSupported(shareMode, desiredFormat, out waveFormatExtensible);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00004024 File Offset: 0x00002224
		public bool IsFormatSupported(AudioClientShareMode shareMode, WaveFormat desiredFormat, out WaveFormatExtensible closestMatchFormat)
		{
			int num = this.audioClientInterface.IsFormatSupported(shareMode, desiredFormat, out closestMatchFormat);
			if (num == 0)
			{
				return true;
			}
			if (num == 1)
			{
				return false;
			}
			if (num == -2004287480)
			{
				return false;
			}
			Marshal.ThrowExceptionForHR(num);
			throw new NotSupportedException("Unknown hresult " + num);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00004070 File Offset: 0x00002270
		public void Start()
		{
			this.audioClientInterface.Start();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000407E File Offset: 0x0000227E
		public void Stop()
		{
			this.audioClientInterface.Stop();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000408C File Offset: 0x0000228C
		public void SetEventHandle(IntPtr eventWaitHandle)
		{
			this.audioClientInterface.SetEventHandle(eventWaitHandle);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000409B File Offset: 0x0000229B
		public void Reset()
		{
			this.audioClientInterface.Reset();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000040AC File Offset: 0x000022AC
		public void Dispose()
		{
			if (this.audioClientInterface != null)
			{
				if (this.audioClockClient != null)
				{
					this.audioClockClient.Dispose();
					this.audioClockClient = null;
				}
				if (this.audioRenderClient != null)
				{
					this.audioRenderClient.Dispose();
					this.audioRenderClient = null;
				}
				if (this.audioCaptureClient != null)
				{
					this.audioCaptureClient.Dispose();
					this.audioCaptureClient = null;
				}
				if (this.audioStreamVolume != null)
				{
					this.audioStreamVolume.Dispose();
					this.audioStreamVolume = null;
				}
				Marshal.ReleaseComObject(this.audioClientInterface);
				this.audioClientInterface = null;
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x04000034 RID: 52
		private IAudioClient audioClientInterface;

		// Token: 0x04000035 RID: 53
		private WaveFormat mixFormat;

		// Token: 0x04000036 RID: 54
		private AudioRenderClient audioRenderClient;

		// Token: 0x04000037 RID: 55
		private AudioCaptureClient audioCaptureClient;

		// Token: 0x04000038 RID: 56
		private AudioClockClient audioClockClient;

		// Token: 0x04000039 RID: 57
		private AudioStreamVolume audioStreamVolume;

		// Token: 0x0400003A RID: 58
		private AudioClientShareMode shareMode;
	}
}
