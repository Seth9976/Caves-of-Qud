using System;

namespace UnityEngine
{
	// Token: 0x020002EC RID: 748
	public enum TouchScreenKeyboardType
	{
		// Token: 0x04000A65 RID: 2661
		Default,
		// Token: 0x04000A66 RID: 2662
		ASCIICapable,
		// Token: 0x04000A67 RID: 2663
		NumbersAndPunctuation,
		// Token: 0x04000A68 RID: 2664
		URL,
		// Token: 0x04000A69 RID: 2665
		NumberPad,
		// Token: 0x04000A6A RID: 2666
		PhonePad,
		// Token: 0x04000A6B RID: 2667
		NamePhonePad,
		// Token: 0x04000A6C RID: 2668
		EmailAddress,
		// Token: 0x04000A6D RID: 2669
		[Obsolete("Wii U is no longer supported as of Unity 2018.1.")]
		NintendoNetworkAccount,
		// Token: 0x04000A6E RID: 2670
		Social,
		// Token: 0x04000A6F RID: 2671
		Search,
		// Token: 0x04000A70 RID: 2672
		DecimalPad,
		// Token: 0x04000A71 RID: 2673
		OneTimeCode
	}
}
