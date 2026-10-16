using System;

namespace Ionic.Zip
{
	// Token: 0x0200000C RID: 12
	public class SaveProgressEventArgs : ZipProgressEventArgs
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00002242 File Offset: 0x00000442
		internal SaveProgressEventArgs(string archiveName, bool before, int entriesTotal, int entriesSaved, ZipEntry entry)
			: base(archiveName, before ? ZipProgressEventType.Saving_BeforeWriteEntry : ZipProgressEventType.Saving_AfterWriteEntry)
		{
			base.EntriesTotal = entriesTotal;
			base.CurrentEntry = entry;
			this._entriesSaved = entriesSaved;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000226B File Offset: 0x0000046B
		internal SaveProgressEventArgs()
		{
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002273 File Offset: 0x00000473
		internal SaveProgressEventArgs(string archiveName, ZipProgressEventType flavor)
			: base(archiveName, flavor)
		{
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002280 File Offset: 0x00000480
		internal static SaveProgressEventArgs ByteUpdate(string archiveName, ZipEntry entry, long bytesXferred, long totalBytes)
		{
			return new SaveProgressEventArgs(archiveName, ZipProgressEventType.Saving_EntryBytesRead)
			{
				ArchiveName = archiveName,
				CurrentEntry = entry,
				BytesTransferred = bytesXferred,
				TotalBytesToTransfer = totalBytes
			};
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000022B4 File Offset: 0x000004B4
		internal static SaveProgressEventArgs Started(string archiveName)
		{
			return new SaveProgressEventArgs(archiveName, ZipProgressEventType.Saving_Started);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000022CC File Offset: 0x000004CC
		internal static SaveProgressEventArgs Completed(string archiveName)
		{
			return new SaveProgressEventArgs(archiveName, ZipProgressEventType.Saving_Completed);
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000022E3 File Offset: 0x000004E3
		public int EntriesSaved
		{
			get
			{
				return this._entriesSaved;
			}
		}

		// Token: 0x04000027 RID: 39
		private int _entriesSaved;
	}
}
