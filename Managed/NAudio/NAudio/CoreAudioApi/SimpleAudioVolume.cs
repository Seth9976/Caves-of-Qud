using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	// Token: 0x02000039 RID: 57
	public class SimpleAudioVolume : IDisposable
	{
		// Token: 0x060000EB RID: 235 RVA: 0x000053E8 File Offset: 0x000035E8
		internal SimpleAudioVolume(ISimpleAudioVolume realSimpleVolume)
		{
			this.simpleAudioVolume = realSimpleVolume;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000053F7 File Offset: 0x000035F7
		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005400 File Offset: 0x00003600
		~SimpleAudioVolume()
		{
			this.Dispose();
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000EE RID: 238 RVA: 0x0000542C File Offset: 0x0000362C
		// (set) Token: 0x060000EF RID: 239 RVA: 0x0000544C File Offset: 0x0000364C
		public float Volume
		{
			get
			{
				float num;
				Marshal.ThrowExceptionForHR(this.simpleAudioVolume.GetMasterVolume(out num));
				return num;
			}
			set
			{
				if ((double)value >= 0.0 && (double)value <= 1.0)
				{
					Marshal.ThrowExceptionForHR(this.simpleAudioVolume.SetMasterVolume(value, Guid.Empty));
				}
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00005480 File Offset: 0x00003680
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x000054A0 File Offset: 0x000036A0
		public bool Mute
		{
			get
			{
				bool flag;
				Marshal.ThrowExceptionForHR(this.simpleAudioVolume.GetMute(out flag));
				return flag;
			}
			set
			{
				Marshal.ThrowExceptionForHR(this.simpleAudioVolume.SetMute(value, Guid.Empty));
			}
		}

		// Token: 0x040000B6 RID: 182
		private readonly ISimpleAudioVolume simpleAudioVolume;
	}
}
