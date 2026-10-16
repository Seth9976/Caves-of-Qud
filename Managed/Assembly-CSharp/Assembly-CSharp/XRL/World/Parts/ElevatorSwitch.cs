using System;
using System.Collections.Generic;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001296 RID: 4758
	[Serializable]
	public class ElevatorSwitch : IPart
	{
		// Token: 0x0600BC2E RID: 48174 RVA: 0x0044CBE4 File Offset: 0x0044ADE4
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("SwitchActivated");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BC2F RID: 48175 RVA: 0x0044CBFC File Offset: 0x0044ADFC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "SwitchActivated")
			{
				List<Cell> adjacentCells = this.ParentObject.CurrentCell.GetAdjacentCells(true);
				adjacentCells.Add(this.ParentObject.CurrentCell);
				bool flag = false;
				foreach (Cell cell in adjacentCells)
				{
					foreach (GameObject gameObject in cell.GetObjectsWithIntProperty("ElevatorPlatform"))
					{
						flag = true;
						if (gameObject.CurrentCell == The.Player.CurrentCell)
						{
							gameObject.CurrentCell.RemoveObject(The.Player, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
							Zone zone = gameObject.CurrentCell.ParentZone.GetZoneAtLevel(this.TopLevel);
							if (this.ParentObject.Physics.CurrentCell.ParentZone.Z == this.TopLevel)
							{
								zone = gameObject.Physics.CurrentCell.ParentZone.GetZoneAtLevel(this.FloorLevel);
							}
							int x = gameObject.CurrentCell.X;
							int y = gameObject.CurrentCell.Y;
							zone.GetCell(x, y).AddObject("Platform", null, null, null, null);
							zone.GetCell(x, y).AddObject(The.Player, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
							The.ZoneManager.SetActiveZone(zone);
							The.ZoneManager.ProcessGoToPartyLeader();
							gameObject.Destroy(null, false, false, null);
							if (zone.Z < this.ParentObject.Physics.CurrentCell.ParentZone.Z)
							{
								Popup.Show("The chrome platform begins to hum as it ascends into the darkness.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
							}
							else
							{
								Popup.Show("The chrome platform begins to hum as it descends into the darkness.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
							}
						}
						else
						{
							IComponent<GameObject>.AddPlayerMessage("Nothing seems to happen when you hit the switch.", null, true);
						}
					}
				}
				if (!flag)
				{
					foreach (Cell cell2 in adjacentCells)
					{
						foreach (GameObject gameObject2 in cell2.GetObjects("OpenShaft"))
						{
							if (!gameObject2.CurrentCell.HasObject("Platform"))
							{
								gameObject2.CurrentCell.AddObject("Platform", null, null, null, null);
							}
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004B6B RID: 19307
		public int TopLevel = 10;

		// Token: 0x04004B6C RID: 19308
		public int FloorLevel = 15;
	}
}
