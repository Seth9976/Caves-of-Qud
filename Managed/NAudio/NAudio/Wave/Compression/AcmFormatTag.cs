using System;

namespace NAudio.Wave.Compression
{
	// Token: 0x02000163 RID: 355
	public class AcmFormatTag
	{
		// Token: 0x06000742 RID: 1858 RVA: 0x000158E2 File Offset: 0x00013AE2
		internal AcmFormatTag(AcmFormatTagDetails formatTagDetails)
		{
			this.formatTagDetails = formatTagDetails;
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x000158F1 File Offset: 0x00013AF1
		public int FormatTagIndex
		{
			get
			{
				return this.formatTagDetails.formatTagIndex;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x000158FE File Offset: 0x00013AFE
		public WaveFormatEncoding FormatTag
		{
			get
			{
				return (WaveFormatEncoding)this.formatTagDetails.formatTag;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x0001590C File Offset: 0x00013B0C
		public int FormatSize
		{
			get
			{
				return this.formatTagDetails.formatSize;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x00015919 File Offset: 0x00013B19
		public AcmDriverDetailsSupportFlags SupportFlags
		{
			get
			{
				return this.formatTagDetails.supportFlags;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x00015926 File Offset: 0x00013B26
		public int StandardFormatsCount
		{
			get
			{
				return this.formatTagDetails.standardFormatsCount;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x00015933 File Offset: 0x00013B33
		public string FormatDescription
		{
			get
			{
				return this.formatTagDetails.formatDescription;
			}
		}

		// Token: 0x040007E9 RID: 2025
		private AcmFormatTagDetails formatTagDetails;
	}
}
