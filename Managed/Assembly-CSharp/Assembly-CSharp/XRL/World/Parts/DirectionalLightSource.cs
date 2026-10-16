using System;
using Genkit;

namespace XRL.World.Parts
{
	// Token: 0x0200101A RID: 4122
	[Serializable]
	public class DirectionalLightSource : IPart
	{
		// Token: 0x0600A433 RID: 42035 RVA: 0x003C0B6C File Offset: 0x003BED6C
		public override bool SameAs(IPart p)
		{
			LightSource lightSource = p as LightSource;
			return lightSource.Darkvision == this.Darkvision && lightSource.OnlyIfCharged == this.OnlyIfCharged && lightSource.Lit == this.Lit && lightSource.Radius == this.Radius && base.SameAs(p);
		}

		// Token: 0x0600A434 RID: 42036 RVA: 0x003C0BC7 File Offset: 0x003BEDC7
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeRenderEvent.ID;
		}

		// Token: 0x0600A435 RID: 42037 RVA: 0x003C0BE0 File Offset: 0x003BEDE0
		public override bool HandleEvent(BeforeRenderEvent E)
		{
			Cell currentCell = this.ParentObject.GetCurrentCell();
			if (currentCell == null)
			{
				return true;
			}
			if (this.OnlyIfCharged && (base.IsBroken() || base.IsRusted() || !this.ParentObject.TestCharge(1, false, 0L, true, true)))
			{
				return true;
			}
			LightLevel lightLevel = (this.Darkvision ? LightLevel.Darkvision : LightLevel.Light);
			for (int i = 0; i < this.Directions.Length; i++)
			{
				Location2D location2D = currentCell.Location;
				while (location2D != null)
				{
					Zone parentZone = currentCell.ParentZone;
					if (((parentZone != null) ? parentZone.GetCell(location2D) : null) == null)
					{
						break;
					}
					currentCell.ParentZone.MixLight(currentCell.X, currentCell.Y, location2D.X, location2D.Y, lightLevel, false);
					if (this.Directions[i] == 'N')
					{
						location2D = location2D.FromDirection("N");
					}
					if (this.Directions[i] == 'S')
					{
						location2D = location2D.FromDirection("S");
					}
					if (this.Directions[i] == 'E')
					{
						location2D = location2D.FromDirection("E");
					}
					if (this.Directions[i] == 'W')
					{
						location2D = location2D.FromDirection("W");
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0400440E RID: 17422
		public bool Darkvision;

		// Token: 0x0400440F RID: 17423
		public bool OnlyIfCharged;

		// Token: 0x04004410 RID: 17424
		public bool Lit = true;

		// Token: 0x04004411 RID: 17425
		public string Directions = "NSEW";

		// Token: 0x04004412 RID: 17426
		public int Radius = 5;
	}
}
