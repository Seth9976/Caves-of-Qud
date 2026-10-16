using System;

namespace XRL.World
{
	// Token: 0x02000734 RID: 1844
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Pool)]
	public class GetCompanionLimitEvent : PooledEvent<GetCompanionLimitEvent>
	{
		// Token: 0x06004C62 RID: 19554 RVA: 0x00179AEA File Offset: 0x00177CEA
		public override int GetCascadeLevel()
		{
			return GetCompanionLimitEvent.CascadeLevel;
		}

		// Token: 0x06004C63 RID: 19555 RVA: 0x00179AF1 File Offset: 0x00177CF1
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004C64 RID: 19556 RVA: 0x00179AFA File Offset: 0x00177CFA
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Means = null;
			this.BaseLimit = 0;
			this.Limit = 0;
		}

		// Token: 0x06004C65 RID: 19557 RVA: 0x00179B20 File Offset: 0x00177D20
		public static int GetFor(GameObject Actor, string Means, int BaseLimit = 0)
		{
			int num = BaseLimit;
			bool flag = true;
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("GetCompanionLimit"))
			{
				Event @event = Event.New("GetCompanionLimit", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Means", Means);
				@event.SetParameter("BaseLimit", BaseLimit);
				@event.SetParameter("Limit", num);
				flag = Actor.FireEvent(@event);
				num = @event.GetIntParameter("Limit", 0);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<GetCompanionLimitEvent>.ID, GetCompanionLimitEvent.CascadeLevel))
			{
				GetCompanionLimitEvent getCompanionLimitEvent = PooledEvent<GetCompanionLimitEvent>.FromPool();
				getCompanionLimitEvent.Actor = Actor;
				getCompanionLimitEvent.Means = Means;
				getCompanionLimitEvent.BaseLimit = BaseLimit;
				getCompanionLimitEvent.Limit = num;
				flag = Actor.HandleEvent(getCompanionLimitEvent);
				num = getCompanionLimitEvent.Limit;
			}
			return num;
		}

		// Token: 0x040024FB RID: 9467
		public new static readonly int CascadeLevel = 15;

		// Token: 0x040024FC RID: 9468
		public GameObject Actor;

		// Token: 0x040024FD RID: 9469
		public string Means;

		// Token: 0x040024FE RID: 9470
		public int BaseLimit;

		// Token: 0x040024FF RID: 9471
		public int Limit;
	}
}
