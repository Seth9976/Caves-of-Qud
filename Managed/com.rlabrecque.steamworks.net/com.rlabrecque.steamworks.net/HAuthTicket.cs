using System;

namespace Steamworks
{
	// Token: 0x0200019C RID: 412
	[Serializable]
	public struct HAuthTicket : IEquatable<HAuthTicket>, IComparable<HAuthTicket>
	{
		// Token: 0x060009BA RID: 2490 RVA: 0x0000F34C File Offset: 0x0000D54C
		public HAuthTicket(uint value)
		{
			this.m_HAuthTicket = value;
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0000F355 File Offset: 0x0000D555
		public override string ToString()
		{
			return this.m_HAuthTicket.ToString();
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0000F362 File Offset: 0x0000D562
		public override bool Equals(object other)
		{
			return other is HAuthTicket && this == (HAuthTicket)other;
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0000F37F File Offset: 0x0000D57F
		public override int GetHashCode()
		{
			return this.m_HAuthTicket.GetHashCode();
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0000F38C File Offset: 0x0000D58C
		public static bool operator ==(HAuthTicket x, HAuthTicket y)
		{
			return x.m_HAuthTicket == y.m_HAuthTicket;
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0000F39C File Offset: 0x0000D59C
		public static bool operator !=(HAuthTicket x, HAuthTicket y)
		{
			return !(x == y);
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0000F3A8 File Offset: 0x0000D5A8
		public static explicit operator HAuthTicket(uint value)
		{
			return new HAuthTicket(value);
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0000F3B0 File Offset: 0x0000D5B0
		public static explicit operator uint(HAuthTicket that)
		{
			return that.m_HAuthTicket;
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0000F3B8 File Offset: 0x0000D5B8
		public bool Equals(HAuthTicket other)
		{
			return this.m_HAuthTicket == other.m_HAuthTicket;
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0000F3C8 File Offset: 0x0000D5C8
		public int CompareTo(HAuthTicket other)
		{
			return this.m_HAuthTicket.CompareTo(other.m_HAuthTicket);
		}

		// Token: 0x04000AB9 RID: 2745
		public static readonly HAuthTicket Invalid = new HAuthTicket(0U);

		// Token: 0x04000ABA RID: 2746
		public uint m_HAuthTicket;
	}
}
