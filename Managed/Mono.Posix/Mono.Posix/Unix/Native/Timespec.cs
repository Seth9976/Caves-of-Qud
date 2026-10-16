using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200005B RID: 91
	[Map("struct timespec")]
	public struct Timespec : IEquatable<Timespec>
	{
		// Token: 0x06000415 RID: 1045 RVA: 0x0000A907 File Offset: 0x00008B07
		public override int GetHashCode()
		{
			return this.tv_sec.GetHashCode() ^ this.tv_nsec.GetHashCode();
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000A920 File Offset: 0x00008B20
		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != base.GetType())
			{
				return false;
			}
			Timespec timespec = (Timespec)obj;
			return timespec.tv_sec == this.tv_sec && timespec.tv_nsec == this.tv_nsec;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000A974 File Offset: 0x00008B74
		public bool Equals(Timespec value)
		{
			return value.tv_sec == this.tv_sec && value.tv_nsec == this.tv_nsec;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000A994 File Offset: 0x00008B94
		public static bool operator ==(Timespec lhs, Timespec rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000A99E File Offset: 0x00008B9E
		public static bool operator !=(Timespec lhs, Timespec rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x04000447 RID: 1095
		[time_t]
		public long tv_sec;

		// Token: 0x04000448 RID: 1096
		public long tv_nsec;
	}
}
