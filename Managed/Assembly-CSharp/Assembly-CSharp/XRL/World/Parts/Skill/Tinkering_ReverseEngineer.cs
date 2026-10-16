using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x020013BD RID: 5053
	[Serializable]
	public class Tinkering_ReverseEngineer : BaseSkill
	{
		// Token: 0x0600C311 RID: 49937 RVA: 0x00473D0E File Offset: 0x00471F0E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C312 RID: 49938 RVA: 0x00473D24 File Offset: 0x00471F24
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
