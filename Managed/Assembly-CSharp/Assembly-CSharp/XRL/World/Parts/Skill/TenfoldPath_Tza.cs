using System;

namespace XRL.World.Parts.Skill
{
	/// This part is not used in the base game.
	// Token: 0x020013B5 RID: 5045
	[Serializable]
	public class TenfoldPath_Tza : BaseInitiatorySkill
	{
		// Token: 0x0600C2C3 RID: 49859 RVA: 0x00472134 File Offset: 0x00470334
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetCooldownEvent>.ID;
		}

		// Token: 0x0600C2C4 RID: 49860 RVA: 0x0047214A File Offset: 0x0047034A
		public override bool HandleEvent(GetCooldownEvent E)
		{
			if (E.Ability.Class == "Mental Mutations")
			{
				E.PercentageReduction += 10;
			}
			return base.HandleEvent(E);
		}
	}
}
