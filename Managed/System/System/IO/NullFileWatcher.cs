using System;

namespace System.IO
{
	// Token: 0x02000527 RID: 1319
	internal class NullFileWatcher : IFileWatcher
	{
		// Token: 0x06002A81 RID: 10881 RVA: 0x00003917 File Offset: 0x00001B17
		public void StartDispatching(object handle)
		{
		}

		// Token: 0x06002A82 RID: 10882 RVA: 0x00003917 File Offset: 0x00001B17
		public void StopDispatching(object handle)
		{
		}

		// Token: 0x06002A83 RID: 10883 RVA: 0x00003917 File Offset: 0x00001B17
		public void Dispose(object handle)
		{
		}

		// Token: 0x06002A84 RID: 10884 RVA: 0x000929A0 File Offset: 0x00090BA0
		public static bool GetInstance(out IFileWatcher watcher)
		{
			if (NullFileWatcher.instance != null)
			{
				watcher = NullFileWatcher.instance;
				return true;
			}
			IFileWatcher fileWatcher;
			watcher = (fileWatcher = new NullFileWatcher());
			NullFileWatcher.instance = fileWatcher;
			return true;
		}

		// Token: 0x04001710 RID: 5904
		private static IFileWatcher instance;
	}
}
