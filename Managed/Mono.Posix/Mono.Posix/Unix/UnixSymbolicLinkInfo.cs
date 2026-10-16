using System;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x02000022 RID: 34
	public sealed class UnixSymbolicLinkInfo : UnixFileSystemInfo
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x000072E4 File Offset: 0x000054E4
		public UnixSymbolicLinkInfo(string path)
			: base(path)
		{
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000072ED File Offset: 0x000054ED
		internal UnixSymbolicLinkInfo(string path, Stat stat)
			: base(path, stat)
		{
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001DA RID: 474 RVA: 0x000072F7 File Offset: 0x000054F7
		public override string Name
		{
			get
			{
				return UnixPath.GetFileName(base.FullPath);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00007304 File Offset: 0x00005504
		[Obsolete("Use GetContents()")]
		public UnixFileSystemInfo Contents
		{
			get
			{
				return this.GetContents();
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000730C File Offset: 0x0000550C
		public string ContentsPath
		{
			get
			{
				return UnixPath.ReadLink(base.FullPath);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00007319 File Offset: 0x00005519
		public bool HasContents
		{
			get
			{
				return UnixPath.TryReadLink(base.FullPath) != null;
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00007329 File Offset: 0x00005529
		public UnixFileSystemInfo GetContents()
		{
			return UnixFileSystemInfo.GetFileSystemEntry(UnixPath.Combine(UnixPath.GetDirectoryName(base.FullPath), new string[] { this.ContentsPath }));
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000734F File Offset: 0x0000554F
		public void CreateSymbolicLinkTo(string path)
		{
			UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.symlink(path, this.FullName));
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00007362 File Offset: 0x00005562
		public void CreateSymbolicLinkTo(UnixFileSystemInfo path)
		{
			UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.symlink(path.FullName, this.FullName));
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000737A File Offset: 0x0000557A
		public override void Delete()
		{
			UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.unlink(base.FullPath));
			base.Refresh();
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00007392 File Offset: 0x00005592
		public override void SetOwner(long owner, long group)
		{
			UnixMarshal.ThrowExceptionForLastErrorIf(Syscall.lchown(base.FullPath, Convert.ToInt32(owner), Convert.ToInt32(group)));
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000073B0 File Offset: 0x000055B0
		protected override bool GetFileStatus(string path, out Stat stat)
		{
			return Syscall.lstat(path, out stat) == 0;
		}
	}
}
