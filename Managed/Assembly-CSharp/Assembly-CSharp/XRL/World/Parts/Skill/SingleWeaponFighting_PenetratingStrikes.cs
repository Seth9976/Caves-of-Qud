using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001394 RID: 5012
	[Serializable]
	public class SingleWeaponFighting_PenetratingStrikes : ISingleWeaponFightingSkill
	{
		// Token: 0x0600C21D RID: 49693 RVA: 0x0046EA4E File Offset: 0x0046CC4E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetAttackerMeleePenetrationEvent.ID;
		}

		// Token: 0x0600C21E RID: 49694 RVA: 0x0046EA64 File Offset: 0x0046CC64
		public override bool HandleEvent(GetAttackerMeleePenetrationEvent E)
		{
			if (this.AbilityEntry.ToggleState && (E.Defender == null || E.Defender.IsCombatObject() || !E.Defender.IsWall()))
			{
				E.Penetrations++;
			}
			return base.HandleEvent(E);
		}
	}
}
