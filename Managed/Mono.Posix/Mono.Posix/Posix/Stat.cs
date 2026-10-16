using System;

namespace Mono.Posix
{
	// Token: 0x0200009F RID: 159
	[Obsolete("Use Mono.Unix.Native.Stat")]
	public struct Stat
	{
		// Token: 0x06000763 RID: 1891 RVA: 0x000103F4 File Offset: 0x0000E5F4
		[Obsolete("Use Mono.Unix.Native.NativeConvert.ToDateTime")]
		public static DateTime UnixToDateTime(long unix)
		{
			return Stat.UnixEpoch.Add(TimeSpan.FromSeconds((double)unix)).ToLocalTime();
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0001041C File Offset: 0x0000E61C
		internal Stat(int device, int inode, int mode, int nlinks, int uid, int gid, int rdev, long size, long blksize, long blocks, long atime, long mtime, long ctime)
		{
			this.Device = device;
			this.INode = inode;
			this.Mode = (StatMode)mode;
			this.NLinks = nlinks;
			this.Uid = uid;
			this.Gid = gid;
			this.DeviceType = (long)rdev;
			this.Size = size;
			this.BlockSize = blksize;
			this.Blocks = blocks;
			if (atime != 0L)
			{
				this.ATime = Stat.UnixToDateTime(atime);
			}
			else
			{
				this.ATime = default(DateTime);
			}
			if (mtime != 0L)
			{
				this.MTime = Stat.UnixToDateTime(mtime);
			}
			else
			{
				this.MTime = default(DateTime);
			}
			if (ctime != 0L)
			{
				this.CTime = Stat.UnixToDateTime(ctime);
				return;
			}
			this.CTime = default(DateTime);
		}

		// Token: 0x04000552 RID: 1362
		[Obsolete("Use Mono.Unix.Native.Stat.st_dev")]
		public readonly int Device;

		// Token: 0x04000553 RID: 1363
		[Obsolete("Use Mono.Unix.Native.Stat.st_ino")]
		public readonly int INode;

		// Token: 0x04000554 RID: 1364
		[Obsolete("Use Mono.Unix.Native.Stat.st_mode")]
		public readonly StatMode Mode;

		// Token: 0x04000555 RID: 1365
		[Obsolete("Use Mono.Unix.Native.Stat.st_nlink")]
		public readonly int NLinks;

		// Token: 0x04000556 RID: 1366
		[Obsolete("Use Mono.Unix.Native.Stat.st_uid")]
		public readonly int Uid;

		// Token: 0x04000557 RID: 1367
		[Obsolete("Use Mono.Unix.Native.Stat.st_gid")]
		public readonly int Gid;

		// Token: 0x04000558 RID: 1368
		[Obsolete("Use Mono.Unix.Native.Stat.st_rdev")]
		public readonly long DeviceType;

		// Token: 0x04000559 RID: 1369
		[Obsolete("Use Mono.Unix.Native.Stat.st_size")]
		public readonly long Size;

		// Token: 0x0400055A RID: 1370
		[Obsolete("Use Mono.Unix.Native.Stat.st_blksize")]
		public readonly long BlockSize;

		// Token: 0x0400055B RID: 1371
		[Obsolete("Use Mono.Unix.Native.Stat.st_blocks")]
		public readonly long Blocks;

		// Token: 0x0400055C RID: 1372
		[Obsolete("Use Mono.Unix.Native.Stat.st_atime")]
		public readonly DateTime ATime;

		// Token: 0x0400055D RID: 1373
		[Obsolete("Use Mono.Unix.Native.Stat.st_mtime")]
		public readonly DateTime MTime;

		// Token: 0x0400055E RID: 1374
		[Obsolete("Use Mono.Unix.Native.Stat.st_ctime")]
		public readonly DateTime CTime;

		// Token: 0x0400055F RID: 1375
		[Obsolete("Use Mono.Unix.Native.NativeConvert.LocalUnixEpoch")]
		public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1);
	}
}
