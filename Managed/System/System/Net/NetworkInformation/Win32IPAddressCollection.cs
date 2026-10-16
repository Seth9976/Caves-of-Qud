using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000726 RID: 1830
	internal class Win32IPAddressCollection : IPAddressCollection
	{
		// Token: 0x06003A5B RID: 14939 RVA: 0x000CA6FC File Offset: 0x000C88FC
		private Win32IPAddressCollection()
		{
		}

		// Token: 0x06003A5C RID: 14940 RVA: 0x000CA704 File Offset: 0x000C8904
		public Win32IPAddressCollection(params IntPtr[] heads)
		{
			foreach (IntPtr intPtr in heads)
			{
				this.AddSubsequentlyString(intPtr);
			}
		}

		// Token: 0x06003A5D RID: 14941 RVA: 0x000CA734 File Offset: 0x000C8934
		public Win32IPAddressCollection(params Win32_IP_ADDR_STRING[] al)
		{
			foreach (Win32_IP_ADDR_STRING win32_IP_ADDR_STRING in al)
			{
				if (!string.IsNullOrEmpty(win32_IP_ADDR_STRING.IpAddress))
				{
					base.InternalAdd(IPAddress.Parse(win32_IP_ADDR_STRING.IpAddress));
					this.AddSubsequentlyString(win32_IP_ADDR_STRING.Next);
				}
			}
		}

		// Token: 0x06003A5E RID: 14942 RVA: 0x000CA78C File Offset: 0x000C898C
		public static Win32IPAddressCollection FromAnycast(IntPtr ptr)
		{
			Win32IPAddressCollection win32IPAddressCollection = new Win32IPAddressCollection();
			IntPtr intPtr = ptr;
			while (intPtr != IntPtr.Zero)
			{
				Win32_IP_ADAPTER_ANYCAST_ADDRESS win32_IP_ADAPTER_ANYCAST_ADDRESS = (Win32_IP_ADAPTER_ANYCAST_ADDRESS)Marshal.PtrToStructure(intPtr, typeof(Win32_IP_ADAPTER_ANYCAST_ADDRESS));
				win32IPAddressCollection.InternalAdd(win32_IP_ADAPTER_ANYCAST_ADDRESS.Address.GetIPAddress());
				intPtr = win32_IP_ADAPTER_ANYCAST_ADDRESS.Next;
			}
			return win32IPAddressCollection;
		}

		// Token: 0x06003A5F RID: 14943 RVA: 0x000CA7E0 File Offset: 0x000C89E0
		public static Win32IPAddressCollection FromDnsServer(IntPtr ptr)
		{
			Win32IPAddressCollection win32IPAddressCollection = new Win32IPAddressCollection();
			IntPtr intPtr = ptr;
			while (intPtr != IntPtr.Zero)
			{
				Win32_IP_ADAPTER_DNS_SERVER_ADDRESS win32_IP_ADAPTER_DNS_SERVER_ADDRESS = (Win32_IP_ADAPTER_DNS_SERVER_ADDRESS)Marshal.PtrToStructure(intPtr, typeof(Win32_IP_ADAPTER_DNS_SERVER_ADDRESS));
				win32IPAddressCollection.InternalAdd(win32_IP_ADAPTER_DNS_SERVER_ADDRESS.Address.GetIPAddress());
				intPtr = win32_IP_ADAPTER_DNS_SERVER_ADDRESS.Next;
			}
			return win32IPAddressCollection;
		}

		// Token: 0x06003A60 RID: 14944 RVA: 0x000CA834 File Offset: 0x000C8A34
		public static Win32IPAddressCollection FromSocketAddress(Win32_SOCKET_ADDRESS addr)
		{
			Win32IPAddressCollection win32IPAddressCollection = new Win32IPAddressCollection();
			if (addr.Sockaddr != IntPtr.Zero)
			{
				win32IPAddressCollection.InternalAdd(addr.GetIPAddress());
			}
			return win32IPAddressCollection;
		}

		// Token: 0x06003A61 RID: 14945 RVA: 0x000CA868 File Offset: 0x000C8A68
		public static Win32IPAddressCollection FromWinsServer(IntPtr ptr)
		{
			Win32IPAddressCollection win32IPAddressCollection = new Win32IPAddressCollection();
			IntPtr intPtr = ptr;
			while (intPtr != IntPtr.Zero)
			{
				Win32_IP_ADAPTER_WINS_SERVER_ADDRESS win32_IP_ADAPTER_WINS_SERVER_ADDRESS = (Win32_IP_ADAPTER_WINS_SERVER_ADDRESS)Marshal.PtrToStructure(intPtr, typeof(Win32_IP_ADAPTER_WINS_SERVER_ADDRESS));
				win32IPAddressCollection.InternalAdd(win32_IP_ADAPTER_WINS_SERVER_ADDRESS.Address.GetIPAddress());
				intPtr = win32_IP_ADAPTER_WINS_SERVER_ADDRESS.Next;
			}
			return win32IPAddressCollection;
		}

		// Token: 0x06003A62 RID: 14946 RVA: 0x000CA8BC File Offset: 0x000C8ABC
		private void AddSubsequentlyString(IntPtr head)
		{
			IntPtr intPtr = head;
			while (intPtr != IntPtr.Zero)
			{
				Win32_IP_ADDR_STRING win32_IP_ADDR_STRING = (Win32_IP_ADDR_STRING)Marshal.PtrToStructure(intPtr, typeof(Win32_IP_ADDR_STRING));
				base.InternalAdd(IPAddress.Parse(win32_IP_ADDR_STRING.IpAddress));
				intPtr = win32_IP_ADDR_STRING.Next;
			}
		}

		// Token: 0x04002268 RID: 8808
		public static readonly Win32IPAddressCollection Empty = new Win32IPAddressCollection(new IntPtr[] { IntPtr.Zero });
	}
}
