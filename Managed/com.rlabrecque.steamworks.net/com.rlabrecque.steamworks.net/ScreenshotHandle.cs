using System;

namespace Steamworks
{
	// Token: 0x020001C3 RID: 451
	[Serializable]
	public struct ScreenshotHandle : IEquatable<ScreenshotHandle>, IComparable<ScreenshotHandle>
	{
		// Token: 0x06000B11 RID: 2833 RVA: 0x00010644 File Offset: 0x0000E844
		public ScreenshotHandle(uint value)
		{
			this.m_ScreenshotHandle = value;
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0001064D File Offset: 0x0000E84D
		public override string ToString()
		{
			return this.m_ScreenshotHandle.ToString();
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0001065A File Offset: 0x0000E85A
		public override bool Equals(object other)
		{
			return other is ScreenshotHandle && this == (ScreenshotHandle)other;
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x00010677 File Offset: 0x0000E877
		public override int GetHashCode()
		{
			return this.m_ScreenshotHandle.GetHashCode();
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x00010684 File Offset: 0x0000E884
		public static bool operator ==(ScreenshotHandle x, ScreenshotHandle y)
		{
			return x.m_ScreenshotHandle == y.m_ScreenshotHandle;
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x00010694 File Offset: 0x0000E894
		public static bool operator !=(ScreenshotHandle x, ScreenshotHandle y)
		{
			return !(x == y);
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x000106A0 File Offset: 0x0000E8A0
		public static explicit operator ScreenshotHandle(uint value)
		{
			return new ScreenshotHandle(value);
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x000106A8 File Offset: 0x0000E8A8
		public static explicit operator uint(ScreenshotHandle that)
		{
			return that.m_ScreenshotHandle;
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x000106B0 File Offset: 0x0000E8B0
		public bool Equals(ScreenshotHandle other)
		{
			return this.m_ScreenshotHandle == other.m_ScreenshotHandle;
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x000106C0 File Offset: 0x0000E8C0
		public int CompareTo(ScreenshotHandle other)
		{
			return this.m_ScreenshotHandle.CompareTo(other.m_ScreenshotHandle);
		}

		// Token: 0x04000B2F RID: 2863
		public static readonly ScreenshotHandle Invalid = new ScreenshotHandle(0U);

		// Token: 0x04000B30 RID: 2864
		public uint m_ScreenshotHandle;
	}
}
