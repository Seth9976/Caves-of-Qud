using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave
{
	// Token: 0x0200017B RID: 379
	[StructLayout(0, CharSet = 4)]
	public struct WaveInCapabilities
	{
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x000165FA File Offset: 0x000147FA
		public int Channels
		{
			get
			{
				return (int)this.channels;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x00016602 File Offset: 0x00014802
		public string ProductName
		{
			get
			{
				return this.productName;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x0001660A File Offset: 0x0001480A
		public Guid NameGuid
		{
			get
			{
				return this.nameGuid;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x00016612 File Offset: 0x00014812
		public Guid ProductGuid
		{
			get
			{
				return this.productGuid;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x0001661A File Offset: 0x0001481A
		public Guid ManufacturerGuid
		{
			get
			{
				return this.manufacturerGuid;
			}
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00016622 File Offset: 0x00014822
		public bool SupportsWaveFormat(SupportedWaveFormat waveFormat)
		{
			return (this.supportedFormats & waveFormat) == waveFormat;
		}

		// Token: 0x04000915 RID: 2325
		private const int MaxProductNameLength = 32;

		// Token: 0x04000916 RID: 2326
		private short manufacturerId;

		// Token: 0x04000917 RID: 2327
		private short productId;

		// Token: 0x04000918 RID: 2328
		private int driverVersion;

		// Token: 0x04000919 RID: 2329
		[MarshalAs(23, SizeConst = 32)]
		private string productName;

		// Token: 0x0400091A RID: 2330
		private SupportedWaveFormat supportedFormats;

		// Token: 0x0400091B RID: 2331
		private short channels;

		// Token: 0x0400091C RID: 2332
		private short reserved;

		// Token: 0x0400091D RID: 2333
		private Guid manufacturerGuid;

		// Token: 0x0400091E RID: 2334
		private Guid productGuid;

		// Token: 0x0400091F RID: 2335
		private Guid nameGuid;
	}
}
