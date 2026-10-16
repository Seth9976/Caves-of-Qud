using System;

namespace Steamworks
{
	// Token: 0x020001A2 RID: 418
	[Serializable]
	public struct HTTPRequestHandle : IEquatable<HTTPRequestHandle>, IComparable<HTTPRequestHandle>
	{
		// Token: 0x060009E8 RID: 2536 RVA: 0x0000F5D7 File Offset: 0x0000D7D7
		public HTTPRequestHandle(uint value)
		{
			this.m_HTTPRequestHandle = value;
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0000F5E0 File Offset: 0x0000D7E0
		public override string ToString()
		{
			return this.m_HTTPRequestHandle.ToString();
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0000F5ED File Offset: 0x0000D7ED
		public override bool Equals(object other)
		{
			return other is HTTPRequestHandle && this == (HTTPRequestHandle)other;
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0000F60A File Offset: 0x0000D80A
		public override int GetHashCode()
		{
			return this.m_HTTPRequestHandle.GetHashCode();
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0000F617 File Offset: 0x0000D817
		public static bool operator ==(HTTPRequestHandle x, HTTPRequestHandle y)
		{
			return x.m_HTTPRequestHandle == y.m_HTTPRequestHandle;
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0000F627 File Offset: 0x0000D827
		public static bool operator !=(HTTPRequestHandle x, HTTPRequestHandle y)
		{
			return !(x == y);
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0000F633 File Offset: 0x0000D833
		public static explicit operator HTTPRequestHandle(uint value)
		{
			return new HTTPRequestHandle(value);
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0000F63B File Offset: 0x0000D83B
		public static explicit operator uint(HTTPRequestHandle that)
		{
			return that.m_HTTPRequestHandle;
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0000F643 File Offset: 0x0000D843
		public bool Equals(HTTPRequestHandle other)
		{
			return this.m_HTTPRequestHandle == other.m_HTTPRequestHandle;
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0000F653 File Offset: 0x0000D853
		public int CompareTo(HTTPRequestHandle other)
		{
			return this.m_HTTPRequestHandle.CompareTo(other.m_HTTPRequestHandle);
		}

		// Token: 0x04000ACD RID: 2765
		public static readonly HTTPRequestHandle Invalid = new HTTPRequestHandle(0U);

		// Token: 0x04000ACE RID: 2766
		public uint m_HTTPRequestHandle;
	}
}
