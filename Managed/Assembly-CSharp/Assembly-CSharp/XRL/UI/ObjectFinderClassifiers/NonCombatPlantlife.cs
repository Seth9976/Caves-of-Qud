using System;
using XRL.World;

namespace XRL.UI.ObjectFinderClassifiers
{
	// Token: 0x020014E0 RID: 5344
	public class NonCombatPlantlife : ObjectFinder.Classifier
	{
		// Token: 0x0600D10E RID: 53518 RVA: 0x004DA5F4 File Offset: 0x004D87F4
		public override bool Check(GameObject go, ObjectFinder.Context context)
		{
			return (go.HasTag("Plant") || go.HasTag("PlantLike") || go.HasTag("Fungus")) && !go.IsCombat;
		}
	}
}
