using System;

namespace Steamworks
{
	// Token: 0x020001B6 RID: 438
	[Serializable]
	public struct HSteamNetPollGroup : IEquatable<HSteamNetPollGroup>, IComparable<HSteamNetPollGroup>
	{
		// Token: 0x06000A91 RID: 2705 RVA: 0x0000FEC4 File Offset: 0x0000E0C4
		public HSteamNetPollGroup(uint value)
		{
			this.m_HSteamNetPollGroup = value;
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0000FECD File Offset: 0x0000E0CD
		public override string ToString()
		{
			return this.m_HSteamNetPollGroup.ToString();
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0000FEDA File Offset: 0x0000E0DA
		public override bool Equals(object other)
		{
			return other is HSteamNetPollGroup && this == (HSteamNetPollGroup)other;
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0000FEF7 File Offset: 0x0000E0F7
		public override int GetHashCode()
		{
			return this.m_HSteamNetPollGroup.GetHashCode();
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0000FF04 File Offset: 0x0000E104
		public static bool operator ==(HSteamNetPollGroup x, HSteamNetPollGroup y)
		{
			return x.m_HSteamNetPollGroup == y.m_HSteamNetPollGroup;
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0000FF14 File Offset: 0x0000E114
		public static bool operator !=(HSteamNetPollGroup x, HSteamNetPollGroup y)
		{
			return !(x == y);
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0000FF20 File Offset: 0x0000E120
		public static explicit operator HSteamNetPollGroup(uint value)
		{
			return new HSteamNetPollGroup(value);
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0000FF28 File Offset: 0x0000E128
		public static explicit operator uint(HSteamNetPollGroup that)
		{
			return that.m_HSteamNetPollGroup;
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0000FF30 File Offset: 0x0000E130
		public bool Equals(HSteamNetPollGroup other)
		{
			return this.m_HSteamNetPollGroup == other.m_HSteamNetPollGroup;
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0000FF40 File Offset: 0x0000E140
		public int CompareTo(HSteamNetPollGroup other)
		{
			return this.m_HSteamNetPollGroup.CompareTo(other.m_HSteamNetPollGroup);
		}

		// Token: 0x04000AE7 RID: 2791
		public static readonly HSteamNetPollGroup Invalid = new HSteamNetPollGroup(0U);

		// Token: 0x04000AE8 RID: 2792
		public uint m_HSteamNetPollGroup;
	}
}
