using System;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000ADD RID: 2781
	public class SmokingAreaE
	{
		// Token: 0x06006FF1 RID: 28657 RVA: 0x002784C0 File Offset: 0x002766C0
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
			Z.GetCell(79, 5).AddObject("SmokecasterW", null, null, null, null);
			Z.GetCell(79, 7).AddObject("SmokecasterW", null, null, null, null);
			Z.GetCell(79, 15).AddObject("SmokecasterW", null, null, null, null);
			return true;
		}
	}
}
