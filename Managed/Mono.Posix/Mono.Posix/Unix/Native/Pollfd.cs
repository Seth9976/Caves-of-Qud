using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000055 RID: 85
	[Map("struct pollfd")]
	public struct Pollfd : IEquatable<Pollfd>
	{
		// Token: 0x060003F1 RID: 1009 RVA: 0x0000A070 File Offset: 0x00008270
		public override int GetHashCode()
		{
			return this.events.GetHashCode() ^ this.revents.GetHashCode();
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000A098 File Offset: 0x00008298
		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != base.GetType())
			{
				return false;
			}
			Pollfd pollfd = (Pollfd)obj;
			return pollfd.events == this.events && pollfd.revents == this.revents;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000A0EC File Offset: 0x000082EC
		public bool Equals(Pollfd value)
		{
			return value.events == this.events && value.revents == this.revents;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000A10C File Offset: 0x0000830C
		public static bool operator ==(Pollfd lhs, Pollfd rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000A116 File Offset: 0x00008316
		public static bool operator !=(Pollfd lhs, Pollfd rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x04000422 RID: 1058
		public int fd;

		// Token: 0x04000423 RID: 1059
		[CLSCompliant(false)]
		public PollEvents events;

		// Token: 0x04000424 RID: 1060
		[CLSCompliant(false)]
		public PollEvents revents;
	}
}
