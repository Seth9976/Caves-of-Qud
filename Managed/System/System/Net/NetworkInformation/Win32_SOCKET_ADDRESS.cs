using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000752 RID: 1874
	internal struct Win32_SOCKET_ADDRESS
	{
		// Token: 0x06003B30 RID: 15152 RVA: 0x000CBE94 File Offset: 0x000CA094
		public IPAddress GetIPAddress()
		{
			Win32_SOCKADDR win32_SOCKADDR = (Win32_SOCKADDR)Marshal.PtrToStructure(this.Sockaddr, typeof(Win32_SOCKADDR));
			byte[] array;
			if (win32_SOCKADDR.AddressFamily == 23)
			{
				array = new byte[16];
				Array.Copy(win32_SOCKADDR.AddressData, 6, array, 0, 16);
			}
			else
			{
				array = new byte[4];
				Array.Copy(win32_SOCKADDR.AddressData, 2, array, 0, 4);
			}
			return new IPAddress(array);
		}

		// Token: 0x0400236A RID: 9066
		public IntPtr Sockaddr;

		// Token: 0x0400236B RID: 9067
		public int SockaddrLength;

		// Token: 0x0400236C RID: 9068
		private const int AF_INET6 = 23;
	}
}
