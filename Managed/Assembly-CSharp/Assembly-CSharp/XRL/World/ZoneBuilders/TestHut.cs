using System;
using XRL.Rules;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AB8 RID: 2744
	public class TestHut
	{
		// Token: 0x06006F6F RID: 28527 RVA: 0x00270450 File Offset: 0x0026E650
		public bool BuildZone(Zone Z)
		{
			int num = Stat.Random(0, 70);
			int num2 = Stat.Random(0, 19);
			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					Cell cell = Z.GetCell(i + num, j + num2);
					cell.Objects.Clear();
					cell.AddObject("BrinestalkWall", null, null, null, null);
				}
			}
			return true;
		}
	}
}
