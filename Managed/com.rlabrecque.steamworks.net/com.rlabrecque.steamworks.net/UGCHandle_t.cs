using System;

namespace Steamworks
{
	// Token: 0x020001C2 RID: 450
	[Serializable]
	public struct UGCHandle_t : IEquatable<UGCHandle_t>, IComparable<UGCHandle_t>
	{
		// Token: 0x06000B06 RID: 2822 RVA: 0x000105A7 File Offset: 0x0000E7A7
		public UGCHandle_t(ulong value)
		{
			this.m_UGCHandle = value;
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x000105B0 File Offset: 0x0000E7B0
		public override string ToString()
		{
			return this.m_UGCHandle.ToString();
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x000105BD File Offset: 0x0000E7BD
		public override bool Equals(object other)
		{
			return other is UGCHandle_t && this == (UGCHandle_t)other;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x000105DA File Offset: 0x0000E7DA
		public override int GetHashCode()
		{
			return this.m_UGCHandle.GetHashCode();
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x000105E7 File Offset: 0x0000E7E7
		public static bool operator ==(UGCHandle_t x, UGCHandle_t y)
		{
			return x.m_UGCHandle == y.m_UGCHandle;
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x000105F7 File Offset: 0x0000E7F7
		public static bool operator !=(UGCHandle_t x, UGCHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x00010603 File Offset: 0x0000E803
		public static explicit operator UGCHandle_t(ulong value)
		{
			return new UGCHandle_t(value);
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0001060B File Offset: 0x0000E80B
		public static explicit operator ulong(UGCHandle_t that)
		{
			return that.m_UGCHandle;
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x00010613 File Offset: 0x0000E813
		public bool Equals(UGCHandle_t other)
		{
			return this.m_UGCHandle == other.m_UGCHandle;
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x00010623 File Offset: 0x0000E823
		public int CompareTo(UGCHandle_t other)
		{
			return this.m_UGCHandle.CompareTo(other.m_UGCHandle);
		}

		// Token: 0x04000B2D RID: 2861
		public static readonly UGCHandle_t Invalid = new UGCHandle_t(ulong.MaxValue);

		// Token: 0x04000B2E RID: 2862
		public ulong m_UGCHandle;
	}
}
