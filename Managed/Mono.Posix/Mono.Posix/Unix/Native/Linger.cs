using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000061 RID: 97
	[Map("struct linger")]
	[CLSCompliant(false)]
	public struct Linger
	{
		// Token: 0x0600041A RID: 1050 RVA: 0x0000A9AB File Offset: 0x00008BAB
		public override string ToString()
		{
			return string.Format("{0}, {1}", this.l_onoff, this.l_linger);
		}

		// Token: 0x04000465 RID: 1125
		public int l_onoff;

		// Token: 0x04000466 RID: 1126
		public int l_linger;
	}
}
