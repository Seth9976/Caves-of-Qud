using System;
using XRL.World.Anatomy;

namespace XRL.World.Parts
{
	// Token: 0x02000F27 RID: 3879
	[Serializable]
	public class CyberneticsEquipmentRack : IPart
	{
		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06009AAE RID: 39598 RVA: 0x00381B83 File Offset: 0x0037FD83
		public string ManagerID
		{
			get
			{
				return this.ParentObject.ID + "::CyberneticsEquipmentRack";
			}
		}

		// Token: 0x06009AAF RID: 39599 RVA: 0x00381B9A File Offset: 0x0037FD9A
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009AB0 RID: 39600 RVA: 0x00381B9D File Offset: 0x0037FD9D
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009AB1 RID: 39601 RVA: 0x00381BA0 File Offset: 0x0037FDA0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ImplantedEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x06009AB2 RID: 39602 RVA: 0x00381BC0 File Offset: 0x0037FDC0
		public override bool HandleEvent(ImplantedEvent E)
		{
			BodyPart body = E.Part.ParentBody.GetBody();
			string text = "Equipment Rack";
			int num = 0;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			string text5 = null;
			string managerID = this.ManagerID;
			string[] array = new string[] { "Missile Weapon", "Hands", "Feet", "Roots", "Thrown Weapon" };
			body.AddPartAt(text, num, text2, text3, text4, text5, managerID, null, null, null, null, null, null, null, null, null, null, null, null, null, "Back", array, true);
			E.Implantee.WantToReequip();
			return base.HandleEvent(E);
		}

		// Token: 0x06009AB3 RID: 39603 RVA: 0x00381CB0 File Offset: 0x0037FEB0
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.RemoveBodyPartsByManager(this.ManagerID, true);
			E.Implantee.WantToReequip();
			return base.HandleEvent(E);
		}
	}
}
