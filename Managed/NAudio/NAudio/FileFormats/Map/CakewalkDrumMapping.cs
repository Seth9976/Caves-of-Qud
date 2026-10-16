using System;

namespace NAudio.FileFormats.Map
{
	// Token: 0x020000A1 RID: 161
	public class CakewalkDrumMapping
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000BE66 File Offset: 0x0000A066
		// (set) Token: 0x06000378 RID: 888 RVA: 0x0000BE6E File Offset: 0x0000A06E
		public string NoteName { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000BE77 File Offset: 0x0000A077
		// (set) Token: 0x0600037A RID: 890 RVA: 0x0000BE7F File Offset: 0x0000A07F
		public int InNote { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000BE88 File Offset: 0x0000A088
		// (set) Token: 0x0600037C RID: 892 RVA: 0x0000BE90 File Offset: 0x0000A090
		public int OutNote { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000BE99 File Offset: 0x0000A099
		// (set) Token: 0x0600037E RID: 894 RVA: 0x0000BEA1 File Offset: 0x0000A0A1
		public int OutPort { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000BEAA File Offset: 0x0000A0AA
		// (set) Token: 0x06000380 RID: 896 RVA: 0x0000BEB2 File Offset: 0x0000A0B2
		public int Channel { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000BEBB File Offset: 0x0000A0BB
		// (set) Token: 0x06000382 RID: 898 RVA: 0x0000BEC3 File Offset: 0x0000A0C3
		public int VelocityAdjust { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000BECC File Offset: 0x0000A0CC
		// (set) Token: 0x06000384 RID: 900 RVA: 0x0000BED4 File Offset: 0x0000A0D4
		public float VelocityScale { get; set; }

		// Token: 0x06000385 RID: 901 RVA: 0x0000BEE0 File Offset: 0x0000A0E0
		public override string ToString()
		{
			return string.Format("{0} In:{1} Out:{2} Ch:{3} Port:{4} Vel+:{5} Vel:{6}%", new object[]
			{
				this.NoteName,
				this.InNote,
				this.OutNote,
				this.Channel,
				this.OutPort,
				this.VelocityAdjust,
				this.VelocityScale * 100f
			});
		}
	}
}
