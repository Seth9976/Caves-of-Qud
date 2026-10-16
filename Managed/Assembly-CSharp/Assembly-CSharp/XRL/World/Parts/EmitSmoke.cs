using System;

namespace XRL.World.Parts
{
	// Token: 0x02001023 RID: 4131
	[Serializable]
	public class EmitSmoke : IActivePart
	{
		// Token: 0x0600A480 RID: 42112 RVA: 0x003C2244 File Offset: 0x003C0444
		public EmitSmoke()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600A481 RID: 42113 RVA: 0x003C226C File Offset: 0x003C046C
		public override bool SameAs(IPart p)
		{
			EmitSmoke emitSmoke = p as EmitSmoke;
			return emitSmoke.ChanceOnEndTurn == this.ChanceOnEndTurn && emitSmoke.ChanceOnEquipperEnteredCell == this.ChanceOnEquipperEnteredCell && !(emitSmoke.StartAngle != this.StartAngle) && !(emitSmoke.EndAngle != this.EndAngle) && base.SameAs(p);
		}

		// Token: 0x0600A482 RID: 42114 RVA: 0x003C22D1 File Offset: 0x003C04D1
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == SingletonEvent<EndTurnEvent>.ID && this.ChanceOnEndTurn > 0) || (ID == EquippedEvent.ID && this.ChanceOnEquipperEnteredCell > 0) || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600A483 RID: 42115 RVA: 0x003C2309 File Offset: 0x003C0509
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.ChanceOnEndTurn.in100())
			{
				this.Smoke();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A484 RID: 42116 RVA: 0x003C2326 File Offset: 0x003C0526
		public override bool HandleEvent(EquippedEvent E)
		{
			E.Actor.RegisterPartEvent(this, "EnteredCell");
			return base.HandleEvent(E);
		}

		// Token: 0x0600A485 RID: 42117 RVA: 0x003C2340 File Offset: 0x003C0540
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.UnregisterPartEvent(this, "EnteredCell");
			return base.HandleEvent(E);
		}

		// Token: 0x0600A486 RID: 42118 RVA: 0x003C235A File Offset: 0x003C055A
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600A487 RID: 42119 RVA: 0x003C235D File Offset: 0x003C055D
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell" && this.ChanceOnEquipperEnteredCell.in100())
			{
				this.ParentObject.Smoke();
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600A488 RID: 42120 RVA: 0x003C2390 File Offset: 0x003C0590
		public bool Smoke()
		{
			if (!base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				return false;
			}
			this.ParentObject.Smoke(this.StartAngle.RollCached(), this.EndAngle.RollCached());
			return true;
		}

		// Token: 0x0400442B RID: 17451
		public int ChanceOnEndTurn;

		// Token: 0x0400442C RID: 17452
		public int ChanceOnEquipperEnteredCell;

		// Token: 0x0400442D RID: 17453
		public string StartAngle = "85";

		// Token: 0x0400442E RID: 17454
		public string EndAngle = "185";
	}
}
