using System;

namespace Steamworks
{
	// Token: 0x020001A4 RID: 420
	[Serializable]
	public struct InputAnalogActionHandle_t : IEquatable<InputAnalogActionHandle_t>, IComparable<InputAnalogActionHandle_t>
	{
		// Token: 0x060009FD RID: 2557 RVA: 0x0000F702 File Offset: 0x0000D902
		public InputAnalogActionHandle_t(ulong value)
		{
			this.m_InputAnalogActionHandle = value;
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0000F70B File Offset: 0x0000D90B
		public override string ToString()
		{
			return this.m_InputAnalogActionHandle.ToString();
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0000F718 File Offset: 0x0000D918
		public override bool Equals(object other)
		{
			return other is InputAnalogActionHandle_t && this == (InputAnalogActionHandle_t)other;
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0000F735 File Offset: 0x0000D935
		public override int GetHashCode()
		{
			return this.m_InputAnalogActionHandle.GetHashCode();
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0000F742 File Offset: 0x0000D942
		public static bool operator ==(InputAnalogActionHandle_t x, InputAnalogActionHandle_t y)
		{
			return x.m_InputAnalogActionHandle == y.m_InputAnalogActionHandle;
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0000F752 File Offset: 0x0000D952
		public static bool operator !=(InputAnalogActionHandle_t x, InputAnalogActionHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0000F75E File Offset: 0x0000D95E
		public static explicit operator InputAnalogActionHandle_t(ulong value)
		{
			return new InputAnalogActionHandle_t(value);
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0000F766 File Offset: 0x0000D966
		public static explicit operator ulong(InputAnalogActionHandle_t that)
		{
			return that.m_InputAnalogActionHandle;
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0000F76E File Offset: 0x0000D96E
		public bool Equals(InputAnalogActionHandle_t other)
		{
			return this.m_InputAnalogActionHandle == other.m_InputAnalogActionHandle;
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0000F77E File Offset: 0x0000D97E
		public int CompareTo(InputAnalogActionHandle_t other)
		{
			return this.m_InputAnalogActionHandle.CompareTo(other.m_InputAnalogActionHandle);
		}

		// Token: 0x04000AD0 RID: 2768
		public ulong m_InputAnalogActionHandle;
	}
}
