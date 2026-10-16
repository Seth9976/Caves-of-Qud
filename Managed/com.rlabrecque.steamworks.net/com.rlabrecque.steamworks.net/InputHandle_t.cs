using System;

namespace Steamworks
{
	// Token: 0x020001A6 RID: 422
	[Serializable]
	public struct InputHandle_t : IEquatable<InputHandle_t>, IComparable<InputHandle_t>
	{
		// Token: 0x06000A11 RID: 2577 RVA: 0x0000F820 File Offset: 0x0000DA20
		public InputHandle_t(ulong value)
		{
			this.m_InputHandle = value;
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0000F829 File Offset: 0x0000DA29
		public override string ToString()
		{
			return this.m_InputHandle.ToString();
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0000F836 File Offset: 0x0000DA36
		public override bool Equals(object other)
		{
			return other is InputHandle_t && this == (InputHandle_t)other;
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0000F853 File Offset: 0x0000DA53
		public override int GetHashCode()
		{
			return this.m_InputHandle.GetHashCode();
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0000F860 File Offset: 0x0000DA60
		public static bool operator ==(InputHandle_t x, InputHandle_t y)
		{
			return x.m_InputHandle == y.m_InputHandle;
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0000F870 File Offset: 0x0000DA70
		public static bool operator !=(InputHandle_t x, InputHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0000F87C File Offset: 0x0000DA7C
		public static explicit operator InputHandle_t(ulong value)
		{
			return new InputHandle_t(value);
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0000F884 File Offset: 0x0000DA84
		public static explicit operator ulong(InputHandle_t that)
		{
			return that.m_InputHandle;
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0000F88C File Offset: 0x0000DA8C
		public bool Equals(InputHandle_t other)
		{
			return this.m_InputHandle == other.m_InputHandle;
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0000F89C File Offset: 0x0000DA9C
		public int CompareTo(InputHandle_t other)
		{
			return this.m_InputHandle.CompareTo(other.m_InputHandle);
		}

		// Token: 0x04000AD2 RID: 2770
		public ulong m_InputHandle;
	}
}
