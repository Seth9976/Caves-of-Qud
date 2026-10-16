using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x020012F1 RID: 4849
	[Serializable]
	public class UnityPrefabImposterChance : IPart
	{
		// Token: 0x0600BE64 RID: 48740 RVA: 0x0045BCD5 File Offset: 0x00459ED5
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BE65 RID: 48741 RVA: 0x0045BCEC File Offset: 0x00459EEC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell")
			{
				if (Stat.RandomCosmetic(1, 100) <= this.Chance)
				{
					BasePrefabImposter basePrefabImposter = (this.VisibleOnly ? new PrefabImposter() : new PrefabImposterFinal());
					basePrefabImposter.Prefab = this.PrefabID;
					this.ParentObject.AddPart<BasePrefabImposter>(basePrefabImposter, true, false);
				}
				this.ParentObject.RemovePart(this);
			}
			return true;
		}

		// Token: 0x04004CA8 RID: 19624
		public bool VisibleOnly = true;

		// Token: 0x04004CA9 RID: 19625
		public int Chance;

		// Token: 0x04004CAA RID: 19626
		public string PrefabID;
	}
}
