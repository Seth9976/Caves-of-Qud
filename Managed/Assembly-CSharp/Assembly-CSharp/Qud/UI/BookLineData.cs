using System;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x02000431 RID: 1073
	public class BookLineData : PooledFrameworkDataElement<BookLineData>
	{
		// Token: 0x060028A8 RID: 10408 RVA: 0x000D2CC9 File Offset: 0x000D0EC9
		public BookLineData set(string text)
		{
			this.text = text;
			return this;
		}

		// Token: 0x060028A9 RID: 10409 RVA: 0x000D2CD3 File Offset: 0x000D0ED3
		public override void free()
		{
			this.text = null;
			base.free();
		}

		// Token: 0x04001458 RID: 5208
		public string text;
	}
}
