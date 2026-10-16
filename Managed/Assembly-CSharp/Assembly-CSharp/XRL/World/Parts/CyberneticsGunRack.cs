using System;
using XRL.World.Anatomy;

namespace XRL.World.Parts
{
	// Token: 0x02000F2C RID: 3884
	[Serializable]
	public class CyberneticsGunRack : IPart
	{
		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06009AD6 RID: 39638 RVA: 0x003823E4 File Offset: 0x003805E4
		public string ManagerID
		{
			get
			{
				return this.ParentObject.ID + "::CyberneticsGunRack";
			}
		}

		// Token: 0x06009AD7 RID: 39639 RVA: 0x003823FB File Offset: 0x003805FB
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009AD8 RID: 39640 RVA: 0x003823FE File Offset: 0x003805FE
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ImplantedEvent.ID || ID == UnimplantedEvent.ID || ID == PooledEvent<BeforeDismemberEvent>.ID;
		}

		// Token: 0x06009AD9 RID: 39641 RVA: 0x00382424 File Offset: 0x00380624
		public override bool HandleEvent(ImplantedEvent E)
		{
			BodyPart body = E.Part.ParentBody.GetBody();
			BodyPart bodyPart = body;
			string text = "Hardpoint";
			int num = 2;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			string text5 = null;
			int? num2 = new int?(6);
			string managerID = this.ManagerID;
			int? num3 = num2;
			string[] array = new string[] { "Hands", "Feet", "Roots", "Thrown Weapon" };
			bool? flag = new bool?(true);
			BodyPart bodyPart2 = bodyPart.AddPartAt(text, num, text2, text3, text4, text5, managerID, num3, null, null, null, flag, null, null, null, null, null, null, null, null, "Missile Weapon", array, true);
			BodyPart bodyPart3 = body;
			BodyPart bodyPart4 = bodyPart2;
			string text6 = "Hardpoint";
			int num4 = 1;
			string text7 = null;
			string text8 = null;
			string text9 = null;
			string text10 = null;
			num2 = new int?(6);
			string managerID2 = this.ManagerID;
			int? num5 = num2;
			flag = new bool?(true);
			bodyPart3.AddPartAt(bodyPart4, text6, num4, text7, text8, text9, text10, managerID2, num5, null, null, null, flag, null, null, null, null, null, null, null, null, true);
			E.Implantee.WantToReequip();
			return base.HandleEvent(E);
		}

		// Token: 0x06009ADA RID: 39642 RVA: 0x003825B4 File Offset: 0x003807B4
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Part.ParentBody.RemovePartsByManager(this.ManagerID, true);
			E.Implantee.WantToReequip();
			return base.HandleEvent(E);
		}

		// Token: 0x06009ADB RID: 39643 RVA: 0x003825E0 File Offset: 0x003807E0
		public override bool HandleEvent(BeforeDismemberEvent E)
		{
			if (E.IsObject)
			{
				BodyPart part = E.Part;
				if (((part != null) ? part.Cybernetics : null) == this.ParentObject)
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009ADC RID: 39644 RVA: 0x0038260D File Offset: 0x0038080D
		public override bool AllowStaticRegistration()
		{
			return true;
		}
	}
}
