using System;
using System.Text;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x02001093 RID: 4243
	[Serializable]
	public class SaveModifier : IActivePart
	{
		// Token: 0x0600A8DD RID: 43229 RVA: 0x003DC79D File Offset: 0x003DA99D
		public SaveModifier()
		{
			this.WorksOnEquipper = true;
		}

		// Token: 0x0600A8DE RID: 43230 RVA: 0x003DC7BC File Offset: 0x003DA9BC
		public override bool SameAs(IPart p)
		{
			SaveModifier saveModifier = p as SaveModifier;
			return !(saveModifier.Vs != this.Vs) && saveModifier.Amount == this.Amount && saveModifier.ShowInShortDescription == this.ShowInShortDescription && base.SameAs(p);
		}

		// Token: 0x0600A8DF RID: 43231 RVA: 0x003DC80C File Offset: 0x003DAA0C
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A8E0 RID: 43232 RVA: 0x003DC810 File Offset: 0x003DAA10
		public override void TurnTick(long TimeTick, int Amount1)
		{
			if (!base.OnWorldMap)
			{
				base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, this.Amount, null, false, 0, false, null);
			}
		}

		// Token: 0x0600A8E1 RID: 43233 RVA: 0x003DC850 File Offset: 0x003DAA50
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID || ID == ModifyDefendingSaveEvent.ID;
		}

		// Token: 0x0600A8E2 RID: 43234 RVA: 0x003DC870 File Offset: 0x003DAA70
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.ShowInShortDescription && (this.WorksOnEquipper || this.WorksOnWearer || this.WorksOnHolder || this.WorksOnCarrier))
			{
				E.Postfix.AppendRules(new Action<StringBuilder>(this.AppendRulesDescription), base.GetEventSensitiveAddStatusSummary(E));
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A8E3 RID: 43235 RVA: 0x003DC8CC File Offset: 0x003DAACC
		public override bool HandleEvent(ModifyDefendingSaveEvent E)
		{
			if (this.IsObjectActivePartSubject(E.Defender) && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && SavingThrows.Applicable(this.Vs, E))
			{
				E.Roll += this.Amount;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A8E4 RID: 43236 RVA: 0x003DC937 File Offset: 0x003DAB37
		private void AppendRulesDescription(StringBuilder SB)
		{
			SavingThrows.AppendSaveBonusDescription(SB, this.Amount, this.Vs, false, false, false);
		}

		// Token: 0x040045C5 RID: 17861
		public string Vs;

		// Token: 0x040045C6 RID: 17862
		public int Amount = 1;

		// Token: 0x040045C7 RID: 17863
		public bool ShowInShortDescription = true;
	}
}
