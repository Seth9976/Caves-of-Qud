using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F1F RID: 3871
	[Serializable]
	public class CyberneticsCathedraBlackOpal : CyberneticsCathedra
	{
		// Token: 0x06009A67 RID: 39527 RVA: 0x0038071C File Offset: 0x0037E91C
		public override void OnImplanted(GameObject Object)
		{
			base.OnImplanted(Object);
			this.ActivatedAbilityID = Object.AddActivatedAbility("Wormhole", "CommandActivateCathedra", "Cybernetics", null, "\u0015", null, false, false, false, false, true, false, false, false, true, true, false, false, -1, "CommandCathedraBlackOpal", null, null, null, null);
		}

		// Token: 0x06009A68 RID: 39528 RVA: 0x00380768 File Offset: 0x0037E968
		public override void CollectStats(Templates.StatCollector stats)
		{
			base.CollectStats(stats);
		}

		// Token: 0x06009A69 RID: 39529 RVA: 0x00380774 File Offset: 0x0037E974
		public override void Activate(GameObject Actor)
		{
			Zone currentZone = Actor.CurrentZone;
			Cell destinationCellFor = SpaceTimeVortex.GetDestinationCellFor(SpaceTimeVortex.GetRandomDestinationZoneID((currentZone != null) ? currentZone.GetZoneWorld() : null, true), Actor, null);
			if (SpaceTimeVortex.Teleport(Actor, destinationCellFor, this.ParentObject, null, true))
			{
				IComponent<GameObject>.XDidY(Actor, "shear", "the fiber of spacetime and burrow to another place", "!", null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			}
			base.Activate(Actor);
		}
	}
}
