using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x020013BA RID: 5050
	[Serializable]
	public class Tinkering_GadgetInspector : BaseSkill
	{
		// Token: 0x0600C2EC RID: 49900 RVA: 0x00472AED File Offset: 0x00470CED
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetTinkeringBonusEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C2ED RID: 49901 RVA: 0x00472B0B File Offset: 0x00470D0B
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("scholarship", 2);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C2EE RID: 49902 RVA: 0x00472B2E File Offset: 0x00470D2E
		public override bool HandleEvent(GetTinkeringBonusEvent E)
		{
			if (E.Type == "Inspect")
			{
				E.Bonus += 5;
			}
			return base.HandleEvent(E);
		}
	}
}
