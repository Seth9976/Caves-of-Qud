using System;

namespace Steamworks
{
	// Token: 0x020001D0 RID: 464
	[Serializable]
	public struct HSteamUser : IEquatable<HSteamUser>, IComparable<HSteamUser>
	{
		// Token: 0x06000B96 RID: 2966 RVA: 0x00010F45 File Offset: 0x0000F145
		public HSteamUser(int value)
		{
			this.m_HSteamUser = value;
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00010F4E File Offset: 0x0000F14E
		public override string ToString()
		{
			return this.m_HSteamUser.ToString();
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00010F5B File Offset: 0x0000F15B
		public override bool Equals(object other)
		{
			return other is HSteamUser && this == (HSteamUser)other;
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00010F78 File Offset: 0x0000F178
		public override int GetHashCode()
		{
			return this.m_HSteamUser.GetHashCode();
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00010F85 File Offset: 0x0000F185
		public static bool operator ==(HSteamUser x, HSteamUser y)
		{
			return x.m_HSteamUser == y.m_HSteamUser;
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00010F95 File Offset: 0x0000F195
		public static bool operator !=(HSteamUser x, HSteamUser y)
		{
			return !(x == y);
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00010FA1 File Offset: 0x0000F1A1
		public static explicit operator HSteamUser(int value)
		{
			return new HSteamUser(value);
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x00010FA9 File Offset: 0x0000F1A9
		public static explicit operator int(HSteamUser that)
		{
			return that.m_HSteamUser;
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x00010FB1 File Offset: 0x0000F1B1
		public bool Equals(HSteamUser other)
		{
			return this.m_HSteamUser == other.m_HSteamUser;
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x00010FC1 File Offset: 0x0000F1C1
		public int CompareTo(HSteamUser other)
		{
			return this.m_HSteamUser.CompareTo(other.m_HSteamUser);
		}

		// Token: 0x04000B46 RID: 2886
		public int m_HSteamUser;
	}
}
