using System;
using System.Runtime.InteropServices;

namespace NAudio.Midi
{
	// Token: 0x020000D9 RID: 217
	[StructLayout(0, CharSet = 4)]
	public struct MidiInCapabilities
	{
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x000104E8 File Offset: 0x0000E6E8
		public Manufacturers Manufacturer
		{
			get
			{
				return (Manufacturers)this.manufacturerId;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x000104F0 File Offset: 0x0000E6F0
		public int ProductId
		{
			get
			{
				return (int)this.productId;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x000104F8 File Offset: 0x0000E6F8
		public string ProductName
		{
			get
			{
				return this.productName;
			}
		}

		// Token: 0x04000589 RID: 1417
		private const int MaxProductNameLength = 32;

		// Token: 0x0400058A RID: 1418
		private ushort manufacturerId;

		// Token: 0x0400058B RID: 1419
		private ushort productId;

		// Token: 0x0400058C RID: 1420
		private uint driverVersion;

		// Token: 0x0400058D RID: 1421
		[MarshalAs(23, SizeConst = 32)]
		private string productName;

		// Token: 0x0400058E RID: 1422
		private int support;
	}
}
