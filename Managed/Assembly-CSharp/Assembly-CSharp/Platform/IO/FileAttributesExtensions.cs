using System;
using System.IO;

namespace Platform.IO
{
	// Token: 0x0200033E RID: 830
	public static class FileAttributesExtensions
	{
		// Token: 0x06001F2D RID: 7981 RVA: 0x000989C4 File Offset: 0x00096BC4
		public static FileAttributes CombineFlags(this FileAttributes a, FileAttributes b)
		{
			return a | b;
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x000989C9 File Offset: 0x00096BC9
		public static bool IsSubset(this FileAttributes superset, FileAttributes subset)
		{
			return (superset & subset) == superset;
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x000989D1 File Offset: 0x00096BD1
		public static bool IsSubset(this FileAttributes superset, FileAttributes subset)
		{
			return (superset & subset) == superset;
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x000989DC File Offset: 0x00096BDC
		public static FileAttributes FromSystemIO(FileAttributes attributes)
		{
			FileAttributes fileAttributes = FileAttributes.None;
			if (attributes.IsSubset(FileAttributes.ReadOnly))
			{
				fileAttributes = fileAttributes.CombineFlags(FileAttributes.ReadOnly);
			}
			if (attributes.IsSubset(FileAttributes.Hidden))
			{
				fileAttributes = fileAttributes.CombineFlags(FileAttributes.Hidden);
			}
			if (attributes.IsSubset(FileAttributes.ReadOnly))
			{
				fileAttributes = fileAttributes.CombineFlags(FileAttributes.ReadOnly);
			}
			if (attributes.IsSubset(FileAttributes.Directory))
			{
				fileAttributes = fileAttributes.CombineFlags(FileAttributes.Directory);
			}
			return fileAttributes;
		}
	}
}
