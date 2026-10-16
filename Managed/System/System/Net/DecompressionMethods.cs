using System;

namespace System.Net
{
	/// <summary>Represents the file compression and decompression encoding format to be used to compress the data received in response to an <see cref="T:System.Net.HttpWebRequest" />.</summary>
	// Token: 0x02000675 RID: 1653
	[Flags]
	public enum DecompressionMethods
	{
		/// <summary>Do not use compression.</summary>
		// Token: 0x04001E96 RID: 7830
		None = 0,
		/// <summary>Use the gZip compression-decompression algorithm.</summary>
		// Token: 0x04001E97 RID: 7831
		GZip = 1,
		/// <summary>Use the deflate compression-decompression algorithm.</summary>
		// Token: 0x04001E98 RID: 7832
		Deflate = 2
	}
}
