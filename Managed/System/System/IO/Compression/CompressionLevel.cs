using System;

namespace System.IO.Compression
{
	/// <summary>Specifies values that indicate whether a compression operation emphasizes speed or compression size.</summary>
	// Token: 0x02000546 RID: 1350
	public enum CompressionLevel
	{
		/// <summary>The compression operation should be optimally compressed, even if the operation takes a longer time to complete.</summary>
		// Token: 0x040017C7 RID: 6087
		Optimal,
		/// <summary>The compression operation should complete as quickly as possible, even if the resulting file is not optimally compressed.</summary>
		// Token: 0x040017C8 RID: 6088
		Fastest,
		/// <summary>No compression should be performed on the file.</summary>
		// Token: 0x040017C9 RID: 6089
		NoCompression
	}
}
