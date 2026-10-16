using System;
using System.Collections.Generic;
using XRL.World.AI;

namespace XRL.World.Parts
{
	// Token: 0x02000EB7 RID: 3767
	[Serializable]
	public class Followers : IPart
	{
		// Token: 0x0600971D RID: 38685 RVA: 0x0036D509 File Offset: 0x0036B709
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600971E RID: 38686 RVA: 0x0036D50C File Offset: 0x0036B70C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == EnteredCellEvent.ID && !this.spawned);
		}

		// Token: 0x0600971F RID: 38687 RVA: 0x0036D530 File Offset: 0x0036B730
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (!this.spawned)
			{
				this.spawned = true;
				Cell currentCell = this.ParentObject.CurrentCell;
				List<Cell> connectedSpawnLocations = currentCell.GetConnectedSpawnLocations(12);
				foreach (PopulationResult populationResult in PopulationManager.Generate(this.Table, null, null, null, null, null))
				{
					for (int i = 0; i < populationResult.Number; i++)
					{
						if (connectedSpawnLocations.Count <= 0)
						{
							Cell cell = currentCell.getClosestEmptyCell() ?? currentCell.getClosestPassableCell();
							if (cell != null)
							{
								connectedSpawnLocations.Add(cell);
							}
						}
						if (connectedSpawnLocations.Count > 0)
						{
							GameObject gameObject = GameObject.Create(populationResult.Blueprint);
							gameObject.SetAlliedLeader<AllyProselytize>(this.ParentObject, 0, false);
							gameObject.MakeActive(false);
							connectedSpawnLocations[0].AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
							connectedSpawnLocations.RemoveAt(0);
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003ECC RID: 16076
		public string Table;

		// Token: 0x04003ECD RID: 16077
		public bool spawned;
	}
}
