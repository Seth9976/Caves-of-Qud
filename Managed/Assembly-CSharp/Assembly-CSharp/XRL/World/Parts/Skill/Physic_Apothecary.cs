using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001362 RID: 4962
	[Serializable]
	public class Physic_Apothecary : BaseSkill
	{
		// Token: 0x0600C135 RID: 49461 RVA: 0x0046B3CE File Offset: 0x004695CE
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetTonicDurationEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C136 RID: 49462 RVA: 0x0046B3EC File Offset: 0x004695EC
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("scholarship", 3);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C137 RID: 49463 RVA: 0x0046B410 File Offset: 0x00469610
		public override bool HandleEvent(GetTonicDurationEvent E)
		{
			if (E.Pass == 1 && E.Healing && E.Checking == "Actor" && E.Actor == this.ParentObject)
			{
				E.Duration++;
			}
			return base.HandleEvent(E);
		}
	}
}
