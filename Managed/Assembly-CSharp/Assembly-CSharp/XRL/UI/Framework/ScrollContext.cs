using System;
using System.Collections.Generic;

namespace XRL.UI.Framework
{
	// Token: 0x0200151C RID: 5404
	public class ScrollContext<Context> : AbstractScrollContext where Context : NavigationContext
	{
		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x0600D270 RID: 53872 RVA: 0x004E1DC4 File Offset: 0x004DFFC4
		public override int length
		{
			get
			{
				return this.contexts.Count;
			}
		}

		// Token: 0x0600D271 RID: 53873 RVA: 0x004E1DD1 File Offset: 0x004DFFD1
		public override NavigationContext GetContextAt(int index)
		{
			if (index >= this.contexts.Count)
			{
				return null;
			}
			return this.contexts[index];
		}

		// Token: 0x0400536B RID: 21355
		public List<Context> contexts = new List<Context>();
	}
}
