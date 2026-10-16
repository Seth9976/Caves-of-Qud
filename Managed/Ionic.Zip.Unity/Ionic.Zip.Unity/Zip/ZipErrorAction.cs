using System;

namespace Ionic.Zip
{
	// Token: 0x02000032 RID: 50
	public enum ZipErrorAction
	{
		// Token: 0x040000FF RID: 255
		Throw,
		// Token: 0x04000100 RID: 256
		Skip,
		// Token: 0x04000101 RID: 257
		Retry,
		// Token: 0x04000102 RID: 258
		InvokeErrorEvent
	}
}
