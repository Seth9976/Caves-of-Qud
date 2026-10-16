using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using LaundryBear.PlatformServices;

namespace Platform.IO
{
	// Token: 0x02000338 RID: 824
	public static class Folder
	{
		// Token: 0x06001F09 RID: 7945 RVA: 0x0009820C File Offset: 0x0009640C
		public static StorageResult DirectoryExists(string path)
		{
			if (Path.IsROM(path))
			{
				try
				{
					if (Directory.Exists(path))
					{
						return StorageResult.Success;
					}
					return StorageResult.DirectoryNotFound;
				}
				catch (Exception ex)
				{
					return ex.ExceptionToStorageResult();
				}
			}
			TaskCompletionSource<StorageResult> tcs = new TaskCompletionSource<StorageResult>();
			State.GetStorage().DirectoryExists(path, delegate(StorageResult result)
			{
				tcs.SetResult(result);
			});
			return tcs.Task.Result;
		}

		// Token: 0x06001F0A RID: 7946 RVA: 0x00098284 File Offset: 0x00096484
		public static async Task<StorageResult> DirectoryExistsAsync(string path)
		{
			if (Path.IsROM(path))
			{
				try
				{
					if (Directory.Exists(path))
					{
						return StorageResult.Success;
					}
					return StorageResult.DirectoryNotFound;
				}
				catch (Exception ex)
				{
					return ex.ExceptionToStorageResult();
				}
			}
			TaskCompletionSource<StorageResult> tcs = new TaskCompletionSource<StorageResult>();
			State.GetStorage().DirectoryExists(path, delegate(StorageResult result)
			{
				tcs.SetResult(result);
			});
			return await tcs.Task;
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x000982C7 File Offset: 0x000964C7
		public static PlatformIOResult CreateDirectory(string path)
		{
			return Folder.CreateDirectoryAsync(path).AwaitResult<PlatformIOResult>();
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x000982D4 File Offset: 0x000964D4
		public static async Task<PlatformIOResult> CreateDirectoryAsync(string path)
		{
			TaskAwaiter<StorageResult> taskAwaiter = Folder.DirectoryExistsAsync(path).GetAwaiter();
			TaskAwaiter<StorageResult> taskAwaiter2;
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<StorageResult>);
			}
			PlatformIOResult platformIOResult;
			if (taskAwaiter.GetResult().WasSuccessful())
			{
				platformIOResult = new PlatformIOResult(StorageResult.Success, null);
			}
			else
			{
				string parentPath = Path.GetDirectoryName(path);
				if (parentPath.EndsWith(":"))
				{
					parentPath += Path.DirectorySeparatorChar.ToString();
				}
				taskAwaiter = Folder.DirectoryExistsAsync(parentPath).GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<StorageResult>);
				}
				if (!taskAwaiter.GetResult().WasSuccessful())
				{
					PlatformIOResult platformIOResult2 = await Folder.CreateDirectoryAsync(parentPath);
					if (!platformIOResult2.WasSuccessful())
					{
						return new PlatformIOResult(platformIOResult2.result, "Could not create parent paths");
					}
				}
				platformIOResult = await Folder.CreateDirectoryIfParentPresent(path);
			}
			return platformIOResult;
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x00098318 File Offset: 0x00096518
		private static Task<PlatformIOResult> CreateDirectoryIfParentPresent(string path)
		{
			TaskCompletionSource<PlatformIOResult> tcs = new TaskCompletionSource<PlatformIOResult>();
			if (Path.IsROM(path))
			{
				tcs.SetResult(new PlatformIOResult(StorageResult.InvalidPermissions, "Can't CreateDirectoryIfParentPresent path " + path + " because it is ROM."));
				return tcs.Task;
			}
			State.GetStorage().CreateDirectory(path, delegate(StorageResult result)
			{
				tcs.SetResult(new PlatformIOResult(result, null));
			});
			return tcs.Task;
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x0009838E File Offset: 0x0009658E
		public static PlatformIOResult DeleteRecursive(string path)
		{
			return Folder.DeleteRecursiveAsync(path).AwaitResult<PlatformIOResult>();
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x0009839C File Offset: 0x0009659C
		public static Task<PlatformIOResult> DeleteRecursiveAsync(string path)
		{
			TaskCompletionSource<PlatformIOResult> tcs = new TaskCompletionSource<PlatformIOResult>();
			if (Path.IsROM(path))
			{
				tcs.SetResult(new PlatformIOResult(StorageResult.InvalidPermissions, "Can't call DeleteRecursiveAsync on path " + path + " because it is ROM."));
				return tcs.Task;
			}
			State.GetStorage().DeleteDirectory(path, delegate(StorageResult result)
			{
				tcs.SetResult(new PlatformIOResult(result, null));
			});
			return tcs.Task;
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x00098412 File Offset: 0x00096612
		public static EnumerateDirectoriesResult EnumerateDirectories(string path)
		{
			return Folder.EnumerateDirectoriesAsync(path).AwaitResult<EnumerateDirectoriesResult>();
		}

		// Token: 0x06001F11 RID: 7953 RVA: 0x00098420 File Offset: 0x00096620
		public static Task<EnumerateDirectoriesResult> EnumerateDirectoriesAsync(string path)
		{
			TaskCompletionSource<EnumerateDirectoriesResult> tcs = new TaskCompletionSource<EnumerateDirectoriesResult>();
			if (Path.IsROM(path))
			{
				try
				{
					IEnumerable<string> enumerable = Directory.EnumerateDirectories(path);
					tcs.SetResult(new EnumerateDirectoriesResult(StorageResult.Success, enumerable.ToArray<string>()));
					return tcs.Task;
				}
				catch (Exception ex)
				{
					tcs.SetResult(new EnumerateDirectoriesResult(ex.ExceptionToStorageResult(), Array.Empty<string>()));
					return tcs.Task;
				}
			}
			State.GetStorage().EnumerateDirectories(path, delegate(StorageResult result, string[] files)
			{
				tcs.SetResult(new EnumerateDirectoriesResult(result, files));
			});
			return tcs.Task;
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x000984D0 File Offset: 0x000966D0
		public static EnumerateFilesResult EnumerateFilesShallow(string path)
		{
			return Folder.EnumerateFilesShallowAsync(path).AwaitResult<EnumerateFilesResult>();
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x000984E0 File Offset: 0x000966E0
		public static Task<EnumerateFilesResult> EnumerateFilesShallowAsync(string path)
		{
			TaskCompletionSource<EnumerateFilesResult> tcs = new TaskCompletionSource<EnumerateFilesResult>();
			if (Path.IsROM(path))
			{
				try
				{
					IEnumerable<string> enumerable = Directory.EnumerateFiles(path);
					tcs.SetResult(new EnumerateFilesResult(StorageResult.Success, enumerable.ToArray<string>()));
					return tcs.Task;
				}
				catch (Exception ex)
				{
					tcs.SetResult(new EnumerateFilesResult(ex.ExceptionToStorageResult(), Array.Empty<string>()));
					return tcs.Task;
				}
			}
			State.GetStorage().EnumerateFiles(path, delegate(StorageResult result, string[] files)
			{
				tcs.SetResult(new EnumerateFilesResult(result, files));
			});
			return tcs.Task;
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x00098590 File Offset: 0x00096790
		public static Task<EnumerateFilesResult> EnumerateFilesRecursiveAsync(string path)
		{
			TaskCompletionSource<EnumerateFilesResult> taskCompletionSource = new TaskCompletionSource<EnumerateFilesResult>();
			if (Path.IsROM(path))
			{
				try
				{
					IEnumerable<string> enumerable = Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories);
					taskCompletionSource.SetResult(new EnumerateFilesResult(StorageResult.Success, enumerable.ToArray<string>()));
					return taskCompletionSource.Task;
				}
				catch (Exception ex)
				{
					taskCompletionSource.SetResult(new EnumerateFilesResult(ex.ExceptionToStorageResult(), Array.Empty<string>()));
					return taskCompletionSource.Task;
				}
			}
			StorageResult storageResult = StorageResult.Success;
			List<string> list = new List<string>();
			Folder.<EnumerateFilesRecursiveAsync>g__recurse|11_0(path, ref storageResult, list);
			taskCompletionSource.SetResult(new EnumerateFilesResult(storageResult, list.ToArray()));
			return taskCompletionSource.Task;
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x00098634 File Offset: 0x00096834
		public static EnumerateFilesResult EnumerateFiles(string path, SearchOption option)
		{
			return Folder.EnumerateFilesAsync(path, option).AwaitResult<EnumerateFilesResult>();
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x00098644 File Offset: 0x00096844
		public static async Task<EnumerateFilesResult> EnumerateFilesAsync(string path, SearchOption option)
		{
			Task<EnumerateFilesResult> task;
			if (option == SearchOption.AllDirectories)
			{
				task = Folder.EnumerateFilesRecursiveAsync(path);
			}
			else
			{
				if (option != SearchOption.TopDirectoryOnly)
				{
					throw new NotImplementedException();
				}
				task = Folder.EnumerateFilesShallowAsync(path);
			}
			return await task;
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x00098690 File Offset: 0x00096890
		[CompilerGenerated]
		internal static bool <EnumerateFilesRecursiveAsync>g__recurse|11_0(string path, ref StorageResult result, List<string> files)
		{
			EnumerateFilesResult result2 = Folder.EnumerateFilesShallowAsync(path).Result;
			if (!result2.WasSuccessful())
			{
				result = result2.result;
				return false;
			}
			if (result2.files != null)
			{
				files.AddRange(result2.files);
			}
			EnumerateDirectoriesResult result3 = Folder.EnumerateDirectoriesAsync(path).Result;
			if (!result3.WasSuccessful())
			{
				return false;
			}
			string[] directories = result3.directories;
			for (int i = 0; i < directories.Length; i++)
			{
				if (!Folder.<EnumerateFilesRecursiveAsync>g__recurse|11_0(directories[i], ref result, files))
				{
					return false;
				}
			}
			return true;
		}
	}
}
