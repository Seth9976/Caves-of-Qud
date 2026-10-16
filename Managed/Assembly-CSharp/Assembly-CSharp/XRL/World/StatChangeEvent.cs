using System;

namespace XRL.World
{
	// Token: 0x0200085D RID: 2141
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class StatChangeEvent : PooledEvent<StatChangeEvent>
	{
		// Token: 0x0600555D RID: 21853 RVA: 0x001986D9 File Offset: 0x001968D9
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600555E RID: 21854 RVA: 0x001986E2 File Offset: 0x001968E2
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Name = null;
			this.Type = null;
			this.OldValue = 0;
			this.NewValue = 0;
			this.OldBaseValue = 0;
			this.NewBaseValue = 0;
			this.Stat = null;
		}

		// Token: 0x0600555F RID: 21855 RVA: 0x00198724 File Offset: 0x00196924
		public static StatChangeEvent FromPool(GameObject Object, string Name = null, string Type = null, int OldValue = 0, int NewValue = 0, int OldBaseValue = 0, int NewBaseValue = 0, Statistic Stat = null)
		{
			StatChangeEvent statChangeEvent = PooledEvent<StatChangeEvent>.FromPool();
			statChangeEvent.Object = Object;
			statChangeEvent.Name = Name;
			statChangeEvent.Type = Type;
			statChangeEvent.OldValue = OldValue;
			statChangeEvent.NewValue = NewValue;
			statChangeEvent.OldBaseValue = OldBaseValue;
			statChangeEvent.NewBaseValue = NewBaseValue;
			statChangeEvent.Stat = Stat;
			return statChangeEvent;
		}

		// Token: 0x04002B01 RID: 11009
		public GameObject Object;

		// Token: 0x04002B02 RID: 11010
		public string Name;

		// Token: 0x04002B03 RID: 11011
		public string Type;

		// Token: 0x04002B04 RID: 11012
		public int OldValue;

		// Token: 0x04002B05 RID: 11013
		public int NewValue;

		// Token: 0x04002B06 RID: 11014
		public int OldBaseValue;

		// Token: 0x04002B07 RID: 11015
		public int NewBaseValue;

		// Token: 0x04002B08 RID: 11016
		public Statistic Stat;
	}
}
