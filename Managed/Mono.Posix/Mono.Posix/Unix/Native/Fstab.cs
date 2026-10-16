using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000066 RID: 102
	public sealed class Fstab : IEquatable<Fstab>
	{
		// Token: 0x0600043A RID: 1082 RVA: 0x0000AF48 File Offset: 0x00009148
		public override int GetHashCode()
		{
			return this.fs_spec.GetHashCode() ^ this.fs_file.GetHashCode() ^ this.fs_vfstype.GetHashCode() ^ this.fs_mntops.GetHashCode() ^ this.fs_type.GetHashCode() ^ this.fs_freq ^ this.fs_passno;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000AFA0 File Offset: 0x000091A0
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			Fstab fstab = (Fstab)obj;
			return this.Equals(fstab);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000AFD4 File Offset: 0x000091D4
		public bool Equals(Fstab value)
		{
			return !(value == null) && (value.fs_spec == this.fs_spec && value.fs_file == this.fs_file && value.fs_vfstype == this.fs_vfstype && value.fs_mntops == this.fs_mntops && value.fs_type == this.fs_type && value.fs_freq == this.fs_freq) && value.fs_passno == this.fs_passno;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000B069 File Offset: 0x00009269
		public override string ToString()
		{
			return this.fs_spec;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000B071 File Offset: 0x00009271
		public static bool operator ==(Fstab lhs, Fstab rhs)
		{
			return object.Equals(lhs, rhs);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000B07A File Offset: 0x0000927A
		public static bool operator !=(Fstab lhs, Fstab rhs)
		{
			return !object.Equals(lhs, rhs);
		}

		// Token: 0x04000473 RID: 1139
		public string fs_spec;

		// Token: 0x04000474 RID: 1140
		public string fs_file;

		// Token: 0x04000475 RID: 1141
		public string fs_vfstype;

		// Token: 0x04000476 RID: 1142
		public string fs_mntops;

		// Token: 0x04000477 RID: 1143
		public string fs_type;

		// Token: 0x04000478 RID: 1144
		public int fs_freq;

		// Token: 0x04000479 RID: 1145
		public int fs_passno;
	}
}
