using System;
using System.Collections.Generic;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000ED4 RID: 3796
	[Serializable]
	public class LiquidBurst : IPart
	{
		// Token: 0x06009804 RID: 38916 RVA: 0x003734B4 File Offset: 0x003716B4
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x06009805 RID: 38917 RVA: 0x003734B7 File Offset: 0x003716B7
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeforeDeathRemoval");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009806 RID: 38918 RVA: 0x003734CC File Offset: 0x003716CC
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
						cell.AddObject(GameObjectFactory.Factory.CreateObject(this.Liquid), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						if (Stat.Random(1, 100) <= this.EelChance && !cell.HasObjectWithPart("EelSpawn"))
						{
							cell.AddObject(GameObjectFactory.Factory.CreateObject("EelSpawn"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003F42 RID: 16194
		public string Liquid = "SlimePuddle";

		// Token: 0x04003F43 RID: 16195
		public int EelChance;
	}
}
