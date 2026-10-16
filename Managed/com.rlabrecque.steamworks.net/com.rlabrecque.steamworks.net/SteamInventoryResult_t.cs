using System;

namespace Steamworks
{
	// Token: 0x020001A9 RID: 425
	[Serializable]
	public struct SteamInventoryResult_t : IEquatable<SteamInventoryResult_t>, IComparable<SteamInventoryResult_t>
	{
		// Token: 0x06000A1F RID: 2591 RVA: 0x0000F8AF File Offset: 0x0000DAAF
		public SteamInventoryResult_t(int value)
		{
			this.m_SteamInventoryResult = value;
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0000F8B8 File Offset: 0x0000DAB8
		public override string ToString()
		{
			return this.m_SteamInventoryResult.ToString();
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0000F8C5 File Offset: 0x0000DAC5
		public override bool Equals(object other)
		{
			return other is SteamInventoryResult_t && this == (SteamInventoryResult_t)other;
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0000F8E2 File Offset: 0x0000DAE2
		public override int GetHashCode()
		{
			return this.m_SteamInventoryResult.GetHashCode();
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0000F8EF File Offset: 0x0000DAEF
		public static bool operator ==(SteamInventoryResult_t x, SteamInventoryResult_t y)
		{
			return x.m_SteamInventoryResult == y.m_SteamInventoryResult;
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0000F8FF File Offset: 0x0000DAFF
		public static bool operator !=(SteamInventoryResult_t x, SteamInventoryResult_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0000F90B File Offset: 0x0000DB0B
		public static explicit operator SteamInventoryResult_t(int value)
		{
			return new SteamInventoryResult_t(value);
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0000F913 File Offset: 0x0000DB13
		public static explicit operator int(SteamInventoryResult_t that)
		{
			return that.m_SteamInventoryResult;
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0000F91B File Offset: 0x0000DB1B
		public bool Equals(SteamInventoryResult_t other)
		{
			return this.m_SteamInventoryResult == other.m_SteamInventoryResult;
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0000F92B File Offset: 0x0000DB2B
		public int CompareTo(SteamInventoryResult_t other)
		{
			return this.m_SteamInventoryResult.CompareTo(other.m_SteamInventoryResult);
		}

		// Token: 0x04000AD6 RID: 2774
		public static readonly SteamInventoryResult_t Invalid = new SteamInventoryResult_t(-1);

		// Token: 0x04000AD7 RID: 2775
		public int m_SteamInventoryResult;
	}
}
