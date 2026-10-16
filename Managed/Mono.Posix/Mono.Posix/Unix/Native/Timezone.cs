using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000059 RID: 89
	[Map("struct timezone")]
	public struct Timezone : IEquatable<Timezone>
	{
		// Token: 0x0600040B RID: 1035 RVA: 0x0000A7F7 File Offset: 0x000089F7
		public override int GetHashCode()
		{
			return this.tz_minuteswest.GetHashCode();
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000A804 File Offset: 0x00008A04
		public override bool Equals(object obj)
		{
			return obj != null && !(obj.GetType() != base.GetType()) && ((Timezone)obj).tz_minuteswest == this.tz_minuteswest;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000A83B File Offset: 0x00008A3B
		public bool Equals(Timezone value)
		{
			return value.tz_minuteswest == this.tz_minuteswest;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000A84B File Offset: 0x00008A4B
		public static bool operator ==(Timezone lhs, Timezone rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000A855 File Offset: 0x00008A55
		public static bool operator !=(Timezone lhs, Timezone rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x04000443 RID: 1091
		public int tz_minuteswest;

		// Token: 0x04000444 RID: 1092
		private int tz_dsttime;
	}
}
