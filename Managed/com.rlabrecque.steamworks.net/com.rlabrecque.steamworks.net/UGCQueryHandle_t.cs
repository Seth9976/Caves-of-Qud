using System;

namespace Steamworks
{
	// Token: 0x020001CB RID: 459
	[Serializable]
	public struct UGCQueryHandle_t : IEquatable<UGCQueryHandle_t>, IComparable<UGCQueryHandle_t>
	{
		// Token: 0x06000B62 RID: 2914 RVA: 0x00010C5E File Offset: 0x0000EE5E
		public UGCQueryHandle_t(ulong value)
		{
			this.m_UGCQueryHandle = value;
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x00010C67 File Offset: 0x0000EE67
		public override string ToString()
		{
			return this.m_UGCQueryHandle.ToString();
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x00010C74 File Offset: 0x0000EE74
		public override bool Equals(object other)
		{
			return other is UGCQueryHandle_t && this == (UGCQueryHandle_t)other;
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x00010C91 File Offset: 0x0000EE91
		public override int GetHashCode()
		{
			return this.m_UGCQueryHandle.GetHashCode();
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x00010C9E File Offset: 0x0000EE9E
		public static bool operator ==(UGCQueryHandle_t x, UGCQueryHandle_t y)
		{
			return x.m_UGCQueryHandle == y.m_UGCQueryHandle;
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x00010CAE File Offset: 0x0000EEAE
		public static bool operator !=(UGCQueryHandle_t x, UGCQueryHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x00010CBA File Offset: 0x0000EEBA
		public static explicit operator UGCQueryHandle_t(ulong value)
		{
			return new UGCQueryHandle_t(value);
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x00010CC2 File Offset: 0x0000EEC2
		public static explicit operator ulong(UGCQueryHandle_t that)
		{
			return that.m_UGCQueryHandle;
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x00010CCA File Offset: 0x0000EECA
		public bool Equals(UGCQueryHandle_t other)
		{
			return this.m_UGCQueryHandle == other.m_UGCQueryHandle;
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x00010CDA File Offset: 0x0000EEDA
		public int CompareTo(UGCQueryHandle_t other)
		{
			return this.m_UGCQueryHandle.CompareTo(other.m_UGCQueryHandle);
		}

		// Token: 0x04000B3F RID: 2879
		public static readonly UGCQueryHandle_t Invalid = new UGCQueryHandle_t(ulong.MaxValue);

		// Token: 0x04000B40 RID: 2880
		public ulong m_UGCQueryHandle;
	}
}
