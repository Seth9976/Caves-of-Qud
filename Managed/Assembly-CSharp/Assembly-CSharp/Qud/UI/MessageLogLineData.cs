using System;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x02000470 RID: 1136
	public class MessageLogLineData : PooledFrameworkDataElement<MessageLogLineData>
	{
		// Token: 0x06002B4C RID: 11084 RVA: 0x000E6739 File Offset: 0x000E4939
		public MessageLogLineData set(string text)
		{
			this.text = text;
			this.sortText = text.ToLower();
			return this;
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x000E674F File Offset: 0x000E494F
		public override void free()
		{
			this.text = null;
			this.sortText = null;
			base.free();
		}

		// Token: 0x04001771 RID: 6001
		public string text;

		// Token: 0x04001772 RID: 6002
		public string sortText;
	}
}
