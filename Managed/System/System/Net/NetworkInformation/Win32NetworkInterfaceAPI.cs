using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200073C RID: 1852
	internal class Win32NetworkInterfaceAPI : NetworkInterfaceFactory
	{
		// Token: 0x06003B0C RID: 15116
		[DllImport("iphlpapi.dll", SetLastError = true)]
		private static extern int GetAdaptersAddresses(uint family, uint flags, IntPtr reserved, IntPtr info, ref int size);

		// Token: 0x06003B0D RID: 15117
		[DllImport("iphlpapi.dll")]
		private static extern uint GetBestInterfaceEx(byte[] ipAddress, out int index);

		// Token: 0x06003B0E RID: 15118 RVA: 0x000CB990 File Offset: 0x000C9B90
		private static Win32_IP_ADAPTER_ADDRESSES[] GetAdaptersAddresses()
		{
			IntPtr intPtr = IntPtr.Zero;
			int num = 0;
			uint num2 = 192U;
			Win32NetworkInterfaceAPI.GetAdaptersAddresses(0U, num2, IntPtr.Zero, intPtr, ref num);
			if (Marshal.SizeOf(typeof(Win32_IP_ADAPTER_ADDRESSES)) > num)
			{
				throw new NetworkInformationException();
			}
			intPtr = Marshal.AllocHGlobal(num);
			int adaptersAddresses = Win32NetworkInterfaceAPI.GetAdaptersAddresses(0U, num2, IntPtr.Zero, intPtr, ref num);
			if (adaptersAddresses != 0)
			{
				throw new NetworkInformationException(adaptersAddresses);
			}
			List<Win32_IP_ADAPTER_ADDRESSES> list = new List<Win32_IP_ADAPTER_ADDRESSES>();
			IntPtr intPtr2 = intPtr;
			while (intPtr2 != IntPtr.Zero)
			{
				Win32_IP_ADAPTER_ADDRESSES win32_IP_ADAPTER_ADDRESSES = Marshal.PtrToStructure<Win32_IP_ADAPTER_ADDRESSES>(intPtr2);
				list.Add(win32_IP_ADAPTER_ADDRESSES);
				intPtr2 = win32_IP_ADAPTER_ADDRESSES.Next;
			}
			return list.ToArray();
		}

		// Token: 0x06003B0F RID: 15119 RVA: 0x000CBA30 File Offset: 0x000C9C30
		public override NetworkInterface[] GetAllNetworkInterfaces()
		{
			Win32_IP_ADAPTER_ADDRESSES[] adaptersAddresses = Win32NetworkInterfaceAPI.GetAdaptersAddresses();
			NetworkInterface[] array = new NetworkInterface[adaptersAddresses.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new Win32NetworkInterface2(adaptersAddresses[i]);
			}
			return array;
		}

		// Token: 0x06003B10 RID: 15120 RVA: 0x000CBA6C File Offset: 0x000C9C6C
		private static int GetBestInterfaceForAddress(IPAddress addr)
		{
			int num;
			int bestInterfaceEx = (int)Win32NetworkInterfaceAPI.GetBestInterfaceEx(new SocketAddress(addr).m_Buffer, out num);
			if (bestInterfaceEx != 0)
			{
				throw new NetworkInformationException(bestInterfaceEx);
			}
			return num;
		}

		// Token: 0x06003B11 RID: 15121 RVA: 0x000CBA97 File Offset: 0x000C9C97
		public override int GetLoopbackInterfaceIndex()
		{
			return Win32NetworkInterfaceAPI.GetBestInterfaceForAddress(IPAddress.Loopback);
		}

		// Token: 0x06003B12 RID: 15122 RVA: 0x0000829A File Offset: 0x0000649A
		public override IPAddress GetNetMask(IPAddress address)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040022C8 RID: 8904
		private const string IPHLPAPI = "iphlpapi.dll";
	}
}
