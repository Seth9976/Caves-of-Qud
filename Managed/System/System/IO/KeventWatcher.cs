using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x02000525 RID: 1317
	internal class KeventWatcher : IFileWatcher
	{
		// Token: 0x06002A7A RID: 10874 RVA: 0x0000219B File Offset: 0x0000039B
		private KeventWatcher()
		{
		}

		// Token: 0x06002A7B RID: 10875 RVA: 0x000928B8 File Offset: 0x00090AB8
		public static bool GetInstance(out IFileWatcher watcher)
		{
			if (KeventWatcher.failed)
			{
				watcher = null;
				return false;
			}
			if (KeventWatcher.instance != null)
			{
				watcher = KeventWatcher.instance;
				return true;
			}
			KeventWatcher.watches = Hashtable.Synchronized(new Hashtable());
			int num = KeventWatcher.kqueue();
			if (num == -1)
			{
				KeventWatcher.failed = true;
				watcher = null;
				return false;
			}
			KeventWatcher.close(num);
			KeventWatcher.instance = new KeventWatcher();
			watcher = KeventWatcher.instance;
			return true;
		}

		// Token: 0x06002A7C RID: 10876 RVA: 0x00092920 File Offset: 0x00090B20
		public void StartDispatching(object handle)
		{
			FileSystemWatcher fileSystemWatcher = handle as FileSystemWatcher;
			KqueueMonitor kqueueMonitor;
			if (KeventWatcher.watches.ContainsKey(fileSystemWatcher))
			{
				kqueueMonitor = (KqueueMonitor)KeventWatcher.watches[fileSystemWatcher];
			}
			else
			{
				kqueueMonitor = new KqueueMonitor(fileSystemWatcher);
				KeventWatcher.watches.Add(fileSystemWatcher, kqueueMonitor);
			}
			kqueueMonitor.Start();
		}

		// Token: 0x06002A7D RID: 10877 RVA: 0x00092970 File Offset: 0x00090B70
		public void StopDispatching(object handle)
		{
			FileSystemWatcher fileSystemWatcher = handle as FileSystemWatcher;
			KqueueMonitor kqueueMonitor = (KqueueMonitor)KeventWatcher.watches[fileSystemWatcher];
			if (kqueueMonitor == null)
			{
				return;
			}
			kqueueMonitor.Stop();
		}

		// Token: 0x06002A7E RID: 10878 RVA: 0x00003917 File Offset: 0x00001B17
		public void Dispose(object handle)
		{
		}

		// Token: 0x06002A7F RID: 10879
		[DllImport("libc")]
		private static extern int close(int fd);

		// Token: 0x06002A80 RID: 10880
		[DllImport("libc")]
		private static extern int kqueue();

		// Token: 0x04001704 RID: 5892
		private static bool failed;

		// Token: 0x04001705 RID: 5893
		private static KeventWatcher instance;

		// Token: 0x04001706 RID: 5894
		private static Hashtable watches;
	}
}
