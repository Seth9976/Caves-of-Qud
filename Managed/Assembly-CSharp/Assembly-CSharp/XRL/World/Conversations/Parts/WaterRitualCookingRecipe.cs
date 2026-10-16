using System;
using Qud.API;
using XRL.UI;
using XRL.World.Parts;
using XRL.World.Skills.Cooking;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C23 RID: 3107
	public class WaterRitualCookingRecipe : IWaterRitualPart
	{
		// Token: 0x06007C55 RID: 31829 RVA: 0x002EB3BB File Offset: 0x002E95BB
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == PrepareTextEvent.ID || ID == GetChoiceTagEvent.ID || ID == EnteredElementEvent.ID;
		}

		// Token: 0x06007C56 RID: 31830 RVA: 0x002EB3E1 File Offset: 0x002E95E1
		public Type GetRecipeType(string Name)
		{
			return ModManager.ResolveType("XRL.World.Skills.Cooking." + Name, false, false, true);
		}

		// Token: 0x06007C57 RID: 31831 RVA: 0x002EB3F8 File Offset: 0x002E95F8
		public void GetSpecificRecipe()
		{
			string propertyOrTag = The.Speaker.GetPropertyOrTag("SharesRecipe", null);
			if (!propertyOrTag.IsNullOrEmpty())
			{
				if (The.Speaker.GetPropertyOrTag("SharesRecipeWithTrueKin", null) == "false")
				{
					this.Genotype = "!True Kin";
				}
				this.Recipe = Activator.CreateInstance(this.GetRecipeType(propertyOrTag)) as CookingRecipe;
				this.Text = The.Speaker.GetPropertyOrTag("SharesRecipeText", null);
				return;
			}
			TeachesDish teachesDish;
			if (The.Speaker.TryGetPart<TeachesDish>(out teachesDish))
			{
				this.Recipe = teachesDish.Recipe;
				this.Text = teachesDish.Text;
				return;
			}
			if (!WaterRitual.RecordFaction.WaterRitualRecipe.IsNullOrEmpty())
			{
				this.Recipe = Activator.CreateInstance(this.GetRecipeType(WaterRitual.RecordFaction.WaterRitualRecipe)) as CookingRecipe;
				this.Text = WaterRitual.RecordFaction.WaterRitualRecipeText;
				this.Genotype = WaterRitual.RecordFaction.WaterRitualRecipeGenotype;
			}
		}

		// Token: 0x06007C58 RID: 31832 RVA: 0x002EB4EC File Offset: 0x002E96EC
		public void GetChefRecipe()
		{
			Chef part = The.Speaker.GetPart<Chef>();
			if (part == null)
			{
				return;
			}
			Random random = new Random(WaterRitual.Record.mySeed);
			this.Recipe = part.signatureDishes.GetRandomElement<CookingRecipe>(random);
		}

		// Token: 0x06007C59 RID: 31833 RVA: 0x002EB52C File Offset: 0x002E972C
		public override void Awake()
		{
			if (The.Speaker.GetxTag("WaterRitual", "SellCookingRecipe", null) == "false")
			{
				return;
			}
			if (this.Chef)
			{
				this.GetChefRecipe();
			}
			else
			{
				this.GetSpecificRecipe();
			}
			if (this.Recipe == null || CookingGameState.KnowsRecipe(this.Recipe))
			{
				return;
			}
			this.Reputation = GetWaterRitualCostEvent.GetFor(The.Player, The.Speaker, "CookingRecipe", 50);
			this.Visible = true;
		}

		// Token: 0x06007C5A RID: 31834 RVA: 0x002EB5AA File Offset: 0x002E97AA
		public override bool HandleEvent(PrepareTextEvent E)
		{
			if (!this.Text.IsNullOrEmpty())
			{
				E.Text.Clear();
				E.Text.Append(this.Text);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007C5B RID: 31835 RVA: 0x002EB5E0 File Offset: 0x002E97E0
		public override bool HandleEvent(EnteredElementEvent E)
		{
			if (!this.Genotype.IsNullOrEmpty())
			{
				if (this.Genotype.HasDelimitedSubstring(',', "!True Kin", StringComparison.Ordinal) && The.Player.IsTrueKin())
				{
					return The.Player.ShowSuccess("True kin cannot digest this meal.", true);
				}
				if (this.Genotype.HasDelimitedSubstring(',', "True Kin", StringComparison.Ordinal) && !The.Player.IsTrueKin())
				{
					return The.Player.ShowSuccess("Only true kin can digest this meal.", true);
				}
				if (this.Genotype.HasDelimitedSubstring(',', "!Mutant", StringComparison.Ordinal) && The.Player.IsMutant())
				{
					return The.Player.ShowSuccess("Mutants cannot digest this meal.", true);
				}
				if (this.Genotype.HasDelimitedSubstring(',', "Mutant", StringComparison.Ordinal) && !The.Player.IsMutant())
				{
					return The.Player.ShowSuccess("Only mutants can digest this meal.", true);
				}
			}
			if (base.UseReputation("WaterRitualUse"))
			{
				Popup.Show(The.Speaker.Does("share", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " the recipe for {{W|" + this.Recipe.GetDisplayName() + "}}!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				JournalAPI.AddRecipeNote(this.Recipe, null, true, true, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007C5C RID: 31836 RVA: 0x002EB744 File Offset: 0x002E9944
		public override bool HandleEvent(GetChoiceTagEvent E)
		{
			E.Tag = string.Concat(new string[]
			{
				"{{",
				this.Lowlight,
				"|[learn to cook {{W|",
				this.Recipe.GetDisplayName(),
				"}}: {{",
				this.Numeric,
				"|",
				base.GetReputationCost("WaterRitualUse").ToString(),
				"}} reputation]}}"
			});
			return false;
		}

		// Token: 0x04003884 RID: 14468
		public CookingRecipe Recipe;

		// Token: 0x04003885 RID: 14469
		public string Text;

		// Token: 0x04003886 RID: 14470
		public string Genotype;

		// Token: 0x04003887 RID: 14471
		public bool Chef;
	}
}
