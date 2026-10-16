using System;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AE0 RID: 2784
	public class SmokingAreaS
	{
		// Token: 0x06006FF7 RID: 28663 RVA: 0x00278754 File Offset: 0x00276954
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
			Z.GetCell(10, 24).AddObject("SmokecasterN", null, null, null, null);
			Z.GetCell(40, 24).AddObject("SmokecasterN", null, null, null, null);
			Z.GetCell(70, 24).AddObject("SmokecasterN", null, null, null, null);
			return true;
		}
	}
}
