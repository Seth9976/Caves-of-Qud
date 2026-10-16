using System;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000D75 RID: 3445
	[Serializable]
	public class QuantumStabilized : Effect
	{
		// Token: 0x0600888B RID: 34955 RVA: 0x0030E656 File Offset: 0x0030C856
		public QuantumStabilized()
		{
			this.Duration = 1;
			this.DisplayName = "{{m|quantum-locked}}";
		}

		// Token: 0x0600888C RID: 34956 RVA: 0x0030E670 File Offset: 0x0030C870
		public QuantumStabilized(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x0600888D RID: 34957 RVA: 0x0030E67F File Offset: 0x0030C87F
		public override int GetEffectType()
		{
			return 83886336;
		}

		// Token: 0x0600888E RID: 34958 RVA: 0x0030E686 File Offset: 0x0030C886
		public override string GetDetails()
		{
			return "Stable from implosion.\nNothing can emerge.";
		}

		// Token: 0x0600888F RID: 34959 RVA: 0x0030E690 File Offset: 0x0030C890
		public override bool Apply(GameObject Object)
		{
			SpaceTimeVortex partDescendedFrom = Object.GetPartDescendedFrom<SpaceTimeVortex>();
			return partDescendedFrom != null && partDescendedFrom.SpaceTimeAnomalyEmergencePermillageBaseChance() > 0 && partDescendedFrom.SpaceTimeAnomalyEmergenceExplodePercentageBaseChance() > 0;
		}

		// Token: 0x06008890 RID: 34960 RVA: 0x0030E6C0 File Offset: 0x0030C8C0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetSpaceTimeAnomalyEmergenceExplodePercentageChanceEvent>.ID || ID == PooledEvent<GetSpaceTimeAnomalyEmergencePermillageChanceEvent>.ID;
		}

		// Token: 0x06008891 RID: 34961 RVA: 0x0030E6E6 File Offset: 0x0030C8E6
		public override bool HandleEvent(GetSpaceTimeAnomalyEmergenceExplodePercentageChanceEvent E)
		{
			E.Chance = 0;
			return base.HandleEvent(E);
		}

		// Token: 0x06008892 RID: 34962 RVA: 0x0030E6F6 File Offset: 0x0030C8F6
		public override bool HandleEvent(GetSpaceTimeAnomalyEmergencePermillageChanceEvent E)
		{
			E.Chance = 0;
			return base.HandleEvent(E);
		}

		// Token: 0x06008893 RID: 34963 RVA: 0x0030E706 File Offset: 0x0030C906
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.Reference)
			{
				E.AddAdjective("stable", 0);
			}
			return base.HandleEvent(E);
		}
	}
}
