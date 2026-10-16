using System;

namespace Steamworks
{
	// Token: 0x020001CC RID: 460
	[Serializable]
	public struct UGCUpdateHandle_t : IEquatable<UGCUpdateHandle_t>, IComparable<UGCUpdateHandle_t>
	{
		// Token: 0x06000B6D RID: 2925 RVA: 0x00010CFB File Offset: 0x0000EEFB
		public UGCUpdateHandle_t(ulong value)
		{
			this.m_UGCUpdateHandle = value;
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x00010D04 File Offset: 0x0000EF04
		public override string ToString()
		{
			return this.m_UGCUpdateHandle.ToString();
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x00010D11 File Offset: 0x0000EF11
		public override bool Equals(object other)
		{
			return other is UGCUpdateHandle_t && this == (UGCUpdateHandle_t)other;
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x00010D2E File Offset: 0x0000EF2E
		public override int GetHashCode()
		{
			return this.m_UGCUpdateHandle.GetHashCode();
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00010D3B File Offset: 0x0000EF3B
		public static bool operator ==(UGCUpdateHandle_t x, UGCUpdateHandle_t y)
		{
			return x.m_UGCUpdateHandle == y.m_UGCUpdateHandle;
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00010D4B File Offset: 0x0000EF4B
		public static bool operator !=(UGCUpdateHandle_t x, UGCUpdateHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00010D57 File Offset: 0x0000EF57
		public static explicit operator UGCUpdateHandle_t(ulong value)
		{
			return new UGCUpdateHandle_t(value);
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x00010D5F File Offset: 0x0000EF5F
		public static explicit operator ulong(UGCUpdateHandle_t that)
		{
			return that.m_UGCUpdateHandle;
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x00010D67 File Offset: 0x0000EF67
		public bool Equals(UGCUpdateHandle_t other)
		{
			return this.m_UGCUpdateHandle == other.m_UGCUpdateHandle;
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x00010D77 File Offset: 0x0000EF77
		public int CompareTo(UGCUpdateHandle_t other)
		{
			return this.m_UGCUpdateHandle.CompareTo(other.m_UGCUpdateHandle);
		}

		// Token: 0x04000B41 RID: 2881
		public static readonly UGCUpdateHandle_t Invalid = new UGCUpdateHandle_t(ulong.MaxValue);

		// Token: 0x04000B42 RID: 2882
		public ulong m_UGCUpdateHandle;
	}
}
