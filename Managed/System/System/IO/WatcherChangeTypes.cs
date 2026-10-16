using System;

namespace System.IO
{
	/// <summary>Changes that might occur to a file or directory.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200052B RID: 1323
	[Flags]
	public enum WatcherChangeTypes
	{
		/// <summary>The creation, deletion, change, or renaming of a file or folder.</summary>
		// Token: 0x04001721 RID: 5921
		All = 15,
		/// <summary>The change of a file or folder. The types of changes include: changes to size, attributes, security settings, last write, and last access time.</summary>
		// Token: 0x04001722 RID: 5922
		Changed = 4,
		/// <summary>The creation of a file or folder.</summary>
		// Token: 0x04001723 RID: 5923
		Created = 1,
		/// <summary>The deletion of a file or folder.</summary>
		// Token: 0x04001724 RID: 5924
		Deleted = 2,
		/// <summary>The renaming of a file or folder.</summary>
		// Token: 0x04001725 RID: 5925
		Renamed = 8
	}
}
