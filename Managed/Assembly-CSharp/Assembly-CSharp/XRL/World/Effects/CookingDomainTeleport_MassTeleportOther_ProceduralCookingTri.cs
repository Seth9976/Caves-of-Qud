using System;

namespace XRL.World.Effects
{
	// Token: 0x02000D12 RID: 3346
	[Serializable]
	public class CookingDomainTeleport_MassTeleportOther_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x060083E7 RID: 33767 RVA: 0x002FBA6A File Offset: 0x002F9C6A
		public override string GetDescription()
		{
			return "@they teleport all creatures surrounding @them.";
		}

		// Token: 0x060083E8 RID: 33768 RVA: 0x002FBA74 File Offset: 0x002F9C74
		public override void Apply(GameObject Subject)
		{
			if (!Subject.OnWorldMap())
			{
				GameObject gameObject = null;
				foreach (Cell cell in Subject.CurrentCell.GetLocalAdjacentCells())
				{
					int num = 0;
					while (++num < 10)
					{
						GameObject combatObject = cell.GetCombatObject();
						if (combatObject == null || combatObject == gameObject)
						{
							break;
						}
						combatObject.RandomTeleport(true, null, null, null, null, 0, 0, true, null, false, true, true, false);
						gameObject = combatObject;
					}
				}
			}
		}
	}
}
