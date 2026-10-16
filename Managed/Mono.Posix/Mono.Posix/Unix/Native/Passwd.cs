using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000068 RID: 104
	public sealed class Passwd : IEquatable<Passwd>
	{
		// Token: 0x06000449 RID: 1097 RVA: 0x0000B2A4 File Offset: 0x000094A4
		public override int GetHashCode()
		{
			return this.pw_name.GetHashCode() ^ this.pw_passwd.GetHashCode() ^ this.pw_uid.GetHashCode() ^ this.pw_gid.GetHashCode() ^ this.pw_gecos.GetHashCode() ^ this.pw_dir.GetHashCode() ^ this.pw_dir.GetHashCode() ^ this.pw_shell.GetHashCode();
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000B310 File Offset: 0x00009510
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			Passwd passwd = (Passwd)obj;
			return this.Equals(passwd);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000B344 File Offset: 0x00009544
		public bool Equals(Passwd value)
		{
			return !(value == null) && (value.pw_uid == this.pw_uid && value.pw_gid == this.pw_gid && value.pw_name == this.pw_name && value.pw_passwd == this.pw_passwd && value.pw_gecos == this.pw_gecos && value.pw_dir == this.pw_dir) && value.pw_shell == this.pw_shell;
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override string ToString()
		{
			return string.Format("{0}:{1}:{2}:{3}:{4}:{5}:{6}", new object[] { this.pw_name, this.pw_passwd, this.pw_uid, this.pw_gid, this.pw_gecos, this.pw_dir, this.pw_shell });
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000B43E File Offset: 0x0000963E
		public static bool operator ==(Passwd lhs, Passwd rhs)
		{
			return object.Equals(lhs, rhs);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000B447 File Offset: 0x00009647
		public static bool operator !=(Passwd lhs, Passwd rhs)
		{
			return !object.Equals(lhs, rhs);
		}

		// Token: 0x0400047E RID: 1150
		public string pw_name;

		// Token: 0x0400047F RID: 1151
		public string pw_passwd;

		// Token: 0x04000480 RID: 1152
		[CLSCompliant(false)]
		public uint pw_uid;

		// Token: 0x04000481 RID: 1153
		[CLSCompliant(false)]
		public uint pw_gid;

		// Token: 0x04000482 RID: 1154
		public string pw_gecos;

		// Token: 0x04000483 RID: 1155
		public string pw_dir;

		// Token: 0x04000484 RID: 1156
		public string pw_shell;
	}
}
