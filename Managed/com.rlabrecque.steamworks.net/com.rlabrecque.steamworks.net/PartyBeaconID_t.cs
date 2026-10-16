using System;

namespace Steamworks
{
	// Token: 0x020001C7 RID: 455
	[Serializable]
	public struct PartyBeaconID_t : IEquatable<PartyBeaconID_t>, IComparable<PartyBeaconID_t>
	{
		// Token: 0x06000B3D RID: 2877 RVA: 0x000108B4 File Offset: 0x0000EAB4
		public PartyBeaconID_t(ulong value)
		{
			this.m_PartyBeaconID = value;
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x000108BD File Offset: 0x0000EABD
		public override string ToString()
		{
			return this.m_PartyBeaconID.ToString();
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x000108CA File Offset: 0x0000EACA
		public override bool Equals(object other)
		{
			return other is PartyBeaconID_t && this == (PartyBeaconID_t)other;
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x000108E7 File Offset: 0x0000EAE7
		public override int GetHashCode()
		{
			return this.m_PartyBeaconID.GetHashCode();
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x000108F4 File Offset: 0x0000EAF4
		public static bool operator ==(PartyBeaconID_t x, PartyBeaconID_t y)
		{
			return x.m_PartyBeaconID == y.m_PartyBeaconID;
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x00010904 File Offset: 0x0000EB04
		public static bool operator !=(PartyBeaconID_t x, PartyBeaconID_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x00010910 File Offset: 0x0000EB10
		public static explicit operator PartyBeaconID_t(ulong value)
		{
			return new PartyBeaconID_t(value);
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00010918 File Offset: 0x0000EB18
		public static explicit operator ulong(PartyBeaconID_t that)
		{
			return that.m_PartyBeaconID;
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x00010920 File Offset: 0x0000EB20
		public bool Equals(PartyBeaconID_t other)
		{
			return this.m_PartyBeaconID == other.m_PartyBeaconID;
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00010930 File Offset: 0x0000EB30
		public int CompareTo(PartyBeaconID_t other)
		{
			return this.m_PartyBeaconID.CompareTo(other.m_PartyBeaconID);
		}

		// Token: 0x04000B37 RID: 2871
		public static readonly PartyBeaconID_t Invalid = new PartyBeaconID_t(0UL);

		// Token: 0x04000B38 RID: 2872
		public ulong m_PartyBeaconID;
	}
}
