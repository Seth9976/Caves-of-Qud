using System;

namespace XRL.World.Parts
{
	// Token: 0x02001032 RID: 4146
	[Serializable]
	public class FungalFortitude : IActivePart
	{
		// Token: 0x0600A525 RID: 42277 RVA: 0x003C6187 File Offset: 0x003C4387
		public FungalFortitude()
		{
			this.WorksOnHolder = true;
			this.WorksOnCarrier = true;
		}

		// Token: 0x0600A526 RID: 42278 RVA: 0x003C61B2 File Offset: 0x003C43B2
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A527 RID: 42279 RVA: 0x003C61B5 File Offset: 0x003C43B5
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.CalculateBonus(true, Amount);
		}

		// Token: 0x0600A528 RID: 42280 RVA: 0x003C61BF File Offset: 0x003C43BF
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID || ID == TakenEvent.ID || ID == DroppedEvent.ID;
		}

		// Token: 0x0600A529 RID: 42281 RVA: 0x003C61E8 File Offset: 0x003C43E8
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.AVBonus > 0)
			{
				E.Postfix.Compound("{{rules|", '\n').AppendSigned(this.AVBonus).Append(" AV per fungal infection");
			}
			if (this.ResistBonus > 0)
			{
				E.Postfix.Compound("{{rules|", '\n').AppendSigned(this.ResistBonus).Append(" to all resists per fungal infection");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A52A RID: 42282 RVA: 0x003C625E File Offset: 0x003C445E
		public override bool HandleEvent(TakenEvent E)
		{
			this.CalculateBonus(false, 1);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A52B RID: 42283 RVA: 0x003C626F File Offset: 0x003C446F
		public override bool HandleEvent(DroppedEvent E)
		{
			this.CalculateBonus(false, 1);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A52C RID: 42284 RVA: 0x003C6280 File Offset: 0x003C4480
		public void CalculateBonus(bool UseCharge = false, int MultipleCharge = 1)
		{
			if (!base.IsReady(UseCharge, false, false, false, false, false, false, false, false, false, MultipleCharge, null, false, 0L, null))
			{
				base.StatShifter.RemoveStatShifts();
				this.LastCount = -1;
				return;
			}
			GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
			int count = 0;
			activePartFirstSubject.ForeachEquippedObject(delegate(GameObject Object)
			{
				if (Object.HasTag("FungalInfection"))
				{
					int count2 = count;
					count = count2 + 1;
				}
			});
			if (this.LastCount == count)
			{
				return;
			}
			this.LastCount = count;
			if (count <= 0)
			{
				base.StatShifter.RemoveStatShifts(activePartFirstSubject);
				return;
			}
			base.StatShifter.SetStatShift(activePartFirstSubject, "AV", this.AVBonus * count, false);
			base.StatShifter.SetStatShift(activePartFirstSubject, "HeatResistance", this.ResistBonus * count, false);
			base.StatShifter.SetStatShift(activePartFirstSubject, "ColdResistance", this.ResistBonus * count, false);
			base.StatShifter.SetStatShift(activePartFirstSubject, "ElectricResistance", this.ResistBonus * count, false);
			base.StatShifter.SetStatShift(activePartFirstSubject, "AcidResistance", this.ResistBonus * count, false);
		}

		// Token: 0x0400444E RID: 17486
		public int AVBonus = 1;

		// Token: 0x0400444F RID: 17487
		public int ResistBonus = 4;

		// Token: 0x04004450 RID: 17488
		[NonSerialized]
		private int LastCount = -1;
	}
}
