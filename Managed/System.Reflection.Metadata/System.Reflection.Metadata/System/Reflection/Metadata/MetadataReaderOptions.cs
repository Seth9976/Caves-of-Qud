using System;

namespace System.Reflection.Metadata
{
	// Token: 0x020000A7 RID: 167
	[Flags]
	public enum MetadataReaderOptions
	{
		/// <summary>All options are disabled.</summary>
		// Token: 0x04000417 RID: 1047
		None = 0,
		/// <summary>The options that are used when a <see cref="T:System.Reflection.Metadata.MetadataReader" /> is obtained via an overload that does not take a <see cref="T:System.Reflection.Metadata.MetadataReaderOptions" /> argument.</summary>
		// Token: 0x04000418 RID: 1048
		Default = 1,
		/// <summary>Windows Runtime projections are enabled (on by default).</summary>
		// Token: 0x04000419 RID: 1049
		ApplyWindowsRuntimeProjections = 1
	}
}
