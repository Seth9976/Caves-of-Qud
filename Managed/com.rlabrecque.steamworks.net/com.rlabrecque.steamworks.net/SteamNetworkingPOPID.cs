using System;

namespace Steamworks
{
	// Token: 0x020001BD RID: 445
	[Serializable]
	public struct SteamNetworkingPOPID : IEquatable<SteamNetworkingPOPID>, IComparable<SteamNetworkingPOPID>
	{
		// Token: 0x06000AD1 RID: 2769 RVA: 0x000102B2 File Offset: 0x0000E4B2
		public SteamNetworkingPOPID(uint value)
		{
			this.m_SteamNetworkingPOPID = value;
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x000102BB File Offset: 0x0000E4BB
		public override string ToString()
		{
			return this.m_SteamNetworkingPOPID.ToString();
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x000102C8 File Offset: 0x0000E4C8
		public override bool Equals(object other)
		{
			return other is SteamNetworkingPOPID && this == (SteamNetworkingPOPID)other;
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x000102E5 File Offset: 0x0000E4E5
		public override int GetHashCode()
		{
			return this.m_SteamNetworkingPOPID.GetHashCode();
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x000102F2 File Offset: 0x0000E4F2
		public static bool operator ==(SteamNetworkingPOPID x, SteamNetworkingPOPID y)
		{
			return x.m_SteamNetworkingPOPID == y.m_SteamNetworkingPOPID;
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00010302 File Offset: 0x0000E502
		public static bool operator !=(SteamNetworkingPOPID x, SteamNetworkingPOPID y)
		{
			return !(x == y);
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0001030E File Offset: 0x0000E50E
		public static explicit operator SteamNetworkingPOPID(uint value)
		{
			return new SteamNetworkingPOPID(value);
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00010316 File Offset: 0x0000E516
		public static explicit operator uint(SteamNetworkingPOPID that)
		{
			return that.m_SteamNetworkingPOPID;
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0001031E File Offset: 0x0000E51E
		public bool Equals(SteamNetworkingPOPID other)
		{
			return this.m_SteamNetworkingPOPID == other.m_SteamNetworkingPOPID;
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0001032E File Offset: 0x0000E52E
		public int CompareTo(SteamNetworkingPOPID other)
		{
			return this.m_SteamNetworkingPOPID.CompareTo(other.m_SteamNetworkingPOPID);
		}

		// Token: 0x04000B25 RID: 2853
		public uint m_SteamNetworkingPOPID;
	}
}
