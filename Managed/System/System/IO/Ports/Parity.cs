using System;

namespace System.IO.Ports
{
	/// <summary>Specifies the parity bit for a <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
	// Token: 0x0200052F RID: 1327
	public enum Parity
	{
		/// <summary>No parity check occurs.</summary>
		// Token: 0x0400172C RID: 5932
		None,
		/// <summary>Sets the parity bit so that the count of bits set is an odd number.</summary>
		// Token: 0x0400172D RID: 5933
		Odd,
		/// <summary>Sets the parity bit so that the count of bits set is an even number.</summary>
		// Token: 0x0400172E RID: 5934
		Even,
		/// <summary>Leaves the parity bit set to 1.</summary>
		// Token: 0x0400172F RID: 5935
		Mark,
		/// <summary>Leaves the parity bit set to 0.</summary>
		// Token: 0x04001730 RID: 5936
		Space
	}
}
