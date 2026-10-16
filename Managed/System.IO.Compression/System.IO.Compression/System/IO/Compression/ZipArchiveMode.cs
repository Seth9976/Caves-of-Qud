using System;

namespace System.IO.Compression
{
	/// <summary>Specifies values for interacting with zip archive entries.</summary>
	// Token: 0x02000032 RID: 50
	public enum ZipArchiveMode
	{
		/// <summary>Only reading archive entries is permitted.</summary>
		// Token: 0x040001B3 RID: 435
		Read,
		/// <summary>Only creating new archive entries is permitted.</summary>
		// Token: 0x040001B4 RID: 436
		Create,
		/// <summary>Both read and write operations are permitted for archive entries.</summary>
		// Token: 0x040001B5 RID: 437
		Update
	}
}
