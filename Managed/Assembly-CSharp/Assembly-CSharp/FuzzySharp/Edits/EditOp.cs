using System;

namespace FuzzySharp.Edits
{
	// Token: 0x020001D2 RID: 466
	public class EditOp
	{
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06001097 RID: 4247 RVA: 0x00065F3E File Offset: 0x0006413E
		// (set) Token: 0x06001098 RID: 4248 RVA: 0x00065F46 File Offset: 0x00064146
		public EditType EditType { get; set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06001099 RID: 4249 RVA: 0x00065F4F File Offset: 0x0006414F
		// (set) Token: 0x0600109A RID: 4250 RVA: 0x00065F57 File Offset: 0x00064157
		public int SourcePos { get; set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600109B RID: 4251 RVA: 0x00065F60 File Offset: 0x00064160
		// (set) Token: 0x0600109C RID: 4252 RVA: 0x00065F68 File Offset: 0x00064168
		public int DestPos { get; set; }

		// Token: 0x0600109D RID: 4253 RVA: 0x00065F71 File Offset: 0x00064171
		public override string ToString()
		{
			return string.Format("{0}({1}, {2})", this.EditType, this.SourcePos, this.DestPos);
		}
	}
}
