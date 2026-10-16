using System;

namespace XRL.UI.Framework
{
	// Token: 0x0200151E RID: 5406
	public class CyclicScrollContext<Data, Context> : ScrollContext<Data, Context> where Context : NavigationContext
	{
		// Token: 0x0600D278 RID: 53880 RVA: 0x004E1ED4 File Offset: 0x004E00D4
		public override void Setup()
		{
			for (int i = 0; i < this.contexts.Count; i++)
			{
				Context context = this.contexts[i];
				if (context != null)
				{
					context.parentContext = this;
					context.Setup();
				}
			}
			base.Setup();
		}

		// Token: 0x0600D279 RID: 53881 RVA: 0x004E1F2C File Offset: 0x004E012C
		public override NavigationContext GetContextAt(int index)
		{
			if (this.data.Count <= index)
			{
				return null;
			}
			if (this.contexts.Count == 0)
			{
				return null;
			}
			if (index % this.contexts.Count < 0 || index % this.contexts.Count >= this.contexts.Count)
			{
				return null;
			}
			ScrollChildContext scrollChildContext = this.contexts[index % this.contexts.Count] as ScrollChildContext;
			if (scrollChildContext != null && !this.intentional)
			{
				int index2 = scrollChildContext.index;
			}
			return scrollChildContext;
		}

		// Token: 0x0600D27A RID: 53882 RVA: 0x004E1FBC File Offset: 0x004E01BC
		public override int? ContextIndex(NavigationContext to)
		{
			foreach (Context context in this.contexts)
			{
				if (to.IsInside(context))
				{
					ScrollChildContext scrollChildContext = context as ScrollChildContext;
					if (scrollChildContext != null)
					{
						return new int?(scrollChildContext.index);
					}
				}
			}
			return null;
		}

		// Token: 0x0600D27B RID: 53883 RVA: 0x004E2040 File Offset: 0x004E0240
		public override void SelectIndex(int index)
		{
			if (this.preSelect != null)
			{
				this.preSelect(index);
			}
			base.SelectIndex(index);
		}

		// Token: 0x0400536F RID: 21359
		public bool intentional;

		// Token: 0x04005370 RID: 21360
		public Action<int> preSelect;
	}
}
