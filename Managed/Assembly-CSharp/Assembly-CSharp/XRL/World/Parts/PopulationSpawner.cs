using System;

namespace XRL.World.Parts
{
	// Token: 0x02001306 RID: 4870
	[Serializable]
	public class PopulationSpawner : IPart
	{
		// Token: 0x0600BEBE RID: 48830 RVA: 0x0045D728 File Offset: 0x0045B928
		public override bool SameAs(IPart p)
		{
			PopulationSpawner populationSpawner = p as PopulationSpawner;
			return populationSpawner.DestroyAfterSpawn == this.DestroyAfterSpawn && populationSpawner.Spawned == this.Spawned && !(populationSpawner.Specification != this.Specification) && !(populationSpawner.Range != this.Range) && base.SameAs(p);
		}

		// Token: 0x0600BEBF RID: 48831 RVA: 0x0045D78D File Offset: 0x0045B98D
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600BEC0 RID: 48832 RVA: 0x0045D7A4 File Offset: 0x0045B9A4
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (!this.Spawned && E.Cell != null)
			{
				bool zeroRangeIncluded = this.Range.RollMinCached() <= 0;
				GameObjectFactory.ProcessSpecification(this.Specification, delegate(GameObject obj)
				{
					int num = 0;
					Cell randomLocalAdjacentCell;
					do
					{
						randomLocalAdjacentCell = E.Cell.GetRandomLocalAdjacentCell(this.Range.RollCached(), zeroRangeIncluded);
					}
					while ((randomLocalAdjacentCell == null || !randomLocalAdjacentCell.IsEmptyForPopulation()) && ++num < 10);
					if (randomLocalAdjacentCell != null && randomLocalAdjacentCell.IsEmptyForPopulation())
					{
						randomLocalAdjacentCell.AddObject(obj, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						this.Spawned = true;
						return;
					}
					obj.Obliterate(null, false, null);
				}, null, 1, 0, 0, null, "PopulationSpawner", null, null, null, null);
				if (this.DestroyAfterSpawn && this.Spawned)
				{
					this.ParentObject.Obliterate(null, false, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BEC1 RID: 48833 RVA: 0x0045D841 File Offset: 0x0045BA41
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04004CCD RID: 19661
		public bool DestroyAfterSpawn = true;

		// Token: 0x04004CCE RID: 19662
		public bool Spawned;

		// Token: 0x04004CCF RID: 19663
		public string Specification = "SaltyWaterPuddle";

		// Token: 0x04004CD0 RID: 19664
		public string Range = "0";
	}
}
