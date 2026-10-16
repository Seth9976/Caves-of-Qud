using System;
using System.Collections.Generic;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000E48 RID: 3656
	[Serializable]
	public class AcidCorpseExplosion : IPart
	{
		// Token: 0x0600942A RID: 37930 RVA: 0x003558F1 File Offset: 0x00353AF1
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600942B RID: 37931 RVA: 0x003558F4 File Offset: 0x00353AF4
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeforeDeathRemoval");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600942C RID: 37932 RVA: 0x0035590C File Offset: 0x00353B0C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeDeathRemoval" && this.ParentObject.Physics.CurrentCell != null)
			{
				List<Cell> adjacentCells = this.ParentObject.Physics.CurrentCell.GetAdjacentCells(true);
				adjacentCells.Add(this.ParentObject.Physics.CurrentCell);
				foreach (Cell cell in adjacentCells)
				{
					if (!cell.IsOccluding() && Stat.Random(0, 100) <= 75)
					{
						cell.AddObject(GameObjectFactory.Factory.CreateObject("AcidPool"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
				}
			}
			return true;
		}
	}
}
