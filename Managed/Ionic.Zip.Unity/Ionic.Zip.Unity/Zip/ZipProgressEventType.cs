using System;

namespace Ionic.Zip
{
	// Token: 0x02000008 RID: 8
	public enum ZipProgressEventType
	{
		// Token: 0x04000008 RID: 8
		Adding_Started,
		// Token: 0x04000009 RID: 9
		Adding_AfterAddEntry,
		// Token: 0x0400000A RID: 10
		Adding_Completed,
		// Token: 0x0400000B RID: 11
		Reading_Started,
		// Token: 0x0400000C RID: 12
		Reading_BeforeReadEntry,
		// Token: 0x0400000D RID: 13
		Reading_AfterReadEntry,
		// Token: 0x0400000E RID: 14
		Reading_Completed,
		// Token: 0x0400000F RID: 15
		Reading_ArchiveBytesRead,
		// Token: 0x04000010 RID: 16
		Saving_Started,
		// Token: 0x04000011 RID: 17
		Saving_BeforeWriteEntry,
		// Token: 0x04000012 RID: 18
		Saving_AfterWriteEntry,
		// Token: 0x04000013 RID: 19
		Saving_Completed,
		// Token: 0x04000014 RID: 20
		Saving_AfterSaveTempArchive,
		// Token: 0x04000015 RID: 21
		Saving_BeforeRenameTempArchive,
		// Token: 0x04000016 RID: 22
		Saving_AfterRenameTempArchive,
		// Token: 0x04000017 RID: 23
		Saving_AfterCompileSelfExtractor,
		// Token: 0x04000018 RID: 24
		Saving_EntryBytesRead,
		// Token: 0x04000019 RID: 25
		Extracting_BeforeExtractEntry,
		// Token: 0x0400001A RID: 26
		Extracting_AfterExtractEntry,
		// Token: 0x0400001B RID: 27
		Extracting_ExtractEntryWouldOverwrite,
		// Token: 0x0400001C RID: 28
		Extracting_EntryBytesWritten,
		// Token: 0x0400001D RID: 29
		Extracting_BeforeExtractAll,
		// Token: 0x0400001E RID: 30
		Extracting_AfterExtractAll,
		// Token: 0x0400001F RID: 31
		Error_Saving
	}
}
