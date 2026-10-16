using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Compression
{
	// Token: 0x02000165 RID: 357
	internal class AcmInterop
	{
		// Token: 0x06000749 RID: 1865
		[DllImport("msacm32.dll")]
		public static extern MmResult acmDriverAdd(out IntPtr driverHandle, IntPtr driverModule, IntPtr driverFunctionAddress, int priority, AcmDriverAddFlags flags);

		// Token: 0x0600074A RID: 1866
		[DllImport("msacm32.dll")]
		public static extern MmResult acmDriverRemove(IntPtr driverHandle, int removeFlags);

		// Token: 0x0600074B RID: 1867
		[DllImport("Msacm32.dll")]
		public static extern MmResult acmDriverClose(IntPtr hAcmDriver, int closeFlags);

		// Token: 0x0600074C RID: 1868
		[DllImport("Msacm32.dll")]
		public static extern MmResult acmDriverEnum(AcmInterop.AcmDriverEnumCallback fnCallback, IntPtr dwInstance, AcmDriverEnumFlags flags);

		// Token: 0x0600074D RID: 1869
		[DllImport("Msacm32.dll")]
		public static extern MmResult acmDriverDetails(IntPtr hAcmDriver, ref AcmDriverDetails driverDetails, int reserved);

		// Token: 0x0600074E RID: 1870
		[DllImport("Msacm32.dll")]
		public static extern MmResult acmDriverOpen(out IntPtr pAcmDriver, IntPtr hAcmDriverId, int openFlags);

		// Token: 0x0600074F RID: 1871
		[DllImport("Msacm32.dll", EntryPoint = "acmFormatChooseW")]
		public static extern MmResult acmFormatChoose(ref AcmFormatChoose formatChoose);

		// Token: 0x06000750 RID: 1872
		[DllImport("Msacm32.dll")]
		public static extern MmResult acmFormatEnum(IntPtr hAcmDriver, ref AcmFormatDetails formatDetails, AcmInterop.AcmFormatEnumCallback callback, IntPtr instance, AcmFormatEnumFlags flags);

		// Token: 0x06000751 RID: 1873
		[DllImport("Msacm32.dll")]
		public static extern MmResult acmFormatSuggest(IntPtr hAcmDriver, [MarshalAs(44, MarshalTypeRef = NAudio.Wave.WaveFormatCustomMarshaler)] [In] WaveFormat sourceFormat, [MarshalAs(44, MarshalTypeRef = NAudio.Wave.WaveFormatCustomMarshaler)] [In] [Out] WaveFormat destFormat, int sizeDestFormat, AcmFormatSuggestFlags suggestFlags);

		// Token: 0x06000752 RID: 1874
		[DllImport("Msacm32.dll", EntryPoint = "acmFormatSuggest")]
		public static extern MmResult acmFormatSuggest2(IntPtr hAcmDriver, IntPtr sourceFormatPointer, IntPtr destFormatPointer, int sizeDestFormat, AcmFormatSuggestFlags suggestFlags);

		// Token: 0x06000753 RID: 1875
		[DllImport("Msacm32.dll")]
		public static extern MmResult acmFormatTagEnum(IntPtr hAcmDriver, ref AcmFormatTagDetails formatTagDetails, AcmInterop.AcmFormatTagEnumCallback callback, IntPtr instance, int reserved);

		// Token: 0x06000754 RID: 1876
		[DllImport("Msacm32.dll")]
		public static extern MmResult acmMetrics(IntPtr hAcmObject, AcmMetrics metric, out int output);

		// Token: 0x06000755 RID: 1877
		[DllImport("Msacm32.dll")]
		public static extern MmResult acmStreamOpen(out IntPtr hAcmStream, IntPtr hAcmDriver, [MarshalAs(44, MarshalTypeRef = NAudio.Wave.WaveFormatCustomMarshaler)] [In] WaveFormat sourceFormat, [MarshalAs(44, MarshalTypeRef = NAudio.Wave.WaveFormatCustomMarshaler)] [In] WaveFormat destFormat, [In] WaveFilter waveFilter, IntPtr callback, IntPtr instance, AcmStreamOpenFlags openFlags);

		// Token: 0x06000756 RID: 1878
		[DllImport("Msacm32.dll", EntryPoint = "acmStreamOpen")]
		public static extern MmResult acmStreamOpen2(out IntPtr hAcmStream, IntPtr hAcmDriver, IntPtr sourceFormatPointer, IntPtr destFormatPointer, [In] WaveFilter waveFilter, IntPtr callback, IntPtr instance, AcmStreamOpenFlags openFlags);

		// Token: 0x06000757 RID: 1879
		[DllImport("Msacm32.dll")]
		public static extern MmResult acmStreamClose(IntPtr hAcmStream, int closeFlags);

		// Token: 0x06000758 RID: 1880
		[DllImport("Msacm32.dll")]
		public static extern MmResult acmStreamConvert(IntPtr hAcmStream, [In] [Out] AcmStreamHeaderStruct streamHeader, AcmStreamConvertFlags streamConvertFlags);

		// Token: 0x06000759 RID: 1881
		[DllImport("Msacm32.dll")]
		public static extern MmResult acmStreamPrepareHeader(IntPtr hAcmStream, [In] [Out] AcmStreamHeaderStruct streamHeader, int prepareFlags);

		// Token: 0x0600075A RID: 1882
		[DllImport("Msacm32.dll")]
		public static extern MmResult acmStreamReset(IntPtr hAcmStream, int resetFlags);

		// Token: 0x0600075B RID: 1883
		[DllImport("Msacm32.dll")]
		public static extern MmResult acmStreamSize(IntPtr hAcmStream, int inputBufferSize, out int outputBufferSize, AcmStreamSizeFlags flags);

		// Token: 0x0600075C RID: 1884
		[DllImport("Msacm32.dll")]
		public static extern MmResult acmStreamUnprepareHeader(IntPtr hAcmStream, [In] [Out] AcmStreamHeaderStruct streamHeader, int flags);

		// Token: 0x02000166 RID: 358
		// (Invoke) Token: 0x0600075F RID: 1887
		public delegate bool AcmDriverEnumCallback(IntPtr hAcmDriverId, IntPtr instance, AcmDriverDetailsSupportFlags flags);

		// Token: 0x02000167 RID: 359
		// (Invoke) Token: 0x06000763 RID: 1891
		public delegate bool AcmFormatEnumCallback(IntPtr hAcmDriverId, ref AcmFormatDetails formatDetails, IntPtr dwInstance, AcmDriverDetailsSupportFlags flags);

		// Token: 0x02000168 RID: 360
		// (Invoke) Token: 0x06000767 RID: 1895
		public delegate bool AcmFormatTagEnumCallback(IntPtr hAcmDriverId, ref AcmFormatTagDetails formatTagDetails, IntPtr dwInstance, AcmDriverDetailsSupportFlags flags);

		// Token: 0x02000169 RID: 361
		// (Invoke) Token: 0x0600076B RID: 1899
		public delegate bool AcmFormatChooseHookProc(IntPtr windowHandle, int message, IntPtr wParam, IntPtr lParam);
	}
}
