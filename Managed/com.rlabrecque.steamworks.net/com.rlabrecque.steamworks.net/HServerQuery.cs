using System;

namespace Steamworks
{
	// Token: 0x020001AE RID: 430
	[Serializable]
	public struct HServerQuery : IEquatable<HServerQuery>, IComparable<HServerQuery>
	{
		// Token: 0x06000A54 RID: 2644 RVA: 0x0000FBA7 File Offset: 0x0000DDA7
		public HServerQuery(int value)
		{
			this.m_HServerQuery = value;
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0000FBB0 File Offset: 0x0000DDB0
		public override string ToString()
		{
			return this.m_HServerQuery.ToString();
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0000FBBD File Offset: 0x0000DDBD
		public override bool Equals(object other)
		{
			return other is HServerQuery && this == (HServerQuery)other;
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0000FBDA File Offset: 0x0000DDDA
		public override int GetHashCode()
		{
			return this.m_HServerQuery.GetHashCode();
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0000FBE7 File Offset: 0x0000DDE7
		public static bool operator ==(HServerQuery x, HServerQuery y)
		{
			return x.m_HServerQuery == y.m_HServerQuery;
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0000FBF7 File Offset: 0x0000DDF7
		public static bool operator !=(HServerQuery x, HServerQuery y)
		{
			return !(x == y);
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0000FC03 File Offset: 0x0000DE03
		public static explicit operator HServerQuery(int value)
		{
			return new HServerQuery(value);
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0000FC0B File Offset: 0x0000DE0B
		public static explicit operator int(HServerQuery that)
		{
			return that.m_HServerQuery;
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0000FC13 File Offset: 0x0000DE13
		public bool Equals(HServerQuery other)
		{
			return this.m_HServerQuery == other.m_HServerQuery;
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0000FC23 File Offset: 0x0000DE23
		public int CompareTo(HServerQuery other)
		{
			return this.m_HServerQuery.CompareTo(other.m_HServerQuery);
		}

		// Token: 0x04000ADF RID: 2783
		public static readonly HServerQuery Invalid = new HServerQuery(-1);

		// Token: 0x04000AE0 RID: 2784
		public int m_HServerQuery;
	}
}
