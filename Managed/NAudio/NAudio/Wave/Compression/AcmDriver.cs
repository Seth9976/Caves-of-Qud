using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using NAudio.Utils;

namespace NAudio.Wave.Compression
{
	// Token: 0x02000159 RID: 345
	public class AcmDriver : IDisposable
	{
		// Token: 0x06000727 RID: 1831 RVA: 0x000152B0 File Offset: 0x000134B0
		public static bool IsCodecInstalled(string shortName)
		{
			foreach (AcmDriver acmDriver in AcmDriver.EnumerateAcmDrivers())
			{
				if (acmDriver.ShortName == shortName)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0001530C File Offset: 0x0001350C
		public static AcmDriver AddLocalDriver(string driverFile)
		{
			IntPtr intPtr = NativeMethods.LoadLibrary(driverFile);
			if (intPtr == IntPtr.Zero)
			{
				throw new ArgumentException("Failed to load driver file");
			}
			IntPtr procAddress = NativeMethods.GetProcAddress(intPtr, "DriverProc");
			if (procAddress == IntPtr.Zero)
			{
				NativeMethods.FreeLibrary(intPtr);
				throw new ArgumentException("Failed to discover DriverProc");
			}
			IntPtr intPtr2;
			MmResult mmResult = AcmInterop.acmDriverAdd(out intPtr2, intPtr, procAddress, 0, AcmDriverAddFlags.Function);
			if (mmResult != MmResult.NoError)
			{
				NativeMethods.FreeLibrary(intPtr);
				throw new MmException(mmResult, "acmDriverAdd");
			}
			AcmDriver acmDriver = new AcmDriver(intPtr2);
			if (string.IsNullOrEmpty(acmDriver.details.longName))
			{
				acmDriver.details.longName = "Local driver: " + Path.GetFileName(driverFile);
				acmDriver.localDllHandle = intPtr;
			}
			return acmDriver;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x000153C8 File Offset: 0x000135C8
		public static void RemoveLocalDriver(AcmDriver localDriver)
		{
			if (localDriver.localDllHandle == IntPtr.Zero)
			{
				throw new ArgumentException("Please pass in the AcmDriver returned by the AddLocalDriver method");
			}
			MmResult mmResult = AcmInterop.acmDriverRemove(localDriver.driverId, 0);
			NativeMethods.FreeLibrary(localDriver.localDllHandle);
			MmException.Try(mmResult, "acmDriverRemove");
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00015418 File Offset: 0x00013618
		public static bool ShowFormatChooseDialog(IntPtr ownerWindowHandle, string windowTitle, AcmFormatEnumFlags enumFlags, WaveFormat enumFormat, out WaveFormat selectedFormat, out string selectedFormatDescription, out string selectedFormatTagDescription)
		{
			AcmFormatChoose acmFormatChoose = default(AcmFormatChoose);
			acmFormatChoose.structureSize = Marshal.SizeOf(acmFormatChoose);
			acmFormatChoose.styleFlags = AcmFormatChooseStyleFlags.None;
			acmFormatChoose.ownerWindowHandle = ownerWindowHandle;
			int num = 200;
			acmFormatChoose.selectedWaveFormatPointer = Marshal.AllocHGlobal(num);
			acmFormatChoose.selectedWaveFormatByteSize = num;
			acmFormatChoose.title = windowTitle;
			acmFormatChoose.name = null;
			acmFormatChoose.formatEnumFlags = enumFlags;
			acmFormatChoose.waveFormatEnumPointer = IntPtr.Zero;
			if (enumFormat != null)
			{
				IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(enumFormat));
				Marshal.StructureToPtr(enumFormat, intPtr, false);
				acmFormatChoose.waveFormatEnumPointer = intPtr;
			}
			acmFormatChoose.instanceHandle = IntPtr.Zero;
			acmFormatChoose.templateName = null;
			MmResult mmResult = AcmInterop.acmFormatChoose(ref acmFormatChoose);
			selectedFormat = null;
			selectedFormatDescription = null;
			selectedFormatTagDescription = null;
			if (mmResult == MmResult.NoError)
			{
				selectedFormat = WaveFormat.MarshalFromPtr(acmFormatChoose.selectedWaveFormatPointer);
				selectedFormatDescription = acmFormatChoose.formatDescription;
				selectedFormatTagDescription = acmFormatChoose.formatTagDescription;
			}
			Marshal.FreeHGlobal(acmFormatChoose.waveFormatEnumPointer);
			Marshal.FreeHGlobal(acmFormatChoose.selectedWaveFormatPointer);
			if (mmResult != MmResult.AcmCancelled && mmResult != MmResult.NoError)
			{
				throw new MmException(mmResult, "acmFormatChoose");
			}
			return mmResult == MmResult.NoError;
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x00015530 File Offset: 0x00013730
		public int MaxFormatSize
		{
			get
			{
				int num;
				MmException.Try(AcmInterop.acmMetrics(this.driverHandle, AcmMetrics.MaxSizeFormat, out num), "acmMetrics");
				return num;
			}
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00015558 File Offset: 0x00013758
		public static AcmDriver FindByShortName(string shortName)
		{
			foreach (AcmDriver acmDriver in AcmDriver.EnumerateAcmDrivers())
			{
				if (acmDriver.ShortName == shortName)
				{
					return acmDriver;
				}
			}
			return null;
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x000155B4 File Offset: 0x000137B4
		public static IEnumerable<AcmDriver> EnumerateAcmDrivers()
		{
			AcmDriver.drivers = new List<AcmDriver>();
			MmException.Try(AcmInterop.acmDriverEnum(new AcmInterop.AcmDriverEnumCallback(AcmDriver.DriverEnumCallback), IntPtr.Zero, (AcmDriverEnumFlags)0), "acmDriverEnum");
			return AcmDriver.drivers;
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x000155E6 File Offset: 0x000137E6
		private static bool DriverEnumCallback(IntPtr hAcmDriver, IntPtr dwInstance, AcmDriverDetailsSupportFlags flags)
		{
			AcmDriver.drivers.Add(new AcmDriver(hAcmDriver));
			return true;
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x000155FC File Offset: 0x000137FC
		private AcmDriver(IntPtr hAcmDriver)
		{
			this.driverId = hAcmDriver;
			this.details = default(AcmDriverDetails);
			this.details.structureSize = Marshal.SizeOf(this.details);
			MmException.Try(AcmInterop.acmDriverDetails(hAcmDriver, ref this.details, 0), "acmDriverDetails");
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x00015654 File Offset: 0x00013854
		public string ShortName
		{
			get
			{
				return this.details.shortName;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x00015661 File Offset: 0x00013861
		public string LongName
		{
			get
			{
				return this.details.longName;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x0001566E File Offset: 0x0001386E
		public IntPtr DriverId
		{
			get
			{
				return this.driverId;
			}
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00015676 File Offset: 0x00013876
		public override string ToString()
		{
			return this.LongName;
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x00015680 File Offset: 0x00013880
		public IEnumerable<AcmFormatTag> FormatTags
		{
			get
			{
				if (this.formatTags == null)
				{
					if (this.driverHandle == IntPtr.Zero)
					{
						throw new InvalidOperationException("Driver must be opened first");
					}
					this.formatTags = new List<AcmFormatTag>();
					AcmFormatTagDetails acmFormatTagDetails = default(AcmFormatTagDetails);
					acmFormatTagDetails.structureSize = Marshal.SizeOf(acmFormatTagDetails);
					MmException.Try(AcmInterop.acmFormatTagEnum(this.driverHandle, ref acmFormatTagDetails, new AcmInterop.AcmFormatTagEnumCallback(this.AcmFormatTagEnumCallback), IntPtr.Zero, 0), "acmFormatTagEnum");
				}
				return this.formatTags;
			}
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00015708 File Offset: 0x00013908
		public IEnumerable<AcmFormat> GetFormats(AcmFormatTag formatTag)
		{
			if (this.driverHandle == IntPtr.Zero)
			{
				throw new InvalidOperationException("Driver must be opened first");
			}
			this.tempFormatsList = new List<AcmFormat>();
			AcmFormatDetails acmFormatDetails = default(AcmFormatDetails);
			acmFormatDetails.structSize = Marshal.SizeOf(acmFormatDetails);
			acmFormatDetails.waveFormatByteSize = 1024;
			acmFormatDetails.waveFormatPointer = Marshal.AllocHGlobal(acmFormatDetails.waveFormatByteSize);
			acmFormatDetails.formatTag = (int)formatTag.FormatTag;
			MmResult mmResult = AcmInterop.acmFormatEnum(this.driverHandle, ref acmFormatDetails, new AcmInterop.AcmFormatEnumCallback(this.AcmFormatEnumCallback), IntPtr.Zero, AcmFormatEnumFlags.None);
			Marshal.FreeHGlobal(acmFormatDetails.waveFormatPointer);
			MmException.Try(mmResult, "acmFormatEnum");
			return this.tempFormatsList;
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x000157C0 File Offset: 0x000139C0
		public void Open()
		{
			if (this.driverHandle == IntPtr.Zero)
			{
				MmException.Try(AcmInterop.acmDriverOpen(out this.driverHandle, this.DriverId, 0), "acmDriverOpen");
			}
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x000157F0 File Offset: 0x000139F0
		public void Close()
		{
			if (this.driverHandle != IntPtr.Zero)
			{
				MmException.Try(AcmInterop.acmDriverClose(this.driverHandle, 0), "acmDriverClose");
				this.driverHandle = IntPtr.Zero;
			}
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00015825 File Offset: 0x00013A25
		private bool AcmFormatTagEnumCallback(IntPtr hAcmDriverId, ref AcmFormatTagDetails formatTagDetails, IntPtr dwInstance, AcmDriverDetailsSupportFlags flags)
		{
			this.formatTags.Add(new AcmFormatTag(formatTagDetails));
			return true;
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0001583E File Offset: 0x00013A3E
		private bool AcmFormatEnumCallback(IntPtr hAcmDriverId, ref AcmFormatDetails formatDetails, IntPtr dwInstance, AcmDriverDetailsSupportFlags flags)
		{
			this.tempFormatsList.Add(new AcmFormat(formatDetails));
			return true;
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00015857 File Offset: 0x00013A57
		public void Dispose()
		{
			if (this.driverHandle != IntPtr.Zero)
			{
				this.Close();
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x0400078F RID: 1935
		private static List<AcmDriver> drivers;

		// Token: 0x04000790 RID: 1936
		private AcmDriverDetails details;

		// Token: 0x04000791 RID: 1937
		private IntPtr driverId;

		// Token: 0x04000792 RID: 1938
		private IntPtr driverHandle;

		// Token: 0x04000793 RID: 1939
		private List<AcmFormatTag> formatTags;

		// Token: 0x04000794 RID: 1940
		private List<AcmFormat> tempFormatsList;

		// Token: 0x04000795 RID: 1941
		private IntPtr localDllHandle;
	}
}
