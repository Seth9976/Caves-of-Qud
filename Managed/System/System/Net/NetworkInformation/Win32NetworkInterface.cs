using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200073F RID: 1855
	internal class Win32NetworkInterface
	{
		// Token: 0x06003B25 RID: 15141
		[DllImport("iphlpapi.dll", SetLastError = true)]
		private static extern int GetNetworkParams(IntPtr ptr, ref int size);

		// Token: 0x06003B26 RID: 15142
		[DllImport("kernel32.dll", SetLastError = true)]
		private unsafe static extern int MultiByteToWideChar(uint CodePage, uint dwFlags, byte* lpMultiByteStr, int cbMultiByte, char* lpWideCharStr, int cchWideChar);

		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x06003B27 RID: 15143 RVA: 0x000CBD00 File Offset: 0x000C9F00
		public unsafe static Win32_FIXED_INFO FixedInfo
		{
			get
			{
				if (!Win32NetworkInterface.initialized)
				{
					int num = 0;
					Win32NetworkInterface.GetNetworkParams(IntPtr.Zero, ref num);
					IntPtr intPtr = Marshal.AllocHGlobal(num);
					Win32NetworkInterface.GetNetworkParams(intPtr, ref num);
					Win32_FIXED_INFO_Marshal win32_FIXED_INFO_Marshal = Marshal.PtrToStructure<Win32_FIXED_INFO_Marshal>(intPtr);
					Win32NetworkInterface.fixedInfo = new Win32_FIXED_INFO
					{
						HostName = Win32NetworkInterface.<get_FixedInfo>g__GetStringFromMultiByte|5_0(&win32_FIXED_INFO_Marshal.HostName.FixedElementField),
						DomainName = Win32NetworkInterface.<get_FixedInfo>g__GetStringFromMultiByte|5_0(&win32_FIXED_INFO_Marshal.DomainName.FixedElementField),
						CurrentDnsServer = win32_FIXED_INFO_Marshal.CurrentDnsServer,
						DnsServerList = win32_FIXED_INFO_Marshal.DnsServerList,
						NodeType = win32_FIXED_INFO_Marshal.NodeType,
						ScopeId = Win32NetworkInterface.<get_FixedInfo>g__GetStringFromMultiByte|5_0(&win32_FIXED_INFO_Marshal.ScopeId.FixedElementField),
						EnableRouting = win32_FIXED_INFO_Marshal.EnableRouting,
						EnableProxy = win32_FIXED_INFO_Marshal.EnableProxy,
						EnableDns = win32_FIXED_INFO_Marshal.EnableDns
					};
					Win32NetworkInterface.initialized = true;
				}
				return Win32NetworkInterface.fixedInfo;
			}
		}

		// Token: 0x06003B29 RID: 15145 RVA: 0x000CBDF0 File Offset: 0x000C9FF0
		[CompilerGenerated]
		internal unsafe static string <get_FixedInfo>g__GetStringFromMultiByte|5_0(byte* bytes)
		{
			int num = Win32NetworkInterface.MultiByteToWideChar(0U, 0U, bytes, -1, null, 0);
			if (num == 0)
			{
				return string.Empty;
			}
			char[] array2;
			char[] array = (array2 = new char[num]);
			char* ptr;
			if (array == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			Win32NetworkInterface.MultiByteToWideChar(0U, 0U, bytes, -1, ptr, num);
			array2 = null;
			return new string(array);
		}

		// Token: 0x040022CE RID: 8910
		private static Win32_FIXED_INFO fixedInfo;

		// Token: 0x040022CF RID: 8911
		private static bool initialized;
	}
}
