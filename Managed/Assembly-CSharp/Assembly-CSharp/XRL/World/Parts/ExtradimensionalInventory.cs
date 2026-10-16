using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000F7A RID: 3962
	[Serializable]
	public class ExtradimensionalInventory : IPart
	{
		// Token: 0x06009DB1 RID: 40369 RVA: 0x00391C98 File Offset: 0x0038FE98
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009DB2 RID: 40370 RVA: 0x00391CB0 File Offset: 0x0038FEB0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell" && !this.Applied)
			{
				this.Applied = true;
				foreach (GameObject gameObject in this.ParentObject.GetInventory())
				{
					if (!gameObject.HasPart<ModExtradimensional>() && Stat.Random(1, 100) <= this.Chance && !string.Equals(gameObject.GetTag("Mods", null), "None"))
					{
						gameObject.AddPart<ModExtradimensional>(new ModExtradimensional(), true, false);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004132 RID: 16690
		public int Chance = 100;

		// Token: 0x04004133 RID: 16691
		public bool Applied;
	}
}
