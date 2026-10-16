using System;
using System.Collections.Generic;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001372 RID: 4978
	[Serializable]
	public class Rifle_FlatteningFire : BaseSkill
	{
		// Token: 0x0600C17E RID: 49534 RVA: 0x0046C1DC File Offset: 0x0046A3DC
		public static bool MeetsCriteria(GameObject GO)
		{
			if (!GameObject.Validate(ref GO))
			{
				return false;
			}
			if (!GO.IsCombat)
			{
				return false;
			}
			int num = 0;
			List<Cell> list = new List<Cell>();
			GO.Physics.CurrentCell.GetAdjacentCells(1, list, false, true);
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].IsSolid())
				{
					num++;
				}
			}
			return num >= 6;
		}
	}
}
