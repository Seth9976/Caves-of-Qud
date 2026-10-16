using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001329 RID: 4905
	[Serializable]
	public class CookingAndGathering_Spicer : BaseSkill
	{
		// Token: 0x0600BFC8 RID: 49096 RVA: 0x0046395F File Offset: 0x00461B5F
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600BFC9 RID: 49097 RVA: 0x00463975 File Offset: 0x00461B75
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
