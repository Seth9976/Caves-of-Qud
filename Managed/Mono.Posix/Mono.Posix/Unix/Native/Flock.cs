using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000054 RID: 84
	[Map("struct flock")]
	public struct Flock : IEquatable<Flock>
	{
		// Token: 0x060003EC RID: 1004 RVA: 0x00009F30 File Offset: 0x00008130
		public override int GetHashCode()
		{
			return this.l_type.GetHashCode() ^ this.l_whence.GetHashCode() ^ this.l_start.GetHashCode() ^ this.l_len.GetHashCode() ^ this.l_pid.GetHashCode();
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00009F84 File Offset: 0x00008184
		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != base.GetType())
			{
				return false;
			}
			Flock flock = (Flock)obj;
			return this.l_type == flock.l_type && this.l_whence == flock.l_whence && this.l_start == flock.l_start && this.l_len == flock.l_len && this.l_pid == flock.l_pid;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000A004 File Offset: 0x00008204
		public bool Equals(Flock value)
		{
			return this.l_type == value.l_type && this.l_whence == value.l_whence && this.l_start == value.l_start && this.l_len == value.l_len && this.l_pid == value.l_pid;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000A059 File Offset: 0x00008259
		public static bool operator ==(Flock lhs, Flock rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000A063 File Offset: 0x00008263
		public static bool operator !=(Flock lhs, Flock rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x0400041D RID: 1053
		[CLSCompliant(false)]
		public LockType l_type;

		// Token: 0x0400041E RID: 1054
		[CLSCompliant(false)]
		public SeekFlags l_whence;

		// Token: 0x0400041F RID: 1055
		[off_t]
		public long l_start;

		// Token: 0x04000420 RID: 1056
		[off_t]
		public long l_len;

		// Token: 0x04000421 RID: 1057
		[pid_t]
		public int l_pid;
	}
}
