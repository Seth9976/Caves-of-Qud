using System;

namespace XRL.World.Parts
{
	// Token: 0x020010C1 RID: 4289
	[Serializable]
	public class Yurtmat : IPart
	{
		// Token: 0x0600AAC1 RID: 43713 RVA: 0x003E740D File Offset: 0x003E560D
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600AAC2 RID: 43714 RVA: 0x003E7410 File Offset: 0x003E5610
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID || ID == UnequippedEvent.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600AAC3 RID: 43715 RVA: 0x003E7436 File Offset: 0x003E5636
		public override bool HandleEvent(EquippedEvent E)
		{
			E.Actor.RegisterEvent(this, EnteredCellEvent.ID, 0, true);
			this.CheckCamouflage();
			return base.HandleEvent(E);
		}

		// Token: 0x0600AAC4 RID: 43716 RVA: 0x003E7458 File Offset: 0x003E5658
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.UnregisterEvent(this, EnteredCellEvent.ID);
			base.StatShifter.RemoveStatShifts(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AAC5 RID: 43717 RVA: 0x003E7483 File Offset: 0x003E5683
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.CheckCamouflage();
			return base.HandleEvent(E);
		}

		// Token: 0x0600AAC6 RID: 43718 RVA: 0x003E7492 File Offset: 0x003E5692
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(this.Bonus.Signed(false) + " DV while occupying the same tile as foliage");
			return base.HandleEvent(E);
		}

		// Token: 0x0600AAC7 RID: 43719 RVA: 0x003E74C0 File Offset: 0x003E56C0
		private void CheckCamouflage()
		{
			GameObject equipped = this.ParentObject.Equipped;
			Cell cell = ((equipped != null) ? equipped.CurrentCell : null);
			if (cell == null)
			{
				return;
			}
			if (cell.HasObjectWithPartOtherThan(typeof(PlantProperties), equipped))
			{
				base.StatShifter.DefaultDisplayName = "camouflage";
				base.StatShifter.SetStatShift(equipped, "DV", this.Bonus, false);
				return;
			}
			base.StatShifter.RemoveStatShifts(equipped);
		}

		// Token: 0x04004665 RID: 18021
		public int Bonus = 2;
	}
}
