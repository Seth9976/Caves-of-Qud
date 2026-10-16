using System;

namespace Ionic.Zip
{
	// Token: 0x02000028 RID: 40
	internal static class ZipConstants
	{
		// Token: 0x04000082 RID: 130
		public const uint PackedToRemovableMedia = 808471376U;

		// Token: 0x04000083 RID: 131
		public const uint Zip64EndOfCentralDirectoryRecordSignature = 101075792U;

		// Token: 0x04000084 RID: 132
		public const uint Zip64EndOfCentralDirectoryLocatorSignature = 117853008U;

		// Token: 0x04000085 RID: 133
		public const uint EndOfCentralDirectorySignature = 101010256U;

		// Token: 0x04000086 RID: 134
		public const int ZipEntrySignature = 67324752;

		// Token: 0x04000087 RID: 135
		public const int ZipEntryDataDescriptorSignature = 134695760;

		// Token: 0x04000088 RID: 136
		public const int SplitArchiveSignature = 134695760;

		// Token: 0x04000089 RID: 137
		public const int ZipDirEntrySignature = 33639248;

		// Token: 0x0400008A RID: 138
		public const int AesKeySize = 192;

		// Token: 0x0400008B RID: 139
		public const int AesBlockSize = 128;

		// Token: 0x0400008C RID: 140
		public const ushort AesAlgId128 = 26126;

		// Token: 0x0400008D RID: 141
		public const ushort AesAlgId192 = 26127;

		// Token: 0x0400008E RID: 142
		public const ushort AesAlgId256 = 26128;
	}
}
