using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace System.IO
{
	// Token: 0x0200050C RID: 1292
	internal class DefaultWatcher : IFileWatcher
	{
		// Token: 0x060029E6 RID: 10726 RVA: 0x0000219B File Offset: 0x0000039B
		private DefaultWatcher()
		{
		}

		// Token: 0x060029E7 RID: 10727 RVA: 0x0008F9FB File Offset: 0x0008DBFB
		public static bool GetInstance(out IFileWatcher watcher)
		{
			if (DefaultWatcher.instance != null)
			{
				watcher = DefaultWatcher.instance;
				return true;
			}
			DefaultWatcher.instance = new DefaultWatcher();
			watcher = DefaultWatcher.instance;
			return true;
		}

		// Token: 0x060029E8 RID: 10728 RVA: 0x0008FA20 File Offset: 0x0008DC20
		public void StartDispatching(object handle)
		{
			FileSystemWatcher fileSystemWatcher = handle as FileSystemWatcher;
			lock (this)
			{
				if (DefaultWatcher.watches == null)
				{
					DefaultWatcher.watches = new Hashtable();
				}
				if (DefaultWatcher.thread == null)
				{
					DefaultWatcher.thread = new Thread(new ThreadStart(this.Monitor));
					DefaultWatcher.thread.IsBackground = true;
					DefaultWatcher.thread.Start();
				}
			}
			Hashtable hashtable = DefaultWatcher.watches;
			lock (hashtable)
			{
				DefaultWatcherData defaultWatcherData = (DefaultWatcherData)DefaultWatcher.watches[fileSystemWatcher];
				if (defaultWatcherData == null)
				{
					defaultWatcherData = new DefaultWatcherData();
					defaultWatcherData.Files = new Dictionary<string, FileData>();
					DefaultWatcher.watches[fileSystemWatcher] = defaultWatcherData;
				}
				defaultWatcherData.FSW = fileSystemWatcher;
				defaultWatcherData.Directory = fileSystemWatcher.FullPath;
				defaultWatcherData.NoWildcards = !fileSystemWatcher.Pattern.HasWildcard;
				if (defaultWatcherData.NoWildcards)
				{
					defaultWatcherData.FileMask = Path.Combine(defaultWatcherData.Directory, fileSystemWatcher.MangledFilter);
				}
				else
				{
					defaultWatcherData.FileMask = fileSystemWatcher.MangledFilter;
				}
				defaultWatcherData.IncludeSubdirs = fileSystemWatcher.IncludeSubdirectories;
				defaultWatcherData.Enabled = true;
				defaultWatcherData.DisabledTime = DateTime.MaxValue;
				this.UpdateDataAndDispatch(defaultWatcherData, false);
			}
		}

		// Token: 0x060029E9 RID: 10729 RVA: 0x0008FB78 File Offset: 0x0008DD78
		public void StopDispatching(object handle)
		{
			FileSystemWatcher fileSystemWatcher = handle as FileSystemWatcher;
			lock (this)
			{
				if (DefaultWatcher.watches == null)
				{
					return;
				}
			}
			Hashtable hashtable = DefaultWatcher.watches;
			lock (hashtable)
			{
				DefaultWatcherData defaultWatcherData = (DefaultWatcherData)DefaultWatcher.watches[fileSystemWatcher];
				if (defaultWatcherData != null)
				{
					object filesLock = defaultWatcherData.FilesLock;
					lock (filesLock)
					{
						defaultWatcherData.Enabled = false;
						defaultWatcherData.DisabledTime = DateTime.UtcNow;
					}
				}
			}
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x00003917 File Offset: 0x00001B17
		public void Dispose(object handle)
		{
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x0008FC3C File Offset: 0x0008DE3C
		private void Monitor()
		{
			int num = 0;
			for (;;)
			{
				Thread.Sleep(750);
				Hashtable hashtable = DefaultWatcher.watches;
				Hashtable hashtable2;
				lock (hashtable)
				{
					if (DefaultWatcher.watches.Count == 0)
					{
						if (++num == 20)
						{
							break;
						}
						continue;
					}
					else
					{
						hashtable2 = (Hashtable)DefaultWatcher.watches.Clone();
					}
				}
				if (hashtable2.Count != 0)
				{
					num = 0;
					using (IEnumerator enumerator = hashtable2.Values.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							DefaultWatcherData defaultWatcherData = (DefaultWatcherData)obj;
							if (this.UpdateDataAndDispatch(defaultWatcherData, true))
							{
								hashtable = DefaultWatcher.watches;
								lock (hashtable)
								{
									DefaultWatcher.watches.Remove(defaultWatcherData.FSW);
								}
							}
						}
						continue;
					}
					break;
				}
			}
			lock (this)
			{
				DefaultWatcher.thread = null;
			}
		}

		// Token: 0x060029EC RID: 10732 RVA: 0x0008FD74 File Offset: 0x0008DF74
		private bool UpdateDataAndDispatch(DefaultWatcherData data, bool dispatch)
		{
			if (!data.Enabled)
			{
				return data.DisabledTime != DateTime.MaxValue && (DateTime.UtcNow - data.DisabledTime).TotalSeconds > 5.0;
			}
			this.DoFiles(data, data.Directory, dispatch);
			return false;
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x0008FDD0 File Offset: 0x0008DFD0
		private static void DispatchEvents(FileSystemWatcher fsw, FileAction action, string filename)
		{
			RenamedEventArgs e = null;
			lock (fsw)
			{
				fsw.DispatchEvents(action, filename, ref e);
				if (fsw.Waiting)
				{
					fsw.Waiting = false;
					global::System.Threading.Monitor.PulseAll(fsw);
				}
			}
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x0008FE28 File Offset: 0x0008E028
		private void DoFiles(DefaultWatcherData data, string directory, bool dispatch)
		{
			bool flag = Directory.Exists(directory);
			if (flag && data.IncludeSubdirs)
			{
				foreach (string text in Directory.GetDirectories(directory))
				{
					this.DoFiles(data, text, dispatch);
				}
			}
			string[] array;
			if (!flag)
			{
				array = DefaultWatcher.NoStringsArray;
			}
			else if (!data.NoWildcards)
			{
				array = Directory.GetFileSystemEntries(directory, data.FileMask);
			}
			else if (File.Exists(data.FileMask) || Directory.Exists(data.FileMask))
			{
				array = new string[] { data.FileMask };
			}
			else
			{
				array = DefaultWatcher.NoStringsArray;
			}
			object filesLock = data.FilesLock;
			lock (filesLock)
			{
				if (data.Enabled)
				{
					this.IterateAndModifyFilesData(data, directory, dispatch, array);
				}
			}
		}

		// Token: 0x060029EF RID: 10735 RVA: 0x0008FF08 File Offset: 0x0008E108
		private void IterateAndModifyFilesData(DefaultWatcherData data, string directory, bool dispatch, string[] files)
		{
			foreach (KeyValuePair<string, FileData> keyValuePair in data.Files)
			{
				FileData value = keyValuePair.Value;
				if (value.Directory == directory)
				{
					value.NotExists = true;
				}
			}
			foreach (string text in files)
			{
				FileData fileData;
				if (!data.Files.TryGetValue(text, out fileData))
				{
					try
					{
						data.Files.Add(text, DefaultWatcher.CreateFileData(directory, text));
					}
					catch
					{
						data.Files.Remove(text);
						goto IL_00CA;
					}
					if (dispatch)
					{
						DefaultWatcher.DispatchEvents(data.FSW, FileAction.Added, Path.GetRelativePath(data.Directory, text));
					}
				}
				else if (fileData.Directory == directory)
				{
					fileData.NotExists = false;
				}
				IL_00CA:;
			}
			if (!dispatch)
			{
				return;
			}
			List<string> list = null;
			foreach (KeyValuePair<string, FileData> keyValuePair2 in data.Files)
			{
				string key = keyValuePair2.Key;
				if (keyValuePair2.Value.NotExists)
				{
					if (list == null)
					{
						list = new List<string>();
					}
					list.Add(key);
					DefaultWatcher.DispatchEvents(data.FSW, FileAction.Removed, Path.GetRelativePath(data.Directory, key));
				}
			}
			if (list != null)
			{
				foreach (string text2 in list)
				{
					data.Files.Remove(text2);
				}
				list = null;
			}
			foreach (KeyValuePair<string, FileData> keyValuePair3 in data.Files)
			{
				string key2 = keyValuePair3.Key;
				FileData value2 = keyValuePair3.Value;
				DateTime creationTime;
				DateTime lastWriteTime;
				try
				{
					creationTime = File.GetCreationTime(key2);
					lastWriteTime = File.GetLastWriteTime(key2);
				}
				catch
				{
					if (list == null)
					{
						list = new List<string>();
					}
					list.Add(key2);
					DefaultWatcher.DispatchEvents(data.FSW, FileAction.Removed, Path.GetRelativePath(data.Directory, key2));
					continue;
				}
				if (creationTime != value2.CreationTime || lastWriteTime != value2.LastWriteTime)
				{
					value2.CreationTime = creationTime;
					value2.LastWriteTime = lastWriteTime;
					DefaultWatcher.DispatchEvents(data.FSW, FileAction.Modified, Path.GetRelativePath(data.Directory, key2));
				}
			}
			if (list != null)
			{
				foreach (string text3 in list)
				{
					data.Files.Remove(text3);
				}
			}
		}

		// Token: 0x060029F0 RID: 10736 RVA: 0x00090210 File Offset: 0x0008E410
		private static FileData CreateFileData(string directory, string filename)
		{
			FileData fileData = new FileData();
			string text = Path.Combine(directory, filename);
			fileData.Directory = directory;
			fileData.Attributes = File.GetAttributes(text);
			fileData.CreationTime = File.GetCreationTime(text);
			fileData.LastWriteTime = File.GetLastWriteTime(text);
			return fileData;
		}

		// Token: 0x04001655 RID: 5717
		private static DefaultWatcher instance;

		// Token: 0x04001656 RID: 5718
		private static Thread thread;

		// Token: 0x04001657 RID: 5719
		private static Hashtable watches;

		// Token: 0x04001658 RID: 5720
		private static string[] NoStringsArray = new string[0];
	}
}
