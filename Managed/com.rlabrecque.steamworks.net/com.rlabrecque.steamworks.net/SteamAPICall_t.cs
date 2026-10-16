using System;

namespace Steamworks
{
	// Token: 0x020001C9 RID: 457
	[Serializable]
	public struct SteamAPICall_t : IEquatable<SteamAPICall_t>, IComparable<SteamAPICall_t>
	{
		// Token: 0x06000B52 RID: 2898 RVA: 0x000109E0 File Offset: 0x0000EBE0
		public SteamAPICall_t(ulong value)
		{
			this.m_SteamAPICall = value;
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x000109E9 File Offset: 0x0000EBE9
		public override string ToString()
		{
			return this.m_SteamAPICall.ToString();
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x000109F6 File Offset: 0x0000EBF6
		public override bool Equals(object other)
		{
			return other is SteamAPICall_t && this == (SteamAPICall_t)other;
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00010A13 File Offset: 0x0000EC13
		public override int GetHashCode()
		{
			return this.m_SteamAPICall.GetHashCode();
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x00010A20 File Offset: 0x0000EC20
		public static bool operator ==(SteamAPICall_t x, SteamAPICall_t y)
		{
			return x.m_SteamAPICall == y.m_SteamAPICall;
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x00010A30 File Offset: 0x0000EC30
		public static bool operator !=(SteamAPICall_t x, SteamAPICall_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00010A3C File Offset: 0x0000EC3C
		public static explicit operator SteamAPICall_t(ulong value)
		{
			return new SteamAPICall_t(value);
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x00010A44 File Offset: 0x0000EC44
		public static explicit operator ulong(SteamAPICall_t that)
		{
			return that.m_SteamAPICall;
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x00010A4C File Offset: 0x0000EC4C
		public bool Equals(SteamAPICall_t other)
		{
			return this.m_SteamAPICall == other.m_SteamAPICall;
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00010A5C File Offset: 0x0000EC5C
		public int CompareTo(SteamAPICall_t other)
		{
			return this.m_SteamAPICall.CompareTo(other.m_SteamAPICall);
		}

		// Token: 0x04000B3A RID: 2874
		public static readonly SteamAPICall_t Invalid = new SteamAPICall_t(0UL);

		// Token: 0x04000B3B RID: 2875
		public ulong m_SteamAPICall;
	}
}
