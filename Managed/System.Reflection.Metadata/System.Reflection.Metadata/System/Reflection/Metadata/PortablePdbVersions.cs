using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000043 RID: 67
	[NullableContext(1)]
	[Nullable(0)]
	internal static class PortablePdbVersions
	{
		// Token: 0x060002C5 RID: 709 RVA: 0x00008C5E File Offset: 0x00006E5E
		internal static uint DebugDirectoryEntryVersion(ushort portablePdbVersion)
		{
			return 1347223552U | (uint)portablePdbVersion;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00008C67 File Offset: 0x00006E67
		internal static uint DebugDirectoryEmbeddedVersion(ushort portablePdbVersion)
		{
			return 16777216U | (uint)portablePdbVersion;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00008C70 File Offset: 0x00006E70
		internal static string Format(ushort version)
		{
			return (version >> 8).ToString() + "." + ((int)(version & 255)).ToString();
		}

		// Token: 0x040001B2 RID: 434
		internal const string DefaultMetadataVersion = "PDB v1.0";

		// Token: 0x040001B3 RID: 435
		internal const ushort DefaultFormatVersion = 256;

		// Token: 0x040001B4 RID: 436
		internal const ushort MinFormatVersion = 256;

		// Token: 0x040001B5 RID: 437
		internal const ushort MinEmbeddedVersion = 256;

		// Token: 0x040001B6 RID: 438
		internal const ushort DefaultEmbeddedVersion = 256;

		// Token: 0x040001B7 RID: 439
		internal const ushort MinUnsupportedEmbeddedVersion = 512;

		// Token: 0x040001B8 RID: 440
		internal const uint DebugDirectoryEmbeddedSignature = 1111773261U;

		// Token: 0x040001B9 RID: 441
		internal const ushort PortableCodeViewVersionMagic = 20557;
	}
}
