using System;

namespace Steamworks
{
	// Token: 0x020001AC RID: 428
	[Serializable]
	public struct SteamItemInstanceID_t : IEquatable<SteamItemInstanceID_t>, IComparable<SteamItemInstanceID_t>
	{
		// Token: 0x06000A3F RID: 2623 RVA: 0x0000FA77 File Offset: 0x0000DC77
		public SteamItemInstanceID_t(ulong value)
		{
			this.m_SteamItemInstanceID = value;
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0000FA80 File Offset: 0x0000DC80
		public override string ToString()
		{
			return this.m_SteamItemInstanceID.ToString();
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0000FA8D File Offset: 0x0000DC8D
		public override bool Equals(object other)
		{
			return other is SteamItemInstanceID_t && this == (SteamItemInstanceID_t)other;
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0000FAAA File Offset: 0x0000DCAA
		public override int GetHashCode()
		{
			return this.m_SteamItemInstanceID.GetHashCode();
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0000FAB7 File Offset: 0x0000DCB7
		public static bool operator ==(SteamItemInstanceID_t x, SteamItemInstanceID_t y)
		{
			return x.m_SteamItemInstanceID == y.m_SteamItemInstanceID;
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0000FAC7 File Offset: 0x0000DCC7
		public static bool operator !=(SteamItemInstanceID_t x, SteamItemInstanceID_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0000FAD3 File Offset: 0x0000DCD3
		public static explicit operator SteamItemInstanceID_t(ulong value)
		{
			return new SteamItemInstanceID_t(value);
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0000FADB File Offset: 0x0000DCDB
		public static explicit operator ulong(SteamItemInstanceID_t that)
		{
			return that.m_SteamItemInstanceID;
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0000FAE3 File Offset: 0x0000DCE3
		public bool Equals(SteamItemInstanceID_t other)
		{
			return this.m_SteamItemInstanceID == other.m_SteamItemInstanceID;
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0000FAF3 File Offset: 0x0000DCF3
		public int CompareTo(SteamItemInstanceID_t other)
		{
			return this.m_SteamItemInstanceID.CompareTo(other.m_SteamItemInstanceID);
		}

		// Token: 0x04000ADB RID: 2779
		public static readonly SteamItemInstanceID_t Invalid = new SteamItemInstanceID_t(ulong.MaxValue);

		// Token: 0x04000ADC RID: 2780
		public ulong m_SteamItemInstanceID;
	}
}
