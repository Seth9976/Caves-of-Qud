using System;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C26 RID: 3110
	public class WaterRitualHermitOath : IWaterRitualPart
	{
		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06007C69 RID: 31849 RVA: 0x002EBCA1 File Offset: 0x002E9EA1
		public override bool Affordable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007C6A RID: 31850 RVA: 0x002EBCA4 File Offset: 0x002E9EA4
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == GetChoiceTagEvent.ID || ID == EnteredElementEvent.ID || ID == PrepareTextEvent.ID;
		}

		// Token: 0x06007C6B RID: 31851 RVA: 0x002EBCCC File Offset: 0x002E9ECC
		public override void Awake()
		{
			if (WaterRitual.Record.hermitChatPenalty > 0 || WaterRitual.Record.Has("madeHermitOath") || WaterRitual.RecordFaction.WaterRitualHermitOath < 0)
			{
				return;
			}
			this.Reputation = WaterRitual.RecordFaction.WaterRitualHermitOath;
			this.Visible = true;
		}

		// Token: 0x06007C6C RID: 31852 RVA: 0x002EBD1C File Offset: 0x002E9F1C
		public override bool HandleEvent(EnteredElementEvent E)
		{
			WaterRitual.Record.attributes.Add("madeHermitOath");
			WaterRitual.Record.hermitChatPenalty = this.Reputation * 2;
			The.Game.PlayerReputation.Modify(WaterRitual.RecordFaction, this.Reputation, null, null, "WaterRitualHermitOath", false, false, false, false);
			return base.HandleEvent(E);
		}

		// Token: 0x06007C6D RID: 31853 RVA: 0x002EBD7C File Offset: 0x002E9F7C
		public override bool HandleEvent(PrepareTextEvent E)
		{
			E.Text.Replace("=hermit=", The.Speaker.GetPropertyOrTag("HermitOathAddressAs", "hermit"));
			return base.HandleEvent(E);
		}

		// Token: 0x06007C6E RID: 31854 RVA: 0x002EBDAC File Offset: 0x002E9FAC
		public override bool HandleEvent(GetChoiceTagEvent E)
		{
			E.Tag = string.Concat(new string[]
			{
				"{{",
				this.Lowlight,
				"|[{{",
				this.Numeric,
				"|+",
				base.GetReputationAward("WaterRitualHermitOath").ToString(),
				"}} reputation]}}"
			});
			return false;
		}
	}
}
