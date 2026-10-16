using System;

namespace Steamworks
{
	// Token: 0x020001CD RID: 461
	[Serializable]
	public struct SteamLeaderboardEntries_t : IEquatable<SteamLeaderboardEntries_t>, IComparable<SteamLeaderboardEntries_t>
	{
		// Token: 0x06000B78 RID: 2936 RVA: 0x00010D98 File Offset: 0x0000EF98
		public SteamLeaderboardEntries_t(ulong value)
		{
			this.m_SteamLeaderboardEntries = value;
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x00010DA1 File Offset: 0x0000EFA1
		public override string ToString()
		{
			return this.m_SteamLeaderboardEntries.ToString();
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00010DAE File Offset: 0x0000EFAE
		public override bool Equals(object other)
		{
			return other is SteamLeaderboardEntries_t && this == (SteamLeaderboardEntries_t)other;
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x00010DCB File Offset: 0x0000EFCB
		public override int GetHashCode()
		{
			return this.m_SteamLeaderboardEntries.GetHashCode();
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x00010DD8 File Offset: 0x0000EFD8
		public static bool operator ==(SteamLeaderboardEntries_t x, SteamLeaderboardEntries_t y)
		{
			return x.m_SteamLeaderboardEntries == y.m_SteamLeaderboardEntries;
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x00010DE8 File Offset: 0x0000EFE8
		public static bool operator !=(SteamLeaderboardEntries_t x, SteamLeaderboardEntries_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x00010DF4 File Offset: 0x0000EFF4
		public static explicit operator SteamLeaderboardEntries_t(ulong value)
		{
			return new SteamLeaderboardEntries_t(value);
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x00010DFC File Offset: 0x0000EFFC
		public static explicit operator ulong(SteamLeaderboardEntries_t that)
		{
			return that.m_SteamLeaderboardEntries;
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x00010E04 File Offset: 0x0000F004
		public bool Equals(SteamLeaderboardEntries_t other)
		{
			return this.m_SteamLeaderboardEntries == other.m_SteamLeaderboardEntries;
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00010E14 File Offset: 0x0000F014
		public int CompareTo(SteamLeaderboardEntries_t other)
		{
			return this.m_SteamLeaderboardEntries.CompareTo(other.m_SteamLeaderboardEntries);
		}

		// Token: 0x04000B43 RID: 2883
		public ulong m_SteamLeaderboardEntries;
	}
}
