using System;
using System.Text;

namespace XRL.World.Units
{
	// Token: 0x020009B6 RID: 2486
	[Serializable]
	public class GameObjectTieredArmorUnit : GameObjectUnit
	{
		// Token: 0x06006B0A RID: 27402 RVA: 0x00227828 File Offset: 0x00225A28
		public override void Apply(GameObject Object)
		{
			int num = 0;
			int num2 = 0;
			while (num < this.Amount && num2 <= 5)
			{
				PopulationResult populationResult = PopulationManager.RollOneFrom("DynamicInheritsTable:Armor:Tier" + this.Tier, null, null);
				string text = ((populationResult != null) ? populationResult.Blueprint : null);
				GameObjectBlueprint gameObjectBlueprint;
				if (!text.IsNullOrEmpty() && GameObjectFactory.Factory.Blueprints.TryGetValue(text, out gameObjectBlueprint))
				{
					if (this.Equippable & (num2 < 5))
					{
						string partParameter = gameObjectBlueprint.GetPartParameter<string>("Armor", "WornOn", null);
						if (!partParameter.IsNullOrEmpty() && partParameter != "*" && !Object.Body.HasPart(partParameter))
						{
							goto IL_00D2;
						}
					}
					num2 = 0;
					num++;
					GameObject gameObject = GameObjectFactory.Factory.CreateObject(gameObjectBlueprint, 0, 0, null, null, null, null, null);
					if (this.Gigantic)
					{
						gameObject.ApplyModification("ModGigantic", true, null, false);
					}
					Object.ReceiveObject(gameObject, false, null);
				}
				IL_00D2:
				num2++;
			}
		}

		// Token: 0x06006B0B RID: 27403 RVA: 0x0022791B File Offset: 0x00225B1B
		public override void Reset()
		{
			base.Reset();
			this.Tier = null;
			this.Amount = 0;
			this.Gigantic = false;
			this.Equippable = false;
		}

		// Token: 0x06006B0C RID: 27404 RVA: 0x00227940 File Offset: 0x00225B40
		public override string GetDescription(bool Inscription = false)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder("Spawns with").Compound(this.Amount).Compound("random pieces of");
			if (this.Gigantic)
			{
				stringBuilder.Compound("gigantic,");
			}
			int num = this.Tier.RollMax();
			int num2 = this.Tier.RollMin();
			string text = "low";
			string text2 = "low";
			if (num2 > 5)
			{
				text = "high";
			}
			else if (num2 > 2)
			{
				text = "mid";
			}
			if (num > 5)
			{
				text2 = "high";
			}
			else if (num > 2)
			{
				text2 = "mid";
			}
			if (string.Equals(text, text2))
			{
				stringBuilder.Compound(text).Append(" tier armor");
			}
			else
			{
				stringBuilder.Compound(text + "-to-" + text2).Append(" tier armor");
			}
			return Event.FinalizeString(stringBuilder);
		}

		// Token: 0x04003104 RID: 12548
		public string Tier;

		// Token: 0x04003105 RID: 12549
		public int Amount;

		// Token: 0x04003106 RID: 12550
		public bool Gigantic;

		// Token: 0x04003107 RID: 12551
		public bool Equippable;
	}
}
