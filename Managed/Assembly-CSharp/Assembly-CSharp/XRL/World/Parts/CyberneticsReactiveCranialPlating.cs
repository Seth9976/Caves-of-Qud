using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F42 RID: 3906
	[Serializable]
	public class CyberneticsReactiveCranialPlating : IPart
	{
		// Token: 0x06009BD1 RID: 39889 RVA: 0x00388409 File Offset: 0x00386609
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009BD2 RID: 39890 RVA: 0x0038840C File Offset: 0x0038660C
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x06009BD3 RID: 39891 RVA: 0x0038840F File Offset: 0x0038660F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ImplantedEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x06009BD4 RID: 39892 RVA: 0x00388430 File Offset: 0x00386630
		public override bool HandleEvent(ImplantedEvent E)
		{
			E.Implantee.RegisterPartEvent(this, "CanApplyDazed");
			E.Implantee.RegisterPartEvent(this, "ApplyDazed");
			E.Implantee.RegisterPartEvent(this, "CanApplyStun");
			E.Implantee.RegisterPartEvent(this, "ApplyStun");
			return base.HandleEvent(E);
		}

		// Token: 0x06009BD5 RID: 39893 RVA: 0x00388488 File Offset: 0x00386688
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.UnregisterPartEvent(this, "CanApplyDazed");
			E.Implantee.UnregisterPartEvent(this, "ApplyDazed");
			E.Implantee.UnregisterPartEvent(this, "CanApplyStun");
			E.Implantee.UnregisterPartEvent(this, "ApplyStun");
			return base.HandleEvent(E);
		}

		// Token: 0x06009BD6 RID: 39894 RVA: 0x003884E0 File Offset: 0x003866E0
		public override bool FireEvent(Event E)
		{
			return !(E.ID == "CanApplyDazed") && !(E.ID == "ApplyDazed") && !(E.ID == "CanApplyStun") && !(E.ID == "ApplyStun") && base.FireEvent(E);
		}
	}
}
