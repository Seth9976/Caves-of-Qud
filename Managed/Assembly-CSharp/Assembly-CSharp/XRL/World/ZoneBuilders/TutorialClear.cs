using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A38 RID: 2616
	[Serializable]
	public class TutorialClear
	{
		// Token: 0x06006DB4 RID: 28084 RVA: 0x0024F0E4 File Offset: 0x0024D2E4
		public bool BuildZone(Zone Z)
		{
			if (The.Game.GetStringGameState("JoppaWorldTutorial", "") != "Yes")
			{
				Z.GetObjectsWithTagOrProperty("Tutorial", false).ForEach(delegate(GameObject o)
				{
					if (o != null)
					{
						o.Obliterate(null, true, null);
					}
				});
				Z.BuildReachableMap();
				if (!string.IsNullOrEmpty(this.ZoneTemplate))
				{
					ZoneTemplateManager.Templates[this.ZoneTemplate].Execute(Z, null);
				}
			}
			return true;
		}

		// Token: 0x0400322C RID: 12844
		public string ZoneTemplate;
	}
}
