using System;
using System.Runtime.InteropServices;
using NAudio.Wave;

namespace NAudio.Dmo
{
	// Token: 0x020001FA RID: 506
	public struct DmoMediaType
	{
		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x000227A8 File Offset: 0x000209A8
		public Guid MajorType
		{
			get
			{
				return this.majortype;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x000227B0 File Offset: 0x000209B0
		public string MajorTypeName
		{
			get
			{
				return MediaTypes.GetMediaTypeName(this.majortype);
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x000227BD File Offset: 0x000209BD
		public Guid SubType
		{
			get
			{
				return this.subtype;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x000227C5 File Offset: 0x000209C5
		public string SubTypeName
		{
			get
			{
				if (this.majortype == MediaTypes.MEDIATYPE_Audio)
				{
					return AudioMediaSubtypes.GetAudioSubtypeName(this.subtype);
				}
				return this.subtype.ToString();
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x000227F6 File Offset: 0x000209F6
		public bool FixedSizeSamples
		{
			get
			{
				return this.bFixedSizeSamples;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000B79 RID: 2937 RVA: 0x000227FE File Offset: 0x000209FE
		public int SampleSize
		{
			get
			{
				return this.lSampleSize;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x00022806 File Offset: 0x00020A06
		public Guid FormatType
		{
			get
			{
				return this.formattype;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x00022810 File Offset: 0x00020A10
		public string FormatTypeName
		{
			get
			{
				if (this.formattype == DmoMediaTypeGuids.FORMAT_None)
				{
					return "None";
				}
				if (this.formattype == Guid.Empty)
				{
					return "Null";
				}
				if (this.formattype == DmoMediaTypeGuids.FORMAT_WaveFormatEx)
				{
					return "WaveFormatEx";
				}
				return this.FormatType.ToString();
			}
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x00022879 File Offset: 0x00020A79
		public WaveFormat GetWaveFormat()
		{
			if (this.formattype == DmoMediaTypeGuids.FORMAT_WaveFormatEx)
			{
				return WaveFormat.MarshalFromPtr(this.pbFormat);
			}
			throw new InvalidOperationException("Not a WaveFormat type");
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x000228A4 File Offset: 0x00020AA4
		public void SetWaveFormat(WaveFormat waveFormat)
		{
			this.majortype = MediaTypes.MEDIATYPE_Audio;
			WaveFormatExtensible waveFormatExtensible = waveFormat as WaveFormatExtensible;
			if (waveFormatExtensible == null)
			{
				WaveFormatEncoding encoding = waveFormat.Encoding;
				switch (encoding)
				{
				case WaveFormatEncoding.Pcm:
					this.subtype = AudioMediaSubtypes.MEDIASUBTYPE_PCM;
					goto IL_0087;
				case WaveFormatEncoding.Adpcm:
					break;
				case WaveFormatEncoding.IeeeFloat:
					this.subtype = AudioMediaSubtypes.MEDIASUBTYPE_IEEE_FLOAT;
					goto IL_0087;
				default:
					if (encoding == WaveFormatEncoding.MpegLayer3)
					{
						this.subtype = AudioMediaSubtypes.WMMEDIASUBTYPE_MP3;
						goto IL_0087;
					}
					break;
				}
				throw new ArgumentException(string.Format("Not a supported encoding {0}", waveFormat.Encoding));
			}
			this.subtype = waveFormatExtensible.SubFormat;
			IL_0087:
			this.bFixedSizeSamples = this.SubType == AudioMediaSubtypes.MEDIASUBTYPE_PCM || this.SubType == AudioMediaSubtypes.MEDIASUBTYPE_IEEE_FLOAT;
			this.formattype = DmoMediaTypeGuids.FORMAT_WaveFormatEx;
			if (this.cbFormat < Marshal.SizeOf(waveFormat))
			{
				throw new InvalidOperationException("Not enough memory assigned for a WaveFormat structure");
			}
			Marshal.StructureToPtr(waveFormat, this.pbFormat, false);
		}

		// Token: 0x04000BFA RID: 3066
		private Guid majortype;

		// Token: 0x04000BFB RID: 3067
		private Guid subtype;

		// Token: 0x04000BFC RID: 3068
		private bool bFixedSizeSamples;

		// Token: 0x04000BFD RID: 3069
		private bool bTemporalCompression;

		// Token: 0x04000BFE RID: 3070
		private int lSampleSize;

		// Token: 0x04000BFF RID: 3071
		private Guid formattype;

		// Token: 0x04000C00 RID: 3072
		private IntPtr pUnk;

		// Token: 0x04000C01 RID: 3073
		private int cbFormat;

		// Token: 0x04000C02 RID: 3074
		private IntPtr pbFormat;
	}
}
