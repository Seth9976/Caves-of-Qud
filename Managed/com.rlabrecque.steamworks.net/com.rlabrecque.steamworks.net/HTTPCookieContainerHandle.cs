using System;

namespace Steamworks
{
	// Token: 0x020001A1 RID: 417
	[Serializable]
	public struct HTTPCookieContainerHandle : IEquatable<HTTPCookieContainerHandle>, IComparable<HTTPCookieContainerHandle>
	{
		// Token: 0x060009DD RID: 2525 RVA: 0x0000F53B File Offset: 0x0000D73B
		public HTTPCookieContainerHandle(uint value)
		{
			this.m_HTTPCookieContainerHandle = value;
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0000F544 File Offset: 0x0000D744
		public override string ToString()
		{
			return this.m_HTTPCookieContainerHandle.ToString();
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0000F551 File Offset: 0x0000D751
		public override bool Equals(object other)
		{
			return other is HTTPCookieContainerHandle && this == (HTTPCookieContainerHandle)other;
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0000F56E File Offset: 0x0000D76E
		public override int GetHashCode()
		{
			return this.m_HTTPCookieContainerHandle.GetHashCode();
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0000F57B File Offset: 0x0000D77B
		public static bool operator ==(HTTPCookieContainerHandle x, HTTPCookieContainerHandle y)
		{
			return x.m_HTTPCookieContainerHandle == y.m_HTTPCookieContainerHandle;
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0000F58B File Offset: 0x0000D78B
		public static bool operator !=(HTTPCookieContainerHandle x, HTTPCookieContainerHandle y)
		{
			return !(x == y);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0000F597 File Offset: 0x0000D797
		public static explicit operator HTTPCookieContainerHandle(uint value)
		{
			return new HTTPCookieContainerHandle(value);
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0000F59F File Offset: 0x0000D79F
		public static explicit operator uint(HTTPCookieContainerHandle that)
		{
			return that.m_HTTPCookieContainerHandle;
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0000F5A7 File Offset: 0x0000D7A7
		public bool Equals(HTTPCookieContainerHandle other)
		{
			return this.m_HTTPCookieContainerHandle == other.m_HTTPCookieContainerHandle;
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0000F5B7 File Offset: 0x0000D7B7
		public int CompareTo(HTTPCookieContainerHandle other)
		{
			return this.m_HTTPCookieContainerHandle.CompareTo(other.m_HTTPCookieContainerHandle);
		}

		// Token: 0x04000ACB RID: 2763
		public static readonly HTTPCookieContainerHandle Invalid = new HTTPCookieContainerHandle(0U);

		// Token: 0x04000ACC RID: 2764
		public uint m_HTTPCookieContainerHandle;
	}
}
