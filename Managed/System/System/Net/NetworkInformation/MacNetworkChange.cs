using System;
using System.Runtime.InteropServices;
using Mono.Util;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000714 RID: 1812
	internal sealed class MacNetworkChange : INetworkChange, IDisposable
	{
		// Token: 0x060039E1 RID: 14817
		[DllImport("/usr/lib/libSystem.dylib")]
		private static extern IntPtr dlopen(string path, int mode);

		// Token: 0x060039E2 RID: 14818
		[DllImport("/usr/lib/libSystem.dylib")]
		private static extern IntPtr dlsym(IntPtr handle, string symbol);

		// Token: 0x060039E3 RID: 14819
		[DllImport("/usr/lib/libSystem.dylib")]
		private static extern int dlclose(IntPtr handle);

		// Token: 0x060039E4 RID: 14820
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern void CFRelease(IntPtr handle);

		// Token: 0x060039E5 RID: 14821
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFRunLoopGetMain();

		// Token: 0x060039E6 RID: 14822
		[DllImport("/System/Library/Frameworks/SystemConfiguration.framework/SystemConfiguration")]
		private static extern IntPtr SCNetworkReachabilityCreateWithAddress(IntPtr allocator, ref MacNetworkChange.sockaddr_in sockaddr);

		// Token: 0x060039E7 RID: 14823
		[DllImport("/System/Library/Frameworks/SystemConfiguration.framework/SystemConfiguration")]
		private static extern bool SCNetworkReachabilityGetFlags(IntPtr reachability, out MacNetworkChange.NetworkReachabilityFlags flags);

		// Token: 0x060039E8 RID: 14824
		[DllImport("/System/Library/Frameworks/SystemConfiguration.framework/SystemConfiguration")]
		private static extern bool SCNetworkReachabilitySetCallback(IntPtr reachability, MacNetworkChange.SCNetworkReachabilityCallback callback, ref MacNetworkChange.SCNetworkReachabilityContext context);

		// Token: 0x060039E9 RID: 14825
		[DllImport("/System/Library/Frameworks/SystemConfiguration.framework/SystemConfiguration")]
		private static extern bool SCNetworkReachabilityScheduleWithRunLoop(IntPtr reachability, IntPtr runLoop, IntPtr runLoopMode);

		// Token: 0x060039EA RID: 14826
		[DllImport("/System/Library/Frameworks/SystemConfiguration.framework/SystemConfiguration")]
		private static extern bool SCNetworkReachabilityUnscheduleFromRunLoop(IntPtr reachability, IntPtr runLoop, IntPtr runLoopMode);

		// Token: 0x1400006F RID: 111
		// (add) Token: 0x060039EB RID: 14827 RVA: 0x000C9130 File Offset: 0x000C7330
		// (remove) Token: 0x060039EC RID: 14828 RVA: 0x000C9168 File Offset: 0x000C7368
		private event NetworkAddressChangedEventHandler networkAddressChanged;

		// Token: 0x14000070 RID: 112
		// (add) Token: 0x060039ED RID: 14829 RVA: 0x000C91A0 File Offset: 0x000C73A0
		// (remove) Token: 0x060039EE RID: 14830 RVA: 0x000C91D8 File Offset: 0x000C73D8
		private event NetworkAvailabilityChangedEventHandler networkAvailabilityChanged;

		// Token: 0x14000071 RID: 113
		// (add) Token: 0x060039EF RID: 14831 RVA: 0x000C920D File Offset: 0x000C740D
		// (remove) Token: 0x060039F0 RID: 14832 RVA: 0x000C9222 File Offset: 0x000C7422
		public event NetworkAddressChangedEventHandler NetworkAddressChanged
		{
			add
			{
				value(null, EventArgs.Empty);
				this.networkAddressChanged += value;
			}
			remove
			{
				this.networkAddressChanged -= value;
			}
		}

		// Token: 0x14000072 RID: 114
		// (add) Token: 0x060039F1 RID: 14833 RVA: 0x000C922B File Offset: 0x000C742B
		// (remove) Token: 0x060039F2 RID: 14834 RVA: 0x000C9246 File Offset: 0x000C7446
		public event NetworkAvailabilityChangedEventHandler NetworkAvailabilityChanged
		{
			add
			{
				value(null, new NetworkAvailabilityEventArgs(this.IsAvailable));
				this.networkAvailabilityChanged += value;
			}
			remove
			{
				this.networkAvailabilityChanged -= value;
			}
		}

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x060039F3 RID: 14835 RVA: 0x000C924F File Offset: 0x000C744F
		private bool IsAvailable
		{
			get
			{
				return (this.flags & MacNetworkChange.NetworkReachabilityFlags.Reachable) != MacNetworkChange.NetworkReachabilityFlags.None && (this.flags & MacNetworkChange.NetworkReachabilityFlags.ConnectionRequired) == MacNetworkChange.NetworkReachabilityFlags.None;
			}
		}

		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x060039F4 RID: 14836 RVA: 0x000C9268 File Offset: 0x000C7468
		public bool HasRegisteredEvents
		{
			get
			{
				return this.networkAddressChanged != null || this.networkAvailabilityChanged != null;
			}
		}

		// Token: 0x060039F5 RID: 14837 RVA: 0x000C9280 File Offset: 0x000C7480
		public MacNetworkChange()
		{
			MacNetworkChange.sockaddr_in sockaddr_in = MacNetworkChange.sockaddr_in.Create();
			this.handle = MacNetworkChange.SCNetworkReachabilityCreateWithAddress(IntPtr.Zero, ref sockaddr_in);
			if (this.handle == IntPtr.Zero)
			{
				throw new Exception("SCNetworkReachabilityCreateWithAddress returned NULL");
			}
			this.callback = new MacNetworkChange.SCNetworkReachabilityCallback(MacNetworkChange.HandleCallback);
			MacNetworkChange.SCNetworkReachabilityContext scnetworkReachabilityContext = new MacNetworkChange.SCNetworkReachabilityContext
			{
				info = GCHandle.ToIntPtr(GCHandle.Alloc(this))
			};
			MacNetworkChange.SCNetworkReachabilitySetCallback(this.handle, this.callback, ref scnetworkReachabilityContext);
			this.scheduledWithRunLoop = this.LoadRunLoopMode() && MacNetworkChange.SCNetworkReachabilityScheduleWithRunLoop(this.handle, MacNetworkChange.CFRunLoopGetMain(), this.runLoopMode);
			MacNetworkChange.SCNetworkReachabilityGetFlags(this.handle, out this.flags);
		}

		// Token: 0x060039F6 RID: 14838 RVA: 0x000C9344 File Offset: 0x000C7544
		private bool LoadRunLoopMode()
		{
			IntPtr intPtr = MacNetworkChange.dlopen("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", 0);
			if (intPtr == IntPtr.Zero)
			{
				return false;
			}
			try
			{
				this.runLoopMode = MacNetworkChange.dlsym(intPtr, "kCFRunLoopDefaultMode");
				if (this.runLoopMode != IntPtr.Zero)
				{
					this.runLoopMode = Marshal.ReadIntPtr(this.runLoopMode);
					return this.runLoopMode != IntPtr.Zero;
				}
			}
			finally
			{
				MacNetworkChange.dlclose(intPtr);
			}
			return false;
		}

		// Token: 0x060039F7 RID: 14839 RVA: 0x000C93D4 File Offset: 0x000C75D4
		public void Dispose()
		{
			lock (this)
			{
				if (!(this.handle == IntPtr.Zero))
				{
					if (this.scheduledWithRunLoop)
					{
						MacNetworkChange.SCNetworkReachabilityUnscheduleFromRunLoop(this.handle, MacNetworkChange.CFRunLoopGetMain(), this.runLoopMode);
					}
					MacNetworkChange.CFRelease(this.handle);
					this.handle = IntPtr.Zero;
					this.callback = null;
					this.flags = MacNetworkChange.NetworkReachabilityFlags.None;
					this.scheduledWithRunLoop = false;
				}
			}
		}

		// Token: 0x060039F8 RID: 14840 RVA: 0x000C9468 File Offset: 0x000C7668
		[MonoPInvokeCallback(typeof(MacNetworkChange.SCNetworkReachabilityCallback))]
		private static void HandleCallback(IntPtr reachability, MacNetworkChange.NetworkReachabilityFlags flags, IntPtr info)
		{
			if (info == IntPtr.Zero)
			{
				return;
			}
			MacNetworkChange macNetworkChange = GCHandle.FromIntPtr(info).Target as MacNetworkChange;
			if (macNetworkChange == null || macNetworkChange.flags == flags)
			{
				return;
			}
			macNetworkChange.flags = flags;
			NetworkAddressChangedEventHandler networkAddressChangedEventHandler = macNetworkChange.networkAddressChanged;
			if (networkAddressChangedEventHandler != null)
			{
				networkAddressChangedEventHandler(null, EventArgs.Empty);
			}
			NetworkAvailabilityChangedEventHandler networkAvailabilityChangedEventHandler = macNetworkChange.networkAvailabilityChanged;
			if (networkAvailabilityChangedEventHandler != null)
			{
				networkAvailabilityChangedEventHandler(null, new NetworkAvailabilityEventArgs(macNetworkChange.IsAvailable));
			}
		}

		// Token: 0x04002223 RID: 8739
		private const string DL_LIB = "/usr/lib/libSystem.dylib";

		// Token: 0x04002224 RID: 8740
		private const string CORE_SERVICES_LIB = "/System/Library/Frameworks/SystemConfiguration.framework/SystemConfiguration";

		// Token: 0x04002225 RID: 8741
		private const string CORE_FOUNDATION_LIB = "/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation";

		// Token: 0x04002226 RID: 8742
		private IntPtr handle;

		// Token: 0x04002227 RID: 8743
		private IntPtr runLoopMode;

		// Token: 0x04002228 RID: 8744
		private MacNetworkChange.SCNetworkReachabilityCallback callback;

		// Token: 0x04002229 RID: 8745
		private bool scheduledWithRunLoop;

		// Token: 0x0400222A RID: 8746
		private MacNetworkChange.NetworkReachabilityFlags flags;

		// Token: 0x02000715 RID: 1813
		// (Invoke) Token: 0x060039FA RID: 14842
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate void SCNetworkReachabilityCallback(IntPtr target, MacNetworkChange.NetworkReachabilityFlags flags, IntPtr info);

		// Token: 0x02000716 RID: 1814
		[StructLayout(LayoutKind.Explicit, Size = 28)]
		private struct sockaddr_in
		{
			// Token: 0x060039FD RID: 14845 RVA: 0x000C94E0 File Offset: 0x000C76E0
			public static MacNetworkChange.sockaddr_in Create()
			{
				return new MacNetworkChange.sockaddr_in
				{
					sin_len = 28,
					sin_family = 2
				};
			}

			// Token: 0x0400222D RID: 8749
			[FieldOffset(0)]
			public byte sin_len;

			// Token: 0x0400222E RID: 8750
			[FieldOffset(1)]
			public byte sin_family;
		}

		// Token: 0x02000717 RID: 1815
		private struct SCNetworkReachabilityContext
		{
			// Token: 0x0400222F RID: 8751
			public IntPtr version;

			// Token: 0x04002230 RID: 8752
			public IntPtr info;

			// Token: 0x04002231 RID: 8753
			public IntPtr retain;

			// Token: 0x04002232 RID: 8754
			public IntPtr release;

			// Token: 0x04002233 RID: 8755
			public IntPtr copyDescription;
		}

		// Token: 0x02000718 RID: 1816
		[Flags]
		private enum NetworkReachabilityFlags
		{
			// Token: 0x04002235 RID: 8757
			None = 0,
			// Token: 0x04002236 RID: 8758
			TransientConnection = 1,
			// Token: 0x04002237 RID: 8759
			Reachable = 2,
			// Token: 0x04002238 RID: 8760
			ConnectionRequired = 4,
			// Token: 0x04002239 RID: 8761
			ConnectionOnTraffic = 8,
			// Token: 0x0400223A RID: 8762
			InterventionRequired = 16,
			// Token: 0x0400223B RID: 8763
			ConnectionOnDemand = 32,
			// Token: 0x0400223C RID: 8764
			IsLocalAddress = 65536,
			// Token: 0x0400223D RID: 8765
			IsDirect = 131072,
			// Token: 0x0400223E RID: 8766
			IsWWAN = 262144,
			// Token: 0x0400223F RID: 8767
			ConnectionAutomatic = 8
		}
	}
}
