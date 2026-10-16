using System;
using XRL.Rules;
using XRL.UI;
using XRL.World.AI;
using XRL.World.Effects;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C27 RID: 3111
	public class WaterRitualJoinParty : IWaterRitualPart
	{
		// Token: 0x06007C70 RID: 31856 RVA: 0x002EBE1B File Offset: 0x002EA01B
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == GetChoiceTagEvent.ID || ID == EnteredElementEvent.ID;
		}

		// Token: 0x06007C71 RID: 31857 RVA: 0x002EBE3C File Offset: 0x002EA03C
		public override void Awake()
		{
			if (!WaterRitual.RecordFaction.WaterRitualJoin || The.Speaker.IsPlayerLed() || The.Speaker.GetxTag("WaterRitual", "NoJoin", null) == "true")
			{
				return;
			}
			this.Reputation = Math.Max(50, 200 + (The.Speaker.Stat("Level", 0) - The.Player.Stat("Level", 0)) * (int)((double)RuleSettings.REPUTATION_BASE_UNIT * 0.25));
			this.Reputation = GetWaterRitualCostEvent.GetFor(The.Player, The.Speaker, "Join", this.Reputation);
			this.Visible = true;
		}

		// Token: 0x06007C72 RID: 31858 RVA: 0x002EBEF0 File Offset: 0x002EA0F0
		public override bool HandleEvent(EnteredElementEvent E)
		{
			if (base.UseReputation("WaterRitualUse"))
			{
				The.Speaker.SetAlliedLeader<AllyProselytize>(The.Player, 0, false);
				Lovesick lovesick;
				if (The.Speaker.TryGetEffect<Lovesick>(out lovesick))
				{
					lovesick.PreviousLeader = The.Player;
				}
				Popup.Show(The.Speaker.Does("join", int.MaxValue, null, null, null, false, false, false, false, true, true, true, false, false, null, false, false, true, null, true, null, false) + " you!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007C73 RID: 31859 RVA: 0x002EBF88 File Offset: 0x002EA188
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
	}
}
