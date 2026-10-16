using System;
using System.Text;
using HistoryKit;
using XRL.Language;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x0200129A RID: 4762
	[Serializable]
	public class TempleDedicationPlaque : IPlaquePart
	{
		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x0600BC47 RID: 48199 RVA: 0x0044D558 File Offset: 0x0044B758
		public override string BaseInscription
		{
			get
			{
				string text = "TempleDedicationPlaque";
				GameObject gameObject = this.GetAnyBasisZone().GetCell(0, 0).RequireObject("Widget", null, null, null, null);
				string text2 = gameObject.GetStringProperty(text, null);
				if (!text2.IsNullOrEmpty())
				{
					return text2;
				}
				text2 = this.GenerateInscription();
				gameObject.SetStringProperty(text, text2, false);
				return text2;
			}
		}

		// Token: 0x0600BC48 RID: 48200 RVA: 0x0044D5AB File Offset: 0x0044B7AB
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BC49 RID: 48201 RVA: 0x0044D5AE File Offset: 0x0044B7AE
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEvent.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600BC4A RID: 48202 RVA: 0x0044D5D4 File Offset: 0x0044B7D4
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return !E.Actor.IsPlayer() && base.HandleEvent(E);
		}

		// Token: 0x0600BC4B RID: 48203 RVA: 0x0044D5EC File Offset: 0x0044B7EC
		public override bool HandleEvent(CommandSmartUseEvent E)
		{
			if (E.Actor.IsPlayer())
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				if (currentCell != null)
				{
					Look.ShowLooker(0, currentCell.X, currentCell.Y);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BC4C RID: 48204 RVA: 0x0044D630 File Offset: 0x0044B830
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Prefix.Append(this.Prefix);
			E.Base.Clear();
			E.Base.Append(this.Inscription);
			E.Postfix.Append(this.Postfix);
			return base.HandleEvent(E);
		}

		// Token: 0x0600BC4D RID: 48205 RVA: 0x0044D686 File Offset: 0x0044B886
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600BC4E RID: 48206 RVA: 0x0044D68C File Offset: 0x0044B88C
		public string GenerateInscription()
		{
			Random seededRandomGenerator = Stat.GetSeededRandomGenerator("TempleDedicationPlaque");
			StringBuilder stringBuilder = Event.NewStringBuilder("This temple was built in 638,01qy by the Exhaustiers' Guild, who detached from their egregore ");
			stringBuilder.Append(Grammar.MakeTitleCase(HistoricStringExpander.ExpandString("<spice.egregorePrefix.!random> <spice.nouns.!random>", seededRandomGenerator)));
			stringBuilder.Append(" in the ");
			stringBuilder.Append(Grammar.MakeTitleCase(HistoricStringExpander.ExpandString("<spice.nouns.!random> Era.", seededRandomGenerator)));
			return Event.FinalizeString(stringBuilder);
		}

		// Token: 0x04004B73 RID: 19315
		public string Prefix = "";

		// Token: 0x04004B74 RID: 19316
		public string Postfix = "";
	}
}
