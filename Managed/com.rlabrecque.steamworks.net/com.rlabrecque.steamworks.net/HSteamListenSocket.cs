using System;

namespace Steamworks
{
	// Token: 0x020001B4 RID: 436
	[Serializable]
	public struct HSteamListenSocket : IEquatable<HSteamListenSocket>, IComparable<HSteamListenSocket>
	{
		// Token: 0x06000A7B RID: 2683 RVA: 0x0000FD8C File Offset: 0x0000DF8C
		public HSteamListenSocket(uint value)
		{
			this.m_HSteamListenSocket = value;
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0000FD95 File Offset: 0x0000DF95
		public override string ToString()
		{
			return this.m_HSteamListenSocket.ToString();
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0000FDA2 File Offset: 0x0000DFA2
		public override bool Equals(object other)
		{
			return other is HSteamListenSocket && this == (HSteamListenSocket)other;
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0000FDBF File Offset: 0x0000DFBF
		public override int GetHashCode()
		{
			return this.m_HSteamListenSocket.GetHashCode();
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0000FDCC File Offset: 0x0000DFCC
		public static bool operator ==(HSteamListenSocket x, HSteamListenSocket y)
		{
			return x.m_HSteamListenSocket == y.m_HSteamListenSocket;
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0000FDDC File Offset: 0x0000DFDC
		public static bool operator !=(HSteamListenSocket x, HSteamListenSocket y)
		{
			return !(x == y);
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0000FDE8 File Offset: 0x0000DFE8
		public static explicit operator HSteamListenSocket(uint value)
		{
			return new HSteamListenSocket(value);
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0000FDF0 File Offset: 0x0000DFF0
		public static explicit operator uint(HSteamListenSocket that)
		{
			return that.m_HSteamListenSocket;
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0000FDF8 File Offset: 0x0000DFF8
		public bool Equals(HSteamListenSocket other)
		{
			return this.m_HSteamListenSocket == other.m_HSteamListenSocket;
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0000FE08 File Offset: 0x0000E008
		public int CompareTo(HSteamListenSocket other)
		{
			return this.m_HSteamListenSocket.CompareTo(other.m_HSteamListenSocket);
		}

		// Token: 0x04000AE3 RID: 2787
		public static readonly HSteamListenSocket Invalid = new HSteamListenSocket(0U);

		// Token: 0x04000AE4 RID: 2788
		public uint m_HSteamListenSocket;
	}
}
