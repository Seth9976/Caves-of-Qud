using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F1C RID: 3868
	[Serializable]
	public class CyberneticsBionicLiver : IPart
	{
		// Token: 0x06009A2E RID: 39470 RVA: 0x0037F54F File Offset: 0x0037D74F
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009A2F RID: 39471 RVA: 0x0037F552 File Offset: 0x0037D752
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x06009A30 RID: 39472 RVA: 0x0037F555 File Offset: 0x0037D755
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeApplyDamageEvent.ID || ID == ImplantedEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x06009A31 RID: 39473 RVA: 0x0037F57C File Offset: 0x0037D77C
		public override bool HandleEvent(BeforeApplyDamageEvent E)
		{
			if (E.Object == this.ParentObject.Implantee && E.Damage.HasAttribute("Poison"))
			{
				NotifyTargetImmuneEvent.Send(E.Weapon, E.Object, E.Actor, E.Damage, this);
				E.Damage.Amount = 0;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009A32 RID: 39474 RVA: 0x0037F5E4 File Offset: 0x0037D7E4
		public override bool HandleEvent(ImplantedEvent E)
		{
			E.Implantee.RegisterPartEvent(this, "BeforeApplyDamage");
			E.Implantee.RegisterPartEvent(this, "CanApplyPoison");
			E.Implantee.RegisterPartEvent(this, "ApplyPoison");
			E.Implantee.RegisterPartEvent(this, "CanApplyPoisonGasPoison");
			E.Implantee.RegisterPartEvent(this, "ApplyPoisonGasPoison");
			E.Implantee.RegisterPartEvent(this, "CanApplyDisease");
			E.Implantee.RegisterPartEvent(this, "ApplyDisease");
			E.Implantee.RegisterPartEvent(this, "CanApplyDiseaseOnset");
			E.Implantee.RegisterPartEvent(this, "ApplyDiseaseOnset");
			return base.HandleEvent(E);
		}

		// Token: 0x06009A33 RID: 39475 RVA: 0x0037F694 File Offset: 0x0037D894
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.UnregisterPartEvent(this, "BeforeApplyDamage");
			E.Implantee.UnregisterPartEvent(this, "CanApplyPoison");
			E.Implantee.UnregisterPartEvent(this, "ApplyPoison");
			E.Implantee.UnregisterPartEvent(this, "CanApplyPoisonGasPoison");
			E.Implantee.UnregisterPartEvent(this, "ApplyPoisonGasPoison");
			E.Implantee.UnregisterPartEvent(this, "CanApplyDisease");
			E.Implantee.UnregisterPartEvent(this, "ApplyDisease");
			E.Implantee.UnregisterPartEvent(this, "CanApplyDiseaseOnset");
			E.Implantee.UnregisterPartEvent(this, "ApplyDiseaseOnset");
			return base.HandleEvent(E);
		}

		// Token: 0x06009A34 RID: 39476 RVA: 0x0037F744 File Offset: 0x0037D944
		public override bool FireEvent(Event E)
		{
			return !(E.ID == "CanApplyPoison") && !(E.ID == "ApplyPoison") && !(E.ID == "CanApplyPoisonGasPoison") && !(E.ID == "ApplyPoisonGasPoison") && !(E.ID == "CanApplyDisease") && !(E.ID == "ApplyDisease") && !(E.ID == "CanApplyDiseaseOnset") && !(E.ID == "ApplyDiseaseOnset") && base.FireEvent(E);
		}
	}
}
