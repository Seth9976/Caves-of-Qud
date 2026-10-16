using System;

namespace System.Reflection.PortableExecutable
{
	/// <summary>Provides options that specify how sections of a PE image are read from a stream.</summary>
	// Token: 0x02000039 RID: 57
	[Flags]
	public enum PEStreamOptions
	{
		/// <summary>By default, the stream is disposed when the <see cref="T:System.Reflection.PortableExecutable.PEReader" /> is disposed, and sections of the PE image are read lazily.</summary>
		// Token: 0x04000184 RID: 388
		Default = 0,
		/// <summary>Keeps the stream open when the <see cref="T:System.Reflection.PortableExecutable.PEReader" /> is disposed.</summary>
		// Token: 0x04000185 RID: 389
		LeaveOpen = 1,
		/// <summary>
		///   <para>Reads the metadata section into memory right away.</para>
		///   <para>Reading from other sections of the file is not allowed (<see cref="T:System.InvalidOperationException" /> is thrown by the <see cref="T:System.Reflection.PortableExecutable.PEReader" />).</para>
		///   <para>
		///     <see cref="T:System.Reflection.PortableExecutable.PEReader" /> closes the stream automatically by the time the constructor returns unless <see cref="F:System.Reflection.PortableExecutable.PEStreamOptions.LeaveOpen" /> is specified. The underlying file may be closed and even deleted after <see cref="T:System.Reflection.PortableExecutable.PEReader" /> is constructed.</para>
		/// </summary>
		// Token: 0x04000186 RID: 390
		PrefetchMetadata = 2,
		/// <summary>Reads the entire image into memory right away. <see cref="T:System.Reflection.PortableExecutable.PEReader" /> closes the stream automatically by the time the constructor returns unless <see cref="F:System.Reflection.PortableExecutable.PEStreamOptions.LeaveOpen" /> is specified.</summary>
		// Token: 0x04000187 RID: 391
		PrefetchEntireImage = 4,
		/// <summary>Indicates that the underlying PE image has been loaded into memory by the OS loader.</summary>
		// Token: 0x04000188 RID: 392
		IsLoadedImage = 8
	}
}
