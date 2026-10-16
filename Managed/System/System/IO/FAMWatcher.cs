using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.IO
{
	// Token: 0x02000513 RID: 1299
	internal class FAMWatcher : IFileWatcher
	{
		// Token: 0x060029F9 RID: 10745 RVA: 0x0000219B File Offset: 0x0000039B
		private FAMWatcher()
		{
		}

		// Token: 0x060029FA RID: 10746 RVA: 0x0009027C File Offset: 0x0008E47C
		public static bool GetInstance(out IFileWatcher watcher, bool gamin)
		{
			if (FAMWatcher.failed)
			{
				watcher = null;
				return false;
			}
			if (FAMWatcher.instance != null)
			{
				watcher = FAMWatcher.instance;
				return true;
			}
			FAMWatcher.use_gamin = gamin;
			FAMWatcher.watches = Hashtable.Synchronized(new Hashtable());
			FAMWatcher.requests = Hashtable.Synchronized(new Hashtable());
			if (FAMWatcher.FAMOpen(out FAMWatcher.conn) == -1)
			{
				FAMWatcher.failed = true;
				watcher = null;
				return false;
			}
			FAMWatcher.instance = new FAMWatcher();
			watcher = FAMWatcher.instance;
			return true;
		}

		// Token: 0x060029FB RID: 10747 RVA: 0x000902F4 File Offset: 0x0008E4F4
		public void StartDispatching(object handle)
		{
			FileSystemWatcher fileSystemWatcher = handle as FileSystemWatcher;
			FAMWatcher famwatcher = this;
			FAMData famdata;
			lock (famwatcher)
			{
				if (FAMWatcher.thread == null)
				{
					FAMWatcher.thread = new Thread(new ThreadStart(this.Monitor));
					FAMWatcher.thread.IsBackground = true;
					FAMWatcher.thread.Start();
				}
				famdata = (FAMData)FAMWatcher.watches[fileSystemWatcher];
			}
			if (famdata == null)
			{
				famdata = new FAMData();
				famdata.FSW = fileSystemWatcher;
				famdata.Directory = fileSystemWatcher.FullPath;
				famdata.FileMask = fileSystemWatcher.MangledFilter;
				famdata.IncludeSubdirs = fileSystemWatcher.IncludeSubdirectories;
				if (famdata.IncludeSubdirs)
				{
					famdata.SubDirs = new Hashtable();
				}
				famdata.Enabled = true;
				FAMWatcher.StartMonitoringDirectory(famdata, false);
				famwatcher = this;
				lock (famwatcher)
				{
					FAMWatcher.watches[fileSystemWatcher] = famdata;
					FAMWatcher.requests[famdata.Request.ReqNum] = famdata;
					FAMWatcher.stop = false;
				}
			}
		}

		// Token: 0x060029FC RID: 10748 RVA: 0x0009041C File Offset: 0x0008E61C
		private static void StartMonitoringDirectory(FAMData data, bool justcreated)
		{
			FAMRequest famrequest;
			if (FAMWatcher.FAMMonitorDirectory(ref FAMWatcher.conn, data.Directory, out famrequest, IntPtr.Zero) == -1)
			{
				throw new Win32Exception();
			}
			FileSystemWatcher fsw = data.FSW;
			data.Request = famrequest;
			if (data.IncludeSubdirs)
			{
				foreach (string text in Directory.GetDirectories(data.Directory))
				{
					FAMData famdata = new FAMData();
					famdata.FSW = data.FSW;
					famdata.Directory = text;
					famdata.FileMask = data.FSW.MangledFilter;
					famdata.IncludeSubdirs = true;
					famdata.SubDirs = new Hashtable();
					famdata.Enabled = true;
					if (justcreated)
					{
						FileSystemWatcher fileSystemWatcher = fsw;
						lock (fileSystemWatcher)
						{
							RenamedEventArgs e = null;
							fsw.DispatchEvents(FileAction.Added, text, ref e);
							if (fsw.Waiting)
							{
								fsw.Waiting = false;
								global::System.Threading.Monitor.PulseAll(fsw);
							}
						}
					}
					FAMWatcher.StartMonitoringDirectory(famdata, justcreated);
					data.SubDirs[text] = famdata;
					FAMWatcher.requests[famdata.Request.ReqNum] = famdata;
				}
			}
			if (justcreated)
			{
				foreach (string text2 in Directory.GetFiles(data.Directory))
				{
					FileSystemWatcher fileSystemWatcher = fsw;
					lock (fileSystemWatcher)
					{
						RenamedEventArgs e2 = null;
						fsw.DispatchEvents(FileAction.Added, text2, ref e2);
						fsw.DispatchEvents(FileAction.Modified, text2, ref e2);
						if (fsw.Waiting)
						{
							fsw.Waiting = false;
							global::System.Threading.Monitor.PulseAll(fsw);
						}
					}
				}
			}
		}

		// Token: 0x060029FD RID: 10749 RVA: 0x000905D4 File Offset: 0x0008E7D4
		public void StopDispatching(object handle)
		{
			FileSystemWatcher fileSystemWatcher = handle as FileSystemWatcher;
			lock (this)
			{
				FAMData famdata = (FAMData)FAMWatcher.watches[fileSystemWatcher];
				if (famdata != null)
				{
					FAMWatcher.StopMonitoringDirectory(famdata);
					FAMWatcher.watches.Remove(fileSystemWatcher);
					FAMWatcher.requests.Remove(famdata.Request.ReqNum);
					if (FAMWatcher.watches.Count == 0)
					{
						FAMWatcher.stop = true;
					}
					if (famdata.IncludeSubdirs)
					{
						foreach (object obj in famdata.SubDirs.Values)
						{
							FAMData famdata2 = (FAMData)obj;
							FAMWatcher.StopMonitoringDirectory(famdata2);
							FAMWatcher.requests.Remove(famdata2.Request.ReqNum);
						}
					}
				}
			}
		}

		// Token: 0x060029FE RID: 10750 RVA: 0x000906E0 File Offset: 0x0008E8E0
		private static void StopMonitoringDirectory(FAMData data)
		{
			if (FAMWatcher.FAMCancelMonitor(ref FAMWatcher.conn, ref data.Request) == -1)
			{
				throw new Win32Exception();
			}
		}

		// Token: 0x060029FF RID: 10751 RVA: 0x000906FC File Offset: 0x0008E8FC
		private void Monitor()
		{
			FAMWatcher famwatcher;
			while (!FAMWatcher.stop)
			{
				famwatcher = this;
				int num;
				lock (famwatcher)
				{
					num = FAMWatcher.FAMPending(ref FAMWatcher.conn);
				}
				if (num > 0)
				{
					this.ProcessEvents();
				}
				else
				{
					Thread.Sleep(500);
				}
			}
			famwatcher = this;
			lock (famwatcher)
			{
				FAMWatcher.thread = null;
				FAMWatcher.stop = false;
			}
		}

		// Token: 0x06002A00 RID: 10752 RVA: 0x0009078C File Offset: 0x0008E98C
		private void ProcessEvents()
		{
			ArrayList arrayList = null;
			lock (this)
			{
				string text;
				int num;
				int num2;
				while (FAMWatcher.InternalFAMNextEvent(ref FAMWatcher.conn, out text, out num, out num2) == 1)
				{
					bool flag2;
					switch (num)
					{
					case 1:
					case 2:
					case 5:
						flag2 = FAMWatcher.requests.ContainsKey(num2);
						break;
					case 3:
					case 4:
					case 6:
					case 7:
					case 8:
					case 9:
						goto IL_0070;
					default:
						goto IL_0070;
					}
					IL_0073:
					if (flag2)
					{
						FAMData famdata = (FAMData)FAMWatcher.requests[num2];
						if (famdata.Enabled)
						{
							FileSystemWatcher fsw = famdata.FSW;
							NotifyFilters notifyFilter = fsw.NotifyFilter;
							RenamedEventArgs e = null;
							FileAction fileAction = (FileAction)0;
							if (num == 1 && (notifyFilter & (NotifyFilters.Attributes | NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.Size)) != (NotifyFilters)0)
							{
								fileAction = FileAction.Modified;
							}
							else if (num == 2)
							{
								fileAction = FileAction.Removed;
							}
							else if (num == 5)
							{
								fileAction = FileAction.Added;
							}
							if (fileAction != (FileAction)0)
							{
								if (fsw.IncludeSubdirectories)
								{
									string fullPath = fsw.FullPath;
									string text2 = famdata.Directory;
									if (text2 != fullPath)
									{
										int length = fullPath.Length;
										int num3 = 1;
										if (length > 1 && fullPath[length - 1] == Path.DirectorySeparatorChar)
										{
											num3 = 0;
										}
										string text3 = text2.Substring(fullPath.Length + num3);
										text2 = Path.Combine(text2, text);
										text = Path.Combine(text3, text);
									}
									else
									{
										text2 = Path.Combine(fullPath, text);
									}
									if (fileAction == FileAction.Added && Directory.Exists(text2))
									{
										if (arrayList == null)
										{
											arrayList = new ArrayList(4);
										}
										arrayList.Add(new FAMData
										{
											FSW = fsw,
											Directory = text2,
											FileMask = fsw.MangledFilter,
											IncludeSubdirs = true,
											SubDirs = new Hashtable(),
											Enabled = true
										});
										arrayList.Add(famdata);
									}
								}
								if (!(text != famdata.Directory) || fsw.Pattern.IsMatch(text))
								{
									FileSystemWatcher fileSystemWatcher = fsw;
									lock (fileSystemWatcher)
									{
										fsw.DispatchEvents(fileAction, text, ref e);
										if (fsw.Waiting)
										{
											fsw.Waiting = false;
											global::System.Threading.Monitor.PulseAll(fsw);
										}
									}
								}
							}
						}
					}
					if (FAMWatcher.FAMPending(ref FAMWatcher.conn) <= 0)
					{
						goto IL_024A;
					}
					continue;
					IL_0070:
					flag2 = false;
					goto IL_0073;
				}
				return;
			}
			IL_024A:
			if (arrayList != null)
			{
				int count = arrayList.Count;
				for (int i = 0; i < count; i += 2)
				{
					FAMData famdata2 = (FAMData)arrayList[i];
					FAMData famdata3 = (FAMData)arrayList[i + 1];
					FAMWatcher.StartMonitoringDirectory(famdata2, true);
					FAMWatcher.requests[famdata2.Request.ReqNum] = famdata2;
					FAMData famdata4 = famdata3;
					lock (famdata4)
					{
						famdata3.SubDirs[famdata2.Directory] = famdata2;
					}
				}
				arrayList.Clear();
			}
		}

		// Token: 0x06002A01 RID: 10753 RVA: 0x00090AD0 File Offset: 0x0008ECD0
		~FAMWatcher()
		{
			FAMWatcher.FAMClose(ref FAMWatcher.conn);
		}

		// Token: 0x06002A02 RID: 10754 RVA: 0x00090B04 File Offset: 0x0008ED04
		private static int FAMOpen(out FAMConnection fc)
		{
			if (FAMWatcher.use_gamin)
			{
				return FAMWatcher.gamin_Open(out fc);
			}
			return FAMWatcher.fam_Open(out fc);
		}

		// Token: 0x06002A03 RID: 10755 RVA: 0x00090B1A File Offset: 0x0008ED1A
		private static int FAMClose(ref FAMConnection fc)
		{
			if (FAMWatcher.use_gamin)
			{
				return FAMWatcher.gamin_Close(ref fc);
			}
			return FAMWatcher.fam_Close(ref fc);
		}

		// Token: 0x06002A04 RID: 10756 RVA: 0x00090B30 File Offset: 0x0008ED30
		private static int FAMMonitorDirectory(ref FAMConnection fc, string filename, out FAMRequest fr, IntPtr user_data)
		{
			if (FAMWatcher.use_gamin)
			{
				return FAMWatcher.gamin_MonitorDirectory(ref fc, filename, out fr, user_data);
			}
			return FAMWatcher.fam_MonitorDirectory(ref fc, filename, out fr, user_data);
		}

		// Token: 0x06002A05 RID: 10757 RVA: 0x00090B4C File Offset: 0x0008ED4C
		private static int FAMCancelMonitor(ref FAMConnection fc, ref FAMRequest fr)
		{
			if (FAMWatcher.use_gamin)
			{
				return FAMWatcher.gamin_CancelMonitor(ref fc, ref fr);
			}
			return FAMWatcher.fam_CancelMonitor(ref fc, ref fr);
		}

		// Token: 0x06002A06 RID: 10758 RVA: 0x00090B64 File Offset: 0x0008ED64
		private static int FAMPending(ref FAMConnection fc)
		{
			if (FAMWatcher.use_gamin)
			{
				return FAMWatcher.gamin_Pending(ref fc);
			}
			return FAMWatcher.fam_Pending(ref fc);
		}

		// Token: 0x06002A07 RID: 10759 RVA: 0x00003917 File Offset: 0x00001B17
		public void Dispose(object handle)
		{
		}

		// Token: 0x06002A08 RID: 10760
		[DllImport("libfam.so.0", EntryPoint = "FAMOpen")]
		private static extern int fam_Open(out FAMConnection fc);

		// Token: 0x06002A09 RID: 10761
		[DllImport("libfam.so.0", EntryPoint = "FAMClose")]
		private static extern int fam_Close(ref FAMConnection fc);

		// Token: 0x06002A0A RID: 10762
		[DllImport("libfam.so.0", EntryPoint = "FAMMonitorDirectory")]
		private static extern int fam_MonitorDirectory(ref FAMConnection fc, string filename, out FAMRequest fr, IntPtr user_data);

		// Token: 0x06002A0B RID: 10763
		[DllImport("libfam.so.0", EntryPoint = "FAMCancelMonitor")]
		private static extern int fam_CancelMonitor(ref FAMConnection fc, ref FAMRequest fr);

		// Token: 0x06002A0C RID: 10764
		[DllImport("libfam.so.0", EntryPoint = "FAMPending")]
		private static extern int fam_Pending(ref FAMConnection fc);

		// Token: 0x06002A0D RID: 10765
		[DllImport("libgamin-1.so.0", EntryPoint = "FAMOpen")]
		private static extern int gamin_Open(out FAMConnection fc);

		// Token: 0x06002A0E RID: 10766
		[DllImport("libgamin-1.so.0", EntryPoint = "FAMClose")]
		private static extern int gamin_Close(ref FAMConnection fc);

		// Token: 0x06002A0F RID: 10767
		[DllImport("libgamin-1.so.0", EntryPoint = "FAMMonitorDirectory")]
		private static extern int gamin_MonitorDirectory(ref FAMConnection fc, string filename, out FAMRequest fr, IntPtr user_data);

		// Token: 0x06002A10 RID: 10768
		[DllImport("libgamin-1.so.0", EntryPoint = "FAMCancelMonitor")]
		private static extern int gamin_CancelMonitor(ref FAMConnection fc, ref FAMRequest fr);

		// Token: 0x06002A11 RID: 10769
		[DllImport("libgamin-1.so.0", EntryPoint = "FAMPending")]
		private static extern int gamin_Pending(ref FAMConnection fc);

		// Token: 0x06002A12 RID: 10770
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int InternalFAMNextEvent(ref FAMConnection fc, out string filename, out int code, out int reqnum);

		// Token: 0x0400166E RID: 5742
		private static bool failed;

		// Token: 0x0400166F RID: 5743
		private static FAMWatcher instance;

		// Token: 0x04001670 RID: 5744
		private static Hashtable watches;

		// Token: 0x04001671 RID: 5745
		private static Hashtable requests;

		// Token: 0x04001672 RID: 5746
		private static FAMConnection conn;

		// Token: 0x04001673 RID: 5747
		private static Thread thread;

		// Token: 0x04001674 RID: 5748
		private static bool stop;

		// Token: 0x04001675 RID: 5749
		private static bool use_gamin;

		// Token: 0x04001676 RID: 5750
		private const NotifyFilters changed = NotifyFilters.Attributes | NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.Size;
	}
}
