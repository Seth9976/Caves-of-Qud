using System;

namespace NAudio.Wave
{
	// Token: 0x020001C0 RID: 448
	public class WaveCallbackInfo
	{
		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x0001B31D File Offset: 0x0001951D
		// (set) Token: 0x0600097C RID: 2428 RVA: 0x0001B325 File Offset: 0x00019525
		public WaveCallbackStrategy Strategy { get; private set; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x0001B32E File Offset: 0x0001952E
		// (set) Token: 0x0600097E RID: 2430 RVA: 0x0001B336 File Offset: 0x00019536
		public IntPtr Handle { get; private set; }

		// Token: 0x0600097F RID: 2431 RVA: 0x0001B33F File Offset: 0x0001953F
		public static WaveCallbackInfo FunctionCallback()
		{
			return new WaveCallbackInfo(WaveCallbackStrategy.FunctionCallback, IntPtr.Zero);
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0001B34C File Offset: 0x0001954C
		public static WaveCallbackInfo NewWindow()
		{
			return new WaveCallbackInfo(WaveCallbackStrategy.NewWindow, IntPtr.Zero);
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0001B359 File Offset: 0x00019559
		public static WaveCallbackInfo ExistingWindow(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentException("Handle cannot be zero");
			}
			return new WaveCallbackInfo(WaveCallbackStrategy.ExistingWindow, handle);
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0001B37A File Offset: 0x0001957A
		private WaveCallbackInfo(WaveCallbackStrategy strategy, IntPtr handle)
		{
			this.Strategy = strategy;
			this.Handle = handle;
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0001B390 File Offset: 0x00019590
		internal void Connect(WaveInterop.WaveCallback callback)
		{
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0001B394 File Offset: 0x00019594
		internal MmResult WaveOutOpen(out IntPtr waveOutHandle, int deviceNumber, WaveFormat waveFormat, WaveInterop.WaveCallback callback)
		{
			MmResult mmResult;
			if (this.Strategy == WaveCallbackStrategy.FunctionCallback)
			{
				mmResult = WaveInterop.waveOutOpen(out waveOutHandle, (IntPtr)deviceNumber, waveFormat, callback, IntPtr.Zero, WaveInterop.WaveInOutOpenFlags.CallbackFunction);
			}
			else
			{
				mmResult = WaveInterop.waveOutOpenWindow(out waveOutHandle, (IntPtr)deviceNumber, waveFormat, this.Handle, IntPtr.Zero, WaveInterop.WaveInOutOpenFlags.CallbackWindow);
			}
			return mmResult;
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0001B3E4 File Offset: 0x000195E4
		internal MmResult WaveInOpen(out IntPtr waveInHandle, int deviceNumber, WaveFormat waveFormat, WaveInterop.WaveCallback callback)
		{
			MmResult mmResult;
			if (this.Strategy == WaveCallbackStrategy.FunctionCallback)
			{
				mmResult = WaveInterop.waveInOpen(out waveInHandle, (IntPtr)deviceNumber, waveFormat, callback, IntPtr.Zero, WaveInterop.WaveInOutOpenFlags.CallbackFunction);
			}
			else
			{
				mmResult = WaveInterop.waveInOpenWindow(out waveInHandle, (IntPtr)deviceNumber, waveFormat, this.Handle, IntPtr.Zero, WaveInterop.WaveInOutOpenFlags.CallbackWindow);
			}
			return mmResult;
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0001B434 File Offset: 0x00019634
		internal void Disconnect()
		{
		}
	}
}
