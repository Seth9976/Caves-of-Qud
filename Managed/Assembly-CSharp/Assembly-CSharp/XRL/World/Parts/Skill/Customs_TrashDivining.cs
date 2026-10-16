using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200133A RID: 4922
	[Serializable]
	public class Customs_TrashDivining : BaseSkill
	{
		// Token: 0x0600C044 RID: 49220 RVA: 0x0046751A File Offset: 0x0046571A
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<PartSupportEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C045 RID: 49221 RVA: 0x00467538 File Offset: 0x00465738
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("scholarship", 3);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C046 RID: 49222 RVA: 0x0046755B File Offset: 0x0046575B
		public override bool HandleEvent(PartSupportEvent E)
		{
			return (E.Skip == this || !(E.Type == "TrashRifling")) && base.HandleEvent(E);
		}

		// Token: 0x0600C047 RID: 49223 RVA: 0x00467581 File Offset: 0x00465781
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C048 RID: 49224 RVA: 0x00467584 File Offset: 0x00465784
		public override bool AddSkill(GameObject GO)
		{
			GO.RequirePart<TrashRifling>(false);
			return base.AddSkill(GO);
		}

		// Token: 0x0600C049 RID: 49225 RVA: 0x00467595 File Offset: 0x00465795
		public override bool RemoveSkill(GameObject GO)
		{
			NeedPartSupportEvent.Send(GO, "TrashRifling", this);
			return base.RemoveSkill(GO);
		}
	}
}
