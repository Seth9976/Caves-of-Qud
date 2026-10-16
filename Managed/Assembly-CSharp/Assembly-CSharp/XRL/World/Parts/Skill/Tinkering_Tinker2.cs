using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x020013C0 RID: 5056
	[Serializable]
	public class Tinkering_Tinker2 : BaseSkill
	{
		// Token: 0x0600C323 RID: 49955 RVA: 0x00474104 File Offset: 0x00472304
		public override bool AddSkill(GameObject GO)
		{
			if (GO.GetIntProperty("ReceivedTinker2Recipe", 0) <= 0 && (GO.CurrentCell != null || GO.IsOriginalPlayerBody()))
			{
				Tinkering.LearnNewRecipe(GO, 4, 6);
				GO.SetIntProperty("ReceivedTinker2Recipe", 1, false);
			}
			if (GO.IsPlayer())
			{
				TinkeringSifrah.AwardInsight();
			}
			return base.AddSkill(GO);
		}

		// Token: 0x0600C324 RID: 49956 RVA: 0x0047415A File Offset: 0x0047235A
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C325 RID: 49957 RVA: 0x00474170 File Offset: 0x00472370
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
