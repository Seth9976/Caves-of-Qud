using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020006A6 RID: 1702
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class BeforeUnequippedEvent : IActOnItemEvent
	{
		// Token: 0x0600487C RID: 18556 RVA: 0x0016C7D5 File Offset: 0x0016A9D5
		public BeforeUnequippedEvent()
		{
			this.ID = BeforeUnequippedEvent.ID;
		}

		// Token: 0x0600487D RID: 18557 RVA: 0x0016C7E8 File Offset: 0x0016A9E8
		public static int CountPool()
		{
			if (BeforeUnequippedEvent.Pool != null)
			{
				return BeforeUnequippedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x0600487E RID: 18558 RVA: 0x0016C7FD File Offset: 0x0016A9FD
		public static void ResetPool()
		{
			while (BeforeUnequippedEvent.PoolCounter > 0)
			{
				BeforeUnequippedEvent.Pool[--BeforeUnequippedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x0600487F RID: 18559 RVA: 0x0016C825 File Offset: 0x0016AA25
		public static void ResetTo(ref BeforeUnequippedEvent E)
		{
			MinEvent.ResetTo<BeforeUnequippedEvent>(E, BeforeUnequippedEvent.Pool, ref BeforeUnequippedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004880 RID: 18560 RVA: 0x0016C83B File Offset: 0x0016AA3B
		public static BeforeUnequippedEvent FromPool()
		{
			return MinEvent.FromPool<BeforeUnequippedEvent>(ref BeforeUnequippedEvent.Pool, ref BeforeUnequippedEvent.PoolCounter, 8192);
		}

		// Token: 0x06004881 RID: 18561 RVA: 0x0016C851 File Offset: 0x0016AA51
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004882 RID: 18562 RVA: 0x0016C865 File Offset: 0x0016AA65
		public static BeforeUnequippedEvent FromPool(GameObject Actor, GameObject Item)
		{
			BeforeUnequippedEvent beforeUnequippedEvent = BeforeUnequippedEvent.FromPool();
			beforeUnequippedEvent.Actor = Actor;
			beforeUnequippedEvent.Item = Item;
			return beforeUnequippedEvent;
		}

		// Token: 0x06004883 RID: 18563 RVA: 0x0016C87C File Offset: 0x0016AA7C
		public static void Send(GameObject Object, GameObject WasEquippedBy)
		{
			if (GameObject.Validate(ref Object))
			{
				if (Object.WantEvent(BeforeUnequippedEvent.ID, MinEvent.CascadeLevel))
				{
					Object.HandleEvent(BeforeUnequippedEvent.FromPool(WasEquippedBy, Object));
					if (!GameObject.Validate(ref Object))
					{
						return;
					}
				}
				if (Object.HasRegisteredEvent("BeforeUnequipped"))
				{
					Object.FireEvent(Event.New("BeforeUnequipped", "Actor", WasEquippedBy));
				}
			}
		}

		// Token: 0x040022B7 RID: 8887
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(BeforeUnequippedEvent), null, new MinEvent.EventPoolCount(BeforeUnequippedEvent.CountPool), new MinEvent.EventPoolReset(BeforeUnequippedEvent.ResetPool));

		// Token: 0x040022B8 RID: 8888
		private static List<BeforeUnequippedEvent> Pool;

		// Token: 0x040022B9 RID: 8889
		private static int PoolCounter;
	}
}
