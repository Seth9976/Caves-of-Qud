using System;

namespace Steamworks
{
	// Token: 0x020001AA RID: 426
	[Serializable]
	public struct SteamInventoryUpdateHandle_t : IEquatable<SteamInventoryUpdateHandle_t>, IComparable<SteamInventoryUpdateHandle_t>
	{
		// Token: 0x06000A2A RID: 2602 RVA: 0x0000F94B File Offset: 0x0000DB4B
		public SteamInventoryUpdateHandle_t(ulong value)
		{
			this.m_SteamInventoryUpdateHandle = value;
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0000F954 File Offset: 0x0000DB54
		public override string ToString()
		{
			return this.m_SteamInventoryUpdateHandle.ToString();
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0000F961 File Offset: 0x0000DB61
		public override bool Equals(object other)
		{
			return other is SteamInventoryUpdateHandle_t && this == (SteamInventoryUpdateHandle_t)other;
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0000F97E File Offset: 0x0000DB7E
		public override int GetHashCode()
		{
			return this.m_SteamInventoryUpdateHandle.GetHashCode();
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0000F98B File Offset: 0x0000DB8B
		public static bool operator ==(SteamInventoryUpdateHandle_t x, SteamInventoryUpdateHandle_t y)
		{
			return x.m_SteamInventoryUpdateHandle == y.m_SteamInventoryUpdateHandle;
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0000F99B File Offset: 0x0000DB9B
		public static bool operator !=(SteamInventoryUpdateHandle_t x, SteamInventoryUpdateHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0000F9A7 File Offset: 0x0000DBA7
		public static explicit operator SteamInventoryUpdateHandle_t(ulong value)
		{
			return new SteamInventoryUpdateHandle_t(value);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0000F9AF File Offset: 0x0000DBAF
		public static explicit operator ulong(SteamInventoryUpdateHandle_t that)
		{
			return that.m_SteamInventoryUpdateHandle;
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0000F9B7 File Offset: 0x0000DBB7
		public bool Equals(SteamInventoryUpdateHandle_t other)
		{
			return this.m_SteamInventoryUpdateHandle == other.m_SteamInventoryUpdateHandle;
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0000F9C7 File Offset: 0x0000DBC7
		public int CompareTo(SteamInventoryUpdateHandle_t other)
		{
			return this.m_SteamInventoryUpdateHandle.CompareTo(other.m_SteamInventoryUpdateHandle);
		}

		// Token: 0x04000AD8 RID: 2776
		public static readonly SteamInventoryUpdateHandle_t Invalid = new SteamInventoryUpdateHandle_t(ulong.MaxValue);

		// Token: 0x04000AD9 RID: 2777
		public ulong m_SteamInventoryUpdateHandle;
	}
}
