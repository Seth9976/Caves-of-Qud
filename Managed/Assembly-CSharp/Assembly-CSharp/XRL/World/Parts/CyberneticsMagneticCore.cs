using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F32 RID: 3890
	[Serializable]
	public class CyberneticsMagneticCore : IPart
	{
		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06009B10 RID: 39696 RVA: 0x00383EDD File Offset: 0x003820DD
		public string ManagerID
		{
			get
			{
				return this.ParentObject.ID + "::CyberneticsMagneticCore";
			}
		}

		// Token: 0x06009B11 RID: 39697 RVA: 0x00383EF4 File Offset: 0x003820F4
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009B12 RID: 39698 RVA: 0x00383EF7 File Offset: 0x003820F7
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ImplantedEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x06009B13 RID: 39699 RVA: 0x00383F18 File Offset: 0x00382118
		public override bool HandleEvent(ImplantedEvent E)
		{
			E.Part.ParentBody.GetBody().AddPart("Floating Nearby", 0, null, null, null, null, this.ManagerID, null, null, null, null, null, null, null, null, null, null, null, null, null, true);
			return base.HandleEvent(E);
		}

		// Token: 0x06009B14 RID: 39700 RVA: 0x00383FC8 File Offset: 0x003821C8
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.RemoveBodyPartsByManager(this.ManagerID, true);
			return base.HandleEvent(E);
		}

		// Token: 0x06009B15 RID: 39701 RVA: 0x00383FE4 File Offset: 0x003821E4
		public override bool AllowStaticRegistration()
		{
			return true;
		}
	}
}
