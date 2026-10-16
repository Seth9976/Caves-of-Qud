using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000162 RID: 354
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct InputDigitalActionData_t
	{
		// Token: 0x04000A39 RID: 2617
		public byte bState;

		// Token: 0x04000A3A RID: 2618
		public byte bActive;
	}
}
