using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave
{
	// Token: 0x02000182 RID: 386
	[StructLayout(0, CharSet = 4)]
	public struct WaveOutCapabilities
	{
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x00016709 File Offset: 0x00014909
		public int Channels
		{
			get
			{
				return (int)this.channels;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x00016711 File Offset: 0x00014911
		public bool SupportsPlaybackRateControl
		{
			get
			{
				return (this.support & WaveOutSupport.PlaybackRate) == WaveOutSupport.PlaybackRate;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x0001671E File Offset: 0x0001491E
		public string ProductName
		{
			get
			{
				return this.productName;
			}
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00016726 File Offset: 0x00014926
		public bool SupportsWaveFormat(SupportedWaveFormat waveFormat)
		{
			return (this.supportedFormats & waveFormat) == waveFormat;
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x00016733 File Offset: 0x00014933
		public Guid NameGuid
		{
			get
			{
				return this.nameGuid;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x0001673B File Offset: 0x0001493B
		public Guid ProductGuid
		{
			get
			{
				return this.productGuid;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x00016743 File Offset: 0x00014943
		public Guid ManufacturerGuid
		{
			get
			{
				return this.manufacturerGuid;
			}
		}

		// Token: 0x04000932 RID: 2354
		private const int MaxProductNameLength = 32;

		// Token: 0x04000933 RID: 2355
		private short manufacturerId;

		// Token: 0x04000934 RID: 2356
		private short productId;

		// Token: 0x04000935 RID: 2357
		private int driverVersion;

		// Token: 0x04000936 RID: 2358
		[MarshalAs(23, SizeConst = 32)]
		private string productName;

		// Token: 0x04000937 RID: 2359
		private SupportedWaveFormat supportedFormats;

		// Token: 0x04000938 RID: 2360
		private short channels;

		// Token: 0x04000939 RID: 2361
		private short reserved;

		// Token: 0x0400093A RID: 2362
		private WaveOutSupport support;

		// Token: 0x0400093B RID: 2363
		private Guid manufacturerGuid;

		// Token: 0x0400093C RID: 2364
		private Guid productGuid;

		// Token: 0x0400093D RID: 2365
		private Guid nameGuid;
	}
}
