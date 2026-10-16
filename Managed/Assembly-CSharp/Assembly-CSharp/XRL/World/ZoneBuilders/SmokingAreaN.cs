using System;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000ADF RID: 2783
	public class SmokingAreaN
	{
		// Token: 0x06006FF5 RID: 28661 RVA: 0x00278678 File Offset: 0x00276878
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
			Z.GetCell(10, 0).AddObject("SmokecasterS", null, null, null, null);
			Z.GetCell(40, 0).AddObject("SmokecasterS", null, null, null, null);
			Z.GetCell(70, 0).AddObject("SmokecasterS", null, null, null, null);
			return true;
		}
	}
}
