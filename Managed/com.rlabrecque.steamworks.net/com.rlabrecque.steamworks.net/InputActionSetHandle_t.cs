using System;

namespace Steamworks
{
	// Token: 0x020001A3 RID: 419
	[Serializable]
	public struct InputActionSetHandle_t : IEquatable<InputActionSetHandle_t>, IComparable<InputActionSetHandle_t>
	{
		// Token: 0x060009F3 RID: 2547 RVA: 0x0000F673 File Offset: 0x0000D873
		public InputActionSetHandle_t(ulong value)
		{
			this.m_InputActionSetHandle = value;
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0000F67C File Offset: 0x0000D87C
		public override string ToString()
		{
			return this.m_InputActionSetHandle.ToString();
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0000F689 File Offset: 0x0000D889
		public override bool Equals(object other)
		{
			return other is InputActionSetHandle_t && this == (InputActionSetHandle_t)other;
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0000F6A6 File Offset: 0x0000D8A6
		public override int GetHashCode()
		{
			return this.m_InputActionSetHandle.GetHashCode();
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0000F6B3 File Offset: 0x0000D8B3
		public static bool operator ==(InputActionSetHandle_t x, InputActionSetHandle_t y)
		{
			return x.m_InputActionSetHandle == y.m_InputActionSetHandle;
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0000F6C3 File Offset: 0x0000D8C3
		public static bool operator !=(InputActionSetHandle_t x, InputActionSetHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0000F6CF File Offset: 0x0000D8CF
		public static explicit operator InputActionSetHandle_t(ulong value)
		{
			return new InputActionSetHandle_t(value);
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0000F6D7 File Offset: 0x0000D8D7
		public static explicit operator ulong(InputActionSetHandle_t that)
		{
			return that.m_InputActionSetHandle;
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0000F6DF File Offset: 0x0000D8DF
		public bool Equals(InputActionSetHandle_t other)
		{
			return this.m_InputActionSetHandle == other.m_InputActionSetHandle;
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0000F6EF File Offset: 0x0000D8EF
		public int CompareTo(InputActionSetHandle_t other)
		{
			return this.m_InputActionSetHandle.CompareTo(other.m_InputActionSetHandle);
		}

		// Token: 0x04000ACF RID: 2767
		public ulong m_InputActionSetHandle;
	}
}
