using System;

namespace XRL.World
{
	// Token: 0x0200078E RID: 1934
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class GetSpaceTimeAnomalyEmergenceExplodePercentageChanceEvent : PooledEvent<GetSpaceTimeAnomalyEmergenceExplodePercentageChanceEvent>
	{
		// Token: 0x06004F20 RID: 20256 RVA: 0x00182869 File Offset: 0x00180A69
		public override int GetCascadeLevel()
		{
			return GetSpaceTimeAnomalyEmergenceExplodePercentageChanceEvent.CascadeLevel;
		}

		// Token: 0x06004F21 RID: 20257 RVA: 0x00182870 File Offset: 0x00180A70
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004F22 RID: 20258 RVA: 0x00182879 File Offset: 0x00180A79
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.BaseChance = 0;
			this.Chance = 0;
		}

		// Token: 0x06004F23 RID: 20259 RVA: 0x00182898 File Offset: 0x00180A98
		public static int GetFor(GameObject Object, int BaseChance = 0)
		{
			bool flag = true;
			int num = BaseChance;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GetSpaceTimeAnomalyEmergenceExplodePercentageChance"))
			{
				Event @event = Event.New("GetSpaceTimeAnomalyEmergenceExplodePercentageChance", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("BaseChance", BaseChance);
				@event.SetParameter("Chance", num);
				flag = Object.FireEvent(@event);
				num = @event.GetIntParameter("Chance", 0);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetSpaceTimeAnomalyEmergenceExplodePercentageChanceEvent>.ID, GetSpaceTimeAnomalyEmergenceExplodePercentageChanceEvent.CascadeLevel))
			{
				GetSpaceTimeAnomalyEmergenceExplodePercentageChanceEvent getSpaceTimeAnomalyEmergenceExplodePercentageChanceEvent = PooledEvent<GetSpaceTimeAnomalyEmergenceExplodePercentageChanceEvent>.FromPool();
				getSpaceTimeAnomalyEmergenceExplodePercentageChanceEvent.Object = Object;
				getSpaceTimeAnomalyEmergenceExplodePercentageChanceEvent.BaseChance = BaseChance;
				getSpaceTimeAnomalyEmergenceExplodePercentageChanceEvent.Chance = num;
				flag = Object.HandleEvent(getSpaceTimeAnomalyEmergenceExplodePercentageChanceEvent);
				num = getSpaceTimeAnomalyEmergenceExplodePercentageChanceEvent.Chance;
			}
			return num;
		}

		// Token: 0x040026D4 RID: 9940
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040026D5 RID: 9941
		public GameObject Object;

		// Token: 0x040026D6 RID: 9942
		public int BaseChance;

		// Token: 0x040026D7 RID: 9943
		public int Chance;
	}
}
