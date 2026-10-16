using System;

namespace XRL.World
{
	// Token: 0x0200076B RID: 1899
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class GetModRarityWeightEvent : PooledEvent<GetModRarityWeightEvent>
	{
		// Token: 0x06004E2A RID: 20010 RVA: 0x0017F704 File Offset: 0x0017D904
		public override int GetCascadeLevel()
		{
			return GetModRarityWeightEvent.CascadeLevel;
		}

		// Token: 0x06004E2B RID: 20011 RVA: 0x0017F70B File Offset: 0x0017D90B
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004E2C RID: 20012 RVA: 0x0017F714 File Offset: 0x0017D914
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Mod = null;
			this.BaseWeight = 0;
			this.LinearAdjustment = 0;
			this.FactorAdjustment = 1.0;
		}

		// Token: 0x06004E2D RID: 20013 RVA: 0x0017F748 File Offset: 0x0017D948
		public static int GetFor(GameObject Object, ModEntry Mod, int BaseWeight)
		{
			bool flag = true;
			int num = 0;
			double num2 = 1.0;
			if (flag)
			{
				bool flag2 = GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GetModRarityWeight");
				bool flag3 = The.Player != null && The.Player.HasRegisteredEvent("GetModRarityWeight");
				if (flag2 || flag3)
				{
					Event @event = Event.New("GetModRarityWeight", 0, 0, 0);
					@event.SetParameter("Object", Object);
					@event.SetParameter("Mod", Mod);
					@event.SetParameter("BaseWeight", BaseWeight);
					@event.SetParameter("LinearAdjustment", num);
					@event.SetParameter("FactorAdjustment", num2);
					if (flag && flag2 && GameObject.Validate(ref Object))
					{
						flag = Object.FireEvent(@event);
					}
					if (flag && flag3 && The.Player != null)
					{
						flag = The.Player.FireEvent(@event);
					}
					num = @event.GetIntParameter("LinearAdjustment", 0);
					num2 = (double)@event.GetParameter("FactorAdjustment");
				}
			}
			if (flag)
			{
				bool flag4 = GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetModRarityWeightEvent>.ID, GetModRarityWeightEvent.CascadeLevel);
				bool flag5 = The.Player != null && The.Player.WantEvent(PooledEvent<GetModRarityWeightEvent>.ID, GetModRarityWeightEvent.CascadeLevel);
				if (flag4 || flag5)
				{
					GetModRarityWeightEvent getModRarityWeightEvent = PooledEvent<GetModRarityWeightEvent>.FromPool();
					getModRarityWeightEvent.Object = Object;
					getModRarityWeightEvent.Mod = Mod;
					getModRarityWeightEvent.BaseWeight = BaseWeight;
					getModRarityWeightEvent.LinearAdjustment = num;
					getModRarityWeightEvent.FactorAdjustment = num2;
					if (flag && flag4 && GameObject.Validate(ref Object))
					{
						flag = Object.HandleEvent(getModRarityWeightEvent);
					}
					if (flag && flag5 && The.Player != null)
					{
						flag = The.Player.HandleEvent(getModRarityWeightEvent);
					}
					num = getModRarityWeightEvent.LinearAdjustment;
					num2 = getModRarityWeightEvent.FactorAdjustment;
				}
			}
			return (int)Math.Round((double)(BaseWeight + num) * num2);
		}

		// Token: 0x0400261C RID: 9756
		public new static readonly int CascadeLevel = 17;

		// Token: 0x0400261D RID: 9757
		public GameObject Object;

		// Token: 0x0400261E RID: 9758
		public ModEntry Mod;

		// Token: 0x0400261F RID: 9759
		public int BaseWeight;

		// Token: 0x04002620 RID: 9760
		public int LinearAdjustment;

		// Token: 0x04002621 RID: 9761
		public double FactorAdjustment = 1.0;
	}
}
