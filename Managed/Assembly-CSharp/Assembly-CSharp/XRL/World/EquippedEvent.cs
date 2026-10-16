using System;
using System.Collections.Generic;
using XRL.World.Anatomy;

namespace XRL.World
{
	// Token: 0x0200070C RID: 1804
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class EquippedEvent : IActOnItemEvent
	{
		// Token: 0x06004B43 RID: 19267 RVA: 0x0017649D File Offset: 0x0017469D
		public EquippedEvent()
		{
			this.ID = EquippedEvent.ID;
		}

		// Token: 0x06004B44 RID: 19268 RVA: 0x001764B0 File Offset: 0x001746B0
		public static int CountPool()
		{
			if (EquippedEvent.Pool != null)
			{
				return EquippedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004B45 RID: 19269 RVA: 0x001764C5 File Offset: 0x001746C5
		public static void ResetPool()
		{
			while (EquippedEvent.PoolCounter > 0)
			{
				EquippedEvent.Pool[--EquippedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004B46 RID: 19270 RVA: 0x001764ED File Offset: 0x001746ED
		public static void ResetTo(ref EquippedEvent E)
		{
			MinEvent.ResetTo<EquippedEvent>(E, EquippedEvent.Pool, ref EquippedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004B47 RID: 19271 RVA: 0x00176503 File Offset: 0x00174703
		public static EquippedEvent FromPool()
		{
			return MinEvent.FromPool<EquippedEvent>(ref EquippedEvent.Pool, ref EquippedEvent.PoolCounter, 8192);
		}

		// Token: 0x06004B48 RID: 19272 RVA: 0x00176519 File Offset: 0x00174719
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004B49 RID: 19273 RVA: 0x0017652D File Offset: 0x0017472D
		public override void Reset()
		{
			base.Reset();
			this.Part = null;
		}

		// Token: 0x06004B4A RID: 19274 RVA: 0x0017653C File Offset: 0x0017473C
		public static void Send(GameObject Actor, GameObject Item, BodyPart Part)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Item) && Item.HasRegisteredEvent("Equipped"))
			{
				Event @event = new Event("Equipped");
				@event.SetParameter("EquippingObject", Actor);
				@event.SetParameter("Object", Item);
				@event.SetParameter("BodyPart", Part);
				@event.SetParameter("Actor", Actor);
				flag = Item.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Item) && Item.WantEvent(EquippedEvent.ID, MinEvent.CascadeLevel))
			{
				EquippedEvent equippedEvent = EquippedEvent.FromPool();
				equippedEvent.Actor = Actor;
				equippedEvent.Item = Item;
				equippedEvent.Part = Part;
				flag = Item.HandleEvent(equippedEvent);
			}
		}

		// Token: 0x04002451 RID: 9297
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(EquippedEvent), null, new MinEvent.EventPoolCount(EquippedEvent.CountPool), new MinEvent.EventPoolReset(EquippedEvent.ResetPool));

		// Token: 0x04002452 RID: 9298
		private static List<EquippedEvent> Pool;

		// Token: 0x04002453 RID: 9299
		private static int PoolCounter;

		// Token: 0x04002454 RID: 9300
		public BodyPart Part;
	}
}
