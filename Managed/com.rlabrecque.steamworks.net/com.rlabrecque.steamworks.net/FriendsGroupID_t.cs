using System;

namespace Steamworks
{
	// Token: 0x0200019F RID: 415
	[Serializable]
	public struct FriendsGroupID_t : IEquatable<FriendsGroupID_t>, IComparable<FriendsGroupID_t>
	{
		// Token: 0x060009C7 RID: 2503 RVA: 0x0000F403 File Offset: 0x0000D603
		public FriendsGroupID_t(short value)
		{
			this.m_FriendsGroupID = value;
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0000F40C File Offset: 0x0000D60C
		public override string ToString()
		{
			return this.m_FriendsGroupID.ToString();
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0000F419 File Offset: 0x0000D619
		public override bool Equals(object other)
		{
			return other is FriendsGroupID_t && this == (FriendsGroupID_t)other;
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0000F436 File Offset: 0x0000D636
		public override int GetHashCode()
		{
			return this.m_FriendsGroupID.GetHashCode();
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0000F443 File Offset: 0x0000D643
		public static bool operator ==(FriendsGroupID_t x, FriendsGroupID_t y)
		{
			return x.m_FriendsGroupID == y.m_FriendsGroupID;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0000F453 File Offset: 0x0000D653
		public static bool operator !=(FriendsGroupID_t x, FriendsGroupID_t y)
		{
			return !(x == y);
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0000F45F File Offset: 0x0000D65F
		public static explicit operator FriendsGroupID_t(short value)
		{
			return new FriendsGroupID_t(value);
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0000F467 File Offset: 0x0000D667
		public static explicit operator short(FriendsGroupID_t that)
		{
			return that.m_FriendsGroupID;
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0000F46F File Offset: 0x0000D66F
		public bool Equals(FriendsGroupID_t other)
		{
			return this.m_FriendsGroupID == other.m_FriendsGroupID;
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0000F47F File Offset: 0x0000D67F
		public int CompareTo(FriendsGroupID_t other)
		{
			return this.m_FriendsGroupID.CompareTo(other.m_FriendsGroupID);
		}

		// Token: 0x04000AC7 RID: 2759
		public static readonly FriendsGroupID_t Invalid = new FriendsGroupID_t(-1);

		// Token: 0x04000AC8 RID: 2760
		public short m_FriendsGroupID;
	}
}
