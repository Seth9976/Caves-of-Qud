using System;

namespace Steamworks
{
	// Token: 0x020001C4 RID: 452
	[Serializable]
	public struct AccountID_t : IEquatable<AccountID_t>, IComparable<AccountID_t>
	{
		// Token: 0x06000B1C RID: 2844 RVA: 0x000106E0 File Offset: 0x0000E8E0
		public AccountID_t(uint value)
		{
			this.m_AccountID = value;
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x000106E9 File Offset: 0x0000E8E9
		public override string ToString()
		{
			return this.m_AccountID.ToString();
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x000106F6 File Offset: 0x0000E8F6
		public override bool Equals(object other)
		{
			return other is AccountID_t && this == (AccountID_t)other;
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00010713 File Offset: 0x0000E913
		public override int GetHashCode()
		{
			return this.m_AccountID.GetHashCode();
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x00010720 File Offset: 0x0000E920
		public static bool operator ==(AccountID_t x, AccountID_t y)
		{
			return x.m_AccountID == y.m_AccountID;
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00010730 File Offset: 0x0000E930
		public static bool operator !=(AccountID_t x, AccountID_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0001073C File Offset: 0x0000E93C
		public static explicit operator AccountID_t(uint value)
		{
			return new AccountID_t(value);
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x00010744 File Offset: 0x0000E944
		public static explicit operator uint(AccountID_t that)
		{
			return that.m_AccountID;
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0001074C File Offset: 0x0000E94C
		public bool Equals(AccountID_t other)
		{
			return this.m_AccountID == other.m_AccountID;
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0001075C File Offset: 0x0000E95C
		public int CompareTo(AccountID_t other)
		{
			return this.m_AccountID.CompareTo(other.m_AccountID);
		}

		// Token: 0x04000B31 RID: 2865
		public static readonly AccountID_t Invalid = new AccountID_t(0U);

		// Token: 0x04000B32 RID: 2866
		public uint m_AccountID;
	}
}
