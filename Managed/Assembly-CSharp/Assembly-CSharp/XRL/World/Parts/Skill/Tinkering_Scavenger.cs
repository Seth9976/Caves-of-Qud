using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x020013BE RID: 5054
	[Serializable]
	public class Tinkering_Scavenger : BaseSkill
	{
		// Token: 0x0600C314 RID: 49940 RVA: 0x00473D4F File Offset: 0x00471F4F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<PartSupportEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C315 RID: 49941 RVA: 0x00473D6D File Offset: 0x00471F6D
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("scholarship", 2);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C316 RID: 49942 RVA: 0x00473D90 File Offset: 0x00471F90
		public override bool HandleEvent(PartSupportEvent E)
		{
			return (E.Skip == this || !(E.Type == "TrashRifling")) && base.HandleEvent(E);
		}

		// Token: 0x0600C317 RID: 49943 RVA: 0x00473DB6 File Offset: 0x00471FB6
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C318 RID: 49944 RVA: 0x00473DB9 File Offset: 0x00471FB9
		public override bool AddSkill(GameObject GO)
		{
			GO.RequirePart<TrashRifling>(false);
			return base.AddSkill(GO);
		}

		// Token: 0x0600C319 RID: 49945 RVA: 0x00473DCA File Offset: 0x00471FCA
		public override bool RemoveSkill(GameObject GO)
		{
			NeedPartSupportEvent.Send(GO, "TrashRifling", this);
			return base.RemoveSkill(GO);
		}
	}
}
