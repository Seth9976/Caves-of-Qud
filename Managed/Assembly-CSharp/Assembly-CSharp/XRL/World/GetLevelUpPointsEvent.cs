using System;

namespace XRL.World
{
	// Token: 0x0200075E RID: 1886
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class GetLevelUpPointsEvent : PooledEvent<GetLevelUpPointsEvent>
	{
		// Token: 0x06004DC9 RID: 19913 RVA: 0x0017DED6 File Offset: 0x0017C0D6
		public override int GetCascadeLevel()
		{
			return GetLevelUpPointsEvent.CascadeLevel;
		}

		// Token: 0x06004DCA RID: 19914 RVA: 0x0017DEDD File Offset: 0x0017C0DD
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004DCB RID: 19915 RVA: 0x0017DEE6 File Offset: 0x0017C0E6
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Level = 0;
			this.HitPoints = 0;
			this.SkillPoints = 0;
			this.MutationPoints = 0;
			this.AttributePoints = 0;
			this.AttributeBonus = 0;
			this.RapidAdvancement = 0;
		}

		// Token: 0x06004DCC RID: 19916 RVA: 0x0017DF28 File Offset: 0x0017C128
		public static void GetFor(GameObject Actor, int Level, ref int HitPoints, ref int SkillPoints, ref int MutationPoints, ref int AttributePoints, ref int AttributeBonus, ref int RapidAdvancement)
		{
			if (Actor.HasRegisteredEvent("GetLevelUpPoints"))
			{
				Event @event = Event.New("GetLevelUpPoints", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Level", Level);
				@event.SetParameter("HitPoints", HitPoints);
				@event.SetParameter("SkillPoints", SkillPoints);
				@event.SetParameter("MutationPoints", MutationPoints);
				@event.SetParameter("AttributePoints", AttributePoints);
				@event.SetParameter("AttributeBonus", AttributeBonus);
				@event.SetParameter("RapidAdvancement", RapidAdvancement);
				bool flag = Actor.FireEvent(@event);
				HitPoints = @event.GetIntParameter("HitPoints", 0);
				SkillPoints = @event.GetIntParameter("SkillPoints", 0);
				MutationPoints = @event.GetIntParameter("MutationPoints", 0);
				AttributePoints = @event.GetIntParameter("AttributePoints", 0);
				AttributeBonus = @event.GetIntParameter("AttributeBonus", 0);
				RapidAdvancement = @event.GetIntParameter("RapidAdvancement", 0);
				if (!flag)
				{
					return;
				}
			}
			if (Actor.WantEvent(PooledEvent<GetLevelUpPointsEvent>.ID, GetLevelUpPointsEvent.CascadeLevel))
			{
				GetLevelUpPointsEvent getLevelUpPointsEvent = PooledEvent<GetLevelUpPointsEvent>.FromPool();
				getLevelUpPointsEvent.Actor = Actor;
				getLevelUpPointsEvent.Level = Level;
				getLevelUpPointsEvent.HitPoints = HitPoints;
				getLevelUpPointsEvent.SkillPoints = SkillPoints;
				getLevelUpPointsEvent.MutationPoints = MutationPoints;
				getLevelUpPointsEvent.AttributePoints = AttributePoints;
				getLevelUpPointsEvent.AttributeBonus = AttributeBonus;
				getLevelUpPointsEvent.RapidAdvancement = RapidAdvancement;
				Actor.HandleEvent(getLevelUpPointsEvent);
				HitPoints = getLevelUpPointsEvent.HitPoints;
				SkillPoints = getLevelUpPointsEvent.SkillPoints;
				MutationPoints = getLevelUpPointsEvent.MutationPoints;
				AttributePoints = getLevelUpPointsEvent.AttributePoints;
				AttributeBonus = getLevelUpPointsEvent.AttributeBonus;
				RapidAdvancement = getLevelUpPointsEvent.RapidAdvancement;
			}
		}

		// Token: 0x040025CB RID: 9675
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040025CC RID: 9676
		public GameObject Actor;

		// Token: 0x040025CD RID: 9677
		public int Level;

		// Token: 0x040025CE RID: 9678
		public int HitPoints;

		// Token: 0x040025CF RID: 9679
		public int SkillPoints;

		// Token: 0x040025D0 RID: 9680
		public int MutationPoints;

		// Token: 0x040025D1 RID: 9681
		public int AttributePoints;

		// Token: 0x040025D2 RID: 9682
		public int AttributeBonus;

		// Token: 0x040025D3 RID: 9683
		public int RapidAdvancement;
	}
}
