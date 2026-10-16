using System;

namespace Steamworks
{
	// Token: 0x020001C6 RID: 454
	[Serializable]
	public struct DepotId_t : IEquatable<DepotId_t>, IComparable<DepotId_t>
	{
		// Token: 0x06000B32 RID: 2866 RVA: 0x00010818 File Offset: 0x0000EA18
		public DepotId_t(uint value)
		{
			this.m_DepotId = value;
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00010821 File Offset: 0x0000EA21
		public override string ToString()
		{
			return this.m_DepotId.ToString();
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0001082E File Offset: 0x0000EA2E
		public override bool Equals(object other)
		{
			return other is DepotId_t && this == (DepotId_t)other;
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0001084B File Offset: 0x0000EA4B
		public override int GetHashCode()
		{
			return this.m_DepotId.GetHashCode();
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00010858 File Offset: 0x0000EA58
		public static bool operator ==(DepotId_t x, DepotId_t y)
		{
			return x.m_DepotId == y.m_DepotId;
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x00010868 File Offset: 0x0000EA68
		public static bool operator !=(DepotId_t x, DepotId_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00010874 File Offset: 0x0000EA74
		public static explicit operator DepotId_t(uint value)
		{
			return new DepotId_t(value);
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0001087C File Offset: 0x0000EA7C
		public static explicit operator uint(DepotId_t that)
		{
			return that.m_DepotId;
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00010884 File Offset: 0x0000EA84
		public bool Equals(DepotId_t other)
		{
			return this.m_DepotId == other.m_DepotId;
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00010894 File Offset: 0x0000EA94
		public int CompareTo(DepotId_t other)
		{
			return this.m_DepotId.CompareTo(other.m_DepotId);
		}

		// Token: 0x04000B35 RID: 2869
		public static readonly DepotId_t Invalid = new DepotId_t(0U);

		// Token: 0x04000B36 RID: 2870
		public uint m_DepotId;
	}
}
