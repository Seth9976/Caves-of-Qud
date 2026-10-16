using System;
using System.Collections.Generic;
using XRL.Rules;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000B01 RID: 2817
	public class CryptOfPriests : ZoneBuilderSandbox
	{
		// Token: 0x06007092 RID: 28818 RVA: 0x002858EC File Offset: 0x00283AEC
		public static void Reset()
		{
			CryptOfPriests.cryptVertexList = null;
		}

		// Token: 0x06007093 RID: 28819 RVA: 0x002858F4 File Offset: 0x00283AF4
		public bool BuildZone(Zone Z)
		{
			Stat.ReseedFrom(Z.ZoneID, false);
			for (int i = 0; i < 16; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					int num = i + Z.X * 16;
					int num2 = j + Z.Y * 5;
					bool flag = CryptOfPriests.cryptVertexList.Contains(num.ToString() + "_" + num2.ToString());
					Box box = new Box(i * 5, j * 5, i * 5 + 4, j * 5 + 4);
					if (num == 4 && num2 == 12)
					{
						Z.GetCell(box.x1 + 2, box.y1 + 2).AddObject("StairsUp", null, null, null, null);
					}
					if (num == 43 && num2 == 12)
					{
						Z.GetCell(box.x1 + 2, box.y1 + 2).AddObject("StairsDown", null, null, null, null);
					}
					if (flag)
					{
						Z.FillHollowBox(box, "TombWarriorCryptWall");
					}
				}
			}
			ZoneBuilderSandbox.EnsureAllVoidsConnected(Z, false, 0.2f, null);
			Z.ForeachCell(delegate(Cell c)
			{
				c.SetReachable(true);
			});
			Z.GetCell(0, 0).AddObject("Finish_TombOfTheEaters_EnterTheTombOfTheEaters", null, null, null, null);
			foreach (Cell cell in Z.GetCellsWithObject("StairsUp"))
			{
				base.EnsureCellReachable(Z, cell);
			}
			foreach (Cell cell2 in Z.GetCellsWithObject("StairsDown"))
			{
				base.EnsureCellReachable(Z, cell2);
			}
			new ChildrenOfTheTomb().BuildZone(Z);
			return true;
		}

		// Token: 0x04003417 RID: 13335
		public static HashSet<string> cryptVertexList;

		// Token: 0x04003418 RID: 13336
		public int lastseed;
	}
}
