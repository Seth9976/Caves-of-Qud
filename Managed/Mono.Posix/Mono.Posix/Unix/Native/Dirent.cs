using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000065 RID: 101
	public sealed class Dirent : IEquatable<Dirent>
	{
		// Token: 0x06000433 RID: 1075 RVA: 0x0000AE4D File Offset: 0x0000904D
		public override int GetHashCode()
		{
			return this.d_ino.GetHashCode() ^ this.d_off.GetHashCode() ^ this.d_reclen.GetHashCode() ^ this.d_type.GetHashCode() ^ this.d_name.GetHashCode();
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000AE8C File Offset: 0x0000908C
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			Dirent dirent = (Dirent)obj;
			return this.Equals(dirent);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000AEC0 File Offset: 0x000090C0
		public bool Equals(Dirent value)
		{
			return !(value == null) && (value.d_ino == this.d_ino && value.d_off == this.d_off && value.d_reclen == this.d_reclen && value.d_type == this.d_type) && value.d_name == this.d_name;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000AF23 File Offset: 0x00009123
		public override string ToString()
		{
			return this.d_name;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000AF2B File Offset: 0x0000912B
		public static bool operator ==(Dirent lhs, Dirent rhs)
		{
			return object.Equals(lhs, rhs);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000AF34 File Offset: 0x00009134
		public static bool operator !=(Dirent lhs, Dirent rhs)
		{
			return !object.Equals(lhs, rhs);
		}

		// Token: 0x0400046E RID: 1134
		[CLSCompliant(false)]
		public ulong d_ino;

		// Token: 0x0400046F RID: 1135
		public long d_off;

		// Token: 0x04000470 RID: 1136
		[CLSCompliant(false)]
		public ushort d_reclen;

		// Token: 0x04000471 RID: 1137
		public byte d_type;

		// Token: 0x04000472 RID: 1138
		public string d_name;
	}
}
