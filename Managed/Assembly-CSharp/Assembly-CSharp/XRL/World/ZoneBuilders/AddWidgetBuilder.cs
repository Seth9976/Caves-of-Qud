using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A3E RID: 2622
	public class AddWidgetBuilder
	{
		// Token: 0x06006DC0 RID: 28096 RVA: 0x0024F578 File Offset: 0x0024D778
		public bool BuildZone(Zone Z)
		{
			if (!this.Blueprint.IsNullOrEmpty())
			{
				Cell cell = Z.GetCell(0, 0);
				if (cell != null)
				{
					cell.RequireObject(this.Blueprint, null, null, null, null);
				}
			}
			if (!this.Object.IsNullOrEmpty())
			{
				GameObject cachedObjects = The.ZoneManager.GetCachedObjects(this.Object);
				if (cachedObjects != null)
				{
					Cell cell2 = Z.GetCell(0, 0);
					if (cell2 != null)
					{
						cell2.AddObject(cachedObjects, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
				}
			}
			return true;
		}

		// Token: 0x04003238 RID: 12856
		public string Blueprint;

		// Token: 0x04003239 RID: 12857
		public string Object;
	}
}
