using System;
using System.Collections.Generic;
using XRL.Language;
using XRL.UI;
using XRL.World.Tinkering;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C2D RID: 3117
	public class WaterRitualTinkeringRecipe : IWaterRitualPart
	{
		// Token: 0x06007CA3 RID: 31907 RVA: 0x002ED084 File Offset: 0x002EB284
		public override void Awake()
		{
			if (WaterRitual.Record.numBlueprints <= 0)
			{
				return;
			}
			List<TinkerData> tinkerRecipes = TinkerData.TinkerRecipes;
			if (WaterRitual.Record.tinkerdata.IsNullOrEmpty<int>())
			{
				Random random = new Random(WaterRitual.Record.mySeed);
				int num = WaterRitual.Record.numBlueprints;
				int num2 = 100;
				while (num2 > 0 && num > 0)
				{
					int num3;
					if (!tinkerRecipes[num3 = random.Next(tinkerRecipes.Count)].Known() && !WaterRitual.Record.tinkerdata.Contains(num3))
					{
						WaterRitual.Record.tinkerdata.Add(num3);
						num--;
					}
					num2--;
				}
			}
			foreach (int num4 in WaterRitual.Record.tinkerdata)
			{
				this.Data = tinkerRecipes[num4];
				if (!this.Data.Known())
				{
					this.Reputation = GetWaterRitualCostEvent.GetFor(The.Player, The.Speaker, "TinkerRecipe", 50 * this.Data.Tier / 3);
					this.Visible = true;
					break;
				}
			}
		}

		// Token: 0x06007CA4 RID: 31908 RVA: 0x002ED1BC File Offset: 0x002EB3BC
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == IsElementVisibleEvent.ID || ID == PrepareTextEvent.ID || ID == GetChoiceTagEvent.ID || ID == EnteredElementEvent.ID;
		}

		// Token: 0x06007CA5 RID: 31909 RVA: 0x002ED1EC File Offset: 0x002EB3EC
		public override bool HandleEvent(PrepareTextEvent E)
		{
			if (this.Data.Type == "Mod")
			{
				E.Text.Replace("=recipe=", "[{{W|Item mod}}] - {{C|" + this.Data.DisplayName + "}}");
			}
			else
			{
				GameObject gameObject = GameObject.CreateSample(this.Data.Blueprint);
				E.Text.Replace("=recipe=", gameObject.IsPluralIfKnown ? gameObject.DisplayNameOnlyDirect : Grammar.Pluralize(gameObject.DisplayNameOnlyDirect));
				gameObject.Obliterate(null, false, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007CA6 RID: 31910 RVA: 0x002ED28C File Offset: 0x002EB48C
		public override bool HandleEvent(EnteredElementEvent E)
		{
			if (base.UseReputation("WaterRitualUse"))
			{
				TinkerData.KnownRecipes.Add(this.Data);
				WaterRitual.Record.numBlueprints--;
				The.Listener.PlayWorldOrUISound("sfx_characterMod_tinkerSchematic_learn", null);
				if (this.Data.Type == "Mod")
				{
					Popup.Show(The.Speaker.Does("teach", int.MaxValue, null, null, null, false, false, false, false, true, true, true, false, false, null, false, false, true, null, true, null, false) + " you to craft the item modification {{W|" + this.Data.DisplayName + "}}.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				else
				{
					GameObject gameObject = GameObject.CreateSample(this.Data.Blueprint);
					gameObject.MakeUnderstood(false);
					Popup.Show(The.Speaker.Does("teach", int.MaxValue, null, null, null, false, false, false, false, true, true, true, false, false, null, false, false, true, null, true, null, false) + " you to craft " + (gameObject.IsPlural ? this.Data.DisplayName : Grammar.Pluralize(gameObject.DisplayNameOnlyDirectAndStripped)) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					gameObject.Obliterate(null, false, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007CA7 RID: 31911 RVA: 0x002ED3F4 File Offset: 0x002EB5F4
		public override bool HandleEvent(GetChoiceTagEvent E)
		{
			E.Tag = string.Concat(new string[]
			{
				"{{",
				this.Lowlight,
				"|[{{",
				this.Numeric,
				"|",
				base.GetReputationCost("WaterRitualUse").ToString(),
				"}} reputation]}}"
			});
			return false;
		}

		// Token: 0x0400389D RID: 14493
		public TinkerData Data;
	}
}
