using System;
using System.Text;

namespace XRL.World.Parts
{
	// Token: 0x02000E3D RID: 3645
	[Serializable]
	public class CarryBonus : IActivePart
	{
		// Token: 0x060093D8 RID: 37848 RVA: 0x00353A69 File Offset: 0x00351C69
		public CarryBonus()
		{
			this.WorksOnEquipper = true;
		}

		// Token: 0x060093D9 RID: 37849 RVA: 0x00353A83 File Offset: 0x00351C83
		public CarryBonus(int Amount, string Style = "Flat")
			: this()
		{
			this.Amount = Amount;
		}

		// Token: 0x060093DA RID: 37850 RVA: 0x00353A92 File Offset: 0x00351C92
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x060093DB RID: 37851 RVA: 0x00353A98 File Offset: 0x00351C98
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetMaxCarriedWeightEvent.ID || ID == GetShortDescriptionEvent.ID || (this.WorksOnEquipper && ID == EquippedEvent.ID) || (this.WorksOnImplantee && ID == ImplantedEvent.ID) || (this.WorksOnEquipper && ID == UnequippedEvent.ID) || (this.WorksOnImplantee && ID == UnimplantedEvent.ID);
		}

		// Token: 0x060093DC RID: 37852 RVA: 0x00353B04 File Offset: 0x00351D04
		public override bool HandleEvent(GetMaxCarriedWeightEvent E)
		{
			if (this.BonusApplied || (this.WorksOnSelf && E.Object == this.ParentObject))
			{
				if (this.Style == "Flat")
				{
					E.Weight += (double)this.Amount;
				}
				else
				{
					E.AdjustWeight((double)(100 + this.Amount) / 100.0);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060093DD RID: 37853 RVA: 0x00353B78 File Offset: 0x00351D78
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.Amount > 0)
			{
				E.Postfix.AppendRules(new Action<StringBuilder>(this.AppendEffect));
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060093DE RID: 37854 RVA: 0x00353BA2 File Offset: 0x00351DA2
		public void AppendEffect(StringBuilder SB)
		{
			SB.AppendSigned(this.Amount).Append((this.Style == "Flat") ? " lbs. carry capacity" : "% carry capacity");
		}

		// Token: 0x060093DF RID: 37855 RVA: 0x00353BD4 File Offset: 0x00351DD4
		public override bool HandleEvent(EquippedEvent E)
		{
			this.BonusApplied = this.ParentObject.IsEquippedProperly(E.Part);
			return base.HandleEvent(E);
		}

		// Token: 0x060093E0 RID: 37856 RVA: 0x00353BF4 File Offset: 0x00351DF4
		public override bool HandleEvent(UnequippedEvent E)
		{
			this.BonusApplied = false;
			return base.HandleEvent(E);
		}

		// Token: 0x060093E1 RID: 37857 RVA: 0x00353C04 File Offset: 0x00351E04
		public override bool HandleEvent(ImplantedEvent E)
		{
			this.BonusApplied = this.ParentObject.IsEquippedProperly(E.Part);
			return base.HandleEvent(E);
		}

		// Token: 0x060093E2 RID: 37858 RVA: 0x00353C24 File Offset: 0x00351E24
		public override bool HandleEvent(UnimplantedEvent E)
		{
			this.BonusApplied = false;
			return base.HandleEvent(E);
		}

		// Token: 0x04003D2A RID: 15658
		public string Style = "Flat";

		// Token: 0x04003D2B RID: 15659
		public int Amount;

		// Token: 0x04003D2C RID: 15660
		public bool BonusApplied;
	}
}
