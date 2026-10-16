using System;

namespace XRL.World
{
	// Token: 0x02000958 RID: 2392
	public interface IThrownWeaponFlexPhaseProvider
	{
		// Token: 0x060062E7 RID: 25319
		void ThrownWeaponFlexPhaseStart(GameObject Weapon);

		// Token: 0x060062E8 RID: 25320
		void ThrownWeaponFlexPhaseEnd(GameObject Weapon);

		// Token: 0x060062E9 RID: 25321
		bool ThrownWeaponFlexPhaseIsActive(GameObject Weapon);

		// Token: 0x060062EA RID: 25322
		bool ThrownWeaponFlexPhaseTraversal(GameObject Actor, GameObject WillHit, GameObject Target, GameObject Weapon, int Phase, Cell FromCell, Cell ToCell, out bool RecheckHit, out bool RecheckPhase, bool HasDynamicTargets = false);
	}
}
