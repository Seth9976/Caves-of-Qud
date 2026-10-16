using System;
using System.Collections.Generic;

namespace System.IO
{
	// Token: 0x0200050A RID: 1290
	internal class DefaultWatcherData
	{
		// Token: 0x04001647 RID: 5703
		public FileSystemWatcher FSW;

		// Token: 0x04001648 RID: 5704
		public string Directory;

		// Token: 0x04001649 RID: 5705
		public string FileMask;

		// Token: 0x0400164A RID: 5706
		public bool IncludeSubdirs;

		// Token: 0x0400164B RID: 5707
		public bool Enabled;

		// Token: 0x0400164C RID: 5708
		public bool NoWildcards;

		// Token: 0x0400164D RID: 5709
		public DateTime DisabledTime;

		// Token: 0x0400164E RID: 5710
		public object FilesLock = new object();

		// Token: 0x0400164F RID: 5711
		public Dictionary<string, FileData> Files;
	}
}
