using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001328 RID: 4904
	[Serializable]
	public class CookingAndGathering_MealPreparation : BaseSkill
	{
		// Token: 0x0600BFC5 RID: 49093 RVA: 0x0046391E File Offset: 0x00461B1E
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600BFC6 RID: 49094 RVA: 0x00463934 File Offset: 0x00461B34
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("salt", 3);
			}
			return base.HandleEvent(E);
		}
	}
}
