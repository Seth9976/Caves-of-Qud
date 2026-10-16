using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200133D RID: 4925
	[Serializable]
	public class Endurance_Juicer : BaseSkill
	{
		// Token: 0x0600C050 RID: 49232 RVA: 0x004675F9 File Offset: 0x004657F9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<GetTonicCapacityEvent>.ID;
		}

		// Token: 0x0600C051 RID: 49233 RVA: 0x0046760F File Offset: 0x0046580F
		public override bool HandleEvent(GetTonicCapacityEvent E)
		{
			E.Capacity++;
			return base.HandleEvent(E);
		}
	}
}
