using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200005A RID: 90
	[Map("struct utimbuf")]
	public struct Utimbuf : IEquatable<Utimbuf>
	{
		// Token: 0x06000410 RID: 1040 RVA: 0x0000A862 File Offset: 0x00008A62
		public override int GetHashCode()
		{
			return this.actime.GetHashCode() ^ this.modtime.GetHashCode();
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000A87C File Offset: 0x00008A7C
		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != base.GetType())
			{
				return false;
			}
			Utimbuf utimbuf = (Utimbuf)obj;
			return utimbuf.actime == this.actime && utimbuf.modtime == this.modtime;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000A8D0 File Offset: 0x00008AD0
		public bool Equals(Utimbuf value)
		{
			return value.actime == this.actime && value.modtime == this.modtime;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000A8F0 File Offset: 0x00008AF0
		public static bool operator ==(Utimbuf lhs, Utimbuf rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000A8FA File Offset: 0x00008AFA
		public static bool operator !=(Utimbuf lhs, Utimbuf rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x04000445 RID: 1093
		[time_t]
		public long actime;

		// Token: 0x04000446 RID: 1094
		[time_t]
		public long modtime;
	}
}
