using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x02001295 RID: 4757
	[Serializable]
	public class DoorSwitch : IPart
	{
		// Token: 0x0600BC2B RID: 48171 RVA: 0x0044CABB File Offset: 0x0044ACBB
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("SwitchActivated");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BC2C RID: 48172 RVA: 0x0044CAD0 File Offset: 0x0044ACD0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "SwitchActivated")
			{
				List<Cell> adjacentCells = this.ParentObject.CurrentCell.GetAdjacentCells(true);
				adjacentCells.Add(this.ParentObject.CurrentCell);
				bool flag = false;
				foreach (Cell cell in adjacentCells)
				{
					foreach (GameObject gameObject in cell.LoopObjectsWithPart("Door"))
					{
						Door part = gameObject.GetPart<Door>();
						if (!part.Open)
						{
							part.PerformOpen(null, null, false);
							IComponent<GameObject>.AddPlayerMessage("The security door unlocks with a loud clank and swings open.", null, true);
							flag = true;
						}
						else
						{
							part.PerformClose(null, false);
							IComponent<GameObject>.AddPlayerMessage("The security door swings closed and locks with a loud clank.", null, true);
							flag = true;
						}
					}
				}
				if (!flag)
				{
					IComponent<GameObject>.AddPlayerMessage("Nothing seems to happen when you hit the switch.", null, true);
				}
			}
			return base.FireEvent(E);
		}
	}
}
