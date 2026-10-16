using System;
using System.Collections.Generic;
using XRL.World.Anatomy;

namespace XRL.World
{
	// Token: 0x0200070D RID: 1805
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class EquipperEquippedEvent : IActOnItemEvent
	{
		// Token: 0x06004B4C RID: 19276 RVA: 0x0017661B File Offset: 0x0017481B
		public EquipperEquippedEvent()
		{
			this.ID = EquipperEquippedEvent.ID;
		}

		// Token: 0x06004B4D RID: 19277 RVA: 0x0017662E File Offset: 0x0017482E
		public static int CountPool()
		{
			if (EquipperEquippedEvent.Pool != null)
			{
				return EquipperEquippedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004B4E RID: 19278 RVA: 0x00176643 File Offset: 0x00174843
		public static void ResetPool()
		{
			while (EquipperEquippedEvent.PoolCounter > 0)
			{
				EquipperEquippedEvent.Pool[--EquipperEquippedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004B4F RID: 19279 RVA: 0x0017666B File Offset: 0x0017486B
		public static void ResetTo(ref EquipperEquippedEvent E)
		{
			MinEvent.ResetTo<EquipperEquippedEvent>(E, EquipperEquippedEvent.Pool, ref EquipperEquippedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004B50 RID: 19280 RVA: 0x00176681 File Offset: 0x00174881
		public static EquipperEquippedEvent FromPool()
		{
			return MinEvent.FromPool<EquipperEquippedEvent>(ref EquipperEquippedEvent.Pool, ref EquipperEquippedEvent.PoolCounter, 8192);
		}

		// Token: 0x06004B51 RID: 19281 RVA: 0x00176697 File Offset: 0x00174897
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004B52 RID: 19282 RVA: 0x001766AB File Offset: 0x001748AB
		public override void Reset()
		{
			base.Reset();
			this.Part = null;
		}

		// Token: 0x06004B53 RID: 19283 RVA: 0x001766BA File Offset: 0x001748BA
		public static EquipperEquippedEvent FromPool(GameObject Actor, GameObject Item, BodyPart Part)
		{
			EquipperEquippedEvent equipperEquippedEvent = EquipperEquippedEvent.FromPool();
			equipperEquippedEvent.Actor = Actor;
			equipperEquippedEvent.Item = Item;
			equipperEquippedEvent.Part = Part;
			return equipperEquippedEvent;
		}

		// Token: 0x06004B54 RID: 19284 RVA: 0x001766D8 File Offset: 0x001748D8
		public static void Send(GameObject Actor, GameObject Item, BodyPart Part)
		{
			if (GameObject.Validate(ref Actor) && Actor.WantEvent(EquipperEquippedEvent.ID, MinEvent.CascadeLevel) && !Actor.HandleEvent(EquipperEquippedEvent.FromPool(Actor, Item, Part)))
			{
				return;
			}
			if (GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("EquipperEquipped"))
			{
				Event @event = new Event("EquipperEquipped");
				@event.SetParameter("Object", Item);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("BodyPart", Part);
				Actor.FireEvent(@event);
				return;
			}
		}

		// Token: 0x04002455 RID: 9301
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(EquipperEquippedEvent), null, new MinEvent.EventPoolCount(EquipperEquippedEvent.CountPool), new MinEvent.EventPoolReset(EquipperEquippedEvent.ResetPool));

		// Token: 0x04002456 RID: 9302
		private static List<EquipperEquippedEvent> Pool;

		// Token: 0x04002457 RID: 9303
		private static int PoolCounter;

		// Token: 0x04002458 RID: 9304
		public BodyPart Part;
	}
}
