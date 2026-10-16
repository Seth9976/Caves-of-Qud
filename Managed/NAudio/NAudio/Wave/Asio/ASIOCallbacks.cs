using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Asio
{
	// Token: 0x02000154 RID: 340
	[StructLayout(0, Pack = 4)]
	internal struct ASIOCallbacks
	{
		// Token: 0x0400078B RID: 1931
		public ASIOCallbacks.ASIOBufferSwitchCallBack pbufferSwitch;

		// Token: 0x0400078C RID: 1932
		public ASIOCallbacks.ASIOSampleRateDidChangeCallBack psampleRateDidChange;

		// Token: 0x0400078D RID: 1933
		public ASIOCallbacks.ASIOAsioMessageCallBack pasioMessage;

		// Token: 0x0400078E RID: 1934
		public ASIOCallbacks.ASIOBufferSwitchTimeInfoCallBack pbufferSwitchTimeInfo;

		// Token: 0x02000155 RID: 341
		// (Invoke) Token: 0x06000718 RID: 1816
		[UnmanagedFunctionPointer(2)]
		internal delegate void ASIOBufferSwitchCallBack(int doubleBufferIndex, bool directProcess);

		// Token: 0x02000156 RID: 342
		// (Invoke) Token: 0x0600071C RID: 1820
		[UnmanagedFunctionPointer(2)]
		internal delegate void ASIOSampleRateDidChangeCallBack(double sRate);

		// Token: 0x02000157 RID: 343
		// (Invoke) Token: 0x06000720 RID: 1824
		[UnmanagedFunctionPointer(2)]
		internal delegate int ASIOAsioMessageCallBack(ASIOMessageSelector selector, int value, IntPtr message, IntPtr opt);

		// Token: 0x02000158 RID: 344
		// (Invoke) Token: 0x06000724 RID: 1828
		[UnmanagedFunctionPointer(2)]
		internal delegate IntPtr ASIOBufferSwitchTimeInfoCallBack(IntPtr asioTimeParam, int doubleBufferIndex, bool directProcess);
	}
}
