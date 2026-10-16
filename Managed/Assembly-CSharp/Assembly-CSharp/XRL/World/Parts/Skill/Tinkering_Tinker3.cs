using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x020013C1 RID: 5057
	[Serializable]
	public class Tinkering_Tinker3 : BaseSkill
	{
		// Token: 0x0600C327 RID: 49959 RVA: 0x0047419C File Offset: 0x0047239C
		public override bool AddSkill(GameObject GO)
		{
			if (GO.GetIntProperty("ReceivedTinker3Recipe", 0) <= 0 && (GO.CurrentCell != null || GO.IsOriginalPlayerBody()))
			{
				Tinkering.LearnNewRecipe(GO, 7, 8);
				GO.SetIntProperty("ReceivedTinker3Recipe", 1, false);
			}
			if (GO.IsPlayer())
			{
				TinkeringSifrah.AwardInsight();
			}
			return base.AddSkill(GO);
		}

		// Token: 0x0600C328 RID: 49960 RVA: 0x004741F2 File Offset: 0x004723F2
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C329 RID: 49961 RVA: 0x00474208 File Offset: 0x00472408
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("scholarship", 2);
			}
			return base.HandleEvent(E);
		}
	}
}
