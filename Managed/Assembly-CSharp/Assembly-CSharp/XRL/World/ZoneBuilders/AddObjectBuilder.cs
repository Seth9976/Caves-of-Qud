using System;
using XRL.Rules;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A3B RID: 2619
	public class AddObjectBuilder
	{
		// Token: 0x06006DBA RID: 28090 RVA: 0x0024F268 File Offset: 0x0024D468
		public bool BuildZone(Zone Z)
		{
			for (int i = 0; i < 500; i++)
			{
				string x = this.X;
				int num = ((x != null) ? x.RollCached() : Stat.Random(0, Z.Width - 1));
				string y = this.Y;
				int num2 = ((y != null) ? y.RollCached() : Stat.Random(0, Z.Height - 1));
				if (Z.GetCell(num, num2).IsEmpty())
				{
					GameObject cachedObjects = The.ZoneManager.GetCachedObjects(this.Object);
					Z.GetCell(num, num2).AddObject(cachedObjects, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					return true;
				}
			}
			return true;
		}

		// Token: 0x04003230 RID: 12848
		public string Object;

		// Token: 0x04003231 RID: 12849
		public string X;

		// Token: 0x04003232 RID: 12850
		public string Y;
	}
}
