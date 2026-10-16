using System;
using System.Collections.Generic;

namespace XRL.UI.Framework
{
	// Token: 0x0200151D RID: 5405
	public class ScrollContext<Data, Context> : AbstractScrollContext where Context : NavigationContext
	{
		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x0600D273 RID: 53875 RVA: 0x004E1E07 File Offset: 0x004E0007
		public override int length
		{
			get
			{
				return this.data.Count;
			}
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x0600D274 RID: 53876 RVA: 0x004E1E14 File Offset: 0x004E0014
		public override int rowWidth
		{
			get
			{
				return this.calculateGridWidth();
			}
		}

		// Token: 0x0600D275 RID: 53877 RVA: 0x004E1E24 File Offset: 0x004E0024
		public override NavigationContext GetContextAt(int index)
		{
			if (this.contexts.Count <= index)
			{
				return null;
			}
			ScrollChildContext scrollChildContext = this.contexts[index] as ScrollChildContext;
			if (scrollChildContext != null)
			{
				scrollChildContext.index = index;
			}
			return this.contexts[index];
		}

		// Token: 0x0600D276 RID: 53878 RVA: 0x004E1E73 File Offset: 0x004E0073
		public virtual Data GetDataAt(int index)
		{
			return this.data[index];
		}

		// Token: 0x0400536C RID: 21356
		public List<Data> data = new List<Data>();

		// Token: 0x0400536D RID: 21357
		public List<Context> contexts = new List<Context>();

		// Token: 0x0400536E RID: 21358
		public Func<int> calculateGridWidth = () => -1;
	}
}
