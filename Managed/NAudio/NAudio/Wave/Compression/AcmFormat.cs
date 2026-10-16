using System;

namespace NAudio.Wave.Compression
{
	// Token: 0x0200015D RID: 349
	public class AcmFormat
	{
		// Token: 0x0600073B RID: 1851 RVA: 0x00015877 File Offset: 0x00013A77
		internal AcmFormat(AcmFormatDetails formatDetails)
		{
			this.formatDetails = formatDetails;
			this.waveFormat = WaveFormat.MarshalFromPtr(formatDetails.waveFormatPointer);
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x00015898 File Offset: 0x00013A98
		public int FormatIndex
		{
			get
			{
				return this.formatDetails.formatIndex;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x000158A5 File Offset: 0x00013AA5
		public WaveFormatEncoding FormatTag
		{
			get
			{
				return (WaveFormatEncoding)this.formatDetails.formatTag;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x000158B3 File Offset: 0x00013AB3
		public AcmDriverDetailsSupportFlags SupportFlags
		{
			get
			{
				return this.formatDetails.supportFlags;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x000158C0 File Offset: 0x00013AC0
		public WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x000158C8 File Offset: 0x00013AC8
		public int WaveFormatByteSize
		{
			get
			{
				return this.formatDetails.waveFormatByteSize;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x000158D5 File Offset: 0x00013AD5
		public string FormatDescription
		{
			get
			{
				return this.formatDetails.formatDescription;
			}
		}

		// Token: 0x040007B6 RID: 1974
		private readonly AcmFormatDetails formatDetails;

		// Token: 0x040007B7 RID: 1975
		private readonly WaveFormat waveFormat;
	}
}
