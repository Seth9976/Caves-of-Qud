using System;

namespace NAudio.Wave.Compression
{
	// Token: 0x0200015B RID: 347
	[Flags]
	public enum AcmDriverDetailsSupportFlags
	{
		// Token: 0x040007AC RID: 1964
		Codec = 1,
		// Token: 0x040007AD RID: 1965
		Converter = 2,
		// Token: 0x040007AE RID: 1966
		Filter = 4,
		// Token: 0x040007AF RID: 1967
		Hardware = 8,
		// Token: 0x040007B0 RID: 1968
		Async = 16,
		// Token: 0x040007B1 RID: 1969
		Local = 1073741824,
		// Token: 0x040007B2 RID: 1970
		Disabled = -2147483648
	}
}
