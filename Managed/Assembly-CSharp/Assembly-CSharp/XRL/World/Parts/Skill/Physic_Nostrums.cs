using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001363 RID: 4963
	[Serializable]
	public class Physic_Nostrums : BaseSkill
	{
		// Token: 0x0600C139 RID: 49465 RVA: 0x0046B46B File Offset: 0x0046966B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C13A RID: 49466 RVA: 0x0046B481 File Offset: 0x00469681
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("scholarship", 3);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C13B RID: 49467 RVA: 0x0046B4A4 File Offset: 0x004696A4
		public override bool AllowStaticRegistration()
		{
			return true;
		}
	}
}
