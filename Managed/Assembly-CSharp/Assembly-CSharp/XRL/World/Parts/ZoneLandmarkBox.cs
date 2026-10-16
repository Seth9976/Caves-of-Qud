using System;
using XRL.Collections;

namespace XRL.World.Parts
{
	// Token: 0x02000DF7 RID: 3575
	[Serializable]
	public class ZoneLandmarkBox : IZoneLandmark
	{
		// Token: 0x060090C5 RID: 37061 RVA: 0x00343123 File Offset: 0x00341323
		public override string GetDisplayName()
		{
			return this.DisplayName;
		}

		// Token: 0x060090C6 RID: 37062 RVA: 0x0034312C File Offset: 0x0034132C
		public override bool IsApplicable(int X, int Y)
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				return false;
			}
			int x = currentCell.X;
			int y = currentCell.Y;
			int num = x + this.Width - 1;
			int num2 = y + this.Height - 1;
			if (X < x)
			{
				return false;
			}
			if (X > num)
			{
				return false;
			}
			if (Y < y)
			{
				return false;
			}
			if (Y > num2)
			{
				return false;
			}
			if (!this.RequireBlueprint.IsNullOrEmpty() && this.RequireCount > 0)
			{
				Zone parentZone = currentCell.ParentZone;
				int num3 = 0;
				for (int i = y; i <= num2; i++)
				{
					for (int j = x; j <= num; j++)
					{
						Cell cell = parentZone.GetCell(j, i);
						if (cell != null)
						{
							using (Container<GameObject>.Enumerator enumerator = cell.Objects.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									if (!(enumerator.Current.Blueprint != this.RequireBlueprint) && ++num3 >= this.RequireCount)
									{
										goto IL_00FB;
									}
								}
							}
						}
					}
				}
				IL_00FB:
				if (num3 < this.RequireCount)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04003C58 RID: 15448
		public string DisplayName;

		// Token: 0x04003C59 RID: 15449
		public string RequireBlueprint;

		// Token: 0x04003C5A RID: 15450
		public int RequireCount;

		// Token: 0x04003C5B RID: 15451
		public int Width;

		// Token: 0x04003C5C RID: 15452
		public int Height;
	}
}
