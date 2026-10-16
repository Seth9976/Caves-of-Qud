using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F2B RID: 3883
	[Serializable]
	public class CyberneticsGraftedMirrorArm : IPart
	{
		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06009ACF RID: 39631 RVA: 0x003822B9 File Offset: 0x003804B9
		public string ManagerID
		{
			get
			{
				return this.ParentObject.ID + "::CyberneticsGraftedMirrorArm";
			}
		}

		// Token: 0x06009AD0 RID: 39632 RVA: 0x003822D0 File Offset: 0x003804D0
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009AD1 RID: 39633 RVA: 0x003822D3 File Offset: 0x003804D3
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009AD2 RID: 39634 RVA: 0x003822D6 File Offset: 0x003804D6
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ImplantedEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x06009AD3 RID: 39635 RVA: 0x003822F4 File Offset: 0x003804F4
		public override bool HandleEvent(ImplantedEvent E)
		{
			E.Part.ParentBody.GetBody().AddPartAt("Thrown Weapon", 0, null, null, null, null, this.ManagerID, null, null, null, null, null, null, null, null, null, null, null, null, null, "Thrown Weapon", null, true);
			E.Implantee.WantToReequip();
			return base.HandleEvent(E);
		}

		// Token: 0x06009AD4 RID: 39636 RVA: 0x003823B5 File Offset: 0x003805B5
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.RemoveBodyPartsByManager(this.ManagerID, true);
			E.Implantee.WantToReequip();
			return base.HandleEvent(E);
		}
	}
}
