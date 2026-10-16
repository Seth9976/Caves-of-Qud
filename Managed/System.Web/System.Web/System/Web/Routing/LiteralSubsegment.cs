using System;

namespace System.Web.Routing
{
	// Token: 0x020004DF RID: 1247
	internal sealed class LiteralSubsegment : PathSubsegment
	{
		// Token: 0x0600381E RID: 14366 RVA: 0x00094D04 File Offset: 0x00092F04
		public LiteralSubsegment(string literal)
		{
			this.Literal = literal;
		}

		// Token: 0x17001197 RID: 4503
		// (get) Token: 0x0600381F RID: 14367 RVA: 0x00094D13 File Offset: 0x00092F13
		// (set) Token: 0x06003820 RID: 14368 RVA: 0x00094D1B File Offset: 0x00092F1B
		public string Literal { get; private set; }
	}
}
