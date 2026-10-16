using System;
using System.Runtime.InteropServices;

namespace NAudio.Mixer
{
	// Token: 0x020000FE RID: 254
	internal class MixerInterop
	{
		// Token: 0x060005AF RID: 1455
		[DllImport("winmm.dll", CharSet = 2)]
		public static extern int mixerGetNumDevs();

		// Token: 0x060005B0 RID: 1456
		[DllImport("winmm.dll", CharSet = 2)]
		public static extern MmResult mixerOpen(out IntPtr hMixer, int uMxId, IntPtr dwCallback, IntPtr dwInstance, MixerFlags dwOpenFlags);

		// Token: 0x060005B1 RID: 1457
		[DllImport("winmm.dll", CharSet = 2)]
		public static extern MmResult mixerClose(IntPtr hMixer);

		// Token: 0x060005B2 RID: 1458
		[DllImport("winmm.dll", CharSet = 2)]
		public static extern MmResult mixerGetControlDetails(IntPtr hMixer, ref MixerInterop.MIXERCONTROLDETAILS mixerControlDetails, MixerFlags dwDetailsFlags);

		// Token: 0x060005B3 RID: 1459
		[DllImport("winmm.dll", CharSet = 2)]
		public static extern MmResult mixerGetDevCaps(IntPtr nMixerID, ref MixerInterop.MIXERCAPS mixerCaps, int mixerCapsSize);

		// Token: 0x060005B4 RID: 1460
		[DllImport("winmm.dll", CharSet = 2)]
		public static extern MmResult mixerGetID(IntPtr hMixer, out int mixerID, MixerFlags dwMixerIDFlags);

		// Token: 0x060005B5 RID: 1461
		[DllImport("winmm.dll", CharSet = 2)]
		public static extern MmResult mixerGetLineControls(IntPtr hMixer, ref MixerInterop.MIXERLINECONTROLS mixerLineControls, MixerFlags dwControlFlags);

		// Token: 0x060005B6 RID: 1462
		[DllImport("winmm.dll", CharSet = 2)]
		public static extern MmResult mixerGetLineInfo(IntPtr hMixer, ref MixerInterop.MIXERLINE mixerLine, MixerFlags dwInfoFlags);

		// Token: 0x060005B7 RID: 1463
		[DllImport("winmm.dll", CharSet = 2)]
		public static extern MmResult mixerMessage(IntPtr hMixer, uint nMessage, IntPtr dwParam1, IntPtr dwParam2);

		// Token: 0x060005B8 RID: 1464
		[DllImport("winmm.dll", CharSet = 2)]
		public static extern MmResult mixerSetControlDetails(IntPtr hMixer, ref MixerInterop.MIXERCONTROLDETAILS mixerControlDetails, MixerFlags dwDetailsFlags);

		// Token: 0x0400063D RID: 1597
		public const uint MIXERCONTROL_CONTROLF_UNIFORM = 1U;

		// Token: 0x0400063E RID: 1598
		public const uint MIXERCONTROL_CONTROLF_MULTIPLE = 2U;

		// Token: 0x0400063F RID: 1599
		public const uint MIXERCONTROL_CONTROLF_DISABLED = 2147483648U;

		// Token: 0x04000640 RID: 1600
		public const int MAXPNAMELEN = 32;

		// Token: 0x04000641 RID: 1601
		public const int MIXER_SHORT_NAME_CHARS = 16;

		// Token: 0x04000642 RID: 1602
		public const int MIXER_LONG_NAME_CHARS = 64;

		// Token: 0x020000FF RID: 255
		[StructLayout(0, CharSet = 4, Pack = 1)]
		public struct MIXERCONTROLDETAILS
		{
			// Token: 0x04000643 RID: 1603
			public int cbStruct;

			// Token: 0x04000644 RID: 1604
			public int dwControlID;

			// Token: 0x04000645 RID: 1605
			public int cChannels;

			// Token: 0x04000646 RID: 1606
			public IntPtr hwndOwner;

			// Token: 0x04000647 RID: 1607
			public int cbDetails;

			// Token: 0x04000648 RID: 1608
			public IntPtr paDetails;
		}

		// Token: 0x02000100 RID: 256
		[StructLayout(0, Pack = 1)]
		public struct MIXERCAPS
		{
			// Token: 0x04000649 RID: 1609
			public ushort wMid;

			// Token: 0x0400064A RID: 1610
			public ushort wPid;

			// Token: 0x0400064B RID: 1611
			public uint vDriverVersion;

			// Token: 0x0400064C RID: 1612
			[MarshalAs(23, SizeConst = 32)]
			public string szPname;

			// Token: 0x0400064D RID: 1613
			public uint fdwSupport;

			// Token: 0x0400064E RID: 1614
			public uint cDestinations;
		}

		// Token: 0x02000101 RID: 257
		[StructLayout(0, Pack = 1)]
		public struct MIXERLINECONTROLS
		{
			// Token: 0x0400064F RID: 1615
			public int cbStruct;

			// Token: 0x04000650 RID: 1616
			public int dwLineID;

			// Token: 0x04000651 RID: 1617
			public int dwControlID;

			// Token: 0x04000652 RID: 1618
			public int cControls;

			// Token: 0x04000653 RID: 1619
			public int cbmxctrl;

			// Token: 0x04000654 RID: 1620
			public IntPtr pamxctrl;
		}

		// Token: 0x02000102 RID: 258
		[Flags]
		public enum MIXERLINE_LINEF
		{
			// Token: 0x04000656 RID: 1622
			MIXERLINE_LINEF_ACTIVE = 1,
			// Token: 0x04000657 RID: 1623
			MIXERLINE_LINEF_DISCONNECTED = 32768,
			// Token: 0x04000658 RID: 1624
			MIXERLINE_LINEF_SOURCE = -2147483648
		}

		// Token: 0x02000103 RID: 259
		[StructLayout(0, Pack = 1)]
		public struct MIXERLINE
		{
			// Token: 0x04000659 RID: 1625
			public int cbStruct;

			// Token: 0x0400065A RID: 1626
			public int dwDestination;

			// Token: 0x0400065B RID: 1627
			public int dwSource;

			// Token: 0x0400065C RID: 1628
			public int dwLineID;

			// Token: 0x0400065D RID: 1629
			public MixerInterop.MIXERLINE_LINEF fdwLine;

			// Token: 0x0400065E RID: 1630
			public IntPtr dwUser;

			// Token: 0x0400065F RID: 1631
			public MixerLineComponentType dwComponentType;

			// Token: 0x04000660 RID: 1632
			public int cChannels;

			// Token: 0x04000661 RID: 1633
			public int cConnections;

			// Token: 0x04000662 RID: 1634
			public int cControls;

			// Token: 0x04000663 RID: 1635
			[MarshalAs(23, SizeConst = 16)]
			public string szShortName;

			// Token: 0x04000664 RID: 1636
			[MarshalAs(23, SizeConst = 64)]
			public string szName;

			// Token: 0x04000665 RID: 1637
			public uint dwType;

			// Token: 0x04000666 RID: 1638
			public uint dwDeviceID;

			// Token: 0x04000667 RID: 1639
			public ushort wMid;

			// Token: 0x04000668 RID: 1640
			public ushort wPid;

			// Token: 0x04000669 RID: 1641
			public uint vDriverVersion;

			// Token: 0x0400066A RID: 1642
			[MarshalAs(23, SizeConst = 32)]
			public string szPname;
		}

		// Token: 0x02000104 RID: 260
		[StructLayout(0, Pack = 1)]
		public struct Bounds
		{
			// Token: 0x0400066B RID: 1643
			public int minimum;

			// Token: 0x0400066C RID: 1644
			public int maximum;

			// Token: 0x0400066D RID: 1645
			public int reserved2;

			// Token: 0x0400066E RID: 1646
			public int reserved3;

			// Token: 0x0400066F RID: 1647
			public int reserved4;

			// Token: 0x04000670 RID: 1648
			public int reserved5;
		}

		// Token: 0x02000105 RID: 261
		[StructLayout(0, Pack = 1)]
		public struct Metrics
		{
			// Token: 0x04000671 RID: 1649
			public int step;

			// Token: 0x04000672 RID: 1650
			public int customData;

			// Token: 0x04000673 RID: 1651
			public int reserved2;

			// Token: 0x04000674 RID: 1652
			public int reserved3;

			// Token: 0x04000675 RID: 1653
			public int reserved4;

			// Token: 0x04000676 RID: 1654
			public int reserved5;
		}

		// Token: 0x02000106 RID: 262
		[StructLayout(0, Pack = 1)]
		public struct MIXERCONTROL
		{
			// Token: 0x04000677 RID: 1655
			public uint cbStruct;

			// Token: 0x04000678 RID: 1656
			public int dwControlID;

			// Token: 0x04000679 RID: 1657
			public MixerControlType dwControlType;

			// Token: 0x0400067A RID: 1658
			public uint fdwControl;

			// Token: 0x0400067B RID: 1659
			public uint cMultipleItems;

			// Token: 0x0400067C RID: 1660
			[MarshalAs(23, SizeConst = 16)]
			public string szShortName;

			// Token: 0x0400067D RID: 1661
			[MarshalAs(23, SizeConst = 64)]
			public string szName;

			// Token: 0x0400067E RID: 1662
			public MixerInterop.Bounds Bounds;

			// Token: 0x0400067F RID: 1663
			public MixerInterop.Metrics Metrics;
		}

		// Token: 0x02000107 RID: 263
		public struct MIXERCONTROLDETAILS_BOOLEAN
		{
			// Token: 0x04000680 RID: 1664
			public int fValue;
		}

		// Token: 0x02000108 RID: 264
		public struct MIXERCONTROLDETAILS_SIGNED
		{
			// Token: 0x04000681 RID: 1665
			public int lValue;
		}

		// Token: 0x02000109 RID: 265
		[StructLayout(0, Pack = 1)]
		public struct MIXERCONTROLDETAILS_LISTTEXT
		{
			// Token: 0x04000682 RID: 1666
			public uint dwParam1;

			// Token: 0x04000683 RID: 1667
			public uint dwParam2;

			// Token: 0x04000684 RID: 1668
			[MarshalAs(23, SizeConst = 64)]
			public string szName;
		}

		// Token: 0x0200010A RID: 266
		public struct MIXERCONTROLDETAILS_UNSIGNED
		{
			// Token: 0x04000685 RID: 1669
			public uint dwValue;
		}
	}
}
