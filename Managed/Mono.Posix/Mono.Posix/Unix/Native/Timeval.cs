using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000058 RID: 88
	[Map("struct timeval")]
	public struct Timeval : IEquatable<Timeval>
	{
		// Token: 0x06000406 RID: 1030 RVA: 0x0000A753 File Offset: 0x00008953
		public override int GetHashCode()
		{
			return this.tv_sec.GetHashCode() ^ this.tv_usec.GetHashCode();
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000A76C File Offset: 0x0000896C
		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != base.GetType())
			{
				return false;
			}
			Timeval timeval = (Timeval)obj;
			return timeval.tv_sec == this.tv_sec && timeval.tv_usec == this.tv_usec;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000A7C0 File Offset: 0x000089C0
		public bool Equals(Timeval value)
		{
			return value.tv_sec == this.tv_sec && value.tv_usec == this.tv_usec;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000A7E0 File Offset: 0x000089E0
		public static bool operator ==(Timeval lhs, Timeval rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000A7EA File Offset: 0x000089EA
		public static bool operator !=(Timeval lhs, Timeval rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x04000441 RID: 1089
		[time_t]
		public long tv_sec;

		// Token: 0x04000442 RID: 1090
		[suseconds_t]
		public long tv_usec;
	}
}
