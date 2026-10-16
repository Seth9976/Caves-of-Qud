using System;
using System.IO;

namespace LaundryBear.PlatformServices
{
	// Token: 0x02000047 RID: 71
	public interface IStorage : IDisposable, IEquatable<IStorage>
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000E4 RID: 228
		bool IsMounted { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000E5 RID: 229
		IUser AssociatedUser { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000E6 RID: 230
		string RootPath { get; }

		// Token: 0x060000E7 RID: 231
		void GetUsedStorageQuota(OnQuotaRemainingCheck callback);

		// Token: 0x060000E8 RID: 232
		void GetTotalStorageQuota(out long quotaSize, out long journalSize);

		// Token: 0x060000E9 RID: 233
		StorageResult ExpandStorageQuota(long newSaveSize, long newJournalSize);

		// Token: 0x060000EA RID: 234
		void GetMaxStorageQuota(out long quotaSizeMax, out long journalSizeMax);

		// Token: 0x060000EB RID: 235
		void DirectoryExists(string path, OnDirectoryExistCheck callback);

		// Token: 0x060000EC RID: 236
		void FileExists(string path, OnFileExistCheck callback);

		// Token: 0x060000ED RID: 237
		bool IsPathRootedToMount(string path);

		// Token: 0x060000EE RID: 238
		string GetPathMount(string path);

		// Token: 0x060000EF RID: 239
		void CreateDirectory(string path, OnDirectoryCreate callback);

		// Token: 0x060000F0 RID: 240
		void DeleteDirectory(string path, OnDirectoryDelete callback);

		// Token: 0x060000F1 RID: 241
		void EnumerateFiles(string path, OnFilesEnumerated callback);

		// Token: 0x060000F2 RID: 242
		void EnumerateDirectories(string path, OnDirectoriesEnumerated callback);

		// Token: 0x060000F3 RID: 243
		void SaveBlob(string path, string contents, OnSaveBlobComplete callback);

		// Token: 0x060000F4 RID: 244
		void SaveBlob(string path, byte[] contents, OnSaveBlobComplete callback);

		// Token: 0x060000F5 RID: 245
		void LoadBlob(string path, OnLoadBlobStringComplete callback);

		// Token: 0x060000F6 RID: 246
		void LoadBlob(string path, OnLoadBlobBytesComplete callback);

		// Token: 0x060000F7 RID: 247
		void OpenStream(string path, FileMode mode, FileAccess access, Action<StorageResult, Stream> onCreate);

		// Token: 0x060000F8 RID: 248
		void DeleteBlob(string path, OnDeleteComplete callback);

		// Token: 0x060000F9 RID: 249
		void FileMetadata(string path, OnGetFileMetadataComplete callback);
	}
}
