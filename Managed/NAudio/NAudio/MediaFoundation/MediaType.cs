using System;
using System.Runtime.InteropServices;
using NAudio.Utils;
using NAudio.Wave;

namespace NAudio.MediaFoundation
{
	// Token: 0x020001AB RID: 427
	public class MediaType
	{
		// Token: 0x060008EB RID: 2283 RVA: 0x00019D6C File Offset: 0x00017F6C
		public MediaType(IMFMediaType mediaType)
		{
			this.mediaType = mediaType;
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00019D7B File Offset: 0x00017F7B
		public MediaType()
		{
			this.mediaType = MediaFoundationApi.CreateMediaType();
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00019D8E File Offset: 0x00017F8E
		public MediaType(WaveFormat waveFormat)
		{
			this.mediaType = MediaFoundationApi.CreateMediaTypeFromWaveFormat(waveFormat);
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00019DA4 File Offset: 0x00017FA4
		private int GetUInt32(Guid key)
		{
			int num;
			this.mediaType.GetUINT32(key, out num);
			return num;
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00019DC0 File Offset: 0x00017FC0
		private Guid GetGuid(Guid key)
		{
			Guid guid;
			this.mediaType.GetGUID(key, out guid);
			return guid;
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00019DDC File Offset: 0x00017FDC
		public int TryGetUInt32(Guid key, int defaultValue = -1)
		{
			int num = defaultValue;
			try
			{
				this.mediaType.GetUINT32(key, out num);
			}
			catch (COMException ex)
			{
				if (ex.GetHResult() != -1072875802)
				{
					if (ex.GetHResult() == -1072875843)
					{
						throw new ArgumentException("Not a UINT32 parameter");
					}
					throw;
				}
			}
			return num;
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x00019E38 File Offset: 0x00018038
		// (set) Token: 0x060008F2 RID: 2290 RVA: 0x00019E45 File Offset: 0x00018045
		public int SampleRate
		{
			get
			{
				return this.GetUInt32(MediaFoundationAttributes.MF_MT_AUDIO_SAMPLES_PER_SECOND);
			}
			set
			{
				this.mediaType.SetUINT32(MediaFoundationAttributes.MF_MT_AUDIO_SAMPLES_PER_SECOND, value);
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x00019E58 File Offset: 0x00018058
		// (set) Token: 0x060008F4 RID: 2292 RVA: 0x00019E65 File Offset: 0x00018065
		public int ChannelCount
		{
			get
			{
				return this.GetUInt32(MediaFoundationAttributes.MF_MT_AUDIO_NUM_CHANNELS);
			}
			set
			{
				this.mediaType.SetUINT32(MediaFoundationAttributes.MF_MT_AUDIO_NUM_CHANNELS, value);
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x00019E78 File Offset: 0x00018078
		// (set) Token: 0x060008F6 RID: 2294 RVA: 0x00019E85 File Offset: 0x00018085
		public int BitsPerSample
		{
			get
			{
				return this.GetUInt32(MediaFoundationAttributes.MF_MT_AUDIO_BITS_PER_SAMPLE);
			}
			set
			{
				this.mediaType.SetUINT32(MediaFoundationAttributes.MF_MT_AUDIO_BITS_PER_SAMPLE, value);
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x00019E98 File Offset: 0x00018098
		public int AverageBytesPerSecond
		{
			get
			{
				return this.GetUInt32(MediaFoundationAttributes.MF_MT_AUDIO_AVG_BYTES_PER_SECOND);
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x00019EA5 File Offset: 0x000180A5
		// (set) Token: 0x060008F9 RID: 2297 RVA: 0x00019EB2 File Offset: 0x000180B2
		public Guid SubType
		{
			get
			{
				return this.GetGuid(MediaFoundationAttributes.MF_MT_SUBTYPE);
			}
			set
			{
				this.mediaType.SetGUID(MediaFoundationAttributes.MF_MT_SUBTYPE, value);
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x00019EC5 File Offset: 0x000180C5
		// (set) Token: 0x060008FB RID: 2299 RVA: 0x00019ED2 File Offset: 0x000180D2
		public Guid MajorType
		{
			get
			{
				return this.GetGuid(MediaFoundationAttributes.MF_MT_MAJOR_TYPE);
			}
			set
			{
				this.mediaType.SetGUID(MediaFoundationAttributes.MF_MT_MAJOR_TYPE, value);
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x00019EE5 File Offset: 0x000180E5
		public IMFMediaType MediaFoundationObject
		{
			get
			{
				return this.mediaType;
			}
		}

		// Token: 0x04000A7C RID: 2684
		private readonly IMFMediaType mediaType;
	}
}
