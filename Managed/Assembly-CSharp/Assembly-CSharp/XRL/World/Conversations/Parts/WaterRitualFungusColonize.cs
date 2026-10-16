using System;
using XRL.World.Anatomy;
using XRL.World.Effects;
using XRL.World.Parts.Mutation;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C24 RID: 3108
	public class WaterRitualFungusColonize : IWaterRitualPart
	{
		// Token: 0x06007C5E RID: 31838 RVA: 0x002EB7CA File Offset: 0x002E99CA
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == GetChoiceTagEvent.ID || ID == EnteredElementEvent.ID;
		}

		// Token: 0x06007C5F RID: 31839 RVA: 0x002EB7E8 File Offset: 0x002E99E8
		public override void Awake()
		{
			if (WaterRitual.Record.numFungusLeft <= 0 || WaterRitual.RecordFaction.WaterRitualFungusInfect < 0)
			{
				return;
			}
			if (!WaterRitual.Record.TryGetAttribute("FungusType:", out this.Type))
			{
				this.Type = SporePuffer.InfectionObjectList.GetRandomElement<string>(null);
				WaterRitual.Record.attributes.Add("FungusType:" + this.Type);
			}
			GameObjectBlueprint gameObjectBlueprint;
			if (GameObjectFactory.Factory.Blueprints.TryGetValue(this.Type, out gameObjectBlueprint))
			{
				this.DisplayName = gameObjectBlueprint.DisplayName();
			}
			this.Reputation = GetWaterRitualCostEvent.GetFor(The.Player, The.Speaker, "FungalInfection", WaterRitual.RecordFaction.WaterRitualFungusInfect);
			this.Visible = true;
		}

		// Token: 0x06007C60 RID: 31840 RVA: 0x002EB8A8 File Offset: 0x002E9AA8
		public override bool HandleEvent(EnteredElementEvent E)
		{
			BodyPart bodyPart;
			string text;
			if (FungalSporeInfection.ChooseLimbForInfection(this.DisplayName, out bodyPart, out text) && base.UseReputation("WaterRitualUse"))
			{
				FungalSporeInfection.ApplyFungalInfection(The.Player, this.Type, bodyPart);
				WaterRitual.Record.numFungusLeft--;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007C61 RID: 31841 RVA: 0x002EB900 File Offset: 0x002E9B00
		public override bool HandleEvent(GetChoiceTagEvent E)
		{
			E.Tag = string.Concat(new string[]
			{
				"{{",
				this.Lowlight,
				"|[become infected with ",
				this.DisplayName,
				": {{",
				this.Numeric,
				"|",
				base.GetReputationCost("WaterRitualUse").ToString(),
				"}} reputation]}}"
			});
			return false;
		}

		// Token: 0x04003888 RID: 14472
		public string Type;

		// Token: 0x04003889 RID: 14473
		public string DisplayName = "a fungus";
	}
}
