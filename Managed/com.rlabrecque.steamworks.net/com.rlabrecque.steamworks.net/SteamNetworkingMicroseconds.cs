using System;

namespace Steamworks
{
	// Token: 0x020001BC RID: 444
	[Serializable]
	public struct SteamNetworkingMicroseconds : IEquatable<SteamNetworkingMicroseconds>, IComparable<SteamNetworkingMicroseconds>
	{
		// Token: 0x06000AC7 RID: 2759 RVA: 0x00010223 File Offset: 0x0000E423
		public SteamNetworkingMicroseconds(long value)
		{
			this.m_SteamNetworkingMicroseconds = value;
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0001022C File Offset: 0x0000E42C
		public override string ToString()
		{
			return this.m_SteamNetworkingMicroseconds.ToString();
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00010239 File Offset: 0x0000E439
		public override bool Equals(object other)
		{
			return other is SteamNetworkingMicroseconds && this == (SteamNetworkingMicroseconds)other;
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00010256 File Offset: 0x0000E456
		public override int GetHashCode()
		{
			return this.m_SteamNetworkingMicroseconds.GetHashCode();
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00010263 File Offset: 0x0000E463
		public static bool operator ==(SteamNetworkingMicroseconds x, SteamNetworkingMicroseconds y)
		{
			return x.m_SteamNetworkingMicroseconds == y.m_SteamNetworkingMicroseconds;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00010273 File Offset: 0x0000E473
		public static bool operator !=(SteamNetworkingMicroseconds x, SteamNetworkingMicroseconds y)
		{
			return !(x == y);
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0001027F File Offset: 0x0000E47F
		public static explicit operator SteamNetworkingMicroseconds(long value)
		{
			return new SteamNetworkingMicroseconds(value);
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00010287 File Offset: 0x0000E487
		public static explicit operator long(SteamNetworkingMicroseconds that)
		{
			return that.m_SteamNetworkingMicroseconds;
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x0001028F File Offset: 0x0000E48F
		public bool Equals(SteamNetworkingMicroseconds other)
		{
			return this.m_SteamNetworkingMicroseconds == other.m_SteamNetworkingMicroseconds;
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0001029F File Offset: 0x0000E49F
		public int CompareTo(SteamNetworkingMicroseconds other)
		{
			return this.m_SteamNetworkingMicroseconds.CompareTo(other.m_SteamNetworkingMicroseconds);
		}

		// Token: 0x04000B24 RID: 2852
		public long m_SteamNetworkingMicroseconds;
	}
}
