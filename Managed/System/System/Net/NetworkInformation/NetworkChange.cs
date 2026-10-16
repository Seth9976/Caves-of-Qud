using System;
using System.ComponentModel;
using Unity;

namespace System.Net.NetworkInformation
{
	/// <summary>Allows applications to receive notification when the Internet Protocol (IP) address of a network interface, also called a network card or adapter, changes.</summary>
	// Token: 0x02000713 RID: 1811
	public sealed class NetworkChange
	{
		/// <summary>Occurs when the IP address of a network interface changes.</summary>
		// Token: 0x1400006D RID: 109
		// (add) Token: 0x060039D8 RID: 14808 RVA: 0x000C8F2C File Offset: 0x000C712C
		// (remove) Token: 0x060039D9 RID: 14809 RVA: 0x000C8F84 File Offset: 0x000C7184
		public static event NetworkAddressChangedEventHandler NetworkAddressChanged
		{
			add
			{
				Type typeFromHandle = typeof(INetworkChange);
				lock (typeFromHandle)
				{
					NetworkChange.MaybeCreate();
					if (NetworkChange.networkChange != null)
					{
						NetworkChange.networkChange.NetworkAddressChanged += value;
					}
				}
			}
			remove
			{
				Type typeFromHandle = typeof(INetworkChange);
				lock (typeFromHandle)
				{
					if (NetworkChange.networkChange != null)
					{
						NetworkChange.networkChange.NetworkAddressChanged -= value;
						NetworkChange.MaybeDispose();
					}
				}
			}
		}

		/// <summary>Occurs when the availability of the network changes.</summary>
		// Token: 0x1400006E RID: 110
		// (add) Token: 0x060039DA RID: 14810 RVA: 0x000C8FDC File Offset: 0x000C71DC
		// (remove) Token: 0x060039DB RID: 14811 RVA: 0x000C9034 File Offset: 0x000C7234
		public static event NetworkAvailabilityChangedEventHandler NetworkAvailabilityChanged
		{
			add
			{
				Type typeFromHandle = typeof(INetworkChange);
				lock (typeFromHandle)
				{
					NetworkChange.MaybeCreate();
					if (NetworkChange.networkChange != null)
					{
						NetworkChange.networkChange.NetworkAvailabilityChanged += value;
					}
				}
			}
			remove
			{
				Type typeFromHandle = typeof(INetworkChange);
				lock (typeFromHandle)
				{
					if (NetworkChange.networkChange != null)
					{
						NetworkChange.networkChange.NetworkAvailabilityChanged -= value;
						NetworkChange.MaybeDispose();
					}
				}
			}
		}

		// Token: 0x060039DC RID: 14812 RVA: 0x000C908C File Offset: 0x000C728C
		private static void MaybeCreate()
		{
			if (NetworkChange.networkChange != null)
			{
				return;
			}
			if (NetworkChange.IsWindows)
			{
				throw new PlatformNotSupportedException("NetworkInformation.NetworkChange is not supported on the current platform.");
			}
			try
			{
				NetworkChange.networkChange = new MacNetworkChange();
			}
			catch
			{
				NetworkChange.networkChange = new LinuxNetworkChange();
			}
		}

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x060039DD RID: 14813 RVA: 0x000C90E0 File Offset: 0x000C72E0
		private static bool IsWindows
		{
			get
			{
				PlatformID platform = Environment.OSVersion.Platform;
				return platform == PlatformID.Win32S || platform == PlatformID.Win32Windows || platform == PlatformID.Win32NT || platform == PlatformID.WinCE;
			}
		}

		// Token: 0x060039DE RID: 14814 RVA: 0x000C910A File Offset: 0x000C730A
		private static void MaybeDispose()
		{
			if (NetworkChange.networkChange != null && NetworkChange.networkChange.HasRegisteredEvents)
			{
				NetworkChange.networkChange.Dispose();
				NetworkChange.networkChange = null;
			}
		}

		/// <summary>Registers a network change instance to receive network change events.</summary>
		/// <param name="nc">The instance to register. </param>
		// Token: 0x060039E0 RID: 14816 RVA: 0x00013BAE File Offset: 0x00011DAE
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		public static void RegisterNetworkChange(NetworkChange nc)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04002222 RID: 8738
		private static INetworkChange networkChange;
	}
}
