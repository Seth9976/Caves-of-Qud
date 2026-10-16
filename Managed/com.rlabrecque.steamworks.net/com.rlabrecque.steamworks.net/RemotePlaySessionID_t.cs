using System;

namespace Steamworks
{
	// Token: 0x020001BE RID: 446
	[Serializable]
	public struct RemotePlaySessionID_t : IEquatable<RemotePlaySessionID_t>, IComparable<RemotePlaySessionID_t>
	{
		// Token: 0x06000ADB RID: 2779 RVA: 0x00010341 File Offset: 0x0000E541
		public RemotePlaySessionID_t(uint value)
		{
			this.m_RemotePlaySessionID = value;
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0001034A File Offset: 0x0000E54A
		public override string ToString()
		{
			return this.m_RemotePlaySessionID.ToString();
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00010357 File Offset: 0x0000E557
		public override bool Equals(object other)
		{
			return other is RemotePlaySessionID_t && this == (RemotePlaySessionID_t)other;
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00010374 File Offset: 0x0000E574
		public override int GetHashCode()
		{
			return this.m_RemotePlaySessionID.GetHashCode();
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x00010381 File Offset: 0x0000E581
		public static bool operator ==(RemotePlaySessionID_t x, RemotePlaySessionID_t y)
		{
			return x.m_RemotePlaySessionID == y.m_RemotePlaySessionID;
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x00010391 File Offset: 0x0000E591
		public static bool operator !=(RemotePlaySessionID_t x, RemotePlaySessionID_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0001039D File Offset: 0x0000E59D
		public static explicit operator RemotePlaySessionID_t(uint value)
		{
			return new RemotePlaySessionID_t(value);
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x000103A5 File Offset: 0x0000E5A5
		public static explicit operator uint(RemotePlaySessionID_t that)
		{
			return that.m_RemotePlaySessionID;
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x000103AD File Offset: 0x0000E5AD
		public bool Equals(RemotePlaySessionID_t other)
		{
			return this.m_RemotePlaySessionID == other.m_RemotePlaySessionID;
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x000103BD File Offset: 0x0000E5BD
		public int CompareTo(RemotePlaySessionID_t other)
		{
			return this.m_RemotePlaySessionID.CompareTo(other.m_RemotePlaySessionID);
		}

		// Token: 0x04000B26 RID: 2854
		public uint m_RemotePlaySessionID;
	}
}
