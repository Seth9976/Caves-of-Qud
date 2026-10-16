using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x02000045 RID: 69
	internal struct CV_PRIMITIVE_TYPE
	{
		// Token: 0x040000FD RID: 253
		private const uint CV_MMASK = 1792U;

		// Token: 0x040000FE RID: 254
		private const uint CV_TMASK = 240U;

		// Token: 0x040000FF RID: 255
		private const uint CV_SMASK = 15U;

		// Token: 0x04000100 RID: 256
		private const int CV_MSHIFT = 8;

		// Token: 0x04000101 RID: 257
		private const int CV_TSHIFT = 4;

		// Token: 0x04000102 RID: 258
		private const int CV_SSHIFT = 0;

		// Token: 0x04000103 RID: 259
		private const uint CV_FIRST_NONPRIM = 4096U;
	}
}
