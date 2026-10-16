using System;
using System.Collections.Generic;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000E75 RID: 3701
	[Serializable]
	public class GiantAmoeba : IPart
	{
		// Token: 0x06009570 RID: 38256 RVA: 0x00360E03 File Offset: 0x0035F003
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x06009571 RID: 38257 RVA: 0x00360E06 File Offset: 0x0035F006
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeforeDeathRemoval");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009572 RID: 38258 RVA: 0x00360E1C File Offset: 0x0035F01C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeDeathRemoval" && this.ParentObject.Physics.CurrentCell != null)
			{
				List<Cell> adjacentCells = this.ParentObject.Physics.CurrentCell.GetAdjacentCells(true);
				adjacentCells.Add(this.ParentObject.Physics.CurrentCell);
				foreach (Cell cell in adjacentCells)
				{
					if (!cell.IsOccluding() && Stat.Random(1, 100) <= 80)
					{
						cell.AddObject(GameObjectFactory.Factory.CreateObject("SlimePuddle"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
				}
			}
			return base.FireEvent(E);
		}
	}
}
