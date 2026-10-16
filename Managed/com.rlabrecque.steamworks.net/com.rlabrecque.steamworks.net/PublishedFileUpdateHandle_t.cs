using System;

namespace Steamworks
{
	// Token: 0x020001C0 RID: 448
	[Serializable]
	public struct PublishedFileUpdateHandle_t : IEquatable<PublishedFileUpdateHandle_t>, IComparable<PublishedFileUpdateHandle_t>
	{
		// Token: 0x06000AF0 RID: 2800 RVA: 0x0001046D File Offset: 0x0000E66D
		public PublishedFileUpdateHandle_t(ulong value)
		{
			this.m_PublishedFileUpdateHandle = value;
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00010476 File Offset: 0x0000E676
		public override string ToString()
		{
			return this.m_PublishedFileUpdateHandle.ToString();
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00010483 File Offset: 0x0000E683
		public override bool Equals(object other)
		{
			return other is PublishedFileUpdateHandle_t && this == (PublishedFileUpdateHandle_t)other;
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x000104A0 File Offset: 0x0000E6A0
		public override int GetHashCode()
		{
			return this.m_PublishedFileUpdateHandle.GetHashCode();
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x000104AD File Offset: 0x0000E6AD
		public static bool operator ==(PublishedFileUpdateHandle_t x, PublishedFileUpdateHandle_t y)
		{
			return x.m_PublishedFileUpdateHandle == y.m_PublishedFileUpdateHandle;
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x000104BD File Offset: 0x0000E6BD
		public static bool operator !=(PublishedFileUpdateHandle_t x, PublishedFileUpdateHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x000104C9 File Offset: 0x0000E6C9
		public static explicit operator PublishedFileUpdateHandle_t(ulong value)
		{
			return new PublishedFileUpdateHandle_t(value);
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x000104D1 File Offset: 0x0000E6D1
		public static explicit operator ulong(PublishedFileUpdateHandle_t that)
		{
			return that.m_PublishedFileUpdateHandle;
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x000104D9 File Offset: 0x0000E6D9
		public bool Equals(PublishedFileUpdateHandle_t other)
		{
			return this.m_PublishedFileUpdateHandle == other.m_PublishedFileUpdateHandle;
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x000104E9 File Offset: 0x0000E6E9
		public int CompareTo(PublishedFileUpdateHandle_t other)
		{
			return this.m_PublishedFileUpdateHandle.CompareTo(other.m_PublishedFileUpdateHandle);
		}

		// Token: 0x04000B29 RID: 2857
		public static readonly PublishedFileUpdateHandle_t Invalid = new PublishedFileUpdateHandle_t(ulong.MaxValue);

		// Token: 0x04000B2A RID: 2858
		public ulong m_PublishedFileUpdateHandle;
	}
}
