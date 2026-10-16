using System;

namespace Steamworks
{
	// Token: 0x020001CF RID: 463
	[Serializable]
	public struct HSteamPipe : IEquatable<HSteamPipe>, IComparable<HSteamPipe>
	{
		// Token: 0x06000B8C RID: 2956 RVA: 0x00010EB6 File Offset: 0x0000F0B6
		public HSteamPipe(int value)
		{
			this.m_HSteamPipe = value;
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00010EBF File Offset: 0x0000F0BF
		public override string ToString()
		{
			return this.m_HSteamPipe.ToString();
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00010ECC File Offset: 0x0000F0CC
		public override bool Equals(object other)
		{
			return other is HSteamPipe && this == (HSteamPipe)other;
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x00010EE9 File Offset: 0x0000F0E9
		public override int GetHashCode()
		{
			return this.m_HSteamPipe.GetHashCode();
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x00010EF6 File Offset: 0x0000F0F6
		public static bool operator ==(HSteamPipe x, HSteamPipe y)
		{
			return x.m_HSteamPipe == y.m_HSteamPipe;
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00010F06 File Offset: 0x0000F106
		public static bool operator !=(HSteamPipe x, HSteamPipe y)
		{
			return !(x == y);
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x00010F12 File Offset: 0x0000F112
		public static explicit operator HSteamPipe(int value)
		{
			return new HSteamPipe(value);
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x00010F1A File Offset: 0x0000F11A
		public static explicit operator int(HSteamPipe that)
		{
			return that.m_HSteamPipe;
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x00010F22 File Offset: 0x0000F122
		public bool Equals(HSteamPipe other)
		{
			return this.m_HSteamPipe == other.m_HSteamPipe;
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00010F32 File Offset: 0x0000F132
		public int CompareTo(HSteamPipe other)
		{
			return this.m_HSteamPipe.CompareTo(other.m_HSteamPipe);
		}

		// Token: 0x04000B45 RID: 2885
		public int m_HSteamPipe;
	}
}
