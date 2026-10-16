using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000057 RID: 87
	[Map]
	[CLSCompliant(false)]
	public struct Statvfs : IEquatable<Statvfs>
	{
		// Token: 0x06000401 RID: 1025 RVA: 0x0000A520 File Offset: 0x00008720
		public override int GetHashCode()
		{
			return this.f_bsize.GetHashCode() ^ this.f_frsize.GetHashCode() ^ this.f_blocks.GetHashCode() ^ this.f_bfree.GetHashCode() ^ this.f_bavail.GetHashCode() ^ this.f_files.GetHashCode() ^ this.f_ffree.GetHashCode() ^ this.f_favail.GetHashCode() ^ this.f_fsid.GetHashCode() ^ this.f_flag.GetHashCode() ^ this.f_namemax.GetHashCode();
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000A5B8 File Offset: 0x000087B8
		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != base.GetType())
			{
				return false;
			}
			Statvfs statvfs = (Statvfs)obj;
			return statvfs.f_bsize == this.f_bsize && statvfs.f_frsize == this.f_frsize && statvfs.f_blocks == this.f_blocks && statvfs.f_bfree == this.f_bfree && statvfs.f_bavail == this.f_bavail && statvfs.f_files == this.f_files && statvfs.f_ffree == this.f_ffree && statvfs.f_favail == this.f_favail && statvfs.f_fsid == this.f_fsid && statvfs.f_flag == this.f_flag && statvfs.f_namemax == this.f_namemax;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000A690 File Offset: 0x00008890
		public bool Equals(Statvfs value)
		{
			return value.f_bsize == this.f_bsize && value.f_frsize == this.f_frsize && value.f_blocks == this.f_blocks && value.f_bfree == this.f_bfree && value.f_bavail == this.f_bavail && value.f_files == this.f_files && value.f_ffree == this.f_ffree && value.f_favail == this.f_favail && value.f_fsid == this.f_fsid && value.f_flag == this.f_flag && value.f_namemax == this.f_namemax;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000A73C File Offset: 0x0000893C
		public static bool operator ==(Statvfs lhs, Statvfs rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000A746 File Offset: 0x00008946
		public static bool operator !=(Statvfs lhs, Statvfs rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x04000436 RID: 1078
		public ulong f_bsize;

		// Token: 0x04000437 RID: 1079
		public ulong f_frsize;

		// Token: 0x04000438 RID: 1080
		[fsblkcnt_t]
		public ulong f_blocks;

		// Token: 0x04000439 RID: 1081
		[fsblkcnt_t]
		public ulong f_bfree;

		// Token: 0x0400043A RID: 1082
		[fsblkcnt_t]
		public ulong f_bavail;

		// Token: 0x0400043B RID: 1083
		[fsfilcnt_t]
		public ulong f_files;

		// Token: 0x0400043C RID: 1084
		[fsfilcnt_t]
		public ulong f_ffree;

		// Token: 0x0400043D RID: 1085
		[fsfilcnt_t]
		public ulong f_favail;

		// Token: 0x0400043E RID: 1086
		public ulong f_fsid;

		// Token: 0x0400043F RID: 1087
		public MountFlags f_flag;

		// Token: 0x04000440 RID: 1088
		public ulong f_namemax;
	}
}
