using System;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000ADE RID: 2782
	public class SmokingAreaW
	{
		// Token: 0x06006FF3 RID: 28659 RVA: 0x0027859C File Offset: 0x0027679C
		public bool BuildZone(Zone Z)
		{
			for (int i = 0; i < 600; i++)
			{
				int num = Stat.Random(0, 79);
				int num2 = Stat.Random(0, 24);
				GameObject firstObjectWithPart = Z.GetCell(num, num2).GetFirstObjectWithPart("LiquidVolume");
				if (firstObjectWithPart != null)
				{
					LiquidVolume liquidVolume = firstObjectWithPart.LiquidVolume;
					if (liquidVolume != null && !liquidVolume.ComponentLiquids.ContainsKey("blood"))
					{
						liquidVolume.MixWith(new LiquidVolume("blood", liquidVolume.Volume), null, null, null, false);
					}
				}
			}
			Z.GetCell(0, 5).AddObject("SmokecasterE", null, null, null, null);
			Z.GetCell(0, 7).AddObject("SmokecasterE", null, null, null, null);
			Z.GetCell(0, 15).AddObject("SmokecasterE", null, null, null, null);
			return true;
		}
	}
}
