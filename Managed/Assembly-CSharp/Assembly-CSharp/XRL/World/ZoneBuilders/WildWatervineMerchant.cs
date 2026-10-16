using System;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000ADC RID: 2780
	public class WildWatervineMerchant
	{
		// Token: 0x06006FEF RID: 28655 RVA: 0x00278440 File Offset: 0x00276640
		public bool BuildZone(Zone Z)
		{
			for (int i = 0; i < 2500; i++)
			{
				int num = Stat.Random(0, 79);
				int num2 = Stat.Random(0, 24);
				if (Z.GetCell(num, num2).Objects.Count > 0 && Z.GetCell(num, num2).Objects[0].HasPart<LiquidVolume>())
				{
					Z.GetCell(num, num2).AddObject("Wild Water Merchant", null, null, null, null);
					return true;
				}
			}
			return false;
		}
	}
}
