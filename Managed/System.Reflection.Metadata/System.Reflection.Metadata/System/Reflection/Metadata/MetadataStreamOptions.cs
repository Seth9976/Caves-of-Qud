using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200004C RID: 76
	[Flags]
	public enum MetadataStreamOptions
	{
		/// <summary>By default, the stream is disposed when <see cref="T:System.Reflection.Metadata.MetadataReaderProvider" /> is disposed and sections of the PE image are read lazily.</summary>
		// Token: 0x040002A0 RID: 672
		Default = 0,
		/// <summary>Keeps the stream open when the <see cref="T:System.Reflection.Metadata.MetadataReaderProvider" /> is disposed.</summary>
		// Token: 0x040002A1 RID: 673
		LeaveOpen = 1,
		/// <summary>
		///   <para>Reads PDB metadata into memory right away.</para>
		///   <para>The underlying file may be closed and even deleted after the <see cref="T:System.Reflection.Metadata.MetadataReaderProvider" /> is constructed. <see cref="T:System.Reflection.Metadata.MetadataReaderProvider" /> closes the stream automatically by the time the constructor returns unless <see cref="F:System.Reflection.Metadata.MetadataStreamOptions.LeaveOpen" /> is specified.</para>
		/// </summary>
		// Token: 0x040002A2 RID: 674
		PrefetchMetadata = 2
	}
}
