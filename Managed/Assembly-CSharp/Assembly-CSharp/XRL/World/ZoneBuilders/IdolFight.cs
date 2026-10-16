using System;
using System.Collections.Generic;
using XRL.Rules;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000ADA RID: 2778
	public class IdolFight
	{
		// Token: 0x06006FEB RID: 28651 RVA: 0x002782B8 File Offset: 0x002764B8
		public bool BuildZone(Zone Z)
		{
			for (int i = 0; i < 2500; i++)
			{
				int num = Stat.Random(0, 79);
				int num2 = Stat.Random(0, 24);
				if (Z.GetCell(num, num2).HasObjectWithPart("LiquidVolume"))
				{
					using (List<Cell>.Enumerator enumerator = Z.GetCell(num, num2).GetLocalAdjacentCells().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.GetObjectCountWithPart("LiquidVolume") == 1)
							{
								Z.GetCell(num, num2).AddObject("Crazed Goatfolk Shaman 1", null, null, null, null);
								int num3 = num + Stat.Random(-5, 5);
								int num4 = num2 + Stat.Random(-5, 5);
								while (num3 < 0 || num4 < 0 || num3 > 79 || num4 > 24 || Z.GetCell(num3, num4).PathDistanceTo(Z.GetCell(num, num2)) < 3)
								{
									num3 = num + Stat.Random(-3, 3);
									num4 = num2 + Stat.Random(-3, 3);
								}
								Z.GetCell(num3, num4).AddObject("Crazed Goatfolk Shaman 2", null, null, null, null);
								return true;
							}
						}
					}
				}
			}
			return false;
		}
	}
}
