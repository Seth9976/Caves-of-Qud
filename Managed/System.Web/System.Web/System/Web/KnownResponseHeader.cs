using System;

namespace System.Web
{
	// Token: 0x020000A9 RID: 169
	internal sealed class KnownResponseHeader : BaseResponseHeader
	{
		// Token: 0x060008FB RID: 2299 RVA: 0x00016037 File Offset: 0x00014237
		internal KnownResponseHeader(int ID, string val)
			: base(val)
		{
			this.ID = ID;
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00016047 File Offset: 0x00014247
		internal override void SendContent(HttpWorkerRequest wr)
		{
			wr.SendKnownResponseHeader(this.ID, base.Value);
		}

		// Token: 0x04000FEB RID: 4075
		public int ID;
	}
}
