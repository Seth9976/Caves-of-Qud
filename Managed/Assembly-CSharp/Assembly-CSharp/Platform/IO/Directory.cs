using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

namespace Platform.IO
{
	// Token: 0x02000336 RID: 822
	public static class Directory
	{
		// Token: 0x06001EDD RID: 7901 RVA: 0x00097868 File Offset: 0x00095A68
		public static bool Exists(string path)
		{
			return Folder.DirectoryExists(path).WasSuccessful();
		}

		// Token: 0x06001EDE RID: 7902 RVA: 0x00097878 File Offset: 0x00095A78
		public static DirectoryInfo CreateDirectory(string path)
		{
			Folder.CreateDirectory(path).ThrowIfFailed();
			return new DirectoryInfo(path);
		}

		// Token: 0x06001EDF RID: 7903 RVA: 0x0009789C File Offset: 0x00095A9C
		public static void Delete(string path, bool recursive = true)
		{
			if (recursive)
			{
				Folder.DeleteRecursive(path).ThrowIfFailed();
				return;
			}
			Debug.LogError("Not implemented: Delete(string path, bool recursive=false)");
			throw new NotImplementedException("Not implemented: Delete(string path, bool recursive=false)");
		}

		// Token: 0x06001EE0 RID: 7904 RVA: 0x000978D0 File Offset: 0x00095AD0
		public static async Task<IEnumerable<string>> EnumerateDirectoriesAsync(string path)
		{
			TaskAwaiter<EnumerateDirectoriesResult> taskAwaiter = Folder.EnumerateDirectoriesAsync(path).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<EnumerateDirectoriesResult> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<EnumerateDirectoriesResult>);
			}
			return taskAwaiter.GetResult().ThrowIfFailed().directories;
		}

		// Token: 0x06001EE1 RID: 7905 RVA: 0x00097914 File Offset: 0x00095B14
		public static IEnumerable<string> EnumerateDirectories(string path)
		{
			return Folder.EnumerateDirectories(path).ThrowIfFailed().directories;
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x00097934 File Offset: 0x00095B34
		public static IEnumerable<string> EnumerateFiles(string path)
		{
			return Folder.EnumerateFilesShallow(path).ThrowIfFailed().files;
		}

		// Token: 0x06001EE3 RID: 7907 RVA: 0x00097954 File Offset: 0x00095B54
		public static async Task<IEnumerable<string>> EnumerateFilesAsync(string path)
		{
			TaskAwaiter<EnumerateFilesResult> taskAwaiter = Folder.EnumerateFilesShallowAsync(path).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<EnumerateFilesResult> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<EnumerateFilesResult>);
			}
			return taskAwaiter.GetResult().ThrowIfFailed().files;
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x00097997 File Offset: 0x00095B97
		public static IEnumerable<string> EnumerateFiles(string path, SearchOption option)
		{
			return Folder.EnumerateFiles(path, option).files;
		}

		// Token: 0x06001EE5 RID: 7909 RVA: 0x000979A8 File Offset: 0x00095BA8
		public static IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption option)
		{
			string[] files = Folder.EnumerateFiles(path, option).files;
			if (searchPattern != "*" && searchPattern != null && searchPattern != "*.*")
			{
				List<string> list = new List<string>();
				foreach (string text in files)
				{
					if (Path.IsMatch(text, searchPattern))
					{
						list.Add(text);
					}
				}
				return list;
			}
			return files;
		}

		// Token: 0x06001EE6 RID: 7910 RVA: 0x00097A10 File Offset: 0x00095C10
		public static async Task<IEnumerable<string>> EnumerateFilesAsync(string path, SearchOption option)
		{
			TaskAwaiter<EnumerateFilesResult> taskAwaiter = Folder.EnumerateFilesAsync(path, option).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<EnumerateFilesResult> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<EnumerateFilesResult>);
			}
			return taskAwaiter.GetResult().ThrowIfFailed().files;
		}

		// Token: 0x06001EE7 RID: 7911 RVA: 0x00097A5C File Offset: 0x00095C5C
		public static IEnumerable<FileSystemInfo> EnumerateFileSystemInfos(string path)
		{
			List<FileSystemInfo> list = new List<FileSystemInfo>();
			foreach (string text in Directory.EnumerateFiles(path))
			{
				list.Add(new FileInfo(text));
			}
			foreach (string text2 in Directory.EnumerateDirectories(path))
			{
				list.Add(new DirectoryInfo(text2));
			}
			return list;
		}

		// Token: 0x06001EE8 RID: 7912 RVA: 0x00097AF8 File Offset: 0x00095CF8
		public static string[] GetDirectories(string path)
		{
			return Directory.EnumerateDirectories(path).ToArray<string>();
		}
	}
}
