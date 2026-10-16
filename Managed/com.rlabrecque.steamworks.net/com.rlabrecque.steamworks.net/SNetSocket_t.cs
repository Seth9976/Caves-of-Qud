using System;

namespace Steamworks
{
	// Token: 0x020001B0 RID: 432
	[Serializable]
	public struct SNetSocket_t : IEquatable<SNetSocket_t>, IComparable<SNetSocket_t>
	{
		// Token: 0x06000A69 RID: 2665 RVA: 0x0000FCD2 File Offset: 0x0000DED2
		public SNetSocket_t(uint value)
		{
			this.m_SNetSocket = value;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0000FCDB File Offset: 0x0000DEDB
		public override string ToString()
		{
			return this.m_SNetSocket.ToString();
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0000FCE8 File Offset: 0x0000DEE8
		public override bool Equals(object other)
		{
			return other is SNetSocket_t && this == (SNetSocket_t)other;
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0000FD05 File Offset: 0x0000DF05
		public override int GetHashCode()
		{
			return this.m_SNetSocket.GetHashCode();
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0000FD12 File Offset: 0x0000DF12
		public static bool operator ==(SNetSocket_t x, SNetSocket_t y)
		{
			return x.m_SNetSocket == y.m_SNetSocket;
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0000FD22 File Offset: 0x0000DF22
		public static bool operator !=(SNetSocket_t x, SNetSocket_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0000FD2E File Offset: 0x0000DF2E
		public static explicit operator SNetSocket_t(uint value)
		{
			return new SNetSocket_t(value);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0000FD36 File Offset: 0x0000DF36
		public static explicit operator uint(SNetSocket_t that)
		{
			return that.m_SNetSocket;
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0000FD3E File Offset: 0x0000DF3E
		public bool Equals(SNetSocket_t other)
		{
			return this.m_SNetSocket == other.m_SNetSocket;
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0000FD4E File Offset: 0x0000DF4E
		public int CompareTo(SNetSocket_t other)
		{
			return this.m_SNetSocket.CompareTo(other.m_SNetSocket);
		}

		// Token: 0x04000AE2 RID: 2786
		public uint m_SNetSocket;
	}
}
