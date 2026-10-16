using System;

namespace XRL.World
{
	// Token: 0x0200078F RID: 1935
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class GetSpaceTimeAnomalyEmergencePermillageChanceEvent : PooledEvent<GetSpaceTimeAnomalyEmergencePermillageChanceEvent>
	{
		// Token: 0x06004F26 RID: 20262 RVA: 0x00182966 File Offset: 0x00180B66
		public override int GetCascadeLevel()
		{
			return GetSpaceTimeAnomalyEmergencePermillageChanceEvent.CascadeLevel;
		}

		// Token: 0x06004F27 RID: 20263 RVA: 0x0018296D File Offset: 0x00180B6D
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004F28 RID: 20264 RVA: 0x00182976 File Offset: 0x00180B76
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.BaseChance = 0;
			this.Chance = 0;
		}

		// Token: 0x06004F29 RID: 20265 RVA: 0x00182994 File Offset: 0x00180B94
		public static int GetFor(GameObject Object, int BaseChance = 0)
		{
			bool flag = true;
			int num = BaseChance;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GetSpaceTimeAnomalyEmergencePermillageChance"))
			{
				Event @event = Event.New("GetSpaceTimeAnomalyEmergencePermillageChance", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("BaseChance", BaseChance);
				@event.SetParameter("Chance", num);
				flag = Object.FireEvent(@event);
				num = @event.GetIntParameter("Chance", 0);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetSpaceTimeAnomalyEmergencePermillageChanceEvent>.ID, GetSpaceTimeAnomalyEmergencePermillageChanceEvent.CascadeLevel))
			{
				GetSpaceTimeAnomalyEmergencePermillageChanceEvent getSpaceTimeAnomalyEmergencePermillageChanceEvent = PooledEvent<GetSpaceTimeAnomalyEmergencePermillageChanceEvent>.FromPool();
				getSpaceTimeAnomalyEmergencePermillageChanceEvent.Object = Object;
				getSpaceTimeAnomalyEmergencePermillageChanceEvent.BaseChance = BaseChance;
				getSpaceTimeAnomalyEmergencePermillageChanceEvent.Chance = num;
				flag = Object.HandleEvent(getSpaceTimeAnomalyEmergencePermillageChanceEvent);
				num = getSpaceTimeAnomalyEmergencePermillageChanceEvent.Chance;
			}
			return num;
		}

		// Token: 0x040026D8 RID: 9944
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040026D9 RID: 9945
		public GameObject Object;

		// Token: 0x040026DA RID: 9946
		public int BaseChance;

		// Token: 0x040026DB RID: 9947
		public int Chance;
	}
}
