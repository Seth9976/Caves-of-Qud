using System;

namespace XRL.World
{
	// Token: 0x020007A3 RID: 1955
	[GameEvent(Cascade = 3, Cache = MinEvent.Cache.Pool)]
	public class GetWaterRitualCostEvent : PooledEvent<GetWaterRitualCostEvent>
	{
		// Token: 0x06004FB0 RID: 20400 RVA: 0x00184ED5 File Offset: 0x001830D5
		public override int GetCascadeLevel()
		{
			return GetWaterRitualCostEvent.CascadeLevel;
		}

		// Token: 0x06004FB1 RID: 20401 RVA: 0x00184EDC File Offset: 0x001830DC
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004FB2 RID: 20402 RVA: 0x00184EE5 File Offset: 0x001830E5
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Target = null;
			this.Type = null;
			this.BaseCost = 0;
			this.Cost = 0;
		}

		// Token: 0x06004FB3 RID: 20403 RVA: 0x00184F10 File Offset: 0x00183110
		public static GetWaterRitualCostEvent FromPool(GameObject Actor, GameObject Target, string Type, int BaseCost, int Cost)
		{
			GetWaterRitualCostEvent getWaterRitualCostEvent = PooledEvent<GetWaterRitualCostEvent>.FromPool();
			getWaterRitualCostEvent.Actor = Actor;
			getWaterRitualCostEvent.Target = Target;
			getWaterRitualCostEvent.Type = Type;
			getWaterRitualCostEvent.BaseCost = BaseCost;
			getWaterRitualCostEvent.Cost = Cost;
			return getWaterRitualCostEvent;
		}

		// Token: 0x06004FB4 RID: 20404 RVA: 0x00184F3C File Offset: 0x0018313C
		public static int GetFor(GameObject Actor, GameObject Target, string Type, int BaseCost)
		{
			int num = BaseCost;
			if (Actor.HasRegisteredEvent("GetWaterRitualCost"))
			{
				Event @event = Event.New("GetWaterRitualCost", 2, 1, 2);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Target", Target);
				@event.SetParameter("Type", Type);
				@event.SetParameter("BaseCost", BaseCost);
				@event.SetParameter("Cost", num);
				if (!Actor.FireEvent(@event))
				{
					return @event.GetIntParameter("Cost", 0);
				}
				num = @event.GetIntParameter("Cost", 0);
			}
			if (Actor.WantEvent(PooledEvent<GetWaterRitualCostEvent>.ID, GetWaterRitualCostEvent.CascadeLevel))
			{
				GetWaterRitualCostEvent getWaterRitualCostEvent = GetWaterRitualCostEvent.FromPool(Actor, Target, Type, BaseCost, num);
				if (!Actor.HandleEvent(getWaterRitualCostEvent))
				{
					return getWaterRitualCostEvent.Cost;
				}
				num = getWaterRitualCostEvent.Cost;
			}
			return num;
		}

		// Token: 0x0400275E RID: 10078
		public new static readonly int CascadeLevel = 3;

		// Token: 0x0400275F RID: 10079
		public GameObject Actor;

		// Token: 0x04002760 RID: 10080
		public GameObject Target;

		// Token: 0x04002761 RID: 10081
		public string Type;

		// Token: 0x04002762 RID: 10082
		public int BaseCost;

		// Token: 0x04002763 RID: 10083
		public int Cost;
	}
}
