using System;

namespace Steamworks
{
	// Token: 0x020001AB RID: 427
	[Serializable]
	public struct SteamItemDef_t : IEquatable<SteamItemDef_t>, IComparable<SteamItemDef_t>
	{
		// Token: 0x06000A35 RID: 2613 RVA: 0x0000F9E8 File Offset: 0x0000DBE8
		public SteamItemDef_t(int value)
		{
			this.m_SteamItemDef = value;
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0000F9F1 File Offset: 0x0000DBF1
		public override string ToString()
		{
			return this.m_SteamItemDef.ToString();
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0000F9FE File Offset: 0x0000DBFE
		public override bool Equals(object other)
		{
			return other is SteamItemDef_t && this == (SteamItemDef_t)other;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0000FA1B File Offset: 0x0000DC1B
		public override int GetHashCode()
		{
			return this.m_SteamItemDef.GetHashCode();
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0000FA28 File Offset: 0x0000DC28
		public static bool operator ==(SteamItemDef_t x, SteamItemDef_t y)
		{
			return x.m_SteamItemDef == y.m_SteamItemDef;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0000FA38 File Offset: 0x0000DC38
		public static bool operator !=(SteamItemDef_t x, SteamItemDef_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0000FA44 File Offset: 0x0000DC44
		public static explicit operator SteamItemDef_t(int value)
		{
			return new SteamItemDef_t(value);
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0000FA4C File Offset: 0x0000DC4C
		public static explicit operator int(SteamItemDef_t that)
		{
			return that.m_SteamItemDef;
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0000FA54 File Offset: 0x0000DC54
		public bool Equals(SteamItemDef_t other)
		{
			return this.m_SteamItemDef == other.m_SteamItemDef;
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0000FA64 File Offset: 0x0000DC64
		public int CompareTo(SteamItemDef_t other)
		{
			return this.m_SteamItemDef.CompareTo(other.m_SteamItemDef);
		}

		// Token: 0x04000ADA RID: 2778
		public int m_SteamItemDef;
	}
}
