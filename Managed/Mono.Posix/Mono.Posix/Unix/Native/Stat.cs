using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000056 RID: 86
	public struct Stat : IEquatable<Stat>
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0000A124 File Offset: 0x00008324
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x0000A154 File Offset: 0x00008354
		public Timespec st_atim
		{
			get
			{
				return new Timespec
				{
					tv_sec = this.st_atime,
					tv_nsec = this.st_atime_nsec
				};
			}
			set
			{
				this.st_atime = value.tv_sec;
				this.st_atime_nsec = value.tv_nsec;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0000A170 File Offset: 0x00008370
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x0000A1A0 File Offset: 0x000083A0
		public Timespec st_mtim
		{
			get
			{
				return new Timespec
				{
					tv_sec = this.st_mtime,
					tv_nsec = this.st_mtime_nsec
				};
			}
			set
			{
				this.st_mtime = value.tv_sec;
				this.st_mtime_nsec = value.tv_nsec;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0000A1BC File Offset: 0x000083BC
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x0000A1EC File Offset: 0x000083EC
		public Timespec st_ctim
		{
			get
			{
				return new Timespec
				{
					tv_sec = this.st_ctime,
					tv_nsec = this.st_ctime_nsec
				};
			}
			set
			{
				this.st_ctime = value.tv_sec;
				this.st_ctime_nsec = value.tv_nsec;
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000A208 File Offset: 0x00008408
		public override int GetHashCode()
		{
			return this.st_dev.GetHashCode() ^ this.st_ino.GetHashCode() ^ this.st_mode.GetHashCode() ^ this.st_nlink.GetHashCode() ^ this.st_uid.GetHashCode() ^ this.st_gid.GetHashCode() ^ this.st_rdev.GetHashCode() ^ this.st_size.GetHashCode() ^ this.st_blksize.GetHashCode() ^ this.st_blocks.GetHashCode() ^ this.st_atime.GetHashCode() ^ this.st_mtime.GetHashCode() ^ this.st_ctime.GetHashCode() ^ this.st_atime_nsec.GetHashCode() ^ this.st_mtime_nsec.GetHashCode() ^ this.st_ctime_nsec.GetHashCode();
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000A2DC File Offset: 0x000084DC
		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != base.GetType())
			{
				return false;
			}
			Stat stat = (Stat)obj;
			return stat.st_dev == this.st_dev && stat.st_ino == this.st_ino && stat.st_mode == this.st_mode && stat.st_nlink == this.st_nlink && stat.st_uid == this.st_uid && stat.st_gid == this.st_gid && stat.st_rdev == this.st_rdev && stat.st_size == this.st_size && stat.st_blksize == this.st_blksize && stat.st_blocks == this.st_blocks && stat.st_atime == this.st_atime && stat.st_mtime == this.st_mtime && stat.st_ctime == this.st_ctime && stat.st_atime_nsec == this.st_atime_nsec && stat.st_mtime_nsec == this.st_mtime_nsec && stat.st_ctime_nsec == this.st_ctime_nsec;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000A408 File Offset: 0x00008608
		public bool Equals(Stat value)
		{
			return value.st_dev == this.st_dev && value.st_ino == this.st_ino && value.st_mode == this.st_mode && value.st_nlink == this.st_nlink && value.st_uid == this.st_uid && value.st_gid == this.st_gid && value.st_rdev == this.st_rdev && value.st_size == this.st_size && value.st_blksize == this.st_blksize && value.st_blocks == this.st_blocks && value.st_atime == this.st_atime && value.st_mtime == this.st_mtime && value.st_ctime == this.st_ctime && value.st_atime_nsec == this.st_atime_nsec && value.st_mtime_nsec == this.st_mtime_nsec && value.st_ctime_nsec == this.st_ctime_nsec;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000A509 File Offset: 0x00008709
		public static bool operator ==(Stat lhs, Stat rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000A513 File Offset: 0x00008713
		public static bool operator !=(Stat lhs, Stat rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x04000425 RID: 1061
		[CLSCompliant(false)]
		[dev_t]
		public ulong st_dev;

		// Token: 0x04000426 RID: 1062
		[CLSCompliant(false)]
		[ino_t]
		public ulong st_ino;

		// Token: 0x04000427 RID: 1063
		[CLSCompliant(false)]
		public FilePermissions st_mode;

		// Token: 0x04000428 RID: 1064
		[NonSerialized]
		private uint _padding_;

		// Token: 0x04000429 RID: 1065
		[CLSCompliant(false)]
		[nlink_t]
		public ulong st_nlink;

		// Token: 0x0400042A RID: 1066
		[CLSCompliant(false)]
		[uid_t]
		public uint st_uid;

		// Token: 0x0400042B RID: 1067
		[CLSCompliant(false)]
		[gid_t]
		public uint st_gid;

		// Token: 0x0400042C RID: 1068
		[CLSCompliant(false)]
		[dev_t]
		public ulong st_rdev;

		// Token: 0x0400042D RID: 1069
		[off_t]
		public long st_size;

		// Token: 0x0400042E RID: 1070
		[blksize_t]
		public long st_blksize;

		// Token: 0x0400042F RID: 1071
		[blkcnt_t]
		public long st_blocks;

		// Token: 0x04000430 RID: 1072
		[time_t]
		public long st_atime;

		// Token: 0x04000431 RID: 1073
		[time_t]
		public long st_mtime;

		// Token: 0x04000432 RID: 1074
		[time_t]
		public long st_ctime;

		// Token: 0x04000433 RID: 1075
		public long st_atime_nsec;

		// Token: 0x04000434 RID: 1076
		public long st_mtime_nsec;

		// Token: 0x04000435 RID: 1077
		public long st_ctime_nsec;
	}
}
