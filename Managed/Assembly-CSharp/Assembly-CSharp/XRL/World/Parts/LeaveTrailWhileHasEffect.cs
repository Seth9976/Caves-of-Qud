using System;

namespace XRL.World.Parts
{
	// Token: 0x02000ED1 RID: 3793
	[Serializable]
	public class LeaveTrailWhileHasEffect : IPart
	{
		// Token: 0x060097E3 RID: 38883 RVA: 0x00372446 File Offset: 0x00370646
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == LeftCellEvent.ID;
		}

		// Token: 0x060097E4 RID: 38884 RVA: 0x0037245C File Offset: 0x0037065C
		public override bool HandleEvent(LeftCellEvent E)
		{
			if (this.ParentObject.HasEffect(this.Effect))
			{
				this.LeaveTrailIn(E.Cell);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060097E5 RID: 38885 RVA: 0x00372485 File Offset: 0x00370685
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x060097E6 RID: 38886 RVA: 0x00372488 File Offset: 0x00370688
		public bool LeaveTrailIn(Cell Cell)
		{
			if (Cell == null || Cell.OnWorldMap())
			{
				return false;
			}
			if (this.Specification.IsNullOrEmpty())
			{
				return false;
			}
			if (!this.Chance.in100())
			{
				return false;
			}
			bool zeroRangeIncluded = this.Range.RollMinCached() <= 0;
			bool any = false;
			GameObjectFactory.ProcessSpecification(this.Specification, delegate(GameObject obj)
			{
				int num = 0;
				Cell randomLocalAdjacentCell;
				do
				{
					randomLocalAdjacentCell = Cell.GetRandomLocalAdjacentCell(this.Range.RollCached(), zeroRangeIncluded);
				}
				while ((randomLocalAdjacentCell == null || !randomLocalAdjacentCell.IsEmptyForPopulation()) && ++num < 10);
				if (randomLocalAdjacentCell != null && randomLocalAdjacentCell.IsEmptyForPopulation())
				{
					randomLocalAdjacentCell.AddObject(obj, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					any = true;
					return;
				}
				obj.Obliterate(null, false, null);
			}, null, 1, 0, 0, null, "LeaveTrailWhileHasEffect", null, null, null, null);
			return any;
		}

		// Token: 0x04003F32 RID: 16178
		public string Effect = "Burrowed";

		// Token: 0x04003F33 RID: 16179
		public string Specification = "PlantWall";

		// Token: 0x04003F34 RID: 16180
		public string Range = "0";

		// Token: 0x04003F35 RID: 16181
		public int Chance = 100;
	}
}
