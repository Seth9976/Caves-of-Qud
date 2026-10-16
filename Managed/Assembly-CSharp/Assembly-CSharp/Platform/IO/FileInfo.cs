using System;
using LaundryBear.PlatformServices;

namespace Platform.IO
{
	// Token: 0x0200033B RID: 827
	public class FileInfo : FileSystemInfo
	{
		// Token: 0x06001F23 RID: 7971 RVA: 0x000987CA File Offset: 0x000969CA
		public FileInfo(string fullPath)
			: base(fullPath)
		{
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x000987D4 File Offset: 0x000969D4
		public override void Refresh()
		{
			base.Refresh();
			this.exists = File.Exists(this.rawPath);
			if (!this.exists)
			{
				this.Length = 0L;
			}
			else
			{
				ObjectLoadResult<FileMetadata> objectLoadResult = Blob.ReadMetadata(base.FullName).ThrowIfFailed();
				this.Length = objectLoadResult.content.SizeInBytes;
			}
			this.LengthInMbs = this.Length / 1000000L;
			this.LengthInKbs = this.Length / 1000L;
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x00098855 File Offset: 0x00096A55
		public override void Delete()
		{
			File.Delete(this.rawPath);
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x00098864 File Offset: 0x00096A64
		public override string ToString()
		{
			return string.Format("{0} {1} [attribs: {2}, Size: {3}]", new object[]
			{
				base.GetType().Name,
				base.FullName,
				this.attributes,
				this.Length
			});
		}

		// Token: 0x0400100A RID: 4106
		public long Length;

		// Token: 0x0400100B RID: 4107
		public long LengthInMbs;

		// Token: 0x0400100C RID: 4108
		public long LengthInKbs;
	}
}
