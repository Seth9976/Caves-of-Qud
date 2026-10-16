using System;
using XRL.World.Tinkering;

namespace XRL.World
{
	// Token: 0x02000808 RID: 2056
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class ModifyBitCostEvent : PooledEvent<ModifyBitCostEvent>
	{
		// Token: 0x0600522A RID: 21034 RVA: 0x0018BF6B File Offset: 0x0018A16B
		public override int GetCascadeLevel()
		{
			return ModifyBitCostEvent.CascadeLevel;
		}

		// Token: 0x0600522B RID: 21035 RVA: 0x0018BF72 File Offset: 0x0018A172
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600522C RID: 21036 RVA: 0x0018BF7B File Offset: 0x0018A17B
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Bits = null;
			this.Context = null;
		}

		// Token: 0x0600522D RID: 21037 RVA: 0x0018BF98 File Offset: 0x0018A198
		public static ModifyBitCostEvent FromPool(GameObject Actor, BitCost Bits, string Context)
		{
			ModifyBitCostEvent modifyBitCostEvent = PooledEvent<ModifyBitCostEvent>.FromPool();
			modifyBitCostEvent.Actor = Actor;
			modifyBitCostEvent.Bits = Bits;
			modifyBitCostEvent.Context = Context;
			return modifyBitCostEvent;
		}

		// Token: 0x0600522E RID: 21038 RVA: 0x0018BFB4 File Offset: 0x0018A1B4
		public static bool Process(GameObject Actor, BitCost Bits, string Context)
		{
			if (GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("ModifyBitCost"))
			{
				Event @event = Event.New("ModifyBitCost", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Bits", Bits);
				@event.SetParameter("Context", Context);
				if (!Actor.FireEvent(@event))
				{
					return false;
				}
			}
			if (GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<ModifyBitCostEvent>.ID, ModifyBitCostEvent.CascadeLevel))
			{
				ModifyBitCostEvent modifyBitCostEvent = ModifyBitCostEvent.FromPool(Actor, Bits, Context);
				if (!Actor.HandleEvent(modifyBitCostEvent))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400294B RID: 10571
		public new static readonly int CascadeLevel = 17;

		// Token: 0x0400294C RID: 10572
		public GameObject Actor;

		// Token: 0x0400294D RID: 10573
		public BitCost Bits;

		// Token: 0x0400294E RID: 10574
		public string Context;
	}
}
