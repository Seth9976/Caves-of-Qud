using System;
using System.IO;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x02000017 RID: 23
	public abstract class UnixFileSystemInfo
	{
		// Token: 0x060000DD RID: 221 RVA: 0x00004C34 File Offset: 0x00002E34
		protected UnixFileSystemInfo(string path)
		{
			UnixPath.CheckPath(path);
			this.originalPath = path;
			this.fullPath = UnixPath.GetFullPath(path);
			this.Refresh(true);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004C5C File Offset: 0x00002E5C
		internal UnixFileSystemInfo(string path, Stat stat)
		{
			this.originalPath = path;
			this.fullPath = UnixPath.GetFullPath(path);
			this.stat = stat;
			this.valid = true;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00004C85 File Offset: 0x00002E85
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x00004C8D File Offset: 0x00002E8D
		protected string FullPath
		{
			get
			{
				return this.fullPath;
			}
			set
			{
				if (this.fullPath != value)
				{
					UnixPath.CheckPath(value);
					this.valid = false;
					this.fullPath = value;
				}
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00004CB1 File Offset: 0x00002EB1
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00004CB9 File Offset: 0x00002EB9
		protected string OriginalPath
		{
			get
			{
				return this.originalPath;
			}
			set
			{
				this.originalPath = value;
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004CC2 File Offset: 0x00002EC2
		private void AssertValid()
		{
			this.Refresh(false);
			if (!this.valid)
			{
				throw new InvalidOperationException("Path doesn't exist!");
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00004CDE File Offset: 0x00002EDE
		public virtual string FullName
		{
			get
			{
				return this.FullPath;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000E5 RID: 229
		public abstract string Name { get; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00004CE6 File Offset: 0x00002EE6
		public bool Exists
		{
			get
			{
				this.Refresh(true);
				return this.valid;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00004CF5 File Offset: 0x00002EF5
		public long Device
		{
			get
			{
				this.AssertValid();
				return Convert.ToInt64(this.stat.st_dev);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00004D0D File Offset: 0x00002F0D
		public long Inode
		{
			get
			{
				this.AssertValid();
				return Convert.ToInt64(this.stat.st_ino);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00004D25 File Offset: 0x00002F25
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00004D38 File Offset: 0x00002F38
		[CLSCompliant(false)]
		public FilePermissions Protection
		{
			get
			{
				this.AssertValid();
				return this.stat.st_mode;
			}
			set
			{
				UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.chmod(this.FullPath, value));
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00004D4B File Offset: 0x00002F4B
		public FileTypes FileType
		{
			get
			{
				this.AssertValid();
				return (FileTypes)(this.stat.st_mode & FilePermissions.S_IFMT);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00004D64 File Offset: 0x00002F64
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00004D80 File Offset: 0x00002F80
		public FileAccessPermissions FileAccessPermissions
		{
			get
			{
				this.AssertValid();
				return (FileAccessPermissions)(this.stat.st_mode & FilePermissions.ACCESSPERMS);
			}
			set
			{
				this.AssertValid();
				int num = (int)this.stat.st_mode;
				num &= -512;
				num |= (int)value;
				this.Protection = (FilePermissions)num;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00004DB2 File Offset: 0x00002FB2
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00004DCC File Offset: 0x00002FCC
		public FileSpecialAttributes FileSpecialAttributes
		{
			get
			{
				this.AssertValid();
				return (FileSpecialAttributes)(this.stat.st_mode & (FilePermissions.S_ISUID | FilePermissions.S_ISGID | FilePermissions.S_ISVTX));
			}
			set
			{
				this.AssertValid();
				int num = (int)this.stat.st_mode;
				num &= -3585;
				num |= (int)value;
				this.Protection = (FilePermissions)num;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00004DFE File Offset: 0x00002FFE
		public long LinkCount
		{
			get
			{
				this.AssertValid();
				return Convert.ToInt64(this.stat.st_nlink);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00004E16 File Offset: 0x00003016
		public UnixUserInfo OwnerUser
		{
			get
			{
				this.AssertValid();
				return new UnixUserInfo(this.stat.st_uid);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00004E2E File Offset: 0x0000302E
		public long OwnerUserId
		{
			get
			{
				this.AssertValid();
				return (long)((ulong)this.stat.st_uid);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00004E42 File Offset: 0x00003042
		public UnixGroupInfo OwnerGroup
		{
			get
			{
				this.AssertValid();
				return new UnixGroupInfo((long)((ulong)this.stat.st_gid));
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00004E5B File Offset: 0x0000305B
		public long OwnerGroupId
		{
			get
			{
				this.AssertValid();
				return (long)((ulong)this.stat.st_gid);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00004E6F File Offset: 0x0000306F
		public long DeviceType
		{
			get
			{
				this.AssertValid();
				return Convert.ToInt64(this.stat.st_rdev);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00004E87 File Offset: 0x00003087
		public long Length
		{
			get
			{
				this.AssertValid();
				return this.stat.st_size;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00004E9A File Offset: 0x0000309A
		public long BlockSize
		{
			get
			{
				this.AssertValid();
				return this.stat.st_blksize;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00004EAD File Offset: 0x000030AD
		public long BlocksAllocated
		{
			get
			{
				this.AssertValid();
				return this.stat.st_blocks;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00004EC0 File Offset: 0x000030C0
		public DateTime LastAccessTime
		{
			get
			{
				this.AssertValid();
				return NativeConvert.ToDateTime(this.stat.st_atime, this.stat.st_atime_nsec);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00004EE4 File Offset: 0x000030E4
		public DateTime LastAccessTimeUtc
		{
			get
			{
				return this.LastAccessTime.ToUniversalTime();
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00004EFF File Offset: 0x000030FF
		public DateTime LastWriteTime
		{
			get
			{
				this.AssertValid();
				return NativeConvert.ToDateTime(this.stat.st_mtime, this.stat.st_mtime_nsec);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00004F24 File Offset: 0x00003124
		public DateTime LastWriteTimeUtc
		{
			get
			{
				return this.LastWriteTime.ToUniversalTime();
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00004F3F File Offset: 0x0000313F
		public DateTime LastStatusChangeTime
		{
			get
			{
				this.AssertValid();
				return NativeConvert.ToDateTime(this.stat.st_ctime, this.stat.st_ctime_nsec);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00004F64 File Offset: 0x00003164
		public DateTime LastStatusChangeTimeUtc
		{
			get
			{
				return this.LastStatusChangeTime.ToUniversalTime();
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00004F7F File Offset: 0x0000317F
		public bool IsDirectory
		{
			get
			{
				this.AssertValid();
				return UnixFileSystemInfo.IsFileType(this.stat.st_mode, FilePermissions.S_IFDIR);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00004F9C File Offset: 0x0000319C
		public bool IsCharacterDevice
		{
			get
			{
				this.AssertValid();
				return UnixFileSystemInfo.IsFileType(this.stat.st_mode, FilePermissions.S_IFCHR);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00004FB9 File Offset: 0x000031B9
		public bool IsBlockDevice
		{
			get
			{
				this.AssertValid();
				return UnixFileSystemInfo.IsFileType(this.stat.st_mode, FilePermissions.S_IFBLK);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00004FD6 File Offset: 0x000031D6
		public bool IsRegularFile
		{
			get
			{
				this.AssertValid();
				return UnixFileSystemInfo.IsFileType(this.stat.st_mode, FilePermissions.S_IFREG);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00004FF3 File Offset: 0x000031F3
		public bool IsFifo
		{
			get
			{
				this.AssertValid();
				return UnixFileSystemInfo.IsFileType(this.stat.st_mode, FilePermissions.S_IFIFO);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00005010 File Offset: 0x00003210
		public bool IsSymbolicLink
		{
			get
			{
				this.AssertValid();
				return UnixFileSystemInfo.IsFileType(this.stat.st_mode, FilePermissions.S_IFLNK);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000502D File Offset: 0x0000322D
		public bool IsSocket
		{
			get
			{
				this.AssertValid();
				return UnixFileSystemInfo.IsFileType(this.stat.st_mode, FilePermissions.S_IFSOCK);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000106 RID: 262 RVA: 0x0000504A File Offset: 0x0000324A
		public bool IsSetUser
		{
			get
			{
				this.AssertValid();
				return UnixFileSystemInfo.IsSet(this.stat.st_mode, FilePermissions.S_ISUID);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00005067 File Offset: 0x00003267
		public bool IsSetGroup
		{
			get
			{
				this.AssertValid();
				return UnixFileSystemInfo.IsSet(this.stat.st_mode, FilePermissions.S_ISGID);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00005084 File Offset: 0x00003284
		public bool IsSticky
		{
			get
			{
				this.AssertValid();
				return UnixFileSystemInfo.IsSet(this.stat.st_mode, FilePermissions.S_ISVTX);
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000050A1 File Offset: 0x000032A1
		internal static bool IsFileType(FilePermissions mode, FilePermissions type)
		{
			return (mode & FilePermissions.S_IFMT) == type;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000050AD File Offset: 0x000032AD
		internal static bool IsSet(FilePermissions mode, FilePermissions type)
		{
			return (mode & type) == type;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000050B5 File Offset: 0x000032B5
		[CLSCompliant(false)]
		public bool CanAccess(AccessModes mode)
		{
			return Syscall.access(this.FullPath, mode) == 0;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000050C6 File Offset: 0x000032C6
		public UnixFileSystemInfo CreateLink(string path)
		{
			UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.link(this.FullName, path));
			return UnixFileSystemInfo.GetFileSystemEntry(path);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000050DF File Offset: 0x000032DF
		public UnixSymbolicLinkInfo CreateSymbolicLink(string path)
		{
			UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.symlink(this.FullName, path));
			return new UnixSymbolicLinkInfo(path);
		}

		// Token: 0x0600010E RID: 270
		public abstract void Delete();

		// Token: 0x0600010F RID: 271 RVA: 0x000050F8 File Offset: 0x000032F8
		[CLSCompliant(false)]
		public long GetConfigurationValue(PathconfName name)
		{
			long num = Syscall.pathconf(this.FullPath, name);
			if (num == -1L && Stdlib.GetLastError() != (Errno)0)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			return num;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00005117 File Offset: 0x00003317
		public void Refresh()
		{
			this.Refresh(true);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005120 File Offset: 0x00003320
		internal void Refresh(bool force)
		{
			if (this.valid && !force)
			{
				return;
			}
			this.valid = this.GetFileStatus(this.FullPath, out this.stat);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005146 File Offset: 0x00003346
		protected virtual bool GetFileStatus(string path, out Stat stat)
		{
			return Syscall.stat(path, out stat) == 0;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005154 File Offset: 0x00003354
		public void SetLength(long length)
		{
			int num;
			do
			{
				num = Syscall.truncate(this.FullPath, length);
			}
			while (UnixMarshal.ShouldRetrySyscall(num));
			UnixMarshal.ThrowExceptionForLastErrorIf(num);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000517C File Offset: 0x0000337C
		public virtual void SetOwner(long owner, long group)
		{
			int num = Convert.ToInt32(owner);
			int num2 = Convert.ToInt32(group);
			UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.chown(this.FullPath, num, num2));
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000051AC File Offset: 0x000033AC
		public void SetOwner(string owner)
		{
			Passwd passwd = Syscall.getpwnam(owner);
			if (passwd == null)
			{
				throw new ArgumentException(Locale.GetText("invalid username"), "owner");
			}
			uint pw_uid = passwd.pw_uid;
			uint pw_gid = passwd.pw_gid;
			this.SetOwner((long)((ulong)pw_uid), (long)((ulong)pw_gid));
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000051F4 File Offset: 0x000033F4
		public void SetOwner(string owner, string group)
		{
			long num = -1L;
			if (owner != null)
			{
				num = new UnixUserInfo(owner).UserId;
			}
			long num2 = -1L;
			if (group != null)
			{
				num2 = new UnixGroupInfo(group).GroupId;
			}
			this.SetOwner(num, num2);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005230 File Offset: 0x00003430
		public void SetOwner(UnixUserInfo owner)
		{
			long num2;
			long num = (num2 = -1L);
			if (owner != null)
			{
				num2 = owner.UserId;
				num = owner.GroupId;
			}
			this.SetOwner(num2, num);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000525C File Offset: 0x0000345C
		public void SetOwner(UnixUserInfo owner, UnixGroupInfo group)
		{
			long num2;
			long num = (num2 = -1L);
			if (owner != null)
			{
				num2 = owner.UserId;
			}
			if (group != null)
			{
				num = owner.GroupId;
			}
			this.SetOwner(num2, num);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000528A File Offset: 0x0000348A
		public override string ToString()
		{
			return this.FullPath;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005292 File Offset: 0x00003492
		public Stat ToStat()
		{
			this.AssertValid();
			return this.stat;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000052A0 File Offset: 0x000034A0
		public static UnixFileSystemInfo GetFileSystemEntry(string path)
		{
			UnixFileSystemInfo unixFileSystemInfo;
			if (UnixFileSystemInfo.TryGetFileSystemEntry(path, out unixFileSystemInfo))
			{
				return unixFileSystemInfo;
			}
			UnixMarshal.ThrowExceptionForLastError();
			throw new DirectoryNotFoundException("UnixMarshal.ThrowExceptionForLastError didn't throw?!");
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000052C8 File Offset: 0x000034C8
		public static bool TryGetFileSystemEntry(string path, out UnixFileSystemInfo entry)
		{
			Stat stat;
			if (Syscall.lstat(path, out stat) != -1)
			{
				if (UnixFileSystemInfo.IsFileType(stat.st_mode, FilePermissions.S_IFDIR))
				{
					entry = new UnixDirectoryInfo(path, stat);
				}
				else if (UnixFileSystemInfo.IsFileType(stat.st_mode, FilePermissions.S_IFLNK))
				{
					entry = new UnixSymbolicLinkInfo(path, stat);
				}
				else
				{
					entry = new UnixFileInfo(path, stat);
				}
				return true;
			}
			if (Stdlib.GetLastError() == Errno.ENOENT)
			{
				entry = new UnixFileInfo(path);
				return true;
			}
			entry = null;
			return false;
		}

		// Token: 0x0400006F RID: 111
		private Stat stat;

		// Token: 0x04000070 RID: 112
		private string fullPath;

		// Token: 0x04000071 RID: 113
		private string originalPath;

		// Token: 0x04000072 RID: 114
		private bool valid;

		// Token: 0x04000073 RID: 115
		internal const FileSpecialAttributes AllSpecialAttributes = FileSpecialAttributes.SetUserId | FileSpecialAttributes.SetGroupId | FileSpecialAttributes.Sticky;

		// Token: 0x04000074 RID: 116
		internal const FileTypes AllFileTypes = (FileTypes)61440;
	}
}
