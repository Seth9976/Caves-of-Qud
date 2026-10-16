using System;

namespace Steamworks
{
	// Token: 0x020001C5 RID: 453
	[Serializable]
	public struct AppId_t : IEquatable<AppId_t>, IComparable<AppId_t>
	{
		// Token: 0x06000B27 RID: 2855 RVA: 0x0001077C File Offset: 0x0000E97C
		public AppId_t(uint value)
		{
			this.m_AppId = value;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00010785 File Offset: 0x0000E985
		public override string ToString()
		{
			return this.m_AppId.ToString();
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00010792 File Offset: 0x0000E992
		public override bool Equals(object other)
		{
			return other is AppId_t && this == (AppId_t)other;
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x000107AF File Offset: 0x0000E9AF
		public override int GetHashCode()
		{
			return this.m_AppId.GetHashCode();
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x000107BC File Offset: 0x0000E9BC
		public static bool operator ==(AppId_t x, AppId_t y)
		{
			return x.m_AppId == y.m_AppId;
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x000107CC File Offset: 0x0000E9CC
		public static bool operator !=(AppId_t x, AppId_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x000107D8 File Offset: 0x0000E9D8
		public static explicit operator AppId_t(uint value)
		{
			return new AppId_t(value);
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x000107E0 File Offset: 0x0000E9E0
		public static explicit operator uint(AppId_t that)
		{
			return that.m_AppId;
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x000107E8 File Offset: 0x0000E9E8
		public bool Equals(AppId_t other)
		{
			return this.m_AppId == other.m_AppId;
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x000107F8 File Offset: 0x0000E9F8
		public int CompareTo(AppId_t other)
		{
			return this.m_AppId.CompareTo(other.m_AppId);
		}

		// Token: 0x04000B33 RID: 2867
		public static readonly AppId_t Invalid = new AppId_t(0U);

		// Token: 0x04000B34 RID: 2868
		public uint m_AppId;
	}
}
