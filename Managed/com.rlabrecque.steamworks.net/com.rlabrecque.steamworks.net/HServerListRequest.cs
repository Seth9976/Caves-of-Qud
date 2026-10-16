using System;

namespace Steamworks
{
	// Token: 0x020001AD RID: 429
	[Serializable]
	public struct HServerListRequest : IEquatable<HServerListRequest>
	{
		// Token: 0x06000A4A RID: 2634 RVA: 0x0000FB14 File Offset: 0x0000DD14
		public HServerListRequest(IntPtr value)
		{
			this.m_HServerListRequest = value;
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0000FB1D File Offset: 0x0000DD1D
		public override string ToString()
		{
			return this.m_HServerListRequest.ToString();
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0000FB2A File Offset: 0x0000DD2A
		public override bool Equals(object other)
		{
			return other is HServerListRequest && this == (HServerListRequest)other;
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0000FB47 File Offset: 0x0000DD47
		public override int GetHashCode()
		{
			return this.m_HServerListRequest.GetHashCode();
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0000FB54 File Offset: 0x0000DD54
		public static bool operator ==(HServerListRequest x, HServerListRequest y)
		{
			return x.m_HServerListRequest == y.m_HServerListRequest;
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0000FB67 File Offset: 0x0000DD67
		public static bool operator !=(HServerListRequest x, HServerListRequest y)
		{
			return !(x == y);
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0000FB73 File Offset: 0x0000DD73
		public static explicit operator HServerListRequest(IntPtr value)
		{
			return new HServerListRequest(value);
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0000FB7B File Offset: 0x0000DD7B
		public static explicit operator IntPtr(HServerListRequest that)
		{
			return that.m_HServerListRequest;
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0000FB83 File Offset: 0x0000DD83
		public bool Equals(HServerListRequest other)
		{
			return this.m_HServerListRequest == other.m_HServerListRequest;
		}

		// Token: 0x04000ADD RID: 2781
		public static readonly HServerListRequest Invalid = new HServerListRequest(IntPtr.Zero);

		// Token: 0x04000ADE RID: 2782
		public IntPtr m_HServerListRequest;
	}
}
