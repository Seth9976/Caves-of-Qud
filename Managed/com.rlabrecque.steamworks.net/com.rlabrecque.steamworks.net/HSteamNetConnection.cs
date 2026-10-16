using System;

namespace Steamworks
{
	// Token: 0x020001B5 RID: 437
	[Serializable]
	public struct HSteamNetConnection : IEquatable<HSteamNetConnection>, IComparable<HSteamNetConnection>
	{
		// Token: 0x06000A86 RID: 2694 RVA: 0x0000FE28 File Offset: 0x0000E028
		public HSteamNetConnection(uint value)
		{
			this.m_HSteamNetConnection = value;
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0000FE31 File Offset: 0x0000E031
		public override string ToString()
		{
			return this.m_HSteamNetConnection.ToString();
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0000FE3E File Offset: 0x0000E03E
		public override bool Equals(object other)
		{
			return other is HSteamNetConnection && this == (HSteamNetConnection)other;
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0000FE5B File Offset: 0x0000E05B
		public override int GetHashCode()
		{
			return this.m_HSteamNetConnection.GetHashCode();
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0000FE68 File Offset: 0x0000E068
		public static bool operator ==(HSteamNetConnection x, HSteamNetConnection y)
		{
			return x.m_HSteamNetConnection == y.m_HSteamNetConnection;
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0000FE78 File Offset: 0x0000E078
		public static bool operator !=(HSteamNetConnection x, HSteamNetConnection y)
		{
			return !(x == y);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0000FE84 File Offset: 0x0000E084
		public static explicit operator HSteamNetConnection(uint value)
		{
			return new HSteamNetConnection(value);
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0000FE8C File Offset: 0x0000E08C
		public static explicit operator uint(HSteamNetConnection that)
		{
			return that.m_HSteamNetConnection;
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0000FE94 File Offset: 0x0000E094
		public bool Equals(HSteamNetConnection other)
		{
			return this.m_HSteamNetConnection == other.m_HSteamNetConnection;
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0000FEA4 File Offset: 0x0000E0A4
		public int CompareTo(HSteamNetConnection other)
		{
			return this.m_HSteamNetConnection.CompareTo(other.m_HSteamNetConnection);
		}

		// Token: 0x04000AE5 RID: 2789
		public static readonly HSteamNetConnection Invalid = new HSteamNetConnection(0U);

		// Token: 0x04000AE6 RID: 2790
		public uint m_HSteamNetConnection;
	}
}
