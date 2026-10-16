using System;

namespace Steamworks
{
	// Token: 0x020001C8 RID: 456
	[Serializable]
	public struct RTime32 : IEquatable<RTime32>, IComparable<RTime32>
	{
		// Token: 0x06000B48 RID: 2888 RVA: 0x00010951 File Offset: 0x0000EB51
		public RTime32(uint value)
		{
			this.m_RTime32 = value;
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0001095A File Offset: 0x0000EB5A
		public override string ToString()
		{
			return this.m_RTime32.ToString();
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00010967 File Offset: 0x0000EB67
		public override bool Equals(object other)
		{
			return other is RTime32 && this == (RTime32)other;
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00010984 File Offset: 0x0000EB84
		public override int GetHashCode()
		{
			return this.m_RTime32.GetHashCode();
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x00010991 File Offset: 0x0000EB91
		public static bool operator ==(RTime32 x, RTime32 y)
		{
			return x.m_RTime32 == y.m_RTime32;
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x000109A1 File Offset: 0x0000EBA1
		public static bool operator !=(RTime32 x, RTime32 y)
		{
			return !(x == y);
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x000109AD File Offset: 0x0000EBAD
		public static explicit operator RTime32(uint value)
		{
			return new RTime32(value);
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x000109B5 File Offset: 0x0000EBB5
		public static explicit operator uint(RTime32 that)
		{
			return that.m_RTime32;
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x000109BD File Offset: 0x0000EBBD
		public bool Equals(RTime32 other)
		{
			return this.m_RTime32 == other.m_RTime32;
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x000109CD File Offset: 0x0000EBCD
		public int CompareTo(RTime32 other)
		{
			return this.m_RTime32.CompareTo(other.m_RTime32);
		}

		// Token: 0x04000B39 RID: 2873
		public uint m_RTime32;
	}
}
