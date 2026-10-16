using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x02000342 RID: 834
	internal struct CV_PRIMITIVE_TYPE
	{
		// Token: 0x04000AE4 RID: 2788
		private const uint CV_MMASK = 1792U;

		// Token: 0x04000AE5 RID: 2789
		private const uint CV_TMASK = 240U;

		// Token: 0x04000AE6 RID: 2790
		private const uint CV_SMASK = 15U;

		// Token: 0x04000AE7 RID: 2791
		private const int CV_MSHIFT = 8;

		// Token: 0x04000AE8 RID: 2792
		private const int CV_TSHIFT = 4;

		// Token: 0x04000AE9 RID: 2793
		private const int CV_SSHIFT = 0;

		// Token: 0x04000AEA RID: 2794
		private const uint CV_FIRST_NONPRIM = 4096U;
	}
}
