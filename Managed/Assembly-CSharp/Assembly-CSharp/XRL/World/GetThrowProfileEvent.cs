using System;
using XRL.Rules;

namespace XRL.World
{
	// Token: 0x02000795 RID: 1941
	[GameEvent(Cascade = 1, Cache = MinEvent.Cache.Pool)]
	public class GetThrowProfileEvent : PooledEvent<GetThrowProfileEvent>
	{
		// Token: 0x06004F4C RID: 20300 RVA: 0x001831BE File Offset: 0x001813BE
		public override int GetCascadeLevel()
		{
			return GetThrowProfileEvent.CascadeLevel;
		}

		// Token: 0x06004F4D RID: 20301 RVA: 0x001831C5 File Offset: 0x001813C5
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004F4E RID: 20302 RVA: 0x001831D0 File Offset: 0x001813D0
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Object = null;
			this.ApparentTarget = null;
			this.TargetCell = null;
			this.Distance = 0;
			this.Range = 0;
			this.Strength = 0;
			this.AimVariance = 0;
			this.Telekinetic = false;
		}

		// Token: 0x06004F4F RID: 20303 RVA: 0x00183224 File Offset: 0x00181424
		public static bool Process(out int Range, out int Strength, out int AimVariance, out bool Telekinetic, GameObject Actor, GameObject Object, GameObject ApparentTarget = null, Cell TargetCell = null, int Distance = 0)
		{
			GameObject.Validate(ref Object);
			GameObject.Validate(ref Actor);
			Range = 4;
			Strength = 0;
			AimVariance = 0;
			Telekinetic = false;
			if (Actor != null)
			{
				Range += Actor.GetIntProperty("ThrowRangeBonus", 0) + Object.GetIntProperty("ThrowRangeSkillBonus", 0);
				Strength = Actor.Stat("Strength", 0);
			}
			if (Object != null)
			{
				Range += Object.GetIntProperty("ThrowRangeBonus", 0);
			}
			AimVariance = Stat.Random(0, 20) - 10;
			if (Actor != null)
			{
				int num = Actor.StatMod("Agility", 0);
				if (num != 0)
				{
					if (AimVariance == 0)
					{
						if (num < 0)
						{
							AimVariance += (50.in100() ? num : (-num));
						}
					}
					else if (AimVariance > 0)
					{
						AimVariance -= num;
						if (AimVariance < 0)
						{
							AimVariance = 0;
						}
					}
					else
					{
						AimVariance += num;
						if (AimVariance > 0)
						{
							AimVariance = 0;
						}
					}
				}
			}
			GetThrowProfileEvent getThrowProfileEvent = PooledEvent<GetThrowProfileEvent>.FromPool();
			getThrowProfileEvent.Actor = Actor;
			getThrowProfileEvent.Object = Object;
			getThrowProfileEvent.ApparentTarget = ApparentTarget;
			getThrowProfileEvent.TargetCell = TargetCell;
			getThrowProfileEvent.Distance = Distance;
			getThrowProfileEvent.Range = Range;
			getThrowProfileEvent.Strength = Strength;
			getThrowProfileEvent.AimVariance = AimVariance;
			getThrowProfileEvent.Telekinetic = Telekinetic;
			bool flag;
			try
			{
				if (Actor != null && Actor.WantEvent(PooledEvent<GetThrowProfileEvent>.ID, GetThrowProfileEvent.CascadeLevel) && !Actor.HandleEvent(getThrowProfileEvent))
				{
					flag = false;
				}
				else if (Object != null && Actor.WantEvent(PooledEvent<GetThrowProfileEvent>.ID, GetThrowProfileEvent.CascadeLevel) && !Object.HandleEvent(getThrowProfileEvent))
				{
					flag = false;
				}
				else
				{
					flag = true;
				}
			}
			finally
			{
				Strength = getThrowProfileEvent.Strength;
				Range = getThrowProfileEvent.Range + Stat.GetScoreModifier(Strength);
				AimVariance = getThrowProfileEvent.AimVariance;
				Telekinetic = getThrowProfileEvent.Telekinetic;
			}
			return flag;
		}

		// Token: 0x040026F7 RID: 9975
		public new static readonly int CascadeLevel = 1;

		// Token: 0x040026F8 RID: 9976
		public GameObject Actor;

		// Token: 0x040026F9 RID: 9977
		public GameObject Object;

		// Token: 0x040026FA RID: 9978
		public GameObject ApparentTarget;

		// Token: 0x040026FB RID: 9979
		public Cell TargetCell;

		// Token: 0x040026FC RID: 9980
		public int Distance;

		// Token: 0x040026FD RID: 9981
		public int Range;

		// Token: 0x040026FE RID: 9982
		public int Strength;

		// Token: 0x040026FF RID: 9983
		public int AimVariance;

		// Token: 0x04002700 RID: 9984
		public bool Telekinetic;
	}
}
