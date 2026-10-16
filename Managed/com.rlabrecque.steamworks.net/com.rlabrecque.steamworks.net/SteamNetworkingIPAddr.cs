using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001B9 RID: 441
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct SteamNetworkingIPAddr : IEquatable<SteamNetworkingIPAddr>
	{
		// Token: 0x06000A9C RID: 2716 RVA: 0x0000FF60 File Offset: 0x0000E160
		public void Clear()
		{
			NativeMethods.SteamAPI_SteamNetworkingIPAddr_Clear(ref this);
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0000FF68 File Offset: 0x0000E168
		public bool IsIPv6AllZeros()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIPAddr_IsIPv6AllZeros(ref this);
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0000FF70 File Offset: 0x0000E170
		public void SetIPv6(byte[] ipv6, ushort nPort)
		{
			NativeMethods.SteamAPI_SteamNetworkingIPAddr_SetIPv6(ref this, ipv6, nPort);
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0000FF7A File Offset: 0x0000E17A
		public void SetIPv4(uint nIP, ushort nPort)
		{
			NativeMethods.SteamAPI_SteamNetworkingIPAddr_SetIPv4(ref this, nIP, nPort);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0000FF84 File Offset: 0x0000E184
		public bool IsIPv4()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIPAddr_IsIPv4(ref this);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0000FF8C File Offset: 0x0000E18C
		public uint GetIPv4()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIPAddr_GetIPv4(ref this);
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0000FF94 File Offset: 0x0000E194
		public void SetIPv6LocalHost(ushort nPort = 0)
		{
			NativeMethods.SteamAPI_SteamNetworkingIPAddr_SetIPv6LocalHost(ref this, nPort);
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0000FF9D File Offset: 0x0000E19D
		public bool IsLocalHost()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIPAddr_IsLocalHost(ref this);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0000FFA8 File Offset: 0x0000E1A8
		public void ToString(out string buf, bool bWithPort)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(48);
			NativeMethods.SteamAPI_SteamNetworkingIPAddr_ToString(ref this, intPtr, 48U, bWithPort);
			buf = InteropHelp.PtrToStringUTF8(intPtr);
			Marshal.FreeHGlobal(intPtr);
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0000FFD8 File Offset: 0x0000E1D8
		public bool ParseString(string pszStr)
		{
			bool flag;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszStr))
			{
				flag = NativeMethods.SteamAPI_SteamNetworkingIPAddr_ParseString(ref this, utf8StringHandle);
			}
			return flag;
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x00010014 File Offset: 0x0000E214
		public bool Equals(SteamNetworkingIPAddr x)
		{
			return NativeMethods.SteamAPI_SteamNetworkingIPAddr_IsEqualTo(ref this, ref x);
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0001001E File Offset: 0x0000E21E
		public ESteamNetworkingFakeIPType GetFakeIPType()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIPAddr_GetFakeIPType(ref this);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00010026 File Offset: 0x0000E226
		public bool IsFakeIP()
		{
			return this.GetFakeIPType() > ESteamNetworkingFakeIPType.k_ESteamNetworkingFakeIPType_NotFake;
		}

		// Token: 0x04000AED RID: 2797
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public byte[] m_ipv6;

		// Token: 0x04000AEE RID: 2798
		public ushort m_port;

		// Token: 0x04000AEF RID: 2799
		public const int k_cchMaxString = 48;
	}
}
