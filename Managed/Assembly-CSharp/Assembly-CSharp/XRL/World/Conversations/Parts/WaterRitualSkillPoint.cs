using System;
using XRL.UI;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C2C RID: 3116
	public class WaterRitualSkillPoint : IWaterRitualPart
	{
		// Token: 0x06007C9E RID: 31902 RVA: 0x002ECEAB File Offset: 0x002EB0AB
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == GetChoiceTagEvent.ID || ID == EnteredElementEvent.ID;
		}

		// Token: 0x06007C9F RID: 31903 RVA: 0x002ECECC File Offset: 0x002EB0CC
		public override void Awake()
		{
			if (WaterRitual.RecordFaction.WaterRitualSkillPointCost < 0 || WaterRitual.RecordFaction.WaterRitualSkillPointAmount <= 0)
			{
				return;
			}
			this.Reputation = GetWaterRitualCostEvent.GetFor(The.Player, The.Speaker, "SkillPoint", WaterRitual.RecordFaction.WaterRitualSkillPointCost);
			this.Points = WaterRitual.RecordFaction.WaterRitualSkillPointAmount;
			this.Visible = true;
		}

		// Token: 0x06007CA0 RID: 31904 RVA: 0x002ECF30 File Offset: 0x002EB130
		public override bool HandleEvent(EnteredElementEvent E)
		{
			if (base.UseReputation("WaterRitualUse"))
			{
				Popup.Show("Talking to " + The.Speaker.T(int.MaxValue, null, null, false, false, false, false, true, true, true, false, null, false, true, false, null, true, null, false) + " rouses in you an inert truth. You once wore the frock of a child. You poured salt through the cracks of your fingers, and you watched worlds form. Can it be all so simple still?", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				Popup.Show("You gained {{C|" + this.Points.ToString() + "}} skill points!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				The.Player.GetStat("SP").BaseValue += this.Points;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007CA1 RID: 31905 RVA: 0x002ECFE8 File Offset: 0x002EB1E8
		public override bool HandleEvent(GetChoiceTagEvent E)
		{
			E.Tag = string.Concat(new string[]
			{
				"{{",
				this.Lowlight,
				"|[gain {{",
				this.Numeric,
				"|",
				this.Points.ToString(),
				"}} {{W|skill points}}: {{",
				this.Numeric,
				"|",
				base.GetReputationCost("WaterRitualUse").ToString(),
				"}} reputation]}}"
			});
			return false;
		}

		// Token: 0x0400389C RID: 14492
		public int Points;
	}
}
