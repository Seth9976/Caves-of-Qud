using System;
using UnityEngine;

namespace XRL.World.Parts
{
	// Token: 0x02000DF5 RID: 3573
	[Serializable]
	public class ZoneLandmarkObject : IZoneLandmark
	{
		// Token: 0x060090BC RID: 37052 RVA: 0x00342DC0 File Offset: 0x00340FC0
		public override string GetDisplayName()
		{
			return this.ParentObject.GetReferenceDisplayName(int.MaxValue, null, null, false, true, false, true, true, false, false, false, null, false);
		}

		// Token: 0x060090BD RID: 37053 RVA: 0x00342DEC File Offset: 0x00340FEC
		public override bool IsApplicable(int X, int Y)
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				return false;
			}
			if (this.Visual)
			{
				Y -= Mathf.RoundToInt((float)(currentCell.Y - Y) * 24f / 32f);
			}
			return (this.Manhattan ? currentCell.ManhattanDistanceTo(X, Y) : currentCell.PathDistanceTo(X, Y)) <= this.Radius;
		}

		// Token: 0x04003C47 RID: 15431
		public int Radius = 1;

		// Token: 0x04003C48 RID: 15432
		public bool Manhattan = true;

		// Token: 0x04003C49 RID: 15433
		public bool Visual = true;
	}
}
