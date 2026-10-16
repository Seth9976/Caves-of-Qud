using System;
using System.Text;

namespace Mono.Unix.Native
{
	// Token: 0x02000067 RID: 103
	public sealed class Group : IEquatable<Group>
	{
		// Token: 0x06000441 RID: 1089 RVA: 0x0000B090 File Offset: 0x00009290
		public override int GetHashCode()
		{
			int num = 0;
			for (int i = 0; i < this.gr_mem.Length; i++)
			{
				num ^= this.gr_mem[i].GetHashCode();
			}
			return this.gr_name.GetHashCode() ^ this.gr_passwd.GetHashCode() ^ this.gr_gid.GetHashCode() ^ num;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000B0E8 File Offset: 0x000092E8
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			Group group = (Group)obj;
			return this.Equals(group);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000B11C File Offset: 0x0000931C
		public bool Equals(Group value)
		{
			if (value == null)
			{
				return false;
			}
			if (value.gr_gid != this.gr_gid)
			{
				return false;
			}
			if (value.gr_gid != this.gr_gid || !(value.gr_name == this.gr_name) || !(value.gr_passwd == this.gr_passwd))
			{
				return false;
			}
			if (value.gr_mem == this.gr_mem)
			{
				return true;
			}
			if (value.gr_mem == null || this.gr_mem == null)
			{
				return false;
			}
			if (value.gr_mem.Length != this.gr_mem.Length)
			{
				return false;
			}
			for (int i = 0; i < this.gr_mem.Length; i++)
			{
				if (this.gr_mem[i] != value.gr_mem[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000B1E0 File Offset: 0x000093E0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.gr_name).Append(":").Append(this.gr_passwd)
				.Append(":");
			stringBuilder.Append(this.gr_gid).Append(":");
			Group.GetMembers(stringBuilder, this.gr_mem);
			return stringBuilder.ToString();
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000B248 File Offset: 0x00009448
		private static void GetMembers(StringBuilder sb, string[] members)
		{
			if (members.Length != 0)
			{
				sb.Append(members[0]);
			}
			for (int i = 1; i < members.Length; i++)
			{
				sb.Append(",");
				sb.Append(members[i]);
			}
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000B287 File Offset: 0x00009487
		public static bool operator ==(Group lhs, Group rhs)
		{
			return object.Equals(lhs, rhs);
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000B290 File Offset: 0x00009490
		public static bool operator !=(Group lhs, Group rhs)
		{
			return !object.Equals(lhs, rhs);
		}

		// Token: 0x0400047A RID: 1146
		public string gr_name;

		// Token: 0x0400047B RID: 1147
		public string gr_passwd;

		// Token: 0x0400047C RID: 1148
		[CLSCompliant(false)]
		public uint gr_gid;

		// Token: 0x0400047D RID: 1149
		public string[] gr_mem;
	}
}
