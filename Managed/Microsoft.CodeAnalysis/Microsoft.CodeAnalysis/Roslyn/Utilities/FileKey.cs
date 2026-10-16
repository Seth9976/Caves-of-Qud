using System;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x02000026 RID: 38
	[NullableContext(1)]
	[Nullable(0)]
	internal struct FileKey : IEquatable<FileKey>
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00003234 File Offset: 0x00001434
		public FileKey(string fullPath, DateTime timestamp)
		{
			this.FullPath = fullPath;
			this.Timestamp = timestamp;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003244 File Offset: 0x00001444
		public static FileKey Create(string fullPath)
		{
			return new FileKey(fullPath, FileUtilities.GetFileTimeStamp(fullPath));
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003254 File Offset: 0x00001454
		public override int GetHashCode()
		{
			return Hash.Combine(StringComparer.OrdinalIgnoreCase.GetHashCode(this.FullPath), this.Timestamp.GetHashCode());
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003284 File Offset: 0x00001484
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is FileKey && this.Equals((FileKey)obj);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000329C File Offset: 0x0000149C
		public override string ToString()
		{
			return string.Format("'{0}'@{1}", this.FullPath, this.Timestamp);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000032B9 File Offset: 0x000014B9
		public bool Equals(FileKey other)
		{
			return this.Timestamp == other.Timestamp && string.Equals(this.FullPath, other.FullPath, 5);
		}

		// Token: 0x0400001E RID: 30
		public readonly string FullPath;

		// Token: 0x0400001F RID: 31
		public readonly DateTime Timestamp;
	}
}
