using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A2E RID: 2606
	public class PlaceAClam : ZoneBuilderSandbox
	{
		// Token: 0x06006D85 RID: 28037 RVA: 0x0024C63B File Offset: 0x0024A83B
		public bool BuildZone(Zone Z)
		{
			GameObject gameObject = GameObject.Create("Giant Clam");
			gameObject.SetIntProperty("ClamId", this.clamNumber, false);
			ZoneBuilderSandbox.PlaceObject(gameObject, Z, "AdjacentToBlueprint:AlgalWaterDeepPool");
			return true;
		}

		// Token: 0x04003203 RID: 12803
		public int clamNumber;
	}
}
