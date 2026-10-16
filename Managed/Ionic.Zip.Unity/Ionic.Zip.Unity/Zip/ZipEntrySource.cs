using System;

namespace Ionic.Zip
{
	// Token: 0x02000031 RID: 49
	public enum ZipEntrySource
	{
		// Token: 0x040000F7 RID: 247
		None,
		// Token: 0x040000F8 RID: 248
		FileSystem,
		// Token: 0x040000F9 RID: 249
		Stream,
		// Token: 0x040000FA RID: 250
		ZipFile,
		// Token: 0x040000FB RID: 251
		WriteDelegate,
		// Token: 0x040000FC RID: 252
		JitStream,
		// Token: 0x040000FD RID: 253
		ZipOutputStream
	}
}
