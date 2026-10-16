using System;
using XRL.Rules;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A39 RID: 2617
	public class AddBlueprintBuilder
	{
		// Token: 0x06006DB6 RID: 28086 RVA: 0x0024F178 File Offset: 0x0024D378
		public bool BuildZone(Zone Z)
		{
			int i = 500;
			GameObject gameObject = GameObjectFactory.Factory.CreateObject(this.Object);
			if (gameObject != null)
			{
				while (i > 0)
				{
					int num = Stat.Random(0, Z.Width - 1);
					int num2 = Stat.Random(0, Z.Height - 1);
					if (Z.GetCell(num, num2).IsEmpty())
					{
						Z.GetCell(num, num2).AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						return true;
					}
					i--;
				}
			}
			return true;
		}

		// Token: 0x0400322D RID: 12845
		public string Object;
	}
}
