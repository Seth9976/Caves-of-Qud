using System;

namespace Steamworks
{
	// Token: 0x020001A5 RID: 421
	[Serializable]
	public struct InputDigitalActionHandle_t : IEquatable<InputDigitalActionHandle_t>, IComparable<InputDigitalActionHandle_t>
	{
		// Token: 0x06000A07 RID: 2567 RVA: 0x0000F791 File Offset: 0x0000D991
		public InputDigitalActionHandle_t(ulong value)
		{
			this.m_InputDigitalActionHandle = value;
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0000F79A File Offset: 0x0000D99A
		public override string ToString()
		{
			return this.m_InputDigitalActionHandle.ToString();
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0000F7A7 File Offset: 0x0000D9A7
		public override bool Equals(object other)
		{
			return other is InputDigitalActionHandle_t && this == (InputDigitalActionHandle_t)other;
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0000F7C4 File Offset: 0x0000D9C4
		public override int GetHashCode()
		{
			return this.m_InputDigitalActionHandle.GetHashCode();
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x0000F7D1 File Offset: 0x0000D9D1
		public static bool operator ==(InputDigitalActionHandle_t x, InputDigitalActionHandle_t y)
		{
			return x.m_InputDigitalActionHandle == y.m_InputDigitalActionHandle;
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0000F7E1 File Offset: 0x0000D9E1
		public static bool operator !=(InputDigitalActionHandle_t x, InputDigitalActionHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x0000F7ED File Offset: 0x0000D9ED
		public static explicit operator InputDigitalActionHandle_t(ulong value)
		{
			return new InputDigitalActionHandle_t(value);
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0000F7F5 File Offset: 0x0000D9F5
		public static explicit operator ulong(InputDigitalActionHandle_t that)
		{
			return that.m_InputDigitalActionHandle;
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0000F7FD File Offset: 0x0000D9FD
		public bool Equals(InputDigitalActionHandle_t other)
		{
			return this.m_InputDigitalActionHandle == other.m_InputDigitalActionHandle;
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0000F80D File Offset: 0x0000DA0D
		public int CompareTo(InputDigitalActionHandle_t other)
		{
			return this.m_InputDigitalActionHandle.CompareTo(other.m_InputDigitalActionHandle);
		}

		// Token: 0x04000AD1 RID: 2769
		public ulong m_InputDigitalActionHandle;
	}
}
