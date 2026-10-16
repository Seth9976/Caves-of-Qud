using System;

namespace FuzzySharp.Edits
{
	// Token: 0x020001D4 RID: 468
	public class OpCode
	{
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060010A7 RID: 4263 RVA: 0x0006600E File Offset: 0x0006420E
		// (set) Token: 0x060010A8 RID: 4264 RVA: 0x00066016 File Offset: 0x00064216
		public EditType EditType { get; set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060010A9 RID: 4265 RVA: 0x0006601F File Offset: 0x0006421F
		// (set) Token: 0x060010AA RID: 4266 RVA: 0x00066027 File Offset: 0x00064227
		public int SourceBegin { get; set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060010AB RID: 4267 RVA: 0x00066030 File Offset: 0x00064230
		// (set) Token: 0x060010AC RID: 4268 RVA: 0x00066038 File Offset: 0x00064238
		public int SourceEnd { get; set; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060010AD RID: 4269 RVA: 0x00066041 File Offset: 0x00064241
		// (set) Token: 0x060010AE RID: 4270 RVA: 0x00066049 File Offset: 0x00064249
		public int DestBegin { get; set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060010AF RID: 4271 RVA: 0x00066052 File Offset: 0x00064252
		// (set) Token: 0x060010B0 RID: 4272 RVA: 0x0006605A File Offset: 0x0006425A
		public int DestEnd { get; set; }

		// Token: 0x060010B1 RID: 4273 RVA: 0x00066064 File Offset: 0x00064264
		public override string ToString()
		{
			return string.Format("{0}({1},{2},{3},{4})", new object[] { this.EditType, this.SourceBegin, this.SourceEnd, this.DestBegin, this.DestEnd });
		}
	}
}
