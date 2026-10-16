using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001BA RID: 442
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct SteamNetworkingIdentity : IEquatable<SteamNetworkingIdentity>
	{
		// Token: 0x06000AA9 RID: 2729 RVA: 0x00010031 File Offset: 0x0000E231
		public void Clear()
		{
			NativeMethods.SteamAPI_SteamNetworkingIdentity_Clear(ref this);
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00010039 File Offset: 0x0000E239
		public bool IsInvalid()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_IsInvalid(ref this);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00010041 File Offset: 0x0000E241
		public void SetSteamID(CSteamID steamID)
		{
			NativeMethods.SteamAPI_SteamNetworkingIdentity_SetSteamID(ref this, (ulong)steamID);
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0001004F File Offset: 0x0000E24F
		public CSteamID GetSteamID()
		{
			return (CSteamID)NativeMethods.SteamAPI_SteamNetworkingIdentity_GetSteamID(ref this);
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0001005C File Offset: 0x0000E25C
		public void SetSteamID64(ulong steamID)
		{
			NativeMethods.SteamAPI_SteamNetworkingIdentity_SetSteamID64(ref this, steamID);
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00010065 File Offset: 0x0000E265
		public ulong GetSteamID64()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_GetSteamID64(ref this);
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00010070 File Offset: 0x0000E270
		public bool SetXboxPairwiseID(string pszString)
		{
			bool flag;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszString))
			{
				flag = NativeMethods.SteamAPI_SteamNetworkingIdentity_SetXboxPairwiseID(ref this, utf8StringHandle);
			}
			return flag;
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x000100AC File Offset: 0x0000E2AC
		public string GetXboxPairwiseID()
		{
			return InteropHelp.PtrToStringUTF8(NativeMethods.SteamAPI_SteamNetworkingIdentity_GetXboxPairwiseID(ref this));
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x000100B9 File Offset: 0x0000E2B9
		public void SetPSNID(ulong id)
		{
			NativeMethods.SteamAPI_SteamNetworkingIdentity_SetPSNID(ref this, id);
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x000100C2 File Offset: 0x0000E2C2
		public ulong GetPSNID()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_GetPSNID(ref this);
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x000100CA File Offset: 0x0000E2CA
		public void SetStadiaID(ulong id)
		{
			NativeMethods.SteamAPI_SteamNetworkingIdentity_SetStadiaID(ref this, id);
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x000100D3 File Offset: 0x0000E2D3
		public ulong GetStadiaID()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_GetStadiaID(ref this);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x000100DB File Offset: 0x0000E2DB
		public void SetIPAddr(SteamNetworkingIPAddr addr)
		{
			NativeMethods.SteamAPI_SteamNetworkingIdentity_SetIPAddr(ref this, ref addr);
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x000100E6 File Offset: 0x0000E2E6
		public SteamNetworkingIPAddr GetIPAddr()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x000100ED File Offset: 0x0000E2ED
		public void SetIPv4Addr(uint nIPv4, ushort nPort)
		{
			NativeMethods.SteamAPI_SteamNetworkingIdentity_SetIPv4Addr(ref this, nIPv4, nPort);
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x000100F7 File Offset: 0x0000E2F7
		public uint GetIPv4()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_GetIPv4(ref this);
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x000100FF File Offset: 0x0000E2FF
		public ESteamNetworkingFakeIPType GetFakeIPType()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_GetFakeIPType(ref this);
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00010107 File Offset: 0x0000E307
		public bool IsFakeIP()
		{
			return this.GetFakeIPType() > ESteamNetworkingFakeIPType.k_ESteamNetworkingFakeIPType_NotFake;
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00010112 File Offset: 0x0000E312
		public void SetLocalHost()
		{
			NativeMethods.SteamAPI_SteamNetworkingIdentity_SetLocalHost(ref this);
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0001011A File Offset: 0x0000E31A
		public bool IsLocalHost()
		{
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_IsLocalHost(ref this);
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00010124 File Offset: 0x0000E324
		public bool SetGenericString(string pszString)
		{
			bool flag;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszString))
			{
				flag = NativeMethods.SteamAPI_SteamNetworkingIdentity_SetGenericString(ref this, utf8StringHandle);
			}
			return flag;
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00010160 File Offset: 0x0000E360
		public string GetGenericString()
		{
			return InteropHelp.PtrToStringUTF8(NativeMethods.SteamAPI_SteamNetworkingIdentity_GetGenericString(ref this));
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0001016D File Offset: 0x0000E36D
		public bool SetGenericBytes(byte[] data, uint cbLen)
		{
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_SetGenericBytes(ref this, data, cbLen);
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00010177 File Offset: 0x0000E377
		public byte[] GetGenericBytes(out int cbLen)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0001017E File Offset: 0x0000E37E
		public bool Equals(SteamNetworkingIdentity x)
		{
			return NativeMethods.SteamAPI_SteamNetworkingIdentity_IsEqualTo(ref this, ref x);
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00010188 File Offset: 0x0000E388
		public void ToString(out string buf)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(128);
			NativeMethods.SteamAPI_SteamNetworkingIdentity_ToString(ref this, intPtr, 128U);
			buf = InteropHelp.PtrToStringUTF8(intPtr);
			Marshal.FreeHGlobal(intPtr);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x000101BC File Offset: 0x0000E3BC
		public bool ParseString(string pszStr)
		{
			bool flag;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszStr))
			{
				flag = NativeMethods.SteamAPI_SteamNetworkingIdentity_ParseString(ref this, utf8StringHandle);
			}
			return flag;
		}

		// Token: 0x04000AF0 RID: 2800
		public ESteamNetworkingIdentityType m_eType;

		// Token: 0x04000AF1 RID: 2801
		private int m_cbSize;

		// Token: 0x04000AF2 RID: 2802
		private uint m_reserved0;

		// Token: 0x04000AF3 RID: 2803
		private uint m_reserved1;

		// Token: 0x04000AF4 RID: 2804
		private uint m_reserved2;

		// Token: 0x04000AF5 RID: 2805
		private uint m_reserved3;

		// Token: 0x04000AF6 RID: 2806
		private uint m_reserved4;

		// Token: 0x04000AF7 RID: 2807
		private uint m_reserved5;

		// Token: 0x04000AF8 RID: 2808
		private uint m_reserved6;

		// Token: 0x04000AF9 RID: 2809
		private uint m_reserved7;

		// Token: 0x04000AFA RID: 2810
		private uint m_reserved8;

		// Token: 0x04000AFB RID: 2811
		private uint m_reserved9;

		// Token: 0x04000AFC RID: 2812
		private uint m_reserved10;

		// Token: 0x04000AFD RID: 2813
		private uint m_reserved11;

		// Token: 0x04000AFE RID: 2814
		private uint m_reserved12;

		// Token: 0x04000AFF RID: 2815
		private uint m_reserved13;

		// Token: 0x04000B00 RID: 2816
		private uint m_reserved14;

		// Token: 0x04000B01 RID: 2817
		private uint m_reserved15;

		// Token: 0x04000B02 RID: 2818
		private uint m_reserved16;

		// Token: 0x04000B03 RID: 2819
		private uint m_reserved17;

		// Token: 0x04000B04 RID: 2820
		private uint m_reserved18;

		// Token: 0x04000B05 RID: 2821
		private uint m_reserved19;

		// Token: 0x04000B06 RID: 2822
		private uint m_reserved20;

		// Token: 0x04000B07 RID: 2823
		private uint m_reserved21;

		// Token: 0x04000B08 RID: 2824
		private uint m_reserved22;

		// Token: 0x04000B09 RID: 2825
		private uint m_reserved23;

		// Token: 0x04000B0A RID: 2826
		private uint m_reserved24;

		// Token: 0x04000B0B RID: 2827
		private uint m_reserved25;

		// Token: 0x04000B0C RID: 2828
		private uint m_reserved26;

		// Token: 0x04000B0D RID: 2829
		private uint m_reserved27;

		// Token: 0x04000B0E RID: 2830
		private uint m_reserved28;

		// Token: 0x04000B0F RID: 2831
		private uint m_reserved29;

		// Token: 0x04000B10 RID: 2832
		private uint m_reserved30;

		// Token: 0x04000B11 RID: 2833
		private uint m_reserved31;

		// Token: 0x04000B12 RID: 2834
		public const int k_cchMaxString = 128;

		// Token: 0x04000B13 RID: 2835
		public const int k_cchMaxGenericString = 32;

		// Token: 0x04000B14 RID: 2836
		public const int k_cchMaxXboxPairwiseID = 33;

		// Token: 0x04000B15 RID: 2837
		public const int k_cbMaxGenericBytes = 32;
	}
}
