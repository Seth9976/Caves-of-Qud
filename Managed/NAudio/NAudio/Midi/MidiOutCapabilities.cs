using System;
using System.Runtime.InteropServices;

namespace NAudio.Midi
{
	// Token: 0x020000E5 RID: 229
	[StructLayout(0, CharSet = 4)]
	public struct MidiOutCapabilities
	{
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x000107BC File Offset: 0x0000E9BC
		public Manufacturers Manufacturer
		{
			get
			{
				return (Manufacturers)this.manufacturerId;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x000107C4 File Offset: 0x0000E9C4
		public short ProductId
		{
			get
			{
				return this.productId;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x000107CC File Offset: 0x0000E9CC
		public string ProductName
		{
			get
			{
				return this.productName;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x000107D4 File Offset: 0x0000E9D4
		public int Voices
		{
			get
			{
				return (int)this.wVoices;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x000107DC File Offset: 0x0000E9DC
		public int Notes
		{
			get
			{
				return (int)this.wNotes;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x000107E4 File Offset: 0x0000E9E4
		public bool SupportsAllChannels
		{
			get
			{
				return this.wChannelMask == ushort.MaxValue;
			}
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x000107F3 File Offset: 0x0000E9F3
		public bool SupportsChannel(int channel)
		{
			return ((int)this.wChannelMask & (1 << channel - 1)) > 0;
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x00010807 File Offset: 0x0000EA07
		public bool SupportsPatchCaching
		{
			get
			{
				return (this.dwSupport & MidiOutCapabilities.MidiOutCapabilityFlags.PatchCaching) != (MidiOutCapabilities.MidiOutCapabilityFlags)0;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x00010817 File Offset: 0x0000EA17
		public bool SupportsSeparateLeftAndRightVolume
		{
			get
			{
				return (this.dwSupport & MidiOutCapabilities.MidiOutCapabilityFlags.LeftRightVolume) != (MidiOutCapabilities.MidiOutCapabilityFlags)0;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x00010827 File Offset: 0x0000EA27
		public bool SupportsMidiStreamOut
		{
			get
			{
				return (this.dwSupport & MidiOutCapabilities.MidiOutCapabilityFlags.Stream) != (MidiOutCapabilities.MidiOutCapabilityFlags)0;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x00010837 File Offset: 0x0000EA37
		public bool SupportsVolumeControl
		{
			get
			{
				return (this.dwSupport & MidiOutCapabilities.MidiOutCapabilityFlags.Volume) != (MidiOutCapabilities.MidiOutCapabilityFlags)0;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00010847 File Offset: 0x0000EA47
		public MidiOutTechnology Technology
		{
			get
			{
				return (MidiOutTechnology)this.wTechnology;
			}
		}

		// Token: 0x040005B2 RID: 1458
		private const int MaxProductNameLength = 32;

		// Token: 0x040005B3 RID: 1459
		private short manufacturerId;

		// Token: 0x040005B4 RID: 1460
		private short productId;

		// Token: 0x040005B5 RID: 1461
		private int driverVersion;

		// Token: 0x040005B6 RID: 1462
		[MarshalAs(23, SizeConst = 32)]
		private string productName;

		// Token: 0x040005B7 RID: 1463
		private short wTechnology;

		// Token: 0x040005B8 RID: 1464
		private short wVoices;

		// Token: 0x040005B9 RID: 1465
		private short wNotes;

		// Token: 0x040005BA RID: 1466
		private ushort wChannelMask;

		// Token: 0x040005BB RID: 1467
		private MidiOutCapabilities.MidiOutCapabilityFlags dwSupport;

		// Token: 0x020000E6 RID: 230
		[Flags]
		private enum MidiOutCapabilityFlags
		{
			// Token: 0x040005BD RID: 1469
			Volume = 1,
			// Token: 0x040005BE RID: 1470
			LeftRightVolume = 2,
			// Token: 0x040005BF RID: 1471
			PatchCaching = 4,
			// Token: 0x040005C0 RID: 1472
			Stream = 8
		}
	}
}
