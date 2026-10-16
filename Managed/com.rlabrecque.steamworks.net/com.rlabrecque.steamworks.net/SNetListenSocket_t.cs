using System;

namespace Steamworks
{
	// Token: 0x020001AF RID: 431
	[Serializable]
	public struct SNetListenSocket_t : IEquatable<SNetListenSocket_t>, IComparable<SNetListenSocket_t>
	{
		// Token: 0x06000A5F RID: 2655 RVA: 0x0000FC43 File Offset: 0x0000DE43
		public SNetListenSocket_t(uint value)
		{
			this.m_SNetListenSocket = value;
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0000FC4C File Offset: 0x0000DE4C
		public override string ToString()
		{
			return this.m_SNetListenSocket.ToString();
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0000FC59 File Offset: 0x0000DE59
		public override bool Equals(object other)
		{
			return other is SNetListenSocket_t && this == (SNetListenSocket_t)other;
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0000FC76 File Offset: 0x0000DE76
		public override int GetHashCode()
		{
			return this.m_SNetListenSocket.GetHashCode();
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0000FC83 File Offset: 0x0000DE83
		public static bool operator ==(SNetListenSocket_t x, SNetListenSocket_t y)
		{
			return x.m_SNetListenSocket == y.m_SNetListenSocket;
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0000FC93 File Offset: 0x0000DE93
		public static bool operator !=(SNetListenSocket_t x, SNetListenSocket_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0000FC9F File Offset: 0x0000DE9F
		public static explicit operator SNetListenSocket_t(uint value)
		{
			return new SNetListenSocket_t(value);
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0000FCA7 File Offset: 0x0000DEA7
		public static explicit operator uint(SNetListenSocket_t that)
		{
			return that.m_SNetListenSocket;
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0000FCAF File Offset: 0x0000DEAF
		public bool Equals(SNetListenSocket_t other)
		{
			return this.m_SNetListenSocket == other.m_SNetListenSocket;
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0000FCBF File Offset: 0x0000DEBF
		public int CompareTo(SNetListenSocket_t other)
		{
			return this.m_SNetListenSocket.CompareTo(other.m_SNetListenSocket);
		}

		// Token: 0x04000AE1 RID: 2785
		public uint m_SNetListenSocket;
	}
}
