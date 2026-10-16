using System;
using System.Collections;

namespace System.IO
{
	// Token: 0x02000512 RID: 1298
	internal class FAMData
	{
		// Token: 0x04001667 RID: 5735
		public FileSystemWatcher FSW;

		// Token: 0x04001668 RID: 5736
		public string Directory;

		// Token: 0x04001669 RID: 5737
		public string FileMask;

		// Token: 0x0400166A RID: 5738
		public bool IncludeSubdirs;

		// Token: 0x0400166B RID: 5739
		public bool Enabled;

		// Token: 0x0400166C RID: 5740
		public FAMRequest Request;

		// Token: 0x0400166D RID: 5741
		public Hashtable SubDirs;
	}
}
