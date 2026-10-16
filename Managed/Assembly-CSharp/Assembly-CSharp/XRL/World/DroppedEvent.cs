using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020006FC RID: 1788
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class DroppedEvent : IActOnItemEvent
	{
		// Token: 0x06004AC9 RID: 19145 RVA: 0x00175334 File Offset: 0x00173534
		public DroppedEvent()
		{
			this.ID = DroppedEvent.ID;
		}

		// Token: 0x06004ACA RID: 19146 RVA: 0x00175347 File Offset: 0x00173547
		public static int CountPool()
		{
			if (DroppedEvent.Pool != null)
			{
				return DroppedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004ACB RID: 19147 RVA: 0x0017535C File Offset: 0x0017355C
		public static void ResetPool()
		{
			while (DroppedEvent.PoolCounter > 0)
			{
				DroppedEvent.Pool[--DroppedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004ACC RID: 19148 RVA: 0x00175384 File Offset: 0x00173584
		public static void ResetTo(ref DroppedEvent E)
		{
			MinEvent.ResetTo<DroppedEvent>(E, DroppedEvent.Pool, ref DroppedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004ACD RID: 19149 RVA: 0x0017539A File Offset: 0x0017359A
		public static DroppedEvent FromPool()
		{
			return MinEvent.FromPool<DroppedEvent>(ref DroppedEvent.Pool, ref DroppedEvent.PoolCounter, 8192);
		}

		// Token: 0x06004ACE RID: 19150 RVA: 0x001753B0 File Offset: 0x001735B0
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004ACF RID: 19151 RVA: 0x001753C4 File Offset: 0x001735C4
		public override void Reset()
		{
			base.Reset();
			this.Forced = false;
		}

		// Token: 0x06004AD0 RID: 19152 RVA: 0x001753D3 File Offset: 0x001735D3
		public static DroppedEvent FromPool(GameObject Actor, GameObject Item, bool Forced = false)
		{
			DroppedEvent droppedEvent = DroppedEvent.FromPool();
			droppedEvent.Actor = Actor;
			droppedEvent.Item = Item;
			droppedEvent.Forced = Forced;
			return droppedEvent;
		}

		// Token: 0x06004AD1 RID: 19153 RVA: 0x001753EF File Offset: 0x001735EF
		public static void Send(GameObject Actor, GameObject Item, bool Forced = false)
		{
			Item.HandleEvent(DroppedEvent.FromPool(Actor, Item, Forced));
		}

		// Token: 0x04002420 RID: 9248
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(DroppedEvent), null, new MinEvent.EventPoolCount(DroppedEvent.CountPool), new MinEvent.EventPoolReset(DroppedEvent.ResetPool));

		// Token: 0x04002421 RID: 9249
		private static List<DroppedEvent> Pool;

		// Token: 0x04002422 RID: 9250
		private static int PoolCounter;

		// Token: 0x04002423 RID: 9251
		public bool Forced;
	}
}
