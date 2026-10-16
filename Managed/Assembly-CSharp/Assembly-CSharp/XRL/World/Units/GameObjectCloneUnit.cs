using System;
using XRL.World.Parts;

namespace XRL.World.Units
{
	// Token: 0x020009A9 RID: 2473
	[Serializable]
	public class GameObjectCloneUnit : GameObjectUnit
	{
		// Token: 0x06006AC5 RID: 27333 RVA: 0x002263D8 File Offset: 0x002245D8
		public override void Apply(GameObject Object)
		{
			CloneOnPlace cloneOnPlace = Object.RequirePart<CloneOnPlace>(false);
			cloneOnPlace.Amount++;
			cloneOnPlace.Context = "Unit";
			cloneOnPlace.Force = true;
		}

		// Token: 0x06006AC6 RID: 27334 RVA: 0x00226400 File Offset: 0x00224600
		public override string GetDescription(bool Inscription = false)
		{
			return "Spawns with a copy in a nearby cell";
		}
	}
}
