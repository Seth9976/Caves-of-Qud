using System;

namespace Steamworks
{
	// Token: 0x020001A0 RID: 416
	[Serializable]
	public struct HHTMLBrowser : IEquatable<HHTMLBrowser>, IComparable<HHTMLBrowser>
	{
		// Token: 0x060009D2 RID: 2514 RVA: 0x0000F49F File Offset: 0x0000D69F
		public HHTMLBrowser(uint value)
		{
			this.m_HHTMLBrowser = value;
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0000F4A8 File Offset: 0x0000D6A8
		public override string ToString()
		{
			return this.m_HHTMLBrowser.ToString();
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0000F4B5 File Offset: 0x0000D6B5
		public override bool Equals(object other)
		{
			return other is HHTMLBrowser && this == (HHTMLBrowser)other;
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0000F4D2 File Offset: 0x0000D6D2
		public override int GetHashCode()
		{
			return this.m_HHTMLBrowser.GetHashCode();
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0000F4DF File Offset: 0x0000D6DF
		public static bool operator ==(HHTMLBrowser x, HHTMLBrowser y)
		{
			return x.m_HHTMLBrowser == y.m_HHTMLBrowser;
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0000F4EF File Offset: 0x0000D6EF
		public static bool operator !=(HHTMLBrowser x, HHTMLBrowser y)
		{
			return !(x == y);
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0000F4FB File Offset: 0x0000D6FB
		public static explicit operator HHTMLBrowser(uint value)
		{
			return new HHTMLBrowser(value);
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0000F503 File Offset: 0x0000D703
		public static explicit operator uint(HHTMLBrowser that)
		{
			return that.m_HHTMLBrowser;
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0000F50B File Offset: 0x0000D70B
		public bool Equals(HHTMLBrowser other)
		{
			return this.m_HHTMLBrowser == other.m_HHTMLBrowser;
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0000F51B File Offset: 0x0000D71B
		public int CompareTo(HHTMLBrowser other)
		{
			return this.m_HHTMLBrowser.CompareTo(other.m_HHTMLBrowser);
		}

		// Token: 0x04000AC9 RID: 2761
		public static readonly HHTMLBrowser Invalid = new HHTMLBrowser(0U);

		// Token: 0x04000ACA RID: 2762
		public uint m_HHTMLBrowser;
	}
}
