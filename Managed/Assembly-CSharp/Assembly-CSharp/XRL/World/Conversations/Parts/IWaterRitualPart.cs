using System;
using XRL.UI;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C01 RID: 3073
	public abstract class IWaterRitualPart : IConversationPart
	{
		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06007B70 RID: 31600 RVA: 0x002E5EAC File Offset: 0x002E40AC
		public virtual bool Affordable
		{
			get
			{
				return The.Game.PlayerReputation.Get(WaterRitual.RecordFaction.Name) >= -this.GetReputationCost("WaterRitualUse");
			}
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06007B71 RID: 31601 RVA: 0x002E5ED8 File Offset: 0x002E40D8
		public virtual bool Available
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06007B72 RID: 31602 RVA: 0x002E5EDB File Offset: 0x002E40DB
		public virtual string Highlight
		{
			get
			{
				if (!this.Affordable || !this.Available)
				{
					return "K";
				}
				return "G";
			}
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06007B73 RID: 31603 RVA: 0x002E5EF8 File Offset: 0x002E40F8
		public virtual string Lowlight
		{
			get
			{
				if (!this.Affordable || !this.Available)
				{
					return "K";
				}
				return "g";
			}
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06007B74 RID: 31604 RVA: 0x002E5F15 File Offset: 0x002E4115
		public virtual string Numeric
		{
			get
			{
				if (!this.Affordable)
				{
					return "r";
				}
				if (!this.Available)
				{
					return "";
				}
				return "C";
			}
		}

		// Token: 0x06007B75 RID: 31605 RVA: 0x002E5F38 File Offset: 0x002E4138
		public int GetReputationCost(string Type = "WaterRitualUse")
		{
			return ReputationChangeEvent.GetFor(WaterRitual.RecordFaction, -this.Reputation, Type, true, false, true);
		}

		// Token: 0x06007B76 RID: 31606 RVA: 0x002E5F4F File Offset: 0x002E414F
		public int GetReputationAward(string Type = "WaterRitualAward")
		{
			return ReputationChangeEvent.GetFor(WaterRitual.RecordFaction, this.Reputation, Type, true, false, true);
		}

		// Token: 0x06007B77 RID: 31607 RVA: 0x002E5F68 File Offset: 0x002E4168
		public bool UseReputation(string Type = "WaterRitualUse")
		{
			int num = -this.GetReputationCost(Type);
			if (The.Game.PlayerReputation.Get(WaterRitual.RecordFaction) < num)
			{
				Popup.Show("You don't have a high enough reputation with " + WaterRitual.RecordFaction.GetFormattedName() + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				return false;
			}
			The.Game.PlayerReputation.Modify(WaterRitual.RecordFaction, -this.Reputation, null, null, Type, false, false, false, false);
			return true;
		}

		// Token: 0x06007B78 RID: 31608 RVA: 0x002E5FE4 File Offset: 0x002E41E4
		public void AwardReputation(int Bonus = 0, string Type = "WaterRitualAward")
		{
			int num = Math.Min(this.Reputation, WaterRitual.Record.totalFactionAvailable);
			WaterRitual.Record.totalFactionAvailable -= num;
			The.Game.PlayerReputation.Modify(WaterRitual.RecordFaction, num + Bonus, null, null, Type, false, false, false, false);
		}

		// Token: 0x06007B79 RID: 31609 RVA: 0x002E6038 File Offset: 0x002E4238
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == IsElementVisibleEvent.ID || ID == ColorTextEvent.ID || ID == HideElementEvent.ID || ID == EnteredElementEvent.ID;
		}

		// Token: 0x06007B7A RID: 31610 RVA: 0x002E6066 File Offset: 0x002E4266
		public override bool HandleEvent(IsElementVisibleEvent E)
		{
			return base.HandleEvent(E) && this.Visible;
		}

		// Token: 0x06007B7B RID: 31611 RVA: 0x002E6079 File Offset: 0x002E4279
		public override bool HandleEvent(ColorTextEvent E)
		{
			E.Color = this.Highlight;
			return false;
		}

		// Token: 0x06007B7C RID: 31612 RVA: 0x002E6088 File Offset: 0x002E4288
		public override bool HandleEvent(HideElementEvent E)
		{
			return false;
		}

		// Token: 0x06007B7D RID: 31613 RVA: 0x002E608B File Offset: 0x002E428B
		public override bool HandleEvent(EnteredElementEvent E)
		{
			this.Visible = false;
			this.Awake();
			return base.HandleEvent(E);
		}

		// Token: 0x04003828 RID: 14376
		public bool Visible;

		// Token: 0x04003829 RID: 14377
		public int Reputation;
	}
}
