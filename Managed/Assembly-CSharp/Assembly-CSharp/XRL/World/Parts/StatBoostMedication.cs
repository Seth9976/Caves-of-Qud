using System;
using XRL.Rules;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020010C9 RID: 4297
	[Serializable]
	public class StatBoostMedication : IPart
	{
		// Token: 0x0600AAFC RID: 43772 RVA: 0x003E8AD0 File Offset: 0x003E6CD0
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600AAFD RID: 43773 RVA: 0x003E8AD3 File Offset: 0x003E6CD3
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600AAFE RID: 43774 RVA: 0x003E8AEC File Offset: 0x003E6CEC
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Apply" && E.Actor.Statistics.ContainsKey(this.Stat))
			{
				string text;
				string text2;
				if (E.Actor.IsTrueKin())
				{
					text = this.TrueManBoost;
					text2 = this.TrueManDuration;
					E.Actor.Statistics[this.Stat].BaseValue += XRL.Rules.Stat.Roll(this.TrueManPermanent, null);
				}
				else
				{
					text = this.MutantBoost;
					text2 = this.MutantDuration;
					E.Actor.Statistics[this.Stat].BaseValue += XRL.Rules.Stat.Roll(this.MutantPermanent, null);
				}
				int num = XRL.Rules.Stat.Roll(text, null);
				int num2 = XRL.Rules.Stat.Roll(text2, null);
				E.Actor.ApplyEffect(new BoostStatistic(num2, this.Stat, num), null);
				if (E.Actor.IsPlayer())
				{
					Popup.Show(this.Message, null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				this.ParentObject.Destroy(null, false, false, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0400467A RID: 18042
		public string Stat = "Agility";

		// Token: 0x0400467B RID: 18043
		public string TrueManBoost = "2d6";

		// Token: 0x0400467C RID: 18044
		public string TrueManDuration = "3d20";

		// Token: 0x0400467D RID: 18045
		public string TrueManPermanent = "0";

		// Token: 0x0400467E RID: 18046
		public string MutantBoost = "1d6";

		// Token: 0x0400467F RID: 18047
		public string MutantDuration = "1d20";

		// Token: 0x04004680 RID: 18048
		public string MutantPermanent = "0";

		// Token: 0x04004681 RID: 18049
		public string Message = "The tube hisses against your skin, and your blood burns!";
	}
}
