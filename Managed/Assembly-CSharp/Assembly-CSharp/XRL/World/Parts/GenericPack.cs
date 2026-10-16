using System;
using System.Collections.Generic;
using XRL.World.AI;

namespace XRL.World.Parts
{
	// Token: 0x02000DDB RID: 3547
	[Serializable]
	public class GenericPack : IPart
	{
		// Token: 0x0600903C RID: 36924 RVA: 0x003408C4 File Offset: 0x0033EAC4
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600903D RID: 36925 RVA: 0x003408C7 File Offset: 0x0033EAC7
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == EnteredCellEvent.ID && !this.Created);
		}

		// Token: 0x0600903E RID: 36926 RVA: 0x003408E8 File Offset: 0x0033EAE8
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (!this.Created)
			{
				this.Created = true;
				try
				{
					List<Cell> list = Event.NewCellList();
					foreach (Cell cell in this.ParentObject.CurrentCell.GetAdjacentCells(this.Radius, true, true))
					{
						if (cell.IsEmptyOfSolid(true))
						{
							list.Add(cell);
						}
					}
					int num = this.Amount.RollCached();
					int i = 0;
					int num2 = num;
					while (i < num2)
					{
						Cell randomElement = list.GetRandomElement<Cell>(null);
						if (randomElement == null)
						{
							break;
						}
						PopulationResult populationResult = PopulationManager.RollOneFrom(this.Table, null, null);
						int j = 0;
						int number = populationResult.Number;
						while (j < number)
						{
							GameObject gameObject = GameObject.Create(populationResult.Blueprint);
							gameObject.SetAlliedLeader<AllyPack>(this.ParentObject, 0, false);
							randomElement.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
							gameObject.MakeActive(false);
							list.Remove(randomElement);
							j++;
						}
						i++;
					}
				}
				catch (Exception ex)
				{
					MetricsManager.LogError("GenericPack setup", ex);
				}
				this.ParentObject.RemovePart(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003C25 RID: 15397
		public string Table = "Bethesda Susa Wharf";

		// Token: 0x04003C26 RID: 15398
		public string Amount = "3d6";

		// Token: 0x04003C27 RID: 15399
		public int Radius = 4;

		// Token: 0x04003C28 RID: 15400
		public bool Created;
	}
}
