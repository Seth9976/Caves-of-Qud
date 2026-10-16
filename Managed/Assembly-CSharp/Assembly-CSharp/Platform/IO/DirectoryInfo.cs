using System;
using System.Collections.Generic;

namespace Platform.IO
{
	// Token: 0x0200033C RID: 828
	public class DirectoryInfo : FileSystemInfo
	{
		// Token: 0x06001F27 RID: 7975 RVA: 0x000988B4 File Offset: 0x00096AB4
		public DirectoryInfo(string path)
			: base(path)
		{
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x000988BD File Offset: 0x00096ABD
		public override void Refresh()
		{
			base.Refresh();
			this.attributes = this.attributes.CombineFlags(FileAttributes.Directory);
			this.exists = Directory.Exists(this.rawPath);
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x000988E9 File Offset: 0x00096AE9
		public override void Delete()
		{
			Directory.Delete(this.rawPath, true);
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x000988F7 File Offset: 0x00096AF7
		public IEnumerable<FileSystemInfo> EnumerateFileSystemInfos()
		{
			return Directory.EnumerateFileSystemInfos(this.rawPath);
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x00098904 File Offset: 0x00096B04
		public IEnumerable<FileInfo> EnumerateFiles(string searchPattern = null, SearchOption option = SearchOption.TopDirectoryOnly)
		{
			IEnumerable<string> enumerable = Directory.EnumerateFiles(this.rawPath, searchPattern, option);
			List<FileInfo> list = new List<FileInfo>();
			foreach (string text in enumerable)
			{
				list.Add(new FileInfo(text));
			}
			return list;
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x00098964 File Offset: 0x00096B64
		public IEnumerable<DirectoryInfo> EnumerateDirectories()
		{
			IEnumerable<string> enumerable = Directory.EnumerateDirectories(this.rawPath);
			List<DirectoryInfo> list = new List<DirectoryInfo>();
			foreach (string text in enumerable)
			{
				list.Add(new DirectoryInfo(text));
			}
			return list;
		}
	}
}
