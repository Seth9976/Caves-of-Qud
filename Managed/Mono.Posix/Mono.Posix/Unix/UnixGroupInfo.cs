using System;
using System.Collections;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x02000018 RID: 24
	public sealed class UnixGroupInfo
	{
		// Token: 0x0600011D RID: 285 RVA: 0x0000533C File Offset: 0x0000353C
		public UnixGroupInfo(string group)
		{
			this.group = new Group();
			Group group2;
			if (Syscall.getgrnam_r(group, this.group, out group2) != 0 || group2 == null)
			{
				throw new ArgumentException(Locale.GetText("invalid group name"), "group");
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005388 File Offset: 0x00003588
		public UnixGroupInfo(long group)
		{
			this.group = new Group();
			Group group2;
			if (Syscall.getgrgid_r(Convert.ToUInt32(group), this.group, out group2) != 0 || group2 == null)
			{
				throw new ArgumentException(Locale.GetText("invalid group id"), "group");
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000053D9 File Offset: 0x000035D9
		public UnixGroupInfo(Group group)
		{
			this.group = UnixGroupInfo.CopyGroup(group);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000053ED File Offset: 0x000035ED
		private static Group CopyGroup(Group group)
		{
			return new Group
			{
				gr_gid = group.gr_gid,
				gr_mem = group.gr_mem,
				gr_name = group.gr_name,
				gr_passwd = group.gr_passwd
			};
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00005424 File Offset: 0x00003624
		public string GroupName
		{
			get
			{
				return this.group.gr_name;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00005431 File Offset: 0x00003631
		public string Password
		{
			get
			{
				return this.group.gr_passwd;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000123 RID: 291 RVA: 0x0000543E File Offset: 0x0000363E
		public long GroupId
		{
			get
			{
				return (long)((ulong)this.group.gr_gid);
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000544C File Offset: 0x0000364C
		public UnixUserInfo[] GetMembers()
		{
			ArrayList arrayList = new ArrayList(this.group.gr_mem.Length);
			for (int i = 0; i < this.group.gr_mem.Length; i++)
			{
				try
				{
					arrayList.Add(new UnixUserInfo(this.group.gr_mem[i]));
				}
				catch (ArgumentException)
				{
				}
			}
			return (UnixUserInfo[])arrayList.ToArray(typeof(UnixUserInfo));
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000054C8 File Offset: 0x000036C8
		public string[] GetMemberNames()
		{
			return (string[])this.group.gr_mem.Clone();
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000054DF File Offset: 0x000036DF
		public override int GetHashCode()
		{
			return this.group.GetHashCode();
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000054EC File Offset: 0x000036EC
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.group.Equals(((UnixGroupInfo)obj).group);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000551C File Offset: 0x0000371C
		public override string ToString()
		{
			return this.group.ToString();
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005529 File Offset: 0x00003729
		public Group ToGroup()
		{
			return UnixGroupInfo.CopyGroup(this.group);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005538 File Offset: 0x00003738
		public static UnixGroupInfo[] GetLocalGroups()
		{
			ArrayList arrayList = new ArrayList();
			object grp_lock = Syscall.grp_lock;
			lock (grp_lock)
			{
				if (Syscall.setgrent() != 0)
				{
					UnixMarshal.ThrowExceptionForLastError();
				}
				try
				{
					Group group;
					while ((group = Syscall.getgrent()) != null)
					{
						arrayList.Add(new UnixGroupInfo(group));
					}
					if (Stdlib.GetLastError() != (Errno)0)
					{
						UnixMarshal.ThrowExceptionForLastError();
					}
				}
				finally
				{
					Syscall.endgrent();
				}
			}
			return (UnixGroupInfo[])arrayList.ToArray(typeof(UnixGroupInfo));
		}

		// Token: 0x04000075 RID: 117
		private Group group;
	}
}
