using System;

namespace XRL.World
{
	// Token: 0x0200077A RID: 1914
	[GameEvent(Cascade = 3, Cache = MinEvent.Cache.Pool)]
	public class GetPropertyModDescription : PooledEvent<GetPropertyModDescription>
	{
		// Token: 0x06004E9A RID: 20122 RVA: 0x00181034 File Offset: 0x0017F234
		public override int GetCascadeLevel()
		{
			return GetPropertyModDescription.CascadeLevel;
		}

		// Token: 0x06004E9B RID: 20123 RVA: 0x0018103B File Offset: 0x0017F23B
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004E9C RID: 20124 RVA: 0x00181044 File Offset: 0x0017F244
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Property = null;
			this.StatName = null;
			this.Stats = null;
		}

		// Token: 0x06004E9D RID: 20125 RVA: 0x00181068 File Offset: 0x0017F268
		public static GetPropertyModDescription FromPool(GameObject Actor, string Property, string StatName, Templates.StatCollector Stats)
		{
			GetPropertyModDescription getPropertyModDescription = PooledEvent<GetPropertyModDescription>.FromPool();
			getPropertyModDescription.Actor = Actor;
			getPropertyModDescription.Property = Property;
			getPropertyModDescription.StatName = StatName;
			getPropertyModDescription.Stats = Stats;
			return getPropertyModDescription;
		}

		// Token: 0x06004E9E RID: 20126 RVA: 0x0018108C File Offset: 0x0017F28C
		public static bool GetFor(GameObject Actor, string Property, string StatName, Templates.StatCollector Stats)
		{
			if (Actor.WantEvent(PooledEvent<GetPropertyModDescription>.ID, GetPropertyModDescription.CascadeLevel))
			{
				GetPropertyModDescription getPropertyModDescription = GetPropertyModDescription.FromPool(Actor, Property, StatName, Stats);
				if (!Actor.HandleEvent(getPropertyModDescription))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004E9F RID: 20127 RVA: 0x001810C1 File Offset: 0x0017F2C1
		public void AddLinearBonusModifier(int bonus, string source)
		{
			Templates.StatCollector stats = this.Stats;
			if (stats == null)
			{
				return;
			}
			stats.AddLinearBonusModifier(this.StatName, bonus, source);
		}

		// Token: 0x04002660 RID: 9824
		public new static readonly int CascadeLevel = 3;

		// Token: 0x04002661 RID: 9825
		public GameObject Actor;

		// Token: 0x04002662 RID: 9826
		public string Property;

		// Token: 0x04002663 RID: 9827
		public string StatName;

		// Token: 0x04002664 RID: 9828
		public Templates.StatCollector Stats;
	}
}
