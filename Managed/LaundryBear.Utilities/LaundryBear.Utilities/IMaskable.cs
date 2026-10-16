using System;

namespace LaundryBear
{
	// Token: 0x0200000E RID: 14
	public interface IMaskable
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600004E RID: 78
		int Layer { get; }

		// Token: 0x0600004F RID: 79
		bool IsMaskedBy(int mask);
	}
}
