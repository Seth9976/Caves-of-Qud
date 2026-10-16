using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000F04 RID: 3844
	[Serializable]
	public class SpawnWithLiquid : IPart
	{
		// Token: 0x0600995A RID: 39258 RVA: 0x0037A8BA File Offset: 0x00378ABA
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600995B RID: 39259 RVA: 0x0037A8BD File Offset: 0x00378ABD
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ZoneBuiltEvent.ID || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600995C RID: 39260 RVA: 0x0037A8DB File Offset: 0x00378ADB
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (E.Cell.ParentZone.Built)
			{
				this.Spawn();
			}
			return true;
		}

		// Token: 0x0600995D RID: 39261 RVA: 0x0037A8F6 File Offset: 0x00378AF6
		public override bool HandleEvent(ZoneBuiltEvent E)
		{
			this.Spawn();
			return true;
		}

		// Token: 0x0600995E RID: 39262 RVA: 0x0037A900 File Offset: 0x00378B00
		public void Spawn()
		{
			if (this.spawned)
			{
				return;
			}
			Cell currentCell = this.ParentObject.GetCurrentCell();
			if (currentCell != null)
			{
				this.spawned = true;
				if (!currentCell.HasObjectWithPart("LiquidVolume") || currentCell.GetFirstObjectWithPart("LiquidVolume").LiquidVolume.MaxVolume >= 0)
				{
					currentCell.AddObject(this.LiquidObject, null, null, null, null);
					foreach (Cell cell in currentCell.GetLocalCardinalAdjacentCells())
					{
						if (cell != null && Stat.Random(1, 100) <= this.AdjacentPoolChance)
						{
							cell.AddObject(this.LiquidObject, null, null, null, null);
						}
					}
				}
				this.ParentObject.RemovePart(this);
			}
		}

		// Token: 0x04003FE5 RID: 16357
		public bool spawned;

		// Token: 0x04003FE6 RID: 16358
		public string LiquidObject = "SaltyWaterPuddle";

		// Token: 0x04003FE7 RID: 16359
		public int AdjacentPoolChance = 25;
	}
}
