using System;

namespace XRL.World
{
	// Token: 0x0200075D RID: 1885
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class GetLevelUpDiceEvent : PooledEvent<GetLevelUpDiceEvent>
	{
		// Token: 0x06004DC3 RID: 19907 RVA: 0x0017DD85 File Offset: 0x0017BF85
		public override int GetCascadeLevel()
		{
			return GetLevelUpDiceEvent.CascadeLevel;
		}

		// Token: 0x06004DC4 RID: 19908 RVA: 0x0017DD8C File Offset: 0x0017BF8C
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004DC5 RID: 19909 RVA: 0x0017DD95 File Offset: 0x0017BF95
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Level = 0;
			this.BaseHPGain = null;
			this.BaseSPGain = null;
			this.BaseMPGain = null;
		}

		// Token: 0x06004DC6 RID: 19910 RVA: 0x0017DDC0 File Offset: 0x0017BFC0
		public static void GetFor(GameObject Actor, int Level, ref string BaseHPGain, ref string BaseSPGain, ref string BaseMPGain)
		{
			if (Actor.HasRegisteredEvent("GetLevelUpDice"))
			{
				Event @event = Event.New("GetLevelUpDice", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Level", Level);
				@event.SetParameter("BaseHPGain", BaseHPGain);
				@event.SetParameter("BaseSPGain", BaseSPGain);
				@event.SetParameter("BaseMPGain", BaseMPGain);
				bool flag = Actor.FireEvent(@event);
				BaseHPGain = @event.GetStringParameter("BaseHPGain", null);
				BaseSPGain = @event.GetStringParameter("BaseSPGain", null);
				BaseMPGain = @event.GetStringParameter("BaseMPGain", null);
				if (!flag)
				{
					return;
				}
			}
			if (Actor.WantEvent(PooledEvent<GetLevelUpDiceEvent>.ID, GetLevelUpDiceEvent.CascadeLevel))
			{
				GetLevelUpDiceEvent getLevelUpDiceEvent = PooledEvent<GetLevelUpDiceEvent>.FromPool();
				getLevelUpDiceEvent.Actor = Actor;
				getLevelUpDiceEvent.Level = Level;
				getLevelUpDiceEvent.BaseHPGain = BaseHPGain;
				getLevelUpDiceEvent.BaseSPGain = BaseSPGain;
				getLevelUpDiceEvent.BaseMPGain = BaseMPGain;
				Actor.HandleEvent(getLevelUpDiceEvent);
				BaseHPGain = getLevelUpDiceEvent.BaseHPGain;
				BaseSPGain = getLevelUpDiceEvent.BaseSPGain;
				BaseMPGain = getLevelUpDiceEvent.BaseMPGain;
			}
		}

		// Token: 0x040025C5 RID: 9669
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040025C6 RID: 9670
		public GameObject Actor;

		// Token: 0x040025C7 RID: 9671
		public int Level;

		// Token: 0x040025C8 RID: 9672
		public string BaseHPGain;

		// Token: 0x040025C9 RID: 9673
		public string BaseSPGain;

		// Token: 0x040025CA RID: 9674
		public string BaseMPGain;
	}
}
