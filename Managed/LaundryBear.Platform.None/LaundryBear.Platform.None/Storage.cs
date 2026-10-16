using System;
using System.IO;
using System.Security;
using System.Threading;
using Unity.Properties;
using UnityEngine;

namespace LaundryBear.PlatformServices.None
{
	// Token: 0x02000006 RID: 6
	internal class Storage : IStorage, IDisposable, IEquatable<IStorage>, ICanDelayShutdown
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002320 File Offset: 0x00000520
		public bool IsMounted
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002323 File Offset: 0x00000523
		// (set) Token: 0x06000028 RID: 40 RVA: 0x0000232B File Offset: 0x0000052B
		public IUser AssociatedUser { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002334 File Offset: 0x00000534
		public string RootPath
		{
			get
			{
				return this.m_rootPath;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000233C File Offset: 0x0000053C
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002344 File Offset: 0x00000544
		public bool IsUserHandlingShutdownDelay { get; set; }

		// Token: 0x0600002C RID: 44 RVA: 0x0000234D File Offset: 0x0000054D
		public void BeginDelayShutdown()
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000234F File Offset: 0x0000054F
		public void EndShutdownDelay()
		{
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002351 File Offset: 0x00000551
		private bool IsOnMain()
		{
			return Thread.CurrentThread.ManagedThreadId == this.mainThreadID;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002365 File Offset: 0x00000565
		public bool IsPathRootedToMount(string path)
		{
			return Path.IsPathRooted(path);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000236D File Offset: 0x0000056D
		public string GetPathMount(string path)
		{
			return Path.GetPathRoot(path);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002378 File Offset: 0x00000578
		internal Storage(string name)
		{
			this.mainThreadID = Thread.CurrentThread.ManagedThreadId;
			if (string.IsNullOrEmpty(name))
			{
				this.m_rootPath = Application.persistentDataPath;
			}
			else
			{
				this.m_rootPath = this.Combine(new string[]
				{
					Application.persistentDataPath,
					name
				});
			}
			if (!Directory.Exists(this.RootPath))
			{
				Directory.CreateDirectory(this.RootPath);
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000023E8 File Offset: 0x000005E8
		public string Combine(params string[] paths)
		{
			foreach (string text in paths)
			{
				if (text.StartsWith(Application.streamingAssetsPath))
				{
					Debug.LogWarning("Attempted to perform IO operation with a ROM path " + text + ". This is not allowed and could crash on platform, but might succeed on Standalone");
				}
			}
			return Path.Combine(paths);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002431 File Offset: 0x00000631
		private string EnsurePathRooted(string path)
		{
			if (!Path.IsPathRooted(path))
			{
				return Path.Combine(this.RootPath, path);
			}
			return path;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000244C File Offset: 0x0000064C
		public void OpenStream(string path, FileMode mode, FileAccess access, Action<StorageResult, Stream> callback)
		{
			try
			{
				FileStream fileStream = File.Open(this.EnsurePathRooted(path), mode, access);
				callback(StorageResult.Success, fileStream);
			}
			catch (Exception ex)
			{
				StorageResult storageResult = this.ExceptionToStorageResult(ex);
				callback(storageResult, null);
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002498 File Offset: 0x00000698
		public void DeleteBlob(string path, OnDeleteComplete callback)
		{
			string text = this.EnsurePathRooted(path);
			try
			{
				File.Delete(text);
				callback(StorageResult.Success);
			}
			catch (ArgumentException)
			{
				callback(StorageResult.InvalidPath);
			}
			catch (DirectoryNotFoundException)
			{
				callback(StorageResult.DirectoryNotFound);
			}
			catch (PathTooLongException)
			{
				callback(StorageResult.PathTooLong);
			}
			catch (IOException)
			{
				callback(StorageResult.InUse);
			}
			catch (NotSupportedException)
			{
				callback(StorageResult.UnknownFailure);
			}
			catch (UnauthorizedAccessException)
			{
				callback(StorageResult.InvalidPermissions);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002548 File Offset: 0x00000748
		public void DeleteBlobs(string[] paths, OnDeleteComplete callback)
		{
			foreach (string text in paths)
			{
				try
				{
					File.Delete(this.EnsurePathRooted(text));
				}
				catch
				{
					callback(StorageResult.UnknownFailure);
					return;
				}
			}
			callback(StorageResult.Success);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000259C File Offset: 0x0000079C
		public void FileMetadata(string path, OnGetFileMetadataComplete callback)
		{
			try
			{
				FileInfo fileInfo = new FileInfo(this.EnsurePathRooted(path));
				callback(StorageResult.Success, new FileMetadata
				{
					SizeInBytes = fileInfo.Length
				});
			}
			catch (Exception ex)
			{
				callback(this.ExceptionToStorageResult(ex), default(FileMetadata));
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002600 File Offset: 0x00000800
		public void DirectoryExists(string path, OnDirectoryExistCheck callback)
		{
			if (Directory.Exists(this.EnsurePathRooted(path)))
			{
				callback(StorageResult.Success);
				return;
			}
			callback(StorageResult.DirectoryNotFound);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000261F File Offset: 0x0000081F
		public void FileExists(string path, OnFileExistCheck callback)
		{
			if (File.Exists(this.EnsurePathRooted(path)))
			{
				callback(StorageResult.Success);
				return;
			}
			callback(StorageResult.FileNotFound);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002640 File Offset: 0x00000840
		public void EnumerateDirectories(string path, OnDirectoriesEnumerated callback)
		{
			string text = this.EnsurePathRooted(path);
			if (Directory.Exists(text))
			{
				callback(StorageResult.Success, Directory.GetDirectories(text));
				return;
			}
			callback(StorageResult.DirectoryNotFound, null);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002674 File Offset: 0x00000874
		public void EnumerateFiles(string path, OnFilesEnumerated callback)
		{
			string text = this.EnsurePathRooted(path);
			if (Directory.Exists(text))
			{
				callback(StorageResult.Success, Directory.GetFiles(text));
				return;
			}
			callback(StorageResult.DirectoryNotFound, null);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000026A7 File Offset: 0x000008A7
		public void LoadBlob(string path, int offset, int length, OnLoadBlobBytesComplete callback)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000026B0 File Offset: 0x000008B0
		public void LoadBlob(string path, OnLoadBlobStringComplete callback)
		{
			try
			{
				string text = File.ReadAllText(this.EnsurePathRooted(path));
				callback(StorageResult.Success, text);
			}
			catch (Exception ex)
			{
				callback(this.ExceptionToStorageResult(ex), null);
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000026F8 File Offset: 0x000008F8
		public void LoadBlob(string path, OnLoadBlobBytesComplete callback)
		{
			try
			{
				byte[] array = File.ReadAllBytes(this.EnsurePathRooted(path));
				callback(StorageResult.Success, array);
			}
			catch (Exception ex)
			{
				callback(this.ExceptionToStorageResult(ex), null);
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002740 File Offset: 0x00000940
		public void LoadBlobs(string[] filenames, OnLoadBlobsStringComplete callback)
		{
			ValueTuple<string, string>[] array = new ValueTuple<string, string>[filenames.Length];
			for (int i = 0; i < filenames.Length; i++)
			{
				try
				{
					string text = this.EnsurePathRooted(filenames[i]);
					array[i] = new ValueTuple<string, string>(filenames[i], File.ReadAllText(text));
				}
				catch (Exception ex)
				{
					callback(this.ExceptionToStorageResult(ex), null);
					return;
				}
			}
			callback(StorageResult.Success, array);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000027B0 File Offset: 0x000009B0
		public void LoadBlobs(string[] paths, OnLoadBlobsBytesComplete callback)
		{
			ValueTuple<string, byte[]>[] array = new ValueTuple<string, byte[]>[paths.Length];
			for (int i = 0; i < paths.Length; i++)
			{
				try
				{
					string text = this.EnsurePathRooted(paths[i]);
					array[i] = new ValueTuple<string, byte[]>(paths[i], File.ReadAllBytes(text));
				}
				catch (Exception ex)
				{
					callback(this.ExceptionToStorageResult(ex), null);
					return;
				}
			}
			callback(StorageResult.Success, array);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002820 File Offset: 0x00000A20
		public void SaveBlob(string path, string contents, OnSaveBlobComplete callback)
		{
			using (new DelayShutdownScope_Platform(this))
			{
				try
				{
					string text = this.EnsurePathRooted(path);
					Directory.CreateDirectory(Path.GetDirectoryName(text));
					File.WriteAllText(text, contents);
					callback(StorageResult.Success);
				}
				catch (Exception ex)
				{
					callback(this.ExceptionToStorageResult(ex));
				}
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002894 File Offset: 0x00000A94
		public void SaveBlob(string path, byte[] contents, OnSaveBlobComplete callback)
		{
			using (new DelayShutdownScope_Platform(this))
			{
				try
				{
					string text = this.EnsurePathRooted(path);
					Directory.CreateDirectory(Path.GetDirectoryName(text));
					File.WriteAllBytes(text, contents);
					callback(StorageResult.Success);
				}
				catch (Exception ex)
				{
					callback(this.ExceptionToStorageResult(ex));
				}
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002908 File Offset: 0x00000B08
		public void SaveBlob(string path, int offset, int length, byte[] contents, OnSaveBlobComplete callback)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002910 File Offset: 0x00000B10
		public void SaveBlobs(ValueTuple<string, string>[] blobs, OnSaveBlobComplete callback)
		{
			using (new DelayShutdownScope_Platform(this))
			{
				foreach (ValueTuple<string, string> valueTuple in blobs)
				{
					try
					{
						string text = this.EnsurePathRooted(valueTuple.Item1);
						Directory.CreateDirectory(Path.GetDirectoryName(text));
						File.WriteAllText(text, valueTuple.Item2);
						callback(StorageResult.Success);
					}
					catch (Exception ex)
					{
						callback(this.ExceptionToStorageResult(ex));
					}
				}
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000029A8 File Offset: 0x00000BA8
		public void SaveBlobs(ValueTuple<string, byte[]>[] blobs, OnSaveBlobComplete callback)
		{
			using (new DelayShutdownScope_Platform(this))
			{
				foreach (ValueTuple<string, byte[]> valueTuple in blobs)
				{
					try
					{
						string text = this.EnsurePathRooted(valueTuple.Item1);
						Directory.CreateDirectory(Path.GetDirectoryName(text));
						File.WriteAllBytes(text, valueTuple.Item2);
						callback(StorageResult.Success);
					}
					catch (Exception ex)
					{
						callback(this.ExceptionToStorageResult(ex));
					}
				}
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002A40 File Offset: 0x00000C40
		public void Dispose()
		{
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002A42 File Offset: 0x00000C42
		public void GetUsedStorageQuota(OnQuotaRemainingCheck callback)
		{
			callback(StorageResult.Success, Storage.GetDirectorySize(this.RootPath));
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002A58 File Offset: 0x00000C58
		public void GetTotalStorageQuota(out long quotaSize, out long journalSize)
		{
			journalSize = -1L;
			quotaSize = 0L;
			foreach (DriveInfo driveInfo in DriveInfo.GetDrives())
			{
				if (driveInfo.Name.StartsWith(Path.GetPathRoot(Path.GetFullPath(Application.persistentDataPath))))
				{
					quotaSize = driveInfo.AvailableFreeSpace;
				}
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002AA9 File Offset: 0x00000CA9
		public void GetMaxStorageQuota(out long quotaSizeMax, out long journalSizeMax)
		{
			this.GetTotalStorageQuota(out quotaSizeMax, out journalSizeMax);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002AB3 File Offset: 0x00000CB3
		public StorageResult ExpandStorageQuota(long newSaveSize, long newJournalSize)
		{
			return StorageResult.Success;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002AB6 File Offset: 0x00000CB6
		public bool Equals(IStorage other)
		{
			return true;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002AB9 File Offset: 0x00000CB9
		private static long GetDirectorySize(string path)
		{
			return Storage.GetDirectorySize(new DirectoryInfo(path));
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002AC8 File Offset: 0x00000CC8
		private static long GetDirectorySize(DirectoryInfo directoryInfo)
		{
			long num = 0L;
			foreach (FileInfo fileInfo in directoryInfo.GetFiles())
			{
				num += fileInfo.Length;
			}
			foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
			{
				num += Storage.GetDirectorySize(directoryInfo2);
			}
			return num;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002B24 File Offset: 0x00000D24
		private StorageResult ExceptionToStorageResult(Exception exception)
		{
			if (exception is InvalidPathException || exception is ArgumentException)
			{
				return StorageResult.InvalidPath;
			}
			if (exception is DirectoryNotFoundException)
			{
				return StorageResult.DirectoryNotFound;
			}
			if (exception is FileNotFoundException)
			{
				return StorageResult.FileNotFound;
			}
			if (exception is PathTooLongException)
			{
				return StorageResult.PathTooLong;
			}
			if (exception is IOException)
			{
				return StorageResult.InUse;
			}
			if (exception is NotSupportedException)
			{
				return StorageResult.UnknownFailure;
			}
			if (!(exception is UnauthorizedAccessException) && !(exception is SecurityException))
			{
				return StorageResult.UnknownFailure;
			}
			return StorageResult.InvalidPermissions;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002B90 File Offset: 0x00000D90
		public void CreateDirectory(string path, OnDirectoryCreate callback)
		{
			using (new DelayShutdownScope_Platform(this))
			{
				try
				{
					Directory.CreateDirectory(this.EnsurePathRooted(path));
					callback(StorageResult.Success);
				}
				catch (Exception ex)
				{
					if (ex is IOException)
					{
						callback(StorageResult.InvalidPath);
					}
					else
					{
						callback(this.ExceptionToStorageResult(ex));
					}
				}
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002C08 File Offset: 0x00000E08
		public void DeleteDirectory(string path, OnDirectoryDelete callback)
		{
			using (new DelayShutdownScope_Platform(this))
			{
				try
				{
					Directory.Delete(this.EnsurePathRooted(path), true);
					callback(StorageResult.Success);
				}
				catch (Exception ex)
				{
					if (ex is IOException)
					{
						callback(StorageResult.InvalidPath);
					}
					else
					{
						callback(this.ExceptionToStorageResult(ex));
					}
				}
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002C80 File Offset: 0x00000E80
		public void DeleteDirectories(string[] paths, OnDirectoryDelete callback)
		{
			using (new DelayShutdownScope_Platform(this))
			{
				foreach (string text in paths)
				{
					try
					{
						Directory.Delete(this.EnsurePathRooted(text), true);
						callback(StorageResult.Success);
					}
					catch (Exception ex)
					{
						if (ex is IOException)
						{
							callback(StorageResult.InvalidPath);
							break;
						}
						callback(this.ExceptionToStorageResult(ex));
						break;
					}
				}
			}
		}

		// Token: 0x04000006 RID: 6
		private int mainThreadID;

		// Token: 0x04000008 RID: 8
		private string m_rootPath;
	}
}
