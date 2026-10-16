using System;

namespace System.IO
{
	/// <summary>Specifies changes to watch for in a file or folder.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000526 RID: 1318
	[Flags]
	public enum NotifyFilters
	{
		/// <summary>The attributes of the file or folder.</summary>
		// Token: 0x04001708 RID: 5896
		Attributes = 4,
		/// <summary>The time the file or folder was created.</summary>
		// Token: 0x04001709 RID: 5897
		CreationTime = 64,
		/// <summary>The name of the directory.</summary>
		// Token: 0x0400170A RID: 5898
		DirectoryName = 2,
		/// <summary>The name of the file.</summary>
		// Token: 0x0400170B RID: 5899
		FileName = 1,
		/// <summary>The date the file or folder was last opened.</summary>
		// Token: 0x0400170C RID: 5900
		LastAccess = 32,
		/// <summary>The date the file or folder last had anything written to it.</summary>
		// Token: 0x0400170D RID: 5901
		LastWrite = 16,
		/// <summary>The security settings of the file or folder.</summary>
		// Token: 0x0400170E RID: 5902
		Security = 256,
		/// <summary>The size of the file or folder.</summary>
		// Token: 0x0400170F RID: 5903
		Size = 8
	}
}
