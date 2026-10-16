using System;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A3A RID: 2618
	public class AddLocationFinder
	{
		// Token: 0x06006DB8 RID: 28088 RVA: 0x0024F200 File Offset: 0x0024D400
		public bool BuildZone(Zone Z)
		{
			GameObject gameObject = GameObject.Create("LocationFinder");
			LocationFinder locationFinder;
			if (gameObject.TryGetPart<LocationFinder>(out locationFinder))
			{
				locationFinder.ID = this.SecretID;
				locationFinder.Value = this.Value;
				Cell cell = Z.GetCell(0, 0);
				if (cell != null)
				{
					cell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
			}
			return true;
		}

		// Token: 0x0400322E RID: 12846
		public string SecretID;

		// Token: 0x0400322F RID: 12847
		public int Value;
	}
}
