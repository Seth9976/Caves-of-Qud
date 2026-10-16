using System;

namespace Steamworks
{
	// Token: 0x020001CE RID: 462
	[Serializable]
	public struct SteamLeaderboard_t : IEquatable<SteamLeaderboard_t>, IComparable<SteamLeaderboard_t>
	{
		// Token: 0x06000B82 RID: 2946 RVA: 0x00010E27 File Offset: 0x0000F027
		public SteamLeaderboard_t(ulong value)
		{
			this.m_SteamLeaderboard = value;
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x00010E30 File Offset: 0x0000F030
		public override string ToString()
		{
			return this.m_SteamLeaderboard.ToString();
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x00010E3D File Offset: 0x0000F03D
		public override bool Equals(object other)
		{
			return other is SteamLeaderboard_t && this == (SteamLeaderboard_t)other;
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x00010E5A File Offset: 0x0000F05A
		public override int GetHashCode()
		{
			return this.m_SteamLeaderboard.GetHashCode();
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x00010E67 File Offset: 0x0000F067
		public static bool operator ==(SteamLeaderboard_t x, SteamLeaderboard_t y)
		{
			return x.m_SteamLeaderboard == y.m_SteamLeaderboard;
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00010E77 File Offset: 0x0000F077
		public static bool operator !=(SteamLeaderboard_t x, SteamLeaderboard_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00010E83 File Offset: 0x0000F083
		public static explicit operator SteamLeaderboard_t(ulong value)
		{
			return new SteamLeaderboard_t(value);
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00010E8B File Offset: 0x0000F08B
		public static explicit operator ulong(SteamLeaderboard_t that)
		{
			return that.m_SteamLeaderboard;
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x00010E93 File Offset: 0x0000F093
		public bool Equals(SteamLeaderboard_t other)
		{
			return this.m_SteamLeaderboard == other.m_SteamLeaderboard;
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00010EA3 File Offset: 0x0000F0A3
		public int CompareTo(SteamLeaderboard_t other)
		{
			return this.m_SteamLeaderboard.CompareTo(other.m_SteamLeaderboard);
		}

		// Token: 0x04000B44 RID: 2884
		public ulong m_SteamLeaderboard;
	}
}
