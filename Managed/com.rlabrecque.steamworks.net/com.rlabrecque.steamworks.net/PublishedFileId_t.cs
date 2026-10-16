using System;

namespace Steamworks
{
	// Token: 0x020001BF RID: 447
	[Serializable]
	public struct PublishedFileId_t : IEquatable<PublishedFileId_t>, IComparable<PublishedFileId_t>
	{
		// Token: 0x06000AE5 RID: 2789 RVA: 0x000103D0 File Offset: 0x0000E5D0
		public PublishedFileId_t(ulong value)
		{
			this.m_PublishedFileId = value;
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x000103D9 File Offset: 0x0000E5D9
		public override string ToString()
		{
			return this.m_PublishedFileId.ToString();
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x000103E6 File Offset: 0x0000E5E6
		public override bool Equals(object other)
		{
			return other is PublishedFileId_t && this == (PublishedFileId_t)other;
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x00010403 File Offset: 0x0000E603
		public override int GetHashCode()
		{
			return this.m_PublishedFileId.GetHashCode();
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x00010410 File Offset: 0x0000E610
		public static bool operator ==(PublishedFileId_t x, PublishedFileId_t y)
		{
			return x.m_PublishedFileId == y.m_PublishedFileId;
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x00010420 File Offset: 0x0000E620
		public static bool operator !=(PublishedFileId_t x, PublishedFileId_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0001042C File Offset: 0x0000E62C
		public static explicit operator PublishedFileId_t(ulong value)
		{
			return new PublishedFileId_t(value);
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x00010434 File Offset: 0x0000E634
		public static explicit operator ulong(PublishedFileId_t that)
		{
			return that.m_PublishedFileId;
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0001043C File Offset: 0x0000E63C
		public bool Equals(PublishedFileId_t other)
		{
			return this.m_PublishedFileId == other.m_PublishedFileId;
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0001044C File Offset: 0x0000E64C
		public int CompareTo(PublishedFileId_t other)
		{
			return this.m_PublishedFileId.CompareTo(other.m_PublishedFileId);
		}

		// Token: 0x04000B27 RID: 2855
		public static readonly PublishedFileId_t Invalid = new PublishedFileId_t(0UL);

		// Token: 0x04000B28 RID: 2856
		public ulong m_PublishedFileId;
	}
}
