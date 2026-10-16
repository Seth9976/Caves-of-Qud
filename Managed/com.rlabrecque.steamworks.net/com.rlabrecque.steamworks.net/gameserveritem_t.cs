using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks
{
	// Token: 0x02000196 RID: 406
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 4, Size = 372)]
	public class gameserveritem_t
	{
		// Token: 0x06000955 RID: 2389 RVA: 0x0000E9A5 File Offset: 0x0000CBA5
		public string GetGameDir()
		{
			return Encoding.UTF8.GetString(this.m_szGameDir, 0, Array.IndexOf<byte>(this.m_szGameDir, 0));
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0000E9C4 File Offset: 0x0000CBC4
		public void SetGameDir(string dir)
		{
			this.m_szGameDir = Encoding.UTF8.GetBytes(dir + "\0");
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0000E9E1 File Offset: 0x0000CBE1
		public string GetMap()
		{
			return Encoding.UTF8.GetString(this.m_szMap, 0, Array.IndexOf<byte>(this.m_szMap, 0));
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x0000EA00 File Offset: 0x0000CC00
		public void SetMap(string map)
		{
			this.m_szMap = Encoding.UTF8.GetBytes(map + "\0");
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x0000EA1D File Offset: 0x0000CC1D
		public string GetGameDescription()
		{
			return Encoding.UTF8.GetString(this.m_szGameDescription, 0, Array.IndexOf<byte>(this.m_szGameDescription, 0));
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0000EA3C File Offset: 0x0000CC3C
		public void SetGameDescription(string desc)
		{
			this.m_szGameDescription = Encoding.UTF8.GetBytes(desc + "\0");
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0000EA59 File Offset: 0x0000CC59
		public string GetServerName()
		{
			if (this.m_szServerName[0] == 0)
			{
				return this.m_NetAdr.GetConnectionAddressString();
			}
			return Encoding.UTF8.GetString(this.m_szServerName, 0, Array.IndexOf<byte>(this.m_szServerName, 0));
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0000EA8E File Offset: 0x0000CC8E
		public void SetServerName(string name)
		{
			this.m_szServerName = Encoding.UTF8.GetBytes(name + "\0");
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0000EAAB File Offset: 0x0000CCAB
		public string GetGameTags()
		{
			return Encoding.UTF8.GetString(this.m_szGameTags, 0, Array.IndexOf<byte>(this.m_szGameTags, 0));
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0000EACA File Offset: 0x0000CCCA
		public void SetGameTags(string tags)
		{
			this.m_szGameTags = Encoding.UTF8.GetBytes(tags + "\0");
		}

		// Token: 0x04000A9D RID: 2717
		public servernetadr_t m_NetAdr;

		// Token: 0x04000A9E RID: 2718
		public int m_nPing;

		// Token: 0x04000A9F RID: 2719
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bHadSuccessfulResponse;

		// Token: 0x04000AA0 RID: 2720
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bDoNotRefresh;

		// Token: 0x04000AA1 RID: 2721
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		private byte[] m_szGameDir;

		// Token: 0x04000AA2 RID: 2722
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		private byte[] m_szMap;

		// Token: 0x04000AA3 RID: 2723
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
		private byte[] m_szGameDescription;

		// Token: 0x04000AA4 RID: 2724
		public uint m_nAppID;

		// Token: 0x04000AA5 RID: 2725
		public int m_nPlayers;

		// Token: 0x04000AA6 RID: 2726
		public int m_nMaxPlayers;

		// Token: 0x04000AA7 RID: 2727
		public int m_nBotPlayers;

		// Token: 0x04000AA8 RID: 2728
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bPassword;

		// Token: 0x04000AA9 RID: 2729
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bSecure;

		// Token: 0x04000AAA RID: 2730
		public uint m_ulTimeLastPlayed;

		// Token: 0x04000AAB RID: 2731
		public int m_nServerVersion;

		// Token: 0x04000AAC RID: 2732
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
		private byte[] m_szServerName;

		// Token: 0x04000AAD RID: 2733
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		private byte[] m_szGameTags;

		// Token: 0x04000AAE RID: 2734
		public CSteamID m_steamID;
	}
}
