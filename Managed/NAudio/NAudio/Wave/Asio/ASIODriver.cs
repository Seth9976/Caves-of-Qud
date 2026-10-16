using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;

namespace NAudio.Wave.Asio
{
	// Token: 0x0200012E RID: 302
	internal class ASIODriver
	{
		// Token: 0x06000675 RID: 1653 RVA: 0x00013F20 File Offset: 0x00012120
		private ASIODriver()
		{
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00013F28 File Offset: 0x00012128
		public static string[] GetASIODriverNames()
		{
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\ASIO");
			string[] array = new string[0];
			if (registryKey != null)
			{
				array = registryKey.GetSubKeyNames();
				registryKey.Close();
			}
			return array;
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00013F60 File Offset: 0x00012160
		public static ASIODriver GetASIODriverByName(string name)
		{
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\ASIO\\" + name);
			if (registryKey == null)
			{
				throw new ArgumentException(string.Format("Driver Name {0} doesn't exist", name));
			}
			string text = registryKey.GetValue("CLSID").ToString();
			return ASIODriver.GetASIODriverByGuid(new Guid(text));
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00013FB4 File Offset: 0x000121B4
		public static ASIODriver GetASIODriverByGuid(Guid guid)
		{
			ASIODriver asiodriver = new ASIODriver();
			asiodriver.initFromGuid(guid);
			return asiodriver;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00013FD0 File Offset: 0x000121D0
		public bool init(IntPtr sysHandle)
		{
			int num = this.asioDriverVTable.init(this.pASIOComObject, sysHandle);
			return num == 1;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00013FFC File Offset: 0x000121FC
		public string getDriverName()
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			this.asioDriverVTable.getDriverName(this.pASIOComObject, stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00014031 File Offset: 0x00012231
		public int getDriverVersion()
		{
			return this.asioDriverVTable.getDriverVersion(this.pASIOComObject);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0001404C File Offset: 0x0001224C
		public string getErrorMessage()
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			this.asioDriverVTable.getErrorMessage(this.pASIOComObject, stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00014081 File Offset: 0x00012281
		public void start()
		{
			this.handleException(this.asioDriverVTable.start(this.pASIOComObject), "start");
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x000140A4 File Offset: 0x000122A4
		public ASIOError stop()
		{
			return this.asioDriverVTable.stop(this.pASIOComObject);
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x000140BC File Offset: 0x000122BC
		public void getChannels(out int numInputChannels, out int numOutputChannels)
		{
			this.handleException(this.asioDriverVTable.getChannels(this.pASIOComObject, out numInputChannels, out numOutputChannels), "getChannels");
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x000140E1 File Offset: 0x000122E1
		public ASIOError GetLatencies(out int inputLatency, out int outputLatency)
		{
			return this.asioDriverVTable.getLatencies(this.pASIOComObject, out inputLatency, out outputLatency);
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x000140FB File Offset: 0x000122FB
		public void getBufferSize(out int minSize, out int maxSize, out int preferredSize, out int granularity)
		{
			this.handleException(this.asioDriverVTable.getBufferSize(this.pASIOComObject, out minSize, out maxSize, out preferredSize, out granularity), "getBufferSize");
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00014124 File Offset: 0x00012324
		public bool canSampleRate(double sampleRate)
		{
			ASIOError asioerror = this.asioDriverVTable.canSampleRate(this.pASIOComObject, sampleRate);
			if (asioerror == ASIOError.ASE_NoClock)
			{
				return false;
			}
			if (asioerror == ASIOError.ASE_OK)
			{
				return true;
			}
			this.handleException(asioerror, "canSampleRate");
			return false;
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00014168 File Offset: 0x00012368
		public double getSampleRate()
		{
			double num;
			this.handleException(this.asioDriverVTable.getSampleRate(this.pASIOComObject, out num), "getSampleRate");
			return num;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00014199 File Offset: 0x00012399
		public void setSampleRate(double sampleRate)
		{
			this.handleException(this.asioDriverVTable.setSampleRate(this.pASIOComObject, sampleRate), "setSampleRate");
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x000141BD File Offset: 0x000123BD
		public void getClockSources(out long clocks, int numSources)
		{
			this.handleException(this.asioDriverVTable.getClockSources(this.pASIOComObject, out clocks, numSources), "getClockSources");
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x000141E2 File Offset: 0x000123E2
		public void setClockSource(int reference)
		{
			this.handleException(this.asioDriverVTable.setClockSource(this.pASIOComObject, reference), "setClockSources");
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00014206 File Offset: 0x00012406
		public void getSamplePosition(out long samplePos, ref ASIO64Bit timeStamp)
		{
			this.handleException(this.asioDriverVTable.getSamplePosition(this.pASIOComObject, out samplePos, ref timeStamp), "getSamplePosition");
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001422C File Offset: 0x0001242C
		public ASIOChannelInfo getChannelInfo(int channelNumber, bool trueForInputInfo)
		{
			ASIOChannelInfo asiochannelInfo = new ASIOChannelInfo
			{
				channel = channelNumber,
				isInput = trueForInputInfo
			};
			this.handleException(this.asioDriverVTable.getChannelInfo(this.pASIOComObject, ref asiochannelInfo), "getChannelInfo");
			return asiochannelInfo;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00014278 File Offset: 0x00012478
		public void createBuffers(IntPtr bufferInfos, int numChannels, int bufferSize, ref ASIOCallbacks callbacks)
		{
			this.pinnedcallbacks = Marshal.AllocHGlobal(Marshal.SizeOf(callbacks));
			Marshal.StructureToPtr(callbacks, this.pinnedcallbacks, false);
			this.handleException(this.asioDriverVTable.createBuffers(this.pASIOComObject, bufferInfos, numChannels, bufferSize, this.pinnedcallbacks), "createBuffers");
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x000142E4 File Offset: 0x000124E4
		public ASIOError disposeBuffers()
		{
			ASIOError asioerror = this.asioDriverVTable.disposeBuffers(this.pASIOComObject);
			Marshal.FreeHGlobal(this.pinnedcallbacks);
			return asioerror;
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x00014314 File Offset: 0x00012514
		public void controlPanel()
		{
			this.handleException(this.asioDriverVTable.controlPanel(this.pASIOComObject), "controlPanel");
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x00014337 File Offset: 0x00012537
		public void future(int selector, IntPtr opt)
		{
			this.handleException(this.asioDriverVTable.future(this.pASIOComObject, selector, opt), "future");
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0001435C File Offset: 0x0001255C
		public ASIOError outputReady()
		{
			return this.asioDriverVTable.outputReady(this.pASIOComObject);
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00014374 File Offset: 0x00012574
		public void ReleaseComASIODriver()
		{
			Marshal.Release(this.pASIOComObject);
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00014384 File Offset: 0x00012584
		private void handleException(ASIOError error, string methodName)
		{
			if (error != ASIOError.ASE_OK && error != ASIOError.ASE_SUCCESS)
			{
				throw new ASIOException(string.Format("Error code [{0}] while calling ASIO method <{1}>, {2}", ASIOException.getErrorName(error), methodName, this.getErrorMessage()))
				{
					Error = error
				};
			}
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x000143C4 File Offset: 0x000125C4
		private void initFromGuid(Guid ASIOGuid)
		{
			int num = ASIODriver.CoCreateInstance(ref ASIOGuid, IntPtr.Zero, 1U, ref ASIOGuid, out this.pASIOComObject);
			if (num != 0)
			{
				throw new COMException("Unable to instantiate ASIO. Check if STAThread is set", num);
			}
			IntPtr intPtr = Marshal.ReadIntPtr(this.pASIOComObject);
			this.asioDriverVTable = new ASIODriver.ASIODriverVTable();
			FieldInfo[] fields = typeof(ASIODriver.ASIODriverVTable).GetFields();
			for (int i = 0; i < fields.Length; i++)
			{
				FieldInfo fieldInfo = fields[i];
				IntPtr intPtr2 = Marshal.ReadIntPtr(intPtr, (i + 3) * IntPtr.Size);
				object delegateForFunctionPointer = Marshal.GetDelegateForFunctionPointer(intPtr2, fieldInfo.FieldType);
				fieldInfo.SetValue(this.asioDriverVTable, delegateForFunctionPointer);
			}
		}

		// Token: 0x06000691 RID: 1681
		[DllImport("ole32.Dll")]
		private static extern int CoCreateInstance(ref Guid clsid, IntPtr inner, uint context, ref Guid uuid, out IntPtr rReturnedComObject);

		// Token: 0x04000721 RID: 1825
		private IntPtr pASIOComObject;

		// Token: 0x04000722 RID: 1826
		private IntPtr pinnedcallbacks;

		// Token: 0x04000723 RID: 1827
		private ASIODriver.ASIODriverVTable asioDriverVTable;

		// Token: 0x0200012F RID: 303
		[StructLayout(0, Pack = 2)]
		private class ASIODriverVTable
		{
			// Token: 0x04000724 RID: 1828
			public ASIODriver.ASIODriverVTable.ASIOInit init;

			// Token: 0x04000725 RID: 1829
			public ASIODriver.ASIODriverVTable.ASIOgetDriverName getDriverName;

			// Token: 0x04000726 RID: 1830
			public ASIODriver.ASIODriverVTable.ASIOgetDriverVersion getDriverVersion;

			// Token: 0x04000727 RID: 1831
			public ASIODriver.ASIODriverVTable.ASIOgetErrorMessage getErrorMessage;

			// Token: 0x04000728 RID: 1832
			public ASIODriver.ASIODriverVTable.ASIOstart start;

			// Token: 0x04000729 RID: 1833
			public ASIODriver.ASIODriverVTable.ASIOstop stop;

			// Token: 0x0400072A RID: 1834
			public ASIODriver.ASIODriverVTable.ASIOgetChannels getChannels;

			// Token: 0x0400072B RID: 1835
			public ASIODriver.ASIODriverVTable.ASIOgetLatencies getLatencies;

			// Token: 0x0400072C RID: 1836
			public ASIODriver.ASIODriverVTable.ASIOgetBufferSize getBufferSize;

			// Token: 0x0400072D RID: 1837
			public ASIODriver.ASIODriverVTable.ASIOcanSampleRate canSampleRate;

			// Token: 0x0400072E RID: 1838
			public ASIODriver.ASIODriverVTable.ASIOgetSampleRate getSampleRate;

			// Token: 0x0400072F RID: 1839
			public ASIODriver.ASIODriverVTable.ASIOsetSampleRate setSampleRate;

			// Token: 0x04000730 RID: 1840
			public ASIODriver.ASIODriverVTable.ASIOgetClockSources getClockSources;

			// Token: 0x04000731 RID: 1841
			public ASIODriver.ASIODriverVTable.ASIOsetClockSource setClockSource;

			// Token: 0x04000732 RID: 1842
			public ASIODriver.ASIODriverVTable.ASIOgetSamplePosition getSamplePosition;

			// Token: 0x04000733 RID: 1843
			public ASIODriver.ASIODriverVTable.ASIOgetChannelInfo getChannelInfo;

			// Token: 0x04000734 RID: 1844
			public ASIODriver.ASIODriverVTable.ASIOcreateBuffers createBuffers;

			// Token: 0x04000735 RID: 1845
			public ASIODriver.ASIODriverVTable.ASIOdisposeBuffers disposeBuffers;

			// Token: 0x04000736 RID: 1846
			public ASIODriver.ASIODriverVTable.ASIOcontrolPanel controlPanel;

			// Token: 0x04000737 RID: 1847
			public ASIODriver.ASIODriverVTable.ASIOfuture future;

			// Token: 0x04000738 RID: 1848
			public ASIODriver.ASIODriverVTable.ASIOoutputReady outputReady;

			// Token: 0x02000130 RID: 304
			// (Invoke) Token: 0x06000694 RID: 1684
			[UnmanagedFunctionPointer(4)]
			public delegate int ASIOInit(IntPtr _pUnknown, IntPtr sysHandle);

			// Token: 0x02000131 RID: 305
			// (Invoke) Token: 0x06000698 RID: 1688
			[UnmanagedFunctionPointer(4)]
			public delegate void ASIOgetDriverName(IntPtr _pUnknown, StringBuilder name);

			// Token: 0x02000132 RID: 306
			// (Invoke) Token: 0x0600069C RID: 1692
			[UnmanagedFunctionPointer(4)]
			public delegate int ASIOgetDriverVersion(IntPtr _pUnknown);

			// Token: 0x02000133 RID: 307
			// (Invoke) Token: 0x060006A0 RID: 1696
			[UnmanagedFunctionPointer(4)]
			public delegate void ASIOgetErrorMessage(IntPtr _pUnknown, StringBuilder errorMessage);

			// Token: 0x02000134 RID: 308
			// (Invoke) Token: 0x060006A4 RID: 1700
			[UnmanagedFunctionPointer(4)]
			public delegate ASIOError ASIOstart(IntPtr _pUnknown);

			// Token: 0x02000135 RID: 309
			// (Invoke) Token: 0x060006A8 RID: 1704
			[UnmanagedFunctionPointer(4)]
			public delegate ASIOError ASIOstop(IntPtr _pUnknown);

			// Token: 0x02000136 RID: 310
			// (Invoke) Token: 0x060006AC RID: 1708
			[UnmanagedFunctionPointer(4)]
			public delegate ASIOError ASIOgetChannels(IntPtr _pUnknown, out int numInputChannels, out int numOutputChannels);

			// Token: 0x02000137 RID: 311
			// (Invoke) Token: 0x060006B0 RID: 1712
			[UnmanagedFunctionPointer(4)]
			public delegate ASIOError ASIOgetLatencies(IntPtr _pUnknown, out int inputLatency, out int outputLatency);

			// Token: 0x02000138 RID: 312
			// (Invoke) Token: 0x060006B4 RID: 1716
			[UnmanagedFunctionPointer(4)]
			public delegate ASIOError ASIOgetBufferSize(IntPtr _pUnknown, out int minSize, out int maxSize, out int preferredSize, out int granularity);

			// Token: 0x02000139 RID: 313
			// (Invoke) Token: 0x060006B8 RID: 1720
			[UnmanagedFunctionPointer(4)]
			public delegate ASIOError ASIOcanSampleRate(IntPtr _pUnknown, double sampleRate);

			// Token: 0x0200013A RID: 314
			// (Invoke) Token: 0x060006BC RID: 1724
			[UnmanagedFunctionPointer(4)]
			public delegate ASIOError ASIOgetSampleRate(IntPtr _pUnknown, out double sampleRate);

			// Token: 0x0200013B RID: 315
			// (Invoke) Token: 0x060006C0 RID: 1728
			[UnmanagedFunctionPointer(4)]
			public delegate ASIOError ASIOsetSampleRate(IntPtr _pUnknown, double sampleRate);

			// Token: 0x0200013C RID: 316
			// (Invoke) Token: 0x060006C4 RID: 1732
			[UnmanagedFunctionPointer(4)]
			public delegate ASIOError ASIOgetClockSources(IntPtr _pUnknown, out long clocks, int numSources);

			// Token: 0x0200013D RID: 317
			// (Invoke) Token: 0x060006C8 RID: 1736
			[UnmanagedFunctionPointer(4)]
			public delegate ASIOError ASIOsetClockSource(IntPtr _pUnknown, int reference);

			// Token: 0x0200013E RID: 318
			// (Invoke) Token: 0x060006CC RID: 1740
			[UnmanagedFunctionPointer(4)]
			public delegate ASIOError ASIOgetSamplePosition(IntPtr _pUnknown, out long samplePos, ref ASIO64Bit timeStamp);

			// Token: 0x0200013F RID: 319
			// (Invoke) Token: 0x060006D0 RID: 1744
			[UnmanagedFunctionPointer(4)]
			public delegate ASIOError ASIOgetChannelInfo(IntPtr _pUnknown, ref ASIOChannelInfo info);

			// Token: 0x02000140 RID: 320
			// (Invoke) Token: 0x060006D4 RID: 1748
			[UnmanagedFunctionPointer(4)]
			public delegate ASIOError ASIOcreateBuffers(IntPtr _pUnknown, IntPtr bufferInfos, int numChannels, int bufferSize, IntPtr callbacks);

			// Token: 0x02000141 RID: 321
			// (Invoke) Token: 0x060006D8 RID: 1752
			[UnmanagedFunctionPointer(4)]
			public delegate ASIOError ASIOdisposeBuffers(IntPtr _pUnknown);

			// Token: 0x02000142 RID: 322
			// (Invoke) Token: 0x060006DC RID: 1756
			[UnmanagedFunctionPointer(4)]
			public delegate ASIOError ASIOcontrolPanel(IntPtr _pUnknown);

			// Token: 0x02000143 RID: 323
			// (Invoke) Token: 0x060006E0 RID: 1760
			[UnmanagedFunctionPointer(4)]
			public delegate ASIOError ASIOfuture(IntPtr _pUnknown, int selector, IntPtr opt);

			// Token: 0x02000144 RID: 324
			// (Invoke) Token: 0x060006E4 RID: 1764
			[UnmanagedFunctionPointer(4)]
			public delegate ASIOError ASIOoutputReady(IntPtr _pUnknown);
		}
	}
}
